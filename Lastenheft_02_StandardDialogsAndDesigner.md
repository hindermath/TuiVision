# Lastenheft: Standarddialoge und Dialog-Designer fuer Beispielwelle 2

**Dokument-Status:** Entwurf
**Erstellt:** 2026-03-29
**Betrifft:** `src/TuiVision.Controls/`, `src/TuiVision.Serialization/`, `tests/TuiVision.Controls.Tests/`, `tests/TuiVision.Examples.SmokeTests/`
**Empfohlene Prioritaet:** vor Start der Wave-2-Portierung abarbeiten

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Die Wave-2-Beispiele `demo`, `sdlg`, `sdlg2` und `dlgdsn` pruefen nicht nur
einzelne Controls, sondern komplette Dialogablaeufe mit zusammengesetzter
Navigation, Validierung, Dateiauswahl, Farbwahl und teilweise dynamischer
Dialogbeschreibung. Genau hier ist die Gefahr gross, dass aus vorhandenen
Klassennamen vorschnell "fertige" Features abgeleitet werden.

The wave-2 examples `demo`, `sdlg`, `sdlg2`, and `dlgdsn` validate not only
individual controls but complete dialog flows with composed navigation,
validation, file picking, color selection, and partly dynamic dialog
descriptions. This is exactly where existing class names can misleadingly
suggest completed features.

Mehrere beteiligte Typen sind bereits vorhanden, aber vom Dateiumfang und der
bisherigen Nutzung her noch nicht auf dem Niveau einer belastbaren
Standarddialog-Bibliothek. Das gilt besonders fuer Datei-/Verzeichnislisten,
Dateiinformation, History-Kopplung, Farbwahl und die Trennung zwischen
Dialogmodell, Laufzeitinstanz und persistierter Beschreibung.

Several participating types already exist, but based on their current size and
usage they are not yet at the level of a durable standard-dialog library. This
is especially true for file and directory lists, file metadata, history
integration, color selection, and the separation between dialog model, runtime
instance, and persisted description.

---

## 2. Betroffene Beispiele / Affected Examples

- `demo`
- `dlgdsn`
- `sdlg`
- `sdlg2`

Diese Beispiele bilden zusammen den eigentlichen Stresstest fuer wiederverwend-
bare Dialog- und Ressourceninfrastruktur in Welle 2.

Taken together, these examples are the real stress test for reusable dialog and
resource infrastructure in wave 2.

---

## 3. Ziele / Goals

- Standarddialoge als echte Framework-Oberflaeche statt als Demo-Hilfscode
  bereitstellen.
- Beispielcode von Infrastrukturcode trennen.
- Dynamische oder ressourcennahe Dialogdefinitionen nur auf einer klaren,
  testbaren Zwischenebene zulassen.

- Provide standard dialogs as a real framework surface instead of demo helper
  code.
- Separate example code from infrastructure code.
- Allow dynamic or resource-oriented dialog definitions only through a clear,
  testable intermediate model.

---

## 4. Anforderungen / Requirements

### R-01: Standarddialoge muessen zusammengesetzte Flows abdecken

Datei-, Verzeichnis-, Farb- und vergleichbare Standarddialoge muessen als
vollstaendige, wiederverwendbare Benutzerablaeufe beschrieben werden. Dazu
gehoeren Listen, History, manuelle Eingabe, Metadatenanzeige, Validierung,
Rueckgabewerte und konsistente Synchronisation der Teilcontrols.

File, directory, color, and comparable standard dialogs must be treated as
complete reusable user flows, including lists, history, manual entry, metadata
display, validation, return values, and consistent synchronization between
their subcontrols.

### R-02: Datei- und Verzeichnisdialoge duerfen keine lockeren Kopplungen haben

`TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine` und
`THistory` muessen einen expliziten Zustandsvertrag erhalten. Pfade, Filter,
selektierte Datei und Dateimetadaten muessen nachweisbar gemeinsam fortgeschrieben
werden.

`TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, and
`THistory` must receive an explicit state contract. Paths, filters, the
selected file, and file metadata must advance together in a verifiable way.

### R-03: Farb- und Anzeigeauswahl darf nicht beim Minimaldialog stehen bleiben

`TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup` und
`TColorDisplay` muessen als zusammenhaengender Auswahlfluss definiert werden.
`sdlg` und `sdlg2` duerfen keine lokal abweichenden Farbwahl-Widgets
implementieren.

`TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, and
`TColorDisplay` must be defined as one coherent selection flow. `sdlg` and
`sdlg2` must not implement divergent local color-picking widgets.

### R-04: Dialog-Designer braucht eine Zwischenreprasentation

Fuer `dlgdsn` muss klar zwischen Dialogbeschreibung, Laufzeitobjekten und
optional persistierter Form unterschieden werden. Ein Designer-Beispiel darf
nicht direkt Controls per unstrukturierter Ad-hoc-Logik erzeugen.

For `dlgdsn`, the project must clearly separate dialog description, runtime
objects, and optional persisted form. A designer example must not create
controls through unstructured ad-hoc logic.

### R-05: Ressourcen- und Serialisierungsanbindung nur mit expliziter Grenze

Falls Standarddialoge oder Designer-Flows Ressourcen, Streams oder
Konfigurationsdaten nutzen, muss die Grenze zu `TuiVision.Serialization`
objektiv beschrieben werden. Persistenz ist erlaubt, aber nicht als versteckte
Seiteneigenschaft eines Controls.

If standard dialogs or designer flows use resources, streams, or configuration
data, the boundary to `TuiVision.Serialization` must be described explicitly.
Persistence is allowed, but not as a hidden side effect of a control.

### R-06: Demo-Anwendung als Integrationsbeweis

`demo` ist nicht nur ein weiteres Beispiel, sondern der Integrationsbeweis fuer
mehrere Wave-2-Komponenten gleichzeitig. Vor seiner Portierung muss feststehen,
welche Teile aus dem Framework kommen und welche bewusst demo-spezifisch bleiben.

`demo` is not just another example; it is the integration proof for several
wave-2 components at once. Before porting it, the team must define which parts
come from the framework and which intentionally remain demo-specific.

### R-07: Keine Vorwegnahme von Editor-/Help- oder Terminal-Themen

Dieses Lastenheft darf weder Editor-/Help-Funktionalitaet aus Welle 3 noch
Terminal-/Zeichensatzthemen aus Welle 4 vorwegziehen. Es dient ausschliesslich
der Dialog- und Designer-Reife fuer Welle 2.

This requirements document must not pull editor/help features from wave 3 or
terminal/charset topics from wave 4 into scope. It exists only to harden dialog
and designer readiness for wave 2.

---

## 5. Nicht im Scope / Out of Scope

- Menue-/Status-/Fenster-Grundrevision aus `Lastenheft_ControlsRevision.md`
- Allgemeine Widget-Nacharbeit aus `Lastenheft_01_ControlsWidgetsAndCollections.md`
- Editor, Help-Compiler, Hilfefenster, Terminalemulation

- Menu/status/window baseline revision from `Lastenheft_ControlsRevision.md`
- General widget follow-up from `Lastenheft_01_ControlsWidgetsAndCollections.md`
- Editor, help compiler, help windows, terminal emulation

---

## 6. Akzeptanzkriterien / Acceptance Criteria

- Ein eigenes Feature kann gruene Tests fuer Datei-/Farb-/Designer-Flows
  liefern, bevor `demo`, `sdlg`, `sdlg2` oder `dlgdsn` voll portiert sind.
- Die betroffenen Beispiele koennen ihre Hauptlogik auf bestehende
  Framework-Typen abstuetzen, statt neue Dialoggerueste zu definieren.
- Persistierte oder dynamische Dialogdefinitionen besitzen eine dokumentierte,
  testbare Zwischenreprasentation.

- A dedicated feature can deliver green tests for file, color, and designer
  flows before `demo`, `sdlg`, `sdlg2`, or `dlgdsn` are fully ported.
- The affected examples can rely on existing framework types for their main
  logic instead of inventing new dialog scaffolding.
- Persisted or dynamic dialog definitions use a documented, testable
  intermediate representation.
