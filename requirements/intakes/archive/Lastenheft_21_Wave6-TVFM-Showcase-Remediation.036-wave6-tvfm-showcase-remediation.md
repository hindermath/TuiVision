# Lastenheft 21: Wave-6 TVFM Showcase Remediation

**Dokumentstatus:** Verbindliche Spec-Kit-Eingabedatei für Feature 036
**Vorgesehene Feature-Nummer:** 036
**Vorgesehener Branch:** `036-wave6-tvfm-showcase-remediation`
**Liefermodus:** `MergeAndSync`
**Verbindliche Reihenfolge:** nach dem vollständig gemergten Feature 035 und
vor einem unabhängigen Wave-6-Abschluss sowie dem Post-Wave-6-Portfolio-Audit
**Ableitungsquelle:** `ShowcaseDelta` in
`specs/035-wave6-tvfm-functional-porting/pr-evidence.md` und
`specs/035-wave6-tvfm-functional-porting/delivery-closeout.md`
**Historische Quelle:** `TVFM/`, vollständig read-only

*This is the binding Spec Kit intake for Feature 036. It defines the visible
and interactive second stage of Wave 6 from the one accepted Feature-035
ShowcaseDelta. It reserves the feature but does not start a Spec Kit run by
itself.*

---

## 0. Ziel / Goal

Feature 036 bringt den in Feature 035 funktional gelieferten
`Tp7FileManager` auf den vollständigen sichtbaren, interaktiven und
didaktischen TuiVision-Showcase-Standard.

Der Lauf verwendet die bereits bewiesenen modernen C#-Verträge für
kontrollierte Navigation, Listen, Vorschau, Suche, Viewerwahl und
Dateioperationen. Er portiert diese Funktionalität nicht erneut. Er macht die
vorhandenen Pfade über sichtbare Menüs, fokussierbare Controls, begrenzte
Dialoge, StatusLine, `Help -> Description`, Tastatur und ergänzende
Mausinteraktion nachvollziehbar erreichbar.

*Feature 036 brings the functionally delivered `Tp7FileManager` to the full
visible, interactive, and didactic TuiVision showcase standard. It exposes
the proven modern C# behavior through real framework UI paths without
re-porting the file-manager domain logic.*

## 1. Verbindliche Basis / Binding Baseline

Feature 035 ist über PR #101, Feature-Head
`207e807ee8835779b9b8641f91868a6a5e80f938` und Merge
`52f77facc518e3084f897148b44ec19e62b3dde6` vollständig geliefert. Der
kausale Closeout ist über PR #102 und Merge
`b0d99052b66f3f575f8343fa291761ec3f65779d` abgeschlossen.

Die verbindliche Stage-1-Basis umfasst:

- `examples/Tp7FileManager/`;
- `examples/Shared/TuiVision.Examples.Wave6/`;
- die Wave-6-Smoke- und Sicherheitsnachweise unter
  `tests/TuiVision.Examples.SmokeTests/`;
- den Guide und die Feature-035-Evidence;
- genau 24 inventarisierte `TVFM/`-Quellen;
- genau zehn funktionale Bereiche und einen startbaren Einstiegspunkt.

Die einzige Stage-2-Disposition lautet `ShowcaseDelta`. Sie benennt:

1. vollständigen sichtbaren Menü-/Dialogzugang für alle bewiesenen Commands;
2. reichere, aber weiterhin begrenzte Drag-/Drop-Politur;
3. constrained-layout Politur;
4. einen getrennten späteren Wave-6-Abschluss.

*The accepted Stage-1 baseline is immutable unless a narrow, reproducible
showcase defect requires a bounded correction. The single accepted delta is
visible access and proof quality, not missing domain behavior.*

## 2. Verbindlicher Umfang / Binding Scope

Der Lauf umfasst genau einen Einstiegspunkt:

- `Tp7FileManager`

Für diesen Einstiegspunkt werden genau diese Showcase-Bereiche geprüft:

