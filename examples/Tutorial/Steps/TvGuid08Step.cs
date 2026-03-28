// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 08: Bildlaufleisten und Delta-Punkt.
/// Entspricht dem Beispiel <c>tvguid08</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 08: Scroll bars and delta point.
/// Corresponds to the <c>tvguid08</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid08Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid08";

    /// <inheritdoc/>
    public int SequenceNumber => 8;

    /// <inheritdoc/>
    public string Title => "Bildlaufleisten und Delta-Punkt / Scroll bars and delta point";

    /// <inheritdoc/>
    public string Description =>
        "Zeigt, wie Bildlaufleisten den Delta-Punkt beeinflussen. / " +
        "Shows how scroll bars affect the delta point.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid08App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 08.
///
/// Application class for tutorial step 08.
/// </summary>
internal sealed class TvGuid08App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid08App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid08App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid08App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Fenster mit Scroll-Delta-Demonstration einfügen / Insert window for scroll-delta demonstration
        TWindow window = new("Scroll-Delta", 2, 2, 40, 15);
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

    #region Lösung Übung 1 – Delta-Punkt in der Titelleiste anzeigen / Solution Exercise 1 – Show delta point in the title bar
    /*
     * Überschreibe HandleEvent() und aktualisiere bei jeder Scroll-Ereignis den
     * Fenstertitel, um den aktuellen Delta-Punkt anzuzeigen.
     * Override HandleEvent() and update the window title on each scroll event
     * to display the current delta point.
     *
     * public override void HandleEvent(TEvent @event)
     * {
     *     base.HandleEvent(@event);
     *     if (@event.What == TEventKind.Broadcast)
     *     {
     *         // Delta-Punkt aus dem Fenster auslesen und Titel aktualisieren
     *         // Read delta point from window and update title
     *         Title = $"Delta: ({myWindow.Delta.X}, {myWindow.Delta.Y})";
     *         DrawView();
     *     }
     * }
     */
    #endregion

    #region Lösung Übung 2 – Scrollbaren Bereich begrenzen / Solution Exercise 2 – Limit the scrollable range
    /*
     * Rufe SetLimit() mit einem festen Maximalwert auf, um zu verhindern, dass
     * der Benutzer über den Inhalt hinaus scrollt.
     * Call SetLimit() with a fixed maximum value to prevent the user from
     * scrolling beyond the content.
     *
     * // Maximal 50 Zeilen und 120 Spalten erlauben:
     * // Allow at most 50 rows and 120 columns:
     * SetLimit(50, 120);
     *
     * // TuiVision passt die Scrollbar-Position automatisch an, wenn der
     * // Delta-Punkt den Grenzwert überschreiten würde.
     * // TuiVision automatically adjusts the scroll bar position when the
     * // delta point would exceed the limit.
     */
    #endregion
}
