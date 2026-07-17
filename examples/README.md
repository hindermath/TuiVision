# Examples — Pflichtbeispiele / Mandatory Examples

Diese Struktur enthält portierte TuiVision-Beispielprogramme aus der ursprünglichen
Turbo-Vision-2.0.3-Quelltextsammlung unter `tv203s/contrib/tvision/examples/`.

This structure contains ported TuiVision example programs from the original
Turbo Vision 2.0.3 source collection under `tv203s/contrib/tvision/examples/`.

---

## Wave-1-Beispiele / Wave 1 Examples

Die folgenden vier Beispiele gehören zur **ersten Pflicht-Welle** (`007-port-wave1-examples`).
Sie entstammen direkt dem Originalordner `tv203s/contrib/tvision/examples/` und sind
keine Bestandteile von `TVDEMOS/` oder `TVFM/`.

The following four examples belong to the **first mandatory wave** (`007-port-wave1-examples`).
They originate directly from the original folder `tv203s/contrib/tvision/examples/` and are
not part of `TVDEMOS/` or `TVFM/`.

`014-wave1-functional-hardening` ist der funktionale Nachweisnachlauf für diese
vier Beispiele. Die primäre Beweismatrix liegt in
`specs/014-wave1-functional-hardening/pr-evidence.md`. Sie dokumentiert die
historischen Quellen, die aktuellen C#-Pfadentscheidungen, die Smoke-Nachweise,
Helper-Klassifikationen und die Grenzen zu Wave-1-Visual-Remediation, Wave 3 und
Wave 4.

`014-wave1-functional-hardening` is the functional proof follow-up for these
four examples. The primary proof matrix is
`specs/014-wave1-functional-hardening/pr-evidence.md`. It documents the
historical sources, current C# path decisions, smoke proof, helper
classifications, and the boundary to Wave-1 visual remediation, Wave 3, and
Wave 4.

`017-wave1-visual-component-remediation` schließt die sichtbare zweite Stufe ab.
Jedes Wave-1-Beispiel besitzt nun eine reale Hauptkomponente, eine echte
`TStatusLine` und den tastaturerreichbaren Pfad `Help -> Description`. Primäre
Smokes führen `app.Run()` aus und verbinden konkreten Zustand, View-Typ und
gerenderte Terminalzellen. Die 20-Zeilen-Matrix aus vier App- und 16
Tutorial-Token-Zeilen liegt in
`tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`; die
Review-Evidence liegt in
`specs/017-wave1-visual-component-remediation/pr-evidence.md`.

`017-wave1-visual-component-remediation` completes the visible second stage.
Every Wave 1 example now has a real main component, a real `TStatusLine`, and the
keyboard-reachable `Help -> Description` path. Primary smokes run `app.Run()` and
combine concrete state, view type, and rendered terminal cells. The 20-row
matrix of four app and 16 Tutorial token rows is in
`tests/TuiVision.Examples.SmokeTests/Wave1VisualSmokeMatrixTests.cs`; review
evidence is in `specs/017-wave1-visual-component-remediation/pr-evidence.md`.

| Verwalteter Name / Managed Name | Originalordner / Original Folder | Startbefehl / Launch Command | Pflichtunterstützung / Required Support Assets |
|---|---|---|---|
| `Desklogo` | `tv203s/contrib/tvision/examples/desklogo/` | `dotnet run --project examples/Desklogo` | Eingebettetes Logo, kontrolliertes Clipping, Status und Beschreibung. `set-logo.cc` und `tv_logo.cc` bleiben read-only Generator-Kontext. |
| `MsgCls` | `tv203s/contrib/tvision/examples/msgcls/` | `dotnet run --project examples/MsgCls` | Sichtbarer Command-/Broadcast-Pfad mit wiederholbaren Nachrichten, Status und Beschreibung; `testdyn.cpp`, `tlnmsg.cpp` und `tlnmsg.h` bleiben Absichtsreferenz. |
| `Tutorial` | `tv203s/contrib/tvision/examples/tutorial/` | `dotnet run --project examples/Tutorial -- tvguid01` | 16 eindeutige repräsentative Komponenten/Zustände mit Aktion, Status und Beschreibung; `tvguid01.cc` bis `tvguid16.cc` bleiben read-only Referenz. |
| `Videomode` | `tv203s/contrib/tvision/examples/videomode/` | `dotnet run --project examples/Videomode` | Sichtbare Probe/Wiederholung mit `supported`, `fallback`, `rejected` oder `unchanged`, Status und ehrlicher Plattformbeschreibung. |

