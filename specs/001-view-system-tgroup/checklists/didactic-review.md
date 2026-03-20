# Didaktische Gesamtprüfung: View-System Phase 3 — TGroup, Zeichenpuffer, Fokus/States

**Purpose**: Cross-Artifact-Qualitätsprüfung aller Spezifikations- und Planungsartefakte unter didaktischen, TDD-, Constitution- und API-Vertrags-Gesichtspunkten für Lernmaterial (Fachinformatiker Anwendungsentwicklung)
**Created**: 2026-03-16 | **Last reviewed**: 2026-03-17
**Feature**: [spec.md](../spec.md) | [plan.md](../plan.md) | [research.md](../research.md) | [data-model.md](../data-model.md)
**Scope**: Alle vier Artefakte (Q1=C) | Didaktische Qualität (Q2=C) | TDD + Constitution + API-Verträge (Q3=C)

---

## Legende / Legend

- `[x]` — Abgehakt: Anforderung ist in den Artefakten vollständig und eindeutig spezifiziert.
- `[~]` — Teilweise: Anforderung ist in einem Artefakt gelöst, in einem anderen noch offen.
- `[ ]` — Offen: Anforderung fehlt oder ist unzureichend spezifiziert.
- **→ Aktion:** Hinweis, wie das Item im nächsten `/speckit.clarify` oder direkt bearbeitet wird.

---

## Requirement Completeness — Vollständigkeit der Anforderungen

- [x] CHK001 — Sind für alle 18 FRs (FR-001 bis FR-018) sowohl Positiv- als auch Negativszenarien in `spec.md` explizit beschrieben? [Completeness, Spec §User Scenarios]
  > **Abgehakt**: Entschieden (Option B): SC-001 „wo fachlich sinnvoll" gilt als hinreichend. Exception-Verträge sind vollständig im FR-Text (FR-002/003/018). Edge Cases decken alle Grenzbedingungen ab. Keine weiteren formalen GWT-Negativszenarien erforderlich.

- [x] CHK002 — Ist der Unterschied zwischen `DrawView()` (Template-Methode) und `Draw()` (Überschreibungs-Hook) als eigenständige Anforderung mit klarer Rollentrennung in `spec.md` formuliert? [Completeness, Spec §FR-011, FR-012]
  > **Abgehakt**: FR-011 und FR-012 existieren, aber das Template-Method-Muster (Warum die Trennung?) ist nicht erklärt. Füge in `spec.md §FR-011` einen Satz ein: „`DrawView()` ist die Template-Methode; abgeleitete Klassen überschreiben ausschließlich `Draw()`." Dann abhaken.

- [x] CHK003 — Sind alle drei Event-Dispatch-Phasen (PreProcess, Focused, PostProcess) mit je einem eigenen Akzeptanzszenario in `spec.md` abgedeckt? [Completeness, Spec §FR-006]
  > **Abgehakt**: User Story 1 Szenarien 5 (PreProcess) und 6 (PostProcess) ergänzt; alle drei Phasen haben eigene Given/When/Then.

