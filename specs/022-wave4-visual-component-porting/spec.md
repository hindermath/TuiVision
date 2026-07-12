# Feature Specification: Wave-4 Visual Component Porting

**Feature Branch**: `022-wave4-visual-component-porting`
**Created**: 2026-07-12
**Status**: Draft
**Binding Input**: `Lastenheft_Wave4-Visual-Component-Porting.md`

## Clarifications

### Session 2026-07-12 (Pass 1)

- Q: Welche Projektordner sind bindend? → A: `examples/Cyrillic`,
  `examples/ETerm`, `examples/Fonts`, `examples/Terminal` und `examples/XTerm`.
- Q: Darf `Terminal` einen Prozess, eine Shell oder ein PTY starten? → A: Nein.
  Die Demo verwendet ausschließlich die kontrollierte In-Process-Sitzung aus
  Feature 021 und zeigt Eingabe, Ausgabe, Cursor, Attribute und Status sichtbar.
- Q: Müssen ETerm- und XTerm-Legacy-Konfigurationssyntax vollständig geparst
  werden? → A: Nein. Repräsentative historische Werte werden als
  source-controlled, unveränderliche Demo-Manifeste mit Herkunft und
  `IntentionalDeviation` gezeigt; kein allgemeiner Legacy-Parser entsteht.
- Q: Welcher Font-Nachweis ist bindend? → A: `Fonts` besitzt eine kontrollierte
  exakte 8x16-/256-Glyphen-/4.096-Byte-Fixture, validiert sie über den 021-Vertrag
  und rendert mindestens eine erkennbare Glyphenmatrix. Generatoren und
  Host-Fontinstallation bleiben ausgeschlossen.
- Q: Wie wird Plattform-Evidence klassifiziert? → A: `DeterministicInProcess`,
  `RemoteCI` und `PhysicalObservation` bleiben getrennt. Nicht verfügbare
  physische Bedingungen sind `NotRun`, nie ein implizites Pass.
- Q: Welche Bedien- und Datengrenze gilt? → A: Tastaturbedienung ist
  verpflichtend; Maus ist ergänzend. Alle Assets sind eingebettet oder
  source-controlled und read-only. Kein Beispiel liest beliebige Nutzerpfade,
  schreibt Nutzerdateien oder verändert Host-Terminal, Font, Codepage oder
  Keyboardmap.
- Q: Welche kleine Bildschirmgrenze gilt? → A: Der normale Proof nutzt eine
  stabile Standardfläche; bei engem Viewport bleiben Identität, Status,
  Beschreibungspfad und ehrlicher Fallback textorientiert erkennbar, auch wenn
  Raster oder Listen gekürzt werden.

### Session 2026-07-12 (Pass 2)

Keine weitere Frage würde Scope, Plan, Task-Zuschnitt, Validierung oder
Abnahme materiell ändern. Die sieben Entscheidungen oben, die 021-Verträge und
das verbindliche Drei-Schichten-Modell reichen für die Planung aus.

No further question would materially change scope, planning, task shaping,
validation, or acceptance. The seven decisions above, the Feature-021
contracts, and the binding three-layer model are sufficient for planning.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Sichtbare Terminal-Sitzung / Visible Terminal Session (Priority: P1)

Als lernende Person möchte ich `Terminal` normal starten und eine echte
TuiVision-Terminalansicht mit kontrollierter Eingabe, sichtbarer Ausgabe,
Cursor, Status und Beschreibung bedienen, ohne dass ein Hostprozess gestartet
wird. / As a learner, I want to start `Terminal` normally and use a real
TuiVision terminal view with controlled input, visible output, cursor, status,
and description without starting a host process.

**Why this priority**: Dieser Slice beweist früh, dass die in Feature 021
gehärteten Verträge in einer normalen sichtbaren Anwendung funktionieren.

**Independent Test**: Ein App-Loop-Smoke injiziert Text und einen
Cursorbefehl, prüft Session-Zustand, konkrete View-Identität, Statuszeile und
gerenderte Zellen und beendet die Anwendung über einen echten Quit-Pfad.

**Acceptance Scenarios**:

1. **Given** `Terminal` starts normally, **When** its first frame is rendered,
   **Then** the terminal view, welcome output, cursor, capability/profile status,
   and keyboard-reachable description are visible.
2. **Given** the application loop is running, **When** controlled text and a
   supported cursor action are injected, **Then** session state, cursor status,
   view tree, and rendered cells agree.
3. **Given** terminal capability is unavailable, **When** the demo starts,
   **Then** a stable text-first fallback remains visible and keyboard quit works.

---

