#!/usr/bin/env python3
"""Validate and maintain the declared workspace fleet without provider writes."""

from __future__ import annotations

import argparse
import ctypes
import fnmatch
import hashlib
import json
import os
import pathlib
import random
import re
import shutil
import subprocess
import sys
import tempfile
import time
import uuid
from datetime import datetime, timezone
from decimal import Decimal, InvalidOperation
from urllib.parse import urlsplit


VALID_KINDS = {"git-repository", "collection"}
VALID_CLASSES = {"canonical-fleet", "preset"}
VALID_FORGES = {"github", "gitlab", "codeberg", "forgejo", "generic-git"}
KNOWN_MSL_LANGUAGES = {
    "ada", "c#", "csharp", "dart", "elixir", "erlang", "f#", "fsharp",
    "go", "haskell", "java", "javascript", "kotlin", "ocaml", "python",
    "ruby", "rust", "scala", "spark", "swift", "typescript",
}
KNOWN_NON_MSL_LANGUAGES = {
    "assembly", "c", "c++", "c89", "cc65", "d", "nim", "objective-c", "zig",
}
BLOCKING_STATES = {
    "AHEAD",
    "DIVERGED",
    "DIRTY",
    "DETACHED",
    "PATH_CONFLICT",
    "REMOTE_MISMATCH",
    "BRANCH_MISMATCH",
    "MISSING_UPSTREAM",
    "UNAVAILABLE",
}


class ContractError(ValueError):
    """Raised when the desired-state contract is unsafe or inconsistent."""


def utc_now() -> str:
    return datetime.now(timezone.utc).replace(microsecond=0).isoformat().replace("+00:00", "Z")


def run_git(repository: pathlib.Path | None, *arguments: str, check: bool = True) -> subprocess.CompletedProcess[str]:
    command = ["git"]
    if repository is not None:
        command.extend(["-C", str(repository)])
    command.extend(arguments)
    result = subprocess.run(command, text=True, capture_output=True, check=False)
    if check and result.returncode != 0:
        detail = (result.stderr or result.stdout).strip().splitlines()
        raise RuntimeError(detail[-1] if detail else f"git exited {result.returncode}")
    return result


def run_git_transaction(
    repository: pathlib.Path,
    *arguments: str,
    author_name: str,
    author_email: str,
    check: bool = True,
) -> subprocess.CompletedProcess[str]:
    """Run one Git transaction with scoped identity and no config mutation."""
    if not author_name.strip() or re.search(r"[\r\n\0]", author_name):
        raise ContractError("transaction Git author name is invalid")
    if re.fullmatch(r"[^\s@]+@[^\s@]+", author_email) is None:
        raise ContractError("transaction Git author email is invalid")
    command = [
        "git", "-C", str(repository),
        "-c", f"user.name={author_name}",
        "-c", f"user.email={author_email}",
        *arguments,
    ]
    result = subprocess.run(command, text=True, capture_output=True, check=False)
    if check and result.returncode != 0:
        detail = (result.stderr or result.stdout).strip().splitlines()
        raise RuntimeError(detail[-1] if detail else f"git exited {result.returncode}")
    return result


def is_transient_git_failure(detail: str) -> bool:
    """Retry network failures, never auth or repository-state failures."""
    if re.search(
        r"auth(?:entication|orization)?|permission|forbidden|not found|dirty|ahead|diverged",
        detail,
        re.IGNORECASE,
    ):
        return False
    return bool(
        re.search(
            r"timed?\s*out|timeout|connection\s+(?:was\s+)?(?:reset|closed|aborted)|temporary failure|"
            r"could not resolve host|name resolution|http\s+50[234]",
            detail,
            re.IGNORECASE,
        )
    )


def run_git_network(
    repository: pathlib.Path | None, *arguments: str
) -> tuple[subprocess.CompletedProcess[str], int, int]:
    attempts = max(1, min(10, int(os.environ.get("HB_GIT_RETRY_ATTEMPTS", "3"))))
    timeout = max(5, min(3600, int(os.environ.get("HB_GIT_TIMEOUT_SECONDS", "300"))))
    command = ["git"]
    if repository is not None:
        command.extend(["-C", str(repository)])
    command.extend(arguments)
    result: subprocess.CompletedProcess[str] | None = None
    started = time.monotonic()
    for attempt in range(1, attempts + 1):
        try:
            result = subprocess.run(
                command,
                text=True,
                capture_output=True,
                check=False,
                timeout=timeout,
            )
        except subprocess.TimeoutExpired:
            result = subprocess.CompletedProcess(
                command, 124, stdout="", stderr=f"operation timed out after {timeout}s"
            )
        detail = (result.stderr or result.stdout).strip()
        if result.returncode == 0 or attempt == attempts or not is_transient_git_failure(detail):
            return result, attempt, int((time.monotonic() - started) * 1000)
        delay = min(3.0, 0.25 * (2 ** (attempt - 1)))
        time.sleep(delay + random.uniform(0, delay / 4))
    assert result is not None
    return result, attempts, int((time.monotonic() - started) * 1000)


def network_attempt(
    operation: str,
    result: subprocess.CompletedProcess[str],
    attempts: int,
    duration_ms: int,
) -> dict:
    detail = (result.stderr or result.stdout).strip().splitlines()
    evidence = detail[-1][:512] if detail and result.returncode != 0 else "N/A"
    if evidence != "N/A":
        evidence = evidence.replace(str(pathlib.Path.home()), "<home>")
        evidence = re.sub(
            r"(?i)\b(https?://)[^/@\s]+:[^@\s]+@",
            r"\1<redacted>@",
            evidence,
        )
        evidence = re.sub(
            r"(?i)\b(token|password|secret)=([^\s&]+)",
            r"\1=<redacted>",
            evidence,
        )
    status = (
        "Succeeded"
        if result.returncode == 0
        else "TimedOut"
        if result.returncode == 124
        else "Failed"
    )
    return {
        "operation": operation,
        "attemptCount": attempts,
        "durationMs": duration_ms,
        "exitCode": result.returncode,
        "status": status,
        "sanitizedEvidence": evidence,
        "nextAction": (
            "N/A"
            if result.returncode == 0
            else "Remote-Zugriff prüfen und den Lauf erneut ausführen / review remote access and rerun."
        ),
    }


def normalize_remote(remote: str) -> str:
    value = remote.strip().replace("\\", "/")
    parsed = urlsplit(value)
    if parsed.scheme in {"http", "https"}:
        if parsed.username or parsed.password:
            raise ContractError("remote URLs with embedded credentials are forbidden")
        value = f"{parsed.scheme.lower()}://{parsed.netloc.lower()}{parsed.path}"
    return value.rstrip("/").removesuffix(".git").lower()


def validate_relative_path(raw: object) -> pathlib.PurePosixPath:
    if not isinstance(raw, str) or not raw or "\\" in raw or raw.startswith("/"):
        raise ContractError(f"unsafe HOME-relative path: {raw!r}")
    value = pathlib.PurePosixPath(raw)
    if any(part in {"", ".", ".."} for part in value.parts):
        raise ContractError(f"unsafe HOME-relative path: {raw!r}")
    return value


# CI budget governance deliberately uses a small, dependency-free validator.
# JSON Schema remains the review contract; these checks enforce the security-
# relevant closed-world boundaries at runtime without adding a package.
CI_PROFILE_ORDER = (
    "public-canary",
    "public-product",
    "private-product",
    "private-governance-scaffold",
    "public-preset",
)
CI_PATH_CATEGORIES = {
    "build", "security", "governance", "dependency", "product", "documentation"
}


def canonical_json_hash(value: object) -> str:
    encoded = json.dumps(
        value, ensure_ascii=False, sort_keys=True, separators=(",", ":")
    ).encode("utf-8")
    return hashlib.sha256(encoded).hexdigest()


# Stage B keeps its delivery contracts separate from the historical Stage-A
# contracts.  The fixed map is a review boundary: callers cannot select an
# arbitrary schema path or silently mix document generations.
STAGE_B_SCHEMA_VERSIONS = {
    "fleet-terminal-evidence": "1.1",
    "repository-rollout-result": "1.1",
    "stage-b-rollout-plan": "1.1",
    "stage-b-ruleset-plan": "1.1",
    "stage-b-run-state": "1.1",
}
STAGE_B_MUTABLE_PLAN_FIELDS = {
    "authorityBinding", "currentAction", "currentRepositoryId", "currentWaveId",
    "lastSafeBoundary", "nextAction", "resumeCount", "status", "stop",
}
STAGE_B_PROVIDER_HOSTS = {"api.github.com", "github.com"}
STAGE_B_EXIT_CODES = {"Success": 0, "Blocked": 1, "Failed": 2, "Usage": 3, "Interrupted": 130}


def normalized_file_bytes(path: pathlib.Path) -> bytes:
    """Return strict UTF-8 bytes with BOM removed and line endings normalized."""
    raw = path.read_bytes()
    if raw.startswith(b"\xef\xbb\xbf"):
        raw = raw[3:]
    text = raw.decode("utf-8", errors="strict")
    if "\0" in text:
        raise ContractError(f"binary NUL is forbidden: {path}")
    return text.replace("\r\n", "\n").replace("\r", "\n").encode("utf-8")


def normalized_file_sha256(path: pathlib.Path) -> str:
    return hashlib.sha256(normalized_file_bytes(path)).hexdigest()


def require_stage_b_schema_match(accepted: pathlib.Path, runtime: pathlib.Path) -> str:
    accepted_hash = normalized_file_sha256(accepted)
    runtime_hash = normalized_file_sha256(runtime)
    if accepted_hash != runtime_hash or accepted.read_bytes() != runtime.read_bytes():
        raise ContractError(f"Stage-B schema drift: {accepted.name}")
    return accepted_hash


def load_stage_b_schema_contracts(repository_root: pathlib.Path) -> dict:
    root = repository_root.resolve()
    accepted_root = root / "specs/030-stage-b-rollout/contracts"
    runtime_root = root / "scripts/config"
    hashes: dict[str, str] = {}
    drift: list[str] = []
    for document_type in STAGE_B_SCHEMA_VERSIONS:
        name = f"{document_type}.schema.json"
        try:
            hashes[document_type] = require_stage_b_schema_match(
                accepted_root / name, runtime_root / name
            )
        except (ContractError, OSError, UnicodeError, json.JSONDecodeError):
            drift.append(document_type)
    if drift:
        raise ContractError(f"Stage-B schema drift: {', '.join(sorted(drift))}")
    return {"versions": dict(STAGE_B_SCHEMA_VERSIONS), "hashes": hashes, "drift": []}


def validate_stage_b_document_set(versions: dict) -> None:
    validate_stage_b_closed_world(
        versions, set(STAGE_B_SCHEMA_VERSIONS), "Stage-B document versions"
    )
    if versions != STAGE_B_SCHEMA_VERSIONS:
        raise ContractError(
            f"Stage-B document version mix is forbidden: expected {STAGE_B_SCHEMA_VERSIONS!r}"
        )


def validate_stage_b_closed_world(value: dict, expected: set[str], label: str) -> None:
    if not isinstance(value, dict):
        raise ContractError(f"{label} must be an object")
    unknown = set(value) - expected
    missing = expected - set(value)
    if unknown or missing:
        raise ContractError(
            f"{label} fields differ; missing={sorted(missing)}, unknown={sorted(unknown)}"
        )


def require_stage_b_plan_binding(value: dict, expected_plan_sha256: str) -> None:
    actual = value.get("planSha256") if isinstance(value, dict) else None
    if actual != expected_plan_sha256 or not re.fullmatch(r"[0-9a-f]{64}", str(actual)):
        raise ContractError("direct planSha256 binding does not match")


def require_stage_b_run_path_binding(value: dict, expected_run_id: str) -> None:
    if not isinstance(value, dict) or value.get("runId") != expected_run_id:
        raise ContractError("runId binding does not match")
    raw_path = value.get("path")
    if not isinstance(raw_path, str) or raw_path.startswith("/") or ".." in pathlib.PurePosixPath(raw_path).parts:
        raise ContractError("Stage-B evidence path is unsafe")
    expected_prefix = f".specify/runtime/autonomous-routing/{expected_run_id}/stage-b/"
    if not raw_path.startswith(expected_prefix) or any(character in raw_path for character in "\0\r\n"):
        raise ContractError("Stage-B evidence path is not bound to runId")


def validate_stage_b_plan_state_separation(plan: dict) -> None:
    if not isinstance(plan, dict):
        raise ContractError("Stage-B plan must be an object")
    mutable = sorted(set(plan) & STAGE_B_MUTABLE_PLAN_FIELDS)
    if mutable:
        raise ContractError(f"mutable state fields are forbidden in immutable plan: {mutable}")


def _resolve_local_schema_ref(root_schema: dict, reference: str) -> dict:
    if not reference.startswith("#/"):
        raise ContractError(f"external JSON Schema reference is forbidden: {reference}")
    current: object = root_schema
    for part in reference[2:].split("/"):
        if not isinstance(current, dict) or part not in current:
            raise ContractError(f"unknown JSON Schema reference: {reference}")
        current = current[part]
    if not isinstance(current, dict):
        raise ContractError(f"JSON Schema reference is not an object: {reference}")
    return current


def validate_stage_b_schema_instance(
    value: object, schema: dict, *, root_schema: dict | None = None, label: str = "document"
) -> None:
    """Validate the dependency-free JSON-Schema subset used by Stage B."""
    root = root_schema or schema
    if "$ref" in schema:
        validate_stage_b_schema_instance(
            value, _resolve_local_schema_ref(root, schema["$ref"]), root_schema=root, label=label
        )
        return
    if "oneOf" in schema:
        matches = 0
        for candidate in schema["oneOf"]:
            try:
                validate_stage_b_schema_instance(value, candidate, root_schema=root, label=label)
                matches += 1
            except ContractError:
                pass
        if matches != 1:
            raise ContractError(f"{label} must match exactly one schema branch")
        return
    if "const" in schema and value != schema["const"]:
        raise ContractError(f"{label} differs from required constant")
    if "enum" in schema and value not in schema["enum"]:
        raise ContractError(f"{label} is outside the accepted enum")
    expected_type = schema.get("type")
    type_matches = {
        "object": isinstance(value, dict),
        "array": isinstance(value, list),
        "string": isinstance(value, str),
        "integer": isinstance(value, int) and not isinstance(value, bool),
        "boolean": isinstance(value, bool),
        "number": isinstance(value, (int, float)) and not isinstance(value, bool),
        "null": value is None,
    }
    if expected_type and not type_matches.get(expected_type, False):
        raise ContractError(f"{label} must be {expected_type}")
    if isinstance(value, dict):
        required = set(schema.get("required", []))
        missing = required - set(value)
        if missing:
            raise ContractError(f"{label} missing required fields: {sorted(missing)}")
        properties = schema.get("properties", {})
        if schema.get("additionalProperties") is False:
            unknown = set(value) - set(properties)
            if unknown:
                raise ContractError(f"{label} has unknown fields: {sorted(unknown)}")
        for key, child in value.items():
            if key in properties:
                validate_stage_b_schema_instance(
                    child, properties[key], root_schema=root, label=f"{label}.{key}"
                )
    elif isinstance(value, list):
        if "minItems" in schema and len(value) < schema["minItems"]:
            raise ContractError(f"{label} has too few items")
        if "maxItems" in schema and len(value) > schema["maxItems"]:
            raise ContractError(f"{label} has too many items")
        if schema.get("uniqueItems") and len({json.dumps(item, sort_keys=True) for item in value}) != len(value):
            raise ContractError(f"{label} items must be unique")
        item_schema = schema.get("items")
        if isinstance(item_schema, dict):
            for index, item in enumerate(value):
                validate_stage_b_schema_instance(
                    item, item_schema, root_schema=root, label=f"{label}[{index}]"
                )
    elif isinstance(value, str):
        if "minLength" in schema and len(value) < schema["minLength"]:
            raise ContractError(f"{label} is too short")
        if "maxLength" in schema and len(value) > schema["maxLength"]:
            raise ContractError(f"{label} is too long")
        if "pattern" in schema and re.fullmatch(schema["pattern"], value) is None:
            raise ContractError(f"{label} does not match its pattern")
        if schema.get("format") == "uuid":
            try:
                parsed = uuid.UUID(value)
            except ValueError as exc:
                raise ContractError(f"{label} is not a UUID") from exc
            if parsed.int == 0:
                raise ContractError(f"{label} must not be the zero UUID")
        if schema.get("format") == "date-time":
            try:
                datetime.fromisoformat(value.replace("Z", "+00:00"))
            except ValueError as exc:
                raise ContractError(f"{label} is not an RFC3339 date-time") from exc
    elif isinstance(value, int) and not isinstance(value, bool):
        if "minimum" in schema and value < schema["minimum"]:
            raise ContractError(f"{label} is below minimum")
        if "maximum" in schema and value > schema["maximum"]:
            raise ContractError(f"{label} exceeds maximum")


