// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 07: Horizontale und vertikale Bildlaufleisten.
/// Entspricht dem Beispiel <c>tvguid07</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 07: Horizontal and vertical scroll bars.
/// Corresponds to the <c>tvguid07</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid07Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid07";

    /// <inheritdoc/>
    public int SequenceNumber => 7;

    /// <inheritdoc/>
    public string Title => "Horizontale und vertikale Bildlaufleisten / Horizontal and vertical scroll bars";

    /// <inheritdoc/>
    public string Description =>
        "Fügt beide Bildlaufleisten zu einem Fenster hinzu. / " +
        "Adds both scroll bars to a window.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid07App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 07.
///
/// Application class for tutorial step 07.
/// </summary>
internal sealed class TvGuid07App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid07App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid07App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid07App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Fenster für beide Bildlaufleisten einfügen / Insert window for both scroll bars
        TWindow window = new("Beide Leisten / Both Bars", 2, 2, 40, 15);
        Desktop?.Insert(window);
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

    #region Lösung Übung 1 – Beide Bildlaufleisten mit Inhalt verknüpfen / Solution Exercise 1 – Link both scroll bars to content
    /*
     * Erstelle sowohl eine horizontale als auch eine vertikale TScrollBar und
     * verknüpfe sie über SetLimit() mit den Inhaltsdimensionen.
     * Create both a horizontal and a vertical TScrollBar and link them
     * to the content dimensions via SetLimit().
     *
     * // Vertikale Scrollbar / Vertical scroll bar:
     * var vScroll = new TScrollBar(new TRect(bounds.B.X - 1, bounds.A.Y + 1,
     *                                        bounds.B.X,     bounds.B.Y - 1),
     *                              TScrollBarKind.Vertical);
     * Insert(vScroll);
     * VScrollBar = vScroll;
     *
     * // Horizontale Scrollbar / Horizontal scroll bar:
     * var hScroll = new TScrollBar(new TRect(bounds.A.X + 1, bounds.B.Y - 1,
     *                                        bounds.B.X - 1, bounds.B.Y),
     *                              TScrollBarKind.Horizontal);
     * Insert(hScroll);
     * HScrollBar = hScroll;
     *
     * // Gesamtdimensionen setzen (Zeilen × Spalten) / Set total dimensions (rows × columns):
     * SetLimit(200, 80);
     */
    #endregion
}
