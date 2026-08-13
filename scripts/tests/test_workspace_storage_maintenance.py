#!/usr/bin/env python3
"""Contract tests for safe Level-2 storage maintenance."""

from __future__ import annotations

import importlib.util
import json
import os
import pathlib
import shutil
import stat
import subprocess
import tempfile
import time
import unittest
from unittest import mock


ROOT = pathlib.Path(__file__).resolve().parents[2]
ENGINE_PATH = ROOT / "scripts/lib/workspace_storage_maintenance.py"
FLEET_ENGINE = ROOT / "scripts/lib/agentic_workspace_fleet.py"
WRAPPER = ROOT / "scripts/maintain-workspace-storage.sh"
POLICY_PATH = ROOT / "scripts/config/workspace-storage-maintenance.json"
SPEC = importlib.util.spec_from_file_location("workspace_storage_maintenance", ENGINE_PATH)
assert SPEC and SPEC.loader
storage = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(storage)


def run(*arguments: str, cwd: pathlib.Path) -> subprocess.CompletedProcess[str]:
    return subprocess.run(arguments, cwd=cwd, text=True, capture_output=True, check=True)


def initialize_repository(path: pathlib.Path) -> None:
    path.mkdir(parents=True)
    run("git", "init", "-q", cwd=path)
    run("git", "config", "user.name", "Storage Test", cwd=path)
    run("git", "config", "user.email", "storage@example.invalid", cwd=path)


def storage_wrapper_command(
    *,
    home: pathlib.Path,
    registry: pathlib.Path,
    report: pathlib.Path,
    profile: str,
    check_only: bool = False,
) -> list[str]:
    """Select the public wrapper that matches the runner operating system."""
    if os.name == "nt":
        command = [
            "pwsh", "-NoProfile", "-File",
            str(ROOT / "scripts/maintain-workspace-storage.ps1"),
            "-HomeDir", str(home),
            "-RegistryPath", str(registry),
            "-PolicyPath", str(POLICY_PATH),
            "-ResultFile", str(report),
            "-CleanupProfile", profile.capitalize(),
        ]
        if check_only:
            command.append("-CheckOnly")
        return command
    command = [
        "bash", str(WRAPPER),
        "--home-dir", str(home),
        "--registry", str(registry),
        "--policy", str(POLICY_PATH),
        "--result-file", str(report),
        "--profile", profile,
    ]
    if check_only:
        command.append("--check-only")
    return command


