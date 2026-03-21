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
}
