# Feature Specification: Dialog-/Control-Schicht (Dialog and Control Layer)

**Feature Branch**: `003-dialog-control-layer`
**Created**: 2026-03-21
**Status**: Draft
**Input**: Pflichtenheft §8.1 Nr. 5 — "Dialog-/Control-Schicht: Eingabezeilen, Listen, Scrollbars, Buttons, usw."

---

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Interaktiver Dialog mit Steuerelementen (Priority: P1)

Als Entwickler einer TUI-Anwendung (Text User Interface) möchte ich ein modales Dialogfenster
(`TDialog`) mit mehreren interaktiven Steuerelementen zusammenstellen können, damit Nutzer
Einstellungen vornehmen oder Eingaben bestätigen können — analog zu klassischen Turbo-Vision-Dialogen.

As a TUI application developer I want to compose a modal dialog (`TDialog`) with multiple
interactive controls so that end users can enter data, confirm choices, and close the dialog
via keyboard or mouse.

**Why this priority**: `TDialog` ist das zentrale Koordinationsobjekt dieser Schicht. Ohne
funktionsfähigen Dialog sind alle anderen Controls zwar isoliert testbar, aber nicht in ihrem
eigentlichen Einsatzkontext nutzbar. Ein lauffähiger Dialog mit mindestens einem Button ist
das kleinste sinnvolle Nutzungsartefakt der gesamten Control-Schicht.

Without a working `TDialog`, all other controls are testable in isolation but not usable in
their primary context. A runnable dialog with at least one button is the smallest meaningful
deliverable of the entire control layer.

**Independent Test**: Ein Dialog mit einem einzigen "OK"-Button (`TButton`) kann eigenständig
instantiiert, im TuiVision-Fenster angezeigt und per Enter-Taste geschlossen werden — vollständig
ohne weitere Controls.

A dialog containing a single "OK" `TButton` can be instantiated, displayed in the TuiVision
window, and closed via Enter — fully independent of other controls.

**Acceptance Scenarios**:

1. **Given** eine laufende TuiVision-Anwendung, **When** ein `TDialog` mit zwei `TButton`-Instanzen ("OK" und "Abbrechen") geöffnet wird, **Then** erscheint das Dialogfenster mit Rahmen und Titel, beide Schaltflächen sind sichtbar, und der Fokus liegt auf dem ersten Button.
2. **Given** ein offener Dialog, **When** der Nutzer Tab drückt, **Then** wechselt der Fokus zyklisch (Wrap-around) zwischen allen fokussierbaren Controls: nach dem letzten Control springt der Fokus zurück zum ersten; Shift-Tab läuft in umgekehrter Richtung mit demselben Wrap-around.
3. **Given** ein offener Dialog mit einem fokussierten "Abbrechen"-Button, **When** der Nutzer Enter drückt, **Then** schließt der Dialog und gibt den zugehörigen Rückgabewert (Command-ID) an die aufrufende Schicht zurück.
4. **Given** ein offener Dialog, **When** der Nutzer Escape drückt, **Then** schließt der Dialog mit `cmCancel` als Standardwert für "Abgebrochen".

---

### User Story 2 - Einzeilige Texteingabe (Priority: P2)

Als Entwickler möchte ich ein einzeiliges Texteingabefeld (`TInputLine`) in einen Dialog
einbauen können, damit Nutzer Zeichenketten (z. B. Dateinamen, Suchwörter) eingeben können.

As a developer I want to embed a single-line text input field (`TInputLine`) in a dialog so
that users can type strings such as file names or search terms.

**Why this priority**: `TInputLine` ist das häufigste Dateneingabe-Control in klassischen
Turbo-Vision-Anwendungen. Es ist Voraussetzung für nahezu alle Formulardialoge und für mehrere
Beispielprogramme (z. B. `inplis`, `tutorial`).

`TInputLine` is the most common data-entry control in Turbo Vision applications and a prerequisite
for nearly all form dialogs and several example programs.