class WorkspaceStorageMaintenanceTests(unittest.TestCase):
    def test_policy_classifies_curated_non_msl_adapters(self) -> None:
        policy = json.loads(POLICY_PATH.read_text(encoding="utf-8"))
        adapters = storage.policy_adapter_map(policy)

        cc65 = adapters["c64projects/cc65"]
        tvision = adapters["clionprojects/tvision"]
        self.assertEqual(("non-msl-c89-6502", "non-msl"), storage.classify_runtime({}, cc65))
        self.assertEqual(("non-msl-cpp14-cmake", "non-msl"), storage.classify_runtime({}, tvision))
        self.assertIn("C89", cc65["justification"])
        self.assertIn("C++14", tvision["justification"])

    def test_generic_candidate_requires_ignored_untracked_contained_directory(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            repository = pathlib.Path(temporary) / "repo"
            initialize_repository(repository)
            (repository / ".gitignore").write_text("bin/\n", encoding="utf-8")
            (repository / "App.csproj").write_text("<Project />\n", encoding="utf-8")
            run("git", "add", ".gitignore", "App.csproj", cwd=repository)
            run("git", "commit", "-qm", "fixture", cwd=repository)
            output = repository / "bin"
            output.mkdir()
            (output / "app.dll").write_bytes(b"generated")
            old = time.time() - 10 * 86400
            os.utime(output / "app.dll", (old, old))
            os.utime(output, (old, old))

            adapter = {
                "id": "dotnet",
                "markers": ["*.csproj"],
                "directoryNames": ["bin"],
            }
            candidates = storage.discover_generic_candidates(repository, adapter, 7, False)

            self.assertEqual(1, len(candidates))
            self.assertEqual("bin", candidates[0]["path"])
            self.assertTrue(candidates[0]["eligible"])

            outside = pathlib.Path(temporary) / "outside"
            outside.mkdir()
            (repository / "linked-bin").symlink_to(outside, target_is_directory=True)
            adapter["directoryNames"] = ["linked-bin"]
            self.assertEqual([], storage.discover_generic_candidates(repository, adapter, 7, True))
            with self.assertRaisesRegex(storage.StorageContractError, "must not be a symlink"):
                storage.contained_path(repository / "linked-bin", repository, "candidate")

    def test_generic_update_removes_only_the_verified_temp_fixture(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            repository = pathlib.Path(temporary) / "repo"
            initialize_repository(repository)
            (repository / ".gitignore").write_text("dist/\n", encoding="utf-8")
            (repository / "package.json").write_text("{}\n", encoding="utf-8")
            run("git", "add", ".gitignore", "package.json", cwd=repository)
            run("git", "commit", "-qm", "fixture", cwd=repository)
            output = repository / "dist"
            output.mkdir()
            (output / "bundle.js").write_text("generated\n", encoding="utf-8")
            policy = {
                "genericBuildAdapters": [{
                    "id": "node",
                    "markers": ["package.json"],
                    "directoryNames": ["dist"],
                }],
                "processNames": {"node": ["node"]},
            }

            candidates, freed, result, used = storage.handle_generic_repository(
                repository,
                policy,
                mode="update",
                retention_days=7,
                pressure_mode=True,
                dirty=False,
                processes=set(),
            )

            self.assertFalse(output.exists())
            self.assertGreater(freed, 0)
            self.assertEqual("Removed", candidates[0]["status"])
            self.assertEqual("Passed", result)
            self.assertEqual(["node"], used)

    def test_cmake_candidate_requires_cache_provenance_and_deduplicates_nested_tree(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            repository = pathlib.Path(temporary) / "repo"
            initialize_repository(repository)
            (repository / ".gitignore").write_text("build/\n", encoding="utf-8")
            (repository / "CMakeLists.txt").write_text("cmake_minimum_required(VERSION 3.20)\n", encoding="utf-8")
            run("git", "add", ".gitignore", "CMakeLists.txt", cwd=repository)
            run("git", "commit", "-qm", "fixture", cwd=repository)
            outer = repository / "build"
            nested = outer / "_deps/dep/cmake-build-debug"
            nested.mkdir(parents=True)
            cache = f"CMAKE_HOME_DIRECTORY:INTERNAL={repository}\n"
            (outer / "CMakeCache.txt").write_text(cache, encoding="utf-8")
            (nested / "CMakeCache.txt").write_text(cache, encoding="utf-8")
            adapter = {"buildDirectoryPatterns": ["build", "cmake-build-*"]}

            candidates = storage.discover_cmake_candidates(repository, adapter, 7, True)

            self.assertEqual(["build"], [item["path"] for item in candidates])
            self.assertEqual("cmake-out-of-source", candidates[0]["adapter"])

    def test_cc65_make_preview_rejects_protected_or_unbounded_deletion(self) -> None:
        storage.validate_cc65_make_preview(
            "rm -rf ../wrk ../bin\n",
            ["wrk", "bin"],
            ["*.d64", "*.prg"],
        )
        with self.assertRaisesRegex(storage.StorageContractError, "protected deletion"):
            storage.validate_cc65_make_preview(
                "rm -f samples/demo.d64\n",
                ["wrk", "bin"],
                ["*.d64", "*.prg"],
            )
        with self.assertRaisesRegex(storage.StorageContractError, "outside declared group"):
            storage.validate_cc65_make_preview(
                "rm -rf ../samples\n",
                ["wrk", "bin"],
                ["*.d64", "*.prg"],
            )
        with self.assertRaisesRegex(storage.StorageContractError, "outside declared group"):
            storage.validate_cc65_make_preview(
                "rm -rf ../wrk ../unexpected\n",
                ["wrk", "bin"],
                ["*.d64", "*.prg"],
            )
        self.assertNotIn("zap", POLICY_PATH.read_text(encoding="utf-8").casefold())

    def test_container_contract_prunes_only_dangling_images(self) -> None:
        with mock.patch.object(storage.shutil, "which", side_effect=lambda name: "/usr/bin/podman" if name == "podman" else None), \
                mock.patch.object(storage, "podman_socket_arguments", return_value=(["--url", "unix:///tmp/podman.sock"], None)), \
                mock.patch.object(storage, "execute_provider") as execute:
            items = storage.handle_container_provider(
                mode="update",
                pressure_mode=True,
                retention_days=7,
                processes=set(),
            )

        command = execute.call_args.args[1]
        self.assertEqual("dangling-image-prune", items[0]["action"])
        self.assertEqual(["image", "prune", "--force"], command[-3:])
        forbidden = {"--all", "system", "volume", "--volumes"}
        self.assertFalse(forbidden.intersection(command))

    def test_missing_process_inventory_blocks_mutating_adapters(self) -> None:
        unavailable = subprocess.CompletedProcess(["ps"], 127, "", "denied")
        with mock.patch.dict(os.environ, {}, clear=False), \
                mock.patch.object(storage, "run_command", return_value=unavailable):
            os.environ.pop("HB_STORAGE_ACTIVE_PROCESSES", None)
            processes = storage.process_inventory()

        self.assertIn(storage.PROCESS_INVENTORY_UNAVAILABLE, processes)
        self.assertTrue(storage.adapter_is_busy("dotnet", processes, {
            "processNames": {"dotnet": []},
        }))

    def test_process_inventory_parsers_exclude_the_engine_pid(self) -> None:
        self.assertEqual(
            {"node"},
            storage.parse_ps_processes(
                "42 /usr/bin/python3 /usr/bin/python3 engine.py\n"
                "99 /opt/node /opt/node /opt/app.js\n",
                42,
            ),
        )
        self.assertEqual(
            {"node"},
            storage.parse_tasklist_processes('"python.exe","42"\n"node.exe","99"\n', 42),
        )
        self.assertEqual(
            {"node", "npm"},
            storage.parse_ps_processes(
                "99 /opt/node /opt/node /opt/npm/bin/npm-cli.js install\n",
                42,
            ),
        )

    def test_deep_update_requires_confirmation_and_writes_private_report(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            home = pathlib.Path(temporary)
            state = home / ".home-baseline"
            state.mkdir()
            registry = state / "level2-repository-registry.json"
            registry.write_text('{"repositories": []}\n', encoding="utf-8")
            report = state / "reports/result.json"
            result = subprocess.run(
                storage_wrapper_command(
                    home=home,
                    registry=registry,
                    report=report,
                    profile="deep",
                ),
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(2, result.returncode)
            payload = json.loads(report.read_text(encoding="utf-8"))
            self.assertEqual("FAILED", payload["overallStatus"])
            # POSIX mode bits do not model the Windows ACL that the matching
            # PowerShell wrapper already enforces before returning.
            if os.name != "nt":
                self.assertEqual(0o600, stat.S_IMODE(report.stat().st_mode))

    def test_none_profile_is_an_atomic_no_op(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            home = pathlib.Path(temporary)
            state = home / ".home-baseline"
            state.mkdir()
            registry = state / "level2-repository-registry.json"
            registry.write_text('{"repositories": []}\n', encoding="utf-8")
            report = state / "reports/result.json"
            result = subprocess.run(
                storage_wrapper_command(
                    home=home,
                    registry=registry,
                    report=report,
                    profile="none",
                    check_only=True,
                ),
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(0, result.returncode, result.stderr)
            payload = json.loads(report.read_text(encoding="utf-8"))
            self.assertEqual("none", payload["profile"])
            self.assertEqual(0, payload["freedBytes"])
            if os.name != "nt":
                self.assertEqual(0o600, stat.S_IMODE(report.stat().st_mode))

    @unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is unavailable")
    def test_powershell_wrapper_keeps_single_python_launcher_as_array(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            home = pathlib.Path(temporary)
            state = home / ".home-baseline"
            state.mkdir()
            registry = state / "level2-repository-registry.json"
            registry.write_text('{"repositories": []}\n', encoding="utf-8")
            report = state / "reports/result.json"
            result = subprocess.run(
                [
                    "pwsh", "-NoProfile", "-File", str(ROOT / "scripts/maintain-workspace-storage.ps1"),
                    "-CheckOnly", "-CleanupProfile", "None",
                    "-HomeDir", str(home), "-RegistryPath", str(registry),
                    "-PolicyPath", str(POLICY_PATH), "-ResultFile", str(report),
                ],
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(0, result.returncode, result.stdout + result.stderr)
            self.assertEqual("SUCCESS", json.loads(report.read_text(encoding="utf-8"))["overallStatus"])

    def test_fleet_report_embeds_validated_storage_result(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = pathlib.Path(temporary)
            report = root / "fleet.json"
            result = root / "storage.json"
            report.write_text(
                json.dumps({"stages": [], "artifacts": {}}) + "\n",
                encoding="utf-8",
            )
            storage_result = {
                "schemaVersion": "1.0",
                "runId": "00000000-0000-0000-0000-000000000001",
                "platform": "darwin",
                "mode": "check-only",
                "profile": "safe",
                "overallStatus": "SUCCESS_WITH_WARNINGS",
                "exitCode": 0,
                "repositories": [],
                "providers": [],
                "warnings": ["podman: ProviderUnavailable"],
                "nextAction": "Review warnings",
            }
            result.write_text(json.dumps(storage_result) + "\n", encoding="utf-8")

            completed = subprocess.run(
                [
                    "python3", str(FLEET_ENGINE), "stage",
                    "--report", str(report),
                    "--stage-id", "storage-cleanup",
                    "--status", "Warning",
                    "--exit-code", "0",
                    "--summary", "Storage warning",
                    "--storage-results", str(result),
                ],
                text=True,
                capture_output=True,
                check=False,
            )

            self.assertEqual(0, completed.returncode, completed.stderr)
            payload = json.loads(report.read_text(encoding="utf-8"))
            self.assertEqual("safe", payload["storageCleanup"]["profile"])
            self.assertEqual(str(result), payload["artifacts"]["storageReportPath"])
            self.assertEqual("SUCCESS_WITH_WARNINGS", payload["overallStatus"])

    def test_one_command_orchestrators_order_preview_and_storage_before_final(self) -> None:
        bash = (ROOT / "scripts/maintain-agentic-workspace.sh").read_text(encoding="utf-8")
        powershell = (ROOT / "scripts/maintain-agentic-workspace.ps1").read_text(encoding="utf-8")

        self.assertLess(bash.index('CURRENT_STAGE="model-routing"'), bash.index('CURRENT_STAGE="storage-cleanup"'))
        self.assertLess(bash.index('CURRENT_STAGE="storage-cleanup"'), bash.index('CURRENT_STAGE="verification"'))
        storage_stage = bash[bash.index('CURRENT_STAGE="storage-cleanup"'):bash.index('CURRENT_STAGE="verification"')]
        self.assertLess(storage_stage.index("--dry-run"), storage_stage.index("update_arguments="))
        self.assertLess(
            powershell.index("-PhaseId 'model-routing'"),
            powershell.index("-PhaseId 'storage-cleanup'"),
        )
        self.assertLess(
            powershell.index("-PhaseId 'storage-cleanup'"),
            powershell.index("-PhaseId 'final'", powershell.index("-PhaseId 'storage-cleanup'")),
        )
        self.assertLess(
            powershell.index("$previewParameters.WhatIf = $true"),
            powershell.index("& $storageMaintainer @storageParameters", powershell.index("$previewParameters.WhatIf = $true")),
        )


if __name__ == "__main__":
    unittest.main()
