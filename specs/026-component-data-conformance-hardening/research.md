# Research: Component and Data Conformance Hardening

## R1 - Shared validation boundary

**Decision**: Add a phase/result contract to `TView`, with ordered recursive
validation in `TGroup` and first-rejection evidence.

**Rationale**: Dialogs need to validate arbitrary relevant children, not only
`TInputLine`. A small default-accepting view contract matches the historical
group-validation intent while remaining type-safe and additive in C#.

**Alternatives considered**: Type-checking only `TInputLine` in `TDialog` would
make validation non-extensible. Reintroducing historical numeric `valid`
commands or pointer transfer would mix lifecycle and data protocols.

## R2 - Completion command classification

**Decision**: Default to `cmOK`, `cmCancel`, `cmYes`, and `cmNo`, with a
protected explicit classifier hook for derived dialogs.

**Rationale**: Turbo Vision 2.0.3 `tdialog.cc` and Free Vision `FV006` both
restrict modal completion to these four commands. The hook supports a bounded
modern extension without treating every command as completion.

**Alternatives considered**: A global mutable command set creates hidden shared
state. Hard-coding only OK/Cancel would regress existing Yes/No dialogs.

## R3 - Validator lifecycle

**Decision**: Preserve `TValidator.IsValid` and add phase-aware validation for
`Edit`, `FocusLoss`, and `Acceptance`. Default edit validation is permissive;
final phases delegate to `IsValid`.

**Rationale**: A range such as 10–20 must allow the temporary edit value `1`
while the user types `10`. Specialized validators may still reject impossible
syntax during edit. Feature-025 `CanReleaseFocus` provides the atomic focus
veto and dialog acceptance supplies the final commit boundary.

**Alternatives considered**: Calling final `IsValid` after every key prevents
valid multi-character input. Validator exceptions or destructive correction
would obscure rejection state and accessibility evidence.

## R4 - Rejection state

**Decision**: Validation returns an immutable result containing validity,
text-first message, phase, and rejection target. `TInputLine` gains a bounded
start/end selection range and explicit range setter. Candidate edits, including
replacement of a non-empty selection, are applied only after acceptance; focus
changes are evaluated before mutation.

**Rationale**: This preserves text, cursor, viewport, insert mode, and the exact
non-empty or collapsed selection state. It also allows a dialog to focus the first
rejecting descendant without a hidden test-only path.

**Alternatives considered**: Boolean-only validation cannot expose accessible
error evidence. Exception-driven normal rejection is not an exceptional flow.

## R5 - File-dialog result

**Decision**: Add `TFileDialogOutcome` as the closed modern contract while
retaining `TFileDecisionResult` as a compatibility projection.

**Rationale**: The existing decision type models accepted Open/Select/Save and
Cancel but cannot distinguish navigation, filter, rejection, or caller
overwrite decision without changing its positional constructor. A new additive
type avoids a breaking change and makes every requested mode explicit.

**Alternatives considered**: Extending the existing positional record would
break callers. Returning strings or exceptions would lose operation semantics.

## R6 - File metadata and TOCTOU boundary

**Decision**: Classify using normalized paths and current metadata only. Do not
open content or promise that a later caller operation sees the same filesystem
state.

**Rationale**: This is sufficient for dialog acceptance and explicit overwrite
choice while avoiding arbitrary user reads, destructive behavior, or false
atomicity claims.

**Alternatives considered**: Opening handles in the dialog would couple UI and
I/O ownership. Treating every non-existing path as Save would accept invalid
parents and Open mistakes.

## R7 - UI resource model

**Decision**: Reuse `TResourceFile` and existing dialog records; add closed
menu and status-line description records with Controls adapters and factories.

**Rationale**: The current registry is already case-sensitive and allowlisted.
Dependency-free record loaders validate persisted structure before catalog
publication. Controls applies the same semantic rules to in-memory descriptions
before runtime creation, preserving project direction and both trust boundaries.

**Alternatives considered**: Serializing runtime object graphs or CLR type
names introduces unsafe activation and ownership ambiguity. One untyped generic
node model weakens compile-time meaning for three distinct structures.

## R8 - Graph and payload limits

**Decision**: Bound files to 4,096 entries, payloads to 4 MiB, description
collections to 4,096 items, and menu depth to 16. Parse and validate a complete
candidate before publication.

**Rationale**: Existing dialog records already use a 4,096-item bound. The new
limits prevent allocation and recursion abuse while remaining far above the
expected terminal UI scale.

**Alternatives considered**: Unbounded lengths trust hostile metadata. Streaming
objects directly into the visible catalog creates partial state on late errors.

## R9 - Historical and Free Vision relation

**Decision**: Treat Turbo Vision 2.0.3 as primary intent and pinned Free Vision
commit `ffc03b34d8cafb85ddcf0686de1c5551601dacb2` as secondary corroboration.

**Rationale**: `tdialog.cc`, `tinputli.cc`, `tfiledia.cc` and relevant headers
show explicit completion, hierarchical validity, validator ownership, and
operation-aware file checks. `FV006`, `FV007`, `FV010`, and `FV012` preserve
those responsibilities while evolving Unicode and resource usage. Retrieved
hashes match the Feature-024 audit ledger.

**Alternatives considered**: Mechanical translation would preserve pointer,
codepage, global-service, and binary-layout assumptions that do not belong in
modern managed C#.

## R10 - Documentation and delivery proof

**Decision**: Public APIs receive DE-first/EN-second XML; therefore DocFX,
Playwright/Axe, and text-first checks are mandatory. Local candidate validation
must use the exact staged set; remote gate claims map to actual workflow/job/
runner/platform semantics before any authorized bypass.

**Rationale**: The feature is API- and learner-facing, and Autonomous Run
Governance v0.1.2 explicitly closes stale-diff and gate-name assumptions.

**Alternatives considered**: Skipping docs for additive APIs violates the
repository contract. Treating branch-wide unstaged state or check labels as the
candidate/gate proof can validate the wrong content or platform.
