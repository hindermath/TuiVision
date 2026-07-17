# Wave-5 TP7 Showcase Acceptance Contract

## 1. Closed scope

The accepted set is exactly:

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

No additional example can replace a missing row. Feature 032 remains the
functional baseline. Wave 6 and Feature 034 do not start.

## 2. Three-layer showcase contract

Each example has:

- a concrete visible and focusable main component built from existing
  TuiVision controls or a bounded Wave-5 presentation view;
- a real `TStatusLine` that reports current state in text;
- a keyboard-reachable `Help -> Description` path.

The first frame states the example's purpose. Mouse interaction is optional
and never the only path.

## 3. Description contract

Each Description is German first and English second at CEFR-B2. It explains:

1. historical learning purpose;
2. complete keyboard operation;
3. intentional modern C# deviation;
4. relevant security, file, parser, or capability boundary;
5. primary proof boundary.

Generic text that does not name the app-specific boundary fails acceptance.

## 4. Primary proof contract

Each example has one primary smoke that:

1. constructs the real application with a deterministic driver;
2. queues keyboard, command, or supported mouse events;
3. executes `app.Run()`;
4. asserts accepted domain state or rejection preservation;
5. asserts concrete main-view identity and focus;
6. asserts real status text;
7. opens and asserts Description through the event path;
8. asserts rendered buffer/cell content in a stable region.

Direct helper calls are `SetupOnly` or `SupplementalProof`.

## 5. Layout contract

Each example declares one constrained viewport. Required purpose, focus or
selection text, main content, status, and Description remain visible without
incoherent overlap or clipped mandatory text.

## 6. Framework decision contract

Every example has exactly one:

- `UseExistingFramework`
- `SmallFrameworkFix`
- `IntentionalDeviation`
- `FollowUpHardening`

`SmallFrameworkFix` requires an observed red test, the smallest fix, and
regression evidence. `FollowUpHardening` records the issue, owner, exclusion
reason, and re-evaluation trigger without implementing it.

## 7. Controlled-boundary contract

- Editor and generator use only source-controlled fixtures or test-owned
  temporary roots.
- Traversal, implicit overwrite, and ambiguous conflict decisions fail closed.
- Resource types remain allowlisted and keys remain ordinal and exact.
- Invalid Resource or Help input publishes no partial model.
- Mouse settings remain local state, capability is honest, keyboard parity is
  complete, and `HostMutationPerformed` is false.

## 8. Historical contract

The matching 15 `TVDEMOS/*.PAS` files remain byte-for-byte unchanged and
provide only learning purpose, user flow, component family, and command
meaning. The modern implementation does not copy Pascal object layout, DOS
runtime behavior, globals, overlays, or host mutation.

## 9. Evidence validator contract

The final evidence contains exactly ten unique showcase rows. Validation
rejects:

- missing, duplicate, or unknown example IDs;
- unknown or multiple framework decisions;
- missing main, status, Description, keyboard, normal, or constrained proof;
- empty focus/selection or fallback text;
- host mutation or a weakened file/Resource/Help boundary.

LF and CRLF documents must produce the same result.

## 10. Documentation and delivery contract

All ten guides describe actual showcase operation in semantic
German-first/English-second text. DocFX and Playwright/Axe pass. The exact
reviewed head passes required local and remote gates, has zero actionable
threads, and is merged under repository policy. The repository then returns
to clean synchronized `main`; a causal closeout is allowed only for facts that
cannot truthfully exist before merge.
