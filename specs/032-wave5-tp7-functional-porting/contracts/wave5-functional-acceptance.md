# Wave-5 Functional Acceptance Contract

## 1. Closed scope

The accepted source set is exactly the 15 files named in Lastenheft 17. The
accepted consumer set is exactly `W5-001` through `W5-006`. The accepted
managed example set is exactly:

1. `Tp7Demo`
2. `Tp7Edit`
3. `Tp7Help`
4. `Tp7ResourceDemo`
5. `Tp7ResourceGenerator`
6. `Tp7AsciiTable`
7. `Tp7Calculator`
8. `Tp7Calendar`
9. `Tp7Puzzle`
10. `Tp7MouseDialog`

No additional example can satisfy or replace a missing member.

## 2. Runnable example contract

Each example:

- has an independent executable project and documented `dotnet run --project`
  command;
- shows purpose and deterministic current state on its first frame;
- exposes at least one keyboard-reachable historical core purpose;
- exits through the existing application command path;
- changes no host configuration and accesses no arbitrary user file.

## 3. Primary proof contract

Each example has at least one primary smoke that:

1. constructs the real application type with a deterministic in-memory driver;
2. queues key, command or mouse events through the application event path;
3. calls `app.Run()` or a proven equivalent real dispatch loop;
4. asserts concrete application/domain state;
5. asserts the relevant concrete view identity;
6. asserts visible buffer/cell content in a stable region;
7. records direct helpers as setup or supplemental only.

Startup-only, direct-method-only and string-only tests are not primary proof.

## 4. Negative and controlled-boundary contract

- Calculator division by zero preserves the last valid value.
- Calendar and puzzle proofs use fixed fixtures, not host time or randomness.
- Invalid puzzle moves preserve the board.
- Invalid Help input publishes no partial model.
- Unknown Resource type, duplicate key, invalid length or unallowed record is
  rejected atomically.
- Editor and generator writes stay below an explicit test-owned root.
- Mouse settings remain local example state and `HostMutationPerformed` is
  false.
- Every mouse flow has an equivalent complete keyboard path.

## 5. Framework decision contract

Each consumer has exactly one decision:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

The accepted starting and expected final decision is
`UseExistingFramework`. `SmallFrameworkFix` requires an observed red test
before the change. `FollowUpHardening` stops the affected slice and records an
owner and later intake boundary.

## 6. Source traceability contract

Every source has exactly one primary role:

- `EntryPoint`
- `SupportUnit`
- `FixtureOrContent`
- `GeneratorIntent`
- `IntentionalOmission`

Every row records historical purpose, modern target, intentional deviation and
proof. Historical and external sources remain byte-for-byte unchanged.

## 7. Showcase delta contract

The final matrix contains exactly ten rows. Each row has one disposition,
delivered functional state, and explicit Visual, Interaction, Layout and A11Y
dimensions. A later showcase Lastenheft may be created from these rows, but no
Feature-033 branch, directory, specification or implementation starts in 032.

## 8. Documentation and A11Y contract

Every example has one semantic DE-first/EN-second CEFR-B2 guide covering:

- purpose and source;
- launch command;
- keyboard operation;
- controlled data or capability boundary;
- intentional modernization;
- A11Y/text-first behavior;
- primary proof and Stage-2 delta.

DocFX must build with zero warnings/errors and Playwright/Axe must pass.

## 9. Delivery contract

The exact reviewed feature head must pass all declared gates. Missing reviewers
remain missing, not passed. Merge requires zero actionable threads. The
authorized narrow bypass applies only when Human Approval is the sole open
rule and every technical gate is green.

After merge, the repository returns to clean synchronized `main`. A causal
closeout is allowed only for post-merge facts that cannot truthfully be
committed on the reviewed feature head.
