// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text.Json;

namespace TuiVision.Drivers.Console;

/// <summary>
/// Benennt den Capability-Zustand eines effektiven Terminalprofils.
///
/// Names the capability state of an effective terminal profile.
/// </summary>
public enum TerminalProfileCapabilityState
{
    /// <summary>Der kontrollierte Vertrag ist verfügbar. / The controlled contract is available.</summary>
    Enabled,

    /// <summary>Der Vertrag ist absichtlich deaktiviert. / The contract is deliberately disabled.</summary>
    Disabled,

    /// <summary>Eine angeforderte Capability ist nicht verfügbar. / A requested capability is unavailable.</summary>
    Unsupported
}

/// <summary>
/// Benennt das Ergebnis des Profil-Ladens.
///
/// Names the profile-load outcome.
/// </summary>
public enum TerminalProfileParseOutcome
{
    /// <summary>Das Profil ist vollständig gültig. / The profile is fully valid.</summary>
    Valid,

    /// <summary>Das Schema ist ungültig und es wurde kein Profil veröffentlicht. / The schema is invalid and no profile was published.</summary>
    Rejected,

    /// <summary>Das Schema ist gültig, aber ein sicherer Capability-Fallback ist aktiv. / The schema is valid, but a safe capability fallback is active.</summary>
    Unsupported
}

/// <summary>
/// Beschreibt das atomare Ergebnis des Profil-Ladens.
///
/// Describes the atomic profile-load result.
/// </summary>
/// <param name="Outcome">Ladestatus. / Load outcome.</param>
/// <param name="Profile">Effektives Profil oder <c>null</c>. / Effective profile or <c>null</c>.</param>
/// <param name="Reason">Textorientierte Begründung. / Text-first rationale.</param>
public readonly record struct TerminalProfileParseResult(
    TerminalProfileParseOutcome Outcome,
    TerminalProfile? Profile,
    string Reason);

/// <summary>
/// Repräsentiert ein geschlossen validiertes, effektives Terminalprofil.
///
/// Represents a closed-schema validated effective terminal profile.
/// </summary>
public sealed class TerminalProfile
{
    /// <summary>Stabile Kennung der eingebauten 8x16-Fixture. / Stable identifier of the built-in 8x16 fixture.</summary>
    public const string BuiltInFontId = "built-in-8x16";

    private TerminalProfile(
        string profileId,
        TerminalCharset charset,
        string? requestedFontId,
        string effectiveFontId,
        ConsoleColor foreground,
        ConsoleColor background,
        TerminalProfileCapabilityState capabilityState,
        string fallbackReason)
    {
        ProfileId = profileId;
        Charset = charset;
        RequestedFontId = requestedFontId;
        EffectiveFontId = effectiveFontId;
        Foreground = foreground;
        Background = background;
        CapabilityState = capabilityState;
        FallbackReason = fallbackReason;
    }

    /// <summary>Stabile Profilkennung. / Stable profile identifier.</summary>
    public string ProfileId { get; }

    /// <summary>Effektiver Zeichensatz. / Effective character set.</summary>
    public TerminalCharset Charset { get; }

    /// <summary>Angeforderte Fontkennung oder <c>null</c>. / Requested font identifier or <c>null</c>.</summary>
    public string? RequestedFontId { get; }

    /// <summary>Effektive Fontkennung. / Effective font identifier.</summary>
    public string EffectiveFontId { get; }

    /// <summary>Effektive Vordergrundfarbe. / Effective foreground color.</summary>
    public ConsoleColor Foreground { get; }

    /// <summary>Effektive Hintergrundfarbe. / Effective background color.</summary>
    public ConsoleColor Background { get; }

    /// <summary>Effektiver Capability-Zustand. / Effective capability state.</summary>
    public TerminalProfileCapabilityState CapabilityState { get; }

    /// <summary>Default-/Fallback-Begründung oder <c>None</c>. / Default/fallback rationale or <c>None</c>.</summary>
    public string FallbackReason { get; }

