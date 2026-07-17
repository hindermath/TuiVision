# Wave-6 TVFM Showcase Acceptance Contract

## 1. Closed scope

The accepted application set is exactly one existing entry point:
`Tp7FileManager`.

The accepted showcase-area set is exactly:

1. `W6S-001` navigation and list
2. `W6S-002` text and hex preview
3. `W6S-003` filter, sort, and tags
4. `W6S-004` search and cancellation
5. `W6S-005` association and internal viewer
6. `W6S-006` copy, rename, delete, and read-only dialogs
7. `W6S-007` drag-and-drop intent
8. `W6S-008` palette and resources
9. `W6S-009` Help and Description
10. `W6S-010` status, focus, and layout

No additional row can replace a missing member. Feature 037, independent
Wave-6 closure, and the post-Wave-6 portfolio audit are not created or
started.

## 2. Preserved functional and authority contract

- Feature-035 domain services and models are reused, not re-ported.
- UI code passes root-relative values into `ControlledFileWorkspace`.
- UI code performs no direct filesystem mutation.
- Controlled-root, path, link, preview, search, viewer, intent, conflict,
  revalidation, mutation, and recovery contracts remain unchanged.
- `TVFM/`, `TVDEMOS/`, `tv203s/`, and external comparison sources remain
  byte-for-byte unchanged.
- No arbitrary user, network, device, drive, shell, process, PTY, external
  viewer, or host-manager access is introduced.

## 3. Visible application contract

`Tp7FileManager`:

- starts normally and with `--smoke`;
- shows purpose, controlled root/path, list, selection, and primary controls
  in the first frame;
- uses one persistent visible main composition with focusable controls;
- provides a real StatusLine and F1/Help Description;
- exposes every accepted core command through a menu, control, dialog, or
  honest status path;
- provides full keyboard access and optional mouse parity;
- keeps normal and `48x16` views understandable and operable;
- exits deterministically through `Ctrl+Q`.

## 4. Menu and command contract

- File, Navigate, View, Search, Options, and Help groups cover the accepted
  operations.
- Every primary command has one stable command ID, visible label, keyboard
  path, enablement rule, and status hint.
- Unavailable commands are disabled or explained, not hidden behind
  test-only access.
- Menu organization is a modern interpretation and does not claim exact
  Pascal source or DOS host parity.

## 5. Dialog and mutation contract

Copy, rename, delete, and read-only decisions use existing focusable controls.
Every applicable flow includes:

1. selected source;
2. bounded target or leaf-name input;
3. normalized root-relative Preview;
4. visible validation and safety boundary;
5. explicit Confirm or Cancel;
6. immediate Feature-035 revalidation;
7. text-first terminal result or rejection.

Cancel, Escape, invalid input, missing confirmation, target conflict, stale
intent, path escape, or link boundary performs no unauthorized mutation.
Delete remains non-recursive and copy/rename never silently overwrite.

## 6. Mouse contract

- Mouse drag starts only from an existing selected fixture entry.
- The mouse path prepares the same operation intent as the keyboard path.
- Mouse release never executes a mutation.
- Invalid target, Escape, capability loss, view removal, and shutdown cancel
  with `NoMutation`.
- Hover, wheel, touch, multiple selection, and general desktop drag/drop are
  outside scope.

## 7. Primary proof contract

Primary showcase proof:

1. constructs the real application with deterministic driver/buffer state;
2. queues real key, command, mouse, and dialog events;
3. calls `app.Run()` or a proven equivalent dispatch loop;
4. asserts concrete application and Feature-035 functional state;
5. asserts concrete view and focused-control identity;
6. asserts StatusLine, Description, and visible text;
7. asserts rendered buffer/cell content in normal and `48x16` regions;
8. asserts controlled exit and filesystem result/non-result.

Direct helpers remain setup or supplemental proof.

## 8. Framework and completion decisions

Every `W6S-###` row has exactly one:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

`SmallFrameworkFix` requires prior red evidence and bounded reusable
regression proof. A broad or unowned gap becomes `FollowUpHardening`.

The one entry-point row has exactly one:

- `ShowcaseComplete`
- `IntentionalMinimalSurface`
- `FollowUpHardening`
- `ProductDecision`

`ProductDecision` stops delivery. No accepted completion may retain an open
`ShowcaseDelta`.

## 9. Evidence contract

The final evidence contains exactly ten area rows and one entry-point row.
Every area records Feature-035 proof, visible access, normal and constrained
proof, focus/status/Description/keyboard proof, framework use, local
composition, historical intent/deviation, filesystem/A11Y/platform/security
boundaries, decision, residual risk, and re-evaluation trigger.

The validator rejects missing, duplicate, unknown, incomplete, `Planned`, or
`Open` accepted rows and inconsistent final decisions.

## 10. Documentation and A11Y contract

The guide and Description are semantic DE-first/EN-second CEFR-B2 content
covering purpose, source, launch, menus, keyboard, optional mouse path,
filesystem safety, constrained layout, platform fallback, modernization, and
proof boundaries. Focus and state are text-first, High Contrast is supported,
and no essential function relies on color or pointer input alone.

## 11. Delivery contract

The exact reviewed head passes all declared local and provider gates. Missing
reviewers remain missing, not passed. Merge requires zero actionable threads.
The authorized narrow bypass applies only when Human Approval is the sole
open rule and every technical gate is green.

After merge, the repository returns to clean synchronized `main`. A causal
closeout is allowed only for post-merge facts that cannot truthfully exist on
the reviewed feature head and must remain non-recursive.
