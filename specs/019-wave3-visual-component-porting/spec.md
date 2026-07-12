# Feature Specification: Wave-3 Visual Component Porting

**Feature Branch**: `019-wave3-visual-component-porting`
**Created**: 2026-07-12
**Status**: Draft
**Binding Input**: `Lastenheft_Wave3-Visual-Component-Porting.md`

## Clarifications

### Session 2026-07-12 (Pass 1)

- Q: Welche finalen Projektordner sind bindend? → A: `examples/BHelp`,
  `examples/HelpDemo`, `examples/I18n`, `examples/TvEdit` und `examples/TvHc`.
- Q: Muss `BHelp` das historische proprietäre Borland-`.tch`-Format portieren?
  → A: Nein. Das Beispiel bewahrt Viewer-, Topic-, Kontext-, Such-/Fallback-
  und Navigationsabsicht über das vorhandene sichere TuiVision-Hilfemodell.
  Der fehlende Binärdecoder ist eine dokumentierte `IntentionalDeviation`.
- Q: Wie wird i18n unabhängig vom Host reproduzierbar? → A: Über den in
  Feature 018 akzeptierten expliziten, geordneten Resource-Lookup; weder
  Prozess-`LANG` noch gettext oder eine neue Native-Abhängigkeit sind nötig.
- Q: Welcher Datei-Sicherheitsvertrag gilt für `TvEdit`? → A: Normaler Start
  zeigt einen kontrollierten leeren oder eingebetteten Lernpuffer; Smokes
  öffnen und speichern ausschließlich source-controlled Fixtures oder Dateien
  in einem eigenen Test-Temp-Verzeichnis und prüfen Safe-Close explizit.
- Q: Welcher Ausgabe-Vertrag gilt für `TvHc`? → A: Die sichtbare Anwendung
  zeigt Eingabe, Erfolg oder stabile Diagnose und ein lesbares Topic. Schreibende
  Proofs dürfen ausschließlich ein Test-Temp-Ziel verwenden; kein beliebiger
  Nutzerpfad wird automatisch gelesen oder überschrieben.

### Session 2026-07-12 (Pass 2)

Keine weitere Frage würde Scope, Plan, Task-Zuschnitt, Validierung oder
Abnahme materiell ändern. Die fünf Entscheidungen oben, Feature 018 und die
verbindliche Drei-Schichten-Anforderung reichen für die Planung aus.

No further question would materially change scope, planning, task shaping,
validation, or acceptance. The five decisions above, Feature 018, and the
binding three-layer requirement are sufficient for planning.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Sichtbarer Editor-Vertical-Slice / Visible Editor Vertical Slice (Priority: P1)

Als lernende Person möchte ich `TvEdit` normal starten und ein echtes
Editorfenster mit Pufferinhalt, Cursor-/Modified-State, Statuszeile und sicherem
Quit-Pfad bedienen. / As a learner, I want to start `TvEdit` normally and use a
real editor window with buffer content, cursor/modified state, a status line,
and a safe quit path.

**Why this priority**: Der Editor ist der komplexeste End-to-End-Slice und
beweist früh, dass bestehende Framework-Verträge als sichtbare Anwendung
funktionieren. / The editor is the most complex end-to-end slice and proves
early that existing framework contracts work as a visible application.

**Independent Test**: Ein App-Loop-Smoke öffnet einen kontrollierten Puffer,
injiziert eine Bearbeitung, prüft Text, Modified-State, View-Baum und gerenderte
Zellen und löst anschließend die Safe-Close-Entscheidung aus.

**Acceptance Scenarios**:

1. **Given** `TvEdit` starts normally, **When** its first frame is rendered,
   **Then** a real editor window, current buffer identity, status line, and
   keyboard-reachable description are visible.
2. **Given** a controlled test buffer, **When** a key event edits it through
   the app loop, **Then** visible text and modified status both change.
3. **Given** unsaved changes, **When** quit or close is requested, **Then** the
   application exposes and respects an explicit safe-close decision without
   touching arbitrary user data.

---

### User Story 2 - Sichtbare Hilfe-Demos / Visible Help Demos (Priority: P1)

