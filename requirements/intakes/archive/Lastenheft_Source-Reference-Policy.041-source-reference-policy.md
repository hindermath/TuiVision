<!-- intake-authoring:begin -->
# Lastenheft: Quellenreferenz-Policy / Requirements Intake: Source Reference Policy

**Status:** ReadyForReview

**Zielgruppe / Audience:** Maintainer, Reviewer, Spec-Kit-Autoren und Beitragende / maintainers, reviewers, Spec Kit authors, and contributors

**Vorausgesetztes Wissen / Assumed prior knowledge:** Grundverständnis von TuiVision und referenzierter Open-Source-Software; die Rollen werden hier vollständig erklärt. / Basic knowledge of TuiVision and referenced open-source software; the roles are explained here.
**Profil / Profile:** `level2-lastenheft`

## Zweck / Purpose

Diese Policy trennt aktuelle Produktsemantik, moderne Designreferenz und
historische Absicht. Sie macht `magiblot/tvision` zur zuerst geprüften modernen
Designreferenz, ohne daraus eine alleinige Normquelle oder eine Pflicht zur
C++-Strukturübernahme zu machen. Bestehende TuiVision-Verträge bleiben bis zu
einer ausdrücklich genehmigten Änderung verbindlich.

*This policy separates current product semantics, modern design reference, and
historical intent. It makes `magiblot/tvision` the first modern design
reference without turning it into the sole authority or requiring C++
structure. Existing TuiVision contracts remain binding until an explicitly
approved change replaces them.*

## Ausgangslage / Current State

Die bestehende historische Quellenpolicy verlangt bei historisch abgeleiteten
Änderungen eine Prüfung von `tv203s/`. Feature 030 hat zusätzlich einen
read-only Evolutionsaudit gegen `magiblot/tvision` am Commit
`57b6f56b38e0ee75240a80a10ee0e11470c24693` durchgeführt. Die Rollen dieser
Quellen sind bislang jedoch nicht repositoryweit als ein gemeinsamer,
prospektiver Entscheidungsworkflow festgeschrieben.

*The current historical-source policy requires `tv203s/` review for
historically derived changes. Feature 030 also completed a read-only evolution
audit against `magiblot/tvision` commit
`57b6f56b38e0ee75240a80a10ee0e11470c24693`. Their roles are not yet defined
repository-wide as one prospective decision workflow.*

## Zielzustand / Target State

Constitution, Agent-Guidance, Pflichtenheft-/Requirements-Regeln und Spec-Kit-
Planungsanweisungen verwenden dieselbe Drei-Achsen-Policy. Jede materiell
historisch berührte Änderung kann ihre aktuelle Produktnorm, ihre moderne
Designreferenz, ihre historische Absicht und die daraus folgende Entscheidung
eindeutig nachweisen.

*The constitution, agent guidance, requirements rules, and Spec Kit planning
instructions use one three-axis policy. Every materially history-related
change can prove its current product contract, modern design reference,
historical intent, and resulting decision.*

## Verbindliche Quellenrollen / Binding source roles

1. Akzeptierte TuiVision-Anforderungen, Spezifikationen, Public Contracts und
   Tests bestimmen die aktuelle Produktsemantik.
2. Der freigegebene `magiblot/tvision`-Pin bestimmt keine Produktsemantik. Er
   wird zuerst für moderne Architektur-, Zerlegungs-, Plattform-, Unicode-,
   Buffer-, Event- und Treiberideen geprüft.
3. Borland-Dokumentation und `tv203s/` bestimmen historische Absicht,
   ursprüngliches Verhalten und dokumentationspflichtige Abweichungen.
4. Free Vision und Terminal.GUI bleiben unabhängige Vergleichsmeinungen.
5. `TVDEMOS/`, `TVFM/` und bestehende TuiVision-Beispiele bleiben
   Consumer-Evidence.

*Accepted TuiVision contracts define product semantics. The approved
`magiblot/tvision` pin is reviewed first for modern design ideas but is not
normative. Borland and `tv203s/` define historical intent. Free Vision and
Terminal.GUI remain independent comparisons, while consumers and examples
remain consumer evidence.*

## Anforderungen / Requirements

- **FR-001:** Die Policy MUSS die fünf Quellenrollen in allen ausgelösten
  Governance-Flächen konsistent und widerspruchsfrei festschreiben.
