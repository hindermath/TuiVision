# Feature Specification: Wave-1 Visual Component Remediation

**Feature Branch**: `017-wave1-visual-component-remediation`  
**Created**: 2026-07-11  
**Status**: Draft  
**Binding Input**: `Lastenheft_Wave1-Visual-Component-Remediation.md`

## Clarifications

### Session 2026-07-11

- Q: Muss jedes Beispiel eine echte `TStatusLine` verwenden? → A: Ja; ein gleichwertiger Statusbereich ist nur mit belegter historischer oder Framework-bedingter Ausnahme zulässig.
- Q: Wie weit muss die visuelle Unterscheidung der 16 Tutorial-Schritte gehen? → A: Token, Lernziel und repräsentative Komponente oder Zustand müssen unterscheidbar sein; eine vollständige historische Neuportierung ist nicht erforderlich.
- Q: Muss Desklogo für den Bediennachweis einen künstlichen Logo-Zustandswechsel erhalten? → A: Nein; sichtbarer Startzustand, tastaturerreichbare Beschreibung und stabiler Quit-Pfad bilden den passenden Bediennachweis.
- Q: Welche Videomode-Ergebnisse sind kanonisch? → A: Genau `supported`, `fallback`, `rejected` oder `unchanged`, jeweils mit ehrlicher sichtbarer Erklärung.
- Q: Welche Evidenz ist für einen primären visuellen Smoke erforderlich? → A: Echter App-Loop-Pfad, konkrete Zustandsassertion, View-Baum-Nachweis und gerenderter Buffer-/Cell-Nachweis; eine technisch unmögliche Teilprüfung braucht eine explizite Proof-Grenze.

No formal clarification remains open. The binding Lastenheft, the completed
functional baseline in feature 014, and the accepted Wave-2 visual-remediation
pattern in feature 013 provide sufficient planning boundaries.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Sichtbare Wave-1-Demos / Visible Wave-1 Demos (Priority: P1)

Als lernende Person möchte ich jedes Wave-1-Beispiel normal starten und sofort
seine historische Hauptidee als sichtbare Komponente oder stabilen sichtbaren
Zustand erkennen. / As a learner, I want to start every Wave-1 example normally
and immediately recognize its historical main idea as a visible component or
stable visible state.

**Why this priority**: Eine startbare Anwendung ohne erkennbare Demo-Idee ist
kein ausreichender Lern- oder Paritätsnachweis. / A runnable application without
a recognizable demo idea is not sufficient learner or parity proof.

**Independent Test**: `Desklogo`, `MsgCls`, alle 16 `Tutorial`-Tokenpfade und
`Videomode` können separat gestartet werden; jeder Start zeigt eine passende
Hauptfläche oder einen stabilen sichtbaren Runtime-Zustand.

**Acceptance Scenarios**:

1. **Given** `Desklogo` starts normally, **When** the first screen is rendered,
   **Then** a logo or honest logo/desktop fallback is visibly identifiable.
2. **Given** `MsgCls` starts normally, **When** its main view is rendered,
   **Then** the message-routing purpose and current routing result are visible.
3. **Given** any token from `tvguid01` through `tvguid16`, **When** that token is
   selected, **Then** the screen identifies the token, its learning goal, and a
   step-specific visible result.
4. **Given** `Videomode` starts on any supported test platform, **When** its
   capability probe completes, **Then** it visibly reports `supported`,
   `fallback`, `rejected`, or `unchanged` without overstating capability.

---

### User Story 2 - Bedienbare Drei-Schichten-Erfahrung / Operable Three-Layer Experience (Priority: P1)

Als manueller Reviewer möchte ich die Kernfunktion jeder Demo über Tastatur,
Menü, Statuszeile oder einen sichtbaren Command-Pfad auslösen und ihren Zustand
anschließend nachvollziehen. / As a manual reviewer, I want to trigger each
demo's core function by keyboard, menu, status line, or a visible command path
and understand the resulting state.

**Why this priority**: Sichtbarkeit allein beweist keine bedienbare Demo; die
Hauptfläche, kurze Statusrückmeldung und textorientierte Beschreibung müssen
zusammenpassen. / Visibility alone does not prove an operable demo; main area,
short status feedback, and text-oriented description must agree.

