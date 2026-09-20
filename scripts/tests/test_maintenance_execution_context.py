"""Sandbox routing fixtures: no production paths, network or Podman required."""
import datetime
import importlib.util
import json
import os
import sys
from pathlib import Path
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
    return {"schemaVersion": 1, "container": "test-sandbox", "user": "tester",
            "source": "/opt/reference", "approvalPath": "sandbox/approval.md",
            "expiresOn": "2099-12-31", "mounts": [{"host": "Projects", "container": "/projects"}]}


class ExecutionContextTests(unittest.TestCase):
    def test_strict_contract_and_expiry(self):
        self.assertEqual(context.validate_contract(contract()), contract())
        for changes in ({"schemaVersion": True}, {"schemaVersion": 2}, {"expiresOn": "2000-01-01"},
                        {"source": "/opt/../root"}, {"approvalPath": "../secret"}, {"unknown": True},
                        {"container": "--latest"}):
            with self.subTest(changes=changes), self.assertRaises(context.ExecutionContextError):
                context.validate_contract({**contract(), **changes})

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

    def test_stopped_wrong_user_wrong_mount_and_changed_identity(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            router = self.make_context(root)
            valid = {"Id": "abc", "Image": "def", "State": {"Running": True},
                     "Config": {"User": "tester"}, "Mounts": [
                         {"Destination": "/projects", "Source": str(root / "Projects"), "Type": "bind", "RW": True}]}
            for changes in ({"State": {"Running": False}}, {"Config": {"User": "root"}}, {"Mounts": []}):
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
        self.assertIn("scripts/config/maintenance-execution-contexts.json", bindings)
        self.assertTrue(all(len(value) == 64 for value in bindings.values()))

    def test_expired_contract_can_be_routed_but_not_executed(self):
        expired = {**contract(), "expiresOn": "2000-01-01"}
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
    def test_two_delegated_fast_forwards_follow_all_fetches(self):
        fixture_spec = importlib.util.spec_from_file_location("fleet_fixtures", Path(__file__).with_name("test_agentic_workspace_maintenance.py"))
        fixtures = importlib.util.module_from_spec(fixture_spec)
        fixture_spec.loader.exec_module(fixtures)
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory).resolve()
            fixture = fixtures.FleetFixture(root)
            fixture.run("update")
            second = fixture.home / "Fleet/Second"
            second_remote = root / "second.git"
            subprocess.run(["git", "clone", "--bare", "-q", str(fixture.remote), str(second_remote)], check=True)
            subprocess.run(["git", "clone", "-q", str(second_remote), str(second)], check=True)
            manifest_path = fixture.manifest()
            manifest = json.loads(manifest_path.read_text())
            manifest["targets"] = [manifest["targets"][0], manifest["targets"][1], {**manifest["targets"][1], "id": "second", "path": "Fleet/Second", "remote": str(second_remote)}]
            manifest_path.write_text(json.dumps(manifest))
            (fixture.seed / "README.md").write_text("remote update\n")
            fixtures.git(fixture.seed, "commit", "-qam", "advance")
            fixtures.git(fixture.seed, "push", "-q")
            fixtures.git(fixture.seed, "push", "-q", str(second_remote), "main")
            expected = fixtures.git(fixture.seed, "rev-parse", "HEAD")
            contract_path = root / "contract.json"
            contract_path.write_text(json.dumps({**contract(), "mounts": [{"host": "Fleet", "container": "/fleet"}]}))
            args = fleet.build_parser().parse_args(["fleet", "--manifest", str(manifest_path), "--home-dir", str(fixture.home),
                "--mode", "update", "--report", str(root / "delegated.json"), "--log", str(root / "run.log"),
                "--execution-contract", str(contract_path)])
            operations = []
            original_network = fleet.run_git_network
            def network(repository, *arguments):
                operations.append(arguments[0])
                return original_network(repository, *arguments)
            def request(router, repository, action, arguments=()):
                if arguments and arguments[0] == "merge":
                    self.assertEqual(operations.count("fetch"), 2)
                    operations.append("local-ff")
                result = subprocess.run([sys.executable, "-c", context.REMOTE_PROGRAM],
                    input=json.dumps({"root": str(fixture.home / "Fleet"), "repository": str(repository),
                                      "action": action, "arguments": arguments}), text=True, capture_output=True)
                self.assertEqual(result.returncode, 0, result.stderr)
                return json.loads(result.stdout)
            with patch.dict(sys.modules, {"maintenance_execution_context": context}), \
                    patch.object(context.ExecutionContexts, "preflight", return_value={"context": "container"}), \
                    patch.object(context.ExecutionContexts, "request", request), \
                    patch.object(fleet, "run_git_network", network):
                self.assertEqual(fleet.execute_fleet(args), 0)
            self.assertEqual(operations.count("local-ff"), 2)
            report = json.loads(args.report.read_text())
            self.assertTrue(report["mutationBarrier"]["domainMutationAllowed"])
            for name in ("Example", "Second"):
                self.assertEqual(fixtures.git(fixture.home / "Fleet" / name, "rev-parse", "HEAD"), expected)
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
