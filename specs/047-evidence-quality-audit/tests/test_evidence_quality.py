import copy
import importlib.util
import unittest
from pathlib import Path

MODULE_PATH = Path(__file__).parents[1] / "tools/render_evidence.py"
SPEC = importlib.util.spec_from_file_location("render_evidence", MODULE_PATH)
MODULE = importlib.util.module_from_spec(SPEC)
assert SPEC.loader is not None
SPEC.loader.exec_module(MODULE)


class EvidenceQualityTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.register, cls.crosswalk, cls.report = MODULE.build()

    def test_positive_dataset(self):
        MODULE.validate(self.register, self.crosswalk)
        self.assertEqual(12, len(self.register["sandboxControls"]))
        self.assertEqual(157, len(self.crosswalk["controls"]))
        self.assertEqual(988, sum(self.register["mechanicalReferenceProof"]["counts"].values()))
        self.assertGreaterEqual(len(self.register["semanticSample"]), 50)
        self.assertEqual(10, len(self.register["inventoryAssessments"]["languageProfiles"]))
        self.assertEqual(12, len(self.register["inventoryAssessments"]["presets"]))
        self.assertEqual(46, len(self.register["inventoryAssessments"]["governanceCheckpoints"]))
        self.assertEqual(12, len(self.register["inventoryAssessments"]["evidenceFamilies"]))
        self.assertGreater(len(self.register["agentSurfaceSemanticSample"]), 0)

    def test_missing_control_fails(self):
        broken = copy.deepcopy(self.crosswalk)
        broken["controls"].pop()
        with self.assertRaisesRegex(ValueError, "EQA-V002"):
            MODULE.validate(self.register, broken)

    def test_duplicate_control_fails(self):
        broken = copy.deepcopy(self.crosswalk)
        broken["controls"][-1]["controlId"] = broken["controls"][0]["controlId"]
        with self.assertRaisesRegex(ValueError, "EQA-V002"):
            MODULE.validate(self.register, broken)

    def test_invalid_decision_fails(self):
        broken = copy.deepcopy(self.crosswalk)
        broken["controls"][0]["qualityDecision"] = "Pass"
        with self.assertRaisesRegex(ValueError, "EQA-V003"):
            MODULE.validate(self.register, broken)

    def test_dangling_finding_fails(self):
        broken = copy.deepcopy(self.crosswalk)
        broken["controls"][0]["findingIds"] = ["EQA999"]
        with self.assertRaisesRegex(ValueError, "EQA-V005"):
            MODULE.validate(self.register, broken)

    def test_short_semantic_sample_fails(self):
        broken = copy.deepcopy(self.register)
        broken["semanticSample"] = broken["semanticSample"][:49]
        with self.assertRaisesRegex(ValueError, "EQA-V006"):
            MODULE.validate(broken, self.crosswalk)

    def test_summary_drift_fails(self):
        broken = copy.deepcopy(self.register)
        broken["summary"]["findingCount"] = 0
        with self.assertRaisesRegex(ValueError, "EQA-V008"):
            MODULE.validate(broken, self.crosswalk)


if __name__ == "__main__":
    unittest.main()
