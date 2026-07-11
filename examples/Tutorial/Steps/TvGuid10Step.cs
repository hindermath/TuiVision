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
    public string Title => "Grenzen beim Ändern der Größe / Resize constraints";

    /// <inheritdoc/>
    public string Description =>
        "Zeigt minimale und maximale Grenzen beim Ändern einer Fenstergröße. / " +
        "Shows minimum and maximum limits while resizing a window.";

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

    #region Lösung Übung 1 – Schließen-Button im Dialog / Solution Exercise 1 – Close button in dialog
    /*
     * Füge dem Dialog einen TButton mit cmCancel oder cmOK hinzu, damit der
     * Benutzer ihn per Mausklick schließen kann.
     * Add a TButton with cmCancel or cmOK to the dialog so the user can
     * close it with a mouse click.
     *
     * var dialog = new TDialog(new TRect(10, 5, 50, 15), "Mein Dialog / My Dialog");
     * var btn = new TButton(new TRect(14, 7, 26, 9), "~O~K", cmOK, TButtonFlags.Default);
     * dialog.Insert(btn);
     * ExecView(dialog);
     */
    #endregion

    #region Lösung Übung 2 – Dialog über Menüpunkt öffnen / Solution Exercise 2 – Open dialog via menu item
    /*
     * Überschreibe InitMenuBar() und füge einen Menüpunkt hinzu, der den Dialog öffnet.
     * Override InitMenuBar() and add a menu item that opens the dialog.
     *
     * private const int cmOpenDialog = 400;
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds,
     *         new TSubMenu("~A~ktionen / ~A~ctions", 0x2400,
     *             new TMenuItem("~D~ialog öffnen / Open ~D~ialog", cmOpenDialog, kbNoKey) +
     *             new TMenuItemDivider() +
     *             new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit, kbAltX)));
     * }
     *
     * public override void HandleEvent(TEvent @event)
     * {
     *     if (@event.What == TEventKind.Command && @event.Message.Command == cmOpenDialog)
     *     {
     *         var dialog = new TDialog(new TRect(10, 5, 50, 15), "Dialog");
     *         ExecView(dialog);
     *         @event.Clear(); return;
     *     }
     *     base.HandleEvent(@event);
     * }
     */
    #endregion
}
