# Ausführungs- und Proof-Matrix / Execution and Proof Matrix

## Proof-Stufen / Proof Levels

- `StaticVerified`: Konfiguration, Version oder Pfad wurde gelesen.
- `PracticallyVerified`: Der konkrete Befehl lief in der genannten Umgebung.
- `PlatformVerified`: Der Befehl lief auf der ausdrücklich genannten Plattform.
- `NotVerified`: Es liegt kein ausreichender Laufnachweis vor.

*Static evidence reads configuration. Practical evidence runs the command in
the named environment. Platform evidence additionally names the actual
platform. `NotVerified` is not a pass.*

## Matrix

| Prüfung / Check | Ort / Location | Aktueller Proof / Current proof | Grenze / Boundary |
|---|---|---|---|
| Build | Sandbox | `StaticVerified` | .NET 10 vorhanden; kein frischer Image-Lauf durch Feature 044 |
| Test | Sandbox | `StaticVerified` | strukturell möglich; kein aktueller Sandbox-Testclaim |
| Format | Sandbox | `StaticVerified` | SDK-Befehl vorhanden; kein aktueller Sandbox-Laufclaim |
| DocFX | CI | `PracticallyVerified` | GitHub Actions installiert DocFX; nicht als Image-Tool behauptet |
| Playwright/Axe | CI | `PracticallyVerified` | System-Chrome im Pages-Job; nicht als generische Image-Fähigkeit behauptet |
| Dependency/SBOM | CI | `PracticallyVerified` | temporäre CycloneDX-SBOM; kein permanenter VEX- oder Releaseclaim |
| Secret-Scan | CI | `PracticallyVerified` | Repository-Inhalte; keine Garantie gegen manuelle Prompt-Offenlegung |
| Agent-Parität | Local host | `PracticallyVerified` | Oberflächenparität; keine Provideranmeldung oder Modellfreigabe |

*Build, test, and format are statically feasible in the image. Documentation,
accessibility, dependency, SBOM, secret, and parity claims remain bound to the
environment where their commands actually execute.*

## Nicht zulässige Operationen / Not Permitted Operations

- Secrets oder lokale Profile in das Repository kopieren;
- den gesamten persönlichen Projekt- oder Home-Root mounten;
- das externe Sandbox-Repository aus diesem Feature ändern;
- aus einem statischen Pass eine Human-, Provider- oder Plattformfreigabe
  ableiten;
- generierte SBOM-, DocFX-, Test- oder Scan-Ausgabe committen.

*Do not copy secrets or profiles, mount broad personal roots, modify the
external sandbox repository, turn static evidence into approval, or commit
generated validation output.*
