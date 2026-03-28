// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 03: Menübefehl-Verarbeitung.
/// Entspricht dem Beispiel <c>tvguid03</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 03: Menu command handling.
/// Corresponds to the <c>tvguid03</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid03Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid03";

    /// <inheritdoc/>
    public int SequenceNumber => 3;

    /// <inheritdoc/>
    public string Title => "Menübefehl-Verarbeitung / Menu command handling";

    /// <inheritdoc/>
    public string Description =>
        "Verarbeitet Befehle aus der Menüleiste. / " +
        "Handles commands from the menu bar.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid03App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 03.
///
/// Application class for tutorial step 03.
/// </summary>
internal sealed class TvGuid03App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid03App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid03App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid03App(TRect bounds, bool headless = false) : base(bounds)
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
}
