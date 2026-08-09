# Abnahmevertrag: Beispielportfolio-Audit / Acceptance Contract: Example Portfolio Audit

## 1. Eingabevertrag / Input Contract

1. Der bindende Intake, sein `Ready`-Review, das `Eligible`-Serienmanifest und
   Receipt stimmen mit den vier Hashes im Feature-State überein.
2. Feature 037 belegt Wave 6 als `Closed`, den Portfolioaudit als `Eligible`,
   null Candidate Findings und null Product Decisions.
3. Die akzeptierten Feature-024/029/030-Pins und ihre lokalen Evidence-Dateien
   werden unverändert verwendet. Bewegliche Upstreams werden nicht gelesen.
4. Historische und externe Quellen sind read-only. Zusammenfassungen sind
   eigene Worte; keine Quelle wird kopiert, mechanisch übersetzt oder vendoriziert.

*The accepted intake lineage, Feature 037 closure, and local Feature-024/029/030
pins are immutable inputs. Historical and external sources remain read-only;
moving upstreams are not accessed.*

## 2. Exakter 37-Zeilen-Vertrag / Exact 37-row contract

Die folgende Tabelle fixiert ID, Rolle, Wave, normalen Entry-Point, Guide und
die bereits akzeptierte Evidence-Basis. Das spätere Quellenmanifest löst die
genannten historischen Authorities in konkrete Source-IDs, Pfade und Hashes
auf. Keine Zeile darf fehlen, doppelt oder unbekannt sein.

*The following table fixes the ID, role, wave, normal entry point, guide, and
accepted evidence baseline. The later source manifest resolves each historical
authority to concrete source IDs, paths, and hashes. No row may be missing,
duplicated, or unknown.*

