# Lastenheft: Terminal-, Charset- und Plattform-Hardening fuer Beispielwelle 4

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-03-29
**Nachgeschaerft:** 2026-06-04
**Betrifft:** `src/TuiVision.Drivers.Console/`,
`src/TuiVision.Compatibility/`, `src/TuiVision.Controls/`,
`tests/TuiVision.Drivers.Tests/`, `tests/TuiVision.Controls.Tests/`,
`tests/TuiVision.Examples.SmokeTests/`, `docs/architecture/`,
`docs/security/`
**Empfohlene Prioritaet:** nach Wave 3 und vor
`Lastenheft_Wave4-Visual-Component-Porting.md`
**Empfohlener Spec-Kit-Branch:** naechste freie Nummer nach Wave 3, z. B.
`017-wave4-terminal-charset-platform-hardening`
**Formaler Anker:** `Pflichtenheft.md` Abschnitt 8.3, M-10, Abschnitt 12,
`Lastenheft_04_MouseSupportAndInteraction.md`,
`Lastenheft_Wave4-Visual-Component-Porting.md`

---

## 0. Spec-Kit-Intake-Zusammenfassung / Spec-Kit Intake Summary

Diese Datei ist die vorbereitete Eingabe fuer einen Spec-Kit-Feature-Lauf.
Sie ist die eigene technische Vorhaertung fuer Welle 4. Der Lauf portiert die
Wave-4-Beispiele noch nicht als sichtbare Demos. Er schafft die pruefbaren
Terminal-, Charset-, Font-, Resource- und Plattformvertraege, auf denen
`cyrillic`, `eterm`, `fonts`, `terminal` und `xterm` spaeter sichtbar und
reviewbar aufsetzen koennen.

This file is the prepared input for a Spec-Kit feature run. It is the dedicated
technical hardening step for wave 4. The run does not yet port the wave-4
examples as visible demos. It creates the testable terminal, charset, font,
resource, and platform contracts on which `cyrillic`, `eterm`, `fonts`,
`terminal`, and `xterm` can later build visibly and reviewably.

- Feature-Ziel: Terminal-, Charset-, Font-, Resource- und Plattformverhalten
  als kontrollierte Framework-/Testvertraege festlegen.
- Voraussetzung: Welle 3 ist abgeschlossen oder der Plan nennt die offenen
  Wave-3-Abhaengigkeiten als Blocker. Relevante Maus-/Interaktionsentscheidungen
  aus `Lastenheft_04_MouseSupportAndInteraction.md` sind zu pruefen, aber nicht
  automatisch Scope.
- Nichtziel: Keine sichtbare Wave-4-Beispielportierung, keine Editor-/Help-/
  Stream-Arbeit, keine TP7-Anschlusswellen und keine native Komplett-
  Emulator-Neuschreibung.
- Abschlussgrenze: Vor `Lastenheft_Wave4-Visual-Component-Porting.md` existiert
  ein dokumentierter, getesteter und plattformbewusster Vertrag fuer Terminal-
  Session, strukturierte Buffer-/Cell-Proofs, Charset-/Font-Mapping,
  Resource-/Config-Fallbacks und sichere Host-Grenzen.

- Feature goal: define terminal, charset, font, resource, and platform
  behaviour as controlled framework and test contracts.
- Precondition: wave 3 is complete, or the plan records open wave-3
  dependencies as blockers. Relevant mouse and interaction decisions from
  `Lastenheft_04_MouseSupportAndInteraction.md` must be reviewed, but they do
  not automatically enter scope.
- Non-goal: no visible wave-4 example porting, no editor/help/stream work, no
  TP7 follow-on waves, and no full native emulator rewrite.
- Completion boundary: before `Lastenheft_Wave4-Visual-Component-Porting.md`,
  a documented, tested, and platform-aware contract exists for terminal
  session state, structured buffer/cell proof, charset/font mapping,
  resource/config fallback, and safe host limits.

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Die Phase-7-Treiberkonsolidierung hat die historische `.cc`-Landschaft auf
verwaltete Faehigkeitsbuckets verdichtet. Fuer Welle 4 reicht diese
Konsolidierung allein nicht aus. Die Beispiele `terminal`, `eterm`, `xterm`,
`fonts` und `cyrillic` pruefen konkrete Laufzeitfaehigkeiten:
Terminal-Sitzung, Escape-Sequenzen, Cursorzustand, Zeichensatzabbildung,
Fontdaten, Resource-/Config-Werte und Plattformgrenzen.

