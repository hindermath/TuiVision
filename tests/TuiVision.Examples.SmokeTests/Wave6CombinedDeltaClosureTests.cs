// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die unabhängige Wave-6-Closure und ihre fail-closed Grenzen.
///
/// Verifies the independent Wave-6 closure and its fail-closed boundaries.
/// </summary>
[TestClass]
public sealed class Wave6CombinedDeltaClosureTests
{
    private static readonly string[] ExpectedCombinedIds =
        Enumerable.Range(1, 10).Select(number => $"W6C-{number:000}").ToArray();

    private static readonly string[] ExpectedFunctionalIds =
        Enumerable.Range(1, 10).Select(number => $"W6-{number:000}").ToArray();

    private static readonly string[] ExpectedShowcaseIds =
        Enumerable.Range(1, 10).Select(number => $"W6S-{number:000}").ToArray();

    private static readonly string[] ExpectedDimensions =
        ["behavior", "interaction", "layout", "proof", "documentation", "a11y", "platform", "security", "frameworkReuse"];

    private static readonly string[] AllowedDimensionValues =
        ["Pass", "IntentionalDeviation", "Gap", "N/A"];

    private static readonly string[] AllowedDecisions =
        ["AcceptedAsIs", "AcceptedIntentionalDeviation", "CandidateFinding", "ProductDecision"];

    private static readonly Dictionary<int, DeltaPin> ExpectedDeltas = new()
    {
        [101] = new("4b32762dfc60e18655de35d816ff1d4ede0185eb", "207e807ee8835779b9b8641f91868a6a5e80f938", "52f77facc518e3084f897148b44ec19e62b3dde6", "FunctionalProduct", 44, "d9cf20ce76e07e7e4d8c3064589fea670fc049fe083e293091ea86df9a82e902"),
        [102] = new("52f77facc518e3084f897148b44ec19e62b3dde6", "e6d5b07ef91ac8770ab03a1c4b9830a17bf334ad", "b0d99052b66f3f575f8343fa291761ec3f65779d", "FunctionalCloseout", 14, "cc60e928453e596aa86afd30d1918d51b44a532fa7dfb4f8ebfa9d5a97eb9841"),
        [103] = new("b0d99052b66f3f575f8343fa291761ec3f65779d", "3d1ee66b6eb7c54e8663430a57945d47a8d63845", "42a842fb63a0695a618a0f87ffec543e9bc3b6c8", "PromptMetadata", 9, "d38454d7882446d97839e60a38f4977b02b474a9d4929f0b854cf5e718f80848"),
        [104] = new("42a842fb63a0695a618a0f87ffec543e9bc3b6c8", "a0d506297c101104fd0e15911a7d21e1c5a21caa", "559bffbfbb94699a33cfe1ad8b01d5ac9b86641d", "ShowcaseProduct", 35, "2130ffa53ec34bbb23f9d64b040d3922a89238c2bcd7d36af44f9329bdc8a6aa"),
        [105] = new("559bffbfbb94699a33cfe1ad8b01d5ac9b86641d", "50a8f4dfab64de6a042555f8e304f01ac6b8596f", "371af97ff1741313ab808c87c2827655073cff2c", "ShowcaseCloseout", 14, "5fea3e2b0e8f988a7f6307ed98045ffa0d071ec29ab70567faebace961e72503")
    };

