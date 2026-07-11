#!/usr/bin/env bash
set -euo pipefail

python3 -c '
import json
import os
import sys

try:
    data = json.load(sys.stdin)
except (json.JSONDecodeError, OSError):
    print("agy | status unavailable")
    raise SystemExit(0)

model = data.get("model") or {}
workspace = data.get("workspace") or {}
vcs = data.get("vcs") or {}
context = data.get("context_window") or {}
sandbox = data.get("sandbox") or {}
cwd = workspace.get("current_dir") or data.get("cwd") or "?"
parts = [
    "agy " + str(data.get("version") or "?"),
    str(model.get("display_name") or model.get("id") or "model:?"),
    str(data.get("agent_state") or "state:?"),
    os.path.basename(cwd.rstrip(os.sep)) or cwd,
]
branch = vcs.get("branch")
if branch:
    parts.append("git:" + str(branch) + ("*" if vcs.get("dirty") else ""))
remaining = context.get("remaining_percentage")
if isinstance(remaining, (int, float)):
    parts.append("ctx:" + format(remaining, ".0f") + "%")
parts.append("sandbox:on" if sandbox.get("enabled") else "sandbox:off")
print(" | ".join(parts))
'
