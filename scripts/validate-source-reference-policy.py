#!/usr/bin/env python3
"""Validate the canonical TuiVision source-reference policy fail-closed."""

from __future__ import annotations

import argparse
import copy
import json
import re
import sys
from pathlib import Path, PurePosixPath


MAX_BYTES = 65_536
EXPECTED_ROOT_KEYS = {
    "schemaVersion",
    "policyId",
    "effectiveMode",
    "sourceRoles",
    "magiblot",
    "workflow",
    "dispositions",
    "conflictRules",
    "provenance",
    "reevaluationTriggers",
    "requiredSurfaces",
}
EXPECTED_ROLES = [
    (1, "TuiVisionProductContract", True, "CurrentProductSemantics"),
    (2, "MagiblotModernDesignReference", False, "ModernArchitectureAndImplementationIdeas"),
    (3, "BorlandTv203HistoricalIntent", False, "HistoricalIntentAndCompatibilityBoundary"),
    (4, "IndependentComparison", False, "FreeVisionAndTerminalGuiComparison"),
    (5, "ConsumerEvidence", False, "TvDemosTvFmAndTuiVisionExamples"),
]
EXPECTED_WORKFLOW = [
    "ReadCurrentTuiVisionContract",
    "ReviewRelevantMagiblotFilesAtApprovedPin",
    "ReviewRelevantTv203ImplementationAndHeaders",
    "ReviewConsumersAndIndependentImplementationsWhenMaterial",
    "RecordExactlyOneDisposition",
]
EXPECTED_DISPOSITIONS = [
    "AdoptModernization",
    "PreserveHistoricalIntent",
    "IntentionalTuiVisionDeviation",
    "N/A",
]
EXPECTED_CONFLICT_RULES = [
    "ExistingTuiVisionContractRemainsBindingUntilApprovedChange",
    "SourceRankAloneNeverResolvesConflict",
    "MagiblotDoesNotRequireCppInheritanceMemoryLayoutOrSourceForm",
    "MaterialHistoricalDeviationRequiresVisibleRationale",
]
EXPECTED_TRIGGERS = [
    "ChangedTuiVisionProductContract",
    "NewApprovedMagiblotPin",
    "MateriallyNewConsumerEvidence",
]
EXPECTED_SURFACES = [
    "docs/source-reference-policy.md",
    ".specify/memory/constitution.md",
    ".specify/templates/constitution-template.md",
    "AGENTS.md",
    "CLAUDE.md",
    "GEMINI.md",
    ".github/copilot-instructions.md",
    ".github/agents/copilot-instructions.md",
    "Pflichtenheft.md",
    ".specify/templates/spec-template.md",
    ".specify/templates/plan-template.md",
    ".specify/templates/tasks-template.md",
    ".specify/templates/commands/plan.md",
]
EXPECTED_PIN = "57b6f56b38e0ee75240a80a10ee0e11470c24693"
EXPECTED_TREE = "96dd03873955689ff0a79f6c8107a8148fe1ebd6"
EXPECTED_COPYRIGHT = "66220baeb9761b723fba913b74cf8257621a65c38cadb941fbb5bc181104b548"


def read_json(path: Path) -> dict:
    raw = path.read_bytes()
    if len(raw) > MAX_BYTES:
        raise ValueError("SRP001: policy exceeds the 65536-byte limit")
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    text = raw.decode("utf-8", errors="strict")
    if "\x00" in text:
        raise ValueError("SRP001: policy contains binary NUL")
    value = json.loads(text)
    if not isinstance(value, dict):
        raise ValueError("SRP001: policy root must be an object")
    return value


def add(errors: list[tuple[str, str]], code: str, message: str) -> None:
    errors.append((code, message))


