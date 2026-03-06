using System.Text;

namespace TuiVision.Serialization;

/// <summary>
/// Schnittstelle für stream-basierte Serialisierung portierter Turbo-Vision-Record-Typen.
/// Klassen, die diese Schnittstelle implementieren, können ihren Zustand in einen
/// <see cref="TBinaryArchiveWriter"/> schreiben und später wiederherstellen.
///
/// Contract for stream-based serialization of ported Turbo Vision record types.
/// Classes that implement this interface can write their state to a
/// <see cref="TBinaryArchiveWriter"/> and restore it later.
/// </summary>
public interface ITStreamSerializable
{
    /// <summary>
    /// Schreibt den Zustand dieses Objekts in den angegebenen Writer.
    ///
    /// Writes the state of this object to the specified writer.
    /// </summary>
    /// <param name="writer">Der Ziel-Writer. / The target writer.</param>
    void SaveTo(TBinaryArchiveWriter writer);
}

/// <summary>
/// Schmale Binär-Writer-Abstraktion für deterministisches Erstellen von Nutzdaten.
/// Kapselt einen <see cref="BinaryWriter"/> mit fester UTF-8-Kodierung.
///
/// Thin binary writer abstraction for deterministic payload creation.
/// Wraps a <see cref="BinaryWriter"/> with fixed UTF-8 encoding.
/// </summary>
public sealed class TBinaryArchiveWriter : IDisposable
{
    /// <summary>
    /// Der interne .NET-BinaryWriter, der die eigentlichen Schreiboperationen ausführt.
    ///
    /// The internal .NET BinaryWriter that performs the actual write operations.
    /// </summary>
    private readonly BinaryWriter _writer;

    /// <summary>
    /// Gibt an, ob der zugrunde liegende Stream beim Freigeben offen bleiben soll.
    ///
    /// Indicates whether the underlying stream should remain open when this writer is disposed.
    /// </summary>
    private readonly bool _leaveOpen;

    /// <summary>
    /// Erstellt einen neuen Writer für den angegebenen Stream.
    ///
    /// Creates a new writer for the specified stream.
    /// </summary>
    /// <param name="stream">
    /// Der Zielstream. Muss beschreibbar sein.
    /// The target stream. Must be writable.
    /// </param>
    /// <param name="leaveOpen">
    /// <c>true</c>, um den Stream nach dem Freigeben dieses Writers offen zu lassen;
    /// <c>false</c>, um ihn zu schließen (Standard).
    ///
    /// <c>true</c> to leave the stream open after disposing this writer;
    /// <c>false</c> to close it (default).
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="stream"/> <c>null</c> ist.
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="stream"/> nicht beschreibbar ist.
    /// Thrown when <paramref name="stream"/> is not writable.
    /// </exception>
    public TBinaryArchiveWriter(Stream stream, bool leaveOpen = false)
    {
        ArgumentNullException.ThrowIfNull(stream);
        if (!stream.CanWrite)
        {
            throw new ArgumentException("Stream must be writable.", nameof(stream));
        }

        _writer = new BinaryWriter(stream, Encoding.UTF8, leaveOpen);
        _leaveOpen = leaveOpen;
    }

