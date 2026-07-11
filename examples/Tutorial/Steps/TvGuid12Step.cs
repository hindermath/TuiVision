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
    public string Title => "Modales Dialogverhalten / Modal dialog behaviour";

    /// <inheritdoc/>
    public string Description =>
        "Führt den Dialog modal aus und wertet sein Ergebnis aus. / " +
        "Runs the dialog modally and evaluates its result.";

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

    #region Lösung Übung 1 – Maximale Eingabelänge begrenzen / Solution Exercise 1 – Limit maximum input length
    /*
     * Übergib die maximale Zeichenanzahl als zweiten Parameter an TInputLine.
     * Pass the maximum character count as the second parameter to TInputLine.
     *
     * // Maximal 20 Zeichen erlauben / Allow at most 20 characters:
     * var input = new TInputLine(new TRect(3, 3, 37, 4), 20);
     * dialog.Insert(input);
     *
     * // TuiVision schneidet die Eingabe automatisch ab, wenn das Limit erreicht ist.
     * // TuiVision automatically truncates input when the limit is reached.
     */
    #endregion

    #region Lösung Übung 2 – Eingabe nach Dialog-Schließen anzeigen / Solution Exercise 2 – Display input after dialog closes
    /*
     * Lese den Inhalt des TInputLine-Feldes nach ExecView() aus und zeige ihn an.
     * Read the TInputLine content after ExecView() and display it.
     *
     * var input = new TInputLine(new TRect(3, 3, 37, 4), 80);
     * dialog.Insert(input);
     *
     * int result = ExecView(dialog);
     * if (result == cmOK)
     * {
     *     string entered = input.Data ?? string.Empty;
     *     // Eingabe im Desktop-Fenster anzeigen / Display input in a desktop window:
     *     var infoWin = new TWindow($"Eingabe: {entered}", 5, 5, 50, 8);
     *     Desktop?.Insert(infoWin);
     * }
     */
    #endregion
}
