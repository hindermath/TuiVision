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
}
