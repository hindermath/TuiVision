"""Exercise Spec Kit plan-path confinement against repository-controlled paths."""

from __future__ import annotations

import json
from pathlib import Path
import shutil
import subprocess
import tempfile
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]


def usable_bash() -> bool:
    executable = shutil.which("bash")
    if not executable:
        return False
    completed = subprocess.run(
        [executable, "--version"],
        text=True,
        capture_output=True,
        check=False,
    )
    return completed.returncode == 0


@unittest.skipUnless(usable_bash(), "A functional Bash runtime is required.")
class SpecKitPlanConfinementTests(unittest.TestCase):
    def _repository(self, root: Path) -> Path:
        repo = root / "repo"
        scripts = repo / ".specify/scripts/bash"
        templates = repo / ".specify/templates"
        scripts.mkdir(parents=True)
        templates.mkdir(parents=True)
        shutil.copy2(REPOSITORY / ".specify/scripts/bash/common.sh", scripts / "common.sh")
        shutil.copy2(REPOSITORY / ".specify/scripts/bash/setup-plan.sh", scripts / "setup-plan.sh")
        (templates / "plan-template.md").write_text("safe plan\n", encoding="utf-8")
        (repo / ".specify/feature.json").write_text(
            json.dumps({"feature_directory": "specs/047-test"}), encoding="utf-8"
        )
        subprocess.run(["git", "init", "-q", "-b", "047-test"], cwd=repo, check=True)
        return repo

    def _run(self, repo: Path) -> subprocess.CompletedProcess[str]:
        return subprocess.run(
            ["bash", ".specify/scripts/bash/setup-plan.sh", "--json"],
            cwd=repo,
            text=True,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            check=False,
            timeout=20,
        )

    def test_rejects_absolute_and_traversing_feature_json(self) -> None:
        for value in ("/tmp/outside", "specs/../outside"):
            with self.subTest(value=value), tempfile.TemporaryDirectory() as directory:
                repo = self._repository(Path(directory))
                (repo / ".specify/feature.json").write_text(
                    json.dumps({"feature_directory": value}), encoding="utf-8"
                )
                completed = self._run(repo)
                self.assertNotEqual(0, completed.returncode, completed.stdout)

    def test_rejects_linked_feature_directory_and_plan_leaf(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            repo = self._repository(root)
            outside = root / "outside"
            outside.mkdir()
            specs = repo / "specs"
            specs.mkdir()
            linked = specs / "047-test"
            linked.symlink_to(outside, target_is_directory=True)
            completed = self._run(repo)
            self.assertNotEqual(0, completed.returncode, completed.stdout)
            linked.unlink()

            feature = specs / "047-test"
            feature.mkdir()
            sentinel = outside / "sentinel"
            sentinel.write_text("unchanged", encoding="utf-8")
            (feature / "plan.md").symlink_to(sentinel)
            completed = self._run(repo)
            self.assertNotEqual(0, completed.returncode, completed.stdout)
            self.assertEqual("unchanged", sentinel.read_text(encoding="utf-8"))

    def test_normal_in_tree_feature_writes_plan(self) -> None:
        with tempfile.TemporaryDirectory() as directory:
            repo = self._repository(Path(directory))
            feature = repo / "specs/047-test"
            feature.mkdir(parents=True)
            completed = self._run(repo)
            self.assertEqual(0, completed.returncode, completed.stdout)
            self.assertEqual("safe plan\n", (feature / "plan.md").read_text(encoding="utf-8"))


if __name__ == "__main__":
    unittest.main()