    /// <summary>
    /// Lädt ein geschlossenes JSON-Profil und veröffentlicht erst nach vollständiger Validierung.
    ///
    /// Loads a closed JSON profile and publishes it only after complete validation.
    /// </summary>
    /// <param name="json">JSON-Text. / JSON text.</param>
    /// <param name="availableFontIds">Kontrollierte verfügbare Fontkennungen. / Controlled available font identifiers.</param>
    /// <param name="hostCapabilityAvailable">Verfügbarkeit des angeforderten Hostvertrags. / Availability of the requested host contract.</param>
    /// <returns>Atomarer Lade- oder Fallbackstatus. / Atomic load or fallback result.</returns>
    public static TerminalProfileParseResult Parse(
        string json,
        IEnumerable<string>? availableFontIds = null,
        bool hostCapabilityAvailable = true)
    {
        ArgumentNullException.ThrowIfNull(json);

        try
        {
            using JsonDocument document = JsonDocument.Parse(
                json,
                new JsonDocumentOptions { AllowTrailingCommas = false, CommentHandling = JsonCommentHandling.Disallow });
            if (document.RootElement.ValueKind != JsonValueKind.Object)
            {
                return Rejected("Profile root must be an object. / Profilwurzel muss ein Objekt sein.");
            }

            Dictionary<string, JsonElement> values = new(StringComparer.Ordinal);
            HashSet<string> allowed = new(StringComparer.Ordinal)
            {
                "ProfileId", "Charset", "FontId", "Foreground", "Background"
            };

            foreach (JsonProperty property in document.RootElement.EnumerateObject())
            {
                if (!allowed.Contains(property.Name) || !values.TryAdd(property.Name, property.Value))
                {
                    return Rejected("Profile contains an unknown or duplicate key. / Profil enthält einen unbekannten oder doppelten Key.");
                }

                if (property.Value.ValueKind != JsonValueKind.String)
                {
                    return Rejected("Profile values must be strings. / Profilwerte müssen Zeichenfolgen sein.");
                }
            }

            if (!TryRequired(values, "ProfileId", out string profileId) ||
                !TryRequired(values, "Charset", out string charsetName) ||
                !TryCharset(charsetName, out TerminalCharset charset))
            {
                return Rejected("ProfileId and supported Charset are required. / ProfileId und unterstützter Charset sind erforderlich.");
            }

            string? requestedFontId = Optional(values, "FontId");
            string? foregroundName = Optional(values, "Foreground");
            string? backgroundName = Optional(values, "Background");
            if (!TryColor(foregroundName, ConsoleColor.Gray, out ConsoleColor foreground) ||
                !TryColor(backgroundName, ConsoleColor.Black, out ConsoleColor background))
            {
                return Rejected("Profile color is invalid. / Profilfarbe ist ungültig.");
            }

            HashSet<string> fonts = new(availableFontIds ?? Array.Empty<string>(), StringComparer.Ordinal);
            fonts.Add(BuiltInFontId);
            bool fontAvailable = requestedFontId is null || fonts.Contains(requestedFontId);
            string effectiveFontId = fontAvailable ? requestedFontId ?? BuiltInFontId : BuiltInFontId;
            TerminalProfileCapabilityState state = fontAvailable && hostCapabilityAvailable
                ? TerminalProfileCapabilityState.Enabled
                : TerminalProfileCapabilityState.Unsupported;

            List<string> fallbacks = new();
            if (requestedFontId is null)
            {
                fallbacks.Add("default font");
            }

            if (foregroundName is null)
            {
                fallbacks.Add("default foreground");
            }

            if (backgroundName is null)
            {
                fallbacks.Add("default background");
            }

            if (!fontAvailable)
            {
                fallbacks.Add("requested font unavailable; built-in font used");
            }

            if (!hostCapabilityAvailable)
            {
                fallbacks.Add("host capability unavailable; in-process contract retained");
            }

            string fallbackReason = fallbacks.Count == 0 ? "None" : string.Join("; ", fallbacks);
            TerminalProfile profile = new(
                profileId,
                charset,
                requestedFontId,
                effectiveFontId,
                foreground,
                background,
                state,
                fallbackReason);
            TerminalProfileParseOutcome outcome = state == TerminalProfileCapabilityState.Enabled
                ? TerminalProfileParseOutcome.Valid
                : TerminalProfileParseOutcome.Unsupported;
            return new TerminalProfileParseResult(outcome, profile, fallbackReason);
        }
        catch (JsonException exception)
        {
            return Rejected($"Malformed profile JSON: {exception.Message}");
        }
    }

    private static bool TryRequired(Dictionary<string, JsonElement> values, string key, out string value)
    {
        value = string.Empty;
        if (!values.TryGetValue(key, out JsonElement element))
        {
            return false;
        }

        value = element.GetString() ?? string.Empty;
        return !string.IsNullOrWhiteSpace(value);
    }

    private static string? Optional(Dictionary<string, JsonElement> values, string key) =>
        values.TryGetValue(key, out JsonElement element) ? element.GetString() : null;

    private static bool TryCharset(string value, out TerminalCharset charset)
    {
        if (string.Equals(value, "Unicode", StringComparison.OrdinalIgnoreCase))
        {
            charset = TerminalCharset.Unicode;
            return true;
        }

        if (string.Equals(value, "KOI8-R", StringComparison.OrdinalIgnoreCase))
        {
            charset = TerminalCharset.Koi8R;
            return true;
        }

        charset = TerminalCharset.Unknown;
        return false;
    }

    private static bool TryColor(string? value, ConsoleColor fallback, out ConsoleColor color)
    {
        if (value is null)
        {
            color = fallback;
            return true;
        }

        return Enum.TryParse(value, ignoreCase: true, out color) && Enum.IsDefined(color);
    }

    private static TerminalProfileParseResult Rejected(string reason) =>
        new(TerminalProfileParseOutcome.Rejected, null, reason);
}

