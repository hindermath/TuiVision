# Plan Review Checklist With Execution Guidance

**Purpose**: Execute a reviewer-grade plan inspection before generating tasks  
**Created**: 2026-07-11  
**Plan**: [plan.md](../plan.md)

## Baseline And Scope

- [x] PRV001 **Check baseline traceability.** Compare plan scope with feature 014 evidence and the binding Lastenheft; ensure no accepted function is silently redefined. **Result:** Baseline and second-stage boundary are explicit.
- [x] PRV002 **Check excluded work.** Search plan/research/contract for Wave-2/3/4, dependencies, broad framework, mouse, persistence, service, AI, and historical-write language; every occurrence must remain an exclusion or trigger. **Result:** No scope leak found.
- [x] PRV003 **Check independently reviewable slices.** Trace `MsgCls`, Desklogo, Tutorial, and Videomode from failing proof to runtime and evidence. **Result:** Vertical-slice and follow-on order is dependency-safe.

## Runtime Design

- [x] PRV004 **Check shared-helper ownership.** Compare proposed shared code with `Wave2Runtime.cs` and framework controls; retain only Wave-1 presentation policy and route framework behavior through the decision gate. **Result:** `examples/Shared/Wave1Runtime.cs` is bounded to composition.
- [x] PRV005 **Check project integration.** Identify every project that must link the shared helper and ensure tasks can serialize project-file changes. **Result:** Four Wave-1 project files are the only planned links.
- [x] PRV006 **Check real status behavior.** Confirm the design targets `TStatusLine`, names the exception rule, and includes rendered proof. **Result:** Contract C2 and proof C4 cover it.
- [x] PRV007 **Check Help/Description behavior.** Confirm keyboard reachability, bilingual content, close behavior, and rendered proof are taskable. **Result:** Runtime and proof designs define the path.
- [x] PRV008 **Check Desklogo integrity.** Ensure status/help additions preserve logo area, clipping, and quit without artificial mutation. **Result:** Explicit in spec, research, and contract.
- [x] PRV009 **Check MsgCls repeatability.** Follow command to broadcast, window update, status, repeated trigger, description, and quit. **Result:** Complete vertical slice.
- [x] PRV010 **Check Tutorial mapping.** Map every token to a representative existing control/state and historical intent; avoid generic token-only cards. **Result:** Concrete 16-row map added to plan.
- [x] PRV011 **Check Tutorial fallback.** Define no-token default, unknown-token fallback, and proof exclusion from the 16 valid rows. **Result:** Spec, data model, and contract align.
- [x] PRV012 **Check Videomode truthfulness.** Map coordinator outcomes to exactly four user states and require post-operation usability. **Result:** Explicit across artifacts.

## Test And Evidence Design

- [x] PRV013 **Check test-first order.** For each slice, require failing matrix/smoke proof before runtime edits. **Result:** Phase 2 ordering is test-first.
- [x] PRV014 **Check proof layers.** Inspect planned assertions for app-loop, concrete state, view identity, buffer/cells, status, and description. **Result:** Contract C4 is complete.
- [x] PRV015 **Check helper classifications.** Ensure no `PrimaryProof` direct-helper shortcut can satisfy the visual matrix. **Result:** Only `None`, `SetupOnly`, or `SupplementalProof` are accepted for primary rows.
- [x] PRV016 **Check proof exceptions.** Require reason, substitute proof, owner/follow-up, and trigger when a render layer is impossible. **Result:** Data model and contract define the fields.
- [x] PRV017 **Check evidence schema.** Compare spec entities with data-model fields and contract gates; require complete rows before completion. **Result:** No empty or ambiguous row type remains.
- [x] PRV018 **Check framework decisions.** Require exactly one accepted decision per example/shared area and complete follow-up fields. **Result:** Four exact terms are consistent.

## Governance And Validation

- [x] PRV019 **Check preset versions.** Compare plan versions with local preset manifests. **Result:** 0.6.0/0.5.0/0.2.0/0.4.0/0.2.0/0.3.0 match.
- [x] PRV020 **Check trigger-based N/A decisions.** Ensure ASVS, supply chain, AI-SBOM, regulatory, architecture, BSI C3A/C5, and scripts each have a re-evaluation trigger. **Result:** Plan and data model require it.
- [x] PRV021 **Check A11Y scope.** Trace keyboard/text-first/bilingual requirements through runtime, guides, DocFX, and axe. **Result:** Full triggered path is planned.
- [x] PRV022 **Check agent parity.** Inspect all five maintained surfaces and update plan markers together. **Result:** All five now carry 017 planning context.
- [x] PRV023 **Check build versioning.** Ensure each build/test increments Build once and commit/push alignment does not increment without a new build/test. **Result:** Plan and quickstart are explicit.
- [x] PRV024 **Check validation scale.** Require targeted smoke, full tests, coverage, formatting, docs/A11Y, diff hygiene, and remote OS checks. **Result:** No required gate omitted.
- [x] PRV025 **Check generated-output hygiene.** Require `_site`, API YAML, caches, logs, and test output to remain untracked. **Result:** Spec, contract, and quickstart agree.
- [x] PRV026 **Check causal delivery closure.** Do not mark merge or main-sync tasks complete before the remote action; use a closeout change only if needed after merge. **Result:** Contract C11 and task-generation guidance preserve causality.

## Result

All 26 review points were executed. One material planning improvement was made:
the exact Tutorial visual-target map was added. No unresolved plan issue remains.
