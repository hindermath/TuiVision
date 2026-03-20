# Specification Quality Checklist: Dialog-/Control-Schicht

**Purpose**: Validate specification completeness and quality before proceeding to planning
**Created**: 2026-03-21
**Updated**: 2026-03-21 (Durchführungshinweise ergänzt nach Clarify-Session)
**Feature**: [../spec.md](../spec.md)

---

## Content Quality

- [x] **No implementation details (languages, frameworks, APIs)**

  > **Durchführungshinweis**: Spec nach Schlüsselwörtern wie `C#`, `.NET`, `MSTest`, `class`, `interface`, `namespace`, `dotnet`, `NuGet`, `override` absuchen. Solche Begriffe dürfen nur in Klammern als Klassenname (z. B. `TDialog`) vorkommen, nicht als Technologieentscheidung. Fragetest: "Könnte diese Anforderung auch mit einer anderen Sprache umgesetzt werden?" — wenn ja, ist sie korrekt technologiefrei.

- [x] **Focused on user value and business needs**

  > **Durchführungshinweis**: Jeden Satz in den User Stories und Requirements daraufhin prüfen, ob er beschreibt, *was* ein Nutzer oder Entwickler erreichen will — nicht *wie* das System es intern umsetzt. Sätze, die mit "Das System verwaltet intern..." oder "Die Klasse speichert..." beginnen, sind verdächtig. Erlaubt ist: "Ein Entwickler kann … einbauen, damit Nutzer …".

- [x] **Written for non-technical stakeholders**

  > **Durchführungshinweis**: Spec einer Person vorlegen (oder selbst simulieren), die kein C++ oder Turbo Vision kennt. Versteht sie, *was* das Feature leisten soll? Technische Klassennamen (`TDialog`, `TButton`) sind als fachliche Entitätsnamen erlaubt; interne Algorithmen oder Datenstrukturen gehören jedoch nicht in die Spec.

- [x] **All mandatory sections completed**

  > **Durchführungshinweis**: Spec-Template mit der vorliegenden Spec abgleichen. Pflichtabschnitte sind: `## User Scenarios & Testing`, `## Requirements` (mit `### Functional Requirements`), `## Success Criteria` (mit `### Measurable Outcomes`). Keiner dieser Abschnitte darf leer oder mit Template-Platzhaltern befüllt sein (z. B. "[Describe this user journey]").

---

## Requirement Completeness

- [x] **No [NEEDS CLARIFICATION] markers remain**

  > **Durchführungshinweis**: Volltextsuche nach `[NEEDS CLARIFICATION` in der Spec-Datei ausführen (z. B. `grep -n "NEEDS CLARIFICATION" spec.md`). Trifft kein Treffer ein, ist dieses Item erfüllt.

- [x] **Requirements are testable and unambiguous**

  > **Durchführungshinweis**: Jeden FR-Satz dem "Tester-Test" unterziehen: Kann ein Tester ohne weitere Rückfragen einen konkreten Testfall ableiten? Prüfmuster: "FR-001 sagt X — wie teste ich X?" Bleibt die Antwort vage ("irgendwie prüfen, ob es funktioniert"), muss der FR präzisiert werden. Gute FRs nennen auslösende Aktion, Vorbedingung und messbares Ergebnis.

- [x] **Success criteria are measurable**

  > **Durchführungshinweis**: Jeden SC-Eintrag auf konkrete Zahlen oder Ja/Nein-Prüfbarkeit prüfen. SC-003 nennt "70 % Line Coverage" — klar messbar. SC-001 beschreibt ein vollständiges Szenario, das pass/fail bewertet werden kann. Vage Formulierungen wie "gut", "ausreichend", "angemessen" sind ein Warnsignal.

- [x] **Success criteria are technology-agnostic (no implementation details)**

  > **Durchführungshinweis**: SCs auf Begriffe wie `dotnet test`, `coverlet`, `MSTest`, `docfx` prüfen. Diese sind als *Messwerkzeug-Hinweis* in Klammern erlaubt (wie in SC-003), aber das Kriterium selbst muss ohne Werkzeugkenntnis verständlich sein: "mindestens 70 % Line Coverage" ist werkzeugagnostisch; "coverlet gibt > 70 % aus" wäre eine Implementierungsdetail.

- [x] **All acceptance scenarios are defined**

  > **Durchführungshinweis**: Für jede User Story prüfen, ob alle Haupt-Interaktionen (Happy Path + mind. 1 Fehlerpfad, soweit fachlich sinnvoll) als Given/When/Then-Szenario vorliegen. US-1 hat 4 Szenarien (Öffnen, Tab, Enter, Escape) — vollständig. US-6 hat 2 — ausreichend für statische Controls. Fehlende Szenarien: neue Zeile ergänzen.