**Independent Test**: Ein `TInputLine` mit einem daneben platzierten `TLabel` und einem
Bestätigungs-`TButton` kann alleinstehend in einem Dialog getestet werden: Texteingabe,
Löschen (Backspace/Delete), und Auslesen des eingegebenen Wertes nach Dialog-Schluss.

**Acceptance Scenarios**:

1. **Given** ein Dialog mit einem `TInputLine`, **When** der Nutzer alphanumerische Zeichen eingibt, **Then** erscheinen diese zeichenweise im Eingabefeld an der Cursorposition.
2. **Given** ein `TInputLine` mit eingegebenen Zeichen, **When** der Nutzer Pos1/Ende oder Pfeiltasten drückt, **Then** bewegt sich der Cursor entsprechend und der sichtbare Ausschnitt scrollt bei langen Texten mit.
3. **Given** ein `TInputLine` mit konfigurierter Maximallänge, **When** der Nutzer diese Länge erreicht hat, **Then** werden weitere Zeichen nicht mehr angenommen.
4. **Given** ein `TInputLine` mit Inhalt, **When** der Nutzer Backspace drückt, **Then** wird das Zeichen links des Cursors gelöscht; **When** der Nutzer Delete drückt, **Then** das Zeichen rechts des Cursors.

---

### User Story 3 - Scrollbare Listenauswahl (Priority: P3)

Als Entwickler möchte ich eine scrollbare Listenansicht (`TListBox`) mit gekoppelter
`TScrollBar` in einen Dialog einbauen, damit Nutzer aus einer längeren Werteliste einen
Eintrag auswählen können.

As a developer I want to embed a scrollable list view (`TListBox`) with a linked `TScrollBar`
in a dialog so that users can select one entry from a longer list of values.

**Why this priority**: Listen mit Scrollbar sind essenziell für Dateiauswahl-Dialoge und
viele Beispielprogramme (`listvi`, `inplis`, `tvedit`). `TListViewer` ist die abstrakte
Basis; `TListBox` ist der erste konkrete Ableger und gibt dem Muster sofort praktischen Nutzen.

**Independent Test**: Eine `TListBox` mit 20 String-Einträgen und einer vertikalen `TScrollBar`
kann eigenständig — ohne weitere Controls — angezeigt und per Tastatur (Pfeiltasten, PgUp/PgDn)
bedient werden. Die Scrollbar aktualisiert ihre Position synchron.

**Acceptance Scenarios**:

1. **Given** eine `TListBox` mit 20 Einträgen in einem fünf Zeilen hohen Control, **When** der Nutzer Pfeil-Ab drückt, **Then** bewegt sich die Markierung zum nächsten Eintrag; eine gekoppelte `TScrollBar` aktualisiert ihre Position.
2. **Given** eine `TListBox` am letzten sichtbaren Eintrag, **When** der Nutzer Pfeil-Ab drückt, **Then** scrollt die Liste um eine Zeile nach unten und zeigt den nächsten Eintrag.
3. **Given** eine leere `TListBox`, **When** sie angezeigt wird, **Then** erscheint ein leeres Rechteck ohne Absturz oder visuellen Artefakt.
4. **Given** eine `TListBox` mit Einträgen, **When** der Nutzer auf einen sichtbaren Eintrag klickt, **Then** wird dieser Eintrag markiert; ein Doppelklick bestätigt dieselbe Auswahl ohne separates zusätzliches Command-Ereignis.

---

### User Story 4 - Schaltflächen (Priority: P4)

Als Entwickler möchte ich `TButton`-Instanzen in Dialoge einbauen, damit Nutzer Aktionen
auslösen oder den Dialog mit einem definierten Ergebnis schließen können.

As a developer I want to place `TButton` instances in dialogs so users can trigger actions
or close the dialog with a defined result.

**Why this priority**: Buttons sind in fast jedem Dialog erforderlich. Sie sind jedoch
einfacher als Listen und Eingabefelder und bauen auf der bereits in P1 beschriebenen
Dialog-Grundstruktur auf.

