"""Cross-platform contract tests for trusted Windows probe selection."""

from __future__ import annotations

import json
import os
from pathlib import Path
import shutil
import stat
import subprocess
import tempfile
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]
MAINTAINER = REPOSITORY / "scripts" / "maintain-agentic-winget-apps.ps1"


@unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is required")
class WindowsMaintenanceRegistryTests(unittest.TestCase):
    def test_alternate_npm_registry_cannot_select_probe_executable(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            binary = root / "bin"
            binary.mkdir()
            marker = root / "sentinel-started"
            sentinel = binary / "sentinel"
            sentinel.write_text(f"#!/usr/bin/env sh\ntouch '{marker}'\n", encoding="utf-8")
            sentinel.chmod(sentinel.stat().st_mode | stat.S_IXUSR)
            winget = binary / "winget"
            winget.write_text("#!/usr/bin/env sh\nexit 0\n", encoding="utf-8")
            winget.chmod(winget.stat().st_mode | stat.S_IXUSR)
            registry = root / "npm.json"
            registry.write_text(
                json.dumps(
                    {
                        "schemaVersion": 1,
                        "tools": [
                            {
                                "id": "codex",
                                "scope": "required",
                                "command": str(sentinel),
                                "args": [],
                            }
                        ],
                    }
                ),
                encoding="utf-8",
            )
            environment = os.environ.copy()
            environment["PATH"] = f"{binary}{os.pathsep}{environment['PATH']}"

            for mode in ("-CompareOnly", "-WhatIf"):
                with self.subTest(mode=mode):
                    completed = subprocess.run(
                        [
                            "pwsh", "-NoProfile", "-File", str(MAINTAINER),
                            "-NpmAgentRegistry", str(registry), mode,
                        ],
                        cwd=REPOSITORY,
                        env=environment,
                        text=True,
                        stdout=subprocess.PIPE,
                        stderr=subprocess.STDOUT,
                        check=False,
                        timeout=20,
                    )
                    self.assertNotEqual(0, completed.returncode, completed.stdout)
                    self.assertIn("Untrusted npm registry field", completed.stdout)
                    self.assertFalse(marker.exists())


if __name__ == "__main__":
    unittest.main()
