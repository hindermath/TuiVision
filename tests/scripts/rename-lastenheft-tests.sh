#!/usr/bin/env bash
# Vertragstests in Wegwerf-Repositories / Contract tests in disposable repositories

set -euo pipefail

repo_root=$(cd "$(dirname "${BASH_SOURCE[0]}")/../.." && pwd)
bash_script="$repo_root/scripts/rename-lastenheft.sh"
pwsh_script="$repo_root/scripts/rename-lastenheft.ps1"
temp_root=$(mktemp -d)
trap 'rm -rf "$temp_root"' EXIT

passed=0

fail()
{
  printf 'FAIL: %s\n' "$*" >&2
  exit 1
}

new_repo()
{
  local name=$1
  local path="$temp_root/$name"
  mkdir -p "$path"
  git -C "$path" init -q
  git -C "$path" config user.name "TuiVision Script Test"
  git -C "$path" config user.email "script-test@example.invalid"
  printf '# Test\n' > "$path/Lastenheft_Test.md"
  printf 'baseline\n' > "$path/unrelated.txt"
  git -C "$path" add Lastenheft_Test.md unrelated.txt
  git -C "$path" commit -q -m baseline
  printf '%s\n' "$path"
}

run_tool()
{
  local impl=$1
  local mode=$2
  local file=${3:-}
  local branch=${4:-}

  if [ "$impl" = bash ]; then
    case "$mode" in
      help) bash "$bash_script" --help ;;
      missing) bash "$bash_script" ;;
      dry-run) bash "$bash_script" --dry-run "$file" "$branch" ;;
      no-commit) bash "$bash_script" --no-commit "$file" "$branch" ;;
      commit) bash "$bash_script" "$file" "$branch" ;;
    esac
  else
    case "$mode" in
      help) pwsh -NoLogo -NoProfile -Command "Get-Help '$pwsh_script' -Full" ;;
      missing) pwsh -NoLogo -NoProfile -File "$pwsh_script" ;;
      dry-run) pwsh -NoLogo -NoProfile -File "$pwsh_script" -File "$file" -BranchName "$branch" -WhatIf ;;
      no-commit) pwsh -NoLogo -NoProfile -File "$pwsh_script" -File "$file" -BranchName "$branch" -NoCommit ;;
      commit) pwsh -NoLogo -NoProfile -File "$pwsh_script" -File "$file" -BranchName "$branch" ;;
    esac
  fi
}

assert_help_and_missing()
{
  local impl=$1
  run_tool "$impl" help | rg -qi 'usage|verwendung|syntax|rename-lastenheft' || fail "$impl help lacks usage"
  if run_tool "$impl" missing >/dev/null 2>&1; then
    fail "$impl accepted missing arguments"
  fi
  passed=$((passed + 2))
}

assert_invalid_input()
{
  local impl=$1
  local repo
  repo=$(new_repo "$impl-invalid")
  printf '# untracked\n' > "$repo/Lastenheft_Untracked.md"

  if (cd "$repo" && run_tool "$impl" no-commit Lastenheft_Untracked.md 016-test) >/dev/null 2>&1; then
    fail "$impl accepted an untracked source"
  fi
  test -f "$repo/Lastenheft_Untracked.md" || fail "$impl mutated untracked source"

  if (cd "$repo" && run_tool "$impl" no-commit Lastenheft_Test.md '../unsafe') >/dev/null 2>&1; then
    fail "$impl accepted an unsafe branch segment"
  fi
  test -f "$repo/Lastenheft_Test.md" || fail "$impl mutated unsafe input"
  test "$(git -C "$repo" rev-list --count HEAD)" = 1 || fail "$impl committed invalid input"
  passed=$((passed + 2))
}

assert_dry_run()
{
  local impl=$1
  local repo
  repo=$(new_repo "$impl-dry")

  (cd "$repo" && run_tool "$impl" dry-run Lastenheft_Test.md 016-test) >/dev/null
  test -f "$repo/Lastenheft_Test.md" || fail "$impl dry-run moved source"
  test ! -e "$repo/Lastenheft_Test.016-test.md" || fail "$impl dry-run created target"
  test -z "$(git -C "$repo" status --porcelain)" || fail "$impl dry-run changed index/worktree"
  passed=$((passed + 1))
}

assert_no_commit_and_idempotence()
{
  local impl=$1
  local repo
  repo=$(new_repo "$impl-no-commit")

  (cd "$repo" && run_tool "$impl" no-commit Lastenheft_Test.md 016-test) >/dev/null
  test ! -e "$repo/Lastenheft_Test.md" || fail "$impl no-commit kept source"
  test -f "$repo/Lastenheft_Test.016-test.md" || fail "$impl no-commit missed target"
  test "$(git -C "$repo" rev-list --count HEAD)" = 1 || fail "$impl no-commit created commit"
  git -C "$repo" diff --cached --name-status | rg -q 'Lastenheft_Test' || fail "$impl no-commit did not stage rename"

  (cd "$repo" && run_tool "$impl" no-commit Lastenheft_Test.016-test.md 016-test) >/dev/null
  test "$(git -C "$repo" rev-list --count HEAD)" = 1 || fail "$impl idempotence created commit"
  passed=$((passed + 2))
}

assert_commit_isolation()
{
  local impl=$1
  local repo
  repo=$(new_repo "$impl-commit")
  printf 'changed but unrelated\n' > "$repo/unrelated.txt"
  git -C "$repo" add unrelated.txt

  (cd "$repo" && run_tool "$impl" commit Lastenheft_Test.md 016-test) >/dev/null
  test "$(git -C "$repo" rev-list --count HEAD)" = 2 || fail "$impl explicit mode missed commit"
  test -f "$repo/Lastenheft_Test.016-test.md" || fail "$impl explicit mode missed target"
  git -C "$repo" show --name-only --pretty=format: HEAD | rg -q 'Lastenheft_Test' || fail "$impl commit missed rename"
  if git -C "$repo" show --name-only --pretty=format: HEAD | rg -q '^unrelated.txt$'; then
    fail "$impl committed unrelated staged content"
  fi
  git -C "$repo" diff --cached --name-only | rg -q '^unrelated.txt$' || fail "$impl consumed unrelated staging"
  passed=$((passed + 1))
}

assert_branch_normalization()
{
  local impl=$1
  local repo
  repo=$(new_repo "$impl-normalize")

  (cd "$repo" && run_tool "$impl" no-commit Lastenheft_Test.md codex/demo) >/dev/null
  test -f "$repo/Lastenheft_Test.codex-demo.md" || fail "$impl did not normalize branch slash"
  passed=$((passed + 1))
}

command -v pwsh >/dev/null || fail 'pwsh is required for parity tests'

for impl in bash pwsh; do
  assert_help_and_missing "$impl"
  assert_invalid_input "$impl"
  assert_dry_run "$impl"
  assert_no_commit_and_idempotence "$impl"
  assert_commit_isolation "$impl"
  assert_branch_normalization "$impl"
done

printf 'PASS: %d rename-lastenheft contract assertions\n' "$passed"
