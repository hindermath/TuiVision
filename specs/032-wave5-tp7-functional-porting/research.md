# Research: Wave-5 TP7 Functional Porting

## R-001 Gemeinsame Beispielassembly / Shared Example Assembly

**Decision**: Use one compiled project at
`examples/Shared/TuiVision.Examples.Wave5/` and ten thin executables.

**Rationale**: Existing waves link shared source into multiple assemblies.
Feature 032 has ten apps and cross-app smoke matrices; one compiled assembly
prevents accidental assumptions about CLR type identity and keeps common
headless/event/status proof code in one place.

**Alternatives considered**:

- Link one `Wave5Runtime.cs` into ten executables: rejected because cross-project
  tests can observe distinct CLR types and maintenance multiplies.
- Put example logic in framework projects: rejected because the behavior is
  Wave-5 composition, not reusable framework semantics.
- One executable with ten modes: rejected because the intake requires ten
  independent normal launch paths.

## R-002 Historical translation boundary

**Decision**: Preserve purpose, commands, state transitions and observable
outcomes; do not preserve Pascal object layout, DOS integration, overlays,
binary record activation, global variables or random/clock dependence.

**Rationale**: TuiVision is a modern C# interpretation. Historical sources are
normative for intent but not for unsafe or platform-specific implementation.

**Alternatives considered**:

- Mechanical Pascal translation: rejected as non-idiomatic and likely to
  reproduce obsolete trust boundaries.
- Use only modern examples without source mapping: rejected because it loses
  traceability and teaching value.

## R-003 Calculator numeric model

**Decision**: Use `decimal` with invariant parsing/formatting, one pending
binary operation and explicit valid/error state.

**Rationale**: `decimal` gives deterministic classroom-scale arithmetic and
avoids locale-dependent input. Division by zero preserves the previous valid
result and emits a visible rejection.

**Alternatives considered**:

- `double`: accepted historically but less deterministic for visible decimal
  examples.
- Arbitrary precision package: rejected because no dependency is needed.

## R-004 Calendar and puzzle determinism

**Decision**: Calendar state starts from a fixed `DateOnly`; puzzle state
starts from a fixed 4x4 arrangement and moves only adjacent tiles.

**Rationale**: Host date, timezone and random shuffle are unsuitable as test
oracles. Fixed fixtures still preserve month navigation and sliding-puzzle
intent.

**Alternatives considered**:

- Current system date and random shuffle: rejected as nondeterministic.
- Mocking system time/random services: rejected as unnecessary infrastructure
  for examples.

## R-005 Editor boundary

**Decision**: Compose existing `TFileEditor` and `TEditWindow`; initialize from
embedded content and permit writes only under an explicit test-owned root.

**Rationale**: Feature 018 already hardened modified, safe-close, conflict and
file behavior. Reusing it proves the closed contract without reading arbitrary
user content.

**Alternatives considered**:

- Reuse the existing `TvEdit` executable directly: rejected because Feature
  032 needs a distinct TP7 source mapping and example identity.
- Add an editor implementation under examples: rejected as a substitute
  framework.

## R-006 Help boundary

**Decision**: Use `THelpSourceCompiler`, `THelpFile`, `THelpWindow` and
controlled `.topic` strings. Prove valid compilation, invalid reference
rejection, known context and fallback.

**Rationale**: The existing compiler is bounded, strict UTF-8 and atomic. It
captures the TP7 compiler/viewer purpose without proprietary binary parity.

**Alternatives considered**:

- Decode historical binary help: rejected as unchecked proprietary format
  scope.
- Copy existing TvHc/HelpDemo output: rejected because the Wave-5 consumer
  needs its own app-loop evidence.

## R-007 Resource generator boundary

**Decision**: Use existing `TRecordRegistry`/`TResourceFile` built-in types,
exact ordinal keys and controlled memory/temp streams. The generator accepts
only fixed allowlisted descriptions.

**Rationale**: This preserves named resource generation and loading while
avoiding arbitrary type activation, executable overlays and unsafe lengths.

**Alternatives considered**:

- General reflection-based serializer: rejected by the closed-schema security
  requirement.
- Historical executable-embedded resources: rejected as platform-specific and
  outside the managed delivery model.

## R-008 Mouse settings boundary

**Decision**: Represent double-click delay and button orientation as local
example state. Existing `TEvent` mouse input may update the visible state, but
keyboard commands provide the complete path and no host setting is changed.

**Rationale**: The historical demo purpose is to explain mouse settings and
double-click feedback. Mutating host mouse configuration is neither portable
nor necessary.

**Alternatives considered**:

- Native host API bridge: rejected by scope and cross-platform constraints.
- Keyboard-only omission of mouse proof: rejected because the consumer
  explicitly covers supported mouse input and fallback.

## R-009 Stage-1 visibility

**Decision**: Each app shows purpose, current state and one core interaction in
a real view. Full three-layer menu/status/description showcase parity is
measured as a later delta, not silently claimed.

**Rationale**: This respects the repository's two-stage example pattern and
prevents Feature 032 from absorbing the showcase stage.

**Alternatives considered**:

- Complete all showcase polish now: rejected as accepted scope expansion.
- Headless state only: rejected because Stage 1 still requires visible app-loop
  proof.

## R-010 Validation depth

**Decision**: Run targeted Wave-5 smokes, full Release tests, canonical
coverage, DocFX/Axe, platform CI and exact-head delivery validation.

**Rationale**: Ten projects, shared example code, new guides and app-loop proof
have repository-wide build and documentation impact.

**Alternatives considered**:

- Targeted tests only: rejected because solution/project integration and
  existing example regressions remain material.
- New scripts for matrices: rejected because MSTest and existing workflows are
  sufficient.