| ExampleId | Name | Rolle / Role | Wave | Entry-Point | Guide | Historische Authority / Historical authority | Akzeptierte Evidence-Basis / Accepted evidence baseline |
|---|---|---|---|---|---|---|---|
| `EX001` | `Desklogo` | `HistoricalExample` | `Wave1` | `examples/Desklogo/Program.cs` | `docs/guides/examples/desklogo.md` | `tv203s/contrib/tvision/examples/desklogo/` | `specs/014-wave1-functional-hardening/pr-evidence.md`; `specs/017-wave1-visual-component-remediation/pr-evidence.md`; `tests/TuiVision.Examples.SmokeTests/DesklogoSmokeTests.cs` |
| `EX002` | `MsgCls` | `HistoricalExample` | `Wave1` | `examples/MsgCls/Program.cs` | `docs/guides/examples/msgcls.md` | `tv203s/contrib/tvision/examples/msgcls/` | Features 014/017 Evidence; `tests/TuiVision.Examples.SmokeTests/MsgClsSmokeTests.cs` |
| `EX003` | `Tutorial` | `HistoricalExample` | `Wave1` | `examples/Tutorial/Program.cs` | `docs/guides/examples/tutorial.md` | `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc`–`tvguid16.cc` | Features 014/017 Evidence; `TutorialSmokeTests.cs`; `Wave1VisualSmokeMatrixTests.cs` |
| `EX004` | `Videomode` | `HistoricalExample` | `Wave1` | `examples/Videomode/Program.cs` | `docs/guides/examples/videomode.md` | `tv203s/contrib/tvision/examples/videomode/` | Features 014/017 Evidence; `VideomodeSmokeTests.cs`; `Wave1VisualSmokeMatrixTests.cs` |
| `EX005` | `Clipboard` | `HistoricalExample` | `Wave2` | `examples/Clipboard/Program.cs` | `docs/guides/examples/clipboard.md` | `tv203s/contrib/tvision/examples/clipboard/`; `include/tv/osclipboard.h` | Features 012/013 Evidence; `ClipboardSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX006` | `Demo` | `HistoricalExample` | `Wave2` | `examples/Demo/Program.cs` | `docs/guides/examples/demo.md` | `tv203s/contrib/tvision/examples/demo/` einschließlich `tvdemo*`, Gadgets und Views | Features 012/013 Evidence; `DemoSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX007` | `DlgDsn` | `HistoricalExample` | `Wave2` | `examples/DlgDsn/Program.cs` | `docs/guides/examples/dlgdsn.md` | `tv203s/contrib/tvision/examples/dlgdsn/` | Features 012/013 Evidence; `DlgDsnSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX008` | `DynTxt` | `HistoricalExample` | `Wave2` | `examples/DynTxt/Program.cs` | `docs/guides/examples/dyntxt.md` | `tv203s/contrib/tvision/examples/dyntext/` | Features 012/013 Evidence; `DynTxtSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX009` | `InpLis` | `HistoricalExample` | `Wave2` | `examples/InpLis/Program.cs` | `docs/guides/examples/inplis.md` | `tv203s/contrib/tvision/examples/inplist/` | Features 012/013 Evidence; `InpLisSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX010` | `ListVi` | `HistoricalExample` | `Wave2` | `examples/ListVi/Program.cs` | `docs/guides/examples/listvi.md` | `tv203s/contrib/tvision/examples/lst_view/` und `classes/tlistvie.cc` | Features 012/013 Evidence; `ListViSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX011` | `ProgBa` | `HistoricalExample` | `Wave2` | `examples/ProgBa/Program.cs` | `docs/guides/examples/progba.md` | `tv203s/contrib/tvision/examples/progbar/` | Features 012/013 Evidence; `ProgBaSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX012` | `Sdlg` | `HistoricalExample` | `Wave2` | `examples/Sdlg/Program.cs` | `docs/guides/examples/sdlg.md` | `tv203s/contrib/tvision/examples/sdlg/` | Features 012/013 Evidence; `SdlgSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX013` | `Sdlg2` | `HistoricalExample` | `Wave2` | `examples/Sdlg2/Program.cs` | `docs/guides/examples/sdlg2.md` | `tv203s/contrib/tvision/examples/sdlg2/` | Features 012/013 Evidence; `Sdlg2SmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX014` | `TCombo` | `HistoricalExample` | `Wave2` | `examples/TCombo/Program.cs` | `docs/guides/examples/tcombo.md` | `tv203s/contrib/tvision/examples/tcombo/` | Features 012/013 Evidence; `TComboSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX015` | `TProgB` | `HistoricalExample` | `Wave2` | `examples/TProgB/Program.cs` | `docs/guides/examples/tprogb.md` | `tv203s/contrib/tvision/examples/tprogb/` | Features 012/013 Evidence; `TProgBSmokeTests.cs`; `Wave2InteractiveSmokeMatrixTests.cs` |
| `EX016` | `BHelp` | `HistoricalExample` | `Wave3` | `examples/BHelp/Program.cs` | `docs/guides/examples/bhelp.md` | `tv203s/contrib/tvision/examples/bhelp/` | `specs/019-wave3-visual-component-porting/pr-evidence.md`; `BHelpSmokeTests.cs`; `Wave3VisualSmokeMatrixTests.cs` |
| `EX017` | `HelpDemo` | `HistoricalExample` | `Wave3` | `examples/HelpDemo/Program.cs` | `docs/guides/examples/helpdemo.md` | `tv203s/contrib/tvision/examples/helpdemo/` | Features 018/019 Evidence; `HelpDemoSmokeTests.cs`; `Wave3VisualSmokeMatrixTests.cs` |
| `EX018` | `I18n` | `HistoricalExample` | `Wave3` | `examples/I18n/Program.cs` | `docs/guides/examples/i18n.md` | `tv203s/contrib/tvision/examples/i18n/` | Features 018/019 Evidence; `I18nSmokeTests.cs`; `Wave3VisualSmokeMatrixTests.cs` |
| `EX019` | `TvEdit` | `HistoricalExample` | `Wave3` | `examples/TvEdit/Program.cs` | `docs/guides/examples/tvedit.md` | `tv203s/contrib/tvision/examples/tvedit/` | Features 018/019 Evidence; `TvEditSmokeTests.cs`; `Wave3VisualSmokeMatrixTests.cs` |
| `EX020` | `TvHc` | `HistoricalExample` | `Wave3` | `examples/TvHc/Program.cs` | `docs/guides/examples/tvhc.md` | `tv203s/contrib/tvision/examples/tvhc/` | Features 018/019 Evidence; `TvHcSmokeTests.cs`; `Wave3VisualSmokeMatrixTests.cs` |
| `EX021` | `Cyrillic` | `HistoricalExample` | `Wave4` | `examples/Cyrillic/Program.cs` | `docs/guides/examples/cyrillic.md` | passende read-only Linux/X11-Cyrillic-Quellen unter `tv203s/` | Features 021/022 Evidence; `CyrillicSmokeTests.cs`; `Wave4VisualSmokeMatrixTests.cs` |
| `EX022` | `ETerm` | `HistoricalExample` | `Wave4` | `examples/ETerm/Program.cs` | `docs/guides/examples/eterm.md` | passende read-only ETerm-Konfigurationen unter `tv203s/` | Features 021/022 Evidence; `ETermSmokeTests.cs`; `Wave4VisualSmokeMatrixTests.cs` |
| `EX023` | `Fonts` | `HistoricalExample` | `Wave4` | `examples/Fonts/Program.cs` | `docs/guides/examples/fonts.md` | passende Font-Quellen und `font.016` unter `tv203s/` | Features 021/022 Evidence; `FontsSmokeTests.cs`; `Wave4VisualSmokeMatrixTests.cs` |
| `EX024` | `Terminal` | `HistoricalExample` | `Wave4` | `examples/Terminal/Program.cs` | `docs/guides/examples/terminal.md` | `tv203s/contrib/tvision/examples/terminal/`; `include/tv/terminal.h` | Features 021/022 Evidence; `TerminalSmokeTests.cs`; `Wave4VisualSmokeMatrixTests.cs` |
| `EX025` | `XTerm` | `HistoricalExample` | `Wave4` | `examples/XTerm/Program.cs` | `docs/guides/examples/xterm.md` | passende XTerm-Ressourcen und Implementierungen unter `tv203s/` | Features 021/022 Evidence; `XTermSmokeTests.cs`; `Wave4VisualSmokeMatrixTests.cs` |
| `EX026` | `Tp7AsciiTable` | `HistoricalExample` | `Wave5` | `examples/Tp7AsciiTable/Program.cs` | `docs/guides/examples/tp7-ascii-table.md` | `TVDEMOS/ASCIITAB*` | Features 032/033/034 Evidence; `Tp7DomainSmokeTests.cs`; Wave-5 functional/showcase/closure matrices |
| `EX027` | `Tp7Calculator` | `HistoricalExample` | `Wave5` | `examples/Tp7Calculator/Program.cs` | `docs/guides/examples/tp7-calculator.md` | `TVDEMOS/CALC*` | Features 032/033/034 Evidence; `Tp7CalculatorSmokeTests.cs`; Wave-5 matrices |
| `EX028` | `Tp7Calendar` | `HistoricalExample` | `Wave5` | `examples/Tp7Calendar/Program.cs` | `docs/guides/examples/tp7-calendar.md` | `TVDEMOS/CALENDAR*` | Features 032/033/034 Evidence; `Tp7DomainSmokeTests.cs`; Wave-5 matrices |
| `EX029` | `Tp7Demo` | `HistoricalExample` | `Wave5` | `examples/Tp7Demo/Program.cs` | `docs/guides/examples/tp7-demo.md` | `TVDEMOS/TVDEMO*`, `DEMOCMDS`, `DEMOSTRS`, `GADGETS` | Features 032/033/034 Evidence; `Tp7ApplicationSmokeTests.cs`; Wave-5 matrices |
| `EX030` | `Tp7Edit` | `HistoricalExample` | `Wave5` | `examples/Tp7Edit/Program.cs` | `docs/guides/examples/tp7-edit.md` | `TVDEMOS/TVEDIT*` | Features 032/033/034 Evidence; `Tp7ApplicationSmokeTests.cs`; Wave-5 matrices |
| `EX031` | `Tp7Help` | `HistoricalExample` | `Wave5` | `examples/Tp7Help/Program.cs` | `docs/guides/examples/tp7-help.md` | `TVDEMOS/TVHC*`, `HELPFILE*`, `DEMOHELP*` | Features 032/033/034 Evidence; `Tp7ApplicationSmokeTests.cs`; Wave-5 matrices |
| `EX032` | `Tp7MouseDialog` | `HistoricalExample` | `Wave5` | `examples/Tp7MouseDialog/Program.cs` | `docs/guides/examples/tp7-mouse-dialog.md` | `TVDEMOS/MOUSEDLG*` | Features 032/033/034 Evidence; `Tp7DomainSmokeTests.cs`; Wave-5 matrices |
| `EX033` | `Tp7Puzzle` | `HistoricalExample` | `Wave5` | `examples/Tp7Puzzle/Program.cs` | `docs/guides/examples/tp7-puzzle.md` | `TVDEMOS/PUZZLE*` | Features 032/033/034 Evidence; `Tp7DomainSmokeTests.cs`; Wave-5 matrices |
| `EX034` | `Tp7ResourceDemo` | `HistoricalExample` | `Wave5` | `examples/Tp7ResourceDemo/Program.cs` | `docs/guides/examples/tp7-resource-demo.md` | `TVDEMOS/TVRDEMO*` | Features 032/033/034 Evidence; `Tp7ResourceSmokeTests.cs`; Wave-5 matrices |
| `EX035` | `Tp7ResourceGenerator` | `HistoricalExample` | `Wave5` | `examples/Tp7ResourceGenerator/Program.cs` | `docs/guides/examples/tp7-resource-generator.md` | `TVDEMOS/GENRDEMO*` | Features 032/033/034 Evidence; `Tp7ResourceSmokeTests.cs`; Wave-5 matrices |
| `EX036` | `Tp7FileManager` | `HistoricalExample` | `Wave6` | `examples/Tp7FileManager/Program.cs` | `docs/guides/examples/tp7-file-manager.md` | exakt 24 direkte historische Dateien unter `TVFM/` gemäß Feature 037 | Features 035/036/037 Evidence; Wave-6 controlled workspace, functional, showcase and closure tests |
| `EX037` | `A11yFramework` | `SupplementalControl` | `Supplemental` | `examples/A11yFramework/Program.cs` | `docs/guides/a11y-framework.md` | `N/A`: keine historische Portfolio-Authority; Trigger ist eine künftig akzeptierte historische Zuordnung | `specs/023-a11y-framework/pr-evidence.md`; `tests/TuiVision.Examples.SmokeTests/A11yFrameworkSmokeTests.cs` |

