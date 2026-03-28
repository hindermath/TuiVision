# Tutorial — Anleitung / Guide

> **Quelle / Source**: `tv203s/contrib/tvision/examples/tutorial/` (16 Schritte: `tvguid01.cc` bis `tvguid16.cc`)
> **Wave**: 1 — Pflichtbeispiel aus dem Originalordner `tv203s/contrib/tvision/examples/`
> **Nicht** Bestandteil von `TVDEMOS/` oder `TVFM/`.

---

## Lernziel / Learning Goal

Die 16-stufige Tutorial-Reihe führt schrittweise in die zentralen TuiVision-Konzepte ein:
von der minimalen `TApplication` bis hin zu vollständigen Dialogen mit Datenspeicherung.
Jeder Schritt baut auf dem vorherigen auf und demonstriert ein eigenständiges Lernziel.

The 16-step tutorial series gradually introduces the core TuiVision concepts:
from the minimal `TApplication` to complete dialogs with data persistence.
Each step builds on the previous one and demonstrates an independent learning objective.

---

## Voraussetzungen / Prerequisites

- **.NET 10 SDK installiert** — lade das SDK von [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0)
  herunter und installiere es auf deinem Rechner. /
  **.NET 10 SDK installed** — download the SDK from [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0)
  and install it on your machine.

- **TuiVision-Repository geklont** — klone das Repository mit:
  `git clone https://github.com/hindermath/TuiVision.git` /
  **TuiVision repository cloned** — clone the repository with:
  `git clone https://github.com/hindermath/TuiVision.git`

- **Grundkenntnisse in C#** — falls du C# noch nicht kennst, bietet
  [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/) einen guten Einstieg. /
  **Basic knowledge of C#** — if you are new to C#,
  [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/) provides a solid starting point.

- **TuiVision-Ereignissystem (optional, empfohlen ab Schritt 03)** — das Tutorial
  verwendet Events intensiv; einen Überblick über das Ereignissystem findest du in
  `src/TuiVision.Core/TEvent.cs` im Repository. /
  **TuiVision event system (optional, recommended from step 03 onwards)** — the tutorial
  makes heavy use of events; refer to `src/TuiVision.Core/TEvent.cs` in the repository
  for an overview of the event system.

---

## Startbefehl / Startup Command

Jeden Schritt einzeln starten — Token als erstes Argument übergeben:

Start each step individually — pass the token as the first argument:

```bash
dotnet run --project examples/Tutorial -- tvguid01
dotnet run --project examples/Tutorial -- tvguid16
```

Ohne Argument wird `tvguid01` gestartet / Without argument, `tvguid01` is started:

```bash
dotnet run --project examples/Tutorial
```

---

## Schritt-Übersicht / Step Overview

| Token | Schritt / Step | Lernziel / Learning Objective |
|---|---|---|
| `tvguid01` | 01 | Minimale TApplication |
| `tvguid02` | 02 | Menüleiste mit Untermenüs |
| `tvguid03` | 03 | Menübefehl-Verarbeitung |
| `tvguid04` | 04 | Ein TWindow öffnen |
| `tvguid05` | 05 | Inhalt in ein Fenster zeichnen |
| `tvguid06` | 06 | Vertikale Bildlaufleiste |
| `tvguid07` | 07 | Horizontale und vertikale Bildlaufleisten |
| `tvguid08` | 08 | Bildlaufleisten und Delta-Punkt |
| `tvguid09` | 09 | Mehrere Fenster |
| `tvguid10` | 10 | Ein TDialog öffnen |
| `tvguid11` | 11 | Schaltflächen im Dialog |
| `tvguid12` | 12 | Eingabefeld im Dialog |
| `tvguid13` | 13 | Zwei Schaltflächen |
| `tvguid14` | 14 | Kontrollkästchen und Optionsfelder |
| `tvguid15` | 15 | Dialogdaten speichern |
| `tvguid16` | 16 | Dialogdaten speichern und wiederherstellen |

