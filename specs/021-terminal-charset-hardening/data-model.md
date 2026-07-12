# Data Model: Terminal and Charset Hardening

## 1. TerminalSession

| Field | Type/shape | Rules |
|---|---|---|
| VisibleBuffer | rectangular cell snapshot | Positive width/height; controlled writes only |
| Cursor | x/y position | Always clamped to visible bounds |
| CurrentAttributes | foreground/background | One of 16 console colors; defaults gray/black |
| History | FIFO cells | Maximum 4,096 cells; oldest cells evicted first |
| ParserState | idle/text/control/CSI | No partial publication before complete validation |
| NoticeCount | non-negative integer | BEL increments in-process only |
| Status | accepted/rejected/unsupported/reset/closed | Text-readable reason accompanies non-accepted states |
| Capability | enabled/disabled/unsupported | Host-independent session remains usable in deterministic mode |
| Lifecycle | active/closed/disposed | Close/dispose idempotent; no input after disposal |

### State transitions

```text
Created -> Active -> Closed -> Disposed
              |          |
              +--Reset---+
              +--CapabilityLost -> Closed
```

Reset returns active content/parser state to defaults but does not dispose the
object. Close ends input; dispose is idempotent. Invalid transitions are
rejected without changing cells or cursor.

## 2. TerminalObservation

| Field | Type/shape | Rules |
|---|---|---|
| RawText | bounded sequence | One control sequence <=64 characters or plain text chunk |
| Kind | text/control/CSI/reset | Determined before publication |
| Parameters | 0..4 integers | Each 0..9,999 |
| Source | deterministic/host/profile | Evidence classification retained |

An observation is ephemeral and untrusted. It becomes an `EmulationResult` only
after complete syntax, command, range, and lifecycle validation.

## 3. EmulationResult

| Field | Type/shape | Rules |
|---|---|---|
| Outcome | Accepted/Rejected/Unsupported | Exactly one outcome |
| Command | canonical command or none | None for malformed input |
| StateChanged | boolean | False for rejected/unsupported input |
| CellsChanged | bounded region | Empty unless accepted output changes cells |
| CursorBefore/After | positions | After always in bounds |
| StatusText | bilingual-capable text | Explains rejection/unsupported boundary |
| RecoveryBoundary | next observation usable | Mandatory for every non-accepted result |

## 4. CharsetMappingResult

| Field | Type/shape | Rules |
|---|---|---|
| SourceCharset | Unicode/KOI8-R | Other values unsupported |
| SourceValue | scalar/byte | Fully validated |
| Glyph | Unicode scalar representable by current cell contract | `U+FFFD` on replacement |
| Outcome | Mapped/Replaced/Rejected/Unsupported | Exactly one |
| Reason | stable text/code | Host locale/codepage independent |

## 5. BitmapFontFixture

| Field | Type/shape | Rules |
|---|---|---|
| Width | integer | Exactly 8 |
| Height | integer | Exactly 16 |
| GlyphCount | integer | Exactly 256 |
| BytesPerGlyph | integer | Exactly 16 |
| Data | raw bytes | Exactly 4,096 bytes |
| SourceId | repository-relative fixture identity | No arbitrary user path |
| Outcome | Valid/Rejected/Unsupported | Published only after all checks pass |

The fixture exposes glyph-row bytes for proof; it never installs a host font or
executes the historical generator.

## 6. TerminalProfile

| Field | Required | Rules/default |
|---|---|---|
| ProfileId | Yes | Non-empty stable identity |
| Charset | Yes | Unicode or KOI8-R |
| FontId | No | Built-in 8x16 when absent/unavailable |
| Foreground | No | Gray |
| Background | No | Black |
| Source | Derived | Built-in/source-controlled fixture |
| CapabilityState | Derived | Enabled/Disabled/Unsupported |
| FallbackReason | Derived | Required when a default replaces a request |

Unknown or duplicate JSON keys and missing/invalid required fields reject the
entire profile. Missing optional values use documented defaults.

An accepted effective profile can be applied to a `TerminalSession`. The
session records the active profile identity, charset, effective font identity,
font capability, and default colors as observable presentation metadata. Core
cells continue to store Unicode glyphs and console colors; custom raster bytes
are proof metadata and are not installed into the host renderer.

## 7. HostEvidenceRecord

| Field | Rules |
|---|---|
| HostFamily | macOS/Linux/Windows/WSL/headless |
| TerminalCondition | Concrete interactive, redirected, or unavailable state |
| EvidenceClass | DeterministicInProcess/RemoteCI/PhysicalObservation |
| Capability | Enabled/Disabled/Unsupported/NotRun |
| Result | Pass/Fail/NotRun |
| EvidencePath | Exact repository or remote check reference |
| ResidualRisk | Required for NotRun/Unsupported |
| ReevaluationTrigger | Concrete host/toolchain change or physical availability |

## 8. FrameworkDecisionRecord

Each of the six contract areas receives exactly one primary decision:
`UseExistingFramework`, `SmallFrameworkFix`, `IntentionalDeviation`, or
`FollowUpHardening`. The row records existing component, proposed local logic,
evidence path, rationale, and follow-up boundary.
