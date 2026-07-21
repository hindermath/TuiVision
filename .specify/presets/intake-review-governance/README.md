# Intake Review Governance Preset

Optional, stackable intake-quality governance for GitHub Spec Kit. Version
`0.1.0` adds three commands, eight templates, and read-only Bash/PowerShell
validators. Recommended priority: `65`, between Agent Parity (`60`) and
Autonomous Run Governance (`70`). Spec Kit `>=0.8.3` is required.

## Commands

- `$speckit-intake-review`: read-only semantic review for a single intake,
  ordered series, or parallel campaign.
- `$speckit-intake-repair`: explicitly authorized repair followed by mandatory
  full re-review.
- `$speckit-intake-review-status`: read-only freshness and consumer-gate check.

## Install

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-intake-review-governance/archive/refs/tags/v0.1.0.zip --priority 65
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