- **FR-002:** Für historisch materiell berührte Änderungen MUSS der Workflow in
  dieser Reihenfolge gelten: aktuellen TuiVision-Vertrag lesen; relevante
  Magiblot-Dateien am freigegebenen Pin prüfen; passende `tv203s`-Implementierung
  und erforderliche Header prüfen; zusätzliche Consumer oder unabhängige
  Implementierungen nur bei materieller Relevanz hinzuziehen; Entscheidung
  dokumentieren.
- **FR-003:** Jede Entscheidung MUSS genau eine Disposition verwenden:
  `AdoptModernization`, `PreserveHistoricalIntent`,
  `IntentionalTuiVisionDeviation` oder `N/A` mit Begründung.
- **FR-004:** Der zunächst freigegebene Magiblot-Stand MUSS Commit
  `57b6f56b38e0ee75240a80a10ee0e11470c24693` und den in Feature 030 gebundenen
  Tree verwenden. Ein beweglicher Branch DARF keine Review- oder
  Implementierungsevidence sein.
- **FR-005:** Ein neuer Magiblot-Pin MUSS einen getrennten read-only
  Provenienz- und Delta-Review durchlaufen. Der neue Pin wird erst nach
  ausdrücklicher Annahme wirksam.
- **FR-006:** Konflikte DÜRFEN nicht allein durch Quellenrang gelöst werden.
  Bestehende Produktverträge gelten bis zu ihrer genehmigten Änderung;
  Magiblot darf die Implementierungsform inspirieren, aber weder C++-
  Vererbung noch Speicherlayout oder Quelltextform erzwingen.
- **FR-007:** Eine materielle Abweichung von historischer Absicht MUSS in Spec,
  Plan, Tasks, Guide oder Evidence sichtbar begründet werden.
- **FR-008:** Externe Magiblot-Checkouts MÜSSEN außerhalb des verfolgten
  Repositorys bleiben. Gespeichert werden dürfen nur Pin, Tree, geprüfte
  Pfade, Hashes, kurze eigene Zusammenfassungen und Permalinks.
- **FR-009:** Der mehrteilige Lizenzstatus von `magiblot/tvision` DARF nicht
  pauschal als MIT bezeichnet werden. Copy, Vendorisierung, Übersetzung und
  abgeleitete Quelltextübernahme sind kein Bestandteil dieser Policy.
- **FR-010:** Die Policy MUSS prospektiv gelten. Abgeschlossene Features und
  Audits bleiben gültig und werden nicht rückwirkend wieder geöffnet.
- **FR-011:** Eine Re-Evaluation MUSS nur bei geändertem TuiVision-Vertrag,
  neuem freigegebenem Pin oder materiell neuer Consumer-Evidence ausgelöst
  werden.
- **FR-012:** Nicht historisch berührte Änderungen DÜRFEN `N/A` mit kurzer
  Begründung dokumentieren, ohne externe Quellen künstlich zu prüfen.
- **FR-013:** Bash- und PowerShell-Governanceprüfungen MÜSSEN die gleichen
  Policy-, Pin-, Prospektivitäts- und No-Copy-Invarianten bestätigen.

## Konflikt- und Entscheidungsmodell / Conflict and decision model

- `AdoptModernization`: Eine moderne Form wird übernommen, weil sie den
  akzeptierten TuiVision-Vertrag idiomatisch besser erfüllt.
- `PreserveHistoricalIntent`: Historische Nutzer- oder Verhaltensabsicht bleibt
  maßgeblich, auch wenn die moderne Referenz anders strukturiert ist.
- `IntentionalTuiVisionDeviation`: TuiVision weicht bewusst und genehmigt von
  moderner Referenz oder historischer Absicht ab.
- `N/A`: Es besteht kein materieller Bezug; Begründung und Re-Evaluation-
  Trigger bleiben sichtbar.

*The disposition records why modernization is adopted, historical intent is
preserved, a TuiVision-specific deviation is intentional, or the comparison is
not materially applicable.*

## Betroffene Governance-Flächen / Governance surfaces

- `.specify/memory/constitution.md` und davon abhängige Plan-Checks;
- `AGENTS.md` einschließlich Historical Source Reference Policy;
- `Pflichtenheft.md`, Requirements-Intake-Regeln und Reihenfolgedokumentation;
- Spec-Kit-Planungs-, Analyse- und Aufgabenanweisungen oder Templates, soweit
  sie Quellenprüfung und Evidence verlangen;
- bilinguale Maintainer-Dokumentation und maschinenlesbare
  Akzeptanz-/Konsistenz-Evidence.

