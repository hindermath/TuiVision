// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 06: Vertikale Bildlaufleiste in einem Fenster.
/// Entspricht dem Beispiel <c>tvguid06</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 06: Vertical scroll bar in a window.
/// Corresponds to the <c>tvguid06</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid06Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid06";

    /// <inheritdoc/>
    public int SequenceNumber => 6;

    /// <inheritdoc/>
    public string Title => "Vertikale Bildlaufleiste / Vertical scroll bar";

    /// <inheritdoc/>
    public string Description =>
        "Fügt eine vertikale Bildlaufleiste zu einem Fenster hinzu. / " +
        "Adds a vertical scroll bar to a window.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid06App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 06.
///
/// Application class for tutorial step 06.
/// </summary>
internal sealed class TvGuid06App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid06App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid06App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid06App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Fenster mit vertikaler Bildlaufleiste einfügen / Insert window with vertical scroll bar
        TWindow window = new("Vertikale Leiste / Vertical Bar", 2, 2, 40, 15);
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
}
