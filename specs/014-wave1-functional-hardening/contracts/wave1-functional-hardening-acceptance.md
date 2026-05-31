# Contract: Wave 1 Functional Hardening Acceptance

**Feature**: `014-wave1-functional-hardening`
**Date**: 2026-05-31

This contract defines the observable historical-review, smoke-test, evidence,
documentation, and governance obligations for Wave-1 functional hardening.

## 1. Common Scope Contract

The feature MUST cover only:

- `Desklogo`
- `MsgCls`
- `Tutorial` steps `tvguid01` through `tvguid16`
- `Videomode`

The feature MUST NOT:

- implement Wave 2, Wave 3, or Wave 4 behavior;
- complete Wave-1 visual component remediation;
- perform broad framework redesign;
- require mouse-only operation;
- add a runtime dependency, database, external service, network dependency, or
  runtime/product AI;
- read arbitrary user file contents or persist user history;
- modify `tv203s/`.

## 2. Primary Evidence Matrix Contract

Implementation MUST create and maintain:

```text
specs/014-wave1-functional-hardening/pr-evidence.md
```

The primary matrix MUST contain, for each covered example area:

- historical source files reviewed;
- historical core function or learning target;
- current managed C# behavior;
- proof method;
- smoke test name when executable proof exists;
- helper/headless/direct proof classification;
- negative or fallback proof where relevant;
- missing-core-function decision where relevant;
- intentional deviation or omission;
- learner-facing documentation trigger and target artifact;
- final validation evidence or blocker.

Guides and `examples/README.md` MAY summarize or link to this matrix, but they
do not replace it as acceptance proof.

## 3. Historical Source Contract

Before a hardened proof claim is accepted, these source reviews MUST be
completed as read-only material:

| Area | Required source review |
|---|---|
| `Desklogo` | `tv203s/contrib/tvision/examples/desklogo/desklogo.cc`; `set-logo.cc` and `tv_logo.cc` only for asset/generator boundaries |
| `MsgCls` | `tv203s/contrib/tvision/examples/msgcls/testdyn.cpp`, `tlnmsg.cpp`, `tlnmsg.h` |
| `Tutorial` | `tv203s/contrib/tvision/examples/tutorial/tvguid01.cc` through `tvguid16.cc` |
| `Videomode` | `tv203s/contrib/tvision/examples/videomode/test.cc` |

Headers or additional declarations under `tv203s/` MUST be reviewed when they
are needed to understand constants, data layout, class relationships, macros,
or signatures.

## 4. Smoke-Proof Contract

Executable smoke proof is required whenever managed runtime behavior exists.

Primary smoke proof MUST:

- exercise real example or application logic;
- use public commands, events, application methods, app-loop paths, launcher
  arguments, or stable public state;
- contain concrete assertions for the historically relevant behavior;
- prove more than startup success, static text presence, or project existence;
- use controlled fixtures or test temporary directories only where data is
  needed;
- record the smoke method name in `pr-evidence.md`.

Evidence-only proof is allowed only when:

- no direct managed runtime target exists;
- the matrix records the no-runtime-target rationale;
- the matrix records the proof boundary and intentional deviation or omission.

## 5. Helper Classification Contract

Every helper, headless, or direct proof path used by relevant Wave-1 smokes
MUST be classified as one of:

- `SetupOnly`
- `PrimaryProof`
- `SupplementalProof`
- `LegacyOrTemporary`

`PrimaryProof` is valid only when the path:

- executes real example or application logic;
- uses public commands, events, application methods, or stable public state;
- contains concrete behavior assertions.

A path MUST NOT be accepted as `PrimaryProof` when it only:

- prepares controlled state;
- inspects private implementation details;
- bypasses the behavior under review;
- repeats static text already asserted elsewhere.

`LegacyOrTemporary` MUST identify the later visual-remediation responsibility it
prepares.

## 6. Per-Area Acceptance Matrix

