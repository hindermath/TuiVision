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


PROTECTED_WORKSPACE_ROOTS = {
    "SecureCaseTrackerProjects",
    "SecureOrderDeskProjects",
    "SecureServiceHarvesterProjects",
}

OWNER_APPROVAL_TRUST_STATE_PENDING = "PendingOwnerApproval"
OWNER_APPROVAL_TRUST_STATE_ACTIVE = "OwnerApproved"
EXECUTION_CONTRACT_PATH = "scripts/config/maintenance-execution-contexts.json"


def relative_path(value: str) -> pathlib.PurePosixPath:
    if not isinstance(value, str) or not value or "\\" in value or ":" in value or any(ord(c) < 32 for c in value):
        raise ExecutionContextError("Invalid relative path")
    path = pathlib.PurePosixPath(value)
    if path.is_absolute() or any(part in ("", ".", "..") for part in value.split("/")):
        raise ExecutionContextError("Unsafe relative path")
    return path


def validate_contract(data: dict, today: datetime.date | None = None, *, check_expiry: bool = True) -> dict:
    if isinstance(data, dict) and type(data.get("schemaVersion")) is int and data["schemaVersion"] == 2:
        raise ExecutionContextError("Schema 2 muss explizit auf Schema 3 migriert werden / Schema 2 requires explicit migration to schema 3; no automatic approval")
    expected = {
        "schemaVersion", "container", "user", "source", "approvalPath",
        "approvalSha256", "approvalBindingSha256", "approvedImage",
        "approvedSourceSha256", "approvalTrustState", "expiresOn", "mounts",
    }
    if not isinstance(data, dict) or set(data) != expected or type(data["schemaVersion"]) is not int or data["schemaVersion"] != 3:
        raise ExecutionContextError("Invalid execution context contract")
    for key in ("container", "user"):
        if not isinstance(data[key], str) or not re.fullmatch(r"[a-zA-Z0-9][a-zA-Z0-9_.-]*", data[key]):
            raise ExecutionContextError(f"Invalid {key}")
    relative_path(data["approvalPath"])
    for key in ("approvalSha256", "approvalBindingSha256", "approvedSourceSha256"):
        if not isinstance(data[key], str) or not re.fullmatch(r"[0-9a-f]{64}", data[key]):
            raise ExecutionContextError(f"Invalid {key}")
    if data["approvalTrustState"] not in (
        OWNER_APPROVAL_TRUST_STATE_PENDING, OWNER_APPROVAL_TRUST_STATE_ACTIVE,
    ):
        raise ExecutionContextError("Invalid owner approval trust state")
    if not isinstance(data["approvedImage"], str) or not re.fullmatch(r"sha256:[0-9a-f]{64}", data["approvedImage"]):
        raise ExecutionContextError("Invalid approved image")
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
    if data["approvalBindingSha256"] != approval_binding_sha256(data):
        raise ExecutionContextError("Owner approval binding does not match the execution contract")
    return data


def approval_binding_sha256(data: dict) -> str:
    """Detect drift; owner identity is established by decision/review, not hashing."""
    bound = {
        key: data[key]
        for key in (
            "container", "user", "source", "approvalPath", "approvalSha256",
            "approvedImage", "approvedSourceSha256", "approvalTrustState",
            "expiresOn", "mounts",
        )
    }
    return hashlib.sha256(
        json.dumps(bound, ensure_ascii=False, sort_keys=True, separators=(",", ":")).encode("utf-8")
    ).hexdigest()


def verify_owner_approval(
    data: dict, approval: pathlib.Path, observed_image: str, source_sha256: str,
) -> str:
    """Check documented approval and exact scope, without claiming authentication."""
    validate_contract(data)
    if data["approvalTrustState"] != OWNER_APPROVAL_TRUST_STATE_ACTIVE:
        raise ExecutionContextError("Owner-Freigabe fehlt / Explicit documented owner approval is pending")
    if approval.resolve() != approval or not approval.is_file():
        raise ExecutionContextError("Owner approval evidence missing or symlinked")
    approval_sha256 = hashlib.sha256(approval.read_bytes()).hexdigest()
    if approval_sha256 != data["approvalSha256"]:
        raise ExecutionContextError("Owner approval evidence digest does not match the bounded contract")
    normalized_image = observed_image
    if re.fullmatch(r"[0-9a-f]{64}", normalized_image):
        normalized_image = "sha256:" + normalized_image
    if normalized_image != data["approvedImage"]:
        raise ExecutionContextError("Sandbox image is not covered by the owner approval")
    if source_sha256 != data["approvedSourceSha256"]:
        raise ExecutionContextError("Paketdrift / Source package is not covered by the owner approval")
    return approval_sha256


