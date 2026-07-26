# TuiVision Requirements Hub / Anforderungszentrale

Diese Struktur trennt die dauerhafte Produktbaseline von der operativen
Spec-Kit-Abarbeitung. `Pflichtenheft.md` im Repository-Root ist der kurze
Einstieg. Die unveränderte historische Vollfassung liegt unter `baseline/`.

*This structure separates the durable product baseline from operational
Spec Kit delivery. Root `Pflichtenheft.md` is the short entry point. The
unchanged historical full version is stored under `baseline/`.*

## Bereiche / Areas

- `baseline/`: unveränderte historische Baselines.
- `intakes/active/`: authorisierte, reviewbare Lastenhefte.
- `intakes/archive/`: abgeschlossene Feature-Intakes.
- `intakes/backlog/`: nicht blockierende optionale Anforderungen.
- `intakes/series/`: maschinenlesbare Reihenfolge, Lifecycle und Abhängigkeiten.
- `traceability/`: Verweise auf Coverage-, Review- und Migrationsnachweise.

## Verbindliche Pflege / Binding Maintenance

Aktive Intakes werden nur mit `$speckit-intake-create` oder
`$speckit-intake-update` verändert. Reihenfolge und Lifecycle werden nur über
die Intake-Serie gepflegt. Root-Indizes werden aus dem kanonischen Modell
erzeugt und nicht als unabhängige Projektstatusquelle bearbeitet.

*Active intakes change only through `$speckit-intake-create` or
`$speckit-intake-update`. Order and lifecycle change only through the intake
series. Root indexes are generated from the canonical model and are not an
independent project-status source.*
