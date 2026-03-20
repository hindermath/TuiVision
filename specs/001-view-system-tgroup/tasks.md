# Tasks: View-System Phase 3 — TGroup, Zeichenpuffer, Fokus/States

**Feature Branch**: `001-view-system-tgroup`
**Generated**: 2026-03-20
**Plan**: [plan.md](plan.md) | **Spec**: [spec.md](spec.md)
**TDD**: Red→Green→Refactor (Constitution §II — NON-NEGOTIABLE)

---

## Format

```
- [ ] T### [P?] [US?] Beschreibung mit Dateipfad
```

- **[P]**: Parallelisierbar — unterschiedliche Dateien, keine gegenseitige Abhängigkeit
- **[US1–US4]**: Zugehörige User Story aus spec.md
- Commit-Präfixe: `test(red):` | `feat(green):` | `refactor:`

---

## Phase 1: Setup — Bestandsaufnahme

**Zweck**: Vorhandenen Code lesen, bevor Änderungen vorgenommen werden.
**Kein Commit** — nur Lesephase.

- [ ] T001 Lese `src/TuiVision.Drivers.Console/Class1.cs` und erfasse vollständige Implementierung von `TConsoleCell` und `TConsoleBuffer` (Felder, Methoden, Signaturen) als Vorbereitung für Migration
- [ ] T002 Lese `src/TuiVision.Controls/TView.cs` und `tests/TuiVision.Controls.Tests/Test1.cs` und erfasse existierende TView-Struktur (States, Options, Methoden) und Testmuster (Namespace, TestClass, TestMethod-Konventionen)
- [ ] T002a Prüfe `Directory.Build.props`: Stelle sicher, dass `<WarningsAsErrors Condition="'$(Configuration)' == 'Release'">CS1591</WarningsAsErrors>` vorhanden ist; füge es hinzu falls fehlend. Führe `dotnet build --configuration Release` aus — muss 0 Fehler ergeben (da noch kein neuer Code ohne Docs vorhanden). Commit: `chore: enforce CS1591 as Release build error (Constitution §III)`

---

## Phase 2: Foundational — TConsoleBuffer-Migration + TView-Erweiterung

**Zweck**: Architektur-Vorbedingung (Constitution IV) und neue TView-Mitglieder.
**Muss vollständig abgeschlossen sein, bevor US1–US4 beginnen.**

> **CS1591-Hinweis**: Jeder `feat(green)`-Commit, der neue public/internal API einführt, MUSS für jedes neue Member mindestens einen `<summary>`-Stub enthalten, damit `dotnet build --configuration Release` (CS1591 als Error in Release) besteht. Vollständige bilingualen Docs folgen in Phase 7 (T021–T025).

### Commits 1–2: TConsoleBuffer nach Core verschieben

- [ ] T003 **[test(red)]** Erstelle `tests/TuiVision.Core.Tests/TConsoleBufferTests.cs` mit fehlschlagenden Tests, die `TConsoleCell` und `TConsoleBuffer` aus `TuiVision.Core` importieren:
  - `TConsoleCell_DefaultEmpty_HasExpectedValues()` — prüft `TConsoleCell.Empty`
  - `TConsoleBuffer_Constructor_SetsWidthAndHeight()` — prüft Width/Height
  - `TConsoleBuffer_SetCell_StoresAndReturns()` — prüft Indexer-Round-Trip
  - `TConsoleBuffer_TrySetCell_ReturnsFalseOutsideBounds()` — prüft Bounds-Check
  - `TConsoleBuffer_Clear_FillsWithEmpty()` — prüft Clear()
  - Build MUSS fehlschlagen (Core kennt Buffer noch nicht)

- [ ] T004 [P] **[feat(green) part 1]** Erstelle `src/TuiVision.Core/TConsoleCell.cs` — verschiebe `TConsoleCell` aus `Class1.cs`:
  - `readonly record struct TConsoleCell`
  - Felder: `char Glyph`, `ConsoleColor Foreground`, `ConsoleColor Background`
  - Statisches Feld: `public static readonly TConsoleCell Empty = new(' ', ConsoleColor.Gray, ConsoleColor.Black)`
  - Namespace: `TuiVision.Core`

