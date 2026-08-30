# Datenmodell: GSDB-Spec-Kit-Intensivprüfung / Data Model: GSDB Spec Kit Intensive Review

**Feature**: `046-gsdb-spec-kit-intensive-review`
**Kanonische Ausgabe / Canonical output**: `docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/gsdb-spec-kit-intensive-review.json`

## 1. Modellprinzipien / Model principles

1. Die JSON-Datei ist kanonisch; Markdown-Dateien sind verlustfreie, deterministische Projektionen.
   The JSON file is canonical; Markdown files are lossless deterministic projections.
2. Nur das Kontrollinventar ist fest: `157` Kontrollen mit der Kapitelverteilung `12/13/15/10/13/11/12/13/17/17/12/12`. Alle Nicht-Kontroll-Anzahlen werden aus Arrays oder dem akzeptierten Snapshot abgeleitet.
   Only the control inventory is fixed: `157` controls with chapter partition `12/13/15/10/13/11/12/13/17/17/12/12`. Every non-control count is derived from arrays or the accepted snapshot.
3. Positive Dispositionen benötigen neue Feature-046-Evidenz. Frühere Features dürfen referenziert, aber nicht als positive Entscheidung kopiert werden.
   Positive dispositions require new Feature 046 evidence. Earlier features may be referenced but not copied as positive decisions.
4. Jeder Pfad ist repository-relativ, mit `/` getrennt und ordinal sortiert. IDs und Schlüssel sind case-sensitive.
   Every path is repository-relative, slash-separated, and ordinally sorted. IDs and keys are case-sensitive.
5. Text-Hashes verwenden UTF-8 und normalisierte LF-Zeilenenden; Binär-Hashes verwenden Rohbytes.
   Text hashes use UTF-8 and normalized LF line endings; binary hashes use raw bytes.

## 2. Wurzelobjekt / Root object

`GsdbReviewSnapshot`

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `schemaVersion` | string | Exakt `1.0`. / Exactly `1.0`. |
| `featureId` | string | Exakt `046-gsdb-spec-kit-intensive-review`. |
| `snapshot` | object | Datum, Branch, Commit, Delivery-Modus und Eingabe-Hashes. |
| `allowedDispositions` | array | Exakt die fünf akzeptierten Code-/Textwerte. |
| `inventoryRules` | object | Ableitungs-, Sortier-, Hash- und Projektionsregeln. |
| `sources` | array | Eindeutiges physisches GSDB-Quelleninventar. |
| `controls` | array | Exakt 157 kanonische Kontrollen. |
| `languageProfiles` | array | Aus Regeln und Repository-Aktivität abgeleitete Sprachprofile. |
| `presets` | array | Alle aktivierten Registry-Einträge, dynamisch abgeleitet. |
| `agentSurfaces` | array | Alle aktuellen projektgeführten Guidance-, Command-, Prompt-, Skill- und Agent-Flächen. |
| `governanceCheckpoints` | array | Verfassungs-, Agenten-, Preset- und Laufpflichten. |
| `evidenceFamilies` | array | Deklarative Suchfamilien und ihre abgeleiteten Treffer. |
| `evidenceReferences` | array | Eindeutige, pfad- und hashgenaue Evidenzanker. |
| `humanBoundaries` | array | Remote-, Provider-, Rechts- und Freigabegrenzen. |
| `observations` | array | Erst nach Prüfung erzeugte Befunde oder Folgehinweise; leer erlaubt. |
| `summary` | object | Ausschließlich aus den vorstehenden Arrays abgeleitete Summen. |
| `validation` | object | Validatorvertragsversion, Zeit und fachlicher Ergebniszustand; keine Projektionshashes, damit der kanonische Hash azyklisch bleibt. |

## 3. Snapshot und Regeln / Snapshot and rules

### `SnapshotMetadata`

