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

Der reale `TDialog` enthält `TCheckBoxes`, eine `TScrollBar` und einen
Aktivierungsbutton. Tab wechselt Controls, Space dreht die lokale
Button-Reihenfolge, Pfeiltasten ändern die Verzögerungsstufe und Enter
aktiviert den vollständigen Tastaturfallback. F1 öffnet die app-spezifische
Description.

The real `TDialog` contains `TCheckBoxes`, a `TScrollBar`, and an activation
button. Tab changes controls, Space reverses the local button order, arrow keys
change the delay step, and Enter activates the complete keyboard fallback. F1
opens the app-specific Description.

Der primäre Smoke führt `app.Run()` aus und prüft Controls, Fokus,
unterstützten Doppelklick, Unsupported, Capability-Verlust, lokale
Einstellungen, Status und sichtbare Zellen. `HostMutationPerformed == false`
bleibt dabei eine verbindliche Grenze.

The primary smoke runs `app.Run()` and verifies controls, focus, supported
double click, Unsupported, capability loss, local settings, status, and
visible cells. `HostMutationPerformed == false` remains a binding boundary.

Die `46x16`-Fixture beweist Dialogidentität, Tastaturhinweis und Description
ohne eine Host-Konfiguration zu verändern.

The `46x16` fixture proves dialog identity, keyboard guidance, and Description
without changing host configuration.
