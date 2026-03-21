# Plan Synchronization Checklist: Dialog-/Control-Schicht

**Purpose**: Validate that the updated implementation plan and its linked design artifacts consistently reflect the latest spec clarifications for `cmCancel` and `TListBox` double-click behavior.
**Created**: 2026-03-21
**Updated**: 2026-03-21 (Prüfpunkte durchgeführt und Planartefakte nachgezogen)
**Feature**: [../plan.md](../plan.md)

---

## Requirement Completeness

- [x] CHK001 Sind beide neuen Spec-Klärungen im Plan vollständig sichtbar abgeleitet: Escape → `cmCancel` und `TListBox`-Doppelklick → Auswahlbestätigung ohne separates zusätzliches Command-Ereignis? [Completeness, Plan §Summary, Plan §Phase 0, Plan §Phase 1]

  > **Durchführungshinweis**: [../plan.md](../plan.md) in `## Summary`, `## Phase 0: Research` und `## Phase 1: Design & Contracts` querlesen. Beide Verhaltensregeln müssen dort ausdrücklich genannt sein. Wenn eine Regel nur in der Spec steht, aber im Plan nicht mehr auftaucht, ist die Plan-Synchronisation unvollständig.

- [x] CHK002 Ist die Escape-Regel mit ihrer Bedingung vollständig beschrieben, also nur als Default-Verhalten, sofern kein Kind-Control das Ereignis vorher konsumiert? [Clarity, Spec §FR-001, Research §R-011, Contract §TDialog]

  > **Durchführungshinweis**: [../spec.md](../spec.md), [../research.md](../research.md) und [../contracts/public-api.md](../contracts/public-api.md) auf dieselbe Formulierung prüfen. Gesucht wird nicht nur "`cmCancel`", sondern auch die Einschränkung, dass ein Child-Control Escape vorher konsumieren kann. Fehlt diese Bedingung in einem Artefakt, bleibt das Verhalten mehrdeutig.

## Requirement Consistency

- [x] CHK003 Beschreiben Plan, Datenmodell und Vertrag die `TListBox`-Doppelklick-Semantik widerspruchsfrei als Auswahlbestätigung ohne separates zusätzliches Command? [Consistency, Plan §Implementation Sequence, Data Model §TListBox, Contract §TListBox]

  > **Durchführungshinweis**: [../plan.md](../plan.md), [../data-model.md](../data-model.md) und [../contracts/public-api.md](../contracts/public-api.md) nebeneinander prüfen. Warnsignale sind Wörter wie „aktiviert“, „sendet Command“, „Owner-Event“ oder implizite Dialog-Schließlogik. Alle drei Dokumente müssen dieselbe semantische Aussage tragen.

- [x] CHK004 Bleibt die Terminologie für `TListBox` über alle Artefakte konsistent, also „bestätigt Auswahl“ statt uneinheitlicher Mischformen wie „aktiviert“, wenn damit eigentlich kein separates Command gemeint ist? [Ambiguity, Conflict]

  > **Durchführungshinweis**: Volltextsuche in `specs/003-dialog-control-layer/` nach `aktiv`, `aktiviert`, `Doppelklick`, `Command`, `cmOK` durchführen. Wenn „aktiviert“ noch vorkommt, prüfen, ob damit wirklich nur Auswahlbestätigung gemeint ist oder ob es ein abweichendes Command-Verständnis nahelegt.

## Acceptance Criteria Quality

- [x] CHK005 Ist die Prüfmethode für SC-004 im Plan präzise genug, um die beiden geklärten Verhaltensfälle objektiv voneinander zu unterscheiden? [Measurability, Plan §Acceptance Criteria]

  > **Durchführungshinweis**: In [../plan.md](../plan.md) die Zeile zu `SC-004` lesen. Erwartet ist, dass dort Escape explizit mit `cmCancel` und `TListBox`-Doppelklick explizit ohne separates Command beschrieben ist. Wenn nur allgemein „Tastatur-/Mausnavigation korrekt“ steht, bleibt die Abnahme zu vage.

- [x] CHK006 Leiten Plan und Quickstart aus den neuen Klärungen konkrete, nachvollziehbare Testabsichten ab, ohne schon Implementierungsdetails vorzuschreiben? [Traceability, Plan §Implementation Sequence, Quickstart §Build & Tests]

  > **Durchführungshinweis**: [../plan.md](../plan.md) und [../quickstart.md](../quickstart.md) darauf prüfen, ob die neuen Verhaltensfälle als benennbare Testgegenstände auftauchen. Die Dokumente dürfen Beispiel-Testnamen oder Prüffälle nennen, aber keine konkrete Implementierung des Event-Handlings vorwegnehmen.

## Scenario Coverage

- [x] CHK007 Decken die Planartefakte sowohl den Primärfall als auch den relevanten Grenzfall der Escape-Regel ab: Standard-Schließen mit `cmCancel` und Ausnahme, wenn ein Kind-Control Escape konsumiert? [Coverage, Gap]

  > **Durchführungshinweis**: In [../plan.md](../plan.md), [../research.md](../research.md) und [../contracts/public-api.md](../contracts/public-api.md) nachsehen, ob neben dem Primärfall auch die Ausnahmeklausel dokumentiert ist. Wenn nur der Happy Path notiert ist, fehlt ein zentraler Abgrenzungsfall für spätere Tests.

- [x] CHK008 Ist für `TListBox` klar genug beschrieben, was Doppelklick fachlich bedeutet, ohne offene Restfrage, ob zusätzlich Dialogschluss, Owner-Benachrichtigung oder nur interne Auswahländerung gemeint ist? [Coverage, Clarity]

  > **Durchführungshinweis**: [../spec.md](../spec.md), [../plan.md](../plan.md), [../data-model.md](../data-model.md) und [../contracts/public-api.md](../contracts/public-api.md) auf versteckte Restdeutungen prüfen. Wenn irgendwo weiterhin offenbleibt, ob ein Doppelklick mehr als Auswahlbestätigung bewirkt, ist die Spezifikation noch nicht vollständig synchronisiert.

## Dependencies & Assumptions

- [x] CHK009 Ist `cmCancel` in den Planartefakten sauber an die vorgesehenen gemeinsamen Command-IDs rückgebunden und nicht als lokaler Sonderwert des Dialogs beschrieben? [Consistency, Assumption, Plan §CommandIDs]

  > **Durchführungshinweis**: In [../plan.md](../plan.md) und [../research.md](../research.md) prüfen, ob `cmCancel` auf `ShellCommandIds.cs` bzw. die gemeinsamen Command-IDs verweist. Wenn Escape nur als „Cancel“ beschrieben ist, aber nicht zur gemeinsamen Command-ID-Familie gehört, fehlt Traceability.

## Notes

- Diese Checkliste prüft die Qualität der aktualisierten Planartefakte nach der Clarify-Session, nicht die Implementierung.
- Fokus dieser Liste sind nur die neuen Änderungen: `cmCancel` als Escape-Standardwert und die präzisierte `TListBox`-Doppelklick-Semantik.