**Independent Test**: Ein einzelner `TButton` mit Tastaturkürzel kann eigenständig in einem
Dialog platziert, per Enter/Leertaste aktiviert und sein Command-ID-Rückgabewert geprüft werden.

**Acceptance Scenarios**:

1. **Given** ein fokussierter `TButton`, **When** der Nutzer Enter oder Leertaste drückt, **Then** wird die dem Button zugeordnete Command-ID ausgelöst.
2. **Given** ein `TButton` mit konfiguriertem Buchstaben-Kürzel (z. B. "~O~K"), **When** der Nutzer Alt+O drückt, **Then** wird der Button aktiviert ohne direkten Fokus auf ihm.
3. **Given** ein deaktivierter `TButton` (Disabled-State), **When** der Nutzer Tab drückt, **Then** wird der Button beim Fokus-Durchlauf übersprungen.
4. **Given** ein Dialog mit einem als Default markierten "OK"-Button und einem fokussierten `TInputLine`, **When** der Nutzer Enter drückt, **Then** wird der Default-Button aktiviert (da `TInputLine` Enter nicht selbst konsumiert, sondern für Zeilenende-Signale reserviert).

---

### User Story 5 - Auswahlgruppen: Checkboxen und Radiobuttons (Priority: P5)

Als Entwickler möchte ich `TCheckBoxes` für Mehrfachauswahl und `TRadioButtons` für
Einfachauswahl in Dialogen verwenden, damit Nutzer aus einer festen Optionsmenge wählen können.

As a developer I want to use `TCheckBoxes` for multi-selection and `TRadioButtons` for
single-selection in dialogs so users can choose from a fixed set of labeled options.

**Why this priority**: Beide bauen auf der gemeinsamen abstrakten Basis `TCluster` auf.
Sie sind eigenständig testbar, aber gegenüber Dialog, Eingabefeld und Liste sekundär, da
sie typischerweise ergänzend eingesetzt werden.

**Independent Test**: Ein `TCheckBoxes`-Control mit drei Optionen kann eigenständig in
einem Dialog angezeigt und per Leertaste/Pfeiltasten bedient werden; der Zustand (Bitmask)
kann nach Interaktion ausgelesen und geprüft werden.

**Acceptance Scenarios**:

1. **Given** ein `TCheckBoxes`-Control mit drei Optionen, **When** der Nutzer Leertaste auf einer Option drückt, **Then** toggelt der Häkchenzustand dieser Option unabhängig von den anderen.
2. **Given** ein `TRadioButtons`-Control mit drei Optionen, **When** der Nutzer eine andere Option per Pfeiltaste oder Leertaste auswählt, **Then** wird die vorherige Option automatisch deselektiert (Einfachauswahl).
3. **Given** ein `TCheckBoxes` oder `TRadioButtons` mit allen deaktivierten Optionen (Disabled), **When** der Fokus auf das Control gesetzt wird, **Then** kann keine der Optionen verändert werden.

---

### User Story 6 - Statische Anzeige: Beschriftungen und Texte (Priority: P6)

Als Entwickler möchte ich `TStaticText` für unveränderliche Textblöcke und `TLabel` als
tastaturgesteuerte Beschriftung eines anderen Controls einsetzen, damit Dialoge strukturiert
und lesbar aufgebaut werden können.

As a developer I want to use `TStaticText` for static text blocks and `TLabel` as a
keyboard-navigable label attached to another control to structure and annotate dialogs.

**Why this priority**: Statische Controls sind nicht-interaktiv und damit am einfachsten
zu implementieren. Sie sind dennoch wichtig für vollständig nutzbare Dialoge.

**Independent Test**: Ein `TStaticText` mit mehrzeiligem Text kann in einem Dialog angezeigt
werden und nimmt keinen Fokus an. Ein `TLabel`, das auf ein `TInputLine` zeigt, leitet bei
Alt+Kürzel den Fokus an das verknüpfte Feld weiter.