| ID | Bereich | Verbindliches sichtbares Ziel |
|---|---|---|
| `W6S-001` | Navigation und Liste | kontrollierte Wurzel, Verzeichniswechsel, Auswahl und Metadaten in fokussierbaren Views |
| `W6S-002` | Text- und Hexvorschau | sichtbarer Viewerwechsel, begrenzter Inhalt und textorientierte Begrenzungsanzeige |
| `W6S-003` | Filter, Sortierung und Tags | erreichbare Commands und sichtbarer aktueller Zustand |
| `W6S-004` | Suche und Abbruch | begrenzter Suchdialog, Trefferansicht, Abbruch und klare Limit-Rückmeldung |
| `W6S-005` | Assoziation und interner Viewer | geschlossene Viewerwahl ohne Prozess- oder Shellstart |
| `W6S-006` | Kopieren, Umbenennen, Löschen und Schreibschutz | Preview, Bestätigung, Abbruch, Fehler und Recovery über begrenzte Dialoge |
| `W6S-007` | Drag-/Drop-Intent | Maus bereitet ausschließlich denselben bestätigungspflichtigen Intent wie die Tastatur vor |
| `W6S-008` | Palette und Ressourcen | geschlossene sichtbare Auswahl ohne Host- oder Legacy-Ressourcenmutation |
| `W6S-009` | Help und Description | F1- und Menüzugang mit Zweck, Bedienung, Sicherheits- und Proof-Grenze |
| `W6S-010` | Status, Fokus und Layout | text-first Fokus-/Auswahlzustand, echte StatusLine und stabile normale sowie enge Ansicht |

*The feature covers one application and ten evidence areas. This exact
matrix keeps the showcase work reviewable and prevents hidden functional
expansion.*

## 3. Nicht-Ziele / Non-Goals

- Keine erneute Pascal-Portierung und keine mechanische Übersetzung.
- Keine Änderung der in Feature 035 akzeptierten Root-, Pfad-, Such-,
  Vorschau-, Viewer-, Intent- oder Mutationsverträge.
- Keine breite Framework-Revision oder zweite lokale UI-Frameworkschicht.
- Keine neue Dependency, kein neues Projekt und kein zweiter Einstiegspunkt.
- Kein Zugriff auf beliebige Benutzerdaten.
- Kein Shell-, Prozess-, PTY-, externer Viewer- oder Host-Dateimanager-Start.
- Keine Netzwerk-, Geräte-, Laufwerks- oder rekursive Massenoperation.
- Keine dauerhafte Host-, Locale-, Terminal-, Font- oder Palettenmutation.
- Keine Änderung unter `TVFM/`, `TVDEMOS/` oder `tv203s/`.
- Kein Post-Wave-6-Portfolio-Audit und kein Start von Feature 037.

*The feature does not re-port behavior, widen filesystem authority, add
dependencies, introduce another application, or start the later closure or
portfolio audit.*

## 4. Gemeinsamer Showcase-Vertrag / Shared Showcase Contract

`Tp7FileManager` erfüllt das Drei-Schichten-Modell:

1. eine reale sichtbare Hauptkomposition aus vorhandenen TuiVision-Views;
2. eine echte `TStatusLine` mit aktuellem textorientiertem Zustand;
3. einen per Tastatur erreichbaren Pfad `Help -> Description`.

Zusätzlich gelten:

- Der normale Start zeigt im ersten Frame Zweck, kontrollierte Lernwurzel,
  Dateiliste und primäre Bedienwege.
- Jeder bewiesene Feature-035-Command besitzt einen sichtbaren Menü-,
  Control-, Dialog- oder Statuszugang.
- Jeder Kernpfad ist vollständig per Tastatur erreichbar.
- Mausinteraktion ist ergänzend und nie die einzige Bedienmöglichkeit.
- Fokus, Auswahl, Bestätigung, Ablehnung, Abbruch, Fehler und Fallback sind
  als Text erkennbar.
- Direkte Helfer bleiben `SetupOnly` oder `SupplementalProof`.

