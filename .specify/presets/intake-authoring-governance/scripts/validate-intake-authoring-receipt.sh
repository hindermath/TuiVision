#!/usr/bin/env bash
# Validate one intake-authoring receipt, its target, and local source hashes.
set -euo pipefail

usage() {
  printf '%s\n' 'Usage: validate-intake-authoring-receipt.sh --receipt FILE [--repo PATH]'
}

receipt=''
repo='.'
while [ "$#" -gt 0 ]; do
  case "$1" in
    --receipt) receipt="${2:-}"; shift 2 ;;
    --repo) repo="${2:-}"; shift 2 ;;
    -h|--help) usage; exit 0 ;;
    *) printf 'ERROR: unknown option: %s\n' "$1" >&2; exit 2 ;;
  esac
done
[ -n "$receipt" ] || { printf 'ERROR: --receipt is required\n' >&2; exit 2; }

python3 - "$receipt" "$repo" <<'PY'
import hashlib
import json
import re
import subprocess
import sys
import uuid
from datetime import datetime
from pathlib import Path, PurePosixPath

receipt_path = Path(sys.argv[1])
repo = Path(sys.argv[2]).resolve()
errors = []

def required_text(obj, key, label):
    value = obj.get(key) if isinstance(obj, dict) else None
    if not isinstance(value, str) or not value.strip():
        errors.append(f"{label}.{key} must be a non-empty string")
        return ""
    return value.strip()

def relative(value):
    path = PurePosixPath(value)
    return not path.is_absolute() and ".." not in path.parts

def normalized_bytes(path):
    raw = path.read_bytes()
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    try:
        text = raw.decode("utf-8", errors="strict")
    except UnicodeDecodeError as exc:
        raise ValueError(f"not strict UTF-8: {exc}") from exc
    if "\x00" in text:
        raise ValueError("binary NUL detected")
    return text.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")

def digest(path):
    return hashlib.sha256(normalized_bytes(path)).hexdigest()

def normalized_digest_bytes(raw):
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    try:
        text = raw.decode("utf-8", errors="strict")
    except UnicodeDecodeError as exc:
        raise ValueError(f"not strict UTF-8: {exc}") from exc
    if "\x00" in text:
        raise ValueError("binary NUL detected")
    normalized = text.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")
    return hashlib.sha256(normalized).hexdigest()

def check_digest(value, label):
    if not re.fullmatch(r"[0-9a-f]{64}", value):
        errors.append(f"{label} must be a lowercase SHA-256")

def has_secret(text):
    patterns = (
        r"-----BEGIN (?:RSA |EC |OPENSSH )?PRIVATE KEY-----",
        r"\bAKIA[0-9A-Z]{16}\b",
        r"\bgh[pousr]_[A-Za-z0-9]{20,}\b",
        r"\bgithub_pat_[A-Za-z0-9_]{20,}\b",
    )
    return any(re.search(pattern, text) for pattern in patterns)

try:
    receipt_raw = receipt_path.read_bytes()
    receipt_text = normalized_bytes(receipt_path).decode("utf-8")
    data = json.loads(receipt_raw.decode("utf-8-sig"))
except Exception as exc:
    print(f"ERROR: invalid receipt: {exc}", file=sys.stderr)
    raise SystemExit(2)
if not isinstance(data, dict):
    errors.append("receipt root must be an object")
    data = {}
if has_secret(receipt_text):
    errors.append("receipt contains a credential or private key pattern")

schema_version = data.get("schemaVersion")
if schema_version not in ("1.0", "1.1"):
    errors.append("schemaVersion must be 1.0 or 1.1")
receipt_id = required_text(data, "receiptId", "receipt")
try:
    if uuid.UUID(receipt_id).int == 0:
        errors.append("receiptId must not be the starter UUID")
except ValueError:
    errors.append("receiptId must be a UUID")

generator = data.get("generator")
if not isinstance(generator, dict):
    errors.append("generator must be an object")
    generator = {}
if required_text(generator, "preset", "generator") != "intake-authoring-governance":
    errors.append("generator.preset is invalid")
generator_version = required_text(generator, "version", "generator")
expected_generator = {"1.0": "0.1.0", "1.1": "0.1.1"}.get(schema_version)
if expected_generator and generator_version != expected_generator:
    errors.append(f"generator.version must be {expected_generator} for schema {schema_version}")

created_at = required_text(data, "createdAt", "receipt")
try:
    if not created_at.endswith("Z"):
        raise ValueError()
    datetime.fromisoformat(created_at[:-1] + "+00:00")
except ValueError:
    errors.append("createdAt must be an ISO-8601 UTC timestamp")

status = required_text(data, "status", "receipt")
if status not in ("ReadyForReview", "NeedsClarification"):
    errors.append("status is invalid")

