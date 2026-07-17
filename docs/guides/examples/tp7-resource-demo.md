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

Die Anwendung lädt ausschließlich die exakten,
groß-/kleinschreibungssensitiven Schlüssel `Dialog`, `Menu` und `Status`.
Sichtbarer Zustand wird erst veröffentlicht, wenn alle drei Records vollständig
und typkorrekt gelesen wurden.

The application loads only the exact, case-sensitive keys `Dialog`,
`Menu`, and `Status`. Visible state is published only after all three records
have been read completely with the expected types.

## Ablehnung und Proof / Rejection and Proof

Doppelte Schlüssel, unbekannte Typen und ungültige Payload-Längen werden ohne
Teilmodell abgelehnt. Nach erfolgreicher Auflösung zeigt ein reales `TDialog`
die Dialog-, Menü- und Statuswerte mit fokussierbarem Select-Button. Tab,
Enter und F1 beziehungsweise `Help -> Description` bleiben textorientiert
erreichbar.

Duplicate keys, unknown types, and invalid payload lengths are rejected without
a partial model. After successful resolution, a real `TDialog` shows the
dialog, menu, and status values with a focusable Select button. Tab, Enter,
and F1 or `Help -> Description` remain text-first and reachable.

Der primäre Smoke führt `app.Run()` aus und verbindet atomaren Load-Zustand,
`TDialog`-/`TButton`-Fokus, echte Statuszeile, Description und gerenderte
Zellen in normaler sowie `48x16`-Ansicht.

The primary smoke runs `app.Run()` and combines atomic load state,
`TDialog`/`TButton` focus, a real status line, Description, and rendered cells
in normal and `48x16` views.
