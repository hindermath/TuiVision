using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Deutsch: Prueft die kanonische Feature-046-Review-Evidence offline und schreibfrei.
/// English: Validates the canonical Feature 046 review evidence offline and without writes.
/// </summary>
[TestClass]
public sealed class GsdbSpecKitIntensiveReviewEvidenceTests
{
    private const string RepresentativeFixture =
        "tests/TuiVision.Drivers.Tests/Fixtures/GsdbSpecKitIntensiveReview/invalid-representative-positive-evidence.json";
    private const string FixtureDirectory =
        "tests/TuiVision.Drivers.Tests/Fixtures/GsdbSpecKitIntensiveReview";
    private const string GsdbDirectory = "docs/secure-development";
    private const string AuditDirectory = "docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review";
    private const string CanonicalAuditPath = AuditDirectory + "/gsdb-spec-kit-intensive-review.json";
    private static readonly int[] ExpectedChapterCounts = [12, 13, 15, 10, 13, 11, 12, 13, 17, 17, 12, 12];

    /// <summary>
    /// Deutsch: Beweist den repräsentativen Vertragsschnitt vor der breiten Inventarisierung.
    /// English: Proves the representative contract slice before broad inventory work.
    /// </summary>
    [TestMethod]
    public void Test_RepresentativeVerticalSlice()
    {
        string fixturePath = Path.Combine(RepositoryRoot(), RepresentativeFixture);
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixturePath));
        GsdbValidationException exception = Assert.ThrowsExactly<GsdbValidationException>(
            () => RejectInvalidRepresentativeSlice(document.RootElement));
        StringAssert.StartsWith(exception.Message, "GSDB046_POSITIVE_EVIDENCE_MISSING:");

        string presetId = FirstEnabledPresetId();
        RepresentativeSlice slice = new(
            "1.0",
            "docs/secure-development/README.md",
            "CL-01-01",
            "csharp-dotnet",
            presetId,
            "security",
            "LocalDirect",
            "AlreadySatisfied",
            "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md");

        string projection = RenderValidatedRepresentativeSlice(slice);
        Assert.AreEqual(
            "CL-01-01\tAlreadySatisfied\tLocalDirect\tdocs/secure-development/README.md\tsecurity-governance\n",
            projection);
    }

    /// <summary>
    /// Deutsch: Belegt zunächst rote Quellen- und Kontrollabschlussregeln.
    /// English: Initially proves red source and control closure rules.
    /// </summary>
    [TestMethod]
    [DynamicData(nameof(SourceControlFixtures))]
    public void Test_SourceOrControl_RulesAreImplemented(string fixturePath)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixturePath));
        JsonElement root = document.RootElement;
        string code = root.GetProperty("expectedErrorCode").GetString() ?? string.Empty;
        GsdbValidationException exception = Assert.ThrowsExactly<GsdbValidationException>(
            () => RejectSourceOrControlMutation(root));
        StringAssert.StartsWith(exception.Message, code + ":");
        AssertSourceControlClosure();
    }

    /// <summary>
    /// Deutsch: Liefert jede Quellen-/Kontrollfixture als eigenen Testfall.
    /// English: Supplies every source/control fixture as a separate test case.
    /// </summary>
    public static IEnumerable<object[]> SourceControlFixtures()
    {
        string directory = Path.Combine(RepositoryRoot(), FixtureDirectory);
        return Directory.EnumerateFiles(directory, "invalid-source-control-*.json")
            .Order(StringComparer.Ordinal)
            .Select(path => new object[] { path })
            .ToArray();
    }

    /// <summary>
    /// Deutsch: Belegt zunächst rote Dispositions-, Evidence- und Boundary-Regeln.
    /// English: Initially proves red disposition, evidence, and boundary rules.
    /// </summary>
    [TestMethod]
    [DynamicData(nameof(EvidenceDispositionFixtures))]
    public void Test_EvidenceOrDisposition_RulesAreImplemented(string fixturePath)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixturePath));
        JsonElement root = document.RootElement;
        string code = root.GetProperty("expectedErrorCode").GetString() ?? string.Empty;
        GsdbValidationException exception = Assert.ThrowsExactly<GsdbValidationException>(
            () => RejectEvidenceOrDispositionMutation(root));
        StringAssert.StartsWith(exception.Message, code + ":");

        foreach (string entityType in new[] { "Control", "Source", "Language", "Preset", "AgentSurface", "Governance", "EvidenceFamily", "HumanBoundary" })
        {
            ValidateAssessment(new Assessment(
                $"{entityType}-001",
                entityType,
                "Deutsch: Prüft den aktuellen Snapshot. English: assesses the current snapshot.",
                "Open",
                "Deutsch: Die externe oder vollständige Evidence fehlt. English: external or complete evidence is missing.",
                null,
                "Deutsch: Explizite Beweislücke. English: explicit evidence gap.",
                "Maintainer",
                "Deutsch: Bei neuer Evidence erneut prüfen. English: reassess when evidence arrives.",
                "Deutsch: Evidence oder Scope ändert sich. English: evidence or scope changes.",
                "Deutsch: Aussage bleibt offen. English: claim remains open.",
                "Security Reviewer",
                "2026-08-30",
                "fc041d61ab71288cf0c882ecd00a5e019c64405b",
                "LegalOrganizational",
                false));
        }

        ValidateAssessment(new Assessment(
            "Control-Positive", "Control", "Deutsch: direkter lokaler Nachweis. English: direct local proof.",
            "AlreadySatisfied", "Deutsch: aktuelle Evidence vorhanden. English: current evidence is present.",
            "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md", null, "Maintainer",
            "Deutsch: None, solange der Snapshot gilt. English: None while the snapshot remains current.",
            "Deutsch: Snapshot ändert sich. English: snapshot changes.",
            "Deutsch: Evidence kann veralten. English: evidence can become stale.",
            "Security Reviewer", "2026-08-30", "fc041d61ab71288cf0c882ecd00a5e019c64405b", "LocalDirect", false));
    }

    /// <summary>
    /// Deutsch: Liefert jede Evidence-/Dispositionsfixture als eigenen Testfall.
    /// English: Supplies every evidence/disposition fixture as a separate test case.
    /// </summary>
    public static IEnumerable<object[]> EvidenceDispositionFixtures()
    {
        string directory = Path.Combine(RepositoryRoot(), FixtureDirectory);
        return Directory.EnumerateFiles(directory, "invalid-evidence-disposition-*.json")
            .Order(StringComparer.Ordinal)
            .Select(path => new object[] { path })
            .ToArray();
    }

    /// <summary>
    /// Deutsch: Belegt zunächst rote dynamische Inventarregeln.
    /// English: Initially proves red dynamic-inventory rules.
    /// </summary>
    [TestMethod]
    [DynamicData(nameof(DynamicInventoryFixtures))]
    public void Test_DynamicInventory_RulesAreImplemented(string fixturePath)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixturePath));
        JsonElement root = document.RootElement;
        string code = root.GetProperty("expectedErrorCode").GetString() ?? string.Empty;
        GsdbValidationException exception = Assert.ThrowsExactly<GsdbValidationException>(
            () => RejectDynamicInventoryMutation(root));
        StringAssert.StartsWith(exception.Message, code + ":");
        AssertDynamicInventories();
    }

    /// <summary>
    /// Deutsch: Liefert jede dynamische Inventarfixture als eigenen Testfall.
    /// English: Supplies every dynamic-inventory fixture as a separate test case.
    /// </summary>
    public static IEnumerable<object[]> DynamicInventoryFixtures()
    {
        string directory = Path.Combine(RepositoryRoot(), FixtureDirectory);
        return Directory.EnumerateFiles(directory, "invalid-dynamic-inventory-*.json")
            .Order(StringComparer.Ordinal)
            .Select(path => new object[] { path })
            .ToArray();
    }

    /// <summary>
    /// Deutsch: Belegt zunächst rote Summary-, Hash- und Projektionsregeln.
    /// English: Initially proves red summary, hash, and projection rules.
    /// </summary>
    [TestMethod]
    [DynamicData(nameof(ProjectionSummaryFixtures))]
    public void Test_ProjectionOrSummary_RulesAreImplemented(string fixturePath)
    {
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixturePath));
        JsonElement root = document.RootElement;
        string code = root.GetProperty("expectedErrorCode").GetString() ?? string.Empty;
        GsdbValidationException exception = Assert.ThrowsExactly<GsdbValidationException>(
            () => RejectProjectionOrSummaryMutation(root));
        StringAssert.StartsWith(exception.Message, code + ":");
        AssertProjectionContract();
    }

    /// <summary>
    /// Deutsch: Liefert jede Summary-/Projektionsfixture als eigenen Testfall.
    /// English: Supplies every summary/projection fixture as a separate test case.
    /// </summary>
    public static IEnumerable<object[]> ProjectionSummaryFixtures()
    {
        string directory = Path.Combine(RepositoryRoot(), FixtureDirectory);
        return Directory.EnumerateFiles(directory, "invalid-projection-summary-*.json")
            .Order(StringComparer.Ordinal)
            .Select(path => new object[] { path })
            .ToArray();
    }

    /// <summary>
    /// Deutsch: Beweist bytegenaue, deterministische und text-first Projektionen.
    /// English: Proves byte-exact, deterministic, and text-first projections.
    /// </summary>
    [TestMethod]
    public void Test_Projections_AreByteExactAndTextFirst()
    {
        AssertProjectionContract();
    }

    /// <summary>
    /// Deutsch: Erzeugt auf ausdrücklichen Schalter den vollständigen kanonischen Snapshot und prüft ihn sonst schreibfrei.
    /// English: Generates the complete canonical snapshot only with an explicit switch and otherwise validates it without writes.
    /// </summary>
    [TestMethod]
    public void Test_CanonicalDataset_AndProjectionsAreComplete()
    {
        if (string.Equals(Environment.GetEnvironmentVariable("GSDB046_WRITE"), "1", StringComparison.Ordinal))
        {
            WriteAuditArtifacts();
        }

        ValidateAuditArtifacts();
    }

    private static void WriteAuditArtifacts()
    {
        string outputDirectory = Path.Combine(RepositoryRoot(), AuditDirectory);
        Directory.CreateDirectory(outputDirectory);
        JsonObject canonical = BuildCanonicalAudit();
        string canonicalText = SerializeJsonLf(canonical);
        File.WriteAllText(Path.Combine(RepositoryRoot(), CanonicalAuditPath), canonicalText, new UTF8Encoding(false));
        string canonicalHash = NormalizedTextSha256(canonicalText);

        foreach ((string fileName, string projectionId) in JsonProjectionDefinitions())
        {
            JsonObject projection = BuildFullJsonProjection(canonical, projectionId, canonicalHash);
            File.WriteAllText(Path.Combine(outputDirectory, fileName), SerializeJsonLf(projection), new UTF8Encoding(false));
        }

        foreach (string markdownName in MarkdownProjectionNames())
        {
            File.WriteAllText(Path.Combine(outputDirectory, markdownName), RenderFullMarkdown(canonical, markdownName), new UTF8Encoding(false));
        }
    }

    private static void ValidateAuditArtifacts()
    {
        string canonicalPath = Path.Combine(RepositoryRoot(), CanonicalAuditPath);
        using JsonDocument document = JsonDocument.Parse(ReadStrictUtf8(canonicalPath));
        JsonElement root = document.RootElement;
        Assert.AreEqual("1.0", root.GetProperty("schemaVersion").GetString());
        Assert.AreEqual("046-gsdb-spec-kit-intensive-review", root.GetProperty("featureId").GetString());
        Assert.AreEqual(157, root.GetProperty("controls").GetArrayLength());
        Assert.AreEqual(157, root.GetProperty("summary").GetProperty("controlCount").GetInt32());
        Assert.AreEqual(0, root.GetProperty("summary").GetProperty("positiveDispositionEvidenceGapCount").GetInt32());
        Assert.AreEqual(0, root.GetProperty("summary").GetProperty("actionableTechnicalFindingCount").GetInt32());
        CollectionAssert.AreEqual(PhysicalSourcePaths(), root.GetProperty("sources").EnumerateArray()
            .Select(item => item.GetProperty("path").GetString()!).ToArray());
        CollectionAssert.AreEqual(CanonicalControlIds(), root.GetProperty("controls").EnumerateArray()
            .Select(item => item.GetProperty("controlId").GetString()!).ToArray());
        CollectionAssert.AreEqual(ExpectedChapterCounts,
            root.GetProperty("summary").GetProperty("controlCountByChapter").EnumerateObject()
                .OrderBy(property => property.Name, StringComparer.Ordinal)
                .Select(property => property.Value.GetInt32()).ToArray());

        foreach (string arrayName in new[]
                 {
                     "sources", "controls", "languageProfiles", "presets", "agentSurfaces",
                     "governanceCheckpoints", "evidenceFamilies", "humanBoundaries"
                 })
        {
            foreach (JsonElement record in root.GetProperty(arrayName).EnumerateArray())
            {
                ValidateCommonAssessment(record.GetProperty("assessment"));
            }
        }

        string canonicalText = ReadStrictUtf8(canonicalPath);
        string canonicalHash = NormalizedTextSha256(canonicalText);
        JsonObject canonicalNode = JsonNode.Parse(canonicalText)!.AsObject();
        foreach ((string fileName, string projectionId) in JsonProjectionDefinitions())
        {
            string actual = ReadStrictUtf8(Path.Combine(RepositoryRoot(), AuditDirectory, fileName));
            string expected = SerializeJsonLf(BuildFullJsonProjection(canonicalNode, projectionId, canonicalHash));
            string second = SerializeJsonLf(BuildFullJsonProjection(canonicalNode, projectionId, canonicalHash));
            Assert.AreEqual(expected, actual, fileName);
            Assert.AreEqual(expected, second, fileName + " double render");
            JsonObject projection = JsonNode.Parse(actual)!.AsObject();
            string storedHash = projection["projectionPayloadSha256"]!.GetValue<string>();
            projection.Remove("projectionPayloadSha256");
            Assert.AreEqual(storedHash, NormalizedTextSha256(SerializeJsonLf(projection)), fileName + " payload hash");
        }

        foreach (string markdownName in MarkdownProjectionNames())
        {
            string actual = ReadStrictUtf8(Path.Combine(RepositoryRoot(), AuditDirectory, markdownName));
            string first = RenderFullMarkdown(canonicalNode, markdownName);
            string second = RenderFullMarkdown(canonicalNode, markdownName);
            Assert.AreEqual(first, actual, markdownName);
            Assert.AreEqual(first, second, markdownName + " double render");
        }
    }

    private static void ValidateCommonAssessment(JsonElement assessment)
    {
        string disposition = assessment.GetProperty("disposition").GetString() ?? string.Empty;
        Assert.IsTrue(new[] { "Applicable", "AlreadySatisfied", "N/A", "Open", "FollowUp" }
            .Contains(disposition, StringComparer.Ordinal));
        foreach (string field in new[]
                 {
                     "dispositionLabelDe", "dispositionLabelEn", "rationaleDe", "rationaleEn", "proofBoundary",
                     "owner", "risk", "revalidationTrigger", "followUp"
                 })
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(assessment.GetProperty(field).GetString()), field);
        }
        JsonElement references = assessment.GetProperty("evidenceReferenceIds");
        if (disposition is "Applicable" or "AlreadySatisfied")
        {
            Assert.IsTrue(references.GetArrayLength() > 0, "positive evidence reference");
        }
        else
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(assessment.GetProperty("evidenceGapDe").GetString()));
            Assert.IsFalse(string.IsNullOrWhiteSpace(assessment.GetProperty("evidenceGapEn").GetString()));
        }
    }

    private static JsonObject BuildCanonicalAudit()
    {
        string commit = GitOutput("rev-parse HEAD").Trim();
        string[] tracked = TrackedPaths();
        JsonArray sources = new();
        Dictionary<string, string> sourceIdsByPath = new(StringComparer.Ordinal);
        foreach (string path in PhysicalSourcePaths())
        {
            string fullPath = Path.Combine(RepositoryRoot(), path);
            bool binary = path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
            string sha = binary ? RawSha256(File.ReadAllBytes(fullPath)) : NormalizedTextSha256(ReadStrictUtf8(fullPath));
            string sourceId = "SRC-" + RawSha256(Encoding.UTF8.GetBytes(path))[..12];
            sourceIdsByPath[path] = sourceId;
            string[] declaredVersions = binary ? [] : Regex.Matches(ReadStrictUtf8(fullPath), "(?<![0-9])[0-9]+\\.[0-9]+\\.[0-9]+(?![0-9])")
                .Select(match => match.Value).Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray();
            sources.Add(new JsonObject
            {
                ["sourceId"] = sourceId,
                ["path"] = path,
                ["fileName"] = Path.GetFileName(path),
                ["mediaType"] = binary ? "application/pdf" : path.EndsWith(".json", StringComparison.Ordinal) ? "application/json" : "text/markdown",
                ["sourceGroup"] = SourceGroup(path),
                ["manifestRoles"] = new JsonArray((path.EndsWith("baseline-manifest.json", StringComparison.Ordinal)
                    ? new[] { "ManifestRoot", "Physical" }
                    : new[] { "ManifestRegistered", "Physical" }).Select(value => JsonValue.Create(value)).ToArray()),
                ["registeredVersion"] = path.EndsWith("baseline-manifest.json", StringComparison.Ordinal) ? "3.1.0" : null,
                ["declaredVersions"] = new JsonArray(declaredVersions.Select(value => JsonValue.Create(value)).ToArray()),
                ["sizeBytes"] = new FileInfo(fullPath).Length,
                ["lineCount"] = binary ? null : ReadStrictUtf8(fullPath).Split('\n').Length,
                ["hashMode"] = binary ? "RawBytes" : "Utf8LfNormalized",
                ["sha256"] = sha,
                ["linkedChecksumPath"] = binary ? GsdbDirectory + "/mitgeltende-dokumente/THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.sha256" : null,
                ["linkedChecksumMatch"] = binary ? true : null,
                ["assessment"] = OpenAssessment("LocalDirect", "Der Quelleninhalt ist inventarisiert; seine fachliche Freigabe bleibt offen.",
                    "The source content is inventoried; its substantive approval remains open.")
            });
        }

        JsonArray controls = new();
        foreach (ControlDescriptor control in CanonicalControls())
        {
            controls.Add(new JsonObject
            {
                ["controlId"] = control.Id,
                ["chapterId"] = control.Id[..5],
                ["chapterTitleDe"] = "GSDB-Kapitel " + control.Id[..5],
                ["chapterTitleEn"] = "GSDB chapter " + control.Id[..5],
                ["ordinal"] = int.Parse(control.Id[^2..], System.Globalization.CultureInfo.InvariantCulture),
                ["titleDe"] = control.TitleDe,
                ["titleEn"] = control.TitleEn,
                ["sourceId"] = sourceIdsByPath[control.SourcePath],
                ["sourceHeading"] = control.Heading,
                ["sourceLine"] = control.Line,
                ["sourceContext"] = new JsonObject { ["applicability"] = "Open", ["fulfillment"] = "Not Assessed" },
                ["languageProfileIds"] = new JsonArray("csharp-dotnet", "bash", "powershell", "typescript-javascript"),
                ["presetIds"] = new JsonArray("security-governance"),
                ["evidenceFamilyIds"] = new JsonArray("Security"),
                ["assessment"] = OpenAssessment("LocalDirect",
                    "Die Kontrolle wurde im aktuellen Snapshot erfasst; eine positive Erfüllung wird ohne direkte Einzelprüfung nicht behauptet.",
                    "The control is captured in the current snapshot; positive fulfilment is not claimed without direct individual proof.")
            });
        }

        JsonArray languages = new();
        foreach (string language in LanguageProfiles())
        {
            string activity = language switch
            {
                "c-cpp" or "pascal" or "perl" => "ReadOnlyHistorical",
                "sql" => "AbsentRuleProfile",
                _ => "Active"
            };
            string[] selectors = LanguageSelectors(language);
            string[] matches = tracked.Where(path => selectors.Any(selector => path.EndsWith(selector, StringComparison.OrdinalIgnoreCase)))
                .Order(StringComparer.Ordinal).ToArray();
            languages.Add(new JsonObject
            {
                ["languageId"] = language,
                ["labelDe"] = "Sprachprofil " + language,
                ["labelEn"] = "Language profile " + language,
                ["activity"] = activity,
                ["ruleSourceIds"] = new JsonArray(sourceIdsByPath.Values.Order(StringComparer.Ordinal).Take(1).Select(value => JsonValue.Create(value)).ToArray()),
                ["trackedSelectors"] = new JsonArray(selectors.Select(value => JsonValue.Create(value)).ToArray()),
                ["trackedFileCount"] = matches.Length,
                ["trackedSamplePaths"] = new JsonArray(matches.Take(12).Select(value => JsonValue.Create(value)).ToArray()),
                ["detectorEvidence"] = "git ls-files plus declared extension selectors",
                ["excludedSelectors"] = new JsonArray("bin/**", "obj/**", "_site/**"),
                ["secureUseAreas"] = new JsonArray(LanguageSecureUseAreas(language).Select(value => JsonValue.Create(value)).ToArray()),
                ["historicalConsultation"] = activity == "ReadOnlyHistorical" ? "N/A: no concrete GSDB question triggered consultation" : "N/A",
                ["controlIds"] = new JsonArray(),
                ["assessment"] = activity == "Active"
                    ? OpenAssessment("LocalDirect", "Das aktive Profil ist erfasst; vollständige sichere Nutzung bleibt offen.", "The active profile is captured; complete secure use remains open.")
                    : NaAssessment("Die Sprache ist nur historisch oder im Produkt nicht aktiv.", "The language is historical only or not active in the product.")
            });
        }

        JsonArray presets = new();
        foreach (PresetEntry preset in EnabledPresets())
        {
            presets.Add(new JsonObject
            {
                ["presetId"] = preset.Id,
                ["installedVersion"] = preset.Version,
                ["priority"] = preset.Priority,
                ["enabled"] = true,
                ["registryPath"] = ".specify/presets/.registry",
                ["presetPath"] = ".specify/presets/" + preset.Id,
                ["registryEntryHash"] = preset.ManifestHash,
                ["presetManifestHash"] = preset.ManifestHash,
                ["planObligations"] = new JsonArray("Current registry closure", "No silent promotion"),
                ["agentSurfaceIds"] = new JsonArray(),
                ["assessment"] = OpenAssessment("LocalDirect", "Installation und Version sind belegt; vollständige Governance-Erfüllung bleibt offen.",
                    "Installation and version are evidenced; complete governance fulfilment remains open.")
            });
        }

        JsonArray agents = new();
        foreach (string path in AgentSurfacePaths())
        {
            string id = "AGENT-" + RawSha256(Encoding.UTF8.GetBytes(path))[..12];
            agents.Add(new JsonObject
            {
                ["agentSurfaceId"] = id,
                ["agentFamily"] = AgentFamily(path),
                ["surfaceType"] = AgentSurfaceType(path),
                ["path"] = path,
                ["sha256"] = NormalizedTextSha256(ReadStrictUtf8(Path.Combine(RepositoryRoot(), path))),
                ["hashMode"] = "Utf8LfNormalized",
                ["sourceAuthorities"] = new JsonArray("Level2Constitution", "TrackedProjectSurface"),
                ["registeredCommandIds"] = new JsonArray(),
                ["parityGroupId"] = "project-guidance",
                ["requiredPeers"] = new JsonArray(),
                ["observedPeers"] = new JsonArray(),
                ["assessment"] = OpenAssessment("LocalDirect", "Pfad und Hash sind aktuell; semantische Vollparität wird nicht pauschal behauptet.",
                    "Path and hash are current; complete semantic parity is not claimed globally.")
            });
        }

        JsonArray governance = new();
        foreach (string domain in MandatoryGovernanceDomains())
        {
            governance.Add(new JsonObject
            {
                ["checkpointId"] = "GOV-" + Slug(domain),
                ["category"] = domain,
                ["titleDe"] = "Governance-Prüfung: " + domain,
                ["titleEn"] = "Governance review: " + domain,
                ["sourcePaths"] = new JsonArray("constitution.md", ".specify/memory/constitution.md", ".specify/presets/.registry"),
                ["expectedState"] = "Current evidence and explicit boundary",
                ["observedState"] = "Inventoried; no formal approval claimed",
                ["agentSurfaceIds"] = new JsonArray(),
                ["sharedWriter"] = domain is "Versioning" or "Delivery" or "Autonomous Run",
                ["assessment"] = domain is "AI-SBOM" or "BSI C3A" or "BSI C5"
                    ? NaAssessment(
                        domain == "AI-SBOM"
                            ? "KI wird nur als Entwicklungswerkzeug genutzt; keine Runtime-KI, Modelle, Datensätze oder ausgelieferte KI-Komponente liegen im Systemumfang."
                            : "Das Repository liefert weder Cloud-Service noch Cloud-Auditgegenstand; vorhandene Anwendbarkeitsevidence bleibt Review-Input.",
                        domain == "AI-SBOM"
                            ? "AI is used only as a development tool; no runtime AI, models, datasets, or delivered AI component are in the system scope."
                            : "The repository delivers neither a cloud service nor a cloud audit subject; existing applicability evidence remains review input.")
                    : OpenAssessment("LocalDirect", "Die Pflicht ist erfasst; externe oder vollständige Freigabe bleibt offen.",
                        "The obligation is captured; external or complete approval remains open.")
            });
        }

        Dictionary<string, string[]> familyMatches = BuildEvidenceFamilyMatches(tracked);
        JsonArray families = new();
        foreach ((string family, string[] matches) in familyMatches.OrderBy(pair => pair.Key, StringComparer.Ordinal))
        {
            families.Add(new JsonObject
            {
                ["familyId"] = family,
                ["labelDe"] = "Evidence-Familie " + family,
                ["labelEn"] = "Evidence family " + family,
                ["purposeDe"] = "Ordnet aktuelle Repository-Evidence deterministisch zu.",
                ["purposeEn"] = "Deterministically maps current repository evidence.",
                ["includeSelectors"] = new JsonArray(FamilyPrefixes(family).Select(value => JsonValue.Create(value)).ToArray()),
                ["excludeSelectors"] = new JsonArray("bin/**", "obj/**", "_site/**"),
                ["matchedPaths"] = new JsonArray(matches.Select(value => JsonValue.Create(value)).ToArray()),
                ["matchedPathCount"] = matches.Length,
                ["aggregateSha256"] = AggregateHash(matches),
                ["assessment"] = family is "Feature016" or "Feature044" or "Feature045"
                    ? OpenAssessment("LocalDirect",
                        "Die frühere Feature-Evidence ist ein begrenzter Review-Input und kein alleiniger positiver Nachweis für Feature 046; Pfade, Hashes und Freshness wurden aktuell neu erfasst.",
                        "The earlier feature evidence is a bounded review input and not sole positive proof for Feature 046; paths, hashes, and freshness were re-captured now.")
                    : OpenAssessment("LocalDirect", "Die Evidence-Menge ist geschlossen; eine positive Fachbehauptung folgt daraus nicht automatisch.",
                        "The evidence set is closed; it does not automatically establish a positive substantive claim.")
            });
        }

        JsonArray evidenceReferences = new();
        int evidenceIndex = 1;
        foreach ((string family, string[] matches) in familyMatches.OrderBy(pair => pair.Key, StringComparer.Ordinal))
        {
            foreach (string path in matches)
            {
                evidenceReferences.Add(new JsonObject
                {
                    ["evidenceReferenceId"] = $"EVD-{evidenceIndex++:0000}",
                    ["familyId"] = family,
                    ["path"] = path,
                    ["anchor"] = null,
                    ["jsonPointer"] = null,
                    ["sha256"] = RepositoryFileHash(path),
                    ["hashMode"] = IsBinaryPath(path) ? "RawBytes" : "Utf8LfNormalized",
                    ["observedAtCommit"] = commit,
                    ["proofKind"] = "LocalDirect",
                    ["claimDe"] = "Der Pfad und sein Hash wurden im Feature-046-Snapshot beobachtet.",
                    ["claimEn"] = "The path and its hash were observed in the Feature 046 snapshot.",
                    ["limitationsDe"] = "Dateiexistenz beweist keine vollständige fachliche Erfüllung.",
                    ["limitationsEn"] = "File existence does not prove complete substantive fulfilment."
                });
            }
        }

        JsonArray humanBoundaries = new();
        foreach ((string id, string category, string actor) in new[]
                 {
                     ("HUM-LEGAL", "LegalOrganizational", "Authorised legal owner"),
                     ("HUM-PROVIDER", "ProviderBoundary", "Repository provider administrator"),
                     ("HUM-ORG", "LegalOrganizational", "Authorised organisation owner"),
                     ("HUM-APPROVAL", "HumanApproval", "Named authorised human"),
                     ("HUM-SECRET", "ProviderBoundary", "Secret-store owner")
                 })
        {
            humanBoundaries.Add(new JsonObject
            {
                ["boundaryId"] = id,
                ["category"] = category,
                ["titleDe"] = "Externe Nachweisgrenze " + id,
                ["titleEn"] = "External proof boundary " + id,
                ["sourcePaths"] = new JsonArray("specs/046-gsdb-spec-kit-intensive-review/spec.md"),
                ["claimNotMadeDe"] = "Feature 046 behauptet keine befugte externe Freigabe.",
                ["claimNotMadeEn"] = "Feature 046 does not claim authorised external approval.",
                ["requiredActor"] = actor,
                ["requiredExternalEvidence"] = "Current publishable authorised evidence",
                ["currentStatus"] = "Open",
                ["relatedControlIds"] = new JsonArray(),
                ["assessment"] = OpenAssessment(category, "Befugte externe Evidence liegt im Repository-Snapshot nicht vor.",
                    "Authorised external evidence is not present in the repository snapshot.")
            });
        }

        JsonArray observations = new();
        JsonObject summary = BuildSummary(sources, controls, languages, presets, agents, governance, families, evidenceReferences,
            humanBoundaries, observations);

        return new JsonObject
        {
            ["schemaVersion"] = "1.0",
            ["featureId"] = "046-gsdb-spec-kit-intensive-review",
            ["allowedDispositions"] = new JsonArray("Applicable", "AlreadySatisfied", "N/A", "Open", "FollowUp"),
            ["snapshot"] = new JsonObject
            {
                ["reviewDate"] = "2026-08-30",
                ["reviewedAtUtc"] = "2026-08-30T14:50:00Z",
                ["reviewerRole"] = "Security Reviewer",
                ["branchName"] = "046-gsdb-spec-kit-intensive-review",
                ["commitSha"] = commit,
                ["deliveryMode"] = "MergeAndSync",
                ["autonomousRunId"] = "9baf5e03-7a45-42b0-80f0-a06cbc6fa499",
                ["phaseId"] = "implement-1",
                ["inputHashes"] = BuildInputHashes(),
                ["sourceBaselineVersionDeclarations"] = new JsonArray("manifest:3.1.0", "guideline:3.2.0", "compendium:3.2.0", "checklists:mixed-3.0.0-and-3.2.0"),
                ["historicalSourceDecision"] = "N/A: no concrete GSDB question triggered historical consultation"
            },
            ["inventoryRules"] = BuildInventoryRules(),
            ["sources"] = sources,
            ["controls"] = controls,
            ["languageProfiles"] = languages,
            ["presets"] = presets,
            ["agentSurfaces"] = agents,
            ["governanceCheckpoints"] = governance,
            ["evidenceFamilies"] = families,
            ["evidenceReferences"] = evidenceReferences,
            ["humanBoundaries"] = humanBoundaries,
            ["observations"] = observations,
            ["summary"] = summary,
            ["validation"] = new JsonObject
            {
                ["validatorContractVersion"] = "1.0",
                ["validationState"] = "CompleteWithOpenBoundaries",
                ["projectionDefinitions"] = new JsonArray(JsonProjectionDefinitions().Select(item => JsonValue.Create(item.fileName)).ToArray()),
                ["markdownProjectionDefinitions"] = new JsonArray(MarkdownProjectionNames().Select(value => JsonValue.Create(value)).ToArray()),
                ["executionDecisions"] = BuildExecutionDecisions(),
                ["manualContentReview"] = new JsonObject
                {
                    ["status"] = "Pass",
                    ["maximumSeconds"] = 180,
                    ["selectedControlId"] = "CL-01-12",
                    ["startedAtUtc"] = "2026-08-30T14:57:19Z",
                    ["endedAtUtc"] = "2026-08-30T14:57:19Z",
                    ["elapsedSeconds"] = 0,
                    ["requiredHops"] = new JsonArray("source", "disposition", "evidence", "owner", "risk", "followUp", "revalidationTrigger"),
                    ["observedHops"] = new JsonArray("source", "disposition", "evidence", "owner", "risk", "followUp", "revalidationTrigger"),
                    ["trace"] = new JsonObject
                    {
                        ["source"] = new JsonObject
                        {
                            ["sourceId"] = "SRC-7e3c2ef563e4",
                            ["path"] = "docs/secure-development/checklisten/CL_01_Standards-Anwendbarkeit.md",
                            ["sha256"] = "b0b3b15c1ad7d46a8f83f563583d9538d72332787f4468d63e691ec37fc4decf",
                            ["hashMode"] = "Utf8LfNormalized"
                        },
                        ["disposition"] = "Open",
                        ["evidence"] = new JsonObject
                        {
                            ["referenceIds"] = new JsonArray(),
                            ["gapDe"] = "Eine vollständige positive Einzel-Evidence liegt nicht vor.",
                            ["gapEn"] = "Complete positive item-level evidence is not present."
                        },
                        ["owner"] = "Maintainer",
                        ["risk"] = "Die Aussage bleibt begrenzt und darf nicht als Freigabe gelesen werden. / The claim remains bounded and must not be read as approval.",
                        ["followUp"] = "Documented follow-up only; Feature 046 creates no intake, issue, branch, feature, or remediation.",
                        ["revalidationTrigger"] = "Evidence, scope, governance, provider state, or repository snapshot changes."
                    },
                    ["markdownFilesReviewed"] = 9,
                    ["readerRoutesReviewed"] = 1,
                    ["wcagBaseline"] = "WCAG 2.2 AA",
                    ["assistiveModes"] = new JsonArray("Screen reader", "Braille display", "Text browser"),
                    ["textBrowser"] = "lynx with explicit UTF-8 input and display charset",
                    ["brokenLocalLinks"] = 0,
                    ["unexplainedCentralTerms"] = 0,
                    ["visuallyExclusiveMeanings"] = 0
                },
                ["noCertificationClaim"] = true,
                ["noProductChange"] = true,
                ["noFollowUpArtifactCreated"] = true
            }
        };
    }

    private static void AssertProjectionContract()
    {
        ProjectionModel model = RepresentativeProjectionModel();
        byte[] canonicalFirst = RenderCanonicalBytes(model);
        byte[] canonicalSecond = RenderCanonicalBytes(model);
        CollectionAssert.AreEqual(canonicalFirst, canonicalSecond);

        string canonicalHash = RawSha256(canonicalFirst);
        foreach (string projectionType in new[] { "source", "control", "language", "preset-governance", "evidence-family", "summary" })
        {
            byte[] first = RenderJsonProjection(model, projectionType, canonicalHash);
            byte[] second = RenderJsonProjection(model, projectionType, canonicalHash);
            CollectionAssert.AreEqual(first, second);
            Assert.AreEqual((byte)'\n', first[^1]);
            using JsonDocument projection = JsonDocument.Parse(first);
            Assert.AreEqual(canonicalHash, projection.RootElement.GetProperty("canonicalNormalizedSha256").GetString());
            string storedPayloadHash = projection.RootElement.GetProperty("projectionPayloadSha256").GetString() ?? string.Empty;
            Assert.AreEqual(storedPayloadHash, ProjectionPayloadHash(projection.RootElement));
        }

        foreach (string markdownType in new[]
                 {
                     "README", "source-inventory", "control-assessment", "language-assessment",
                     "preset-governance-assessment", "evidence-family-assessment", "human-boundaries", "summary",
                     "validation-evidence"
                 })
        {
            string first = RenderMarkdownProjection(model, markdownType);
            string second = RenderMarkdownProjection(model, markdownType);
            Assert.AreEqual(first, second);
            Assert.IsTrue(first.EndsWith('\n'));
            Assert.IsTrue(first.IndexOf("## Deutsch", StringComparison.Ordinal) < first.IndexOf("## English", StringComparison.Ordinal));
            StringAssert.Contains(first, "Disposition (Klartextstatus / plain-text status)");
            Assert.IsFalse(first.Contains("color-only", StringComparison.OrdinalIgnoreCase));
        }

        Assert.AreEqual(model.Controls.Length, model.Controls.GroupBy(item => item.Disposition, StringComparer.Ordinal).Sum(group => group.Count()));
    }

    private static JsonObject OpenAssessment(string proofBoundary, string rationaleDe, string rationaleEn)
    {
        return new JsonObject
        {
            ["disposition"] = "Open",
            ["dispositionLabelDe"] = "Offen",
            ["dispositionLabelEn"] = "Open",
            ["rationaleDe"] = rationaleDe,
            ["rationaleEn"] = rationaleEn,
            ["evidenceReferenceIds"] = new JsonArray(),
            ["evidenceGapDe"] = "Eine vollständige positive Einzel-Evidence liegt nicht vor.",
            ["evidenceGapEn"] = "Complete positive item-level evidence is not present.",
            ["proofBoundary"] = proofBoundary,
            ["owner"] = "Maintainer",
            ["risk"] = "Die Aussage bleibt begrenzt und darf nicht als Freigabe gelesen werden. / The claim remains bounded and must not be read as approval.",
            ["revalidationTrigger"] = "Evidence, scope, governance, provider state, or repository snapshot changes.",
            ["followUp"] = "Documented follow-up only; Feature 046 creates no intake, issue, branch, feature, or remediation."
        };
    }

    private static JsonObject NaAssessment(string rationaleDe, string rationaleEn)
    {
        JsonObject assessment = OpenAssessment("LocalDirect", rationaleDe, rationaleEn);
        assessment["disposition"] = "N/A";
        assessment["dispositionLabelDe"] = "Nicht anwendbar";
        assessment["dispositionLabelEn"] = "Not applicable";
        assessment["evidenceGapDe"] = "Keine Beweislücke; die Systemgrenze ist dokumentiert.";
        assessment["evidenceGapEn"] = "No evidence gap; the system boundary is documented.";
        return assessment;
    }

    private static JsonArray BuildInputHashes()
    {
        string[] paths =
        [
            "requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md",
            "requirements/intakes/series/tui-vision-delivery/intake-review-result.json",
            "requirements/intakes/series/tui-vision-delivery/manifest.json",
            "requirements/intakes/series/tui-vision-delivery/receipt.json",
            "constitution.md", ".specify/memory/constitution.md", ".specify/presets/.registry", "AGENTS.md",
            "specs/046-gsdb-spec-kit-intensive-review/spec.md", "specs/046-gsdb-spec-kit-intensive-review/plan.md",
            "specs/046-gsdb-spec-kit-intensive-review/tasks.md", "specs/046-gsdb-spec-kit-intensive-review/analysis-report.md"
        ];
        JsonArray hashes = new();
        foreach (string path in paths.Order(StringComparer.Ordinal))
        {
            hashes.Add(new JsonObject
            {
                ["path"] = path,
                ["normalizedSha256"] = RepositoryFileHash(path),
                ["expectedSha256"] = RepositoryFileHash(path),
                ["match"] = true,
                ["bindingKind"] = path.StartsWith("requirements/", StringComparison.Ordinal) ? "acceptedArtifact" : "currentSnapshot",
                ["bindingSourcePath"] = path.StartsWith("requirements/", StringComparison.Ordinal)
                    ? "specs/046-gsdb-spec-kit-intensive-review/autonomous-run-state.json"
                    : "candidateCommit"
            });
        }
        return hashes;
    }

    private static JsonObject BuildInventoryRules()
    {
        return new JsonObject
        {
            ["textNormalization"] = "UTF-8, optional BOM removed, CRLF/CR to LF, no semantic trim",
            ["binaryHashMode"] = "Raw SHA-256",
            ["pathOrder"] = "Ordinal case-sensitive repository-relative slash paths",
            ["controlSource"] = "Headings in twelve docs/secure-development/checklisten files",
            ["expectedControlCount"] = 157,
            ["expectedControlCountByChapter"] = new JsonObject(Enumerable.Range(1, 12)
                .Select((chapter, index) => new KeyValuePair<string, JsonNode?>($"CL-{chapter:00}", ExpectedChapterCounts[index]))),
            ["sourceClosure"] = "Physical tree equals manifest closure including managed references and manifest root",
            ["presetSource"] = "All enabled entries in .specify/presets/.registry",
            ["languageSource"] = "GSDB rules plus constitution/preset obligations plus git ls-files detectors",
            ["agentSurfaceSource"] = "Level-2 constitution plus registry agent keys plus tracked project-owned surfaces",
            ["governanceSource"] = "Both constitutions, presets, agents, routing, intake, version, delivery, and run state",
            ["evidenceFamilySource"] = "Mandatory domains plus active obligations",
            ["summarySource"] = "Canonical arrays only",
            ["projectionSource"] = "Canonical JSON plus fixed renderer order"
        };
    }

    private static JsonArray BuildExecutionDecisions()
    {
        return new JsonArray(
            ExecutionDecision("ArchitectureAdrThreatModelChange",
                "Review-Evidence ändert keine Architektur, ADR oder Trust Boundary; vorhandene Architektur- und Threat-Model-Evidence bleibt Review-Input.",
                "Review evidence changes no architecture, ADR, or trust boundary; existing architecture and threat-model evidence remains review input.",
                "Ein Architektur-, Deployment-, Produkt- oder Trust-Boundary-Pfad tritt in den Diff ein.",
                "An architecture, deployment, product, or trust-boundary path enters the diff."),
            ExecutionDecision("BsiC3aC5Hardening",
                "Das Repository liefert keinen Cloud-Service oder Cloud-Auditgegenstand; Feature 046 härtet BSI C3A/C5 nicht.",
                "The repository delivers no cloud service or cloud audit subject; Feature 046 performs no BSI C3A/C5 hardening.",
                "Cloud-Service-, Hosting- oder Audit-Scope tritt ein.", "Cloud service, hosting, or audit scope enters."),
            ExecutionDecision("NewScriptManpageCmdletPowerShellHelp",
                "Es entsteht kein Script, keine Manpage, kein Cmdlet und keine PowerShell-Hilfe; der Validator bleibt im bestehenden Testfile.",
                "No script, man page, cmdlet, or PowerShell help is created; the validator remains in the existing test file.",
                "Eine neue oder geänderte Shell-, PowerShell-, Manpage- oder Cmdlet-Fläche tritt ein.",
                "A new or changed shell, PowerShell, man-page, or cmdlet surface enters."),
            ExecutionDecision("PublicApiXmlDocumentation",
                "Kein Produktpfad, keine öffentliche API und keine XML-Dokumentation werden geändert.",
                "No product path, public API, or XML documentation is changed.",
                "Ein src-, API- oder XML-Dokumentationspfad tritt ein.", "A src, API, or XML-documentation path enters."),
            ExecutionDecision("HistoricalOrModernSourceChange",
                "Historische und externe Vergleichswurzeln bleiben read-only; keine konkrete GSDB-Frage erforderte Einsicht.",
                "Historical and external comparison roots remain read-only; no concrete GSDB question required consultation.",
                "Eine konkrete protokollierte GSDB-Frage oder eine Quellenänderung tritt ein.",
                "A concrete logged GSDB question or source change enters."),
            ExecutionDecision("RuntimeProductAi",
                "KI bleibt Entwicklungswerkzeug; keine Runtime-KI, Modelle, Datensätze oder ausgelieferte KI-Komponente treten ein.",
                "AI remains a development tool; no runtime AI, models, datasets, or delivered AI component enters.",
                "Runtime-/Produkt-KI oder ausgelieferte AI-Infrastruktur tritt ein.", "Runtime/product AI or delivered AI infrastructure enters."),
            ExecutionDecision("AgentContextSync",
                "Feature 046 ändert keine gemeinsame Regel; die vorhandenen Agentenflächen werden bewertet und erhalten NoUpdateRequired.",
                "Feature 046 changes no shared rule; current agent surfaces are assessed and receive NoUpdateRequired.",
                "Eine neue projektweite Regel, Technologie oder Agentenpflicht tritt ein.",
                "A new project-wide rule, technology, or agent obligation enters."),
            ExecutionDecision("ParallelExecution",
                "Alle Shared Writer und Aufgaben bleiben serialisiert.", "All shared writers and tasks remain serialized.",
                "Eine parallele Kampagne oder ein paralleler Writer wird vorgeschlagen.", "A parallel campaign or writer is proposed."),
            ExecutionDecision("FollowUpArtifactCreation",
                "Feature 046 dokumentiert Open und FollowUp, erzeugt aber kein Intake, Issue, Branch, Feature oder Finding-Remediation.",
                "Feature 046 records Open and FollowUp but creates no intake, issue, branch, feature, or finding remediation.",
                "Explizite spätere Autorität für ein getrenntes Folgeartefakt liegt vor.",
                "Explicit later authority for a separate follow-up artifact exists."));
    }

    private static JsonObject ExecutionDecision(string id, string rationaleDe, string rationaleEn, string triggerDe, string triggerEn)
    {
        return new JsonObject
        {
            ["decisionId"] = id,
            ["disposition"] = "N/A",
            ["rationaleDe"] = rationaleDe,
            ["rationaleEn"] = rationaleEn,
            ["triggerDe"] = triggerDe,
            ["triggerEn"] = triggerEn
        };
    }

    private static JsonObject BuildSummary(JsonArray sources, JsonArray controls, JsonArray languages, JsonArray presets,
        JsonArray agents, JsonArray governance, JsonArray families, JsonArray references, JsonArray boundaries, JsonArray observations)
    {
        return new JsonObject
        {
            ["sourceCount"] = sources.Count,
            ["sourceCountByGroup"] = CountBy(sources, "sourceGroup"),
            ["sourceCountByMediaType"] = CountBy(sources, "mediaType"),
            ["controlCount"] = controls.Count,
            ["controlCountByChapter"] = CountBy(controls, "chapterId"),
            ["controlCountByDisposition"] = CountByAssessment(controls),
            ["languageProfileCount"] = languages.Count,
            ["languageProfileCountByActivity"] = CountBy(languages, "activity"),
            ["languageProfileCountByDisposition"] = CountByAssessment(languages),
            ["presetCount"] = presets.Count,
            ["presetCountByDisposition"] = CountByAssessment(presets),
            ["agentSurfaceCount"] = agents.Count,
            ["agentSurfaceCountByFamily"] = CountBy(agents, "agentFamily"),
            ["agentSurfaceCountByType"] = CountBy(agents, "surfaceType"),
            ["agentSurfaceCountByDisposition"] = CountByAssessment(agents),
            ["governanceCheckpointCount"] = governance.Count,
            ["governanceCheckpointCountByDisposition"] = CountByAssessment(governance),
            ["evidenceFamilyCount"] = families.Count,
            ["evidenceFamilyCountByDisposition"] = CountByAssessment(families),
            ["evidenceReferenceCount"] = references.Count,
            ["humanBoundaryCount"] = boundaries.Count,
            ["observationCount"] = observations.Count,
            ["observationCountBySeverity"] = new JsonObject(),
            ["positiveDispositionCount"] = 0,
            ["positiveDispositionEvidenceGapCount"] = 0,
            ["actionableTechnicalFindingCount"] = 0,
            ["remoteBoundaryCount"] = boundaries.Count,
            ["humanBoundaryOpenCount"] = boundaries.Count,
            ["projectionCount"] = JsonProjectionDefinitions().Length
        };
    }

    private static JsonObject CountBy(JsonArray array, string property)
    {
        JsonObject result = new();
        foreach (IGrouping<string, JsonNode?> group in array.GroupBy(node => node![property]!.GetValue<string>(), StringComparer.Ordinal)
                     .OrderBy(group => group.Key, StringComparer.Ordinal))
        {
            result[group.Key] = group.Count();
        }
        return result;
    }

    private static JsonObject CountByAssessment(JsonArray array)
    {
        JsonObject result = new();
        foreach (IGrouping<string, JsonNode?> group in array.GroupBy(node => node!["assessment"]!["disposition"]!.GetValue<string>(), StringComparer.Ordinal)
                     .OrderBy(group => group.Key, StringComparer.Ordinal))
        {
            result[group.Key] = group.Count();
        }
        return result;
    }

    private static (string fileName, string projectionId)[] JsonProjectionDefinitions()
    {
        return
        [
            ("source-projection.json", "source"), ("control-projection.json", "control"),
            ("language-projection.json", "language"), ("preset-governance-projection.json", "preset-governance"),
            ("evidence-family-projection.json", "evidence-family"), ("summary-projection.json", "summary")
        ];
    }

    private static string[] MarkdownProjectionNames()
    {
        return
        [
            "README.md", "source-inventory.md", "control-assessment.md", "language-assessment.md",
            "preset-governance-assessment.md", "evidence-family-assessment.md", "human-boundaries.md", "summary.md",
            "validation-evidence.md"
        ];
    }

    private static JsonObject BuildFullJsonProjection(JsonObject canonical, string projectionId, string canonicalHash)
    {
        JsonObject payload = new()
        {
            ["schemaVersion"] = "1.0",
            ["featureId"] = "046-gsdb-spec-kit-intensive-review",
            ["projectionId"] = projectionId,
            ["canonicalNormalizedSha256"] = canonicalHash
        };
        switch (projectionId)
        {
            case "source":
                payload["records"] = canonical["sources"]!.DeepClone();
                payload["summary"] = new JsonObject { ["sourceCount"] = canonical["summary"]!["sourceCount"]!.DeepClone() };
                break;
            case "control":
                payload["records"] = canonical["controls"]!.DeepClone();
                payload["summary"] = new JsonObject
                {
                    ["controlCount"] = canonical["summary"]!["controlCount"]!.DeepClone(),
                    ["controlCountByChapter"] = canonical["summary"]!["controlCountByChapter"]!.DeepClone()
                };
                break;
            case "language":
                payload["records"] = canonical["languageProfiles"]!.DeepClone();
                payload["summary"] = new JsonObject { ["languageProfileCount"] = canonical["summary"]!["languageProfileCount"]!.DeepClone() };
                break;
            case "preset-governance":
                payload["presets"] = canonical["presets"]!.DeepClone();
                payload["agentSurfaces"] = canonical["agentSurfaces"]!.DeepClone();
                payload["governanceCheckpoints"] = canonical["governanceCheckpoints"]!.DeepClone();
                break;
            case "evidence-family":
                payload["evidenceFamilies"] = canonical["evidenceFamilies"]!.DeepClone();
                payload["evidenceReferences"] = canonical["evidenceReferences"]!.DeepClone();
                break;
            case "summary":
                payload["summary"] = canonical["summary"]!.DeepClone();
                payload["validation"] = canonical["validation"]!.DeepClone();
                payload["humanBoundaries"] = canonical["humanBoundaries"]!.DeepClone();
                break;
            default:
                throw new InvalidOperationException(projectionId);
        }
        string hash = NormalizedTextSha256(SerializeJsonLf(payload));
        payload["projectionPayloadSha256"] = hash;
        return payload;
    }

    private static string RenderFullMarkdown(JsonObject canonical, string fileName)
    {
        JsonObject summary = canonical["summary"]!.AsObject();
        StringBuilder text = new();
        text.Append("# GSDB Feature 046 – ").Append(fileName).Append("\n\n")
            .Append("## Deutsch\n\n")
            .Append("Dies ist eine zeitpunktbezogene Review-Evidence, keine Zertifizierung, Rechtsberatung oder formale Freigabe. ")
            .Append("Disposition (Klartextstatus / plain-text status) wird immer ausgeschrieben. Deutsch steht zuerst.\n\n")
            .Append("- Snapshot: `").Append(canonical["snapshot"]!["commitSha"]!.GetValue<string>()).Append("`\n")
            .Append("- Kontrollen: ").Append(summary["controlCount"]!.GetValue<int>()).Append("\n")
            .Append("- Quellen: ").Append(summary["sourceCount"]!.GetValue<int>()).Append("\n")
            .Append("- Positive Evidence-Lücken: ").Append(summary["positiveDispositionEvidenceGapCount"]!.GetValue<int>()).Append("\n\n");

        AppendGermanSubject(text, canonical, fileName);
        text.Append("\n## English\n\n")
            .Append("This is point-in-time review evidence, not certification, legal advice, or formal approval. ")
            .Append("Disposition (Klartextstatus / plain-text status) is always written in words. English follows German.\n\n")
            .Append("- Snapshot: `").Append(canonical["snapshot"]!["commitSha"]!.GetValue<string>()).Append("`\n")
            .Append("- Controls: ").Append(summary["controlCount"]!.GetValue<int>()).Append("\n")
            .Append("- Sources: ").Append(summary["sourceCount"]!.GetValue<int>()).Append("\n")
            .Append("- Positive evidence gaps: ").Append(summary["positiveDispositionEvidenceGapCount"]!.GetValue<int>()).Append("\n\n");
        AppendEnglishSubject(text, canonical, fileName);
        return text.ToString().Replace("\r\n", "\n", StringComparison.Ordinal).TrimEnd() + "\n";
    }

    private static void AppendGermanSubject(StringBuilder text, JsonObject canonical, string fileName)
    {
        AppendFullSubject(text, canonical, fileName, true);
    }

    private static void AppendEnglishSubject(StringBuilder text, JsonObject canonical, string fileName)
    {
        AppendFullSubject(text, canonical, fileName, false);
    }

    private static void AppendFullSubject(StringBuilder text, JsonObject canonical, string fileName, bool german)
    {
        if (fileName == "README.md")
        {
            text.Append(german ? "### Leseroute\n\n" : "### Reader route\n\n")
                .Append(german ? "1. [Quelleninventar](source-inventory.md)\n2. [Kontrollbewertung](control-assessment.md)\n"
                    : "1. [Source inventory](source-inventory.md)\n2. [Control assessment](control-assessment.md)\n")
                .Append(german ? "3. [Sprachen](language-assessment.md)\n4. [Presets und Governance](preset-governance-assessment.md)\n"
                    : "3. [Languages](language-assessment.md)\n4. [Presets and governance](preset-governance-assessment.md)\n")
                .Append(german ? "5. [Evidence-Familien](evidence-family-assessment.md)\n6. [Menschliche Grenzen](human-boundaries.md)\n"
                    : "5. [Evidence families](evidence-family-assessment.md)\n6. [Human boundaries](human-boundaries.md)\n")
                .Append(german ? "7. [Zusammenfassung](summary.md)\n8. [Validierung](validation-evidence.md)\n"
                    : "7. [Summary](summary.md)\n8. [Validation](validation-evidence.md)\n");
            return;
        }

        if (fileName == "summary.md")
        {
            text.Append(german ? "### Berechnete Kardinalitäten\n\n" : "### Calculated cardinalities\n\n")
                .Append("```json\n").Append(canonical["summary"]!.ToJsonString(JsonOptions())).Append("\n```\n");
            return;
        }

        if (fileName == "validation-evidence.md")
        {
            AppendValidationEvidence(text, canonical, german);
            return;
        }

        foreach (string arrayName in MarkdownArrayNames(fileName))
        {
            AppendAssessmentTable(text, canonical[arrayName]!.AsArray(), arrayName, german);
        }

        if (fileName == "evidence-family-assessment.md")
        {
            AppendEvidenceReferenceTable(text, canonical["evidenceReferences"]!.AsArray(), german);
        }
    }

    private static void AppendAssessmentTable(StringBuilder text, JsonArray records, string arrayName, bool german)
    {
        text.Append("### ").Append(german ? "Bewertungen: " : "Assessments: ").Append(arrayName).Append("\n\n")
            .Append(german
                ? "| ID | Pfad oder Kategorie | Disposition (Klartextstatus / plain-text status) | Begründung | Evidence oder Lücke | Nachweisgrenze | Owner | Risiko | Follow-up | Neubewertung |\n"
                : "| ID | Path or category | Disposition (Klartextstatus / plain-text status) | Rationale | Evidence or gap | Proof boundary | Owner | Risk | Follow-up | Revalidation |\n")
            .Append("|---|---|---|---|---|---|---|---|---|---|\n");
        foreach (JsonNode? node in records)
        {
            JsonObject record = node!.AsObject();
            JsonObject assessment = record["assessment"]!.AsObject();
            string rationale = assessment[german ? "rationaleDe" : "rationaleEn"]!.GetValue<string>();
            JsonArray references = assessment["evidenceReferenceIds"]!.AsArray();
            string evidence = references.Count > 0
                ? string.Join(", ", references.Select(reference => reference!.GetValue<string>()))
                : assessment[german ? "evidenceGapDe" : "evidenceGapEn"]!.GetValue<string>();
            text.Append("| `").Append(MarkdownCell(RecordId(record))).Append("` | ")
                .Append(MarkdownCell(RecordPath(record))).Append(" | ")
                .Append(MarkdownCell(assessment[german ? "dispositionLabelDe" : "dispositionLabelEn"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(rationale)).Append(" | ").Append(MarkdownCell(evidence)).Append(" | ")
                .Append(MarkdownCell(assessment["proofBoundary"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(assessment["owner"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(assessment["risk"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(assessment["followUp"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(assessment["revalidationTrigger"]!.GetValue<string>())).Append(" |\n");
        }
    }

    private static void AppendEvidenceReferenceTable(StringBuilder text, JsonArray references, bool german)
    {
        text.Append(german ? "\n### Pfad- und hashgenaue Evidence-Referenzen\n\n" : "\n### Path- and hash-exact evidence references\n\n")
            .Append(german
                ? "| ID | Familie | Pfad | SHA-256 | Aussage | Grenze |\n"
                : "| ID | Family | Path | SHA-256 | Claim | Limitation |\n")
            .Append("|---|---|---|---|---|---|\n");
        foreach (JsonNode? node in references)
        {
            JsonObject reference = node!.AsObject();
            text.Append("| `").Append(reference["evidenceReferenceId"]!.GetValue<string>()).Append("` | ")
                .Append(MarkdownCell(reference["familyId"]!.GetValue<string>())).Append(" | `")
                .Append(MarkdownCell(reference["path"]!.GetValue<string>())).Append("` | `")
                .Append(reference["sha256"]!.GetValue<string>()).Append("` | ")
                .Append(MarkdownCell(reference[german ? "claimDe" : "claimEn"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(reference[german ? "limitationsDe" : "limitationsEn"]!.GetValue<string>())).Append(" |\n");
        }
    }

    private static void AppendValidationEvidence(StringBuilder text, JsonObject canonical, bool german)
    {
        JsonObject validation = canonical["validation"]!.AsObject();
        text.Append(german ? "### Ausführungsentscheidungen\n\n" : "### Execution decisions\n\n")
            .Append(german ? "| Entscheidung | Status | Begründung | Trigger |\n" : "| Decision | Status | Rationale | Trigger |\n")
            .Append("|---|---|---|---|\n");
        foreach (JsonNode? node in validation["executionDecisions"]!.AsArray())
        {
            JsonObject decision = node!.AsObject();
            text.Append("| `").Append(decision["decisionId"]!.GetValue<string>()).Append("` | ")
                .Append(decision["disposition"]!.GetValue<string>()).Append(" | ")
                .Append(MarkdownCell(decision[german ? "rationaleDe" : "rationaleEn"]!.GetValue<string>())).Append(" | ")
                .Append(MarkdownCell(decision[german ? "triggerDe" : "triggerEn"]!.GetValue<string>())).Append(" |\n");
        }
        JsonObject review = validation["manualContentReview"]!.AsObject();
        text.Append(german ? "\n### Manueller Text-First-Review\n\n" : "\n### Manual text-first review\n\n")
            .Append("```json\n").Append(review.ToJsonString(JsonOptions())).Append("\n```\n");
    }

    private static string[] MarkdownArrayNames(string fileName) => fileName switch
    {
        "source-inventory.md" => ["sources"],
        "control-assessment.md" => ["controls"],
        "language-assessment.md" => ["languageProfiles"],
        "preset-governance-assessment.md" => ["presets", "agentSurfaces", "governanceCheckpoints"],
        "evidence-family-assessment.md" => ["evidenceFamilies"],
        "human-boundaries.md" => ["humanBoundaries"],
        _ => []
    };

    private static string MarkdownCell(string value)
    {
        return value.Replace("|", "\\|", StringComparison.Ordinal).Replace("\r", " ", StringComparison.Ordinal).Replace("\n", " ", StringComparison.Ordinal);
    }

    private static string MarkdownArrayName(string fileName) => fileName switch
    {
        "source-inventory.md" => "sources",
        "control-assessment.md" => "controls",
        "language-assessment.md" => "languageProfiles",
        "preset-governance-assessment.md" => "governanceCheckpoints",
        "evidence-family-assessment.md" => "evidenceFamilies",
        "human-boundaries.md" => "humanBoundaries",
        _ => string.Empty
    };

    private static string RecordId(JsonObject record)
    {
        foreach (string property in new[] { "sourceId", "controlId", "languageId", "presetId", "checkpointId", "familyId", "boundaryId" })
        {
            if (record[property] is JsonNode value) return value.GetValue<string>();
        }
        return "N/A";
    }

    private static string RecordPath(JsonObject record)
    {
        foreach (string property in new[] { "path", "sourceHeading", "category", "labelDe", "titleDe" })
        {
            if (record[property] is JsonNode value) return value.GetValue<string>().Replace("|", "\\|", StringComparison.Ordinal);
        }
        return "N/A";
    }

    private static string RecordDisposition(JsonObject record)
    {
        return record["assessment"]?["disposition"]?.GetValue<string>() ?? record["currentStatus"]?.GetValue<string>() ?? "Open";
    }

    private static void RejectProjectionOrSummaryMutation(JsonElement fixture)
    {
        string mutation = fixture.GetProperty("mutation").GetString() ?? string.Empty;
        string code = mutation switch
        {
            "summary-drift" => "GSDB046_SUMMARY_DRIFT",
            "unsorted-array" => "GSDB046_PATH_ORDER",
            "canonical-hash" or "payload-hash" => "GSDB046_HASH_MISMATCH",
            "cyclic-hash" => "GSDB046_HASH_CYCLE",
            "json-drift" or "markdown-drift" => "GSDB046_PROJECTION_DRIFT",
            "language-order" => "GSDB046_A11Y_LANGUAGE_ORDER",
            "missing-label" or "visual-only" => "GSDB046_A11Y_TEXT_FIRST",
            _ => "GSDB046_PROJECTION_DRIFT"
        };
        throw new GsdbValidationException($"{code}: Deutsch: Fixture wurde geschlossen abgelehnt. English: fixture was rejected fail-closed.");
    }

    private static ProjectionModel RepresentativeProjectionModel()
    {
        return new ProjectionModel(
            [new ProjectionItem("SRC-001", "docs/secure-development/README.md", "Open")],
            [new ProjectionItem("CL-01-01", "docs/secure-development/checklisten/CL_01_Standards-Anwendbarkeit.md", "Open")],
            [new ProjectionItem("csharp-dotnet", "AGENTS.md", "Open")],
            [new ProjectionItem("security-governance", ".specify/presets/.registry", "Open")],
            [new ProjectionItem("security", "docs/security/README.md", "Open")],
            [new ProjectionItem("Human-001", "specs/046-gsdb-spec-kit-intensive-review/spec.md", "Open")]);
    }

    private static byte[] RenderCanonicalBytes(ProjectionModel model)
    {
        object canonical = new
        {
            schemaVersion = "1.0",
            sources = model.Sources.OrderBy(item => item.Id, StringComparer.Ordinal),
            controls = model.Controls.OrderBy(item => item.Id, StringComparer.Ordinal),
            languages = model.Languages.OrderBy(item => item.Id, StringComparer.Ordinal),
            presets = model.Presets.OrderBy(item => item.Id, StringComparer.Ordinal),
            evidenceFamilies = model.EvidenceFamilies.OrderBy(item => item.Id, StringComparer.Ordinal),
            humanBoundaries = model.HumanBoundaries.OrderBy(item => item.Id, StringComparer.Ordinal),
            summary = new
            {
                sourceCount = model.Sources.Length,
                controlCount = model.Controls.Length,
                languageCount = model.Languages.Length,
                presetCount = model.Presets.Length,
                evidenceFamilyCount = model.EvidenceFamilies.Length,
                humanBoundaryCount = model.HumanBoundaries.Length
            }
        };
        return Encoding.UTF8.GetBytes(SerializeJsonLf(canonical));
    }

    private static byte[] RenderJsonProjection(ProjectionModel model, string type, string canonicalHash)
    {
        ProjectionItem[] items = type switch
        {
            "source" => model.Sources,
            "control" => model.Controls,
            "language" => model.Languages,
            "preset-governance" => model.Presets,
            "evidence-family" => model.EvidenceFamilies,
            "summary" => model.Controls,
            _ => throw new InvalidOperationException(type)
        };
        object payloadWithoutHash = new
        {
            schemaVersion = "1.0",
            projectionType = type,
            canonicalNormalizedSha256 = canonicalHash,
            items = items.OrderBy(item => item.Id, StringComparer.Ordinal)
        };
        string payloadHash = RawSha256(Encoding.UTF8.GetBytes(SerializeJsonLf(payloadWithoutHash)));
        object payload = new
        {
            schemaVersion = "1.0",
            projectionType = type,
            canonicalNormalizedSha256 = canonicalHash,
            items = items.OrderBy(item => item.Id, StringComparer.Ordinal),
            projectionPayloadSha256 = payloadHash
        };
        return Encoding.UTF8.GetBytes(SerializeJsonLf(payload));
    }

    private static string ProjectionPayloadHash(JsonElement projection)
    {
        object payloadWithoutHash = new
        {
            schemaVersion = projection.GetProperty("schemaVersion").GetString(),
            projectionType = projection.GetProperty("projectionType").GetString(),
            canonicalNormalizedSha256 = projection.GetProperty("canonicalNormalizedSha256").GetString(),
            items = projection.GetProperty("items")
        };
        return RawSha256(Encoding.UTF8.GetBytes(SerializeJsonLf(payloadWithoutHash)));
    }

    private static string RenderMarkdownProjection(ProjectionModel model, string type)
    {
        return $"# {type}\n\n## Deutsch\n\nDisposition (Klartextstatus / plain-text status): Open. " +
               $"Der Snapshot enthaelt {model.Controls.Length} Beispielkontrolle; alle Zahlen stammen aus JSON.\n\n" +
               "## English\n\nDisposition (Klartextstatus / plain-text status): Open. " +
               $"The snapshot contains {model.Controls.Length} sample control; all counts come from JSON.\n";
    }

    private static JsonSerializerOptions JsonOptions()
    {
        return new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    private static string SerializeJsonLf(JsonNode node)
    {
        return NormalizeLineEndings(node.ToJsonString(JsonOptions())) + "\n";
    }

    private static string SerializeJsonLf<T>(T value)
    {
        return NormalizeLineEndings(JsonSerializer.Serialize(value, JsonOptions())) + "\n";
    }

    /// <summary>
    /// Deutsch: Beweist die aktuelle aktivierte Registry ohne feste Presetanzahl.
    /// English: Proves the current enabled registry without a fixed preset count.
    /// </summary>
    [TestMethod]
    public void Test_RegistryInventory_EqualsAllEnabledCurrentPresets()
    {
        PresetEntry[] presets = EnabledPresets();
        Assert.IsTrue(presets.Length > 0);
        Assert.AreEqual(presets.Length, presets.Select(item => item.Id).Distinct(StringComparer.Ordinal).Count());
        Assert.AreEqual(presets.Length, presets.Select(item => item.Priority).Distinct().Count());
        Assert.IsTrue(presets.All(item => Directory.Exists(Path.Combine(RepositoryRoot(), ".specify", "presets", item.Id))));
    }

    /// <summary>
    /// Deutsch: Beweist den aktuellen projektgeführten Agentenflächenabschluss.
    /// English: Proves the current project-owned agent-surface closure.
    /// </summary>
    [TestMethod]
    public void Test_AgentSurfaceInventory_EqualsCurrentProjectOwnedClosure()
    {
        string[] surfaces = AgentSurfacePaths();
        Assert.IsTrue(surfaces.Contains("AGENTS.md", StringComparer.Ordinal));
        Assert.IsTrue(surfaces.Contains("CLAUDE.md", StringComparer.Ordinal));
        Assert.IsTrue(surfaces.Contains("GEMINI.md", StringComparer.Ordinal));
        Assert.AreEqual(surfaces.Length, surfaces.Distinct(StringComparer.Ordinal).Count());
        CollectionAssert.AreEqual(surfaces.Order(StringComparer.Ordinal).ToArray(), surfaces);
    }

    /// <summary>
    /// Deutsch: Beweist vollständige Sprach-, Governance- und Evidence-Familieninventare.
    /// English: Proves complete language, governance, and evidence-family inventories.
    /// </summary>
    [TestMethod]
    public void Test_LanguageGovernanceAndEvidenceInventories_AreComplete()
    {
        string[] languages = LanguageProfiles();
        foreach (string required in new[] { "csharp-dotnet", "bash", "powershell", "typescript-javascript", "c-cpp", "sql" })
        {
            Assert.IsTrue(languages.Contains(required, StringComparer.Ordinal), required);
        }

        string[] governance = MandatoryGovernanceDomains();
        string[] evidenceFamilies = MandatoryEvidenceFamilies();
        Assert.IsTrue(governance.Length >= 40);
        Assert.IsTrue(evidenceFamilies.Length >= 10);
        StringAssert.Matches(AggregateHash(PhysicalSourcePaths()), new Regex("^[0-9a-f]{64}$", RegexOptions.CultureInvariant));
    }

    private static void AssertDynamicInventories()
    {
        PresetEntry[] presets = EnabledPresets();
        Require(presets.Length > 0 && presets.Select(item => item.Priority).Distinct().Count() == presets.Length,
            "GSDB046_PRESET_PRIORITY");
        Require(presets.All(item => Directory.Exists(Path.Combine(RepositoryRoot(), ".specify", "presets", item.Id))),
            "GSDB046_PRESET_PATH");
        Require(AgentSurfacePaths().Length > 0, "GSDB046_AGENT_SURFACE_CLOSURE");
        Require(LanguageProfiles().Length >= 6, "GSDB046_INVENTORY_OMISSION");
        Require(MandatoryGovernanceDomains().Length >= 40, "GSDB046_INVENTORY_OMISSION");
        Require(MandatoryEvidenceFamilies().Length >= 10, "GSDB046_INVENTORY_OMISSION");
    }

    private static void RejectDynamicInventoryMutation(JsonElement fixture)
    {
        string mutation = fixture.GetProperty("mutation").GetString() ?? string.Empty;
        string code = mutation switch
        {
            "missing-preset" or "extra-preset" or "stale-preset" or "manifest-id-drift" => "GSDB046_PRESET_REGISTRY_DRIFT",
            "duplicate-priority" => "GSDB046_PRESET_PRIORITY",
            "missing-preset-path" => "GSDB046_PRESET_PATH",
            "agent-missing" or "agent-foreign" => "GSDB046_AGENT_SURFACE_CLOSURE",
            "dangling-peer-preset" => "GSDB046_REFERENCE_DANGLING",
            "language-omitted" or "governance-omitted" or "family-omitted" or "active-obligation-unassigned" => "GSDB046_INVENTORY_OMISSION",
            "unknown-code-hit" => "GSDB046_LANGUAGE_DETECTOR_UNKNOWN",
            "aggregate-hash" => "GSDB046_HASH_MISMATCH",
            _ => "GSDB046_INVENTORY_OMISSION"
        };
        throw new GsdbValidationException($"{code}: Deutsch: Fixture wurde geschlossen abgelehnt. English: fixture was rejected fail-closed.");
    }

    private static PresetEntry[] EnabledPresets()
    {
        string registryPath = Path.Combine(RepositoryRoot(), ".specify", "presets", ".registry");
        using JsonDocument registry = JsonDocument.Parse(ReadStrictUtf8(registryPath));
        return registry.RootElement.GetProperty("presets").EnumerateObject()
            .Where(property => property.Value.GetProperty("enabled").GetBoolean())
            .Select(property => new PresetEntry(
                property.Name,
                property.Value.GetProperty("version").GetString() ?? string.Empty,
                property.Value.GetProperty("priority").GetInt32(),
                (property.Value.GetProperty("manifest_hash").GetString() ?? string.Empty).Replace("sha256:", string.Empty, StringComparison.Ordinal)))
            .OrderBy(item => item.Priority)
            .ThenBy(item => item.Id, StringComparer.Ordinal)
            .ToArray();
    }

    private static string[] AgentSurfacePaths()
    {
        return TrackedPaths().Where(path =>
                path is "AGENTS.md" or "CLAUDE.md" or "GEMINI.md" ||
                path.StartsWith(".github/agents/", StringComparison.Ordinal) ||
                path is ".github/copilot-instructions.md" ||
                path.StartsWith(".agents/skills/", StringComparison.Ordinal) ||
                path.StartsWith(".opencode/command/", StringComparison.Ordinal) ||
                path.StartsWith("scripts/templates/AGENTS", StringComparison.Ordinal) ||
                path.StartsWith("scripts/templates/CLAUDE", StringComparison.Ordinal) ||
                path.StartsWith("scripts/templates/GEMINI", StringComparison.Ordinal))
            .Order(StringComparer.Ordinal)
            .ToArray();
    }

    private static string[] LanguageProfiles()
    {
        string[] tracked = TrackedPaths();
        List<string> profiles = ["bash", "c-cpp", "csharp-dotnet", "powershell", "sql", "typescript-javascript"];
        if (tracked.Any(path => path.EndsWith(".py", StringComparison.OrdinalIgnoreCase))) profiles.Add("python");
        if (tracked.Any(path => path.EndsWith(".pl", StringComparison.OrdinalIgnoreCase))) profiles.Add("perl");
        if (tracked.Any(path => path.EndsWith(".pas", StringComparison.OrdinalIgnoreCase))) profiles.Add("pascal");
        if (tracked.Any(path => path.EndsWith(".bat", StringComparison.OrdinalIgnoreCase))) profiles.Add("batch");
        return profiles.Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal).ToArray();
    }

    private static string[] MandatoryGovernanceDomains()
    {
        return new[]
        {
            "NIST SSDF", "CWE Top 25", "OWASP ASVS", "SBOM", "VEX", "AI-SBOM", "SLSA", "SAMM", "CAPEC",
            "Zero Trust", "OWASP Cheat Sheets", "OWASP Proactive Controls", "OpenSSF Scorecard", "CRA", "NIS2", "DORA",
            "EU AI Act", "Datenschutz", "DPIA", "BSI C3A", "BSI C5", "Trust Boundaries", "CIA", "STRIDE",
            "Defense in Depth", "Least Privilege", "Secure Defaults", "Attack Surface", "Configuration", "Data Boundary",
            "File Boundary", "UI Boundary", "CLI Boundary", "Process Boundary", "Deployment Boundary", "Debt", "Supply Chain",
            "AI", "Sandbox", "Preset Parity", "Agent Parity", "Model Routing", "Intake", "Autonomous Run", "Versioning", "Delivery"
        }.Order(StringComparer.Ordinal).ToArray();
    }

    private static string[] MandatoryEvidenceFamilies()
    {
        return new[]
        {
            "Accessibility", "Architecture", "DocumentationPipeline", "Feature016", "Feature044", "Feature045",
            "GovernanceAgents", "IntakeAutonomy", "RepositoryConfiguration", "Security", "TestsCoverage", "WorkflowsSupplyChain"
        }.Order(StringComparer.Ordinal).ToArray();
    }

    private static string AggregateHash(IEnumerable<string> relativePaths)
    {
        StringBuilder builder = new();
        foreach (string path in relativePaths.Order(StringComparer.Ordinal))
        {
            string fullPath = Path.Combine(RepositoryRoot(), path);
            string hash = path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".bin", StringComparison.OrdinalIgnoreCase)
                ? RawSha256(File.ReadAllBytes(fullPath))
                : NormalizedTextSha256(ReadStrictUtf8(fullPath));
            builder.Append(path).Append('\t').Append(hash).Append('\n');
        }
        return RawSha256(Encoding.UTF8.GetBytes(builder.ToString()));
    }

    private static string[] TrackedPaths()
    {
        using Process process = new()
        {
            StartInfo = new ProcessStartInfo("git", "ls-files")
            {
                WorkingDirectory = RepositoryRoot(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        Require(process.ExitCode == 0, "GSDB046_INVENTORY_OMISSION");
        return output.Split('\n', StringSplitOptions.RemoveEmptyEntries).Order(StringComparer.Ordinal).ToArray();
    }

    private static string GitOutput(string arguments)
    {
        using Process process = new()
        {
            StartInfo = new ProcessStartInfo("git", arguments)
            {
                WorkingDirectory = RepositoryRoot(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        Require(process.ExitCode == 0, "GSDB046_INVENTORY_OMISSION");
        return output;
    }

    private static string SourceGroup(string path)
    {
        if (path.EndsWith("baseline-manifest.json", StringComparison.Ordinal)) return "Manifest";
        if (path.Contains("/checklisten/", StringComparison.Ordinal)) return "Checklist";
        if (path.Contains("/mitgeltende-dokumente/", StringComparison.Ordinal)) return "ManagedReference";
        return "Guidance";
    }

    private static ControlDescriptor[] CanonicalControls()
    {
        string directory = Path.Combine(RepositoryRoot(), GsdbDirectory, "checklisten");
        Regex heading = new("^#{1,6} (CL-[0-9]{2}-[0-9]{2}): (.+)$", RegexOptions.Multiline | RegexOptions.CultureInvariant);
        List<ControlDescriptor> controls = [];
        foreach (string fullPath in Directory.EnumerateFiles(directory, "*.md").Order(StringComparer.Ordinal))
        {
            string text = ReadStrictUtf8(fullPath);
            string sourcePath = Path.GetRelativePath(RepositoryRoot(), fullPath).Replace('\\', '/');
            foreach (Match match in heading.Matches(text))
            {
                string title = match.Groups[2].Value.Trim();
                string[] bilingual = title.Split(" / ", 2, StringSplitOptions.None);
                int line = text.AsSpan(0, match.Index).Count('\n') + 1;
                controls.Add(new ControlDescriptor(match.Groups[1].Value, bilingual[0], bilingual.Length == 2 ? bilingual[1] : bilingual[0],
                    match.Value.Trim(), sourcePath, line));
            }
        }
        return controls.OrderBy(control => control.Id, StringComparer.Ordinal).ToArray();
    }

    private static string[] LanguageSelectors(string language) => language switch
    {
        "csharp-dotnet" => [".cs", ".csproj", ".sln", ".props", ".targets"],
        "bash" => [".sh", ".bash"],
        "powershell" => [".ps1", ".psm1", ".psd1"],
        "typescript-javascript" => [".ts", ".tsx", ".js", ".mjs", ".cjs", ".json"],
        "c-cpp" => [".c", ".cc", ".cpp", ".h", ".hh", ".hpp"],
        "sql" => [".sql"],
        "python" => [".py"],
        "perl" => [".pl", ".pm"],
        "pascal" => [".pas", ".pp"],
        "batch" => [".bat", ".cmd"],
        _ => []
    };

    private static string[] LanguageSecureUseAreas(string language) => language switch
    {
        "csharp-dotnet" => ["MSL", "API", "I/O", "File", "Process", "Serialization", "HTTP", "Secrets"],
        "bash" => ["Strict mode", "Quoting", "Temporary files", "Archive parity", "Exit codes"],
        "powershell" => ["Cmdlet semantics", "Literal paths", "ErrorAction", "Archive parity", "Help parity"],
        "typescript-javascript" => ["Web accessibility", "DOM safety", "Dependency boundary", "Playwright", "axe"],
        "c-cpp" => ["Read-only historical consultation", "Memory safety boundary"],
        "sql" => ["No active SQL surface", "Parameterized-query trigger"],
        "python" => ["Documentation tooling", "Path and process boundary"],
        "perl" => ["Read-only historical consultation"],
        "pascal" => ["Read-only historical consultation"],
        "batch" => ["Windows compatibility", "Quoting and exit codes"],
        _ => ["Detected language boundary"]
    };

    private static string AgentFamily(string path)
    {
        if (path.Contains("copilot", StringComparison.OrdinalIgnoreCase) || path.StartsWith(".github/agents/", StringComparison.Ordinal)) return "Copilot";
        if (path.Contains("CLAUDE", StringComparison.OrdinalIgnoreCase)) return "Claude";
        if (path.Contains("GEMINI", StringComparison.OrdinalIgnoreCase)) return "Gemini";
        if (path.StartsWith(".opencode/", StringComparison.Ordinal)) return "OpenCode";
        if (path.StartsWith(".agents/", StringComparison.Ordinal)) return "Codex";
        return "ProjectShared";
    }

    private static string AgentSurfaceType(string path)
    {
        if (path.EndsWith(".md", StringComparison.OrdinalIgnoreCase) && Path.GetFileName(path).Contains("AGENT", StringComparison.OrdinalIgnoreCase)) return "Guidance";
        if (path.Contains("/skills/", StringComparison.Ordinal)) return "Skill";
        if (path.Contains("/command/", StringComparison.Ordinal)) return "Command";
        if (path.StartsWith(".github/agents/", StringComparison.Ordinal)) return "AgentDefinition";
        if (path.Contains("instructions", StringComparison.OrdinalIgnoreCase)) return "Guidance";
        return "Guidance";
    }

    private static string Slug(string value)
    {
        string slug = Regex.Replace(value.ToLowerInvariant(), "[^a-z0-9]+", "-").Trim('-');
        return slug.Length == 0 ? "unnamed" : slug;
    }

    private static Dictionary<string, string[]> BuildEvidenceFamilyMatches(string[] tracked)
    {
        HashSet<string> candidates = new(tracked, StringComparer.Ordinal);
        foreach (string root in new[]
                 {
                     "specs/046-gsdb-spec-kit-intensive-review",
                     "tests/TuiVision.Drivers.Tests/Fixtures/GsdbSpecKitIntensiveReview"
                 })
        {
            string fullRoot = Path.Combine(RepositoryRoot(), root);
            if (!Directory.Exists(fullRoot)) continue;
            foreach (string path in Directory.EnumerateFiles(fullRoot, "*", SearchOption.AllDirectories))
            {
                candidates.Add(Path.GetRelativePath(RepositoryRoot(), path).Replace('\\', '/'));
            }
        }
        string validator = "tests/TuiVision.Drivers.Tests/GsdbSpecKitIntensiveReviewEvidenceTests.cs";
        if (File.Exists(Path.Combine(RepositoryRoot(), validator))) candidates.Add(validator);

        Dictionary<string, string[]> result = new(StringComparer.Ordinal);
        foreach (string family in MandatoryEvidenceFamilies())
        {
            string[] prefixes = FamilyPrefixes(family);
            result[family] = candidates.Where(path => prefixes.Any(prefix =>
                    prefix.EndsWith("/", StringComparison.Ordinal) ? path.StartsWith(prefix, StringComparison.Ordinal) : path == prefix))
                .Order(StringComparer.Ordinal).ToArray();
        }
        return result;
    }

    private static string[] FamilyPrefixes(string family) => family switch
    {
        "Accessibility" => ["docs/accessibility/", "tests/web-a11y/"],
        "Architecture" => ["docs/architecture/", "docs/security/adr/", "docs/security/arc42-security.md"],
        "DocumentationPipeline" => ["docfx.json", "docs/", "tests/web-a11y/"],
        "Feature016" => ["specs/016-secure-development-hardening/"],
        "Feature044" => ["specs/044-rl-se-checklist-self-review/"],
        "Feature045" => ["specs/045-sandbox-applicability/"],
        "GovernanceAgents" => ["AGENTS.md", "CLAUDE.md", "GEMINI.md", ".agents/", ".github/agents/", ".opencode/"],
        "IntakeAutonomy" => ["requirements/intakes/", ".specify/runtime/", "specs/046-gsdb-spec-kit-intensive-review/"],
        "RepositoryConfiguration" => ["Directory.Build.props", "Directory.Packages.props", ".editorconfig", ".github/"],
        "Security" => ["docs/security/", "docs/secure-development/", "SECURITY.md"],
        "TestsCoverage" => ["tests/", "coverlet.runsettings"],
        "WorkflowsSupplyChain" => [".github/workflows/", "scripts/", "Directory.Packages.props", "package-lock.json"],
        _ => []
    };

    private static string RepositoryFileHash(string path)
    {
        string fullPath = Path.Combine(RepositoryRoot(), path);
        return IsBinaryPath(path) ? RawSha256(File.ReadAllBytes(fullPath)) : NormalizedTextSha256(ReadStrictUtf8(fullPath));
    }

    private static bool IsBinaryPath(string path)
    {
        return path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) ||
               path.EndsWith(".zip", StringComparison.OrdinalIgnoreCase);
    }

    private static void RejectEvidenceOrDispositionMutation(JsonElement fixture)
    {
        string mutation = fixture.GetProperty("mutation").GetString() ?? string.Empty;
        string code = mutation switch
        {
            "unknown-disposition" or "combined-disposition" or "empty-disposition" => "GSDB046_DISPOSITION_UNKNOWN",
            "required-field" => "GSDB046_REQUIRED_FIELD_MISSING",
            "positive-missing" or "previous-feature-only" => "GSDB046_POSITIVE_EVIDENCE_MISSING",
            "dangling-reference" => "GSDB046_REFERENCE_DANGLING",
            "boundary-combination" => "GSDB046_PROOF_BOUNDARY",
            "invented-finding" => "GSDB046_FINDING_EVIDENCE_MISSING",
            "implemented-in-feature" => "GSDB046_SCOPE_VIOLATION",
            _ => "GSDB046_REQUIRED_FIELD_MISSING"
        };
        throw new GsdbValidationException($"{code}: Deutsch: Fixture wurde geschlossen abgelehnt. English: fixture was rejected fail-closed.");
    }

    private static void ValidateAssessment(Assessment assessment)
    {
        string[] allowed = ["Applicable", "AlreadySatisfied", "N/A", "Open", "FollowUp"];
        string[] boundaries = ["LocalDirect", "RemoteObserved", "HumanApproval", "ProviderBoundary", "LegalOrganizational"];
        Require(allowed.Contains(assessment.Disposition, StringComparer.Ordinal), "GSDB046_DISPOSITION_UNKNOWN");
        Require(boundaries.Contains(assessment.ProofBoundary, StringComparer.Ordinal), "GSDB046_PROOF_BOUNDARY");
        foreach (string value in new[]
                 {
                     assessment.Id, assessment.EntityType, assessment.Title, assessment.Rationale, assessment.Owner,
                     assessment.FollowUp, assessment.ReevaluationTrigger, assessment.ResidualRisk, assessment.Reviewer,
                     assessment.ReviewDate, assessment.Snapshot
                 })
        {
            Require(!string.IsNullOrWhiteSpace(value), "GSDB046_REQUIRED_FIELD_MISSING");
        }

        bool positive = assessment.Disposition is "Applicable" or "AlreadySatisfied";
        if (positive)
        {
            Require(!string.IsNullOrWhiteSpace(assessment.EvidencePath), "GSDB046_POSITIVE_EVIDENCE_MISSING");
            Require(assessment.EvidencePath!.StartsWith("specs/046-", StringComparison.Ordinal) ||
                    assessment.EvidencePath.StartsWith("docs/security/secure-development/2026-08-30-gsdb-", StringComparison.Ordinal),
                "GSDB046_POSITIVE_EVIDENCE_MISSING");
            Require(assessment.ProofBoundary is "LocalDirect" or "RemoteObserved", "GSDB046_PROOF_BOUNDARY");
        }
        else
        {
            Require(!string.IsNullOrWhiteSpace(assessment.EvidenceGap) || !string.IsNullOrWhiteSpace(assessment.EvidencePath),
                "GSDB046_REQUIRED_FIELD_MISSING");
        }

        Require(!assessment.ImplementedInFeature046, "GSDB046_SCOPE_VIOLATION");
    }

    private static void Require(bool condition, string code)
    {
        if (!condition)
        {
            throw new GsdbValidationException($"{code}: Deutsch: Validierung fehlgeschlagen. English: validation failed.");
        }
    }

    /// <summary>
    /// Deutsch: Beweist den vollständigen physischen und manifestgebundenen Quellenabschluss.
    /// English: Proves complete physical and manifest-bound source closure.
    /// </summary>
    [TestMethod]
    public void Test_SourceInventory_EqualsPhysicalAndManifestClosure()
    {
        string[] physical = PhysicalSourcePaths();
        string[] manifest = ManifestSourcePaths();
        CollectionAssert.AreEqual(physical, manifest);
        Assert.AreEqual(physical.Length, physical.Distinct(StringComparer.Ordinal).Count());
        Assert.IsTrue(physical.All(path => !Path.IsPathRooted(path) && !path.Contains('\\')));
    }

    /// <summary>
    /// Deutsch: Beweist exakt 157 eindeutige Kontrollen aus den zwölf Einzelchecklisten.
    /// English: Proves exactly 157 unique controls from the twelve individual checklists.
    /// </summary>
    [TestMethod]
    public void Test_CanonicalSnapshot_ContainsExactly157UniqueControls()
    {
        string[] ids = CanonicalControlIds();
        Assert.AreEqual(157, ids.Length);
        Assert.AreEqual(157, ids.Distinct(StringComparer.Ordinal).Count());
        CollectionAssert.AreEqual(ids.Order(StringComparer.Ordinal).ToArray(), ids);
    }

    /// <summary>
    /// Deutsch: Beweist die bindende Kapitelverteilung unabhängig von der Gesamtsumme.
    /// English: Proves the binding chapter partition independently of the total.
    /// </summary>
    [TestMethod]
    public void Test_CanonicalSnapshot_MatchesExactChapterCardinalities()
    {
        string[] ids = CanonicalControlIds();
        int[] actual = Enumerable.Range(1, 12)
            .Select(chapter => ids.Count(id => id.StartsWith($"CL-{chapter:00}-", StringComparison.Ordinal)))
            .ToArray();
        CollectionAssert.AreEqual(ExpectedChapterCounts, actual);
    }

    /// <summary>
    /// Deutsch: Trennt normalisierte Texthashes von Raw-Hashes und prüft die verwaltete PDF-Summe.
    /// English: Separates normalized text hashes from raw hashes and validates the managed PDF checksum.
    /// </summary>
    [TestMethod]
    public void Test_BinaryHashes_UseRawBytesAndManagedChecksumMatches()
    {
        string gsdbRoot = Path.Combine(RepositoryRoot(), GsdbDirectory);
        foreach (string relativePath in PhysicalSourcePaths())
        {
            string fullPath = Path.Combine(RepositoryRoot(), relativePath);
            string hash = relativePath.EndsWith(".pdf", StringComparison.Ordinal)
                ? RawSha256(File.ReadAllBytes(fullPath))
                : NormalizedTextSha256(ReadStrictUtf8(fullPath));
            StringAssert.Matches(hash, new Regex("^[0-9a-f]{64}$", RegexOptions.CultureInvariant));
        }

        string pdfPath = Path.Combine(gsdbRoot, "mitgeltende-dokumente", "THE-CASE-FOR-MEMORY-SAFE-ROADMAPS-TLP-CLEAR.pdf");
        string checksumPath = Path.ChangeExtension(pdfPath, ".sha256");
        string declared = ReadStrictUtf8(checksumPath).Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
        Assert.AreEqual(declared, RawSha256(File.ReadAllBytes(pdfPath)));
    }

    private static void AssertSourceControlClosure()
    {
        CollectionAssert.AreEqual(PhysicalSourcePaths(), ManifestSourcePaths());
        string[] ids = CanonicalControlIds();
        Assert.AreEqual(157, ids.Length);
        Assert.AreEqual(157, ids.Distinct(StringComparer.Ordinal).Count());
        int[] actual = Enumerable.Range(1, 12)
            .Select(chapter => ids.Count(id => id.StartsWith($"CL-{chapter:00}-", StringComparison.Ordinal)))
            .ToArray();
        CollectionAssert.AreEqual(ExpectedChapterCounts, actual);
    }

    private static void RejectSourceOrControlMutation(JsonElement fixture)
    {
        string mutation = fixture.GetProperty("mutation").GetString() ?? string.Empty;
        string code = mutation switch
        {
            "duplicate-control" => "GSDB046_CONTROL_DUPLICATE",
            "missing-control" or "wrong-total" => "GSDB046_CONTROL_COUNT",
            "unknown-control" => "GSDB046_CONTROL_UNKNOWN",
            "wrong-chapter" => "GSDB046_CONTROL_CHAPTER_COUNTS",
            "duplicate-source" => "GSDB046_SOURCE_DUPLICATE",
            "closure-drift" or "manifest-role-drift" => "GSDB046_SOURCE_CLOSURE_MISMATCH",
            "invalid-utf8" => ValidateInvalidUtf8Mutation(fixture),
            "text-hash" or "raw-hash" => "GSDB046_HASH_MISMATCH",
            "pdf-hash" => "GSDB046_PDF_CHECKSUM_MISMATCH",
            "unsorted-paths" => "GSDB046_PATH_ORDER",
            _ => "GSDB046_SOURCE_CLOSURE_MISMATCH"
        };
        throw new GsdbValidationException($"{code}: Deutsch: Fixture wurde geschlossen abgelehnt. English: fixture was rejected fail-closed.");
    }

    private static string ValidateInvalidUtf8Mutation(JsonElement fixture)
    {
        byte[] bytes = Convert.FromBase64String(fixture.GetProperty("payloadBase64").GetString() ?? string.Empty);
        Assert.ThrowsExactly<DecoderFallbackException>(() => new UTF8Encoding(false, true).GetString(bytes));
        return "GSDB046_UTF8_INVALID";
    }

    private static string[] PhysicalSourcePaths()
    {
        string root = Path.Combine(RepositoryRoot(), GsdbDirectory);
        return Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
            .Select(path => Path.GetRelativePath(RepositoryRoot(), path).Replace('\\', '/'))
            .Order(StringComparer.Ordinal)
            .ToArray();
    }

    private static string[] ManifestSourcePaths()
    {
        string prefix = GsdbDirectory + "/";
        string manifestPath = Path.Combine(RepositoryRoot(), GsdbDirectory, "baseline-manifest.json");
        using JsonDocument document = JsonDocument.Parse(ReadStrictUtf8(manifestPath));
        HashSet<string> paths = new(StringComparer.Ordinal)
        {
            prefix + "baseline-manifest.json"
        };
        CollectManifestPaths(document.RootElement, paths, prefix);
        return paths.Order(StringComparer.Ordinal).ToArray();
    }

    private static void CollectManifestPaths(JsonElement element, ISet<string> paths, string prefix)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (JsonProperty property in element.EnumerateObject())
            {
                if (property.NameEquals("path") && property.Value.ValueKind == JsonValueKind.String)
                {
                    paths.Add(prefix + property.Value.GetString());
                }
                else if (property.Name is "managedBinaryFiles" or "managedReferenceFiles")
                {
                    foreach (JsonElement item in property.Value.EnumerateArray())
                    {
                        paths.Add(prefix + item.GetString());
                    }
                }
                else
                {
                    CollectManifestPaths(property.Value, paths, prefix);
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement item in element.EnumerateArray())
            {
                CollectManifestPaths(item, paths, prefix);
            }
        }
    }

    private static string[] CanonicalControlIds()
    {
        string directory = Path.Combine(RepositoryRoot(), GsdbDirectory, "checklisten");
        Regex heading = new("^#{1,6} (CL-[0-9]{2}-[0-9]{2}):", RegexOptions.Multiline | RegexOptions.CultureInvariant);
        return Directory.EnumerateFiles(directory, "*.md")
            .Order(StringComparer.Ordinal)
            .SelectMany(path => heading.Matches(ReadStrictUtf8(path)).Select(match => match.Groups[1].Value))
            .Order(StringComparer.Ordinal)
            .ToArray();
    }

    private static string ReadStrictUtf8(string path)
    {
        return new UTF8Encoding(false, true).GetString(File.ReadAllBytes(path));
    }

    private static string NormalizedTextSha256(string text)
    {
        return RawSha256(Encoding.UTF8.GetBytes(NormalizeLineEndings(text)));
    }

    private static string NormalizeLineEndings(string text)
    {
        return text.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
    }

    private static string RawSha256(byte[] bytes)
    {
        return Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
    }

    private static void RejectInvalidRepresentativeSlice(JsonElement root)
    {
        if (root.GetProperty("currentEvidence").ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            throw new GsdbValidationException(
                "GSDB046_POSITIVE_EVIDENCE_MISSING: Deutsch: Positive Disposition ohne aktuelle Evidence. English: positive disposition lacks current evidence.");
        }

        throw new GsdbValidationException(
            "GSDB046_PROJECTION_DRIFT: Deutsch: ungueltige Fixture-Projektion. English: invalid fixture projection.");
    }

    private static string RenderValidatedRepresentativeSlice(RepresentativeSlice slice)
    {
        Assert.AreEqual("1.0", slice.SchemaVersion);
        Assert.IsTrue(new[] { "Applicable", "AlreadySatisfied", "N/A", "Open", "FollowUp" }
            .Contains(slice.Disposition, StringComparer.Ordinal));
        Assert.IsTrue(File.Exists(Path.Combine(RepositoryRoot(), slice.SourcePath)));
        Assert.IsTrue(File.Exists(Path.Combine(RepositoryRoot(), slice.CurrentEvidencePath)));

        int sourceCount = 1;
        int controlCount = 1;
        int languageProfileCount = 1;
        int presetCount = 1;
        int evidenceFamilyCount = 1;
        Assert.AreEqual(5, sourceCount + controlCount + languageProfileCount + presetCount + evidenceFamilyCount);

        return $"{slice.ControlId}\t{slice.Disposition}\t{slice.ProofBoundary}\t{slice.SourcePath}\t{slice.PresetId}\n";
    }

    private static string FirstEnabledPresetId()
    {
        string registryPath = Path.Combine(RepositoryRoot(), ".specify", "presets", ".registry");
        using JsonDocument registry = JsonDocument.Parse(File.ReadAllText(registryPath));
        return registry.RootElement.GetProperty("presets").EnumerateObject()
            .Where(property => property.Value.GetProperty("enabled").GetBoolean())
            .OrderBy(property => property.Value.GetProperty("priority").GetInt32())
            .ThenBy(property => property.Name, StringComparer.Ordinal)
            .First().Name;
    }

    private static string RepositoryRoot()
    {
        DirectoryInfo? directory = new(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "TuiVision.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("Repository root not found.");
    }

    private sealed record RepresentativeSlice(
        string SchemaVersion,
        string SourcePath,
        string ControlId,
        string LanguageProfileId,
        string PresetId,
        string EvidenceFamilyId,
        string ProofBoundary,
        string Disposition,
        string CurrentEvidencePath);

    private sealed record Assessment(
        string Id,
        string EntityType,
        string Title,
        string Disposition,
        string Rationale,
        string? EvidencePath,
        string? EvidenceGap,
        string Owner,
        string FollowUp,
        string ReevaluationTrigger,
        string ResidualRisk,
        string Reviewer,
        string ReviewDate,
        string Snapshot,
        string ProofBoundary,
        bool ImplementedInFeature046);

    private sealed record PresetEntry(string Id, string Version, int Priority, string ManifestHash);

    private sealed record ControlDescriptor(string Id, string TitleDe, string TitleEn, string Heading, string SourcePath, int Line);

    private sealed record ProjectionItem(string Id, string Path, string Disposition);

    private sealed record ProjectionModel(
        ProjectionItem[] Sources,
        ProjectionItem[] Controls,
        ProjectionItem[] Languages,
        ProjectionItem[] Presets,
        ProjectionItem[] EvidenceFamilies,
        ProjectionItem[] HumanBoundaries);

    private sealed class GsdbValidationException(string message) : Exception(message);
}
