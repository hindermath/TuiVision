# Feature Specification: Editor, Help, and Resources Hardening

**Feature Branch**: `018-editor-help-resources-hardening`
**Created**: 2026-07-12
**Status**: Draft
**Binding Input**: `Lastenheft_03_EditorHelpAndResourcesHardening.md`

## Relationship to Existing Work

Feature 004 remains the binding functional foundation for editor, file, help,
stream, and resource components. This feature is a bounded readiness layer: it
closes demonstrable end-to-end contracts needed by the later Wave-3 examples
without replacing Feature 004 or porting those examples.

## Clarifications

### Session 2026-07-12

- No formal question was required. Two focused autonomous clarification passes
  found no ambiguity that would materially change planning, task decomposition,
  validation, acceptance, or scope.
- The Feature 004 decisions remain binding: behavioral rather than byte-level
  historical compatibility, LF for new files, preserved loaded line endings,
  exact case-sensitive resource keys, preserved shared references without cycle
  support, and explicit overwrite decisions after external file changes.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Sicherer Editor-Anwendungspfad / Safe Editor Application Path (Priority: P1)

Als nutzende Person möchte ich ein Dokument öffnen, bearbeiten, durchsuchen,
ersetzen und speichern können, ohne ungespeicherte oder extern geänderte Daten
unbemerkt zu verlieren. / As a user, I want to open, edit, search, replace, and
save a document without silently losing unsaved or externally changed data.

**Why this priority**: Dieser zusammenhängende Pfad ist die Voraussetzung für
einen dünnen `tvedit`-Port und schützt reale Nutzerdaten. / This coherent path
is the prerequisite for a thin `tvedit` port and protects real user data.

**Independent Test**: Ein temporäres Dokument wird über Editorfenster und
Shell-Kommandos geöffnet, geändert, durchsucht, ersetzt und gespeichert; Safe-
Close sowie externe Änderung werden separat mit expliziten Entscheidungen
geprüft.

**Acceptance Scenarios**:

1. **Given** an existing document, **When** it is opened, edited, searched,
   replaced, and saved through the application path, **Then** content, cursor,
   selection, modified state, title, and command availability remain coherent.
2. **Given** unsaved changes, **When** close or document replacement is
   requested, **Then** an explicit save, discard, or cancel decision is required
   and cancellation preserves the current session.
3. **Given** a loaded file changed externally, **When** save is requested,
   **Then** the file is not overwritten until an explicit conflict decision is
   accepted.
4. **Given** a save target is invalid or unwritable, **When** saving fails,
   **Then** the editor keeps its content and modified state and exposes a useful
   failure result.

---

### User Story 2 - Navigierbare Laufzeithilfe / Navigable Runtime Help (Priority: P1)

Als lernende Person möchte ich kontextsensitive Hilfe öffnen, Querverweisen
folgen und bei unbekannten Themen eine verständliche Rückmeldung erhalten. / As
a learner, I want to open context-sensitive help, follow cross-references, and
receive understandable feedback for unknown topics.

**Why this priority**: `bhelp` und `helpdemo` dürfen Navigation und Fallbacks
nicht als lokale Sonderlogik neu erfinden. / `bhelp` and `helpdemo` must not
reinvent navigation and fallbacks as local special logic.

**Independent Test**: Eine gespeicherte Hilfedatei wird geladen, ein Kontext
geöffnet, ein gültiger Querverweis aktiviert, zurück navigiert und anschließend
ein fehlender Kontext angefordert.

**Acceptance Scenarios**:

1. **Given** a valid persisted help file, **When** a known context is opened,
   **Then** the matching topic appears with stable title, paragraphs, and
   selectable references.
2. **Given** a valid cross-reference, **When** it is activated through the
   normal view/event route, **Then** the target topic opens and back navigation
   restores the preceding topic.
3. **Given** an unknown context or reference target, **When** navigation is
   attempted, **Then** the viewer shows bounded fallback content and remains
   usable rather than presenting an empty or broken state.