target = data.get("target")
if not isinstance(target, dict):
    errors.append("target must be an object")
    target = {}
target_path_text = required_text(target, "path", "target")
target_hash = required_text(target, "normalizedSha256", "target")
check_digest(target_hash, "target.normalizedSha256")
if target_path_text and not relative(target_path_text):
    errors.append("target.path must be repository-relative")
target_path = repo / target_path_text
target_text = ""
if target_path_text and not target_path.is_file():
    errors.append(f"target missing: {target_path_text}")
elif target_path_text:
    try:
        actual = digest(target_path)
        target_text = normalized_bytes(target_path).decode("utf-8")
    except ValueError as exc:
        errors.append(f"target {target_path_text}: {exc}")
    else:
        if actual != target_hash:
            errors.append(f"target hash drift: {target_path_text}")
        if has_secret(target_text):
            errors.append("target contains a credential or private key pattern")

sources = data.get("sources")
if not isinstance(sources, list) or not sources:
    errors.append("sources must be a non-empty array")
    sources = []
blocked_extensions = {
    ".7z", ".doc", ".docx", ".gif", ".gz", ".jpeg", ".jpg", ".pdf",
    ".png", ".tar", ".webp", ".xls", ".xlsx", ".zip"
}
for index, source in enumerate(sources):
    label = f"sources[{index}]"
    if not isinstance(source, dict):
        errors.append(f"{label} must be an object")
        continue
    if source.get("order") != index + 1:
        errors.append(f"{label}.order must be {index + 1}")
    kind = required_text(source, "kind", label)
    if kind not in ("Inline", "Pasted", "File"):
        errors.append(f"{label}.kind is invalid")
    required_text(source, "label", label)
    location = required_text(source, "location", label)
    if location not in ("Repository", "SnapshotOnly", "ExternalSnapshot"):
        errors.append(f"{label}.location is invalid")
    path_text = required_text(source, "path", label)
    source_hash = required_text(source, "normalizedSha256", label)
    check_digest(source_hash, f"{label}.normalizedSha256")
    required_text(source, "gitBlob", label)
    if location == "Repository":
        if kind != "File":
            errors.append(f"{label}: Repository location requires File kind")
        if path_text == "N/A" or not relative(path_text):
            errors.append(f"{label}.path must be repository-relative")
            continue
        if path_text == target_path_text:
            errors.append(f"{label}.path cannot be the generated target")
        source_path = repo / path_text
        if source_path.suffix.lower() in blocked_extensions:
            errors.append(f"{label}.path uses a known binary/document extension")
        if not source_path.is_file():
            errors.append(f"source missing: {path_text}")
        else:
            try:
                actual = digest(source_path)
                source_text = normalized_bytes(source_path).decode("utf-8")
            except ValueError as exc:
                errors.append(f"source {path_text}: {exc}")
            else:
                if actual != source_hash:
                    errors.append(f"source hash drift: {path_text}")
                if has_secret(source_text):
                    errors.append(f"source contains a credential or private key pattern: {path_text}")
    else:
        if path_text != "N/A":
            errors.append(f"{label}.path must be N/A for snapshot sources")
        if kind == "File" and location != "ExternalSnapshot":
            errors.append(f"{label}: external File must use ExternalSnapshot")
        if kind in ("Inline", "Pasted") and location != "SnapshotOnly":
            errors.append(f"{label}: inline or pasted source must use SnapshotOnly")

required_text(data, "profile", "receipt")
required_text(data, "languagePolicy", "receipt")

decisions = data.get("decisions")
if not isinstance(decisions, list):
    errors.append("decisions must be an array")
    decisions = []
decision_ids = set()
open_from_decisions = set()
for index, decision in enumerate(decisions):
    label = f"decisions[{index}]"
    decision_id = required_text(decision, "id", label)
    if not re.fullmatch(r"IAD[0-9]{3,}", decision_id) or decision_id in decision_ids:
        errors.append(f"{label}.id must be a unique IAD### identifier")
    decision_ids.add(decision_id)
    decision_status = required_text(decision, "status", label)
    if decision_status not in ("Answered", "Open"):
        errors.append(f"{label}.status is invalid")
    required_text(decision, "question", label)
    required_text(decision, "evidence", label)
    answer = decision.get("answer") if isinstance(decision, dict) else None
    if decision_status == "Answered" and (not isinstance(answer, str) or not answer.strip()):
        errors.append(f"{label}.answer is required for Answered")
    if decision_status == "Open":
        open_from_decisions.add(decision_id)

open_ids = data.get("openDecisionIds")
if not isinstance(open_ids, list) or any(not isinstance(item, str) for item in open_ids):
    errors.append("openDecisionIds must be a string array")
    open_ids = []
if len(open_ids) != len(set(open_ids)):
    errors.append("openDecisionIds must be unique")
