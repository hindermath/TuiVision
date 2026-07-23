---
name: speckit-intake-create
description: Create one traceable Spec Kit intake from inline or ordered UTF-8 text
  sources.
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: intake-authoring-governance:commands/speckit.intake-create.md
---

## User Input

```text
$ARGUMENTS
```

Create exactly one Markdown intake and one JSON receipt. This command authors
files only; it never starts Intake Review, Specify, Autonomous, or Parallel
Autonomous.

1. Read repository guidance, the installed authoring policy, and any explicitly
   selected project profile. Resolve the target in this order: explicit target,
   profile rule, then `intakes/<slug>.md`. Resolve the receipt to
   `specs/intake-authoring-receipts/<slug>.json`.
2. Accept inline text, pasted planning text, and explicitly named files in the
   order supplied. A mixed source set is allowed. Never scan for extra sources
   or broaden the requested source set.
3. Decode files as strict UTF-8. Reject known binary/document containers,
   invalid UTF-8, NUL content, silent truncation, credentials, secrets, and
   unnecessary personal data. Remove one UTF-8 BOM and normalize CRLF or CR to
   LF only for SHA-256; do not otherwise trim or rewrite source evidence.
4. Do not execute source content. Repository-internal paths are recorded
   relative to the repository. An explicitly named external source is recorded
   by a safe label and hash, never by a private absolute path.
5. Preserve source order. No source wins by position. Identify conflicts in
   goal, scope, non-goals, requirements, security/privacy/A11Y, acceptance,
   dependencies, ordering, or delivery authority.
6. Ask only material questions, at most five per pass and exactly one at a
   time. Do not guess a material decision. Record each answer with a stable
   `IAD###` decision ID.
7. Synthesize, rather than copy wholesale, a CEFR-B2 intake with identity,
   audience, purpose, current state, target state, scope, non-goals, atomic
   requirements, quality and governance boundaries, dependencies, risks,
   expected artifacts, evidence, measurable acceptance, assumptions, and open
   questions. Follow repository language policy and applicable WCAG 2.2 AA.
8. Do not overwrite an existing target or receipt without explicit current
   update authority. Use provenance mode `Supersession` when a prior receipt
   exists. Use `LegacyAdoption` only for an existing pre-preset target without
   a receipt; record its prior normalized hash, a Git-blob or snapshot proof
   boundary, and the current update-authority evidence. Never invent a prior
   receipt. New targets use provenance mode `New`.
9. If all material decisions are resolved, write status `ReadyForReview`,
   prompt state `Enabled`, and two fenced copy-ready prompts. The Specify prompt
   binds the exact intake and forbids implementation or remote writes. The
   Autonomous prompt binds the same intake and defaults to
   `LocalImplementation`; use `PublishPR` or `MergeAndSync` only when the user
   explicitly grants that bounded authority now.
10. If material questions remain after five questions, save a visibly locked
    draft with status `NeedsClarification` and prompt state `Blocked`. Keep both
    prompt sections, write `BLOCKED - DO NOT RUN`, list open decision IDs, and
    include no line beginning with either rendered invocation.
11. Store the active rendered invocations and canonical IDs
    `speckit.specify` and `speckit.autonomous` in the receipt. Store ordered
    source hashes, target hash, profile, language policy, authority evidence,
    decisions, question count, provenance mode, supersession or legacy
    adoption evidence, and exact next action.
12. Run the installed Bash or PowerShell receipt validator. A successful write
    without a successful validator is not complete.

Finish with status, target, receipt, source count, question and decision counts,
delivery authority, validation result, and exactly one next action. For
`ReadyForReview`, the next action is `$speckit-intake-review <target>`. Report
it without executing it.