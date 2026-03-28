// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 01: Minimale TApplication ohne Anpassungen.
/// Entspricht dem Beispiel <c>tvguid01</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 01: Minimal TApplication without customizations.
/// Corresponds to the <c>tvguid01</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid01Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid01";

    /// <inheritdoc/>
    public int SequenceNumber => 1;

    /// <inheritdoc/>
    public string Title => "Minimale TApplication / Minimal TApplication";

    /// <inheritdoc/>
    public string Description =>
        "Die einfachste Turbo-Vision-Anwendung: nur TApplication ohne Anpassungen. / " +
        "The simplest Turbo Vision application: just TApplication without customization.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid01App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 01.
///
/// Application class for tutorial step 01.
/// </summary>
internal sealed class TvGuid01App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid01App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid01App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid01App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
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

    #region Lösung Übung 1 – InitMenuBar() überschreiben / Solution Exercise 1 – Override InitMenuBar()
    /*
     * Überschreibe InitMenuBar() in TvGuid01App, um eine leere Menüleiste zu erzeugen.
     * Override InitMenuBar() in TvGuid01App to produce an empty menu bar.
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds, null);
     * }
     *
     * Ergebnis: Die Menüleiste erscheint oben, ist aber leer.
     * Result: The menu bar appears at the top but is empty.
     */
    #endregion

    #region Lösung Übung 2 – Terminalgröße beobachten / Solution Exercise 2 – Observe terminal size
    /*
     * Die Terminalgröße beeinflusst die bounds, die dem Konstruktor übergeben werden.
     * Starte die Anwendung in einem kleineren oder größeren Terminalfenster und beobachte,
     * wie sich der Desktop anpasst. Du musst dafür keinen Code ändern.
     * The terminal size influences the bounds passed to the constructor.
     * Start the application in a smaller or larger terminal window and observe
     * how the desktop adapts. No code change is required.
     *
     * // Experimentiere mit verschiedenen Terminalgrößen, z. B.:
     * // Experiment with different terminal sizes, e.g.:
     * // Terminal → 40×12: kleiner Desktop / small desktop
     * // Terminal → 132×50: großer Desktop / large desktop
     */
    #endregion
}