---

## Schritt 01 — Minimale TApplication / Minimal TApplication

**Quelle / Source**: `tvguid01.cc`

**Lernziel**: Die kleinstmögliche TuiVision-Anwendung starten und sauber beenden.
**Learning goal**: Start the smallest possible TuiVision application and exit cleanly.

**Ergebnis / Expected outcome**: Ein leeres Fenster mit Menüleiste, Desktop und Statuszeile erscheint.
`Alt-X` beendet die Anwendung.

**Architektur**: Nur `TApplication` ohne Anpassungen. / Architecture: Just `TApplication` without customization.

**Übungen / Exercises**:
1. Überschreiben Sie `InitMenuBar()` und geben Sie eine leere Menüleiste zurück.
2. Ändern Sie die Terminalgröße und beobachten Sie das Verhalten.

---

## Schritt 02 — Menüleiste mit Untermenüs / Menu Bar with Submenus

**Quelle / Source**: `tvguid02.cc`

**Lernziel**: Eine Menüleiste mit mindestens einem Untermenü und Menüpunkten hinzufügen.
**Learning goal**: Add a menu bar with at least one submenu and menu items.

**Ergebnis**: Eine Menüleiste erscheint oben. Untermenüs können mit der Maus oder Tastatur geöffnet werden.

**Übungen**:
1. Fügen Sie einen zweiten Untermenüeintrag hinzu.
2. Testen Sie, ob `Alt-F10` die Menüleiste fokussiert.

---

## Schritt 03 — Menübefehl-Verarbeitung / Menu Command Handling

**Quelle / Source**: `tvguid03.cc`

**Lernziel**: Auf Menübefehle in `HandleEvent()` reagieren.
**Learning goal**: Respond to menu commands in `HandleEvent()`.

**Ergebnis**: Ein Menüpunkt zeigt beim Klick eine Aktion (z. B. ein Meldungsfenster).

**Übungen**:
1. Implementieren Sie „Über dieses Programm / About" als Menüpunkt.
2. Deaktivieren Sie einen Menüpunkt dynamisch mit `DisableCommand()`.

---

## Schritt 04 — Ein TWindow öffnen / Opening a TWindow

**Quelle / Source**: `tvguid04.cc`

**Lernziel**: Ein `TWindow` erstellen und in den Desktop einfügen.
**Learning goal**: Create a `TWindow` and insert it into the desktop.

**Ergebnis**: Ein Fenster erscheint auf dem Desktop. Es kann mit der Maus bewegt werden.

**Übungen**:
1. Öffnen Sie das Fenster zentriert mit dem `TViewOptions.Centered`-Flag.
2. Setzen Sie einen benutzerdefinierten Fenstertitel.

---

## Schritt 05 — Inhalt in ein Fenster zeichnen / Drawing Content into a Window

**Quelle / Source**: `tvguid05.cc`

**Lernziel**: Die `Draw()`-Methode eines Fensters überschreiben und benutzerdefinierten
Text in den Fensterinhalt zeichnen.

**Learning goal**: Override a window's `Draw()` method and draw custom text into the window content.

**Ergebnis**: Ein Fenster mit eigenem Text-Inhalt erscheint auf dem Desktop.

**Übungen**:
1. Zeichnen Sie mehrere Textzeilen.
2. Verwenden Sie verschiedene Farben für Vordergrund und Hintergrund.

---

## Schritt 06 — Vertikale Bildlaufleiste / Vertical Scroll Bar

**Quelle / Source**: `tvguid06.cc`

**Lernziel**: Eine vertikale Bildlaufleiste zu einem Fenster hinzufügen.
**Learning goal**: Add a vertical scroll bar to a window.

**Ergebnis**: Eine Bildlaufleiste erscheint am rechten Rand des Fensters.

