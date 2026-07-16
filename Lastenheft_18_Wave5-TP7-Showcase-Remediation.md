# Lastenheft 18: Wave-5 TP7 Showcase Remediation

**Dokumentstatus:** Verbindliche Spec-Kit-Eingabedatei für das nächste
Wave-5-Feature nach dem vollständig gemergten Feature 032
**Vorgesehene Feature-Nummer:** 033
**Vorgesehener Branch:** `033-wave5-tp7-showcase-remediation`
**Liefermodus:** `MergeAndSync`
**Verbindliche Reihenfolge:** nach
`032-wave5-tp7-functional-porting`, vor Wave 6
**Ableitungsquelle:** vollständige Showcase-Delta-Matrix in
`specs/032-wave5-tp7-functional-porting/pr-evidence.md`
**Historische Quelle:** `TVDEMOS/`, read-only

*This is the binding Spec Kit intake for the Wave-5 showcase stage after
Feature 032 has been fully merged. It is derived only from the completed
Feature-032 showcase-delta matrix. Wave 6 remains blocked until this feature
and its closeout are complete.*

---

## 0. Ziel / Goal

Feature 033 bringt die zehn in Feature 032 funktional gelieferten TP7-Beispiele
auf den vollständigen sichtbaren, interaktiven und didaktischen
TuiVision-Showcase-Standard.

Jedes Beispiel behält seine bewiesene moderne C#-Fachlogik. Der Lauf portiert
keine Funktion erneut. Er ergänzt reale Controls, fokussierbare
Bedienoberflächen, konsistente Menü- und Statuspfade, `Help -> Description`,
constrained Layout-Proof und vollständige text-first A11Y-Nachweise.

*Feature 033 brings the ten functional TP7 examples delivered by Feature 032
to the complete visible, interactive, and didactic TuiVision showcase
standard. It reuses the proven modern C# domain logic and does not port the
functionality again.*

## 1. Verbindlicher Umfang / Binding Scope

Der Lauf umfasst genau diese zehn Beispiele:

1. `Tp7Demo`
2. `Tp7Edit`
3. `Tp7Help`
4. `Tp7ResourceDemo`
5. `Tp7ResourceGenerator`
6. `Tp7AsciiTable`
7. `Tp7Calculator`
8. `Tp7Calendar`
9. `Tp7Puzzle`
10. `Tp7MouseDialog`

Die bestehende Assembly `examples/Shared/TuiVision.Examples.Wave5/`, die zehn
Startprojekte, ihre Guides und die Feature-032-Smokes sind die verbindliche
funktionale Basis.

*The existing shared Wave-5 assembly, the ten launch projects, their guides,
and the Feature-032 smokes are the binding functional baseline.*

## 2. Nicht-Ziele / Non-Goals

- Keine erneute Pascal-Portierung und keine mechanische Übersetzung.
- Keine Änderung der in Feature 032 akzeptierten Fach-, Datei-, Resource-,
  Help-, Capability- oder Sicherheitsverträge.
- Keine breite Framework-Revision.
- Keine neue Dependency, kein externer Dienst, kein Prozess, keine Shell und
  kein PTY.
- Keine beliebigen Benutzerdateien oder persistente Benutzerhistorie im Proof.
- Keine Host-Maus-, Terminal-, Font-, Locale- oder Codepage-Mutation.
- Kein Start von Wave 6.
- Kein Start des Post-Wave-6-Portfolio-Audits.

*The feature does not re-port Pascal behavior, broaden framework scope, add
dependencies or services, access arbitrary user files, mutate host settings,
or start Wave 6.*

## 3. Gemeinsamer Showcase-Vertrag / Shared Showcase Contract

Jedes Beispiel muss das Drei-Schichten-Modell erfüllen:

1. eine reale sichtbare Hauptkomponente aus vorhandenen TuiVision-Controls;
2. eine echte `TStatusLine` mit aktuellem, textorientiertem Zustand;
3. einen tastaturerreichbaren Pfad `Help -> Description`.

Zusätzlich gelten:

- Der normale Start zeigt Zweck und primäre Bedienoberfläche im ersten Frame.
- Jeder Kernpfad ist vollständig per Tastatur erreichbar.
- Mauspfade sind ergänzend und dürfen nie die einzige Bedienmöglichkeit sein.
- Fokus, Auswahl, Ablehnung und Fallback sind im Text erkennbar und nicht nur
  über Farbe oder Position.
- Primäre Smokes führen `app.Run()` aus und verbinden konkreten Zustand,
  View-Baum-Identität und gerenderte Buffer-/Cell-Evidence.
- Jede Anwendung besitzt mindestens einen stabilen constrained Viewport.
- Direkte Helfer sind nur `SetupOnly` oder `SupplementalProof`.

*Every example must provide a real main component, a real status line, and a
keyboard-reachable `Help -> Description` path. Primary proof runs the real
application loop and combines state, view identity, and rendered cells.*

## 4. Konkrete Delta-Matrix / Concrete Delta Matrix

| Beispiel | Sichtbares Delta | Interaktionsdelta | Layoutdelta | A11Y-/Lerndelta | Priorität |
|---|---|---|---|---|---|
| `Tp7Demo` | Vollständige historische Demo-Fensterfamilie und Anordnung | Tile, Cascade, Next, Close und sichtbare Commands | Mehrfenster-Proof bei enger Ansicht | Fokus-, Shortcut- und Description-Text | P1 |
| `Tp7Edit` | Vollständige Editor-Chrome und kontrollierte Dateidialoge | Edit-, Search- und File-Menüpfade | Editor- und Dialog-Proof bei enger Ansicht | Fokus, Shortcut-Inventar und Description | P1 |
| `Tp7Help` | Sichtbare Compilerdiagnosen und Viewer-Komposition | Cross-Reference- und Back-Navigation | Topic-/Diagnose-Proof bei enger Ansicht | Help-Shortcuts, Fokus und Description | P1 |
| `Tp7ResourceDemo` | Sichtbar rekonstruierter Dialog, Menü und Status | Resource-Auswahl und Ablehnungsdialog | Resource-Layout bei enger Ansicht | Shortcut-/Fokustext und Description | P1 |
| `Tp7ResourceGenerator` | Generator-Controls sowie Fortschritt/Fehler | Kontrollierte Zielwahl und Generate-Command | Generator-/Dialog-Layout bei enger Ansicht | Keyboard-Labels, Fokus und Description | P1 |
| `Tp7AsciiTable` | Sichtbares 16x16-Raster mit Auswahl | Pfeile, Paging und direkte Auswahl | Tabellen-Proof bei enger Ansicht | Fokusierter Bytewert als Text und Description | P2 |
| `Tp7Calculator` | Display und sichtbares Tastenraster | Direkte Rechner-Shortcuts | Stabile `40x12`-Komposition | Widget-Texte, Fokusreihenfolge und Description | P1 |
| `Tp7Calendar` | Monats- und Tagesraster | Tag-/Monat-Tastaturnavigation | Kalender-Proof bei enger Ansicht | Ausgewähltes Datum als Text und Description | P2 |
| `Tp7Puzzle` | Auswählbares 4x4-Kachelraster | Pfeile und direkte Kachelauswahl | Stabiler 4x4-Board-Proof | Leerfeld-/Kachelfokus als Text und Description | P2 |
| `Tp7MouseDialog` | Reale Settings-Controls und Aktivierungsziel | Fokus-Controls und vollständige Shortcuts | Dialog-Proof bei enger Ansicht | Capability-/Fallback-Text, Fokus und Description | P1 |

Diese Matrix ist vollständig. Neue funktionale Wünsche dürfen nicht still in
Feature 033 aufgenommen werden. Ein entdeckter Framework-Defekt erhält
`SmallFrameworkFix` nur bei enger Ursache und eigenem Red-/Green-Proof;
größere Arbeit wird `FollowUpHardening`.

*This matrix is complete. New functional wishes are not silently added.
Bounded framework defects may receive a small test-first fix; broader work is
routed to follow-up hardening.*

## 5. Framework-Usage-Gate