*The first frame, menus, controls, dialogs, status, help, and keyboard paths
must expose the already proven behavior. Pointer interaction is always
optional.*

## 5. Menü- und Command-Vertrag / Menu and Command Contract

Die sichtbare Menüstruktur muss die vorhandenen funktionalen Bereiche
auffindbar machen, ohne historische Menüs mechanisch zu kopieren.

Mindestens erforderlich sind fachlich klar gruppierte Zugänge für:

- Navigation, Aktualisierung, Filter, Sortierung und Tags;
- Text-/Hexvorschau und interne Viewerwahl;
- Suche, Trefferfortsetzung und Abbruch;
- Kopieren, Umbenennen, Löschen und Schreibschutz;
- geschlossene Palette-/Ressourcenauswahl;
- Hilfe, Description und kontrolliertes Beenden.

Jeder Menüeintrag besitzt einen klaren Text, einen eindeutigen Command, einen
Tastaturpfad und kontextabhängige Aktivierung. Nicht verfügbare Aktionen
bleiben sichtbar erklärt oder ehrlich deaktiviert. Es gibt keine
Pointer-only- oder versteckten Testcommands als primäre Bedienoberfläche.

*Menus expose the accepted commands with clear labels, keyboard paths, and
honest availability. Test-only commands do not count as the user interface.*

## 6. Dialog- und Dateioperationsvertrag / Dialog and File Operation Contract

Mutierende Operationen verwenden die Feature-035-Einmal-Intents und bleiben
auf die kopierte Lernwurzel begrenzt. Die Showcase-Stufe ergänzt nur die
sichtbare Entscheidungskette:

1. Operation und Quelle auswählen;
2. Ziel oder neuen Namen kontrolliert eingeben;
3. normalisierte Preview mit Sicherheitsgrenze anzeigen;
4. explizit bestätigen oder abbrechen;
5. Intent unmittelbar vor Ausführung revalidieren;
6. Ergebnis, Ablehnung oder Recovery text-first anzeigen.

Dialoge müssen fokussierbare vorhandene Controls, stabile Tab-Reihenfolge,
Enter-/Escape-Verhalten, sichtbare Validierungsfehler und vollständige
Tastaturbedienung besitzen. Löschung bleibt nicht rekursiv. Copy/Rename
überschreibt nicht still. Schreibschutz ändert nur die kontrollierte Fixture.

*Dialogs present the existing one-shot intent model. They do not create new
filesystem authority and never silently overwrite, delete, or broaden the
controlled root.*

## 7. Drag-/Drop- und Mausgrenze / Drag-and-Drop and Mouse Boundary

Drag-/Drop-Politur ist nur für einen bereits ausgewählten Fixture-Eintrag
zulässig. Der Mauspfad darf:

- Auswahl und Ziel sichtbar machen;
- denselben Operation-Intent wie der Tastaturpfad vorbereiten;
- bei Release außerhalb eines gültigen Ziels abbrechen;
- bei Escape, Capability-Verlust, View-Entfernung oder Shutdown abbrechen.

Der Mauspfad darf keine Operation direkt ausführen. Bestätigung,
Revalidierung und Ergebnisanzeige bleiben identisch zum Tastaturpfad. Hover,
Wheel, Touch, Mehrfachauswahl und allgemeines Desktop-Drag-and-Drop sind
außerhalb des Umfangs.

*Mouse drag prepares the same safe intent as keyboard interaction. It never
executes a file mutation directly.*

## 8. Layout-, Fokus- und A11Y-Vertrag / Layout, Focus, and A11Y Contract

Der Showcase benötigt mindestens:

- eine stabile normale Ansicht;
- eine dokumentierte enge Ansicht, mindestens `48x16`;
- keine unlesbaren Überlagerungen oder abgeschnittenen Primärbefehle;
- deterministische Fokusreihenfolge und sichtbaren Fokuswechsel;
- textorientierte Auswahl-, Status-, Fehler- und Capability-Rückmeldung;
- High-Contrast-taugliche Zustände ohne reine Farbcodierung;
- vollständige Shortcut-Inventur;
- `F1` beziehungsweise `Help -> Description` aus jedem primären Bereich.

