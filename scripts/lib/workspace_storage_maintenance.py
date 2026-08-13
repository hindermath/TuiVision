#!/usr/bin/env python3
"""Inventory and safely reclaim generated workspace storage.

The engine is shared by the Bash and PowerShell public wrappers.  It treats
paths as untrusted input: every deletion candidate must be registry-owned,
contained by a resolved Level-2 repository, ignored by Git, and untracked.
"""

from __future__ import annotations

import argparse
import csv
import fnmatch
import json
import os
import pathlib
import re
import shlex
import shutil
import stat
import subprocess
import sys
import tempfile
import time
import uuid
from datetime import datetime, timezone
from typing import Iterable


ENGINE_SCHEMA = "1.0"
VALID_PROFILES = {"safe", "deep", "none"}
VALID_MODES = {"check-only", "dry-run", "update"}
KNOWN_MSL_TOKENS = (
    "c#", "f#", "vb.net", "rust", "swift", "java", "kotlin", "scala",
    "golang", " go ", "python", "ruby", "javascript", "typescript",
    "haskell", "ocaml", "erlang", "elixir", "ada", "spark", "dart",
)
KNOWN_NON_MSL_TOKENS = (
    "c++", "c/c89", "c89", "cc65", "6502", "assembly", "objective-c",
    " zig", " nim", " d without", " d ohne",
)
WALK_EXCLUDES = {
    ".git", ".idea", ".vscode", "node_modules", ".venv", "venv",
    ".tox", ".mypy_cache", ".ruff_cache",
}
PROCESS_INVENTORY_UNAVAILABLE = "__inventory_unavailable__"


class StorageContractError(ValueError):
    """Raised when policy, path, or evidence cannot be handled safely."""


def utc_now() -> str:
    return datetime.now(timezone.utc).replace(microsecond=0).isoformat().replace("+00:00", "Z")


def atomic_write_json(path: pathlib.Path, payload: dict) -> None:
    """Publish one private, complete report through atomic replacement."""
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_name(f".{path.name}.{uuid.uuid4().hex}.tmp")
    fd = os.open(temporary, os.O_WRONLY | os.O_CREAT | os.O_EXCL, 0o600)
    try:
        with os.fdopen(fd, "w", encoding="utf-8") as stream:
            json.dump(payload, stream, ensure_ascii=False, indent=2)
            stream.write("\n")
            stream.flush()
            os.fsync(stream.fileno())
        os.replace(temporary, path)
        try:
            os.chmod(path, 0o600)
        except OSError:
            # Windows ACLs are applied by the PowerShell wrapper.
            if os.name != "nt":
                raise
    finally:
        if temporary.exists():
            temporary.unlink()


def load_json_object(path: pathlib.Path, label: str) -> dict:
    try:
        value = json.loads(path.read_text(encoding="utf-8-sig"))
    except (OSError, UnicodeError, json.JSONDecodeError) as exc:
        raise StorageContractError(f"{label} is unreadable or invalid: {exc}") from exc
    if not isinstance(value, dict):
        raise StorageContractError(f"{label} must be a JSON object")
    return value


def validate_relative_path(raw: object, label: str) -> pathlib.PurePosixPath:
    if not isinstance(raw, str) or not raw.strip():
        raise StorageContractError(f"{label} must be a non-empty relative path")
    normalized = raw.replace("\\", "/")
    path = pathlib.PurePosixPath(normalized)
    if path.is_absolute() or any(part in {"", ".", ".."} for part in path.parts):
        raise StorageContractError(f"unsafe {label}: {raw}")
    return path


def contained_path(path: pathlib.Path, root: pathlib.Path, label: str) -> pathlib.Path:
    if path.is_symlink():
        raise StorageContractError(f"{label} must not be a symlink: {path}")
    resolved = path.resolve(strict=False)
    resolved_root = root.resolve(strict=True)
    try:
        resolved.relative_to(resolved_root)
    except ValueError as exc:
        raise StorageContractError(f"{label} escapes its repository: {path}") from exc
    if resolved == resolved_root:
        raise StorageContractError(f"{label} must not be a repository root")
    return resolved


def run_command(
    arguments: list[str],
    *,
    cwd: pathlib.Path | None = None,
    environment: dict[str, str] | None = None,
    timeout: int = 600,
) -> subprocess.CompletedProcess[str]:
    env = os.environ.copy()
    if environment:
        env.update(environment)
    try:
        return subprocess.run(
            arguments,
            cwd=cwd,
            env=env,
            text=True,
            capture_output=True,
            check=False,
            timeout=timeout,
        )
    except subprocess.TimeoutExpired as exc:
        return subprocess.CompletedProcess(arguments, 124, exc.stdout or "", exc.stderr or "timeout")
    except OSError as exc:
        # A restricted automation sandbox may deny process inspection even
        # though the provider binary exists. Treat that as unavailable
        # evidence; callers remain conservative and never infer a safe delete.
        return subprocess.CompletedProcess(arguments, 127, "", str(exc))


def git(repository: pathlib.Path, *arguments: str) -> subprocess.CompletedProcess[str]:
    return run_command(["git", "-C", str(repository), *arguments], timeout=60)


def repository_is_dirty(repository: pathlib.Path) -> bool:
    result = git(repository, "status", "--porcelain=v1", "--untracked-files=all")
    if result.returncode != 0:
        raise StorageContractError(f"Git status failed for {repository}")
    return bool(result.stdout.strip())


def git_path_is_ignored_and_untracked(repository: pathlib.Path, path: pathlib.Path) -> bool:
    try:
        relative = path.resolve(strict=False).relative_to(repository.resolve(strict=True)).as_posix()
    except ValueError:
        return False
    ignored = git(repository, "check-ignore", "-q", "--", relative).returncode == 0
    tracked = git(repository, "ls-files", "--error-unmatch", "--", relative).returncode == 0
    return ignored and not tracked


