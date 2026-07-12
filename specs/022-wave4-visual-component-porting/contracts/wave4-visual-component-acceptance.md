# Acceptance Contract: Wave-4 Visual Component Porting

## Scope

The contract covers exactly `Cyrillic`, `ETerm`, `Fonts`, `Terminal`, and
`XTerm`. Feature 021 is the reusable baseline. Full emulation, host processes,
host mutation, legacy parser compatibility, Feature 023, and TP7/Wave-5 work are excluded.

## Common Visual Contract

Every example must expose:

1. a visible domain main composition or stable visible fallback,
2. dynamic text-first status,
3. keyboard-reachable bilingual description,
4. at least one real operation through application dispatch,
5. one primary app-loop proof with concrete state, exact view identity, and cells.

Startup-only, helper-only, status-only, explanation-only, screenshot-only, or
host-rendering-only evidence is not primary proof.

## Example Contracts

| Example | Required visible contract | Required negative/fallback contract |
|---|---|---|
| Terminal | Session view, text, cursor/attribute action, profile/capability status, description, quit | Rejected/unsupported action is atomic, next input works, unavailable capability remains visible |
| Cyrillic | Labeled KOI8-R/Unicode grid, source values, glyphs, outcomes | Replaced, invalid, and unsupported outcomes agree in state/status/cells |
| Fonts | Exact fixture metadata and recognizable 8x16 glyph raster | At least four shape/source/format/blank or unsupported fallback classes |
| ETerm | At least three immutable menu/theme/presentation entries with source identity | Out-of-subset entry is visibly unsupported; no general parser claim |
| XTerm | At least three immutable resource/sequence/capability entries with source identity | Native resource/host integration remains visibly unsupported |

## Asset and Host Contract

- Assets are embedded or project-owned source-controlled read-only files.
- No arbitrary user-path read/write, process, shell, PTY, command execution,
  audio, font/codepage/keyboard/terminal mutation, or generator execution is allowed.
- Host rows separate deterministic, remote CI, and physical observation.
- An unavailable physical condition is `NotRun` with risk and trigger.

## Historical Contract

Relevant implementation, header, config, resource, README, setup script, and
fixture metadata under `tv203s/` are reviewed read-only. Each example records
retained intent and material deviation. ETerm/XTerm native syntax omission is
an explicit `IntentionalDeviation` unless a different accepted framework
decision is proven during planning review.

## Framework Gate

Each example receives exactly one primary decision:

| Decision | Meaning |
|---|---|
| `UseExistingFramework` | Existing contracts and composition satisfy acceptance |
| `SmallFrameworkFix` | One narrow reusable gap is fixed with focused tests |
| `IntentionalDeviation` | Historical behavior is deliberately represented differently |
| `FollowUpHardening` | A real issue lies outside 022 and is not implemented here |

Reusable terminal, mapping, font, profile, host, or proof behavior may not be
duplicated in example-local code.

## Evidence Contract

`pr-evidence.md` contains one row per example plus host, governance,
validation, generated-output, archive, and remote-delivery tables. Every remote
task names that path or the pre-named `closeout-evidence.md` path.

## Validation Contract

Triggered gates are static diff/scope/secret checks, targeted Wave-4 smokes,
full Release tests, canonical coverage, format, DocFX, Playwright/axe, UTF-8
text-browser review, generated-output cleanup, remote checks, thread-aware
review convergence, authorized merge, and local-main synchronization.
