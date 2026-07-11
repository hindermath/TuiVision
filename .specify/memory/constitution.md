<!--
Sync Impact Report
Version change: 1.13.0 -> 1.14.0
Modified principles:
- XIV. Secure Development Standards & Applicability Matrix (add AI-SBOM)
- XVI. Supply-Chain Transparency & Build Integrity (add G7/BSI AI-SBOM applicability)
- XIX. EU Cyber Resilience Act (CRA) Compliance Awareness (add AI Act / AI-SBOM awareness)
Added sections:
- None
Removed sections:
- None
Templates requiring updates:
- ✅ .specify/templates/constitution-template.md
- ✅ .specify/templates/plan-template.md
- ✅ .specify/templates/spec-template.md
- ✅ .specify/templates/supply-chain-evidence-template.md
- ✅ .specify/templates/tasks-template.md
Runtime guidance requiring updates:
- ✅ AGENTS.md
- ✅ CLAUDE.md
- ✅ GEMINI.md
- ✅ .github/copilot-instructions.md
- ✅ .specify/memory/constitution.md (mirror)
Follow-up TODOs:
- None
-->

# Constitution v1.14.0

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
  Currently allowed subdirectories: `.claude/commands/` and `.agents/skills/`
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
| Agent Spec-Kit commands | `.claude/commands/`, `.agents/skills/` |

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

### X. Level-2 Project Environment Addenda

Level-2 project constitutions MUST preserve the shared policy layer and add a
project-local environment addendum or a clearly applicable entry in the shared
Level-2 environment registry instead of relying on a generic copy.

Mandatory rules:
- Each Level-2 `constitution.md` MUST document the local runtime, build system,
  test framework, documentation/A11Y toolchain, statistics baseline, and
  repository-specific agent surfaces.
- The shared Level-2 Project Environment Registry in this constitution is the
  canonical cross-repository index for those project environments.
- Project-specific addenda MUST enrich the shared constitution; they MUST NOT
  weaken Security-First, A11Y, bilingual, statistics, or four-agent parity
  requirements.
- When a project-specific runtime or tooling baseline changes, this registry,
  the local `constitution.md`, `.specify/memory/constitution.md`, and affected
  agent guidance files MUST be reviewed together.
- Level-0 and Level-1 constitutions define shared policy. Level-2 constitutions
  define the same policy plus the concrete project environment. A Level-2
  repository MUST treat its registry row as binding local context for Spec-Kit
  plans, generated tasks, and agent runtime decisions.

**Rationale**: A generic constitution is not sufficient for real project work.
Agentic tools need the binding shared rules and the local build/test/runtime
context in the same policy surface so generated plans do not drift away from
the actual project environment.

### XI. Memory-Safe Languages (MSL) Preference for Level-2 Projects

Level-2 project repositories SHOULD select a memory-safe language (MSL) as
their primary implementation runtime whenever the target platform allows it.
This is a recommendation, not a hard block: legacy, embedded, and retro-
hardware targets that cannot be built with an MSL toolchain remain explicitly
permitted when their Level-2 `constitution.md` documents a short justification.

Mandatory rules:
- The `Runtime / Language` column of every Level-2 registry row is the
  authoritative primary-language declaration for that project.
- When the declared primary language is **not** on the MSL allow-list below,
  the Level-2 `constitution.md` MUST include a short written justification
  (target hardware, legacy C API, retro platform, interoperability
  requirement, safety-certified non-MSL toolchain, etc.). The justification
  MUST name the constraint, not merely restate the fact.
- The Spec-Kit `speckit.constitution` skill and `speckit.specify` SHOULD emit
  a non-blocking advisory warning when a repository's primary implementation
  language is not an MSL. The warning MUST NOT prevent constitution creation,
  amendment, or repository bootstrap.
- Adding a new non-MSL Level-2 project is allowed with justification. Removing
  the MSL preference itself requires a MAJOR constitution amendment.

**MSL allow-list** (baseline: NSA "Software Memory Safety", Nov 2022; CISA
"The Case for Memory Safe Roadmaps", Dec 2023; extended with obvious
CLR/JVM/BEAM and functional peers of the NSA/CISA-listed languages):

| Family | Memory-safe languages |
|---|---|
| Systems / compiled | Rust, Swift |
| .NET / CLR | C#, F#, VB.NET |
| JVM | Java, Kotlin, Scala, Clojure, Groovy |
| Google-originated | Go, Dart |
| Dynamic / scripting | Python, Ruby, JavaScript, TypeScript, PHP (Zend ≥ 7), Lua |
| Functional | Haskell, OCaml, Elm, PureScript |
| BEAM (actor VM) | Erlang, Elixir, Gleam |
| Safety-critical / formally verified | Ada, SPARK |

**Explicitly NOT memory-safe** (primary use requires justification):
C, C++, classic Objective-C, Assembly (6502, ARM, x86, RISC-V, Z80, …),
the `cc65` C89 toolchain, Zig (pre-1.0, only partial runtime checks), Nim
(manual-memory mode), D without the default GC.

