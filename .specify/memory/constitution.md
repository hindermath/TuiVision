<!--
Sync Impact Report
- Version change: 1.10.1 -> 1.11.0
- Bump rationale:
  - MINOR: Added workspace-baseline alignment guidance from the repository root `constitution.md` without removing repository-specific principles.
- Modified principles:
  - None
- Added sections:
  - Workspace Baseline Alignment / Observability & Continuous Measurement
  - Workspace Baseline Alignment / Programmierung #include<everyone> — Inclusion & Accessibility By Default
  - Workspace Baseline Alignment / Runtime Guidance References
- Removed sections:
  - None
- Templates requiring updates:
  - .specify/templates/plan-template.md: pending review
  - .specify/templates/spec-template.md: pending review
  - .specify/templates/tasks-template.md: pending review
  - .specify/templates/commands/constitution.md: pending review
- Follow-up TODOs:
  - Review template and runtime-guidance wording for repository-specific propagation where needed.
-->

# TuiVision Constitution

## Core Principles

### I. Managed-Only Runtime (NON-NEGOTIABLE)

Every module in `src/` MUST run exclusively on the managed .NET 10 runtime.
P/Invoke calls, native library bindings, and OS-specific packages are prohibited.
Platform-specific behavior (terminal control, character encoding) MUST be
abstracted inside `TuiVision.Drivers.Console` using managed .NET APIs only.

**Rationale**: The project targets macOS, Linux, and Windows without requiring
any native installation beyond the .NET 10 SDK. This is a hard gate for CI
reproducibility and cross-platform portability (Pflichtenheft M-11).

### II. Test-First Development — TDD (NON-NEGOTIABLE)

All implementation MUST follow the Red-Green-Refactor cycle:

1. Write the test(s) for the target behaviour — tests MUST fail before implementation starts.
2. Implement the minimum code to make the tests pass (Green).
3. Refactor without breaking the passing tests.

Additional constraints:

- Test framework: **MSTest** exclusively.
- Minimum line coverage: **70%** in `TuiVision.Core`, `TuiVision.Controls`,
  `TuiVision.Serialization`, `TuiVision.Compatibility`, and
  `TuiVision.Drivers.Console`. This is measured and enforced in CI.
  Coverage is evaluated per target assembly and must be reported separately
  for each of the five gate assemblies even when exercising tests are
  distributed across multiple test projects.
- Every ported core component MUST have at least one positive test and one
  negative/error-case test where technically meaningful.
- Integration tests MUST cover: event loop, focus transitions, menu execution,
  and dialog interaction.
- Smoke tests MUST be present for all 25 ported example programs and run in CI.
- No commit that removes or skips an existing passing test is permitted without
  explicit documentation of the rationale.

Pedagogical obligation:

- For every new feature, tests MUST be written and compiled first so that the
  test suite is verifiably red before any implementation begins.
- The Red (failing) → Green (passing) → Refactor sequence MUST be preserved and
  visible in the commit history so that trainees can follow the TDD workflow
  step-by-step as a learning example.
- Agents and contributors MUST NOT shortcut this sequence (e.g., writing
  implementation and tests in the same commit).

**Rationale**: TDD enforces correctness-by-construction on a complex porting
project. Failures are caught at the earliest possible moment rather than
discovered in integration (Pflichtenheft M-07, M-19, section 9). The explicit
Red-Green commit sequence also serves as a live teaching example: trainees can
read the git log and understand how Test-Driven Development is applied in
professional practice.

### III. Didactic and Linguistic Clarity (NON-NEGOTIABLE)

All documentation — code comments, XML documentation, API reference, guides,
and examples — MUST serve as learning material for IT application-development
specialists (Fachinformatiker Anwendungsentwicklung).

Mandatory language and structure rules:

- Explanatory documentation blocks MUST be bilingual: German block first,
  English block second.
- German and English text MUST target CEFR level B2 readability.
- Explanatory comments MUST describe the why (decision, trade-off, constraint),
  not only the what.

