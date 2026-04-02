#!/usr/bin/env bash
# rename-lastenheft.sh — Lastenheft umbenennen / Rename Lastenheft
# FR-REV-B03; Contract: rename-lastenheft-cli.md
# Usage: bash scripts/rename-lastenheft.sh <lh-file> <branch-name>

set -euo pipefail

LH_FILE="${1:-}"
BRANCH_NAME="${2:-}"

if [ -z "$LH_FILE" ] || [ -z "$BRANCH_NAME" ]; then
  echo "Fehler: Verwendung: bash scripts/rename-lastenheft.sh <lh-file> <branch-name>" >&2
  echo "Error: Usage: bash scripts/rename-lastenheft.sh <lh-file> <branch-name>" >&2
  exit 1
fi

if [ ! -f "$LH_FILE" ]; then
  echo "Fehler: Datei nicht gefunden: ${LH_FILE}" >&2
  echo "Error: File not found: ${LH_FILE}" >&2
  exit 1
fi

# Extract stem (filename without .md extension)
filename=$(basename "$LH_FILE")
stem="${filename%.md}"
new_name="${stem}.${BRANCH_NAME}.md"
target_dir=$(dirname "$LH_FILE")
new_path="${target_dir}/${new_name}"

if [ "$LH_FILE" = "$new_path" ]; then
  echo "INFO: Datei bereits korrekt benannt: ${filename}"
  exit 0
fi

git mv "$LH_FILE" "$new_path"
git commit -m "chore: rename Lastenheft to ${new_name}

Co-authored-by: Copilot <223556219+Copilot@users.noreply.github.com>"

echo "✓ Umbenannt: ${filename} -> ${new_name}"