Phase 7 driver consolidation condensed the historical `.cc` landscape into
managed capability buckets. For wave 4, that consolidation alone is not
enough. The examples `terminal`, `eterm`, `xterm`, `fonts`, and `cyrillic`
validate concrete runtime capabilities: terminal session state, escape
sequences, cursor state, charset mapping, font data, resource/config values,
and platform limits.

Das Risiko besteht nicht nur in fehlender Funktion. Ein unscharfer Nachweis
waere ebenso problematisch: "Auf meinem Terminal sah es richtig aus" reicht
nicht. Wave 4 braucht strukturierte, reproduzierbare Nachweise ueber
TuiVision-/Driver-Puffer, View-Zustaende, Zellen, Cursorpositionen,
Resource-Werte und dokumentierte Fallbacks.

The risk is not only missing functionality. A weak proof would be a problem as
well: "it looked right on my terminal" is not enough. Wave 4 needs structured,
reproducible proof through TuiVision/driver buffers, view state, cells, cursor
positions, resource values, and documented fallbacks.

---

## 2. Betroffene Beispiele / Affected Examples

- `cyrillic`
- `eterm`
- `fonts`
- `terminal`
- `xterm`

Diese Beispiele werden in diesem Hardening-Lauf noch nicht als vollstaendige
sichtbare Demos portiert. Der Lauf muss aber jede spaetere Demo so weit
vorbereiten, dass ihr sichtbarer Port nicht ueber rohe Konsolenzugriffe,
Host-Screenshots oder lokale Sonderlogik beweisen muss, was eigentlich ein
Framework-Vertrag sein sollte.

These examples are not yet ported as complete visible demos in this hardening
run. However, the run must prepare each later demo well enough that the
visible port does not have to prove framework contracts through raw console
access, host screenshots, or local special logic.

---

## 3. Ziele / Goals

- Terminal- und Charset-Verhalten vor der Beispielportierung explizit
  definieren.
- Plattformunterschiede sichtbar und testbar machen.
- Nicht-Unterstuetzungen kontrolliert statt implizit behandeln.
- Primaere Nachweise ueber strukturierte Buffer-/Cell-/View-Zustaende
  ermoeglichen.
- Resource-, Config- und Font-Fallbacks deterministisch machen.

- Define terminal and charset behaviour explicitly before example porting.
- Make platform differences visible and testable.
- Handle unsupported capabilities in a controlled rather than implicit way.
- Enable primary proof through structured buffer, cell, and view states.
- Make resource, config, and font fallbacks deterministic.

---

## 4. Zu haertende Vertragsbereiche / Contract Areas to Harden

| Bereich | Spaeteres Beispiel | Mindestvertrag fuer diesen Lauf |
|---|---|---|
| Terminal-Session | `terminal` | Kontrollierte Session mit Eingabe, Ausgabe, Cursor, Status und Quit-Pfad ohne persistente Host-Aenderung |
| Escape-/Emulation-Subset | `terminal`, `eterm`, `xterm` | Bewusst begrenzte Sequenzen, Attribute, Cursoraktionen und Unsupported-Fallbacks |
| Charset-/Unicode-Mapping | `cyrillic`, `fonts` | Deterministische Abbildung, Ersatzzeichen und Mapping-Status |
| Font-/Rasterdaten | `fonts` | Source-controlled Fixtures, Generator-Grenze und lesbare Fehler |
| Resource-/Config-Laden | `eterm`, `xterm` | Geladene Werte, fehlende Keys, nicht unterstuetzte Werte und Fallbacks |
| Plattformgrenzen | alle | Multi-Mac, Linux und Windows/WSL als Review-Kontext mit dokumentierten Capabilities |

