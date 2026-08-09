---
description: Inspect harness-local model routing without changing files
---

# Model Routing Status

Inspect the current harness, its version, discovery capability, available
models and reasoning levels, installed `model-routing.json` catalogs, and the
effective local role bindings. This command is strictly read-only. Do not
create or refresh a profile, do not invoke a model, and do not change Git or
remote state.

Resolve the script from the repository's top-level `scripts` directory when it
exists; otherwise use
`.specify/presets/model-routing-governance/scripts/resolve-model-routing.ps1`
or its Bash wrapper. Run `-Action Status`. Report `Aligned`, `RefreshRequired`,
`NeedsClarification`, or `Blocked`, followed by a text-first reason and exact
next action.
