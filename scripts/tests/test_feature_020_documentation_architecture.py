#!/usr/bin/env python3
"""Expose Feature-020 contract and aggregate validation to provider CI."""

from __future__ import annotations

from pathlib import Path
import json
import subprocess
import sys
import unittest


REPOSITORY_ROOT = Path(__file__).resolve().parents[2]
FEATURE_ROOT = REPOSITORY_ROOT / "specs" / "020-documentation-architecture-audit"


def ensure_accepted_commit() -> None:
    audit = json.loads(
        (FEATURE_ROOT / "documentation-architecture-audit.json").read_text(encoding="utf-8")
    )
    commit = audit["repositoryCommit"]
    present = subprocess.run(
        ["git", "cat-file", "-e", f"{commit}^{{commit}}"],
        cwd=REPOSITORY_ROOT,
        capture_output=True,
        check=False,
    )
    if present.returncode == 0:
        return
    fetch = subprocess.run(
        ["git", "fetch", "--no-tags", "--depth=1", "origin", commit],
        cwd=REPOSITORY_ROOT,
        text=True,
        capture_output=True,
        check=False,
    )
    if fetch.returncode != 0:
        raise AssertionError(fetch.stdout + fetch.stderr)


class Feature020ProviderDiscoveryTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls) -> None:
        if not FEATURE_ROOT.is_dir():
            raise unittest.SkipTest(
                "Feature 020 documentation architecture artifacts are not installed "
                "in this repository."
            )

    def test_feature_contract_suite(self) -> None:
        process = subprocess.run(
            [
                sys.executable,
                "-m",
                "unittest",
                "discover",
                "-s",
                str(FEATURE_ROOT / "tests"),
                "-p",
                "test_*.py",
            ],
            cwd=REPOSITORY_ROOT,
            text=True,
            capture_output=True,
            check=False,
        )
        self.assertEqual(0, process.returncode, process.stdout + process.stderr)

    def test_committed_audit_aggregate(self) -> None:
        ensure_accepted_commit()
        process = subprocess.run(
            [
                sys.executable,
                str(FEATURE_ROOT / "tools" / "validate_documentation_architecture.py"),
                "--repo",
                str(REPOSITORY_ROOT),
                "--audit",
                str(FEATURE_ROOT / "documentation-architecture-audit.json"),
            ],
            cwd=REPOSITORY_ROOT,
            text=True,
            capture_output=True,
            check=False,
        )
        self.assertEqual(0, process.returncode, process.stdout + process.stderr)


if __name__ == "__main__":
    unittest.main()
