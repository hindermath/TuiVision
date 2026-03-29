# Lastenheft: Controls-Schicht-Revision — Portierungslücken schließen

**Dokument-Status:** Entwurf
**Erstellt:** 2026-03-28
**Betrifft:** `src/TuiVision.Controls/`, `tests/TuiVision.Controls.Tests/`
**Ziel-Branch:** `008-controls-revision` (vorgesehen)

---

## 1. Ausgangslage und Problemstellung / Background and Problem Statement

Die `porting-status.md` listet mehrere `.cc`-Quelldateien aus `tv203s/contrib/tvision/classes/` als
„portiert + getestet". Eine vergleichende Analyse zeigt jedoch, dass in mehreren Fällen nur eine
**Minimalimplementierung** entstanden ist, die den Umfang des Originals deutlich unterschreitet.
Die in Sprint 007 behobenen Bugs (F10-Taste wirkungslos, Hotkey-Hervorhebung fehlerhaft,
Untermenü-Popup nicht sichtbar) sind direkte Symptome dieser Lücken.

The `porting-status.md` lists several `.cc` source files from `tv203s/contrib/tvision/classes/` as
"portiert + getestet". A comparative analysis shows that in several cases only a **minimal
implementation** was created that falls significantly short of the original scope.
The bugs fixed in sprint 007 (F10 key non-functional, hotkey highlighting incorrect,
submenu popup not visible) are direct symptoms of these gaps.

---

## 2. Betroffene Komponenten / Affected Components

### 2.1 TMenuView → TMenuBar (kritisch / critical)

| Merkmal | Original `tmenuvie.cc` | C#-Port `TMenuBar.cs` |
|---|---|---|
| Dateigröße | 644 Zeilen | ~330 Zeilen (nach Bugfixes) |
| Modale Ausführung | `execute()` — blockierender Event-Loop | fehlt |
| Pfeiltasten-Navigation | `trackKey()`, links/rechts/oben/unten | fehlt |
| Maus-Tracking | `trackMouse()`, Hover-Hervorhebung | fehlt |
| Selektionshervorhebung | Aktuell gewählter Eintrag invertiert | fehlt |
| `TMenuBox` (Popup-Renderer) | Eigene Klasse `tmenubox.cc` (164 Zeilen) | in `TMenuBar` improvisiert |
| `TSubMenu` als eigene Klasse | `tsubmenu.cc` (50 Zeilen), eigenständig | fehlt als Klasse |
| `hotKey(ushort)` — globale Shortcuts | Globale Tastensuche durch Menübaum | fehlt |
| Palette konfigurierbar | `getPalette()` virtual | hartkodiert Cyan/Schwarz |
| Streaming | `write()` / `read()` | fehlt |

**Konkrete Auswirkung:** Untermenüs können derzeit nur per Hotkey navigiert werden.
Pfeiltasten, Maus-Hover und invertierte Selektion fehlen vollständig. Das entspricht nicht
dem erwarteten TUI-Verhalten.

**Concrete impact:** Submenus can currently only be navigated via hotkeys.
Arrow keys, mouse hover, and inverted selection are completely absent. This does not
match the expected TUI behaviour.

---

### 2.2 TStatusLine (wesentlich / significant)

| Merkmal | Original `tstatusl.cc` | C#-Port `TStatusLine.cs` |
|---|---|---|
| Dateigröße | 369 Zeilen | ~80 Zeilen |
| `TStatusDef` — Kontextzuordnung | Items an Help-Kontext-IDs gebunden | fehlt |
| `drawSelect()` | Hervorhebung des aktiven Items | fehlt |
| `itemMouseIsIn()` | Maus-Klick auf Status-Item | fehlt |
| `hint(ushort helpCtx)` virtual | Kontextabhängiger Hilfetext | statischer Text |
| `update()` | Reagiert auf Fokus-Wechsel | nur DrawView() |
| `findItems()` | Kontextabhängige Item-Suche | fehlt |
| Keyboard-Dispatch | Tastendrücke auf Items weiterleiten | fehlt |
| Streaming | `write()` / `read()` | fehlt |