| Area | Later example | Minimum contract for this run |
|---|---|---|
| Terminal session | `terminal` | Controlled session with input, output, cursor, status, and quit path without persistent host changes |
| Escape/emulation subset | `terminal`, `eterm`, `xterm` | Consciously bounded sequences, attributes, cursor actions, and unsupported fallbacks |
| Charset/Unicode mapping | `cyrillic`, `fonts` | Deterministic mapping, replacement characters, and mapping status |
| Font/raster data | `fonts` | Source-controlled fixtures, generator boundary, and readable errors |
| Resource/config loading | `eterm`, `xterm` | Loaded values, missing keys, unsupported values, and fallbacks |
| Platform limits | all | Multi-Mac, Linux, and Windows/WSL as review context with documented capabilities |

---

## 5. Anforderungen / Requirements

### R-01: Terminal-Sitzungsmodell statt direkter Konsolenzugriffe

Wave-4-Beispiele muessen spaeter auf einem expliziten Terminal-Sitzungs- oder
Praesentationsvertrag aufsetzen. Dieser Hardening-Lauf muss die Schicht
benennen, ueber die Eingabe, Ausgabe, Cursorposition, Status und Beenden
kontrolliert beobachtbar werden. Direkte, unstrukturierte Konsolenzugriffe im
spaeteren Beispielcode sind kein zulaessiger Primaerbeweis.

Wave-4 examples must later build on an explicit terminal session or
presentation contract. This hardening run must name the layer through which
input, output, cursor position, status, and quit behaviour become controllably
observable. Direct, unstructured console access in later example code is not an
acceptable primary proof.

### R-02: Escape- und Emulationsumfang muss bewusst zugeschnitten werden

Fuer `terminal`, `eterm` und `xterm` ist ein klarer unterstuetzter Umfang an
Escape-Sequenzen, Cursorbewegungen, Attributwechseln, Resource-Werten und
Fallback-Verhalten zu definieren. Alles ausserhalb dieses Umfangs muss als
bewusst ausgelassen, Unsupported-Fallback oder Follow-up dokumentiert werden.

For `terminal`, `eterm`, and `xterm`, the project must define a clear supported
subset of escape sequences, cursor movement, attribute changes, resource
values, and fallback behaviour. Anything outside this subset must be documented
as intentionally omitted, unsupported fallback, or follow-up.

### R-03: Strukturierte Buffer-/Cell-Proofs sind der bevorzugte Nachweis

Primaere Tests duerfen sich nicht auf Host-Screenshots, zufaellige
Terminalausgaben oder menschliche Sichtpruefung stuetzen. Der Hardening-Lauf
muss stabile Assertions ueber TuiVision-/Driver-Puffer, gerenderte Zellen,
Zeichen, Attribute, Cursorpositionen, View-Zustaende oder geladene
Resource-Werte ermoeglichen.

Primary tests must not rely on host screenshots, accidental terminal output,
or human visual inspection. The hardening run must enable stable assertions on
TuiVision/driver buffers, rendered cells, characters, attributes, cursor
positions, view states, or loaded resource values.

### R-04: Zeichensatz-, Unicode- und Font-Abbildung braucht einen Framework-Vertrag

`fonts` und `cyrillic` verlangen eine explizite Zuordnung zwischen
historischen Zeichensaetzen, Unicode-Darstellung, Ersatzzeichen,
Font-/Rasterdaten und Plattformgrenzen. Diese Zuordnung muss im Framework, in
Tests und in Evidence sichtbar sein. Generatoren wie historische Raw-Font-
Werkzeuge duerfen nur kontrolliert ueber Fixtures oder als dokumentierte
Grenze einbezogen werden.

`fonts` and `cyrillic` require an explicit mapping between historical
character sets, Unicode rendering, replacement characters, font/raster data,
and platform limits. This mapping must be visible in framework code, tests,
and evidence. Generators such as historical raw-font tools may only be used
through controlled fixtures or as documented boundaries.

### R-05: Resource- und Config-Fallbacks muessen deterministisch sein

`eterm` und `xterm` duerfen spaeter nicht erst im Beispielcode erfinden, wie
Konfigurations- oder Resource-Werte geladen, abgelehnt oder ersetzt werden.
Dieser Lauf muss pruefbare Pfade fuer gueltige Werte, fehlende Keys,
ungueltige Werte, nicht unterstuetzte Capabilities und lesbare Fallback-
Meldungen festlegen.

