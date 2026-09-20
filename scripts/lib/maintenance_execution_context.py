"""Bounded Podman transport shared by the Bash and PowerShell orchestrators.

DE: Netzwerk-Git bleibt auf dem Host; lokale Repo-Operationen haben keinen
Host-Fallback. Container-IDs werden pro Lauf gebunden, nicht konfiguriert.
EN: Network Git stays on the host; local repository operations never fall
back to the host. Container IDs are bound per run, not configured.
"""
from __future__ import annotations

import datetime
import json
import pathlib
import re
import subprocess
import hashlib
import argparse
import os
import sys
import stat


class ExecutionContextError(ValueError):
    """A missing or changed boundary blocks execution."""


def relative_path(value: str) -> pathlib.PurePosixPath:
    if not isinstance(value, str) or not value or "\\" in value or ":" in value or any(ord(c) < 32 for c in value):
        raise ExecutionContextError("Invalid relative path")
    path = pathlib.PurePosixPath(value)
    if path.is_absolute() or any(part in ("", ".", "..") for part in value.split("/")):
        raise ExecutionContextError("Unsafe relative path")
    return path


def validate_contract(data: dict, today: datetime.date | None = None, *, check_expiry: bool = True) -> dict:
    expected = {"schemaVersion", "container", "user", "source", "approvalPath", "expiresOn", "mounts"}
    if not isinstance(data, dict) or set(data) != expected or type(data["schemaVersion"]) is not int or data["schemaVersion"] != 1:
        raise ExecutionContextError("Invalid execution context contract")
    for key in ("container", "user"):
        if not isinstance(data[key], str) or not re.fullmatch(r"[a-zA-Z0-9][a-zA-Z0-9_.-]*", data[key]):
            raise ExecutionContextError(f"Invalid {key}")
    relative_path(data["approvalPath"])
    if not isinstance(data["expiresOn"], str) or not re.fullmatch(r"\d{4}-\d{2}-\d{2}", data["expiresOn"]):
        raise ExecutionContextError("Invalid approval expiry")
    try:
        expires = datetime.date.fromisoformat(data["expiresOn"])
    except (ValueError, TypeError) as exc:
        raise ExecutionContextError("Invalid approval expiry") from exc
    if check_expiry and (today or datetime.datetime.now(datetime.timezone.utc).date()) > expires:
        raise ExecutionContextError("Sandbox approval expired")
    mounts = data["mounts"]
    if not isinstance(mounts, list) or not mounts:
        raise ExecutionContextError("Missing mount mappings")
    hosts, destinations = set(), set()
    for item in mounts:
        if not isinstance(item, dict) or set(item) != {"host", "container"}:
            raise ExecutionContextError("Invalid mount mapping")
        host = relative_path(item["host"])
        destination = item["container"]
        if not isinstance(destination, str) or not destination.startswith("/"):
            raise ExecutionContextError("Container path must be absolute")
        relative_path(destination[1:])
        if len(host.parts) != 1 or len(pathlib.PurePosixPath(destination).parts) != 2:
            raise ExecutionContextError("Mount mappings must name distinct workspace roots")
        if str(host).casefold() in hosts or destination in destinations:
            raise ExecutionContextError("Duplicate mount mapping")
        hosts.add(str(host).casefold())
        destinations.add(destination)
    if not isinstance(data["source"], str) or not data["source"].startswith("/"):
        raise ExecutionContextError("Invalid source path")
    relative_path(data["source"][1:])
    return data


# This small, fixed program runs *inside* Podman. Arguments are JSON, never
# shell fragments. Resolve every path component there before touching Git.
REMOTE_PROGRAM = r'''
import json, pathlib, re, subprocess, sys
p = json.load(sys.stdin)
root = pathlib.Path(p["root"])
repo = pathlib.Path(p["repository"])
if root.resolve() != root or repo.resolve() != repo:
    raise SystemExit("Symlink boundary rejected")
repo.relative_to(root)
if (repo / ".git").is_symlink():
    raise SystemExit("Symlink Git directory rejected")
if p["action"] == "probe":
    print(json.dumps({"exists":repo.exists(), "directory":repo.is_dir(),
                      "git":(repo / ".git").is_dir()}))
elif p["action"] == "git":
    if not (repo / ".git").is_dir():
        raise SystemExit("Missing declared Git repository")
    args = p["arguments"]
    allowed = {"remote", "symbolic-ref", "rev-parse", "rev-list", "status", "cat-file", "merge", "diff", "log"}
    effective = args[2:] if args[:2] == ["-c", "core.quotePath=false"] else args
    if not effective or effective[0] not in allowed:
        raise SystemExit("Git operation outside local maintenance contract")
    if effective[0] == "remote" and effective != ["remote", "get-url", "origin"]:
        raise SystemExit("Remote mutation is not allowed")
    if effective[0] == "symbolic-ref" and effective not in (
        ["symbolic-ref", "--quiet", "refs/remotes/origin/HEAD"],
        ["symbolic-ref", "--quiet", "--short", "HEAD"]):
        raise SystemExit("Symbolic-ref mutation is not allowed")
    if effective[0] == "merge" and (len(effective) != 3 or effective[1] != "--ff-only" or not re.fullmatch(r"[0-9a-f]{40,64}", effective[2])):
        raise SystemExit("Only bounded fast-forward merge is allowed")
    result = subprocess.run(["git", "-C", str(repo), *args], capture_output=True, text=True)
    print(json.dumps({"returncode":result.returncode,"stdout":result.stdout,"stderr":result.stderr}))
else:
    raise SystemExit("Unknown maintenance action")
'''

