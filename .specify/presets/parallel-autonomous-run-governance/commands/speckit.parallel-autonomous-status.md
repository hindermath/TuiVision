---
description: Inspect a parallel autonomous campaign without changing it.
---

Read the campaign manifest, state, runtime result files, Git worktrees, and
referenced autonomous states. Report queued, running, completed, failed,
blocked, interrupted, and ready workers; observed concurrency; missing or stale
evidence; stop state; selection state; runner profile and explicitly declared
stable routing role, non-secret model metadata, reasoning effort, and preflight
result; attempt counts; consolidation checkpoints;
post-merge actions; and the next exact action.

Status is read-only. A running marker without a trustworthy live process or
result is `Interrupted`, never success.

Support machine-readable JSON and accessible text. Never expose executable
arguments, environment values, credentials, or undeclared provider settings.

*DE: Status ist read-only und bietet JSON sowie barrierearmen Text. Fehlende
Pflichtbindungen werden als `Blocked`, nicht als stiller Standard angezeigt.*

## Nutzungshilfe / Reader help

DE: Mehrere autorisierte Feature-Läufe mit getrennten Arbeitskopien koordinieren. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/blob/main/docs/user-guide.md).

EN: Coordinate multiple authorized feature runs with isolated working copies. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
