# Research: View-System Phase 3 — TGroup, Zeichenpuffer, Fokus/States

**Branch**: `001-view-system-tgroup` | **Phase**: 0 — Research | **Date**: 2026-03-16

---

## Architectural Decision 1: Wo lebt TConsoleBuffer?

**Problem**: FR-014 erfordert, dass `TGroup` einen internen Zeichenpuffer hält.
Der bestehende `TConsoleBuffer` liegt in `TuiVision.Drivers.Console`.
`TuiVision.Controls` darf laut Constitution IV nur von `TuiVision.Core` abhängen.
Eine direkte Referenz `Controls → Drivers.Console` würde dieses Gate verletzen.

**Decision**: `TConsoleBuffer` und `TConsoleCell` werden aus `TuiVision.Drivers.Console`
nach `TuiVision.Core` verschoben.

**Rationale**: `TConsoleBuffer` ist eine reine Datenstruktur (2D-Array aus `TConsoleCell`-Werten).
Sie enthält keine Rendering-Logik; das eigentliche Rendern übernimmt `TConsoleDriver`.
Datenstrukturen gehören zu `TuiVision.Core`; Rendering-Treiber bleiben in `TuiVision.Drivers.Console`.
Diese Zuordnung entspricht dem Originaldesign: Turbo Vision trennt Buffer-Verwaltung (`ushort* buffer`)
von der plattformspezifischen Ausgabe (DOS/Linux-Treiber).

**Alternatives Considered**:
- Interface `IDrawBuffer` in Core, `TConsoleBuffer` in Drivers → Overhead ohne Nutzen;
  `TConsoleBuffer` ist kein austauschbarer Treiber sondern ein stabiler Datentyp.
- Dependency `Controls → Drivers.Console` erlauben → verletzt Constitution IV; abgelehnt.

**Impact**: `TuiVision.Drivers.Console/Class1.cs` wird aufgeteilt:
- `TConsoleCell` → `TuiVision.Core/TConsoleCell.cs`
- `TConsoleBuffer` → `TuiVision.Core/TConsoleBuffer.cs`
- `IConsolePresenter`, `TConsoleDriver` bleiben in `TuiVision.Drivers.Console/`
- Bestehende Tests in `TuiVision.Drivers.Tests` referenzieren dann `TuiVision.Core`; keine Logikänderung.

---

## Architectural Decision 2: Datenstruktur der Kind-Liste in TGroup

**Problem**: Wie verwaltet `TGroup` seine Kind-Views intern?

**Decision**: Zirkuläre doppelt-verlinkte Liste, analog zum C++-Original.
`TView` erhält zwei interne Felder: `TView? _next` und eine `Owner`-Eigenschaft (`TGroup?`).
Das Anker-Feld in `TGroup` ist `_last` (nullable; `null` = leere Gruppe).

**Rationale**: Das C++-Original verwendet diese Struktur für alle Gruppen-Operationen
(forEach, selectNext, insertBefore, drawSubViews). Eine 1:1-Übernahme sichert
Verhaltenskonformität und macht Port-Entscheidungen im Commit-Log nachvollziehbar.
Eine `List<TView>` hätte andere Iterator-Invalidierungs-Semantik und würde
den didaktischen Vergleich zum Original erschweren.

**Alternatives Considered**:
- `List<TView>` → einfacher, aber abweichend vom Original; Index-basiertes Lookup ändert Semantik.
- `LinkedList<TView>` (BCL) → verringert Kontrolle über die zirkuläre Traversal-Logik.

**Traversal-Konventionen**:
- `First()` → `_last?._next` (erster Eintrag)
- `ForEach(action)` → iteriert von First() bis _last, pre-fetcht `_next` für sichere Mutation
- Leer: `_last == null`

---

## Architectural Decision 3: Draw-Phase Enum Sichtbarkeit

**Problem**: `phFocused`, `phPreProcess`, `phPostProcess` steuern das Event-Dispatching intern.
Sollen sie public oder internal sein?

