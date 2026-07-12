// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Console.Tests;

/// <summary>
/// Prüft das geschlossene Terminalprofil, sichere Defaults, atomare Ablehnung
/// und getrennte Host-Capability-Evidence.
///
/// Verifies the closed terminal profile, safe defaults, atomic rejection, and
/// separate host-capability evidence.
/// </summary>
[TestClass]
public sealed class TerminalProfileTests
{
    /// <summary>
    /// Prüft ein minimales Profil mit allen dokumentierten Defaults.
    ///
    /// Verifies a minimal profile with all documented defaults.
    /// </summary>
    [TestMethod]
    public void Profile_Minimal_UsesSafeDefaults()
    {
        TerminalProfileParseResult result = TerminalProfile.Parse("""
            { "ProfileId": "minimal", "Charset": "Unicode" }
            """);

        Assert.AreEqual(TerminalProfileParseOutcome.Valid, result.Outcome);
        Assert.IsNotNull(result.Profile);
        Assert.AreEqual("minimal", result.Profile.ProfileId);
        Assert.AreEqual(TerminalCharset.Unicode, result.Profile.Charset);
        Assert.AreEqual(TerminalProfile.BuiltInFontId, result.Profile.EffectiveFontId);
        Assert.AreEqual(ConsoleColor.Gray, result.Profile.Foreground);
        Assert.AreEqual(ConsoleColor.Black, result.Profile.Background);
        Assert.IsTrue(result.Profile.FallbackReason.Contains("default", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Prüft ein vollständiges gültiges Profil und die Anwendung auf eine Sitzung.
    ///
    /// Verifies a complete valid profile and its application to a session.
    /// </summary>
    [TestMethod]
    public void Profile_Full_AppliesObservablePresentationMetadata()
    {
        TerminalProfileParseResult result = TerminalProfile.Parse("""
            {
              "ProfileId": "koi8-proof",
              "Charset": "KOI8-R",
              "FontId": "fixture-8x16",
              "Foreground": "Yellow",
              "Background": "DarkBlue"
            }
            """, availableFontIds: new[] { "fixture-8x16" });

        Assert.AreEqual(TerminalProfileParseOutcome.Valid, result.Outcome);
        Assert.IsNotNull(result.Profile);
        using TerminalSession session = new(8, 2);
        session.ApplyProfile(result.Profile);

        Assert.AreEqual("koi8-proof", session.ActiveProfileId);
        Assert.AreEqual(TerminalCharset.Koi8R, session.ActiveCharset);
        Assert.AreEqual("fixture-8x16", session.ActiveFontId);
        Assert.AreEqual(ConsoleColor.Yellow, session.Foreground);
        Assert.AreEqual(ConsoleColor.DarkBlue, session.Background);
    }

    /// <summary>
    /// Prüft jedes optionale Feld unabhängig auf seinen Default.
    ///
    /// Verifies the default for each optional field independently.
    /// </summary>
    [TestMethod]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"Unicode\", \"Foreground\": \"Red\", \"Background\": \"Blue\" }")]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"Unicode\", \"FontId\": \"built-in-8x16\", \"Background\": \"Blue\" }")]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"Unicode\", \"FontId\": \"built-in-8x16\", \"Foreground\": \"Red\" }")]
    public void Profile_MissingOptionalField_RemainsValid(string json)
    {
        TerminalProfileParseResult result = TerminalProfile.Parse(json);

        Assert.AreEqual(TerminalProfileParseOutcome.Valid, result.Outcome);
        Assert.IsNotNull(result.Profile);
    }

    /// <summary>
    /// Prüft fehlende oder leere Pflichtfelder.
    ///
    /// Verifies missing or empty required fields.
    /// </summary>
    [TestMethod]
    [DataRow("{ \"Charset\": \"Unicode\" }")]
    [DataRow("{ \"ProfileId\": \"\", \"Charset\": \"Unicode\" }")]
    [DataRow("{ \"ProfileId\": \"p\" }")]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"CP437\" }")]
    public void Profile_InvalidRequiredField_RejectsWholeProfile(string json)
    {
        TerminalProfileParseResult result = TerminalProfile.Parse(json);

        Assert.AreEqual(TerminalProfileParseOutcome.Rejected, result.Outcome);
        Assert.IsNull(result.Profile);
    }

    /// <summary>
    /// Prüft malformed, unbekannte, doppelte und typfalsche Werte.
    ///
    /// Verifies malformed, unknown, duplicate, and wrongly typed values.
    /// </summary>
    [TestMethod]
    [DataRow("{")]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"Unicode\", \"Unknown\": 1 }")]
    [DataRow("{ \"ProfileId\": \"p\", \"ProfileId\": \"q\", \"Charset\": \"Unicode\" }")]
    [DataRow("{ \"ProfileId\": 12, \"Charset\": \"Unicode\" }")]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"Unicode\", \"Foreground\": \"Invisible\" }")]
    [DataRow("{ \"ProfileId\": \"p\", \"Charset\": \"Unicode\" } trailing")]
    public void Profile_InvalidSchema_RejectsWholeProfile(string json)
    {
        TerminalProfileParseResult result = TerminalProfile.Parse(json);

        Assert.AreEqual(TerminalProfileParseOutcome.Rejected, result.Outcome);
        Assert.IsNull(result.Profile);
    }

    /// <summary>
    /// Prüft sichtbaren sicheren Fallback für nicht verfügbare Capabilities.
    ///
    /// Verifies visible safe fallback for unavailable capabilities.
    /// </summary>
    [TestMethod]
    public void Profile_UnavailableFontOrHost_UsesSafeFallbackAndUnsupportedStatus()
    {
        const string json = """
            { "ProfileId": "fallback", "Charset": "KOI8-R", "FontId": "missing-font" }
            """;

        TerminalProfileParseResult result = TerminalProfile.Parse(
            json,
            availableFontIds: Array.Empty<string>(),
            hostCapabilityAvailable: false);

        Assert.AreEqual(TerminalProfileParseOutcome.Unsupported, result.Outcome);
        Assert.IsNotNull(result.Profile);
        Assert.AreEqual("missing-font", result.Profile.RequestedFontId);
        Assert.AreEqual(TerminalProfile.BuiltInFontId, result.Profile.EffectiveFontId);
        Assert.AreEqual(TerminalProfileCapabilityState.Unsupported, result.Profile.CapabilityState);
        Assert.IsFalse(string.IsNullOrWhiteSpace(result.Profile.FallbackReason));
    }

    /// <summary>
    /// Prüft macOS, Linux, WSL, Windows und Headless ohne physische Überbehauptung.
    ///
    /// Verifies macOS, Linux, WSL, Windows, and headless states without physical overclaiming.
    /// </summary>
    [TestMethod]
    public void HostDetector_ClassifiesControlledHostsWithoutPhysicalClaim()
    {
        Assert.AreEqual(TerminalHostFamily.MacOS, TerminalHostCapabilityDetector.Detect(false, false, true, false, false).HostFamily);
        Assert.AreEqual(TerminalHostFamily.Linux, TerminalHostCapabilityDetector.Detect(false, false, false, true, false).HostFamily);
        Assert.AreEqual(TerminalHostFamily.Wsl, TerminalHostCapabilityDetector.Detect(false, false, false, true, true).HostFamily);
        Assert.AreEqual(TerminalHostFamily.Windows, TerminalHostCapabilityDetector.Detect(false, true, false, false, false).HostFamily);
        TerminalHostCapability headless = TerminalHostCapabilityDetector.Detect(true, false, true, false, false);
        Assert.AreEqual(TerminalHostFamily.Headless, headless.HostFamily);
        Assert.AreEqual(TerminalProfileCapabilityState.Unsupported, headless.State);
        Assert.AreEqual(TerminalHostEvidenceClass.DeterministicInProcess, headless.EvidenceClass);
    }
}
