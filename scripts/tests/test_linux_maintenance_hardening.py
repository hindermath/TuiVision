#!/usr/bin/env python3
"""Exercise Linux maintenance contracts without machine or package mutation."""

from __future__ import annotations

import hashlib
import json
import os
from pathlib import Path
import platform
import shlex
import signal
import stat
import subprocess
import tarfile
import tempfile
import time
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]
BREW_MAINTAINER = REPOSITORY / "scripts" / "maintain-agentic-brew-apps.sh"
WORKSPACE_MAINTAINER = REPOSITORY / "scripts" / "maintain-agentic-workspace.sh"
HELPER = REPOSITORY / "scripts" / "lib" / "linux-maintenance-hardening.py"
FLEET_ENGINE = REPOSITORY / "scripts" / "lib" / "agentic_workspace_fleet.py"


def write_json(path: Path, value: object) -> None:
    path.write_text(
        json.dumps(value, ensure_ascii=False, indent=2) + "\n",
        encoding="utf-8",
    )


def write_executable(path: Path, source: str) -> None:
    path.write_text(source, encoding="utf-8")
    path.chmod(path.stat().st_mode | stat.S_IXUSR)


def empty_registries(root: Path) -> dict[str, Path]:
    paths = {
        "brew": root / "brew.json",
        "vscode": root / "vscode.json",
        "cli": root / "cli.json",
        "npm": root / "npm.json",
        "powershell": root / "powershell.json",
    }
    write_json(
        paths["brew"],
        {"schemaVersion": 1, "formulae": [], "casks": [], "aptFallback": {"packages": []}},
    )
    write_json(paths["vscode"], {"schemaVersion": 1, "extensions": []})
    write_json(paths["cli"], {"schemaVersion": 1, "tools": []})
    write_json(paths["npm"], {"schemaVersion": 1, "tools": []})
    write_json(paths["powershell"], {"schemaVersion": 1, "modules": []})
    return paths


def install_fake_brew(root: Path) -> tuple[Path, Path, Path]:
    bin_dir = root / "bin"
    bin_dir.mkdir()
    state = root / "brew-state"
    log = root / "brew-install-log"
    state.write_text("", encoding="utf-8")
    log.write_text("", encoding="utf-8")
    write_executable(
        bin_dir / "brew",
        """#!/usr/bin/env bash
set -euo pipefail
state="${HB_TEST_BREW_STATE:?}"
log="${HB_TEST_BREW_LOG:?}"
case "${1:-}" in
  list)
    if [ "${2:-}" = "--formula" ] && [ "${3:-}" = "--versions" ]; then
      grep -Fxq "${4:-}" "$state"
    elif [ "${2:-}" = "--formula" ] && [ "${3:-}" = "--full-name" ]; then
      sort -u "$state"
    elif [ "${2:-}" = "--cask" ]; then
      exit 0
    else
      exit 2
    fi
    ;;
  info)
    printf '{"formulae":[]}\n'
    ;;
  install)
    item="${2:-}"
    if [ ! -s "$log" ]; then
      cat >/dev/null
    fi
    printf '%s\n' "$item" >> "$log"
    printf '%s\n' "$item" >> "$state"
    ;;
  --prefix)
    printf '%s\n' "${HB_TEST_BREW_PREFIX:-/fixture/brew}"
    ;;
  update|upgrade|link)
    ;;
  *)
    exit 2
    ;;
esac
""",
    )
    return bin_dir, state, log


def run_brew_maintainer(
    root: Path,
    paths: dict[str, Path],
    *,
    extra: tuple[str, ...] = (),
    environment_updates: dict[str, str] | None = None,
) -> subprocess.CompletedProcess[str]:
    result_file = root / "toolchain-result.json"
    environment = os.environ.copy()
    for variable in (
        "SWIFTLY_HOME_DIR",
        "SWIFTLY_BIN_DIR",
        "SWIFTLY_TOOLCHAINS_DIR",
    ):
        environment.pop(variable, None)
    environment.update(
        {
            "PATH": f"{root / 'bin'}{os.pathsep}{environment['PATH']}",
            "HOME": str(root / "home"),
            "HB_TEST_BREW_STATE": str(root / "brew-state"),
            "HB_TEST_BREW_LOG": str(root / "brew-install-log"),
        }
    )
    if environment_updates:
        environment.update(environment_updates)
    return subprocess.run(
        [
            "bash",
            str(BREW_MAINTAINER),
            "--registry",
            str(paths["brew"]),
            "--vscode-registry",
            str(paths["vscode"]),
            "--cli-registry",
            str(paths["cli"]),
            "--npm-agent-registry",
            str(paths["npm"]),
            "--powershell-module-registry",
            str(paths["powershell"]),
            "--skip-upgrade",
            "--skip-vscode-extensions",
            "--result-file",
            str(result_file),
            *extra,
        ],
        cwd=REPOSITORY,
        env=environment,
        text=True,
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        check=False,
        timeout=20,
    )


