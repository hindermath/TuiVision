# Planning Checklist: Application Framework Shell

**Purpose**: Validate the quality, completeness, clarity, and consistency of `plan.md` and its related planning artifacts before task generation or implementation
**Created**: 2026-03-20
**Feature**: [/Users/thorstenhindermann/RiderProjects/TuiVision/specs/002-application-framework/spec.md](../spec.md)

**Note**: This checklist evaluates the written planning artifacts as requirements-and-design documents. It does not verify implementation behavior.

## Requirement Completeness

- [x] CHK001 - Are all shell-scope deliverables from the specification represented in the planning set, including `TProgram`, `TApplication`, `TDesktop`, `TMenuBar`, and `TStatusLine`? [Completeness, Spec §FR-001, §FR-001a, §FR-011, Plan §Phase 1 Design Overview]
  Durchführungshinweis: Vergleiche die genannten Shell-Typen aus `spec.md` direkt mit `plan.md`, `contracts/application-shell-api.md` und dem geplanten Dateibaum. Markiere den Punkt nur als erfüllt, wenn jeder Scope-Baustein in allen relevanten Plan-Artefakten auftaucht oder bewusst begrenzt ist.
- [x] CHK002 - Does the plan explicitly define whether lightweight menu/status action models and shared shell command identifiers are required artifacts or only provisional examples? [Clarity, Plan §Project Structure, §Implementation Strategy]
  Durchführungshinweis: Prüfe, ob `TMenuItem`, `TStatusItem` und `ShellCommandIds` im Plan als verbindliche Artefakte beschrieben sind oder nur als Platzhalter im Strukturbaum erscheinen. Falls der Status nicht eindeutig ist, als offene Klarstellung markieren.
- [x] CHK003 - Are all intended output artifacts for this planning phase documented and internally consistent across `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/application-shell-api.md`? [Completeness, Plan §Project Structure, Research, Data Model, Quickstart, Contract]
  Durchführungshinweis: Gehe Datei für Datei durch und prüfe, ob jede im Plan angekündigte Datei vorhanden ist und inhaltlich zur gleichen Feature-Version gehört. Achte besonders darauf, dass Begriffe, Scope und Rollenbilder nicht zwischen den Dokumenten wechseln.
- [x] CHK004 - Does the planning set state whether shell layout responsibilities belong entirely to `TProgram`/`TApplication` or are shared with the planned view types in a way that is sufficiently documented? [Completeness, Plan §Phase 1 Design Overview, Contract §Public Surface Contract]
  Durchführungshinweis: Lies die Rollenbeschreibungen von `TProgram`, `TApplication`, `TDesktop`, `TMenuBar` und `TStatusLine` nebeneinander. Prüfe, ob Layout-Verantwortung, Komposition und Eigentümerschaft dokumentiert oder noch implizit gelassen sind.

## Requirement Clarity

- [x] CHK005 - Is the distinction between “shell coordinator”, “default shell creator”, and “desktop host” described clearly enough to prevent overlapping responsibilities between `TProgram`, `TApplication`, and `TDesktop`? [Clarity, Plan §Phase 1 Design Overview, Contract §TProgram, §TApplication, §TDesktop]
  Durchführungshinweis: Suche nach aktiven Verben wie „coordinates“, „creates“, „hosts“, „routes“ und ordne sie den drei Typen zu. Wenn zwei Typen dieselbe Kernverantwortung ohne Abgrenzung beanspruchen, gilt der Punkt als nicht erfüllt.
- [x] CHK006 - Are terms such as “controlled shutdown”, “valid focus target”, and “effectively immediate” defined with enough specificity to support task writing without reinterpretation? [Ambiguity, Plan §Technical Context, §Phase 1 Design Overview, Data Model §Shell Lifecycle, §Desktop Focus]
  Durchführungshinweis: Markiere alle qualitativen Begriffe ohne direkt messbaren oder beobachtbaren Bezug. Prüfe anschließend, ob Datenmodell, Contract oder Quickstart diese Begriffe konkretisieren; falls nicht, als Ambiguität notieren.
