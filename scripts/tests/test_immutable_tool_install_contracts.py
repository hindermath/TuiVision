"""Validate immutable executable-tool installation contracts."""

from __future__ import annotations

import base64
import json
from pathlib import Path
import re
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]


class ImmutableToolInstallContractTests(unittest.TestCase):
    def test_npm_agents_use_exact_versions_and_sha512_integrity(self) -> None:
        registry = json.loads(
            (REPOSITORY / "scripts/config/npm-agent-cli-registry.json").read_text(encoding="utf-8")
        )

        for tool in registry["tools"]:
            with self.subTest(tool=tool["id"]):
                self.assertRegex(tool["version"], r"^[0-9]+\.[0-9]+\.[0-9]+(?:[-.][0-9A-Za-z.-]+)?$")
                self.assertTrue(tool["integrity"].startswith("sha512-"))
                digest = base64.b64decode(tool["integrity"].removeprefix("sha512-"), validate=True)
                self.assertEqual(64, len(digest))

    def test_spec_kit_uses_full_commit_revision(self) -> None:
        registry = json.loads(
            (REPOSITORY / "scripts/config/required-cli-tools-registry.json").read_text(encoding="utf-8")
        )
        specify = next(tool for tool in registry["tools"] if tool["id"] == "specify")
        install = specify["install"]

        self.assertEqual("uv-git", install["manager"])
        self.assertEqual("https://github.com/github/spec-kit.git", install["repository"])
        self.assertTrue(re.fullmatch(r"[0-9a-f]{40}", install["revision"]))
        self.assertNotIn("arguments", install)

    def test_installers_use_verified_local_npm_tarball(self) -> None:
        bash = (REPOSITORY / "scripts/maintain-agentic-brew-apps.sh").read_text(encoding="utf-8")
        powershell = (REPOSITORY / "scripts/maintain-agentic-winget-apps.ps1").read_text(encoding="utf-8")

        for source in (bash, powershell):
            self.assertIn("npm pack", source)
            self.assertIn("integrity", source.lower())
            self.assertIn("pack-destination", source)
        self.assertNotIn('npm install -g "$package_name"', bash)
        self.assertNotIn("@('install', '-g', [string]$tool.package)", powershell)


if __name__ == "__main__":
    unittest.main()
