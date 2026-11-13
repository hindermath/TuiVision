"""Credential-free bundle import; authenticated host transport never opens target checkouts.

DE: Nur kanonische GitHub-Adressen; keine Ziel-Konfiguration auf dem Host.
EN: Only canonical GitHub addresses; no target configuration on the host.
"""
from __future__ import annotations

import datetime
import hashlib
import json
import os
from pathlib import Path
import re
import shutil
import subprocess
import tempfile
import uuid
from contextlib import contextmanager


class BundleError(ValueError):
    pass


@contextmanager
def transport_workspace():
    """One private run directory; keep failed evidence, clean successful data."""
    workspace = Path(tempfile.mkdtemp(prefix="hb-host-bundles-")).resolve()
    os.chmod(workspace, 0o700)
    try:
        yield workspace
    except Exception as exc:
        raise BundleError("Bundle transport blocked; private diagnostic artifacts retained at " + str(workspace)) from exc
    else:
        shutil.rmtree(workspace)


def canonical_url(target):
    # Derive the allowlist from the reviewed fleet, not from remote.get-url.
    url = target.get("remote", "")
    if not re.fullmatch(r"https://github\.com/hindermath/[A-Za-z0-9][A-Za-z0-9_.-]*\.git", url):
        raise BundleError("Transport requires a canonical hindermath HTTPS URL")
    if not re.fullmatch(r"[a-z0-9][a-z0-9-]*", target.get("id", "")):
        raise BundleError("Invalid target ID")
    branch = target.get("defaultBranch", "")
    if not valid_ref("refs/heads/" + branch):
        raise BundleError("Invalid default branch")
    return url


def valid_ref(ref):
    return (isinstance(ref, str) and ref.startswith("refs/heads/")
            and bool(re.fullmatch(r"refs/heads/[A-Za-z0-9_][A-Za-z0-9_./-]*", ref))
            and not any(s in ref for s in ("..", "//", "@{"))
            and not any(p.startswith(".") or p.endswith((".", ".lock")) for p in ref.split("/"))
            and not ref.endswith("/"))


def isolated_env():
    # Remove Git injection, tokens and agent credentials. The gh helper reads
    # the host's existing credential store; no secret enters an argv or bundle.
    env = {key: value for key, value in os.environ.items()
           if not key.startswith(("GIT_", "GH_", "GITHUB_", "SSH_"))
           and key not in ("LD_PRELOAD", "LD_LIBRARY_PATH", "DYLD_INSERT_LIBRARIES", "DYLD_LIBRARY_PATH")}
    env.update(GIT_CONFIG_NOSYSTEM="1", GIT_CONFIG_GLOBAL=os.devnull,
               GIT_CONFIG_SYSTEM=os.devnull, GIT_TERMINAL_PROMPT="0", GIT_OPTIONAL_LOCKS="0")
    return env


def git_command(repository, arguments, *, network=False, input=None):
    git = shutil.which("git")
    if not git:
        raise BundleError("Git unavailable")
    command = [git, "-c", "core.fsmonitor=false", "-c", "core.hooksPath=" + os.devnull,
               "-c", "credential.helper=", "-c", "protocol.allow=never",
               "-c", "submodule.recurse=false", "-c", "fetch.recurseSubmodules=false",
               "-c", "gc.auto=0", "-c", "maintenance.auto=false"]
    if network:
        gh = shutil.which("gh")
        if not gh:
            raise BundleError("Host gh credential helper unavailable")
        # Fixed executable path, quoted for Git's helper shell; no user input.
        import shlex
        command += ["-c", "protocol.https.allow=always", "-c", "http.followRedirects=false",
                    "-c", "credential.helper=!" + shlex.quote(gh) + " auth git-credential"]
    if repository is not None:
        command += ["-c", "safe.directory=", "-c", "safe.directory=" + str(repository), "-C", str(repository)]
    # Git's transaction protocol is LF-delimited on Windows too. Text pipes
    # would translate LF to CRLF and corrupt commands/OIDs before update-ref.
    result = subprocess.run([*command, *arguments], input=input.encode("utf-8") if input is not None else None, text=False,
                            capture_output=True, env=isolated_env(), timeout=300,
                            cwd=str(repository) if repository is not None else tempfile.gettempdir())
    if result.returncode:
        # Do not echo raw provider stderr, URLs, helper output or credentials.
        raise BundleError("Isolated Git operation failed: " + arguments[0])
    return result.stdout.decode("utf-8") if isinstance(result.stdout, bytes) else result.stdout


