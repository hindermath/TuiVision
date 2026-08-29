# Beispiel-Lernpfade / Example Learning Paths

## Zweck / Purpose

Diese Matrix ergänzt die vorhandenen Detail-Guides um einen einheitlichen
Lernvertrag. Jedes der 38 ausführbaren Beispielprojekte kommt genau einmal vor
und erhält Lernziel, Voraussetzung, Start, Bedienung, Architekturhinweis und
Übung. Die verlinkten Guides bleiben die fachlichen Detailquellen.

*This matrix adds one consistent learning contract to the existing detailed
guides. Each of the 38 executable example projects occurs exactly once with a
learning goal, prerequisite, launch command, operation, architecture note, and
exercise. The linked guides remain the detailed sources.*

## Gemeinsame Voraussetzungen / Shared Prerequisites

- **P1**: Repository-Wurzel, .NET SDK 10 und ein Terminal; zuerst
  [Getting Started](getting-started.md) lesen. / Repository root, .NET SDK 10,
  and a terminal; read Getting Started first.
- **P2**: P1 plus ein Terminal, dessen Capability/Fallback du bewusst prüfen
  kannst. / P1 plus a terminal whose capability or fallback you can review.
- **P3**: P1 plus Verständnis der
  [Serialisierungsgrenze](concepts/serialization.md); nur
  source-controlled oder test-eigene Daten verwenden. / P1 plus an
  understanding of the serialization boundary; use source-controlled or
  test-owned data only.

## Wave 1 und Tutorial / Wave 1 and Tutorial

| Projekt und Guide | Lernziel / Goal | Voraussetzung | Start | Bedienung / Operation | Architektur / Architecture | Übung / Exercise | Entscheidung |
|---|---|---|---|---|---|---|---|
| `Desklogo` [Guide](examples/desklogo.md) | DE: erste Shell und sichtbares Logo; EN: first shell and visible logo | P1 | `dotnet run --project examples/Desklogo` | DE: F1, Status, `Ctrl+Q`; EN: F1, status, quit | DE: `TApplication`, Desktop, Cells; EN: application, desktop, cells | DE: enge Ansicht prüfen; EN: inspect a narrow view | `GuideAdequate` |
| `MsgCls` [Guide](examples/msgcls.md) | DE: Command und Nachricht; EN: command and message | P1 | `dotnet run --project examples/MsgCls` | DE: Menübefehl, Status, F1; EN: menu command, status, F1 | DE: Command-Routing durch echte Views; EN: command routing through real views | DE: Command im Smoke verfolgen; EN: trace the command in its smoke | `GuideAdequate` |
| `Tutorial` [Guide](examples/tutorial.md) | DE: Framework schrittweise aufbauen; EN: build the framework model step by step | P1 | `dotnet run --project examples/Tutorial -- tvguid01` | DE: Tokens `tvguid01` bis `tvguid16`; EN: tokens `tvguid01` through `tvguid16` | DE: Shell, Views, Dialoge und Transfer wachsen kontrolliert; EN: shell, views, dialogs, and transfer grow deliberately | DE: `tvguid11` und `tvguid12` vergleichen; EN: compare `tvguid11` and `tvguid12` | `GuideAdequate` |
| `Videomode` [Guide](examples/videomode.md) | DE: Capability und ehrlicher Fallback; EN: capability and truthful fallback | P2 | `dotnet run --project examples/Videomode` | DE: Moduswechsel, Status, F1; EN: mode transition, status, F1 | DE: Driver-Capability statt Hardwarebehauptung; EN: driver capability instead of hardware claim | DE: Supported und Unsupported erklären; EN: explain supported and unsupported outcomes | `GuideAdequate` |

## Wave 2 / Wave 2

