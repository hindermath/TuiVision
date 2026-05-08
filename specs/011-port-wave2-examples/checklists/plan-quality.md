# Plan Quality Checklist: Port Wave 2 Examples

**Purpose**: Validate `plan.md`, `research.md`, `data-model.md`,
`contracts/wave2-example-acceptance.md`, `quickstart.md`, `tasks.md`, and
related proof surfaces before task generation and before implementation starts.
**Created**: 2026-05-06
**Feature**: [spec.md](../spec.md), [plan.md](../plan.md)

## Requirement Completeness

- [x] CHK001 Every wave-2 example from `Pflichtenheft.md` appears in plan,
  data model, contract, and quickstart.
- [x] CHK002 Each example has a planned project, smoke-test class, and guide.
- [x] CHK003 Smoke requirements include an example-specific deterministic
  interaction and visible-result assertion.
- [x] CHK004 `sdlg` and `sdlg2` are scoped to historical
  `ScrollDialog`/`ScrollGroup` behavior, not standard-dialog ownership.
- [x] CHK005 Standard-dialog proof remains assigned to `demo` and `dlgdsn`
  only; no third wave-2 flow is accepted for that role.

## Scope Clarity

- [x] CHK006 Wave-3/4 examples cannot satisfy wave-2 acceptance.
- [x] CHK007 Editor, help, stream, terminal emulation, runtime mouse, and real
  charset effects are excluded from wave-2 acceptance.
- [x] CHK008 File-content I/O is excluded from standard-dialog acceptance.
- [x] CHK009 Historical Example Parity Cleanup is non-blocking and scheduled
  no earlier than after mandatory waves 1-4.

## Governance Coverage

- [x] CHK010 Constitution checks cover branching, versioning, .NET 10/C# 14,
  MSL, security, architecture, A11Y, statistics, and agent parity.
- [x] CHK011 Architecture evidence under `docs/architecture/` is explicitly
  planned.
- [x] CHK012 Existing `docs/security/` evidence is referenced with justified
  ASVS/Zero-Trust N/A conditions.
- [x] CHK013 DE-first/EN-second CEFR-B2 guide work is planned for every new
  example.
- [x] CHK014 DocFX and Playwright/axe are conditional on public API or generated
  documentation output changes.

## Task-Generation Readiness

- [x] CHK015 Project structure names concrete target directories and files.
- [x] CHK016 Testing strategy distinguishes example smoke tests from focused
  framework tests.
- [x] CHK017 Quickstart includes build, test, coverage, format, and conditional
  documentation validation.
- [x] CHK018 Success criteria traceability maps every SC to a plan evidence
  path.

## Review Result 2026-05-06

- All checklist items were reviewed while creating the plan artifacts.
- No additional user clarification was required before `/speckit-tasks`.

## Pre-Implementation Readiness Review 2026-05-08

These checklist items validate the updated requirement set after plan, task,
coverage, and agent-surface remediation. They test whether the written
requirements are complete, clear, consistent, measurable, and ready for
implementation.
The `Durchfuehrungshinweis` entries describe how to review the written
artifacts; they do not add implementation or runtime-test work.

## Requirement Completeness After Plan/Task Changes

- [x] CHK019 Are all eleven wave-2 examples represented across `spec.md`,
  `plan.md`, `tasks.md`, the contract, data model, and quickstart without an
  unnamed acceptance vehicle? [Completeness, Spec §FR-001, Plan §Wave-2
  Checklist Traceability]
  - Durchfuehrungshinweis: Vergleiche die elf Namen in allen genannten
    Artefakten und markiere den Punkt nur als erledigt, wenn keine Aliasnamen,
    Platzhalter oder offene "third example"-Formulierungen uebrig sind.
- [x] CHK020 Are smoke-proof requirements defined as example-specific
  interactions rather than startup/exit-only evidence for every example family?
  [Completeness, Spec §FR-002, Plan §Interaction-Family Mapping, Contract
  §Per-Example Acceptance]
  - Durchfuehrungshinweis: Pruefe fuer jede Interaktionsfamilie, ob das
    Artefakt eine sichtbare, beispielspezifische Zustandsaenderung fordert und
    nicht nur "startet ohne Fehler" beschreibt.
- [x] CHK021 Are guides, index updates, `Pflichtenheft.md`, statistics,
  architecture, security, A11Y, and agent surfaces documented as completion
  scope rather than follow-up cleanup? [Completeness, Spec §FR-012, Research
  §Decision 10, Tasks §T080-T084/T096-T100a]
  - Durchfuehrungshinweis: Gehe die Abschlussartefakte einzeln durch und
    bestaetige, dass sie im Plan oder in Tasks als Feature-Abschluss genannt
    sind, nicht nur als optionale spaetere Pflege.