- [x] **Edge cases are identified**

  > **Durchführungshinweis**: Abschnitt `### Edge Cases` lesen. Jeder Eintrag sollte eine konkrete Grenz- oder Fehlersituation beschreiben (leer, null, Limit, außerhalb Bounds). Prüfen: Gibt es weitere offensichtliche Grenzen, die noch nicht genannt sind? (z. B. `TInputLine` mit Maximallänge = 0 ist notiert; `TScrollBar` an Grenze ist notiert.)

- [x] **Scope is clearly bounded**

  > **Durchführungshinweis**: Abschnitt `## Assumptions` lesen. Sind alle absichtlich *nicht* enthaltenen Komponenten explizit genannt? Hier: `TDirListBox`, `TFileDialog`, Standard-Dialoge aus `dialogs.h`. Jede Klasse aus dem C++-Original, die *nicht* portiert wird, sollte namentlich ausgeschlossen oder einer späteren Phase zugeordnet sein.

- [x] **Dependencies and assumptions identified**

  > **Durchführungshinweis**: Prüfen ob alle externen Voraussetzungen im `## Assumptions`-Abschnitt stehen: Welche bestehenden Klassen werden vorausgesetzt (`TView`, `TGroup`, `TEvent`, etc.)? Welche Phasen müssen abgeschlossen sein? Welche Teile des TuiVision-Systems (Event-Loop, Console-Buffer) müssen bereits funktionieren?

---

## Feature Readiness

- [x] **All functional requirements have clear acceptance criteria**

  > **Durchführungshinweis**: Jedes FR (FR-001 bis FR-012) einer User Story oder einem Acceptance Scenario zuordnen. FR-001 → US-1; FR-002 → US-2; FR-003/004 → US-3; FR-005 → US-4 (inkl. Default-Button-Szenario); FR-006 → US-5; FR-007 → US-6. Unmapped FRs (FR-010 bis FR-012: Event-Integration, XML-Doku, Tests) haben SC-002/003/006 als Abnahmekriterium. Alle FRs sind abgedeckt.

- [x] **User scenarios cover primary flows**

  > **Durchführungshinweis**: Die 6 User Stories mit den 13 zu portierenden Klassen abgleichen: US-1 → TDialog; US-2 → TInputLine + TLabel; US-3 → TListViewer + TListBox + TStringList + TScrollBar + TScroller; US-4 → TButton; US-5 → TCluster + TCheckBoxes + TRadioButtons; US-6 → TStaticText + TLabel. Jede Klasse ist in mindestens einer Story referenziert.

- [x] **Feature meets measurable outcomes defined in Success Criteria**

  > **Durchführungshinweis**: SC-001 bis SC-006 gegen die FRs und User Stories querprüfen. SC-001 deckt Integration aller Hauptcontrols ab. SC-002 prüft Vollständigkeit (alle 13 Klassen). SC-003 setzt 70 % Coverage als Gate. SC-004/005 prüfen Keyboard- und Render-Verhalten. SC-006 prüft Dokumentation. Jedes SC sollte mindestens einem FR zugeordnet werden können.

- [x] **No implementation details leak into specification**

  > **Durchführungshinweis**: Gezielte Suche nach Programmiersprachen-Konstrukten in Requirements und User Stories: `public`, `override`, `virtual`, `sealed`, `base.`, `:`, `=>`, `new()`. Diese gehören in den Plan, nicht in die Spec. Klassennamen wie `TDialog` sind fachliche Entitäten und erlaubt; Vererbungshierarchien (`TDialog : TWindow`) sind Implementierungsdetails.

---

## Notes

- Scope boundary ist klar im `## Assumptions`-Abschnitt dokumentiert: `TDirListBox`, `TFileDialog` und Standard-Dialoge aus `dialogs.h` sind explizit ausgeschlossen (Phase 6).
- Maus-Interaktion ist auf den Stand des bestehenden Event-Systems begrenzt; vollständige Treiberintegration ist Phase 7.
- Alle 13 Zielklassen sind in SC-002 und FR-001 bis FR-012 explizit aufgeführt.
- Clarify-Session 2026-03-21: 3 Klärungen integriert (Dialog-Ausführungsmodell, Fokus-Wrap, Default-Button).
- Spec ist bereit für `/speckit.plan`.
