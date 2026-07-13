// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Persistierbarer Container fuer benannte Objekte mit exakter Schluesselauflosung.
///
/// Persistable container for named objects with exact key resolution.
/// </summary>
public sealed class TResourceFile
{
    private const int MaxEntries = 4096;
    private const int MaxPayloadBytes = 4 * 1024 * 1024;
    private readonly TRecordRegistry _registry;

    /// <summary>
    /// Initialisiert eine neue Ressourcen-Datei.
    ///
    /// Initializes a new resource file.
    /// </summary>
    /// <param name="registry">Die Typ-Registry. / The type registry.</param>
    public TResourceFile(TRecordRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        Resources = new TResourceCollection();
    }

    /// <summary>
    /// Der Ressourcenkatalog.
    ///
    /// The resource catalog.
    /// </summary>
    public TResourceCollection Resources { get; }

    /// <summary>
    /// Registriert die eingebauten Persistenztypen fuer Ressourcen und Hilfe.
    ///
    /// Registers the built-in persistence types for resources and help.
    /// </summary>
    /// <param name="registry">Die zu erweiternde Registry. / The registry to extend.</param>
    public static void RegisterBuiltInTypes(TRecordRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);
        THelpFile.RegisterRecords(registry);
        TDialogDescriptionRecord.Register(registry);
        TDialogDescriptionRecord.RegisterResourceStream(registry);
        TMenuDescriptionRecord.Register(registry);
        TStatusLineDescriptionRecord.Register(registry);
    }

    /// <summary>
    /// Speichert oder ersetzt einen Eintrag.
    ///
    /// Stores or replaces an entry.
    /// </summary>
    /// <param name="key">Der Ressourcenkey. / The resource key.</param>
    /// <param name="value">Der Ressourcenwert. / The resource value.</param>
    public void Put(string key, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(value);
        Resources.Set(key, value);
    }

    /// <summary>
    /// Holt einen Eintrag typisiert.
    ///
    /// Retrieves an entry as a typed value.
    /// </summary>
    /// <typeparam name="T">Der Zieltyp. / The target type.</typeparam>
    /// <param name="key">Der Ressourcenkey. / The resource key.</param>
    /// <returns>Der Ressourcenwert oder <c>null</c>. / The resource value or <c>null</c>.</returns>
    public T? Get<T>(string key) where T : class
    {
        return Resources.TryGet<T>(key, out T? value) ? value : null;
    }

    /// <summary>
    /// Entfernt einen Eintrag.
    ///
    /// Removes an entry.
    /// </summary>
    /// <param name="key">Der Ressourcenkey. / The resource key.</param>
    /// <returns><c>true</c>, wenn entfernt wurde. / <c>true</c> if the entry was removed.</returns>
    public bool Remove(string key) => Resources.Remove(key);

    /// <summary>
    /// Persistiert den Katalog in einen Stream.
    ///
    /// Persists the catalog into a stream.
    /// </summary>
    /// <param name="stream">Der Zielstream. / The destination stream.</param>
    public void Save(Stream stream)
    {
        using TBinaryArchiveWriter writer = new(stream, leaveOpen: true);
        List<KeyValuePair<string, object>> entries = Resources.Enumerate().OrderBy(pair => pair.Key, StringComparer.Ordinal).ToList();
        if (entries.Count > MaxEntries)
        {
            throw new InvalidDataException($"Resource entry count exceeds {MaxEntries}.");
        }

        writer.WriteInt32(entries.Count);
        foreach (KeyValuePair<string, object> entry in entries)
        {
            writer.WriteString(entry.Key);
            using MemoryStream payload = new();
            using (opstream payloadWriter = new(payload, _registry, leaveOpen: true))
            {
                payloadWriter.WriteObject(entry.Value);
            }

            byte[] bytes = payload.ToArray();
            if (bytes.Length > MaxPayloadBytes)
            {
                throw new InvalidDataException($"Resource payload exceeds {MaxPayloadBytes} bytes.");
            }

            writer.WriteInt32(bytes.Length);
            writer.WriteBytes(bytes);
        }
    }

    /// <summary>
    /// Laedt einen Ressourcenkatalog aus einem Stream.
    ///
    /// Loads a resource catalog from a stream.
    /// </summary>
    /// <param name="stream">Der Quellstream. / The source stream.</param>
    /// <param name="registry">Die Typ-Registry. / The type registry.</param>
    /// <returns>Die geladene Ressourcen-Datei. / The loaded resource file.</returns>
    public static TResourceFile Load(Stream stream, TRecordRegistry registry)
    {
        try
        {
            return LoadCore(stream, registry);
        }
        catch (EndOfStreamException exception)
        {
            throw new InvalidDataException("Resource file is truncated.", exception);
        }
    }

    private static TResourceFile LoadCore(Stream stream, TRecordRegistry registry)
    {
        TResourceFile resourceFile = new(registry);
        using TBinaryArchiveReader reader = new(stream, leaveOpen: true);
        int count = reader.ReadInt32();
        // Negative Zähler würden sonst wie eine gültige leere Ressourcendatei wirken.
        // Negative counts would otherwise look like a valid empty resource file.
        if (count is < 0 or > MaxEntries)
        {
            throw new InvalidDataException("Resource entry count must be non-negative.");
        }

        for (int index = 0; index < count; index++)
        {
            string key = reader.ReadString();
            // Doppelte persistierte Keys duerfen nicht still die zuvor validierte Ressource ersetzen.
            // Duplicate persisted keys must not silently replace a previously validated resource.
            if (string.IsNullOrWhiteSpace(key) || resourceFile.Resources.Keys.Contains(key, StringComparer.Ordinal))
            {
                throw new InvalidDataException($"Resource key '{key}' is missing or duplicated.");
            }

            int payloadLength = reader.ReadInt32();
            if (payloadLength is < 0 or > MaxPayloadBytes)
            {
                throw new InvalidDataException("Resource payload length must be non-negative.");
            }

            byte[] payload = reader.ReadBytes(payloadLength);
            using MemoryStream payloadStream = new(payload, writable: false);
            using ipstream payloadReader = new(payloadStream, registry, leaveOpen: true);
            object? value = payloadReader.ReadObject();
            payloadReader.EnsureFullyConsumed();
            resourceFile.Put(key, value ?? throw new InvalidDataException("Resource payload is missing."));
        }

        reader.EnsureFullyConsumed();
        return resourceFile;
    }
}
