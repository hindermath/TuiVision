#!/usr/bin/env bash
# check-homogeneity.sh — Workspace Homogeneity Guardian Compliance Scanner v1.0
# FR-001 through FR-006, FR-016-FR-019; Contracts: check-homogeneity-cli.md
#
# Usage: check-homogeneity.sh [OPTIONS] [TARGET_DIR]
# Options:
#   --verbose       Show all checked files (including PASS)
#   --json          Machine-readable JSON output (takes precedence over --verbose)
#   --dry-run       No writes (no STATS.md, no memory-patch.md)
#   --apply-patch   <path>  Apply memory-patch.md and commit
#   --no-patch      Do not generate memory-patch.md
#   --fail-fast     Abort on first FAIL
#   --yes           Non-interactive confirmation (for --apply-patch)
# Exit codes: 0=all pass, 1=fail/warn, 2=fatal error

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
LIB_DIR="${SCRIPT_DIR}/lib"

# Source all hg-*.sh libs
for _lib in "${LIB_DIR}"/hg-*.sh; do
  [ -f "$_lib" ] && . "$_lib"
done

# ─── Argument Parsing ────────────────────────────────────────────────────────

OPT_VERBOSE=false
OPT_JSON=false
OPT_DRY_RUN=false
OPT_APPLY_PATCH=""
OPT_NO_PATCH=false
OPT_FAIL_FAST=false
OPT_YES=false
TARGET_DIR="${HOME}"

while [ $# -gt 0 ]; do
  case "$1" in
    --verbose)    OPT_VERBOSE=true ;;
    --json)       OPT_JSON=true ;;
    --dry-run)    OPT_DRY_RUN=true ;;
    --apply-patch)
      if [ -n "${2:-}" ]; then OPT_APPLY_PATCH="$2"; shift; fi ;;
    --no-patch)   OPT_NO_PATCH=true ;;
    --fail-fast)  OPT_FAIL_FAST=true ;;
    --yes)        OPT_YES=true ;;
    --*) echo "ERROR: unknown option $1" >&2; exit 2 ;;
    *) TARGET_DIR="$1" ;;
  esac
  shift
done

# --json takes precedence over --verbose
$OPT_JSON && OPT_VERBOSE=false

# Expand tilde
TARGET_DIR="${TARGET_DIR/#\~/$HOME}"
TARGET_DIR="${TARGET_DIR%/}"

# ─── Prerequisites Check ─────────────────────────────────────────────────────

if ! command -v rg >/dev/null 2>&1; then
  echo "FATAL: ripgrep (rg) not found — install with: brew install ripgrep / apt install ripgrep" >&2
  exit 2
fi

# ─── Apply-Patch Mode ────────────────────────────────────────────────────────

if [ -n "$OPT_APPLY_PATCH" ]; then
  patch_file="${OPT_APPLY_PATCH/#\~/$HOME}"
  if ! [ -f "$patch_file" ]; then
    echo "FATAL: patch file not found: ${patch_file}" >&2
    exit 2
  fi

  echo "Patch-Datei: ${patch_file}"
  echo ""
  echo "Vorgeschlagene Aenderungen:"
  grep '^##\|^- \|^\*\*' "$patch_file" | head -30
  echo ""

  if $OPT_YES; then
    answer="j"
  else
    printf "Patch anwenden? [j/N] "
    read -r answer
  fi

  if echo "$answer" | grep -qiE '^[jy]'; then
    patch_count=0
    target_file=""
    while IFS= read -r pline; do
      case "$pline" in
        "### Target: "*)
          target_file="${pline#### Target: }"
          target_file="${target_file/#\~/$HOME}" ;;
        "+++ "*)
          content_to_append="${pline#+++ }"
          if [ -n "$target_file" ]; then
            printf '%s\n' "$content_to_append" >> "$target_file"
            patch_count=$((patch_count + 1))
          fi ;;
      esac
    done < "$patch_file"
    git -C "$TARGET_DIR" add -A 2>/dev/null || true
    git -C "$TARGET_DIR" commit -m "chore: apply memory-patch -- ${patch_count} entries updated" 2>/dev/null || true
    echo "Patch angewendet: ${patch_count} Eintraege, git-Commit erstellt."
  else
    echo "Patch abgebrochen."
  fi
  exit 0
