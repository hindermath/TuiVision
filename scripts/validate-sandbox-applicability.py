#!/usr/bin/env python3
"""Validate TuiVision sandbox applicability evidence without modifying files."""

from __future__ import annotations

import argparse
import json
from pathlib import Path, PurePosixPath
import re
import sys
from typing import Any


CONTROL_IDS = {f"CL-12-{number:02d}" for number in range(1, 13)}
REQUIRED_MOUNTS = {
    "TuiVisionCheckout",
    "AgentStateVolume",
    "BuildCacheVolume",
    "AuditMetadataDirectory",
}
REQUIRED_EXECUTIONS = {
    "Build",
    "Test",
    "Format",
    "DocFX",
    "A11Y",
    "DependencySBOM",
    "SecretScan",
    "AgentParity",
}
APPLICABILITY = {"Applicable", "N/A", "Open"}
IMPLEMENTATION = {"Fulfilled", "Partly Fulfilled", "Not Fulfilled", "Not Assessed"}
RECOMMENDATIONS = {"ApprovedWithBoundaries", "ConditionallyUsable", "NotApproved", "NeedsDecision"}
ACCESS = {"ReadOnly", "ReadWrite", "NamedVolume", "NotMounted"}
LOCATIONS = {"Sandbox", "LocalHost", "CI", "NotPermitted", "Open"}
PROOF_LEVELS = {"StaticVerified", "PracticallyVerified", "PlatformVerified", "NotVerified"}
REQUIRED_CONTROL_FIELDS = {
    "rationale",
    "evidence",
    "owner",
    "reviewer",
    "reviewDate",
    "residualRisk",
    "followUp",
    "reevaluationTrigger",
}
PLACEHOLDER = re.compile(r"(?:\bTODO\b|\bTBD\b|_TODO_|replace-with|\[path\])", re.IGNORECASE)
SECRET = re.compile(
    r"(?:\bghp_[A-Za-z0-9]{20,}|\bsk-[A-Za-z0-9]{10,}|\bAKIA[0-9A-Z]{16}\b|\bAIza[0-9A-Za-z_-]{20,}|-----BEGIN (?:RSA |EC |OPENSSH )?PRIVATE KEY-----)"
)


def _text(value: Any, label: str, errors: list[str], *, placeholders: bool = True) -> str:
    if not isinstance(value, str) or not value.strip():
        errors.append(f"{label} must be a non-empty string")
        return ""
    result = value.strip()
    if placeholders and PLACEHOLDER.search(result):
        errors.append(f"{label} contains a placeholder")
    if SECRET.search(result):
        errors.append(f"{label} contains secret-like material")
    return result


def _unique(items: Any, key: str, label: str, errors: list[str]) -> dict[str, dict[str, Any]]:
    if not isinstance(items, list):
        errors.append(f"{label} must be an array")
        return {}
    result: dict[str, dict[str, Any]] = {}
    for index, item in enumerate(items):
        if not isinstance(item, dict):
            errors.append(f"{label}[{index}] must be an object")
            continue
        value = _text(item.get(key), f"{label}[{index}].{key}", errors)
        if value in result:
            errors.append(f"{label} contains duplicate {key} {value}")
        elif value:
            result[value] = item
    return result