Die verkürzten Testdateinamen in der Tabelle liegen jeweils unter
`tests/TuiVision.Examples.SmokeTests/`. Die kanonische JSON-Evidence speichert
immer den vollständigen repository-relativen Pfad und, wo nötig, Testnamen
oder Markdown-Anker.

*Short test filenames in the table are under
`tests/TuiVision.Examples.SmokeTests/`. Canonical JSON evidence always records
the complete repository-relative path and, where needed, test name or anchor.*

## 3. Relationsvertrag / Relation Contract

1. Jeder `HistoricalSourceId` und `ModernizationSourceId` existiert genau
   einmal im Manifest und nennt die Portfoliozeile in seiner Rückrelation.
2. Jeder `EvidenceId` existiert genau einmal, zeigt auf einen kontrollierten
   Pfad und nennt alle referenzierenden Zeilen.
3. Jede `FindingId` ist zwischen Portfoliozeile und Finding reziprok.
4. Kein Source-, Evidence-, Finding-, Owner- oder Intake-Knoten ist verwaist.
5. `A11yFramework` hat keine historische Source-ID und eine begründete
   `HistoricalRelation=N/A`; Learning-, A11Y- oder Proof-Findings bleiben möglich.
6. Ein Vergleichsprojekt ist nie Produktnorm. Übereinstimmung oder Abweichung
   ohne reproduzierbare TuiVision-Lücke erzeugt kein Finding.

