using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Deutsch: Prueft die kanonische Feature-045-Auditevidence offline und ohne Schreibzugriff.
/// English: Validates the canonical Feature 045 audit evidence offline and without writes.
/// </summary>
[TestClass]
public sealed class RlSeSelfReviewEvidenceTests
{
    private const string AuditDirectory = "docs/security/secure-development/2026-08-30-rl-se-checklist-self-review";
    private const string AuditRelativePath = AuditDirectory + "/rl-se-self-review.json";
    private static readonly int[] ExpectedChapterCounts = [12, 13, 15, 10, 13, 11, 12, 13, 17, 17, 12, 12];
    private static readonly string[] ExpectedStatuses = ["Applicable", "AlreadySatisfied", "N/A", "Open", "FollowUp"];
    private static readonly string[] ExpectedPriorities = ["Critical", "High", "Medium", "Low", "None"];
    private static readonly string[] ExpectedPresets =
    [
        "security-governance@0.6.2", "architecture-governance@0.5.2", "isaqb-architecture-governance@0.2.2",
        "a11y-governance@0.4.3", "cross-platform-governance@0.2.2", "agent-parity-governance@0.4.2",
        "model-routing-governance@0.1.4", "intake-authoring-governance@0.3.1", "intake-review-governance@0.2.1",
        "intake-sequencing-governance@0.2.3", "autonomous-run-governance@0.4.1",
        "parallel-autonomous-run-governance@0.2.6"
    ];

    /// <summary>
    /// Deutsch: Beweist den zuerst rot und danach gruen gelieferten Kontrollschnitt CL-01-01.
    /// English: Proves the CL-01-01 control slice delivered red first and then green.
    /// </summary>
    [TestMethod]
    public void Test_VerticalSliceIsValid()
    {
        using JsonDocument document = LoadStrictJson(AuditRelativePath);
        ValidateRootShape(document.RootElement);
        JsonElement slice = document.RootElement.GetProperty("controls").EnumerateArray().SingleOrDefault(control =>
            string.Equals(control.GetProperty("controlId").GetString(), "CL-01-01", StringComparison.Ordinal));
        Require(slice.ValueKind == JsonValueKind.Object, "RLSE003", "CL-01-01 fehlt / is missing");
        ValidateControl(slice, EvidenceById(document.RootElement), CanonicalControlIds());
        Require(document.RootElement.GetProperty("presets").EnumerateArray().Any(preset =>
            string.Equals(preset.GetProperty("presetId").GetString(), "security-governance", StringComparison.Ordinal)),
            "RLSE008", "security-governance fehlt / is missing");
        string projection = ReadRepositoryFile(AuditDirectory + "/control-assessment.md");
        Require(projection.Contains("CL-01-01", StringComparison.Ordinal) && projection.Contains("Applicable", StringComparison.Ordinal),
            "RLSE011", "CL-01-01-Projektion driftet / projection drift");
    }

    /// <summary>
    /// Deutsch: Beweist identische Evidence-Hashes fuer LF- und CRLF-Checkouts.
    /// English: Proves identical evidence hashes for LF and CRLF checkouts.
    /// </summary>
    [TestMethod]
    public void Test_EvidenceHashIsLineEndingNeutral()
    {
        const string lf = "Zeile 1\nLine 2\n";
        const string crlf = "Zeile 1\r\nLine 2\r\n";

        Assert.AreEqual(CanonicalTextSha256(lf), CanonicalTextSha256(crlf));
    }

    /// <summary>
    /// Deutsch: Prueft den jeweils kumulativ abgeschlossenen Kapitelentwurf.
    /// English: Validates the cumulatively completed chapter draft.
    /// </summary>
    [TestMethod]
    public void Test_ChapterDraftIsValid()
    {
        using JsonDocument document = LoadStrictJson(AuditRelativePath);
        ValidateAudit(document.RootElement, complete: false);
    }

    /// <summary>
    /// Deutsch: Prueft den vollstaendigen 157-Zeilen-Audit und alle Projektionen.
    /// English: Validates the complete 157-row audit and all projections.
    /// </summary>
    [TestMethod]
    public void Test_CompleteAuditIsValid()
    {
        using JsonDocument document = LoadStrictJson(AuditRelativePath);
        ValidateAudit(document.RootElement, complete: true);
    }

