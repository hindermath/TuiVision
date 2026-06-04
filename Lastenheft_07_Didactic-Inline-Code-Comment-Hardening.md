# Lastenheft: Didactic Inline Code Comment Hardening

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Erstellt:** 2026-06-04
**Empfohlene Prioritaet:** nach `014-wave1-functional-hardening`, vor
`Lastenheft_Wave1-Visual-Component-Remediation.md`
**Betrifft:** zentrale Framework-Flows in `src/`, relevante Smoke-Test-Helfer
in `tests/`, sowie Evidence-, Guide- und Agent-Guidance-Oberflaechen, wenn sie
durch den Lauf beruehrt werden.

---

## 1. Ziel / Goal

Deutsch:
TuiVision ist ein Lern- und Portierungsprojekt. XML-Kommentare erklaeren die
oeffentliche API fuer DocFX. Dieses Lastenheft ergaenzt gezielt die
Code-nahe Erklaerung dort, wo Auszubildende oder spaetere Maintainer sonst nur
sehen, dass ein Ablauf funktioniert, aber nicht warum er so gebaut wurde.

Der Lauf soll zentrale Framework-Flows und Smoke-Test-Helfer didaktisch
nachschaerfen. Kommentare sollen Entscheidungen, Randbedingungen, Trade-offs,
historische Abweichungen und wichtige Lernpfade erklaeren. Sie sollen nicht
jeden Ausdruck im Code nacherzaehlen.

English:
TuiVision is both a learning and porting project. XML comments explain the
public API for DocFX. This requirements document adds focused code-near
explanations where apprentices or later maintainers would otherwise see that a
flow works, but not why it is built that way.

The feature should harden central framework flows and smoke-test helpers for
learning. Comments explain decisions, constraints, trade-offs, historical
deviations, and important learning paths. They must not repeat every code
expression in prose.

---

## 2. Ausgangslage / Current State

Deutsch:
`Pflichtenheft.md` Abschnitt 10.5 fordert bereits Datei-/Modulkommentare und
erklaerende Kommentare fuer nicht-triviale Logik. Die konkrete Intensitaet ist
aber noch nicht als eigener Spec-Kit-Lauf geprueft. Dadurch besteht das Risiko,
dass zentrale Stellen entweder zu wenig erklaert bleiben oder durch zu viele
triviale Kommentare schwerer lesbar werden.

English:
`Pflichtenheft.md` section 10.5 already requires file/module comments and
explanatory comments for non-trivial logic. The exact intensity has not yet been
reviewed in a dedicated Spec-Kit run. This creates a risk that central code
paths either remain under-explained or become harder to read through too many
trivial comments.

---

## 3. Scope

In Scope:
- zentrale Framework-Flows in `src/TuiVision.Core/`,
  `src/TuiVision.Controls/`, `src/TuiVision.Drivers.Console/`,
  `src/TuiVision.Serialization/` und `src/TuiVision.Compatibility/`, soweit sie
  Lernwert oder Wartungsrisiko besitzen;
- Smoke-Test-Helfer und Proof-Pfade in `tests/`, besonders fuer App-Loop,
  Event-/Command-Dispatch, Fokus, StatusLine, Dialogzustand, Buffer-/Cell-Proof,
  Rendering und Fallbacks;
- bestehende Kommentare, die veraltet, trivial oder irrefuehrend sind, wenn sie
  im geprueften Bereich liegen;
- Evidence fuer gepruefte Dateien oder Flow-Bereiche;
- Agent-Guidance, wenn projektweite Kommentarregeln praezisiert werden.

Out of Scope:
- keine Verhaltensaenderung am Runtime-Code;
- keine breite Framework-Revision;
- keine neue Beispielportierung;
- kein globales "jede Methode kommentieren";
- keine neuen Runtime-Abhaengigkeiten;
- keine DocFX-Regeneration, solange nur `//`- oder `/* */`-Inline-Kommentare
  ohne XML-Kommentar- oder API-Aenderung betroffen sind.

