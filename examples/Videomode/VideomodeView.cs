// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Videomode;

/// <summary>
/// Fenster, das das Ergebnis eines Videomode-Übergangsversuchs anzeigt.
/// Speichert das zuletzt angezeigte Ergebnis für Smoke-Test-Abfragen.
///
/// Window that displays the result of a video mode transition attempt.
/// Stores the last shown outcome for smoke-test queries.
/// </summary>
public class VideomodeView : TWindow
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="VideomodeView"/>-Klasse
    /// mit dem Titel „Videomode".
    ///
    /// Initializes a new instance of the <see cref="VideomodeView"/> class
    /// with the title "Videomode".
    /// </summary>
    /// <param name="bounds">Die Grenzen des Fensters. / The bounds of the window.</param>
    public VideomodeView(TRect bounds)
        : base("Videomode", bounds.A.X, bounds.A.Y, bounds.Width, bounds.Height)
    {
    }

    /// <summary>
    /// Das zuletzt angezeigte Übergangsergebnis, oder <c>null</c>, wenn noch keines angezeigt wurde.
    ///
    /// The last shown transition outcome, or <c>null</c> if none has been shown yet.
    /// </summary>
    public DisplayModeOutcome? LastShownOutcome { get; private set; }

    /// <summary>
    /// Die zuletzt angezeigte Übergangsmeldung, oder <c>null</c>, wenn noch keine angezeigt wurde.
    ///
    /// The last shown transition message, or <c>null</c> if none has been shown yet.
    /// </summary>
    public string? LastShownMessage { get; private set; }

    /// <summary>
    /// Zeigt das Ergebnis des Videomode-Übergangs an und speichert es für Abfragen.
    ///
    /// Displays the result of the video mode transition and stores it for queries.
    /// </summary>
    /// <param name="outcome">Das Ergebnis des Übergangsversuchs. / The outcome of the transition attempt.</param>
    /// <param name="message">Die zum Ergebnis gehörende Meldung. / The message associated with the outcome.</param>
    public void ShowOutcome(DisplayModeOutcome outcome, string message)
    {
        // Ergebnis für spätere Abfragen zwischenspeichern / Cache outcome for later queries
        LastShownOutcome = outcome;
        LastShownMessage = message;

        // Ansicht neu zeichnen, um das Ergebnis sichtbar zu machen / Redraw view to make outcome visible
        DrawView();
    }

    /// <summary>
    /// Zeichnet den Fensterrahmen und zeigt die zuletzt gespeicherte Übergangsmeldung an.
    ///
    /// Draws the window frame and displays the last stored transition message.
    /// </summary>
    public override void Draw()
    {
        // Rahmen zeichnen / Draw border
        base.Draw();

        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer == null || buffer.Width < 3 || buffer.Height < 3 || LastShownMessage == null)
        {
            return;
        }

        int innerWidth = buffer.Width - 2;
        ReadOnlySpan<char> text = LastShownMessage.AsSpan(0, Math.Min(LastShownMessage.Length, innerWidth));
        buffer.WriteText(1, 1, text, ConsoleColor.White, ConsoleColor.DarkBlue);
    }
}
