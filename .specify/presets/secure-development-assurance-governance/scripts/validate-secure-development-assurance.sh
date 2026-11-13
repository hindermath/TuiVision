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

resolve_confined_file() {
  local path="$1" root="$2" label="$3" physical_root physical_path
  [[ -f "$path" ]] || die "$label fehlt: $path"
  physical_root="$(realpath "$root")" || die "$label-Wurzel kann nicht aufgeloest werden: $root"
  physical_path="$(realpath "$path")" || die "$label kann nicht aufgeloest werden: $path"
  [[ "$physical_path" == "$physical_root/"* ]] || die "$label verlaesst das Repository: $path"
  printf '%s\n' "$physical_path"
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
  local owner reviewer reviewed_at review_due residual_risk trigger today parsed_due
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
  if [[ "$(uname -s)" == Darwin ]]; then
    parsed_due="$(date -u -j -f '%Y-%m-%d' "$review_due" '+%Y-%m-%d' 2>/dev/null)" ||
      die "review.reviewDue ist kein ISO-Datum: $file"
  else
    parsed_due="$(date -u -d "$review_due" '+%Y-%m-%d' 2>/dev/null)" ||
      die "review.reviewDue ist kein ISO-Datum: $file"
  fi
  [[ "$parsed_due" == "$review_due" ]] || die "review.reviewDue ist kein ISO-Datum: $file"
  today="$(date -u +%Y-%m-%d)"
  [[ "$review_due" > "$today" || "$review_due" == "$today" ]] || die "Review ist abgelaufen ($review_due): $file"
}

validate_assessments() {
  local file="$1"
  "${SDA_JQ[@]}" -e '.assessments | type == "array" and length > 0' "$file" >/dev/null || die "Assessments fehlen: $file"
  "${SDA_JQ[@]}" -e 'all(.assessments[];
    type == "object" and (.id | type == "string") and
    (.applicability | type == "string") and
    (.implementation | type == "string") and (.evidence | type == "string"))' "$file" >/dev/null ||
    die "Assessment-Felder sind ungueltig: $file"
  local duplicate_ids
  duplicate_ids="$("${SDA_JQ[@]}" -r '.assessments[].id // empty' "$file" | LC_ALL=C sort | uniq -d)"
  [[ -z "$duplicate_ids" ]] || die "Doppelte Assessment-ID in $file: $duplicate_ids"
  local rows
  rows="$("${SDA_JQ[@]}" -r '.assessments[] | [(.id // ""),(.applicability // ""),(.implementation // ""),(.evidence // "")] | @tsv' "$file")" ||
    die "Assessment-Zeilen koennen nicht gelesen werden: $file"
  while IFS=$'\t' read -r id applicability implementation evidence; do
    [[ -n "$id" ]] || die "Assessment-ID fehlt: $file"
    require_value "$applicability" 'Applicable N/A Open' "assessments[$id].applicability"
    case "$implementation" in
      Fulfilled|'Partly Fulfilled'|'Not Fulfilled'|'Not Assessed') ;;
      *) die "Unzulässiger Umsetzungswert '$implementation' in $file." ;;
    esac
    [[ -n "$evidence" ]] || die "Evidence fehlt für Assessment $id: $file"
  done <<< "$rows"

  "${SDA_JQ[@]}" -e '[.assessments[] | select(.applicability == "N/A") | select((.reason // "") == "" or (.reevaluationTrigger // "") == "")] | length == 0' "$file" >/dev/null ||
    die "N/A benötigt Begründung und Re-Evaluation-Trigger: $file"
  "${SDA_JQ[@]}" -e '[.assessments[] | select(.applicability == "Open" or (.applicability == "Applicable" and .implementation != "Fulfilled")) | select((.nextAction // "") == "" or (.owner // "") == "" or (.dueAt // "") == "")] | length == 0' "$file" >/dev/null ||
    die "Offene oder nicht erfüllte Punkte benötigen nächste Aktion, Owner und Zieltermin: $file"
}

