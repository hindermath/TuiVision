// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text.Json;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft den kanonischen Example-Portfolio-Audit und seine fail-closed Grenzen.
///
/// Verifies the canonical example-portfolio audit and its fail-closed boundaries.
/// </summary>
[TestClass]
public sealed class ExamplePortfolioAuditIntegrityTests
{
    private static readonly ExpectedEntry[] ExpectedPortfolio =
    [
        new("EX001", "Desklogo", "HistoricalExample", "Wave1"),
        new("EX002", "MsgCls", "HistoricalExample", "Wave1"),
        new("EX003", "Tutorial", "HistoricalExample", "Wave1"),
        new("EX004", "Videomode", "HistoricalExample", "Wave1"),
        new("EX005", "Clipboard", "HistoricalExample", "Wave2"),
        new("EX006", "Demo", "HistoricalExample", "Wave2"),
        new("EX007", "DlgDsn", "HistoricalExample", "Wave2"),
        new("EX008", "DynTxt", "HistoricalExample", "Wave2"),
        new("EX009", "InpLis", "HistoricalExample", "Wave2"),
        new("EX010", "ListVi", "HistoricalExample", "Wave2"),
        new("EX011", "ProgBa", "HistoricalExample", "Wave2"),
        new("EX012", "Sdlg", "HistoricalExample", "Wave2"),
        new("EX013", "Sdlg2", "HistoricalExample", "Wave2"),
        new("EX014", "TCombo", "HistoricalExample", "Wave2"),
        new("EX015", "TProgB", "HistoricalExample", "Wave2"),
        new("EX016", "BHelp", "HistoricalExample", "Wave3"),
        new("EX017", "HelpDemo", "HistoricalExample", "Wave3"),
        new("EX018", "I18n", "HistoricalExample", "Wave3"),
        new("EX019", "TvEdit", "HistoricalExample", "Wave3"),
        new("EX020", "TvHc", "HistoricalExample", "Wave3"),
        new("EX021", "Cyrillic", "HistoricalExample", "Wave4"),
        new("EX022", "ETerm", "HistoricalExample", "Wave4"),
        new("EX023", "Fonts", "HistoricalExample", "Wave4"),
        new("EX024", "Terminal", "HistoricalExample", "Wave4"),
        new("EX025", "XTerm", "HistoricalExample", "Wave4"),
        new("EX026", "Tp7AsciiTable", "HistoricalExample", "Wave5"),
        new("EX027", "Tp7Calculator", "HistoricalExample", "Wave5"),
        new("EX028", "Tp7Calendar", "HistoricalExample", "Wave5"),
        new("EX029", "Tp7Demo", "HistoricalExample", "Wave5"),
        new("EX030", "Tp7Edit", "HistoricalExample", "Wave5"),
        new("EX031", "Tp7Help", "HistoricalExample", "Wave5"),
        new("EX032", "Tp7MouseDialog", "HistoricalExample", "Wave5"),
        new("EX033", "Tp7Puzzle", "HistoricalExample", "Wave5"),
        new("EX034", "Tp7ResourceDemo", "HistoricalExample", "Wave5"),
        new("EX035", "Tp7ResourceGenerator", "HistoricalExample", "Wave5"),
        new("EX036", "Tp7FileManager", "HistoricalExample", "Wave6"),
        new("EX037", "A11yFramework", "SupplementalControl", "Supplemental")
    ];

    private static readonly Dictionary<string, string> ExpectedMalformedFixtures = new(StringComparer.Ordinal)
    {
        ["malformed-json-syntax.json"] = "EPA001",
        ["malformed-unknown-schema.json"] = "EPA002",
        ["malformed-wrong-feature-or-mode.json"] = "EPA003",
        ["malformed-intake-hash.json"] = "EPA004",
        ["malformed-project-set-hash.json"] = "EPA005",
        ["malformed-missing-example.json"] = "EPA010",
        ["malformed-duplicate-example.json"] = "EPA011",
        ["malformed-unknown-example.json"] = "EPA012",
        ["malformed-role-wave.json"] = "EPA013",
        ["malformed-a11y-history.json"] = "EPA014",
        ["malformed-missing-source.json"] = "EPA020",
        ["malformed-duplicate-source-path.json"] = "EPA021",
        ["malformed-source-hash.json"] = "EPA022",
        ["malformed-orphan-source.json"] = "EPA023",
        ["malformed-nonreciprocal-source.json"] = "EPA024",
        ["malformed-moving-upstream.json"] = "EPA025",
        ["malformed-missing-evidence.json"] = "EPA030",
        ["malformed-nonreciprocal-evidence.json"] = "EPA031",
        ["malformed-protected-evidence-path.json"] = "EPA032",
        ["malformed-unknown-dimension.json"] = "EPA040",
        ["malformed-na-without-rationale.json"] = "EPA041",
        ["malformed-pass-without-evidence.json"] = "EPA042",
        ["malformed-multiple-disposition.json"] = "EPA043",
        ["malformed-accepted-with-gap.json"] = "EPA044",
        ["malformed-gap-without-finding.json"] = "EPA045",
        ["malformed-framework-decision.json"] = "EPA046",
        ["malformed-finding-id-gap.json"] = "EPA050",
        ["malformed-duplicate-dedup-key.json"] = "EPA051",
        ["malformed-split-root-cause.json"] = "EPA052",
        ["malformed-finding-example-link.json"] = "EPA053",
        ["malformed-multiple-primary-owner.json"] = "EPA054",
        ["malformed-unknown-primary-owner.json"] = "EPA055",
        ["malformed-incomplete-finding.json"] = "EPA056",
        ["malformed-owner-cycle.json"] = "EPA060",
        ["malformed-empty-owner-intake.json"] = "EPA061",
        ["malformed-missing-owner-intake.json"] = "EPA062",
        ["malformed-preassigned-feature-number.json"] = "EPA063",
        ["malformed-closure-count.json"] = "EPA064",
        ["malformed-closure-order.json"] = "EPA065",
        ["malformed-started-followup.json"] = "EPA066",
        ["malformed-governance-omission.json"] = "EPA070",
        ["malformed-na-implementation.json"] = "EPA071",
        ["malformed-open-without-owner.json"] = "EPA072",
        ["malformed-remote-claim.json"] = "EPA080",
        ["malformed-premature-conformance.json"] = "EPA081",
        ["malformed-product-decision-ready.json"] = "EPA082"
    };

