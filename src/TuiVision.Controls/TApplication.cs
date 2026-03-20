// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Die Standardklasse für TuiVision-Anwendungen.
/// Sie erstellt automatisch eine Shell mit Menüleiste, Desktop und Statuszeile.
///
/// The standard class for TuiVision applications.
/// It automatically creates a shell with a menu bar, desktop, and status line.
/// </summary>
public class TApplication : TProgram
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TApplication"/>-Klasse.
    /// Erstellt das Standard-Layout mit Menüleiste, Desktop und Statuszeile.
    ///
    /// Initializes a new instance of the <see cref="TApplication"/> class.
    /// Creates the default layout with menu bar, desktop, and status line.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    public TApplication(TRect bounds) : base(bounds)
    {
        // 1. Menüleiste (Höhe 1, oben)
        TRect menuBounds = new(bounds.A.X, bounds.A.Y, bounds.B.X, bounds.A.Y + 1);
        MenuBar = new TMenuBar(menuBounds);
        BuildMenuBar();
        Insert(MenuBar);

        // 2. Statuszeile (Höhe 1, unten)
        TRect statusBounds = new(bounds.A.X, bounds.B.Y - 1, bounds.B.X, bounds.B.Y);
        StatusLine = new TStatusLine(statusBounds);
        BuildStatusLine();
        Insert(StatusLine);

        // 3. Desktop (Füllt den Rest aus)
        TRect desktopBounds = new(bounds.A.X, bounds.A.Y + 1, bounds.B.X, bounds.B.Y - 1);
        Desktop = new TDesktop(desktopBounds);
        Insert(Desktop);
    }

    /// <summary>
    /// Wird während der Initialisierung aufgerufen, um die Menüleiste zu konfigurieren.
    /// Abgeleitete Klassen können diese Methode überschreiben, um Menüpunkte hinzuzufügen.
    ///
    /// Called during initialization to configure the menu bar.
    /// Derived classes can override this method to add menu items.
    /// </summary>
    protected virtual void BuildMenuBar()
    {
    }

    /// <summary>
    /// Wird während der Initialisierung aufgerufen, um die Statuszeile zu konfigurieren.
    /// Abgeleitete Klassen können diese Methode überschreiben, um Status-Einträge hinzuzufügen.
    ///
    /// Called during initialization to configure the status line.
    /// Derived classes can override this method to add status items.
    /// </summary>
    protected virtual void BuildStatusLine()
    {
    }
}
