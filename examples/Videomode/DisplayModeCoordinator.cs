// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Runtime.InteropServices;

namespace TuiVision.Examples.Videomode;

/// <summary>
/// Mögliche Ergebnisse eines Bildschirmmodus-Übergangsversuchs.
///
/// Possible outcomes of a display mode transition attempt.
/// </summary>
public enum DisplayModeOutcome
{
    /// <summary>Noch kein Übergangsversuch wurde unternommen. / No transition attempt has been made yet.</summary>
    Unknown = 0,

    /// <summary>Der Übergang wurde erfolgreich auf der Konsole durchgeführt. / The transition was successfully applied to the console.</summary>
    RealTransition = 1,

    /// <summary>
    /// Kein echter Übergang möglich; ein sichtbarer Fallback wurde angewendet.
    ///
    /// No real transition possible; a visible fallback was applied.
    /// </summary>
    VisibleFallback = 2
}

/// <summary>
/// Erkennt die Größenänderungs-Fähigkeit des aktuellen Terminals und versucht,
/// die Konsolengröße auf einen angeforderten Modus umzuschalten.
///
/// Detects the resize capability of the current terminal and attempts to switch
/// the console size to a requested mode.
/// </summary>
public class DisplayModeCoordinator
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DisplayModeCoordinator"/>-Klasse
    /// und prüft die Terminal-Größenänderungs-Fähigkeit.
    ///
    /// Initializes a new instance of the <see cref="DisplayModeCoordinator"/> class
    /// and probes the terminal's resize capability.
    /// </summary>
    public DisplayModeCoordinator()
    {
        IsResizeSupported = ProbeResizeSupport();
    }

    /// <summary>
    /// Gibt an, ob das aktuelle Terminal Größenänderungen über die Konsolen-API unterstützt.
    ///
    /// Indicates whether the current terminal supports resizing via the console API.
    /// </summary>
    public bool IsResizeSupported { get; private set; }

    /// <summary>
    /// Das Ergebnis des zuletzt ausgeführten Übergangsversuchs.
    ///
    /// The outcome of the most recently executed transition attempt.
    /// </summary>
    public DisplayModeOutcome LastOutcome { get; private set; } = DisplayModeOutcome.Unknown;

    /// <summary>
    /// Die Fallback-Meldung, die angezeigt wird, wenn keine echte Größenänderung möglich ist.
    ///
    /// The fallback message displayed when no real resize is possible.
    /// </summary>
    public string FallbackMessage =>
        "Dieses Terminal unterstützt keine Größenänderung. / This terminal does not support resizing.";

    /// <summary>
    /// Versucht, die Konsolengröße auf die angegebenen Abmessungen umzuschalten.
    /// Gibt <see cref="DisplayModeOutcome.RealTransition"/> zurück, wenn die Änderung
    /// erfolgreich war, andernfalls <see cref="DisplayModeOutcome.VisibleFallback"/>.
    ///
    /// Attempts to switch the console size to the specified dimensions.
    /// Returns <see cref="DisplayModeOutcome.RealTransition"/> if the change succeeded,
    /// otherwise <see cref="DisplayModeOutcome.VisibleFallback"/>.
    /// </summary>
    /// <param name="width">Die gewünschte Terminalbreite in Zeichen. / The desired terminal width in characters.</param>
    /// <param name="height">Die gewünschte Terminalhöhe in Zeichen. / The desired terminal height in characters.</param>
    /// <returns>Das Ergebnis des Übergangsversuchs. / The outcome of the transition attempt.</returns>
    public DisplayModeOutcome TryTransition(int width, int height)
    {
        if (IsResizeSupported)
        {
            try
            {
                // Konsolengröße setzen / Set console size
#pragma warning disable CA1416 // Nur aufgerufen, wenn IsResizeSupported = true / Only called when IsResizeSupported = true
                Console.SetWindowSize(width, height);
#pragma warning restore CA1416
                LastOutcome = DisplayModeOutcome.RealTransition;
                return LastOutcome;
            }
            catch
            {
                // Größenänderung ist trotz Sondierung fehlgeschlagen / Resize failed despite probing
            }
        }

        // Fallback: echter Übergang nicht möglich / Fallback: real transition not possible
        LastOutcome = DisplayModeOutcome.VisibleFallback;
        return LastOutcome;
    }

    // Prüft, ob das Terminal Größenänderungen unterstützt.
    // Probes whether the terminal supports resizing.
    private static bool ProbeResizeSupport()
    {
        // Windows-Terminals unterstützen SetWindowSize in der Regel über die Win32-API
        // Windows terminals typically support SetWindowSize via the Win32 API
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                int currentWidth = Console.WindowWidth;

                // Kurzer Selbsttest: Größe auf denselben Wert setzen und prüfen, ob keine Exception geworfen wird
                // Quick self-test: set size to the same value and verify no exception is thrown
#pragma warning disable CA1416 // SetWindowSize wird unter Windows unterstützt / SetWindowSize is supported on Windows
                Console.SetWindowSize(currentWidth, Console.WindowHeight);
#pragma warning restore CA1416
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Auf macOS/Linux ist SetWindowSize in der Regel nicht zuverlässig — absichtlicher Probe-Aufruf
        // On macOS/Linux, SetWindowSize is generally not reliable — intentional probe call
        try
        {
            int currentWidth = Console.WindowWidth;
#pragma warning disable CA1416 // Absichtliche plattformübergreifende Probe / Intentional cross-platform probe
            Console.SetWindowSize(currentWidth, Console.WindowHeight);
#pragma warning restore CA1416
            return true;
        }
        catch
        {
            return false;
        }
    }
}