def validate_policy(data: dict, repo: Path, check_surfaces: bool) -> list[tuple[str, str]]:
    errors: list[tuple[str, str]] = []

    if set(data) != EXPECTED_ROOT_KEYS:
        add(errors, "SRP010", "root keys must match the closed policy schema")
    if data.get("schemaVersion") != "1.0" or data.get("policyId") != "tuivision-source-reference-v1":
        add(errors, "SRP002", "schemaVersion and policyId must match version 1")

    actual_roles = []
    for role in data.get("sourceRoles", []) if isinstance(data.get("sourceRoles"), list) else []:
        if isinstance(role, dict):
            actual_roles.append(
                (role.get("order"), role.get("id"), role.get("normative"), role.get("purpose"))
            )
    if actual_roles != EXPECTED_ROLES:
        add(errors, "SRP003", "source roles or their order differ from the five-role contract")

    magiblot = data.get("magiblot") if isinstance(data.get("magiblot"), dict) else {}
    if (
        magiblot.get("repository") != "https://github.com/magiblot/tvision.git"
        or magiblot.get("commit") != EXPECTED_PIN
        or magiblot.get("tree") != EXPECTED_TREE
        or magiblot.get("copyrightSha256") != EXPECTED_COPYRIGHT
        or magiblot.get("pinUpdateMode") != "SeparateReadOnlyProvenanceAndDeltaReview"
    ):
        add(errors, "SRP004", "Magiblot repository, pin, tree, copyright hash, or update mode drifted")
    if magiblot.get("movingBranchesAllowed") is not False:
        add(errors, "SRP009", "moving branches must never be accepted as evidence")

    if data.get("workflow") != EXPECTED_WORKFLOW:
        add(errors, "SRP003", "source-review workflow differs from the required order")
    if data.get("dispositions") != EXPECTED_DISPOSITIONS:
        add(errors, "SRP005", "the four closed dispositions must remain exact and ordered")
    if data.get("conflictRules") != EXPECTED_CONFLICT_RULES:
        add(errors, "SRP005", "conflict rules differ from the approved contract")
    if data.get("effectiveMode") != "Prospective" or data.get("reevaluationTriggers") != EXPECTED_TRIGGERS:
        add(errors, "SRP006", "prospective mode and the three triggers must remain exact")

    provenance = data.get("provenance") if isinstance(data.get("provenance"), dict) else {}
    if (
        provenance.get("externalCheckout") != "OutsideTrackedRepository"
        or provenance.get("allowSourceCopy") is not False
        or provenance.get("licenseRepresentation") != "MultipartNotRepositoryWideMIT"
        or provenance.get("allowedStoredEvidence")
        != ["Pin", "Tree", "ReviewedPaths", "Hashes", "OriginalShortSummaries", "Permalinks"]
    ):
        add(errors, "SRP007", "no-copy, checkout, allowed-evidence, or multipart-license boundary drifted")

    if data.get("requiredSurfaces") != EXPECTED_SURFACES:
        add(errors, "SRP008", "required governance surfaces must remain exact and ordered")

    if check_surfaces and data.get("requiredSurfaces") == EXPECTED_SURFACES:
        tokens = [
            "<!-- source-reference-policy:begin -->",
            "<!-- source-reference-policy:end -->",
            EXPECTED_PIN,
            EXPECTED_TREE,
            "AdoptModernization",
            "PreserveHistoricalIntent",
            "IntentionalTuiVisionDeviation",
            "Prospective",
            "MultipartNotRepositoryWideMIT",
        ]
        for relative in EXPECTED_SURFACES:
            candidate = PurePosixPath(relative)
            if candidate.is_absolute() or ".." in candidate.parts:
                add(errors, "SRP008", f"unsafe required surface: {relative}")
                continue
            path = repo / relative
            try:
                raw = path.read_bytes()
                if len(raw) > 2_000_000:
                    raise ValueError("surface exceeds size limit")
                content = raw.decode("utf-8", errors="strict")
            except (OSError, UnicodeError, ValueError) as exc:
                add(errors, "SRP008", f"cannot read required surface {relative}: {exc}")
                continue
            if content.count(tokens[0]) != 1 or content.count(tokens[1]) != 1:
                add(errors, "SRP008", f"required marker pair is missing or duplicated in {relative}")
                continue
            missing = [token for token in tokens[2:] if token not in content]
            if missing:
                add(errors, "SRP008", f"required policy tokens are missing in {relative}")

    return errors


def set_path(document: dict, path: list[str], value: object) -> None:
    current: object = document
    for segment in path[:-1]:
        if not isinstance(current, dict) or segment not in current:
            raise ValueError(f"invalid fixture path segment: {segment}")
        current = current[segment]
    if not isinstance(current, dict):
        raise ValueError("fixture path does not resolve to an object")
    current[path[-1]] = value


def run_self_test(policy_path: Path, fixture_path: Path, repo: Path) -> int:
    baseline = read_json(policy_path)
    fixtures = read_json(fixture_path).get("fixtures", [])
    if not isinstance(fixtures, list) or not fixtures:
        print("SRP001: fixture matrix must contain fixtures", file=sys.stderr)
        return 2
    for fixture in fixtures:
        mutated = copy.deepcopy(baseline)
        set_path(mutated, fixture["path"], fixture["value"])
        codes = sorted({code for code, _ in validate_policy(mutated, repo, False)})
        expected = [fixture["expectedCode"]]
        if codes != expected:
            print(
                f"SRP001: fixture {fixture.get('name', '<unnamed>')} returned {codes}, expected {expected}",
                file=sys.stderr,
            )
            return 2
    print(f"PASS: {len(fixtures)} controlled negative source-policy fixtures")
    return 0


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--policy", required=True)
    parser.add_argument("--repo", default=".")
    parser.add_argument("--skip-surface-checks", action="store_true")
    parser.add_argument("--self-test")
    parser.add_argument("--json", action="store_true")
    args = parser.parse_args()

    repo = Path(args.repo).resolve()
    policy_path = Path(args.policy)
    if not policy_path.is_absolute():
        policy_path = repo / policy_path
    try:
        if args.self_test:
            fixture_path = Path(args.self_test)
            if not fixture_path.is_absolute():
                fixture_path = repo / fixture_path
            return run_self_test(policy_path, fixture_path, repo)
        data = read_json(policy_path)
        errors = validate_policy(data, repo, not args.skip_surface_checks)
    except (OSError, UnicodeError, ValueError, json.JSONDecodeError) as exc:
        print(str(exc) if str(exc).startswith("SRP") else f"SRP001: {exc}", file=sys.stderr)
        return 2

    if errors:
        for code, message in errors:
            print(f"{code}: {message}", file=sys.stderr)
        return 2
    if args.json:
        print(json.dumps({"status": "Pass", "policyId": data["policyId"], "surfaces": len(EXPECTED_SURFACES)}))
    else:
        print(f"PASS: {data['policyId']} ({len(EXPECTED_SURFACES)} surfaces, exact pin {EXPECTED_PIN[:12]})")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
