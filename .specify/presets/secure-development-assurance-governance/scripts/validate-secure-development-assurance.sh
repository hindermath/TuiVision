#!/usr/bin/env bash
set -euo pipefail

readonly EXTERNAL_COMPARISON_BOUNDARY='HOSK/GWDG: ExternalComparison only; never local evidence'
readonly REQUIRED_SECURITY_VERSION='0.6.1'
TEMP_FILES=()
SDA_JQ=(jq)

cleanup() {
  if (( ${#TEMP_FILES[@]} > 0 )); then
    rm -f "${TEMP_FILES[@]}"
  fi
}

trap cleanup EXIT

die() {
  printf 'Blocked: %s\n' "$1" >&2
  exit 2
}

require_command() {
  command -v "$1" >/dev/null 2>&1 || die "Erforderliches Werkzeug fehlt: $1"
}

require_value() {
  local value="$1"
  local allowed="$2"
  local label="$3"
  case " $allowed " in
    *" $value "*) ;;
    *) die "Unzulässiger Wert '$value' für $label." ;;
  esac
}

require_json_text() {
  local file="$1"
  local query="$2"
  local label="$3"
  local value
  value="$("${SDA_JQ[@]}" -er "$query | select(type == \"string\" and length > 0)" "$file" 2>/dev/null)" ||
    die "$label fehlt oder ist leer: $file"
  printf '%s\n' "$value"
}

validate_relative_path() {
  local path="$1"
  local label="$2"
  [[ -n "$path" && "$path" != /* && "$path" != *\\* ]] ||
    die "$label muss repository-relativ sein: $path"
  case "/$path/" in
    */../*|*/./*) die "$label enthält ein unzulässiges Segment: $path" ;;
  esac
}

normalized_sha256() {
  local file="$1"
  # Native jq.exe kann normalisierte LF beim Ausgeben wieder in CRLF umwandeln.
  # Native jq.exe may turn normalized LF back into CRLF while writing output.
  if command -v sha256sum >/dev/null 2>&1; then
    "${SDA_JQ[@]}" -jRs 'sub("^\uFEFF"; "") | gsub("\r\n?"; "\n")' "$file" |
      tr -d '\r' |
      sha256sum | awk '{print $1}'
  else
    "${SDA_JQ[@]}" -jRs 'sub("^\uFEFF"; "") | gsub("\r\n?"; "\n")' "$file" |
      tr -d '\r' |
      shasum -a 256 | awk '{print $1}'
  fi
}

version_at_least() {
  awk -v actual="$1" -v required="$2" 'BEGIN {
    split(actual, a, "."); split(required, r, ".")
    for (i = 1; i <= 3; i++) {
      av = a[i] + 0; rv = r[i] + 0
      if (av > rv) exit 0
      if (av < rv) exit 1
    }
    exit 0
  }'
}

resolve_context() {
  local supplied="${1:-}"
  if [[ -n "$supplied" ]]; then
    printf '%s\n' "$supplied"
    return
  fi
  local latest
  latest="$(find docs/security/secure-development -mindepth 1 -maxdepth 1 -type d 2>/dev/null | LC_ALL=C sort | tail -n 1)"
  [[ -n "$latest" ]] || die 'Kein Evidence-Verzeichnis gefunden.'
  printf '%s\n' "$latest"
}

validate_security_dependency() {
  local manifest='.specify/presets/security-governance/preset.yml'
  [[ -f "$manifest" ]] || die "Fachliche Voraussetzung fehlt: security-governance >=$REQUIRED_SECURITY_VERSION"
  local version
  version="$(awk '
    /^preset:/ { in_preset=1; next }
    in_preset && /^[^[:space:]]/ { in_preset=0 }
    in_preset && /^[[:space:]]+version:[[:space:]]*/ {
      value=$0
      sub(/^[^:]+:[[:space:]]*/, "", value)
      gsub(/["[:space:]]/, "", value)
      print value
      exit
    }
  ' "$manifest")"
  [[ "$version" =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]] || die "security-governance-Version ist ungültig: $version"
  version_at_least "$version" "$REQUIRED_SECURITY_VERSION" ||
    die "security-governance $version ist älter als $REQUIRED_SECURITY_VERSION."
}

validate_review_metadata() {
  local file="$1"
  local owner reviewer reviewed_at review_due residual_risk trigger today
  owner="$(require_json_text "$file" '.review.owner' 'review.owner')"
  reviewer="$(require_json_text "$file" '.review.reviewer' 'review.reviewer')"
  reviewed_at="$(require_json_text "$file" '.review.reviewedAt' 'review.reviewedAt')"
  review_due="$(require_json_text "$file" '.review.reviewDue' 'review.reviewDue')"
  residual_risk="$(require_json_text "$file" '.review.residualRisk' 'review.residualRisk')"
  trigger="$(require_json_text "$file" '.review.reevaluationTrigger' 'review.reevaluationTrigger')"
  [[ -n "$owner" && -n "$reviewer" && -n "$residual_risk" && -n "$trigger" ]] || die "Review-Metadaten sind unvollständig: $file"
  [[ "$reviewed_at" =~ ^[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}([.][0-9]+)?(Z|[+-][0-9]{2}:[0-9]{2})$ ]] ||
    die "review.reviewedAt ist kein ISO-8601-Zeitpunkt: $file"
  [[ "$review_due" =~ ^[0-9]{4}-[0-9]{2}-[0-9]{2}$ ]] || die "review.reviewDue ist kein ISO-Datum: $file"
  today="$(date -u +%Y-%m-%d)"
  [[ "$review_due" > "$today" || "$review_due" == "$today" ]] || die "Review ist abgelaufen ($review_due): $file"
}

validate_assessments() {
  local file="$1"
  "${SDA_JQ[@]}" -e '.assessments | type == "array" and length > 0' "$file" >/dev/null || die "Assessments fehlen: $file"
  local duplicate_ids
  duplicate_ids="$("${SDA_JQ[@]}" -r '.assessments[].id // empty' "$file" | LC_ALL=C sort | uniq -d)"
  [[ -z "$duplicate_ids" ]] || die "Doppelte Assessment-ID in $file: $duplicate_ids"
  while IFS=$'\t' read -r id applicability implementation evidence; do
    [[ -n "$id" ]] || die "Assessment-ID fehlt: $file"
    require_value "$applicability" 'Applicable N/A Open' "assessments[$id].applicability"
    case "$implementation" in
      Fulfilled|'Partly Fulfilled'|'Not Fulfilled'|'Not Assessed') ;;
      *) die "Unzulässiger Umsetzungswert '$implementation' in $file." ;;
    esac
    [[ -n "$evidence" ]] || die "Evidence fehlt für Assessment $id: $file"
  done < <("${SDA_JQ[@]}" -r '.assessments[] | [(.id // ""),(.applicability // ""),(.implementation // ""),(.evidence // "")] | @tsv' "$file")

  "${SDA_JQ[@]}" -e '[.assessments[] | select(.applicability == "N/A") | select((.reason // "") == "" or (.reevaluationTrigger // "") == "")] | length == 0' "$file" >/dev/null ||
    die "N/A benötigt Begründung und Re-Evaluation-Trigger: $file"
  "${SDA_JQ[@]}" -e '[.assessments[] | select(.applicability == "Open" or (.applicability == "Applicable" and .implementation != "Fulfilled")) | select((.nextAction // "") == "" or (.owner // "") == "" or (.dueAt // "") == "")] | length == 0' "$file" >/dev/null ||
    die "Offene oder nicht erfüllte Punkte benötigen nächste Aktion, Owner und Zieltermin: $file"
}

validate_accepted_risks() {
  local file="$1"
  "${SDA_JQ[@]}" -e 'if has("acceptedRisks") then (.acceptedRisks | type == "array") else true end' "$file" >/dev/null ||
    die "acceptedRisks muss ein JSON-Array sein: $file"
  # Wie IsNullOrWhiteSpace in PowerShell: kein Typ-Coercing und dieselben Unicode-Leerzeichen.
  # Match PowerShell IsNullOrWhiteSpace: no type coercion and the same Unicode whitespace.
  "${SDA_JQ[@]}" -e 'all((.acceptedRisks // [])[];
    (.id? // null) | if type == "string" then
      test("[^\u0009-\u000d\u0020\u0085\u00a0\u1680\u2000-\u200a\u2028\u2029\u202f\u205f\u3000]")
    else false end)' "$file" >/dev/null || die "acceptedRisks.id fehlt oder ist leer: $file"
  "${SDA_JQ[@]}" -e '[(.acceptedRisks // [])[] | select((.owner // "") == "" or (.reviewer // "") == "" or (.reviewedAt // "") == "" or (.reviewDue // "") == "" or (.residualRisk // "") == "" or (.reevaluationTrigger // "") == "")] | length == 0' "$file" >/dev/null ||
    die "Akzeptierte Risiken benötigen Owner, Reviewer, Reviewdatum, Restrisiko und Wiedervorlage: $file"
}

validate_claim_boundary() {
  local file="$1"
  if "${SDA_JQ[@]}" -r '.. | strings' "$file" |
      grep -Eiq '(^|[^[:alnum:]])(c5[ -]?(compliant|certified|conformant|ready)|c5[- ]?(konform|zertifiziert|testatbereit)|iso[ /-]?[0-9]+[ -]?(compliant|certified|konform|zertifiziert)|testatbereit|attestation[- ]ready)([^[:alnum:]]|$)'; then
    die "Unzulässige Zertifizierungs-, Konformitäts- oder Testatbehauptung: $file"
  fi
}

validate_gate_file() {
  local file="$1"
  local expected_gate="$2"
  [[ -f "$file" ]] || die "Evidence fehlt: $file"
  # Ein Evidence-Artefakt ist genau ein JSON-Dokument, kein auswertbarer JSON-Datenstrom.
  # An evidence artefact is exactly one JSON document, not an evaluable JSON stream.
  "${SDA_JQ[@]}" -se 'length == 1' "$file" >/dev/null || die "Ungültiges JSON: $file"
  "${SDA_JQ[@]}" -e '.schemaVersion == "1.0" and .documentType == "SecureDevelopmentGateEvidence"' "$file" >/dev/null ||
    die "Evidence-Schema ist ungültig: $file"
  local gate outcome context_id mode
  gate="$(require_json_text "$file" '.gate' 'gate')"
  outcome="$(require_json_text "$file" '.outcome' 'outcome')"
  context_id="$(require_json_text "$file" '.contextId' 'contextId')"
  mode="$(require_json_text "$file" '.mode' 'mode')"
  [[ "$gate" == "$expected_gate" ]] || die "Gate-Drift in $file: $gate"
  [[ "$context_id" =~ ^[a-z0-9][a-z0-9-]*$ ]] || die "contextId ist ungültig: $file"
  require_value "$mode" 'training mixed development' 'mode'
  require_value "$outcome" 'Ready ReadyWithAcceptedRisks NeedsRemediation Blocked' 'outcome'
  validate_review_metadata "$file"
  validate_assessments "$file"
  validate_accepted_risks "$file"
  validate_claim_boundary "$file"
  if [[ "$outcome" == 'Ready' ]]; then
    "${SDA_JQ[@]}" -e '[.assessments[] | select(.applicability == "Open" or (.applicability == "Applicable" and .implementation != "Fulfilled"))] | length == 0' "$file" >/dev/null ||
      die "Ready ist bei offenen oder unerfüllten Pflichtpunkten unzulässig: $file"
  fi
  if [[ "$outcome" == 'ReadyWithAcceptedRisks' ]]; then
    "${SDA_JQ[@]}" -e '.acceptedRisks | type == "array" and length > 0' "$file" >/dev/null ||
      die "ReadyWithAcceptedRisks benötigt mindestens ein akzeptiertes Risiko: $file"
  fi
  "${SDA_JQ[@]}" -e --arg boundary "$EXTERNAL_COMPARISON_BOUNDARY" '.externalComparisonBoundary == $boundary' "$file" >/dev/null ||
    die "Externe Vergleichsgrenze fehlt: $file"
}

validate_document_version() {
  local full_path="$1"
  local relative="$2"
  local version="$3"
  local kind="$4"
  case "$kind" in
    guideline) grep -Fq "| Versionsnummer | $version |" "$full_path" || die "Richtlinienversion stimmt nicht: $relative" ;;
    compendium) grep -Fq "**Dokumentversion / Document version:** $version" "$full_path" || die "Sammelbandversion stimmt nicht: $relative" ;;
    *) grep -Fq "**Version / Version:** $version" "$full_path" || die "Dokumentversion stimmt nicht: $relative" ;;
  esac
}