def install_fake_swiftly_archive(root: Path) -> tuple[Path, Path]:
    install_log = root / "swift-install-log"
    install_log.write_text("", encoding="utf-8")
    swiftly = root / "swiftly-fixture"
    write_executable(
        swiftly,
        """#!/usr/bin/env bash
set -euo pipefail
swift_home="${SWIFTLY_HOME_DIR:-$HOME/.local/share/swiftly}"
swift_bin="${SWIFTLY_BIN_DIR:-$HOME/.local/bin}"
case "${1:-}" in
  init)
    mkdir -p "$swift_home" "$swift_bin"
    cp "$0" "$swift_bin/swiftly"
    chmod +x "$swift_bin/swiftly"
    printf 'export PATH="%s:$PATH"\\n' "$swift_bin" > "$swift_home/env.sh"
    ;;
  use)
    [ -x "$swift_bin/swift" ] || exit 1
    ;;
  install)
    printf '%s\\n' "${2:-missing}" >> "${HB_TEST_SWIFT_LOG:?}"
    printf '#!/usr/bin/env bash\\nprintf "Swift version 6.3.3 (fixture)\\\\n"\\n' > "$swift_bin/swift"
    chmod +x "$swift_bin/swift"
    previous=""
    for argument in "$@"; do
      if [ "$previous" = "--post-install-file" ]; then
        : > "$argument"
      fi
      previous="$argument"
    done
    ;;
  *)
    exit 2
    ;;
esac
""",
    )
    archive = root / "swiftly-1.1.2-x86_64.tar.gz"
    with tarfile.open(archive, mode="w:gz") as bundle:
        bundle.add(swiftly, arcname="swiftly")
    return archive, install_log


def write_swift_registry(path: Path, digest: str) -> None:
    write_json(
        path,
        {
            "schemaVersion": 1,
            "tools": [
                {
                    "id": "swift",
                    "scope": "required",
                    "platforms": ["Linux"],
                    "command": "swift",
                    "args": ["--version"],
                    "install": {
                        "manager": "swiftly",
                        "linux": {
                            "swiftlyVersion": "1.1.2",
                            "swiftVersion": "6.3.3",
                            "profiles": {
                                "ubuntu:22.04": "ubuntu2204",
                                "ubuntu:24.04": "ubuntu2404",
                            },
                            "artifacts": {
                                "x86_64": {
                                    "url": "https://download.swift.org/swiftly/linux/swiftly-1.1.2-x86_64.tar.gz",
                                    "sha256": digest,
                                }
                            },
                        },
                    },
                }
            ],
        },
    )


def run_helper(*arguments: str, timeout: float = 10) -> subprocess.CompletedProcess[str]:
    return subprocess.run(
        ["python3", str(HELPER), *arguments],
        cwd=REPOSITORY,
        text=True,
        stdout=subprocess.PIPE,
        stderr=subprocess.STDOUT,
        check=False,
        timeout=timeout,
    )


def base_report(run_id: str) -> dict:
    return {
        "schemaVersion": "1.0",
        "runId": run_id,
        "platform": "linux",
        "mode": "update",
        "startedAt": "2026-07-28T00:00:00Z",
        "completedAt": "2026-07-28T00:00:01Z",
        "overallStatus": "SUCCESS",
        "exitCode": 0,
        "stages": [
            {
                "stageId": "fleet",
                "status": "Passed",
                "exitCode": 0,
                "durationMs": 1,
                "summary": "ok",
                "nextAction": "N/A",
            }
        ],
        "targets": [],
        "toolchain": [],
        "findings": [],
        "counts": {"targets": 0, "passed": 0, "warnings": 0, "blocked": 0, "failed": 0},
        "artifacts": {"logPath": "fixture.log", "reportPath": "fixture.json"},
    }


