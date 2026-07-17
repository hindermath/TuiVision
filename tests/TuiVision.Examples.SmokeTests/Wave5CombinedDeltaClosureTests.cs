// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft den geschlossenen Wave-5-Delta und seine fail-closed Grenzen.
///
/// Verifies the closed Wave-5 delta and its fail-closed boundaries.
/// </summary>
[TestClass]
public sealed class Wave5CombinedDeltaClosureTests
{
    private static readonly string[] ExpectedExamples =
    [
        "Tp7Demo",
        "Tp7Edit",
        "Tp7Help",
        "Tp7ResourceDemo",
        "Tp7ResourceGenerator",
        "Tp7AsciiTable",
        "Tp7Calculator",
        "Tp7Calendar",
        "Tp7Puzzle",
        "Tp7MouseDialog"
    ];

    private static readonly string[] ExpectedConsumers =
        ["W5-001", "W5-002", "W5-003", "W5-004", "W5-005", "W5-006"];

    private static readonly string[] ExpectedSources =
    [
        "TVDEMOS/TVDEMO.PAS",
        "TVDEMOS/DEMOCMDS.PAS",
        "TVDEMOS/DEMOSTRS.PAS",
        "TVDEMOS/GADGETS.PAS",
        "TVDEMOS/TVEDIT.PAS",
        "TVDEMOS/TVHC.PAS",
        "TVDEMOS/HELPFILE.PAS",
        "TVDEMOS/DEMOHELP.PAS",
        "TVDEMOS/TVRDEMO.PAS",
        "TVDEMOS/GENRDEMO.PAS",
        "TVDEMOS/ASCIITAB.PAS",
        "TVDEMOS/CALC.PAS",
        "TVDEMOS/CALENDAR.PAS",
        "TVDEMOS/PUZZLE.PAS",
        "TVDEMOS/MOUSEDLG.PAS"
    ];

    private static readonly string[] AllowedDimensions =
        ["Pass", "IntentionalDeviation", "Gap", "N/A"];

    private static readonly string[] AllowedDecisions =
        ["AcceptedAsIs", "AcceptedIntentionalDeviation", "CandidateFinding", "ProductDecision"];

    private static readonly string[] ExpectedDimensionNames =
        ["behavior", "interaction", "layout", "proof", "documentation", "a11y", "platform", "security", "frameworkReuse"];

    private static readonly Dictionary<int, (string Base, string Head, string Merge, string Role, int Files, string FileSetHash)>
        ExpectedDeltas = new()
        {
            [93] = ("269c54f5f882c69e21f46f97d3e89a938bfb568f", "cf274c61968fdc5422d3c1cf16ed5488ad5d37ad", "e74c33d256ebbf2cf8e6a78f2548ee6e3f6cf3d6", "FunctionalProduct", 77, "359196f832566eb58d16c2da5ce1d9586c19f2268ada6cf9815cfcf9ed5a13a0"),
            [94] = ("e74c33d256ebbf2cf8e6a78f2548ee6e3f6cf3d6", "4cf4554676f7b04a50acfcf33d981798f741c687", "355f8ec6ffcb7e85373348df42eeab5134472bd5", "FunctionalCloseout", 6, "d0988de3cdb9157dd81c41e07d4ad541b40639aef68022497b84ba9f6b799083"),
            [95] = ("355f8ec6ffcb7e85373348df42eeab5134472bd5", "c0b021708bfb545b3fe85ddd62dc89e60557b14c", "5df2ec3cef5dd0e534abe630a8d472e0e5bc0236", "PromptMetadata", 6, "0fb7cd7ee2bbf65430af28a01f2cbba8277eea773a2bde83e84a4eb392489254"),
            [96] = ("5df2ec3cef5dd0e534abe630a8d472e0e5bc0236", "8921bd3f9e354b38835528442f950f53c9d925f0", "d476e63ccfc053a9a2be1a51eb6d43a875c57384", "ShowcaseProduct", 50, "d43daa4b9ecb8c90f844fa4661d185dd3abc5d862162578b409f8cd55befff85"),
            [97] = ("d476e63ccfc053a9a2be1a51eb6d43a875c57384", "0481f46aa41d09f2ffcf79c6b3946cc860222251", "1e9976568613b9657654668b4f764a145e1204b3", "ShowcaseCloseout", 12, "c446663546a51ba6df21612e82e699e8397cb2bda5a4787cfc3839040ee46288")
        };

