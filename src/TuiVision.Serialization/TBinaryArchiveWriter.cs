// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;

namespace TuiVision.Serialization;

/// <summary>
/// Schmaler BinaryWriter fuer deterministische Serialisierung.
///
/// Thin binary writer for deterministic serialization.
/// </summary>
public sealed class TBinaryArchiveWriter : IDisposable
{
    private readonly BinaryWriter _writer;
    private readonly bool _leaveOpen;

    /// <summary>
    /// Initialisiert einen neuen Archiv-Writer.
    ///
    /// Initializes a new archive writer.
    /// </summary>
    /// <param name="stream">Der Zielstream. / The destination stream.</param>
    /// <param name="leaveOpen">Gibt an, ob der Stream offen bleiben soll. / Indicates whether the stream should stay open.</param>
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
    /// Schreibt einen Byte-Wert.
    ///
    /// Writes a byte value.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteByte(byte value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen 32-Bit-Integer.
    ///
    /// Writes a 32-bit integer.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteInt32(int value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen 64-Bit-Integer.
    ///
    /// Writes a 64-bit integer.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteInt64(long value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen UInt16-Wert.
    ///
    /// Writes a UInt16 value.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteUInt16(ushort value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen Bool-Wert.
    ///
    /// Writes a Boolean value.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteBoolean(bool value) => _writer.Write(value);

    /// <summary>
    /// Schreibt einen String.
    ///
    /// Writes a string.
    /// </summary>
    /// <param name="value">Der zu schreibende String. / The string to write.</param>
    public void WriteString(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        _writer.Write(value);
    }

    /// <summary>
    /// Schreibt Rohbytes.
    ///
    /// Writes raw bytes.
    /// </summary>
    /// <param name="bytes">Die zu schreibenden Bytes. / The bytes to write.</param>
    public void WriteBytes(ReadOnlySpan<byte> bytes) => _writer.Write(bytes);

    /// <summary>
    /// Gibt den Writer frei.
    ///
    /// Disposes the writer.
    /// </summary>
    public void Dispose()
    {
        if (_leaveOpen)
        {
            _writer.Flush();
            return;
        }

        _writer.Dispose();
    }
}
