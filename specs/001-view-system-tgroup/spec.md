# Feature Specification: View-System Phase 3 — TGroup, Zeichenpuffer, Fokus/States

**Feature Branch**: `001-view-system-tgroup`
**Created**: 2026-03-16
**Status**: Draft
**Pflichtenheft**: Abschnitt 8.1 Nr. 3 — View-System

---

## Kontext / Context

Phase 3 der Portierungsstrategie (Pflichtenheft Abschnitt 8.1) vervollständigt das View-System.
`TView` ist bereits portiert und testbar. Diese Phase ergänzt die fehlenden Teile:
`TGroup` als Container aller Kind-Ansichten, die Zeichenpuffer-Integration und das vollständige Fokus-/State-Management.

Phase 3 of the porting strategy completes the View-System.
`TView` is already ported and testable. This phase adds the missing pieces:
`TGroup` as container for all child views, drawing-buffer integration, and complete focus/state management.

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 — TGroup als Container-View (Priority: P1)

Ein Entwickler, der eine TUI-Anwendung mit TuiVision aufbaut, möchte mehrere Ansichten
(z. B. eine Menüleiste und einen Desktop-Bereich) in einem gemeinsamen Container zusammenfassen,
damit Ereignisse, Fokus und Neuzeichnen zentral verwaltet werden.

A developer building a TUI application with TuiVision wants to combine multiple views
(e.g. a menu bar and a desktop area) into a shared container so that events, focus, and
repainting are managed centrally.

**Why this priority**: Alle nachfolgenden Portierungsphasen (TProgram, TDialog, TWindow etc.)
erben von `TGroup`. Ohne `TGroup` können keine zusammengesetzten Oberflächen existieren.

**Independent Test**: Eine `TGroup` kann erstellt werden, zwei Kind-`TView`-Instanzen aufnehmen,
und ein Tastaturereignis wird an die fokussierte Kind-Ansicht weitergeleitet.

**Acceptance Scenarios**:

1. **Given** eine `TGroup` mit zwei Kind-Views, **When** `Insert(view)` aufgerufen wird, **Then** ist die View Teil der internen Kind-Liste und ihr `Owner` zeigt auf die Gruppe.
2. **Given** eine `TGroup` mit einer sichtbaren Kind-View, **When** `Remove(view)` aufgerufen wird, **Then** ist die View nicht mehr in der Kind-Liste und `Owner` ist `null`.
3. **Given** eine `TGroup` mit zwei Kind-Views, **When** `HandleEvent` mit einem Tastaturereignis aufgerufen wird, **Then** empfängt nur die fokussierte Kind-View das Ereignis.
4. **Given** eine `TGroup`, **When** `ShutDown()` aufgerufen wird, **Then** werden alle Kind-Views rekursiv heruntergefahren und die Kind-Liste ist leer.
5. **Given** eine `TGroup` mit einer Kind-View mit gesetzter `PreProcess`-Option und einer fokussierten Kind-View, **When** `HandleEvent` mit einem Tastaturereignis aufgerufen wird, **Then** empfängt die PreProcess-View das Ereignis vor der fokussierten View (Phase 1 vor Phase 2).
6. **Given** eine `TGroup` mit einer Kind-View mit gesetzter `PostProcess`-Option und einer fokussierten Kind-View, **When** `HandleEvent` mit einem Tastaturereignis aufgerufen wird und die fokussierte View das Ereignis nicht auf `Nothing` setzt, **Then** empfängt die PostProcess-View das Ereignis nach der fokussierten View (Phase 3 nach Phase 2).

---

### User Story 2 — Fokus-Management (Priority: P2)

Ein Entwickler möchte mit `Tab`/`Shift+Tab` oder per Mausklick den Eingabefokus zwischen
auswählbaren Kind-Views einer Gruppe wechseln, damit die aktive View visuell hervorgehoben
wird und Tastatureingaben empfängt.

A developer wants to move input focus between selectable child views of a group using
`Tab`/`Shift+Tab` or mouse click, so that the active view is visually highlighted and
receives keyboard input.

**Why this priority**: Fokus-Management ist Voraussetzung für jede interaktive Oberfläche.
Ohne Fokuswechsel sind Dialoge und Eingabefelder (Phase 5) nicht nutzbar.

