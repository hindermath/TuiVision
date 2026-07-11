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

Das Programm öffnet ein Nachrichtenfenster auf dem Desktop. `Message -> Post`
sendet eine Nachricht über den Command- und Broadcast-Pfad. Der Vorgang kann
wiederholt werden. Die Statuszeile meldet den letzten sichtbaren Zustand;
`Help -> Description` erklärt die View-Hierarchie.

The program opens a message window on the desktop. `Message -> Post` sends a
message through the command and broadcast path. The operation can be repeated.
The status line reports the latest visible state; `Help -> Description` explains
the view hierarchy.

---

## Trigger-Ablauf / Trigger Flow

1. Der Befehl `cmPostLoremIpsum` erreicht `MsgClsApp.HandleEvent()`.
2. `MsgClsApp.PostMessage(text)` erzeugt ein Broadcast-Ereignis mit `MsgClsEvents.cmPostToMsgWindow`.
3. Das Ereignis wird über `HandleEvent()` an die View-Hierarchie gesendet.
4. `MsgClsWindow.HandleEvent()` fängt den Broadcast ab, ergänzt `Messages` und macht das Ergebnis sichtbar.
5. Die Anwendung aktualisiert die Statuszeile; Wiederholungen bleiben in Empfangsreihenfolge erhalten.

1. The `cmPostLoremIpsum` command reaches `MsgClsApp.HandleEvent()`.
2. `MsgClsApp.PostMessage(text)` creates a broadcast event with `MsgClsEvents.cmPostToMsgWindow`.
3. The event is sent through the view hierarchy via `HandleEvent()`.
4. `MsgClsWindow.HandleEvent()` intercepts the broadcast, adds to `Messages`, and makes the result visible.
5. The application updates the status line; repeated messages keep receive order.

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

## Nachweisstatus / Proof Status

Der funktionale Nachweis für `014-wave1-functional-hardening` prüft den
echten Command- und Broadcast-Pfad. Ein Smoke-Test sendet den
`cmPostLoremIpsum`-Befehl an `MsgClsApp.HandleEvent()`, danach muss
`MsgClsWindow.Messages` den Text `Lorem Ipsum dolor sit amet.` enthalten.
Weitere Tests prüfen die Headless-Initialnachricht und wiederholtes
`PostMessage()` in Empfangsreihenfolge.

The functional proof for `014-wave1-functional-hardening` checks the real
command and broadcast path. A smoke test sends the `cmPostLoremIpsum` command
to `MsgClsApp.HandleEvent()`, after which `MsgClsWindow.Messages` must contain
`Lorem Ipsum dolor sit amet.` Further tests verify the headless startup message
and repeated `PostMessage()` calls in receive order.

`017-wave1-visual-component-remediation` prüft zusätzlich den vollständigen
App-Loop. Der primäre Test stellt den Befehl und `Help -> Description` in die
Ereignisfolge, prüft die konkrete Nachrichtenliste, den sichtbaren `TWindow`-Typ,
die Beschreibungsregion und die gerenderte Statuszeile. Direkte Helfer sind für
diesen primären Nachweis nicht erforderlich.

`017-wave1-visual-component-remediation` also verifies the complete app loop.
The primary test queues the command and `Help -> Description`, then verifies the
concrete message list, visible `TWindow` type, description region, and rendered
status line. Direct helpers are not needed for this primary proof.

## Barrierearmer Bedienpfad / Accessible Operation Path

Posten, Wiederholen, Status, Beschreibung und Beenden sind per Tastatur
erreichbar. Reihenfolge und Ergebnis werden als Text dargestellt und hängen
nicht nur von Fokusfarbe, Maus oder Fensterposition ab.

Posting, repeating, status, description, and quitting are keyboard reachable.
Order and result are presented as text and do not depend only on focus colour,
pointer input, or window position.

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