    private static readonly Dictionary<string, string> ExpectedInputHashes = new(StringComparer.Ordinal)
    {
        ["specs/032-wave5-tp7-functional-porting/pr-evidence.md"] = "97f84e968ef7717b507baef6737053aea3b8193b2f6183769a1718cd7e52b258",
        ["specs/032-wave5-tp7-functional-porting/delivery-closeout.md"] = "eec79e1aaee4bd960c18a62c6f509e91541e7c5cb24d6e4cb36902698ae4558f",
        ["specs/032-wave5-tp7-functional-porting/retrospective.md"] = "199933d44e0f2ada06b3497cf7f8db9cf0cfffb49bf8b7c324f61a2544d1328d",
        ["specs/033-wave5-tp7-showcase-remediation/pr-evidence.md"] = "46539e658964f1e6b5ca5ba86b35069cfc046f80c14a29fbf3342ebe8c5261a6",
        ["specs/033-wave5-tp7-showcase-remediation/delivery-closeout.md"] = "42e63501a4244e25697b5c44b4fe601cdb2d4dc09e709d9ad2aa3d6ac14e7178",
        ["specs/033-wave5-tp7-showcase-remediation/retrospective.md"] = "c75caf7948ffb4c08009d7527be55aeb2fd6e63ff3e694256f44baf9411b95d8",
        ["specs/033-wave5-tp7-showcase-remediation/plan.md"] = "fe016cd8f6acfb90ef78490412cdd654c988772bf94229394dfff8cd197306c8"
    };

    private static readonly Dictionary<string, string> ExpectedSourceBlobs = new(StringComparer.Ordinal)
    {
        ["TVDEMOS/TVDEMO.PAS"] = "9aa23c875edb1fd6c219c26cfe7d4b0b2dfcad89",
        ["TVDEMOS/DEMOCMDS.PAS"] = "daf053e9ef1072347cab9888fa76bd8df44cee25",
        ["TVDEMOS/DEMOSTRS.PAS"] = "e1abf0fb9206f9bb053793f960c0411c6ce05c09",
        ["TVDEMOS/GADGETS.PAS"] = "26314f38406cfbc5ff3bd0a9cea27f28c43b54ea",
        ["TVDEMOS/TVEDIT.PAS"] = "56279ad1b73dfc9e9fe7b2fd48f169983531d784",
        ["TVDEMOS/TVHC.PAS"] = "a5bbbf1bd40ebf9790636f2b6f8b0dda2195cfd8",
        ["TVDEMOS/HELPFILE.PAS"] = "de7912376481c34666222e31b67a225eaac37db1",
        ["TVDEMOS/DEMOHELP.PAS"] = "013dd90709574262ff106484787d9ad671792e40",
        ["TVDEMOS/TVRDEMO.PAS"] = "a5e2bd30328c22c7595db2b0c2c4002ac99e81ba",
        ["TVDEMOS/GENRDEMO.PAS"] = "2ee61b1619ccae47da985b28ada778a22e802a23",
        ["TVDEMOS/ASCIITAB.PAS"] = "2b22640533e9454212633cead224d6d1e5014109",
        ["TVDEMOS/CALC.PAS"] = "d4d3565deea4ab2b77a2f5467744b6f624e9d51b",
        ["TVDEMOS/CALENDAR.PAS"] = "3667ae873c6ce3a99699ecf03922d48ecfb0b0ca",
        ["TVDEMOS/PUZZLE.PAS"] = "30da629232e72575a8213c4c8e5b3ef718ea920a",
        ["TVDEMOS/MOUSEDLG.PAS"] = "5929db027c8a7de1213ab9f07b626ece9af5f393"
    };

    /// <summary>
    /// Prüft exakte PR-Dateimengen, Eingabehashes, Quellen und Consumer.
    ///
    /// Verifies exact PR file sets, input hashes, sources, and consumers.
    /// </summary>
    [TestMethod]
    public void Test_ProvenanceSourcesAndConsumersAreExact()
    {
        JsonObject root = LoadEvidence();
        ValidateProductDeltas(root);
        ValidateAcceptedInputs(root);
        ValidateSourcesAndConsumers(root);
    }

    /// <summary>
    /// Prüft genau zehn vollständige Funktions-, Showcase-, Guide- und Beispielzeilen.
    ///
    /// Verifies exactly ten complete function, showcase, guide, and example rows.
    /// </summary>
    [TestMethod]
    public void Test_CombinedExampleMatrixIsComplete()
    {
        ValidateCombinedRows(LoadEvidence());
    }

    /// <summary>
    /// Prüft Framework-Ownership, Proof-Rollen und Governance-Metadaten.
    ///
    /// Verifies framework ownership, proof roles, and governance metadata.
    /// </summary>
    [TestMethod]
    public void Test_FrameworkProofAndGovernanceAreComplete()
    {
        JsonObject root = LoadEvidence();
        ValidateFrameworkAreas(root);
        ValidateGovernance(root);
    }

