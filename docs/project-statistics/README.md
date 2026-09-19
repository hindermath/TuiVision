# Reproduzierbare Projekttransparenz / Reproducible project transparency

## Zweck und Einstieg / Purpose and entry

Dieser getrennte Kontext beschreibt den Git-getrackten Textbestand und die
sichtbare Aenderungsaktivitaet von TuiVision. Die
[bisherige Profil-2-Statistik](../project-statistics.md) bleibt kanonisch;
historische Eintraege und Referenzwerte werden nicht migriert.
Der [Bericht](report.md) ist lesbar, der [Snapshot](snapshot.json) bindet
Quellrevision, Stichtag, Methodik und Hashes. Diese Werte bewerten weder
Personen, Lernleistung, Qualitaet, Sicherheit noch KI-Produktivitaet.

This separate context describes tracked text and visible Git activity.
Existing Profile 2 remains authoritative. The report is human-readable;
the snapshot binds source revision, cutoff, methodology and hashes.
These values do not measure people, learning, quality, security or AI productivity.

## Vertrag und Unterschiede / Contract and differences

- `project-transparency/1`, UTC, 52 Wochen; Referenz-Modellrechnungen aus.
- Der vorhandene Ausschluss von `Directory.Build.props` wird uebernommen.
- Keine Phasenwerte kopiert: alte manuelle Phasensummen sind keine neue Messung.
- Der gesamte neue Kontext wird von beiden Berechnungen ausgeschlossen.
- Abweichende Zeitzone und eigener Methodikvertrag koennen andere Aktivtage
  ergeben. Gleiche Zahlen werden nicht pauschal vorausgesetzt.
- Bestand gehoert zur Quellrevision; der Stichtag begrenzt die Aktivitaet,
  nicht eine historische Rekonstruktion des gesamten Repositorys.

Use UTC and 52 weeks with reference estimates disabled. Preserve the existing `Directory.Build.props` exclusion. Do not reinterpret authored phase totals as
fresh measurements. Exclude this output context from both calculations.
Different timezone/methodology contracts may produce different activity counts.
Inventory describes the source commit; the cutoff limits the activity window.

## Bedienung und Pflege / Operation and maintenance

Voraussetzungen: Git mit vollstaendiger Historie, PowerShell 7 und das
installierte Preset. Befehle ab Repository-Wurzel ausfuehren. Zuerst lesen:

```bash
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh status --repo . --config docs/project-statistics/config.json --json
```

```powershell
pwsh -NoProfile -File .specify/presets/project-statistics-governance/scripts/project-statistics.ps1 -Action Status -Repo . -Config docs/project-statistics/config.json -Json
```

Nach abgeschlossenem Feature oder Implementierungsabschnitt: fachliche
Inhaltsaenderungen zuerst committen; dann Update mit ausdruecklichem Auftrag
und sauberem Git-Stand. Auf Unix zuerst Vorschau mit `--dry-run`, danach
derselbe Befehl ohne diese Option; Windows verwendet `-WhatIf`.
Den Stichtag bewusst waehlen und im Nachweis erhalten.

```bash
bash .specify/presets/project-statistics-governance/scripts/project-statistics.sh update --repo . --config docs/project-statistics/config.json --as-of 2026-09-19 --dry-run --json
```

Danach die alte Profil-2-Statistik aus derselben Inhaltsrevision aktualisieren,
generierte Ausgaben committen und beide
Statuspruefungen ausfuehren. Keine dirty-tree-Ausnahme. Die CI schreibt keine
Statistiken; nach neuen fachlichen Aenderungen wird der Nachlauf wiederholt.
Exit 0: aktuell; Exit 1: Drift/fehlender Snapshot; Exit 2: blockiert/ungueltig.

Run from the repository root with complete Git history and PowerShell 7.
Status is read-only. After feature/implementation completion, commit content
first, preview an explicitly authorized update, then generate using an explicit
cutoff. Refresh legacy Profile 2 from the same content revision, commit outputs, and verify both contexts. No dirty-tree bypass or
automatic CI commits. Exit 0 is current, 1 is drift, and 2 is blocked/invalid.

## Nachweise und Abnahme / Evidence and acceptance

[Rollout- und Installationsnachweis](../maintenance/project-statistics-rollout-v010.md)
und [Pruefkommando](../man/prove-project-statistics-context.1.md).
CI-Artefakte enthalten native Linux-/Windows-Ergebnisse am exakten Liefer-Head;
lokale macOS-Ergebnisse werden im PR verlinkt. Technische Ergebnisse und
fachliche Abnahme durch Thorsten bleiben getrennt. Kein Admin-Bypass.

See the integration receipt and proof-command manual. CI artifacts bind native
Linux/Windows results to the exact delivery head; the PR links macOS evidence.
Technical results do not grant human acceptance or admin bypass.
