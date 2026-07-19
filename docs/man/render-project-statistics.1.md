# render-project-statistics(1)

## NAME

`render-project-statistics.sh`, `render-project-statistics.ps1` - rendert das
ASCII-Statistikprofil 2. *Renders ASCII Statistics Profile 2.*

## SYNOPSIS

```bash
bash scripts/render-project-statistics.sh --repo PATH [--check-only|--dry-run] [--json]
```

```powershell
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo PATH `
  [-CheckOnly] [-WhatIf] [-Json]
```

## DESCRIPTION

Der Renderer liest `docs/project-statistics.config.json`, berechnet
reproduzierbare Git- und Artefaktmetriken und aktualisiert nur den markierten
Profil-2-Block in `docs/project-statistics.md`. Diagramme verwenden ASCII,
exakte Werte sowie deutsche und englische Textalternativen.

*The renderer reads `docs/project-statistics.config.json`, derives reproducible
Git and artifact metrics, and updates only the marked Profile 2 block in
`docs/project-statistics.md`. Charts use ASCII, exact values, and German and
English text alternatives.*

## OPTIONS

| Bash | PowerShell | Bedeutung / Meaning |
| --- | --- | --- |
| `--repo PATH` | `-Repo PATH` | Zielrepository / target repository |
| `--check-only` | `-CheckOnly` | Drift pruefen / check drift |
| `--dry-run` | `-WhatIf` | Vorschau ohne Schreiben / preview without writing |
| `--json` | `-Json` | Maschinenlesbarer Status / machine-readable status |
| `--help` | `Get-Help` | Hilfe / help |

## EXIT STATUS

| Code | Bedeutung / Meaning |
| ---: | --- |
| 0 | Aktuell oder erfolgreich / current or successful |
| 1 | Generierter Block driftet / generated block has drift |
| 2 | Aufruf/Schema/Git/Tooling fehlerhaft / invalid |

## ACCESSIBILITY

Heatmaps verwenden `0` bis `4`; `-` kennzeichnet noch nicht abgelaufene Tage.
Gauges verwenden `#` und `.`. Jede Grafik besitzt genaue Zahlen und eine
bilinguale Textalternative.

*Heatmaps use `0` through `4`; `-` marks days that have not elapsed. Gauges use
`#` and `.`. Every chart includes exact values and a bilingual text
alternative.*

## REPRODUCIBLE TESTING

Wenn `SOURCE_DATE_EPOCH` gesetzt ist, verwendet der Renderer diesen Zeitpunkt
als reproduzierbares Auswertungsdatum. Der normale Betrieb laesst die Variable
unbelegt und verwendet das aktuelle Datum in der konfigurierten Zeitzone.

*When `SOURCE_DATE_EPOCH` is set, the renderer uses that instant as its
reproducible evaluation date. Normal operation leaves the variable unset and
uses the current date in the configured time zone.*

## SEE ALSO

`init-stats(1)`, `check-homogeneity(1)` und
`maintain-agentic-workspace(1)`, sofern die Manpages installiert sind.
Direkte Repository-Einstiegspunkte:
`scripts/init-stats.*`, `scripts/check-homogeneity.*` und
`scripts/maintain-agentic-workspace.*`.

*See `init-stats(1)`, `check-homogeneity(1)`, and
`maintain-agentic-workspace(1)` when the manpages are installed. Direct
repository entry points are `scripts/init-stats.*`,
`scripts/check-homogeneity.*`, and `scripts/maintain-agentic-workspace.*`.*
