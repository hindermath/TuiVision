# Implementierungsplan: Beispielportfolio-Konformitätsaudit / Implementation Plan: Example Portfolio Conformance Audit

**Branch**: `038-example-portfolio-conformance-audit` | **Datum / Date**: 2026-08-09 | **Spezifikation / Spec**: [spec.md](spec.md)

**Eingabe / Input**: Bindender Intake `requirements/intakes/active/Lastenheft_15_Post-Wave6-Example-Portfolio-Conformance-Audit.md`

**Liefermodus / Delivery mode**: `MergeAndSync` für den ausdrücklich autorisierten Resume; der read-only Produktscope bleibt unverändert.

**Plan-Phasengrenze / Plan phase boundary**: Planung vollständig; `tasks.md`, Implementierung, Build, Test und Remote-Lieferung bleiben ungestartet.

## Zusammenfassung / Summary

Feature 038 erstellt einen read-only Konformitätsaudit für exakt 37 gelieferte
Portfolioeinträge. Ein kanonischer JSON-Datensatz hält Inventar, Quellen,
Evidence, Dimensionsentscheidungen, Findings, Owner-DAG, Handoff, Governance
und Gate-Status zusammen. Bilinguale Markdown-Sichten machen dieselben Fakten
text-first prüfbar. Ein späterer, deterministischer MSTest-Validator prüft nur
die Integrität dieser Auditdaten und kontrollierte fehlerhafte Fixtures; er
ändert weder Produktcode noch Beispiele.

*Feature 038 creates a read-only conformance audit for exactly 37 delivered
portfolio entries. One canonical JSON dataset joins inventory, sources,
evidence, dimension decisions, findings, owner DAG, handoff, governance, and
gate status. Bilingual Markdown views make the same facts reviewable in a
text-first form. A later deterministic MSTest validator checks only audit-data
integrity and controlled malformed fixtures; it changes neither product code
nor examples.*

Die Arbeit beginnt evidence-first. Danach beweist `EX036 Tp7FileManager` als
repräsentativer Vertikalschnitt das vollständige Relations- und
Entscheidungsmodell. Erst wenn dieser Schnitt rot/grün nachvollziehbar ist,
werden die übrigen 36 Zeilen geprüft. Reproduzierbare Lücken werden nach ihrer
Ursache dedupliziert, bei `EF001` lückenlos nummeriert und genau einem Primary
Owner zugeordnet. Nur nicht leere Owner-Gruppen erzeugen unnummerierte
Remediation-Lastenhefte; anschließend folgt genau ein unabhängiger Closure.

*Work starts evidence-first. `EX036 Tp7FileManager` then proves the complete
relation and decision model as the representative vertical slice. The other 36
rows are reviewed only after this slice has a traceable red/green result.
Reproducible gaps are deduplicated by root cause, numbered contiguously from
`EF001`, and assigned to exactly one primary owner. Only non-empty owner groups
produce unnumbered remediation intakes; exactly one independent closure follows.*

## Technischer Kontext / Technical Context

| Aspekt / Concern | Festlegung / Decision |
|---|---|
| Sprache und Laufzeit / Language and runtime | .NET 10, C# 14 beziehungsweise `LangVersion=latest`; C# wird nur für den test-only Validator verwendet. |
| Primäre Abhängigkeiten / Primary dependencies | Vorhandenes MSTest 4.3.2 und `System.Text.Json`; keine neue NuGet-, Runtime- oder Tool-Abhängigkeit. |
| Speicherung / Storage | Repository-lokale UTF-8-JSON- und Markdown-Dateien; keine Datenbank, kein Dienst, keine beliebigen Nutzerdaten. |
| Testprojekt / Test project | `tests/TuiVision.Examples.SmokeTests/`; neuer Auditvalidator und Fixtures ohne Änderung der Projektdatei. |
| Zielplattform / Target platform | Plattformneutrale Auditdaten; lokale Ausführung auf macOS. Windows, Linux und WSL werden als bestehende Evidence oder ehrliche Grenze bewertet, nicht in diesem lokalen Lauf ferngesteuert. |
| Projekttyp / Project type | Read-only Evidence- und Governance-Audit eines .NET-Terminal-UI-Frameworks. |
| Leistungsziel / Performance goal | Validatorlauf deterministisch und ohne Netzwerk; lineare Prüfung der 37 Zeilen und ihrer Relationen. Kein Laufzeit-Performanceziel für das Produkt. |
| Grenzen / Constraints | Exakt 37 direkte Beispielprojekte; `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/`, Public API und Dependencies bleiben unverändert. |
| Umfang / Scale | 25 historische Waves-1–4-Beispiele, 10 Wave-5-Beispiele, 1 Wave-6-Beispiel und 1 `SupplementalControl`; neun fachliche Markdown-Projektionen plus `pr-evidence.md` ergeben exakt zehn Markdown-Evidence-Familien; 0–n `EF001+`-Findings. |
| Ausgangs-HEAD / Planning baseline | `01c4759ca9883b78914affecfd8cfb224789654b`; die sortierte Liste der 37 direkten `examples/*/*.csproj`-Pfade hat SHA-256 `cb2f6568b70f2a62cd529250777e849dd2cd026c05732df81733b2fc3d177333`. |
| Externe Quellen / External sources | Nur bereits akzeptierte lokale Pins und Evidence aus Features 024, 029 und 030; kein Zugriff auf bewegliche Upstreams. |

