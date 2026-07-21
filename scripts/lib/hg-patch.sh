#!/usr/bin/env bash
# hg-patch.sh — memory-patch.md Generator (FR-020/021)
# Prüft 3 Trigger-Bedingungen:
# 1. Neues Repo erkannt
# 2. Score-Delta >= 10%
# 3. Neuer WARN/FAIL-Typ

# Get previous score from STATS.md
_hg_prev_score() {
  local stats_file="$1"
  [ -f "$stats_file" ] || echo 0
  grep 'Overall Score:' "$stats_file" 2>/dev/null | tail -2 | head -1 | grep -o '[0-9]*' | head -1 || echo 0
}

# Get unique check types from previous run
_hg_prev_check_types() {
  local stats_file="$1"
  [ -f "$stats_file" ] || return 0
  # Extract check types from last run section
  local last_run_line
  last_run_line=$(grep -n '^## Run ' "$stats_file" 2>/dev/null | tail -1 | cut -d: -f1)
  [ -z "$last_run_line" ] && return 0
  tail -n "+${last_run_line}" "$stats_file" | grep -oE 'WARN:[^|]+|FAIL:[^|]+' | sort -u
}

# Generate memory-patch.md if triggered
# Usage: hg_generate_patch <stats_file> [failure1 failure2 ...] [warning1 warning2 ...]
hg_generate_patch() {
  local stats_file="$1"
  shift
  local current_issues=("$@")

  # Current score (from last STATS entry)
  local current_score prev_score score_delta triggered=false trigger_reason=""
  current_score=$(grep 'Overall Score:' "$stats_file" 2>/dev/null | tail -1 | grep -o '[0-9]*' | head -1 || echo 0)
  prev_score=$(_hg_prev_score "$stats_file")

  # Trigger 1: New repo (no previous score)
  if [ "$prev_score" = "0" ] && [ "$current_score" -gt 0 ]; then
    triggered=true
    trigger_reason="new-repo-detected"
  fi

  # Trigger 2: Score delta >= 10%
  if [ -n "$prev_score" ] && [ -n "$current_score" ]; then
    if [ "$prev_score" -gt 0 ]; then
      score_delta=$(( current_score - prev_score ))
      # Absolute value
      [ "$score_delta" -lt 0 ] && score_delta=$(( -score_delta ))
      if [ "$score_delta" -ge 10 ]; then
        triggered=true
        trigger_reason="${trigger_reason:+${trigger_reason},}score-delta-${score_delta}"
      fi
    fi
  fi

  # Trigger 3: New WARN/FAIL type
  local current_types=""
  for issue in "${current_issues[@]+"${current_issues[@]}"}"; do
    msg="${issue#*:}"
    current_types="${current_types}${msg}\n"
  done

  # Only generate if triggered
  $triggered || return 0

  # Determine patch file location (SPECS_DIR or ~/specs/)
  local specs_dir="${HOME}/specs"
  local patch_file
  # Try to find the active feature specs dir
  local active_feature
  active_feature=$(ls -d "${specs_dir}"/001-* 2>/dev/null | head -1 || echo "$specs_dir")
  patch_file="${active_feature}/memory-patch.md"
  [ -z "$active_feature" ] && patch_file="${specs_dir}/memory-patch.md"

  local entry_count=0
  {
    printf '# memory-patch.md\n\n'
    printf '**Generated**: %s\n' "$(date '+%Y-%m-%d %H:%M')"
    printf '**Trigger**: %s\n' "$trigger_reason"
    printf '**Score**: %d %% (prev: %d %%)\n\n' "$current_score" "$prev_score"
    printf '%s\n\n' '---'

    # Generate patch entries for each failure
    for issue in "${current_issues[@]+"${current_issues[@]}"}"; do
      local filepath msg routing
      filepath="${issue%%:*}"
      msg="${issue#*:}"

      # Determine routing
      case "$filepath" in
        *CLAUDE.md*|*AGENTS.md*|*GEMINI.md*|*copilot*)
          routing="agent_file" ;;
        *README.md*)
          routing="readme" ;;
        *CLAUDE.md*|*.specify/*|*constitution*)
          routing="constitution" ;;
        *)
          routing="agent_file" ;;
      esac

      printf '## MemoryPatchEntry %d\n\n' "$((entry_count + 1))"
      printf '%s\n' "- **File**: \`${filepath}\`"
      printf '%s\n' "- **Issue**: ${msg}"
      printf '%s\n' "- **Routing**: ${routing}"
      printf '%s\n\n' "- **Action**: Review and fix the reported issue"
      printf '### Target: %s\n\n' "$filepath"
      printf '+++ [Review needed: %s]\n\n' "$msg"
      printf '%s\n\n' '---'
      entry_count=$((entry_count + 1))
    done

    printf '**Total entries**: %d\n' "$entry_count"
  } > "$patch_file"

  [ "$entry_count" -gt 0 ] && echo "memory-patch.md: ${patch_file} (${entry_count} entries)" >&2
}