### User Story 2 - Kyrillisch und Font-Raster / Cyrillic and Font Grids (Priority: P1)

Als Nutzerin möchte ich `Cyrillic` und `Fonts` starten und erkennbare Zeichen-
beziehungsweise Pixelraster sehen, damit direkte Abbildung, Ersatzzeichen,
Fontquelle und Grenzen verständlich werden. / As a user, I want to start
`Cyrillic` and `Fonts` and see recognizable character or pixel grids so direct
mapping, replacement, font source, and boundaries are understandable.

**Why this priority**: Diese beiden Beispiele liefern den sichtbaren
Lernnachweis für Charset- und Fontverträge statt nur Metadaten zu behaupten.

**Independent Test**: Getrennte App-Loop-Smokes prüfen bekannte kyrillische
Zellen, Mapping-/Fallbackstatus sowie eine validierte Font-Glyphe mit
konkreten Rasterzellen, Status und Beschreibung.

**Acceptance Scenarios**:

1. **Given** `Cyrillic` starts, **When** the KOI8-R sample is mapped, **Then**
   known Cyrillic characters, charset identity, mapping result, and fallback
   explanation are visible without host-locale dependence.
2. **Given** an invalid or unsupported source value, **When** mapping runs,
   **Then** U+FFFD or `Unsupported` is shown consistently in state, status, and cells.
3. **Given** `Fonts` starts with its controlled fixture, **When** the selected
   glyph is rendered, **Then** fixture identity, exact metadata, and a recognizable
   8x16 pixel matrix are visible.

---

### User Story 3 - ETerm- und XTerm-Resource-Demos / ETerm and XTerm Resource Demos (Priority: P1)

Als Maintainer möchte ich `ETerm` und `XTerm` sinnvoll starten können, obwohl
die historischen Quellen überwiegend Konfiguration oder Resources sind, damit
die Portierung weder leer noch fälschlich als vollständiger Emulator erscheint.
/ As a maintainer, I want to start `ETerm` and `XTerm` meaningfully even though
their historical sources are mainly configuration or resources, so the port is
neither empty nor presented as a complete emulator.

**Why this priority**: Sichtbare, begrenzte Resource-Manifeste erhalten die
historische Lehrabsicht ohne neue unsichere Parser- oder Hostgrenzen.

**Independent Test**: Jede Demo zeigt mehrere repräsentative benannte Werte,
Herkunft, unterstützte Teilmenge, bewusste Abweichung und Fallback; der Smoke
prüft Zustand, View, Status und Zellen durch den App-Loop.

**Acceptance Scenarios**:

1. **Given** `ETerm` starts, **When** its manifest is displayed, **Then** menu,
   theme, source identity, and resource-only deviation are visible.
2. **Given** `XTerm` starts, **When** its manifest is displayed, **Then** named
   resource values, supported sequence subset, and unsupported host boundaries
   are visible.
3. **Given** a requested manifest item is outside the accepted subset, **When**
   it is selected, **Then** the demo shows a stable unsupported/fallback result
   rather than guessing or parsing arbitrary data.

---

### User Story 4 - Plattformbewusster Lernpfad / Platform-Aware Learning Path (Priority: P2)

Als textorientiert arbeitende Person möchte ich jede Wave-4-Demo über
Tastatur, Statuszeile, Beschreibung, Guide und Evidence verstehen können und
erkennen, welche Aussagen deterministisch, remote oder physisch geprüft wurden.
/ As a text-oriented user, I want to understand every Wave-4 demo through
keyboard, status line, description, guide, and evidence and see which claims
were proven deterministically, remotely, or physically.

**Independent Test**: Jeder Guide und jede Evidence-Zeile nennt Start,
Hauptfläche, Bedienpfad, Status, Beschreibung, historische Quelle,
Framework-Entscheidung, Host-Evidence-Klasse, Fallback und Proof in Deutsch
zuerst und Englisch danach auf ungefähr CEFR-B2.

**Acceptance Scenarios**:

1. **Given** any Wave-4 demo or guide, **When** it is used without color or
   pointer input, **Then** purpose, current state, next action, and fallback
   remain understandable.
2. **Given** a physical host condition was unavailable, **When** evidence is
   reviewed, **Then** it is `NotRun` with residual risk and re-evaluation trigger.
3. **Given** implementation completion, **When** the acceptance matrix is
   inspected, **Then** all five examples have historical, framework, app-loop,
   view, cell, A11Y, host, and validation evidence.

### Edge Cases

- A viewport is too small for the full terminal, character grid, glyph raster,
  or resource list.
- A terminal sequence is malformed, truncated, unsupported, overlong, or
  outside the Feature-021 subset.
