# Research: TV203 and magiblot/tvision Evolution Audit

## Decision 1: Keep accepted TuiVision contracts canonical

Feature 024 remains the contract catalog, Features 025 and 026 remain the
hardening history, Feature 028 remains the prior closure, and Feature 029
remains the Terminal.GUI handoff. Feature 030 extends evidence and does not
rewrite those decisions.

## Decision 2: Use exact Git objects, not moving master

The audit uses commit `57b6f56b38e0ee75240a80a10ee0e11470c24693`,
tree `96dd03873955689ff0a79f6c8107a8148fe1ebd6`, and the exact COPYRIGHT
hash. A moving branch cannot be acceptance evidence.

## Decision 3: Treat magiblot as a modernization witness with shared bias

The direct C++ lineage is useful for evolution choices but is not an
independent confirmation. Every relation records shared-bias risk and remains
subordinate to historical intent, accepted TuiVision behavior, C# safety,
A11Y, platform boundaries, and real consumers.

## Decision 4: Store selected source records, not upstream content

The repository stores only identity, paths, SHA-256 values, short original
summaries, and pinned permalinks. It does not store source excerpts, fixtures,
binaries, build output, or the external checkout.

## Decision 5: Add one test-only Feature-030 validator

The validator belongs beside the existing audit validators in
`TuiVision.Drivers.Tests`. It reads closed JSON through `System.Text.Json`,
checks exact identities and cardinalities, validates reciprocal relations and
DAGs, and rejects malformed values. No product assembly is changed.

## Decision 6: Use D02 as the vertical slice

`C004`-`C006` combine event representation, command semantics, dispatch,
application-loop evidence, and real consumer relevance. They exercise the
complete source-to-contract-to-observation-to-disposition chain before the
remaining repeated rows are generated.

## Decision 7: Create one MB observation per accepted contract

One observation per contract keeps cardinality and deduplication explicit.
Where no defect exists, the observation still records why the comparison is
an intentional deviation, already satisfied with new evidence, or rejected.

## Decision 8: Deduplicate TG and MB only through TuiVision gaps

Language, architecture, and lineage similarity do not define identity. Two
observations deduplicate only when they describe the same reproducible
TuiVision contract, consumer, safety, A11Y, platform, or real-path proof gap.

## Decision 9: Compute follow-up numbering from the final owner DAG

Empty owner groups create nothing. Non-empty groups receive Feature numbers
from 031 in topological order; exactly one independent closure follows. With
zero findings, 031 is the closure.

## Decision 10: Keep delivery truth separate from audit truth

The committed audit can state local and computed results. Exact current-head
checks, review threads, and merge facts remain provider evidence. A closeout
PR is used only when causal post-merge truth cannot be stated beforehand.

## Decision 11: Require full tests and coverage

The new validator extends shared audit evidence infrastructure and touches a
test project used by repository gates. Full Release and canonical coverage are
therefore required even though product runtime is unchanged.

## Decision 12: Keep script parity conditional

No new script is planned. Existing Bash and PowerShell validation helpers are
used as-is. Any discovered need for a new portable script requires paired
implementation and a plan/task amendment before use.

## Decision 13: Model hard abort as stale-state recovery

The UI abort may leave `Active`, outdated task counts, or an uncertain provider
operation. Status remains read-only, the general command refuses implicit
resume, and explicit resume reconciles authoritative artifacts and provider
state under renewed authority.

## Resolved External Evidence

| Item | Binding value |
|---|---|
| Repository | `https://github.com/magiblot/tvision.git` |
| Commit | `57b6f56b38e0ee75240a80a10ee0e11470c24693` |
| Tree | `96dd03873955689ff0a79f6c8107a8148fe1ebd6` |
| Timestamp | `2026-05-12T18:22:58+02:00` |
| Subject | `Also restore terminal state on SIGBUS and SIGPIPE` |
| COPYRIGHT SHA-256 | `66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548` |
| License summary | Borland disclaimer plus MIT-covered modifications and third-party notices |
