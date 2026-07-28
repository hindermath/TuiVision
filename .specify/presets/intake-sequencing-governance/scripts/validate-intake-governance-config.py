#!/usr/bin/env python3
"""Validate the portable requirements/intake governance configuration."""

from __future__ import annotations

import argparse
import hashlib
import json
import re
import sys
import uuid
from pathlib import Path, PurePosixPath

BCP47 = re.compile(r"^[A-Za-z]{2,3}(?:-[A-Za-z0-9]{2,8})*$")
PROFILES = {
    "de": {
        "canonicalIndex": "Pflichtenheft.md",
        "intakePattern": "Lastenheft_<slug>.md",
        "orderView": "Lastenheft_Abarbeitungsreihenfolge.md",
    },
    "en": {
        "canonicalIndex": "RequirementsIndex.md",
        "intakePattern": "RequirementsIntake_<slug>.md",
        "orderView": "RequirementsIntakeOrder.md",
    },
}
ROLE_KEYS = {
    "requirements-index",
    "requirements-intake",
    "intake-order",
    "requirements-baseline",
}
COLLECTION_KEYS = {
    "baseline",
    "active",
    "archive",
    "backlog",
    "history",
    "seriesManifest",
}


class ContractError(Exception):
    """A stable validation error."""

    def __init__(self, code: str, message: str, outcome: str = "Blocked") -> None:
        super().__init__(message)
        self.code = code
        self.outcome = outcome


def fail(code: str, message: str, outcome: str = "Blocked") -> None:
    raise ContractError(code, message, outcome)


def load_json(path: Path) -> dict:
    try:
        raw = path.read_bytes()
        if raw.startswith(b"\xef\xbb\xbf"):
            raw = raw[3:]
        text = raw.decode("utf-8", errors="strict")
        if "\x00" in text:
            fail("RIG001", "binary NUL is not allowed")
        data = json.loads(text.replace("\r\n", "\n").replace("\r", "\n"))
    except (OSError, UnicodeDecodeError, json.JSONDecodeError) as exc:
        fail("RIG001", f"invalid strict UTF-8 JSON: {exc}")
    if not isinstance(data, dict):
        fail("RIG001", "configuration root must be an object")
    return data


def relative(value: str) -> bool:
    candidate = PurePosixPath(value)
    return bool(value) and not candidate.is_absolute() and ".." not in candidate.parts


def required_text(obj: dict, key: str, code: str) -> str:
    value = obj.get(key)
    if not isinstance(value, str) or not value.strip():
        fail(code, f"{key} must be a non-empty string")
    return value.strip()


def validate_path(value: str, label: str) -> None:
    if not relative(value):
        fail("RIG004", f"{label} must be repository-relative")


def normalized_bytes(path: Path) -> bytes:
    try:
        raw = path.read_bytes()
        if raw.startswith(b"\xef\xbb\xbf"):
            raw = raw[3:]
        text = raw.decode("utf-8", errors="strict")
    except (OSError, UnicodeDecodeError) as exc:
        fail("RIG014", f"cannot read strict UTF-8 target {path}: {exc}")
    if "\x00" in text:
        fail("RIG014", f"binary NUL is not allowed in {path}")
    return text.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")


def normalized_sha256(path: Path) -> str:
    return hashlib.sha256(normalized_bytes(path)).hexdigest()


def intake_name_matches(name: str, pattern: str) -> bool:
    prefix, suffix = pattern.split("<slug>", 1)
    return name.startswith(prefix) and name.endswith(suffix) and len(name) > len(prefix) + len(suffix)


def belongs_to_nested_repository(path: Path, repo: Path) -> bool:
    """Return true when a candidate belongs to a nested Git checkout."""
    current = path.parent
    while current != repo:
        if (current / ".git").exists():
            return True
        if current == current.parent:
            break
        current = current.parent
    return False


