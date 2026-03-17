# Data Model: View-System Phase 3 — TGroup, Zeichenpuffer, Fokus/States

**Branch**: `001-view-system-tgroup` | **Phase**: 1 — Design | **Date**: 2026-03-16

---

## Entitäten / Entities

### 1. TConsoleCell (verschoben nach TuiVision.Core)

**Herkunft**: Bisher in `TuiVision.Drivers.Console`; wird nach `TuiVision.Core` verschoben.
**Origin**: Previously in `TuiVision.Drivers.Console`; moved to `TuiVision.Core`.

| Feld | Typ | Beschreibung / Description |
|---|---|---|
| `Glyph` | `char` | Das darzustellende Zeichen / The character to display |
| `Foreground` | `ConsoleColor` | Vordergrundfarbe / Foreground color |
| `Background` | `ConsoleColor` | Hintergrundfarbe / Background color |

- **Modellierungsregel**: `readonly record struct` (unveränderlich, wertbasierte Gleichheit)
- **Statisches Feld**: `TConsoleCell.Empty` → `new TConsoleCell(' ', ConsoleColor.Gray, ConsoleColor.Black)`

---

### 2. TConsoleBuffer (verschoben nach TuiVision.Core)

**Herkunft**: Bisher in `TuiVision.Drivers.Console`; wird nach `TuiVision.Core` verschoben.
**Origin**: Previously in `TuiVision.Drivers.Console`; moved to `TuiVision.Core`.

| Eigenschaft/Methode | Typ | Beschreibung / Description |
|---|---|---|
| `Width` | `int` (readonly) | Breite in Zeichen / Width in characters |
| `Height` | `int` (readonly) | Höhe in Zeilen / Height in rows |
| `this[int x, int y]` | `TConsoleCell` (get/set) | Indexer / Indexer |
| `GetCell(x, y)` | `TConsoleCell` | Lesen mit Bounds-Check / Read with bounds check |
| `SetCell(x, y, cell)` | `void` | Schreiben mit Bounds-Check / Write with bounds check |
| `TrySetCell(x, y, cell)` | `bool` | Schreiben mit Clip; kein Fehler außerhalb / Clipped write; no error outside |
| `Clear()` | `void` | Puffer mit TConsoleCell.Empty füllen / Fill with empty cell |
| `WriteText(x, y, text, fg, bg)` | `void` | Zeile schreiben / Write a line |
| `Clone()` | `TConsoleBuffer` | Snapshot / Snapshot |

- **Modellierungsregel**: `sealed class`
- **Invariante**: Width > 0, Height > 0 (im Konstruktor validiert)

---

### 3. TView — neue Mitglieder (TuiVision.Controls, bestehende Klasse erweitert)

| Mitglied | Typ | Sichtbarkeit | Beschreibung / Description |
|---|---|---|---|
| `Owner` | `TGroup?` | `public` (get); `internal` (set) | Rückwärts-Referenz auf Eigentümer-Gruppe / Back-reference to owning group |
| `Next` | `TView?` | `internal` (get/set) | Nächster Knoten in der zirkulären Kind-Liste / Next node in circular child list |
| `Draw()` | `virtual void` | `public` | Zeichenoperation; Basis ist No-Op / Drawing operation; base is no-op |
| `DrawView()` | `void` | `public` | Ruft Draw() wenn sichtbar / Calls Draw() when visible |

- **Invariante**: `Owner != null` ↔ View ist in einer Gruppe; wird von TGroup verwaltet.
- **Invariante**: `Next` bildet mit anderen Views eine zirkuläre Liste; einzelne View: `Next == this`.

---

### 4. TGroup (NEU — TuiVision.Controls, erbt TView)

#### Felder / Fields

| Feld | Typ | Sichtbarkeit | Beschreibung / Description |
|---|---|---|---|
| `_last` | `TView?` | `private` | Anker der zirkulären Kind-Liste (last inserted) / Anchor of circular child list |
| `_lockFlag` | `int` | `private` | Draw-Lock-Zähler / Draw lock counter |
| `_buffer` | `TConsoleBuffer?` | `private` | Optionaler Zeichenpuffer / Optional draw buffer |
| `_clip` | `TRect` | `private` | Schnittrechteck für Rendering / Clip rectangle for rendering |

#### Eigenschaften / Properties

| Eigenschaft | Typ | Beschreibung / Description                                    |
|---|---|---------------------------------------------------------------|
| `Current` | `TView?` | Aktuell fokussierte Kind-View.Initial: `null`; wird nicht automatisch bei `Insert` gesetzt. / Currently focused child view |
| `Phase` | `DrawPhase` | Interne Dispatch-Phase / Internal dispatch phase              |

#### Methoden / Methods

