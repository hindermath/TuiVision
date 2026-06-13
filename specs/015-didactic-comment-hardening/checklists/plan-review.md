# Plan Review Checklist: Didactic Inline Code Comment Hardening

**Purpose**: Validate `plan.md` and the related 015 planning artifacts before `/speckit-tasks`, with one practical execution hint per checkpoint.
**Created**: 2026-06-14
**Feature**: [spec.md](../spec.md)

**Note**: This checklist reviews the quality, traceability, and task-readiness of the planning artifacts. It does not test the later implementation.

## Artifact Scope and Traceability

- [x] CHK001 Are `plan.md`, `spec.md`, `research.md`, `data-model.md`, `quickstart.md`, and the acceptance contract all anchored to `015-didactic-comment-hardening`? [Completeness, Plan §Summary]
  - Durchfuehrungshinweis: Oeffne die Kopfzeilen der sechs Artefakte und gleiche Branch, Feature-Name, Datum und Spec-Link ab. EN: Compare headers for branch, feature name, date, and spec link.
- [x] CHK002 Is the binding input `Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md` preserved without expanding the accepted scope? [Traceability, Spec §FR-001]
  - Durchfuehrungshinweis: Vergleiche die Scope-Saetze in Spec, Plan und Contract mit dem Lastenheft-Bezug; markiere jede zusaetzliche Runtime- oder API-Pflicht als Befund. EN: Compare scope text and flag any added runtime or API obligation.
- [x] CHK003 Is the required ordering after `014-wave1-functional-hardening` and before Wave-1 visual remediation stated consistently? [Consistency, Plan §Baseline Assumptions]
  - Durchfuehrungshinweis: Suche nach `014-wave1-functional-hardening` und `Wave1-Visual`; die Reihenfolge muss in Spec und Plan gleich bleiben. EN: Search both markers and confirm the order is stable.
- [x] CHK004 Are all explicit out-of-scope boundaries repeated where task generation needs them? [Completeness, Plan §Constraints, Contract §1]
  - Durchfuehrungshinweis: Pruefe besonders Runtime-Verhalten, API, Dependencies, Beispielportierung, Framework-Revision und Wave-1-Visual-Remediation. EN: Check the listed exclusions are visible before task generation.

## Evidence Ledger and Hotspot Coverage

- [x] CHK005 Is `specs/015-didactic-comment-hardening/pr-evidence.md` defined as the single primary evidence ledger? [Clarity, Plan §Summary, Contract §2]
  - Durchfuehrungshinweis: Stelle sicher, dass andere Artefakte Evidence nur ergaenzen und nicht als gleichrangige Primaerquelle beschreiben. EN: Ensure other artifacts only supplement the evidence ledger.
- [x] CHK006 Are the required evidence columns complete enough for every reviewed file or named flow area? [Completeness, Data Model §FeatureEvidenceEntry]
  - Durchfuehrungshinweis: Vergleiche `FeatureEvidenceEntry` mit Contract §2; fehlende Felder muessen vor `/speckit-tasks` nachgezogen werden. EN: Compare the model with the contract and close missing fields.
- [x] CHK007 Are all hotspot categories from the contract represented in the data model with stable allowed values? [Coverage, Contract §3, Data Model §HotspotCategory]
  - Durchfuehrungshinweis: Zaehle die Contract-Kategorien und gleiche sie mit `HotspotCategory` ab; Namensabweichungen muessen bewusst sein. EN: Count and compare categories; note intentional naming differences.
- [x] CHK008 Does the plan explain how categories with no current comment need will still be evidenced? [Coverage, Plan §Phase 2]
  - Durchfuehrungshinweis: Suche nach `NoCommentNeeded` und `CommentAdequate`; beide muessen als belegbare Nicht-Aenderungsentscheidungen nutzbar sein. EN: Confirm both values can evidence unchanged areas.
- [x] CHK009 Are cross-file flows allowed as review areas when a hotspot is not owned by one file? [Clarity, Data Model §ReviewArea]
  - Durchfuehrungshinweis: Pruefe, ob `PathOrFlow` und Phase-2-Inventar named flow areas zulassen; sonst drohen kuenstliche Datei-Zuordnungen. EN: Confirm named flow areas are allowed.

