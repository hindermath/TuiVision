# Lastenheft: Wave-1 Functional Hardening

**Dokument-Status:** Spec-Kit-Eingabedatei, vorbereitet fuer einen Folge-Feature-Lauf nach `012-interactive-wave2-demos`
**Erstellt:** 2026-05-10
**Betrifft:** `examples/Desklogo/`, `examples/MsgCls/`, `examples/Tutorial/`, `examples/Videomode/`, `tests/TuiVision.Examples.SmokeTests/`, `docs/guides/examples/`
**Empfohlene Prioritaet:** nach Abschluss von `012-interactive-wave2-demos`, vor einer interaktiven Wave-1-Demo-Politur
**Empfohlener Spec-Kit-Branch:** naechste freie Nummer, z. B. `013-wave1-functional-hardening`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10 und das zweistufige Beispielwellen-Liefermuster

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist als direkte Eingabe fuer `/speckit-specify` gedacht. Sie
beschreibt die erste Stufe eines nachtraeglichen Wave-1-Qualitaetslaufs:
zuerst die bereits portierten Wave-1-Funktionen fachlich gegen die
historischen C/C++-Quellen pruefen, Abweichungen sichtbar dokumentieren und
Tests dort haerten, wo bisher nur Startup-, String- oder Headless-Hilfspfade
beweisen.

This file is intended as direct input for `/speckit-specify`. It describes the
first stage of a follow-up quality pass for wave 1: first review the already
ported wave-1 functions against the historical C/C++ sources, document
deviations, and harden tests where the current proof relies too much on
startup, string, or headless-helper paths.

- Feature-Ziel: Wave-1-Funktionen gegen historische Quellen haerten.
- Folgefeature: `Lastenheft_Interactive-Wave1-Demos.md` baut danach auf diesem
  gehaerteten Funktionsstand auf.
- Nichtziel: Keine interaktive Demo-Politur in diesem Feature; keine neue
  Wave-2-/Wave-3-/Wave-4-Funktionalitaet.
- Abschlussgrenze: Fuer jedes Wave-1-Beispiel ist klar, welche historische
  Kernfunktion portiert, ersetzt oder bewusst ausgelassen ist, und die
  vorhandenen Smokes pruefen relevante Funktion statt nur Oberflaechenpraesenz.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Wave 1 wurde in `007-port-wave1-examples` geliefert und umfasst:

- `Desklogo`
- `MsgCls`
- `Tutorial` mit `tvguid01` bis `tvguid16`
- `Videomode`

Die Beispiele bauen, besitzen Smoke-Tests und Guides. Nach den Erkenntnissen
aus `011-port-wave2-examples` und `012-interactive-wave2-demos` ist aber
sinnvoll, Wave 1 nachtraeglich mit demselben zweistufigen Muster zu betrachten:
erst Funktions- und Nachweis-Haertung, danach interaktive Demo-Reife.

Wave 1 was delivered in `007-port-wave1-examples` and includes `Desklogo`,
`MsgCls`, the full `Tutorial` sequence from `tvguid01` through `tvguid16`, and
`Videomode`. The examples build, have smoke tests, and have guides. The
lessons from `011` and `012` suggest applying the same two-stage model to wave
1 retroactively: first harden function and proof quality, then improve
interactive demo readiness.

Die aktuelle Welle-1-Basis ist nicht als falsch zu behandeln. Dieser Lauf soll
keine bestehende Abnahme zuruecknehmen. Er soll pruefen, ob die bisher
gelieferten Funktionen fachlich belastbar genug sind, damit spaeter sichtbare
interaktive Demos darauf aufgebaut werden koennen.

The current wave-1 baseline is not considered wrong. This work must not revoke
existing acceptance. It should check whether the delivered functions are
strong enough to serve as a base for later visible interactive demos.

---

## 2. Ziel / Goal

Die Wave-1-Beispiele sollen fachlich gegen die historischen Quellen
nachgeprueft und dort gehaertet werden, wo die C#-Portierung oder die Tests
zu wenig ueber das tatsaechliche historische Beispielverhalten aussagen.

