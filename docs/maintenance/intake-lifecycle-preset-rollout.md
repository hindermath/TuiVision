# Intake-Lifecycle-Preset-Rollout / Intake lifecycle preset rollout

Stand: 2026-09-13. Owner: TuiVision Maintainer (Thorsten Hindermann).

Die drei bestehenden Presets wurden aus veroeffentlichten Tag-ZIPs installiert.
Die Ausfuehrung einschliesslich `MergeAndSync` und Admin-Bypass ist ausdruecklich
beauftragt. Technische Gates bleiben vor dem Merge erforderlich. Diese Freigabe
ist keine Behauptung einer unabhaengigen menschlichen Codepruefung.

| Preset | Vorher | Installiert | Prioritaet |
|---|---|---|---|
| Authoring | 0.3.1 | [0.3.2](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases/tag/v0.3.2) | 64 |
| Review | 0.2.1 | [0.2.2](https://github.com/hindermath/spec-kit-preset-intake-review-governance/releases/tag/v0.2.2) | 65 |
| Sequencing | 0.2.3 | [0.2.4](https://github.com/hindermath/spec-kit-preset-intake-sequencing-governance/releases/tag/v0.2.4) | 66 |

Die Tag-ZIPs wurden heruntergeladen, ausgepackt und dateiweise mit den getesteten
Merge-Staenden verglichen (38/29/39 Dateien). Anschliessend wurden die installierten
Dateien mit diesen ZIPs verglichen. URLs, SHA-256, Commits, Manifestbindungen und
Coverage-Zahlen stehen in [der maschinenlesbaren Evidence](intake-lifecycle-preset-rollout.json).
Die Vorschau erfolgte in einem isolierten Checkout; die echte Installation nutzt
`specify preset remove` und `specify preset add --from <Tag-ZIP> --priority <Wert>`.

Die bisherige lokale Lifecycle-Korrektur wird durch die allgemeinen Regeln der
Quellpresets ersetzt. Die Projektpruefung `validate-requirements-intake-alignment`
und ihre exakten Zuordnungs-Fixtures bleiben erhalten. Alte Feldnotizen innerhalb
der Pakete werden durch die versionierte Lifecycle-Dokumentation ersetzt; deren
historischer TuiVision-Stand bleibt im Baseline-Commit erhalten. Alle anderen
Preset-Eintraege, aktive Projekterweiterungen, Intakes und Receipts bleiben erhalten.
Weitere Verbraucher und die zentrale Standard-Achtermatrix werden nicht veraendert.

## Pruefnachweise / Validation evidence

- Drei Konfigurationsvalidatoren: identisches `Aligned`-JSON in Bash und PowerShell.
- Zehn `Completed`-Mitglieder im Archiv, null aktive Dateien, null aktive Serienziele,
  null `Eligible`; Manifest und Serien-Receipt gueltig in beiden Shells.
- Zehn historische Authoring-Receipts gueltig in beiden Shells. Git-Diff und
  untracked Inventar bleiben waehrend der Validatoraufrufe unveraendert.
- Projektspezifisch: vier positive und 18 negative Fixtures, zehn Zuordnungen,
  sechs Kanten, ein letzter Abschluss und ein Backlog-Eintrag.
- Vollstaendiger Release-Testlauf: 1028 bestanden, null Fehler, null uebersprungen.
  Coverage: Core 94.97 %, Controls 86.96 %, Serialization 90.47 %,
  Compatibility 83.33 %, Drivers.Console 93.93 %. Gate: jede Assembly mindestens
  70 %. Aggregation vereinigt Source-Datei/Zeilennummer ueber die fuenf
  Cobertura-Berichte. Beispiel-Smokes laufen ohne eigenen Coverage-Collector;
  sie sind gemaess `coverlet.runsettings` nicht Teil des Coverage-Gates.
- Der erste Testlauf erkannte drei veraltete Audit-Evidence-Bindungen. Nach
  semantischer Revalidierung wurden sechs Quellen neu gebunden und die drei
  erwarteten Preset-Versionen aktualisiert. Kein Kontrollstatus wurde verbessert
  und keine Pruefung deaktiviert. Buildzaehler: 522 fuer den ersten, 523 fuer den
  erfolgreichen zweiten Testaufruf.
- Der bestehende Requirements-Workflow prueft installierte Presets, Manifest und
  alle Receipts jetzt auf macOS, Linux und Windows in beiden Shells. Native PR-CI
  und frischer Checkout bleiben Abschlussnachweise des Merge-Vorgangs.

## Dokumentationsauswirkung / Documentation impact

Entscheidung: `UpdateRequired`. Zielgruppen: Maintainer, Agenten und Pruefende.
Leserpfad: README / Agent-Guidance → dieser Rollout → JSON und native CI.
Kanonische Quellen: Preset-Tags, lokale Registry und ausgefuehrte Pruefungen;
Owner: TuiVision Maintainer. Dokumentklasse: Betriebs-/Release-Evidence.
README, fuenf Agent-Dateien, optionale Version in der Constitution und Audit-
Projektionen wurden gemeinsam aktualisiert; die Root-Constitution wurde geprueft
und benoetigt keine Versionsaenderung. DE/EN stehen gemeinsam in diesem Dokument
und in der Guidance. CLI-Ausgaben sind textbasiert; A11Y benoetigt keine Farbe.
Distribution: repository-lokal, kein Home-Sync. Statistik wird aus ihrer bestehenden
Konfiguration gerendert. Re-Evaluation bei Preset-, Registry- oder Lifecycle-Drift.
C#-Produktlogik, APIs und XML-Dokumentation sind unveraendert; der C#-Test aendert
nur drei erwartete Versionsliterale. Native Build-/DocFX-Gates bleiben verbindlich.

## English

The existing three presets were upgraded from published tag ZIPs with unchanged
priorities. Every archive file matches the tested merge tree and installed files.
The isolated installation preview preserved project-specific validators, fixtures,
receipts and unrelated presets. Only TuiVision is rolled out; the central matrix
and other consumers are unchanged.

Both shells prove ten archived completed members, no active or executable targets,
and valid manifest, series receipt and ten historical authoring receipts without
checkout changes. All 1028 .NET tests pass, and each required assembly exceeds
70% line coverage. Six audit source hashes and three version expectations were
revalidated after the update; historical control dispositions were preserved.
Native CI now repeats installed lifecycle checks on macOS, Linux and Windows.

Documentation impact is `UpdateRequired`, owned by the TuiVision maintainer.
README and all five agent surfaces lead here, then to JSON and CI evidence.
Documentation is repository-local, bilingual and text accessible; no Home sync
is needed. Reassess after preset, registry or lifecycle drift. Explicit current
MergeAndSync/admin-bypass authority applies after technical gates and does not
assert independent human review.
