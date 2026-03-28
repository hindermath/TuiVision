// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 04: Ein TWindow auf dem Desktop öffnen.
/// Entspricht dem Beispiel <c>tvguid04</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 04: Opening a TWindow on the desktop.
/// Corresponds to the <c>tvguid04</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid04Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid04";

    /// <inheritdoc/>
    public int SequenceNumber => 4;

    /// <inheritdoc/>
    public string Title => "Ein TWindow öffnen / Opening a TWindow";

    /// <inheritdoc/>
    public string Description =>
        "Öffnet ein Fenster auf dem Desktop. / " +
        "Opens a window on the desktop.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid04App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 04.
///
/// Application class for tutorial step 04.
/// </summary>
internal sealed class TvGuid04App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid04App"/>-Klasse.
    /// Öffnet ein einfaches Fenster auf dem Desktop.
    ///
    /// Initializes a new instance of the <see cref="TvGuid04App"/> class.
    /// Opens a simple window on the desktop.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid04App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Einfaches Fenster erzeugen und einfügen / Create and insert a simple window
        TWindow window = new("Fenster / Window", 2, 2, 40, 10);
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