*Affected surfaces are the constitution, agent guidance, requirements rules,
Spec Kit planning instructions, and bilingual, machine-reviewable evidence.*

## Nicht-Ziele / Non-Goals

- Keine rückwirkende Neubeurteilung abgeschlossener Features oder Audits.
- Keine Produkt-, Runtime-, Public-API-, Paket-, Dependency- oder
  Beispieländerung.
- Keine Vendorisierung oder Build-Abhängigkeit von `magiblot/tvision`.
- Keine Behauptung, dass moderne Referenz und historische Absicht immer
  übereinstimmen.
- Keine automatische Konfliktauflösung und keine Lizenzberatung.

## Qualität, Sicherheit und Accessibility / Quality, security, and accessibility

Die Policy arbeitet read-only mit externen Quellen, begrenzt gespeicherte
Provenienz und lehnt bewegliche oder unklare Pins fail-closed ab. Dokumente
sind deutsch zuerst und englisch danach, ungefähr CEFR-B2, text-first und ohne
farb- oder layoutabhängige Kernaussage. Beispiele und Pfade dürfen keine
Secrets, Zugangsdaten oder lokale Benutzerinformationen enthalten.

*External review remains read-only, persisted provenance is bounded, and
moving or unclear pins fail closed. Documentation is German-first,
English-second, CEFR-B2, text-first, and contains no secrets or user-specific
local information.*

## Abnahmekriterien / Acceptance Criteria

- **AC-001:** Alle ausgelösten Governance-Flächen nennen dieselben fünf Rollen
  und dieselbe Prüfungsreihenfolge.
- **AC-002:** Der exakte Commit und Feature-030-Tree sind konsistent; kein
  beweglicher Branch ist Evidence.
- **AC-003:** Die vier Dispositionen und ihre Konfliktregeln sind in Planungs-
  und Evidence-Flächen prüfbar.
- **AC-004:** Prospektive Gültigkeit und drei Re-Evaluation-Trigger sind
  ausdrücklich festgeschrieben; abgeschlossene Evidence bleibt unverändert.
- **AC-005:** No-Copy-, externer Checkout- und mehrteilige Lizenzgrenze sind
  vollständig dokumentiert.
- **AC-006:** Bash- und PowerShell-Validatoren liefern für positive und
  kontrollierte negative Fixtures dieselben Ergebnisse.
- **AC-007:** Release-Build, vollständige Tests, fünf Coverage-Schwellen,
  Format, DocFX, Playwright/Axe und Lynx bestehen, sofern ihre Flächen
  ausgelöst sind.
- **AC-008:** `src/`, `examples/`, Public API, Projekte, Pakete,
  Dependencies und historische Quellen haben kein fachliches Delta.

## Abhängigkeiten und Reihenfolge / Dependencies and order

Der unabhängige Example Portfolio Closure muss abgeschlossen sein. Die
Constitution-Änderung und diese Policy teilen Schreibflächen und werden daher
seriell bearbeitet (`SharedWriterSerialization`), ohne daraus eine fachliche
Produktabhängigkeit abzuleiten. Das Transactional Form Model darf erst nach
dieser Policy beginnen.

*Example Portfolio Closure must be complete. Constitution work and this policy
share writer surfaces and therefore run serially. The Transactional Form Model
starts only after this policy is accepted and delivered.*

## Annahmen und offene Fragen / Assumptions and open questions

Die Produktentscheidungen sind durch den genehmigten Plan vollständig. Es gibt
keine offene materielle Frage. Delivery Authority bleibt
`LocalImplementation`; Commit, Push, PR, Merge und Remote-Administration sind
nicht autorisiert.

<!-- intake-authoring:prompts -->
## Copy-Ready Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
### Specify

```text
$speckit-specify requirements/intakes/active/Lastenheft_Source-Reference-Policy.md. Binde exakt diesen reviewten Intake und die Feature-030-Provenienz. Erzeuge nur die Spezifikation; implementiere nichts und führe keine Remote-Schreibaktion aus.
```

<!-- spec-kit-command-id: speckit.autonomous -->
### Autonomous

```text
$speckit-autonomous requirements/intakes/active/Lastenheft_Source-Reference-Policy.md mit Delivery-Authority LocalImplementation. Liefere die prospektive Policy konsistent in allen ausgelösten Governance-Flächen. Commit, Push, PR, Merge, Bypass, Provider-Administration und Secret-Zugriff sind nicht autorisiert.
```

<!-- intake-authoring:end -->
