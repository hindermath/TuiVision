# Component and Data Conformance Acceptance Contract

## Contract Purpose

This contract is the review boundary for Feature 026. A finding closes only
when its real production path and all listed negative boundaries pass. Helper,
comment, inherited, or hidden-method evidence is supplemental only.

## Finding Matrix

| Finding | Contract | Required production path | Required negative boundary |
|---|---|---|---|
| `F010` | `C019` | `TDialog.HandleEvent` -> completion classifier -> ordered child acceptance -> modal result | Unrelated command remains open; first invalid child preserves state and receives focus |
| `F011` | `C021` | `TInputLine` candidate edit / `CanReleaseFocus` / dialog acceptance -> attached validator phase | Rejection preserves data, cursor, viewport, insert mode, exact selection range and text evidence |
| `F012` | `C023` | `TFileDialog` mode classifier -> typed outcome -> compatibility projection -> optional close | Missing Open, mismatch, invalid path, and missing Save parent reject; existing Save requires caller choice |
| `F013` | `C026` | exact resource key -> registered record -> complete parse/semantic validation -> Controls adapter/factory | Unknown type/version, truncation, trailing data, duplicate key, invalid command/reference, cycle and limits fail atomically |

## Dialog Completion Matrix

| Command class | Default examples | Validate content | Close |
|---|---|---:|---:|
| Affirmative completion | `cmOK`, `cmYes`, `cmNo` | Yes | Only when valid |
| Cancel completion | `cmCancel` | No | Yes unless safe-close veto applies |
| Derived explicit completion | Classifier override | Yes unless explicitly cancel-equivalent | Only when valid |
| Navigation/help/application/unknown | Any non-classified command | No completion validation | No |

## Validator Matrix

| Phase | Default `TValidator` behavior | Rejection effect |
|---|---|---|
| `Edit` | Accept temporary candidate; specialized validator may reject impossible syntax | Candidate not committed |
| `FocusLoss` | Delegate to final `IsValid` | Focus transition rejected before mutation |
| `Acceptance` | Delegate to final `IsValid` | Dialog remains open and focuses first invalid target |

## File Outcome Matrix

| Input and mode | Outcome | File operation |
|---|---|---|
| Directory navigation | `Navigation` | None |
| Wildcard/filter input | `Filter` | None |
| Existing file + Open | `OpenAccepted` | None |
| Missing target + Open | `Rejected` | None |
| New target with existing parent + Save | `SaveAccepted` | None |
| Existing file + Save | `OverwriteDecisionRequired` | None; caller decides later |
| Valid requested selection | `SelectionAccepted` | None |
| Invalid path or mode mismatch | `Rejected` | None |
| Cancel | `Canceled` | None |

The existing `ConfirmDecision` compatibility API projects a rejection to the
additive `FileDecisionKind.Rejected`, leaves the dialog open, and never returns
an earlier accepted result.

## Resource Acceptance Matrix

| Resource | Valid proof | Rejection proof |
|---|---|---|
| Dialog description | Existing versioned record, semantic adapter and runtime factory | Invalid role/navigation/command/version and trailing payload |
| Menu description | Stable IDs/parents/order reconstruct `TMenuBar`/`TMenuItem` | Unknown parent, duplicate ID, invalid command, cycle, depth/item limit |
| Status-line description | Ordered context ranges/items reconstruct `TStatusLine` | Invalid range, invalid command, item limit, unsupported version |
| Resource file | Exact case-sensitive key and registered payload roundtrip | Duplicate key, unknown type, truncation, trailing data, entry/payload limit |

Persisted record loaders reject invalid versions, IDs, references, commands,
graphs, ranges, sizes, and depths before catalog publication. Controls validators
apply the same rules to in-memory descriptions before runtime reconstruction.

## Proof and Closure Rules

1. Each finding starts with a recorded failing test against unchanged production code.
2. Each Green proof executes the production path named in the Finding Matrix.
3. `Implemented` or `AlreadySatisfied` is the only closure decision.
4. `AlreadySatisfied` still requires a new production-path proof and unchanged rationale.
5. `FollowUpHardening` never closes an accepted finding.
6. `ProductDecision` stops a breaking, format-compatibility, runtime-activation, or destructive-policy change.
7. Feature-024 audit status changes only after all required Green proofs pass.

## Validation Gates

- Targeted Controls and Serialization Release tests pass.
- Full Release tests pass.
- Canonical coverage is at least 70 percent for all five governed assemblies.
- `git diff --check` and `dotnet format --verify-no-changes` pass.
- Public API/XML changes pass DocFX, Playwright/Axe, and text-first review.
- Exact staged candidate, secret scan, preset/agent parity, scope firewall, and generated-output checks pass.
- Actual Ubuntu, macOS, and Windows/WSL workflow/job/runner evidence is mapped before merge.
- No actionable review thread remains; an unavailable reviewer is recorded as missing, not passed.
