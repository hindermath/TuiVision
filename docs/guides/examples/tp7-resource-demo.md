# TP7 Resource Demo: exakte Ressourcen / Exact Resources

## Zweck / Purpose

`Tp7ResourceDemo` übernimmt den Lernzweck aus `TVDEMOS/TVRDEMO.PAS`: Ein
Dialog, ein Menü und eine Statuszeile werden über benannte Ressourcen
rekonstruiert.

`Tp7ResourceDemo` retains the learning purpose from `TVDEMOS/TVRDEMO.PAS`: a
dialog, menu, and status line are reconstructed from named resources.

## Start / Launch

```bash
dotnet run --project examples/Tp7ResourceDemo
```

Der kontrollierte Entry-Point-Smoke verwendet:

The controlled entry-point smoke uses:

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7ResourceDemo -- --smoke
```

## Ressourcenvertrag / Resource Contract

Die funktionale erste Stufe lädt ausschließlich die exakten,
groß-/kleinschreibungssensitiven Schlüssel `Dialog`, `Menu` und `Status`.
Sichtbarer Zustand wird erst veröffentlicht, wenn alle drei Records vollständig
und typkorrekt gelesen wurden.

The functional first stage loads only the exact, case-sensitive keys `Dialog`,
`Menu`, and `Status`. Visible state is published only after all three records
have been read completely with the expected types.

## Ablehnung und Proof / Rejection and Proof

Doppelte Schlüssel, unbekannte Typen und ungültige Payload-Längen werden ohne
Teilmodell abgelehnt. Der primäre Smoke führt `app.Run()` aus und verbindet
Load-Zustand, sichtbares Fenster und gerenderte Dialog-, Menü- und Statuszellen.

Duplicate keys, unknown types, and invalid payload lengths are rejected without
a partial model. The primary smoke runs `app.Run()` and combines load state,
the visible window, and rendered dialog, menu, and status cells.

## Grenze zur Showcase-Stufe / Showcase-Stage Boundary

Feature 032 liefert die sichere Rekonstruktion. Die Showcase-Stufe ergänzt
sichtbare Resource-Auswahl, vollständige Dialogkomposition, Shortcuts und
`Help -> Description`.

Feature 032 delivers safe reconstruction. The showcase stage adds visible
resource selection, complete dialog composition, shortcuts, and
`Help -> Description`.