@unittest.skipIf(os.name == "nt", "Linux maintenance fixtures require a POSIX host.")
class LinuxMaintenanceHardeningTests(unittest.TestCase):
    def test_empty_optional_module_probe_still_publishes_one_valid_result(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            paths = empty_registries(root)
            bin_dir, _, _ = install_fake_brew(root)
            write_executable(bin_dir / "pwsh", "#!/usr/bin/env bash\nexit 1\n")

            completed = run_brew_maintainer(root, paths)

            self.assertEqual(completed.returncode, 0, completed.stdout)
            self.assertNotIn("Traceback", completed.stdout)
            result_path = root / "toolchain-result.json"
            inspected = run_helper("inspect-result", "--input", str(result_path))
            self.assertEqual(inspected.returncode, 0, inspected.stdout)
            result = json.loads(result_path.read_text(encoding="utf-8"))
            self.assertEqual(result["overallStatus"], "SUCCESS")

    def test_toolchain_result_contract_classifies_invalid_inputs(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            cases = {
                "missing.json": (None, "ResultMissing"),
                "empty.json": (b"", "ResultEmpty"),
                "truncated.json": (b'{"schemaVersion":"1.0"', "ResultTruncated"),
                "malformed.json": (b'{"schemaVersion":}', "ResultMalformed"),
                "utf8.json": (b"\xff\xfe", "ResultInvalidUtf8"),
                "schema.json": (b'{"schemaVersion":"9.9"}\n', "ResultSchemaMismatch"),
            }
            for name, (payload, failure_class) in cases.items():
                with self.subTest(name=name):
                    path = root / name
                    if payload is not None:
                        path.write_bytes(payload)
                    completed = run_helper("inspect-result", "--input", str(path))
                    self.assertEqual(completed.returncode, 2, completed.stdout)
                    output = json.loads(completed.stdout)
                    self.assertEqual(output["failureClass"], failure_class)
                    self.assertNotIn(str(root), completed.stdout)

    def test_failure_result_is_atomic_and_valid(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            result_path = root / "toolchain.json"
            completed = run_helper(
                "failure-result",
                "--output", str(result_path),
                "--platform", "Darwin",
                "--mode", "compare-only",
                "--failure-class", "ProducerExitedBeforeResult",
            )
            self.assertEqual(completed.returncode, 2, completed.stdout)
            inspected = run_helper("inspect-result", "--input", str(result_path))
            self.assertEqual(inspected.returncode, 0, inspected.stdout)
            result = json.loads(result_path.read_text(encoding="utf-8"))
            self.assertEqual(result["overallStatus"], "FAILED")
            self.assertEqual(result["exitCode"], 2)
            self.assertEqual(result["failureClass"], "ProducerExitedBeforeResult")
            self.assertEqual(list(root.glob(".*.tmp")), [])

    def test_stdin_consuming_brew_processes_all_formulae_once(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            paths = empty_registries(root)
            _, state, install_log = install_fake_brew(root)
            write_json(
                paths["brew"],
                {
                    "schemaVersion": 1,
                    "formulae": [
                        {"name": name, "scope": "required"}
                        for name in ("alpha", "beta", "gamma")
                    ],
                    "casks": [],
                    "aptFallback": {"packages": []},
                },
            )

            first = run_brew_maintainer(root, paths)
            self.assertEqual(first.returncode, 0, first.stdout)
            self.assertEqual(
                install_log.read_text(encoding="utf-8").splitlines(),
                ["alpha", "beta", "gamma"],
            )
            result = json.loads((root / "toolchain-result.json").read_text(encoding="utf-8"))
            self.assertEqual(
                [(item["itemId"], item["finalStatus"]) for item in result["items"]],
                [("alpha", "Installed"), ("beta", "Installed"), ("gamma", "Installed")],
            )

            second = run_brew_maintainer(root, paths)
            self.assertEqual(second.returncode, 0, second.stdout)
            self.assertEqual(
                install_log.read_text(encoding="utf-8").splitlines(),
                ["alpha", "beta", "gamma"],
            )
            self.assertEqual(
                sorted(state.read_text(encoding="utf-8").splitlines()),
                ["alpha", "beta", "gamma"],
            )

    def test_required_cli_drift_fails_but_optional_only_does_not(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            paths = empty_registries(root)
            install_fake_brew(root)
            write_json(
                paths["cli"],
                {
                    "schemaVersion": 1,
                    "tools": [
                        {
                            "id": "required-missing",
                            "scope": "required",
                            "platforms": [platform.system()],
                            "command": "required-missing",
                            "args": ["--version"],
                        },
                        {
                            "id": "optional-missing",
                            "scope": "optional",
                            "platforms": [platform.system()],
                            "command": "optional-missing",
                            "args": ["--version"],
                        },
                    ],
                },
            )
            required = run_brew_maintainer(root, paths)
            self.assertEqual(required.returncode, 1, required.stdout)
            self.assertIn("missing_on_machine.required.cli_tools", required.stdout)
            self.assertIn("required-missing", required.stdout)

            write_json(
                paths["cli"],
                {
                    "schemaVersion": 1,
                    "tools": [
                        {
                            "id": "optional-missing",
                            "scope": "optional",
                            "platforms": [platform.system()],
                            "command": "optional-missing",
                            "args": ["--version"],
                        }
                    ],
                },
            )
            optional = run_brew_maintainer(root, paths)
            self.assertEqual(optional.returncode, 0, optional.stdout)
            self.assertIn("optional-missing", optional.stdout)

    def test_probe_classifies_timeout_capability_and_redacts_paths(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            bin_dir = root / "bin"
            bin_dir.mkdir()
            write_executable(
                bin_dir / "unusable",
                f"#!/usr/bin/env bash\nprintf 'failure in {root}/secret\\n' >&2\nexit 7\n",
            )
            write_executable(
                bin_dir / "blocked",
                "#!/usr/bin/env bash\nprintf 'snap-confine lacks cap_dac_override capability\\n' >&2\nexit 1\n",
            )
            write_executable(bin_dir / "hang", "#!/usr/bin/env bash\nsleep 30\n")

            results = {}
            for label in ("unusable", "blocked", "hang"):
                timeout_seconds = "0.2" if label == "hang" else "1"
                completed = run_helper(
                    "probe",
                    "--tool-id",
                    label,
                    "--timeout-seconds",
                    timeout_seconds,
                    "--redact",
                    str(root),
                    "--",
                    str(bin_dir / label),
                    timeout=5,
                )
                self.assertIn(completed.returncode, (0, 1), completed.stdout)
                results[label] = json.loads(completed.stdout)

            self.assertEqual(results["unusable"]["status"], "Unusable")
            self.assertEqual(results["blocked"]["status"], "CapabilityBlocked")
            self.assertEqual(results["hang"]["status"], "TimedOut")
            self.assertTrue(results["hang"]["processTreeCleaned"])
            self.assertNotIn(str(root), results["unusable"]["sanitizedEvidence"])

    def test_swift_contract_accepts_current_matrix_and_rejects_ubuntu_2004(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            registry = root / "cli.json"
            write_json(
                registry,
                {
                    "schemaVersion": 1,
                    "tools": [
                        {
                            "id": "swift",
                            "scope": "required",
                            "platforms": ["Linux"],
                            "command": "swift",
                            "args": ["--version"],
                            "install": {
                                "manager": "swiftly",
                                "linux": {
                                    "swiftlyVersion": "1.1.2",
                                    "swiftVersion": "6.3.3",
                                    "profiles": {
                                        "ubuntu:22.04": "ubuntu2204",
                                        "ubuntu:24.04": "ubuntu2404",
                                    },
                                    "artifacts": {
                                        "x86_64": {
                                            "url": "https://download.swift.org/swiftly/linux/swiftly-1.1.2-x86_64.tar.gz",
                                            "sha256": "1" * 64,
                                        },
                                        "aarch64": {
                                            "url": "https://download.swift.org/swiftly/linux/swiftly-1.1.2-aarch64.tar.gz",
                                            "sha256": "2" * 64,
                                        },
                                    },
                                },
                            },
                        }
                    ],
                },
            )
            os_release = root / "os-release"
            os_release.write_text('ID=ubuntu\nVERSION_ID="24.04"\n', encoding="utf-8")
            supported = run_helper(
                "swift-contract",
                "--registry",
                str(registry),
                "--os-release",
                str(os_release),
                "--architecture",
                "x86_64",
            )
            self.assertEqual(supported.returncode, 0, supported.stdout)
            resolved = json.loads(supported.stdout)
            self.assertEqual(resolved["status"], "Supported")
            self.assertEqual(resolved["swiftVersion"], "6.3.3")
            self.assertEqual(resolved["swiftlyPlatform"], "ubuntu2404")

            os_release.write_text('ID=ubuntu\nVERSION_ID="20.04"\n', encoding="utf-8")
            unsupported = run_helper(
                "swift-contract",
                "--registry",
                str(registry),
                "--os-release",
                str(os_release),
                "--architecture",
                "x86_64",
            )
            self.assertEqual(unsupported.returncode, 1, unsupported.stdout)
            self.assertEqual(json.loads(unsupported.stdout)["status"], "Unsupported")

    def test_checksum_verification_is_fail_closed(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            archive = Path(directory) / "swiftly.tar.gz"
            archive.write_bytes(b"fixture archive")
            digest = hashlib.sha256(archive.read_bytes()).hexdigest()
            accepted = run_helper(
                "verify-sha256",
                "--path",
                str(archive),
                "--expected",
                digest,
            )
            rejected = run_helper(
                "verify-sha256",
                "--path",
                str(archive),
                "--expected",
                "0" * 64,
            )
            self.assertEqual(accepted.returncode, 0, accepted.stdout)
            self.assertEqual(rejected.returncode, 1, rejected.stdout)
            self.assertEqual(json.loads(rejected.stdout)["status"], "IntegrityMismatch")

    @unittest.skipUnless(platform.system() == "Linux", "Linux-only Swift provisioning")
    def test_swiftly_install_is_verified_active_and_idempotent(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            paths = empty_registries(root)
            bin_dir, _, _ = install_fake_brew(root)
            archive, install_log = install_fake_swiftly_archive(root)
            digest = hashlib.sha256(archive.read_bytes()).hexdigest()
            write_swift_registry(paths["cli"], digest)
            write_executable(
                bin_dir / "swift",
                "#!/usr/bin/env bash\nprintf 'old Swift fixture\\n'\nexit 1\n",
            )
            write_executable(
                bin_dir / "curl",
                """#!/usr/bin/env bash
set -euo pipefail
output=""
while [ "$#" -gt 0 ]; do
  if [ "$1" = "-o" ]; then
    output="${2:?}"
    shift
  fi
  shift
done
cp "${HB_TEST_SWIFT_ARCHIVE:?}" "$output"
""",
            )
            environment = {
                "HB_TEST_SWIFT_ARCHIVE": str(archive),
                "HB_TEST_SWIFT_LOG": str(install_log),
            }
            first = run_brew_maintainer(
                root,
                paths,
                extra=("--allow-admin-prompts",),
                environment_updates=environment,
            )
            installed_swift = root / "home" / ".local" / "bin" / "swift"
            diagnostic = (
                installed_swift.read_text(encoding="utf-8")
                if installed_swift.exists()
                else "installed Swift fixture is missing"
            )
            self.assertEqual(first.returncode, 0, first.stdout + "\n" + diagnostic)
            result = json.loads((root / "toolchain-result.json").read_text(encoding="utf-8"))
            swift = next(item for item in result["items"] if item["itemId"] == "swift")
            self.assertEqual(swift["finalStatus"], "Installed")
            self.assertEqual(swift["probeStatus"], "Available")
            self.assertEqual(install_log.read_text(encoding="utf-8").splitlines(), ["6.3.3"])

            second = run_brew_maintainer(
                root,
                paths,
                extra=("--allow-admin-prompts",),
                environment_updates=environment,
            )
            self.assertEqual(second.returncode, 0, second.stdout)
            self.assertEqual(install_log.read_text(encoding="utf-8").splitlines(), ["6.3.3"])

    @unittest.skipUnless(platform.system() == "Linux", "Linux-only Swift provisioning")
    def test_swiftly_bad_hash_and_missing_admin_authority_do_not_execute(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            paths = empty_registries(root)
            bin_dir, _, _ = install_fake_brew(root)
            archive, install_log = install_fake_swiftly_archive(root)
            write_swift_registry(paths["cli"], "0" * 64)
            write_executable(
                bin_dir / "swift",
                "#!/usr/bin/env bash\nprintf 'old Swift fixture\\n'\nexit 1\n",
            )
            write_executable(
                bin_dir / "curl",
                """#!/usr/bin/env bash
set -euo pipefail
while [ "$#" -gt 0 ]; do
  if [ "$1" = "-o" ]; then
    cp "${HB_TEST_SWIFT_ARCHIVE:?}" "${2:?}"
    exit 0
  fi
  shift
done
exit 2
""",
            )
            environment = {
                "HB_TEST_SWIFT_ARCHIVE": str(archive),
                "HB_TEST_SWIFT_LOG": str(install_log),
            }
            denied = run_brew_maintainer(
                root,
                paths,
                environment_updates=environment,
            )
            self.assertEqual(denied.returncode, 1, denied.stdout)
            self.assertIn("DEFERRED_ADMIN_REQUIRED", denied.stdout)
            self.assertEqual(install_log.read_text(encoding="utf-8"), "")

            bad_hash = run_brew_maintainer(
                root,
                paths,
                extra=("--allow-admin-prompts",),
                environment_updates=environment,
            )
            self.assertEqual(bad_hash.returncode, 1, bad_hash.stdout)
            self.assertIn("SHA-256", bad_hash.stdout)
            self.assertEqual(install_log.read_text(encoding="utf-8"), "")

    def test_fleet_finalization_replaces_stale_success_and_is_idempotent(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            report_path = root / "report.json"
            write_json(report_path, base_report("run-fixture"))
            command = [
                "python3",
                str(FLEET_ENGINE),
                "finalize",
                "--report",
                str(report_path),
                "--stage-id",
                "toolchain",
                "--status",
                "Failed",
                "--exit-code",
                "2",
                "--summary",
                "late failure",
                "--next-action",
                "review fixture",
            ]
            first = subprocess.run(command, text=True, capture_output=True, check=False)
            second = subprocess.run(command, text=True, capture_output=True, check=False)
            self.assertEqual(first.returncode, 0, first.stdout + first.stderr)
            self.assertEqual(second.returncode, 0, second.stdout + second.stderr)
            report = json.loads(report_path.read_text(encoding="utf-8"))
            self.assertEqual(report["overallStatus"], "FAILED")
            self.assertEqual(report["exitCode"], 2)
            self.assertEqual(report["lastStage"], "toolchain")
            self.assertTrue(report["finalized"])
            self.assertEqual(
                sum(item["stageId"] == "toolchain" for item in report["stages"]),
                1,
            )
            self.assertEqual(list(root.glob(".*.tmp")), [])

    def test_toolchain_required_drift_is_preserved_in_parent_report(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            report_path = root / "report.json"
            result_path = root / "toolchain.json"
            write_json(report_path, base_report("run-toolchain"))
            write_json(
                result_path,
                {
                    "schemaVersion": "1.0",
                    "platform": "Darwin",
                    "mode": "compare-only",
                    "overallStatus": "PARTIAL",
                    "exitCode": 1,
                    "items": [
                        {
                            "sequence": 1,
                            "kind": "cli",
                            "itemId": "missing-a",
                            "scope": "required",
                            "attempted": False,
                            "finalStatus": "StillMissing",
                            "probeStatus": "Missing",
                            "sanitizedEvidence": "fixture",
                            "nextAction": "install",
                        },
                        {
                            "sequence": 2,
                            "kind": "cli",
                            "itemId": "missing-b",
                            "scope": "required",
                            "attempted": False,
                            "finalStatus": "StillMissing",
                            "probeStatus": "Missing",
                            "sanitizedEvidence": "fixture",
                            "nextAction": "install",
                        },
                    ],
                    "remainingRequired": ["missing-a", "missing-b"],
                    "optionalDrift": [],
                    "nextAction": "resolve",
                },
            )
            completed = subprocess.run(
                [
                    "python3",
                    str(FLEET_ENGINE),
                    "stage",
                    "--report",
                    str(report_path),
                    "--stage-id",
                    "toolchain",
                    "--status",
                    "Blocked",
                    "--exit-code",
                    "1",
                    "--summary",
                    "required drift",
                    "--next-action",
                    "resolve",
                    "--toolchain-results",
                    str(result_path),
                ],
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(completed.returncode, 0, completed.stdout + completed.stderr)
            report = json.loads(report_path.read_text(encoding="utf-8"))
            self.assertEqual(report["overallStatus"], "PARTIAL")
            self.assertEqual(report["exitCode"], 1)
            self.assertEqual(
                report["toolchainResult"]["remainingRequired"],
                ["missing-a", "missing-b"],
            )
            self.assertEqual(
                [item["itemId"] for item in report["toolchain"]],
                ["missing-a", "missing-b"],
            )

    def test_parent_consumer_reports_stable_toolchain_result_failure_classes(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            cases = {
                "missing.json": (None, "ResultMissing"),
                "empty.json": (b"", "ResultEmpty"),
                "truncated.json": (b'{"schemaVersion":"1.0"', "ResultTruncated"),
                "malformed.json": (b'{"schemaVersion":}', "ResultMalformed"),
                "schema.json": (b'{"schemaVersion":"9.9"}\n', "ResultSchemaMismatch"),
            }
            for name, (payload, failure_class) in cases.items():
                with self.subTest(name=name):
                    report_path = root / f"report-{name}"
                    result_path = root / name
                    write_json(report_path, base_report(name))
                    if payload is not None:
                        result_path.write_bytes(payload)
                    completed = subprocess.run(
                        [
                            "python3", str(FLEET_ENGINE), "stage",
                            "--report", str(report_path),
                            "--stage-id", "toolchain",
                            "--status", "Failed",
                            "--exit-code", "2",
                            "--summary", "fixture",
                            "--next-action", "regenerate",
                            "--toolchain-results", str(result_path),
                        ],
                        text=True,
                        capture_output=True,
                        check=False,
                    )
                    self.assertEqual(completed.returncode, 2, completed.stdout)
                    self.assertIn(failure_class, completed.stdout)
                    self.assertNotIn(str(root), completed.stdout)
                    self.assertNotIn("Traceback", completed.stdout + completed.stderr)

    def test_signal_finalization_uses_canonical_exitcodes(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            for signal_name, exit_code in (("INT", 130), ("TERM", 143)):
                report_path = root / f"{signal_name}.json"
                write_json(report_path, base_report(f"run-{signal_name.lower()}"))
                completed = subprocess.run(
                    [
                        "python3",
                        str(FLEET_ENGINE),
                        "finalize",
                        "--report",
                        str(report_path),
                        "--stage-id",
                        "toolchain",
                        "--status",
                        "Interrupted",
                        "--exit-code",
                        str(exit_code),
                        "--signal",
                        signal_name,
                        "--summary",
                        f"signal {signal_name}",
                        "--next-action",
                        "rerun",
                    ],
                    text=True,
                    capture_output=True,
                    check=False,
                )
                self.assertEqual(completed.returncode, 0, completed.stdout + completed.stderr)
                report = json.loads(report_path.read_text(encoding="utf-8"))
                self.assertEqual(report["overallStatus"], "INTERRUPTED")
                self.assertEqual(report["exitCode"], exit_code)
                self.assertEqual(report["signal"], signal_name)

    def test_workspace_signal_and_late_error_paths_finalize_exactly_once(self) -> None:
        source = WORKSPACE_MAINTAINER.read_text(encoding="utf-8")
        start = source.index("completion_event_details() {")
        end = source.index("\nwhile [ $# -gt 0 ]; do", start)
        finalization_functions = source[start:end]
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            cases = (
                ("TERM", signal.SIGTERM, 143, "INTERRUPTED"),
                ("late-error", None, 1, "FAILED"),
            )
            for label, sent_signal, exit_code, overall in cases:
                with self.subTest(label=label):
                    report_path = root / f"{label}.json"
                    ready_path = root / f"{label}.ready"
                    write_json(report_path, base_report(f"run-{label.lower()}"))
                    harness = root / f"{label}.sh"
                    body = (
                        "#!/usr/bin/env bash\n"
                        "set -euo pipefail\n"
                        f"FLEET_ENGINE={shlex.quote(str(FLEET_ENGINE))}\n"
                        f"REPORT_FILE={shlex.quote(str(report_path))}\n"
                        "CURRENT_STAGE=toolchain\n"
                        "FINALIZED=0\n"
                        "LOCK_DIR=''\n"
                        "LOG_FILE=''\n"
                        "release_resources() { :; }\n"
                        "start_event_phase() { :; }\n"
                        "event_status() { printf 'FAILED'; }\n"
                        "emit_event() { :; }\n"
                        f"{finalization_functions}\n"
                        "trap handle_exit EXIT\n"
                        "trap 'handle_signal INT 130' INT\n"
                        "trap 'handle_signal TERM 143' TERM\n"
                    )
                    if sent_signal:
                        body += f": > {shlex.quote(str(ready_path))}\n"
                    body += "while :; do sleep 1; done\n" if sent_signal else "false\n"
                    write_executable(harness, body)
                    process = subprocess.Popen(
                        ["bash", str(harness)],
                        cwd=REPOSITORY,
                        text=True,
                        stdout=subprocess.PIPE,
                        stderr=subprocess.STDOUT,
                        start_new_session=True,
                    )
                    if sent_signal:
                        deadline = time.monotonic() + 5
                        while not ready_path.exists():
                            if process.poll() is not None:
                                stdout, _ = process.communicate(timeout=5)
                                self.fail(
                                    "signal harness exited before becoming ready:\n"
                                    f"{stdout}"
                                )
                            if time.monotonic() >= deadline:
                                try:
                                    os.killpg(process.pid, signal.SIGKILL)
                                except ProcessLookupError:
                                    pass
                                stdout, _ = process.communicate(timeout=5)
                                self.fail(
                                    "signal harness did not become ready within 5 seconds:\n"
                                    f"{stdout}"
                                )
                            time.sleep(0.01)
                        os.killpg(process.pid, sent_signal)
                    try:
                        stdout, _ = process.communicate(timeout=5)
                    except subprocess.TimeoutExpired:
                        try:
                            os.killpg(process.pid, signal.SIGKILL)
                        except ProcessLookupError:
                            pass
                        stdout, _ = process.communicate(timeout=5)
                        self.fail(f"harness did not exit within 5 seconds:\n{stdout}")
                    self.assertEqual(process.returncode, exit_code, stdout)
                    self.assertEqual(stdout.count("ABSCHLUSS / FINAL"), 1, stdout)
                    report = json.loads(report_path.read_text(encoding="utf-8"))
                    self.assertEqual(report["overallStatus"], overall)
                    self.assertEqual(report["exitCode"], exit_code)
                    self.assertEqual(report["lastStage"], "toolchain")
                    self.assertTrue(report["finalized"])
                    self.assertEqual(
                        sum(
                            item["stageId"] == "toolchain"
                            for item in report["stages"]
                        ),
                        1,
                    )

    def test_probe_timeout_is_bounded(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            executable = Path(directory) / "hang"
            write_executable(executable, "#!/usr/bin/env bash\nsleep 30\n")
            started = time.monotonic()
            completed = run_helper(
                "probe",
                "--tool-id",
                "hang",
                "--timeout-seconds",
                "0.2",
                "--",
                str(executable),
                timeout=5,
            )
            self.assertEqual(completed.returncode, 1, completed.stdout)
            self.assertLess(time.monotonic() - started, 3)

    def test_public_scripts_integrate_hardening_contracts(self) -> None:
        brew = BREW_MAINTAINER.read_text(encoding="utf-8")
        workspace = WORKSPACE_MAINTAINER.read_text(encoding="utf-8")
        for token in (
            "--allow-admin-prompts",
            "--result-file",
            "linux-maintenance-hardening.py",
            "DEFERRED_ADMIN_REQUIRED",
        ):
            self.assertIn(token, brew)
        for token in (
            "finalize_run",
            "CURRENT_STAGE",
            "INTERRUPTED",
            "--toolchain-results",
            "lease-recover",
            "lease-create",
            "lease-release",
            "mutationBarrier",
            "--level0-dir",
        ):
            self.assertIn(token, workspace)


if __name__ == "__main__":
    unittest.main()
