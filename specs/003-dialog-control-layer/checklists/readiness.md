# Task Readiness Checklist: Dialog-/Control-Schicht

**Purpose**: Validate that the current specification and design artifacts are complete, consistent, and review-ready before deriving implementation tasks.
**Created**: 2026-03-21
**Updated**: 2026-03-21 (Prüfpunkte durchgeführt und Readiness-Lücken geschlossen)
**Feature**: [../spec.md](../spec.md)

**Note**: Diese Checkliste ist für Autor und PR-Reviewer gedacht. Sie prüft die
Qualität der Anforderungen und Designartefakte vor `/speckit.tasks`, nicht die
korrekte Implementierung.

---

## Requirement Completeness

- [x] CHK001 Sind alle 13 Zielklassen im Zusammenspiel von User Stories, Functional Requirements, Datenmodell und Plan vollständig abgedeckt? [Completeness, Spec §User Scenarios, Spec §FR-001..FR-012, Data Model, Plan §Implementation Sequence]

  > **Durchführungshinweis**: [../spec.md](../spec.md), [../data-model.md](../data-model.md) und [../plan.md](../plan.md) nebeneinander prüfen. Für jede Zielklasse (`TDialog`, `TInputLine`, `TListViewer`, `TListBox`, `TStringList`, `TScrollBar`, `TScroller`, `TButton`, `TCluster`, `TCheckBoxes`, `TRadioButtons`, `TStaticText`, `TLabel`) muss erkennbar sein: fachlicher Nutzen in einer Story, Pflicht im FR-Block, Entität im Datenmodell oder explizite Planposition. Fehlende Traceability ist ein Lückensignal.

- [x] CHK002 Sind alle geklärten Dialog-Grundregeln vollständig in den Unterlagen sichtbar: synchron blockierend, Fokus-Wrap-around, Default-Button-Verhalten und Escape → `cmCancel`? [Completeness, Spec §FR-001, Clarifications, Research §R-001/R-003/R-011, Plan §Phase 0]

  > **Durchführungshinweis**: [../spec.md](../spec.md), [../research.md](../research.md) und [../plan.md](../plan.md) auf diese vier Regeln abgleichen. Keine der Regeln darf nur in der Clarify-Liste stehen; sie muss jeweils in mindestens einer normativen Stelle der Spec und einer Design-/Planstelle auftauchen.

- [x] CHK003 Ist die `TListBox`-Doppelklickregel vollständig beschrieben, einschließlich der negativen Abgrenzung, dass kein separates zusätzliches Command-Ereignis entsteht? [Completeness, Spec §FR-003, Spec §User Story 3, Research §R-012, Contract §TListBox]

  > **Durchführungshinweis**: In [../spec.md](../spec.md), [../research.md](../research.md) und [../contracts/public-api.md](../contracts/public-api.md) nach derselben Kernaussage suchen. Entscheidend ist nicht nur „Doppelklick bestätigt Auswahl“, sondern auch die explizite Abgrenzung „kein separates zusätzliches Command-Ereignis“.

## Requirement Clarity

- [x] CHK004 Ist die Escape-Regel präzise genug formuliert, also als Default-Verhalten und nicht als absolute Regel ohne Ausnahme für konsumierende Kind-Controls? [Clarity, Spec §FR-001, Research §R-011, Data Model §TDialog, Contract §TDialog]

  > **Durchführungshinweis**: [../spec.md](../spec.md), [../research.md](../research.md), [../data-model.md](../data-model.md) und [../contracts/public-api.md](../contracts/public-api.md) darauf prüfen, ob die Ausnahme „sofern kein Kind-Control das Ereignis vorher konsumiert“ sauber dokumentiert ist. Fehlt diese Einschränkung in einem Artefakt, bleibt das Verhalten mehrdeutig.