## Comment Decision Model

- [x] CHK010 Is the five-value decision model exact and closed across all planning artifacts? [Consistency, Spec §FR-009, Research Decision 3]
  - Durchfuehrungshinweis: Suche nach allen Decision-Namen und nach abweichenden Synonymen wie `FollowUp`; nur die fuenf Primaerwerte duerfen als Entscheidung gelten. EN: Search for decision names and reject extra primary values.
- [x] CHK011 Are `CommentAdequate` and `NoCommentNeeded` distinguishable for unchanged code? [Clarity, Contract §4]
  - Durchfuehrungshinweis: Pruefe, ob vorhandene gute Kommentare und bewusst kommentarfreie klare Stellen getrennt begruendet werden koennen. EN: Confirm existing good comments and intentionally uncommented code are separate states.
- [x] CHK012 Are `CommentNeeded` and `UpdateExistingComment` distinguishable for task decomposition? [Clarity, Data Model §CommentDecision]
  - Durchfuehrungshinweis: Achte darauf, ob neue Kommentarstellen und Korrekturen vorhandener Kommentare spaeter getrennte Tasks ergeben koennen. EN: Ensure new comments and comment corrections can become separate tasks.
- [x] CHK013 Is `FollowUpHardening` bounded so it cannot become hidden runtime work inside 015? [Scope Control, Spec §FR-013]
  - Durchfuehrungshinweis: Pruefe, ob Issue, Out-of-scope-Grund und Follow-up-Ziel verpflichtend sind. EN: Confirm issue, scope reason, and follow-up destination are required.

## Comment Style and Learning Value

- [x] CHK014 Are the didactic comment purposes limited to why, trade-off, constraint, historical deviation, or proof boundary? [Clarity, Spec §FR-014]
  - Durchfuehrungshinweis: Vergleiche Spec, Plan und Contract; kein Artefakt darf triviales Was-Kommentieren als Ziel formulieren. EN: Ensure no artifact promotes obvious what-comments.
- [x] CHK015 Is the normal 1-to-3-line intensity rule present with explicit longer-comment exceptions? [Measurability, Spec §FR-016]
  - Durchfuehrungshinweis: Pruefe, ob laengere Kommentare nur fuer komplexe Flows, Historie, Security/A11Y-Randbedingungen oder Proof-Grenzen erlaubt sind. EN: Confirm longer comments have limited exceptions.
- [x] CHK016 Is German-first/English-second CEFR-B2 scoped to didactic explanation blocks without touching license or tool markers? [Consistency, Spec §FR-017, §FR-018]
  - Durchfuehrungshinweis: Gleiche die Sprachregel mit der Marker-/Lizenz-Ausnahme ab; beides muss gleichzeitig gelten. EN: Check the bilingual rule and marker exceptions together.
- [x] CHK017 Are trivial comments explicitly rejected with objective examples? [Clarity, Spec §FR-015]
  - Durchfuehrungshinweis: Suche nach Identifiern, Operatoren, Zuweisungen, Assertions und offensichtlichem Kontrollfluss als Negativgrenze. EN: Search for concrete trivial-comment boundaries.

## Smoke Helpers and Proof Boundaries

- [x] CHK018 Are smoke-test helpers treated as first-class review targets? [Coverage, Research Decision 4]
  - Durchfuehrungshinweis: Pruefe, ob Tests und Helper nicht nur als Validierung, sondern als comment-relevante Review-Bereiche vorkommen. EN: Confirm helpers are review areas, not only validation tools.
- [x] CHK019 Are proof purpose, stability reason, and proof boundary required for non-obvious helper paths? [Completeness, Data Model §SmokeProofBoundary]
  - Durchfuehrungshinweis: Vergleiche Quickstart §3, Contract §6 und Data Model; alle drei Begriffe muessen spaeter eintragbar sein. EN: Confirm all three proof fields are represented.
- [x] CHK020 Are setup-only and supplemental helpers protected from being overstated as primary behavior proof? [Acceptance, Contract §6]
  - Durchfuehrungshinweis: Suche nach `SetupOnly`, `SupplementalProof`, `PrimaryProof` und pruefe, ob Rollen im Evidence-Modell trennbar sind. EN: Search helper-role terms and check separation.
