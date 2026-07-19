# check-homogeneity(1)

## NAME

`check-homogeneity.sh`, `check-homogeneity.ps1` - Compliance-Scanner des Workspace Homogeneity Guardian. *Compliance scanner of the Workspace Homogeneity Guardian.*

## SYNOPSIS

```bash
bash scripts/check-homogeneity.sh [options] [TARGET_DIR]
```

```powershell
pwsh -NoProfile -File scripts/check-homogeneity.ps1 [-TargetDir <dir>] [options]
```

## DESCRIPTION

Das Skript prueft ein Zielverzeichnis gegen die Homogenitaets-Baseline des Workspace, erzeugt einen Compliance-Score und optional `STATS.md` sowie einen `memory-patch.md`. Die Ausgabe ist wahlweise menschenlesbar oder als JSON verfuegbar; mit `--apply-patch` wird ein Patch angewendet und committet.

*The script checks a target directory against the workspace homogeneity baseline, produces a compliance score, and optionally writes `STATS.md` and a `memory-patch.md`. Output is human-readable or JSON; with `--apply-patch` a patch is applied and committed.*

## OPTIONS

| Bash | PowerShell | Bedeutung / Meaning |
|---|---|---|
| `[TARGET_DIR]` | `-TargetDir <dir>` | Zu pruefendes Verzeichnis |
| `--verbose` | `-Verbose` | Alle geprueften Dateien anzeigen (auch PASS) |
| `--json` | `-Json` | Maschinenlesbare JSON-Ausgabe (hat Vorrang) |
| `--dry-run` | `-DryRun` | Keine Schreibvorgaenge (kein `STATS.md`/`memory-patch.md`) |
| `--apply-patch <path>` | `-ApplyPatch <path>` | `memory-patch.md` anwenden und committen |
| `--no-patch` | `-NoPatch` | Keinen `memory-patch.md` erzeugen |
| `--fail-fast` | `-FailFast` | Beim ersten FAIL abbrechen |
| `--yes` | `-Yes` | Nicht-interaktive Bestaetigung (fuer `--apply-patch`) |

## EXAMPLES

```bash
bash scripts/check-homogeneity.sh --json ~/RiderProjects
```

```powershell
pwsh -NoProfile -File scripts/check-homogeneity.ps1 -TargetDir ~/RiderProjects -DryRun
```

## EXIT STATUS

| Code | Bedeutung / Meaning |
|---:|---|
| 0 | kein Fehler; Warnungen sind zulaessig / no failure; warnings are allowed |
| 1 | mindestens ein Fehler / at least one failure |
| 2 | fataler Fehler / fatal error |

## SEE ALSO

`migrate-workspace.1`, `init-stats.1`
