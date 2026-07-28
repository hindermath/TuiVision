#!/usr/bin/env python3
"""Validate intake-series manifests and receipts without modifying files."""

from __future__ import annotations

import argparse
import hashlib
import json
import re
import sys
import uuid
from pathlib import Path, PurePosixPath

EDGE_TYPES = {
    "RequirementsGovernanceGate",
    "HardCompletionGate",
    "AssessmentBaseline",
    "AssuranceAuditBaseline",
    "PublishedPresetBaseline",
    "SandboxBaseline",
    "SecureDevelopmentBaseline",
    "DocumentationSurfaceBaseline",
    "CommentSurfaceBaseline",
    "FinalAuditInput",
    "PreferredSerialOrder",
    "SharedWriterSerialization",
}
BINDING_TYPES = EDGE_TYPES - {"PreferredSerialOrder", "SharedWriterSerialization"}
SERIES_STATES = {"Draft", "NeedsClarification", "Ready", "Active", "Idle", "Completed", "Deleted"}
TARGET_STATES = {"Pending", "Blocked", "Eligible", "Active", "Completed", "Withdrawn"}


class ValidationError(Exception):
    """Carries a stable cross-shell error class."""

    def __init__(self, code: str, message: str) -> None:
        super().__init__(message)
        self.code = code


def fail(code: str, message: str) -> None:
    raise ValidationError(code, message)


def normalized_bytes(path: Path) -> bytes:
    raw = path.read_bytes()
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    try:
        text = raw.decode("utf-8", errors="strict")
    except UnicodeDecodeError as exc:
        fail("ISG001", f"{path}: not strict UTF-8: {exc}")
    if "\x00" in text:
        fail("ISG001", f"{path}: binary NUL detected")
    return text.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")


def load_json(path: Path) -> dict:
    try:
        data = json.loads(normalized_bytes(path).decode("utf-8"))
    except (OSError, json.JSONDecodeError) as exc:
        fail("ISG001", f"invalid JSON: {path}: {exc}")
    if not isinstance(data, dict):
        fail("ISG001", f"JSON root must be an object: {path}")
    return data


def digest(path: Path) -> str:
    return hashlib.sha256(normalized_bytes(path)).hexdigest()


def text(obj: dict, key: str, code: str) -> str:
    value = obj.get(key)
    if not isinstance(value, str) or not value.strip():
        fail(code, f"{key} must be a non-empty string")
    return value.strip()


def relative_path(value: str) -> bool:
    candidate = PurePosixPath(value)
    return not candidate.is_absolute() and ".." not in candidate.parts and value != "."


def valid_uuid(value: str) -> bool:
    try:
        return uuid.UUID(value).int != 0
    except ValueError:
        return False