- [x] CHK021 Are rendering snapshot, buffer/cell, and terminal fallback boundaries covered without forcing runtime changes? [Scope Control, Plan §Phase 2]
  - Durchfuehrungshinweis: Pruefe, ob diese Proof-Arten als Erklaerungs- und Evidence-Punkte erscheinen, nicht als neue Framework-Funktion. EN: Confirm these are explanation/evidence topics, not new behavior.

## Historical and Architecture Boundaries

- [x] CHK022 Is `tv203s/` consistently read-only and comprehension-focused? [Consistency, Plan §Constraints, Contract §7]
  - Durchfuehrungshinweis: Suche nach `tv203s`; jeder Treffer muss Referenz oder Historienkontext sein, nie Bearbeitungsziel. EN: Search `tv203s` and ensure it is reference-only.
- [x] CHK023 Are historical deviations reviewed only when they clarify modern code or proof boundaries? [Scope Control, Research Decision 7]
  - Durchfuehrungshinweis: Pruefe, ob historische Paritaetsfixes und Re-Portierungen explizit ausserhalb des Kommentar-Laufs bleiben. EN: Confirm parity fixes and re-porting stay out of scope.
- [x] CHK024 Are architecture/security follow-ups routed to `FollowUpHardening` or existing evidence paths instead of new plan scope? [Consistency, Plan §Constitution Check]
  - Durchfuehrungshinweis: Vergleiche Architecture-Governance-`N/A` mit Follow-up-Regeln; echte Funde duerfen Scope nicht heimlich erweitern. EN: Compare governance N/A with follow-up handling.

## Governance Applicability

- [x] CHK025 Are all six local Spec-Kit presets named with the current versions in the plan? [Completeness, Plan §Summary]
  - Durchfuehrungshinweis: Gleiche die sechs Versionsnummern mit `.specify/presets/.registry` oder `specify preset list` ab, falls spaeter Zweifel entstehen. EN: Compare preset versions with local preset metadata if needed.
- [x] CHK026 Are NIST SSDF and CWE Top 25 retained as Level-2 context without creating artificial implementation tasks? [Governance, Plan §Constitution Check]
  - Durchfuehrungshinweis: Pruefe, ob sie als Kontext, nicht als neue Code-/Security-Arbeit formuliert sind. EN: Confirm they are context, not new implementation work.
- [x] CHK027 Are ASVS, SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM, NIS2, CRA, EU AI Act, and DORA `N/A` decisions trigger-based? [Governance, Contract §10]
  - Durchfuehrungshinweis: Fuer jeden Standard muss ein Ausloeser genannt oder ausgeschlossen sein; pauschales `N/A` reicht nicht. EN: Each standard needs a trigger rationale or exclusion.
- [x] CHK028 Are STRIDE/CIA/CAPEC, S-ADR, arc42, Zero Trust, SAMM, BSI C3A, and BSI C5 `N/A` because architecture/cloud/deployment boundaries are unchanged? [Governance, Plan §Constitution Check]
  - Durchfuehrungshinweis: Gleiche die `N/A`-Begruendung mit Scope und Constraints ab; Cloud/provider/deployment darf nicht still betroffen sein. EN: Match N/A rationale to unchanged boundaries.
- [x] CHK029 Is cross-platform script governance explicitly `N/A` because no script-shaped tool changes? [Governance, Plan §Constitution Check]
  - Durchfuehrungshinweis: Suche nach neuen oder geaenderten Scripts in der geplanten Struktur; bei keinem Treffer bleibt die Parity-Pflicht `N/A`. EN: Confirm no planned script change exists.

## Documentation, A11Y, and Agent Guidance

- [x] CHK030 Are DocFX and web-a11y validation triggers conditional and consistently described? [Consistency, Research Decision 6, Contract §8]
  - Durchfuehrungshinweis: Vergleiche Plan, Quickstart und Contract; pure `//` oder `/* */` duerfen keinen DocFX-Zwang ausloesen. EN: Compare artifacts and confirm pure inline comments do not trigger DocFX.
- [x] CHK031 Are changed Markdown evidence and guidance required to remain text-first and accessible? [A11Y, Spec §FR-022]
  - Durchfuehrungshinweis: Pruefe, ob Tabellen, Listen und Evidence ohne Farbe/Layout als einzige Bedeutungstraeger nutzbar bleiben. EN: Confirm text-first evidence remains understandable without visual-only cues.
