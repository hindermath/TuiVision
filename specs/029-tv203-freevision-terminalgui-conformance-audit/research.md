# Research: TV203, Free Vision, and Terminal.GUI Conformance Audit

## Decision 1: Keep Feature 024 canonical

**Decision**: Preserve Feature-024 `conformance-audit.json` as the canonical
TV203/Free Vision dataset and create a separate Feature-029 Terminal.GUI
relation and handoff dataset.

**Rationale**: A third-source audit must not rewrite accepted historical
observations, findings, resolutions, or Free Vision relations.

**Alternatives rejected**: add Terminal.GUI fields directly to Feature 024;
copy the full 024 dataset; keep the third-source review only in prose.

## Decision 2: Use the exact Terminal.GUI v1.9.0 tag and peeled commit

**Decision**: Use repository `https://github.com/tui-cs/Terminal.Gui.git`,
annotated tag object `4b812e44798f2c7567afec50ba9a9293b6beb6de`,
and peeled commit `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3`.

**Rationale**: `git ls-remote` and direct tag inspection confirm the binding
values. Immutable source identity makes every path and SHA-256 reproducible.

**Alternatives rejected**: Terminal.GUI v2; current main; a later v1 commit;
NuGet binaries without corresponding source.

## Decision 3: Treat Terminal.GUI as advisory modern C# evidence

**Decision**: Compare observable responsibilities and proof boundaries, not
class names, inheritance, static APIs, rendering architecture, or extra
features.

**Rationale**: Turbo Vision remains normative, while Free Vision and
Terminal.GUI are independent implementation opinions. Modern designs can
differ while both remain valid.

**Alternatives rejected**: architecture parity; mechanical API mapping;
class-by-class recreation.

## Decision 4: Store selected source records, not an exhaustive upstream tree

**Decision**: Create stable `TGSR###` source records for the production and
UnitTests files needed to explain the 16 contract domains. Each record stores
path, SHA-256, behavior summary, source kind, license relation, and retrieval
date.

**Rationale**: The contract audit needs reproducible evidence but not a copy of
the upstream repository or one record per unrelated file.

**Alternatives rejected**: track the external checkout; copy source excerpts;
record only broad directory names without hashes.

## Decision 5: Add a separate test-only validator

**Decision**: Add `TerminalGuiConformanceEvidenceTests.cs` to
`TuiVision.Drivers.Tests` and validate the Feature-029 datasets with
`System.Text.Json`.

**Rationale**: The Drivers test project already owns Feature-024 and
Feature-028 evidence validators and references the framework assemblies needed
to verify proof paths. A test-only validator cannot become runtime behavior.

**Alternatives rejected**: production parser; new package or project; ad-hoc
shell text matching.

## Decision 6: Use D02/C004-C006 as the vertical slice

**Decision**: Establish the red missing-dataset proof, then add Event, Command,
and Dispatch relations using Terminal.GUI Application, MainLoop, Responder,
ConsoleDriver, and related UnitTests evidence.

**Rationale**: D02 crosses input, routing, handling, and proof boundaries and
is representative of the later relation pattern.

**Alternatives rejected**: start with simple geometry only; populate all 48
rows before validating one complete relation; use a documentation-only slice.

## Decision 7: Require exact contract cardinality and reciprocal links

**Decision**: Require exactly `C001` through `C048`, exactly one Terminal.GUI
relation per contract, valid domain IDs, valid source IDs, existing TuiVision
proof paths, and reciprocal source-to-contract links.

**Rationale**: Fixed cardinality makes omissions, duplicates, and one-sided
evidence observable.

## Decision 8: Do not create new contracts by default

**Decision**: Record zero `C049+` contracts unless review proves a material
consumer responsibility with historical or justified modern ownership,
TuiVision source/proof review, Terminal.GUI evidence, and no existing contract
coverage.

**Rationale**: New contracts should represent new framework responsibility,
not vocabulary differences or upstream extras.

## Decision 9: Keep findings provisional until Feature 030

**Decision**: Record `TG*` observations with complete decisions and provisional
owner/dependency data, but create no hardening or closure Lastenheft.

**Rationale**: Feature 030 must compare magiblot/tvision and deduplicate `TG*`
and `MB*` evidence into canonical `CF*` findings.

## Decision 10: Keep delivery truth separate from audit truth

**Decision**: Commit source, relation, consumer, observation, governance, and
local-validation facts. Keep exact reviewed-head provider evidence temporary
and use one causal closeout only when post-merge facts cannot be stated before
merge.

**Rationale**: A commit that records its own current reviewed head would
invalidate that statement.

## Decision 11: Trigger full tests and coverage

**Decision**: Run targeted Drivers tests, the full Release suite, and canonical
coverage because a shared evidence-validator test file is added.

**Rationale**: The validator is test-only but participates in a shared project
and reads canonical repository evidence. Full validation prevents accidental
test-infrastructure or project-reference regressions.

## Decision 12: Keep script parity conditional

**Decision**: Add no repository script. Use existing Bash and PowerShell
autonomous validators and archive helpers.

**Rationale**: The feature needs data and MSTest validation, not another
cross-platform command surface.

## Resolved External Evidence

| Item | Verified value |
|---|---|
| Repository | `https://github.com/tui-cs/Terminal.Gui.git` |
| Tag | `v1.9.0` |
| Annotated tag object | `4b812e44798f2c7567afec50ba9a9293b6beb6de` |
| Peeled commit | `d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` |
| License | MIT |
| License SHA-256 | `2a7331c273b7c121f5e1f6f10e13d279a739ac310c49b56f2fb251d0490988d0` |
| External checkout | temporary and untracked under `/tmp` |

No unresolved research question remains.
