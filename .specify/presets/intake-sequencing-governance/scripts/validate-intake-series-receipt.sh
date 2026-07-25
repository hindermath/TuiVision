#!/usr/bin/env bash
# Validate one intake-series receipt and its bound manifest without writes.
set -euo pipefail

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
exec python3 "$script_dir/validate-intake-series.py" --kind receipt "$@"
