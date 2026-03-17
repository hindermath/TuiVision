# Implementation Plan: View-System Phase 3 — TGroup, Zeichenpuffer, Fokus/States

**Branch**: `001-view-system-tgroup` | **Date**: 2026-03-16 | **Spec**: [spec.md](spec.md)

---

## Summary

Diese Phase vervollständigt das View-System (Pflichtenheft §8.1 Nr. 3) durch:

1. **Verschieben** von `TConsoleBuffer` und `TConsoleCell` aus `TuiVision.Drivers.Console` nach `TuiVision.Core` (Architektur-Fix; Constitution IV).
2. **Erweitern** von `TView` um `Owner`, `Next` (internal), `Draw()` und `DrawView()`.
3. **Implementieren** von `TGroup` als zirkuläre Kind-Listen-Container-Klasse mit Drei-Phasen-Event-Dispatching, Fokus-Management, Zeichenpuffer und State-Propagation.

Alle 18 Functional Requirements (FR-001 bis FR-018) aus `spec.md` werden durch TDD abgedeckt.

---

## Technical Context

**Language/Version**: C# `latest` (C# 14) / .NET 10 (`net10.0`)
**Primary Dependencies**: TuiVision.Core (TPoint, TRect, TEvent, TObject, TConsoleBuffer ← verschoben)
**Storage**: N/A
**Testing**: MSTest; min. 70% Line Coverage in `TuiVision.Controls`
**Target Platform**: macOS, Linux, Windows (.NET 10 SDK — kein nativer Zusatz)
**Project Type**: Framework-Bibliothek
**Performance Goals**: Keine expliziten Latenzziele; interaktive TUI-Responsivität
**Constraints**: Kein P/Invoke; kein Natives; `Controls` darf nur `Core` referenzieren
**Scale/Scope**: 2 neue Typen (TGroup, DrawPhase), 4 neue TView-Mitglieder, ~20 neue Tests

---

## Constitution Check

*GATE: Muss bestehen, bevor Phase 0. Nochmals nach Phase 1 prüfen.*

| Prinzip | Status | Anmerkung |
|---|---|---|
| I. Managed-Only Runtime | ✅ PASS | Kein P/Invoke; alles managed .NET 10 |
| II. TDD (NON-NEGOTIABLE) | ✅ REQUIRED | Red→Green→Refactor; Commit-Sequenz in research.md dokumentiert |
| III. Didactic & Linguistic Clarity | ✅ REQUIRED | Bilingual DE/EN B2 XML-Docs auf allen neuen/geänderten Membern |
| IV. Modular Architecture | ✅ RESOLVED | Ursprüngliche Verletzung (Controls→Drivers.Console) durch Verschieben von TConsoleBuffer nach Core behoben; siehe research.md Decision 1 |
| V. Cross-Platform Portability | ✅ PASS | Kein OS-spezifischer Code in Core oder Controls |
| VI. License & Disclaimer Integrity | ✅ PASS | Neuer Code unter MIT; tv203s unverändert |

**Constitution IV — Complexity Tracking**:

| Verletzung (vor Auflösung) | Warum aufgelöst | Ablehnung der Alternative |
|---|---|---|
| `Controls → Drivers.Console` für TConsoleBuffer | TConsoleBuffer ist reine Datenstruktur (2D-Array); Rendering bleibt in Drivers.Console | Interface IDrawBuffer: Overhead ohne Mehrwert; TConsoleBuffer ist kein austauschbarer Treiber |

---

## Project Structure

### Dokumentation (diese Phase)

```text
specs/001-view-system-tgroup/
├── plan.md              ← dieses Dokument
├── research.md          ← Phase 0: 5 Architektur-Entscheidungen
├── data-model.md        ← Phase 1: Entitäten und Beziehungen
├── quickstart.md        ← Phase 1: Nutzungsbeispiele
└── tasks.md             ← Phase 2 (/speckit.tasks — noch nicht erstellt)
```

### Quellcode (Repository Root)

