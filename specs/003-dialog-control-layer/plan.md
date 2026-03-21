# Implementation Plan: Dialog-/Control-Schicht (Dialog and Control Layer)

**Branch**: `003-dialog-control-layer` | **Date**: 2026-03-21 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `specs/003-dialog-control-layer/spec.md`

---

## Summary

Portierung der Dialog-/Control-Schicht von Turbo Vision 2.0.3 (C++) nach C#/.NET 10.
13 neue Klassen werden in `TuiVision.Controls` implementiert, beginnend mit unabhängigen
Basisklassen (`TStringList`, `TScrollBar`) und endend mit dem Dialog-Koordinator (`TDialog`).
Alle Klassen folgen dem TDD Red-Green-Refactor-Zyklus mit MSTest, erreichen ≥ 70 % Line Coverage
und tragen vollständige zweisprachige XML-Dokumentation. Die Spec präzisiert zusätzlich,
dass Escape `TDialog` standardmäßig mit `cmCancel` schließt und dass ein Doppelklick in
`TListBox` die aktuelle Auswahl bestätigt, ohne ein separates zusätzliches Command-Ereignis
auszulösen.

Porting of the dialog/control layer from Turbo Vision 2.0.3 (C++) to C#/.NET 10.
13 new classes are implemented in `TuiVision.Controls`, starting with independent base
classes (`TStringList`, `TScrollBar`) and finishing with the dialog coordinator (`TDialog`).
All classes follow the TDD Red-Green-Refactor cycle with MSTest, achieve ≥ 70% line coverage,
and carry complete bilingual XML documentation. The spec now additionally clarifies that
Escape closes `TDialog` with `cmCancel` by default and that a double-click in `TListBox`
confirms the current selection without emitting a separate additional command event.

---

## Technical Context

**Language/Version**: C# latest (C# 14) / .NET 10 (`net10.0`)
**Primary Dependencies**: `TuiVision.Core` (TView, TGroup, TEvent, TObject, TPoint, TRect,
TConsoleBuffer); `TuiVision.Compatibility` (Tastatur-Scan-Codes / keyboard scan codes)
**Storage**: N/A — in-memory UI state only; keine Persistenz in Phase 5
**Testing**: MSTest (exklusiv); Coverlet für Coverage-Messung
**Target Platform**: macOS, Linux, Windows (.NET 10 managed runtime, keine nativen Abhängigkeiten)
**Project Type**: Framework-Bibliothek (library) — Modul `TuiVision.Controls`
**Performance Goals**: Tastatur-Events werden innerhalb eines Event-Loop-Ticks verarbeitet
und gerendert; kein wahrnehmbares Lag (<16 ms pro Keystroke — Standard für TUI-Anwendungen)
**Constraints**: Kein P/Invoke; kein nativer Code; CS1591 nicht unterdrückt;
alle OS-Anpassungen ausschließlich in `TuiVision.Drivers.Console`; falls für dieses
Feature später doch projektinternes JSON in Tests, Hilfswerkzeugen oder
Begleitformaten benötigt wird, ist dafür `System.Text.Json` verbindlich und
`Newtonsoft.Json` nur mit dokumentierter Ausnahme zulässig
**Scale/Scope**: 13 neue Produktions-Klassen + 13 neue Test-Klassen; Coverage-Gate ≥ 70 %
auf dem gesamten Modul `TuiVision.Controls`

---

## Constitution Check

*GATE: Muss vor Phase-0-Research bestehen. Nachprüfung nach Phase-1-Design.*

| Prinzip / Principle | Status | Begründung / Rationale |
|---|---|---|
| **I. Managed-Only Runtime** | ✅ PASS | Alle 13 Klassen rendern über `TConsoleBuffer`; kein P/Invoke, kein nativer Code |
| **II. Test-First TDD** | ✅ PASS | Red-Green-Refactor pro Klasse; MSTest; 70 %-Gate auf `TuiVision.Controls` |
| **III. Didactic/Linguistic Clarity** | ✅ PASS | Zweisprachige XML-Doku (DE zuerst, EN zweite) für alle public **und** non-public Member; CEFR-B2 |
| **IV. Modular Architecture** | ✅ PASS | Alle 13 Klassen in `TuiVision.Controls`; Abhängigkeit nur auf `TuiVision.Core` |
| **V. Cross-Platform Portability** | ✅ PASS | Keine `#if`-Blöcke in Controls; OS-Adaptation bleibt in `Drivers.Console` |
| **VI. License & Disclaimer** | ✅ PASS | MIT für neuen Code; `tv203s/` unverändert; Disclaimer in README/LICENSE bleibt |

