// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Steuert sichtbare Fensterfähigkeiten: Schließen und Verschieben.
/// Nur <see cref="Close"/> und <see cref="Move"/> sind für diese Revision definiert;
/// Zoom und Grow liegen außerhalb des aktuellen Geltungsbereichs.
///
/// Controls visible window capabilities: closing and moving.
/// Only <see cref="Close"/> and <see cref="Move"/> are defined for this revision;
/// zoom and grow are out of scope.
/// </summary>
[Flags]
public enum WindowFlags
{
    /// <summary>
    /// Keine besonderen Fähigkeiten aktiviert.
    ///
    /// No special capabilities enabled.
    /// </summary>
    None = 0,

    /// <summary>
    /// Zeigt ein Schließen-Symbol (×) in der Titelzeile an und erlaubt das Schließen
    /// per Ctrl+W sowie per Escape (wenn kein Kind das Ereignis vorher verarbeitet).
    ///
    /// Displays a close affordance (×) in the title bar and allows closing
    /// with Ctrl+W or Escape (when no child has consumed the event first).
    /// </summary>
    Close = 0x01,

    /// <summary>
    /// Ermöglicht das Verschieben des Fensters per Ctrl+F5 gefolgt von Pfeiltasten.
    /// Enter übernimmt die neue Position; Escape stellt die ursprüngliche Position wieder her.
    ///
    /// Enables moving the window via Ctrl+F5 followed by arrow keys.
    /// Enter commits the new position; Escape restores the original position.
    /// </summary>
    Move = 0x02
}