    /// <summary>
    /// Schreibt einen 32-Bit-Integer-Wert in den Stream.
    ///
    /// Writes a 32-bit integer value to the stream.
    /// </summary>
    /// <param name="value">Der zu schreibende Wert. / The value to write.</param>
    public void WriteInt32(int value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen vorzeichenlosen 16-Bit-Integer-Wert in den Stream.
    ///
    /// Writes an unsigned 16-bit integer value to the stream.
    /// </summary>
    /// <param name="value">Der zu schreibende Wert. / The value to write.</param>
    public void WriteUInt16(ushort value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen booleschen Wert in den Stream.
    ///
    /// Writes a boolean value to the stream.
    /// </summary>
    /// <param name="value">Der zu schreibende Wert. / The value to write.</param>
    public void WriteBoolean(bool value) => _writer.Write(value);

    /// <summary>
    /// Schreibt eine Zeichenkette im .NET-BinaryWriter-Format (Länge + UTF-8-Bytes) in den Stream.
    ///
    /// Writes a string in .NET BinaryWriter format (length prefix + UTF-8 bytes) to the stream.
    /// </summary>
    /// <param name="value">Die zu schreibende Zeichenkette. / The string to write.</param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="value"/> <c>null</c> ist.
    /// Thrown when <paramref name="value"/> is <c>null</c>.
    /// </exception>
    public void WriteString(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _writer.Write(value);
    }

    /// <summary>
    /// Schreibt einen Byte-Span direkt in den Stream.
    ///
    /// Writes a byte span directly to the stream.
    /// </summary>
    /// <param name="bytes">Die zu schreibenden Bytes. / The bytes to write.</param>
    public void WriteBytes(ReadOnlySpan<byte> bytes) => _writer.Write(bytes);

    /// <summary>
    /// Gibt den Writer frei. Schließt den zugrunde liegenden Stream,
    /// sofern <c>leaveOpen</c> beim Erstellen nicht auf <c>true</c> gesetzt wurde.
    ///
    /// Disposes the writer. Closes the underlying stream
    /// unless <c>leaveOpen</c> was set to <c>true</c> at construction.
    /// </summary>
    public void Dispose()
    {
        if (!_leaveOpen)
        {
            _writer.Dispose();
        }
    }
}

/// <summary>
/// Schmale Binär-Reader-Abstraktion, die zu <see cref="TBinaryArchiveWriter"/> passt.
/// Kapselt einen <see cref="BinaryReader"/> mit fester UTF-8-Kodierung.
///
/// Thin binary reader abstraction matching <see cref="TBinaryArchiveWriter"/>.
/// Wraps a <see cref="BinaryReader"/> with fixed UTF-8 encoding.
/// </summary>
public sealed class TBinaryArchiveReader : IDisposable
{
    /// <summary>
    /// Der interne .NET-BinaryReader, der die eigentlichen Leseoperationen ausführt.
    ///
    /// The internal .NET BinaryReader that performs the actual read operations.
    /// </summary>
    private readonly BinaryReader _reader;

    /// <summary>
    /// Der zugrunde liegende Stream, der für die Positionsprüfung benötigt wird.
    ///
    /// The underlying stream, required for position validation.
    /// </summary>
    private readonly Stream _stream;

    /// <summary>
    /// Gibt an, ob der zugrunde liegende Stream beim Freigeben offen bleiben soll.
    ///
    /// Indicates whether the underlying stream should remain open when this reader is disposed.
    /// </summary>
    private readonly bool _leaveOpen;

    /// <summary>
    /// Erstellt einen neuen Reader für den angegebenen Stream.
    ///
    /// Creates a new reader for the specified stream.
    /// </summary>
    /// <param name="stream">
    /// Der Quellstream. Muss lesbar sein.
    /// The source stream. Must be readable.
    /// </param>
    /// <param name="leaveOpen">
    /// <c>true</c>, um den Stream nach dem Freigeben dieses Readers offen zu lassen;
    /// <c>false</c>, um ihn zu schließen (Standard).
    ///
    /// <c>true</c> to leave the stream open after disposing this reader;
    /// <c>false</c> to close it (default).
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="stream"/> <c>null</c> ist.
    /// Thrown when <paramref name="stream"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="stream"/> nicht lesbar ist.
    /// Thrown when <paramref name="stream"/> is not readable.
    /// </exception>
    public TBinaryArchiveReader(Stream stream, bool leaveOpen = false)
    {
        ArgumentNullException.ThrowIfNull(stream);
        if (!stream.CanRead)
        {
            throw new ArgumentException("Stream must be readable.", nameof(stream));
        }

        _stream = stream;
        _reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen);
        _leaveOpen = leaveOpen;
    }

    /// <summary>
    /// Liest einen 32-Bit-Integer-Wert aus dem Stream.
    ///
    /// Reads a 32-bit integer value from the stream.
    /// </summary>
    /// <returns>
    /// Der gelesene Wert.
    /// The value that was read.
    /// </returns>
    public int ReadInt32() => _reader.ReadInt32();

    /// <summary>
    /// Liest einen vorzeichenlosen 16-Bit-Integer-Wert aus dem Stream.
    ///
    /// Reads an unsigned 16-bit integer value from the stream.
    /// </summary>
    /// <returns>
    /// Der gelesene Wert.
    /// The value that was read.
    /// </returns>
    public ushort ReadUInt16() => _reader.ReadUInt16();

    /// <summary>
    /// Liest einen booleschen Wert aus dem Stream.
    ///
    /// Reads a boolean value from the stream.
    /// </summary>
    /// <returns>
    /// Der gelesene Wert.
    /// The value that was read.
    /// </returns>
    public bool ReadBoolean() => _reader.ReadBoolean();

    /// <summary>
    /// Liest eine Zeichenkette im .NET-BinaryReader-Format (Länge + UTF-8-Bytes) aus dem Stream.
    ///
    /// Reads a string in .NET BinaryReader format (length prefix + UTF-8 bytes) from the stream.
    /// </summary>
    /// <returns>
    /// Die gelesene Zeichenkette.
    /// The string that was read.
    /// </returns>
    public string ReadString() => _reader.ReadString();

    /// <summary>
    /// Liest eine bestimmte Anzahl Bytes aus dem Stream und gibt sie als Array zurück.
    ///
    /// Reads a specified number of bytes from the stream and returns them as an array.
    /// </summary>
    /// <param name="count">
    /// Die Anzahl der zu lesenden Bytes. Darf nicht negativ sein.
    /// The number of bytes to read. Must not be negative.
    /// </param>
    /// <returns>
    /// Ein Byte-Array mit exakt <paramref name="count"/> Bytes.
    /// A byte array with exactly <paramref name="count"/> bytes.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn <paramref name="count"/> negativ ist.
    /// Thrown when <paramref name="count"/> is negative.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Wird ausgelöst, wenn der Stream weniger Bytes enthält als erwartet.
    /// Thrown when the stream contains fewer bytes than expected.
    /// </exception>
    public byte[] ReadBytes(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Byte count must be non-negative.");
        }

        byte[] result = _reader.ReadBytes(count);
        if (result.Length != count)
        {
            throw new EndOfStreamException("Unexpected end of stream.");
        }

        return result;
    }