4. **Given** structurally invalid help content, **When** it is loaded, **Then**
   the failure is explicit and partial content is not exposed as valid help.

---

### User Story 3 - Gemeinsamer Help-Compiler-Vertrag / Shared Help Compiler Contract (Priority: P2)

Als Maintainer möchte ich eine textuelle Hilfebeschreibung deterministisch in
dieselbe persistierte Help- und Ressourcenstruktur übersetzen, die die Laufzeit
liest. / As a maintainer, I want to compile a textual help description
deterministically into the same persisted help and resource structure consumed
by the runtime.

**Why this priority**: `tvhc` bleibt nur dann ein dünner Port, wenn Compiler und
Laufzeit Typregistrierung, Kontext-IDs und Fehlersemantik teilen. / `tvhc`
remains a thin port only when compiler and runtime share type registration,
context identifiers, and failure semantics.

**Independent Test**: Eine kleine gültige Quelle mit zwei Themen und einem
Querverweis wird übersetzt, persistiert, neu geladen und über die Laufzeithilfe
navigiert; ungültige Quellen werden mit positionsbezogenen Diagnosen abgelehnt.

**Acceptance Scenarios**:

1. **Given** a valid help source, **When** it is compiled twice, **Then** both
   results represent the same topics, contexts, references, and resource names.
2. **Given** compiled help output, **When** it is loaded by the runtime path,
   **Then** no compiler-only conversion or example-local registry is required.
3. **Given** duplicate contexts, unresolved references, malformed directives,
   or invalid resource names, **When** compilation runs, **Then** it rejects the
   source with a clear location and reason and emits no apparently valid output.

---

### User Story 4 - Sprachabhängiger Ressourcen-Lookup / Language-Aware Resource Lookup (Priority: P2)

Als Anwendungsentwickler möchte ich Ressourcen über exakte Namen und eine
klar definierte Sprach-Fallback-Reihenfolge abrufen können, damit `i18n` keine
eigene Lookup-Logik benötigt. / As an application developer, I want to retrieve
resources by exact names and a defined language fallback order so that `i18n`
does not need its own lookup logic.

**Why this priority**: Wiederverwendbare Lookup-Semantik verhindert
widersprüchliche Sprach- und Fehlerpfade in späteren Beispielen. / Reusable
lookup semantics prevent conflicting language and failure paths in later
examples.

**Independent Test**: Eine Ressourcensammlung mit neutraler und
sprachspezifischer Variante wird gespeichert und neu geladen; exakte Variante,
Fallback, Groß-/Kleinschreibung und fehlende Ressource werden geprüft.

**Acceptance Scenarios**:

1. **Given** an exact language-specific resource exists, **When** it is
   requested, **Then** that exact resource is returned.
2. **Given** the requested language variant is absent, **When** a configured
   fallback or neutral resource exists, **Then** the documented fallback order
   selects it deterministically.
3. **Given** no accepted candidate exists, **When** lookup is attempted, **Then**
   a distinguishable missing-resource result is returned without silently
   choosing an unrelated key or language.
4. **Given** keys differ only by case, **When** lookup, replacement, removal, or
   enumeration runs, **Then** exact case-sensitive semantics are preserved.

---

### User Story 5 - Harte Persistenzfehler / Hard Persistence Failures (Priority: P2)

Als Maintainer möchte ich beschädigte oder mehrdeutige Daten zuverlässig
ablehnen, damit Beispiele keine Teilobjekte als gültigen Zustand anzeigen. / As
a maintainer, I want corrupted or ambiguous data to be rejected reliably so
examples never present partial objects as valid state.

**Why this priority**: Editor-, Hilfe- und Ressourcenflüsse teilen denselben
Persistenz-Unterbau; stille Annahmen würden alle späteren Beispiele schwächen. /
Editor, help, and resource flows share the same persistence foundation; silent
acceptance would weaken every later example.