## Verfassungsprüfung vor Phase 0 / Constitution Check before Phase 0

| Gate | Planentscheidung / Plan decision | Status |
|---|---|---|
| Branch und PR-Fluss | Der vorhandene Branch ist bindend. `MergeAndSync` erlaubt Commit, Push, Feature-PR, Review-Konvergenz, Merge, Branchbereinigung und `main`-Synchronisierung. Der begrenzte Bypass gilt nur bei grünen technischen Gates, null umsetzbaren Threads und ausschließlich offener Human Approval. | Pass |
| Level-2-Umgebung | Die TuiVision-Registerzeile bindet .NET 10/C#, MSTest, Coverlet, DocFX, Playwright/Axe, UTF-8-Lynx, text-first A11Y, Statistikprofil 2 und alle gepflegten Agentenflächen. | Pass |
| Toolchain | Produkt- und Testbaseline bleibt .NET 10/C# 14. Keine Projekt- oder Paketdatei wird geändert. | Pass |
| Memory-safe language | C# steht auf der MSL-Allowlist. Der test-only Parser validiert alle JSON-Eingaben fail-closed; MSL ersetzt diese Validierung nicht. | Pass |
| Secure code generation | Der Validator verwendet `System.Text.Json`, geschlossene Wertebereiche, Längen-/Duplikat-/Relationsprüfungen, kontrollierte Pfade und verständliche Fehler. Relevante CWE-Linsen sind CWE-20, CWE-22, CWE-400, CWE-502 und CWE-703. | Pass |
| Architekturgrenzen | Das Produkt und seine Schichten bleiben unverändert. Auditdataset, Markdown-Projektionen und test-only Validator bilden getrennte Verantwortungen; Source- und Evidence-IDs sind stabile Schnittstellen. | Pass |
| iSAQB/arc42 | Framework-Reuse, Qualitätsrisiken und Technical-Debt-Handoffs sind anwendbar und werden feature-lokal dokumentiert. Neue ADRs oder Dateien unter `docs/architecture/` sind `N/A`, solange kein `ProductDecision` oder architektonisch signifikanter Fund entsteht. | Pass |
| Sichere Architektur | Trust Boundary, Deployment, Authentisierung, Autorisierung, Kryptografie und Cloud ändern sich nicht. STRIDE/CIA/CAPEC-, S-ADR-, arc42-Security-, Zero-Trust-, C3A- und C5-Updates sind `N/A` mit Trigger bei einem tatsächlichen Grenz- oder Produktdelta. | Pass |
| Security-Standards | NIST SSDF und CWE Top 25 sind `Applicable` für Evidence-Integrität, Scope-Schutz und fail-closed Validierung. ASVS, SBOM, VEX, SLSA, OpenSSF, NIS2, CRA, EU AI Act und DORA sind für den reinen Audit `N/A`; jeder Trigger wird in Governance-Evidence festgehalten. | Pass |
| AI-SBOM | AI ist ausschließlich Entwicklungs-/Agentenwerkzeug. Kein Modell, Datensatz, Dienst oder Inferenzbaustein wird ausgeliefert; `N/A` bis Runtime- oder Produkt-AI in Scope tritt. | Pass |
| Supply Chain | Keine Dependency, kein Paket und keine Pipeline ändert sich. Bestehende Dependabot-/Supply-Chain-Evidence wird nur statusgeprüft; keine neue SBOM/VEX/SLSA-Erzeugung wird behauptet. | Pass |
| Security-First | Keine Credentials, Agent-History, Logs, SQLite-Zustände oder externe Checkouts werden verfolgt. Secret- und Protected-Root-Scans bleiben Abschlussgates. | Pass |
| Red-Green-Refactor | Zuerst existieren Evidence-Skelett und vollständige Kompilationsoberfläche. Danach scheitert der fokussierte `EX036`-Akzeptanztest kontrolliert an der fehlenden Zeile, wird mit dem vollständigen Slice grün und erst dann auf 37 Zeilen verbreitert. | Pass |
| Coverage | Targeted Validator, vollständige Release-Suite und das kanonische Coverlet-Gate mit mindestens 70 % je Pflichtassembly, Ziel 80 %, sind nach der Implementierung Pflicht. | Pass |
| Dependencies und Pinning | Keine NuGet-Änderung. Free Vision, Terminal.GUI v1.9.0 und magiblot/tvision bleiben exakt an die akzeptierten Feature-024/029/030-Pins gebunden. | Pass |
| Serialisierung | `System.Text.Json`, UTF-8, geschlossene Enums, explizite Schema-Version, deterministische Ordinalsortierung und atomare Ablehnung fehlerhafter Daten. | Pass |
| Inclusion/A11Y | Alle neuen Markdown-Sichten sind semantisch, text-first, tastatur-/Screenreader-/Braille-/Textbrowser-tauglich und verlassen sich nicht auf Farbe oder Layout. | Pass |
| Bilinguale Lieferung | Lern- und Reviewtexte sind Deutsch zuerst, Englisch direkt danach, ungefähr CEFR B2; technische und Spec-Kit-Begriffe werden bei Erstnutzung erklärt. | Pass |
| XML/DocFX | Keine Public API und keine XML-Dokumentation ändert sich. Die verpflichtende Statistikänderung unter `docs/` löst dennoch DocFX, Playwright/Axe und UTF-8-Lynx aus. | Pass |
| Didaktische Kommentare | Produktkommentare sind `N/A`. Der neue nicht triviale Validator erhält nur dort moderate DE-first/EN-second Kommentare, wo Relations-, Dedup- oder Fail-Closed-Grenzen sonst unklar wären. | Pass |
| Cross-Platform-Skripte | Es entsteht kein Skript. Bash/PowerShell-Paar, Manpage, Cmdlet, Help und Paritätsartefakt sind `N/A`; Trigger ist ein script-shaped Diff. | Pass |
| Agentenparität | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`, Templates und Constitution bleiben `NoUpdateRequired`. Ein gemeinsamer Regel- oder Statusfund löst eine atomare Neubewertung aus. | Pass |
| Statistik | `docs/project-statistics.md` wird erst am abgeschlossenen Implementierungsmeilenstein mit Profil 2, 80 Zeilen/Arbeitstag manuell und 125 Zeilen/Arbeitstag Thorsten-Solo aktualisiert. | Pass |
| Dokumentationsauswirkung | Genau eine Entscheidung: `UpdateRequired`. Feature-lokale Audit-Evidence und Projektstatistik ändern sich; Beispiel-Guides werden nur als Findings benannt, nicht sofort korrigiert. | Pass |

Es besteht keine Verfassungsverletzung und keine Ausnahme ist nötig.

*There is no constitution violation and no exception is required.*

## Projektstruktur / Project Structure

### Planungs- und spätere Evidence-Artefakte / Planning and later evidence artefacts

```text
specs/038-example-portfolio-conformance-audit/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── autonomous-gate-requirements.json
├── contracts/
│   ├── example-portfolio-audit-acceptance.md
│   └── example-portfolio-integrity-validator.md
├── checklists/
│   ├── requirements.md
│   ├── portfolio-audit.md
│   └── plan-quality.md
├── example-portfolio-audit.json                 # später: kanonischer Datensatz
├── example-portfolio-source-manifest.md         # später: lesbare Quellenprojektion
├── example-portfolio-inventory.md               # später: exakte 37 Zeilen
├── example-conformance-matrix.md                 # später: Dimensionsentscheidungen
├── example-framework-usage-review.md             # später: Reuse/Abweichung/Finding
├── example-proof-and-platform-review.md          # später: Real-Path und Plattformen
├── example-learning-a11y-review.md               # später: Guides, Lernen und A11Y
├── example-portfolio-findings.md                 # später: EF001+ oder explizit leer
├── example-remediation-handoff.md                # später: nicht leere Gruppen + Closure
├── example-portfolio-gate.md                     # später: Auditstatus ohne Vorab-Closure
└── pr-evidence.md                                # später: lokale Evidence, kein PR-Claim

