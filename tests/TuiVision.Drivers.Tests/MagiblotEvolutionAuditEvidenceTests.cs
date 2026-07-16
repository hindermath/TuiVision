// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Prüft den strukturierten Nachweis des magiblot/tvision-Evolutionsaudits.
///
/// Validates the structured evidence for the magiblot/tvision evolution audit.
/// </summary>
[TestClass]
public sealed class MagiblotEvolutionAuditEvidenceTests
{
    private const string FeatureDirectoryName = "030-tv203-magiblot-evolution-audit";
    private const string AuditFileName = "magiblot-evolution-audit.json";
    private const string CombinedFileName = "combined-conformance-findings.json";
    private const string ExpectedRunId = "030-tv203-magiblot-evolution-audit";
    private const string ExpectedRepository = "https://github.com/magiblot/tvision.git";
    private const string ExpectedCommit = "57b6f56b38e0ee75240a80a10ee0e11470c24693";
    private const string ExpectedTree = "96dd03873955689ff0a79f6c8107a8148fe1ebd6";
    private const string ExpectedCopyrightSha256 =
        "66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548";

    private static readonly IReadOnlySet<string> AllowedRelations =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "CorroboratesOriginal",
            "CorroboratesModernization",
            "AlternativeModernization",
            "DivergesFromTuiVision",
            "NotApplicable",
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

