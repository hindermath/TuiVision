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

Das Programm startet, zeigt das Logo auf dem Desktop und wartet auf `Alt-X` zum Beenden.
The program starts, displays the logo on the desktop, and waits for `Alt-X` to quit.

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
