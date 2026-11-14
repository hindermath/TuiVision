"""Bundle security/round-trip fixtures: local temporary Git only, no provider access."""
import copy
import datetime
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import subprocess
import tempfile
import unittest
from unittest.mock import patch
import uuid

MODULE = Path(__file__).resolve().parents[1] / "lib/maintenance_git_bundle.py"
spec = importlib.util.spec_from_file_location("maintenance_git_bundle", MODULE)
bundle = importlib.util.module_from_spec(spec)
spec.loader.exec_module(bundle)


class BundleTests(unittest.TestCase):
    def test_git_transaction_stdin_is_utf8_bytes_without_crlf_translation(self):
        with patch.object(bundle.shutil, "which", return_value="/trusted/git"), \
                patch.object(bundle.subprocess, "run", return_value=subprocess.CompletedProcess([], 0, b"commit: ok\n", b"")) as run:
            output = bundle.git_command(Path("/fresh/repo"), ["update-ref", "--stdin"], input="start\ncommit\n")
            self.assertEqual(run.call_args.kwargs["input"], b"start\ncommit\n")
            self.assertFalse(run.call_args.kwargs["text"])
            self.assertEqual(output, "commit: ok\n")

    def test_remote_mismatch_and_auth_failure_do_not_leak_or_mutate(self):
        with tempfile.TemporaryDirectory() as tmp:
            target, artifact, snapshot, info, run, _ = self.fixture(Path(tmp).resolve())
            before = self.git(target, "show-ref")
            self.git(target, "remote", "set-url", "origin", "https://github.com/hindermath/other.git")
            with self.assertRaisesRegex(bundle.BundleError, "origin"):
                bundle.import_bundle(target, artifact, snapshot, info, run)
            self.assertEqual(before, self.git(target, "show-ref"))
        with patch.object(bundle.shutil, "which", side_effect=lambda name: "/trusted/" + name), \
                patch.object(bundle.subprocess, "run", return_value=subprocess.CompletedProcess([], 1, "secret-token", "secret-token")):
            with self.assertRaises(bundle.BundleError) as failure:
                bundle.git_command(Path("/fresh/bare.git"), ["fetch", "https://github.com/hindermath/fixture.git"], network=True)
            self.assertNotIn("secret-token", str(failure.exception))

    def test_delegated_classifier_preserves_dirty_feature_and_diverged_targets(self):
        fleet_spec = importlib.util.spec_from_file_location("bundle_test_fleet", MODULE.with_name("agentic_workspace_fleet.py"))
        fleet = importlib.util.module_from_spec(fleet_spec)
        fleet_spec.loader.exec_module(fleet)
        with tempfile.TemporaryDirectory() as tmp:
            root = Path(tmp).resolve()
            target, artifact, snapshot, info, run, old = self.fixture(root)
            self.git(target, "remote", "set-url", "origin", info["remote"])
            bundle.import_bundle(target, artifact, snapshot, info, run)
            descriptor = {**info, "path": "Projects/repo", "kind": "git-repository", "maintenanceClass": "canonical-fleet"}
            class Router:
                bundle_mode = "update"
                def mapping(self, path):
                    return (str(root), str(target))
                def bundle_snapshot(self, path):
                    return snapshot
                def request(self, path, action):
                    if action != "probe":
                        raise AssertionError(action)
                    return {"exists": True, "directory": True, "git": True}
                def git(self, path, arguments):
                    try:
                        output = bundle.git_command(path, list(arguments))
                        return subprocess.CompletedProcess([], 0, output, "")
                    except bundle.BundleError:
                        return subprocess.CompletedProcess([], 1, "", "fixture")
            fleet.EXECUTION_CONTEXTS = Router()
            (target / "local.txt").write_text("keep")
            self.assertEqual(fleet.classify_repository(descriptor, target, "update")["status"], "DIRTY")
            self.assertEqual(self.git(target, "rev-parse", "HEAD"), old)
            (target / "local.txt").unlink()
            self.git(target, "checkout", "-b", "feature")
            self.assertEqual(fleet.classify_repository(descriptor, target, "update")["status"], "BRANCH_MISMATCH")
            self.git(target, "checkout", "main")
            (target / "local.txt").write_text("local commit")
            self.git(target, "add", "local.txt")
            self.git(target, "commit", "-m", "local work")
            local = self.git(target, "rev-parse", "HEAD")
            self.assertEqual(fleet.classify_repository(descriptor, target, "update")["status"], "DIVERGED")
            self.assertEqual(self.git(target, "rev-parse", "HEAD"), local)

    def test_download_uses_fresh_bare_repo_and_detects_remote_change(self):
        with tempfile.TemporaryDirectory() as tmp:
            root = Path(tmp).resolve()
            _, _, _, info, run_id, _ = self.fixture(root)
            original = bundle.git_command
            network_calls = []
            def fixture_command(repo, arguments, *, network=False, input=None):
                if not network:
                    return original(repo, arguments, input=input)
                network_calls.append((repo, arguments))
                args = [str(root / "upstream") if arg == info["remote"] else arg for arg in arguments]
                return self.git(repo, *args) + "\n"
            workspace = root / "transport"
            workspace.mkdir()
            with patch.object(bundle, "git_command", side_effect=fixture_command):
                artifact, snapshot = bundle.download(info, run_id, workspace)
            self.assertTrue(artifact.is_file())
            self.assertTrue(all(repo == workspace / "objects.git" for repo, _ in network_calls))
            self.assertEqual(snapshot["url"], info["remote"])
            self.assertEqual(set(snapshot["refs"]), {"refs/heads/main", "refs/heads/release"})
            self.assertFalse((workspace / "objects.git/README.md").exists())
            second = root / "second"
            second.mkdir()
            def moving(repo, arguments, *, network=False, input=None):
                if network and arguments[:2] == ["ls-remote", "--heads"]:
                    return "a" * 40 + "\trefs/heads/main\n"
                return fixture_command(repo, arguments, network=network, input=input)
            with patch.object(bundle, "git_command", side_effect=moving), self.assertRaisesRegex(bundle.BundleError, "Remote changed"):
                bundle.download(info, run_id, second)

    def test_symbolic_tracking_ref_cannot_overwrite_local_branch(self):
        with tempfile.TemporaryDirectory() as tmp:
            target, artifact, snapshot, info, run, old = self.fixture(Path(tmp).resolve())
            self.git(target, "symbolic-ref", "refs/remotes/origin/main", "refs/heads/main")
            with self.assertRaisesRegex(bundle.BundleError, "symbolic"):
                bundle.import_bundle(target, artifact, snapshot, info, run)
            self.assertEqual(self.git(target, "rev-parse", "HEAD"), old)

    def git(self, root, *args):
        command = ["git", "-c", "core.fsmonitor=false", "-c", "core.hooksPath=" + os.devnull,
                   "-c", "protocol.file.allow=always", "-c", "user.name=Fixture",
                   "-c", "user.email=fixture@example.invalid"]
        return subprocess.run([*command, "-C", str(root), *args], check=True,
                              capture_output=True, text=True, env=bundle.isolated_env()).stdout.strip()

    def fixture(self, root):
        upstream, target = root / "upstream", root / "target"
        upstream.mkdir()
        self.git(upstream, "init", "-b", "main")
        (upstream / "README.md").write_text("base\n")
        self.git(upstream, "add", "README.md")
        self.git(upstream, "commit", "-m", "base")
        self.git(root, "clone", str(upstream), str(target))
        old = self.git(target, "rev-parse", "HEAD")
        self.git(upstream, "branch", "release")
        (upstream / "README.md").write_text("updated\n")
        self.git(upstream, "commit", "-am", "update")
        artifact = root / "snapshot.bundle"
        self.git(upstream, "bundle", "create", str(artifact), "refs/heads/main", "refs/heads/release")
        target_info = {"id": "fixture", "remote": "https://github.com/hindermath/fixture.git", "defaultBranch": "main"}
        self.git(target, "remote", "set-url", "origin", target_info["remote"])
        run_id = str(uuid.uuid4())
        snapshot = {"schemaVersion": 1, "runId": run_id, "targetId": "fixture",
                    "url": target_info["remote"], "defaultBranch": "main",
                    "refs": bundle.parse_refs(self.git(upstream, "bundle", "list-heads", str(artifact))),
                    "createdAt": datetime.datetime.now(datetime.timezone.utc).isoformat(),
                    "bundleSha256": hashlib.sha256(artifact.read_bytes()).hexdigest()}
        return target, artifact, snapshot, target_info, run_id, old

    def test_roundtrip_tracks_heads_prunes_only_tracking_and_fast_forwards(self):
        with tempfile.TemporaryDirectory() as tmp:
            target, artifact, snapshot, info, run, old = self.fixture(Path(tmp).resolve())
            self.git(target, "branch", "keep-local")
            self.git(target, "tag", "keep-tag")
            self.git(target, "update-ref", "refs/remotes/origin/stale", old)
            bundle.import_bundle(target, artifact, snapshot, info, run)
            self.assertEqual(self.git(target, "rev-parse", "HEAD"), old)
            self.assertEqual(self.git(target, "rev-parse", "origin/main"), snapshot["refs"]["refs/heads/main"])
            refs = self.git(target, "for-each-ref", "--format=%(refname)").splitlines()
            self.assertIn("refs/heads/keep-local", refs)
            self.assertIn("refs/tags/keep-tag", refs)
            self.assertIn("refs/remotes/origin/release", refs)
            self.assertNotIn("refs/remotes/origin/stale", refs)
            bundle.git_command(target, ["merge", "--ff-only", snapshot["refs"]["refs/heads/main"]])
            self.assertEqual(self.git(target, "rev-list", "--left-right", "--count", "HEAD...origin/main"), "0\t0")

    def test_preview_leaves_refs_index_and_worktree_unchanged(self):
        with tempfile.TemporaryDirectory() as tmp:
            target, artifact, snapshot, info, run, _ = self.fixture(Path(tmp).resolve())
            before = (self.git(target, "show-ref"), (target / ".git/index").read_bytes(), (target / "README.md").read_bytes())
            bundle.import_bundle(target, artifact, snapshot, info, run, dry_run=True)
            self.assertEqual(before, (self.git(target, "show-ref"), (target / ".git/index").read_bytes(), (target / "README.md").read_bytes()))

    def test_rejects_tampering_wrong_target_refs_run_missing_main_and_stale_time(self):
        with tempfile.TemporaryDirectory() as tmp:
            target, artifact, snapshot, info, run, _ = self.fixture(Path(tmp).resolve())
            cases = [{"targetId": "other"}, {"runId": str(uuid.uuid4())}, {"bundleSha256": "0" * 64},
                     {"refs": {"refs/heads/other": "a" * 40}},
                     {"refs": {**snapshot["refs"], "refs/heads/unexpected": "a" * 40}},
                     {"createdAt": "2000-01-01T00:00:00+00:00"},
                     {"refs": {"refs/heads/../escape": "a" * 40}}]
            before = self.git(target, "show-ref")
            for changes in cases:
                with self.subTest(changes=changes), self.assertRaises((bundle.BundleError, ValueError)):
                    bundle.import_bundle(target, artifact, {**snapshot, **changes}, info, run)
            artifact.write_bytes(artifact.read_bytes() + b"tampered")
            with self.assertRaisesRegex(bundle.BundleError, "digest"):
                bundle.import_bundle(target, artifact, snapshot, info, run)
            self.assertEqual(before, self.git(target, "show-ref"))

    def test_hooks_credentials_and_url_rewrites_cannot_launch_during_import(self):
        with tempfile.TemporaryDirectory() as tmp:
            root = Path(tmp).resolve()
            target, artifact, snapshot, info, run, _ = self.fixture(root)
            marker = root / "executed"
            hook_dir = root / "hooks"
            hook_dir.mkdir()
            for name in ("reference-transaction", "post-merge"):
                hook = hook_dir / name
                hook.write_text("#!/bin/sh\ntouch '" + str(marker) + "'\n")
                hook.chmod(0o755)
            self.git(target, "config", "core.hooksPath", str(hook_dir))
            self.git(target, "config", "credential.helper", "!touch '" + str(marker) + "'")
            self.git(target, "config", "url.ext::touch.insteadOf", "/")
            (target / ".gitmodules").write_text("[submodule \"bad\"]\npath=bad\nurl=ext::touch marker\n")
            bundle.import_bundle(target, artifact, snapshot, info, run)
            self.assertFalse(marker.exists())

    def test_host_environment_and_command_are_isolated(self):
        with patch.dict(os.environ, {"GH_TOKEN": "fixture-token", "GIT_CONFIG_PARAMETERS": "injected",
                                     "GIT_ALTERNATE_OBJECT_DIRECTORIES": "/unsafe", "GIT_SSH_COMMAND": "injected"}), \
                patch.object(bundle.shutil, "which", side_effect=lambda name: "/trusted/" + name), \
                patch.object(bundle.subprocess, "run", return_value=subprocess.CompletedProcess([], 0, "", "")) as run:
            bundle.git_command(Path("/fresh/bare.git"), ["fetch", "https://github.com/hindermath/fixture.git"], network=True)
            command = run.call_args.args[0]
            env = run.call_args.kwargs["env"]
            self.assertNotIn("GH_TOKEN", env)
            self.assertNotIn("GIT_CONFIG_PARAMETERS", env)
            self.assertNotIn("GIT_ALTERNATE_OBJECT_DIRECTORIES", env)
            self.assertEqual(env["GIT_CONFIG_GLOBAL"], os.devnull)
            self.assertIn("protocol.allow=never", command)
            self.assertIn("protocol.https.allow=always", command)
            self.assertIn("credential.helper=!/trusted/gh auth git-credential", command)
            self.assertNotIn("fixture-token", str(command))

    def test_canonical_urls_and_refs_reject_transport_injection(self):
        for url in ("file:///tmp/repo", "https://github.com/other/repo.git", "ext::bad",
                    "https://github.com/hindermath/repo.git?token=secret", "https://evil.invalid/repo.git"):
            with self.subTest(url=url), self.assertRaises(bundle.BundleError):
                bundle.canonical_url({"id": "fixture", "remote": url, "defaultBranch": "main"})
        for ref in ("refs/tags/v1", "refs/heads/-unsafe", "refs/heads/a.lock", "refs/heads/a..b", "refs/heads/a\nb"):
            self.assertFalse(bundle.valid_ref(ref))


if __name__ == "__main__":
    unittest.main()