Für jedes Beispiel wird genau eine Entscheidung dokumentiert:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

Wiederverwendbare UI- oder Interaktionslogik darf nicht als zweite lokale
Framework-Schicht unter `examples/` entstehen. Gemeinsame reine
Showcase-Komposition darf in der bestehenden Wave-5-Assembly bleiben.

*Reusable framework behavior must not become a second examples-only framework.
Shared pure showcase composition may remain in the existing Wave-5 assembly.*

## 6. Historische Ausrichtung / Historical Alignment

Die zugehörigen `TVDEMOS/*.PAS`-Dateien bleiben read-only
Absichtsreferenzen. Übernommen werden Lernzweck, Nutzerfluss, sichtbare
Komponentenfamilie und Command-Bedeutung.

Die C#-Umsetzung bleibt modern und idiomatisch:

- keine Pascal-Objektmodell- oder Speicherlayoutkopie;
- keine DOS- oder TP7-Runtime-Abhängigkeit;
- keine globale veränderliche Anwendungslogik;
- vorhandene TuiVision-Contracts und typisierte C#-Zustände bleiben erhalten.

Free Vision, Terminal.GUI v1.9.x und `magiblot/tvision` bleiben sekundäre,
nicht normative Meinungen aus den abgeschlossenen Audit-Features.

*Historical sources define intent, not source shape. The implementation stays
modern idiomatic C# and retains the accepted TuiVision contracts.*

## 7. Datei-, Resource- und Help-Grenzen

- Editor- und Generator-Proofs verwenden nur source-controlled Fixtures oder
  test-eigene temporäre Verzeichnisse.
- Pfad-Traversal, implizites Überschreiben und unklare Konfliktentscheidungen
  bleiben fail-closed.
- Resource-Typen bleiben allowlist-basiert und Schlüssel ordinal/exakt.
- Ungültige Resource- oder Help-Eingabe veröffentlicht kein Teilmodell.
- Der Help-Pfad beweist bekannte Kontexte, Navigation, Back und sichtbaren
  Fallback.

*File, resource, and help proof remains controlled, exact, atomic, and
fail-closed.*

## 8. Maus- und Plattformgrenze

`Tp7MouseDialog` darf nur lokalen Beispielzustand für
Doppelklickstufe und Button-Reihenfolge verändern. Capability-Zustände werden
ehrlich als `Enabled`, `Disabled` oder `Unsupported` gezeigt.

Ein Capability-Verlust beendet laufende lokale Mausinteraktion. Jeder
Mauspfad besitzt denselben vollständigen Tastaturpfad. Native Host-Mutation
bleibt verboten und `HostMutationPerformed` bleibt in allen Proofs `false`.

*Mouse settings remain local example state. Capability loss cancels local
interaction, every mouse path has complete keyboard parity, and no host state
is changed.*

## 9. A11Y- und Dokumentationsanforderungen

- Deutsche Erklärung zuerst, englische Erklärung danach, CEFR-B2.
- Semantische Markdown-Struktur und text-first Nachweise.
- Keine wesentliche Information nur über Farbe, Layout oder Pointer.
- Sichtbare Fokus- und Auswahltexte für kompakte und normale Viewports.
- Vollständige Shortcut-Inventur je Beispiel.
- `Help -> Description` erklärt Zweck, Bedienung, moderne Abweichung,
  Sicherheits-/Capability-Grenze und Proof-Grenze.
- Geänderte XML-Kommentare oder öffentliche APIs lösen DocFX plus
  Playwright/Axe aus.

*Learner-facing text remains German-first/English-second at CEFR-B2, semantic,
text-first, keyboard-complete, and suitable for assistive use.*

## 10. Test- und Evidence-Anforderungen

Die Feature-Evidence enthält genau:

- zehn Beispielzeilen;
- zehn Framework-Entscheidungen;
- zehn Main-/Status-/Description-Proofs;
- zehn normale Startpfade;
- zehn constrained Layout-Proofs;
- ein vollständiges Shortcut-Inventar;
- Maus-Capability- und Keyboard-Parity-Evidence;
- alle bewussten historischen Abweichungen;
- alle `FollowUpHardening`-Grenzen.

