# Planqualitäts-Checkliste / Plan Quality Checklist

**Zweck / Purpose**: Prüfen, ob Feature 038 vollständig und sicher genug für
die spätere unabhängige Plan-Review-Phase geplant ist, ohne Tasks oder
Implementierung vorwegzunehmen.

**Datum / Date**: 2026-08-09

**Plan**: [plan.md](../plan.md)

## Scope und Autorität / Scope and Authority

- [x] **PLQ001** Der Plan bindet die aktuelle `MergeAndSync`-Autorität an
  Feature 038, begrenzt den Bypass eng und verbietet Provider-/Upstream-Handlung
  sowie Folgefeature-Start.
- [x] **PLQ002** `src/`, `examples/`, `tv203s/`, `TVDEMOS/`, `TVFM/`, Public
  API, Dependencies und Projektdateien sind als geschützte Flächen benannt.
- [x] **PLQ003** Teständerungen sind auf einen Audit-Integritätsvalidator und
  kontrollierte Fixtures im bestehenden Testprojekt begrenzt.
- [x] **PLQ004** Routing-Metadaten, State-/Resume-Regeln und reine
  Plan-Phasengrenze sind ausdrücklich erhalten.

## Eingaben und Quellen / Inputs and Sources

- [x] **PLQ005** Die vier akzeptierten Input-Hashes und die Feature-037-
  `Closed`/`Eligible`-Grenze sind bindend.
- [x] **PLQ006** Free Vision, Terminal.GUI v1.9.0 und magiblot/tvision sind mit
  den akzeptierten Pins und lokalen Evidence-Hashes festgelegt.
- [x] **PLQ007** Bewegliche Upstreams, neue Releases, Kopie, mechanische
  Übersetzung und Vendorisierung sind ausgeschlossen.
- [x] **PLQ008** Die Quellenhierarchie hält historische Absicht, akzeptierte
  TuiVision-Semantik und sekundäre Vergleichsmeinungen auseinander.

## Exaktes Inventar und Relationen / Exact Inventory and Relations

- [x] **PLQ009** Der Abnahmevertrag enthält exakt `EX001`–`EX037` mit Name,
  Rolle, Wave, Entry-Point, Guide, historischer Authority und Evidence-Basis.
- [x] **PLQ010** Die 37 direkten Projekte sind von den zwei Shared-Assemblies
  abgegrenzt und durch einen stabilen Pfadlistenhash gebunden.
- [x] **PLQ011** Source-, Evidence-, Finding-, Owner- und Intake-Relationen sind
  bidirektional und ohne verwaiste Knoten geplant.
- [x] **PLQ012** `A11yFramework` ist genau einmal `SupplementalControl` mit
  historischer Relation `N/A` samt Begründung und Trigger.

## Evidence- und Datenmodell / Evidence and Data Model

- [x] **PLQ013** Eine kanonische JSON-Wahrheitsquelle und zehn vollständige
  text-first Markdown-Projektionen sind benannt.
- [x] **PLQ014** Alle FR-005-Felder, geschlossenen Vokabulare,
  `DimensionDecision`, Status-/Disposition-Konsistenz und Reviewfelder sind
  modelliert.
- [x] **PLQ015** Evidence-Skelett, `pr-evidence` und Gate-Anforderungen müssen
  vor dem ersten Implementierungsedit existieren.
- [x] **PLQ016** JSON ist UTF-8, schema-versioniert, ordinal sortiert und wird
  fail-closed ohne Teilannahme validiert.

## Vertikalschnitt und Testdesign / Vertical Slice and Test Design

- [x] **PLQ017** `EX036 Tp7FileManager` ist als risikoreicher, vollständiger
  repräsentativer Vertikalschnitt vor der breiten Prüfung begründet.
- [x] **PLQ018** Der erste Red-Lauf ist ein semantischer fehlender-Slice-Fund,
  kein Kompilations-, Restore- oder Infrastrukturfehler.
- [x] **PLQ019** Der Validatorvertrag definiert positive Invarianten, stabile
  `EPA###`-Diagnosen und kontrollierte malformed Fixtures.
- [x] **PLQ020** Pfad-, Größen-, Locale-, Zeit-, Netzwerk-, Zufalls- und
  Nutzerdatengrenzen des Validators sind festgelegt.
- [x] **PLQ021** Didaktische Kommentare sind moderat, zweisprachig und auf
  nicht offensichtliche Relations-/Dedup-/Authority-Grenzen begrenzt.

