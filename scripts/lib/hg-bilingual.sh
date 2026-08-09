#!/usr/bin/env bash
# hg-bilingual.sh — Bilingualitätsprüfung Markdown (FR-004, R-01)
# Prüft ob Datei deutsche UND englische Heading-Keywords enthält

DE_PATTERNS="Überblick|Verwendung|Einrichtung|Voraussetzungen|Azubi|Hinweise|Zweck|Beschreibung|Schnellstart|Für Azubis|Zusammenfassung|Anleitung"
EN_PATTERNS="Overview|Usage|Setup|Prerequisites|Apprentice|Notes|Purpose|Description|Quickstart|For Apprentices|Summary|Instructions"

hg_check_bilingual() {
  local file="$1"

  if ! [ -f "$file" ]; then
    return 0
  fi

  # Only check markdown files
  case "$file" in
    *.md|*.MD) ;;
    *) return 0 ;;
  esac

  local has_de has_en
  has_de=$(rg -ic "^#{1,3} .*(${DE_PATTERNS})" "$file" 2>/dev/null || echo 0)
  has_en=$(rg -ic "^#{1,3} .*(${EN_PATTERNS})" "$file" 2>/dev/null || echo 0)

  if [ "$has_de" -gt 0 ] && [ "$has_en" -gt 0 ]; then
    echo "PASS|${file}|bilingual-ok"
  else
    local partner base_name partner_name lower_file pair_root candidate
    lower_file="$(printf '%s' "$file" | tr '[:upper:]' '[:lower:]')"
    local -a candidates
    if [[ "$lower_file" == *.en.md ]]; then
      pair_root="$(printf '%s' "$file" | sed -E 's/[.][eE][nN][.][mM][dD]$//')"
      candidates=("${pair_root}.md" "${pair_root}.MD")
    else
      pair_root="$(printf '%s' "$file" | sed -E 's/[.][mM][dD]$//')"
      candidates=(
        "${pair_root}.en.md" "${pair_root}.EN.md" \
        "${pair_root}.en.MD" "${pair_root}.EN.MD"
      )
    fi
    base_name="${file##*/}"
    for candidate in "${candidates[@]}"; do
      partner_name="${candidate##*/}"
      # Link text also selects the intended casing on case-insensitive hosts.
      if [ -f "$candidate" ] &&
         rg -Fq "(${partner_name})" "$file" 2>/dev/null &&
         rg -Fq "(${base_name})" "$candidate" 2>/dev/null; then
        echo "PASS|${file}|bilingual-language-pair"
        return 0
      fi
    done
    echo "WARN|${file}|bilingual-section-missing"
  fi
}