Mandatory source documentation rules:

- Every class, interface, struct, enum, method, constructor, property, field,
  parameter, and return value in project-owned source code MUST be documented —
  regardless of access level (public, internal, protected, private).
- Public APIs MUST use complete XML documentation (`<summary>`, `<param>`,
  `<returns>`, and `<exception>` where applicable; `<remarks>` and `<example>`
  where instructive).
- Non-public code elements MUST also use XML documentation (`<summary>` at
  minimum) so that IDE tooling and documentation generators can surface
  explanations at every level of the code.
- Local variables cannot be documented with XML comments in C#. When their
  purpose, invariant, or didactic role is not obvious from naming and nearby
  code, they MUST be explained with a nearby block or line comment. These
  comments MUST also be bilingual (German first, English second) at CEFR-B2
  level.
- In addition to XML documentation, block or line comments MAY be placed at
  didactically important locations to highlight key learning points, design
  decisions, or porting trade-offs. These additional comments MUST also be
  bilingual (German first, English second) at CEFR-B2 level.
- Missing XML documentation for public API members is treated as a build error
  (CS1591 MUST NOT be suppressed globally).
- When API signatures or XML comments change, documentation output MUST be
  regenerated in the same commit/PR.

B2 Readability Rationale:

- CEFR-B1 may be sufficient for entering vocational training, but this project
  sets B2 as the documentation baseline because trainees must understand
  technical texts, write project documentation, follow exam-style tasks, and
  communicate clearly in team and customer contexts.
- The bilingual German-first/English-second structure is required because the
  project is used by native and non-native German-speaking trainees who must be
  able to understand the full source code and documentation without relying on
  expert mediation.

**Rationale**: The project is an educational modernization showcase and must be
understandable for native and non-native German-speaking trainees with at least
intermediate language proficiency (Pflichtenheft M-15, M-16, M-18,
section 10.1–10.5). Requiring XML docs at every access level — not only on
public surfaces — ensures that trainees exploring the full source code always
find clear, machine-readable explanations.

### IV. Modular Architecture

Source code is organised into exactly five modules, each with a single, clear
responsibility. No module MUST depend on another module outside this hierarchy:

| Module | Responsibility | May depend on |
|---|---|---|
| `TuiVision.Core` | Foundation types: geometry, events, base object | — |
| `TuiVision.Controls` | UI components: views, groups, menus, dialogs | Core |
| `TuiVision.Drivers.Console` | Managed console rendering and input | Core |
| `TuiVision.Serialization` | Binary archive, resource streams | Core |
| `TuiVision.Compatibility` | Key-code translation, porting helpers | Core |

Additional modules require documented justification in the plan for that feature.
Organisational-only assemblies without a clear behavioural boundary are not permitted.

**Rationale**: Modular boundaries keep each slice independently testable and
deployable, and map cleanly onto the original Turbo Vision subsystem structure
(Pflichtenheft section 7).

### V. Cross-Platform Portability

The framework MUST build and all tests MUST pass on macOS, Linux, and Windows
using only the .NET 10 SDK with no additional native installations.

- CI MUST run on at least one Linux runner and one macOS runner.
- No OS-specific `#if` blocks or `RuntimeInformation` guards are permitted in
  `TuiVision.Core` or `TuiVision.Controls`.
- Platform adaptation is confined to `TuiVision.Drivers.Console`.

**Rationale**: The framework targets the full .NET Core runtime surface and must
be useful on any developer machine (Pflichtenheft M-11, section 11).

### VI. License & Disclaimer Integrity

- New code written for TuiVision is licensed under the **MIT License**.
- Files under `tv203s/` remain under their original Borland/Inprise/community
  license terms; they MUST NOT be modified.
- Every release artefact (README, LICENSE) MUST carry the disclaimer mandated by
  Pflichtenheft section 10.2: TuiVision is an educational example project, not an
  official Turbo Vision continuation, and carries no competitive intent.
