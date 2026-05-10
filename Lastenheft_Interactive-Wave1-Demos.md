# Lastenheft: Interaktive Wave-1-Demos

**Dokument-Status:** Spec-Kit-Eingabedatei, vorbereitet fuer einen Folge-Feature-Lauf nach Wave-1 Functional Hardening
**Erstellt:** 2026-05-10
**Betrifft:** `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`, `tests/TuiVision.Examples.SmokeTests/`, `docs/guides/examples/`
**Empfohlene Prioritaet:** nach `Lastenheft_Wave1-Functional-Hardening.md` und nach Abschluss von `012-interactive-wave2-demos`
**Empfohlener Spec-Kit-Branch:** naechste freie Nummer nach dem Functional-Hardening-Lauf, z. B. `014-interactive-wave1-demos`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10 und das zweistufige Beispielwellen-Liefermuster

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist als direkte Eingabe fuer `/speckit-specify` gedacht. Sie
beschreibt die zweite Stufe fuer Wave 1: Auf den gehaerteten Funktionen aus
`Lastenheft_Wave1-Functional-Hardening.md` sollen echte sichtbare CLI-Demos
aufgebaut werden.

This file is intended as direct input for `/speckit-specify`. It describes the
second stage for wave 1: build real visible CLI demos on top of the hardened
functions from `Lastenheft_Wave1-Functional-Hardening.md`.

- Feature-Ziel: Wave-1-Beispiele beim normalen CLI-Start sichtbar bedienbar
  machen.
- Voraussetzung: Wave-1 Functional Hardening ist abgeschlossen oder als
  belastbare Grundlage verfuegbar.
- Nichtziel: Keine erneute fachliche Komplettportierung; keine neuen
  Beispielwellen; keine breite Framework-Revision.
- Abschlussgrenze: Jedes Wave-1-Beispiel hat sichtbaren Zweck, echten
  Bedienpfad, deterministischen Quit-Pfad, App-Loop-Smoke und aktualisierten
  Guide.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Wave 1 wurde urspruenglich als erste verpflichtende Beispielwelle geliefert.
Die Beispiele sind startbar und smoke-getestet. Nach den spaeteren
Erkenntnissen aus Wave 2 ist aber klarer geworden, dass Beispielprogramme
nicht nur Funktionen beweisen, sondern auch als echte interaktive
Lern-Demos funktionieren muessen.

Wave 1 was delivered as the first mandatory example wave. The examples are
runnable and smoke-tested. Later wave-2 experience made clearer that example
programs should not only prove functions; they should also work as real
interactive learning demos.

Dieses Lastenheft soll die interaktive Runtime-Politur fuer Wave 1 planen.
Es baut auf dem gehaerteten Funktionsnachweis aus
`Lastenheft_Wave1-Functional-Hardening.md` auf. Es soll nicht erneut klaeren,
ob eine historische Kernfunktion korrekt portiert wurde; es soll diese
Funktionen ueber sichtbare Runtime-Pfade bedienbar machen.

This requirements brief plans the interactive runtime polish for wave 1. It
builds on the hardened proof from `Lastenheft_Wave1-Functional-Hardening.md`.
It should not re-decide whether a historical core function was ported
correctly; it should expose those functions through visible runtime paths.

---

## 2. Ziel / Goal

Alle Wave-1-Beispiele sollen beim normalen Start ueber `dotnet run --project`
sichtbar, tastaturbedienbar und fuer Lernende nachvollziehbar sein.

All wave-1 examples shall be visible, keyboard-operable, and understandable for
learners when started through `dotnet run --project`.

Pflicht-Startpfade:

```bash
dotnet run --project examples/Desklogo
dotnet run --project examples/MsgCls
dotnet run --project examples/Tutorial
dotnet run --project examples/Videomode
```

Fuer `Tutorial` muss zusaetzlich die bestehende Token-Auswahl erhalten bleiben,
z. B.:

```bash
dotnet run --project examples/Tutorial -- tvguid01
dotnet run --project examples/Tutorial -- tvguid16
```

---

## 3. Scope / Scope

### 3.1 Beispiele / Examples

- `Desklogo`
- `MsgCls`
- `Tutorial`
- `Videomode`

### 3.2 Zu pruefende C#-Artefakte / C# Artefacts

- `examples/Desklogo/`
- `examples/MsgCls/`
- `examples/Tutorial/`
- `examples/Videomode/`
- `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs`
- `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs`
- `tests/TuiVision.Examples.SmokeTests/TutorialSmokeTests.cs`
- `tests/TuiVision.Examples.SmokeTests/VideomodeSmokeTests.cs`
- `docs/guides/examples/desklogo.md`
- `docs/guides/examples/msgcls.md`
- `docs/guides/examples/tutorial.md`
- `docs/guides/examples/videomode.md`
- `examples/README.md`

### 3.3 Historischer Bezug / Historical Reference

Der historische Bezug bleibt verbindlich, aber in diesem zweiten Schritt wird
er als bereits gehaertete Grundlage verwendet. Wenn im interaktiven Lauf eine
neue sichtbare Abweichung entsteht, muss sie gegen die passenden historischen
Quellen dokumentiert werden:

- `desklogo/desklogo.cc`, plus `set-logo.cc`/`tv_logo.cc` als Asset-/Generator-
  Abgrenzung
- `msgcls/testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h`
- `tutorial/tvguid01.cc` bis `tvguid16.cc`
- `videomode/test.cc`

---

## 4. Kernanforderungen / Core Requirements

### IW1-01: Sichtbarer Zweck beim Start

Jedes Wave-1-Beispiel muss beim normalen CLI-Start sofort zeigen, was es
demonstriert. Die Information muss text-first sein und darf nicht nur ueber
Farbe, Layout oder Code-Kommentare vermittelt werden.

Each wave-1 example must immediately show what it demonstrates when started
from the CLI. The information must be text-first and must not depend only on
color, layout, or code comments.

### IW1-02: Echte Menue-, Status-, Tastatur- oder Command-Pfade

Jedes Beispiel muss mindestens einen echten Bedienpfad besitzen:

- Menuebefehl
- Statuszeilenbefehl
- Tastaturbefehl
- sichtbare Command-Oberflaeche

Der Pfad muss dieselbe Funktion ausloesen, die im Functional-Hardening-Lauf
als fachlich relevant bestaetigt wurde.

### IW1-03: Sichtbare Rueckmeldung nach Aktionen

Nach einer Aktion muss ein beobachtbarer Zustand erscheinen, z. B.:

- Statuszeilentext
- Desktop-Text
- Auswahl- oder Schrittzustand
- Message-Routing-Ergebnis
- Terminalgroessen-/Fallback-Ergebnis
- klare Fehlermeldung

### IW1-04: Deterministischer Quit-Pfad

Jedes Beispiel muss einen deterministischen Quit-Pfad fuer manuelle Nutzung
und Smoke-Tests besitzen. `Ctrl+Q`, `Ctrl+C`, `Alt+X` oder ein sichtbarer
Menuebefehl sind akzeptabel, sofern sie stabil funktionieren.

### IW1-05: UI-/Event-Smokes ueber App-Loop

Die primaeren Smoke-Tests muessen die sichtbaren Bedienpfade ueber den
App-Loop oder einen aequivalenten Runtime-Dispatch mit injizierten Events
ausloesen. Direkte Methoden duerfen nur Setup oder Zusatzbeweis sein.

### IW1-06: Guides DE-first/EN-second

Alle betroffenen Guides muessen den echten Bedienpfad beschreiben:

- Startbefehl
- Bedienhandlung
- erwartete Ausgabe
- A11Y-Hinweis
- historischer Bezug
- bekannte Abweichungen

Deutsch kommt zuerst, Englisch danach; die Sprache bleibt ungefaehr CEFR-B2.

---

## 5. Beispielbezogene Ziele / Per-Example Goals

### Desklogo

`Desklogo` soll als minimale sichtbare Desktop-Demo erhalten bleiben, aber
einen klaren Runtime-Kontext besitzen:

- Zwecktext oder Statushinweis
- sichtbarer Logo-Zustand
- deterministischer Quit-Pfad
- Smoke-Test ueber Start, sichtbaren Zustand und Quit
- Guide-Hinweis zur Asset-/Generator-Entscheidung gegen `set-logo.cc` und
  `tv_logo.cc`

### MsgCls

`MsgCls` soll Message-Routing nicht nur als internen Broadcast beweisen,
sondern ueber einen sichtbaren Bedienpfad ausloesen:

- Menue- oder Tastaturpfad fuer Nachricht ausloesen
- sichtbares Ergebnis im Fenster oder Status
- wiederholter Trigger bleibt stabil
- unerwartete Reihenfolge oder leerer Zustand wird sichtbar behandelt
- Smoke-Test ueber denselben Eventpfad

### Tutorial

`Tutorial` soll die 16 Schritte weiterhin einzeln erkennbar halten und als
Lern-Demo bedienbarer werden:

- erster Bildschirm nennt aktuellen Schritt und Lernziel
- Navigation oder Auswahlpfad fuer Schritte bleibt klar
- jeder Schritt hat sichtbares, schritt-spezifisches Ergebnis
- Smoke-Matrix prueft alle 16 Schritte weiterhin einzeln
- Guide erklaert Bedienung statt nur Quellstruktur

### Videomode

`Videomode` soll den Unterschied zwischen real moeglicher Transition und
sichtbarem Fallback fuer Lernende klar machen:

- Bedienpfad fuer Modus-/Groessenwechsel oder Probe
- sichtbare Rueckmeldung: supported, fallback, rejected oder unchanged
- post-transition usability bleibt erhalten
- Smoke-Test prueft realen oder simulierten Testkontext deterministisch, ohne
  eine falsche echte Terminalfaehigkeit zu behaupten

---

## 6. User Stories fuer Spec-Kit / User Stories for Spec-Kit

### US1: Startable learner-facing Wave-1 demos