---

## 4. Kommentar-Intensitaet / Comment Intensity

Deutsch:
Die Zielintensitaet ist moderat und reviewbar:

- Datei-/Modulkommentar: 1 bis 3 Zeilen fuer fachlich gepflegte Dateien, wenn
  noch kein sinnvoller Kommentar existiert.
- Block- oder Inline-Kommentar: 1 bis 3 Zeilen vor nicht-trivialer Logik.
- Mehrzeilige Kommentare nur bei komplexen Flows, historischen Abweichungen,
  Sicherheits-/A11Y-Randbedingungen oder Test-Proof-Pfaden.
- Keine Kommentare, die nur Namen, Operatoren oder offensichtliche
  Zuweisungen wiederholen.
- German-first/English-second und CEFR-B2 fuer didaktische Erklaerbloecke;
  rein technische Lizenz-, Marker- oder Generatorzeilen bleiben unveraendert.

English:
The target intensity is moderate and reviewable:

- File/module comment: 1 to 3 lines for maintained source files when no useful
  comment exists.
- Block or inline comment: 1 to 3 lines before non-trivial logic.
- Multi-line comments only for complex flows, historical deviations,
  security/A11Y constraints, or test-proof paths.
- No comments that only repeat names, operators, or obvious assignments.
- German-first/English-second and CEFR-B2 for didactic explanation blocks;
  technical license, marker, or generated-file lines remain unchanged.

---

## 5. Review-Modell / Review Model

Jeder gepruefte Flow oder jede gepruefte Datei erhaelt eine der folgenden
Entscheidungen in der Feature-Evidence:

- `CommentAdequate`: vorhandene Kommentare reichen.
- `CommentNeeded`: nicht-triviale Logik braucht kurze didaktische Erklaerung.
- `NoCommentNeeded`: Code ist selbsterklaerend; ein Kommentar waere Rauschen.
- `UpdateExistingComment`: vorhandener Kommentar ist veraltet oder zu ungenau.
- `FollowUpHardening`: beim Review wurde ein echtes Framework- oder Testproblem
  sichtbar, das nicht in diesen Kommentar-Lauf gehoert.

---

## 6. Fachliche Hotspots / Functional Hotspots

Der Spec-Kit-Lauf soll mindestens diese Bereiche pruefen:

- Event-, Command- und Dispatch-Flows;
- Fokuswechsel und View-Hierarchie;
- StatusLine- und Help-/Description-Pfade;
- Dialogzustand, Validation und Rejection;
- Buffer-/Cell-Proof, Rendering-Snapshots und Terminal-Fallbacks;
- historische Turbo-Vision-Abweichungen;
- Smoke-Test-Helfer, die fuer Auszubildende sonst wie Magie wirken.

---

## 7. User Stories

### US1: Didaktische Framework-Erklaerung

Als Auszubildender moechte ich zentrale Framework-Flows am Codepunkt kurz
erklaert bekommen, damit ich nicht nur die API sehe, sondern auch die
Entscheidung hinter Event-, Fokus-, Rendering- oder Dialogverhalten verstehe.

**Akzeptanz:** Gepruefte nicht-triviale Framework-Flows haben eine Evidence-
Entscheidung und bei Bedarf kurze didaktische Kommentare.

### US2: Verstaendliche Smoke-Test-Helfer

Als Maintainer moechte ich Smoke-Test-Helfer fuer App-Loop-, View-Tree- und
Buffer-/Cell-Proofs so erklaert sehen, dass der Nachweisweg fuer Reviews und
Ausbildung nachvollziehbar bleibt.

**Akzeptanz:** Gepruefte Test-Helfer dokumentieren, warum der Proof-Pfad stabil
ist und welche Grenze er hat.

### US3: Kommentar-Rauschen vermeiden

Als Reviewer moechte ich keine flaechenhafte Kommentierung offensichtlicher
Zeilen, damit die fachlich wichtigen Hinweise sichtbar bleiben.

