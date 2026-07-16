# Source and Provenance Checklist: Feature 030

**Purpose**: Verify the pinned modernization witness and no-copy boundary.

- [x] Is Turbo Vision 2.0.3 still named as the historical authority?
- [x] Are accepted TuiVision contracts and behavior still the product authority?
- [x] Is magiblot/tvision limited to modernization-witness status?
- [x] Are repository, commit, tree, timestamp, subject, and COPYRIGHT hash exact?
- [x] Is the multipart license context preserved without calling the whole repository simply MIT?
- [x] Must relevant headers be reviewed where declarations carry the contract?
- [x] Must the external checkout stay outside tracked repository paths?
- [x] Are copied source, long excerpts, fixtures, binaries, caches, and generated output forbidden?
- [x] Are relative paths, SHA-256 values, short original summaries, and optional pinned permalinks sufficient evidence?
- [x] Does unverifiable provenance block the run?

## Durchführungshinweis / Review Instruction

Reproduce commit, tree, and COPYRIGHT hash in an external detached checkout.
Inventory every selected relative path and compare the final Git candidate
against forbidden external or generated content.
