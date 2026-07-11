# Lieferketten-Evidenz / Supply-Chain Evidence

**Stand / Current as of**: 2026-07-11
**Scope**: TuiVision source, NuGet/npm tools, GitHub Actions, release readiness

## CycloneDX-SBOM

CycloneDX for .NET 6.2.0 ist in `.config/dotnet-tools.json` gepinnt. Eine BOM
wird aus `TuiVision.sln` in einem temporären Verzeichnis erzeugt und danach
gelöscht.

*CycloneDX for .NET 6.2.0 is pinned in `.config/dotnet-tools.json`. A BOM is
generated from `TuiVision.sln` in a temporary directory and then deleted.*

```bash
dotnet tool restore
sbom_dir="$(mktemp -d)"
dotnet tool run dotnet-CycloneDX -- \
  TuiVision.sln \
  --output "$sbom_dir" \
  --output-format Json \
  --spec-version 1.7 \
  --configuration Release
jq -e '.bomFormat == "CycloneDX" and .specVersion == "1.7" and (.components | length > 0)' \
  "$sbom_dir/bom.json"
rm -rf "$sbom_dir"
```

Observed result: CycloneDX 1.7 JSON, metadata component `TuiVision`, 21
components, 22 dependency nodes, zero tracked BOM files.

## Statusübersicht / Status Overview

| Control | Status | Evidence and boundary |
|---|---|---|
| Dependency vulnerability/deprecation | PASS at 2026-07-11 | No vulnerable/deprecated direct or transitive package reported; see `dependency-audit.md` |
| SBOM | `Applicable`, PASS | Reproducible local tool and CI command; generated output untracked |
| VEX | `N/A` | No known vulnerability in evaluated/shipped components; trigger on any vulnerability finding |
| SLSA provenance | `FollowUp` | Current release-please workflow does not emit attestable package provenance; do not fabricate it |
| Reproducible build | `FollowUp` | Deterministic build evidence is not yet a formal release contract |
| NuGet lock policy | `FollowUp` | npm has a lock file; NuGet lock-mode policy is not established |
| Verified registries | `AlreadySatisfied` with boundary | NuGet/npm/GitHub sources are standard; local source details are not copied into Git |
| Automated updates | `Applicable`, PASS | Dependabot covers NuGet, GitHub Actions, and `tests/web-a11y` npm |
| CVE monitoring | `Applicable`, PASS | Local commands and `security-supply-chain.yml`; provider alerts remain human-owned |
| OpenSSF Scorecard | `Applicable`, partial | Public API returned no indexed result at review; publication/provider settings remain Human-only `Open` |
| GitHub Actions integrity | PASS | Every `uses:` reference is a full immutable SHA with readable alias comment |
| Build secrets | PASS with provider boundary | No new secrets; Gitleaks/agent scans remain gates; provider secret values are not evidence |
| AI-SBOM | `N/A` | AI is development tooling only; trigger on delivered models/services/datasets/inference assets |

## CI-Nachweis / CI Evidence

`.github/workflows/security-supply-chain.yml` restores the solution and local
tool, checks vulnerable/deprecated packages, generates the BOM under `mktemp`,
validates JSON with `jq`, and removes output through a trap. It uses read-only
repository permissions and immutable Actions.

## Release- und Retention-Grenze / Release and Retention Boundary

Feature 016 creates no new release artifact or legal conformity statement.
Generated BOMs may be CI/release artifacts in a future release design but are
not source files. Re-evaluate VEX, SLSA, Scorecard publication, licence report,
and artifact hashes when a distributable release pipeline is hardened.
