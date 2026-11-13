"""Sandbox routing fixtures: no production paths, network or Podman required."""
import datetime
import importlib.util
import json
import os
import sys
from pathlib import Path, PurePosixPath, PureWindowsPath
import subprocess
import tempfile
from types import SimpleNamespace
import unittest
from unittest.mock import patch

MODULE = Path(__file__).resolve().parents[1] / "lib/maintenance_execution_context.py"
# Consumer packages deliberately omit the Level-0 orchestration manifest and
# installers. Keep their unit contracts runnable through ordinary discovery;
# the complete integration suite remains mandatory in the canonical source.
CANONICAL_SOURCE = (MODULE.parents[2] / "scripts/config/agentic-toolchain-maintenance-files.json").is_file()
central_only = unittest.skipUnless(CANONICAL_SOURCE, "Requires canonical Level-0 source; covered by central/image integration tests")
spec = importlib.util.spec_from_file_location("maintenance_execution_context", MODULE)
context = importlib.util.module_from_spec(spec)
spec.loader.exec_module(context)
worker_spec = importlib.util.spec_from_file_location("maintenance_container_worker", MODULE.with_name("maintenance_container_worker.py"))
worker = importlib.util.module_from_spec(worker_spec)
worker_spec.loader.exec_module(worker)
fleet_spec = importlib.util.spec_from_file_location("delegation_test_fleet", MODULE.with_name("agentic_workspace_fleet.py"))
fleet = importlib.util.module_from_spec(fleet_spec)
fleet_spec.loader.exec_module(fleet)


def contract():
    value = {"schemaVersion": 3, "container": "test-sandbox", "user": "tester",
             "source": "/opt/reference", "approvalPath": "sandbox/approval.md",
             "approvalSha256": "a" * 64, "approvalBindingSha256": "0" * 64,
             "approvedImage": "sha256:" + "b" * 64,
             "approvedSourceSha256": "c" * 64,
             "approvalTrustState": "PendingOwnerApproval",
             "expiresOn": "2099-12-31", "mounts": [{"host": "Projects", "container": "/projects"}]}
    value["approvalBindingSha256"] = context.approval_binding_sha256(value)
    return value


