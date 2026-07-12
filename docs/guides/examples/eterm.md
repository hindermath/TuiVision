# ETerm Beispiel / ETerm Example

## Deutsch

`ETerm` zeigt ausgewählte historische Menü-, Theme- und
Präsentationswerte als unveränderliches, typisiertes Manifest. Start:

```bash
dotnet run --project examples/ETerm
```

Die Hauptfläche nennt Schlüssel, Wert, Kategorie und historische Quelle.
`Nächster Eintrag` ändert die Auswahl; die Statuszeile nennt den aktiven
Schlüssel. `Help -> Description` erklärt die Grenze.

Die Demo parst weder `menus.cfg` noch `theme.cfg`. Sie führt insbesondere keine
historischen `spawn`, `save`, `exit`- oder Theme-Aktionen aus. Fehlende oder
nicht belegte Einträge erscheinen als textorientierter `Unsupported`-Fallback.

Damit bleibt der historische Zweck sichtbar, ohne eine native ETerm-Capability
vorzutäuschen. Alle Hauptpfade sind per Tastatur erreichbar und nicht nur durch
Farbe unterscheidbar.

Host-Nachweis: Der automatische Pfad beweist nur das In-Process-Manifest.
Physische ETerm- oder Theme-Beobachtung wird nicht behauptet.

## English

`ETerm` shows selected historical menu, theme, and presentation values as an
immutable typed manifest. Launch it with:

```bash
dotnet run --project examples/ETerm
```

The main area names each key, value, category, and historical source. `Next
entry` changes the selection, and the status line names the active key. `Help ->
Description` explains the boundary.

The demo parses neither `menus.cfg` nor `theme.cfg`. In particular, it executes
none of the historical `spawn`, `save`, `exit`, or theme actions. Missing or
unproven entries appear as a text-first `Unsupported` fallback.

This keeps the historical purpose visible without claiming a native ETerm
capability. All primary paths are keyboard reachable and do not rely on color.

Host evidence: The automated path proves only the in-process manifest. It does
not claim physical ETerm or theme observation.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~ETermSmokeTests"
```
