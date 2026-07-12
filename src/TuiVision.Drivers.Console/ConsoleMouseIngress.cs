// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Globalization;
using TuiVision.Core;

namespace TuiVision.Drivers.Console;

/// <summary>
/// Beschreibt den Aktivierungszustand des Konsolen-Mauseingangs.
///
/// Describes the activation state of the console mouse ingress.
/// </summary>
public enum ConsoleMouseCapabilityState
{
    /// <summary>Der Host unterstützt den Vertrag, er ist aber deaktiviert. / The host supports the contract, but it is disabled.</summary>
    Disabled,

    /// <summary>Der Vertrag ist unterstützt und aktiviert. / The contract is supported and enabled.</summary>
    Enabled,

    /// <summary>Der Host bietet keinen belegten Vertrag. / The host has no proven contract.</summary>
    Unsupported
}

/// <summary>
/// Benennt die für Maussupport relevanten Hostfamilien.
///
/// Names the host families relevant to mouse support.
/// </summary>
public enum ConsoleMouseHostFamily
{
    /// <summary>Unbekannter Host. / Unknown host.</summary>
    Unknown,

    /// <summary>macOS-Terminal. / macOS terminal.</summary>
    MacOS,

    /// <summary>Linux-Terminal. / Linux terminal.</summary>
    Linux,

    /// <summary>Windows Subsystem for Linux. / Windows Subsystem for Linux.</summary>
    Wsl,

    /// <summary>Native Windows-Konsole. / Native Windows console.</summary>
    WindowsConsole,

    /// <summary>Umgeleitete oder nicht interaktive Ein-/Ausgabe. / Redirected or non-interactive input/output.</summary>
    Headless
}

/// <summary>
/// Benennt den aktivierten Terminal-Mausprotokollvertrag.
///
/// Names the enabled terminal mouse protocol contract.
/// </summary>
public enum ConsoleMouseProtocol
{
    /// <summary>Kein Protokoll. / No protocol.</summary>
    None,

    /// <summary>SGR-1006-Zellkoordinaten. / SGR 1006 cell coordinates.</summary>
    Sgr1006
}

/// <summary>
/// Benennt den Grund, aus dem eine Host-Beobachtung nicht veröffentlicht wurde.
///
/// Names the reason why a host observation was not published.
/// </summary>
public enum ConsoleMouseRejectionReason
{
    /// <summary>Keine Ablehnung. / No rejection.</summary>
    None,

    /// <summary>Capability ist deaktiviert oder nicht unterstützt. / The capability is disabled or unsupported.</summary>
    CapabilityUnavailable,

    /// <summary>Die Syntax ist unvollständig oder unzulässig. / The syntax is incomplete or invalid.</summary>
    InvalidSyntax,

    /// <summary>Die Sequenz überschreitet die feste Grenze. / The sequence exceeds the fixed limit.</summary>
    SequenceTooLong,

    /// <summary>Ein Zahlenfeld ist ungültig. / A numeric field is invalid.</summary>
    InvalidNumber,

    /// <summary>Koordinaten liegen außerhalb des aktuellen Puffers. / Coordinates lie outside the current buffer.</summary>
    CoordinatesOutOfRange,

    /// <summary>Button oder Interaktion gehören nicht zum Feature-Vertrag. / The button or interaction is outside the feature contract.</summary>
    UnsupportedButton,

    /// <summary>Die Phase passt nicht zum aktuellen Press-Zustand. / The phase does not match the current press state.</summary>
    InvalidTransition
}

/// <summary>
/// Beschreibt Host, Protokoll und Begründung eines Mauseingangs-Zustands.
///
/// Describes the host, protocol, and rationale of a mouse ingress state.
/// </summary>
/// <param name="State">Aktivierungszustand. / Activation state.</param>
/// <param name="HostFamily">Hostfamilie. / Host family.</param>
/// <param name="Protocol">Protokollvertrag. / Protocol contract.</param>
/// <param name="Reason">Textorientierte Begründung. / Text-first rationale.</param>
public readonly record struct ConsoleMouseCapability(
    ConsoleMouseCapabilityState State,
    ConsoleMouseHostFamily HostFamily,
    ConsoleMouseProtocol Protocol,
    string Reason);

