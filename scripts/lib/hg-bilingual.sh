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
  elif [[ "$file" == *.md ]]; then
    local partner base_name partner_name
    case "$file" in
      *.en.md) partner="${file%.en.md}.md" ;;
      *) partner="${file%.md}.en.md" ;;
    esac
    base_name="${file##*/}"
    partner_name="${partner##*/}"
    # A language pair counts only when both files link back to each other.
    if [ -f "$partner" ] &&
       rg -Fq "(${partner_name})" "$file" 2>/dev/null &&
       rg -Fq "(${base_name})" "$partner" 2>/dev/null; then
      echo "PASS|${file}|bilingual-language-pair"
    else
      echo "WARN|${file}|bilingual-section-missing"
    fi
  else
    echo "WARN|${file}|bilingual-section-missing"
  fi
}
