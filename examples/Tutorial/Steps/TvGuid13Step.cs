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

    #region Lösung Übung 1 – Rückgabewert auswerten / Solution Exercise 1 – Evaluate the return value
    /*
     * Werte den Rückgabewert von ExecView() aus, um unterschiedlich auf OK und
     * Abbrechen zu reagieren.
     * Evaluate the return value of ExecView() to react differently to OK and Cancel.
     *
     * int result = ExecView(dialog);
     * if (result == cmOK)
     * {
     *     // Aktion bei OK / Action on OK
     *     Desktop?.Insert(new TWindow("OK gewählt / OK chosen", 5, 3, 40, 6));
     * }
     * else
     * {
     *     // Aktion bei Abbrechen / Action on Cancel
     *     Desktop?.Insert(new TWindow("Abgebrochen / Cancelled", 5, 3, 40, 6));
     * }
     */
    #endregion

    #region Lösung Übung 2 – Dritte Schaltfläche „Hilfe" hinzufügen / Solution Exercise 2 – Add a third "Help" button
    /*
     * Füge eine dritte Schaltfläche mit einem benutzerdefinierten Befehlscode hinzu.
     * Add a third button with a custom command code.
     *
     * private const int cmHelp = 500;
     *
     * var btnHelp = new TButton(new TRect(38, 8, 50, 10), "~H~ilfe / ~H~elp",
     *                            cmHelp, TButtonFlags.Normal);
     * dialog.Insert(btnHelp);
     *
     * // In HandleEvent() auf cmHelp reagieren / React to cmHelp in HandleEvent():
     * if (@event.Message.Command == cmHelp)
     * {
     *     // Hilfetext anzeigen / Show help text
     *     @event.Clear(); return;
     * }
     */
    #endregion
}
