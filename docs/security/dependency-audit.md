# Abhängigkeits-Audit / Dependency Audit

**Stand / Current as of**: 2026-07-11
**Scope**: `TuiVision.sln`, direct and transitive NuGet packages
**Toolchain**: .NET SDK 10.0.301

## Befehle / Commands

```bash
dotnet list TuiVision.sln package --vulnerable --include-transitive
dotnet list TuiVision.sln package --deprecated --include-transitive
dotnet list TuiVision.sln package --outdated --include-transitive
```

## Ergebnis / Result

| Review | Ergebnis / Result | Entscheidung / Decision |
|---|---|---|
| Vulnerable packages | Keine gemeldet / None reported | PASS at evidence date |
| Deprecated packages | Keine gemeldet / None reported | PASS at evidence date |
| Outdated production/example packages | Keine gemeldet / None reported | No change |
| Outdated test tooling | MSTest 4.0.1 -> 4.3.0; coverlet.collector 6.0.4 -> 10.0.1 available | Unchanged: no vulnerability, deprecation, compatibility, or feature need |
| Transitive test-tool updates | Newer versions available | Inherited from test tooling; review through Dependabot |

Die Prüfung verwendete konfigurierte Paketquellen. Source-URLs und mögliche
lokale Authentifizierungsbestandteile werden bewusst nicht in Evidence
übernommen. Ein sauberer Lauf beweist nur den Stand und die erreichbaren
Advisory-Daten am Prüfdatum.

*The review used configured package sources. Source URLs and possible local
authentication material are deliberately not copied into evidence. A clean run
proves only the resolved graph and reachable advisory data at the review date.*

## Update- und Failure-Grenze / Update and Failure Boundary

- `.github/dependabot.yml` proposes bounded NuGet, Actions, and npm updates.
- Package versions change only for a concrete vulnerability, deprecation,
  compatibility need, or separately approved maintenance decision.
- An unavailable feed/advisory service is recorded as a failed/blocked run, not
  converted to `N/A`.
- Re-evaluate on every dependency/tool manifest change and before release.
