// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Console;

/// <summary>
/// Kategorisiert historische Treiberfähigkeiten in überprüfbare Gruppen für die Phase-7-Konsolidierung.
///
/// Categorises historical driver capabilities into reviewable buckets for Phase-7 consolidation.
/// </summary>
public enum DriverCapabilityBucket
{
    /// <summary>
    /// Bildschirmausgabe, Rahmenzeichen, Cursorpositionierung.
    /// Screen output, frame characters, cursor positioning.
    /// </summary>
    ScreenPresentation,

    /// <summary>
    /// Tastatureingabe-Verarbeitung und Scancode-Übersetzung.
    /// Keyboard input processing and scan-code translation.
    /// </summary>
    KeyboardInput,

    /// <summary>
    /// Mausereignis-Verarbeitung und Koordinatenauflösung.
    /// Mouse event processing and coordinate resolution.
    /// </summary>
    MouseInput,

    /// <summary>
    /// Zeichensätze, Codepages, Schriften, Hardware-Farbkonfiguration.
    /// Character sets, codepages, fonts, hardware colour configuration.
    /// </summary>
    DisplayAdaptation,

    /// <summary>
    /// Terminalmodus-Einstellung (raw/cooked) und Teardown.
    /// Terminal mode setup (raw/cooked) and teardown.
    /// </summary>
    TerminalModeControl,
}

/// <summary>
/// Stellt den kanonischen Satz der Phase-7-Fähigkeitsgruppen bereit und beschreibt den
/// jeweils zugeordneten verwalteten Ersatz in <c>TuiVision.Drivers.Console</c>.
///
/// Provides the canonical set of Phase-7 capability buckets and describes the
/// corresponding managed replacement in <c>TuiVision.Drivers.Console</c>.
/// </summary>
public static class DriverCapabilityMap
{
    /// <summary>
    /// Alle definierten Fähigkeitsgruppen. Muss alle <see cref="DriverCapabilityBucket"/>-Werte enthalten.
    ///
    /// All defined capability buckets. Must contain every <see cref="DriverCapabilityBucket"/> value.
    /// </summary>
    public static IReadOnlyList<DriverCapabilityBucket> AllBuckets { get; } =
        Enum.GetValues<DriverCapabilityBucket>().ToList().AsReadOnly();

    /// <summary>
    /// Gibt die Beschreibung des verwalteten Ersatzes für die angegebene Fähigkeitsgruppe zurück.
    ///
    /// Returns the managed replacement description for the given capability bucket.
    /// </summary>
    /// <param name="bucket">Die Fähigkeitsgruppe. / The capability bucket.</param>
    /// <returns>
    /// Nicht-leere Beschreibung des verwalteten Ersatzes.
    /// Non-empty description of the managed replacement.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn kein Eintrag für den Bucket vorliegt.
    /// Thrown when no entry exists for the bucket.
    /// </exception>
    public static string GetManagedReplacement(DriverCapabilityBucket bucket) => bucket switch
    {
        DriverCapabilityBucket.ScreenPresentation =>
            "TConsoleDriver.Present() / TConsoleBuffer — manages the active back-buffer and publishes " +
            "immutable frame snapshots via IConsolePresenter. Replaces historical *dis.cc and *scr.cc " +
            "per-platform screen output using the managed System.Console API.",

        DriverCapabilityBucket.KeyboardInput =>
            "System.Console.ReadKey(intercept: true) via the managed .NET 10 runtime. " +
            "TuiVision.Compatibility.TConsoleInputAdapter and TKeyCodeTranslator convert the " +
            "managed console keys into Turbo-Vision-compatible key events. Replaces historical " +
            "*key.cc per-platform raw keyboard polling (BIOS INT 16h, Linux ioctl, " +
            "Win32 ReadConsoleInput).",

        DriverCapabilityBucket.MouseInput =>
            "ConsoleMouseIngress and TConsoleDriver provide a bounded managed SGR 1006 path for " +
            "interactive macOS/Linux terminals and WSL. Mouse payloads remain represented by " +
            "TuiVision.Core.TEvent and TMouseEvent. Native Windows Console, X10, wheel, hover, " +
            "touch, and the historical per-platform *mouse.cc matrix are not reproduced one-to-one; " +
            "they remain explicit unsupported or follow-up boundaries.",

        DriverCapabilityBucket.DisplayAdaptation =>
            "TerminalCharsetMapper provides host-independent Unicode and KOI8-R mapping with U+FFFD " +
            "replacement. BitmapFontFixture validates raw 8x16 metadata, and TerminalProfile applies " +
            "closed-schema safe defaults without installing host fonts or changing codepages. These " +
            "managed contracts replace the historical codepage.cc, fontcoll.cc, vga.cc, and vesa.cc " +
            "configuration intent without reproducing hardware-level mutation.",

        DriverCapabilityBucket.TerminalModeControl =>
            "TerminalSession provides a bounded in-process transcript, cursor, attributes, history, " +
            "reset, and lifecycle contract. TProgram retains managed System.Console cleanup for the " +
            "visible application shell. No shell, PTY, raw/cooked termios, host font, codepage, or " +
            "terminal-profile mutation is part of the session contract.",

        _ => throw new ArgumentOutOfRangeException(nameof(bucket), bucket, "No managed replacement defined for this bucket."),
    };

    /// <summary>
    /// Gibt an, welche historischen plattformspezifischen Quelldatei-Familien zu dieser
    /// Fähigkeitsgruppe gehören.
    ///
    /// Returns which historical platform-specific source file families belong to this capability bucket.
    /// </summary>
    /// <param name="bucket">Die Fähigkeitsgruppe. / The capability bucket.</param>
    /// <returns>
    /// Lesbare Aufzählung der historischen Quelldatei-Muster.
    /// Human-readable enumeration of historical source file patterns.
    /// </returns>
    public static string GetHistoricalSourcePattern(DriverCapabilityBucket bucket) => bucket switch
    {
        DriverCapabilityBucket.ScreenPresentation =>
            "*dis.cc, *scr.cc (dos, linux, unix, win32, winnt, qnx4, qnxrtp, wingr, x11), " +
            "tdisplay.cc, tscreen.cc",

        DriverCapabilityBucket.KeyboardInput =>
            "*key.cc (dos, linux, unix, win32, winnt, qnx4, qnxrtp, wingr, x11)",

        DriverCapabilityBucket.MouseInput =>
            "*mouse.cc (dos, linux, unix, win32, winnt, qnx4, qnxrtp, wingr, x11), " +
            "tmouse.cc, thwmouse.cc",

        DriverCapabilityBucket.DisplayAdaptation =>
            "codepage.cc, fontcoll.cc, dos/vga.cc, dos/vesa.cc",

        DriverCapabilityBucket.TerminalModeControl =>
            "dos/screen.cc, dos/sescreen.cc, dos/rhscreen.cc",

        _ => throw new ArgumentOutOfRangeException(nameof(bucket), bucket, "No source pattern defined for this bucket."),
    };
}
