# TP7 Mouse Dialog: lokale Capability / Local Capability

## Zweck / Purpose

`Tp7MouseDialog` übernimmt den Lernzweck aus `TVDEMOS/MOUSEDLG.PAS`:
Doppelklick, Button-Reihenfolge und Capability-Grenzen verständlich machen.

`Tp7MouseDialog` retains the learning purpose from `TVDEMOS/MOUSEDLG.PAS`:
make double click, button order, and capability boundaries understandable.

## Start / Launch

```bash
dotnet run --project examples/Tp7MouseDialog
```

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7MouseDialog -- --smoke
```

## Sicherheits- und Plattformgrenze / Security and Platform Boundary

Doppelklickstufe und Button-Reihenfolge sind nur lokaler Beispielzustand. Das
Beispiel verändert keine Host-Mauseinstellung. `Enabled`, `Disabled` und
`Unsupported` bleiben ehrlich sichtbar; bei Capability-Verlust wird eine
laufende lokale Interaktion beendet.

Double-click step and button order are local example state only. The example
does not change a host mouse setting. `Enabled`, `Disabled`, and `Unsupported`
remain honestly visible; capability loss ends an active local interaction.

## Tastatur und Proof / Keyboard and Proof

Jede Aktivierung besitzt einen vollständigen Tastaturfallback. Der primäre
Smoke führt `app.Run()` aus und prüft unterstützten Doppelklick, Unsupported,
Capability-Verlust, lokale Einstellungen, sichtbare Zellen und
`HostMutationPerformed == false`.

Every activation has a complete keyboard fallback. The primary smoke runs
`app.Run()` and verifies supported double click, Unsupported, capability loss,
local settings, visible cells, and `HostMutationPerformed == false`.

## Showcase-Grenze / Showcase Boundary

Die spätere Stufe ergänzt reale fokussierbare Controls, sichtbare
Capability-Auswahl, Shortcut-Hinweise und `Help -> Description`.

The later stage adds real focusable controls, visible capability selection,
shortcut hints, and `Help -> Description`.
