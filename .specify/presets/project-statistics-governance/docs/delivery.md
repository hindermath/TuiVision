# Lieferung und Feldtest / Delivery and field test

## Autorisierter Umfang / Authorized scope

Owner und fachlicher Reviewer: @hindermath. Zentrales Tracking:
[Preset #1](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/issues/1).
Piloten:
[Home #298](https://github.com/hindermath/home-baseline/issues/298),
[TinyCalc #84](https://github.com/hindermath/TinyCalc/issues/84),
[Sandbox #70](https://github.com/hindermath/absdd-image-sandbox/issues/70).

MIT-Paket, v0.1.0-Pre-Release, drei getrennte Pilotberichte und kanonischer
Feldbericht. MergeAndSync mit administrativem Review-Bypass nur nach
erfolgreichen technischen Checks am exakten Head. Keine stabile Freigabe,
Community-Einreichung, allgemeine Flottenverteilung oder Legacy-Abloesung.
Die menschliche Sandbox-Pruefung vor Commit/Push bleibt ein separates Gate.

MIT package, v0.1.0 pre-release, three separate pilot reports and a canonical
field report. MergeAndSync allows administrative review bypass only after
technical checks pass on the exact head. No stable promotion, community
submission, fleet rollout or replacement of existing statistics.
Human sandbox review before commit/push remains a separate gate.

## Dokumentationsauswirkung / Documentation impact

UpdateRequired. Owner: Thorsten Hindermann. Zielgruppen: Lernende, Maintainer,
Pruefende. Leserpfad: README -> Methodik/Vertrag -> CLI-Hilfe -> Feldbericht.
Quelle: dieses Repository; Dokumentklasse: ActiveSemantic, Nachweise getrennt.
DE zuerst/EN danach, ASCII-Diagramme mit exakten Textwerten.
Distribution: Preset-Paket; kein Home-Runtime-Sync.
Plattformnachweis: native CI macOS/Linux/Windows, plus lokaler macOS-Safe-Mode.
Ergebnisse erst nach ausgefuehrten Tests im Feldbericht behaupten.
Wiedervorlage: bei Version, Methodik, Konfiguration oder Quellenwechsel.

UpdateRequired. Owner: Thorsten Hindermann. Audience: learners, maintainers and
reviewers. Reader path: README -> methodology/contract -> CLI help -> field report.
Canonical source: this repository. Active semantic docs, separate evidence.
German first, then English; ASCII diagrams with exact textual values.
Distribution: preset package, no Home Runtime sync. Native platform evidence
comes from CI and local macOS safe-mode checks; never claim unexecuted tests.
Reevaluate on version, methodology, configuration or source changes.
