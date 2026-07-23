## Intake Authoring Governance

When `intake-authoring-governance` is installed, use
`speckit.intake-create` only when a user explicitly asks to turn raw planning
text or named UTF-8 sources into one saved intake. Preserve source order, ask
at most five material questions per pass, and never guess scope, security, or
delivery authority. Existing targets require explicit update authority. A
`ReadyForReview` receipt is authoring evidence, not review acceptance. Report
`speckit.intake-review` as the next action without starting it. A
`NeedsClarification` draft keeps non-runnable blocked prompt sections. Intake
creation alone grants no implementation or remote authority.

For a pre-preset target without a receipt, require `LegacyAdoption` with the
prior normalized target hash and a Git-blob or snapshot proof boundary. Never
invent a superseded receipt or treat general write permission as current
update authority.
