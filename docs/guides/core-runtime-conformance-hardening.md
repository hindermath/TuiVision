# Core-Runtime-Konformität / Core Runtime Conformance

## Zweck / Purpose

Feature 025 schließt neun Framework-Findings aus dem TV203-/Free-Vision-
Konformitätsaudit. Die Umsetzung übernimmt die historische Verantwortung von
Events, Fokus, View-Hierarchie, Anwendungsschleife, Desktop, Modalität,
Commands, Tastatureingang und Drag. Der Code bleibt dabei idiomatisches C# und
bildet weder Zeigerlayout noch native Nachrichtenschleifen mechanisch nach.

Feature 025 closes nine framework findings from the TV203/Free Vision
conformance audit. The implementation retains the historical responsibilities
for events, focus, view hierarchy, application loop, desktop, modality,
commands, keyboard ingress, and drag. The code remains idiomatic C# and does
not mechanically reproduce pointer layouts or native message loops.

## Verträge im Überblick / Contract Overview

| Bereich / Area | Vertrag / Contract | Nachweisgrenze / Proof boundary |
|---|---|---|
| Event | Eine Factory akzeptiert genau eine konkrete Ereignisart / A factory accepts exactly one concrete event kind | Öffentliche Factory, keine Filtermaske / Public factory, not a filter mask |
| Fokus / Focus | Die aktuelle View darf den Fokusverlust vor jeder Mutation ablehnen / Current view may reject focus loss before mutation | `TrySetFocus` liefert `Accepted`, `Rejected` oder `NoOp` |
| Hierarchie / Hierarchy | State-Bits folgen ihrer jeweiligen Owner-Verantwortung / State bits follow their owner responsibility | Direkte und verschachtelte Gruppen / Direct and nested groups |
| Event-Loop | Ein Pending-Slot, danach Host-Poll, dann genau ein Idle / One pending slot, then host poll, then exactly one idle | Reale `Run`- und `GetEvent`-Pfade / Real `Run` and `GetEvent` paths |
| Desktop | Insert, Next, Tile, Cascade und Close-All liefern explizite Ergebnisse / Insert, next, tile, cascade, and close-all return explicit results | Fokus, Z-Order, Bounds und Veto-Zähler / Focus, Z-order, bounds, and veto counts |
| Modalität / Modality | Ein direktes modales Kind pro Owner; Cleanup läuft immer / One direct modal child per owner; cleanup always runs | Result, Ownership und Fokuswiederherstellung / Result, ownership, and focus restoration |
| Commands | Eine unveränderliche Momentaufnahme steuert Menü, Status und Dispatch / One immutable snapshot drives menu, status, and dispatch | Fokus, Event, Idle und Pre-Dispatch / Focus, event, idle, and pre-dispatch |
| Tastatur / Keyboard | Reale Konsoleneingabe nutzt den kanonischen Compatibility-Adapter / Real console input uses the canonical Compatibility adapter | Zeichen, Navigation, Funktionstasten und Modifier / Characters, navigation, function keys, and modifiers |
| Drag | Pointer und Tastatur verwenden dieselbe begrenzte Session / Pointer and keyboard use the same bounded session | Threshold, Capture, Ziel, Drop und Abbruch / Threshold, capture, target, drop, and cancellation |

## Fokus und View-State / Focus and View State

`TGroup.TrySetFocus` prüft zuerst Owner, Sichtbarkeit, Sperre und Auswahlbarkeit.
Danach fragt es die aktuelle View mit `CanReleaseFocus` genau einmal. Bei einem
Veto bleiben `Current`, Daten, State und sichtbare Fokusinformationen
unverändert. Das kompatible `SetFocus` verwendet denselben Pfad, verwirft aber
das Ergebnis.

`TGroup.TrySetFocus` first checks owner, visibility, disablement, and
selectability. It then asks the current view through `CanReleaseFocus` exactly
once. On a veto, `Current`, data, state, and visible focus information remain
unchanged. Compatible `SetFocus` uses the same path but discards the result.

Die State-Matrix ist absichtlich nicht einheitlich: `Active` und `Dragging`
erreichen alle direkten Kinder, `Focused` nur `Current`, und `Exposed` nur
sichtbare Kinder. `Disabled` bleibt lokal bei der jeweiligen View. Dadurch kann
eine Gruppe Dispatch begrenzen, ohne den eigenen Sperrzustand eines Kindes zu
überschreiben.

