# Quickstart: GSDB-Spec-Kit-Intensivprüfung / GSDB Spec Kit Intensive Review

**Zweck / Purpose**: Ausführungs- und Prüfreihenfolge für die spätere evidenz-only Implementierung. Dieses Dokument autorisiert in der Planphase keine Umsetzung oder Lieferung.

**Purpose**: Execution and verification order for the later evidence-only implementation. This document authorizes no implementation or delivery during the planning phase.

## 1. Voraussetzungen prüfen / Check prerequisites

Vom Repository-Stamm aus arbeiten. Zuerst nur lesen:

Work from the repository root. Start with read-only checks:

```bash
git branch --show-current
git rev-parse HEAD
git status --short
```

Erwartet wird der Feature-046-Branch und ein autonomer Lauf, der die aktuelle Phase autorisiert. Danach Intake, Review, Manifest und Receipt gegen `acceptedArtifacts` prüfen. Für Feature-Artefakte zuerst den `resultSha256` der neuesten abgeschlossenen Routing-Ergebnisdatei und danach deren `payloadSha256` prüfen. Der gebundene `plan-review-1`-Bericht attestiert die post-remediation Hashes seiner ausdrücklich gelisteten Planungsartefakte und hat für diese Pfade Vorrang vor `plan-1`; andere ältere Payloads derselben Datei sind nur Historie. Artefakte ohne Routing-Payload oder Review-Attestation mit aktuellem Hash inventarisieren und später durch den exakten Kandidaten-Commit binden. Ein Fehler stoppt den Lauf.

The Feature 046 branch and an autonomous run authorizing the current phase are expected. Then check intake, review, manifest, and receipt against `acceptedArtifacts`. For feature artifacts, first verify the `resultSha256` of the latest completed routing result and then its `payloadSha256`. The bound `plan-review-1` report attests the post-remediation hashes of its explicitly listed planning artifacts and takes precedence over `plan-1` for those paths; other older payloads for the same file are historical only. Inventory artifacts without a routing payload or review attestation by current hash and later bind them through the exact candidate commit. Any mismatch stops the run.

## 2. Scope-Firewall erfassen / Capture the scope firewall

Vor dem ersten Schreibvorgang eine sortierte Delivery-Set-Baseline erfassen. Zulässige spätere Änderungen sind ausschließlich:

Before the first write, capture a sorted delivery-set baseline. Allowed later changes are limited to:

- das datierte Evidenzverzeichnis unter `docs/security/secure-development/`;
- die Navigation in `docs/security/README.md`;
- ein test-only Validatorfile und kleine Fixtures im bestehenden `tests/TuiVision.Drivers.Tests`-Projekt;
- Feature-046-Evidenz, Statistik, Versionsfelder und kausal spätere Intake-/Serien-/Closeout-Dateien.

Änderungen unter `src/`, `examples/`, `.github/workflows/`, in Projekt-/Paketdateien, Provider-Einstellungen, Secrets oder historischen Quellen sind nicht erlaubt.

Changes under `src/`, `examples/`, `.github/workflows/`, project/package files, provider settings, secrets, or historical sources are prohibited.

## 3. Version vor Build oder Test / Version before build or test

Vor jedem `dotnet build` oder `dotnet test` den Buildzähler erhöhen. Alle drei Felder in `Directory.Build.props` müssen identisch sein:

Before every `dotnet build` or `dotnet test`, increment the build counter. All three fields in `Directory.Build.props` must be identical:

```text
1.46.<aktuelle Feature-Branch-Commitzahl>.<inkrementierter Buildzähler>
1.46.<current feature-branch commit count>.<incremented build counter>
```

Die Versionsänderung ist eine serialisierte gemeinsame Schreibfläche.

The version change is a serialized shared-writer operation.

Vor einem Commit ist der Patchwert die prospektive Commitzahl (`git rev-list --count HEAD` plus eins). Direkt nach dem Commit muss der Patchwert der tatsächlichen HEAD-Commitzahl entsprechen. Für den finalen Exact-Head-Lauf werden prospektiver Patch und nächster Buildzähler vor dem Kandidaten-Commit gesetzt; danach läuft der kanonische finale Test ohne weitere getrackte Änderung.

Before a commit, the patch value is the prospective commit count (`git rev-list --count HEAD` plus one). Immediately after the commit, it must equal the actual HEAD commit count. For the final exact-head run, set the prospective patch and next build counter before the candidate commit; then run the canonical final test without another tracked change.

