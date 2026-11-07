"""Security regressions for confined memory-patch application."""

from __future__ import annotations

import os
import shutil
import subprocess
import tempfile
import unittest
from pathlib import Path


REPOSITORY = Path(__file__).resolve().parents[2]
BASH_SCRIPT = REPOSITORY / "scripts" / "check-homogeneity.sh"
POWERSHELL_SCRIPT = REPOSITORY / "scripts" / "check-homogeneity.ps1"


class CheckHomogeneityPatchTests(unittest.TestCase):
    def setUp(self) -> None:
        self.temporary = tempfile.TemporaryDirectory()
        self.root = Path(self.temporary.name) / "repository"
        self.root.mkdir()
        subprocess.run(["git", "init", "-q", str(self.root)], check=True)
        subprocess.run(["git", "-C", str(self.root), "config", "user.email", "tests@example.invalid"], check=True)
        subprocess.run(["git", "-C", str(self.root), "config", "user.name", "Security Tests"], check=True)
        (self.root / "target.txt").write_text("base\n", encoding="utf-8")
        (self.root / "unrelated.txt").write_text("clean\n", encoding="utf-8")
        subprocess.run(["git", "-C", str(self.root), "add", "."], check=True)
        subprocess.run(["git", "-C", str(self.root), "commit", "-qm", "initial"], check=True)

    def tearDown(self) -> None:
        self.temporary.cleanup()

    def variants(self) -> list[str]:
        variants = [] if os.name == "nt" else ["bash"]
        if shutil.which("pwsh"):
            variants.append("powershell")
        return variants

    def run_patch(self, variant: str, patch: Path) -> subprocess.CompletedProcess[str]:
        if variant == "bash":
            command = ["bash", str(BASH_SCRIPT), "--apply-patch", str(patch), "--yes", str(self.root)]
        else:
            command = [
                "pwsh", "-NoProfile", "-File", str(POWERSHELL_SCRIPT),
                "-TargetDir", str(self.root), "-ApplyPatch", str(patch), "-Yes",
            ]
        return subprocess.run(command, text=True, capture_output=True, check=False)

    def write_patch(self, name: str, entries: list[tuple[str, str]]) -> Path:
        patch = Path(self.temporary.name) / name
        lines = ["# memory-patch.md", ""]
        for target, content in entries:
            lines.extend([f"### Target: {target}", f"+++ {content}", ""])
        patch.write_text("\n".join(lines), encoding="utf-8")
        return patch

    def test_invalid_second_target_is_rejected_before_any_mutation(self) -> None:
        outside = Path(self.temporary.name) / "outside.txt"
        outside.write_text("outside\n", encoding="utf-8")
        for variant in self.variants():
            with self.subTest(variant=variant):
                patch = self.write_patch(
                    f"invalid-{variant}.md",
                    [("target.txt", "allowed"), (str(outside), "escaped")],
                )
                result = self.run_patch(variant, patch)
                self.assertEqual(2, result.returncode, result.stdout + result.stderr)
                self.assertEqual("base\n", (self.root / "target.txt").read_text(encoding="utf-8"))
                self.assertEqual("outside\n", outside.read_text(encoding="utf-8"))

    def test_parent_wildcard_and_colon_targets_are_rejected_with_yes(self) -> None:
        for variant in self.variants():
            for target in ("../outside.txt", "*.txt", "target.txt:stream"):
                with self.subTest(variant=variant, target=target):
                    patch = self.write_patch(f"invalid-{variant}-{len(target)}.md", [(target, "escaped")])
                    result = self.run_patch(variant, patch)
                    self.assertEqual(2, result.returncode, result.stdout + result.stderr)
                    self.assertEqual("base\n", (self.root / "target.txt").read_text(encoding="utf-8"))

    def test_link_ancestor_escape_is_rejected(self) -> None:
        outside = Path(self.temporary.name) / "outside"
        outside.mkdir()
        (self.root / "linked").symlink_to(outside, target_is_directory=True)
        for variant in self.variants():
            with self.subTest(variant=variant):
                patch = self.write_patch(f"link-{variant}.md", [("linked/escaped.txt", "escaped")])
                result = self.run_patch(variant, patch)
                self.assertEqual(2, result.returncode, result.stdout + result.stderr)
                self.assertFalse((outside / "escaped.txt").exists())

    def test_git_metadata_variants_are_rejected(self) -> None:
        for variant in self.variants():
            for target in (".git/config", ".GiT/hooks/post-commit", ".git./config", ".git /config"):
                with self.subTest(variant=variant, target=target):
                    patch = self.write_patch(f"git-{variant}-{len(target)}.md", [(target, "unsafe")])
                    result = self.run_patch(variant, patch)
                    self.assertEqual(2, result.returncode, result.stdout + result.stderr)
                    config = subprocess.run(
                        ["git", "-C", str(self.root), "config", "--local", "--get", "unsafe"],
                        text=True, capture_output=True, check=False,
                    )
                    self.assertNotEqual(0, config.returncode)

    def test_valid_target_is_applied_and_unrelated_change_is_not_staged(self) -> None:
        for variant in self.variants():
            with self.subTest(variant=variant):
                (self.root / "target.txt").write_text("base\n", encoding="utf-8")
                subprocess.run(["git", "-C", str(self.root), "add", "target.txt"], check=True)
                subprocess.run(["git", "-C", str(self.root), "commit", "-qm", "reset target"], check=False)
                (self.root / "unrelated.txt").write_text(f"dirty-{variant}\n", encoding="utf-8")
                patch = self.write_patch(f"valid-{variant}.md", [("target.txt", f"added-{variant}")])

                result = self.run_patch(variant, patch)

                self.assertEqual(0, result.returncode, result.stdout + result.stderr)
                self.assertIn(f"added-{variant}", (self.root / "target.txt").read_text(encoding="utf-8"))
                staged = subprocess.run(
                    ["git", "-C", str(self.root), "diff", "--cached", "--name-only"],
                    text=True, capture_output=True, check=True,
                ).stdout
                self.assertEqual("", staged)
                dirty = subprocess.run(
                    ["git", "-C", str(self.root), "diff", "--name-only"],
                    text=True, capture_output=True, check=True,
                ).stdout.splitlines()
                self.assertEqual(["unrelated.txt"], dirty)


if __name__ == "__main__":
    unittest.main()