    private static readonly IReadOnlySet<string> AllowedOwners =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "CoreRuntimeDriver",
            "ComponentDataInteraction",
            "CrossCuttingA11YProof",
        };

    private static readonly IReadOnlySet<string> AllowedGovernanceApplicability =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "Applicable",
            "N/A",
            "Open",
        };

    /// <summary>
    /// Stellt sicher, dass beide akzeptierten Feature-030-Datensätze vorhanden sind.
    ///
    /// Ensures that both accepted Feature-030 datasets exist.
    /// </summary>
    [TestMethod]
    public void Test_AcceptedDatasetsExist()
    {
        Assert.IsTrue(File.Exists(GetFeaturePath(AuditFileName)), $"Missing accepted dataset: {AuditFileName}");
        Assert.IsTrue(File.Exists(GetFeaturePath(CombinedFileName)), $"Missing accepted dataset: {CombinedFileName}");
    }

    /// <summary>
    /// Bindet den Auditlauf an die unveränderlichen magiblot-Quellobjekte.
    ///
    /// Binds the audit run to the immutable magiblot source objects.
    /// </summary>
    [TestMethod]
    public void Test_AuditRunUsesExactPins()
    {
        JsonObject run = RequiredObject(LoadObject(AuditFileName), "run");

        Assert.AreEqual("1.0", RequiredString(run, "schemaVersion"));
        Assert.AreEqual(ExpectedRunId, RequiredString(run, "runId"));
        Assert.AreEqual("029-tv203-freevision-terminalgui-conformance-audit", RequiredString(run, "handoffFeature"));
        Assert.AreEqual(ExpectedRepository, RequiredString(run, "magiblotRepository"));
        Assert.AreEqual(ExpectedCommit, RequiredString(run, "magiblotCommit"));
        Assert.AreEqual(ExpectedTree, RequiredString(run, "magiblotTree"));
        Assert.AreEqual(ExpectedCopyrightSha256, RequiredString(run, "copyrightSha256"));
        Assert.AreEqual("BlockedPendingCombinedConformanceClosure", RequiredString(run, "wave5State"));
        Assert.AreEqual("BlockedPendingCombinedConformanceClosure", RequiredString(run, "wave6State"));
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
        JsonObject[] relations = RequiredObjects(root, "relations");
        JsonObject[] observations = RequiredObjects(root, "observations");
        var sourceIds = RequiredObjects(root, "sources")
            .Select(source => RequiredString(source, "sourceId"))
            .ToHashSet(StringComparer.Ordinal);

        foreach (string contractId in new[] { "C004", "C005", "C006" })
        {
            JsonObject relation = relations.Single(item => RequiredString(item, "contractId") == contractId);
            JsonObject observation = observations.Single(item => RequiredString(item, "contractId") == contractId);

            Assert.AreEqual("D02", RequiredString(relation, "domainId"));
            Assert.Contains(RequiredString(relation, "magiblotRelation"), AllowedRelations);
            Assert.IsTrue(StringValues(RequiredArray(relation, "magiblotSourceIds")).All(sourceIds.Contains));
            Assert.IsNotEmpty(RequiredString(relation, "tuiVisionProof"));
            Assert.AreEqual(RequiredString(relation, "observationId"), RequiredString(observation, "observationId"));
        }
    }

    /// <summary>
    /// Prüft die vollständigen geschlossenen Mengen und reziproken Beziehungen.
    ///
    /// Validates the complete closed sets and reciprocal relationships.
    /// </summary>
    [TestMethod]
    public void Test_CompleteAuditRelationshipsAreValid()
    {
        string[] errors = ValidateAudit(LoadObject(AuditFileName)).ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));
    }

    /// <summary>
    /// Vergleicht Quellenmanifest, gepinnte Pfade und verfügbare Dateihashes.
    ///
    /// Compares the source manifest, pinned paths, and available file hashes.
    /// </summary>
    [TestMethod]
    public void Test_SourceManifestAndPinnedHashesAreComplete()
    {
        JsonObject[] sources = RequiredObjects(LoadObject(AuditFileName), "sources");
        string manifest = File.ReadAllText(GetFeaturePath("magiblot-source-manifest.md"));

        Assert.HasCount(50, sources);
        foreach (JsonObject source in sources)
        {
            string sourceId = RequiredString(source, "sourceId");
            string relativePath = RequiredString(source, "path");
            string expectedHash = RequiredString(source, "sha256");
            StringAssert.Contains(manifest, sourceId);
            StringAssert.Contains(manifest, expectedHash);
            StringAssert.Contains(RequiredString(source, "permalink"), ExpectedCommit);

            string externalPath = Path.Combine("/tmp/magiblot-tvision-030-57b6f56", relativePath);
            if (!File.Exists(externalPath))
                continue;

            string actualHash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(externalPath))).ToLowerInvariant();
            Assert.AreEqual(expectedHash, actualHash, $"{sourceId} hash mismatch for {relativePath}");
        }
    }

    /// <summary>
    /// Prüft die vollständige und read-only behandelte Consumer-Baseline.
    ///
    /// Validates the complete consumer baseline and its read-only boundary.
    /// </summary>
    [TestMethod]
    public void Test_ConsumerBaselineIsCompleteAndReadOnly()
    {
        JsonObject[] consumers = RequiredObjects(LoadObject(AuditFileName), "consumers");
        string[] expectedIds = Enumerable.Range(1, 6)
            .Select(number => $"W5-{number:000}")
            .Concat(Enumerable.Range(1, 7).Select(number => $"W6-{number:000}"))
            .ToArray();

        CollectionAssert.AreEquivalent(expectedIds, consumers.Select(item => RequiredString(item, "consumerId")).ToArray());
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        foreach (JsonObject consumer in consumers)
        {
            Assert.IsNotEmpty(RequiredString(consumer, "decision"));
            Assert.IsNotEmpty(RequiredString(consumer, "magiblotRelevance"));
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
    /// Prüft vollständige Deduplizierung und deterministische Folgeintakes.
    ///
    /// Validates complete deduplication and deterministic follow-up intakes.
    /// </summary>
    [TestMethod]
    public void Test_CombinedDispositionsAndFollowupsAreDeterministic()
    {
        JsonObject audit = LoadObject(AuditFileName);
        JsonObject combined = LoadObject(CombinedFileName);
        JsonObject[] observations = RequiredObjects(audit, "observations");
        JsonObject[] dispositions = RequiredObjects(combined, "dispositions");
        JsonObject[] findings = RequiredObjects(combined, "findings");

        Assert.HasCount(48, observations);
        Assert.HasCount(96, dispositions);
        AddDuplicateErrors(dispositions, "observationId", new List<string>());

        string[] expectedTg = Enumerable.Range(1, 48).Select(number => $"TGO{number:000}").ToArray();
        string[] expectedMb = Enumerable.Range(1, 48).Select(number => $"MB{number:000}").ToArray();
        CollectionAssert.AreEquivalent(
            expectedTg.Concat(expectedMb).ToArray(),
            dispositions.Select(item => RequiredString(item, "observationId")).ToArray());

        string[] errors = ValidateCombined(combined).ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));

        JsonObject closure = RequiredObject(combined, "closureIntake");
        if (findings.Length == 0)
        {
            Assert.AreEqual(31, RequiredInt32(closure, "featureNumber"));
            Assert.HasCount(0, RequiredArray(combined, "hardeningIntakes"));
        }
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();
        string intakePath = Path.Combine(repoRoot, RequiredString(closure, "fileName"));
        if (!File.Exists(intakePath))
        {
            // Der akzeptierte Handoff behält den ursprünglichen Namen; der Abschluss archiviert ihn mit dem Branch-Suffix.
            // The accepted handoff keeps the original name; closure archives it with the branch suffix.
            string archivedFileName = $"{Path.GetFileNameWithoutExtension(RequiredString(closure, "fileName"))}.{RequiredString(closure, "featureBranch")}.md";
            intakePath = Path.Combine(repoRoot, archivedFileName);
        }

        Assert.IsTrue(File.Exists(intakePath), $"Missing current or archived closure intake: {intakePath}");
    }

    /// <summary>
    /// Beweist die geschlossene Ablehnung beschädigter oder zyklischer Daten.
    ///
    /// Proves fail-closed rejection of malformed or cyclic data.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsMalformedUnknownDuplicateOrCyclicData()
    {
        Assert.Throws<JsonException>(() => JsonNode.Parse("{"));

        JsonObject unknown = (JsonObject)LoadObject(AuditFileName).DeepClone();
        RequiredObjects(unknown, "relations")[0]["magiblotRelation"] = "Aligned";
        Assert.IsTrue(ValidateAudit(unknown).Any(error => error.Contains("unknown relation", StringComparison.Ordinal)));

        JsonObject duplicate = (JsonObject)LoadObject(AuditFileName).DeepClone();
        RequiredArray(duplicate, "sources").Add(RequiredObjects(duplicate, "sources")[0].DeepClone());
        Assert.IsTrue(ValidateAudit(duplicate).Any(error => error.Contains("duplicate sourceId", StringComparison.Ordinal)));

        JsonObject cyclic = (JsonObject)LoadObject(CombinedFileName).DeepClone();
        RequiredArray(cyclic, "findings").Add(Finding("CF901", "CF902"));
        RequiredArray(cyclic, "findings").Add(Finding("CF902", "CF901"));
        Assert.IsTrue(ValidateCombined(cyclic).Any(error => error.Contains("cycle", StringComparison.Ordinal)));
    }

    private static IEnumerable<string> ValidateAudit(JsonObject root)
    {
        var errors = new List<string>();
        JsonObject[] sources = RequiredObjects(root, "sources");
        JsonObject[] relations = RequiredObjects(root, "relations");
        JsonObject[] consumers = RequiredObjects(root, "consumers");
        JsonObject[] observations = RequiredObjects(root, "observations");

        AddDuplicateErrors(sources, "sourceId", errors);
        AddDuplicateErrors(relations, "contractId", errors);
        AddDuplicateErrors(observations, "observationId", errors);

        CollectionErrors(
            Enumerable.Range(1, 48).Select(number => $"C{number:000}"),
            relations.Select(item => RequiredString(item, "contractId")),
            "contract",
            errors);
        CollectionErrors(
            Enumerable.Range(1, 16).Select(number => $"D{number:00}"),
            relations.Select(item => RequiredString(item, "domainId")).Distinct(),
            "domain",
            errors);

        var sourceIds = sources.Select(item => RequiredString(item, "sourceId")).ToHashSet(StringComparer.Ordinal);
        var contractIds = relations.Select(item => RequiredString(item, "contractId")).ToHashSet(StringComparer.Ordinal);
        var consumerIds = consumers.Select(item => RequiredString(item, "consumerId")).ToHashSet(StringComparer.Ordinal);
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();

        foreach (JsonObject source in sources)
        {
            string sourceId = RequiredString(source, "sourceId");
            if (!Regex.IsMatch(sourceId, "^MBSR[0-9]{3}$", RegexOptions.CultureInvariant))
                errors.Add($"invalid sourceId: {sourceId}");
            if (!Regex.IsMatch(RequiredString(source, "sha256"), "^[0-9a-f]{64}$", RegexOptions.CultureInvariant))
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
            string decision = RequiredString(relation, "magiblotRelation");
            string[] linkedSources = StringValues(RequiredArray(relation, "magiblotSourceIds"));
            if (!AllowedRelations.Contains(decision))
                errors.Add($"{contractId} has unknown relation: {decision}");
            if (decision == "NotApplicable")
            {
                if (linkedSources.Length != 0)
                    errors.Add($"{contractId} fabricates sources for NotApplicable");
                if (string.IsNullOrWhiteSpace(RequiredString(relation, "reevaluationTrigger")))
                    errors.Add($"{contractId} lacks a re-evaluation trigger");
            }
            else if (linkedSources.Length == 0 || linkedSources.Any(id => !sourceIds.Contains(id)))
            {
                errors.Add($"{contractId} lacks valid magiblot sources");
            }

            foreach (string sourceId in linkedSources)
            {
                JsonObject source = sources.First(item => RequiredString(item, "sourceId") == sourceId);
                if (!StringValues(RequiredArray(source, "contractIds")).Contains(contractId, StringComparer.Ordinal))
                    errors.Add($"{contractId} and {sourceId} are not reciprocal");
            }

            ValidateProofReferences(contractId, RequiredString(relation, "tuiVisionProof"), repoRoot, errors);
            foreach (string consumerId in StringValues(RequiredArray(relation, "consumerIds")))
            {
                if (!consumerIds.Contains(consumerId))
                    errors.Add($"{contractId} references unknown consumer: {consumerId}");
            }
        }

        foreach (JsonObject observation in observations)
        {
            string id = RequiredString(observation, "observationId");
            if (!Regex.IsMatch(id, "^MB[0-9]{3}$", RegexOptions.CultureInvariant))
                errors.Add($"invalid observationId: {id}");
            if (!AllowedObservationDecisions.Contains(RequiredString(observation, "decision")))
                errors.Add($"{id} has unknown observation decision");
        }

        foreach (JsonObject governance in RequiredObjects(root, "governance"))
        {
            if (!AllowedGovernanceApplicability.Contains(RequiredString(governance, "applicability")))
                errors.Add("unknown governance applicability");
        }
        return errors;
    }

    private static IEnumerable<string> ValidateCombined(JsonObject root)
    {
        var errors = new List<string>();
        JsonObject[] dispositions = RequiredObjects(root, "dispositions");
        JsonObject[] findings = RequiredObjects(root, "findings");
        AddDuplicateErrors(dispositions, "observationId", errors);
        AddDuplicateErrors(findings, "findingId", errors);

        var findingIds = findings.Select(item => RequiredString(item, "findingId")).ToHashSet(StringComparer.Ordinal);
        foreach (JsonObject disposition in dispositions)
        {
            string outcome = RequiredString(disposition, "disposition");
            string findingId = RequiredString(disposition, "canonicalFindingId");
            if (outcome == "CanonicalFinding" && !findingIds.Contains(findingId))
                errors.Add($"{RequiredString(disposition, "observationId")} references unknown finding");
            if (outcome == "NonFinding" && findingId != "None")
                errors.Add($"{RequiredString(disposition, "observationId")} has contradictory non-finding state");
        }

        var byId = findings.ToDictionary(item => RequiredString(item, "findingId"), StringComparer.Ordinal);
        foreach (JsonObject finding in findings)
        {
            string id = RequiredString(finding, "findingId");
            if (!AllowedOwners.Contains(RequiredString(finding, "primaryOwner")))
                errors.Add($"{id} has unknown Primary Owner");
            foreach (string dependency in StringValues(RequiredArray(finding, "dependencies")))
            {
                if (!byId.ContainsKey(dependency))
                    errors.Add($"{id} references unknown dependency");
            }
        }

        // Deutsch: Die Dreifarbensuche weist echte Owner-/Finding-Zyklen nach.
        // English: The three-color search detects real owner/finding cycles.
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
                if (!Visit(dependency))
                    return false;
            }
            state[id] = 2;
            return true;
        }

        foreach (string id in byId.Keys)
        {
            if (!Visit(id))
            {
                errors.Add($"finding dependency cycle reaches {id}");
                break;
            }
        }
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
        foreach (IGrouping<string, JsonObject> group in items.GroupBy(
                     item => RequiredString(item, propertyName),
                     StringComparer.Ordinal))
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

    private static JsonObject Finding(string id, string dependency)
        => new()
        {
            ["findingId"] = id,
            ["primaryOwner"] = "CoreRuntimeDriver",
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

    private static JsonObject[] RequiredObjects(JsonObject parent, string propertyName)
        => RequiredArray(parent, propertyName).OfType<JsonObject>().ToArray();

    private static string RequiredString(JsonObject parent, string propertyName)
        => parent[propertyName]?.GetValue<string>()
           ?? throw new JsonException($"Required string is missing: {propertyName}");

    private static int RequiredInt32(JsonObject parent, string propertyName)
        => parent[propertyName]?.GetValue<int>()
           ?? throw new JsonException($"Required integer is missing: {propertyName}");

    private static JsonArray RequiredArray(JsonObject parent, string propertyName)
        => parent[propertyName]?.AsArray()
           ?? throw new JsonException($"Required array is missing: {propertyName}");

    private static string[] StringValues(JsonArray array)
        => array.Select(item => item?.GetValue<string>() ?? throw new JsonException("Expected string array value.")).ToArray();

    private static string GetFeaturePath(string fileName)
        => Path.Combine(
            Phase7DriverTestContext.FindRepoRoot(),
            "specs",
            FeatureDirectoryName,
            fileName);
}
