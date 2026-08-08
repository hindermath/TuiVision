# Abnahmevertrag: Wave-6 Combined Delta Closure

## 1. Input Contract

1. PR #101 and #104 are the only product deliveries.
2. PR #102 and #105 are causal closeouts; PR #103 is metadata-only.
3. Base, head, merge, changed files and set hashes are exact.
4. Binding predecessor files are pinned by path and SHA-256.
5. `TVFM/`, `TVDEMOS/`, `tv203s/` and comparison sources stay read-only.

## 2. Cardinality Contract

- exactly 24 historical source records;
- exactly ten functional proof records;
- exactly ten showcase proof records;
- exactly ten combined area records;
- exactly one `Tp7FileManager` entry point;
- exactly one primary decision for each combined area;
- no missing, duplicate, unknown, orphan or non-reciprocal relation.

LF and CRLF produce the same canonical hash for `.PAS` and `.BAT`; `.PAL` and
`.TVR` remain byte-exact.

## 3. Combined Contract

Each combined row links historical intent, Feature-035 function, Feature-036
showcase, visible access, app-loop, state, view, focus, dialog where relevant,
StatusLine, F1 Description, keyboard/quit, cells, framework ownership, local
composition, safety, A11Y, platform, evidence, risk and trigger.

## 4. Decision Contract

Allowed decisions are `AcceptedAsIs`, `AcceptedIntentionalDeviation`,
`CandidateFinding` and `ProductDecision`. Allowed dimensions are `Pass`,
`IntentionalDeviation`, `Gap` and `N/A`.

- accepted rows contain no `Gap`;
- intentional deviations explain historical intent and modern boundary;
- findings include `W6D###`, reproduction, evidence, owner and follow-up;
- a product decision or finding stops the run;
- source-style differences alone do not create findings.

## 5. Framework Contract

`Tp7FileManagerApp`, `Wave6ShowcaseWindow`, `ControlledFileWorkspace` and local
state models may compose this one learning example. A candidate finding is
required only when local logic replaces a framework contract or is reusable by
an independent consumer. Feature 037 does not move or modify code.

## 6. Proof Contract

Primary evidence executes `app.Run()` or an equivalent real application loop
and combines state, concrete view/focus identity, real StatusLine, Description
and rendered cells. Direct helpers remain supplemental. The entry point keeps a
controlled `--smoke` start and a bounded normal PTY start with primary action,
F1 and `Ctrl+Q` evidence.

## 7. Safety and A11Y Contract

Filesystem operations remain controlled-root, explicit, confirmed, one-shot
and fail-closed. No arbitrary host discovery, shell, external viewer, network,
locale or time dependency is introduced. Pointer actions retain keyboard
parity. Focus, status and Description remain text-first and learner-facing
evidence remains German-first/English-second at CEFR B2.

## 8. Validation Contract

Positive and negative closure tests, targeted Wave-6 tests, full Release,
canonical five-assembly coverage, format, DocFX/Axe, UTF-8, secret, supply
chain, scope and agent parity must pass locally where applicable. Ubuntu,
macOS and Windows exact-head evidence must pass before merge and must not be
inferred from workflow names or predecessor runs.

## 9. Wave Contract

The reviewed feature head remains `Wave6 = BlockedPendingDelivery` and
`PortfolioAudit = BlockedPendingWave6Closure`. Successful local validation
sets only `ReadyForDelivery`. Actual `Closed` and `Eligible` facts require the
authorized merge and exactly one causal evidence-only closeout.

## 10. Delivery Contract

Delivery mode is `MergeAndSync`. Commit, push, one non-empty feature PR,
exact-head provider/review convergence, merge commit and a genuinely required
non-empty causal closeout are allowed. Bypass is limited to the Human Approval
rule after all technical gates pass and no actionable thread remains. Feature
038 is not created or started. The run ends with complete tasks,
retrospective, terminal state, clean synchronized `main` and
`nextExactAction: N/A`.