**Independent Test**: Eine Gruppe mit drei auswählbaren Views wechselt den Fokus korrekt
durch alle Views bei wiederholten `SelectNext(true)`-Aufrufen.

**Acceptance Scenarios**:

1. **Given** eine `TGroup` mit drei auswählbaren Kind-Views, **When** `SelectNext(forward: true)` aufgerufen wird, **Then** hat die nächste auswählbare View den Zustand `Focused`.
2. **Given** eine fokussierte Kind-View in einer Gruppe, **When** `SetFocus(otherView)` aufgerufen wird, **Then** verliert die bisherige View den `Focused`-State und die neue View erhält ihn.
3. **Given** eine `TGroup` mit einer deaktivierten View zwischen zwei auswählbaren Views, **When** `SelectNext` aufgerufen wird, **Then** wird die deaktivierte View übersprungen.
4. **Given** eine Gruppe ohne auswählbare Kind-Views, **When** `SelectNext` aufgerufen wird, **Then** bleibt der Fokus-State unverändert (kein Absturz, kein endloser Loop).

---

### User Story 3 — Draw-Integration / Zeichenpuffer (Priority: P3)

Ein Entwickler, der eine eigene View-Klasse ableitet, möchte `Draw()` überschreiben und
den Zeichenpuffer (`TConsoleBuffer`) beschreiben, damit der Inhalt der View auf dem
Bildschirm erscheint, wenn die Gruppe neu gezeichnet wird.

A developer deriving a custom view class wants to override `Draw()` and write to the
drawing buffer (`TConsoleBuffer`) so that the view's content appears on screen when the
group repaints.

**Why this priority**: Ohne `Draw()` können keine Inhalte dargestellt werden.
Alle sichtbaren Elemente (Rahmen, Text, Buttons) hängen davon ab.

**Independent Test**: Eine abgeleitete `TView`-Klasse mit überschriebenem `Draw()` wird
in eine `TGroup` eingefügt; nach `DrawView()` enthält der Puffer den erwarteten Text.

**Acceptance Scenarios**:

1. **Given** eine abgeleitete View mit überschriebenem `Draw()`, **When** `DrawView()` aufgerufen wird, **Then** wird `Draw()` genau einmal aufgerufen und der Puffer enthält die gesetzten Zeichen.
2. **Given** eine sichtbare und eine unsichtbare Kind-View in einer Gruppe, **When** `DrawView()` auf der Gruppe aufgerufen wird, **Then** wird nur die sichtbare View neu gezeichnet.
3. **Given** eine `TGroup` mit aktiviertem Puffer-Modus, **When** die Größe der Gruppe geändert wird, **Then** wird der Puffer neu allokiert und anschließend alle Kind-Views neu gezeichnet.
4. **Given** ein gesperrter Zeichenpuffer (`LockDraw()`), **When** `DrawView()` aufgerufen wird, **Then** wird kein Neuzeichnen durchgeführt, bis `UnlockDraw()` aufgerufen wird.

---

### User Story 4 — State-Propagation in der Gruppen-Hierarchie (Priority: P4)

Ein Entwickler möchte, dass das Aktivieren oder Deaktivieren einer `TGroup` automatisch
alle Kind-Views betrifft, damit z. B. ein modaler Dialog die restliche Oberfläche sperrt.

A developer wants activating or deactivating a `TGroup` to automatically affect all child
views, so that e.g. a modal dialog locks the rest of the interface.

**Why this priority**: State-Propagation ist nötig für modale Dialoge (Phase 5) und
das Aktiv/Inaktiv-Feedback für den Benutzer.

**Independent Test**: `SetState(TViewState.Active, true)` auf einer Gruppe setzt den
`Active`-State auch bei allen direkten Kind-Views.

**Acceptance Scenarios**:

1. **Given** eine `TGroup` mit zwei Kind-Views, **When** `SetState(Active, true)` aufgerufen wird, **Then** haben alle Kind-Views ebenfalls den `Active`-State.
2. **Given** eine Gruppe im `Disabled`-State, **When** ein Maus- oder Tastaturereignis eingeht, **Then** wird das Ereignis von allen Kind-Views ignoriert.
3. *(Deferred — Phase 4)* **Given** eine `TGroup` im `Modal`-State, **When** ein Ereignis ausserhalb der Gruppe auftritt, **Then** wird das Ereignis nicht an Views ausserhalb der Gruppe weitergeleitet. — Dieses Verhalten erfordert `TProgram`/`TApplication` als Event-Router (Phase 4); `TGroup` selbst implementiert kein globales Event-Gating. Kein FR und keine Task in dieser Phase.

