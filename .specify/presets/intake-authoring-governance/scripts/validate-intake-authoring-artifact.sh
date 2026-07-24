#!/usr/bin/env bash
# Validate an intake-authoring series, operation, or tombstone artifact.
set -euo pipefail

usage() {
  printf '%s\n' 'Usage: validate-intake-authoring-artifact.sh --artifact FILE [--repo PATH]'
}

artifact=''
repo='.'
while [ "$#" -gt 0 ]; do
  case "$1" in
    --artifact) artifact="${2:-}"; shift 2 ;;
    --repo) repo="${2:-}"; shift 2 ;;
    -h|--help) usage; exit 0 ;;
    *) printf 'ERROR: unknown option: %s\n' "$1" >&2; exit 2 ;;
  esac
done
[ -n "$artifact" ] || { printf 'ERROR: --artifact is required\n' >&2; exit 2; }

python3 - "$artifact" "$repo" <<'PY'
import hashlib
import json
import re
import sys
import uuid
from datetime import datetime
from pathlib import Path, PurePosixPath

artifact_path = Path(sys.argv[1])
repo = Path(sys.argv[2]).resolve()
errors = []

def text(obj, key, label):
    value = obj.get(key) if isinstance(obj, dict) else None
    if not isinstance(value, str) or not value.strip():
        errors.append(f"{label}.{key} must be a non-empty string")
        return ""
    return value.strip()

def relative(value):
    path = PurePosixPath(value)
    return not path.is_absolute() and ".." not in path.parts

def normalized(path):
    raw = path.read_bytes()
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    value = raw.decode("utf-8", errors="strict")
    if "\x00" in value:
        raise ValueError("binary NUL detected")
    return value.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")

def digest(path):
    return hashlib.sha256(normalized(path)).hexdigest()

def check_hash(value, label):
    if not re.fullmatch(r"[0-9a-f]{64}", value):
        errors.append(f"{label} must be a lowercase SHA-256")

def check_uuid(value, label):
    try:
        if uuid.UUID(value).int == 0:
            raise ValueError()
    except (ValueError, AttributeError):
        errors.append(f"{label} must be a non-zero UUID")

def check_time(value, label):
    try:
        if not value.endswith("Z"):
            raise ValueError()
        datetime.fromisoformat(value[:-1] + "+00:00")
    except ValueError:
        errors.append(f"{label} must be an ISO-8601 UTC timestamp")

def check_relative_file(value, label, expected_hash=None, must_be_absent=False):
    if not relative(value):
        errors.append(f"{label} must be repository-relative")
        return
    path = repo / value
    if must_be_absent:
        if path.exists():
            errors.append(f"{label} must no longer exist")
        return
    if not path.is_file():
        errors.append(f"{label} is missing: {value}")
        return
    if expected_hash is not None:
        check_hash(expected_hash, f"{label} hash")
        try:
            actual = digest(path)
        except (UnicodeDecodeError, ValueError) as exc:
            errors.append(f"{label} is not strict UTF-8 text: {exc}")
        else:
            if actual != expected_hash:
                errors.append(f"{label} hash drift")

try:
    data = json.loads(artifact_path.read_text(encoding="utf-8-sig"))
except Exception as exc:
    print(f"ERROR: invalid artifact: {exc}", file=sys.stderr)
    raise SystemExit(2)
if not isinstance(data, dict):
    data = {}
    errors.append("artifact root must be an object")
if data.get("schemaVersion") != "1.0":
    errors.append("schemaVersion must be 1.0")
document_type = text(data, "documentType", "artifact")