| Projekt und Guide | Lernziel / Goal | Voraussetzung | Start | Bedienung / Operation | Architektur / Architecture | Übung / Exercise | Entscheidung |
|---|---|---|---|---|---|---|---|
| `Clipboard` [Guide](examples/clipboard.md) | DE: kontrollierten Clipboard-Zustand sehen; EN: inspect controlled clipboard state | P1 | `dotnet run --project examples/Clipboard` | DE: Copy/Paste-Commands, Status, F1; EN: copy/paste commands, status, F1 | DE: lokaler Lernzustand statt Host-Clipboard; EN: local learning state instead of host clipboard | DE: leeren Paste-Pfad prüfen; EN: inspect empty paste | `MatrixCompletesContract` |
| `Demo` [Guide](examples/demo.md) | DE: mehrere Control- und Dialogfamilien; EN: several control and dialog families | P1 | `dotnet run --project examples/Demo` | DE: Demo-Menü, Dialoge, Status; EN: demo menu, dialogs, status | DE: Framework-Komposition mit begrenzten Omissionen; EN: framework composition with bounded omissions | DE: drei sichtbare Familien benennen; EN: name three visible families | `MatrixCompletesContract` |
| `DlgDsn` [Guide](examples/dlgdsn.md) | DE: Dialogbeschreibung und Preview; EN: dialog description and preview | P1 | `dotnet run --project examples/DlgDsn` | DE: Controls wählen, Preview öffnen, F1; EN: select controls, open preview, F1 | DE: deklarative Beschreibung wird zu echten Views; EN: declarative description becomes real views | DE: ein Feld ergänzen und View-Typ prüfen; EN: add one field and inspect its view type | `MatrixCompletesContract` |
| `DynTxt` [Guide](examples/dyntxt.md) | DE: dynamischen Textzustand; EN: dynamic text state | P1 | `dotnet run --project examples/DynTxt` | DE: Text-Command, Status, F1; EN: text command, status, F1 | DE: Zustand rendert über `TStaticText`; EN: state renders through `TStaticText` | DE: zwei Zustände im Buffer vergleichen; EN: compare two states in the buffer | `MatrixCompletesContract` |
| `InpLis` [Guide](examples/inplis.md) | DE: Eingabe und Liste koppeln; EN: combine input and list | P1 | `dotnet run --project examples/InpLis` | DE: eingeben, auswählen, bestätigen; EN: enter, select, confirm | DE: Fokus und Transfer zwischen Controls; EN: focus and transfer between controls | DE: ungültige Auswahl ablehnen; EN: reject an invalid selection | `MatrixCompletesContract` |
| `ListVi` [Guide](examples/listvi.md) | DE: Listenansicht und Navigation; EN: list view and navigation | P1 | `dotnet run --project examples/ListVi` | DE: Pfeile, Auswahl, Status; EN: arrows, selection, status | DE: `TListViewer` trennt Daten und sichtbare Zeilen; EN: `TListViewer` separates data and visible rows | DE: Anfang und Ende prüfen; EN: inspect first and last item | `MatrixCompletesContract` |
| `ProgBa` [Guide](examples/progba.md) | DE: Fortschritt als Zustand; EN: progress as state | P1 | `dotnet run --project examples/ProgBa` | DE: Fortschritt starten/ändern; EN: start or change progress | DE: `TProgressBar` rendert deterministische Werte; EN: `TProgressBar` renders deterministic values | DE: 0, Mitte und 100 vergleichen; EN: compare 0, midpoint, and 100 | `MatrixCompletesContract` |
| `Sdlg` [Guide](examples/sdlg.md) | DE: einfachen Dialog bedienen; EN: operate a simple dialog | P1 | `dotnet run --project examples/Sdlg` | DE: Tab, Enter, Esc, F1; EN: Tab, Enter, Esc, F1 | DE: modaler `TDialog` und Ergebniscode; EN: modal `TDialog` and result code | DE: Accept und Cancel vergleichen; EN: compare accept and cancel | `MatrixCompletesContract` |
| `Sdlg2` [Guide](examples/sdlg2.md) | DE: erweiterten Dialogzustand; EN: extended dialog state | P1 | `dotnet run --project examples/Sdlg2` | DE: mehrere Controls und Validation; EN: multiple controls and validation | DE: Transfer bleibt atomar; EN: transfer remains atomic | DE: Validation-Ablehnung auslösen; EN: trigger validation rejection | `MatrixCompletesContract` |
| `TCombo` [Guide](examples/tcombo.md) | DE: Combo-Eingabe und Auswahl; EN: combo input and selection | P1 | `dotnet run --project examples/TCombo` | DE: Text, Liste, Tastaturnavigation; EN: text, list, keyboard navigation | DE: Komposition vorhandener Input-/List-Controls; EN: composition of existing input/list controls | DE: freien und gelisteten Wert vergleichen; EN: compare free and listed values | `MatrixCompletesContract` |
| `TProgB` [Guide](examples/tprogb.md) | DE: historische Progress-Variante; EN: historical progress variant | P1 | `dotnet run --project examples/TProgB` | DE: Progress-Command, Status, F1; EN: progress command, status, F1 | DE: moderne Control-Komposition bewahrt Demo-Zweck; EN: modern control composition preserves demo intent | DE: Proof mit `ProgBa` vergleichen; EN: compare proof with `ProgBa` | `MatrixCompletesContract` |

