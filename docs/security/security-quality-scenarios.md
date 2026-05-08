# Sicherheits-Qualitätsszenarien / Security Quality Scenarios: TuiVision

**Projekt / Project**: TuiVision (Level-2)
**Datum / Date**: 2026-04-24
**Status**: Stub — mit projektspezifischen Inhalten zu befuellen / Stub — to be populated
**Template-Quelle / Template Source**: `.specify/templates/security-quality-scenarios-template.md`

<!--
  Dieses Dokument ist ein Stub. Die vollstaendige Struktur findet sich im
  Template unter .specify/templates/security-quality-scenarios-template.md. Bei der Befuellung das Template als Vorlage
  verwenden.

  This document is a stub. The complete structure can be found in the
  template at .specify/templates/security-quality-scenarios-template.md. Use the template as a guide when populating.
-->

[Zu befuellen / To be populated — see template]

## 011-port-wave2-examples

| Szenario / Scenario | Erwartung / Expected Result |
|---|---|
| Fehlerhafte `dlgdsn`-Beschreibung | Malformed, incomplete, duplicate-control und invalid-navigation Varianten werden sichtbar abgelehnt. / Malformed, incomplete, duplicate-control, and invalid-navigation variants are visibly rejected. |
| Ungueltiger Standarddialog-Pfad | `demo` zeigt eine sichtbare Invalid-Path-Entscheidung ohne Dateiinhalt zu lesen oder zu schreiben. / `demo` shows a visible invalid-path decision without reading or writing file content. |
| Isoliertes Clipboard | `clipboard` zeigt einen Fallback-Zustand statt den Test still zu ueberspringen. / `clipboard` shows a fallback state instead of silently skipping the test. |
| Fortschrittsabbruch | `tprogb` erreicht einen sichtbaren Abbruchzustand ohne Wall-Clock-Abhaengigkeit. / `tprogb` reaches a visible canceled state without wall-clock dependency. |
