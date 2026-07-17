# TP7 File Manager: vollständige Wave-6-Showcase-Stufe

## Zweck

`Tp7FileManager` überträgt den Lernzweck des historischen Turbo-Pascal-
Dateimanagers aus `TVFM/` in eine moderne C#-Anwendung. Die Anwendung zeigt
kontrollierte Navigation, Vorschau, Suche und Dateioperationen über echte
TuiVision-Menüs, Views, Dialoge, Statusmeldungen und Tastaturpfade. Sie ist
kein Ersatz für Finder, Explorer oder einen produktiven Dateimanager.

## Start

```bash
dotnet run --project examples/Tp7FileManager
```

Der normale Start kopiert ausschließlich mitgelieferte Fixtures in ein
temporäres Verzeichnis. Beim Beenden wird dieses Verzeichnis gelöscht.

Der deterministische Smoke-Pfad verwendet dieselbe Anwendungsschleife:

```bash
dotnet run --project examples/Tp7FileManager -- --smoke
```

## Erster sichtbarer Zustand

Der erste Frame enthält ein persistentes Fenster `TP7 TVFM`, eine
fokussierbare Dateiliste, einen Detailbereich und eine echte `TStatusLine`.
Der Kopf zeigt den kontrollierten relativen Pfad, Filter und Sortierung. Bei
normaler Breite stehen Liste und Detail nebeneinander; bei `48x16` werden sie
übereinander angeordnet. `F1 Description` und `Ctrl+Q Quit` bleiben auch im
begrenzten Layout sichtbar.

## Menüs und Tastatur

- `File` öffnet Copy-, Rename-, Delete- und Read-only-Dialoge. Die
  vorbereiteten Confirm-/Cancel-Pfade bleiben für den Nachweis ebenfalls
  vorhanden.
- `Navigate -> First directory` wechselt in das erste kontrollierte
  Unterverzeichnis.
- `View -> Text`, `View -> Hex` und `View -> Associated` verwenden nur
  interne, begrenzte Viewer. Ein unbekannter Dateityp zeigt einen ehrlichen
  Fallback.
- `Search` bietet Filter, Sortierung und die begrenzte Suche.
- `Options` schaltet Markierung und eine geschlossene Palette einschließlich
  `HighContrast`.
- `Help -> Description` oder `F1` erklärt Zweck, Sicherheitsgrenze,
  Modernisierung, Plattformgrenze und Proof.
- Pfeiltasten bewegen die Listenauswahl. `Tab` und `Shift+Tab` wechseln den
  Dialogfokus. `Enter` bestätigt die Default-Schaltfläche, `Escape` bricht
  Dialog oder aktive Mausgeste ab, und `Ctrl+Q` beendet die Anwendung.

## Sichere Dateioperationen

Copy, Rename, Delete und Read-only öffnen echte modale `TDialog`-Instanzen.
Copy und Rename validieren ein root-relatives Ziel; Delete und Read-only
erzeugen keine erfundene Zieleingabe. Vor `OK` existiert keine
Schreibautorität. Nach `OK` erzeugt der kontrollierte Workspace genau ein
typisiertes Intent und prüft Quelle, Ziel und Root-Grenze unmittelbar vor der
Ausführung erneut.

Absolute Pfade, leere Namen, `..`, symbolische Links, Reparse-Punkte,
entfernte Quellen, veränderte Ziele und Overwrite-Konflikte werden sichtbar
abgewiesen. Cancel, Escape und ungültige Eingaben enden mit `NoMutation`.
Löschen bleibt nicht rekursiv; Shells und externe Viewer werden nie gestartet.

## Maus und Tastaturfallback

Eine optionale linke Drag-Folge innerhalb des sichtbaren Hauptfensters darf
nur dieselbe Copy-Absicht wie der Tastaturpfad vorbereiten. MouseUp führt
keine Dateioperation aus. Erst der bestehende Confirm-Pfad besitzt
Schreibautorität.

Ungültige Quelle oder Zielregion, nicht unterstützte Maustasten, Escape,
Capability-Verlust, View-Entfernung und Shutdown beenden die Geste ohne
Mutation. Die vollständige Bedienung bleibt per Tastatur erreichbar.

## Begrenzungen und Plattformen

Alle Pfade sind relativ zu einer expliziten, kanonischen Lernwurzel.
Vorschauen lesen höchstens 4 KiB und 80 Textzeilen. Die Suche ist auf Tiefe 8,
256 geprüfte Dateien und 100 Treffer begrenzt. Pfadvergleich und
Schreibschutz folgen den Fähigkeiten des Betriebssystems; nicht verfügbare
Link- oder Attributfunktionen werden sichtbar klassifiziert und nicht als
erfolgreiche Parität behauptet.

## Historische Einordnung und Modernisierung

Alle 24 Dateien unter `TVFM/` bleiben unveränderte Referenzen. Übernommen
werden Lernzweck und wesentliche Bedienverträge, nicht DOS-Laufwerkszugriff,
globale Pascal-Zustände, binäre Ressourcenformate oder externe
Programmstarts. Der C#-Code nutzt Records, `System.IO`, explizite Zustände und
vorhandene TuiVision-Komponenten. Die moderne Implementierung bleibt
absichtlich keine zeilenweise Pascal-Übersetzung.

## Barrierefreiheit und Proof

Status, Auswahl, Fehler, Fallbacks und Palette werden als Text benannt; Farbe
trägt keine alleinige Bedeutung. Fokusreihenfolge, `F1`, `Enter`, `Escape`,
`Ctrl+Q`, `HighContrast` und das begrenzte Layout besitzen automatisierte
Nachweise.

Die primären Tests führen `app.Run()` aus und prüfen Zustand, persistente
View-Identität, Fokus, StatusLine sowie gerenderte Buffer-/Cell-Inhalte. Die
Evidence schließt exakt zehn Showcase-Bereiche und einen Einstiegspunkt. Alle
24 historischen Quellen werden zusätzlich über SHA-256 gegen Drift geprüft.

---

# TP7 File Manager: Complete Wave-6 Showcase Stage

## Purpose and launch

`Tp7FileManager` carries the learning intent of the historical `TVFM/` file
manager into modern C#. Run it with the normal or `--smoke` command shown
above. Both modes operate only on copied, source-controlled fixtures and use
the real application loop.

## Visible workflow

The first frame keeps a persistent `TP7 TVFM` window, focusable file list,
detail area, and real `TStatusLine`. Six menu groups expose navigation, bounded
text and hex previews, filtering, sorting, tags, search, internal viewer
selection, closed palettes, help, and safe file-operation dialogs. The layout
switches from split to stacked at `48x16` while preserving `F1` and `Ctrl+Q`.

Copy, rename, delete, and read-only changes require a modal decision. Cancel
and invalid input carry no write authority. Confirm creates one typed intent,
then the controlled workspace revalidates source, target, and root boundaries
immediately before execution. There is no shell, external viewer, recursive
delete, silent overwrite, or access to arbitrary user files.

Optional mouse drag only prepares the same confirmable copy intent as the
keyboard path. Release never executes the operation. Escape, capability loss,
view removal, invalid regions, and shutdown remain non-mutating, and every
workflow retains a complete keyboard fallback.

## Modernization and proof

The 24 `TVFM/` files remain read-only intent references. The implementation
preserves the historical learning purpose while using idiomatic C#, explicit
state, and existing TuiVision controls. App-loop tests verify state, view
identity, focus, status, rendered cells, dialogs, constrained layout, safety,
and recovery. Exact evidence validation closes ten showcase areas, one entry
point, and all 24 historical source hashes.