- [x] CHK005 Ist die Terminologie für `TListBox` fachlich einheitlich, also „bestätigt Auswahl“ statt missverständlicher Mischformen wie „aktiviert“, wenn damit kein zusätzliches Command gemeint ist? [Ambiguity, Conflict, Spec §User Story 3, Plan §Implementation Sequence]

  > **Durchführungshinweis**: Volltextsuche in `specs/003-dialog-control-layer/` nach `aktiviert`, `Aktivierung`, `Doppelklick`, `Command`, `cmOK` durchführen. Vorkommen sind nur dann unkritisch, wenn sie eindeutig zu Buttons oder allgemeiner Command-Sprache gehören; für `TListBox` darf kein implizites Aktivierungs-Command suggeriert werden.

- [x] CHK006 Sind die Command-ID-Begriffe klar genug rückgebunden, sodass `cmCancel` als gemeinsame Command-ID und nicht als lokaler Sonderwert des Dialogs verstanden wird? [Clarity, Assumption, Research §R-005/R-011, Plan §CommandIDs, Contract §Predefined Command IDs]

  > **Durchführungshinweis**: [../research.md](../research.md), [../plan.md](../plan.md) und [../contracts/public-api.md](../contracts/public-api.md) prüfen. Es muss sichtbar sein, dass `cmCancel` zur gemeinsamen Command-ID-Familie gehört (`cmOK`, `cmCancel`, `cmYes`, `cmNo`) und nicht als impliziter Magic Value entsteht.

## Requirement Consistency

- [x] CHK007 Stimmen Spec, Research, Data Model, Contract und Plan für das Escape-Verhalten inhaltlich überein, ohne Widerspruch zwischen Rückgabewert, Ausnahmefall und Testableitung? [Consistency, Spec §FR-001, Research §R-011, Data Model §TDialog, Contract §TDialog, Plan §SC-004]

  > **Durchführungshinweis**: Die fünf Artefakte parallel lesen und folgende Fragen beantworten: Ist der Rückgabewert überall `cmCancel`? Ist der Ausnahmefall überall vorhanden? Ist die Testableitung im Plan konsistent dazu? Schon ein einzelner abweichender Wortlaut kann spätere Task-Fehlableitung verursachen.

- [x] CHK008 Stimmen Spec, Research, Data Model, Contract und Plan für die `TListBox`-Doppelklick-Semantik überein, ohne versteckte Owner-Benachrichtigung, Dialogschluss oder abweichende Nebenwirkung? [Consistency, Spec §FR-003, Research §R-012, Data Model §TListBox, Contract §TListBox, Plan §SC-004]

  > **Durchführungshinweis**: Auf Aussagen wie „sendet Command“, „schließt Dialog“, „aktiviert Eintrag“ oder „Owner-Event“ achten. Wenn ein Artefakt mehr als Auswahlbestätigung beschreibt, ist die Synchronisation unvollständig.

- [x] CHK009 Bleiben die Dokumente konsistent darin, dass dieses Feature keine JSON-, Persistenz- oder sonstige externe Austauschfläche einführt, obwohl mehrere Planartefakte erweitert wurden? [Consistency, Plan §Technical Context, Research §R-006, Constitution Alignment]

  > **Durchführungshinweis**: [../plan.md](../plan.md), [../research.md](../research.md), [../data-model.md](../data-model.md) und [constitution-alignment.md](constitution-alignment.md) querprüfen. Erweiterte Dokumentation darf keine implizite neue Serialisierungs- oder Austauschverantwortung in Phase 5 einführen.

## Acceptance Criteria Quality

- [x] CHK010 Ist die Prüfmethode für SC-004 im Plan objektiv genug, um Tab-Wrap, Default-Button, Escape → `cmCancel` und `TListBox`-Doppelklick ohne separates Command klar voneinander zu unterscheiden? [Measurability, Plan §Acceptance Criteria]

  > **Durchführungshinweis**: In [../plan.md](../plan.md) die Zeile zu `SC-004` lesen. Erwartet ist eine Formulierung, die die vier Verhaltensgruppen konkret benennt. Wenn mehrere Regeln in einer unpräzisen Sammelformulierung verschwimmen, ist die Abnahme nicht sauber messbar.