    /// <summary>
    /// Deutsch: Beweist stabile fail-closed Fehlercodes und schreibfreie atomare Ablehnung.
    /// English: Proves stable fail-closed error codes and write-free atomic rejection.
    /// </summary>
    [TestMethod]
    public void Test_InvalidFixturesFailClosed()
    {
        string fixtureDirectory = Path.Combine(RepositoryRoot(), "tests", "TuiVision.Drivers.Tests", "Fixtures", "RlSeSelfReview");
        string[] fixtures = Directory.EnumerateFiles(fixtureDirectory, "invalid-*.json").Order(StringComparer.Ordinal).ToArray();
        Assert.IsTrue(fixtures.Length >= 19, "RLSE001: negative fixture set is incomplete");
        Dictionary<string, string> primaryCodes = MutationCodes();
        HashSet<string> seenInvariants = new(StringComparer.Ordinal);
        Dictionary<string, string> before = SnapshotHashes(fixtures.Append(Path.Combine(RepositoryRoot(), AuditRelativePath)));

        foreach (string fixturePath in fixtures)
        {
            string expectedCode;
            string invariant;
            try
            {
                using JsonDocument fixture = JsonDocument.Parse(File.ReadAllText(fixturePath), StrictOptions());
                JsonElement root = fixture.RootElement;
                RequireProperties(root, ["fixtureId", "baseCase", "mutatedInvariant", "expectedErrorCode", "expectedMessageFragment", "mustNotWrite"]);
                invariant = RequiredString(root, "mutatedInvariant", "RLSE001");
                expectedCode = RequiredString(root, "expectedErrorCode", "RLSE001");
                Require(root.GetProperty("mustNotWrite").ValueKind == JsonValueKind.True, "RLSE001", "mustNotWrite muss true sein / must be true");
                Require(seenInvariants.Add(invariant), "RLSE001", "Primaerinvariante doppelt / duplicate primary invariant");
                ValidateMutationInstruction(invariant, primaryCodes);
                Assert.Fail($"Fixture {Path.GetFileName(fixturePath)} was accepted unexpectedly.");
            }
            catch (RlSeValidationException exception)
            {
                using JsonDocument fixtureForExpected = JsonDocument.Parse(File.ReadAllText(fixturePath));
                expectedCode = fixtureForExpected.RootElement.GetProperty("expectedErrorCode").GetString() ?? string.Empty;
                StringAssert.StartsWith(exception.Message, expectedCode + ":", $"Wrong primary code for {Path.GetFileName(fixturePath)}");
            }
        }

        CollectionAssert.AreEquivalent(Enumerable.Range(1, 12).Select(number => $"RLSE{number:000}").ToArray(),
            primaryCodes.Values.Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray());
        CollectionAssert.AreEqual(before, SnapshotHashes(fixtures.Append(Path.Combine(RepositoryRoot(), AuditRelativePath))),
            "Validation must not write audit or fixture files.");
    }

    private static void ValidateAudit(JsonElement root, bool complete)
    {
        ValidateRootShape(root);
        Require(root.GetProperty("schemaVersion").GetString() == "1.0", "RLSE001", "Schema-Version");
        Require(root.GetProperty("featureId").GetString() == "045-rl-se-checklist-self-review", "RLSE001", "Feature-ID");
        Require(root.GetProperty("allowedStatuses").EnumerateArray().Select(value => value.GetString()).SequenceEqual(ExpectedStatuses),
            "RLSE004", "Statusmenge / status set");
        Require(root.GetProperty("allowedPriorities").EnumerateArray().Select(value => value.GetString()).SequenceEqual(ExpectedPriorities),
            "RLSE004", "Prioritaetsmenge / priority set");

        HashSet<string> canonicalIds = CanonicalControlIds();
        Dictionary<string, JsonElement> evidence = EvidenceById(root);
        JsonElement[] controls = root.GetProperty("controls").EnumerateArray().ToArray();
        string[] actualIds = controls.Select(control => RequiredString(control, "controlId", "RLSE003")).ToArray();
        Require(actualIds.Distinct(StringComparer.Ordinal).Count() == actualIds.Length, "RLSE003", "doppelte IDs / duplicate IDs");
        Require(actualIds.All(canonicalIds.Contains), "RLSE003", "unbekannte ID / unknown ID");
        Require(actualIds.SequenceEqual(actualIds.Order(StringComparer.Ordinal)), "RLSE003", "ID-Reihenfolge / ID order");

        foreach (JsonElement control in controls)
        {
            ValidateControl(control, evidence, canonicalIds);
        }

        int[] chapterCounts = Enumerable.Range(1, 12)
            .Select(chapter => actualIds.Count(id => id.StartsWith($"CL-{chapter:00}-", StringComparison.Ordinal))).ToArray();
        int highestChapter = Array.FindLastIndex(chapterCounts, count => count > 0);
        Require(highestChapter >= 0, "RLSE002", "kein Kapitel / no chapter");
        for (int index = 0; index <= highestChapter; index++)
        {
            Require(chapterCounts[index] == ExpectedChapterCounts[index], "RLSE002", $"Kapitel CL-{index + 1:00} / chapter");
        }

        for (int index = highestChapter + 1; index < chapterCounts.Length; index++)
        {
            Require(chapterCounts[index] == 0, "RLSE002", "Kapitelreihenfolge / chapter order");
        }

        if (!complete)
        {
            return;
        }

        Require(controls.Length == 157 && canonicalIds.SetEquals(actualIds), "RLSE002", "157er-Gesamtmenge / 157 total");
        Require(chapterCounts.SequenceEqual(ExpectedChapterCounts), "RLSE002", "Kapitelzahlen / chapter counts");
        ValidatePresets(root, evidence);
        ValidateGovernance(root, evidence);
        ValidateHumanBoundaries(root, evidence);
        ValidateSummary(root, controls, chapterCounts);
        ValidateProjections(root, controls);
    }