- [x] CHK022 Are versioning requirements connected to every build/test
  validation command through one source of truth instead of duplicated partial
  rules? [Completeness, Tasks §Versioning Rule, Tasks §T085/T100, Quickstart
  §5]
  - Durchfuehrungshinweis: Suche nach allen `dotnet build`- und `dotnet test`-
    Stellen und pruefe, ob sie auf die zentrale Versionierungsregel verweisen,
    ohne abweichende Nebenregeln zu formulieren.

## Scope And Boundary Clarity

- [x] CHK023 Is wave-2 scope clear enough to prevent editor, help, stream,
  terminal-emulation, runtime-mouse, and real charset behavior from being
  counted toward acceptance? [Clarity, Spec §FR-003/FR-004, Plan §Design
  Overview]
  - Durchfuehrungshinweis: Lies die Scope- und Demo-Abschnitte gemeinsam und
    notiere jede Funktion aus einer spaeteren Welle, die noch als
    Akzeptanzbeitrag missverstanden werden koennte.
- [x] CHK024 Is standard-dialog proof unambiguously owned by `demo` and
  `dlgdsn`, and excluded from `sdlg`, `sdlg2`, or unnamed third examples?
  [Clarity, Consistency, Spec §FR-005a, Research §Decision 5, Contract
  §Standard-Dialog Contract]
  - Durchfuehrungshinweis: Vergleiche alle Standarddialog-Erwaehnungen und
    hake nur ab, wenn `demo` und `dlgdsn` die einzigen Akzeptanztraeger sind.
- [x] CHK025 Is file-system scope bounded to metadata, filters, manual paths,
  cancellation, and invalid paths without file-content I/O? [Clarity, Spec
  §FR-005a, Research §Decision 6, Contract §Standard-Dialog Contract]
  - Durchfuehrungshinweis: Pruefe, ob alle Datei-/Verzeichnisformulierungen
    Entscheidungen und Metadaten beschreiben und keine Lese-, Schreib-,
    Speicher-, Loesch- oder Ueberschreibpflicht erzeugen.
- [x] CHK026 Are `sdlg` and `sdlg2` requirements precise enough to distinguish
  mandatory historical `ScrollDialog`/`ScrollGroup` completion from later
  Historical Example Parity Cleanup? [Clarity, Spec §FR-005/FR-015, Data Model
  §ScrollableDialogFlow]
  - Durchfuehrungshinweis: Trenne beim Lesen Muss-Akzeptanz fuer Scrollachsen,
    Fokus, Grenzen und sichtbaren Zustand von optionaler historischer Paritaet
    und markiere unklare Ueberschneidungen.
- [x] CHK027 Is `dlgdsn` acceptance defined across structured-description
  create/load, rendering, one simple change, persisted fixture use, and all
  named invalid variants? [Clarity, Completeness, Spec §FR-006, Plan §Design
  Overview, Data Model §StructuredDialogDescription]
  - Durchfuehrungshinweis: Pruefe die `dlgdsn`-Artefakte gegen eine kurze
    Abdeckungsmatrix: create/load, render, simple change, fixture roundtrip,
    malformed, incomplete, duplicate-control, invalid-navigation.

## Plan/Task Consistency And Sequencing

- [x] CHK028 Are `TScrollGroup` foundation dependencies for ADR, failing tests,
  implementation, and porting evidence consistent between plan, research, and
  task dependency notes? [Consistency, Research §Decision 11, Tasks
  §Dependencies]
  - Durchfuehrungshinweis: Vergleiche Decision 11 mit T023a-T023d und den
    Dependency-Notizen; die Reihenfolge muss ADR, fehlende oder failing
    Testevidenz, Implementierung und Evidenz ohne Widerspruch abbilden.
- [x] CHK029 Are `[P]` markers constrained so no parallel task has unresolved
  file or logical dependency conflicts with its prerequisite chain?
  [Consistency, Tasks §Parallel Opportunities]
  - Durchfuehrungshinweis: Betrachte jede `[P]`-Gruppe gegen die genannten
    Dateipfade und fachlichen Gate-Abhaengigkeiten; unklar parallele Aufgaben
    brauchen eine Praezisierung.
- [x] CHK030 Are T023e/T023f documented as early-visibility or final-validation
  prerequisites without accidentally blocking US1, US2, or US3 acceptance?
  [Consistency, Tasks §T023e/T023f, Tasks §Dependencies]
  - Durchfuehrungshinweis: Lies T023e/T023f zusammen mit den Dependency-Notizen
    und bestaetige, dass sie T089/CI-Konvention klaeren, aber keine Story-
    Akzeptanz kuenstlich sperren.
