"""Regression tests for commit-range secret scanning in the pre-push hook."""

from __future__ import annotations

import os
from pathlib import Path
import stat
import subprocess
import tempfile
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]
HOOK = REPOSITORY / "scripts" / "hooks" / "pre-push"
ZERO_SHA = "0" * 40


class PrePushSecretHistoryTests(unittest.TestCase):
    def create_history(self, root: Path) -> tuple[str, str]:
        subprocess.run(["git", "init", "-q", str(root)], check=True)
        subprocess.run(["git", "-C", str(root), "config", "user.email", "tests@example.invalid"], check=True)
        subprocess.run(["git", "-C", str(root), "config", "user.name", "Security Tests"], check=True)
        (root / "base.txt").write_text("base\n", encoding="utf-8")
        subprocess.run(["git", "-C", str(root), "add", "."], check=True)
        subprocess.run(["git", "-C", str(root), "commit", "-qm", "base"], check=True)
        base = subprocess.run(
            ["git", "-C", str(root), "rev-parse", "HEAD"],
            text=True, capture_output=True, check=True,
        ).stdout.strip()
        (root / "transient.txt").write_text("api_key=synthetic_fixture_only\n", encoding="utf-8")
        subprocess.run(["git", "-C", str(root), "add", "."], check=True)
        subprocess.run(["git", "-C", str(root), "commit", "-qm", "add transient fixture"], check=True)
        (root / "transient.txt").unlink()
        subprocess.run(["git", "-C", str(root), "add", "-u"], check=True)
        subprocess.run(["git", "-C", str(root), "commit", "-qm", "remove transient fixture"], check=True)
        tip = subprocess.run(
            ["git", "-C", str(root), "rev-parse", "HEAD"],
            text=True, capture_output=True, check=True,
        ).stdout.strip()
        return base, tip

    def isolated_environment(self, root: Path, with_gitleaks: bool) -> tuple[dict[str, str], Path]:
        binary = root / "bin"
        binary.mkdir()
        (binary / "git").symlink_to(subprocess.run(
            ["which", "git"], text=True, capture_output=True, check=True,
        ).stdout.strip())
        log = root / "gitleaks-args"
        if with_gitleaks:
            gitleaks = binary / "gitleaks"
            gitleaks.write_text(
                f"#!/bin/sh\nprintf '%s\\n' \"$@\" > '{log}'\nexit 0\n",
                encoding="utf-8",
            )
            gitleaks.chmod(gitleaks.stat().st_mode | stat.S_IXUSR)
        environment = os.environ.copy()
        environment["PATH"] = str(binary)
        environment["HOME"] = str(root / "home")
        return environment, log

    def run_hook(self, root: Path, environment: dict[str, str], line: str) -> subprocess.CompletedProcess[str]:
        return subprocess.run(
            ["/bin/bash", str(HOOK)],
            cwd=root,
            env=environment,
            input=line,
            text=True,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            check=False,
        )

    def test_missing_gitleaks_blocks_deleted_intermediate_secret_for_all_ref_shapes(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory) / "repository"
            root.mkdir()
            base, tip = self.create_history(root)
            environment, _ = self.isolated_environment(Path(directory), with_gitleaks=False)

            for remote_sha in (base, ZERO_SHA):
                with self.subTest(remote_sha=remote_sha):
                    result = self.run_hook(
                        root,
                        environment,
                        f"refs/heads/main {tip} refs/heads/main {remote_sha}\n",
                    )
                    self.assertEqual(2, result.returncode, result.stdout)
                    self.assertIn("gitleaks is missing", result.stdout)

    def test_available_gitleaks_receives_exact_existing_ref_range(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory) / "repository"
            root.mkdir()
            base, tip = self.create_history(root)
            environment, log = self.isolated_environment(Path(directory), with_gitleaks=True)

            result = self.run_hook(
                root,
                environment,
                f"refs/heads/main {tip} refs/heads/main {base}\n",
            )

            self.assertEqual(0, result.returncode, result.stdout)
            self.assertIn(f"--log-opts={base}..{tip}", log.read_text(encoding="utf-8").splitlines())

    def test_unresolvable_remote_range_fails_closed_before_scanner(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory) / "repository"
            root.mkdir()
            _, tip = self.create_history(root)
            environment, log = self.isolated_environment(Path(directory), with_gitleaks=True)

            result = self.run_hook(
                root,
                environment,
                f"refs/heads/main {tip} refs/heads/main {'f' * 40}\n",
            )

            self.assertEqual(2, result.returncode, result.stdout)
            self.assertIn("could not be resolved", result.stdout)
            self.assertFalse(log.exists())


if __name__ == "__main__":
    unittest.main()
