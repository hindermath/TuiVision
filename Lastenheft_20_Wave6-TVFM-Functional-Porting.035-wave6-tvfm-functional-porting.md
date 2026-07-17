# Lastenheft 20: Wave-6 TVFM Functional Porting

**Dokumentstatus:** Verbindliche Spec-Kit-Eingabedatei für Feature 035
**Vorgesehene Feature-Nummer:** 035
**Vorgesehener Branch:** `035-wave6-tvfm-functional-porting`
**Liefermodus:** `MergeAndSync`
**Verbindliche Reihenfolge:** nach Feature 034 und vor einer erst aus dem
tatsächlichen Feature-035-Delta abgeleiteten Showcase-Remediation
**Historische Quelle:** `TVFM/`, vollständig read-only
**Wave-Status:** Wave 5 `Closed`; Wave 6 `EligibleForIntake`, aber nicht
gestartet

*This is the binding Spec Kit intake for Feature 035. It defines the
functional first stage of Wave 6 for the historical TVFM file-manager example.
It reserves the feature but does not start a Spec Kit run by itself.*

---

## 0. Ziel / Goal

Feature 035 überträgt den historischen Lern- und Demonstrationszweck des
Turbo-Pascal-Dateimanagers aus `TVFM/` in eine moderne, idiomatische
C#-Interpretation mit dem vorhandenen TuiVision-Framework.

Die erste Stufe liefert nur funktional vollständige, sicher begrenzte
Dateimanager-Verträge und reale Proof-Pfade. Sichtbare Showcase-Politur wird
nicht vorweggenommen. Eine zweite Stufe darf erst aus den tatsächlich
verbleibenden Bedien-, Layout-, Lern- und A11Y-Deltas von Feature 035
abgeleitet werden.

*Feature 035 ports the historical learning intent and functional contracts of
the TVFM file-manager example to modern idiomatic C#. It proves safe,
controlled behavior first. A later showcase stage must be derived from actual
remaining deltas rather than assumed in advance.*

## 1. Ausgangslage / Starting Point

Feature 034 hat Wave 5 mit null `CandidateFinding` und null
`ProductDecision` geschlossen. Wave 6 ist damit `EligibleForIntake`.

TuiVision besitzt bereits relevante Framework-Verträge für:

- Anwendungsschleife, Desktop, Fenster, Fokus, Commands und StatusLine;
- Dialoge, Listen, Outline-/Tree-nahe Views und Scrollbars;
- Datei- und Verzeichnisdialoge, Editor, Help und History;
- Ressourcen, Serialisierung und kontrollierte Validierung;
- Tastatur, begrenzte Mausunterstützung und text-first A11Y;
- View-, Buffer-/Cell- und Plattform-Proofs.

Feature 035 prüft diese Verträge zuerst auf Wiederverwendbarkeit. Es erzeugt
keine parallele Dateimanager-Mikroarchitektur unter `examples/`, wenn eine
kleine wiederverwendbare Framework-Lücke nachweisbar ist.

*Existing framework contracts are reused before local example logic is added.
Reusable behavior must not remain hidden as an example-only replacement for
the framework.*

## 2. Verbindliches historisches Inventar / Binding Historical Inventory

Das vollständige read-only Inventar besteht aus:

### 2.1 Pascal-Quellen

- `TVFM/TVFM.PAS`
- `TVFM/GLOBALS.PAS`
- `TVFM/DIRVIEW.PAS`
- `TVFM/TREEWIN.PAS`
- `TVFM/FILEVIEW.PAS`
- `TVFM/VIEWTEXT.PAS`
- `TVFM/VIEWHEX.PAS`
- `TVFM/FILEFIND.PAS`
- `TVFM/FILECOPY.PAS`
- `TVFM/DRAGDROP.PAS`
- `TVFM/TRASH.PAS`
- `TVFM/ASSOC.PAS`
- `TVFM/COLORS.PAS`
- `TVFM/EDITPAL.PAS`
- `TVFM/GAUGES.PAS`
- `TVFM/INFOVIEW.PAS`
- `TVFM/TOOLS.PAS`
- `TVFM/EQU.PAS`
- `TVFM/MAKERES.PAS`

### 2.2 Ressourcen und Build-Kontext

- `TVFM/TVFM.TVR`
- `TVFM/DEFAULT.PAL`
- `TVFM/CYAN.PAL`
- `TVFM/ROSE.PAL`
- `TVFM/MAKETVFM.BAT`