## 4. Red/Green-Vertikalschnitt / Red/green vertical slice

Die erste Implementierung umfasst nur einen repräsentativen Schnitt: eine Quelle, `CL-01-01`, ein Sprachprofil, ein aktuelles Preset, eine Evidenzfamilie, eine Nachweisgrenze, Summary und eine Projektion.

The first implementation covers only one representative slice: one source, `CL-01-01`, one language profile, one current preset, one evidence family, one proof boundary, the summary, and one projection.

Geplante gezielte Ausführung:

Planned targeted execution:

```bash
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release --filter "FullyQualifiedName~GsdbSpecKitIntensiveReviewEvidenceTests"
```

Zuerst muss mindestens eine gezielte Regel nachvollziehbar rot sein. Danach wird nur so viel Validatorlogik ergänzt, dass der repräsentative Schnitt grün wird. Der dauerhafte Auditdatensatz wird erst nach diesem Schnitt erstellt.

At least one targeted rule must first fail demonstrably. Then add only enough validator logic to make the representative slice pass. The durable audit dataset is created only after this slice.

## 5. Inventare ableiten / Derive inventories

### Quellen / Sources

1. Alle physischen Dateien unter `docs/secure-development/` ordinal sortieren.
2. Eindeutigen Abschluss aus dem Baseline-Manifest bilden.
3. Beide Mengen vergleichen; Rollen derselben Datei zusammenführen.
4. Text als UTF-8 mit LF-Normalisierung hashen; Binärdaten roh hashen.
5. Verwaltete PDF-Prüfsumme zusätzlich gegen die SHA-Datei prüfen.

### Kontrollen / Controls

1. Nur Kontrollüberschriften der zwölf Checklisten verwenden.
2. IDs und Reihenfolge ableiten.
3. Eindeutigkeit und exakt 157 Kontrollen prüfen.
4. Kapitelzahlen aus den Kontrollzeilen berechnen und exakt gegen `12/13/15/10/13/11/12/13/17/17/12/12` prüfen.

### Sprachen / Languages

GSDB-Regeln, Verfassungs-/Preset-Profile und `git ls-files`-basierte, im Datensatz deklarierte Dateityp-/Shebang-Detektoren vereinigen. Jedes Profil als `Active`, `ReadOnlyHistorical` oder `AbsentRuleProfile` klassifizieren. Unbekannte codeartige Treffer schlagen geschlossen fehl. Historische C/C++-Bestände bleiben standardmäßig read-only `N/A`.

Union GSDB rules, constitution/preset profiles, and `git ls-files`-based file-type/shebang detectors declared in the dataset. Classify every profile as `Active`, `ReadOnlyHistorical`, or `AbsentRuleProfile`. Unknown code-like matches fail closed. Historical C/C++ trees remain read-only `N/A` by default.

### Presets und Governance / Presets and governance

Alle aktivierten Einträge direkt aus `.specify/presets/.registry` lesen. Beim Planen waren es 12; die Implementierung muss die aktuelle Zahl und Versionen neu ableiten. Agentenflächen separat aus Level-2-Verfassung, Registry-Agentenschlüsseln und tatsächlich versionierten Guidance-/Command-/Prompt-/Skill-/Agent-Pfaden schließen. Beide Verfassungen, Agentenflächen, Modell-Routing, Intake-Serie und Laufzustand getrennt prüfen.

Read every enabled entry directly from `.specify/presets/.registry`. There were 12 at planning time; implementation must rederive the current count and versions. Separately close agent surfaces from the Level-2 constitution, registry agent keys, and actually tracked guidance/command/prompt/skill/agent paths. Assess both constitutions, agent surfaces, model routing, intake series, and run state separately.

### Evidenzfamilien / Evidence families

Deklarative Pfadselektoren für den akzeptierten Pflichtdomänenkatalog und jede zusätzliche aktive Preset-/Governance-Pflicht definieren, Treffer sortieren, Dateihashes speichern und einen Aggregathash berechnen. Familien sind Suchrahmen und noch keine positive Kontrollaussage; fehlende Pflichtfamilien schlagen geschlossen fehl.

Define declarative path selectors for the accepted mandatory domain catalog and every additional active preset/governance obligation, sort matches, store file hashes, and calculate an aggregate hash. Families are search frames and not yet positive control claims; missing mandatory families fail closed.

## 6. Unabhängig bewerten / Assess independently

