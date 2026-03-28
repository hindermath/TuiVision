// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 12: Eingabefeld in einem Dialog.
/// Entspricht dem Beispiel <c>tvguid12</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 12: Input line in a dialog.
/// Corresponds to the <c>tvguid12</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid12Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid12";

    /// <inheritdoc/>
    public int SequenceNumber => 12;

    /// <inheritdoc/>
    public string Title => "Eingabefeld im Dialog / Input line in a dialog";

    /// <inheritdoc/>
    public string Description =>
        "Fügt ein Eingabefeld in einen Dialog ein. / " +
        "Adds an input line to a dialog.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid12App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 12.
///
/// Application class for tutorial step 12.
/// </summary>
internal sealed class TvGuid12App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid12App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid12App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid12App(TRect bounds, bool headless = false) : base(bounds)
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