**Complexity Tracking**: Keine Verletzungen — kein Eintrag erforderlich.

**Post-Design Re-check**: Alle Prinzipien nach Phase 1 weiterhin erfüllt. `TDialog` erbt
von `TGroup` (bestehend in `TuiVision.Controls`) — keine neue Modul-Abhängigkeit.
`TStringList` ist keine `TView`-Subklasse (kein visuelles Rendering) — korrekt als
eigenständige Hilfsklasse ohne Modulgrenzen-Verletzung. Die Constitution-Ergänzung
zur JSON-Bibliothek ist derzeit ebenfalls erfüllt, weil dieses Feature keine
JSON-Schnittstelle definiert; eventuelle spätere JSON-Hilfsformate müssten
`System.Text.Json` verwenden.

---

## Project Structure

### Documentation (this feature)

```text
specs/003-dialog-control-layer/
├── plan.md              # Dieser Plan / This file
├── research.md          # Phase-0-Output (generiert / generated)
├── data-model.md        # Phase-1-Output (generiert / generated)
├── quickstart.md        # Phase-1-Output (generiert / generated)
├── contracts/
│   └── public-api.md    # Phase-1-Output (generiert / generated)
├── checklists/
│   └── requirements.md  # Spec-Qualitätsprüfung mit Durchführungshinweisen
└── tasks.md             # Phase-2-Output (/speckit.tasks — noch nicht erstellt)
```

### Source Code (repository root)

```text
src/TuiVision.Controls/
├── TStringList.cs        ← neu (Phase 5, Schritt 1)
├── TScrollBar.cs         ← neu (Phase 5, Schritt 2)
├── TScroller.cs          ← neu (Phase 5, Schritt 3)
├── TStaticText.cs        ← neu (Phase 5, Schritt 4)
├── TCluster.cs           ← neu (Phase 5, Schritt 5)
├── TCheckBoxes.cs        ← neu (Phase 5, Schritt 6)
├── TRadioButtons.cs      ← neu (Phase 5, Schritt 7)
├── TLabel.cs             ← neu (Phase 5, Schritt 8)
├── TListViewer.cs        ← neu (Phase 5, Schritt 9)
├── TListBox.cs           ← neu (Phase 5, Schritt 10)
├── TButton.cs            ← neu (Phase 5, Schritt 11; enthält TButtonFlags)
├── TInputLine.cs         ← neu (Phase 5, Schritt 12)
├── TDialog.cs            ← neu (Phase 5, Schritt 13)
│
│   [bestehend / existing]
├── TView.cs
├── TGroup.cs
├── TApplication.cs
├── TDesktop.cs
├── TMenuBar.cs
├── TMenuItem.cs
├── TProgram.cs
├── TStatusItem.cs
├── TStatusLine.cs
├── ShellCommandIds.cs    ← erweitern um cmOK/cmCancel/cmYes/cmNo
└── DrawPhase.cs

tests/TuiVision.Controls.Tests/
├── TStringListTests.cs   ← neu
├── TScrollBarTests.cs    ← neu
├── TScrollerTests.cs     ← neu
├── TStaticTextTests.cs   ← neu
├── TClusterTests.cs      ← neu
├── TCheckBoxesTests.cs   ← neu
├── TRadioButtonsTests.cs ← neu
├── TLabelTests.cs        ← neu
├── TListViewerTests.cs   ← neu
├── TListBoxTests.cs      ← neu
├── TButtonTests.cs       ← neu
├── TInputLineTests.cs    ← neu
└── TDialogTests.cs       ← neu
```

**Structure Decision**: Single-project-Struktur (Option 1). Alle neuen Klassen erweitern
das bestehende `TuiVision.Controls`-Projekt. Kein neues Projekt notwendig — entspricht
Constitution §IV (genau 5 Module).