/// <summary>
/// Klassifiziert den aktuellen oder einen kontrolliert beschriebenen Host für
/// den begrenzten SGR-1006-Vertrag.
///
/// Classifies the current host or a controlled host description for the bounded
/// SGR 1006 contract.
/// </summary>
public static class ConsoleMouseCapabilityDetector
{
    /// <summary>
    /// Erkennt den aktuellen Prozess- und Terminalzustand.
    ///
    /// Detects the current process and terminal state.
    /// </summary>
    /// <returns>Erkannte Capability. / Detected capability.</returns>
    public static ConsoleMouseCapability DetectCurrent()
    {
        bool isWsl = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WSL_DISTRO_NAME"));
        return Detect(
            System.Console.IsInputRedirected,
            System.Console.IsOutputRedirected,
            Environment.GetEnvironmentVariable("TERM"),
            OperatingSystem.IsWindows(),
            isWsl,
            OperatingSystem.IsMacOS(),
            OperatingSystem.IsLinux());
    }

    /// <summary>
    /// Klassifiziert kontrollierte Hostmerkmale für deterministische Tests und
    /// die echte Laufzeiterkennung.
    ///
    /// Classifies controlled host traits for deterministic tests and real runtime detection.
    /// </summary>
    /// <param name="inputRedirected">Eingabe ist umgeleitet. / Input is redirected.</param>
    /// <param name="outputRedirected">Ausgabe ist umgeleitet. / Output is redirected.</param>
    /// <param name="term">Wert der `TERM`-Variable. / Value of the `TERM` variable.</param>
    /// <param name="isWindows">Windows-Laufzeit. / Windows runtime.</param>
    /// <param name="isWsl">WSL-Umgebung. / WSL environment.</param>
    /// <param name="isMacOS">macOS-Laufzeit. / macOS runtime.</param>
    /// <param name="isLinux">Linux-Laufzeit. / Linux runtime.</param>
    /// <returns>Klassifizierte Capability. / Classified capability.</returns>
    public static ConsoleMouseCapability Detect(
        bool inputRedirected,
        bool outputRedirected,
        string? term,
        bool isWindows,
        bool isWsl,
        bool isMacOS,
        bool isLinux)
    {
        if (inputRedirected || outputRedirected)
        {
            return new ConsoleMouseCapability(
                ConsoleMouseCapabilityState.Unsupported,
                ConsoleMouseHostFamily.Headless,
                ConsoleMouseProtocol.None,
                "Redirected or non-interactive input/output does not provide the SGR mouse contract.");
        }

        ConsoleMouseHostFamily host = isWsl
            ? ConsoleMouseHostFamily.Wsl
            : isMacOS
                ? ConsoleMouseHostFamily.MacOS
                : isLinux
                    ? ConsoleMouseHostFamily.Linux
                    : isWindows
                        ? ConsoleMouseHostFamily.WindowsConsole
                        : ConsoleMouseHostFamily.Unknown;

        if (host == ConsoleMouseHostFamily.WindowsConsole)
        {
            return new ConsoleMouseCapability(
                ConsoleMouseCapabilityState.Unsupported,
                host,
                ConsoleMouseProtocol.None,
                "Native Windows Console mouse records require a separate backend contract.");
        }

        if (string.IsNullOrWhiteSpace(term) || string.Equals(term, "dumb", StringComparison.OrdinalIgnoreCase))
        {
            return new ConsoleMouseCapability(
                ConsoleMouseCapabilityState.Unsupported,
                host,
                ConsoleMouseProtocol.None,
                "The terminal does not advertise a usable interactive protocol.");
        }

        if (host is ConsoleMouseHostFamily.MacOS or ConsoleMouseHostFamily.Linux or ConsoleMouseHostFamily.Wsl)
        {
            return new ConsoleMouseCapability(
                ConsoleMouseCapabilityState.Disabled,
                host,
                ConsoleMouseProtocol.Sgr1006,
                "SGR 1006 is available and must be enabled explicitly by the runtime lifecycle.");
        }

        return new ConsoleMouseCapability(
            ConsoleMouseCapabilityState.Unsupported,
            ConsoleMouseHostFamily.Unknown,
            ConsoleMouseProtocol.None,
            "The current host family has no proven mouse ingress contract.");
    }
}

/// <summary>
/// Wandelt vollständige, begrenzte SGR-1006-Beobachtungen in kanonische
/// <see cref="TEvent"/>-Instanzen um.
///
/// Converts complete, bounded SGR 1006 observations into canonical
/// <see cref="TEvent"/> instances.
/// </summary>
public sealed class ConsoleMouseIngress
{
    /// <summary>Maximal akzeptierte Sequenzlänge. / Maximum accepted sequence length.</summary>
    public const int MaximumSequenceLength = 64;

    /// <summary>Inklusive Doppelklickgrenze in Millisekunden. / Inclusive double-click limit in milliseconds.</summary>
    public const long DoubleClickMilliseconds = 500;