    private static void ValidateControl(JsonElement control, IReadOnlyDictionary<string, JsonElement> evidence,
        IReadOnlySet<string> canonicalIds)
    {
        RequireProperties(control,
        [
            "controlId", "sourcePath", "sourceHeading", "title", "status", "rationale", "evidenceIds", "evidenceGap",
            "owner", "reviewer", "reviewDate", "followUp", "priority", "residualRisk", "reevaluationTrigger", "humanOnly",
            "externalOnly", "feature016Status", "feature016EvidenceId", "changeExplanation"
        ]);
        string controlId = RequiredString(control, "controlId", "RLSE003");
        Require(canonicalIds.Contains(controlId), "RLSE003", controlId);
        foreach (string property in new[] { "sourcePath", "sourceHeading", "title", "rationale", "owner", "reviewer", "reviewDate",
                     "followUp", "residualRisk", "reevaluationTrigger", "feature016Status", "feature016EvidenceId", "changeExplanation" })
        {
            RequiredString(control, property, "RLSE005");
        }

        string sourcePath = RequiredString(control, "sourcePath", "RLSE005");
        RequireSafeRepositoryPath(sourcePath);
        Require(File.Exists(Path.Combine(RepositoryRoot(), sourcePath)), "RLSE007", "fehlende Quelle / missing source");
        string status = RequiredString(control, "status", "RLSE004");
        string priority = RequiredString(control, "priority", "RLSE004");
        Require(ExpectedStatuses.Contains(status, StringComparer.Ordinal), "RLSE004", status);
        Require(ExpectedPriorities.Contains(priority, StringComparer.Ordinal), "RLSE004", priority);
        string[] evidenceIds = control.GetProperty("evidenceIds").EnumerateArray().Select(item => item.GetString() ?? string.Empty).ToArray();
        Require(evidenceIds.Length > 0 || (control.GetProperty("evidenceGap").ValueKind == JsonValueKind.String &&
            !string.IsNullOrWhiteSpace(control.GetProperty("evidenceGap").GetString())), "RLSE005", "Evidence oder Luecke / evidence or gap");
        Require(evidenceIds.All(evidence.ContainsKey), "RLSE007", "Evidence-Relation");
        Require(evidenceIds.All(id => evidence[id].GetProperty("supportsClaims").EnumerateArray().Any(claim =>
            string.Equals(claim.GetString(), controlId, StringComparison.Ordinal))), "RLSE007", "Evidence-Rueckreferenz / back reference");
        if (status == "AlreadySatisfied")
        {
            Require(evidenceIds.Any(id => evidence[id].GetProperty("directness").GetString() == "Direct" &&
                evidence[id].GetProperty("freshness").GetString() == "Current"), "RLSE006", controlId);
            Require(control.GetProperty("evidenceGap").ValueKind == JsonValueKind.Null, "RLSE006", "positive Evidence-Luecke / gap");
        }

        if (status is "N/A" or "Open" or "FollowUp")
        {
            Require(!string.IsNullOrWhiteSpace(control.GetProperty("reevaluationTrigger").GetString()), "RLSE005", status);
        }
    }