- [ ] T005 [P] **[feat(green) part 2]** Erstelle `src/TuiVision.Core/TConsoleBuffer.cs` — verschiebe `TConsoleBuffer` aus `Class1.cs`:
  - `sealed class TConsoleBuffer`
  - Eigenschaften: `int Width`, `int Height` (readonly)
  - Konstruktor validiert: `Width > 0 && Height > 0`
  - Indexer `this[int x, int y]`, `GetCell`, `SetCell`, `TrySetCell`, `Clear()`, `WriteText(x, y, text, fg, bg)`, `Clone()`
  - Namespace: `TuiVision.Core`

- [ ] T006 **[feat(green) part 3]** Extrahiere `src/TuiVision.Drivers.Console/TConsoleDriver.cs` aus `Class1.cs`:
  - Behalte nur `IConsolePresenter` und `TConsoleDriver` in dieser Datei
  - Füge `using TuiVision.Core;` hinzu (für TConsoleBuffer/TConsoleCell)
  - Lösche oder leere `Class1.cs`
  - Namespace bleibt: `TuiVision.Drivers.Console`

- [ ] T007 **[feat(green) part 4]** Aktualisiere `tests/TuiVision.Drivers.Tests/Test1.cs`:
  - Ändere `using TuiVision.Drivers.Console;` → `using TuiVision.Core;` für Buffer-Typen
  - Alle bestehenden `ConsoleDriverTests`-Methoden müssen weiterhin grün sein
  - Commit: `feat(green): move TConsoleCell + TConsoleBuffer to TuiVision.Core`

### Commits 3–4: TView um Owner, Next, Draw(), DrawView() erweitern

- [ ] T008 **[test(red)]** Erstelle `tests/TuiVision.Controls.Tests/TViewExtendedTests.cs` mit fehlschlagenden Tests:
  - `TView_Owner_IsNullByDefault()` — neue View hat `Owner == null`
  - `TView_Draw_BaseImplementation_IsNoOp()` — kein Fehler beim Aufruf
  - `TView_DrawView_CallsDraw_WhenVisible()` — überschriebenes `Draw()` wird aufgerufen
  - `TView_DrawView_SkipsDraw_WhenInvisible()` — `SetState(Visible, false)` verhindert Aufruf
  - `TView_DrawView_WithNullOwner_IsNoOp()` — kein Fehler wenn `Owner == null` (FR-011 Edge Case)
  - `TView_HandleEvent_Disabled_IgnoresAllEvents()` — **FR-019**: Disabled-View ignoriert jedes Ereignis; prüft, dass `HandleEvent` sofort zurückkehrt ohne Seiteneffekte (Session 2026-03-20)
  - **Wichtig für Lernende**: Tests 1–5 sind **build-red** (Compiler-Fehler, da `Owner`/`Draw()`/`DrawView()` noch nicht existieren). Test 6 (FR-019) ist **runtime-red** — `HandleEvent` existiert bereits, enthält aber den Disabled-Guard noch nicht; der Build gelingt, der Test schlägt zur Laufzeit fehl.

- [ ] T009 **[feat(green)]** Erweitere `src/TuiVision.Controls/TView.cs`:
  - Füge hinzu: `public TGroup? Owner { get; internal set; }` (oben in der Klasse, nach vorhandenen Properties)
  - Füge hinzu: `internal TView? Next { get; set; }` (nach Owner)
  - Füge hinzu: `public virtual void Draw() { }` (No-Op Basis)
  - Füge hinzu: `public void DrawView()` — Rumpf: `if (!GetState(TViewState.Visible) || (Owner?.IsLocked ?? false)) return; Draw();`
  - Erweitere `HandleEvent(TEvent @event)`: füge als **erste Zeile** `if (GetState(TViewState.Disabled)) return;` ein (FR-019; konform zum C++-Original)
  - Commit: `feat(green): add Owner, Draw(), DrawView(), HandleEvent Disabled-guard (FR-019) to TView`

