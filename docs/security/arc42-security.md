# Sicherheits-Querschnittskonzepte / Security Cross-Cutting Concepts: TuiVision

**Projekt / Project**: TuiVision (Level-2)
**Datum / Date**: 2026-04-24
**Status**: Stub — mit projektspezifischen Inhalten zu befuellen / Stub — to be populated
**Template-Quelle / Template Source**: `.specify/templates/arc42-security-template.md`

<!--
  Dieses Dokument ist ein Stub. Die vollstaendige Struktur findet sich im
  Template unter .specify/templates/arc42-security-template.md. Bei der Befuellung das Template als Vorlage
  verwenden.

  This document is a stub. The complete structure can be found in the
  template at .specify/templates/arc42-security-template.md. Use the template as a guide when populating.
-->

[Zu befuellen / To be populated — see template]

## 011-port-wave2-examples

Datum: 2026-05-08.

Die Sicherheits-Querschnittskonzepte bleiben lokal und proportional:

- Validierung vor Nutzung: `dlgdsn` validiert strukturierte
  Dialogbeschreibungen vor Runtime-Erzeugung.
- Nicht-destruktive Pfade: `demo` zeigt Dateisystem-Metadaten, Wildcards,
  manuelle Pfade und Fehlerzustaende ohne Dateiinhalt-I/O.
- Deterministische Ausfuehrung: Progress- und Smoke-Flows nutzen keine
  unkontrollierten Timer oder Hintergrundarbeit.
- Sichere Meldungen: sichtbare Fehlerzustaende enthalten keine Secrets,
  Tokens, Stack-Traces oder lokalen privaten Verlaufsdaten.

Security cross-cutting concepts remain local and proportional: validate before
use, keep file paths non-destructive, make execution deterministic, and keep
messages free of secrets or internal traces.
