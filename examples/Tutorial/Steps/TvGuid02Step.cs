// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 02: Menüleiste mit Untermenüs und Menüpunkten.
/// Entspricht dem Beispiel <c>tvguid02</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 02: Menu bar with submenus and menu items.
/// Corresponds to the <c>tvguid02</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid02Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid02";

    /// <inheritdoc/>
    public int SequenceNumber => 2;

    /// <inheritdoc/>
    public string Title => "Menüleiste mit Untermenüs / Menu bar with submenus";

    /// <inheritdoc/>
    public string Description =>
        "Fügt eine Menüleiste mit Untermenüs und Menüpunkten hinzu. / " +
        "Adds a menu bar with submenus and menu items.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid02App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 02.
///
/// Application class for tutorial step 02.
/// </summary>
internal sealed class TvGuid02App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid02App"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TvGuid02App"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid02App(TRect bounds, bool headless = false) : base(bounds)
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

    #region Lösung Übung 1 – Zweiten Untermenüeintrag hinzufügen / Solution Exercise 1 – Add a second submenu item
    /*
     * Überschreibe InitMenuBar() in TvGuid02App und füge einen zweiten Untermenüeintrag hinzu.
     * Override InitMenuBar() in TvGuid02App and add a second submenu item.
     *
     * protected override TMenuBar InitMenuBar(TRect bounds)
     * {
     *     return new TMenuBar(bounds,
     *         new TSubMenu("~D~atei / ~F~ile", 0x2100,
     *             new TMenuItem("~N~eu / ~N~ew",   cmNew,  kbNoKey) +
     *             new TMenuItem("~Ö~ffnen / ~O~pen", cmOpen, kbNoKey) +   // ← zweiter Eintrag / second item
     *             new TMenuItemDivider() +
     *             new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit, kbAltX)));
     * }
     *
     * private const int cmNew  = 101;
     * private const int cmOpen = 102;
     */
    #endregion

    #region Lösung Übung 2 – Alt-F10 testen / Solution Exercise 2 – Test Alt-F10
    /*
     * Alt-F10 ist eine TuiVision-Standardtastenkombination, um die Menüleiste zu fokussieren.
     * Du musst dafür keinen Code schreiben — starte die Anwendung und drücke Alt-F10.
     * Alt-F10 is a built-in TuiVision shortcut to focus the menu bar.
     * No code change is needed — start the application and press Alt-F10.
     *
     * // Erwartetes Verhalten / Expected behaviour:
     * // Die Menüleiste erhält den Fokus und der erste Menüpunkt wird hervorgehoben.
     * // The menu bar receives focus and the first menu item is highlighted.
     */
    #endregion
}