## Findings und Handoff / Findings and Handoff

- [x] **PLQ022** `EF001+` wird erst nach Root-Cause-Freeze deterministisch und
  lückenlos vergeben.
- [x] **PLQ023** Der Deduplication Key ist kontrolliert, ursachenbezogen und
  unabhängig von Beispielname oder Freitext.
- [x] **PLQ024** Genau ein Primary Owner wird über eine eindeutige Ursachenregel
  zugeordnet; sekundäre Wirkungen ändern ihn nicht.
- [x] **PLQ025** Jede Gap-Dimension verweist auf ein Finding oder einen
  blockierenden `ProductDecision`-Stop.
- [x] **PLQ026** Nur nicht leere Owner-Gruppen erzeugen genau einen
  unnummerierten Remediation-Intake; leere Gruppen sind `Suppressed`.
- [x] **PLQ027** Der Owner-DAG ist azyklisch und danach folgt genau ein
  unnummerierter unabhängiger Closure, der nicht gestartet wird.

## Governance und Dokumentation / Governance and Documentation

- [x] **PLQ028** Alle zwölf installierten Presets sind mit Version,
  Priorität, Applicability und proportionaler Grenze berücksichtigt.
- [x] **PLQ029** NIST SSDF/CWE, MSL, Secure Coding, Architektur, iSAQB,
  A11Y, Cross-Platform, Agent Parity, Intake und Autonomous Governance sind
  ausdrücklich geplant.
- [x] **PLQ030** Nicht anwendbare ASVS-, Supply-Chain-, AI-SBOM-, Regulatory-,
  Cloud-, Zero-Trust-, C3A/C5-, SAMM-, Script- und allgemeine
  Architekturartefakte besitzen Begründung und Re-Evaluation-Trigger.
- [x] **PLQ031** Governance-Entscheidungen trennen Applicability und
  Implementation und führen Evidence, Owner, Reviewer, Restrisiko, Trigger und
  Follow-up.
- [x] **PLQ032** Dokumentation ist `UpdateRequired`; DE-first/EN-second,
  CEFR-B2, WCAG 2.2 AA und text-first Reader Paths sind geplant.
- [x] **PLQ033** Statistik löst DocFX, Playwright/Axe und UTF-8-Lynx aus;
  XML/Public API, Agentenflächen und Scripts bleiben triggerbasiert `N/A`.

## Validierung und autonome Gates / Validation and Autonomous Gates

- [x] **PLQ034** Der Plan nennt targeted Validator, Beispiel-Smokes,
  vollständige Release-Suite, fünf Assembly-Coverage-Gates, Format, Scope,
  Doku/A11Y und Governance-Scans in Reihenfolge.
- [x] **PLQ035** Vor jedem einzelnen `dotnet build` oder `dotnet test` ist ein
  eigener Build-Counter-Schritt mit ausgerichteten Versionsfeldern geplant.
- [x] **PLQ036** Shared Single-Writer-Flächen und sichere Stop-Grenzen sind
  vollständig benannt; Parallel-Autonomie ist `N/A`.
- [x] **PLQ037** Remote Exact-Head und Merge/Sync sind anwendbar und werden nur
  aus tatsächlichen Workflow-/Joblogs belegt; ein kausaler Closeout bleibt auf
  echte Post-Merge-Fakten begrenzt.
- [x] **PLQ038** Kein Gate bleibt ohne Rationale offen; spätere `Open`-Werte
  brauchen Owner, Follow-up und Trigger.

## Phasengrenze / Phase Boundary

- [x] **PLQ039** `tasks.md` wurde nicht erstellt.
- [x] **PLQ040** Implementierung, Build, Test, Commit, Push, PR, Merge und
  Folgefeature wurden nicht gestartet.
- [x] **PLQ041** Die nächste zulässige Phase ist ausschließlich die getrennte
  Plan-Review-Checklist; sie wird durch diesen Plan nicht als bestanden markiert.

## Ergebnis / Result

`PASS` – 41 von 41 Planqualitätskriterien sind im Plan, Research,
Datenmodell, Quickstart und den beiden Verträgen vollständig abgedeckt. Die
unabhängige Plan-Review-Phase bleibt ausstehend.

*`PASS` – all 41 plan-quality criteria are covered. The independent plan-review
phase remains pending.*