| Area | Required functional proof |
|---|---|
| `Desklogo` | Logo or desktop intent; asset source or replacement rationale; undersized or unsupported display fallback where applicable |
| `MsgCls` | Custom message triggering, routing, observable result, repeated-trigger stability, and intentional differences from historical message-class structure |
| `Tutorial` | Each of the 16 step tokens remains individually selectable and has step-specific learning target or behavior proof |
| `Videomode` | Real capability outcome or clear fallback, post-transition usability, and modern platform limitation where applicable |

## 7. Tutorial Step Contract

`Tutorial` acceptance MUST keep these records individually traceable:

| Step | Required record |
|---|---|
| `tvguid01` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid02` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid03` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid04` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid05` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid06` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid07` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid08` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid09` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid10` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid11` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid12` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid13` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid14` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid15` | Historical source, managed step path, learning target or behavior proof, deviation decision |
| `tvguid16` | Historical source, managed step path, learning target or behavior proof, deviation decision |

No single generic tutorial proof may replace these 16 records.

## 8. Negative, Fallback, and Missing-Core Contract

Relevant negative or fallback paths MUST either:

- be deterministically reproduced by smoke proof; or
- be recorded in `pr-evidence.md` with trigger, expected deviation, observed
  fallback, and proof boundary.

When historical review finds a missing core function:

- implement and smoke-prove it if it is necessary for the existing Wave-1
  functional purpose and feasible without broad framework work, visual
  remediation, new runtime dependencies, or out-of-scope behavior;
- otherwise record it as an intentional deviation or follow-up in
  `pr-evidence.md`.

## 9. Documentation and A11Y Contract

Affected learner-facing guides or `examples/README.md` MUST be updated when the
feature changes:

- runtime behavior;
- usage path;
- visible output;
- historical deviation;
- learner-facing proof explanation.

Review-only helper classifications, proof boundaries, and traceability details
MAY remain only in `pr-evidence.md`.

Learner-facing updates MUST be German-first and English-second at roughly
CEFR-B2 and remain text-first for screen readers, Braille displays, and text
browsers.

DocFX plus `tests/web-a11y` validation is required when generated
documentation output, navigation, or API documentation is affected.

## 10. Governance Contract

Implementation MUST record or mark unchanged:

- NIST SSDF and CWE Top 25 applicability;
- `OWASP ASVS`, `CAPEC`, and Zero Trust `N/A` rationale unless trigger
  conditions change;
- SBOM/VEX/SLSA unchanged or changed-dependency rationale;
- AI-SBOM `N/A` rationale while no runtime/product AI is delivered;
- architecture/security/A11Y evidence location;
- statistics update;
- agent guidance review for `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`, and
  `.github/agents/copilot-instructions.md` when active feature context,
  technologies, project structure, or shared workflow rules change.

## 11. Validation Contract

Formal implementation completion evidence MUST include:

```bash
dotnet restore
dotnet build --configuration Release
dotnet test tests/TuiVision.Examples.SmokeTests/ --configuration Release
dotnet test --configuration Release
dotnet test --configuration Release --collect:"XPlat Code Coverage" --settings coverlet.runsettings
dotnet format --verify-no-changes
git diff --check
```

When guides, DocFX content, documentation navigation, or API documentation are
affected, completion evidence MUST also include:

```bash
docfx docfx.json
cd tests/web-a11y
npm run test:docfx
```

Before each build/test command on the numbered branch, `Directory.Build.props`
MUST be aligned to `1.14.<patch>.<build>` and the manual build counter MUST be
incremented according to repository versioning rules.

If a validation command cannot run locally, the reason and equivalent CI/manual
evidence MUST be recorded in `pr-evidence.md`.

## Deutsch / English

Deutsch: Dieser Vertrag beschreibt, woran die spaetere Umsetzung gemessen wird:
historische Quellenpruefung, primaere Evidence-Matrix, echte Smoke-Nachweise,
Helper-Klassifikation, negative Pfade, fehlende Kernfunktionen und
learner-facing Dokumentation.

English: This contract describes how the later implementation will be judged:
historical source review, primary evidence matrix, real smoke proof, helper
classification, negative paths, missing core functions, and learner-facing
documentation.