fi

# ─── Scan State (Bash 3.x-compatible, no associative arrays) ─────────────────

# Parallel arrays: index-matched
SCAN_DIRS=()        # dir paths
SCAN_TOTALS=()      # total checks per dir
SCAN_PASSES=()      # passed checks per dir

FAILURES=()
WARNINGS=()
TOTAL_CHECKS=0
TOTAL_PASS=0
CURRENT_DIR_IDX=-1
CURRENT_LEVEL=0

# Per-level counters for by_level JSON
L0_TOTAL=0 L0_PASS=0
L1_TOTAL=0 L1_PASS=0
L2_TOTAL=0 L2_PASS=0

# ─── Directory index lookup ───────────────────────────────────────────────────

get_dir_idx() {
  local search="$1"
  local i=0
  for d in "${SCAN_DIRS[@]+"${SCAN_DIRS[@]}"}"; do
    [ "$d" = "$search" ] && echo "$i" && return
    i=$((i + 1))
  done
  echo "-1"
}

# ─── Check Helpers ───────────────────────────────────────────────────────────

emit_result() {
  local status="$1" filepath="$2" message="$3" dir="$4"
  local idx
  idx=$(get_dir_idx "$dir")
  TOTAL_CHECKS=$((TOTAL_CHECKS + 1))
  SCAN_TOTALS[$idx]=$(( ${SCAN_TOTALS[$idx]:-0} + 1 ))

  # Per-level tracking
  case "$CURRENT_LEVEL" in
    0) L0_TOTAL=$((L0_TOTAL + 1)); [ "$status" = "PASS" ] && L0_PASS=$((L0_PASS + 1)) ;;
    1) L1_TOTAL=$((L1_TOTAL + 1)); [ "$status" = "PASS" ] && L1_PASS=$((L1_PASS + 1)) ;;
    2) L2_TOTAL=$((L2_TOTAL + 1)); [ "$status" = "PASS" ] && L2_PASS=$((L2_PASS + 1)) ;;
  esac

  if [ "$status" = "PASS" ]; then
    TOTAL_PASS=$((TOTAL_PASS + 1))
    SCAN_PASSES[$idx]=$(( ${SCAN_PASSES[$idx]:-0} + 1 ))
    if ! $OPT_JSON && $OPT_VERBOSE; then
      printf "  %-4s %-40s %s\n" "✓" "$filepath" "$message"
    fi
  elif [ "$status" = "WARN" ]; then
    WARNINGS+=("${CURRENT_LEVEL}|${dir##*/}|${filepath}|${message}")
    if ! $OPT_JSON; then
      printf "  %-4s %-40s %s\n" "WARN" "$filepath" "WARN: ${message}"
    fi
  elif [ "$status" = "FAIL" ]; then
    FAILURES+=("${CURRENT_LEVEL}|${dir##*/}|${filepath}|${message}")
    if ! $OPT_JSON; then
      printf "  %-4s %-40s %s\n" "✗" "$filepath" "FAIL: ${message}"
    fi
    if $OPT_FAIL_FAST; then
      echo "" >&2
      echo "FAIL-FAST: aborted at first FAIL" >&2
      exit 1
    fi
  fi
}

check_file_presence() {
  local dir="$1" file="$2"
  local full="${dir}/${file}"
  if [ -f "$full" ]; then
    emit_result "PASS" "$file" "file present" "$dir"
  else
    emit_result "FAIL" "$file" "file missing" "$dir"
  fi
}