- License headers in new source files MUST be added where required by the chosen
  MIT boilerplate.

**Rationale**: The historical Turbo Vision source carries mixed licensing
(Pflichtenheft section 11 risk register). A clear, visible disclaimer protects
the project and its contributors (Pflichtenheft M-12).

## Technology Stack & Quality Gates

### Mandated Technology

| Concern | Choice |
|---|---|
| Language | C# (LangVersion: latest, targeting C# 14 features) |
| Runtime | .NET 10 (`net10.0`), managed code only |
| JSON library | `System.Text.Json` for project-owned JSON parsing and serialization |
| Test framework | MSTest |
| Documentation generator | docfx (external command `docfx`) |
| CI platform | GitHub Actions |
| Version control | Git; remote: `https://github.com/hindermath/TuiVision.git` |
| Primary IDEs | JetBrains Rider (primary), VS Code (secondary) |
| Dev tooling | `gh`, optional `glab`, `codex`, `claude`, `copilot`, `gemini`, optional `opencode`, optional `junie`, GitHub Spec-Kit |

Project-owned code MUST use `System.Text.Json` for JSON parsing and
serialization. Introducing `Newtonsoft.Json` requires documented justification
in the relevant plan or PR and explicit reviewer approval in the same change.

### Mandatory Quality Gates (all MUST pass before merge to `main`)

1. `dotnet build --configuration Release` exits with code 0 and zero warnings
   treated as errors.
2. `dotnet test` — all required tests pass (see Pflichtenheft section 9.4).
3. Line coverage ≥ 70% per assembly in `TuiVision.Core`, `TuiVision.Controls`,
   `TuiVision.Serialization`, `TuiVision.Compatibility`, and
   `TuiVision.Drivers.Console`. Assembly-specific evidence required; aggregated
   coverage alone does not satisfy this gate.
4. `dotnet format --verify-no-changes` — no formatting violations.
5. No CS1591 suppressions added without documented justification.
6. If `docfx.json` exists in the repository root and public API/XML docs changed,
   `docfx docfx.json` MUST succeed in the same change.
7. Smoke tests pass for all currently-ported example programs.

### Global Build Settings

All projects share settings via `Directory.Build.props`:

```xml
<TargetFramework>net10.0</TargetFramework>
<LangVersion>latest</LangVersion>
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
```

For numbered Spec-Kit branches, `Directory.Build.props` MUST also carry aligned
repo-wide `Version`, `AssemblyVersion`, and `FileVersion` values using
`Major.Minor.Patch.Build`, where `Minor` = the numerically interpreted
Spec-Kit feature/branch number as the canonical PR number for versioning
(`007` -> `7`), `Patch` = the commit count in that feature/PR branch after the
current change is committed, and `Build` = a manual build counter incremented
before each `dotnet build` or `dotnet test`.

## Development Workflow

### Branching

- `main` is the integration branch.
- Feature branches remain the preferred path for substantial feature delivery.
- Direct pushes to `main` are permitted for tightly scoped, user-approved
  repository changes (including documentation, governance, and small corrective
  updates) when the applicable quality gates remain satisfied.
- Feature branches follow either the agent-prefixed pattern
  `codex/<short-description>` (or another supported agent prefix such as
  `claude/`, `gemini/`, `copilot/`, `opencode/`) or the numbered Spec-Kit
  feature pattern `NNN-short-description` when the feature workflow creates and
  maintains that branch shape.
- CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `gemini/**`,
  `opencode/**` and `copilot/**`.

### Daily Development Loop

```bash
git checkout main && git pull --ff-only origin main
dotnet restore
dotnet build
dotnet test
# If docfx.json exists at repository root and docs/API changed:
docfx docfx.json
```

### Code Style & Naming Conventions

The following conventions apply to all new and ported C# code:

- **Identifiers**: Types, methods, and properties use **PascalCase**; local
  variables and parameters use **camelCase**.