---

## Wave-2-Beispiele / Wave 2 Examples

Die folgenden elf Beispiele gehören zur **zweiten Pflicht-Welle**
(`011-port-wave2-examples`) und sind durch `012-interactive-wave2-demos`
interaktiv nachpoliert. `013-wave2-visual-component-remediation` ergänzt den
strengeren sichtbaren Nachweis: Jedes Beispiel besitzt eine echte sichtbare
Hauptkomponente, eine echte `TStatusLine`-Rueckmeldung und den einheitlichen
Pfad `Help -> Description`. Die primären Smokes prüfen `app.Run()`, konkrete
Zustaende, View-Baum-Typen und gerenderte Buffer-Regionen.

The following eleven examples belong to the **second mandatory wave**
(`011-port-wave2-examples`) and were polished interactively by
`012-interactive-wave2-demos`. `013-wave2-visual-component-remediation` adds the
stricter visible proof: each example has a real visible main component, real
`TStatusLine` feedback, and the shared `Help -> Description` path. Primary
smokes verify `app.Run()`, concrete states, view-tree types, and rendered
buffer regions.

| Verwalteter Name / Managed Name | Originalordner / Original Folder | Startbefehl / Launch Command | Pflichtunterstuetzung / Required Support Assets |
|---|---|---|---|
| `Clipboard` | `tv203s/contrib/tvision/examples/clipboard/` | `dotnet run --project examples/Clipboard` | Sichtbare `TInputLine`, Statuszeile und `Help -> Description` für Copy, Cut, Paste und Unavailable. |
| `Demo` | `tv203s/contrib/tvision/examples/demo/` | `dotnet run --project examples/Demo` | Sichtbare `TDialog`/`TWindow`-Familien für Controls/Dialog/Gadget, Datei-/Pfadmetadaten und Farb-/Displayauswahl; Welle 3/4 bleibt außer Scope. |
| `DlgDsn` | `tv203s/contrib/tvision/examples/dlgdsn/` | `dotnet run --project examples/DlgDsn` | Sichtbare Runtime-Dialoge und Rejection-Dialoge; Fixtures bleiben unter `examples/DlgDsn/Fixtures/`. |
| `DynTxt` | `tv203s/contrib/tvision/examples/dyntxt/` | `dotnet run --project examples/DynTxt` | Sichtbare `TStaticText`-Hauptkomponente für Short, Long und Constrained. |
| `InpLis` | `tv203s/contrib/tvision/examples/inplis/` | `dotnet run --project examples/InpLis` | Sichtbarer Dialog mit Liste, Eingabe, session-only History, Grenzen und leeren Listen. |
| `ListVi` | `tv203s/contrib/tvision/examples/listvi/` | `dotnet run --project examples/ListVi` | Sichtbarer Listen-Dialog mit Auswahlbewegung, erster/letzter Grenze und leerer Liste. |
| `ProgBa` | `tv203s/contrib/tvision/examples/progba/` | `dotnet run --project examples/ProgBa` | Sichtbarer `TProgressBar` bis Completed. |
| `Sdlg` | `tv203s/contrib/tvision/examples/sdlg/` | `dotnet run --project examples/Sdlg` | Sichtbare vertikale `TScrollGroup` mit Scroll, Focus und Boundary. |
| `Sdlg2` | `tv203s/contrib/tvision/examples/sdlg2/` | `dotnet run --project examples/Sdlg2` | Sichtbare zweiachsige `TScrollGroup` mit Scroll both, Focus far und Boundary. |
| `TCombo` | `tv203s/contrib/tvision/examples/tcombo/` | `dotnet run --project examples/TCombo` | Sichtbarer Combo-Dialog mit Auswahl, Eingabewert, Boundary und Empty. |
| `TProgB` | `tv203s/contrib/tvision/examples/tprogb/` | `dotnet run --project examples/TProgB` | Sichtbares Progress-Fenster mit Partial, Abort und Cancelled. |

