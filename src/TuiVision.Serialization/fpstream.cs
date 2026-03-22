#pragma warning disable CS8981
// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Dateibasierter Kompatibilitaets-Stream.
///
/// File-backed compatibility stream.
/// </summary>
public sealed class fpstream : pstream
{
    private readonly FileStream _fileStream;

    /// <summary>
    /// Initialisiert einen neuen dateibasierten Stream.
    ///
    /// Initializes a new file-backed stream.
    /// </summary>
    /// <param name="path">Der Dateipfad. / The file path.</param>
    /// <param name="mode">Der Dateimodus. / The file mode.</param>
    /// <param name="access">Der Dateizugriff. / The file access.</param>
    /// <param name="registry">Die Typ-Registry. / The type registry.</param>
    public fpstream(string path, FileMode mode, FileAccess access, TRecordRegistry registry)
        : base(new FileStream(path, mode, access, FileShare.ReadWrite), registry)
    {
        _fileStream = (FileStream)BaseStream;
    }

    /// <summary>
    /// Oeffnet einen lesenden Stream.
    ///
    /// Opens a read stream.
    /// </summary>
    /// <returns>Ein lesender Stream. / A read stream.</returns>
    public ipstream OpenReader() => new(_fileStream, Registry, leaveOpen: true);

    /// <summary>
    /// Oeffnet einen schreibenden Stream.
    ///
    /// Opens a write stream.
    /// </summary>
    /// <returns>Ein write stream. / A write stream.</returns>
    public opstream OpenWriter() => new(_fileStream, Registry, leaveOpen: true);
}