Jede Datei erhält genau eine Inventarrolle, auch wenn sie bewusst nicht in
Runtime-Verhalten übersetzt wird. Binäre, Paletten- und DOS-Build-Artefakte
dienen als historische Evidence und werden nicht kopiert oder ausgeführt.

*Every historical file receives exactly one inventory role. Binary resources,
palettes, and DOS build artifacts remain evidence and are not copied or
executed.*

## 3. Quellenhierarchie / Source Hierarchy

1. `TVFM/` bestimmt den historischen Zweck und die ursprünglichen
   Nutzerabläufe.
2. `tv203s/` erklärt bei Bedarf zugrunde liegende Turbo-Vision-Verträge.
3. Akzeptierte TuiVision-APIs und modernes idiomatisches C# bestimmen die
   Produktumsetzung.
4. Die gemergte Free-Vision-, Terminal.GUI- und magiblot-Evidence kann nur bei
   einer konkreten neuen Frage als sekundäre Meinung genutzt werden.

Keine Vergleichsquelle überschreibt die historische Absicht oder akzeptierte
TuiVision-Semantik. Objektmodell, Speicherlayout, DOS-API, Quelltextform und
visuelle Pixelnähe sind keine Konformitätsziele.

*Historical intent is authoritative, while accepted TuiVision contracts define
the modern implementation. Source-text and object-layout parity are not
goals.*

## 4. Funktionsbereiche / Functional Areas

Feature 035 bildet aus dem Inventar eine exakte, deduplizierte
Funktionsmatrix. Sie deckt mindestens ab:

1. Anwendung, Desktop, Menüs, Commands und kontextsensitive Statushilfe;
2. Verzeichnisbaum, Laufwerks-/Wurzelgrenzen und Verzeichnisnavigation;
3. Dateiliste, Sortierung, Filter, Markierung und Dateiinformation;
4. Text- und Hex-Anzeige mit begrenztem Scrollen;
5. Suche nach kontrolliertem Muster und kontrolliertem Startverzeichnis;
6. Kopieren, Umbenennen, Löschen und Attribute als sichere
   Operationsentscheidungen;
7. Drag-/Drop-Absicht und vollständige Tastaturfallbacks;
8. Dateiassoziationen und Viewer-Entscheidungen;
9. Fortschritt, Abbruch, Fehler- und Recovery-Rückmeldung;
10. Palette, Konfiguration und Ressourcen als moderne, begrenzte
    Persistenzentscheidungen.

Jeder Bereich erhält genau eine Entscheidung:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

*Each functional area has one framework decision and evidence. The matrix
prevents broad or implicit porting claims.*

## 5. Sichere Dateisystemgrenze / Safe Filesystem Boundary

Alle automatisierten und interaktiven Proofs arbeiten ausschließlich in
source-controlled Fixtures oder test-eigenen temporären Verzeichnissen.

Verbindlich:

- kein Lesen beliebiger Benutzerdaten;
- kein Schreiben außerhalb einer expliziten Testwurzel;
- keine Änderung des aktuellen Benutzerverzeichnisses als persistenter
  Seiteneffekt;
- keine Ausführung externer Programme, Shells oder Viewer;
- keine echten Laufwerks-, Netzwerkfreigabe- oder Geräteoperationen;
- keine stillen Überschreib-, Lösch- oder Attributänderungen;
- atomare Validierung vor jeder mutierenden Operation;
- explizite Bestätigung oder kontrollierte Testentscheidung;
- Symlink-, Traversal-, Race- und Plattformgrenzen ehrlich dokumentieren.

Destruktive historische Flows dürfen durch sichere Simulation,
Preview/Decision-Modelle oder test-eigene Kopien ersetzt werden. Diese
Abweichung muss sichtbar und begründet sein.

*All proof uses controlled roots. Historical destructive behavior may be
represented by safe simulation or explicit decision models and must never
operate on arbitrary user data.*

## 6. Funktionale Lieferform / Functional Delivery Shape

Feature 035 liefert eine startbare moderne C#-Anwendung oder eine kleine,
fachlich begründete Gruppe startbarer Anwendungen unter `examples/`.

Jeder gelieferte Einstiegspunkt benötigt:

- normalen Release-Start;
- kontrollierten `--smoke`-Start;
- realen `app.Run()`- oder gleichwertigen Application-Loop-Pfad;
- sichtbaren ersten Zustand, der den Dateimanagerzweck erkennen lässt;
- mindestens einen sicheren primären Funktionspfad;
- kontrollierte Beendigung;
- deterministische Fixtures;
- Guide und Evidence.

