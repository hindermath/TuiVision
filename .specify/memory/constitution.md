<!--
  SYNC IMPACT REPORT
  ==================
  Version change: (unversioned template) → 1.0.0
  Bump rationale: MAJOR — initial ratification; complete replacement of all placeholder tokens.

  Modified principles: N/A (first creation)

  Added sections:
    - Core Principles (6 principles: I–VI)
    - Technology Stack & Quality Gates
    - Development Workflow
    - Governance

  Removed sections: N/A (template placeholders replaced)

  Templates requiring updates:
    - .specify/templates/plan-template.md ✅ aligned — Constitution Check section is generic
      and will automatically reflect these principles when filled by /speckit.plan.
    - .specify/templates/spec-template.md ✅ aligned — user stories and acceptance scenarios
      match the test-first discipline defined in Principle II.
    - .specify/templates/tasks-template.md ✅ aligned — tasks-template already mandates
      "Tests MUST be written and FAIL before implementation", consistent with Principle II.
    - .claude/commands/*.md ✅ no outdated agent-specific references found that conflict.

  Follow-up TODOs:
    - None. All fields resolved from project documents (Lasten_Heft.md, Pflichtenheft.md,
      README.md, CLAUDE.md, AGENTS.md, GEMINI.md, docs/guides/multi-mac-workflow.md).
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
- Minimum line coverage: **70%** in `TuiVision.Core`, `TuiVision.Controls`, and
  `TuiVision.Serialization`. This is measured and enforced in CI.
- Every ported core component MUST have at least one positive test and one
  negative/error-case test (where fachlich sinnvoll / technically meaningful).
- Integration tests MUST cover: event loop, focus transitions, menu execution,
  and dialog interaction.
- Smoke tests MUST be present for all 25 ported example programs and run in CI.
- No commit that removes or skips an existing passing test is permitted without
  explicit documentation of the rationale.

**Rationale**: TDD enforces correctness-by-construction on a complex porting
project. Failures are caught at the earliest possible moment rather than
discovered in integration (Pflichtenheft M-07, M-19, section 9).

### III. Didactic Documentation (NON-NEGOTIABLE)

All documentation — code, API reference, guides, and examples — MUST serve as
learning material for IT application-development specialists (Fachinformatiker
Anwendungsentwicklung):

- Every `public` type, member, parameter, and return value MUST carry complete
  XML documentation (`<summary>`, `<param>`, `<returns>`, and `<exception>` where
  applicable; `<remarks>` and `<example>` where instructive).
- Comments explain the **why** (decision, trade-off, constraint), not only the what.
- Non-trivial logic (multiple branches, complex state changes) MUST carry inline
  explanatory comments.
- Portation decisions that deviate from Turbo Vision 2.0.3 behaviour MUST be
  documented at the point of deviation with a rationale.
- Missing XML documentation for public API members is treated as a build error
  (CS1591 MUST NOT be suppressed globally).
- When API signatures or XML comments change, the docfx output MUST be regenerated
  in the same commit/PR.

**Rationale**: The explicit purpose of TuiVision is as a showcase and learning
reference for Agentic-AI modernisation workflows (Pflichtenheft M-15, M-16, M-18,
section 10.1–10.5).

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

**Rationale**: The historical Turbo Vision source carries mixed licensing (Pflichtenheft
section 11 risk register). A clear, visible disclaimer protects the project and
its contributors (Pflichtenheft M-12).

## Technology Stack & Quality Gates

### Mandated Technology

| Concern | Choice                                                                                                                                    |
|---|-------------------------------------------------------------------------------------------------------------------------------------------|
| Language | C# (LangVersion: latest, targeting C# 14 features)                                                                                        |
| Runtime | .NET 10 (`net10.0`), managed code only                                                                                                    |
| Test framework | MSTest                                                                                                                                    |
| Documentation generator | docfx (installed as .NET global tool)                                                                                                     |
| CI platform | GitHub Actions                                                                                                                            |
| Version control | Git; remote: `https://github.com/hindermath/TuiVision.git`                                                                                |
| Primary IDEs | JetBrains Rider (primary), VS Code (secondary)                                                                                            |
| Dev tooling | `gh` (GitHub CLI), `codex` (Codex CLI), `claude` (Claude CLI), `gemini` (Gemini CLI), `opencode` (openCode CLI), `copilot` (Copilot CLI), |

### Mandatory Quality Gates (all MUST pass before merge to `main`)

1. `dotnet build --configuration Release` exits with code 0 and zero warnings
   treated as errors.
2. `dotnet test` — all MUSS-Tests pass (see Pflichtenheft section 9.4).
3. Line coverage ≥ 70% in Core, Controls, Serialization.
4. `dotnet format --verify-no-changes` — no formatting violations.
5. No CS1591 suppressions added without documented justification.
6. docfx build succeeds whenever `docs/docfx.json` is present and any public
   API or XML comment has changed.
7. Smoke tests pass for all currently-ported example programs.

### Global Build Settings

All projects share settings via `Directory.Build.props`:

```xml
<TargetFramework>net10.0</TargetFramework>
<LangVersion>latest</LangVersion>
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
```

## Development Workflow

### Branching

- `main` is the integration branch; direct pushes to `main` are only permitted
  for administrative changes (not code).
- Feature branches follow the pattern: `codex/<short-description>`.
- CI runs on pushes to `main`, `master`, `codex/**`, `claude/**`, `gemini/**`, `opencode/**` and `copilot/**`.

### Daily Development Loop

```bash
git checkout main && git pull --ff-only origin main
dotnet restore
dotnet build
dotnet test
# When API or XML comments change:
docfx docs/docfx.json
```

### Multi-Mac Workflow

The documented workflow in `docs/guides/multi-mac-workflow.md` is the canonical
reference for development on **MacBook Air M2** and **Mac mini M4 Pro**. It MUST
be kept current with any tooling or workflow changes. Prerequisites (`gh`, `codex`, `claude`, `gemini`, `opencode`, `copilot`, `docfx`,
.NET 10 SDK) MUST be documented with version check commands.

### Portation Sequence

New classes are ported following the inkrementell (incremental) sequence defined
in Pflichtenheft section 8.1:

1. Base infrastructure (solution, build, test, docs pipeline)
2. Core objects (`TObject`, collections, geometry, events)
3. View system (`TView`, `TGroup`, draw buffer, focus/states)
4. Application frame (`TProgram`, `TApplication`, menus, statusbar, desktop)
5. Dialog/control layer (inputs, lists, scrollbars, buttons)
6. Editor/file/help/streams
7. Driver consolidation (managed console driver)
8. Examples (all 25, in waves)

### Commit Discipline

Each commit MUST leave the repository in a passing build-and-test state.
API changes, implementation changes, and documentation updates that belong to
the same change MUST be in the same commit.

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
Complexity beyond what the architecture principle IV defines MUST be justified
in writing in the relevant plan document.

Use `CLAUDE.md`, `GEMINI.md`, `copilot-instructions.md` and `AGENTS.md` for runtime agent-specific development guidance.
Use `docs/guides/multi-mac-workflow.md` for local multi-machine workflow details.

**Version**: 1.0.0 | **Ratified**: 2026-03-01 | **Last Amended**: 2026-03-01