    private readonly object _sync = new();
    private bool _leftPressed;
    private long? _lastClickTimestamp;
    private TPoint? _lastClickPosition;
    private string? _lastClickTarget;

    /// <summary>
    /// Erstellt einen Eingang mit dem angegebenen Capability-Zustand.
    ///
    /// Creates an ingress with the specified capability state.
    /// </summary>
    /// <param name="capability">Anfänglicher Capability-Zustand. / Initial capability state.</param>
    public ConsoleMouseIngress(ConsoleMouseCapability capability)
    {
        ValidateCapability(capability);
        Capability = capability;
    }

    /// <summary>
    /// Liefert den aktuellen Capability-Zustand.
    ///
    /// Gets the current capability state.
    /// </summary>
    public ConsoleMouseCapability Capability { get; private set; }

    /// <summary>
    /// Setzt den Capability-Zustand und verwirft jeden transienten Press- oder
    /// Klickzustand, damit Reaktivierung keine alte Interaktion fortsetzt.
    ///
    /// Sets the capability state and discards transient press or click state so
    /// reactivation cannot continue an old interaction.
    /// </summary>
    /// <param name="capability">Neuer Zustand. / New state.</param>
    public void SetCapability(ConsoleMouseCapability capability)
    {
        ValidateCapability(capability);
        lock (_sync)
        {
            Capability = capability;
            ResetTransientState();
        }
    }