Für jede Kontrolle und jeden Governance-Datensatz:

For every control and governance record:

1. Aktuelle Feature-046-Evidenz lesen.
2. Genau eine Disposition wählen.
3. Deutsch-englische Begründung, Eigentümerrolle, Risiko, Revalidierungstrigger und Evidenzreferenzen erfassen.
4. Positive Aussagen nur bei aktueller, pfadgenauer Evidenz zulassen.
5. Feature 016, 044 und 045 nur als Evidenzquelle oder Strukturmuster verwenden.
6. Befunde als dokumentierte Folgehinweise festhalten; nichts reparieren.

Keine leere oder optimistische Standarddisposition verwenden. Ein unbekannter Zustand ist `Open`, nicht positiv.

Do not use an empty or optimistic default disposition. An unknown state is `Open`, not positive.

## 7. Ausgabe erzeugen / Produce output

Die kanonische Datei lautet:

The canonical file is:

```text
docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/gsdb-spec-kit-intensive-review.json
```

Danach ausschließlich daraus erzeugen:

Then generate only from it:

- `source-projection.json`
- `control-projection.json`
- `language-projection.json`
- `preset-governance-projection.json`
- `evidence-family-projection.json`
- `summary-projection.json`
- `source-inventory.md`
- `control-assessment.md`
- `language-assessment.md`
- `preset-governance-assessment.md`
- `evidence-family-assessment.md`
- `human-boundaries.md`
- `summary.md`
- `validation-evidence.md`
- `README.md`

Die JSON-Projektionen enthalten je ihren Typ, den normalisierten Hash der kanonischen Datei, die fachlich passende sortierte Teilmenge und den normalisierten Hash ihres Payloads ohne das Hashfeld selbst. Sie sind abgeleitete Ausgaben, keine zweite Pflegequelle. Alle Markdown-Dateien sind Deutsch zuerst, Englisch danach, CEFR-B2 und text-first. `docs/security/README.md` erhält den Reader-Link.

The JSON projections each contain their type, the normalized hash of the canonical file, the related sorted subset, and the normalized hash of their payload excluding that hash field itself. They are derived outputs, not a second maintenance source. All Markdown files are German first, English second, CEFR-B2, and text-first. `docs/security/README.md` receives the reader link.

## 8. Lokale Validierung / Local validation

Die genaue Befehlsfolge wird mit Exitcodes und aktuellem Commit in `pr-evidence.md` festgehalten. Vor jedem `dotnet`-Build/Test gilt erneut die Versionsregel.

The exact command sequence is recorded with exit codes and current commit in `pr-evidence.md`. The version rule applies again before every `dotnet` build/test.

```bash
xmllint --noout coverlet.runsettings
dotnet restore
dotnet test tests/TuiVision.Drivers.Tests/ --configuration Release --filter "FullyQualifiedName~GsdbSpecKitIntensiveReviewEvidenceTests"
dotnet test TuiVision.sln --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
dotnet list TuiVision.sln package --vulnerable --include-transitive
dotnet list TuiVision.sln package --deprecated --include-transitive
dotnet list TuiVision.sln package --outdated
```

Zusätzlich:

Additionally:

- alle fünf Gate-Assemblies einzeln auf mindestens 70 % Line Coverage prüfen;
- Workflow-Referenzen auf immutable SHA-Bindung prüfen;
- vollständigen Delivery-Diff auf Secrets/Credentials prüfen, ohne Trefferinhalte preiszugeben;
- Agenten-/Preset-Parität und Scope-Firewall prüfen;
- DocFX, Playwright/axe und Textbrowser-Smoke ausführen, wenn der tatsächliche Diff DocFX-Eingaben berührt.

Wenn DocFX anwendbar ist:

If DocFX applies:

```bash
docfx docfx.json
cd tests/web-a11y
npm install
npx playwright install chromium
npm run test:docfx
```

Der Textbrowser-Smoke wird mit dem im Projekt dokumentierten vorhandenen Pfad ausgeführt. Es wird kein neues Skript erstellt.

The text-browser smoke uses the existing project-documented path. No new script is created.

## 9. Exact-head-Gates / Exact-head gates

