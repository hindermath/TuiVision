// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Tutorial-Schritt 05: Benutzerdefinierten Text in ein Fenster zeichnen.
/// Entspricht dem Beispiel <c>tvguid05</c> aus dem Turbo-Vision-2.0.3-Quelltext.
///
/// Tutorial step 05: Drawing custom text into a window.
/// Corresponds to the <c>tvguid05</c> example from the Turbo Vision 2.0.3 source.
/// </summary>
public sealed class TvGuid05Step : ITutorialStep
{
    /// <inheritdoc/>
    public string Token => "tvguid05";

    /// <inheritdoc/>
    public int SequenceNumber => 5;

    /// <inheritdoc/>
    public string Title => "Inhalt in ein Fenster zeichnen / Drawing content into a window";

    /// <inheritdoc/>
    public string Description =>
        "Zeichnet benutzerdefinierten Text in ein Fenster. / " +
        "Draws custom text into a window.";

    /// <inheritdoc/>
    public TApplication CreateApp(TRect bounds, bool headless) => new TvGuid05App(bounds, headless);
}

/// <summary>
/// Anwendungsklasse für Tutorial-Schritt 05.
///
/// Application class for tutorial step 05.
/// </summary>
internal sealed class TvGuid05App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TvGuid05App"/>-Klasse.
    /// Fügt ein Fenster mit benutzerdefiniertem Zeicheninhalt ein.
    ///
    /// Initializes a new instance of the <see cref="TvGuid05App"/> class.
    /// Inserts a window with custom drawing content.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public TvGuid05App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        // Fenster mit benutzerdefiniertem Inhalt einfügen / Insert window with custom content
        ContentWindow window = new("Zeicheninhalt / Drawing Content", 2, 2, 40, 10);
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

    // Hilfsfenster mit überschriebener Draw-Methode / Helper window with overridden Draw method
    private sealed class ContentWindow : TWindow
    {
        /// <summary>
        /// Initialisiert das Inhaltsfenster.
        ///
        /// Initializes the content window.
        /// </summary>
        public ContentWindow(string title, int x, int y, int width, int height)
            : base(title, x, y, width, height)
        {
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            // Basis-Draw ausführen, dann eigene Ausgabe hinzufügen
            // Execute base draw, then add custom output
            base.Draw();
        }
    }

    #region Lösung Übung 1 – Mehrere Textzeilen zeichnen / Solution Exercise 1 – Draw multiple text lines
    /*
     * Überschreibe Draw() in deiner TWindow-Unterklasse und zeichne mehrere Zeilen
     * mit WriteText() oder WriteChar().
     * Override Draw() in your TWindow subclass and draw multiple lines
     * using WriteText() or WriteChar().
     *
     * public override void Draw()
     * {
     *     base.Draw();
     *     WriteText(1, 1, "Zeile 1 / Line 1");
     *     WriteText(1, 2, "Zeile 2 / Line 2");
     *     WriteText(1, 3, "Zeile 3 / Line 3");
     * }
     */
    #endregion

    #region Lösung Übung 2 – Farben für Vordergrund und Hintergrund / Solution Exercise 2 – Foreground and background colours
    /*
     * Erstelle ein TColorAttr mit der gewünschten Farb-Kombination und übergib es
     * an WriteText() oder WriteChar().
     * Create a TColorAttr with the desired colour combination and pass it
     * to WriteText() or WriteChar().
     *
     * // Weißer Text auf blauem Hintergrund / White text on blue background:
     * var attr = new TColorAttr(TColor.White, TColor.Blue);
     * WriteText(1, 1, "Farbiger Text / Coloured text", attr);
     *
     * // Gelber Text auf rotem Hintergrund / Yellow text on red background:
     * var attrError = new TColorAttr(TColor.Yellow, TColor.Red);
     * WriteText(1, 2, "Fehler! / Error!", attrError);
     */
    #endregion
}
