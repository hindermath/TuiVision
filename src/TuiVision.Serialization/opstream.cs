// Deutsch: Implementiert den schreibenden Kompatibilitäts-Stream für persistierte Objekte mit Referenzerhaltung.
// English: Implements the writing compatibility stream for persisted objects with reference preservation.

#pragma warning disable CS8981
// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Runtime.CompilerServices;

namespace TuiVision.Serialization;

/// <summary>
/// Schreibender Kompatibilitaets-Stream mit Referenzerhaltung.
///
/// Writing compatibility stream with reference preservation.
/// </summary>
public sealed class opstream : pstream
{
    private const byte NullMarker = 0;
    private const byte NewObjectMarker = 1;
    private const byte ReferenceMarker = 2;

    private readonly TBinaryArchiveWriter _writer;
    private readonly Dictionary<object, int> _referenceIds = new(ReferenceEqualityComparer.Instance);
    private readonly HashSet<object> _activeObjects = new(ReferenceEqualityComparer.Instance);
    private int _nextReferenceId = 1;

    /// <summary>
    /// Initialisiert einen neuen Schreibstream.
    ///
    /// Initializes a new write stream.
    /// </summary>
    /// <param name="stream">Der Zielstream. / The destination stream.</param>
    /// <param name="registry">Die Typ-Registry. / The type registry.</param>
    /// <param name="leaveOpen">Gibt an, ob der Stream offen bleiben soll. / Indicates whether the stream should remain open.</param>
    public opstream(Stream stream, TRecordRegistry registry, bool leaveOpen = false)
        : base(stream, registry, leaveOpen)
    {
        _writer = new TBinaryArchiveWriter(stream, leaveOpen: true);
    }

    /// <summary>
    /// Schreibt ein Byte.
    ///
    /// Writes a byte.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteByte(byte value) => _writer.WriteByte(value);

    /// <summary>
    /// Schreibt einen Bool-Wert.
    ///
    /// Writes a Boolean value.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteBoolean(bool value) => _writer.WriteBoolean(value);

    /// <summary>
    /// Schreibt einen 32-Bit-Integer.
    ///
    /// Writes a 32-bit integer.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteInt32(int value) => _writer.WriteInt32(value);

    /// <summary>
    /// Schreibt einen String.
    ///
    /// Writes a string.
    /// </summary>
    /// <param name="value">Der Wert. / The value.</param>
    public void WriteString(string value) => _writer.WriteString(value);

    /// <summary>
    /// Schreibt ein registriertes Objekt mit Shared-Reference-Semantik.
    ///
    /// Writes a registered object with shared-reference semantics.
    /// </summary>
    /// <typeparam name="T">Der Referenztyp. / The reference type.</typeparam>
    /// <param name="value">Das zu schreibende Objekt. / The object to write.</param>
    public void WriteObject<T>(T? value) where T : class
    {
        if (value is null)
        {
            WriteByte(NullMarker);
            return;
        }

        if (_referenceIds.TryGetValue(value, out int existingReferenceId))
        {
            if (_activeObjects.Contains(value))
            {
                throw new InvalidDataException("Cyclic object graphs are not supported.");
            }

            WriteByte(ReferenceMarker);
            WriteInt32(existingReferenceId);
            return;
        }

        TRecordRegistry.StreamWriterRegistration registration = Registry.ResolveWriter(value.GetType());
        int referenceId = _nextReferenceId++;
        _referenceIds[value] = referenceId;

        WriteByte(NewObjectMarker);
        WriteString(registration.TypeId);
        WriteInt32(referenceId);

        _activeObjects.Add(value);
        try
        {
            registration.Writer(this, value);
        }
        finally
        {
            _activeObjects.Remove(value);
        }
    }

    /// <summary>
    /// Schreibt ein Objekt mit Laufzeittyp-Aufloesung.
    ///
    /// Writes an object with runtime type resolution.
    /// </summary>
    /// <param name="value">Das zu schreibende Objekt. / The object to write.</param>
    public void WriteObject(object? value)
    {
        WriteObject<object>(value as object);
    }

    /// <summary>
    /// Gibt den Stream frei.
    ///
    /// Disposes the stream.
    /// </summary>
    public override void Dispose()
    {
        _writer.Dispose();
        base.Dispose();
    }

    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();

        public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);

        public int GetHashCode(object obj) => RuntimeHelpers.GetHashCode(obj);
    }
}