`eterm` and `xterm` must not later invent inside example code how config or
resource values are loaded, rejected, or replaced. This run must define
testable paths for valid values, missing keys, invalid values, unsupported
capabilities, and readable fallback messages.

### R-06: Kompatibilitaetsnachweise muessen Host-spezifisch sein

Wave-4-Akzeptanz darf nicht nur auf einem Mac-Terminal beruhen. Fuer relevante
Terminal-, Charset- und Font-Faehigkeiten sind reviewbare Nachweise oder
dokumentierte Grenzen fuer Multi-Mac, Linux und Windows/WSL notwendig. Wenn
ein Host eine Faehigkeit nicht reproduzierbar bietet, muss der Fallback
sichtbar, testbar und dokumentiert bleiben.

Wave-4 acceptance must not rely on one Mac terminal only. Relevant terminal,
charset, and font capabilities need reviewable evidence or documented limits
for Multi-Mac, Linux, and Windows/WSL. If a host does not provide a capability
reproducibly, the fallback must remain visible, testable, and documented.

### R-07: Host-Terminal-Manipulationen bleiben kontrolliert

Historische Skripte oder Quellen, die Host-Terminal, Codepage, Font oder
Terminalmodus veraendern, sind read-only Intent-Quellen. Der C#-Hardening-Lauf
darf keine persistenten Terminal-, Shell-, Font-, Codepage- oder
Benutzerprofil-Aenderungen als Proof-Pfad verwenden. Tests muessen mit
kontrollierten Fixtures, Test-Temp-Verzeichnissen und stabilen In-Process-
Zustaenden arbeiten.

Historical scripts or sources that alter host terminal, codepage, font, or
terminal mode are read-only intent references. The C# hardening run must not
use persistent terminal, shell, font, codepage, or user-profile changes as a
proof path. Tests must use controlled fixtures, test temp directories, and
stable in-process states.

### R-08: Primaere Smokes muessen spaetere App-Loop-Pfade vorbereiten

Dieser Lauf muss die Testgrundlagen so schneiden, dass
`Lastenheft_Wave4-Visual-Component-Porting.md` spaeter echte App-Loop-,
Command-, Key- oder Event-Smokes fuer Terminal-/Charset-Zustaende nutzen kann.
Direkte Helper duerfen Setup oder Zusatzbeweis sein, aber nicht die einzige
Akzeptanzschicht.

This run must shape the test foundations so
`Lastenheft_Wave4-Visual-Component-Porting.md` can later use real app-loop,
command, key, or event smokes for terminal and charset states. Direct helpers
may support setup or supplemental proof, but they must not be the only
acceptance layer.

### R-09: Dokumentation und Evidence bleiben text-first

Alle learner-facing Hinweise, Guides, Evidence-Tabellen und
Fallback-Beschreibungen muessen German-first/English-second, CEFR-B2-
orientiert und text-first/A11Y-tauglich formuliert werden. Wesentliche
Bedeutung darf nicht nur ueber Farbe, Screenshot, Host-Layout oder
Zeichensatzwirkung vermittelt werden.

All learner-facing notes, guides, evidence tables, and fallback descriptions
must be German-first/English-second, CEFR-B2-oriented, and suitable for
text-first accessibility. Essential meaning must not be communicated only
through colour, screenshots, host layout, or charset appearance.

### R-10: Abgrenzung zu Wave 2, Wave 3 und TP7 bleibt bestehen

Dieses Dokument darf weder fehlende Dialog-Widgets aus Wave 2 noch Editor-,
Datei-, Help- oder Stream-Vertraege aus Wave 3 mitziehen. Der dedizierte
Runtime-Maussupport aus `Lastenheft_04_MouseSupportAndInteraction.md` ist nur
dann Scope, wenn er fuer einen eng begrenzten Terminal-Vertrag zwingend ist
und im Plan explizit begruendet wird. TP7-Anschlusswellen bleiben ausser
Scope.

