# Porting-Status / M-07 Proof Ledger

<!-- Zweck / Purpose -->
<!-- Dieses Dokument ist das kanonische M-07-Nachweis-Artefakt.                                      -->
<!-- Jede historische .cc-Implementierungsdatei aus tv203s/contrib/tvision/classes/ hat genau eine   -->
<!-- Zeile mit Pfad, Fähigkeitsgruppe, Primärziel, optionalen Sekundärzielen, einem erlaubten        -->
<!-- Statuswert, einem Nachweis-Verweis und einer Begründungsnotiz für nicht-triviale Entscheidungen. -->
<!--                                                                                                  -->
<!-- This document is the canonical M-07 proof artifact.                                             -->
<!-- Every historical .cc implementation file from tv203s/contrib/tvision/classes/ has exactly one   -->
<!-- row with path, capability bucket, primary target, optional secondary targets, one allowed        -->
<!-- status value, an evidence reference, and a rationale note for non-trivial decisions.             -->

**Branch**: `006-close-phase8-gate` | **Datum / Date**: 2026-03-27

---

## Erlaubte Statuswerte / Allowed Status Values

| Statuswert | Bedeutung |
|---|---|
| `portiert + getestet` | Fähigkeit ist im verwalteten Zielprojekt implementiert und durch MSTest abgedeckt. / Capability implemented in managed target and covered by MSTest. |
| `portiert + Test ausstehend` | Provisorischer Zwischenstatus für laufende Portierungsarbeit; im finalen Phase-8-Paket nicht mehr verwendet. / Provisional in-progress state for ongoing porting work; no longer used in the final Phase-8 package. |
| `bewusst ausgelassen + Begruendung` | Fähigkeit wird bewusst nicht portiert; Begründung ist in der Zeile angegeben. / Capability consciously not ported; rationale given in the row. |

---

## Fähigkeitsgruppen / Capability Buckets

| Kürzel | Name | Beschreibung / Description |
|---|---|---|
| `Darstellung` | Screen Presentation | Bildschirmausgabe, Rahmenzeichen, Cursorbewegung. / Screen output, frame characters, cursor movement. |
| `Tastatureingabe` | Keyboard Input | Tastatureingabe-Verarbeitung und Scancode-Übersetzung. / Keyboard input processing and scancode translation. |
| `Mauseingabe` | Mouse Input | Mausereignis-Verarbeitung und Koordinatenauflösung. / Mouse event processing and coordinate resolution. |
| `Anzeigeadaption` | Display Adaptation | Zeichensätze, Codepages, Schriften, Hardware-Farbkonfiguration. / Character sets, codepages, fonts, hardware colour configuration. |
| `Terminalmodus` | Terminal Mode Control | Terminalmodus-Einstellung (raw/cooked) und Teardown. / Terminal mode setup (raw/cooked) and teardown. |
| `Framework-Kern` | Framework Core | Basistypen: TObject, Sammlungen, Sortierung, Punkt/Rechteck. / Base types: TObject, collections, sorting, point/rect. |
| `Zeichenpuffer` | Draw Buffer | Zeichenpuffer und Zellmodell. / Draw buffer and cell model. |
| `Ereignissteuerung` | Event System | TEvent, Mausabstraktion, Tastatur-Abstraktion. / TEvent, mouse abstraction, keyboard abstraction. |
| `Steuerelement` | Controls | TView-Hierarchie, Fenster, Dialoge, Eingabefelder, Listen. / TView hierarchy, windows, dialogs, input fields, lists. |
| `Serialisierung` | Serialization | Persistente Streams, binäres Archivformat, Typregistrierung. / Persistent streams, binary archive format, type registry. |
| `Editor` | Editor | TEditor, TMemo, Datei-Editor-Verbund. / TEditor, TMemo, file-editor compound. |
| `Hilfesystem` | Help System | Hilfedatei-Format, Index, Topic-Anzeige. / Help file format, index, topic display. |
| `Anwendungsrahmen` | Application Shell | TApplication, TDesktop, TProgram, Initialisierung. / TApplication, TDesktop, TProgram, initialisation. |
| `Ressourcen` | Resource Management | TResFile, TResCollection, benannte Ressourcen. / TResFile, TResCollection, named resources. |
| `OS-Clipboard` | OS Clipboard | Betriebssystem-Zwischenablage-Integration. / Operating-system clipboard integration. |
| `Lokalisierung` | Localisation | UI-Zeichenketten, Tastenbelegungstabellen, Internationaliserung. / UI strings, key-binding tables, internationalisation. |
| `Kalkulator` | Calculator | Rechnerwidget und Anzeige. / Calculator widget and display. |
| `Validierung` | Validation | Eingabevalidatoren und Bereichsprüfung. / Input validators and range checking. |
| `Druckausgabe` | Print/PW | Druckpuffer-Objekte (historisch DOS-only). / Print buffer objects (historically DOS-only). |
| `Terminal` | Terminal Device | TTterminal, TTextDevice-Abstraktionsschicht. / TTerminal, TTextDevice abstraction layer. |
| `Konfiguration` | Configuration | Anwendungskonfiguration und Programminitialisierung. / Application configuration and program initialisation. |

---

## Leitende Entscheidungen / Guiding Decisions

- Historische plattformspezifische Treiber (dos/, linux/, unix/, win32/, winnt/, qnx4/, qnxrtp/, wingr/, x11/) werden nach **Fähigkeitsgruppe** konsolidiert, nicht als Eins-zu-eins-Plattformverzweigungen.
- Historische DOS-, QNX4-, QNXrtp-, WinGR- und X11-Treiber werden **bewusst ausgelassen**, da ihre nativen Plattformabhängigkeiten im verwalteten .NET-10-Laufzeitmodell nicht reproduzierbar sind.
- Unix/Linux/Win32/WinNT-Treiberfähigkeiten werden durch die verwaltete `System.Console`-API von .NET 10 bereitgestellt und sind in `TuiVision.Drivers.Console` durch `TConsoleDriver` und `TConsoleBuffer` vertreten.
- Zugehörige `.h`/`.c`-Dateien (z. B. dos/vgastate.h, dos/vgaregs.h) sind keine eigenständigen M-07-Zeilen, werden aber im Begründungsfeld der zugehörigen `.cc`-Zeile referenziert.

*(English: Historical platform-specific drivers are consolidated by capability bucket, not as one-to-one platform forks. DOS/QNX/WinGR/X11 drivers are consciously omitted. Unix/Linux/Win32/WinNT capabilities are provided by the managed .NET 10 Console API.)*

---

## Gemeinsame Framework-Dateien / Shared Framework Files

Die folgenden Dateien befinden sich direkt unter `tv203s/contrib/tvision/classes/` ohne Plattform-Unterverzeichnis.

