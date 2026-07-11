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

Der funktionale Nachweis für `014-wave1-functional-hardening` prüft alle 16
Tokens einzeln. Nach `TutorialApp.Run()` meldet der Launcher über
`LastRunStepToken`, welcher Schritt wirklich gestartet wurde; unbekannte Tokens
setzen stattdessen den Fallback-Zustand. Die Smoke-Tests prüfen außerdem
Sequenznummer, zweisprachige Beschreibung und ein schrittspezifisches
Lernziel-Fragment für jeden Eintrag.

The functional proof for `014-wave1-functional-hardening` checks all 16 tokens
individually. After `TutorialApp.Run()`, the launcher reports the actually
started step through `LastRunStepToken`; unknown tokens set the fallback state
instead. The smoke tests also check sequence number, bilingual description, and
a step-specific learning-target fragment for every entry.

`017-wave1-visual-component-remediation` macht jeden gültigen Token sichtbar
unterscheidbar. `Lesson -> Action` aktiviert den repräsentativen Zustand, die
echte Statuszeile nennt Token und Zustand, und `Help -> Description` öffnet die
zweisprachige Erklärung. Ein unbekannter Token bleibt ein erklärter Fallback;
er wird nicht als gültiger Lernschritt dargestellt.

`017-wave1-visual-component-remediation` makes every valid token visibly
distinct. `Lesson -> Action` activates the representative state, the real status
line names token and state, and `Help -> Description` opens the bilingual
explanation. An unknown token remains an explained fallback and is not presented
as a valid lesson.

---

## Schritt-Übersicht / Step Overview

| Token | Schritt / Step | Lernziel / Learning Objective |
|---|---|---|
| `tvguid01` | 01 | Minimale TApplication |
| `tvguid02` | 02 | Statuszeilen-Eintrag |
| `tvguid03` | 03 | Menü und Befehlsverarbeitung |
| `tvguid04` | 04 | Ein TWindow öffnen |
| `tvguid05` | 05 | Inhalt in ein Fenster zeichnen |
| `tvguid06` | 06 | Einführung in scrollbaren Inhalt |
| `tvguid07` | 07 | Verbesserter Inhalt mit zwei Achsen |
| `tvguid08` | 08 | Bildlaufleisten und Delta-Punkt |
| `tvguid09` | 09 | Mehrere sichtbare Bereiche |
| `tvguid10` | 10 | Größenbeschränkungen |
| `tvguid11` | 11 | Nicht-modaler Dialog |
| `tvguid12` | 12 | Modaler Dialogzustand |
| `tvguid13` | 13 | Zwei Schaltflächen |
| `tvguid14` | 14 | Kontrollkästchen und Optionsfelder |
| `tvguid15` | 15 | Eingabezeile |
| `tvguid16` | 16 | Datentransfer und Validierung |

Jede Zeile besitzt einen eindeutigen sichtbaren Marker. Die Implementierung
zeigt die prägende Ergänzung des jeweiligen kumulativen C++-Schritts, nicht eine
mechanische 1:1-Rekonstruktion. Die 16 App-Loop-Smokes prüfen eindeutige
Signaturen, View-Typen und Token-Zellen in stabilen Regionen.

Every row has a unique visible marker. The implementation shows the defining
addition of each cumulative C++ step, not a mechanical one-to-one reconstruction.
The 16 app-loop smokes verify unique signatures, view types, and token cells in
stable regions.

---

## Schritt 01 — Minimale TApplication / Minimal TApplication

**Quelle / Source**: `tvguid01.cc`

**Lernziel**: Die kleinstmögliche TuiVision-Anwendung starten und sauber beenden.
**Learning goal**: Start the smallest possible TuiVision application and exit cleanly.

**Ergebnis / Expected outcome**: Ein leeres Fenster mit Menüleiste, Desktop und Statuszeile erscheint.
`Alt-X` beendet die Anwendung.

**Architektur**: Nur `TApplication` ohne Anpassungen. / Architecture: Just `TApplication` without customization.

