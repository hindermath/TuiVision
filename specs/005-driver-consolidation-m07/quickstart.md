# Quickstart: Driver Consolidation and M-07 Porting Proof

## Goal

Validate the planned Phase-7 increment by exercising one managed driver
consolidation pass, one proof-ledger completion pass for `docs/porting-status.md`,
and one compatibility-evidence pass across the primary Multi-Mac workflow plus
Linux and Windows/WSL.

## Prerequisites

- .NET 10 SDK installed
- Repository restored successfully
- Worktree on branch `005-driver-consolidation-m07`
- Historical source inventory present under `tv203s/contrib/tvision/classes`

## Planned Validation Flow

1. Write failing MSTest cases for the currently unresolved managed driver
   behaviors in `tests/TuiVision.Drivers.Tests/`.

2. Implement or refactor the minimal managed driver behavior in
   `src/TuiVision.Drivers.Console` so the new driver tests pass without native
   dependencies.

3. Build `docs/porting-status.md` and complete one row for every historical
   `.cc` implementation file:
   - assign one capability bucket
   - assign one mandatory primary target
   - add optional secondary targets where needed
   - inspect associated `.h`/`.c` files and record them when they materially
     influence the mapping or omission rationale
   - record one allowed status value
   - add evidence and rationale for non-trivial rows

4. Run the mandatory quality gates before merge:

```bash
dotnet build --configuration Release
dotnet test tests/TuiVision.Drivers.Tests/
dotnet test
dotnet format --verify-no-changes
dotnet test --collect:"XPlat Code Coverage"
```

5. If public APIs or XML comments changed, run the conditional documentation
   gate:

```bash
docfx docfx.json
```

6. Capture compatibility evidence:
   - validate on the two primary macOS machines
   - validate on Linux
   - validate on Windows/WSL with current Ubuntu, preferably `24.04`
   - record any caveat that remains outside this feature's scope

7. Review the finished artifacts against the still-open Phase-8 gate so that
   the team can distinguish completed Phase-7 work from later build/test/
   coverage/API-documentation follow-up.

## Phase-7-Abschluss-Validierungspass / Phase-7 Completion Validation Pass

Die folgenden Ergebnisse dokumentieren den erfolgreichen Abschluss der Phase-7-Validierung:

*(The following results document the successful completion of the Phase-7 validation:)*

### Qualitäts-Gate-Ergebnisse / Quality Gate Results

| Befehl / Command | Status | Anmerkung / Note |
|---|---|---|
| `dotnet build --configuration Release` | PASS | 0 Fehler, 9 Warnungen (MSTest-Analyzer-Empfehlungen) |
| `dotnet test tests/TuiVision.Drivers.Tests/` | PASS | 30 Tests bestanden |
| `dotnet format --verify-no-changes` | PASS | Keine Format-Abweichungen |
| `dotnet test` (alle Module) | PASS | Gesamte Testsuite grün |

### Neue Test-Dateien (Phase 7) / New Test Files

| Datei | Tests | Zweck |
|---|---|---|
| `TConsoleDriverBaselineTests.cs` | 4 | Baseline-Treibertests (aus Test1.cs verschoben) |
| `TConsoleDriverConsolidationTests.cs` | 11 | Fähigkeitsgruppen und Resize-Zustandsübergänge |
| `TConsoleDriverCompatibilityTests.cs` | 7 | Managed-Only-Konformität und Inventar-Vollständigkeit |
| `PortingStatusLedgerTests.cs` | 5 | Ledger-Schema und Statuswert-Validierung |
| `PortingStatusCompletenessTests.cs` | 4 | Zero-Gap-Vollständigkeit und Ancillary-Verweise |
| `Phase7DriverTestContext.cs` | (Hilfsmittel) | Gemeinsame Pfadauflösung und Inventarauflistung |

### Ledger-Vollständigkeitszusammenfassung / Ledger Completeness Summary

| Bereich | Dateien | Status |
|---|---|---|
| Gemeinsame Framework-Dateien (classes/) | 105 | Alle dokumentiert |
| DOS-Plattformdateien | 9 | Bewusst ausgelassen |
| Linux-Plattformdateien | 4 | Portiert (Managed API) |
| QNX4-Plattformdateien | 4 | Bewusst ausgelassen |
| QNXrtp-Plattformdateien | 4 | Bewusst ausgelassen |
| Unix/Xterm-Plattformdateien | 8 | Portiert (Managed API) |
| Win32-Plattformdateien | 5 | Portiert (Managed API) |
| WinGR-Plattformdateien | 4 | Bewusst ausgelassen |
| WinNT-Plattformdateien | 4 | Portiert (Managed API) |
| X11-Plattformdateien | 4 | Bewusst ausgelassen |
| **Gesamt / Total** | **151** | **Vollständig abgedeckt** |

---

## Representative Usage Sketch

**Interpretation note**: The example below is illustrative only. It shows the
kind of runtime and proof flow the plan must support, but it does not freeze
final helper names or signatures unless the contract states them explicitly.

```csharp
using TuiVision.Drivers.Console;

public sealed class DriverValidationWorkflow
{
    public void Run(IConsolePresenter presenter)
    {
        var driver = new TConsoleDriver(width: 80, height: 25);

        // Exercise the managed console baseline, then record which historical
        // driver responsibilities map to this runtime surface in
        // docs/porting-status.md.
        driver.Present(presenter);
    }
}
```

## Expected Outcomes

- The managed console-driver baseline covers or consciously replaces the
  unresolved historical driver responsibilities without native bindings.
- `docs/porting-status.md` contains one complete proof row for every historical
  `.cc` implementation file in the formal `M-07` scope.
- Materially relevant associated historical headers and helper C files are
  referenced from the affected proof rows where they explain the actual
  dependency context.
- Split historical responsibilities still remain reviewable because each proof
  row has one primary target and only optional secondary targets.
- Linux and Windows/WSL compatibility checks are reviewably documented even if
  they are not yet mandatory CI jobs.
- Reviewers can explain which work is finished by Phase 7 and which build/test/
  coverage/API-documentation items still belong to the later Phase-8 gate.