Wenn ein Bereich in der engen Ansicht bewusst vereinfacht wird, muss der
Nutzer weiterhin Zweck, Auswahl, verfügbaren nächsten Schritt und
Beendigungspfad erkennen können.

*Normal and constrained layouts remain keyboard-complete, text-first, and
understandable without relying on color, pointer position, or wide terminal
space.*

## 9. Framework-Usage- und Remediation-Gate

Jeder der zehn Showcase-Bereiche erhält genau eine Framework-Entscheidung:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

Gemeinsame reine Showcase-Komposition darf in
`TuiVision.Examples.Wave6` bleiben. Wiederverwendbares View-, Dialog-,
Fokus-, Command-, Maus- oder Layoutverhalten darf nicht als dauerhafte
Wave-6-Sonderlösung entstehen.

`SmallFrameworkFix` ist nur für eine kleine reproduzierbare Lücke mit
test-first Red-/Green-Proof zulässig. Breite Runtime-, API-, Architektur-,
Dateisystem- oder Sicherheitsfragen werden als `FollowUpHardening`
dokumentiert und nicht in Feature 036 behoben.

*Each showcase area records one framework decision. Reusable behavior belongs
in a bounded framework fix or a separate follow-up, not a hidden example
framework.*

## 10. Historische Ausrichtung / Historical Alignment

Die 24 in Feature 035 inventarisierten Dateien unter `TVFM/` bleiben die
read-only Absichtsreferenz. Feature 036 übernimmt sichtbare
Komponentenfamilien, Nutzerfluss, Command-Bedeutung und Lernzweck, soweit sie
zu den akzeptierten sicheren Verträgen passen.

Die C#-Umsetzung bleibt modern und idiomatisch:

- keine Kopie des Pascal-Objektmodells oder globalen Zustands;
- keine DOS-, Laufwerks- oder Ressourcenformat-Abhängigkeit;
- keine Pixel-, Quelltext- oder Speicherlayout-Parität;
- typisierte Zustände, begrenzte Services und vorhandene TuiVision-APIs;
- bewusste moderne Abweichungen bleiben im Guide und in der Evidence sichtbar.

Free Vision, Terminal.GUI v1.9.x und `magiblot/tvision` sind sekundäre,
nicht normative Meinungen. Sie werden nur bei einer neuen konkreten,
reproduzierbaren Showcase-Frage erneut konsultiert.

*Historical sources define learning intent and user flow. Accepted TuiVision
contracts and modern idiomatic C# define the implementation.*

## 11. Evidence- und Entscheidungsmodell / Evidence and Decision Model

Die Feature-Evidence enthält genau eine Einstiegspunktzeile für
`Tp7FileManager` und genau zehn Bereichszeilen `W6S-001` bis `W6S-010`.

Jede Bereichszeile dokumentiert:

- Feature-035-Funktionsproof;
- sichtbaren Zugang und primäre Aktion;
- normale und enge Layout-Evidence;
- Fokus-, StatusLine-, Description- und Tastaturnachweis;
- verwendete Framework-Komponenten;
- lokale Showcase-Komposition;
- historische Absicht und moderne Abweichung;
- Datei-, A11Y-, Plattform- und Sicherheitsgrenze;
- Framework-Entscheidung;
- Restrisiko und Wiederbewertungsauslöser.

Die Einstiegspunktzeile erhält genau eine Abschlussentscheidung:

- `ShowcaseComplete`
- `IntentionalMinimalSurface`
- `FollowUpHardening`
- `ProductDecision`

`FollowUpHardening` benennt Ursache, Owner, Evidence und klare Grenze. Ein
`ProductDecision` stoppt den autonomen Lauf. Ein offenes `ShowcaseDelta` darf
nicht als abgeschlossen bewertet werden.

*Evidence has one application row and ten area rows. The final decision must
truthfully state whether the showcase is complete, intentionally minimal, a
bounded follow-up, or blocked by a product decision.*