class ExecutionContextTests(unittest.TestCase):
    @central_only
    def test_bundle_transport_finishes_all_downloads_before_import_and_closes_on_failure(self):
        targets = json.loads((MODULE.parents[2] / "scripts/config/agentic-workspace-fleet.json").read_text())["targets"]
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            data = json.loads((MODULE.parents[2] / context.EXECUTION_CONTRACT_PATH).read_text())
            config = root / "contract.json"
            config.write_text(json.dumps(data))
            router = context.ExecutionContexts(config, root, targets)
            count = []
            def download(target, run_id, workspace):
                count.append(target["id"])
                artifact = workspace / "snapshot.bundle"
                artifact.write_bytes(b"fixture")
                return artifact, {"targetId": target["id"]}
            def receive(*args, **kwargs):
                self.assertEqual(len(count), 21)
                self.assertNotIn(b"fixture-secret", kwargs["input"])
                return subprocess.CompletedProcess([], 0, b'{"status":"Verified"}', b"")
            transport = SimpleNamespace(download=download, transport_workspace=tempfile.TemporaryDirectory)
            evidence = {"sourceSha256": context.source_binding_sha256(context.source_bindings(MODULE.parents[2]))}
            with patch.dict(sys.modules, {"maintenance_git_bundle": transport}), \
                    patch.dict(os.environ, {"GH_TOKEN": "fixture-secret"}), \
                    patch.object(router, "preflight", return_value=evidence), \
                    patch.object(router, "inspect", return_value={}), \
                    patch.object(context.subprocess, "run", side_effect=receive) as process:
                router.prepare_bundles(targets, "fixture", "dry-run")
                self.assertEqual(process.call_count, 21)
            count.clear()
            def failed(target, run_id, workspace):
                raise ValueError("download failed")
            transport.download = failed
            with patch.dict(sys.modules, {"maintenance_git_bundle": transport}), \
                    patch.object(router, "preflight", return_value=evidence), \
                    patch.object(context.subprocess, "run") as process, self.assertRaises(ValueError):
                router.prepare_bundles(targets, "fixture", "update")
            process.assert_not_called()

    def test_preflight_uses_package_not_git_revision_and_reports_reference_failures(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            approval = root / "sandbox/approval.md"
            approval.parent.mkdir()
            approval.write_text("Explicit owner decision", encoding="utf-8")
            bindings = {"scripts/worker.py": "a" * 64}
            value = {**contract(), "approvalTrustState": "OwnerApproved",
                     "approvalSha256": context.hashlib.sha256(approval.read_bytes()).hexdigest(),
                     "approvedSourceSha256": context.source_binding_sha256(bindings)}
            value["approvalBindingSha256"] = context.approval_binding_sha256(value)
            router = self.make_context(root)
            router.data = value
            observed = {"Id": "container", "Image": value["approvedImage"]}
            for revision in ("1" * 40, "2" * 40):
                with patch.object(router, "inspect", return_value=observed), \
                        patch.object(context, "source_bindings", return_value=bindings), \
                        patch.object(context.subprocess, "run", side_effect=[
                            subprocess.CompletedProcess([], 0, revision, ""),
                            subprocess.CompletedProcess([], 0, "ready", "")]):
                    evidence = router.preflight(root)
                    self.assertEqual(evidence["sourceRevision"], revision)
                    self.assertEqual(evidence["sourceSha256"], value["approvedSourceSha256"])
            for message in ("Missing reference source", "Canonical source must be immutable to the sandbox user",
                            "Missing or writable source: scripts/worker.py", "Source binding mismatch: scripts/worker.py"):
                with patch.object(router, "inspect", return_value=observed), \
                        patch.object(context, "source_bindings", return_value=bindings), \
                        patch.object(context.subprocess, "run", side_effect=[
                            subprocess.CompletedProcess([], 0, "1" * 40, ""),
                            subprocess.CompletedProcess([], 1, "", message)]), \
                        self.assertRaisesRegex(context.ExecutionContextError, "reference preflight blocked"):
                    router.preflight(root)

    def test_schema_two_requires_explicit_migration(self):
        with self.assertRaisesRegex(context.ExecutionContextError, "explicit migration"):
            context.validate_contract({"schemaVersion": 2})

    def test_expiry_includes_last_day_of_2026_only(self):
        value = {**contract(), "expiresOn": "2026-12-31"}
        value["approvalBindingSha256"] = context.approval_binding_sha256(value)
        context.validate_contract(value, today=datetime.date(2026, 12, 31))
        with self.assertRaisesRegex(context.ExecutionContextError, "expired"):
            context.validate_contract(value, today=datetime.date(2027, 1, 1))

    def test_approval_rejects_document_image_and_package_drift(self):
        with tempfile.TemporaryDirectory() as directory:
            approval = Path(directory).resolve() / "approval.md"
            approval.write_text("Owner decision", encoding="utf-8")
            value = {**contract(), "approvalTrustState": "OwnerApproved",
                     "approvalSha256": context.hashlib.sha256(approval.read_bytes()).hexdigest()}
            value["approvalBindingSha256"] = context.approval_binding_sha256(value)
            for image, package in (("sha256:" + "d" * 64, value["approvedSourceSha256"]),
                                   (value["approvedImage"], "d" * 64)):
                with self.assertRaises(context.ExecutionContextError):
                    context.verify_owner_approval(value, approval, image, package)
            approval.write_text("Changed decision", encoding="utf-8")
            with self.assertRaisesRegex(context.ExecutionContextError, "digest"):
                context.verify_owner_approval(value, approval, value["approvedImage"], value["approvedSourceSha256"])
            approval.unlink()
            with self.assertRaisesRegex(context.ExecutionContextError, "missing"):
                context.verify_owner_approval(value, approval, value["approvedImage"], value["approvedSourceSha256"])

    @central_only
    def test_package_fingerprint_excludes_only_contract_not_manifest_or_code(self):
        source = MODULE.parents[2]
        bindings = context.source_bindings(source)
        baseline = context.source_binding_sha256(bindings)
        self.assertEqual(baseline, context.source_binding_sha256(dict(reversed(list(bindings.items())))))
        # Model documentation/commit changes without modifying the real source.
        original = Path.read_bytes
        def changed(path):
            if path == source / context.EXECUTION_CONTRACT_PATH or path == source / "README.md":
                return b"documentation or approval changed"
            return original(path)
        with patch.object(Path, "read_bytes", changed):
            self.assertEqual(baseline, context.source_binding_sha256(context.source_bindings(source)))
        for relative in ("scripts/config/agentic-toolchain-maintenance-files.json",
                         "scripts/lib/maintenance_container_worker.py", "scripts/lib/maintenance_execution_context.py"):
            altered = {**bindings, relative: "d" * 64}
            self.assertNotEqual(baseline, context.source_binding_sha256(altered))

    def test_documented_owner_approval_needs_no_key_or_openssl(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            approval = root / "approval.md"
            approval.write_text("Owner\n31.12.2099\n", encoding="utf-8")
            value = contract()
            value["approvalSha256"] = context.hashlib.sha256(approval.read_bytes()).hexdigest()
            value["approvalTrustState"] = "OwnerApproved"
            value["approvalBindingSha256"] = context.approval_binding_sha256(value)
            with patch.object(context.subprocess, "run", side_effect=AssertionError("No signing subprocess allowed")):
                context.validate_contract(value)
                self.assertEqual(context.verify_owner_approval(value, approval, value["approvedImage"], value["approvedSourceSha256"]), value["approvalSha256"])
                forged = {**value, "mounts": [{"host": "Other", "container": "/other"}]}
                with self.assertRaises(context.ExecutionContextError):
                    context.verify_owner_approval(forged, approval, forged["approvedImage"], forged["approvedSourceSha256"])

    def test_pending_owner_approval_fails_closed(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            approval = root / "approval.md"
            approval.write_text("approved\n", encoding="utf-8")
            value = contract()
            value["approvalSha256"] = context.hashlib.sha256(approval.read_bytes()).hexdigest()
            value["approvalBindingSha256"] = context.approval_binding_sha256(value)
            context.validate_contract(value)
            with self.assertRaisesRegex(context.ExecutionContextError, "pending"):
                context.verify_owner_approval(value, approval, value["approvedImage"], value["approvedSourceSha256"])

    def test_owner_approval_rejects_link_and_contract_binding_drift(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            approval = root / "approval.md"
            approval.write_text("approved\n", encoding="utf-8")
            link = root / "approval-link.md"
            link.symlink_to(approval)
            value = contract()
            value["approvalTrustState"] = "OwnerApproved"
            value["approvalSha256"] = context.hashlib.sha256(approval.read_bytes()).hexdigest()
            value["approvalBindingSha256"] = context.approval_binding_sha256(value)
            with self.assertRaises(context.ExecutionContextError):
                context.verify_owner_approval(
                    value, link, value["approvedImage"], value["approvedSourceSha256"]
                )
            drifted = {**value, "mounts": [{"host": "Other", "container": "/other"}]}
            with self.assertRaisesRegex(context.ExecutionContextError, "binding"):
                context.validate_contract(drifted)

    def test_windows_mount_accepts_only_exact_native_or_vm_drive_path(self):
        expected = PureWindowsPath("C:/Users/test user/Projects")
        with patch.object(context.sys, "platform", "win32"):
            for source in (str(expected), "C:/Users/test user/Projects", "/mnt/c/Users/test user/Projects"):
                with self.subTest(source=source):
                    self.assertTrue(context.mount_source_matches(source, expected))
            for source in ("/mnt/d/Users/test user/Projects", "/mnt/c/Users/other/Projects",
                           "/mnt/c/Users/test user/Projects-other", "/mnt/c/Users/test user/Projects/repo",
                           "/mnt/c/Users/test user/projects", "/mnt/C/Users/test user/Projects",
                           "/mnt/c/Users/test user/../test user/Projects", "/mnt/c/Users/test user/./Projects",
                           "/mnt/c//Users/test user/Projects", "/mnt/c/Users/test user/Projects/",
                           "/c/Users/test user/Projects", "/Users/test user/Projects",
                           "C:Users/test user/Projects", "Users/test user/Projects",
                           "C:/Users/test user/../test user/Projects", "", None,
                           "/mnt/c/Users/test user/Projects\n"):
                with self.subTest(source=source):
                    self.assertFalse(context.mount_source_matches(source, expected))
            self.assertFalse(context.mount_source_matches("/mnt/c/server/share/Projects",
                                                         PureWindowsPath("//server/share/Projects")))

    def test_vm_translation_is_never_applied_on_macos_or_linux(self):
        for platform in ("darwin", "linux"):
            with self.subTest(platform=platform), patch.object(context.sys, "platform", platform):
                self.assertFalse(context.mount_source_matches("/mnt/c/Users/test/Projects",
                                                              PureWindowsPath("C:/Users/test/Projects")))

    @unittest.skipIf(os.name == "nt", "Native POSIX path comparison")
    def test_posix_mount_comparison_remains_exact(self):
        expected = PurePosixPath("/home/test/Projects")
        for platform in ("darwin", "linux"):
            with patch.object(context.sys, "platform", platform):
                self.assertTrue(context.mount_source_matches(str(expected), expected))
                self.assertFalse(context.mount_source_matches("/home/test/Projects-other", expected))
                self.assertFalse(context.mount_source_matches("/home/test/projects", expected))

    def test_command_trust_without_global_config_after_recreation(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            repo = root / "repo with spaces"
            subprocess.run(["git", "init", "-q", str(repo)], check=True)
            payload = {"root": str(root), "repository": str(repo), "action": "git",
                       "arguments": ["status", "--porcelain=v1"]}
            # A new HOME models a recreated container with no user Git config.
            for generation in ("first", "recreated"):
                home = root / generation
                home.mkdir()
                env = {**os.environ, "HOME": str(home), "GIT_CONFIG_NOSYSTEM": "1",
                       "GIT_CONFIG_GLOBAL": str(home / ".gitconfig"),
                       "GIT_TEST_ASSUME_DIFFERENT_OWNER": "1"}
                denied = subprocess.run(["git", "-C", str(repo), "status"], env=env, capture_output=True)
                self.assertNotEqual(denied.returncode, 0)
                result = subprocess.run([sys.executable, "-c", context.REMOTE_PROGRAM],
                                        input=json.dumps(payload), text=True, capture_output=True, env=env)
                self.assertEqual(result.returncode, 0, result.stderr)
                self.assertEqual(json.loads(result.stdout)["returncode"], 0, result.stdout)
                self.assertFalse((home / ".gitconfig").exists())

    @unittest.skipIf(os.name == "nt", "Linux container environment: Windows drops empty env values; Windows hosts delegate to Linux")
    def test_worker_trust_exact_inherited_and_restored_on_error(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            payload = self.worker_payload(root)
            repo = Path(payload["entries"][0]["containerPath"])
            subprocess.run(["git", "init", "-q", str(repo)], check=True)
            other = root / "not-declared"
            subprocess.run(["git", "init", "-q", str(other)], check=True)
            with patch.dict(os.environ, {"GIT_TEST_ASSUME_DIFFERENT_OWNER": "1",
                                        "GIT_CONFIG_COUNT": "1", "GIT_CONFIG_KEY_0": "safe.directory",
                                        "GIT_CONFIG_VALUE_0": "*"}):
                before = dict(os.environ)
                with self.assertRaisesRegex(RuntimeError, "fixture"):
                    with worker.repository_trust(payload["entries"]):
                        for _ in range(2):
                            child = ["bash", "-c", 'git -C "$1" status --porcelain=v1', "bash", str(repo)]
                            good = subprocess.run(child, capture_output=True)
                            bad = subprocess.run(["git", "-C", str(other), "status"], capture_output=True)
                            self.assertEqual(good.returncode, 0, good.stderr)
                            self.assertNotEqual(bad.returncode, 0)
                        raise RuntimeError("fixture")
                self.assertEqual(dict(os.environ), before)

    def test_worker_trust_rejects_wildcard_and_escape_before_environment_change(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            payload = self.worker_payload(root)
            before = dict(os.environ)
            for path in (str(root / "*"), str(root.parent)):
                entry = {**payload["entries"][0], "containerPath": path}
                with self.assertRaises(ValueError):
                    with worker.repository_trust([entry]):
                        self.fail("Unsafe target accepted")
                self.assertEqual(dict(os.environ), before)

    def test_strict_contract_and_expiry(self):
        self.assertEqual(context.validate_contract(contract()), contract())
        for changes in ({"schemaVersion": True}, {"schemaVersion": 1}, {"expiresOn": "2000-01-01"},
                        {"source": "/opt/../root"}, {"approvalPath": "../secret"}, {"unknown": True},
                        {"container": "--latest"}, {"approvalSha256": "0"},
                        {"approvedImage": "latest"}, {"approvedSourceSha256": "main"}):
            with self.subTest(changes=changes), self.assertRaises(context.ExecutionContextError):
                candidate = {**contract(), **changes}
                if set(candidate) == set(contract()) and "approvalBindingSha256" not in changes:
                    candidate["approvalBindingSha256"] = context.approval_binding_sha256(candidate)
                context.validate_contract(candidate)

    def test_paths_reject_escape_and_ambiguity(self):
        for value in ("", ".", "../escape", "/root", "a//b", "a/./b", "a\\b", 1):
            with self.subTest(value=value), self.assertRaises(context.ExecutionContextError):
                context.relative_path(value)
        self.assertEqual(str(context.relative_path("path with spaces/repo")), "path with spaces/repo")

    def make_context(self, root):
        path = root / "contract.json"
        path.write_text(json.dumps(contract()))
        return context.ExecutionContexts(path, root, [
            {"path": "Projects", "active": True, "kind": "git-repository"},
            {"path": "Projects/repo with spaces", "active": True, "kind": "git-repository"}])

    def test_exact_mapping_not_sibling_prefix(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            router = self.make_context(root)
            self.assertEqual(router.mapping(root / "Projects/repo with spaces"),
                             ("/projects", "/projects/repo with spaces"))
            self.assertIsNone(router.mapping(root / "Projects-other/repo"))
            with self.assertRaises(context.ExecutionContextError):
                router.mapping(root / "Projects/not-declared")

    def test_missing_sandbox_never_falls_back(self):
        with tempfile.TemporaryDirectory() as directory:
            router = self.make_context(Path(directory))
            with patch.object(context.subprocess, "run", side_effect=FileNotFoundError), \
                    self.assertRaises(context.ExecutionContextError):
                router.inspect()

    @unittest.skipUnless(os.name == "nt", "Native Windows Podman source spelling")
    def test_windows_inspection_accepts_vm_source_but_rejects_wrong_mount(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            router = self.make_context(root)
            expected = root / "Projects"
            source = "/mnt/" + expected.drive[0].lower() + expected.as_posix()[2:]
            mount = {"Destination": "/projects", "Source": source, "Type": "bind", "RW": True}
            valid = {"Id": "abc", "Image": "def", "State": {"Running": True},
                     "HostConfig": {"SecurityOpt": ["no-new-privileges"], "CapDrop": ["ALL"]},
                     "Config": {"User": "tester"}, "Mounts": [mount]}
            response = subprocess.CompletedProcess([], 0, json.dumps([valid]), "")
            with patch.object(context.subprocess, "run", return_value=response):
                self.assertEqual(router.inspect()["Id"], "abc")
            for changes in ({"Source": source + "-other"}, {"Source": source + "/child"},
                            {"RW": False}, {"Type": "volume"}):
                response = subprocess.CompletedProcess([], 0, json.dumps([
                    {**valid, "Mounts": [{**mount, **changes}]}]), "")
                with patch.object(context.subprocess, "run", return_value=response), \
                        self.assertRaises(context.ExecutionContextError):
                    router.inspect()

    def test_stopped_wrong_user_wrong_mount_and_changed_identity(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            router = self.make_context(root)
            valid = {"Id": "abc", "Image": "def", "State": {"Running": True},
                     "HostConfig": {"SecurityOpt": ["no-new-privileges"], "CapDrop": ["ALL"]},
                     "Config": {"User": "tester"}, "Mounts": [
                         {"Destination": "/projects", "Source": str(root / "Projects"), "Type": "bind", "RW": True}]}
            with patch.object(context.subprocess, "run", return_value=subprocess.CompletedProcess([], 0, json.dumps([valid]), "")):
                self.assertEqual(router.inspect()["Id"], "abc")
            for changes in ({"State": {"Running": False}}, {"Config": {"User": "root"}}, {"Mounts": []},
                            {"HostConfig": {}},
                            {"HostConfig": {**valid["HostConfig"], "CapAdd": ["CAP_SYS_ADMIN"]}},
                            {"HostConfig": {**valid["HostConfig"], "Privileged": True}},
                            {"HostConfig": {**valid["HostConfig"], "CapDrop": []}},
                            {"Mounts": valid["Mounts"] + [{"Destination": "/opt/reference", "RW": True}]}):
                response = subprocess.CompletedProcess([], 0, json.dumps([{**valid, **changes}]), "")
                with patch.object(context.subprocess, "run", return_value=response), self.assertRaises(context.ExecutionContextError):
                    router.inspect()
            router.container_id, router.image = "different", "def"
            with patch.object(context.subprocess, "run", return_value=subprocess.CompletedProcess([], 0, json.dumps([valid]), "")), self.assertRaises(context.ExecutionContextError):
                router.inspect()

    def test_remote_probe_and_symlink_rejection(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            repo = root / "repo with spaces"
            repo.mkdir()
            (repo / ".git").mkdir()
            payload = {"root": str(root), "repository": str(repo), "action": "probe"}
            result = subprocess.run(["python3", "-c", context.REMOTE_PROGRAM], input=json.dumps(payload), text=True, capture_output=True)
            self.assertEqual(result.returncode, 0, result.stderr)
            self.assertTrue(json.loads(result.stdout)["git"])
            alias = root / "alias"
            alias.symlink_to(repo, target_is_directory=True)
            payload["repository"] = str(alias)
            result = subprocess.run(["python3", "-c", context.REMOTE_PROGRAM], input=json.dumps(payload), text=True, capture_output=True)
            self.assertNotEqual(result.returncode, 0)

    def test_binding_rejects_missing_or_symlinked_package_files(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            config = root / "scripts/config"
            config.mkdir(parents=True)
            manifest = config / "agentic-toolchain-maintenance-files.json"
            manifest.write_text(json.dumps({"files": [{"path": "missing"}]}))
            with self.assertRaises(context.ExecutionContextError):
                context.source_bindings(root)
            manifest.write_text(json.dumps({"files": [{"path": "../escape"}]}))
            with self.assertRaises(context.ExecutionContextError):
                context.source_bindings(root)

    @central_only
    def test_real_package_binds_worker_and_contract(self):
        bindings = context.source_bindings(MODULE.parents[2])
        self.assertIn("scripts/lib/maintenance_container_worker.py", bindings)
        self.assertIn("scripts/lib/secure-development-hardening.sh", bindings)
        self.assertIn("scripts/register-level2-repository.sh", bindings)
        self.assertIn("scripts/propagate-agentic-toolchain-maintenance.sh", bindings)
        self.assertNotIn("scripts/config/maintenance-execution-contexts.json", bindings)
        self.assertTrue(all(len(value) == 64 for value in bindings.values()))

    def test_contract_must_cover_every_active_protected_workspace_root(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            contract_path = root / "contract.json"
            contract_path.write_text(json.dumps(contract()), encoding="utf-8")
            targets = [
                {"path": "Projects/ordinary", "active": True, "kind": "git-repository"},
                {"path": "SecureOrderDeskProjects", "active": True, "kind": "collection"},
            ]
            with self.assertRaisesRegex(context.ExecutionContextError, "SecureOrderDeskProjects"):
                context.ExecutionContexts(contract_path, root, targets)

    def test_expired_contract_can_be_routed_but_not_executed(self):
        expired = {**contract(), "expiresOn": "2000-01-01"}
        expired["approvalBindingSha256"] = context.approval_binding_sha256(expired)
        self.assertEqual(context.validate_contract(expired, check_expiry=False), expired)
        with self.assertRaises(context.ExecutionContextError):
            context.validate_contract(expired)

    def test_remote_program_rejects_delivery_and_unbounded_merges(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            repo = root / "repo"
            repo.mkdir()
            (repo / ".git").mkdir()
            for args in (["push"], ["commit"], ["merge", "main"], ["merge", "--ff-only", "origin/main"]):
                payload = {"root": str(root), "repository": str(repo), "action": "git", "arguments": args}
                result = subprocess.run(["python3", "-c", context.REMOTE_PROGRAM], input=json.dumps(payload), text=True, capture_output=True)
                self.assertNotEqual(result.returncode, 0, args)

    def worker_payload(self, root, phase="propagation", mode="check-only"):
        repo = root / "repo with spaces"
        repo.mkdir()
        (repo / ".git").mkdir()
        return {"source": str(root), "mode": mode, "phase": phase, "runId": "fixture",
                "repairDrift": False, "cleanupProfile": "safe", "defaultPresetProfile": "none",
                "entries": [{"containerRoot": str(root), "containerPath": str(repo), "targetId": "fixture",
                             "registry": {"path": "Projects/repo with spaces", "level": 2}}]}

    def test_worker_never_repairs_without_explicit_flag(self):
        with tempfile.TemporaryDirectory() as directory:
            payload = self.worker_payload(Path(directory).resolve(), mode="update")
            with patch.object(worker.subprocess, "run", return_value=subprocess.CompletedProcess([], 1, "drift", "")) as run:
                result = worker.execute(payload)
            self.assertEqual(result["status"], "Blocked")
            self.assertEqual(run.call_count, 1)
            self.assertEqual(run.call_args.args[0][-1], "--check-only")
            self.assertIn(payload["entries"][0]["containerPath"], run.call_args.args[0])

    def test_worker_preview_preserves_drift_status(self):
        with tempfile.TemporaryDirectory() as directory:
            payload = self.worker_payload(Path(directory).resolve(), mode="dry-run")
            with patch.object(worker.subprocess, "run", return_value=subprocess.CompletedProcess([], 1, "drift", "")):
                result = worker.execute(payload)
            self.assertEqual(result["status"], "Blocked")

    def test_worker_registry_preview_does_not_claim_update_success(self):
        with tempfile.TemporaryDirectory() as directory:
            payload = self.worker_payload(Path(directory).resolve(), phase="registry")
            with patch.object(worker.subprocess, "run", return_value=subprocess.CompletedProcess([], 0, "updated: fixture\n", "")):
                result = worker.execute(payload)
            self.assertEqual(result["status"], "Blocked")
            self.assertEqual(result["registry"][0]["path"], payload["entries"][0]["containerPath"])

    def test_worker_refuses_dirty_repair(self):
        with tempfile.TemporaryDirectory() as directory:
            payload = self.worker_payload(Path(directory).resolve(), mode="update")
            payload["repairDrift"] = True
            results = [subprocess.CompletedProcess([], 1, "drift", ""),
                       subprocess.CompletedProcess([], 0, " M user-file\n", "")]
            with patch.object(worker.subprocess, "run", side_effect=results) as run:
                result = worker.execute(payload)
            self.assertEqual(run.call_count, 2)
            self.assertEqual(result["records"][0]["reason"], "DirtyWorktreeRequiresReview")
            self.assertEqual(result["status"], "Blocked")

    def test_worker_refuses_symlink_git_directory(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            payload = self.worker_payload(root)
            gitdir = Path(payload["entries"][0]["containerPath"]) / ".git"
            gitdir.rmdir()
            gitdir.symlink_to(root, target_is_directory=True)
            with patch.object(worker.subprocess, "run") as run, self.assertRaises(ValueError):
                worker.execute(payload)
            run.assert_not_called()

    @unittest.skipIf(os.name == "nt", "Linux leaf worker; native Windows wrapper is tested separately")
    @central_only
    def test_real_registry_worker_preserves_curated_metadata(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            payload = self.worker_payload(root, phase="registry", mode="update")
            payload["source"] = str(MODULE.parents[2])
            entry = payload["entries"][0]
            subprocess.run(["git", "init", "-q", entry["containerPath"]], check=True)
            entry["registry"].update(primaryLanguage="csharp", mslStatus="msl", gsdbRequired=True,
                                     presetProfile="none", role="level-2-project", source="owner-curated",
                                     registeredAt="2026-01-01")
            result = worker.execute(payload)
            self.assertEqual(result["status"], "Passed", result)
            returned = result["registry"][0]
            for key in ("primaryLanguage", "mslStatus", "gsdbRequired", "presetProfile", "registeredAt", "source"):
                self.assertEqual(returned[key], entry["registry"][key])

    @unittest.skipIf(os.name == "nt", "Linux leaf worker; native Windows wrapper is tested separately")
    @central_only
    def test_real_propagation_worker_reports_drift_without_writes(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            payload = self.worker_payload(root, mode="check-only")
            payload["source"] = str(MODULE.parents[2])
            repo = Path(payload["entries"][0]["containerPath"])
            subprocess.run(["git", "init", "-q", str(repo)], check=True)
            (repo / "AGENTS.md").write_text("# Fixture\n", encoding="utf-8")
            before = sorted(p.relative_to(repo).as_posix() for p in repo.rglob("*"))
            result = worker.execute(payload)
            self.assertEqual(result["status"], "Blocked", result)
            self.assertEqual(before, sorted(p.relative_to(repo).as_posix() for p in repo.rglob("*")))

    @central_only
    def test_preflight_failure_closes_barrier_before_git(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            args = fleet.build_parser().parse_args(["fleet", "--manifest", str(MODULE.parents[2] / "scripts/config/agentic-workspace-fleet.json"),
                "--home-dir", str(root), "--mode", "update", "--report", str(root / "report.json"),
                "--log", str(root / "run.log"), "--execution-contract", str(MODULE.parents[2] / "scripts/config/maintenance-execution-contexts.json")])
            with patch.dict(sys.modules, {"maintenance_execution_context": context}), \
                    patch.object(context.ExecutionContexts, "preflight", side_effect=context.ExecutionContextError("fixture unavailable")), \
                    patch.object(fleet, "run_git_network") as network:
                self.assertEqual(fleet.execute_fleet(args), 2)
            network.assert_not_called()
            report = json.loads(args.report.read_text())
            self.assertFalse(report["mutationBarrier"]["domainMutationAllowed"])
            self.assertEqual(report["findings"][0]["code"], "SandboxPreflightBlocked")

    @central_only
    def test_sandbox_bound_repositories_never_use_host_git_transport(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            repository = root / "Projects/repo with spaces"

            class Router:
                @staticmethod
                def mapping(path):
                    return ("/projects", "/projects/repo with spaces") if path == repository else None

                @staticmethod
                def request(path, action):
                    self.assertEqual((path, action), (repository, "probe"))
                    return {"exists": True, "directory": True, "git": True}

                @staticmethod
                def git(path, arguments):
                    self.assertEqual(path, repository)
                    if tuple(arguments) == ("remote", "get-url", "origin"):
                        return subprocess.CompletedProcess([], 0, "https://example.invalid/repo.git\n", "")
                    raise AssertionError(f"Unexpected delegated Git operation: {arguments}")

            fleet.EXECUTION_CONTEXTS = Router()
            try:
                with patch.object(fleet.subprocess, "run") as host_git:
                    for operation in ("fetch", "ls-remote", "pull"):
                        with self.subTest(operation=operation), self.assertRaises(fleet.ContractError):
                            fleet.run_git_network(repository, operation)
                    result = fleet.classify_repository(
                        {"id": "fixture", "path": "Projects/repo with spaces",
                         "kind": "git-repository", "maintenanceClass": "canonical-fleet",
                         "remote": "https://example.invalid/repo.git"},
                        repository, "update",
                    )
                host_git.assert_not_called()
                self.assertEqual(result["result"], "Blocked")
                self.assertEqual(result["findingCode"], "HostGitTransportBlocked")
            finally:
                fleet.EXECUTION_CONTEXTS = None

    def phase_fixture(self, root, mode="update"):
        router = self.make_context(root)
        contract_path = root / "contract.json"
        targets = [{"id": "fixture", "path": "Projects/repo with spaces", "active": True, "kind": "git-repository"}]
        manifest = root / "manifest.json"
        manifest.write_text(json.dumps({"targets": targets}))
        registry = root / "registry.json"
        registry.write_text(json.dumps({"repositories": [{"path": targets[0]["path"], "level": 2}, {"path": "Other/repo", "level": 2}]}))
        evidence = {"context": "container", "containerId": "fixture", "image": "fixture-image", "approvalSha256": "a", "sourceSha256": "b"}
        if CANONICAL_SOURCE:
            evidence["sourceSha256"] = context.source_binding_sha256(context.source_bindings(MODULE.parents[2]))
        report = root / "report.json"
        report.write_text(json.dumps({"runId": "fixture", "executionContexts": [evidence], "mutationBarrier": {"domainMutationAllowed": True}}))
        args = SimpleNamespace(contract=contract_path, manifest=manifest, home_dir=root, source=MODULE.parents[2],
            registry=registry, report=report, phase="registry", mode=mode, repair_drift=False, cleanup_profile="none",
            confirm_deep_cleanup=False, output=root / "host-registry.json")
        return args, evidence

    @central_only
    def test_registry_return_is_translated_and_host_entries_are_preserved(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            args, evidence = self.phase_fixture(root)
            response = {"status": "Passed", "records": [{"targetId": "fixture", "status": "Passed"}],
                        "registry": [{"path": "/projects/repo with spaces", "level": 2, "primaryLanguage": "csharp"}]}
            with patch.object(context.ExecutionContexts, "preflight", return_value=evidence), \
                    patch.object(context.ExecutionContexts, "inspect", return_value={}), \
                    patch.object(context.subprocess, "run", return_value=subprocess.CompletedProcess([], 0, json.dumps(response), "")), \
                    patch.dict(sys.modules, {"agentic_workspace_fleet": fleet}):
                self.assertEqual(context.run_phase(args), 0)
            entries = json.loads(args.registry.read_text())["repositories"]
            self.assertEqual(entries[0]["path"], "Projects/repo with spaces")
            self.assertEqual(entries[0]["primaryLanguage"], "csharp")
            self.assertEqual(entries[1], {"path": "Other/repo", "level": 2})
            self.assertEqual(json.loads(args.report.read_text())["delegatedPhases"][0]["executionContext"], "container")

    @central_only
    def test_lost_container_does_not_modify_registry_or_report(self):
        with tempfile.TemporaryDirectory() as directory:
            args, evidence = self.phase_fixture(Path(directory).resolve())
            before = (args.registry.read_bytes(), args.report.read_bytes())
            with patch.object(context.ExecutionContexts, "preflight", return_value=evidence), \
                    patch.object(context.subprocess, "run", return_value=subprocess.CompletedProcess([], 125, "", "lost")), \
                    self.assertRaises(context.ExecutionContextError):
                context.run_phase(args)
            self.assertEqual(before, (args.registry.read_bytes(), args.report.read_bytes()))

    def test_host_registry_filter_excludes_delegated_targets(self):
        with tempfile.TemporaryDirectory() as directory:
            args, evidence = self.phase_fixture(Path(directory).resolve(), mode="check-only")
            args.phase = "host-registry"
            with patch.object(context.ExecutionContexts, "preflight", return_value=evidence), patch.object(context.subprocess, "run") as run:
                self.assertEqual(context.run_phase(args), 0)
            run.assert_not_called()
            self.assertEqual(json.loads(args.output.read_text())["repositories"], [{"path": "Other/repo", "level": 2}])


if __name__ == "__main__":
    unittest.main()