---

## Phase 3: User Story 1 — TGroup als Container-View (P1)

**Ziel**: TGroup erstellt, zwei Kind-Views aufgenommen, Tastaturereignis an fokussierte View weitergeleitet.
**Unabhängiger Test**: TGroup mit zwei Kind-Views + HandleEvent mit Tastatur → nur fokussierte View empfängt Ereignis.

### Commits 5–6: TGroup-Lebenszyklus (Insert/Remove/ShutDown)

- [ ] T010 [US1] **[test(red)]** Erstelle `tests/TuiVision.Controls.Tests/TGroupTests.cs` mit Lebenszyklus-Tests:
  - `TGroup_Constructor_SetsSelectableAndBufferedOptions()`
  - `TGroup_Constructor_Current_IsNull()`
  - `TGroup_Insert_AddsViewToList_AndSetsOwner()`
  - `TGroup_Insert_SecondView_CreatesCircularList()`
  - `TGroup_Insert_Null_ThrowsArgumentNullException()`
  - `TGroup_Insert_Duplicate_ThrowsArgumentException()`
  - `TGroup_Insert_Self_ThrowsArgumentException()`
  - `TGroup_Remove_RemovesView_AndClearsOwner()`
  - `TGroup_Remove_Null_ThrowsArgumentNullException()`
  - `TGroup_Remove_NonMember_ThrowsArgumentException()`
  - `TGroup_ShutDown_CallsShutDownOnAllChildren_InLIFOOrder()`
  - `TGroup_ShutDown_OnEmptyGroup_IsIdempotent()`
  - Build MUSS fehlschlagen (TGroup existiert noch nicht)

- [ ] T011 [US1] **[feat(green) part 1]** Erstelle `src/TuiVision.Controls/DrawPhase.cs`:
  - `internal enum DrawPhase { Focused, PreProcess, PostProcess }`
  - Namespace: `TuiVision.Controls`

- [ ] T012 [US1] **[feat(green) part 2]** Erstelle `src/TuiVision.Controls/TGroup.cs` mit Lebenszyklus-Kern:
  - Felder: `private TView? _last`, `private int _lockFlag`, `private TConsoleBuffer? _buffer`, `private TRect _clip`
  - Eigenschaften: `public TView? Current { get; private set; }`, `internal DrawPhase Phase { get; private set; }`
  - Property: `internal bool IsLocked => _lockFlag > 0`
  - Konstruktor `TGroup(TRect bounds)`: ruft `base(bounds)` auf; setzt `Options |= TViewOptions.Selectable | TViewOptions.Buffered`; `_clip = GetExtent()`
  - Private Methoden: `TView? First() => _last?.Next`, `void ForEach(Action<TView> action)` (pre-fetcht Next vor Aufruf), `TView? FindPrev(TView target)` (O(n)-Traversal in der Kreisliste)
  - `void Insert(TView view)`: GUARD null→ANE; GUARD view.Owner!=null→AE; GUARD view==this→AE; Kreislisten-Einfügung nach plan.md §1.3
  - `void Remove(TView view)`: GUARD null→ANE; GUARD view.Owner!=this→AE; Kreislisten-Entfernung + `if (Current==view) ResetCurrent()`
  - `private void ResetCurrent()` → `Current = null`
  - `override void ShutDown()`: LIFO-Traversal (FindPrev von _last rückwärts); ruft `child.ShutDown()` + `Remove(child)` für jedes Kind; danach `base.ShutDown()`
  - Commit: `feat(green): TGroup circular list + lifecycle`

### Commits 7–8: Drei-Phasen-Ereignis-Dispatch