## 4. Entscheidungsvertrag / Decision Contract

- Jede Zeile besitzt genau eine `PortfolioRole`, eine `FrameworkDecision` und
  eine `PrimaryDisposition`.
- Jede der zehn Dimensionen besitzt genau einen Status aus `Pass`,
  `IntentionalDeviation`, `Gap`, `N/A`.
- `N/A` besitzt Begründung und Re-Evaluationsauslöser.
- `Gap` verweist auf genau ein dedupliziertes Finding oder blockiert als
  `ProductDecision`.
- `AcceptedAsIs` und `AcceptedIntentionalDeviation` enthalten kein Gap.
- Struktur-, Optik-, API-, Layout-, Vererbungs-, Speicher- oder
  Quelltextunterschied allein ist kein Finding.

## 5. Proof-, Lern- und A11Y-Vertrag / Proof, Learning, and A11Y Contract

Jede Zeile nennt sichtbaren Startzweck, Hauptinteraktion, Framework-Komponenten,
lokale Sonderlogik, App-Loop-/State-/View-/Cell-Proof, StatusLine,
`Help -> Description`, negative beziehungsweise Fallback-Pfade, Guide,
Lernziel, Fokus, Shortcuts, High Contrast, textbasierte Rückmeldung,
Small-Terminal- und Plattformgrenzen oder ein begründetes `N/A`.