---

## Wave-3-Beispiele / Wave 3 Examples

Feature `019-wave3-visual-component-porting` macht die fünf Editor-, Hilfe-,
Ressourcen- und Compilerbeispiele sichtbar. Jedes Beispiel besitzt eine reale
Hauptkomponente, eine echte `TStatusLine`, `Help -> Description` und einen
primären App-Loop-Smoke mit Zustands-, View- und Buffer-Proof.

Feature `019-wave3-visual-component-porting` makes the five editor, help,
resource, and compiler examples visible. Every example has a real main
component, a real `TStatusLine`, `Help -> Description`, and a primary app-loop
smoke with state, view, and buffer proof.

| Verwalteter Name / Managed Name | Startbefehl / Launch Command | Sichtbarer Pfad und Grenze / Visible Path and Boundary |
|---|---|---|
| `TvEdit` | `dotnet run --project examples/TvEdit` | Echter `TFileEditor`, Modified-/Safe-Close-Status; Datei-Proof nur im Test-Temp-Ordner. |
| `BHelp` | `dotnet run --project examples/BHelp` | `THelpWindow`, Navigation und Fallback; proprietärer `.tch`-Decoder bewusst ausgelassen. |
| `HelpDemo` | `dotnet run --project examples/HelpDemo` | Fokus, Kontext, Hinweis und Help-Fallback; vollständiger Tastaturpfad, Maus folgt in Feature 020. |
| `I18n` | `dotnet run --project examples/I18n` | Explizite Sprache, Schlüssel- und Sprachfallback; unabhängig von Host-Locale und `gettext`. |
| `TvHc` | `dotnet run --project examples/TvHc` | Kontrollierte `.topic`-Kompilierung und Diagnose; Ausgabe-Proof nur im Test-Temp-Ordner. |

---

## Wave-4-Beispiele / Wave 4 Examples

Feature `022-wave4-visual-component-porting` macht Terminal-, Charset-, Font-
und Resource-Zustände sichtbar. Die Beispiele verwenden die kontrollierten
Verträge aus Feature 021 und verändern keine Host-Terminal-, Font-, Codepage-
oder Keyboard-Einstellung.

Feature `022-wave4-visual-component-porting` makes terminal, charset, font, and
resource states visible. The examples use the controlled Feature-021 contracts
and do not change host terminal, font, codepage, or keyboard settings.

| Verwalteter Name / Managed Name | Startbefehl / Launch Command | Sichtbarer Pfad und Grenze / Visible Path and Boundary |
|---|---|---|
| `Terminal` | `dotnet run --project examples/Terminal` | Echte `TTerminalView`, kontrollierte Eingabe, Cursor, Ablehnung/Recovery und `Unsupported`-Fallback; kein Prozess, keine Shell und kein PTY. |
| `Cyrillic` | `dotnet run --project examples/Cyrillic` | Beschriftete KOI8-R-/Unicode-Zellen und vier Mappingzustände über feste Framework-Tabelle; keine Host-Locale- oder Codepage-Änderung. |
| `Fonts` | `dotnet run --project examples/Fonts` | Projektkontrollierte rohe 8x16-Fixture, Metadaten, Glyphenraster und sichtbare Fallbackklassen; keine Fontinstallation oder Generatorausführung. |
| `ETerm` | `dotnet run --project examples/ETerm` | Unveränderliches Manifest aus Menü-, Theme- und Präsentationswerten mit `Unsupported`-Fallback; kein Legacy-Parser, Spawn, Save oder Host-Theme. |
| `XTerm` | `dotnet run --project examples/XTerm` | Unveränderliches Ressourcen-/Sequenzmanifest mit nativer Resource-Fallbackgrenze; keine X-Datenbank, terminfo-Auswertung oder externe Kommandos. |

---

## Wave-5-Beispiele, vollständige Showcase-Stufe / Wave 5 Examples, Complete Showcase Stage