    /// <summary>
    /// Prüft, ob der Stream vollständig gelesen wurde.
    /// Wirft eine Ausnahme, wenn am Ende unerwartete Daten übrig sind.
    ///
    /// Checks whether the stream has been fully consumed.
    /// Throws an exception if unexpected trailing data remains.
    /// </summary>
    /// <exception cref="InvalidDataException">
    /// Wird ausgelöst, wenn der Stream noch ungelesene Bytes enthält.
    /// Thrown when the stream still contains unread bytes.
    /// </exception>
    public void EnsureFullyConsumed()
    {
        if (_stream.Position != _stream.Length)
        {
            throw new InvalidDataException("Unexpected trailing data in serialized payload.");
        }
    }

    /// <summary>
    /// Gibt den Reader frei. Schließt den zugrunde liegenden Stream,
    /// sofern <c>leaveOpen</c> beim Erstellen nicht auf <c>true</c> gesetzt wurde.
    ///
    /// Disposes the reader. Closes the underlying stream
    /// unless <c>leaveOpen</c> was set to <c>true</c> at construction.
    /// </summary>
    public void Dispose()
    {
        if (!_leaveOpen)
        {
            _reader.Dispose();
        }
    }
}

/// <summary>
/// Registry, die stabile Typbezeichner auf Factory-Delegates abbildet.
/// Ermöglicht polymorphe Deserialisierung ohne direkte Typabhängigkeiten.
///
/// Registry that maps stable type identifiers to factory delegates.
/// Enables polymorphic deserialization without direct type dependencies.
/// </summary>
public sealed class TRecordRegistry
{
    /// <summary>
    /// Die interne Zuordnung von Typbezeichner zu Factory-Delegate.
    /// Jeder Bezeichner darf nur einmal registriert sein.
    ///
    /// The internal mapping from type identifier to factory delegate.
    /// Each identifier may only be registered once.
    /// </summary>
    private readonly Dictionary<string, Func<TBinaryArchiveReader, object>> _factories = new(StringComparer.Ordinal);

