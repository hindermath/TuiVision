# Qualitaetsszenarien Welle 2 / Quality Scenarios Wave 2

## Deterministische In-Process-Smoke-Interaktion

Ein Smoke-Test erstellt die Beispielanwendung mit `headless: true`, fuehrt
mindestens eine beispielspezifische Aktion aus und beendet die Anwendung ueber
den `GetEvent()`-Quit-Pfad. Startup plus sauberer Exit zaehlt nicht allein.

A smoke test creates the example application with `headless: true`, performs at
least one example-specific action, and exits through the `GetEvent()` quit path.
Startup plus clean exit alone does not count.

012-Ergebnis: Die primaeren Wave-2-Smokes injizieren lokale Command-Events in
die App-Queue, lassen `app.Run()` dispatchen und pruefen danach
`VisibleHistory`. Direkte Hilfsmethoden werden ueber `DirectHelperUsage`
klassifiziert.

012 outcome: The primary Wave 2 smokes inject local command events into the app
queue, let `app.Run()` dispatch them, and then verify `VisibleHistory`. Direct
helpers are classified through `DirectHelperUsage`.

## Text-First Und Keyboard-First

Jede pruefbare Interaktion liefert textorientierte sichtbare Zustandswerte.
Farbe, Layout, Maus oder Host-spezifische Terminaleffekte duerfen nicht die
einzige Informationsquelle sein.

Every testable interaction returns text-first visible state. Color, layout,
mouse input, or host-specific terminal effects must not be the only source of
information.

## Kein Dateiinhalt-I/O In Standarddialogen

`demo` und `dlgdsn` duerfen lokale Dateisystem-Metadaten, Wildcards, manuelle
Pfade, Abbruch und ungueltige Pfade zeigen. Sie duerfen innerhalb der
Standarddialog-Akzeptanz keine Datei lesen, schreiben, speichern, loeschen oder
ueberschreiben.

`demo` and `dlgdsn` may show local file-system metadata, wildcards, manual
paths, cancel, and invalid paths. Inside standard-dialog acceptance they must
not read, write, save, delete, or overwrite file contents.

## Coverage-Konvention Fuer Zukuenftige CI

Wenn die Repository-CI spaeter Coverage misst, MUSS der Aufruf aus dem
Repository-Root `--collect:"XPlat Code Coverage" --settings coverlet.runsettings`
enthalten. Ohne das Argument `--settings` werden die Include/Exclude-Filter in
`coverlet.runsettings` ignoriert und das `>=70%`-Gate pro Pflicht-Assembly ist
ungueltig.

If repository CI is later extended to measure coverage, the invocation MUST
include `--collect:"XPlat Code Coverage" --settings coverlet.runsettings`, run
from the repository root. Without the `--settings` argument the Include/Exclude
filters in `coverlet.runsettings` are ignored and the `>=70%` per-required-
assembly gate is invalid.

## DocFX-Auswirkung

011 fuegte mit `TScrollGroup` eine neue oeffentliche Controls-Oberflaeche
hinzu. 012 aendert Guides und Nachweisflaechen, aber keine neue oeffentliche
Framework-API. DocFX ist wegen der Dokumentationsaenderungen trotzdem nach der
finalen Validierung zu regenerieren und mit dem vorhandenen Playwright/axe-
Smoke-Test zu pruefen.

011 added `TScrollGroup` as a new public Controls surface. 012 changes guides
and evidence surfaces, but no new public framework API. DocFX still has to be
regenerated after final validation because documentation changed, and it must
be checked with the existing Playwright/axe smoke test.

## 013 Qualitaetsszenarien / 013 Quality Scenarios

Deutsch: 013 schaerft die Akzeptanzqualitaet fuer Welle 2. Jeder primaere
Smoke muss konkrete Zustandswerte, View-Baum-Nachweis und Buffer-/Cell-Region
kombinieren. `VisibleText` und `VisibleHistory` sind nur noch Ergaenzung. Da
Guides und README geaendert werden, bleiben DocFX und Playwright/axe fuer die
finale Validierung erforderlich.

English: 013 raises the acceptance quality for Wave 2. Each primary smoke must
combine concrete state values, view-tree proof, and buffer/cell region.
`VisibleText` and `VisibleHistory` are supplemental only. Because guides and
README change, DocFX and Playwright/axe remain required for final validation.