**Übungen**:
1. Verknüpfen Sie die Bildlaufleiste mit dem Fensterinhalt.
2. Testen Sie das Verhalten, wenn der Inhalt kürzer als das Fenster ist.

---

## Schritt 07 — Horizontale und vertikale Bildlaufleisten / Horizontal and Vertical Scroll Bars

**Quelle / Source**: `tvguid07.cc`

**Lernziel**: Beide Bildlaufleisten zu einem Fenster hinzufügen.
**Learning goal**: Add both scroll bars to a window.

**Ergebnis**: Ein Fenster mit horizontaler und vertikaler Bildlaufleiste erscheint.

**Übungen**:
1. Verknüpfen Sie beide Bildlaufleisten mit dem Fensterinhalt.

---

## Schritt 08 — Bildlaufleisten und Delta-Punkt / Scroll Bars and Delta Point

**Quelle / Source**: `tvguid08.cc`

**Lernziel**: Verstehen, wie Bildlaufleisten den `Delta`-Punkt beeinflussen.
**Learning goal**: Understand how scroll bars affect the `Delta` point.

**Ergebnis**: Der Delta-Punkt ändert sich, wenn die Bildlaufleiste bewegt wird.

**Übungen**:
1. Zeige den aktuellen Delta-Punkt (X- und Y-Versatz) dynamisch im Fenstertitel an.
2. Begrenze den scrollbaren Bereich auf maximal 50 Zeilen und 120 Spalten mit `SetLimit()`.

---

## Schritt 09 — Mehrere Fenster / Multiple Windows

**Quelle / Source**: `tvguid09.cc`

**Lernziel**: Mehrere Fenster auf dem Desktop öffnen und ihre Z-Reihenfolge verstehen.
**Learning goal**: Open multiple windows on the desktop and understand their Z-order.

**Ergebnis**: Mehrere Fenster erscheinen. Das aktive Fenster liegt oben.

**Übungen**:
1. Öffne beim Start drei Fenster mit je einem eigenen Titel an verschiedenen Positionen.
2. Implementiere einen Menüpunkt, der mit `Desktop?.SelectNext(false)` zum nächsten Fenster wechselt (äquivalent zu F6).

---

## Schritt 10 — Ein TDialog öffnen / Opening a TDialog

**Quelle / Source**: `tvguid10.cc`

**Lernziel**: Einen modalen Dialog öffnen und schließen.
**Learning goal**: Open and close a modal dialog.

**Ergebnis**: Ein Dialog blockiert die restliche Anwendung, bis er geschlossen wird.

**Übungen**:
1. Füge dem Dialog einen `TButton` mit `cmOK` hinzu, damit er per Mausklick geschlossen werden kann.
2. Öffne den Dialog über einen Menüpunkt mit einem eigenen Befehlscode.

---

## Schritt 11 — Schaltflächen im Dialog / Buttons in a Dialog

**Quelle / Source**: `tvguid11.cc`

**Lernziel**: Schaltflächen (`TButton`) in einen Dialog einfügen.
**Learning goal**: Insert buttons (`TButton`) into a dialog.

**Ergebnis**: Der Dialog zeigt eine Schaltfläche, die beim Klick den Dialog schließt.

**Übungen**:
1. Füge neben dem OK-Button einen zweiten `TButton` für „Abbrechen / Cancel" ein.
2. Werte den Rückgabewert von `ExecView()` aus und zeige das Ergebnis in der Statuszeile an.

---

## Schritt 12 — Eingabefeld im Dialog / Input Line in a Dialog

**Quelle / Source**: `tvguid12.cc`

**Lernziel**: Ein Eingabefeld (`TInputLine`) in einen Dialog einfügen.
**Learning goal**: Add an input line (`TInputLine`) to a dialog.

**Ergebnis**: Der Dialog zeigt ein Textfeld für Benutzereingaben.

