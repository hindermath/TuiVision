# Pflichtenheft: TuiVision

## Zweck / Purpose

TuiVision ist eine moderne, idiomatische C#/.NET-10-Interpretation von Turbo
Vision 2.0.3. Historische Quellen erklären Absicht und Verträge, werden aber
nicht mechanisch kopiert. Das Framework bleibt vollständig managed,
plattformbewusst, testbar, dokumentiert und für Lernende zugänglich.

*TuiVision is a modern, idiomatic C#/.NET 10 interpretation of Turbo Vision
2.0.3. Historical sources explain intent and contracts but are not copied
mechanically. The framework remains fully managed, platform-aware, testable,
documented, and accessible to learners.*

## Normative Baseline / Normative Baseline

Die vor der Intake-Konsolidierung gültige vollständige Fassung ist bytegleich
archiviert:

- `requirements/baseline/Pflichtenheft.pre-intake-split.2026-07-26.md`

Ihre 167 atomisierten Checklist-Aussagen, Evidence und Restlücken sind hier
klassifiziert:

- `specs/requirements-reconciliation-20260726/requirements-coverage.json`
- `specs/requirements-reconciliation-20260726/reconciliation-report.md`

Die Baseline wird nicht mehr als parallele Fortschrittsliste bearbeitet.
Produktfortschritt folgt ausschließlich aus Feature-Evidence,
Intake-Receipts und dem Serien-Lifecycle.

## Verbindliche Produktregeln / Binding Product Rules

- C#/.NET 10, idiomatische C#-Architektur und keine native Pflichtabhängigkeit.
- Historische Quellen unter `tv203s/`, `TVDEMOS/` und `TVFM/` bleiben
  read-only Referenzen.
- Öffentliche APIs besitzen vollständige XML-Dokumentation.
- Nicht triviale Logik wird auf didaktischen Kommentarwert geprüft.
- Pflichtgates umfassen Release-Build, Tests, fünf Assembly-scharfe
  Coverage-Schwellen, Formatierung, DocFX/A11Y und relevante Plattformgates.
- Lernmaterial ist German-first/English-second auf CEFR-B2-Niveau und
  text-first nach `Programmierung #include<everyone>`.
- Runtime-, API-, Paket- oder Scope-Änderungen benötigen einen autorisierten
  aktiven Intake und vollständigen Spec-Kit-Zyklus.

## Aktive Abarbeitung / Active Delivery

- Aktive Intakes: `requirements/intakes/active/`
- Kanonische Serie:
  `requirements/intakes/series/tui-vision-delivery/manifest.json`
- Lesbare Reihenfolge: `Lastenheft_Abarbeitungsreihenfolge.md`
- Abgeschlossene Intakes: `requirements/intakes/archive/`
- Nicht blockierender Backlog: `requirements/intakes/backlog/`

Wave 6 ist durch Feature 037 und PR #139 geschlossen. Bevorzugter nächster
fachlicher Intake ist der Post-Wave-6-Beispielportfolio-Audit. Dieses Dokument
reserviert oder startet Feature 038 nicht.

*Wave 6 is closed by Feature 037 and PR #139. The preferred next product intake
is the post-Wave-6 example-portfolio audit. This document neither reserves nor
starts Feature 038.*

## Änderungskontrolle / Change Control

Aktive Lastenhefte werden nur mit den Intake-Authoring-Befehlen geändert.
Reihenfolge, Abhängigkeiten und Lifecycle werden nur über Intake Sequencing
gepflegt. Die Root-Indizes sind erzeugte Ansichten und keine unabhängigen
Entscheidungsquellen.
