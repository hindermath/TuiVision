// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 15: Dialogdaten in einer Struktur speichern.
/// Entspricht dem Beispiel <c>tvguid15</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 15: Saving dialog contents to a structure.
/// Corresponds to the <c>tvguid15</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid15Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid15";

    /// <inheritdoc/>
    public int SequenceNumber => 15;

    /// <inheritdoc/>
    public string Title => "Dialogdaten speichern / Saving dialog data";

    /// <inheritdoc/>
    public string Description =>
        "Speichert Dialoginhalte in einer Struktur. / " +
        "Saves dialog contents to a structure.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid15App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 15.
///
/// Application class for tutorial step 15.
/// </summary>
internal sealed class TvGuid15App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid15App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid15App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid15App(TRect bounds, bool headless = false) : base(bounds)
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
