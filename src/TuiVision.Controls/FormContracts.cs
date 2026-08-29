// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Beschreibt einen frameworkneutralen Formularfehler.
///
/// Describes a framework-neutral form error.
/// </summary>
/// <param name="Code">Der stabile Fehlercode. / The stable error code.</param>
/// <param name="Message">Die textorientierte Meldung. / The text-first message.</param>
/// <param name="FieldName">Der optionale vollständige Feldpfad. / The optional full field path.</param>
public sealed record FormValidationError(string Code, string Message, string? FieldName = null);

/// <summary>
/// Beschreibt eine unveränderliche Feldänderung.
///
/// Describes one immutable field change.
/// </summary>
/// <param name="Name">Der vollständige Feldpfad. / The full field path.</param>
/// <param name="OriginalValue">Der Baseline-Wert. / The baseline value.</param>
/// <param name="CurrentValue">Der aktuelle Snapshot-Wert. / The current snapshot value.</param>
public sealed record FormChange(string Name, object? OriginalValue, object? CurrentValue);

/// <summary>
/// Enthält eine stabil geordnete, unveränderliche Änderungsliste.
///
/// Contains a stable, immutable list of changes.
/// </summary>
public sealed class FormChangeSet
{
    /// <summary>Erstellt ein Change-Set. / Creates a change set.</summary>
    /// <param name="changes">Die Änderungen. / The changes.</param>
    public FormChangeSet(IEnumerable<FormChange> changes) => Changes = changes?.ToArray() ?? [];

    /// <summary>Die Änderungen. / The changes.</summary>
    public IReadOnlyList<FormChange> Changes { get; }
}

/// <summary>
/// Klassifiziert ein Submit-Ergebnis.
///
/// Classifies a submit result.
/// </summary>
public enum FormSubmitStatus
{
    /// <summary>Der Snapshot ist gültig. / The snapshot is valid.</summary>
    Success,

    /// <summary>Der Snapshot enthält Validierungsfehler. / The snapshot contains validation errors.</summary>
    ValidationFailed,

    /// <summary>Die Session änderte sich während der Prüfung. / The session changed during validation.</summary>
    Stale
}

/// <summary>
/// Liefert Status, Change-Set und Fehler eines Submit-Snapshots.
///
/// Provides the status, change set, and errors of a submit snapshot.
/// </summary>
public sealed class FormSubmitResult
{
    /// <summary>Erstellt ein Submit-Ergebnis. / Creates a submit result.</summary>
    /// <param name="status">Der Status. / The status.</param>
    /// <param name="changeSet">Das Snapshot-Change-Set. / The snapshot change set.</param>
    /// <param name="errors">Die Fehler. / The errors.</param>
    public FormSubmitResult(FormSubmitStatus status, FormChangeSet changeSet, IEnumerable<FormValidationError> errors)
    {
        Status = status;
        ChangeSet = changeSet ?? throw new ArgumentNullException(nameof(changeSet));
        Errors = errors?.ToArray() ?? [];
    }

    /// <summary>Der Submit-Status. / The submit status.</summary>
    public FormSubmitStatus Status { get; }

    /// <summary>Das unveränderliche Change-Set. / The immutable change set.</summary>
    public FormChangeSet ChangeSet { get; }

    /// <summary>Die stabil geordneten Fehler. / The stable ordered errors.</summary>
    public IReadOnlyList<FormValidationError> Errors { get; }
}

/// <summary>
/// Definiert die untypisierte Formularfeldoberfläche.
///
/// Defines the untyped form-field surface.
/// </summary>
public interface IFormField
{
    /// <summary>Der sessionslokale Name. / The session-local name.</summary>
    string Name { get; }

    /// <summary>Der aktuelle Wert. / The current value.</summary>
    object? UntypedValue { get; }

    /// <summary>Der Baseline-Wert. / The baseline value.</summary>
    object? UntypedOriginalValue { get; }

    /// <summary>Ob der Wert von der Baseline abweicht. / Whether the value differs from the baseline.</summary>
    bool IsModified { get; }

    /// <summary>Ob die zuletzt veröffentlichte Prüfung gültig ist. / Whether the latest published validation is valid.</summary>
    bool IsValid { get; }

    /// <summary>Die zuletzt veröffentlichten Fehler. / The latest published errors.</summary>
    IReadOnlyList<FormValidationError> ValidationErrors { get; }

    /// <summary>Übernimmt den aktuellen Wert als Baseline. / Accepts the current value as the baseline.</summary>
    void AcceptChanges();

    /// <summary>Stellt die Baseline wieder her. / Restores the baseline.</summary>
    void RejectChanges();
}

/// <summary>
/// Definiert ein typsicheres Formularfeld.
///
/// Defines a type-safe form field.
/// </summary>
/// <typeparam name="T">Der Werttyp. / The value type.</typeparam>
public interface IFormField<T> : IFormField
{
    /// <summary>Der aktuelle Wert. / The current value.</summary>
    T Value { get; set; }

    /// <summary>Der Baseline-Wert. / The baseline value.</summary>
    T OriginalValue { get; }
}

/// <summary>
/// Meldet einen Binding-Fehler samt bestmöglicher Rollback-Evidence.
///
/// Reports a binding failure together with best-effort rollback evidence.
/// </summary>
public sealed class FormBindingCommitException : InvalidOperationException
{
    /// <summary>Erstellt die Ausnahme. / Creates the exception.</summary>
    /// <param name="fieldName">Das auslösende Feld. / The failing field.</param>
    /// <param name="innerException">Der Apply-Fehler. / The apply failure.</param>
    /// <param name="rollbackErrors">Fehler aus dem bestmöglichen Rollback. / Best-effort rollback failures.</param>
    /// <param name="validationError">Der optionale Konvertierungsfehler. / The optional conversion error.</param>
    public FormBindingCommitException(
        string fieldName,
        Exception innerException,
        IEnumerable<Exception> rollbackErrors,
        FormValidationError? validationError = null)
        : base(
            $"Binding commit failed for field '{fieldName}'. Baselines were not changed; setter side effects outside the property contract may remain.",
            innerException)
    {
        FieldName = fieldName;
        RollbackErrors = rollbackErrors?.ToArray() ?? [];
        ValidationError = validationError;
    }

    /// <summary>Das auslösende Feld. / The failing field.</summary>
    public string FieldName { get; }

    /// <summary>Die Rollback-Fehler. / The rollback failures.</summary>
    public IReadOnlyList<Exception> RollbackErrors { get; }

    /// <summary>Der optionale Konvertierungsfehler. / The optional conversion error.</summary>
    public FormValidationError? ValidationError { get; }
}

internal sealed record FormFieldSnapshot(
    IFormFieldRuntime Field,
    string Path,
    object? OriginalValue,
    object? CurrentValue,
    long Revision);

internal interface IFormFieldRuntime : IFormField
{
    FormSession? Owner { get; set; }

    long Revision { get; }

    bool HasBinding { get; }

    FormFieldSnapshot Capture(string path);

    bool IsSnapshotModified(FormFieldSnapshot snapshot);

    ValueTask<IReadOnlyList<FormValidationError>> ValidateSnapshotAsync(
        FormFieldSnapshot snapshot,
        CancellationToken cancellationToken);

    void PublishErrors(IReadOnlyList<FormValidationError> errors);

    object? CaptureModelValue();

    void ApplyModelValue();

    void RestoreModelValue(object? value);

    FormValidationError? GetBindingValidationError(Exception exception);

    void AcceptBaseline();

    void RejectBaseline();
}