**Übungen / Exercises**:
1. Überschreibe `InitMenuBar()` und gib eine leere Menüleiste zurück. / Override `InitMenuBar()` and return an empty menu bar.
2. Ändere die Terminalgröße und beobachte das Verhalten. / Resize the terminal and observe the behaviour.

---

## Schritt 02: Statuszeilen-Eintrag / Status-Line Item

**Quelle / Source**: `tvguid02.cc`

**Lernziel**: Einen tastaturerreichbaren Eintrag in der Statuszeile ergänzen.
**Learning goal**: Add a keyboard-reachable item to the status line.

**Ergebnis / Result**: Der sichtbare Marker nennt die Statuszeilen-Lektion; die
reale Statuszeile zeigt Token, Zustand, Beschreibungspfad und Beenden-Hinweis.

**Übungen / Exercises**:
1. Ergänze einen zweiten Statuszeilen-Hinweis. / Add a second status-line hint.
2. Prüfe die Erreichbarkeit nur mit der Tastatur. / Verify keyboard-only reachability.

---

## Schritt 03 — Menübefehl-Verarbeitung / Menu Command Handling

**Quelle / Source**: `tvguid03.cc`

**Lernziel**: Auf Menübefehle in `HandleEvent()` reagieren.
**Learning goal**: Respond to menu commands in `HandleEvent()`.

**Ergebnis**: Ein Menüpunkt zeigt beim Klick eine Aktion (z. B. ein Meldungsfenster).

**Übungen / Exercises**:
1. Implementiere einen „Über dieses Programm / About"-Menüpunkt. / Implement an "About this program" menu item.
2. Deaktiviere einen Menüpunkt dynamisch mit `DisableCommand()`. / Dynamically disable a menu item using `DisableCommand()`.

---

## Schritt 04 — Ein TWindow öffnen / Opening a TWindow

**Quelle / Source**: `tvguid04.cc`

**Lernziel**: Ein `TWindow` erstellen und in den Desktop einfügen.
**Learning goal**: Create a `TWindow` and insert it into the desktop.

**Ergebnis**: Ein Fenster erscheint auf dem Desktop. Es kann mit der Maus bewegt werden.

**Übungen / Exercises**:
1. Öffne das Fenster zentriert mit dem `TViewOptions.Centered`-Flag. / Open the window centred using the `TViewOptions.Centered` flag.
2. Setze einen benutzerdefinierten Fenstertitel. / Set a custom window title.

---

## Schritt 05 — Inhalt in ein Fenster zeichnen / Drawing Content into a Window

**Quelle / Source**: `tvguid05.cc`

**Lernziel**: Die `Draw()`-Methode eines Fensters überschreiben und benutzerdefinierten
Text in den Fensterinhalt zeichnen.

**Learning goal**: Override a window's `Draw()` method and draw custom text into the window content.

**Ergebnis**: Ein Fenster mit eigenem Text-Inhalt erscheint auf dem Desktop.

**Übungen / Exercises**:
1. Zeichne mehrere Textzeilen. / Draw multiple lines of text.
2. Verwende verschiedene Farben für Vordergrund und Hintergrund. / Use different colours for foreground and background.

---

## Schritt 06: Einführung in scrollbaren Inhalt / Scrollable Content Introduction

**Quelle / Source**: `tvguid06.cc`

**Lernziel**: Den kumulativen Inhaltsaufbau mit einer sichtbaren vertikalen
Scroll-Grenze darstellen. / **Learning goal**: Represent the cumulative content
growth with a visible vertical scroll boundary.

**Ergebnis**: Eine Bildlaufleiste erscheint am rechten Rand des Fensters.

**Übungen / Exercises**:
1. Verknüpfe die Bildlaufleiste mit dem Fensterinhalt. / Link the scroll bar to the window content.
2. Teste das Verhalten, wenn der Inhalt kürzer als das Fenster ist. / Test the behaviour when the content is shorter than the window.

---

## Schritt 07: Verbesserter Inhalt mit zwei Achsen / Improved Two-Axis Content

**Quelle / Source**: `tvguid07.cc`

