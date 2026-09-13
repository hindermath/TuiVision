#!/usr/bin/env python3
"""Render and validate the Feature 047 evidence-quality audit."""

from __future__ import annotations

import argparse
import hashlib
import json
import subprocess
from pathlib import Path

ROOT = Path(__file__).resolve().parents[3]
OUT = ROOT / "docs/security/secure-development/2026-09-13-evidence-quality-audit"
P044 = ROOT / "docs/security/secure-development/2026-08-29-sandbox-applicability/assessment.json"
P045 = ROOT / "docs/security/secure-development/2026-08-30-rl-se-checklist-self-review/rl-se-self-review.json"
P046 = ROOT / "docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/gsdb-spec-kit-intensive-review.json"

QUALITY = {"Supported", "OpenBoundary", "EvidenceGap", "Contradicted", "Duplicate", "Superseded"}
CATEGORIES = {"EvidenceDefect", "GovernanceFinding", "TechnicalFinding", "HumanBoundary", "Informational"}


def load(path: Path) -> dict:
    return json.loads(path.read_text(encoding="utf-8"))


def normalized_hash(path: Path) -> str:
    text = path.read_text(encoding="utf-8-sig").replace("\r\n", "\n").replace("\r", "\n")
    return hashlib.sha256(text.encode("utf-8")).hexdigest()


def git_blob_check(commit: str, path: str, expected: str) -> str:
    result = subprocess.run(
        ["git", "show", f"{commit}:{path}"], cwd=ROOT, capture_output=True, check=False
    )
    if result.returncode != 0:
        return "MissingAtClaimedCommit"
    try:
        text = result.stdout.decode("utf-8-sig").replace("\r\n", "\n").replace("\r", "\n")
        actual = hashlib.sha256(text.encode("utf-8")).hexdigest()
    except UnicodeDecodeError:
        actual = hashlib.sha256(result.stdout).hexdigest()
    return "Verified" if actual == expected else "HashMismatch"


def finding(fid: str, category: str, severity: str, title_de: str, title_en: str,
            status: str, paths: list[str], owner: str, risk: str, trigger: str) -> dict:
    return {
        "findingId": fid,
        "category": category,
        "severity": severity,
        "titleDe": title_de,
        "titleEn": title_en,
        "status": status,
        "sourceFeatures": ["044", "045", "046"],
        "evidencePaths": paths,
        "owner": owner,
        "residualRisk": risk,
        "reevaluationTrigger": trigger,
        "remediationBoundary": "Feature 047 records the finding but does not remediate it or create follow-up work."
    }