**Independent Test**: Jede Beispielgruppe besitzt unabhängig prüfbar eine
Hauptfläche, eine Statuszeile oder einen gleichwertigen Statusbereich und einen
tastaturerreichbaren Beschreibungsweg.

**Acceptance Scenarios**:

1. **Given** a Wave-1 example is running, **When** its documented primary
   operation is triggered, **Then** a visible state changes and status feedback
   names the result or next action.
2. **Given** a keyboard-only user, **When** the description command is invoked,
   **Then** concise text explains the visible component, operation, historical
   intent, and relevant status feedback.
3. **Given** the operation is repeated, **When** the same route is used again,
   **Then** the application remains stable and the visible result stays
   understandable.

---

### User Story 3 - Sichtbarer App-Loop-Nachweis / Visible App-Loop Proof (Priority: P2)

Als Maintainer möchte ich die realen Bedienpfade über den Anwendungs-Loop
regressionssicher prüfen, damit Startup, statischer Text oder direkte
Hilfsmethoden nicht fälschlich als visueller Nachweis gelten. / As a maintainer,
I want to verify the real operation paths through the application loop so that
startup, static text, or direct helpers are not mistaken for visual proof.

**Why this priority**: Der Nachweis schützt die sichtbare Runtime-Reife nach der
Auslieferung. / The proof protects visible runtime maturity after delivery.

**Independent Test**: Die vier Beispielgruppen besitzen getrennte primäre
Smoke-Szenarien, die Events, Commands oder Tasten durch den echten App-Loop
führen und konkrete sichtbare Zustände nachweisen.

**Acceptance Scenarios**:

1. **Given** a primary smoke scenario, **When** it drives the documented event,
   command, or key route, **Then** it verifies concrete state plus visible view
   or rendered cell/buffer evidence.
2. **Given** a direct helper remains useful, **When** proof is reviewed, **Then**
   the helper is setup or supplemental evidence and cannot replace the primary
   app-loop proof.
3. **Given** all tutorial tokens, **When** the smoke matrix runs, **Then** all 16
   token paths remain individually selected and visibly distinguishable.

---

### User Story 4 - Text-first-Lern- und Reviewpfad / Text-First Learning and Review Path (Priority: P2)

Als sehbehinderte oder textorientiert arbeitende Person möchte ich die visuelle
Demo über Status, Beschreibung, Guide und Evidence verstehen können, ohne dass
die Textbeschreibung die eigentliche Hauptfläche ersetzt. / As a visually
impaired or text-oriented user, I want to understand the visual demo through
status, description, guide, and evidence without text replacing the actual
main area.

**Why this priority**: Die visuelle Remediation muss inklusiv und historisch
nachvollziehbar bleiben. / The visual remediation must remain inclusive and
historically traceable.

**Independent Test**: Für jede Beispielgruppe lassen sich Start, Hauptfläche,
Bedienweg, Status, Beschreibung, historische Quelle, bewusste Abweichung und
Proof-Grenze in deutschsprachigem und anschließend englischem CEFR-B2-Text
nachvollziehen.

**Acceptance Scenarios**:

1. **Given** an affected guide, **When** a learner reads it, **Then** German
   appears first, English follows, and essential meaning does not depend on
   color, pointer input, or layout alone.
2. **Given** a new visible historical deviation, **When** the feature is
   reviewed, **Then** the deviation and rationale are traceable to read-only
   historical source evidence.
3. **Given** implementation completion, **When** project evidence is inspected,
   **Then** every example has a framework-use decision, proof route, validation
   result, and bounded follow-up status.

### Edge Cases

- The terminal is too small to show the complete Desklogo asset.
- The historical Desklogo generator path is unsuitable as a managed runtime
  dependency, while an embedded or honest fallback representation remains
  sufficient.
- MsgCls receives the same trigger repeatedly or its message window is not the
  currently focused view.
- A Tutorial token is unknown, missing, or visually too similar to another step.
- A Tutorial step's historical teaching goal has no exact modern control
  counterpart and needs a documented intentional deviation.
- The terminal refuses, ignores, or cannot prove a requested video-mode change.
- A visible state exists in the view tree but is clipped or absent from rendered
  cells in the current viewport.
