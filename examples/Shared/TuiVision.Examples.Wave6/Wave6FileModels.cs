// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Examples.Wave6;

/// <summary>Art eines kontrollierten Verzeichniseintrags. / Kind of a controlled directory entry.</summary>
public enum Wave6EntryKind
{
    /// <summary>Verzeichnis. / Directory.</summary>
    Directory,
    /// <summary>Datei. / File.</summary>
    File
}

/// <summary>Sortierung der sichtbaren Dateiliste. / Sort order of the visible file list.</summary>
public enum Wave6Sort
{
    /// <summary>Name. / Name.</summary>
    Name,
    /// <summary>Größe. / Size.</summary>
    Size,
    /// <summary>Änderungszeit. / Modification time.</summary>
    Modified
}

/// <summary>Interne Viewer-Entscheidung. / Internal viewer decision.</summary>
public enum Wave6ViewerDecision
{
    /// <summary>Textvorschau. / Text preview.</summary>
    Text,
    /// <summary>Hexvorschau. / Hex preview.</summary>
    Hex,
    /// <summary>Sichtbarer Auswahlfallback. / Visible selection fallback.</summary>
    Fallback
}

/// <summary>Unterstützte sichere Dateioperation. / Supported safe file operation.</summary>
public enum Wave6OperationKind
{
    /// <summary>Kopieren. / Copy.</summary>
    Copy,
    /// <summary>Umbenennen. / Rename.</summary>
    Rename,
    /// <summary>Löschen. / Delete.</summary>
    Delete,
    /// <summary>Schreibschutz setzen. / Set read-only.</summary>
    SetReadOnly,
    /// <summary>Schreibschutz entfernen. / Clear read-only.</summary>
    ClearReadOnly
}

/// <summary>Zustand einer expliziten Dateioperation. / State of an explicit file operation.</summary>
public enum Wave6OperationState
{
    /// <summary>Wartet auf Entscheidung. / Awaiting decision.</summary>
    AwaitingDecision,
    /// <summary>Bestätigt. / Confirmed.</summary>
    Confirmed,
    /// <summary>Abgebrochen. / Canceled.</summary>
    Canceled,
    /// <summary>Abgelehnt. / Rejected.</summary>
    Rejected,
    /// <summary>Wird ausgeführt. / Executing.</summary>
    Executing,
    /// <summary>Abgeschlossen. / Completed.</summary>
    Completed,
    /// <summary>Fehlgeschlagen. / Failed.</summary>
    Failed
}

/// <summary>Geschlossene Lernpalette. / Closed learning palette.</summary>
public enum Wave6Palette
{
    /// <summary>Standard. / Default.</summary>
    Default,
    /// <summary>Cyan. / Cyan.</summary>
    Cyan,
    /// <summary>Rose. / Rose.</summary>
    Rose,
    /// <summary>Hoher Kontrast. / High contrast.</summary>
    HighContrast
}

/// <summary>Datei- oder Verzeichniseintrag unter der kontrollierten Wurzel. / File or directory entry below the controlled root.</summary>
/// <param name="RelativePath">Relativer Pfad. / Relative path.</param>
/// <param name="Name">Name. / Name.</param>
/// <param name="Kind">Art. / Kind.</param>
/// <param name="Size">Dateigröße. / File size.</param>
/// <param name="LastWriteUtc">Letzte Änderung in UTC. / Last modification in UTC.</param>
/// <param name="ReadOnly">Schreibschutz. / Read-only state.</param>
public readonly record struct Wave6DirectoryEntry(
    string RelativePath,
    string Name,
    Wave6EntryKind Kind,
    long? Size,
    DateTime LastWriteUtc,
    bool ReadOnly);