```text
src/
  TuiVision.Core/
    TPoint.cs                    (unverändert)
    TRect.cs                     (unverändert)
    TEvent.cs                    (unverändert)
    TObject.cs                   (unverändert)
    TConsoleCell.cs              ← NEU (aus Drivers.Console verschoben)
    TConsoleBuffer.cs            ← NEU (aus Drivers.Console verschoben)

  TuiVision.Controls/
    TView.cs                     ← ERWEITERT (Owner, Next internal, Draw, DrawView)
    TGroup.cs                    ← NEU (FR-001–FR-018)
    DrawPhase.cs                 ← NEU (internal enum; FR-006)

  TuiVision.Drivers.Console/
    TConsoleDriver.cs            ← AUFGETEILT (war Class1.cs; enthält nur TConsoleDriver + IConsolePresenter)

tests/
  TuiVision.Core.Tests/
    Test1.cs                     (CorePortTests — unverändert)
    TConsoleBufferTests.cs       ← NEU (verschobene Klassen; bisherige Treiber-Tests migriert)

  TuiVision.Controls.Tests/
    Test1.cs                     (TViewPortTests — unverändert)
    TViewExtendedTests.cs        ← NEU (Owner, Draw, DrawView; FR-005, FR-011, FR-012)
    TGroupTests.cs               ← NEU (FR-001–FR-018 vollständig)

  TuiVision.Drivers.Tests/
    Test1.cs                     ← AKTUALISIERT (TConsoleBuffer-Import auf Core; TConsoleDriver-Tests unverändert)
```

**Structure Decision**: Option 1 (Single project). Alle Quellen und Tests befinden sich
in den bestehenden 5 Modulen; keine neuen Projekte. `TConsoleBuffer` wechselt das Modul
(Drivers.Console → Core); keine neuen Module entstehen.

---

## Phase 0: Forschungsergebnisse

Alle NEEDS CLARIFICATION aus dem Technical Context sind aufgelöst. Vollständige Dokumentation
in `research.md`. Zusammenfassung:

| Entscheidung | Ergebnis |
|---|---|
| TConsoleBuffer Modul | Nach `TuiVision.Core` verschoben (Constitution IV Compliance) |
| Kind-Listen-Datenstruktur | Zirkuläre doppelt-verlinkte Liste (wie C++-Original) |
| DrawPhase Sichtbarkeit | `internal enum` |
| TView.Next Sichtbarkeit | `internal` property |
| ShutDown-Iterationsreihenfolge | Rückwärts (LIFO, wie Original) |

---

## Phase 1: Design-Entscheidungen

### 1.1 TConsoleBuffer/TConsoleCell — Migration

- Neue Dateien `TConsoleCell.cs` und `TConsoleBuffer.cs` in `TuiVision.Core/`
- `TuiVision.Drivers.Console/Class1.cs` → zwei neue Dateien: `TConsoleDriver.cs` (nur Treiber)
- `TuiVision.Drivers.Console.csproj`: Projektreferenz auf `TuiVision.Core` bleibt
- Alle bestehenden Tests in `TuiVision.Drivers.Tests` passen nur den Using-Import an

### 1.2 TView — Neue Mitglieder

| Mitglied | Design-Entscheidung |
|---|---|
| `Owner` | `public TGroup? Owner { get; internal set; }` — öffentlich lesbar, intern schreibbar |
| `Next` | `internal TView? Next { get; set; }` — nur für TGroup zugänglich |
| `Draw()` | `public virtual void Draw()` — leere Basis; kein `base.Draw()` nötig |
| `DrawView()` | `public void Draw­View()` — ruft `Draw()` wenn `GetState(TViewState.Visible) && !IsDrawLocked()` |

*Hinweis*: `IsDrawLocked()` wird intern über `Owner?.IsLocked ?? false` implementiert.
`DrawView()` prüft also den Lock der Eigentümer-Gruppe, bevor es `Draw()` aufruft.

### 1.3 TGroup — Implementierungsdetails

#### Konstruktor
```
TGroup(TRect bounds):
  base(bounds)
  Options |= Selectable | Buffered
  EventMask = TEventKind.All (0xFFFF)
  _clip = GetExtent()
```

#### Insert-Algorithmus
```
Insert(view):
  GUARD: view == null → ArgumentNullException
  GUARD: view.Owner != null → ArgumentException (bereits Mitglied einer Gruppe)
  GUARD: view == this → ArgumentException
  view.Owner = this
  if _last == null:
    view.Next = view   // Selbst-Loop
    _last = view
  else:
    view.Next = _last.Next  // einfügen nach _last
    _last.Next = view
    _last = view
```

#### Remove-Algorithmus
```
Remove(view):
  GUARD: view == null → ArgumentNullException
  GUARD: view.Owner != this → ArgumentException
  // Vorgänger in der zirkulären Liste suchen
  prev = FindPrev(view)   // O(n) Traversal
  if view == view.Next:   // Einziges Element
    _last = null
  else:
    prev.Next = view.Next
    if _last == view: _last = prev
  view.Next = null
  view.Owner = null
  if Current == view: ResetCurrent()
```