- [ ] T013 [US1] **[test(red)]** Erweitere `tests/TuiVision.Controls.Tests/TGroupTests.cs` mit HandleEvent-Tests:
  - `TGroup_HandleEvent_Keyboard_DeliversToCurrentOnly()`
  - `TGroup_HandleEvent_PreProcess_ReceivesBeforeFocused()`
  - `TGroup_HandleEvent_PostProcess_ReceivesAfterFocused_WhenNotConsumed()`
  - `TGroup_HandleEvent_PostProcess_Skipped_WhenEventConsumedInFocusedPhase()`
  - `TGroup_HandleEvent_PreProcess_ConsumedEvent_StopsAllSubsequentPhases()`
  - `TGroup_Integration_TwoChildren_FocusSwitch_KeyboardDispatch()` — **SC-004**: kombiniert Insert, SetFocus und HandleEvent in einem einzigen Test; deckt das Akzeptanzszenario aus spec.md SC-004 vollständig ab

- [ ] T014 [US1] **[feat(green)]** Implementiere `override void HandleEvent(TEvent @event)` in `src/TuiVision.Controls/TGroup.cs` nach Pseudocode in plan.md §1.3:
  - Phase = PreProcess: `ForEach(v => { if (v.Options.HasFlag(PreProcess) && (v.EventMask & event.What) != 0) v.HandleEvent(event); })`
  - `if (event.What != TEventKind.Nothing)`: Phase = Focused; Tastatur → `Current?.HandleEvent(event)`
  - `if (event.What != TEventKind.Nothing)`: Phase = PostProcess: `ForEach(v => { if (v.Options.HasFlag(PostProcess) && ...) v.HandleEvent(event); })`
  - Commit: `feat(green): TGroup three-phase HandleEvent`

---

## Phase 4: User Story 2 — Fokus-Management (P2)

**Ziel**: Tab-Navigation zwischen auswählbaren Kind-Views; direkter Fokuswechsel via SetFocus.
**Unabhängiger Test**: Gruppe mit drei auswählbaren Views → wiederholte `SelectNext(true)`-Aufrufe durchlaufen alle Views korrekt.

### Commits 9–10: SelectNext + SetFocus

- [ ] T015 [US2] **[test(red)]** Erweitere `tests/TuiVision.Controls.Tests/TGroupTests.cs` mit Fokus-Tests:
  - `TGroup_SelectNext_Forward_MovesToNextSelectableView()`
  - `TGroup_SelectNext_Forward_WrapsAroundCircularly()`
  - `TGroup_SelectNext_Backward_MovesToPreviousView()`
  - `TGroup_SelectNext_SkipsDisabledViews()`
  - `TGroup_SelectNext_SkipsInvisibleViews()`
  - `TGroup_SelectNext_OnEmptyGroup_IsNoOp()`
  - `TGroup_SelectNext_WithSingleSelectableView_StaysFocused()`
  - `TGroup_SetFocus_TransfersFocusedState()`
  - `TGroup_SetFocus_ClearsPreviousCurrent_Focused()`
  - `TGroup_SetFocus_AlreadyFocused_IsNoOp()`
  - `TGroup_SetFocus_Null_ThrowsArgumentNullException()`
  - `TGroup_SetFocus_NonMember_ThrowsArgumentException()`

- [ ] T016 [US2] **[feat(green)]** Implementiere `void SelectNext(bool forward)` und `void SetFocus(TView view)` in `src/TuiVision.Controls/TGroup.cs` nach plan.md §1.3:
  - `SelectNext`: `if (_last == null) return`; `start = Current ?? First()`; zirkuläre Suche (max n Iterationen) nach nächster Selectable+Visible+!Disabled View; bei Fund → `SetFocus(found)`
  - `SetFocus`: GUARD null→ANE; GUARD view.Owner!=this→AE; `if (Current == view) return`; `Current?.SetState(TViewState.Focused, false)`; `Current = view`; `Current.SetState(TViewState.Focused, true)`
  - Commit: `feat(green): TGroup focus management`

---

## Phase 5: User Story 3 — Draw-Integration / Zeichenpuffer (P3)

**Ziel**: Überschriebenes Draw() schreibt in TConsoleBuffer; nur sichtbare Views werden gezeichnet; LockDraw/UnlockDraw steuern Timing.
**Unabhängiger Test**: Abgeleitete TView mit Draw()-Override → nach DrawView() enthält Puffer erwartete Zeichen.

### Commits 11–12: TGroup.Draw() + Buffer-Lifecycle