**Decision**: `internal enum DrawPhase` in `TuiVision.Controls`.

**Rationale**: Die Phase ist ein Implementierungsdetail des Dispatching-Mechanismus.
Kein externer Konsument muss sie kennen. `internal` schützt vor versehentlicher
Abhängigkeit von der Reihenfolge der Enum-Werte.

---

## Architectural Decision 4: Sichtbarkeit von _next in TView

**Problem**: `TGroup` muss die `_next`-Verknüpfung von `TView` setzen.
Soll `_next` public, internal oder protected sein?

**Decision**: `internal TView? Next` in `TView` (internal property, get/set).

**Rationale**: Nur `TGroup` (im selben Assembly `TuiVision.Controls`) benötigt Zugriff.
`internal` wahrt die Invariante, dass die Liste ausschließlich von `TGroup` mutiert wird,
ohne die öffentliche API von `TView` zu belasten.

---

## Architectural Decision 5: ShutDown-Iterationsreihenfolge

**Problem**: In welcher Reihenfolge fährt `TGroup.ShutDown()` die Kinder herunter?

**Decision**: Rückwärts (von `_last` rückwärts über `Prev`), wie im C++-Original.

**Rationale**: Das Original iteriert von `last` rückwärts (`p->prev()`), um die
zuletzt eingefügte View zuerst zu zerstören (LIFO). Dies verhindert Dangling-Pointer
bei Gruppen, die während ShutDown Ereignisse auslösen.

---

## Dependency Graph nach Refactoring

```
TuiVision.Core          (keine Abhängigkeiten)
  ├── TPoint, TRect, TEvent, TObject
  ├── TConsoleCell        ← NEU (verschoben)
  └── TConsoleBuffer      ← NEU (verschoben)

TuiVision.Controls      (→ Core)
  ├── TView               ← ERWEITERT (Owner, Draw, DrawView, internal Next)
  ├── TGroup              ← NEU
  └── DrawPhase (internal)← NEU

TuiVision.Drivers.Console (→ Core)
  ├── IConsolePresenter   (unverändert)
  └── TConsoleDriver      (unverändert; nutzt TConsoleBuffer aus Core)

TuiVision.Serialization  (→ Core) — unverändert
TuiVision.Compatibility  (→ Core) — unverändert
```

---

## TDD Commit-Sequenz (Red → Green → Refactor)

Gemäß Constitution Principle II MUSS die Commit-Sequenz im Git-Log sichtbar sein:

| Commit | Inhalt |
|---|---|
| `test(red): TConsoleBuffer in Core — failing tests` | Tests für verschobene Klassen |
| `feat(green): move TConsoleBuffer/TConsoleCell to Core` | Dateien verschieben, Tests grün |
| `test(red): TView Owner/Draw/DrawView — failing tests` | FR-005, FR-011, FR-012 Tests |
| `feat(green): add Owner, Draw, DrawView to TView` | Implementierung |
| `test(red): TGroup lifecycle (Insert/Remove/ShutDown) — failing tests` | FR-001–FR-004 Tests |
| `feat(green): TGroup lifecycle` | Implementierung |
| `test(red): TGroup event dispatch — failing tests` | FR-006, FR-007 Tests |
| `feat(green): TGroup three-phase HandleEvent` | Implementierung |
| `test(red): TGroup focus (SelectNext/SetFocus) — failing tests` | FR-008–FR-010, FR-018 Tests |
| `feat(green): TGroup focus management` | Implementierung |
| `test(red): TGroup draw buffer (Draw/LockDraw) — failing tests` | FR-013–FR-016 Tests |
| `feat(green): TGroup draw and buffer` | Implementierung |
| `test(red): TGroup state propagation — failing tests` | FR-017 Tests |
| `feat(green): TGroup SetState propagation` | Implementierung |
| `refactor: TGroup/TView cleanup + docs pass` | Bilinguales XML, Refactoring |
