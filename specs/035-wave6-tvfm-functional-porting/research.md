# Research: Wave-6 TVFM Functional Porting

## R-001 One application, one shared example assembly

**Decision**: Deliver one `Tp7FileManager` executable backed by one compiled
`TuiVision.Examples.Wave6` assembly.

**Rationale**: TVFM is historically one integrated application. Splitting its
units into artificial executables would weaken command, focus, list, preview,
mutation and recovery teaching paths. A compiled example assembly keeps
cross-test type identity stable without moving application composition into
the framework.

**Alternatives considered**:

- Multiple executables per Pascal unit: rejected because units are cooperating
  parts, not independent user journeys.
- Put file-manager logic in `TuiVision.Controls`: rejected unless a
  reproducible reusable framework gap appears.
- Link shared source into executable and tests: rejected because it creates
  distinct CLR type identities.

## R-002 Safe default workspace

**Decision**: Copy source-controlled fixture content into a new
process-owned temporary root at normal startup; tests supply their own roots.

**Rationale**: Real mutation proof needs writable files, but repository files
and arbitrary user data must stay untouched. A fresh root is deterministic,
visible, disposable, and cannot inherit unrelated host content.

**Alternatives considered**:

- Open the current directory: rejected as arbitrary user-data access.
- Mutate source-controlled fixtures directly: rejected as repository damage.
- Use only an in-memory virtual filesystem: rejected because the intake
  requires safe real filesystem behavior and platform evidence.

## R-003 Canonical root and link policy

**Decision**: Accept relative paths only, canonicalize against one root, use
platform-appropriate ordinal comparison, and reject any traversed symbolic
link or reparse point.

**Rationale**: Prefix string checks alone are vulnerable to sibling-prefix,
separator, casing and link escapes. Refusing links is the smallest portable
policy that keeps the learning boundary auditable.

**Alternatives considered**:

- Follow links whose final target appears inside the root: rejected because
  race-free portable verification is disproportionately complex here.
- Permit absolute paths under the root: rejected because relative-only input
  makes ownership clearer.
- Resolve only the final path: rejected because an intermediate linked
  directory can escape.

## R-004 Bounded previews

**Decision**: Read at most 4 KiB. Text preview uses strict-first UTF-8
classification with replacement fallback and at most 80 visible lines; hex
preview emits deterministic offsets, 16 bytes per row and printable ASCII.

**Rationale**: The bound is sufficient for a teaching fixture and prevents a
viewer action from becoming unbounded file loading. Replacement plus visible
status is honest for malformed text.

**Alternatives considered**:

- Read the complete file: rejected due memory and latency risk.
- Use ambient code pages: rejected as platform- and locale-dependent.
- Reject all invalid UTF-8 without preview: rejected because a bounded,
  explicit fallback is more instructive.

## R-005 Bounded search

**Decision**: Search to depth 8, inspect at most 256 files, return at most 100
matches, order paths ordinally, and observe cancellation before each
directory and file.

**Rationale**: Explicit limits make progress and abort testable while covering
the historical recursive-search purpose. The fixture is far smaller, so the
limits are safety ceilings rather than expected workload.

**Alternatives considered**:

- Unbounded recursion: rejected as resource-exhaustion risk.
- Background worker and streaming UI: deferred because the functional first
  stage can prove bounded cancellation synchronously.
- Host index/search API: rejected as external and platform-specific.

## R-006 Mutation intent and stale revalidation

**Decision**: Separate request, validation, explicit decision and execution.
Capture source metadata in the intent and revalidate source, target and root
immediately before execution.

**Rationale**: A UI confirmation should authorize one well-described action,
not an arbitrary later filesystem state. Revalidation narrows time-of-check/
time-of-use risk and enables clear conflict evidence.

**Alternatives considered**:

- Execute directly from a button command: rejected because cancel and review
  boundaries disappear.
- Keep an open file handle through confirmation: rejected as intrusive for a
  teaching app and unsuitable for rename/delete portability.
- Automatic overwrite: rejected by the binding intake.

## R-007 Mutation implementation

**Decision**: Support file copy, file rename, file delete, and portable
read-only toggling within the root. Directories are navigated and searched but
not recursively copied or deleted.

**Rationale**: File-level paths preserve the primary TVFM intent while keeping
rollback and destructive scope bounded. Recursive mutation would add a large
policy surface not required for the first stage.

**Alternatives considered**:

- Full directory copy/delete: rejected as disproportionate destructive scope.
- Simulate all mutations: rejected because safe real-path proof is required.
- Broader ACL/attribute editor: rejected as non-portable and security-sensitive.

## R-008 Internal associations only

**Decision**: Map a closed set of text extensions to text preview, known binary
extensions to hex preview, and all others to a visible fallback that lets the
user choose text or hex.

**Rationale**: This preserves association intent without command strings,
shell parsing, process launch or host registration.

**Alternatives considered**:

- Historical command associations: rejected as external execution.
- OS default application: rejected as host-dependent and outside the trust
  boundary.

## R-009 Existing TuiVision controls

**Decision**: Compose `TApplication`, `TWindow`, `TListBox`, `TStaticText`,
`TStatusLine`, command/event dispatch, Help/Description and `TProgressBar`
where progress is visible. Keep root enforcement in example-domain code.

**Rationale**: Existing controls already provide the visual and interaction
contracts. The new logic is application-specific policy around a controlled
workspace, not a general file-dialog replacement.

**Alternatives considered**:

- Use `TFileDialog` as the root guard: rejected because it intentionally
  accepts caller-supplied host paths and does not own this feature's strict
  sandbox policy.
- Add new framework tree/file-manager controls now: rejected absent a proven
  reusable gap.

## R-010 Stage-2 boundary and validation depth

**Decision**: Deliver a functional compact combined view, then record exactly
one `ShowcaseComplete`, `ShowcaseDelta`, `IntentionalMinimalSurface`, or
`ProductDecision`. Run full repository, docs, A11Y, security, platform and
exact-head gates.

**Rationale**: The functional first stage must be genuinely runnable but must
not pre-claim final visual parity. Real filesystem paths and a new executable
have repository-wide and cross-platform impact.

**Alternatives considered**:

- Complete all visual polish inside Feature 035: rejected as scope expansion.
- Targeted tests only: rejected because solution, docs, platform and
  filesystem integration remain material.
