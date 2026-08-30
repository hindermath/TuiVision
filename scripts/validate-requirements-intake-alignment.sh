#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

node scripts/render-requirements-intake-governance.mjs
node scripts/validate-requirements-intake-alignment.mjs

for receipt in specs/intake-authoring-receipts/*.json; do
  target_path="$(node -e 'const fs=require("fs"); console.log(JSON.parse(fs.readFileSync(process.argv[1], "utf8")).target.path)' "$receipt")"
  if [[ -f "$target_path" ]]; then
    bash .specify/presets/intake-authoring-governance/scripts/validate-intake-authoring-receipt.sh \
      --receipt "$receipt" --repo "$repo_root"
  else
    printf 'historical completed intake receipt PASS: %s\n' "$receipt"
  fi
done

bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-manifest.sh \
  --file requirements/intakes/series/tui-vision-delivery/manifest.json --repo "$repo_root"
bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-receipt.sh \
  --file requirements/intakes/series/tui-vision-delivery/receipt.json --repo "$repo_root"
bash .specify/presets/intake-review-governance/scripts/validate-intake-review-result.sh \
  --result requirements/intakes/series/tui-vision-delivery/intake-review-result.json --repo "$repo_root"
