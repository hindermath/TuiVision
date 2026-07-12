# Research: Terminal and Charset Hardening

## Decision 1: Driver-owned in-process terminal model

**Decision**: `TuiVision.Drivers.Console` owns the terminal session, bounded
emulation, charset mapping, font fixture, and profile contracts.

**Rationale**: These contracts transform terminal observations into canonical
buffer state and are independent of a concrete `TView`. The Driver already owns
`TConsoleBuffer` presentation and host capability classification.

**Alternatives considered**:
- Core ownership was rejected because legacy charsets and terminal protocols are
  not universal geometry/event primitives.
- Controls-only ownership was rejected because later non-visual proofs and
  drivers must use the same state without a view dependency.
- A new project was rejected as unnecessary architecture expansion.

## Decision 2: Controls integration through one terminal view

**Decision**: `TuiVision.Controls` receives one bounded `TTerminalView` that
projects a session buffer, cursor, status, and controlled key input.

**Rationale**: This prepares a real app-loop/view/cell proof for Feature 022
without porting any Wave-4 example in Feature 021.

**Alternatives considered**:
- A headless helper only would not prove view composition or event-loop
  readiness.
- A new example would violate the accepted hardening-before-porting order.

## Decision 3: Explicit small terminal grammar

**Decision**: Support text; BEL, BS, TAB, CR, LF; CSI `A/B/C/D`, `H/f`, `J`,
`K`, and `m`; plus full session reset. Limit one sequence to 64 characters,
four numeric parameters, and values 0..9,999.

**Rationale**: This is enough for Wave-4 foundations and 16-color/cursor proof,
while keeping untrusted-input validation finite and reviewable.

**Alternatives considered**:
- Full ANSI/VT100/XTerm emulation is too broad.
- Plain text only would not prepare `terminal`, `eterm`, or `xterm`.
- Passing unknown sequences through would leak host behavior into proof.

## Decision 4: Atomic parser observation boundary

**Decision**: The parser buffers one bounded sequence and mutates the session
only after syntax, command, parameter count, value ranges, and state are valid.
Rejected/unsupported sequences leave no partial cursor/cell/attribute change and
the next independent observation remains usable.

**Rationale**: Terminal input is untrusted. Complete-before-publish mirrors the
successful Feature-020 ingress boundary and supports grouped negative proof.

**Alternatives considered**:
- Streaming partial mutation makes recovery ambiguous.
- Exceptions for expected invalid input would turn host variance into control
  flow and obscure rejection evidence.

## Decision 5: Fixed buffer, history, resize, reset, and BEL semantics

**Decision**: Scrolling moves visible lines upward and appends evicted cells to
a 4,096-cell FIFO. Resize preserves the top-left intersection, initializes new
cells empty, and clamps the cursor. Full reset clears visible/history/parser/
fallback state and restores cursor 0/0 plus default attributes. BEL updates an
in-process notice counter and text status only.

**Rationale**: These deterministic rules match the current driver resize model,
avoid host sound/flash, and make boundary tests stable.

**Alternatives considered**:
- Bottom-anchored reflow was rejected as an unnecessary terminal-layout engine.
- Host BEL and host resize effects violate the no-persistent-host-proof rule.

## Decision 6: Unicode plus KOI8-R, with U+FFFD replacement

**Decision**: Unicode is the canonical visible representation. KOI8-R is the
only historical byte mapping in 021. Invalid Unicode input or an unmappable unit
uses `U+FFFD` and records `Replaced`; other codepages are unsupported.

**Rationale**: Both historical Cyrillic examples explicitly use KOI8-R. One
replacement value prevents host locale/codepage drift.

**Alternatives considered**:
- Host codepages are non-deterministic and platform-specific.
- Supporting all historical codepages would expand the feature beyond Wave 4.

## Decision 7: One raw 8x16 font fixture contract

**Decision**: Accept exactly 256 glyphs, width 8, height 16, 16 bytes per glyph,
and 4,096 raw bytes. Validate metadata and bytes before publication. Historic
compressed/font-generator assets remain read-only references.

**Rationale**: The historical `font.016` boundary and 8x16 cell geometry give a
small reproducible contract without installing or generating host fonts.

**Alternatives considered**:
- Parsing `.sft`, PSF, gzip, or arbitrary raster formats is out of scope.
- Executing `genraw.cc` would add a generator/toolchain proof path.

## Decision 8: Closed JSON profile schema

**Decision**: Profiles use project-owned JSON parsing with required
`ProfileId` and `Charset`; optional `FontId`, `Foreground`, and `Background`.
Unknown/duplicate keys or missing required values reject the complete profile.
Missing optional fields default to built-in 8x16, gray, and black. Unavailable
font/host capability reports unsupported and uses the safe default.

**Rationale**: `System.Text.Json` is the repository standard, supports strict
token/duplicate-key inspection without a package, and avoids ad-hoc string
parsing. A closed schema keeps Eterm/XTerm profile evidence bounded.

**Alternatives considered**:
- Reusing arbitrary historical Eterm/XTerm resource syntax would import a much
  larger and unsafe configuration surface.
- A dictionary-only API would not prove controlled config loading.

## Decision 9: Existing xterm key compatibility remains a boundary

**Decision**: Existing `TConsoleInputAdapter` xterm-compatible key translation
is reviewed and reused by Controls proof where relevant; no new
Drivers-to-Compatibility project reference is introduced.

**Rationale**: Input-key compatibility and output/session emulation have
separate ownership. Coupling Drivers to Compatibility would expand the project
graph for no accepted behavior gain.

**Alternatives considered**:
- Moving existing compatibility APIs would be a broad revision.
- Duplicating key translation in Controls or Drivers is prohibited.

## Decision 10: Historical sources are intent-only

**Decision**: Review at minimum `examples/terminal/terminal.cc`,
`include/tv/terminal.h`, Cyrillic `test.cc` and setup scripts, fonts `test.cc`,
`genraw.cc`, `fontcoll.cc/.h`, Eterm configs/docs, XTerm resources/docs, and
relevant Unix xterm display/key/screen files. Do not edit or execute host setup.

**Rationale**: The historical examples mix reusable intent with host-specific
font, keyboard map, shell, and terminal setup that is unsafe and non-portable as
modern proof.

**Alternatives considered**:
- Mechanical source porting would retain obsolete host assumptions.
- Ignoring history would lose the original terminal/font/charset purpose.

## Decision 11: Host evidence classes stay separate

**Decision**: Record `DeterministicInProcess`, `RemoteCI`, and
`PhysicalObservation` independently for macOS, Linux, and Windows/WSL. A
headless host label does not satisfy physical evidence; it remains `NotRun`.

**Rationale**: Feature 020 proved that OS identity and physical terminal
availability are different facts.

**Alternatives considered**:
- Treating CI or injected tests as physical terminal proof would overclaim.
- Blocking all delivery on unavailable physical hardware would discard valid
  deterministic contracts; the residual boundary is more accurate.

## Decision 12: No new deterministic workflow script yet

**Decision**: No script is added for terminal/charset proof or autonomous
closeout. Existing test runners and GitHub/Spec-Kit commands are sufficient.

**Rationale**: No repeated stack-neutral script requirement has been proven.
Any future script must have Bash/PowerShell parity and a deterministic contract.

**Alternatives considered**:
- A host setup script conflicts with the no-host-manipulation rule.
- A workflow detector remains a Home-Baseline PresetFollowUp candidate, not
  Feature-021 runtime scope.
