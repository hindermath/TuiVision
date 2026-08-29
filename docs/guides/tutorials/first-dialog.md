# Erster Dialog mit dem vorhandenen Tutorial / First Dialog with the Existing Tutorial

## Deutsch

### Lernziel und Voraussetzungen

Nach [Getting Started](../getting-started.md),
[Event-Loop](../concepts/event-loop.md) und
[View-Hierarchie](../concepts/view-hierarchy.md) kannst du einen modalen Dialog
starten, bedienen und in den Owner-Baum einordnen. Dieser Guide ergänzt keinen
neuen Beispielcode; er verwendet den geprüften Tutorial-Schritt `tvguid12`.

### Start

```bash
dotnet run --project examples/Tutorial -- tvguid12
```

Der erste Frame zeigt die Anwendungsshell. Öffne den Dialog über das
Tutorial-Menü. Wechsle mit `Tab` zwischen erreichbaren Controls, bestätige mit
`Enter` oder brich mit `Esc` ab. `Ctrl+Q` beendet die Anwendung.

### Was passiert?

1. Die Anwendung erzeugt einen `TDialog` mit Bounds relativ zum Desktop.
2. `ExecView()` fügt ihn vorübergehend in die Gruppe ein.
3. Der Dialog erhält Fokus und verarbeitet Events über seinen modalen
   `Run()`-Pfad.
4. Der End-Command liefert einen Ergebniscode.
5. Der Dialog wird entfernt; gültiger vorheriger Fokus wird wiederhergestellt.

Abbruch ist ein fachlicher Ergebnisweg, kein Fehler. Eine Validation darf den
Fokuswechsel oder Abschluss ablehnen, ohne einen halben Zustand zu übernehmen.

### Architekturhinweis und Proof

Der Dialog verwendet vorhandene Framework-Views. Der zugehörige Smoke führt den
echten App-Loop aus und kombiniert Zustand, View-Identität und sichtbare Zellen.
Direkte Konstruktion allein wäre nur Setup.

### Übung

Führe den Dialog einmal mit Bestätigung und einmal mit Abbruch aus. Notiere,
welcher Fokus danach aktiv ist. Vergleiche anschließend `tvguid11` und
`tvguid12`: nicht-modal und modal unterscheiden sich im Lebenszyklus, nicht in
der Grundidee einer View-Gruppe.

## English

### Learning goal and prerequisites

After the introductory, event-loop, and view-hierarchy guides, run the proven
`tvguid12` modal-dialog step. This guide adds no example code.

### Launch and operation

Use the command above, open the tutorial dialog, move with `Tab`, accept with
`Enter`, cancel with `Esc`, and quit with `Ctrl+Q`.

`ExecView()` inserts the dialog temporarily, gives it focus, runs its modal
event path, receives a result command, removes it, and restores eligible prior
focus. Cancel is a normal result. Validation may reject completion without
publishing partial state.

### Exercise

Run one accepted and one cancelled dialog. Record the restored focus, then
compare `tvguid11` and `tvguid12` to distinguish non-modal and modal lifetime.
