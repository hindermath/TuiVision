"""Exercise the Windows npm metadata boundary with real PowerShell parsing."""

from __future__ import annotations

import json
from pathlib import Path
import shutil
import subprocess
import tempfile
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]
MODULE = REPOSITORY / "scripts/lib/windows-maintenance-hardening.psm1"


@unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is required")
class WindowsNpmIntegrityTests(unittest.TestCase):
    def _run_fixture(self, root: Path, integrity: str) -> subprocess.CompletedProcess[str]:
        tarball = root / "openai-codex-fixture.tgz"
        tarball.touch()
        metadata = json.dumps(
            [
                {
                    "name": "@openai/codex",
                    "version": "0.167.0",
                    "integrity": integrity,
                    "filename": tarball.name,
                }
            ],
            indent=2,
        )
        metadata_path = root / "pack.json"
        metadata_path.write_text(metadata, encoding="utf-8")
        marker = root / "install-reached"
        command = """
param($Module, $Metadata, $PackDirectory, $Marker)
Import-Module $Module -Force
try {
    $lines = Get-Content -LiteralPath $Metadata
    $resolved = Resolve-HBNpmPackTarball -PackJson $lines `
        -PackDirectory $PackDirectory -ExpectedPackage '@openai/codex' `
        -ExpectedVersion '0.167.0' `
        -ExpectedIntegrity 'sha512-1FSQgbTIKAdTub5Ln6z36NT4eXDmBRkinfigzycbgn9yLZdJ3UdkQZqr0N7jnl8/D1xxT/Pz3Fl2l7S2dmRaFA=='
    Set-Content -LiteralPath $Marker -Value $resolved
} catch {
    Write-Error $_
    exit 2
}
"""
        fixture = root / "verify-fixture.ps1"
        fixture.write_text(command, encoding="utf-8")
        return subprocess.run(
            [
                "pwsh", "-NoLogo", "-NoProfile", "-File", str(fixture),
                "-Module", str(MODULE), "-Metadata", str(metadata_path),
                "-PackDirectory", str(root), "-Marker", str(marker),
            ],
            cwd=REPOSITORY,
            text=True,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            check=False,
            timeout=20,
        )

    def test_multiline_metadata_accepts_only_verified_local_archive(self) -> None:
        expected = "sha512-1FSQgbTIKAdTub5Ln6z36NT4eXDmBRkinfigzycbgn9yLZdJ3UdkQZqr0N7jnl8/D1xxT/Pz3Fl2l7S2dmRaFA=="
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            completed = self._run_fixture(root, expected)
            self.assertEqual(0, completed.returncode, completed.stdout)
            self.assertEqual(
                str(root / "openai-codex-fixture.tgz"),
                (root / "install-reached").read_text(encoding="utf-8").strip(),
            )

    def test_integrity_mismatch_fails_before_install_boundary(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            completed = self._run_fixture(root, "sha512-wrong")
            self.assertNotEqual(0, completed.returncode, completed.stdout)
            self.assertIn("identity or integrity mismatch", completed.stdout)
            self.assertFalse((root / "install-reached").exists())


if __name__ == "__main__":
    unittest.main()