**Akzeptanz:** Evidence dokumentiert auch `NoCommentNeeded`-Entscheidungen, wenn
ein Bereich bewusst unkommentiert bleibt.

---

## 8. Akzeptanzkriterien / Acceptance Criteria

- Eine Feature-Evidence-Datei dokumentiert gepruefte Dateien oder Flow-Bereiche,
  Entscheidung, Kommentarbedarf, Aenderung und Follow-up-Grenzen.
- Keine Runtime-Verhaltensaenderung entsteht allein durch diesen Lauf.
- Neue oder geaenderte Kommentare erklaeren Warum, Trade-off, Randbedingung,
  historische Abweichung oder Proof-Grenze.
- Triviale Kommentare werden nicht neu eingefuehrt.
- Veraltete Kommentare in geprueften Bereichen werden aktualisiert oder entfernt.
- Agent-Guidance haelt die Regel fuer kuenftige Dateien und Feature-Laeufe fest.
- Wenn XML-Kommentare oder API-Signaturen beruehrt werden, gilt der normale
  DocFX-/A11Y-Nachweispfad.

---

## 9. Spec-Kit-Readiness

Dieses Lastenheft ist Specify-ready. Es beschreibt Ziel, Scope, Nichtziele,
Review-Modell, Hotspots, User Stories und Akzeptanzkriterien. Der spaetere
Spec-Kit-Lauf muss daraus eine Feature-Spezifikation erzeugen, die nicht als
pauschaler Kommentar-Rollout missverstanden wird.

### Kopierbarer `/speckit-specify`-Prompt

```text
/speckit-specify Nutze Lastenheft_07_Didactic-Inline-Code-Comment-Hardening.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen didaktischen Inline-Code-Kommentar-Hardening-Lauf.

Ziel: Zentrale TuiVision-Framework-Flows und relevante Smoke-Test-Helfer muessen fuer Auszubildende und Maintainer besser nachvollziehbar werden. XML-Kommentare bleiben die primaere API-/DocFX-Erklaerung; dieser Lauf ergaenzt nur Code-nahe didaktische Kommentare bei nicht-trivialer Logik.

Wichtig:
- Reihenfolge: nach `014-wave1-functional-hardening`, vor `Lastenheft_Wave1-Visual-Component-Remediation.md`.
- Keine Runtime-Verhaltensaenderung, keine breite Framework-Revision, keine neue Beispielportierung und kein globales "jede Methode kommentieren".
- Kommentarintensitaet moderat halten: 1 bis 3 Zeilen fuer Datei-/Modulkommentare oder nicht-triviale Blocks; mehrzeilig nur bei komplexen Flows, historischen Abweichungen, Sicherheits-/A11Y-Randbedingungen oder Test-Proof-Pfaden.
- Kommentare muessen Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze erklaeren, nicht triviales Was.
- German-first/English-second und CEFR-B2 fuer didaktische Erklaerbloecke; technische Lizenz-, Marker- oder Generatorzeilen bleiben unveraendert.
- Review-Modell aufnehmen: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, `FollowUpHardening`.
- Mindestens pruefen: Event-/Command-/Dispatch-Flows, Fokuswechsel, View-Hierarchie, StatusLine, Help/Description, Dialogzustand, Validation/Rejection, Buffer-/Cell-Proof, Rendering-Snapshots, Terminal-Fallbacks, historische Turbo-Vision-Abweichungen und Smoke-Test-Helfer.
- Feature-Evidence anlegen, die gepruefte Dateien oder Flow-Bereiche, Entscheidung, Kommentarbedarf, Aenderung und Follow-up-Grenzen dokumentiert.
- Wenn XML-Kommentare oder API-Signaturen beruehrt werden, gilt der normale DocFX-/A11Y-Nachweispfad; reine `//`- oder `/* */`-Kommentarhaertung loest keinen DocFX-Zwang aus.
- Agent-Guidance fuer kuenftige neue oder geaenderte nicht-triviale Logik beruecksichtigen.
```
