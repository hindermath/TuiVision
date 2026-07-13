// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Persistierbarer Menüeintrag aus primitiven, allowlistbaren Werten.
///
/// Persistable menu item made of primitive, allowlisted values.
/// </summary>
/// <param name="Id">Die stabile ID. / The stable identifier.</param>
/// <param name="ParentId">Die optionale Eltern-ID. / The optional parent identifier.</param>
/// <param name="Order">Die Geschwisterreihenfolge. / The sibling order.</param>
/// <param name="Label">Die Beschriftung. / The label.</param>
/// <param name="CommandId">Die Command-ID oder 0 für Container/Separator. / The command ID or zero for containers/separators.</param>
/// <param name="HelpContext">Der Hilfekontext. / The help context.</param>
/// <param name="Disabled">Der statische Deaktivierungszustand. / The static disabled state.</param>
public sealed record TMenuItemDescriptionRecord(
    string Id,
    string? ParentId,
    int Order,
    string Label,
    ushort CommandId,
    int HelpContext,
    bool Disabled);

/// <summary>
/// Versionierte persistierbare Menübeschreibung ohne Runtime-Objekte.
///
/// Versioned persistable menu description without runtime objects.
/// </summary>
public sealed record TMenuDescriptionRecord : ITStreamSerializable
{
    /// <summary>Die stabile Typ-ID. / The stable type identifier.</summary>
    public const string TypeId = "tui.menu-description";

    /// <summary>Die aktuell unterstützte Formatversion. / The currently supported format version.</summary>
    public const int CurrentFormatVersion = 1;

    /// <summary>Die maximale Eintragszahl. / The maximum item count.</summary>
    public const int MaxItems = 4096;

    /// <summary>
    /// Erstellt einen Record.
    ///
    /// Creates a record.
    /// </summary>
    /// <param name="formatVersion">Die Formatversion. / The format version.</param>
    /// <param name="items">Die Einträge. / The items.</param>
    public TMenuDescriptionRecord(int formatVersion, IEnumerable<TMenuItemDescriptionRecord> items)
    {
        FormatVersion = formatVersion;
        Items = items?.ToArray() ?? [];
    }

    /// <summary>Die Formatversion. / The format version.</summary>
    public int FormatVersion { get; }

    /// <summary>Die unveränderliche Eintragsfolge. / The immutable item sequence.</summary>
    public IReadOnlyList<TMenuItemDescriptionRecord> Items { get; }

    /// <inheritdoc />
    public void SaveTo(TBinaryArchiveWriter writer)
    {
        ArgumentNullException.ThrowIfNull(writer);
        Validate(FormatVersion, Items);
        writer.WriteInt32(FormatVersion);
        writer.WriteInt32(Items.Count);
        foreach (TMenuItemDescriptionRecord item in Items)
        {
            WriteItem(writer.WriteString, writer.WriteInt32, writer.WriteBoolean, item);
        }
    }

    /// <summary>Lädt einen Record aus dem Archiv. / Loads a record from the archive.</summary>
    /// <param name="reader">Der Reader. / The reader.</param>
    /// <returns>Der validierte Record. / The validated record.</returns>
    public static TMenuDescriptionRecord LoadFrom(TBinaryArchiveReader reader)
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

