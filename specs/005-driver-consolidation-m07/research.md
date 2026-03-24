# Research: Driver Consolidation and M-07 Porting Proof

## Decision 1: Consolidate historical drivers by capability, not by operating-system lineage

- **Decision**: Treat the historical driver directories as sources for capability buckets such as screen presentation, keyboard handling, mouse handling, display adaptation, and terminal capability handling rather than as one-to-one runtime targets.
- **Rationale**: The project explicitly wants one managed `TuiVision.Drivers.Console` baseline without native bindings. Capability-oriented consolidation keeps the design aligned with that target and avoids reproducing the historical platform split inside the C# runtime surface.
- **Alternatives considered**:
  - Preserve one managed implementation branch per historical operating system: rejected because it would recreate the complexity that Phase 7 is supposed to remove.
  - Ignore the historical driver split and treat the current driver surface as already complete: rejected because `M-07` and the Pflichtenheft explicitly say unresolved driver scope still exists.

## Decision 2: Use `.cc` files as the formal M-07 proof inventory while reviewing associated `.h`/`.c` context

- **Decision**: `docs/porting-status.md` will use the historical `.cc` implementation files under `tv203s/contrib/tvision/classes` as the formal row set for `M-07`, but associated `.h` and `.c` files must still be inspected and referenced whenever they explain a row's mapping or replacement rationale.
- **Rationale**: The Pflichtenheft names the `.cc` implementation inventory as the required proof scope for `M-07` and for the Phase-8 entrance gate. This keeps the acceptance target unambiguous while still acknowledging that some historical driver implementations only make sense when their included headers or helper C files are reviewed alongside them.
- **Alternatives considered**:
  - Add every `.c` and `.h` file as a mandatory ledger row: rejected because it would broaden the formal proof scope beyond the explicit Pflichtenheft wording.
  - Exclude all mention of ancillary native-support files: rejected because some conscious replacement decisions become harder to explain without them.

## Decision 3: Make `docs/porting-status.md` the canonical human-readable proof ledger

- **Decision**: The feature will introduce `docs/porting-status.md` as a repository-local Markdown ledger with one row per historical `.cc` file and with support-file references or rationale notes whenever associated `.h`/`.c` files materially influence the mapping.
- **Rationale**: The Pflichtenheft explicitly requires that document, and a repository-local Markdown file is durable, diffable, reviewable, and easy for trainees to follow.
- **Alternatives considered**:
  - Use a spreadsheet or external tracking board: rejected because it would be harder to review in Git and would violate the repository-local proof expectation.
  - Rely only on code search and tests as proof: rejected because conscious replacements and non-one-to-one mappings would remain too implicit.

## Decision 4: Require one primary target and allow optional secondary targets

- **Decision**: Every ledger row will contain one mandatory primary target plus optional secondary targets where a historical source file now influences more than one maintained area.
- **Rationale**: This follows the clarification decision and keeps the ledger reviewable without pretending that every historical source maps perfectly to a single modern file.
- **Alternatives considered**:
  - Allow only one target per row: rejected because it would force misleading oversimplification for split responsibilities.
  - Allow multiple unordered targets with no primary target: rejected because it weakens traceability and review focus.

## Decision 5: Keep driver validation centered in `tests/TuiVision.Drivers.Tests`

- **Decision**: The existing `tests/TuiVision.Drivers.Tests` project remains the primary MSTest home for Phase-7 behavior, with cross-module regressions only where driver changes visibly affect other layers.
- **Rationale**: The repository already has a dedicated driver test project, even though it is currently narrow. Expanding it is lower risk and keeps driver failures isolated.
- **Alternatives considered**:
  - Move driver tests into Controls tests: rejected because it would blur module responsibility and failure ownership.
  - Create another new test project for proof-only validation: rejected because the existing driver test project is sufficient.

## Decision 6: Accept manual or semi-automated Linux and Windows/WSL evidence in this increment

- **Decision**: Linux and Windows/WSL compatibility checks must be reviewable in this phase, but they do not yet have to be mandatory CI gates.
- **Rationale**: This matches the clarification outcome and the current repository state, where Ubuntu and macOS CI already exist but Windows or WSL validation is not yet a hard gate.
- **Alternatives considered**:
  - Require a full CI matrix including Windows before Phase 7 can proceed: rejected as a higher-cost infrastructure step than the current specification requires.
  - Treat non-macOS compatibility checks as optional: rejected because the project governance now explicitly names Linux and Windows/WSL as important validation environments.

## Decision 7: Keep Phase 7 separate from final Phase-8 entrance-gate closure

- **Decision**: The plan will explicitly separate driver consolidation plus proof-ledger creation from the remaining build/test/coverage/API-documentation work that still belongs to the full Phase-8 gate.
- **Rationale**: The Pflichtenheft distinguishes Phase 7 from the later entrance-gate closure, and the project wants trainees to see that sequence clearly.
- **Alternatives considered**:
  - Merge all gate work into this feature: rejected because it would over-broaden the phase and obscure responsibility.
  - Ignore the later gate dependencies during planning: rejected because it would make the phase outcome hard to interpret.