**Acceptance Scenarios**:

1. **Given** ein `TStaticText` in einem Dialog, **When** der Nutzer Tab drückt, **Then** wird das Control beim Fokus-Durchlauf übersprungen.
2. **Given** ein `TLabel` mit Kürzel "~N~ame:" das mit einem `TInputLine` verknüpft ist, **When** der Nutzer Alt+N drückt, **Then** erhält das verknüpfte `TInputLine` den Fokus.

---

### Edge Cases

- Was passiert, wenn eine `TListBox` mit null Einträgen angezeigt wird (leere `TStringList`)?
- Wie verhält sich `TInputLine`, wenn die konfigurierte Maximallänge 0 beträgt?
- Wie reagiert `TDialog`, wenn kein einziges enthaltenes Control fokussierbar ist?
- Wie verhält sich `TScrollBar`, wenn sie an den Grenzen (Position 0 bzw. Maximum) angelangt ist und der Nutzer weiter scrollt?
- Was passiert, wenn `TRadioButtons` mit nur einer Option gerendert wird?
- Wie reagiert das System auf Mausereignisse außerhalb eines offenen modalen Dialogs?

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Das Framework MUSS eine `TDialog`-Klasse bereitstellen, die ein gerahmtes Dialogfenster mit Titel darstellt, modale Ausführung per **synchron blockierendem Event-Loop** (analog zu `TGroup.ExecView()` im Original) unterstützt und den Fokus automatisch unter allen enthaltenen fokussierbaren Controls verwaltet. `TDialog.Run()` kehrt erst zurück, wenn der Dialog über eine Command-ID geschlossen wird. Die Escape-Taste MUSS den Dialog standardmäßig mit `cmCancel` schließen. *(The framework MUST provide a `TDialog` class that displays a bordered, titled dialog window, supports modal execution via a **synchronously blocking event loop** (analogous to `TGroup.execView()` in the original), and automatically manages focus across all focusable controls. `TDialog.Run()` returns only when the dialog is closed with a command ID. The Escape key MUST close the dialog with `cmCancel` by default.)*

- **FR-002**: Das Framework MUSS eine `TInputLine`-Klasse bereitstellen, die einzeilige Texteingabe mit konfigurierbarer Maximallänge, Cursor-Bewegung (Pos1, Ende, Pfeiltasten), Einfüge-/Überschreibmodus und zeichenweisem Löschen (Backspace, Delete) unterstützt. *(The framework MUST provide a `TInputLine` class supporting single-line text input with configurable maximum length, cursor movement, insert/overwrite mode, and character deletion.)*

- **FR-003**: Das Framework MUSS eine abstrakte `TListViewer`-Basisklasse und eine konkrete `TListBox`-Klasse bereitstellen, die scrollbare String-Kollektionen anzeigt und Einzel-Itemauswahl per Tastatur (Pfeiltasten, PgUp, PgDn, Pos1, Ende) und Mausklick/-doppelklick unterstützt. Ein Doppelklick bestätigt den angeklickten Eintrag als Auswahl, löst aber innerhalb dieses Feature-Umfangs kein separates zusätzliches Command-Ereignis aus. *(The framework MUST provide an abstract `TListViewer` base class and a concrete `TListBox` class that displays scrollable string collections with item selection via keyboard and mouse. A double-click confirms the clicked item as the selected item but does not emit a separate additional command event within this feature scope.)*

- **FR-004**: Das Framework MUSS eine `TScrollBar`-Klasse bereitstellen, die horizontale oder vertikale Scroll-Position visuell anzeigt und Scroll-Ereignisse an die verknüpfte scrollbare View weiterleitet. *(The framework MUST provide a `TScrollBar` class that displays scroll position and forwards scroll events to the linked scrollable view.)*

