#!/usr/bin/env python3
"""Check the paired maintenance-TUI wrapper contract without real maintenance."""

from __future__ import annotations

from pathlib import Path
import json
import os
import shutil
import subprocess
import tempfile
import unittest
import uuid


REPOSITORY = Path(__file__).resolve().parents[2]
BASH_WRAPPER = REPOSITORY / "scripts" / "maintain-agentic-workspace.sh"
POWERSHELL_WRAPPER = REPOSITORY / "scripts" / "maintain-agentic-workspace.ps1"
FLEET_ENGINE = REPOSITORY / "scripts" / "lib" / "agentic_workspace_fleet.py"


class MaintenanceTuiWrapperTests(unittest.TestCase):
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
