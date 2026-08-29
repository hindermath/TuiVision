import copy
import importlib.util
import json
from pathlib import Path
import unittest


REPOSITORY_ROOT = Path(__file__).resolve().parents[2]
VALIDATOR_PATH = REPOSITORY_ROOT / "scripts" / "validate-sandbox-applicability.py"
FIXTURE_ROOT = Path(__file__).resolve().parent / "sandbox-applicability" / "fixtures"


def load_validator():
    spec = importlib.util.spec_from_file_location("sandbox_applicability", VALIDATOR_PATH)
    module = importlib.util.module_from_spec(spec)
    assert spec.loader is not None
    spec.loader.exec_module(module)
    return module


class SandboxApplicabilityTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.validator = load_validator()
        cls.valid = json.loads((FIXTURE_ROOT / "invalid-missing-control.json").read_text(encoding="utf-8"))
        cls.valid["controls"].append(
            {
                "controlId": "CL-12-12",
                "applicability": "Applicable",
                "implementationStatus": "Fulfilled",
                "rationale": "The installed preset matrix is validated.",
                "evidence": ".specify/presets/",
                "owner": "Maintainer",
                "reviewer": "Reviewer",
                "reviewDate": "2026-08-29",
                "residualRisk": "Low.",
                "followUp": "Review after a preset update.",
                "reevaluationTrigger": "Preset matrix changes.",
            }
        )

    def test_valid_fixture_passes(self):
        self.assertEqual([], self.validator.validate(self.valid))

    def test_missing_control_fixture_fails(self):
        data = json.loads((FIXTURE_ROOT / "invalid-missing-control.json").read_text(encoding="utf-8"))
        self.assertTrue(any("controls" in error for error in self.validator.validate(data)))

    def test_unknown_recommendation_fails(self):
        data = copy.deepcopy(self.valid)
        data["recommendation"] = "Approved"
        self.assertTrue(any("recommendation" in error for error in self.validator.validate(data)))

    def test_duplicate_control_fails(self):
        data = copy.deepcopy(self.valid)
        data["controls"][-1]["controlId"] = "CL-12-11"
        self.assertTrue(any("controls" in error for error in self.validator.validate(data)))

    def test_open_cannot_be_fulfilled(self):
        data = copy.deepcopy(self.valid)
        data["controls"][0]["implementationStatus"] = "Fulfilled"
        self.assertTrue(any("Open" in error for error in self.validator.validate(data)))

    def test_private_host_path_fails(self):
        data = copy.deepcopy(self.valid)
        data["mounts"][0]["mountRole"] = "/Users/example/private-project"
        self.assertTrue(any("mountRole" in error for error in self.validator.validate(data)))

    def test_platform_proof_needs_platform(self):
        data = copy.deepcopy(self.valid)
        data["executions"][0]["proofLevel"] = "PlatformVerified"
        data["executions"][0]["platform"] = ""
        self.assertTrue(any("platform" in error for error in self.validator.validate(data)))

    def test_not_permitted_has_no_command(self):
        data = copy.deepcopy(self.valid)
        data["executions"][0]["location"] = "NotPermitted"
        self.assertTrue(any("NotPermitted" in error for error in self.validator.validate(data)))


if __name__ == "__main__":
    unittest.main()