- **FR-005**: Das Framework MUSS eine `TButton`-Klasse bereitstellen, die bei Aktivierung per Enter-Taste, Leertaste, Mausklick oder konfiguriertem Buchstaben-Kürzel eine Command-ID auslöst. `TButton` MUSS ein **Default-Button-Flag** (`bfDefault`) unterstützen: Ein als Default markierter Button wird durch Enter aktiviert, wenn das aktuell fokussierte Control die Enter-Taste nicht selbst konsumiert. *(The framework MUST provide a `TButton` class that fires a command ID when activated via Enter, spacebar, mouse click, or configured letter shortcut. `TButton` MUST support a **default button flag** (`bfDefault`): a button marked as default is activated by Enter whenever the currently focused control does not consume the Enter key itself.)*

- **FR-006**: Das Framework MUSS eine abstrakte `TCluster`-Basis sowie konkrete `TCheckBoxes`- und `TRadioButtons`-Klassen bereitstellen. `TCheckBoxes` MUSS unabhängige Mehrfachauswahl (Bitmask) und `TRadioButtons` gegenseitig ausschließende Einfachauswahl aus beschrifteten Optionen unterstützen. *(The framework MUST provide an abstract `TCluster` base and concrete `TCheckBoxes` (multi-selection bitmask) and `TRadioButtons` (mutually exclusive single-selection) classes.)*

- **FR-007**: Das Framework MUSS eine `TStaticText`-Klasse für nicht-interaktive Textanzeige und eine `TLabel`-Klasse bereitstellen, die per Tastaturkürzel den Fokus an ein verknüpftes Peer-Control weiterleitet. *(The framework MUST provide `TStaticText` for non-interactive text display and `TLabel` that redirects keyboard focus to a linked peer control via a hotkey.)*

- **FR-008**: Das Framework MUSS eine `TScroller`-Basisklasse bereitstellen, die koordiniertes horizontales und vertikales Scrollen mit gekoppelten `TScrollBar`-Instanzen ermöglicht. *(The framework MUST provide a `TScroller` base class that enables coordinated horizontal and vertical scrolling with linked `TScrollBar` instances.)*

- **FR-009**: Das Framework MUSS eine `TStringList`-Klasse als indizierte String-Sammlung bereitstellen, die als Datenmodell für `TListBox` und andere auf `TListViewer` basierende Controls dient. *(The framework MUST provide a `TStringList` class as an indexed string container serving as data model for `TListBox` and other `TListViewer`-based controls.)*

- **FR-010**: Alle Control-Klassen MÜSSEN in das bestehende TuiVision-Ereignissystem (`TEvent`) integriert sein, das Fokus-/Zustandsmodell aus `TView` einhalten und ausschließlich über den TuiVision-Consolenbuffer rendern — ohne native OS-Abhängigkeiten. *(All control classes MUST integrate with the TuiVision event system, honor the `TView` focus/state model, and render exclusively through the TuiVision console buffer without native OS dependencies.)*

- **FR-011**: Alle portierten Klassen MÜSSEN vollständige zweisprachige XML-Dokumentation (Deutsch zuerst, Englisch zweite Sprache) gemäß Pflichtenheft §10.1 und §10.3 aufweisen. *(All ported classes MUST carry complete bilingual XML documentation — German first, English second — conforming to Pflichtenheft §10.1 and §10.3.)*

- **FR-012**: Alle portierten Controls MÜSSEN durch MSTest-Unit-Tests mit mindestens einem Positiv- und einem Negativ-/Fehlerfalltest pro Klasse abgedeckt sein (Pflichtenheft §9.4 Nr. 2). *(All ported controls MUST be covered by MSTest unit tests with at least one positive and one negative/error-case test per class, per Pflichtenheft §9.4 Nr. 2.)*

### Key Entities

- **TDialog**: Modales oder modusloses Dialogfenster; koordiniert Fokussteuerung über eine Sammlung interaktiver Controls; gibt eine Command-ID als Schlussergebnis zurück. *(Modal or modeless dialog window; coordinates focus across a collection of interactive controls; returns a command ID as result.)*