Vor einer späteren Lieferung muss der committed candidate exakt gebunden werden. Vor seinem Commit werden prospektiver Patch und nächster Buildzähler gesetzt. Danach Commit erstellen, sauberen Arbeitsbaum und Patch-zu-HEAD-Gleichheit prüfen und den kanonischen finalen Release-/Coverlet-Lauf auf genau diesem HEAD ausführen. Jedes spätere `dotnet build` oder `dotnet test` verlangt einen neuen Zähler, Kandidaten-Commit und finalen Lauf. Temporäre Gate-Evidenz wird nur als `/private/tmp/046-gsdb-spec-kit-intensive-review.premerge-gate-evidence.json` und `/private/tmp/046-gsdb-spec-kit-intensive-review.postmerge-gate-evidence.json` erzeugt und nicht als dauerhaftes Auditartefakt ausgegeben.

Before later delivery, the committed candidate must be bound exactly. Before its commit, set the prospective patch and next build counter. Commit, verify a clean tree and patch-to-HEAD equality, then run the canonical final Release/Coverlet solution command on that exact HEAD. Any later `dotnet build` or `dotnet test` requires a new counter, candidate commit, and final run. Temporary gate evidence is created only at the `/private/tmp` paths defined by the gate contract and is not emitted as durable audit evidence.

Remote-Checks zählen nur am exakten Kandidaten-HEAD. Ein Human-Approval-Bypass ist nur für genau ein nachweislich nicht verfügbares Remote-Gate nach vollständigem lokalem technischem Grün, null umsetzbaren technischen Befunden, null actionable Review-Threads, null Scope-Verstößen und Human Approval als einziger offener Regel möglich. Er benötigt Gate, autorisierte Person, Zeitpunkt, Begründung, Evidence-Grenze und Ablaufzeitpunkt. Diese Quickstart-Phase erteilt keine solche Genehmigung.

Remote checks count only at the exact candidate HEAD. A Human-Approval bypass is possible only for one demonstrably unavailable remote gate after complete local technical green status, zero actionable technical findings, zero actionable review threads, zero scope violations, and Human Approval as the sole open rule. It requires the gate, authorized person, timestamp, rationale, evidence boundary, and expiry. This quickstart grants no such approval.

## 10. MergeAndSync und Closeout / MergeAndSync and closeout

Nach exact-head Remote-Grün:

After exact-head remote green:

1. autorisierten Merge ausführen;
2. Hauptbranch synchronisieren und Post-Merge-HEAD prüfen;
3. erst jetzt Merge-Fakten, den finalen Statistikprofil-2-Stand und Retrospektive schreiben;
4. bindendes Intake mit dem vorhandenen Bash-/PowerShell-Rename-Ablauf in den Feature-046-Archivnamen verschieben;
5. akzeptierte Intake-Serie und Governance-Archive über den bestehenden Intake-Sequencing-Ablauf serialisiert fortschreiben und danach Manifest, Receipt sowie Review-Freshness validieren;
6. falls nötig einen reinen Evidence-Closeout liefern;
7. keine Review-Feststellung im Closeout beheben.

Die aktuelle `/speckit.plan`-Phase endet vor all diesen Schritten.

The current `/speckit.plan` phase ends before all these steps.

## 11. Fertigdefinition / Definition of done

- Kanonisches JSON ist vollständig, reproduzierbar und validatorgrün.
- Exakt 157 Kontrollen haben je eine vollständige Disposition.
- Ihre Kapitelverteilung ist exakt `12/13/15/10/13/11/12/13/17/17/12/12`.
- Alle Nicht-Kontroll-Inventarzahlen sind aus dem aktuellen Snapshot abgeleitet.
- Alle Projektionen stimmen bytegenau mit dem Renderer überein.
- Lokale technische Gates und anwendbare Dokumentations-/Security-Gates sind belegt.
- Externe, menschliche und Provider-Grenzen sind ehrlich getrennt.
- Keine verbotene Datei oder Wirkung ist im Delivery-Set.
- Exact-head Lieferung, MergeAndSync und kausaler Closeout sind später vollständig belegt.

- Canonical JSON is complete, reproducible, and validator-green.
- Exactly 157 controls each have one complete disposition.
- Their chapter partition is exactly `12/13/15/10/13/11/12/13/17/17/12/12`.
- Every non-control inventory count is derived from the current snapshot.
- Every projection matches the renderer byte-for-byte.
- Local technical gates and applicable documentation/security gates are evidenced.
- External, human, and provider boundaries are honestly separated.
- No prohibited file or effect is in the delivery set.
- Exact-head delivery, MergeAndSync, and causal closeout are later fully evidenced.
