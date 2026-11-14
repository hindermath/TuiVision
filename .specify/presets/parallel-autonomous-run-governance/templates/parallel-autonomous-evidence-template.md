# Evidenz einer parallelen autonomen Kampagne / Parallel Autonomous Campaign Evidence

## Kampagne / Campaign

| Field | Value |
|---|---|
| Campaign ID | `[UUID]` |
| Manifest SHA-256 | `[sha256]` |
| Execution environment | `[native/container]` |
| Environment identifier | `[host platform or image digest]` |
| Agent family | `[agent]` |
| Policy override | `[authorizer, date, scope, expiry or N/A]` |
| Configured concurrency | `[count]` |
| Maximum observed concurrency | `[count]` |

## Worker / Workers

| Worker | Run ID | Repository | Head | Autonomous state | Gates | Result |
|---|---|---|---|---|---|---|
| `[id]` | `[UUID]` | `[repo]` | `[sha]` | `[path + hash]` | `[evidence]` | `[status]` |

## Uebergaben / Handoffs

| Producer | Consumer | Path | SHA-256 | Validation |
|---|---|---|---|---|
| `[worker]` | `[worker]` | `[path]` | `[sha256]` | `[Pass/Open]` |

## Konsolidierung / Consolidation

Die All-ready-Barriere, gegebenenfalls die menschliche Alternativauswahl, die
deterministische Merge-Reihenfolge, jeden exakt geprueften Head, Provider-
Preflights, Teilmerges, Stop/Resume und alle Post-Merge-Aktionen dokumentieren.
Evidenz beschreibt Berechtigung, erteilt sie aber nicht.

*Record the all-ready barrier, human alternative selection where applicable,
deterministic merge order, every exact reviewed head, provider preflights,
partial merges, stop/resume, and all post-merge actions. Evidence documents
authority but never grants it.*

## Nutzungshilfe / Reader help

DE: Mehrere autorisierte Feature-Läufe mit getrennten Arbeitskopien koordinieren. Diese Vorlage ist ein auszufüllender Starter, kein bestandener Nachweis. Vorhandene passende Evidence weiterverwenden.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/blob/main/docs/user-guide.md).

EN: Coordinate multiple authorized feature runs with isolated working copies. This template is a starter to complete, not passed evidence. Reuse suitable existing evidence.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