*Each row records visible first-screen purpose, primary interaction, framework
components, local special logic, app-loop/state/view/cell proof, status line,
description, negative/fallback paths, guide, learning goal, focus, shortcuts,
high contrast, textual feedback, small-terminal behavior, and platform limits
or a justified `N/A`.*

Datei- und Persistenz-Proof verwendet nur source-controlled Fixtures oder
test-eigene temporäre Verzeichnisse. Beliebige Nutzerdaten bleiben unberührt.

## 6. Finding- und Deduplizierungsvertrag / Finding and Deduplication Contract

- Findings beginnen bei `EF001` und sind lückenlos.
- Gleiche Ursachen teilen exakt einen kontrollierten `DeduplicationKey` und
  ein Finding mit allen betroffenen, ordinal sortierten `ExampleIds`.
- Jedes Finding hat genau einen Primary Owner aus `FrameworkReuse`,
  `BehaviorInteraction`, `ProofPlatform`, `LearningA11Y`.
- Das administrative Feld `Owner` ist separat; Cross-Cutting-Wirkungen stehen
  in `SecondaryImpacts`.
- Reproduktion, Source-Relationen, Risiko, Red-Proof,
  Real-Path-Green-Proof, API-/A11Y-/Plattformwirkung, Review und Trigger sind
  vollständig.
- Nicht reproduzierbare Beobachtungen werden verworfen oder blockieren; sie
  werden nicht als Follow-up ausgegeben.

## 7. Handoff- und Closure-Vertrag / Handoff and Closure Contract

1. Jede nicht leere Owner-Gruppe erzeugt genau einen unnummerierten
   Remediation-Intake mit Findings, Dependencies und Proof-Anforderungen.
2. Jede leere Gruppe wird mit `Suppressed` dokumentiert und erzeugt keine Datei.
3. Der Owner-Graph ist azyklisch; die Intake-Reihenfolge ist topologisch.
4. Nach allen tatsächlich emittierten Remediation-Intakes folgt genau ein
   unnummerierter `Lastenheft_Example-Portfolio-Closure.md`.
5. Weder Remediation noch Closure erhält in Feature 038 eine Feature-Nummer,
   einen Branch oder einen gestarteten Lauf.
6. Nur der spätere Closure darf vollständige Konformität und Lernreife erklären.

## 8. Governance- und Scope-Vertrag / Governance and Scope Contract

- Alle zwölf Presets und alle benannten Standards besitzen getrennte
  Applicability-/Implementation-Felder sowie Rationale, Evidence, Owner,
  Reviewer, Restrisiko, Trigger und Follow-up.
- NIST SSDF, CWE Top 25, iSAQB-Review, A11Y, Plattformbewertung,
  Agentenparitätsentscheidung, Intake- und Autonomous-Governance sind
  proportional anwendbar.
- Web/API/Auth, Runtime-AI, neue Release-/Supply-Chain-Artefakte, Cloud,
  Distributed Services und Scripts sind `N/A`, solange ihr Trigger nicht im
  tatsächlichen Diff erscheint.
- Der finale Diff enthält null Änderung unter `src/`, `examples/`, `tv203s/`,
  `TVDEMOS/`, `TVFM/`, null Public-API-/Dependency-/Projektänderung und null
  ungepinnte externe Quelle.

## 9. Lokaler Gate-Vertrag / Local Gate Contract

Das Audit ist lokal nur abgeschlossen, wenn Portfolio-, Relations-, Finding-,
Handoff-, Scope-, targeted Test-, vollständige Release-, Coverage-, Format-,
Dokumentations/A11Y- und Governance-Gates bestehen. Im aktuellen
`MergeAndSync`-Lauf müssen Remote Exact-Head, Review, Merge und
`main`-Synchronisierung zusätzlich wahrheitsgemäß belegt werden; ein kausaler
Closeout ist nur bei einer echten Post-Merge-Evidence-Lücke zulässig.

*The local audit completes only after every applicable portfolio, relation,
finding, handoff, scope, test, coverage, format, documentation/A11Y, and
governance gate passes. The current `MergeAndSync` run must additionally prove
remote exact-head delivery, review, merge, and default-branch synchronization;
a causal closeout is used only for a genuine post-merge evidence gap.*
