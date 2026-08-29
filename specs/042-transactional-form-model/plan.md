# Implementation Plan: Transactional Form Model

## Technical context

- .NET 10 / C#; `System.Text.Json`; keine neue Dependency.
- Kern- und UI-Integration in `TuiVision.Controls`; persistierte semantische
  Records und geschlossener JSON-Codec in `TuiVision.Serialization`.
- MSTest in Controls-, Serialization- und Examples-Smoke-Suites.
- Beispielprojekt `examples/FormTransaction` mit eingebettetem JSON.

## Source review and disposition

1. Aktueller Vertrag: reviewter Intake und Issue #154.
2. Magiblot-Pin `57b6f56b38e0ee75240a80a10ee0e11470c24693`, Tree
   `96dd03873955689ff0a79f6c8107a8148fe1ebd6`: `tdialog.cpp`,
   `tinputli.cpp`, `dialogs.h`, `views.h` bestätigen additive Dialog-, Input-,
   Daten- und Validierungsverantwortung ohne Formtransaktion.
3. Historisch: `tv203s/contrib/tvision/classes/tdialog.cc`, `tinputli.cc` sowie
   `include/tv/dialog.h`, `inputln.h`, `dialogs.h`, `views.h` bestätigen
   Command-Abschluss, Cancel-Bypass, Feldtransfer und Validierung.
4. Entscheidung: `IntentionalTuiVisionDeviation` für die neue Sessionsemantik,
   `PreserveHistoricalIntent` für Event/Dialog/Input-Verhalten und
   `AdoptModernization` für Komposition, typed snapshots und sichere Registry.

## Architecture

1. `FormField<T>` kapselt Wert/Baseline/Revision, Validatoren und optionales
   typed Binding.
2. `FormSession` friert einen rekursiven Snapshot ein, serialisiert Submit-
   Aufrufe und veröffentlicht Ergebnisse nur ohne Drift.
3. Accept erfasst Binding-Ursprungswerte, wendet Setter stabil an, rollt
   Fehler rückwärts zurück und setzt Baselines erst nach vollständigem Apply.
4. `FormInputLineAdapter` synchronisiert ordinary `TInputLine` nur bei
   expliziter Session-Teilnahme.
5. Serialization besitzt reine Semantic Records plus strikten JSON-Codec;
   Controls prüft alle Schlüssel gegen die trusted Runtime-Registry.

## Security and A11Y

JSON ist eine Trust Boundary mit 256-KiB-, 32-Tiefen- und 4096-Item-Grenzen,
geschlossenen Properties, strikten Registry-Schlüsseln und atomarem Ergebnis.
Keine Runtime-AI-, Cloud-, Service-, Auth- oder Dependency-Grenze entsteht.
Das Beispiel und der Guide sind DE-first/EN-second, CEFR-B2, text-first und
belegen Status, Hilfe und sichtbare Cells ohne Farbbedeutung.

## Validation sequence

Targeted red/green tests → Release-Build → full tests → Coverlet five-assembly
gate → format → policy/series/review/state validators in Bash/PowerShell →
DocFX → Playwright/Axe → Lynx → final scope/statistics evidence.