---

### Edge Cases

- Was passiert, wenn `Insert` dieselbe View zweimal aufgerufen wird? → Wirft `ArgumentException`; doppeltes Insert ist ein Vertragsfehler.
- Was passiert, wenn `Draw()` einer Kind-View eine Exception wirft? → Exception propagiert; kein stilles Schlucken.
- Was passiert, wenn `SelectNext` auf einer leeren Gruppe (keine Kind-Views) aufgerufen wird? → No-Op; kein Fehler, kein Fokus-Wechsel.
- Was passiert, wenn `SelectNext` auf einer Gruppe mit genau einer auswählbaren View aufgerufen wird? → Die View bleibt fokussiert, kein Loop.
- Was passiert bei verschachtelten Gruppen (Gruppe in Gruppe)? → Ereignis-Dispatching und Focus-Traversal gelten nur für direkte Kind-Views; verschachtelte Gruppen verwalten ihren eigenen Fokus.
- Was passiert, wenn `GrowTo` auf einer Gruppe aufgerufen wird? → Kind-Views behalten ihre absolute Position; Größenänderung betrifft nur den Container selbst (GrowMode ist optionale Erweiterung dieser Phase).
- Mehrfaches LockDraw() inkrementiert den Zähler; erst wenn UnlockDraw() denselben Aufrufanzahl erreicht, wird neu gezeichnet.
- `ShutDown()` auf einer bereits leeren Gruppe ist idempotent — kein Fehler, kein Effekt.
- `LockDraw()` zweimal + `UnlockDraw()` einmal → Zähler ist 1, kein Neuzeichnen.
- FR-017 State-Propagation bei verschachtelten Gruppen: Die innere Gruppe erhält den State als Kind-View, propagiert ihn aber eigenständig an ihre eigenen Kinder.
- Eine Exception in `Insert()` lässt die Kind-Liste unverändert; der View-State ist nach der Exception konsistent.
---

## Requirements *(mandatory)*

### Functional Requirements

**TGroup — Lebenszyklus und Kind-Verwaltung**

- **FR-001**: Das System MUSS eine `TGroup`-Klasse bereitstellen, die von `TView` erbt und eine geordnete Liste von Kind-Views verwaltet.
- **FR-002**: `TGroup.Insert(view)` MUSS eine Kind-View in die interne Liste aufnehmen und deren `Owner`-Eigenschaft auf die Gruppe setzen. Wird `null` übergeben, MUSS eine `ArgumentNullException` geworfen werden. Wird eine View übergeben, deren `Owner` bereits gesetzt ist (egal ob durch diese oder eine andere Gruppe), MUSS eine `ArgumentException` geworfen werden. Wird die Gruppe selbst übergeben (`view == this`), MUSS eine `ArgumentException` geworfen werden.
- **FR-003**: `TGroup.Remove(view)` MUSS eine Kind-View aus der Liste entfernen und deren `Owner`-Eigenschaft auf `null` setzen. Wird `null` übergeben, MUSS eine `ArgumentNullException` geworfen werden. Wird eine View übergeben, die nicht zur Gruppe gehört, MUSS eine `ArgumentException` geworfen werden.
- **FR-004**: `TGroup.ShutDown()` MUSS alle Kind-Views rekursiv herunterfahren und die Kind-Liste leeren, bevor `TView.ShutDown()` aufgerufen wird. Die Kind-Views werden in umgekehrter Einfügereihenfolge (LIFO) heruntergefahren.
- **FR-005**: `TView` MUSS eine `Owner`-Eigenschaft vom Typ `TGroup?` besitzen, die die übergeordnete Gruppe referenziert.

**TGroup — Ereignis-Dispatching**

