# Prüfliste: Provenienz und Kardinalität

**Purpose**: Ensure the closure can reconstruct the exact Wave-6 delta.

- [x] PR #101 and #104 are identified as the only product deliveries.
- [x] PR #102 and #105 are identified as causal closeouts.
- [x] PR #103 is excluded from the product delta as metadata-only work.
- [x] Base, head, merge, file-count and file-set-hash evidence is required.
- [x] Exactly 24 `TVFM/` source rows are required and current hashes match.
- [x] Exactly ten functional and ten showcase areas are required.
- [x] Exactly ten combined contract rows and one entry point are required.
- [x] Missing, duplicate, unknown and orphan relations fail closed.
- [x] LF/CRLF-neutral handling is required for text-source evidence.
- [x] Historical and comparison roots remain read-only.

## Result

`PASS` - 10/10 items complete. No provenance or cardinality ambiguity remains.