- [x] CHK007 - Is the phrase “customize or replace those regions afterward” constrained clearly enough to show what customization is expected in this increment versus what belongs to later increments? [Clarity, Spec §FR-001a, Contract §TApplication, Plan §Summary]
  Durchführungshinweis: Vergleiche die Spezifikationsformulierung mit dem Quickstart-Beispiel und dem Contract zu `TApplication`. Beurteile, ob erkennbar ist, welche Anpassungspunkte geplant sind und welche nicht in API-Verpflichtungen ausufern sollen.
- [x] CHK008 - Is the boundary between “behavioral contract” and “internal implementation freedom” documented clearly enough to avoid under-specifying the future public API? [Clarity, Research §Decision 7, Contract §Purpose]
  Durchführungshinweis: Prüfe, ob der Contract klare Verhaltensgarantien enthält, ohne intern zu detailliert zu werden, und ob der Plan trotzdem genug Orientierung für `tasks.md` liefern würde. Wenn Reviewer daraus keine belastbaren Aufgaben ableiten könnten, Punkt offen lassen.

## Requirement Consistency

- [x] CHK009 - Do the scope boundaries stay consistent across the spec, plan, contract, and quickstart regarding the exclusion of dialogs, controls, and specialized window types? [Consistency, Spec §FR-011, §Assumptions, Plan §Note, Contract §Behavioral Guarantees, Quickstart §Expected Outcomes]
  Durchführungshinweis: Suche in allen Artefakten nach Begriffen wie `dialog`, `control`, `widget`, `window type`. Prüfe, ob diese überall als out of scope behandelt werden und nirgends versehentlich als Lieferumfang erscheinen.
- [x] CHK010 - Are the command-routing expectations aligned across the spec, research decisions, data model, and contract so that menu, status line, and keyboard entry points all rely on the same conceptual command path? [Consistency, Spec §FR-004, Research §Decision 3, Data Model §CommandBinding, Contract §Behavioral Guarantees]
  Durchführungshinweis: Lies die Beschreibungen zu Command-Routing in Spec, Research, Data Model und Contract hintereinander. Achte darauf, ob überall dieselbe konzeptionelle Route beschrieben wird oder ob einzelne Dokumente noch alternative Pfade implizieren.
- [x] CHK011 - Is the stated TDD-first approach consistent between the constitution check, research decisions, implementation strategy, and quickstart flow? [Consistency, Plan §Constitution Check, §Implementation Strategy, Research §Decision 6, Quickstart §Planned Validation Flow]
  Durchführungshinweis: Kontrolliere, ob überall dieselbe Reihenfolge gilt: erst fehlschlagende Tests, dann minimale Implementierung, dann Refactoring. Wenn ein Dokument eine spätere Testergänzung oder eine parallele Entstehung nahelegt, als Inkonsistenz markieren.
- [x] CHK012 - Do the planned source-file locations and test-file locations align with the documented module boundaries and the “no new assembly” decision? [Consistency, Plan §Project Structure, §Structure Decision, Research §Decision 1]
  Durchführungshinweis: Vergleiche den Strukturbaum im Plan mit den Modulregeln aus dem Constitution Check. Prüfe, ob alle geplanten Typen in `src/TuiVision.Controls` und alle zugehörigen Tests in `tests/TuiVision.Controls.Tests` landen sollen.

## Acceptance Criteria Quality

- [x] CHK013 - Are the planned validation commands and shell-level integration expectations connected clearly enough to the measurable success criteria from the specification? [Traceability, Spec §SC-001, §SC-004, Plan §Testing Strategy, Quickstart §Expected Outcomes]
  Durchführungshinweis: Ordne jede relevante Success Criterion mindestens einem Plan-Abschnitt oder Validierungsbefehl zu. Wenn aus dem Plan nicht erkennbar wird, wie ein Kriterium überprüfbar vorbereitet wird, den Punkt offen halten.
