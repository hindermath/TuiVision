// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 04: Ein TWindow auf dem Desktop öffnen.
/// Entspricht dem Beispiel <c>tvguid04</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 04: Opening a TWindow on the desktop.
/// Corresponds to the <c>tvguid04</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid04Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid04";

    /// <inheritdoc/>
    public int SequenceNumber => 4;

    /// <inheritdoc/>
    public string Title => "Ein TWindow öffnen / Opening a TWindow";

    /// <inheritdoc/>
    public string Description =>
        "Öffnet ein Fenster auf dem Desktop. / " +
        "Opens a window on the desktop.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid04App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 04.
///
/// Application class for tutorial step 04.
/// </summary>
internal sealed class TvGuid04App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid04App"/>-Klasse.
    /// Öffnet ein einfaches Fenster auf dem Desktop.
    ///
    /// Initializes a new instance of the <see cref="TvGuid04App"/> class.
    /// Opens a simple window on the desktop.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid04App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Einfaches Fenster erzeugen und einfügen / Create and insert a simple window
        TWindow window = new("Fenster / Window", 2, 2, 40, 10);
        Desktop?.Insert(window);
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

    #region Lösung Übung 1 – Fenster zentrieren / Solution Exercise 1 – Centre the window
    /*
     * Setze das Centered-Flag beim Erstellen des Fensters, damit TuiVision es
     * automatisch auf dem Desktop zentriert.
     * Set the Centered flag when creating the window so TuiVision centres it
     * automatically on the desktop.
     *
     * var window = new TWindow("Mein Fenster / My Window", 0, 0, 40, 10);
     * window.Options |= TViewOptions.Centered;
     * Desktop?.Insert(window);
     */
    #endregion

    #region Lösung Übung 2 – Benutzerdefinierten Fenstertitel setzen / Solution Exercise 2 – Set a custom window title
    /*
     * Übergib den gewünschten Titel direkt an den TWindow-Konstruktor.
     * Pass the desired title directly to the TWindow constructor.
     *
     * var window = new TWindow("Mein eigener Titel / My Custom Title", 5, 3, 40, 10);
     * Desktop?.Insert(window);
     *
     * // Oder ändere den Titel nach dem Erstellen / Or change the title after creation:
     * window.Title = "Neuer Titel / New Title";
     */
    #endregion
}
