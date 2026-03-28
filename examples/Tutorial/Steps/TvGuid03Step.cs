// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 03: Menübefehl-Verarbeitung.
/// Entspricht dem Beispiel <c>tvguid03</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 03: Menu command handling.
/// Corresponds to the <c>tvguid03</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid03Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid03";

    /// <inheritdoc/>
    public int SequenceNumber => 3;

    /// <inheritdoc/>
    public string Title => "Menübefehl-Verarbeitung / Menu command handling";

    /// <inheritdoc/>
    public string Description =>
        "Verarbeitet Befehle aus der Menüleiste. / " +
        "Handles commands from the menu bar.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid03App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 03.
///
/// Application class for tutorial step 03.
/// </summary>
internal sealed class TvGuid03App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid03App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid03App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid03App(TRect bounds, bool headless = false) : base(bounds)
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

    #region Lösung Übung 1 – „Über dieses Programm"-Menüpunkt / Solution Exercise 1 – "About" menu item
    /*
     * Füge einen „Über dieses Programm / About"-Menüpunkt hinzu und zeige beim Klick
     * einen Informationsdialog an.
     * Add an "About" menu item and show an information dialog when clicked.
     *
     * private const int cmAbout = 200;
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds,
     *         new TSubMenu("~H~ilfe / ~H~elp", 0x2300,
     *             new TMenuItem("~Ü~ber / ~A~bout", cmAbout, kbNoKey) +
     *             new TMenuItemDivider() +
     *             new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit, kbAltX)));
     * }
     *
     * public override void HandleEvent(TEvent @event)
     * {
     *     if (@event.What == TEventKind.Command && @event.Message.Command == cmAbout)
     *     {
     *         // Hier einen modalen Dialog anzeigen / Show a modal dialog here
     *         // MessageBox.Show("TuiVision Tutorial – Schritt 03", "OK");
     *         @event.Clear();
     *         return;
     *     }
     *     base.HandleEvent(@event);
     * }
     */
    #endregion

    #region Lösung Übung 2 – Menüpunkt dynamisch deaktivieren / Solution Exercise 2 – Disable a menu item dynamically
    /*
     * Rufe DisableCommand() auf, um einen Befehl zu deaktivieren, und EnableCommand()
     * um ihn wieder zu aktivieren. TuiVision graut den Menüpunkt automatisch aus.
     * Call DisableCommand() to disable a command and EnableCommand() to re-enable it.
     * TuiVision automatically grays out the menu item.
     *
     * // Befehl deaktivieren / Disable command:
     * DisableCommand(cmAbout);
     *
     * // Befehl wieder aktivieren / Re-enable command:
     * EnableCommand(cmAbout);
     *
     * // Tipp: Rufe DisableCommand() z. B. beim Start auf, um den Effekt sofort zu sehen.
     * // Tip: call DisableCommand() e.g. in the constructor to see the effect immediately.
     */
    #endregion
}
