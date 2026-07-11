// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Drivers.Console;

/// <summary>
/// Stellt einen <see cref="TConsoleBuffer"/>-Frame auf der echten <see cref="System.Console"/> dar.
/// Alle Konsolen-API-Aufrufe sind von <c>try/catch</c>-Blöcken umgeben, damit der Presenter
/// in Umgebungen ohne Terminal (z. B. CI, umgeleitete E/A, Smoke-Tests) stumm bleibt.
///
/// Presents a <see cref="TConsoleBuffer"/> frame on the real <see cref="System.Console"/>.
/// All console API calls are wrapped in <c>try/catch</c> blocks so the presenter
/// stays silent in environments without a terminal (e.g. CI, redirected I/O, smoke tests).
/// </summary>
public sealed class SystemConsolePresenter : IConsolePresenter
{
    /// <summary>
    /// Schreibt jeden Puffer-Zelle einzeln in die Konsole.
    /// Die letzte Zelle der letzten Zeile wird übersprungen, um einen ungewollten Scroll zu verhindern.
    ///
    /// Writes every buffer cell individually to the console.
    /// The last cell of the last row is skipped to prevent an unwanted scroll.
    /// </summary>
    /// <param name="frame">
    /// Der darzustellende Frame als unveränderlicher Puffer-Schnappschuss.
    /// The frame to present as an immutable buffer snapshot.
    /// </param>
    public void Present(TConsoleBuffer frame)
    {
        try { System.Console.CursorVisible = false; } catch { }

        for (int y = 0; y < frame.Height; y++)
        {
            try { System.Console.SetCursorPosition(0, y); } catch { break; }

            for (int x = 0; x < frame.Width; x++)
            {
                // Manche Terminals scrollen beim Schreiben der letzten Zelle; Auslassen hält die Frame-Geometrie stabil.
                // Some terminals scroll when the last cell is written; skipping it keeps frame geometry stable.
                if (y == frame.Height - 1 && x == frame.Width - 1)
                {
                    break;
                }

                TConsoleCell cell = frame.GetCell(x, y);
                try
                {
                    System.Console.ForegroundColor = cell.Foreground;
                    System.Console.BackgroundColor = cell.Background;
                    System.Console.Write(cell.Glyph);
                }
                catch { break; }
            }
        }

        try { System.Console.ResetColor(); } catch { }
    }
}