tests/TuiVision.Examples.SmokeTests/
├── ExamplePortfolioAuditIntegrityTests.cs        # später: test-only Validator
└── Fixtures/ExamplePortfolioAudit/
    ├── valid-vertical-slice.json
    └── malformed-*.json                          # später: kontrollierte Negativfälle

requirements/intakes/active/
├── Lastenheft_Example-Portfolio-<Owner>-Remediation.md  # später, nur je nicht leerer Gruppe
└── Lastenheft_Example-Portfolio-Closure.md              # später, exakt einmal

docs/
└── project-statistics.md                         # später: Abschlussmeilenstein
```

`tasks.md` ist ein Artefakt der nächsten `/speckit.tasks`-Phase und wird durch
diesen Planlauf nicht erstellt.

*`tasks.md` belongs to the later `/speckit.tasks` phase and is not created by
this planning run.*

### Read-only Produkt- und Quellenstruktur / Read-only product and source structure

```text
src/                         # geschützt / protected
examples/                    # 37 direkte Portfolio-Projekte; geschützt
tv203s/                      # historische Waves 1-4; read-only
TVDEMOS/                     # historische Wave 5; read-only
TVFM/                        # historische Wave 6; read-only
docs/guides/examples/        # zu auditieren; Korrekturen nur als Finding
tests/TuiVision.Examples.SmokeTests/  # vorhandene Real-Path-Evidence; nur Validator neu
specs/024-*/                 # akzeptierte Free-Vision-Evidence; read-only
specs/029-*/                 # akzeptierte Terminal.GUI-Evidence; read-only
specs/030-*/                 # akzeptierte magiblot-Evidence; read-only
specs/037-*/                 # bindende Wave-6-Closure-Evidence; read-only
```

**Strukturentscheidung / Structure decision**: Der kanonische Datensatz bleibt
eine Datei, damit Relationen atomar validiert werden. Neun fachliche
Markdown-Projektionen plus `pr-evidence.md` ergeben exakt zehn vollständige,
reviewbare Markdown-Evidence-Familien und keine zweite Wahrheitsquelle. Der Validator liegt im bestehenden Evidence-owning
Smoke-Testprojekt; es entsteht kein neues Projekt und keine Projekt-Referenz.

*The canonical dataset remains one file so relations can be validated
atomically. Nine domain Markdown projections plus `pr-evidence.md` form exactly
ten complete reviewable Markdown evidence families, not a second source of truth. The validator lives in the existing
evidence-owning smoke-test project; no new project or project reference is added.*

## Quellen- und Evidence-Architektur / Source and Evidence Architecture

Die bindende Hierarchie bleibt unverändert: TV203/Borland beziehungsweise die
passenden TP7-/TVFM-Quellen bestimmen historische Absicht; akzeptierte
TuiVision-Verträge und beobachtbares Verhalten bestimmen Produktsemantik; die
drei gepinnten Vergleichsprojekte sind nur sekundäre Meinungen.

*The binding hierarchy remains unchanged: TV203/Borland or the matching
TP7/TVFM sources define historical intent; accepted TuiVision contracts and
observable behavior define product semantics; the three pinned comparison
projects are secondary opinions only.*

- Feature 024: Free Vision Commit
  `ffc03b34d8cafb85ddcf0686de1c5551601dacb2`, bestehende IDs `FV001`–`FV015`.
- Feature 029: Terminal.GUI Tag `v1.9.0`, Tag-Objekt
  `4b812e44798f2c7567afec50ba9a9293b6beb6de`, Commit
  `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`, bestehende IDs
  `TGSR001`–`TGSR025`.
- Feature 030: magiblot/tvision Commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693`, Tree
  `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, bestehende IDs
  `MBSR001`–`MBSR050`.
- Feature 037: `wave6State=Closed`, `portfolioAuditState=Eligible`, null
  Candidate Findings und null Product Decisions; diese Evidence ist die
  Eintrittsgrenze für `EX036`.

Historische neue Manifest-IDs werden je Authority-Präfix und lexikografisch
sortiert vergeben: `TV203-E001+`, `TVDEMOS-E001+`, `TVFM-E001+`. Unveränderliche
akzeptierte TuiVision-Vorgängerevidence erhält nach normalisiertem relativem
Pfad ordinal `BASE-E001+`. Bereits akzeptierte `FV*`, `TGSR*` und `MBSR*`
werden nicht umnummeriert. Jede Quelle trägt Pfad, Hash, Authority, Rolle,
No-Copy-Grenze und rückwärtige `ExampleIds`. Jede Portfoliozeile nennt die
vorwärts gerichteten IDs. Evidence-IDs verwenden `EVD001+` und verbinden Pfad,
Evidence-Klasse und alle referenzierenden Beispiele. Der Validator verlangt
die jeweilige Gegenrelation.

*New historical manifest IDs are assigned per authority prefix and ordinally
sorted path: `TV203-E001+`, `TVDEMOS-E001+`, and `TVFM-E001+`. Immutable
accepted TuiVision predecessor evidence uses ordinal `BASE-E001+` IDs sorted by
normalized relative path. Existing `FV*`, `TGSR*`, and `MBSR*` IDs are never
renumbered. Every source records path, hash, authority, role, no-copy boundary,
and reverse `ExampleIds`; every portfolio row records the forward IDs. Evidence
IDs use `EVD001+` and connect paths, evidence classes, and all referencing
examples. The validator requires each reverse relation.*

Die exakte Zuordnung der 37 `ExampleId`-Werte, Waves, Rollen, Entry-Points,
Guides und akzeptierten Evidence-Basen steht im
[Abnahmevertrag](contracts/example-portfolio-audit-acceptance.md). Änderungen
an der direkten Projektmenge blockieren als Portfolio-Drift.

## Implementierungsstrategie / Implementation Strategy

### Phase 0 – Evidence zuerst / Evidence first

1. Erzeuge `pr-evidence.md`, einen parsefähigen, absichtlich noch nicht
   abnahmegültigen kanonischen `NotAssessed`-Skelettdatensatz, alle benannten
   Markdown-Evidence-Sichten und das bereits geplante
   `autonomous-gate-requirements.json`, bevor der Validator geändert wird.
2. Halte Authority, Baseline-HEAD, vier akzeptierte Intake-Hashes, Pin-Hashes,
   Protected Roots, aktuelle Projektmenge und `Not Assessed`-Gatezustände fest.
3. Prüfe die vollständige spätere Kompilations-/Ausführungsoberfläche, bevor
   ein erwarteter Red-Lauf gestartet wird. Vor jedem dafür nötigen
   `dotnet build` oder `dotnet test` gilt die Build-Counter-Regel.

*Create evidence and gate contracts before validator edits. The initial
`NotAssessed` dataset is parseable but intentionally not acceptance-valid until
all 37 rows and relations are complete. Record authority, stable inputs,
protected roots, and honest `Not Assessed` states. Confirm the complete
compile/execution surface before the first expected-red command.*

### Phase 1 – Repräsentativer Vertikalschnitt / Representative vertical slice

1. Lege einen fokussierten Akzeptanztest für `EX036 Tp7FileManager` an. Der
   erste Lauf scheitert erwartet, weil die vollständige Zeile und ihre
   Gegenrelationen noch fehlen.
2. Ergänze ausschließlich den vollständigen `EX036`-Slice: TVFM-Quellen,
   Feature-037-Evidence, Entry-Point, Guide, Smokes, Framework-Entscheidung,
   zehn Dimensionsrelationen, Disposition, Review, Risiko und Trigger.
3. Beweise Grün für Slice, Quell-/Evidence-Reziprozität, kontrollierte
   Dateipfade, app-loop/state/view/cell-Proof, A11Y und Plattformfallback.
4. Stoppe bei Finding, `ProductDecision`, unklarer Quelle oder Owner; behebe
   kein Produktverhalten in Feature 038.

### Phase 2 – Validator und fehlerhafte Fixtures / Validator and malformed fixtures

Implementiere einen deterministischen, internen Validator mit
`System.Text.Json`. Er akzeptiert einen expliziten Repository-Root, liest nur
kontrollierte Feature-/Fixture-Pfade, prüft Exit-/Fehlerdiagnostik textuell und
verwendet keine Netzwerk-, Zeit-, Locale- oder Zufallsabhängigkeit. Die
vollständige Fixture-Liste und Fehlerklassen stehen im
[Validatorvertrag](contracts/example-portfolio-integrity-validator.md).

*Implement a deterministic internal validator using `System.Text.Json`. It
accepts an explicit repository root, reads only controlled feature/fixture
paths, produces textual diagnostics, and has no network, time, locale, or
random dependency.*

### Phase 3 – Breiter 37-Zeilen-Review / Broad 37-row review

1. Verbreitere nach Wave: Wave 1, Wave 2, Wave 3, Wave 4, Wave 5, danach
   `EX037 A11yFramework`.
2. Prüfe jede Zeile gegen historische Absicht, akzeptierte TuiVision-Semantik,
   Framework-Reuse, sichtbare Interaktion, Real-Path-Proof, Guide/Lernziel,
   A11Y und Plattformgrenzen.
3. Verwende Vergleichsrelationen nur bei fachlich vergleichbarer Verantwortung;
   andernfalls `N/A` mit Begründung und Trigger.
4. Halte Unterschiede in Optik, API-Form, Vererbung, Layout, Speicher oder
   Quelltext ohne reproduzierbare TuiVision-Lücke ausdrücklich als Nicht-Fund fest.
5. Führe alle Vorwärts-/Rückwärtsrelationen und Markdown-Projektionen atomar
   aus dem kanonischen Datensatz nach.

### Phase 4 – Findings, Deduplizierung und Owner-DAG / Findings, deduplication, and owner DAG

1. Sammle nur reproduzierbare Gap-Beobachtungen. Der kontrollierte
   `DeduplicationKey` hat das Format
   `<primary-owner>:<dimension>:<root-cause-slug>` in Kleinbuchstaben und
   ASCII-Kebab-Case; Freitext ist nicht Teil des Schlüssels.
2. Gruppiere zuerst nach Ursache. Sortiere danach nach der festen Owner-Reihenfolge
   `FrameworkReuse`, `BehaviorInteraction`, `ProofPlatform`, `LearningA11Y`
   und innerhalb der Gruppe ordinal nach `DeduplicationKey`.
3. Vergib bei Audit-Freeze lückenlos `EF001+`. Alle betroffenen `ExampleIds`
   werden ordinal sortiert; jede Zeile und jedes Finding besitzen reziproke IDs.
4. Weise genau einen Primary Owner zu. Cross-Cutting-Folgen stehen nur in
   `SecondaryImpacts`. Unklare Ownership erzeugt einen fail-closed Stop.
5. Verlange für jedes Finding Reproduktion, kontrollierten Red-Proof,
   Real-Path-Green-Anforderung, Risiko, Abhängigkeiten, Owner, Reviewer,
   Restrisiko und Re-Evaluationsauslöser.
6. `Dependencies` eines Findings A nennen die Finding-IDs, die A voraussetzt.
   Liegen A und eine Voraussetzung B bei verschiedenen Primary Ownern, entsteht
   genau die Owner-Kante `Owner(B) -> Owner(A)`; gleiche Owner bleiben intern,
   doppelte Kanten werden kollabiert.

### Phase 5 – Nicht leerer Handoff und unabhängiger Closure / Non-empty handoff and independent closure

1. Eine Owner-Gruppe ist nur mit mindestens einem finalen deduplizierten
   Finding nicht leer. Nur dann entsteht genau ein unnummeriertes
   `Lastenheft_Example-Portfolio-<Owner>-Remediation.md`.
2. Ordne diese 0 bis 4 Remediation-Knoten topologisch und lehne Zyklen ab.
   Bei mehreren gleichzeitig freien Knoten gilt als deterministischer
   Tie-Breaker die feste Owner-Reihenfolge aus Phase 4. Leere Gruppen erzeugen
   keine Datei, keinen Branch und kein Feature.
3. Der Handoff selbst ist nie leer: Er dokumentiert die vier Gruppen als
   `Emitted` oder `Suppressed`, ihre Finding-IDs und anschließend genau einen
   `Lastenheft_Example-Portfolio-Closure.md`.
4. Der Closure hängt von allen tatsächlich emittierten Remediation-Intakes ab
   und steht immer zuletzt. Er erhält keine Feature-Nummer und wird nicht gestartet.
5. Das Feature-038-Gate darf `AuditCompleteWithRemediation` oder
   `AuditCompleteNoFindings` aussagen, niemals bereits vollständige
   Portfolio-Konformität oder Lernreife.

### Phase 6 – Governance und lokale Validierung / Governance and local validation

1. Schließe jede der zwölf Preset- und Standardsentscheidungen mit getrennten
   Feldern für Applicability und Umsetzung, Begründung, Evidence, Owner,
   Reviewer, Restrisiko, Trigger und Follow-up.
2. Aktualisiere `docs/project-statistics.md` erst nach abgeschlossenem
   Implementierungsmeilenstein. Bewerte Agentenparität, Security- und
   Architektur-Evidence triggerbasiert.
3. Führe die Gates in der bindenden Reihenfolge Intake/State, Portfolio,
   Relationen, Findings, Handoff, Diff/Scope/API/Dependency/Security,
   Preset/Routing, Format, targeted Validator, Beispiel-Smokes, vollständige
   Release-Suite, Coverage, Statistik, Dokumentation/A11Y, abschließende
   Governance-/Generated-Output-Prüfung und Exact-Head-Delivery aus.
4. Remote Exact-Head, Review, Merge und `main`-Synchronisierung sind für den
   aktuellen Resume anwendbar. Ein kausaler evidence-only Closeout wird nur
   bei einer echten Post-Merge-Evidence-Lücke verwendet.

## Test- und Validierungsdesign / Test and Validation Design

Die spätere Validierung ist proportional, obwohl das bindende Repository-Gate
vollständig bleibt. Erst laufen die fokussierten Datenintegritätstests, dann
die bestehende Beispiel-Smoke-Suite, die vollständige Release-Suite und das
Coverage-Gate. Kein Build oder Test wird in der Planphase ausgeführt.

*Later validation is proportional while retaining the binding full repository
gate. Focused data-integrity tests run first, followed by the existing example
smokes, full Release suite, and coverage gate. No build or test runs during the
planning phase.*

| Trigger | Späterer stabiler Command-Token / Later stable command token | Grenze / Boundary |
|---|---|---|
| Plan-/Intake-Konsistenz | `specify check` und `.specify/scripts/bash/check-prerequisites.sh --json` | Erst nach Plan- und späterer Tasks-Konvergenz; kein offener High-Fund. |
| Diff und Scope | `git diff --check` plus exakter Pfad-/API-/Dependency-Scan | Null Diff in geschützten Wurzeln und null Produktdelta. |
| Security und Governance-Preflight | Secret-, Dependency-, Preset- und Routing-Checks | Aktuelles Ergebnis oder begründetes `N/A` mit Trigger. |
| Format | `dotnet format --verify-no-changes` | Keine Formatabweichung. |
| Targeted Validator | `dotnet test tests/TuiVision.Examples.SmokeTests/... --configuration Release --filter FullyQualifiedName~ExamplePortfolioAuditIntegrityTests` | Positive Invarianten und exakt 46 kanonische Negative Fixtures. |
| Beispiel-Smokes | `dotnet test tests/TuiVision.Examples.SmokeTests/... --configuration Release` | Bestehende Real-Path-Evidence bleibt grün. |
| Vollständige Regression | `dotnet test TuiVision.sln --configuration Release` | Alle Repositorytests. |
| Coverage | `dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings` | Jede der fünf Pflichtassemblies mindestens 70 %, Ziel 80 %. |
| Statistik | Profil-2-Aktualisierung | Erst nach grünem lokalen Implementierungsmeilenstein. |
| DocFX | `docfx docfx.json` | Ausgelöst durch `docs/project-statistics.md`; 0 Warnungen/Fehler. |
| A11Y HTML | `npm run test:docfx` unter `tests/web-a11y` | Playwright/Axe nach DocFX. |
| Textbrowser | UTF-8-Lynx-Prüfung der geänderten publizierten Seiten | Keine Ersatzzeichen; verständlicher Textpfad. |
| Final Governance | Agent-Paritäts-, Generated-Output- und Abschluss-Scans | Vollständige lokale Candidate-Evidence. |
| Exact-head Delivery | Providerneutrale Gate-Evidence, PR-Checks, Reviews, Merge und Sync | Exakter geprüfter Head; danach sauberes `main == origin/main`. |

Vor jedem einzelnen späteren `dotnet build` oder `dotnet test` wird genau
einmal der Build-Anteil in `Directory.Build.props` erhöht und `Version`,
`AssemblyVersion` und `FileVersion` werden auf
`1.38.<aktueller-Branch-Commit-Count>.<Build>` ausgerichtet. Ein `dotnet test`,
das implizit baut, erhält nur einen Counter-Schritt. Restore, Format, DocFX,
NPM und reine Dateiscans erhöhen den Counter nicht. In dieser Planphase findet
keine Versionsänderung statt.

## Autonomous Execution Contract / Vertrag für autonome Ausführung

### Autorität und Preflight / Authority and preflight

- Autorität gilt nur für Feature 038 im Modus `MergeAndSync`; sie umfasst die
  Delivery-Aktionen, aber weder Provider-Administration noch ein Folgefeature.
- Die vier im Run-State hashgebundenen Intake-/Review-/Serienartefakte müssen
  vor Tasks, Implementierung und nach jeder Unterbrechung erneut hashgleich sein.
- Feature 037 muss weiterhin `Closed`/`Eligible` sowie null Candidate Findings
  und null Product Decisions belegen.
- Routing-, Scope-, Governance- oder Artefaktdrift blockiert fail-closed.

### Evidence-first und Vertikalschnitt / Evidence first and vertical slice

- `pr-evidence.md`, Dataset-Skelett, neun fachliche Markdown-Projektionen und Gate-Anforderungen
  existieren vor dem ersten Validator-Edit.
- `EX036` ist der einzige erste Slice. Sein fokussiertes Rot muss ausschließlich
  die fehlende Auditzeile melden; Kompilations-, Restore- oder Fremdfehler sind
  kein akzeptiertes Red.
- Der Slice ist erst grün, wenn Zustand, View, Fokus/Interaktion, Status,
  Description, Cells, kontrollierte Dateigrenze, Guide, A11Y, Plattform und
  alle Gegenrelationen vollständig sind.

### Konvergenzgates / Convergence gates

- Clarify und beide Requirements-Checklists sind bereits vollständig.
- Plan endet nur mit vollständigen Artefakten und `plan-quality.md=PASS`.
- Plan Review, Tasks und Analyze bleiben getrennte spätere Phasen; kein Gate
  wird vorweggenommen.
- Implementierung endet bei 37/37 Zeilen, vollständigen Relationen, finaler
  Deduplizierung, azyklischem Handoff und null undisponierten Findings.
- Lokale Validierung endet nur mit allen anwendbaren lokalen Gates grün.
- Delivery endet erst nach grünen Remote-Checks, null umsetzbaren Threads,
  validierter Exact-Head-Evidence, Merge und sauberem synchronem `main`.
- Ein kausaler Closeout ist nur für notwendige Post-Merge-Fakten zulässig.

### Single Writer und Shared Files / Single writer and shared files

Die folgenden Flächen werden später streng seriell geschrieben:

1. `example-portfolio-audit.json` und alle daraus abgeleiteten Markdown-Sichten;
2. `Directory.Build.props` vor jedem Build/Test;
3. `autonomous-run-state.json` nur an validierten Phasengrenzen;
4. `docs/project-statistics.md` genau am Abschlussmeilenstein;
5. spätere Remediation-/Closure-Intakes nach finalem Finding-Freeze;
6. Agenten- und Governanceflächen nur bei einem ausgelösten, ausdrücklich
   dokumentierten Paritätstrigger.

Parallel-Autonomie ist nicht autorisiert. Es gibt daher keinen Multi-Writer-
oder Konsolidierungsplan.

### Scope Firewall / Scope-Firewall

Erlaubte spätere Schreibflächen sind Feature-038-Artefakte, der eine test-only
Validator samt Fixtures, die bei nicht leeren Gruppen erzeugten Intake-Dateien,
der eine Closure-Intake, die erforderliche Versionszeile und die abschließende
Projektstatistik. Verboten sind Änderungen an `src/`, `examples/`, `tv203s/`,
`TVDEMOS/`, `TVFM/`, Public API, Projekt-/Paketdateien, Dependencies,
externen Checkouts und generiertem DocFX-Output. Jede Verletzung stoppt.

### Stop, Resume und Retrospektive / Stop, resume, and retrospective

- Nächste sichere Stop-Grenzen sind Ende Plan, Ende Vertikalschnitt, Ende jeder
  Wave-Gruppe, Finding-Freeze, Handoff-Freeze und Ende lokaler Validierung.
- `PausedByUser` braucht explizites Resume. Eine unbekannte oder unterbrochene
  Operation setzt `NeedsRevalidation`; sie wird nicht still fortgesetzt.
- Resume prüft Hashes, HEAD, Diff, Scope, Routing, Build-Counter und den letzten
  beweisbaren Gatezustand, bevor eine Operation wiederholt oder verworfen wird.
- `ProductDecision`, nicht reproduzierbares Finding, unklare Ownership,
  Portfolio-Drift oder nicht behebbarer Evidence-/Security-Fehler stoppt hart.
- Die spätere Retrospektive darf nur reproduzierbares providerneutrales Lernen
  als `PresetFollowUp` nennen. `NoPromotion` erzeugt nichts extern.

## Governance-Evidence / Governance Evidence

`example-portfolio-gate.md` und `pr-evidence.md` führen jeden Checkpoint mit
`Applicability`, `Implementation`, `Rationale`, `EvidencePath`, `Owner`,
`Reviewer`, `ResidualRisk`, `ReevaluationTrigger` und `FollowUp`. `N/A` bleibt
bei `Not Assessed`; `Open` braucht Owner und konkreten Folgepunkt. Neue Dateien
unter `docs/security/`, `docs/architecture/` oder `docs/accessibility/` werden
nur durch einen tatsächlichen Fund oder eine geänderte Grenze ausgelöst.

*The feature-local gate and run evidence record every checkpoint with separate
applicability and implementation status plus rationale, evidence, ownership,
risk, trigger, and follow-up. General governance documents change only when an
actual finding or changed boundary triggers them.*

## Dokumentation und A11Y-Trigger / Documentation and A11Y Triggers

- Immer: semantische Überschriften, korrekte Tabellen, vollständige Link-/Pfad-
  relationen, Deutsch zuerst/Englisch danach, CEFR-B2, Textalternative für
  Status/Entscheidungen, korrekte Umlaute/`ß`, getaggte Codeblöcke und UTF-8.
- Immer je Portfoliozeile: Guide-, Tastatur-, Screenreader-, Braille-,
  Textbrowser-, High-Contrast-, Small-Terminal- und Plattformentscheidung oder
  begründetes `N/A`.
- Ausgelöst: `docs/project-statistics.md` bewirkt DocFX, Playwright/Axe und
  UTF-8-Lynx. Navigation wird nur geändert, wenn neue publizierte Seiten sonst
  nicht auffindbar wären.
- Nicht ausgelöst: XML/Public API, CLI-Hilfe, Screenshots/Bild-Alttexte,
  Agenten-Guidance und Script-Dokumentation, solange deren Diff leer bleibt.

## Komplexitätsverfolgung / Complexity Tracking

Keine Verfassungsverletzung ist zu rechtfertigen. Der einzige zusätzliche Code
ist ein interner Validator im bestehenden Testprojekt. Ein einzelner kanonischer
JSON-Datensatz und abgeleitete Markdown-Sichten sind einfacher als mehrere
unabhängige strukturierte Wahrheitsquellen.

*No constitution violation requires justification. The only added code is an
internal validator in the existing test project. One canonical JSON dataset and
derived Markdown views are simpler than multiple independent structured sources.*

## Verfassungsprüfung nach Phase 1 / Post-design Constitution Re-check

Die Entwurfsartefakte verändern keine Runtime-, API-, Dependency-, Deployment-
oder Trust Boundary. Security-, Architektur-, A11Y-, Cross-Platform-, Agent-
und Autonomous-Gates sind vollständig als `Applicable` oder begründetes `N/A`
geplant. Evidence entsteht vor Code, der Vertikalschnitt ist messbar, und jede
spätere Phase besitzt einen eindeutigen Stop. Die Verfassungsprüfung bleibt
`Pass`.

*The design artifacts change no runtime, API, dependency, deployment, or trust
boundary. Every governance gate is explicitly planned as applicable or justified
`N/A`. Evidence precedes code, the vertical slice is measurable, and every later
phase has a clear stop condition. The constitution check remains `Pass`.*
