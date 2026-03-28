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
}
