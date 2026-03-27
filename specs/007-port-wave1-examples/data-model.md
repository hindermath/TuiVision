# Data Model: Mandatory Example Wave 1 Ports

## Overview

This feature adds managed example-application deliveries rather than database
storage. The data model therefore focuses on runnable example definitions,
tutorial-step identity, smoke-validation state, guide surfaces, and
runtime-visible fallback behavior for `videomode`.

## Entities

### Wave1ExamplePort

- **Purpose**: Represents one managed delivery for a mandatory wave-1 example.
- **Key attributes**:
  - Canonical example name (`desklogo`, `msgcls`, `tutorial`, `videomode`)
  - Historical source folder
  - Managed project path
  - Primary teaching purpose
  - Delivery state (`planned`, `implemented`, `smoke-covered`, `documented`)
- **Relationships**:
  - Owns one `ExampleEntryPoint`
  - Owns one or many `SmokeValidationScenario`
  - Owns one `ExampleGuideSurface`
  - May depend on zero or many `ExampleSupportAsset`
- **Validation rules**:
  - Each wave-1 example name appears exactly once
  - Delivery is incomplete until runnable code, smoke coverage, and guide
    coverage all exist
  - Only the four mandatory wave-1 examples are valid in this model

### ExampleEntryPoint

- **Purpose**: Captures how a reviewer or smoke test launches a managed example.
- **Key attributes**:
  - Managed project path
  - Startup command or invocation pattern
  - Optional selector token
  - Expected defining behavior
  - Clean-exit path
- **Relationships**:
  - Belongs to exactly one `Wave1ExamplePort`
- **Validation rules**:
  - Every wave-1 example has one canonical entry point
  - `tutorial` may expose multiple selector tokens through one entry point
  - Startup and exit must be deterministic enough for smoke validation
  - A clean-exit path must be assertable without forced termination or hanging
    pending interaction

### TutorialStep

- **Purpose**: Represents one original tutorial lesson inside the shared
  managed tutorial delivery.
- **Key attributes**:
  - Canonical token (`tvguid01` through `tvguid16`)
  - Sequence number
  - Step title or learning focus
  - Independently runnable state
  - Expected visible outcome
- **Relationships**:
  - Belongs to exactly one `Wave1ExamplePort` named `tutorial`
  - Owns exactly one `SmokeValidationScenario`
  - Is documented in one section of the shared `ExampleGuideSurface`
- **Validation rules**:
  - All 16 original tokens must be present exactly once
  - Sequence numbers remain ordered from 1 to 16
  - Each step keeps its own runnable and smoke-covered identity

### SmokeValidationScenario

- **Purpose**: Represents one repeatable acceptance path for a wave-1 example
  or tutorial step.
- **Key attributes**:
  - Scenario identifier
  - Validation seam kind (`in-process-host`, `process-launch`, `mixed`)
  - Launch path
  - Trigger action or setup
  - Expected defining behavior
  - Expected exit behavior
  - Result state (`red`, `green`, `refactored`)
- **Relationships**:
  - Belongs to exactly one `Wave1ExamplePort` or one `TutorialStep`
- **Validation rules**:
  - Every wave-1 example has at least one scenario
  - `tutorial` has one scenario per original step
  - A scenario is not complete until startup, defining behavior, and clean exit
    are all asserted
  - In-process scenarios must still exercise the real example host contract

### ExampleGuideSurface

- **Purpose**: Represents the didactic documentation page for one wave-1
  example scope.
- **Key attributes**:
  - Guide path
  - Learning goals
  - Prerequisites
  - Startup instructions
  - Usage flow
  - Architecture hints
  - Exercises
- **Relationships**:
  - Belongs to exactly one `Wave1ExamplePort`
  - May contain many `TutorialStep` sections when the example is `tutorial`
- **Validation rules**:
  - `desklogo`, `msgcls`, and `videomode` each own a dedicated guide page
  - `tutorial` owns one shared guide page with 16 clearly separated step
    sections
  - Guide content is part of acceptance, not optional follow-up work

### ExampleSupportAsset

- **Purpose**: Captures a historical helper utility, generated asset, or
  support file that may still matter to a managed port.
- **Key attributes**:
  - Historical file path
  - Support role (`generator`, `asset`, `helper`, `none`)
  - Inclusion decision (`required`, `not-required`)
  - Rationale
- **Relationships**:
  - May belong to one `Wave1ExamplePort`
- **Validation rules**:
  - Support assets are only included when they are necessary for visible
    behavior, assets, or repeatable smoke validation
  - A non-required helper does not block acceptance of the primary example

### DisplayModeTransitionRequest

- **Purpose**: Represents one requested runtime transition inside `videomode`.
- **Key attributes**:
  - Requested width or height change
  - Optional requested mode label
  - Runtime capability state
  - Fallback requirement
- **Relationships**:
  - Belongs to the `Wave1ExamplePort` named `videomode`
  - Produces one `DisplayModeOutcome`
- **Validation rules**:
  - Supported requests prefer real transitions
  - Unsupported requests must not fail silently

### DisplayModeOutcome

- **Purpose**: Represents the observable result of a `videomode` transition
  attempt.
- **Key attributes**:
  - Outcome kind (`real-transition`, `visible-fallback`)
  - User-visible message or state
  - Post-transition usability state
- **Relationships**:
  - Belongs to exactly one `DisplayModeTransitionRequest`
- **Validation rules**:
  - Every request ends in one explicit outcome
  - A fallback must remain visible and reviewable
  - The example must stay usable after either outcome kind

### ProgressArtifactUpdate

- **Purpose**: Represents one required synchronization update for wave-1
  progress in repository-governance artifacts.
- **Key attributes**:
  - Target artifact path
  - Update purpose
  - Trigger condition
  - Completion state
- **Relationships**:
  - May reference one or many `Wave1ExamplePort` records
- **Validation rules**:
  - `Pflichtenheft.md` and `docs/project-statistics.md` are mandatory update
    targets when implementation lands
  - Shared agent-guidance files become update targets whenever plan-derived
    technology, structure, or workflow guidance changes
  - Tracking updates must distinguish wave 1 from later mandatory waves
