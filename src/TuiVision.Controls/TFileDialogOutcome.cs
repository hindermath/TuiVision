// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Geschlossene Art eines beobachtbaren Dateidialog-Ergebnisses.
///
/// Closed kind of an observable file-dialog outcome.
/// </summary>
public enum TFileDialogOutcomeKind
{
    /// <summary>Der Verzeichniskontext wurde gewechselt. / The directory context changed.</summary>
    Navigation,

    /// <summary>Der aktive Dateifilter wurde gewechselt. / The active file filter changed.</summary>
    Filter,

    /// <summary>Ein vorhandenes Dateiziel wurde zum Öffnen akzeptiert. / An existing file target was accepted for opening.</summary>
    OpenAccepted,

    /// <summary>Ein neues Speicherziel mit vorhandenem Elternordner wurde akzeptiert. / A new save target with an existing parent was accepted.</summary>
    SaveAccepted,

    /// <summary>Ein vorhandenes Speicherziel benötigt eine nachgelagerte Entscheidung. / An existing save target requires a later decision.</summary>
    OverwriteDecisionRequired,

    /// <summary>Ein vorhandenes Datei- oder Verzeichnisziel wurde ausgewählt. / An existing file or directory target was selected.</summary>
    SelectionAccepted,

    /// <summary>Die Eingabe wurde mit Code und Meldung abgelehnt. / The input was rejected with a code and message.</summary>
    Rejected,

    /// <summary>Der Dialog wurde explizit abgebrochen. / The dialog was explicitly canceled.</summary>
    Canceled
}

/// <summary>
/// Unveränderliches, mode-abhängiges Dateidialog-Ergebnis ohne Besitz an
/// Dateiinhalt oder späterer I/O-Ausführung.
///
/// Immutable, mode-aware file-dialog outcome without ownership of file content
/// or later I/O execution.
/// </summary>
/// <param name="Kind">Die Ergebnisart. / The outcome kind.</param>
/// <param name="Mode">Der angeforderte Dialogmodus. / The requested dialog mode.</param>
/// <param name="Path">Der normalisierte Pfad, sofern vorhanden. / The normalized path, when present.</param>
/// <param name="Filter">Der aktive Filter. / The active filter.</param>
/// <param name="MetadataSnapshot">Der Metadaten-Snapshot. / The metadata snapshot.</param>
/// <param name="RejectionCode">Der stabile Ablehnungscode. / The stable rejection code.</param>
/// <param name="Message">Die text-first Meldung. / The text-first message.</param>
/// <param name="RequiresCallerDecision">Ob eine nachgelagerte Caller-Entscheidung erforderlich ist. / Whether a later caller decision is required.</param>
public readonly record struct TFileDialogOutcome(
    TFileDialogOutcomeKind Kind,
    TFileDialogMode Mode,
    string? Path,
    string? Filter,
    TFileSelectionInfo MetadataSnapshot,
    string? RejectionCode,
    string? Message,
    bool RequiresCallerDecision);
