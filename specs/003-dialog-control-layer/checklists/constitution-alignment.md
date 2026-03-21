# Constitution Alignment Checklist: Dialog-/Control-Schicht

**Purpose**: Validate that the implementation plan and plan-linked artifacts remain aligned with the amended constitution after the `System.Text.Json` technology mandate.
**Created**: 2026-03-21
**Updated**: 2026-03-21 (Durchführungshinweise geprüft und erfüllt)
**Feature**: [../plan.md](../plan.md)

---

## Plan Completeness

- [x] CHK001 Ist im technischen Kontext des Plans explizit festgehalten, dass für dieses Feature derzeit kein JSON-Format vorgesehen ist oder bei späterem Bedarf `System.Text.Json` verbindlich wäre? [Completeness, Plan §Technical Context]

  > **Durchführungshinweis**: Abschnitt `## Technical Context` in [../plan.md](../plan.md) lesen. Dort muss entweder "kein JSON im Scope" erkennbar sein oder eine explizite Bindung an `System.Text.Json` für spätere JSON-Hilfsformate stehen. Fehlt beides, bleibt die Constitution-Änderung im Plan unsichtbar.

- [x] CHK002 Ist im Constitution-Check oder einer gleichwertigen Nachprüfung des Plans dokumentiert, warum die neue JSON-Vorgabe für dieses Feature erfüllt ist? [Consistency, Plan §Constitution Check]

  > **Durchführungshinweis**: In [../plan.md](../plan.md) den Abschnitt `## Constitution Check` und den `Post-Design Re-check` prüfen. Erwartet ist eine kurze, widerspruchsfreie Aussage wie "keine JSON-Schnittstelle im Scope" oder "bei JSON-Bedarf nur System.Text.Json". Wenn die Constitution ergänzt wurde, der Plan aber weiterhin so wirkt, als gäbe es dazu keine Vorgabe, ist die Synchronisation unvollständig.

## Quickstart & Review Procedure

- [x] CHK003 Definiert die Quickstart-Datei einen prüfbaren Review-Schritt, mit dem eine unbeabsichtigte Einführung von `Newtonsoft.Json` erkannt werden kann? [Measurability, Quickstart §Quality Gates]

  > **Durchführungshinweis**: In [../quickstart.md](../quickstart.md) unter `## Quality Gates vor Merge` nach einem konkreten Such- oder Prüfkommando suchen. Ein bloßer Hinweis "aufpassen" reicht nicht; es muss eine nachvollziehbare Durchführung angegeben sein, zum Beispiel per `rg`-Suche.

- [x] CHK004 Ist in der Quickstart-Datei klar beschrieben, unter welcher Bedingung eine Ausnahme für `Newtonsoft.Json` zulässig wäre? [Clarity, Quickstart §Quality Gates]

  > **Durchführungshinweis**: Prüfen, ob [../quickstart.md](../quickstart.md) die Ausnahmebedingung an dokumentierte Begründung im Plan oder PR koppelt. Fehlt diese Bedingung, könnten Reviewer die Governance-Regel unterschiedlich auslegen.

## Cross-Artifact Consistency

- [x] CHK005 Bleiben `research.md`, `data-model.md` und `contracts/public-api.md` konsistent zur Aussage, dass Phase 5 keine JSON-Schnittstelle, Persistenz oder JSON-Vertragsfläche einführt? [Consistency, Research §R-006, Data Model §TStringList, Contract Scope]

  > **Durchführungshinweis**: [../research.md](../research.md), [../data-model.md](../data-model.md) und [../contracts/public-api.md](../contracts/public-api.md) querlesen. Gesucht werden Begriffe wie `JSON`, `serialize`, `deserialize`, `persist`, `import`, `export`. Wenn solche Begriffe auftauchen, muss der Plan erklären, ob sie wirklich zum Feature gehören und welche JSON-Bibliothek dann gilt.

- [x] CHK006 Ist klar ausgeschlossen oder dokumentiert, dass featurebezogene Testdaten, Helper-Skripte oder Begleitformate keine verdeckte JSON-Abhängigkeit einführen? [Coverage, Gap]

  > **Durchführungshinweis**: Nicht nur Produktionscode, sondern auch geplante Testdaten und Begleitdateien im Feature betrachten. Wenn zum Beispiel neue JSON-Files, Snapshot-Converter oder Tooling-Skripte erwähnt werden, muss der Plan ihre Bibliothekswahl und den Scope dazu explizit festhalten.

## Ambiguities & Future Changes

- [x] CHK007 Ist geregelt, wie vorzugehen ist, wenn im Verlauf der Implementierung doch JSON-basierte Test- oder Austauschformate nötig werden? [Gap, Ambiguity]

  > **Durchführungshinweis**: Prüfen, ob der Plan oder die Quickstart-Anleitung eine Eskalationsregel enthält: Plan/PR ergänzen, `System.Text.Json` verwenden, Ausnahme begründen. Ohne diese Regel entsteht Rework-Risiko, sobald während der Umsetzung ein JSON-Hilfsformat auftaucht.

- [x] CHK008 Sind die Planartefakte frei von Formulierungen, die implizit eine beliebige JSON-Bibliothek erlauben oder die Technology-Mandate der Constitution aushebeln? [Conflict, Assumption]

  > **Durchführungshinweis**: In allen Dateien unter `specs/003-dialog-control-layer/` nach offenen Formulierungen wie "JSON export", "serialisiert als JSON" oder generischen Package-Hinweisen suchen. Solche Aussagen sind nur zulässig, wenn sie die Constitution-Regel nicht offenlassen oder ihr ausdrücklich folgen.

## Notes

- Diese Checkliste prüft die Qualität und Vollständigkeit der Planartefakte, nicht die Implementierung.
- Der Fokus liegt auf der neuen Constitution-Vorgabe zu `System.Text.Json` und ihrer sichtbaren Ableitung in den Feature-Unterlagen.
