# MsgCls — Anleitung / Guide

> **Quelle / Source**: `tv203s/contrib/tvision/examples/msgcls/` (Hauptprogramm: `testdyn.cpp`, Klassen: `tlnmsg.cpp`/`tlnmsg.h`)
> **Wave**: 1 — Pflichtbeispiel aus dem Originalordner `tv203s/contrib/tvision/examples/`
> **Nicht** Bestandteil von `TVDEMOS/` oder `TVFM/`.

---

## Lernziel / Learning Goal

Dieses Beispiel demonstriert benutzerdefiniertes Nachrichten-Routing durch das
TuiVision-Broadcast-Ereignissystem. Ein `MsgClsWindow` empfängt Broadcast-Ereignisse
mit einem benutzerdefinierten Befehlscode und akkumuliert die enthaltenen Texte.

This example demonstrates custom message routing through the TuiVision broadcast event system.
A `MsgClsWindow` receives broadcast events with a custom command code and accumulates
the contained texts.

---

## Voraussetzungen / Prerequisites

- **.NET 10 SDK** installiert — lade es von [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) herunter und installiere es, bevor du weitermachst. /
  **.NET 10 SDK** installed — download and install it from [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/10.0) before proceeding.

- **TuiVision-Repository** geklont — führe folgenden Befehl aus, um das Repository lokal verfügbar zu machen:
  `git clone https://github.com/hindermath/TuiVision.git` /
  **TuiVision repository** cloned — run the following command to get a local copy:
  `git clone https://github.com/hindermath/TuiVision.git`

- **C#-Grundkenntnisse** — ein grundlegendes Verständnis von C# ist hilfreich; eine gute Einführung findest du auf [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/). /
  **Basic C# knowledge** — a foundational understanding of C# is helpful; a good starting point is [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/).

- **TuiVision-Ereignissystem** — Kenntnisse des Ereignismodells erleichtern das Verständnis erheblich; für eine tiefere Lektüre sieh dir `src/TuiVision.Core/TEvent.cs` im Repository an. /
  **TuiVision event system** — familiarity with the event model is recommended; for deeper reading, refer to `src/TuiVision.Core/TEvent.cs` in the repository.

---

## Starten / Startup

```bash
dotnet run --project examples/MsgCls
```

Das Programm öffnet ein Nachrichtenfenster auf dem Desktop. Nachrichten können
über den Menüeintrag (wenn vorhanden) oder programmatisch über `PostMessage()` gepostet werden.

The program opens a message window on the desktop. Messages can be posted via the menu entry
(if present) or programmatically via `PostMessage()`.

---

## Trigger-Ablauf / Trigger Flow

1. `MsgClsApp.PostMessage(text)` erzeugt ein Broadcast-Ereignis mit `MsgClsEvents.cmPostToMsgWindow`.
2. Das Ereignis wird über `HandleEvent()` an alle Kindansichten der Anwendung gesendet.
3. `MsgClsWindow.HandleEvent()` fängt den Broadcast ab und fügt den Text zu `Messages` hinzu.

1. `MsgClsApp.PostMessage(text)` creates a broadcast event with `MsgClsEvents.cmPostToMsgWindow`.
2. The event is sent to all child views of the application via `HandleEvent()`.
3. `MsgClsWindow.HandleEvent()` intercepts the broadcast and adds the text to `Messages`.

---

## Nachrichten-Routing — Erklärung / Message Routing Explanation

```
MsgClsApp.PostMessage("Hallo")
  → TEvent.CreateBroadcast(cmPostToMsgWindow, "Hallo")
  → MsgClsApp.HandleEvent()
  → TGroup.HandleEvent() → alle Kinder / all children
  → MsgClsWindow.HandleEvent() ← fängt ab / intercepts
  → Messages.Add("Hallo")
```

Der Schlüssel: Broadcast-Ereignisse durchlaufen die gesamte View-Hierarchie. Jede Ansicht,
die den Befehlscode kennt, kann reagieren — ohne direkte Objektreferenz.

The key: broadcast events traverse the entire view hierarchy. Any view that knows the command code
can respond — without a direct object reference.

---

## Architekturhinweise / Architecture Hints

```
MsgClsApp (TApplication)
├── MsgClsWindow (TWindow)    ← empfängt Broadcasts / receives broadcasts
│   └── Messages: List<string>
└── PostMessage(text)         ← öffentliche API / public API
```

- `MsgClsEvents.cmPostToMsgWindow = 201` — benutzerdefinierter Befehlscode
- Die Headless-Smoke-Seam: Im Headless-Modus postet der Konstruktor sofort eine Testnachricht.

- `MsgClsEvents.cmPostToMsgWindow = 201` — custom command code
- The headless smoke seam: In headless mode the constructor immediately posts a test message.

---

## Übungen / Exercises

1. Fügen Sie eine zweite Fensterkategorie hinzu (z. B. Fehlermeldungen in Rot).
   Add a second window category (e.g., error messages in red).

2. Persistieren Sie die Nachrichten in einer Datei beim Beenden.
   Persist the messages to a file on exit.

3. Testen Sie, was passiert, wenn das Nachrichtenfenster geschlossen wird und dann
   eine weitere Nachricht gepostet wird.
   Test what happens when the message window is closed and another message is then posted.

---

## Quellenrückverfolgung / Source Traceability

| Verwaltete Datei / Managed File | Historische Quelle / Historical Source |
|---|---|
| `examples/MsgCls/MsgClsWindow.cs` | `tv203s/contrib/tvision/examples/msgcls/tlnmsg.cpp` + `tlnmsg.h` |
| `examples/MsgCls/MsgClsApp.cs` | `tv203s/contrib/tvision/examples/msgcls/testdyn.cpp` |
| `examples/MsgCls/MsgClsEvents.cs` | Befehlskonstanten aus `testdyn.cpp` / command constants from `testdyn.cpp` |