#### HandleEvent — Drei-Phasen-Dispatch
```
HandleEvent(event):
  Phase = PreProcess
  ForEach(v => { if options.HasFlag(PreProcess) && eventMask matches: v.HandleEvent(event) })
  if event.What != Nothing:
    Phase = Focused
    if positional event (mouse):
      route to view containing mouse position
    else if focused event (keyboard):
      Current?.HandleEvent(event)
    else:
      ForEach(v => v.HandleEvent(event))
  if event.What != Nothing:
    Phase = PostProcess
    ForEach(v => { if options.HasFlag(PostProcess) && eventMask matches: v.HandleEvent(event) })
```

#### SelectNext-Algorithmus
```
SelectNext(forward):
  if _last == null: return
  start = Current ?? First()
  current = forward ? start.Next : FindPrev(start)
  loop (max n iterations to prevent infinite loop):
    if current.Options.HasFlag(Selectable)
       && current.GetState(Visible)
       && !current.GetState(Disabled):
      SetFocus(current); return
    current = forward ? current.Next : FindPrev(current)
    if current == start: break  // Volles zirkuläres Durchlaufen ohne Treffer
```

#### SetFocus-Algorithmus
```
SetFocus(view):
  GUARD: view == null → ArgumentNullException
  GUARD: view.Owner != this → ArgumentException
  if Current == view: return  // bereits fokussiert — No-Op
  Current?.SetState(Focused, false)
  Current = view
  Current.SetState(Focused, true)
```

#### SetState — Propagation
```
SetState(state, enable):
  base.SetState(state, enable)
  if state has (Active | Focused | Disabled):
    ForEach(v => v.SetState(state, enable))
```

### 1.4 Zeichenpuffer — Buffer-Lifecycle

```
OnBoundsChanged():
  if _buffer != null:
    _buffer = new TConsoleBuffer(Size.X, Size.Y)
  _clip = GetExtent()

Draw():
  if _buffer == null && Options.HasFlag(Buffered) && GetState(Exposed):
    _buffer = new TConsoleBuffer(Size.X, Size.Y)
  ForEach(v => { if v.GetState(Visible): v.DrawView() })

LockDraw():
  _lockFlag++

UnlockDraw():
  if _lockFlag > 0: _lockFlag--
  if _lockFlag == 0: DrawView()
```

---

## TDD-Commit-Plan

Gemäß Constitution II (NON-NEGOTIABLE): Jeder Implementierungs-Commit MUSS einem
vorangehenden Red-Commit folgen. Reihenfolge verbindlich:

```
1. test(red): move TConsoleBuffer to Core — failing imports
2. feat(green): move TConsoleCell + TConsoleBuffer to TuiVision.Core
3. test(red): TView Owner/Draw/DrawView
4. feat(green): TView Owner, Next (internal), Draw(), DrawView()
5. test(red): TGroup lifecycle (Insert/Remove/ShutDown)
6. feat(green): TGroup circular list + lifecycle
7. test(red): TGroup three-phase HandleEvent
8. feat(green): TGroup HandleEvent dispatch
9. test(red): TGroup focus (SelectNext/SetFocus)
10. feat(green): TGroup focus management
11. test(red): TGroup draw buffer (Draw/Lock/Unlock)
12. feat(green): TGroup buffer + lock mechanics
13. test(red): TGroup state propagation
14. feat(green): TGroup SetState propagation
15. refactor: documentation pass (bilingual XML, remarks, examples)
```

---

## Qualitätsgates (vor Merge nach main)

- [ ] `dotnet build --configuration Release` — 0 Warnungen/Fehler
- [ ] `dotnet test` — alle Tests grün (bestehende 19 + neue ~20)
- [ ] Line Coverage `TuiVision.Controls` ≥ 70 %
- [ ] `dotnet format --verify-no-changes` — keine Formatverstöße
- [ ] Alle öffentlichen und nicht-öffentlichen API-Mitglieder mit XML-Docs (bilingual DE/EN, CEFR B2)
- [ ] `docfx docfx.json` (wenn vorhanden) fehlerfrei ausführbar
- [ ] Constitution IV: `Controls` referenziert nicht `Drivers.Console` (Projektdatei prüfen)