## Wave 3 / Wave 3

| Projekt und Guide | Lernziel / Goal | Voraussetzung | Start | Bedienung / Operation | Architektur / Architecture | Übung / Exercise | Entscheidung |
|---|---|---|---|---|---|---|---|
| `BHelp` [Guide](examples/bhelp.md) | DE: Help-Kontext und Fallback; EN: help context and fallback | P3 | `dotnet run --project examples/BHelp` | DE: Thema und unbekannten Kontext öffnen; EN: open a topic and unknown context | DE: sicherer `THelpFile` statt ungeprüftem `.tch`; EN: safe `THelpFile` instead of unchecked `.tch` | DE: Fallback-Text begründen; EN: explain the fallback text | `MatrixCompletesContract` |
| `HelpDemo` [Guide](examples/helpdemo.md) | DE: Hilfe-Navigation; EN: help navigation | P3 | `dotnet run --project examples/HelpDemo` | DE: F1, Querverweis, Zurück; EN: F1, cross-reference, back | DE: `THelpWindow` und persistierter Graph; EN: `THelpWindow` and persisted graph | DE: zwei Kontexte und Back prüfen; EN: inspect two contexts and back | `MatrixCompletesContract` |
| `I18n` [Guide](examples/i18n.md) | DE: explizite Sprach-Fallbacks; EN: explicit language fallbacks | P3 | `dotnet run --project examples/I18n` | DE: Sprache wechseln, fehlenden Key prüfen; EN: switch language, inspect missing key | DE: case-sensitive Ressourcen ohne Ambient Locale; EN: case-sensitive resources without ambient locale | DE: Fallback-Reihenfolge erklären; EN: explain fallback order | `MatrixCompletesContract` |
| `TvEdit` [Guide](examples/tvedit.md) | DE: Editor- und Safe-Close-Fluss; EN: editor and safe-close flow | P3 | `dotnet run --project examples/TvEdit` | DE: editieren, suchen, speichern, schließen; EN: edit, search, save, close | DE: bestehende `TEditor`-/`TFileEditor`-Verträge; EN: existing editor contracts | DE: geänderte Datei sicher verwerfen; EN: safely reject a modified file | `MatrixCompletesContract` |
| `TvHc` [Guide](examples/tvhc.md) | DE: Help-Quelle kompilieren; EN: compile help source | P3 | `dotnet run --project examples/TvHc` | DE: gültige und ungültige Source prüfen; EN: inspect valid and invalid source | DE: atomarer Compiler ohne Teilmodell; EN: atomic compiler without partial model | DE: Forward-Reference und Fehler vergleichen; EN: compare forward reference and error | `MatrixCompletesContract` |

## Wave 4 / Wave 4

| Projekt und Guide | Lernziel / Goal | Voraussetzung | Start | Bedienung / Operation | Architektur / Architecture | Übung / Exercise | Entscheidung |
|---|---|---|---|---|---|---|---|
| `Cyrillic` [Guide](examples/cyrillic.md) | DE: KOI8-R-Mapping; EN: KOI8-R mapping | P2 | `dotnet run --project examples/Cyrillic` | DE: Mapping und Fallback anzeigen; EN: show mapping and fallback | DE: deterministische Cells statt Host-Locale; EN: deterministic cells instead of host locale | DE: bekannten Bytewert prüfen; EN: inspect a known byte value | `MatrixCompletesContract` |
| `ETerm` [Guide](examples/eterm.md) | DE: begrenzte Emulation; EN: bounded emulation | P2 | `dotnet run --project examples/ETerm` | DE: Sequenz ausführen und Ablehnung prüfen; EN: execute a sequence and inspect rejection | DE: In-Process-Session ohne Shell; EN: in-process session without shell | DE: unsupported Folge isolieren; EN: isolate an unsupported sequence | `MatrixCompletesContract` |
| `Fonts` [Guide](examples/fonts.md) | DE: feste Font-Fixture; EN: fixed font fixture | P2 | `dotnet run --project examples/Fonts` | DE: Glyphen/Bytes anzeigen; EN: display glyphs and bytes | DE: source-controlled 8x16-Daten statt Host-Font; EN: source-controlled 8x16 data instead of host font | DE: Glyphenzeile mit Bytewert verbinden; EN: relate a glyph row to a byte | `MatrixCompletesContract` |
| `Terminal` [Guide](examples/terminal.md) | DE: Terminalpuffer und Cursor; EN: terminal buffer and cursor | P2 | `dotnet run --project examples/Terminal` | DE: Eingabe, Ausgabe, Cursor, Fallback; EN: input, output, cursor, fallback | DE: `TTerminalView` über kontrollierter Session; EN: `TTerminalView` over a controlled session | DE: ungültige Sequenz und Recovery prüfen; EN: inspect invalid sequence and recovery | `MatrixCompletesContract` |
| `XTerm` [Guide](examples/xterm.md) | DE: XTerm-Profilgrenze; EN: XTerm profile boundary | P2 | `dotnet run --project examples/XTerm` | DE: Capability-Profil und Fallback; EN: capability profile and fallback | DE: explizites Profil statt Terminalnamen-Raten; EN: explicit profile instead of terminal-name guessing | DE: zwei Profile vergleichen; EN: compare two profiles | `MatrixCompletesContract` |