- **TInputLine**: Einzeiliges Texteingabefeld; verwaltet Textinhalt, Cursorposition und Maximallänge; rendert optional mit sichtbarem Cursor. *(Single-line text input field; maintains text content, cursor position, and maximum length.)*

- **TListViewer**: Abstrakte Basisklasse für Listenansichten; verwaltet Fokus-Item, Bereich und Verknüpfungen zu `TScrollBar`-Instanzen. *(Abstract base for list displays; manages focus item, range, and links to scroll bars.)*

- **TListBox**: Konkrete Listenansicht; referenziert eine `TStringList` als Datenquelle; erbt von `TListViewer`. *(Concrete list control; references a `TStringList` as data source; inherits from `TListViewer`.)*

- **TStringList**: Indizierte String-Sammlung; dient als Datenmodell für listbasierte Controls. *(Indexed string collection; serves as the data model for list-based controls.)*

- **TScrollBar**: Visueller Scrollanzeiger; leitet Scroll-Kommandos an seinen Besitzer weiter. *(Visual scroll indicator; forwards scroll commands to its owner.)*

- **TScroller**: Scrollbare View-Basis; koordiniert sich mit horizontalen und vertikalen `TScrollBar`-Instanzen. *(Scrollable view base; coordinates with horizontal and vertical `TScrollBar` instances.)*

- **TButton**: Schaltfläche; speichert Beschriftung und Command-ID; löst Kommando-Ereignisse bei Aktivierung aus. *(Push button; stores caption and command ID; fires command events on activation.)*

- **TCluster**: Abstrakte Basis für gruppierte Auswahl-Controls; verwaltet Zustands-Bitmask und Item-Beschriftungen. *(Abstract base for grouped choice controls; manages state bitmask and item labels.)*

- **TCheckBoxes**: Konkrete Mehrfachauswahl-Gruppe; erbt von `TCluster`; jede Option ist unabhängig. *(Concrete multi-selection group; inherits from `TCluster`; each option is independent.)*

- **TRadioButtons**: Konkrete Einfachauswahl-Gruppe; erbt von `TCluster`; gegenseitiger Ausschluss aller Optionen. *(Concrete single-selection group; inherits from `TCluster`; mutually exclusive options.)*

- **TStaticText**: Nicht-interaktive Textanzeige; nimmt keinen Fokus an. *(Non-interactive text display; never receives focus.)*

- **TLabel**: Text-Beschriftung mit Tastaturkürzel; leitet Fokus bei Kürzel-Aktivierung an ein verknüpftes Peer-Control weiter. *(Text label with hotkey; redirects focus to a linked peer control on hotkey activation.)*

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Ein Entwickler kann innerhalb einer TuiVision-Anwendung ohne plattformspezifischen Code einen funktionsfähigen Dialog zusammenstellen und ausführen, der mindestens `TInputLine`, `TListBox`, `TButton`, `TCheckBoxes` und `TRadioButtons` enthält. *(A developer can compose and run a functional dialog containing at minimum `TInputLine`, `TListBox`, `TButton`, `TCheckBoxes`, and `TRadioButtons` without writing any platform-specific code.)*

- **SC-002**: Alle 13 Control-Klassen (`TDialog`, `TInputLine`, `TListViewer`, `TListBox`, `TStringList`, `TScrollBar`, `TScroller`, `TButton`, `TCluster`, `TCheckBoxes`, `TRadioButtons`, `TStaticText`, `TLabel`) sind in `TuiVision.Controls` vorhanden, buildbar und einzeln testbar. *(All 13 control classes are present in `TuiVision.Controls`, buildable, and independently testable.)*

- **SC-003**: Die Line-Coverage von `TuiVision.Controls` erreicht nach Fertigstellung aller Control-Schicht-Klassen mindestens 70 % (Pflichtenheft §9.4 Nr. 1). *(Line coverage for `TuiVision.Controls` reaches at least 70% after all control-layer classes are added, per Pflichtenheft §9.4 Nr. 1.)*

