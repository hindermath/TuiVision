# Intake Review Runbook

## Purpose

Use intake review before feature creation when a project or campaign requires
semantic readiness evidence. The preset accepts generic UTF-8 Markdown or text;
`Lastenheft*.md` is one supported profile, not the universal format.

## Workflow

1. Choose `Single`, `Series`, or `Campaign` and create a review request.
2. Run `$speckit-intake-review` without modifying targets.
3. Resolve material questions with a human owner.
4. When mutation is authorized, run `$speckit-intake-repair`; it always ends
   with a complete re-review.
5. Run `$speckit-intake-review-status` to prove freshness.
6. Start Specify, Preset 7, or Preset 8 only when active policy accepts the
   current result.

## Outcome Contract

- `Ready`: no open material finding or risk.
- `ReadyWithAcceptedRisks`: only human-accepted Medium/Low residual risks.
- `NeedsClarification`: material answers are missing.
- `NeedsRemediation`: the intent is known but target changes are required.
- `Rejected`: unsafe, contradictory, unauthorized, or unusable intake.

Critical and High findings always block. Operator exceptions do not rewrite
intake meaning; campaign consumers validate owner, reason, date, and expiry.

## Hash Contract

Decode strict UTF-8, remove one BOM, normalize CRLF and lone CR to LF, and do
not trim spaces or final lines. Hash the resulting bytes with SHA-256. Binary or
non-UTF-8 input is rejected. This normalization makes equivalent text portable
without hiding meaningful edits.

## Consumer Contract

Preset 7 and Preset 8 remain usable without this optional preset. When the
preset is installed and policy says `required: true`, they require exactly one
current `Ready` or human-approved `ReadyWithAcceptedRisks` result. Result drift,
missing coverage, unanswered questions, or open blocking findings stops work
before feature creation or worker scheduling.