- A KOI8-R value maps directly, is replaced, is invalid, or names an unsupported charset.
- A font fixture has the wrong length, geometry, format, source identity, or
  contains a blank selected glyph.
- An ETerm/XTerm manifest item is absent, duplicated, outside the accepted
  representative set, or mistakenly presented as native configuration support.
- Host capability is disabled, unsupported, redirected, or physically unobserved.
- A visible view exists in the tree but is clipped or absent from rendered cells.
- A helper bypasses `app.Run()` or duplicates reusable 021 framework behavior.
- A discovered full-emulation, native-resource, process, host-mutation, A11Y,
  or architecture gap is too broad for 022 and must become `FollowUpHardening`.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The feature MUST cover exactly `Cyrillic`, `ETerm`, `Fonts`,
  `Terminal`, and `XTerm` as startable example projects under `examples/`.
- **FR-002**: Feature 021 session, emulation, mapping, font-fixture, profile,
  host-evidence, and Controls contracts MUST be the accepted technical baseline;
  examples MUST NOT reimplement those reusable contracts locally.
- **FR-003**: Every example MUST show a visible terminal, charset, font, or
  resource composition during normal CLI startup, or a stable visible fallback.
- **FR-004**: Every example MUST implement the three-layer model: visible main
  area, real status line or equivalent framework status surface, and
  keyboard-reachable Help, Description, or About content.
- **FR-005**: Startup success, static status, direct helper output, explanatory
  text alone, or host screenshot alone MUST NOT count as primary visual proof.
- **FR-006**: Primary smokes MUST run `app.Run()` or an equivalent real
  application loop and inject events, commands, or keys through real dispatch.
- **FR-007**: Every primary smoke MUST verify concrete state, exact view-tree
  identity, and rendered buffer/cell visibility at stable positions or regions.
- **FR-008**: Direct helpers MAY support deterministic setup or supplemental
  assertions but MUST NOT be the primary acceptance proof.
- **FR-009**: `Terminal` MUST expose a visible `TTerminalView`-equivalent
  composition with controlled input/output, cursor, attributes, session/profile/
  capability status, quit, and description without process, shell, or PTY access.
- **FR-010**: `Terminal` MUST visibly prove at least plain text, one accepted
  cursor or attribute action, one rejected or unsupported action, reset or
  fallback state, and a usable next independent input.
- **FR-011**: `Cyrillic` MUST show a deterministic KOI8-R/Unicode character grid
  with direct, replacement, invalid, and unsupported outcomes independent of host locale.
- **FR-012**: `Cyrillic` MUST expose source charset, source value, visible glyph,
  mapping outcome, fallback reason, status, and description.
- **FR-013**: `Fonts` MUST validate one exact source-controlled raw
  8x16/256-glyph/16-byte-stride/4,096-byte fixture before displaying it.
- **FR-014**: `Fonts` MUST show fixture identity, metadata, selected glyph,
  recognizable pixel rows, status, description, and invalid-fixture fallback.
- **FR-015**: Historical font generators, setup scripts, compressed/native font
  formats, host font installation, and automatic host font restoration MUST NOT run.
- **FR-016**: `ETerm` MUST show a source-controlled immutable manifest with
  representative historical menu, theme, and terminal-presentation values.
- **FR-017**: `XTerm` MUST show a source-controlled immutable manifest with
  representative resource values, the accepted 021 sequence/capability subset,
  and explicit unsupported host/native-resource boundaries.
- **FR-018**: ETerm/XTerm legacy syntax parsing MUST be an
  `IntentionalDeviation`; examples MUST NOT claim general native config or resource support.
- **FR-019**: Missing or out-of-subset manifest values MUST produce stable
  visible unsupported/fallback state without arbitrary file parsing.
- **FR-020**: All example assets MUST be embedded or source-controlled and
  read-only; no example may discover arbitrary user paths, persist user data,
  or mutate terminal, font, codepage, locale, keyboard map, or audio state.
- **FR-021**: macOS, Linux, Windows/WSL, redirected/headless, and unavailable
  capability conditions MUST have reviewable host rows with explicit evidence class.
- **FR-022**: Deterministic in-process, remote CI, and physical observations
  MUST remain separate; an unavailable physical condition MUST be `NotRun`.
- **FR-023**: Every example MUST be checked against relevant `.c`, `.cc`, `.h`,
  config, resource, README, script, and fixture files under `tv203s/` read-only.
- **FR-024**: Every intentional historical deviation MUST record source,
  retained intent, modern behavior, rationale, and learner-visible effect.