- [ ] T017 [US3] **[test(red)]** Erweitere `tests/TuiVision.Controls.Tests/TGroupTests.cs` mit Draw/Buffer-Tests:
  - `TGroup_Draw_CallsDrawViewOnVisibleChildrenInInsertionOrder()`
  - `TGroup_Draw_SkipsInvisibleChildren()`
  - `TGroup_Draw_AllocatesBuffer_WhenBufferedAndExposed()`
  - `TGroup_OnBoundsChanged_ReallocatesBuffer_WhenBufferExists()`
  - `TGroup_LockDraw_PreventsDrawView()`
  - `TGroup_UnlockDraw_TriggersExactlyOneDrawView()`
  - `TGroup_LockDraw_Nested_CounterMustReachZero_BeforeRedraw()` — LockDraw×2 + UnlockDraw×1 → kein Neuzeichnen; erst UnlockDraw×2 → Neuzeichnen
  - `TGroup_Draw_VisibleChild_WritesExpectedCellsToBuffer()` — **SC-005**: Snapshot-Test; prüft konkrete `TConsoleCell`-Werte an erwarteten Koordinaten im `TConsoleBuffer` nach `DrawView()`; eine sichtbare und eine unsichtbare Kind-View; Puffer enthält nur Zellen der sichtbaren View

- [ ] T018 [US3] **[feat(green)]** Implementiere in `src/TuiVision.Controls/TGroup.cs` nach plan.md §1.4:
  - `override void Draw()`: Puffer-Allokation wenn `_buffer == null && Options.HasFlag(Buffered) && GetState(Exposed)`; `ForEach(v => { if (v.GetState(TViewState.Visible)) v.DrawView(); })`
  - `void LockDraw()` — `_lockFlag++`
  - `void UnlockDraw()` — `if (_lockFlag > 0) _lockFlag--`; `if (_lockFlag == 0) DrawView()`
  - `override void OnBoundsChanged()` — `if (_buffer != null) _buffer = new TConsoleBuffer(Size.X, Size.Y)`; `_clip = GetExtent()`
  - Commit: `feat(green): TGroup buffer + lock mechanics`

---

## Phase 6: User Story 4 — State-Propagation (P4)

**Ziel**: SetState(Active/Focused/Disabled) auf TGroup propagiert an alle direkten Kind-Views.
**Unabhängiger Test**: `SetState(TViewState.Active, true)` → alle direkten Kinder haben Active-State.

### Commits 13–14: SetState-Propagation

- [ ] T019 [US4] **[test(red)]** Erweitere `tests/TuiVision.Controls.Tests/TGroupTests.cs` mit State-Propagations-Tests:
  - `TGroup_SetState_Active_PropagatesDirectChildren()`
  - `TGroup_SetState_Disabled_PropagatesDirectChildren()`
  - `TGroup_SetState_Focused_PropagatesDirectChildren()`
  - `TGroup_SetState_Active_NestedGroup_ReceivesAsOneChild()`
  - `TGroup_SetState_Active_EmptyGroup_IsIdempotent()`

- [ ] T020 [US4] **[feat(green)]** Implementiere `override void SetState(TViewState state, bool enable)` in `src/TuiVision.Controls/TGroup.cs`:
  - `base.SetState(state, enable)`
  - `if ((state & (TViewState.Active | TViewState.Focused | TViewState.Disabled)) != 0) { ForEach(v => v.SetState(state, enable)); }`
  - Commit: `feat(green): TGroup SetState propagation`

---

## Phase 7: Polish — XML-Dokumentation + Qualitätsgates

**Zweck**: Bilinguales DE/EN CEFR-B2 XML, Formatierung, Coverage-Nachweis.
**Commit 15**: `refactor: documentation pass (bilingual XML, remarks, examples)`

