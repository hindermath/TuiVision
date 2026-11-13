#!/usr/bin/env python3
"""Resolve a missing receipt target without rewriting historical evidence."""

import argparse
import json
import runpy
from pathlib import Path


def resolve_target(receipt_path: Path, repo: Path, source_index: int | None = None) -> str:
    contract = runpy.run_path(str(Path(__file__).with_name("validate-intake-governance-config.py")))
    load = contract["load_json"]
    fail = contract["fail"]
    receipt = load(receipt_path)
    config = load(repo / "requirements/intake-governance-config.json")
    result = contract["validate_config"](config, repo)
    if result["outcome"] != "Aligned":
        fail("RIG018", "archive resolution requires an Aligned collection configuration")
    collections = result["collections"]
    reference = receipt.get("target", {}) if source_index is None else receipt["sources"][source_index]
    if source_index is not None and (source_index < 0 or reference.get("kind") != "File"
                                     or reference.get("location") != "Repository"):
        fail("RIG018", "only repository file sources support archive resolution")
    original = reference.get("path", "")
    contract["validate_path"](original, "receipt target")
    active = (repo / collections["active"]).resolve()
    archive = (repo / collections["archive"]).resolve()
    original_path = (repo / original).resolve()
    if not original_path.is_relative_to(repo) or not original_path.is_relative_to(active):
        fail("RIG018", "historical target must belong to the active collection")
    manifest = load(repo / collections["seriesManifest"])
    series = receipt.get("series", {})
    standalone = all(series.get(key) == "N/A" for key in ("seriesId", "manifestPath", "order", "role"))
    standalone = standalone and series.get("supersedesIntakeIds") == []
    bound = (series.get("seriesId") == manifest.get("seriesId")
             and series.get("seriesId") not in (None, "", "N/A")
             and series.get("manifestPath") == collections["seriesManifest"])
    if not standalone and not bound:
        fail("RIG018", "historical receipt has an incompatible series binding")
    expected = reference.get("normalizedSha256")
    matches = []
    for target in manifest["orderedTargets"]:
        candidate = (repo / target["path"]).resolve()
        # DE: Name und Hash verhindern, dass ein fremdes Archivdokument als Nachfolger gilt.
        # EN: Name and hash prevent an unrelated archive document from becoming a successor.
        same_name = candidate.name == original_path.name or (
            candidate.name.startswith(original_path.stem + ".")
            and candidate.suffix == original_path.suffix
        )
        if (target["status"] == "Completed" and candidate.is_relative_to(archive)
                and candidate.is_relative_to(repo) and same_name
                and target["normalizedSha256"] == expected
                and contract["normalized_sha256"](candidate) == expected):
            matches.append(target["path"])
    if len(matches) != 1:
        fail("RIG018", "missing receipt target requires exactly one completed archive successor")
    return matches[0]


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--receipt", required=True)
    parser.add_argument("--repo", required=True)
    parser.add_argument("--source-index", type=int)
    args = parser.parse_args()
    try:
        print(json.dumps({"resolvedTarget": resolve_target(Path(args.receipt), Path(args.repo).resolve(), args.source_index)}))
    except Exception as exc:
        # DE: Ungueltige Evidence liefert einen begrenzten Fehler statt interner Details.
        # EN: Invalid evidence returns a bounded error instead of internal details.
        print(json.dumps({"errorClass": "RIG018", "message": "archive successor resolution failed"}))
        raise SystemExit(1) from None
