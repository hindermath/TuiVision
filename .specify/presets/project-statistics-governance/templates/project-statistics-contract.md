# Projekttransparenz-Vertrag / Project transparency contract

## Zweck / Purpose

Reproduzierbare Projekttransparenz: gleiche Git-Objekte, Konfiguration,
Werkzeugversion und gleicher Stichtag erzeugen gleiche Ergebnisse.
Bestand und Aktivitaet sind keine Qualitaets-, Sicherheits-, Ausbildungs-
oder KI-Produktivitaetsnachweise. Keine Personenranglisten.

Reproducible project transparency: identical Git objects, configuration,
tool version and cutoff produce identical results. Inventory and activity
do not prove quality, security, learner attainment or AI productivity.
Never rank people using lines or commits.

## Quellen und Grenzen / Sources and boundaries

- Git-Quellrevision, Konfiguration, Methodik, Renderer und generierter Abschnitt
  werden gebunden. / Bind Git revision, configuration, methodology, renderer
  and generated section.
- Status prueft Reproduzierbarkeit und Aktualitaet getrennt, ohne Schreiben.
  / Status checks reproducibility and freshness separately, without writes.
- Init und Update verlangen aktuellen Auftrag, Vorschau und sauberen Git-Stand.
  / Init and Update require current authority, preview and a clean worktree.
- Kein Fetch, Tool-Install, Stash, Commit, Push oder Merge durch das Preset.
  / No fetch, tool installation, stash, commit, push or merge by the preset.
- Fehlende oder unvollstaendige Quellen blockieren mit Exitcode 2.
  Drift liefert 1; Erfolg liefert 0. / Missing or incomplete sources block
  with exit 2; drift is 1; success is 0.
- Referenzszenarien sind aus, bis das Projekt sie ausdruecklich aktiviert.
  Sie bleiben Modellrechnungen. / References are off until explicitly enabled;
  they remain model estimates, never measured time savings.
- Historische Eintraege und manuelle Abschnitte erhalten. Pilotkontexte
  ersetzen keine bestehende Statistik. / Preserve history and authored
  sections. Pilot contexts do not replace existing statistics.
- Gemeinsame Ausgaben sequenziell nach Feature-/Implementierungsabschluss
  pflegen. / Serialize shared updates after feature/implementation completion.

## Bedienung / Operation

Nutze die installierten `speckit.statistics-init`,
`speckit.statistics-status` und `speckit.statistics-update` Commands.
Der Konfigurationspfad bestimmt einen getrennten Kontext; darin liegen
`config.json`, `report.md` und `snapshot.json`.
Konfiguration nach Init bewusst pruefen und committen, bevor Update startet.
Die Git-Lieferung erfolgt ausserhalb dieses Presets unter eigener Autoritaet.

Use the installed commands with an explicit context. The configuration
directory contains configuration, report and snapshot. Review and commit the
configuration after Init before running Update. Git delivery is separate and
requires its own authority.
