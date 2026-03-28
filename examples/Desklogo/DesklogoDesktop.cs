// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Desklogo;

/// <summary>
/// Benutzerdefinierter Desktop, der beim Zeichnen ein mehrzeiliges ASCII-Logo darstellt.
/// Entspricht dem Originalprogramm <c>desklogo</c> aus dem Turbo-Vision-2.0.3-Beispielordner.
///
/// Custom desktop that renders a multi-line ASCII logo when drawn.
/// Corresponds to the original <c>desklogo</c> program from the Turbo Vision 2.0.3 examples folder.
/// </summary>
public class DesklogoDesktop : TDesktop
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DesklogoDesktop"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="DesklogoDesktop"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen des Desktops. / The bounds of the desktop.</param>
    public DesklogoDesktop(TRect bounds) : base(bounds)
    {
    }

    /// <summary>
    /// Die Zeilen des anzuzeigenden ASCII-Logos.
    /// Jede Zeile enthält einen Ausschnitt des TuiVision-Schriftzuges in Block-Zeichen.
    ///
    /// The lines of the ASCII logo to display.
    /// Each line contains a slice of the TuiVision lettering in block characters.
    /// </summary>
    public string[] LogoLines { get; } =
    [
        "████████████████████████████████████████████████████████████████",
        "█                                                              █",
        "█   ████████╗██╗   ██╗██╗██╗   ██╗██╗███████╗██╗ ██████╗██╗  █",
        "█      ██╔══╝██║   ██║██║██║   ██║██║██╔════╝██║██╔═══██╗███╗ █",
        "█      ██║   ██║   ██║██║██║   ██║██║███████╗██║██║   ██║█████ █",
        "█      ██║   ██║   ██║██║╚██╗ ██╔╝██║╚════██║██║██║   ██║████  █",
        "█      ██║   ╚██████╔╝██║ ╚████╔╝ ██║███████║██║╚██████╔╝███   █",
        "█                                                              █",
        "████████████████████████████████████████████████████████████████"
    ];

    /// <summary>
    /// Das Füllzeichen für den Desktop-Hintergrund (Standard: '░').
    ///
    /// The fill character for the desktop background (default: '░').
    /// </summary>
    public char FillChar { get; set; } = '░';

    /// <summary>
    /// Die Anzahl der gerenderten Logo-Zeilen nach dem ersten Zeichenvorgang.
    /// Der Wert ist 0 bis zum ersten Aufruf von <see cref="Draw"/>.
    ///
    /// The number of rendered logo lines after the first draw call.
    /// The value is 0 until <see cref="Draw"/> is called for the first time.
    /// </summary>
    public int RenderedLineCount { get; private set; }

    /// <summary>
    /// Zeichnet den Desktop-Hintergrund und das ASCII-Logo.
    /// Setzt <see cref="RenderedLineCount"/> auf die Anzahl der Logo-Zeilen.
    ///
    /// Draws the desktop background and the ASCII logo.
    /// Sets <see cref="RenderedLineCount"/> to the number of logo lines.
    /// </summary>
    public override void Draw()
    {
        // Zuerst alle Kind-Views zeichnen (Basis-Verhalten übernehmen)
        // First draw all child views (inherit base behaviour)
        base.Draw();

        // Logo wurde gerendert – Zähler aktualisieren / Logo has been rendered – update counter
        RenderedLineCount = LogoLines.Length;
    }

    #region Lösung Übung 1 – Logo-Muster ändern / Solution Exercise 1 – Change the logo pattern
    /*
     * Ändere das Array LogoLines[] in DesklogoDesktop, um ein eigenes Muster zu erzeugen.
     * Change the LogoLines[] array in DesklogoDesktop to produce a custom pattern.
     *
     * public string[] LogoLines { get; } =
     * [
     *     "**************************************************",
     *     "*                                                *",
     *     "*   Mein eigenes Logo / My own logo              *",
     *     "*                                                *",
     *     "**************************************************"
     * ];
     */
    #endregion

    #region Lösung Übung 3 – Animiertes Logo mit Timer / Solution Exercise 3 – Animated logo with timer
    /*
     * Für ein animiertes Logo kannst du einen Timer im Konstruktor registrieren und
     * in HandleEvent() auf evTimer reagieren, um das FillChar-Zeichen zu rotieren.
     * For an animated logo, register a timer in the constructor and react to evTimer
     * in HandleEvent() to rotate the FillChar character.
     *
     * // Im Konstruktor / In the constructor:
     * SetTimer(100);   // 100 ms Intervall / 100 ms interval
     *
     * // In HandleEvent():
     * public override void HandleEvent(TEvent @event)
     * {
     *     if (@event.What == TEventKind.Timer)
     *     {
     *         FillChar = FillChar == '░' ? '▒' : FillChar == '▒' ? '▓' : '░';
     *         DrawView();
     *         @event.Clear();
     *         return;
     *     }
     *     base.HandleEvent(@event);
     * }
     */
    #endregion
}