- **FR-006**: `TGroup.HandleEvent(event)` MUSS Ereignisse in drei Phasen verteilen: Pre-Process (Views mit `PreProcess`-Option), Focused-Phase (fokussierte View), Post-Process (Views mit `PostProcess`-Option). In der Focused-Phase werden Tastaturereignisse an `Current` weitergeleitet; Maus-Positional-Routing (Ereignis an die View unter dem Mauszeiger) ist in dieser Phase auf die direkte Weiterleitung an `Current` beschränkt und wird in Phase 4 (TProgram) vollständig ausgebaut. / In the Focused phase, keyboard events are routed to `Current`; full mouse positional routing (event dispatched to the view under the cursor) is limited to forwarding to `Current` in this phase and will be fully implemented in Phase 4 (TProgram).
- **FR-007**: Das System MUSS sicherstellen, dass ein von einer Kind-View auf `TEventKind.Nothing` gesetztes Ereignis nicht an weitere Views weitergeleitet wird.

**TGroup — Fokus-Management**

- **FR-008**: `TGroup` MUSS eine `Current`-Eigenschaft vom Typ `TView?` besitzen, die die aktuell fokussierte Kind-View referenziert.
- **FR-009**: `TGroup.SelectNext(bool forward)` MUSS den Fokus zur nächsten (oder vorherigen) auswählbaren, sichtbaren und nicht deaktivierten Kind-View verschieben. Die Traversal ist zirkulär: nach der letzten View wird zur ersten gewechselt und umgekehrt. Bei leerer Gruppe (keine Kind-Views) ist die Methode ein No-Op — kein Fehler, kein Fokus-Wechsel.
- **FR-010**: Beim Fokuswechsel MUSS die bisher fokussierte View den `Focused`-State verlieren und die neue View ihn erhalten.
- **FR-018**: `TGroup` MUSS eine öffentliche Methode `SetFocus(TView view)` bereitstellen, die den Fokus direkt auf eine bestimmte Kind-View setzt. `SelectNext` verwendet `SetFocus` intern. Wird `null` übergeben, MUSS eine `ArgumentNullException` geworfen werden. Wird eine View übergeben, die nicht zur Gruppe gehört, MUSS eine `ArgumentException` geworfen werden. Wird die bereits fokussierte View übergeben, ist die Methode ein No-Op.

**TView — Draw-Protokoll**

- **FR-011**: `TView` MUSS eine parameterfreie virtuelle `Draw()`-Methode (`void Draw()`) besitzen, die von abgeleiteten Klassen überschrieben wird; die Basis-Implementierung ist eine No-Op. Eine View, die zeichnen möchte, greift intern über die `Owner`-Eigenschaft auf den Zeichenpuffer der übergeordneten `TGroup` zu (analog zum C++-Original). `DrawView()` ist die Template-Methode; abgeleitete Klassen überschreiben ausschließlich `Draw()`. Ruft eine View `Draw()` ohne Eigentümer-Gruppe auf, ist die Methode ein No-Op.
- **FR-012**: `TView` MUSS eine `DrawView()`-Methode besitzen, die `Draw()` aufruft, sofern die View sichtbar und nicht gesperrt ist. sichtbar = `GetState(TViewState.Visible) == true`

**TView — Ereignis-Verarbeitung**

- **FR-019**: `TView.HandleEvent(event)` MUSS als erste Operation prüfen, ob die View deaktiviert ist (`GetState(TViewState.Disabled)`); ist dies der Fall, MUSS die Methode sofort zurückkehren ohne das Ereignis zu verarbeiten oder weiterzuleiten. Konform zum C++-Original: `if (state & sfDisabled) return;` war die erste Zeile von `TView::handleEvent`. Zusammen mit FR-017 (Disabled-Propagation) deckt dies US4 Szenario 2 ab.

**TGroup — Draw**

- **FR-013**: `TGroup.Draw()` MUSS alle sichtbaren Kind-Views in Z-Reihenfolge neu zeichnen. Z-Reihenfolge = Einfügereihenfolge; erste eingefügte View wird zuerst (unten), zuletzt eingefügte zuletzt (oben) gezeichnet.

**TGroup — Zeichenpuffer**

- **FR-014**: `TGroup` MUSS einen optionalen internen Zeichenpuffer (`TConsoleBuffer`) unterstützen, der bei `TViewOptions.Buffered` genutzt wird.
- **FR-015**: Bei einer Grössenänderung der Gruppe MUSS der Zeichenpuffer neu allokiert werden.
- **FR-016**: `TGroup.LockDraw()` MUSS das Neuzeichnen temporär sperren; `TGroup.UnlockDraw()` MUSS das Neuzeichnen freigeben. Nach Freigabe wird `DrawView()` **genau einmal** aufgerufen, unabhängig von der Anzahl vorangegangener `LockDraw()`-Aufrufe.