Die funktionale Stufe darf einfache oder begrenzte Oberflächen verwenden.
Sie behauptet noch keinen endgültigen Showcase-Abschluss. Fehlende sichtbare
Bedienqualität wird als konkrete Delta-Zeile dokumentiert.

*The functional stage must be runnable and provable, but it does not claim
final showcase quality. Remaining visible deltas are recorded explicitly.*

## 7. Framework-Usage- und Remediation-Gate

Für jeden Funktionsbereich und jeden Einstiegspunkt dokumentiert die Evidence:

- historische Verantwortlichkeit;
- verwendete TuiVision-Komponenten;
- lokale Sonderlogik;
- Wiederverwendbarkeit außerhalb Wave 6;
- Framework-Entscheidung;
- Proof;
- Restrisiko und Wiederbewertungsauslöser.

`SmallFrameworkFix` ist nur zulässig, wenn eine kleine, allgemein
wiederverwendbare Lücke mit test-first Red-/Green-Proof belegt ist.
`FollowUpHardening` hält größere Runtime-, API-, Architektur- oder
Sicherheitsfragen außerhalb Feature 035.

*A small framework fix requires a real reusable gap and red/green proof.
Larger changes remain explicit follow-up work.*

## 8. Proof-Vertrag / Proof Contract

Primäre Proofs müssen reale Produktpfade ausführen:

- Application Loop und Event-/Command-Dispatch;
- Fokus- und View-Hierarchie;
- sichtbare Status- oder Ergebnisrückmeldung;
- Buffer-/Cell-Nachweis für den relevanten ersten und resultierenden Zustand;
- kontrollierte Dateioperation oder sichere negative Entscheidung;
- Abbruch-, Fehler- und Plattformfallback, soweit anwendbar.

Direkte Helfer sind nur `SupplementalProof`, `SetupOnly` oder begründet
`LegacyOrTemporary`. Sie ersetzen keinen primären Anwendungsloop.

Negative Tests decken mindestens ungültige Pfade, Traversal, Quelle gleich
Ziel, bereits vorhandenes Ziel, fehlende Quelle, unlesbare Daten, Abbruch,
begrenzte Kapazität und nicht unterstützte Host-Fähigkeiten ab.

*Primary proof executes real application paths. Direct helpers cannot replace
event-loop, state, view, and rendered-cell evidence.*

## 9. A11Y, Dokumentation und Lernwert

Jeder Einstiegspunkt besitzt:

- vollständige Tastaturbedienung für den primären Flow;
- text-first sichtbare Fokus-, Auswahl-, Status- und Fehlerzustände;
- eine per `F1` oder `Help -> Description` erreichbare Beschreibung;
- dokumentierte Mausoptionen mit vollständigem Tastaturfallback;
- nachvollziehbare kleine-Terminal- und High-Contrast-Grenzen;
- einen zweisprachigen CEFR-B2-Guide mit Lernziel, Start, Bedienung,
  Sicherheitsgrenze, historischem Bezug, Abweichungen und Tests.

Neue oder geänderte nicht triviale Logik wird auf didaktischen
Inline-Kommentarwert geprüft. XML-Kommentare bleiben die primäre API- und
DocFX-Fläche.

*Keyboard access, text-first state, help, constrained layouts, and learner
documentation are acceptance requirements rather than optional polish.*

## 10. Harte Grenzen / Hard Boundaries

- Keine mechanische Pascal-zu-C#-Übersetzung.
- Keine Änderung unter `TVFM/`, `TVDEMOS/` oder `tv203s/`.
- Keine neue externe Dependency, Datenbank, Netzwerk- oder Cloud-Funktion.
- Kein Shell-, Prozess-, PTY- oder externer Viewer-Start.
- Keine breite Framework-Revision.
- Keine uneingeschränkte Host-Dateimanager-Anwendung.
- Keine dauerhafte lokale Framework-Duplikation unter `examples/`.
- Keine vorab erfundene Showcase-Remediation.
- Kein Start des Post-Wave-6-Portfolio-Audits.

*The feature remains a bounded educational port, not a general-purpose host
file manager or a framework rewrite.*

## 11. Evidence und Governance

Vor der ersten Implementierungsänderung entstehen:

- `specs/035-wave6-tvfm-functional-porting/pr-evidence.md`;
- `autonomous-run-state.json`;
- `autonomous-gate-requirements.json`;
- historische Source- und Funktionsmatrix;
- Framework-Usage- und Delta-Matrix.

Die Evidence dokumentiert die sieben installierten Presets, Security- und
Dateisystemgrenzen, A11Y, Agent-Parität, Plattformen und triggerbasierte
`N/A`-Entscheidungen. Remote-Rechte werden nicht aus diesem Lastenheft
abgeleitet, sondern bei Laufstart aktuell bestätigt.

