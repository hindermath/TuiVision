#!/usr/bin/env bash
# hg-deps.sh — Bezahlte NuGet-Paket-Erkennung (FR-016, R-06)
# Prüft *.csproj auf bekannte kostenpflichtige Komponentenlieferanten

PAID_VENDORS="DevExpress\.|Telerik\.|Syncfusion\.|Infragistics\.|MESCIUS\.|ComponentOne\.|GrapeCity\.|Actipro\."

hg_check_deps() {
  local dir="$1"

  if ! command -v rg &>/dev/null; then
    return 0
  fi

  # Find all *.csproj files in directory (max depth 5)
  local match
  while IFS= read -r match; do
    local filepath linenum package
    filepath=$(echo "$match" | cut -d: -f1)
    linenum=$(echo "$match" | cut -d: -f2)
    package=$(echo "$match" | grep -oE 'Include="[^"]*"' | head -1 | sed 's/Include="//;s/"//')
    echo "WARN|${filepath}:${linenum}|paid-dependency-detected: ${package}"
  done < <(rg --glob '*.csproj' \
    -n \
    "<PackageReference Include=\"(${PAID_VENDORS})" \
    "$dir" 2>/dev/null)
}
