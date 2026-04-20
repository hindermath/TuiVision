# Constitution v1.5.0

# home-baseline Constitution

## Beschreibung / Description

Diese Verfassung definiert die verbindlichen Prinzipien und Standards für alle home-baseline Workspaces.

*This constitution defines the non-negotiable principles and standards for all home-baseline workspaces.*

Leitspruch: `Programmierung #include<everyone>`.

*Guiding motto: `Programmierung #include<everyone>`.*

## Core Principles

### I. Security-First (NON-NEGOTIABLE)

Every file tracked in any workspace repository MUST be safe to publish.
The `.gitignore` in every workspace uses a **whitelist model**: everything is
excluded by default (`/*`, `/.*`), and only explicitly listed safe entries are
allowed.

Non-negotiable rules:
- Credential files (`.env*`, `*.key`, `*.pem`, `*secret*`, `.aws/`, `.ssh/`,
  `.kube/`, `.docker/`, `.gnupg/`) MUST never be tracked.
- The sensitive root-level content of AI agent state directories MUST never
  be tracked: `.claude/` (history, sessions, cache), `.codex/` (auth, SQLite
  DBs), `.gemini/` (oauth_creds.json, google_accounts.json), `.junie/`
  (history, logs), `.opencode/`.
- **Surgical subdirectory exception**: A specific subdirectory within an
  otherwise-blocked agent directory MAY be tracked if and only if it contains
  exclusively tool-definition files (no credentials, no session data). The
  `.gitignore` MUST use the block-then-allow pattern:
  ```
  !.claude/
  .claude/*
  !.claude/commands/
  ```
  Currently allowed subdirectories: `.claude/commands/` and `.gemini/commands/`
  (Spec-Kit command definitions only).
- Every workspace MUST have a `pre-push` hook installed that blocks pushes
  containing secret-like filenames or credential patterns (tokens matching
  `ghp_*`, `sk-*`, `AKIA*`, `AIza*`, PEM private-key headers).
- `scripts/scan-agent-secrets.sh --fail-on-high` MUST be run before pushing
  any change that touches hook or scanner logic.

**Rationale**: Accidental secret exposure in a private repo is a critical security
incident. Automated prevention at push time is the last reliable gate. The
surgical subdirectory exception enables Spec-Kit tool definitions to be
synchronized across devices without exposing any credentials.

### II. Cross-Platform Parity & Documentation

Every critical script MUST exist in two variants:
- Bash (`.sh`) for macOS/Linux
- PowerShell Core 7+ (`.ps1`) for Windows

Both variants MUST provide identical functionality and produce equivalent output.
A new script is not considered complete until:
1. Both variants exist and pass manual verification.
2. A corresponding Unix man-page is provided for the Bash variant (stored in `docs/man/`).
3. Complete bilingual comment-based help is provided for the PowerShell variant.
4. PowerShell scripts MUST also be available as Cmdlets (Advanced Functions) using the `Verb-Noun` naming convention (e.g., `New-HBWorkspace`).
5. Help switches (`-h`, `--help`) point to the man-page or internal help.

All files MUST be committed together in the same commit.

**Rationale**: The workspace is used on macOS and Windows. Bash-only or PowerShell-only scripts create a second-class experience. Professional documentation ensures maintainability and ease of use across platforms.

### III. Bootstrap Automation

New workspaces MUST be created exclusively via the bootstrap scripts:
- `bash ~/scripts/bootstrap-workspace.sh <WorkspaceName>` (macOS/Linux)
- `pwsh ~/scripts/bootstrap-workspace.ps1 -WorkspaceName <Name>` (Windows)

Manual `git init` + `gh repo create` outside the bootstrap flow is prohibited
for new workspaces. The bootstrap script is the single authoritative source of
the correct workspace setup sequence.

Workspace removal MUST be performed exclusively via the teardown scripts:
- `bash ~/scripts/teardown-workspace.sh <WorkspaceName>` (macOS/Linux)
- `pwsh ~/scripts/teardown-workspace.ps1 -WorkspaceName <Name>` (Windows)
- or the alias: `bash ~/scripts/bootstrap-workspace.sh --teardown <WorkspaceName>`