    /// <summary>
    /// Registriert eine Factory-Funktion für den angegebenen Typbezeichner.
    ///
    /// Registers a factory function for the specified type identifier.
    /// </summary>
    /// <typeparam name="T">
    /// Der zu registrierende Typ. Darf nicht <c>null</c> sein.
    /// The type to register. Must not be <c>null</c>.
    /// </typeparam>
    /// <param name="typeId">
    /// Der stabile Bezeichner des Typs. Darf nicht leer oder nur aus Leerzeichen bestehen.
    /// The stable identifier of the type. Must not be empty or whitespace-only.
    /// </param>
    /// <param name="factory">
    /// Eine Funktion, die aus einem Reader eine Instanz von <typeparamref name="T"/> erstellt.
    /// A function that creates an instance of <typeparamref name="T"/> from a reader.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="typeId"/> leer oder nur Leerzeichen ist.
    /// Thrown when <paramref name="typeId"/> is empty or whitespace-only.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Wird ausgelöst, wenn <paramref name="typeId"/> bereits registriert ist.
    /// Thrown when <paramref name="typeId"/> is already registered.
    /// </exception>
    public void Register<T>(string typeId, Func<TBinaryArchiveReader, T> factory) where T : notnull
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(factory);

        if (!_factories.TryAdd(typeId, reader => factory(reader)))
        {
            throw new InvalidOperationException($"Type id '{typeId}' is already registered.");
        }
    }

    /// <summary>
    /// Erstellt eine Instanz des zum Bezeichner gehörenden Typs aus dem Reader.
    ///
    /// Creates an instance of the type associated with the identifier, reading from the reader.
    /// </summary>
    /// <param name="typeId">
    /// Der stabile Typbezeichner. Muss zuvor registriert worden sein.
    /// The stable type identifier. Must have been registered previously.
    /// </param>
    /// <param name="reader">Der Reader mit den Nutzdaten. / The reader containing the payload.</param>
    /// <returns>
    /// Die deserialisierte Instanz.
    /// The deserialized instance.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Wird ausgelöst, wenn für <paramref name="typeId"/> keine Factory registriert ist.
    /// Thrown when no factory is registered for <paramref name="typeId"/>.
    /// </exception>
    public object Create(string typeId, TBinaryArchiveReader reader)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(reader);

        if (!_factories.TryGetValue(typeId, out Func<TBinaryArchiveReader, object>? factory))
        {
            throw new KeyNotFoundException($"No factory registered for type id '{typeId}'.");
        }

        return factory(reader);
    }
}

/// <summary>
/// Umschlag-Serialisierer, der Typ-ID und Nutzdaten-Bytes in einem binären Format kapselt.
/// Das Umschlag-Format ist: Typbezeichner (String) + Nutzdatenlänge (Int32) + Nutzdaten (Bytes).
///
/// Envelope serializer that wraps a type ID and payload bytes in a binary format.
/// The envelope format is: type identifier (string) + payload length (Int32) + payload (bytes).
/// </summary>
public sealed class TRecordSerializer
{
    /// <summary>
    /// Die Registry, die Typbezeichner auf Factory-Delegates abbildet.
    ///
    /// The registry that maps type identifiers to factory delegates.
    /// </summary>
    private readonly TRecordRegistry _registry;

