# Research: Editor, File, Help, and Stream Components

## Decision 1: Keep UI concerns in `TuiVision.Controls` and persistence concerns in `TuiVision.Serialization`

- **Decision**: Implement editor, file-dialog, history, and runtime help UI in `src/TuiVision.Controls`, while placing stream primitives, help-file models, and resource persistence in `src/TuiVision.Serialization`.
- **Rationale**: This split matches the constitution's module boundaries: visual behavior belongs in Controls, persisted formats and stream logic belong in Serialization. It also keeps file-backed editing from leaking binary concerns into UI classes.
- **Alternatives considered**:
  - Put everything into `TuiVision.Controls`: rejected because persistence and stream semantics would blur module boundaries.
  - Introduce a sixth source module for editor/help persistence: rejected because Constitution principle IV forbids new modules without strong justification.

## Decision 2: Build compatibility streams on top of the existing binary archive foundation

- **Decision**: Implement `pstream`, `ipstream`, `opstream`, and `fpstream` as a higher-level compatibility layer that reuses `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TRecordRegistry`, and `TRecordSerializer` where practical.
- **Rationale**: The repository already contains deterministic primitive/archive helpers and registry-based type reconstruction. Reusing that substrate reduces duplication while still allowing reference tracking, seek/tell behavior, and stricter malformed-input rules in the compatibility layer.
- **Alternatives considered**:
  - Replace the current archive layer entirely: rejected because it would discard working code and widen risk.
  - Use the current `TRecordSerializer` directly for all phase-6 needs: rejected because file-backed seek/tell and shared-reference semantics would become awkward and under-specified.

## Decision 3: Preserve shared references but reject cyclic graphs

- **Decision**: The compatibility stream layer tracks repeated object references inside supported object graphs but treats cyclic graphs as an explicit unsupported input.
- **Rationale**: This matches the specification's clarification and keeps the serializer design tractable. Shared references materially affect correctness; cyclic-graph support would add disproportionate complexity and test burden for this increment.
- **Alternatives considered**:
  - Support only tree-shaped graphs with no shared references: rejected because it would lose required identity semantics.
  - Fully support cyclic graphs: rejected because it would expand the phase into a deeper serializer project.

## Decision 4: Use a dedicated help file model separate from generic resource containers

- **Decision**: Runtime help loads from a dedicated help-file abstraction (`THelpFile`, `THelpTopic`, `THelpIndex`) rather than from the generic resource container API.
- **Rationale**: The specification explicitly requires a dedicated help file as the runtime source. Keeping help persistence separate makes context lookup, topic navigation, and fallback handling easier to test and reason about.
- **Alternatives considered**:
  - Load help through generic resource entries only: rejected because it conflicts with the clarified dedicated-help-file requirement.
  - Keep the persistence source abstract during planning: rejected because it would leave contracts and tests ambiguous.

## Decision 5: Scope history recall by history identifier

- **Decision**: Model history as internal buckets keyed by history identifier; linked fields share recall only when they use the same ID.
- **Rationale**: This follows the clarified feature behavior and keeps unrelated path/text histories from mixing. It also provides a narrow internal abstraction for tests without forcing cross-session persistence.
- **Alternatives considered**:
  - One global history list for all fields: rejected because it causes cross-feature interference.
  - Per-control isolated history only: rejected because it prevents legitimate shared recall between related fields.

## Decision 6: Detect external file modification through a stored file snapshot

- **Decision**: `TFileEditor` stores file metadata from load time and compares it again before save to detect external modification.
- **Rationale**: The specification requires an explicit overwrite decision when the file changed during the editing session. A managed metadata snapshot keeps the design cross-platform and avoids file-locking assumptions.
- **Alternatives considered**:
  - Always overwrite on save: rejected by clarification and data-loss risk.
  - Use mandatory file locks for the whole session: rejected because it is brittle across platforms and overshoots the feature scope.

## Decision 7: Preserve line endings for loaded files and default new files to `LF`

- **Decision**: Store the loaded document's newline mode in the document session and reapply it on save; untitled/new documents default to `LF`.
- **Rationale**: This matches the clarified acceptance criteria and minimizes unnecessary diffs in edited files while providing one deterministic default for new documents.
- **Alternatives considered**:
  - Normalize every save to platform default: rejected because behavior would vary by machine.
  - Normalize every save to one fixed format regardless of input: rejected because it would rewrite existing files unexpectedly.

## Decision 8: Support `TEditWindow` and `THelpWindow` through a narrow framed-host abstraction

- **Decision**: Allow a small reusable framed/titled host helper in `TuiVision.Controls` if needed by both `TEditWindow` and `THelpWindow`, but do not widen the increment into a full generic `TWindow` subsystem unless proven necessary during implementation.
- **Rationale**: Both UI surfaces need non-modal framed hosting, while `TDialog` remains modal and unsuitable. A narrow helper preserves scope discipline.
- **Alternatives considered**:
  - Reuse `TDialog` directly: rejected because modal run-loop semantics do not fit editor/help windows.
  - Introduce a broad general-purpose `TWindow` framework now: rejected as premature scope expansion.

## Decision 9: Use exact ordinal key semantics for resources

- **Decision**: Resource identifiers use exact case-sensitive matching, backed by ordinal key comparison.
- **Rationale**: The feature clarification requires case-sensitive lookup/replacement/removal. Exact key semantics avoid platform-dependent ambiguity and keep tests deterministic.
- **Alternatives considered**:
  - Case-insensitive matching: rejected because it collapses distinct keys unexpectedly.
  - Mixed rules by resource type: rejected because it complicates the container contract.

## Decision 10: Add a dedicated Serialization test project

- **Decision**: Introduce `tests/TuiVision.Serialization.Tests` for stream, help-file, and resource persistence behavior.
- **Rationale**: The feature adds substantial new logic to `TuiVision.Serialization`, and the repository currently lacks module-specific serialization tests. Keeping these tests separate from Controls tests improves failure isolation and respects the repository's mirrored test-project pattern.
- **Alternatives considered**:
  - Put serialization tests into `TuiVision.Controls.Tests`: rejected because it would mix unrelated failure domains.
  - Rely on repository-wide `dotnet test` only: rejected because module-specific coverage and TDD discipline would remain too coarse.