**Konkrete Auswirkung:** Die Statuszeile zeigt immer denselben statischen Text.
Kontextabhängige Hinweise (z.B. „F2 Speichern" im Editor, „Esc Abbrechen" im Dialog)
sind nicht möglich. Maus-Klicks auf Status-Items lösen keine Befehle aus.

**Concrete impact:** The status line always shows the same static text. Context-sensitive
hints (e.g. "F2 Save" in the editor, "Esc Cancel" in a dialog) are not possible.
Mouse clicks on status items do not fire commands.

---

### 2.3 TWindow — Fenster-Flags (mittel / medium)

| Merkmal | Original `twindow.cc` | C#-Port `TWindow.cs` |
|---|---|---|
| `wfMove` Flag | Fenster verschiebbar | nicht implementiert |
| `wfClose` Flag | Schließen-Icon in Titelzeile | nicht implementiert |
| `wfZoom` Flag | Maximieren/Minimieren | nicht implementiert |
| `wfGrow` Flag | Größenänderung per Maus | nicht implementiert |
| Zoom-Rect speichern | `zoomRect` für Toggle | fehlt |
| `sizeLimits()` | Min/Max-Größe definierbar | fehlt |
| Streaming | `write()` / `read()` | fehlt |

**Konkrete Auswirkung:** Alle Fenster sind statisch positioniert. Nutzer können
Fenster weder verschieben noch schließen noch in der Größe ändern — alles
Kernfunktionen von Turbo Vision.

**Concrete impact:** All windows are statically positioned. Users cannot move,
close, or resize windows — all of which are core Turbo Vision features.

---

### 2.4 TDialog — Validierung und Schließverhalten (mittel / medium)

| Merkmal | Original `tdialog.cc` | C#-Port `TDialog.cs` |
|---|---|---|
| `valid(ushort command)` | Validierung vor Schließen | fehlt |
| `getPalette()` | Eigene Dialog-Farbpalette | hartkodiert |
| `endModal(ushort result)` | Modal-Result zurückgeben | nur bool |
| Default-Button bei Enter | Automatische Enter-Aktivierung | vorhanden ✓ |

---

### 2.5 TMenuBar — Fehlende Neuberechnung bei Größenänderung (gering / minor)

| Merkmal | Original | C# |
|---|---|---|
| `computeLength()` | Menübreite neu berechnen | fehlt |
| `changeBounds()` | Layout bei Resize aktualisieren | fehlt |

**Konkrete Auswirkung:** Bei Terminal-Resize werden Menüpunkte möglicherweise
abgeschnitten oder falsch positioniert.

---

## 3. Nicht im Scope / Out of Scope

Folgende Punkte sind **ausdrücklich ausgeschlossen**, um den Umfang kontrollierbar zu halten:

- Streaming / Serialisierung für Menu/Status/Dialog/Window — das ist Spec-Kit 004 (TStream)
- Maus-Unterstützung — kein Terminal-Maus-Protokoll im aktuellen Treiber implementiert
- `getPalette()` / konfigurierbare Farbpaletten — separates Theme-Feature
- Neue Beispiel-Waves (Wave 2+) — Voraussetzung für Wave 2, aber nicht Teil dieser Revision
- `TEditor`, `TMemo`, `TFileEditor` — Spec-Kit 004

---

## 4. Anforderungen / Requirements

### R-01: TMenuView-Pfeiltasten-Navigation

Die `TMenuBar`-Klasse (bzw. eine neue `TMenuView`-Basisklasse) **muss** Pfeiltasten
für folgende Aktionen unterstützen:

- **Links/Rechts**: zwischen Top-Level-Menüpunkten wechseln (wenn Menü aktiv)
- **Oben/Unten**: zwischen Untermenü-Einträgen wechseln (wenn Untermenü offen)
- **Enter**: aktuell hervorgehobenen Eintrag auslösen
- **Escape**: Untermenü schließen / Menü deaktivieren (bereits implementiert)