- **SC-004**: Jedes Control verarbeitet Tastaturnavigation korrekt (Tab/Shift-Tab für Fokus, Enter/Escape für Dialog-Schluss, Pfeiltasten für Listen- und Cluster-Navigation) ohne native OS-Eingabe-APIs. *(Every control correctly handles keyboard navigation — Tab/Shift-Tab for focus, Enter/Escape for dialog closure, arrow keys for list and cluster navigation — without native OS input APIs.)*

- **SC-005**: Jedes Control rendert alle relevanten Zustände (Normal, Fokussiert, Deaktiviert, Ausgewählt) korrekt in den TuiVision-Consolenbuffer, ohne visuelle Artefakte oder Überschreibung außerhalb der deklarierten Bounds. *(Every control renders all relevant visual states — Normal, Focused, Disabled, Selected — correctly into the TuiVision console buffer without artifacts or overdraw beyond declared bounds.)*

- **SC-006**: Die gesamte öffentliche API aller 13 Klassen ist mit zweisprachigen XML-Kommentaren vollständig dokumentiert; `docfx` generiert lückenlose API-Referenzseiten ohne Warnungen für dieses Modul. *(The complete public API of all 13 classes is documented with bilingual XML comments; `docfx` generates complete API reference pages for this module without warnings.)*

---

## Assumptions

- Die bereits vorhandenen Klassen `TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuBar` und `TStatusLine` aus den Portierungsphasen 1–4 sind stabil und liefern die Basis-Infrastruktur (Ereignissystem, Fokusmodell, Consolenbuffer) für die neue Control-Schicht. *(The already-ported classes `TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuBar`, and `TStatusLine` from phases 1–4 are stable and provide the event system, focus model, and console buffer infrastructure needed by this layer.)*

- `TDirListBox` (Quelle: `tdirlist.cc`) wird als Bestandteil der Editor-/Datei-Schicht (Portierungsphase 6) behandelt und ist **nicht** Teil dieses Feature-Umfangs, da es von Datei-/Verzeichnis-Systemkenntnissen abhängt, die in Phase 5 noch nicht vorausgesetzt werden.

- `TFileDialog` und die Standard-Dialoge aus `dialogs.h` sind ebenfalls **nicht** Teil dieses Umfangs; sie werden in Phase 6 (Editor/Datei/Hilfe) portiert.

- Maus-Interaktion wird implementiert soweit vom TuiVision-Event-System (`TEvent`) bereits unterstützt; vollständige Maus-Treiberintegration ist Gegenstand von Phase 7 (Treiberkonsolidierung).

---

## Clarifications

### Session 2026-03-21

- Q: Wie soll das modale Ausführungsmodell von `TDialog` gestaltet sein — synchron blockierend oder event-getrieben nicht-blockierend? → A: Synchron blockierend: `TDialog.Run()` blockiert den aufrufenden Code bis zum Dialog-Schluss, analog zu `TGroup.execView()` im Turbo Vision Original.
- Q: Fokus-Wrap-Verhalten in `TDialog`: springt Tab am letzten Control zum ersten (Wrap-around) oder stoppt an der Grenze? → A: Wrap-around: Tab am letzten Control → springt zum ersten; Shift-Tab am ersten → springt zum letzten.
- Q: Soll `TButton` ein Default-Button-Flag (`bfDefault`) unterstützen, das Enter-Aktivierung auch ohne direkten Fokus erlaubt? → A: Ja — Default-Button-Flag unterstützen; Enter aktiviert den Default-Button, wenn das fokussierte Control Enter nicht selbst konsumiert.
- Q: Welchen Standard-Rückgabewert soll Escape für `TDialog` liefern? → A: `cmCancel`.
- Q: Was bedeutet Doppelklick in `TListBox` genau? → A: Doppelklick bestätigt die angeklickte Auswahl, löst aber kein separates zusätzliches Command-Ereignis aus.
