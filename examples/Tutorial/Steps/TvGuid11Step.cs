// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 11: Schaltflächen in einem Dialog.
/// Entspricht dem Beispiel <c>tvguid11</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 11: Buttons in a dialog.
/// Corresponds to the <c>tvguid11</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid11Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid11";

    /// <inheritdoc/>
    public int SequenceNumber => 11;

    /// <inheritdoc/>
    public string Title => "Schaltflächen im Dialog / Buttons in a dialog";

    /// <inheritdoc/>
    public string Description =>
        "Fügt Schaltflächen in einen Dialog ein. / " +
        "Inserts buttons into a dialog.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid11App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 11.
///
/// Application class for tutorial step 11.
/// </summary>
internal sealed class TvGuid11App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid11App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid11App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid11App(TRect bounds, bool headless = false) : base(bounds)
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
