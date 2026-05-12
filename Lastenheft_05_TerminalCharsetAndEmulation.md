# Lastenheft: Terminal-, Zeichensatz- und Emulations-Haertung fuer Beispielwelle 4

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-03-29
**Betrifft:** `src/TuiVision.Drivers.Console/`, `src/TuiVision.Compatibility/`, `src/TuiVision.Controls/`, `tests/TuiVision.Drivers.Tests/`, `tests/TuiVision.Examples.SmokeTests/`
**Empfohlene Prioritaet:** jetzt anlegen, aber erst nach Wave-3-Basis abarbeiten

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Die Phase-7-Treiberkonsolidierung hat die historische `.cc`-Landschaft auf
verwaltete Faehigkeitsbuckets verdichtet. Fuer Wave 4 reicht diese Konsolidierung
allein jedoch nicht aus: `terminal`, `eterm`, `xterm`, `fonts` und `cyrillic`
pruefen konkrete Laufzeitfaehigkeiten, Escape-Sequenzen, Zeichensatzabbildung
und Plattformverhalten.

Phase-7 driver consolidation condensed the historical `.cc` landscape into
managed capability buckets. For wave 4, however, this consolidation alone is
not sufficient: `terminal`, `eterm`, `xterm`, `fonts`, and `cyrillic` validate
concrete runtime behaviour, escape-sequence handling, charset mapping, and
platform-specific behaviour.

Das Hauptproblem fuer diese Welle ist nicht mehr "existiert ein Treiber?",
sondern "welche Terminal- und Zeichensatzvertraege sind auf welchem Host
wirklich reproduzierbar und wie werden Nicht-Unterstuetzungen sichtbar?"

The main problem for this wave is no longer "does a driver exist?" but rather
"which terminal and charset contracts are actually reproducible on which host,
and how are unsupported cases made visible?"

---

## 2. Betroffene Beispiele / Affected Examples

- `cyrillic`
- `eterm`
- `fonts`
- `terminal`
- `xterm`

---

## 3. Ziele / Goals

- Terminal- und Zeichensatzverhalten vor der Beispielportierung explizit
  definieren.
- Plattformunterschiede sichtbar und testbar machen.
- Nicht-Unterstuetzungen kontrolliert statt implizit behandeln.

- Define terminal and charset behaviour explicitly before example porting.
- Make platform differences visible and testable.
- Handle unsupported capabilities in a controlled rather than implicit way.

---

## 4. Anforderungen / Requirements

### R-01: Terminal-Sitzungsmodell statt direkter Konsolenzugriffe

Wave-4-Beispiele muessen auf einem expliziten Terminal-Sitzungs- oder
Praesentationsvertrag aufsetzen. Direkte, unstrukturierte Konsolenzugriffe im
Beispielcode sind nicht zulaessig.

Wave-4 examples must build on an explicit terminal session or presentation
contract. Direct unstructured console access inside example code is not
acceptable.

### R-02: Escape- und Emulationsumfang muss bewusst zugeschnitten werden

Fuer `terminal`, `eterm` und `xterm` ist ein klarer unterstuetzter Umfang an
Escape-Sequenzen, Cursorbewegungen, Attributwechseln und Fallback-Verhalten zu
definieren. "Best effort" ohne dokumentierten Vertrag reicht nicht.

For `terminal`, `eterm`, and `xterm`, the project must define a clear supported
subset of escape sequences, cursor movement, attribute changes, and fallback
behaviour. Undocumented "best effort" behaviour is not sufficient.

### R-03: Zeichensatz- und Font-Abbildung braucht einen Framework-Vertrag

`fonts` und `cyrillic` verlangen eine explizite Zuordnung zwischen
historischen Zeichensaetzen, Unicode-Darstellung, moeglichen Ersatzzeichen und
Plattformgrenzen. Diese Zuordnung muss im Framework und in den Tests sichtbar
sein.

`fonts` and `cyrillic` require an explicit mapping between historical character
sets, Unicode rendering, possible replacement characters, and platform limits.
That mapping must be visible in the framework and in tests.

### R-04: Kompatibilitaetsnachweise muessen Host-spezifisch sein

Wave-4-Akzeptanz darf nicht nur auf einem Mac-Terminal beruhen. Fuer die
relevanten Terminalfaehigkeiten sind reviewbare Nachweise fuer Multi-Mac,
Linux und Windows/WSL notwendig.

Wave-4 acceptance must not rely on one Mac terminal only. Reviewable evidence
for Multi-Mac, Linux, and Windows/WSL is required for the relevant terminal
capabilities.

### R-05: Nicht-Unterstuetzung muss sichtbar und testbar bleiben

