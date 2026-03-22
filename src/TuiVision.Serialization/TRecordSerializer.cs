// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Envelope-Serializer fuer archivbasierte Typ-IDs plus Nutzdaten.
///
/// Envelope serializer for archive-based type identifiers plus payloads.
/// </summary>
public sealed class TRecordSerializer
{
    private readonly TRecordRegistry _registry;

    /// <summary>
    /// Initialisiert einen neuen Serializer.
    ///
    /// Initializes a new serializer.
    /// </summary>
    /// <param name="registry">Die verwendete Registry. / The registry to use.</param>
    public TRecordSerializer(TRecordRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

    /// <summary>
    /// Serialisiert einen Record in ein Envelope-Array.
    ///
    /// Serializes a record into an envelope array.
    /// </summary>
    /// <param name="typeId">Die stabile Typ-ID. / The stable type identifier.</param>
    /// <param name="value">Der zu serialisierende Wert. / The value to serialize.</param>
    /// <returns>Das Envelope-Array. / The envelope array.</returns>
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
    /// Deserialisiert ein Envelope-Array.
    ///
    /// Deserializes an envelope array.
    /// </summary>
    /// <param name="data">Die Eingabedaten. / The input data.</param>
    /// <returns>Das deserialisierte Objekt. / The deserialized object.</returns>
    public object Deserialize(ReadOnlySpan<byte> data)
    {
        if (data.IsEmpty)
        {
            throw new InvalidDataException("Serialized data is empty.");
        }

        try
        {
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
        catch (EndOfStreamException exception)
        {
            throw new InvalidDataException("Serialized payload is truncated.", exception);
        }
    }

    /// <summary>
    /// Deserialisiert ein Envelope-Array typisiert.
    ///
    /// Deserializes an envelope array as a typed value.
    /// </summary>
    /// <typeparam name="T">Der erwartete Typ. / The expected type.</typeparam>
    /// <param name="data">Die Eingabedaten. / The input data.</param>
    /// <returns>Der deserialisierte Wert. / The deserialized value.</returns>
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
