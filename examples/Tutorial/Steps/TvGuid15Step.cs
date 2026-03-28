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

    #region Lösung Übung 1 – Zustand auch bei Abbrechen speichern / Solution Exercise 1 – Save state even on Cancel
    /*
     * Standardmäßig werden Dialogdaten nur bei cmOK übernommen. Speichere sie
     * zusätzlich bei cmCancel, indem du getData() vor dem Schließen aufrufst.
     * By default dialog data is only accepted on cmOK. Save it also on cmCancel
     * by calling getData() before closing.
     *
     * // Datenstruktur vor ExecView befüllen / Fill data structure before ExecView:
     * var data = new MyDialogData { Name = "Standard / Default" };
     * dialog.SetData(ref data);
     *
     * int result = ExecView(dialog);
     *
     * // Daten immer auslesen, unabhängig vom Ergebnis:
     * // Always read data regardless of result:
     * dialog.GetData(ref data);
     * _savedData = data;
     */
    #endregion

    #region Lösung Übung 2 – Gespeicherten Zustand im Hauptfenster anzeigen / Solution Exercise 2 – Display saved state in the main window
    /*
     * Zeige den zuletzt gespeicherten Dialogzustand als Fenstertitel oder
     * als Text im Desktop an.
     * Display the last saved dialog state as a window title or
     * as text on the desktop.
     *
     * // Nach ExecView() und GetData():
     * // After ExecView() and GetData():
     * string summary = $"Name: {_savedData.Name}";
     * var resultWindow = new TWindow(summary, 2, 2, 50, 6);
     * Desktop?.Insert(resultWindow);
     *
     * // Oder: Fenstertitel aktualisieren / Or: update window title:
     * myInfoWindow.Title = summary;
     * myInfoWindow.DrawView();
     */
    #endregion
}