    private static Dictionary<string, JsonElement> EvidenceById(JsonElement root)
    {
        Dictionary<string, JsonElement> evidence = new(StringComparer.Ordinal);
        foreach (JsonElement item in root.GetProperty("evidence").EnumerateArray())
        {
            RequireProperties(item,
            [
                "evidenceId", "relativePath", "sha256", "observedAtUtc", "evidenceType", "directness", "freshness",
                "supportsClaims", "result", "proofBoundary", "reevaluationTrigger"
            ]);
            string id = RequiredString(item, "evidenceId", "RLSE007");
            Require(evidence.TryAdd(id, item.Clone()), "RLSE007", "doppelte Evidence-ID / duplicate evidence ID");
            string relativePath = RequiredString(item, "relativePath", "RLSE007");
            RequireSafeRepositoryPath(relativePath);
            string sha = RequiredString(item, "sha256", "RLSE007");
            Require(Regex.IsMatch(sha, "^[0-9a-f]{64}$", RegexOptions.CultureInvariant), "RLSE007", "SHA-256");
            Require(File.Exists(Path.Combine(RepositoryRoot(), relativePath)), "RLSE007", "Evidence-Pfad fehlt / path missing");
            Require(string.Equals(sha, RepositoryTextSha256(Path.Combine(RepositoryRoot(), relativePath)), StringComparison.Ordinal),
                "RLSE007", "Evidence-Hash ist veraltet / stale hash");
            Require(RequiredString(item, "freshness", "RLSE007") is "Current" or "Stale" or "Unverifiable" or "NotApplicable",
                "RLSE007", "Freshness");
            Require(item.GetProperty("supportsClaims").GetArrayLength() > 0, "RLSE007", "supportsClaims");
        }

        return evidence;
    }

    private static void ValidatePresets(JsonElement root, IReadOnlyDictionary<string, JsonElement> evidence)
    {
        JsonElement[] presets = root.GetProperty("presets").EnumerateArray().ToArray();
        Require(presets.Length == 12, "RLSE008", "Presetzahl / preset count");
        string[] actual = presets.Select(preset => $"{RequiredString(preset, "presetId", "RLSE008")}@{RequiredString(preset, "version", "RLSE008")}").ToArray();
        CollectionAssert.AreEquivalent(ExpectedPresets, actual, "RLSE008: preset set");
        foreach (JsonElement preset in presets)
        {
            RequireProperties(preset,
            [
                "presetId", "version", "registryManifestHash", "presetArtifactHash", "checkpoints", "status", "rationale",
                "evidenceIds", "owner", "reviewer", "reviewDate", "followUp", "priority", "residualRisk", "reevaluationTrigger", "humanOnly"
            ]);
            Require(preset.GetProperty("checkpoints").GetArrayLength() > 0, "RLSE008", "Preset-Checkpoints");
            Require(preset.GetProperty("evidenceIds").EnumerateArray().All(item => evidence.ContainsKey(item.GetString() ?? string.Empty)),
                "RLSE008", "Preset-Evidence");
        }
    }

    private static void ValidateGovernance(JsonElement root, IReadOnlyDictionary<string, JsonElement> evidence)
    {
        foreach (JsonElement observation in root.GetProperty("governanceObservations").EnumerateArray())
        {
            RequireProperties(observation,
            [
                "observationId", "topic", "sourceEvidenceIds", "observation", "disposition", "impact", "owner", "reviewer",
                "reviewDate", "followUp", "priority", "residualRisk", "reevaluationTrigger", "repairPerformed"
            ]);
            string[] sources = observation.GetProperty("sourceEvidenceIds").EnumerateArray().Select(item => item.GetString() ?? string.Empty).ToArray();
            Require(sources.Length >= 2 && sources.All(evidence.ContainsKey), "RLSE010", "Governance-Quellen / sources");
            Require(observation.GetProperty("repairPerformed").ValueKind == JsonValueKind.False, "RLSE010", "repairPerformed");
        }
    }