check_markdown_file() {
  local dir="$1" file="$2"
  local full="${dir}/${file}"
  [ -f "$full" ] || return 0

  # Bilingual check
  local bil_result
  bil_result=$(hg_check_bilingual "$full" 2>/dev/null || true)
  if [ -n "$bil_result" ]; then
    local b_status b_msg
    b_status="${bil_result%%|*}"
    b_msg="${bil_result##*|}"
    emit_result "$b_status" "$file" "$b_msg" "$dir"
  fi

  # A11Y checks
  while IFS= read -r a11y_line; do
    [ -z "$a11y_line" ] && continue
    local a_status a_msg
    a_status="${a11y_line%%|*}"
    a_msg="${a11y_line##*|}"
    emit_result "$a_status" "$file" "$a_msg" "$dir"
  done < <(hg_check_a11y "$full" 2>/dev/null || true)

  # Secrets check
  while IFS= read -r sec_line; do
    [ -z "$sec_line" ] && continue
    local s_status s_rest s_msg
    s_status="${sec_line%%|*}"
    s_rest="${sec_line#*|}"
    s_msg="${s_rest##*|}"
    emit_result "$s_status" "$file" "$s_msg" "$dir"
  done < <(hg_scan_file_secrets "$full" 2>/dev/null || true)
}

# ─── REV-B01 New Check Helpers ────────────────────────────────────────────────

check_en_placeholder() {
  local dir="$1" file="$2"
  local full="${dir}/${file}"
  [ -f "$full" ] || return 0
  if rg -q '<!-- EN:' "$full" 2>/dev/null; then
    emit_result "PASS" "$file" "EN placeholder" "$dir"
  else
    emit_result "FAIL" "$file" "EN placeholder missing" "$dir"
  fi
}

check_readme_sections() {
  local dir="$1"
  local full="${dir}/README.md"
  [ -f "$full" ] || return 0

  # A11Y section
  if rg -qi '^## .*Barrierefreiheit' "$full" 2>/dev/null; then
    emit_result "PASS" "README.md" "A11Y section" "$dir"
  else
    emit_result "FAIL" "README.md" "A11Y section missing" "$dir"
  fi

  # Spec-kit section
  if rg -qi '^## .*Spec-kit' "$full" 2>/dev/null; then
    emit_result "PASS" "README.md" "Spec-kit section" "$dir"
  else
    emit_result "FAIL" "README.md" "Spec-kit section missing" "$dir"
  fi

  # Azubis section
  if rg -qi '^## .*Azubis' "$full" 2>/dev/null; then
    emit_result "PASS" "README.md" "Azubis section" "$dir"
  else
    emit_result "FAIL" "README.md" "Azubis section missing" "$dir"
  fi
}

check_ansi_in_scripts() {
  local dir="$1"
  local scripts_dir="${dir}/scripts"
  [ -d "$scripts_dir" ] || return 0

  # Three-pattern exhaustive ANSI scan (NFR-REV-07, M4-orig: use rg not grep -rP)
  local ansi_files
  ansi_files=$(rg -l -e $'\x1b\[' -e $'\\033\[' -e $'\\e\[' "$scripts_dir" 2>/dev/null || true)
  if [ -z "$ansi_files" ]; then
    emit_result "PASS" "scripts/" "no ANSI codes in scripts/" "$dir"
  else
    local first_file
    first_file=$(printf '%s' "$ansi_files" | head -1 | sed "s|${scripts_dir}/||")
    emit_result "FAIL" "scripts/" "ANSI escape codes found: ${first_file}" "$dir"
  fi
}

check_editorconfig_csharp() {
  local dir="$1"
  # Only applies if C# solution files are present at project root
  if find "$dir" -maxdepth 1 -name "*.sln" 2>/dev/null | grep -q .; then
    if [ -f "${dir}/.editorconfig" ]; then
      emit_result "PASS" ".editorconfig" ".editorconfig present (C# project)" "$dir"
    else
      emit_result "FAIL" ".editorconfig" ".editorconfig missing (C# project)" "$dir"
    fi
  fi
}

check_workflow_yml() {
  local dir="$1"
  local yml="${dir}/.github/workflows/homogeneity-check.yml"
  if [ -f "$yml" ]; then
    emit_result "PASS" ".github/workflows/homogeneity-check.yml" "file present" "$dir"
  else
    emit_result "FAIL" ".github/workflows/homogeneity-check.yml" "file missing" "$dir"
  fi
}

