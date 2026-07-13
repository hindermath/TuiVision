// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>Persistierbarer StatusLine-Eintrag. / Persistable status-line item.</summary>
/// <param name="Label">Die Beschriftung. / The label.</param>
/// <param name="CommandId">Die Command-ID. / The command identifier.</param>
/// <param name="KeyCode">Der Tastencode oder 0. / The key code or zero.</param>
/// <param name="Disabled">Der statische Deaktivierungszustand. / The static disabled state.</param>
public sealed record TStatusItemDescriptionRecord(string Label, ushort CommandId, ushort KeyCode, bool Disabled);

/// <summary>Persistierbare Kontextdefinition. / Persistable context definition.</summary>
public sealed record TStatusDefinitionDescriptionRecord
{
    /// <summary>Erstellt eine Kontextdefinition. / Creates a context definition.</summary>
    /// <param name="minContext">Der kleinste Kontext. / The minimum context.</param>
    /// <param name="maxContext">Der größte Kontext. / The maximum context.</param>
    /// <param name="order">Die First-Match-Reihenfolge. / The first-match order.</param>
    /// <param name="items">Die Einträge. / The items.</param>
    public TStatusDefinitionDescriptionRecord(int minContext, int maxContext, int order, IEnumerable<TStatusItemDescriptionRecord> items)
    {
        MinContext = minContext;
        MaxContext = maxContext;
        Order = order;
        Items = items?.ToArray() ?? [];
    }

    /// <summary>Der kleinste Kontext. / The minimum context.</summary>
    public int MinContext { get; }
    /// <summary>Der größte Kontext. / The maximum context.</summary>
    public int MaxContext { get; }
    /// <summary>Die Reihenfolge. / The order.</summary>
    public int Order { get; }
    /// <summary>Die Einträge. / The items.</summary>
    public IReadOnlyList<TStatusItemDescriptionRecord> Items { get; }
}

/// <summary>Versionierte StatusLine-Beschreibung ohne Runtime-Objekte. / Versioned status-line description without runtime objects.</summary>
public sealed record TStatusLineDescriptionRecord : ITStreamSerializable
{
    /// <summary>Die stabile Typ-ID. / The stable type identifier.</summary>
    public const string TypeId = "tui.status-line-description";
    /// <summary>Die aktuelle Formatversion. / The current format version.</summary>
    public const int CurrentFormatVersion = 1;
    /// <summary>Die maximale Definitions- oder Eintragszahl. / The maximum definition or item count.</summary>
    public const int MaxItems = 4096;

    /// <summary>Erstellt einen Record. / Creates a record.</summary>
    /// <param name="formatVersion">Die Formatversion. / The format version.</param>
    /// <param name="definitions">Die Definitionen. / The definitions.</param>
    public TStatusLineDescriptionRecord(int formatVersion, IEnumerable<TStatusDefinitionDescriptionRecord> definitions)
    {
        FormatVersion = formatVersion;
        Definitions = definitions?.ToArray() ?? [];
    }

    /// <summary>Die Formatversion. / The format version.</summary>
    public int FormatVersion { get; }
    /// <summary>Die Definitionen. / The definitions.</summary>
    public IReadOnlyList<TStatusDefinitionDescriptionRecord> Definitions { get; }

    /// <inheritdoc />
    public void SaveTo(TBinaryArchiveWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        Validate(FormatVersion, Definitions);
        Write(writer.WriteInt32, writer.WriteString, writer.WriteBoolean, this);
    }

    /// <summary>Lädt einen Record. / Loads a record.</summary>
    /// <param name="reader">Der Reader. / The reader.</param>
    /// <returns>Der validierte Record. / The validated record.</returns>
    public static TStatusLineDescriptionRecord LoadFrom(TBinaryArchiveReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        return Read(reader.ReadInt32, reader.ReadString, reader.ReadBoolean);
    }