def parse_refs(text):
    refs = {}
    for line in text.splitlines():
        fields = line.split()
        if len(fields) != 2 or not re.fullmatch(r"[0-9a-f]{40}", fields[0]) or not valid_ref(fields[1]):
            raise BundleError("Unexpected bundle ref")
        if fields[1] in refs:
            raise BundleError("Duplicate bundle ref")
        refs[fields[1]] = fields[0]
    if not refs:
        raise BundleError("Empty branch snapshot")
    return refs


def validate_snapshot(snapshot, target, run_id):
    expected = {"schemaVersion", "runId", "targetId", "url", "defaultBranch", "refs", "createdAt", "bundleSha256"}
    if not isinstance(snapshot, dict) or set(snapshot) != expected or type(snapshot["schemaVersion"]) is not int or snapshot["schemaVersion"] != 1:
        raise BundleError("Invalid bundle snapshot")
    if (snapshot["runId"] != run_id or str(uuid.UUID(run_id)) != run_id
            or snapshot["targetId"] != target["id"] or snapshot["url"] != canonical_url(target)
            or snapshot["defaultBranch"] != target["defaultBranch"]):
        raise BundleError("Bundle target or run mismatch")
    refs = snapshot["refs"]
    if not isinstance(refs, dict) or parse_refs("\n".join(f"{oid} {ref}" for ref, oid in refs.items())) != refs:
        raise BundleError("Invalid snapshot refs")
    if "refs/heads/" + target["defaultBranch"] not in refs:
        raise BundleError("Default branch missing from snapshot")
    if not re.fullmatch(r"[0-9a-f]{64}", snapshot["bundleSha256"]):
        raise BundleError("Invalid bundle hash")
    try:
        stamp = datetime.datetime.fromisoformat(snapshot["createdAt"].replace("Z", "+00:00"))
        age = datetime.datetime.now(datetime.timezone.utc) - stamp
    except (ValueError, TypeError):
        raise BundleError("Invalid snapshot timestamp") from None
    if stamp.utcoffset() != datetime.timedelta(0) or not datetime.timedelta(minutes=-1) <= age <= datetime.timedelta(hours=1):
        raise BundleError("Snapshot is stale or from the future")


def download(target, run_id, workspace):
    """Host only: brand-new bare repo, explicit remote, never a target path."""
    url = canonical_url(target)
    workspace = Path(workspace)
    bare = workspace / "objects.git"
    if bare.exists() or bare.is_symlink():
        raise BundleError("Transport workspace is not fresh")
    git_command(None, ["init", "--bare", "--template=", str(bare)])
    git_command(bare, ["fetch", "--no-tags", "--no-recurse-submodules", url,
                       "+refs/heads/*:refs/heads/*"], network=True)
    refs = parse_refs(git_command(bare, ["for-each-ref", "--format=%(objectname) %(refname)", "refs/heads/"]))
    # Detect a moving remote before exporting the artifact; do not mix snapshots.
    current = parse_refs(git_command(bare, ["ls-remote", "--heads", url], network=True))
    if refs != current:
        raise BundleError("Remote changed while downloading snapshot")
    head = git_command(bare, ["ls-remote", "--symref", url, "HEAD"], network=True).splitlines()
    expected_ref = "refs/heads/" + target["defaultBranch"]
    if set(head) != {f"ref: {expected_ref}\tHEAD", f"{refs.get(expected_ref)}\tHEAD"}:
        raise BundleError("Remote default branch changed or is ambiguous")
    bundle = workspace / "snapshot.bundle"
    git_command(bare, ["bundle", "create", str(bundle), *sorted(refs)])
    os.chmod(bundle, 0o600)
    if bundle.stat().st_size > 1024 * 1024 * 1024:
        raise BundleError("Bundle exceeds 1 GiB limit")
    snapshot = {"schemaVersion": 1, "runId": run_id, "targetId": target["id"], "url": url,
                "defaultBranch": target["defaultBranch"], "refs": refs,
                "createdAt": datetime.datetime.now(datetime.timezone.utc).isoformat(),
                "bundleSha256": hashlib.sha256(bundle.read_bytes()).hexdigest()}
    validate_snapshot(snapshot, target, run_id)
    return bundle, snapshot


