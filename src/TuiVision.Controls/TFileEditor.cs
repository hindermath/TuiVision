// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Repräsentiert den gespeicherten Dateistand fuer Konflikterkennung.
///
/// Represents the stored file state for conflict detection.
/// </summary>
/// <param name="Length">Die Dateilaenge. / The file length.</param>
/// <param name="LastWriteTicks">Der letzte Schreibzeitpunkt in UTC-Ticks. / The last write time in UTC ticks.</param>
public readonly record struct TFileSnapshot(long Length, long LastWriteTicks);

/// <summary>
/// Klassifiziert Speicherkonflikte fuer Dateieditoren.
///
/// Classifies save conflicts for file editors.
/// </summary>
public enum TFileSaveConflictKind
{
    /// <summary>Existierendes Ziel wird ersetzt. / An existing target is replaced.</summary>
    ReplaceExisting,

    /// <summary>Die Quelldatei wurde extern geaendert. / The source file was modified externally.</summary>
    ExternalModification
}

/// <summary>
/// Beschreibt eine Save-Konfliktsituation.
///
/// Describes a save conflict situation.
/// </summary>
/// <param name="Path">Der betroffene Pfad. / The affected path.</param>
/// <param name="Kind">Die Konfliktart. / The conflict kind.</param>
public readonly record struct TFileSaveConflict(string Path, TFileSaveConflictKind Kind);

/// <summary>
/// Beschreibt den Zeilenendungsmodus des aktiven Dokuments.
///
/// Describes the line-ending mode of the active document.
/// </summary>
public enum TLineEndingMode
{
    /// <summary>Line feed. / Line feed.</summary>
    Lf,

    /// <summary>Carriage return + line feed. / Carriage return + line feed.</summary>
    CrLf
}

/// <summary>
/// Dateigebundener Editor mit Snapshot-basierter Konflikterkennung.
///
/// File-backed editor with snapshot-based conflict detection.
/// </summary>
public sealed class TFileEditor : TEditor
{
    /// <summary>
    /// Initialisiert einen neuen Dateieditor.
    ///
    /// Initializes a new file editor.
    /// </summary>
    /// <param name="bounds">Die Bounds des Editors. / The editor bounds.</param>
    /// <param name="fileName">Optionaler Dateiname. / Optional file name.</param>
    public TFileEditor(TRect bounds, string? fileName = null) : base(bounds)
    {
        FileName = fileName;
    }

    /// <summary>
    /// Der aktuell gebundene Dateiname.
    ///
    /// The currently attached file name.
    /// </summary>
    public string? FileName { get; private set; }

    /// <summary>
    /// Der Zeilenendungsmodus des Dokuments.
    ///
    /// The document line-ending mode.
    /// </summary>
    public TLineEndingMode LineEndingMode { get; private set; } = TLineEndingMode.Lf;

    /// <summary>
    /// Der zuletzt bekannte Dateisnapshot.
    ///
    /// The last known file snapshot.
    /// </summary>
    public TFileSnapshot? Snapshot { get; private set; }

    /// <summary>
    /// Optionale Callback-Funktion fuer Overwrite-Entscheidungen.
    ///
    /// Optional callback for overwrite decisions.
    /// </summary>
    public Func<TFileSaveConflict, bool>? ConfirmOverwrite { get; set; }

    /// <summary>
    /// Laedt eine Datei in den Editor.
    ///
    /// Loads a file into the editor.
    /// </summary>
    /// <param name="path">Der Dateipfad. / The file path.</param>
    public void LoadFile(string path)
    {
        string content = File.ReadAllText(path);
        LineEndingMode = content.Contains("\r\n", StringComparison.Ordinal) ? TLineEndingMode.CrLf : TLineEndingMode.Lf;
        FileName = Path.GetFullPath(path);
        LoadText(content, markClean: true);
        Snapshot = CaptureSnapshot(FileName);
    }

    /// <summary>
    /// Speichert das Dokument unter dem aktuellen Dateinamen.
    ///
    /// Saves the document under the current file name.
    /// </summary>
    /// <returns><c>true</c>, wenn gespeichert wurde. / <c>true</c> if saving succeeded.</returns>
    public bool Save()
    {
        if (string.IsNullOrWhiteSpace(FileName))
        {
            throw new InvalidOperationException("The editor is not attached to a file.");
        }

        return SaveAs(FileName);
    }

    /// <summary>
    /// Speichert das Dokument unter einem Zielpfad.
    ///
    /// Saves the document to a target path.
    /// </summary>
    /// <param name="path">Der Zielpfad. / The target path.</param>
    /// <returns><c>true</c>, wenn gespeichert wurde. / <c>true</c> if saving succeeded.</returns>
    public bool SaveAs(string path)
    {
        string fullPath = Path.GetFullPath(path);
        TFileSaveConflict? conflict = GetPendingConflict(fullPath);
        if (conflict is TFileSaveConflict pending && !(ConfirmOverwrite?.Invoke(pending) ?? false))
        {
            return false;
        }

        string persisted = LineEndingMode == TLineEndingMode.CrLf
            ? GetText().Replace("\n", "\r\n", StringComparison.Ordinal)
            : GetText();
        File.WriteAllText(fullPath, persisted);
        FileName = fullPath;
        Snapshot = CaptureSnapshot(fullPath);
        MarkClean();
        return true;
    }

    private TFileSaveConflict? GetPendingConflict(string fullPath)
    {
        if (File.Exists(fullPath))
        {
            if (FileName is not null && string.Equals(fullPath, FileName, StringComparison.Ordinal))
            {
                TFileSnapshot current = CaptureSnapshot(fullPath);
                if (Snapshot is TFileSnapshot snapshot && snapshot != current)
                {
                    return new TFileSaveConflict(fullPath, TFileSaveConflictKind.ExternalModification);
                }
            }
            else
            {
                return new TFileSaveConflict(fullPath, TFileSaveConflictKind.ReplaceExisting);
            }
        }

        return null;
    }

    private static TFileSnapshot CaptureSnapshot(string path)
    {
        FileInfo info = new(path);
        return new TFileSnapshot(info.Length, info.LastWriteTimeUtc.Ticks);
    }
}
