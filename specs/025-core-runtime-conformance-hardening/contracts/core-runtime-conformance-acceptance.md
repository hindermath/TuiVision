# Contract: Core Runtime Conformance Acceptance

## Public API Contract

- Existing public methods remain source-compatible; additive `Try`, result,
  context, close, modal, desktop, and drag contracts may be introduced.
- `TEventKind` remains a flags enum for filters; factories accept only concrete
  event kinds.
- Existing `SetFocus` remains callable and delegates to the veto-capable path.
- Existing manual menu/status `Disabled` values remain authoritative and are
  never overwritten as context state.
- Existing Compatibility translator names and behavior remain canonical.
- Every new or changed public member has complete DE-first/EN-second XML docs.
- A required breaking signature or accepted-semantic change is
  `ProductDecision` and stops implementation.

## Finding Acceptance Matrix

| Finding | Required observable contract |
|---|---|
| `F001` | concrete mouse kinds pass; masks, mixed channels and unknown kinds fail before dispatch |
| `F002` | old current owns one pre-mutation veto; rejection preserves focus/data/announcement |
| `F003` | state-specific propagation yields one focused child and owner-local disabled behavior |
| `F004` | one pending slot precedes input; idle occurs only on no-event and releases CPU |
| `F005` | desktop insertion, top/next, tile, cascade and close-all preserve bounds/focus/Z-order |
| `F006` | close removes visible target when accepted; modal result/isolation/cleanup/focus are complete |
| `F007` | View, menu, StatusLine and keyboard share one refreshed command truth |
| `F008` | real ConsoleKeyInfo path uses canonical scan/modifier translation and fallbacks |
| `F009` | one bounded generic session supports capture, target decision, cancellation and keyboard parity |

Each row closes only with `Implemented` or `AlreadySatisfied` and complete
Finding Evidence. A comment, renamed test or normalized-event-only proof cannot
close a row.

## Lifecycle Contract

1. Events become concrete before dispatch.
2. Focus requests validate membership/eligibility and then ask the previous
   current View once before any mutation.
3. Pending events are drained before physical input; idle runs only after no
   event and is followed by CPU release.
4. Desktop operations use one owner-local snapshot and leave a deterministic
   focus/Z-order result.
5. Close requests report closed/vetoed/non-closeable explicitly.
6. Modal execution has one direct session per owner and cleans up in all normal,
   cancel, failure, and shutdown paths.
7. Command context refreshes after focus, handled events and idle; execution
   performs a final check.
8. Drag sessions have exactly one terminal state and always release capture.

## Real-Path Proof Contract

- `F001`: public factory to dispatch rejection boundary.
- `F008`: controlled `ConsoleKeyInfo` through production `TProgram.GetEvent`.
- `F002`/`F003`: actual Group focus/state transition plus focus announcement.
- `F004`/`F007`: actual `TProgram.Run` ordering and command surfaces.
- `F005`/`F006`: actual application/Desktop tree with rendered end state.
- `F009`: actual app loop with pointer and keyboard sessions.
- Visible lifecycle proofs combine concrete state, View-tree identity, focus and
  Buffer/Cell evidence. Direct helper calls are supplemental unless they are the
  public production boundary under test.

## Historical and Modernization Contract

- Matching `tv203s` `.cc` and required headers are cited read-only.
- Free Vision source IDs and immutable commit are cited as secondary evidence.
- No historical/external source file or substantial excerpt is tracked.
- Each material difference explains why the managed C# contract is safer,
  clearer or more maintainable without erasing historical responsibility.

## A11Y and Platform Contract

- Pointer drag has complete arrow/Enter/Escape keyboard parity.
- Focus changes preserve one announcement path and text-first observability.
- No essential state is conveyed only by color, pointer position or timing.
- Keyboard/terminal changes include macOS/Linux and Windows/WSL evidence or an
  explicitly blocking unavailable proof.

## Validation Contract

- `git diff --check` and `dotnet format --verify-no-changes --no-restore` pass.
- Targeted Core, Compatibility, Controls and Drivers Release tests pass.
- Full Release tests pass.
- Canonical Coverlet gate is at least 70 percent in Core, Controls,
  Serialization, Compatibility and Drivers.Console.
- DocFX has zero warnings/errors; Playwright/Axe and UTF-8 text review pass.
- Feature-024 resolution metadata and validators reconcile all nine findings.
- No new package, Wave application, generated output, historical source,
  external source or unresolved breaking decision is in the diff.
- Required remote checks are green and actionable review threads are zero.

## Scope Contract

No `F010`-`F013`, Feature 026/028 implementation, Wave 5/6 application code,
full desktop drag-and-drop protocol, new external runtime dependency, broad
framework rewrite, pointer-only interaction, or historical platform recreation.
