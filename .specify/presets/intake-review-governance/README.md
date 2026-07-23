# Intake Review Governance Preset

Optional, stackable intake-quality governance for GitHub Spec Kit. Version
`0.1.1` provides three commands, eight templates, and read-only
Bash/PowerShell validators. Series reviews use a schema-1.1 request binding
with normalized SHA-256, explicit roots, complete target ordering, and
validated acyclic dependency edges. Single and Campaign schema 1.0 results
remain compatible. Recommended priority: `65`, between Agent Parity (`60`) and
Autonomous Run Governance (`70`). Spec Kit `>=0.8.3` is required.

## Commands

- `$speckit-intake-review`: read-only semantic review for a single intake,
  ordered series, or parallel campaign.
- `$speckit-intake-repair`: explicitly authorized repair followed by mandatory
  full re-review.
- `$speckit-intake-review-status`: read-only freshness and consumer-gate check.

## Install

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-intake-review-governance/archive/refs/tags/v0.1.1.zip --priority 65
specify preset list
specify preset resolve
```

Installing the preset does not silently change the standard eight-preset
profile. Projects activate the gate through the policy template; campaigns may
activate it through their schema-1.2 `intakeReview` object.

## Safety

Review and status never modify intake targets. Repair needs explicit mutation
authority. An agent cannot accept residual risk or invent an operator
exception. The validators are read-only and grant no delivery authority.

An accepted Series result must use schema 1.1 and bind its repository-relative
request through `requestEvidence`. The validators reject request drift,
identity or role mismatch, incomplete ordering, unknown or duplicate edges,
cycles, and roots that differ from the graph's zero-indegree targets.
