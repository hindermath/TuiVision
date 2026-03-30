# TuiVision Development Guidelines

Auto-generated from all feature plans. Last updated: 2026-03-29

## Active Technologies
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)

- C# `latest` on .NET 10 (`net10.0`) + `TuiVision.Core`, `TuiVision.Controls`, `TuiVision.Drivers.Console`, `TuiVision.Serialization`, `TuiVision.Compatibility`; MSTest; Coverlet; docfx; wave-1 example projects under `examples/` (007-port-wave1-examples — Wave 1 delivered)

## Project Structure

```text
src/
tests/
examples/
docs/guides/examples/
```

## Commands

```bash
dotnet restore
dotnet build --configuration Release
dotnet test
dotnet format --verify-no-changes
dotnet test tests/TuiVision.Examples.SmokeTests/
cd tests/web-a11y && npm install && npx playwright install chromium && npm run test:docfx
docfx docfx.json && cd tests/web-a11y && npm run test:docfx
```

## Code Style

C# `latest` on .NET 10 (`net10.0`): Follow standard conventions. All XML docs and explanatory comments must be bilingual: German first, English second, CEFR-B2 readability. Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md` instead of an oversized inline-bilingual file; the German version remains canonical unless explicitly marked otherwise. Follow `Programmierung #include<everyone>`: guides, statistics, examples, and generated API docs must stay usable in text-first assistive setups such as Braille displays, screen readers, and text browsers. Generated HTML documentation should target WCAG 2.2 conformance level AA. Keep the Playwright + `@axe-core/playwright` smoke checks in `tests/web-a11y/` aligned with the current DocFX structure and use `lynx` as an extra text-browser spot check when available. Every successful `docfx docfx.json` regeneration must be followed by the matching `tests/web-a11y/` A11y smoke check in the same work item. Treat bilingual CEFR-B2 delivery plus the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts.

## Recent Changes
- 008-controls-revision: Controls revision implemented: `TSubMenu`, `TStatusDef`, `WindowFlags` added; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` expanded; 338 tests green, 84.02 % Controls coverage, format gate passed; `docs/porting-status.md`, `Pflichtenheft.md`, and `docs/project-statistics.md` updated; `Lastenheft_ControlsRevision.md` renamed.
- 008-controls-revision: Added the Controls-revision plan baseline for `TMenuBar`, `TStatusLine`, `TWindow`, and `TDialog`, including one-level submenu support, explicit help-context status routing, `WindowFlags`, and proof-surface follow-through in `docs/porting-status.md`, `Pflichtenheft.md`, and `docs/project-statistics.md`.

- 007-port-wave1-examples: Wave 1 delivered (2026-03-28): `desklogo`, `msgcls`, `tutorial` (16 token-based steps), `videomode` ported with headless smoke seam; 41 smoke tests green; Release build and format pass clean; Pflichtenheft Wave-1 checklist ticked off; next step is Wave 2 Controls and Dialogs.

<!-- MANUAL ADDITIONS START -->
- Maintain `docs/project-statistics.md` as a living ledger and keep a final top-level `## Gesamtstatistik` block as the last section of the file.
- Inside that final `## Gesamtstatistik` block, keep compact ASCII-only trend diagrams directly below the textual summary, including documented acceleration factors from agentic-AI plus Spec-Kit/SDD support and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window; refresh them together with every statistics update.
- Keep each short CEFR-B2 explanatory text directly adjacent to its matching ASCII diagram group, ideally immediately before or after it, so apprentices do not need to scroll between explanation and diagram.
- When progression across an X-axis helps comprehension, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, Markdown-readable, and explained in CEFR-B2 language.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
<!-- MANUAL ADDITIONS END -->

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