PHASE_PROGRAM = r'''
import contextlib, hashlib, importlib, io, json, pathlib, sys
p = json.load(sys.stdin)
source = pathlib.Path(p["source"])
for relative, expected in p["bindings"].items():
    path = source / relative
    if path.resolve() != path or hashlib.sha256(path.read_bytes().replace(b"\r\n", b"\n")).hexdigest() != expected:
        raise SystemExit("Delegated source binding mismatch")
sys.path.insert(0, str(source / "scripts/lib"))
worker = importlib.import_module("maintenance_container_worker")
log = io.StringIO()
with contextlib.redirect_stdout(log):
    result = worker.execute(p)
result["log"] = log.getvalue()
print(json.dumps(result))
'''


SOURCE_PROGRAM = r'''
import hashlib, json, os, pathlib, shutil, sys
p = json.load(sys.stdin)
source = pathlib.Path(p["source"])
if source.resolve() != source or not source.is_dir() or os.access(source, os.W_OK):
    raise SystemExit("Canonical source must be immutable to the sandbox user")
if not all(shutil.which(t) for t in ("git", "bash", "pwsh", "python3")):
    raise SystemExit("Missing maintenance tool")
for relative, expected in p["bindings"].items():
    path = source / relative
    if path.resolve() != path or not path.is_file() or os.access(path, os.W_OK):
        raise SystemExit("Missing or writable source: " + relative)
    if hashlib.sha256(path.read_bytes().replace(b"\r\n", b"\n")).hexdigest() != expected:
        raise SystemExit("Source binding mismatch: " + relative)
print("ready")
'''


def source_bindings(source: pathlib.Path) -> dict:
    """Bind only declared package members, including the declaration itself."""
    manifest = "scripts/config/agentic-toolchain-maintenance-files.json"
    package = json.loads((source / manifest).read_text(encoding="utf-8"))
    paths = {manifest, *(entry["path"] for entry in package["files"])}
    paths.update({"scripts/lib/maintenance_execution_context.py",
                  "scripts/lib/maintenance_container_worker.py",
                  "scripts/config/maintenance-execution-contexts.json"})
    bindings = {}
    source = source.resolve()
    for relative in sorted(paths):
        relative_path(relative)
        path = source / relative
        if path.resolve() != path or not path.is_file():
            raise ExecutionContextError("Unsafe or missing package source: " + relative)
        # The declared package consists of text files. Match repository text
        # across Windows CRLF and the Linux image without ignoring other bytes.
        bindings[relative] = hashlib.sha256(path.read_bytes().replace(b"\r\n", b"\n")).hexdigest()
    return bindings


def filter_registry(router, registry):
    return {**registry, "repositories": [entry for entry in registry["repositories"]
                                         if not router.mapping(router.home / entry["path"])]}


