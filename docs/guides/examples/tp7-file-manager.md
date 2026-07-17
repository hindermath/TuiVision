# TP7 File Manager: kontrollierte funktionale Wave-6-Stufe

## Zweck

`Tp7FileManager` überträgt den Lernzweck des historischen Turbo-Pascal-
Dateimanagers aus `TVFM/` in eine moderne C#-Anwendung. Die erste Wave-6-Stufe
zeigt Navigation, Listen, interne Text- und Hexvorschau, begrenzte Suche sowie
explizit bestätigte Dateioperationen. Sie ist kein Ersatz für Finder, Explorer
oder einen produktiven Dateimanager.

## Start

```bash
dotnet run --project examples/Tp7FileManager
```

Der normale Start kopiert ausschließlich die mitgelieferten Fixtures in ein
temporäres Verzeichnis. Beim Beenden wird dieses Verzeichnis gelöscht.

Der deterministische Smoke-Pfad verwendet dieselbe Anwendungsschleife:

```bash
dotnet run --project examples/Tp7FileManager -- --smoke
```

## Bedienung und aktueller Umfang

- `File -> Exit` beendet die Anwendung.
- `View -> Text` und `View -> Hex` öffnen begrenzte interne Vorschauen.
- `Help -> Description` beziehungsweise `F1` erklärt Lernzweck und Grenze.
- `Ctrl+Q` bleibt der kontrollierte Beendigungspfad.
- Navigation, Filter, Sortierung, Markierung, Suche, Viewerwahl und
  Dateioperationen sind in dieser funktionalen Stufe über echte Commands und
  App-Loop-Smokes bewiesen.

Die vollständige sichtbare Menü- und Dialogführung dieser Funktionen ist ein
belegtes Stage-2-Delta. Sie wird erst in einem getrennten Showcase-Lastenheft
aus dem tatsächlichen Feature-035-Ergebnis geplant.

## Sicherheitsgrenze

Alle Pfade sind relativ zu einer expliziten, kanonischen Lernwurzel. Absolute
Pfade, `..`-Fluchten, symbolische Links und Reparse-Punkte werden abgewiesen.
Vorschauen lesen höchstens 4 KiB und 80 Textzeilen. Die Suche ist auf Tiefe 8,
256 geprüfte Dateien und 100 Treffer begrenzt.

Kopieren, Umbenennen, Löschen und Schreibschutzänderungen gelten nur für
einzelne Dateien. Jede Änderung benötigt ein vom Workspace erzeugtes
Einmal-Intent, eine ausdrückliche Bestätigung und eine erneute Prüfung von
Quelle, Ziel und Root-Grenze. Es gibt keinen Shell-Aufruf, keinen externen
Viewer, kein rekursives Löschen und keinen stillen Overwrite.

## Historische Einordnung

Alle 24 Dateien unter `TVFM/` bleiben unveränderte Referenzen. Übernommen werden
Lernzweck und wesentliche Bedienverträge, nicht DOS-Laufwerkszugriff, globale
Pascal-Zustände, binäre Ressourcenformate oder externe Programmstarts. Der
C#-Code nutzt Records, `System.IO`, explizite Zustände und bestehende
TuiVision-Views.

## Barrierefreiheit und Plattformen

Status und Fehler werden als Text ausgegeben. Alle mutierenden Pfade besitzen
eine Tastaturalternative; Zeigerinteraktion darf dieselbe Bestätigung nicht
umgehen. `HighContrast` ist eine geschlossene, sichtbare Palettenwahl.

Pfadvergleich und Schreibschutz folgen den Fähigkeiten des Betriebssystems.
Nicht verfügbare Link- oder Attributfunktionen werden in Tests sichtbar
klassifiziert und nicht als erfolgreiche Parität behauptet.

---

# TP7 File Manager: Controlled Functional Wave-6 Stage

## Purpose

`Tp7FileManager` carries the learning intent of the historical `TVFM/` file
manager into modern C#. It demonstrates controlled navigation, lists, bounded
internal text and hex previews, search, and explicitly confirmed file
operations. It is not a replacement for a production file manager.

## Launch and proof

Use the commands shown above. Both modes operate only on copied,
source-controlled fixtures. The smoke mode enters the real application loop.
Tests verify state, views, status text, rendered cells, path rejection,
resource limits, one-shot authorization, cancellation, and recovery
boundaries.

The functional contracts are complete, but full visible menu and dialog access
is intentionally deferred to an evidence-derived Stage 2. No later feature is
created or started by Feature 035.