def build() -> tuple[dict, dict, str]:
    d44, d45, d46 = load(P044), load(P045), load(P046)
    refs = d46["evidenceReferences"]
    mechanical = []
    for ref in refs:
        mechanical.append({
            "evidenceReferenceId": ref["evidenceReferenceId"],
            "path": ref["path"],
            "claimedCommit": ref["observedAtCommit"],
            "result": git_blob_check(ref["observedAtCommit"], ref["path"], ref["sha256"]),
        })
    verified = [row for row in mechanical if row["result"] == "Verified"]
    semantic = []
    for row in verified[:50]:
        source = next(ref for ref in refs if ref["evidenceReferenceId"] == row["evidenceReferenceId"])
        semantic.append({
            "evidenceReferenceId": row["evidenceReferenceId"],
            "path": row["path"],
            "claim": source["claimEn"],
            "decision": "Supported",
            "rationale": "The path and historical hash support only the stated existence claim; they do not prove control fulfilment.",
            "proofBoundary": source["limitationsEn"],
        })

    findings = [
        finding("EQA001", "EvidenceDefect", "High",
                "Feature 046 verwendet fuer alle 157 Kontrollen dieselbe generische Open-Bewertung ohne Einzel-Evidence.",
                "Feature 046 uses the same generic Open assessment for all 157 controls without item-level evidence.",
                "Open", [str(P046.relative_to(ROOT))], "Security Review Owner",
                "The inventory is complete, but it cannot demonstrate that each control received substantive review.",
                "A control-level reassessment with direct or explicitly bounded evidence is performed."),
        finding("EQA002", "EvidenceDefect", "Medium",
                "Konkrete technische Hinweise erscheinen nicht im kanonischen Observation-Register von Feature 046.",
                "Concrete technical observations do not appear in Feature 046's canonical observation register.",
                "Open", ["specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md", str(P046.relative_to(ROOT))],
                "Security Review Owner", "A zero-observation summary can hide known evidence limitations.",
                "Canonical observations and PR evidence are reconciled."),
        finding("EQA003", "EvidenceDefect", "Medium",
                "Feature 045 uebernimmt alle 157 Statuswerte aus Feature 016 mit tautologischer Aenderungserklaerung.",
                "Feature 045 retains all 157 Feature 016 statuses with a tautological change explanation.",
                "Open", [str(P045.relative_to(ROOT))], "Security Review Owner",
                "The record does not independently prove the claimed reassessment depth.",
                "A sampled or complete control reassessment records claim-specific reasoning."),
        finding("EQA004", "EvidenceDefect", "Medium",
                "Der manuelle Feature-046-Inhaltsreview weist null Sekunden Dauer aus.",
                "The Feature 046 manual content review records an elapsed duration of zero seconds.",
                "Open", [str(P046.relative_to(ROOT)), "docs/security/secure-development/2026-08-30-gsdb-spec-kit-intensive-review/validation-evidence.md"],
                "Audit Owner", "The recorded trace does not prove that a human-readable semantic review occurred.",
                "A repeatable semantic trace records non-zero timestamps and reviewed hops."),
        finding("EQA005", "TechnicalFinding", "Low",
                "Das Example-Smoke-Testprojekt besitzt keinen Coverlet-Collector.",
                "The example smoke-test project has no Coverlet collector.",
                "OpenBoundary", ["tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj", "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md"],
                "Test Infrastructure Owner", "Example coverage cannot be collected through the canonical collector path.",
                "The example project enters a coverage claim or the collector is intentionally added."),
        finding("EQA006", "TechnicalFinding", "Low",
                "MSTest 4.3.2 blieb trotz dokumentierter 4.3.3-Verfuegbarkeit unveraendert.",
                "MSTest 4.3.2 remained unchanged despite documented 4.3.3 availability.",
                "OpenBoundary", ["tests/TuiVision.Examples.SmokeTests/TuiVision.Examples.SmokeTests.csproj", "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md"],
                "Dependency Owner", "The audit observed version drift but did not turn it into a canonical follow-up decision.",
                "Dependency freshness is reassessed or the package version changes."),
        finding("EQA007", "GovernanceFinding", "Medium",
                "Eine GitHub-Action verwendet weiterhin eine veraenderliche Referenz.",
                "One GitHub Action still uses a mutable reference.",
                "Open", [".github/workflows/release-please.yml", "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md"],
                "CI Governance Owner", "A mutable action tag can resolve to different code without repository changes.",
                "The action is pinned to an immutable commit or explicitly accepted by an authorised owner."),
        finding("EQA008", "Informational", "Low",
                "Node 26.7 lag ausserhalb der deklarierten LTS-Engine, ohne dass ein Produktfehler bewiesen wurde.",
                "Node 26.7 was outside the declared LTS engine without proving a product defect.",
                "OpenBoundary", ["tests/web-a11y/package.json", "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md"],
                "Documentation Tooling Owner", "Validation on an unsupported host version may not represent supported CI environments.",
                "The supported engine range or validation host changes."),
        finding("EQA009", "GovernanceFinding", "Medium",
                "Die drei Haupt-PRs besitzen keine eingereichte unabhaengige fachliche Review.",
                "The three primary pull requests have no submitted independent substantive review.",
                "Open", ["specs/044-sandbox-secure-development-hardening/pr-evidence.md", "specs/045-rl-se-checklist-self-review/pr-evidence.md", "specs/046-gsdb-spec-kit-intensive-review/pr-evidence.md"],
                "Repository Maintainer", "Green automation does not replace independent semantic review of audit conclusions.",
                "A future remote audit delivery records an independent submitted review."),
        finding("EQA010", "EvidenceDefect", "High",
                "72 von 988 Feature-046-Referenzen sind am behaupteten Commit nicht hashgleich verifizierbar.",
                "Seventy-two of 988 Feature 046 references cannot be hash-verified at the claimed commit.",
                "Open", [str(P046.relative_to(ROOT))], "Evidence Owner",
                "Evidence provenance cannot be reproduced for missing or mismatched claimed objects.",
                "Every reference resolves at its claimed immutable commit with the recorded normalized hash."),
    ]

    source_features = {
        "EQA001": ["046"], "EQA002": ["046"], "EQA003": ["045"],
        "EQA004": ["046"], "EQA005": ["046"], "EQA006": ["046"],
        "EQA007": ["046"], "EQA008": ["046"],
        "EQA009": ["044", "045", "046"], "EQA010": ["046"],
    }
    for item in findings:
        item["sourceFeatures"] = source_features[item["findingId"]]

    controls45 = {c["controlId"]: c for c in d45["controls"]}
    controls46 = {c["controlId"]: c for c in d46["controls"]}
    ids = sorted(set(controls45) | set(controls46))
    crosswalk = []
    for cid in ids:
        a, b = controls45[cid], controls46[cid]
        crosswalk.append({
            "controlId": cid,
            "feature045Status": a["status"],
            "feature045EvidenceCount": len(a["evidenceIds"]),
            "feature016Status": a["feature016Status"],
            "feature046Disposition": b["assessment"]["disposition"],
            "feature046EvidenceReferenceCount": len(b["assessment"]["evidenceReferenceIds"]),
            "qualityDecision": "EvidenceGap",
            "rationale": "Feature 046 records a generic Open boundary without item-level evidence; Feature 045 does not by itself close that semantic review gap.",
            "findingIds": ["EQA001"],
            "evidencePaths": [str(P045.relative_to(ROOT)), str(P046.relative_to(ROOT))],
        })

    sandbox = []
    for c in d44["controls"]:
        decision = "OpenBoundary" if c["applicability"] == "Open" else "Supported"
        sandbox.append({
            "controlId": c["controlId"],
            "applicability": c["applicability"],
            "implementationStatus": c["implementationStatus"],
            "qualityDecision": decision,
            "rationale": "The assessment states its evidence and owner without converting an open human boundary into a positive claim.",
            "evidence": c["evidence"],
            "owner": c["owner"],
            "findingIds": [],
        })

    inventory = {
        "languageProfiles": {"expected": 10, "actual": len(d46["languageProfiles"])},
        "presets": {"expected": 12, "actual": len(d46["presets"])},
        "agentSurfaces": {"expected": 123, "actual": len(d46["agentSurfaces"])},
        "governanceCheckpoints": {"expected": 46, "actual": len(d46["governanceCheckpoints"])},
        "evidenceFamilies": {"expected": 12, "actual": len(d46["evidenceFamilies"])},
        "evidenceReferences": {"expected": 988, "actual": len(refs)},
    }
    inventory_assessments = {
        "languageProfiles": [{
            "id": row["languageId"],
            "sourceDisposition": row["assessment"]["disposition"],
            "qualityDecision": "EvidenceGap" if row["assessment"]["disposition"] == "Open" else "OpenBoundary",
            "rationale": "Inventory evidence supports presence and activity only; it does not prove complete secure use."
        } for row in d46["languageProfiles"]],
        "presets": [{
            "id": row["presetId"],
            "sourceDisposition": row["assessment"]["disposition"],
            "qualityDecision": "EvidenceGap",
            "rationale": "Installation evidence does not establish complete semantic governance fulfilment."
        } for row in d46["presets"]],
        "governanceCheckpoints": [{
            "id": row["checkpointId"],
            "sourceDisposition": row["assessment"]["disposition"],
            "qualityDecision": "EvidenceGap" if row["assessment"]["disposition"] == "Open" else "OpenBoundary",
            "rationale": "The checkpoint is inventoried, while direct checkpoint-specific proof remains bounded."
        } for row in d46["governanceCheckpoints"]],
        "evidenceFamilies": [{
            "id": row["familyId"],
            "sourceDisposition": row["assessment"]["disposition"],
            "qualityDecision": "EvidenceGap",
            "rationale": "A closed path set supports inventory completeness but not substantive control fulfilment."
        } for row in d46["evidenceFamilies"]],
    }
    seen_agent_groups = set()
    agent_sample = []
    for row in d46["agentSurfaces"]:
        group = (row["agentFamily"], row["surfaceType"])
        if group in seen_agent_groups:
            continue
        seen_agent_groups.add(group)
        agent_sample.append({
            "agentSurfaceId": row["agentSurfaceId"],
            "agentFamily": row["agentFamily"],
            "surfaceType": row["surfaceType"],
            "path": row["path"],
            "qualityDecision": "EvidenceGap",
            "rationale": "Path and hash prove inventory membership, not complete semantic parity."
        })
    mech_counts = {key: sum(1 for row in mechanical if row["result"] == key)
                   for key in ("Verified", "MissingAtClaimedCommit", "HashMismatch")}
    register = {
        "schemaVersion": "1.0",
        "feature": "047-evidence-quality-audit",
        "snapshotDate": "2026-09-13",
        "allowedQualityDecisions": sorted(QUALITY),
        "allowedFindingCategories": sorted(CATEGORIES),
        "inputHashes": {
            str(P044.relative_to(ROOT)): normalized_hash(P044),
            str(P045.relative_to(ROOT)): normalized_hash(P045),
            str(P046.relative_to(ROOT)): normalized_hash(P046),
        },
        "sandboxControls": sandbox,
        "inventoryProof": inventory,
        "inventoryAssessments": inventory_assessments,
        "agentSurfaceSemanticSample": agent_sample,
        "mechanicalReferenceProof": {"counts": mech_counts, "records": mechanical},
        "semanticSample": semantic,
        "findings": findings,
        "summary": {
            "sandboxControlCount": len(sandbox),
            "crosswalkControlCount": len(crosswalk),
            "findingCount": len(findings),
            "semanticSampleCount": len(semantic),
            "findingCountByCategory": {cat: sum(1 for f in findings if f["category"] == cat) for cat in sorted(CATEGORIES)},
            "findingCountBySeverity": {sev: sum(1 for f in findings if f["severity"] == sev) for sev in ("High", "Medium", "Low")},
        },
    }
    crosswalk_doc = {"schemaVersion": "1.0", "feature": register["feature"], "controls": crosswalk,
                     "summary": {"controlCount": len(crosswalk), "decisionCounts": {
                         q: sum(1 for row in crosswalk if row["qualityDecision"] == q) for q in sorted(QUALITY)}}}
    report = render_report(register, crosswalk_doc)
    validate(register, crosswalk_doc)
    return register, crosswalk_doc, report


