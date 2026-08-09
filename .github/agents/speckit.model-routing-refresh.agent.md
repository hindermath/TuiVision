---
description: Refresh the local harness model-to-role binding
---


<!-- Source: model-routing-governance -->
# Model Routing Refresh

Refresh exactly one local, non-versioned model-routing profile under current
explicit authority. Resolve the script from repository `.specify/scripts/` first and
otherwise from `.specify/presets/model-routing-governance/scripts/`. Detect the
active harness, enumerate models where the
harness supports it, and otherwise validate only configured candidates.

Automatically accept only a known, unique mapping whose model and reasoning
effort pass the adapter checks. Unknown, ambiguous, unavailable, or
cross-provider mappings must fail closed with a proposal. Never write
credentials, machine identifiers, private paths, or repository files. Do not
start Spec Kit, autonomous delivery, commits, pushes, pull requests, or merges.