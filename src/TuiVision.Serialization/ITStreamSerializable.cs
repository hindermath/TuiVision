// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Schnittstelle fuer archivbasierte Serialisierung portierter Record-Typen.
///
/// Contract for archive-based serialization of ported record types.
/// </summary>
public interface ITStreamSerializable
{
    /// <summary>
    /// Schreibt den Zustand des Objekts in den angegebenen Writer.
    ///
    /// Writes the state of the object to the specified writer.
    /// </summary>
    /// <param name="writer">Der Ziel-Writer. / The destination writer.</param>
    void SaveTo(TBinaryArchiveWriter writer);
}