The `TMenuBar` class (or a new `TMenuView` base class) **must** support arrow keys
for navigation.

**Akzeptanzkriterium / Acceptance criterion:**
Automatisierter Test: Taste `ConsoleKey.DownArrow` in aktivem Menü
→ erster Untermenüeintrag wird als selektiert markiert.

---

### R-02: Selektionshervorhebung in Untermenü

Das offene Untermenü **muss** den aktuell fokussierten Eintrag visuell hervorheben
(invertierte Farben: Schwarz auf Gelb oder gleichwertiges Kontrast-Schema).

The open submenu **must** visually highlight the currently focused item
(inverted colours: black on yellow or equivalent high-contrast scheme).

**Akzeptanzkriterium:** Hervorgehobener Eintrag ist im Render-Buffer mit invertierter
Vorder-/Hintergrundfarbe gesetzt.

---

### R-03: TSubMenu als eigenständige Klasse

`TSubMenu` **sollte** als eigenständige Klasse in `src/TuiVision.Controls/TSubMenu.cs`
existieren und eine konsistente API zum Aufbau von Menühierarchien bieten.
Dies schließt Kompatibilität mit dem tvguid02-Stil ein:

```csharp
new TSubMenu("~D~atei / ~F~ile", 0,
    new TMenuItem("~N~eu / ~N~ew", CmFileNew) +
    new TMenuItem("~Ö~ffnen / ~O~pen", CmFileOpen) +
    new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit))
```

`TSubMenu` **should** exist as a standalone class providing a consistent API for
building menu hierarchies.

**Akzeptanzkriterium:** tvguid02-Übungskommentar (Lösungsblock §Übung 1) compiliert
ohne Änderungen.

---

### R-04: TStatusLine — Kontextabhängige Items

`TStatusLine` **muss** ein System unterstützen, bei dem verschiedene `TStatusItem`-Listen
in Abhängigkeit vom aktiven `HelpContext` angezeigt werden. Mindestimplementierung:

- `TStatusDef`: Verknüpft einen Help-Kontext-Bereich mit einer `TStatusItem`-Liste
- `TStatusLine(TRect, TStatusDef[])`: Konstruktor mit Kontext-Definitionen
- `Update()`: Aktualisiert die angezeigte Item-Liste basierend auf dem aktuellen Fokus

`TStatusLine` **must** support displaying different `TStatusItem` lists depending on
the active `HelpContext`.

**Akzeptanzkriterium:** Test: `TStatusLine` mit zwei `TStatusDef`-Einträgen für
Help-Kontext 0 und 1 — bei Kontext-Wechsel werden korrekte Items angezeigt.

---

### R-05: TWindow — Schließen-Icon (wfClose)

`TWindow` **muss** ein sichtbares Schließen-Icon (`[✕]` oder `[ ]`) in der linken
Titelzeile rendern, wenn das Flag `WindowFlags.Close` gesetzt ist.
Ein Klick auf das Icon **oder** `Ctrl+W` / `Escape` (wenn kein anderes Steuerelement
fokussiert ist) **muss** das Fenster schließen.

`TWindow` **must** render a close icon in the title bar and must support closing
via icon click or keyboard shortcut.

**Akzeptanzkriterium:** Test: `TWindow` mit `WindowFlags.Close` →
Buffer enthält `[` + `✕` + `]` an Position `(Origin.X, Origin.Y)`.

---

### R-06: TWindow — Verschieben (wfMove)

`TWindow` **sollte** per Tastatur verschiebbar sein, wenn `WindowFlags.Move` gesetzt ist.
Standardtastenkombination: `Ctrl+F5` öffnet den Move-Modus (Pfeiltasten verschieben,
Enter/Escape beenden).

`TWindow` **should** be keyboard-movable when `WindowFlags.Move` is set.

**Akzeptanzkriterium:** Test: Move-Befehl + 3× `DownArrow` → `Origin.Y` um 3 erhöht.

---

### R-07: TMenuBar — Neuberechnung bei changeBounds