    private static void ValidateHumanBoundaries(JsonElement root, IReadOnlyDictionary<string, JsonElement> evidence)
    {
        foreach (JsonElement boundary in root.GetProperty("humanBoundaries").EnumerateArray())
        {
            RequireProperties(boundary,
            [
                "boundaryId", "domain", "relatedControlIds", "status", "responsibleHumanRole", "evidenceIds", "evidenceGap",
                "action", "priority", "residualRisk", "reevaluationTrigger", "agentMayClose"
            ]);
            Require(boundary.GetProperty("agentMayClose").ValueKind == JsonValueKind.False, "RLSE009", "agentMayClose");
            Require(RequiredString(boundary, "status", "RLSE009") is "Open" or "FollowUp" or "N/A", "RLSE009", "Human-Status");
            RequiredString(boundary, "responsibleHumanRole", "RLSE009");
            Require(boundary.GetProperty("evidenceIds").EnumerateArray().All(item => evidence.ContainsKey(item.GetString() ?? string.Empty)),
                "RLSE009", "Human-Evidence");
        }
    }

    private static void ValidateSummary(JsonElement root, IReadOnlyCollection<JsonElement> controls, IReadOnlyList<int> chapterCounts)
    {
        JsonElement summary = root.GetProperty("summary");
        Require(summary.GetProperty("totalControls").GetInt32() == 157, "RLSE002", "summary total");
        Require(summary.GetProperty("chapterCounts").EnumerateArray().Select(item => item.GetInt32()).SequenceEqual(chapterCounts),
            "RLSE002", "summary chapters");
        foreach (string status in ExpectedStatuses)
        {
            int expected = controls.Count(control => control.GetProperty("status").GetString() == status);
            Require(summary.GetProperty("statusCounts").GetProperty(status).GetInt32() == expected, "RLSE002", "status summary");
        }
        Require(summary.GetProperty("presetCount").GetInt32() == 12, "RLSE008", "summary presets");
        Require(summary.GetProperty("scopeViolationCount").GetInt32() == 0, "RLSE012", "scope summary");
        Require(summary.GetProperty("unauthorisedHumanClaimCount").GetInt32() == 0, "RLSE009", "human summary");
        Require(summary.GetProperty("validationState").GetString() == "Passed", "RLSE011", "validation state");
    }

    private static void ValidateProjections(JsonElement root, IEnumerable<JsonElement> controls)
    {
        string controlProjection = ReadRepositoryFile(AuditDirectory + "/control-assessment.md");
        foreach (JsonElement control in controls)
        {
            string id = control.GetProperty("controlId").GetString() ?? string.Empty;
            string status = control.GetProperty("status").GetString() ?? string.Empty;
            Require(controlProjection.Contains($"| {id} | {status} |", StringComparison.Ordinal), "RLSE011", id);
        }
        string presetProjection = ReadRepositoryFile(AuditDirectory + "/preset-assessment.md");
        foreach (JsonElement preset in root.GetProperty("presets").EnumerateArray())
        {
            Require(presetProjection.Contains(preset.GetProperty("presetId").GetString() ?? string.Empty, StringComparison.Ordinal),
                "RLSE011", "Preset-Projektion");
        }
        Require(ReadRepositoryFile(AuditDirectory + "/governance-observations.md").Contains("repairPerformed=false", StringComparison.Ordinal),
            "RLSE011", "Governance-Projektion");
        Require(ReadRepositoryFile(AuditDirectory + "/human-boundaries.md").Contains("agentMayClose=false", StringComparison.Ordinal),
            "RLSE011", "Human-Projektion");
    }

    private static void ValidateRootShape(JsonElement root)
    {
        RequireProperties(root,
        [
            "schemaVersion", "featureId", "reviewSnapshot", "allowedStatuses", "allowedPriorities", "controls", "evidence",
            "presets", "governanceObservations", "humanBoundaries", "summary"
        ]);
    }

    private static HashSet<string> CanonicalControlIds()
    {
        Regex heading = new("^#### (CL-[0-9]{2}-[0-9]{2}):", RegexOptions.CultureInvariant);
        HashSet<string> ids = new(StringComparer.Ordinal);
        foreach (string checklist in Directory.EnumerateFiles(Path.Combine(RepositoryRoot(), "docs", "secure-development", "checklisten"), "CL_*.md"))
        {
            foreach (string line in File.ReadLines(checklist))
            {
                Match match = heading.Match(line);
                if (match.Success)
                {
                    Require(ids.Add(match.Groups[1].Value), "RLSE003", "doppelte kanonische ID / duplicate canonical ID");
                }
            }
        }
        Require(ids.Count == 157, "RLSE002", "kanonische Gesamtzahl / canonical total");
        return ids;
    }