/// <summary>Stabiler Verzeichnis-Snapshot. / Stable directory snapshot.</summary>
/// <param name="RelativeDirectory">Relatives Verzeichnis. / Relative directory.</param>
/// <param name="Entries">Sortierte Einträge. / Sorted entries.</param>
/// <param name="Filter">Aktiver Filter. / Active filter.</param>
/// <param name="Sort">Sortierung. / Sort order.</param>
/// <param name="Descending">Absteigend. / Descending.</param>
/// <param name="Status">Textstatus. / Text status.</param>
public sealed record Wave6DirectorySnapshot(
    string RelativeDirectory,
    IReadOnlyList<Wave6DirectoryEntry> Entries,
    string Filter,
    Wave6Sort Sort,
    bool Descending,
    string Status);

/// <summary>Begrenzte Vorschau. / Bounded preview.</summary>
/// <param name="RelativePath">Relativer Dateipfad. / Relative file path.</param>
/// <param name="Decision">Viewer-Entscheidung. / Viewer decision.</param>
/// <param name="Content">Textorientierter Inhalt. / Text-first content.</param>
/// <param name="BytesRead">Gelesene Bytes. / Bytes read.</param>
/// <param name="Truncated">Abgeschnitten. / Truncated.</param>
/// <param name="InvalidUtf8">Ungültiges UTF-8 ersetzt. / Invalid UTF-8 replaced.</param>
/// <param name="Status">Textstatus. / Text status.</param>
public sealed record Wave6PreviewResult(
    string RelativePath,
    Wave6ViewerDecision Decision,
    string Content,
    int BytesRead,
    bool Truncated,
    bool InvalidUtf8,
    string Status);

/// <summary>Begrenztes Suchergebnis. / Bounded search result.</summary>
/// <param name="Matches">Relative Treffer. / Relative matches.</param>
/// <param name="VisitedFiles">Besuchte Dateien. / Visited files.</param>
/// <param name="Canceled">Abgebrochen. / Canceled.</param>
/// <param name="LimitReached">Grenze erreicht. / Limit reached.</param>
/// <param name="Status">Textstatus. / Text status.</param>
public sealed record Wave6SearchResult(
    IReadOnlyList<string> Matches,
    int VisitedFiles,
    bool Canceled,
    bool LimitReached,
    string Status);

/// <summary>Eine validierte, noch nicht ausgeführte Dateiaktion. / A validated file action not yet executed.</summary>
/// <param name="OperationId">Einmalige ID. / One-time ID.</param>
/// <param name="Kind">Operationsart. / Operation kind.</param>
/// <param name="SourceRelativePath">Quelle. / Source.</param>
/// <param name="TargetRelativePath">Optionales Ziel. / Optional target.</param>
/// <param name="SourceLength">Quellgröße beim Preview. / Source length at preview.</param>
/// <param name="SourceLastWriteUtc">Quellzeit beim Preview. / Source timestamp at preview.</param>
/// <param name="State">Zustand. / State.</param>
/// <param name="DecisionReason">Entscheidungsgrund. / Decision reason.</param>
public sealed record Wave6OperationIntent(
    Guid OperationId,
    Wave6OperationKind Kind,
    string SourceRelativePath,
    string? TargetRelativePath,
    long SourceLength,
    DateTime SourceLastWriteUtc,
    Wave6OperationState State,
    string DecisionReason);

/// <summary>Terminales Ergebnis einer Dateiaktion. / Terminal result of a file action.</summary>
/// <param name="OperationId">Operations-ID. / Operation identifier.</param>
/// <param name="State">Terminaler Zustand. / Terminal state.</param>
/// <param name="AffectedRelativePaths">Betroffene Pfade. / Affected paths.</param>
/// <param name="ProgressPercent">Fortschritt. / Progress.</param>
/// <param name="ErrorCode">Optionaler Fehlercode. / Optional error code.</param>
/// <param name="RecoveryBoundary">Recovery-Grenze. / Recovery boundary.</param>
public sealed record Wave6OperationResult(
    Guid OperationId,
    Wave6OperationState State,
    IReadOnlyList<string> AffectedRelativePaths,
    int ProgressPercent,
    string? ErrorCode,
    string RecoveryBoundary);