- [x] CHK032 Are agent guidance surfaces complete and synchronized when shared rules change? [Agent Parity, Spec §FR-023]
  - Durchfuehrungshinweis: Gleiche die fuenf genannten Dateien mit Plan und Contract ab; fehlende Surface-Namen sind ein Blocker fuer Tasks. EN: Compare all five surfaces against the plan and contract.
- [x] CHK033 Is `.specify/templates/` impact explicitly `N/A` unless repository-owned templates are changed? [Agent Parity, Data Model §AgentGuidanceReview]
  - Durchfuehrungshinweis: Pruefe, ob Template-Aenderungen nicht implizit erwartet werden; bei Aenderung muss Agent-Parity-Governance neu greifen. EN: Ensure template changes are not implicit.

## Validation and Versioning

- [x] CHK034 Are validation levels proportional to the touched artifact types? [Acceptance, Plan §Testing, Contract §11]
  - Durchfuehrungshinweis: Ordne Kommentar-only, Source/Test-Helper, shared helper, XML/API/Guide und generated-docs Faelle den passenden Befehlen zu. EN: Map artifact types to required validation commands.
- [x] CHK035 Is `dotnet format --verify-no-changes` present as a final implementation evidence requirement? [Completeness, Contract §11]
  - Durchfuehrungshinweis: Pruefe Contract und Quickstart; wenn es fehlt, wird die spaetere No-runtime-change-Evidence zu schwach. EN: Check contract and quickstart for format validation.
- [x] CHK036 Are full Release tests and coverage gate required only when shared logic or broad proof helpers are materially touched? [Measurability, Quickstart §8]
  - Durchfuehrungshinweis: Stelle sicher, dass die Schwelle fuer breite Tests nicht beliebig und nicht zu niedrig ist. EN: Ensure the broad-test threshold is explicit.
- [x] CHK037 Is branch versioning defined before any build, test, commit, or push on the numbered branch? [Consistency, Plan §Testing, Contract §11]
  - Durchfuehrungshinweis: Gleiche die Planregel mit `AGENTS.md` ab; `Directory.Build.props` muss `1.15.<patch>.<build>` folgen. EN: Compare with agent guidance and versioning rules.

## Task-Readiness

- [x] CHK038 Can `/speckit-tasks` derive setup, evidence-ledger, hotspot-inventory, review, comment-edit, governance, validation, and statistics tasks without new decisions? [Readiness, Plan §Phase 2]
  - Durchfuehrungshinweis: Lies die elf Phase-2-Schritte als Task-Skeleton; jeder Schritt sollte direkt in konkrete Tasks zerlegbar sein. EN: Read Phase 2 as a task skeleton.
- [x] CHK039 Are future task dependencies clear enough to avoid parallel edits to shared evidence or agent guidance? [Dependency, Plan §Phase 2]
  - Durchfuehrungshinweis: Markiere alle geplanten Shared-Dateien; Evidence- und Agent-Guidance-Arbeit sollte spaeter serialisiert werden. EN: Mark shared files and serialize evidence/guidance work.
- [x] CHK040 Are completion signals explicit enough for a reviewer to accept the plan before implementation starts? [Acceptance, Plan §Post-Design Gate Review]
  - Durchfuehrungshinweis: Pruefe, ob Accepted-Zustand, ValidationRecord, Governance-Rationale und keine unbounded Follow-ups zusammen ein klares Ende bilden. EN: Confirm acceptance has a clear end state.

## Notes

- Use this checklist after `plan-quality.md` and before `/speckit-tasks`.
- Mark a checkpoint as done only when the referenced artifact text is present, consistent, and sufficient for task generation.
- Record findings inline or in the later task-generation notes; do not turn this checklist into implementation validation.
- Review execution on 2026-06-14 completed CHK001 through CHK040. Corrections were applied to `plan.md`, `data-model.md`, `contracts/didactic-comment-hardening-acceptance.md`, and `quickstart.md` for artifact structure, evidence fields, unchanged-comment decisions, versioning, smoke-helper stability wording, and shared-task serialization.