**Current registry status**:
- All `RiderProjects/*` entries (C# / .NET 9–10) — MSL ✓
- `DataGripProjects/InventarDb` (C# / .NET integration context) — MSL ✓
- `C64Projects/cc65` (C / 6502 assembler targeting Commodore 64) —
  **not MSL**; justification: the target platform is 8-bit retro hardware
  with no MSL toolchain available, and the repository's purpose is parity
  with the historical cc65 reference. Justification to be documented inline
  in its Level-2 `constitution.md`.

**Rationale**: Since 2022/2023 NSA and CISA have identified the transition to
memory-safe languages as the single highest-leverage mitigation against the
most common CVE classes (buffer overflows, use-after-free, double-free, type
confusion, out-of-bounds reads). Encoding the preference at workspace level
keeps new Level-2 projects actively choosing memory safety instead of drifting
into unsafe defaults, while preserving deliberate room for legacy, embedded,
and hardware-bound repositories.

### XII. Secure Code Generation (ISO 27001/27002 A.8.28)

AI-generated code MUST follow the established secure-coding best practices of
the target language and framework. LLMs do not reliably produce secure code by
default; explicit enforcement at the governance level is required.

Mandatory rules:
- Generated code MUST avoid known vulnerability classes from the OWASP Top 10
  and the language-specific CWE lists relevant to the project runtime.
- Language-specific secure-coding standards MUST be applied (see
  `.specify/templates/secure-coding-language-rules-template.md` for the
  detailed checklist):
  - **C / C89 (cc65)**: bounds checking on all buffer operations, no `gets()`,
    no unchecked `sprintf()`/`strcpy()`, integer overflow guards, CERT C Coding
    Standard where applicable.
  - **C# / .NET**: parameterised queries, output encoding against XSS,
    anti-forgery tokens for forms, policy-based authorisation, secure
    deserialisation defaults, `HttpClient` timeout/SSRF review, Microsoft
    Secure Coding Guidelines.
  - **Rust**: isolate and justify `unsafe`, avoid panic paths from untrusted
    input, validate deserialised data, use reviewed cryptography, and run
    `cargo audit` or equivalent advisory scanning.
  - **Go**: propagate `context`, set HTTP server/client timeouts, prevent SSRF,
    use `crypto/rand`, constrain file paths, and run `govulncheck` or
    equivalent.
  - **Swift**: avoid force unwraps on untrusted data, validate decoded input,
    use Keychain/CryptoKit/platform TLS defaults, and constrain file URLs.
  - **Java / Kotlin**: validate DTOs, parameterise persistence access, restrict
    deserialisation, enforce framework authentication, authorisation, CSRF,
    CORS, and session settings.
  - **Python**: validate boundary input, avoid unsafe deserialisation and
    dynamic execution, constrain subprocess/file access, keep TLS verification
    enabled, and run dependency auditing.
  - **TypeScript / JavaScript**: validate runtime input, prevent XSS,
    prototype pollution and SSRF, avoid dynamic code execution, review auth,
    cookie and CSP settings, and audit lock files.
  - **SQL**: parameterised statements only, least-privilege access patterns,
    no dynamic SQL from untrusted input.
  - **Bash**: quoted variable expansions (`"$var"`), no `eval` on untrusted
    input, `--` end-of-options sentinel for external commands, CERT Shell
    Scripting guidelines.
  - **PowerShell**: `Set-StrictMode -Version Latest`, validated parameters,
    no `Invoke-Expression` on untrusted input, PowerShell security best
    practices.
- Cryptographic choices MUST use current, recommended algorithms and
  key lengths (e.g., AES-256, RSA >= 3072 bit, SHA-256+, Ed25519).
  Deprecated algorithms (MD5, SHA-1 for signatures, DES, RC4) MUST NOT be
  used unless interfacing with legacy systems — and then only with an
  explicit risk acknowledgement in the code comment and PR.
- Error handling MUST NOT expose internal state, stack traces, or connection
  strings to end users.
- Dependencies added by AI-generated code MUST be from actively maintained
  sources with no known critical CVEs at the time of addition.
- Code reviews (human or automated) MUST include a security perspective
  for any change that touches input handling, authentication, authorisation,
  cryptography, or file/network I/O.

**Rationale**: ISO 27002:2022 control A.8.28 (Secure coding) requires that
secure coding principles are applied to software development. LLMs routinely
generate code with buffer overflows, injection vulnerabilities, insecure
defaults, and deprecated cryptographic choices. Making secure coding an
explicit constitutional requirement ensures that AI-assisted development
produces code that is defensible under increasing cyber threat levels and
compatible with ISO 27001/27002 certification requirements.

Mandatory security documentation (Principle XII extensions):
- Every Level-2 project MUST maintain a **Security Checklist**
  (`security-checklist-template.md`) for code reviews touching security-relevant
  code. The checklist MUST cover the general section and all language-specific
  sections applicable to the project.
- Every Level-2 project MUST maintain a **Dependency Audit**
  (`dependency-audit-template.md`) that is updated before each release and at
  least monthly. The audit MUST cover CVE status, license compliance, registry
  verification, lock-file status, and supply-chain risks.
- Every Level-2 project SHOULD maintain **Security Quality Scenarios**
  (`security-quality-scenarios-template.md`) following iSAQB CPSA-F quality
  attribute scenario methodology to make security requirements testable and
  measurable.
- Secure-code reviews MUST use the matching checklist chapters from
  `docs/secure-development/` as the reusable review and hardening baseline
  when the checklist scope applies. The filled project evidence remains in
  `docs/security/` or the active Spec-Kit artefacts.
- Templates for these documents are located in `.specify/templates/` and
  project-specific instances are maintained in `docs/security/`.

### XIII. Secure Software Architecture (ISO 27001/27002 A.8.27)

AI-generated and human-written software architecture MUST follow established
secure-architecture principles. Secure code (Principle XII) without a secure
architecture is insufficient — both levels must work together to achieve
resilient systems. This principle aligns with ISO 27002:2022 control A.8.27
(Secure system architecture and engineering principles) and with the
iSAQB CPSA curriculum's treatment of security as a first-class quality
attribute.

Mandatory architectural principles:
- **Trust boundaries**: Every system MUST define explicit trust boundaries.
  All input crossing a trust boundary (user input, external API responses,
  file content, environment variables from untrusted sources) MUST be
  validated and sanitised before processing. Internal components behind the
  same trust boundary MAY trust each other.
- **Defense in depth**: Security MUST NOT depend on a single control. At least
  two independent layers MUST protect critical assets (e.g., input validation
  at the API gateway AND parameterised queries at the data-access layer).
- **Principle of least privilege**: Every component, service, user, and
  process MUST operate with the minimum permissions required for its function.
  Database connections MUST use role-specific accounts with restricted
  grants, not administrative credentials. File-system access MUST be scoped
  to required directories.
- **Fail-safe defaults**: Access MUST be denied by default and granted
  explicitly. Error paths MUST fall back to a secure state (deny access,
  close connection, return generic error) rather than an open or permissive
  state.
- **Attack surface reduction**: Unused endpoints, services, ports, and
  features MUST be disabled or removed. Public APIs MUST expose only the
  minimum required interface. Debug endpoints, verbose error output, and
  diagnostic tools MUST NOT be accessible in production configurations.
- **Separation of concerns**: Authentication, authorisation, logging, and
  input validation MUST be implemented as cross-cutting architectural
  concerns (middleware, filters, interceptors, decorators), not scattered
  ad-hoc across business logic. Security-relevant decisions MUST be
  centralised, not duplicated.
- **Secure configuration management**: Secrets (connection strings, API keys,
  tokens) MUST be stored in platform-appropriate secret stores (e.g., Azure
  Key Vault, macOS Keychain, environment-variable injection from CI/CD
  secrets), never in source code, configuration files tracked in Git, or
  hard-coded constants.
- **Dependency and supply-chain security**: All third-party dependencies MUST
  be sourced from verified package registries. Lock files (`packages.lock.json`,
  `package-lock.json`, `Cargo.lock`) SHOULD be committed. Known-vulnerable
  dependencies MUST be updated or replaced before release.

Language-specific architectural guidance:
- **C# / .NET**: Use ASP.NET Core middleware pipelines for authentication,
  authorisation, CORS, and anti-forgery. Prefer dependency injection for
  all security-relevant services. Use `IDataProtectionProvider` for
  encryption at rest. Configure HTTPS-only transport via `UseHttpsRedirection`.
- **C / C89 (cc65)**: Isolate external input handling in dedicated modules
  with bounds-checked buffer APIs. Minimise global mutable state. Use
  `const` annotations for read-only data.
- **SQL**: Enforce least-privilege at the schema level (separate read/write
  roles). Use stored procedures or parameterised views as API boundaries.
  Row-level security where the DBMS supports it.
- **Bash / PowerShell**: Treat all positional arguments and environment
  variables as untrusted at script entry. Validate and sanitise before
  passing to subprocesses. Use `--` sentinel to prevent option injection.

**Rationale**: ISO 27002:2022 control A.8.27 requires that organisations
establish, document, maintain, and apply secure architecture and engineering
principles to any information system development. The iSAQB CPSA Foundation
curriculum identifies security (confidentiality, integrity, availability) as
a mandatory quality attribute that must be addressed at the architecture
level, not just at the code level. Principles XII and XIII together form a
complete secure-development approach: XII ensures safe tactical code patterns,
XIII ensures the strategic system structure is defensible. In an environment
of increasing cyber threats, neither layer alone provides sufficient
resilience.

Mandatory security documentation (Principle XIII extensions):
- Every Level-2 project MUST maintain a **Threat Model**
  (`threat-model-template.md`) using the STRIDE methodology. The model MUST
  identify trust boundaries, assess risks per STRIDE category, document
  mitigations, and track residual risks.
- Security-relevant architectural decisions MUST be recorded as **Security
  Architecture Decision Records (S-ADR)** (`adr-template.md`) with context,
  decision, rationale, alternatives considered, consequences, and a
  compliance evidence table mapping to Constitution principles.
- Every Level-2 project MUST maintain an **arc42 Section 8 Security
  Cross-Cutting Concepts** document (`arc42-security-template.md`) covering
  authentication strategy, authorisation model, encryption (in-transit and
  at-rest), input validation, error handling, logging/audit trail, dependency
  management, and deployment security.
- Secure-architecture reviews MUST use the matching checklist chapters from
  `docs/secure-development/` as the reusable review and hardening baseline
  when the checklist scope applies. The filled project evidence remains in
  `docs/security/` or the active Spec-Kit artefacts.
- Templates for these documents are located in `.specify/templates/` and
  project-specific instances are maintained in `docs/security/`.
- S-ADRs are stored as individual files in `docs/security/adr/`.

### XIV. Secure Development Standards & Applicability Matrix

The following standards matrix defines which secure-development and
software-architecture standards are mandatory, recommended, or dependent on
the project type. Every Level-2 feature, plan, task list, review, and release
MUST use this matrix to determine which standards apply.

The generic secure-development guide in `docs/secure-development/` is the
reusable training, review, and hardening baseline for Level-2 work. Spec-Kit
runs MUST consider the relevant guideline, individual checklist, and compendium
items as review context. The guide does not replace project-specific evidence
in `docs/security/`; it defines the shared assessment logic, checklist context,
and `N/A` rationale expectations.

| Standard / guide | Priority | Applies when | Minimum expectation |
|---|---|---|---|
| NIST SSDF (SP 800-218) | MUST | All Level-2 projects | Secure SDLC work covers prepare, protect, produce, and respond practices |
| CWE Top 25 | MUST | All Level-2 projects | Relevant weaknesses are checked during design, implementation, review, and remediation |
| OWASP ASVS | MUST | Web, API, HTTP, or authentication-bearing services | Select and document an ASVS level and verification scope |
| SBOM | MUST | Release-capable or distributable artefacts | Generate machine-readable component inventory per release |
| AI-SBOM / G7 SBOM for AI Minimum Elements | Project-type-dependent | AI models, AI services, training or embedding datasets, inference infrastructure, or AI runtime components are part of the released or operated system | Assess AI-SBOM applicability; when applicable, record the seven G7/BSI clusters: metadata, system-level properties, models, datasets, infrastructure, security properties, and key performance indicators |
| VEX | MUST | Known vulnerabilities in shipped or evaluated components | Record whether the project is affected, not affected, mitigated, or under investigation |
| SLSA | SHOULD | CI/CD-built or published artefacts | Target build provenance and integrity controls; at least L1 where feasible |
| OWASP SAMM | SHOULD | Long-lived Level-1 and Level-2 workspaces/projects | Periodic self-assessment with prioritized improvement actions |
| CAPEC | SHOULD | Threat modeling of material attack paths | Reference relevant attack patterns for high-risk flows and abuse cases |
| NIST Zero Trust (SP 800-207) | Project-type-dependent | Distributed, service-based, cloud, remote-managed, or multi-device systems | Explicit applicability decision with controls or justified N/A |
| OWASP Cheat Sheet Series / Proactive Controls | SHOULD | All developer-facing projects | Use as day-to-day implementation guidance below the constitution |
| OpenSSF Scorecard | Project-type-dependent | Public OSS repositories or high-impact external dependencies | Review repository/dependency security posture before adoption or release |

Mandatory rules:
- Every Level-2 feature specification, plan, task list, PR, and release note
  MUST identify the applicable entries from this matrix and mark all other
  ambiguous entries as `N/A` with a short justification. Silent omission is
  not allowed.
- `NIST SSDF` and `CWE Top 25` are never `N/A` for Level-2 work.
- Where a standard applies, the implementation evidence MUST be reflected in
  the relevant artefacts: `spec.md`, `plan.md`, `tasks.md`, `docs/security/`,
  S-ADRs, release assets, or CI/CD configuration as appropriate.
- The default evidence location for Level-2 projects is `docs/security/` using
  the canonical filenames and templates defined in `.specify/templates/`.
  A repository MAY use an equivalent governance location only when
  `docs/security/` would be structurally inappropriate; in that case the
  alternative location MUST be explicitly linked from `docs/security/README.md`
  or equivalent repository-local index documentation.
- Level-1 workspaces SHOULD also prefer `docs/security/` for security-governance
  evidence. If a workspace uses another governance document or directory, the
  chosen location MUST be stated in its local security index.

**Rationale**: Secure-development standards are often partially remembered and
selectively applied. A binding applicability matrix keeps teams, agents, and
future templates aligned on what is always required, what is recommended, and
what depends on project shape rather than personal preference.

### XV. Secure SDLC & Verification Standards

Level-2 projects MUST integrate modern secure-development and verification
standards into the full software lifecycle, not only into final code review.

Mandatory rules:
- All Level-2 projects MUST align their secure-development lifecycle with
  `NIST SP 800-218` (Secure Software Development Framework, SSDF). Security
  work MUST cover preparation, source/build protection, secure production of
  software, and vulnerability response/improvement.
- All Level-2 projects MUST use the current `CWE Top 25` as a root-cause and
  prioritization lens for architecture, implementation, review checklists, and
  remediation planning. When a relevant Top-25 weakness applies, the chosen
  mitigations SHOULD be named in the checklist, threat model, ADR, or PR.
- Web, API, HTTP, and authentication-bearing services MUST select an `OWASP
  ASVS` verification level:
  - `ASVS Level 1` for simple or internal web applications with limited risk.
  - `ASVS Level 2` for authenticated, multi-user, privileged, internet-facing,
    or data-bearing services.
  - `ASVS Level 3` only when the project has explicit high-assurance,
    high-impact, or regulatory security requirements.
- Web/API projects MUST record the selected ASVS level and verification scope
  in `docs/security/` (for example as an ASVS verification matrix or
  equivalent repository-local format).
- Web/API projects MUST maintain an ASVS evidence document using
  `asvs-verification-template.md` or an equivalent repository-local format that
  captures scope, selected level, covered controls, gaps, and follow-up work.
- `OWASP Cheat Sheet Series` and `OWASP Proactive Controls` SHOULD be used as
  day-to-day implementation guidance wherever language/framework standards do
  not already provide stricter or more specific rules.

**Rationale**: SSDF provides the process frame, CWE Top 25 provides defect
prioritization, ASVS provides application verification depth, and OWASP cheat
sheets provide tactical implementation help. Together they reduce the gap
between abstract policy and day-to-day engineering decisions.

### XVI. Supply-Chain Transparency & Build Integrity

Secure development MUST include transparency about what is shipped and how it
was produced.

Mandatory rules:
- Every release-capable or distributable Level-2 project MUST generate a
  machine-readable `SBOM` for each released artefact set. The SBOM MAY be
  stored as a release asset and/or mirrored in `docs/security/`.
- When a released or evaluated component has a known vulnerability that is
  relevant to consumers or reviewers, the project MUST publish or record a
  `VEX`-style status statement indicating whether the product is affected,
  not affected, mitigated, or still under investigation.
- Projects with CI/CD-built or published artefacts SHOULD target `SLSA`
  controls for build integrity and provenance. At minimum, scripted/automated
  builds and provenance evidence SHOULD exist where tooling makes this
  practical; publicly consumed artefacts SHOULD aim for `SLSA L2` or better
  over time.
- Public OSS repositories and the adoption of high-impact external
  dependencies SHOULD consider `OpenSSF Scorecard` findings (or an equivalent
  source of repository security posture evidence) before release or adoption.
- Dependency, SBOM, AI-SBOM, VEX, provenance, and Scorecard evidence MUST feed into the
  repository's dependency audit and release review process.
- Release-capable projects MUST maintain a supply-chain evidence document using
  `supply-chain-evidence-template.md` or an equivalent repository-local format.
  That document MUST reference the current SBOM, AI-SBOM applicability/evidence, VEX decisions, provenance or
  SLSA status, and any relevant OpenSSF Scorecard observations.

**Rationale**: A project can follow secure coding rules and still ship opaque
or tampered artefacts. SBOM, AI-SBOM, VEX, SLSA, and Scorecard address transparency,
integrity, and supplier trustworthiness across the software supply chain.
The G7/BSI AI-SBOM minimum elements extend that transparency to AI-specific dependencies without creating a blanket requirement for development-tool-only AI usage.

### XVII. Threat Modeling & Attack Pattern Coverage

Threat modeling MUST describe both what the system values and how it can be
attacked.

Mandatory rules:
- Every Level-2 threat model MUST use `STRIDE` as the base categorization
  scheme, as already required by Principle XIII.
- Threat models SHOULD reference relevant `CAPEC` attack patterns for the
  highest-risk trust boundaries, abuse cases, or externally reachable flows.
  The goal is not exhaustive mapping, but explicit coverage of realistic
  attacker techniques.
- Threat models MUST be updated when authentication, authorization,
  privilege boundaries, deployment topology, externally reachable endpoints,
  sensitive data flows, or third-party integrations materially change.
- Security-relevant mitigations and residual risks identified through STRIDE
  or CAPEC analysis SHOULD be reflected in S-ADRs, checklists, and tasks.
- Threat-model evidence SHOULD capture CAPEC references directly in the threat
  model document, not only in ADRs or tasks, so the attacker-technique mapping
  stays reviewable in one place.

**Rationale**: STRIDE is strong for systematic coverage of threat categories;
CAPEC complements it by adding attacker behavior and attack-pattern language.
Using both helps avoid sterile threat models that classify risks but fail to
anticipate realistic exploitation paths.

### XVIII. Zero Trust Applicability & Security Program Maturity

Secure architecture is not static; it must account for modern distributed
access patterns and continuously improve over time.

Mandatory rules:
- Distributed, service-based, cloud, remote-managed, multi-device, or
  identity-federated systems MUST explicitly evaluate the applicability of
  `NIST SP 800-207` (Zero Trust Architecture). The architecture documentation
  MUST record either the relevant zero-trust controls or a justified `N/A`
  decision.
- Where zero-trust principles apply, systems MUST NOT rely on implicit trust
  based solely on network location. User, workload, and device access SHOULD
  be authenticated and authorized before access to protected resources, and
  policy decisions SHOULD be observable in logging or audit evidence.
- Long-lived Level-1 workspaces and Level-2 projects SHOULD perform periodic
  `OWASP SAMM` self-assessments and maintain a short, prioritized improvement
  backlog or assessment note in `docs/security/` or equivalent governance
  documentation.
- Findings from incidents, audits, dependency reviews, and SAMM assessments
  SHOULD feed back into templates, checklists, security docs, and AI-agent
  guidance files so improvements become structural rather than one-off fixes.
- Systems where Zero Trust applicability is material SHOULD maintain a
  dedicated applicability note using `zero-trust-applicability-template.md` or
  an equivalent repository-local format.
- Repositories performing periodic SAMM reviews SHOULD maintain their current
  assessment snapshot and follow-up actions using `samm-assessment-template.md`
  or an equivalent repository-local format.

**Rationale**: Zero Trust addresses the realities of remote access, services,
and cloud deployment; SAMM addresses the maturity of the development program
itself. Together they keep security architecture and security process moving
forward instead of freezing at a one-time baseline.

### XIX. EU Cyber Resilience Act (CRA) Compliance Awareness

Software placed on the EU market is subject to the Cyber Resilience Act
(Regulation (EU) 2024/2847), which establishes mandatory cybersecurity
requirements for products with digital elements. This principle requires
that all workspace projects maintain awareness of CRA applicability and
align their practices accordingly.

Mandatory rules:
- AI functionality, models, datasets, inference infrastructure, and runtime AI
  components MUST be assessed for `AI-SBOM` applicability where they are part
  of software placed on the EU market. The G7/BSI AI-SBOM minimum elements do
  not create direct legal obligations by themselves, but they are a target
  architecture for transparent AI supply-chain evidence and support readiness
  for the EU AI Act, CRA, and sector-specific rules.
- All projects MUST assess whether their software qualifies as a "product
  with digital elements" under the CRA (commercial sale, licensing, or
  free distribution for economic purposes within the EU market). Even
  open-source projects distributed for economic benefit may fall in scope.
- CRA-scoped projects MUST generate SBOMs for each released version (see
  Principle XVI — this requirement applies at all levels for this reason).
- CRA-scoped projects MUST implement a documented vulnerability disclosure
  and handling process. Actively exploited vulnerabilities MUST be reported
  to relevant authorities within 24 hours and patched within established
  deadlines per the CRA.
- CRA-scoped projects MUST document their conformity assessment approach
  (self-assessment for most products; third-party assessment for critical
  or important products under Annex III/IV of the CRA).
- All projects SHOULD align security practices with CRA principles
  regardless of formal scope applicability, as the CRA reflects emerging
  industry baseline expectations for secure software development:
  secure-by-design, secure-by-default, vulnerability management, lifecycle
  transparency, and SBOM availability.
- The CRA applicability decision MUST be recorded in `docs/security/` or
  equivalent governance documentation (e.g., as a note in the supply-chain
  evidence document or a dedicated S-ADR).

**Rationale**: The EU Cyber Resilience Act (in force since December 2024,
with compliance deadlines phased through 2027) is the most significant
EU regulatory development in software security since GDPR. It codifies
many existing best practices — SBOM, vulnerability disclosure, secure
development lifecycle, security-by-design — as legal obligations for
software placed on the EU market. Recording CRA applicability and aligning
practices proactively reduces legal and reputational risk and builds on the
security work already required by Principles XII–XVIII.

### XX. General Architecture Governance (iSAQB / arc42)

Software architecture MUST be treated as an explicit, reviewable work product
whenever a change affects structure, interfaces, quality attributes, runtime
behavior, deployment, long-term maintainability, or technical debt. This
general architecture governance is separate from, and complementary to, the
secure-architecture rules in Principle XIII.

Mandatory rules:
- Architecture work SHOULD follow lightweight iSAQB/CPSA-F method discipline
  and arc42-compatible documentation where this improves reviewability.
- Architecturally significant decisions MUST be documented as architecture
  decision records under `docs/architecture/adr/`, unless the feature plan
  records a justified `N/A` decision.
- Architecture goals, system context, building blocks, runtime views,
  deployment constraints, and interface boundaries MUST be documented when
  they materially affect acceptance, maintenance, onboarding, or later change
  decisions.
- Quality attributes MUST be expressed as concrete scenarios with stimulus,
  environment, response, and measurable outcome. Generic claims such as
  "fast", "robust", or "maintainable" are not sufficient on their own.
- Architecture risks, accepted trade-offs, and technical debt MUST be recorded
  with owner, impact, mitigation, and review trigger when they remain after a
  feature is completed.
- The default architecture evidence location is `docs/architecture/`.
  Templates are maintained in `.specify/templates/`:
  `architecture-vision-template.md`, `context-view-template.md`,
  `building-block-view-template.md`, `runtime-view-template.md`,
  `deployment-view-template.md`, `quality-scenarios-template.md`,
  `architecture-decision-template.md`, and
  `architecture-risks-template.md`.
- If a feature affects security-relevant architecture, Principle XIII and the
  security evidence under `docs/security/` MUST also be applied. General
  architecture ADRs and security ADRs MAY cross-reference each other but MUST
  keep their evidence locations explicit.
- Spec-Kit `spec.md`, `plan.md`, and `tasks.md` MUST record whether
  architecture evidence is required. Silent omission is not allowed; `N/A`
  decisions require a short rationale.

**Rationale**: Security architecture alone does not cover all architectural
work. TuiVision and the Level-2 workspace family also need visible reasoning
about module boundaries, runtime behavior, documentation structure, quality
attributes, and accepted technical debt. Lightweight iSAQB/arc42 evidence
keeps these decisions reviewable without forcing heavyweight documentation for
small changes.

## Level-2 Project Environment Registry / Level-2-Projektumgebungsregister

This registry consolidates the constitution-relevant Level-2 project facts
extracted from the project-local `.specify/memory/constitution.md` files.
Spec-Kit planning and agent-generated work MUST use the matching row as binding
project context.

| Level-2 Project | Runtime / Language | Build & Test Baseline | Docs / A11Y Baseline | Statistics Baseline | Agent Surfaces |
|---|---|---|---|---|---|
| `C64Projects/cc65` | C/C89-oriented host tools, 6502 assembler/runtime libraries, C64 and 8-bit target support | GNU `make`; `make`, `make test`, `make check`, `make checkstyle`, `make -C targettest SYS=c64` | `doc/`, `samples/`, generated `html/`; DE-first/EN-second additions where local scope allows; no color-only meaning | Manual conservative `80` lines/workday; no C# default unless a justified Thorsten-Solo baseline is documented | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, Spec-Kit command/prompt surfaces |
| `DataGripProjects/InventarDb` | Database/schema artefacts with C#/.NET Framework integration context where documented | Repository-specific DataGrip/database validation plus homogeneity checks after agent-guidance changes | SQL, documentation, generated templates, and reports remain text-first, bilingual where user-facing, and WCAG 2.2 AA-oriented where applicable | Manual conservative `80` lines/workday; C#/.NET integration work uses `125` lines/workday unless justified otherwise | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, Spec-Kit surfaces |
| `RiderProjects/InventarWorkerService` | .NET 10 / C# 14 multi-project inventory solution: worker/API, harvester, Terminal UI, shared libraries, SQLite/MongoDB/PostgreSQL | `dotnet restore/build/test` on `InventarWorkerService.sln`; MSTest unit/integration tests; Playwright setup when required | DocFX output and learner-facing docs require text-oriented A11Y review; generated `api/` and `_site/` remain build artefacts | Manual conservative `80`; repo-specific Thorsten-Solo `100` lines/workday unless all agent files change it | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, Spec-Kit surfaces |
| `RiderProjects/TinyCalc` | .NET 10 / C# spreadsheet and Terminal.Gui TUI port; Pascal reference artefacts for behaviour parity | `dotnet restore/build/test MicroCalc.sln`; xUnit suites; non-interactive TUI smoke mode | DocFX changes require text-oriented A11Y smoke review; documentation and didactic comments stay DE-first/EN-second at CEFR B2 | Manual conservative `80`; Thorsten-Solo `125` lines/workday for this Pascal-derived C#/.NET port | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, Spec-Kit surfaces |
| `RiderProjects/TinyPl0` | .NET 10 / C# 14 compiler, VM, CLI, and Terminal.Gui IDE for PL/0 | `dotnet restore/build/test`; coverage collection; `scripts/update-golden-code.sh` for intentional compiler-output changes | Learner-facing compiler docs, examples, generated API docs, and IDE flows follow DE-first/EN-second and WCAG 2.2 AA-oriented review | Manual conservative `80`; C#/.NET Thorsten-Solo `125` unless all agent files justify a deviation | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.codex` prompt/rule surfaces, Spec-Kit surfaces; `.codex` credentials/logs/history/SQLite state are forbidden |
| `RiderProjects/TuiVision` | .NET 10 / C# terminal UI framework and Turbo Vision port: framework libraries, managed console driver, compatibility, controls, serialization, examples | `dotnet restore/build/test`; MSTest suites; Coverlet coverage gates for core assemblies; `dotnet format` where configured | DocFX regeneration requires Playwright + axe and lynx-oriented A11Y smoke review for generated documentation | Manual conservative `80`; C#/.NET Thorsten-Solo `125` unless all agent files justify a deviation | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`, Spec-Kit surfaces |
| `RiderProjects/WebApplication1` | .NET 10 / C# single-project ASP.NET Core MVC application | `dotnet build`; `dotnet build -c Release/Debug`; `dotnet run --project WebApplication1/WebApplication1.csproj` | HTML views, documentation, templates, and UI output follow WCAG 2.2 AA where applicable and stay keyboard/AT usable | Manual conservative `80`; C#/.NET Thorsten-Solo `125` unless all agent files justify a deviation | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, Spec-Kit surfaces |
| `RiderProjects/inventarworkerservice2` | .NET 9.0 via `global.json`; cross-platform Worker Service; Windows/Systemd hosting; hardware inventory; PowerShell SDK; YAML/JSON status output | `dotnet build InventarWorkerService2.sln`; `dotnet run --project InventarWorkerService2/InventarWorkerService2.csproj`; CI includes Gitleaks and agent-secret-scan | CLI/service status output, generated templates, docs, JSON/YAML reports, and logs remain text-first and accessibility-aware | Manual conservative `80`; C#/.NET Thorsten-Solo `125` unless all agent files justify a deviation | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.opencode`, Spec-Kit surfaces; credentials/history/logs/SQLite state are forbidden |
| `RiderProjects/sysinfotool` | .NET 10 / C# 14 cross-platform system-information CLI; Spectre.Console; Windows/macOS/Linux/FreeBSD services; DE/EN localization | `dotnet restore/build/test`; coverage collection; `docfx docfx.json`; GitLab CI stages `build/test/docs/scan` | Generated DocFX HTML targets WCAG 2.2 AA; DocFX regeneration requires Playwright + axe and lynx-oriented review where applicable | Manual conservative `80`; repo-specific Thorsten-Solo `100` lines/workday for this .NET CLI codebase | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.opencode`, Spec-Kit surfaces; credentials/history/logs/SQLite state are forbidden |

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

**Spec Kit preset governance**: The standard governance preset set for this
workspace family consists of:

| Preset | Version | Priority | Scope |
|---|---:|---:|---|
| `security-governance` | `v0.6.0` | `10` | secure development, MSL, language-specific secure coding, SSDF, ASVS, SBOM/VEX/SLSA, AI-SBOM, CRA/regulatory applicability |
| `architecture-governance` | `v0.5.0` | `20` | secure architecture, STRIDE/CAPEC, Zero Trust, SAMM, S-ADR, BSI C3A cloud autonomy, BSI C5 cloud assurance |
| `isaqb-architecture-governance` | `v0.2.0` | `30` | general iSAQB/arc42 architecture governance |
| `a11y-governance` | `v0.4.0` | `40` | WCAG 2.2 AA, bilingual DE/EN, CEFR B2, inclusive artefacts, didactic inline-code-comment review |
| `cross-platform-governance` | `v0.2.0` | `50` | Bash/PowerShell parity, macOS/Linux/Windows script governance |
| `agent-parity-governance` | `v0.3.0` | `60` | synchronized agent guidance and agent-neutral Spec-Kit model routing across declared AI-agent files |

All six governance presets MUST produce or require audit-ready Spec-Kit run evidence for applicable checks. Each relevant checkpoint is recorded as `Applicable`, `N/A`, or `Open` with rationale, evidence path, reviewer, residual risk, and follow-up for the current Spec-Kit run.

This matrix is the authoritative source for preset IDs, versions, priorities,
and scope. Any preset version or priority change MUST update the matching
compact overview and installation snippets in `README.md`,
`.specify/memory/constitution.md`, the four agent guidance files,
`scripts/templates/speckit-workflow-section.md`, and the matching agent
templates under `scripts/templates/` in the same change.

All six presets are published as standalone repositories under
`https://github.com/hindermath/spec-kit-preset-*` and are included in the
`github/spec-kit` community preset catalog as of 2026-05-04. New Level-2
projects SHOULD install the applicable subset during Spec-Kit initialization.
For C#/.NET Level-2 projects, the default subset is all six presets unless the
project documents a narrow reason to omit one. Existing C# Level-2 projects
`RiderProjects/TinyPl0`, `RiderProjects/TinyCalc`, `RiderProjects/TuiVision`,
and `RiderProjects/InventarWorkerService` are the reference rollout set for
all-six installation.

Use the community catalog when supported by the local `specify` CLI. Use
versioned GitHub ZIP URLs when a reproducible release pin is required. Commit
`.specify/presets/` and all generated agent-command updates when presets are
project policy. Do not commit `.specify/presets/.cache/`. Preset updates MUST
be verified with `specify preset list`, at least one `specify preset info`, and
where relevant `specify preset resolve`.

Local working clones of the published preset repositories live under
`~/SpecKitPresetProjects/`. Canonical scaffolds in this repository live under
`specs/spec-kit-presets/` and `specs/spec-kit-preset-repos/`. Preset
improvements SHOULD be made in the home-baseline scaffold first, propagated to
the affected standalone preset repositories, committed, pushed, and smoke-tested
via the GitHub ZIP URL before use in dependent projects. Preset-rule changes
MUST review whether `constitution.md`, `.specify/memory/constitution.md`,
`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, and
the relevant templates under `scripts/templates/` need matching updates.
Community/catalog coordination is tracked in `github/spec-kit#2362`.

**Runtime guidance**: Use `AGENTS.md` / `CLAUDE.md` / `GEMINI.md` /
`.github/copilot-instructions.md` for per-agent operational guidance. This
constitution is the authoritative policy layer above all agent-specific files.

**Version**: 1.14.0 | **Ratified**: 2026-03-31 | **Last Amended**: 2026-05-22

## Level-2 Project Environment Addendum / Level-2-Projektumgebung

- Projekt / Project: TuiVision .NET terminal UI framework and Turbo Vision port.
- Laufzeit und Sprache / Runtime and language: .NET 10 / C#, framework libraries under src/, managed console driver, compatibility layers, controls, serialization, and examples.
- Build und Tests / Build and tests: dotnet restore/build/test; MSTest suites; coverlet coverage gates for core assemblies; dotnet format where configured.
- Doku und A11Y / Docs and A11Y: DocFX regeneration requires Playwright + axe and lynx-oriented A11Y smoke review for generated documentation.
- Statistik / Statistics: manual conservative baseline 80 lines/workday; C#/.NET Thorsten-Solo baseline 125 lines/workday unless all agent files document a justified deviation.
- Agentenflaechen / Agent surfaces: AGENTS.md, CLAUDE.md, GEMINI.md, .github/copilot-instructions.md, .github/agents/copilot-instructions.md, and Spec-Kit surfaces stay synchronized for shared rules.
