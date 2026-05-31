# Research: Wave 1 Functional Hardening

**Feature**: `014-wave1-functional-hardening`
**Spec**: [spec.md](./spec.md)
**Date**: 2026-05-31

## Decision 1: Use historical source review as the first acceptance gate

**Decision**: Each Wave-1 example area must be reviewed against the named read-only historical source files before proof is accepted: `desklogo/desklogo.cc`; `desklogo/set-logo.cc` and `desklogo/tv_logo.cc` only for asset/generator boundaries; `msgcls/testdyn.cpp`, `msgcls/tlnmsg.cpp`, `msgcls/tlnmsg.h`; `tutorial/tvguid01.cc` through `tutorial/tvguid16.cc`; and `videomode/test.cc`.

**Rationale**: This feature is a functional hardening pass, not a new porting wave. The historical files define the intended behavior to prove, replace, or intentionally omit. Reviewing them first prevents the implementation from strengthening tests around an already-thinned managed behavior.

**Alternatives considered**:
- Rely only on current C# examples. Rejected because existing proof may already be too shallow.
- Translate historical files line by line. Rejected because TuiVision remains an idiomatic C# port and not a mechanical C/C++ clone.

## Decision 2: Maintain `pr-evidence.md` as the primary proof matrix

**Decision**: The implementation creates and maintains `specs/014-wave1-functional-hardening/pr-evidence.md` as the authoritative proof matrix for historical source, C# behavior, proof method, helper classification, missing-core decisions, negative/fallback proof, and deviations.

**Rationale**: A single feature-local matrix gives reviewers and later Wave-1 visual remediation a stable evidence surface. Guides and README pages remain learner-facing summaries and should not become the only place where acceptance proof is reconstructed.

**Alternatives considered**:
- Store proof only in guides and README. Rejected because acceptance evidence would be scattered.
- Create a permanent guide-only matrix. Rejected because the matrix is first a feature proof artifact; learner-facing material should summarize only what learners need.

## Decision 3: Require executable smoke proof whenever managed runtime behavior exists

**Decision**: If a historical core point has a managed runtime target, it must be covered by executable smoke proof. Evidence-only proof is allowed only when no direct managed runtime target exists and the proof boundary is recorded.

**Rationale**: The feature exists because startup, string, or headless-helper proof can overstate correctness. Runtime behavior needs a deterministic executable proof path so later remediation can trust the functional baseline.

**Alternatives considered**:
- Accept evidence-only proof for all historical decisions. Rejected because it would not harden runtime behavior.
- Require executable proof for didactic-only historical points. Rejected because some tutorial points may be historical or explanatory rather than directly executable in the managed port.

## Decision 4: Use strict helper/headless classification

**Decision**: Every helper, headless, or direct proof path used by relevant Wave-1 smokes is classified as `SetupOnly`, `PrimaryProof`, `SupplementalProof`, or `LegacyOrTemporary`. `PrimaryProof` is reserved for paths that execute real example or application logic through public commands, events, application methods, or stable public state and contain concrete assertions.

**Rationale**: Helper paths are useful, but they can hide gaps. The classification tells reviewers which proof is already functionally sufficient and which proof exists only as setup, supplemental support, or a temporary bridge for later visual remediation.

**Alternatives considered**:
- Treat any helper assertion as primary proof. Rejected because it could bypass the behavior under review.
- Disallow all helper-based primary proof. Rejected because this feature is functional hardening and may legitimately prove behavior through stable public state before visual remediation exists.

## Decision 5: Handle negative and fallback paths with smoke or explicit proof boundary

**Decision**: A relevant negative or fallback path that affects acceptance must either be deterministically reproducible by smoke proof or recorded in `pr-evidence.md` with trigger, expected deviation, observed fallback, and proof boundary.

**Rationale**: Some fallback states depend on terminal capabilities or environment limits. The plan should require proof where feasible while still allowing honest evidence when CI or local environments cannot trigger a condition deterministically.

**Alternatives considered**:
- Document all negative/fallback cases without tests. Rejected because reproducible cases should be executable.
- Require tests for every fallback. Rejected because some terminal/platform conditions are not deterministic across supported environments.

## Decision 6: Implement missing historical core behavior only within the functional scope

