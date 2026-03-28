// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 13: Dialog mit zwei Schaltflächen (OK und Abbrechen).
/// Entspricht dem Beispiel <c>tvguid13</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 13: Dialog with two buttons (OK and Cancel).
/// Corresponds to the <c>tvguid13</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid13Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid13";

    /// <inheritdoc/>
    public int SequenceNumber => 13;

    /// <inheritdoc/>
    public string Title => "Zwei Schaltflächen / Two buttons";

    /// <inheritdoc/>
    public string Description =>
        "Zeigt einen Dialog mit zwei Schaltflächen (OK und Abbrechen). / " +
        "Shows a dialog with two buttons (OK and Cancel).";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid13App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 13.
///
/// Application class for tutorial step 13.
/// </summary>
internal sealed class TvGuid13App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid13App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid13App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid13App(TRect bounds, bool headless = false) : base(bounds)
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
