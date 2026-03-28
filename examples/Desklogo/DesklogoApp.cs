// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Desklogo;

/// <summary>
/// Minimale TApplication-Unterklasse für das Desklogo-Beispiel.
/// Verwendet einen benutzerdefinierten Desktop, der ein ASCII-Logo darstellt.
///
/// Minimal TApplication subclass for the Desklogo example.
/// Uses a custom desktop that renders an ASCII logo.
/// </summary>
public class DesklogoApp : TApplication
{
    // Headless-Modus für automatisierte Smoke-Tests.
    // Headless mode for automated smoke tests.
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DesklogoApp"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="DesklogoApp"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">
    /// Wenn <c>true</c>, liefert <see cref="GetEvent"/> sofort einen Quit-Befehl
    /// und ermöglicht so das in-process Testen ohne Konsoleninteraktion.
    ///
    /// When <c>true</c>, <see cref="GetEvent"/> immediately delivers a quit command,
    /// enabling in-process testing without console interaction.
    /// </param>
    public DesklogoApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
    }

    /// <summary>
    /// Erstellt den benutzerdefinierten <see cref="DesklogoDesktop"/> für die Anwendungsshell.
    ///
    /// Creates the custom <see cref="DesklogoDesktop"/> for the application shell.
    /// </summary>
    /// <param name="bounds">Die Grenzen des Desktops. / The bounds of the desktop.</param>
    /// <returns>
    /// Eine neue <see cref="DesklogoDesktop"/>-Instanz.
    /// A new <see cref="DesklogoDesktop"/> instance.
    /// </returns>
    protected override TDesktop InitDesktop(TRect bounds) => new DesklogoDesktop(bounds);

    /// <summary>
    /// Ruft das nächste Ereignis ab.
    /// Im Headless-Modus wird beim ersten Aufruf ein Quit-Befehl zurückgegeben.
    ///
    /// Retrieves the next event.
    /// In headless mode, a quit command is returned on the first call.
    /// </summary>
    /// <param name="event">Das abgerufene Ereignis. / The retrieved event.</param>
    public override void GetEvent(out TEvent @event)
    {
        // Headless-Pfad: einmaliges Quit-Signal für den Smoke-Test / Headless path: one-time quit signal for smoke test
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    #region Lösung Übung 2 – Menüleiste hinzufügen / Solution Exercise 2 – Add a menu bar
    /*
     * Überschreibe InitMenuBar() in DesklogoApp, um eine Menüleiste mit einem Untermenü hinzuzufügen.
     * Override InitMenuBar() in DesklogoApp to add a menu bar with one submenu.
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds,
     *         new TSubMenu("~D~esklogo", 0x2100,
     *             new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit, kbAltX)));
     * }
     */
    #endregion
}