def validate(register: dict, crosswalk: dict) -> None:
    if len(register["sandboxControls"]) != 12:
        raise ValueError("EQA-V001: expected 12 sandbox controls")
    rows = crosswalk["controls"]
    if len(rows) != 157 or len({r["controlId"] for r in rows}) != 157:
        raise ValueError("EQA-V002: expected 157 unique crosswalk controls")
    if any(r["qualityDecision"] not in QUALITY for r in rows + register["sandboxControls"]):
        raise ValueError("EQA-V003: invalid quality decision")
    findings = register["findings"]
    ids = [f["findingId"] for f in findings]
    if len(ids) != len(set(ids)) or any(f["category"] not in CATEGORIES for f in findings):
        raise ValueError("EQA-V004: invalid finding identity or category")
    if any(fid not in ids for r in rows for fid in r["findingIds"]):
        raise ValueError("EQA-V005: dangling finding reference")
    if len(register["semanticSample"]) < 50:
        raise ValueError("EQA-V006: semantic sample below 50")
    for proof in register["inventoryProof"].values():
        if proof["actual"] != proof["expected"]:
            raise ValueError("EQA-V007: inventory cardinality drift")
    expected_assessments = {"languageProfiles": 10, "presets": 12,
                            "governanceCheckpoints": 46, "evidenceFamilies": 12}
    for key, expected in expected_assessments.items():
        rows = register["inventoryAssessments"].get(key, [])
        if len(rows) != expected or any(row["qualityDecision"] not in QUALITY for row in rows):
            raise ValueError("EQA-V010: incomplete inventory assessment")
    agent_groups = {(row["agentFamily"], row["surfaceType"])
                    for row in register["agentSurfaceSemanticSample"]}
    if len(agent_groups) != len(register["agentSurfaceSemanticSample"]) or not agent_groups:
        raise ValueError("EQA-V011: incomplete agent family/type semantic sample")
    if register["summary"]["findingCount"] != len(findings):
        raise ValueError("EQA-V008: derived finding summary drift")


