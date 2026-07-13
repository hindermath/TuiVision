# Lastenheft 10: Core Runtime Conformance Hardening

## 0. Dokumentstatus

**Vorgesehener Spec-Kit-Branch:** `025-core-runtime-conformance-hardening`

**Verbindliche Reihenfolge:** nach Audit-Revision 2, vor Feature 026, Feature
028, Wave 5 und Wave 6

**Nächster Intake:** Dieses Lastenheft ist der nächste auszuführende
Spec-Kit-Lauf. Das Anlegen dieses Dokuments startet den Lauf nicht.

**Lieferart:** begrenzte Runtime- und Proof-Härtung für neun akzeptierte
Core-Findings

**Kanonische Sprache:** Deutsch; englische Erklärungen folgen als zweiter Block

*This is the next Spec Kit intake. Feature 025 runs after Audit Revision 2 and
before Feature 026, Feature 028, Wave 5, and Wave 6. Creating this document does
not start the run.*

---

## 1. Ausgangslage

Die kombinierte Verbraucherprüfung in
`specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md`
hat die isolierte Null-Finding-Entscheidung des ersten Audits für die weitere
Planung superseded. Neun Findings betreffen gemeinsame Runtime-Verträge, die
sowohl `TVDEMOS/` als auch `TVFM/` benötigen:

`F001`, `F002`, `F003`, `F004`, `F005`, `F006`, `F007`, `F008`, `F009`.

Die C#-Basis wird nicht verworfen. Ziel ist eine moderne, kleine und testbare
Umsetzung der historischen Verantwortung. Turbo Vision 2.0.3 bleibt die
Primärquelle für die Absicht; Free Vision bleibt die gepinnte zweite
Implementierungsmeinung. Weder C++ noch Pascal werden mechanisch übersetzt.

*The combined consumer review supersedes the original zero-finding decision for
forward planning. Nine findings affect shared runtime contracts needed by both
consumer families. The C# foundation remains; the goal is a small, modern, and
testable implementation of the historical responsibility, not a mechanical
translation.*

## 2. Verbindliche Eingaben

1. `specs/024-tv203-freevision-conformance-audit/conformance-audit.json`,
   Revision 2
2. `specs/024-tv203-freevision-conformance-audit/findings.md`
3. `specs/024-tv203-freevision-conformance-audit/consumer-readiness-review.md`
4. `specs/024-tv203-freevision-conformance-audit/pre-wave5-gate.md`
5. relevante Borland-Dokumentation und `tv203s/`-Implementierungen einschließlich
   Headern, read-only
6. der in `freevision-source-manifest.md` gepinnte Free-Vision-Stand, extern und
   untracked
7. aktuelle TuiVision-Quellen, Tests, Constitution, Agent-Guidance und lokale
   Preset-Matrix

Die Findings werden nicht umgedeutet oder still zusammengelegt. Falls die
Implementierung zeigt, dass ein Finding bereits erfüllt ist, benötigt es
trotzdem einen neuen real-path Proof und eine explizite `AlreadySatisfied`-
Entscheidung in der Feature-Evidence.

## 3. Ziele

1. Eindeutige konkrete Eventarten am Erzeugungsrand erzwingen.
2. Fokusübergang und View-Zustandsweitergabe auf einen konsistenten,
   validierungsfähigen Vertrag bringen.
3. Einen deterministischen Idle-/Pending-Event-Lifecycle für die reale
   Anwendungsschleife bereitstellen.
4. Desktop-, Window-Stack-, Modal- und Close-Lifecycle als wiederverwendbare
   Frameworkverantwortung schließen.
5. Eine gemeinsame, kontextabhängige Command-Freigabe für View, Menü,
   StatusLine und Tastatur nachweisen.
6. Den realen Terminal-Tastatureingang durch genau eine kanonische Übersetzung
   führen.
7. Einen begrenzten generischen Drag-Vertrag mit Tastaturalternative und
   Abbruchgrenze liefern.
8. Alle Änderungen mit real-path Tests, historischem Intent, Free-Vision-
   Zweitmeinung und moderner C#-Begründung belegen.

*The feature hardens event construction, focus and state, idle lifecycle,
desktop and modal behavior, shared command state, real keyboard ingress, and a
bounded accessible drag contract. Every change requires real-path proof and an
explicit modern C# rationale.*

