# Research: M-07 Closure and Phase-8 Entrance Gate

## Decision 1: Coverage gate applies independently to five modules

- **Decision**: `TuiVision.Core`, `TuiVision.Controls`,
  `TuiVision.Serialization`, `TuiVision.Compatibility`, and
  `TuiVision.Drivers.Console` must each reach at least 70% line coverage
  before the Phase-8 entrance gate may be declared closed, and that result
  must be reviewable separately for each target assembly.
- **Rationale**: The clarified specification now treats the final Phase-8
  readiness claim as a five-module completeness statement. Compatibility and
  the managed driver baseline are part of the same framework proof surface and
  should not stay outside the hard numeric gate.
- **Alternatives considered**:
  - Keep 70% only on Controls and merely record the other modules as soft
    evidence.
  - Limit the hard gate to Core/Controls/Serialization while leaving
    Compatibility or Drivers.Console outside the threshold.

## Decision 1a: Coverage evidence is assembly-specific even when tests are shared

- **Decision**: The final evidence package must report line coverage
  assembly-by-assembly for the five gate modules, even when the exercising
  tests come from shared or cross-module repository test projects.
- **Rationale**: The clarified specification explicitly rejects an aggregated
  coverage number as sufficient proof. Reviewers need a direct pass/fail view
  for each gate assembly.
- **Alternatives considered**:
  - Accept one aggregated repository coverage percentage.
  - Require one dedicated test project per gate module instead of allowing
    shared suites with separated reporting.

## Decision 2: Non-driver `geplant` rows are implementation work, not narrative cleanup

- **Decision**: Every non-driver ledger row still mapped to a `geplant` target
  in Core, Controls, Serialization, or Compatibility must be implemented and
  covered by automated tests unless it can be reclassified as a true
  architecture replacement or obsolete special case with explicit rationale.
- **Rationale**: `M-07 vollstaendig schliessen` would be undercut if remaining
  framework gaps could be closed only by editing ledger prose rather than by
  providing real maintained runtime behavior.
- **Alternatives considered**:
  - Reclassify all still-planned rows as `bewusst ausgelassen + Begruendung`.
  - Allow mixed narrative closure without new code whenever example-port waves
    do not immediately depend on the missing type.

## Decision 3: Full-suite validation means all repository test projects

- **Decision**: The Phase-8 gate requires a successful `dotnet test` across all
  test projects in the repository, not only the touched module tests.
- **Rationale**: The closure claim is repository-wide, and the clarified spec
  now ties gate success to all test projects rather than to a narrow subset.
- **Alternatives considered**:
  - Limit the hard gate to four modules and leave Compatibility outside it.
  - Run only the directly affected test projects and treat the rest as
    optional follow-up validation.

## Decision 4: The closure requires a dedicated git commit

- **Decision**: The final Phase-8 gate closure must be represented by a
  dedicated git commit that references the supporting proof artifacts.
- **Rationale**: Reviewability and later auditability both improve when the
  closure is visible as one explicit historical marker instead of being spread
  implicitly across unrelated commits.
- **Alternatives considered**:
  - Accept a checklist-only marker without a dedicated commit.
  - Accept a PR description or review note instead of an explicit repository
    commit boundary.

## Decision 5: Linux and Windows/WSL evidence is conditional on material platform relevance

- **Decision**: Linux and Windows/WSL execution evidence is mandatory whenever
  the implemented changes materially affect runtime behavior, terminal behavior,
  portability, or build reliability. Otherwise the evidence package may record
  a reviewable not-applicable rationale.
- **Rationale**: This keeps portability evidence meaningful without forcing
  ritual re-execution for documentation-only or purely formal proof updates.
- **Alternatives considered**:
  - Require Linux and Windows/WSL evidence for every closure change, even
    documentation-only updates.
  - Make Linux and Windows/WSL evidence purely optional for the gate.

## Decision 6: The proof package remains repository-local and review-oriented

- **Decision**: The authoritative gate-evidence package remains inside the
  repository: code, MSTest suites, `docs/porting-status.md`, `Pflichtenheft.md`,
  quickstart guidance, and the dedicated gate-closure commit.
- **Rationale**: The project already treats repository-visible artifacts as the
  required proof surface. External spreadsheets, hidden notes, or oral context
  would weaken the claim that Phase 8 may begin on objective evidence.
- **Alternatives considered**:
  - Split evidence across repository files and external review notes.
  - Treat CI output alone as sufficient without synchronizing the human-readable
    proof documents.

## Decision 7: Placeholder-only or no-op-only modules cannot satisfy the gate

- **Decision**: A module counted toward the Phase-8 coverage gate must carry
  real remaining framework responsibility. Placeholder-only or no-op-only code
  with trivial tests is not acceptable proof; such a module must either gain
  real responsibility or be restructured out of the hard gate before closure is
  claimed.
- **Rationale**: Otherwise the expanded five-module gate could be passed
  formally while weakening the actual meaning of framework completeness.
- **Alternatives considered**:
  - Accept trivial placeholder coverage as long as the percentage passes.
  - Allow only `TuiVision.Compatibility` as a special placeholder exception.

## Decision 8: Coverage conflicts require explicit resolution

- **Decision**: If local and CI coverage results diverge for a gate assembly,
  the Phase-8 gate remains open until the discrepancy is explained and the
  final repository-visible evidence package identifies the authoritative
  result.
- **Rationale**: Assembly-specific proof is only useful if reviewers know which
  result governs the closure decision when multiple measurements exist.
- **Alternatives considered**:
  - Let the latest local result implicitly override CI.
  - Let CI always win without documenting the discrepancy.
