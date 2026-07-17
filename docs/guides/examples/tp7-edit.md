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

Der echte `TFileEditor` ist im `TEditWindow` fokussiert. File, Edit, Search und
Help stellen ihre aktuell ausführbaren Mnemoniken über die vorhandene
Command-Context-Logik bereit. Modified-State, Safe-Close-Ablehnung, Konflikt
und Traversal-Rejection sind textorientiert. F1 oder
`Help -> Description` erklärt Bedienung, Root-Grenze und Proof-Umfang.

The real `TFileEditor` is focused inside `TEditWindow`. File, Edit, Search,
and Help expose their currently executable mnemonics through the existing
command-context logic. Modified state, safe-close rejection, conflict, and
traversal rejection are text-first. F1 or `Help -> Description` explains
operation, the root boundary, and proof scope.

Der primäre Smoke führt `app.Run()` aus und verbindet Editorzustand, Fokus,
Menü-Shortcuts, kontrollierte Dateientscheidungen, Status und Zellen. Die
enge `48x16`-Ansicht verwendet denselben echten Editor.

The primary smoke runs `app.Run()` and combines editor state, focus, menu
shortcuts, controlled file decisions, status, and cells. The constrained
`48x16` view uses the same real editor.
