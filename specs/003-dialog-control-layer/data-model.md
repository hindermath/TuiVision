# Data Model: Dialog-/Control-Schicht (003-dialog-control-layer)

**Phase**: 1 — Design & Contracts
**Date**: 2026-03-21
**Spec**: [spec.md](spec.md) | **Research**: [research.md](research.md)

---

## Klassendiagramm (Überblick / Overview)

```
TView (existing)
├── TStaticText
├── TLabel               → links to TView (peer)
├── TScrollBar
├── TScroller            → optional TScrollBar (H + V)
├── TButton              TButtonFlags { bfNormal, bfDefault, bfLeftJustify }
├── TInputLine
├── TCluster             → string[] Items, uint Value
│   ├── TCheckBoxes
│   └── TRadioButtons
└── TListViewer          → optional TScrollBar (V + H)
    └── TListBox         → TStringList?

TGroup (existing, : TView)
└── TDialog

TStringList              (standalone, no TView inheritance)
```

---

## Entitäten / Entities

### TStringList

Eigenständige, managed-code-Liste für String-Daten. Dient als Datenmodell für `TListBox`.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `_items` | `List<string>` (private) | Interne String-Sammlung / Internal string collection |
| `Count` | `int` (read-only property) | Anzahl der Einträge / Number of entries |
| `this[int index]` | `string` (indexer) | Zugriff per Index (0-basiert) / Index access (0-based) |

**State transitions**: Nur `Add` und `Clear` ändern den Zustand.
Invariante: `Count >= 0`; `this[i]` wirft `ArgumentOutOfRangeException` für `i < 0` oder `i >= Count`.

---

### TScrollBar

Visueller Scroll-Indikator; koppelt sich an eine scrollbare View.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Value` | `int` | Aktuelle Scroll-Position (0 ≤ Value ≤ Max) |
| `Max` | `int` | Maximaler Scroll-Wert (0 = keine Scroll-Kapazität) |
| `PgStep` | `int` | Schrittzahl für Seite-Rauf/Runter / Step size for page up/down |
| `ArStep` | `int` | Schrittzahl für Pfeiltaste / Step size for arrow key |
| `Horizontal` | `bool` | `true` = horizontal, `false` = vertikal |

**State transitions**:
- `ScrollTo(int pos)`: setzt `Value = Clamp(pos, 0, Max)` → sendet Scroll-Event an Owner.
- `SetLimit(int max)`: setzt `Max`; berechnet `PgStep` aus Sichtbereich neu.

Invariante: `0 ≤ Value ≤ Max`, `Max ≥ 0`.

---

### TScroller

Abstrakte Basis für scrollbare Views mit koordinierten Scrollbars.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Delta` | `TPoint` | Aktueller Scroll-Offset (X=horizontal, Y=vertikal) |
| `Limit` | `TPoint` | Maximaler Scroll-Offset (exklusiv) |
| `HScrollBar` | `TScrollBar?` | Optionale horizontale Scrollbar |
| `VScrollBar` | `TScrollBar?` | Optionale vertikale Scrollbar |

**State transitions**:
- `ScrollTo(TPoint delta)`: setzt `Delta = Clamp(delta, TPoint.Zero, Limit - ViewSize)`.
- Beide Scrollbars werden bei `ScrollTo` synchronisiert.

---

### TStaticText

Nicht-interaktives Textlabel; nimmt keinen Fokus an.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Text` | `string` | Anzuzeigender Text (kann `\n`-Umbrüche enthalten) |

Optionen: `TViewOptions.PreProcess` und `TViewOptions.PostProcess` sind **nicht** gesetzt
(kein Ereignis-Handling).

---

### TLabel

Beschriftung mit Tastaturkürzel; leitet Fokus an ein verknüpftes Control weiter.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Text` | `string` | Beschriftungstext; Kürzel durch `~` markiert (z. B. `"~N~ame:"`) |
| `Link` | `TView?` | Verknüpftes Peer-Control, das den Fokus erhält |
| `Light` | `bool` | `true` wenn das verknüpfte Control fokussiert ist (Hervorhebung) |

**State transitions**:
- `HandleEvent(evKeyDown, Alt+Kürzel)`: setzt Fokus auf `Link`; markiert Ereignis als verarbeitet.

---

### TButton

Schaltfläche mit Beschriftung, Command-ID und Default-Button-Unterstützung.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Title` | `string` | Schaltflächen-Beschriftung (Kürzel durch `~` markiert) |
| `Command` | `ushort` | Auszulösende Command-ID bei Aktivierung |
| `Flags` | `TButtonFlags` | `bfNormal`, `bfDefault`, `bfLeftJustify` (Flags-Enum) |
| `AmDefault` | `bool` | `true` wenn dieser Button momentan als Default markiert ist |

**TButtonFlags-Enum**:
```
[Flags]
enum TButtonFlags : byte {
    bfNormal      = 0x00,  // Standard-Button
    bfDefault     = 0x01,  // Default-Button (aktiviert durch Enter)
    bfLeftJustify = 0x02   // Beschriftung linksbündig
}
```

**State transitions**:
- Aktivierung (Enter/Space/Alt+Kürzel/Mausklick): sendet Command-Event an Besitzer-Group.
- Default-Markierung: `TViewState.Default` wird gesetzt/gelöscht durch `TDialog`.

---

### TInputLine

Einzeiliges Texteingabefeld.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Data` | `string` | Aktueller Eingabe-Inhalt |
| `MaxLen` | `int` | Maximale Zeichenanzahl (0 = gesperrt) |
| `CurPos` | `int` | Cursor-Position (0-basiert, 0 ≤ CurPos ≤ Data.Length) |
| `FirstPos` | `int` | Erster sichtbarer Zeichenindex (für horizontales Scrolling) |
| `InsertMode` | `bool` | `true` = Einfügemodus, `false` = Überschreibmodus |