if set(open_ids) != open_from_decisions:
    errors.append("openDecisionIds must match Open decisions exactly")

question_count = data.get("questionCount")
if not isinstance(question_count, int) or isinstance(question_count, bool) or not 0 <= question_count <= 5:
    errors.append("questionCount must be an integer from 0 through 5")

surface = data.get("agentSurface")
if not isinstance(surface, dict):
    errors.append("agentSurface must be an object")
    surface = {}
if required_text(surface, "specifyCanonicalId", "agentSurface") != "speckit.specify":
    errors.append("agentSurface.specifyCanonicalId is invalid")
if required_text(surface, "autonomousCanonicalId", "agentSurface") != "speckit.autonomous":
    errors.append("agentSurface.autonomousCanonicalId is invalid")
specify_invocation = required_text(surface, "specifyInvocation", "agentSurface")
autonomous_invocation = required_text(surface, "autonomousInvocation", "agentSurface")
if "\n" in specify_invocation or "\r" in specify_invocation:
    errors.append("specifyInvocation must be one line")
if "\n" in autonomous_invocation or "\r" in autonomous_invocation:
    errors.append("autonomousInvocation must be one line")

authority = required_text(data, "deliveryAuthority", "receipt")
if authority not in ("LocalImplementation", "PublishPR", "MergeAndSync"):
    errors.append("deliveryAuthority is invalid")
authority_evidence = required_text(data, "authorityEvidence", "receipt")
if authority in ("PublishPR", "MergeAndSync") and authority_evidence.lower().startswith("default"):
    errors.append("remote delivery authority needs explicit evidence")

prompt_state = required_text(data, "promptState", "receipt")
if prompt_state not in ("Enabled", "Blocked"):
    errors.append("promptState is invalid")
if status == "ReadyForReview" and open_ids:
    errors.append("ReadyForReview cannot contain open decisions")
if status == "ReadyForReview" and prompt_state != "Enabled":
    errors.append("ReadyForReview requires Enabled prompts")
if status == "NeedsClarification" and not open_ids:
    errors.append("NeedsClarification requires open decisions")
if status == "NeedsClarification" and prompt_state != "Blocked":
    errors.append("NeedsClarification requires Blocked prompts")

supersedes = data.get("supersedes")
if not isinstance(supersedes, dict):
    errors.append("supersedes must be an object")
    supersedes = {}
old_receipt = required_text(supersedes, "receiptPath", "supersedes")
old_target_hash = required_text(supersedes, "targetNormalizedSha256", "supersedes")
update_authorized = data.get("updateAuthorized")
if not isinstance(update_authorized, bool):
    errors.append("updateAuthorized must be boolean")
if (old_receipt == "N/A") != (old_target_hash == "N/A"):
    errors.append("supersedes fields must both be N/A or both be populated")

if schema_version == "1.0":
    if old_receipt == "N/A":
        if update_authorized is True:
            errors.append("new intake cannot claim updateAuthorized")
    else:
        if not update_authorized:
            errors.append("supersession requires updateAuthorized")
        if not relative(old_receipt):
            errors.append("supersedes.receiptPath must be repository-relative")
        elif not (repo / old_receipt).is_file():
            errors.append(f"superseded receipt missing: {old_receipt}")
        check_digest(old_target_hash, "supersedes.targetNormalizedSha256")
