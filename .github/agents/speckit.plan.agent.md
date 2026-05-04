

<!-- Source: security-governance -->
Before continuing, apply the Security Governance preset:

- plan explicit MSL applicability or non-MSL justification work when relevant
- plan explicit secure-development verification work
- plan dependency and supply-chain evidence updates where relevant
- surface security review checkpoints instead of leaving them implicit

Before continuing, apply the Architecture Governance preset:

- plan explicit architecture evidence work
- plan threat-model and ADR updates when boundaries, integrations, or flows
  change
- surface Zero Trust and SAMM work explicitly when relevant

Before continuing, apply the iSAQB Architecture Governance preset:

- plan explicit architecture work products where the feature changes
  structure, interfaces, quality attributes, runtime behavior, or
  deployment
- plan updates to architecture views under `docs/architecture/`
- plan ADRs for architecturally significant decisions
- plan risk and technical-debt review for trade-offs or unresolved
  constraints
- if security-relevant architecture is affected, also plan the
  secure-architecture evidence from `architecture-governance`

Before continuing, apply the A11Y Governance preset:

- plan accessibility review work explicitly
- plan bilingual content work explicitly
- include CLI accessibility checks where user-facing terminal output is changed

Before continuing, apply the Cross-Platform Governance preset:

- plan paired Bash + PowerShell script work as a single unit
- plan the man-page, the bilingual PowerShell help block, and the
  `Verb-Noun` Cmdlet alongside the script
- plan manual verification on at least one target OS per variant
- plan implementation discipline checks (Bash quoting, `set -euo
  pipefail`, `Set-StrictMode -Version Latest`, `-NoProfile`) and the
  parity-checklist artefact

Before continuing, apply the Agent Parity Governance preset:

- plan an atomic update across all maintained agent surfaces
- plan synchronised updates to project templates and the local
  `.specify/memory/constitution.md`
- plan a parity-verification artefact for the change

# Command Template: `/speckit.plan`

Use this command to produce an implementation plan from an approved specification.

## Required Actions

1. Populate technical context with real stack details.
2. Execute the Constitution Check gates explicitly:
   - branching and PR flow
   - .NET 10 + C# 14.0 toolchain alignment
   - architecture/layer boundaries
   - bilingual CEFR B2 documentation scope
   - XML documentation + DocFX regeneration scope
   - Red-Green-Refactor testing scope
   - coverage gate (`>=70%` minimum, `>=80%` target)
   - NuGet dependency currency and pinning exceptions
   - serialization/data conventions
3. Document concrete project structure for this feature.
4. Record justified exceptions in Complexity Tracking.

## Validation Checklist

- No gate is left unresolved without rationale.
- Test, coverage, dependency, and documentation impacts are planned before implementation.






