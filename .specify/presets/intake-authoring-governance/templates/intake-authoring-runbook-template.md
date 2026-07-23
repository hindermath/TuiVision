# Intake Authoring Runbook

## Purpose

Use Intake Authoring to turn raw planning material into one traceable Spec Kit
intake. Use Intake Review afterward for independent semantic acceptance.

## Workflow

1. Name inline, pasted, repository-local, and explicit external sources in
   their intended order.
2. Select a target and profile or accept the policy fallback.
3. Run `$speckit-intake-create`.
4. Answer only material questions, one at a time and at most five per pass.
5. Validate the saved intake and receipt.
6. Run `$speckit-intake-create-status` when source or target freshness matters.
7. For `ReadyForReview`, manually start `$speckit-intake-review <target>`.

## Status Boundary

- `ReadyForReview`: authoring is internally consistent and prompts are enabled.
- `NeedsClarification`: a draft is saved, prompts are blocked, and open decision
  IDs require a human answer.

Only Intake Review can produce `Ready`, `ReadyWithAcceptedRisks`,
`NeedsRemediation`, or `Rejected`.

## Source And Hash Contract

Strict UTF-8 is required. Remove one UTF-8 BOM, normalize CRLF and lone CR to
LF, and change nothing else before SHA-256. Repository-local sources are
freshness-checkable. Inline and safely labelled external sources are snapshots;
their later freshness cannot be inferred.

## Authority Contract

Creation and status never imply implementation. The generated Autonomous
prompt defaults to `LocalImplementation`. A broader mode needs explicit,
current, bounded authority. Bypass, secrets, provider administration, and
follow-on feature execution are never inferred.

## Update Contract

An existing target is immutable by default. An authorized update records the
old target hash, superseded receipt, current authority, and new source set. Do
not reuse an old receipt after target or source drift.

For a pre-preset target without a receipt, use `LegacyAdoption` instead of
inventing a superseded receipt. Record the prior normalized target hash,
explicit current update authority, and either the exact Git blob or an honest
snapshot-only proof boundary. The prior target hash must also occur in the
ordered source inventory. `LegacyAdoption` preserves the target path and does
not grant implementation or remote-delivery authority.