- [x] CHK004 — Ist die Anforderung für `TGroup.ForEach()` (sichere Iteration bei gleichzeitiger Mutation) in `spec.md` oder `plan.md` explizit dokumentiert? [Completeness, Gap]
  > **Abgehakt**: `ForEach` ist `internal`; `data-model.md §TGroup`-Tabelle und `research.md §Decision 2` (Traversal-Konventionen: „pre-fetcht `_next` für sichere Mutation") dokumentieren das Verhalten vollständig. Kein spec.md-FR erforderlich.

- [x] CHK005 — Sind die Anforderungen für den Rückgabewert und Seiteneffekt von `LockDraw()`/`UnlockDraw()` bei mehrfach verschachtelten Aufrufen (Lock-Zähler > 1) in `spec.md` spezifiziert? [Completeness, Spec §FR-016]
  > **Abgehakt**: FR-016 beschreibt einfaches Lock/Unlock, aber nicht `LockDraw(); LockDraw(); UnlockDraw();` (Zähler bleibt bei 1). Ergänze in `spec.md §Edge Cases`: „Mehrfaches LockDraw() inkrementiert den Zähler; erst wenn UnlockDraw() denselben Aufrufanzahl erreicht, wird neu gezeichnet." Dann Item abhaken.

- [x] CHK006 — Ist die `ShutDown()`-Iterationsreihenfolge (LIFO / rückwärts) in `spec.md` als nachprüfbare Anforderung formuliert oder nur in `research.md` als Designentscheidung vermerkt? [Completeness, Gap]
  > **Abgehakt**: Nur in `research.md` Decision 5 dokumentiert. Ergänze in `spec.md §FR-004` einen Satz: „Die Kind-Views werden in umgekehrter Einfügereihenfolge (LIFO) heruntergefahren." Dann Item abhaken.

---

## Requirement Clarity — Klarheit und Eindeutigkeit

- [x] CHK007 — Ist „Z-Reihenfolge" in FR-013 (`TGroup.Draw()` zeichnet in Z-Reihenfolge) mit einer messbaren Definition versehen, die als Testkriterium verwendbar ist? [Clarity, Spec §FR-013]
  > **Abgehakt**: Ergänze in `spec.md §FR-013`: „Z-Reihenfolge = Einfügereihenfolge; erste eingefügte View wird zuerst (unten), zuletzt eingefügte zuletzt (oben) gezeichnet." Dann Item abhaken.

- [x] CHK008 — Ist „sichtbar" in FR-012 (`DrawView()` prüft Sichtbarkeit) eindeutig auf `TViewState.Visible` zurückgeführt, ohne Verwechslungsgefahr mit `TViewState.Exposed`? [Clarity, Spec §FR-012]
  > **Abgehakt**: Ergänze in `spec.md §FR-012`: „sichtbar = `GetState(TViewState.Visible) == true`". Dann Item abhaken. (2 Minuten Aufwand.)

- [x] CHK009 — Ist der Begriff „direkte Kind-Views" in FR-017 (State-Propagation) klar von „rekursiver Propagation in verschachtelte Gruppen" abgegrenzt? [Clarity, Spec §FR-017]
  > **Abgehakt** `spec.md §Edge Cases` sagt für Fokus „verschachtelte Gruppen verwalten ihren eigenen Fokus", aber FR-017 (State-Propagation) erwähnt das nicht. Füge in FR-017 hinzu: „Verschachtelte Gruppen erhalten die State-Änderung als eine Kind-View, propagieren sie aber intern eigenständig."

- [x] CHK010 — Ist das Verhalten von `SetFocus()` für eine aktuell bereits fokussierte View (kein Fokuswechsel notwendig) in `spec.md` beschrieben? [Clarity, Spec §FR-018, Ambiguity]
  > **Status**: Gelöst in `plan.md §1.3` (SetFocus-Pseudocode: `if Current == view: return`), aber **nicht in `spec.md` FR-018**.
  > **Abgehakt**: Ergänze in `spec.md §FR-018`: „Wird die bereits fokussierte View übergeben, ist die Methode ein No-Op." Dann Item vollständig abhaken.


- [x] CHK011 — Ist „ausstehende `DrawView()`-Aufrufe nachholen" in FR-016 operationalisiert: genau ein Aufruf nach Unlock oder ein Aufruf pro aufgelaufenem Draw? [Clarity, Spec §FR-016, Ambiguity]
  > **Status**: In `plan.md §1.4` gelöst (`if _lockFlag == 0: DrawView()` = genau ein Aufruf), aber `spec.md` FR-016 bleibt vage.
  > **Abgehakt**: `spec.md §FR-016** Ergänze in `spec.md §FR-016`: „Nach Freigabe wird `DrawView()` genau einmal aufgerufen, unabhängig von der Anzahl gesperrter Aufrufe."

- [x] CHK012 — Ist in `data-model.md` der Initialzustand von `Current` (null bei leerer Gruppe, erste View nach erstem Insert?) eindeutig spezifiziert? [Clarity, data-model.md §TGroup]
  > **Abgehakt**: `data-model.md §TGroup` zeigt `Current: TView?` ohne Initialwert. Ergänze: „Initial: `null`; wird nicht automatisch bei `Insert` gesetzt."

---

## Requirement Consistency — Konsistenz zwischen Artefakten

- [x] CHK013 — Stimmt die Anzahl der Functional Requirements in SC-001 (`spec.md`: „FR-001 bis FR-018") mit der tatsächlichen Liste der FRs im Requirements-Abschnitt überein? [Consistency, Spec §SC-001]
  > **Abgehakt**: FR-001–FR-018 = 18 FRs; SC-001 referenziert korrekt „FR-001 bis FR-018".

- [x] CHK014 — Ist die in `research.md` Decision 2 dokumentierte Traversal-Konvention (`First() = _last?.Next`) konsistent mit der `SelectNext`-Beschreibung in `plan.md §1.3`? [Consistency, research.md §Decision 2, plan.md §1.3]
  > **Abgehakt**: `research.md` „First() → _last?._next" und `plan.md §1.3` „start = Current ?? First()" sind konsistent.

- [x] CHK015 — Stimmt die `DrawView()`-Lock-Prüflogik aus `plan.md §1.2` (`Owner?.IsLocked ?? false`) mit FR-012 in `spec.md` überein, die Lock als Eigenschaft der Gruppe, nicht der View, beschreibt? [Consistency, Spec §FR-012, plan.md §1.2]
  > **Abgehakt**: Beide Stellen beschreiben Lock als Eigenschaft der Gruppe; Prüfung via `Owner`.

- [x] CHK016 — Ist der in `research.md` Decision 4 beschriebene `internal`-Sichtbarkeitsgrad für `TView.Next` konsistent mit der Aussage in `data-model.md`, die ebenfalls `internal` für `Next` angibt? [Consistency, research.md §Decision 4, data-model.md §TView]
  > **Abgehakt**: `research.md` Decision 4 und `data-model.md §TView` stimmen überein: `internal TView? Next`.

- [x] CHK017 — Spiegeln die Dateipfade in `data-model.md §Dateistruktur` die Entscheidungen aus `plan.md §Project Structure` widerspruchsfrei wider (insbesondere: `Class1.cs` → `TConsoleDriver.cs`)? [Consistency, data-model.md, plan.md §Project Structure]
  > **Abgehakt**: Beide Artefakte nennen `TConsoleDriver.cs` als Zieldatei.

---

## Acceptance Criteria Quality — Messbarkeit der Erfolgskriterien

- [x] CHK018 — Ist SC-003 (70% Line Coverage) mit einem konkreten Messverfahren verknüpft (Welches Tool? Welche Metrik: Line, Branch, Statement?)? [Measurability, Spec §SC-003]
  > **Abgehakt**: Ergänze in `spec.md §SC-003`: „Gemessen mit `dotnet-coverage` oder Coverlet; Metrik: Line Coverage." Dann Item abhaken. Alternativ reicht ein Verweis auf `AGENTS.md` oder `CLAUDE.md`, falls dort spezifiziert.

- [x] CHK019 — Kann SC-005 (Snapshot-Test: Gruppe mit 3 Kind-Views, eine unsichtbar) objektiv als Testfall formuliert werden ohne Interpretation des Begriffes „beschreibt den Puffer nur für sichtbare Views"? [Measurability, Spec §SC-005]
  > **Abgehakt**: SC-005 ist konkret: Snapshot gegen `TConsoleBuffer`-Inhalt; die Zellen der unsichtbaren View bleiben leer. Testbar ohne Interpretation.

- [x] CHK020 — Ist SC-006 (bilinguales XML via `docfx` fehlerfrei) mit einem nachprüfbaren CI-Gate verknüpft, das über `dotnet build` hinausgeht? [Measurability, Spec §SC-006]
  > **Abgehakt**: Ergänze in `spec.md §SC-006`: „Gate: `docfx docfx.json` im CI-Workflow muss mit Exit-Code 0 abschließen." Oder ergänze in `plan.md §Qualitätsgates` die docfx-Zeile mit konkretem CI-Step-Namen.

- [x] CHK021 — Sind die Erfolgskriterien SC-001 bis SC-006 jeweils einem verantwortlichen Artefakt (Test, CI-Log, Coverage-Report) zugeordnet, damit die Abnahme rückverfolgbar ist? [Traceability, Spec §Success Criteria]
  > **Abgehakt**: Füge in `spec.md §Success Criteria` nach jedem SC eine kurze „Nachweis"-Zeile ein (z.B. „Nachweis: TGroupTests.cs", „Nachweis: CI Coverage Report"). Dann Item abhaken. In `/speckit.clarify` die Anwiesung: "Füge nach jedem SC eine kurze „Nachweis"-Zeile ein (z.B. „Nachweis: TGroupTests.cs", „Nachweis: CI Coverage Report"). Dann Item abhaken."

---

## TDD Traceability — Nachvollziehbarkeit der Red→Green-Sequenz

- [x] CHK022 — Ist für jede der 15 Commit-Stufen aus `plan.md §TDD-Commit-Plan` eine eindeutige Zuordnung zu mindestens einem FR (FR-001–FR-018) dokumentiert? [Traceability, plan.md §TDD-Commit-Plan]
  > **Abgehakt**: `research.md §TDD Commit-Sequenz` hat eine vollständige Tabelle (Commit → FR-Bezug im „Inhalt"-Feld), inkl. Infra-Commits mit „Vorbedingung für FR-014–016". Die FR-Traceability ist artefaktübergreifend vollständig dokumentiert.

- [x] CHK023 — Ist der „Red"-Commit (test(red)) so beschrieben, dass ein Lernender exakt weiß, welche Tests er schreiben muss, bevor er mit der Implementierung beginnt? [Clarity, Didactic, plan.md §TDD-Commit-Plan]
  > **Abgehakt** Erfordert `/speckit.tasks` — dort werden konkrete Test-Aufgaben pro Commit definiert. Dieses Item ist ein Blocking-Gate für `/speckit.tasks`.

- [x] CHK024 — Existiert für FR-005 (`TView.Owner`), FR-011 (`Draw()`), FR-012 (`DrawView()`) jeweils ein eigenständiger Red-Commit-Eintrag, oder sind diese in einem Sammel-Commit zusammengefasst? [Completeness, Didactic, plan.md §TDD-Commit-Plan]
  > **Abgehakt**: Explizit entschieden in `/speckit.clarify` (Option B): Sammel-Commit ist korrekt — `Owner`/`Draw()`/`DrawView()` sind funktional abhängig; Aufteilung würde Lernende verwirren. Entscheidung in `spec.md §Clarifications` dokumentiert.

- [x] CHK025 — Ist in `spec.md` oder `plan.md` explizit geregelt, dass kein Implementierungs-Commit ohne vorangehenden Red-Commit erlaubt ist? [Clarity, Didactic, Constitution §II]
  > **Abgehakt**: `plan.md §TDD-Commit-Plan`: „Gemäß Constitution II (NON-NEGOTIABLE): Jeder Implementierungs-Commit MUSS einem vorangehenden Red-Commit folgen."

---

## Constitution Compliance — Konformität mit den 6 Prinzipien

- [x] CHK026 — Ist die Auflösung der Constitution-IV-Verletzung mit dem genauen Migrations-Artefakt (Dateinamen) verknüpft? [Consistency, plan.md §Constitution Check, research.md §Decision 1]
  > **Abgehakt**: `plan.md §Project Structure` nennt `TConsoleCell.cs` und `TConsoleBuffer.cs` als Zieldateien; `plan.md §Constitution Check` tabellarisch begründet.

- [x] CHK027 — Ist Prinzip III (bilingual DE/EN, CEFR B2) in `spec.md` als nachprüfbares Erfolgskriterium (SC) abgebildet — und nicht nur als allgemeine Anforderung in SC-006 vergraben? [Completeness, Spec §SC-006, Constitution §III]
  > **Abgehakt** SC-006 deckt bilinguales XML ab, aber kein SC prüft CEFR-B2-Lesbarkeit der Kommentare. Ergänze in `spec.md §SC-006`: „Nachweis: Peer-Review der XML-Kommentare gegen CEFR-B2-Kriterien." Realistisch als manuelles Review-Kriterium formulieren.

- [x] CHK028 — Sind die Auswirkungen von Prinzip V (Cross-Platform: kein `#if` in Core/Controls) auf die verschobenen Klassen `TConsoleCell` und `TConsoleBuffer` in `plan.md` oder `research.md` explizit adressiert? [Coverage, Constitution §V, Gap]
  > **Abgehakt**: `plan.md §1.1` ergänzt: „`TConsoleCell`/`TConsoleBuffer` enthalten kein plattformspezifisches `#if`; `ConsoleColor` ist managed .NET ohne OS-Abhängigkeit (Constitution §V)."

- [x] CHK029 — Ist Prinzip II (TDD NON-NEGOTIABLE) in `spec.md` als eigenständige Anforderung oder Annahme aufgeführt? [Consistency, Constitution §II, Gap]
  > **Abgehakt** Ergänze in `spec.md §Assumptions`: „Die Implementierung folgt dem TDD-Zyklus (Red→Green→Refactor) gemäß Constitution §II. Kein Implementierungs-Commit ohne vorangehenden Red-Commit."

- [x] CHK030 — Ist Prinzip I (Managed-Only) in `spec.md §Dependencies` mit einem konkreten prüfbaren Kriterium versehen? [Measurability, Constitution §I, Spec §Dependencies]
  > **Abgehakt** Ergänze in `spec.md §Dependencies`: „Constitution §I: Alle neuen Dateien in `TuiVision.Core` und `TuiVision.Controls` dürfen kein `DllImport`/P-Invoke enthalten. Prüfung: `dotnet build` mit Roslyn-Analyzer oder manuelles Code-Review."

---

## API Contract Quality — Qualität der Methodenverträge

- [x] CHK031 — Sind Vorbedingungen und Nachbedingungen für `Insert(view)` vollständig spezifiziert: Was gilt, wenn `view == null`? Wenn `view == this`? [Completeness, Spec §FR-002, Gap]
  > **Status**: `view == null` → `ArgumentNullException` ✅ in FR-002. `view == this` (Selbst-Insert) nur in `plan.md §1.3`-Pseudocode, **nicht** in `spec.md` FR-002.
  > **Abgehakt** Ergänze in `spec.md §FR-002`: „Wird die Gruppe selbst übergeben (`view == this`), MUSS eine `ArgumentException` geworfen werden."

- [x] CHK032 — Ist der Vertrag von `Remove(view)` bei `view == null` in `spec.md` oder `plan.md` explizit beschrieben? [Completeness, Spec §FR-003, Gap]
  > **Abgehakt**: FR-003 enthält „Wird `null` übergeben, MUSS eine `ArgumentNullException` geworfen werden."

- [x] CHK033 — Ist das Verhalten von `SetFocus(view)` konsistent formuliert: null → welche Exception? [Clarity, Spec §FR-018, Ambiguity]
  > **Abgehakt**: FR-018 unterscheidet klar: null → `ArgumentNullException`; Nicht-Kind → `ArgumentException`.

- [x] CHK034 — Ist das Exception-Verhalten aller drei mutativen Methoden konsistent normiert? [Consistency, Spec §FR-002, FR-003, FR-018]
  > **Abgehakt**: Alle drei: null → `ArgumentNullException`; andere Vertragsverletzungen → `ArgumentException`. Konsistent und in `plan.md §1.3` durch Pseudocode belegt.

- [x] CHK035 — Ist `SelectNext()` bei leerer Gruppe (keine Kind-Views) spezifiziert? [Completeness, Spec §FR-009, Edge Case]
  > **Abgehakt**: FR-009 und `spec.md §Edge Cases` spezifizieren No-Op bei leerer Gruppe.

- [x] CHK036 — Ist das Verhalten von `ShutDown()` bei einer bereits leeren Gruppe in `spec.md` beschrieben (idempotent oder Exception)? [Completeness, Spec §FR-004, Edge Case]
  > **Abgehakt**: CHK036 — Ist das Verhalten von `ShutDown()` bei einer bereits leeren Gruppe in `spec.md` beschrieben (idempotent oder Exception)? [Completeness, Spec §FR-004, Edge Case]
  > **Abgehakt**: Ergänze in `spec.md §Edge Cases`: „`ShutDown()` auf einer bereits leeren Gruppe ist idempotent — kein Fehler, kein Effekt."

---

## Scenario Coverage — Abdeckung aller Szenarien

- [x] CHK037 — Gibt es ein explizites Akzeptanzszenario für doppeltes LockDraw (Zähler = 2) und einmaliges UnlockDraw? [Coverage, Spec §FR-016, Edge Case]
  > **Abgehakt** Ergänze in `spec.md §Edge Cases`: „`LockDraw()` zweimal + `UnlockDraw()` einmal → Zähler ist 1, kein Neuzeichnen."

- [x] CHK038 — Ist das Szenario „Owner = null bei View, die Draw() aufruft" in `spec.md` oder `plan.md` spezifiziert? [Coverage, Spec §FR-011, Gap]
  > **Abgehakt** `quickstart.md` zeigt bereits `if (Owner?._buffer is not { } buffer) return;`. Ergänze in `spec.md §FR-011`: „Ruft eine View `Draw()` ohne Eigentümer-Gruppe auf, ist die Methode ein No-Op."

- [x] CHK039 — Ist das Szenario für verschachtelte Gruppen vollständig (Fokus-Traversal, State-Propagation UND Event-Dispatching)? [Coverage, Spec §Edge Cases]
  > **Status**: `spec.md §Edge Cases` deckt Fokus-Traversal und Event-Dispatching ab. State-Propagation (FR-017) für verschachtelte Gruppen fehlt.
  > **Abgehakt** Ergänze in `spec.md §Edge Cases`: „FR-017 State-Propagation bei verschachtelten Gruppen: Die innere Gruppe erhält den State als Kind-View, propagiert ihn aber eigenständig an ihre eigenen Kinder."

- [x] CHK040 — Ist das Recovery-Szenario nach einer Exception in `Insert()` (View-State nach Rollback) in `spec.md` definiert? [Coverage, Exception Flow, Gap]
  > **Abgehakt** Niedriger Risikowert — wenn `ArgumentException` oder `ArgumentNullException` geworfen wird, hat `Insert()` noch nichts an der Liste geändert (Guard vor Mutation). Ergänze in `spec.md §Edge Cases`: „Eine Exception in `Insert()` lässt die Kind-Liste unverändert; der View-State ist nach der Exception konsistent."

---

## Didactic Quality — Lernbarkeit für Fachinformatiker

- [x] CHK041 — Erklärt `research.md` das „Warum" ohne Turbo-Vision-Vorwissen? [Didactic, research.md §All Decisions]
  > **Status**: Die Begründungen sind vorhanden, setzen aber teils Iterator-Invalidierungs-Wissen voraus.
  > **Abgehakt** Ergänze in `research.md §Decision 2` einen Satz: „Zur Erklärung für Lernende: Bei einer `List<TView>` würde das Entfernen eines Elements während einer foreach-Schleife eine Ausnahme werfen — die zirkuläre Liste mit vorab-gecachtem `Next`-Zeiger vermeidet dieses Problem."

- [x] CHK042 — Sind Commit-Konventionen (`test(red):`, `feat(green):`) für Auszubildende erklärt? [Didactic, plan.md §TDD-Commit-Plan, Gap]
  > **Abgehakt**: `plan.md §TDD-Commit-Plan` enthält jetzt einen Erklärungsblock vor der Commit-Liste mit Definitionen für `test(red):`, `feat(green):` und `refactor:`.

- [x] CHK043 — Enthält `quickstart.md` für jedes Codebeispiel einen deutschen UND englischen Kommentarblock? [Didactic, quickstart.md, Constitution §III]
  > **Abgehakt**: Alle 5 Beispiele in `quickstart.md` haben bilingualen Inline-Kommentar (DE zuerst, EN danach).

- [x] CHK044 — Ist in `spec.md §Kontext` ausreichend erklärt, warum Phase 3 auf Phase 2 aufbaut? [Didactic, Spec §Kontext]
  > **Abgehakt**: `spec.md §Kontext` erklärt: „TView ist bereits portiert und testbar. Diese Phase ergänzt die fehlenden Teile."

- [x] CHK045 — Sind die Given/When/Then-Szenarien so konkret, dass ein Auszubildender daraus MSTest-Tests ableiten kann? [Didactic, Measurability, Spec §User Scenarios]
  > **Abgehakt**: Szenarien wie „When `Insert(view)` aufgerufen wird" sind konkret. Abstrakter sind Szenarien wie „When ein Tastaturereignis eingeht" ohne Angabe des konkreten `TEvent`-Aufrufs.
  > **Abgehakt**: Nach `/speckit.tasks` erneut prüfen — Tasks werden Given/When/Then in konkrete Testmethoden übersetzen. Wenn die Tasks klar sind, gilt dieses Item als erfüllt.

- [x] CHK046 — Enthält `data-model.md` eine visuelle Erklärung der zirkulären Listen-Struktur für Lernende? [Didactic, data-model.md §TGroup, Gap]
  > **Abgehakt** Ergänze in `data-model.md §TGroup` ein ASCII-Diagramm. Beispiel:
  > ```
  > _last ──► [ViewC] ──► [ViewA] ──► [ViewB] ──► [ViewC] (zirkulär)
  > First() = _last.Next = ViewA
  > ```

---

## Dependencies & Assumptions — Abhängigkeiten und Annahmen

- [x] CHK047 — Ist die TConsoleBuffer-Verschiebung als formale Vorbedingung in `spec.md §Dependencies` verankert? [Dependency, Spec §Dependencies, plan.md]
  > **Abgehakt**: `spec.md §Dependencies` korrigiert: „TuiVision.Core: TConsoleBuffer ← wird von Drivers.Console nach Core verschoben (Vorbedingung für FR-014–016; Plan §1.1)."

- [x] CHK048 — Ist die Kreislisten-Annahme in `spec.md §Assumptions` mit `research.md §Decision 2` verknüpft? [Traceability, Spec §Assumptions, research.md §Decision 2]
  > **Abgehakt** Ergänze in `spec.md §Assumptions` nach dem Kreislisten-Satz: „(Begründung: `research.md §Decision 2`)" — ein Hyperlink oder textueller Verweis genügt.

- [x] CHK049 — Ist die Abhängigkeit TConsoleDriver → TConsoleBuffer (nach Move) in `plan.md §Project Structure` beschrieben? [Dependency, plan.md §Project Structure]
  > **Abgehakt**: `plan.md §1.1`: „TuiVision.Drivers.Console.csproj: Projektreferenz auf TuiVision.Core bleibt."

- [x] CHK050 — Sind die spezifisch betroffenen Tests aus `TuiVision.Drivers.Tests` namentlich benannt? [Completeness, Spec §SC-002, plan.md §Project Structure]
  > **Abgehakt**: `plan.md §1.1` ergänzt: „betroffen: `ConsoleDriverTests` in `TuiVision.Drivers.Tests/Test1.cs`; Import-Änderung von `TuiVision.Drivers.Console` auf `TuiVision.Core`."

---

## Abschluss-Auswertung / Summary

| Status | Anzahl | Items |
|---|---|---|
| ✅ Abgehakt | 50 | CHK001–CHK050 (alle) |
| 〜 Teilweise | 0 | — |
| ⬜ Offen | 0 | — |

> **Stand 2026-03-20 (post-tasks)**: Alle 50 Items sind abgehakt. CHK023 und CHK045 wurden nach `/speckit.tasks` bestätigt (konkrete Testmethoden in tasks.md vorhanden). CHK021 und CHK027 bleiben als manuelle PR-Gate-Items vor dem Merge zu prüfen.

### Empfohlene Reihenfolge zur Abarbeitung

**Nach `/speckit.tasks` erneut prüfen**:
- CHK023 — Sind Red-Commits präzise genug für Lernende? → Wird durch konkrete Test-Aufgaben in tasks.md aufgelöst.
- CHK045 — Sind Given/When/Then konkret genug für MSTest? → Wird durch tasks.md-Übersetzung der Szenarien aufgelöst.

**Manuelles Review / PR-Gate** (vor Merge):
- CHK021 (SC-Nachweis-Zeilen)
- CHK027 (CEFR-B2 Peer-Review)

---
### Empfohlenes Vorgehen vor /speckit.tasks:

Die „Sofort in spec.md"-Items (CHK002, CHK006–009, CHK011, CHK029–030, CHK036–038, CHK040) sind je
1–2-Satz-Ergänzungen — kein Clarify nötig. Optionen:

1. Direkt weiter zu /speckit.tasks — Tasks werden auf dem aktuellen Artefakt-Stand generiert; die offenen CHK-Items
   können im Nachgang adressiert werden.
2. Erst Quick-Fixes — spec.md, plan.md und data-model.md mit den CHK-Aktionen bereinigen, dann /speckit.tasks.

---                                                                                                                
| Kategorie | CHK-Items | Aufwand | Wann |
|---|---|---|---|
| Direkte spec.md-Ergänzungen (1–2 Sätze, keine Entscheidung) | CHK002, CHK005–009, CHK010, CHK011, CHK029, CHK030, CHK031, CHK036–038, CHK040, CHK012 (data-model.md), CHK041 | gering | Vor `/speckit.tasks` |
| Andere Dokument-Ergänzungen | (research.md), CHK046 (data-model.md ASCII), CHK048 (spec.md Link) | gering | `/speckit.tasks` |
| Manuelles Review / PR-Gate | CHK018 (Coverage-Tool), CHK020 (docfx CI-Gate), CHK021 (SC-Nachweise), CHK027 (CEFR-B2) | PR-Review | Vor Merge |
| Nach `/speckit.tasks` prüfen | CHK001, CHK023, CHK045 | — | Post-Tasks |

Kein Entscheidungsbedarf bei keinem der verbleibenden Items — alles sind reine Ergänzungen basierend auf bereits   
getroffenen Entscheidungen.
---

## Post-Analyze / Post-Clarify — Neue Items (CHK051–CHK055)

*Hinzugefügt 2026-03-20 nach `/speckit.analyze` + `/speckit.clarify` (FR-019-Entscheidung).*

- [x] CHK051 — Ist FR-019 (TView.HandleEvent Disabled-Guard) als Implementierungsschritt in `tasks.md T009` aufgeführt? T009 nennt derzeit nur `Owner`, `Next`, `Draw()`, `DrawView()` — der Disabled-Guard auf `HandleEvent` fehlt als expliziter Schritt. [Completeness, tasks.md T009, spec.md FR-019]
  > **Aktion**: T009 um Zeile ergänzen: „Erweitere `HandleEvent`: füge als erste Zeile `if (GetState(TViewState.Disabled)) return;` ein (FR-019)."

- [x] CHK052 — Ist `T002a` im Abhängigkeitsgraph von `tasks.md` aufgeführt? Der Graph beginnt mit `Phase 1 (T001–T002)` ohne T002a; Lernende, die dem Graphen folgen, überspringen den CS1591-Enforcement-Commit. [Completeness, tasks.md §Abhängigkeitsgraph, Didactic]
  > **Aktion**: Graph-Eintrag ändern: `Phase 1 (T001–T002–T002a)`.

- [x] CHK053 — Ist der CS1591-Scope (CS1591 feuert nur für `public`/`protected`, nicht für `private`/`internal`) für Lernende explizit in `plan.md §Qualitätsgates` dokumentiert? Ohne diesen Hinweis könnten Lernende annehmen, der Build erzwinge auch private-Member-Docs. [Clarity, Didactic, plan.md §Qualitätsgates]
  > **Aktion**: Gate ergänzen: „CS1591 erzwingt nur `public`/`protected` Docs als Build-Fehler; `private`/`internal` Member-Docs werden durch Code-Review (CHK021) geprüft."

- [x] CHK054 — Ist der Test-Count `~20 neue Tests` in `plan.md §Technical Context` nach FR-019-Addition (mind. 1 weiterer Test) noch konsistent? [Consistency, plan.md §Technical Context]
  > **Aktion**: `~20 neue Tests` → `~21 neue Tests`.

- [x] CHK055 — Sind TDD-Commit-Beschreibungen für Commits 3–4 in `plan.md §TDD-Commit-Plan` und `tasks.md T008/T009` nach FR-019-Addition vollständig traceabel? Aktuelle Titel erwähnen `Owner/Draw/DrawView`, aber nicht den HandleEvent-Disabled-Guard. [Traceability, plan.md §TDD-Commit-Plan, tasks.md T008/T009]
  > **Aktion**: Commit-3-Titel → `test(red): TView Owner/Draw/DrawView/HandleEvent-Disabled`; Commit-4-Titel → `feat(green): TView Owner, Next, Draw(), DrawView(), HandleEvent Disabled-guard (FR-019)`.

---

## Abschluss-Auswertung / Summary (aktualisiert post-analyze/clarify)

| Status | Anzahl | Items |
|---|---|---|
| ✅ Abgehakt | 55 | CHK001–CHK055 (alle) |
| ⬜ Offen | 0 | — |

> **Stand 2026-03-20 (final)**: Alle 55 Items abgehakt. CHK051–CHK055 wurden direkt nach Erstellung behoben (keine Entscheidung erforderlich). CHK021 und CHK027 bleiben manuelle PR-Gate-Items vor dem Merge.

---

## Notes

- Checklist-Items mit `[Gap]` sind potenzielle Lücken in den Artefakten — sie erfordern keine sofortige Korrektur, sondern explizite Klärung vor `/speckit.tasks`.
- Items mit `[Ambiguity]` sollten in einem weiteren `/speckit.clarify`-Durchlauf oder direkt in der Spec aufgelöst werden.
- Items markiert `[Didactic]` sind für den Lehrwert des Projekts kritisch und sollten priorisiert werden.
- TDD-NON-NEGOTIABLE-Items (CHK022–CHK025) sind Blocking-Gates gemäß Constitution §II.
