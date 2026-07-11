// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Die Standardklasse für TuiVision-Anwendungen.
/// Sie erstellt automatisch eine Shell mit Menüleiste, Desktop und Statuszeile
/// und bietet virtuelle Initialisierungsmethoden zur Anpassung dieser Regionen.
///
/// The standard class for TuiVision applications.
/// It automatically creates a shell with a menu bar, desktop, and status line,
/// and exposes virtual initialization methods for customizing those regions.
/// </summary>
public class TApplication : TProgram
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TApplication"/>-Klasse.
    /// Erstellt das Standard-Layout mit Menüleiste, Desktop und Statuszeile
    /// und setzt den initialen Fokus auf den Desktop (FR-006).
    ///
    /// Initializes a new instance of the <see cref="TApplication"/> class.
    /// Creates the default layout with menu bar, desktop, and status line,
    /// and sets the initial focus to the desktop (FR-006).
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    public TApplication(TRect bounds) : base(bounds)
    {
        // Die Randzeilen bleiben für globale Befehle reserviert; der Desktop erhält nur den sicheren Arbeitsbereich.
        // The edge rows stay reserved for global commands; the desktop receives only the safe workspace.
        TRect menuBounds = new(bounds.A.X, bounds.A.Y, bounds.B.X, bounds.A.Y + 1);
        MenuBar = InitMenuBar(menuBounds);
        Insert(MenuBar);

        TRect statusBounds = new(bounds.A.X, bounds.B.Y - 1, bounds.B.X, bounds.B.Y);
        StatusLine = InitStatusLine(statusBounds);
        Insert(StatusLine);

        TRect desktopBounds = new(bounds.A.X, bounds.A.Y + 1, bounds.B.X, bounds.B.Y - 1);
        Desktop = InitDesktop(desktopBounds);
        Insert(Desktop);

        // Der Desktop startet fokussiert, damit Tastaturbefehle sofort einen stabilen Shell-Empfänger haben.
        // The desktop starts focused so keyboard commands immediately have a stable shell receiver.
        SetFocus(Desktop);
    }

    /// <summary>
    /// Erstellt die Menüleiste für die Standardshell.
    /// Abgeleitete Klassen können diese Methode überschreiben, um eine andere
    /// <see cref="TMenuBar"/>-Instanz oder -Unterklasse zurückzugeben.
    ///
    /// Creates the menu bar for the default shell.
    /// Derived classes can override this method to return a different
    /// <see cref="TMenuBar"/> instance or subclass.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Menüleiste. / The bounds of the menu bar.</param>
    /// <returns>Die zu verwendende Menüleisten-Instanz. / The menu bar instance to use.</returns>
    protected virtual TMenuBar InitMenuBar(TRect bounds) => new(bounds);

    /// <summary>
    /// Erstellt den Desktop-Arbeitsbereich für die Standardshell.
    /// Abgeleitete Klassen können diese Methode überschreiben, um einen anderen
    /// <see cref="TDesktop"/>-Typ zurückzugeben.
    ///
    /// Creates the desktop workspace for the default shell.
    /// Derived classes can override this method to return a different
    /// <see cref="TDesktop"/> type.
    /// </summary>
    /// <param name="bounds">Die Grenzen des Desktops. / The bounds of the desktop.</param>
    /// <returns>Die zu verwendende Desktop-Instanz. / The desktop instance to use.</returns>
    protected virtual TDesktop InitDesktop(TRect bounds) => new(bounds);

    /// <summary>
    /// Erstellt die Statuszeile für die Standardshell.
    /// Abgeleitete Klassen können diese Methode überschreiben, um eine andere
    /// <see cref="TStatusLine"/>-Instanz oder -Unterklasse zurückzugeben.
    ///
    /// Creates the status line for the default shell.
    /// Derived classes can override this method to return a different
    /// <see cref="TStatusLine"/> instance or subclass.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Statuszeile. / The bounds of the status line.</param>
    /// <returns>Die zu verwendende Statuszeilen-Instanz. / The status line instance to use.</returns>
    protected virtual TStatusLine InitStatusLine(TRect bounds) => new(bounds);
}
