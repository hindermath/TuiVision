#!/usr/bin/env python3
"""Validate and maintain the declared workspace fleet without provider writes."""

from __future__ import annotations

import argparse
import ctypes
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
            r"timed?\s*out|timeout|connection\s+(?:reset|closed|aborted)|temporary failure|"
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
            mutationAllowed=not dirty
        )
    return target_result(
        target, branch=branch_name, upstream=upstream_name,
        retryAttempts=fetch_attempts, resumeAccepted=resume_accepted,
        freshnessAttempt=freshness, defaultBranchEvidence=default_evidence,
        mutationAllowed=not dirty
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


def record_stage(args: argparse.Namespace) -> int:
    try:
        report = json.loads(args.report.read_text(encoding="utf-8"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        print(f"ERROR\treport\tFAILED\t{exc}")
        return 2
    stages = report.setdefault("stages", [])
    stages[:] = [item for item in stages if item.get("stageId") != args.stage_id]
    stages.append({
        "stageId": args.stage_id,
        "status": args.status,
        "exitCode": args.exit_code,
        "durationMs": args.duration_ms,
        "summary": args.summary,
        "nextAction": args.next_action,
    })
    if args.toolchain_results:
        try:
            toolchain_result = json.loads(
                args.toolchain_results.read_text(encoding="utf-8")
            )
        except (OSError, UnicodeError, json.JSONDecodeError) as exc:
            print(f"ERROR\ttoolchain-results\tFAILED\t{exc}")
            return 2
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


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser()
    subparsers = parser.add_subparsers(dest="command", required=True)
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
    arguments = build_parser().parse_args()
    return arguments.handler(arguments)


if __name__ == "__main__":
    raise SystemExit(main())