## 12. Proof-Vertrag / Proof Contract

Primäre Showcase-Proofs führen den realen Anwendungspfad aus:

- `app.Run()` oder gleichwertige echte Anwendungsschleife;
- Event-, Command- und Dialogdispatch;
- Fokuswechsel und View-Hierarchie;
- sichtbare Menü-, Control-, Status- und Description-Zustände;
- Buffer-/Cell-Evidence an erwarteten Positionen oder Regionen;
- normale und constrained Viewports;
- erfolgreiche, abgelehnte, abgebrochene und nicht unterstützte Pfade;
- kontrollierte Dateisystem-Fixtures.

Mindestens erforderlich sind:

1. ein vertikaler Referenz-Slice für Navigation, Auswahl, Status und
   Description;
2. sichtbare read-only Pfade für Preview, Filter/Sort/Tag, Suche und Viewer;
3. sichtbare Dialogpfade für jede mutierende Operation;
4. Maus-/Tastaturparität für den begrenzten Drag-/Drop-Intent;
5. ein vollständiger normaler Start und ein kontrollierter `--smoke`-Start;
6. ein stabiler `48x16`- oder engerer Proof.

*Primary proof drives the real UI and combines behavior, focus, views, text,
and rendered cells. Helpers alone cannot satisfy showcase acceptance.*

## 13. Dokumentation und Lernwert / Documentation and Learning Value

Der bestehende `Tp7FileManager`-Guide wird für die sichtbare Stage 2
aktualisiert. Er enthält:

- Lernziel und historischen Bezug;
- normalen und kontrollierten Start;
- vollständige Menü- und Tastaturübersicht;
- Navigation, Preview, Suche und Dateioperationsdialoge;
- Mausoptionen und Tastaturfallback;
- kontrollierte Fixture- und Sicherheitsgrenze;
- constrained-layout und Plattformgrenzen;
- bewusste moderne Abweichungen;
- Proof- und Testgrenze.

Lerntexte sind Deutsch zuerst, Englisch danach und auf CEFR-B2-Niveau.
Markdown bleibt semantisch und text-first. Neue oder geänderte nicht triviale
Logik wird auf didaktischen Inline-Kommentarwert geprüft. XML-Kommentare
bleiben die primäre API-/DocFX-Fläche.

*Learner documentation remains bilingual, semantic, text-first, and explicit
about safe boundaries, historical intent, modern deviations, and proof.*

## 14. Validierung / Validation

Erforderlich sind:

1. `specify check`, Voraussetzungen und vollständige optionale Konvergenz;
2. exakte 1/10-Evidence-Cardinality;
3. test-first Red-/Green-Proof für neue oder geänderte Showcase-Pfade;
4. gezielte Wave-6-Showcase- und Dateisystem-Sicherheitstests;
5. normaler PTY-Start mit primärer Aktion, F1 und `Ctrl+Q`;
6. kontrollierter `--smoke`-Start;
7. vollständige Release-Tests;
8. kanonisches Fünf-Assembly-Coverage-Gate;
9. `git diff --check` und `dotnet format --verify-no-changes`;
10. DocFX und Playwright/Axe;
11. UTF-8-, Text-first- und Guide-Prüfung;
12. Secret-, Supply-Chain- und Agent-Paritätsprüfungen;
13. Ubuntu-, macOS- und Windows-Gates;
14. Exact-Head-Evidence unmittelbar vor Merge.

Negative Tests verwerfen mindestens:

- fehlende oder doppelte Bereichs- oder Einstiegspunktzeilen;
- unbekannte Framework- oder Abschlussentscheidungen;
- fehlende Menü-, Dialog-, Tastatur-, Status-, Description- oder
  constrained-layout Evidence;
- direkte Mausmutation ohne Bestätigung;
- Pfad-, Root-, Symlink-, Traversal- oder Überschreibgrenzverletzung;
- akzeptierten Abschluss mit offenem Showcase-Delta;
- Finding oder Follow-up ohne Owner, Evidence oder Wiederbewertungsauslöser.

