// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Persistierbare Control-Beschreibung ohne Controls-Abhaengigkeit.
///
/// Persistable control description without Controls dependency.
/// </summary>
/// <param name="ControlId">Die Control-ID. / The control identifier.</param>
/// <param name="Role">Die Control-Rolle. / The control role.</param>
/// <param name="Label">Die Beschriftung. / The label.</param>
/// <param name="InitialValue">Der Anfangswert. / The initial value.</param>
/// <param name="CanFocus">Ob das Control fokussierbar ist. / Whether the control can receive focus.</param>
public sealed record TDialogControlDescriptionRecord(
    string ControlId,
    string Role,
    string Label,
    string? InitialValue,
    bool CanFocus);

/// <summary>
/// Persistierbare Command-Bindung ohne Controls-Abhaengigkeit.
///
/// Persistable command binding without Controls dependency.
/// </summary>
/// <param name="CommandId">Die Command-ID. / The command identifier.</param>
/// <param name="TargetControlId">Die optionale Ziel-Control-ID. / The optional target control identifier.</param>
/// <param name="Meaning">Die Bedeutung. / The meaning.</param>
/// <param name="KeyboardTrigger">Der Tastaturausloeser. / The keyboard trigger.</param>
public sealed record TDialogCommandBindingRecord(
    ushort CommandId,
    string? TargetControlId,
    string Meaning,
    string KeyboardTrigger);

/// <summary>
/// Persistierbare Minimaldarstellung einer Dialogbeschreibung.
///
/// Persistable minimal representation of a dialog description.
/// </summary>
public sealed record TDialogDescriptionRecord : ITStreamSerializable
{
    private const int MaxItems = 4096;

    /// <summary>
    /// Erstellt einen Dialogbeschreibungs-Record.
    ///
    /// Creates a dialog-description record.
    /// </summary>
    /// <param name="formatVersion">Die Formatversion. / The format version.</param>
    /// <param name="descriptionId">Die Beschreibungs-ID. / The description identifier.</param>
    /// <param name="modelVersion">Die Modellversion. / The model version.</param>
    /// <param name="title">Der Dialogtitel. / The dialog title.</param>
    /// <param name="controls">Die Controls. / The controls.</param>
    /// <param name="navigationOrder">Die Navigationsreihenfolge. / The navigation order.</param>
    /// <param name="commandBindings">Die Command-Bindungen. / The command bindings.</param>
    /// <param name="runtimeStateIncluded">Ob Runtime-Zustand enthalten ist. / Whether runtime state is included.</param>
    public TDialogDescriptionRecord(
        int formatVersion,
        string descriptionId,
        int modelVersion,
        string title,
        IEnumerable<TDialogControlDescriptionRecord> controls,
        IEnumerable<string> navigationOrder,
        IEnumerable<TDialogCommandBindingRecord> commandBindings,
        bool runtimeStateIncluded = false)
    {
        FormatVersion = formatVersion;
        DescriptionId = descriptionId ?? string.Empty;
        ModelVersion = modelVersion;
        Title = title ?? string.Empty;
        Controls = controls?.ToArray() ?? [];
        NavigationOrder = navigationOrder?.ToArray() ?? [];
        CommandBindings = commandBindings?.ToArray() ?? [];
        RuntimeStateIncluded = runtimeStateIncluded;
    }

    /// <summary>Die Formatversion. / The format version.</summary>
    public int FormatVersion { get; }

    /// <summary>Die Beschreibungs-ID. / The description identifier.</summary>
    public string DescriptionId { get; }

    /// <summary>Die Modellversion. / The model version.</summary>
    public int ModelVersion { get; }

    /// <summary>Der Dialogtitel. / The dialog title.</summary>
    public string Title { get; }

    /// <summary>Die Control-Records. / The control records.</summary>
    public IReadOnlyList<TDialogControlDescriptionRecord> Controls { get; }

    /// <summary>Die Navigationsreihenfolge. / The navigation order.</summary>
    public IReadOnlyList<string> NavigationOrder { get; }

    /// <summary>Die Command-Bindungen. / The command bindings.</summary>
    public IReadOnlyList<TDialogCommandBindingRecord> CommandBindings { get; }

    /// <summary>Ob Runtime-Zustand enthalten ist. / Whether runtime state is included.</summary>
    public bool RuntimeStateIncluded { get; }