- **Value types**:
  - `readonly record struct` for immutable payload types
    (e.g., `TMouseEvent`, `TKeyDownEvent`).
  - Plain `struct` (mutable) for geometry types that mirror original Turbo Vision
    mutation semantics (e.g., `TPoint`, `TRect`).
- **Enumerations**: Use `[Flags]` enums with bitmask values matching original
  Turbo Vision constants (e.g., `TEventKind`, `TViewState`, `TViewOptions`).
- **Class design**: Prefer `sealed class` where inheritance is not intended;
  use interfaces for contracts and abstractions.
- **`var`**: Permitted when the inferred type is unambiguous from context.
- **Test method naming**: `ClassName_MethodName_ExpectedBehavior`
  (e.g., `TRect_Contains_UsesTopLeftInclusiveBottomRightExclusive`).
- **Magic numbers**: MUST NOT appear in production code — use named constants
  or enums instead.

These conventions MUST be consistent across all modules and are checked by
`dotnet format --verify-no-changes` and code review.

### Documentation Compliance Review

Each PR that changes source code, API signatures, comments, or guides MUST
include an explicit documentation compliance check:

1. Verify bilingual (German first, English second) B2-level documentation blocks
   were added or updated where required.
2. Verify XML documentation coverage for all changed members (public and non-public).
3. Regenerate docfx output when `docfx.json` is present and documentation-related
   inputs changed.
4. Close all identified gaps in the same PR; deferred documentation is prohibited.

### Multi-Mac Workflow

The documented workflow in `docs/guides/multi-mac-workflow.md` is the canonical
reference for development on **MacBook Air M2** and **Mac mini M4 Pro**. It MUST
be kept current with any tooling or workflow changes. Prerequisites (`gh`,
optional `glab`, `codex`, `claude`, `copilot`, `gemini`, optional `opencode`,
optional `junie`, GitHub Spec-Kit, `docfx`, `.NET 10 SDK`) MUST be documented
with version check commands.

`codex`, `claude`, `copilot`, and `gemini` are the four supported AI agents for
this project. GitHub Spec-Kit MUST be installed and usable in all four agents.
Installation, version checks, and the basic Spec-Kit workflow commands MUST be
documented for both Macs.

`glab` is an optional supplementary CLI. `opencode` and JetBrains `junie` are
optional supplementary AI agents. Whenever `opencode` or `junie` are configured
locally, GitHub Spec-Kit MUST also be installed, version-checked, and included
in the documented workflow for that machine.

The Multi-Mac setup is the project's primary development and day-to-day test
workflow. Linux and Windows are additional compatibility-validation
environments; on Windows, WSL with a current Ubuntu release (currently Ubuntu
24.04) is the preferred setup. Where practical, these compatibility checks
SHOULD also be represented in GitHub Actions or an equivalent automated
validation path.

### Statistical Documentation

`docs/project-statistics.md` is the mandatory, living statistical ledger for the
repository. It MUST be updated whenever one of the following happens:

1. A Spec-Kit implementation phase is completed or materially re-scoped.
2. An agent-driven work package changes repository content (code, tests, specs,
   plans, tasks, governance, or operational docs).
3. A contributor explicitly requests a statistics refresh.


Within the `## Fortschreibungsprotokoll` section, table rows MUST remain in strict chronological order: oldest entry first, newest and most recently added entry last, while rows with the same date keep their insertion order.

Every update MUST record, at minimum:

- branch or phase identifier and current status,
- observable git-based work window (first/last date, commit days where possible),
- current or change-based counts for production code, test code, and
  documentation,
- the main work packages or delivered artefacts,
- whether the numbers come from committed history, the working tree, or both,
- a conservative manual-effort baseline using **80 code lines per day** for an
  experienced developer.

Manual-effort estimates for a small team MAY be derived from that baseline, but
the formula and assumptions MUST be stated explicitly. Documentation effort is
tracked separately and MUST NOT be hidden inside the code-line estimate.

