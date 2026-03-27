# Phase-7 vs. Phase-8 Gate-Trennung / Gate Separation Review Checklist

**Feature**: `005-driver-consolidation-m07` | **Datum / Date**: 2026-03-27

Diese Checkliste stellt sicher, dass Phase-7-Arbeit von den verbleibenden Phase-8-Eingangstor-Anforderungen
klar getrennt ist, bevor der Branch gemergt wird.

*(This checklist ensures Phase-7 work is clearly separated from the remaining Phase-8 entrance gate
requirements before the branch is merged.)*

---

## Phase-7-Abschluss-Nachweise / Phase-7 Completion Evidence

- [X] `docs/porting-status.md` existiert und deckt jede historische `.cc`-Datei ab (151 Dateien).
- [X] Jede Ledger-Zeile hat genau einen Primärzielwert.
- [X] Jede Ledger-Zeile trägt einen der drei erlaubten Statuswerte.
- [X] Jede `bewusst ausgelassen + Begruendung`-Zeile hat eine nicht-leere Begründungsnotiz.
- [X] Zugehörige Hilfsdateien `dos/vgastate.h`, `dos/vgastate.c`, `dos/vgaregs.h`, `dos/vgaregs.c` sind im Ledger referenziert.
- [X] `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs` definiert alle 5 Fähigkeitsgruppen.
- [X] `DriverCapabilityMap.AllBuckets` enthält genau 5 Einträge.
- [X] Jede Fähigkeitsgruppe hat eine nicht-leere Beschreibung des verwalteten Ersatzes.
- [X] `src/TuiVision.Drivers.Console/Class1.cs` ist kein Catch-all mehr (Kommentar-Stub).
- [X] `tests/TuiVision.Drivers.Tests/TConsoleDriverBaselineTests.cs` enthält die Baseline-Tests.
- [X] `tests/TuiVision.Drivers.Tests/TConsoleDriverConsolidationTests.cs` deckt Fähigkeitsgruppen und Resize ab.
- [X] `tests/TuiVision.Drivers.Tests/TConsoleDriverCompatibilityTests.cs` deckt Managed-Only-Konformität ab.
- [X] `tests/TuiVision.Drivers.Tests/PortingStatusLedgerTests.cs` validiert Ledger-Schema und -Statuswerte.
- [X] `tests/TuiVision.Drivers.Tests/PortingStatusCompletenessTests.cs` validiert Vollständigkeit.
- [X] `dotnet test tests/TuiVision.Drivers.Tests/` — alle Tests bestehen (PASS).
- [X] `dotnet build --configuration Release` — Build erfolgreich (PASS).
- [X] `docs/guides/multi-mac-workflow.md` enthält den Phase-7-Kompatibilitätsnachweis-Abschnitt.
- [X] Kompatibilitätsnachweis für MacBook Air M2 dokumentiert.
- [X] Kompatibilitätsnachweis für Mac mini M4 Pro dokumentiert.
- [X] Kompatibilitätsnachweis für Linux (Ubuntu 24.04 / WSL) dokumentiert (manuell).
- [X] Kompatibilitätsnachweis für Windows/WSL dokumentiert (manuell).
- [X] `Pflichtenheft.md` — `>>> NAECHSTER SCHRITT <<<`-Marker zeigt auf nächsten offenen Schritt.
- [X] Offene Phase-8-Eingangstor-Punkte in `docs/porting-status.md` aufgelistet.

---

## Phase-8-Eingangstor-Abschlussstand / Phase-8 Entrance Gate Closure State

Diese Punkte sind in Feature `006-close-phase8-gate` geschlossen worden und
bilden jetzt den reviewbaren Eingangstor-Nachweis vor Welle 1.

*(These items were closed in feature `006-close-phase8-gate` and now form the
reviewable entrance-gate evidence ahead of wave 1.)*

- [X] `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` erreichen jeweils ≥ 70 % Line Coverage (`89.11 %`, `84.10 %`, `83.33 %`, `80.95 %`, `97.43 %`).
- [X] Vollständige `dotnet test` für alle Module läuft ohne Fehler.
- [X] `dotnet format --verify-no-changes` läuft ohne Abweichungen.
- [X] Alle Ledger-Zeilen mit Status `portiert + Test ausstehend` wurden eliminiert; keine provisorische `.cc`-Zeile verbleibt.
- [X] `docfx docfx.json` wurde wegen API-/XML-Kommentar-Änderungen im 006-Scope erfolgreich ausgeführt.
- [X] M-07-Vollbeweis: alle 151 `.cc`-Zeilen enden mit `portiert + getestet` ODER `bewusst ausgelassen + Begruendung`.
- [X] Beispielportierungen Wellen 1–4 bleiben bis zum dedizierten Closure-Commit blockiert und sind danach freigegeben.
- [X] Linux- und Windows/WSL-Kompatibilitätsnachweise bleiben für Phase 8 reviewbar dokumentiert; für die reinen Gate-Abschlussaufgaben `T022` bis `T034` war kein zusätzlicher Plattform-Rerun erforderlich.

**Closure-Commit-Referenz / Closure Commit Reference**:
`docs: close phase-8 entrance gate for feature 006`

---

## Grenzlinienklärung / Boundary Clarification

| Aspekt | Phase 7 (dieser Branch) | Phase 8 (folgender Branch) |
|---|---|---|
| Managed Driver Baseline | ✓ Abgeschlossen | Beibehalten |
| M-07 Proof Ledger | ✓ Vollständig | ✓ Final geschlossen |
| Fähigkeitsgruppen-Dokumentation | ✓ Vollständig | Beibehalten |
| Kompatibilitätsnachweis (macOS) | ✓ PASS | ✓ Gate-Paket verwendet denselben Multi-Mac-Baseline-Workflow |
| Kompatibilitätsnachweis (Linux/WSL) | ✓ Manuell dokumentiert | ✓ Vorhandene Evidence bleibt gültig; kein zusätzlicher Rerun für reine Gate-Abschlussarbeit |
| Core-/Controls-/Serialization-/Compatibility-/Drivers.Console-Testabdeckung je ≥ 70 % | Ausstehend — Phase-8-Gate | ✓ Geschlossen vor Beispielbeginn |
| Beispielportierungen (25 MUSS) | Nicht begonnen | Welle 1 darf nach Closure-Commit beginnen |
| Vollständige `dotnet test` Coverage | Phase-7-Treibertests abgedeckt | ✓ Alle Module im Closure-Paket verifiziert |