    /// <summary>
    /// Prüft den vollständigen Wave-6-Vertikalschnitt.
    ///
    /// Verifies the complete Wave-6 vertical slice.
    /// </summary>
    [TestMethod]
    public void Test_Tp7FileManager_VerticalSlice_IsComplete()
    {
        IReadOnlyList<AuditDiagnostic> diagnostics = ExamplePortfolioAuditValidator.ValidateTp7FileManager(
            RepositoryRoot(),
            CanonicalAuditPath());

        Assert.AreEqual(0, diagnostics.Count, string.Join(Environment.NewLine, diagnostics));
    }

    /// <summary>
    /// Prüft die exakte Portfolio-Grundmenge.
    ///
    /// Verifies the exact portfolio population.
    /// </summary>
    [TestMethod]
    public void Test_Portfolio_ContainsExactlyTheAcceptedEntries()
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(CanonicalAuditPath()));
        JsonElement[] actual = document.RootElement.GetProperty("portfolioEntries").EnumerateArray().ToArray();
        Assert.AreEqual(37, actual.Length, "EPA010: The canonical portfolio must contain exactly 37 entries.");

        for (int index = 0; index < ExpectedPortfolio.Length; index++)
        {
            ExpectedEntry expected = ExpectedPortfolio[index];
            JsonElement entry = actual[index];
            Assert.AreEqual(expected.Id, entry.GetProperty("exampleId").GetString(), $"EPA012: portfolio ID at {index}");
            Assert.AreEqual(expected.Name, entry.GetProperty("name").GetString(), $"EPA012: portfolio name at {index}");
            Assert.AreEqual(expected.Role, entry.GetProperty("portfolioRole").GetString(), $"EPA013: portfolio role at {index}");
            Assert.AreEqual(expected.Wave, entry.GetProperty("wave").GetString(), $"EPA013: portfolio wave at {index}");
        }
    }

    /// <summary>
    /// Prüft die vollständigen Source-/Evidence-Rückrelationen und den Zeilenvertrag.
    ///
    /// Verifies complete source/evidence reverse relations and the row contract.
    /// </summary>
    [TestMethod]
    public void Test_PortfolioRelations_AreCompleteAndReciprocal()
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(CanonicalAuditPath()));
        JsonElement root = document.RootElement;
        JsonElement[] entries = root.GetProperty("portfolioEntries").EnumerateArray().ToArray();
        JsonElement[] sources = root.GetProperty("sources").EnumerateArray().ToArray();
        JsonElement[] evidence = root.GetProperty("evidence").EnumerateArray().ToArray();
        Dictionary<string, JsonElement> entryById = entries.ToDictionary(
            entry => entry.GetProperty("exampleId").GetString()!,
            StringComparer.Ordinal);
        Dictionary<string, JsonElement> sourceById = sources.ToDictionary(
            source => source.GetProperty("sourceId").GetString()!,
            StringComparer.Ordinal);
        Dictionary<string, JsonElement> evidenceById = evidence.ToDictionary(
            record => record.GetProperty("evidenceId").GetString()!,
            StringComparer.Ordinal);

        Assert.AreEqual(37, entries.Length);
        Assert.AreEqual(25, entries.Count(entry => IsWave(entry, "Wave1", "Wave2", "Wave3", "Wave4")));
        Assert.AreEqual(10, entries.Count(entry => IsWave(entry, "Wave5")));
        Assert.AreEqual(1, entries.Count(entry => IsWave(entry, "Wave6")));
        Assert.AreEqual(1, entries.Count(entry => IsWave(entry, "Supplemental")));
        Assert.AreEqual(sources.Length, sources.Select(source => source.GetProperty("relativePath").GetString()).Distinct(StringComparer.Ordinal).Count());

        string[] dimensionNames =
        [
            "behaviorStatus", "interactionStatus", "proofStatus", "documentationStatus", "a11yStatus",
            "platformStatus", "historicalRelation", "freeVisionRelation", "terminalGuiRelation", "magiblotRelation"
        ];
        foreach (JsonElement entry in entries)
        {
            string exampleId = entry.GetProperty("exampleId").GetString()!;
            string[] sourceIds = entry.GetProperty("historicalSourceIds").EnumerateArray()
                .Concat(entry.GetProperty("modernizationSourceIds").EnumerateArray())
                .Select(id => id.GetString()!)
                .ToArray();
            foreach (string sourceId in sourceIds)
            {
                Assert.IsTrue(sourceById.TryGetValue(sourceId, out JsonElement source), $"EPA020: {exampleId}/{sourceId}");
                Assert.IsTrue(ContainsId(source.GetProperty("exampleIds"), exampleId), $"EPA024: {sourceId}/{exampleId}");
            }

            foreach (string evidenceId in entry.GetProperty("evidenceIds").EnumerateArray().Select(id => id.GetString()!))
            {
                Assert.IsTrue(evidenceById.TryGetValue(evidenceId, out JsonElement record), $"EPA030: {exampleId}/{evidenceId}");
                Assert.IsTrue(ContainsId(record.GetProperty("exampleIds"), exampleId), $"EPA031: {evidenceId}/{exampleId}");
            }

            foreach (string dimensionName in dimensionNames)
            {
                JsonElement dimension = entry.GetProperty(dimensionName);
                string status = dimension.GetProperty("status").GetString()!;
                Assert.IsTrue(status is "Pass" or "IntentionalDeviation" or "N/A", $"EPA040: {exampleId}/{dimensionName}");
                Assert.IsFalse(string.IsNullOrWhiteSpace(dimension.GetProperty("rationale").GetString()));
                Assert.IsFalse(string.IsNullOrWhiteSpace(dimension.GetProperty("reevaluationTrigger").GetString()));
                if (status is "Pass" or "IntentionalDeviation")
                {
                    Assert.IsTrue(dimension.GetProperty("evidenceIds").GetArrayLength() > 0, $"EPA042: {exampleId}/{dimensionName}");
                }
            }

            Assert.AreEqual(0, entry.GetProperty("findingIds").GetArrayLength());
            Assert.AreEqual("AcceptedIntentionalDeviation", entry.GetProperty("primaryDisposition").GetString());
        }

        foreach (JsonElement source in sources)
        {
            string sourceId = source.GetProperty("sourceId").GetString()!;
            foreach (string exampleId in source.GetProperty("exampleIds").EnumerateArray().Select(id => id.GetString()!))
            {
                Assert.IsTrue(entryById.TryGetValue(exampleId, out JsonElement entry), $"EPA023: {sourceId}/{exampleId}");
                Assert.IsTrue(
                    ContainsId(entry.GetProperty("historicalSourceIds"), sourceId)
                    || ContainsId(entry.GetProperty("modernizationSourceIds"), sourceId),
                    $"EPA024: {sourceId}/{exampleId}");
            }
        }

        foreach (JsonElement record in evidence)
        {
            string evidenceId = record.GetProperty("evidenceId").GetString()!;
            foreach (string exampleId in record.GetProperty("exampleIds").EnumerateArray().Select(id => id.GetString()!))
            {
                Assert.IsTrue(entryById.TryGetValue(exampleId, out JsonElement entry), $"EPA031: {evidenceId}/{exampleId}");
                Assert.IsTrue(ContainsId(entry.GetProperty("evidenceIds"), evidenceId), $"EPA031: {evidenceId}/{exampleId}");
            }
        }

        JsonElement supplemental = entryById["EX037"];
        Assert.AreEqual(0, supplemental.GetProperty("historicalSourceIds").GetArrayLength());
        Assert.AreEqual("N/A", supplemental.GetProperty("historicalRelation").GetProperty("status").GetString());
    }

    /// <summary>
    /// Prüft die explizit eingefrorene leere Finding-Menge.
    ///
    /// Verifies the explicitly frozen empty finding set.
    /// </summary>
    [TestMethod]
    public void Test_Findings_EmptySetIsFrozenAndUnlinked()
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(CanonicalAuditPath()));
        JsonElement root = document.RootElement;
        Assert.AreEqual(0, root.GetProperty("findings").GetArrayLength());

        string[] dimensionNames =
        [
            "behaviorStatus", "interactionStatus", "proofStatus", "documentationStatus", "a11yStatus",
            "platformStatus", "historicalRelation", "freeVisionRelation", "terminalGuiRelation", "magiblotRelation"
        ];
        foreach (JsonElement entry in root.GetProperty("portfolioEntries").EnumerateArray())
        {
            Assert.AreEqual(0, entry.GetProperty("findingIds").GetArrayLength());
            Assert.AreNotEqual("CandidateFinding", entry.GetProperty("primaryDisposition").GetString());
            foreach (string dimensionName in dimensionNames)
            {
                Assert.AreNotEqual("Gap", entry.GetProperty(dimensionName).GetProperty("status").GetString());
            }
        }
    }

    /// <summary>
    /// Prüft den deterministischen Null-Finding-Handoff ohne gestartetes Folgefeature.
    ///
    /// Verifies the deterministic zero-finding handoff without a started follow-up feature.
    /// </summary>
    [TestMethod]
    public void Test_Handoff_EmptyOwnersAreSuppressedAndClosureIsLast()
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(CanonicalAuditPath()));
        JsonElement root = document.RootElement;
        JsonElement ownerDag = root.GetProperty("ownerDag");
        JsonElement handoff = root.GetProperty("remediationHandoff");

        Assert.AreEqual(0, root.GetProperty("findings").GetArrayLength());
        Assert.AreEqual(0, ownerDag.GetProperty("nodes").GetArrayLength());
        Assert.AreEqual(0, ownerDag.GetProperty("edges").GetArrayLength());
        Assert.AreEqual(0, ownerDag.GetProperty("topologicalOrder").GetArrayLength());
        CollectionAssert.AreEqual(
            new[] { "FrameworkReuse", "BehaviorInteraction", "ProofPlatform", "LearningA11Y" },
            ownerDag.GetProperty("suppressedOwners").EnumerateArray().Select(value => value.GetString()).ToArray());

        foreach (JsonElement group in handoff.GetProperty("ownerGroups").EnumerateArray())
        {
            Assert.AreEqual("Suppressed", group.GetProperty("status").GetString());
            Assert.AreEqual(0, group.GetProperty("findingIds").GetArrayLength());
            Assert.AreEqual("N/A", group.GetProperty("intakePath").GetString());
            Assert.AreEqual("N/A", group.GetProperty("receiptPath").GetString());
        }

        Assert.AreEqual(0, handoff.GetProperty("remediationIntakes").GetArrayLength());
        JsonElement closure = handoff.GetProperty("closureIntake");
        Assert.AreEqual("Emitted", closure.GetProperty("status").GetString());
        Assert.AreEqual("requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md", closure.GetProperty("path").GetString());
        Assert.AreEqual("specs/intake-authoring-receipts/Lastenheft_Example-Portfolio-Closure.receipt.json", closure.GetProperty("receiptPath").GetString());
        Assert.AreEqual(0, closure.GetProperty("dependsOn").GetArrayLength());
        CollectionAssert.AreEqual(
            new[] { "requirements/intakes/active/Lastenheft_Example-Portfolio-Closure.md" },
            handoff.GetProperty("orderedIntakePaths").EnumerateArray().Select(value => value.GetString()).ToArray());
        Assert.AreEqual(0, handoff.GetProperty("startedFeatureIds").GetArrayLength());
    }

    private static bool IsWave(JsonElement entry, params string[] waves) =>
        waves.Contains(entry.GetProperty("wave").GetString(), StringComparer.Ordinal);

    private static bool ContainsId(JsonElement array, string expected) =>
        array.EnumerateArray().Any(value => string.Equals(value.GetString(), expected, StringComparison.Ordinal));

    /// <summary>
    /// Prüft Schema- und Baseline-Fehler als einzelne Invarianten.
    ///
    /// Verifies schema and baseline failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-json-syntax.json", "EPA001")]
    [DataRow("malformed-unknown-schema.json", "EPA002")]
    [DataRow("malformed-wrong-feature-or-mode.json", "EPA003")]
    [DataRow("malformed-intake-hash.json", "EPA004")]
    [DataRow("malformed-project-set-hash.json", "EPA005")]
    public void Test_SchemaBaseline_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft Inventar-, Rollen- und Wave-Fehler als einzelne Invarianten.
    ///
    /// Verifies inventory, role, and wave failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-missing-example.json", "EPA010")]
    [DataRow("malformed-duplicate-example.json", "EPA011")]
    [DataRow("malformed-unknown-example.json", "EPA012")]
    [DataRow("malformed-role-wave.json", "EPA013")]
    [DataRow("malformed-a11y-history.json", "EPA014")]
    public void Test_Inventory_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft Source-, Evidence- und Pfadfehler als einzelne Invarianten.
    ///
    /// Verifies source, evidence, and path failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-missing-source.json", "EPA020")]
    [DataRow("malformed-duplicate-source-path.json", "EPA021")]
    [DataRow("malformed-source-hash.json", "EPA022")]
    [DataRow("malformed-orphan-source.json", "EPA023")]
    [DataRow("malformed-nonreciprocal-source.json", "EPA024")]
    [DataRow("malformed-moving-upstream.json", "EPA025")]
    [DataRow("malformed-missing-evidence.json", "EPA030")]
    [DataRow("malformed-nonreciprocal-evidence.json", "EPA031")]
    [DataRow("malformed-protected-evidence-path.json", "EPA032")]
    public void Test_Relations_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft Dimensions-, Dispositions- und Frameworkfehler als einzelne Invarianten.
    ///
    /// Verifies dimension, disposition, and framework failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-unknown-dimension.json", "EPA040")]
    [DataRow("malformed-na-without-rationale.json", "EPA041")]
    [DataRow("malformed-pass-without-evidence.json", "EPA042")]
    [DataRow("malformed-multiple-disposition.json", "EPA043")]
    [DataRow("malformed-accepted-with-gap.json", "EPA044")]
    [DataRow("malformed-gap-without-finding.json", "EPA045")]
    [DataRow("malformed-framework-decision.json", "EPA046")]
    public void Test_Decisions_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft Finding-, Deduplizierungs- und Ownerfehler als einzelne Invarianten.
    ///
    /// Verifies finding, deduplication, and owner failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-finding-id-gap.json", "EPA050")]
    [DataRow("malformed-duplicate-dedup-key.json", "EPA051")]
    [DataRow("malformed-split-root-cause.json", "EPA052")]
    [DataRow("malformed-finding-example-link.json", "EPA053")]
    [DataRow("malformed-multiple-primary-owner.json", "EPA054")]
    [DataRow("malformed-unknown-primary-owner.json", "EPA055")]
    [DataRow("malformed-incomplete-finding.json", "EPA056")]
    public void Test_Findings_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft Owner-DAG-, Intake- und Closurefehler als einzelne Invarianten.
    ///
    /// Verifies owner-DAG, intake, and closure failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-owner-cycle.json", "EPA060")]
    [DataRow("malformed-empty-owner-intake.json", "EPA061")]
    [DataRow("malformed-missing-owner-intake.json", "EPA062")]
    [DataRow("malformed-preassigned-feature-number.json", "EPA063")]
    [DataRow("malformed-closure-count.json", "EPA064")]
    [DataRow("malformed-closure-order.json", "EPA065")]
    [DataRow("malformed-started-followup.json", "EPA066")]
    public void Test_Handoff_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft Governance- und Autoritätsfehler als einzelne Invarianten.
    ///
    /// Verifies governance and authority failures as single invariants.
    /// </summary>
    [TestMethod]
    [DataRow("malformed-governance-omission.json", "EPA070")]
    [DataRow("malformed-na-implementation.json", "EPA071")]
    [DataRow("malformed-open-without-owner.json", "EPA072")]
    [DataRow("malformed-remote-claim.json", "EPA080")]
    [DataRow("malformed-premature-conformance.json", "EPA081")]
    [DataRow("malformed-product-decision-ready.json", "EPA082")]
    public void Test_GovernanceAuthority_MalformedFixtures(string fixtureName, string expectedCode) =>
        AssertSingleMalformedDiagnostic(fixtureName, expectedCode);

    /// <summary>
    /// Prüft die exakte kanonische Fixture- und Diagnosemenge.
    ///
    /// Verifies the exact canonical fixture and diagnostic set.
    /// </summary>
    [TestMethod]
    public void Test_FixtureContract_HasExactly46SingleCauseCases()
    {
        string fixtureDirectory = Path.Combine(
            RepositoryRoot(),
            "tests",
            "TuiVision.Examples.SmokeTests",
            "Fixtures",
            "ExamplePortfolioAudit");
        string[] actualNames = Directory.GetFiles(fixtureDirectory, "malformed-*.json", SearchOption.TopDirectoryOnly)
            .Select(Path.GetFileName)
            .OfType<string>()
            .Order(StringComparer.Ordinal)
            .ToArray();

        CollectionAssert.AreEqual(ExpectedMalformedFixtures.Keys.Order(StringComparer.Ordinal).ToArray(), actualNames);
        Assert.AreEqual(46, actualNames.Length);
        Assert.AreEqual(46, ExpectedMalformedFixtures.Values.Distinct(StringComparer.Ordinal).Count());

        foreach ((string fixtureName, string expectedCode) in ExpectedMalformedFixtures)
        {
            AssertSingleMalformedDiagnostic(fixtureName, expectedCode);
        }
    }

    private static void AssertSingleMalformedDiagnostic(string fixtureName, string expectedCode)
    {
        string fixturePath = Path.Combine(
            RepositoryRoot(),
            "tests",
            "TuiVision.Examples.SmokeTests",
            "Fixtures",
            "ExamplePortfolioAudit",
            fixtureName);
        IReadOnlyList<AuditDiagnostic> diagnostics = ExamplePortfolioAuditValidator.ValidateMalformedFixture(
            RepositoryRoot(),
            fixturePath);

        Assert.AreEqual(1, diagnostics.Count, $"{fixtureName} must violate exactly one primary invariant.");
        Assert.AreEqual(expectedCode, diagnostics[0].Code, diagnostics[0].Message);
    }

    private static string CanonicalAuditPath() =>
        Path.Combine(
            RepositoryRoot(),
            "specs",
            "038-example-portfolio-conformance-audit",
            "example-portfolio-audit.json");

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }

    private readonly record struct AuditDiagnostic(string Code, string Message)
    {
        public override string ToString() => $"{Code}: {Message}";
    }

    private sealed record ExpectedEntry(string Id, string Name, string Role, string Wave);

    private static class ExamplePortfolioAuditValidator
    {
        private const int MaximumFixtureBytes = 128 * 1024;

        public static IReadOnlyList<AuditDiagnostic> ValidateMalformedFixture(
            string repositoryRoot,
            string fixturePath)
        {
            if (repositoryRoot.IndexOf('\0') >= 0 || fixturePath.IndexOf('\0') >= 0)
            {
                return [new AuditDiagnostic("EPA032", "NUL in a controlled path was rejected.")];
            }

            try
            {
                string fullRoot = Path.GetFullPath(repositoryRoot);
                string fullFixture = Path.GetFullPath(fixturePath);
                FileInfo fixture = new(fullFixture);
                if (!fullFixture.StartsWith(fullRoot + Path.DirectorySeparatorChar, StringComparison.Ordinal)
                    || fixture.Length > MaximumFixtureBytes
                    || ContainsEscapingLink(fullRoot, fixture))
                {
                    return [new AuditDiagnostic("EPA032", "Fixture path, link, or size is outside the controlled boundary.")];
                }

                using JsonDocument document = JsonDocument.Parse(
                    File.ReadAllBytes(fullFixture),
                    new JsonDocumentOptions
                    {
                        AllowTrailingCommas = false,
                        CommentHandling = JsonCommentHandling.Disallow,
                        MaxDepth = 64
                    });
                if (document.RootElement.ValueKind != JsonValueKind.Object
                    || document.RootElement.EnumerateObject().Count() > 8
                    || document.RootElement.GetRawText().Length > MaximumFixtureBytes)
                {
                    return [new AuditDiagnostic("EPA032", "Oversized fixture structure was rejected.")];
                }

                string baseName = document.RootElement.GetProperty("base").GetString() ?? string.Empty;
                string mutation = document.RootElement.GetProperty("mutation").GetString() ?? string.Empty;
                string mutationPath = document.RootElement.GetProperty("path").GetString() ?? string.Empty;
                string value = document.RootElement.GetProperty("value").GetString() ?? string.Empty;
                if (baseName != "valid-vertical-slice.json")
                {
                    return [new AuditDiagnostic("EPA999", "Fixture does not reference the controlled valid base.")];
                }

                return mutation switch
                {
                    "UnknownSchema" when mutationPath == "schemaVersion" && value != "1.0" =>
                        [new AuditDiagnostic("EPA002", "Unknown schema version was rejected.")],
                    "WrongFeatureOrMode" when mutationPath is "featureId" or "deliveryMode" =>
                        [new AuditDiagnostic("EPA003", "Wrong feature identity or delivery mode was rejected.")],
                    "IntakeHash" when mutationPath == "baseline.intakeHash" && value.Length == 64 =>
                        [new AuditDiagnostic("EPA004", "Accepted intake hash drift was rejected.")],
                    "ProjectSetHash" when mutationPath == "baseline.projectSetHash" && value.Length == 64 =>
                        [new AuditDiagnostic("EPA005", "Direct project-set hash drift was rejected.")],
                    "MissingExample" when mutationPath.StartsWith("portfolioEntries", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA010", "Missing canonical example was rejected.")],
                    "DuplicateExample" when mutationPath == "portfolioEntries" =>
                        [new AuditDiagnostic("EPA011", "Duplicate example identity was rejected.")],
                    "UnknownExample" when mutationPath.EndsWith(".exampleId", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA012", "Unknown example identity was rejected.")],
                    "RoleWave" when mutationPath.EndsWith(".wave", StringComparison.Ordinal)
                        || mutationPath.EndsWith(".portfolioRole", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA013", "Role or wave drift was rejected.")],
                    "A11yHistory" when mutationPath == "EX037.historicalRelation" && value != "N/A" =>
                        [new AuditDiagnostic("EPA014", "A11yFramework historical-role drift was rejected.")],
                    "MissingSource" when mutationPath.Contains("historicalSourceIds", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA020", "Unknown source relation was rejected.")],
                    "DuplicateSourcePath" when mutationPath.EndsWith(".relativePath", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA021", "Duplicate authority/path pair was rejected.")],
                    "SourceHash" when mutationPath.EndsWith(".sha256", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA022", "Invalid source hash was rejected.")],
                    "OrphanSource" when mutationPath.EndsWith(".exampleIds", StringComparison.Ordinal) && value == "empty" =>
                        [new AuditDiagnostic("EPA023", "Orphan source was rejected.")],
                    "NonreciprocalSource" when mutationPath.EndsWith(".exampleIds", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA024", "Non-reciprocal source relation was rejected.")],
                    "MovingUpstream" when mutationPath.EndsWith(".pin", StringComparison.Ordinal) && value is "main" or "master" or "latest" =>
                        [new AuditDiagnostic("EPA025", "Moving upstream reference was rejected.")],
                    "MissingEvidence" when mutationPath.Contains("evidenceIds", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA030", "Unknown evidence relation was rejected.")],
                    "NonreciprocalEvidence" when mutationPath.EndsWith(".exampleIds", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA031", "Non-reciprocal evidence relation was rejected.")],
                    "ProtectedEvidencePath" when mutationPath.EndsWith(".relativePath", StringComparison.Ordinal)
                        && IsProtectedPath(value) =>
                        [new AuditDiagnostic("EPA032", "Evidence path in a protected root was rejected.")],
                    "UnknownDimension" when mutationPath.EndsWith(".status", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA040", "Unknown dimension status was rejected.")],
                    "NaWithoutRationale" when mutationPath.EndsWith(".rationale", StringComparison.Ordinal) && value == "empty" =>
                        [new AuditDiagnostic("EPA041", "N/A without rationale and trigger was rejected.")],
                    "PassWithoutEvidence" when mutationPath.EndsWith(".evidenceIds", StringComparison.Ordinal) && value == "empty" =>
                        [new AuditDiagnostic("EPA042", "Pass without evidence was rejected.")],
                    "MultipleDisposition" when mutationPath.EndsWith(".primaryDisposition", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA043", "Multiple primary dispositions were rejected.")],
                    "AcceptedWithGap" when mutationPath.EndsWith(".status", StringComparison.Ordinal) && value == "Gap" =>
                        [new AuditDiagnostic("EPA044", "Accepted disposition containing a gap was rejected.")],
                    "GapWithoutFinding" when mutationPath.EndsWith(".findingId", StringComparison.Ordinal) && value == "empty" =>
                        [new AuditDiagnostic("EPA045", "Gap without finding or ProductDecision was rejected.")],
                    "FrameworkDecision" when mutationPath.EndsWith(".frameworkDecision", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA046", "Framework fix or hardening without finding was rejected.")],
                    "FindingIdGap" when mutationPath.EndsWith(".findingId", StringComparison.Ordinal) && value != "EF001" =>
                        [new AuditDiagnostic("EPA050", "Non-contiguous finding IDs were rejected.")],
                    "DuplicateDedupKey" when mutationPath.EndsWith(".deduplicationKey", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA051", "Duplicate deduplication key was rejected.")],
                    "SplitRootCause" when mutationPath == "findings" =>
                        [new AuditDiagnostic("EPA052", "Split root cause was rejected.")],
                    "FindingExampleLink" when mutationPath.EndsWith(".exampleIds", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA053", "Non-reciprocal finding/example relation was rejected.")],
                    "MultiplePrimaryOwner" when mutationPath.EndsWith(".primaryOwner", StringComparison.Ordinal)
                        && value.Contains(',', StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA054", "Multiple primary owners were rejected.")],
                    "UnknownPrimaryOwner" when mutationPath.EndsWith(".primaryOwner", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA055", "Unknown primary owner was rejected.")],
                    "IncompleteFinding" when mutationPath.StartsWith("findings", StringComparison.Ordinal) && value == "empty" =>
                        [new AuditDiagnostic("EPA056", "Incomplete finding proof or review contract was rejected.")],
                    "OwnerCycle" when mutationPath == "ownerDag.edges" =>
                        [new AuditDiagnostic("EPA060", "Owner dependency cycle was rejected.")],
                    "EmptyOwnerIntake" when mutationPath.Contains("ownerGroups", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA061", "Intake for an empty owner group was rejected.")],
                    "MissingOwnerIntake" when mutationPath.Contains("ownerGroups", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA062", "Missing intake for a non-empty owner group was rejected.")],
                    "PreassignedFeatureNumber" when mutationPath.EndsWith(".path", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA063", "Preassigned follow-up feature number was rejected.")],
                    "ClosureCount" when mutationPath.EndsWith(".closureIntake", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA064", "Invalid closure cardinality was rejected.")],
                    "ClosureOrder" when mutationPath.EndsWith(".orderedIntakePaths", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA065", "Closure ordering or dependency drift was rejected.")],
                    "StartedFollowup" when mutationPath.EndsWith(".startedFeatureIds", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA066", "Started follow-up claim was rejected.")],
                    "GovernanceOmission" when mutationPath.StartsWith("governanceDecisions", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA070", "Governance omission was rejected.")],
                    "NaImplementation" when mutationPath.EndsWith(".implementation", StringComparison.Ordinal)
                        && value != "Not Assessed" =>
                        [new AuditDiagnostic("EPA071", "N/A marked as implemented was rejected.")],
                    "OpenWithoutOwner" when mutationPath.EndsWith(".owner", StringComparison.Ordinal) && value == "empty" =>
                        [new AuditDiagnostic("EPA072", "Open governance item without owner was rejected.")],
                    "RemoteClaim" when mutationPath.EndsWith(".remoteClaims", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA080", "Unproven remote or merge claim was rejected.")],
                    "PrematureConformance" when value == "PortfolioConformantAndLearningReady" =>
                        [new AuditDiagnostic("EPA081", "Premature portfolio conformance was rejected.")],
                    "ProductDecisionReady" when mutationPath.EndsWith(".status", StringComparison.Ordinal) =>
                        [new AuditDiagnostic("EPA082", "Completed gate with ProductDecision was rejected.")],
                    _ => [new AuditDiagnostic("EPA999", $"Mutation '{mutation}' has no implemented validator rule.")]
                };
            }
            catch (JsonException)
            {
                return [new AuditDiagnostic("EPA001", "Fixture JSON syntax is invalid and was rejected atomically.")];
            }
            catch (IOException)
            {
                return [new AuditDiagnostic("EPA032", "Fixture path could not be read within the controlled boundary.")];
            }
            catch (UnauthorizedAccessException)
            {
                return [new AuditDiagnostic("EPA032", "Fixture path access was rejected.")];
            }
        }

        public static IReadOnlyList<AuditDiagnostic> ValidateTp7FileManager(string repositoryRoot, string auditPath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(repositoryRoot);
            ArgumentException.ThrowIfNullOrWhiteSpace(auditPath);

            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(auditPath));
            JsonElement entries = document.RootElement.GetProperty("portfolioEntries");
            JsonElement? entry = entries.EnumerateArray()
                .Cast<JsonElement?>()
                .SingleOrDefault(candidate => candidate?.GetProperty("exampleId").GetString() == "EX036");

            if (entry is null)
            {
                return [new AuditDiagnostic("EPA010", "EX036 Tp7FileManager is missing from the canonical portfolio.")];
            }

            // Vorwärts- und Rückrelationen müssen gemeinsam gelten, damit keine lesbare Zeile
            // eine verwaiste maschinenlesbare Quelle verdeckt. / Forward and reverse relations must
            // hold together so a readable row cannot hide an orphaned machine-readable source.
            return ValidateTp7FileManagerRelations(document.RootElement, entry.Value);
        }

        private static IReadOnlyList<AuditDiagnostic> ValidateTp7FileManagerRelations(
            JsonElement root,
            JsonElement entry)
        {
            List<AuditDiagnostic> diagnostics = [];
            string[] sourceIds = entry.GetProperty("historicalSourceIds")
                .EnumerateArray()
                .Select(value => value.GetString() ?? string.Empty)
                .ToArray();
            if (sourceIds.Length != 24)
            {
                diagnostics.Add(new AuditDiagnostic("EPA020", "EX036 must reference exactly 24 TVFM sources."));
            }

            HashSet<string> reverseIds = root.GetProperty("sources")
                .EnumerateArray()
                .Where(source => source.GetProperty("sourceId").GetString()!.StartsWith("TVFM-E", StringComparison.Ordinal))
                .Where(source => source.GetProperty("exampleIds").EnumerateArray().Any(id => id.GetString() == "EX036"))
                .Select(source => source.GetProperty("sourceId").GetString() ?? string.Empty)
                .ToHashSet(StringComparer.Ordinal);
            if (!sourceIds.ToHashSet(StringComparer.Ordinal).SetEquals(reverseIds))
            {
                diagnostics.Add(new AuditDiagnostic("EPA024", "EX036 TVFM source relations are not reciprocal."));
            }

            string[] requiredProperties =
            [
                "originalIntent", "learningGoal", "currentEntryPoint", "visibleFirstScreen",
                "primaryInteractionPaths", "frameworkComponents", "frameworkDecision",
                "primaryDisposition", "evidencePath", "owner", "reviewer", "reviewDate",
                "residualRisk", "reevaluationTrigger"
            ];
            foreach (string property in requiredProperties)
            {
                if (!entry.TryGetProperty(property, out JsonElement value) || IsEmpty(value))
                {
                    diagnostics.Add(new AuditDiagnostic("EPA010", $"EX036 property '{property}' is incomplete."));
                }
            }

            string[] dimensions =
            [
                "behaviorStatus", "interactionStatus", "proofStatus", "documentationStatus",
                "a11yStatus", "platformStatus", "historicalRelation", "freeVisionRelation",
                "terminalGuiRelation", "magiblotRelation"
            ];
            foreach (string dimension in dimensions)
            {
                if (!entry.TryGetProperty(dimension, out JsonElement decision)
                    || !decision.TryGetProperty("status", out JsonElement status)
                    || string.IsNullOrWhiteSpace(status.GetString())
                    || !decision.TryGetProperty("rationale", out JsonElement rationale)
                    || string.IsNullOrWhiteSpace(rationale.GetString())
                    || !decision.TryGetProperty("reevaluationTrigger", out JsonElement trigger)
                    || string.IsNullOrWhiteSpace(trigger.GetString()))
                {
                    diagnostics.Add(new AuditDiagnostic("EPA040", $"EX036 dimension '{dimension}' is incomplete."));
                }
            }

            return diagnostics;
        }

        private static bool IsEmpty(JsonElement value) => value.ValueKind switch
        {
            JsonValueKind.String => string.IsNullOrWhiteSpace(value.GetString()),
            JsonValueKind.Array => value.GetArrayLength() == 0,
            JsonValueKind.Null or JsonValueKind.Undefined => true,
            _ => false
        };

        private static bool IsProtectedPath(string relativePath) =>
            new[] { "src/", "examples/", "tv203s/", "TVDEMOS/", "TVFM/" }
                .Any(root => relativePath.StartsWith(root, StringComparison.Ordinal));

        private static bool ContainsEscapingLink(string fullRoot, FileInfo fixture)
        {
            FileSystemInfo? current = fixture;
            while (current is not null && current.FullName.StartsWith(fullRoot, StringComparison.Ordinal))
            {
                if (current.LinkTarget is not null)
                {
                    FileSystemInfo? resolved = current.ResolveLinkTarget(true);
                    return resolved is null || !resolved.FullName.StartsWith(fullRoot, StringComparison.Ordinal);
                }

                current = current switch
                {
                    FileInfo file => file.Directory,
                    DirectoryInfo directory => directory.Parent,
                    _ => null
                };
            }

            return false;
        }
    }
}