validate_accepted_risks() {
  local file="$1"
  local reviewed_at review_due parsed_due parsed_review_date today zone
  "${SDA_JQ[@]}" -e 'if has("acceptedRisks") then (.acceptedRisks | type == "array") else true end' "$file" >/dev/null ||
    die "acceptedRisks muss ein JSON-Array sein: $file"
  # Wie IsNullOrWhiteSpace in PowerShell: kein Typ-Coercing und dieselben Unicode-Leerzeichen.
  # Match PowerShell IsNullOrWhiteSpace: no type coercion and the same Unicode whitespace.
  "${SDA_JQ[@]}" -e 'all((.acceptedRisks // [])[];
    (.id? // null) | if type == "string" then
      test("[^\u0009-\u000d\u0020\u0085\u00a0\u1680\u2000-\u200a\u2028\u2029\u202f\u205f\u3000]")
    else false end)' "$file" >/dev/null || die "acceptedRisks.id fehlt oder ist leer: $file"
  "${SDA_JQ[@]}" -e 'all((.acceptedRisks // [])[]; . as $risk |
    all(["owner", "reviewer", "reviewedAt", "reviewDue", "scope", "rationale", "residualRisk", "reevaluationTrigger"][];
      ($risk[.] | type) == "string" and
      ($risk[.] | test("[^\u0009-\u000d\u0020\u0085\u00a0\u1680\u2000-\u200a\u2028\u2029\u202f\u205f\u3000]"))))' "$file" >/dev/null ||
    die "Akzeptierte Risiken benötigen Owner, Reviewer, Reviewdatum, Scope, Begründung, Restrisiko und Wiedervorlage: $file"
  while IFS=$'\t' read -r reviewed_at review_due; do
    [[ "$reviewed_at" =~ ^[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}([.][0-9]+)?(Z|[+-][0-9]{2}:[0-9]{2})$ ]] ||
      die "acceptedRisks.reviewedAt ist kein ISO-8601-Zeitpunkt: $file"
    if [[ "$(uname -s)" == Darwin ]]; then
      parsed_review_date="$(date -u -j -f '%Y-%m-%d' "${reviewed_at:0:10}" '+%Y-%m-%d' 2>/dev/null)" ||
        die "acceptedRisks.reviewedAt ist kein gueltiger Kalendertag: $file"
    else
      parsed_review_date="$(date -u -d "${reviewed_at:0:10}" '+%Y-%m-%d' 2>/dev/null)" ||
        die "acceptedRisks.reviewedAt ist kein gueltiger Kalendertag: $file"
    fi
    [[ "$parsed_review_date" == "${reviewed_at:0:10}" ]] &&
      (( 10#${reviewed_at:11:2} <= 23 && 10#${reviewed_at:14:2} <= 59 &&
        10#${reviewed_at:17:2} <= 59 )) ||
      die "acceptedRisks.reviewedAt ist kein gueltiger Zeitpunkt: $file"
    if [[ "$reviewed_at" != *Z ]]; then
      zone="${reviewed_at: -6}"
      (( 10#${zone:1:2} <= 14 && 10#${zone:4:2} <= 59 &&
        (10#${zone:1:2} < 14 || 10#${zone:4:2} == 0) )) ||
        die "acceptedRisks.reviewedAt hat einen ungueltigen UTC-Offset: $file"
    fi
    if [[ "$(uname -s)" == Darwin ]]; then
      review_day_epoch="$(date -u -j -f '%Y-%m-%dT%H:%M:%S' "${parsed_review_date}T00:00:00" '+%s')" ||
        die "acceptedRisks.reviewedAt ist kein gueltiger Zeitpunkt: $file"
    else
      review_day_epoch="$(date -u -d "$parsed_review_date" '+%s')" ||
        die "acceptedRisks.reviewedAt ist kein gueltiger Zeitpunkt: $file"
    fi
    review_offset=0
    if [[ "$reviewed_at" != *Z ]]; then
      review_offset=$((10#${zone:1:2} * 3600 + 10#${zone:4:2} * 60))
      if [[ "${zone:0:1}" == - ]]; then
        review_offset=$((-review_offset))
      fi
    fi
    review_epoch=$((review_day_epoch + 10#${reviewed_at:11:2} * 3600 +
      10#${reviewed_at:14:2} * 60 + 10#${reviewed_at:17:2} - review_offset))
    now_stamp="$(date -u '+%s %N')"
    now_epoch="${now_stamp%% *}"
    (( review_epoch <= now_epoch )) ||
      die "acceptedRisks.reviewedAt liegt in der Zukunft: $file"
    if (( review_epoch == now_epoch )) &&
      [[ "$reviewed_at" =~ \.([0-9]+)(Z|[+-][0-9]{2}:[0-9]{2})$ ]]; then
      review_fraction="${BASH_REMATCH[1]}"
      now_fraction="${now_stamp#* }"
      [[ "$now_fraction" =~ ^[0-9]{9}$ ]] ||
        die "acceptedRisks.reviewedAt kann nicht sub-sekundengenau geprueft werden: $file"
      while (( ${#review_fraction} < ${#now_fraction} )); do review_fraction="${review_fraction}0"; done
      while (( ${#now_fraction} < ${#review_fraction} )); do now_fraction="${now_fraction}0"; done
      if [[ "$review_fraction" > "$now_fraction" ]]; then
        die "acceptedRisks.reviewedAt liegt in der Zukunft: $file"
      fi
    fi
    [[ "$review_due" =~ ^[0-9]{4}-[0-9]{2}-[0-9]{2}$ ]] || die "acceptedRisks.reviewDue ist kein ISO-Datum: $file"
    if [[ "$(uname -s)" == Darwin ]]; then
      parsed_due="$(date -u -j -f '%Y-%m-%d' "$review_due" '+%Y-%m-%d' 2>/dev/null)" ||
        die "acceptedRisks.reviewDue ist kein ISO-Datum: $file"
    else
      parsed_due="$(date -u -d "$review_due" '+%Y-%m-%d' 2>/dev/null)" ||
        die "acceptedRisks.reviewDue ist kein ISO-Datum: $file"
    fi
    [[ "$parsed_due" == "$review_due" ]] || die "acceptedRisks.reviewDue ist kein ISO-Datum: $file"
    today="$(date -u +%Y-%m-%d)"
    [[ "$review_due" > "$today" || "$review_due" == "$today" ]] ||
      die "Akzeptiertes Risiko ist abgelaufen ($review_due): $file"
  done < <("${SDA_JQ[@]}" -r '(.acceptedRisks // [])[] | [.reviewedAt, .reviewDue] | @tsv' "$file")
}

validate_claim_boundary() {
  local file="$1"
  local pattern='(^|[^a-z0-9])(c5[ -]?(compliant|certified|conformant|ready)|c5[- ]?(konform|zertifiziert|testatbereit)|iso[ /-]?[0-9]+[ -]?(compliant|certified|konform|zertifiziert)|testatbereit|attestation[- ]ready)([^a-z0-9]|$)'
  # Let jq inspect decoded JSON values and keys in one process. A grep pipe can
  # close early and make jq fail with SIGPIPE under pipefail after a real match.
  if "${SDA_JQ[@]}" -e --arg pattern "$pattern" \
      'any(((.. | strings), (.. | objects | keys[])); test($pattern; "i"))' "$file" >/dev/null; then
    die "Unzulässige Zertifizierungs-, Konformitäts- oder Testatbehauptung: $file"
  else
    local status=$?
    [[ $status -eq 1 ]] || die "Claim-Prüfung fehlgeschlagen: $file"
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
    "${SDA_JQ[@]}" -e '. as $doc |
      all([.assessments[] | select(.applicability == "Open" or (.applicability == "Applicable" and .implementation != "Fulfilled")) | .id][];
        . as $id | any($doc.acceptedRisks[]; (.assessmentIds? | type) == "array" and (.assessmentIds | index($id)) != null))' "$file" >/dev/null ||
      die "ReadyWithAcceptedRisks benötigt ein zugeordnetes Risiko für jeden offenen Pflichtpunkt: $file"
  fi
  "${SDA_JQ[@]}" -e --arg boundary "$EXTERNAL_COMPARISON_BOUNDARY" '.externalComparisonBoundary == $boundary' "$file" >/dev/null ||
    die "Externe Vergleichsgrenze fehlt: $file"
}

validate_active_metadata() {
  local full_path="$1" relative="$2" prefix="$3" expected="$4" heading="${5:-}" embedded_baseline="${6:-}"
  # Only active metadata counts as a match; duplicate fields anywhere fail closed.
  # Nur aktive Metadaten gelten als Treffer; doppelte Felder bleiben unzulaessig.
  "${SDA_JQ[@]}" -jRs 'sub("^\uFEFF"; "") | gsub("\r\n?"; "\n")' "$full_path" |
    awk -v prefix="$prefix" -v expected="$expected" -v heading="$heading" -v embedded_baseline="$embedded_baseline" '
      { sub(/\r$/, "") }
      heading != "" {
        if ($0 ~ /^## /) { active = ($0 == heading); if (active) sections++; table = 0; ended = 0 }
        if (active && $0 ~ /^\|/ && !ended) table = 1
        if (active && $0 != heading && $0 !~ /^\|/ && (table || $0 !~ /^[[:space:]]*$/)) ended = 1
        if (index($0, prefix) == 1 && split($0, cells, "[|]") == 4) {
          count++; if (active && table && !ended && $0 == expected) matched++
        }
        next
      }
      !started && /^#+ / { headings++; if (headings > 1) closed = 1 }
      !started && headings && $0 !~ /^#+ / && $0 !~ /^[[:space:]]*$/ && $0 !~ /^>/ && $0 !~ /^\*\*[^*]+:\*\*/ { closed = 1 }
      !started && !closed && /^\*\*[^*]+:\*\*/ { started = 1; active = 1 }
      started && $0 !~ /^\*\*[^*]+:\*\*/ { active = 0 }
      # Embedded checklist blocks own their baseline, separately from the compendium header.
      embedded_baseline && !active && /^\*\*Dokument-ID \/ Document ID:\*\* CL-(0[1-9]|1[0-2])$/ { embedded = 1; embedded_count = 0 }
      $0 !~ /^\*\*[^*]+:\*\*/ { embedded = 0 }
      embedded && index($0, prefix) == 1 { embedded_count++; if (embedded_count > 1) invalid = 1; next }
      index($0, prefix) == 1 { count++; if (active && $0 == expected) matched++ }
      END { exit !(count == 1 && matched == 1 && !invalid && (heading == "" || sections == 1)) }
    ' || die "Aktive Metadaten fehlen, weichen ab oder sind doppelt: $relative ($prefix)"
}

validate_document_version() {
  local full_path="$1"
  local relative="$2"
  local version="$3"
  local kind="$4"
  case "$kind" in
    guideline)
      validate_active_metadata "$full_path" "$relative" '| Versionsnummer |' "| Versionsnummer | $version |" '## Dokumenten-Metadaten'
      validate_active_metadata "$full_path" "$relative" '| Version |' "| Version | $version |" '## Document Metadata'
      ;;
    compendium) validate_active_metadata "$full_path" "$relative" '**Dokumentversion / Document version:**' "**Dokumentversion / Document version:** $version" ;;
    *) validate_active_metadata "$full_path" "$relative" '**Version / Version:**' "**Version / Version:** $version" ;;
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
  manifest_file="$(resolve_confined_file "$PWD/$manifest_path" "$PWD" 'Baseline-Manifest')"
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
    full_path="$(resolve_confined_file "$(dirname "$manifest_file")/$relative" "$(dirname "$manifest_file")" 'Kontrolliertes Dokument')"
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
    checklist_file="$(resolve_confined_file "$(dirname "$manifest_file")/$relative" "$(dirname "$manifest_file")" 'Checkliste')"
    validate_active_metadata "$checklist_file" "$relative" '**Dokument-ID / Document ID:**' "**Dokument-ID / Document ID:** $id"
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
  local compendium_file
  compendium_file="$(resolve_confined_file "$(dirname "$manifest_file")/$compendium_path" "$(dirname "$manifest_file")" 'Sammelband')"
  grep -Eo '^#### CL-[0-9]{2}-[0-9]{2}:' "$compendium_file" | sed 's/^#### //; s/:$//' > "$compendium_ids_file"
  [[ "$(LC_ALL=C sort "$ids_file")" == "$(LC_ALL=C sort "$compendium_ids_file")" ]] || die 'Sammelbanddrift gegenüber den zwölf Einzelchecklisten.'
  validate_active_metadata "$compendium_file" "$compendium_path" '**Baseline-Version / Baseline version:**' "**Baseline-Version / Baseline version:** $baseline_version" '' 'embedded-baseline'
  rm -f "$ids_file" "$compendium_ids_file"

  while IFS= read -r relative; do
    validate_relative_path "$relative" 'Verwalteter Referenzpfad'
    resolve_confined_file "$(dirname "$manifest_file")/$relative" "$(dirname "$manifest_file")" 'Verwaltete Referenz' >/dev/null
  done < <("${SDA_JQ[@]}" -r '(.managedBinaryFiles + .managedReferenceFiles)[]' "$manifest_file")
}

validate_image_impact() {
  local file="$1"
  "${SDA_JQ[@]}" -e '.imageChecks | type == "object" and has("build") and has("compose") and has("toolchain") and has("ociDigest") and has("sbom") and has("secrets") and has("mounts") and has("network") and has("ci") and all(.[]; type == "string")' "$file" >/dev/null ||
    die 'Image-Impact-Nachweise sind unvollständig.'
  local rows
  rows="$("${SDA_JQ[@]}" -r '.imageChecks | to_entries[] | [.key, (.value | gsub(" "; "-"))] | @tsv' "$file")" ||
    die 'Image-Impact-Zeilen koennen nicht gelesen werden.'
  while IFS=$'\t' read -r key value; do
    require_value "$value" 'Fulfilled Partly-Fulfilled Not-Fulfilled Not-Assessed N/A' "imageChecks.$key"
  done <<< "$rows"
  if [[ "$("${SDA_JQ[@]}" -r '.outcome' "$file")" == 'Ready' ]]; then
    "${SDA_JQ[@]}" -e '[.imageChecks[] | select(. != "Fulfilled" and . != "N/A")] | length == 0' "$file" >/dev/null ||
      die 'Ready ist bei offenen Image-Impact-Prüfungen unzulässig.'
  fi
}

validate_closure() {
  local file="$1"
  "${SDA_JQ[@]}" -e '(if has("nextAction") and .nextAction != null then .nextAction else "" end) | (type == "string" and (explode | all(. >= 32 and (. <= 126 or . >= 160) and . != 8232 and . != 8233)))' "$file" >/dev/null ||
    die 'nextAction enthält Steuerzeichen oder ist kein Text.'
  "${SDA_JQ[@]}" -e '.humanDecisions | type == "object" and has("technicalValidation") and has("pilotAuthorization") and has("projectAcceptance") and has("generalRelease") and all(.[]; type == "object" and (.status | type == "string") and (.authority | type == "string") and (.evidence | type == "string"))' "$file" >/dev/null ||
    die 'Getrennte Entscheidungsgrenzen fehlen.'
  local rows
  rows="$("${SDA_JQ[@]}" -r '.humanDecisions | to_entries[] | [.key, (.value.status | gsub(" "; "-")), (.value.authority // ""), (.value.evidence // "")] | @tsv' "$file")" ||
    die 'Entscheidungsgrenzen koennen nicht gelesen werden.'
  while IFS=$'\t' read -r key status authority evidence; do
    require_value "$status" 'Fulfilled Open Blocked Not-Assessed' "humanDecisions.$key.status"
    [[ -n "$authority" && -n "$evidence" ]] || die "Entscheidungsgrenze ist unvollständig: $key"
  done <<< "$rows"
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
  local expected_id expected_mode
  expected_id="$(require_json_text "$context_dir/baseline.json" '.contextId' 'contextId')"
  expected_mode="$(require_json_text "$context_dir/baseline.json" '.mode' 'mode')"
  local delta_count=0 delta_file
  while IFS= read -r delta_file; do
    validate_gate_file "$delta_file" delta
    [[ "$(require_json_text "$delta_file" '.contextId' 'contextId')" == "$expected_id" ]] || die "Kontext-ID-Drift: $delta_file"
    [[ "$(require_json_text "$delta_file" '.mode' 'mode')" == "$expected_mode" ]] || die "Betriebsart-Drift: $delta_file"
    delta_count=$((delta_count + 1))
  done < <(find "$context_dir/deltas" -maxdepth 1 -type f -name '*.json' 2>/dev/null | LC_ALL=C sort)
  (( delta_count > 0 )) || die 'Mindestens eine Delta-Evidence fehlt.'
  validate_gate_file "$context_dir/closure.json" closure
  [[ "$(require_json_text "$context_dir/closure.json" '.contextId' 'contextId')" == "$expected_id" ]] || die 'Kontext-ID-Drift: closure.json'
  [[ "$(require_json_text "$context_dir/closure.json" '.mode' 'mode')" == "$expected_mode" ]] || die 'Betriebsart-Drift: closure.json'
  validate_closure "$context_dir/closure.json"
  validate_gate_file "$context_dir/image-impact.json" image-impact
  [[ "$(require_json_text "$context_dir/image-impact.json" '.contextId' 'contextId')" == "$expected_id" ]] || die 'Kontext-ID-Drift: image-impact.json'
  [[ "$(require_json_text "$context_dir/image-impact.json" '.mode' 'mode')" == "$expected_mode" ]] || die 'Betriebsart-Drift: image-impact.json'
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
  STATUS_OVERALL="$overall"
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
  status|ready)
    context_dir="$(resolve_context "${2:-}")"
    validate_context "$context_dir"
    print_status "$context_dir"
    if [[ "$action" == ready && "$STATUS_OVERALL" != Ready && "$STATUS_OVERALL" != ReadyWithAcceptedRisks ]]; then
      die "Assurance-Evidence ist nicht bereit: $STATUS_OVERALL"
    fi
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