check_copilot_instructions() {
  local dir="$1"
  local f="${dir}/.github/copilot-instructions.md"
  if [ -f "$f" ]; then
    emit_result "PASS" ".github/copilot-instructions.md" "file present" "$dir"
  else
    emit_result "FAIL" ".github/copilot-instructions.md" "file missing" "$dir"
  fi
}

# ─── Header ──────────────────────────────────────────────────────────────────

if ! $OPT_JSON; then
  echo "Workspace Homogeneity Guardian — check-homogeneity v1.0"
  echo "Scan-Startpunkt: ${TARGET_DIR}"
  printf '%.0s=' {1..54}; echo
  echo ""
fi

# ─── Main Scan ───────────────────────────────────────────────────────────────

REQUIRED_FILES="AGENTS.md CLAUDE.md GEMINI.md README.md STATS.md constitution.md"

while IFS='|' read -r level dir _type; do
  CURRENT_LEVEL="$level"
  SCAN_DIRS+=("$dir")
  SCAN_TOTALS+=(0)
  SCAN_PASSES+=(0)
  local_idx=$(( ${#SCAN_DIRS[@]} - 1 ))

  if ! $OPT_JSON; then
    echo "[Level ${level}] ${dir}/"
  fi

  # Required files
  for req_file in $REQUIRED_FILES; do
    check_file_presence "$dir" "$req_file"
    check_markdown_file "$dir" "$req_file"
  done

  # README.md content checks (A11Y, Spec-kit, Azubis)
  check_readme_sections "$dir"

  # copilot-instructions.md presence (Level 0 and 1 only)
  if [ "$level" -le 1 ]; then
    check_copilot_instructions "$dir"
  fi

  # EN placeholder checks (Level 0 and 1)
  if [ "$level" -le 1 ]; then
    for en_file in README.md AGENTS.md CLAUDE.md GEMINI.md constitution.md; do
      check_en_placeholder "$dir" "$en_file"
    done
    check_en_placeholder "$dir" ".github/copilot-instructions.md"
  fi

  # homogeneity-check.yml presence (all levels)
  check_workflow_yml "$dir"

  # ANSI escape scan in scripts/ (Level 0 only, global)
  if [ "$level" -eq 0 ]; then
    check_ansi_in_scripts "$dir"
  fi

  # Hook check (Level 1+2 have .git/)
  if [ "$level" -ge 1 ]; then
    hook_result=$(hg_check_hook "$dir" 2>/dev/null || true)
    if [ -n "$hook_result" ]; then
      h_status="${hook_result%%|*}"
      h_msg="${hook_result##*|}"
      emit_result "$h_status" ".git/hooks/pre-push" "$h_msg" "$dir"
    fi
  fi

  # Level 0: canonical hook presence
  if [ "$level" -eq 0 ]; then
    if [ -f "${HOME}/scripts/hooks/pre-push" ]; then
      emit_result "PASS" "scripts/hooks/pre-push" "canonical hook present" "$dir"
    else
      emit_result "WARN" "scripts/hooks/pre-push" "canonical hook missing" "$dir"
    fi
  fi

  # .editorconfig for C# Level-2 (FR-REV-E02)
  if [ "$level" -eq 2 ]; then
    check_editorconfig_csharp "$dir"
  fi

  # Deps + speckit for projects (Level 2)
  if [ "$level" -eq 2 ]; then
    while IFS= read -r dep_line; do
      [ -z "$dep_line" ] && continue
      d_status="${dep_line%%|*}"
      d_rest="${dep_line#*|}"
      d_msg="${d_rest##*|}"
      emit_result "$d_status" "*.csproj" "$d_msg" "$dir"
    done < <(hg_check_deps "$dir" 2>/dev/null || true)

    spec_file=$(find "${dir}/specs" -name "spec.md" -maxdepth 3 2>/dev/null | head -1 || true)
    if [ -n "$spec_file" ]; then
      sk_line=$(hg_check_speckit "$spec_file" 2>/dev/null || true)
      if [ -n "$sk_line" ]; then
        sk_status="${sk_line%%|*}"
        sk_msg="${sk_line##*|}"
        emit_result "$sk_status" "specs/spec.md" "$sk_msg" "$dir"
      fi
    fi
  fi

  if ! $OPT_JSON; then echo ""; fi

done < <(hg_scan "$TARGET_DIR")

# ─── Summary ─────────────────────────────────────────────────────────────────

OVERALL_SCORE=0
[ "$TOTAL_CHECKS" -gt 0 ] && OVERALL_SCORE=$(( (TOTAL_PASS * 100) / TOTAL_CHECKS ))

WORKSPACES_COUNT=0
PROJECTS_COUNT=0
while IFS='|' read -r level _dir _type; do
  [ "$level" -eq 1 ] && WORKSPACES_COUNT=$((WORKSPACES_COUNT + 1))
  [ "$level" -eq 2 ] && PROJECTS_COUNT=$((PROJECTS_COUNT + 1))
done < <(hg_scan "$TARGET_DIR")

if $OPT_JSON; then
  # JSON output — contract format: {"score":N,"by_level":{"0":N,"1":N,"2":N},"failures":[...],"warnings":[...]}
  L0_SCORE=0; [ "$L0_TOTAL" -gt 0 ] && L0_SCORE=$(( (L0_PASS * 100) / L0_TOTAL ))
  L1_SCORE=0; [ "$L1_TOTAL" -gt 0 ] && L1_SCORE=$(( (L1_PASS * 100) / L1_TOTAL ))
  L2_SCORE=0; [ "$L2_TOTAL" -gt 0 ] && L2_SCORE=$(( (L2_PASS * 100) / L2_TOTAL ))

  fail_json="["
  first_f=true
  for f in "${FAILURES[@]+"${FAILURES[@]}"}"; do
    $first_f || fail_json+=","
    flevel="${f%%|*}"
    frest="${f#*|}"; fws="${frest%%|*}"
    frest2="${frest#*|}"; ffile="${frest2%%|*}"; fcheck="${frest2#*|}"
    fail_json+="{\"file\":\"${ffile}\",\"check\":\"${fcheck}\",\"level\":${flevel},\"workspace\":\"${fws}\"}"
    first_f=false
  done
  fail_json+="]"

  warn_json="["
  first_w=true
  for w in "${WARNINGS[@]+"${WARNINGS[@]}"}"; do
    $first_w || warn_json+=","
    wlevel="${w%%|*}"
    wrest="${w#*|}"; wws="${wrest%%|*}"
    wrest2="${wrest#*|}"; wfile="${wrest2%%|*}"; wcheck="${wrest2#*|}"
    warn_json+="{\"file\":\"${wfile}\",\"check\":\"${wcheck}\",\"level\":${wlevel},\"workspace\":\"${wws}\"}"
    first_w=false
  done
  warn_json+="]"

  printf '{"score":%d,"by_level":{"0":%d,"1":%d,"2":%d},"failures":%s,"warnings":%s}\n' \
    "$OVERALL_SCORE" "$L0_SCORE" "$L1_SCORE" "$L2_SCORE" \
    "$fail_json" "$warn_json"
else
  printf '%.0s=' {1..54}; echo
  echo "COMPLIANCE SUMMARY"
  echo ""

  i=0
  for d in "${SCAN_DIRS[@]+"${SCAN_DIRS[@]}"}"; do
    lt=${SCAN_TOTALS[$i]:-0}
    lp=${SCAN_PASSES[$i]:-0}
    ls_score=0
    [ "$lt" -gt 0 ] && ls_score=$(( (lp * 100) / lt ))
    bar_filled=$(( ls_score * 10 / 100 ))
    bar_empty=$(( 10 - bar_filled ))
    bar=""
    j=0
    while [ $j -lt $bar_filled ]; do bar="${bar}█"; j=$((j+1)); done
    j=0
    while [ $j -lt $bar_empty ]; do bar="${bar}░"; j=$((j+1)); done
    short_name="${d/#$HOME/~}"
    printf "%-30s [%s] %3d %%  (%d/%d checks)\n" \
      "$short_name" "$bar" "$ls_score" "$lp" "$lt"
    i=$((i + 1))
  done

  echo ""
  printf "Overall: %d %%  |  Workspaces: %d  |  Projects: %d\n" \
    "$OVERALL_SCORE" "$WORKSPACES_COUNT" "$PROJECTS_COUNT"

  FAIL_COUNT=${#FAILURES[@]+"${#FAILURES[@]}"}
  WARN_COUNT=${#WARNINGS[@]+"${#WARNINGS[@]}"}
  # Bash 3 safe empty array check
  FAIL_COUNT=0; for _ in "${FAILURES[@]+"${FAILURES[@]}"}"; do FAIL_COUNT=$((FAIL_COUNT+1)); done
  WARN_COUNT=0; for _ in "${WARNINGS[@]+"${WARNINGS[@]}"}"; do WARN_COUNT=$((WARN_COUNT+1)); done

  if ! $OPT_DRY_RUN; then
    echo "STATS.md updated: ${HOME}/STATS.md"
  fi

  echo ""
  if [ "$FAIL_COUNT" -gt 0 ] || [ "$WARN_COUNT" -gt 0 ]; then
    printf "Exit code: 1 (%d FAIL, %d WARN)\n" "$FAIL_COUNT" "$WARN_COUNT"
  else
    printf "Exit code: 0 (all checks passed)\n"
  fi
fi

# ─── GitHub Step Summary ────────────────────────────────────────────────────

if [ -n "${GITHUB_STEP_SUMMARY:-}" ]; then
  {
    echo "## Homogeneity Check Report"
    echo ""
    echo "| Level | Datei | Check | Status |"
    echo "|---|---|---|---|"
    for f in "${FAILURES[@]+"${FAILURES[@]}"}"; do
      flevel="${f%%|*}"; frest="${f#*|}"; ffile="${frest#*|}"; ffile="${ffile%%|*}"; fcheck="${ffile#*|}"; ffile="${ffile%%|*}"
      # Re-parse properly
      arr_level="${f%%|*}"
      arr_rest="${f#*|}"
      arr_ws="${arr_rest%%|*}"
      arr_rest2="${arr_rest#*|}"
      arr_file="${arr_rest2%%|*}"
      arr_check="${arr_rest2#*|}"
      echo "| ${arr_level} | ${arr_file} | ${arr_check} | ✗ |"
    done
    for w in "${WARNINGS[@]+"${WARNINGS[@]}"}"; do
      arr_level="${w%%|*}"
      arr_rest="${w#*|}"
      arr_ws="${arr_rest%%|*}"
      arr_rest2="${arr_rest#*|}"
      arr_file="${arr_rest2%%|*}"
      arr_check="${arr_rest2#*|}"
      echo "| ${arr_level} | ${arr_file} | ${arr_check} | WARN |"
    done
    echo ""
    echo "**Score: ${OVERALL_SCORE}% (${TOTAL_PASS}/${TOTAL_CHECKS} checks passed)**"
  } >> "$GITHUB_STEP_SUMMARY"
fi

# ─── Post-scan writes ─────────────────────────────────────────────────────────

if ! $OPT_DRY_RUN; then
  if [ -f "${LIB_DIR}/hg-stats.sh" ]; then
    . "${LIB_DIR}/hg-stats.sh"
    hg_write_stats "${HOME}/STATS.md" "$OVERALL_SCORE" "${SCAN_DIRS[@]+"${SCAN_DIRS[@]}"}" 2>/dev/null || true
  fi

  if ! $OPT_NO_PATCH; then
    if [ -f "${LIB_DIR}/hg-patch.sh" ]; then
      . "${LIB_DIR}/hg-patch.sh"
      hg_generate_patch "${HOME}/STATS.md" "${FAILURES[@]+"${FAILURES[@]}"}" 2>/dev/null || true
    fi
  fi
fi

# ─── Exit Code ───────────────────────────────────────────────────────────────
# Exit 0: all PASS or WARN — no ✗ (per contract)
# Exit 1: at least one ✗ (FAIL)

_fail_cnt=0; for _ in "${FAILURES[@]+"${FAILURES[@]}"}"; do _fail_cnt=$((_fail_cnt+1)); done

if [ "$_fail_cnt" -gt 0 ]; then
  exit 1
fi
exit 0
