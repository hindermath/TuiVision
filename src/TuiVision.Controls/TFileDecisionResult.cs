// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Zielart einer Datei- oder Verzeichnisentscheidung.
///
/// Target kind of a file or directory decision.
/// </summary>
public enum FileSelectionTargetKind
{
    /// <summary>Kein Ziel, zum Beispiel bei Abbruch. / No target, for example on cancel.</summary>
    None,

    /// <summary>Dateiziel. / File target.</summary>
    File,

    /// <summary>Verzeichnisziel. / Directory target.</summary>
    Directory
}

/// <summary>
/// Art der caller-sichtbaren Dateidialogentscheidung.
///
/// Kind of caller-visible file-dialog decision.
/// </summary>
public enum FileDecisionKind
{
    /// <summary>Abbruch. / Canceled.</summary>
    Canceled,

    /// <summary>Oeffnen eines vorhandenen Ziels. / Open an existing target.</summary>
    Open,

    /// <summary>Auswahl eines Datei- oder Verzeichnisziels. / Select a file or directory target.</summary>
    Select,

    /// <summary>Speicherziel, ohne Dateiinhalt zu schreiben. / Save target without writing file content.</summary>
    SaveTarget
}

/// <summary>
/// Explizites Ergebnis eines Datei- oder Verzeichnisdialogs.
///
/// Explicit result of a file or directory dialog.
/// </summary>
/// <param name="Kind">Die Entscheidungsart. / The decision kind.</param>
/// <param name="TargetKind">Die Zielart. / The target kind.</param>
/// <param name="Path">Der aufgeloeste Zielpfad. / The resolved target path.</param>
/// <param name="Filter">Der aktive Filter. / The active filter.</param>
/// <param name="MetadataSnapshot">Der Metadaten-Snapshot. / The metadata snapshot.</param>
/// <param name="RequiresCallerDecision">Ob der Aufrufer eine Inhaltsentscheidung treffen muss. / Whether the caller must make a content decision.</param>
public readonly record struct TFileDecisionResult(
    FileDecisionKind Kind,
    FileSelectionTargetKind TargetKind,
    string? Path,
    string? Filter,
    TFileSelectionInfo MetadataSnapshot,
    bool RequiresCallerDecision)
{
    /// <summary>
    /// Erstellt ein Abbruchergebnis.
    ///
    /// Creates a cancellation result.
    /// </summary>
    /// <returns>Das Abbruchergebnis. / The cancellation result.</returns>
    public static TFileDecisionResult Canceled() =>
        new(FileDecisionKind.Canceled, FileSelectionTargetKind.None, null, null, default, false);
}
