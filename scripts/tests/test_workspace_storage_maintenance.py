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
    def test_npm_cleanup_binds_the_verified_custom_cache_path(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            home = pathlib.Path(temporary) / "home"
            cache = home / "custom-npm-cache"
            cache.mkdir(parents=True)
            (cache / "entry").write_text("cache data", encoding="utf-8")
            for profile, action, suffix in (
                ("deep", "cache-clean", ["clean", "--force"]),
                ("safe", "cache-verify", ["verify"]),
            ):
                with (
                    self.subTest(profile=profile),
                    mock.patch.object(storage.shutil, "which", side_effect=lambda name: "npm" if name == "npm" else None),
                    mock.patch.object(storage, "run_command", return_value=subprocess.CompletedProcess(
                        ["npm", "config", "get", "cache"], 0, str(cache) + "\n", ""
                    )),
                    mock.patch.object(storage, "execute_provider") as execute,
                ):
                    items = storage.handle_cache_providers(
                        home, mode="update", profile=profile, pressure_mode=False,
                        retention_days=30, processes=set(),
                    )
                    self.assertEqual(action, items[0]["action"])
                    self.assertEqual(
                        ["npm", "--cache", str(cache.resolve()), "cache", *suffix],
                        execute.call_args.args[1],
                    )

    def test_npm_cleanup_rejects_home_or_outside_as_cache_root(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            home = pathlib.Path(temporary) / "home"
            home.mkdir()
            (home / "personal-file").write_text("keep", encoding="utf-8")
            outside = pathlib.Path(temporary) / "outside"
            outside.mkdir()
            (outside / "personal-file").write_text("keep", encoding="utf-8")
            for candidate in (home, outside):
                with (
                    self.subTest(candidate=candidate),
                    mock.patch.object(storage.shutil, "which", side_effect=lambda name: "npm" if name == "npm" else None),
                    mock.patch.object(storage, "run_command", return_value=subprocess.CompletedProcess(
                        ["npm", "config", "get", "cache"], 0, str(candidate) + "\n", ""
                    )),
                    mock.patch.object(storage, "execute_provider") as execute,
                ):
                    items = storage.handle_cache_providers(
                        home, mode="update", profile="deep", pressure_mode=False,
                        retention_days=30, processes=set(),
                    )
                    self.assertEqual("ProviderUnavailable", items[0]["status"])
                    execute.assert_not_called()

    @unittest.skipUnless(shutil.which("npm"), "npm is unavailable")
    def test_npm_cleanup_uses_project_configured_cache_inside_home(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = pathlib.Path(temporary)
            home = root / "home"
            cache = home / "custom-npm-cache"
            cache.mkdir(parents=True)
            (cache / "entry").write_text("cache data", encoding="utf-8")
            project = root / "project"
            project.mkdir()
            (project / "package.json").write_text("{}\n", encoding="utf-8")
            (project / ".npmrc").write_text(f"cache={cache}\n", encoding="utf-8")
            environment = {key: value for key, value in os.environ.items() if key.lower() != "npm_config_cache"}
            environment["NPM_CONFIG_USERCONFIG"] = str(root / "empty-user.npmrc")
            previous_directory = pathlib.Path.cwd()
            try:
                os.chdir(project)
                with (
                    mock.patch.dict(os.environ, environment, clear=True),
                    mock.patch.object(storage.shutil, "which", side_effect=lambda name: "npm" if name == "npm" else None),
                    mock.patch.object(storage, "execute_provider") as execute,
                ):
                    items = storage.handle_cache_providers(
                        home, mode="update", profile="deep", pressure_mode=False,
                        retention_days=30, processes=set(),
                    )
            finally:
                os.chdir(previous_directory)
            self.assertEqual("cache-clean", items[0]["action"])
            self.assertEqual(
                ["npm", "--cache", str(cache.resolve()), "cache", "clean", "--force"],
                execute.call_args.args[1],
            )

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

    def test_cmake_update_removes_verified_tree_without_executing_build_rules(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            repository = pathlib.Path(temporary) / "repo"
            initialize_repository(repository)
            (repository / ".gitignore").write_text("build/\n", encoding="utf-8")
            (repository / "CMakeLists.txt").write_text("cmake_minimum_required(VERSION 3.20)\n", encoding="utf-8")
            run("git", "add", ".gitignore", "CMakeLists.txt", cwd=repository)
            run("git", "commit", "-qm", "fixture", cwd=repository)
            build = repository / "build"
            build.mkdir()
            (build / "CMakeCache.txt").write_text(
                f"CMAKE_HOME_DIRECTORY:INTERNAL={repository}\n",
                encoding="utf-8",
            )
            (build / "build.ninja").write_text("rule malicious\n  command = touch ../outside\n", encoding="utf-8")
            (build / "output.o").write_bytes(b"generated")
            adapter = {"buildDirectoryPatterns": ["build"]}
            commands: list[list[str]] = []
            original_run_command = storage.run_command

            def record_build_commands(command: list[str], **kwargs: object) -> subprocess.CompletedProcess[str]:
                if command and command[0] in {"cmake", "make", "ninja"}:
                    commands.append(command.copy())
                return original_run_command(command, **kwargs)

            with mock.patch.object(storage, "run_command", side_effect=record_build_commands):
                candidates, freed, status = storage.handle_cmake(
                    repository,
                    adapter,
                    mode="update",
                    retention_days=7,
                    pressure_mode=True,
                    dirty=False,
                    busy=False,
                )

            self.assertEqual([], commands)
            self.assertFalse(build.exists())
            self.assertFalse((repository / "outside").exists())
            self.assertGreater(freed, 0)
            self.assertEqual("Passed", status)
            self.assertEqual("RemoveVerifiedBuildTree", candidates[0]["action"])
            self.assertEqual("Removed", candidates[0]["status"])

    def test_cc65_cleanup_has_no_make_authorization_path(self) -> None:
        source = pathlib.Path(storage.__file__).read_text(encoding="utf-8")
        self.assertNotIn("validate_cc65_make_preview", source)
        self.assertNotIn("make_preview", source)
        self.assertNotIn("zap", POLICY_PATH.read_text(encoding="utf-8").casefold())

    @unittest.skipUnless(os.name != "nt" and shutil.which("make"), "GNU Make fixture requires a POSIX runner")
    def test_cc65_inventory_and_blocked_update_modes_never_evaluate_makefiles(self) -> None:
        cases = (
            ("safe", "check-only", False, False, "RemoveVerifiedGeneratedTree", "Planned"),
            ("safe", "dry-run", False, False, "RemoveVerifiedGeneratedTree", "Planned"),
            ("deep", "check-only", False, False, "RemoveVerifiedGeneratedTree", "Planned"),
            ("deep", "dry-run", False, False, "RemoveVerifiedGeneratedTree", "Planned"),
            ("safe", "update", True, False, "RemoveVerifiedGeneratedTree", "SkippedDirtyOrBusy"),
            ("deep", "update", False, True, "RemoveVerifiedGeneratedTree", "SkippedDirtyOrBusy"),
        )
        for profile, mode, dirty, busy, action, expected_status in cases:
            with self.subTest(profile=profile, mode=mode, dirty=dirty, busy=busy), \
                    tempfile.TemporaryDirectory() as temporary:
                repository = pathlib.Path(temporary) / "cc65"
                initialize_repository(repository)
                marker = pathlib.Path(temporary) / "makefile-evaluated"
                (repository / ".gitignore").write_text("wrk/\n", encoding="utf-8")
                (repository / "src").mkdir()
                makefile = (
                    'SIDE_EFFECT := $(shell touch "$(HB_STORAGE_MARKER)")\n'
                    ".PHONY: clean\n"
                    "clean:\n\trm -rf wrk\n"
                )
                (repository / "Makefile").write_text(makefile, encoding="utf-8")
                (repository / "src/Makefile").write_text(
                    makefile.replace("rm -rf wrk", "rm -rf ../wrk"),
                    encoding="utf-8",
                )
                run("git", "add", ".gitignore", "Makefile", "src/Makefile", cwd=repository)
                run("git", "commit", "-qm", "fixture", cwd=repository)
                (repository / "wrk").mkdir()
                (repository / "wrk/output.o").write_bytes(b"generated")
                adapter = {
                    "safeGroups": [{
                        "id": "host-tools",
                        "makeDirectory": "src",
                        "target": "clean",
                        "paths": ["wrk"],
                    }],
                    "protectedEvidencePatterns": [],
                }

                with mock.patch.dict(os.environ, {"HB_STORAGE_MARKER": str(marker)}):
                    candidates, _, _, result_status, _ = storage.handle_cc65(
                        repository,
                        adapter,
                        mode=mode,
                        profile=profile,
                        retention_days=7,
                        pressure_mode=True,
                        dirty=dirty,
                        busy=busy,
                    )

                self.assertFalse(marker.exists(), f"{profile}/{mode} evaluated a Makefile")
                self.assertEqual(action, candidates[0]["action"])
                self.assertEqual(expected_status, candidates[0]["status"])
                self.assertEqual("Warning" if mode == "update" else "Passed", result_status)
                self.assertTrue((repository / "wrk").exists())

    @unittest.skipUnless(os.name != "nt" and shutil.which("make"), "GNU Make fixture requires a POSIX runner")
    def test_cc65_authorized_update_directly_removes_verified_tree(self) -> None:
        for profile, make_directory, recipe in (
            ("safe", "src", "rm -rf ../wrk; touch ../outside"),
            ("deep", ".", "rm -rf wrk/../../../outside"),
        ):
            with self.subTest(profile=profile), tempfile.TemporaryDirectory() as temporary:
                repository = pathlib.Path(temporary) / "cc65"
                initialize_repository(repository)
                (repository / ".gitignore").write_text("wrk/\n", encoding="utf-8")
                (repository / "src").mkdir()
                (repository / make_directory / "Makefile").write_text(
                    f".PHONY: clean\nclean:\n\t{recipe}\n",
                    encoding="utf-8",
                )
                tracked_makefile = "Makefile" if make_directory == "." else "src/Makefile"
                run("git", "add", ".gitignore", tracked_makefile, cwd=repository)
                run("git", "commit", "-qm", "fixture", cwd=repository)
                (repository / "wrk").mkdir()
                (repository / "wrk/output.o").write_bytes(b"generated")
                adapter = {
                    "safeGroups": [{
                        "id": "host-tools",
                        "makeDirectory": make_directory,
                        "target": "clean",
                        "paths": ["wrk"],
                    }],
                    "protectedEvidencePatterns": [],
                }

                commands: list[list[str]] = []
                original_run_command = storage.run_command

                def reject_build_commands(command: list[str], **kwargs: object) -> subprocess.CompletedProcess[str]:
                    if command and command[0] in {"make", "cmake"}:
                        commands.append(command.copy())
                    return original_run_command(command, **kwargs)

                with mock.patch.object(storage, "run_command", side_effect=reject_build_commands):
                    candidates, _, freed, status, warnings = storage.handle_cc65(
                        repository,
                        adapter,
                        mode="update",
                        profile=profile,
                        retention_days=7,
                        pressure_mode=True,
                        dirty=False,
                        busy=False,
                    )

                self.assertFalse(repository.joinpath("wrk").exists())
                self.assertFalse(repository.joinpath("outside").exists())
                self.assertEqual([], commands)
                self.assertGreater(freed, 0)
                self.assertEqual("Passed", status)
                self.assertEqual([], warnings)
                self.assertEqual("RemoveVerifiedGeneratedTree", candidates[0]["action"])
                self.assertEqual("Removed", candidates[0]["status"])

    @unittest.skipUnless(os.name != "nt" and shutil.which("make"), "GNU Make fixture requires a POSIX runner")
    def test_cc65_deep_ignores_repository_make_logic(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            repository = pathlib.Path(temporary) / "cc65"
            initialize_repository(repository)
            (repository / ".gitignore").write_text("wrk/\n", encoding="utf-8")
            (repository / "src").mkdir()
            (repository / "Makefile").write_text(
                ".PHONY: clean\nclean:\n\trm -rf unexpected\n",
                encoding="utf-8",
            )
            (repository / "src/Makefile").write_text(
                ".PHONY: clean\nclean:\n\trm -rf ../wrk\n",
                encoding="utf-8",
            )
            run("git", "add", ".gitignore", "Makefile", "src/Makefile", cwd=repository)
            run("git", "commit", "-qm", "fixture", cwd=repository)
            (repository / "wrk").mkdir()
            (repository / "wrk/output.o").write_bytes(b"generated")
            adapter = {
                "safeGroups": [{
                    "id": "host-tools",
                    "makeDirectory": "src",
                    "target": "clean",
                    "paths": ["wrk"],
                }],
                "protectedEvidencePatterns": [],
            }
            make_commands: list[list[str]] = []
            original_run_command = storage.run_command

            def record_make_commands(command: list[str], **kwargs: object) -> subprocess.CompletedProcess[str]:
                if command and command[0] == "make":
                    make_commands.append(command.copy())
                return original_run_command(command, **kwargs)

            with mock.patch.object(storage, "run_command", side_effect=record_make_commands):
                candidates, _, freed, status, warnings = storage.handle_cc65(
                    repository,
                    adapter,
                    mode="update",
                    profile="deep",
                    retention_days=7,
                    pressure_mode=True,
                    dirty=False,
                    busy=False,
                )

            self.assertEqual([], make_commands)
            self.assertFalse(repository.joinpath("wrk").exists())
            self.assertGreater(freed, 0)
            self.assertEqual("Passed", status)
            self.assertEqual([], warnings)
            self.assertEqual("RemoveVerifiedGeneratedTree", candidates[0]["action"])
            self.assertEqual("Removed", candidates[0]["status"])

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