def load_stage_b_document(path: pathlib.Path, schema_path: pathlib.Path) -> dict:
    try:
        value = json.loads(normalized_file_bytes(path).decode("utf-8"))
        schema = json.loads(normalized_file_bytes(schema_path).decode("utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError(f"Stage-B document is not strict UTF-8 JSON: {exc}") from exc
    if not isinstance(value, dict) or not isinstance(schema, dict):
        raise ContractError("Stage-B document and schema roots must be objects")
    validate_stage_b_schema_instance(value, schema)
    return value


def _redact_stage_b_text(text: str, limit: int = 4096) -> str:
    sanitized = text.replace(str(pathlib.Path.home()), "<home>")
    sanitized = re.sub(r"(?i)\b(token|password|secret|authorization)\s*[:=]\s*[^\s,;]+", r"\1=<redacted>", sanitized)
    sanitized = re.sub(r"(?i)(https?://)[^/@\s]+:[^@\s]+@", r"\1<redacted>@", sanitized)
    return sanitized[:limit]


def _restrict_stage_b_evidence_permissions(
    path: pathlib.Path,
    *,
    platform_name: str | None = None,
    runner=subprocess.run,
) -> str:
    """Apply an owner-only evidence boundary before the atomic replace."""
    selected_platform = os.name if platform_name is None else platform_name
    if selected_platform != "nt":
        os.chmod(path, 0o600)
        return "POSIX-0600"

    identity = runner(
        ["whoami.exe", "/user", "/fo", "csv", "/nh"],
        text=True,
        capture_output=True,
        check=False,
        timeout=10,
    )
    sid_match = re.search(r"\bS-\d(?:-\d+){1,}\b", identity.stdout or "")
    if identity.returncode != 0 or sid_match is None:
        detail = _redact_stage_b_text(identity.stderr or identity.stdout or "identity unavailable")
        raise ContractError(f"Windows evidence identity resolution failed: {detail}")
    sid = sid_match.group(0)
    # chmod(0600) maps back to 0666 on Windows and therefore cannot prove an
    # owner-only boundary. Rebuild the access-rule set because disabling
    # inheritance alone can retain explicit SYSTEM, Administrators, or
    # OWNER-RIGHTS entries inherited when the temporary file was created.
    acl_script = (
        "$ErrorActionPreference='Stop';Set-StrictMode -Version Latest;"
        "$evidencePath=[Environment]::GetEnvironmentVariable('STAGE_B_EVIDENCE_PATH');"
        "$sidValue=[Environment]::GetEnvironmentVariable('STAGE_B_EVIDENCE_SID');"
        "$principal=[Security.Principal.SecurityIdentifier]::new($sidValue);"
        "$acl=Get-Acl -LiteralPath $evidencePath;"
        "$acl.SetAccessRuleProtection($true,$false);"
        "foreach($rule in @($acl.Access)){[void]$acl.RemoveAccessRuleSpecific($rule)};"
        "$ownerRule=[Security.AccessControl.FileSystemAccessRule]::new("
        "$principal,[Security.AccessControl.FileSystemRights]::Modify,"
        "[Security.AccessControl.AccessControlType]::Allow);"
        "[void]$acl.AddAccessRule($ownerRule);"
        "Set-Acl -LiteralPath $evidencePath -AclObject $acl"
    )
    acl_environment = os.environ.copy()
    acl_environment["STAGE_B_EVIDENCE_PATH"] = str(path)
    acl_environment["STAGE_B_EVIDENCE_SID"] = sid
    acl = runner(
        ["pwsh.exe", "-NoProfile", "-NonInteractive", "-Command", acl_script],
        text=True,
        capture_output=True,
        check=False,
        timeout=10,
        env=acl_environment,
    )
    if acl.returncode != 0:
        detail = _redact_stage_b_text(acl.stderr or acl.stdout or "ACL update failed")
        raise ContractError(f"Windows evidence DACL restriction failed: {detail}")
    return sid


def _sync_stage_b_evidence_metadata(
    path: pathlib.Path, *, platform_name: str | None = None
) -> None:
    """Flush the strongest supported post-replace metadata boundary."""
    selected_platform = os.name if platform_name is None else platform_name
    if selected_platform == "nt":
        # Windows rejects POSIX-style directory descriptors. Reopen and flush
        # the published file after os.replace so the atomic replacement and
        # restrictive final mode remain intact without relying on that handle.
        # os.fsync() on Windows rejects a read-only descriptor with EBADF, so
        # request write access without writing any additional bytes.
        sync_path = path
        flags = os.O_RDWR | getattr(os, "O_BINARY", 0)
    else:
        # POSIX filesystems need the parent directory flush to make the rename
        # durable independently from the already flushed temporary file.
        sync_path = path.parent
        flags = os.O_RDONLY | getattr(os, "O_DIRECTORY", 0)
    descriptor = os.open(sync_path, flags)
    try:
        os.fsync(descriptor)
    finally:
        os.close(descriptor)


def publish_stage_b_evidence(
    path: pathlib.Path, value: dict, schema_path: pathlib.Path, *, temporary_primary: bool = False
) -> str:
    """Schema-check and atomically publish one redacted Stage-B JSON record."""
    schema = json.loads(normalized_file_bytes(schema_path).decode("utf-8"))
    validate_stage_b_schema_instance(value, schema)
    encoded = (json.dumps(value, ensure_ascii=False, sort_keys=True, indent=2) + "\n").encode("utf-8")
    decoded = encoded.decode("utf-8")
    if _redact_stage_b_text(decoded) != decoded:
        raise ContractError("Stage-B evidence contains restricted data")
    path.parent.mkdir(mode=0o700, parents=True, exist_ok=True)
    # The caller supplies the evidence root; reject a direct symlink boundary
    # without treating platform aliases such as macOS /var as attacker input.
    if path.is_symlink() or path.parent.is_symlink():
        raise ContractError("Stage-B evidence path crosses a symlink")
    descriptor, temporary_name = tempfile.mkstemp(prefix=f".{path.name}.", dir=path.parent)
    temporary = pathlib.Path(temporary_name)
    try:
        os.fchmod(descriptor, 0o600)
        with os.fdopen(descriptor, "wb", closefd=True) as stream:
            stream.write(encoded)
            stream.flush()
            os.fsync(stream.fileno())
        # Restrict the complete temporary file before it becomes visible at
        # the final path. A DACL failure therefore leaves prior evidence intact.
        _restrict_stage_b_evidence_permissions(temporary)
        os.replace(temporary, path)
        _sync_stage_b_evidence_metadata(path)
    finally:
        if temporary.exists():
            temporary.unlink()
    if temporary_primary and "primary" not in path.parts:
        raise ContractError("temporary Primary evidence must use the primary namespace")
    return normalized_file_sha256(path)


def validate_stage_b_provider_identity(
    provider_repository_id: object, slug: object, host: str = "api.github.com"
) -> tuple[str, str]:
    repository_id = str(provider_repository_id)
    if re.fullmatch(r"[1-9][0-9]*", repository_id) is None:
        raise ContractError("provider repository ID must be a positive integer")
    if not isinstance(slug, str) or re.fullmatch(r"[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+", slug) is None:
        raise ContractError("provider repository slug is invalid")
    if host not in STAGE_B_PROVIDER_HOSTS:
        raise ContractError("provider host is outside the fixed allowlist")
    return repository_id, slug


def build_stage_b_gh_read_args(slug: str, endpoint: str) -> list[str]:
    _, validated_slug = validate_stage_b_provider_identity("1", slug)
    if endpoint.startswith(("-", "http://", "https://")) or ".." in pathlib.PurePosixPath(endpoint).parts:
        raise ContractError("free provider URLs and traversal are forbidden")
    if re.fullmatch(r"[A-Za-z0-9_./{}=-]+", endpoint) is None:
        raise ContractError("provider endpoint contains unsafe characters")
    return ["gh", "api", f"repos/{validated_slug}/{endpoint.lstrip('/')}", "--method", "GET"]


def build_stage_b_gh_write_args(
    slug: str, endpoint: str, method: str, input_path: pathlib.Path
) -> list[str]:
    if method not in {"POST", "PATCH", "PUT", "DELETE"}:
        raise ContractError("provider write method is invalid")
    arguments = build_stage_b_gh_read_args(slug, endpoint)[:-2]
    if not input_path.is_file() or input_path.is_symlink():
        raise ContractError("provider write input must be a regular file")
    return [*arguments, "--method", method, "--input", str(input_path)]


def classify_stage_b_provider_failure(exit_code: int, detail: str) -> str:
    if exit_code == 0:
        return "Passed"
    lowered = detail.lower()
    if exit_code == 124 or re.search(
        r"timeout|connection (?:was )?(?:reset|closed|aborted)|could not resolve|"
        r"gnutls_handshake|tls connection was non-properly terminated|\b50[234]\b",
        lowered,
    ):
        return "TransientRead"
    if re.search(r"billing|quota|rate limit|recent account payments|spending limit", lowered):
        return "BillingOrQuotaRefusal"
    if re.search(r"\b403\b|\b404\b|forbidden|not found", lowered):
        return "ProviderRefusal"
    return "TechnicalFailure"


def run_stage_b_provider_read(
    slug: str, endpoint: str, *, runner=subprocess.run, attempts: int = 3, timeout: int = 30
) -> dict:
    command = build_stage_b_gh_read_args(slug, endpoint)
    bounded_attempts = max(1, min(5, attempts))
    last: subprocess.CompletedProcess[str] | None = None
    for attempt in range(1, bounded_attempts + 1):
        try:
            last = runner(command, text=True, capture_output=True, check=False, timeout=timeout)
        except subprocess.TimeoutExpired:
            last = subprocess.CompletedProcess(command, 124, stdout="", stderr="timeout")
        classification = classify_stage_b_provider_failure(
            last.returncode, (last.stderr or last.stdout)[:4096]
        )
        if classification != "TransientRead" or attempt == bounded_attempts:
            response = {
                "command": command,
                "attemptCount": attempt,
                "exitCode": last.returncode,
                "classification": classification,
                "diagnostic": _redact_stage_b_text(last.stderr or last.stdout),
            }
            if last.returncode == 0:
                try:
                    response["value"] = json.loads(last.stdout)
                except json.JSONDecodeError:
                    response["classification"] = "TechnicalFailure"
            return response
    raise AssertionError("bounded provider loop did not return")


STAGE_B_MAX_WRITERS = 3
STAGE_B_MAX_READERS = 4


def stage_b_concurrency_limit(operation: str) -> int:
    """Return the audited ceiling for mutating and read-only fleet work."""
    if operation == "write":
        return STAGE_B_MAX_WRITERS
    if operation == "read":
        return STAGE_B_MAX_READERS
    raise ContractError("Stage-B concurrency operation must be read or write")


def run_stage_b_provider_write(
    slug: str,
    endpoint: str,
    method: str,
    input_path: pathlib.Path,
    reconcile_endpoint: str,
    *,
    runner=subprocess.run,
    reader=run_stage_b_provider_read,
    attempts: int = 3,
    timeout: int = 30,
) -> dict:
    """Retry a provider write only after exact read-back proves it absent.

    A transient response can hide a successful remote mutation. The read-back
    therefore runs before another write and accepts an already converged value.
    """
    command = build_stage_b_gh_write_args(slug, endpoint, method, input_path)
    try:
        desired = json.loads(input_path.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError("provider write input must contain valid JSON") from exc
    desired_hash = canonical_json_hash(desired)
    bounded_attempts = max(1, min(3, attempts))
    for attempt in range(1, bounded_attempts + 1):
        try:
            completed = runner(command, text=True, capture_output=True, check=False, timeout=timeout)
        except subprocess.TimeoutExpired:
            completed = subprocess.CompletedProcess(command, 124, stdout="", stderr="timeout")
        classification = classify_stage_b_provider_failure(
            completed.returncode, (completed.stderr or completed.stdout)[:4096]
        )
        if completed.returncode == 0:
            return {
                "command": command, "attemptCount": attempt, "exitCode": 0,
                "classification": "Passed", "reconciled": False, "diagnostic": "N/A",
            }
        if classification != "TransientRead":
            return {
                "command": command, "attemptCount": attempt,
                "exitCode": completed.returncode, "classification": classification,
                "reconciled": False,
                "diagnostic": _redact_stage_b_text(completed.stderr or completed.stdout),
            }
        read_back = reader(slug, reconcile_endpoint, attempts=3, timeout=timeout)
        if read_back.get("classification") == "Passed":
            observed = read_back.get("value")
            if observed is None:
                try:
                    observed = json.loads(read_back.get("diagnostic", ""))
                except (TypeError, json.JSONDecodeError):
                    observed = None
            if observed is not None and canonical_json_hash(observed) == desired_hash:
                return {
                    "command": command, "attemptCount": attempt,
                    "exitCode": 0, "classification": "Passed",
                    "reconciled": True, "diagnostic": "N/A",
                }
            return {
                "command": command, "attemptCount": attempt,
                "exitCode": completed.returncode, "classification": "TechnicalFailure",
                "reconciled": False,
                "diagnostic": "provider read-back differs from the requested state",
            }
        read_back_detail = str(read_back.get("diagnostic", ""))
        if not (
            read_back.get("classification") == "ProviderRefusal"
            and re.search(r"\b404\b|not found", read_back_detail, re.IGNORECASE)
        ):
            return {
                "command": command, "attemptCount": attempt,
                "exitCode": completed.returncode, "classification": "TransportUnknown",
                "reconciled": False,
                "diagnostic": _redact_stage_b_text(completed.stderr or completed.stdout),
            }
        if attempt == bounded_attempts:
            return {
                "command": command, "attemptCount": attempt,
                "exitCode": completed.returncode, "classification": "TransportUnknown",
                "reconciled": False,
                "diagnostic": _redact_stage_b_text(completed.stderr or completed.stdout),
            }
        delay = min(3.0, 0.25 * (2 ** (attempt - 1)))
        time.sleep(delay + random.uniform(0, delay / 4))
    raise AssertionError("bounded provider write loop did not return")


def execute_stage_b_action(args: argparse.Namespace) -> int:
    action = args.action.capitalize()
    if action not in {"Preflight", "Validate", "Deliver", "Resume", "Verify"}:
        print("STAGE_B\tUSAGE\tunknown action", file=sys.stderr)
        return STAGE_B_EXIT_CODES["Usage"]
    try:
        for value in (args.repository_id, args.profile_id):
            if value != "N/A":
                validate_ci_input_component(value)
        if action == "Preflight":
            return execute_stage_b_preflight(args)
        decision = "Preview" if args.dry_run and action in {"Deliver", "Resume"} else action
        fields = [
            ("Run-ID / Run ID", args.run_id), ("Autoritaet / Authority", args.delivery_mode),
            ("Welle / Wave", args.wave_id), ("Repository-ID", args.repository_id),
            ("Profil / Profile", args.profile_id), ("Entscheidung / Decision", decision),
            ("Status", "Passed"), ("Blocker", "N/A"),
            ("Naechste Aktion / Next action", "N/A"),
        ]
        for name, value in fields:
            print(f"{name}: {value}")
        return STAGE_B_EXIT_CODES["Success"]
    except KeyboardInterrupt:
        print("STAGE_B\tINTERRUPTED\tcontrolled stop", file=sys.stderr)
        return STAGE_B_EXIT_CODES["Interrupted"]
    except CIGateBlocked as exc:
        print(f"STAGE_B\tBLOCKED\t{exc}", file=sys.stderr)
        return STAGE_B_EXIT_CODES["Blocked"]
    except (ContractError, OSError, RuntimeError, json.JSONDecodeError) as exc:
        print(f"STAGE_B\tFAILED\t{exc}", file=sys.stderr)
        return STAGE_B_EXIT_CODES["Failed"]


STAGE_B_RUN_ID = "954ff259-ffed-44a8-883f-28742b031a9b"
STAGE_B_G3_REVIEWED_HEAD = "e1ff2a0b5146604b2a71a20576dbd4341d618121"
STAGE_B_G3_MERGE_COMMIT = "b6a0d81760e9ef68a058e5d9578073b5e78b61b8"
STAGE_B_WAVES = (
    "public-canaries", "public-products", "private-products",
    "private-governance-scaffold", "public-presets",
)
STAGE_B_PROFILE_WAVE = {
    "public-canary": "public-canaries",
    "public-product": "public-products",
    "private-product": "private-products",
    "private-governance-scaffold": "private-governance-scaffold",
    "public-preset": "public-presets",
}
STAGE_B_PROVIDER_MANAGED_WORKFLOW_PATHS = frozenset({
    "dynamic/agents/copilot-pull-request-reviewer",
    "dynamic/copilot-pull-request-reviewer/copilot-pull-request-reviewer",
    "dynamic/copilot-swe-agent/copilot",
    "dynamic/dependabot/dependabot-updates",
})
STAGE_B_ALLOWED_FORKS = {
    "cc65": ("hindermath/cc65", "cc65/cc65"),
    "tvision": ("hindermath/tvision", "magiblot/tvision"),
}


def _atomic_stage_b_json(path: pathlib.Path, value: dict) -> str:
    encoded = (json.dumps(value, ensure_ascii=False, sort_keys=True, indent=2) + "\n").encode()
    path.parent.mkdir(mode=0o700, parents=True, exist_ok=True)
    descriptor, temporary_name = tempfile.mkstemp(prefix=f".{path.name}.", dir=path.parent)
    temporary = pathlib.Path(temporary_name)
    try:
        os.fchmod(descriptor, 0o600)
        with os.fdopen(descriptor, "wb", closefd=True) as stream:
            stream.write(encoded)
            stream.flush()
            os.fsync(stream.fileno())
        os.replace(temporary, path)
        os.chmod(path, 0o600)
    finally:
        temporary.unlink(missing_ok=True)
    return normalized_file_sha256(path)


def _stage_b_git_object_sha(object_type: str, content: bytes) -> str:
    header = f"{object_type} {len(content)}\0".encode("ascii")
    return hashlib.sha1(header + content).hexdigest()


def _stage_b_tree_sha(entries: dict[str, tuple[str, str]]) -> str:
    """Calculate a Git tree without writing an object into a target repository."""
    tree: dict[str, object] = {}
    for path, (mode, object_sha) in entries.items():
        parts = pathlib.PurePosixPath(path).parts
        if not parts or any(part in {"", ".", ".."} for part in parts):
            raise ContractError(f"unsafe Stage-B tree path: {path!r}")
        node = tree
        for part in parts[:-1]:
            child = node.setdefault(part, {})
            if not isinstance(child, dict):
                raise ContractError(f"Stage-B tree collision: {path}")
            node = child
        node[parts[-1]] = (mode, object_sha)

    def encode(node: dict[str, object]) -> str:
        encoded_entries: list[tuple[bytes, bytes]] = []
        for name, value in node.items():
            if isinstance(value, dict):
                mode, object_sha, sort_name = "40000", encode(value), f"{name}/"
            else:
                mode, object_sha = value
                sort_name = name
            record = mode.encode("ascii") + b" " + name.encode("utf-8") + b"\0"
            record += bytes.fromhex(object_sha)
            encoded_entries.append((sort_name.encode("utf-8"), record))
        content = b"".join(record for _, record in sorted(encoded_entries))
        return _stage_b_git_object_sha("tree", content)

    return encode(tree)


def _stage_b_git_tree_entries(repository: pathlib.Path, head: str) -> dict[str, tuple[str, str]]:
    result = run_git(repository, "ls-tree", "-rz", "--full-tree", "-r", head)
    entries: dict[str, tuple[str, str]] = {}
    for raw in result.stdout.split("\0"):
        if not raw:
            continue
        metadata, path = raw.split("\t", 1)
        mode, object_type, object_sha = metadata.split(" ", 2)
        if object_type not in {"blob", "commit"} or not re.fullmatch(r"[0-9a-f]{40}", object_sha):
            raise ContractError(f"unsupported Stage-B Git tree entry: {path}")
        entries[path] = (mode, object_sha)
    return entries


def _stage_b_environment_hash(
    repository: pathlib.Path, repository_id: str, profile_id: str
) -> str:
    for relative in (".specify/memory/constitution.md", "constitution.md"):
        candidate = repository / relative
        if candidate.is_file() and not candidate.is_symlink():
            return canonical_json_hash({
                "repositoryId": repository_id,
                "contextPath": relative,
                "contextSha256": normalized_file_sha256(candidate),
            })
    if profile_id == "public-preset":
        # Content-only preset repositories intentionally have no project
        # constitution. Bind both exact package surfaces so that this narrow
        # equivalent-context fallback cannot weaken other repository profiles.
        context_paths = ("preset.yml", "README.md")
        candidates = [repository / relative for relative in context_paths]
        if all(candidate.is_file() and not candidate.is_symlink() for candidate in candidates):
            return canonical_json_hash({
                "repositoryId": repository_id,
                "contextType": "PublicPresetEquivalent",
                "contexts": [
                    {
                        "contextPath": relative,
                        "contextSha256": normalized_file_sha256(candidate),
                    }
                    for relative, candidate in zip(context_paths, candidates, strict=True)
                ],
            })
    raise ContractError(f"environment registry context is missing: {repository_id}")


def _stage_b_local_repository_root(
    repository_root: pathlib.Path, manifest_target: dict | None
) -> pathlib.Path:
    if manifest_target is None:
        return repository_root
    fleet_root = pathlib.Path(os.environ.get("HB_STAGE_B_FLEET_ROOT", str(pathlib.Path.home())))
    return (fleet_root / validate_relative_path(manifest_target["path"])).resolve()


def _stage_b_validate_local_repository(
    repository: pathlib.Path, identity: dict, expected_remote: str
) -> None:
    if not repository.is_dir() or not (repository / ".git").exists():
        raise ContractError(f"local target repository is unavailable: {identity['repositoryId']}")
    if run_git(repository, "status", "--porcelain=v1", "--untracked-files=all").stdout:
        raise ContractError(f"dirty target blocks Stage B: {identity['repositoryId']}")
    branch = run_git(repository, "branch", "--show-current").stdout.strip()
    head = run_git(repository, "rev-parse", "HEAD").stdout.strip()
    remote = run_git(repository, "remote", "get-url", "origin").stdout.strip()
    if branch != identity["defaultBranch"]:
        raise ContractError(f"default branch drift blocks Stage B: {identity['repositoryId']}")
    if head != identity["defaultHead"]:
        raise ContractError(f"stale target head blocks Stage B: {identity['repositoryId']}")
    if normalize_remote(remote) != normalize_remote(expected_remote):
        raise ContractError(f"remote identity drift blocks Stage B: {identity['repositoryId']}")


def _stage_b_github_get_json(slug: str, endpoint: str = "") -> object:
    validate_stage_b_provider_identity("1", slug)
    suffix = endpoint.lstrip("/")
    if suffix and (
        suffix.startswith(("-", "http://", "https://"))
        or ".." in pathlib.PurePosixPath(suffix).parts
        or re.fullmatch(r"[A-Za-z0-9_./{}=?&%-]+", suffix) is None
    ):
        raise ContractError("Stage-B provider endpoint is unsafe")
    target = f"repos/{slug}" + (f"/{suffix}" if suffix else "")
    command = ["gh", "api", target, "--method", "GET"]
    for attempt in range(1, 4):
        try:
            completed = subprocess.run(
                command, text=True, capture_output=True, check=False, timeout=30, shell=False
            )
        except subprocess.TimeoutExpired:
            completed = subprocess.CompletedProcess(command, 124, "", "timeout")
        if completed.returncode == 0:
            try:
                return json.loads(completed.stdout)
            except json.JSONDecodeError as exc:
                raise ContractError(f"GitHub GET returned invalid JSON for {target}") from exc
        classification = classify_stage_b_provider_failure(
            completed.returncode, completed.stderr or completed.stdout
        )
        if classification != "TransientRead" or attempt == 3:
            detail = _redact_stage_b_text(completed.stderr or completed.stdout, 512)
            raise ContractError(f"GitHub read-only inventory failed for {target}: {detail}")
        time.sleep(0.25 * (2 ** (attempt - 1)))
    raise AssertionError("bounded Stage-B GitHub retry loop exhausted")


def validate_stage_b_provider_lifecycle(
    metadata: dict, repository_id: str, slug: str
) -> None:
    archived = metadata.get("archived")
    fork = metadata.get("fork")
    if not isinstance(archived, bool) or not isinstance(fork, bool):
        raise ContractError(f"provider lifecycle metadata is invalid: {repository_id}")
    if archived:
        raise ContractError(f"archived target blocks Stage B: {repository_id}")
    if not fork:
        return
    allowed = STAGE_B_ALLOWED_FORKS.get(repository_id)
    parent = metadata.get("parent")
    parent_name = str(parent.get("full_name", "")) if isinstance(parent, dict) else ""
    if (
        allowed is None
        or slug.lower() != allowed[0]
        or parent_name.lower() != allowed[1]
    ):
        raise ContractError(f"unbound fork target blocks Stage B: {repository_id}")


def stage_b_stable_identity(row: dict) -> dict:
    expected = {
        "repositoryId", "providerRepositoryId", "remoteIdentity", "slug",
        "profileId", "visibility", "defaultBranch", "defaultHead", "defaultTree",
        "localRepositoryRootHash", "environmentRegistryHash", "observedAt",
    }
    validate_stage_b_closed_world(row, expected, "Stage-B repository identity")
    repository_id = validate_ci_input_component(row["repositoryId"])
    provider_id, slug = validate_stage_b_provider_identity(
        row["providerRepositoryId"], row["slug"]
    )
    remote = normalize_remote(row["remoteIdentity"])
    if not remote.endswith(f"/{slug.lower()}"):
        raise ContractError(f"remote identity does not match bound slug for {repository_id}")
    if row["visibility"] not in {"public", "private"}:
        raise ContractError(f"visibility is invalid for {repository_id}")
    if not isinstance(row["defaultBranch"], str) or not row["defaultBranch"]:
        raise ContractError(f"default branch is invalid for {repository_id}")
    if row["profileId"] not in STAGE_B_PROFILE_WAVE:
        raise ContractError(f"profile is invalid for {repository_id}")
    for field, length in (("defaultHead", 40), ("defaultTree", 40)):
        if re.fullmatch(rf"[0-9a-f]{{{length}}}", str(row[field])) is None:
            raise ContractError(f"{field} is invalid for {repository_id}")
    for field in ("localRepositoryRootHash", "environmentRegistryHash"):
        if re.fullmatch(r"[0-9a-f]{64}", str(row[field])) is None:
            raise ContractError(f"{field} is invalid for {repository_id}")
    try:
        datetime.fromisoformat(str(row["observedAt"]).replace("Z", "+00:00"))
    except ValueError as exc:
        raise ContractError(f"observedAt is invalid for {repository_id}") from exc
    return {
        "repositoryId": repository_id,
        "providerRepositoryId": provider_id,
        "remoteIdentity": remote,
        "slug": slug.lower(),
        "profileId": row["profileId"],
        "visibility": row["visibility"],
        "defaultBranch": row["defaultBranch"],
        "defaultHead": row["defaultHead"],
        "defaultTree": row["defaultTree"],
        "localRepositoryRootHash": row["localRepositoryRootHash"],
        "environmentRegistryHash": row["environmentRegistryHash"],
        "observedAt": row["observedAt"],
    }


def validate_stage_b_git_state(row: dict) -> None:
    expected = {
        "repositoryId", "status", "ahead", "behind", "localHead", "remoteHead",
        "defaultBranch", "observedDefaultBranch",
    }
    validate_stage_b_closed_world(row, expected, "Stage-B Git state")
    repository_id = validate_ci_input_component(row["repositoryId"])
    if row["status"] not in ([], ""):
        raise ContractError(f"dirty target blocks Stage B: {repository_id}")
    if row["ahead"] != 0 or row["behind"] != 0:
        raise ContractError(f"divergent target blocks Stage B: {repository_id}")
    if row["localHead"] != row["remoteHead"]:
        raise ContractError(f"stale target head blocks Stage B: {repository_id}")
    if row["defaultBranch"] != row["observedDefaultBranch"]:
        raise ContractError(f"default branch drift blocks Stage B: {repository_id}")


def _stage_b_gate_refs(gate_set: dict) -> list[dict]:
    return [
        {
            "gateId": gate["gateId"],
            "commandToken": " ".join([gate["executable"], *gate["arguments"]]),
            "runnerOrPlatformToken": "LocalTarget",
        }
        for gate in sorted(gate_set["gates"], key=lambda item: (item["order"], item["gateId"]))
    ]


def _stage_b_target_change_input(
    repository: pathlib.Path,
    identity: dict,
    profile: dict,
    contracts: dict,
    workflow_template: pathlib.Path,
    ruleset_plan_hash: str,
    remote_gate_refs: list[dict],
) -> dict:
    repository_id = identity["repositoryId"]
    profile_id = profile["profileId"]
    baseline_head = identity["defaultHead"]
    baseline_tree = identity["defaultTree"]
    entries = _stage_b_git_tree_entries(repository, baseline_head)
    changes: list[dict] = []
    workflow_action = "Preserve" if profile_id in {"public-canary", "public-product"} else "N/A"
    if profile_id == "private-governance-scaffold":
        desired_path = ".github/workflows/home-baseline-ci-minimal-gate.yml"
        desired_content = normalized_file_bytes(workflow_template)
        desired_blob = _stage_b_git_object_sha("blob", desired_content)
        previous = entries.get(desired_path)
        if previous is None:
            workflow_action = "Add"
            changes.append({
                "path": desired_path, "action": "Add", "modeBefore": "N/A",
                "modeAfter": "100644", "blobBefore": "N/A", "blobAfter": desired_blob,
                "sourceTemplateHash": normalized_file_sha256(workflow_template),
                "semanticContractIds": ["private-governance-minimal-gate"],
            })
        elif previous != ("100644", desired_blob):
            workflow_action = "Update"
            changes.append({
                "path": desired_path, "action": "Modify", "modeBefore": previous[0],
                "modeAfter": "100644", "blobBefore": previous[1], "blobAfter": desired_blob,
                "sourceTemplateHash": normalized_file_sha256(workflow_template),
                "semanticContractIds": ["private-governance-minimal-gate"],
            })
        else:
            workflow_action = "Preserve"
    candidate_entries = dict(entries)
    for change in changes:
        candidate_entries[change["path"]] = (change["modeAfter"], change["blobAfter"])
    candidate_tree = _stage_b_tree_sha(candidate_entries) if changes else baseline_tree
    gate_set = next(
        item for item in contracts["profiles"]["gateSets"]
        if item["gateSetId"] == profile["gateSetId"]
    )
    stage_a_decision, stage_a_diff, _ = _ci_rollout_decision(profile_id)
    return {
        "repositoryId": repository_id,
        "baselineHead": baseline_head,
        "baselineTree": baseline_tree,
        "defaultBranch": identity["defaultBranch"],
        "stageAPlanHash": canonical_json_hash({
            "repositoryId": repository_id,
            "profileId": profile_id,
            "decision": stage_a_decision,
            "plannedDiff": stage_a_diff,
            "remoteGateRefs": remote_gate_refs,
        }),
        "gateSetHash": canonical_json_hash(gate_set),
        "pathContractHash": contracts["pathContractHash"],
        "changes": sorted(changes, key=lambda item: (item["path"], item["action"])),
        "candidateTree": candidate_tree,
        "workflowAction": workflow_action,
        "rulesetPlanHash": ruleset_plan_hash,
        "mergeMethod": "merge" if changes else "N/A",
        "requiredLocalGates": _stage_b_gate_refs(gate_set),
        "requiredRemoteGates": remote_gate_refs,
    }


def _stage_b_ruleset_plan_hash(
    slug: str, identity: dict, ruleset_template: pathlib.Path
) -> str:
    if identity["profileId"] != "private-governance-scaffold":
        return "N/A"
    desired = _read_ci_json(ruleset_template, "private governance ruleset")
    summaries = _stage_b_github_get_json(slug, "rulesets?per_page=100")
    if not isinstance(summaries, list):
        raise ContractError(f"ruleset inventory is invalid: {identity['repositoryId']}")
    if len(summaries) >= 100:
        raise ContractError(f"ruleset inventory pagination is incomplete: {identity['repositoryId']}")
    matching = [item for item in summaries if isinstance(item, dict) and item.get("name") == desired["rulesetName"]]
    if len(matching) > 1:
        raise ContractError(f"duplicate Stage-B ruleset identity: {identity['repositoryId']}")
    if not matching:
        return canonical_json_hash({
            "action": "Create", "repositoryId": identity["repositoryId"],
            "providerRepositoryId": identity["providerRepositoryId"],
            "defaultBranch": identity["defaultBranch"], "desired": desired,
        })
    ruleset_id = str(matching[0].get("id", ""))
    if re.fullmatch(r"[1-9][0-9]*", ruleset_id) is None:
        raise ContractError(f"ruleset provider ID is invalid: {identity['repositoryId']}")
    detail = _stage_b_github_get_json(slug, f"rulesets/{ruleset_id}")
    if not isinstance(detail, dict) or not isinstance(detail.get("rules"), list):
        raise ContractError(f"ruleset detail is incomplete: {identity['repositoryId']}")
    rules = {item.get("type"): item.get("parameters", {}) for item in detail["rules"] if isinstance(item, dict)}
    pull_request = rules.get("pull_request", {})
    checks = rules.get("required_status_checks", {})
    contexts = checks.get("required_status_checks", []) if isinstance(checks, dict) else []
    context_names = sorted(
        item.get("context") for item in contexts
        if isinstance(item, dict) and isinstance(item.get("context"), str)
    )
    observed = {
        "target": detail.get("target"), "rulesetName": detail.get("name"),
        "enforcement": detail.get("enforcement"),
        "pullRequestRequired": "pull_request" in rules,
        "requiredApprovingReviews": pull_request.get("required_approving_review_count"),
        "requiredStatusChecks": context_names,
        "strictStatusChecks": checks.get("strict_required_status_checks_policy") if isinstance(checks, dict) else None,
        "bypassActors": detail.get("bypass_actors"),
    }
    expected = {
        "target": desired["target"], "rulesetName": desired["rulesetName"],
        "enforcement": desired["enforcement"],
        "pullRequestRequired": desired["pullRequestRequired"],
        "requiredApprovingReviews": desired["requiredApprovingReviews"],
        "requiredStatusChecks": desired["requiredStatusChecks"],
        "strictStatusChecks": desired["strictStatusChecks"],
        "bypassActors": desired["bypassActors"],
    }
    if observed == expected and set(rules) == {"pull_request", "required_status_checks"}:
        return "N/A"
    return canonical_json_hash({
        "action": "Update", "repositoryId": identity["repositoryId"],
        "providerRepositoryId": identity["providerRepositoryId"],
        "defaultBranch": identity["defaultBranch"],
        "previousRulesetId": ruleset_id, "previous": observed, "desired": desired,
    })


def _stage_b_workflow_gate_refs(slug: str, profile_id: str, repository_id: str) -> list[dict]:
    response = _stage_b_github_get_json(slug, "actions/workflows?per_page=100")
    if not isinstance(response, dict) or not isinstance(response.get("workflows"), list):
        raise ContractError(f"workflow inventory is incomplete: {repository_id}")
    workflows = response["workflows"]
    if response.get("total_count") != len(workflows):
        raise ContractError(f"workflow inventory pagination is incomplete: {repository_id}")
    active = []
    for workflow in workflows:
        if not isinstance(workflow, dict):
            raise ContractError(f"workflow inventory row is invalid: {repository_id}")
        path = str(workflow.get("path", ""))
        state = workflow.get("state")
        if state != "active":
            continue
        # GitHub exposes provider-managed Copilot and Dependabot entries in the
        # workflow inventory even though they are not repository-owned files.
        # They remain outside CI-budget gate evidence; every other non-file path
        # still fails closed instead of being trusted by name or display label.
        if path in STAGE_B_PROVIDER_MANAGED_WORKFLOW_PATHS:
            continue
        if not path.startswith(".github/workflows/") or any(character in path for character in "\0\r\n"):
            raise ContractError(f"workflow path is unsafe: {repository_id}")
        stem = pathlib.PurePosixPath(path).stem.lower()
        gate_id = re.sub(r"[^a-z0-9-]+", "-", stem).strip("-")
        validate_ci_input_component(gate_id)
        active.append({
            "gateId": gate_id,
            "commandToken": f"GitHubActionsWorkflow:{path}",
            "runnerOrPlatformToken": "GitHub",
        })
    refs = sorted(active, key=lambda item: (item["gateId"], item["commandToken"]))
    if len({item["gateId"] for item in refs}) != len(refs):
        raise ContractError(f"workflow gate identity is ambiguous: {repository_id}")
    if profile_id in {"public-canary", "public-product"} and not refs:
        raise ContractError(f"required public CI workflow is missing: {repository_id}")
    return refs


def load_stage_b_live_inputs(repository_root: pathlib.Path) -> dict:
    """Read the complete current fleet/provider input set without writing anywhere."""
    root = repository_root.resolve()
    manifest_path = root / "scripts/config/agentic-workspace-fleet.json"
    profiles_path = root / "scripts/config/ci-budget-profiles.json"
    paths_path = root / "scripts/config/ci-budget-path-contracts.json"
    workflow_template = root / "scripts/templates/ci-budget-governance/private-governance-minimal-gate.yml"
    ruleset_template = root / "scripts/templates/ci-budget-governance/private-governance-ruleset.json"
    manifest = load_manifest(manifest_path)
    contracts = load_ci_budget_contracts(profiles_path, paths_path, workflow_template)
    validate_private_governance_workflow_paths(workflow_template, contracts["paths"])
    simulate_private_governance_policy(workflow_template, ruleset_template)
    authoritative = authoritative_ci_repositories(root, manifest, contracts["profiles"])
    manifest_by_id = {
        item["id"]: item for item in manifest["targets"]
        if item.get("active") is True and item.get("kind") == "git-repository"
    }
    profiles = {item["profileId"]: item for item in contracts["profiles"]["profiles"]}
    observed_at = utc_now()
    inventory: list[dict] = []
    targets: list[dict] = []
    for expected in authoritative:
        repository_id = expected["repositoryId"]
        slug = _github_repository_slug(expected["remoteIdentity"])
        metadata = _stage_b_github_get_json(slug)
        if not isinstance(metadata, dict):
            raise ContractError(f"GitHub repository metadata is invalid: {repository_id}")
        provider_id = str(metadata.get("id", ""))
        full_name = str(metadata.get("full_name", ""))
        default_branch = str(metadata.get("default_branch", ""))
        validate_stage_b_provider_lifecycle(metadata, repository_id, slug)
        if full_name.lower() != slug.lower() or default_branch != expected["defaultBranch"]:
            raise ContractError(f"provider identity/default-branch drift: {repository_id}")
        branch = _stage_b_github_get_json(slug, f"branches/{default_branch}")
        if not isinstance(branch, dict) or not isinstance(branch.get("commit"), dict):
            raise ContractError(f"provider branch metadata is incomplete: {repository_id}")
        default_head = str(branch["commit"].get("sha", ""))
        commit = _stage_b_github_get_json(slug, f"git/commits/{default_head}")
        if not isinstance(commit, dict) or not isinstance(commit.get("tree"), dict):
            raise ContractError(f"provider commit metadata is incomplete: {repository_id}")
        default_tree = str(commit["tree"].get("sha", ""))
        local_repository = _stage_b_local_repository_root(root, manifest_by_id.get(repository_id))
        identity = {
            "repositoryId": repository_id,
            "providerRepositoryId": provider_id,
            "remoteIdentity": expected["remoteIdentity"],
            "slug": slug,
            "profileId": expected["assignmentProfileId"],
            "visibility": "private" if metadata.get("private") is True else "public",
            "defaultBranch": default_branch,
            "defaultHead": default_head,
            "defaultTree": default_tree,
            "localRepositoryRootHash": canonical_json_hash({
                "repositoryId": repository_id,
                "manifestPath": manifest_by_id.get(repository_id, {}).get("path", "executing-level0"),
            }),
            "environmentRegistryHash": _stage_b_environment_hash(
                local_repository, repository_id, expected["assignmentProfileId"]
            ),
            "observedAt": observed_at,
        }
        stable = stage_b_stable_identity(identity)
        if stable["visibility"] != profiles[stable["profileId"]]["requiredVisibility"]:
            raise ContractError(f"provider visibility conflicts with profile: {repository_id}")
        _stage_b_validate_local_repository(local_repository, stable, expected["remoteIdentity"])
        inventory.append(stable)
        ruleset_plan_hash = _stage_b_ruleset_plan_hash(slug, stable, ruleset_template)
        remote_gate_refs = _stage_b_workflow_gate_refs(slug, stable["profileId"], repository_id)
        targets.append(_stage_b_target_change_input(
            local_repository, stable, profiles[stable["profileId"]], contracts,
            workflow_template, ruleset_plan_hash, remote_gate_refs,
        ))
    source_revision = canonical_json_hash({
        # Capture time is observation metadata, not a semantic provider input.
        # Keeping it out makes preview and publication bind the same live facts.
        "repositories": [
            {key: value for key, value in item.items() if key != "observedAt"}
            for item in inventory
        ],
        "targets": targets,
    })
    return {
        "source": "GitHubReadOnly",
        "sourceRevision": source_revision,
        "providerInventory": inventory,
        "assignments": contracts["profiles"]["assignments"],
        "targets": targets,
    }


def _load_stage_b_cli_inputs(repository_root: pathlib.Path, *, dry_run: bool) -> dict:
    fixture_path = os.environ.get("HB_STAGE_B_TEST_FIXTURE")
    if fixture_path:
        if os.environ.get("HB_STAGE_B_TEST_MODE") != "1":
            raise ContractError("Stage-B fixture input requires explicit test mode")
        if not dry_run:
            raise ContractError("Stage-B fixture input is forbidden for plan/state publication")
        fixture = _read_ci_json(pathlib.Path(fixture_path), "Stage-B test input")
        if fixture.get("source") != "Fixture":
            raise ContractError("Stage-B test input must identify Fixture source")
        return fixture
    return load_stage_b_live_inputs(repository_root)


class StageBFleetPreflight:
    """Validate the complete Stage-B read-only input set before any mutation."""

    def __init__(self, repository_root: pathlib.Path):
        self.root = repository_root.resolve()

    def execute(
        self,
        provider_inventory: list[dict],
        *,
        source: str = "Fixture",
        source_revision: str | None = None,
        snapshot_path: pathlib.Path | None = None,
    ) -> dict:
        g3_path = self.root / "specs/029-ci-budget-governance/autonomous-run-gate-evidence-postmerge.json"
        g3 = json.loads(normalized_file_bytes(g3_path).decode())
        if (
            g3.get("snapshotType") != "PostMerge"
            or g3.get("reviewedHead") != STAGE_B_G3_REVIEWED_HEAD
            or g3.get("mergeCommit") != STAGE_B_G3_MERGE_COMMIT
        ):
            raise ContractError("terminal G3 evidence drift blocks Stage B")
        g3_state_path = self.root / "specs/029-ci-budget-governance/autonomous-run-state.json"
        g3_state = json.loads(normalized_file_bytes(g3_state_path).decode())
        if (
            g3_state.get("featurePath") != "specs/029-ci-budget-governance"
            or g3_state.get("status") != "Completed"
            or g3_state.get("checkpointCommit") != STAGE_B_G3_MERGE_COMMIT
            or any(value != "Completed" for value in g3_state.get("closeout", {}).values())
        ):
            raise ContractError("terminal G3 run-state drift blocks Stage B")
        manifest = _read_ci_json(
            self.root / "scripts/config/agentic-workspace-fleet.json", "fleet manifest"
        )
        profiles = _read_ci_json(
            self.root / "scripts/config/ci-budget-profiles.json", "CI profile registry"
        )
        manifest_ids = {"home-baseline"} | {
            item["id"] for item in manifest.get("targets", [])
            if item.get("active") is True and item.get("kind") == "git-repository"
        }
        assignment_ids = [item.get("repositoryId") for item in profiles.get("assignments", [])]
        if len(assignment_ids) != len(set(assignment_ids)):
            raise ContractError("duplicate Stage-B profile assignment")
        inventory = [stage_b_stable_identity(item) for item in provider_inventory]
        inventory_ids = [item["repositoryId"] for item in inventory]
        if len(inventory_ids) != len(set(inventory_ids)):
            raise ContractError("duplicate Stage-B provider repository identity")
        if manifest_ids != set(assignment_ids) or manifest_ids != set(inventory_ids):
            raise ContractError("Stage-B fleet set equality failed before mutation")
        assignments = {item["repositoryId"]: item["profileId"] for item in profiles["assignments"]}
        profile_visibility = {
            item["profileId"]: item["requiredVisibility"] for item in profiles["profiles"]
        }
        for item in inventory:
            if item["profileId"] != assignments[item["repositoryId"]]:
                raise ContractError(f"Stage-B profile assignment drift: {item['repositoryId']}")
            if item["visibility"] != profile_visibility[item["profileId"]]:
                raise ContractError(f"Stage-B visibility drift: {item['repositoryId']}")
        observed_values = {item["observedAt"] for item in inventory}
        if len(observed_values) != 1:
            raise ContractError("Stage-B provider snapshot is not atomic")
        if source not in {"Fixture", "GitHubReadOnly"}:
            raise ContractError("Stage-B provider source is invalid")
        revision = source_revision or canonical_json_hash([
            {key: value for key, value in item.items() if key != "observedAt"}
            for item in inventory
        ])
        if re.fullmatch(r"[0-9a-f]{64}", revision) is None:
            raise ContractError("Stage-B provider source revision is invalid")
        level0_head = run_git(self.root, "rev-parse", "HEAD").stdout.strip()
        if re.fullmatch(r"[0-9a-f]{40}", level0_head) is None:
            raise ContractError("current Level-0 head is invalid")
        level0 = next(item for item in inventory if item["repositoryId"] == "home-baseline")
        if level0["defaultHead"] != level0_head:
            raise ContractError("current Level-0 head differs from live provider default head")
        environment_hash = canonical_json_hash({
            item["repositoryId"]: item["environmentRegistryHash"] for item in inventory
        })
        authority = {
            "status": "Pending", "deliveryMode": "MergeAndSync", "source": "N/A",
            "authorizedAt": "N/A", "validatedAt": "N/A",
            "externalWriteGate": "Closed", "adminBypass": "NotAuthorized",
        }
        inputs = {
            "g3RunStateSha256": normalized_file_sha256(g3_state_path),
            "g3PostMergeEvidenceSha256": normalized_file_sha256(g3_path),
            "level0Head": level0_head,
            "constitutionSha256": normalized_file_sha256(self.root / "constitution.md"),
            "constitutionMirrorSha256": normalized_file_sha256(self.root / ".specify/memory/constitution.md"),
            "manifestSha256": normalized_file_sha256(self.root / "scripts/config/agentic-workspace-fleet.json"),
            "profileRegistrySha256": normalized_file_sha256(self.root / "scripts/config/ci-budget-profiles.json"),
            "pathRegistrySha256": normalized_file_sha256(self.root / "scripts/config/ci-budget-path-contracts.json"),
            "environmentRegistryHash": environment_hash,
            "gateSetHash": canonical_json_hash(profiles.get("gateSets", [])),
            "budgetHash": canonical_json_hash(profiles.get("budgetAssumptions", {})),
            "authorityHash": canonical_json_hash(authority),
            "providerSource": source,
            "providerSourceRevision": revision,
        }
        captured_at = next(iter(observed_values)) if source == "Fixture" else utc_now()
        snapshot = {
            "schemaVersion": "1.0",
            "snapshotId": str(uuid.uuid5(uuid.UUID(STAGE_B_RUN_ID), revision)),
            "capturedAt": captured_at,
            "level0Head": level0_head,
            "g3ReviewedHead": STAGE_B_G3_REVIEWED_HEAD,
            "g3MergeCommit": STAGE_B_G3_MERGE_COMMIT,
            "g3PostMergeEvidenceSha256": inputs["g3PostMergeEvidenceSha256"],
            "fleetManifestHash": inputs["manifestSha256"],
            "profileRegistryHash": inputs["profileRegistrySha256"],
            "pathContractHash": inputs["pathRegistrySha256"],
            "environmentRegistryHash": environment_hash,
            "gateSetHash": inputs["gateSetHash"],
            "authorityHash": inputs["authorityHash"],
            "source": source,
            "sourceRevision": revision,
            "repositoryIds": sorted(manifest_ids),
            "repositoryIdsHash": canonical_json_hash(sorted(manifest_ids)),
            "repositories": sorted(inventory, key=lambda item: item["repositoryId"]),
            "inputs": inputs,
            "inputSetHash": canonical_json_hash(inputs),
            "writes": 0,
            "result": "Passed",
        }
        snapshot_hash_payload = {
            key: value for key, value in snapshot.items() if key != "capturedAt"
        }
        snapshot_hash_payload["repositories"] = [
            {key: value for key, value in item.items() if key != "observedAt"}
            for item in snapshot["repositories"]
        ]
        snapshot["fleetSnapshotHash"] = canonical_json_hash(snapshot_hash_payload)
        if snapshot_path is not None:
            _atomic_stage_b_json(snapshot_path, snapshot)
        return snapshot


class StageBRolloutPlanner:
    """Create one immutable five-wave plan from a fresh preflight snapshot."""

    def __init__(self, run_id: str = STAGE_B_RUN_ID):
        self.run_id = str(uuid.UUID(run_id))

    def build(self, snapshot: dict, assignments: list[dict], targets: list[dict]) -> dict:
        profile_by_id = {item["repositoryId"]: item["profileId"] for item in assignments}
        snapshot_ids = snapshot.get("repositoryIds", [])
        target_ids = [item.get("repositoryId") for item in targets]
        if set(snapshot_ids) != set(profile_by_id) or set(snapshot_ids) != set(target_ids):
            raise ContractError("plan input repository-ID sets differ")
        ordered_targets: list[dict] = []
        for sequence, target in enumerate(
            sorted(targets, key=lambda item: (STAGE_B_WAVES.index(STAGE_B_PROFILE_WAVE[profile_by_id[item["repositoryId"]]]), item["repositoryId"])),
            start=1,
        ):
            repository_id = target["repositoryId"]
            profile_id = profile_by_id[repository_id]
            wave_id = STAGE_B_PROFILE_WAVE[profile_id]
            baseline_head = target["baselineHead"]
            baseline_tree = target.get("baselineTree", baseline_head)
            changes = target.get("changes", [])
            decision = "PullRequest" if changes else "NoOpCandidate"
            planned_diff = canonical_json_hash(changes)
            ordered_targets.append({
                "transactionId": str(uuid.uuid5(uuid.UUID(self.run_id), repository_id)),
                "repositoryId": repository_id,
                "waveId": wave_id,
                "sequence": sequence,
                "baselineHead": baseline_head,
                "baselineTree": baseline_tree,
                "defaultBranch": target["defaultBranch"],
                "profileId": profile_id,
                "stageAPlanHash": target["stageAPlanHash"],
                "gateSetHash": target["gateSetHash"],
                "pathContractHash": target["pathContractHash"],
                "changes": changes,
                "plannedDiffHash": planned_diff,
                "candidateTree": target.get("candidateTree", baseline_tree),
                "workflowAction": target.get("workflowAction", "N/A"),
                "rulesetPlanHash": target.get("rulesetPlanHash", "N/A"),
                "mergeMethod": target.get("mergeMethod", "N/A" if not changes else "merge"),
                "branchName": f"codex/stage-b-{self.run_id[:8]}-{repository_id}",
                "requiredLocalGates": target.get("requiredLocalGates", []),
                "requiredRemoteGates": target.get("requiredRemoteGates", []),
                "idempotencyKeys": [canonical_json_hash({"runId": self.run_id, "repositoryId": repository_id, "action": action}) for action in ("branch", "commit", "pull-request", "merge", "ruleset")],
                "decision": decision,
                "blockers": [],
            })
        waves = [
            {
                "waveId": wave_id,
                "order": index,
                "repositoryIds": sorted(item["repositoryId"] for item in ordered_targets if item["waveId"] == wave_id),
            }
            for index, wave_id in enumerate(STAGE_B_WAVES, start=1)
        ]
        first_target = next(
            (
                item for item in ordered_targets
                if item["decision"] == "PullRequest" or item["rulesetPlanHash"] != "N/A"
            ),
            None,
        )
        plan = {
            "schemaVersion": "1.1",
            "planId": str(uuid.uuid5(uuid.UUID(self.run_id), snapshot["fleetSnapshotHash"])),
            "runId": self.run_id,
            "createdAt": snapshot["capturedAt"],
            "deliveryMode": "MergeAndSync",
            "stageAReference": {
                "featurePath": "specs/029-ci-budget-governance",
                "reviewedHead": STAGE_B_G3_REVIEWED_HEAD,
                "mergeCommit": STAGE_B_G3_MERGE_COMMIT,
                "postMergeEvidenceSha256": snapshot["g3PostMergeEvidenceSha256"],
            },
            "fleetSnapshotHash": snapshot["fleetSnapshotHash"],
            "inputSetHash": snapshot["inputSetHash"],
            "firstMutation": (
                {
                    "repositoryId": first_target["repositoryId"],
                    "actionKind": "Commit" if first_target["changes"] else "Ruleset",
                    "baselineHead": first_target["baselineHead"],
                }
                if first_target else "N/A"
            ),
            "waves": waves,
            "targets": ordered_targets,
            "planHash": "0" * 64,
        }
        validate_stage_b_plan_state_separation(plan)
        plan["planHash"] = canonical_json_hash({
            key: value for key, value in plan.items() if key not in {"createdAt", "planHash"}
        })
        return plan


def build_stage_b_run_state(plan: dict, feature_path: str = "specs/030-stage-b-rollout") -> dict:
    require_stage_b_plan_binding({"planSha256": plan["planHash"]}, plan["planHash"])
    run_id = plan["runId"]
    evidence_root = f".specify/runtime/autonomous-routing/{run_id}/stage-b/evidence/v1"
    state = {
        "schemaVersion": "1.1", "runId": run_id, "featurePath": feature_path,
        "status": "Prepared", "deliveryMode": "MergeAndSync",
        "rolloutPlanBinding": {
            "planId": plan["planId"],
            "planPath": f".specify/runtime/autonomous-routing/{run_id}/stage-b/rollout-plan.json",
            "planSha256": plan["planHash"],
        },
        "authorityBinding": {
            "status": "Pending", "deliveryMode": "MergeAndSync", "source": "N/A",
            "authorizedAt": "N/A", "validatedAt": "N/A", "runId": run_id,
            "planSha256": plan["planHash"], "scopeHash": plan["inputSetHash"],
            "repositoryIdsHash": canonical_json_hash(sorted(item["repositoryId"] for item in plan["targets"])),
            "externalWriteGate": "Closed", "adminBypass": "NotAuthorized",
            "authorityHash": "0" * 64,
        },
        "stageAReference": {
            **plan["stageAReference"],
            "postMergeEvidencePath": "specs/029-ci-budget-governance/autonomous-run-gate-evidence-postmerge.json",
        },
        "fleetSnapshotHash": plan["fleetSnapshotHash"],
        "evidenceLayout": {
            "schemaVersion": "v1", "stageBEvidenceRoot": evidence_root,
            "operationalNamespace": "operational", "primarySnapshotNamespace": "primary",
            "internalRoutingRoot": f".specify/runtime/autonomous-routing/{run_id}",
            "committedFeatureEvidenceRoot": "specs/030-stage-b-rollout/evidence/v1",
        },
        "currentWaveId": "N/A", "currentRepositoryId": "N/A",
        "currentAction": "Prepared", "lastSafeBoundary": "PlanValidated",
        "nextAction": "Revalidate authority before the first external write.",
        "waveResults": [], "targetResults": [], "budgetProjections": [],
        "terminalEvidence": {"planSha256": plan["planHash"], "status": "Pending", "path": "N/A", "sha256": "N/A"},
        "closeout": {"planSha256": plan["planHash"], "status": "Pending", "path": "N/A", "sha256": "N/A"},
        "stop": {"reason": "N/A", "category": "N/A", "stoppedAt": "N/A", "inFlightOperation": "N/A", "requiresExplicitResume": False},
        "resumeCount": 0, "lastRevalidatedAt": "N/A", "stateHash": "0" * 64,
    }
    authority = state["authorityBinding"]
    authority["authorityHash"] = canonical_json_hash({key: value for key, value in authority.items() if key != "authorityHash"})
    state["stateHash"] = canonical_json_hash({key: value for key, value in state.items() if key != "stateHash"})
    return state


def validate_stage_b_plan_semantics(plan: dict) -> None:
    validate_stage_b_plan_state_separation(plan)
    expected_hash = canonical_json_hash({
        key: value for key, value in plan.items() if key not in {"createdAt", "planHash"}
    })
    if plan.get("planHash") != expected_hash:
        raise ContractError("Stage-B planHash differs from its immutable canonical payload")
    target_ids = [item.get("repositoryId") for item in plan.get("targets", [])]
    wave_ids = [repository_id for wave in plan.get("waves", []) for repository_id in wave.get("repositoryIds", [])]
    if len(target_ids) != len(set(target_ids)) or sorted(target_ids) != sorted(wave_ids):
        raise ContractError("Stage-B plan fleet/wave/target equality failed")
    decisions = [item.get("decision") for item in plan["targets"]]
    has_ruleset_mutation = any(item.get("rulesetPlanHash") != "N/A" for item in plan["targets"])
    if plan.get("firstMutation") == "N/A" and (
        any(decision != "NoOpCandidate" for decision in decisions) or has_ruleset_mutation
    ):
        raise ContractError("firstMutation N/A requires an all-no-op plan")


def validate_stage_b_prepared_state(state: dict, plan: dict) -> None:
    validate_stage_b_plan_semantics(plan)
    if state.get("runId") != plan.get("runId"):
        raise ContractError("Stage-B state runId differs from plan")
    binding = state.get("rolloutPlanBinding", {})
    if binding.get("planId") != plan.get("planId") or binding.get("planSha256") != plan.get("planHash"):
        raise ContractError("Stage-B state does not bind the exact plan")
    authority = state.get("authorityBinding", {})
    expected_pending = {
        "status": "Pending", "source": "N/A", "authorizedAt": "N/A",
        "validatedAt": "N/A", "externalWriteGate": "Closed",
        "adminBypass": "NotAuthorized",
    }
    drift = [key for key, value in expected_pending.items() if authority.get(key) != value]
    if drift:
        raise ContractError(f"prepared Stage-B authority is not truthful: {drift}")
    expected_authority_hash = canonical_json_hash({
        key: value for key, value in authority.items() if key != "authorityHash"
    })
    if authority.get("authorityHash") != expected_authority_hash:
        raise ContractError("prepared Stage-B authorityHash differs")
    expected_state_hash = canonical_json_hash({
        key: value for key, value in state.items() if key != "stateHash"
    })
    if state.get("stateHash") != expected_state_hash:
        raise ContractError("prepared Stage-B stateHash differs")


def validate_stage_b_published_state(
    repository_root: pathlib.Path, state_path: pathlib.Path, plan_schema_path: pathlib.Path,
    state_schema_path: pathlib.Path,
) -> tuple[dict, dict]:
    state = load_stage_b_document(state_path, state_schema_path)
    plan_relative = state.get("rolloutPlanBinding", {}).get("planPath")
    if not isinstance(plan_relative, str):
        raise ContractError("Stage-B state has no bound plan path")
    plan_path = (repository_root.resolve() / plan_relative).resolve()
    if repository_root.resolve() not in plan_path.parents:
        raise ContractError("Stage-B plan path escapes repository root")
    if not plan_path.is_file():
        raise ContractError("Stage-B state exists without its bound plan")
    plan = load_stage_b_document(plan_path, plan_schema_path)
    validate_stage_b_prepared_state(state, plan)
    return plan, state


def publish_stage_b_preflight(
    plan_path: pathlib.Path,
    state_path: pathlib.Path,
    plan: dict,
    state: dict,
    plan_schema_path: pathlib.Path,
    state_schema_path: pathlib.Path,
    *,
    failure_injector=None,
) -> None:
    """Validate both documents, then publish plan first and state as commit marker."""
    plan_schema = json.loads(normalized_file_bytes(plan_schema_path).decode("utf-8"))
    state_schema = json.loads(normalized_file_bytes(state_schema_path).decode("utf-8"))
    validate_stage_b_schema_instance(plan, plan_schema, label="Stage-B rollout plan")
    validate_stage_b_schema_instance(state, state_schema, label="Stage-B run state")
    validate_stage_b_prepared_state(state, plan)
    if state_path.exists():
        raise ContractError("existing Stage-B state must be validated and handled before preflight publication")
    if failure_injector is not None:
        failure_injector("before-plan-publish")
    _atomic_stage_b_json(plan_path, plan)
    published_plan = load_stage_b_document(plan_path, plan_schema_path)
    validate_stage_b_plan_semantics(published_plan)
    if failure_injector is not None:
        failure_injector("before-state-publish")
    _atomic_stage_b_json(state_path, state)
    published_state = load_stage_b_document(state_path, state_schema_path)
    validate_stage_b_prepared_state(published_state, published_plan)


def _print_stage_b_preflight(plan: dict, *, dry_run: bool, plan_path: str, state_path: str) -> None:
    first_mutation = plan["firstMutation"]
    first_value = "N/A" if first_mutation == "N/A" else json.dumps(first_mutation, sort_keys=True)
    fields = [
        ("Run-ID / Run ID", plan["runId"]),
        ("Autoritaetsstatus / Authority status", "Pending"),
        ("Dynamische Zielanzahl / Dynamic target count", str(len(plan["targets"]))),
        ("Wellen / Waves", ",".join(wave["waveId"] for wave in plan["waves"])),
        ("Erste Mutation / First mutation", first_value),
        ("Budgetstatus / Budget status", "Passed"),
        ("Entscheidung / Decision", "Preview" if dry_run else "PublishedLocalPlanAndState"),
        ("Status", "Passed"),
        ("Blocker", "N/A"),
        ("Naechste Aktion / Next action", "Deliver T141-R before live preflight." if dry_run else "Revalidate authority before any external write."),
        ("Planziel / Plan target", plan_path),
        ("State-Ziel / State target", state_path),
        ("Vollstaendiger Plan / Complete plan", json.dumps(plan, ensure_ascii=False, sort_keys=True, separators=(",", ":"))),
    ]
    for name, value in fields:
        print(f"{name}: {value}")


def execute_stage_b_preflight(args: argparse.Namespace) -> int:
    root = args.repository_root.resolve()
    run_id = str(uuid.UUID(args.run_id))
    inputs = _load_stage_b_cli_inputs(root, dry_run=args.dry_run)
    snapshot = StageBFleetPreflight(root).execute(
        inputs["providerInventory"],
        source=inputs["source"],
        source_revision=inputs["sourceRevision"],
    )
    plan = StageBRolloutPlanner(run_id).build(snapshot, inputs["assignments"], inputs["targets"])
    state = build_stage_b_run_state(plan)
    stage_b_root = root / ".specify/runtime/autonomous-routing" / run_id / "stage-b"
    plan_path = stage_b_root / "rollout-plan.json"
    state_path = stage_b_root / "stage-b-run-state.json"
    plan_relative = plan_path.relative_to(root).as_posix()
    state_relative = state_path.relative_to(root).as_posix()
    if not args.dry_run:
        publish_stage_b_preflight(
            plan_path, state_path, plan, state,
            root / "scripts/config/stage-b-rollout-plan.schema.json",
            root / "scripts/config/stage-b-run-state.schema.json",
        )
    _print_stage_b_preflight(
        plan, dry_run=args.dry_run, plan_path=plan_relative, state_path=state_relative
    )
    return STAGE_B_EXIT_CODES["Success"]


def validate_external_write_gate(
    state: dict, *, expected_run_id: str, expected_plan_sha256: str,
    expected_scope_hash: str, expected_repository_ids_hash: str,
    expected_delivery_set_hash: str, actual_delivery_set_hash: str,
) -> None:
    # This is the last trust boundary before provider mutation: every authority
    # dimension is rebound to the live delivery set instead of trusting a preview.
    authority = state.get("authorityBinding", {})
    expected = {
        "status": "Authorized", "deliveryMode": "MergeAndSync", "runId": expected_run_id,
        "planSha256": expected_plan_sha256, "scopeHash": expected_scope_hash,
        "repositoryIdsHash": expected_repository_ids_hash, "externalWriteGate": "Open",
    }
    mismatches = [key for key, value in expected.items() if authority.get(key) != value]
    if expected_delivery_set_hash != actual_delivery_set_hash:
        mismatches.append("deliverySetHash")
    if mismatches:
        raise ContractError(f"ExternalWriteGate is closed by drift: {sorted(set(mismatches))}")


def _stage_b_exact_change_key(change: dict) -> tuple:
    required = {
        "path", "action", "modeBefore", "modeAfter", "blobBefore", "blobAfter"
    }
    validate_stage_b_closed_world(change, required, "Stage-B planned change")
    path = str(change["path"])
    if path.startswith("/") or ".." in pathlib.PurePosixPath(path).parts or any(c in path for c in "\0\r\n"):
        raise ContractError(f"unsafe Stage-B change path: {path!r}")
    if change["action"] not in {"Add", "Modify", "Delete", "Rename"}:
        raise ContractError(f"invalid Stage-B change action: {change['action']!r}")
    if change["modeBefore"] not in {"N/A", "100644", "100755", "120000"} or change["modeAfter"] not in {"N/A", "100644", "100755", "120000"}:
        raise ContractError(f"invalid Stage-B mode for {path}")
    for name in ("blobBefore", "blobAfter"):
        value = change[name]
        if value != "N/A" and re.fullmatch(r"[0-9a-f]{40}", str(value)) is None:
            raise ContractError(f"invalid {name} for {path}")
    return tuple(change[name] for name in ("path", "action", "modeBefore", "modeAfter", "blobBefore", "blobAfter"))


def require_exact_stage_b_diff(planned: list[dict], observed: list[dict]) -> str:
    planned_keys = sorted(_stage_b_exact_change_key(item) for item in planned)
    observed_keys = sorted(_stage_b_exact_change_key(item) for item in observed)
    if planned_keys != observed_keys:
        raise ContractError("observed Path/Action/Mode/Blob diff differs from TargetChangePlan")
    return canonical_json_hash(planned_keys)


def stage_b_branch_name(run_id: str, repository_id: str) -> str:
    parsed = str(uuid.UUID(run_id))
    stable_id = validate_ci_input_component(repository_id)
    return f"codex/stage-b-{parsed[:8]}-{stable_id}"


def validate_stage_b_gate_result(result: dict, expected_head: str, gate_id: str) -> dict:
    expected = {"gateId", "headSha", "workflow", "job", "runnerOrPlatform", "executedCommand", "result"}
    validate_stage_b_closed_world(result, expected, f"Stage-B gate {gate_id}")
    if result["gateId"] != gate_id or result["headSha"] != expected_head:
        raise ContractError(f"Stage-B gate identity/head drift: {gate_id}")
    if result["result"] != "Passed" or not all(str(result[field]).strip() for field in ("workflow", "job", "runnerOrPlatform", "executedCommand")):
        raise ContractError(f"Stage-B gate failed or lacks concrete execution proof: {gate_id}")
    return result


class StageBRulesetTransaction:
    """Apply one exact private-governance ruleset with one bounded restore."""

    def __init__(self, fixture: dict, provider: object):
        self.fixture = fixture
        self.provider = provider

    def execute(self) -> dict:
        repository_id, slug = validate_stage_b_provider_identity(
            self.fixture["providerRepositoryId"], self.fixture["slug"]
        )
        ruleset_id = str(self.fixture.get("rulesetId", "1"))
        if re.fullmatch(r"[1-9][0-9]*", ruleset_id) is None:
            raise ContractError("ruleset ID must be a positive integer")
        desired = self.fixture["desiredRuleset"]
        exact = {
            "target": "default_branch", "enforcement": "active",
            "pullRequestRequired": True, "requiredApprovingReviews": 1,
            "requiredStatusChecks": ["home-baseline/ci-minimal-gate"],
            "strictStatusChecks": True,
            "bypassActors": [{
                "actor_id": 5,
                "actor_type": "RepositoryRole",
                "bypass_mode": "pull_request",
            }],
            "blockedWritePaths": ["api", "direct", "web"],
            "adminBypassNormalPath": False,
        }
        if desired != exact:
            raise ContractError("private-governance desired ruleset is not exact")
        before = self.provider.read_ruleset(repository_id, ruleset_id)
        before_hash = canonical_json_hash(before)
        desired_hash = canonical_json_hash(desired)
        if before_hash == desired_hash:
            return {"action": "NoChange", "beforeHash": before_hash, "afterHash": before_hash, "restore": "N/A"}
        action_id = self.provider.write_ruleset(repository_id, ruleset_id, desired)
        after = self.provider.read_ruleset(repository_id, ruleset_id)
        after_hash = canonical_json_hash(after)
        if after_hash != desired_hash:
            restore_hash = canonical_json_hash(before)
            self.provider.restore_ruleset(repository_id, ruleset_id, before, restore_hash)
            restored = self.provider.read_ruleset(repository_id, ruleset_id)
            raise ContractError(
                "ruleset post-write verification failed; bounded restore attempted and run must stop"
            )
        return {
            "action": "Update", "providerActionId": str(action_id),
            "beforeHash": before_hash, "afterHash": after_hash, "restore": "N/A",
            "slug": slug,
        }


class StageBTargetTransaction:
    """Serialize one exact target lifecycle; provider methods receive arrays/data, never shell text."""

    def __init__(
        self, fixture: dict, provider: object, *, preview: bool = False,
        evidence_root: pathlib.Path | None = None,
    ):
        self.fixture = fixture
        self.provider = provider
        self.preview = preview
        self.evidence_root = evidence_root
        self.events: list[str] = []

    def _event(self, name: str) -> None:
        self.events.append(name)

    def execute(self) -> dict:
        fixture = self.fixture
        run_id = str(uuid.UUID(fixture["runId"]))
        repository_id = validate_ci_input_component(fixture["repositoryId"])
        validate_stage_b_provider_identity(fixture["providerRepositoryId"], fixture["slug"])
        if re.fullmatch(r"[0-9a-f]{64}", str(fixture.get("environmentRegistryHash", ""))) is None:
            raise ContractError("target Environment Registry hash is missing")
        expected_branch = stage_b_branch_name(run_id, repository_id)
        if fixture["branchName"] != expected_branch:
            raise ContractError("run-bound Stage-B branch name differs")
        planned = fixture["plannedChanges"]
        reconcile = getattr(self.provider, "read_existing_result", None)
        if callable(reconcile):
            existing = reconcile(run_id, repository_id, fixture["planSha256"])
            if existing is not None:
                if existing.get("outcome") != "Converged" or existing.get("repositoryId") != repository_id:
                    raise ContractError("existing target result is not trustworthy for resume")
                return {**existing, "reconciled": True, "writes": 0}
        if self.preview:
            return {
                "outcome": "Preview", "repositoryId": repository_id,
                "branchName": expected_branch, "plannedDiffHash": canonical_json_hash(planned),
                "writes": 0, "events": [],
            }
        branch_result = self.provider.ensure_branch(expected_branch, fixture["baselineHead"])
        if branch_result not in {"Created", "ExistingAtBoundHead"}:
            raise ContractError("existing Stage-B branch identity/head is ambiguous")
        self._event("Branch")
        observed = self.provider.materialize_changes(planned)
        diff_hash = require_exact_stage_b_diff(planned, observed)
        candidate = self.provider.prepare_candidate()
        if candidate.get("headSha") != fixture["candidateHead"] or candidate.get("treeSha") != fixture["candidateTree"]:
            raise ContractError("candidate head/tree differs from exact TargetChangePlan")
        local_gate_evidence = []
        for gate_command in fixture["requiredLocalGates"]:
            result = self.provider.run_local_gate(gate_command, candidate["headSha"])
            local_gate_evidence.append(
                validate_stage_b_gate_result(result, candidate["headSha"], result.get("gateId", ""))
            )
        security = self.provider.run_secret_scan(candidate["headSha"])
        if security.get("result") != "Passed" or security.get("restrictedFindings") != 0:
            raise ContractError("candidate secret/redaction scan failed")
        premerge = {
            "schemaVersion": "1.0", "runId": run_id, "planSha256": fixture["planSha256"],
            "repositoryId": repository_id, "candidateHead": candidate["headSha"],
            "candidateTree": candidate["treeSha"], "plannedDiffHash": diff_hash,
            "localGates": local_gate_evidence, "security": security,
            "requiredReview": fixture["requiredReview"], "authority": "MergeAndSync",
        }
        premerge_hash = canonical_json_hash(premerge)
        if self.evidence_root is not None:
            _atomic_stage_b_json(self.evidence_root / "repositories" / repository_id / "premerge.json", premerge)
        self._event("PreMerge")
        commit = self.provider.stage_and_commit(planned, candidate["treeSha"])
        if commit.get("headSha") != candidate["headSha"] or commit.get("treeSha") != candidate["treeSha"] or commit.get("stagedDiffCheck") != "Passed":
            raise ContractError("staged path/tree inventory differs from exact candidate")
        self._event("Commit")
        self.provider.push(expected_branch, candidate["headSha"])
        self._event("Push")
        pull_request = self.provider.ensure_pull_request(
            expected_branch, candidate["headSha"], diff_hash, run_id
        )
        if pull_request.get("headSha") != candidate["headSha"] or pull_request.get("count") != 1:
            raise ContractError("pull request reconciliation is not unique at candidate head")
        self._event("PullRequest")
        remote_gate_evidence = []
        for gate_id in fixture["requiredRemoteGates"]:
            result = self.provider.read_remote_gate(pull_request["number"], gate_id)
            remote_gate_evidence.append(validate_stage_b_gate_result(result, candidate["headSha"], gate_id))
        review = self.provider.read_review(pull_request["number"])
        if review.get("status") != "Approved" or review.get("headSha") != candidate["headSha"]:
            raise ContractError("regular review is missing or bound to another head")
        if review.get("unresolvedThreads") != 0:
            raise ContractError("actionable review threads remain unresolved")
        self._event("ReviewAndGates")
        independent_hashes = {
            "acceptance": canonical_json_hash(remote_gate_evidence),
            "security": canonical_json_hash(security),
            "review": canonical_json_hash(review),
            "preMerge": premerge_hash,
        }
        merge = None
        protection_refusal = None
        for method in fixture["mergeMethods"]:
            attempt = self.provider.merge(pull_request["number"], method, admin=False)
            if attempt.get("result") == "Merged":
                merge = attempt
                break
            if attempt.get("classification") == "ProtectionOnlyRefusal":
                protection_refusal = attempt
                continue
            if attempt.get("classification") not in {"MethodUnavailable"}:
                raise ContractError("regular merge failed for a non-protection reason")
        bypass_evidence = {"used": False, "reason": "N/A"}
        if merge is None:
            # Bypass is downstream of independent gate/review proof; it can
            # address protection mechanics, never weaken acceptance.
            authority = fixture.get("adminBypassAuthority")
            if protection_refusal is None or not isinstance(authority, dict):
                raise ContractError("regular merge did not converge and bypass is unavailable")
            required_authority = {
                "repositoryId": repository_id, "prHead": candidate["headSha"],
                "runId": run_id, "scope": "ProtectionOnlyMerge",
            }
            if any(authority.get(key) != value for key, value in required_authority.items()):
                raise ContractError("admin-bypass authority is stale or out of scope")
            try:
                expires_at = datetime.fromisoformat(str(authority["expiresAt"]).replace("Z", "+00:00"))
            except (KeyError, ValueError) as exc:
                raise ContractError("admin-bypass authority expiry is missing or invalid") from exc
            if expires_at <= datetime.now(timezone.utc):
                raise ContractError("admin-bypass authority is expired")
            merge = self.provider.merge(pull_request["number"], fixture["mergeMethods"][0], admin=True)
            if merge.get("result") != "Merged":
                raise ContractError("narrow admin-bypass merge did not converge")
            bypass_evidence = {
                "used": True, "repositoryId": repository_id,
                "prHead": candidate["headSha"], "reason": authority.get("reason", "ProtectionOnlyRefusal"),
                "scope": authority["scope"], "authorityHash": canonical_json_hash(authority),
                "protectionRefusalHash": canonical_json_hash(protection_refusal),
                "independentEvidenceHashes": independent_hashes,
                "providerActionId": str(merge.get("providerActionId", "N/A")),
            }
        self._event("Merge")
        sync = self.provider.sync_default(fixture["defaultBranch"], merge["mergeCommit"])
        if sync.get("localHead") != merge["mergeCommit"] or sync.get("remoteHead") != merge["mergeCommit"]:
            raise ContractError("local/remote default branch synchronization failed")
        self._event("DefaultSync")
        postmerge = {
            "schemaVersion": "1.0", "preMergeEvidenceSha256": premerge_hash,
            "mergeCommit": merge["mergeCommit"], "defaultSync": sync,
            "providerHash": self.provider.final_provider_hash(),
            "planSha256": fixture["planSha256"],
        }
        if self.evidence_root is not None:
            _atomic_stage_b_json(self.evidence_root / "repositories" / repository_id / "postmerge.json", postmerge)
        self._event("PostMerge")
        return {
            "outcome": "Converged", "repositoryId": repository_id,
            "branchName": expected_branch, "plannedDiffHash": diff_hash,
            "preMerge": premerge, "preMergeSha256": premerge_hash,
            "postMerge": postmerge, "postMergeSha256": canonical_json_hash(postmerge),
            "adminBypass": bypass_evidence, "events": self.events,
            "pullRequestNumber": pull_request["number"], "mergeCommit": merge["mergeCommit"],
        }


STAGE_B_STATE_TRANSITIONS = {
    "Prepared": {"Preflighted", "Blocked", "Stopped"},
    "Preflighted": {"SliceValidated", "Delivering", "Completed", "Blocked", "Stopped"},
    "SliceValidated": {"Delivering", "Blocked", "Stopped"},
    "Delivering": {"Delivering", "Completed", "Blocked", "Stopped"},
    "Stopped": {"Preflighted", "Delivering", "Blocked"},
    "Blocked": {"Preflighted", "Delivering", "Blocked"},
    "Completed": set(),
}


def stage_b_idempotency_key(
    run_id: str, repository_id: str, action: str, baseline_head: str,
    candidate_head: str, plan_sha256: str,
) -> str:
    str(uuid.UUID(run_id))
    validate_ci_input_component(repository_id)
    validate_ci_input_component(action)
    for label, value in (("baseline", baseline_head), ("candidate", candidate_head)):
        if re.fullmatch(r"[0-9a-f]{40}", value) is None:
            raise ContractError(f"{label} head is invalid")
    if re.fullmatch(r"[0-9a-f]{64}", plan_sha256) is None:
        raise ContractError("planSha256 is invalid")
    return canonical_json_hash({
        "runId": run_id, "repositoryId": repository_id, "action": action,
        "baselineHead": baseline_head, "candidateHead": candidate_head,
        "planSha256": plan_sha256,
    })


def transition_stage_b_state(state: dict, next_status: str, **updates: object) -> dict:
    current = state.get("status")
    if current not in STAGE_B_STATE_TRANSITIONS or next_status not in STAGE_B_STATE_TRANSITIONS[current]:
        raise ContractError(f"invalid Stage-B state transition: {current} -> {next_status}")
    updated = json.loads(json.dumps(state))
    updated["status"] = next_status
    allowed_updates = {
        "currentWaveId", "currentRepositoryId", "currentAction", "lastSafeBoundary",
        "nextAction", "waveResults", "targetResults", "budgetProjections",
        "terminalEvidence", "closeout", "stop", "resumeCount", "lastRevalidatedAt",
    }
    unknown = set(updates) - allowed_updates
    if unknown:
        raise ContractError(f"unknown mutable Stage-B state fields: {sorted(unknown)}")
    updated.update(updates)
    expected_plan = state.get("rolloutPlanBinding", {}).get("planSha256")
    if not expected_plan:
        raise ContractError("Stage-B state lost direct planSha256 binding")
    updated["stateHash"] = canonical_json_hash({key: value for key, value in updated.items() if key != "stateHash"})
    return updated


def persist_stage_b_stop(
    state: dict, path: pathlib.Path, *, category: str, reason: str,
    in_flight_operation: str, last_safe_boundary: str, next_action: str,
) -> dict:
    if category not in {"Drift", "Gate", "Review", "Push", "Ruleset", "Merge", "Budget", "Provider", "Security", "UserStop"}:
        raise ContractError("Stage-B stop category is invalid")
    # Publish the safe boundary atomically so resume never guesses whether an
    # interrupted provider operation completed.
    stopped = transition_stage_b_state(
        state, "Stopped", currentAction="Stopped", lastSafeBoundary=last_safe_boundary,
        nextAction=next_action,
        stop={
            "reason": reason, "category": category, "stoppedAt": utc_now(),
            "inFlightOperation": in_flight_operation, "requiresExplicitResume": True,
        },
    )
    _atomic_stage_b_json(path, stopped)
    return stopped


def evaluate_stage_b_noop(case: dict, *, ruleset_transaction: object | None = None) -> dict:
    required = {
        "caseId", "treeEqual", "profileEqual", "workflowEqual", "gateEqual",
        "rulesetEqual", "providerFresh", "expected",
    }
    validate_stage_b_closed_world(case, required, "Stage-B no-op fixture")
    if not case["providerFresh"]:
        raise ContractError("no-op provider evidence is stale")
    if not all(case[field] for field in ("treeEqual", "profileEqual", "workflowEqual", "gateEqual")):
        raise ContractError("empty Git diff is not semantic no-op convergence")
    ruleset_result = "N/A"
    if not case["rulesetEqual"]:
        if ruleset_transaction is None:
            return {"outcome": "RulesetTransaction", "branchWrites": 0, "commitWrites": 0, "pullRequestWrites": 0}
        ruleset_result = ruleset_transaction.execute()
    return {
        "outcome": "NoOpConverged", "capturedAt": utc_now(),
        "semanticContractHash": canonical_json_hash(case),
        "providerHash": canonical_json_hash({"caseId": case["caseId"], "fresh": True}),
        "ruleset": ruleset_result,
        "branchWrites": 0, "commitWrites": 0, "pullRequestWrites": 0,
    }


def complete_all_noop_stage_b_state(state: dict, results: list[dict]) -> dict:
    if not results or any(item.get("outcome") != "NoOpConverged" for item in results):
        raise ContractError("all-no-op completion requires complete semantic convergence")
    if state.get("authorityBinding", {}).get("externalWriteGate") != "Closed":
        raise ContractError("all-no-op fleet must keep ExternalWriteGate closed")
    return transition_stage_b_state(
        state, "Completed", currentAction="CompletedNoOp",
        lastSafeBoundary="FleetSemanticallyConverged", nextAction="N/A",
        targetResults=results,
    )


def resume_stage_b_state(
    state: dict, *, plan_sha256: str, fleet_snapshot_hash: str,
    authority_hash: str, provider_hash: str, budget_hash: str,
) -> dict:
    # Resume rebinds every mutable authority input; a matching run ID alone is
    # insufficient because fleet or provider truth may have changed.
    if state.get("status") not in {"Stopped", "Blocked"}:
        raise ContractError("Stage-B resume requires Stopped or Blocked state")
    if state.get("rolloutPlanBinding", {}).get("planSha256") != plan_sha256:
        raise ContractError("Stage-B resume plan drift")
    if state.get("fleetSnapshotHash") != fleet_snapshot_hash:
        raise ContractError("Stage-B resume fleet drift")
    if state.get("authorityBinding", {}).get("authorityHash") != authority_hash:
        raise ContractError("Stage-B resume authority drift")
    for label, value in (("providerHash", provider_hash), ("budgetHash", budget_hash)):
        if re.fullmatch(r"[0-9a-f]{64}", value) is None:
            raise ContractError(f"Stage-B resume {label} is missing")
    target_results = state.get("targetResults", [])
    first_non_converged = next(
        (item.get("repositoryId") for item in target_results if item.get("outcome") not in {"Converged", "NoOpConverged"}),
        state.get("currentRepositoryId", "N/A"),
    )
    return transition_stage_b_state(
        state, "Delivering", currentRepositoryId=first_non_converged,
        currentAction="ResumeRevalidated", lastSafeBoundary="ResumeAuditPassed",
        nextAction=f"Resume at {first_non_converged}.",
        resumeCount=int(state.get("resumeCount", 0)) + 1,
        lastRevalidatedAt=utc_now(),
        stop={"reason": "N/A", "category": "N/A", "stoppedAt": "N/A", "inFlightOperation": "N/A", "requiresExplicitResume": False},
    )


def validate_stage_b_profile_contract(profile_id: str, target: dict) -> None:
    if profile_id == "public-canary":
        if target.get("repositoryId") not in {"agent-operations-cockpit", "home-baseline", "tui-vision"}:
            raise ContractError("public-canary identity is outside the exact representative set")
    elif profile_id == "public-product":
        if target.get("preserveRequiredPublicCI") is not True or not target.get("environmentGates"):
            raise ContractError("public-product must preserve required CI and environment gates")
    elif profile_id == "private-product":
        if target.get("pathDependentProductGates") is not True or target.get("unconditionalMainRebuild") is True:
            raise ContractError("private-product gate contract is too broad or missing")
    elif profile_id == "private-governance-scaffold":
        expected = {
            "requiredStatusChecks": ["home-baseline/ci-minimal-gate"],
            "requiredApprovingReviews": 1, "strictStatusChecks": True,
            "bypassActors": [{
                "actor_id": 5,
                "actor_type": "RepositoryRole",
                "bypass_mode": "pull_request",
            }], "fullPullRequestBuild": False,
            "fullMainBuild": False,
        }
        if any(target.get(key) != value for key, value in expected.items()):
            raise ContractError("private-governance profile differs from the exact server contract")
    elif profile_id == "public-preset":
        if target.get("repositoryWorkflows") not in ([], None) or target.get("fleetPipelineEvidence") is not True:
            raise ContractError("public-preset forbids repository-specific workflows")
    else:
        raise ContractError(f"unknown Stage-B profile: {profile_id}")


def build_stage_b_wave_result(
    wave_id: str, plan_sha256: str, repository_results: list[dict],
    predecessor_result_sha256: str, budget_projection_sha256: str,
) -> dict:
    if wave_id not in STAGE_B_WAVES:
        raise ContractError("unknown Stage-B wave")
    if any(item.get("outcome") not in {"Converged", "NoOpConverged"} for item in repository_results):
        raise ContractError("partial Stage-B wave cannot be Converged")
    repository_hashes = [
        {"repositoryId": item["repositoryId"], "resultSha256": item["resultSha256"]}
        for item in sorted(repository_results, key=lambda value: value["repositoryId"])
    ]
    result = {
        "schemaVersion": "1.0", "waveId": wave_id,
        "planSha256": plan_sha256, "status": "Converged",
        "repositoryResults": repository_hashes,
        "predecessorResultSha256": predecessor_result_sha256,
        "budgetProjectionSha256": budget_projection_sha256,
    }
    result["resultSha256"] = canonical_json_hash(result)
    return result


class StageBWaveCoordinator:
    """Run exactly one target writer at a time in the accepted five-wave order."""

    def __init__(self, target_handler, budget_handler):
        self.target_handler = target_handler
        self.budget_handler = budget_handler
        self.started: list[str] = []
        self.active_writers = 0
        self.maximum_active_writers = 0

    def execute(self, wave_targets: dict[str, list[dict]], plan_sha256: str) -> list[dict]:
        if list(wave_targets) != list(STAGE_B_WAVES):
            raise ContractError("Stage-B wave map must use the exact accepted order")
        predecessor = "N/A"
        wave_results: list[dict] = []
        for wave_id in STAGE_B_WAVES:
            targets = wave_targets[wave_id]
            ids = [item["repositoryId"] for item in targets]
            if ids != sorted(ids) or len(ids) != len(set(ids)):
                raise ContractError(f"Stage-B wave order/identity drift: {wave_id}")
            if wave_id == "public-canaries" and ids != ["agent-operations-cockpit", "home-baseline", "tui-vision"]:
                raise ContractError("all three Public Canaries must converge before breadth")
            repository_results = []
            for target in targets:
                # A checked counter turns serialized delivery into an invariant,
                # not an assumption about caller behavior.
                if self.active_writers != 0:
                    raise ContractError("concurrent Stage-B target writer detected")
                self.active_writers += 1
                self.maximum_active_writers = max(self.maximum_active_writers, self.active_writers)
                self.started.append(target["repositoryId"])
                try:
                    validate_stage_b_profile_contract(target["profileId"], target)
                    result = self.target_handler(target)
                finally:
                    self.active_writers -= 1
                if result.get("outcome") not in {"Converged", "NoOpConverged"}:
                    raise ContractError(f"Stage-B target failed; stop before next target: {target['repositoryId']}")
                repository_results.append(result)
            budget = self.budget_handler(wave_id, repository_results, predecessor)
            if budget.get("result") != "Pass":
                raise ContractError(f"Stage-B budget blocks after wave: {wave_id}")
            wave_result = build_stage_b_wave_result(
                wave_id, plan_sha256, repository_results, predecessor,
                budget["projectionSha256"],
            )
            wave_results.append(wave_result)
            predecessor = wave_result["resultSha256"]
        return wave_results


class StageBBudgetProjector:
    """Reuse the exact Decimal 52/12 model and keep Copilot categories separate."""

    def project(
        self, *, wave_id: str, plan_sha256: str, fleet_snapshot_hash: str,
        wave_result_sha256: str, predecessor_result_sha256: str,
        expected_predecessor_sha256: str, minutes_per_week: str,
        provider_fresh: bool, provider_observed_at: str,
        copilot_categories: dict[str, str],
    ) -> dict:
        if wave_id not in STAGE_B_WAVES:
            raise ContractError("budget projection wave is unknown")
        for value in (plan_sha256, fleet_snapshot_hash, wave_result_sha256):
            if re.fullmatch(r"[0-9a-f]{64}", value) is None:
                raise ContractError("budget projection hash binding is invalid")
        if predecessor_result_sha256 != expected_predecessor_sha256:
            raise ContractError("budget predecessor hash drift")
        if not provider_fresh or not provider_observed_at.endswith("Z"):
            raise ContractError("budget provider data is missing or stale")
        try:
            weekly = Decimal(minutes_per_week)
        except InvalidOperation as exc:
            raise ContractError("missing budget data is not zero") from exc
        if weekly < 0:
            raise ContractError("budget minutes cannot be negative")
        monthly = weekly * Decimal(52) / Decimal(12)
        target = Decimal(500)
        result = "Pass" if monthly < target else "Blocked"
        if set(copilot_categories) != {"review-runner-time", "premium-requests", "seat-consumption"}:
            raise ContractError("Copilot consumption categories must remain separate")
        projection = {
            "schemaVersion": "1.0", "waveId": wave_id,
            "planSha256": plan_sha256, "fleetSnapshotHash": fleet_snapshot_hash,
            "waveResultSha256": wave_result_sha256,
            "predecessorResultSha256": predecessor_result_sha256,
            "providerObservedAt": provider_observed_at,
            "assumptions": {
                "weeksPerMonthNumerator": "52", "weeksPerMonthDenominator": "12",
                "privateMonthlyBudgetMinutes": "3000",
                "privateMonthlyTargetExclusiveMinutes": "500",
            },
            "minutesPerWeek": format(weekly, "f"),
            "projectedPrivateMonthlyMinutes": format(monthly.quantize(Decimal("0.000001")), "f"),
            "copilotCategories": dict(copilot_categories),
            "result": result,
        }
        projection["projectionSha256"] = canonical_json_hash(projection)
        return projection


def validate_stage_b_redaction(value: object) -> dict:
    text = json.dumps(value, ensure_ascii=False, sort_keys=True)
    patterns = {
        "secret": r"(?i)\b(?:gh[pousr]_[A-Za-z0-9]+|token\s*[:=]|password\s*[:=]|authorization\s*[:=])",
        "privatePath": re.escape(str(pathlib.Path.home())),
        "providerRawResponse": r"(?i)\b(?:set-cookie|x-github-request-id|rawProviderResponse)\b",
    }
    findings = {name: len(re.findall(pattern, text)) for name, pattern in patterns.items()}
    if any(findings.values()):
        raise ContractError(f"restricted Stage-B evidence fields: {findings}")
    return {"secretFindings": 0, "privatePathFindings": 0, "providerRawResponseFindings": 0, "personalDataFindings": 0}


class StageBTerminalVerifier:
    """Prove every fresh authoritative repository exactly once at terminal state."""

    def verify(
        self, *, run_id: str, plan_id: str, plan_sha256: str,
        fleet_snapshot_hash: str, authoritative_repository_ids: list[str],
        repository_results: list[dict], wave_results: list[dict],
        budget_projections: list[dict], level0_control_plane: dict,
        g4_isolation: dict,
    ) -> dict:
        # This proves the exact captured fleet and hashes; it neither authorizes
        # later G4 work nor claims facts beyond this immutable snapshot.
        ids = sorted(authoritative_repository_ids)
        result_ids = [item.get("repositoryId") for item in repository_results]
        if len(ids) != len(set(ids)) or sorted(result_ids) != ids or len(result_ids) != len(set(result_ids)):
            raise ContractError("terminal fleet ID set/count equality failed")
        if any(item.get("outcome") not in {"Converged", "NoOpConverged"} for item in repository_results):
            raise ContractError("terminal fleet contains a non-converged repository")
        if len(wave_results) != 5 or [item.get("waveId") for item in wave_results] != list(STAGE_B_WAVES):
            raise ContractError("terminal fleet requires exactly five ordered waves")
        if len(budget_projections) != 5 or any(item.get("result") != "Pass" for item in budget_projections):
            raise ContractError("terminal fleet requires five fresh passing budgets")
        for collection in (repository_results, wave_results, budget_projections):
            if any(item.get("planSha256") != plan_sha256 for item in collection):
                raise ContractError("terminal evidence lost direct planSha256 binding")
        evidence = {
            "schemaVersion": "1.1", "terminalEvidenceId": str(uuid.uuid4()),
            "runId": run_id, "planId": plan_id, "planSha256": plan_sha256,
            "capturedAt": utc_now(), "authoritativeRepositoryIds": ids,
            "fleetSnapshotHash": fleet_snapshot_hash,
            "repositoryResults": repository_results, "waveResults": wave_results,
            "budgetProjectionHashes": [item["projectionSha256"] for item in budget_projections],
            "convergedRepositoryCount": len(repository_results),
            "authoritativeRepositoryCount": len(ids),
            "level0ControlPlane": level0_control_plane,
            "g4Isolation": g4_isolation,
        }
        evidence["redaction"] = validate_stage_b_redaction(evidence)
        evidence["terminalHash"] = canonical_json_hash(evidence)
        return evidence


def build_stage_b_g4_isolation(
    *, baseline_hashes: dict[str, str], current_hashes: dict[str, str]
) -> dict:
    required = {"g4", "intakeSeries", "copilot", "account", "subscription"}
    validate_stage_b_closed_world(baseline_hashes, required, "G4 isolation baseline")
    validate_stage_b_closed_world(current_hashes, required, "G4 isolation current state")
    if baseline_hashes != current_hashes:
        drift = sorted(key for key in required if baseline_hashes[key] != current_hashes[key])
        raise ContractError(f"forbidden G4/series/Copilot/account/subscription drift: {drift}")
    return {
        "status": "Unchanged", "hashes": dict(current_hashes),
        "executedActions": [],
        "nextExactAction": "Request a separately authorized intake-series sequencing update.",
    }


def build_stage_b_causal_evidence(
    *, plan_sha256: str, candidate_head: str, premerge_sha256: str,
    merge_commit: str, default_head: str, provider_hash: str,
) -> dict:
    if merge_commit != default_head:
        raise ContractError("causal Stage-B default head differs from merge commit")
    for value in (candidate_head, merge_commit, default_head):
        if re.fullmatch(r"[0-9a-f]{40}", value) is None:
            raise ContractError("causal Stage-B Git identity is invalid")
    for value in (plan_sha256, premerge_sha256, provider_hash):
        if re.fullmatch(r"[0-9a-f]{64}", value) is None:
            raise ContractError("causal Stage-B hash binding is invalid")
    evidence = {
        "schemaVersion": "2.0", "snapshotType": "PostMerge",
        "snapshotId": str(uuid.uuid4()), "capturedAt": utc_now(),
        "planSha256": plan_sha256, "reviewedHead": candidate_head,
        "acceptedPreMergeSha256": premerge_sha256,
        "mergeCommit": merge_commit, "defaultHead": default_head,
        "providerHash": provider_hash, "productDelta": False,
    }
    validate_stage_b_redaction(evidence)
    return evidence


def validate_ci_input_component(value: object) -> str:
    if (
        not isinstance(value, str)
        or not value
        or not re.fullmatch(r"[a-z0-9][a-z0-9-]*", value)
        or value.startswith(("-", "/"))
        or "\\" in value
        or any(character in value for character in ("\r", "\n", "\0"))
        or ".." in pathlib.PurePosixPath(value).parts
    ):
        raise ContractError(f"unsafe CI input component: {value!r}")
    return value


def resolve_ci_contained_path(
    root: pathlib.Path, relative: pathlib.PurePosixPath
) -> pathlib.Path:
    if relative.is_absolute() or any(part in {"", ".", ".."} for part in relative.parts):
        raise ContractError(f"unsafe repository-relative path: {relative}")
    resolved_root = root.resolve()
    candidate = resolved_root.joinpath(*relative.parts).resolve(strict=False)
    try:
        candidate.relative_to(resolved_root)
    except ValueError as exc:
        raise ContractError(f"path escapes repository boundary: {relative}") from exc
    return candidate


def require_matching_profile_ids(
    repository_id: str, assignment_profile_id: str, observed_profile_id: str
) -> None:
    if assignment_profile_id != observed_profile_id:
        raise ContractError(
            f"denormalized profileId drift for {repository_id}: "
            f"{assignment_profile_id!r} != {observed_profile_id!r}"
        )


def _read_ci_json(path: pathlib.Path, label: str) -> dict:
    try:
        value = json.loads(path.read_text(encoding="utf-8-sig"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError(f"{label} cannot be read as UTF-8 JSON: {exc}") from exc
    if not isinstance(value, dict):
        raise ContractError(f"{label} root must be an object")
    return value


def _require_exact_keys(value: dict, expected: set[str], label: str) -> None:
    unknown = set(value) - expected
    missing = expected - set(value)
    if unknown or missing:
        raise ContractError(
            f"{label} fields differ; missing={sorted(missing)}, unknown={sorted(unknown)}"
        )


def _validate_ci_profiles(value: dict) -> tuple[set[str], set[str]]:
    _require_exact_keys(
        value,
        {
            "schemaVersion", "registryVersion", "level0Self", "fleetManifestPath",
            "profiles", "assignments", "gateSets", "budgetAssumptions",
        },
        "profile registry",
    )
    if value["schemaVersion"] != "1.0" or not re.fullmatch(
        r"\d+\.\d+\.\d+", str(value["registryVersion"])
    ):
        raise ContractError("profile registry version is invalid")
    if value["level0Self"] != {
        "repositoryId": "home-baseline",
        "remoteResolution": "executing-level0-configured-origin",
    }:
        raise ContractError("level0Self is invalid")
    if value["fleetManifestPath"] != "scripts/config/agentic-workspace-fleet.json":
        raise ContractError("fleetManifestPath is invalid")
    profiles = value["profiles"]
    if not isinstance(profiles, list) or [item.get("profileId") for item in profiles if isinstance(item, dict)] != list(CI_PROFILE_ORDER):
        raise ContractError("exactly five profiles in canonical order are required")
    profile_fields = {
        "profileId", "displayName", "requiredVisibility", "gateSetId",
        "workflowPolicyId", "budgetClass",
    }
    for index, profile in enumerate(profiles):
        if not isinstance(profile, dict):
            raise ContractError(f"profiles[{index}] must be an object")
        _require_exact_keys(profile, profile_fields, f"profiles[{index}]")
        if profile["requiredVisibility"] not in {"public", "private"}:
            raise ContractError(f"profiles[{index}].requiredVisibility is invalid")
    assignments = value["assignments"]
    if not isinstance(assignments, list) or not assignments:
        raise ContractError("assignments must be a non-empty array")
    assignment_ids: set[str] = set()
    for index, assignment in enumerate(assignments):
        if not isinstance(assignment, dict):
            raise ContractError(f"assignments[{index}] must be an object")
        _require_exact_keys(
            assignment, {"repositoryId", "profileId", "rationale"}, f"assignments[{index}]"
        )
        repository_id = validate_ci_input_component(assignment["repositoryId"])
        if repository_id in assignment_ids:
            raise ContractError(f"duplicate assignment: {repository_id}")
        assignment_ids.add(repository_id)
        if assignment["profileId"] not in CI_PROFILE_ORDER or not assignment["rationale"]:
            raise ContractError(f"invalid assignment: {repository_id}")
    gate_sets = value["gateSets"]
    if not isinstance(gate_sets, list) or not gate_sets:
        raise ContractError("gateSets must be a non-empty array")
    gate_ids: set[str] = set()
    for set_index, gate_set in enumerate(gate_sets):
        _require_exact_keys(gate_set, {"gateSetId", "version", "gates"}, f"gateSets[{set_index}]")
        validate_ci_input_component(gate_set["gateSetId"])
        if not re.fullmatch(r"\d+\.\d+\.\d+", str(gate_set["version"])):
            raise ContractError("gate-set version is invalid")
        gates = gate_set["gates"]
        if not isinstance(gates, list) or not gates:
            raise ContractError("gate-set gates must be non-empty")
        expected_order = 1
        for gate_index, gate in enumerate(gates):
            _require_exact_keys(
                gate,
                {"order", "gateId", "executable", "arguments", "workingDirectory", "timeoutSeconds"},
                f"gateSets[{set_index}].gates[{gate_index}]",
            )
            if gate["order"] != expected_order:
                raise ContractError("gate orders must be contiguous and start at one")
            expected_order += 1
            gate_id = validate_ci_input_component(gate["gateId"])
            if gate_id in gate_ids:
                raise ContractError(f"duplicate gateId: {gate_id}")
            gate_ids.add(gate_id)
            executable = gate["executable"]
            if not isinstance(executable, str) or not executable or re.search(r"[\r\n\0]", executable):
                raise ContractError(f"unsafe executable for {gate_id}")
            arguments = gate["arguments"]
            if not isinstance(arguments, list) or any(
                not isinstance(argument, str) or re.search(r"[\r\n\0]", argument)
                for argument in arguments
            ):
                raise ContractError(f"unsafe argument array for {gate_id}")
            working = gate["workingDirectory"]
            if working != ".":
                resolve_ci_contained_path(pathlib.Path.cwd(), pathlib.PurePosixPath(working))
            if not isinstance(gate["timeoutSeconds"], int) or not 1 <= gate["timeoutSeconds"] <= 3600:
                raise ContractError(f"invalid timeout for {gate_id}")
    assumptions = value["budgetAssumptions"]
    _require_exact_keys(
        assumptions,
        {
            "weeksPerMonthNumerator", "weeksPerMonthDenominator",
            "privateMonthlyBudgetMinutes", "privateMonthlyTargetExclusiveMinutes",
            "recurringPrivateJobsPerWeekReference",
        },
        "budgetAssumptions",
    )
    if assumptions != {
        "weeksPerMonthNumerator": 52,
        "weeksPerMonthDenominator": 12,
        "privateMonthlyBudgetMinutes": 3000,
        "privateMonthlyTargetExclusiveMinutes": 500,
        "recurringPrivateJobsPerWeekReference": 22,
    }:
        raise ContractError("budget assumptions differ from the accepted contract")
    return assignment_ids, gate_ids


def _validate_ci_paths(value: dict, gate_ids: set[str], product_job_ids: set[str]) -> None:
    _require_exact_keys(value, {"schemaVersion", "registryVersion", "pathContracts"}, "path registry")
    if value["schemaVersion"] != "1.0" or not re.fullmatch(
        r"\d+\.\d+\.\d+", str(value["registryVersion"])
    ):
        raise ContractError("path registry version is invalid")
    contracts = value["pathContracts"]
    if not isinstance(contracts, list) or not contracts:
        raise ContractError("pathContracts must be a non-empty array")
    categories: set[str] = set()
    contract_ids: set[str] = set()
    for index, contract in enumerate(contracts):
        if not isinstance(contract, dict):
            raise ContractError(f"pathContracts[{index}] must be an object")
        expected = {"pathContractId", "category", "includePatterns", "excludePatterns", "gateIds"}
        if contract.get("category") == "product":
            expected.add("productJobId")
        _require_exact_keys(contract, expected, f"pathContracts[{index}]")
        contract_id = validate_ci_input_component(contract["pathContractId"])
        if contract_id in contract_ids:
            raise ContractError(f"duplicate pathContractId: {contract_id}")
        contract_ids.add(contract_id)
        category = contract["category"]
        if category not in CI_PATH_CATEGORIES:
            raise ContractError(f"invalid path category: {category}")
        categories.add(category)
        for field in ("includePatterns", "excludePatterns"):
            patterns = contract[field]
            if not isinstance(patterns, list) or (field == "includePatterns" and not patterns):
                raise ContractError(f"{contract_id}.{field} is invalid")
            if len(patterns) != len(set(patterns)):
                raise ContractError(f"{contract_id}.{field} contains duplicates")
            for pattern in patterns:
                if (
                    not isinstance(pattern, str)
                    or not pattern
                    or pattern.startswith(("/", "-"))
                    or "\\" in pattern
                    or any(character in pattern for character in ("\r", "\n", "\0"))
                    or ".." in pathlib.PurePosixPath(pattern).parts
                ):
                    raise ContractError(f"unsafe path pattern: {pattern!r}")
        references = contract["gateIds"]
        if not isinstance(references, list) or not references or len(references) != len(set(references)):
            raise ContractError(f"invalid gate references for {contract_id}")
        unknown_gates = set(references) - gate_ids
        if unknown_gates:
            raise ContractError(f"unknown gate references: {sorted(unknown_gates)}")
        if category == "product" and contract["productJobId"] not in product_job_ids:
            raise ContractError(f"unknown product job reference: {contract['productJobId']}")
    if categories != CI_PATH_CATEGORIES:
        raise ContractError(f"all six path categories are required, found {sorted(categories)}")


def load_ci_budget_contracts(
    profiles_path: pathlib.Path,
    paths_path: pathlib.Path,
    workflow_template_path: pathlib.Path,
) -> dict:
    profiles = _read_ci_json(profiles_path, "profile registry")
    paths = _read_ci_json(paths_path, "path registry")
    _, gate_ids = _validate_ci_profiles(profiles)
    try:
        workflow_text = workflow_template_path.read_text(encoding="utf-8")
    except (OSError, UnicodeError) as exc:
        raise ContractError(f"workflow template cannot be read: {exc}") from exc
    product_job_ids = set(re.findall(
        r"^# home-baseline-product-job:\s*([a-z0-9][a-z0-9-]*)\s*$",
        workflow_text,
        re.MULTILINE,
    ))
    if not product_job_ids:
        raise ContractError("workflow template declares no stable product job IDs")
    _validate_ci_paths(paths, gate_ids, product_job_ids)
    return {
        "profiles": profiles,
        "paths": paths,
        "profileRegistryHash": canonical_json_hash(profiles),
        "pathContractHash": canonical_json_hash(paths),
        "loadCounts": {"profiles": 1, "paths": 1},
    }


def validate_private_governance_workflow_paths(
    workflow_template_path: pathlib.Path,
    path_registry: dict,
) -> None:
    """Bind the live target workflow to the canonical path registry exactly."""
    workflow_text = workflow_template_path.read_text(encoding="utf-8")
    paths_block = re.search(
        r"^    paths:\s*$\n(?P<body>(?:^      - .+$\n?)+)",
        workflow_text,
        re.MULTILINE,
    )
    if paths_block is None:
        raise ContractError("private governance workflow path filter is missing")
    workflow_paths: list[str] = []
    for line in paths_block.group("body").splitlines():
        value = line.removeprefix("      - ").strip()
        if len(value) >= 2 and value[0] == value[-1] and value[0] in {"'", '"'}:
            value = value[1:-1]
        workflow_paths.append(value)
    expected_workflow_paths: list[str] = []
    for contract in path_registry["pathContracts"]:
        expected_workflow_paths.extend(contract["includePatterns"])
        expected_workflow_paths.extend(f"!{pattern}" for pattern in contract["excludePatterns"])
    if workflow_paths != expected_workflow_paths:
        raise ContractError("private governance workflow paths differ from the canonical path registry")


def load_manifest(path: pathlib.Path) -> dict:
    try:
        data = json.loads(path.read_text(encoding="utf-8-sig"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError(f"manifest cannot be read: {exc}") from exc
    if not isinstance(data, dict) or data.get("schemaVersion") != "1.0":
        raise ContractError("unsupported manifest schemaVersion")
    targets = data.get("targets")
    if not isinstance(targets, list) or not targets:
        raise ContractError("targets must be a non-empty array")

    ids: set[str] = set()
    paths: dict[str, dict] = {}
    remotes: set[str] = set()
    for index, target in enumerate(targets):
        if not isinstance(target, dict):
            raise ContractError(f"targets[{index}] must be an object")
        allowed = {
            "id", "kind", "level", "path", "active", "maintenanceClass",
            "remote", "forge", "defaultBranch", "memberDiscovery",
        }
        unknown = set(target) - allowed
        if unknown:
            raise ContractError(f"unknown target fields at targets[{index}]: {sorted(unknown)}")
        required = {"id", "kind", "level", "path", "active", "maintenanceClass"}
        if not required.issubset(target):
            raise ContractError(f"missing target fields at targets[{index}]")
        target_id = target.get("id")
        if not isinstance(target_id, str) or not re.fullmatch(r"[a-z0-9][a-z0-9-]*", target_id):
            raise ContractError(f"targets[{index}].id is invalid")
        if target_id in ids:
            raise ContractError(f"duplicate target id: {target_id}")
        ids.add(target_id)
        kind = target.get("kind")
        level = target.get("level")
        maintenance_class = target.get("maintenanceClass")
        if kind not in VALID_KINDS or level not in {1, 2} or maintenance_class not in VALID_CLASSES:
            raise ContractError(f"invalid target classification: {target_id}")
        if not isinstance(target.get("active"), bool):
            raise ContractError(f"active must be boolean: {target_id}")
        relative = validate_relative_path(target.get("path"))
        path_key = relative.as_posix().casefold()
        if path_key in paths:
            raise ContractError(f"duplicate target path: {relative}")
        paths[path_key] = target
        if kind == "collection":
            forbidden = {"remote", "forge", "defaultBranch"} & target.keys()
            if forbidden or target.get("memberDiscovery") != "declared-targets":
                raise ContractError(f"invalid collection fields: {target_id}")
        else:
            branch = target.get("defaultBranch")
            if target.get("forge") not in VALID_FORGES or not isinstance(branch, str) or not branch.strip():
                raise ContractError(f"invalid Git target fields: {target_id}")
            remote = target.get("remote")
            if not isinstance(remote, str) or not remote:
                raise ContractError(f"missing remote: {target_id}")
            normalized = normalize_remote(remote)
            if normalized in remotes and target.get("active"):
                raise ContractError(f"duplicate active remote: {remote}")
            if target.get("active"):
                remotes.add(normalized)

    # Eltern werden exakt deklariert, damit Discovery keine unbekannten Ziele legitimiert.
    # Parents are explicit so discovery cannot legitimize unknown targets.
    for target in targets:
        if target["level"] != 2:
            continue
        parent = pathlib.PurePosixPath(target["path"]).parent
        parent_target = paths.get(parent.as_posix().casefold())
        if not parent_target or parent_target["level"] != 1 or not parent_target.get("active"):
            raise ContractError(f"orphan Level-2 target: {target['id']}")
    return data


def target_result(target: dict, **values: object) -> dict:
    result = {
        "targetId": target["id"],
        "path": target["path"],
        "kind": target["kind"],
        "maintenanceClass": target["maintenanceClass"],
        "status": "CURRENT",
        "action": "NONE",
        "result": "Pass",
        "branch": target.get("defaultBranch", "N/A"),
        "upstream": "N/A",
        "ahead": 0,
        "behind": 0,
        "findingCode": "N/A",
        "nextAction": "N/A",
        "retryAttempts": 0,
        "resumeAccepted": False,
        "freshnessAttempt": None,
        "defaultBranchEvidence": None,
        "mutationAllowed": target.get("kind") != "git-repository",
    }
    result.update(values)
    return result


def resolve_default_branch_evidence(
    target: dict,
    path: pathlib.Path,
    local_symbolic_ref: str,
) -> tuple[dict | None, dict | None, str | None]:
    if local_symbolic_ref:
        prefix = "refs/remotes/origin/"
        if not local_symbolic_ref.startswith(prefix):
            return None, None, "RemoteHeadInvalid"
        branch_name = local_symbolic_ref.removeprefix(prefix)
        if target.get("defaultBranch") and target["defaultBranch"] != branch_name:
            return None, None, "RemoteHeadMismatch"
        tracking = run_git(
            path, "rev-parse", "--verify", local_symbolic_ref, check=False
        )
        if tracking.returncode != 0:
            return None, None, "RemoteTrackingRefMissing"
        tracking_commit = tracking.stdout.strip().lower()
        return {
            "source": "LocalSymbolicHead",
            "symbolicRef": f"refs/heads/{branch_name}",
            "trackingRef": local_symbolic_ref,
            "remoteCommit": tracking_commit,
            "trackingCommit": tracking_commit,
            "validatedAt": utc_now(),
            "remoteHeadAttempt": None,
        }, None, None

    remote, attempts, duration_ms = run_git_network(
        path, "ls-remote", "--symref", "origin", "HEAD"
    )
    attempt = network_attempt("ls-remote", remote, attempts, duration_ms)
    if remote.returncode != 0:
        return None, attempt, "RemoteHeadUnavailable"

    symbolic_refs = []
    remote_commits = []
    for line in remote.stdout.splitlines():
        fields = line.split()
        if len(fields) >= 3 and fields[0] == "ref:" and fields[2] == "HEAD":
            symbolic_refs.append(fields[1])
        elif len(fields) >= 2 and fields[1] == "HEAD" and re.fullmatch(
            r"[0-9a-fA-F]{40,64}", fields[0]
        ):
            remote_commits.append(fields[0].lower())
    if len(set(symbolic_refs)) != 1 or len(set(remote_commits)) != 1:
        return None, attempt, "RemoteHeadAmbiguous"

    symbolic_ref = symbolic_refs[0]
    if not symbolic_ref.startswith("refs/heads/"):
        return None, attempt, "RemoteHeadInvalid"
    branch_name = symbolic_ref.removeprefix("refs/heads/")
    tracking_ref = f"refs/remotes/origin/{branch_name}"
    if target.get("defaultBranch") and target["defaultBranch"] != branch_name:
        return None, attempt, "RemoteHeadMismatch"

    tracking = run_git(path, "rev-parse", "--verify", tracking_ref, check=False)
    if tracking.returncode != 0:
        return None, attempt, "RemoteTrackingRefMissing"
    tracking_commit = tracking.stdout.strip().lower()
    remote_commit = remote_commits[0]
    if tracking_commit != remote_commit:
        return None, attempt, "RemoteCommitMismatch"
    return {
        "source": "RemoteSymbolicHead",
        "symbolicRef": symbolic_ref,
        "trackingRef": tracking_ref,
        "remoteCommit": remote_commit,
        "trackingCommit": tracking_commit,
        "validatedAt": utc_now(),
        "remoteHeadAttempt": attempt,
    }, attempt, None


def classify_repository(
    target: dict, path: pathlib.Path, mode: str, allowed_dirty_paths: set[str] | None = None
) -> dict:
    if path.is_symlink() or (path.exists() and not path.is_dir()):
        return target_result(target, status="PATH_CONFLICT", result="Blocked", findingCode="PathConflict",
                             nextAction="Konfliktpfad nach manueller Prüfung entfernen oder verschieben / remove or relocate it after review.")
    if not path.exists():
        if mode == "check-only":
            return target_result(target, status="MISSING", action="CLONE_REQUIRED", result="Blocked",
                                 findingCode="MissingTarget", nextAction="Nach Remote-Prüfung im Update-Modus ausführen / run update after reviewing the remote.")
        if mode == "dry-run":
            return target_result(target, status="MISSING", action="WOULD_CLONE", result="Warning",
                                 findingCode="MissingTarget", nextAction="Update-Modus zum Klonen ausführen / run update to clone this target.")
        return clone_repository(target, path)
    if not (path / ".git").exists():
        return target_result(target, status="PATH_CONFLICT", result="Blocked", findingCode="PathConflict",
                             nextAction="Nicht-Git-Verzeichnis prüfen; es wird nie automatisch entfernt / review the directory; it is never removed.")

    origin = run_git(path, "remote", "get-url", "origin", check=False)
    if origin.returncode != 0 or normalize_remote(origin.stdout) != normalize_remote(target["remote"]):
        return target_result(target, status="REMOTE_MISMATCH", result="Blocked",
                             findingCode="RemoteMismatch", nextAction="origin nur nach manueller Prüfung korrigieren / correct origin only after review.")

    local_symbolic = run_git(
        path, "symbolic-ref", "--quiet", "refs/remotes/origin/HEAD", check=False
    )
    local_symbolic_ref = local_symbolic.stdout.strip() if local_symbolic.returncode == 0 else ""
    fetch, fetch_attempts, fetch_duration = run_git_network(path, "fetch", "--prune")
    freshness = network_attempt("fetch", fetch, fetch_attempts, fetch_duration)
    if fetch.returncode != 0:
        finding = "FetchTimedOut" if fetch.returncode == 124 else "FetchFailed"
        return target_result(
            target,
            status="UNAVAILABLE",
            result="Blocked",
            findingCode=finding,
            retryAttempts=fetch_attempts,
            freshnessAttempt=freshness,
            nextAction="Remote-Zugriff wiederherstellen und erneut ausführen / restore access and retry.",
        )

    default_evidence, _, default_error = resolve_default_branch_evidence(
        target, path, local_symbolic_ref
    )
    if default_error:
        return target_result(
            target,
            status="REMOTE_HEAD_INVALID",
            result="Blocked",
            findingCode=default_error,
            retryAttempts=fetch_attempts,
            freshnessAttempt=freshness,
            nextAction="Symbolischen origin-HEAD und Tracking-Ref prüfen / review the symbolic origin HEAD and tracking ref.",
        )

    branch = run_git(path, "symbolic-ref", "--quiet", "--short", "HEAD", check=False)
    if branch.returncode != 0:
        return target_result(
            target,
            status="DETACHED",
            result="Blocked",
            findingCode="DetachedHead",
            freshnessAttempt=freshness,
            defaultBranchEvidence=default_evidence,
            nextAction="Deklarierten Branch manuell auswählen / select the declared branch manually.",
        )
    branch_name = branch.stdout.strip()
    canonical_branch = default_evidence["symbolicRef"].removeprefix("refs/heads/")
    if branch_name != canonical_branch:
        return target_result(
            target,
            status="BRANCH_MISMATCH",
            result="Blocked",
            branch=branch_name,
            findingCode="BranchMismatch",
            freshnessAttempt=freshness,
            defaultBranchEvidence=default_evidence,
            nextAction="Nach Prüfung zum kanonischen Branch wechseln / switch to the canonical branch after review.",
        )

    dirty = run_git(path, "-c", "core.quotePath=false", "status", "--porcelain=v1", "--untracked-files=all").stdout
    resume_accepted = False
    if dirty:
        dirty_paths = {
            f"{target['path']}/{line[3:].split(' -> ')[-1]}".replace("\\", "/")
            for line in dirty.splitlines()
            if len(line) >= 4
        }
        resume_accepted = bool(allowed_dirty_paths) and dirty_paths.issubset(allowed_dirty_paths)
    if dirty and not resume_accepted:
        return target_result(
            target,
            status="DIRTY",
            result="Blocked",
            branch=branch_name,
            findingCode="DirtyWorktree",
            retryAttempts=fetch_attempts,
            freshnessAttempt=freshness,
            defaultBranchEvidence=default_evidence,
            nextAction="Lokale Arbeit ausdrücklich committen, stashen oder verwerfen / handle local work explicitly.",
        )
    upstream = run_git(path, "rev-parse", "--abbrev-ref", "--symbolic-full-name", "@{upstream}", check=False)
    if upstream.returncode != 0:
        return target_result(
            target,
            status="MISSING_UPSTREAM",
            result="Blocked",
            branch=branch_name,
            findingCode="MissingUpstream",
            freshnessAttempt=freshness,
            defaultBranchEvidence=default_evidence,
            nextAction="Deklarierten origin-Branch als Upstream setzen / set the declared origin branch as upstream.",
        )
    upstream_name = upstream.stdout.strip()
    if upstream_name != f"origin/{canonical_branch}":
        return target_result(
            target,
            status="MISSING_UPSTREAM",
            result="Blocked",
            branch=branch_name,
            upstream=upstream_name,
            findingCode="UpstreamMismatch",
            freshnessAttempt=freshness,
            defaultBranchEvidence=default_evidence,
            nextAction="Upstream auf den kanonischen origin-Branch ausrichten / align upstream with the canonical origin branch.",
        )
    counts = run_git(path, "rev-list", "--left-right", "--count", f"HEAD...{upstream_name}").stdout.split()
    ahead, behind = map(int, counts)
    if ahead and behind:
        return target_result(
            target, status="DIVERGED", result="Blocked", branch=branch_name,
            upstream=upstream_name, ahead=ahead, behind=behind, findingCode="Diverged",
            freshnessAttempt=freshness, defaultBranchEvidence=default_evidence,
            nextAction="Divergenz manuell lösen; kein Reset oder Force-Push / resolve manually; no reset or force push."
        )
    if ahead:
        return target_result(
            target, status="AHEAD", result="Blocked", branch=branch_name,
            upstream=upstream_name, ahead=ahead, findingCode="Ahead",
            freshnessAttempt=freshness, defaultBranchEvidence=default_evidence,
            nextAction="Lokale Commits separat prüfen und pushen / review and push separately."
        )
    if behind:
        if dirty:
            return target_result(
                target, status="DIRTY", result="Blocked", branch=branch_name,
                upstream=upstream_name, behind=behind, findingCode="DirtyWorktree",
                retryAttempts=fetch_attempts, resumeAccepted=resume_accepted,
                freshnessAttempt=freshness, defaultBranchEvidence=default_evidence,
                nextAction="Dirty-Zustand vor einem Fast-forward ausdrücklich auflösen / resolve dirty state before fast-forward."
            )
        if mode == "check-only":
            return target_result(
                target, status="BEHIND", action="PULL_REQUIRED", result="Blocked",
                branch=branch_name, upstream=upstream_name, behind=behind,
                findingCode="Behind", freshnessAttempt=freshness,
                defaultBranchEvidence=default_evidence,
                nextAction="Update-Modus für Fast-forward ausführen / run update for fast-forward."
            )
        if mode == "dry-run":
            return target_result(
                target, status="BEHIND", action="WOULD_PULL", result="Warning",
                branch=branch_name, upstream=upstream_name, behind=behind,
                findingCode="Behind", freshnessAttempt=freshness,
                defaultBranchEvidence=default_evidence,
                nextAction="Update-Modus zum Fast-forward ausführen / run update to fast-forward."
            )
        pull, pull_attempts, pull_duration = run_git_network(path, "pull", "--ff-only")
        pull_evidence = network_attempt("pull", pull, pull_attempts, pull_duration)
        if pull.returncode != 0:
            return target_result(
                target, status="UNAVAILABLE", action="PULL", result="Failed",
                branch=branch_name, upstream=upstream_name, behind=behind,
                retryAttempts=pull_attempts, resumeAccepted=resume_accepted,
                findingCode="PullFailed", freshnessAttempt=freshness,
                defaultBranchEvidence=default_evidence, pullAttempt=pull_evidence,
                nextAction="Log prüfen, manuell reparieren und erneut ausführen / inspect, repair and retry."
            )
        return target_result(
            target, status="UPDATED", action="PULL", branch=branch_name,
            upstream=upstream_name, retryAttempts=pull_attempts,
            resumeAccepted=resume_accepted, freshnessAttempt=freshness,
            defaultBranchEvidence=default_evidence, pullAttempt=pull_evidence,
            mutationAllowed=not dirty or resume_accepted
        )
    return target_result(
        target, branch=branch_name, upstream=upstream_name,
        retryAttempts=fetch_attempts, resumeAccepted=resume_accepted,
        freshnessAttempt=freshness, defaultBranchEvidence=default_evidence,
        mutationAllowed=not dirty or resume_accepted
    )


def clone_repository(target: dict, path: pathlib.Path) -> dict:
    path.parent.mkdir(parents=True, exist_ok=True)
    # Erst der geprüfte Geschwisterklon wird atomar sichtbar; Teilklone bleiben nie Sollzustand.
    # Only a verified sibling becomes visible; partial clones never become desired state.
    temporary = pathlib.Path(tempfile.mkdtemp(prefix=f".{path.name}.clone-", dir=path.parent))
    try:
        shutil.rmtree(temporary)
        clone, clone_attempts, clone_duration = run_git_network(
            None, "clone", "--origin", "origin", "--branch", target["defaultBranch"],
            "--single-branch", "--", target["remote"], str(temporary)
        )
        if clone.returncode != 0:
            return target_result(target, status="UNAVAILABLE", action="CLONE", result="Failed",
                                 retryAttempts=clone_attempts,
                                 freshnessAttempt=network_attempt(
                                     "clone", clone, clone_attempts, clone_duration
                                 ),
                                 findingCode="CloneFailed", nextAction="Remote prüfen und erneut ausführen; Ziel nicht akzeptiert / inspect and retry; target not accepted.")
        origin = run_git(temporary, "remote", "get-url", "origin").stdout
        branch = run_git(temporary, "branch", "--show-current").stdout.strip()
        dirty = run_git(temporary, "status", "--porcelain=v1", "--untracked-files=all").stdout
        if normalize_remote(origin) != normalize_remote(target["remote"]) or branch != target["defaultBranch"] or dirty:
            return target_result(target, status="UNAVAILABLE", action="CLONE", result="Failed",
                                 findingCode="CloneVerificationFailed",
                                 nextAction="Temporäre Clone-Evidence prüfen; Ziel nicht akzeptiert / inspect clone evidence; target not accepted.")
        os.replace(temporary, path)
        return target_result(
            target, status="CREATED", action="CLONE", branch=branch,
            upstream=f"origin/{branch}", retryAttempts=clone_attempts,
            freshnessAttempt=network_attempt("clone", clone, clone_attempts, clone_duration),
            mutationAllowed=True
        )
    finally:
        if temporary.exists():
            shutil.rmtree(temporary, ignore_errors=True)


def collection_result(target: dict, path: pathlib.Path, mode: str) -> dict:
    if path.is_symlink() or (path.exists() and not path.is_dir()):
        return target_result(target, status="PATH_CONFLICT", result="Blocked", findingCode="PathConflict",
                             nextAction="Konfliktpfad nach manueller Prüfung entfernen oder verschieben / remove or relocate it after review.")
    if path.exists():
        return target_result(target)
    if mode == "check-only":
        return target_result(target, status="MISSING", action="CREATE_REQUIRED", result="Blocked",
                             findingCode="MissingCollection", nextAction="Update-Modus zum Erstellen der Collection ausführen / run update to create it.")
    if mode == "dry-run":
        return target_result(target, status="MISSING", action="WOULD_CREATE", result="Warning",
                             findingCode="MissingCollection", nextAction="Update-Modus zum Erstellen der Collection ausführen / run update to create it.")
    path.mkdir(parents=True)
    return target_result(target, status="CREATED", action="CREATE")


def derive_status(results: list[dict], mode: str) -> tuple[str, int]:
    if any(item["result"] == "Failed" for item in results):
        return "PARTIAL", 1
    if any(item["result"] == "Blocked" for item in results):
        return "DRIFT", 1
    if any(item["result"] == "Warning" for item in results):
        return "DRIFT", 1
    return "SUCCESS", 0


def write_report(path: pathlib.Path, report: dict) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    # Der atomare Austausch erhält die letzte vollständige Evidence bei Schreibfehlern.
    # Atomic replacement preserves the last complete evidence on write failure.
    temporary = path.with_name(f".{path.name}.{uuid.uuid4().hex}.tmp")
    temporary.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    os.replace(temporary, path)


def process_identity(pid: int) -> str | None:
    if pid <= 0:
        return None
    if os.name == "nt":
        kernel32 = ctypes.windll.kernel32
        process = kernel32.OpenProcess(0x1000, False, pid)
        if not process:
            return None
        try:
            creation = ctypes.c_ulonglong()
            exit_time = ctypes.c_ulonglong()
            kernel = ctypes.c_ulonglong()
            user = ctypes.c_ulonglong()
            if not kernel32.GetProcessTimes(
                process,
                ctypes.byref(creation),
                ctypes.byref(exit_time),
                ctypes.byref(kernel),
                ctypes.byref(user),
            ):
                return None
            return f"windows-filetime:{creation.value}"
        finally:
            kernel32.CloseHandle(process)
    proc_stat = pathlib.Path(f"/proc/{pid}/stat")
    if proc_stat.is_file():
        try:
            fields = proc_stat.read_text(encoding="utf-8").split()
            return f"proc-start-ticks:{fields[21]}"
        except (OSError, UnicodeError, IndexError):
            return None
    result = subprocess.run(
        ["ps", "-o", "lstart=", "-p", str(pid)],
        text=True,
        capture_output=True,
        check=False,
    )
    value = result.stdout.strip()
    return f"ps-lstart:{value}" if result.returncode == 0 and value else None


def contained_path(raw: pathlib.Path, root: pathlib.Path, label: str) -> pathlib.Path:
    resolved = raw.resolve()
    try:
        resolved.relative_to(root.resolve())
    except ValueError as exc:
        raise ContractError(f"{label} escapes the reserved state directory") from exc
    return resolved


def load_worktree_lease(path: pathlib.Path, state_root: pathlib.Path) -> dict:
    lease_path = contained_path(path, state_root, "lease path")
    try:
        data = json.loads(lease_path.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError(f"lease cannot be read: {exc}") from exc
    required = {
        "schemaVersion",
        "runId",
        "ownerPid",
        "ownerProcessStartedAt",
        "repository",
        "remoteRef",
        "commit",
        "worktreePath",
        "leasePath",
        "createdAt",
    }
    if not isinstance(data, dict) or set(data) != required:
        raise ContractError("lease fields are incomplete or unknown")
    if data["schemaVersion"] != "1.0" or not isinstance(data["ownerPid"], int):
        raise ContractError("lease schema or owner PID is invalid")
    if pathlib.Path(data["leasePath"]).resolve() != lease_path:
        raise ContractError("lease path does not match its evidence")
    repository = pathlib.Path(data["repository"]).resolve()
    if not (repository / ".git").exists():
        raise ContractError("lease repository is not a Git checkout")
    worktree = contained_path(
        pathlib.Path(data["worktreePath"]), state_root, "worktree path"
    )
    if worktree == state_root.resolve():
        raise ContractError("worktree path cannot be the state root")
    if not re.fullmatch(r"[0-9a-fA-F]{40}|[0-9a-fA-F]{64}", str(data["commit"])):
        raise ContractError("lease commit is invalid")
    if not re.fullmatch(r"refs/remotes/origin/[A-Za-z0-9._/-]+", str(data["remoteRef"])):
        raise ContractError("lease remote ref is invalid")
    commit = run_git(repository, "cat-file", "-e", f"{data['commit']}^{{commit}}", check=False)
    if commit.returncode != 0:
        raise ContractError("lease commit is not present in its repository")
    data["_leasePath"] = lease_path
    data["_repository"] = repository
    data["_worktreePath"] = worktree
    return data


def worktree_registered(repository: pathlib.Path, worktree: pathlib.Path) -> bool:
    result = run_git(repository, "worktree", "list", "--porcelain", check=False)
    if result.returncode != 0:
        return False
    registered = {
        pathlib.Path(line.removeprefix("worktree ")).resolve()
        for line in result.stdout.splitlines()
        if line.startswith("worktree ")
    }
    return worktree.resolve() in registered


def remove_owned_worktree(lease: dict) -> None:
    repository = lease["_repository"]
    worktree = lease["_worktreePath"]
    lease_path = lease["_leasePath"]
    registered = worktree_registered(repository, worktree)
    if registered:
        worktree_head = run_git(worktree, "rev-parse", "HEAD", check=False)
        if (
            worktree_head.returncode != 0
            or worktree_head.stdout.strip().lower() != lease["commit"].lower()
        ):
            raise ContractError("registered worktree no longer matches its leased commit")
        status = run_git(worktree, "status", "--porcelain=v1", "-uall", check=False)
        if status.returncode != 0 or status.stdout:
            raise ContractError("registered worktree changed after cleanup authorization")
        removed = run_git(
            repository, "worktree", "remove", "--force", str(worktree), check=False
        )
        if removed.returncode != 0:
            raise ContractError("owned worktree could not be removed")
    elif worktree.exists():
        raise ContractError("unregistered worktree path is not safe to remove")
    # Die Kandidatenmenge wird nach Git erneut bestimmt; nur der nun leere,
    # leasegebundene Elternpfad darf entfernt werden.
    # Re-inventory after Git; only the now-empty lease-owned parent may be removed.
    parent = worktree.parent
    if parent.is_dir() and not any(parent.iterdir()):
        parent.rmdir()
    lease_path.unlink(missing_ok=True)


def create_worktree_lease(args: argparse.Namespace) -> int:
    try:
        state_root = args.state_root.resolve()
        state_root.mkdir(parents=True, exist_ok=True)
        lease_path = contained_path(args.lease, state_root, "lease path")
        worktree = contained_path(args.worktree, state_root, "worktree path")
        if lease_path.exists() or worktree.exists():
            raise ContractError("lease or worktree path already exists")
        if not args.run_id.strip():
            raise ContractError("lease run ID is empty")
        repository = args.repository.resolve()
        if not (repository / ".git").exists():
            raise ContractError("lease repository is not a Git checkout")
        identity = args.owner_process_identity or process_identity(args.owner_pid)
        if not identity:
            raise ContractError("owner process identity is unavailable")
        payload = {
            "schemaVersion": "1.0",
            "runId": args.run_id,
            "ownerPid": args.owner_pid,
            "ownerProcessStartedAt": identity,
            "repository": str(repository),
            "remoteRef": args.remote_ref,
            "commit": args.commit.lower(),
            "worktreePath": str(worktree),
            "leasePath": str(lease_path),
            "createdAt": utc_now(),
        }
        if not re.fullmatch(r"[0-9a-fA-F]{40}|[0-9a-fA-F]{64}", payload["commit"]):
            raise ContractError("lease commit is invalid")
        lease_path.parent.mkdir(parents=True, exist_ok=True)
        temporary = lease_path.with_name(f".{lease_path.name}.{uuid.uuid4().hex}.tmp")
        temporary.write_text(
            json.dumps(payload, ensure_ascii=False, indent=2) + "\n",
            encoding="utf-8",
        )
        os.replace(temporary, lease_path)
        print(f"LEASE\tCREATED\t{lease_path}")
        return 0
    except ContractError as exc:
        print(f"LEASE\tFAILED\t{exc}", file=sys.stderr)
        return 2


def release_worktree_lease(args: argparse.Namespace) -> int:
    try:
        lease_path = contained_path(args.lease, args.state_root.resolve(), "lease path")
        if not lease_path.exists():
            print(f"LEASE\tALREADY_RELEASED\t{lease_path}")
            return 0
        lease = load_worktree_lease(args.lease, args.state_root)
        if lease["runId"] != args.run_id:
            raise ContractError("lease run ID does not match release authority")
        remove_owned_worktree(lease)
        print(f"LEASE\tRELEASED\t{args.lease}")
        return 0
    except ContractError as exc:
        print(f"LEASE\tAMBIGUOUS\t{exc}", file=sys.stderr)
        return 1


def recover_worktree_leases(args: argparse.Namespace) -> int:
    state_root = args.state_root.resolve()
    lease_dir = contained_path(args.lease_dir, state_root, "lease directory")
    if not lease_dir.exists():
        print("LEASE_RECOVERY\tCURRENT\t0")
        return 0
    blocked = 0
    recovered = 0
    for lease_path in sorted(lease_dir.glob("*.json")):
        try:
            lease = load_worktree_lease(lease_path, state_root)
            current_identity = process_identity(lease["ownerPid"])
            if current_identity == lease["ownerProcessStartedAt"]:
                print(f"LEASE_RECOVERY\tACTIVE\t{lease_path.name}")
                continue
            if current_identity is not None:
                blocked += 1
                print(
                    f"LEASE_RECOVERY\tAMBIGUOUS_PID_REUSE\t{lease_path.name}",
                    file=sys.stderr,
                )
                continue
            remove_owned_worktree(lease)
            recovered += 1
            print(f"LEASE_RECOVERY\tRECOVERED\t{lease_path.name}")
        except ContractError as exc:
            blocked += 1
            print(
                f"LEASE_RECOVERY\tAMBIGUOUS\t{lease_path.name}\t{exc}",
                file=sys.stderr,
            )
    print(f"LEASE_RECOVERY\tSUMMARY\trecovered={recovered}\tblocked={blocked}")
    return 1 if blocked else 0


def execute_fleet(args: argparse.Namespace) -> int:
    started = time.monotonic()
    started_at = utc_now()
    run_id = args.run_id or str(uuid.uuid4())
    try:
        manifest = load_manifest(args.manifest)
    except ContractError as exc:
        report = {
            "schemaVersion": "1.0", "runId": run_id, "platform": sys.platform, "mode": args.mode,
            "startedAt": started_at, "completedAt": utc_now(), "overallStatus": "FAILED", "exitCode": 2,
            "stages": [{"stageId": "fleet", "status": "Failed", "exitCode": 2, "durationMs": 0,
                        "summary": str(exc), "nextAction": "Manifest korrigieren und erneut ausführen / correct the manifest and retry."}],
            "targets": [], "toolchain": [], "findings": [{"code": "ManifestInvalid", "severity": "Fatal",
            "summary": str(exc), "nextAction": "Manifest korrigieren und erneut ausführen / correct the manifest and retry."}],
            "artifacts": {"logPath": str(args.log), "reportPath": str(args.report)}
        }
        write_report(args.report, report)
        print(f"ERROR\tmanifest\tFAILED\t{exc}")
        return 2

    home = args.home_dir.resolve()
    allowed_dirty_paths = {item.replace("\\", "/") for item in args.allowed_dirty_path}
    results: list[dict] = []
    if args.level0_dir is not None:
        level0 = args.level0_dir.resolve()
        origin = run_git(level0, "remote", "get-url", "origin", check=False)
        branch = run_git(level0, "branch", "--show-current", check=False)
        if origin.returncode == 0 and branch.returncode == 0 and branch.stdout.strip():
            level0_target = {
                "id": "level0",
                "path": ".",
                "kind": "git-repository",
                "maintenanceClass": "canonical-fleet",
                "remote": origin.stdout.strip(),
                "defaultBranch": branch.stdout.strip(),
            }
            level0_result = classify_repository(
                level0_target, level0, args.mode, allowed_dirty_paths
            )
            results.append(level0_result)
            print(
                f"TARGET\tlevel0\t{level0_result['status']}\t"
                f"{level0_result['action']}\t{level0_result['nextAction']}"
            )
        else:
            results.append(
                target_result(
                    {
                        "id": "level0",
                        "path": ".",
                        "kind": "git-repository",
                        "maintenanceClass": "canonical-fleet",
                    },
                    status="REMOTE_MISMATCH",
                    result="Blocked",
                    findingCode="Level0OriginMissing",
                    nextAction="Level-0 origin und Branch prüfen / review Level 0 origin and branch.",
                )
            )
    for target in manifest["targets"]:
        if not target["active"]:
            continue
        relative = validate_relative_path(target["path"])
        target_path = home.joinpath(*relative.parts)
        result = (collection_result(target, target_path, args.mode)
                  if target["kind"] == "collection"
                  else classify_repository(target, target_path, args.mode, allowed_dirty_paths))
        results.append(result)
        print(f"TARGET\t{target['id']}\t{result['status']}\t{result['action']}\t{result['nextAction']}")

    overall, exit_code = derive_status(results, args.mode)
    findings = [
        {"targetId": item["targetId"], "code": item["findingCode"],
         "severity": "Blocking" if item["result"] in {"Blocked", "Failed"} else "Warning",
         "summary": item["status"], "nextAction": item["nextAction"]}
        for item in results if item["findingCode"] != "N/A"
    ]
    git_results = [item for item in results if item["kind"] == "git-repository"]
    collection_results = [item for item in results if item["kind"] == "collection"]
    completed_freshness = sum(
        isinstance(item.get("freshnessAttempt"), dict)
        and item["freshnessAttempt"].get("status") == "Succeeded"
        for item in git_results
    )
    fleet_ready = (
        len(git_results) == completed_freshness
        and all(item["result"] == "Pass" and item.get("mutationAllowed") for item in git_results)
    )
    operations = []
    for sequence, item in enumerate(results, start=1):
        attempt = item.get("freshnessAttempt")
        operations.append(
            {
                "sequence": sequence,
                "kind": (
                    attempt.get("operation", "inventory")
                    if isinstance(attempt, dict)
                    else "inventory"
                ),
                "targetId": item["targetId"],
                "status": (
                    attempt.get("status", item["status"])
                    if isinstance(attempt, dict)
                    else item["status"]
                ),
            }
        )
    operations.append(
        {
            "sequence": len(operations) + 1,
            "kind": "mutation-barrier",
            "targetId": "fleet",
            "status": "Open" if fleet_ready else "Blocked",
        }
    )
    report = {
        "schemaVersion": "1.0", "runId": run_id, "platform": sys.platform, "mode": args.mode,
        "startedAt": started_at, "completedAt": utc_now(), "overallStatus": overall, "exitCode": exit_code,
        "stages": [{"stageId": "fleet", "status": "Passed" if exit_code == 0 else "Blocked",
                    "exitCode": exit_code, "durationMs": int((time.monotonic() - started) * 1000),
                    "summary": f"{len(results)} active targets evaluated.",
                    "nextAction": "N/A" if exit_code == 0 else "Blockierende Zielbefunde beheben / resolve blocking target findings."}],
        "targets": results, "toolchain": [], "findings": findings,
        "operations": operations,
        "mutationBarrier": {
            "expectedGitTargets": len(git_results),
            "completedGitTargets": completed_freshness,
            "collectionTargets": len(collection_results),
            "allFetchAttemptsCompleted": completed_freshness == len(git_results),
            "fleetReady": fleet_ready,
            "domainMutationAllowed": fleet_ready and args.mode == "update",
            "decidedAt": utc_now(),
            "nextAction": (
                "N/A"
                if fleet_ready
                else "Alle blockierenden Flottenbefunde beheben / resolve all blocking fleet findings."
            ),
        },
        "counts": {
            "targets": len(results),
            "passed": sum(item["result"] == "Pass" for item in results),
            "warnings": sum(item["result"] == "Warning" for item in results),
            "blocked": sum(item["result"] == "Blocked" for item in results),
            "failed": sum(item["result"] == "Failed" for item in results)
        },
        "artifacts": {"logPath": str(args.log), "reportPath": str(args.report)}
    }
    write_report(args.report, report)
    print(f"SUMMARY\tfleet\t{overall}\t{exit_code}\t{args.report}")
    return exit_code


def read_toolchain_result(path: pathlib.Path) -> tuple[dict | None, str | None]:
    if not path.exists():
        return None, "ResultMissing"
    if not path.is_file():
        return None, "ResultNotFile"
    try:
        payload = path.read_bytes()
    except OSError:
        return None, "ResultUnreadable"
    if not payload.strip():
        return None, "ResultEmpty"
    try:
        text = payload.decode("utf-8-sig")
    except UnicodeDecodeError:
        return None, "ResultInvalidUtf8"
    try:
        result = json.loads(text)
    except json.JSONDecodeError:
        failure = "ResultTruncated" if not text.rstrip().endswith("}") else "ResultMalformed"
        return None, failure
    if not isinstance(result, dict):
        return None, "ResultSchemaMismatch"
    required_types = {
        "schemaVersion": str,
        "platform": str,
        "mode": str,
        "overallStatus": str,
        "exitCode": int,
        "items": list,
        "remainingRequired": list,
        "optionalDrift": list,
        "nextAction": str,
    }
    if any(not isinstance(result.get(key), expected) for key, expected in required_types.items()):
        return None, "ResultSchemaMismatch"
    expected_exit = 2 if result["overallStatus"] == "FAILED" else 1 if result["overallStatus"] == "PARTIAL" else 0
    if (
        result["schemaVersion"] != "1.0"
        or result["platform"] not in {"Darwin", "Linux"}
        or result["mode"] not in {"update", "dry-run", "compare-only"}
        or result["overallStatus"] not in {"SUCCESS", "SUCCESS_WITH_WARNINGS", "PARTIAL", "FAILED"}
        or result["exitCode"] != expected_exit
        or any(not isinstance(item, str) for item in result["remainingRequired"])
        or any(not isinstance(item, str) for item in result["optionalDrift"])
    ):
        return None, "ResultSchemaMismatch"
    return result, None


def read_storage_result(path: pathlib.Path) -> tuple[dict | None, str | None]:
    """Read the atomically published storage result with a fail-closed schema."""
    if not path.exists():
        return None, "ResultMissing"
    if not path.is_file():
        return None, "ResultNotFile"
    try:
        payload = path.read_bytes()
    except OSError:
        return None, "ResultUnreadable"
    if not payload.strip():
        return None, "ResultEmpty"
    try:
        text = payload.decode("utf-8-sig")
    except UnicodeDecodeError:
        return None, "ResultInvalidUtf8"
    try:
        result = json.loads(text)
    except json.JSONDecodeError:
        failure = "ResultTruncated" if not text.rstrip().endswith("}") else "ResultMalformed"
        return None, failure
    if not isinstance(result, dict):
        return None, "ResultSchemaMismatch"
    required_types = {
        "schemaVersion": str,
        "runId": str,
        "platform": str,
        "mode": str,
        "profile": str,
        "overallStatus": str,
        "exitCode": int,
        "repositories": list,
        "providers": list,
        "warnings": list,
        "nextAction": str,
    }
    if any(not isinstance(result.get(key), expected) for key, expected in required_types.items()):
        return None, "ResultSchemaMismatch"
    expected_exit = 2 if result["overallStatus"] == "FAILED" else 0
    if (
        result["schemaVersion"] != "1.0"
        or result["platform"] not in {"darwin", "linux", "win32"}
        or result["mode"] not in {"update", "dry-run", "check-only"}
        or result["profile"] not in {"safe", "deep", "none"}
        or result["overallStatus"] not in {"SUCCESS", "SUCCESS_WITH_WARNINGS", "FAILED"}
        or result["exitCode"] != expected_exit
        or any(not isinstance(item, str) for item in result["warnings"])
    ):
        return None, "ResultSchemaMismatch"
    try:
        if str(uuid.UUID(result["runId"])) != result["runId"]:
            return None, "ResultSchemaMismatch"
    except ValueError:
        return None, "ResultSchemaMismatch"
    return result, None


def record_stage(args: argparse.Namespace) -> int:
    try:
        report = json.loads(args.report.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        print(f"ERROR\treport\tFAILED\t{exc}")
        return 2
    stages = report.setdefault("stages", [])
    stages[:] = [item for item in stages if item.get("stageId") != args.stage_id]
    stage = {
        "stageId": args.stage_id,
        "status": args.status,
        "exitCode": args.exit_code,
        "durationMs": args.duration_ms,
        "summary": args.summary,
        "nextAction": args.next_action,
    }
    if args.evidence_path:
        stage["evidencePath"] = str(args.evidence_path)
    stages.append(stage)
    if args.toolchain_results:
        toolchain_result, failure_class = read_toolchain_result(args.toolchain_results)
        if failure_class:
            print(
                "ERROR\ttoolchain-results\tFAILED\t"
                f"{failure_class}\tRegenerate the toolchain result atomically."
            )
            return 2
        assert toolchain_result is not None
        items = toolchain_result.get("items")
        if (
            toolchain_result.get("schemaVersion") != "1.0"
            or not isinstance(items, list)
        ):
            print("ERROR\ttoolchain-results\tFAILED\tinvalid result schema")
            return 2
        report["toolchain"] = items
        report["toolchainResult"] = {
            "overallStatus": toolchain_result.get("overallStatus"),
            "exitCode": toolchain_result.get("exitCode"),
            "remainingRequired": toolchain_result.get("remainingRequired", []),
            "optionalDrift": toolchain_result.get("optionalDrift", []),
            "nextAction": toolchain_result.get("nextAction", "N/A"),
        }
        if "failureClass" in toolchain_result:
            report["toolchainResult"]["failureClass"] = toolchain_result["failureClass"]
    if args.storage_results:
        storage_result, failure_class = read_storage_result(args.storage_results)
        if failure_class:
            print(
                "ERROR\tstorage-results\tFAILED\t"
                f"{failure_class}\tRegenerate the storage result atomically."
            )
            return 2
        assert storage_result is not None
        if report.get("runId") and storage_result["runId"] != report["runId"]:
            print("ERROR\tstorage-results\tFAILED\tRunMismatch")
            return 2
        report["storageCleanup"] = storage_result
        report.setdefault("artifacts", {})["storageReportPath"] = str(args.storage_results)
    report["completedAt"] = utc_now()
    statuses = {item.get("status") for item in stages}
    if "Interrupted" in statuses:
        report["overallStatus"] = "INTERRUPTED"
        report["exitCode"] = max(
            int(item.get("exitCode", 0))
            for item in stages
            if item.get("status") == "Interrupted"
        )
    elif "Failed" in statuses:
        report["overallStatus"], report["exitCode"] = "FAILED", 2
    elif statuses.intersection({"Blocked", "DeferredAdminRequired"}):
        report["overallStatus"], report["exitCode"] = "PARTIAL", 1
    elif "Warning" in statuses:
        warning_exit = max(
            int(item.get("exitCode", 0))
            for item in stages
            if item.get("status") == "Warning"
        )
        report["overallStatus"] = "SUCCESS_WITH_WARNINGS"
        report["exitCode"] = warning_exit
    write_report(args.report, report)
    return 0


def finalize_report(args: argparse.Namespace) -> int:
    """Finalize exactly one run-correlated report through atomic replacement."""
    try:
        report = json.loads(args.report.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        print(f"ERROR\treport\tFAILED\t{exc}")
        return 2
    if report.get("finalized") is True:
        print(
            f"FINALIZE\t{report.get('runId', 'unknown')}\tALREADY_FINALIZED\t"
            f"{report.get('exitCode', 2)}"
        )
        return 0

    signal_name = args.signal
    canonical_signal_exit = {"INT": 130, "TERM": 143}
    if signal_name != "N/A":
        expected = canonical_signal_exit.get(signal_name)
        if expected is None or args.status != "Interrupted" or args.exit_code != expected:
            print("ERROR\tfinalize\tFAILED\tinvalid signal finalization contract")
            return 2

    stages = report.setdefault("stages", [])
    stages[:] = [item for item in stages if item.get("stageId") != args.stage_id]
    stages.append(
        {
            "stageId": args.stage_id,
            "status": args.status,
            "exitCode": args.exit_code,
            "durationMs": args.duration_ms,
            "summary": args.summary,
            "nextAction": args.next_action,
        }
    )
    if args.status == "Interrupted":
        overall = "INTERRUPTED"
    elif args.status == "Failed":
        overall = "FAILED"
    elif args.status in {"Blocked", "DeferredAdminRequired"}:
        overall = "PARTIAL"
    elif args.status == "Warning":
        overall = "SUCCESS_WITH_WARNINGS"
    else:
        overall = "SUCCESS"
    completed_at = utc_now()
    report.update(
        {
            "completedAt": completed_at,
            "overallStatus": overall,
            "exitCode": args.exit_code,
            "lastStage": args.stage_id,
            "signal": signal_name,
            "finalized": True,
            "finalizedAt": completed_at,
            "nextAction": args.next_action,
        }
    )
    write_report(args.report, report)
    print(
        f"FINALIZE\t{report.get('runId', 'unknown')}\t{overall}\t"
        f"{args.exit_code}\t{args.stage_id}"
    )
    return 0


def validate_registry(args: argparse.Namespace) -> int:
    try:
        manifest = load_manifest(args.manifest)
        registry = json.loads(args.registry.read_text(encoding="utf-8-sig"))
    except (ContractError, OSError, UnicodeError, json.JSONDecodeError) as exc:
        print(f"REGISTRY\tFAILED\t{exc}")
        return 2
    expected = {
        item["path"].casefold()
        for item in manifest["targets"]
        if item["active"] and item["kind"] == "git-repository"
        and item["maintenanceClass"] == "canonical-fleet"
    }
    entries = registry.get("repositories", []) if isinstance(registry, dict) else registry
    if not isinstance(entries, list):
        print("REGISTRY\tFAILED\trepositories must be an array")
        return 2
    actual: set[str] = set()
    language_findings: list[str] = []
    for entry in entries:
        if not isinstance(entry, dict) or not isinstance(entry.get("path"), str):
            print("REGISTRY\tFAILED\tinvalid repository entry")
            return 2
        path = validate_relative_path(entry["path"]).as_posix().casefold()
        if path not in expected:
            print(f"REGISTRY\tFAILED\tnon-canonical propagation target: {entry['path']}")
            return 2
        if path in actual:
            print(f"REGISTRY\tFAILED\tduplicate propagation target: {entry['path']}")
            return 2
        actual.add(path)
        language = str(entry.get("primaryLanguage", "")).strip().casefold()
        status = str(entry.get("mslStatus", "")).strip().casefold()
        expected_status = None
        if language in KNOWN_MSL_LANGUAGES:
            expected_status = "msl"
        elif language in KNOWN_NON_MSL_LANGUAGES:
            expected_status = "non-msl"
        elif language == "none":
            expected_status = "n/a"
        if expected_status is not None and status != expected_status:
            language_findings.append(
                f"{entry['path']}: language={entry.get('primaryLanguage')} "
                f"expects mslStatus={expected_status}, found {entry.get('mslStatus')}"
            )
    missing = expected - actual
    if missing:
        print(f"REGISTRY\tDRIFT\tmissing canonical targets: {len(missing)}")
        return 1
    if language_findings:
        for finding in language_findings:
            print(
                "REGISTRY\tLANGUAGE_MSL_CONFLICT\t"
                f"{finding}\tnext=Kuratierte Registry-Einstufung prüfen / review curated classification"
            )
        return 1
    print(f"REGISTRY\tCURRENT\tcanonical targets: {len(actual)}")
    return 0


def resolve_preset_profile(args: argparse.Namespace) -> int:
    try:
        catalog = json.loads(args.catalog.read_text(encoding="utf-8-sig"))
        profiles = catalog.get("profiles", {}) if isinstance(catalog, dict) else {}
        if not isinstance(profiles, dict):
            raise ContractError("preset profiles must be an object")
        profile_name = args.profile or catalog.get("defaultProfile")
        if not isinstance(profile_name, str) or profile_name not in profiles:
            raise ContractError(f"unknown preset profile: {profile_name}")
        profile = profiles[profile_name]
        relative = profile.get("presetConfig") if isinstance(profile, dict) else None
        if not isinstance(relative, str) or not relative:
            raise ContractError(f"preset profile has no matrix: {profile_name}")
        source_root = args.source_root.resolve()
        config = contained_path(source_root / relative, source_root, "preset matrix")
        matrix = json.loads(config.read_text(encoding="utf-8-sig"))
        presets = matrix.get("presets", []) if isinstance(matrix, dict) else []
        preset_ids = [
            item.get("id") for item in presets
            if isinstance(item, dict) and isinstance(item.get("id"), str)
        ]
        if not preset_ids or len(preset_ids) != len(presets) or len(set(preset_ids)) != len(preset_ids):
            raise ContractError("preset matrix IDs are empty, invalid or duplicated")
        result = {
            "profileName": profile_name,
            "presetConfig": str(config),
            "presetIds": preset_ids,
            "presetCount": len(preset_ids),
        }
        if args.field == "path":
            print(result["presetConfig"])
        else:
            print(json.dumps(result, ensure_ascii=False, separators=(",", ":")))
        return 0
    except (ContractError, OSError, UnicodeError, json.JSONDecodeError) as exc:
        print(f"PROFILE\tFAILED\t{exc}", file=sys.stderr)
        return 2


def list_canonical_repositories(args: argparse.Namespace) -> int:
    """Print active canonical Git repositories as level/path TSV records."""
    try:
        manifest = load_manifest(args.manifest)
    except ContractError as exc:
        print(f"REPOSITORIES\tFAILED\t{exc}", file=sys.stderr)
        return 2

    home = args.home_dir.resolve()
    repositories: list[tuple[int, pathlib.Path]] = []
    for target in manifest["targets"]:
        if (
            not target["active"]
            or target["kind"] != "git-repository"
            or target["maintenanceClass"] != "canonical-fleet"
        ):
            continue
        relative = validate_relative_path(target["path"])
        repository = home.joinpath(*relative.parts).resolve()
        try:
            repository.relative_to(home)
        except ValueError:
            print(
                f"REPOSITORIES\tFAILED\ttarget resolves outside HOME: {target['path']}",
                file=sys.stderr,
            )
            return 2
        if args.existing_only and not (repository / ".git").is_dir():
            continue
        repositories.append((target["level"], repository))

    for level, repository in sorted(repositories, key=lambda item: (item[0], str(item[1]).casefold())):
        print(f"{level}\t{repository}")
    return 0


def print_default_remote_ref(args: argparse.Namespace) -> int:
    repository = args.repository.resolve()
    origin = run_git(repository, "remote", "get-url", "origin", check=False)
    if origin.returncode != 0:
        print("DEFAULT_REF\tFAILED\torigin is unavailable", file=sys.stderr)
        return 2
    local = run_git(
        repository,
        "symbolic-ref",
        "--quiet",
        "refs/remotes/origin/HEAD",
        check=False,
    )
    local_ref = local.stdout.strip() if local.returncode == 0 else ""
    evidence, _, error = resolve_default_branch_evidence(
        {"remote": origin.stdout.strip(), "defaultBranch": ""},
        repository,
        local_ref,
    )
    if error or evidence is None:
        print(f"DEFAULT_REF\tFAILED\t{error or 'unknown'}", file=sys.stderr)
        return 1
    print(evidence["trackingRef"])
    return 0


def append_maintenance_event(args: argparse.Namespace) -> int:
    """Append one validated, complete JSONL maintenance event."""
    try:
        run_id = str(uuid.UUID(args.run_id))
        details = json.loads(args.details_json)
        if not isinstance(details, dict):
            raise ContractError("event details must be an object")
        if args.sequence < 1:
            raise ContractError("event sequence must be positive")
        event_stream = args.event_stream.resolve()
        event_stream.parent.mkdir(mode=0o700, parents=True, exist_ok=True)
        payload = {
            "schemaVersion": 1,
            "runId": run_id,
            "sequence": args.sequence,
            "timestampUtc": utc_now(),
            "eventType": args.event_type,
            "status": args.status,
            "phaseId": args.phase_id,
            "targetId": args.target_id,
            "messageDe": args.message_de,
            "messageEn": args.message_en,
            "details": details,
        }
        encoded = (
            json.dumps(payload, ensure_ascii=False, separators=(",", ":")) + "\n"
        ).encode("utf-8")
        descriptor = os.open(
            event_stream,
            os.O_APPEND | os.O_CREAT | os.O_WRONLY,
            0o600,
        )
        try:
            os.write(descriptor, encoded)
            os.fsync(descriptor)
        finally:
            os.close(descriptor)
        return 0
    except (ContractError, OSError, UnicodeError, ValueError, json.JSONDecodeError) as exc:
        print(f"EVENT\tFAILED\t{exc}", file=sys.stderr)
        return 2


class CIGateBlocked(RuntimeError):
    """Raised for a validated input whose current gate decision is blocked."""


def _ci_platform() -> str:
    if sys.platform == "darwin":
        return "macos"
    if sys.platform.startswith("win"):
        return "windows"
    return "linux"


def _ci_repository_id(repository: pathlib.Path, explicit: str | None) -> str:
    if explicit:
        return validate_ci_input_component(explicit)
    origin = run_git(repository, "remote", "get-url", "origin", check=False)
    if origin.returncode != 0:
        raise ContractError("configured origin is required to resolve repositoryId")
    normalized = normalize_remote(origin.stdout.strip())
    if normalized.endswith("/home-baseline"):
        return "home-baseline"
    return validate_ci_input_component(pathlib.PurePosixPath(normalized).name)


def _ci_selected_gates(contracts: dict, profile: dict, changed_paths: list[str]) -> tuple[list[dict], list[str]]:
    gate_set = next(
        (item for item in contracts["profiles"]["gateSets"] if item["gateSetId"] == profile["gateSetId"]),
        None,
    )
    if gate_set is None:
        raise ContractError(f"unknown gateSetId: {profile['gateSetId']}")
    normalized_paths = sorted({_normalize_changed_path(path) for path in changed_paths})
    matched_contracts: list[dict] = []
    selected_ids: set[str] = set()
    for contract in contracts["paths"]["pathContracts"]:
        if not normalized_paths:
            continue
        matched_paths = [
            path for path in normalized_paths
            if any(fnmatch.fnmatch(path, pattern) for pattern in contract["includePatterns"])
            and not any(fnmatch.fnmatch(path, pattern) for pattern in contract["excludePatterns"])
        ]
        if matched_paths:
            matched_contracts.append(contract)
            selected_ids.update(contract["gateIds"])
    if not selected_ids:
        selected_ids = {gate["gateId"] for gate in gate_set["gates"]}
    selected = [gate for gate in gate_set["gates"] if gate["gateId"] in selected_ids]
    if len(selected) != len(selected_ids):
        raise ContractError("selected gate set contains an unknown gate")
    return selected, sorted(contract["pathContractId"] for contract in matched_contracts)


def _normalize_changed_path(raw: str) -> str:
    if (
        not isinstance(raw, str)
        or not raw
        or raw.startswith(("/", "-"))
        or "\\" in raw
        or any(character in raw for character in ("\r", "\n", "\0"))
        or ".." in pathlib.PurePosixPath(raw).parts
    ):
        raise ContractError(f"unsafe changed path: {raw!r}")
    return pathlib.PurePosixPath(raw).as_posix()


def _ci_gate_set_hash(
    gate_set_id: str, version: str, gates: list[dict], matched_contract_ids: list[str]
) -> str:
    return canonical_json_hash(
        {
            "schemaVersion": "1.0",
            "gateSetId": gate_set_id,
            "version": version,
            "gates": gates,
            "matchedPathContractIds": matched_contract_ids,
        }
    )


def _ci_head(repository: pathlib.Path, fixture_head: str | None = None) -> str:
    sequence_file = os.environ.get("HB_CI_HEAD_SEQUENCE_FILE")
    if sequence_file:
        sequence_path = pathlib.Path(sequence_file)
        values = sequence_path.read_text(encoding="utf-8").splitlines()
        if not values:
            raise ContractError("injected HEAD sequence is exhausted")
        head = values.pop(0)
        sequence_path.write_text("\n".join(values) + ("\n" if values else ""), encoding="utf-8")
        if not re.fullmatch(r"[0-9a-f]{40}|[0-9a-f]{64}", head):
            raise ContractError("injected HEAD is invalid")
        return head
    if fixture_head is not None:
        if not re.fullmatch(r"[0-9a-f]{40}|[0-9a-f]{64}", fixture_head):
            raise ContractError("fixture head must be a full lowercase Git object ID")
        return fixture_head
    result = run_git(repository, "rev-parse", "HEAD", check=False)
    head = result.stdout.strip()
    if result.returncode != 0 or not re.fullmatch(r"[0-9a-f]{40}|[0-9a-f]{64}", head):
        raise ContractError("a stable full HEAD is required")
    return head


def _ci_evidence_path(
    root: pathlib.Path, repository_id: str, head: str, *, create_parent: bool = True
) -> pathlib.Path:
    repository_id = validate_ci_input_component(repository_id)
    if not re.fullmatch(r"[0-9a-f]{40}|[0-9a-f]{64}", head):
        raise ContractError("unsafe evidence HEAD")
    expanded_root = root.expanduser()
    probe = expanded_root if expanded_root.is_absolute() else pathlib.Path.cwd() / expanded_root
    # macOS exposes /var as the supported /private/var alias. Reject an
    # attacker-controlled evidence-root link itself while canonicalizing
    # platform-owned ancestor aliases.
    if probe.exists() and probe.is_symlink():
        raise ContractError(f"unsafe symlink evidence parent: {probe}")
    root = expanded_root.resolve(strict=False)
    destination = root / repository_id / f"{head}.json"
    destination_parent = destination.parent
    if create_parent:
        destination_parent.mkdir(mode=0o700, parents=True, exist_ok=True)
    if destination_parent.exists() and destination_parent.is_symlink():
        raise ContractError("unsafe symlink evidence directory")
    return destination


def _atomic_ci_json(path: pathlib.Path, value: dict) -> None:
    descriptor, temporary_name = tempfile.mkstemp(prefix=f".{path.name}.", dir=path.parent)
    temporary = pathlib.Path(temporary_name)
    try:
        os.fchmod(descriptor, 0o600)
        encoded = (json.dumps(value, ensure_ascii=False, separators=(",", ":")) + "\n").encode("utf-8")
        os.write(descriptor, encoded)
        os.fsync(descriptor)
        os.close(descriptor)
        descriptor = -1
        parsed = json.loads(temporary.read_text(encoding="utf-8"))
        _validate_ci_gate_evidence(parsed)
        os.replace(temporary, path)
        try:
            directory_descriptor = os.open(path.parent, os.O_RDONLY)
            try:
                os.fsync(directory_descriptor)
            finally:
                os.close(directory_descriptor)
        except OSError:
            pass
    finally:
        if descriptor >= 0:
            os.close(descriptor)
        temporary.unlink(missing_ok=True)


def _validate_ci_gate_evidence(value: dict) -> None:
    expected = {
        "schemaVersion", "repositoryId", "headCommit", "ciProfile", "gateSetHash",
        "platform", "generatedAt", "hookVersion", "status", "results",
    }
    _require_exact_keys(value, expected, "CI gate evidence")
    if value["schemaVersion"] != "1.0" or value["status"] != "Passed":
        raise ContractError("only schema 1.0 Passed evidence may be published")
    validate_ci_input_component(value["repositoryId"])
    if not re.fullmatch(r"[0-9a-f]{40}|[0-9a-f]{64}", str(value["headCommit"])):
        raise ContractError("evidence HEAD is invalid")
    if not re.fullmatch(r"[0-9a-f]{64}", str(value["gateSetHash"])):
        raise ContractError("evidence gate-set hash is invalid")
    if value["platform"] not in {"macos", "linux", "windows"}:
        raise ContractError("evidence platform is invalid")
    if not re.fullmatch(r"\d+\.\d+\.\d+", str(value["hookVersion"])):
        raise ContractError("evidence hookVersion is invalid")
    results = value["results"]
    if not isinstance(results, list) or not results:
        raise ContractError("evidence results must be non-empty")
    for expected_order, result in enumerate(results, start=1):
        _require_exact_keys(
            result,
            {"order", "gateId", "commandDigest", "status", "exitCode", "durationMs"},
            f"CI gate evidence result {expected_order}",
        )
        if (
            result["order"] != expected_order
            or result["status"] != "Passed"
            or result["exitCode"] != 0
            or not re.fullmatch(r"[0-9a-f]{64}", str(result["commandDigest"]))
        ):
            raise ContractError("CI gate result is incomplete or unordered")


def _print_ci_gate_status(
    profile_name: str,
    decision: str,
    status: str,
    blocker: str,
    next_action: str,
    gate_hash: str,
    gates: list[dict],
    evidence_path: pathlib.Path,
) -> None:
    print(f"Profil / Profile: {profile_name}")
    print(f"Entscheidung / Decision: {decision}")
    print(f"Status: {status}")
    print(f"Blocker: {blocker}")
    print(f"Naechste Aktion / Next action: {next_action}")
    print(f"Gate-Set-Hash: {gate_hash}")
    for gate in gates:
        print(f"Gate {gate['order']}: {gate['gateId']}")
    print(f"Evidence-Ziel / Evidence target: {evidence_path}")


def execute_ci_gate(args: argparse.Namespace) -> int:
    try:
        repository = args.repository_root.resolve()
        contracts = load_ci_budget_contracts(args.profiles, args.path_contracts, args.workflow_template)
        repository_id = _ci_repository_id(repository, args.repository_id)
        assignment = next(
            (item for item in contracts["profiles"]["assignments"] if item["repositoryId"] == repository_id),
            None,
        )
        if assignment is None:
            raise CIGateBlocked(f"repository has no explicit profile assignment: {repository_id}")
        profile = next(
            item for item in contracts["profiles"]["profiles"]
            if item["profileId"] == assignment["profileId"]
        )
        changed_paths = list(args.changed_path)
        if not changed_paths:
            changed = run_git(repository, "diff", "--name-only", "HEAD", check=False)
            if changed.returncode == 0:
                changed_paths = [line for line in changed.stdout.splitlines() if line]
        gates, matched_contracts = _ci_selected_gates(contracts, profile, changed_paths)
        gate_set = next(
            item for item in contracts["profiles"]["gateSets"]
            if item["gateSetId"] == profile["gateSetId"]
        )
        gate_hash = _ci_gate_set_hash(
            gate_set["gateSetId"], gate_set["version"], gates, matched_contracts
        )
        head_before = _ci_head(repository, args.fixture_head)
        evidence_root = args.evidence_root or pathlib.Path(
            os.environ.get("HB_CI_EVIDENCE_ROOT", "~/.home-baseline/evidence/ci-gates")
        )
        evidence_path = _ci_evidence_path(
            evidence_root, repository_id, head_before, create_parent=not args.dry_run
        )
        if args.dry_run:
            _print_ci_gate_status(
                profile["displayName"], "Preview", "Passed", "None",
                "Run without preview only after review.", gate_hash, gates, evidence_path,
            )
            return 0
        evidence_path.unlink(missing_ok=True)
        results = []
        for gate in gates:
            working = repository if gate["workingDirectory"] == "." else resolve_ci_contained_path(
                repository, pathlib.PurePosixPath(gate["workingDirectory"])
            )
            executable = gate["executable"]
            if "/" in executable:
                executable_path = resolve_ci_contained_path(repository, pathlib.PurePosixPath(executable))
                if not executable_path.is_file():
                    raise ContractError(f"gate executable is missing: {executable}")
                command = [str(executable_path), *gate["arguments"]]
            else:
                if shutil.which(executable) is None:
                    raise ContractError(f"gate executable is unavailable: {executable}")
                command = [executable, *gate["arguments"]]
            started = time.monotonic()
            try:
                completed = subprocess.run(
                    command,
                    cwd=working,
                    text=True,
                    capture_output=True,
                    shell=False,
                    check=False,
                    timeout=gate["timeoutSeconds"],
                )
            except subprocess.TimeoutExpired as exc:
                raise CIGateBlocked(f"gate timed out: {gate['gateId']}") from exc
            if completed.returncode != 0:
                diagnostic = (completed.stderr or completed.stdout).strip().splitlines()
                safe_detail = diagnostic[-1][:240] if diagnostic else "no diagnostic"
                safe_detail = safe_detail.replace(str(pathlib.Path.home()), "<home>")
                raise CIGateBlocked(
                    f"gate failed: {gate['gateId']} ({completed.returncode}; {safe_detail})"
                )
            results.append(
                {
                    # A path-dependent subset retains gate precedence but its
                    # evidence rows form their own contiguous executed sequence.
                    "order": len(results) + 1,
                    "gateId": gate["gateId"],
                    "commandDigest": canonical_json_hash(command),
                    "status": "Passed",
                    "exitCode": 0,
                    "durationMs": int((time.monotonic() - started) * 1000),
                }
            )
        head_after = _ci_head(repository, args.fixture_head)
        reloaded = load_ci_budget_contracts(args.profiles, args.path_contracts, args.workflow_template)
        gates_after, matched_after = _ci_selected_gates(reloaded, profile, changed_paths)
        gate_set_after = next(
            item for item in reloaded["profiles"]["gateSets"]
            if item["gateSetId"] == profile["gateSetId"]
        )
        hash_after = _ci_gate_set_hash(
            gate_set_after["gateSetId"], gate_set_after["version"], gates_after, matched_after
        )
        if head_before != head_after or gate_hash != hash_after:
            raise CIGateBlocked("HEAD or gate-set hash changed during execution")
        evidence = {
            "schemaVersion": "1.0",
            "repositoryId": repository_id,
            "headCommit": head_before,
            "ciProfile": profile["displayName"],
            "gateSetHash": gate_hash,
            "platform": _ci_platform(),
            "generatedAt": utc_now(),
            "hookVersion": "1.0.0",
            "status": "Passed",
            "results": results,
        }
        _atomic_ci_json(evidence_path, evidence)
        _print_ci_gate_status(
            profile["displayName"], "LocalGate", "Passed", "None",
            "Review the evidence; server-side policy remains required.", gate_hash, gates, evidence_path,
        )
        return 0
    except KeyboardInterrupt:
        print("Status: Interrupted", file=sys.stderr)
        return 130
    except CIGateBlocked as exc:
        print(f"CI_GATE\tBLOCKED\t{exc}", file=sys.stderr)
        return 1
    except (ContractError, OSError, RuntimeError, StopIteration) as exc:
        print(f"CI_GATE\tFAILED\t{exc}", file=sys.stderr)
        return 2


def simulate_private_governance_policy(
    workflow_template: pathlib.Path,
    ruleset_template: pathlib.Path,
) -> dict:
    """Validate the deliberately small Stage-A workflow/ruleset subset."""
    try:
        workflow_text = workflow_template.read_text(encoding="utf-8")
        ruleset = json.loads(ruleset_template.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise ContractError(f"workflow/ruleset template cannot be read: {exc}") from exc

    def metadata(name: str) -> str:
        match = re.search(
            rf"^# home-baseline-{re.escape(name)}:\s*([^\n]+)\s*$",
            workflow_text,
            re.MULTILINE,
        )
        if match is None:
            raise ContractError(f"workflow metadata is missing: {name}")
        return match.group(1).strip().strip('"\'')

    on_block = re.search(r"^on:\s*$\n(?P<body>(?:^\s+.*$\n?)+)", workflow_text, re.MULTILINE)
    triggers = re.findall(
        r"^  ([a-z_]+):\s*$", on_block.group("body"), re.MULTILINE
    ) if on_block else []
    gate_ids = re.findall(
        r"^# home-baseline-gate-id:\s*([a-z0-9-]+)\s*$",
        workflow_text,
        re.MULTILINE,
    )
    product_jobs = re.findall(
        r"^# home-baseline-product-job:\s*([a-z0-9][a-z0-9-]*)\s*$",
        workflow_text,
        re.MULTILINE,
    )
    jobs_body = workflow_text.split("\njobs:\n", 1)[-1]
    job_ids = re.findall(r"^  ([a-z0-9][a-z0-9-]*):\s*$", jobs_body, re.MULTILINE)
    required_status = "home-baseline/ci-minimal-gate"
    valid_workflow = (
        metadata("schema-version") == "1.0"
        and metadata("workflow-id") == "private-governance-minimal-gate"
        and triggers == ["pull_request"]
        and metadata("full-build") == "false"
        and metadata("path-dependent") == "true"
        and job_ids == ["ci-minimal-gate"]
        and re.search(r"^name:\s*home-baseline/ci-minimal-gate\s*$", workflow_text, re.MULTILINE) is not None
        and re.search(r"^    name:\s*home-baseline/ci-minimal-gate\s*$", workflow_text, re.MULTILINE) is not None
        and re.search(r"^    runs-on:\s*ubuntu-latest\s*$", workflow_text, re.MULTILINE) is not None
        and re.search(r"^    timeout-minutes:\s*2\s*$", workflow_text, re.MULTILINE) is not None
        and re.search(r"^permissions:\s*$\n  contents:\s*read\s*$", workflow_text, re.MULTILINE) is not None
        and "uses:" not in workflow_text
        and "set -euo pipefail" in workflow_text
        and ".github/workflows/home-baseline-ci-minimal-gate.yml" in workflow_text
        and len(gate_ids) == len(set(gate_ids))
        and bool(gate_ids)
        and sorted(product_jobs) == [
            "casetracker-csharp", "casetracker-go", "casetracker-java",
            "casetracker-python", "casetracker-rust", "casetracker-swift",
        ]
    )
    expected_ruleset_keys = {
        "schemaVersion", "active", "applied", "target", "rulesetName", "enforcement",
        "pullRequestRequired", "requiredApprovingReviews", "requiredStatusChecks",
        "requireStatusChecksToPass", "strictStatusChecks", "bypassActors",
        "blockedWritePaths", "adminBypassNormalPath", "remoteConverged",
    }
    valid_ruleset = (
        set(ruleset) == expected_ruleset_keys
        and ruleset.get("schemaVersion") == "1.0"
        and ruleset.get("active") is False
        and ruleset.get("applied") is False
        and ruleset.get("target") == "default_branch"
        and ruleset.get("rulesetName") == "home-baseline/private-governance-default"
        and ruleset.get("enforcement") == "active"
        and ruleset.get("pullRequestRequired") is True
        and ruleset.get("requiredApprovingReviews") == 1
        and ruleset.get("requiredStatusChecks") == ["home-baseline/ci-minimal-gate"]
        and ruleset.get("requireStatusChecksToPass") is True
        and ruleset.get("strictStatusChecks") is True
        and ruleset.get("bypassActors") == [{
            "actor_id": 5,
            "actor_type": "RepositoryRole",
            "bypass_mode": "pull_request",
        }]
        and sorted(ruleset.get("blockedWritePaths", [])) == ["api", "direct", "web"]
        and ruleset.get("adminBypassNormalPath") is False
        and ruleset.get("remoteConverged") is False
    )
    if not valid_workflow or not valid_ruleset:
        raise CIGateBlocked("private governance workflow/ruleset contract is unsafe or too broad")
    return {
        "pullRequestRequired": True,
        "requiredStatusChecks": [required_status],
        "blockedWritePaths": ["api", "direct", "web"],
        "adminBypassNormalPath": False,
        "remoteConverged": False,
        "hookRequiredForServerEnforcement": False,
    }


def authoritative_ci_repositories(
    repository_root: pathlib.Path,
    manifest: dict,
    profile_registry: dict,
) -> list[dict]:
    """Return Level-0 self plus every active Git target, never collections."""
    targets = manifest.get("targets")
    if not isinstance(targets, list):
        raise ContractError("fleet manifest targets must be an array")
    active_git: list[dict] = []
    seen: set[str] = set()
    excluded_collections: list[str] = []
    for target in targets:
        if not isinstance(target, dict) or not target.get("active"):
            continue
        repository_id = validate_ci_input_component(target.get("id"))
        if target.get("kind") == "collection":
            excluded_collections.append(repository_id)
            continue
        if target.get("kind") != "git-repository":
            continue
        if repository_id == "home-baseline":
            raise CIGateBlocked("fleet manifest must not duplicate the Level-0 self record")
        if repository_id in seen:
            raise CIGateBlocked(f"duplicate active repository ID: {repository_id}")
        seen.add(repository_id)
        active_git.append({
            "repositoryId": repository_id,
            "remoteIdentity": normalize_remote(str(target.get("remote", ""))),
            "defaultBranch": str(target.get("defaultBranch", "")),
            "origin": "fleet-manifest",
        })
    origin = run_git(repository_root, "remote", "get-url", "origin", check=False)
    if origin.returncode != 0 or not origin.stdout.strip():
        raise ContractError("Level-0 configured origin is required")
    records = [{
        "repositoryId": "home-baseline",
        "remoteIdentity": normalize_remote(origin.stdout.strip()),
        "defaultBranch": "main",
        "origin": "executing-level0-configured-origin",
    }, *active_git]
    records.sort(key=lambda item: item["repositoryId"])
    assignment_rows = profile_registry.get("assignments", [])
    assignment_ids = [item.get("repositoryId") for item in assignment_rows if isinstance(item, dict)]
    expected_ids = [item["repositoryId"] for item in records]
    if len(assignment_ids) != len(set(assignment_ids)) or set(assignment_ids) != set(expected_ids):
        missing = sorted(set(expected_ids) - set(assignment_ids))
        unknown = sorted(set(assignment_ids) - set(expected_ids))
        raise CIGateBlocked(f"profile assignments differ from authoritative set; missing={missing}, unknown={unknown}")
    assignments = {item["repositoryId"]: item["profileId"] for item in assignment_rows}
    for canary in ("home-baseline", "agent-operations-cockpit", "tui-vision"):
        if assignments.get(canary) != "public-canary":
            raise CIGateBlocked(f"required public canary assignment is missing: {canary}")
    for record in records:
        record["assignmentProfileId"] = assignments[record["repositoryId"]]
    if "spec-kit-preset-projects" not in excluded_collections:
        raise CIGateBlocked("the authoritative collection exclusion is missing")
    return records


def _normalize_ci_workflows(raw: object, label: str) -> list[dict]:
    if not isinstance(raw, list):
        raise ContractError(f"{label}.workflows must be an array")
    normalized: list[dict] = []
    allowed_triggers = {"pull_request", "push", "schedule", "workflow_dispatch"}
    for index, item in enumerate(raw):
        if not isinstance(item, dict):
            raise ContractError(f"{label}.workflows[{index}] must be an object")
        _require_exact_keys(
            item,
            {"workflowId", "jobId", "triggers", "runners", "averageDurationSeconds", "plannedRuns"},
            f"{label}.workflows[{index}]",
        )
        workflow_id = validate_ci_input_component(item["workflowId"])
        job_id = validate_ci_input_component(item["jobId"])
        triggers = sorted(set(item["triggers"])) if isinstance(item["triggers"], list) else []
        runners = sorted(set(item["runners"])) if isinstance(item["runners"], list) else []
        if not triggers or set(triggers) - allowed_triggers or not runners:
            raise ContractError(f"{label}.workflows[{index}] has invalid triggers or runners")
        decimals: dict[str, str | None] = {}
        for field in ("averageDurationSeconds", "plannedRuns"):
            value = item[field]
            if value is None:
                decimals[field] = None
                continue
            try:
                parsed = Decimal(str(value))
            except InvalidOperation as exc:
                raise ContractError(f"{label}.workflows[{index}].{field} is invalid") from exc
            if parsed < 0:
                raise ContractError(f"{label}.workflows[{index}].{field} is negative")
            decimals[field] = format(parsed, "f")
        normalized.append({
            "workflowId": workflow_id,
            "jobId": job_id,
            "triggers": triggers,
            "runners": runners,
            **decimals,
        })
    normalized.sort(key=lambda item: (item["workflowId"], item["jobId"]))
    keys = [(item["workflowId"], item["jobId"]) for item in normalized]
    if len(keys) != len(set(keys)):
        raise ContractError(f"{label}.workflows contains duplicate jobs")
    return normalized


def validate_ci_inventory(
    raw_rows: object,
    authoritative: list[dict],
    profile_registry: dict,
    *,
    source_revision: str,
    observed_at: str,
) -> list[dict]:
    """Validate one complete atomic snapshot and copy only accepted assignments."""
    if not source_revision or any(character in source_revision for character in ("\r", "\n", "\0")):
        raise ContractError("one non-empty atomic sourceRevision is required")
    if not re.fullmatch(r"\d{4}-\d\d-\d\dT\d\d:\d\d:\d\dZ", observed_at):
        raise ContractError("inventory observedAt must be RFC 3339 UTC without fractions")
    if not isinstance(raw_rows, list):
        raise ContractError("inventory repositories must be an array")
    expected_ids = {item["repositoryId"] for item in authoritative}
    raw_ids = [item.get("repositoryId") for item in raw_rows if isinstance(item, dict)]
    if len(raw_ids) != len(set(raw_ids)) or set(raw_ids) != expected_ids:
        raise CIGateBlocked("inventory IDs do not exactly match the authoritative repository set")
    profiles = {item["profileId"]: item for item in profile_registry["profiles"]}
    assignments = {item["repositoryId"]: item["profileId"] for item in profile_registry["assignments"]}
    authoritative_by_id = {item["repositoryId"]: item for item in authoritative}
    normalized = []
    for raw in raw_rows:
        if not isinstance(raw, dict):
            raise ContractError("inventory row must be an object")
        allowed = {"repositoryId", "remoteIdentity", "visibility", "defaultBranch", "workflows", "profileId"}
        if set(raw) - allowed or not {"repositoryId", "remoteIdentity", "visibility", "defaultBranch", "workflows"} <= set(raw):
            raise ContractError(f"inventory row fields are invalid: {raw.get('repositoryId', '<unknown>')}")
        repository_id = validate_ci_input_component(raw["repositoryId"])
        profile_id = assignments[repository_id]
        if raw.get("profileId", profile_id) != profile_id:
            raise CIGateBlocked(f"inventory profileId drift: {repository_id}")
        if raw["visibility"] != profiles[profile_id]["requiredVisibility"]:
            raise CIGateBlocked(f"inventory visibility conflicts with profile: {repository_id}")
        remote_identity = normalize_remote(str(raw["remoteIdentity"]))
        if remote_identity != authoritative_by_id[repository_id]["remoteIdentity"]:
            raise CIGateBlocked(f"inventory remote identity drift: {repository_id}")
        default_branch = str(raw["defaultBranch"])
        if not default_branch or any(character in default_branch for character in ("\r", "\n", "\0")):
            raise ContractError(f"inventory default branch is invalid: {repository_id}")
        normalized.append({
            "repositoryId": repository_id,
            "remoteIdentity": remote_identity,
            "visibility": raw["visibility"],
            "defaultBranch": default_branch,
            "profileId": profile_id,
            "observedAt": observed_at,
            "workflows": _normalize_ci_workflows(raw["workflows"], repository_id),
        })
    return sorted(normalized, key=lambda item: item["repositoryId"])


def _github_repository_slug(remote: str) -> str:
    parsed = urlsplit(remote)
    path = parsed.path.strip("/") if parsed.scheme else remote.split(":", 1)[-1].strip("/")
    path = path.removesuffix(".git")
    if not re.fullmatch(r"[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+", path):
        raise ContractError(f"GitHub remote cannot be minimized safely: {remote}")
    return path


def _github_get_json(endpoint: str) -> dict:
    assert_stage_a_operation("GET", endpoint)
    command = ["gh", "api", "--method", "GET", endpoint]
    for attempt in range(1, 4):
        try:
            completed = subprocess.run(
                command, text=True, capture_output=True, check=False, timeout=30
            )
        except subprocess.TimeoutExpired:
            completed = subprocess.CompletedProcess(command, 124, "", "timeout")
        if completed.returncode == 0:
            try:
                value = json.loads(completed.stdout)
            except json.JSONDecodeError as exc:
                raise ContractError(f"GitHub GET returned invalid JSON for {endpoint}") from exc
            if not isinstance(value, dict):
                raise ContractError(f"GitHub GET returned an invalid object for {endpoint}")
            return value
        detail = (completed.stderr or completed.stdout).strip()
        transient = bool(re.search(
            r"timeout|reset|resolve|rate limit|HTTP 50[234]|error connecting|internet connection",
            detail,
            re.IGNORECASE,
        ))
        if not transient or attempt == 3:
            raise ContractError(f"GitHub read-only inventory failed for {endpoint}: {detail[-200:]}")
        time.sleep(0.25 * (2 ** (attempt - 1)))
    raise AssertionError("bounded GitHub retry loop exhausted")


def github_read_only_inventory(
    authoritative: list[dict],
    *,
    transport=None,
    observed_at: str | None = None,
) -> tuple[str, str, list[dict]]:
    """Minimize repository metadata from GET-only GitHub observations."""
    getter = transport or _github_get_json
    observed = observed_at or utc_now()
    rows = []
    for expected in authoritative:
        slug = _github_repository_slug(expected["remoteIdentity"])
        value = getter(f"repos/{slug}")
        rows.append({
            "repositoryId": expected["repositoryId"],
            "remoteIdentity": normalize_remote(str(value.get("html_url", expected["remoteIdentity"]))),
            "visibility": "private" if value.get("private") is True else "public",
            "defaultBranch": str(value.get("default_branch", "")),
            "workflows": [],
        })
    revision = canonical_json_hash({"observedAt": observed, "repositories": rows})
    return revision, observed, rows


def evaluate_ci_paths(contracts: dict, changed_paths: list[str]) -> dict:
    normalized_paths = sorted({_normalize_changed_path(path) for path in changed_paths})
    matches: list[dict] = []
    for contract in contracts["paths"]["pathContracts"]:
        if any(
            any(fnmatch.fnmatch(path, pattern) for pattern in contract["includePatterns"])
            and not any(fnmatch.fnmatch(path, pattern) for pattern in contract["excludePatterns"])
            for path in normalized_paths
        ):
            matches.append(contract)
    gate_order = {
        gate["gateId"]: gate["order"]
        for gate_set in contracts["profiles"]["gateSets"]
        for gate in gate_set["gates"]
    }
    gate_ids = sorted(
        {gate for contract in matches for gate in contract["gateIds"]},
        key=lambda gate: (gate_order[gate], gate),
    )
    return {
        "matchedPathContractIds": sorted(
            (contract["pathContractId"] for contract in matches),
            key=lambda contract_id: next(
                (item["category"], item["pathContractId"])
                for item in matches if item["pathContractId"] == contract_id
            ),
        ),
        "gateIds": gate_ids,
        "productJobIds": sorted({
            contract["productJobId"] for contract in matches if "productJobId" in contract
        }),
    }


def assert_stage_a_operation(action: str, target: str) -> None:
    """Fail closed before any Stage-A remote, delivery, or target write."""
    normalized_action = action.strip().upper()
    normalized_target = target.strip().lower()
    if normalized_action in {"POST", "PUT", "PATCH", "DELETE", "COMMIT", "PUSH", "MERGE", "HOME-SYNC", "G4"}:
        raise ContractError(f"Stage A forbids operation: {normalized_action}")
    if normalized_action != "GET":
        raise ContractError(f"Stage A permits only GET for remote transport: {normalized_action}")
    forbidden_fragments = ("/rulesets", "/collaborators", "/reviewers", "/copilot", "/actions/permissions")
    if any(fragment in normalized_target for fragment in forbidden_fragments):
        raise ContractError(f"Stage A forbids mutable administration surface: {target}")


def simulate_ci_workflow_policy(
    profile_id: str,
    repository_id: str,
    event: str,
    path_decision: dict,
) -> dict:
    """Apply the closed Stage-A trigger subset for all five profiles."""
    if event not in {"pull_request", "push", "schedule", "workflow_dispatch"}:
        raise ContractError(f"unsupported workflow event: {event}")
    jobs: list[dict] = []
    decision = "Blocked"
    if repository_id == "private-release-please":
        if event in {"schedule", "workflow_dispatch"}:
            decision = "LocalGate"
            jobs = [{"jobId": "release-please", "runner": "ubuntu-latest"}]
    elif profile_id == "private-governance-scaffold":
        if event == "pull_request":
            decision = "LocalGate"
            jobs = [{"jobId": "ci-minimal-gate", "runner": "ubuntu-latest"}]
    elif profile_id == "private-product":
        product_jobs = list(path_decision.get("productJobIds", []))
        if event == "pull_request":
            decision = "ProductPRGate" if product_jobs else "LocalGate"
            jobs = [
                {"jobId": job_id, "runner": "ubuntu-latest"}
                for job_id in sorted(set(product_jobs))
            ]
        elif repository_id == "secure-casetracker-swift" and event in {"schedule", "workflow_dispatch"}:
            decision = "ProductPRGate"
            jobs = [{"jobId": "casetracker-swift-platform", "runner": "macos-latest"}]
    elif profile_id == "public-preset":
        decision = "FleetPipeline"
    elif profile_id in {"public-canary", "public-product"}:
        decision = "PublicCI"
    else:
        raise ContractError(f"unknown profile policy: {profile_id}")
    return {
        "profileId": profile_id,
        "repositoryId": repository_id,
        "event": event,
        "gateDecision": decision,
        "plannedJobs": jobs,
        "fullBuildCount": 0 if profile_id.startswith("private-") else None,
        "remoteConverged": False,
    }


def project_ci_costs(
    assumptions: dict,
    *,
    recurring_jobs: object = 22,
    recurring_duration_seconds: object = 120,
    demand_runs: object = 20,
    demand_duration_seconds: object = 60,
    copilot_minutes: object = 15,
) -> dict:
    """Keep Actions and Copilot minutes separate and avoid early rounding."""
    values = [recurring_jobs, recurring_duration_seconds, demand_runs, demand_duration_seconds, copilot_minutes]
    if any(value is None for value in values):
        raise CIGateBlocked("budget inputs are incomplete; missing values are not treated as zero")
    try:
        recurring, recurring_duration, demand, demand_duration, copilot = [Decimal(str(value)) for value in values]
    except InvalidOperation as exc:
        raise ContractError("budget inputs must be decimal values") from exc
    if any(value < 0 for value in (recurring, recurring_duration, demand, demand_duration, copilot)):
        raise ContractError("budget inputs must not be negative")
    weeks = Decimal(assumptions["weeksPerMonthNumerator"]) / Decimal(assumptions["weeksPerMonthDenominator"])
    actions_minutes = (recurring * weeks * recurring_duration / Decimal(60)) + (demand * demand_duration / Decimal(60))
    target = Decimal(assumptions["privateMonthlyTargetExclusiveMinutes"])
    if actions_minutes >= target:
        raise CIGateBlocked("private Actions projection does not satisfy the exclusive target")
    return {
        "weeksPerMonthNumerator": assumptions["weeksPerMonthNumerator"],
        "weeksPerMonthDenominator": assumptions["weeksPerMonthDenominator"],
        "recurringPrivateJobsPerWeek": format(recurring, "f"),
        "demandMinimalGateRuns": format(demand, "f"),
        "privateActionsMinutesPerMonth": format(actions_minutes.quantize(Decimal("0.01")), "f"),
        "privateMonthlyBudgetMinutes": assumptions["privateMonthlyBudgetMinutes"],
        "copilotReviewRunnerMinutes": format(copilot, "f"),
        "assumptions": [
            "52/12 weeks per month; recurring and demand Actions minutes are calculated separately.",
            "Copilot review runner minutes remain a separate non-Actions category.",
            "The recurring reference is approximately 22 private jobs per week.",
        ],
    }


def _ci_rollout_decision(profile_id: str) -> tuple[str, list[dict], str]:
    if profile_id == "private-governance-scaffold":
        return "LocalGate", [{"artifactId": "minimal-gate", "action": "Preserve", "stage": "A-Level0Template"}], "Lokale Evidence prüfen; Serverkonvergenz benötigt neue Autorisierung. / Review local evidence; server convergence needs new authorization."
    if profile_id == "private-product":
        return "ProductPRGate", [{"artifactId": "product-pr-gate", "action": "NoChange", "stage": "B-TargetRepository"}], "Pfadabhängigen PR-Gate später autorisieren. / Authorize the path-dependent PR gate later."
    if profile_id == "public-preset":
        return "FleetPipeline", [{"artifactId": "fleet-pipeline", "action": "Preserve", "stage": "A-Level0Template"}], "Flottenpipeline oder lokale Evidence beibehalten. / Keep the fleet pipeline or local evidence."
    return "PublicCI", [{"artifactId": "public-ci", "action": "Preserve", "stage": "B-TargetRepository"}], "Erforderliche öffentliche CI beibehalten. / Preserve required public CI."


def build_ci_inventory_rollout_plan(
    contracts: dict,
    authoritative: list[dict],
    repositories: list[dict],
    *,
    source: str,
    source_revision: str,
    generated_at: str,
) -> dict:
    if source not in {"Fixture", "GitHubReadOnly"}:
        raise ContractError("inventory source is invalid")
    if not re.fullmatch(r"\d{4}-\d\d-\d\dT\d\d:\d\d:\d\dZ", generated_at):
        raise ContractError("generatedAt must be RFC 3339 UTC without fractions")
    expected_ids = [item["repositoryId"] for item in authoritative]
    repository_ids = [item["repositoryId"] for item in repositories]
    if repository_ids != sorted(expected_ids):
        raise CIGateBlocked("validated inventory order or ID set drifted before rollout")
    profiles = {item["profileId"]: item for item in contracts["profiles"]["profiles"]}
    rollout = []
    cardinalities = {item["displayName"]: 0 for item in contracts["profiles"]["profiles"]}
    for repository in repositories:
        profile_id = repository["profileId"]
        decision, planned_diff, next_action = _ci_rollout_decision(profile_id)
        cardinalities[profiles[profile_id]["displayName"]] += 1
        rollout.append({
            "repositoryId": repository["repositoryId"],
            "profileId": profile_id,
            "plannedDiff": sorted(planned_diff, key=lambda item: item["artifactId"]),
            "gateDecision": decision,
            "blockers": [],
            "nextAction": next_action,
            "remoteConverged": False,
        })
    inventory_hash = canonical_json_hash([
        {key: value for key, value in repository.items() if key != "observedAt"}
        for repository in repositories
    ])
    return {
        "schemaVersion": "1.0",
        "stage": "A",
        "deliveryMode": "LocalImplementation",
        "generatedAt": generated_at,
        "source": source,
        "sourceRevision": source_revision,
        "authoritativeRepositorySet": {
            "level0SelfRepositoryId": "home-baseline",
            "level0RemoteResolution": "executing-level0-configured-origin",
            "fleetManifestPath": "scripts/config/agentic-workspace-fleet.json",
            "includedRepositoryCount": len(repositories),
            "excludedCollectionIds": ["spec-kit-preset-projects"],
        },
        "inventorySnapshotHash": inventory_hash,
        "profileRegistryHash": contracts["profileRegistryHash"],
        "pathContractHash": contracts["pathContractHash"],
        "mutationsPerformed": False,
        "profileCardinalities": cardinalities,
        "repositories": repositories,
        "rollout": rollout,
        "costProjection": project_ci_costs(contracts["profiles"]["budgetAssumptions"]),
    }


def execute_ci_budget_plan(args: argparse.Namespace) -> int:
    """Build one deterministic, read-only Stage-A inventory and rollout plan."""
    try:
        repository = args.repository_root.resolve()
        contracts = load_ci_budget_contracts(args.profiles, args.path_contracts, args.workflow_template)
        manifest = _read_ci_json(args.manifest, "fleet manifest")
        authoritative = authoritative_ci_repositories(repository, manifest, contracts["profiles"])
        if not args.check_only:
            raise ContractError("Stage A CI budget planning requires --check-only")
        if args.output != "-":
            raise ContractError("Stage A permits only --output -")
        if args.adapter == "fixture":
            if args.inventory is None:
                raise ContractError("fixture adapter requires --inventory")
            raw_inventory = _read_ci_json(args.inventory, "fixture inventory")
            source = "Fixture"
            source_revision = str(raw_inventory.get("sourceRevision", ""))
            observed_at = str(raw_inventory.get("observedAt", ""))
            raw_rows = raw_inventory.get("repositories")
        else:
            source = "GitHubReadOnly"
            source_revision, observed_at, raw_rows = github_read_only_inventory(authoritative)
        repositories = validate_ci_inventory(
            raw_rows,
            authoritative,
            contracts["profiles"],
            source_revision=source_revision,
            observed_at=observed_at,
        )
        plan = build_ci_inventory_rollout_plan(
            contracts,
            authoritative,
            repositories,
            source=source,
            source_revision=source_revision,
            generated_at=os.environ.get("HB_CI_FIXTURE_CLOCK", observed_at or utc_now()),
        )
        print(json.dumps(plan, ensure_ascii=False, separators=(",", ":")))
        return 0
    except CIGateBlocked as exc:
        print(f"CI_BUDGET_PLAN\tBLOCKED\t{exc}", file=sys.stderr)
        return 1
    except (ContractError, OSError, RuntimeError) as exc:
        print(f"CI_BUDGET_PLAN\tFAILED\t{exc}", file=sys.stderr)
        return 2


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser()
    subparsers = parser.add_subparsers(dest="command", required=True)
    stage_b = subparsers.add_parser("stage-b")
    stage_b.add_argument(
        "--action", choices=("preflight", "validate", "deliver", "resume", "verify"), required=True
    )
    stage_b.add_argument("--repository-root", type=pathlib.Path, required=True)
    stage_b.add_argument("--run-id", default=STAGE_B_RUN_ID)
    stage_b.add_argument("--delivery-mode", choices=("MergeAndSync",), default="MergeAndSync")
    stage_b.add_argument("--wave-id", default="N/A")
    stage_b.add_argument("--repository-id", default="N/A")
    stage_b.add_argument("--profile-id", default="N/A")
    stage_b.add_argument("--dry-run", action="store_true")
    stage_b.set_defaults(handler=execute_stage_b_action)
    ci_gate = subparsers.add_parser("ci-gate")
    ci_gate.add_argument("--repository-root", type=pathlib.Path, required=True)
    ci_gate.add_argument("--profiles", type=pathlib.Path, default=pathlib.Path("scripts/config/ci-budget-profiles.json"))
    ci_gate.add_argument("--path-contracts", type=pathlib.Path, default=pathlib.Path("scripts/config/ci-budget-path-contracts.json"))
    ci_gate.add_argument("--workflow-template", type=pathlib.Path, default=pathlib.Path("scripts/templates/ci-budget-governance/private-governance-minimal-gate.yml"))
    ci_gate.add_argument("--repository-id")
    ci_gate.add_argument("--evidence-root", type=pathlib.Path)
    ci_gate.add_argument("--changed-path", action="append", default=[])
    ci_gate.add_argument("--fixture-head")
    ci_gate.add_argument("--dry-run", action="store_true")
    ci_gate.set_defaults(handler=execute_ci_gate)
    ci_plan = subparsers.add_parser("ci-budget-plan")
    ci_plan.add_argument("--repository-root", type=pathlib.Path, required=True)
    ci_plan.add_argument("--manifest", type=pathlib.Path, required=True)
    ci_plan.add_argument("--profiles", type=pathlib.Path, required=True)
    ci_plan.add_argument("--path-contracts", type=pathlib.Path, required=True)
    ci_plan.add_argument("--workflow-template", type=pathlib.Path, default=pathlib.Path("scripts/templates/ci-budget-governance/private-governance-minimal-gate.yml"))
    ci_plan.add_argument("--adapter", choices=("fixture", "github-read-only"), required=True)
    ci_plan.add_argument("--inventory", type=pathlib.Path)
    ci_plan.add_argument("--check-only", action="store_true")
    ci_plan.add_argument("--output", required=True)
    ci_plan.set_defaults(handler=execute_ci_budget_plan)
    fleet = subparsers.add_parser("fleet")
    fleet.add_argument("--manifest", type=pathlib.Path, required=True)
    fleet.add_argument("--home-dir", type=pathlib.Path, required=True)
    fleet.add_argument("--mode", choices=("check-only", "dry-run", "update"), required=True)
    fleet.add_argument("--report", type=pathlib.Path, required=True)
    fleet.add_argument("--log", type=pathlib.Path, required=True)
    fleet.add_argument("--run-id")
    fleet.add_argument("--allowed-dirty-path", action="append", default=[])
    fleet.add_argument("--level0-dir", type=pathlib.Path)
    fleet.set_defaults(handler=execute_fleet)
    stage = subparsers.add_parser("stage")
    stage.add_argument("--report", type=pathlib.Path, required=True)
    stage.add_argument("--stage-id", required=True)
    stage.add_argument(
        "--status",
        choices=(
            "Passed",
            "Warning",
            "Blocked",
            "Failed",
            "Skipped",
            "DeferredAdminRequired",
            "Interrupted",
        ),
        required=True,
    )
    stage.add_argument("--exit-code", type=int, required=True)
    stage.add_argument("--duration-ms", type=int, default=0)
    stage.add_argument("--summary", required=True)
    stage.add_argument("--next-action", default="N/A")
    stage.add_argument("--toolchain-results", type=pathlib.Path)
    stage.add_argument("--storage-results", type=pathlib.Path)
    stage.add_argument("--evidence-path", type=pathlib.Path)
    stage.set_defaults(handler=record_stage)
    finalize = subparsers.add_parser("finalize")
    finalize.add_argument("--report", type=pathlib.Path, required=True)
    finalize.add_argument("--stage-id", required=True)
    finalize.add_argument(
        "--status",
        choices=(
            "Passed",
            "Warning",
            "Blocked",
            "Failed",
            "DeferredAdminRequired",
            "Interrupted",
        ),
        required=True,
    )
    finalize.add_argument("--exit-code", type=int, required=True)
    finalize.add_argument("--duration-ms", type=int, default=0)
    finalize.add_argument("--signal", choices=("N/A", "INT", "TERM"), default="N/A")
    finalize.add_argument("--summary", required=True)
    finalize.add_argument("--next-action", default="N/A")
    finalize.set_defaults(handler=finalize_report)
    registry = subparsers.add_parser("registry")
    registry.add_argument("--manifest", type=pathlib.Path, required=True)
    registry.add_argument("--registry", type=pathlib.Path, required=True)
    registry.set_defaults(handler=validate_registry)
    profile = subparsers.add_parser("profile")
    profile.add_argument("--catalog", type=pathlib.Path, required=True)
    profile.add_argument("--source-root", type=pathlib.Path, required=True)
    profile.add_argument("--profile")
    profile.add_argument("--field", choices=("json", "path"), default="json")
    profile.set_defaults(handler=resolve_preset_profile)
    repositories = subparsers.add_parser("canonical-repositories")
    repositories.add_argument("--manifest", type=pathlib.Path, required=True)
    repositories.add_argument("--home-dir", type=pathlib.Path, required=True)
    repositories.add_argument("--existing-only", action="store_true")
    repositories.set_defaults(handler=list_canonical_repositories)
    default_ref = subparsers.add_parser("default-ref")
    default_ref.add_argument("--repository", type=pathlib.Path, required=True)
    default_ref.set_defaults(handler=print_default_remote_ref)
    event = subparsers.add_parser("event")
    event.add_argument("--event-stream", type=pathlib.Path, required=True)
    event.add_argument("--run-id", required=True)
    event.add_argument("--sequence", type=int, required=True)
    event.add_argument(
        "--event-type",
        choices=(
            "run-started",
            "phase-started",
            "phase-progress",
            "finding",
            "phase-completed",
            "run-completed",
        ),
        required=True,
    )
    event.add_argument(
        "--status",
        choices=(
            "RUNNING",
            "PASSED",
            "PARTIAL",
            "BLOCKED",
            "WARNING",
            "SKIPPED",
            "FAILED",
        ),
        required=True,
    )
    event.add_argument(
        "--phase-id",
        choices=(
            "fleet",
            "level0",
            "home-sync",
            "registry",
            "propagation",
            "preset-profiles",
            "toolchain",
            "final",
        ),
    )
    event.add_argument("--target-id")
    event.add_argument("--message-de", required=True)
    event.add_argument("--message-en", required=True)
    event.add_argument("--details-json", default="{}")
    event.set_defaults(handler=append_maintenance_event)
    lease_create = subparsers.add_parser("lease-create")
    lease_create.add_argument("--state-root", type=pathlib.Path, required=True)
    lease_create.add_argument("--lease", type=pathlib.Path, required=True)
    lease_create.add_argument("--run-id", required=True)
    lease_create.add_argument("--owner-pid", type=int, required=True)
    lease_create.add_argument("--owner-process-identity")
    lease_create.add_argument("--repository", type=pathlib.Path, required=True)
    lease_create.add_argument("--remote-ref", required=True)
    lease_create.add_argument("--commit", required=True)
    lease_create.add_argument("--worktree", type=pathlib.Path, required=True)
    lease_create.set_defaults(handler=create_worktree_lease)
    lease_release = subparsers.add_parser("lease-release")
    lease_release.add_argument("--state-root", type=pathlib.Path, required=True)
    lease_release.add_argument("--lease", type=pathlib.Path, required=True)
    lease_release.add_argument("--run-id", required=True)
    lease_release.set_defaults(handler=release_worktree_lease)
    lease_recover = subparsers.add_parser("lease-recover")
    lease_recover.add_argument("--state-root", type=pathlib.Path, required=True)
    lease_recover.add_argument("--lease-dir", type=pathlib.Path, required=True)
    lease_recover.set_defaults(handler=recover_worktree_leases)
    return parser


def main() -> int:
    counter_file = os.environ.get("HB_CI_ENGINE_COUNTER_FILE")
    if counter_file:
        counter_path = pathlib.Path(counter_file)
        counter_path.parent.mkdir(mode=0o700, parents=True, exist_ok=True)
        with counter_path.open("a", encoding="utf-8", newline="\n") as stream:
            stream.write(f"{os.getpid()}\n")
            stream.flush()
    arguments = build_parser().parse_args()
    return arguments.handler(arguments)


if __name__ == "__main__":
    raise SystemExit(main())