Features `032-wave5-tp7-functional-porting` und
`033-wave5-tp7-showcase-remediation` liefern gemeinsam die vollständige
Wave-5-Stufe. Die zehn modernen C#-Beispiele übernehmen die Lern- und
Nutzerabsicht aus 15 read-only Quellen unter `TVDEMOS/`, verwenden aber
vorhandene TuiVision-Verträge statt Pascal-, DOS- oder
Beispiel-Ersatzframeworks.

Features `032-wave5-tp7-functional-porting` and
`033-wave5-tp7-showcase-remediation` jointly deliver the complete Wave-5
stage. The ten modern C# examples retain the learning and user intent from 15
read-only sources under `TVDEMOS/`, while using existing TuiVision contracts
instead of Pascal, DOS, or examples-only substitute frameworks.

Jeder Pfad startet normal, besitzt einen kontrollierten `--smoke`-Pfad und
wird über `app.Run()`, konkreten Zustand, View-Identität und gerenderte Zellen
bewiesen. Zusätzlich bietet jedes Beispiel eine sichtbare Hauptkomponente,
eine echte `TStatusLine`, eine per `F1` erreichbare zweisprachige Beschreibung
und stabile Tastatur- sowie begrenzte Layoutpfade.

Each path starts normally, has a controlled `--smoke` path, and is proven
through `app.Run()`, concrete state, view identity, and rendered cells. Every
example also provides a visible main component, a real `TStatusLine`, an
`F1`-reachable bilingual description, and stable keyboard and constrained
layout paths.

| Verwalteter Name / Managed Name | Startbefehl / Launch Command | Sichtbarer Pfad und Grenze / Visible Path and Boundary |
|---|---|---|
| `Tp7Demo` | `dotnet run --project examples/Tp7Demo` | Desktop-Fenster, Menü-Commands, Status und Beschreibung; zwei begrenzte Idle-Zyklen bleiben der reproduzierbare Hintergrundpfad. |
| `Tp7Edit` | `dotnet run --project examples/Tp7Edit` | Echtes `TEditWindow`, Modified/Safe-Close, Konfliktentscheidung, Status und Beschreibung; beliebige Benutzerdaten bleiben ausgeschlossen. |
| `Tp7Help` | `dotnet run --project examples/Tp7Help` | Echtes Help-Fenster mit Querverweis, Zurück, Status, Beschreibung und atomarem Fallback ohne Teilmodell. |
| `Tp7ResourceDemo` | `dotnet run --project examples/Tp7ResourceDemo` | Rekonstruiertes Dialog-, Menü- und Statusmodell mit Auswahl, Beschreibung und atomarer Ablehnung ungültiger Ressourcen. |
| `Tp7ResourceGenerator` | `dotnet run --project examples/Tp7ResourceGenerator` | Ziel-Eingabe, Generate-Button, Fortschritt, Ergebnis und Beschreibung; absolute Pfade und Traversal werden abgelehnt. |
| `Tp7AsciiTable` | `dotnet run --project examples/Tp7AsciiTable` | Fokussierbare 16x16-Matrix für `0..255`, Tastaturnavigation, Auswahlstatus und textorientierte Beschreibung. |
| `Tp7Calculator` | `dotnet run --project examples/Tp7Calculator` | Echter Dialog mit 20 Buttons, Grundrechenarten, Status, Beschreibung und atomarer Division-durch-null-Ablehnung. |
| `Tp7Calendar` | `dotnet run --project examples/Tp7Calendar` | Fokussierbare Monatsmatrix, Tag-/Monatsnavigation, Status und feste reproduzierbare Fixture ohne Systemdatum oder Locale. |
| `Tp7Puzzle` | `dotnet run --project examples/Tp7Puzzle` | Fokussierbares 4x4-Board, Tastaturzug, Status, Beschreibung und zustandserhaltende Ablehnung. |
| `Tp7MouseDialog` | `dotnet run --project examples/Tp7MouseDialog` | Echte Controls, lokale Einstellungen, ehrliche Capability, Aktivierung und vollständiger Tastaturfallback ohne Host-Mutation. |

---

## Wave-6-Dateimanager, funktionale Stufe / Wave-6 File Manager, Functional Stage

Feature `035-wave6-tvfm-functional-porting` liefert einen kontrollierten
modernen C#-Dateimanager als erste Wave-6-Stufe. Er arbeitet ausschließlich in
einer kopierten Fixture-Wurzel und führt keine Shell oder externen Viewer aus.