if document_type == "IntakeSeries":
    series_id = text(data, "seriesId", "series")
    check_uuid(series_id, "seriesId")
    version = data.get("version")
    if not isinstance(version, int) or isinstance(version, bool) or version < 1:
        errors.append("version must be a positive integer")
    status = text(data, "status", "series")
    if status not in ("Proposed", "Approved", "ReadyForReview", "NeedsClarification", "Superseded"):
        errors.append("series.status is invalid")
    check_uuid(text(data, "operationId", "series"), "operationId")
    proposal_hash = text(data, "proposalNormalizedSha256", "series")
    check_hash(proposal_hash, "proposalNormalizedSha256")
    approval = data.get("approval")
    if not isinstance(approval, dict):
        errors.append("approval must be an object")
        approval = {}
    approved = approval.get("approved")
    if not isinstance(approved, bool):
        errors.append("approval.approved must be boolean")
    if approved:
        if text(approval, "approvedBy", "approval") == "N/A":
            errors.append("approved series requires approvedBy")
        approved_at = text(approval, "approvedAt", "approval")
        if approved_at == "N/A":
            errors.append("approved series requires approvedAt")
        else:
            check_time(approved_at, "approval.approvedAt")
        if text(approval, "evidence", "approval") == "N/A":
            errors.append("approved series requires approval evidence")
    ordered = data.get("orderedIntakeIds")
    members = data.get("members")
    roots = data.get("roots")
    edges = data.get("edges")
    if not isinstance(ordered, list) or not ordered:
        errors.append("orderedIntakeIds must be a non-empty array")
        ordered = []
    if not isinstance(members, list) or not members:
        errors.append("members must be a non-empty array")
        members = []
    if not isinstance(roots, list) or not roots:
        errors.append("roots must be a non-empty array")
        roots = []
    if not isinstance(edges, list):
        errors.append("edges must be an array")
        edges = []
    member_ids = []
    orders = []
    for index, member in enumerate(members):
        label = f"members[{index}]"
        if not isinstance(member, dict):
            errors.append(f"{label} must be an object")
            continue
        intake_id = text(member, "intakeId", label)
        check_uuid(intake_id, f"{label}.intakeId")
        member_ids.append(intake_id)
        if intake_id in member_ids[:-1]:
            errors.append(f"{label}.intakeId must be unique")
        path = text(member, "path", label)
        receipt = text(member, "receiptPath", label)
        member_hash = text(member, "normalizedSha256", label)
        check_relative_file(path, f"{label}.path", member_hash)
        check_relative_file(receipt, f"{label}.receiptPath")
        text(member, "title", label)
        text(member, "role", label)
        order = member.get("order")
        if not isinstance(order, int) or isinstance(order, bool) or order < 1:
            errors.append(f"{label}.order must be a positive integer")
        orders.append(order)
        predecessors = member.get("supersedesIntakeIds")
        if not isinstance(predecessors, list):
            errors.append(f"{label}.supersedesIntakeIds must be an array")
        else:
            for predecessor in predecessors:
                check_uuid(predecessor, f"{label}.supersedesIntakeIds[]")
    if len(ordered) != len(set(ordered)) or ordered != member_ids:
        errors.append("orderedIntakeIds must contain every member exactly once in member order")
    if orders != list(range(1, len(members) + 1)):
        errors.append("member order values must be contiguous from one")
    position = {value: index for index, value in enumerate(ordered)}
    incoming = {value: 0 for value in member_ids}
    adjacency = {value: [] for value in member_ids}
    pairs = set()
    for index, edge in enumerate(edges):
        label = f"edges[{index}]"
        if not isinstance(edge, dict):
            errors.append(f"{label} must be an object")
            continue
        source = text(edge, "from", label)
        target = text(edge, "to", label)
        text(edge, "kind", label)
        if source not in position or target not in position:
            errors.append(f"{label} references an unknown member")
            continue
        if source == target:
            errors.append(f"{label} must not be a self-edge")
        if (source, target) in pairs:
            errors.append(f"{label} duplicates an edge")
        pairs.add((source, target))
        if position[source] >= position[target]:
            errors.append(f"{label} contradicts member order")
        incoming[target] += 1
        adjacency[source].append(target)
    if len(roots) != len(set(roots)) or any(root not in position for root in roots):
        errors.append("roots must be unique known members")
    expected_roots = {value for value, count in incoming.items() if count == 0}
    if set(roots) != expected_roots:
        errors.append("roots must exactly match members without incoming edges")
    state = {}
    def visit(node):
        state[node] = 1
        for child in adjacency.get(node, []):
            if state.get(child) == 1:
                return True
            if state.get(child, 0) == 0 and visit(child):
                return True
        state[node] = 2
        return False
    if any(state.get(node, 0) == 0 and visit(node) for node in member_ids):
        errors.append("series graph must be acyclic")
    coverage = data.get("coverage")
    if not isinstance(coverage, list) or not coverage:
        errors.append("coverage must be a non-empty array")
    else:
        coverage_ids = set()
        for index, row in enumerate(coverage):
            label = f"coverage[{index}]"
            source_id = text(row, "sourceId", label)
            if source_id in coverage_ids:
                errors.append(f"{label}.sourceId must be unique")
            coverage_ids.add(source_id)
            text(row, "topic", label)
            references = row.get("intakeIds")
            if not isinstance(references, list) or not references or any(item not in position for item in references):
                errors.append(f"{label}.intakeIds must reference known members")
            if text(row, "disposition", label) not in ("Covered", "ExcludedWithRationale", "NeedsClarification"):
                errors.append(f"{label}.disposition is invalid")
    if status in ("Approved", "ReadyForReview") and not approved:
        errors.append("approved or ready series requires explicit approval")
    if status == "ReadyForReview" and any(
        row.get("disposition") == "NeedsClarification" for row in data.get("coverage", []) if isinstance(row, dict)
    ):
        errors.append("ReadyForReview cannot contain unresolved coverage")
    review_path = text(data, "reviewRequestPath", "series")
    if status == "ReadyForReview":
        check_relative_file(review_path, "reviewRequestPath")
    elif review_path != "N/A" and not relative(review_path):
        errors.append("reviewRequestPath must be repository-relative or N/A")
    text(data, "nextAction", "series")

