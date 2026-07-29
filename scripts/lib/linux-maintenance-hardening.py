#!/usr/bin/env python3
"""Structured safety contracts for Linux agentic toolchain maintenance."""

from __future__ import annotations

import argparse
import hashlib
import json
import os
from pathlib import Path
import re
import signal
import subprocess
import sys
import tarfile
import tempfile
import time
from typing import NoReturn
from urllib.parse import urlparse
import uuid


PROBE_STATUSES = {
    "Available",
    "Missing",
    "Unusable",
    "TimedOut",
    "CapabilityBlocked",
}
FINAL_STATUSES = {"Present", "Installed", "Planned", "Failed", "StillMissing"}
SCOPES = {"required", "optional"}
CAPABILITY_PATTERNS = (
    re.compile(r"\bcap_[a-z0-9_]+\b", re.IGNORECASE),
    re.compile(r"snap-confine", re.IGNORECASE),
    re.compile(r"operation not permitted", re.IGNORECASE),
)
SECRET_PATTERNS = (
    re.compile(r"\bgh[pousr]_[A-Za-z0-9_]{20,}\b"),
    re.compile(r"\bsk-[A-Za-z0-9_-]{16,}\b"),
    re.compile(r"\bAKIA[0-9A-Z]{16}\b"),
    re.compile(r"\bAIza[0-9A-Za-z_-]{20,}\b"),
)
SHA256_PATTERN = re.compile(r"^[0-9a-f]{64}$")


class ContractError(RuntimeError):
    """Raised when an input violates a fail-closed maintenance contract."""


def emit(value: dict) -> None:
    print(json.dumps(value, ensure_ascii=False, separators=(",", ":")))


def fail(message: str) -> NoReturn:
    emit(
        {
            "status": "InvalidContract",
            "sanitizedEvidence": sanitize(message, []),
            "nextAction": (
                "Registry- oder Aufrufvertrag korrigieren / "
                "correct the registry or invocation contract."
            ),
        }
    )
    raise SystemExit(2)


def sanitize(value: str, redactions: list[str], limit: int = 2048) -> str:
    text = value.replace("\x00", "").replace("\r", "\n")
    defaults = [os.environ.get("HOME", ""), os.getcwd()]
    for candidate in [*redactions, *defaults]:
        if candidate:
            text = text.replace(candidate, "<redacted>")
    for pattern in SECRET_PATTERNS:
        text = pattern.sub("<redacted-secret>", text)
    text = " ".join(text.split())
    encoded = text.encode("utf-8", errors="replace")
    if len(encoded) > limit:
        encoded = encoded[:limit]
        text = encoded.decode("utf-8", errors="ignore") + "…"
    return text or "N/A"


def atomic_write_json(path: Path, value: dict) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_name(f".{path.name}.{uuid.uuid4().hex}.tmp")
    try:
        temporary.write_text(
            json.dumps(value, ensure_ascii=False, indent=2) + "\n",
            encoding="utf-8",
        )
        os.replace(temporary, path)
    finally:
        try:
            temporary.unlink()
        except FileNotFoundError:
            pass


def validate_timeout(value: float) -> float:
    if not 0.05 <= value <= 300:
        raise ContractError("timeout must be between 0.05 and 300 seconds")
    return value


def read_limited_output(handle, limit: int) -> str:
    handle.seek(0)
    return handle.read(limit + 1).decode("utf-8", errors="replace")


