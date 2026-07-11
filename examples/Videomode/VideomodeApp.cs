// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

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
    /// <summary>Probe-/Wiederholungsbefehl. / Probe/retry command.</summary>
    public const ushort CmProbe = 17031;

    /// <summary>Help-Description-Befehl. / Help description command.</summary>
    public const ushort CmDescription = 17032;

    // Headless-Modus für automatisierte Smoke-Tests.
    // Headless mode for automated smoke tests.
    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private bool _headlessEventFired;
    private TView? _descriptionView;

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
        LastVisibleComponentKind = nameof(VideomodeView);
        LastVisibleRegion = Desktop is null ? new TRect(0, 0, 0, 0) : Wave1Runtime.ScreenRegion(Desktop, View);

        // Übergangsversuch immer sofort ausführen und Ergebnis anzeigen (interaktiv und headless).
        // Always execute the transition attempt immediately and show the result (interactive and headless).
        ApplyProbe();
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

    /// <summary>Letzter kanonischer Ergebniszustand. / Latest canonical result state.</summary>
    public string CanonicalResultState => Coordinator.LastResultState;

    /// <summary>Letzter sichtbarer Komponententyp. / Last visible component type.</summary>
    public string LastVisibleComponentKind { get; private set; }

    /// <summary>Letzte stabile sichtbare Region. / Last stable visible region.</summary>
    public TRect LastVisibleRegion { get; private set; }

    /// <summary>Letzter Statuszeilentext. / Last status-line message.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Text fuer Help -> Description. / Text for Help -> Description.</summary>
    public string DescriptionText =>
        "Videomode description: Die Anwendung prueft eine Terminalgroesse, meldet supported, fallback, rejected oder unchanged und behauptet keinen plattformweiten Moduswechsel. / " +
        "The application probes a terminal size, reports supported, fallback, rejected, or unchanged, and does not claim a platform-wide mode switch.";

    /// <summary>Fuegt deterministische App-Loop-Ereignisse hinzu. / Adds deterministic app-loop events.</summary>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~V~ideomode",
            0,
            Wave1Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
            new TMenuItem("~P~ruefen / ~P~robe", CmProbe))
    };

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave1StatusLine(bounds, Wave1Runtime.Status("Videomode", "probing"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmProbe)
        {
            ApplyProbe();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowDescription();
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

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
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        // Headless-Pfad: einmaliges Quit-Signal für den Smoke-Test / Headless path: one-time quit signal for smoke test
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    private void ApplyProbe()
    {
        DisplayModeOutcome outcome = Coordinator.TryTransition(80, 25);
        string state = Coordinator.LastResultState;
        string message = state switch
        {
            "supported" => "Groessenwechsel unterstuetzt. / Resize supported.",
            "unchanged" => "Zielgroesse bereits aktiv. / Target size already active.",
            "rejected" => "Groessenwechsel wurde abgelehnt. / Resize was rejected.",
            _ => Coordinator.FallbackMessage
        };

        View.ShowOutcome(outcome, message, state);
        if (Desktop is not null)
        {
            LastVisibleRegion = Wave1Runtime.ScreenRegion(Desktop, View);
        }

        LastVisibleComponentKind = nameof(VideomodeView);
        SetStatus(state);
    }

    private void ShowDescription()
    {
        if (Desktop is null)
        {
            return;
        }

        if (_descriptionView?.Owner == Desktop)
        {
            Desktop.Remove(_descriptionView);
        }

        _descriptionView = Wave1Runtime.CreateDescriptionWindow(Desktop, "Videomode", DescriptionText);
        if (_descriptionView is null)
        {
            return;
        }

        Desktop.Insert(_descriptionView);
        LastVisibleComponentKind = "TWindow";
        LastVisibleRegion = Wave1Runtime.ScreenRegion(Desktop, _descriptionView);
        SetStatus("description visible");
    }

    private void SetStatus(string state)
    {
        LastStatusMessage = Wave1Runtime.Status("Videomode", state);
        Wave1Runtime.SetStatus(StatusLine, LastStatusMessage);
    }

    #region Lösung Übung 1 – Menüeintrag für Terminalgrößen / Solution Exercise 1 – Menu entry for terminal sizes
    /*
     * Überschreibe InitMenuBar() in VideomodeApp, um Terminalgrößen als Menüpunkte anzubieten.
     * Override InitMenuBar() in VideomodeApp to offer terminal sizes as menu items.
     *
     * private const int cmMode80x25  = 1001;
     * private const int cmMode132x50 = 1002;
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds,
     *         new TSubMenu("~V~ideomode", 0x2200,
     *             new TMenuItem("80×25",  cmMode80x25,  kbNoKey) +
     *             new TMenuItem("132×50", cmMode132x50, kbNoKey) +
     *             new TMenuItemDivider() +
     *             new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit, kbAltX)));
     * }
     *
     * // In HandleEvent() auf die neuen Befehle reagieren:
     * // In HandleEvent() respond to the new commands:
     * public override void HandleEvent(TEvent @event)
     * {
     *     if (@event.What == TEventKind.Command)
     *     {
     *         switch (@event.Message.Command)
     *         {
     *             case cmMode80x25:
     *                 var o1 = Coordinator.TryTransition(80, 25);
     *                 View.ShowOutcome(o1, o1 == DisplayModeOutcome.RealTransition
     *                     ? "80×25 gesetzt / 80×25 set" : Coordinator.FallbackMessage);
     *                 @event.Clear(); return;
     *             case cmMode132x50:
     *                 var o2 = Coordinator.TryTransition(132, 50);
     *                 View.ShowOutcome(o2, o2 == DisplayModeOutcome.RealTransition
     *                     ? "132×50 gesetzt / 132×50 set" : Coordinator.FallbackMessage);
     *                 @event.Clear(); return;
     *         }
     *     }
     *     base.HandleEvent(@event);
     * }
     */
    #endregion

    #region Lösung Übung 2 – Übergänge protokollieren / Solution Exercise 2 – Log transitions to file
    /*
     * Erstelle eine Hilfsmethode LogTransition(), die jeden Übergangsversuch mit
     * Zeitstempel, Zielgröße und Ergebnis in eine Datei schreibt.
     * Create a helper method LogTransition() that writes each transition attempt
     * with timestamp, target size, and outcome to a file.
     *
     * private void LogTransition(int width, int height, DisplayModeOutcome outcome)
     * {
     *     string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {width}x{height} | {outcome}";
     *     File.AppendAllText("videomode.log", line + Environment.NewLine);
     * }
     *
     * // Aufruf nach TryTransition() / Call after TryTransition():
     * DisplayModeOutcome outcome = Coordinator.TryTransition(80, 25);
     * LogTransition(80, 25, outcome);
     * View.ShowOutcome(outcome, ...);
     */
    #endregion

    #region Lösung Übung 3 – Post-Übergangs-Usability-Test / Solution Exercise 3 – Post-transition usability test
    /*
     * Nach einem erfolgreichen Übergang kannst du prüfen, ob die Anwendung weiterhin
     * korrekt zeichnet, indem du DrawView() auf der Hauptansicht aufrufst und
     * das Ergebnis mit dem erwarteten LastShownOutcome vergleichst.
     * After a successful transition, verify that the application still draws correctly
     * by calling DrawView() on the main view and comparing the result with the
     * expected LastShownOutcome.
     *
     * private bool RunPostTransitionCheck()
     * {
     *     // Ansicht neu zeichnen und Ergebnis prüfen / Redraw the view and check outcome
     *     View.DrawView();
     *     return View.LastShownOutcome == DisplayModeOutcome.RealTransition
     *         && View.LastShownMessage != null;
     * }
     *
     * // Aufruf nach dem Übergang / Call after the transition:
     * DisplayModeOutcome outcome = Coordinator.TryTransition(80, 25);
     * View.ShowOutcome(outcome, ...);
     * bool isHealthy = RunPostTransitionCheck();
     * // isHealthy == true  → Anwendung zeichnet korrekt / application draws correctly
     * // isHealthy == false → Anwendung in inkonsistentem Zustand / application in inconsistent state
     */
    #endregion
}