## TP7, Wave 5 und Wave 6 / TP7, Wave 5, and Wave 6

| Projekt und Guide | Lernziel / Goal | Voraussetzung | Start | Bedienung / Operation | Architektur / Architecture | Übung / Exercise | Entscheidung |
|---|---|---|---|---|---|---|---|
| `Tp7Demo` [Guide](examples/tp7-demo.md) | DE: vollständige Anwendungsshell; EN: complete application shell | P1 | `dotnet run --project examples/Tp7Demo` | DE: Fenster, Tile, Cascade, F1; EN: windows, tile, cascade, F1 | DE: bestehender Desktop- und Window-Vertrag; EN: existing desktop and window contract | DE: Fokus nach `Next` erklären; EN: explain focus after `Next` | `MatrixCompletesContract` |
| `Tp7Edit` [Guide](examples/tp7-edit.md) | DE: kontrollierter Editor; EN: controlled editor | P3 | `dotnet run --project examples/Tp7Edit` | DE: editieren, speichern, abbrechen; EN: edit, save, cancel | DE: test-eigene Dateigrenze und `TFileEditor`; EN: test-owned file boundary and `TFileEditor` | DE: Safe-Close beweisen; EN: prove safe close | `MatrixCompletesContract` |
| `Tp7Help` [Guide](examples/tp7-help.md) | DE: Help-Compiler und Viewer; EN: help compiler and viewer | P3 | `dotnet run --project examples/Tp7Help` | DE: Kontext, Link und Fallback; EN: context, link, and fallback | DE: Compiler-Ausgabe nutzt sichere Help-Verträge; EN: compiler output uses safe help contracts | DE: unbekannten Kontext prüfen; EN: inspect an unknown context | `MatrixCompletesContract` |
| `Tp7ResourceDemo` [Guide](examples/tp7-resource-demo.md) | DE: Ressourcen exakt laden; EN: load resources exactly | P3 | `dotnet run --project examples/Tp7ResourceDemo` | DE: Key lesen und malformed Daten ablehnen; EN: read a key and reject malformed data | DE: `TResourceFile` mit exakter Registry; EN: `TResourceFile` with exact registry | DE: Groß-/Kleinschreibung testen; EN: test key casing | `MatrixCompletesContract` |
| `Tp7ResourceGenerator` [Guide](examples/tp7-resource-generator.md) | DE: kontrollierte Ressourcenausgabe; EN: controlled resource output | P3 | `dotnet run --project examples/Tp7ResourceGenerator` | DE: test-eigenes Ziel erzeugen; EN: generate a test-owned target | DE: atomarer Writer ohne beliebigen Nutzerpfad; EN: atomic writer without arbitrary user path | DE: zwei Ausgaben bytegleich vergleichen; EN: compare two outputs byte for byte | `MatrixCompletesContract` |
| `Tp7AsciiTable` [Guide](examples/tp7-ascii-table.md) | DE: Bytewerte und Zeichen; EN: byte values and characters | P1 | `dotnet run --project examples/Tp7AsciiTable` | DE: Tabelle navigieren und F1; EN: navigate the table and use F1 | DE: Zellen tragen Zeichen und Attribute; EN: cells carry characters and attributes | DE: drei Bytewerte erklären; EN: explain three byte values | `MatrixCompletesContract` |
| `Tp7Calculator` [Guide](examples/tp7-calculator.md) | DE: Rechnerzustand; EN: calculator state | P1 | `dotnet run --project examples/Tp7Calculator` | DE: Ziffern, Operator, Ergebnis, Fehler; EN: digits, operator, result, error | DE: begrenzte Demo-Logik in sichtbaren Controls; EN: bounded demo logic in visible controls | DE: Division-durch-null-Pfad prüfen; EN: inspect division by zero | `MatrixCompletesContract` |
| `Tp7Calendar` [Guide](examples/tp7-calendar.md) | DE: deterministischen Monat; EN: deterministic month | P1 | `dotnet run --project examples/Tp7Calendar` | DE: Monat vor/zurück, Status; EN: previous/next month, status | DE: explizites Datum statt Ambient-Zeit; EN: explicit date instead of ambient time | DE: Monatsgrenze prüfen; EN: inspect a month boundary | `MatrixCompletesContract` |
| `Tp7Puzzle` [Guide](examples/tp7-puzzle.md) | DE: reproduzierbares Puzzle; EN: reproducible puzzle | P1 | `dotnet run --project examples/Tp7Puzzle` | DE: Stein bewegen, ungültigen Zug prüfen; EN: move a tile, inspect invalid move | DE: feste Fixture statt Zufallsstart; EN: fixed fixture instead of random start | DE: Zustand in drei Zügen notieren; EN: record state across three moves | `MatrixCompletesContract` |
| `Tp7MouseDialog` [Guide](examples/tp7-mouse-dialog.md) | DE: Maus plus Tastaturfallback; EN: mouse plus keyboard fallback | P2 | `dotnet run --project examples/Tp7MouseDialog` | DE: Klick oder Tastatur, Doppelklickgrenze; EN: click or keyboard, double-click boundary | DE: SGR-Ingress wird vor Dispatch validiert; EN: SGR ingress is validated before dispatch | DE: denselben Befehl per Tastatur auslösen; EN: invoke the same command by keyboard | `MatrixCompletesContract` |
| `Tp7FileManager` [Guide](examples/tp7-file-manager.md) | DE: kontrollierten Dateimanager; EN: controlled file manager | P3 | `dotnet run --project examples/Tp7FileManager` | DE: navigieren, markieren, sichere Dateiaktion, F1; EN: navigate, select, safe file action, F1 | DE: Framework-Dialoge über test-eigener Sandbox; EN: framework dialogs over a test-owned sandbox | DE: Abbruch ohne Änderung beweisen; EN: prove cancel without change | `GuideAdequate` |