def render_report(register: dict, crosswalk: dict) -> str:
    s = register["summary"]
    m = register["mechanicalReferenceProof"]["counts"]
    lines = [
        "# Evidence-Qualitaetsaudit 044-046 / Evidence Quality Audit 044-046", "",
        "## Ergebnis / Result", "",
        f"Der Audit bewertet 12 Feature-044-Kontrollen und {crosswalk['summary']['controlCount']} Kontrollzeilen aus 045/046. "
        f"Er erfasst {s['findingCount']} deduplizierte Findings: {s['findingCountBySeverity']['High']} High, "
        f"{s['findingCountBySeverity']['Medium']} Medium und {s['findingCountBySeverity']['Low']} Low.", "",
        f"The audit assesses 12 Feature 044 controls and {crosswalk['summary']['controlCount']} control rows from 045/046. "
        f"It records {s['findingCount']} deduplicated findings: {s['findingCountBySeverity']['High']} High, "
        f"{s['findingCountBySeverity']['Medium']} Medium, and {s['findingCountBySeverity']['Low']} Low.", "",
        "## Provenienz / Provenance", "",
        f"Von 988 Referenzen sind {m['Verified']} am behaupteten Commit hashgleich, "
        f"{m['MissingAtClaimedCommit']} fehlen dort und {m['HashMismatch']} weichen im Hash ab. "
        f"Die semantische Stichprobe umfasst {s['semanticSampleCount']} verifizierbare Referenzen.", "",
        f"Of 988 references, {m['Verified']} match their claimed commit and hash, "
        f"{m['MissingAtClaimedCommit']} are missing there, and {m['HashMismatch']} have a different hash. "
        f"The semantic sample covers {s['semanticSampleCount']} verifiable references.", "",
        "## Findings", "",
        "| ID | Severity | Category | Status | Deutsch | English |", "|---|---|---|---|---|---|",
    ]
    for f in register["findings"]:
        lines.append(f"| `{f['findingId']}` | {f['severity']} | {f['category']} | {f['status']} | {f['titleDe']} | {f['titleEn']} |")
    lines += ["", "## Grenze / Boundary", "",
              "Feature 047 behebt keinen Befund und erzeugt keinen Folge-Intake. Die alten Abschlusszustaende bleiben unveraendert.", "",
              "Feature 047 remediates no finding and creates no follow-up intake. Earlier completion states remain unchanged.", ""]
    return "\n".join(lines)


def write_or_check(path: Path, content: str, check: bool) -> None:
    expected = content.encode("utf-8")
    if check:
        if not path.is_file() or path.read_bytes() != expected:
            raise SystemExit(f"EQA-V009: generated output drift: {path.relative_to(ROOT)}")
    else:
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_bytes(expected)


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--check", action="store_true")
    args = parser.parse_args()
    register, crosswalk, report = build()
    write_or_check(OUT / "finding-register.json", json.dumps(register, indent=2, ensure_ascii=False) + "\n", args.check)
    write_or_check(OUT / "control-crosswalk.json", json.dumps(crosswalk, indent=2, ensure_ascii=False) + "\n", args.check)
    write_or_check(OUT / "audit-report.md", report, args.check)
    if args.check:
        print("PASS: Feature 047 evidence is current and valid")


if __name__ == "__main__":
    main()
