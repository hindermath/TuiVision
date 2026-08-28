# PR Evidence: Transactional Form Model

## Delivery boundary

Feature 042 completed its product work under `LocalImplementation`. The user
subsequently granted explicit `MergeAndSync` authority with a narrow
admin-bypass authorization. That bypass may cover only a remaining repository
approval rule after every technical, exact-head and review gate passes; it may
not replace evidence or a failed check. The first compile-red run at repository
version `1.42.762.445` failed because the planned runtime registry did not yet
exist; the final implementation closes that contract without a placeholder.

## Bindings

- Intake SHA-256: `45d8f8b589c3d9fd417f9b8dc9d25d09c0597f02c075b04454bbce95e095a239`
- Series review: `339a343c-4973-4c86-a6f9-03ae6290a210`
- Issue: `#154`
- Magiblot commit/tree: `57b6f56b38e0ee75240a80a10ee0e11470c24693` / `96dd03873955689ff0a79f6c8107a8148fe1ebd6`

## Source decisions

| Decision | Evidence | Result |
|---|---|---|
| `IntentionalTuiVisionDeviation` | Issue #154 and the accepted intake | Transactional fields, snapshots, nested sessions and declarative form semantics are TuiVision-specific product contracts; neither external source defines them. |
| `AdoptModernization` | Magiblot `tdialog.cpp`, `tinputli.cpp`, `dialogs.h`, `views.h` at the pinned commit/tree | Additive composition, separated responsibilities and a trusted symbolic registry guide the implementation shape without copying C++ inheritance, storage or source text. |
| `PreserveHistoricalIntent` | `tv203s/contrib/tvision/classes/tdialog.cc`, `tinputli.cc` and required `dialog.h`, `inputln.h`, `dialogs.h`, `views.h` | Existing dialog commands, cancel behavior, input transfer, focus and validation remain compatible; ordinary controls are unchanged. |

The Magiblot checkout stayed external and untracked. Repository evidence stores
only pin/tree/path provenance and original summaries; it does not characterize
the multipart upstream license as repository-wide MIT.

## Delivered product contract

- `IFormField`, `IFormField<T>`, `FormField<T>` and `FormSession` provide
  baseline, explicit equality, Dirty, immutable ordered ChangeSet, validation,
  recursive Accept and Reject.
- Direct property expressions provide typed POCO binding. Explicit-culture
  converters mediate different field/model types. Submit never changes POCO or
  baseline; Accept follows successful external persistence.
- Submit-time async validators run on one stable recursive snapshot. Parallel
  submission is rejected, cancellation propagates and revision drift returns
  `Stale` without publishing obsolete errors.
- Setter failure performs best-effort reverse rollback, including restoration
  of the setter that threw. Baselines remain unchanged; setter side effects are
  reported as an unavoidable contract boundary.
- Version-1 form JSON is a closed, size/depth/item-bounded data format. A safe
  registry resolves trusted keys only. Unknown properties/versions/keys,
  duplicates, bad references, conflicts, sharing and cycles fail atomically.
- `examples/FormTransaction` loads embedded source-controlled JSON and shows a
  customer/address transaction through real controls, status, keyboard paths
  and `Help -> Description`; persistence is in memory only.

## Gate ledger

| Gate | Status | Evidence |
|---|---|---|
| Field/session core | Pass | Controls target includes Dirty/equality/change-set/accept/reject and recursive ownership cases. |
| Binding/converter/rollback | Pass | Direct POCO expressions, explicit `de-DE` conversion failures and reverse setter rollback are executable tests. |
| Async snapshot/children | Pass | Success, validation failure, cancellation, concurrent-submit rejection, snapshot drift and root-only child operations pass. |
| JSON/registry | Pass | Serialization target passes 12/12 roundtrip and malformed-input cases; registry resolution and type/key conflicts pass in Controls. |
| FormTransaction app-loop | Pass | Four `app.Run()` smoke tests cover accept/persistence, invalid/reject, cancellation/stale and Help Description with state, tree, buffer/cell and status assertions. |
| Release build | Pass | Version `1.42.762.451`: 0 warnings, 0 errors. |
| Full Release tests | Pass | Version `1.42.762.455`: 965/965 (Core 52, Serialization 60, Compatibility 18, Drivers 151, Controls 382, Examples 302). |
| Five coverage gates | Pass | Version `1.42.762.456`: Core 92.96%, Controls 86.95%, Serialization 90.47%, Compatibility 80.55%, Drivers.Console 89.18%; `coverlet.runsettings` is XML-valid. |
| Format and diff | Pass | `dotnet format ... --verify-no-changes` reports 0/583 files; `git diff --check` passes. |
| Governance/source policy | Pass | Source-policy Bash/PowerShell validation covers 13 surfaces and 7/7 negative fixtures; intake authoring, 10-target series, full Ready review, 12-preset routing, agent parity 3/3 and secrets checks pass. |
| Documentation/A11Y | Pass | DocFX reports 0 warnings/errors; Playwright/Axe passes 2/2; Lynx exposes both languages and the Submit/Accept/registry contracts without visual-only meaning. |
| Conformance follow-through | Pass | Inventory is extended to 147 historical sources and 235 public types with reciprocal module, public-type, decision and capability links; feature 031 binds the new accepted audit hash. |
| Remote delivery authority | Authorized, pending execution | Current user instruction: `DeliveryMode MergeAndSync mit Admin-Bypass`; exact delivery set, PR checks, review state and PreMerge evidence remain mandatory. |

## Security and residual boundaries

The embedded example document has no database, network or arbitrary user-file
path. JSON cannot name CLR types, members, methods or executable artifacts.
Reflection is bounded to the application-authored direct property expression
and lookup of the example's fixed embedded resource; JSON cannot select either
operation.
Malformed persistence fails before a model becomes observable. Secret scans
report zero high-severity findings and gitleaks reports no secrets.

Rollback cannot undo external side effects performed inside a user-provided
property setter. Applications should keep bound setters deterministic and
side-effect-light. Async validators are deliberately submit-time only; live
validation, general binding engines and executable declarative forms remain out
of scope.

## Decision

`LocalValidationCompleteRemoteCloseoutAuthorized`. All local feature acceptance
gates pass. Commit, push, PR, exact-head convergence, narrow admin merge and
default-branch synchronization are the remaining authorized operations.
