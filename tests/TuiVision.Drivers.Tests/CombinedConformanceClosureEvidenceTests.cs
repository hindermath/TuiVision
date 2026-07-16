// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Prüft den unabhängigen gemeinsamen Konformitätsabschluss vor Wave 5.
///
/// Validates the independent combined conformance closure before Wave 5.
/// </summary>
[TestClass]
public sealed class CombinedConformanceClosureEvidenceTests
{
    private const string FeatureDirectoryName = "031-combined-conformance-closure";

    private static readonly IReadOnlySet<string> AllowedApplicability =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "Applicable",
            "N/A",
            "Open",
        };

    /// <summary>
    /// Prüft den ersten vollständigen Slice über Vertrag, Consumer, Beobachtungen und früheres Finding.
    ///
    /// Validates the first complete slice across contract, consumer, observations, and prior finding.
    /// </summary>
    [TestMethod]
    public void Test_RepresentativeSliceIsComplete()
    {
        JsonObject root = LoadClosureObject();

        JsonObject contract = RequiredObjects(root, "contractClosures")
            .Single(row => RequiredString(row, "contractId") == "C001");
        JsonObject consumer = RequiredObjects(root, "consumerClosures")
            .Single(row => RequiredString(row, "consumerId") == "W5-001");
        JsonObject tgo = RequiredObjects(root, "observationClosures")
            .Single(row => RequiredString(row, "observationId") == "TGO001");
        JsonObject mb = RequiredObjects(root, "observationClosures")
            .Single(row => RequiredString(row, "observationId") == "MB001");
        JsonObject finding = RequiredObjects(root, "priorFindingClosures")
            .Single(row => RequiredString(row, "findingId") == "F001");

        Assert.AreEqual("Pass", RequiredString(contract, "result"));
        Assert.AreEqual("Pass", RequiredString(consumer, "result"));
        Assert.AreEqual("Pass", RequiredString(tgo, "result"));
        Assert.AreEqual("Pass", RequiredString(mb, "result"));
        Assert.AreEqual("Closed", RequiredString(finding, "result"));
    }

    /// <summary>
    /// Bindet jede akzeptierte Vorgängerdatei an ihren aktuellen SHA-256-Wert.
    ///
    /// Binds every accepted predecessor file to its current SHA-256 value.
    /// </summary>
    [TestMethod]
    public void Test_AcceptedInputHashesAreExact()
    {
        JsonObject root = LoadClosureObject();
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();

        JsonObject intake = RequiredObject(root, "bindingIntake");
        AssertFileHash(repoRoot, RequiredString(intake, "path"), RequiredString(intake, "sha256"));

        JsonObject[] inputs = RequiredObjects(root, "acceptedInputs");
        Assert.HasCount(7, inputs);
        Assert.HasCount(7, inputs.Select(row => RequiredString(row, "path")).Distinct(StringComparer.Ordinal));

        foreach (JsonObject input in inputs)
        {
            Assert.AreEqual("Pass", RequiredString(input, "result"));
            AssertFileHash(repoRoot, RequiredString(input, "path"), RequiredString(input, "sha256"));
            Assert.IsNotEmpty(RequiredString(input, "role"));
            Assert.IsNotEmpty(RequiredString(input, "reevaluationTrigger"));
        }
    }

    /// <summary>
    /// Vergleicht Pins und Quellenmengen mit den drei akzeptierten Vergleichsaudits.
    ///
    /// Compares pins and source sets with the three accepted comparison audits.
    /// </summary>
    [TestMethod]
    public void Test_ExternalSourceBaselinesAreExact()
    {
        JsonObject closure = LoadClosureObject();
        JsonObject audit024 = LoadFeatureObject("024-tv203-freevision-conformance-audit", "conformance-audit.json");
        JsonObject audit029 = LoadFeatureObject("029-tv203-freevision-terminalgui-conformance-audit", "terminalgui-conformance-audit.json");
        JsonObject audit030 = LoadFeatureObject("030-tv203-magiblot-evolution-audit", "magiblot-evolution-audit.json");

        JsonObject freeVision = FindBy(closure, "externalSourceBaselines", "sourceName", "FreeVision");
        JsonObject terminalGui = FindBy(closure, "externalSourceBaselines", "sourceName", "TerminalGUI");
        JsonObject magiblot = FindBy(closure, "externalSourceBaselines", "sourceName", "MagiblotTvision");

        Assert.AreEqual(RequiredString(RequiredObject(audit024, "run"), "freeVisionCommit"), RequiredString(freeVision, "commit"));
        AssertExactValues(
            RequiredArray(freeVision, "sourceIds"),
            RequiredObjects(audit024, "externalSources").Select(row => RequiredString(row, "sourceId")));

        JsonObject terminalRun = RequiredObject(audit029, "run");
        Assert.AreEqual(RequiredString(terminalRun, "terminalGuiTagObject"), RequiredString(terminalGui, "tagObject"));
        Assert.AreEqual(RequiredString(terminalRun, "terminalGuiCommit"), RequiredString(terminalGui, "commit"));
        Assert.AreEqual(RequiredString(terminalRun, "licenseSha256"), RequiredString(terminalGui, "licenseSha256"));
        AssertExactValues(
            RequiredArray(terminalGui, "sourceIds"),
            RequiredObjects(audit029, "sources").Select(row => RequiredString(row, "sourceId")));

        JsonObject magiblotRun = RequiredObject(audit030, "run");
        Assert.AreEqual(RequiredString(magiblotRun, "magiblotCommit"), RequiredString(magiblot, "commit"));
        Assert.AreEqual(RequiredString(magiblotRun, "magiblotTree"), RequiredString(magiblot, "tree"));
        Assert.AreEqual(RequiredString(magiblotRun, "copyrightSha256"), RequiredString(magiblot, "licenseSha256"));
        AssertExactValues(
            RequiredArray(magiblot, "sourceIds"),
            RequiredObjects(audit030, "sources").Select(row => RequiredString(row, "sourceId")));

        CollectionAssert.AreEqual(new[] { 15, 25, 50 }, new[]
        {
            RequiredInt32(freeVision, "expectedSourceCount"),
            RequiredInt32(terminalGui, "expectedSourceCount"),
            RequiredInt32(magiblot, "expectedSourceCount"),
        });

        foreach (JsonObject baseline in RequiredObjects(closure, "externalSourceBaselines"))
        {
            Assert.AreEqual("Pass", RequiredString(baseline, "result"));
            AssertReviewMetadata(baseline);
        }
    }

    /// <summary>
    /// Prüft alle geschlossenen Mengen und ihre Beziehungen zu den Vorgängerdatensätzen.
    ///
    /// Validates every closed set and its relationships to predecessor datasets.
    /// </summary>
    [TestMethod]
    public void Test_CompleteDatasetRelationshipsAreExact()
    {
        JsonObject closure = LoadClosureObject();
        string[] errors = ValidateClosure(closure).ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));

        JsonObject audit024 = LoadFeatureObject("024-tv203-freevision-conformance-audit", "conformance-audit.json");
        JsonObject audit028 = LoadFeatureObject("028-pre-wave5-wave6-conformance-closure", "closure-evidence.json");
        JsonObject audit029 = LoadFeatureObject("029-tv203-freevision-terminalgui-conformance-audit", "terminalgui-conformance-audit.json");
        JsonObject audit030 = LoadFeatureObject("030-tv203-magiblot-evolution-audit", "magiblot-evolution-audit.json");
        JsonObject combined = LoadFeatureObject("030-tv203-magiblot-evolution-audit", "combined-conformance-findings.json");

        // Die Closure vergleicht Quellzeilen direkt; gleiche Summen allein könnten Duplikate verdecken.
        // The closure compares source rows directly because matching totals alone could hide duplicates.
        foreach (JsonObject row in RequiredObjects(closure, "contractClosures"))
        {
            string contractId = RequiredString(row, "contractId");
            JsonObject source = FindBy(audit024, "contracts", "contractId", contractId);
            JsonObject terminalRelation = FindBy(audit029, "relations", "contractId", contractId);
            JsonObject terminalObservation = FindBy(audit029, "observations", "contractId", contractId);
            JsonObject magiblotRelation = FindBy(audit030, "relations", "contractId", contractId);
            JsonObject magiblotObservation = FindBy(audit030, "observations", "contractId", contractId);

            Assert.AreEqual(RequiredString(source, "primaryDecision"), RequiredString(row, "feature024Decision"));
            Assert.AreEqual(RequiredString(source, "freeVisionRelation"), RequiredString(row, "freeVisionRelation"));
            Assert.AreEqual(RequiredString(terminalRelation, "terminalGuiRelation"), RequiredString(row, "terminalGuiRelation"));
            Assert.AreEqual(RequiredString(terminalObservation, "findingId"), RequiredString(row, "tgoObservationId"));
            Assert.AreEqual(RequiredString(magiblotRelation, "magiblotRelation"), RequiredString(row, "magiblotRelation"));
            Assert.AreEqual(RequiredString(magiblotObservation, "observationId"), RequiredString(row, "mbObservationId"));
            Assert.AreEqual(RequiredString(source, "proof"), RequiredString(row, "proof"));
            AssertExactValues(RequiredArray(row, "freeVisionSourceIds"), StringValues(RequiredArray(source, "freeVisionSourceIds")));
            AssertExactValues(RequiredArray(row, "terminalGuiSourceIds"), StringValues(RequiredArray(terminalRelation, "terminalGuiSourceIds")));
            AssertExactValues(RequiredArray(row, "magiblotSourceIds"), StringValues(RequiredArray(magiblotRelation, "magiblotSourceIds")));
            AssertProofReferences(RequiredString(row, "proof"));
            AssertReviewMetadata(row);
        }

        JsonObject terminalLicense = FindBy(audit029, "sources", "sourceId", "TGSR001");
        AssertExactValues(
            RequiredArray(terminalLicense, "contractIds"),
            RequiredObjects(closure, "contractClosures").Select(row => RequiredString(row, "contractId")));

        foreach (JsonObject row in RequiredObjects(closure, "consumerClosures"))
        {
            string id = RequiredString(row, "consumerId");
            JsonObject source028 = FindBy(audit028, "consumerReadiness", "consumerId", id);
            JsonObject source029 = FindBy(audit029, "consumers", "consumerId", id);
            JsonObject source030 = FindBy(audit030, "consumers", "consumerId", id);

            AssertExactValues(RequiredArray(row, "sourcePaths"), StringValues(RequiredArray(source028, "sourcePaths")));
            AssertExactValues(RequiredArray(row, "contractIds"), StringValues(RequiredArray(source028, "contractIds")));
            AssertExactValues(RequiredArray(row, "contractIds"), StringValues(RequiredArray(source029, "contractIds")));
            AssertExactValues(RequiredArray(row, "contractIds"), StringValues(RequiredArray(source030, "contractIds")));
            Assert.AreEqual("Pass", RequiredString(row, "terminalGuiAgreement"));
            Assert.AreEqual("Pass", RequiredString(row, "magiblotAgreement"));
            Assert.AreEqual("Pass", RequiredString(row, "result"));
            AssertReviewMetadata(row);
        }

        foreach (JsonObject row in RequiredObjects(closure, "observationClosures"))
        {
            string id = RequiredString(row, "observationId");
            JsonObject disposition = FindBy(combined, "dispositions", "observationId", id);
            Assert.AreEqual(RequiredString(disposition, "contractId"), RequiredString(row, "contractId"));
            Assert.AreEqual(RequiredString(disposition, "disposition"), RequiredString(row, "combinedDisposition"));
            Assert.AreEqual(RequiredString(disposition, "canonicalFindingId"), RequiredString(row, "canonicalFindingId"));
            Assert.AreEqual("Pass", RequiredString(row, "result"));
            AssertProofReferences(RequiredString(row, "proof"));
            AssertReviewMetadata(row);
        }
    }

    /// <summary>
    /// Beweist die geschlossene Finding-Menge und die leeren Hardening-Grenzen.
    ///
    /// Proves the closed finding set and empty hardening boundaries.
    /// </summary>
    [TestMethod]
    public void Test_NoFindingWasSuppressed()
    {
        JsonObject closure = LoadClosureObject();
        JsonObject audit024 = LoadFeatureObject("024-tv203-freevision-conformance-audit", "conformance-audit.json");
        JsonObject audit028 = LoadFeatureObject("028-pre-wave5-wave6-conformance-closure", "closure-evidence.json");

        foreach (JsonObject row in RequiredObjects(closure, "priorFindingClosures"))
        {
            string id = RequiredString(row, "findingId");
            JsonObject resolution = FindBy(audit024, "resolutions", "findingId", id);
            JsonObject sourceClosure = FindBy(audit028, "findingClosures", "findingId", id);

            Assert.AreEqual("Closed", RequiredString(row, "result"));
            Assert.AreEqual(RequiredString(resolution, "resolutionId"), RequiredString(row, "resolutionId"));
            Assert.AreEqual(RequiredString(resolution, "status"), RequiredString(row, "resolutionStatus"));
            Assert.AreEqual(RequiredString(sourceClosure, "closureDecision"), RequiredString(row, "feature028Decision"));
            foreach (string proof in StringValues(RequiredArray(row, "proofReferences")))
                AssertProofReferences(proof);
            AssertReviewMetadata(row);
        }

        Assert.HasCount(0, RequiredArray(closure, "canonicalFindings"));
        Assert.HasCount(0, RequiredArray(closure, "productDecisions"));
        Assert.HasCount(0, RequiredArray(closure, "dependencyEdges"));
        Assert.HasCount(0, RequiredArray(closure, "hardeningIntakes"));

        JsonObject[] owners = RequiredObjects(closure, "ownerGroups");
        Assert.HasCount(3, owners);
        foreach (JsonObject owner in owners)
        {
            Assert.HasCount(0, RequiredArray(owner, "findingIds"));
            Assert.AreEqual("Pass", RequiredString(owner, "result"));
            AssertReviewMetadata(owner);
        }
    }

    /// <summary>
    /// Prüft die vollständige Governance-Metadaten- und Applicability-Grenze.
    ///
    /// Validates complete governance metadata and applicability boundaries.
    /// </summary>
    [TestMethod]
    public void Test_GovernanceRowsAreComplete()
    {
        JsonObject[] rows = RequiredObjects(LoadClosureObject(), "governanceDecisions");
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

        CollectionAssert.AreEquivalent(
            expectedPresets,
            rows.Select(row => RequiredString(row, "presetName")).Distinct(StringComparer.Ordinal).ToArray());

        foreach (JsonObject row in rows)
        {
            Assert.Contains(RequiredString(row, "applicability"), AllowedApplicability);
            AssertReviewMetadata(row);
            Assert.IsNotEmpty(RequiredString(row, "checkpoint"));
            Assert.IsNotEmpty(RequiredString(row, "presetVersion"));
            Assert.IsNotEmpty(RequiredString(row, "result"));
            if (RequiredString(row, "applicability") == "N/A")
                Assert.IsNotEmpty(RequiredString(row, "reevaluationTrigger"));
        }
    }

    /// <summary>
    /// Prüft die kausale Grenze zwischen reviewtem Feature-Head und Closeout.
    ///
    /// Validates the causal boundary between reviewed feature head and closeout.
    /// </summary>
    [TestMethod]
    public void Test_WaveTransitionRequiresCausalCloseout()
    {
        JsonObject root = LoadClosureObject();
        JsonObject run = RequiredObject(root, "run");
        JsonObject transition = RequiredObject(root, "waveTransition");
        string featureDirectory = GetFeatureDirectory();
        string closeoutPath = Path.Combine(featureDirectory, "delivery-closeout.md");
        string gatePath = Path.Combine(featureDirectory, "pre-wave-gate.md");

        Assert.AreEqual("BlockedPendingCausalClosure", RequiredString(run, "featureHeadWave5State"));
        Assert.AreEqual("BlockedPendingCausalClosure", RequiredString(run, "featureHeadWave6State"));
        Assert.AreEqual("Eligible", RequiredString(run, "postMergeWave5Target"));
        Assert.AreEqual("ConditionallyReady", RequiredString(run, "postMergeWave6Target"));

        // Der Markerzustand darf nur durch bereits vorhandene kausale Merge-Evidence wechseln.
        // Marker state may change only when causal merge evidence already exists.
        if (!File.Exists(closeoutPath))
        {
            Assert.IsTrue(File.Exists(gatePath), "Feature-head gate evidence is missing.");
            string gate = File.ReadAllText(gatePath);
            StringAssert.Contains(gate, "BlockedPendingCausalClosure");
            Assert.AreEqual("N/A", RequiredString(transition, "reviewedHead"));
            Assert.AreEqual("N/A", RequiredString(transition, "featureMerge"));
            return;
        }

        string closeout = File.ReadAllText(closeoutPath);
        StringAssert.Contains(closeout, "Eligible");
        StringAssert.Contains(closeout, "ConditionallyReady");
        Assert.AreNotEqual("N/A", RequiredString(transition, "reviewedHead"));
        Assert.AreNotEqual("N/A", RequiredString(transition, "featureMerge"));
        Assert.AreEqual("Pass", RequiredString(transition, "result"));
    }

    /// <summary>
    /// Beweist die Ablehnung fehlender, doppelter, unbekannter und verfrühter Evidence.
    ///
    /// Proves rejection of missing, duplicate, unknown, and premature evidence.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsMalformedAndContradictoryData()
    {
        Assert.Throws<JsonException>(() => JsonNode.Parse("{"));

        JsonObject missing = DeepClone();
        RequiredArray(missing, "contractClosures").RemoveAt(0);
        Assert.IsTrue(ValidateClosure(missing).Any(error => error.Contains("missing contractId", StringComparison.Ordinal)));

        JsonObject duplicate = DeepClone();
        RequiredArray(duplicate, "observationClosures").Add(RequiredArray(duplicate, "observationClosures")[0]!.DeepClone());
        Assert.IsTrue(ValidateClosure(duplicate).Any(error => error.Contains("duplicate observationId", StringComparison.Ordinal)));

        JsonObject unknown = DeepClone();
        RequiredArray(unknown, "observationClosures")[0]!.AsObject()["combinedDisposition"] = "Applicable";
        Assert.IsTrue(ValidateClosure(unknown).Any(error => error.Contains("unknown disposition", StringComparison.Ordinal)));

        JsonObject owner = DeepClone();
        RequiredArray(owner, "ownerGroups")[0]!.AsObject()["findingIds"] = new JsonArray("CF001");
        Assert.IsTrue(ValidateClosure(owner).Any(error => error.Contains("non-empty owner group", StringComparison.Ordinal)));

        JsonObject finding = DeepClone();
        RequiredArray(finding, "canonicalFindings").Add(new JsonObject { ["findingId"] = "CF001" });
        Assert.IsTrue(ValidateClosure(finding).Any(error => error.Contains("canonical findings must be empty", StringComparison.Ordinal)));

        JsonObject intake = DeepClone();
        RequiredArray(intake, "hardeningIntakes").Add(new JsonObject { ["featureNumber"] = 32 });
        Assert.IsTrue(ValidateClosure(intake).Any(error => error.Contains("hardening intakes must be empty", StringComparison.Ordinal)));

        JsonObject premature = DeepClone();
        RequiredObject(premature, "run")["featureHeadWave5State"] = "Eligible";
        Assert.IsTrue(ValidateClosure(premature).Any(error => error.Contains("feature-head Wave 5", StringComparison.Ordinal)));

        JsonObject excessiveWave6 = DeepClone();
        RequiredObject(excessiveWave6, "run")["postMergeWave6Target"] = "Eligible";
        Assert.IsTrue(ValidateClosure(excessiveWave6).Any(error => error.Contains("post-merge Wave 6", StringComparison.Ordinal)));

        JsonObject sourceCount = DeepClone();
        RequiredArray(FindBy(sourceCount, "externalSourceBaselines", "sourceName", "FreeVision"), "sourceIds").RemoveAt(0);
        Assert.IsTrue(ValidateClosure(sourceCount).Any(error => error.Contains("source count", StringComparison.Ordinal)));

        JsonObject sourcePin = DeepClone();
        FindBy(sourcePin, "externalSourceBaselines", "sourceName", "TerminalGUI")["commit"] = new string('0', 40);
        Assert.IsTrue(ValidateClosure(sourcePin).Any(error => error.Contains("TerminalGUI pin", StringComparison.Ordinal)));

        JsonObject governanceNa = DeepClone();
        JsonObject naRow = RequiredObjects(governanceNa, "governanceDecisions")
            .First(row => RequiredString(row, "applicability") == "N/A");
        naRow["reevaluationTrigger"] = string.Empty;
        Assert.IsTrue(ValidateClosure(governanceNa).Any(error => error.Contains("N/A governance row", StringComparison.Ordinal)));

        JsonObject governanceOpen = DeepClone();
        JsonObject openRow = RequiredObjects(governanceOpen, "governanceDecisions")
            .First(row => RequiredString(row, "applicability") == "Applicable");
        openRow["applicability"] = "Open";
        openRow["followUp"] = string.Empty;
        Assert.IsTrue(ValidateClosure(governanceOpen).Any(error => error.Contains("Open governance row", StringComparison.Ordinal)));
    }

    private static JsonObject DeepClone() => (JsonObject)LoadClosureObject().DeepClone();

    private static JsonObject LoadClosureObject() =>
        LoadFeatureObject(FeatureDirectoryName, "closure-evidence.json");

    private static JsonObject LoadFeatureObject(string featureDirectoryName, string fileName)
    {
        string path = Path.Combine(
            Phase7DriverTestContext.FindRepoRoot(),
            "specs",
            featureDirectoryName,
            fileName);

        return JsonNode.Parse(File.ReadAllText(path))?.AsObject()
            ?? throw new JsonException($"Evidence root must be an object: {path}");
    }

    private static string GetFeatureDirectory() => Path.Combine(
        Phase7DriverTestContext.FindRepoRoot(),
        "specs",
        FeatureDirectoryName);

    private static JsonObject FindBy(JsonObject root, string arrayName, string propertyName, string value) =>
        RequiredObjects(root, arrayName)
            .Single(row => RequiredString(row, propertyName) == value);

    private static JsonObject RequiredObject(JsonObject parent, string propertyName) =>
        parent[propertyName]?.AsObject()
        ?? throw new JsonException($"Required object is missing: {propertyName}");

    private static JsonObject[] RequiredObjects(JsonObject parent, string propertyName) =>
        RequiredArray(parent, propertyName).OfType<JsonObject>().ToArray();

    private static JsonArray RequiredArray(JsonObject parent, string propertyName) =>
        parent[propertyName]?.AsArray()
        ?? throw new JsonException($"Required array is missing: {propertyName}");

    private static string RequiredString(JsonObject parent, string propertyName)
    {
        string value = parent[propertyName]?.GetValue<string>()?.Trim() ?? string.Empty;
        return value.Length > 0
            ? value
            : throw new JsonException($"Required string is missing or empty: {propertyName}");
    }

    private static int RequiredInt32(JsonObject parent, string propertyName) =>
        parent[propertyName]?.GetValue<int>()
        ?? throw new JsonException($"Required integer is missing: {propertyName}");

    private static bool HasText(JsonObject parent, string propertyName) =>
        !string.IsNullOrWhiteSpace(OptionalString(parent, propertyName));

    private static string OptionalString(JsonObject parent, string propertyName) =>
        parent[propertyName]?.GetValue<string>()?.Trim() ?? string.Empty;

    private static string[] StringValues(JsonArray values) =>
        values.Select(value => value?.GetValue<string>() ?? string.Empty).ToArray();

    private static IEnumerable<string> ValidateClosure(JsonObject root)
    {
        var errors = new List<string>();
        JsonObject closedSets = RequiredObject(root, "closedSets");
        JsonObject run = RequiredObject(root, "run");
        JsonObject[] contracts = RequiredObjects(root, "contractClosures");
        JsonObject[] consumers = RequiredObjects(root, "consumerClosures");
        JsonObject[] observations = RequiredObjects(root, "observationClosures");
        JsonObject[] findings = RequiredObjects(root, "priorFindingClosures");
        JsonObject[] owners = RequiredObjects(root, "ownerGroups");
        JsonObject[] sourceBaselines = RequiredObjects(root, "externalSourceBaselines");
        JsonObject[] governance = RequiredObjects(root, "governanceDecisions");

        ValidateExactIds(contracts, "contractId", StringValues(RequiredArray(closedSets, "contractIds")), errors);
        ValidateExactIds(consumers, "consumerId", StringValues(RequiredArray(closedSets, "consumerIds")), errors);
        ValidateExactIds(
            observations,
            "observationId",
            StringValues(RequiredArray(closedSets, "tgoObservationIds"))
                .Concat(StringValues(RequiredArray(closedSets, "mbObservationIds"))),
            errors);
        ValidateExactIds(findings, "findingId", StringValues(RequiredArray(closedSets, "priorFindingIds")), errors);
        ValidateExactIds(owners, "primaryOwner", StringValues(RequiredArray(closedSets, "ownerGroupIds")), errors);

        var contractIds = contracts.Select(row => RequiredString(row, "contractId")).ToHashSet(StringComparer.Ordinal);
        var consumerIds = consumers.Select(row => RequiredString(row, "consumerId")).ToHashSet(StringComparer.Ordinal);
        var observationIds = observations.Select(row => RequiredString(row, "observationId")).ToHashSet(StringComparer.Ordinal);

        foreach (JsonObject contract in contracts)
        {
            string id = RequiredString(contract, "contractId");
            if (!observationIds.Contains(RequiredString(contract, "tgoObservationId"))
                || !observationIds.Contains(RequiredString(contract, "mbObservationId")))
            {
                errors.Add($"{id} has an orphaned observation.");
            }

            if (RequiredString(contract, "combinedDisposition") != "NonFinding")
                errors.Add($"{id} has unknown disposition {RequiredString(contract, "combinedDisposition")}.");

            foreach (string consumerId in StringValues(RequiredArray(contract, "consumerIds")))
                if (!consumerIds.Contains(consumerId))
                    errors.Add($"{id} references unknown consumerId {consumerId}.");
        }

        foreach (JsonObject consumer in consumers)
        {
            string id = RequiredString(consumer, "consumerId");
            foreach (string contractId in StringValues(RequiredArray(consumer, "contractIds")))
                if (!contractIds.Contains(contractId)
                    && !(id == "W6-007" && contractId == "ProductPolicy"))
                {
                    errors.Add($"{id} references unknown contractId {contractId}.");
                }
        }

        foreach (JsonObject observation in observations)
        {
            string id = RequiredString(observation, "observationId");
            if (!contractIds.Contains(RequiredString(observation, "contractId")))
                errors.Add($"{id} references unknown contractId.");
            if (RequiredString(observation, "combinedDisposition") != "NonFinding")
                errors.Add($"{id} has unknown disposition {RequiredString(observation, "combinedDisposition")}.");
            if (RequiredString(observation, "canonicalFindingId") != "None")
                errors.Add($"{id} references a canonical finding.");
        }

        foreach (JsonObject owner in owners)
            if (RequiredArray(owner, "findingIds").Count > 0)
                errors.Add($"non-empty owner group: {RequiredString(owner, "primaryOwner")}");

        foreach (JsonObject baseline in sourceBaselines)
        {
            string name = RequiredString(baseline, "sourceName");
            if (RequiredArray(baseline, "sourceIds").Count != RequiredInt32(baseline, "expectedSourceCount"))
                errors.Add($"{name} source count does not match the closed baseline.");
        }

        JsonObject freeVision = sourceBaselines.Single(row => RequiredString(row, "sourceName") == "FreeVision");
        if (RequiredString(freeVision, "commit") != "ffc03b34d8cafb85ddcf0686de1c5551601dacb2")
            errors.Add("FreeVision pin does not match the accepted baseline.");

        JsonObject terminalGui = sourceBaselines.Single(row => RequiredString(row, "sourceName") == "TerminalGUI");
        if (RequiredString(terminalGui, "tagObject") != "4b812e44798f2c7567afec50ba9a9293b6beb6de"
            || RequiredString(terminalGui, "commit") != "d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3"
            || RequiredString(terminalGui, "licenseSha256") != "2a7331c273b7c121f5e1f6f10e13d279a739ac310c49b56f2fb251d0490988d0")
        {
            errors.Add("TerminalGUI pin or license does not match the accepted baseline.");
        }

        JsonObject magiblot = sourceBaselines.Single(row => RequiredString(row, "sourceName") == "MagiblotTvision");
        if (RequiredString(magiblot, "commit") != "57b6f56b38e0ee75240a80a10ee0e11470c24693"
            || RequiredString(magiblot, "tree") != "96dd03873955689ff0a79f6c8107a8148fe1ebd6"
            || RequiredString(magiblot, "licenseSha256") != "66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548")
        {
            errors.Add("MagiblotTvision pin, tree, or COPYRIGHT does not match the accepted baseline.");
        }

        if (RequiredArray(root, "canonicalFindings").Count > 0)
            errors.Add("canonical findings must be empty.");
        if (RequiredArray(root, "productDecisions").Count > 0)
            errors.Add("product decisions must be empty.");
        if (RequiredArray(root, "dependencyEdges").Count > 0)
            errors.Add("dependency edges must be empty.");
        if (RequiredArray(root, "hardeningIntakes").Count > 0)
            errors.Add("hardening intakes must be empty.");

        foreach (JsonObject row in governance)
        {
            string applicability = OptionalString(row, "applicability");
            if (!AllowedApplicability.Contains(applicability))
                errors.Add($"unknown governance applicability: {applicability}");
            if (applicability == "N/A" && !HasText(row, "reevaluationTrigger"))
                errors.Add("N/A governance row requires a re-evaluation trigger.");
            if (applicability == "Open"
                && (!HasText(row, "owner") || !HasText(row, "followUp") || !HasText(row, "reevaluationTrigger")))
            {
                errors.Add("Open governance row requires owner, follow-up, and re-evaluation trigger.");
            }
        }

        if (RequiredString(run, "featureHeadWave5State") != "BlockedPendingCausalClosure")
            errors.Add("feature-head Wave 5 must remain blocked.");
        if (RequiredString(run, "featureHeadWave6State") != "BlockedPendingCausalClosure")
            errors.Add("feature-head Wave 6 must remain blocked.");
        if (RequiredString(run, "postMergeWave6Target") != "ConditionallyReady")
            errors.Add("post-merge Wave 6 must not exceed ConditionallyReady.");

        return errors;
    }

    private static void ValidateExactIds(
        IEnumerable<JsonObject> rows,
        string propertyName,
        IEnumerable<string> expectedValues,
        ICollection<string> errors)
    {
        string[] actual = rows.Select(row => RequiredString(row, propertyName)).ToArray();
        string[] expected = expectedValues.ToArray();

        foreach (IGrouping<string, string> duplicate in actual
                     .GroupBy(value => value, StringComparer.Ordinal)
                     .Where(group => group.Count() > 1))
        {
            errors.Add($"duplicate {propertyName}: {duplicate.Key}");
        }

        foreach (string missing in expected.Except(actual, StringComparer.Ordinal))
            errors.Add($"missing {propertyName}: {missing}");
        foreach (string unknown in actual.Except(expected, StringComparer.Ordinal))
            errors.Add($"unknown {propertyName}: {unknown}");
    }

    private static void AssertReviewMetadata(JsonObject row)
    {
        foreach (string property in new[]
                 {
                     "owner",
                     "reviewer",
                     "reviewDate",
                     "evidencePath",
                     "residualRisk",
                     "followUp",
                     "reevaluationTrigger",
                 })
        {
            Assert.IsNotEmpty(RequiredString(row, property));
        }
    }

    private static void AssertFileHash(string repoRoot, string relativePath, string expectedHash)
    {
        string path = Path.Combine(repoRoot, relativePath);
        Assert.IsTrue(File.Exists(path), $"Accepted input is missing: {relativePath}");
        string actualHash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))).ToLowerInvariant();
        Assert.AreEqual(expectedHash, actualHash, $"Accepted input hash drift: {relativePath}");
    }

    private static void AssertProofReferences(string references)
    {
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        foreach (string reference in references.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            string[] parts = reference.Split("::", 2, StringSplitOptions.TrimEntries);
            Assert.HasCount(2, parts, $"Proof reference must use path::method: {reference}");
            string path = Path.Combine(repoRoot, parts[0]);
            Assert.IsTrue(File.Exists(path), $"Proof file is missing: {parts[0]}");
            StringAssert.Contains(File.ReadAllText(path), parts[1], $"Proof method is missing: {reference}");
        }
    }

    private static void AssertExactValues(JsonArray actualValues, IEnumerable<string> expectedValues) =>
        CollectionAssert.AreEqual(expectedValues.ToArray(), StringValues(actualValues));
}
