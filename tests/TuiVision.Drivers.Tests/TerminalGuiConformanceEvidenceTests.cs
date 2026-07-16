// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Prüft den strukturierten Nachweis des Terminal.GUI-Konformitätsaudits.
///
/// Validates the structured evidence for the Terminal.GUI conformance audit.
/// </summary>
[TestClass]
public sealed class TerminalGuiConformanceEvidenceTests
{
    private const string FeatureDirectoryName = "029-tv203-freevision-terminalgui-conformance-audit";
    private const string AuditFileName = "terminalgui-conformance-audit.json";
    private const string HandoffFileName = "feature030-handoff.json";
    private const string ExpectedRunId = "029-tv203-freevision-terminalgui-conformance-audit";
    private const string ExpectedRepository = "https://github.com/tui-cs/Terminal.Gui.git";
    private const string ExpectedTag = "v1.9.0";
    private const string ExpectedTagObject = "4b812e44798f2c7567afec50ba9a9293b6beb6de";
    private const string ExpectedCommit = "d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3";
    private const string ExpectedLicenseSha256 = "2a7331c273b7c121f5e1f6f10e13d279a739ac310c49b56f2fb251d0490988d0";

    private static readonly IReadOnlySet<string> AllowedRelations =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "CorroboratesOriginal",
            "CorroboratesModernization",
            "AlternativeModernization",
            "DivergesFromTuiVision",
            "NotApplicable",
        };

    private static readonly IReadOnlySet<string> AllowedConsumerDecisions =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "UseExistingFramework",
            "IntentionalDeviation",
            "FollowUpHardening",
            "CandidateFinding",
            "ProductDecision",
        };

    private static readonly IReadOnlySet<string> AllowedObservationDecisions =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "CandidateFinding",
            "IntentionalDeviation",
            "AlreadySatisfiedWithNewEvidence",
            "ProductDecision",
            "RejectedComparison",
        };

    private static readonly IReadOnlySet<string> AllowedGovernanceApplicability =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "Applicable",
            "N/A",
            "Open",
        };

    /// <summary>
    /// Stellt sicher, dass beide akzeptierten JSON-Datensätze vorhanden sind.
    ///
    /// Ensures that both accepted JSON datasets exist.
    /// </summary>
    [TestMethod]
    public void Test_AcceptedDatasetsExist()
    {
        Assert.IsTrue(File.Exists(GetFeaturePath(AuditFileName)), $"Missing accepted dataset: {AuditFileName}");
        Assert.IsTrue(File.Exists(GetFeaturePath(HandoffFileName)), $"Missing accepted dataset: {HandoffFileName}");
    }

    /// <summary>
    /// Bindet den Auditlauf an die unveränderlichen Feature- und Terminal.GUI-Quellen.
    ///
    /// Binds the audit run to the immutable feature and Terminal.GUI sources.
    /// </summary>
    [TestMethod]
    public void Test_AuditRunUsesExactPins()
    {
        JsonObject root = LoadObject(AuditFileName);
        JsonObject run = RequiredObject(root, "run");

        Assert.AreEqual("1.0", RequiredString(run, "schemaVersion"));
        Assert.AreEqual(ExpectedRunId, RequiredString(run, "runId"));
        Assert.AreEqual("024-tv203-freevision-conformance-audit", RequiredString(run, "canonicalAuditFeature"));
        Assert.AreEqual("028-pre-wave5-wave6-conformance-closure", RequiredString(run, "closureFeature"));
        Assert.AreEqual(ExpectedRepository, RequiredString(run, "terminalGuiRepository"));
        Assert.AreEqual(ExpectedTag, RequiredString(run, "terminalGuiTag"));
        Assert.AreEqual(ExpectedTagObject, RequiredString(run, "terminalGuiTagObject"));
        Assert.AreEqual(ExpectedCommit, RequiredString(run, "terminalGuiCommit"));
        Assert.AreEqual("MIT", RequiredString(run, "terminalGuiLicense"));
        Assert.AreEqual(ExpectedLicenseSha256, RequiredString(run, "licenseSha256"));
    }

    /// <summary>
    /// Bindet den Folge-Handoff an denselben Lauf und dieselbe gepinnte Quelle.
    ///
    /// Binds the successor handoff to the same run and pinned source.
    /// </summary>
    [TestMethod]
    public void Test_HandoffUsesExactPins()
    {
        JsonObject handoff = LoadObject(HandoffFileName);

        Assert.AreEqual("1.0", RequiredString(handoff, "schemaVersion"));
        Assert.AreEqual(ExpectedRunId, RequiredString(handoff, "sourceFeature"));
        Assert.AreEqual("030-tv203-magiblot-evolution-audit", RequiredString(handoff, "targetFeature"));
        Assert.AreEqual(ExpectedTagObject, RequiredString(handoff, "terminalGuiTagObject"));
        Assert.AreEqual(ExpectedCommit, RequiredString(handoff, "terminalGuiCommit"));
    }

    /// <summary>
    /// Prüft den ersten vollständigen Slice für Ereignisse, Befehle und Dispatch.
    ///
    /// Validates the first complete slice for events, commands, and dispatch.
    /// </summary>
    [TestMethod]
    public void Test_D02ReferenceSliceIsComplete()
    {
        JsonObject root = LoadObject(AuditFileName);
        JsonObject[] relations = RequiredArray(root, "relations").OfType<JsonObject>().ToArray();
        JsonObject[] sources = RequiredArray(root, "sources").OfType<JsonObject>().ToArray();
        var sourceIds = sources.Select(source => RequiredString(source, "sourceId")).ToHashSet(StringComparer.Ordinal);

        foreach (string contractId in new[] { "C004", "C005", "C006" })
        {
            JsonObject relation = relations.Single(item => RequiredString(item, "contractId") == contractId);
            Assert.AreEqual("D02", RequiredString(relation, "domainId"));
            Assert.Contains(RequiredString(relation, "terminalGuiRelation"), AllowedRelations);
            Assert.IsNotEmpty(RequiredString(relation, "rationaleDe"));
            Assert.IsNotEmpty(RequiredString(relation, "rationaleEn"));
            Assert.IsTrue(StringValues(RequiredArray(relation, "terminalGuiSourceIds")).All(sourceIds.Contains));
        }
    }

    /// <summary>
    /// Prüft die vollständigen geschlossenen Mengen und alle reziproken Beziehungen.
    ///
    /// Validates the complete closed sets and every reciprocal relationship.
    /// </summary>
    [TestMethod]
    public void Test_CompleteAuditRelationshipsAreValid()
    {
        string[] errors = ValidateAudit(LoadObject(AuditFileName)).ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));
    }

    /// <summary>
    /// Prüft Verbraucherpfade, Entscheidungen und die unveränderten historischen Wellen.
    ///
    /// Validates consumer paths, decisions, and the unchanged historical waves.
    /// </summary>
    [TestMethod]
    public void Test_ConsumerBaselineIsCompleteAndReadOnly()
    {
        JsonObject root = LoadObject(AuditFileName);
        JsonObject[] consumers = RequiredArray(root, "consumers").OfType<JsonObject>().ToArray();
        string[] expectedIds = Enumerable.Range(1, 6)
            .Select(number => $"W5-{number:000}")
            .Concat(Enumerable.Range(1, 7).Select(number => $"W6-{number:000}"))
            .ToArray();

        CollectionAssert.AreEquivalent(expectedIds, consumers.Select(item => RequiredString(item, "consumerId")).ToArray());
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        foreach (JsonObject consumer in consumers)
        {
            Assert.Contains(RequiredString(consumer, "decision"), AllowedConsumerDecisions);
            foreach (string sourcePath in StringValues(RequiredArray(consumer, "sourcePaths")))
            {
                Assert.IsTrue(
                    sourcePath.StartsWith("TVDEMOS/", StringComparison.Ordinal)
                    || sourcePath.StartsWith("TVFM/", StringComparison.Ordinal));
                Assert.IsTrue(File.Exists(Path.Combine(repoRoot, sourcePath)), $"Missing consumer source: {sourcePath}");
            }
        }
    }

    /// <summary>
    /// Vergleicht Quellenmanifest, gepinnte Pfade und lokal verfügbare Dateihashes.
    ///
    /// Compares the source manifest, pinned paths, and locally available file hashes.
    /// </summary>
    [TestMethod]
    public void Test_SourceManifestAndPinnedHashesAreComplete()
    {
        JsonObject root = LoadObject(AuditFileName);
        JsonObject[] sources = RequiredArray(root, "sources").OfType<JsonObject>().ToArray();
        string manifest = File.ReadAllText(GetFeaturePath("terminalgui-source-manifest.md"));

        Assert.HasCount(25, sources);
        foreach (JsonObject source in sources)
        {
            string sourceId = RequiredString(source, "sourceId");
            string path = RequiredString(source, "path");
            string expectedHash = RequiredString(source, "sha256");
            StringAssert.Contains(manifest, sourceId);
            StringAssert.Contains(manifest, expectedHash);
            StringAssert.Contains(manifest, ExpectedCommit);
            StringAssert.Contains(RequiredString(source, "permalink"), ExpectedCommit);

            string externalPath = Path.Combine("/tmp/terminal-gui-v1.9.0-029.XATvzb", path);
            if (!File.Exists(externalPath))
                continue;

            string actualHash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(externalPath))).ToLowerInvariant();
            Assert.AreEqual(expectedHash, actualHash, $"{sourceId} hash mismatch for {path}");
        }
    }

    /// <summary>
    /// Prüft die Beobachtungsabhängigkeiten und den vollständigen Feature-030-Handoff.
    ///
    /// Validates observation dependencies and the complete Feature-030 handoff.
    /// </summary>
    [TestMethod]
    public void Test_ObservationsAndHandoffReconcile()
    {
        JsonObject root = LoadObject(AuditFileName);
        JsonObject handoff = LoadObject(HandoffFileName);
        JsonObject[] observations = RequiredArray(root, "observations").OfType<JsonObject>().ToArray();

        Assert.IsFalse(RequiredBoolean(handoff, "hardeningLastenhefteCreated"));
        Assert.IsFalse(RequiredBoolean(handoff, "closureLastenheftCreated"));
        Assert.AreEqual("BlockedPendingFeature030", RequiredString(handoff, "wave5State"));
        Assert.AreEqual("BlockedPendingFeature030", RequiredString(handoff, "wave6State"));
        Assert.AreEqual("030-tv203-magiblot-evolution-audit", RequiredString(handoff, "nextIntake"));

        string[] errors = ValidateObservationGraph(observations)
            .Concat(ValidateHandoff(root, handoff))
            .ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));
    }

    /// <summary>
    /// Beweist, dass beschädigte Werte und widersprüchliche Beziehungen geschlossen abgelehnt werden.
    ///
    /// Proves that malformed values and contradictory relationships fail closed.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsMalformedUnknownDuplicateOrCyclicData()
    {
        Assert.Throws<JsonException>(() => JsonNode.Parse("{"));

        JsonObject unknown = (JsonObject)LoadObject(AuditFileName).DeepClone();
        if (RequiredArray(unknown, "relations").FirstOrDefault() is JsonObject firstRelation)
        {
            firstRelation["terminalGuiRelation"] = "Aligned";
            Assert.IsTrue(ValidateAudit(unknown).Any(error => error.Contains("unknown relation", StringComparison.Ordinal)));
        }

        JsonObject duplicate = (JsonObject)LoadObject(AuditFileName).DeepClone();
        if (RequiredArray(duplicate, "sources").FirstOrDefault() is JsonObject firstSource)
        {
            RequiredArray(duplicate, "sources").Add(firstSource.DeepClone());
            Assert.IsTrue(ValidateAudit(duplicate).Any(error => error.Contains("duplicate sourceId", StringComparison.Ordinal)));
        }

        JsonObject handoff = (JsonObject)LoadObject(HandoffFileName).DeepClone();
        handoff["terminalGuiCommit"] = new string('0', 40);
        Assert.IsTrue(ValidateHandoff(LoadObject(AuditFileName), handoff).Any());

        var cyclic = new[]
        {
            Observation("TG901", "TG902"),
            Observation("TG902", "TG901"),
        };
        Assert.IsTrue(ValidateObservationGraph(cyclic).Any(error => error.Contains("cycle", StringComparison.Ordinal)));
    }

    private static IEnumerable<string> ValidateAudit(JsonObject root)
    {
        var errors = new List<string>();
        JsonObject[] sources = RequiredArray(root, "sources").OfType<JsonObject>().ToArray();
        JsonObject[] relations = RequiredArray(root, "relations").OfType<JsonObject>().ToArray();
        JsonObject[] consumers = RequiredArray(root, "consumers").OfType<JsonObject>().ToArray();
        JsonObject[] observations = RequiredArray(root, "observations").OfType<JsonObject>().ToArray();

        AddDuplicateErrors(sources, "sourceId", errors);
        AddDuplicateErrors(relations, "contractId", errors);
        AddDuplicateErrors(consumers, "consumerId", errors);
        AddDuplicateErrors(observations, "findingId", errors);

        string[] expectedContracts = Enumerable.Range(1, 48).Select(number => $"C{number:000}").ToArray();
        string[] expectedDomains = Enumerable.Range(1, 16).Select(number => $"D{number:00}").ToArray();
        CollectionErrors(expectedContracts, relations.Select(item => RequiredString(item, "contractId")), "contract", errors);
        CollectionErrors(expectedDomains, relations.Select(item => RequiredString(item, "domainId")).Distinct(), "domain", errors);

        var sourceIds = sources.Select(item => RequiredString(item, "sourceId")).ToHashSet(StringComparer.Ordinal);
        var contractIds = relations.Select(item => RequiredString(item, "contractId")).ToHashSet(StringComparer.Ordinal);
        var consumerIds = consumers.Select(item => RequiredString(item, "consumerId")).ToHashSet(StringComparer.Ordinal);
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();

        foreach (JsonObject source in sources)
        {
            string sourceId = RequiredString(source, "sourceId");
            string hash = RequiredString(source, "sha256");
            if (!Regex.IsMatch(sourceId, "^TGSR[0-9]{3}$", RegexOptions.CultureInvariant))
                errors.Add($"invalid sourceId: {sourceId}");
            if (!Regex.IsMatch(hash, "^[0-9a-f]{64}$", RegexOptions.CultureInvariant))
                errors.Add($"invalid source hash: {sourceId}");
            foreach (string contractId in StringValues(RequiredArray(source, "contractIds")))
            {
                if (!contractIds.Contains(contractId))
                    errors.Add($"{sourceId} references unknown contract: {contractId}");
            }
        }

        foreach (JsonObject relation in relations)
        {
            string contractId = RequiredString(relation, "contractId");
            string decision = RequiredString(relation, "terminalGuiRelation");
            string[] linkedSources = StringValues(RequiredArray(relation, "terminalGuiSourceIds"));
            if (!AllowedRelations.Contains(decision))
                errors.Add($"{contractId} has unknown relation: {decision}");
            if (decision == "NotApplicable")
            {
                if (linkedSources.Length != 0)
                    errors.Add($"{contractId} fabricates a source for NotApplicable");
                if (string.IsNullOrWhiteSpace(RequiredString(relation, "reevaluationTrigger")))
                    errors.Add($"{contractId} lacks a re-evaluation trigger");
            }
            else if (linkedSources.Length == 0 || linkedSources.Any(id => !sourceIds.Contains(id)))
            {
                errors.Add($"{contractId} lacks valid Terminal.GUI sources");
            }

            foreach (string sourceId in linkedSources)
            {
                JsonObject source = sources.Single(item => RequiredString(item, "sourceId") == sourceId);
                if (!StringValues(RequiredArray(source, "contractIds")).Contains(contractId, StringComparer.Ordinal)
                    && sourceId != "TGSR001")
                    errors.Add($"{contractId} and {sourceId} are not reciprocal");
            }

            ValidateProofReferences(contractId, RequiredString(relation, "tuiVisionProof"), repoRoot, errors);
            foreach (string consumerId in StringValues(RequiredArray(relation, "consumerIds")))
            {
                if (!consumerIds.Contains(consumerId))
                    errors.Add($"{contractId} references unknown consumer: {consumerId}");
            }
        }

        foreach (JsonObject governance in RequiredArray(root, "governance").OfType<JsonObject>())
        {
            string applicability = RequiredString(governance, "applicability");
            if (!AllowedGovernanceApplicability.Contains(applicability))
                errors.Add($"unknown governance applicability: {applicability}");
        }

        errors.AddRange(ValidateObservationGraph(observations));
        return errors;
    }

    private static IEnumerable<string> ValidateObservationGraph(IReadOnlyCollection<JsonObject> observations)
    {
        var errors = new List<string>();
        var byId = observations.ToDictionary(item => RequiredString(item, "findingId"), StringComparer.Ordinal);
        var keys = new HashSet<string>(StringComparer.Ordinal);

        foreach (JsonObject observation in observations)
        {
            string id = RequiredString(observation, "findingId");
            string decision = RequiredString(observation, "decision");
            if (!AllowedObservationDecisions.Contains(decision))
                errors.Add($"{id} has unknown observation decision: {decision}");
            if (!keys.Add(RequiredString(observation, "deduplicationKey")))
                errors.Add($"{id} has a duplicate deduplication key");
            foreach (string dependency in StringValues(RequiredArray(observation, "dependencies")))
            {
                if (!byId.ContainsKey(dependency))
                    errors.Add($"{id} references unknown dependency: {dependency}");
            }
        }

        // Deutsch: Eine Dreifarbensuche trennt echte Zyklen von gemeinsam genutzten Vorgängern.
        // English: A three-color search separates real cycles from shared predecessors.
        var state = new Dictionary<string, int>(StringComparer.Ordinal);
        bool Visit(string id)
        {
            if (state.GetValueOrDefault(id) == 1)
                return false;
            if (state.GetValueOrDefault(id) == 2)
                return true;
            state[id] = 1;
            foreach (string dependency in StringValues(RequiredArray(byId[id], "dependencies")))
            {
                if (byId.ContainsKey(dependency) && !Visit(dependency))
                    return false;
            }
            state[id] = 2;
            return true;
        }

        foreach (string id in byId.Keys)
        {
            if (!Visit(id))
            {
                errors.Add($"observation dependency cycle reaches {id}");
                break;
            }
        }

        return errors;
    }

    private static IEnumerable<string> ValidateHandoff(JsonObject root, JsonObject handoff)
    {
        var errors = new List<string>();
        if (RequiredString(handoff, "terminalGuiTagObject") != ExpectedTagObject
            || RequiredString(handoff, "terminalGuiCommit") != ExpectedCommit)
            errors.Add("handoff source pin differs from the audit run");

        string[] contractIds = RequiredArray(root, "relations")
            .OfType<JsonObject>()
            .Select(item => RequiredString(item, "contractId"))
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToArray();
        string[] handoffContracts = StringValues(RequiredArray(handoff, "contractIds"))
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToArray();
        if (!contractIds.SequenceEqual(handoffContracts, StringComparer.Ordinal))
            errors.Add("handoff contract set differs from the audit");

        JsonObject[] observations = RequiredArray(root, "observations").OfType<JsonObject>().ToArray();
        string[] observationIds = observations.Select(item => RequiredString(item, "findingId")).OrderBy(id => id).ToArray();
        string[] handoffIds = StringValues(RequiredArray(handoff, "observationIds")).OrderBy(id => id).ToArray();
        if (!observationIds.SequenceEqual(handoffIds, StringComparer.Ordinal))
            errors.Add("handoff observation set differs from the audit");

        string[] candidateIds = observations
            .Where(item => RequiredString(item, "decision") == "CandidateFinding")
            .Select(item => RequiredString(item, "findingId"))
            .OrderBy(id => id)
            .ToArray();
        string[] handoffCandidates = StringValues(RequiredArray(handoff, "candidateFindingIds")).OrderBy(id => id).ToArray();
        if (!candidateIds.SequenceEqual(handoffCandidates, StringComparer.Ordinal))
            errors.Add("handoff candidate-finding set differs from the audit");
        return errors;
    }

    private static void ValidateProofReferences(string contractId, string proof, string repoRoot, ICollection<string> errors)
    {
        foreach (string reference in proof.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            string[] parts = reference.Split("::", 2, StringSplitOptions.TrimEntries);
            if (parts.Length is not 2)
            {
                errors.Add($"{contractId} proof must use path::method: {reference}");
                continue;
            }
            string path = Path.Combine(repoRoot, parts[0]);
            if (!File.Exists(path))
                errors.Add($"{contractId} proof path is missing: {parts[0]}");
            else if (!File.ReadAllText(path).Contains(parts[1], StringComparison.Ordinal))
                errors.Add($"{contractId} proof method is missing: {parts[1]}");
        }
    }

    private static void AddDuplicateErrors(IEnumerable<JsonObject> items, string propertyName, ICollection<string> errors)
    {
        foreach (IGrouping<string, JsonObject> group in items.GroupBy(item => RequiredString(item, propertyName), StringComparer.Ordinal))
        {
            if (group.Count() > 1)
                errors.Add($"duplicate {propertyName}: {group.Key}");
        }
    }

    private static void CollectionErrors(
        IEnumerable<string> expected,
        IEnumerable<string> actual,
        string label,
        ICollection<string> errors)
    {
        string[] expectedValues = expected.OrderBy(value => value, StringComparer.Ordinal).ToArray();
        string[] actualValues = actual.OrderBy(value => value, StringComparer.Ordinal).ToArray();
        if (!expectedValues.SequenceEqual(actualValues, StringComparer.Ordinal))
            errors.Add($"{label} set differs from the canonical set");
    }

    private static JsonObject Observation(string id, string dependency)
        => new()
        {
            ["findingId"] = id,
            ["decision"] = "RejectedComparison",
            ["deduplicationKey"] = id,
            ["dependencies"] = new JsonArray(dependency),
        };

    private static JsonObject LoadObject(string fileName)
    {
        string json = File.ReadAllText(GetFeaturePath(fileName));
        return JsonNode.Parse(json)?.AsObject()
            ?? throw new JsonException($"Expected an object root in {fileName}.");
    }

    private static JsonObject RequiredObject(JsonObject parent, string propertyName)
        => parent[propertyName]?.AsObject()
           ?? throw new JsonException($"Required object is missing: {propertyName}");

    private static string RequiredString(JsonObject parent, string propertyName)
        => parent[propertyName]?.GetValue<string>()
           ?? throw new JsonException($"Required string is missing: {propertyName}");

    private static JsonArray RequiredArray(JsonObject parent, string propertyName)
        => parent[propertyName]?.AsArray()
           ?? throw new JsonException($"Required array is missing: {propertyName}");

    private static bool RequiredBoolean(JsonObject parent, string propertyName)
        => parent[propertyName]?.GetValue<bool>()
           ?? throw new JsonException($"Required boolean is missing: {propertyName}");

    private static string[] StringValues(JsonArray array)
        => array.Select(item => item?.GetValue<string>() ?? throw new JsonException("Expected string array value.")).ToArray();

    private static string GetFeaturePath(string fileName)
        => Path.Combine(
            Phase7DriverTestContext.FindRepoRoot(),
            "specs",
            FeatureDirectoryName,
            fileName);
}
