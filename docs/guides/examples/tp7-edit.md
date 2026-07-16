# TP7 Edit: kontrollierter Editor / Controlled Editor

## Zweck / Purpose

`Tp7Edit` übernimmt den Zweck von `TVDEMOS/TVEDIT.PAS` über die vorhandenen
`TFileEditor`- und `TEditWindow`-Verträge. Änderungen, Safe-Close und
Speicherkonflikte bleiben sichtbar.

`Tp7Edit` retains the purpose of `TVDEMOS/TVEDIT.PAS` through the existing
`TFileEditor` and `TEditWindow` contracts. Modifications, safe close, and save
conflicts remain visible.

## Start und Datei-Grenze / Launch and File Boundary

```bash
dotnet run --project examples/Tp7Edit
```

Der normale Start verwendet einen eingebetteten Lernpuffer. Schreibende
Proofs erhalten ausdrücklich ein test-eigenes Root. Relative Pfade dürfen
diesen Root nicht verlassen; bestehende Ziele oder externe Änderungen
benötigen eine ausdrückliche Konfliktentscheidung.

Normal launch uses an embedded learner buffer. Write proofs receive an
explicit test-owned root. Relative paths must not leave that root; existing
targets or external changes require an explicit conflict decision.

## A11Y und Proof / A11Y and Proof

Der echte Editor erhält Tastaturereignisse über `app.Run()`. Modified-State,
Safe-Close-Ablehnung, Konflikt und Traversal-Rejection sind textorientiert.
Stage 2 ergänzt vollständige Datei-Menüs, Dialoge, Shortcut-Hinweise und
`Help -> Description`.

The real editor receives keyboard events through `app.Run()`. Modified state,
safe-close rejection, conflict, and traversal rejection are text-first. Stage
2 adds complete file menus, dialogs, shortcut hints, and
`Help -> Description`.
