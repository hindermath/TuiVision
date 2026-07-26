#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

node scripts/render-requirements-intake-governance.mjs
node scripts/validate-requirements-intake-alignment.mjs

for receipt in specs/intake-authoring-receipts/*.json; do
  bash .specify/presets/intake-authoring-governance/scripts/validate-intake-authoring-receipt.sh \
    --receipt "$receipt" --repo "$repo_root"
done

bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-manifest.sh \
  --file requirements/intakes/series/tui-vision-delivery/manifest.json --repo "$repo_root"
bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-receipt.sh \
  --file requirements/intakes/series/tui-vision-delivery/receipt.json --repo "$repo_root"
bash .specify/presets/intake-review-governance/scripts/validate-intake-review-result.sh \
  --result requirements/intakes/series/tui-vision-delivery/intake-review-result.json --repo "$repo_root"