---

## Phase 0: Research (abgeschlossen / completed)

Alle Unbekannten wurden aufgelöst. Details: [research.md](research.md)

| Unbekannte / Unknown | Entscheidung / Decision | Quelle |
|---|---|---|
| Dialog-Ausführungsmodell | Synchron blockierend (`TDialog.Run()`) | Clarify Q1 + R-001 |
| Fokus-Wrap in TDialog | Wrap-around (zirkuläre TGroup-Kindliste) | Clarify Q2 + R-003 |
| Default-Button-Flag | `TButtonFlags.bfDefault` + `TViewState.Default` | Clarify Q3 + R-002 |
| Escape-Standardwert | `cmCancel` | Clarify Q4 + R-011 |
| TScrollBar-Kopplung | Optional (nullable) für TListBox | R-004 |
| CommandID-Typ | `ushort` (konsistent mit TEvent.Command) | R-005 |
| TStringList-Modell | `List<string>` (managed, ohne Serialisierung) | R-006 |
| TCluster-Zustand | `uint Value` (Bitmask/Index) + `int Sel` | R-007 |
| Render-Zustände | Normal/Focused/Disabled/Selected via TViewState | R-008 |
| Implementierungsreihenfolge | 13 Schritte nach Abhängigkeitsgraph | R-009 |
| TDD-Strategie | Buffer-Inspektion + Event-Injection + Negativ-Tests | R-010 |
| TListBox-Doppelklick | Bestätigt Auswahl ohne separates Command | Clarify Q5 + R-012 |

---

## Phase 1: Design & Contracts (abgeschlossen / completed)

### Datenmodell / Data Model

Vollständige Entitätsdefinitionen mit State Transitions: [data-model.md](data-model.md)

**Schlüssel-Entscheidungen**:
- `TStringList` ist **keine** `TView`-Subklasse — reine Datenklasse.
- `TCluster` ist abstrakt; `TCheckBoxes` und `TRadioButtons` sind `sealed`.
- `TListViewer` ist abstrakt; `TListBox` konkret aber nicht `sealed` (für Phase 6 erweiterbar durch `TDirListBox`).
- `TDialog` erbt von `TGroup` (nicht von `TWindow`) — entspricht dem Original.
- `TButtonFlags.bfDefault` setzt zusätzlich `TViewState.Default` (0x400) — nutzt bestehenden State.
- Escape schließt `TDialog` standardmäßig mit `cmCancel`.
- `TListBox` bestätigt per Doppelklick nur die aktuelle Auswahl und sendet kein separates zusätzliches Command-Ereignis.

### Interface Contracts

Vollständige öffentliche API-Signaturen: [contracts/public-api.md](contracts/public-api.md)

### Quickstart

Entwicklungs-Leitfaden: [quickstart.md](quickstart.md)

---

## Implementierungsplan (Reihenfolge / Implementation Sequence)

### Schritt 1–4: Basisklassen ohne gegenseitige Abhängigkeiten

| Klasse | Testklasse | C++-Quelle |
|---|---|---|
| `TStringList` | `TStringListTests` | `tstrlist.cc` |
| `TScrollBar` | `TScrollBarTests` | `tscrollb.cc` |
| `TScroller` | `TScrollerTests` | `tscrolle.cc` |
| `TStaticText` | `TStaticTextTests` | `tstatict.cc` |

Pro Klasse mindestens: 1 Positiv-Test + 1 Negativ-/Grenzfall-Test.

### Schritt 5–8: Auswahl- und Beschriftungs-Controls

| Klasse | Testklasse | C++-Quelle | Abhängigkeit |
|---|---|---|---|
| `TCluster` (abstract) | `TClusterTests` | `cluster.h` | — |
| `TCheckBoxes` | `TCheckBoxesTests` | `tcheckbo.cc` | TCluster |
| `TRadioButtons` | `TRadioButtonsTests` | `tradiobu.cc` | TCluster |
| `TLabel` | `TLabelTests` | `tlabel.cc` | TView (existing) |

### Schritt 9–10: Listen-Hierarchie

