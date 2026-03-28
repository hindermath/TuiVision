// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 14: Kontrollkästchen und Optionsfelder in einem Dialog.
/// Entspricht dem Beispiel <c>tvguid14</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 14: Check boxes and radio buttons in a dialog.
/// Corresponds to the <c>tvguid14</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid14Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid14";

    /// <inheritdoc/>
    public int SequenceNumber => 14;

    /// <inheritdoc/>
    public string Title => "Kontrollkästchen und Optionsfelder / Check boxes and radio buttons";

    /// <inheritdoc/>
    public string Description =>
        "Fügt Kontrollkästchen und Optionsfelder in einen Dialog ein. / " +
        "Adds check boxes and radio buttons to a dialog.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid14App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 14.
///
/// Application class for tutorial step 14.
/// </summary>
internal sealed class TvGuid14App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid14App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid14App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid14App(TRect bounds, bool headless = false) : base(bounds)
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