Als Reviewer möchte ich `BHelp` und `HelpDemo` über normale Menü-, Tastatur-
und Command-Pfade bedienen, damit Topic, Kontextwechsel, Cross-Reference,
Hint-Status und unbekannter Kontext sichtbar prüfbar sind. / As a reviewer, I
want to operate `BHelp` and `HelpDemo` through normal menu, keyboard, and
command paths so topic, context transitions, cross-references, hint status, and
unknown contexts are visibly verifiable.

**Why this priority**: Die sichtbaren Help-Flows sind der Nutzerbeweis für die
in Feature 018 gehärteten Help-Verträge. / Visible help flows are the user proof
for the help contracts hardened in Feature 018.

**Independent Test**: Beide Anwendungen besitzen getrennte App-Loop-Smokes,
die mindestens einen Topic-/Kontextwechsel und einen ehrlichen Fallback mit
konkretem View- und Cell-Proof prüfen.

**Acceptance Scenarios**:

1. **Given** `BHelp` starts, **When** its initial topic is rendered, **Then** a
   topic viewer, topic/context status, navigation command, and description are
   visible.
2. **Given** `HelpDemo` has focusable controls, **When** focus or a help command
   changes through real dispatch, **Then** the matching help context or hint is
   visible.
3. **Given** an unknown context or unresolved target, **When** it is requested,
   **Then** the demo shows a stable fallback and does not claim unavailable help.

---

### User Story 3 - Ressourcen, i18n und Compiler / Resources, i18n, and Compiler (Priority: P1)

Als Maintainer möchte ich `I18n` und `TvHc` mit kontrollierten Eingaben
bedienen, damit Sprachwahl, Fallback, Resource-Key, Compiler-Ergebnis und
Diagnose sichtbar und reproduzierbar bleiben. / As a maintainer, I want to use
`I18n` and `TvHc` with controlled inputs so language selection, fallback,
resource key, compiler result, and diagnostics remain visible and reproducible.

**Why this priority**: Diese Demos schließen die sichtbare Lücke zwischen den
018-APIs und einem lernbaren Anwendungsablauf. / These demos close the visible
gap between the 018 APIs and a learnable application flow.

**Independent Test**: `I18n` wechselt deterministisch zwischen neutraler und
alternativer Sprache einschließlich Fallback; `TvHc` kompiliert eine
source-controlled Quelle, zeigt Erfolg sowie Fehler und schreibt bei Bedarf nur
in ein Test-Temp-Ziel.

**Acceptance Scenarios**:

1. **Given** `I18n` starts, **When** the alternative language is selected,
   **Then** main content and status identify the selected language and matched
   resource key.
2. **Given** a missing localized key, **When** lookup runs, **Then** visible
   neutral fallback text and the fallback reason agree.
3. **Given** valid and invalid controlled help sources, **When** `TvHc`
   compiles them through application commands, **Then** success or stable
   diagnostic, source identity, and proof boundary are visible.

---

### User Story 4 - Text-first Lern- und Reviewpfad / Text-First Learning and Review Path (Priority: P2)

Als textorientiert arbeitende Person möchte ich jede Demo über Tastatur,
Statuszeile, Beschreibung, Guide und Evidence verstehen können, ohne dass
reine Textbeschreibung die sichtbare Hauptkomposition ersetzt. / As a
text-oriented user, I want to understand every demo through keyboard, status
line, description, guide, and evidence without text-only description replacing
the visible main composition.

**Independent Test**: Jeder Guide und jede Evidence-Zeile nennt Start,
Bedienpfad, Hauptfläche, Status, Beschreibung, historische Absicht,
Framework-Entscheidung, A11Y-Eigenschaft, Proof und Abweichung in Deutsch zuerst
und Englisch danach auf ungefähr CEFR-B2.

**Acceptance Scenarios**:

1. **Given** any Wave-3 guide, **When** it is read without color or pointer
   input, **Then** the main state and keyboard operation remain understandable.
2. **Given** a historical deviation, **When** evidence is reviewed, **Then**
   source, retained intent, modern behavior, rationale, and learner effect are
   traceable.
3. **Given** implementation completion, **When** the acceptance matrix is
   inspected, **Then** every example has one framework decision and complete
   app-loop, view-tree, buffer/cell, documentation, and validation evidence.