- `reviewDate`: `2026-08-30`; `reviewedAtUtc`: tatsächlicher Abschlusszeitpunkt.
- `reviewerRole`: verantwortliche Reviewer-Rolle; kein erfundener Personenname.
- `branchName`, `commitSha`: tatsächlich geprüfter lokaler Snapshot.
- `deliveryMode`: `MergeAndSync`.
- `autonomousRunId`, `phaseId`: Bindung an den Lauf; während der Umsetzung ist die Phase nicht mehr `plan-1`.
- `inputHashes`: sortierte Datensätze aus `path`, `normalizedSha256`, `expectedSha256`, `match`, `bindingKind` und `bindingSourcePath`. `acceptedArtifact` bindet Intake/Review/Manifest/Receipt; `routingPayload` bindet jeweils den neuesten abgeschlossenen Phasen-Payload; `reviewAttestation` bindet die post-remediation Hashliste in einem selbst als Routing-Payload gebundenen Reviewbericht. Ein älterer Payload derselben Datei ist nur Historie und eine spätere Review-Attestation hat für ihre ausdrücklich gelisteten Pfade Vorrang.
- `sourceBaselineVersionDeclarations`: abgeleitete, nicht harmonisierte Versionsangaben.
- `historicalSourceDecision`: normalerweise `N/A`; bei begrenzter Einsicht mit Frage und Pfaden.

### `InventoryRules`

- `textNormalization`: UTF-8, optionales BOM entfernen, `CRLF` und `CR` zu `LF`, kein inhaltliches Trimmen.
- `binaryHashMode`: Raw SHA-256.
- `pathOrder`: ordinal, case-sensitive.
- `controlSource`: Überschriften der zwölf GSDB-Checklisten.
- `expectedControlCountByChapter`: exakt `CL-01=12`, `CL-02=13`, `CL-03=15`, `CL-04=10`, `CL-05=13`, `CL-06=11`, `CL-07=12`, `CL-08=13`, `CL-09=17`, `CL-10=17`, `CL-11=12`, `CL-12=12`; aus den Kontrollzeilen neu berechnen und gegen diese akzeptierte Kontrollinvariante vergleichen.
- `sourceClosure`: physischer Baum entspricht eindeutigem Manifestabschluss.
- `presetSource`: alle `enabled`-Einträge der aktiven Registry.
- `languageSource`: Vereinigung aus GSDB-Regeln, Verfassungs-/Preset-Profilen und `git ls-files`-basierten versionierten Dateityp-/Shebang-Detektoren. Detektoren und Ausschlüsse stehen mit Begründung im Datensatz; ein unbekannter codeartiger Treffer ist ein Fehler, keine stille Auslassung.
- `agentSurfaceSource`: Vereinigung aus dem TuiVision-Level-2-Eintrag, Agentenschlüsseln aller aktivierten Registry-Einträge und tatsächlich versionierten Guidance-/Command-/Prompt-/Skill-/Agent-Pfaden. Persönlicher Agentenzustand und nicht vorhandene Flächen sind ausgeschlossen.
- `governanceSource`: stabil benannte Pflichtkategorien aus beiden Verfassungen, aktivierten Presets, Agentenflächen, Modell-Routing, Intake-/Serienzustand, Versionierung, Delivery-Modus und autonomem Laufzustand; jede aktive Quelle muss mindestens einem Checkpoint zugeordnet sein.
- `evidenceFamilySource`: akzeptierter Pflichtdomänenkatalog plus zusätzliche aktive Preset-/Governance-Pflichten; jede Pflicht ist genau einer primären Familie zugeordnet und darf weitere Familien referenzieren.
- `summarySource`: nur Array-Aggregate; keine manuell gepflegten Zahlen.
- `projectionSource`: kanonisches JSON plus fest definierte Renderer-Reihenfolge.

## 4. Gemeinsamer Bewertungsblock / Common assessment block

`Assessment`

| Feld / Field | Pflicht / Required | Bedeutung / Meaning |
|---|---:|---|
| `disposition` | ja / yes | Einer der erlaubten Codes. / One allowed code. |
| `dispositionLabelDe` | ja / yes | Deutsche Klartextbezeichnung. |
| `dispositionLabelEn` | ja / yes | English expanded label. |
| `rationaleDe` | ja / yes | Evidenzbezogene Begründung, CEFR-B2. |
| `rationaleEn` | ja / yes | Evidence-based rationale, CEFR-B2. |
| `evidenceReferenceIds` | ja / yes | Mindestens ein aktueller Eintrag bei positiver Disposition; sonst eine explizite Beweislücke oder Grenze im Assessment. |
| `proofBoundary` | ja / yes | `LocalDirect`, `RemoteObserved`, `HumanApproval`, `ProviderBoundary` oder `LegalOrganizational`. |
| `owner` | ja / yes | Verantwortliche Rolle, keine erfundene Person. |
| `risk` | ja / yes | Konkretes Risiko oder `NoneObserved` mit Begründung. |
| `revalidationTrigger` | ja / yes | Ereignis, das eine neue Prüfung auslöst. |
| `followUp` | ja / yes | Dokumentierter Hinweis oder `None`; keine Umsetzung in Feature 046. |