| Klasse | Testklasse | C++-Quelle | Abhängigkeiten |
|---|---|---|---|
| `TListViewer` (abstract) | `TListViewerTests` | `tlistvie.cc` | TScrollBar |
| `TListBox` | `TListBoxTests` | `tlistbox.cc` | TListViewer, TStringList |

Für `TListBoxTests` zusätzlich absichern: Doppelklick bestätigt die angeklickte Auswahl,
löst aber kein separates zusätzliches Command-Ereignis aus.

### Schritt 11–12: Eingabe-Controls

| Klasse | Testklasse | C++-Quelle |
|---|---|---|
| `TButton` (+ `TButtonFlags`) | `TButtonTests` | `tbutton.cc` |
| `TInputLine` | `TInputLineTests` | `tinputli.cc` |

### Schritt 13: Dialog-Koordinator (letzter Schritt)

| Klasse | Testklasse | C++-Quelle | Abhängigkeiten |
|---|---|---|---|
| `TDialog` | `TDialogTests` | `tdialog.cc` | TGroup (existing) + alle 12 vorherigen |

`TDialogTests` müssen explizit absichern, dass Escape den Dialog mit `cmCancel`
schließt, sofern kein Kind-Control das Ereignis vorher konsumiert.

### Ergänzung: CommandIDs

`ShellCommandIds.cs` um `cmOK = 10`, `cmCancel = 11`, `cmYes = 12`, `cmNo = 13` ergänzen —
**vor** Step 11 (TButton braucht diese Konstanten).

### Negativ- und Grenzfall-Fokus

Die Task-Ableitung MUSS zusätzlich sichtbare Negativ-/Grenzfall-Abdeckung für diese
bereits in der Spec genannten Punkte erzeugen:

- `TDialog` mit keinem fokussierbaren Kind-Control
- `TListBox` mit leerer `TStringList`
- `TInputLine` mit `MaxLen = 0`
- `TScrollBar` an Clamp-/Grenzpositionen
- Mausereignisse außerhalb eines offenen modalen Dialogs

Diese Fälle sind als Test- und Designabgrenzung zu behandeln, nicht als Anlass für
zusätzliche nicht spezifizierte Feature-Erweiterungen.

---

## Acceptance Criteria (aus Spec SC-001 bis SC-006)

| Kriterium | Prüfmethode |
|---|---|
| SC-001: Vollständiger Dialog ohne nativen Code lauffähig | Integrations-Test: TDialog.Run() mit allen 5 Pflicht-Controls |
| SC-002: Alle 13 Klassen vorhanden und buildbar | `dotnet build` + Klassen-Existenzprüfung |
| SC-003: Line Coverage ≥ 70 % | `dotnet test --collect:"XPlat Code Coverage"` → Coverlet-Report |
| SC-004: Korrekte Tastatur-Navigation | TDialogTests: Tab-Wrap, Escape → `cmCancel`, Enter-Default-Button; TListBoxTests: Doppelklick bestätigt Auswahl ohne separates Command |
| SC-005: Korrekte visuelle Zustandsdarstellung | Snapshot-Tests auf TConsoleBuffer-Inhalt |
| SC-006: Vollständige zweisprachige XML-Doku | `dotnet build` ohne CS1591-Fehler; `docfx docfx.json` erfolgreich |

---

## Risiken / Risks

| Risiko / Risk | Wahrscheinlichkeit | Gegenmassnahme |
|---|---|---|
| `TDialog.Run()` blockiert Unit-Tests, die keinen echten Event-Loop haben | Mittel | Test-Spy/Fake-Event-Pump für TDialogTests; Dialog über injiziertes Schließ-Kommando beenden |
| Coverage-Gate nicht erreicht nach ersten 12 Klassen | Niedrig | Coverage nach Schritt 6 messen; ggf. Negativ-Tests ergänzen |
| `TViewState.Default` Konflikt mit bestehendem Code | Niedrig | Flag bereits in TViewState (0x400) definiert — kein Konflikt |
| TDD-Reihenfolge in Commits nicht eingehalten | Mittel | Branch-Schutz + PR-Review prüft Commit-Reihenfolge (Red vor Green) |

---

## Nächster Schritt / Next Step

`/speckit.tasks` — Generierung der konkreten, geordneten Task-Liste aus diesem Plan.