- [x] CHK014 - Does the planning set define how reviewers can tell that focus recovery, disabled-action visibility, and shared-command behavior have been specified sufficiently before tasks are written? [Measurability, Spec §FR-006, §FR-009a, Plan §Testing Strategy, Contract §Behavioral Guarantees]
  Durchführungshinweis: Prüfe, ob zu den drei Kernverhalten klare Prüffragen, Garantien oder Testverpflichtungen dokumentiert sind. Fehlt für einen Bereich ein beobachtbarer Maßstab, ist der Punkt nicht erfüllt.

## Scenario Coverage

- [x] CHK015 - Are primary, alternate, and recovery scenarios all represented across the planning artifacts, including startup with an empty desktop, shared-command invocation, and active-child removal? [Coverage, Spec §User Story 1-3, Data Model §Desktop Focus, Quickstart §Planned Validation Flow]
  Durchführungshinweis: Lege eine kleine Matrix mit Primär-, Alternativ- und Recovery-Szenarien an und hake je Artefakt ab, wo der Fall adressiert wird. Wenn eine Szenarioklasse nur in der Spec vorkommt, aber im Planset nicht mehr auftaucht, als Lücke markieren.
- [x] CHK016 - Are reviewer-facing planning requirements documented for both author customization scenarios and end-user interaction scenarios, rather than only one perspective? [Coverage, Spec §User Story 1, §User Story 2, §User Story 3, Plan §Summary]
  Durchführungshinweis: Prüfe, ob sowohl Framework-Konsumenten als auch Endnutzer im Plan sichtbar bleiben. Wenn die Planung nur technische Autorenperspektive oder nur Interaktionsperspektive enthält, ist die Abdeckung unvollständig.

## Edge Case Coverage

- [x] CHK017 - Does the planning set define where constrained screen area, zero-child desktop state, and disabled-command representation are addressed, or are these edge conditions still under-specified? [Coverage, Edge Case, Spec §Edge Cases, Plan §Technical Context, Data Model §Command Availability]
  Durchführungshinweis: Suche die drei Edge Cases gezielt in Plan, Datenmodell, Quickstart und Contract. Markiere den Punkt nur als erfüllt, wenn jeder Fall zumindest einem Planbaustein oder einer Review-Verpflichtung zugeordnet werden kann.
- [x] CHK018 - Is the focus-recovery path after closing the active desktop child documented with enough detail to distinguish fallback-to-next-child from fallback-to-desktop behavior? [Clarity, Edge Case, Spec §Edge Cases, Data Model §Desktop Focus, Contract §Focus recovery guarantee]
  Durchführungshinweis: Lies die Focus-Passagen so, als müsstest du daraus Aufgaben formulieren. Wenn nicht erkennbar ist, wann auf ein anderes Kind und wann auf den Desktop zurückgefallen wird, bleibt der Punkt offen.

## Non-Functional Requirements

- [x] CHK019 - Are non-functional requirements for portability, documentation completeness, and test discipline translated from the constitution check into actionable planning constraints without relying on implicit project knowledge? [Non-Functional, Plan §Constitution Check, §Technical Context]
  Durchführungshinweis: Vergleiche die Constitution-Gates mit den konkreten Plan-Constraints. Prüfe, ob ein Reviewer ohne Zusatzwissen erkennen kann, welche verbindlichen Qualitätsanforderungen in Tasks und Implementierung mitzunehmen sind.
- [x] CHK020 - Is the performance expectation for the first interactive shell frame specific enough to guide implementation trade-offs, or does it still depend on subjective interpretation? [Ambiguity, Non-Functional, Plan §Technical Context, Spec §SC-001]
  Durchführungshinweis: Untersuche, ob Begriffe wie „first interactive shell frame“ und „without extra setup steps“ in beobachtbare Anforderungen übersetzbar sind. Wenn das nur gefühlt statt objektiv beurteilbar ist, als Ambiguität markieren.

