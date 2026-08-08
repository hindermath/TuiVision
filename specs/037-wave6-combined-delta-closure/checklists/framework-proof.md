# Prüfliste: Framework und Proof

**Purpose**: Verify that the two Wave-6 stages form one reviewable contract.

- [x] Each functional area is paired with exactly one showcase area.
- [x] `Tp7FileManager` is the only accepted entry point.
- [x] State, app-loop, view, focus, dialog and cell proofs are required.
- [x] StatusLine, F1 Description, keyboard and quit paths are required.
- [x] Controlled filesystem boundaries and no-mutation failures are required.
- [x] Normal and constrained layouts remain represented in predecessor proof.
- [x] Controlled `--smoke` and bounded normal PTY starts are required.
- [x] Example-local composition is reviewed for framework duplication.
- [x] Direct helpers cannot replace primary app-loop evidence.
- [x] No runtime or example remediation is permitted in Feature 037.

## Result

`PASS` - 10/10 items complete. Framework ownership and proof limits are clear.