else:
    provenance_mode = required_text(data, "provenanceMode", "receipt")
    if provenance_mode not in ("New", "Supersession", "LegacyAdoption"):
        errors.append("provenanceMode is invalid")
    update_evidence = required_text(data, "updateAuthorityEvidence", "receipt")
    legacy = data.get("legacyAdoption")
    if not isinstance(legacy, dict):
        errors.append("legacyAdoption must be an object")
        legacy = {}
    legacy_type = required_text(legacy, "evidenceType", "legacyAdoption")
    legacy_hash = required_text(
        legacy, "priorTargetNormalizedSha256", "legacyAdoption"
    )
    legacy_blob = required_text(legacy, "priorGitBlob", "legacyAdoption")

    if provenance_mode == "New":
        if update_authorized is True:
            errors.append("new intake cannot claim updateAuthorized")
        if old_receipt != "N/A" or old_target_hash != "N/A":
            errors.append("New provenance cannot supersede an intake")
        if (legacy_type, legacy_hash, legacy_blob) != ("N/A", "N/A", "N/A"):
            errors.append("New provenance cannot contain legacy-adoption evidence")
        if update_evidence != "N/A":
            errors.append("New provenance requires updateAuthorityEvidence N/A")
    elif provenance_mode == "Supersession":
        if not update_authorized:
            errors.append("supersession requires updateAuthorized")
        if update_evidence == "N/A":
            errors.append("supersession requires updateAuthorityEvidence")
        if old_receipt == "N/A":
            errors.append("Supersession requires a prior receipt")
        elif not relative(old_receipt):
            errors.append("supersedes.receiptPath must be repository-relative")
        elif not (repo / old_receipt).is_file():
            errors.append(f"superseded receipt missing: {old_receipt}")
        check_digest(old_target_hash, "supersedes.targetNormalizedSha256")
        if (legacy_type, legacy_hash, legacy_blob) != ("N/A", "N/A", "N/A"):
            errors.append("Supersession cannot contain legacy-adoption evidence")
    elif provenance_mode == "LegacyAdoption":
        if not update_authorized:
            errors.append("legacy adoption requires updateAuthorized")
        if update_evidence == "N/A":
            errors.append("legacy adoption requires updateAuthorityEvidence")
        if old_receipt != "N/A" or old_target_hash != "N/A":
            errors.append("LegacyAdoption cannot invent a superseded receipt")
        if legacy_type not in ("GitBlob", "SnapshotOnly"):
            errors.append("legacyAdoption.evidenceType is invalid")
        check_digest(
            legacy_hash, "legacyAdoption.priorTargetNormalizedSha256"
        )
        if legacy_hash not in {
            source.get("normalizedSha256")
            for source in sources
            if isinstance(source, dict)
        }:
            errors.append("legacy target hash must occur in the source inventory")
        if legacy_type == "SnapshotOnly":
            if legacy_blob != "N/A":
                errors.append("SnapshotOnly legacy adoption requires priorGitBlob N/A")
        elif legacy_type == "GitBlob":
            if not re.fullmatch(r"(?:[0-9a-f]{40}|[0-9a-f]{64})", legacy_blob):
                errors.append("legacyAdoption.priorGitBlob must be a Git object id")
            else:
                try:
                    completed = subprocess.run(
                        ["git", "-C", str(repo), "cat-file", "blob", legacy_blob],
                        check=True,
                        stdout=subprocess.PIPE,
                        stderr=subprocess.PIPE,
                    )
                    blob_hash = normalized_digest_bytes(completed.stdout)
                    if blob_hash != legacy_hash:
                        errors.append("legacy Git blob does not match prior target hash")
                except (OSError, subprocess.CalledProcessError, ValueError) as exc:
                    errors.append(f"legacy Git blob cannot be verified: {exc}")

next_action = required_text(data, "nextAction", "receipt")
if status == "ReadyForReview":
    if "speckit-intake-review" not in next_action or target_path_text not in next_action:
        errors.append("ReadyForReview nextAction must name Intake Review and the target")
else:
    if "speckit-intake-review" in next_action:
        errors.append("NeedsClarification cannot hand off to Intake Review")

if target_text:
    markers = (
        "<!-- intake-authoring:begin -->",
        "<!-- intake-authoring:prompts -->",
        "<!-- spec-kit-command-id: speckit.specify -->",
        "<!-- spec-kit-command-id: speckit.autonomous -->",
        "<!-- intake-authoring:end -->",
    )
    for marker in markers:
        if target_text.count(marker) != 1:
            errors.append(f"target must contain marker exactly once: {marker}")
    if status == "ReadyForReview":
        if "BLOCKED - DO NOT RUN" in target_text:
            errors.append("enabled target cannot contain BLOCKED - DO NOT RUN")
        if not re.search(rf"(?m)^{re.escape(specify_invocation)}(?:\s|$)", target_text):
            errors.append("enabled target lacks rendered Specify invocation")
        if not re.search(rf"(?m)^{re.escape(autonomous_invocation)}(?:\s|$)", target_text):
            errors.append("enabled target lacks rendered Autonomous invocation")
        if target_path_text not in target_text:
            errors.append("enabled prompts must name the exact target path")
        if authority not in target_text:
            errors.append("enabled Autonomous prompt must name deliveryAuthority")
    else:
        if "BLOCKED - DO NOT RUN" not in target_text:
            errors.append("blocked target must contain BLOCKED - DO NOT RUN")
        if re.search(rf"(?m)^{re.escape(specify_invocation)}(?:\s|$)", target_text):
            errors.append("blocked target contains executable Specify invocation")
        if re.search(rf"(?m)^{re.escape(autonomous_invocation)}(?:\s|$)", target_text):
            errors.append("blocked target contains executable Autonomous invocation")
        for decision_id in open_ids:
            if decision_id not in target_text:
                errors.append(f"blocked target does not name open decision: {decision_id}")

if errors:
    for error in errors:
        print(f"ERROR: {error}", file=sys.stderr)
    raise SystemExit(2)
print(
    f"PASS: intake authoring {receipt_id} is current "
    f"({status}, {len(sources)} sources, {target_path_text})"
)
PY