This document must not pull missing dialog widgets from wave 2 or editor,
file, help, or stream contracts from wave 3 into scope. Dedicated runtime
mouse support from `Lastenheft_04_MouseSupportAndInteraction.md` is in scope
only when it is strictly required for a narrow terminal contract and explicitly
justified in the plan. TP7 follow-on waves remain out of scope.

---

## 6. Nicht im Scope / Out of Scope

- Sichtbare Portierung von `cyrillic`, `eterm`, `fonts`, `terminal` oder
  `xterm` als finale Demos.
- Allgemeine Dialog-/Widget-Nacharbeit.
- Editor-, Datei-, Help- und Stream-End-to-End-Fluesse.
- Vollstaendige native XTerm-, ANSI-, Maus- oder Font-Emulation.
- Persistente Host-Terminal-, Shell-, Font-, Codepage- oder Profil-
  Aenderungen.
- Beliebige Nutzerdaten, externe Services, Datenbanken, Netzwerk-Proof-Pfade
  oder Runtime-/Produkt-KI.

- Visible porting of `cyrillic`, `eterm`, `fonts`, `terminal`, or `xterm` as
  final demos.
- General dialog or widget follow-up.
- Editor, file, help, and stream end-to-end flows.
- Full native XTerm, ANSI, mouse, or font emulation.
- Persistent host terminal, shell, font, codepage, or profile changes.
- Arbitrary user data, external services, databases, network proof paths, or
  runtime/product AI.

---

## 7. Akzeptanzkriterien / Acceptance Criteria

- Vor dem ersten Wave-4-Visual-Port existiert ein dokumentierter und getesteter
  Terminal-/Charset-/Plattform-Vertrag.
- `terminal`, `eterm`, `xterm`, `fonts` und `cyrillic` koennen ihre
  Besonderheit spaeter auf Basis gemeinsamer Infrastruktur zeigen statt ueber
  rohe Konsolenzugriffe.
- Primaere Tests koennen strukturierte Buffer-/Cell-/View-Zustaende,
  Cursorpositionen, Mapping-Status, Resource-/Config-Werte und Fallbacks
  pruefen.
- Plattformgrenzen fuer Multi-Mac, Linux und Windows/WSL sind dokumentiert
  oder als Blocker/Evidence-Grenze benannt.
- Host-Manipulationen aus historischen Quellen bleiben read-only Intent und
  werden nicht als C#-Proof-Pfad ausgefuehrt.
- Die spaetere Wave-4-Visual-Spezifikation kann auf diese Vorhaertung
  verweisen, ohne neue Framework-Grundsatzentscheidungen zu erfinden.

- Before the first wave-4 visual port starts, a documented and tested terminal,
  charset, and platform contract exists.
- `terminal`, `eterm`, `xterm`, `fonts`, and `cyrillic` can later demonstrate
  their specific behaviour on shared infrastructure instead of raw console
  access.
- Primary tests can verify structured buffer, cell, and view state, cursor
  positions, mapping status, resource/config values, and fallbacks.
- Platform limits for Multi-Mac, Linux, and Windows/WSL are documented or
  named as blockers/evidence boundaries.
- Host manipulations from historical sources remain read-only intent and are
  not executed as C# proof paths.
- The later wave-4 visual specification can refer to this hardening step
  without inventing new framework-level decisions.

---

## 7.1 Framework-Usage- und Remediation-Gate / Framework Usage and Remediation Gate

Der spaetere Spec-Kit-Lauf muss pro Terminal-, Charset-, Font-, Resource- oder
Plattformvertrag dokumentieren, welche bestehende TuiVision-Framework-
Komponente genutzt wird. Lokale Sonderlogik in spaeteren `examples/` ist nur
als Beispiel-Komposition erlaubt. Wenn sie Terminal-, Driver-, Buffer-,
Mapping- oder Resource-Verhalten ersetzt oder in mehreren Beispielen nuetzlich
waere, muss sie als `SmallFrameworkFix` geschlossen oder als
`FollowUpHardening` dokumentiert werden.

The later Spec-Kit run must document for each terminal, charset, font,
resource, or platform contract which existing TuiVision framework component is
used. Local special logic in later `examples/` is only allowed as example
composition. If it replaces terminal, driver, buffer, mapping, or resource
behavior or would be useful for multiple examples, it must be closed as
`SmallFrameworkFix` or recorded as `FollowUpHardening`.