Als Lernender moechte ich jedes Wave-1-Beispiel normal starten und sofort
sehen, was es demonstriert, damit ich nicht in einer leeren App-Schale lande.

**Akzeptanz:** Alle vier Beispielbereiche zeigen Zweck, Bedienpfad und
sichtbaren Zustand beim normalen Start.

### US2: Real operation paths

Als manueller Reviewer moechte ich die Kernfunktion jedes Beispiels ueber
Tastatur, Menue, Statuszeile oder Command-Pfad ausloesen, damit ich nicht auf
interne Testmethoden angewiesen bin.

**Akzeptanz:** Jeder Beispielbereich besitzt mindestens einen sichtbaren
Bedienpfad zur fachlichen Kernfunktion.

### US3: App-loop smoke proof

Als Maintainer moechte ich dieselben Bedienpfade per Smoke-Test ueber den
App-Loop pruefen, damit interaktive Runtime-Reife regressionssicher bleibt.

**Akzeptanz:** Primaere Smokes nutzen Event-/Command-/Key-Pfade und zaehlen
Direktmethoden nicht als Hauptbeweis.

### US4: Learner-ready guides

Als sehbehinderter oder textorientiert arbeitender Lernender moechte ich die
Bedienung in Deutsch und Englisch nachvollziehen koennen, damit die Beispiele
auch mit Screenreader, Braille-Zeile und Textbrowser nutzbar bleiben.

**Akzeptanz:** Alle vier Guides und `examples/README.md` beschreiben Bedienung,
Rueckmeldung, A11Y und historischen Bezug.

---

## 7. Erfolgskriterien / Success Criteria

- `dotnet run --project examples/Desklogo` zeigt Zweck, Logo-Zustand und
  stabilen Quit-Pfad.
- `dotnet run --project examples/MsgCls` besitzt einen sichtbaren
  Nachrichtentrigger und sichtbares Routing-Ergebnis.
- `dotnet run --project examples/Tutorial` und die 16 Tokenpfade bleiben
  startbar, unterscheidbar und smoke-geprueft.
- `dotnet run --project examples/Videomode` zeigt reale Transition oder
  sichtbaren Fallback ohne falsche Faehigkeitsbehauptung.
- Primaere Wave-1-Smokes laufen ueber sichtbare Runtime-/Eventpfade.
- Guides und `examples/README.md` sind auf den tatsaechlichen Bedienpfad
  aktualisiert.
- A11Y-/Text-first-Anforderungen bleiben erfuellt.
- Historische Abweichungen, die durch die interaktive Politur neu sichtbar
  werden, sind dokumentiert.

---

## 8. Nichtziele / Non-Goals

- keine erneute funktionale Komplettportierung der Wave-1-Beispiele
- keine Wave-2-/Wave-3-/Wave-4-Arbeit
- keine Pflicht zur Mausbedienung
- keine breite Framework-Revision
- keine Bearbeitung historischer Dateien unter `tv203s/`
- keine generierten DocFX-Artefakte im Commit

---

## 9. Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artefacts

Der spaetere Spec-Kit-Lauf soll mindestens erzeugen oder aktualisieren:

- `specs/<NNN-interactive-wave1-demos>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/interactive-wave1-demo-acceptance.md`
- `tasks.md`
- `pr-evidence.md`

`tasks.md` soll die Reihenfolge ausdruecklich so schneiden:

1. Functional-Hardening-Evidence pruefen.
2. Einen kleinen Vertical Slice waehlen, vorzugsweise `MsgCls` oder
   `Desklogo`.
3. Sichtbaren Bedienpfad und App-loop-Smoke fuer den Slice liefern.
4. `Tutorial` und `Videomode` mit demselben Muster nachziehen.
5. Guides, README, A11Y, Statistik, Pflichtenheft und PR-Evidence
   aktualisieren.

---

## 10. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
Erstelle eine Spec-Kit-Spezifikation fuer interaktive Wave-1-Demos.

Nutze `Lastenheft_Interactive-Wave1-Demos.md` als verbindliche Eingabe. Der
Feature-Lauf soll nach dem Wave-1 Functional Hardening stattfinden und auf
dessen gehaerteten Funktionen aufbauen.

Scope:
- `dotnet run --project examples/Desklogo`
- `dotnet run --project examples/MsgCls`
- `dotnet run --project examples/Tutorial`
- `dotnet run --project examples/Videomode`

Jedes Beispiel braucht sichtbaren Zweck beim Start, echten Menue-/Status-/
Tastatur-/Command-Pfad, deterministischen Quit-Pfad, UI-/Event-Smoke ueber den
App-Loop und DE-first/EN-second Guide-Updates mit Bedienpfad, erwarteter
Ausgabe, A11Y-Hinweis und historischem Bezug.

Direktmethoden duerfen nur Setup oder Zusatzbeweis sein. Primaere Akzeptanz
erfolgt ueber sichtbare Runtime-Pfade. Neue historische Abweichungen sind
gegen die C/C++-Quellen aus `tv203s/contrib/tvision/examples/` in Guide oder
PR-Evidence zu dokumentieren.
```
