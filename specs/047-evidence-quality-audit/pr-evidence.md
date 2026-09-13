# PR Evidence: Feature 047

## Scope

Feature 047 is a local, read-only evidence-quality audit of Features 044, 045,
and 046. It does not modify audited evidence, product code, dependencies,
workflows, examples, or the completed intake series.

## Input Baseline

The canonical input hashes are recorded in the validated intake-authoring
receipt. The final audit report repeats and verifies those hashes.

## Validation Ledger

| Command | Result | Boundary |
|---|---|---|
| Intake receipt validator, Bash and PowerShell | PASS | 9 sources |
| Intake review validator, Bash and PowerShell | PASS | Single, Ready |
| `python3 .../render_evidence.py --check` | PASS | 12/157 rows, derived summaries |
| `python3 -m unittest .../test_evidence_quality.py` | PASS, 7/7 | Positive and malformed-data cases |
| `git diff --check origin/main...HEAD` | PASS after independent-review remediation | Entire committed Feature-047 delta, including Markdown EOF and trailing-space checks |
| `dotnet format --verify-no-changes --no-restore` | PASS | No C# changes |
| `scripts/scan-agent-secrets.sh --fail-on-high .` | PASS | high=0; existing local `.claude` configuration reported Medium |
| Audited-input normalized SHA-256 comparison | PASS, 9/9 | Inputs unchanged |
| Feature allowlist and forbidden-root check | PASS | No product, example, workflow, or historical-source change |
| `specify check` | PASS | Local toolchain available |
| Prerequisite check with tasks | PASS | Feature 047 resolved |
| Autonomous state validator, Bash and PowerShell | PASS | Schema 1.1, `Publish`, `Active`, 53/58 before remote closeout |
| Accepted-artifact hash comparison | PASS, 10/10 | State, task, gate contract, and normalized intake binding current |
| `docfx ../../docfx.json` from `tests/web-a11y` | PASS with 19 pre-existing link warnings | 0 errors |
| `npm install` | PASS | 0 vulnerabilities; Node 26.7 engine warning confirms EQA008 |
| `npm run test:docfx` | PASS, 2/2 | Playwright/Axe including project statistics |
| Feature commits and branch push | PASS | Five non-empty commits preceded the causal gate correction on `origin/047-evidence-quality-audit` |
| Repository intake-alignment validator | PASS | Causal delivery correction accepts a reviewed standalone intake only with matching receipt, review, feature state, lifecycle, and accepted-artifact hashes; 5 positive and 21 negative cases |
| Intake-alignment wrappers, Bash and PowerShell | PASS | Both native entry points validate the unchanged completed series and the separate active intake |
| Causal governance PR #181 | PASS, merged as `1054f4c` | All technical gates green; zero actionable review threads; narrow admin bypass used only for the remaining Human Approval rule |
| Independent Antigravity review | APPROVE, zero actionable findings | Exact head `df49962`; transcript SHA-256 `7d7d7de510b9ff744132b606e3128a9ee58cf57c7d6907f0eacc19bc584263dc`; operator trace in PR comment `#issuecomment-5654283478` |
| Provider-neutral PreMerge evidence | PASS, Bash and PowerShell | Exact head `df49962`; snapshot SHA-256 `87d015c1fdfc4da3ceb2bc855ace8afaf81ca7221d3706599c54ae72e7c20114` |
| PR #180 exact-head checks | PASS | All mandatory Ubuntu, macOS, Windows, DocFX, security, package, tooling, and maintenance jobs completed without failure |
| PR #180 delivery | PASS, merge commit `18f3387` | Zero actionable threads; Human Approval was the sole remaining rule; narrow admin bypass used as authorized |
| Local default-branch synchronization | PASS | Clean `main`; `HEAD == origin/main == 18f3387` before this causal closeout |

Two command corrections are retained as proof boundaries. A hash-check helper
first referenced a mistyped, non-existent receipt path and changed nothing; the
corrected 9/9 check passed. The first DocFX command used the web-test directory
without `../../docfx.json` and changed nothing; the corrected command passed.
The statistics renderer correctly reports `DRIFT` while the new manual ledger
row is uncommitted. A temporary clean-copy render was rejected as final evidence
because it bound a temporary commit; the committed generated block was restored.
This LocalImplementation run therefore updates the manual ledger but does not
claim a current generated statistics snapshot before an authorised commit.

## Audit Results

