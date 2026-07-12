# Implementation Plan: A11Y Framework

**Branch**: `023-a11y-framework` | **Date**: 2026-07-12 | **Spec**: [spec.md](spec.md)

## Summary

Feature 023 ergänzt eine kleine öffentliche A11Y-Schicht: opt-in Widget-Texte,
typisierte Fokusankündigungen auf dem bestehenden Broadcast, strukturiert
abfragbare Shortcuts, eine explizite High-Contrast-Palette, vollständige
Tastatur-Proof-Inventur und eine reale Referenz-App. Die Umsetzung erweitert
bestehende Core-/Controls-Verträge ohne native Assistive-Technik-Brücke,
Abhängigkeit oder breite Control-Migration.

## Technical Context

**Language/Version**: C# / .NET 10  
**Primary Dependencies**: repository-owned Core, Controls, Console Driver, MSTest  
**Storage**: none  
**Testing**: MSTest, FakeDriver/in-process app loop, Coverlet, DocFX, Playwright/Axe  
**Target Platforms**: macOS/Linux and Windows/WSL-compatible managed runtime  
**Constraints**: no new package, no native AT bridge, `tv203s/` read-only, DE-first/EN-second XML/docs  
**Delivery**: authorized `MergeAndSync`

## Constitution Check

| Gate | Decision |
|---|---|
| Security and safe publication | PASS: bounded text validation, no secret or external input path |
| Public API XML documentation | PASS: all new public contracts are bilingual and Release-enforced |
| Tests and 70 % coverage | PASS: red-first targeted tests, full Release and canonical coverage |
| Historical source | PASS: modern A11Y has no direct historical equivalent; relevant focus/menu/status intent reviewed read-only |
| A11Y and inclusion | PASS: feature purpose; keyboard, focus, contrast, text-first and DocFX/Axe proof |
| Agent parity | PASS: five surfaces updated together only when active context changes |
| Versioning | PASS: `1.23.<patch>.<build>`, Build increment before each build/test command |
| Project statistics | PASS: chronological ledger update, final Gesamtstatistik preserved |

## Architecture

### Core contracts

- `IAccessibleWidget`: opt-in semantic text and current focus ability.
- `TAccessibleShortcut`: immutable key/text/command/source description.
- `IAccessibleShortcutProvider`: read-only shortcut query contract.

### Controls integration

- A Controls-owned focus payload composes `TView` plus the Core semantic snapshot so existing `cmFocusChanged` consumers can migrate without a second broadcast.
- `TProgram.CurrentChanged` emits exactly one payload after `TGroup.SetFocus` performs an actual transition.
- `TStatusLine` accepts both the new payload and legacy raw-`TView` input during the bounded compatibility period.
- `TMenuBar` and `TStatusLine` implement the shortcut provider from their existing linked structures. No mutable global registry is introduced.
- `TColorScheme` maps semantic roles to console colours. `TColorScheme.HighContrast` is explicit; participating views apply it while default rendering remains unchanged.

### Reference slice

`examples/A11yFramework` is a small framework demonstration, not a historical
port. It contains two labeled focusable widgets, menu/status shortcuts, visible
focus text, a High-Contrast toggle and keyboard-reachable Description. Smoke
tests run the real event loop and combine state, view-tree and cell proof.

## Project Structure

```text
src/TuiVision.Core/
  IAccessibleWidget.cs
  TAccessibleShortcut.cs
src/TuiVision.Controls/
  TFocusAnnouncementEvent.cs
  TColorScheme.cs
  TProgram.cs
  TMenuBar.cs
  TStatusLine.cs
tests/TuiVision.Core.Tests/
  AccessibleContractsTests.cs
tests/TuiVision.Controls.Tests/
  AccessibleFocusTests.cs
  AccessibleShortcutTests.cs
  KeyboardAccessibilityMatrixTests.cs
  TColorSchemeTests.cs
examples/A11yFramework/
tests/TuiVision.Examples.SmokeTests/
  A11yFrameworkSmokeTests.cs
docs/guides/a11y-framework.md
specs/023-a11y-framework/
```

## Delivery Phases

1. Evidence foundation, historical N/A/rationale and complete compile-surface review.
2. Red Core contract tests, then minimal XML-documented contracts.
3. Red focus/shortcut/contrast tests, then bounded Controls integration.
4. Red keyboard inventory matrix, then close all proof/N/A rows.
5. Red reference-app smoke, then visible app-loop/state/view/cell implementation.
6. Pflichtenheft, guide, agent parity, governance, statistics and Lastenheft archive.
7. Static, targeted, full Release, coverage, DocFX/Axe, text-browser and remote validation.
8. PR/review/merge, causal closeout only if required, main synchronization.

## Test Strategy

- Test-first batches are deliberately small: Core contract, focus payload,
  shortcuts, colour scheme, keyboard matrix and reference app.
- The keyboard matrix inventories control families explicitly. Each row names
  Tab, Shift+Tab, arrows, Enter and direct shortcut as `Proof` or justified
  `N/A`; reflection alone cannot claim behavior.
- Existing `pages.yml` is statically validated and executed remotely. Local
  DocFX is immediately followed by `tests/web-a11y`.
- Shared runtime changes trigger full Release plus canonical assembly coverage.

## Governance and Evidence

`pr-evidence.md` is created before implementation changes and records requirement
coverage, API decisions, keyboard rows, historical rationale, governance
applicability, validation commands, remote review truth and follow-ups. Native
AT integration, complete migration and terminal-wide WCAG claims are
`FollowUpHardening`, not implicit completion.

## Complexity Tracking

No constitution violation or exception is accepted. The extra reference project
is justified by the Lastenheft's visible example requirement and provides one
independent vertical proof without changing Wave 1-4 examples.
