# Research: Wave-1 Visual Component Remediation

## R1 - Functional Baseline

**Decision**: Treat feature 014 and its `pr-evidence.md` as the accepted
functional baseline. Preserve its executable behavior and helper classifications
unless visual integration exposes a focused defect.

**Rationale**: Feature 017 is the second delivery stage. Re-porting behavior
would mix functional and visual scope and weaken traceability.

**Alternatives considered**: Rebuild all four demos from historical sources was
rejected as duplicate scope; evidence-only visual claims were rejected because
the Lastenheft requires visible runtime proof.

## R2 - Vertical Slice

**Decision**: Implement `MsgCls` first.

**Rationale**: It has the smallest complete interaction chain: visible command,
application dispatch, broadcast routing, message window state, repeated trigger,
status feedback, and description. It validates the shared pattern before the
larger Tutorial matrix.

**Alternatives considered**: Desklogo is smaller but mostly static and does not
exercise command-to-visible-state routing. Tutorial is too broad for the first
slice. Videomode is platform-dependent.

## R3 - Shared Composition Boundary

**Decision**: Add a linked `examples/Shared/Wave1Runtime.cs` for repeated
example-composition concerns only: drawable status, Help menu, message update,
and stable region conversion.

**Rationale**: Four local copies would violate the reusable-logic gate. A new
framework API is not justified because the existing controls are sufficient and
the behavior is example presentation policy.

**Alternatives considered**: Reusing the Wave-2-named helper directly was
rejected as misleading ownership. Renaming and changing all Wave-2 examples was
rejected as out-of-scope churn. A public `TStatusLine` framework expansion is
reserved for a focused failing test and `SmallFrameworkFix` decision.

## R4 - Tutorial Visual Model

**Decision**: Keep the 16 `ITutorialStep` metadata implementations and add an
internal `TutorialVisualFactory` that creates a representative visible component
or state for each token.

**Rationale**: The step catalog already provides stable identity, title, and
description. A factory can use existing controls without changing the accepted
step contract or mechanically porting historical C++.

**Alternatives considered**: Adding construction methods to the public step
interface was rejected as unnecessary contract churn. Generic token text only
was rejected as insufficient visual proof. Full historical code recreation was
rejected by scope.

## R5 - Tutorial Historical Interpretation

**Decision**: Use the historical progression as intent, including the source
comments where current guide titles differ: minimal app, status/menu/command,
window and drawing, scrolling/delta/panes/resizing, dialog/modal behavior,
buttons, selection controls, input, and data transfer.

**Rationale**: The historical files are cumulative tutorials. Visible states
must show the defining addition of each step, not reproduce every preceding line.

**Alternatives considered**: Trusting current titles without source review was
rejected because some title/source relationships appear shifted. Treating all
steps as independent full applications was rejected as unnecessary duplication.

## R6 - Primary Smoke Proof

**Decision**: Require real app-loop dispatch plus concrete state, view-tree, and
rendered buffer/cell proof for each primary scenario. Direct helpers remain setup
or supplemental.

**Rationale**: This is the proven Wave-2 visual acceptance pattern and closes
the exact gap named by the Lastenheft.

**Alternatives considered**: Startup plus `VisibleText` and direct calls were
rejected. Screenshot-only tests were rejected as brittle and less text-first.

## R7 - Status And Description

**Decision**: Use a real `TStatusLine` by default and a consistent keyboard-
reachable `Help -> Description` path. An equivalent status area requires an
explicit historical/framework rationale.

**Rationale**: Consistency helps learners and produces stable proof while still
allowing a bounded exception.

**Alternatives considered**: Main-window footer text alone was rejected because
it conflates main content and status. Description-only output was rejected
because it cannot replace the visual surface.

## R8 - Videomode Capability Semantics

**Decision**: Map runtime outcomes to `supported`, `fallback`, `rejected`, or
`unchanged`, show the result visibly, and prove the app remains usable.

**Rationale**: Terminal resizing varies by platform and CI. Honest capability
reporting is more correct than forcing a transition or treating fallback as a
test failure.

**Alternatives considered**: Simulating support as primary proof was rejected
as misleading. Windows-only acceptance was rejected by cross-platform policy.

## R9 - Security And Supply Chain

**Decision**: Apply NIST SSDF/CWE review to changed code and retain repository
supply-chain evidence. Keep ASVS, new SBOM/VEX/SLSA/OpenSSF, AI-SBOM, CAPEC,
Zero Trust, SAMM change, BSI C3A/C5, and new regulatory evidence `N/A` unless a
documented trigger appears.

**Rationale**: The feature changes local terminal UI and tests, not web, cloud,
provider, dependency, distribution, trust, or AI runtime boundaries.

**Alternatives considered**: Producing duplicate non-triggered artefacts was
rejected because it obscures actual risk and evidence ownership.

## R10 - Documentation And A11Y

**Decision**: Update the four guides and examples README in German-first/
English-second CEFR-B2, then run DocFX and Playwright/axe. Use text-first status,
description, semantic Markdown, and keyboard paths.

**Rationale**: Learner-facing documentation changes are certain and therefore
trigger the repository's full documentation/A11Y path.

**Alternatives considered**: Evidence-only documentation was rejected because
the actual operation paths change. Visual screenshot documentation was rejected
as the primary explanation because it is less accessible and harder to maintain.

## R11 - Cross-Platform Validation

**Decision**: Use local macOS proof plus GitHub CI for Linux/macOS/Windows
compatibility, with explicit Videomode outcome differences. No scripts are
planned, so script-pair and man-page obligations remain `N/A`.

**Rationale**: Terminal capabilities differ by OS and host. Existing CI is the
authoritative remote compatibility surface.

**Alternatives considered**: Requiring identical resize outcomes was rejected;
the contract requires truthful classification, not uniform capability.