def validate_series_manifest(
    path: Path,
    repo: Path,
    active_dir: Path,
    pattern: str,
    inventory_mode: str,
) -> dict:
    manifest = load_json(path)
    targets = manifest.get("orderedTargets")
    if not isinstance(targets, list):
        fail("RIG014", "series manifest must contain orderedTargets")
    series_status = manifest.get("status")
    if series_status == "Idle":
        if targets or manifest.get("roots") != [] or manifest.get("dependencies") != []:
            fail("RIG017", "Idle requires zero targets, roots, and dependencies")
        return {
            "activeIntakeCount": 0,
            "seriesTargetCount": 0,
            "eligibleCandidate": "N/A",
            "dependencyCount": 0,
        }
    if not targets:
        fail("RIG014", "series manifest must contain non-empty orderedTargets")

    active_paths = {
        item.relative_to(repo).as_posix()
        for item in active_dir.iterdir()
        if item.is_file() and intake_name_matches(item.name, pattern)
    }
    target_paths: list[str] = []
    eligible: list[str] = []
    for index, target in enumerate(targets):
        if not isinstance(target, dict):
            fail("RIG014", f"orderedTargets[{index}] must be an object")
        target_path = required_text(target, "path", "RIG014")
        validate_path(target_path, f"orderedTargets[{index}].path")
        if target_path in target_paths:
            fail("RIG013", f"duplicate active target {target_path}")
        target_paths.append(target_path)
        target_file = repo / target_path
        if not target_file.is_file():
            fail("RIG014", f"missing active target {target_path}")
        expected_hash = required_text(target, "normalizedSha256", "RIG015")
        if not re.fullmatch(r"[0-9a-f]{64}", expected_hash):
            fail("RIG015", f"invalid normalizedSha256 for {target_path}")
        if normalized_sha256(target_file) != expected_hash:
            fail("RIG015", f"hash drift for {target_path}")
        status = required_text(target, "status", "RIG017")
        if status == "Eligible":
            eligible.append(target_path)

    if inventory_mode == "DirectoryStrict" and set(target_paths) != active_paths:
        missing = sorted(active_paths - set(target_paths))
        extra = sorted(set(target_paths) - active_paths)
        fail("RIG013", f"active inventory mismatch; missing={missing}, extra={extra}")
    if len(eligible) != 1:
        fail("RIG017", f"exactly one Eligible target is required, found {len(eligible)}")

    dependencies = manifest.get("dependencies", [])
    if not isinstance(dependencies, list):
        fail("RIG016", "dependencies must be an array")
    positions = {value: index for index, value in enumerate(target_paths)}
    for index, edge in enumerate(dependencies):
        if not isinstance(edge, dict):
            fail("RIG016", f"dependencies[{index}] must be an object")
        source = required_text(edge, "from", "RIG016")
        target = required_text(edge, "to", "RIG016")
        if source not in positions or target not in positions or source == target:
            fail("RIG016", f"dependency {source} -> {target} has invalid references")
        if positions[source] >= positions[target]:
            fail("RIG016", f"dependency {source} -> {target} contradicts order")

    return {
        "activeIntakeCount": len(target_paths),
        "seriesTargetCount": len(target_paths),
        "eligibleCandidate": eligible[0],
        "dependencyCount": len(dependencies),
    }