## Zusätzliche Framework-Beispiele / Additional Framework Examples

| Projekt und Guide | Lernziel / Goal | Voraussetzung | Start | Bedienung / Operation | Architektur / Architecture | Übung / Exercise | Entscheidung |
|---|---|---|---|---|---|---|---|
| `A11yFramework` [Guide](a11y-framework.md) | DE: textbasierte A11Y-Verträge; EN: text-based accessibility contracts | P1 | `dotnet run --project examples/A11yFramework/A11yFramework.csproj` | DE: Fokus, Shortcuts, High Contrast, F1; EN: focus, shortcuts, high contrast, F1 | DE: opt-in `IAccessibleWidget` ohne native Bridge-Behauptung; EN: opt-in widget contract without native bridge claim | DE: Fokus-Snapshot und sichtbaren Text vergleichen; EN: compare focus snapshot and visible text | `MatrixCompletesContract` |
| `FormTransaction` [Guide](transactional-form-model.md) | DE: transaktionale Felder und Submit; EN: transactional fields and submit | P3 | `dotnet run --project examples/FormTransaction/FormTransaction.csproj` | DE: ändern, validieren, persistieren, Accept/Reject; EN: edit, validate, persist, accept/reject | DE: additive Session über normalen Controls; EN: additive session over ordinary controls | DE: Stale- und Cancel-Pfad vergleichen; EN: compare stale and cancellation | `MatrixCompletesContract` |

## Abschluss / Completion

Wähle ein Beispiel aus jeder Gruppe und führe Start, primäre Bedienung, F1 und
`Ctrl+Q` aus. Wenn ein Guide oder Beispiel nicht mit der Matrix übereinstimmt,
ist die Matrix kein Ersatz für die Korrektur: dokumentiere den reproduzierbaren
Befund und aktualisiere die kanonische Detailquelle.

*Choose one example from each group and exercise launch, primary operation, F1,
and `Ctrl+Q`. If a guide or example disagrees with this matrix, the matrix is
not a substitute for correction: record the reproducible finding and update
the canonical detailed source.*