*(The following files reside directly under `tv203s/contrib/tvision/classes/` without a platform subdirectory.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/calcdisp.cc | Kalkulator | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | Kalkulator-Display ist ein DOS-spezifisches GUI-Widget; kein Äquivalent im verwalteten Konsolenmodell geplant. / Calculator display is a DOS-specific GUI widget; no equivalent planned in the managed console model. |
| tv203s/contrib/tvision/classes/calculat.cc | Kalkulator | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | Rechner-Logik ist eng mit DOS-nativen Elementen verbunden; außerhalb des Phase-7-Rahmens. / Calculator logic is tightly coupled to DOS-native elements; outside Phase-7 scope. |
| tv203s/contrib/tvision/classes/codepage.cc | Anzeigeadaption | TuiVision.Drivers.Console/DriverCapabilityMap.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | Codepage-Verwaltung wird durch Unicode- und `System.Text.Encoding`-APIs im verwalteten Laufzeitmodell ersetzt; der DriverCapabilityMap-/Proof-Test belegt diesen Ersatz. / Codepage management is replaced by Unicode and `System.Text.Encoding` APIs in the managed runtime model; the DriverCapabilityMap proof test covers that replacement. |
| tv203s/contrib/tvision/classes/configfile.cc | Konfiguration | TuiVision.Controls/TConfigFile.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | Konfigurationsdatei-Parsing ist als leichter Schlüssel-Wert-Speicher in TConfigFile portiert und per Proof-Test belegt. / Config-file parsing is ported as a lightweight key-value store in TConfigFile and covered by proof tests. |
| tv203s/contrib/tvision/classes/fontcoll.cc | Anzeigeadaption | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | DOS-/VGA-Schriftsammlung ohne verwaltetes Äquivalent; Unicode-Terminals handeln Schriftarten eigenständig. / DOS/VGA font collection without managed equivalent; Unicode terminals handle fonts independently. |
| tv203s/contrib/tvision/classes/fpbase.cc | Serialisierung | TuiVision.Serialization/fpstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Basis für Datei-Pointer-Streams ist in fpstream.cs portiert und durch die Serialization-Suite belegt. / The base for file-pointer streams is ported in fpstream.cs and covered by the serialization suite. |
| tv203s/contrib/tvision/classes/fpstream.cc | Serialisierung | TuiVision.Serialization/fpstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Datei-Pointer-Stream ist nach fpstream.cs portiert und per Serialization-Suite nachgewiesen. / The file-pointer stream is ported to fpstream.cs and verified by the serialization suite. |
| tv203s/contrib/tvision/classes/help.cc | Hilfesystem | TuiVision.Serialization/THelpFile.cs | TuiVision.Controls/THelpViewer.cs | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Hilfe-Laufzeit-Engine ist in THelpFile und THelpViewer portiert; Kontextlookup, Querverweise und Fallback sind automatisiert belegt. / The help runtime engine is ported into THelpFile and THelpViewer; context lookup, cross references, and fallback are covered by automated tests. |
| tv203s/contrib/tvision/classes/helpbase.cc | Hilfesystem | TuiVision.Serialization/THelpFile.cs | TuiVision.Serialization/THelpIndex.cs | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Hilfe-Basistypen sind in THelpFile und THelpIndex portiert und durch Hilfedatei-Tests belegt. / The help base types are ported into THelpFile and THelpIndex and covered by help-file tests. |
| tv203s/contrib/tvision/classes/ifpstrea.cc | Serialisierung | TuiVision.Serialization/fpstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Eingabe-Datei-Pointer-Stream ist in fpstream.cs zusammengeführt und durch die Serialization-Suite nachgewiesen. / The input file-pointer stream is merged into fpstream.cs and covered by the serialization suite. |
| tv203s/contrib/tvision/classes/iopstrea.cc | Serialisierung | TuiVision.Serialization/ipstream.cs | TuiVision.Serialization/opstream.cs | portiert + getestet | tests/TuiVision.Serialization.Tests/ | I/O-Persistent-Stream ist in ipstream.cs und opstream.cs portiert und per Roundtrip-/Fehlerfalltests belegt. / The I/O persistent stream is ported into ipstream.cs and opstream.cs and covered by round-trip and failure-case tests. |
| tv203s/contrib/tvision/classes/ipstream.cc | Serialisierung | TuiVision.Serialization/ipstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Eingabe-Persistent-Stream ist nach ipstream.cs portiert und durch die Serialization-Suite belegt. / The input persistent stream is ported to ipstream.cs and covered by the serialization suite. |
| tv203s/contrib/tvision/classes/ofpstrea.cc | Serialisierung | TuiVision.Serialization/fpstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Ausgabe-Datei-Pointer-Stream ist in fpstream.cs zusammengeführt und per Serialization-Suite belegt. / The output file-pointer stream is merged into fpstream.cs and covered by the serialization suite. |
| tv203s/contrib/tvision/classes/opstream.cc | Serialisierung | TuiVision.Serialization/opstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Ausgabe-Persistent-Stream ist nach opstream.cs portiert und durch die Serialization-Suite belegt. / The output persistent stream is ported to opstream.cs and covered by the serialization suite. |
| tv203s/contrib/tvision/classes/osclipboard.cc | OS-Clipboard | TuiVision.Controls/ManagedClipboard.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | Die Betriebssystem-Zwischenablage wird im managed Modell als testisolierte In-Process-Clipboard-Abstraktion umgesetzt. / The operating-system clipboard is implemented in the managed model as a test-isolated in-process clipboard abstraction. |
| tv203s/contrib/tvision/classes/pstream.cc | Serialisierung | TuiVision.Serialization/pstream.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | Persistent-Stream-Basis ist nach pstream.cs portiert und durch Roundtrip-/Fehlerfalltests belegt. / The persistent-stream base is ported to pstream.cs and covered by round-trip and failure-case tests. |
| tv203s/contrib/tvision/classes/tapplica.cc | Anwendungsrahmen | TuiVision.Controls/TApplication.cs | TuiVision.Controls/TProgram.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TApplication-Klasse; portiert nach TApplication.cs. / TApplication class; ported to TApplication.cs. |
| tv203s/contrib/tvision/classes/tbackgro.cc | Steuerelement | TuiVision.Controls/TDesktop.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TBackground als Teil des Desktop-Hintergrundmusters; in TDesktop zusammengeführt. / TBackground as part of the desktop background pattern; merged into TDesktop. |
| tv203s/contrib/tvision/classes/tbutton.cc | Steuerelement | TuiVision.Controls/TButton.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TButton; portiert nach TButton.cs. / TButton; ported to TButton.cs. |
| tv203s/contrib/tvision/classes/tchdirdi.cc | Steuerelement | TuiVision.Controls/TFileDialog.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TChDirDialog; Verzeichniswechsel-Dialog; in TFileDialog integriert. / TChDirDialog; change-directory dialog; merged into TFileDialog. |
| tv203s/contrib/tvision/classes/tcheckbo.cc | Steuerelement | TuiVision.Controls/TCheckBoxes.cs | TuiVision.Controls/TCluster.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TCheckBoxes; portiert nach TCheckBoxes.cs. / TCheckBoxes; ported to TCheckBoxes.cs. |
| tv203s/contrib/tvision/classes/tclrdisp.cc | Steuerelement | TuiVision.Controls/TColorDisplay.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TColorDisplay ist als verwaltete Farbvorschau portiert und durch Proof-Tests belegt. / TColorDisplay is ported as a managed colour preview and covered by proof tests. |
| tv203s/contrib/tvision/classes/tcluster.cc | Steuerelement | TuiVision.Controls/TCluster.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TCluster; Basis für Checkbox- und Radio-Gruppen; portiert nach TCluster.cs. / TCluster; base for checkbox and radio groups; ported to TCluster.cs. |
| tv203s/contrib/tvision/classes/tcollect.cc | Framework-Kern | TuiVision.Core/TCollection.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TCollection ist als verwaltete geordnete Sammlung portiert und per Collection-Proof-Tests belegt. / TCollection is ported as a managed ordered collection and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tcolordi.cc | Steuerelement | TuiVision.Controls/TColorDialog.cs | TuiVision.Controls/TColorSelector.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TColorDialog ist als verwalteter Farbkonfigurationsdialog portiert und koppelt Palette, Selektor und Vorschau nachweisbar. / TColorDialog is ported as a managed colour configuration dialog and demonstrably wires palette, selector, and preview together. |
| tv203s/contrib/tvision/classes/tcolorgr.cc | Steuerelement | TuiVision.Controls/TColorGroup.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TColorGroup ist als Farbgruppen-Container portiert und per Proof-Test belegt. / TColorGroup is ported as a colour-group container and covered by proof tests. |
| tv203s/contrib/tvision/classes/tcolorit.cc | Steuerelement | TuiVision.Controls/TColorItem.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TColorItem ist als benannter Farbwert portiert und automatisiert belegt. / TColorItem is ported as a named colour value and covered by automated tests. |
| tv203s/contrib/tvision/classes/tcolorse.cc | Steuerelement | TuiVision.Controls/TColorSelector.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TColorSelector ist als verwaltete Farbauswahl portiert und per Proof-Test belegt. / TColorSelector is ported as a managed colour selector and covered by proof tests. |
| tv203s/contrib/tvision/classes/tcommand.cc | Steuerelement | TuiVision.Controls/ShellCommandIds.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TCommandSet/TCommand-IDs; portiert als ShellCommandIds-Enumeration. / TCommandSet/TCommand IDs; ported as ShellCommandIds enumeration. |
| tv203s/contrib/tvision/classes/tdesktop.cc | Anwendungsrahmen | TuiVision.Controls/TDesktop.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TDesktop; portiert nach TDesktop.cs. / TDesktop; ported to TDesktop.cs. |
| tv203s/contrib/tvision/classes/tdialog.cc | Steuerelement | TuiVision.Controls/TDialog.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TDialog; portiert nach TDialog.cs. / TDialog; ported to TDialog.cs. |
| tv203s/contrib/tvision/classes/tdircoll.cc | Steuerelement | TuiVision.Controls/TDirListBox.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TDirCollection; Verzeichnissammlung; in TDirListBox zusammengeführt. / TDirCollection; directory collection; merged into TDirListBox. |
| tv203s/contrib/tvision/classes/tdirlist.cc | Steuerelement | TuiVision.Controls/TDirListBox.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TDirListBox; Verzeichnis-Listenfeld; portiert nach TDirListBox.cs. / TDirListBox; directory list box; ported to TDirListBox.cs. |
| tv203s/contrib/tvision/classes/tdisplay.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | TDisplay-Verwaltung; Kerntreiberlogik portiert nach TConsoleDriver. / TDisplay management; core driver logic ported to TConsoleDriver. |
| tv203s/contrib/tvision/classes/tdrawbuf.cc | Zeichenpuffer | TuiVision.Core/TConsoleBuffer.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | TDrawBuffer; portiert nach TConsoleBuffer. WriteText-Clipping getestet. / TDrawBuffer; ported to TConsoleBuffer. WriteText clipping tested. |
| tv203s/contrib/tvision/classes/teditor.cc | Editor | TuiVision.Controls/TEditor.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TEditor-Kern; portiert nach TEditor.cs. / TEditor core; ported to TEditor.cs. |
| tv203s/contrib/tvision/classes/teditorf.cc | Editor | TuiVision.Controls/TFileEditor.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TEditorFilter; Datei-Editor-Filterschicht; portiert nach TFileEditor.cs. / TEditorFilter; file-editor filter layer; ported to TFileEditor.cs. |
| tv203s/contrib/tvision/classes/teditwin.cc | Editor | TuiVision.Controls/TEditWindow.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TEditWindow; portiert nach TEditWindow.cs. / TEditWindow; ported to TEditWindow.cs. |
| tv203s/contrib/tvision/classes/tevent.cc | Ereignissteuerung | TuiVision.Core/TEvent.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/ | TEvent; portiert nach TEvent.cs mit statischen Fabrikmethoden. / TEvent; ported to TEvent.cs with static factory methods. |
| tv203s/contrib/tvision/classes/tfilecol.cc | Steuerelement | TuiVision.Controls/TFileList.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TFileCollection; Dateisammlung; in TFileList zusammengeführt. / TFileCollection; file collection; merged into TFileList. |
| tv203s/contrib/tvision/classes/tfiledia.cc | Steuerelement | TuiVision.Controls/TFileDialog.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TFileDialog; portiert nach TFileDialog.cs. / TFileDialog; ported to TFileDialog.cs. |
| tv203s/contrib/tvision/classes/tfileedi.cc | Editor | TuiVision.Controls/TFileEditor.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TFileEditor; portiert nach TFileEditor.cs. / TFileEditor; ported to TFileEditor.cs. |
| tv203s/contrib/tvision/classes/tfileinf.cc | Steuerelement | TuiVision.Controls/TFileInfo.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TFileInfo ist als verwaltetes Dateimetadatenmodell portiert und per Proof-Test belegt. / TFileInfo is ported as a managed file metadata model and covered by proof tests. |
| tv203s/contrib/tvision/classes/tfileinp.cc | Steuerelement | TuiVision.Controls/TFileInputLine.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TFileInputLine; portiert nach TFileInputLine.cs. / TFileInputLine; ported to TFileInputLine.cs. |
| tv203s/contrib/tvision/classes/tfilelis.cc | Steuerelement | TuiVision.Controls/TFileList.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TFileList; portiert nach TFileList.cs. / TFileList; ported to TFileList.cs. |
| tv203s/contrib/tvision/classes/tfilterv.cc | Validierung | TuiVision.Controls/TFilterValidator.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TFilterValidator ist als Zeichensatz-Validator portiert und per Proof-Test belegt. / TFilterValidator is ported as a character-set validator and covered by proof tests. |
| tv203s/contrib/tvision/classes/tframe.cc | Steuerelement | TuiVision.Controls/TGroup.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TFrame; Fensterrahmen; als Teil der TGroup-Darstellungslogik integriert. / TFrame; window frame; integrated as part of TGroup drawing logic. |
| tv203s/contrib/tvision/classes/tgkey.cc | Lokalisierung | TuiVision.Compatibility/Class1.cs | – | portiert + getestet | tests/TuiVision.Compatibility.Tests/TKeyCodeTranslatorTests.cs | Globale Tastenbelegung und Scancode-Zuordnung sind in TShiftState und TKeyCodeTranslator portiert und per Kompatibilitätstests belegt. / Global key binding and scan-code mapping are ported into TShiftState and TKeyCodeTranslator and covered by compatibility tests. |
| tv203s/contrib/tvision/classes/tgroup.cc | Steuerelement | TuiVision.Controls/TGroup.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TGroup; portiert nach TGroup.cs. / TGroup; ported to TGroup.cs. |
| tv203s/contrib/tvision/classes/thistory.cc | Steuerelement | TuiVision.Controls/THistory.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | THistory; portiert nach THistory.cs. / THistory; ported to THistory.cs. |
| tv203s/contrib/tvision/classes/thistvie.cc | Steuerelement | TuiVision.Controls/THelpViewer.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | THistoryViewer; Verlaufsansicht; portiert als Basis für THelpViewer. / THistoryViewer; history viewer; ported as base for THelpViewer. |
| tv203s/contrib/tvision/classes/thistwin.cc | Steuerelement | TuiVision.Controls/THelpWindow.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | THistoryWindow; Verlaufsfenster; portiert nach THelpWindow.cs. / THistoryWindow; history window; ported to THelpWindow.cs. |
| tv203s/contrib/tvision/classes/thwmouse.cc | Ereignissteuerung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | THardwareMouse war eine plattformspezifische Raw-Maus-Hardwareabstraktion; im verwalteten .NET-10-Konsolenmodell wird kein dedizierter Hardware-Maustreiber nachgebildet. Das UI-seitige Mausmodell bleibt in `TuiVision.Core/TEvent.cs` erhalten. / THardwareMouse was a platform-specific raw mouse hardware abstraction; the managed .NET 10 console model does not recreate a dedicated hardware mouse driver. The UI-facing mouse model remains in `TuiVision.Core/TEvent.cs`. |
| tv203s/contrib/tvision/classes/tindicat.cc | Steuerelement | TuiVision.Controls/TIndicator.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TIndicator; portiert nach TIndicator.cs. / TIndicator; ported to TIndicator.cs. |
| tv203s/contrib/tvision/classes/tinputli.cc | Steuerelement | TuiVision.Controls/TInputLine.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TInputLine; portiert nach TInputLine.cs. / TInputLine; ported to TInputLine.cs. |
| tv203s/contrib/tvision/classes/tlabel.cc | Steuerelement | TuiVision.Controls/TLabel.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TLabel; portiert nach TLabel.cs. / TLabel; ported to TLabel.cs. |
| tv203s/contrib/tvision/classes/tlistbox.cc | Steuerelement | TuiVision.Controls/TListBox.cs | TuiVision.Controls/TListViewer.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TListBox; portiert nach TListBox.cs. / TListBox; ported to TListBox.cs. |
| tv203s/contrib/tvision/classes/tlistvie.cc | Steuerelement | TuiVision.Controls/TListViewer.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TListViewer; portiert nach TListViewer.cs. / TListViewer; ported to TListViewer.cs. |
| tv203s/contrib/tvision/classes/tmemo.cc | Editor | TuiVision.Controls/TMemo.cs | TuiVision.Controls/TEditor.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TMemo; portiert nach TMemo.cs (erbt von TEditor). / TMemo; ported to TMemo.cs (inherits from TEditor). |
| tv203s/contrib/tvision/classes/tmenubar.cc | Steuerelement | TuiVision.Controls/TMenuBar.cs | TuiVision.Controls/TMenuItem.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TMenuBar; portiert nach TMenuBar.cs. / TMenuBar; ported to TMenuBar.cs. |
| tv203s/contrib/tvision/classes/tmenubox.cc | Steuerelement | TuiVision.Controls/TMenuItem.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TMenuBox; Menüfeld-Darstellung; in TMenuItem-Modell zusammengeführt. / TMenuBox; menu box display; merged into TMenuItem model. |
| tv203s/contrib/tvision/classes/tmenuvie.cc | Steuerelement | TuiVision.Controls/TMenuItem.cs | TuiVision.Controls/TMenuBar.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TMenuView; Menüansicht-Basis; in TMenuItem und TMenuBar zusammengeführt. / TMenuView; menu view base; merged into TMenuItem and TMenuBar. |
| tv203s/contrib/tvision/classes/tmonosel.cc | Steuerelement | TuiVision.Controls/TMonoSelector.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TMonoSelector ist als verwaltete Einzelauswahl portiert und per Proof-Test belegt. / TMonoSelector is ported as a managed single selector and covered by proof tests. |
| tv203s/contrib/tvision/classes/tmouse.cc | Ereignissteuerung | TuiVision.Core/TEvent.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | TMouse ist als verwaltete Maus-Zustandsabstraktion über `TMouseEvent` und `TEvent.CreateMouse` portiert; der Proof-Test belegt Buttons, Koordinaten und Doppelklickstatus. / TMouse is ported as a managed mouse-state abstraction through `TMouseEvent` and `TEvent.CreateMouse`; the proof test covers buttons, coordinates, and double-click state. |
| tv203s/contrib/tvision/classes/tnscolle.cc | Framework-Kern | TuiVision.Core/TNSCollection.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TNSCollection ist als verwaltete nicht-besitzende Sammlung portiert und per Collection-Proof-Tests belegt. / TNSCollection is ported as a managed non-owning collection and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tnssorte.cc | Framework-Kern | TuiVision.Core/TNSSorter.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TNSSorter ist als verwaltete Sortierhilfe portiert und per Collection-Proof-Tests belegt. / TNSSorter is ported as a managed sort helper and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tobject.cc | Framework-Kern | TuiVision.Core/TObject.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/ | TObject; Basisklasse aller TV-Typen; portiert nach TObject.cs. / TObject; base class for all TV types; ported to TObject.cs. |
| tv203s/contrib/tvision/classes/tpalette.cc | Steuerelement | TuiVision.Controls/TPalette.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TPalette ist als verwaltete Farbgruppensammlung portiert und per Proof-Test belegt. / TPalette is ported as a managed colour-group collection and covered by proof tests. |
| tv203s/contrib/tvision/classes/tparamte.cc | Steuerelement | TuiVision.Controls/TParamText.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TParamText ist als parametrisierbarer UI-Text portiert und per Proof-Test belegt. / TParamText is ported as parameterised UI text and covered by proof tests. |
| tv203s/contrib/tvision/classes/tpoint.cc | Framework-Kern | TuiVision.Core/TPoint.cs | TuiVision.Core/TRect.cs | portiert + getestet | tests/TuiVision.Core.Tests/ | TPoint/TRect; portiert nach TPoint.cs und TRect.cs. / TPoint/TRect; ported to TPoint.cs and TRect.cs. |
| tv203s/contrib/tvision/classes/tprogini.cc | Konfiguration | TuiVision.Controls/TIniFile.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | Die historische Programm-INI-Verwaltung ist im managed Modell als TIniFile portiert und per Proof-Test belegt. / The historical program INI handling is ported in the managed model as TIniFile and covered by proof tests. |
| tv203s/contrib/tvision/classes/tprogram.cc | Anwendungsrahmen | TuiVision.Controls/TProgram.cs | TuiVision.Controls/TApplication.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TProgram; portiert nach TProgram.cs. / TProgram; ported to TProgram.cs. |
| tv203s/contrib/tvision/classes/tpwobj.cc | Druckausgabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | TPWObj; historisches DOS-Druckpuffer-Objekt; kein Äquivalent im verwalteten Modell geplant. / TPWObj; historical DOS print-buffer object; no equivalent planned in the managed model. |
| tv203s/contrib/tvision/classes/tpwreado.cc | Druckausgabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | TPWReadObj; historischer DOS-Drucklesepuffer; kein Äquivalent im verwalteten Modell geplant. / TPWReadObj; historical DOS print-read buffer; no equivalent planned. |
| tv203s/contrib/tvision/classes/tpwritte.cc | Druckausgabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | TPWriter; historischer DOS-Druckschreibpuffer; kein Äquivalent im verwalteten Modell geplant. / TPWriter; historical DOS print-write buffer; no equivalent planned. |
| tv203s/contrib/tvision/classes/tradiobu.cc | Steuerelement | TuiVision.Controls/TRadioButtons.cs | TuiVision.Controls/TCluster.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TRadioButtons; portiert nach TRadioButtons.cs. / TRadioButtons; ported to TRadioButtons.cs. |
| tv203s/contrib/tvision/classes/trangeva.cc | Validierung | TuiVision.Controls/TRangeValidator.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TRangeValidator ist als numerischer Bereichsvalidator portiert und per Proof-Test belegt. / TRangeValidator is ported as a numeric range validator and covered by proof tests. |
| tv203s/contrib/tvision/classes/trescoll.cc | Ressourcen | TuiVision.Serialization/TResourceCollection.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | TResCollection ist nach TResourceCollection.cs portiert und durch exakte Key-/Persistenztests belegt. / TResCollection is ported to TResourceCollection.cs and covered by exact-key and persistence tests. |
| tv203s/contrib/tvision/classes/tresfile.cc | Ressourcen | TuiVision.Serialization/TResourceFile.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | TResFile ist nach TResourceFile.cs portiert und per Save/Load-/Randfalltests belegt. / TResFile is ported to TResourceFile.cs and covered by save/load and edge-case tests. |
| tv203s/contrib/tvision/classes/tscreen.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | TScreen; Bildschirmverwaltung; portiert als TConsoleDriver. Resize und Present getestet. / TScreen; screen management; ported as TConsoleDriver. Resize and Present tested. |
| tv203s/contrib/tvision/classes/tscrollb.cc | Steuerelement | TuiVision.Controls/TScrollBar.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TScrollBar; portiert nach TScrollBar.cs. / TScrollBar; ported to TScrollBar.cs. |
| tv203s/contrib/tvision/classes/tscrolle.cc | Steuerelement | TuiVision.Controls/TScroller.cs | TuiVision.Controls/TScrollBar.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TScroller; portiert nach TScroller.cs. / TScroller; ported to TScroller.cs. |
| tv203s/contrib/tvision/classes/tsortedc.cc | Framework-Kern | TuiVision.Core/TSortedCollection.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TSortedCollection ist als verwaltete sortierte Sammlung portiert und per Collection-Proof-Tests belegt. / TSortedCollection is ported as a managed sorted collection and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tsortedl.cc | Framework-Kern | TuiVision.Core/TSortedList.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TSortedList ist als verwaltete sortierte Schlüssel-Wert-Liste portiert und per Collection-Proof-Tests belegt. / TSortedList is ported as a managed sorted key-value list and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tstatict.cc | Steuerelement | TuiVision.Controls/TStaticText.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TStaticText; portiert nach TStaticText.cs. / TStaticText; ported to TStaticText.cs. |
| tv203s/contrib/tvision/classes/tstatusd.cc | Steuerelement | TuiVision.Controls/TStatusLine.cs | TuiVision.Controls/TStatusItem.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TStatusDef; Statuszeilen-Definition; in TStatusLine und TStatusItem zusammengeführt. / TStatusDef; status line definition; merged into TStatusLine and TStatusItem. |
| tv203s/contrib/tvision/classes/tstatusl.cc | Steuerelement | TuiVision.Controls/TStatusLine.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TStatusLine; portiert nach TStatusLine.cs. / TStatusLine; ported to TStatusLine.cs. |
| tv203s/contrib/tvision/classes/tstrinde.cc | Framework-Kern | TuiVision.Core/TStringIndex.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TStringIndex ist als case-sensitiver Zeichenkettenindex portiert und per Collection-Proof-Tests belegt. / TStringIndex is ported as a case-sensitive string index and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tstringc.cc | Framework-Kern | TuiVision.Core/TStringCollection.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TStringCollection ist als sortierte eindeutige Zeichenkettensammlung portiert und per Collection-Proof-Tests belegt. / TStringCollection is ported as a sorted unique string collection and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tstringl.cc | Steuerelement | TuiVision.Controls/TStringList.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TStringList; portiert nach TStringList.cs. / TStringList; ported to TStringList.cs. |
| tv203s/contrib/tvision/classes/tstrlist.cc | Framework-Kern | TuiVision.Core/TStrList.cs | – | portiert + getestet | tests/TuiVision.Core.Tests/CollectionProofTests.cs | TStrList ist als Einfügereihenfolge-Zeichenkettenliste portiert und per Collection-Proof-Tests belegt. / TStrList is ported as an insertion-order string list and covered by collection proof tests. |
| tv203s/contrib/tvision/classes/tstrmcla.cc | Serialisierung | TuiVision.Serialization/TRecordRegistry.cs | – | portiert + getestet | tests/TuiVision.Serialization.Tests/ | TStreamClass ist in TRecordRegistry portiert und durch Registrierungs-/Kompatibilitätstests belegt. / TStreamClass is ported into TRecordRegistry and covered by registration and compatibility tests. |
| tv203s/contrib/tvision/classes/tstrmtyp.cc | Serialisierung | TuiVision.Serialization/TRecordRegistry.cs | TuiVision.Serialization/TRecordSerializer.cs | portiert + getestet | tests/TuiVision.Serialization.Tests/ | TStreamTypes sind in TRecordRegistry und TRecordSerializer portiert und per Serialization-Suite belegt. / TStreamTypes are ported into TRecordRegistry and TRecordSerializer and covered by the serialization suite. |
| tv203s/contrib/tvision/classes/tsubmenu.cc | Steuerelement | TuiVision.Controls/TMenuBar.cs | TuiVision.Controls/TMenuItem.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ | TSubMenu; Untermenü-Steuerelement; in TMenuBar und TMenuItem integriert. / TSubMenu; submenu control; integrated into TMenuBar and TMenuItem. |
| tv203s/contrib/tvision/classes/ttermina.cc | Terminal | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | TTerminal; historischer DOS-Terminalemulator; kein Äquivalent im verwalteten Modell geplant; Console-API ersetzt die Funktion. / TTerminal; historical DOS terminal emulator; no equivalent planned; Console API replaces the function. |
| tv203s/contrib/tvision/classes/ttextdev.cc | Terminal | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | TTextDevice; Textgeräte-Abstraktion (historisch DOS); durch System.Console ersetzt. / TTextDevice; text device abstraction (historically DOS); replaced by System.Console. |
| tv203s/contrib/tvision/classes/tvalidat.cc | Validierung | TuiVision.Controls/TValidator.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TValidator ist als verwaltete Basisklasse für Validierung portiert und durch Proof-Tests belegt. / TValidator is ported as the managed base class for validation and covered by proof tests. |
| tv203s/contrib/tvision/classes/tvedit1.cc | Editor | TuiVision.Controls/TEditor.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TEditor-Teil 1 (Textoperationen); in TEditor.cs zusammengeführt. / TEditor part 1 (text operations); merged into TEditor.cs. |
| tv203s/contrib/tvision/classes/tvedit2.cc | Editor | TuiVision.Controls/TEditor.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TEditor-Teil 2 (Such/Ersatz); in TEditor.cs zusammengeführt. / TEditor part 2 (search/replace); merged into TEditor.cs. |
| tv203s/contrib/tvision/classes/tvedit3.cc | Editor | TuiVision.Controls/TEditor.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TEditor-Teil 3 (Datei/Befehl); in TEditor.cs zusammengeführt. / TEditor part 3 (file/command); merged into TEditor.cs. |
| tv203s/contrib/tvision/classes/tview.cc | Steuerelement | TuiVision.Controls/TView.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ | TView; Basis aller sichtbaren Elemente; portiert nach TView.cs. / TView; base of all visible elements; ported to TView.cs. |
| tv203s/contrib/tvision/classes/tvintl.cc | Lokalisierung | TuiVision.Compatibility/Class1.cs | – | portiert + getestet | tests/TuiVision.Compatibility.Tests/TKeyCodeTranslatorTests.cs | Die TV-Internationalisierung der Tasten- und Modifier-Semantik ist in TShiftState und TKeyCodeTranslator portiert und durch Kompatibilitätstests belegt. / The TV internationalisation of key and modifier semantics is ported into TShiftState and TKeyCodeTranslator and covered by compatibility tests. |
| tv203s/contrib/tvision/classes/tvtext1.cc | Lokalisierung | TuiVision.Controls/TvUIStrings.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TV-UI-Zeichenketten Teil 1 ist in TvUIStrings portiert und per Proof-Test belegt. / TV UI strings part 1 is ported into TvUIStrings and covered by proof tests. |
| tv203s/contrib/tvision/classes/tvtext2.cc | Lokalisierung | TuiVision.Controls/TvUIStrings.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TV-UI-Zeichenketten Teil 2 ist in TvUIStrings portiert und per Proof-Test belegt. / TV UI strings part 2 is ported into TvUIStrings and covered by proof tests. |
| tv203s/contrib/tvision/classes/twindow.cc | Steuerelement | TuiVision.Controls/TWindow.cs | TuiVision.Controls/TGroup.cs | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | TWindow ist als Fenster-Container auf Basis von TGroup portiert und per Proof-Test belegt. / TWindow is ported as a window container on top of TGroup and covered by proof tests. |

---

## DOS-Plattformdateien / DOS Platform Files

Alle DOS-Treiberdateien werden **bewusst ausgelassen**, da DOS-native Bildschirm-, Tastatur- und Mausschnittstellen keine Entsprechung im verwalteten .NET-10-Laufzeitmodell haben.

*(All DOS driver files are consciously omitted: DOS-native screen, keyboard, and mouse interfaces have no equivalent in the managed .NET 10 runtime model.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/dos/dosdis.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | DOS-native Bildschirmausgabe über BIOS-Interrupts und direkte Videospeicherschreibung. Kein Äquivalent im verwalteten Modell. / DOS-native screen output via BIOS interrupts and direct video memory writes. No equivalent in the managed model. |
| tv203s/contrib/tvision/classes/dos/doskey.cc | Tastatureingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | DOS-native Tastaturabfrage über BIOS INT 16h. Durch System.Console.ReadKey() ersetzt. / DOS-native keyboard polling via BIOS INT 16h. Replaced by System.Console.ReadKey(). |
| tv203s/contrib/tvision/classes/dos/dosmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | DOS-native Maussteuerung über INT 33h (Microsoft Mouse Driver). Kein verwaltetes Äquivalent. / DOS-native mouse control via INT 33h (Microsoft Mouse Driver). No managed equivalent. |
| tv203s/contrib/tvision/classes/dos/dosscr.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | DOS-Bildschirmseiten-Verwaltung über direkte Videospeicheroperationen. Kein verwaltetes Äquivalent. / DOS screen-page management via direct video memory operations. No managed equivalent. |
| tv203s/contrib/tvision/classes/dos/rhscreen.cc | Terminalmodus | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | REGEN-basierter Bildschirmzugriff (DOS-spezifisch). Kein verwaltetes Äquivalent. / REGEN-based screen access (DOS-specific). No managed equivalent. |
| tv203s/contrib/tvision/classes/dos/screen.cc | Terminalmodus | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | DOS-Bildschirm-Initialisierung und Terminalmodus-Einstellung. Abhängig von vgastate.h/vgaregs.h (native Registerzugriffe). Kein verwaltetes Äquivalent. / DOS screen initialisation and terminal mode setup. Depends on vgastate.h/vgaregs.h (native register access). No managed equivalent. Ancillary: dos/vgastate.h, dos/vgastate.c, dos/vgaregs.h, dos/vgaregs.c. |
| tv203s/contrib/tvision/classes/dos/sescreen.cc | Terminalmodus | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | ANSI-Terminalemulation für DOS. Kein verwaltetes Äquivalent; moderne Terminals unterstützen ANSI nativ. / ANSI terminal emulation for DOS. No managed equivalent; modern terminals support ANSI natively. |
| tv203s/contrib/tvision/classes/dos/vesa.cc | Anzeigeadaption | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | VESA-Grafikmodus-Treiber (DOS). Nicht anwendbar auf verwaltete Konsolenmodelle. Abhängig von vgastate.h/vgaregs.h. / VESA graphics mode driver (DOS). Not applicable to managed console models. Depends on vgastate.h/vgaregs.h. Ancillary: dos/vgastate.h, dos/vgastate.c, dos/vgaregs.h, dos/vgaregs.c. |
| tv203s/contrib/tvision/classes/dos/vga.cc | Anzeigeadaption | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | VGA-Hardware-Treiber (DOS). Nicht anwendbar. Abhängig von vgastate.h/vgaregs.h für Register-Layout-Daten. / VGA hardware driver (DOS). Not applicable. Depends on vgastate.h/vgaregs.h for register layout data. Ancillary: dos/vgastate.h, dos/vgastate.c, dos/vgaregs.h, dos/vgaregs.c. |

**Ancillary support files referenced above / Referenzierte Hilfsdateien:**
- `dos/vgastate.h` / `dos/vgastate.c`: VGA-Zustandsstrukturen und Speicherlayout; beeinflusst screen.cc, vesa.cc und vga.cc.
- `dos/vgaregs.h` / `dos/vgaregs.c`: VGA-Registeradressen und Konstanten; beeinflusst screen.cc, vesa.cc und vga.cc.

---

## Linux-Plattformdateien / Linux Platform Files

Linux-Treiberdateien werden **portiert** — die Fähigkeiten werden durch die verwaltete `System.Console`-API von .NET 10 bereitgestellt, die auf Linux transparent funktioniert.

*(Linux display and keyboard capabilities are ported through the managed .NET 10 console stack; the historical raw mouse driver is consciously omitted because no dedicated cross-platform managed mouse driver exists.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/linux/linuxdis.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Linux-Terminal-Anzeigetreiber; Linux-spezifische ioctl-Aufrufe durch verwaltete Console-API ersetzt. TConsoleDriver.Present() auf Linux verifiziert. / Linux terminal display driver; Linux-specific ioctl calls replaced by managed Console API. TConsoleDriver.Present() verified on Linux. |
| tv203s/contrib/tvision/classes/linux/linuxkey.cc | Tastatureingabe | TuiVision.Drivers.Console/TConsoleDriver.cs | TuiVision.Compatibility/TConsoleInputAdapter.cs | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | Linux-Tastatur-Rohmodus wird im managed Modell durch `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator` ersetzt; der Driver-Proof-Test belegt die Event-Übersetzung. / Linux raw keyboard mode is replaced in the managed model by `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator`; the driver proof test covers the event translation. |
| tv203s/contrib/tvision/classes/linux/linuxmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | Linux-Raw-Mauseingabe (z. B. GPM/ioctl-basierte Treiber) wird im verwalteten Konsolenmodell nicht als eigener Treiber nachgebildet; nur das UI-seitige Mausereignismodell bleibt erhalten. / Linux raw mouse input (for example GPM/ioctl-based drivers) is not recreated as a dedicated driver in the managed console model; only the UI-facing mouse event model remains. |
| tv203s/contrib/tvision/classes/linux/linuxscr.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Linux-Bildschirm-Initialisierung; durch TConsoleDriver.Resize() und TConsoleBuffer-Initialisierung ersetzt. / Linux screen initialisation; replaced by TConsoleDriver.Resize() and TConsoleBuffer initialisation. |

---

## QNX4-Plattformdateien / QNX4 Platform Files

Alle QNX4-Treiberdateien werden **bewusst ausgelassen** — QNX 4 ist ein veraltetes RTOS ohne verwaltetes .NET-Laufzeit-Äquivalent.

*(All QNX4 driver files are consciously omitted — QNX 4 is an obsolete RTOS with no managed .NET runtime equivalent.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/qnx4/qnx4dis.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX 4 Anzeige-Treiber. Veraltetes RTOS, nicht im Zielbereich der verwalteten .NET-Laufzeit. / QNX 4 display driver. Obsolete RTOS, outside the managed .NET runtime target scope. |
| tv203s/contrib/tvision/classes/qnx4/qnx4key.cc | Tastatureingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX 4 Tastatur-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX 4 keyboard driver. Obsolete RTOS, outside target scope. |
| tv203s/contrib/tvision/classes/qnx4/qnx4mouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX 4 Maus-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX 4 mouse driver. Obsolete RTOS, outside target scope. |
| tv203s/contrib/tvision/classes/qnx4/qnx4scr.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX 4 Bildschirm-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX 4 screen driver. Obsolete RTOS, outside target scope. |

---

## QNXrtp-Plattformdateien / QNXrtp Platform Files

Alle QNXrtp-Treiberdateien werden **bewusst ausgelassen** — QNX RTP ist ein veraltetes RTOS ohne verwaltetes .NET-Laufzeit-Äquivalent.

*(All QNXrtp driver files are consciously omitted — QNX RTP is an obsolete RTOS with no managed .NET runtime equivalent.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/qnxrtp/qnxdis.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX RTP Anzeige-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX RTP display driver. Obsolete RTOS, outside target scope. |
| tv203s/contrib/tvision/classes/qnxrtp/qnxkey.cc | Tastatureingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX RTP Tastatur-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX RTP keyboard driver. Obsolete RTOS, outside target scope. |
| tv203s/contrib/tvision/classes/qnxrtp/qnxmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX RTP Maus-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX RTP mouse driver. Obsolete RTOS, outside target scope. |
| tv203s/contrib/tvision/classes/qnxrtp/qnxscr.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | QNX RTP Bildschirm-Treiber. Veraltetes RTOS, nicht im Zielbereich. / QNX RTP screen driver. Obsolete RTOS, outside target scope. |

---

## Unix/Xterm-Plattformdateien / Unix/Xterm Platform Files

Unix- und Xterm-Treiberdateien werden **portiert** — POSIX-Terminalfähigkeiten sind durch die verwaltete `System.Console`-API von .NET 10 abgedeckt.

*(Unix and Xterm display/keyboard capabilities are ported through the managed .NET 10 console stack; the historical raw mouse protocols are consciously omitted because no dedicated managed mouse-driver layer exists.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/unix/unixdis.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Unix-Terminal-Anzeige; POSIX-Terminalausgabe durch verwaltete Console-API ersetzt. TConsoleDriver.Present() auf macOS/Linux verifiziert. / Unix terminal display; POSIX terminal output replaced by managed Console API. TConsoleDriver.Present() verified on macOS/Linux. |
| tv203s/contrib/tvision/classes/unix/unixkey.cc | Tastatureingabe | TuiVision.Drivers.Console/TConsoleDriver.cs | TuiVision.Compatibility/TConsoleInputAdapter.cs | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | Unix-Tastatur wird im managed Modell durch `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator` ersetzt; der Driver-Proof-Test belegt die TV-Ereignisübersetzung. / Unix keyboard input is replaced in the managed model by `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator`; the driver proof test covers the TV event translation. |
| tv203s/contrib/tvision/classes/unix/unixmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | Unix-Raw-Mauseingabe hatte keine stabile verwaltete Konsolenentsprechung; Phase 8 bewahrt nur das Mausereignismodell in `TuiVision.Core/TEvent.cs`, nicht aber einen plattformspezifischen Eingabetreiber. / Unix raw mouse input had no stable managed console equivalent; Phase 8 preserves only the mouse event model in `TuiVision.Core/TEvent.cs`, not a platform-specific input driver. |
| tv203s/contrib/tvision/classes/unix/unixscr.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Unix-Bildschirm-Initialisierung; durch TConsoleDriver.Resize() und TConsoleBuffer ersetzt. / Unix screen initialisation; replaced by TConsoleDriver.Resize() and TConsoleBuffer. |
| tv203s/contrib/tvision/classes/unix/xtermdis.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Xterm-Anzeige; durch verwaltete Console-API ersetzt. macOS-Terminals (iTerm2, Terminal.app) sind xterm-kompatibel. / Xterm display; replaced by managed Console API. macOS terminals (iTerm2, Terminal.app) are xterm-compatible. |
| tv203s/contrib/tvision/classes/unix/xtermkey.cc | Tastatureingabe | TuiVision.Drivers.Console/TConsoleDriver.cs | TuiVision.Compatibility/TConsoleInputAdapter.cs | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | Xterm-Tastatursequenzen werden im managed Modell über `System.Console.ReadKey()` und die explizit xterm-kompatible Tastenmenge des `TConsoleInputAdapter` ersetzt; der Proof-Test deckt Navigations- und Funktionstasten ab. / Xterm key sequences are replaced in the managed model via `System.Console.ReadKey()` and the explicit xterm-compatible key subset in `TConsoleInputAdapter`; the proof test covers navigation and function keys. |
| tv203s/contrib/tvision/classes/unix/xtermmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | Das historische Xterm-Mausprotokoll wird im verwalteten Konsolenmodell nicht als eigener Protokolltreiber nachgebildet; nur das allgemeine Mausereignismodell bleibt erhalten. / The historical Xterm mouse protocol is not recreated as a dedicated protocol driver in the managed console model; only the general mouse event model remains. |
| tv203s/contrib/tvision/classes/unix/xtermscr.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Xterm-Bildschirm; durch TConsoleDriver und TConsoleBuffer ersetzt. / Xterm screen; replaced by TConsoleDriver and TConsoleBuffer. |

---

## Win32-Plattformdateien / Win32 Platform Files

Win32-Treiberdateien werden **portiert** — Win32-Console-API-Fähigkeiten sind durch die verwaltete `System.Console`-API von .NET 10 auf Windows abgedeckt.

*(Win32 display and keyboard capabilities are ported through the managed .NET 10 console stack on Windows; the historical raw mouse driver is consciously omitted because no dedicated managed mouse-driver layer exists.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/win32/win32clip.cc | OS-Clipboard | TuiVision.Controls/ManagedClipboard.cs | – | portiert + getestet | tests/TuiVision.Controls.Tests/ControlsProofTests.cs | Die Win32-Zwischenablage ist im managed Modell durch ManagedClipboard ersetzt und per Proof-Test belegt. / The Win32 clipboard is replaced in the managed model by ManagedClipboard and covered by proof tests. |
| tv203s/contrib/tvision/classes/win32/win32dis.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Win32-Console-Anzeige; durch verwaltete Console-API ersetzt. TConsoleDriver.Present() auf Windows/WSL verifiziert (manueller Lauf). / Win32 console display; replaced by managed Console API. TConsoleDriver.Present() verified on Windows/WSL (manual run). |
| tv203s/contrib/tvision/classes/win32/win32key.cc | Tastatureingabe | TuiVision.Drivers.Console/TConsoleDriver.cs | TuiVision.Compatibility/TConsoleInputAdapter.cs | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | Win32-Tastatureingabe wird im managed Modell durch `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator` ersetzt; der Driver-Proof-Test belegt die Turbo-Vision-Ereignisabbildung. / Win32 keyboard input is replaced in the managed model by `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator`; the driver proof test covers the Turbo Vision event mapping. |
| tv203s/contrib/tvision/classes/win32/win32mouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | Win32-Raw-Mauseingabe über Console-Input-Records wird in Phase 8 nicht als eigener Treiber nachgebildet; stattdessen bleibt nur das UI-seitige Mausereignismodell erhalten. / Win32 raw mouse input through console input records is not recreated as a dedicated driver in Phase 8; instead only the UI-facing mouse event model remains. |
| tv203s/contrib/tvision/classes/win32/win32scr.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | Win32-Bildschirm-Initialisierung; durch TConsoleDriver.Resize() und TConsoleBuffer ersetzt. / Win32 screen initialisation; replaced by TConsoleDriver.Resize() and TConsoleBuffer. |

---

## WinGR-Plattformdateien / WinGR Platform Files

Alle WinGR-Treiberdateien werden **bewusst ausgelassen** — WinGR war ein Win16/GDI-basierter grafischer Treiber ohne verwaltetes Konsolenäquivalent.

*(All WinGR driver files are consciously omitted — WinGR was a Win16/GDI-based graphical driver with no managed console equivalent.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/wingr/wingrdis.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | WinGR GDI-basierter Anzeige-Treiber (Win16). Kein Äquivalent im verwalteten Konsolenmodell. / WinGR GDI-based display driver (Win16). No equivalent in the managed console model. |
| tv203s/contrib/tvision/classes/wingr/wingrkey.cc | Tastatureingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | WinGR GDI-basierte Tastatureingabe (Win16). Kein Äquivalent. / WinGR GDI-based keyboard input (Win16). No equivalent. |
| tv203s/contrib/tvision/classes/wingr/wingrmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | WinGR GDI-basierte Mauseingabe (Win16). Kein Äquivalent. / WinGR GDI-based mouse input (Win16). No equivalent. |
| tv203s/contrib/tvision/classes/wingr/wingrscr.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | WinGR GDI-basierter Bildschirm-Treiber (Win16). Kein Äquivalent. / WinGR GDI-based screen driver (Win16). No equivalent. |

---

## WinNT-Plattformdateien / WinNT Platform Files

WinNT-Treiberdateien werden für Darstellung und Tastatur **portiert**; der historische Raw-Maus-Treiber wird bewusst ausgelassen, weil kein dedizierter verwalteter Maus-Treiberstack existiert.

*(WinNT display and keyboard capabilities are ported through the managed .NET 10 console stack; the historical raw mouse driver is consciously omitted because no dedicated managed mouse-driver layer exists.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/winnt/winntdis.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | WinNT-Console-Anzeige; durch verwaltete Console-API ersetzt. TConsoleDriver.Present() auf Windows/WSL verifiziert (manueller Lauf). / WinNT console display; replaced by managed Console API. TConsoleDriver.Present() verified on Windows/WSL (manual run). |
| tv203s/contrib/tvision/classes/winnt/winntkey.cc | Tastatureingabe | TuiVision.Drivers.Console/TConsoleDriver.cs | TuiVision.Compatibility/TConsoleInputAdapter.cs | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs | WinNT-Tastatureingabe wird im managed Modell durch `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator` ersetzt; der Driver-Proof-Test belegt die TV-Ereignisübersetzung. / WinNT keyboard input is replaced in the managed model by `System.Console.ReadKey()` plus `TConsoleInputAdapter`/`TKeyCodeTranslator`; the driver proof test covers the TV event translation. |
| tv203s/contrib/tvision/classes/winnt/winntmouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | WinNT-Raw-Mauseingabe wird im verwalteten Konsolenmodell nicht als eigener Treiber nachgebildet; nur das allgemeine Mausereignismodell bleibt erhalten. / WinNT raw mouse input is not recreated as a dedicated driver in the managed console model; only the general mouse event model remains. |
| tv203s/contrib/tvision/classes/winnt/winntscr.cc | Darstellung | TuiVision.Drivers.Console/TConsoleDriver.cs | – | portiert + getestet | tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs | WinNT-Bildschirm-Initialisierung; durch TConsoleDriver.Resize() und TConsoleBuffer ersetzt. / WinNT screen initialisation; replaced by TConsoleDriver.Resize() and TConsoleBuffer. |

---

## X11-Plattformdateien / X11 Platform Files

Alle X11-Treiberdateien werden **bewusst ausgelassen** — X11 ist eine Fenstermanager-Schicht ohne verwaltetes Konsolenäquivalent.

*(All X11 driver files are consciously omitted — X11 is a window manager layer with no managed console equivalent.)*

| Quelldatei | Fähigkeitsgruppe | Primärziel | Sekundärziel(e) | Status | Nachweis | Begründung / Anmerkung |
|---|---|---|---|---|---|---|
| tv203s/contrib/tvision/classes/x11/x11dis.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | X11-Anzeige-Treiber (X Window System). Grafischer Fenstermanager ohne verwaltetes Konsolenäquivalent. / X11 display driver (X Window System). Graphical window manager with no managed console equivalent. |
| tv203s/contrib/tvision/classes/x11/x11key.cc | Tastatureingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | X11-Tastatureingabe. Kein Äquivalent im verwalteten Konsolenmodell. / X11 keyboard input. No equivalent in the managed console model. |
| tv203s/contrib/tvision/classes/x11/x11mouse.cc | Mauseingabe | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | X11-Mauseingabe. Kein Äquivalent im verwalteten Konsolenmodell. / X11 mouse input. No equivalent in the managed console model. |
| tv203s/contrib/tvision/classes/x11/x11src.cc | Darstellung | bewusst ausgelassen | – | bewusst ausgelassen + Begruendung | – | X11-Bildschirm-Quellcode (Initialisierung und Ressourcen). Kein Äquivalent im verwalteten Konsolenmodell. / X11 screen source (initialisation and resources). No equivalent in the managed console model. |

---

## Kompatibilitätsnachweise / Compatibility Evidence

### Phase-7-Primärnachweis (Multi-Mac)

| Umgebung | Modus | Ergebnis | Anmerkung |
|---|---|---|---|
| MacBook Air M2 | lokal / local | `dotnet test tests/TuiVision.Drivers.Tests/` — PASS | Primäre Entwicklungsumgebung. / Primary development environment. |
| Mac mini M4 Pro | lokal / local | `dotnet test tests/TuiVision.Drivers.Tests/` — PASS | Sekundäre Entwicklungsumgebung. / Secondary development environment. |

### Plattformkompatibilitätsnachweise (Phase-7-Increment)

| Umgebung | Modus | Ergebnis | Anmerkung |
|---|---|---|---|
| Linux (Ubuntu 24.04 / WSL) | manuell / manual | `dotnet build --configuration Release && dotnet test tests/TuiVision.Drivers.Tests/` — PASS (manueller Lauf auf WSL Ubuntu 24.04) | Manuell ausgeführt; noch kein CI-Gate. / Manually executed; not yet a CI gate. |
| Windows/WSL | manuell / manual | `dotnet build --configuration Release && dotnet test tests/TuiVision.Drivers.Tests/` — PASS (manueller Lauf auf WSL Ubuntu 24.04 unter Windows) | Manuell ausgeführt; noch kein CI-Gate. / Manually executed; not yet a CI gate. |

Vollständige Ausführungsdetails: siehe `docs/guides/multi-mac-workflow.md`, Abschnitt „Phase-7-Kompatibilitätsnachweis".

---

## Phase-8-Eingangstor-Nachweis / Phase-8 Entrance Gate Evidence

Die Phase-8-Eingangsvoraussetzungen sind im Closure-Paket vom `2026-03-27`
vollständig nachgewiesen. Welle 1 der Beispielportierungen darf ab dem
dedizierten Closure-Commit `docs: close phase-8 entrance gate for feature 006`
beginnen.

*(The Phase-8 entrance requirements are fully evidenced in the 2026-03-27
closure package. Example wave 1 may begin starting with the dedicated closure
commit `docs: close phase-8 entrance gate for feature 006`.)*

| Gate-Bereich | Ergebnis 2026-03-27 | Nachweis / Anmerkung |
|---|---|---|
| Build-Gate: `dotnet build --configuration Release` für alle Module | PASS | Erfolgreich ohne verbleibende Warnungen aus dem Closure-Paket. |
| Test-Gate: `dotnet test` (alle Module) | PASS | Alle Repository-Testprojekte grün; keine dokumentationspflichtigen Skip-/Ignore-Fälle. |
| Format-Gate: `dotnet format --verify-no-changes` | PASS | Nach MSTest-Analyzer-Bereinigung ohne Abweichungen. |
| Coverage-Gate: `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` jeweils ≥ 70 % Line Coverage | PASS | Alle fünf Gate-Assemblies separat mit Coverlet nachgewiesen; Details siehe Tabelle unten. |
| API-Doku-Gate: XML-Kommentare + `docfx docfx.json` | PASS | Bedingter Doku-Gate-Lauf erfolgreich, da öffentliche API/XML-Kommentare im 006-Paket geändert wurden. |
| Vollständiger M-07-Beweis: alle `.cc`-Zeilen mit `portiert + getestet` oder `bewusst ausgelassen + Begruendung` | PASS | Keine Ledger-Zeile verbleibt im provisorischen Status. |
| Beispielportierungen Wellen 1–4 | FREIGEGEBEN | Phase 8 darf ab dem dedizierten Closure-Commit beginnen; Welle 1 ist der nächste offene Hauptschritt. |

### Coverage-Ergebnisse je Gate-Assembly / Coverage Results per Gate Assembly

| Ziel-Assembly | Testprojekt / Evidence Source | Line Coverage | Status |
|---|---|---:|---|
| `TuiVision.Core` | `dotnet test tests/TuiVision.Core.Tests/ --collect:"XPlat Code Coverage"` | `89.11 %` | PASS |
| `TuiVision.Controls` | `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"` | `84.10 %` | PASS |
| `TuiVision.Serialization` | `dotnet test tests/TuiVision.Serialization.Tests/ --collect:"XPlat Code Coverage"` | `83.33 %` | PASS |
| `TuiVision.Compatibility` | `dotnet test tests/TuiVision.Compatibility.Tests/ --collect:"XPlat Code Coverage"` | `80.95 %` | PASS |
| `TuiVision.Drivers.Console` | `dotnet test tests/TuiVision.Drivers.Tests/ --collect:"XPlat Code Coverage"` | `97.43 %` | PASS |

Die lokalen Coverlet-Läufe vom `2026-03-27` sind für das Eingangstor das
maßgebliche Ergebnis, weil im Repository derzeit kein separates,
assembly-scharfes CI-Artefakt mit abweichenden Werten vorliegt.

*(The local 2026-03-27 Coverlet runs are the authoritative entrance-gate
result because the repository currently contains no separate assembly-specific
CI artifact with conflicting values.)*

### Skip-/Ignore-Prüfung / Skip and Ignore Review

- `dotnet test` meldet am `2026-03-27` keine `[Ignore]`- oder `Skip`-Fälle in
  gate-relevanten Testprojekten.
- Vorhandene `[DoNotParallelize]`-Attribute in einzelnen Controls-Tests sind
  Ausführungssteuerung, keine ausgelassenen Tests.

### Closure-Commit-Referenz / Closure Commit Reference

- Dedizierter Gate-Closure-Commit: `docs: close phase-8 entrance gate for feature 006`
