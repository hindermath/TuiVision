# Source and Provenance Requirements Checklist

**Purpose**: Review whether source identity, pinning, licensing, and no-copy requirements are complete and unambiguous
**Created**: 2026-07-16
**Feature**: [spec.md](../spec.md)

## Source Identity

- [x] CHK001 Are all three source perspectives and their normative or advisory roles explicitly distinguished? [Completeness, Spec FR-002-FR-005]
- [x] CHK002 Are the Free Vision and Terminal.GUI revisions identified by immutable commit-level values? [Clarity, Spec FR-003-FR-004]
- [x] CHK003 Is the annotated Terminal.GUI tag object distinguished from its peeled commit? [Clarity, Spec FR-004]
- [x] CHK004 Are later Terminal.GUI revisions and magiblot/tvision explicitly excluded from this audit? [Consistency, Spec FR-005]

## Provenance and Licensing

- [x] CHK005 Does the manifest requirement define URL, tag, commit, license, retrieval date, path, hash, and summary fields? [Completeness, Spec FR-006]
- [x] CHK006 Is the MIT license evidence requirement stated without implying that source may be copied into TuiVision? [Consistency, Spec FR-006-FR-007]
- [x] CHK007 Is failure behavior defined for an unavailable, moved, or mismatching pinned source? [Coverage, Spec FR-022]
- [x] CHK008 Is the no-copy boundary measurable for source files, excerpts, fixtures, and generated artefacts? [Measurability, Spec SC-006]

## Evidence Boundaries

- [x] CHK009 Are acceptable repository evidence forms limited to paths, hashes, short summaries, and optional commit permalinks? [Clarity, Spec FR-006-FR-007]
- [x] CHK010 Are external checkout, cache, log, and generated-output exclusions consistent across requirements and scope? [Consistency, Spec FR-007 and Out of Scope]
- [x] CHK011 Is read-only treatment specified for every historical, Pascal, Free Vision, and Terminal.GUI source family? [Coverage, Spec FR-002-FR-007 and FR-028]
- [x] CHK012 Can provenance acceptance be objectively measured against the exact tag object, commit, license, and SHA-256 inventory? [Acceptance Criteria, Spec SC-006]

## Review Result

All source and provenance requirements are complete. No specification change is required.