def validate_config(data: dict, repo: Path) -> dict:
    schema = data.get("schemaVersion")
    if schema == "1.0":
        fail("RIG002", "schema 1.0 requires an authorized migration", "MigrationRequired")
    if schema != "2.0":
        fail("RIG002", "schemaVersion must be 2.0")

    language = data.get("documentationLanguage")
    if not isinstance(language, str) or not language.strip():
        fail("RIG003", "documentationLanguage needs an explicit BCP-47 value", "NeedsClarification")
    language = language.strip()
    if language in {"Undetermined", "und"}:
        fail("RIG003", "documentation language remains ambiguous", "NeedsClarification")
    if not BCP47.fullmatch(language):
        fail("RIG003", "documentationLanguage is not a supported BCP-47 shape")

    naming = data.get("artifactNaming")
    if not isinstance(naming, dict):
        fail("RIG005", "artifactNaming must be an object")
    profile = required_text(naming, "profile", "RIG005")
    if profile not in {"de", "en", "explicit"}:
        fail("RIG005", "artifactNaming.profile must be de, en, or explicit")
    resolved = dict(PROFILES.get(profile, {}))
    for key in ("canonicalIndex", "intakePattern", "orderView"):
        override = naming.get(key)
        if override is not None:
            if not isinstance(override, str) or not override.strip():
                fail("RIG005", f"artifactNaming.{key} must be non-empty")
            resolved[key] = override.strip()
        if key not in resolved:
            fail("RIG005", f"explicit profile requires artifactNaming.{key}")
        validate_path(resolved[key], f"artifactNaming.{key}")
    if "<slug>" not in resolved["intakePattern"]:
        fail("RIG005", "intakePattern must contain <slug>")

    roles = data.get("roles")
    if not isinstance(roles, dict) or set(roles) != ROLE_KEYS:
        fail("RIG006", "roles must contain exactly the four portable role IDs")
    for key, value in roles.items():
        if not isinstance(value, str):
            fail("RIG006", f"roles.{key} must be a string")
        validate_path(value, f"roles.{key}")

    collections = data.get("collections")
    if not isinstance(collections, dict) or set(collections) != COLLECTION_KEYS:
        fail("RIG007", "collections must contain exactly the six collection paths")
    for key, value in collections.items():
        if not isinstance(value, str):
            fail("RIG007", f"collections.{key} must be a string")
        validate_path(value, f"collections.{key}")
    values = list(collections.values())
    if len(values) != len(set(values)):
        fail("RIG007", "collection paths must be unique")

    aliases = data.get("legacyArtifactNames", [])
    if not isinstance(aliases, list) or len(aliases) > 20:
        fail("RIG008", "legacyArtifactNames must be an array with at most 20 entries")
    for index, alias in enumerate(aliases):
        if not isinstance(alias, str):
            fail("RIG008", f"legacyArtifactNames[{index}] must be a string")
        validate_path(alias, f"legacyArtifactNames[{index}]")
    if len(aliases) != len(set(aliases)):
        fail("RIG008", "legacyArtifactNames must be unique")
    inventory_mode = data.get("inventoryMode", "DirectoryStrict")
    if inventory_mode not in {"DirectoryStrict", "SeriesManifest"}:
        fail("RIG013", "inventoryMode must be DirectoryStrict or SeriesManifest")
    role_paths = list(roles.values())
    if len(role_paths) != len(set(role_paths)):
        fail("RIG006", "portable role paths must be unique")
    if roles["requirements-index"] != resolved["canonicalIndex"]:
        fail("RIG006", "requirements-index must equal the resolved canonicalIndex")
    if roles["intake-order"] != resolved["orderView"]:
        fail("RIG006", "intake-order must equal the resolved orderView")
    if roles["requirements-intake"] != collections["active"]:
        fail("RIG007", "requirements-intake must equal collections.active")
    if roles["requirements-baseline"] != collections["baseline"]:
        fail("RIG007", "requirements-baseline must equal collections.baseline")

    missing: list[str] = []
    for key in ("requirements-index", "intake-order"):
        if not (repo / roles[key]).is_file():
            missing.append(roles[key])
    for key in ("requirements-intake", "requirements-baseline"):
        if not (repo / roles[key]).is_dir():
            missing.append(roles[key])
    if not (repo / collections["seriesManifest"]).is_file():
        missing.append(collections["seriesManifest"])

    outcome = "Aligned" if not missing else "MigrationRequired"
    inventory = {
        "baselineCount": 0,
        "activeIntakeCount": 0,
        "archiveIntakeCount": 0,
        "backlogIntakeCount": 0,
        "historyIntakeCount": 0,
        "seriesTargetCount": 0,
        "dependencyCount": 0,
        "eligibleCandidate": "N/A",
    }
    if outcome == "Aligned":
        pattern = resolved["intakePattern"]
        active_dir = repo / collections["active"]
        inventory.update(
            validate_series_manifest(
                repo / collections["seriesManifest"],
                repo,
                active_dir,
                pattern,
                inventory_mode,
            )
        )
        for key, result_key in (
            ("baseline", "baselineCount"),
            ("archive", "archiveIntakeCount"),
            ("backlog", "backlogIntakeCount"),
            ("history", "historyIntakeCount"),
        ):
            directory = repo / collections[key]
            if directory.is_dir():
                inventory[result_key] = sum(1 for item in directory.rglob("*") if item.is_file())

        canonical_name = Path(resolved["canonicalIndex"]).name
        canonical_candidates = [
            item
            for item in repo.rglob(canonical_name)
            if ".git" not in item.parts
            and "history" not in item.parts
            and "archive" not in item.parts
            and not belongs_to_nested_repository(item, repo)
        ]
        if canonical_candidates != [repo / roles["requirements-index"]]:
            fail("RIG013", f"exactly one current canonical index is required: {canonical_candidates}")

    return {
        "schemaVersion": "2.0",
        "outcome": outcome,
        "documentationLanguage": language,
        "profile": profile,
        "resolvedNaming": resolved,
        "roles": roles,
        "collections": collections,
        "legacyArtifactNames": aliases,
        "inventoryMode": inventory_mode,
        "inventory": inventory,
        "missingPaths": sorted(set(missing)),
        "nextAction": "N/A" if outcome == "Aligned" else "$speckit-intake-update",
    }