    /// <summary>
    /// Prüft Finding-, Product-Decision- und kausale Wave-Grenzen.
    ///
    /// Verifies finding, product-decision, and causal Wave boundaries.
    /// </summary>
    [TestMethod]
    public void Test_FindingsAndWaveTransitionAreFailClosed()
    {
        JsonObject root = LoadEvidence();
        ValidateFindings(root);
        ValidateWaveTransition(root, ReadCloseoutIfPresent());
    }

    /// <summary>
    /// Prüft fehlende, doppelte und unbekannte Matrixdaten.
    ///
    /// Verifies missing, duplicate, and unknown matrix data.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsCardinalityAndDecisionMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject missing = Clone(valid);
        Array(missing, "examples").RemoveAt(0);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(missing));

        JsonObject duplicate = Clone(valid);
        Array(duplicate, "examples").Add(Array(duplicate, "examples")[0]!.DeepClone());
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(duplicate));

        JsonObject unknownDecision = Clone(valid);
        Object(Array(unknownDecision, "examples")[0])["primaryDecision"] = "UnknownDecision";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(unknownDecision));

        JsonObject acceptedGap = Clone(valid);
        Object(Object(Array(acceptedGap, "examples")[0])["dimensions"])["proof"] = "Gap";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(acceptedGap));
    }

    /// <summary>
    /// Prüft unabhängige Pins gegen gemeinsam veränderte Daten und Selbsthashes.
    ///
    /// Verifies independent pins against jointly changed data and self-hashes.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsProvenanceMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject prPin = Clone(valid);
        Object(Array(prPin, "productDeltas")[0])["headCommit"] = new string('0', 40);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateProductDeltas(prPin));

        JsonObject fileSet = Clone(valid);
        JsonObject delta = Object(Array(fileSet, "productDeltas")[0]);
        Array(delta, "changedFiles").RemoveAt(0);
        delta["changedFileCount"] = Array(delta, "changedFiles").Count;
        delta["changedFileSetSha256"] = HashLines(Array(delta, "changedFiles").Select(Text));
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateProductDeltas(fileSet));

        JsonObject input = Clone(valid);
        Object(Array(input, "acceptedInputs")[0])["sha256"] = new string('0', 64);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateAcceptedInputs(input));

        JsonObject source = Clone(valid);
        Object(Array(source, "historicalSources")[0])["gitBlob"] = new string('0', 40);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateSourcesAndConsumers(source));

        JsonObject duplicateSource = Clone(valid);
        Array(duplicateSource, "historicalSources").Add(Array(duplicateSource, "historicalSources")[0]!.DeepClone());
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateSourcesAndConsumers(duplicateSource));

        JsonObject duplicateConsumer = Clone(valid);
        Array(duplicateConsumer, "consumers").Add(Array(duplicateConsumer, "consumers")[0]!.DeepClone());
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateSourcesAndConsumers(duplicateConsumer));
    }

    /// <summary>
    /// Prüft unvollständige App-Loop-, Guide-, F1-, Quit-, Cell- und Dimensionsnachweise.
    ///
    /// Verifies incomplete app-loop, guide, F1, quit, cell, and dimension proof.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsIncompleteProofMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject appLoop = Clone(valid);
        Object(Array(appLoop, "functionalProofs")[0])["appLoop"] = "Direct helper only.";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(appLoop));

        JsonObject cell = Clone(valid);
        Object(Array(cell, "showcaseClosures")[0])["cellProof"] = "";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(cell));

        JsonObject guide = Clone(valid);
        Object(Array(guide, "guideLaunchPaths")[0])["normalCommand"] = "dotnet run --project examples/Tp7Demo";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(guide));

        JsonObject f1 = Clone(valid);
        Object(Array(f1, "examples")[0])["descriptionPath"] = "Help menu only";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(f1));

        JsonObject quit = Clone(valid);
        Object(Array(quit, "examples")[0])["exitPath"] = "Alt+X";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(quit));

        JsonObject dimension = Clone(valid);
        JsonObject dimensions = Object(Object(Array(dimension, "examples")[0])["dimensions"]);
        dimensions.Remove("security");
        dimensions["unknown"] = "Pass";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedRows(dimension));
    }

    /// <summary>
    /// Prüft Helper-Ownership und vollständige triggerbasierte N/A-Einträge.
    ///
    /// Verifies helper ownership and complete trigger-based N/A entries.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsFrameworkAndGovernanceMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject framework = Clone(valid);
        Object(Array(framework, "frameworkAreas")[0])["decision"] = "FrameworkReplacement";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateFrameworkAreas(framework));

        JsonObject reusable = Clone(valid);
        Object(Array(reusable, "frameworkAreas")[0])["reusableOutsideWave5"] = true;
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateFrameworkAreas(reusable));

        JsonObject governance = Clone(valid);
        JsonObject security = Object(Array(governance, "governance")[0]);
        Object(Array(security, "notApplicable")[0])["reevaluationTrigger"] = "";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateGovernance(governance));
    }

    /// <summary>
    /// Prüft unvollständige Findings, Product Decisions und verfrühte Freigaben.
    ///
    /// Verifies incomplete findings, product decisions, and premature release.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsFindingAndWaveMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject finding = Clone(valid);
        Array(finding, "candidateFindings").Add(new JsonObject
        {
            ["findingId"] = "W5D001",
            ["exampleIds"] = new JsonArray("Tp7Calculator"),
            ["evidencePaths"] = new JsonArray(),
            ["owner"] = "",
            ["reproduction"] = "Missing owner and evidence.",
            ["followUpBoundary"] = "Outside Feature 034.",
            ["reevaluationTrigger"] = "Always."
        });
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateFindings(finding));

        JsonObject productDecision = Clone(valid);
        Array(productDecision, "productDecisions").Add("PD001");
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateFindings(productDecision));

        JsonObject premature = Clone(valid);
        JsonObject transition = Object(premature["waveTransition"]);
        transition["currentWave5State"] = "Closed";
        transition["currentWave6State"] = "EligibleForIntake";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateWaveTransition(premature, closeoutText: null));

        const string completeCloseout =
            "Reviewed feature head: abc\nFeature merge: def\nWave 5: `Closed`\nWave 6: `EligibleForIntake`";
        ValidateWaveTransition(premature, completeCloseout);

        JsonObject started = Clone(valid);
        Object(started["waveTransition"])["featureStarted"] = true;
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateWaveTransition(started, closeoutText: null));

        JsonObject ownerGroup = Clone(valid);
        Array(ownerGroup, "ownerGroups").Add("ExampleOwner");
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateFindings(ownerGroup));

        JsonObject hardeningIntake = Clone(valid);
        Array(hardeningIntake, "hardeningIntakes").Add("Lastenheft_empty.md");
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateFindings(hardeningIntake));

        JsonObject incompleteCloseout = Clone(premature);
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateWaveTransition(incompleteCloseout, "Wave 5: `Closed`\nWave 6: `EligibleForIntake`"));
    }

    /// <summary>
    /// Prüft identische strukturierte Auswertung für LF und CRLF.
    ///
    /// Verifies identical structured evaluation for LF and CRLF.
    /// </summary>
    [TestMethod]
    public void Test_LineEndingsProduceTheSameResult()
    {
        string json = File.ReadAllText(EvidencePath());
        string lf = NormalizeLineEndings(json);
        string crlf = lf.Replace("\n", "\r\n", StringComparison.Ordinal);

        ValidateCombinedRows(Parse(lf));
        ValidateCombinedRows(Parse(crlf));
    }

    /// <summary>
    /// Prüft plattformneutrale Hashes für textuelle Vorgänger-Evidence.
    ///
    /// Verifies platform-neutral hashes for textual predecessor evidence.
    /// </summary>
    [TestMethod]
    public void Test_AcceptedInputHashesAreLineEndingNeutral()
    {
        const string lf = "erste Zeile\nsecond line\n";
        string crlf = lf.Replace("\n", "\r\n", StringComparison.Ordinal);

        Assert.AreEqual(CanonicalTextSha256(lf), CanonicalTextSha256(crlf));
    }

    private static void ValidateProductDeltas(JsonObject root)
    {
        JsonArray deltas = Array(root, "productDeltas");
        RequireExactIds(deltas, [93, 94, 95, 96, 97], row => Integer(row, "prNumber"), "PR");

        foreach (JsonObject delta in deltas.Select(Object))
        {
            int pr = Integer(delta, "prNumber");
            var expected = ExpectedDeltas[pr];
            RequireEqual(expected.Role, Text(delta, "role"), $"PR #{pr} role");
            RequireEqual(expected.Base, Text(delta, "baseCommit"), $"PR #{pr} base");
            RequireEqual(expected.Head, Text(delta, "headCommit"), $"PR #{pr} head");
            RequireEqual(expected.Merge, Text(delta, "mergeCommit"), $"PR #{pr} merge");
            RequireEqual(expected.Files, Integer(delta, "changedFileCount"), $"PR #{pr} changed-file count");
            RequireEqual(expected.FileSetHash, Text(delta, "changedFileSetSha256"), $"PR #{pr} changed-file-set hash");
            RequireGitId(delta, "baseCommit");
            RequireGitId(delta, "headCommit");
            RequireGitId(delta, "mergeCommit");

            string[] paths = Array(delta, "changedFiles").Select(Text).ToArray();
            if (paths.Length != Integer(delta, "changedFileCount")
                || paths.Distinct(StringComparer.Ordinal).Count() != paths.Length
                || !paths.SequenceEqual(paths.Order(StringComparer.Ordinal), StringComparer.Ordinal)
                || HashLines(paths) != Text(delta, "changedFileSetSha256"))
            {
                throw new InvalidDataException($"PR #{pr} changed-file set is incomplete, duplicated, unsorted, or drifted.");
            }
        }
    }

    private static void ValidateAcceptedInputs(JsonObject root)
    {
        string repositoryRoot = RepositoryRoot();
        JsonArray inputs = Array(root, "acceptedInputs");
        RequireExactIds(inputs, ExpectedInputHashes.Keys, row => Text(row, "path"), "accepted input");
        if (inputs.Count != 7)
        {
            throw new InvalidDataException("Exactly seven accepted predecessor inputs are required.");
        }

        foreach (JsonObject input in inputs.Select(Object))
        {
            string relativePath = Text(input, "path");
            string path = Path.Combine(repositoryRoot, relativePath);
            if (!File.Exists(path)
                || Text(input, "sha256") != ExpectedInputHashes[relativePath]
                || CanonicalTextSha256(File.ReadAllText(path)) != ExpectedInputHashes[relativePath]
                || string.IsNullOrWhiteSpace(Text(input, "role"))
                || string.IsNullOrWhiteSpace(Text(input, "reevaluationTrigger")))
            {
                throw new InvalidDataException($"Accepted input '{relativePath}' is missing, drifted, or incomplete.");
            }
        }
    }

    private static void ValidateSourcesAndConsumers(JsonObject root)
    {
        JsonArray sources = Array(root, "historicalSources");
        RequireExactIds(sources, ExpectedSources, row => Text(row, "path"), "source");

        string repositoryRoot = RepositoryRoot();
        foreach (JsonObject source in sources.Select(Object))
        {
            string path = Text(source, "path");
            string fullPath = Path.Combine(repositoryRoot, path);
            if (!File.Exists(fullPath)
                || Text(source, "gitBlob") != ExpectedSourceBlobs[path]
                || GitBlobId(File.ReadAllBytes(fullPath)) != ExpectedSourceBlobs[path]
                || Array(source, "consumerIds").Count == 0
                || Array(source, "exampleIds").Count == 0)
            {
                throw new InvalidDataException($"Historical source '{path}' is missing, drifted, or unowned.");
            }
        }

        JsonArray consumers = Array(root, "consumers");
        RequireExactIds(consumers, ExpectedConsumers, row => Text(row, "consumerId"), "consumer");
        HashSet<string> sourceSet = ExpectedSources.ToHashSet(StringComparer.Ordinal);
        HashSet<string> exampleSet = ExpectedExamples.ToHashSet(StringComparer.Ordinal);
        foreach (JsonObject consumer in consumers.Select(Object))
        {
            if (Text(consumer, "frameworkDecision") != "UseExistingFramework"
                || Array(consumer, "sourcePaths").Select(Text).Any(path => !sourceSet.Contains(path))
                || Array(consumer, "exampleIds").Select(Text).Any(id => !exampleSet.Contains(id))
                || string.IsNullOrWhiteSpace(Text(consumer, "frameworkContracts"))
                || string.IsNullOrWhiteSpace(Text(consumer, "reevaluationTrigger")))
            {
                throw new InvalidDataException($"Consumer '{Text(consumer, "consumerId")}' is incomplete.");
            }
        }
    }

    private static void ValidateCombinedRows(JsonObject root)
    {
        JsonArray functional = Array(root, "functionalProofs");
        JsonArray showcase = Array(root, "showcaseClosures");
        JsonArray guides = Array(root, "guideLaunchPaths");
        JsonArray examples = Array(root, "examples");

        RequireExactIds(functional, ExpectedExamples, row => Text(row, "exampleId"), "functional proof");
        RequireExactIds(showcase, ExpectedExamples, row => Text(row, "exampleId"), "showcase closure");
        RequireExactIds(guides, ExpectedExamples, row => Text(row, "exampleId"), "guide/launch");
        RequireExactIds(examples, ExpectedExamples, row => Text(row, "exampleId"), "combined example");

        string repositoryRoot = RepositoryRoot();
        foreach (JsonObject row in examples.Select(Object))
        {
            string exampleId = Text(row, "exampleId");
            string decision = Text(row, "primaryDecision");
            JsonObject dimensions = Object(row["dimensions"]);
            if (!AllowedDecisions.Contains(decision, StringComparer.Ordinal)
                || dimensions.Count != 9
                || !dimensions.Select(pair => pair.Key).Order(StringComparer.Ordinal)
                    .SequenceEqual(ExpectedDimensionNames.Order(StringComparer.Ordinal), StringComparer.Ordinal)
                || dimensions.Any(pair => pair.Value is null
                    || !AllowedDimensions.Contains(Text(pair.Value), StringComparer.Ordinal))
                || (decision is "AcceptedAsIs" or "AcceptedIntentionalDeviation"
                    && dimensions.Any(pair => Text(pair.Value) == "Gap"))
                || Array(row, "sourcePaths").Count == 0
                || Array(row, "frameworkComponents").Count == 0
                || Array(row, "evidencePaths").Count == 0
                || Text(row, "proofRole") != "PrimaryProof"
                || string.IsNullOrWhiteSpace(Text(row, "primaryOperation"))
                || string.IsNullOrWhiteSpace(Text(row, "statusPath"))
                || !Text(row, "descriptionPath").Contains("F1", StringComparison.Ordinal)
                || !Text(row, "exitPath").Contains("Ctrl+Q", StringComparison.Ordinal)
                || string.IsNullOrWhiteSpace(Text(row, "decisionRationale"))
                || string.IsNullOrWhiteSpace(Text(row, "residualRisk"))
                || string.IsNullOrWhiteSpace(Text(row, "reevaluationTrigger")))
            {
                throw new InvalidDataException($"Combined example '{exampleId}' is incomplete or contradictory.");
            }

            JsonObject functionalRow = functional.Select(Object).Single(candidate => Text(candidate, "exampleId") == exampleId);
            JsonObject showcaseRow = showcase.Select(Object).Single(candidate => Text(candidate, "exampleId") == exampleId);
            JsonObject guideRow = guides.Select(Object).Single(candidate => Text(candidate, "exampleId") == exampleId);
            JsonObject consumer = Array(root, "consumers").Select(Object)
                .Single(candidate => Text(candidate, "consumerId") == Text(row, "consumerId"));
            RequireEqual(Text(row, "functionalProofId"), Text(functionalRow, "proofId"), $"{exampleId} functional reference");
            RequireEqual(Text(row, "showcaseId"), Text(showcaseRow, "showcaseId"), $"{exampleId} showcase reference");
            string[] rowSources = Array(row, "sourcePaths").Select(Text).Order(StringComparer.Ordinal).ToArray();
            string[] reciprocalSources = Array(consumer, "sourcePaths").Select(Text)
                .Where(path => Array(root, "historicalSources").Select(Object)
                    .Any(source => Text(source, "path") == path
                        && Array(source, "exampleIds").Select(Text).Contains(exampleId, StringComparer.Ordinal)))
                .Order(StringComparer.Ordinal)
                .ToArray();
            if (!rowSources.SequenceEqual(reciprocalSources, StringComparer.Ordinal)
                || !Array(consumer, "exampleIds").Select(Text).Contains(exampleId, StringComparer.Ordinal))
            {
                throw new InvalidDataException($"{exampleId} has an incomplete reciprocal source or consumer set.");
            }

            foreach (string pathField in new[] { "projectPath", "guidePath" })
            {
                string path = Text(guideRow, pathField);
                if (!File.Exists(Path.Combine(repositoryRoot, path)))
                {
                    throw new InvalidDataException($"{exampleId} {pathField} is missing.");
                }
            }

            if (!Text(functionalRow, "appLoop").Contains("app.Run()", StringComparison.Ordinal)
                || Text(functionalRow, "proofRole") != "PrimaryProof"
                || string.IsNullOrWhiteSpace(Text(functionalRow, "cellProof"))
                || !Text(showcaseRow, "descriptionProof").Contains("F1", StringComparison.Ordinal)
                || string.IsNullOrWhiteSpace(Text(showcaseRow, "cellProof"))
                || !Text(guideRow, "normalCommand").Contains("--no-build", StringComparison.Ordinal)
                || !Text(guideRow, "smokeCommand").Contains("--smoke", StringComparison.Ordinal)
                || Text(guideRow, "exitPath") != "Ctrl+Q")
            {
                throw new InvalidDataException($"{exampleId} lacks a real app-loop, Description, or controlled launch proof.");
            }
        }
    }

    private static void ValidateFrameworkAreas(JsonObject root)
    {
        string[] expected = ["Wave5Application", "Wave5ConsoleHost", "Wave5StatusLine", "Wave5GridView"];
        JsonArray areas = Array(root, "frameworkAreas");
        RequireExactIds(areas, expected, row => Text(row, "areaId"), "framework area");
        foreach (JsonObject area in areas.Select(Object))
        {
            if (Text(area, "decision") != "ExampleComposition"
                || Boolean(area, "reusableOutsideWave5")
                || Text(area, "findingId") != "None"
                || string.IsNullOrWhiteSpace(Text(area, "rationale")))
            {
                throw new InvalidDataException($"Framework area '{Text(area, "areaId")}' is not explicitly bounded.");
            }
        }
    }

    private static void ValidateGovernance(JsonObject root)
    {
        string[] expected =
        [
            "security-governance",
            "architecture-governance",
            "isaqb-architecture-governance",
            "a11y-governance",
            "cross-platform-governance",
            "agent-parity-governance",
            "autonomous-run-governance"
        ];
        JsonArray rows = Array(root, "governance");
        RequireExactIds(rows, expected, row => Text(row, "presetName"), "governance preset");
        foreach (JsonObject row in rows.Select(Object))
        {
            if (string.IsNullOrWhiteSpace(Text(row, "presetVersion"))
                || string.IsNullOrWhiteSpace(Text(row, "checkpoint"))
                || string.IsNullOrWhiteSpace(Text(row, "rationale"))
                || string.IsNullOrWhiteSpace(Text(row, "evidencePath"))
                || string.IsNullOrWhiteSpace(Text(row, "owner"))
                || string.IsNullOrWhiteSpace(Text(row, "reviewer"))
                || string.IsNullOrWhiteSpace(Text(row, "reviewDate"))
                || string.IsNullOrWhiteSpace(Text(row, "result"))
                || string.IsNullOrWhiteSpace(Text(row, "residualRisk"))
                || string.IsNullOrWhiteSpace(Text(row, "followUp"))
                || string.IsNullOrWhiteSpace(Text(row, "reevaluationTrigger")))
            {
                throw new InvalidDataException($"Governance row '{Text(row, "presetName")}' is incomplete.");
            }

            if (row["notApplicable"] is JsonArray notApplicable
                && notApplicable.Select(Object).Any(entry =>
                    string.IsNullOrWhiteSpace(Text(entry, "checkpoints"))
                    || string.IsNullOrWhiteSpace(Text(entry, "rationale"))
                    || string.IsNullOrWhiteSpace(Text(entry, "reevaluationTrigger"))))
            {
                throw new InvalidDataException(
                    $"Governance row '{Text(row, "presetName")}' has an incomplete N/A decision.");
            }
        }
    }

    private static void ValidateFindings(JsonObject root)
    {
        JsonArray findings = Array(root, "candidateFindings");
        string[] findingIds = findings.Select(Object).Select(finding => Text(finding, "findingId")).ToArray();
        if (findingIds.Distinct(StringComparer.Ordinal).Count() != findingIds.Length)
        {
            throw new InvalidDataException("Candidate Finding IDs must be unique.");
        }

        foreach (JsonObject finding in findings.Select(Object))
        {
            string findingId = Text(finding, "findingId");
            if (findingId.Length != 6
                || !findingId.StartsWith("W5D", StringComparison.Ordinal)
                || !int.TryParse(findingId.AsSpan(3), out int findingNumber)
                || findingNumber <= 0
                || Array(finding, "exampleIds").Count == 0
                || Array(finding, "evidencePaths").Count == 0
                || string.IsNullOrWhiteSpace(Text(finding, "owner"))
                || string.IsNullOrWhiteSpace(Text(finding, "reproduction"))
                || string.IsNullOrWhiteSpace(Text(finding, "followUpBoundary"))
                || string.IsNullOrWhiteSpace(Text(finding, "reevaluationTrigger")))
            {
                throw new InvalidDataException("A Candidate Finding lacks stable identity, reproduction, evidence, owner, or follow-up.");
            }
        }

        if (Array(root, "productDecisions").Count != 0
            || Array(root, "ownerGroups").Count != 0
            || Array(root, "hardeningIntakes").Count != 0)
        {
            throw new InvalidDataException("A product decision or remediation intake blocks the accepted clean closure.");
        }
    }

    private static void ValidateWaveTransition(JsonObject root, string? closeoutText)
    {
        JsonObject transition = Object(root["waveTransition"]);
        bool hasCloseout = !string.IsNullOrWhiteSpace(closeoutText);
        string expectedWave5 = hasCloseout ? "Closed" : "BlockedPendingCausalClosure";
        string expectedWave6 = hasCloseout ? "EligibleForIntake" : "BlockedPendingCausalClosure";
        RequireEqual(expectedWave5, Text(transition, "currentWave5State"), "Wave 5 current state");
        RequireEqual(expectedWave6, Text(transition, "currentWave6State"), "Wave 6 current state");

        if (Text(transition, "postMergeWave5Target") != "Closed"
            || Text(transition, "postMergeWave6Target") != "EligibleForIntake"
            || Text(transition, "reservedFeature") != "035"
            || Boolean(transition, "featureStarted"))
        {
            throw new InvalidDataException("Wave transition target or Feature-035 no-start boundary is invalid.");
        }

        if (hasCloseout
            && (!closeoutText!.Contains("Wave 5: `Closed`", StringComparison.Ordinal)
                || !closeoutText.Contains("Wave 6: `EligibleForIntake`", StringComparison.Ordinal)
                || !closeoutText.Contains("Reviewed feature head", StringComparison.Ordinal)
                || !closeoutText.Contains("Feature merge", StringComparison.Ordinal)))
        {
            throw new InvalidDataException("The causal closeout is incomplete.");
        }
    }

    private static void RequireExactIds<T>(
        JsonArray rows,
        IEnumerable<T> expectedIds,
        Func<JsonObject, T> selector,
        string label)
        where T : notnull
    {
        T[] expected = expectedIds.ToArray();
        T[] actual = rows.Select(Object).Select(selector).ToArray();
        if (actual.Distinct().Count() != actual.Length
            || !actual.OrderBy(value => value).SequenceEqual(expected.OrderBy(value => value)))
        {
            throw new InvalidDataException($"The {label} set is missing, duplicated, or unknown.");
        }
    }

    private static JsonObject LoadEvidence() => Parse(File.ReadAllText(EvidencePath()));

    private static JsonObject Parse(string json) =>
        JsonNode.Parse(json)?.AsObject() ?? throw new InvalidDataException("Closure evidence root is missing.");

    private static JsonObject Clone(JsonObject source) => source.DeepClone().AsObject();

    private static JsonArray Array(JsonObject owner, string property) =>
        owner[property]?.AsArray() ?? throw new InvalidDataException($"Missing array '{property}'.");

    private static JsonObject Object(JsonNode? node) =>
        node?.AsObject() ?? throw new InvalidDataException("Expected JSON object.");

    private static string Text(JsonObject owner, string property) =>
        owner[property]?.GetValue<string>() ?? throw new InvalidDataException($"Missing text '{property}'.");

    private static string Text(JsonNode? node) =>
        node?.GetValue<string>() ?? throw new InvalidDataException("Expected text value.");

    private static int Integer(JsonObject owner, string property) =>
        owner[property]?.GetValue<int>() ?? throw new InvalidDataException($"Missing integer '{property}'.");

    private static bool Boolean(JsonObject owner, string property) =>
        owner[property]?.GetValue<bool>() ?? throw new InvalidDataException($"Missing boolean '{property}'.");

    private static void RequireGitId(JsonObject owner, string property)
    {
        string value = Text(owner, property);
        if (value.Length != 40 || value.Any(character => !Uri.IsHexDigit(character)))
        {
            throw new InvalidDataException($"'{property}' is not a full Git object ID.");
        }
    }

    private static void RequireEqual<T>(T expected, T actual, string label)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new InvalidDataException($"{label} differs: expected '{expected}', actual '{actual}'.");
        }
    }

    private static string HashLines(IEnumerable<string> values) =>
        Sha256(Encoding.UTF8.GetBytes(string.Join('\n', values) + "\n"));

    private static string Sha256(byte[] bytes) =>
        Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();

    private static string CanonicalTextSha256(string document) =>
        Sha256(Encoding.UTF8.GetBytes(NormalizeLineEndings(document)));

    private static string GitBlobId(byte[] bytes)
    {
        // Git definiert Blob-IDs über Header plus Rohbytes; SHA-1 ist hier Dateiformat, keine Sicherheitsentscheidung.
        // Git defines blob IDs over header plus raw bytes; SHA-1 is the file format here, not a security choice.
        byte[] header = Encoding.ASCII.GetBytes($"blob {bytes.Length}\0");
        byte[] payload = new byte[header.Length + bytes.Length];
        Buffer.BlockCopy(header, 0, payload, 0, header.Length);
        Buffer.BlockCopy(bytes, 0, payload, header.Length, bytes.Length);
        return Convert.ToHexString(SHA1.HashData(payload)).ToLowerInvariant();
    }

    private static string NormalizeLineEndings(string document) =>
        document.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');

    private static string? ReadCloseoutIfPresent()
    {
        string path = Path.Combine(
            RepositoryRoot(),
            "specs",
            "034-wave5-combined-delta-closure",
            "delivery-closeout.md");
        return File.Exists(path) ? File.ReadAllText(path) : null;
    }

    private static string EvidencePath() =>
        Path.Combine(
            RepositoryRoot(),
            "specs",
            "034-wave5-combined-delta-closure",
            "wave5-combined-delta.json");

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }
}