The wave-1 examples shall be reviewed against the historical sources and
hardened where the C# port or tests say too little about the actual historical
example behavior.

Der Lauf soll insbesondere beantworten:

- Ist jede historische Kernfunktion bewusst portiert, ersetzt oder ausgelassen?
- Sind die C#-Funktionen wirklich durch Tests abgesichert oder nur sichtbar
  vorhanden?
- Nutzen Smokes echte relevante Verhalten oder nur einfache Startup- und
  String-Pruefungen?
- Sind Abweichungen in Guide oder Evidence nachvollziehbar dokumentiert?
- Gibt es Headless- oder Helper-Pfade, die zu viel beweisen und dadurch echte
  Runtime-Luecken verdecken?

---

## 3. Scope und historische Quellen / Scope and Historical Sources

| Beispiel | Zu pruefende historische Quellen | Prueffokus |
|---|---|---|
| `Desklogo` | `tv203s/contrib/tvision/examples/desklogo/desklogo.cc`; bewusst auch `set-logo.cc` und `tv_logo.cc` als Asset-/Generator-Abgrenzung | Logo-Quelle, Asset-Entscheidung, minimale Desktop-App, Terminalgroessen-Fallback |
| `MsgCls` | `tv203s/contrib/tvision/examples/msgcls/testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h` | Benutzerdefinierte Nachrichtenklasse, Broadcast-/Routing-Verhalten, wiederholte Trigger, unerwartete Reihenfolge |
| `Tutorial` | `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc` bis `tvguid16.cc` | 16 einzelne Lernziele, Token-Auswahl, Sequenz, Schritt-spezifische Funktion statt nur Textanzeige |
| `Videomode` | `tv203s/contrib/tvision/examples/videomode/test.cc` | Real moegliche Terminalgroessen-/Modusreaktion, expliziter sichtbarer Fallback, post-transition usability |

Build-Hilfsdateien wie `.bmk`, `.mkf`, `.imk`, `.umk`, `.gpr` und `rhide.env`
sind nur Kontext und nicht automatisch Pflichtumfang. Sie muessen nur dann
einbezogen werden, wenn sie fuer Asset-Herkunft, Startparameter oder
historische Intent-Abgrenzung wichtig sind.

Build helper files such as `.bmk`, `.mkf`, `.imk`, `.umk`, `.gpr`, and
`rhide.env` are context only and are not automatically required scope. They
should be considered only when they matter for asset origin, launch arguments,
or historical intent.

---

## 4. Zu pruefende C#-Artefakte / C# Artefacts to Review

Dieser Lauf soll begrenzt und strukturiert bleiben. Zu pruefen sind:

- alle Example-App-Klassen unter `examples/Desklogo/`, `examples/MsgCls/`,
  `examples/Tutorial/` und `examples/Videomode/`
- alle zugehoerigen Smoke-Tests:
  `DesklogoSmokeTests.cs`, `MsgClsSmokeTests.cs`,
  `TutorialSmokeTests.cs`, `VideomodeSmokeTests.cs`
- alle Wave-1-Guides:
  `docs/guides/examples/desklogo.md`,
  `docs/guides/examples/msgcls.md`,
  `docs/guides/examples/tutorial.md`,
  `docs/guides/examples/videomode.md`
- alle Hilfs-/Proof-Methoden, die von diesen Smokes genutzt werden
- die wenigen Framework-/Driver-Erweiterungen, die Wave 1 direkt beruehrt,
  besonders `Videomode`-/Terminalgroessen-Faehigkeiten und Message-Routing

Nicht pauschal neu zu auditieren ist das gesamte `src/`-Verzeichnis. Ein
Framework- oder Driver-Pfad wird nur dann Bestandteil dieses Features, wenn
ein Wave-1-Beispiel ihn nachweisbar direkt braucht.

---

## 5. Funktionale Anforderungen / Functional Requirements

### FH-01: Historische Quellenpruefung je Beispiel

Jedes Wave-1-Beispiel muss gegen die in Abschnitt 3 genannten historischen
Quellen geprueft werden. Das Ergebnis muss in einem Evidence-Artefakt
festhalten:

