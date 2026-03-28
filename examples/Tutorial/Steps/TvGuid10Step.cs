// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 10: Einen modalen TDialog öffnen.
/// Entspricht dem Beispiel <c>tvguid10</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 10: Opening a modal TDialog.
/// Corresponds to the <c>tvguid10</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid10Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid10";

    /// <inheritdoc/>
    public int SequenceNumber => 10;

    /// <inheritdoc/>
    public string Title => "Ein TDialog öffnen / Opening a TDialog";

    /// <inheritdoc/>
    public string Description =>
        "Zeigt, wie ein modaler Dialog geöffnet wird. / " +
        "Shows how to open a modal dialog.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid10App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 10.
///
/// Application class for tutorial step 10.
/// </summary>
internal sealed class TvGuid10App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid10App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid10App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid10App(TRect bounds, bool headless = false) : base(bounds)
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