The state matrix is deliberately not uniform: `Active` and `Dragging` reach
all direct children, `Focused` only reaches `Current`, and `Exposed` only
reaches visible children. `Disabled` remains local to each view. A group can
therefore limit dispatch without overwriting a child's own disablement.

## Pending und Idle / Pending and Idle

`PutEvent` besitzt genau einen Pending-Slot. Ein zweites Ereignis ersetzt nicht
stillschweigend das erste. `GetEvent` leert den Slot vor dem nicht blockierenden
Host-Poll. Nur wenn beide leer sind, ruft `Run` einmal `Idle` auf und gibt danach
CPU-Zeit frei. Ein Shutdown aus `Idle` startet keine weitere Poll-Runde.

`PutEvent` owns exactly one pending slot. A second event does not silently
replace the first. `GetEvent` drains the slot before the non-blocking host poll.
Only when both are empty does `Run` call `Idle` once and then release CPU time.
A shutdown from `Idle` starts no further polling round.

Diese Reihenfolge erlaubt Uhr-, Status- oder inkrementelle Aktualisierung, ohne
einen Hintergrundthread oder eine anwendungseigene Busy-Loop einzuführen.

This order permits clock, status, or incremental refresh without introducing a
background thread or an application-owned busy loop.

## Desktop, Close und Modalität / Desktop, Close, and Modality

`TDesktop` arbeitet nur auf direkten Kindern und liefert für jede Operation ein
`TDesktopOperationResult`. Tile und Cascade begrenzen Geometrie auf den
Desktop. Close-All zählt geschlossene, abgelehnte und übersprungene Views
getrennt. Eine View entscheidet über `ICloseableView` ausdrücklich, ob sie
geschlossen werden darf; veränderte Daten werden nicht stillschweigend
verworfen.

`TDesktop` operates only on direct children and returns a
`TDesktopOperationResult` for each operation. Tile and cascade constrain
geometry to the desktop. Close-all counts closed, vetoed, and skipped views
separately. A view explicitly decides through `ICloseableView` whether it may
close; modified data is not discarded silently.

`ExecuteModal` erlaubt genau ein direktes modales Kind pro Owner. Ein bereits
aktiver Dialog darf ein eigenes modales Kind öffnen, aber kein gleichrangiger
zweiter Dialog darf den Owner übernehmen. `finally` entfernt temporäre
Ownership und stellt den vorherigen gültigen Fokus auch nach Ausnahme oder
Shutdown wieder her.

`ExecuteModal` permits exactly one direct modal child per owner. An active
dialog may open its own modal child, but a second peer dialog cannot take over
the owner. `finally` removes temporary ownership and restores the previous
valid focus even after an exception or shutdown.

## Gemeinsamer Command-Kontext / Shared Command Context

`TCommandContext` ist eine unveränderliche Momentaufnahme. Opt-in Views liefern
ihren Zustand über `ICommandStateProvider`. `TProgram` aktualisiert den Kontext
nach Fokuswechsel, behandeltem Ereignis und Idle sowie unmittelbar vor einem
Command-Dispatch. Menü und StatusLine wenden diesen Zustand als getrenntes
Overlay an; eine manuelle Sperre bleibt immer wirksam.

`TCommandContext` is an immutable snapshot. Opt-in views provide their state
through `ICommandStateProvider`. `TProgram` refreshes the context after focus
changes, handled events, and idle, and immediately before command dispatch.
Menu and status line apply this state as a separate overlay; a manual
disablement always remains effective.

Damit stimmen Tastatur, Menü, StatusLine und aktive View überein, ohne einen
globalen Anwendungskatalog oder veränderlichen Singleton-Zustand einzuführen.

This keeps keyboard, menu, status line, and active view consistent without
introducing a global application catalog or mutable singleton state.

## Tastatur- und Drag-Pfade / Keyboard and Drag Paths

`TProgram.GetEvent` übersetzt reale `ConsoleKeyInfo`-Werte ausschließlich über
`TConsoleInputAdapter`. Druckbare Zeichen, Navigation, Funktionstasten, Alt,
Ctrl, Shift und unbekannte Fallbacks nutzen damit dieselben Modifier-Bits wie
die Controls. Tests, die bereits normalisierte Events einspeisen, ersetzen
diesen Ingress-Nachweis nicht.