- A proposed local example helper duplicates reusable framework behavior.
- A discovered visual or framework defect requires broader redesign than the
  narrow remediation boundary allows.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST cover exactly `Desklogo`, `MsgCls`, `Tutorial`
  tokens `tvguid01` through `tvguid16`, and `Videomode`.
- **FR-002**: Feature 014 functional-hardening evidence MUST be reviewed as the
  accepted behavioral baseline before visual remediation begins.
- **FR-003**: Every covered example area MUST show a real visible main component
  or stable visible runtime state matching its historical main idea.
- **FR-004**: Every covered example area MUST provide the three-layer model:
  visible main area, real `TStatusLine` or a historically/framework-justified
  equivalent status area, and
  keyboard-reachable Help, Description, or About content.
- **FR-005**: Static explanatory text, startup success, `VisibleText`, history
  state, or direct-helper output alone MUST NOT count as primary visual proof.
- **FR-006**: Each covered example MUST expose at least one keyboard, menu,
  status-line, or visible command path that exercises the function accepted by
  feature 014 and produces an observable result.
- **FR-007**: `Desklogo` MUST visibly prove logo/desktop intent, controlled
  undersized-terminal behavior, logo source or fallback status, description
  access, and stable quit behavior; it MUST NOT add an artificial logo mutation
  only to satisfy the operation-path requirement.
- **FR-008**: `MsgCls` MUST expose a visible message trigger, visible routing
  result, repeat-trigger stability, status feedback, and a description of the
  broadcast/message-class idea.
- **FR-009**: `Tutorial` MUST preserve all 16 token launch paths and give every
  token a distinct visible learning goal, visible result, status/navigation
  feedback, and text-first description; distinction MUST include token, goal,
  and a representative component or state without requiring a complete
  historical re-port.
- **FR-010**: `Videomode` MUST expose a visible capability operation and report
  exactly one honest result class: `supported`, `fallback`, `rejected`, or
  `unchanged`, while remaining usable after the operation.
- **FR-010a**: Starting `Tutorial` without a token MUST select and visibly name
  the documented default step; an unknown token MUST produce an honest visible
  fallback rather than silently selecting an unrelated step.
- **FR-011**: Every primary smoke MUST drive the real application loop through
  events, commands, or keys and verify concrete runtime state together with
  view-tree and rendered buffer/cell visibility. If one proof layer is
  technically impossible for a specific state, evidence MUST identify the
  missing layer, reason, substitute proof, and follow-up boundary.
- **FR-012**: Direct helpers MAY prepare state or add assertions, but MUST be
  classified as setup or supplemental proof and MUST NOT replace primary proof.
- **FR-013**: The Tutorial smoke matrix MUST exercise all 16 tokens separately
  and reject accidental generic or duplicate visible outcomes.
- **FR-014**: Every covered example MUST be checked against its relevant
  historical `.c`, `.cc`, `.cpp`, and required header sources under `tv203s/`
  as read-only intent evidence.
- **FR-015**: New user-visible deviations from historical intent MUST record the
  historical source, modern behavior, rationale, and learner-visible effect.
- **FR-016**: For every example, feature evidence MUST identify framework
  components used for main area, status, description, operation, and smoke proof.
- **FR-017**: Every example or shared contract area MUST receive exactly one
  framework decision: `UseExistingFramework`, `SmallFrameworkFix`,
  `IntentionalDeviation`, or `FollowUpHardening`.
- **FR-018**: Local example logic MAY compose existing controls, but reusable or
  repeated framework behavior MUST be handled through `SmallFrameworkFix` or a
  bounded `FollowUpHardening` entry.
- **FR-019**: A `SmallFrameworkFix` MUST be narrowly required by visible
  composition, status, description, operation, or deterministic smoke proof and
  MUST have focused regression coverage.
- **FR-020**: `FollowUpHardening` MUST state the discovered problem, why it is
  outside feature 017, its owner or tracked boundary, and a re-evaluation trigger.
- **FR-021**: Affected guides and `examples/README.md` MUST describe startup,
  visible main area, operation, status feedback, description path, expected
  result, A11Y use, historical source, and known deviation in German first and
  English second at approximately CEFR-B2.
- **FR-022**: Feature evidence MUST trace every example and all 16 Tutorial
  tokens from historical source through framework decision, runtime behavior,
  primary smoke, rendered proof, documentation, validation, and follow-up.