def import_bundle(repository, bundle, snapshot, target, run_id, *, dry_run=False):
    """Sandbox only: unbundle objects, then atomically update tracking refs only."""
    validate_snapshot(snapshot, target, run_id)
    repository, bundle = Path(repository), Path(bundle)
    if repository.resolve() != repository or not (repository / ".git").is_dir() or (repository / ".git").is_symlink():
        raise BundleError("Unsafe target Git directory")
    if git_command(repository, ["remote", "get-url", "origin"]).strip() != canonical_url(target):
        raise BundleError("Target origin does not match canonical remote")
    for name in ("objects", "refs", "refs/remotes", "refs/remotes/origin"):
        path = repository / ".git" / name
        if path.resolve() != path:
            raise BundleError("Linked Git metadata directory is not allowed")
    if bundle.resolve() != bundle or not bundle.is_file() or bundle.is_symlink():
        raise BundleError("Unsafe bundle file")
    if hashlib.sha256(bundle.read_bytes()).hexdigest() != snapshot["bundleSha256"]:
        raise BundleError("Bundle digest mismatch")
    with bundle.open("rb") as stream:
        if stream.readline() != b"# v2 git bundle\n":
            raise BundleError("Only complete version 2 bundles are accepted")
        for line in stream:
            if line == b"\n":
                break
            if line.startswith(b"-"):
                raise BundleError("Bundle prerequisites are not allowed")
    refs = parse_refs(git_command(repository, ["bundle", "list-heads", str(bundle)]))
    if refs != snapshot["refs"]:
        raise BundleError("Bundle refs do not match snapshot")
    git_command(repository, ["bundle", "verify", str(bundle)])
    if dry_run:
        return
    git_command(repository, ["bundle", "unbundle", str(bundle)])
    for oid in set(refs.values()):
        if git_command(repository, ["cat-file", "-t", oid]).strip() != "commit":
            raise BundleError("Branch head is not a commit")
    old = {}
    for line in git_command(repository, ["for-each-ref", "--format=%(refname) %(objectname) %(symref)", "refs/remotes/origin/"]).splitlines():
        fields = line.split()
        if len(fields) == 2:
            old[fields[0]] = fields[1]
        elif fields[0] != "refs/remotes/origin/HEAD":
            raise BundleError("Unexpected symbolic tracking ref")
    new = {ref.replace("refs/heads/", "refs/remotes/origin/", 1): oid for ref, oid in refs.items()}
    transaction = ["start"]
    for ref, oid in sorted(new.items()):
        transaction.append(f"update {ref} {oid} {old[ref]}" if ref in old else f"create {ref} {oid}")
    for ref in sorted(old.keys() - new.keys()):
        if ref != "refs/remotes/origin/HEAD":
            transaction.append(f"delete {ref} {old[ref]}")
    transaction += ["prepare", "commit"]
    git_command(repository, ["update-ref", "--no-deref", "--stdin"], input="\n".join(transaction) + "\n")


def receive(payload, stream):
    """The input contains only a bounded snapshot and raw bundle, never credentials."""
    with tempfile.TemporaryDirectory(prefix="hb-bundle-") as directory:
        os.chmod(directory, 0o700)
        bundle = Path(directory) / "snapshot.bundle"
        total = 0
        with bundle.open("xb") as output:
            os.chmod(bundle, 0o600)
            while chunk := stream.read(1024 * 1024):
                total += len(chunk)
                if total > 1024 * 1024 * 1024:
                    raise BundleError("Bundle exceeds 1 GiB limit")
                output.write(chunk)
        import_bundle(payload["repository"], bundle, payload["snapshot"], payload["target"],
                      payload["runId"], dry_run=payload["dryRun"])
    return {"status": "Verified" if payload["dryRun"] else "Imported"}
