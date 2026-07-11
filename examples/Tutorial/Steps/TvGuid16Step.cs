// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 16: Dialogdaten speichern und wiederherstellen.
/// Entspricht dem Beispiel <c>tvguid16</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 16: Save and restore dialog data.
/// Corresponds to the <c>tvguid16</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid16Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid16";

    /// <inheritdoc/>
    public int SequenceNumber => 16;

    /// <inheritdoc/>
    public string Title => "Dialogdaten übertragen und validieren / Dialog data transfer and validation";

    /// <inheritdoc/>
    public string Description =>
        "Überträgt, speichert, stellt wieder her und validiert Dialogdaten. / " +
        "Transfers, saves, restores, and validates dialog data.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid16App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 16.
///
/// Application class for tutorial step 16.
/// </summary>
internal sealed class TvGuid16App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid16App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid16App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid16App(TRect bounds, bool headless = false) : base(bounds)
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

    #region Lösung Übung 1 – Reset-Button für Standardwerte / Solution Exercise 1 – Reset button for default values
    /*
     * Füge dem Dialog einen Reset-Button hinzu, der alle Felder auf Standardwerte
     * zurücksetzt, ohne den Dialog zu schließen.
     * Add a Reset button to the dialog that resets all fields to default values
     * without closing the dialog.
     *
     * private const int cmReset = 600;
     *
     * var btnReset = new TButton(new TRect(3, 10, 17, 12), "~Z~urücksetzen / ~R~eset",
     *                             cmReset, TButtonFlags.Normal);
     * dialog.Insert(btnReset);
     *
     * // Im Dialog-HandleEvent (Unterklasse von TDialog):
     * // In dialog's HandleEvent (TDialog subclass):
     * if (@event.Message.Command == cmReset)
     * {
     *     var defaults = new MyDialogData(); // Standardkonstruktor / default constructor
     *     SetData(ref defaults);
     *     DrawView();
     *     @event.Clear(); return;
     * }
     */
    #endregion

    #region Lösung Übung 2 – Eingabe vor dem Schließen validieren / Solution Exercise 2 – Validate input before closing
    /*
     * Überschreibe Valid() in deiner TDialog-Unterklasse, um die Eingabe zu
     * prüfen bevor der Dialog geschlossen wird.
     * Override Valid() in your TDialog subclass to validate input
     * before the dialog closes.
     *
     * public override bool Valid(int command)
     * {
     *     if (command == cmOK)
     *     {
     *         var data = new MyDialogData();
     *         GetData(ref data);
     *
     *         if (string.IsNullOrWhiteSpace(data.Name))
     *         {
     *             // Fehlermeldung anzeigen / Show error message:
     *             // MessageBox.Error("Name darf nicht leer sein / Name must not be empty");
     *             return false;  // Dialog bleibt offen / dialog stays open
     *         }
     *     }
     *     return base.Valid(command);
     * }
     */
    #endregion
}