- [x] CHK031 Are Red-Green-Refactor requirements expressed for each user-story
  task group before implementation tasks begin? [Coverage, Tasks
  §Red-Green-Refactor Rules]
  - Durchfuehrungshinweis: Ordne die Testaufgaben den Implementierungsaufgaben
    pro Story zu und pruefe, ob die schriftliche Reihenfolge failing/missing
    evidence vor Implementierung verlangt.
- [x] CHK032 Are US1/US2/US3 independence claims consistent with shared Phase-2
  prerequisites and final documentation dependencies? [Consistency, Tasks
  §User Story Dependencies]
  - Durchfuehrungshinweis: Pruefe, ob jede Story eigenstaendig validierbar
    bleibt, waehrend gemeinsame Phase-2-Gates und der finale US3-Proof
    korrekt als Abhaengigkeiten benannt sind.

## Acceptance Criteria And Traceability Quality

- [x] CHK033 Can every SC-001 through SC-009 be objectively mapped to an
  evidence path in plan/tasks without relying on unstated reviewer knowledge?
  [Measurability, Spec §SC-001..SC-009, Plan §Success-Criteria Traceability]
  - Durchfuehrungshinweis: Nutze im Review die vorhandene
    Traceability-Tabelle oder eine kurze SC-zu-Evidenz-Matrix und bestaetige
    fuer jedes SC einen benannten Pfad, Task oder Nachweis.
- [x] CHK034 Is SC-007's "no wave-3/4 examples" rule specified with a
  reproducible evidence model for macOS/Linux/WSL and Windows-native
  PowerShell? [Acceptance Criteria, Spec §SC-007, Tasks §T087a]
  - Durchfuehrungshinweis: Pruefe, ob das Bash- und PowerShell-Evidenzmodell
    die gleiche Soll-Liste verwendet und ob das erwartete leere Ergebnis als
    Akzeptanzkriterium beschrieben ist.
- [x] CHK035 Are interaction-family requirements complete enough to cover
  primary, alternate, exception, and non-functional states for clipboard, lists,
  combo boxes, progress, dynamic text, scrollable dialogs, standard dialogs,
  `dlgdsn`, and `demo`? [Coverage, Plan §Interaction-Family Mapping, Contract
  §Interaction-Family Contract/Boundary And Failure Contract]
  - Durchfuehrungshinweis: Nutze je Interaktionsfamilie eine einfache
    Review-Matrix mit Primary, Alternate, Exception und Non-Functional;
    fehlende oder implizite Felder als Review-Befund notieren.
- [x] CHK036 Are non-success dialog outcomes specified as requirement quality
  criteria instead of being left to individual example interpretation? [Edge
  Case Coverage, Contract §Boundary And Failure Contract, Data Model
  §DialogFlow]
  - Durchfuehrungshinweis: Suche nach cancel, close, invalid selection und
    failed validation und bewerte, ob die Artefakte sichtbare Ergebniszustaende
    verlangen statt nur Fehlerbehandlung anzudeuten.
- [x] CHK037 Are long, empty, and boundary-content scenarios defined where
  relevant for list, combo, dynamic text, progress, and scrollable-dialog
  flows? [Edge Case Coverage, Quickstart §4, Contract §Per-Example Acceptance]
  - Durchfuehrungshinweis: Vergleiche die Boundary-Hinweise im Quickstart mit
    Contract und Plan; hake nur ab, wenn die relevanten Beispielklassen nicht
    auf Normalfaelle beschraenkt bleiben.

## Governance And Evidence Readiness

- [x] CHK038 Are architecture-evidence minimum bars specific enough for each
  required `docs/architecture/` artifact and ADR without over-scoping new
  architecture work? [Non-Functional Requirements, Plan §Architecture
  evidence, Research §Decision 9]
  - Durchfuehrungshinweis: Pruefe je Architekturdatei, ob Mindestinhalt und
    Zweck genannt sind und ob weitere ADRs nur bei neuen querschnittlichen
    Entscheidungen verlangt werden.
- [x] CHK039 Are security-applicability requirements complete for NIST SSDF,
  CWE Top 25, ASVS N/A, Zero Trust/CAPEC, supply-chain/provenance, and
  dependency-currency evidence? [Non-Functional Requirements, Spec
  §CR-006..CR-010, Tasks §T024-T028]
  - Durchfuehrungshinweis: Gehe die Sicherheitsstandards einzeln durch und
    bestaetige, dass entweder ein Evidenzpfad oder eine begruendete N/A-
    Aussage im Artefaktsatz vorhanden ist.
