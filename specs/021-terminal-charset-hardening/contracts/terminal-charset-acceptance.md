# Acceptance Contract: Terminal and Charset Hardening

## Session and Emulation Matrix

Every row requires state, cursor, cell, status, and recovery evidence.

| Area | Required accepted cases | Required negative/boundary cases |
|---|---|---|
| Text | printable text, wrap, scroll, clipping | empty, one-cell/one-row, disposed session |
| C0 controls | BEL, BS, TAB, CR, LF | boundary positions, mixed CR/LF, no host BEL |
| Cursor relative | CSI A/B/C/D | 0/default, 9,999, bounds clamp, fifth parameter |
| Cursor absolute | CSI H/f | row/column, defaults, out-of-range clamp |
| Erase | CSI J/K accepted modes | unsupported mode, malformed/partial sequence |
| Attributes | CSI m reset plus 16 foreground/background colors | unsupported code, too many/large parameters |
| Full reset | visible/history/parser/status defaults | repeated reset and reset after rejection |
| Recovery | next valid text/sequence | truncated, unknown, 63/64/65 characters |
| History | FIFO to 4,096 cells | 4,095/4,096/4,097 and resize interaction |

## Charset Contract

- Unicode is canonical display text.
- KOI8-R is the sole historical byte map in 021.
- Invalid/unmappable units produce `U+FFFD` and `Replaced`.
- Other codepages produce `Unsupported` without host codepage fallback.
- Mapping evidence is invariant under host locale and encoding defaults.

## Font Fixture Contract

| Property | Accepted | Rejected/Unsupported |
|---|---|---|
| Geometry | 8x16 | Any other width/height |
| Glyphs | 256 | Any other count |
| Bytes/glyph | 16 | Any other stride |
| Total length | 4,096 bytes | Truncated or oversized |
| Format | raw uncompressed fixture | gzip, PSF, SFT, host font install |
| Source | repository/test-owned | arbitrary user path or downloaded asset |

## Profile Contract

The closed JSON schema accepts required `ProfileId` and `Charset` plus optional
`FontId`, `Foreground`, and `Background`. Unknown/duplicate keys, malformed JSON,
or missing/invalid required fields reject the complete profile. Optional values
default to built-in 8x16, gray, and black. Unavailable requests report
`Unsupported`, retain the requested/source evidence, and use the safe default.

## Controls Integration Contract

One framework-level terminal view must be independently provable through a real
application loop. The proof combines controlled key/text input, session state,
cursor, effective profile/charset/font metadata, status text, concrete view
identity, visible buffer/cell positions, and deterministic quit. Raw font bytes
remain metadata rather than a host-installed renderer. The proof does not
create or port a Wave-4 example.

## Host Evidence Contract

macOS, Linux, and Windows/WSL each receive separate
`DeterministicInProcess`, `RemoteCI`, and available `PhysicalObservation` rows.
Unavailable physical conditions remain `NotRun`. Native host font/codepage,
shell, PTY, keyboard-map, audio, or terminal-profile mutation is never a proof
step.

## Framework Usage Contract

Exactly one decision per area is required:

| Area | Expected initial decision boundary |
|---|---|
| Session/emulation | `SmallFrameworkFix` in Drivers.Console |
| Buffer/cell snapshot | `UseExistingFramework` |
| Charset mapping | `SmallFrameworkFix` in Drivers.Console |
| Font fixture | `SmallFrameworkFix` in Drivers.Console |
| Profile loading/fallback | `SmallFrameworkFix` in Drivers.Console |
| Controls/App-loop projection | `SmallFrameworkFix` using existing Controls shell |

Research may justify a different allowed decision, but the evidence row and
scope consequences must remain explicit.

## Validation Contract

- Always: `git diff --check`, scope/placeholder/generated/secret/historical
  scans, `dotnet format --verify-no-changes`.
- Targeted: Drivers and Controls; Compatibility only if its source/tests change.
- Shared executable changes: full Release suite and canonical five-assembly
  Coverlet gate.
- Public XML, guide, navigation, or architecture/security docs: DocFX followed
  by Playwright/axe and UTF-8 text-browser review.
- Scripts: N/A unless scope changes; any new script requires Bash/PowerShell
  parity, help, and man-page evidence.
- Remote: current head required checks green, zero actionable GraphQL threads,
  unavailable reviewers recorded as missing, explicit authority for merge or
  bypass, and clean synchronized `main`. Self-invalidating reviewed-head and
  post-merge facts use the pre-named causal path
  `specs/021-terminal-charset-hardening/closeout-evidence.md`.
