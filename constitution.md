# Constitution v1.1.0

# home-baseline Constitution

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

### II. Cross-Platform Parity

Every critical script MUST exist in two variants:
- Bash (`.sh`) for macOS/Linux
- PowerShell Core 7+ (`.ps1`) for Windows

Both variants MUST provide identical functionality and produce equivalent output.
A new script is not considered complete until both variants exist, pass manual
verification, and are committed together in the same commit.

**Rationale**: The workspace is used on macOS (Mac Mini M4 Pro, MacBook Air M2)
and may be used on Windows. Bash-only or PowerShell-only scripts create a
second-class experience on one platform.

### III. Bootstrap Automation

New workspaces MUST be created exclusively via the bootstrap scripts:
- `bash ~/scripts/bootstrap-workspace.sh <WorkspaceName>` (macOS/Linux)
- `pwsh ~/scripts/bootstrap-workspace.ps1 -WorkspaceName <Name>` (Windows)

Manual `git init` + `gh repo create` outside the bootstrap flow is prohibited
for new workspaces. The bootstrap script is the single authoritative source of
the correct workspace setup sequence.

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
| Documentation | `README.md`, `.gitignore`, `.gitconfig` |
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
environments across all devices (Mac Mini M4 Pro, MacBook Air M2).

### V. Manual-First Verification

There is no automated CI/CD pipeline for `home-baseline` scripts. Verification
is manual and MUST follow the safe-mode-first rule:

- Bootstrap changes: always test with `--dry-run` (Bash) / `-WhatIf` (PowerShell)
  before running for real.
- Hook changes: reinstall with `bash scripts/install-hooks.sh` after every edit
  under `scripts/hooks/`, then verify behaviour manually.
- Scanner changes: run `bash scripts/scan-agent-secrets.sh --fail-on-high .`
  and confirm expected exit codes before committing.

Automated test tooling MUST NOT be added to this repository unless a formal
decision is made and documented in this constitution (Governance section).

**Rationale**: The scripts are low-churn infrastructure. The overhead of a test
framework would exceed the benefit for the current scope.

## Script & Code Conventions

Coding style rules that apply to all scripts in this repository:

- **Bash shebang & safety flags**: `#!/usr/bin/env bash` + `set -euo pipefail`
- **PowerShell header**: `#Requires -Version 7` + `Set-StrictMode -Version Latest`
  \+ `$ErrorActionPreference = 'Stop'`
- **Indentation**: 2 spaces in Bash, 4 spaces in PowerShell
- **Filenames**: kebab-case (e.g., `bootstrap-workspace.sh`)
- **PowerShell parameters**: PascalCase (e.g., `-WorkspaceName`, `-WhatIf`)
- **Bash variables**: lowercase_underscore (e.g., `repo_name`)
- **User-facing messages**: German primary (`Fehler:`, `Verzeichnis nicht gefunden`);
  English is acceptable in code comments
- **Visual output**: box-drawing characters (╔, ║, ╚, ✓, →) for structured console blocks

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
   (`.specify/templates/plan-template.md`, `spec-template.md`, `tasks-template.md`)
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

**Runtime guidance**: Use `AGENTS.md` / `CLAUDE.md` / `GEMINI.md` for
per-agent operational guidance. This constitution is the authoritative policy
layer above all agent-specific files.

**Version**: 1.1.0 | **Ratified**: 2026-03-31 | **Last Amended**: 2026-03-31

<!-- EN: constitution.md placeholder
[DE-Zusammenfassung: constitution.md beschreibt die Prinzipien und Standards für alle home-baseline Workspaces.]
-->