- [x] CHK011 Leiten Plan und Quickstart aus den geklärten Verhaltensregeln nachvollziehbare Testabsichten ab, ohne schon konkrete Implementierungslogik zu diktieren? [Traceability, Plan §Implementation Sequence, Quickstart §Build & Tests]

  > **Durchführungshinweis**: [../plan.md](../plan.md) und [../quickstart.md](../quickstart.md) darauf prüfen, ob die neuen Verhaltensfälle als erkennbare Testgegenstände benannt sind. Beispiel-Testnamen sind zulässig; verbotene Übersteuerung wäre eine detaillierte Vorwegnahme interner Event-Dispatch-Implementierung.

## Scenario Coverage

- [x] CHK012 Sind die zentralen Grenz- und Ausnahmefälle für `TDialog` und `TListBox` nicht nur in `Edge Cases`, sondern auch in Design-/Planartefakten wiederaufgenommen oder bewusst abgegrenzt? [Coverage, Spec §Edge Cases, Research, Data Model, Plan]

  > **Durchführungshinweis**: Die Edge Cases in [../spec.md](../spec.md) mit [../research.md](../research.md), [../data-model.md](../data-model.md) und [../plan.md](../plan.md) abgleichen. Besonders kritisch sind: kein fokussierbares Control, leere `TListBox`, Scrollbar-Grenzen, Escape im modalen Dialog. Wenn Randfälle nur als lose Stichworte in der Spec stehen, fehlt Design-Reife.

- [x] CHK013 Ist klar abgegrenzt, welche Verhaltensfragen bewusst nicht Teil dieses Features sind, damit `/speckit.tasks` keine Aufgaben für nicht spezifizierte Nebenwirkungen erzeugt? [Coverage, Assumption, Spec §Assumptions, Plan §Phase 1]

  > **Durchführungshinweis**: [../spec.md](../spec.md) und [../plan.md](../plan.md) prüfen, ob außerhalb des Scopes liegende Erweiterungen wie zusätzliche Listen-Command-APIs, Dialogschluss durch Listen-Doppelklick oder Datei-/Dialog-Spezialfälle explizit ausgeschlossen oder erkennbar nicht beschrieben sind.

## Dependencies & Assumptions

- [x] CHK014 Sind alle Abhängigkeiten, die für die geklärten Verhaltensregeln nötig sind, ausreichend dokumentiert, insbesondere `ShellCommandIds.cs`, `TGroup`-Navigation und das bestehende `TEvent`-Modell? [Dependency, Plan §Project Structure, Research §R-003/R-005, Spec §FR-010]

  > **Durchführungshinweis**: [../plan.md](../plan.md), [../research.md](../research.md) und [../spec.md](../spec.md) darauf prüfen, ob die geklärten Regeln auf bestehende Mechanismen zurückgeführt werden. Wenn Escape, Wrap-around oder Command-IDs ohne Bezug zu bestehenden Basisklassen beschrieben wären, müssten Tasks später Architektur erfinden statt ableiten.

## Ambiguities & Conflicts

- [x] CHK015 Bleiben nach der Clarify- und Plan-Synchronisation keine hochwirksamen Restambiguitäten zurück, die die Task-Erzeugung für Dialogschluss, Listen-Selektion oder Command-Routing noch verfälschen könnten? [Ambiguity, Conflict, Gap]

  > **Durchführungshinweis**: Abschließenden Querlesedurchgang über [../spec.md](../spec.md), [../plan.md](../plan.md), [../research.md](../research.md), [../data-model.md](../data-model.md), [../contracts/public-api.md](../contracts/public-api.md) und [../quickstart.md](../quickstart.md) machen. Suche nach offenen Begriffen wie „aktiviert“, „standardmäßig“, „bestätigt“, „Command“, „schließt“ und prüfe, ob sie überall gleich interpretiert werden können. Wenn dabei noch eine alternative Lesart plausibel bleibt, ist die Unterlage noch nicht task-reif.

## Notes

- Diese Checkliste ist als strenges Gate vor `/speckit.tasks` ausgelegt.
- Sie ergänzt die bestehenden Listen `requirements.md`, `constitution-alignment.md` und `plan-sync.md`, ersetzt sie aber nicht.