- historische Kernfunktion
- C#-Abbildung
- Tests, die diese Abbildung pruefen
- bewusste Abweichungen oder ausgelassene Hilfsprogramme

### FH-02: Tests pruefen fachliches Verhalten

Smoke-Tests duerfen nicht nur bestaetigen, dass ein Beispiel startet oder ein
statischer Text vorkommt. Fuer jedes Beispiel muss mindestens ein Test eine
fachlich relevante Kernfunktion pruefen.

Examples:

- `Desklogo`: Logo-Inhalt, Desktop-Platzierung oder begruendeter Fallback
- `MsgCls`: Nachrichtentrigger, Routing und sichtbares Ergebnis
- `Tutorial`: je Schritt ein spezifisches Lernziel oder definierendes Verhalten
- `Videomode`: reale Transition oder sichtbarer Fallback mit nachvollziehbarem
  Ergebnis

### FH-03: Headless- und Helper-Pfade werden klassifiziert

Alle Hilfs- und Headless-Pfade muessen klassifiziert werden:

- `SetupOnly`: bereitet Testzustand vor
- `PrimaryProof`: prueft die eigentliche Beispiel-Funktion
- `SupplementalProof`: ergaenzt den sichtbaren oder funktionalen Beweis
- `LegacyOrTemporary`: soll spaeter durch interaktive Eventpfade ersetzt werden

Wenn ein Helper aktuell als `PrimaryProof` wirkt, muss klar sein, ob das fuer
die Funktionshaertung akzeptabel ist oder im interaktiven Folgefeature ersetzt
werden muss.

### FH-04: Abweichungen werden dokumentiert

Jede bewusst andere C#-Umsetzung gegenueber der historischen Quelle muss in
Guide oder Evidence dokumentiert werden. Das gilt besonders fuer:

- eingebettetes Logo statt historischer Asset-Generator-Lauf
- moderne Terminalgroessen-Grenzen bei `Videomode`
- tutorial-Schritte, die wegen anderer Framework-Reife nur didaktisch
  angenaehert sind
- Message-Routing, das im Managed Framework anders strukturiert ist

### FH-05: Keine interaktive Demo-Politur im Scope

Dieses Feature darf vorbereiten und klassifizieren, welche Funktionen spaeter
interaktiv verdrahtet werden muessen. Es soll aber noch keine neuen
Menues, Statuszeilen, Desktop-Control-Flows oder UI-Event-Smokes als
Abschlussziel erzwingen. Das gehoert in `Lastenheft_Interactive-Wave1-Demos.md`.

### FH-06: Evidence und Guides bleiben lernbar

Guide- und Evidence-Ergaenzungen muessen Deutsch zuerst und Englisch danach
liefern, auf CEFR-B2-Niveau bleiben und fuer Screenreader, Braille-Zeilen und
Textbrowser verstaendlich sein.

---

## 6. User Stories fuer Spec-Kit / User Stories for Spec-Kit

### US1: Historical proof matrix for Wave 1

Als Maintainer moechte ich fuer jedes Wave-1-Beispiel eine klare Matrix aus
historischer Quelle, C#-Abbildung, Testnachweis und Abweichung sehen, damit
ich die spaetere interaktive Demo-Stufe auf belastbare Funktionen stuetze.

**Akzeptanz:** Vier Beispielbereiche und alle 16 Tutorial-Schritte sind in der
Matrix enthalten.

### US2: Hardened smoke proof

Als Reviewer moechte ich sehen, dass Smoke-Tests fachliche Kernfunktionen
pruefen, damit Wave 1 nicht nur als startbare App, sondern als portiertes
Beispiel abgesichert ist.

**Akzeptanz:** Jeder Wave-1-Bereich hat mindestens einen fachlich relevanten
Smoke-Beweis; `Tutorial` hat weiterhin 16 einzeln nachvollziehbare
Schrittbeweise.

### US3: Helper classification