Vor jedem einzelnen `dotnet build` oder `dotnet test` wird der manuelle
Build-Zähler genau einmal erhöht.

*Validation combines targeted UI proof, controlled filesystem safety, full
repository gates, three platforms, and exact-head evidence.*

## 15. Abnahmekriterien / Acceptance Criteria

Feature 036 ist nur abgeschlossen, wenn:

1. `Tp7FileManager` das vollständige Drei-Schichten-Modell sichtbar erfüllt;
2. alle zehn Showcase-Bereiche genau eine vollständige Evidence-Zeile haben;
3. jeder Feature-035-Kerncommand sichtbar und per Tastatur erreichbar ist;
4. Dateioperationen Preview, Bestätigung, Abbruch, Revalidierung und
   Ergebnisrückmeldung zeigen;
5. Drag-/Drop nur denselben bestätigungspflichtigen Intent vorbereitet;
6. normale und constrained Ansichten App-Loop-, View-, Fokus- und
   Buffer-/Cell-Proof besitzen;
7. Guide, Shortcut-Inventar, Description und text-first A11Y vollständig sind;
8. Framework-Wiederverwendung und lokale Showcase-Komposition erklärt sind;
9. historische, Sicherheits-, Dependency- und Hostgrenzen unverändert bleiben;
10. genau eine zulässige Abschlussentscheidung vorliegt;
11. alle lokalen, Remote-, Review- und Exact-Head-Gates konvergiert sind;
12. Feature 037 und der Post-Wave-6-Audit nicht automatisch gestartet wurden.

Nach Feature 036 wird separat entschieden, welcher unabhängige
Wave-6-Abschluss nötig ist. Erst dessen vollständig gemergte Evidence darf
den Post-Wave-6-Portfolio-Audit freigeben.

*Acceptance requires visible, keyboard-complete, safely bounded showcase
proof. A later independent Wave-6 closure remains a separate decision.*

## 16. Stop-Grenzen / Stop Boundaries

Der autonome Lauf stoppt bei:

- notwendiger Aufweichung eines Feature-035-Sicherheitsvertrags;
- Zugriff auf beliebige Benutzer-, Netzwerk-, Geräte- oder Hostpfade;
- notwendiger destruktiver oder Breaking-Produktentscheidung;
- unklarer Ownership einer wiederverwendbaren Framework-Lücke;
- nicht reproduzierbarem App-Loop-, Dialog-, Fokus- oder Cell-Proof;
- unvollständiger 1/10-Evidence-Cardinality;
- Änderung historischer Quellen;
- nicht behebbarer Security-, A11Y-, Plattform- oder Exact-Head-Lücke.

## 17. Kopierbarer Specify-Prompt / Copyable Specify Prompt

```text
$speckit-specify Use
`Lastenheft_21_Wave6-TVFM-Showcase-Remediation.036-wave6-tvfm-showcase-remediation.md` as the binding intake for
Feature 036.

Create exactly `specs/036-wave6-tvfm-showcase-remediation` on branch
`036-wave6-tvfm-showcase-remediation`. Do not create Feature 037 and do not
start the independent Wave-6 closure or the post-Wave-6 portfolio audit.

Specify the visible, interactive, and didactic second stage of the existing
Tp7FileManager. Use Feature 035 PR #101, closeout PR #102, pr-evidence,
delivery-closeout, the existing Wave-6 code, tests, guide, and exactly 24
read-only TVFM sources as the binding baseline.

Do not re-port functional behavior or widen the accepted controlled-root,
path, search, preview, viewer, intent, mutation, dependency, process, shell,
host, or arbitrary-user-data boundaries.

Require exactly one entry-point evidence row and ten showcase-area rows for
navigation/list, text/hex preview, filter/sort/tag, search/cancel, internal
viewer selection, copy/rename/delete/read-only dialogs, bounded drag/drop
intent, palette/resources, help/Description, and status/focus/layout.

Require visible menus, focusable controls, bounded dialogs, a real status
line, F1 Description, complete keyboard access, optional mouse parity,
normal plus constrained layout, and real app-loop/state/view/cell proof.

Give each showcase area exactly one UseExistingFramework,
SmallFrameworkFix, IntentionalDeviation, or FollowUpHardening decision.
Give Tp7FileManager exactly one ShowcaseComplete,
IntentionalMinimalSurface, FollowUpHardening, or ProductDecision result.
Stop on ProductDecision or any unsafe filesystem boundary.

Keep TVFM, TVDEMOS, tv203s, and external comparison sources read-only.
Require bilingual CEFR-B2 guidance, full local and remote validation, reviews,
and exact-head evidence. Do not start the next feature.
```

