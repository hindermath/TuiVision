# Plan Quality Checklist: Port Wave 2 Examples

**Purpose**: Validate `plan.md`, `research.md`, `data-model.md`,
`contracts/wave2-example-acceptance.md`, and `quickstart.md` before task
generation.
**Created**: 2026-05-06
**Feature**: [spec.md](../spec.md), [plan.md](../plan.md)

## Requirement Completeness

- [x] CHK001 Every wave-2 example from `Pflichtenheft.md` appears in plan,
  data model, contract, and quickstart.
- [x] CHK002 Each example has a planned project, smoke-test class, and guide.
- [x] CHK003 Smoke requirements include an example-specific deterministic
  interaction and visible-result assertion.
- [x] CHK004 `sdlg` and `sdlg2` are scoped to historical
  `ScrollDialog`/`ScrollGroup` behavior, not standard-dialog ownership.
- [x] CHK005 Standard-dialog proof remains assigned to `demo`, `dlgdsn`, or a
  historically justified wave-2 flow.

## Scope Clarity

- [x] CHK006 Wave-3/4 examples cannot satisfy wave-2 acceptance.
- [x] CHK007 Editor, help, stream, terminal emulation, runtime mouse, and real
  charset effects are excluded from wave-2 acceptance.
- [x] CHK008 File-content I/O is excluded from standard-dialog acceptance.
- [x] CHK009 Historical Example Parity Cleanup is non-blocking and scheduled
  no earlier than after mandatory waves 1-4.

## Governance Coverage

- [x] CHK010 Constitution checks cover branching, versioning, .NET 10/C# 14,
  MSL, security, architecture, A11Y, statistics, and agent parity.
- [x] CHK011 Architecture evidence under `docs/architecture/` is explicitly
  planned.
- [x] CHK012 Existing `docs/security/` evidence is referenced with justified
  ASVS/Zero-Trust N/A conditions.
- [x] CHK013 DE-first/EN-second CEFR-B2 guide work is planned for every new
  example.
- [x] CHK014 DocFX and Playwright/axe are conditional on public API or generated
  documentation output changes.

## Task-Generation Readiness

- [x] CHK015 Project structure names concrete target directories and files.
- [x] CHK016 Testing strategy distinguishes example smoke tests from focused
  framework tests.
- [x] CHK017 Quickstart includes build, test, coverage, format, and conditional
  documentation validation.
- [x] CHK018 Success criteria traceability maps every SC to a plan evidence
  path.

## Review Result 2026-05-06

- All checklist items were reviewed while creating the plan artifacts.
- No additional user clarification was required before `/speckit-tasks`.