def run_phase(args):
    manifest = json.loads(args.manifest.read_text(encoding="utf-8"))
    router = ExecutionContexts(args.contract, args.home_dir, manifest["targets"])
    report = json.loads(args.report.read_text(encoding="utf-8"))
    evidence = router.preflight(args.source)
    previous = next((item for item in report.get("executionContexts", []) if item["context"] == "container"), None)
    if previous is None or any(previous[key] != evidence[key] for key in ("containerId", "image", "approvalSha256", "sourceSha256")):
        raise ExecutionContextError("Delegation no longer matches fleet preflight")
    registry_bytes = args.registry.read_bytes()
    if args.registry.is_symlink():
        raise ExecutionContextError("Registry symlink is not a writable delegation target")
    registry = json.loads(registry_bytes)
    if args.phase == "host-registry":
        args.output.write_text(json.dumps(filter_registry(router, registry)), encoding="utf-8")
        return 0
    mode = args.mode
    if mode == "update" and report.get("mutationBarrier", {}).get("domainMutationAllowed") is not True:
        raise ExecutionContextError("Fleet mutation barrier is closed")
    targets = {item["path"]: item for item in manifest["targets"] if item["active"]}
    entries = []
    for entry in registry["repositories"]:
        mapping = router.mapping(router.home / entry["path"])
        if mapping:
            entries.append({"registry": entry, "targetId": targets[entry["path"]]["id"],
                            "containerRoot": mapping[0], "containerPath": mapping[1]})
    expected = {item["path"] for item in manifest["targets"] if item["active"] and router.mapping(router.home / item["path"])}
    if {entry["registry"]["path"] for entry in entries} != expected:
        raise ExecutionContextError("Registry does not cover every delegated target")
    bindings = source_bindings(args.source)
    payload = {"source": router.data["source"], "mode": mode, "phase": args.phase,
               "entries": entries, "bindings": bindings, "runId": report["runId"],
               "defaultPresetProfile": registry.get("defaultPresetProfile", "standard-eight-governance-presets"),
               "repairDrift": args.repair_drift, "cleanupProfile": args.cleanup_profile,
               "confirmDeepCleanup": args.confirm_deep_cleanup}
    result = subprocess.run(["podman", "exec", "-i", router.container_id, "python3", "-c", PHASE_PROGRAM],
                            input=json.dumps(payload), text=True, capture_output=True, timeout=3600)
    if result.returncode:
        raise ExecutionContextError("Container phase failed (source drift, unavailable tool or lost sandbox); no host fallback")
    output = json.loads(result.stdout)
    output.update(phase=args.phase, executionContext="container", containerId=router.container_id)
    if args.phase == "registry" and mode == "update" and output["status"] == "Passed":
        translated = {entry["containerPath"]: entry["registry"]["path"] for entry in entries}
        replacement = {}
        for entry in output["registry"]:
            path = translated[entry["path"]]
            replacement[path] = {**entry, "path": path}
        if set(replacement) != expected or args.registry.read_bytes() != registry_bytes:
            raise ExecutionContextError("Concurrent or unexpected registry change")
        registry["repositories"] = [replacement.get(entry["path"], entry) for entry in registry["repositories"]]
        temporary = args.registry.with_name(args.registry.name + ".delegation-tmp")
        with temporary.open("x", encoding="utf-8") as stream:
            json.dump(registry, stream, ensure_ascii=False, indent=2)
            stream.write("\n")
        os.chmod(temporary, stat.S_IMODE(args.registry.stat().st_mode))
        os.replace(temporary, args.registry)
    report.setdefault("delegatedPhases", []).append(output)
    from agentic_workspace_fleet import write_report
    write_report(args.report, report)
    for row in output["records"]:
        print(f"CONTAINER\t{args.phase}\t{row.get('targetId', 'storage')}\t{row['status']}")
    if output["status"] == "Passed" and output.get("repaired"):
        return 3
    return {"Passed": 0, "Blocked": 1, "Failed": 2}[output["status"]]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--contract", type=pathlib.Path, required=True)
    parser.add_argument("--manifest", type=pathlib.Path, required=True)
    parser.add_argument("--home-dir", type=pathlib.Path, required=True)
    parser.add_argument("--source", type=pathlib.Path, required=True)
    parser.add_argument("--registry", type=pathlib.Path, required=True)
    parser.add_argument("--report", type=pathlib.Path, required=True)
    parser.add_argument("--phase", choices=("registry", "propagation", "preset-profiles", "storage-cleanup", "host-registry"), required=True)
    parser.add_argument("--mode", choices=("check-only", "dry-run", "update"), required=True)
    parser.add_argument("--output", type=pathlib.Path)
    parser.add_argument("--repair-drift", action="store_true")
    parser.add_argument("--cleanup-profile", choices=("none", "safe", "deep"), default="safe")
    parser.add_argument("--confirm-deep-cleanup", action="store_true")
    args = parser.parse_args()
    try:
        return run_phase(args)
    except (ExecutionContextError, OSError, ValueError, KeyError, subprocess.SubprocessError) as exc:
        print(f"CONTAINER\t{args.phase}\tBLOCKED\t{exc}", file=sys.stderr)
        return 2