    private static readonly Dictionary<string, string> ExpectedInputHashes = new(StringComparer.Ordinal)
    {
        ["specs/035-wave6-tvfm-functional-porting/pr-evidence.md"] = "0a44474af9e700a9e4000c1f7141e32eb3097e43450d9953964e1a1d6939e29c",
        ["specs/035-wave6-tvfm-functional-porting/delivery-closeout.md"] = "55e1663260d6248b67ce3cb057d252b3adecc5d2c6fe5c8d88bc447969b120e9",
        ["specs/035-wave6-tvfm-functional-porting/retrospective.md"] = "22fdeda63fce7b5e11c2fe220804748b4536bdf7ecc258455df1700fd82819e6",
        ["specs/035-wave6-tvfm-functional-porting/plan.md"] = "fb3da3a015451ec2e22d496d58550ce29919f7808f3763eb1802b447decbca3b",
        ["specs/036-wave6-tvfm-showcase-remediation/pr-evidence.md"] = "e2ba63f09efe707be79294538bcbaa98b733264f9bf40dc78021f40013069d9d",
        ["specs/036-wave6-tvfm-showcase-remediation/delivery-closeout.md"] = "3daed913d02a496da4bd9f9063de8a84149c39ccffbe5956204bbde377988432",
        ["specs/036-wave6-tvfm-showcase-remediation/retrospective.md"] = "bb3dbdf1cd4759d98b7b508b6efb29a6823879b222f849f8b76f576f3d201e21",
        ["specs/036-wave6-tvfm-showcase-remediation/plan.md"] = "d3bf0f482d8628010e4635529d56c60e80040b58b821fc3ff9d2cde78ce4db11"
    };

