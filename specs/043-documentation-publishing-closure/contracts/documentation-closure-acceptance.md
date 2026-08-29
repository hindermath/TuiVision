# Acceptance Contract: Documentation and Publishing Closure

## Cardinalities

- Genau 7 allgemeine Guide-Themen sind vorhanden und im DocFX-TOC erreichbar.
- Genau 38 Beispielprojekte besitzen genau eine primäre Lernvertragszeile und
  genau einen zugeordneten Detail-Guide.
- Genau 27 `DocumentationAndPublishing`-Anforderungen besitzen genau eine
  Closure-Zeile.

## Allowed decisions

- Beispiel-Guides: `GuideAdequate`, `MatrixCompletesContract`,
  `AcceptedBoundary`.
- Abstimmungs-Closure: `Closed`, `AcceptedBoundary`.
- Dokumentationswirkung: `UpdateRequired`, `NoUpdateRequired`,
  `GeneratedUpdate`, `FollowUp`.

Andere Werte, Duplikate, fehlende IDs, leere Evidence-Pfade oder eine
`AcceptedBoundary` ohne Begründung und Wiederbewertungsauslöser sind
unzulässig.

## Content contract

Jeder allgemeine Guide und jede Beispiel-Lernvertragszeile deckt Lernziel,
Voraussetzungen, Start oder Einstieg, Bedienung oder Ablauf,
Architekturhinweis und Übung oder Abschlussaufgabe ab. Lernende Inhalte sind
Deutsch zuerst, Englisch danach, ungefähr CEFR-B2 und text-first verständlich.

## Publishing contract

- Release-Build mit CS1591 besteht.
- DocFX beendet sich ohne Warnung oder Fehler.
- Playwright/Axe besteht ohne Accessibility-Verstoß.
- Ein Textbrowser-Dump zeigt Zweck, Navigation und nächste Aktion.
- `_site/` und generierte `api/*.yml` werden nicht getrackt.
- Remote-Gates prüfen denselben exakten Feature-Head auf Ubuntu, macOS und
  Windows.

## Scope contract

Der finale Delivery-Diff enthält keine Änderung an `src/`, `tests/`,
`examples/`, Projektdateien, Packages, öffentlichen Signaturen oder
historischen Quellen. Ein Befund außerhalb dieser Grenze wird als Follow-up
dokumentiert und nicht in Feature 043 behoben.