## 4. Scope

### 4.1 Im Scope

- `src/TuiVision.Core/`
- `src/TuiVision.Controls/`
- `src/TuiVision.Compatibility/`
- `src/TuiVision.Drivers.Console/` nur bei notwendiger Ingress-Grenze
- zugehörige Tests in den bestehenden Testprojekten
- XML-Dokumentation und Guides für neue oder geänderte öffentliche Verträge
- Audit-, Feature-, Governance- und PR-Evidence
- kleine additive öffentliche API-Erweiterungen, wenn ein Finding anders nicht
  als Frameworkvertrag lösbar ist

### 4.2 Nicht im Scope

- Wave-5- oder Wave-6-Beispiele portieren oder verändern
- `TVDEMOS/`, `TVFM/`, `tv203s/` oder externe Free-Vision-Quellen ändern
- Dialog-, InputLine-, Datei- oder Ressourcen-Findings `F010` bis `F013`
  vorwegnehmen
- breite Framework-Neuschreibung oder neue Architektur-Schicht ohne
  Finding-Bezug
- binäre Turbo-Vision-Kompatibilität, rohe Pointer, DOS-Speichermodelle oder
  historische Plattformtreiber nachbauen
- neue Runtime-Abhängigkeiten
- öffentliches Breaking Change autonom entscheiden
- pointer-only Interaktion ohne vollständige Tastaturalternative
- Wave-5-/Wave-6-Anwendungslogik wie Rechner, Dateimanager, Kopieren, Löschen
  oder Papierkorb implementieren

## 5. Finding-Anforderungen

### R-025-001: Konkrete Eventart (`F001`, `C004`)

Event-Factories müssen genau eine konkrete Eventart akzeptieren. Kategorien und
Masken dürfen nur zum Filtern verwendet werden. Composite Mouse-Masken, gemischte
Kanäle und unbekannte Werte werden vor Dispatch deterministisch abgelehnt.
Bestehende konkrete Key-, Mouse-, Command-, Broadcast- und None-Pfade bleiben
kompatibel.

### R-025-002: Validierungsfähiger Fokusübergang (`F002`, `C008`)

Ein Fokuswechsel muss einen geordneten Veto-Punkt besitzen, den Komponenten wie
ein validiertes Eingabefeld verwenden können. Bei Ablehnung bleiben aktueller
View, Fokuszustand, Eingabedaten und sichtbare Fehlerrückmeldung konsistent.
Feature 025 definiert den generischen Fokusvertrag; die konkrete
InputLine-/Validator-Integration folgt in 026.

### R-025-003: Zustandsabhängige Hierarchie (`F003`, `C009`)

`TGroup` darf `Focused` nur dem aktuellen Kind zuordnen. `Active`, `Dragging`,
`Disabled`, `Exposed` und weitere propagierte Zustände benötigen jeweils eine
begründete, historisch verglichene Regel. Tests, die die bekannte uniforme
Fehlpropagierung erwarten, werden test-first korrigiert statt als Schutz des
alten Fehlers weitergeführt.

### R-025-004: Idle- und Pending-Event-Lifecycle (`F004`, `C013`)

Die Anwendungsschleife benötigt einen deterministischen Hook für Arbeit, wenn
kein Ereignis ansteht. Er darf keinen Busy Loop erzeugen, keine echte Eingabe
verdrängen und keine unkontrollierte Nebenläufigkeit einführen. Tests müssen
Reihenfolge, Leerlauf, wartende Ereignisse, Shutdown und wiederholte Idle-
Aktualisierung beweisen.

### R-025-005: Desktop- und Window-Stack (`F005`, `C014`)

Das Framework muss eine kleine gemeinsame Grenze für Window-Insertion,
Top-/Next-Window, Tile, Cascade und sicheres Close-All besitzen. Fokus, Bounds,
Z-Order, nicht schließbare Fenster und leerer Desktop werden deterministisch
behandelt. Anwendungsabhängige Fenstertypen bleiben außerhalb des Frameworks.

### R-025-006: Modalität und Abschluss (`F006`, `C015`)