    private static readonly Dictionary<string, SourcePin> ExpectedSources = new(StringComparer.Ordinal)
    {
        ["TVFM/ASSOC.PAS"] = new("8e58e8cade4d59ea7886ce561ebddeda42a67055", "6f063ce05767a5759d8e010f439ada682db9855108e09abe0657a79769b330d6", "CanonicalText"),
        ["TVFM/COLORS.PAS"] = new("6e12a7360061f644947b78556b9c6afb22cb696c", "70a5304e75fdff06bb5042b65b3e9126796235c05f7027c9b0482af6c80f724f", "CanonicalText"),
        ["TVFM/CYAN.PAL"] = new("287f247aadce23737d439d8218d1da679e74a29a", "7e76ad41b91f79bbd94271e0918b35c1d110a2dee0a2c50c4396ae9305b06b22", "ByteExact"),
        ["TVFM/DEFAULT.PAL"] = new("3cd91ee343d6d3ee1d6d8417406004a78257ea3b", "ea7964e4f88681a12009326e90cc4c75ac35a89bbcd35c61b400e1791988941f", "ByteExact"),
        ["TVFM/DIRVIEW.PAS"] = new("433c79fdea675514f15f175d4286ca3c9be600ee", "dd443d44d4d414607f857d570aaa186671e55eafc24f14f536ab12af5a55f0ba", "CanonicalText"),
        ["TVFM/DRAGDROP.PAS"] = new("4a89b891f95e04d03f1b7b9456bf5b091c2e3bcd", "48d2303a068d023ab1ed4d33a408500a14e98124b1d4748b128827ea1dc7ded7", "CanonicalText"),
        ["TVFM/EDITPAL.PAS"] = new("dbd7422e260bb8387a29ff8c403267121b72d425", "0cd9ae275d4bcc53a348be511892b65755df28fed535f0c58696eef74b753cbf", "CanonicalText"),
        ["TVFM/EQU.PAS"] = new("dfc5579094189919418264e5b95e67872571c556", "0d576743ffd297cfeee5181b07ad2958c9028bf270893be669ceb48c75c7dc71", "CanonicalText"),
        ["TVFM/FILECOPY.PAS"] = new("ee897434c618478c21bcbfda5f2a86ecf6a0bcf5", "7811657673bd12839a227e490ee98525c61371f292713c221f281f8d84ba308d", "CanonicalText"),
        ["TVFM/FILEFIND.PAS"] = new("3ad5cc3ce472872d1b0c179279d1390728b9e0ec", "224a627877b40b54673a103721b7728dd8eb122fc570760d461e016772a95b75", "CanonicalText"),
        ["TVFM/FILEVIEW.PAS"] = new("b5a91ef20e4aff23b02bd5aa97577dcbd3997818", "343dee0bddbc869eff7e4729f6f3decbafeb0ba50ee1da137d67ebe8d72fd479", "CanonicalText"),
        ["TVFM/GAUGES.PAS"] = new("d856f32e8ce00e0a4a6c4a6d93a1b80908ed7635", "7478ac17cd21f65245f00b2a5d53ea8d6207fd6192e12f644c10e39609eefc39", "CanonicalText"),
        ["TVFM/GLOBALS.PAS"] = new("17fc4ec2718cea01e27280d37b0a7be8d1aaa94f", "f593e95784b1c38b5aa899f43fa299c9297c1bf7fe33d3c0edcf3c3462d107f2", "CanonicalText"),
        ["TVFM/INFOVIEW.PAS"] = new("3c2f241d9865ea430929b01fe799e418f76afabd", "f644b0e7b0369a18b49f5d70cb1c07935911c0cd87b4076fb44920a6d7c2067d", "CanonicalText"),
        ["TVFM/MAKERES.PAS"] = new("5d43cede68b0b287657338e85d4fc99b91831959", "7543c7a0f471893b208e5ab46dcacbff5f4a175d267ae55519386f3d228e2197", "CanonicalText"),
        ["TVFM/MAKETVFM.BAT"] = new("89e02a63f61d70df37d526961c0de316cfcc675a", "4231a8f3b888ad3ca8f1b23a1f4d1810cee4565cef30a45d94f4566eda568285", "CanonicalText"),
        ["TVFM/ROSE.PAL"] = new("f68b7fc915689a9615e04eae237f640a2274ac23", "d557882668638308c2fe1e1157acaf43da87bbb2ae55c59d6c5e812772beb0d6", "ByteExact"),
        ["TVFM/TOOLS.PAS"] = new("d9e8c4ca6834f052eb9ffb28cd69e77953ec36a2", "a8800797bca0ed45a780086601a8ba04a3ff7f9846376477f7851cf0bf72807e", "CanonicalText"),
        ["TVFM/TRASH.PAS"] = new("fb0e380ca6dc0d6a3cd7d6290e9fbab5f6bac7df", "115b64620483df2b54641f41352819caf80f9c8fcef67c8f1bbb527a225b78e0", "CanonicalText"),
        ["TVFM/TREEWIN.PAS"] = new("d88ed66b39ad911c827a00004942c48146f81424", "54b7ef2d03655040e24c0b75843d4b679b658aba04785f13355241a89c6fff40", "CanonicalText"),
        ["TVFM/TVFM.PAS"] = new("663cbfdf227a2cef1257494191e7a2402f2fc3e3", "8e8da1b47d1d43e8835c98d82a9bc4cfc461d56d934080d75b60985703b15d0a", "CanonicalText"),
        ["TVFM/TVFM.TVR"] = new("2b5237af63e50782c189b7e0fa471f70fd380761", "0e0f359e852a5e640355ca2748605f3565254c814e4b61b0a910aaf59821122c", "ByteExact"),
        ["TVFM/VIEWHEX.PAS"] = new("e51c35a3ffbff2316d372cd0b918812508d0f371", "e478bc15a0b6dd663e9da4e976bf3656b86e78e2c6237f6f90c4bd7148b80cb2", "CanonicalText"),
        ["TVFM/VIEWTEXT.PAS"] = new("309e67560f32976f3eb64c466e069d6fe10b569d", "15f1a7335d68b0e71536b9da1a36039e7231100932d4023b46c1098710920c64", "CanonicalText")
    };

    /// <summary>
    /// Prüft exakte PR-, Eingabe- und historische Quellenpins.
    ///
    /// Verifies exact PR, input, and historical source pins.
    /// </summary>
    [TestMethod]
    public void Test_ProvenanceAndHistoricalSourcesAreExact()
    {
        JsonObject root = LoadEvidence();
        ValidateRoot(root);
        ValidateProductDeltas(root);
        ValidateAcceptedInputs(root);
        ValidateHistoricalSources(root);
    }

    /// <summary>
    /// Prüft die vollständige kombinierte Vertragsmatrix.
    ///
    /// Verifies the complete combined contract matrix.
    /// </summary>
    [TestMethod]
    public void Test_CombinedAreaMatrixIsComplete()
    {
        ValidateCombinedMatrix(LoadEvidence());
    }