/// <summary>
/// Benennt Hostfamilien für getrennte Terminal-Evidence.
///
/// Names host families for separate terminal evidence.
/// </summary>
public enum TerminalHostFamily
{
    /// <summary>Unbekannter Host. / Unknown host.</summary>
    Unknown,

    /// <summary>macOS. / macOS.</summary>
    MacOS,

    /// <summary>Linux. / Linux.</summary>
    Linux,

    /// <summary>Windows Subsystem for Linux. / Windows Subsystem for Linux.</summary>
    Wsl,

    /// <summary>Natives Windows. / Native Windows.</summary>
    Windows,

    /// <summary>Umgeleitete oder nicht interaktive Umgebung. / Redirected or non-interactive environment.</summary>
    Headless
}

/// <summary>
/// Benennt die Nachweisklasse ohne Gleichsetzung mit physischer Beobachtung.
///
/// Names the evidence class without equating it with physical observation.
/// </summary>
public enum TerminalHostEvidenceClass
{
    /// <summary>Deterministischer In-Process-Nachweis. / Deterministic in-process proof.</summary>
    DeterministicInProcess,

    /// <summary>Remote-CI-Nachweis. / Remote CI proof.</summary>
    RemoteCI,

    /// <summary>Physische Hostbeobachtung. / Physical host observation.</summary>
    PhysicalObservation
}

/// <summary>
/// Beschreibt eine deterministische Hostklassifikation.
///
/// Describes a deterministic host classification.
/// </summary>
/// <param name="HostFamily">Hostfamilie. / Host family.</param>
/// <param name="State">Capability-Zustand. / Capability state.</param>
/// <param name="EvidenceClass">Nachweisklasse. / Evidence class.</param>
/// <param name="Reason">Textorientierte Begründung. / Text-first rationale.</param>
public readonly record struct TerminalHostCapability(
    TerminalHostFamily HostFamily,
    TerminalProfileCapabilityState State,
    TerminalHostEvidenceClass EvidenceClass,
    string Reason);

/// <summary>
/// Klassifiziert kontrollierte Hostmerkmale, ohne physische Terminalprüfung zu behaupten.
///
/// Classifies controlled host traits without claiming physical terminal observation.
/// </summary>
public static class TerminalHostCapabilityDetector
{
    /// <summary>
    /// Erkennt den aktuellen Prozesszustand als deterministische Evidence.
    ///
    /// Detects the current process state as deterministic evidence.
    /// </summary>
    /// <returns>Aktuelle Hostklassifikation. / Current host classification.</returns>
    public static TerminalHostCapability DetectCurrent() => Detect(
        System.Console.IsInputRedirected || System.Console.IsOutputRedirected,
        OperatingSystem.IsWindows(),
        OperatingSystem.IsMacOS(),
        OperatingSystem.IsLinux(),
        !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("WSL_DISTRO_NAME")));

    /// <summary>
    /// Klassifiziert kontrollierte Hostmerkmale für Tests und Evidence.
    ///
    /// Classifies controlled host traits for tests and evidence.
    /// </summary>
    /// <param name="redirected">Ein-/Ausgabe ist umgeleitet. / Input/output is redirected.</param>
    /// <param name="isWindows">Windows-Laufzeit. / Windows runtime.</param>
    /// <param name="isMacOS">macOS-Laufzeit. / macOS runtime.</param>
    /// <param name="isLinux">Linux-Laufzeit. / Linux runtime.</param>
    /// <param name="isWsl">WSL-Umgebung. / WSL environment.</param>
    /// <returns>Deterministische Klassifikation. / Deterministic classification.</returns>
    public static TerminalHostCapability Detect(
        bool redirected,
        bool isWindows,
        bool isMacOS,
        bool isLinux,
        bool isWsl)
    {
        if (redirected)
        {
            return new TerminalHostCapability(
                TerminalHostFamily.Headless,
                TerminalProfileCapabilityState.Unsupported,
                TerminalHostEvidenceClass.DeterministicInProcess,
                "Redirected I/O has no physical terminal claim. / Umgeleitete Ein-/Ausgabe hat keinen physischen Terminalnachweis.");
        }

        TerminalHostFamily family = isWsl
            ? TerminalHostFamily.Wsl
            : isMacOS
                ? TerminalHostFamily.MacOS
                : isLinux
                    ? TerminalHostFamily.Linux
                    : isWindows
                        ? TerminalHostFamily.Windows
                        : TerminalHostFamily.Unknown;
        TerminalProfileCapabilityState state = family == TerminalHostFamily.Unknown
            ? TerminalProfileCapabilityState.Unsupported
            : TerminalProfileCapabilityState.Enabled;
        return new TerminalHostCapability(
            family,
            state,
            TerminalHostEvidenceClass.DeterministicInProcess,
            "Host family classified; physical observation remains separate. / Hostfamilie klassifiziert; physische Beobachtung bleibt getrennt.");
    }
}
