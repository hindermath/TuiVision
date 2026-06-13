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

`014-wave1-functional-hardening` ist der funktionale Nachweisnachlauf fuer diese
vier Beispiele. Die primaere Beweismatrix liegt in
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

| Verwalteter Name / Managed Name | Originalordner / Original Folder | Startbefehl / Launch Command | Pflichtunterstützung / Required Support Assets |
|---|---|---|---|
| `Desklogo` | `tv203s/contrib/tvision/examples/desklogo/` | `dotnet run --project examples/Desklogo` | `desklogo.cc` (Hauptprogramm / main program) **erforderlich**. `set-logo.cc` und `tv_logo.cc` sind Hilfswerkzeuge für Logo-Generierung — **nicht erforderlich** für die verwaltete Portierung, da das Logo als Zeichenkettenkonstante eingebettet ist. |
| `MsgCls` | `tv203s/contrib/tvision/examples/msgcls/` | `dotnet run --project examples/MsgCls` | `testdyn.cpp` (Hauptprogramm / main program) mit `tlnmsg.cpp`/`tlnmsg.h` (Nachrichtenfenster-Klassen) **erforderlich** für vollständiges Broadcast-Routing. `readme.txt` — Dokumentation, **nicht erforderlich** als Laufzeitdatei. |
| `Tutorial` | `tv203s/contrib/tvision/examples/tutorial/` | `dotnet run --project examples/Tutorial -- tvguid01` | `tvguid01.cc` bis `tvguid16.cc` (16 Lernschritte / 16 tutorial steps) **alle erforderlich** — jeder Schritt demonstriert ein eigenständiges Lernziel. Nur `.cc`-Dateien sind Scope; Build-Hilfsdateien (`.bmk`, `.mkf`, `.imk`, `.umk`, `.gpr`) **nicht erforderlich**. |
| `Videomode` | `tv203s/contrib/tvision/examples/videomode/` | `dotnet run --project examples/Videomode` | `test.cc` (Hauptprogramm / main program) **erforderlich**. Keine weiteren Hilfswerkzeuge — **kein** externer Hilfscode erforderlich. |

---

## Wave-2-Beispiele / Wave 2 Examples

Die folgenden elf Beispiele gehoeren zur **zweiten Pflicht-Welle**
(`011-port-wave2-examples`) und sind durch `012-interactive-wave2-demos`
interaktiv nachpoliert. `013-wave2-visual-component-remediation` ergaenzt den
strengeren sichtbaren Nachweis: Jedes Beispiel besitzt eine echte sichtbare
Hauptkomponente, eine echte `TStatusLine`-Rueckmeldung und den einheitlichen
Pfad `Help -> Description`. Die primaeren Smokes pruefen `app.Run()`, konkrete
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
| `Clipboard` | `tv203s/contrib/tvision/examples/clipboard/` | `dotnet run --project examples/Clipboard` | Sichtbare `TInputLine`, Statuszeile und `Help -> Description` fuer Copy, Cut, Paste und Unavailable. |
| `Demo` | `tv203s/contrib/tvision/examples/demo/` | `dotnet run --project examples/Demo` | Sichtbare `TDialog`/`TWindow`-Familien fuer Controls/Dialog/Gadget, Datei-/Pfadmetadaten und Farb-/Displayauswahl; Welle-3/4 bleibt ausser Scope. |
| `DlgDsn` | `tv203s/contrib/tvision/examples/dlgdsn/` | `dotnet run --project examples/DlgDsn` | Sichtbare Runtime-Dialoge und Rejection-Dialoge; Fixtures bleiben unter `examples/DlgDsn/Fixtures/`. |
| `DynTxt` | `tv203s/contrib/tvision/examples/dyntxt/` | `dotnet run --project examples/DynTxt` | Sichtbare `TStaticText`-Hauptkomponente fuer Short, Long und Constrained. |
| `InpLis` | `tv203s/contrib/tvision/examples/inplis/` | `dotnet run --project examples/InpLis` | Sichtbarer Dialog mit Liste, Eingabe, session-only History, Grenzen und leeren Listen. |
| `ListVi` | `tv203s/contrib/tvision/examples/listvi/` | `dotnet run --project examples/ListVi` | Sichtbarer Listen-Dialog mit Auswahlbewegung, erster/letzter Grenze und leerer Liste. |
| `ProgBa` | `tv203s/contrib/tvision/examples/progba/` | `dotnet run --project examples/ProgBa` | Sichtbarer `TProgressBar` bis Completed. |
| `Sdlg` | `tv203s/contrib/tvision/examples/sdlg/` | `dotnet run --project examples/Sdlg` | Sichtbare vertikale `TScrollGroup` mit Scroll, Focus und Boundary. |
| `Sdlg2` | `tv203s/contrib/tvision/examples/sdlg2/` | `dotnet run --project examples/Sdlg2` | Sichtbare zweiachsige `TScrollGroup` mit Scroll both, Focus far und Boundary. |
| `TCombo` | `tv203s/contrib/tvision/examples/tcombo/` | `dotnet run --project examples/TCombo` | Sichtbarer Combo-Dialog mit Auswahl, Eingabewert, Boundary und Empty. |
| `TProgB` | `tv203s/contrib/tvision/examples/tprogb/` | `dotnet run --project examples/TProgB` | Sichtbares Progress-Fenster mit Partial, Abort und Cancelled. |

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

---

## Smoke-Tests / Smoke Tests

Die automatisierten Smoke-Tests befinden sich unter `tests/TuiVision.Examples.SmokeTests/`:

Automated smoke tests are located under `tests/TuiVision.Examples.SmokeTests/`:

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Desklogo"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~MsgCls"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Tutorial"
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~Videomode"
dotnet test tests/TuiVision.Examples.SmokeTests/
```
