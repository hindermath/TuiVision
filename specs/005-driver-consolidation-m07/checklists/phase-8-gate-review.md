# Phase-7 vs. Phase-8 Gate-Trennung / Gate Separation Review Checklist

**Feature**: `005-driver-consolidation-m07` | **Datum / Date**: 2026-03-23

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

## Noch offene Phase-8-Eingangstor-Punkte / Open Phase-8 Entrance Gate Items

Diese Punkte sind **nicht** Teil von Phase 7. Sie müssen vor Beginn der Beispielportierungen (Phase 8) geschlossen sein.

*(These items are NOT part of Phase 7. They must be closed before beginning example porting (Phase 8).)*

- [ ] `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Serialization`, `TuiVision.Compatibility` und `TuiVision.Drivers.Console` Testabdeckung jeweils ≥ 70 % Line Coverage (SC-003 Pflichtenheft §9.4 Nr. 1).
- [ ] Vollständige `dotnet test` für alle Module (nicht nur Treiber-Tests) ohne Fehler.
- [ ] `dotnet format --verify-no-changes` ohne Abweichungen.
- [ ] Alle Ledger-Zeilen mit Status `portiert + Test ausstehend` haben entsprechende MSTest-Coverage in einem der Testprojekte.
- [ ] `docfx docfx.json` bei API-/XML-Kommentar-Änderungen in Phase-8-Scope erfolgreich ausgeführt.
- [ ] M-07-Vollbeweis: alle 151 `.cc`-Zeilen mit `portiert + getestet` ODER `bewusst ausgelassen + Begruendung` (aktuell viele auf `portiert + Test ausstehend`).
- [ ] Beispielportierungen Wellen 1–4 beginnen erst nach bestandenem Eingangstor.
- [ ] Linux- und Windows/WSL-Kompatibilitätsnachweise als CI-Gate etabliert (optional für Phase 8, empfohlen).

---

## Grenzlinienklärung / Boundary Clarification

| Aspekt | Phase 7 (dieser Branch) | Phase 8 (folgender Branch) |
|---|---|---|
| Managed Driver Baseline | ✓ Abgeschlossen | Beibehalten |
| M-07 Proof Ledger | ✓ Vollständig | Statuswerte aktualisieren wenn Tests folgen |
| Fähigkeitsgruppen-Dokumentation | ✓ Vollständig | Beibehalten |
| Kompatibilitätsnachweis (macOS) | ✓ PASS | Fortlaufend |
| Kompatibilitätsnachweis (Linux/WSL) | ✓ Manuell dokumentiert | Als CI-Gate etablieren |
| Core-/Controls-/Serialization-/Compatibility-/Drivers.Console-Testabdeckung je ≥ 70 % | Ausstehend — Phase-8-Gate | Schließen vor Beispielbeginn |
| Beispielportierungen (25 MUSS) | Nicht begonnen | Phase 8 nach Gate-Bestehen |
| Vollständige `dotnet test` Coverage | Phase-7-Treibertests abgedeckt | Alle Module nach Phase 8 |