def validate_manifest(path: Path, repo: Path) -> tuple[dict, dict]:
    data = load_json(path)
    if data.get("schemaVersion") != "1.0" or data.get("documentType") != "IntakeSeriesManifest":
        fail("ISG001", "manifest schema or document type is invalid")
    series_id = text(data, "seriesId", "ISG002")
    if not valid_uuid(series_id):
        fail("ISG002", "seriesId must be a non-zero UUID")
    text(data, "title", "ISG002")
    text(data, "policy", "ISG002")
    status = text(data, "status", "ISG009")
    if status not in SERIES_STATES:
        fail("ISG009", f"unknown series status: {status}")

    targets = data.get("orderedTargets")
    if not isinstance(targets, list):
        fail("ISG003", "orderedTargets must be an array")
    roots = data.get("roots")
    dependencies = data.get("dependencies")
    if status == "Idle":
        if targets or roots != [] or dependencies != []:
            fail("ISG009", "Idle requires zero targets, roots, and dependencies")
        return data, {
            "seriesId": series_id,
            "status": status,
            "targets": 0,
            "roots": 0,
            "dependencies": 0,
            "eligible": [],
            "declaredEligible": [],
            "blockers": {},
        }
    if not targets:
        fail("ISG003", "orderedTargets must be a non-empty array")
    paths: list[str] = []
    target_states: dict[str, str] = {}
    for index, target in enumerate(targets):
        if not isinstance(target, dict):
            fail("ISG003", f"orderedTargets[{index}] must be an object")
        target_path = text(target, "path", "ISG003")
        if not relative_path(target_path) or target_path in paths:
            fail("ISG003", f"target path must be unique and repository-relative: {target_path}")
        role = text(target, "role", "ISG003")
        if role not in {"Primary", "OrderedMember"}:
            fail("ISG003", f"invalid target role: {role}")
        expected_hash = text(target, "normalizedSha256", "ISG004")
        if not re.fullmatch(r"[0-9a-f]{64}", expected_hash):
            fail("ISG004", f"invalid target hash: {target_path}")
        full_path = repo / target_path
        if not full_path.is_file() or digest(full_path) != expected_hash:
            fail("ISG004", f"target missing or hash drift: {target_path}")
        target_status = text(target, "status", "ISG009")
        if target_status not in TARGET_STATES:
            fail("ISG009", f"unknown target status: {target_status}")
        paths.append(target_path)
        target_states[target_path] = target_status

    if not isinstance(roots, list) or len(roots) != len(set(roots)):
        fail("ISG008", "roots must be a unique string array")
    if any(not isinstance(root, str) or root not in paths for root in roots):
        fail("ISG008", "roots contain an unknown target")

    if not isinstance(dependencies, list):
        fail("ISG005", "dependencies must be an array")
    order = {target_path: index for index, target_path in enumerate(paths)}
    pairs: set[tuple[str, str]] = set()
    binding_indegree = {target_path: 0 for target_path in paths}
    all_indegree = {target_path: 0 for target_path in paths}
    adjacency = {target_path: [] for target_path in paths}
    for index, edge in enumerate(dependencies):
        if not isinstance(edge, dict):
            fail("ISG005", f"dependencies[{index}] must be an object")
        source = text(edge, "from", "ISG005")
        destination = text(edge, "to", "ISG005")
        kind = text(edge, "kind", "ISG006")
        binding = edge.get("binding")
        if source not in order or destination not in order or source == destination:
            fail("ISG005", f"invalid dependency reference: {source} -> {destination}")
        if (source, destination) in pairs:
            fail("ISG006", f"duplicate dependency pair: {source} -> {destination}")
        if kind not in EDGE_TYPES:
            fail("ISG006", f"unknown dependency kind: {kind}")
        expected_binding = kind in BINDING_TYPES
        if binding is not expected_binding:
            fail("ISG006", f"binding flag disagrees with dependency kind: {kind}")
        if order[source] >= order[destination]:
            fail("ISG007", f"dependency contradicts declared order: {source} -> {destination}")
        pairs.add((source, destination))
        adjacency[source].append(destination)
        all_indegree[destination] += 1
        if binding:
            binding_indegree[destination] += 1

    actual_roots = {target_path for target_path, degree in all_indegree.items() if degree == 0}
    if set(roots) != actual_roots or not roots:
        fail("ISG008", "roots must equal the zero-indegree target set")

    remaining = dict(all_indegree)
    queue = [target_path for target_path, degree in remaining.items() if degree == 0]
    visited = 0
    while queue:
        node = queue.pop()
        visited += 1
        for successor in adjacency[node]:
            remaining[successor] -= 1
            if remaining[successor] == 0:
                queue.append(successor)
    if visited != len(paths):
        fail("ISG007", "dependencies must be acyclic")

    eligible: list[str] = []
    blockers: dict[str, list[str]] = {}
    for target_path in paths:
        if target_states[target_path] not in {"Pending", "Blocked", "Eligible"}:
            continue
        required = [
            source
            for source, destination in pairs
            if destination == target_path
            and next(
                edge["binding"]
                for edge in dependencies
                if edge["from"] == source and edge["to"] == destination
            )
        ]
        incomplete = [source for source in required if target_states[source] != "Completed"]
        if incomplete:
            blockers[target_path] = incomplete
        else:
            eligible.append(target_path)

    declared_eligible = [
        target_path for target_path, target_status in target_states.items()
        if target_status == "Eligible"
    ]
    if len(declared_eligible) > 1:
        fail("ISG009", "at most one target may declare Eligible")
    if declared_eligible and declared_eligible[0] not in eligible:
        fail("ISG009", "declared Eligible target still has a binding blocker")

    summary = {
        "seriesId": series_id,
        "status": status,
        "targets": len(paths),
        "roots": len(roots),
        "dependencies": len(dependencies),
        "eligible": eligible,
        "declaredEligible": declared_eligible,
        "blockers": blockers,
    }
    return data, summary


