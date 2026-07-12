# Research: Wave-3 Visual Component Porting

## R1 - Feature 018 Baseline

**Decision**: Reuse `TFileEditor`, `TEditWindow`, `THelpFile`, `THelpWindow`,
`THelpSourceCompiler`, and `TLocalizedResourceLookup` as the accepted baseline.

**Rationale**: Feature 019 demonstrates these contracts in visible applications;
reimplementation would create local special logic and duplicate Feature 018.

**Rejected**: Reopening 018 or writing generic editor/help/compiler logic inside
`examples/`.

## R2 - Vertical Slice

**Decision**: Implement `TvEdit` first with grouped red proof for first frame,
edit/modified state, controlled path, and safe close.

**Rationale**: It exercises the broadest existing framework chain while remaining
bounded to one project. It can test whether grouping project-local negative
boundaries reduces build cycles without weakening test-first traceability.

**Rejected**: `I18n` is smaller but does not prove nested view/editor/focus or
safe-close behavior. Implementing all five before the first proof would increase risk.

## R3 - Shared Presentation Boundary

**Decision**: Link `examples/Shared/Wave3Runtime.cs` into all five projects for
status drawing, help menu, description window, and stable proof-region conversion.

**Rationale**: These are repeated example-presentation concerns, not domain
behavior. A Wave-3-specific file avoids changing Wave-1/Wave-2 helpers.

**Rejected**: Five copies violate the reuse gate. A public framework API is not
justified while existing controls are sufficient.

## R4 - BHelp Historical Boundary

**Decision**: Preserve visible topic viewing, navigation, context/search-style
selection, and unavailable-help behavior using the modern `THelpFile` model.
Classify omission of the proprietary Borland `.tch` binary decoder as
`IntentionalDeviation`.

**Rationale**: `bhelp.cc`, `bhelp.h`, and `thelp.cc` show the learning intent but
also contain unchecked native binary parsing that is outside this safe example run.

**Rejected**: A mechanical decoder port, unsafe compatibility parser, or new
native dependency.

## R5 - HelpDemo Context Flow

**Decision**: Preserve focus-dependent help contexts, hint/status feedback,
help commands, and visible topic/fallback result with current controls.

**Rationale**: `helpdemo.cc` primarily teaches the connection between menu,
status hints, focused controls, context IDs, and command dispatch.

**Rejected**: Static help text with no focus/command path.

## R6 - Deterministic i18n

**Decision**: Use explicit neutral and Spanish dictionaries with
`TLocalizedResourceLookup`, selected by application command.

**Rationale**: The historical example forces `LANG=es` and gettext. Feature 018
provides a deterministic managed lookup with ordered fallback and no host state.

**Rejected**: Mutating process locale, relying on installed gettext catalogs, or
adding a localization dependency.

## R7 - TvHc Compiler Flow

**Decision**: Use `THelpSourceCompiler` for controlled `.topic` source,
diagnostics, cross-references, aliases, and resulting `THelpFile` topics.

**Rationale**: The historical `tvhc.cc`, `tvhc.h`, and `demohelp.txt` define the
compiler's teaching intent; Feature 018 already supplies bounded strict parsing.

**Rejected**: Re-porting global-buffer native compiler code, arbitrary command-
line file discovery, or persistent output outside test-owned temp paths.

## R8 - Primary Visual Proof

**Decision**: Require real app-loop dispatch plus concrete state, view-tree, and
rendered cell proof for each example.

**Rationale**: This is the accepted Wave-1/Wave-2 pattern and directly excludes
startup-only or helper-only proof.

**Rejected**: Screenshot-only tests, direct app methods as primary proof, or
status-string assertions without main-surface evidence.

## R9 - Controlled I/O and Threat Review

**Decision**: Use embedded/source-controlled input and unique test-temp output;
reject unknown or malformed states explicitly and keep visible status safe.

**Rationale**: The changed trust boundary is local untrusted file/source input.
STRIDE/CIA/CAPEC review is proportional; no network/cloud boundary exists.

**Rejected**: Home-directory discovery, persisted user history, implicit current-
directory writes, or silently partial compiler output.

## R10 - Governance Applicability

**Decision**: Apply NIST SSDF/CWE, secure coding, iSAQB reuse review, A11Y,
cross-platform runtime proof, and agent parity. Keep ASVS, new supply-chain
artifacts, AI-SBOM/regulatory, S-ADR/Zero Trust/SAMM/C3A/C5, and script governance
trigger-based `N/A` unless actual scope changes.

**Rationale**: Evidence should remain proportional and auditable.

## R11 - Documentation and Delivery

**Decision**: Add five bilingual guides, update navigation/index, run DocFX and
axe, and use `MergeAndSync` with explicit evidence paths on every remote task.

**Rationale**: Learner-facing documentation and authorized remote completion are
part of acceptance, not optional post-work.

## R12 - Generic Workflow Promotion Boundary

**Decision**: Feature 019 records field observations but does not modify generic
autonomous templates. Any proven workflow correction is handled after merge on
a separate non-empty retrospective PR and then handed to Home Baseline.

**Rationale**: This prevents implementation scope from silently absorbing preset
productization.