def git_file_is_tracked(repository: pathlib.Path, path: pathlib.Path) -> bool:
    try:
        relative = path.resolve(strict=False).relative_to(repository.resolve(strict=True)).as_posix()
    except ValueError:
        return False
    return git(repository, "ls-files", "--error-unmatch", "--", relative).returncode == 0


def directory_metrics(path: pathlib.Path) -> tuple[int, int, int]:
    """Return bytes, file count, and newest mtime without following symlinks."""
    if not path.exists() or path.is_symlink():
        return 0, 0, 0
    total = 0
    files = 0
    newest = int(path.stat(follow_symlinks=False).st_mtime)
    for root, directories, names in os.walk(path, followlinks=False):
        root_path = pathlib.Path(root)
        safe_directories: list[str] = []
        for name in directories:
            candidate = root_path / name
            if candidate.is_symlink():
                continue
            safe_directories.append(name)
            try:
                newest = max(newest, int(candidate.stat(follow_symlinks=False).st_mtime))
            except OSError:
                pass
        directories[:] = safe_directories
        for name in names:
            candidate = root_path / name
            try:
                information = candidate.stat(follow_symlinks=False)
            except OSError:
                continue
            if stat.S_ISLNK(information.st_mode):
                continue
            files += 1
            total += information.st_size
            newest = max(newest, int(information.st_mtime))
    return total, files, newest