def validate_receipt(path: Path, repo: Path) -> dict:
    data = load_json(path)
    if data.get("schemaVersion") != "1.0" or data.get("documentType") != "IntakeSeriesReceipt":
        fail("ISR001", "receipt schema or document type is invalid")
    receipt_id = text(data, "receiptId", "ISR002")
    series_id = text(data, "seriesId", "ISR002")
    if not valid_uuid(receipt_id) or not valid_uuid(series_id):
        fail("ISR002", "receiptId and seriesId must be non-zero UUIDs")
    operation = data.get("operation")
    if not isinstance(operation, dict):
        fail("ISR003", "operation must be an object")
    operation_id = text(operation, "operationId", "ISR003")
    if not valid_uuid(operation_id):
        fail("ISR003", "operationId must be a non-zero UUID")
    operation_type = text(operation, "type", "ISR003")
    if operation_type not in {"Create", "Update", "Delete", "LegacyAdoption"}:
        fail("ISR003", f"unknown operation type: {operation_type}")
    text(operation, "authorityEvidence", "ISR003")
    manifest = data.get("manifest")
    if not isinstance(manifest, dict):
        fail("ISR004", "manifest evidence must be an object")
    manifest_path = text(manifest, "path", "ISR004")
    expected_hash = text(manifest, "normalizedSha256", "ISR004")
    if not relative_path(manifest_path) or not re.fullmatch(r"[0-9a-f]{64}", expected_hash):
        fail("ISR004", "manifest evidence is invalid")
    full_manifest = repo / manifest_path
    if not full_manifest.is_file() or digest(full_manifest) != expected_hash:
        fail("ISR004", "manifest missing or hash drift")
    manifest_data, summary = validate_manifest(full_manifest, repo)
    if manifest_data.get("seriesId") != series_id:
        fail("ISR004", "receipt seriesId differs from manifest")
    status = text(data, "status", "ISR005")
    if status not in {"Ready", "NeedsClarification", "Deleted"}:
        fail("ISR005", f"unknown receipt status: {status}")
    supersedes = data.get("supersedes")
    if not isinstance(supersedes, dict):
        fail("ISR006", "supersedes must be an object")
    if operation_type in {"Update", "Delete"}:
        for field in ("receiptPath", "receiptNormalizedSha256", "manifestArchivePath", "manifestArchiveSha256"):
            value = text(supersedes, field, "ISR006")
            if field.endswith("Path") and not relative_path(value):
                fail("ISR006", f"{field} must be repository-relative")
            if field.endswith("Sha256") and not re.fullmatch(r"[0-9a-f]{64}", value):
                fail("ISR006", f"{field} must be a SHA-256 value")
        for field in ("receiptPath", "manifestArchivePath"):
            evidence_path = repo / supersedes[field]
            hash_field = "receiptNormalizedSha256" if field == "receiptPath" else "manifestArchiveSha256"
            if not evidence_path.is_file() or digest(evidence_path) != supersedes[hash_field]:
                fail("ISR006", f"archive evidence missing or drifted: {field}")
    tombstone = data.get("tombstone")
    if operation_type == "Delete":
        if not isinstance(tombstone, dict):
            fail("ISR007", "Delete requires tombstone evidence")
        tombstone_path = text(tombstone, "path", "ISR007")
        tombstone_hash = text(tombstone, "normalizedSha256", "ISR007")
        if not relative_path(tombstone_path) or not re.fullmatch(r"[0-9a-f]{64}", tombstone_hash):
            fail("ISR007", "tombstone evidence is invalid")
        full_tombstone = repo / tombstone_path
        if not full_tombstone.is_file() or digest(full_tombstone) != tombstone_hash:
            fail("ISR007", "tombstone missing or hash drift")
    return {"receiptId": receipt_id, **summary}


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--kind", choices=("manifest", "receipt"), required=True)
    parser.add_argument("--file", required=True)
    parser.add_argument("--repo", default=".")
    parser.add_argument("--json", action="store_true")
    args = parser.parse_args()
    repo = Path(args.repo).resolve()
    path = Path(args.file)
    if not path.is_absolute():
        path = Path.cwd() / path
    try:
        if args.kind == "manifest":
            _, summary = validate_manifest(path, repo)
        else:
            summary = validate_receipt(path, repo)
    except ValidationError as exc:
        print(f"ERROR {exc.code}: {exc}", file=sys.stderr)
        return 2
    if args.json:
        print(json.dumps(summary, sort_keys=True))
    else:
        print(
            "PASS: "
            f"{summary['seriesId']} "
            f"({summary['targets']} targets, {summary['roots']} roots, "
            f"{summary['dependencies']} dependencies)"
        )
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
