// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Videomode;

/// <summary>
/// Anwendungsklasse für das Videomode-Beispiel.
/// Erkennt die Terminal-Fähigkeiten und versucht einen Bildschirmmodus-Wechsel.
///
/// Application class for the Videomode example.
/// Detects terminal capabilities and attempts a display mode transition.
/// </summary>
public class VideomodeApp : TApplication
{
    // Headless-Modus für automatisierte Smoke-Tests.
    // Headless mode for automated smoke tests.
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="VideomodeApp"/>-Klasse.
    /// Erstellt den <see cref="DisplayModeCoordinator"/> und die <see cref="VideomodeView"/>,
    /// fügt die Ansicht in den Desktop ein und führt im Headless-Modus einen
    /// Übergangsversuch durch.
    ///
    /// Initializes a new instance of the <see cref="VideomodeApp"/> class.
    /// Creates the <see cref="DisplayModeCoordinator"/> and <see cref="VideomodeView"/>,
    /// inserts the view into the desktop, and in headless mode performs a transition attempt.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">
    /// Wenn <c>true</c>, wird ein Übergangsversuch sofort nach der Initialisierung
    /// ausgeführt und die Anwendung beendet sich beim ersten <see cref="GetEvent"/>-Aufruf.
    ///
    /// When <c>true</c>, a transition attempt is executed immediately after initialization
    /// and the application terminates on the first <see cref="GetEvent"/> call.
    /// </param>
    public VideomodeApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Koordinator initialisieren / Initialize coordinator
        Coordinator = new DisplayModeCoordinator();

        // Ansicht im Desktop-Bereich erstellen und einfügen / Create and insert view in desktop area
        TRect desktopBounds = Desktop?.GetBounds() ?? new TRect(0, 1, bounds.Width, bounds.Height - 1);
        TRect viewBounds = new(
            desktopBounds.A.X + 2,
            desktopBounds.A.Y + 1,
            desktopBounds.B.X - 2,
            desktopBounds.A.Y + 8);

        View = new VideomodeView(viewBounds);
        Desktop?.Insert(View);

        // Im Headless-Modus: Übergangsversuch sofort ausführen und Ergebnis anzeigen
        // In headless mode: execute transition attempt immediately and show result
        if (_headless)
        {
            DisplayModeOutcome outcome = Coordinator.TryTransition(80, 25);
            string message = outcome == DisplayModeOutcome.RealTransition
                ? "Übergang erfolgreich / Transition successful"
                : Coordinator.FallbackMessage;
            View.ShowOutcome(outcome, message);
        }
    }

    /// <summary>
    /// Der Modus-Koordinator, der die Terminal-Fähigkeiten erkennt und Übergänge verwaltet.
    ///
    /// The mode coordinator that detects terminal capabilities and manages transitions.
    /// </summary>
    public DisplayModeCoordinator Coordinator { get; }

    /// <summary>
    /// Die Ansicht, die das Ergebnis des Bildschirmmodus-Wechsels anzeigt.
    ///
    /// The view that displays the result of the display mode transition.
    /// </summary>
    public VideomodeView View { get; }

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
}