Wenn ein Host bestimmte Emulations- oder Zeichensatzfaehigkeiten nicht bietet,
muessen die Beispiele dies klar anzeigen und die Tests den Fallback- oder
Ablehnungspfad belegen.

If a host does not provide a required emulation or charset capability, the
examples must surface that limitation clearly and the tests must validate the
fallback or rejection path.

### R-06: Abgrenzung zu Wave 2 und 3 bleibt bestehen

Dieses Dokument darf weder fehlende Dialog-Widgets aus Wave 2 noch Editor- und
Hilfevertraege aus Wave 3 mitziehen. Der dedizierte Laufzeit-Maussupport wird
vorab in `Lastenheft_04_MouseSupportAndInteraction.md` geschnitten. Terminal
und Charset bleiben hier der eigene Vorbereitungsblock fuer Wave 4.

This document must not pull missing dialog widgets from wave 2 or editor/help
contracts from wave 3 into scope. Dedicated runtime mouse support is scoped
first in `Lastenheft_04_MouseSupportAndInteraction.md`. Terminal and charset
readiness remain the separate preparation block for wave 4 here.

---

## 5. Nicht im Scope / Out of Scope

- Allgemeine Dialog-/Widget-Nacharbeit
- Editor-, Datei-, Help- und Stream-End-to-End-Fluesse
- Dedizierter Runtime-Maussupport aus `Lastenheft_04_MouseSupportAndInteraction.md`
- Vollstaendige native Mausprotokoll-Portierung ueber den dokumentierten
  Emulationsumfang hinaus

- General dialog and widget follow-up
- Editor, file, help, and stream end-to-end flows
- Dedicated runtime mouse support from `Lastenheft_04_MouseSupportAndInteraction.md`
- Full native mouse-protocol porting beyond the documented emulation scope

---

## 6. Akzeptanzkriterien / Acceptance Criteria

- Vor dem ersten Wave-4-Beispiel existiert ein dokumentierter und getesteter
  Terminal-/Charset-Vertrag.
- `terminal`, `eterm`, `xterm`, `fonts` und `cyrillic` koennen ihre Besonderheit
  auf Basis gemeinsamer Infrastruktur zeigen statt ueber rohe Konsolenzugriffe.
- Plattformspezifische Unterschiede sind in Tests oder Review-Dokumenten
  sichtbar, nicht nur implizit im Verhalten versteckt.

- Before the first wave-4 example starts, a documented and tested terminal and
  charset contract exists.
- `terminal`, `eterm`, `xterm`, `fonts`, and `cyrillic` can demonstrate their
  specific behaviour on shared infrastructure instead of raw console access.
- Platform-specific differences are visible in tests or review documents rather
  than hidden implicitly in runtime behaviour.

---

## 7. Spec-Kit-Readiness / Spec-Kit Readiness

Dieses Lastenheft ist als direkte Eingabedatei fuer `/speckit-specify`
verwendbar. Der spaetere Spec-Kit-Lauf muss die Anforderungen Deutsch zuerst
und Englisch danach uebernehmen, auf CEFR-B2-Niveau formulieren und
text-first A11Y-Anforderungen fuer Terminal-, Charset-, Fallback- und
Host-Grenzen sichtbar halten.

This requirements document can be used directly as input for
`/speckit-specify`. The later Spec-Kit run must carry the requirements in
German first and English second, use CEFR-B2 language, and keep text-first
accessibility requirements visible for terminal, charset, fallback, and host
limits.

---

## 8. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_05_TerminalCharsetAndEmulation.md als verbindliche Eingabe. Erstelle die Feature-Spezifikation fuer einen Terminal-, Charset- und Emulations-Hardening-Lauf als Voraussetzung fuer Wave-4-Beispiele.

Ziel: Terminal-Sitzungsmodell, Escape-Umfang, Charset-/Font-Mapping und Host-Fallbacks muessen definiert und getestet werden, bevor terminal, eterm, xterm, fonts und cyrillic als sichtbare Wave-4-Beispiele portiert werden.

Pflicht:
- Anforderungen Deutsch zuerst und Englisch danach, CEFR-B2 und text-first A11Y formulieren.
- Strukturierte Terminal-/Presentation-Vertraege statt direkter unstrukturierter Konsolenzugriffe verlangen.
- Escape-Sequenzen, Cursorbewegungen, Attribute, Charset-Mapping, Ersatzzeichen und Plattformgrenzen bewusst zuschneiden.
- Multi-Mac, Linux und Windows/WSL als Review-Kontext beruecksichtigen.
- Keine Editor-/Help-Flows, keine Dialog-/Widget-Nacharbeit und keinen dedizierten Runtime-Maussupport in diesen Lauf ziehen.
```
