// Deutsch: Implementiert den lesenden Kompatibilitäts-Stream für persistierte Objekte und Referenzwiederherstellung.
// English: Implements the reading compatibility stream for persisted objects and reference reconstruction.

#pragma warning disable CS8981
// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Lesender Kompatibilitaets-Stream mit Referenzwiederherstellung.
///
/// Reading compatibility stream with reference reconstruction.
/// </summary>
public sealed class ipstream : pstream
{
    private const byte NullMarker = 0;
    private const byte NewObjectMarker = 1;
    private const byte ReferenceMarker = 2;

    private readonly TBinaryArchiveReader _reader;
    private readonly Dictionary<int, object> _references = new();

    /// <summary>
    /// Initialisiert einen neuen Lesestream.
    ///
    /// Initializes a new read stream.
    /// </summary>
    /// <param name="stream">Der Quellstream. / The source stream.</param>
    /// <param name="registry">Die Typ-Registry. / The type registry.</param>
    /// <param name="leaveOpen">Gibt an, ob der Stream offen bleiben soll. / Indicates whether the stream should remain open.</param>
    public ipstream(Stream stream, TRecordRegistry registry, bool leaveOpen = false)
        : base(stream, registry, leaveOpen)
    {
        _reader = new TBinaryArchiveReader(stream, leaveOpen: true);
    }

    /// <summary>
    /// Liest ein Byte.
    ///
    /// Reads a byte.
    /// </summary>
    /// <returns>Das gelesene Byte. / The read byte.</returns>
    public byte ReadByte() => _reader.ReadByte();

    /// <summary>
    /// Liest einen Bool-Wert.
    ///
    /// Reads a Boolean value.
    /// </summary>
    /// <returns>Der gelesene Wert. / The read value.</returns>
    public bool ReadBoolean() => _reader.ReadBoolean();

    /// <summary>
    /// Liest einen 32-Bit-Integer.
    ///
    /// Reads a 32-bit integer.
    /// </summary>
    /// <returns>Der gelesene Wert. / The read value.</returns>
    public int ReadInt32() => _reader.ReadInt32();

    /// <summary>
    /// Liest einen String.
    ///
    /// Reads a string.
    /// </summary>
    /// <returns>Der gelesene String. / The read string.</returns>
    public string ReadString() => _reader.ReadString();

    /// <summary>
    /// Liest ein registriertes Objekt mit Shared-Reference-Semantik.
    ///
    /// Reads a registered object with shared-reference semantics.
    /// </summary>
    /// <typeparam name="T">Der erwartete Typ. / The expected type.</typeparam>
    /// <returns>Das gelesene Objekt oder <c>null</c>. / The read object or <c>null</c>.</returns>
    public T? ReadObject<T>() where T : class
    {
        try
        {
            byte marker = ReadByte();
            return marker switch
            {
                NullMarker => null,
                ReferenceMarker => ReadReference<T>(),
                NewObjectMarker => ReadNewObject<T>(),
                _ => throw new InvalidDataException($"Unknown object marker '{marker}'.")
            };
        }
        catch (EndOfStreamException exception)
        {
            throw new InvalidDataException("Serialized stream is truncated.", exception);
        }
    }

    /// <summary>
    /// Liest ein Objekt ohne statischen Zieltyp.
    ///
    /// Reads an object without a static target type.
    /// </summary>
    /// <returns>Das gelesene Objekt oder <c>null</c>. / The read object or <c>null</c>.</returns>
    public object? ReadObject()
    {
        return ReadObject<object>();
    }

    /// <summary>
    /// Stellt sicher, dass keine Restdaten uebrig sind.
    ///
    /// Ensures that no trailing data remains.
    /// </summary>
    public void EnsureFullyConsumed() => _reader.EnsureFullyConsumed();

    /// <summary>
    /// Gibt den Stream frei.
    ///
    /// Disposes the stream.
    /// </summary>
    public override void Dispose()
    {
        _reader.Dispose();
        base.Dispose();
    }

    private T ReadReference<T>() where T : class
    {
        int referenceId = ReadInt32();
        if (!_references.TryGetValue(referenceId, out object? value))
        {
            throw new InvalidDataException($"Unknown reference id '{referenceId}'.");
        }

        if (value is not T typed)
        {
            throw new InvalidDataException($"Reference id '{referenceId}' does not contain '{typeof(T).Name}'.");
        }

        return typed;
    }

    private T ReadNewObject<T>() where T : class
    {
        string typeId = ReadString();
        int referenceId = ReadInt32();
        object value = Registry.CreateStream(typeId, this);
        _references[referenceId] = value;

        if (value is not T typed)
        {
            throw new InvalidDataException($"Stream object '{typeId}' does not contain '{typeof(T).Name}'.");
        }

        return typed;
    }
}
