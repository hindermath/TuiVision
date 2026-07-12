# Research: Wave-4 Visual Component Porting

## R1 - Feature 021 Baseline

**Decision**: Reuse the controlled terminal session, bounded emulation subset,
KOI8-R/Unicode mapper, raw font-fixture validator, closed profile/fallback,
host-classification, and `TTerminalView` contracts from Feature 021.

**Rationale**: Feature 022 demonstrates these contracts visibly. Reimplementation
inside examples would create local special logic and competing parsers.

**Rejected**: Reopening 021, local escape parsing, host codec use, or a second terminal view.

## R2 - Vertical Slice

**Decision**: Implement `Terminal` first with one complete project-local red
matrix for first frame, input/output, cursor/attribute action, rejection,
recovery, fallback, description, cell proof, and quit.

**Rationale**: It exercises the entire Driver/Core/Controls/app-loop chain and
detects ownership or harness gaps before four compositions repeat the pattern.

**Rejected**: Starting with a static manifest demo or implementing all five before proof.

## R3 - Shared Presentation Boundary

**Decision**: Link `examples/Shared/Wave4Runtime.cs` into all five projects for
status drawing, description, bounded regions, manifest display, and scripted events.

**Rationale**: These are repeated presentation/proof concerns. Domain contracts
stay in framework types and per-example compositions.

**Rejected**: Five copies or a public framework API without a reusable framework gap.

## R4 - Linked-source Identity

**Decision**: Cross-example matrix tests use public state or delegates and do
not cast linked `Wave4Runtime` types across example assemblies.

**Rationale**: One source file linked into five assemblies creates five CLR type identities.

## R5 - Cyrillic Intent

**Decision**: Show a fixed labeled KOI8-R sample through the 021 mapper, plus
direct/replaced/invalid/unsupported outcomes and visible status.

**Rationale**: The historical Linux/X11 examples teach Cyrillic display and
host setup constraints; deterministic Unicode cells preserve that intent safely.

**Rejected**: `LANG` mutation, host codepages, `/dev/vcsa`, `consolechars`,
`loadkeys`, root setup scripts, or claims of physical console parity.

## R6 - Font Fixture

**Decision**: Copy one exact 4,096-byte raw 8x16 fixture into the modern Fonts
project, validate before publication, and render a known nonblank glyph as a
text-first pixel matrix.

**Rationale**: A project-owned fixture makes normal startup and tests independent
of repository root and host font installation while retaining historical origin.

**Rejected**: Executing `genraw`, parsing SFT/compressed formats, installing a
font, or using a blank-only fixture as visible proof.

## R7 - ETerm Resource Boundary

**Decision**: Represent selected menu/theme/presentation values as immutable
typed entries with exact historical source identity.

**Rationale**: `menus.cfg` and `theme.cfg` are configuration-only teaching
sources. A visible manifest is useful without claiming native parser compatibility.

**Rejected**: General ETerm parser, arbitrary path loading, or terminal theme mutation.

## R8 - XTerm Resource Boundary

**Decision**: Represent selected `Xterm.res` values and the accepted 021
sequence/capability subset as immutable typed entries with unsupported boundaries.

**Rationale**: Resource/protocol intent becomes visible while input translation
and session emulation remain owned by existing framework contracts.

**Rejected**: X resource database parser, terminfo/native XTerm integration, or full emulator claims.

## R9 - Primary Visual Proof

**Decision**: Require real app-loop dispatch plus concrete state, exact view,
dynamic status, description route, and rendered cell proof for every example.

**Rationale**: This is the proven Wave-1 through Wave-3 acceptance pattern and
excludes helper-only, screenshot-only, or text-description-only proof.

## R10 - Host Evidence

**Decision**: Maintain distinct `DeterministicInProcess`, `RemoteCI`, and
`PhysicalObservation` rows. Missing physical conditions are `NotRun` with risk
and re-evaluation trigger.

**Rationale**: A runner OS label does not prove an interactive terminal, font,
locale, or emulator condition. Features 020 and 021 established this evidence rule.

## R11 - Security and Resource Bounds

**Decision**: Keep assets source-controlled/read-only, validate fixture shape,
use fixed typed manifests, enforce the 021 parser bounds, and publish safe fallback.

**Rationale**: The relevant STRIDE/CIA/CAPEC risks are malformed input, state
corruption, resource exhaustion, spoofed capability/source claims, and host mutation.

## R12 - Governance Applicability

**Decision**: Apply NIST SSDF/CWE, proportional STRIDE/CAPEC, iSAQB reuse,
A11Y, host cross-platform proof, and agent parity. Keep ASVS, new supply-chain
evidence, AI/regulatory, S-ADR/Zero Trust/SAMM/C3A/C5, and script parity
trigger-based `N/A` unless actual scope changes.

## R13 - Documentation and Delivery

**Decision**: Add five bilingual guides, update navigation/index, run DocFX,
axe, and lynx, and use `MergeAndSync` with exact evidence paths and one pre-named closeout.

## R14 - Retrospective Boundary

**Decision**: Record 022 field observations in feature evidence; modify generic
autonomous workflow only through a separate non-empty post-feature PR and Home-Baseline handoff.

**Rationale**: Product implementation and preset productization remain separate review units.
