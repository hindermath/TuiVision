# Research: Dialog-/Control-Schicht (003-dialog-control-layer)

**Phase**: 0 — Research & Unknowns Resolution
**Date**: 2026-03-21
**Spec**: [spec.md](spec.md)

---

## Entscheidungen / Decisions

### R-001: TDialog — Ausführungsmodell (Execution Model)

**Decision**: `TDialog.Run()` blockiert synchron den aufrufenden Thread, bis der Dialog
durch eine Command-ID geschlossen wird. Das Dialogfenster führt dabei einen eigenen
inneren Event-Loop aus, der Events aus dem übergeordneten TuiVision-Event-System entgegennimmt.

**Decision (EN)**: `TDialog.Run()` synchronously blocks the calling thread until the dialog
is closed with a command ID. The dialog runs an inner event loop that receives events from
the parent TuiVision event system.

**Rationale**: Entspricht exakt dem Verhalten von `TGroup::execView()` im Turbo-Vision-Original
(`tgroup.cc`, Zeile ~450). Ermöglicht sequenzielle Aufruflogik (`ushort result = dialog.Run()`)
ohne Callbacks oder Task-basierte Asynchronität. Bereits in Clarify-Session bestätigt (Q1).

**Rationale (EN)**: Matches `TGroup::execView()` in the original (`tgroup.cc`, ~line 450).
Enables sequential call logic (`ushort result = dialog.Run()`) without callbacks or
task-based async. Confirmed in clarify session (Q1).

**Alternatives considered**:
- Event-driven non-blocking callback model: rejected — overcomplicates consumer code,
  breaks Turbo Vision compatibility contract.
- Task<ushort>-based async: rejected — .NET async requires `await` propagation throughout
  the entire call stack; incompatible with synchronous TUI event pump.

---

### R-002: TButton — Default-Button-Flag

**Decision**: `TButton` definiert ein eigenes `TButtonFlags`-Enum (`bfNormal`, `bfDefault`,
`bfLeftJustify`) analog zu den originalen `bf`-Konstanten aus `button.h`. Das `bfDefault`-Flag
setzt zusätzlich `TViewState.Default` auf der View — was bereits in `TViewState` (0x400)
definiert ist. `TDialog.HandleEvent()` prüft bei Enter-Events den Default-Button und aktiviert
ihn, wenn das fokussierte Control Enter nicht konsumiert hat.

**Decision (EN)**: `TButton` defines its own `TButtonFlags` enum (`bfNormal`, `bfDefault`,
`bfLeftJustify`) matching the original `bf` constants from `button.h`. The `bfDefault` flag
additionally sets `TViewState.Default` on the view — which is already defined in `TViewState`
(0x400). `TDialog.HandleEvent()` checks for the default button on Enter events and activates
it when the focused control has not consumed Enter.

**Rationale**: `TViewState.Default` ist in der bestehenden Codebasis bereits vorhanden und
konsistent mit dem Original. Ein separates `TButtonFlags`-Enum ist trotzdem sinnvoll, weil
es buttonspezifische Flags (LeftJustify) sauber von allgemeinen View-States trennt.

**Alternatives considered**:
- Nur `TViewState.Default` verwenden ohne eigenes Enum: zu wenig ausdrucksstark für
  button-spezifische Flags wie `bfLeftJustify`.

---

### R-003: Fokus-Navigation — Wrap-around in TGroup/TDialog

**Decision**: `TDialog` erbt die Tab-Navigation von `TGroup`. `TGroup.NextView()`/`PrevView()`
implementieren bereits zirkuläres Durchlaufen der Kind-Liste (Wrap-around). `TDialog` muss
Tab/Shift-Tab-Events an `TGroup.HandleEvent()` delegieren, ohne dieses Verhalten zu überschreiben.

**Decision (EN)**: `TDialog` inherits Tab navigation from `TGroup`. `TGroup.NextView()` /
`PrevView()` already implement circular traversal of the child list (wrap-around). `TDialog`
must delegate Tab/Shift-Tab events to `TGroup.HandleEvent()` without overriding this behaviour.

**Rationale**: Die zirkuläre Kindliste in `TGroup` (`_last.Next == First()` — Invariante aus
dem bestehenden Code) macht Wrap-around strukturell natürlich. Confirmed in clarify (Q2).

---

### R-004: TScrollBar — Kopplung (Optional vs. verpflichtend)

**Decision**: `TScrollBar` ist für `TListBox` **optional** (nullable). Eine `TListBox` ohne
Scrollbar ist vollständig funktionsfähig; sie zeigt nur keinen visuellen Scroll-Indikator.
`TListViewer` hält `TScrollBar?`-Referenzen (`VScrollBar`, `HScrollBar`).

**Decision (EN)**: `TScrollBar` is **optional** (nullable) for `TListBox`. A `TListBox`
without a scrollbar is fully functional; it simply displays no visual scroll indicator.
`TListViewer` holds `TScrollBar?` references (`VScrollBar`, `HScrollBar`).

**Rationale**: Entspricht dem Original (`tlistvie.cc`, `scrollBar` kann `null` sein). Maximale
Flexibilität für Entwickler; vermeidet Zwang zu visuell überfüllten Dialogen.

**Alternatives considered**:
- Verpflichtende Scrollbar: zu restriktiv; original erlaubt `null`.

---

### R-005: CommandID-Typ