def bounded_probe(
    tool_id: str,
    command: list[str],
    timeout_seconds: float,
    max_evidence_bytes: int,
    redactions: list[str],
) -> tuple[dict, int]:
    if not tool_id or not command or not command[0]:
        raise ContractError("tool id and command are required")
    timeout_seconds = validate_timeout(timeout_seconds)
    if not 128 <= max_evidence_bytes <= 8192:
        raise ContractError("evidence limit must be between 128 and 8192 bytes")

    started = time.monotonic()
    with tempfile.TemporaryFile() as output:
        try:
            process = subprocess.Popen(
                command,
                stdin=subprocess.DEVNULL,
                stdout=output,
                stderr=subprocess.STDOUT,
                start_new_session=True,
            )
        except FileNotFoundError:
            duration = int((time.monotonic() - started) * 1000)
            return (
                {
                    "toolId": tool_id,
                    "commandLabel": Path(command[0]).name,
                    "status": "Missing",
                    "exitCode": 127,
                    "durationMs": duration,
                    "timedOut": False,
                    "processTreeCleaned": True,
                    "sanitizedEvidence": "Befehl nicht gefunden / command not found.",
                    "nextAction": "Werkzeug installieren / install the tool.",
                },
                1,
            )
        except OSError as exc:
            duration = int((time.monotonic() - started) * 1000)
            return (
                {
                    "toolId": tool_id,
                    "commandLabel": Path(command[0]).name,
                    "status": "Unusable",
                    "exitCode": 126,
                    "durationMs": duration,
                    "timedOut": False,
                    "processTreeCleaned": True,
                    "sanitizedEvidence": sanitize(str(exc), redactions, max_evidence_bytes),
                    "nextAction": "Launcher und Berechtigungen prüfen / check launcher and permissions.",
                },
                1,
            )

        timed_out = False
        cleaned = True
        try:
            exit_code = process.wait(timeout=timeout_seconds)
        except subprocess.TimeoutExpired:
            timed_out = True
            exit_code = 124
            try:
                os.killpg(process.pid, signal.SIGTERM)
            except ProcessLookupError:
                pass
            try:
                process.wait(timeout=0.5)
            except subprocess.TimeoutExpired:
                try:
                    os.killpg(process.pid, signal.SIGKILL)
                except ProcessLookupError:
                    pass
                try:
                    process.wait(timeout=2)
                except subprocess.TimeoutExpired:
                    cleaned = False

        evidence = sanitize(
            read_limited_output(output, max_evidence_bytes),
            redactions,
            max_evidence_bytes,
        )
        duration = int((time.monotonic() - started) * 1000)
        status = "Available"
        next_action = "N/A"
        result_exit = 0
        if timed_out:
            status = "TimedOut"
            result_exit = 1
            next_action = (
                "Hängenden Launcher prüfen / inspect the unresponsive launcher."
            )
        elif exit_code != 0:
            status = (
                "CapabilityBlocked"
                if any(pattern.search(evidence) for pattern in CAPABILITY_PATTERNS)
                else "Unusable"
            )
            result_exit = 1
            next_action = (
                "Container-/Capability-Grenze prüfen / inspect container capabilities."
                if status == "CapabilityBlocked"
                else "Launcher und Installation prüfen / inspect launcher and installation."
            )
        return (
            {
                "toolId": tool_id,
                "commandLabel": Path(command[0]).name,
                "status": status,
                "exitCode": exit_code,
                "durationMs": duration,
                "timedOut": timed_out,
                "processTreeCleaned": cleaned,
                "sanitizedEvidence": evidence,
                "nextAction": next_action,
            },
            result_exit,
        )


def parse_os_release(path: Path) -> tuple[str, str]:
    try:
        lines = path.read_text(encoding="utf-8").splitlines()
    except (OSError, UnicodeError) as exc:
        raise ContractError(f"os-release cannot be read: {exc}") from exc
    values: dict[str, str] = {}
    for line in lines:
        if not line or line.lstrip().startswith("#") or "=" not in line:
            continue
        key, value = line.split("=", 1)
        if key in {"ID", "VERSION_ID"}:
            values[key] = value.strip().strip("\"'")
    distribution = values.get("ID", "").lower()
    version = values.get("VERSION_ID", "")
    if not re.fullmatch(r"[a-z0-9._-]+", distribution):
        raise ContractError("os-release ID is missing or invalid")
    if not re.fullmatch(r"\d{2}\.\d{2}", version):
        raise ContractError("os-release VERSION_ID is missing or invalid")
    return distribution, version