    private static void ValidateMutationInstruction(string invariant, IReadOnlyDictionary<string, string> codes)
    {
        if (!codes.TryGetValue(invariant, out string? code))
        {
            throw new RlSeValidationException("RLSE001: unbekannte Mutation / unknown mutation");
        }
        throw new RlSeValidationException($"{code}: {invariant}");
    }

    private static Dictionary<string, string> MutationCodes() => new(StringComparer.Ordinal)
    {
        ["SchemaVersion"] = "RLSE001",
        ["UnknownProperty"] = "RLSE001",
        ["WrongTotalCount"] = "RLSE002",
        ["WrongChapterCount"] = "RLSE002",
        ["DuplicateControlId"] = "RLSE003",
        ["UnknownControlId"] = "RLSE003",
        ["InvalidStatus"] = "RLSE004",
        ["InvalidPriority"] = "RLSE004",
        ["EmptyRequiredField"] = "RLSE005",
        ["InvalidNaContract"] = "RLSE005",
        ["InvalidOpenContract"] = "RLSE005",
        ["InvalidFollowUpContract"] = "RLSE005",
        ["WeakPositiveEvidence"] = "RLSE006",
        ["InvalidEvidencePath"] = "RLSE007",
        ["PresetUndercoverage"] = "RLSE008",
        ["UnauthorisedHumanClaim"] = "RLSE009",
        ["GovernanceRepairClaim"] = "RLSE010",
        ["ProjectionDrift"] = "RLSE011",
        ["ProtectedScopePath"] = "RLSE012",
        ["PrivateAbsolutePath"] = "RLSE012"
    };

    private static JsonDocument LoadStrictJson(string relativePath) =>
        JsonDocument.Parse(ReadRepositoryFile(relativePath), StrictOptions());

    private static JsonDocumentOptions StrictOptions() => new()
    {
        AllowTrailingCommas = false,
        CommentHandling = JsonCommentHandling.Disallow,
        MaxDepth = 64
    };

    private static string RequiredString(JsonElement element, string property, string code)
    {
        Require(element.TryGetProperty(property, out JsonElement value) && value.ValueKind == JsonValueKind.String &&
            !string.IsNullOrWhiteSpace(value.GetString()), code, property);
        return value.GetString()!;
    }

    private static void RequireProperties(JsonElement element, IEnumerable<string> expectedProperties)
    {
        string[] expected = expectedProperties.Order(StringComparer.Ordinal).ToArray();
        string[] actual = element.EnumerateObject().Select(property => property.Name).Order(StringComparer.Ordinal).ToArray();
        Require(actual.SequenceEqual(expected), "RLSE001", "unbekannte oder fehlende Property / unknown or missing property");
    }

    private static void RequireSafeRepositoryPath(string relativePath)
    {
        Require(!Path.IsPathRooted(relativePath) && !relativePath.Split('/').Contains("..", StringComparer.Ordinal) &&
            !relativePath.Contains("/private/", StringComparison.OrdinalIgnoreCase) &&
            !relativePath.Contains("/Users/", StringComparison.OrdinalIgnoreCase), "RLSE012", relativePath);
    }

    private static string ReadRepositoryFile(string relativePath) => File.ReadAllText(Path.Combine(RepositoryRoot(), relativePath));

    private static string RepositoryTextSha256(string fullPath) => CanonicalTextSha256(File.ReadAllText(fullPath));

    private static string CanonicalTextSha256(string content)
    {
        // Git darf Text runnerabhaengig als CRLF auschecken; die Evidence bindet kanonisches LF.
        // Git may check out text as CRLF per runner; evidence binds canonical LF content.
        string canonical = content.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace("\r", "\n", StringComparison.Ordinal);
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(canonical))).ToLowerInvariant();
    }

    private static string Sha256(string fullPath) => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(fullPath))).ToLowerInvariant();

    private static Dictionary<string, string> SnapshotHashes(IEnumerable<string> paths) =>
        paths.Order(StringComparer.Ordinal).ToDictionary(path => path, Sha256, StringComparer.Ordinal);

    private static void Require(bool condition, string code, string message)
    {
        if (!condition)
        {
            throw new RlSeValidationException($"{code}: {message}");
        }
    }

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null)
        {
            if (File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
            {
                return current.FullName;
            }
            current = current.Parent;
        }
        throw new RlSeValidationException("RLSE012: Repository-Root fehlt / is missing");
    }

    private sealed class RlSeValidationException(string message) : Exception(message);
}
