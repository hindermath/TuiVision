# Architekturrisiken Welle 2 / Architecture Risks Wave 2

## AcceptedLimitations

- **ExampleName**: `clipboard`
- **HistoricalBehavior**: Das Original nutzt OS-spezifische Primaer- und Sekundaer-Clipboards.
- **Reduction**: Die verwaltete Portierung zeigt Copy, Cut und Paste ueber `ManagedClipboard` plus einen sichtbaren isolierten/unverfuegbaren Zustand.
- **Rationale**: OS-Clipboard-Anbindung ist host-sensitiv und nicht erforderlich, um die Controls-Integration in Welle 2 deterministisch zu pruefen.
- **AcceptanceImpact**: Keine Blockade; Clipboard-Interaktionen und Fallback sind sichtbar nachgewiesen.
- **EarliestFollowUpPoint**: Nach Abschluss der Pflichtwellen 1-4.
- **TraceableReference**: `docs/guides/examples/clipboard.md`

- **ExampleName**: `demo`
- **HistoricalBehavior**: Das Original enthaelt breite Demo-Anteile fuer Editor, Hilfe, Streams, Terminaleffekte, Mausdialoge und Zeichensatzdetails.
- **Reduction**: Welle 2 zaehlt nur Controls, Standarddialoge, Farb-/Display-Auswahl und Gadgets; ausgeschlossene Bereiche werden dokumentiert.
- **Rationale**: Editor, Hilfe, Streams, Terminalemulation, Runtime-Maus und reale Charset-Effekte sind spaeteren Wellen zugeordnet.
- **AcceptanceImpact**: Keine Blockade; die Breitenintegration bleibt auf Controls/Dialoge begrenzt.
- **EarliestFollowUpPoint**: Welle 3 fuer Editor/Hilfe/Streams, Welle 4 fuer Terminal/Charset/Runtime-Maus.
- **TraceableReference**: `docs/guides/examples/demo.md`

## 012 Interactive Showcase Risk Review

- **Risk**: Schein-Interaktion durch direkte Hilfsmethoden statt Runtime-Pfad.
- **Mitigation**: Primaere Wave-2-Smokes nutzen `QueueEvents(...)`,
  `app.Run()` und sichtbare `VisibleHistory`-Assertions; direkte Hilfen sind
  nur als `SetupOnly` oder `SupplementalAssertion` klassifiziert.
- **ResidualRisk**: Niedrig; normale Starts zeigen Menue-/Befehlspfade, und
  die Smoke-Matrix listet alle elf App-Loop-Szenarien.

- **Risk**: Datei-/Fixture-Beispiele koennten Nutzerdaten lesen oder
  persistente History schreiben.
- **Mitigation**: `Demo` prueft Metadaten ohne Dateiinhalt-I/O, `DlgDsn`
  erlaubt nur source-controlled Fixture-Namen, und `InpLis` speichert History
  nur im Speicher.
- **ResidualRisk**: Niedrig; keine neue externe Trust-Boundary.

- **ExampleName**: `dlgdsn`
- **HistoricalBehavior**: Das Original enthaelt einen umfangreichen Designer mit Property-Editoren und Code-Builder-Anteilen.
- **Reduction**: Die Portierung beweist strukturierte Beschreibung laden/erzeugen, rendern, einfach aendern und fehlerhafte Beschreibungen ablehnen.
- **Rationale**: Vollstaendige Designer-Paritaet wuerde ueber die Dialog-Readiness von Welle 2 hinausgehen.
- **AcceptanceImpact**: Keine Blockade; die dynamische Dialogakzeptanz ist vollstaendig abgedeckt.
- **EarliestFollowUpPoint**: Nach Abschluss der Pflichtwellen 1-4.
- **TraceableReference**: `docs/guides/examples/dlgdsn.md`

- **ExampleName**: `progba`
- **HistoricalBehavior**: Das Original nutzt modellose Dialoge, Busy-State-Anzeige, Pausen und Host-spezifische Codepage-Zeichen.
- **Reduction**: Die Portierung beweist deterministische Fortschrittsfertigstellung ohne Wall-Clock-Abhaengigkeit.
- **Rationale**: Timing und Host-Codepages sind fuer reproduzierbare Smoke-Tests nicht stabil genug.
- **AcceptanceImpact**: Keine Blockade; Abschlusszustand und sichtbarer Fortschritt sind beweisbar.
- **EarliestFollowUpPoint**: Nach Abschluss der Pflichtwellen 1-4.
- **TraceableReference**: `docs/guides/examples/progba.md`

- **ExampleName**: `tprogb`
- **HistoricalBehavior**: Das Original koppelt Fortschritt an eine laenger laufende Berechnung und interaktiven Abbruch.
- **Reduction**: Die Portierung zeigt deterministischen Fortschritt und einen sichtbaren Abbruchzustand ohne unkontrollierte Laufzeit.
- **Rationale**: Smoke-Tests duerfen nicht von CPU-Geschwindigkeit oder Schlafzeiten abhaengen.
- **AcceptanceImpact**: Keine Blockade; Fortschritt und Abbruch sind sichtbar getrennt.
- **EarliestFollowUpPoint**: Nach Abschluss der Pflichtwellen 1-4.
- **TraceableReference**: `docs/guides/examples/tprogb.md`

## HistoricalExampleParityCleanup

- **AffectedExample**: `sdlg`
- **DeferredBehavior**: Vollstaendige historische Dialogoptik, Grow-Flags und Hintergrundverhalten ausserhalb des ScrollGroup-Kerns.
- **Rationale**: Welle 2 akzeptiert `sdlg` fuer den historischen vertikalen ScrollDialog/ScrollGroup-Zweck.
- **EarliestSchedulingPoint**: Nach Abschluss der Pflichtwellen 1-4.
- **TraceableReference**: `docs/guides/examples/sdlg.md`

- **AffectedExample**: `sdlg2`
- **DeferredBehavior**: Vollstaendige historische Dialogoptik, Grow-Flags und Hintergrundverhalten ausserhalb des horizontal/vertikal scrollbaren ScrollGroup-Kerns.
- **Rationale**: Welle 2 akzeptiert `sdlg2` fuer den historischen horizontalen und vertikalen ScrollDialog/ScrollGroup-Zweck.
- **EarliestSchedulingPoint**: Nach Abschluss der Pflichtwellen 1-4.
- **TraceableReference**: `docs/guides/examples/sdlg2.md`

- **AffectedExample**: `demo`
- **DeferredBehavior**: Historische Demo-Anteile fuer Editor, Hilfe, Streams, Terminaleffekte, Mausdialoge und echte Charset-Effekte.
- **Rationale**: Diese Bereiche gehoeren zu spaeteren Pflichtwellen und duerfen Welle-2-Akzeptanz nicht verfaelschen.
- **EarliestSchedulingPoint**: Welle 3 beziehungsweise Welle 4.
- **TraceableReference**: `docs/guides/examples/demo.md`
