// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Verwaltet Felder und Child-Sessions als eine rekursiv atomare Transaktion.
///
/// Manages fields and child sessions as one recursively atomic transaction.
/// </summary>
public sealed class FormSession
{
    private readonly List<IFormFieldRuntime> _fields = [];
    private readonly List<FormSession> _children = [];
    private readonly List<IFormControlAdapter> _adapters = [];
    private long _structureRevision;
    private int _submitActive;

    /// <summary>Erstellt eine FormSession. / Creates a form session.</summary>
    /// <param name="name">Der sessionslokale Name. / The session-local name.</param>
    public FormSession(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Contains('.'))
        {
            throw new ArgumentException("Session name must be non-empty and must not contain '.'.", nameof(name));
        }

        Name = name;
    }

    /// <summary>Der sessionslokale Name. / The session-local name.</summary>
    public string Name { get; }

    /// <summary>Die Felder in stabiler Einfügereihenfolge. / The fields in stable insertion order.</summary>
    public IReadOnlyList<IFormField> Fields => _fields;

    /// <summary>Die Child-Sessions in stabiler Einfügereihenfolge. / The child sessions in stable insertion order.</summary>
    public IReadOnlyList<FormSession> ChildSessions => _children;

    /// <summary>Ob ein Feld im rekursiven Baum geändert ist. / Whether any field in the recursive tree is modified.</summary>
    public bool IsModified => _fields.Any(item => item.IsModified) || _children.Any(child => child.IsModified);

    /// <summary>Ob alle zuletzt veröffentlichten Feldprüfungen gültig sind. / Whether all latest published field validations are valid.</summary>
    public bool IsValid => _fields.All(item => item.IsValid) && _children.All(child => child.IsValid);

    internal FormSession? Parent { get; private set; }

    /// <summary>Fügt ein Framework-Formularfeld hinzu. / Adds a framework form field.</summary>
    /// <typeparam name="T">Der Feldtyp. / The field type.</typeparam>
    /// <param name="field">Das Feld. / The field.</param>
    public void AddField<T>(FormField<T> field)
    {
        ArgumentNullException.ThrowIfNull(field);
        EnsureStructureMutable();
        EnsureNameAvailable(field.Name);
        IFormFieldRuntime runtime = field;
        if (runtime.Owner is not null)
        {
            throw new InvalidOperationException("A form field can belong to only one session.");
        }

        runtime.Owner = this;
        _fields.Add(runtime);
        _structureRevision++;
    }

    /// <summary>Fügt eine eindeutig besessene, azyklische Child-Session hinzu. / Adds a uniquely owned, acyclic child session.</summary>
    /// <param name="child">Die Child-Session. / The child session.</param>
    public void AddChild(FormSession child)
    {
        ArgumentNullException.ThrowIfNull(child);
        EnsureStructureMutable();
        child.EnsureStructureMutable();
        EnsureNameAvailable(child.Name);
        if (ReferenceEquals(child, this) || child.ContainsSession(this))
        {
            throw new InvalidOperationException("A child session must not create a cycle.");
        }

        if (child.Parent is not null)
        {
            throw new InvalidOperationException("A child session can have only one parent.");
        }

        child.Parent = this;
        _children.Add(child);
        _structureRevision++;
    }

    /// <summary>Bindet einen opt-in Control-Adapter an einen Feldbaum. / Attaches an opt-in control adapter to a field tree.</summary>
    /// <param name="adapter">Der Adapter. / The adapter.</param>
    public void AttachAdapter(IFormControlAdapter adapter)
    {
        ArgumentNullException.ThrowIfNull(adapter);
        EnsureStructureMutable();
        if (!ContainsField(adapter.Field))
        {
            throw new InvalidOperationException("The adapter field must belong to this session tree.");
        }

        FormSession root = GetRoot();
        if (root.ContainsAdapter(adapter))
        {
            throw new InvalidOperationException("The adapter is already attached.");
        }

        _adapters.Add(adapter);
        _structureRevision++;
    }

    /// <summary>
    /// Erzeugt das aktuelle, unveränderliche Change-Set.
    ///
    /// Creates the current immutable change set.
    /// </summary>
    /// <returns>Das Change-Set. / The change set.</returns>
    public FormChangeSet GetChangeSet()
    {
        PullAdaptersRecursively();
        SessionSnapshot snapshot = CaptureSnapshot();
        return CreateChangeSet(snapshot.Fields);
    }

    /// <summary>
    /// Validiert einen stabilen Snapshot synchron und asynchron, ohne Modell oder
    /// Baseline zu verändern. Gleichzeitige Submits werden deterministisch abgelehnt.
    ///
    /// Validates one stable snapshot synchronously and asynchronously without changing
    /// the model or baseline. Concurrent submits are rejected deterministically.
    /// </summary>
    /// <param name="cancellationToken">Das Abbruchsignal. / The cancellation signal.</param>
    /// <returns>Das Snapshot-Ergebnis. / The snapshot result.</returns>
    /// <exception cref="InvalidOperationException">Ein Submit läuft bereits. / A submit is already running.</exception>
    /// <exception cref="OperationCanceledException">Die Prüfung wurde abgebrochen. / Validation was cancelled.</exception>
    public async Task<FormSubmitResult> SubmitAsync(CancellationToken cancellationToken = default)
    {
        if (Parent is not null)
        {
            throw new InvalidOperationException("Submit the root session to preserve the atomic child-session boundary.");
        }

        if (Interlocked.CompareExchange(ref _submitActive, 1, 0) != 0)
        {
            throw new InvalidOperationException("A submit operation is already active for this session.");
        }

        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            PullAdaptersRecursively();
            SessionSnapshot snapshot = CaptureSnapshot();
            List<FormValidationError> allErrors = [];
            Dictionary<IFormFieldRuntime, IReadOnlyList<FormValidationError>> fieldErrors = [];
            foreach (FormFieldSnapshot fieldSnapshot in snapshot.Fields)
            {
                IReadOnlyList<FormValidationError> errors = await fieldSnapshot.Field
                    .ValidateSnapshotAsync(fieldSnapshot, cancellationToken)
                    .ConfigureAwait(false);
                fieldErrors.Add(fieldSnapshot.Field, errors);
                allErrors.AddRange(errors);
            }

            FormChangeSet changeSet = CreateChangeSet(snapshot.Fields);
            if (HasDrift(snapshot))
            {
                // Alte Async-Ergebnisse duerfen den sichtbaren Feldstatus nicht ueberschreiben.
                // Stale async results must not overwrite the visible field status.
                return new FormSubmitResult(FormSubmitStatus.Stale, changeSet, []);
            }

            foreach ((IFormFieldRuntime field, IReadOnlyList<FormValidationError> errors) in fieldErrors)
            {
                field.PublishErrors(errors);
            }

            FormSubmitStatus status = allErrors.Count == 0
                ? FormSubmitStatus.Success
                : FormSubmitStatus.ValidationFailed;
            return new FormSubmitResult(status, changeSet, allErrors);
        }
        finally
        {
            Volatile.Write(ref _submitActive, 0);
        }
    }

    /// <summary>
    /// Wendet geänderte Bindings an und verschiebt danach alle Baselines atomar.
    ///
    /// Applies modified bindings and then advances all baselines atomically.
    /// </summary>
    /// <exception cref="InvalidOperationException">Ein Submit läuft. / A submit is active.</exception>
    /// <exception cref="FormBindingCommitException">Apply oder Rollback schlug fehl. / Apply or rollback failed.</exception>
    public void AcceptChanges()
    {
        EnsureStructureMutable();
        PullAdaptersRecursively();
        List<IFormFieldRuntime> fields = [];
        CollectFields(fields);
        List<(IFormFieldRuntime Field, object? Captured)> attempted = [];

        foreach (IFormFieldRuntime field in fields)
        {
            if (!field.HasBinding || !field.IsModified)
            {
                continue;
            }

            try
            {
                object? captured = field.CaptureModelValue();
                attempted.Add((field, captured));
                field.ApplyModelValue();
            }
            catch (Exception exception)
            {
                List<Exception> rollbackErrors = RollBack(attempted);
                throw new FormBindingCommitException(
                    field.Name,
                    exception,
                    rollbackErrors,
                    field.GetBindingValidationError(exception));
            }
        }

        foreach (IFormFieldRuntime field in fields)
        {
            field.AcceptBaseline();
        }

        PushAdaptersRecursively();
    }

    /// <summary>Stellt alle Baselines rekursiv wieder her. / Restores all baselines recursively.</summary>
    /// <exception cref="InvalidOperationException">Ein Submit läuft. / A submit is active.</exception>
    public void RejectChanges()
    {
        EnsureStructureMutable();
        foreach (IFormFieldRuntime field in _fields)
        {
            field.RejectBaseline();
        }

        foreach (FormSession child in _children)
        {
            child.RejectChangesCore();
        }

        PushAdaptersRecursively();
    }

    private void RejectChangesCore()
    {
        foreach (IFormFieldRuntime field in _fields)
        {
            field.RejectBaseline();
        }

        foreach (FormSession child in _children)
        {
            child.RejectChangesCore();
        }
    }

    private SessionSnapshot CaptureSnapshot()
    {
        List<FormFieldSnapshot> fields = [];
        List<(FormSession Session, long Revision)> sessions = [];
        CaptureSnapshot(string.Empty, fields, sessions);
        return new SessionSnapshot(fields, sessions);
    }

    private void CaptureSnapshot(
        string prefix,
        ICollection<FormFieldSnapshot> fields,
        ICollection<(FormSession Session, long Revision)> sessions)
    {
        sessions.Add((this, _structureRevision));
        foreach (IFormFieldRuntime field in _fields)
        {
            string path = string.IsNullOrEmpty(prefix) ? field.Name : $"{prefix}.{field.Name}";
            fields.Add(field.Capture(path));
        }

        foreach (FormSession child in _children)
        {
            string childPrefix = string.IsNullOrEmpty(prefix) ? child.Name : $"{prefix}.{child.Name}";
            child.CaptureSnapshot(childPrefix, fields, sessions);
        }
    }

    private static FormChangeSet CreateChangeSet(IEnumerable<FormFieldSnapshot> fields) =>
        new(fields
            .Where(snapshot => snapshot.Field.IsSnapshotModified(snapshot))
            .Select(snapshot => new FormChange(snapshot.Path, snapshot.OriginalValue, snapshot.CurrentValue)));

    private static bool HasDrift(SessionSnapshot snapshot) =>
        snapshot.Fields.Any(field => field.Field.Revision != field.Revision)
        || snapshot.Sessions.Any(item => item.Session._structureRevision != item.Revision);

    private static List<Exception> RollBack(
        IReadOnlyList<(IFormFieldRuntime Field, object? Captured)> attempted)
    {
        List<Exception> errors = [];
        for (int index = attempted.Count - 1; index >= 0; index--)
        {
            try
            {
                attempted[index].Field.RestoreModelValue(attempted[index].Captured);
            }
            catch (Exception exception)
            {
                errors.Add(exception);
            }
        }

        return errors;
    }

    private void CollectFields(ICollection<IFormFieldRuntime> target)
    {
        foreach (IFormFieldRuntime field in _fields)
        {
            target.Add(field);
        }

        foreach (FormSession child in _children)
        {
            child.CollectFields(target);
        }
    }

    private void PullAdaptersRecursively()
    {
        foreach (IFormControlAdapter adapter in _adapters)
        {
            adapter.PullFromControl();
        }

        foreach (FormSession child in _children)
        {
            child.PullAdaptersRecursively();
        }
    }

    private void PushAdaptersRecursively()
    {
        foreach (IFormControlAdapter adapter in _adapters)
        {
            adapter.PushToControl();
        }

        foreach (FormSession child in _children)
        {
            child.PushAdaptersRecursively();
        }
    }

    private bool ContainsField(IFormField field) =>
        _fields.Any(candidate => ReferenceEquals(candidate, field))
        || _children.Any(child => child.ContainsField(field));

    private bool ContainsAdapter(IFormControlAdapter adapter) =>
        _adapters.Any(candidate => ReferenceEquals(candidate, adapter))
        || _children.Any(child => child.ContainsAdapter(adapter));

    private FormSession GetRoot()
    {
        FormSession root = this;
        while (root.Parent is not null)
        {
            root = root.Parent;
        }

        return root;
    }

    private bool ContainsSession(FormSession session) =>
        ReferenceEquals(this, session) || _children.Any(child => child.ContainsSession(session));

    private void EnsureNameAvailable(string name)
    {
        if (_fields.Any(field => string.Equals(field.Name, name, StringComparison.Ordinal))
            || _children.Any(child => string.Equals(child.Name, name, StringComparison.Ordinal)))
        {
            throw new InvalidOperationException($"Duplicate form member name '{name}'.");
        }
    }

    private void EnsureStructureMutable()
    {
        if (Volatile.Read(ref _submitActive) != 0 || HasActiveSubmitInAncestors() || HasActiveSubmitInDescendants())
        {
            throw new InvalidOperationException("The session cannot be mutated while a submit is active.");
        }
    }

    private bool HasActiveSubmitInAncestors() =>
        Parent is not null && (Volatile.Read(ref Parent._submitActive) != 0 || Parent.HasActiveSubmitInAncestors());

    private bool HasActiveSubmitInDescendants() =>
        _children.Any(child => Volatile.Read(ref child._submitActive) != 0 || child.HasActiveSubmitInDescendants());

    private sealed record SessionSnapshot(
        IReadOnlyList<FormFieldSnapshot> Fields,
        IReadOnlyList<(FormSession Session, long Revision)> Sessions);
}
