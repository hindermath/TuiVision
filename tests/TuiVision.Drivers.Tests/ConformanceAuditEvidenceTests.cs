// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Prüft den strukturierten Nachweis des TV203-/Free-Vision-Konformitätsaudits.
///
/// Validates the structured evidence for the TV203/Free Vision conformance audit.
/// </summary>
[TestClass]
public sealed class ConformanceAuditEvidenceTests
{
    private const string FeatureDirectoryName = "024-tv203-freevision-conformance-audit";
    private const string FreeVisionRepository = "https://gitlab.com/freepascal.org/fpc/source.git";
    private const string FreeVisionCommit = "ffc03b34d8cafb85ddcf0686de1c5551601dacb2";

    private static readonly IReadOnlySet<string> AllowedPrimaryDecisions =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "Aligned",
            "IntentionalModernization",
            "BehavioralDrift",
            "EvidenceGap",
            "ConsciouslyOmitted",
        };

    private static readonly IReadOnlySet<string> AllowedFreeVisionRelations =
        new HashSet<string>(StringComparer.Ordinal)
        {
            "CorroboratesOriginal",
            "CorroboratesModernization",
            "DivergesFromOriginal",
            "NotApplicable",
        };

    /// <summary>
    /// Stellt sicher, dass der kanonische Auditdatensatz vor jeder weiteren Prüfung vorhanden ist.
    ///
    /// Ensures that the canonical audit dataset exists before any further validation.
    /// </summary>
    [TestMethod]
    public void Test_CanonicalDatasetExists()
    {
        string path = GetAuditPath();

        Assert.IsTrue(
            File.Exists(path),
            $"Canonical conformance audit dataset is missing: {path}");
    }

    /// <summary>
    /// Prüft den ersten vollständigen vertikalen Slice für Ereignisse, Befehle und Dispatch.
    ///
    /// Validates the first complete vertical slice for events, commands, and dispatch.
    /// </summary>
    [TestMethod]
    public void Test_D02ReferenceSliceIsComplete()
    {
        JsonObject root = LoadAuditObject();
        JsonArray domains = RequiredArray(root, "domains");
        Assert.HasCount(16, domains);

        JsonObject domain = domains
            .OfType<JsonObject>()
            .Single(item => RequiredString(item, "domainId") == "D02");

        Assert.IsNotEmpty(RequiredArray(domain, "historicalItemIds"));
        Assert.IsNotEmpty(RequiredArray(domain, "modernSourceItemIds"));
        Assert.IsNotEmpty(RequiredArray(domain, "publicContractItemIds"));
        Assert.IsNotEmpty(RequiredArray(domain, "contractIds"));
        Assert.AreEqual("Concrete", RequiredString(domain, "freeVisionCoverage"));

        string[] contractIds = StringValues(RequiredArray(domain, "contractIds"));
        JsonObject[] contracts = RequiredArray(root, "contracts")
            .OfType<JsonObject>()
            .Where(contract => contractIds.Contains(RequiredString(contract, "contractId"), StringComparer.Ordinal))
            .ToArray();

        Assert.HasCount(contractIds.Length, contracts);
        foreach (JsonObject contract in contracts)
        {
            Assert.AreEqual("D02", RequiredString(contract, "domainId"));
            Assert.Contains(RequiredString(contract, "primaryDecision"), AllowedPrimaryDecisions);
            Assert.Contains(RequiredString(contract, "freeVisionRelation"), AllowedFreeVisionRelations);
            Assert.IsNotEmpty(RequiredString(contract, "historicalIntent"));
            Assert.IsNotEmpty(RequiredString(contract, "observedBehavior"));
            Assert.IsNotEmpty(RequiredString(contract, "proof"));
            Assert.IsNotEmpty(RequiredArray(contract, "freeVisionSourceIds"));
        }

        string matrix = ReadFeatureFile("framework-conformance-matrix.md");
        foreach (string contractId in contractIds)
            StringAssert.Contains(matrix, contractId);
    }

    /// <summary>
    /// Beweist, dass unbekannte Entscheidungen, doppelte IDs und defektes JSON abgelehnt werden.
    ///
    /// Proves that unknown decisions, duplicate IDs, and malformed JSON are rejected.
    /// </summary>
    [TestMethod]
    public void Test_ValidatorRejectsMalformedUnknownAndDuplicateData()
    {
        Assert.Throws<JsonException>(() => JsonNode.Parse("{"));

        JsonObject unknown = (JsonObject)LoadAuditObject().DeepClone();
        JsonObject firstContract = RequiredArray(unknown, "contracts")[0]!.AsObject();
        firstContract["primaryDecision"] = "Applicable";
        Assert.IsTrue(
            ValidateClosedRelationships(unknown).Any(error => error.Contains("unknown primary decision", StringComparison.Ordinal)));

        JsonObject duplicate = (JsonObject)LoadAuditObject().DeepClone();
        JsonArray historical = RequiredArray(duplicate, "historicalItems");
        historical.Add(historical[0]!.DeepClone());
        Assert.IsTrue(
            ValidateClosedRelationships(duplicate).Any(error => error.Contains("duplicate historical itemId", StringComparison.Ordinal)));
    }

    /// <summary>
    /// Vergleicht die 151 historischen Dateien mit Ledger und Datensatz.
    ///
    /// Compares the 151 historical files with the ledger and dataset.
    /// </summary>
    [TestMethod]
    public void Test_HistoricalInventoryMatchesFilesystemAndLedger()
    {
        JsonObject root = LoadAuditObject();
        string[] actualFiles = Phase7DriverTestContext.GetHistoricalCcFiles().ToArray();
        string[] ledgerFiles = Phase7DriverTestContext.ParseLedgerRows()
            .Select(row => row.SourceFile)
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();
        string[] auditFiles = RequiredArray(root, "historicalItems")
            .OfType<JsonObject>()
            .Select(item => RequiredString(item, "ledgerPath"))
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();

        Assert.HasCount(151, actualFiles);
        CollectionAssert.AreEqual(actualFiles, ledgerFiles);
        CollectionAssert.AreEqual(actualFiles, auditFiles);
    }

    /// <summary>
    /// Vergleicht alle gepflegten Produktionsdateien mit dem Datensatz.
    ///
    /// Compares all maintained production files with the dataset.
    /// </summary>
    [TestMethod]
    public void Test_ModernSourceInventoryMatchesFilesystem()
    {
        JsonObject root = LoadAuditObject();
        string[] actualFiles = GetModernSourceFiles();
        string[] auditFiles = RequiredArray(root, "modernSourceItems")
            .OfType<JsonObject>()
            .Select(item => RequiredString(item, "path"))
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();

        CollectionAssert.AreEqual(actualFiles, auditFiles);
    }

    /// <summary>
    /// Vergleicht alle exportierten Typen der fünf Framework-Assemblies mit dem Datensatz.
    ///
    /// Compares all exported types from the five framework assemblies with the dataset.
    /// </summary>
    [TestMethod]
    public void Test_PublicTypeInventoryMatchesReflection()
    {
        JsonObject root = LoadAuditObject();
        string[] actualTypes = GetFrameworkAssemblies()
            .SelectMany(assembly => assembly.GetExportedTypes())
            .Select(type => type.FullName ?? type.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();
        string[] auditTypes = RequiredArray(root, "publicContractItems")
            .OfType<JsonObject>()
            .Select(item => RequiredString(item, "fullTypeName"))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        CollectionAssert.AreEqual(actualTypes, auditTypes);
    }

    /// <summary>
    /// Prüft Primärentscheidungen, historische Quellpfade und konkrete Testmethoden.
    ///
    /// Validates primary decisions, historical source paths, and concrete test methods.
    /// </summary>
    [TestMethod]
    public void Test_PrimaryDecisionsAndProofReferencesAreComplete()
    {
        JsonObject root = LoadAuditObject();
        string repoRoot = Phase7DriverTestContext.FindRepoRoot();

        foreach (JsonObject contract in RequiredArray(root, "contracts").OfType<JsonObject>())
        {
            string contractId = RequiredString(contract, "contractId");
            string decision = RequiredString(contract, "primaryDecision");
            Assert.Contains(decision, AllowedPrimaryDecisions, $"{contractId} has an unknown primary decision.");
            Assert.IsNotEmpty(RequiredString(contract, "historicalIntent"));
            Assert.IsNotEmpty(RequiredString(contract, "observedBehavior"));
            Assert.IsNotEmpty(RequiredString(contract, "risk"));

            string rationale = RequiredString(contract, "modernCSharpRationale");
            if (decision is "IntentionalModernization" or "ConsciouslyOmitted")
                Assert.AreNotEqual("None", rationale, $"{contractId} requires a modernization or omission rationale.");

            string[] borlandSources = StringValues(RequiredArray(contract, "borlandSources"));
            Assert.IsNotEmpty(borlandSources, $"{contractId} needs a concrete Borland/tv203 source.");
            foreach (string source in borlandSources)
                Assert.IsTrue(File.Exists(Path.Combine(repoRoot, source)), $"{contractId} references a missing historical source: {source}");

            string[] proofReferences = RequiredString(contract, "proof")
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Assert.IsNotEmpty(proofReferences, $"{contractId} needs a concrete proof reference.");
            foreach (string proofReference in proofReferences)
            {
                string[] parts = proofReference.Split("::", 2, StringSplitOptions.TrimEntries);
                Assert.HasCount(2, parts, $"{contractId} proof must use path::method: {proofReference}");
                string testPath = Path.Combine(repoRoot, parts[0]);
                Assert.IsTrue(File.Exists(testPath), $"{contractId} references a missing proof file: {parts[0]}");
                StringAssert.Contains(File.ReadAllText(testPath), parts[1], $"{contractId} references a missing proof method: {parts[1]}");
            }
        }
    }

    /// <summary>
    /// Prüft alle Free-Vision-Relationen und lokal verfügbare gepinnte Dateihashes.
    ///
    /// Validates all Free Vision relations and locally available pinned file hashes.
    /// </summary>
    [TestMethod]
    public void Test_FreeVisionRelationsAndPinnedHashesAreComplete()
    {
        JsonObject root = LoadAuditObject();
        JsonObject[] sources = RequiredArray(root, "externalSources").OfType<JsonObject>().ToArray();
        var sourceIds = sources.Select(source => RequiredString(source, "sourceId")).ToHashSet(StringComparer.Ordinal);

        foreach (JsonObject domain in RequiredArray(root, "domains").OfType<JsonObject>())
            Assert.Contains(RequiredString(domain, "freeVisionCoverage"), new[] { "Concrete", "NotApplicable" });

        foreach (JsonObject contract in RequiredArray(root, "contracts").OfType<JsonObject>())
        {
            string contractId = RequiredString(contract, "contractId");
            string relation = RequiredString(contract, "freeVisionRelation");
            string[] links = StringValues(RequiredArray(contract, "freeVisionSourceIds"));
            Assert.Contains(relation, AllowedFreeVisionRelations, $"{contractId} has an unknown Free Vision relation.");
            if (relation == "NotApplicable")
                Assert.HasCount(0, links, $"{contractId} must not fabricate an external source for NotApplicable.");
            else
                Assert.IsTrue(links.Length > 0 && links.All(sourceIds.Contains), $"{contractId} lacks a valid external source link.");
        }

        string externalRoot = "/tmp/tuivision-fv-024-ffc03b34";
        if (!Directory.Exists(externalRoot))
            return;

        foreach (JsonObject source in sources)
        {
            string path = Path.Combine(externalRoot, RequiredString(source, "path"));
            Assert.IsTrue(File.Exists(path), $"Pinned Free Vision source is missing locally: {path}");
            string actualHash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))).ToLowerInvariant();
            Assert.AreEqual(RequiredString(source, "sha256"), actualHash);
        }
    }

    /// <summary>
    /// Prüft die exakte Finding-Menge und die findings-gesteuerte Pre-Wave-5-Grenze.
    ///
    /// Validates the exact finding set and the findings-driven pre-Wave-5 boundary.
    /// </summary>
    [TestMethod]
    public void Test_FindingsAndPreWave5GateAreConsistent()
    {
        JsonObject root = LoadAuditObject();
        string[] expectedContractIds = RequiredArray(root, "contracts")
            .OfType<JsonObject>()
            .Where(contract => RequiredString(contract, "primaryDecision") is "BehavioralDrift" or "EvidenceGap")
            .Select(contract => RequiredString(contract, "contractId"))
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToArray();
        string[] actualContractIds = RequiredArray(root, "findings")
            .OfType<JsonObject>()
            .Select(finding => RequiredString(finding, "contractId"))
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToArray();

        CollectionAssert.AreEqual(expectedContractIds, actualContractIds);

        string findings = ReadFeatureFile("findings.md");
        StringAssert.Contains(findings, $"Total findings | {actualContractIds.Length}");
        string gate = ReadFeatureFile("pre-wave5-gate.md");
        StringAssert.Contains(gate, "`Core025` | 0 | Suppressed");
        StringAssert.Contains(gate, "`ComponentData026` | 0 | Suppressed");
        StringAssert.Contains(gate, "`Closure027` | Required");
        StringAssert.Contains(gate, "Wave 5 | Blocked");
    }

    /// <summary>
    /// Prüft alle geschlossenen Vokabulare, Beziehungen und Findings.
    ///
    /// Validates every closed vocabulary, relationship, and finding.
    /// </summary>
    [TestMethod]
    public void Test_DatasetRelationshipsAndFindingRulesAreComplete()
    {
        string[] errors = ValidateClosedRelationships(LoadAuditObject()).ToArray();
        Assert.HasCount(0, errors, string.Join(Environment.NewLine, errors));
    }

    /// <summary>
    /// Prüft die lesbaren Tabellen und die Grenze zu externem Quelltext.
    ///
    /// Validates readable tables and the external-source boundary.
    /// </summary>
    [TestMethod]
    public void Test_MarkdownReconcilesIdsWithoutExternalSourceFiles()
    {
        JsonObject root = LoadAuditObject();
        string matrix = ReadFeatureFile("framework-conformance-matrix.md");
        string manifest = ReadFeatureFile("freevision-source-manifest.md");
        string findings = ReadFeatureFile("findings.md");

        foreach (JsonObject contract in RequiredArray(root, "contracts").OfType<JsonObject>())
            StringAssert.Contains(matrix, RequiredString(contract, "contractId"));
        foreach (JsonObject source in RequiredArray(root, "externalSources").OfType<JsonObject>())
            StringAssert.Contains(manifest, RequiredString(source, "sourceId"));
        foreach (JsonObject finding in RequiredArray(root, "findings").OfType<JsonObject>())
            StringAssert.Contains(findings, RequiredString(finding, "findingId"));

        string featureDirectory = GetFeatureDirectory();
        string[] externalFiles = Directory.GetFiles(featureDirectory, "*", SearchOption.AllDirectories)
            .Where(path => path.EndsWith(".pas", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".pp", StringComparison.OrdinalIgnoreCase)
                || path.EndsWith(".inc", StringComparison.OrdinalIgnoreCase))
            .ToArray();
        Assert.HasCount(0, externalFiles);

        foreach (JsonObject source in RequiredArray(root, "externalSources").OfType<JsonObject>())
        {
            string scope = RequiredString(source, "behavioralScope");
            Assert.IsLessThanOrEqualTo(500, scope.Length);
            Assert.IsFalse(scope.Contains('\n', StringComparison.Ordinal));
        }
    }

    private static IEnumerable<string> ValidateClosedRelationships(JsonObject root)
    {
        JsonObject[] domains = RequiredArray(root, "domains").OfType<JsonObject>().ToArray();
        JsonObject[] historical = RequiredArray(root, "historicalItems").OfType<JsonObject>().ToArray();
        JsonObject[] modern = RequiredArray(root, "modernSourceItems").OfType<JsonObject>().ToArray();
        JsonObject[] publicTypes = RequiredArray(root, "publicContractItems").OfType<JsonObject>().ToArray();
        JsonObject[] contracts = RequiredArray(root, "contracts").OfType<JsonObject>().ToArray();
        JsonObject[] sources = RequiredArray(root, "externalSources").OfType<JsonObject>().ToArray();
        JsonObject[] findings = RequiredArray(root, "findings").OfType<JsonObject>().ToArray();

        string[] expectedDomains = Enumerable.Range(1, 16).Select(index => $"D{index:00}").ToArray();
        string[] domainIds = domains.Select(item => RequiredString(item, "domainId")).OrderBy(id => id, StringComparer.Ordinal).ToArray();
        if (!expectedDomains.SequenceEqual(domainIds, StringComparer.Ordinal))
            yield return "domain IDs must be exactly D01 through D16";

        foreach (string error in DuplicateErrors(historical, "itemId", "historical itemId"))
            yield return error;
        foreach (string error in DuplicateErrors(historical, "ledgerPath", "historical ledgerPath"))
            yield return error;
        foreach (string error in DuplicateErrors(modern, "itemId", "modern itemId"))
            yield return error;
        foreach (string error in DuplicateErrors(modern, "path", "modern path"))
            yield return error;
        foreach (string error in DuplicateErrors(publicTypes, "itemId", "public itemId"))
            yield return error;
        foreach (string error in DuplicateErrors(publicTypes, "fullTypeName", "public fullTypeName"))
            yield return error;
        foreach (string error in DuplicateErrors(contracts, "contractId", "contractId"))
            yield return error;
        foreach (string error in DuplicateErrors(sources, "sourceId", "sourceId"))
            yield return error;
        foreach (string error in DuplicateErrors(findings, "findingId", "findingId"))
            yield return error;

        var validDomainIds = domainIds.ToHashSet(StringComparer.Ordinal);
        var validContractIds = contracts.Select(item => RequiredString(item, "contractId")).ToHashSet(StringComparer.Ordinal);
        var validSourceIds = sources.Select(item => RequiredString(item, "sourceId")).ToHashSet(StringComparer.Ordinal);

        foreach (JsonObject item in historical.Concat(modern).Concat(publicTypes))
        {
            string itemId = RequiredString(item, "itemId");
            if (!validDomainIds.Contains(RequiredString(item, "domainId")))
                yield return $"{itemId} has an unknown domain";
            string[] links = StringValues(RequiredArray(item, "contractIds"));
            if (links.Length == 0 || links.Any(link => !validContractIds.Contains(link)))
                yield return $"{itemId} has missing or unknown contract links";
        }

        foreach (JsonObject contract in contracts)
        {
            string contractId = RequiredString(contract, "contractId");
            string decision = RequiredString(contract, "primaryDecision");
            string relation = RequiredString(contract, "freeVisionRelation");
            if (!AllowedPrimaryDecisions.Contains(decision))
                yield return $"{contractId} has unknown primary decision {decision}";
            if (!AllowedFreeVisionRelations.Contains(relation))
                yield return $"{contractId} has unknown Free Vision relation {relation}";
            if (!validDomainIds.Contains(RequiredString(contract, "domainId")))
                yield return $"{contractId} has an unknown domain";
            string[] sourceIds = StringValues(RequiredArray(contract, "freeVisionSourceIds"));
            if (relation != "NotApplicable" && (sourceIds.Length == 0 || sourceIds.Any(id => !validSourceIds.Contains(id))))
                yield return $"{contractId} lacks a valid Free Vision source";

            string findingId = RequiredString(contract, "findingId");
            bool needsFinding = decision is "BehavioralDrift" or "EvidenceGap";
            if (needsFinding != !string.Equals(findingId, "None", StringComparison.Ordinal))
                yield return $"{contractId} violates finding cardinality";
        }

        foreach (JsonObject source in sources)
        {
            string sourceId = RequiredString(source, "sourceId");
            if (RequiredString(source, "repository") != FreeVisionRepository
                || RequiredString(source, "commit") != FreeVisionCommit)
                yield return $"{sourceId} has invalid external provenance";
            if (!Regex.IsMatch(RequiredString(source, "sha256"), "^[0-9a-f]{64}$", RegexOptions.CultureInvariant))
                yield return $"{sourceId} has invalid SHA-256";
        }

        foreach (JsonObject finding in findings)
        {
            string findingId = RequiredString(finding, "findingId");
            string contractId = RequiredString(finding, "contractId");
            JsonObject? contract = contracts.SingleOrDefault(item => RequiredString(item, "contractId") == contractId);
            if (contract is null || RequiredString(contract, "findingId") != findingId)
                yield return $"{findingId} has no reciprocal contract link";
        }

        foreach (JsonObject domain in domains)
        {
            string domainId = RequiredString(domain, "domainId");
            if (RequiredArray(domain, "contractIds").Count == 0)
                yield return $"{domainId} has no contracts";
            if (RequiredString(domain, "freeVisionCoverage") is not ("Concrete" or "NotApplicable"))
                yield return $"{domainId} has invalid Free Vision coverage";
        }
    }

    private static IEnumerable<string> DuplicateErrors(IEnumerable<JsonObject> items, string propertyName, string label)
        => items
            .Select(item => RequiredString(item, propertyName))
            .GroupBy(value => value, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(group => $"duplicate {label}: {group.Key}");

    private static string[] GetModernSourceFiles()
    {
        string root = Phase7DriverTestContext.FindRepoRoot();
        string[] modulePaths =
        [
            "src/TuiVision.Compatibility",
            "src/TuiVision.Controls",
            "src/TuiVision.Core",
            "src/TuiVision.Drivers.Console",
            "src/TuiVision.Serialization",
        ];

        return modulePaths
            .SelectMany(module => Directory.GetFiles(Path.Combine(root, module), "*.cs", SearchOption.AllDirectories))
            .Where(path => !path.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.Ordinal)
                && !path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.Ordinal))
            .Select(path => Path.GetRelativePath(root, path).Replace('\\', '/'))
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToArray();
    }

    private static Assembly[] GetFrameworkAssemblies()
    {
        return
        [
            typeof(TuiVision.Compatibility.TConsoleInputAdapter).Assembly,
            typeof(TuiVision.Controls.TView).Assembly,
            typeof(TuiVision.Core.TObject).Assembly,
            typeof(TuiVision.Drivers.Console.TConsoleDriver).Assembly,
            typeof(TuiVision.Serialization.pstream).Assembly,
        ];
    }

    private static string GetFeatureDirectory()
        => Path.Combine(Phase7DriverTestContext.FindRepoRoot(), "specs", FeatureDirectoryName);

    private static string GetAuditPath()
        => Path.Combine(GetFeatureDirectory(), "conformance-audit.json");

    private static JsonObject LoadAuditObject()
    {
        string path = GetAuditPath();
        Assert.IsTrue(File.Exists(path), $"Canonical conformance audit dataset is missing: {path}");
        return JsonNode.Parse(File.ReadAllText(path))?.AsObject()
            ?? throw new JsonException("The canonical audit root must be a JSON object.");
    }

    private static string ReadFeatureFile(string fileName)
    {
        string path = Path.Combine(GetFeatureDirectory(), fileName);
        Assert.IsTrue(File.Exists(path), $"Required audit evidence is missing: {path}");
        return File.ReadAllText(path);
    }

    private static JsonArray RequiredArray(JsonObject owner, string propertyName)
        => owner[propertyName]?.AsArray()
            ?? throw new JsonException($"Required array is missing: {propertyName}");

    private static string RequiredString(JsonObject owner, string propertyName)
        => owner[propertyName]?.GetValue<string>()
            ?? throw new JsonException($"Required string is missing: {propertyName}");

    private static string[] StringValues(JsonArray values)
        => values.Select(value => value?.GetValue<string>() ?? string.Empty).ToArray();
}
