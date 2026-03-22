// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;

namespace TuiVision.Serialization;

/// <summary>
/// Schmaler BinaryReader fuer deterministische Deserialisierung.
///
/// Thin binary reader for deterministic deserialization.
/// </summary>
public sealed class TBinaryArchiveReader : IDisposable
{
    private readonly BinaryReader _reader;
    private readonly Stream _stream;
    private readonly bool _leaveOpen;

    /// <summary>
    /// Initialisiert einen neuen Archiv-Reader.
    ///
    /// Initializes a new archive reader.
    /// </summary>
    /// <param name="stream">Der Quellstream. / The source stream.</param>
    /// <param name="leaveOpen">Gibt an, ob der Stream offen bleiben soll. / Indicates whether the stream should stay open.</param>
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
    public string ReadString() => _reader.ReadString();

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

        byte[] result = _reader.ReadBytes(count);
        if (result.Length != count)
        {
            throw new EndOfStreamException("Unexpected end of stream.");
        }

        return result;
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