Als spaeterer  interaktiver Demo-Implementierer moechte ich wissen, welche
Hilfsmethoden echte Funktion pruefen und welche nur Testkomfort sind, damit
ich sie im zweiten Schritt korrekt ueber UI-Eventpfade ersetzen oder
weiterverwenden kann.

**Akzeptanz:** Alle von Wave-1-Smokes genutzten Hilfs-/Proof-Methoden sind
klassifiziert.

### US4: Learner-facing traceability

Als Lernender moechte ich in den Guides erkennen, welche historische Funktion
das Beispiel zeigt und wo moderne Abweichungen bewusst sind, damit ich die
Portierung nicht mit dem Original verwechseln muss.

**Akzeptanz:** Die vier Guides enthalten DE-first/EN-second Traceability- und
Abweichungshinweise.

---

## 7. Erfolgskriterien / Success Criteria

- 100 % der vier Wave-1-Beispielbereiche haben eine dokumentierte
  historische Quellenpruefung.
- 16/16 Tutorial-Schritte bleiben einzeln auffindbar, startbar und
  smoke-geprueft.
- Jeder Wave-1-Beispielbereich besitzt mindestens einen Smoke-Test, der eine
  fachliche Kernfunktion statt nur Startup oder statischen Text prueft.
- Alle Wave-1-Hilfs-/Proof-Methoden, die in Smokes genutzt werden, sind
  klassifiziert.
- Alle bewussten historischen Abweichungen sind in Guide oder Evidence
  dokumentiert.
- Das Feature erzeugt keine neuen interaktiven Demo-Pflichten; es bereitet
  diese fuer das naechste Lastenheft vor.

---

## 8. Nichtziele / Non-Goals

- keine neue Wave-2-/Wave-3-/Wave-4-Arbeit
- keine breite Framework-Revision
- keine Pflicht zur Mausunterstuetzung
- keine bitgenaue Replikation jeder historischen Plattformbesonderheit
- keine interaktive Runtime-Politur als Abschlussziel
- keine Bearbeitung von Dateien unter `tv203s/`

---

## 9. Erwartete Spec-Kit-Artefakte / Expected Spec-Kit Artefacts

Der spaetere Spec-Kit-Lauf soll mindestens erzeugen oder aktualisieren:

- `specs/<NNN-wave1-functional-hardening>/spec.md`
- `plan.md`
- `research.md`
- `data-model.md`
- `quickstart.md`
- `contracts/wave1-functional-hardening-acceptance.md`
- `tasks.md`
- `pr-evidence.md`

`tasks.md` soll pro Beispiel die historische Quellenpruefung, Smoke-Haertung,
Guide-/Evidence-Aktualisierung und finalen Validierungsnachweis getrennt
sichtbar machen.

---

## 10. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
Erstelle eine Spec-Kit-Spezifikation fuer Wave-1 Functional Hardening.

Nutze `Lastenheft_Wave1-Functional-Hardening.md` als verbindliche Eingabe.
Der Feature-Lauf soll nach `012-interactive-wave2-demos` stattfinden und die
bereits gelieferten Wave-1-Beispiele gegen ihre historischen C/C++-Quellen
haerten, ohne schon die interaktive Demo-Politur zu liefern.

Scope:
- `Desklogo` gegen `desklogo.cc`, plus `set-logo.cc`/`tv_logo.cc` als
  Asset-/Generator-Abgrenzung
- `MsgCls` gegen `testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h`
- `Tutorial` gegen `tvguid01.cc` bis `tvguid16.cc`
- `Videomode` gegen `test.cc`

Die Spezifikation muss pruefen lassen, ob jede historische Kernfunktion
bewusst portiert, ersetzt oder ausgelassen ist; ob Smoke-Tests echte
fachliche Funktionen statt nur Startup/String-Praesenz pruefen; ob
Headless-/Helper-Pfade klassifiziert sind; und ob Abweichungen in Guide oder
PR-Evidence dokumentiert sind.

Nicht im Scope: interaktive Menue-/Status-/UI-Event-Demo-Politur. Diese folgt
spaeter ueber `Lastenheft_Interactive-Wave1-Demos.md`.
```