## Dependencies & Assumptions

- [x] CHK021 - Are assumptions about the sufficiency of existing `TView`/`TGroup` semantics documented strongly enough to justify reuse without additional research tasks? [Assumption, Plan §Summary, Research §Decision 2]
  Durchführungshinweis: Prüfe, ob die Wiederverwendung bestehender Semantik begründet und nicht nur behauptet wird. Wenn Risiken oder Grenzen der Wiederverwendung gar nicht angesprochen werden, Punkt als nur teilweise erfüllt behandeln.
- [x] CHK022 - Does the planning set document all dependencies on current repository abstractions, especially the relationship between `TuiVision.Controls` and `TuiVision.Drivers.Console`, without implying forbidden coupling? [Dependency, Plan §Technical Context, §Constitution Check, Research §Decision 1]
  Durchführungshinweis: Lies alle Stellen zu `TuiVision.Controls` und `TuiVision.Drivers.Console` im Zusammenhang. Achte darauf, dass Zusammenarbeit beschrieben ist, aber keine direkte Verantwortungsvermischung oder neue Kopplung vorgeschlagen wird.
- [x] CHK023 - Is the absence of persistent storage and the in-memory-only model documented consistently enough that later tasks will not introduce unintended persistence responsibilities? [Consistency, Assumption, Plan §Technical Context, Data Model §Overview]
  Durchführungshinweis: Vergleiche den Abschnitt `Storage: N/A` mit `data-model.md` und Contract. Wenn irgendwo implizit Zustandsablage außerhalb des Laufzeitmodells anklingt, ist der Punkt nicht erfüllt.

## Ambiguities & Conflicts

- [x] CHK024 - Does the planning set resolve whether the quickstart’s example customization hooks are illustrative only or part of the committed public API surface? [Ambiguity, Quickstart §Representative Usage Sketch, Contract §TApplication, Plan §Phase 1 Design Overview]
  Durchführungshinweis: Prüfe, ob das Quickstart-Beispiel normative Wirkung entfaltet oder nur eine mögliche API-Idee zeigt. Wenn Reviewer daraus fälschlich verpflichtende Methoden ableiten könnten, als Ambiguität markieren.
- [x] CHK025 - Is there any conflict between the contract’s narrow behavioral guarantees and the plan’s proposed file list that could cause premature API surface expansion? [Conflict, Contract §Behavioral Guarantees, Plan §Project Structure, Research §Decision 7]
  Durchführungshinweis: Vergleiche die schmale Contract-Sprache mit der relativ konkreten Dateiliste im Plan. Wenn die Dateiliste schon mehr öffentliche API suggeriert als der Contract absichert, notiere einen möglichen Zielkonflikt.
- [x] CHK026 - Does the planning set establish a sufficiently explicit traceability path from spec requirements to plan sections and related artifacts, or is an additional ID/linking scheme still needed? [Traceability, Gap, Spec §FR-001-§FR-011, Plan, Research, Data Model, Contract]
  Durchführungshinweis: Versuche stichprobenartig mehrere `FR-*`-Punkte aus der Spec den Plan-Artefakten zuzuordnen. Wenn diese Zuordnung nur mit viel Interpretationsleistung gelingt, ist zusätzliche Traceability wahrscheinlich nötig.

## Notes

- Use this checklist during author review or PR review before `/speckit.tasks`.
- Items marked with `[Gap]`, `[Ambiguity]`, `[Conflict]`, `[Assumption]`, or `[Dependency]` indicate likely refinement targets in the planning artifacts.
- This checklist complements `checklists/requirements.md`; it does not replace the specification-quality checklist.
- 2026-03-20 review pass: all items were rechecked against `plan.md`, `research.md`, `data-model.md`, `quickstart.md`, and `contracts/application-shell-api.md`; open gaps were resolved in those artifacts before items were marked complete.