Zulaessige Entscheidungen / Allowed decisions:

- `UseExistingFramework`: vorhandene Framework-Komponente reicht.
- `SmallFrameworkFix`: kleine laufbezogene Framework-Korrektur mit Test.
- `IntentionalDeviation`: bewusste Abweichung mit Guide- oder Evidence-Bezug.
- `FollowUpHardening`: zu gross fuer diesen Lauf, eigenes Hardening-Follow-up.

---

## 8. Spec-Kit-Readiness / Spec-Kit Readiness

Dieses Lastenheft ist als direkte Eingabedatei fuer `/speckit-specify`
verwendbar. Der spaetere Spec-Kit-Lauf muss die Anforderungen Deutsch zuerst
und Englisch danach uebernehmen, auf CEFR-B2-Niveau formulieren und
text-first A11Y-Anforderungen fuer Terminal-, Charset-, Font-, Resource-,
Fallback- und Host-Grenzen sichtbar halten.

This requirements document can be used directly as input for
`/speckit-specify`. The later Spec-Kit run must carry the requirements in
German first and English second, use CEFR-B2 language, and keep text-first
accessibility requirements visible for terminal, charset, font, resource,
fallback, and host limits.

---

## 9. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
/speckit-specify Nutze Lastenheft_05_TerminalCharsetAndEmulation.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen Wave-4-Terminal-, Charset- und Plattform-Hardening-Lauf als Voraussetzung fuer Lastenheft_Wave4-Visual-Component-Porting.md.

Ziel: Terminal-Session-Vertrag, Escape-/Emulations-Subset, strukturierte Buffer-/Cell-Proofs, Charset-/Unicode-/Font-Mapping, Resource-/Config-Fallbacks und Host-/Plattformgrenzen muessen definiert und getestet werden, bevor cyrillic, eterm, fonts, terminal und xterm als sichtbare Wave-4-Beispiele portiert werden.

Pflicht:
- Anforderungen Deutsch zuerst und Englisch danach, CEFR-B2 und text-first A11Y formulieren.
- Historische Quellen unter tv203s/ read-only als Intent-Quelle pruefen; Host-Manipulationsskripte nur dokumentieren, nicht als Proof-Pfad ausfuehren.
- Strukturierte Terminal-/Presentation-Vertraege statt direkter unstrukturierter Konsolenzugriffe verlangen.
- Primaere Nachweise ueber TuiVision-/Driver-Puffer, gerenderte Zellen, Zeichen, Attribute, Cursorpositionen, View-Zustaende, Resource-/Config-Werte oder Mapping-Status vorbereiten.
- Escape-Sequenzen, Cursorbewegungen, Attribute, Charset-Mapping, Ersatzzeichen, Font-/Rasterdaten und Plattformgrenzen bewusst zuschneiden.
- Resource-/Config-Fallbacks fuer eterm und xterm sowie Charset-/Font-Fallbacks fuer cyrillic und fonts deterministisch machen.
- Multi-Mac, Linux und Windows/WSL als Review-Kontext beruecksichtigen; nicht reproduzierbare Host-Faehigkeiten als sichtbare Fallbacks oder Evidence-Grenzen dokumentieren.
- Framework-Usage-Gate aufnehmen: pro Vertragsbereich bestehende Framework-Komponente, lokale Sonderlogik, Remediation-Entscheidung und Evidence-Pfad dokumentieren.
- Wiederverwendbare Logik nicht dauerhaft als lokale `examples/`-Sonderloesung belassen; bei Wiederholung als Framework-Fix oder Follow-up-Hardening behandeln.
- Kontrollierte Fixtures und Test-Temp-Verzeichnisse nutzen; keine beliebigen Nutzerdaten, keine externen Proof-Pfade, keine persistenten Host-Terminal-/Shell-/Font-/Codepage-Aenderungen.
- Keine sichtbare Wave-4-Beispielportierung, keine Wave-3-Editor-/Help-/Stream-Arbeit, keine TP7-Anschlusswellen und keine breite Framework-Revision in diesen Lauf ziehen.
```