**Lernziel**: Die nächste kumulative Inhaltsstufe mit horizontaler und vertikaler
Grenze darstellen. / **Learning goal**: Represent the next cumulative content
stage with horizontal and vertical boundaries.

**Ergebnis**: Ein Fenster mit horizontaler und vertikaler Bildlaufleiste erscheint.

**Übungen / Exercises**:
1. Verknüpfe beide Bildlaufleisten mit dem Fensterinhalt. / Link both scroll bars to the window content.

---

## Schritt 08 — Bildlaufleisten und Delta-Punkt / Scroll Bars and Delta Point

**Quelle / Source**: `tvguid08.cc`

**Lernziel**: Verstehen, wie Bildlaufleisten den `Delta`-Punkt beeinflussen.
**Learning goal**: Understand how scroll bars affect the `Delta` point.

**Ergebnis**: Der Delta-Punkt ändert sich, wenn die Bildlaufleiste bewegt wird.

**Übungen / Exercises**:
1. Zeige den aktuellen Delta-Punkt (X- und Y-Versatz) dynamisch im Fenstertitel an. / Display the current delta point (X and Y offset) dynamically in the window title.
2. Begrenze den scrollbaren Bereich auf maximal 50 Zeilen und 120 Spalten mit `SetLimit()`. / Limit the scrollable area to at most 50 rows and 120 columns using `SetLimit()`.

---

## Schritt 09: Mehrere sichtbare Bereiche / Multiple Visible Panes

**Quelle / Source**: `tvguid09.cc`

**Lernziel**: Zwei Inhaltsbereiche in einer gemeinsamen sichtbaren Komposition
unterscheiden. / **Learning goal**: Distinguish two content panes in one visible
composition.

**Ergebnis / Result**: Ein Fenster zeigt die Marker `left pane` und `right pane`.

**Übungen / Exercises**:
1. Ergänze einen dritten textorientierten Bereich. / Add a third text-oriented pane.
2. Prüfe, ob jeder Bereich ohne Farbe unterscheidbar bleibt. / Verify that every pane remains distinct without colour.

---

## Schritt 10: Größenbeschränkungen / Resize Constraints

**Quelle / Source**: `tvguid10.cc`

**Lernziel**: Mindest- und Höchstgrenzen einer veränderbaren Ansicht verstehen.
**Learning goal**: Understand minimum and maximum bounds of a resizable view.

**Ergebnis / Result**: Das sichtbare Fenster nennt den Zustand
`minimum 24x7 maximum desktop`.

**Übungen / Exercises**:
1. Ändere die Mindestgröße und prüfe die sichtbare Erklärung. / Change the minimum size and verify the visible explanation.
2. Prüfe die Grenzen bei einem kleinen Terminal. / Verify the bounds on a small terminal.

---

## Schritt 11: Nicht-modaler Dialog / Non-Modal Dialog

**Quelle / Source**: `tvguid11.cc`

**Lernziel**: Einen Dialog als nicht-modalen sichtbaren Zustand einfügen.
**Learning goal**: Insert a dialog as a non-modal visible state.

**Ergebnis / Result**: Ein `TDialog` zeigt den Marker `non-modal dialog state`.

**Übungen / Exercises**:
1. Füge einen zweiten nicht-modalen Dialog ein. / Insert a second non-modal dialog.
2. Zeige den aktiven Zustand in der Statuszeile. / Show the active state in the status line.

---

## Schritt 12: Modaler Dialogzustand / Modal Dialog State

**Quelle / Source**: `tvguid12.cc`

**Lernziel**: Den Wechsel vom nicht-modalen zum modalen Dialogzustand verstehen.
**Learning goal**: Understand the transition from non-modal to modal dialog state.

**Ergebnis / Result**: Ein `TDialog` zeigt den Marker `modal result pending`.

**Übungen / Exercises**:
1. Ergänze einen sichtbaren OK-Ergebniszustand. / Add a visible OK result state.
2. Vergleiche den Fokuspfad mit Schritt 11. / Compare the focus path with step 11.

---

## Schritt 13 — Zwei Schaltflächen / Two Buttons

**Quelle / Source**: `tvguid13.cc`

