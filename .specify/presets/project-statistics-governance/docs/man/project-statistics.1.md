# project-statistics(1)

## NAME

project-statistics.sh, project-statistics.ps1 - reproduzierbare Projekttransparenz /
reproducible project transparency

## SYNOPSIS

```bash
bash scripts/project-statistics.sh init|status|update --repo PATH --config PATH
bash scripts/project-statistics.sh update --revision COMMIT --as-of YYYY-MM-DD --dry-run
```

```powershell
pwsh -NoProfile -File scripts/project-statistics.ps1 -Action Status -Repo PATH -Config PATH
pwsh -NoProfile -File scripts/project-statistics.ps1 -Action Update -WhatIf -Json
```

## DESCRIPTION

Init schreibt ausschliesslich neue Konfiguration/Vorlage; bestehende Dateien
blockieren. Erst nach Konfigurations-Commit Update verwenden. Status schreibt
nichts; Update braucht sauberen Git-Stand und ausdruecklichen Auftrag.
Default-Kontext: docs/project-statistics/config.json. Ein Pilot kann
docs/project-statistics-pilot/config.json verwenden.
Revision und AsOf steuern die neue Messung; Status prueft den gespeicherten Stand.

Init only creates new configuration/template files; existing files block.
Commit the configuration before Update. Status never writes; Update needs a
clean worktree and explicit authority. The configuration selects a separate
context. Revision and AsOf select a new measurement; Status replays the stored one.

## OPTIONS

- --repo / -Repo: Git-Repository / Git repository.
- --config / -Config: relativer Pfad / relative path.
- --revision / -Revision: Update-Quellrevision / Update source revision, HEAD by default.
- --as-of / -AsOf: optionaler Aktivitaets-Stichtag / optional activity cutoff.
- --dry-run / -WhatIf: Vorschau ohne Schreiben / preview without writes.
- --json / -Json: strukturierter Status / structured status.
- --check-only: Bash-Alias fuer Status / Bash alias for Status.

## EXIT STATUS

0 = Erfolg/aktuell / success/current.
1 = Drift oder fehlender Snapshot / drift or missing snapshot.
2 = ungueltige Eingaben oder fehlende Voraussetzungen / invalid or blocked.

## SAFETY

Git und PowerShell 7 sind erforderlich. Kein Netzwerk, Fetch, Tool-Install,
Stash oder automatische Git-Lieferung. Keine Personenranglisten oder
KI-Zeitersparnisbehauptungen. Siehe docs/methodology.md.

Git and PowerShell 7 are required. No network, fetch, tool installation, stash
or automatic Git delivery. No rankings of people or AI time-saving claims.