Manual `rm -rf` without teardown is prohibited because it orphans remote
repositories, `~/README.md` table entries, `~/.gitignore` entries, and
`~/.gitconfig` `[includeIf]` blocks.

`~/README.md` MUST be updated (automatically or manually) whenever a new
workspace is added. The workspace table anchor `<!-- workspace-table-end -->`
MUST be preserved.

**Rationale**: Consistency across all workspaces — same `.gitignore` whitelist,
same scripts, same hooks — can only be guaranteed by a single automated flow.

### IV. Workspace Isolation

Each workspace directory under `~/` is an **independent Git repository**.
Git submodules MUST NOT be used. Sub-repositories inside a workspace are
detected by the bootstrap script and excluded via `.gitignore` entries.

The `home-baseline` repo tracks the following categories of files (all others
are excluded by the whitelist `.gitignore`):

| Category | Tracked paths |
|----------|--------------|
| Infrastructure scripts | `scripts/` |
| Documentation | `README.md`, `.gitignore`, `.gitconfig`, `docs/` |
| AI agent guidance | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` |
| Spec-Kit tooling | `.specify/` (config, templates, memory/constitution), `.agents/skills/`, `.github/agents/`, `.github/prompts/` |
| Agent Spec-Kit commands | `.claude/commands/`, `.gemini/commands/` |

Rules:
- Changes to `home-baseline` scripts do NOT auto-propagate to child workspaces;
  workspaces sync manually by re-running the relevant script or copying updates.
- Each workspace owns its own `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and
  `.github/copilot-instructions.md`.
- Adding a new tracked category MUST be accompanied by a constitution amendment
  (PATCH or MINOR depending on scope).

**Rationale**: Submodules create fragile cross-repo coupling. Independent repos
give each workspace its own clean history and deployment lifecycle. Tracking
AI agent guidance files and Spec-Kit tooling ensures consistent development
environments across all devices.

### V. Manual-First Verification

`home-baseline` uses a blended verification model: manual verification remains
mandatory for script changes, and lightweight automated CI/CD guardrails on
GitHub MAY complement it. GitLab release automation is also maintained in this
repository as reusable baseline logic and MUST be validated through real
project pipelines before it is treated as production-ready. Verification MUST
follow the safe-mode-first rule:

- Bootstrap changes: always test with `--dry-run` (Bash) / `-WhatIf` (PowerShell)
  before running for real.
- Hook changes: reinstall with `bash scripts/install-hooks.sh` after every edit
  under `scripts/hooks/`, then verify behaviour manually.
- Scanner changes: run `bash scripts/scan-agent-secrets.sh --fail-on-high .`
  and confirm expected exit codes before committing.

Automated test tooling MUST NOT be added to this repository unless a formal
decision is made and documented in this constitution (Governance section).

**Rationale**: The scripts are low-churn infrastructure. Manual dry-runs and
real pipeline validation catch the most relevant operational risks with less
maintenance overhead than a broad scripted test framework.

### VI. Observability & Continuous Measurement

Every repository — including `home-baseline` and every Level-2 workspace — MUST maintain a living statistics ledger at `docs/project-statistics.md`.

Mandatory content and update rules:

- **Fortschreibungsprotokoll**: chronological table (oldest entry first, newest last) recording cumulative lines, active days, and commit count at each milestone.
- **Gesamtstatistik**: always the final top-level section; includes compact ASCII-only diagrams (artefakt mix, phase volume, speedup factors, manual-reference comparison).
- **Update triggers**: after each completed Spec-Kit implementation phase, after each merged feature, or when explicitly requested.
- **Reference baselines**:
  - Manual reference: `80` lines/workday (conservative) — project-specific Thorsten-Solo baseline documented consistently in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md`.
  - Default C#/.NET Thorsten-Solo baseline: `125` lines/workday unless a repository documents and justifies a different project-specific value. `home-baseline` itself keeps `100` lines/workday as the scripting-infrastructure Thorsten-Solo reference.
  - TVöD workday: `7.8 h` (`7h 48m`). Month: `21.5` workdays. Vacation: 30 days until end of 2026, 31 days from 2027 onwards.
- **Acceleration factor** = blended repository speedup — delivery density against manual reference, **not** stopwatch time.
- **Diagram format**: compact ASCII-only; each diagram followed by a CEFR-B2 bilingual explanation (DE + EN).
- **Consistency rule**: When statistics methodology or shared guidance changes, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md` MUST be updated together in the same commit. The same shared rules MUST also be propagated to the relevant project templates and `.specify/memory/constitution.md`.

