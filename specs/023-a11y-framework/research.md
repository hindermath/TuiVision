# Research: A11Y Framework

## R1 - Focus transport

**Decision**: Reuse `ShellCommandIds.cmFocusChanged`; replace its raw-view-only
payload with a typed compatibility payload and keep `TStatusLine` tolerant of
the legacy shape.

**Rationale**: One focus transition must have one authoritative event. A second
A11Y command would create ordering and duplication risk.

## R2 - Widget adoption

**Decision**: Keep `IAccessibleWidget` opt-in. The reference widget and a small
representative Controls subset implement it; other views remain valid with a
missing semantic label.

**Rejected**: Making `TView` implement the interface would falsely claim
accessible text for every derived control.

## R3 - Shortcut ownership

**Decision**: Use immutable `TAccessibleShortcut` values exposed through
`IAccessibleShortcutProvider`. `TMenuBar` and `TStatusLine` derive values from
their existing item chains.

**Rejected**: A global mutable registry because lifetime, duplicates and test
isolation would become hidden shared state.

## R4 - High Contrast

**Decision**: Model semantic colour roles in `TColorScheme`, with an explicit
`HighContrast` instance and application to participating views. Default colours
remain unchanged until activation.

## R5 - Keyboard inventory

**Decision**: Maintain an explicit test matrix of selectable control families.
Every requested key family is either behaviorally proven or has a named `N/A`
rationale.

**Rejected**: Reflection-only coverage because construction does not prove
navigation or activation.

## R6 - Documentation CI

**Decision**: Treat `.github/workflows/pages.yml` as the canonical implementation.
It already runs on main and pull requests, builds DocFX, installs dependencies
with `npm ci` and runs Playwright/Axe. Feature 023 validates rather than
duplicates it in `ci.yml`.

## R7 - Historical source

**Decision**: Review historical focus, menu and status intent where useful, but
record modern semantic A11Y contracts as no-direct-equivalent. `tv203s/` stays
read-only and cannot be evidence for current WCAG support.

## R8 - Security and regulation

Labels and shortcut text are bounded in-process strings. No new service,
identity, persistence, package, cloud or AI product boundary is introduced.
Supply-chain, regulatory, C3A and C5 checkpoints remain trigger-based `N/A` with
re-evaluation triggers in evidence.
