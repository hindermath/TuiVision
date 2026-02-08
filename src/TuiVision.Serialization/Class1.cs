using System.Text;

namespace TuiVision.Serialization;

/// <summary>
/// Contract for stream-based serialization used by ported Turbo Vision record types.
/// </summary>
public interface ITStreamSerializable
{
    void SaveTo(TBinaryArchiveWriter writer);
}

/// <summary>
/// Small binary writer abstraction for deterministic payload creation.
/// </summary>
public sealed class TBinaryArchiveWriter : IDisposable
{
    private readonly BinaryWriter _writer;
    private readonly bool _leaveOpen;

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

    public void WriteInt32(int value) => _writer.Write(value);

    public void WriteUInt16(ushort value) => _writer.Write(value);

    public void WriteBoolean(bool value) => _writer.Write(value);

    public void WriteString(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _writer.Write(value);
    }

    public void WriteBytes(ReadOnlySpan<byte> bytes) => _writer.Write(bytes);

    public void Dispose()
    {
        if (!_leaveOpen)
        {
            _writer.Dispose();
        }
    }
}

/// <summary>
/// Small binary reader abstraction matching <see cref="TBinaryArchiveWriter"/>.
/// </summary>
public sealed class TBinaryArchiveReader : IDisposable
{
    private readonly BinaryReader _reader;
    private readonly Stream _stream;
    private readonly bool _leaveOpen;

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

    public int ReadInt32() => _reader.ReadInt32();

    public ushort ReadUInt16() => _reader.ReadUInt16();

    public bool ReadBoolean() => _reader.ReadBoolean();

    public string ReadString() => _reader.ReadString();

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

    public void EnsureFullyConsumed()
    {
        if (_stream.Position != _stream.Length)
        {
            throw new InvalidDataException("Unexpected trailing data in serialized payload.");
        }
    }

    public void Dispose()
    {
        if (!_leaveOpen)
        {
            _reader.Dispose();
        }
    }
}

/// <summary>
/// Registry that maps stable type identifiers to factory delegates.
/// </summary>
public sealed class TRecordRegistry
{
    private readonly Dictionary<string, Func<TBinaryArchiveReader, object>> _factories = new(StringComparer.Ordinal);

    public void Register<T>(string typeId, Func<TBinaryArchiveReader, T> factory) where T : notnull
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeId);
        ArgumentNullException.ThrowIfNull(factory);

        if (!_factories.TryAdd(typeId, reader => factory(reader)))
        {
            throw new InvalidOperationException($"Type id '{typeId}' is already registered.");
        }
    }

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
/// Envelope serializer using type id + payload bytes.
/// </summary>
public sealed class TRecordSerializer
{
    private readonly TRecordRegistry _registry;

    public TRecordSerializer(TRecordRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

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