class ExecutionContexts:
    def __init__(self, contract: pathlib.Path, home: pathlib.Path, targets: list[dict]):
        self.data = validate_contract(json.loads(contract.read_text(encoding="utf-8")))
        self.home = home.absolute()
        self.targets = {item["path"] for item in targets if item.get("active") and item.get("kind") == "git-repository"}
        self.container_id = None
        self.image = None

    def mapping(self, repository: pathlib.Path) -> tuple[str, str] | None:
        try:
            relative = repository.absolute().relative_to(self.home).as_posix()
        except ValueError:
            return None
        for item in self.data["mounts"]:
            root = item["host"]
            if relative == root or relative.startswith(root + "/"):
                if relative not in self.targets:
                    raise ExecutionContextError("Undeclared sandbox target")
                suffix = relative[len(root):]
                return item["container"], item["container"] + suffix
        return None

    def inspect(self) -> dict:
        try:
            process = subprocess.run(["podman", "inspect", self.container_id or self.data["container"]],
                                     capture_output=True, text=True, timeout=30, check=True)
            rows = json.loads(process.stdout)
            if len(rows) != 1:
                raise ExecutionContextError("Ambiguous container")
            info = rows[0]
        except (OSError, subprocess.SubprocessError, ValueError) as exc:
            raise ExecutionContextError("Sandbox unavailable; no host fallback") from exc
        if not info.get("State", {}).get("Running") or info.get("Config", {}).get("User") != self.data["user"]:
            raise ExecutionContextError("Sandbox stopped or unexpected user")
        if self.container_id and (info["Id"] != self.container_id or info["Image"] != self.image):
            raise ExecutionContextError("Sandbox identity changed during run")
        for mapping in self.data["mounts"]:
            matches = [m for m in info.get("Mounts", []) if m.get("Destination") == mapping["container"]]
            if len(matches) != 1 or matches[0].get("Type") != "bind" or not matches[0].get("RW"):
                raise ExecutionContextError("Missing writable workspace mount")
            if pathlib.Path(matches[0]["Source"]).absolute() != self.home / mapping["host"]:
                raise ExecutionContextError("Workspace mount source mismatch")
        return info

    def preflight(self, source: pathlib.Path | None = None) -> dict:
        validate_contract(self.data)
        # Owner evidence is read on the control plane; this grants no new role.
        approval = self.home.joinpath(*relative_path(self.data["approvalPath"]).parts)
        if approval.resolve() != approval or not approval.is_file():
            raise ExecutionContextError("Owner approval evidence missing or symlinked")
        evidence = approval.read_text(encoding="utf-8")
        expiry_label = datetime.date.fromisoformat(self.data["expiresOn"]).strftime("%d.%m.%Y")
        if expiry_label not in evidence or "Owner" not in evidence:
            raise ExecutionContextError("Owner approval evidence does not match the bounded contract")
        info = self.inspect()
        self.container_id, self.image = info["Id"], info["Image"]
        bindings = source_bindings(source or pathlib.Path(__file__).resolve().parents[2])
        result = subprocess.run(
            ["podman", "exec", "-i", self.container_id, "python3", "-c", SOURCE_PROGRAM],
            input=json.dumps({"source": self.data["source"], "bindings": bindings}),
            text=True, capture_output=True, timeout=60)
        if result.returncode:
            raise ExecutionContextError("Sandbox source binding or required tools invalid; publish and pin the reviewed package before maintenance")
        return {"context": "container", "containerId": self.container_id, "image": self.image,
                "expiresOn": self.data["expiresOn"], "approvalPath": self.data["approvalPath"],
                "approvalSha256": hashlib.sha256(evidence.encode("utf-8")).hexdigest(),
                "sourceSha256": hashlib.sha256(json.dumps(bindings, sort_keys=True).encode()).hexdigest()}

    def request(self, repository: pathlib.Path, action: str, arguments: tuple[str, ...] = ()) -> dict:
        mapping = self.mapping(repository)
        if mapping is None:
            raise ExecutionContextError("Target is not delegated")
        if self.container_id is None:
            raise ExecutionContextError("Sandbox preflight required")
        self.inspect()
        process = subprocess.run(
            ["podman", "exec", "-i", self.container_id, "python3", "-c", REMOTE_PROGRAM],
            input=json.dumps({"root": mapping[0], "repository": mapping[1], "action": action,
                              "arguments": arguments}), text=True, capture_output=True, timeout=120)
        if process.returncode:
            raise ExecutionContextError("Container maintenance request failed; no host fallback")
        try:
            return json.loads(process.stdout)
        except ValueError as exc:
            raise ExecutionContextError("Malformed container response") from exc

    def git(self, repository: pathlib.Path, arguments: tuple[str, ...]) -> subprocess.CompletedProcess:
        result = self.request(repository, "git", arguments)
        return subprocess.CompletedProcess(["podman", "exec", "git", *arguments],
                                           result["returncode"], result["stdout"], result["stderr"])


if __name__ == "__main__":
    raise SystemExit(main())