def age_days(newest_epoch: int, now_epoch: int | None = None) -> int:
    if newest_epoch <= 0:
        return 0
    now = int(time.time()) if now_epoch is None else now_epoch
    return max(0, (now - newest_epoch) // 86400)


def disk_snapshot(home: pathlib.Path) -> dict:
    usage = shutil.disk_usage(home)
    free_percent = (usage.free / usage.total * 100.0) if usage.total else 0.0
    return {
        "totalBytes": usage.total,
        "usedBytes": usage.used,
        "freeBytes": usage.free,
        "freePercent": round(free_percent, 2),
    }


def process_inventory() -> set[str]:
    if os.name == "nt":
        result = run_command(["tasklist", "/FO", "CSV", "/NH"], timeout=20)
        if result.returncode != 0:
            return {PROCESS_INVENTORY_UNAVAILABLE}
        return parse_tasklist_processes(result.stdout, os.getpid())
    result = run_command(["ps", "-axo", "pid=,comm=,args="], timeout=20)
    if result.returncode != 0:
        return {PROCESS_INVENTORY_UNAVAILABLE}
    return parse_ps_processes(result.stdout, os.getpid())


def parse_tasklist_processes(output: str, current_pid: int) -> set[str]:
    values: set[str] = set()
    for row in csv.reader(output.splitlines()):
        if len(row) < 2:
            continue
        try:
            process_id = int(row[1])
        except ValueError:
            continue
        if process_id == current_pid:
            continue
        name = pathlib.Path(row[0].strip()).stem.casefold()
        if name:
            values.add(name)
    return values


def parse_ps_processes(output: str, current_pid: int) -> set[str]:
    values: set[str] = set()
    for line in output.splitlines():
        fields = line.strip().split(maxsplit=2)
        if len(fields) < 2:
            continue
        try:
            process_id = int(fields[0])
        except ValueError:
            continue
        if process_id == current_pid:
            continue
        values.add(pathlib.Path(fields[1]).stem.casefold())
        if len(fields) == 3:
            values.update(package_manager_process_tags(fields[2]))
    return values


def package_manager_process_tags(command_line: str) -> set[str]:
    normalized = command_line.replace("\\", "/").casefold()
    patterns = {
        "npm": (r"(?:^|[/\s])npm(?:-cli\.js)?(?:\s|$)",),
        "npx": (r"(?:^|[/\s])npx(?:-cli\.js)?(?:\s|$)",),
        "pnpm": (r"(?:^|[/\s])pnpm(?:\.c?js)?(?:\s|$)",),
        "yarn": (r"(?:^|[/\s])yarn(?:\.c?js)?(?:\s|$)",),
    }
    return {
        name
        for name, expressions in patterns.items()
        if any(re.search(expression, normalized) for expression in expressions)
    }


def adapter_is_busy(adapter: str, processes: set[str], policy: dict) -> bool:
    configured = policy.get("processNames", {}).get(adapter, [])
    expected = {str(item).casefold() for item in configured}
    return PROCESS_INVENTORY_UNAVAILABLE in processes or bool(processes.intersection(expected))


def provider_is_busy(processes: set[str], expected: set[str]) -> bool:
    return PROCESS_INVENTORY_UNAVAILABLE in processes or bool(processes.intersection(expected))


def classify_runtime(entry: dict, configured_adapter: dict | None) -> tuple[str, str]:
    if configured_adapter:
        return str(configured_adapter["runtimeClass"]), str(configured_adapter["mslStatus"])
    declared = str(entry.get("mslStatus", "")).strip().casefold()
    language = f" {str(entry.get('primaryLanguage', '')).strip().casefold()} "
    if any(token in language for token in KNOWN_NON_MSL_TOKENS):
        return "non-msl-unconfigured", "non-msl"
    if any(token in language for token in KNOWN_MSL_TOKENS):
        return "msl", "msl"
    if declared in {"msl", "non-msl", "n/a"}:
        return declared, declared
    return "unknown", "unknown"


def has_non_msl_justification(repository: pathlib.Path) -> bool:
    for name in ("constitution.md", ".specify/memory/constitution.md"):
        path = repository / name
        if not path.is_file():
            continue
        try:
            content = path.read_text(encoding="utf-8", errors="replace")
        except OSError:
            continue
        if re.search(
            r"Non-MSL justification|Nicht-MSL-Begr(?:uendung|ündung)|"
            r"(?:not MSL|Nicht-MSL)[\s\S]{0,240}justification|"
            r"justification[\s\S]{0,240}(?:not MSL|Nicht-MSL)",
            content,
            re.IGNORECASE,
        ):
            return True
    return False


def policy_adapter_map(policy: dict) -> dict[str, dict]:
    adapters = policy.get("nonMslAdapters")
    if not isinstance(adapters, list):
        raise StorageContractError("nonMslAdapters must be an array")
    result: dict[str, dict] = {}
    for adapter in adapters:
        if not isinstance(adapter, dict):
            raise StorageContractError("non-MSL adapter must be an object")
        relative = validate_relative_path(adapter.get("repositoryPath"), "adapter repositoryPath").as_posix()
        if relative.casefold() in result:
            raise StorageContractError(f"duplicate non-MSL adapter: {relative}")
        if adapter.get("adapter") not in {"cc65-make", "cmake-out-of-source"}:
            raise StorageContractError(f"unsupported non-MSL adapter: {adapter.get('adapter')}")
        result[relative.casefold()] = adapter
    return result


def validate_policy(policy: dict) -> None:
    if policy.get("schemaVersion") != 1:
        raise StorageContractError("unsupported storage policy schemaVersion")
    retention = policy.get("retentionDays")
    pressure = policy.get("pressureFreePercent")
    if not isinstance(retention, int) or not 1 <= retention <= 365:
        raise StorageContractError("retentionDays must be between 1 and 365")
    if not isinstance(pressure, int) or not 1 <= pressure <= 50:
        raise StorageContractError("pressureFreePercent must be between 1 and 50")
    policy_adapter_map(policy)
    if not isinstance(policy.get("genericBuildAdapters"), list):
        raise StorageContractError("genericBuildAdapters must be an array")


def registry_level2_entries(registry: dict) -> list[dict]:
    entries = registry.get("repositories")
    if not isinstance(entries, list):
        raise StorageContractError("registry repositories must be an array")
    result = []
    seen: set[str] = set()
    for entry in entries:
        if not isinstance(entry, dict) or entry.get("level") != 2:
            continue
        relative = validate_relative_path(entry.get("path"), "registry repository path").as_posix()
        key = relative.casefold()
        if key in seen:
            raise StorageContractError(f"duplicate Level-2 registry path: {relative}")
        seen.add(key)
        normalized = dict(entry)
        normalized["path"] = relative
        result.append(normalized)
    return result


def candidate_record(
    repository: pathlib.Path,
    path: pathlib.Path,
    adapter: str,
    retention_days: int,
    pressure_mode: bool,
) -> dict:
    contained = contained_path(path, repository, "cleanup candidate")
    if contained.is_symlink():
        raise StorageContractError(f"cleanup candidate is a symlink: {contained}")
    size, files, newest = directory_metrics(contained)
    days = age_days(newest)
    eligible = pressure_mode or days >= retention_days
    return {
        "path": contained.relative_to(repository.resolve()).as_posix(),
        "adapter": adapter,
        "sizeBytes": size,
        "fileCount": files,
        "newestMtimeUtc": (
            datetime.fromtimestamp(newest, timezone.utc).replace(microsecond=0).isoformat().replace("+00:00", "Z")
            if newest else "N/A"
        ),
        "ageDays": days,
        "eligible": eligible,
        "action": "Remove" if eligible else "RetainByAge",
        "status": "Planned" if eligible else "Retained",
    }


def remove_candidate(repository: pathlib.Path, candidate: dict) -> int:
    path = contained_path(repository / candidate["path"], repository, "cleanup candidate")
    if not git_path_is_ignored_and_untracked(repository, path):
        raise StorageContractError(f"candidate lost ignored/untracked proof: {candidate['path']}")
    size_before = directory_metrics(path)[0]
    if path.is_dir() and not path.is_symlink():
        shutil.rmtree(path)
    else:
        raise StorageContractError(f"candidate is not a safe directory: {candidate['path']}")
    candidate["status"] = "Removed"
    candidate["freedBytes"] = size_before
    return size_before


def make_preview(repository: pathlib.Path, make_directory: str, target: str) -> subprocess.CompletedProcess[str]:
    return run_command(["make", "-C", make_directory, "-n", target], cwd=repository, timeout=120)


def validate_cc65_make_preview(preview: str, allowed_paths: Iterable[str], protected: Iterable[str]) -> None:
    allowed = {str(item).replace("\\", "/").strip("/").casefold() for item in allowed_paths}
    protected_suffixes = tuple(pattern[1:].casefold() for pattern in protected if pattern.startswith("*."))
    saw_delete = False
    for line in preview.splitlines():
        match = re.search(r"(?:^|\s)(rm|del)(?:\s|$)(.*)", line, re.IGNORECASE)
        if not match:
            continue
        saw_delete = True
        lower = line.casefold()
        if ".git" in lower or any(suffix in lower for suffix in protected_suffixes):
            raise StorageContractError(f"MakeCleanScopeDrift: protected deletion in preview: {line}")
        try:
            tokens = shlex.split(match.group(2), posix=os.name != "nt")
        except ValueError as exc:
            raise StorageContractError(f"MakeCleanScopeDrift: invalid deletion syntax: {line}") from exc
        targets = [
            token for token in tokens
            if not token.startswith("-") and not (match.group(1).casefold() == "del" and token.startswith("/"))
        ]
        if not targets:
            raise StorageContractError(f"MakeCleanScopeDrift: deletion has no bounded target: {line}")
        for target in targets:
            normalized = target.strip('"\'').replace("\\", "/")
            if (
                normalized.startswith("/")
                or re.match(r"^[A-Za-z]:", normalized)
                or any(character in normalized for character in ("*", "?", "$", "`", ";", "|", "&", ">", "<"))
            ):
                raise StorageContractError(f"MakeCleanScopeDrift: deletion outside declared group: {line}")
            parts = [part.casefold() for part in normalized.split("/") if part not in {"", ".", ".."}]
            if not parts or parts[0] not in allowed:
                raise StorageContractError(f"MakeCleanScopeDrift: deletion outside declared group: {line}")
    if not saw_delete:
        raise StorageContractError("MakeCleanScopeDrift: make preview contains no bounded deletion")


def protected_cc65_evidence(repository: pathlib.Path, patterns: list[str]) -> list[dict]:
    evidence = []
    for root_name in ("samples", "targettest"):
        root = repository / root_name
        if not root.is_dir():
            continue
        for current, directories, names in os.walk(root, followlinks=False):
            directories[:] = [name for name in directories if name not in WALK_EXCLUDES]
            for name in names:
                if not any(fnmatch.fnmatch(name.casefold(), pattern.casefold()) for pattern in patterns):
                    continue
                path = pathlib.Path(current) / name
                if path.is_symlink() or not git_path_is_ignored_and_untracked(repository, path):
                    continue
                try:
                    size = path.stat(follow_symlinks=False).st_size
                except OSError:
                    size = 0
                evidence.append({"path": path.relative_to(repository).as_posix(), "sizeBytes": size})
    return evidence[:500]


def handle_cc65(
    repository: pathlib.Path,
    adapter: dict,
    *,
    mode: str,
    profile: str,
    retention_days: int,
    pressure_mode: bool,
    dirty: bool,
    busy: bool,
) -> tuple[list[dict], list[dict], int, str, list[str]]:
    candidates: list[dict] = []
    protected = protected_cc65_evidence(repository, list(adapter.get("protectedEvidencePatterns", [])))
    freed = 0
    status = "Passed"
    warnings: list[str] = []
    if profile == "deep":
        all_paths = [path for group in adapter["safeGroups"] for path in group["paths"]]
        existing = [repository / path for path in all_paths if (repository / path).exists()]
        for path in existing:
            if not git_path_is_ignored_and_untracked(repository, path):
                raise StorageContractError(f"cc65 deep candidate is not ignored and untracked: {path}")
            candidates.append(candidate_record(repository, path, "cc65-root-clean", retention_days, True))
        if not existing:
            return candidates, protected, 0, status, warnings
        preview = run_command(["make", "-n", "clean"], cwd=repository, timeout=180)
        try:
            if preview.returncode != 0:
                raise StorageContractError("cc65 root make clean preview failed")
            validate_cc65_make_preview(
                preview.stdout + "\n" + preview.stderr,
                all_paths,
                adapter.get("protectedEvidencePatterns", []),
            )
            for candidate in candidates:
                candidate["action"] = "RootMakeClean"
            if mode == "update" and not dirty and not busy:
                before = sum(item["sizeBytes"] for item in candidates)
                result = run_command(["make", "clean"], cwd=repository, timeout=900)
                if result.returncode != 0:
                    raise StorageContractError("cc65 root make clean failed")
                freed = before
                for candidate in candidates:
                    candidate["status"] = "Removed"
                    candidate["freedBytes"] = candidate["sizeBytes"]
            elif mode == "update":
                status = "Warning"
                for candidate in candidates:
                    candidate["status"] = "SkippedDirtyOrBusy"
            return candidates, protected, freed, status, warnings
        except StorageContractError as exc:
            # The upstream root clean currently reaches additional tool and
            # sample paths. Preserve those paths and retain useful cleanup by
            # falling back to the separately proven safe groups.
            warnings.append(f"DeepRootCleanFallback: {exc}")
            candidates.clear()
            status = "Warning"

    effective_pressure = pressure_mode or profile == "deep"
    for group in adapter.get("safeGroups", []):
        paths = [repository / value for value in group.get("paths", []) if (repository / value).exists()]
        if not paths:
            continue
        for path in paths:
            if not git_path_is_ignored_and_untracked(repository, path):
                raise StorageContractError(f"cc65 candidate is not ignored and untracked: {path}")
        group_candidates = [
            candidate_record(repository, path, f"cc65-{group['id']}", retention_days, effective_pressure)
            for path in paths
        ]
        candidates.extend(group_candidates)
        if not any(item["eligible"] for item in group_candidates):
            continue
        preview = make_preview(repository, str(group["makeDirectory"]), str(group["target"]))
        if preview.returncode != 0:
            raise StorageContractError(f"cc65 make preview failed for {group['id']}")
        validate_cc65_make_preview(
            preview.stdout + "\n" + preview.stderr,
            group["paths"],
            adapter.get("protectedEvidencePatterns", []),
        )
        for candidate in group_candidates:
            if candidate["eligible"]:
                candidate["action"] = "NativeMakeClean"
        if mode == "update" and not dirty and not busy:
            before = sum(item["sizeBytes"] for item in group_candidates if item["eligible"])
            result = run_command(
                ["make", "-C", str(group["makeDirectory"]), str(group["target"])],
                cwd=repository,
                timeout=900,
            )
            if result.returncode != 0:
                raise StorageContractError(f"cc65 make clean failed for {group['id']}")
            freed += before
            for candidate in group_candidates:
                if candidate["eligible"]:
                    candidate["status"] = "Removed"
                    candidate["freedBytes"] = candidate["sizeBytes"]
        elif mode == "update":
            status = "Warning"
            for candidate in group_candidates:
                if candidate["eligible"]:
                    candidate["status"] = "SkippedDirtyOrBusy"
    return candidates, protected, freed, status, warnings


def read_cmake_home(cache: pathlib.Path) -> pathlib.Path | None:
    try:
        for line in cache.read_text(encoding="utf-8", errors="replace").splitlines():
            if line.startswith("CMAKE_HOME_DIRECTORY:INTERNAL="):
                return pathlib.Path(line.split("=", 1)[1]).resolve(strict=False)
    except OSError:
        return None
    return None


def discover_cmake_candidates(
    repository: pathlib.Path,
    adapter: dict,
    retention_days: int,
    pressure_mode: bool,
) -> list[dict]:
    patterns = [str(item) for item in adapter.get("buildDirectoryPatterns", [])]
    build_directories: list[pathlib.Path] = []
    for current, directories, names in os.walk(repository, followlinks=False):
        directories[:] = [name for name in directories if name not in WALK_EXCLUDES]
        if "CMakeCache.txt" not in names:
            continue
        build = pathlib.Path(current).resolve(strict=False)
        if not any(fnmatch.fnmatch(build.name, pattern) for pattern in patterns):
            continue
        source = read_cmake_home(build / "CMakeCache.txt")
        if source is None:
            continue
        try:
            source.relative_to(repository.resolve())
        except ValueError:
            continue
        if not git_file_is_tracked(repository, source / "CMakeLists.txt"):
            continue
        if not git_path_is_ignored_and_untracked(repository, build):
            continue
        build_directories.append(build)
    selected: list[pathlib.Path] = []
    for build in sorted(set(build_directories), key=lambda item: len(item.parts)):
        if any(build == parent or parent in build.parents for parent in selected):
            continue
        selected.append(build)
    return [
        candidate_record(repository, build, "cmake-out-of-source", retention_days, pressure_mode)
        for build in selected
    ]


def handle_cmake(
    repository: pathlib.Path,
    adapter: dict,
    *,
    mode: str,
    retention_days: int,
    pressure_mode: bool,
    dirty: bool,
    busy: bool,
) -> tuple[list[dict], int, str]:
    candidates = discover_cmake_candidates(repository, adapter, retention_days, pressure_mode)
    freed = 0
    status = "Passed"
    for candidate in candidates:
        if not candidate["eligible"]:
            continue
        candidate["action"] = "CMakeCleanAndRemoveTree"
        if mode != "update":
            continue
        if dirty or busy:
            candidate["status"] = "SkippedDirtyOrBusy"
            status = "Warning"
            continue
        build = contained_path(repository / candidate["path"], repository, "CMake build tree")
        if shutil.which("cmake") is None:
            candidate["status"] = "ProviderUnavailable"
            status = "Warning"
            continue
        result = run_command(["cmake", "--build", str(build), "--target", "clean"], timeout=900)
        if result.returncode != 0:
            candidate["status"] = "NativeCleanFailed"
            candidate["providerExitCode"] = result.returncode
            status = "Warning"
            continue
        freed += remove_candidate(repository, candidate)
    return candidates, freed, status


def marker_matches(name: str, markers: list[str]) -> bool:
    return any(fnmatch.fnmatch(name, marker) for marker in markers)


def generic_adapter_roots(repository: pathlib.Path, markers: list[str]) -> list[pathlib.Path]:
    roots: set[pathlib.Path] = set()
    for current, directories, names in os.walk(repository, followlinks=False):
        directories[:] = [name for name in directories if name not in WALK_EXCLUDES]
        if any(marker_matches(name, markers) for name in names):
            roots.add(pathlib.Path(current))
    return sorted(roots)


def discover_generic_candidates(
    repository: pathlib.Path,
    adapter: dict,
    retention_days: int,
    pressure_mode: bool,
) -> list[dict]:
    names = set(str(item) for item in adapter.get("directoryNames", []))
    found: set[pathlib.Path] = set()
    for root in generic_adapter_roots(repository, list(adapter.get("markers", []))):
        for current, directories, _ in os.walk(root, followlinks=False):
            current_path = pathlib.Path(current)
            retained: list[str] = []
            for name in directories:
                if name in WALK_EXCLUDES:
                    continue
                candidate = current_path / name
                if name in names:
                    if git_path_is_ignored_and_untracked(repository, candidate) and not candidate.is_symlink():
                        found.add(candidate.resolve(strict=False))
                    continue
                retained.append(name)
            directories[:] = retained
    return [
        candidate_record(repository, path, str(adapter["id"]), retention_days, pressure_mode)
        for path in sorted(found)
    ]


def handle_generic_repository(
    repository: pathlib.Path,
    policy: dict,
    *,
    mode: str,
    retention_days: int,
    pressure_mode: bool,
    dirty: bool,
    processes: set[str],
) -> tuple[list[dict], int, str, list[str]]:
    all_candidates: list[dict] = []
    freed = 0
    status = "Passed"
    adapters_used: list[str] = []
    for adapter in policy.get("genericBuildAdapters", []):
        candidates = discover_generic_candidates(repository, adapter, retention_days, pressure_mode)
        if not candidates:
            continue
        adapter_id = str(adapter["id"])
        adapters_used.append(adapter_id)
        busy = adapter_is_busy(adapter_id, processes, policy)
        for candidate in candidates:
            if candidate["eligible"] and mode == "update":
                if dirty or busy:
                    candidate["status"] = "SkippedDirtyOrBusy"
                    status = "Warning"
                else:
                    freed += remove_candidate(repository, candidate)
        all_candidates.extend(candidates)
    return all_candidates, freed, status, adapters_used


def cache_path_metrics(paths: Iterable[pathlib.Path]) -> tuple[int, int]:
    size = 0
    newest = 0
    for path in paths:
        if path.exists() and not path.is_symlink():
            current_size, _, current_newest = directory_metrics(path)
            size += current_size
            newest = max(newest, current_newest)
    return size, newest


def provider_item(provider: str, action: str, size: int, mode: str) -> dict:
    return {
        "provider": provider,
        "action": action,
        "sizeBytesBefore": size,
        "status": "Planned" if mode != "update" else "Pending",
        "exitCode": 0,
    }


def execute_provider(item: dict, command: list[str], *, environment: dict[str, str] | None = None) -> None:
    result = run_command(command, environment=environment, timeout=900)
    item["exitCode"] = result.returncode
    item["status"] = "Completed" if result.returncode == 0 else "Warning"
    if result.returncode != 0:
        detail = (result.stderr or result.stdout).strip().splitlines()
        item["evidence"] = detail[-1][:300] if detail else "Provider command failed"


def handle_cache_providers(
    home: pathlib.Path,
    *,
    mode: str,
    profile: str,
    pressure_mode: bool,
    retention_days: int,
    processes: set[str],
) -> list[dict]:
    items: list[dict] = []
    npm = shutil.which("npm")
    npm_cache = pathlib.Path(os.environ.get("npm_config_cache", home / ".npm"))
    npm_cache_safe = False
    try:
        npm_cache.resolve(strict=False).relative_to(home.resolve(strict=True))
        npm_cache_safe = not npm_cache.is_symlink()
    except ValueError:
        npm_cache_safe = False
    npm_size, _ = cache_path_metrics([npm_cache]) if npm_cache_safe else (0, 0)
    if npm and not npm_cache_safe:
        item = provider_item("npm", "cache-maintenance", 0, mode)
        item["status"] = "ProviderUnavailable"
        item["evidence"] = "npm cache path is outside the verified home or is a symlink"
        items.append(item)
    elif npm and npm_size:
        action = "cache-clean" if pressure_mode or profile == "deep" else "cache-verify"
        item = provider_item("npm", action, npm_size, mode)
        npm_busy_names = {"npm", "npx", "pnpm", "yarn"}
        if os.name == "nt":
            # tasklist cannot distinguish package-manager Node processes from
            # unrelated Node applications, so Windows remains conservative.
            npm_busy_names.add("node")
        if mode == "update" and not provider_is_busy(processes, npm_busy_names):
            command = [npm, "cache", "clean", "--force"] if action == "cache-clean" else [npm, "cache", "verify"]
            execute_provider(item, command)
        elif mode == "update":
            item["status"] = (
                "SkippedProcessInventoryUnavailable"
                if PROCESS_INVENTORY_UNAVAILABLE in processes
                else "SkippedBusy"
            )
        items.append(item)

    dotnet = shutil.which("dotnet")
    nuget_locations: dict[str, list[pathlib.Path]] = {
        "global-packages": [home / ".nuget" / "packages"],
        "http-cache": [home / ".local" / "share" / "NuGet" / "v3-cache"],
        "plugins-cache": [home / ".local" / "share" / "NuGet" / "plugins-cache"],
        "temp": [pathlib.Path(tempfile.gettempdir()) / "NuGetScratch"],
    }
    if os.name == "nt":
        local = pathlib.Path(os.environ.get("LOCALAPPDATA", home / "AppData" / "Local"))
        nuget_locations.update({
            "http-cache": [local / "NuGet" / "v3-cache"],
            "plugins-cache": [local / "NuGet" / "plugins-cache"],
            "temp": [pathlib.Path(tempfile.gettempdir()) / "NuGetScratch"],
        })
    selected_nuget = ["http-cache", "plugins-cache", "temp"]
    if profile == "deep":
        selected_nuget.append("global-packages")
    for location in selected_nuget:
        size, newest = cache_path_metrics(nuget_locations[location])
        if not size:
            continue
        if profile != "deep" and not pressure_mode and age_days(newest) < retention_days:
            continue
        item = provider_item("nuget", f"clear-{location}", size, mode)
        if mode == "update" and dotnet and not provider_is_busy(
            processes, {"dotnet", "msbuild", "vstest", "testhost"}
        ):
            execute_provider(
                item,
                [dotnet, "nuget", "locals", location, "--clear", "--force-english-output"],
                environment={
                    "DOTNET_CLI_TELEMETRY_OPTOUT": "1",
                    "DOTNET_NOLOGO": "1",
                    "DOTNET_SKIP_FIRST_TIME_EXPERIENCE": "1",
                },
            )
        elif mode == "update":
            item["status"] = (
                "ProviderUnavailable"
                if not dotnet
                else "SkippedProcessInventoryUnavailable"
                if PROCESS_INVENTORY_UNAVAILABLE in processes
                else "SkippedBusy"
            )
        items.append(item)

    go = shutil.which("go")
    if go and (pressure_mode or profile == "deep"):
        action = "clean-build-test-module-cache" if profile == "deep" else "clean-build-test-cache"
        item = provider_item("go", action, 0, mode)
        if mode == "update" and not provider_is_busy(processes, {"go", "gofmt"}):
            command = [go, "clean", "-cache", "-testcache"]
            if profile == "deep":
                command.append("-modcache")
            execute_provider(item, command)
        elif mode == "update":
            item["status"] = (
                "SkippedProcessInventoryUnavailable"
                if PROCESS_INVENTORY_UNAVAILABLE in processes
                else "SkippedBusy"
            )
        items.append(item)

    brew = shutil.which("brew")
    if brew:
        cache = home / "Library" / "Caches" / "Homebrew"
        size, _ = cache_path_metrics([cache])
        item = provider_item("homebrew", "prune-all" if pressure_mode or profile == "deep" else "cleanup", size, mode)
        if mode == "update" and not provider_is_busy(processes, {"brew"}):
            command = [brew, "cleanup"]
            if pressure_mode or profile == "deep":
                command.append("--prune=all")
            execute_provider(item, command)
        elif mode == "update":
            item["status"] = (
                "SkippedProcessInventoryUnavailable"
                if PROCESS_INVENTORY_UNAVAILABLE in processes
                else "SkippedBusy"
            )
        items.append(item)
    return items


def podman_socket_arguments() -> tuple[list[str], str | None]:
    if sys.platform != "darwin":
        return [], None
    inspect = run_command(["podman", "machine", "inspect"], timeout=30)
    if inspect.returncode != 0:
        return [], "Podman machine inspection failed"
    try:
        machines = json.loads(inspect.stdout)
    except json.JSONDecodeError:
        return [], "Podman machine inspection returned invalid JSON"
    if not isinstance(machines, list):
        return [], "Podman machine inspection returned an invalid shape"
    for machine in machines:
        if not isinstance(machine, dict) or str(machine.get("State", "")).casefold() != "running":
            continue
        socket_path = machine.get("ConnectionInfo", {}).get("PodmanSocket", {}).get("Path")
        if not isinstance(socket_path, str):
            continue
        path = pathlib.Path(socket_path)
        try:
            mode = path.stat().st_mode
        except OSError:
            continue
        if stat.S_ISSOCK(mode):
            return ["--url", f"unix://{path}"], None
    return [], "Podman machine is not running or has no verified Unix socket"


def handle_container_provider(
    *,
    mode: str,
    pressure_mode: bool,
    retention_days: int,
    processes: set[str],
) -> list[dict]:
    items: list[dict] = []
    if provider_is_busy(processes, {"podman", "docker", "buildah"}):
        return [{
            "provider": "containers",
            "action": "dangling-image-prune",
            "sizeBytesBefore": 0,
            "status": (
                "SkippedProcessInventoryUnavailable"
                if PROCESS_INVENTORY_UNAVAILABLE in processes
                else "SkippedBusy"
            ),
            "exitCode": 0,
        }]
    podman = shutil.which("podman")
    docker = shutil.which("docker")
    if podman:
        socket_arguments, failure = podman_socket_arguments()
        item = provider_item("podman", "dangling-image-prune", 0, mode)
        if failure:
            item["status"] = "ProviderUnavailable"
            item["evidence"] = failure
            items.append(item)
            return items
        command = [podman, *socket_arguments, "image", "prune", "--force"]
        if not pressure_mode:
            command.extend(["--filter", f"until={retention_days * 24}h"])
        item["commandContract"] = "image prune --force" + ("" if pressure_mode else f" --filter until={retention_days * 24}h")
        if mode == "update":
            execute_provider(item, command)
        items.append(item)
        return items
    if docker:
        item = provider_item("docker", "dangling-image-prune", 0, mode)
        command = [docker, "image", "prune", "--force"]
        if not pressure_mode:
            command.extend(["--filter", f"until={retention_days * 24}h"])
        item["commandContract"] = "image prune --force" + ("" if pressure_mode else f" --filter until={retention_days * 24}h")
        if mode == "update":
            execute_provider(item, command)
        items.append(item)
    return items


def repository_result_base(entry: dict, runtime_class: str, msl_status: str, adapter: dict | None) -> dict:
    return {
        "repositoryPath": entry["path"],
        "runtimeClass": runtime_class,
        "mslStatus": msl_status,
        "cleanupAdapter": adapter.get("adapter") if adapter else "generic-detection",
        "justificationEvidence": adapter.get("justificationEvidence", "N/A") if adapter else "N/A",
        "justification": adapter.get("justification", "N/A") if adapter else "N/A",
        "compensatingControls": list(adapter.get("compensatingControls", [])) if adapter else [],
        "eligibleBytes": 0,
        "freedBytes": 0,
        "preservedEvidence": [],
        "candidates": [],
        "status": "Passed",
    }


def execute(args: argparse.Namespace) -> int:
    started = utc_now()
    try:
        home = args.home_dir.resolve(strict=True)
        policy = load_json_object(args.policy, "storage policy")
        registry = load_json_object(args.registry, "Level-2 registry")
        validate_policy(policy)
        run_id = args.run_id or str(uuid.uuid4())
        try:
            if str(uuid.UUID(run_id)) != run_id:
                raise StorageContractError("run ID must be a canonical UUID")
        except ValueError as exc:
            raise StorageContractError("run ID must be a canonical UUID") from exc
        if args.profile not in VALID_PROFILES or args.mode not in VALID_MODES:
            raise StorageContractError("invalid profile or mode")
        if args.profile == "deep" and args.mode == "update" and not args.confirm_deep_cleanup:
            raise StorageContractError("deep cleanup requires --confirm-deep-cleanup in update mode")
        before = disk_snapshot(home)
        pressure_mode = before["freePercent"] < policy["pressureFreePercent"]
        retention_days = int(policy["retentionDays"])
        result: dict = {
            "schemaVersion": ENGINE_SCHEMA,
            "runId": run_id,
            "platform": sys.platform,
            "mode": args.mode,
            "profile": args.profile,
            "retentionDays": retention_days,
            "pressureFreePercent": policy["pressureFreePercent"],
            "pressureMode": pressure_mode,
            "startedAt": started,
            "completedAt": started,
            "overallStatus": "SUCCESS",
            "exitCode": 0,
            "diskBefore": before,
            "diskAfter": before,
            "eligibleBytes": 0,
            "freedBytes": 0,
            "repositories": [],
            "providers": [],
            "warnings": [],
            "nextAction": "N/A",
        }
        if args.profile == "none":
            result["completedAt"] = utc_now()
            atomic_write_json(args.report, result)
            print("STORAGE\tSUCCESS\tprofile=none\tfreed=0")
            return 0

        adapters = policy_adapter_map(policy)
        processes = process_inventory()
        for entry in registry_level2_entries(registry):
            relative = entry["path"]
            registry_path = home / pathlib.PurePosixPath(relative)
            repository = registry_path.resolve(strict=False)
            adapter = adapters.get(relative.casefold())
            runtime_class, msl_status = classify_runtime(entry, adapter)
            repo_result = repository_result_base(entry, runtime_class, msl_status, adapter)
            result["repositories"].append(repo_result)
            if registry_path.is_symlink():
                repo_result["status"] = "SkippedSymlinkRepository"
                result["warnings"].append(f"{relative}: SkippedSymlinkRepository")
                continue
            if not repository.is_dir() or not (repository / ".git").exists():
                repo_result["status"] = "SkippedMissing"
                continue
            try:
                repository.relative_to(home)
            except ValueError:
                raise StorageContractError(f"registry repository escapes home: {relative}")
            dirty = repository_is_dirty(repository)
            repo_result["repositoryDirty"] = dirty
            if msl_status == "non-msl" and not has_non_msl_justification(repository):
                repo_result["status"] = "NonMslJustificationMissing"
                result["warnings"].append(f"{relative}: NonMslJustificationMissing")
                continue
            if msl_status == "non-msl" and adapter is None:
                repo_result["status"] = "UnsupportedNonMslProfile"
                result["warnings"].append(f"{relative}: UnsupportedNonMslProfile")
                continue
            try:
                if adapter and adapter["adapter"] == "cc65-make":
                    busy = adapter_is_busy("cc65-make", processes, policy)
                    candidates, preserved, freed, status, adapter_warnings = handle_cc65(
                        repository,
                        adapter,
                        mode=args.mode,
                        profile=args.profile,
                        retention_days=retention_days,
                        pressure_mode=pressure_mode,
                        dirty=dirty,
                        busy=busy,
                    )
                    repo_result["candidates"] = candidates
                    repo_result["preservedEvidence"] = preserved
                    repo_result["freedBytes"] = freed
                    repo_result["status"] = status
                    if adapter_warnings:
                        repo_result["warnings"] = adapter_warnings
                        result["warnings"].extend(
                            f"{relative}: {warning}" for warning in adapter_warnings
                        )
                elif adapter and adapter["adapter"] == "cmake-out-of-source":
                    busy = adapter_is_busy("cmake-out-of-source", processes, policy)
                    candidates, freed, status = handle_cmake(
                        repository,
                        adapter,
                        mode=args.mode,
                        retention_days=retention_days,
                        pressure_mode=pressure_mode,
                        dirty=dirty,
                        busy=busy,
                    )
                    repo_result["candidates"] = candidates
                    repo_result["freedBytes"] = freed
                    repo_result["status"] = status
                else:
                    candidates, freed, status, used = handle_generic_repository(
                        repository,
                        policy,
                        mode=args.mode,
                        retention_days=retention_days,
                        pressure_mode=pressure_mode,
                        dirty=dirty,
                        processes=processes,
                    )
                    repo_result["cleanupAdapter"] = ",".join(used) if used else "none-detected"
                    repo_result["candidates"] = candidates
                    repo_result["freedBytes"] = freed
                    repo_result["status"] = status
            except StorageContractError as exc:
                repo_result["status"] = "Warning"
                repo_result["warning"] = str(exc)
                result["warnings"].append(f"{relative}: {exc}")
            repo_result["eligibleBytes"] = sum(
                int(candidate.get("sizeBytes", 0))
                for candidate in repo_result["candidates"]
                if candidate.get("eligible")
            )

        result["providers"].extend(handle_cache_providers(
            home,
            mode=args.mode,
            profile=args.profile,
            pressure_mode=pressure_mode,
            retention_days=retention_days,
            processes=processes,
        ))
        result["providers"].extend(handle_container_provider(
            mode=args.mode,
            pressure_mode=pressure_mode,
            retention_days=retention_days,
            processes=processes,
        ))
        for item in result["providers"]:
            if item.get("status") in {
                "Warning",
                "ProviderUnavailable",
                "SkippedBusy",
                "SkippedProcessInventoryUnavailable",
            }:
                result["warnings"].append(f"{item['provider']}: {item['status']}")
        result["eligibleBytes"] = sum(item["eligibleBytes"] for item in result["repositories"])
        result["freedBytes"] = sum(item["freedBytes"] for item in result["repositories"])
        result["diskAfter"] = disk_snapshot(home)
        if args.mode == "update":
            measured = max(0, result["diskAfter"]["freeBytes"] - before["freeBytes"])
            result["freedBytesMeasured"] = measured
        result["completedAt"] = utc_now()
        if result["warnings"]:
            result["overallStatus"] = "SUCCESS_WITH_WARNINGS"
            result["nextAction"] = "Warnungen im Storage-Bericht prüfen / review warnings in the storage report"
        atomic_write_json(args.report, result)
        print(
            f"STORAGE\t{result['overallStatus']}\tprofile={args.profile}\t"
            f"pressure={str(pressure_mode).lower()}\teligible={result['eligibleBytes']}\t"
            f"freed={result['freedBytes']}"
        )
        return 0
    except StorageContractError as exc:
        failure = {
            "schemaVersion": ENGINE_SCHEMA,
            "runId": args.run_id or "N/A",
            "platform": sys.platform,
            "mode": args.mode,
            "profile": args.profile,
            "startedAt": started,
            "completedAt": utc_now(),
            "overallStatus": "FAILED",
            "exitCode": 2,
            "failureClass": "StorageContractError",
            "summary": str(exc),
            "repositories": [],
            "providers": [],
            "warnings": [],
            "nextAction": "Policy, Register und Pfadnachweis prüfen / review policy, registry, and path evidence",
        }
        atomic_write_json(args.report, failure)
        print(f"STORAGE\tFAILED\t{exc}", file=sys.stderr)
        return 2


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--home-dir", type=pathlib.Path, required=True)
    parser.add_argument("--registry", type=pathlib.Path, required=True)
    parser.add_argument("--policy", type=pathlib.Path, required=True)
    parser.add_argument("--report", type=pathlib.Path, required=True)
    parser.add_argument("--mode", choices=sorted(VALID_MODES), required=True)
    parser.add_argument("--profile", choices=sorted(VALID_PROFILES), default="safe")
    parser.add_argument("--confirm-deep-cleanup", action="store_true")
    parser.add_argument("--run-id")
    return parser


def main() -> int:
    return execute(build_parser().parse_args())


if __name__ == "__main__":
    raise SystemExit(main())