### Edge Cases

- A terminal is too small for a complete editor, help viewer, resource list, or
  compiler result area.
- `TvEdit` starts without a file, receives an unknown file argument, sees an
  external change, or is closed with unsaved edits.
- A help context is unknown, a topic has a missing cross-reference, or persisted
  help data is truncated or malformed.
- An i18n language or localized key is unavailable and must fall back through a
  deterministic ordered chain.
- A help source is empty, malformed, too large, contains an unresolved target,
  duplicate symbol, invalid explicit context, or invalid UTF-8.
- A visible view exists in the tree but is clipped or absent from rendered cells.
- A local helper duplicates reusable framework behavior or bypasses `app.Run()`.
- A discovered mouse, terminal/charset, runtime, or architecture issue is too
  broad for Wave 3 and must become `FollowUpHardening`.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST cover exactly `BHelp`, `HelpDemo`, `I18n`,
  `TvEdit`, and `TvHc` as .NET example projects under `examples/`.
- **FR-002**: Feature 018 and its editor, file, help compiler, resource, and i18n
  contracts MUST be the accepted technical baseline; the feature MUST NOT
  reimplement those contracts locally in examples.
- **FR-003**: Every example MUST show a visible main composition or stable
  runtime state during normal CLI startup.
- **FR-004**: Every example MUST implement the three-layer model: visible main
  area, real `TStatusLine` or a documented framework/historical equivalent, and
  keyboard-reachable Help, Description, or About content.
- **FR-005**: Startup success, static status text, direct helper output, or
  explanatory text alone MUST NOT count as primary visual proof.
- **FR-006**: Primary smokes MUST run `app.Run()` or an equivalent real
  application loop and inject events, commands, or keys through real dispatch.
- **FR-007**: Every primary smoke MUST verify concrete state, view-tree
  identity, and rendered buffer/cell visibility at a stable region. A missing
  proof layer requires an explicit reason, substitute proof, and follow-up boundary.
- **FR-008**: Direct helpers MAY support setup or supplemental assertions but
  MUST NOT be the primary acceptance proof.
- **FR-009**: `TvEdit` MUST expose a visible editor window, controlled initial
  buffer, cursor or selection state, modified state, file/buffer identity,
  edit command, safe-close decision, status feedback, and description path.
- **FR-010**: `TvEdit` acceptance MUST read only source-controlled fixtures,
  fixed repository paths, or its own test-temp files and MUST write only to its
  test-temp directory.
- **FR-011**: `BHelp` MUST show an initial topic, context/topic status,
  keyboard navigation, unknown-context fallback, and description path using the
  modern TuiVision help model.
- **FR-012**: `BHelp` MUST document the proprietary Borland `.tch` reader and
  search UI as historical context and `IntentionalDeviation`; no unsafe binary
  decoder or new dependency is required.
- **FR-013**: `HelpDemo` MUST show a help-context demonstration with focusable
  controls, context-aware hint/status feedback, a real command path, and a
  visible topic or fallback result.
- **FR-014**: `I18n` MUST show neutral and alternative language states plus a
  missing-key or unavailable-language fallback through explicit deterministic
  lookup independent of host locale.
- **FR-015**: `I18n` MUST expose selected language, matched resource key or
  fallback reason, main translated content, status feedback, and description.
- **FR-016**: `TvHc` MUST show a controlled help source, compile action, visible
  success or stable diagnostic, resulting topic/resource relationship, status,
  and description.
- **FR-017**: `TvHc` acceptance MUST use source-controlled inputs and write only
  to a dedicated test-temp target when persisted output is part of proof.
- **FR-018**: Unknown help contexts, missing references, missing resource keys,
  malformed or truncated data, invalid compiler source, and rejected file
  decisions MUST remain visible and testable rather than being swallowed.
- **FR-019**: Every example MUST be checked against relevant `.c`, `.cc`, `.h`,
  resource, PO, README, or fixture files under `tv203s/` as read-only intent evidence.
- **FR-020**: Every intentional historical deviation MUST record source,
  retained intent, modern behavior, rationale, and learner-visible effect.
- **FR-021**: Feature evidence MUST identify the existing framework components
  used for each example's main area, status, description, operation, file/help/
  resource flow, and smoke proof.
