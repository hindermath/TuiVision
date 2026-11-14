---
description: Consolidate campaign results under explicit current authority.
---

For `AlternativeSolutions`, require a named human-selected worker and reject
automatic scoring or merging of several candidates.

For `MergeAndSync`, require every eligible worker to be `ReadyForMerge` with an
exact reviewed head, passing required checks, no actionable review thread, and
current explicit merge authority. Cross the all-ready barrier before the first
merge. Use a provider-bound preflight contract for open/not-draft state, exact
head, mergeability, current change requests and threads, and check policy.
Revalidate all remaining direct or stacked PR bases after every merge.

Checkpoint every verified merge. On resume, skip verified merged workers and
adopt an externally merged exact head. Drift becomes `NeedsRevalidation`.
Honor cooperative stop between operations. Execute only manifest-declared,
idempotent post-merge actions. Set `Completed` only after merge,
synchronization, post-merge work, and final validation all succeed. Never
claim cross-repository atomicity.

*DE: Teilmerges sind fortsetzbar. Worker-Handoffs duerfen keine
Post-Merge-Kommandos definieren.*

## Nutzungshilfe / Reader help

DE: Mehrere autorisierte Feature-Läufe mit getrennten Arbeitskopien koordinieren. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/blob/main/docs/user-guide.md).

EN: Coordinate multiple authorized feature runs with isolated working copies. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