    internal static void Validate(int version, IReadOnlyList<TMenuItemDescriptionRecord> items)
    {
        if (version != CurrentFormatVersion)
        {
            throw new InvalidDataException($"Unsupported menu description format version '{version}'.");
        }

        if (items.Count > MaxItems)
        {
            throw new InvalidDataException($"Menu item count exceeds {MaxItems}.");
        }

        Dictionary<string, TMenuItemDescriptionRecord> byId = new(StringComparer.Ordinal);
        foreach (TMenuItemDescriptionRecord item in items)
        {
            if (string.IsNullOrWhiteSpace(item.Id) || !byId.TryAdd(item.Id, item))
            {
                throw new InvalidDataException("Menu item IDs must be non-blank and unique.");
            }

            if (item.Order < 0 || string.IsNullOrWhiteSpace(item.Label) || item.HelpContext < 0)
            {
                throw new InvalidDataException($"Menu item '{item.Id}' has invalid order, label, or help context.");
            }
        }

        foreach (TMenuItemDescriptionRecord item in items)
        {
            if (item.ParentId is not null && !byId.ContainsKey(item.ParentId))
            {
                throw new InvalidDataException($"Menu item '{item.Id}' references unknown parent '{item.ParentId}'.");
            }
        }

        foreach (IGrouping<string?, TMenuItemDescriptionRecord> siblings in items.GroupBy(item => item.ParentId, StringComparer.Ordinal))
        {
            if (siblings.Select(item => item.Order).Distinct().Count() != siblings.Count())
            {
                throw new InvalidDataException("Sibling menu orders must be unique.");
            }
        }

        foreach (TMenuItemDescriptionRecord item in items)
        {
            bool hasChildren = items.Any(candidate => string.Equals(candidate.ParentId, item.Id, StringComparison.Ordinal));
            bool isSeparator = item.Label == "---";
            if ((!hasChildren && !isSeparator && item.CommandId == 0) || ((hasChildren || isSeparator) && item.CommandId != 0))
            {
                throw new InvalidDataException($"Menu item '{item.Id}' has an invalid command role.");
            }

            HashSet<string> visited = new(StringComparer.Ordinal);
            TMenuItemDescriptionRecord current = item;
            int depth = 1;
            while (current.ParentId is not null)
            {
                if (!visited.Add(current.Id) || ++depth > 16)
                {
                    throw new InvalidDataException("Menu graph is cyclic or exceeds depth 16.");
                }

                current = byId[current.ParentId];
            }
        }
    }

    private static TMenuDescriptionRecord LoadFromStream(ipstream stream) =>
        Read(stream.ReadInt32, stream.ReadString, stream.ReadBoolean);

    private static void SaveToStream(opstream stream, TMenuDescriptionRecord record)
    {
        Validate(record.FormatVersion, record.Items);
        stream.WriteInt32(record.FormatVersion);
        stream.WriteInt32(record.Items.Count);
        foreach (TMenuItemDescriptionRecord item in record.Items)
        {
            WriteItem(stream.WriteString, stream.WriteInt32, stream.WriteBoolean, item);
        }
    }

    private static TMenuDescriptionRecord Read(Func<int> readInt, Func<string> readString, Func<bool> readBoolean)
    {
        int version = readInt();
        int count = ReadCount(readInt());
        List<TMenuItemDescriptionRecord> items = new(count);
        for (int index = 0; index < count; index++)
        {
            string id = readString();
            string? parentId = readBoolean() ? readString() : null;
            int order = readInt();
            string label = readString();
            int command = readInt();
            int helpContext = readInt();
            bool disabled = readBoolean();
            if (command is < 0 or > ushort.MaxValue)
            {
                throw new InvalidDataException("Menu command is outside UInt16 range.");
            }

            items.Add(new TMenuItemDescriptionRecord(id, parentId, order, label, (ushort)command, helpContext, disabled));
        }

        Validate(version, items);
        return new TMenuDescriptionRecord(version, items);
    }

    private static void WriteItem(Action<string> writeString, Action<int> writeInt, Action<bool> writeBoolean, TMenuItemDescriptionRecord item)
    {
        writeString(item.Id);
        writeBoolean(item.ParentId is not null);
        if (item.ParentId is not null)
        {
            writeString(item.ParentId);
        }

        writeInt(item.Order);
        writeString(item.Label);
        writeInt(item.CommandId);
        writeInt(item.HelpContext);
        writeBoolean(item.Disabled);
    }

    private static int ReadCount(int count)
    {
        if (count is < 0 or > MaxItems)
        {
            throw new InvalidDataException($"Invalid menu item count '{count}'.");
        }

        return count;
    }
}