**State-Propagation**

- **FR-017**: `TGroup.SetState(state, enable)` MUSS für die States `Active`, `Focused` und `Disabled` die Änderung an alle direkten Kind-Views weitergeben. Verschachtelte Gruppen erhalten die State-Änderung als eine Kind-View, propagieren sie aber intern eigenständig.

### Key Entities

- **TGroup**: Direkt abgeleitete Klasse von `TView`; verwaltet eine doppelt-verlinkte zirkuläre Liste von Kind-Views; besitzt `Current` (fokussierte View), optionalen Zeichenpuffer und Lock-Zähler.
- **TView.Owner**: Rückwärts-Referenz einer Kind-View auf ihre übergeordnete `TGroup`; nullable.
- **DrawPhase**: Interner Aufzählungstyp der Gruppe, der steuert, in welcher Phase des Dispatching eine View ein Ereignis empfängt (Focused, PreProcess, PostProcess).

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Alle 19 funktionalen Anforderungen (FR-001 bis FR-019) sind durch mindestens einen Positiv- und — wo fachlich sinnvoll — einen Negativtest abgedeckt.
- **SC-002**: `dotnet test` läuft in CI ohne Fehler; alle bestehenden 19 Tests bleiben grün.
- **SC-003**: Die Testabdeckung (Line Coverage) für `TuiVision.Controls` erreicht mindestens 70 % (Pflichtenheft Abschnitt 9.4 Nr. 1). Gemessen mit `dotnet-coverage`; Metrik: Line Coverage. AGENTS.md und CLAUDE.md ergänzen, falls dort noch nicht vorhanden.
- **SC-004**: Ein Entwickler kann eine `TGroup` mit zwei Kind-Views erstellen, Fokus wechseln und ein Tastaturereignis erfolgreich an die fokussierte View dispatchen — nachweisbar durch einen Integrationstest.
- **SC-005**: Das Neuzeichnen einer Gruppe mit drei Kind-Views (eine davon unsichtbar) beschreibt den Puffer nur für sichtbare Views — nachweisbar durch einen Snapshot-Test gegen den `TConsoleBuffer`.
- **SC-006**: Die vollständige öffentliche API von `TGroup` und die neuen `TView`-Mitglieder (`Owner`, `Draw`, `DrawView`) sind mit bilingualen XML-Kommentaren (Deutsch zuerst, Englisch danach) dokumentiert und durch `docfx` fehlerfrei verarbeitbar. Gate: `docfx docfx.json` im CI-Workflow muss mit Exit-Code 0 abschließen. Nachweis: Peer-Review der XML-Kommentare gegen CEFR-B2-Kriterien.

---

## Assumptions

- `TGroup` verwendet zunächst eine einfache doppelt-verlinkte Kreisliste (analog zum C++-Original mit `prev`/`next`) statt einer generischen .NET-Collection, um das Original-Verhalten exakt abzubilden. (Begründung: `research.md §Decision 2`)
- `GrowMode`-Unterstützung für Kind-Views (automatisches Mitskalieren) wird in dieser Phase als optionale Erweiterung behandelt, da sie nicht zum Kern-Fokus gehört.
- Das Zeichenprotokoll nutzt `TConsoleBuffer` aus `TuiVision.Core` (nach Verschiebung aus `TuiVision.Drivers.Console`; Begründung: `research.md §Decision 1`); eine direkte Kopplung der Treiberschicht an `TGroup` entfällt damit.
- Phase 3 liefert keine sichtbare Benutzeroberfläche — nur die Infrastruktur. Sichtbare Ergebnisse entstehen erst ab Phase 4 (TProgram/TApplication).
- Die Implementierung folgt dem TDD-Zyklus (Red→Green→Refactor) gemäß Constitution §II. Kein Implementierungs-Commit ohne vorangehenden Red-Commit.
---

## Clarifications

### Session 2026-03-16