**Independent Test**: Trunkierte Daten, unbekannte Typen, Zyklen, trailing data,
ungültige Cross-References und fehlende Ressourcen werden einzeln eingespeist
und mit stabilen Fehlergrenzen geprüft.

**Acceptance Scenarios**:

1. **Given** truncated, trailing, unknown-type, cyclic, or structurally invalid
   input, **When** it is read, **Then** the complete operation fails explicitly
   and no partial result is committed.
2. **Given** a failure is presented by a later application, **When** the reusable
   contract is inspected, **Then** the original failure kind remains available
   and is not reduced to a generic success or empty result.

### Edge Cases

- Empty and very long editor documents, mixed line endings, and empty searches.
- Close cancellation followed by another edit or save attempt.
- External change with unchanged timestamp or changed length/content snapshot.
- Help topics without references and references at text boundaries.
- Duplicate, negative, missing, or overflowing context identifiers.
- Compiler input with forward references, duplicate symbols, malformed UTF-8,
  no final newline, and a failed destination write.
- Language tags with region subtags, unsupported variants, neutral resources,
  and keys differing only by case.
- Resource containers with zero entries, duplicate persisted keys, invalid entry
  counts, unknown registered types, cycles, truncation, and trailing bytes.
- A proposed fix would require example porting, broad serialization redesign,
  mouse support, or terminal/charset work.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Feature 004 MUST remain the functional foundation; feature 018
  MUST close only Wave-3-readiness gaps and MUST NOT recreate its broad plan.
- **FR-002**: The feature MUST assess the current repository before changes and
  classify each editor, file, help, compiler, resource, and i18n contract as
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or
  `FollowUpHardening`.
- **FR-003**: Every contract area MUST identify the reusable framework surface,
  any local special logic, its decision, focused proof, and follow-up boundary.
- **FR-004**: The editor proof MUST cover open, edit, insert/overwrite,
  selection, search/replace, save, modified state, title/command synchronization,
  safe close, and external-change overwrite decisions as coherent application
  flows rather than unrelated unit assertions.
- **FR-005**: A failed or cancelled save/close operation MUST preserve document
  content, target identity, modified state, and continued usability.
- **FR-006**: Loaded files MUST preserve their detected line-ending convention;
  new files MUST use LF unless the user explicitly selects another supported
  behavior already accepted by Feature 004.
- **FR-007**: The help proof MUST cover persisted context lookup, viewer/window
  presentation, selectable cross-references, back navigation, missing-context
  fallback, and invalid-reference rejection through reusable components.
- **FR-008**: The feature MUST provide or close a reusable deterministic path
  from bounded textual help source to the persisted help/resource model consumed
  by the runtime.
- **FR-009**: Compiler and runtime MUST share context, topic, cross-reference,
  type-registration, and resource-name semantics; examples MUST NOT maintain a
  second incompatible registry or data model.
- **FR-010**: Compiler failures MUST identify the source location and reason for
  duplicate contexts, unresolved references, malformed declarations, invalid
  encoding, or invalid resource names, and MUST NOT publish partial output.
- **FR-011**: Resource lookup MUST retain exact case-sensitive keys and define a
  deterministic language selection order: exact requested language, configured
  parent/fallback languages in order, then neutral resource, otherwise missing.
- **FR-012**: Missing language or resource candidates MUST remain distinguishable
  from empty valid resource values and MUST NOT silently select unrelated data.
- **FR-013**: Truncated streams, trailing data, unknown types, cyclic graphs,
  invalid counts, duplicate persisted keys, invalid cross-references, malformed
  compiler source, and missing resources MUST have explicit negative proof.
- **FR-014**: Persistence failures MUST be atomic: no partially reconstructed or
  partially emitted graph MAY be exposed as a valid committed result.
- **FR-015**: Reusable logic MUST reside in the framework or be routed to
  `FollowUpHardening`; later Wave-3 examples MAY only compose these contracts.
- **FR-016**: A `SmallFrameworkFix` MUST be narrow, required for one accepted
  contract, and protected by focused regression proof.