## 18. Kopierbarer Autonomous-Prompt / Copyable Autonomous Prompt

```text
$speckit-autonomous Execute the complete autonomous Spec Kit run for Feature
036 using `Lastenheft_21_Wave6-TVFM-Showcase-Remediation.036-wave6-tvfm-showcase-remediation.md` as the binding
intake. Delivery mode: MergeAndSync.

Start from clean synchronized main after Feature 035 PR #101 and closeout PR
#102 are fully merged. Verify the Feature-035 run state is Retrospective,
Completed, 163/163, with nextExactAction N/A. Create exactly branch
`036-wave6-tvfm-showcase-remediation` and feature directory
`specs/036-wave6-tvfm-showcase-remediation`. Do not create Feature 037 and do
not start the independent Wave-6 closure or post-Wave-6 portfolio audit.

Run Specify, repeated Clarify, all useful showcase, historical, filesystem
safety, framework usage, proof, A11Y, governance, and readiness checklists,
Plan, plan-review remediation, Tasks, repeated Analyze, Implement,
validation, delivery, and retrospective to documented convergence. Create
run state, gate requirements, and pr-evidence before the first implementation
change.

Use the one accepted Feature-035 ShowcaseDelta as the complete scope. Reuse
the proven Tp7FileManager domain behavior and controlled filesystem contracts.
Do not re-port Pascal functionality, widen authority, add dependencies,
create another entry point, or access arbitrary user data. Keep TVFM,
TVDEMOS, tv203s, and external comparison sources read-only.

Deliver visible menu and control access for every proven command, bounded
focus-correct dialogs for copy, rename, delete, and read-only decisions,
text-first navigation, preview, filter, sort, tag, search, internal viewer,
palette/resource, status, and Help/Description paths. Improve drag/drop only
as an optional preparation of the same confirmation-required keyboard intent.

Maintain exactly one entry-point row and ten showcase-area rows. Give every
area exactly one UseExistingFramework, SmallFrameworkFix,
IntentionalDeviation, or FollowUpHardening decision. Give Tp7FileManager
exactly one ShowcaseComplete, IntentionalMinimalSurface,
FollowUpHardening, or ProductDecision result. Stop on ProductDecision,
unsafe filesystem authority, or a broad framework decision.

Prove new or changed showcase behavior test-first through real app-loop,
event, command, dialog, focus, view-tree, status, Description, buffer/cell,
negative, abort, recovery, constrained-layout, mouse/keyboard-parity, and
platform paths. Keep direct helpers supplemental.

Provide a normal PTY path with primary action, F1, and Ctrl+Q, plus a
controlled --smoke path. Update the bilingual CEFR-B2 guide and shortcut
inventory. Validate targeted tests, full Release, canonical coverage,
formatting, DocFX/Axe, UTF-8 text-first content, Linux/macOS/Windows, agent
parity, secrets, supply chain, reviews, and temporary exact-head evidence.
Increment the manual build counter before every individual dotnet build or
dotnet test.

Commit, push, create a non-empty feature PR, converge all mandatory checks
and actionable review threads, validate the exact reviewed head, merge under
the currently authorized narrow policy, perform a causal closeout only when
required, delete obsolete branches, return to clean synchronized main, and
record the retrospective. Promote no preset change without a reproducible
provider-neutral defect. Do not start the next feature.
```