Der Standardpfad für `cmClose`, Ctrl+W und Escape muss ein Fenster sichtbar aus
seinem Owner entfernen, sofern kein Safe-Close-Veto greift. Modale Ausführung
muss Ergebnis, Event-Isolation, Verschachtelungsgrenze, Abbruch, Shutdown und
Wiederherstellung des vorherigen Fokus beweisen. Ein reines gesendetes Signal
ist kein Abschlussnachweis.

### R-025-007: Gemeinsame Command-Freigabe (`F007`, `C017`)

Command-Verfügbarkeit muss aus einer gemeinsamen, testbaren Kontextquelle
ableitbar sein. Menü, StatusLine, Tastatur und aktiver View dürfen nach Fokus-,
Auswahl- oder Window-Wechsel nicht widersprechen. Bestehende lokale `Enabled`-
Eigenschaften dürfen als Darstellung erhalten bleiben, aber keine zweite
Wahrheitsquelle bilden.

### R-025-008: Realer Tastatur-Ingress (`F008`, `C034`)

Der reale `ConsoleKeyInfo`-Pfad muss dieselbe kanonische Übersetzung und dieselben
Modifier-Bits verwenden wie Compatibility-Tests und Adapter. Zu beweisen sind
druckbare Zeichen, Navigation, Function Keys, Alt, Ctrl, Shift, Ctrl+W,
Alt-basierte Shortcuts, unbekannte Eingaben und Terminal-Fallbacks. Direkt
eingespeiste normalisierte `TEvent`-Werte sind nur ergänzender Proof.

### R-025-009: Begrenzter generischer Drag (`F009`, `C036`)

Das Framework benötigt eine allgemeine, aber kleine Drag-Session für Views:
Startschwelle, Capture, Bewegung, Grenzen, Zielprüfung, Drop-Ergebnis, Escape-
Abbruch, Owner-/Lifecycle-Verlust und Tastaturäquivalent. Titelzeilen-Drag bleibt
ein konkreter Nutzer dieses Vertrags. Ein vollständiges Desktop-Drag-and-Drop-
Protokoll ist nicht gefordert.

## 6. Moderne C#- und historische Grenze

- Historische Verantwortung wird erhalten; Klassenlayout und Implementierung
  dürfen idiomatisch modern sein.
- Managed Ownership, immutable Result-Typen, klare Nullability,
  `ArgumentException`/`InvalidOperationException` und kleine Interfaces sind
  erlaubt, wenn sie bestehende Repository-Muster respektieren.
- Keine rohe Union, keine Pointer-Casts und keine Plattformverzweigung werden
  nur aus Paritätsgründen eingeführt.
- Free Vision darf eine Interpretation stützen oder eine Alternative zeigen,
  ist aber keine normative API-Vorlage.
- Jede wesentliche Nutzer- oder API-Abweichung vom Original wird in Plan,
  Tests, Guide und PR-Evidence erklärt.

## 7. Test- und Proof-Vertrag

1. Für jedes Finding entsteht zuerst ein fehlschlagender Test oder ein
   reproduzierbarer Red-Proof gegen den realen Pfad.
2. Keyboard-Proofs beginnen bei `ConsoleKeyInfo` oder dem echten Adapterrand.
3. Event-loop-Proofs führen `Run()` oder die äquivalente reale Schleife aus.
4. Close-, Modal-, Stack- und Drag-Proofs prüfen sichtbaren Endzustand,
   View-Tree, Fokus und Buffer/Cell, nicht nur einen gesendeten Command.
5. Negative Pfade decken Ablehnung, Abbruch, Shutdown, leeren Zustand,
   Grenzwerte und wiederholte Ausführung ab.
6. Consumer-Quellen dienen nur zum Contract-Mapping; Tests portieren keine
   Wave-Anwendung.
7. Alle fünf kanonischen Assembly-Coverage-Gates bleiben bei mindestens 70 %.

## 8. A11Y, Sicherheit und Plattform

- Jeder Mouse-/Drag-Pfad besitzt eine vollständige Tastaturalternative.
- Fokusänderungen bleiben für `TFocusAnnouncement` und text-first Proofs
  nachvollziehbar.
- Keine Aktion wird ausschließlich durch Farbe, Zeigerposition oder Timing
  verständlich.