validate_baseline_contract() {
  local baseline_file="$1"
  validate_security_dependency
  local manifest_path baseline_version expected_manifest_hash manifest_file actual_manifest_hash
  manifest_path="$(require_json_text "$baseline_file" '.baselineBinding.manifestPath' 'baselineBinding.manifestPath')"
  baseline_version="$(require_json_text "$baseline_file" '.baselineBinding.baselineVersion' 'baselineBinding.baselineVersion')"
  expected_manifest_hash="$(require_json_text "$baseline_file" '.baselineBinding.manifestNormalizedSha256' 'baselineBinding.manifestNormalizedSha256')"
  validate_relative_path "$manifest_path" 'baselineBinding.manifestPath'
  [[ "$expected_manifest_hash" =~ ^[0-9a-f]{64}$ ]] || die 'baselineBinding.manifestNormalizedSha256 ist ungültig.'
  manifest_file="$PWD/$manifest_path"
  [[ -f "$manifest_file" ]] || die "Baseline-Manifest fehlt: $manifest_path"
  "${SDA_JQ[@]}" -e . "$manifest_file" >/dev/null || die "Baseline-Manifest ist kein gültiges JSON: $manifest_path"
  "${SDA_JQ[@]}" -e '.schemaVersion == 1 and (.checklists | type == "array" and length == 12)' "$manifest_file" >/dev/null ||
    die "Baseline-Manifest benötigt Schema 1 und genau zwölf Checklisten: $manifest_path"
  actual_manifest_hash="$(normalized_sha256 "$manifest_file")"
  [[ "$actual_manifest_hash" == "$expected_manifest_hash" ]] || die "Manifest-Hashdrift: $manifest_path"
  [[ "$("${SDA_JQ[@]}" -r '.baselineVersion // empty' "$manifest_file")" == "$baseline_version" ]] || die 'Baseline-Version und Evidence-Bindung stimmen nicht überein.'
  "${SDA_JQ[@]}" -e '.statusModel.applicability == ["Applicable","N/A","Open"] and .statusModel.implementation == ["Fulfilled","Partly Fulfilled","Not Fulfilled","Not Assessed"]' "$manifest_file" >/dev/null ||
    die 'Statusmodell im Baseline-Manifest ist ungültig.'

  local actual_ids expected_ids
  actual_ids="$("${SDA_JQ[@]}" -r '.checklists[].id' "$manifest_file" | LC_ALL=C sort)"
  expected_ids="$(printf 'CL-%02d\n' {1..12})"
  [[ "$actual_ids" == "$expected_ids" ]] || die 'Checklisten-IDs müssen eindeutig CL-01 bis CL-12 entsprechen.'

  "${SDA_JQ[@]}" -e '.baselineBinding.documentBindings | type == "array" and length > 0' "$baseline_file" >/dev/null ||
    die 'baselineBinding.documentBindings fehlt.'
  local duplicate_paths
  duplicate_paths="$("${SDA_JQ[@]}" -r '.baselineBinding.documentBindings[].path // empty' "$baseline_file" | LC_ALL=C sort | uniq -d)"
  [[ -z "$duplicate_paths" ]] || die "Doppelte Dokumentbindung: $duplicate_paths"

  local expected_count binding_count
  expected_count="$("${SDA_JQ[@]}" '1 + 1 + (.checklists | length) + (.relatedDocuments | length) + (.learningDocuments | length)' "$manifest_file")"
  binding_count="$("${SDA_JQ[@]}" '.baselineBinding.documentBindings | length' "$baseline_file")"
  [[ "$binding_count" -eq "$expected_count" ]] || die "Dokumentbindungen unvollständig: erwartet $expected_count, gefunden $binding_count."

  while IFS=$'\t' read -r relative version kind; do
    validate_relative_path "$relative" 'Manifest-Dokumentpfad'
    local binding_count_for_path bound_version bound_hash full_path actual_hash
    binding_count_for_path="$("${SDA_JQ[@]}" --arg path "$relative" '[.baselineBinding.documentBindings[] | select(.path == $path)] | length' "$baseline_file")"
    [[ "$binding_count_for_path" -eq 1 ]] || die "Dokumentbindung fehlt oder ist doppelt: $relative"
    bound_version="$("${SDA_JQ[@]}" -r --arg path "$relative" '.baselineBinding.documentBindings[] | select(.path == $path) | .version // empty' "$baseline_file")"
    bound_hash="$("${SDA_JQ[@]}" -r --arg path "$relative" '.baselineBinding.documentBindings[] | select(.path == $path) | .normalizedSha256 // empty' "$baseline_file")"
    [[ "$bound_version" == "$version" ]] || die "Versionsbindung stimmt nicht: $relative"
    [[ "$bound_hash" =~ ^[0-9a-f]{64}$ ]] || die "Hashbindung ist ungültig: $relative"
    full_path="$(dirname "$manifest_file")/$relative"
    [[ -f "$full_path" ]] || die "Kontrolliertes Dokument fehlt: $relative"
    actual_hash="$(normalized_sha256 "$full_path")"
    [[ "$actual_hash" == "$bound_hash" ]] || die "Dokument-Hashdrift: $relative"
    validate_document_version "$full_path" "$relative" "$version" "$kind"
  done < <("${SDA_JQ[@]}" -r '
    [.guideline.path,.guideline.version,"guideline"],
    [.compendium.path,.compendium.version,"compendium"],
    (.checklists[] | [.path,.version,"checklist"]),
    (.relatedDocuments[] | [.path,.version,"related"]),
    (.learningDocuments[] | [.path,.version,"learning"])
    | @tsv
  ' "$manifest_file")

  local ids_file compendium_ids_file
  ids_file="$(mktemp)"
  compendium_ids_file="$(mktemp)"
  TEMP_FILES+=("$ids_file" "$compendium_ids_file")
  while IFS=$'\t' read -r id relative _version; do
    local checklist_file
    checklist_file="$(dirname "$manifest_file")/$relative"
    grep -Fq "**Dokument-ID / Document ID:** $id" "$checklist_file" || die "Dokument-ID stimmt nicht: $relative"
    grep -Eo '^#### CL-[0-9]{2}-[0-9]{2}:' "$checklist_file" | sed 's/^#### //; s/:$//' >> "$ids_file"
  done < <("${SDA_JQ[@]}" -r '.checklists[] | [.id,.path,.version] | @tsv' "$manifest_file")
  local item_count actual_item_count
  item_count="$("${SDA_JQ[@]}" -r '.checklistItemCount' "$manifest_file")"
  actual_item_count="$(wc -l < "$ids_file" | tr -d ' ')"
  [[ "$actual_item_count" -eq "$item_count" ]] || die "Checklistenmenge stimmt nicht: erwartet $item_count, gefunden $actual_item_count."
  [[ -z "$(LC_ALL=C sort "$ids_file" | uniq -d)" ]] || die 'Doppelte Prüfpunkte in den zwölf Checklisten.'
  grep -Fxq 'CL-02-13' "$ids_file" || die 'Der projektgeführte Prüfpunkt CL-02-13 Cloud-Compliance-Assurance fehlt.'
  local compendium_path
  compendium_path="$("${SDA_JQ[@]}" -r '.compendium.path' "$manifest_file")"
  grep -Eo '^#### CL-[0-9]{2}-[0-9]{2}:' "$(dirname "$manifest_file")/$compendium_path" | sed 's/^#### //; s/:$//' > "$compendium_ids_file"
  [[ "$(LC_ALL=C sort "$ids_file")" == "$(LC_ALL=C sort "$compendium_ids_file")" ]] || die 'Sammelbanddrift gegenüber den zwölf Einzelchecklisten.'
  grep -Fq "**Baseline-Version / Baseline version:** $baseline_version" "$(dirname "$manifest_file")/$compendium_path" ||
    die 'Baseline-Version im Sammelband stimmt nicht.'
  rm -f "$ids_file" "$compendium_ids_file"

  while IFS= read -r relative; do
    validate_relative_path "$relative" 'Verwalteter Referenzpfad'
    [[ -f "$(dirname "$manifest_file")/$relative" ]] || die "Verwaltete Referenz fehlt: $relative"
  done < <("${SDA_JQ[@]}" -r '(.managedBinaryFiles + .managedReferenceFiles)[]' "$manifest_file")
}

validate_image_impact() {
  local file="$1"
  "${SDA_JQ[@]}" -e '.imageChecks | has("build") and has("compose") and has("toolchain") and has("ociDigest") and has("sbom") and has("secrets") and has("mounts") and has("network") and has("ci")' "$file" >/dev/null ||
    die 'Image-Impact-Nachweise sind unvollständig.'
  while IFS=$'\t' read -r key value; do
    require_value "$value" 'Fulfilled Partly-Fulfilled Not-Fulfilled Not-Assessed N/A' "imageChecks.$key"
  done < <("${SDA_JQ[@]}" -r '.imageChecks | to_entries[] | [.key, (.value | gsub(" "; "-"))] | @tsv' "$file")
  if [[ "$("${SDA_JQ[@]}" -r '.outcome' "$file")" == 'Ready' ]]; then
    "${SDA_JQ[@]}" -e '[.imageChecks[] | select(. != "Fulfilled" and . != "N/A")] | length == 0' "$file" >/dev/null ||
      die 'Ready ist bei offenen Image-Impact-Prüfungen unzulässig.'
  fi
}

validate_closure() {
  local file="$1"
  "${SDA_JQ[@]}" -e '.humanDecisions | has("technicalValidation") and has("pilotAuthorization") and has("projectAcceptance") and has("generalRelease")' "$file" >/dev/null ||
    die 'Getrennte Entscheidungsgrenzen fehlen.'
  while IFS=$'\t' read -r key status authority evidence; do
    require_value "$status" 'Fulfilled Open Blocked Not-Assessed' "humanDecisions.$key.status"
    [[ -n "$authority" && -n "$evidence" ]] || die "Entscheidungsgrenze ist unvollständig: $key"
  done < <("${SDA_JQ[@]}" -r '.humanDecisions | to_entries[] | [.key, (.value.status | gsub(" "; "-")), (.value.authority // ""), (.value.evidence // "")] | @tsv' "$file")
  local technical
  technical="$("${SDA_JQ[@]}" -r '.humanDecisions.technicalValidation.status' "$file")"
  for decision in pilotAuthorization projectAcceptance generalRelease; do
    local status
    status="$("${SDA_JQ[@]}" -r --arg key "$decision" '.humanDecisions[$key].status' "$file")"
    if [[ "$technical" != 'Fulfilled' && "$status" == 'Fulfilled' ]]; then
      die "Menschliche Freigabe darf technische Validierung nicht überspringen: $decision"
    fi
  done
}

validate_context() {
  local context_dir="$1"
  [[ -d "$context_dir" ]] || die "Evidence-Verzeichnis fehlt: $context_dir"
  [[ -f "$context_dir/evidence-matrix.md" ]] || die 'evidence-matrix.md fehlt.'
  validate_gate_file "$context_dir/baseline.json" baseline
  validate_baseline_contract "$context_dir/baseline.json"
  local delta_count=0 delta_file
  while IFS= read -r delta_file; do
    validate_gate_file "$delta_file" delta
    delta_count=$((delta_count + 1))
  done < <(find "$context_dir/deltas" -maxdepth 1 -type f -name '*.json' 2>/dev/null | LC_ALL=C sort)
  (( delta_count > 0 )) || die 'Mindestens eine Delta-Evidence fehlt.'
  validate_gate_file "$context_dir/closure.json" closure
  validate_closure "$context_dir/closure.json"
  validate_gate_file "$context_dir/image-impact.json" image-impact
  validate_image_impact "$context_dir/image-impact.json"
}

print_status() {
  local context_dir="$1"
  local baseline_outcome delta_outcome closure_outcome image_outcome overall next_action
  baseline_outcome="$("${SDA_JQ[@]}" -r '.outcome' "$context_dir/baseline.json")"
  delta_outcome="$(find "$context_dir/deltas" -maxdepth 1 -type f -name '*.json' -print0 | xargs -0 -n1 "${SDA_JQ[@]}" -r '.outcome' | awk '
    BEGIN {result="Ready"}
    $0=="Blocked" {result="Blocked"}
    $0=="NeedsRemediation" && result!="Blocked" {result="NeedsRemediation"}
    $0=="ReadyWithAcceptedRisks" && result=="Ready" {result="ReadyWithAcceptedRisks"}
    END {print result}
  ')"
  closure_outcome="$("${SDA_JQ[@]}" -r '.outcome' "$context_dir/closure.json")"
  image_outcome="$("${SDA_JQ[@]}" -r '.outcome' "$context_dir/image-impact.json")"
  overall="$(printf '%s\n' "$baseline_outcome" "$delta_outcome" "$closure_outcome" "$image_outcome" | awk '
    BEGIN {result="Ready"}
    $0=="Blocked" {result="Blocked"}
    $0=="NeedsRemediation" && result!="Blocked" {result="NeedsRemediation"}
    $0=="ReadyWithAcceptedRisks" && result=="Ready" {result="ReadyWithAcceptedRisks"}
    END {print result}
  ')"
  next_action="$("${SDA_JQ[@]}" -r '.nextAction // empty' "$context_dir/closure.json")"
  [[ -n "$next_action" ]] || next_action='No further technical action recorded.'
  printf 'Context: %s\n' "$context_dir"
  printf 'Gate baseline: %s\n' "$baseline_outcome"
  printf 'Gate delta: %s\n' "$delta_outcome"
  printf 'Gate closure: %s\n' "$closure_outcome"
  printf 'Gate image-impact: %s\n' "$image_outcome"
  printf 'Overall: %s\n' "$overall"
  for decision in technicalValidation pilotAuthorization projectAcceptance generalRelease; do
    printf 'Decision %s: %s\n' "$decision" "$("${SDA_JQ[@]}" -r --arg key "$decision" '.humanDecisions[$key].status' "$context_dir/closure.json")"
  done
  printf 'Next action: %s\n' "$next_action"
}

require_command jq

# Native Windows-Ausgaben brauchen --binary; POSIX-jq 1.6 bleibt ohne dieses Flag nutzbar.
# Native Windows output needs --binary; POSIX jq 1.6 remains usable without that flag.
if ( command jq --binary -n null >/dev/null 2>&1 ); then
  SDA_JQ+=(--binary)
else
  jq_line_probe="$(command jq -nr '"a\nb"' 2>&1)" || die 'jq-Ausgabeprobe fehlgeschlagen / jq output probe failed.'
  [[ "$jq_line_probe" != *$'\r'* ]] ||
    die 'Dieses jq erzeugt CRLF und benötigt --binary-Unterstützung / This jq emits CRLF and requires --binary support.'
  [[ "$jq_line_probe" == $'a\nb' ]] || die 'jq-Ausgabeprobe fehlgeschlagen / jq output probe failed.'
fi
readonly -a SDA_JQ

action="${1:-status}"
case "$action" in
  status)
    context_dir="$(resolve_context "${2:-}")"
    validate_context "$context_dir"
    print_status "$context_dir"
    ;;
  review)
    [[ $# -eq 4 ]] || die 'Syntax: review <baseline|delta|closure|image-impact> <context-id> <training|mixed|development>'
    gate="$2"
    context_id="$3"
    mode="$4"
    require_value "$gate" 'baseline delta closure image-impact' 'gate'
    require_value "$mode" 'training mixed development' 'mode'
    [[ "$context_id" =~ ^[a-z0-9][a-z0-9-]*$ ]] || die 'context-id ist ungültig.'
    # Nur die volle ID hinter dem Datum bindet Autoritaet; Mehrdeutigkeit wird nicht aufgeloest.
    # Only the full ID after the date binds authority; never guess among duplicate contexts.
    context_matches=()
    while IFS= read -r candidate; do
      [[ "${candidate##*/}" =~ ^[0-9]{4}-[0-9]{2}-[0-9]{2}-${context_id}$ ]] && context_matches+=("$candidate")
    done < <(find docs/security/secure-development -mindepth 1 -maxdepth 1 -type d 2>/dev/null)
    [[ ${#context_matches[@]} -gt 0 ]] || die "Kontext nicht gefunden: $context_id"
    [[ ${#context_matches[@]} -eq 1 ]] || die "Kontext muss genau einmal vorhanden sein: $context_id"
    context_dir="${context_matches[0]}"
    case "$gate" in
      baseline)
        gate_file="$context_dir/baseline.json"
        validate_gate_file "$gate_file" baseline
        validate_baseline_contract "$gate_file"
        ;;
      closure)
        gate_file="$context_dir/closure.json"
        validate_gate_file "$gate_file" closure
        validate_closure "$gate_file"
        ;;
      image-impact)
        gate_file="$context_dir/image-impact.json"
        validate_gate_file "$gate_file" image-impact
        validate_image_impact "$gate_file"
        ;;
      delta)
        gate_file="$(find "$context_dir/deltas" -maxdepth 1 -type f -name '*.json' | LC_ALL=C sort | tail -n 1)"
        [[ -n "$gate_file" ]] || die 'Delta-Evidence fehlt.'
        validate_gate_file "$gate_file" delta
        ;;
    esac
    [[ "$(require_json_text "$gate_file" '.contextId' 'contextId')" == "$context_id" ]] || die "Kontext-ID-Drift: $gate_file"
    [[ "$(require_json_text "$gate_file" '.mode' 'mode')" == "$mode" ]] || die "Betriebsart-Drift: $gate_file"
    if [[ "$mode" != development ]]; then
      [[ -f "docs/runbooks/secure-development/$gate-$context_id.md" ]] ||
        die "Runbook fehlt für $mode: $gate-$context_id.md"
    else
      [[ -f "docs/runbooks/secure-development/$gate-$context_id.md" ]] ||
        "${SDA_JQ[@]}" -e '.runbookApplicability == "N/A" and (.runbookRationale // "") != ""' "$gate_file" >/dev/null ||
        die "Development benötigt Runbook oder begründetes N/A: $gate-$context_id.md"
    fi
    printf 'Reviewed: gate=%s context=%s mode=%s outcome=%s\n' "$gate" "$context_id" "$mode" "$("${SDA_JQ[@]}" -r '.outcome' "$gate_file")"
    ;;
  *) die "Unbekannte Aktion: $action" ;;
esac
