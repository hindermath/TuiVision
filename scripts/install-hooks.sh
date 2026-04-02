#!/usr/bin/env bash
# Installiert die Git-Hooks aus scripts/hooks/ in .git/hooks/.
# Auf jedem neuen Gerät nach dem Clonen einmalig ausführen:
#   bash scripts/install-hooks.sh

set -euo pipefail

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"
HOOKS_SRC="$REPO_ROOT/scripts/hooks"
HOOKS_DST="$REPO_ROOT/.git/hooks"

if [ ! -d "$HOOKS_DST" ]; then
  echo "Fehler: $HOOKS_DST nicht gefunden. Ist dies ein Git-Repository?" >&2
  exit 1
fi

installed=0
for hook in "$HOOKS_SRC"/*; do
  name="$(basename "$hook")"
  target="$HOOKS_DST/$name"
  cp "$hook" "$target"
  chmod +x "$target"
  echo "✓ $name installiert → $target"
  installed=$((installed + 1))
done

echo ""
echo "$installed Hook(s) erfolgreich installiert."
