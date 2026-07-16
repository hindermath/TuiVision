# Quickstart: magiblot/tvision Evolution Audit

## 1. Confirm the Feature Context

```bash
git branch --show-current
cat .specify/feature.json
specify check
```

Expected branch and feature directory are
`030-tv203-magiblot-evolution-audit`.

## 2. Verify the External Pin

Use a detached checkout outside TuiVision and verify:

```bash
git rev-parse HEAD
git rev-parse HEAD^{tree}
git show -s --format='%cI%n%s' HEAD
shasum -a 256 COPYRIGHT
```

Compare with the exact values in `research.md`. Do not build or copy upstream
source.

## 3. Review the Vertical Slice

Start with D02 and contracts `C004`-`C006`. Confirm source records, relations,
MB observations, TG/MB dispositions, consumer/proof links, and shared-bias
entries before expanding repeated rows.

## 4. Run the Evidence Validator

Before the first test command, increment the manual build counter once. Then
run the isolated Feature-030 test filter in Release configuration. The initial
red boundary may be only the missing accepted Feature-030 datasets.

## 5. Review Every Contract and Consumer

Validate one relation and one MB observation per accepted contract, complete
consumer cardinality, all fourteen comparison chapters, and reciprocal source
and proof links.

## 6. Validate Combined Findings and Follow-ups

Confirm one disposition per TG/MB observation, zero duplicate CF findings, one
Primary Owner per finding, an acyclic DAG, only non-empty hardening intakes,
and exactly one final closure intake.

## 7. Recover an Interrupted Run

Run status read-only, confirm the general autonomous command refuses implicit
continuation, then use explicit resume with renewed `MergeAndSync` authority.
Reconstruct uncertain operations before repeating them.

## 8. Run Final Local Validation

Run static checks first, then targeted tests, full Release, canonical coverage,
DocFX/Axe/Lynx, scope and secret scans, and agent parity according to the final
diff. Record every command and counter in `pr-evidence.md`.

## 9. Deliver

Stage only intended files, validate cached diff and inventory, commit and push,
create the PR, converge exact-head checks and reviews, merge under authorized
policy, prune, switch to `main`, pull, and prove a clean synchronized tree.
