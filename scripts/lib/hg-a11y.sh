#!/usr/bin/env bash
# hg-a11y.sh — Accessibility-Prüfung Markdown (FR-005/006, R-02)
# Drei Checks: Heading-Hierarchie, leere Alt-Texte, nichtssagende Link-Texte

# Check heading hierarchy gaps — skips fenced and indented code blocks
check_heading_hierarchy() {
  local file="$1"
  local prev=0
  local in_fence=0
  while IFS= read -r line; do
    # Toggle fenced code block state
    if echo "$line" | grep -q '^```'; then
      [ "$in_fence" -eq 0 ] && in_fence=1 || in_fence=0
      continue
    fi
    [ "$in_fence" -eq 1 ] && continue
    # Skip indented code blocks (4+ spaces or tab)
    echo "$line" | grep -qE '^(    |\t)' && continue

    if echo "$line" | grep -q '^#'; then
      local raw_level
      raw_level=$(echo "$line" | grep -o '^#*' | wc -c)
      local level=$(( raw_level - 1 ))  # wc -c counts newline
      if [ "$level" -gt $(( prev + 1 )) ] && [ "$prev" -gt 0 ]; then
        echo "WARN|${file}|heading-gap-h${prev}-to-h${level}"
      fi
      prev=$level
    fi
  done < "$file"
}

hg_check_a11y() {
  local file="$1"

  if ! [ -f "$file" ]; then
    return 0
  fi

  case "$file" in
    *.md|*.MD) ;;
    *) return 0 ;;
  esac

  # 1. Heading hierarchy gaps
  check_heading_hierarchy "$file"

  # 2. Empty alt texts: ![]()
  local empty_alt
  empty_alt=$(rg -c '!\[\]\(' "$file" 2>/dev/null || echo 0)
  [ "$empty_alt" -gt 0 ] && echo "WARN|${file}|empty-alt-text"

  # 3. Non-descriptive link texts — strip inline code spans to avoid false positives
  local bad_links
  bad_links=$(sed 's/`[^`]*`//g' "$file" | rg -ic '\[(hier|here|click here|link|mehr|more|this)\]\(' 2>/dev/null || echo 0)
  [ "$bad_links" -gt 0 ] && echo "WARN|${file}|non-descriptive-link-text"

  # 4. Color-only styling (inline HTML)
  local color_only
  color_only=$(rg -c 'style="[^"]*color:' "$file" 2>/dev/null || echo 0)
  [ "$color_only" -gt 0 ] && echo "WARN|${file}|colour-only-styling"
}