`TMenuBar` **muss** bei `Locate()`/`Resize()` die intern berechneten Spaltenpositionen
der Menüpunkte neu berechnen, sodass Menüpunkte nach einem Terminal-Resize korrekt
positioniert sind.

`TMenuBar` **must** recalculate menu item column positions on `Locate()`/`Resize()`.

**Akzeptanzkriterium:** Test: `TMenuBar` auf Breite 40 → alle Items passen;
Resize auf Breite 20 → Items rechts werden abgeschnitten oder weggelassen,
keine falsche Position.

---

## 5. Priorisierung / Prioritization

| ID | Beschreibung | Priorität | Begründung |
|---|---|---|---|
| R-01 | Pfeiltasten-Navigation Menü | **Hoch** | Blockiert Wave-2-Beispiele (`tvguid03`+) |
| R-02 | Selektionshervorhebung Untermenü | **Hoch** | Visuell notwendig für Usability |
| R-04 | TStatusLine Kontextabhängig | **Mittel** | Benötigt für Editor-Beispiele (004) |
| R-05 | TWindow Schließen-Icon | **Mittel** | Kernfunktion, Wave-2-Voraussetzung |
| R-03 | TSubMenu Klasse | **Mittel** | Verbessert API, tvguid02-Übung |
| R-06 | TWindow Verschieben | **Niedrig** | Komfort, kein Wave-2-Blocker |
| R-07 | TMenuBar Resize-Neuberechnung | **Niedrig** | Randfall, funktional ausreichend |

---

## 6. Abgrenzung zu bestehenden Spec-Kits / Demarcation from existing Spec-Kits

| Spec-Kit | Überschneidung | Abgrenzung |
|---|---|---|
| 004 (Editor/File/Help) | TStatusLine Hint-System | 004 erfordert funktionsfähige TStatusLine als Voraussetzung — diese Revision liefert sie |
| 007 (Wave-1-Examples) | TMenuBar Fixes | Wave-1 abgeschlossen; diese Revision enablet Wave 2 |
| 003 (Dialog/Control) | TDialog valid() | 003 war die ursprüngliche Portierung; diese Revision schließt Lücken nach |
| 005/006 (Driver/Gate) | Keine direkten Abhängigkeiten | — |

---

## 7. Offene Fragen / Open Questions

1. **TMenuView als Basisklasse oder als integrierter Teil von TMenuBar?**
   Das Original hat `TMenuBar : TMenuView : TView`. Lohnt sich die Extraktion von
   `TMenuView` als abstrakte Basisklasse für `TMenuBar` und `TMenuBox`?
   → Empfehlung: Ja, wenn R-01/R-02 implementiert werden, sonst technische Schulden.

2. **Maus-Support in Scope?**
   `tmenuvie.cc` enthält umfangreiche Maus-Tracking-Logik. Da der Treiber kein
   Terminal-Maus-Protokoll implementiert, ist Maus-Support derzeit nicht realisierbar.
   → Ausgeschlossen, aber als zukünftiges Feature markieren.

3. **Separater Branch `008-controls-revision` oder Teil von `007`?**
   Da Wave-1 formal abgeschlossen ist, empfiehlt sich ein eigener Branch `008`.

---

## 8. Akzeptanzkriterien gesamt / Overall Acceptance Criteria

- [ ] Alle unter R-01 bis R-07 genannten automatisierten Tests sind grün
- [ ] `dotnet build --configuration Release` ohne Warnungen und Fehler
- [ ] `dotnet test` — alle bestehenden 41 Smoke-Tests weiterhin grün
- [ ] Coverage-Gate ≥ 70 % für `TuiVision.Controls` weiterhin erfüllt
- [ ] `dotnet format --verify-no-changes` besteht
- [ ] Alle neuen öffentlichen Typen und Member mit vollständigen XML-Kommentaren (DE/EN, CEFR-B2)
- [ ] `porting-status.md` aktualisiert: betroffene Einträge spiegeln den tatsächlichen Implementierungsstand wider

---

*Ende des Lastenhefts / End of requirements document*
