#!/usr/bin/env python3
"""Check the paired maintenance-TUI wrapper contract without real maintenance."""

from __future__ import annotations

from pathlib import Path
import json
import os
import shlex
import shutil
import subprocess
import tempfile
import unittest
import uuid


REPOSITORY = Path(__file__).resolve().parents[2]
BASH_WRAPPER = REPOSITORY / "scripts" / "maintain-agentic-workspace.sh"
POWERSHELL_WRAPPER = REPOSITORY / "scripts" / "maintain-agentic-workspace.ps1"
FLEET_ENGINE = REPOSITORY / "scripts" / "lib" / "agentic_workspace_fleet.py"


def extract_bash_function(name: str) -> str:
    """Return one complete top-level Bash function from the real wrapper."""
    lines = BASH_WRAPPER.read_text(encoding="utf-8").splitlines()
    signature = f"{name}() {{"
    try:
        start = lines.index(signature)
    except ValueError as exception:
        raise AssertionError(f"Bash function not found: {name}") from exception
    for end in range(start + 1, len(lines)):
        if lines[end] == "}":
            return "\n".join(lines[start : end + 1]) + "\n"
    raise AssertionError(f"Bash function is incomplete: {name}")


class MaintenanceTuiWrapperTests(unittest.TestCase):
    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_bash_emit_event_persists_object_details_without_sequence_gap(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            event_stream = Path(directory) / "events.jsonl"
            run_id = str(uuid.uuid4())
            script = "\n".join(
                (
                    "set -euo pipefail",
                    f"FLEET_ENGINE={shlex.quote(str(FLEET_ENGINE))}",
                    f"EVENT_STREAM={shlex.quote(str(event_stream))}",
                    f"RUN_ID={shlex.quote(run_id)}",
                    "EVENT_SEQUENCE=0",
                    "warn() { printf 'WARN: %s\\n' \"$*\" >&2; }",
                    extract_bash_function("emit_event"),
                    "emit_event run-started RUNNING '' '' 'Start.' 'Started.' '{\"mode\":\"check-only\"}'",
                    "emit_event phase-completed PASSED fleet '' 'Fertig.' 'Done.' '{\"exitCode\":0}'",
                    "emit_event run-completed PASSED final '' 'Abschluss.' 'Completed.' "
                    "'{\"reportPath\":\"report.json\",\"logPath\":\"run.log\",\"overallStatus\":\"PASSED\",\"exitCode\":0}'",
                )
            )

            completed = subprocess.run(
                ["bash", "-c", script],
                text=True,
                capture_output=True,
                check=False,
            )

            self.assertEqual(completed.returncode, 0, completed.stderr)
            self.assertTrue(event_stream.exists(), completed.stderr)
            events = [json.loads(line) for line in event_stream.read_text(encoding="utf-8").splitlines()]
            self.assertEqual([event["sequence"] for event in events], [1, 2, 3])
            self.assertEqual(events[0]["details"], {"mode": "check-only"})
            self.assertEqual(events[-1]["eventType"], "run-completed")
            self.assertEqual(events[-1]["details"]["logPath"], "run.log")

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_bash_failed_event_does_not_consume_sequence(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            event_stream = Path(directory) / "events.jsonl"
            script = "\n".join(
                (
                    "set -euo pipefail",
                    f"FLEET_ENGINE={shlex.quote(str(FLEET_ENGINE))}",
                    f"EVENT_STREAM={shlex.quote(str(event_stream))}",
                    f"RUN_ID={shlex.quote(str(uuid.uuid4()))}",
                    "EVENT_SEQUENCE=0",
                    "warn() { printf 'WARN: %s\\n' \"$*\" >&2; }",
                    extract_bash_function("emit_event"),
                    "emit_event finding WARNING fleet target 'Ungültig.' 'Invalid.' '[]'",
                    "emit_event phase-completed PASSED fleet '' 'Fertig.' 'Done.' '{\"exitCode\":0}'",
                )
            )

            completed = subprocess.run(
                ["bash", "-c", script],
                text=True,
                capture_output=True,
                check=False,
            )

            self.assertEqual(completed.returncode, 0, completed.stderr)
            events = [json.loads(line) for line in event_stream.read_text(encoding="utf-8").splitlines()]
            self.assertEqual(len(events), 1)
            self.assertEqual(events[0]["sequence"], 1)
            self.assertIn("event could not be written", completed.stderr)

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_completion_details_include_log_path(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            report = root / "report.json"
            report.write_text(
                json.dumps(
                    {
                        "overallStatus": "PASSED",
                        "artifacts": {"logPath": str(root / "run.log")},
                    }
                ),
                encoding="utf-8",
            )
            script = "\n".join(
                (
                    "set -euo pipefail",
                    extract_bash_function("completion_event_details"),
                    f"completion_event_details {shlex.quote(str(report))} 0 PASSED",
                )
            )

            completed = subprocess.run(
                ["bash", "-c", script],
                text=True,
                capture_output=True,
                check=False,
            )

            self.assertEqual(completed.returncode, 0, completed.stderr)
            details = json.loads(completed.stdout)
            self.assertEqual(details["reportPath"], str(report))
            self.assertEqual(details["logPath"], str(root / "run.log"))
            self.assertEqual(details["overallStatus"], "PASSED")
            self.assertEqual(details["exitCode"], 0)

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_completion_details_survive_missing_or_invalid_report(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            cases = (
                (root / "missing.json", False),
                (root / "invalid.json", True),
            )
            for report, create_invalid_report in cases:
                with self.subTest(report=report.name):
                    if create_invalid_report:
                        report.write_text("not-json\n", encoding="utf-8")
                    script = "\n".join(
                        (
                            "set -euo pipefail",
                            extract_bash_function("completion_event_details"),
                            f"completion_event_details {shlex.quote(str(report))} 17 FAILED",
                        )
                    )

                    completed = subprocess.run(
                        ["bash", "-c", script],
                        text=True,
                        capture_output=True,
                        check=False,
                    )

                    self.assertEqual(completed.returncode, 0, completed.stderr)
                    details = json.loads(completed.stdout)
                    self.assertEqual(details["reportPath"], str(report))
                    self.assertEqual(details["logPath"], "N/A")
                    self.assertEqual(details["overallStatus"], "FAILED")
                    self.assertEqual(details["exitCode"], 17)

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_home_runtime_delegates_zero_one_and_many_arguments_under_bin_bash(self) -> None:
        cases = (
            (),
            ("--check-only",),
            ("--manifest", ""),
            ("--manifest", "value with spaces;*.md'\"double"),
            ("--check-only", "--scripts-only", "--manifest", "fixture value.json"),
        )
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            home = root / "home"
            runtime_scripts = home / "scripts"
            runtime_lib = runtime_scripts / "lib"
            source = root / "source"
            source_scripts = source / "scripts"
            runtime_lib.mkdir(parents=True)
            source_scripts.mkdir(parents=True)
            shutil.copy2(BASH_WRAPPER, runtime_scripts / BASH_WRAPPER.name)
            shutil.copy2(
                REPOSITORY / "scripts" / "lib" / "resolve-home-baseline-source.sh",
                runtime_lib / "resolve-home-baseline-source.sh",
            )
            (runtime_lib / "agentic_workspace_fleet.py").write_text(
                "#!/usr/bin/env python3\n",
                encoding="utf-8",
            )
            (source_scripts / "sync-home.sh").write_text("#!/usr/bin/env bash\nexit 0\n", encoding="utf-8")
            (source_scripts / BASH_WRAPPER.name).write_text(
                """#!/usr/bin/env bash
set -euo pipefail
python3 - "$HB_CAPTURE" "$@" <<'PY'
import json
import pathlib
import sys
path = pathlib.Path(sys.argv[1])
rows = json.loads(path.read_text(encoding="utf-8")) if path.exists() else []
rows.append(sys.argv[2:])
path.write_text(json.dumps(rows), encoding="utf-8")
PY
""",
                encoding="utf-8",
            )
            subprocess.run(["git", "init", "-q", str(source)], check=True)
            subprocess.run(
                ["git", "-C", str(source), "remote", "add", "origin", str(root / "remote.git")],
                check=True,
            )
            capture = root / "capture.json"
            environment = os.environ.copy()
            environment.update(
                {
                    "HOME": str(home),
                    "HOME_BASELINE_SOURCE": str(source),
                    "HB_CAPTURE": str(capture),
                }
            )

            for arguments in cases:
                with self.subTest(arguments=arguments):
                    completed = subprocess.run(
                        ["/bin/bash", str(runtime_scripts / BASH_WRAPPER.name), *arguments],
                        text=True,
                        capture_output=True,
                        env=environment,
                        check=False,
                    )
                    self.assertEqual(completed.returncode, 0, completed.stdout + completed.stderr)

            self.assertEqual(json.loads(capture.read_text(encoding="utf-8")), [list(case) for case in cases])

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_bash_help_exposes_all_ui_selectors(self) -> None:
        result = subprocess.run(
            ["bash", str(BASH_WRAPPER), "--help"],
            text=True,
            capture_output=True,
            check=False,
        )
        self.assertEqual(result.returncode, 0, result.stderr)
        for selector in ("--tui", "--plain-ui", "--no-tui"):
            self.assertIn(selector, result.stdout)

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_bash_rejects_conflicting_ui_selectors_before_engine_start(self) -> None:
        for selectors in (
            ("--tui", "--plain-ui"),
            ("--tui", "--no-tui"),
            ("--plain-ui", "--no-tui"),
            ("--tui", "--plain-ui", "--no-tui"),
        ):
            with self.subTest(selectors=selectors):
                result = subprocess.run(
                    ["bash", str(BASH_WRAPPER), *selectors],
                    text=True,
                    capture_output=True,
                    check=False,
                )
                self.assertEqual(result.returncode, 2, result.stdout + result.stderr)

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_bash_unsupported_terminal_falls_back_and_cancels_before_engine(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            result = subprocess.run(
                [
                    "bash",
                    str(BASH_WRAPPER),
                    "--tui",
                    "--home-dir",
                    directory,
                ],
                input="3\nn\nn\nn\nn\n",
                text=True,
                capture_output=True,
                env={
                    "HOME": directory,
                    "PATH": str(Path("/usr/bin")) + ":" + str(Path("/bin")),
                    "TERM": "dumb",
                },
                check=False,
            )
            self.assertEqual(result.returncode, 130, result.stdout + result.stderr)
            self.assertIn("lineare Ausgabe", result.stdout + result.stderr)
            self.assertIn("Cancelled before engine start", result.stdout)

    @unittest.skipIf(os.name == "nt", "The Bash wrapper runs on Unix targets.")
    def test_bash_rejects_ui_with_preselected_maintenance_mode(self) -> None:
        cases = (
            ("--check-only",),
            ("--dry-run",),
            ("--scripts-only",),
            ("--repair-drift",),
            ("--include-optional",),
            ("--allow-admin-prompts",),
            ("--manifest", "/tmp/fixture.json"),
        )
        for selector in ("--tui", "--plain-ui"):
            for maintenance_option in cases:
                with self.subTest(
                    selector=selector,
                    maintenance_option=maintenance_option,
                ):
                    result = subprocess.run(
                        [
                            "bash",
                            str(BASH_WRAPPER),
                            selector,
                            *maintenance_option,
                        ],
                        text=True,
                        capture_output=True,
                        check=False,
                    )
                    self.assertEqual(
                        result.returncode,
                        2,
                        result.stdout + result.stderr,
                    )

    def test_no_argument_tui_contract_is_tty_gated_in_both_wrappers(self) -> None:
        bash_source = BASH_WRAPPER.read_text(encoding="utf-8")
        powershell_source = POWERSHELL_WRAPPER.read_text(encoding="utf-8")

        self.assertIn(
            '[ "$UI_MODE" = "auto" ] && [ "$ARGUMENT_COUNT" -eq 0 ] '
            "&& [ -t 0 ] && [ -t 1 ]",
            bash_source,
        )
        self.assertIn("$implicitInteractiveUi", powershell_source)
        self.assertIn("-not [Console]::IsInputRedirected", powershell_source)
        self.assertIn("-not [Console]::IsOutputRedirected", powershell_source)

    def test_powershell_declares_equivalent_ui_and_event_parameters(self) -> None:
        source = POWERSHELL_WRAPPER.read_text(encoding="utf-8")
        for parameter in ("$Tui", "$PlainUi", "$NoTui", "$EventStream", "$RunId"):
            self.assertIn(parameter, source)

    @unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is required.")
    def test_powershell_rejects_ui_with_preselected_maintenance_mode(self) -> None:
        for selector in ("-Tui", "-PlainUi"):
            for maintenance_option in (
                "-CheckOnly",
                "-WhatIf",
                "-ScriptsOnly",
                "-RepairDrift",
                "-IncludeOptional",
                "-AllowAdminPrompts",
            ):
                with self.subTest(
                    selector=selector,
                    maintenance_option=maintenance_option,
                ):
                    result = subprocess.run(
                        [
                            "pwsh",
                            "-NoProfile",
                            "-File",
                            str(POWERSHELL_WRAPPER),
                            selector,
                            maintenance_option,
                        ],
                        text=True,
                        capture_output=True,
                        check=False,
                    )
                    self.assertEqual(
                        result.returncode,
                        2,
                        result.stdout + result.stderr,
                    )

    @unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is required.")
    def test_powershell_plain_update_defaults_to_no_before_engine_start(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            result = subprocess.run(
                [
                    "pwsh",
                    "-NoProfile",
                    "-File",
                    str(POWERSHELL_WRAPPER),
                    "-PlainUi",
                    "-HomeDir",
                    directory,
                ],
                input="3\nn\nn\nn\nn\n",
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(result.returncode, 130, result.stdout + result.stderr)
            self.assertIn(
                "Cancelled before engine start",
                result.stdout + result.stderr,
            )

    def test_internal_event_parameters_are_not_documented_as_user_authority(self) -> None:
        bash_source = BASH_WRAPPER.read_text(encoding="utf-8")
        powershell_source = POWERSHELL_WRAPPER.read_text(encoding="utf-8")
        self.assertIn("--event-stream", bash_source)
        self.assertIn("$EventStream", powershell_source)
        self.assertNotIn("administrator authority", bash_source.lower())

    def test_event_writer_appends_one_complete_private_record(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            event_stream = Path(directory) / "events.jsonl"
            run_id = str(uuid.uuid4())
            result = subprocess.run(
                [
                    "python3",
                    str(FLEET_ENGINE),
                    "event",
                    "--event-stream",
                    str(event_stream),
                    "--run-id",
                    run_id,
                    "--sequence",
                    "1",
                    "--event-type",
                    "run-started",
                    "--status",
                    "RUNNING",
                    "--message-de",
                    "Wartung gestartet.",
                    "--message-en",
                    "Maintenance started.",
                    "--details-json",
                    '{"mode":"dry-run"}',
                ],
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(result.returncode, 0, result.stderr)
            raw = event_stream.read_bytes()
            self.assertTrue(raw.endswith(b"\n"))
            self.assertEqual(raw.count(b"\n"), 1)
            event = json.loads(raw.decode("utf-8"))
            self.assertEqual(event["schemaVersion"], 1)
            self.assertEqual(event["runId"], run_id)
            self.assertEqual(event["sequence"], 1)
            self.assertEqual(event["details"]["mode"], "dry-run")
            if os.name != "nt":
                self.assertEqual(event_stream.stat().st_mode & 0o077, 0)

    def test_event_writer_rejects_non_object_details(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            result = subprocess.run(
                [
                    "python3",
                    str(FLEET_ENGINE),
                    "event",
                    "--event-stream",
                    str(Path(directory) / "events.jsonl"),
                    "--run-id",
                    str(uuid.uuid4()),
                    "--sequence",
                    "1",
                    "--event-type",
                    "finding",
                    "--status",
                    "WARNING",
                    "--message-de",
                    "Befund.",
                    "--message-en",
                    "Finding.",
                    "--details-json",
                    "[]",
                ],
                text=True,
                capture_output=True,
                check=False,
            )
            self.assertEqual(result.returncode, 2)

    def test_bash_and_powershell_keep_phase_start_before_phase_work(self) -> None:
        bash_source = BASH_WRAPPER.read_text(encoding="utf-8")
        powershell_source = POWERSHELL_WRAPPER.read_text(encoding="utf-8")
        self.assertLess(
            bash_source.index('start_event_phase "registry"'),
            bash_source.index("ensure_registry || true"),
        )
        self.assertLess(
            bash_source.index('start_event_phase "toolchain"'),
            bash_source.index('"${maintenance[@]}" || toolchain_status=$?'),
        )
        self.assertLess(
            powershell_source.index("Start-HBMaintenanceEventPhase -PhaseId 'registry'"),
            powershell_source.index("Test-HBRegistry", powershell_source.index("$fleetStatus")),
        )
        self.assertLess(
            powershell_source.index("Start-HBMaintenanceEventPhase -PhaseId 'toolchain'"),
            powershell_source.index("& $maintenance @parameters"),
        )

    def test_cache_bootstrap_is_locked_atomic_and_fails_to_plain_ui(self) -> None:
        bash_source = BASH_WRAPPER.read_text(encoding="utf-8")
        powershell_source = POWERSHELL_WRAPPER.read_text(encoding="utf-8")
        test_lock = (
            "scripts/lib/maintenance-tui/tests/"
            "HomeBaseline.MaintenanceTui.Tests/packages.lock.json"
        )
        for token in (
            "command -v dotnet",
            "--locked-mode",
            "dotnet publish",
            ".build.",
            "mktemp -d",
            "mv --",
            test_lock,
            "using plain output",
            "TUI-Build fehlgeschlagen",
            "TUI-Cache konnte nicht atomar veröffentlicht werden",
        ):
            self.assertIn(token, bash_source)
        for token in (
            "Get-Command dotnet",
            "--locked-mode",
            "dotnet publish",
            ".build.",
            "Move-Item",
            test_lock,
            "lineare Ausgabe",
            "TUI-Build fehlgeschlagen",
            "TUI-Cache konnte nicht atomar veröffentlicht werden",
        ):
            self.assertIn(token, powershell_source)

    @unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is required.")
    def test_powershell_help_exposes_all_ui_selectors(self) -> None:
        result = subprocess.run(
            [
                "pwsh",
                "-NoProfile",
                "-Command",
                f"Get-Help '{POWERSHELL_WRAPPER}' -Full | Out-String",
            ],
            text=True,
            capture_output=True,
            check=False,
        )
        self.assertEqual(result.returncode, 0, result.stderr)
        for selector in ("-Tui", "-PlainUi", "-NoTui"):
            self.assertIn(selector, result.stdout)


if __name__ == "__main__":
    unittest.main()
