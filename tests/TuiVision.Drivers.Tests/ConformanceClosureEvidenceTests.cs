// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text.Json;
using System.Text.Json.Nodes;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Prueft den strukturierten Nachweis des Pre-Wave-5-/Wave-6-Abschlusses.
///
/// Validates the structured evidence for the pre-Wave-5/Wave-6 closure.
/// </summary>
[TestClass]
public sealed class ConformanceClosureEvidenceTests
{
    private const string FeatureDirectoryName = "028-pre-wave5-wave6-conformance-closure";

    private static readonly IReadOnlySet<string> AllowedFindingDecisions =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "Closed",
            "AlreadySatisfiedWithNewProof",
            "Reopened025",
            "Reopened026",
            "ProductDecision",
        };

    private static readonly IReadOnlySet<string> AllowedConsumerDecisions =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "UseExistingFramework",
            "SmallFrameworkFix",
            "IntentionalDeviation",
            "FollowUpHardening",
            "ProductDecision",
        };

    /// <summary>
    /// Stellt sicher, dass der Feature-028-Abschlussdatensatz vorhanden ist.
    ///
    /// Ensures that the Feature-028 closure dataset exists.
    /// </summary>
    [TestMethod]
    public void Test_ClosureDatasetExists()
    {
        string path = GetClosurePath();

        Assert.IsTrue(File.Exists(path), $"Closure evidence dataset is missing: {path}");
    }

    /// <summary>
    /// Prueft den ersten vollstaendigen Finding-, Integrations- und Consumer-Slice.
    ///
    /// Validates the first complete finding, integration, and consumer slice.
    /// </summary>
    [TestMethod]
    public void Test_RepresentativeSliceIsComplete()
    {
        JsonObject root = LoadClosureObject();
        CollectionAssert.AreEquivalent(
            new[] { "run", "findingClosures", "integrationSlices", "consumerReadiness", "governanceDecisions", "validationEvidence" },
            root.Select(property => property.Key).ToArray());

        JsonObject run = RequiredObject(root, "run");
        Assert.AreEqual(FeatureDirectoryName, RequiredString(run, "runId"));
        Assert.AreEqual("1.0", RequiredString(run, "schemaVersion"));
        Assert.AreEqual("ReadyForTerminalGuiAudit", RequiredString(run, "existingGateDecision"));
        Assert.AreEqual("BlockedPendingTerminalGuiAudit", RequiredString(run, "wave5State"));
        Assert.AreEqual("BlockedPendingTerminalGuiAudit", RequiredString(run, "wave6State"));
        Assert.AreEqual("029-tv203-freevision-terminalgui-conformance-audit", RequiredString(run, "nextIntake"));

        JsonObject finding = RequiredArray(root, "findingClosures").OfType<JsonObject>()
            .Single(row => RequiredString(row, "findingId") == "F001");
        Assert.AreEqual("F001", RequiredString(finding, "findingId"));
        Assert.AreEqual("C004", RequiredString(finding, "contractId"));
        Assert.AreEqual("Closed", RequiredString(finding, "closureDecision"));
        AssertProofReferences(RequiredArray(finding, "realPathProofs"));
        AssertReviewMetadata(finding);

        JsonObject slice = RequiredArray(root, "integrationSlices").OfType<JsonObject>()
            .Single(row => RequiredString(row, "sliceId") == "R-028-001");
        Assert.AreEqual("R-028-001", RequiredString(slice, "sliceId"));
        Assert.AreEqual("PrimaryProof: every cited test enters a maintained production boundary; the Feature-028 validator only checks evidence integrity.", RequiredString(slice, "helperRole"));
        AssertProofReferences(RequiredArray(slice, "proofReferences"));
        AssertReviewMetadata(slice);

        JsonObject consumer = RequiredArray(root, "consumerReadiness").OfType<JsonObject>()
            .Single(row => RequiredString(row, "consumerId") == "W5-001");
        Assert.AreEqual("W5-001", RequiredString(consumer, "consumerId"));
        Assert.AreEqual("UseExistingFramework", RequiredString(consumer, "decision"));
        AssertProofReferences(RequiredArray(consumer, "proofReferences"));
        AssertReviewMetadata(consumer);
    }

    /// <summary>
    /// Prueft vollstaendige Mengen, Relationen, Pfade, Metadaten und Gate-Regeln.
    ///
    /// Validates complete sets, relationships, paths, metadata, and gate rules.
    /// </summary>
    [TestMethod]
    public void Test_CompleteDatasetRelationshipsAndGateRules()
    {
        string[] errors = ValidateCompleteDataset(LoadClosureObject()).ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));
    }

    /// <summary>
    /// Beweist die Ablehnung von defektem JSON, Duplikaten und unbekannten Entscheidungen.
    ///
    /// Proves rejection of malformed JSON, duplicate IDs, and unknown decisions.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsMalformedUnknownAndDuplicateData()
    {
        Assert.Throws<JsonException>(() => JsonNode.Parse("{"));

        JsonObject duplicate = (JsonObject)LoadClosureObject().DeepClone();
        JsonArray duplicateFindings = RequiredArray(duplicate, "findingClosures");
        duplicateFindings.Add(duplicateFindings[0]!.DeepClone());
        Assert.IsTrue(
            ValidateCompleteDataset(duplicate).Any(error => error.Contains("duplicate findingId", StringComparison.Ordinal)));

        JsonObject unknown = (JsonObject)LoadClosureObject().DeepClone();
        RequiredArray(unknown, "findingClosures")[0]!.AsObject()["closureDecision"] = "Applicable";
        Assert.IsTrue(
            ValidateCompleteDataset(unknown).Any(error => error.Contains("unknown finding decision", StringComparison.Ordinal)));
    }

    /// <summary>
    /// Beweist, dass ein Ready-Gate bei jeder unvollstaendigen oder blockierenden Zeile scheitert.
    ///
    /// Proves that a ready gate fails for every incomplete or blocking row.
    /// </summary>
    [TestMethod]
    public void Test_GateFailsClosedForIncompleteOrBlockingEvidence()
    {
        JsonObject incomplete = (JsonObject)LoadClosureObject().DeepClone();
        RequiredObject(incomplete, "run")["existingGateDecision"] = "ReadyForTerminalGuiAudit";
        RequiredArray(incomplete, "validationEvidence").Clear();
        Assert.IsTrue(
            ValidateCompleteDataset(incomplete).Any(error => error.Contains("ready gate requires", StringComparison.Ordinal)));

        JsonObject reopened = (JsonObject)incomplete.DeepClone();
        RequiredArray(reopened, "findingClosures")[0]!.AsObject()["closureDecision"] = "Reopened025";
        Assert.IsTrue(
            ValidateCompleteDataset(reopened).Any(error => error.Contains("closed finding decisions", StringComparison.Ordinal)));

        JsonObject blockingConsumer = (JsonObject)incomplete.DeepClone();
        RequiredArray(blockingConsumer, "consumerReadiness")[0]!.AsObject()["decision"] = "SmallFrameworkFix";
        Assert.IsTrue(
            ValidateCompleteDataset(blockingConsumer).Any(error => error.Contains("non-blocking consumer decisions", StringComparison.Ordinal)));

        JsonObject failedSlice = (JsonObject)incomplete.DeepClone();
        RequiredArray(failedSlice, "integrationSlices")[0]!.AsObject()["result"] = "Fail";
        Assert.IsTrue(
            ValidateCompleteDataset(failedSlice).Any(error => error.Contains("passing integration slices", StringComparison.Ordinal)));
    }

    /// <summary>
    /// Vergleicht alle Finding-Zeilen mit Audit, Resolution, Vertrag und realem Proof.
    ///
    /// Reconciles every finding row with its audit, resolution, contract, and real proof.
    /// </summary>
    [TestMethod]
    public void Test_FindingClosuresReconcileCanonicalAuditAndResolutions()
    {
        JsonObject closure = LoadClosureObject();
        JsonObject audit = LoadAuditObject();
        JsonObject[] closures = RequiredArray(closure, "findingClosures").OfType<JsonObject>().ToArray();
        JsonObject[] findings = RequiredArray(audit, "findings").OfType<JsonObject>().ToArray();
        JsonObject[] resolutions = RequiredArray(audit, "resolutions").OfType<JsonObject>().ToArray();
        JsonObject[] contracts = RequiredArray(audit, "contracts").OfType<JsonObject>().ToArray();

        CollectionAssert.AreEqual(ExpectedIds("F", 13).ToArray(), closures.Select(row => RequiredString(row, "findingId")).ToArray());
        foreach (JsonObject row in closures)
        {
            string findingId = RequiredString(row, "findingId");
            JsonObject finding = findings.Single(item => RequiredString(item, "findingId") == findingId);
            JsonObject resolution = resolutions.Single(item => RequiredString(item, "findingId") == findingId);
            JsonObject contract = contracts.Single(item => RequiredString(item, "contractId") == RequiredString(finding, "contractId"));

            Assert.AreEqual(RequiredString(finding, "contractId"), RequiredString(row, "contractId"));
            Assert.AreEqual(RequiredString(finding, "observation"), RequiredString(row, "originalObservation"));
            Assert.AreEqual(RequiredString(finding, "consumerScope"), RequiredString(row, "consumerScope"));
            Assert.AreEqual(RequiredString(resolution, "featureId"), RequiredString(row, "ownerFeature"));
            Assert.AreEqual(RequiredString(resolution, "redProof"), RequiredString(row, "redProof"));
            Assert.AreEqual(RequiredString(contract, "historicalIntent"), RequiredString(row, "historicalIntent"));
            StringAssert.StartsWith(RequiredString(row, "freeVisionRelation"), RequiredString(contract, "freeVisionRelation"));
            Assert.AreEqual("Closed", RequiredString(row, "closureDecision"));

            CollectionAssert.AreEquivalent(
                StringValues(RequiredArray(resolution, "changeEvidence")),
                StringValues(RequiredArray(row, "mergedChangePaths")));
            AssertProofReferences(RequiredArray(row, "realPathProofs"));
            AssertReviewMetadata(row);
            Assert.AreEqual(
                findingId is "F010" or "F011" or "F012" or "F013"
                    ? "026-component-data-conformance-hardening"
                    : "025-core-runtime-conformance-hardening",
                RequiredString(row, "ownerFeature"));
        }
    }

    /// <summary>
    /// Prueft alle sieben Integrations-Slices samt Negativgrenze und Proof-Rolle.
    ///
    /// Validates all seven integration slices, negative boundaries, and proof roles.
    /// </summary>
    [TestMethod]
    public void Test_IntegrationSlicesAreCompleteAndRealPathBacked()
    {
        JsonObject[] slices = RequiredArray(LoadClosureObject(), "integrationSlices").OfType<JsonObject>().ToArray();
        CollectionAssert.AreEqual(
            Enumerable.Range(1, 7).Select(value => $"R-028-{value:000}").ToArray(),
            slices.Select(slice => RequiredString(slice, "sliceId")).ToArray());

        foreach (JsonObject slice in slices)
        {
            Assert.AreEqual("Pass", RequiredString(slice, "result"));
            Assert.IsNotEmpty(RequiredArray(slice, "contractIds"));
            Assert.IsNotEmpty(RequiredArray(slice, "findingIds"));
            Assert.IsNotEmpty(RequiredString(slice, "productionEntry"));
            Assert.IsNotEmpty(RequiredString(slice, "assertions"));
            Assert.IsNotEmpty(RequiredString(slice, "negativeOrFallback"));
            Assert.IsTrue(
                RequiredString(slice, "helperRole").StartsWith("PrimaryProof", StringComparison.Ordinal),
                $"{RequiredString(slice, "sliceId")} must explain why its named tests are PrimaryProof.");
            Assert.IsNotEmpty(RequiredString(slice, "proofLimit"));
            Assert.IsNotEmpty(RequiredString(slice, "platforms"));
            Assert.IsNotEmpty(RequiredString(slice, "a11yEvidence"));
            AssertProofReferences(RequiredArray(slice, "proofReferences"));
            AssertReviewMetadata(slice);
        }
    }

    /// <summary>
    /// Prueft die unveraenderte Consumer-Baseline und ihre eine primaere Entscheidung.
    ///
    /// Validates the unchanged consumer baseline and its single primary decision.
    /// </summary>
    [TestMethod]
    public void Test_ConsumerReadinessIsCompleteAndProtected()
    {
        JsonObject[] consumers = RequiredArray(LoadClosureObject(), "consumerReadiness")
            .OfType<JsonObject>()
            .ToArray();
        string[] expected = Enumerable.Range(1, 6).Select(value => $"W5-{value:000}")
            .Concat(Enumerable.Range(1, 7).Select(value => $"W6-{value:000}"))
            .ToArray();

        CollectionAssert.AreEqual(expected, consumers.Select(row => RequiredString(row, "consumerId")).ToArray());
        foreach (JsonObject consumer in consumers)
        {
            string id = RequiredString(consumer, "consumerId");
            Assert.AreEqual(id == "W6-007" ? "FollowUpHardening" : "UseExistingFramework", RequiredString(consumer, "decision"));
            Assert.AreEqual(id.StartsWith("W5-", StringComparison.Ordinal) ? "Wave5" : "Wave6", RequiredString(consumer, "wave"));
            Assert.IsNotEmpty(RequiredArray(consumer, "contractIds"));
            Assert.IsNotEmpty(RequiredString(consumer, "flow"));
            Assert.IsNotEmpty(RequiredString(consumer, "rationale"));
            Assert.IsNotEmpty(RequiredString(consumer, "followUpBoundary"));
            ValidateProtectedConsumerPaths(RequiredArray(consumer, "sourcePaths"), id);

            var proofErrors = new List<string>();
            ValidateProofReferences(RequiredArray(consumer, "proofReferences"), id, proofErrors, allowPolicyBoundary: id == "W6-007");
            Assert.HasCount(0, proofErrors, string.Join(Environment.NewLine, proofErrors));
            AssertReviewMetadata(consumer);
        }
    }

    private static string GetClosurePath() => Path.Combine(
        Phase7DriverTestContext.FindRepoRoot(),
        "specs",
        FeatureDirectoryName,
        "closure-evidence.json");

    private static string GetAuditPath() => Path.Combine(
        Phase7DriverTestContext.FindRepoRoot(),
        "specs",
        "024-tv203-freevision-conformance-audit",
        "conformance-audit.json");

    private static JsonObject LoadClosureObject()
    {
        string text = File.ReadAllText(GetClosurePath());
        return JsonNode.Parse(text)?.AsObject()
            ?? throw new JsonException("Closure evidence root must be an object.");
    }

    private static JsonObject LoadAuditObject()
    {
        string text = File.ReadAllText(GetAuditPath());
        return JsonNode.Parse(text)?.AsObject()
            ?? throw new JsonException("Conformance audit root must be an object.");
    }

    private static JsonObject RequiredObject(JsonObject source, string name) =>
        source[name]?.AsObject() ?? throw new JsonException($"Required object is missing: {name}");

    private static JsonArray RequiredArray(JsonObject source, string name) =>
        source[name]?.AsArray() ?? throw new JsonException($"Required array is missing: {name}");

    private static string RequiredString(JsonObject source, string name)
    {
        string value = source[name]?.GetValue<string>()?.Trim() ?? string.Empty;
        return value.Length > 0
            ? value
            : throw new JsonException($"Required string is missing or empty: {name}");
    }

    private static string[] StringValues(JsonArray values) =>
        values.Select(value => value?.GetValue<string>() ?? string.Empty).ToArray();

    private static IEnumerable<string> ValidateCompleteDataset(JsonObject root)
    {
        var errors = new List<string>();
        JsonObject run = RequiredObject(root, "run");
        JsonObject[] findings = RequiredArray(root, "findingClosures").OfType<JsonObject>().ToArray();
        JsonObject[] slices = RequiredArray(root, "integrationSlices").OfType<JsonObject>().ToArray();
        JsonObject[] consumers = RequiredArray(root, "consumerReadiness").OfType<JsonObject>().ToArray();
        JsonObject[] governance = RequiredArray(root, "governanceDecisions").OfType<JsonObject>().ToArray();
        JsonObject[] validations = RequiredArray(root, "validationEvidence").OfType<JsonObject>().ToArray();

        ValidateExactIds(findings, "findingId", ExpectedIds("F", 13), errors);
        ValidateExactIds(slices, "sliceId", Enumerable.Range(1, 7).Select(value => $"R-028-{value:000}"), errors);
        ValidateExactIds(
            consumers,
            "consumerId",
            Enumerable.Range(1, 6).Select(value => $"W5-{value:000}")
                .Concat(Enumerable.Range(1, 7).Select(value => $"W6-{value:000}")),
            errors,
            allowAdditional: true);

        foreach (JsonObject finding in findings)
        {
            string id = RequiredString(finding, "findingId");
            string decision = RequiredString(finding, "closureDecision");
            if (!AllowedFindingDecisions.Contains(decision))
                errors.Add($"{id} has unknown finding decision {decision}.");
            ValidateRequiredFields(
                finding,
                id,
                errors,
                "contractId", "ownerFeature", "originalObservation", "redProof", "historicalIntent",
                "freeVisionRelation", "consumerScope", "apiImpact", "a11yImpact", "platformImpact",
                "evidencePath", "owner", "reviewer", "reviewDate", "residualRisk", "followUp", "reevaluationTrigger");
            ValidateExistingPaths(RequiredArray(finding, "mergedChangePaths"), id, errors);
            ValidateProofReferences(RequiredArray(finding, "realPathProofs"), id, errors);
        }

        foreach (JsonObject slice in slices)
        {
            string id = RequiredString(slice, "sliceId");
            ValidateRequiredFields(
                slice,
                id,
                errors,
                "title", "productionEntry", "assertions", "negativeOrFallback", "helperRole", "proofLimit",
                "result", "platforms", "a11yEvidence", "owner", "reviewer", "reviewDate", "evidencePath",
                "residualRisk", "followUp", "reevaluationTrigger");
            ValidateProofReferences(RequiredArray(slice, "proofReferences"), id, errors);
        }

        foreach (JsonObject consumer in consumers)
        {
            string id = RequiredString(consumer, "consumerId");
            string decision = RequiredString(consumer, "decision");
            if (!AllowedConsumerDecisions.Contains(decision))
                errors.Add($"{id} has unknown consumer decision {decision}.");
            ValidateRequiredFields(
                consumer,
                id,
                errors,
                "flow", "decision", "rationale", "wave", "residualRisk", "followUpBoundary", "owner",
                "reviewer", "reviewDate", "evidencePath", "reevaluationTrigger");
            ValidateExistingPaths(RequiredArray(consumer, "sourcePaths"), id, errors);
            ValidateProofReferences(RequiredArray(consumer, "proofReferences"), id, errors, allowPolicyBoundary: id == "W6-007");
        }

        string[] expectedPresets =
        {
            "security-governance",
            "architecture-governance",
            "isaqb-architecture-governance",
            "a11y-governance",
            "cross-platform-governance",
            "agent-parity-governance",
            "autonomous-run-governance",
        };
        ValidateExactIds(governance, "preset", expectedPresets, errors);
        foreach (JsonObject row in governance)
            ValidateRequiredFields(row, RequiredString(row, "preset"), errors, "version", "checkpoint", "applicability", "rationale", "evidencePath", "owner", "reviewer", "reviewDate", "result", "residualRisk", "followUp", "reevaluationTrigger");

        if (validations.Length == 0)
            errors.Add("validationEvidence must contain at least one complete row.");
        foreach (JsonObject row in validations)
            ValidateRequiredFields(row, RequiredString(row, "validationId"), errors, "command", "scope", "candidateHead", "runnerOrPlatform", "result", "metric", "failureBoundary", "evidencePath", "owner", "reviewer", "reviewDate", "residualRisk", "followUp", "reevaluationTrigger", "buildCounter");

        bool ready = RequiredString(run, "existingGateDecision") == "ReadyForTerminalGuiAudit";
        if (ready && findings.Length != 13)
            errors.Add("ready gate requires all thirteen finding rows.");
        if (ready && findings.Any(row => RequiredString(row, "closureDecision") is not ("Closed" or "AlreadySatisfiedWithNewProof")))
            errors.Add("ready gate requires closed finding decisions.");
        if (ready && (slices.Length != 7 || slices.Any(row => RequiredString(row, "result") != "Pass")))
            errors.Add("ready gate requires seven passing integration slices.");
        if (ready && consumers.Any(row => RequiredString(row, "decision") is "SmallFrameworkFix" or "ProductDecision"))
            errors.Add("ready gate requires non-blocking consumer decisions.");
        if (ready && (governance.Length != expectedPresets.Length || governance.Any(row => RequiredString(row, "applicability") == "Open")))
            errors.Add("ready gate requires complete governance decisions.");
        if (ready && (validations.Length == 0 || validations.Any(row => RequiredString(row, "result") is "Fail" or "Open")))
            errors.Add("ready gate requires passing or justified N/A validation evidence.");

        return errors;
    }

    private static IEnumerable<string> ExpectedIds(string prefix, int count) =>
        Enumerable.Range(1, count).Select(value => $"{prefix}{value:000}");

    private static void ValidateExactIds(
        IEnumerable<JsonObject> rows,
        string field,
        IEnumerable<string> expected,
        ICollection<string> errors,
        bool allowAdditional = false)
    {
        string[] actual = rows.Select(row => RequiredString(row, field)).ToArray();
        foreach (IGrouping<string, string> duplicate in actual.GroupBy(value => value, StringComparer.Ordinal).Where(group => group.Count() > 1))
            errors.Add($"duplicate {field}: {duplicate.Key}");

        string[] expectedIds = expected.ToArray();
        foreach (string missing in expectedIds.Except(actual, StringComparer.Ordinal))
            errors.Add($"missing {field}: {missing}");
        if (!allowAdditional)
            foreach (string unknown in actual.Except(expectedIds, StringComparer.Ordinal))
                errors.Add($"unknown {field}: {unknown}");
    }

    private static void ValidateRequiredFields(JsonObject row, string id, ICollection<string> errors, params string[] fields)
    {
        foreach (string field in fields)
        {
            try
            {
                _ = RequiredString(row, field);
            }
            catch (JsonException)
            {
                errors.Add($"{id} is missing required field {field}.");
            }
        }
    }

    private static void ValidateExistingPaths(JsonArray paths, string id, ICollection<string> errors)
    {
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        if (paths.Count == 0)
            errors.Add($"{id} requires at least one repository path.");
        foreach (JsonNode? node in paths)
        {
            string path = node?.GetValue<string>() ?? string.Empty;
            if (!File.Exists(Path.Combine(repoRoot, path)) && !Directory.Exists(Path.Combine(repoRoot, path)))
                errors.Add($"{id} references missing path {path}.");
        }
    }

    private static void ValidateProofReferences(JsonArray references, string id, ICollection<string> errors, bool allowPolicyBoundary = false)
    {
        if (references.Count == 0)
        {
            errors.Add($"{id} requires at least one proof reference.");
            return;
        }

        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        foreach (JsonNode? node in references)
        {
            string reference = node?.GetValue<string>() ?? string.Empty;
            if (allowPolicyBoundary && !reference.Contains("::", StringComparison.Ordinal))
                continue;
            string[] parts = reference.Split("::", 2, StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                errors.Add($"{id} proof must use path::method: {reference}");
                continue;
            }

            string path = Path.Combine(repoRoot, parts[0]);
            if (!File.Exists(path))
                errors.Add($"{id} references missing proof file {parts[0]}.");
            else if (!File.ReadAllText(path).Contains(parts[1], StringComparison.Ordinal))
                errors.Add($"{id} references missing proof method {parts[1]}.");
        }
    }

    private static void ValidateProtectedConsumerPaths(JsonArray paths, string id)
    {
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        Assert.IsNotEmpty(paths, $"{id} requires at least one protected consumer path.");
        foreach (JsonNode? node in paths)
        {
            string path = node?.GetValue<string>() ?? string.Empty;
            Assert.IsTrue(
                path.StartsWith("TVDEMOS/", StringComparison.Ordinal) || path.StartsWith("TVFM/", StringComparison.Ordinal),
                $"{id} escapes the protected consumer roots: {path}");
            Assert.IsTrue(File.Exists(Path.Combine(repoRoot, path)), $"{id} references missing consumer source: {path}");
        }
    }

    private static void AssertProofReferences(JsonArray references)
    {
        Assert.IsNotEmpty(references);
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();

        // Der Validator beweist nur die benannte Testoberflaeche; die Fachtests beweisen das Verhalten.
        // The validator proves only the named test surface; the domain tests prove behavior.
        foreach (JsonNode? node in references)
        {
            string reference = node?.GetValue<string>() ?? string.Empty;
            string[] parts = reference.Split("::", 2, StringSplitOptions.TrimEntries);
            Assert.HasCount(2, parts, $"Proof must use path::method: {reference}");
            string path = Path.Combine(repoRoot, parts[0]);
            Assert.IsTrue(File.Exists(path), $"Proof file is missing: {parts[0]}");
            StringAssert.Contains(File.ReadAllText(path), parts[1], $"Proof method is missing: {parts[1]}");
        }
    }

    private static void AssertReviewMetadata(JsonObject row)
    {
        Assert.IsNotEmpty(RequiredString(row, "owner"));
        Assert.IsNotEmpty(RequiredString(row, "reviewer"));
        Assert.IsTrue(DateOnly.TryParse(RequiredString(row, "reviewDate"), out _));
        Assert.IsNotEmpty(RequiredString(row, "evidencePath"));
        Assert.IsNotEmpty(RequiredString(row, "residualRisk"));
        Assert.IsNotEmpty(RequiredString(row, "reevaluationTrigger"));
    }
}
