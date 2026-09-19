# Project Statistics Governance

Reproduzierbare Projekttransparenz fuer Git und Spec Kit.
Reproducible project transparency for Git and Spec Kit.

## Einstieg / Getting started

Das optionale Preset bindet Textbestand, Artefaktmix und Git-Aktivitaet an
nachpruefbare Quellen. Es misst weder Qualitaet noch Ausbildungsleistung oder
KI-Zeitersparnis. Referenz-Modellrechnungen sind standardmaessig ausgeschaltet.

The optional preset binds text inventory, artifact mix and Git activity to
reproducible sources. It does not measure quality, learner performance or
AI time savings. Reference estimates are disabled by default.

Voraussetzungen: Git mit vollstaendiger lokaler Historie, PowerShell 7 und
Spec Kit >=0.12.8. Bash verwendet dieselbe PowerShell-Engine. Kein GitHub-Konto,
Home-Baseline-Klon oder anderes Governance-Preset ist fuer die Messung notwendig.

Requirements: Git with complete local history, PowerShell 7 and Spec Kit
>=0.12.8. Bash delegates to the same engine. Measurement needs no GitHub account,
Home Baseline checkout or companion governance preset.

## Installation / Installation

Der folgende Befehl gilt nach Veroeffentlichung des v0.1.0-Pre-Releases.
Vor Installation Quellcode und veroeffentlichten ZIP-SHA-256 pruefen.
This command applies once the v0.1.0 pre-release is published. Review source
code and the published ZIP SHA-256 before installation.

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-project-statistics-governance/archive/refs/tags/v0.1.0.zip --priority 90
specify preset info project-statistics-governance
specify preset resolve project-statistics-contract
```

Installation startet keine Messung oder Migration. Zur lokalen Entwicklung:
`specify preset add --dev <package-directory> --priority 90`.
Dabei ein entpacktes Git-Archiv ohne `.git` verwenden, keinen Arbeitsklon.
Installation starts no measurement or migration. The development command uses
an explicitly selected unpacked Git archive without `.git`, not a working clone.

## Sicherer Ablauf / Safe workflow

1. `speckit.statistics-init`: Vorschau, dann neue Konfiguration/Vorlage anlegen.
2. Konfiguration pruefen und separat committen; nur eigene freigegebene Dateien.
3. `speckit.statistics-update`: Vorschau, dann Bericht und Snapshot erzeugen.
4. `speckit.statistics-status`: Reproduzierbarkeit und Aktualitaet read-only pruefen.

1. Preview initialization, then create new configuration/template files.
2. Review and separately commit the configuration; only authorized owned files.
3. Preview the update, then create the report and snapshot.
4. Check reproducibility and freshness without writing.

Direkter macOS/Linux-Einstieg / Direct macOS/Linux entry point:

```bash
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh init --dry-run
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh status --json
```

Windows / Windows:

```powershell
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Init -WhatIf
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Status -Json
```

Default-Kontext: `docs/project-statistics/config.json`. Die drei Feldtests nutzen
`--config docs/project-statistics-pilot/config.json` beziehungsweise `-Config`.
Bestehende Statistiken bleiben kanonisch. Das Preset fuehrt keinen Fetch,
Tool-Install, Stash, Commit, Push oder Merge aus. Exitcodes: 0 Erfolg, 1 Drift,
2 Fehler oder blockierte Voraussetzung.

The default context is separate from legacy reports; pilots use the explicitly
selected pilot path. No fetch, tool install, stash, commit, push or merge.
Exit codes: 0 success, 1 drift, 2 invalid input or blocked prerequisite.

## Vertiefung / Details

- [Messmethodik und Grenzen / Methodology and limits](docs/methodology.md)
- [Portabler Vertrag / Portable contract](templates/project-statistics-contract.md)
- [CLI-Referenz / CLI reference](docs/man/project-statistics.1.md)
- [Herkunft und Lieferkette / Provenance and supply chain](docs/provenance.md)
- [Lieferung und Feldtest / Delivery and field test](docs/delivery.md)

## Tests und Status / Tests and status

```powershell
pwsh -NoProfile -File tests/test-project-statistics.ps1
pwsh -NoProfile -File tests/test-installed-preset.ps1
```

Native CI prueft macOS, Linux und Windows. Ein gruener technischer Test ersetzt
keine menschliche Abnahme. Ziel ist v0.1.0 als Pre-Release; stabile Freigabe,
Community-Einreichung und Legacy-Migration sind separate Entscheidungen.
Feldtest-Tracking: [Issue #1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/issues/1).

Native CI covers macOS, Linux and Windows. Passing technical tests does not
grant human acceptance. Target: v0.1.0 pre-release. Stable release, community
submission and legacy migration require separate decisions.