    /// <summary>
    /// Erstellt einen neuen Serialisierer mit der angegebenen Registry.
    ///
    /// Creates a new serializer with the specified registry.
    /// </summary>
    /// <param name="registry">
    /// Die Registry mit den registrierten Typen. Darf nicht <c>null</c> sein.
    /// The registry with the registered types. Must not be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="registry"/> <c>null</c> ist.
    /// Thrown when <paramref name="registry"/> is <c>null</c>.
    /// </exception>
    public TRecordSerializer(TRecordRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

    /// <summary>
    /// Serialisiert einen Wert in ein Umschlag-Byte-Array.
    ///
    /// Serializes a value into an envelope byte array.
    /// </summary>
    /// <param name="typeId">
    /// Der stabile Typbezeichner. Darf nicht leer oder nur aus Leerzeichen bestehen.
    /// The stable type identifier. Must not be empty or whitespace-only.
    /// </param>
    /// <param name="value">
    /// Der zu serialisierende Wert. Darf nicht <c>null</c> sein.
    /// The value to serialize. Must not be <c>null</c>.
    /// </param>
    /// <returns>
    /// Ein Byte-Array im Umschlag-Format.
    /// A byte array in envelope format.
    /// </returns>
    public byte[] Serialize(string typeId, ITStreamSerializable value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(value);

        using MemoryStream payloadStream = new();
        using (TBinaryArchiveWriter payloadWriter = new(payloadStream, leaveOpen: true))
        {
            value.SaveTo(payloadWriter);
        }

        byte[] payload = payloadStream.ToArray();

        using MemoryStream envelopeStream = new();
        using (TBinaryArchiveWriter writer = new(envelopeStream, leaveOpen: true))
        {
            writer.WriteString(typeId);
            writer.WriteInt32(payload.Length);
            writer.WriteBytes(payload);
        }

        return envelopeStream.ToArray();
    }

    /// <summary>
    /// Deserialisiert ein Umschlag-Byte-Array und gibt die enthaltene Instanz zurück.
    ///
    /// Deserializes an envelope byte array and returns the contained instance.
    /// </summary>
    /// <param name="data">
    /// Das zu deserialisierende Byte-Array. Darf nicht leer sein.
    /// The byte array to deserialize. Must not be empty.
    /// </param>
    /// <returns>
    /// Die deserialisierte Instanz.
    /// The deserialized instance.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Wird ausgelöst, wenn die Daten leer, fehlerhaft oder unvollständig sind.
    /// Thrown when the data is empty, malformed, or incomplete.
    /// </exception>
    public object Deserialize(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            throw new InvalidDataException("Serialized data is empty.");
        }

        using MemoryStream envelopeStream = new(data.ToArray(), writable: false);
        using TBinaryArchiveReader reader = new(envelopeStream, leaveOpen: true);

        string typeId = reader.ReadString();
        int payloadLength = reader.ReadInt32();
        if (payloadLength < 0)
        {
            throw new InvalidDataException("Payload length must be non-negative.");
        }

        byte[] payload = reader.ReadBytes(payloadLength);
        reader.EnsureFullyConsumed();

        using MemoryStream payloadStream = new(payload, writable: false);
        using TBinaryArchiveReader payloadReader = new(payloadStream, leaveOpen: true);
        object value = _registry.Create(typeId, payloadReader);
        payloadReader.EnsureFullyConsumed();
        return value;
    }

    /// <summary>
    /// Deserialisiert ein Umschlag-Byte-Array und gibt die enthaltene Instanz als <typeparamref name="T"/> zurück.
    ///
    /// Deserializes an envelope byte array and returns the contained instance as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Der erwartete Ergebnistyp. Muss eine Klasse sein.
    /// The expected result type. Must be a class.
    /// </typeparam>
    /// <param name="data">
    /// Das zu deserialisierende Byte-Array.
    /// The byte array to deserialize.
    /// </param>
    /// <returns>
    /// Die deserialisierte Instanz als <typeparamref name="T"/>.
    /// The deserialized instance as <typeparamref name="T"/>.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Wird ausgelöst, wenn die Nutzdaten keine Instanz von <typeparamref name="T"/> enthalten.
    /// Thrown when the payload does not contain an instance of <typeparamref name="T"/>.
    /// </exception>
    public T Deserialize<T>(ReadOnlySpan<byte> data) where T : class
    {
        object value = Deserialize(data);
        if (value is not T typedValue)
        {
            throw new InvalidDataException(
                $"Serialized payload does not contain a '{typeof(T).Name}' instance.");
        }

        return typedValue;
    }
}