*Evidence is created before implementation and records all governance,
permission, safety, and re-evaluation boundaries.*

## 12. Validierung / Validation

Erforderlich sind:

1. `specify check`, Voraussetzungen und vollständige optionale Konvergenz;
2. exakte historische Inventar- und Funktionsmatrix;
3. test-first Proof für jeden neuen oder geänderten Vertrag;
4. gezielte Feature-035- und Dateisystem-Sicherheitstests;
5. kontrollierte `--smoke`- und normale PTY-Pfade;
6. vollständige Release-Tests;
7. kanonisches Fünf-Assembly-Coverage-Gate;
8. `git diff --check` und `dotnet format --verify-no-changes`;
9. DocFX und Playwright/Axe;
10. UTF-8-, Text-first- und Guide-Prüfung;
11. Secret-, Supply-Chain- und Agent-Paritätsprüfungen;
12. Ubuntu-, macOS- und Windows-Gates;
13. Exact-Head-Evidence unmittelbar vor Merge.

Vor jedem einzelnen `dotnet build` oder `dotnet test` wird der manuelle
Build-Zähler genau einmal erhöht.

*Validation covers functional behavior, safe filesystem boundaries, full
repository quality gates, three platforms, and the exact reviewed head.*

## 13. Delta- und Folgeregel / Delta and Follow-up Rule

Jeder funktionale Einstiegspunkt erhält genau eine Stage-2-Entscheidung:

- `ShowcaseComplete`
- `ShowcaseDelta`
- `IntentionalMinimalSurface`
- `ProductDecision`

Nur konkrete `ShowcaseDelta`-Zeilen dürfen ein nicht leeres
Wave-6-Showcase-Lastenheft erzeugen. Dieses könnte Feature 036 werden, wird
aber in Feature 035 weder angelegt noch gestartet. Ein `ProductDecision`
stoppt den Lauf.

Wenn keine Showcase-Remediation nötig ist, muss trotzdem ein unabhängiger
Wave-6-Closure folgen, bevor das vorgemerkte Post-Wave-6-Portfolio-Audit
starten darf.

*A later showcase feature is derived only from actual Feature-035 deltas.
Wave 6 still requires an independent closeout before the portfolio audit.*

## 14. Akzeptanzkriterien / Acceptance Criteria

Feature 035 ist funktional abgenommen, wenn:

1. jede historische TVFM-Datei genau eine Inventarrolle besitzt;
2. jeder Funktionsbereich genau eine Framework-Entscheidung besitzt;
3. jeder gelieferte Einstiegspunkt normal und kontrolliert startbar ist;
4. jeder Primärflow über reale App-Loop-, View-, State- und Cell-Proofs läuft;
5. jede mutierende Operation auf eine kontrollierte Testwurzel begrenzt ist;
6. negative, Abbruch-, Recovery- und Plattformgrenzen belegt sind;
7. Framework-Wiederverwendung und lokale Sonderlogik vollständig erklärt sind;
8. Guides, Tastatur, Description und text-first A11Y vollständig sind;
9. Produkt-, API-, Dependency- und historische Grenzen eingehalten sind;
10. jede tatsächliche Stage-2-Lücke explizit erfasst ist;
11. alle lokalen, Remote-, Review- und Exact-Head-Gates konvergiert sind;
12. Feature 036 und der Post-Wave-6-Audit nicht automatisch gestartet wurden.

*Acceptance requires complete inventory, safe real-path behavior, framework
reuse, learner-facing proof, converged gates, and no automatic next feature.*

## 15. Stop-Grenzen / Stop Boundaries

Der autonome Lauf stoppt bei:

- unvollständigem oder widersprüchlichem historischen Inventar;
- notwendiger destruktiver oder Breaking-Produktentscheidung;
- nicht sicher begrenzbarer Dateioperation;
- unklarer Ownership einer wiederverwendbaren Framework-Lücke;
- nicht reproduzierbarem Proof;
- Versuch, beliebige Benutzerdaten zu lesen oder zu verändern;
- Versuch, historische Quellen zu ändern oder zu kopieren;
- nicht behebbarer Evidence-, Security- oder Plattformlücke.

## 16. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
$speckit-specify Use
`Lastenheft_20_Wave6-TVFM-Functional-Porting.md` as the binding intake for
Feature 035.

Create exactly `specs/035-wave6-tvfm-functional-porting` on branch
`035-wave6-tvfm-functional-porting`. Do not create Feature 036 and do not
start the post-Wave-6 portfolio audit.