- **FR-023**: User-facing status and descriptions MUST remain understandable by
  keyboard-only users, screen readers, Braille displays, and text browsers and
  MUST NOT rely only on color, layout, or pointer interaction.
- **FR-024**: The implementation MUST update project statistics, completion
  status, next-step routing, and active agent guidance context when affected.
- **FR-025**: The completed Lastenheft MUST be archived with the feature-branch
  suffix through the repository's rename workflow.
- **FR-026**: The feature MUST NOT re-port all Wave-1 behavior, add Wave-2/3/4
  functionality, require mouse operation, introduce new runtime dependencies,
  edit historical sources, or perform a broad framework revision.
- **FR-027**: Generated DocFX output, caches, logs, credentials, and validation
  output MUST remain untracked.

### Constitution Requirements *(mandatory)*

- **CR-001**: The TuiVision Level-2 environment and Constitution v1.14.0 are
  binding for this feature.
- **CR-002**: C#/.NET remains the primary implementation language and is the
  approved memory-safe language for managed runtime and smoke-test work.
- **CR-003**: NIST SSDF and CWE Top 25 review are mandatory; any narrow runtime
  change MUST receive secure-coding and malformed-state review proportional to
  its changed boundary.
- **CR-004**: OWASP ASVS is `N/A` unless the feature unexpectedly introduces a
  web, API, HTTP, authentication, or authorization surface.
- **CR-005**: Existing repository SBOM, VEX, SLSA, OpenSSF Scorecard, and supply-
  chain evidence remain applicable at repository level; feature-specific new
  evidence is `N/A` unless dependencies, packaging, release provenance, or
  distributed artifacts change.
- **CR-006**: AI is development tooling only. AI-SBOM is `N/A` unless models,
  datasets, inference services, AI infrastructure, or AI runtime components
  enter the released or operated product.
- **CR-007**: NIS2, CRA, EU AI Act, and DORA receive explicit screening. No new
  regulatory evidence is triggered by local example composition alone; scope
  change requires re-evaluation.
- **CR-008**: STRIDE, CIA, CAPEC, S-ADR, arc42 security concepts, Zero Trust,
  SAMM, BSI C3A, and BSI C5 MUST be screened. They are feature-level `N/A`
  unless trust boundaries, cloud services, provider dependencies, deployment
  topology, distributed flows, or security architecture change.
- **CR-009**: A11Y governance applies because terminal UI and learner-facing
  guides change; WCAG 2.2 AA, text-first review, keyboard paths, German-first/
  English-second CEFR-B2 content, DocFX, and axe evidence apply where triggered.
- **CR-010**: Cross-platform governance applies to runtime and test behavior on
  macOS, Linux, and Windows/WSL. New script-pair/man-page obligations are `N/A`
  unless script-shaped tooling is added or changed.
- **CR-011**: Agent parity governance applies to `AGENTS.md`, `CLAUDE.md`,
  `GEMINI.md`, `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md`; all maintained surfaces MUST be
  reviewed together when active feature context or shared guidance changes.
- **CR-012**: `.specify/templates/` are `N/A` unless this feature intentionally
  changes repository-owned Spec-Kit templates.

### Governance Applicability

| Preset | Version | Applicability | Feature boundary |
|---|---:|---|---|
| `security-governance` | 0.6.0 | Applicable | NIST SSDF/CWE review and repository supply-chain baseline apply; ASVS, new SBOM/VEX/SLSA/Scorecard evidence, AI-SBOM, and regulatory implementation are trigger-based `N/A`. |
| `architecture-governance` | 0.5.0 | Applicable | Narrow component/runtime decisions and risks are reviewed; cloud, distributed-system, provider, trust-boundary, BSI C3A, and BSI C5 changes are `N/A`. |
| `isaqb-architecture-governance` | 0.2.0 | Applicable | Existing runtime-view, quality-scenario, and architecture-risk evidence is checked proportionally; new S-ADR or broad arc42 work is trigger-based `N/A`. |
| `a11y-governance` | 0.4.0 | Applicable | Terminal keyboard operation, text-first fallbacks, bilingual guides, DocFX, and axe proof are acceptance concerns. |
| `cross-platform-governance` | 0.2.0 | Applicable | Runtime and smoke proof cover platform capability differences; script deliverables are `N/A` unless scripts change. |
| `agent-parity-governance` | 0.3.0 | Applicable | Maintained agent surfaces are reviewed and synchronized for changed active context; templates remain `N/A` unless intentionally touched. |

