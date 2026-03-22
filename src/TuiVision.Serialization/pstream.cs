// Deutsch: Definiert die gemeinsame Basisklasse für die historischen Kompatibilitäts-Streams in der Serialisierungsschicht.
// English: Defines the shared base class for the historical compatibility streams in the serialization layer.

#pragma warning disable CS8981
// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Gemeinsame Basis fuer Kompatibilitaets-Streams.
///
/// Shared base for compatibility streams.
/// </summary>
public abstract class pstream : IDisposable
{
    /// <summary>
    /// Initialisiert einen neuen Kompatibilitaets-Stream.
    ///
    /// Initializes a new compatibility stream.
    /// </summary>
    /// <param name="stream">Der zugrunde liegende Stream. / The underlying stream.</param>
    /// <param name="registry">Die Typ-Registry. / The type registry.</param>
    /// <param name="leaveOpen">Gibt an, ob der Stream offen bleiben soll. / Indicates whether the stream should remain open.</param>
    protected pstream(Stream stream, TRecordRegistry registry, bool leaveOpen = false)
    {
        BaseStream = stream ?? throw new ArgumentNullException(nameof(stream));
        Registry = registry ?? throw new ArgumentNullException(nameof(registry));
        LeaveOpen = leaveOpen;
    }

    /// <summary>
    /// Der zugrunde liegende Stream.
    ///
    /// The underlying stream.
    /// </summary>
    protected Stream BaseStream { get; }

    /// <summary>
    /// Die Typ-Registry.
    ///
    /// The type registry.
    /// </summary>
    protected TRecordRegistry Registry { get; }

    /// <summary>
    /// Gibt an, ob der zugrunde liegende Stream offen bleiben soll.
    ///
    /// Indicates whether the underlying stream should remain open.
    /// </summary>
    protected bool LeaveOpen { get; }

    /// <summary>
    /// Liefert die aktuelle Stream-Position.
    ///
    /// Returns the current stream position.
    /// </summary>
    /// <returns>Die aktuelle Position. / The current position.</returns>
    public long Tell() => BaseStream.Position;

    /// <summary>
    /// Verschiebt die aktuelle Stream-Position.
    ///
    /// Moves the current stream position.
    /// </summary>
    /// <param name="offset">Der Offset. / The offset.</param>
    /// <param name="origin">Der Bezugspunkt. / The reference point.</param>
    /// <returns>Die neue Position. / The new position.</returns>
    public long Seek(long offset, SeekOrigin origin) => BaseStream.Seek(offset, origin);

    /// <summary>
    /// Gibt den Stream frei.
    ///
    /// Disposes the stream.
    /// </summary>
    public virtual void Dispose()
    {
        if (!LeaveOpen)
        {
            BaseStream.Dispose();
        }
    }
}