    /// <summary>
    /// Speichert den Record in einen Archiv-Writer.
    ///
    /// Saves the record to an archive writer.
    /// </summary>
    /// <param name="writer">Der Writer. / The writer.</param>
    public void SaveTo(TBinaryArchiveWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        writer.WriteInt32(FormatVersion);
        writer.WriteString(DescriptionId);
        writer.WriteInt32(ModelVersion);
        writer.WriteString(Title);
        writer.WriteBoolean(RuntimeStateIncluded);

        writer.WriteInt32(Controls.Count);
        foreach (TDialogControlDescriptionRecord control in Controls)
        {
            writer.WriteString(control.ControlId);
            writer.WriteString(control.Role);
            writer.WriteString(control.Label);
            writer.WriteBoolean(control.InitialValue is not null);
            if (control.InitialValue is not null)
            {
                writer.WriteString(control.InitialValue);
            }

            writer.WriteBoolean(control.CanFocus);
        }

        writer.WriteInt32(NavigationOrder.Count);
        foreach (string item in NavigationOrder)
        {
            writer.WriteString(item);
        }

        writer.WriteInt32(CommandBindings.Count);
        foreach (TDialogCommandBindingRecord binding in CommandBindings)
        {
            writer.WriteUInt16(binding.CommandId);
            writer.WriteBoolean(binding.TargetControlId is not null);
            if (binding.TargetControlId is not null)
            {
                writer.WriteString(binding.TargetControlId);
            }

            writer.WriteString(binding.Meaning);
            writer.WriteString(binding.KeyboardTrigger);
        }
    }

    /// <summary>
    /// Laedt einen Record aus einem Archiv-Reader.
    ///
    /// Loads a record from an archive reader.
    /// </summary>
    /// <param name="reader">Der Reader. / The reader.</param>
    /// <returns>Der geladene Record. / The loaded record.</returns>
    public static TDialogDescriptionRecord LoadFrom(TBinaryArchiveReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        int formatVersion = reader.ReadInt32();
        if (formatVersion != PersistedDialogRepresentation.CurrentFormatVersion)
        {
            throw new InvalidDataException($"Unsupported dialog description format version '{formatVersion}'.");
        }

        string descriptionId = reader.ReadString();
        int modelVersion = reader.ReadInt32();
        string title = reader.ReadString();
        bool runtimeStateIncluded = reader.ReadBoolean();
        if (runtimeStateIncluded)
        {
            throw new InvalidDataException("Runtime state is not supported in persisted dialog descriptions.");
        }

        int controlCount = ReadCount(reader, "control");
        List<TDialogControlDescriptionRecord> controls = new(controlCount);
        for (int index = 0; index < controlCount; index++)
        {
            string controlId = reader.ReadString();
            string role = reader.ReadString();
            string label = reader.ReadString();
            string? initialValue = reader.ReadBoolean() ? reader.ReadString() : null;
            bool canFocus = reader.ReadBoolean();
            controls.Add(new TDialogControlDescriptionRecord(controlId, role, label, initialValue, canFocus));
        }

        int navigationCount = ReadCount(reader, "navigation");
        List<string> navigationOrder = new(navigationCount);
        for (int index = 0; index < navigationCount; index++)
        {
            navigationOrder.Add(reader.ReadString());
        }

        int bindingCount = ReadCount(reader, "command");
        List<TDialogCommandBindingRecord> bindings = new(bindingCount);
        for (int index = 0; index < bindingCount; index++)
        {
            ushort commandId = reader.ReadUInt16();
            string? targetControlId = reader.ReadBoolean() ? reader.ReadString() : null;
            string meaning = reader.ReadString();
            string keyboardTrigger = reader.ReadString();
            bindings.Add(new TDialogCommandBindingRecord(commandId, targetControlId, meaning, keyboardTrigger));
        }

        return new TDialogDescriptionRecord(
            formatVersion,
            descriptionId,
            modelVersion,
            title,
            controls,
            navigationOrder,
            bindings,
            runtimeStateIncluded);
    }

    /// <summary>
    /// Registriert den Dialogbeschreibungs-Record.
    ///
    /// Registers the dialog-description record.
    /// </summary>
    /// <param name="registry">Die Registry. / The registry.</param>
    public static void Register(TRecordRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);
        registry.Register(PersistedDialogRepresentation.TypeId, LoadFrom);
    }

    private static int ReadCount(TBinaryArchiveReader reader, string name)
    {
        int count = reader.ReadInt32();
        if (count < 0 || count > MaxItems)
        {
            throw new InvalidDataException($"Invalid {name} count '{count}'.");
        }

        return count;
    }
}