### Key Entities

- **Visual Example Area**: One of Desklogo, MsgCls, Tutorial, or Videomode with
  its historical intent, current runtime, three visible layers, and acceptance
  state.
- **Tutorial Step**: One token from `tvguid01` through `tvguid16`, with sequence,
  learning goal, visible result, operation, status, description, and proof.
- **Visible Proof Record**: Traceability from event/command/key route to concrete
  state, view-tree presence, rendered region/cells, and proof boundary.
- **Framework Usage Decision**: Exactly one of `UseExistingFramework`,
  `SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening`, with
  rationale and evidence.
- **Governance Checkpoint**: Preset/version checkpoint with applicability,
  rationale, owner, reviewer, evidence, result, residual risk, follow-up, and
  re-evaluation trigger.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All four Wave-1 example areas show a historically relevant visible
  main component or stable visible runtime state during normal startup.
- **SC-002**: All four example areas provide all three layers: main area, status
  line/equivalent status area, and keyboard-reachable description.
- **SC-003**: All 16 Tutorial tokens remain startable and each has a distinct
  visible learning goal and result; the acceptance matrix reports 16/16.
- **SC-004**: Every example has at least one documented primary app-loop smoke
  that verifies concrete state and visible view/render evidence after a real
  event, command, or key route.
- **SC-005**: Zero primary visual proofs rely only on startup, static text,
  history, private inspection, or direct-helper output.
- **SC-006**: Desklogo proves complete or controlled clipped logo visibility;
  MsgCls proves repeated visible routing; Videomode proves one honest result
  class and post-operation usability.
- **SC-007**: Every scoped example and every shared framework contract area has
  exactly one allowed framework decision and an evidence path.
- **SC-008**: Every newly visible historical deviation is documented against a
  reviewed read-only source, with zero changes under `tv203s/`.
- **SC-009**: All affected learner-facing documents pass bilingual CEFR-B2,
  semantic text-first, keyboard, DocFX, and web-A11Y checks required by their
  trigger conditions.
- **SC-010**: Targeted smoke tests, full Release tests, the canonical coverage
  gate, formatting verification, and diff hygiene pass without regression; all
  five gated assemblies remain at or above 70% line coverage.
- **SC-011**: Governance evidence covers all six installed presets with no empty
  applicability row; every `N/A` has rationale and re-evaluation trigger.
- **SC-012**: Completion evidence confirms no Wave-2/3/4 functionality, broad
  framework redesign, new runtime dependency, generated output, or historical
  source change entered the feature.

## Assumptions

- Feature 014 is the accepted functional baseline and is not reopened unless a
  visual path exposes a narrow blocking defect.
- Feature 013 supplies an accepted proof pattern, but Wave-1 code reuses only
  abstractions that fit the existing framework and does not copy example-local
  Wave-2 behavior mechanically.
- A real `TStatusLine` is preferred. An equivalent status area is acceptable
  only when the example's historical intent or framework constraints make it
  more appropriate and evidence records the reason.
- Tutorial steps need distinct learner-visible outcomes, not a mechanical
  line-by-line recreation of every historical implementation detail.
- Terminal capability differs by platform; honest deterministic fallback is a
  valid Videomode outcome and is not treated as failed parity.
- Documentation changes trigger DocFX and web-A11Y validation; XML/API changes
  are not expected but trigger the same path if they occur.
- The next prioritized intake after completion remains Wave 3 unless a bounded
  `FollowUpHardening` item is explicitly ranked higher by project governance.

## Out of Scope

- Functional re-porting already accepted by feature 014.
- Wave-2, Wave-3, or Wave-4 example delivery.
- Mandatory pointer or mouse operation.
- Broad framework, driver, rendering, or event-system redesign.
- New packages, services, databases, network dependencies, persistent user
  history, arbitrary user-file access, or runtime/product AI.
- Changes to historical sources under `tv203s/` or tracked generated DocFX/test
  output.