Erlaubt sind ausschließlich `Applicable`, `AlreadySatisfied`, `N/A`, `Open` und `FollowUp`. Das gilt für Kontrollen und alle zusätzlichen Richtlinien-, Dokument-, Sprach-, Preset-, Constitution-, Governance- und Evidenz-Checkpoints. `Pass`, kombinierte Werte und domänenspezifische Ersatzcodes sind ungültig.

Only `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, and `FollowUp` are allowed. This applies to controls and every additional policy, document, language, preset, constitution, governance, and evidence checkpoint. `Pass`, combined values, and domain-specific replacement codes are invalid.

## 5. Quellen / Sources

`SourceRecord`

- `sourceId`: stabil aus normalisiertem Pfad abgeleitet.
- `path`, `fileName`, `mediaType`, `sourceGroup`.
- `manifestRoles`: sortierte Liste; Mehrfachrollen bleiben an einer Datei.
- `registeredVersion`: Manifestwert, falls vorhanden.
- `declaredVersions`: aus dem Inhalt abgeleitete sortierte Liste.
- `sizeBytes`, `lineCount` für Text oder `null` für Binärdaten.
- `hashMode`, `sha256`.
- `linkedChecksumPath`, `linkedChecksumMatch` für verwaltete Binärdateien.
- `assessment`.

Invarianten:

- Pfad und `sourceId` sind eindeutig.
- Die Menge entspricht dem physischen GSDB-Baum und dem eindeutigen Manifestabschluss.
- Keine Datei wird wegen mehrerer Rollen doppelt gezählt.
- Jede Textdatei wird als UTF-8 geprüft; ungültiges UTF-8 schlägt fehl.
- Die PDF-Prüfsumme stimmt mit der verwalteten SHA-Datei überein oder erzeugt einen expliziten fehlgeschlagenen Zustand.

## 6. Kontrollen / Controls

`ControlRecord`

- `controlId`: kanonische `CL-XX-NN`-ID.
- `chapterId`, `chapterTitleDe`, `chapterTitleEn`.
- `ordinal`, `titleDe`, `titleEn`.
- `sourceId`, `sourceHeading`, `sourceLine`.
- `sourceContext`: bewahrt getrennt `applicability` aus `Applicable`, `N/A`, `Open` und `fulfillment` aus `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed`; diese Werte ersetzen nie `assessment.disposition`.
- `languageProfileIds`, `presetIds`, `evidenceFamilyIds`.
- `assessment`.

Invarianten:

- Exakt 157 Datensätze.
- Jede ID ist eindeutig, entspricht `^CL-[0-9]{2}-[0-9]{2}$` und kommt genau einmal in den Checklisten vor.
- Reihenfolge ist Kapitelnummer, dann Kontrollnummer.
- Kapitel- und Gesamtzahlen werden aus den Zeilen berechnet.
- Die berechneten Kapitelzahlen entsprechen exakt `12/13/15/10/13/11/12/13/17/17/12/12`; eine Umverteilung bei weiterhin 157 Zeilen ist ungültig.
- Jede Kontrolle hat genau eine Disposition.
- Die zweiachsigen GSDB-Quellbegriffe bleiben als Quellkontext erhalten und erzeugen keinen positiven Claim.
- `Applicable` und `AlreadySatisfied` benötigen mindestens eine aktuelle `LocalDirect`-Evidenzreferenz oder eine ausdrücklich zulässige externe Grenze; frühere positive Disposition allein ist ungültig.

## 7. Sprachprofile / Language profiles

`LanguageProfile`

- `languageId`, `labelDe`, `labelEn`.
- `activity`: `Active`, `ReadOnlyHistorical` oder `AbsentRuleProfile`.
- `ruleSourceIds`: GSDB-Quellen mit Sprachregeln.
- `trackedSelectors`, `trackedFileCount`, `trackedSamplePaths`.
- `detectorEvidence`: Dateityp-, Shebang- oder explizite Regelquelle, die das Profil in die Vereinigung aufgenommen hat.
- `excludedSelectors` mit Begründung, zum Beispiel generierte oder externe Bestände.
- `historicalConsultation`: `N/A` oder konkrete Frage, Pfade und Ergebnis.
- `controlIds`, `assessment`.

Die Zahl der Profile ist die Kardinalität der abgeleiteten Vereinigung. Ein Profil darf nicht entfallen, nur weil es keine aktiven Dateien hat.

The profile count is the cardinality of the derived union. A profile may not disappear merely because it has no active files.

## 8. Presets und Governance / Presets and governance

### `PresetRecord`

- `presetId`, `installedVersion`, `priority`, `enabled`.
- `registryPath`, `presetPath`, `registryEntryHash`, `presetManifestHash`.
- `planObligations`, `agentSurfaceIds`, `assessment`.

Invarianten:

- Genau ein Datensatz pro aktuell aktiviertem Registry-Eintrag.
- Anzahl und Versionen werden zur Laufzeit aus der Registry abgeleitet.
- Beim Planen wurden 12 aktivierte Presets beobachtet; dies ist kein Validatorliteral.
- Doppelte Prioritäten, fehlende Presetpfade oder Registry-/Manifest-ID-Abweichungen schlagen fehl.

### `AgentSurface`

- `agentSurfaceId`, `agentFamily`, `surfaceType` aus `Guidance`, `Command`, `Prompt`, `Skill` oder `AgentDefinition`.
- `path`, `sha256`, `hashMode`, `sourceAuthorities`.
- `registeredCommandIds`, soweit die aktive Registry diese Fläche bindet.
- `parityGroupId`, `requiredPeers`, `observedPeers`, `assessment`.

Invarianten:

- Jeder aktuell projektgeführte, versionierte Pfad erscheint genau einmal; Verzeichnisnamen allein sind keine Datensätze.
- Die Menge wird aus aktueller Verfassung, aktivierter Registry und tracked paths geschlossen. Beim Planen beobachtete Agentennamen oder eine feste Anzahl sind keine Validatorliterale.
- `PresetRecord.agentSurfaceIds`, `requiredPeers` und `observedPeers` verweisen nur auf vorhandene `AgentSurface`-IDs.
- Persönliche Zustände, Sessions, Logs, Credentials und nicht vorhandene Junie- oder andere Flächen werden weder inventarisiert noch als erfüllt behauptet.

### `GovernanceCheckpoint`

- `checkpointId`, `category`, `titleDe`, `titleEn`, `sourcePaths`.
- `expectedState`, `observedState`.
- `agentSurfaceIds`: Referenzen auf die tatsächlich abgeleiteten projektgeführten Flächen.
- `sharedWriter`: boolescher Wert für serialisierte Schreibflächen.
- `assessment`.

Checkpoints decken mindestens beide Verfassungen, Registry/Preset-Konsistenz, Agenten-Parität, Modell-Routing, autonome Laufbindung, Intake-Serie, Versionierung, Liefermodus und gemeinsame Writer ab. Die tatsächliche Menge entsteht aus `governanceSource`; eine aktive Quellpflicht ohne Checkpoint ist ungültig.

Checkpoints cover at minimum both constitutions, registry/preset consistency, agent parity, model routing, autonomous run binding, intake series, versioning, delivery mode, and shared writers. The actual set derives from `governanceSource`; an active source obligation without a checkpoint is invalid.

## 9. Evidenz / Evidence

### `EvidenceFamily`

- `familyId`, `labelDe`, `labelEn`, `purposeDe`, `purposeEn`.
- `includeSelectors`, `excludeSelectors`.
- `matchedPaths`, `matchedPathCount`, `aggregateSha256`.
- `assessment`.

`aggregateSha256` wird aus der UTF-8-Darstellung der sortierten Zeilen `path<TAB>fileSha256<LF>` gebildet. Leere Familien benötigen eine explizite Disposition und Begründung.

`aggregateSha256` is built from the UTF-8 representation of sorted `path<TAB>fileSha256<LF>` lines. Empty families require an explicit disposition and rationale.

Die Familienmenge wird aus `evidenceFamilySource` abgeleitet. Der Pflichtdomänenkatalog umfasst Security, Architektur, Barrierefreiheit, Feature-Evidenz 016/044/045, Workflows/Lieferkette, Tests/Coverage, Dokumentationspipeline, Governance/Agenten, Intake/Autonomie und Repository-Konfiguration. Zusätzliche aktive Preset- oder Governance-Pflichten erweitern diese Menge deterministisch; ihre Anzahl ist nicht fest.

The family set is derived from `evidenceFamilySource`. The mandatory domain catalog covers security, architecture, accessibility, Feature 016/044/045 evidence, workflows/supply chain, tests/coverage, the documentation pipeline, governance/agents, intake/autonomy, and repository configuration. Additional active preset or governance obligations extend this set deterministically; its count is not fixed.

### `EvidenceReference`

- `evidenceReferenceId`, `familyId`, `path`, `anchor` oder `jsonPointer`.
- `sha256`, `hashMode`, `observedAtCommit`.
- `proofKind`, `claimDe`, `claimEn`.
- `limitationsDe`, `limitationsEn`.

Eine Referenz muss auf einen Pfad in `sources`, `evidenceFamilies.matchedPaths` oder auf einen explizit gekennzeichneten temporären Gate-Beleg zeigen. Temporäre Belege werden nicht als dauerhafte Quellen ausgegeben.

An evidence reference must point to a path in `sources`, `evidenceFamilies.matchedPaths`, or an explicitly marked temporary gate record. Temporary records are not emitted as durable sources.

## 10. Menschliche Grenzen und Beobachtungen / Human boundaries and observations

### `HumanBoundary`

- `boundaryId`, `category`, `titleDe`, `titleEn`, `sourcePaths`, `claimNotMadeDe`, `claimNotMadeEn`.
- `requiredActor`, `requiredExternalEvidence`, `currentStatus`.
- `relatedControlIds`, `assessment`.

### `Observation`

- `observationId`, `severity`, `category`.
- `statementDe`, `statementEn`.
- `evidenceReferenceIds`, `affectedRecordIds`.
- `actionability`: `DocumentationOnly`, `FollowUpCandidate`, `HumanDecision`, `ProviderDecision`.
- `recommendedNextStepDe`, `recommendedNextStepEn`.
- `implementedInFeature046`: immer `false`.

Der Plan erzeugt keine Beobachtungen vorab. Das Array wird nur mit tatsächlich belegten Ergebnissen gefüllt. Es darf leer bleiben.

The plan pre-creates no observations. The array is populated only with evidenced results. It may remain empty.

## 11. Zusammenfassung / Summary

`Summary`

- `sourceCount`, `sourceCountByGroup`, `sourceCountByMediaType`.
- `controlCount` und `controlCountByChapter`, `controlCountByDisposition`.
- `languageProfileCount`, `languageProfileCountByActivity`, `languageProfileCountByDisposition`.
- `presetCount`, `presetCountByDisposition`.
- `agentSurfaceCount`, `agentSurfaceCountByFamily`, `agentSurfaceCountByType`, `agentSurfaceCountByDisposition`.
- `governanceCheckpointCount`, `governanceCheckpointCountByDisposition`.
- `evidenceFamilyCount`, `evidenceFamilyCountByDisposition`.
- `evidenceReferenceCount`.
- `humanBoundaryCount`, `observationCount`, `observationCountBySeverity`.
- `positiveDispositionCount`, `positiveDispositionEvidenceGapCount`.
- `actionableTechnicalFindingCount`, `remoteBoundaryCount`, `humanBoundaryOpenCount`.
- `projectionCount`, abgeleitet aus den in `inventoryRules` deklarierten Projektionsdefinitionen.

Jeder Wert ist ein berechnetes Resultat. Der Validator berechnet die Summen unabhängig und vergleicht sie mit dem JSON. Für das Kontrollinventar gelten zusätzlich `controlCount == 157` und die exakte Kapitelverteilung `12/13/15/10/13/11/12/13/17/17/12/12`; alle Nicht-Kontroll-Summen bleiben dynamisch.

Every value is computed. The validator independently calculates the summaries and compares them with the JSON. The control inventory additionally requires `controlCount == 157` and the exact chapter partition `12/13/15/10/13/11/12/13/17/17/12/12`; every non-control summary remains dynamic.

## 12. Projektionen / Projections

### Maschinenlesbare Projektionen / Machine-readable projections

Jede Projektion ist ein deterministisch gerendertes JSON-Objekt mit `schemaVersion`, `featureId`, `projectionId`, `canonicalNormalizedSha256`, `records` oder `summary` und `projectionPayloadSha256`. Dieser letzte Wert ist der normalisierte SHA-256 des Projektion-Payloads ohne das Hashfeld selbst; dadurch entsteht keine zirkuläre Prüfsumme. Die Projektion wird ausschließlich aus dem kanonischen Wurzelobjekt erzeugt und niemals manuell gepflegt.

Every projection is a deterministically rendered JSON object with `schemaVersion`, `featureId`, `projectionId`, `canonicalNormalizedSha256`, `records` or `summary`, and `projectionPayloadSha256`. The last value is the normalized SHA-256 of the projection payload excluding the hash field itself, avoiding a circular checksum. The projection is generated only from the canonical root object and is never maintained manually.

| Datei / File | Projektion / Projection |
|---|---|
| `source-projection.json` | `sources` und abgeleitete Quellsummen / `sources` and derived source summaries |
| `control-projection.json` | `controls` und abgeleitete Kontrollsummen |
| `language-projection.json` | `languageProfiles` und abgeleitete Sprachsummen |
| `preset-governance-projection.json` | `presets`, `agentSurfaces`, `governanceCheckpoints` und ihre Summen |
| `evidence-family-projection.json` | `evidenceFamilies`, `evidenceReferences` und ihre Summen |
| `summary-projection.json` | `summary`, Validierungsstatus und Nachweisgrenzen ohne neue Aussage |

Die Projektionsmenge folgt den akzeptierten Kategorien; ihre Zählung wird aus der versionierten Projektionsliste abgeleitet. Jeder Projektions-Payload-Hash muss mit dem gerenderten Payload übereinstimmen und jeder `canonicalNormalizedSha256` auf dasselbe kanonische JSON zeigen. Das kanonische JSON speichert umgekehrt keine Projektionshashes; die vollständige Hashliste gehört in `validation-evidence.md` und `pr-evidence.md`. So bleibt der Hashgraph azyklisch.

The projection set follows the accepted categories; its count is derived from the versioned projection list. Every payload hash must match the normalized rendered payload excluding its own hash field, and every `canonicalNormalizedSha256` must point to the same canonical JSON.

### Lesbare Projektionen / Human-readable projections

Die kanonische Datei erzeugt genau diese fachlichen Projektionen im datierten Verzeichnis:

The canonical file generates exactly these subject projections in the dated directory:

| Datei / File | Primäre Datensätze / Primary records |
|---|---|
| `source-inventory.md` | `sources`, Source summary |
| `control-assessment.md` | `controls`, Control summary |
| `language-assessment.md` | `languageProfiles` |
| `preset-governance-assessment.md` | `presets`, `agentSurfaces`, `governanceCheckpoints` |
| `evidence-family-assessment.md` | `evidenceFamilies`, `evidenceReferences` |
| `human-boundaries.md` | `humanBoundaries`, externe Nachweisgrenzen |
| `summary.md` | ausschließlich `summary`, keine neue Aussage |
| `validation-evidence.md` | `validation`, lokale/remote Gate-Grenzen |
| `README.md` | Navigation, Scope, Nicht-Zertifizierung, Leseanleitung |

Die Projektionen werden bytegenau mit dem Validator-Renderer verglichen. Manuelle Abweichungen sind ungültig. Übersetzungen enthalten denselben fachlichen Inhalt und keine unabhängigen Zählungen.

The projections are compared byte-for-byte with the validator renderer. Manual differences are invalid. Translations contain the same semantic content and no independent counts.

## 13. Zustandsübergänge / State transitions

```text
AcceptedInputs
    -> InventoryDerived
    -> EvidenceAssessed
    -> CanonicalJsonComplete
    -> ProjectionsRendered
    -> LocalValidationGreen
    -> CommittedCandidateValidated
    -> RemoteExactHeadObserved
    -> MergeAndSyncObserved
    -> CausalCloseoutComplete
```

Ein Übergang darf nur vorwärts erfolgen, wenn sein Gate vollständig belegt ist. `HumanApproval` kann ausschließlich ein ausdrücklich benanntes, nachweislich nicht verfügbares Remote-Gate nach vollständigem lokalem technischem Grün, null umsetzbaren technischen Befunden, null actionable Review-Threads, null Scope-Verstößen und Human Approval als einziger offener Regel ersetzen. Der temporäre Gate-Beleg nennt Gate, autorisierte Person, Zeitpunkt, Begründung, Evidence-Grenze und Ablaufzeitpunkt. Die Ausnahme verändert keine fachliche Disposition und ist in dieser Planphase nicht autorisiert.

A transition moves forward only when its gate is fully evidenced. `HumanApproval` can replace only one explicitly named and demonstrably unavailable remote gate after complete local technical green status, zero actionable technical findings, zero actionable review threads, zero scope violations, and Human Approval as the sole open rule. The temporary gate record states the gate, authorized person, timestamp, rationale, evidence boundary, and expiry. The exception changes no substantive disposition and is not authorized in this planning phase.
