// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 09: Mehrere Fenster und Z-Reihenfolge.
/// Entspricht dem Beispiel <c>tvguid09</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 09: Multiple windows and Z-order.
/// Corresponds to the <c>tvguid09</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid09Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid09";

    /// <inheritdoc/>
    public int SequenceNumber => 9;

    /// <inheritdoc/>
    public string Title => "Mehrere Fenster / Multiple windows";

    /// <inheritdoc/>
    public string Description =>
        "Öffnet mehrere Fenster und zeigt die Z-Reihenfolge. / " +
        "Opens multiple windows and demonstrates Z-order.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid09App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 09.
///
/// Application class for tutorial step 09.
/// </summary>
internal sealed class TvGuid09App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid09App"/>-Klasse.
    /// Erstellt mehrere Fenster mit unterschiedlicher Z-Position.
    ///
    /// Initializes a new instance of the <see cref="TvGuid09App"/> class.
    /// Creates multiple windows with different Z-positions.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid09App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Mehrere Fenster einfügen und Z-Reihenfolge demonstrieren
        // Insert multiple windows to demonstrate Z-order
        TWindow window1 = new("Fenster 1 / Window 1", 1, 1, 30, 10);
        TWindow window2 = new("Fenster 2 / Window 2", 6, 4, 30, 10);
        TWindow window3 = new("Fenster 3 / Window 3", 12, 7, 30, 10);
        Desktop?.Insert(window1);
        Desktop?.Insert(window2);
        Desktop?.Insert(window3);
    }

    /// <inheritdoc/>
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    #region Lösung Übung 1 – Drei Fenster mit verschiedenen Titeln öffnen / Solution Exercise 1 – Open three windows with different titles
    /*
     * Erstelle beim Start drei TWindow-Instanzen mit je einem eigenen Titel und
     * füge sie an verschiedenen Positionen in den Desktop ein.
     * Create three TWindow instances at startup, each with its own title,
     * and insert them at different positions into the desktop.
     *
     * Desktop?.Insert(new TWindow("Fenster 1 / Window 1",  2,  1, 30, 8));
     * Desktop?.Insert(new TWindow("Fenster 2 / Window 2",  8,  4, 30, 8));
     * Desktop?.Insert(new TWindow("Fenster 3 / Window 3", 14,  7, 30, 8));
     *
     * // Das zuletzt eingefügte Fenster liegt automatisch oben (Z-Reihenfolge).
     * // The last inserted window is automatically on top (Z-order).
     */
    #endregion

    #region Lösung Übung 2 – Zwischen Fenstern wechseln / Solution Exercise 2 – Switch between windows
    /*
     * Rufe Desktop?.SelectNext(false) auf (z. B. über einen Menübefehl), um
     * zyklisch zum nächsten Fenster zu wechseln — äquivalent zu F6 in Turbo Vision.
     * Call Desktop?.SelectNext(false) (e.g. via a menu command) to cycle
     * to the next window — equivalent to F6 in Turbo Vision.
     *
     * private const int cmNextWindow = 300;
     *
     * // In InitMenuBar():
     * new TMenuItem("~N~ächstes Fenster / ~N~ext Window", cmNextWindow, kbF6)
     *
     * // In HandleEvent():
     * case cmNextWindow:
     *     Desktop?.SelectNext(false);
     *     @event.Clear(); return;
     */
    #endregion
}