The bootstrap scripts (`bootstrap-project.sh` / `.ps1`) MUST create an initial `docs/project-statistics.md` stub at project creation time. `docs/` MUST be whitelisted in every project `.gitignore`.

**Rationale**: Blended speedup metrics are educational for developers and apprentices. They make the productivity impact of AI-assisted workflows visible and comparable across projects. A living ledger that accumulates over the project lifetime is the only reliable source of this data.

### VII. Programmierung #include<everyone> — Inclusion & Accessibility By Default

`Programmierung #include<everyone>` is a binding repository-wide principle, not a slogan.
All user-facing artefacts MUST be designed and reviewed for inclusive use:

- CLI output
- Documentation and Markdown
- HTML and generated websites
- Graphical user interfaces
- Generated templates and scaffolding

Mandatory rules:
- WCAG 2.2 Level AA is the default accessibility baseline wherever the criteria are applicable.
- User-facing artefacts MUST remain usable with keyboard-only interaction, screen readers, Braille displays, and text browsers.
- Text-first fallbacks MUST be preferred for status reporting, diagrams, and operational guidance.
- Accessibility review is part of completion, not post-processing.

**Rationale**: Inclusive delivery improves quality for everyone, reduces retrofit work, and makes the repositories usable in real assistive-technology workflows from the start.

### VIII. DE-First / EN-Second Bilingual Delivery

German is the canonical first language for user-facing documentation and governance in this workspace family; English follows directly after it.

Mandatory rules:
- Headings MUST follow the `DE / EN` pattern unless the heading is a proper noun or tool name.
- Learner-facing and user-facing documentation MUST be maintained bilingually at approximately CEFR-B2 readability.
- Large normative documents MAY use a synchronized `.EN.md` companion file when inline bilingual maintenance would become unreadable.
- Changes that materially affect user-facing guidance MUST update both language tracks in the same change.

**Rationale**: DE-first / EN-second delivery reflects the actual audience while keeping the content usable for mixed-language teams, apprentices, and external review.

### IX. Four-Agent Guidance Parity & Template Synchronization

Shared AI-agent guidance in this workspace family is only valid when the four maintained agent surfaces stay aligned:

- `AGENTS.md` for Codex/Codex-like agents
- `CLAUDE.md`
- `GEMINI.md`
- `.github/copilot-instructions.md`

Mandatory rules:
- Shared operational rules MUST NOT be updated in only one of the four files.
- Any intentional deviation MUST be documented explicitly in the same change.
- The corresponding project templates and `.specify/memory/constitution.md` MUST be updated in the same change whenever a shared principle changes.
- Runtime guidance references in governance text MUST name all four maintained agent surfaces.

**Rationale**: Divergent agent instructions create silent process drift. Atomic parity keeps different AI tools aligned and makes future project bootstraps inherit the same governance baseline.

## Script & Code Conventions

Coding style rules that apply to all scripts in this repository:

- **Bash shebang & safety flags**: `#!/usr/bin/env bash` + `set -euo pipefail`
- **PowerShell header**: `#Requires -Version 7` + `Set-StrictMode -Version Latest`
  \+ `$ErrorActionPreference = 'Stop'`