Negative Tests prüfen mindestens:

- fehlende oder doppelte Beispielzeile;
- unbekannte Framework-Entscheidung;
- fehlenden Main-, Status- oder Description-Proof;
- fehlenden Keyboard-Pfad;
- leere constrained-layout Evidence;
- nicht textorientierten Fallback;
- Host-Mutation;
- Pfad-/Resource-/Help-Grenzverletzung.

*Evidence has exactly one complete row per example and negative tests reject
missing, duplicate, unknown, keyboard-incomplete, layout-empty, or
boundary-violating claims.*

## 11. Validierung / Validation

Erforderlich sind:

1. `specify check` und vollständige Spec-Kit-Konvergenz;
2. `git diff --check`;
3. `dotnet format TuiVision.sln --verify-no-changes`;
4. gezielte Wave-5-Showcase-Smokes;
5. vollständige Release-Tests;
6. kanonisches Coverlet-Gate für die fünf Framework-Assemblies;
7. `docfx docfx.json`;
8. Playwright/Axe unter `tests/web-a11y`;
9. zehn normale Release-Entry-Point-Smokes;
10. Linux-, macOS- und Windows-Gates;
11. Agent-Parität, Secret- und Supply-Chain-Prüfung;
12. Exact-Head-Evidence vor Merge.

Vor jedem einzelnen `dotnet build` oder `dotnet test` wird der manuelle
Build-Zähler genau einmal erhöht.

*Validation includes targeted and full Release tests, coverage, DocFX/Axe,
all ten entry points, three platforms, parity, security, and exact-head proof.*

## 12. Abnahmekriterien / Acceptance Criteria

Feature 033 ist nur abgeschlossen, wenn:

1. alle zehn Beispiele das Drei-Schichten-Modell sichtbar erfüllen;
2. alle zehn Kernpfade vollständig per Tastatur bedienbar sind;
3. jede Anwendung einen realen App-Loop-, Zustands-, View- und Cell-Proof hat;
4. alle zehn constrained Layouts ohne Überlappung oder abgeschnittene
   Pflichttexte bestehen;
5. Resource-, Help-, Datei- und Mausgrenzen unverändert fail-closed bleiben;
6. keine Host-Mutation, neue Dependency oder lokale Framework-Kopie entsteht;
7. alle Guides und `Help -> Description` die tatsächliche Bedienung erklären;
8. alle lokalen und Remote-Gates sowie Reviews konvergiert sind;
9. Wave 5 danach vollständig abgeschlossen ist;
10. Wave 6 erst nach einer separaten Prüfung des tatsächlichen
    Feature-033-Deltas freigegeben wird.

*Completion requires all ten visible showcases, complete keyboard paths,
real-loop state/view/cell and constrained-layout proof, unchanged fail-closed
boundaries, no host mutation or new dependency, converged validation, and a
separate post-Wave-5 delta decision before Wave 6.*

## 13. Optimaler Specify-Prompt / Recommended Specify Prompt

```text
$speckit-specify Use
`Lastenheft_18_Wave5-TP7-Showcase-Remediation.md` as the binding intake for
Feature 033.

Create exactly `specs/033-wave5-tp7-showcase-remediation` on branch
`033-wave5-tp7-showcase-remediation`. Do not start Wave 6.

Preserve the complete functional behavior and security boundaries delivered
by Feature 032. Remediate only the ten evidence-derived showcase deltas:
real visible controls, the shared three-layer model, complete keyboard paths,
constrained layouts, text-first A11Y, guides, and real app-loop/state/view/cell
proof.

Use `TVDEMOS/` only as read-only historical intent. Keep modern idiomatic C#,
existing TuiVision framework contracts, controlled file/resource/help
boundaries, honest mouse capability, and zero host mutation.

Require exactly one framework decision and one complete showcase proof row per
example. Route broader runtime defects to `FollowUpHardening`. Run all required
local, remote, coverage, DocFX/Axe, platform, parity, security, review, and
exact-head gates in `MergeAndSync` mode.
```