**Übungen**:
1. Begrenze die maximale Eingabelänge auf 20 Zeichen über den zweiten Parameter von `TInputLine`.
2. Lese nach dem Schließen des Dialogs den eingegebenen Text aus und zeige ihn in einem neuen Fenster an.

---

## Schritt 13 — Zwei Schaltflächen / Two Buttons

**Quelle / Source**: `tvguid13.cc`

**Lernziel**: Zwei Schaltflächen (OK und Abbrechen) in einen Dialog einfügen.
**Learning goal**: Insert two buttons (OK and Cancel) into a dialog.

**Ergebnis**: Der Dialog kann über zwei verschiedene Schaltflächen geschlossen werden.

**Übungen**:
1. Werte den Rückgabewert von `ExecView()` aus und reagiere unterschiedlich auf OK und Abbrechen.
2. Füge eine dritte Schaltfläche „Hilfe / Help" mit einem eigenen Befehlscode hinzu.

---

## Schritt 14 — Kontrollkästchen und Optionsfelder / Check Boxes and Radio Buttons

**Quelle / Source**: `tvguid14.cc`

**Lernziel**: `TCheckBoxes` und `TRadioButtons` in einen Dialog einfügen.
**Learning goal**: Insert `TCheckBoxes` and `TRadioButtons` into a dialog.

**Ergebnis**: Der Dialog zeigt auswählbare Optionen.

**Übungen**:
1. Lese nach `ExecView()` den Zustand der Checkboxen (Bitmask) und den ausgewählten RadioButton (Index) aus.
2. Setze vor dem Öffnen des Dialogs Standardwerte über die `Value`-Eigenschaft vor.

---

## Schritt 15 — Dialogdaten speichern / Saving Dialog Data

**Quelle / Source**: `tvguid15.cc`

**Lernziel**: Dialogzustand in einer Datenstruktur speichern.
**Learning goal**: Save dialog state in a data structure.

**Ergebnis**: Der Dialog merkt sich seine Zustände zwischen Öffnungen.

**Übungen**:
1. Speichere den Dialogzustand auch beim Klick auf „Abbrechen", indem du `GetData()` unabhängig vom Rückgabewert aufrufst.
2. Zeige den zuletzt gespeicherten Zustand nach dem Schließen des Dialogs im Hauptfenster an.

---

## Schritt 16 — Dialogdaten speichern und wiederherstellen / Save and Restore Dialog Data

**Quelle / Source**: `tvguid16.cc`

**Lernziel**: Dialogdaten mit `setData`/`getData` übertragen.
**Learning goal**: Transfer dialog data with `setData`/`getData`.

**Ergebnis**: Der Dialog stellt beim erneuten Öffnen die zuletzt eingegebenen Werte wieder her.

**Übungen**:
1. Füge dem Dialog einen Reset-Button hinzu, der alle Felder auf Standardwerte zurücksetzt, ohne den Dialog zu schließen.
2. Überschreibe `Valid()` in einer `TDialog`-Unterklasse, um die Eingabe vor dem Schließen zu prüfen (z. B. Name darf nicht leer sein).

---

## Quellenrückverfolgung / Source Traceability

| Verwaltete Datei / Managed File | Historische Quelle / Historical Source |
|---|---|
| `examples/Tutorial/Steps/TvGuid01Step.cs` | `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc` |
| `examples/Tutorial/Steps/TvGuid02Step.cs` | `tv203s/contrib/tvision/examples/tutorial/tvguid02.cc` |
| … | … |
| `examples/Tutorial/Steps/TvGuid16Step.cs` | `tv203s/contrib/tvision/examples/tutorial/tvguid16.cc` |
| `examples/Tutorial/TutorialApp.cs` | Gemeinsamer Launcher — kein direktes historisches Gegenstück |
| `examples/Tutorial/Steps/TutorialStepCatalog.cs` | Kein historisches Gegenstück — neue Infrastruktur |