# This small, fixed program runs *inside* Podman. Arguments are JSON, never
# shell fragments. Resolve every path component there before touching Git.
REMOTE_PROGRAM = r'''
import json, pathlib, re, subprocess, sys
p = json.load(sys.stdin)
root = pathlib.Path(p["root"])
repo = pathlib.Path(p["repository"])
if not root.is_absolute() or root.resolve() != root or repo.resolve() != repo:
    raise SystemExit("Symlink boundary rejected")
repo.relative_to(root)
if any(char in str(repo) for char in ("*", "\n", "\r")):
    raise SystemExit("Wildcard or control character in Git target")
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
    # Reset inherited trust (including '*') and trust only this validated repo.
    # Command-scope configuration survives recreation without writing .gitconfig.
    result = subprocess.run(["git", "-c", "core.hooksPath=/dev/null", "-c", "core.fsmonitor=false",
                             "-c", "submodule.recurse=false", "-c", "protocol.allow=never",
                             "-c", "credential.helper=", "-c", "safe.directory=", "-c", "safe.directory=" + str(repo),
                             "-C", str(repo), *args], capture_output=True, text=True)
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

BUNDLE_PROGRAM = r'''
import hashlib, json, pathlib, sys
p = json.loads(sys.stdin.buffer.readline(1048576))
root, repository = pathlib.Path(p["root"]), pathlib.Path(p["repository"])
if root.resolve() != root or repository.resolve() != repository:
    raise SystemExit("Unsafe repository path")
repository.relative_to(root)
source = pathlib.Path(p["source"])
if source.resolve() != source:
    raise SystemExit("Unsafe source path")
for relative, expected in p["bindings"].items():
    path = source / relative
    if path.resolve() != path or hashlib.sha256(path.read_bytes().replace(b"\r\n", b"\n")).hexdigest() != expected:
        raise SystemExit("Bundle worker source binding mismatch")
sys.path.insert(0, str(source / "scripts/lib"))
from maintenance_git_bundle import receive
print(json.dumps(receive(p, sys.stdin.buffer)))
'''


SOURCE_PROGRAM = r'''
import hashlib, json, os, pathlib, shutil, sys
p = json.load(sys.stdin)
source = pathlib.Path(p["source"])
if source.resolve() != source:
    raise SystemExit("Unsafe reference source path")
if not source.is_dir():
    raise SystemExit("Missing reference source")
if os.access(source, os.W_OK):
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
    """Bind declared members and declaration; approval metadata is checked separately."""
    manifest = "scripts/config/agentic-toolchain-maintenance-files.json"
    package = json.loads((source / manifest).read_text(encoding="utf-8"))
    paths = {manifest, *(entry["path"] for entry in package["files"])}
    paths.update({"scripts/lib/maintenance_execution_context.py",
                  "scripts/lib/maintenance_git_bundle.py",
                  "scripts/config/agentic-workspace-fleet.json",
                  "scripts/lib/maintenance_container_worker.py",
                  "scripts/lib/secure-development-hardening.sh",
                  "scripts/register-level2-repository.sh",
                  "scripts/propagate-agentic-toolchain-maintenance.sh",
                  "scripts/config/maintenance-execution-contexts.json"})
    bindings = {}
    # Including the approval's own package digest would create a hash cycle.
    # This is the only exclusion, even if the manifest declares the contract.
    paths.discard(EXECUTION_CONTRACT_PATH)
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


def source_binding_sha256(bindings: dict) -> str:
    return hashlib.sha256(json.dumps(bindings, sort_keys=True, separators=(",", ":")).encode("utf-8")).hexdigest()


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
    if source_binding_sha256(bindings) != evidence["sourceSha256"]:
        raise ExecutionContextError("Source package changed after phase preflight")
    router.inspect()
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


def mount_source_matches(source: str, expected: pathlib.PurePath) -> bool:
    """Accept only the exact host path or its Windows Podman /mnt/<drive> spelling."""
    if not isinstance(source, str) or not source or any(ord(char) < 32 for char in source):
        return False
    if sys.platform == "win32":
        expected = pathlib.PureWindowsPath(expected)
        if not expected.is_absolute() or ".." in expected.parts:
            return False
        if source.startswith("/"):
            # Podman's Linux VM reports the Windows bind source via its drive
            # mount. Do not suffix-match, case-fold Linux paths or accept UNC,
            # traversal, alternate mount roots or sibling workspace prefixes.
            if not re.fullmatch(r"[A-Za-z]:", expected.drive):
                return False
            return source == "/mnt/" + expected.drive[0].lower() + expected.as_posix()[2:]
        observed = pathlib.PureWindowsPath(source)
        return observed.is_absolute() and ".." not in observed.parts and observed == expected
    return pathlib.Path(source).absolute() == expected