**State transitions**:
- Zeichen eingeben: wenn `Data.Length < MaxLen` → `Data.Insert(CurPos, char)`, `CurPos++`.
- Backspace: wenn `CurPos > 0` → `Data.Remove(CurPos-1, 1)`, `CurPos--`.
- Delete: wenn `CurPos < Data.Length` → `Data.Remove(CurPos, 1)`.
- Pfeiltaste links/rechts: `CurPos = Clamp(CurPos ± 1, 0, Data.Length)`.
- Pos1 / Ende: `CurPos = 0` / `CurPos = Data.Length`.
- Ins: `InsertMode = !InsertMode`.

Invariante: `0 ≤ CurPos ≤ Data.Length ≤ MaxLen`; `0 ≤ FirstPos ≤ CurPos`.

---

### TCluster (abstrakt)

Abstrakte Basis für gruppierte Auswahl-Controls.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Items` | `string[]` | Array der Beschriftungen (fest zur Instanziierungszeit) |
| `Value` | `uint` | Zustandswert: Bitmask (CheckBoxes) oder Index (RadioButtons) |
| `Sel` | `int` | Aktuell markierte Option im Cluster (für Pfeiltasten-Navigation) |

Abstrakte Methoden: `Mark(int item)`, `Press(int item)` — Implementierung in Unterklassen.

---

### TCheckBoxes

Mehrfachauswahl-Gruppe; erbt von `TCluster`.

- `Value` ist Bitmask: Bit `i` gesetzt ⟺ Option `i` ausgewählt.
- `Mark(i)`: gibt `(Value & (1u << i)) != 0` zurück.
- `Press(i)`: toggelt Bit `i` in `Value`.

**State transitions**: Unabhängige Optionen; keine gegenseitige Beeinflussung.

---

### TRadioButtons

Einfachauswahl-Gruppe; erbt von `TCluster`.

- `Value` ist der **Index** der ausgewählten Option (0-basiert).
- `Mark(i)`: gibt `Value == (uint)i` zurück.
- `Press(i)`: setzt `Value = (uint)i`.

**State transitions**: Auswahl einer Option deselektiert automatisch alle anderen.

---

### TListViewer (abstrakt)

Abstrakte Basis für Listenansichten.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `NumCols` | `int` | Anzahl der Spalten (1 für Standardlisten) |
| `TopItem` | `int` | Index des ersten sichtbaren Eintrags |
| `FocusedItem` | `int` | Index des fokussierten Eintrags |
| `VScrollBar` | `TScrollBar?` | Optionale vertikale Scrollbar |
| `HScrollBar` | `TScrollBar?` | Optionale horizontale Scrollbar |

Abstrakte Methoden: `int GetNumItems()`, `string GetText(int item, int maxChars)`.

**State transitions**:
- `FocusItem(int item)`: setzt `FocusedItem = Clamp(item, 0, GetNumItems()-1)`; aktualisiert `TopItem` und Scrollbars.
- Scrollbars werden nach jedem `FocusItem`-Aufruf synchronisiert.

Invariante: `0 ≤ TopItem ≤ FocusedItem < GetNumItems()` (wenn Liste nicht leer); `0 ≤ TopItem` (wenn Liste leer).

---

### TListBox

Konkrete Listenansicht; erbt von `TListViewer`.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `List` | `TStringList?` | Datenquelle (null = leere Liste) |
| `Selection` | `int` | Index des zuletzt bestätigten Eintrags (nach expliziter Auswahlbestätigung im Control, z. B. per Doppelklick) |

- `GetNumItems()`: gibt `List?.Count ?? 0` zurück.
- `GetText(i, max)`: gibt `List?[i][..max]` zurück.
- Doppelklick auf einen sichtbaren Eintrag setzt `Selection` auf den angeklickten
  Index und bestätigt die Auswahl ohne separates zusätzliches Command-Ereignis.

---

### TDialog

Modales Dialogfenster; erbt von `TGroup`.

| Feld / Field | Typ / Type | Beschreibung / Description |
|---|---|---|
| `Title` | `string?` | Fenstertitel (angezeigt im oberen Rahmen) |
| `_result` | `ushort` (private) | Ergebnis-Command-ID (gesetzt beim Schließen) |
| `_running` | `bool` (private) | `true` während `Run()` den Event-Loop ausführt |

**State Machine**:
```
[Geschlossen/Closed]
    │  Run() aufgerufen / Run() called
    ▼
[Offen+Modal/Open+Modal]  ← HandleEvent verarbeitet Events / processes events
    │  CloseDialog(cmd) aufgerufen / called
    ▼
[Geschlossen/Closed]  → Run() gibt cmd zurück / returns cmd
```

**Schlüsseloperationen**:
- `Run() → ushort`: öffnet Dialog modal, startet inneren Event-Loop, gibt `_result` zurück.
- `CloseDialog(ushort cmd)`: setzt `_result = cmd`, beendet Event-Loop.
- Tab-Navigation: delegiert an `TGroup.HandleEvent()` (Wrap-around durch zirkuläre Kind-Liste).
- Default-Button: sucht bei unverarbeiteten Enter-Events nach Kind-View mit `TViewState.Default`.
- Escape: ruft `CloseDialog(cmCancel)` auf, sofern kein Kind-Control das
  Escape-Ereignis vorher konsumiert.