| Methode | Signatur | Beschreibung / Description |
|---|---|---|
| Konstruktor | `TGroup(TRect bounds)` | Initialisiert mit Selectable+Buffered, EventMask = 0xFFFF |
| Insert | `void Insert(TView view)` | Fügt Kind hinzu; ArgumentException bei Duplikat |
| Remove | `void Remove(TView view)` | Entfernt Kind; ArgumentException wenn nicht enthalten |
| First | `TView? First()` | Erster Knoten: `_last?.Next` |
| ForEach | `void ForEach(Action<TView> action)` | Sichere Traversal; pre-fetcht Next |
| HandleEvent | `override void HandleEvent(TEvent @event)` | Drei-Phasen-Dispatch |
| SetFocus | `void SetFocus(TView view)` | Setzt Fokus; ArgumentException bei Nicht-Kind |
| SelectNext | `void SelectNext(bool forward)` | Zirkuläre Tab-Navigation |
| SetState | `override void SetState(TViewState state, bool enable)` | Propagiert Active/Focused/Disabled |
| Draw | `override void Draw()` | Zeichnet alle sichtbaren Kinder in Z-Reihenfolge |
| LockDraw | `void LockDraw()` | Inkrementiert _lockFlag |
| UnlockDraw | `void UnlockDraw()` | Dekrementiert; bei 0 → DrawView() + ResetCursor |
| ShutDown | `override void ShutDown()` | Rückwärts-Traversal, alle Kinder herunterfahren |
| OnBoundsChanged | `override void OnBoundsChanged()` | Puffer neu allokieren |

---

### 5. DrawPhase (NEU — internal enum, TuiVision.Controls)

| Wert | Beschreibung / Description |
|---|---|
| `Focused` | Fokussierte View empfängt das Ereignis / Focused view receives the event |
| `PreProcess` | Views mit PreProcess-Option empfangen zuerst / PreProcess-option views receive first |
| `PostProcess` | Views mit PostProcess-Option empfangen zuletzt / PostProcess-option views receive last |

---

## Beziehungen / Relationships

```
TObject
  └── TView  ────────────────────────────────────┐
        ├── Owner: TGroup? (nullable)             │
        ├── Next: TView? (internal, circular list)│
        ├── Draw(): virtual                       │
        └── DrawView(): calls Draw() if visible   │
              ↑                                   │
        TGroup (inherits TView) ──────────────────┘
          ├── _last: TView? (list anchor)
          ├── Current: TView? (focused child)
          ├── _buffer: TConsoleBuffer? (from TuiVision.Core)
          ├── Insert(TView)  → sets view.Owner, view.Next
          ├── Remove(TView)  → clears view.Owner, view.Next
          └── ForEach(Action<TView>)

TConsoleCell  (TuiVision.Core — moved)
TConsoleBuffer (TuiVision.Core — moved)
  └── referenced by TGroup._buffer
```

---

## State-Transitions für TView in TGroup

```
[Inserted, Sichtbar]
       │ Show() / SetState(Visible, true)
       ▼
  [Visible]
       │ SetFocus(view) oder SelectNext
       ▼
  [Visible + Focused]
       │ SetState(Disabled, true)
       ▼
  [Visible + Disabled]     ← SelectNext überspringt diesen State
       │ Remove(view)
       ▼
  [Removed — Owner = null]
```

---

## Dateistruktur nach Refactoring

```text
src/
  TuiVision.Core/
    TPoint.cs                    (unverändert)
    TRect.cs                     (unverändert)
    TEvent.cs                    (unverändert)
    TObject.cs                   (unverändert)
    TConsoleCell.cs              ← NEU (verschoben aus Drivers.Console)
    TConsoleBuffer.cs            ← NEU (verschoben aus Drivers.Console)

  TuiVision.Controls/
    TView.cs                     ← ERWEITERT (Owner, Next, Draw, DrawView)
    TGroup.cs                    ← NEU
    DrawPhase.cs                 ← NEU (internal enum)

  TuiVision.Drivers.Console/
    TConsoleDriver.cs            ← UMBENANNT (war Class1.cs; enthält nur TConsoleDriver + IConsolePresenter)

tests/
  TuiVision.Core.Tests/
    Test1.cs                     (bestehende CorePortTests — unverändert)
    TConsoleBufferTests.cs       ← NEU (Tests für verschobene Klassen)

  TuiVision.Controls.Tests/
    Test1.cs                     (bestehende TViewPortTests — unverändert)
    TGroupTests.cs               ← NEU (FR-001 bis FR-018)
    TViewExtendedTests.cs        ← NEU (Draw, DrawView, Owner)

  TuiVision.Drivers.Tests/
    Test1.cs                     ← AKTUALISIERT (TConsoleBuffer-Import auf Core; ConsoleDriver-Tests bleiben)
```