    /// <summary>
    /// Prüft eine vollständige Host-Beobachtung und veröffentlicht bei Erfolg
    /// genau ein kanonisches Mausereignis.
    ///
    /// Validates one complete host observation and publishes exactly one
    /// canonical mouse event on success.
    /// </summary>
    /// <param name="sequence">Vollständige SGR-1006-Sequenz. / Complete SGR 1006 sequence.</param>
    /// <param name="timestampMilliseconds">Monotone Zeit in Millisekunden. / Monotonic time in milliseconds.</param>
    /// <param name="bufferWidth">Aktuelle Pufferbreite. / Current buffer width.</param>
    /// <param name="bufferHeight">Aktuelle Pufferhöhe. / Current buffer height.</param>
    /// <param name="targetResolver">Optionaler Zielschlüssel-Auflöser ohne Controls-Abhängigkeit. / Optional target-key resolver without a Controls dependency.</param>
    /// <param name="event">Kanonisches Ereignis oder `Nothing`. / Canonical event or `Nothing`.</param>
    /// <param name="rejectionReason">Ablehnungsgrund oder `None`. / Rejection reason or `None`.</param>
    /// <returns><c>true</c> genau dann, wenn ein Ereignis veröffentlicht wurde. / <c>true</c> exactly when an event was published.</returns>
    public bool TryAccept(
        string sequence,
        long timestampMilliseconds,
        int bufferWidth,
        int bufferHeight,
        Func<TPoint, string?>? targetResolver,
        out TEvent @event,
        out ConsoleMouseRejectionReason rejectionReason)
    {
        @event = TEvent.CreateNone();
        rejectionReason = ConsoleMouseRejectionReason.None;

        lock (_sync)
        {
            if (Capability.State != ConsoleMouseCapabilityState.Enabled
                || Capability.Protocol != ConsoleMouseProtocol.Sgr1006)
            {
                rejectionReason = ConsoleMouseRejectionReason.CapabilityUnavailable;
                return false;
            }

            if (sequence is null || sequence.Length == 0)
            {
                rejectionReason = ConsoleMouseRejectionReason.InvalidSyntax;
                return false;
            }

            if (sequence.Length > MaximumSequenceLength)
            {
                rejectionReason = ConsoleMouseRejectionReason.SequenceTooLong;
                return false;
            }

            if (!TryParse(sequence.AsSpan(), out int buttonCode, out int hostX, out int hostY, out bool release, out rejectionReason))
            {
                return false;
            }

            if (bufferWidth <= 0 || bufferHeight <= 0
                || hostX < 1 || hostY < 1 || hostX > bufferWidth || hostY > bufferHeight)
            {
                rejectionReason = ConsoleMouseRejectionReason.CoordinatesOutOfRange;
                return false;
            }

            TPoint where = new((short)(hostX - 1), (short)(hostY - 1));
            if (release)
            {
                if (!_leftPressed || (buttonCode != 0 && buttonCode != 3))
                {
                    rejectionReason = buttonCode is 0 or 3
                        ? ConsoleMouseRejectionReason.InvalidTransition
                        : ConsoleMouseRejectionReason.UnsupportedButton;
                    return false;
                }

                _leftPressed = false;
                @event = TEvent.CreateMouse(TEventKind.MouseUp, TMouseButtons.None, false, where);
                return true;
            }

            if (buttonCode == 32)
            {
                if (!_leftPressed)
                {
                    rejectionReason = ConsoleMouseRejectionReason.InvalidTransition;
                    return false;
                }

                @event = TEvent.CreateMouse(TEventKind.MouseMove, TMouseButtons.Left, false, where);
                return true;
            }

            if (buttonCode != 0)
            {
                rejectionReason = ConsoleMouseRejectionReason.UnsupportedButton;
                return false;
            }

            if (_leftPressed)
            {
                rejectionReason = ConsoleMouseRejectionReason.InvalidTransition;
                return false;
            }

            string? target = targetResolver?.Invoke(where);
            bool doubleClick = IsDoubleClick(timestampMilliseconds, where, target);
            _leftPressed = true;
            _lastClickTimestamp = timestampMilliseconds;
            _lastClickPosition = where;
            _lastClickTarget = target;
            @event = TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, doubleClick, where);
            return true;
        }
    }

    private static bool TryParse(
        ReadOnlySpan<char> sequence,
        out int buttonCode,
        out int x,
        out int y,
        out bool release,
        out ConsoleMouseRejectionReason rejectionReason)
    {
        buttonCode = 0;
        x = 0;
        y = 0;
        release = false;
        rejectionReason = ConsoleMouseRejectionReason.InvalidSyntax;

        if (sequence.Length < 9 || !sequence.StartsWith("\x1b[<".AsSpan()))
        {
            return false;
        }

        char terminator = sequence[^1];
        if (terminator != 'M' && terminator != 'm')
        {
            return false;
        }

        ReadOnlySpan<char> body = sequence[3..^1];
        int firstSeparator = body.IndexOf(';');
        int secondSeparator = firstSeparator < 0 ? -1 : body[(firstSeparator + 1)..].IndexOf(';');
        if (firstSeparator <= 0 || secondSeparator < 1)
        {
            return false;
        }

        secondSeparator += firstSeparator + 1;
        if (body[(secondSeparator + 1)..].IndexOf(';') >= 0)
        {
            return false;
        }

        if (!TryParseNumber(body[..firstSeparator], out buttonCode)
            || !TryParseNumber(body[(firstSeparator + 1)..secondSeparator], out x)
            || !TryParseNumber(body[(secondSeparator + 1)..], out y))
        {
            rejectionReason = ConsoleMouseRejectionReason.InvalidNumber;
            return false;
        }

        release = terminator == 'm';
        rejectionReason = ConsoleMouseRejectionReason.None;
        return true;
    }

    private static bool TryParseNumber(ReadOnlySpan<char> value, out int result)
    {
        result = 0;
        return value.Length is > 0 and <= 9
            && int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out result);
    }

    private bool IsDoubleClick(long timestampMilliseconds, TPoint where, string? target)
    {
        // Eine rückläufige Zeitquelle beendet die Vergleichskette, statt alte Klicks fälschlich zusammenzuführen.
        // A regressing time source ends the comparison chain instead of combining stale clicks incorrectly.
        if (_lastClickTimestamp is long previous && timestampMilliseconds < previous)
        {
            _lastClickTimestamp = null;
            _lastClickPosition = null;
            _lastClickTarget = null;
            return false;
        }

        return target is not null
            && _lastClickTarget is not null
            && _lastClickTimestamp is long prior
            && timestampMilliseconds - prior <= DoubleClickMilliseconds
            && _lastClickPosition == where
            && string.Equals(_lastClickTarget, target, StringComparison.Ordinal);
    }

    private void ResetTransientState()
    {
        _leftPressed = false;
        _lastClickTimestamp = null;
        _lastClickPosition = null;
        _lastClickTarget = null;
    }

    private static void ValidateCapability(ConsoleMouseCapability capability)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(capability.Reason);
        if (capability.State == ConsoleMouseCapabilityState.Enabled
            && capability.Protocol == ConsoleMouseProtocol.None)
        {
            throw new ArgumentException("An enabled mouse capability requires a protocol.", nameof(capability));
        }
    }
}

/// <summary>
/// Eine kontrollierte vollständige Host-Beobachtung für die Driver-Queue.
///
/// A controlled complete host observation for the Driver queue.
/// </summary>
/// <param name="Sequence">SGR-1006-Sequenz. / SGR 1006 sequence.</param>
/// <param name="TimestampMilliseconds">Monotone Zeit. / Monotonic time.</param>
public readonly record struct ConsoleMouseObservation(string Sequence, long TimestampMilliseconds);