- **FR-017**: `IntentionalDeviation` MUST name historical intent, modern
  behavior, rationale, and user-visible or proof-visible effect.
- **FR-018**: `FollowUpHardening` MUST identify the issue, why it exceeds 018,
  owner or tracked boundary, and re-evaluation trigger.
- **FR-019**: Relevant historical `.c`, `.cc`, and required headers under
  `tv203s/` MUST be reviewed as read-only intent references for editor, help,
  resource, and compiler contracts.
- **FR-020**: Feature evidence MUST trace every contract area from Feature 004
  and historical intent through decision, change, positive/negative proof,
  validation, residual risk, and follow-up.
- **FR-021**: New or changed non-trivial logic MUST be reviewed for selective
  didactic inline-comment value under the Feature 015 rules.
- **FR-022**: User-facing diagnostics, guides, and evidence MUST be text-first,
  keyboard-understandable, German first and English second at CEFR-B2 level,
  without relying only on color, layout, or pointer input.
- **FR-023**: Project statistics, completion routing, Pflichtenheft markers, and
  maintained agent guidance MUST be updated together when affected.
- **FR-024**: The completed Lastenheft MUST be archived with the exact feature-
  branch suffix through the repository rename workflow.
- **FR-025**: Wave-3 example implementation, Wave-4 terminal/charset work,
  runtime mouse support, TP7 emulation, new external services, broad framework
  restructuring, and new runtime dependencies MUST remain outside this feature.
- **FR-026**: Historical sources, generated DocFX output, caches, logs,
  credentials, and validation output MUST remain unmodified or untracked.

### Constitution Requirements *(mandatory)*

- **CR-001**: The TuiVision Level-2 environment, Constitution v1.14.0, AGENTS
  guidance, and all six installed governance presets are binding.
- **CR-002**: C#/.NET is the primary implementation language and approved
  memory-safe language for the managed runtime and tests.
- **CR-003**: NIST SSDF and CWE Top 25 review are mandatory; malformed input,
  path handling, resource exhaustion, atomic output, and exception boundaries
  MUST receive proportional secure-coding review.
- **CR-004**: OWASP ASVS is `N/A` unless a web, HTTP, API, authentication, or
  authorization surface enters scope; that trigger would require re-evaluation.
- **CR-005**: Existing SBOM, VEX, SLSA, OpenSSF Scorecard, and repository supply-
  chain evidence remain applicable; new feature evidence is `N/A` unless
  dependencies, packaging, release provenance, or distributable scope changes.
- **CR-006**: AI is development tooling only. AI-SBOM is `N/A` unless runtime
  models, services, datasets, inference infrastructure, or delivered AI
  components enter the product.
- **CR-007**: Regulatory screening for NIS2, CRA, EU AI Act, and DORA MUST be
  recorded; feature-specific controls are `N/A` absent new operated services,
  regulated product claims, AI runtime, or financial-service boundaries.
- **CR-008**: STRIDE/CIA/CAPEC review MUST cover file, parser, persistence, and
  resource trust boundaries. Zero Trust, BSI C3A, and BSI C5 are `N/A` absent a
  cloud service, provider dependency, identity perimeter, deployment topology,
  or distributed service flow.
- **CR-009**: Architecture evidence MUST state whether bounded S-ADR, arc42,
  quality-scenario, or SAMM updates are required; no broad architecture rewrite
  is implied by this feature.
- **CR-010**: A11Y governance applies to text-first diagnostics, keyboard help
  navigation, bilingual learner guidance, and didactic comment review. XML/API
  changes trigger DocFX plus the web A11Y path.
- **CR-011**: Cross-platform governance applies only if repository scripts are
  added or changed; otherwise Bash/PowerShell artifact parity is `N/A` with the
  script-change trigger recorded.
- **CR-012**: Agent parity applies if shared workflow, active-feature, or
  implementation guidance changes; all maintained agent surfaces MUST then be
  reviewed together. `.specify/templates/` remain `N/A` unless intentionally
  changed.