**Decision**: Command-IDs werden als `ushort` dargestellt — konsistent mit
`TEvent.Command` im bestehenden TuiVision-Code. `TDialog.Run()` gibt `ushort` zurück.
Vordefinierte Werte: `cmOK = 10`, `cmCancel = 11`, `cmYes = 12`, `cmNo = 13`
(aus `ShellCommandIds.cs` bzw. Standard-Turbo-Vision-Konstanten).

**Decision (EN)**: Command IDs are represented as `ushort` — consistent with `TEvent.Command`
in the existing TuiVision code. `TDialog.Run()` returns `ushort`. Predefined values:
`cmOK = 10`, `cmCancel = 11`, `cmYes = 12`, `cmNo = 13`.

**Rationale**: Typ-Konsistenz mit dem bestehenden Event-System ist zwingend erforderlich,
um ohne Casts zwischen Controls und dem Event-Dispatcher zu arbeiten.

---

### R-006: TStringList — Implementierungsmodell

**Decision**: `TStringList` wird als einfache, managed-code-Liste über `List<string>` implementiert.
Sie stellt die Schnittstelle `int Count`, `string this[int index]`, `void Add(string)`,
`void Clear()` bereit. Im Gegensatz zum originalen TStringList (das serialisiert werden konnte)
ist die Serialisierung für Phase 5 nicht erforderlich — das ist Aufgabe von
`TuiVision.Serialization` in Phase 6.

**Decision (EN)**: `TStringList` is implemented as a simple managed-code list over `List<string>`.
It exposes `int Count`, `string this[int index]`, `void Add(string)`, `void Clear()`.
Unlike the original TStringList (which supported serialisation), serialisation is not required
for Phase 5 — that is `TuiVision.Serialization`'s responsibility in Phase 6.

**Rationale**: YAGNI — nur was für Phase 5 gebraucht wird; kein Over-Engineering.

---

### R-007: TCluster — Zustandsdarstellung

**Decision**: `TCluster` speichert den Auswahlzustand als `uint Value` (Bitmask für
`TCheckBoxes`; Index für `TRadioButtons`). Die Item-Beschriftungen werden als `string[]`
(fest zur Instanziierungszeit) gehalten. Das Enum `TClusterFlags` wird für Flag-Werte
analog zu den originalen `sf`-Konstanten für Cluster eingeführt.

**Decision (EN)**: `TCluster` stores selection state as `uint Value` (bitmask for
`TCheckBoxes`; index for `TRadioButtons`). Item labels are held as `string[]` (fixed
at instantiation time). `TClusterFlags` enum is introduced for flag values analogous
to the original `sf` constants for clusters.

---

### R-008: Render-Zustände (Visual States) pro Control

Alle Controls müssen mindestens diese vier visuellen Zustände korrekt in den
TuiVision-Consolenbuffer rendern (SC-005):

| Zustand / State | TViewState-Flag |
|---|---|
| Normal | — (kein Flag) |
| Focused | `sfFocused` (`TViewState.Focused`) |
| Disabled | `sfDisabled` (`TViewState.Disabled`) |
| Selected | `sfSelected` (`TViewState.Selected`) |

Controls lesen diese Flags in ihrer `Draw()`-Methode aus und wählen den passenden Zeichensatz.
Die Farb-/Zeichensatz-Auswahl erfolgt über `GetPalette()` / Palette-Index, analog zum Original.

---

### R-009: Implementierungsreihenfolge (Abhängigkeitsanalyse)

Empfohlene Portierungs-Reihenfolge nach Abhängigkeiten (jede Klasse baut auf den
vorangehenden auf):

| Schritt | Klasse | Abhängigkeiten (neu) |
|---|---|---|
| 1 | `TStringList` | — |
| 2 | `TScrollBar` | — |
| 3 | `TScroller` | `TScrollBar` |
| 4 | `TStaticText` | — |
| 5 | `TCluster` | — |
| 6 | `TCheckBoxes` | `TCluster` |
| 7 | `TRadioButtons` | `TCluster` |
| 8 | `TLabel` | — (Link zu TView) |
| 9 | `TListViewer` | `TScrollBar` |
| 10 | `TListBox` | `TListViewer`, `TStringList` |
| 11 | `TButton` | — |
| 12 | `TInputLine` | — |
| 13 | `TDialog` | Alle vorherigen |

Für jede Klasse gilt: **Test zuerst (Red), dann Implementation (Green), dann Refactor**.

---

### R-010: TDD-Strategie für Controls (Konsistenzbedingung II)

Da die Controls keinen echten Terminal-Output produzieren (das macht `TuiVision.Drivers.Console`),
können Unit-Tests den Consolenbuffer direkt inspizieren:

- **Render-Tests**: Instanz des Controls erstellen → `Draw()` aufrufen → Consolenbuffer-Inhalt
  mit erwartetem Text/Farbe abgleichen (Snapshot-Ansatz).
- **Ereignis-Tests**: Fake-`TEvent`s (KeyDown, MouseDown) in `HandleEvent()` injizieren →
  Zustandsänderungen (Value, CurPos, FocusedItem) prüfen.
- **Negativ-Tests**: Ungültige Eingaben (leere Liste, MaxLen=0, Out-of-Bounds-Index) →
  kein Absturz, definierter Fehlerzustand.

Testklassen-Namenskonvention (Constitution §"Code Style"):
`TDialog_Run_ReturnsCommandIdOnClose`, `TButton_HandleEvent_ActivatesOnEnter`, etc.