- Feature 044 sandbox controls: `12/12`.
- Feature 045/046 crosswalk: `157/157`, each with exactly one primary decision.
- Feature 046 inventories: language profiles `10/10`, presets `12/12`, agent
  surfaces `123/123`, governance checkpoints `46/46`, evidence families
  `12/12`, and evidence references `988/988`.
- Semantic evidence-reference sample: `50`.
- Mechanical provenance: `916` verified, `66` missing at the claimed commit,
  and `6` hash mismatches.
- Canonical findings: `10` total, comprising `2 High`, `5 Medium`, and `3 Low`.

The canonical result is in
`docs/security/secure-development/2026-09-13-evidence-quality-audit/`.

## Governance Decisions

- Security and evidence governance: applicable; findings remain open and are
  not compliance claims.
- A11Y: applicable to changed reader-facing Markdown; DocFX and Axe passed.
- Architecture, iSAQB, STRIDE/CIA/CAPEC, S-ADR, arc42, Zero Trust, SAMM, BSI
  C3A/C5, cloud, deployment, AI-SBOM, regulatory, and product-AI work: `N/A`
  because no product boundary or architecture changed. Re-evaluate on such a
  scope change.
- Supply-chain remediation: `N/A` for implementation; the mutable action and
  dependency observations remain findings. Re-evaluate when remediation is
  authorised.
- Agent parity: `N/A`; no shared guidance, template, command, or agent surface
  changed. Re-evaluate when shared guidance changes.
- Historical source policy: `N/A`; the audit has no Turbo Vision behavior claim.
- Full .NET tests and coverage: not triggered because no C#, project, package,
  runtime, API, or test-helper surface changed. The Python audit validator and
  documentation gates are the proportional proof.

## Scope Proof

No file under `src/`, `examples/`, `.github/workflows/`, or `tv203s/` changed.
No package, project, API, generated DocFX output, remediation intake, or remote
delivery artifact is part of the tracked result.

The first exact-head CI run exposed a stale repository governance assumption:
the intake-alignment validator authorized completed series members but not a
separately reviewed active intake during `MergeAndSync`. The causal correction
adds that narrowly proven lifecycle without changing the completed series or
granting remote authority. Negative fixtures reject missing receipts, stale
review hashes, and stale accepted-artifact hashes.

The workflow cardinality correction was deliberately separated into PR #181
because Feature 047 forbids workflow mutation. It retains exactly ten archived
Completed series members and zero Eligible series targets while deriving the
valid receipt count as ten plus the number of separately validated active
intakes. Copilot failed at provider level; the Claude review workflow passed and
no actionable review thread existed.

The initial `autonomous-gate-requirements.json` used an internal changed-path
allowlist shape and therefore could not be consumed by the installed schema-2.0
gate-evidence validator. The corrected contract declares six Applicable gates
and one explicit product-scope `N/A`; its remote gate cannot pass until the
current pushed head and review state are final.

An independent local Antigravity review of exact head `b8121da` returned
`CHANGES_REQUESTED` and is retained at SHA-256
`af9556e8b55e48f57ae8018e5ac2a4eb0ab4e35fe53da052f14136a917315414`.
Its actionable findings are addressed together: standalone archive successors
are resolved outside the completed series, completed and local standalone
lifecycles have positive and negative tests, both wrappers validate a current
standalone review, branch-wide whitespace is checked, and delivery wording plus
phase evidence reflect the later explicit authority. A fresh independent review
of corrected exact head `df49962` then returned `APPROVE` with zero actionable
findings. The exact-head GitHub checks all passed and both gate-evidence
validators accepted the same temporary PreMerge snapshot before merge.

The GitHub Claude job was green at workflow level but its internal result had
`is_error: true` and produced no review. Copilot did not submit a review for the
final exact head. Neither provider result was counted as independent approval;
the local Antigravity review satisfied that non-bypassable gate. This green-job
versus failed-inner-result mismatch is a provider-neutral `PresetFollowUp`, not
a reason to alter the Feature-047 audit findings.

## Delivery Boundary

The audit first completed under `LocalImplementation`. The user then granted
`MergeAndSync` with a narrow admin bypass subject to green technical gates,
zero actionable review threads, and Human Approval as the only remaining rule.
PR #180 was merged under that narrow condition as merge commit `18f3387`. The
active intake was then archived through the repository rename workflow, and this
causal evidence-only closeout records the post-merge facts without rewriting the
reviewed feature head.
