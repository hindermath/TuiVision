"""Static security boundaries for credential-bearing workflows."""

from pathlib import Path
import json
import unittest


REPOSITORY = Path(__file__).resolve().parents[2]


class WorkflowSecurityBoundaryTests(unittest.TestCase):
    def test_docfx_is_restored_from_exact_local_tool_manifest(self) -> None:
        manifest = json.loads((REPOSITORY / ".config/dotnet-tools.json").read_text(encoding="utf-8"))
        docfx = manifest["tools"]["docfx"]
        # DE: Freigegebener Manifest-Pin; Restore- und RollForward-Grenzen bleiben Pflicht.
        # EN: Reviewed manifest pin; restore and rollForward boundaries remain mandatory.
        self.assertEqual("2.81.0", docfx["version"])
        self.assertEqual(["docfx"], docfx["commands"])
        self.assertIs(False, docfx["rollForward"])

        for relative in (".github/workflows/ci.yml", ".github/workflows/pages.yml"):
            with self.subTest(workflow=relative):
                workflow = (REPOSITORY / relative).read_text(encoding="utf-8")
                self.assertIn("dotnet tool restore", workflow)
                self.assertIn("dotnet tool run docfx -- docfx.json", workflow)
                self.assertNotIn("tool install --global docfx", workflow)
                self.assertNotIn("tool update --global docfx", workflow)
                self.assertNotIn("\n          docfx docfx.json", workflow)

    def test_pages_pr_build_has_no_publish_authority_or_persisted_token(self) -> None:
        workflow = (REPOSITORY / ".github/workflows/pages.yml").read_text(encoding="utf-8")
        build = workflow[workflow.index("  build:"):workflow.index("  deploy:")]
        deploy = workflow[workflow.index("  deploy:"):]

        self.assertIn("permissions:\n      contents: read", build)
        self.assertNotIn("pages: write", build)
        self.assertNotIn("id-token: write", build)
        self.assertIn("persist-credentials: false", build)
        self.assertNotIn("actions/configure-pages@", build)
        self.assertIn("pages: write", deploy)
        self.assertIn("id-token: write", deploy)
        self.assertIn("actions/configure-pages@", deploy)
        self.assertIn("actions/deploy-pages@", deploy)
        self.assertIn("if: github.event_name != 'pull_request'", deploy)


if __name__ == "__main__":
    unittest.main()
