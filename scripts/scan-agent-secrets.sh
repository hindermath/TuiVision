#!/usr/bin/env bash

set -euo pipefail

fail_on_high=0
workspace_root="."

while [ "$#" -gt 0 ]; do
  case "$1" in
    --fail-on-high)
      fail_on_high=1
      ;;
    -h|--help)
      echo "Usage: $0 [--fail-on-high] [workspace-root]"
      exit 0
      ;;
    *)
      workspace_root="$1"
      ;;
  esac
  shift
done

if ! command -v rg >/dev/null 2>&1; then
  echo "error: rg is required" >&2
  exit 1
fi

if ! command -v find >/dev/null 2>&1; then
  echo "error: find is required" >&2
  exit 1
fi

workspace_root="$(cd "$workspace_root" && pwd)"

agent_dir_patterns=(
  '.claude'
  '.codex'
  '.gemini'
  '.junie'
  '.opencode'
)

declare -a agent_dirs=()
while IFS= read -r dir; do
  agent_dirs+=("$dir")
done < <(
  find "$workspace_root" \
    \( -path '*/.git' -o -path '*/bin' -o -path '*/obj' -o -path '*/node_modules' -o -path '*/_site' -o -path '*/TestResults' \) -prune -o \
    \( -name '.claude' -o -name '.codex' -o -name '.gemini' -o -name '.junie' -o -name '.opencode' -o -path '*/.github/agents' -o -path '*/.github/prompts' \) \
    -type d -print | sort
)

if [ "${#agent_dirs[@]}" -eq 0 ]; then
  echo "No agent directories found under $workspace_root"
  exit 0
fi

secret_name_regex='(^|/)(auth\.json|\.env(\..*)?|[^/]*(secret|credential|creds)[^/]*|[^/]*id_rsa[^/]*|[^/]*\.(pem|key|p12|pfx))$'
secret_content_regex='(id_token|access_token|refresh_token|api[_-]?key|client[_-]?secret|password|authorization:|bearer|sk-[A-Za-z0-9]{20,}|ghp_[A-Za-z0-9]{20,}|github_pat_[A-Za-z0-9_]{20,}|AIza[0-9A-Za-z_-]{20,}|AKIA[0-9A-Z]{16}|-----BEGIN (RSA|OPENSSH|EC|DSA) PRIVATE KEY-----)'

high_count=0
medium_count=0
low_count=0

print_result() {
  local risk="$1"
  local path="$2"
  local reason="$3"
  printf '%-6s | %s | %s\n' "$risk" "$path" "$reason"
}

join_reasons() {
  local first=1
  local reason
  for reason in "$@"; do
    if [ -n "$reason" ]; then
      if [ "$first" -eq 1 ]; then
        printf '%s' "$reason"
        first=0
      else
        printf '; %s' "$reason"
      fi
    fi
  done
}

for dir in "${agent_dirs[@]}"; do
  rel_dir="${dir#$workspace_root/}"
  [ "$rel_dir" = "$dir" ] && rel_dir="."

  files=()
  while IFS= read -r line; do
    rel_file="${line#$dir/}"
    case "$rel_file" in
      */.claude/*|*/.codex/*|*/.gemini/*|*/.junie/*|*/.opencode/*|*/.github/agents/*|*/.github/prompts/*)
        continue
        ;;
    esac
    files+=("$line")
  done < <(find "$dir" -type f | sort)

  if [ "${#files[@]}" -eq 0 ]; then
    low_count=$((low_count + 1))
    print_result "low" "$rel_dir" "empty agent directory"
    continue
  fi

  has_high=0
  has_medium=0
  declare -a reasons=()

  high_name_hits=()
  while IFS= read -r line; do
    high_name_hits+=("$line")
  done < <(printf '%s\n' "${files[@]}" | sed "s#^$workspace_root/##" | rg -i "$secret_name_regex" || true)
  if [ "${#high_name_hits[@]}" -gt 0 ]; then
    has_high=1
    reasons+=("secret-like file present ($(basename "${high_name_hits[0]}"))")
  fi

  high_content_hits=()
  while IFS= read -r line; do
    rel_file="${line#$dir/}"
    case "$rel_file" in
      */.claude/*|*/.codex/*|*/.gemini/*|*/.junie/*|*/.opencode/*|*/.github/agents/*|*/.github/prompts/*)
        continue
        ;;
    esac
    high_content_hits+=("$line")
  done < <(rg -I -l -i "$secret_content_regex" "$dir" || true)
  if [ "${#high_content_hits[@]}" -gt 0 ]; then
    has_high=1
    reasons+=("secret field or token pattern present ($(basename "${high_content_hits[0]}"))")
  fi

  settings_hits=()
  while IFS= read -r line; do
    settings_hits+=("$line")
  done < <(printf '%s\n' "${files[@]}" | sed "s#^$workspace_root/##" | rg '/settings\.local\.json$' || true)
  if [ "${#settings_hits[@]}" -gt 0 ]; then
    has_medium=1
    reasons+=("local agent permissions/config ($(basename "${settings_hits[0]}"))")
  fi

  memory_hits=()
  while IFS= read -r line; do
    memory_hits+=("$line")
  done < <(printf '%s\n' "${files[@]}" | sed "s#^$workspace_root/##" | rg '(/memory/|/sessions/|/shell_snapshots/|history\.jsonl$|\.log$|state_[0-9]+\.sqlite(-shm|-wal)?$)' || true)
  if [ "${#memory_hits[@]}" -gt 0 ]; then
    has_medium=1
    reasons+=("agent memory, logs, or state data present ($(basename "${memory_hits[0]}"))")
  fi

  if [ "$has_high" -eq 1 ]; then
    high_count=$((high_count + 1))
    print_result "high" "$rel_dir" "$(join_reasons "${reasons[@]}")"
  elif [ "$has_medium" -eq 1 ]; then
    medium_count=$((medium_count + 1))
    print_result "medium" "$rel_dir" "$(join_reasons "${reasons[@]}")"
  else
    low_count=$((low_count + 1))
    print_result "low" "$rel_dir" "prompt, command, or template files only"
  fi
done

echo
echo "Summary: high=$high_count medium=$medium_count low=$low_count total=${#agent_dirs[@]}"

if [ "$fail_on_high" -eq 1 ] && [ "$high_count" -gt 0 ]; then
  exit 2
fi