def validate(data: Any) -> list[str]:
    errors: list[str] = []
    if not isinstance(data, dict):
        return ["assessment root must be an object"]

    if _text(data.get("schemaVersion"), "schemaVersion", errors) != "1.0":
        errors.append("schemaVersion must be 1.0")
    _text(data.get("project"), "project", errors)
    _text(data.get("reviewDate"), "reviewDate", errors)
    recommendation = _text(data.get("recommendation"), "recommendation", errors)
    if recommendation and recommendation not in RECOMMENDATIONS:
        errors.append(f"recommendation is invalid: {recommendation}")
    for field in ("nextSafeAction", "owner", "reviewer", "residualRisk"):
        _text(data.get(field), field, errors)

    reference = data.get("sandboxReference")
    if not isinstance(reference, dict):
        errors.append("sandboxReference must be an object")
        reference = {}
    for field in ("repository", "defaultBranch"):
        _text(reference.get(field), f"sandboxReference.{field}", errors)
    commit = _text(reference.get("commit"), "sandboxReference.commit", errors)
    if commit and not re.fullmatch(r"[0-9a-f]{40}", commit):
        errors.append("sandboxReference.commit must be a 40-character lowercase Git hash")
    hashes = _unique(reference.get("sourceHashes"), "path", "sandboxReference.sourceHashes", errors)
    if not hashes:
        errors.append("sandboxReference.sourceHashes must not be empty")
    for path, item in hashes.items():
        pure_path = PurePosixPath(path)
        if pure_path.is_absolute() or ".." in pure_path.parts:
            errors.append(f"source hash path must be relative: {path}")
        digest = _text(item.get("sha256"), f"source hash {path}.sha256", errors)
        if digest and not re.fullmatch(r"[0-9a-f]{64}", digest):
            errors.append(f"source hash {path}.sha256 is invalid")

    controls = _unique(data.get("controls"), "controlId", "controls", errors)
    if set(controls) != CONTROL_IDS:
        missing = sorted(CONTROL_IDS - set(controls))
        unexpected = sorted(set(controls) - CONTROL_IDS)
        errors.append(f"controls must contain exactly CL-12-01 through CL-12-12; missing={missing}, unexpected={unexpected}")
    for control_id, control in controls.items():
        applicability = _text(control.get("applicability"), f"{control_id}.applicability", errors)
        status = _text(control.get("implementationStatus"), f"{control_id}.implementationStatus", errors)
        if applicability and applicability not in APPLICABILITY:
            errors.append(f"{control_id}.applicability is invalid")
        if status and status not in IMPLEMENTATION:
            errors.append(f"{control_id}.implementationStatus is invalid")
        for field in REQUIRED_CONTROL_FIELDS:
            _text(control.get(field), f"{control_id}.{field}", errors)
        if applicability == "Open" and status == "Fulfilled":
            errors.append(f"{control_id}: Open cannot be Fulfilled")
        if applicability == "N/A" and status != "Not Assessed":
            errors.append(f"{control_id}: N/A must use Not Assessed")

    mounts = _unique(data.get("mounts"), "mountRole", "mounts", errors)
    missing_mounts = sorted(REQUIRED_MOUNTS - set(mounts))
    if missing_mounts:
        errors.append(f"mounts is missing required roles: {missing_mounts}")
    for role, mount in mounts.items():
        if not re.fullmatch(r"[A-Za-z][A-Za-z0-9]+", role):
            errors.append(f"mountRole must be a portable role, not a path: {role}")
        target = _text(mount.get("containerTarget"), f"mount {role}.containerTarget", errors)
        if target and not target.startswith("/"):
            errors.append(f"mount {role}.containerTarget must be an absolute container path")
        access = _text(mount.get("access"), f"mount {role}.access", errors)
        if access and access not in ACCESS:
            errors.append(f"mount {role}.access is invalid")
        for field in ("purpose", "allowedContent", "excludedContent", "evidence", "reevaluationTrigger"):
            _text(mount.get(field), f"mount {role}.{field}", errors)

    executions = _unique(data.get("executions"), "checkId", "executions", errors)
    missing_executions = sorted(REQUIRED_EXECUTIONS - set(executions))
    if missing_executions:
        errors.append(f"executions is missing required checks: {missing_executions}")
    for check_id, execution in executions.items():
        location = _text(execution.get("location"), f"execution {check_id}.location", errors)
        proof = _text(execution.get("proofLevel"), f"execution {check_id}.proofLevel", errors)
        platform = _text(execution.get("platform"), f"execution {check_id}.platform", errors, placeholders=False)
        command = _text(execution.get("command"), f"execution {check_id}.command", errors, placeholders=False)
        if location and location not in LOCATIONS:
            errors.append(f"execution {check_id}.location is invalid")
        if proof and proof not in PROOF_LEVELS:
            errors.append(f"execution {check_id}.proofLevel is invalid")
        for field in ("evidence", "proofBoundary", "reevaluationTrigger"):
            _text(execution.get(field), f"execution {check_id}.{field}", errors)
        if proof == "PlatformVerified" and platform in ("", "N/A"):
            errors.append(f"execution {check_id}: PlatformVerified requires a platform")
        if location == "NotPermitted" and command != "N/A":
            errors.append(f"execution {check_id}: NotPermitted must use command N/A")
        if location == "Open" and proof != "NotVerified":
            errors.append(f"execution {check_id}: Open must use NotVerified")

    if recommendation == "ApprovedWithBoundaries" and any(
        control.get("applicability") == "Open" for control in controls.values()
    ):
        errors.append("ApprovedWithBoundaries is incompatible with Open controls")
    return errors


def _parse_args(argv: list[str]) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Validate sandbox applicability evidence read-only.")
    parser.add_argument("--evidence", required=True, help="Repository-relative assessment JSON path.")
    parser.add_argument("--repo-root", default=".", help="Repository root used to resolve the evidence path.")
    parser.add_argument("--json", action="store_true", help="Emit one machine-readable result object.")
    return parser.parse_args(argv)


def main(argv: list[str] | None = None) -> int:
    args = _parse_args(sys.argv[1:] if argv is None else argv)
    root = Path(args.repo_root).resolve()
    evidence = Path(args.evidence)
    evidence = evidence.resolve() if evidence.is_absolute() else (root / evidence).resolve()
    try:
        evidence.relative_to(root)
    except ValueError:
        print("ERROR: evidence must remain inside the repository root", file=sys.stderr)
        return 2
    try:
        data = json.loads(evidence.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        print(f"ERROR: cannot read assessment: {exc}", file=sys.stderr)
        return 2
    errors = validate(data)
    result = {
        "status": "Pass" if not errors else "Fail",
        "evidence": evidence.relative_to(root).as_posix(),
        "controls": len(data.get("controls", [])) if isinstance(data, dict) else 0,
        "recommendation": data.get("recommendation", "N/A") if isinstance(data, dict) else "N/A",
        "errors": errors,
    }
    if args.json:
        print(json.dumps(result, sort_keys=True))
    elif errors:
        print(f"FAIL: sandbox applicability evidence has {len(errors)} error(s)", file=sys.stderr)
        for error in errors:
            print(f"- {error}", file=sys.stderr)
    else:
        print(
            f"PASS: {result['evidence']} has {result['controls']} CL-12 controls; "
            f"recommendation={result['recommendation']}"
        )
    return 0 if not errors else 1


if __name__ == "__main__":
    raise SystemExit(main())
