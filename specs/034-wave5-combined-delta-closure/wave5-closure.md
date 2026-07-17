# Wave-5 Combined Delta Closure

## Ergebnis / Result

Der kombinierte Produktdelta aus Feature 032 und Feature 033 ist für alle zehn
TP7-Beispiele geschlossen. Es gibt keine offene `Gap`-Dimension, kein
`CandidateFinding` und keine `ProductDecision`.

The combined product delta from Features 032 and 033 is closed for all ten TP7
examples. There is no open `Gap` dimension, `CandidateFinding`, or
`ProductDecision`.

| Menge / Set | Ergebnis / Result |
|---|---:|
| Produktdeltas / Product deltas | 5 |
| Historische Quellen / Historical sources | 15 |
| Consumer-Gruppen / Consumer groups | 6 |
| Funktionsnachweise / Functional proofs | 10 |
| Showcase-Abschlüsse / Showcase closures | 10 |
| Guides und Startpfade / Guides and launch paths | 10 |
| Kombinierte Beispielzeilen / Combined example rows | 10 |
| `AcceptedIntentionalDeviation` | 10 |
| `Gap` / `CandidateFinding` / `ProductDecision` | 0 / 0 / 0 |

## Kombinierte Matrix / Combined Matrix

| Beispiel / Example | Quellen / Sources | Consumer | Funktion / Function | Showcase | Hauptentscheidung / Primary decision |
|---|---|---|---|---|---|
| `Tp7Demo` | `TVDEMO`, `DEMOCMDS`, `DEMOSTRS`, `GADGETS` | `W5-001` | App-Loop, Commands, Idle, Fenster / windows | Desktop, Fokus, Status, F1, Zellen / cells | `AcceptedIntentionalDeviation` |
| `Tp7Edit` | `TVEDIT` | `W5-002` | Edit, Save, Safe Close, Traversal | Editor, Menüs / menus, Fokus, Status, F1 | `AcceptedIntentionalDeviation` |
| `Tp7Help` | `TVHC`, `HELPFILE`, `DEMOHELP` | `W5-001` | Compile, Context, Reference, Back, Fallback | Help-Viewer, Fokus, Status, F1, Zellen / cells | `AcceptedIntentionalDeviation` |
| `Tp7ResourceDemo` | `TVRDEMO` | `W5-003` | Load, Select, Atomic Rejection | Dialog, Liste / list, Status, F1, Zellen / cells | `AcceptedIntentionalDeviation` |
| `Tp7ResourceGenerator` | `GENRDEMO` | `W5-004` | Generate, Controlled Root, Rejection | Form, Preview, Fokus, Status, F1 | `AcceptedIntentionalDeviation` |
| `Tp7AsciiTable` | `ASCIITAB` | `W5-005` | Navigate, Select, Boundary | Focusable Grid, Status, F1, `52x22` | `AcceptedIntentionalDeviation` |
| `Tp7Calculator` | `CALC` | `W5-005` | Arithmetic, Clear, Back, Atomic Division Rejection | Dialog, 20 Buttons, Fokus, Status, F1, `40x12` | `AcceptedIntentionalDeviation` |
| `Tp7Calendar` | `CALENDAR` | `W5-005` | Fixed Month, Day and Year Navigation | Focusable Grid, Status, F1, `42x16` | `AcceptedIntentionalDeviation` |
| `Tp7Puzzle` | `PUZZLE` | `W5-005` | Fixed Board, Move, Atomic Rejection | Focusable Grid, Status, F1, `38x15` | `AcceptedIntentionalDeviation` |
| `Tp7MouseDialog` | `MOUSEDLG` | `W5-006` | Capability, Mouse, Keyboard Fallback, Loss | Dialog Controls, Fokus, Status, F1, `46x16` | `AcceptedIntentionalDeviation` |

Alle Zeilen bewerten Verhalten, Interaktion, Layout, Proof, Dokumentation,
A11Y, Plattform, Sicherheit und Framework-Wiederverwendung. Die
`IntentionalDeviation`-Werte betreffen moderne, deterministische C#-Zustände,
kompakte Terminal-Layouts oder ehrliche Plattformgrenzen; ein Unterschied zum
Pascal-Quelltext allein wurde nicht als Finding gewertet.

Every row evaluates behavior, interaction, layout, proof, documentation, A11Y,
platform, security, and framework reuse. `IntentionalDeviation` values cover
modern deterministic C# state, compact terminal layouts, or honest platform
boundaries; a difference from Pascal source alone was not treated as a finding.

## Framework- und Proof-Grenze / Framework and Proof Boundary

| Bereich / Area | Entscheidung / Decision | Grenze / Boundary |
|---|---|---|
| `Wave5Application` | `ExampleComposition` | Delegiert Event-Loop, Desktop, Fokus und Views an bestehende Framework-Verträge / delegates event loop, desktop, focus, and views to existing framework contracts |
| `Wave5ConsoleHost` | `ExampleComposition` | Wählt nur Normal-/Smoke-Start und eine ehrliche Fallbackgröße / selects only normal or smoke launch and an honest fallback size |
| `Wave5StatusLine` | `ExampleComposition` | Ist eine echte `TStatusLine` mit veränderlichem Lernerstatus / is a real `TStatusLine` with mutable learner status |
| `Wave5GridView` | `ExampleComposition` | Nutzt `TView`-Fokus, Draw und Buffer; keine eigene Dispatch-Schicht / uses `TView` focus, draw, and buffer; no independent dispatch layer |

Echte `app.Run()`-Pfade mit Zustand, View-Identität, Fokus, Status,
Description und Buffer-/Cell-Prüfung sind `PrimaryProof`. Testeigene
Verzeichnisse und Fixtures sind `SetupOnly`; direkte Hilfsinspektionen sind
höchstens `SupplementalProof`.

Real `app.Run()` paths with state, view identity, focus, status, Description,
and buffer/cell assertions are `PrimaryProof`. Test-owned directories and
fixtures are `SetupOnly`; direct helper inspection is at most
`SupplementalProof`.

## Kausale Grenze / Causal Boundary

Der reviewte Feature-Head hält Wave 5 und Wave 6 bis zum tatsächlichen Merge
gesperrt. Erst ein nicht leerer Evidence-Closeout darf Wave 5 auf `Closed` und
Wave 6 auf `EligibleForIntake` setzen. Feature 035 wird dabei nur reserviert
und nicht gestartet.

The reviewed feature head keeps Wave 5 and Wave 6 blocked until the actual
merge. Only a non-empty evidence closeout may set Wave 5 to `Closed` and Wave 6
to `EligibleForIntake`. Feature 035 is reserved but not started.