- **FR-025**: Feature evidence MUST identify the existing framework components
  used for main area, status, description, operation, host fallback, and proof.
- **FR-026**: Every example MUST receive exactly one framework decision:
  `UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or
  `FollowUpHardening`.
- **FR-027**: Reusable logic MUST NOT remain duplicated in example-local code;
  it MUST use existing framework, a narrow tested fix, or a named follow-up.
- **FR-028**: `FollowUpHardening` MUST name the issue, out-of-scope reason,
  owner or tracked boundary, residual risk, and re-evaluation trigger.
- **FR-029**: Every example MUST remain keyboard operable. Mouse support MAY
  supplement interaction but MUST NOT be required to reach primary behavior,
  description, fallback, or quit.
- **FR-030**: Descriptions, guides, and `examples/README.md` MUST be German
  first and English second at approximately CEFR-B2 and remain text-first usable.
- **FR-031**: `pr-evidence.md` MUST trace every example through historical
  intent, framework decision, visible state, app-loop/view/cell proof, host
  evidence, safety/A11Y review, validation, residual risk, and follow-up.
- **FR-032**: New or changed non-trivial logic MUST receive selective didactic
  inline-comment review under the accepted moderate reason-focused policy.
- **FR-033**: Project statistics, Pflichtenheft completion/next-intake markers,
  five maintained agent contexts, and affected guides/indexes MUST update together.
- **FR-034**: The completed Lastenheft MUST be archived with the exact
  `022-wave4-visual-component-porting` suffix through the repository workflow.
- **FR-035**: The feature MUST NOT add full terminal emulation, process/shell/
  PTY integration, host mutation, Wave-5/TP7 examples, broad framework redesign,
  arbitrary user data, new runtime dependencies, or edits under `tv203s/`.
- **FR-036**: Generated DocFX output, API YAML, caches, logs, credentials, test
  output, and validation output MUST remain untracked.

### Constitution Requirements *(mandatory)*

- **CR-001**: The TuiVision Level-2 registry entry, current Constitution, C# as
  approved memory-safe language, and .NET 10/MSTest environment are binding.
- **CR-002**: NIST SSDF, CWE Top 25, secure input/resource validation, size
  bounds, fail-safe fallback, and least host privilege are applicable.
- **CR-003**: STRIDE/CIA/CAPEC review is applicable to terminal input, resource
  identity, state integrity, resource exhaustion, and false capability claims.
- **CR-004**: OWASP ASVS is `N/A` unless web/API/HTTP/auth scope enters.
- **CR-005**: Repository SBOM, VEX, SLSA, OpenSSF, and supply-chain controls
  remain applicable at repository level; new feature evidence is `N/A` unless
  dependency, packaging, provenance, or distributable scope changes.
- **CR-006**: AI-SBOM, NIS2, CRA, EU AI Act, and DORA are `N/A` while AI is
  development tooling and no regulated, operated, or released AI component enters.
- **CR-007**: S-ADR, arc42 security-concept changes, Zero Trust, SAMM, BSI C3A,
  and BSI C5 are `N/A` unless architecture, cloud, provider, deployment, or
  distributed-service boundaries change.
- **CR-008**: iSAQB/arc42 goals, runtime/component views, quality scenarios,
  risks, and debt MUST record reuse of the 021 Driver/Core/Controls ownership.
- **CR-009**: WCAG 2.2 AA applicability, keyboard completeness, text status,
  visible non-color-only state, bilingual CEFR-B2 docs, and didactic comments apply.
- **CR-010**: Cross-platform governance applies to host evidence and asset/path
  behavior; Bash/PowerShell script parity is `N/A` unless a script changes.
- **CR-011**: Agent parity applies when active context or shared guidance changes;
  all five maintained surfaces MUST be reviewed together.
- **CR-012**: `.specify/templates/` are `N/A` unless a generic autonomous-run
  correction is accepted through the separate retrospective workflow.
- **CR-013**: All six installed preset rows MUST record `Applicable`, `N/A`, or
  `Open` with rationale, evidence, owner, reviewer, date/result, residual risk,
  follow-up, and re-evaluation trigger.
- **CR-014**: Historical source review MUST include relevant implementations,
  headers, configs, resources, scripts, and binary fixture metadata read-only.
- **CR-015**: Remote gates MUST be verified on an unchanged reviewed head;
  self-invalidating and post-merge facts MUST use one pre-named closeout path.
- **CR-016**: Every remote/delivery task MUST name the exact repository evidence
  path that records its acceptance result.

### Key Entities

- **Wave4Example**: Project identity, historical sources, main component,
  status, description, operation, framework decision, and primary proof.
- **VisibleTerminalState**: Session/profile/capability, cursor, attributes,
  visible cells, last outcome, and fallback.
- **CharacterGridState**: Source charset/value, mapped glyph, outcome, grid
  position, replacement, and reason.
- **FontGridState**: Fixture identity, exact metadata, selected glyph, 16 row
  bytes, visible pixels, and validation state.
- **ResourceManifestState**: Source identity, representative immutable entries,
  accepted subset, selected entry, and intentional-deviation boundary.
- **HostEvidenceRecord**: Host family, condition, evidence class, capability,
  result, residual risk, and re-evaluation trigger.
- **FrameworkUsageDecision**: One allowed decision plus rationale, evidence,
  residual risk, owner, and follow-up boundary.
- **ProofRecord**: App-loop route, concrete state, view identity, cell region,
  operation, validation command, and result.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All five documented CLI starts launch the intended project and
  show a recognizable main composition, dynamic status, and description route.
- **SC-002**: Five of five examples have exactly one framework decision and
  complete historical-source, operation, status, description, host, and proof evidence.
- **SC-003**: Primary smokes cover all five examples and each uses the app loop
  plus concrete state, exact view identity, and rendered cell-region proof.
- **SC-004**: `Terminal` proves accepted input, one supported cursor/attribute
  action, one rejected/unsupported action, recovery, fallback, and quit without host process.
- **SC-005**: `Cyrillic` proves direct, replacement, invalid, and unsupported
  mapping states with host-independent visible glyph/status agreement.
- **SC-006**: `Fonts` proves one exact 4,096-byte fixture, a recognizable 8x16
  glyph, and at least four distinct invalid-fixture/fallback classes.
- **SC-007**: `ETerm` and `XTerm` each prove at least three representative
  immutable manifest entries plus one out-of-subset fallback without legacy parser claims.
- **SC-008**: macOS, Linux, Windows/WSL, and redirected/headless rows each have
  explicit deterministic, remote, physical, unsupported, or `NotRun` evidence.
- **SC-009**: Five guides plus `examples/README.md` pass DE-first/EN-second
  CEFR-B2, keyboard/text-first, semantic, historical-deviation, and fallback review.
- **SC-010**: Targeted Wave-4 smokes, full Release tests, canonical coverage,
  format, DocFX, web-A11Y, text-browser, secret, generated-output, and remote
  gates pass with exact results recorded.
- **SC-011**: The final diff contains no full emulator, process/shell/PTY,
  host mutation, new dependency, Wave-5/TP7, arbitrary user-data, generated,
  or `tv203s/` change.
- **SC-012**: All six governance presets and all remote tasks have complete
  evidence fields/paths; Analyze converges with no Critical, High, or unresolved Medium.
- **SC-013**: Authorized delivery ends with green required checks, zero
  actionable threads, merge/branch cleanup evidence, and clean local `main`
  equal to `origin/main`.

## Assumptions

- Features 019, 020, and 021 are accepted prerequisites and are not reopened.
- Existing TuiVision controls and 021 contracts can compose the five demos;
  only narrow, test-backed framework fixes may address a proven reusable gap.
- Historical ETerm/XTerm config/resource sources provide intent and selected
  values, not a requirement for general native parser compatibility.
- A source-controlled exact raw font fixture may be copied into the modern
  example as a read-only asset with origin recorded.
- Keyboard operation is mandatory; mouse remains supplemental.
- Runtime/product AI, cloud services, databases, network services, and user
  persistence are absent.

## Scope Boundaries

### In Scope

- Five visible Wave-4 example projects, app-loop/view/cell smokes, controlled
  assets/manifests, guides, evidence, governance, routing, statistics, archive,
  and only proven narrow reusable fixes.

### Out of Scope

- Full ANSI/VT/XTerm/Eterm emulation or native config/resource compatibility
- process, shell, PTY, external command, audio, terminal/font/codepage/keyboard mutation
- Wave-5, TP7, editor/help/resource Wave-3 work, or A11Y-framework Feature 023
- arbitrary user file discovery, writes, persistence, database, network, or cloud
- new runtime dependencies, generated documentation output, and `tv203s/` edits

### Decision and Follow-up Model

- Each example uses exactly one of `UseExistingFramework`,
  `SmallFrameworkFix`, `IntentionalDeviation`, or `FollowUpHardening`.
- `FollowUpHardening` records issue, scope reason, owner/boundary, residual risk,
  and re-evaluation trigger; it does not silently expand Feature 022.
- Host evidence uses `DeterministicInProcess`, `RemoteCI`, or
  `PhysicalObservation`; unavailable physical proof is `NotRun`.
