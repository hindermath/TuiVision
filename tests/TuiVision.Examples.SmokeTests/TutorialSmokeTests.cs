// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Tutorial;
using TuiVision.Examples.Tutorial.Steps;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests für alle 16 Tutorial-Schritte.
/// Jeder Test prüft Start, schritttypisches Verhalten, geordnete Auffindbarkeit
/// und saubere Beendigung für das jeweilige Token.
///
/// Smoke tests for all 16 tutorial steps.
/// Each test verifies startup, step-specific defining behaviour, ordered discoverability,
/// and clean exit for the respective token.
/// </summary>
[TestClass]
public sealed class TutorialSmokeTests : ExampleTestBase
{
    private static readonly IReadOnlyDictionary<string, string> ExpectedTitleFragments =
        new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["tvguid01"] = "Minimal TApplication",
            ["tvguid02"] = "Menu bar with submenus",
            ["tvguid03"] = "Menu command handling",
            ["tvguid04"] = "Opening a TWindow",
            ["tvguid05"] = "Drawing content into a window",
            ["tvguid06"] = "Vertical scroll bar",
            ["tvguid07"] = "Horizontal and vertical scroll bars",
            ["tvguid08"] = "Scroll bars and delta point",
            ["tvguid09"] = "Multiple windows",
            ["tvguid10"] = "Opening a TDialog",
            ["tvguid11"] = "Buttons in a dialog",
            ["tvguid12"] = "Input line in a dialog",
            ["tvguid13"] = "Two buttons",
            ["tvguid14"] = "Check boxes and radio buttons",
            ["tvguid15"] = "Saving dialog data",
            ["tvguid16"] = "Save and restore dialog data"
        };

    // ── Katalog-Struktur-Tests / Catalog structure tests ──────────────────────────

    /// <summary>
    /// Stellt sicher, dass der Katalog genau 16 Schritte enthält.
    ///
    /// Asserts that the catalog contains exactly 16 steps.
    /// </summary>
    [TestMethod]
    public void Tutorial_Catalog_Contains16Steps()
    {
        AssertEqual(16, TutorialStepCatalog.All.Count, "Katalog muss 16 Schritte enthalten / Catalog must contain 16 steps");
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalProof);
        AssertDirectHelperUsage(DirectHelperUsage.SupplementalProof);
    }

    /// <summary>
    /// Stellt sicher, dass alle Schritte in geordneter Reihenfolge im Katalog vorliegen.
    ///
    /// Asserts that all steps are present in ordered sequence in the catalog.
    /// </summary>
    [TestMethod]
    public void Tutorial_Catalog_StepsAreOrdered()
    {
        IReadOnlyList<ITutorialStep> ordered = TutorialStepCatalog.InOrder;
        for (int i = 0; i < ordered.Count; i++)
        {
            AssertEqual(i + 1, ordered[i].SequenceNumber,
                $"Schritt {i + 1} muss Sequenznummer {i + 1} haben / Step {i + 1} must have sequence number {i + 1}");
        }

        RecordDirectHelperUsage(DirectHelperUsage.SupplementalProof);
        AssertDirectHelperUsage(DirectHelperUsage.SupplementalProof);
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="TutorialApp"/> bei einem unbekannten Token eine
    /// Fallback-Anwendung startet und sauber beendet wird.
    ///
    /// Asserts that <see cref="TutorialApp"/> starts a fallback application and exits cleanly
    /// when given an unknown token.
    /// </summary>
    [TestMethod]
    public void Tutorial_UnknownToken_StartsFallbackAndExitsCleanly()
    {
        TutorialApp launcher = new("tvguidXX", DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => launcher.Run());
        AssertTrue(
            launcher.LastRunUsedFallback,
            "Unbekanntes Token muss den Fallback-Pfad markieren / Unknown token must mark the fallback path");
        Assert.IsNull(
            launcher.LastRunStepToken,
            "Fallback darf keinen ausgefuehrten Tutorial-Schritt melden. / Fallback must not report an executed tutorial step.");
        RecordDirectHelperUsage(DirectHelperUsage.PrimaryProof);
        AssertDirectHelperUsage(DirectHelperUsage.PrimaryProof);
    }

    // ── tvguid01–tvguid08 / Steps 01–08 ──────────────────────────────────────────

    /// <summary>Start und saubere Beendigung: tvguid01. / Startup and clean exit: tvguid01.</summary>
    [TestMethod]
    public void Tutorial_TvGuid01_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid01");

    /// <summary>Start und saubere Beendigung: tvguid02. / Startup and clean exit: tvguid02.</summary>
    [TestMethod]
    public void Tutorial_TvGuid02_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid02");

    /// <summary>Start und saubere Beendigung: tvguid03. / Startup and clean exit: tvguid03.</summary>
    [TestMethod]
    public void Tutorial_TvGuid03_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid03");

    /// <summary>Start und saubere Beendigung: tvguid04. / Startup and clean exit: tvguid04.</summary>
    [TestMethod]
    public void Tutorial_TvGuid04_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid04");

    /// <summary>Start und saubere Beendigung: tvguid05. / Startup and clean exit: tvguid05.</summary>
    [TestMethod]
    public void Tutorial_TvGuid05_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid05");

    /// <summary>Start und saubere Beendigung: tvguid06. / Startup and clean exit: tvguid06.</summary>
    [TestMethod]
    public void Tutorial_TvGuid06_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid06");

    /// <summary>Start und saubere Beendigung: tvguid07. / Startup and clean exit: tvguid07.</summary>
    [TestMethod]
    public void Tutorial_TvGuid07_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid07");

    /// <summary>Start und saubere Beendigung: tvguid08. / Startup and clean exit: tvguid08.</summary>
    [TestMethod]
    public void Tutorial_TvGuid08_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid08");

    // ── tvguid09–tvguid16 / Steps 09–16 ──────────────────────────────────────────

    /// <summary>Start und saubere Beendigung: tvguid09. / Startup and clean exit: tvguid09.</summary>
    [TestMethod]
    public void Tutorial_TvGuid09_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid09");

    /// <summary>Start und saubere Beendigung: tvguid10. / Startup and clean exit: tvguid10.</summary>
    [TestMethod]
    public void Tutorial_TvGuid10_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid10");

    /// <summary>Start und saubere Beendigung: tvguid11. / Startup and clean exit: tvguid11.</summary>
    [TestMethod]
    public void Tutorial_TvGuid11_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid11");

    /// <summary>Start und saubere Beendigung: tvguid12. / Startup and clean exit: tvguid12.</summary>
    [TestMethod]
    public void Tutorial_TvGuid12_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid12");

    /// <summary>Start und saubere Beendigung: tvguid13. / Startup and clean exit: tvguid13.</summary>
    [TestMethod]
    public void Tutorial_TvGuid13_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid13");

    /// <summary>Start und saubere Beendigung: tvguid14. / Startup and clean exit: tvguid14.</summary>
    [TestMethod]
    public void Tutorial_TvGuid14_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid14");

    /// <summary>Start und saubere Beendigung: tvguid15. / Startup and clean exit: tvguid15.</summary>
    [TestMethod]
    public void Tutorial_TvGuid15_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid15");

    /// <summary>Start und saubere Beendigung: tvguid16. / Startup and clean exit: tvguid16.</summary>
    [TestMethod]
    public void Tutorial_TvGuid16_StartsAndExitsCleanly()
        => AssertStepSmokeRun("tvguid16");

    // ── Token-Identitäts-Tests / Token identity tests ─────────────────────────────

    /// <summary>
    /// Stellt sicher, dass jedes der 16 Tokens im Katalog gefunden wird und
    /// eine valide Beschreibung hat.
    ///
    /// Asserts that each of the 16 tokens is found in the catalog and has a valid description.
    /// </summary>
    [TestMethod]
    public void Tutorial_AllTokensAreDiscoverable()
    {
        for (int i = 1; i <= 16; i++)
        {
            string token = $"tvguid{i:D2}";
            AssertTrue(
                TutorialStepCatalog.All.ContainsKey(token),
                $"Token '{token}' muss im Katalog vorhanden sein / Token '{token}' must be present in catalog");

            ITutorialStep step = TutorialStepCatalog.All[token];
            AssertTrue(
                !string.IsNullOrWhiteSpace(step.Title),
                $"Schritt '{token}' muss einen nicht-leeren Titel haben / Step '{token}' must have a non-empty title");
            AssertVisibleContains(
                step.Title,
                ExpectedTitleFragments[token],
                $"Schritt '{token}' muss sein spezifisches Lernziel im Titel tragen / " +
                $"Step '{token}' must carry its specific learning target in the title");
            AssertVisibleContains(
                step.Description,
                " / ",
                $"Schritt '{token}' muss eine zweisprachige Beschreibung tragen / " +
                $"Step '{token}' must carry a bilingual description");
        }

        RecordDirectHelperUsage(DirectHelperUsage.SupplementalProof);
        AssertDirectHelperUsage(DirectHelperUsage.SupplementalProof);
    }

    // ── Hilfsmethode / Helper method ──────────────────────────────────────────────

    // Führt einen vollständigen Smoke-Zyklus für einen Tutorial-Schritt aus.
    // Runs a complete smoke cycle for a tutorial step.
    private void AssertStepSmokeRun(string token)
    {
        AssertTrue(
            TutorialStepCatalog.All.ContainsKey(token),
            $"Token '{token}' muss im Katalog vorhanden sein / Token '{token}' must exist in catalog");

        ITutorialStep step = TutorialStepCatalog.All[token];
        int expectedSequence = int.Parse(token[^2..], System.Globalization.CultureInfo.InvariantCulture);
        AssertEqual(
            expectedSequence,
            step.SequenceNumber,
            $"Token '{token}' muss die passende Sequenznummer tragen / Token '{token}' must carry the matching sequence number");
        AssertVisibleContains(
            step.Title,
            ExpectedTitleFragments[token],
            $"Token '{token}' muss ein schrittspezifisches Lernziel beweisen / " +
            $"Token '{token}' must prove a step-specific learning target");
        AssertVisibleContains(
            step.Description,
            " / ",
            $"Token '{token}' muss einen zweisprachigen Lernzieltext besitzen / " +
            $"Token '{token}' must have bilingual learning-target text");

        TutorialApp launcher = new(token, DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => launcher.Run());
        AssertEqual(
            token,
            launcher.LastRunStepToken,
            $"Launcher muss genau '{token}' ausgefuehrt haben / Launcher must have executed exactly '{token}'");
        Assert.IsFalse(
            launcher.LastRunUsedFallback,
            $"Token '{token}' darf keinen Fallback nutzen. / Token '{token}' must not use fallback.");
        RecordPrimaryAssertionUsedAppLoop();
        RecordDirectHelperUsage(DirectHelperUsage.PrimaryProof);
        AssertPrimaryAssertionUsedAppLoop();
        AssertDirectHelperUsage(DirectHelperUsage.PrimaryProof);
    }
}