def validate_journal(data: dict) -> dict:
    if data.get("schemaVersion") != "2.0" or data.get("documentType") != "IntakeGovernanceMigrationJournal":
        fail("RIG009", "migration journal schema or document type is invalid")
    state = required_text(data, "state", "RIG009")
    if state not in {"Proposed", "Authorized", "Applying", "Completed", "RolledBack", "NeedsRepair"}:
        fail("RIG009", "migration journal state is invalid")
    authority = required_text(data, "authorityEvidence", "RIG010")
    if state != "Proposed" and authority == "N/A":
        fail("RIG010", "a mutating journal state requires authority evidence")
    for key in ("beforeSha256", "afterSha256"):
        value = required_text(data, key, "RIG011")
        if value != "N/A" and not re.fullmatch(r"[0-9a-f]{64}", value):
            fail("RIG011", f"{key} must be N/A or lowercase SHA-256")
    if state == "Completed" and data.get("afterSha256") == "N/A":
        fail("RIG011", "Completed requires afterSha256")
    if state == "NeedsRepair" and not data.get("repairBoundary"):
        fail("RIG012", "NeedsRepair requires repairBoundary")
    operation_id = required_text(data, "operationId", "RIG009")
    try:
        uuid.UUID(operation_id)
    except ValueError:
        fail("RIG009", "operationId must be a UUID")
    for key in ("moves", "updatedReferences", "validation"):
        value = data.get(key)
        if not isinstance(value, list):
            fail("RIG009", f"{key} must be an array")
    if state == "Completed":
        if not data["validation"]:
            fail("RIG011", "Completed requires validation evidence")
        if required_text(data, "rollback", "RIG011") == "N/A":
            fail("RIG011", "Completed requires a documented rollback boundary")
    return {"schemaVersion": "2.0", "outcome": state, "nextAction": "N/A"}


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--config")
    parser.add_argument("--journal")
    parser.add_argument("--repo", default=".")
    parser.add_argument("--json", action="store_true")
    args = parser.parse_args()
    if bool(args.config) == bool(args.journal):
        print("ERROR [RIG001]: exactly one of --config or --journal is required", file=sys.stderr)
        return 2
    try:
        path = Path(args.config or args.journal)
        data = load_json(path)
        result = validate_config(data, Path(args.repo).resolve()) if args.config else validate_journal(data)
    except ContractError as exc:
        result = {"outcome": exc.outcome, "errorClass": exc.code, "message": str(exc)}
        if args.json:
            print(json.dumps(result, ensure_ascii=False, sort_keys=True))
        else:
            print(f"ERROR [{exc.code}] {exc.outcome}: {exc}", file=sys.stderr)
        return 1 if exc.outcome in {"MigrationRequired", "NeedsClarification"} else 2
    if args.json:
        print(json.dumps(result, ensure_ascii=False, sort_keys=True))
    else:
        print(f"PASS: requirements intake governance is {result['outcome']}")
    return 0 if result["outcome"] == "Aligned" or args.journal else 1


if __name__ == "__main__":
    raise SystemExit(main())
