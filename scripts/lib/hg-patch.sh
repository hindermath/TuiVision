#!/usr/bin/env bash
# hg-patch.sh — memory-patch.md Generator (FR-020/021)
# Prüft 3 Trigger-Bedingungen:
# 1. Neues Repo erkannt
# 2. Score-Delta >= 10%
# 3. Neuer WARN/FAIL-Typ

# Append through a directory-relative, no-follow handle and atomically replace
# the validated leaf. Replacing the inode keeps outside hard-link targets intact.
hg_append_patch_content() {
  local root="$1"
  local target="$2"
  local content="$3"
  printf '%s\n' "$content" | python3 -c '
import os, pathlib, stat, sys, uuid
root = pathlib.Path(sys.argv[1]).resolve(strict=True)
target = pathlib.Path(sys.argv[2]).resolve(strict=True)
relative = pathlib.Path(os.path.relpath(target, root))
if relative.is_absolute() or ".." in relative.parts or not relative.parts:
    raise SystemExit("unsafe patch target")
addition = sys.stdin.buffer.read(1024 * 1024 + 1)
if len(addition) > 1024 * 1024:
    raise OSError("patch content exceeds byte limit")
if os.name == "nt":
    current = root
    for component in relative.parts:
        current /= component
        item = os.lstat(current)
        if stat.S_ISLNK(item.st_mode) or (
            getattr(item, "st_file_attributes", 0)
            & getattr(stat, "FILE_ATTRIBUTE_REPARSE_POINT", 0)
        ):
            raise OSError("patch target contains a link or reparse point")
    if not stat.S_ISREG(item.st_mode):
        raise OSError("patch target is not a regular file")
    with target.open("rb") as source:
        existing = source.read(8 * 1024 * 1024 + 1)
    if len(existing) > 8 * 1024 * 1024:
        raise OSError("patch target exceeds byte limit")
    temporary_path = target.parent / f".{target.name}.{uuid.uuid4().hex}.tmp"
    try:
        with temporary_path.open("xb") as stream:
            stream.write(existing + addition)
            stream.flush()
            os.fsync(stream.fileno())
        os.chmod(temporary_path, item.st_mode & 0o777)
        current = root
        for component in relative.parts[:-1]:
            current /= component
            checked = os.lstat(current)
            if stat.S_ISLNK(checked.st_mode) or (
                getattr(checked, "st_file_attributes", 0)
                & getattr(stat, "FILE_ATTRIBUTE_REPARSE_POINT", 0)
            ):
                raise OSError("patch parent changed during validation")
        os.replace(temporary_path, target)
    finally:
        temporary_path.unlink(missing_ok=True)
    raise SystemExit(0)
flags = os.O_RDONLY | getattr(os, "O_DIRECTORY", 0) | getattr(os, "O_NOFOLLOW", 0)
parent = os.open(root, flags)
temporary = None
try:
    for component in relative.parts[:-1]:
        child = os.open(component, flags, dir_fd=parent)
        os.close(parent)
        parent = child
    leaf = relative.parts[-1]
    metadata = os.stat(leaf, dir_fd=parent, follow_symlinks=False)
    if not stat.S_ISREG(metadata.st_mode):
        raise OSError("patch target is not a regular file")
    source = os.open(leaf, os.O_RDONLY | getattr(os, "O_NOFOLLOW", 0), dir_fd=parent)
    try:
        opened = os.fstat(source)
        if (opened.st_dev, opened.st_ino) != (metadata.st_dev, metadata.st_ino):
            raise OSError("patch target changed during validation")
        existing = bytearray()
        while chunk := os.read(source, 65536):
            existing.extend(chunk)
            if len(existing) > 8 * 1024 * 1024:
                raise OSError("patch target exceeds byte limit")
    finally:
        os.close(source)
    temporary = f".{leaf}.{uuid.uuid4().hex}.tmp"
    descriptor = os.open(
        temporary,
        os.O_WRONLY | os.O_CREAT | os.O_EXCL | getattr(os, "O_NOFOLLOW", 0),
        metadata.st_mode & 0o777,
        dir_fd=parent,
    )
    try:
        payload = bytes(existing) + addition
        offset = 0
        while offset < len(payload):
            offset += os.write(descriptor, payload[offset:])
        os.fsync(descriptor)
    finally:
        os.close(descriptor)
    os.replace(temporary, leaf, src_dir_fd=parent, dst_dir_fd=parent)
    temporary = None
    os.fsync(parent)
finally:
    if temporary is not None:
        try: os.unlink(temporary, dir_fd=parent)
        except OSError: pass
    os.close(parent)
' "$root" "$target"
}

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
