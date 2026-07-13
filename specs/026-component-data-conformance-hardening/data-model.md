# Data Model: Component and Data Conformance Hardening

## 1. Validation

### TValidationPhase

| Value | Meaning |
|---|---|
| `Edit` | A proposed edit may be checked without committing it |
| `FocusLoss` | The current view is asked to release focus |
| `Acceptance` | A confirming dialog completion validates final state |

### TValidationResult

| Field | Type | Rule |
|---|---|---|
| `IsValid` | `bool` | Exactly one accepted/rejected outcome |
| `Phase` | `TValidationPhase` | Phase that produced the result |
| `Message` | `string?` | Required and non-blank for rejection; null for acceptance |
| `Target` | `TView?` | First rejecting view; null for acceptance |

**Transition**: `Candidate -> Accepted -> Commit` or
`Candidate -> Rejected -> Preserve prior state`.

### Input validation state

| Field | Rule |
|---|---|
| Validator | Optional; absence preserves current behavior |
| Text | Never partially changed after rejection |
| Cursor / viewport / insert mode | Snapshot and preserve on rejection |
| Selection start/end | Explicit bounded range; preserve the exact non-empty or collapsed range on rejection |
| Last result | Observable text-first evidence for the latest phase |

## 2. Dialog completion

### DialogCompletionDecision

| Field | Rule |
|---|---|
| Command | `ushort`; default completion set is OK, Cancel, Yes, No |
| IsCompletion | False leaves or forwards the command |
| RequiresValidation | False only for Cancel unless safe-close overrides |
| ValidationResult | First ordered child rejection or accepted result |
| Modal result | Set only after completion and required validation succeed |

Children are visited in stable owner order. A group returns the first rejection
from its subtree. The dialog focuses that target through the existing focus
contract before exposing the error text.

## 3. File dialog outcome

### TFileDialogOutcomeKind

| Value | Meaning | Closes dialog |
|---|---|---|
| `Navigation` | Directory context changed | No |
| `Filter` | Wildcard/filter changed | No |
| `OpenAccepted` | Existing file accepted for Open | Yes |
| `SaveAccepted` | New target with valid existing parent | Yes |
| `OverwriteDecisionRequired` | Existing Save target requires caller choice | Yes |
| `SelectionAccepted` | Valid file/directory selection | Yes |
| `Rejected` | Mode/path mismatch or invalid path | No |
| `Canceled` | Explicit cancel | Yes |

### TFileDialogOutcome

| Field | Type | Rule |
|---|---|---|
| `Kind` | enum | Closed set above |
| `Mode` | `TFileDialogMode` | Original requested operation |
| `Path` | `string?` | Full normalized path for path-bearing outcomes |
| `Filter` | `string?` | Active wildcard/filter |
| `MetadataSnapshot` | `TFileSelectionInfo` | Metadata only; no content ownership |
| `RejectionCode` | `string?` | Stable code for `Rejected` |
| `Message` | `string?` | Text-first explanation |
| `RequiresCallerDecision` | `bool` | True only for overwrite boundary |

The compatibility `TFileDecisionResult` is projected from accepted, overwrite,
selection, rejected, and canceled outcomes. Its `FileDecisionKind` receives the
additive `Rejected` value; rejection detail remains in the metadata fallback
while the new outcome carries the stable code and message.

## 4. Menu description

### MenuDescription

| Field | Rule |
|---|---|
| Version | Exactly current supported version |
| Items | 0–4,096 immutable `MenuItemDescription` entries |

### MenuItemDescription

| Field | Rule |
|---|---|
| Id | Non-blank, ordinal-unique |
| ParentId | Null for top level; otherwise references an existing item |
| Order | Stable non-negative sibling order |
| Label | Non-blank; `---` identifies separator |
| CommandId | Nonzero for actionable item; zero for submenu/separator |
| HelpContext | Non-negative |
| Disabled | Initial static state only |

The parent graph must be acyclic and at most 16 levels deep. Sibling order and
IDs make reconstruction deterministic without serializing pointers.

## 5. Status-line description

### StatusLineDescription

| Field | Rule |
|---|---|
| Version | Exactly current supported version |
| Definitions | 0–4,096 ordered context definitions |

### StatusDefinitionDescription

| Field | Rule |
|---|---|
| MinContext / MaxContext | Non-negative and `Min <= Max` |
| Order | Stable first-match-wins order |
| Items | Bounded immutable status item list |

### StatusItemDescription

| Field | Rule |
|---|---|
| Label | Non-blank text-first action hint |
| CommandId | Nonzero |
| KeyCode | Explicit key or zero for display-only compatibility |
| Disabled | Initial static state only |

## 6. Persisted records and resource transaction

`TMenuDescriptionRecord` and `TStatusLineDescriptionRecord` implement the
existing archive contract and carry an explicit format version. Their records
contain only primitives and immutable lists. Their loaders validate structural
and semantic invariants before returning a record. `TRecordRegistry` is the type
allowlist; no persisted CLR name is resolved. Controls validators apply the
same invariants to programmatically created models before runtime factories run.

### ResourceLoadTransaction

| Stage | State rule |
|---|---|
| Header | Entry count is 0–4,096 |
| Entry | Key is non-blank and ordinal-unique; payload is 0–4 MiB |
| Parse | Registered type only; payload fully consumed |
| Semantic validation | Version, IDs, commands, graph, size and depth valid |
| Publish | Return a new complete `TResourceFile` |
| Failure | Throw deterministic data error; expose no partial candidate |

## 7. Finding and governance evidence

Each finding row contains finding/contract/requirement IDs, closure decision,
Red proof, change or unchanged rationale, Green production-path proof,
historical intent, Free Vision relation, consumer boundary, API/A11Y effect,
residual risk, and follow-up boundary.

Each governance row contains run ID, preset and version, checkpoint,
`Applicable`/`N/A`/`Open`, rationale, evidence path, owner, reviewer, review
date, result, residual risk, follow-up, and re-evaluation trigger.