- Q: Signatur von `TView.Draw()` — parameterfrei oder mit Puffer-Parameter? → A: Parameterfrei (`void Draw()`); View greift intern über `Owner` auf den Puffer der Eigentümer-Gruppe zu (Option A, konform zum C++-Original).
- Q: Verhalten bei doppeltem `Insert` derselben View? → A: Wirft `ArgumentException` (Option A); doppeltes Insert ist ein Vertragsfehler, der sofort sichtbar sein muss.
- Q: `SelectNext` an der Listengrenze — zirkulär oder stopp? → A: Zirkulär (Option A); nach der letzten View wird zur ersten gewechselt, konform zum C++-Original und TUI-Tab-Konvention.

### Session 2026-03-16 (Fortsetzung)

- Q: `SetFocus` — eigenständige öffentliche Methode oder nur interner Mechanismus von `SelectNext`? → A: Eigenständige öffentliche Methode (Option A); `SelectNext` ruft `SetFocus` intern auf; bei Übergabe einer Nicht-Kind-View → `ArgumentException` (FR-018 ergänzt).
- Q: `Remove(view)` mit nicht-enthaltener View — ignorieren oder Exception? → A: Wirft `ArgumentException` (Option A); konsistenter Vertrag mit Insert und SetFocus (FR-003 aktualisiert).

### Session 2026-03-17

- Q: `null`-Übergabe an `Insert`, `Remove`, `SetFocus` — `ArgumentNullException` oder `ArgumentException`? → A: `ArgumentNullException` (Option A); .NET-konform, `ArgumentNullException.ThrowIfNull()` nutzbar; trennbar von anderen Vertragsfehlern (FR-002, FR-003, FR-018 aktualisiert).
- Q: `SelectNext` bei leerer Gruppe (keine Kind-Views) — No-Op oder Exception? → A: No-Op (Option A); kein Fehler, kein Fokus-Wechsel; konform zum C++-Original (`last == 0` → keine Iteration). FR-009 und Edge Cases aktualisiert.

### Session 2026-03-17 (Fortsetzung)

- Q: Sollen für FR-006 (Drei-Phasen-Dispatch) eigene Given/When/Then-Szenarien für PreProcess- und PostProcess-Phase ergänzt werden? → A: Ja, beide (Option A); PreProcess-View empfängt Event vor fokussierter View (Phase 1), PostProcess-View danach (Phase 3); User Story 1 Szenarien 5 und 6 ergänzt.
- Q: TDD-Commit-Granularität für FR-005/011/012 — aufteilen oder Sammel-Commit beibehalten? → A: Sammel-Commit beibehalten (Option B); Owner/Draw/DrawView sind funktional abhängig und bilden eine logische Implementierungseinheit; plan.md §TDD-Commit-Plan unverändert.

### Session 2026-03-20

- Q: Sollen für FR-002/003/007/013–014 dedizierte `Given/When/Then`-Negativszenarien in den User Stories ergänzt werden (SC-001-Abdeckung)? → A: Nein (Option B); Exception-Verträge sind im FR-Text vollständig spezifiziert; Edge Cases decken die Grenzbedingungen ab; SC-001 „wo fachlich sinnvoll" gilt als hinreichend — keine weiteren formalen GWT-Negative nötig.
- Q: Wo soll der Disabled-Guard für Event-Suppression verankert werden — in `TView.HandleEvent` oder `TGroup.HandleEvent`? → A: In `TView.HandleEvent` (Option A); konform zum C++-Original (`if (state & sfDisabled) return;` als erste Operation); FR-019 ergänzt; Test `TView_HandleEvent_Disabled_IgnoresAllEvents()` in T008 (TViewExtendedTests).

---

## Dependencies

- `TuiVision.Core`: `TPoint`, `TRect`, `TEvent`, `TObject` — vollständig portiert (Phase 2 abgeschlossen).
- `TuiVision.Controls`: `TView` — vollständig portiert, Tests grün.
- `TuiVision.Core`: `TConsoleBuffer` ← wird von `TuiVision.Drivers.Console` nach `TuiVision.Core` verschoben (Vorbedingung für FR-014–016; Plan §1.1).
- Referenz-Quellcode: `tv203s/contrib/tvision/classes/tgroup.cc`, `tview.cc` (nicht verändern).
- Constitution §I: Alle neuen Dateien in `TuiVision.Core` und `TuiVision.Controls` dürfen kein `DllImport`/P-Invoke enthalten. Prüfung: `dotnet build` mit Roslyn-Analyzer oder manuelles Code-Review.