**Lernziel**: Zwei Schaltflächen (OK und Abbrechen) in einen Dialog einfügen.
**Learning goal**: Insert two buttons (OK and Cancel) into a dialog.

**Ergebnis**: Der Dialog kann über zwei verschiedene Schaltflächen geschlossen werden.

**Übungen / Exercises**:
1. Werte den Rückgabewert von `ExecView()` aus und reagiere unterschiedlich auf OK und Abbrechen. / Evaluate the return value of `ExecView()` and react differently to OK and Cancel.
2. Füge eine dritte Schaltfläche „Hilfe / Help" mit einem eigenen Befehlscode hinzu. / Add a third "Help" button with a custom command code.

---

## Schritt 14 — Kontrollkästchen und Optionsfelder / Check Boxes and Radio Buttons

**Quelle / Source**: `tvguid14.cc`

**Lernziel**: `TCheckBoxes` und `TRadioButtons` in einen Dialog einfügen.
**Learning goal**: Insert `TCheckBoxes` and `TRadioButtons` into a dialog.

**Ergebnis**: Der Dialog zeigt auswählbare Optionen.

**Übungen / Exercises**:
1. Lese nach `ExecView()` den Zustand der Checkboxen (Bitmask) und den ausgewählten RadioButton (Index) aus. / After `ExecView()`, read the checkbox state (bitmask) and the selected radio button (index).
2. Setze vor dem Öffnen des Dialogs Standardwerte über die `Value`-Eigenschaft vor. / Before opening the dialog, set default values using the `Value` property.

---

## Schritt 15: Eingabezeile / Input Line

**Quelle / Source**: `tvguid15.cc`

**Lernziel**: Eine `TInputLine` als fokussierbare Eingabefläche ergänzen.
**Learning goal**: Add a `TInputLine` as a focusable input surface.

**Ergebnis / Result**: Eine sichtbare `TInputLine` enthält Token, Lernziel und
den aktuellen Eingabezustand.

**Übungen / Exercises**:
1. Ändere den Eingabetext per Tastatur. / Change the input text with the keyboard.
2. Spiegle den Eingabezustand in der Statuszeile. / Mirror the input state in the status line.

---

## Schritt 16: Datentransfer und Validierung / Data Transfer and Validation

**Quelle / Source**: `tvguid16.cc`

**Lernziel**: Dialogdaten übertragen, wiederherstellen und ungültige Eingaben
sichtbar ablehnen. / **Learning goal**: Transfer and restore dialog data and
visibly reject invalid input.

**Ergebnis / Result**: Der Dialog zeigt `saved restored` sowie
`validation: rejected -> restored` als textorientierte Zustände.

**Übungen / Exercises**:
1. Füge dem Dialog einen Reset-Button hinzu, der alle Felder auf Standardwerte zurücksetzt, ohne den Dialog zu schließen. / Add a Reset button to the dialog that restores all fields to their default values without closing the dialog.
2. Überschreibe `Valid()` in einer `TDialog`-Unterklasse, um die Eingabe vor dem Schließen zu prüfen (z. B. Name darf nicht leer sein). / Override `Valid()` in a `TDialog` subclass to validate input before closing (e.g. name must not be empty).

---

## Barrierearmer Bedien- und Nachweispfad / Accessible Operation and Proof Path

Alle 16 Lernpfade sind über Token und Tastatur erreichbar. Hauptzustand,
Aktivierung, Status und Beschreibung liegen als Text und gerenderte Zellen vor.
Gemeinsame View-Typen bleiben durch Token, historischen Marker und Zustand
eindeutig. Die Bedeutung hängt nicht nur von Farbe, Maus oder räumlicher Lage ab
und bleibt für Screenreader, Braillezeilen und Textbrowser nachvollziehbar.

All 16 lesson paths are reachable by token and keyboard. Main state, activation,
status, and description are available as text and rendered cells. Shared view
types remain unique through token, historical marker, and state. Meaning does
not depend only on colour, pointer input, or spatial position and remains
understandable for screen readers, Braille displays, and text browsers.

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
