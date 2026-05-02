# Abhängigkeits-Audit / Dependency Audit: TuiVision

**Projekt / Project**: TuiVision (Level-2)
**Datum / Date**: 2026-04-24
**Status**: Stub — mit projektspezifischen Inhalten zu befuellen / Stub — to be populated
**Template-Quelle / Template Source**: `.specify/templates/dependency-audit-template.md`

<!--
  Dieses Dokument ist ein Stub. Die vollstaendige Struktur findet sich im
  Template unter .specify/templates/dependency-audit-template.md. Bei der Befuellung das Template als Vorlage
  verwenden.

  This document is a stub. The complete structure can be found in the
  template at .specify/templates/dependency-audit-template.md. Use the template as a guide when populating.
-->

## 010-standard-dialogs-designer

Datum: 2026-05-02.

Keine neue NuGet-, npm- oder sonstige Laufzeitabhaengigkeit wurde fuer die
Feature-Implementierung hinzugefuegt. Die Arbeit nutzt vorhandene
Projektgrenzen: `TuiVision.Controls`, `TuiVision.Serialization`, MSTest und die
bereits bestehende Coverlet-Testinfrastruktur.

No new NuGet, npm, or other runtime dependency was added for this feature. The
work uses existing project boundaries: `TuiVision.Controls`,
`TuiVision.Serialization`, MSTest, and the already existing Coverlet test
infrastructure.

| Bereich / Area | Ergebnis / Result |
|---|---|
| Neue Produktionsabhaengigkeiten / New production dependencies | keine / none |
| Neue Testabhaengigkeiten / New test dependencies | keine / none |
| Bekannte kritische CVE durch neue Pakete / Known critical CVE from new packages | nicht anwendbar / not applicable |
| Release-SBOM/VEX | bei Release erzeugen bzw. aktualisieren / create or update at release time |
