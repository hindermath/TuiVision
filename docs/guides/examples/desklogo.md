# Desklogo — Anleitung / Guide

> **Quelle / Source**: `tv203s/contrib/tvision/examples/desklogo/` (Hauptprogramm: `desklogo.cc`)
> **Wave**: 1 — Pflichtbeispiel aus dem Originalordner `tv203s/contrib/tvision/examples/`
> **Nicht** Bestandteil von `TVDEMOS/` oder `TVFM/`.

---

## Lernziel / Learning Goal

Dieses Beispiel zeigt, wie man den TuiVision-Desktop durch eine benutzerdefinierte
`TDesktop`-Unterklasse ersetzt, die ein statisches ASCII-Logo als Hintergrundmuster zeichnet.
Damit lernt man die Grundlage jeder TuiVision-Anwendung: `TApplication` starten, Desktop
anpassen und sauber beenden.

This example shows how to replace the TuiVision desktop with a custom `TDesktop` subclass
that draws a static ASCII logo as background pattern. This teaches the foundation of every
TuiVision application: starting `TApplication`, customising the desktop, and exiting cleanly.

---

## Voraussetzungen / Prerequisites

- **.NET 10 SDK** installiert — lade es von [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0)
  herunter und installiere es, bevor du das Projekt baust oder ausführst. /
  **.NET 10 SDK** installed — download and install it from
  [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) before building or running the project.

- **TuiVision-Repository geklont** — klone das Repository mit
  `git clone https://github.com/hindermath/TuiVision.git` und öffne das Verzeichnis in deiner IDE oder im Terminal. /
  **TuiVision repository cloned** — clone the repository with
  `git clone https://github.com/hindermath/TuiVision.git` and open the directory in your IDE or terminal.

- **Grundkenntnisse in C#** — falls du noch neu in C# bist, bietet
  [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/) einen kostenlosen Einstiegskurs. /
  **Basic knowledge of C#** — if you are new to C#, the free course on
  [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/) is a good starting point.

- **Vertrautheit mit dem TuiVision-Ereignissystem** — schau dir `src/TuiVision.Core/TEvent.cs` im Repository an,
  um zu verstehen, wie Ereignisse erzeugt und verarbeitet werden. /
  **Familiarity with the TuiVision event system** — review `src/TuiVision.Core/TEvent.cs` in the repository
  to understand how events are created and processed.

---

## Starten / Startup

```bash
dotnet run --project examples/Desklogo
```

Das Programm startet mit dem eingebetteten Logo, einer echten Statuszeile und dem
Menü `Help -> Description`. Die Statuszeile nennt den Logo- oder Clipping-Zustand
sowie den Beenden-Pfad. `Alt-X` oder `Ctrl-Q` beendet die Anwendung.

The program starts with the embedded logo, a real status line, and the
`Help -> Description` menu. The status line names the logo or clipping state and
the quit path. `Alt-X` or `Ctrl-Q` exits the application.

**Verhalten bei zu kleinen Terminals / Behaviour on undersized terminals**:
Wenn das Terminal kleiner als das Logo ist (weniger als 64 Spalten oder 9 Zeilen),
wird das Logo abgeschnitten dargestellt. Die Anwendung bleibt dabei stabil und
sauber beendbar — keine Fehlerbehandlung nötig.

If the terminal is smaller than the logo (fewer than 64 columns or 9 rows),
the logo is clipped. The application remains stable and cleanly exitable — no error handling needed.

---

## Architekturhinweise / Architecture Hints

```
DesklogoApp (TApplication)
└── DesklogoDesktop (TDesktop)      ← überschreibt InitDesktop / overrides InitDesktop
    └── LogoLines: string[]         ← eingebettetes Logo-Muster / embedded logo pattern
```

- `DesklogoApp` überschreibt `InitDesktop()`, um einen `DesklogoDesktop` zurückzugeben.
- `DesklogoDesktop` speichert das Logo in `LogoLines[]`.
- Der Headless-Smoke-Seam: `DesklogoApp(bounds, headless: true)` gibt beim ersten
  `GetEvent()`-Aufruf einen `cmQuit`-Befehl zurück und beendet sich sauber.