elif document_type == "IntakeOperation":
    check_uuid(text(data, "operationId", "operation"), "operationId")
    operation_type = text(data, "type", "operation")
    if operation_type not in ("Create", "Update", "Delete", "CreateSeries", "UpdateSeries", "DeleteSeries"):
        errors.append("operation.type is invalid")
    status = text(data, "status", "operation")
    if status not in ("Proposed", "Approved", "Applying", "Completed", "Failed"):
        errors.append("operation.status is invalid")
    check_time(text(data, "createdAt", "operation"), "createdAt")
    text(data, "authorityEvidence", "operation")
    proposal_path = text(data, "proposalPath", "operation")
    proposal_hash = text(data, "proposalNormalizedSha256", "operation")
    check_relative_file(proposal_path, "proposalPath", proposal_hash)
    approval = data.get("approval")
    if not isinstance(approval, dict):
        errors.append("approval must be an object")
        approval = {}
    approved = approval.get("approved")
    if not isinstance(approved, bool):
        errors.append("approval.approved must be boolean")
    intended = data.get("intendedTargets")
    validated = data.get("validatedTargets")
    published = data.get("publishedTargets")
    for value, label in ((intended, "intendedTargets"), (validated, "validatedTargets"), (published, "publishedTargets")):
        if not isinstance(value, list) or len(value) != len(set(value)) or any(not isinstance(item, str) or not relative(item) for item in value):
            errors.append(f"{label} must be a unique repository-relative path array")
    if status == "Completed":
        if not approved:
            errors.append("Completed operation requires explicit approval")
        if intended != validated or intended != published:
            errors.append("Completed operation requires intended, validated, and published targets to match")
    text(data, "rollbackBoundary", "operation")
    failure = data.get("failure")
    if not isinstance(failure, dict):
        errors.append("failure must be an object")
    elif status == "Failed":
        if text(failure, "class", "failure") == "N/A" or text(failure, "message", "failure") == "N/A":
            errors.append("Failed operation requires failure class and message")
    text(data, "nextAction", "operation")

elif document_type == "IntakeTombstone":
    check_uuid(text(data, "tombstoneId", "tombstone"), "tombstoneId")
    check_uuid(text(data, "intakeId", "tombstone"), "intakeId")
    check_uuid(text(data, "operationId", "tombstone"), "operationId")
    check_time(text(data, "deletedAt", "tombstone"), "deletedAt")
    text(data, "reason", "tombstone")
    text(data, "deleteAuthorityEvidence", "tombstone")
    original = data.get("original")
    archive = data.get("archive")
    if not isinstance(original, dict):
        errors.append("original must be an object")
        original = {}
    if not isinstance(archive, dict):
        errors.append("archive must be an object")
        archive = {}
    original_target = text(original, "targetPath", "original")
    original_receipt = text(original, "receiptPath", "original")
    check_relative_file(original_target, "original.targetPath", must_be_absent=True)
    check_relative_file(original_receipt, "original.receiptPath", must_be_absent=True)
    original_target_hash = text(original, "targetNormalizedSha256", "original")
    original_receipt_hash = text(original, "receiptNormalizedSha256", "original")
    check_hash(original_target_hash, "original.targetNormalizedSha256")
    check_hash(original_receipt_hash, "original.receiptNormalizedSha256")
    archive_target = text(archive, "targetPath", "archive")
    archive_receipt = text(archive, "receiptPath", "archive")
    archive_target_hash = text(archive, "targetNormalizedSha256", "archive")
    archive_receipt_hash = text(archive, "receiptNormalizedSha256", "archive")
    check_relative_file(archive_target, "archive.targetPath", archive_target_hash)
    check_relative_file(archive_receipt, "archive.receiptPath", archive_receipt_hash)
    if archive_target_hash != original_target_hash or archive_receipt_hash != original_receipt_hash:
        errors.append("archive hashes must preserve the original target and receipt hashes")
    series = data.get("seriesImpact")
    if not isinstance(series, dict):
        errors.append("seriesImpact must be an object")
    else:
        series_id = text(series, "seriesId", "seriesImpact")
        migration_id = text(series, "migrationOperationId", "seriesImpact")
        if (series_id == "N/A") != (migration_id == "N/A"):
            errors.append("seriesImpact fields must both be N/A or UUIDs")
        if series_id != "N/A":
            check_uuid(series_id, "seriesImpact.seriesId")
            check_uuid(migration_id, "seriesImpact.migrationOperationId")
    text(data, "reactivationBoundary", "tombstone")
    text(data, "nextAction", "tombstone")
else:
    errors.append("documentType must be IntakeSeries, IntakeOperation, or IntakeTombstone")

if errors:
    for error in errors:
        print(f"ERROR: {error}", file=sys.stderr)
    raise SystemExit(2)
print(f"PASS: {document_type} artifact is valid ({artifact_path})")
PY
