# Quickstart: Terminal.GUI Conformance Audit

## 1. Confirm the Feature Context

```bash
git branch --show-current
jq .feature_directory .specify/feature.json
specify check
```bash

Expected branch and feature directory:

```text
029-tv203-freevision-terminalgui-conformance-audit
specs/029-tv203-freevision-terminalgui-conformance-audit
```bash

Validate the autonomous state:

```bash
bash .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.sh \
  --state specs/029-tv203-freevision-terminalgui-conformance-audit/autonomous-run-state.json
```bash

## 2. Verify the External Pin

```bash
git ls-remote https://github.com/tui-cs/Terminal.Gui.git \
  'refs/tags/v1.9.0' 'refs/tags/v1.9.0^{}'
```bash

Expected values:

```text
4b812e44798f2c7567afec50ba9a9293b6beb6de refs/tags/v1.9.0
d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3 refs/tags/v1.9.0^{}
```bash

Keep the checkout outside the repository. Do not copy source or test fixtures
into TuiVision.

## 3. Review the Vertical Slice

Start with Domain D02 and contracts `C004` through `C006`:

1. Read their canonical Feature-024 rows.
2. Read the relevant historical and accepted Free Vision evidence.
3. Read pinned Terminal.GUI Application, MainLoop, Responder, ConsoleDriver,
   and UnitTests sources.
4. Record source IDs and hashes.
5. Assign exactly one relation per contract.
6. Link the current TuiVision proof and consumer relevance.
7. Reject architecture-only finding arguments.

## 4. Run the Evidence Validator

Before the first test invocation, increment the manual build counter and align
all version fields to `1.29.<patch>.<build>`.

```bash
dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj \
  --configuration Release \
  --filter 'FullyQualifiedName~TerminalGuiConformanceEvidenceTests'
```bash

The initial red result must be the missing Feature-029 dataset. After the
vertical slice and full dataset are present, malformed and inconsistent data
must still fail closed while the accepted dataset passes.

## 5. Review Every Contract and Consumer

- Contract IDs are exactly `C001` through `C048`.
- Domains are exactly `D01` through `D16`.
- Every contract has one relation.
- Every source relation is reciprocal.
- Every relevant consumer row is present.
- `C049+` is absent unless all admission conditions pass.
- Every observation is classified and owned.

## 6. Validate the Feature-030 Handoff

```bash
jq empty specs/029-tv203-freevision-terminalgui-conformance-audit/feature030-handoff.json
```bash

The handoff must contain all observations, owner proposals, dependencies,
proof requirements, and deduplication keys. Both follow-up-document flags must
be false, both Waves must remain blocked, and Feature 030 must be next.

## 7. Run Final Local Validation

Run static checks first, then targeted Drivers tests, full Release tests,
canonical coverage, DocFX, Axe, Lynx, secrets, dependency review, protected
source scans, generated-output scans, and agent parity. Record every command,
build counter, result, skip trigger, and failure boundary in `pr-evidence.md`.

## 8. Deliver

Stage only intended files and run:

```bash
git diff --cached --check
```bash

After PR checks finish, create temporary provider-neutral gate evidence for the
exact reviewed head and validate it with both installed validators. Merge only
after technical and review convergence. Feature 030 does not start inside this
run.