- Unbekannte Eventarten und ungültige Zustandsübergänge werden fail-closed
  abgelehnt.
- Keine neue Trust-, Cloud-, Provider-, Auth-, Supply-Chain- oder Produkt-AI-
  Grenze wird eingeführt. Governance-`N/A`-Entscheidungen benötigen weiterhin
  Trigger und Begründung.
- Linux, macOS und Windows/WSL werden für berührte Keyboard-, Terminal- und
  Modifier-Pfade berücksichtigt; nicht verfügbare Plattformproofs bleiben
  sichtbar und blockieren, wenn sie für die Änderung erforderlich sind.

## 9. Evidence-Artefakte

Feature 025 muss mindestens liefern:

- `specs/025-core-runtime-conformance-hardening/spec.md`
- vollständige Plan-, Research-, Datenmodell-, Contract-, Quickstart-,
  Checklist- und Tasks-Artefakte
- `specs/025-core-runtime-conformance-hardening/pr-evidence.md`
- eine Finding-Tabelle für `F001` bis `F009` mit Red-Proof, Änderung, Real-Path-
  Proof, historischem Intent, Free-Vision-Relation, API-/A11Y-Auswirkung und
  Restgrenze
- aktualisierte Feature-024-Entscheidungen erst nach bestandenem Proof
- DocFX-/A11Y-Evidence bei jeder XML-, API-, Guide- oder Navigationsänderung
- aktualisierte Agent-Kontexte, Pflichtenheft-Marker, Reihenfolge und
  `docs/project-statistics.md`

## 10. Akzeptanzkriterien

1. Alle neun Findings haben genau einen Abschlussstatus und konkrete Evidence.
2. Kein Finding wird nur durch Umbenennung, Kommentar oder schwächeren Test
   geschlossen.
3. Der reale Keyboard-Ingress und die Modifier-Semantik stimmen überein.
4. Fokus, Gruppenstatus, Idle, Desktop, Modalität, Close, Command-State und Drag
   bestehen positive, negative und Lifecycle-Proofs.
5. Keine Wave-Anwendung und keine historische/externe Quelle wurde verändert.
6. Keine neue Abhängigkeit und kein unentschiedenes Breaking Change ist im Diff.
7. Targeted Tests, full Release, Coverage, Format und alle ausgelösten
   DocFX-/A11Y-/Plattform-Gates bestehen.
8. Feature 026 bleibt der nächste Intake; Wave 5 und Wave 6 bleiben blockiert.

## 11. Stop-Grenzen

Der autonome Lauf stoppt bei einem notwendigen Breaking Change, einem Konflikt
mit akzeptierter öffentlicher Semantik, einer unklaren destruktiven
Produktentscheidung, einem nicht behebbaren Pflichtcheck oder einer Lösung, die
eine breite Framework-Neuschreibung erfordert. Er portiert nicht in Wave 5 oder
Wave 6 weiter.

## 12. Kopierbarer autonomer Intake-Prompt

```text
$speckit-autonomous Use `Lastenheft_10_Core-Runtime-Conformance-Hardening.md`
as the binding intake for Feature `025-core-runtime-conformance-hardening`.

Execute the complete Spec Kit lifecycle through the explicitly authorized
repository delivery mode, but preserve the exact finding scope F001-F009. Read
Feature-024 Revision-2 audit data, findings, consumer review, gate, relevant
tv203s sources, and the pinned Free Vision manifest before specification.

Do not port or modify TVDEMOS, TVFM, or historical/external sources. Do not
implement F010-F013, Wave 5, or Wave 6. Prefer small modern C# contracts over
mechanical C++ or Pascal translation. Breaking public-contract changes require
a ProductDecision and stop autonomous implementation.

Run Clarify, focused checklists, Plan, plan review, Tasks, and repeated Analyze
until no material issue remains. Implement test-first with real ConsoleKeyInfo,
real app-loop, visible close/modal/stack state, focus, view-tree, and buffer/cell
proof as applicable. Maintain finding evidence, version/build-counter rules,
coverage, conditional DocFX/A11Y, governance, agent parity, review convergence,
merge, synchronized main, and retrospective. Finish with Feature 026 as the
next intake and keep both example waves blocked.
```