### Portation Sequence

New classes are ported following the incremental sequence defined in
Pflichtenheft section 8.1:

1. Base infrastructure (solution, build, test, docs pipeline)
2. Core objects (`TObject`, collections, geometry, events)
3. View system (`TView`, `TGroup`, draw buffer, focus/states)
4. Application frame (`TProgram`, `TApplication`, menus, statusbar, desktop)
5. Dialog/control layer (inputs, lists, scrollbars, buttons)
6. Editor/file/help/streams
7. Driver consolidation (managed console driver)
8. Examples (all 25, in waves)

The 25 original example programs from `tv203s/contrib/tvision/examples` are the
only mandatory example scope for acceptance. Additional Turbo-Pascal follow-on
waves from `TVDEMOS/` and `TVFM/` MAY be pursued only after the mandatory scope
is complete and MUST NOT displace or dilute the acceptance criteria for the 25
original examples.

### Commit Discipline

Each commit presented for review, handoff, or merge to `main` MUST leave the
repository in a passing build-and-test state.
One narrowly scoped exception is permitted for Test-Driven Development on a
feature branch: an intentionally red commit MAY fail temporarily when it exists
only to record the new failing test slice for the next behavior change, adds no
production implementation for that slice, and is followed by a green commit
before merge, reviewer handoff, or branch completion.
API changes, implementation changes, and documentation updates that belong to
the same change MUST be in the same commit, except for the explicit Red →
Green → optional Refactor separation mandated by principle II.

## Workspace Baseline Alignment

This Spec-Kit constitution inherits the binding workspace-family governance from `constitution.md` in the repository root. Project-specific rules remain in force; where both apply, the stricter rule wins.

### A. Observability & Continuous Measurement

Every repository MUST maintain `docs/project-statistics.md` as a living statistics ledger. The conservative manual reference is `80` lines/workday. Because this is a C#/.NET repository, the default Thorsten-Solo baseline is `125` lines/workday unless the repository documents and justifies a different project-specific value. The TVöD workday baseline is `7.8 h` (`7h 48m`). Shared statistics guidance MUST stay consistent across `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md`.

### B. Programmierung #include<everyone> — Inclusion & Accessibility By Default

`Programmierung #include<everyone>` is a binding repository-wide principle. All user-facing artefacts — including CLI output, documentation and Markdown, HTML and generated websites, graphical user interfaces, and generated templates or scaffolding — MUST follow WCAG 2.2 Level AA wherever the criteria are applicable. They MUST remain usable with keyboard-only interaction, screen readers, Braille displays, and text browsers. Accessibility review is part of completion, not post-processing.

### C. Runtime Guidance References

Governance text that references runtime guidance MUST name all four maintained agent surfaces: `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md`.

## Governance

This constitution supersedes all other practice documents in case of conflict.
Amendments require:

1. A documented rationale for the change (in the PR description).
2. Version bump according to semantic rules:
   - **MAJOR**: backward-incompatible governance/principle removal or redefinition.
   - **MINOR**: new principle or section added, or materially expanded guidance.
   - **PATCH**: clarifications, wording corrections, non-semantic refinements.
3. `LAST_AMENDED_DATE` updated to the merge date.
4. Consistency propagation: templates and dependent artefacts reviewed and updated
   in the same PR (see steps 4–5 of the `speckit.constitution` command).

All PRs and code reviews MUST verify compliance with principles I–VI.
Complexity beyond what architecture principle IV defines MUST be justified in
writing in the relevant plan document.

Use `CLAUDE.md`, `GEMINI.md`, `copilot-instructions.md`, and `AGENTS.md` for
runtime agent-specific development guidance.
Use `docs/guides/multi-mac-workflow.md` for local multi-machine workflow details.
Use `docs/project-statistics.md` for the living project-statistics ledger and
manual-effort baseline tracking.

**Version**: 1.11.0 | **Ratified**: 2026-03-01 | **Last Amended**: 2026-04-20