Feature `035-wave6-tvfm-functional-porting` delivers a controlled modern C#
file manager as the first Wave-6 stage. It operates only inside a copied
fixture root and launches neither a shell nor an external viewer.

| Name | Startbefehl / Launch Command | Deterministischer Proof / Deterministic Proof |
|---|---|---|
| `Tp7FileManager` | `dotnet run --project examples/Tp7FileManager` | `dotnet run --project examples/Tp7FileManager -- --smoke` |

Die vollständige sichtbare Menü- und Dialogführung bleibt ein aus Feature 035
abzuleitendes Stage-2-Delta.

The complete visible menu and dialog workflow remains a Stage-2 delta derived
from Feature 035.

---

## A11Y-Referenz / Accessibility Reference

Feature `023-a11y-framework` liefert eine kleine Referenz-App für opt-in
Widget-Texte, Fokusankündigungen, strukturierte Shortcuts, vollständige
Tastatur-Proofs und explizites High Contrast.

Feature `023-a11y-framework` provides a small reference app for opt-in widget
text, focus announcements, structured shortcuts, complete keyboard proof and
explicit high contrast.

| Name | Startbefehl / Launch Command | Sichtbarer Pfad und Grenze / Visible Path and Boundary |
|---|---|---|
| `A11yFramework` | `dotnet run --project examples/A11yFramework` | Zwei fokussierbare Widgets, Menü-/Status-Shortcuts, High-Contrast-Text, `Help -> Description` und ehrlicher `native bridge unavailable`-Fallback. |

---

## Didaktische Begleitdokumentation / Didactic companion documentation

Die Anleitungen für diese Beispiele befinden sich unter `docs/guides/examples/`:

Guides for these examples are located under `docs/guides/examples/`:

- `docs/guides/examples/desklogo.md`
- `docs/guides/examples/msgcls.md`
- `docs/guides/examples/tutorial.md`
- `docs/guides/examples/videomode.md`
- `docs/guides/examples/clipboard.md`
- `docs/guides/examples/demo.md`
- `docs/guides/examples/dlgdsn.md`
- `docs/guides/examples/dyntxt.md`
- `docs/guides/examples/inplis.md`
- `docs/guides/examples/listvi.md`
- `docs/guides/examples/progba.md`
- `docs/guides/examples/sdlg.md`
- `docs/guides/examples/sdlg2.md`
- `docs/guides/examples/tcombo.md`
- `docs/guides/examples/tprogb.md`
- `docs/guides/examples/tvedit.md`
- `docs/guides/examples/bhelp.md`
- `docs/guides/examples/helpdemo.md`
- `docs/guides/examples/i18n.md`
- `docs/guides/examples/tvhc.md`
- `docs/guides/examples/terminal.md`
- `docs/guides/examples/cyrillic.md`
- `docs/guides/examples/fonts.md`
- `docs/guides/examples/eterm.md`
- `docs/guides/examples/xterm.md`
- `docs/guides/examples/tp7-demo.md`
- `docs/guides/examples/tp7-edit.md`
- `docs/guides/examples/tp7-help.md`
- `docs/guides/examples/tp7-resource-demo.md`
- `docs/guides/examples/tp7-resource-generator.md`
- `docs/guides/examples/tp7-ascii-table.md`
- `docs/guides/examples/tp7-calculator.md`
- `docs/guides/examples/tp7-calendar.md`
- `docs/guides/examples/tp7-puzzle.md`
- `docs/guides/examples/tp7-mouse-dialog.md`
- `docs/guides/examples/tp7-file-manager.md`
- `docs/guides/a11y-framework.md`

---

## Smoke-Tests / Smoke Tests

Die automatisierten Smoke-Tests befinden sich unter `tests/TuiVision.Examples.SmokeTests/`:

Automated smoke tests are located under `tests/TuiVision.Examples.SmokeTests/`:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Desklogo"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~MsgCls"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Tutorial"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Videomode"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Wave3"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Tp7"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Wave5Functional"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Wave6"
dotnet test tests/TuiVision.Examples.SmokeTests/
```