    /// <summary>Registriert Archiv- und Resource-Stream-Fabriken. / Registers archive and resource-stream factories.</summary>
    /// <param name="registry">Die Allowlist-Registry. / The allowlist registry.</param>
    public static void Register(TRecordRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);
        registry.Register(TypeId, LoadFrom);
        registry.RegisterStream(TypeId, LoadFromStream, SaveToStream);
    }

    internal static void Validate(int version, IReadOnlyList<TStatusDefinitionDescriptionRecord> definitions)
    {
        if (version != CurrentFormatVersion)
        {
            throw new InvalidDataException($"Unsupported status-line description format version '{version}'.");
        }

        if (definitions.Count > MaxItems || definitions.Sum(definition => definition.Items.Count) > MaxItems)
        {
            throw new InvalidDataException($"Status-line item count exceeds {MaxItems}.");
        }

        if (definitions.Select(definition => definition.Order).Distinct().Count() != definitions.Count)
        {
            throw new InvalidDataException("Status-line definition orders must be unique.");
        }

        foreach (TStatusDefinitionDescriptionRecord definition in definitions)
        {
            if (definition.MinContext < 0 || definition.MaxContext < definition.MinContext || definition.Order < 0)
            {
                throw new InvalidDataException("Status-line context range or order is invalid.");
            }

            foreach (TStatusItemDescriptionRecord item in definition.Items)
            {
                if (string.IsNullOrWhiteSpace(item.Label) || item.CommandId == 0)
                {
                    throw new InvalidDataException("Status-line labels must be non-blank and commands nonzero.");
                }
            }
        }
    }

    private static TStatusLineDescriptionRecord LoadFromStream(ipstream stream) =>
        Read(stream.ReadInt32, stream.ReadString, stream.ReadBoolean);

    private static void SaveToStream(opstream stream, TStatusLineDescriptionRecord record)
    {
        Validate(record.FormatVersion, record.Definitions);
        Write(stream.WriteInt32, stream.WriteString, stream.WriteBoolean, record);
    }

    private static void Write(Action<int> writeInt, Action<string> writeString, Action<bool> writeBoolean, TStatusLineDescriptionRecord record)
    {
        writeInt(record.FormatVersion);
        writeInt(record.Definitions.Count);
        foreach (TStatusDefinitionDescriptionRecord definition in record.Definitions)
        {
            writeInt(definition.MinContext);
            writeInt(definition.MaxContext);
            writeInt(definition.Order);
            writeInt(definition.Items.Count);
            foreach (TStatusItemDescriptionRecord item in definition.Items)
            {
                writeString(item.Label);
                writeInt(item.CommandId);
                writeInt(item.KeyCode);
                writeBoolean(item.Disabled);
            }
        }
    }

    private static TStatusLineDescriptionRecord Read(Func<int> readInt, Func<string> readString, Func<bool> readBoolean)
    {
        int version = readInt();
        int definitionCount = ReadCount(readInt(), "definition");
        List<TStatusDefinitionDescriptionRecord> definitions = new(definitionCount);
        int totalItems = 0;
        for (int index = 0; index < definitionCount; index++)
        {
            int min = readInt();
            int max = readInt();
            int order = readInt();
            int itemCount = ReadCount(readInt(), "item");
            totalItems += itemCount;
            if (totalItems > MaxItems)
            {
                throw new InvalidDataException($"Status-line item count exceeds {MaxItems}.");
            }

            List<TStatusItemDescriptionRecord> items = new(itemCount);
            for (int itemIndex = 0; itemIndex < itemCount; itemIndex++)
            {
                string label = readString();
                int command = readInt();
                int key = readInt();
                bool disabled = readBoolean();
                if (command is < 0 or > ushort.MaxValue || key is < 0 or > ushort.MaxValue)
                {
                    throw new InvalidDataException("Status-line command or key is outside UInt16 range.");
                }

                items.Add(new TStatusItemDescriptionRecord(label, (ushort)command, (ushort)key, disabled));
            }

            definitions.Add(new TStatusDefinitionDescriptionRecord(min, max, order, items));
        }

        Validate(version, definitions);
        return new TStatusLineDescriptionRecord(version, definitions);
    }

    private static int ReadCount(int count, string name)
    {
        if (count is < 0 or > MaxItems)
        {
            throw new InvalidDataException($"Invalid status-line {name} count '{count}'.");
        }

        return count;
    }
}