    /// <summary>
    /// Prüft den ersten vollständigen vertikalen Vertrags-Slice.
    ///
    /// Verifies the first complete vertical contract slice.
    /// </summary>
    [TestMethod]
    public void Test_FirstCombinedAreaFormsACompleteVerticalSlice()
    {
        JsonObject root = LoadEvidence();
        JsonObject functional = Array(root, "functionalProofs").Select(Object)
            .Single(row => Text(row, "functionalId") == "W6-001");
        JsonObject showcase = Array(root, "showcaseProofs").Select(Object)
            .Single(row => Text(row, "showcaseId") == "W6S-009");
        JsonObject combined = Array(root, "combinedAreas").Select(Object)
            .Single(row => Text(row, "combinedId") == "W6C-001");

        RequireNonEmpty(functional, "scope", "frameworkDecision", "proof", "evidencePath", "residualRisk", "reevaluationTrigger");
        RequireNonEmpty(showcase, "scope", "visibleAccess", "layoutProof", "focusStatusDescriptionKeyboard", "cellProof", "evidencePath", "residualRisk", "reevaluationTrigger");
        RequireEqual("W6-001", Text(combined, "functionalId"), "vertical functional relation");
        RequireEqual("W6S-009", Text(combined, "showcaseId"), "vertical showcase relation");
        RequireEqual("Tp7FileManager", Text(combined, "entryPoint"), "vertical entry point");
        RequireNonEmpty(combined, "scope", "firstVisibleState", "primaryAction", "appLoopProof", "viewProof", "focusProof", "statusProof", "descriptionProof", "keyboardExitProof", "cellProof", "frameworkContracts", "localComposition", "safetyBoundary", "a11yBoundary", "platformBoundary", "evidencePath", "residualRisk", "reevaluationTrigger");
        RequireEqual("AcceptedAsIs", Text(combined, "primaryDecision"), "vertical decision");
        RequireExactIds(
            new JsonArray(Object(combined["dimensions"]).Select(pair => JsonValue.Create(pair.Key)).ToArray()),
            ExpectedDimensions,
            Text,
            "vertical dimension");
    }

    /// <summary>
    /// Prüft Governance und die lokale kausale Grenze.
    ///
    /// Verifies governance and the local causal boundary.
    /// </summary>
    [TestMethod]
    public void Test_GovernanceAndLocalCausalBoundaryAreComplete()
    {
        JsonObject root = LoadEvidence();
        ValidateGovernance(root);
        ValidateLocalBoundary(root);
    }

