---
description: Projektstatistik status / Project statistics status
---

# Statistik status / Statistics status

DE: Verwende das ausdruecklich benannte Repository und den Kontext.
Default: `docs/project-statistics/config.json`. Pilotkontexte sind getrennt.
Strikt read-only; keine Reparatur oder automatischen Folgeschritte.
Auf macOS/Linux:
`bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh status --repo . --config <config>`
Auf Windows:
`pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Status -Repo . -Config <config>`

EN: Use the explicitly named repository and configuration. Do not shorten the
installed script paths. Read-only status must not repair or start follow-up work.
Report reproducibility separately from freshness, source revision, cutoff,
coverage and exact exit code. Missing tools or sources block; never install
tools, fetch history, stash, commit, push, merge or grant human approvals.
Reference estimates remain opt-in and are not measured AI productivity.
Follow the resolved `project-statistics-contract`.

## Nutzungshilfe / Reader help

DE: Reproduzierbare Projekttransparenz aus Git-Daten zeigen, nicht KI-Produktivität messen. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/main/docs/user-guide.md).

EN: Show reproducible project transparency from Git data, not measured AI productivity. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