- **FR-022**: Every example MUST receive exactly one framework decision:
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or
  `FollowUpHardening`.
- **FR-023**: Reusable logic MUST NOT remain duplicated as local example logic;
  it MUST use a narrow `SmallFrameworkFix` with focused tests or be bounded as
  `FollowUpHardening`.
- **FR-024**: `FollowUpHardening` MUST state the discovered problem, why it is
  outside Feature 019, owner or tracked boundary, and re-evaluation trigger.
- **FR-025**: User-facing descriptions and guides MUST be German first and
  English second at approximately CEFR-B2 and remain usable by keyboard-only
  users, screen readers, Braille displays, and text browsers.
- **FR-026**: Each guide and `examples/README.md` MUST document startup,
  operation, visible main area, status, description, expected result, A11Y use,
  historical sources, known deviations, controlled I/O, and fallback boundaries.
- **FR-027**: `pr-evidence.md` MUST trace every example through historical
  intent, framework decision, primary proof, rendered evidence, safety/A11Y
  review, validation, residual risk, and follow-up.
- **FR-028**: Project statistics, Pflichtenheft completion/next-intake markers,
  and all affected maintained agent contexts MUST be updated together.
- **FR-029**: The completed Lastenheft MUST be archived with the exact
  `019-wave3-visual-component-porting` suffix through the repository workflow.
- **FR-030**: The feature MUST NOT add Wave-4 terminal/charset/emulation work,
  mandatory mouse support, TP7 follow-on examples, broad framework revision,
  arbitrary user-data access, new runtime dependencies, or edits under `tv203s/`.
- **FR-031**: Generated DocFX output, generated API YAML, caches, logs,
  credentials, test output, and validation output MUST remain untracked.
- **FR-032**: New or changed non-trivial logic MUST receive a selective
  didactic inline-comment review under the accepted moderate comment policy.

### Constitution Requirements *(mandatory)*

- **CR-001**: The TuiVision Level-2 environment and current Constitution are
  binding for this feature.
- **CR-002**: C#/.NET remains the approved memory-safe implementation language.
- **CR-003**: NIST SSDF, CWE Top 25, secure coding, input/path validation, and
  fail-safe error handling are applicable to the controlled file, help, resource,
  and compiler boundaries.
- **CR-004**: OWASP ASVS is `N/A` unless a web, API, HTTP, authentication, or
  authorization surface unexpectedly enters scope.
- **CR-005**: Repository SBOM, VEX, SLSA, OpenSSF Scorecard, and supply-chain
  controls remain applicable at repository level; feature-specific new evidence
  is `N/A` unless dependencies, packaging, provenance, or distributable scope changes.
- **CR-006**: AI-SBOM, NIS2, CRA, EU AI Act, and DORA are `N/A` for this local
  training/example scope while AI remains development tooling and no regulated,
  operated, or released AI component enters the product.
- **CR-007**: STRIDE/CIA/CAPEC review is proportional to controlled file and
  parser trust boundaries. S-ADR, arc42 security concept changes, Zero Trust,
  SAMM, BSI C3A, and BSI C5 are `N/A` unless architecture, cloud, provider,
  deployment, or distributed-service boundaries change.
- **CR-008**: iSAQB/arc42 goals, views, quality scenarios, risks, and debt MUST
  record that existing framework architecture is reused; a material architecture
  decision requires an S-ADR rather than local example special logic.
- **CR-009**: WCAG 2.2 AA applicability, text-first proof, keyboard access,
  German-first/English-second CEFR-B2 documentation, and selective didactic
  comments are applicable.
- **CR-010**: Cross-platform script governance is `N/A` unless a repository
  script is added or changed; test-temp and path behavior still require macOS,
  Linux, and Windows-compatible contracts.
- **CR-011**: Agent parity is applicable when active feature context or shared
  guidance changes; all five maintained agent surfaces MUST be reviewed together.
- **CR-012**: `.specify/templates/` are `N/A` unless this field run discovers a
  generic autonomous-workflow correction that is accepted through the separate
  retrospective PR, not silently inside Feature 019.