- `DesklogoApp` overrides `InitDesktop()` to return a `DesklogoDesktop`.
- `DesklogoDesktop` stores the logo in `LogoLines[]`.
- The headless smoke seam: `DesklogoApp(bounds, headless: true)` returns a `cmQuit` command
  on the first `GetEvent()` call and exits cleanly.

---

## Nachweisstatus / Proof Status

Der funktionale Nachweis für `014-wave1-functional-hardening` prüft mehr als
den Start. Die Smoke-Tests bestaetigen, dass `DesklogoDesktop` ein breites
Logo-Muster rendert, dass die eingebetteten `LogoLines` die historischen
Generator-Dateien ersetzen, und dass ein kleines Terminal das Logo kontrolliert
abschneidet. `set-logo.cc` und `tv_logo.cc` bleiben historische
Asset-/Generator-Kontexte und werden nicht als Runtime-Abhaengigkeit portiert.

The functional proof for `014-wave1-functional-hardening` checks more than
startup. The smoke tests confirm that `DesklogoDesktop` renders a wide logo
pattern, that the embedded `LogoLines` replace the historical generator files,
and that a small terminal clips the logo in a controlled way. `set-logo.cc` and
`tv_logo.cc` remain historical asset/generator context and are not ported as a
runtime dependency.

`017-wave1-visual-component-remediation` ergänzt den sichtbaren Nachweis. Der
primäre Smoke-Test führt `DesklogoApp.Run()` aus und prüft den Typ
`DesklogoDesktop`, Block-Glyphen im Terminalpuffer und den gerenderten
Statuszeilentext. Ein zweiter App-Loop-Pfad öffnet `Help -> Description` und
beweist das Beschreibungsfenster, ohne das eingebettete Logo künstlich zu ändern.

`017-wave1-visual-component-remediation` adds visible proof. The primary smoke
test runs `DesklogoApp.Run()` and verifies the `DesklogoDesktop` type, block
glyphs in the terminal buffer, and rendered status-line text. A second app-loop
path opens `Help -> Description` and proves the description window without
artificially changing the embedded logo.

## Barrierearmer Bedienpfad / Accessible Operation Path

Logo, Clipping-Zustand, Status und Beschreibung sind als Text und Terminalzellen
verfügbar. Keine wesentliche Aussage hängt nur von Farbe, Maus oder räumlicher
Anordnung ab. Bei kleinen Terminals bleibt ein kontrolliert abgeschnittener Teil
des Logos sichtbar; die Beschreibung erklärt die historische Generatorgrenze.

Logo, clipping state, status, and description are available as text and terminal
cells. No essential statement depends only on colour, pointer input, or spatial
layout. On small terminals, a controlled clipped part of the logo remains
visible; the description explains the historical generator boundary.

---

## Übungen / Exercises

1. Ändern Sie das Logo-Muster in `DesklogoDesktop.LogoLines[]`.
   Change the logo pattern in `DesklogoDesktop.LogoLines[]`.

2. Fügen Sie eine Menüleiste hinzu, indem Sie `InitMenuBar()` in `DesklogoApp` überschreiben.
   Add a menu bar by overriding `InitMenuBar()` in `DesklogoApp`.

3. Ersetzen Sie das Logo durch eine animierte Version mit einem Timer-Ereignis.
   Replace the logo with an animated version using a timer event.

---

## Quellenrückverfolgung / Source Traceability

| Verwaltete Datei / Managed File | Historische Quelle / Historical Source |
|---|---|
| `examples/Desklogo/DesklogoApp.cs` | `tv203s/contrib/tvision/examples/desklogo/desklogo.cc` (Klasse `TApp`) |
| `examples/Desklogo/DesklogoDesktop.cs` | `tv203s/contrib/tvision/examples/desklogo/desklogo.cc` (Klasse `TNewDeskTop`) |

Die Hilfsdateien `set-logo.cc` und `tv_logo.cc` (Logo-Generatoren) sind in der verwalteten
Portierung **nicht erforderlich**, da das Logo als Zeichenkettenkonstante eingebettet ist.

The helper files `set-logo.cc` and `tv_logo.cc` (logo generators) are **not required**
in the managed port, as the logo is embedded as a string constant.