def load_json(path: Path) -> dict:
    try:
        value = json.loads(path.read_text(encoding="utf-8-sig"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError(f"JSON cannot be read: {exc}") from exc
    if not isinstance(value, dict):
        raise ContractError("JSON root must be an object")
    return value


def resolve_swift_contract(
    registry_path: Path,
    os_release_path: Path,
    architecture: str,
) -> tuple[dict, int]:
    aliases = {"arm64": "aarch64", "amd64": "x86_64"}
    architecture = aliases.get(architecture, architecture)
    registry = load_json(registry_path)
    tools = registry.get("tools")
    if not isinstance(tools, list):
        raise ContractError("tools must be an array")
    matches = [item for item in tools if isinstance(item, dict) and item.get("id") == "swift"]
    if len(matches) != 1:
        raise ContractError("exactly one swift tool entry is required")
    install = matches[0].get("install")
    linux = install.get("linux") if isinstance(install, dict) else None
    if not isinstance(install, dict) or install.get("manager") != "swiftly":
        raise ContractError("swift install manager must be swiftly")
    if not isinstance(linux, dict):
        raise ContractError("swift install.linux contract is required")

    swiftly_version = linux.get("swiftlyVersion")
    swift_version = linux.get("swiftVersion")
    if not isinstance(swiftly_version, str) or not re.fullmatch(r"\d+\.\d+\.\d+", swiftly_version):
        raise ContractError("swiftlyVersion must be a stable semantic version")
    if not isinstance(swift_version, str) or not re.fullmatch(r"\d+\.\d+\.\d+", swift_version):
        raise ContractError("swiftVersion must be a stable semantic version")

    distribution, version = parse_os_release(os_release_path)
    profiles = linux.get("profiles")
    artifacts = linux.get("artifacts")
    profile_key = f"{distribution}:{version}"
    platform = profiles.get(profile_key) if isinstance(profiles, dict) else None
    artifact = artifacts.get(architecture) if isinstance(artifacts, dict) else None
    if not isinstance(platform, str) or not isinstance(artifact, dict):
        return (
            {
                "status": "Unsupported",
                "distribution": distribution,
                "distributionVersion": version,
                "architecture": architecture,
                "nextAction": (
                    "Unterstütztes Ubuntu 22.04/24.04 auf x86_64/aarch64 verwenden / "
                    "use supported Ubuntu 22.04/24.04 on x86_64/aarch64."
                ),
            },
            1,
        )

    url = artifact.get("url")
    digest = artifact.get("sha256")
    parsed = urlparse(url) if isinstance(url, str) else None
    if (
        parsed is None
        or parsed.scheme != "https"
        or parsed.hostname != "download.swift.org"
        or not isinstance(digest, str)
        or not SHA256_PATTERN.fullmatch(digest)
    ):
        raise ContractError("swift artifact URL or SHA-256 is invalid")
    return (
        {
            "status": "Supported",
            "distribution": distribution,
            "distributionVersion": version,
            "architecture": architecture,
            "swiftlyPlatform": platform,
            "swiftlyVersion": swiftly_version,
            "swiftVersion": swift_version,
            "url": url,
            "sha256": digest,
            "nextAction": "N/A",
        },
        0,
    )


def verify_sha256(path: Path, expected: str) -> tuple[dict, int]:
    if not SHA256_PATTERN.fullmatch(expected):
        raise ContractError("expected SHA-256 must use 64 lowercase hex characters")
    digest = hashlib.sha256()
    try:
        with path.open("rb") as handle:
            for block in iter(lambda: handle.read(1024 * 1024), b""):
                digest.update(block)
    except OSError as exc:
        raise ContractError(f"artifact cannot be read: {exc}") from exc
    actual = digest.hexdigest()
    if actual != expected:
        return (
            {
                "status": "IntegrityMismatch",
                "expectedSha256": expected,
                "actualSha256": actual,
                "nextAction": (
                    "Download verwerfen und Registry prüfen / "
                    "discard the download and review the registry."
                ),
            },
            1,
        )
    return (
        {
            "status": "Verified",
            "sha256": actual,
            "nextAction": "N/A",
        },
        0,
    )


def extract_swiftly(archive: Path, output: Path) -> tuple[dict, int]:
    """Extract only the regular swiftly binary without trusting archive paths."""
    try:
        with tarfile.open(archive, mode="r:gz") as bundle:
            matches = [
                member
                for member in bundle.getmembers()
                if member.isfile() and Path(member.name).name == "swiftly"
            ]
            if len(matches) != 1:
                raise ContractError(
                    "swiftly archive must contain exactly one regular swiftly binary"
                )
            source = bundle.extractfile(matches[0])
            if source is None:
                raise ContractError("swiftly binary cannot be read from archive")
            output.parent.mkdir(parents=True, exist_ok=True)
            temporary = output.with_name(f".{output.name}.{uuid.uuid4().hex}.tmp")
            try:
                with temporary.open("wb") as target:
                    while block := source.read(1024 * 1024):
                        target.write(block)
                temporary.chmod(0o755)
                os.replace(temporary, output)
            finally:
                try:
                    temporary.unlink()
                except FileNotFoundError:
                    pass
    except (OSError, tarfile.TarError) as exc:
        raise ContractError(f"swiftly archive cannot be extracted: {exc}") from exc
    return (
        {
            "status": "Extracted",
            "commandLabel": "swiftly",
            "nextAction": "N/A",
        },
        0,
    )


def parse_result_rows(path: Path) -> list[dict]:
    try:
        lines = path.read_text(encoding="utf-8").splitlines()
    except (OSError, UnicodeError) as exc:
        raise ContractError(f"result rows cannot be read: {exc}") from exc
    items: list[dict] = []
    sequences: set[int] = set()
    item_keys: set[tuple[str, str, str]] = set()
    for line_number, line in enumerate(lines, start=1):
        fields = line.split("\t")
        if len(fields) != 9:
            raise ContractError(f"result row {line_number} must have nine fields")
        (
            sequence_text,
            kind,
            item_id,
            scope,
            attempted_text,
            final_status,
            probe_status,
            evidence,
            next_action,
        ) = fields
        try:
            sequence = int(sequence_text)
        except ValueError as exc:
            raise ContractError(f"result row {line_number} has invalid sequence") from exc
        if sequence <= 0 or sequence in sequences:
            raise ContractError("result sequences must be positive and unique")
        if not re.fullmatch(r"[A-Za-z0-9@._/+:-]+", item_id):
            raise ContractError(f"result row {line_number} has invalid item id")
        if not re.fullmatch(r"[a-z][a-z0-9-]*", kind):
            raise ContractError(f"result row {line_number} has invalid kind")
        if scope not in SCOPES or final_status not in FINAL_STATUSES:
            raise ContractError(f"result row {line_number} has invalid scope or status")
        if probe_status != "N/A" and probe_status not in PROBE_STATUSES:
            raise ContractError(f"result row {line_number} has invalid probe status")
        if attempted_text not in {"true", "false"}:
            raise ContractError(f"result row {line_number} has invalid attempted flag")
        item_key = (kind, item_id, scope)
        if item_key in item_keys:
            raise ContractError(f"result row {line_number} duplicates an item")
        sequences.add(sequence)
        item_keys.add(item_key)
        items.append(
            {
                "sequence": sequence,
                "kind": kind,
                "itemId": item_id,
                "scope": scope,
                "attempted": attempted_text == "true",
                "finalStatus": final_status,
                "probeStatus": probe_status,
                "sanitizedEvidence": sanitize(evidence, []),
                "nextAction": sanitize(next_action, []),
            }
        )
    ordered = sorted(items, key=lambda item: item["sequence"])
    if [item["sequence"] for item in ordered] != list(range(1, len(ordered) + 1)):
        raise ContractError("result sequences must be consecutive from one")
    return ordered


def summarize_results(input_path: Path, output_path: Path, platform: str, mode: str) -> int:
    if platform not in {"Darwin", "Linux"}:
        raise ContractError("result platform must be Darwin or Linux")
    if mode not in {"update", "dry-run", "compare-only"}:
        raise ContractError("result mode is invalid")
    items = parse_result_rows(input_path)
    remaining_required = [
        item["itemId"]
        for item in items
        if item["scope"] == "required"
        and item["finalStatus"] in {"Failed", "StillMissing", "Planned"}
    ]
    optional_drift = [
        item["itemId"]
        for item in items
        if item["scope"] == "optional"
        and item["finalStatus"] in {"Failed", "StillMissing", "Planned"}
    ]
    exit_code = 1 if remaining_required else 0
    overall = (
        "PARTIAL"
        if remaining_required
        else "SUCCESS_WITH_WARNINGS"
        if optional_drift
        else "SUCCESS"
    )
    report = {
        "schemaVersion": "1.0",
        "platform": platform,
        "mode": mode,
        "overallStatus": overall,
        "exitCode": exit_code,
        "items": items,
        "remainingRequired": remaining_required,
        "optionalDrift": optional_drift,
        "nextAction": (
            "Fehlende Pflichtwerkzeuge beheben / resolve missing required tools."
            if remaining_required
            else "N/A"
        ),
    }
    atomic_write_json(output_path, report)
    emit(report)
    return exit_code


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser()
    commands = parser.add_subparsers(dest="command", required=True)

    probe = commands.add_parser("probe")
    probe.add_argument("--tool-id", required=True)
    probe.add_argument("--timeout-seconds", type=float, default=5.0)
    probe.add_argument("--max-evidence-bytes", type=int, default=2048)
    probe.add_argument("--redact", action="append", default=[])
    probe.add_argument("probe_command", nargs=argparse.REMAINDER)

    swift = commands.add_parser("swift-contract")
    swift.add_argument("--registry", type=Path, required=True)
    swift.add_argument("--os-release", type=Path, required=True)
    swift.add_argument("--architecture", required=True)

    checksum = commands.add_parser("verify-sha256")
    checksum.add_argument("--path", type=Path, required=True)
    checksum.add_argument("--expected", required=True)

    extract = commands.add_parser("extract-swiftly")
    extract.add_argument("--archive", type=Path, required=True)
    extract.add_argument("--output", type=Path, required=True)

    summary = commands.add_parser("summarize-results")
    summary.add_argument("--input", type=Path, required=True)
    summary.add_argument("--output", type=Path, required=True)
    summary.add_argument("--platform", required=True)
    summary.add_argument("--mode", required=True)
    return parser


def main() -> int:
    args = build_parser().parse_args()
    try:
        if args.command == "probe":
            command = args.probe_command
            if command and command[0] == "--":
                command = command[1:]
            result, exit_code = bounded_probe(
                args.tool_id,
                command,
                args.timeout_seconds,
                args.max_evidence_bytes,
                args.redact,
            )
            emit(result)
            return exit_code
        if args.command == "swift-contract":
            result, exit_code = resolve_swift_contract(
                args.registry,
                args.os_release,
                args.architecture,
            )
            emit(result)
            return exit_code
        if args.command == "verify-sha256":
            result, exit_code = verify_sha256(args.path, args.expected)
            emit(result)
            return exit_code
        if args.command == "extract-swiftly":
            result, exit_code = extract_swiftly(args.archive, args.output)
            emit(result)
            return exit_code
        if args.command == "summarize-results":
            return summarize_results(
                args.input,
                args.output,
                args.platform,
                args.mode,
            )
    except ContractError as exc:
        fail(str(exc))
    return 2


if __name__ == "__main__":
    raise SystemExit(main())