- **CR-013**: Every governance row MUST record `Applicable`, `N/A`, or `Open`
  with rationale, evidence path, owner, reviewer, result, residual risk,
  follow-up, and re-evaluation trigger.
- **CR-014**: Every remote or delivery task MUST name the exact repository
  evidence path that records its acceptance result.

### Key Entities

- **Wave3Example**: Project identity, historical sources, main component,
  status line, description route, framework decision, and primary proof.
- **VisibleState**: Main content, short status, focused view, rendered region,
  fallback class, and user operation that produced it.
- **ControlledArtifact**: Source-controlled fixture, embedded learning content,
  or test-temp file with explicit read/write ownership.
- **HelpDemoState**: Context, topic, cross-reference target, hint, and fallback.
- **LocalizationState**: Requested language, attempted languages, matched key,
  visible value, and fallback reason.
- **CompilerDemoState**: Source identity, compile result, diagnostics, generated
  topic/resource relation, and optional controlled output path.
- **FrameworkUsageDecision**: One allowed decision plus rationale, evidence,
  residual risk, and follow-up boundary.
- **ProofRecord**: App-loop route, concrete state, view-tree identity,
  buffer/cell region, validation command, and result.

## Success Criteria *(mandatory)*

- **SC-001**: All five documented CLI start commands launch the intended .NET
  project and show a recognizable main composition, status line, and description route.
- **SC-002**: Five of five examples have exactly one framework decision and
  complete historical-source, operation, status, description, and proof evidence.
- **SC-003**: Primary smoke coverage includes all five examples and every smoke
  uses the app loop or real dispatch plus concrete state, view-tree, and rendered
  buffer/cell proof, or an explicitly accepted proof boundary.
- **SC-004**: `TvEdit` proves edit, modified state, safe close, and controlled
  file ownership without reading or overwriting arbitrary user data.
- **SC-005**: `BHelp` and `HelpDemo` each prove a visible topic/context
  transition and an honest unavailable-help or fallback condition.
- **SC-006**: `I18n` proves neutral, alternative, and fallback states with
  deterministic host-independent results.
- **SC-007**: `TvHc` proves valid and invalid controlled input, stable
  diagnostics, readable compiled topic state, and test-temp-only writes.
- **SC-008**: Five guides plus `examples/README.md` pass German-first/English-
  second CEFR-B2, keyboard/text-first, semantic structure, and historical-
  deviation review.
- **SC-009**: Targeted Wave-3 smokes, full Release tests, the canonical coverage
  gate, format check, DocFX, web-A11Y, secret scan, and remote required checks
  pass, with every trigger and result recorded in `pr-evidence.md`.
- **SC-010**: The final diff contains no Wave-4, mandatory mouse, TP7, new
  dependency, arbitrary user-data, generated output, or `tv203s/` changes.
- **SC-011**: All six governance presets have complete applicability rows with
  no empty required field and no unjustified `Open` result.
- **SC-012**: All generated remote/delivery tasks name an exact evidence path,
  and repeated Analyze converges with no Critical, High, or unresolved Medium finding.
- **SC-013**: After authorized delivery, required checks are green, actionable
  review threads are zero, the merge and branch cleanup are recorded, and local
  clean `main` equals `origin/main`.

## Assumptions

- Feature 018 is the accepted technical prerequisite and requires no reopening.
- Existing TuiVision controls can compose the five demos; only narrow,
  test-backed framework fixes may be added when an actual reusable gap appears.
- The examples use deterministic embedded/source-controlled learning content
  for normal startup and controlled test-temp paths for write proof.
- Keyboard operation is mandatory; mouse-specific interaction remains Feature 020.
- Terminal/charset emulation and Wave-4 examples remain Features 021 and 022.
- Runtime/product AI, cloud services, external databases, and network services
  are absent.

## Out of Scope

- Wave-4 terminal, charset, font, or emulation behavior
- mandatory mouse operation or drag interaction
- TP7 follow-on examples from `TVDEMOS/` or `TVFM/`
- proprietary Borland `.tch` decoding
- broad editor/help/resource architecture revision
- arbitrary user-file discovery, persistent user history, database, network, or cloud services
- new runtime dependencies and generated documentation output in Git
- edits to historical sources under `tv203s/`