class ExecutionContexts:
    def __init__(self, contract: pathlib.Path, home: pathlib.Path, targets: list[dict]):
        self.data = validate_contract(json.loads(contract.read_text(encoding="utf-8")))
        self.home = home.absolute()
        active_targets = {item["path"] for item in targets if item.get("active")}
        self.targets = {
            item["path"]
            for item in targets
            if item.get("active") and item.get("kind") == "git-repository"
        }
        required_roots = {
            pathlib.PurePosixPath(path).parts[0]
            for path in active_targets
            if pathlib.PurePosixPath(path).parts
            and pathlib.PurePosixPath(path).parts[0] in PROTECTED_WORKSPACE_ROOTS
        }
        mounted_roots = {item["host"] for item in self.data["mounts"]}
        missing_roots = required_roots - mounted_roots
        if missing_roots:
            raise ExecutionContextError(
                "Protected workspace roots missing from sandbox contract: "
                + ", ".join(sorted(missing_roots))
            )
        self.container_id = None
        self.image = None
        self.bundle_snapshots = {}
        self.bundle_mode = None

    def prepare_bundles(self, targets: list[dict], run_id: str, mode: str):
        """Download every snapshot before importing; never open host target repos."""
        from maintenance_git_bundle import download, transport_workspace
        if mode not in ("dry-run", "check-only", "update"):
            raise ExecutionContextError("Invalid bundle mode")
        self.bundle_snapshots = {}
        self.bundle_mode = None
        self.preflight()
        selected = [target for target in targets if target.get("active") and
                    target.get("kind") == "git-repository" and self.mapping(self.home / target["path"])]
        canonical_file = pathlib.Path(__file__).resolve().parents[2] / "scripts/config/agentic-workspace-fleet.json"
        canonical = {item["path"]: item for item in json.loads(canonical_file.read_text())["targets"]
                     if item.get("active") and item.get("kind") == "git-repository"
                     and pathlib.PurePosixPath(item["path"]).parts[0] in PROTECTED_WORKSPACE_ROOTS}
        if len(canonical) != 21 or {item["path"] for item in selected} != set(canonical):
            raise ExecutionContextError("Bundle transport requires the complete canonical 21-target fleet")
        for target in selected:
            if any(target.get(key) != canonical[target["path"]].get(key) for key in ("id", "remote", "defaultBranch")):
                raise ExecutionContextError("Bundle target does not match the immutable canonical fleet")
        pending = []
        # A failed download leaves every target's refs and worktree unchanged.
        with transport_workspace() as temporary:
            for target in selected:
                workspace = pathlib.Path(temporary) / target["id"]
                workspace.mkdir(mode=0o700)
                bundle, snapshot = download(target, run_id, workspace)
                pending.append((target, bundle, snapshot))
                if sum(entry[1].stat().st_size for entry in pending) > 2 * 1024 * 1024 * 1024:
                    raise ExecutionContextError("Bundle run exceeds 2 GiB limit")
            evidence = self.preflight()
            bindings = source_bindings(pathlib.Path(__file__).resolve().parents[2])
            if source_binding_sha256(bindings) != evidence["sourceSha256"]:
                raise ExecutionContextError("Bundle source changed after preflight")
            for target, bundle, snapshot in pending:
                self.inspect()
                root, repository = self.mapping(self.home / target["path"])
                payload = {"source": self.data["source"], "bindings": bindings,
                           "root": root, "repository": repository, "target": target,
                           "snapshot": snapshot, "runId": run_id, "dryRun": mode == "dry-run"}
                body = json.dumps(payload).encode("utf-8") + b"\n" + bundle.read_bytes()
                response = subprocess.run(["podman", "exec", "-i", self.container_id, "python3", "-c", BUNDLE_PROGRAM],
                                          input=body, capture_output=True, timeout=300)
                if response.returncode:
                    raise ExecutionContextError("Sandbox bundle verification/import failed for " + target["id"])
                result = json.loads(response.stdout)
                if result.get("status") != ("Verified" if mode == "dry-run" else "Imported"):
                    raise ExecutionContextError("Unexpected bundle worker response")
                self.bundle_snapshots[target["path"]] = snapshot
        self.bundle_mode = mode

    def bundle_snapshot(self, repository: pathlib.Path) -> dict | None:
        return self.bundle_snapshots.get(repository.relative_to(self.home).as_posix())

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
        validate_contract(self.data)
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
        if self.data["user"] in ("root", "0") or self.data["user"].split(":", 1)[0] in ("root", "0"):
            raise ExecutionContextError("Sandbox user must be non-root")
        host_config = info.get("HostConfig", {})
        options = host_config.get("SecurityOpt") or []
        dropped = set(host_config.get("CapDrop") or [])
        # Accept ALL or the complete Podman default capability drop set, but
        # never additional capabilities or a privileged execution context.
        default_caps = {"CAP_CHOWN", "CAP_DAC_OVERRIDE", "CAP_FOWNER", "CAP_FSETID",
                        "CAP_KILL", "CAP_NET_BIND_SERVICE", "CAP_SETFCAP", "CAP_SETGID",
                        "CAP_SETPCAP", "CAP_SETUID", "CAP_SYS_CHROOT"}
        if ("no-new-privileges" not in options or host_config.get("Privileged")
                or host_config.get("CapAdd") or not ("ALL" in dropped or default_caps <= dropped)):
            raise ExecutionContextError("Sandbox requires no-new-privileges and bounded dropped capabilities")
        if self.container_id and (info["Id"] != self.container_id or info["Image"] != self.image):
            raise ExecutionContextError("Sandbox identity changed during run")
        for mount in info.get("Mounts", []):
            destination = mount.get("Destination", "")
            if destination == self.data["source"] or self.data["source"].startswith(destination.rstrip("/") + "/"):
                if mount.get("RW"):
                    raise ExecutionContextError("Reference source mount must be read-only")
        for mapping in self.data["mounts"]:
            matches = [m for m in info.get("Mounts", []) if m.get("Destination") == mapping["container"]]
            if len(matches) != 1 or matches[0].get("Type") != "bind" or not matches[0].get("RW"):
                raise ExecutionContextError("Missing writable workspace mount")
            if not mount_source_matches(matches[0]["Source"], self.home / mapping["host"]):
                raise ExecutionContextError("Workspace mount source mismatch")
        return info

    def preflight(self, source: pathlib.Path | None = None) -> dict:
        validate_contract(self.data)
        # Pending remains pending even when every technical test succeeds.
        if self.data["approvalTrustState"] != OWNER_APPROVAL_TRUST_STATE_ACTIVE:
            raise ExecutionContextError("Owner-Freigabe fehlt / Explicit documented owner approval is pending")
        approval = self.home.joinpath(*relative_path(self.data["approvalPath"]).parts)
        info = self.inspect()
        self.container_id, self.image = info["Id"], info["Image"]
        source_root = (source or pathlib.Path(__file__).resolve().parents[2]).resolve()
        try:
            revision = subprocess.run(
                ["git", "-C", str(source_root), "rev-parse", "HEAD"],
                capture_output=True, text=True, timeout=10, check=True,
            ).stdout.strip()
        except (OSError, subprocess.SubprocessError) as exc:
            raise ExecutionContextError("Approved source revision cannot be verified") from exc
        bindings = source_bindings(source_root)
        package_sha256 = source_binding_sha256(bindings)
        approval_sha256 = verify_owner_approval(
            self.data, approval, str(info.get("Image") or ""), package_sha256
        )
        result = subprocess.run(
            ["podman", "exec", "-i", self.container_id, "python3", "-c", SOURCE_PROGRAM],
            input=json.dumps({"source": self.data["source"], "bindings": bindings}),
            text=True, capture_output=True, timeout=60)
        if result.returncode:
            detail = result.stderr.strip()
            known = ("Missing reference source", "Unsafe reference source path",
                     "Canonical source must be immutable to the sandbox user", "Missing maintenance tool",
                     "Missing or writable source:", "Source binding mismatch:")
            reason = next((message for message in known if detail.startswith(message)), "Reference source unavailable or invalid")
            raise ExecutionContextError("Sandbox reference preflight blocked: " + reason)
        return {"context": "container", "containerId": self.container_id, "image": self.image,
                "expiresOn": self.data["expiresOn"], "approvalPath": self.data["approvalPath"],
                "approvalSha256": approval_sha256,
                "approvalBindingSha256": self.data["approvalBindingSha256"],
                "sourceRevision": revision,
                "sourceSha256": package_sha256}

    def request(self, repository: pathlib.Path, action: str, arguments: tuple[str, ...] = ()) -> dict:
        mapping = self.mapping(repository)
        if mapping is None:
            raise ExecutionContextError("Target is not delegated")
        if self.container_id is None:
            raise ExecutionContextError("Sandbox preflight required")
        validate_contract(self.data)
        if self.data["approvalTrustState"] != OWNER_APPROVAL_TRUST_STATE_ACTIVE:
            raise ExecutionContextError("Owner approval is no longer active")
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
