// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Definiert die standardmäßigen Shell-Befehl-IDs.
///
/// Defines the standard shell command IDs.
/// </summary>
public static class ShellCommandIds
{
    /// <summary>
    /// Bestätigt einen Dialog oder eine Aktion. / Confirms a dialog or action.
    /// </summary>
    public const ushort cmOK = 10;

    /// <summary>
    /// Bricht einen Dialog oder eine Aktion ab. / Cancels a dialog or action.
    /// </summary>
    public const ushort cmCancel = 11;

    /// <summary>
    /// Bestätigt eine Ja/Nein-Frage mit Ja. / Confirms a yes/no question with yes.
    /// </summary>
    public const ushort cmYes = 12;

    /// <summary>
    /// Bestätigt eine Ja/Nein-Frage mit Nein. / Confirms a yes/no question with no.
    /// </summary>
    public const ushort cmNo = 13;

    /// <summary>
    /// Beendet die Anwendung. / Quits the application.
    /// </summary>
    public const ushort cmQuit = 101;

    /// <summary>
    /// Wird gesendet, wenn sich der Fokus geändert hat. / Sent when focus has changed.
    /// </summary>
    public const ushort cmFocusChanged = 102;

    /// <summary>
    /// Schliesst den aktiven Host. / Closes the active host.
    /// </summary>
    public const ushort cmClose = 103;

    /// <summary>
    /// Speichert das aktive Dokument. / Saves the active document.
    /// </summary>
    public const ushort cmSave = 104;

    /// <summary>
    /// Oeffnet einen Dateidialog. / Opens a file dialog.
    /// </summary>
    public const ushort cmOpen = 105;

    /// <summary>
    /// Aktiviert Rueckgaengig. / Activates undo.
    /// </summary>
    public const ushort cmUndo = 106;

    /// <summary>
    /// Aktiviert Kopieren. / Activates copy.
    /// </summary>
    public const ushort cmCopy = 107;

    /// <summary>
    /// Aktiviert Ausschneiden. / Activates cut.
    /// </summary>
    public const ushort cmCut = 108;

    /// <summary>
    /// Aktiviert Einfuegen. / Activates paste.
    /// </summary>
    public const ushort cmPaste = 109;

    /// <summary>
    /// Aktiviert Suchen. / Activates find.
    /// </summary>
    public const ushort cmFind = 110;

    /// <summary>
    /// Aktiviert Ersetzen. / Activates replace.
    /// </summary>
    public const ushort cmReplace = 111;

    /// <summary>
    /// Oeffnet die Hilfe. / Opens help.
    /// </summary>
    public const ushort cmHelp = 112;

    /// <summary>
    /// Meldet eine Änderung der Runtime-Maus-Capability, damit aktive
    /// Interaktionen fail-safe beendet werden.
    ///
    /// Reports a runtime mouse-capability change so active interactions can end fail-safe.
    /// </summary>
    public const ushort cmMouseCapabilityChanged = 113;
}