- [x] CHK040 Are A11Y and documentation requirements precise enough to cover
  terminal examples, smoke output, guides, generated HTML, DocFX navigation,
  Playwright/axe, and text-first fallback? [Non-Functional Requirements, Spec
  §GA-004/SC-008, Quickstart §6-7, Tasks §T067-T092]
  - Durchfuehrungshinweis: Pruefe Dokumentations- und A11Y-Pfade getrennt fuer
    Markdown-Guides, Terminal-/Smoke-Ausgaben und generierte HTML-Dokumentation.
- [x] CHK041 Are agent-surface synchronization requirements complete across all
  five maintained files when active context, coverage workflow, statistics, or
  next-step markers change? [Consistency, Spec §GA-006, Tasks §T083/T084/T100a]
  - Durchfuehrungshinweis: Vergleiche die genannte Fuenferliste mit den Tasks;
    hake nur ab, wenn klar ist, wann alle betroffenen Agentenflaechen gemeinsam
    geprueft oder aktualisiert werden.
- [x] CHK042 Is the Lastenheft rename or N/A decision specified as a completion
  requirement without conflicting with this wave's `Pflichtenheft.md`-driven
  scope? [Completeness, Tasks §T101, Quickstart §7]
  - Durchfuehrungshinweis: Pruefe, ob ein vorhandenes Lastenheft nur dann
    umbenannt werden muss, wenn es wirklich zu dieser Welle gehoert; sonst muss
    die N/A-Begruendung nachvollziehbar sein.

## Coverage-Gate And Validation Readiness

- [x] CHK043 Are `coverlet.runsettings` requirements aligned across plan,
  tasks, quickstart, root runsettings file, and agent surfaces so the gate
  cannot be measured with default filters? [Consistency, Plan §Coverage Gate,
  Tasks §T023e/T089/T100a, Quickstart §5, coverlet.runsettings]
  - Durchfuehrungshinweis: Vergleiche die exakte Coverage-Kommandoform in allen
    Artefakten und bestaetige, dass `--settings coverlet.runsettings` und der
    Repository-Root-Kontext nicht optional wirken.
- [x] CHK044 Is the coverage-gate requirement measurable at assembly level for
  only the five framework modules, with example and test assemblies excluded
  from gate aggregation? [Measurability, Plan §Coverage Gate,
  coverlet.runsettings]
  - Durchfuehrungshinweis: Vergleiche Include- und Exclude-Liste im
    `coverlet.runsettings` mit Plan/T023e; Unterschiede in Assemblynamen oder
    fehlende Testassembly-Ausschluesse sind Review-Befunde.
- [x] CHK045 Is the `>=80%` tracking target explicitly separated from the
  `>=70%` hard gate so acceptance cannot be blocked by an informational target?
  [Clarity, Plan §Coverage Gate, coverlet.runsettings]
  - Durchfuehrungshinweis: Suche nach `80` und `70` im Artefaktsatz und
    bestaetige, dass nur `>=70%` als hartes Gate formuliert ist.
- [x] CHK046 Are platform-evidence requirements specific enough to distinguish
  required macOS evidence from practical Linux/Windows/WSL evidence and
  documented follow-up or N/A paths? [Clarity, Tasks §T093, Quickstart §7]
  - Durchfuehrungshinweis: Pruefe, ob fuer jede Plattformklasse klar ist, ob
    sie verpflichtend, praktisch erwartet oder mit begruendetem Follow-up
    dokumentierbar ist.
- [x] CHK047 Are final proof requirements ordered so `Pflichtenheft.md` marker,
  statistics, and PR evidence cannot be claimed before validation, coverage,
  formatting, security, and staged-file hygiene requirements are satisfied?
  [Consistency, Tasks §T085-T101, Spec §FR-012/FR-014]
  - Durchfuehrungshinweis: Lies T085-T101 sequenziell und pruefe, ob
    Pflichtenheft-Haken, Next-Step-Marker, Statistik und PR-Zusammenfassung
    erst nach den Validierungs- und Hygiene-Nachweisen liegen.

## Review Result 2026-05-08

- CHK019-CHK047 were reviewed against `spec.md`, `plan.md`, `tasks.md`,
  `quickstart.md`, `research.md`, `data-model.md`,
  `contracts/wave2-example-acceptance.md`, `coverlet.runsettings`, and the
  maintained agent surfaces.
- Follow-up edits were applied before marking the items complete: the checklist
  purpose and CHK005 were updated to the post-task baseline; quickstart now
  names all eleven wave-2 examples and includes scrollable-dialog bounds in
  boundary evidence; `spec.md` and the contract now name the same `dlgdsn`
  invalid-description variants as plan, tasks, and data model.
- No additional blocking clarification remains before implementation starts.