**Decision**: If historical review finds a missing core function, implement and smoke-prove it when it is necessary for the existing Wave-1 functional purpose and feasible without broad framework work, visual remediation, new runtime dependencies, or out-of-scope behavior. Otherwise record it as an intentional deviation or follow-up.

**Rationale**: The feature must be strong enough to fix small functional gaps, but it must not become a hidden visual-remediation or framework-redesign project.

**Alternatives considered**:
- Document all missing behavior only. Rejected because small necessary functional gaps would remain uncorrected.
- Implement every missing historical function. Rejected because that would reopen full porting and visual-remediation scope.

## Decision 7: Preserve all 16 tutorial steps as individual proof records

**Decision**: `Tutorial` is modeled as one example area with 16 individually traceable step records, one for each `tvguid01` through `tvguid16`.

**Rationale**: The tutorial series is didactic and sequential. Collapsing it into one generic row would lose the learning-target evidence that later hardening and remediation need.

**Alternatives considered**:
- Treat `Tutorial` as one smoke target. Rejected because it would hide step-specific regressions.
- Require separate example projects for every step. Rejected because the existing managed structure already supports step selection.

## Decision 8: Update learner-facing docs only when learner-visible facts change

**Decision**: Guides and `examples/README.md` must be updated when runtime behavior, usage path, visible output, historical deviation, or learner-facing proof explanation changes. Review-only classification details may remain only in `pr-evidence.md`.

**Rationale**: This avoids unnecessary guide churn while keeping learner-facing content accurate and bilingual when behavior or interpretation changes.

**Alternatives considered**:
- Update every guide regardless of change. Rejected because it creates noise and review overhead.
- Keep all new information only in `pr-evidence.md`. Rejected because learners would miss changed usage or deviation explanations.

## Decision 9: Keep fixtures controlled and local

**Decision**: Proof may use source-controlled fixtures, existing repository files, or test temporary directories when needed. It must not read arbitrary user file contents, persist user history, add a database, call external services, or depend on network access.

**Rationale**: Controlled proof data keeps tests deterministic and avoids privacy, security, and portability risks.

**Alternatives considered**:
- Use live user files or current working-directory content. Rejected as non-deterministic and privacy-sensitive.
- Add persistent storage for proof. Rejected because the feature has no persistence requirement.

## Decision 10: Record proportional governance evidence

**Decision**: NIST SSDF and CWE Top 25 remain applicable Level-2 baselines. `OWASP ASVS`, `CAPEC`, and `Zero Trust` are `N/A` unless implementation introduces web/API/auth or changed trust boundaries. SBOM/VEX/SLSA evidence stays unchanged unless dependency, release, or supply-chain impact occurs. AI-SBOM is `N/A` because AI is only development/agent tooling and no runtime/product AI is delivered.

**Rationale**: The feature changes local terminal examples, tests, and documentation. It does not add externally reachable surfaces, AI runtime components, non-C# languages, release packaging, or new runtime dependencies.

**Alternatives considered**:
- Skip governance evidence. Rejected because Level-2 planning requires explicit applicability decisions.
- Produce full AI-SBOM evidence. Rejected because no delivered AI component exists.

## Decision 11: Validation follows the repository gate, scaled to planning and implementation

**Decision**: This plan run validates generated artifacts with placeholder scans and `git diff --check`. The later implementation must run the repository gates: restore, Release build, targeted example smokes, full Release tests, Coverlet coverage, format check, `git diff --check`, and DocFX/web-a11y if documentation output or navigation changes.

**Rationale**: Planning artifacts do not require build/test execution by themselves. Implementation will affect code, tests, and learner-facing evidence, so it must satisfy the full repository gate.

**Alternatives considered**:
- Run full build/test during planning. Rejected because no runtime artifact is changed by planning.
- Defer validation entirely to PR CI. Rejected because local evidence is required for implementation completion.

## Entscheidung / Decision Summary

Deutsch: Die Planung macht 014 zu einem gezielten Funktionsnachlauf: historische Absicht wird in einer Primaermatrix nachgewiesen, echte Runtime-Funktionen bekommen Smoke-Proof, und groessere visuelle oder frameworkweite Luecken bleiben dokumentierte Folgearbeit.

English: The plan makes 014 a targeted functional follow-up: historical intent is proven in one primary matrix, real runtime functions get smoke proof, and larger visual or framework-wide gaps remain documented follow-up work.