Specify the functional first stage of Wave 6 as a modern idiomatic C# port of
the historical TVFM learning intent. Inventory every file under TVFM exactly
once and keep TVFM, TVDEMOS, tv203s, and external comparison sources
read-only.

Require exact functional matrices for application/desktop, directory tree,
file list, filtering/sorting/tagging, text/hex viewing, controlled search,
safe copy/rename/delete/attribute decisions, drag/drop intent with keyboard
fallback, associations, progress/error recovery, palette/configuration, and
resources.

Use existing TuiVision framework contracts first. Give every functional area
exactly one UseExistingFramework, SmallFrameworkFix, IntentionalDeviation, or
FollowUpHardening decision. Allow a SmallFrameworkFix only for a reproducible
reusable gap with test-first red/green proof.

Constrain every file operation to source-controlled fixtures or test-owned
temporary roots. Do not read or mutate arbitrary user data, launch external
programs or shells, access network drives, add dependencies, or build a
general-purpose host file manager.

Require normal and controlled entry points, real app-loop/state/view/cell
proof, keyboard access, status, F1 Description, safe negative and fallback
paths, bilingual CEFR-B2 guides, full validation, and exact-head evidence.

Record actual Stage-2 deltas but do not pre-create or start a showcase
feature. Stop on ProductDecision or an unsafe filesystem boundary.
```

## 17. Kopierbarer Autonomous-Prompt / Copyable Autonomous Prompt

```text
$speckit-autonomous Execute the complete autonomous Spec Kit run for Feature
035 using `Lastenheft_20_Wave6-TVFM-Functional-Porting.md` as the binding
intake. Delivery mode: MergeAndSync.

Start from clean synchronized main after Feature 034 and its causal closeout
are fully merged. Verify Wave 5 is Closed and Wave 6 is EligibleForIntake.
Create exactly branch `035-wave6-tvfm-functional-porting` and feature
directory `specs/035-wave6-tvfm-functional-porting`. Do not create Feature
036 or start the post-Wave-6 portfolio audit.

Run Specify, repeated Clarify, all useful historical-inventory,
filesystem-safety, framework-usage, proof, A11Y, governance, and readiness
checklists, Plan, plan-review remediation, Tasks, repeated Analyze,
Implement, validation, delivery, and retrospective to documented
convergence. Create run state, gate requirements, and pr-evidence before the
first implementation change.

Inventory every TVFM file exactly once and keep TVFM, TVDEMOS, tv203s, and
external comparison checkouts read-only. Reconstruct historical intent
without mechanical translation. Reuse accepted TuiVision contracts and
preserve modern idiomatic C#.

Deliver bounded functional paths for directory navigation, file listing,
filtering/sorting/tagging, text/hex viewing, controlled search, safe
copy/rename/delete/attribute decisions, drag/drop intent with complete
keyboard fallback, associations, progress/error recovery, configuration,
palette, and resources. Use only source-controlled fixtures or test-owned
temporary roots. Never read or mutate arbitrary user data, launch external
programs or shells, or access network devices.

Give every functional area exactly one UseExistingFramework,
SmallFrameworkFix, IntentionalDeviation, or FollowUpHardening decision.
Prove new or changed behavior test-first through real app-loop, event,
command, focus, view-tree, status, buffer/cell, negative, abort, recovery,
and platform paths. Keep direct helpers supplemental.

Provide normal and --smoke entry points, keyboard access, F1 Description,
bilingual CEFR-B2 guides, honest platform fallbacks, and complete evidence.
Validate targeted tests, controlled PTY paths, full Release, canonical
coverage, formatting, DocFX/Axe, UTF-8 text-first content,
Linux/macOS/Windows, agent parity, secrets, supply chain, reviews, and
temporary exact-head evidence. Increment the manual build counter before
every individual dotnet build or dotnet test.

Record one ShowcaseComplete, ShowcaseDelta, IntentionalMinimalSurface, or
ProductDecision disposition per delivered entry point. Derive a non-empty
showcase intake only from actual ShowcaseDelta rows, but do not create or
start Feature 036 in this run. Stop on ProductDecision or an unsafe
filesystem boundary.

Commit, push, create a non-empty feature PR, converge all mandatory checks
and actionable review threads, validate the exact reviewed head, merge under
the currently authorized narrow policy, perform a causal closeout only when
required, delete obsolete branches, return to clean synchronized main, and
record the retrospective. Promote no preset change without a reproducible
provider-neutral defect.
```
