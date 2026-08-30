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

<!-- source-reference-policy:begin -->
## Drei-Achsen-Quellenpolicy / Three-Axis Source Reference Policy

Aktuelle TuiVision-Anforderungen, Spezifikationen, Public Contracts und Tests
sind die Produktnorm. Bei historisch berührten Änderungen wird
`magiblot/tvision` zuerst als moderne, nicht normative Designreferenz am Commit
`57b6f56b38e0ee75240a80a10ee0e11470c24693` und Tree
`96dd03873955689ff0a79f6c8107a8148fe1ebd6` geprüft; Borland und `tv203s/`
bestimmen historische Absicht. Vergleichs- und Consumer-Evidence bleiben
eigenständige Rollen. Die Entscheidung lautet genau `AdoptModernization`,
`PreserveHistoricalIntent`, `IntentionalTuiVisionDeviation` oder `N/A`.

Quellenrang allein löst keinen Konflikt. Externe Checkouts und Quellkopien
bleiben außerhalb des Repositorys; der Lizenzstatus lautet
`MultipartNotRepositoryWideMIT`. Die Policy gilt `Prospective`; nur geänderte
TuiVision-Verträge, ein neuer freigegebener Magiblot-Pin oder materiell neue
Consumer-Evidence lösen eine Re-Evaluation aus. Bewegliche Branches sind keine
Evidence.

*Current TuiVision contracts remain normative. The exact Magiblot pin is
reviewed first for modern design without replacing historical or consumer
evidence. The no-copy policy applies prospectively with three closed triggers.*
<!-- source-reference-policy:end -->

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

Wave 6, Portfolioaudit, unabhängiger Portfolioabschluss, Constitution-
Revalidation, Quellenpolicy, Transactional Form Model, die
Documentation-Publishing-Closure, das Sandbox-Security-Hardening und die
RL-SE-Checklist-Selbstprüfung sind abgeschlossen. Bevorzugter nächster
fachlicher Intake ist die unabhängige GSDB-Spec-Kit-Intensivprüfung. Die
Reihenfolge autorisiert keinen impliziten Specify-, Implementierungs- oder
Remote-Schritt.

*Wave 6, the portfolio audit, independent portfolio closure, constitution
revalidation, source policy, the Transactional Form Model, Documentation
Publishing Closure, sandbox security hardening, and the RL-SE checklist
self-review are complete. The independent GSDB Spec Kit intensive review is
the preferred next intake. Ordering grants no implicit specification,
implementation, or remote authority.*

## Änderungskontrolle / Change Control

Aktive Lastenhefte werden nur mit den Intake-Authoring-Befehlen geändert.
Reihenfolge, Abhängigkeiten und Lifecycle werden nur über Intake Sequencing
gepflegt. Die Root-Indizes sind erzeugte Ansichten und keine unabhängigen
Entscheidungsquellen.
