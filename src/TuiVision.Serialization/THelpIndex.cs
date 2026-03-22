// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Kontextindex fuer runtime-lesbare Hilfethemen.
///
/// Context index for runtime-readable help topics.
/// </summary>
public sealed class THelpIndex : ITStreamSerializable
{
    private readonly Dictionary<int, int> _positions = new();

    /// <summary>
    /// Registriert einen Kontext und seine Themenposition.
    ///
    /// Registers a context and its topic position.
    /// </summary>
    /// <param name="context">Die Kontext-ID. / The context identifier.</param>
    /// <param name="position">Die Themenposition. / The topic position.</param>
    public void Add(int context, int position)
    {
        _positions[context] = position;
    }

    /// <summary>
    /// Versucht eine Themenposition fuer einen Kontext aufzulösen.
    ///
    /// Tries to resolve a topic position for a context.
    /// </summary>
    /// <param name="context">Die Kontext-ID. / The context identifier.</param>
    /// <param name="position">Die Themenposition. / The topic position.</param>
    /// <returns><c>true</c>, wenn eine Position existiert. / <c>true</c> if a position exists.</returns>
    public bool TryGetPosition(int context, out int position) => _positions.TryGetValue(context, out position);

    /// <summary>
    /// Schreibt den Index in einen Archiv-Writer.
    ///
    /// Writes the index to an archive writer.
    /// </summary>
    /// <param name="writer">Der Ziel-Writer. / The destination writer.</param>
    public void SaveTo(TBinaryArchiveWriter writer)
    {
        writer.WriteInt32(_positions.Count);
        foreach ((int context, int position) in _positions.OrderBy(pair => pair.Key))
        {
            writer.WriteInt32(context);
            writer.WriteInt32(position);
        }
    }

    /// <summary>
    /// Laedt einen Index aus einem Archiv-Reader.
    ///
    /// Loads an index from an archive reader.
    /// </summary>
    /// <param name="reader">Der Quell-Reader. / The source reader.</param>
    /// <returns>Der geladene Index. / The loaded index.</returns>
    public static THelpIndex LoadFrom(TBinaryArchiveReader reader)
    {
        THelpIndex index = new();
        int count = reader.ReadInt32();
        for (int current = 0; current < count; current++)
        {
            index.Add(reader.ReadInt32(), reader.ReadInt32());
        }

        return index;
    }
}