- **Indentation**: 2 spaces in Bash, 4 spaces in PowerShell
- **Filenames**: kebab-case (e.g., `bootstrap-workspace.sh`)
- **PowerShell parameters**: PascalCase (e.g., `-WorkspaceName`, `-WhatIf`)
- **PowerShell naming**: Use the standard `Verb-Noun` pattern for functions and Cmdlets (e.g., `New-HBWorkspace`, `Set-HBSettings`).
- **Bash variables**: lowercase_underscore (e.g., `repo_name`)
- **Documentation**:
  - Bash scripts MUST have a corresponding man-page in `docs/man/` (section 1).
  - PowerShell scripts MUST include complete comment-based help (`.SYNOPSIS`, `.DESCRIPTION`, etc.).
  - Both MUST be bilingual (DE / EN) or consistent with existing script headers.
- **User-facing messages**: German primary (`Fehler:`, `Verzeichnis nicht gefunden`);
  English is acceptable in code comments
- **Visual output**: box-drawing characters (╔, ║, ╚, ✓, →) for structured console blocks
- **End-of-options sentinel**: Bash scripts that accept positional arguments MUST support `--` to terminate option parsing, allowing names that start with `-` (e.g., `teardown-workspace.sh -- -myworkspace`)

## Commit & Pull Request Standards

- **Commit message format**: Conventional Commits — `chore:`, `docs:`, `feat:`, `fix:`
  followed by a short imperative subject line
- **Co-authored-by trailer**: Every commit MUST include:
  ```
  Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>
  ```
- **PR description MUST include**:
  - Which scripts or docs are affected
  - Manual verification commands run (with `--dry-run` / `-WhatIf` output)
  - Sample console output when user-visible output changes
  - Explicit security risk statement for any change touching hook or scanner logic
- **Lastenheft rename on feature completion**: When a feature's implementation is fully merged, the corresponding `Lastenheft_*.md` MUST be renamed via `bash scripts/rename-lastenheft.sh <LH-file> <branch-name>` (macOS/Linux) or `pwsh scripts/rename-lastenheft.ps1 -File <LH-file> -BranchName <branch-name>` (Windows). This stamps the feature branch name onto the filename and marks the Lastenheft as archived. The rename commit MUST be included in the final tasks.md as the last step of the Polish phase.

## Governance

This constitution supersedes all other practices documented in `AGENTS.md`,
`CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md` where they
conflict. Those files provide runtime guidance for AI agents; this constitution
defines non-negotiable structural rules.

**Amendment procedure**:
1. Propose the change in a PR; describe the principle being added, changed, or removed.
2. Update `LAST_AMENDED_DATE` to the PR merge date.
3. Increment `CONSTITUTION_VERSION` following semantic versioning:
   - MAJOR: backward-incompatible principle removal or redefinition
   - MINOR: new principle or section added / materially expanded guidance
   - PATCH: clarifications, wording fixes, non-semantic refinements
4. Propagate any principle changes to dependent templates
   (`.specify/templates/plan-template.md`, `spec-template.md`, `tasks-template.md`,
   relevant `scripts/templates/*`, and `.specify/memory/constitution.md`)
   and AI agent guidance files, committing all changes atomically.
5. All PRs and AI-assisted sessions MUST verify compliance with the current
   version of this constitution before committing code or scripts.

**Version policy**: Constitution version is independent of any software release
version. It tracks the governance document's own evolution.

**Compliance review**: Any change to `scripts/hooks/pre-push` or
`scripts/scan-agent-secrets.*` MUST explicitly state in the PR which security
rule (Principle I) it affects and include scanner output confirming no regressions.
Any expansion of the surgical subdirectory exception (Principle I) MUST include
a security justification confirming no credentials are present in the newly
allowed path.

**Runtime guidance**: Use `AGENTS.md` / `CLAUDE.md` / `GEMINI.md` /
`.github/copilot-instructions.md` for per-agent operational guidance. This
constitution is the authoritative policy layer above all agent-specific files.

**Version**: 1.5.0 | **Ratified**: 2026-03-31 | **Last Amended**: 2026-04-20

<!-- EN: constitution.md placeholder
[DE-Zusammenfassung: constitution.md beschreibt die Prinzipien und Standards für alle home-baseline Workspaces.]
-->
