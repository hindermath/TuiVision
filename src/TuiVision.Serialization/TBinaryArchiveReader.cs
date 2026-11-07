// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;

namespace TuiVision.Serialization;

internal sealed class TArchiveReadBudget
{
    internal long DecodedStringBytes { get; set; }

    internal int CollectionItems { get; set; }

    internal int NewObjects { get; set; }
}

/// <summary>
/// Schmaler BinaryReader fuer deterministische Deserialisierung.
///
/// Thin binary reader for deterministic deserialization.
/// </summary>
public sealed class TBinaryArchiveReader : IDisposable
{
    internal const int MaximumStringByteCount = 64 * 1024;
    internal const long MaximumAggregateStringBytes = 64L * 1024 * 1024;
    internal const int MaximumCollectionItems = 100_000;

    private static readonly UTF8Encoding StrictUtf8 = new(false, true);
    private readonly BinaryReader _reader;
    private readonly Stream _stream;
    private readonly bool _leaveOpen;
    private readonly TArchiveReadBudget _budget;

    /// <summary>
    /// Initialisiert einen neuen Archiv-Reader.
    ///
    /// Initializes a new archive reader.
    /// </summary>
    /// <param name="stream">Der Quellstream. / The source stream.</param>
    /// <param name="leaveOpen">Gibt an, ob der Stream offen bleiben soll. / Indicates whether the stream should stay open.</param>
    public TBinaryArchiveReader(Stream stream, bool leaveOpen = false)
        : this(stream, leaveOpen, new TArchiveReadBudget())
    {
    }

    internal TBinaryArchiveReader(Stream stream, bool leaveOpen, TArchiveReadBudget budget)
    {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentNullException.ThrowIfNull(budget);
        if (!stream.CanRead)
        {
            throw new ArgumentException("Stream must be readable.", nameof(stream));
        }

        _stream = stream;
        _reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen);
        _leaveOpen = leaveOpen;
        _budget = budget;
    }

    /// <summary>
    /// Liest ein Byte.
    ///
    /// Reads a byte.
    /// </summary>
    /// <returns>Das gelesene Byte. / The read byte.</returns>
    public byte ReadByte() => _reader.ReadByte();

    /// <summary>
    /// Liest einen 32-Bit-Integer.
    ///
    /// Reads a 32-bit integer.
    /// </summary>
    /// <returns>Der gelesene Wert. / The read value.</returns>
    public int ReadInt32() => _reader.ReadInt32();

    /// <summary>
    /// Liest einen 64-Bit-Integer.
    ///
    /// Reads a 64-bit integer.
    /// </summary>
    /// <returns>Der gelesene Wert. / The read value.</returns>
    public long ReadInt64() => _reader.ReadInt64();

    /// <summary>
    /// Liest einen UInt16-Wert.
    ///
    /// Reads a UInt16 value.
    /// </summary>
    /// <returns>Der gelesene Wert. / The read value.</returns>
    public ushort ReadUInt16() => _reader.ReadUInt16();

    /// <summary>
    /// Liest einen Bool-Wert.
    ///
    /// Reads a Boolean value.
    /// </summary>
    /// <returns>Der gelesene Wert. / The read value.</returns>
    public bool ReadBoolean() => _reader.ReadBoolean();

    /// <summary>
    /// Liest einen String.
    ///
    /// Reads a string.
    /// </summary>
    /// <returns>Der gelesene String. / The read string.</returns>
    public string ReadString()
    {
        int byteCount = Read7BitEncodedLength();
        if (byteCount > MaximumStringByteCount)
        {
            throw new InvalidDataException($"Encoded string exceeds {MaximumStringByteCount} bytes.");
        }

        if (_budget.DecodedStringBytes > MaximumAggregateStringBytes - byteCount)
        {
            throw new InvalidDataException($"Aggregate encoded strings exceed {MaximumAggregateStringBytes} bytes.");
        }

        byte[] bytes = ReadBytes(byteCount);
        _budget.DecodedStringBytes += byteCount;
        try
        {
            return StrictUtf8.GetString(bytes);
        }
        catch (DecoderFallbackException exception)
        {
            throw new InvalidDataException("Serialized string is not valid UTF-8.", exception);
        }
    }

    /// <summary>
    /// Liest exakt die angegebene Anzahl Bytes.
    ///
    /// Reads exactly the specified number of bytes.
    /// </summary>
    /// <param name="count">Die erwartete Bytezahl. / The expected byte count.</param>
    /// <returns>Die gelesenen Bytes. / The read bytes.</returns>
    public byte[] ReadBytes(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Byte count must be non-negative.");
        }

        if (_stream.CanSeek && count > GetRemainingByteCount())
        {
            throw new EndOfStreamException("Unexpected end of stream.");
        }

        byte[] result = _reader.ReadBytes(count);
        if (result.Length != count)
        {
            throw new EndOfStreamException("Unexpected end of stream.");
        }

        return result;
    }

    /// <summary>
    /// Liefert die noch lesbare Bytezahl fuer Streams mit bekannter Laenge.
    ///
    /// Returns the remaining readable byte count for streams with a known length.
    /// </summary>
    /// <returns>Die verbleibende Bytezahl. / The remaining byte count.</returns>
    /// <exception cref="NotSupportedException">Der Stream unterstuetzt keine Positionsabfrage. / The stream does not support position queries.</exception>
    internal long GetRemainingByteCount()
    {
        if (!_stream.CanSeek)
        {
            throw new NotSupportedException("The remaining byte count requires a seekable stream.");
        }

        return checked(_stream.Length - _stream.Position);
    }

    internal int ReadBoundedCount(int maximum, string description)
    {
        int count = ReadInt32();
        if (count is < 0 || count > maximum)
        {
            throw new InvalidDataException($"{description} count must be between 0 and {maximum}.");
        }

        if (_budget.CollectionItems > MaximumCollectionItems - count)
        {
            throw new InvalidDataException($"Aggregate collection count exceeds {MaximumCollectionItems}.");
        }

        _budget.CollectionItems += count;
        return count;
    }

    private int Read7BitEncodedLength()
    {
        uint value = 0;
        for (int shift = 0; shift < 35; shift += 7)
        {
            byte current = _reader.ReadByte();
            if (shift == 28 && (current & 0xF0) != 0)
            {
                throw new InvalidDataException("Encoded string length is invalid.");
            }

            value |= (uint)(current & 0x7F) << shift;
            if ((current & 0x80) == 0)
            {
                if (value > int.MaxValue)
                {
                    throw new InvalidDataException("Encoded string length is invalid.");
                }

                return (int)value;
            }
        }

        throw new InvalidDataException("Encoded string length is invalid.");
    }

    /// <summary>
    /// Stellt sicher, dass keine Restdaten uebrig sind.
    ///
    /// Ensures that no trailing data remains.
    /// </summary>
    public void EnsureFullyConsumed()
    {
        if (_stream.Position != _stream.Length)
        {
            throw new InvalidDataException("Unexpected trailing data in serialized payload.");
        }
    }

    /// <summary>
    /// Gibt den Reader frei.
    ///
    /// Disposes the reader.
    /// </summary>
    public void Dispose()
    {
        if (_leaveOpen)
        {
            return;
        }

        _reader.Dispose();
    }
}