- **CR-013**: Default governance evidence under `docs/security/` and feature-
  local `pr-evidence.md` MUST be used or an equivalent location justified.

### Key Entities

- **Editor Session**: Document content, cursor, selection, modified state, file
  identity, disk snapshot, line-ending convention, and pending decision.
- **Help Source Unit**: Textual declaration of a topic, context, paragraphs,
  references, symbols, and source locations before compilation.
- **Compiled Help Model**: Runtime-readable topic/index graph plus exact resource
  identity produced atomically from valid source.
- **Language Resource Request**: Exact base key, requested language, ordered
  fallback languages, neutral fallback, and distinguishable missing result.
- **Contract Evidence Row**: Area, existing surface, historical source,
  decision, change, positive and negative proof, validation, residual risk, and
  follow-up boundary.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All six contract areas (editor, file, help, compiler, resources,
  i18n) have exactly one accepted framework decision and traceable evidence.
- **SC-002**: Automated end-to-end proof completes the coherent editor flow,
  safe-close cancellation, external-change rejection, and failed-save recovery
  without data loss.
- **SC-003**: Automated help proof compiles or loads persisted content, opens a
  context, follows and returns from a reference, and handles missing and invalid
  references without an empty or broken viewer.
- **SC-004**: The same valid help source compiled twice yields logically
  identical topics, contexts, references, and resource names; all named invalid
  source classes fail without accepted partial output.
- **SC-005**: Resource/i18n proof demonstrates exact language, ordered fallback,
  neutral fallback, missing result, and case-sensitive key separation.
- **SC-006**: Every required malformed persistence class is rejected explicitly
  and atomically, with zero partial results accepted as valid.
- **SC-007**: `bhelp`, `helpdemo`, `i18n`, `tvedit`, and `tvhc` can each be
  planned as thin composition over named reusable contracts, with no example-
  local replacement behavior required.
- **SC-008**: All changed projects pass focused and full required validation;
  each gate-relevant assembly remains at or above 70% line coverage.
- **SC-009**: All user-facing feature documentation is German-first/English-
  second CEFR-B2 and remains understandable through text and keyboard paths.
- **SC-010**: The final diff contains no Wave-3 example port, mouse, terminal,
  charset, dependency, generated-output, or historical-source changes.

## Assumptions

- Feature 004 contracts and its accepted line-ending, exact-key, shared-
  reference, and explicit-overwrite decisions remain valid.
- Real user files are not needed for deterministic proof; tests use isolated
  temporary directories and source-controlled fixtures.
- Behavioral compatibility with historical Turbo Vision is required; byte-for-
  byte compatibility with historical help or resource files is not required.
- Forward references in bounded help source are acceptable when all targets are
  resolved before output is committed.
- The ordered language fallback list is application-selected; no ambient host
  locale lookup or persistent user preference is required by this feature.
- Existing public contracts are preferred. Public API additions are allowed
  only when necessary for reusable Wave-3 readiness and receive XML, DocFX, and
  A11Y evidence.

## Scope Boundaries

### In Scope

- Bounded framework and proof hardening for editor, file, help, compiler,
  resource, i18n, and malformed-state integration contracts.
- Feature evidence, focused guides, project statistics, completion routing, and
  affected synchronized agent guidance.

### Out of Scope

- Porting `bhelp`, `helpdemo`, `i18n`, `tvedit`, or `tvhc`.
- Mouse support, terminal emulation, charset/font work, Wave 4, TP7 emulation,
  cloud services, external databases, broad persistence redesign, and new
  runtime dependencies.

### Decision and Follow-up Model

- `UseExistingFramework`: Current reusable component and proof are sufficient.
- `SmallFrameworkFix`: A narrow reusable correction is required and tested.
- `IntentionalDeviation`: Modern behavior intentionally differs from historical
  intent and its effect is documented.
- `FollowUpHardening`: The discovered issue exceeds feature 018 and receives a
  named owner/boundary plus re-evaluation trigger.