`TProgram.GetEvent` translates real `ConsoleKeyInfo` values exclusively through
`TConsoleInputAdapter`. Printable characters, navigation, function keys, Alt,
Ctrl, Shift, and unknown fallbacks therefore use the same modifier bits as the
controls. Tests that inject already normalized events do not replace this
ingress proof.

Eine `TDragSession` beginnt `Pending`, übernimmt nach einer Cell Bewegung genau
ein Capture und endet mit einem unveränderlichen `TDragResult`. Das Ziel muss
über `IDragTarget` ausdrücklich zustimmen. Escape, Capability-Verlust,
Deaktivierung, Entfernung und Shutdown brechen kontrolliert ab. Beim Fenster
verwenden Titelzeilen-Drag und `Ctrl+F5` plus Pfeile, Enter oder Escape dieselbe
Session.

A `TDragSession` starts as `Pending`, captures exactly once after one cell of
movement, and ends with an immutable `TDragResult`. The target must explicitly
opt in through `IDragTarget`. Escape, capability loss, disablement, removal,
and shutdown cancel in a controlled way. For a window, title-row drag and
`Ctrl+F5` plus arrows, Enter, or Escape use the same session.

## A11Y- und Textnachweis / A11Y and Text Proof

Alle primären Interaktionen besitzen einen Tastaturpfad. Fokus, Command-
Verfügbarkeit, Close-Ergebnis, Modal-Result und Drag-Abschluss sind als Zustand
oder Text prüfbar und nicht nur durch Farbe oder Pointerbewegung. Render-Tests
kombinieren View-Identität, konkrete Bounds und sichtbare Buffer-/Cell-Werte.

Every primary interaction has a keyboard path. Focus, command availability,
close result, modal result, and drag completion are testable as state or text,
not only through color or pointer movement. Render tests combine view identity,
concrete bounds, and visible buffer/cell values.

## Historische und Plattformgrenzen / Historical and Platform Boundaries

Die passenden TV203-Dateien unter `tv203s/` wurden schreibgeschützt geprüft.
Free Vision am festgehaltenen Commit diente als zweite Meinung für
Verantwortung und Lifecycle. Es wurde kein externer Quelltext kopiert oder in
Git aufgenommen. Managed Results, unveränderliche Snapshots und `finally`-
Cleanup sind bewusste moderne Abweichungen.

Matching TV203 files under `tv203s/` were reviewed read-only. Free Vision at
the recorded commit served as a second opinion for responsibility and
lifecycle. No external source was copied or tracked. Managed results,
immutable snapshots, and `finally` cleanup are deliberate modern deviations.

Die deterministischen Verträge sind hostunabhängig. Reale Keyboard- und
Modifier-Semantik wird lokal auf macOS/Linux und zusätzlich durch die
Repository-CI für Windows/WSL geprüft. Native Nachrichtenschleifen, vollständige
Desktop-Drag-and-Drop-Protokolle und Wave-5-/Wave-6-Anwendungscode bleiben
außerhalb von Feature 025.

The deterministic contracts are host-independent. Real keyboard and modifier
semantics are checked locally on macOS/Linux and additionally by repository CI
for Windows/WSL. Native message loops, complete desktop drag-and-drop
protocols, and Wave-5/Wave-6 application code remain outside Feature 025.

## Verifikation / Verification

```bash
dotnet test tests/TuiVision.Core.Tests/TuiVision.Core.Tests.csproj --configuration Release
dotnet test tests/TuiVision.Compatibility.Tests/TuiVision.Compatibility.Tests.csproj --configuration Release
dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release
```

Der maschinenlesbare Abschluss steht in
`specs/024-tv203-freevision-conformance-audit/conformance-audit.json`. Die
vollständige Red-/Green- und Governance-Evidence steht in
`specs/025-core-runtime-conformance-hardening/pr-evidence.md`.

The machine-readable closure is stored in
`specs/024-tv203-freevision-conformance-audit/conformance-audit.json`. Complete
red/green and governance evidence is stored in
`specs/025-core-runtime-conformance-hardening/pr-evidence.md`.