- [ ] T021 [P] Ergänze vollständige bilingualen DE/EN CEFR-B2 XML-Dokumentation (`<summary>`, `<param>`, `<returns>`, `<exception>`, `<remarks>`) für **alle Members (public, internal, private)** von `src/TuiVision.Core/TConsoleCell.cs` — Constitution §III: kein Access Level ausgenommen
- [ ] T022 [P] Ergänze vollständige bilingualen DE/EN CEFR-B2 XML-Dokumentation für **alle Members (public, internal, private inkl. interne Felder/Hilfsmethoden)** von `src/TuiVision.Core/TConsoleBuffer.cs` — Constitution §III
- [ ] T023 [P] Ergänze vollständige bilingualen DE/EN CEFR-B2 XML-Dokumentation für `Owner`, `Next`, `Draw()`, `DrawView()` und `HandleEvent(TEvent event)` (FR-019 Disabled-Guard) in `src/TuiVision.Controls/TView.cs`
- [ ] T024 [P] Ergänze bilingualen DE/EN XML-Dokumentation für alle Werte des internen `DrawPhase`-Enum in `src/TuiVision.Controls/DrawPhase.cs`
- [ ] T025 [P] Ergänze vollständige bilingualen DE/EN CEFR-B2 XML-Dokumentation inkl. `<example>`-Blöcken für **alle Members** von `src/TuiVision.Controls/TGroup.cs`:
  - public/internal: alle Methoden, Properties, Konstruktor (`<summary>`, `<param>`, `<returns>`, `<exception>`, `<remarks>`)
  - private Felder: `_last`, `_lockFlag`, `_buffer`, `_clip` — je `<summary>` bilingual (Zweck + Invariante)
  - private Methoden: `First()`, `ForEach()`, `FindPrev()`, `ResetCurrent()` — je `<summary>` + `<param>` bilingual
  - Algorithmus-kritische Stellen (Insert, Remove, SelectNext) erhalten zusätzliche bilingualen Block-Kommentare (Constitution §III)
- [ ] T026 Führe `dotnet build --configuration Release` aus und behebe alle Warnungen (CS1591 für fehlende XML-Docs; Build muss 0 Fehler / 0 Warnungen ergeben)
- [ ] T027 Führe `dotnet test` aus; stelle sicher, dass alle Tests grün sind; messe Line Coverage mit `dotnet-coverage collect "dotnet test" --output coverage.xml` → TuiVision.Controls ≥ 70 %
- [ ] T028 Führe `dotnet format --verify-no-changes` aus und behebe alle Formatierungsverstöße in den neuen Dateien

---

## Abhängigkeitsgraph

```
Phase 1 (T001–T002–T002a)
  └── Phase 2 Commits 1-2 (T003–T007)
        ├── T003 [test(red)]
        ├── T004 [P] + T005 [P]    ← parallel möglich
        ├── T006 (nach T004, T005)
        └── T007 (nach T004, T005)
              └── Phase 2 Commits 3-4 (T008–T009)
                    ├── T008 [test(red)]
                    └── T009 (nach T008)
                          └── Phase 3/US1 (T010–T014)
                                ├── T010 [test(red)]
                                ├── T011 → T012 (sequenziell)
                                ├── T013 [test(red)] (nach T012)
                                └── T014 (nach T013)
                                      ├── Phase 4/US2 (T015–T016)
                                      ├── Phase 5/US3 (T017–T018)
                                      └── Phase 6/US4 (T019–T020)
                                            └── Phase 7/Polish (T021–T028)
                                                  ├── T021–T025 [P] parallel
                                                  ├── T026 (nach T021–T025)
                                                  ├── T027 (nach T026)
                                                  └── T028 (nach T026)
```

**US2, US3, US4 können nach Abschluss von US1 parallel implementiert werden** (unterschiedliche Methoden in TGroup.cs, aber Vorsicht bei gleichzeitigen Edits derselben Datei).

---

## MVP-Scope

**MVP = User Story 1 vollständig** (Phase 2 + Phase 3, T001–T014):
Nach T014 ist eine funktionsfähige TGroup mit Insert/Remove/ShutDown und Drei-Phasen-Ereignis-Dispatch vorhanden — ausreichend für Integration in TProgram (Phase 4 der Portierung).

US2–US4 sind eigenständige Inkremente und können separat integriert und getestet werden.