    /// <summary>
    /// Prüft fehlende, doppelte und ungültige Matrixentscheidungen.
    ///
    /// Verifies missing, duplicate, and invalid matrix decisions.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsCardinalityAndDecisionMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject missing = Clone(valid);
        Array(missing, "combinedAreas").RemoveAt(0);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedMatrix(missing));

        JsonObject duplicate = Clone(valid);
        Array(duplicate, "combinedAreas").Add(Array(duplicate, "combinedAreas")[0]!.DeepClone());
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedMatrix(duplicate));

        JsonObject unknownDecision = Clone(valid);
        Object(Array(unknownDecision, "combinedAreas")[0])["primaryDecision"] = "UnknownDecision";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedMatrix(unknownDecision));

        JsonObject acceptedGap = Clone(valid);
        Object(Object(Array(acceptedGap, "combinedAreas")[0])["dimensions"])["proof"] = "Gap";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateCombinedMatrix(acceptedGap));
    }

    /// <summary>
    /// Prüft manipulierte Provenienz und nicht reziproke Beziehungen.
    ///
    /// Verifies manipulated provenance and non-reciprocal relationships.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsProvenanceAndRelationshipMutations()
    {
        JsonObject valid = LoadEvidence();

        JsonObject pin = Clone(valid);
        Object(Array(pin, "productDeltas")[0])["headCommit"] = new string('0', 40);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateProductDeltas(pin));

        JsonObject source = Clone(valid);
        Object(Array(source, "historicalSources")[0])["sha256"] = new string('0', 64);
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateHistoricalSources(source));

        JsonObject relation = Clone(valid);
        Array(Object(Array(relation, "historicalSources")[0]), "combinedIds").Clear();
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateHistoricalSources(relation));
    }

    /// <summary>
    /// Prüft unvollständige Findings und falsch behauptete Closure-Zustände.
    ///
    /// Verifies incomplete findings and falsely claimed closure states.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsIncompleteFindingAndFalseClosure()
    {
        JsonObject valid = LoadEvidence();

        JsonObject finding = Clone(valid);
        Array(finding, "candidateFindings").Add(new JsonObject
        {
            ["findingId"] = "W6D001",
            ["owner"] = ""
        });
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateLocalBoundary(finding));

        JsonObject falseClosure = Clone(valid);
        falseClosure["wave6State"] = "Closed";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateLocalBoundary(falseClosure));
    }

    /// <summary>
    /// Prüft plattformneutrale Texthashes und die Byte-Grenze binärer Daten.
    ///
    /// Verifies platform-neutral text hashes and the binary byte boundary.
    /// </summary>
    [TestMethod]
    public void Test_TextHashIsLineEndingNeutralAndBinaryHashIsNotNormalized()
    {
        const string lf = "first\nsecond\n";
        const string crlf = "first\r\nsecond\r\n";
        Assert.AreEqual(
            HashHistoricalSource("TVFM/ASSOC.PAS", Encoding.ASCII.GetBytes(lf)),
            HashHistoricalSource("TVFM/ASSOC.PAS", Encoding.ASCII.GetBytes(crlf)));
        Assert.AreNotEqual(
            HashHistoricalSource("TVFM/CYAN.PAL", Encoding.ASCII.GetBytes(lf)),
            HashHistoricalSource("TVFM/CYAN.PAL", Encoding.ASCII.GetBytes(crlf)));
    }

    private static void ValidateRoot(JsonObject root)
    {
        RequireEqual("1.0", Text(root, "schemaVersion"), "schema version");
        RequireEqual("037-wave6-combined-delta-closure", Text(root, "runId"), "run ID");
        RequireEqual("MergeAndSync", Text(root, "deliveryMode"), "delivery mode");
        RequireGitId(root, "baselineCommit");
        RequireNonEmpty(root, "owner", "reviewer", "reviewDate", "result", "residualRisk", "reevaluationTrigger");
    }

    private static void ValidateProductDeltas(JsonObject root)
    {
        JsonArray deltas = Array(root, "productDeltas");
        RequireExactIds(deltas, ExpectedDeltas.Keys, row => Integer(row, "prNumber"), "PR");
        foreach (JsonObject delta in deltas.Select(Object))
        {
            int pr = Integer(delta, "prNumber");
            DeltaPin expected = ExpectedDeltas[pr];
            RequireEqual(expected.Base, Text(delta, "baseCommit"), $"PR #{pr} base");
            RequireEqual(expected.Head, Text(delta, "headCommit"), $"PR #{pr} head");
            RequireEqual(expected.Merge, Text(delta, "mergeCommit"), $"PR #{pr} merge");
            RequireEqual(expected.Role, Text(delta, "role"), $"PR #{pr} role");
            RequireEqual(expected.Files, Integer(delta, "changedFileCount"), $"PR #{pr} file count");
            RequireEqual(expected.FileSetHash, Text(delta, "changedFileSetSha256"), $"PR #{pr} file-set hash");
            string[] paths = Array(delta, "changedFiles").Select(Text).ToArray();
            if (paths.Length != expected.Files
                || paths.Distinct(StringComparer.Ordinal).Count() != paths.Length
                || !paths.SequenceEqual(paths.Order(StringComparer.Ordinal))
                || HashLines(paths) != expected.FileSetHash)
            {
                throw new InvalidDataException($"PR #{pr} changed-file set is incomplete or unordered.");
            }

            JsonArray productPaths = Array(delta, "productPaths");
            bool productRole = expected.Role is "FunctionalProduct" or "ShowcaseProduct";
            if (productRole != (productPaths.Count > 0)
                || productPaths.Select(Text).Any(path => !paths.Contains(path, StringComparer.Ordinal)))
            {
                throw new InvalidDataException($"PR #{pr} product-path boundary is invalid.");
            }
        }
    }

    private static void ValidateAcceptedInputs(JsonObject root)
    {
        JsonArray rows = Array(root, "acceptedInputs");
        RequireExactIds(rows, ExpectedInputHashes.Keys, row => Text(row, "path"), "accepted input");
        foreach (JsonObject row in rows.Select(Object))
        {
            string path = Text(row, "path");
            RequireEqual(ExpectedInputHashes[path], Text(row, "sha256"), $"input {path} pin");
            RequireEqual(ExpectedInputHashes[path], CanonicalTextSha256(File.ReadAllText(Path.Combine(RepositoryRoot(), path))), $"input {path} content");
            RequireNonEmpty(row, "role", "result", "reevaluationTrigger");
        }
    }

    private static void ValidateHistoricalSources(JsonObject root)
    {
        JsonArray rows = Array(root, "historicalSources");
        RequireExactIds(rows, ExpectedSources.Keys, row => Text(row, "path"), "historical source");
        HashSet<string> combinedSet = ExpectedCombinedIds.ToHashSet(StringComparer.Ordinal);
        foreach (JsonObject row in rows.Select(Object))
        {
            string path = Text(row, "path");
            SourcePin expected = ExpectedSources[path];
            RequireEqual(expected.Blob, Text(row, "gitBlob"), $"source {path} blob");
            RequireEqual(expected.Hash, Text(row, "sha256"), $"source {path} hash");
            RequireEqual(expected.HashMode, Text(row, "hashMode"), $"source {path} hash mode");
            RequireNonEmpty(row, "role", "historicalPurpose", "reevaluationTrigger");
            string fullPath = Path.Combine(RepositoryRoot(), path);
            string actual = HashHistoricalSource(path, File.ReadAllBytes(fullPath));
            RequireEqual(expected.Hash, actual, $"source {path} current content");
            string[] combinedIds = Array(row, "combinedIds").Select(Text).ToArray();
            if (combinedIds.Length == 0 || combinedIds.Any(id => !combinedSet.Contains(id)))
            {
                throw new InvalidDataException($"Source '{path}' has no valid combined-area relation.");
            }
        }

        JsonArray combinedAreas = Array(root, "combinedAreas");
        foreach (JsonObject area in combinedAreas.Select(Object))
        {
            string combinedId = Text(area, "combinedId");
            foreach (string sourcePath in Array(area, "sourcePaths").Select(Text))
            {
                JsonObject source = rows.Select(Object).Single(row => Text(row, "path") == sourcePath);
                if (!Array(source, "combinedIds").Select(Text).Contains(combinedId, StringComparer.Ordinal))
                {
                    throw new InvalidDataException($"Source relation '{sourcePath}' -> '{combinedId}' is not reciprocal.");
                }
            }
        }
    }

    private static void ValidateCombinedMatrix(JsonObject root)
    {
        JsonArray functional = Array(root, "functionalProofs");
        JsonArray showcase = Array(root, "showcaseProofs");
        JsonArray combined = Array(root, "combinedAreas");
        RequireExactIds(functional, ExpectedFunctionalIds, row => Text(row, "functionalId"), "functional proof");
        RequireExactIds(showcase, ExpectedShowcaseIds, row => Text(row, "showcaseId"), "showcase proof");
        RequireExactIds(combined, ExpectedCombinedIds, row => Text(row, "combinedId"), "combined area");
        RequireExactIds(combined, ExpectedFunctionalIds, row => Text(row, "functionalId"), "combined functional relation");
        RequireExactIds(combined, ExpectedShowcaseIds, row => Text(row, "showcaseId"), "combined showcase relation");

        foreach (JsonObject row in functional.Select(Object))
        {
            RequireNonEmpty(row, "scope", "frameworkDecision", "proof", "evidencePath", "residualRisk", "reevaluationTrigger");
        }

        foreach (JsonObject row in showcase.Select(Object))
        {
            RequireNonEmpty(row, "scope", "visibleAccess", "layoutProof", "focusStatusDescriptionKeyboard", "cellProof", "evidencePath", "residualRisk", "reevaluationTrigger");
        }

        foreach (JsonObject row in combined.Select(Object))
        {
            RequireEqual("Tp7FileManager", Text(row, "entryPoint"), "combined entry point");
            RequireNonEmpty(row, "scope", "firstVisibleState", "primaryAction", "appLoopProof", "viewProof", "focusProof", "statusProof", "descriptionProof", "keyboardExitProof", "cellProof", "frameworkContracts", "localComposition", "safetyBoundary", "a11yBoundary", "platformBoundary", "evidencePath", "residualRisk", "reevaluationTrigger");
            if (Array(row, "sourcePaths").Count == 0)
            {
                throw new InvalidDataException($"Combined area '{Text(row, "combinedId")}' lacks historical sources.");
            }

            string decision = Text(row, "primaryDecision");
            if (!AllowedDecisions.Contains(decision, StringComparer.Ordinal))
            {
                throw new InvalidDataException($"Combined area '{Text(row, "combinedId")}' has an unknown decision.");
            }

            JsonObject dimensions = Object(row["dimensions"]);
            RequireExactIds(new JsonArray(dimensions.Select(pair => JsonValue.Create(pair.Key)).ToArray()), ExpectedDimensions, Text, "dimension");
            string[] values = dimensions.Select(pair => pair.Value?.GetValue<string>() ?? "").ToArray();
            if (values.Any(value => !AllowedDimensionValues.Contains(value, StringComparer.Ordinal))
                || (decision is "AcceptedAsIs" or "AcceptedIntentionalDeviation") && values.Contains("Gap", StringComparer.Ordinal))
            {
                throw new InvalidDataException($"Combined area '{Text(row, "combinedId")}' has an invalid dimension state.");
            }
        }

        JsonArray entryPoints = Array(root, "entryPoints");
        RequireExactIds(entryPoints, ["Tp7FileManager"], row => Text(row, "entryPoint"), "entry point");
        RequireNonEmpty(Object(entryPoints[0]), "projectPath", "guidePath", "normalCommand", "smokeCommand", "firstVisibleState", "primaryAction", "descriptionPath", "exitPath", "appLoopProof", "viewFocusStatusCellProof", "platformBoundary", "result", "reevaluationTrigger");
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
            "intake-authoring-governance",
            "intake-review-governance",
            "intake-sequencing-governance",
            "autonomous-run-governance",
            "parallel-autonomous-governance"
        ];
        JsonArray rows = Array(root, "governance");
        RequireExactIds(rows, expected, row => Text(row, "presetName"), "governance preset");
        foreach (JsonObject row in rows.Select(Object))
        {
            RequireNonEmpty(row, "presetVersion", "checkpoint", "applicability", "rationale", "evidencePath", "owner", "reviewer", "reviewDate", "result", "residualRisk", "followUp", "reevaluationTrigger");
            if (Text(row, "applicability") == "N/A" && Text(row, "reevaluationTrigger") == "N/A")
            {
                throw new InvalidDataException($"N/A governance row '{Text(row, "presetName")}' lacks a trigger.");
            }
        }
    }

    private static void ValidateLocalBoundary(JsonObject root)
    {
        string candidateDecision = Text(root, "candidateDecision");
        if (candidateDecision is not ("ActiveAudit" or "ReadyForDelivery"))
        {
            throw new InvalidDataException($"Candidate decision '{candidateDecision}' is invalid.");
        }

        RequireEqual("BlockedPendingDelivery", Text(root, "wave6State"), "Wave 6 state");
        RequireEqual("BlockedPendingWave6Closure", Text(root, "portfolioAuditState"), "portfolio audit state");
        JsonArray findings = Array(root, "candidateFindings");
        foreach (JsonObject finding in findings.Select(Object))
        {
            string id = Text(finding, "findingId");
            if (!id.StartsWith("W6D", StringComparison.Ordinal)
                || id.Length != 6
                || !int.TryParse(id.AsSpan(3), out int number)
                || number <= 0)
            {
                throw new InvalidDataException("Candidate Finding ID is invalid.");
            }

            RequireNonEmpty(finding, "category", "reproduction", "evidencePath", "owner", "followUpBoundary", "reevaluationTrigger");
        }

        if (findings.Count != 0 || Array(root, "productDecisions").Count != 0)
        {
            throw new InvalidDataException("A finding or product decision blocks ReadyForDelivery.");
        }

        foreach (JsonObject validation in Array(root, "validations").Select(Object))
        {
            RequireNonEmpty(validation, "gateId", "scope", "command", "platform", "candidate", "result", "metric", "evidencePath", "failureBoundary", "reevaluationTrigger");
            string result = Text(validation, "result");
            bool accepted = candidateDecision == "ReadyForDelivery"
                ? result is "Pass" or "N/A" or "NotTriggered"
                : result is "Pass" or "N/A" or "NotTriggered" or "Pending";
            if (!accepted)
            {
                throw new InvalidDataException($"Validation '{Text(validation, "gateId")}' is not terminal.");
            }
        }
    }

    private static void RequireExactIds<T>(JsonArray rows, IEnumerable<T> expectedIds, Func<JsonObject, T> selector, string label)
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

    private static void RequireExactIds(JsonArray rows, IEnumerable<string> expectedIds, Func<JsonNode?, string> selector, string label)
    {
        string[] expected = expectedIds.ToArray();
        string[] actual = rows.Select(selector).ToArray();
        if (actual.Distinct(StringComparer.Ordinal).Count() != actual.Length
            || !actual.Order(StringComparer.Ordinal).SequenceEqual(expected.Order(StringComparer.Ordinal)))
        {
            throw new InvalidDataException($"The {label} set is missing, duplicated, or unknown.");
        }
    }

    private static void RequireNonEmpty(JsonObject owner, params string[] properties)
    {
        foreach (string property in properties)
        {
            if (string.IsNullOrWhiteSpace(Text(owner, property)))
            {
                throw new InvalidDataException($"Property '{property}' is empty.");
            }
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

    private static string HashHistoricalSource(string relativePath, byte[] content)
    {
        string extension = Path.GetExtension(relativePath);
        byte[] canonical = extension.Equals(".PAS", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".BAT", StringComparison.OrdinalIgnoreCase)
                ? NormalizeHistoricalTextBytes(content)
                : content;
        return Sha256(canonical);
    }

    private static byte[] NormalizeHistoricalTextBytes(byte[] content)
    {
        List<byte> normalized = new(content.Length);
        for (int index = 0; index < content.Length; index++)
        {
            if (content[index] != (byte)'\r')
            {
                normalized.Add(content[index]);
                continue;
            }

            // Git darf historische Textquellen als CRLF auschecken; andere Legacy-Bytes bleiben unverändert.
            // Git may check out historical text sources as CRLF; other legacy bytes remain unchanged.
            if (index + 1 < content.Length && content[index + 1] == (byte)'\n')
            {
                index++;
            }

            normalized.Add((byte)'\n');
        }

        return normalized.ToArray();
    }

    private static string NormalizeLineEndings(string document) =>
        document.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');

    private static string EvidencePath() =>
        Path.Combine(RepositoryRoot(), "specs", "037-wave6-combined-delta-closure", "wave6-combined-delta.json");

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }

    private sealed record DeltaPin(string Base, string Head, string Merge, string Role, int Files, string FileSetHash);

    private sealed record SourcePin(string Blob, string Hash, string HashMode);
}
