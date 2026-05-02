# TuiVision Development Guidelines

Auto-generated from all feature plans. Last updated: 2026-05-02

## Active Technologies
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions for existing CI validation (008-controls-revision)
- In-memory UI state only in production; source-controlled planning, tests, and proof artifacts in `specs/`, `tests/`, and `docs/`; no database or external service storage (008-controls-revision)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex (009-controls-widgets-and-collections)
- C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling (010-standard-dialogs-designer)
- In-memory dialog state and session-only history; real local file-system metadata for file-listing/validation only; source-controlled tests/proof artifacts; minimal persisted dialog-description fixture through existing serialization/resource primitives; no database or external service storage (010-standard-dialogs-designer)

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
- 010-standard-dialogs-designer: Added C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell, dialog, file, color, history, and widget types (`TDialog`, `TFileDialog`, `TFileList`, `TDirListBox`, `TFileInfo`, `TFileInputLine`, `THistory`, `TColorDialog`, `TColorSelector`, `TMonoSelector`, `TColorGroup`, `TColorDisplay`, `TComboBox`, `TProgressBar`, `TParamText`); existing `TuiVision.Serialization` archive/resource foundation (`TRecordRegistry`, `TRecordSerializer`, `TBinaryArchiveReader`, `TBinaryArchiveWriter`, `TResourceFile`, `TResourceCollection`, `pstream` family); MSTest; Coverlet; conditional DocFX and web A11Y smoke tooling
- 009-controls-widgets-and-collections: Added C# `latest` on .NET 10 (`net10.0`) + Existing `TuiVision.Core` geometry/event/buffer types; existing `TuiVision.Controls` shell foundation delivered in `008-controls-revision` (`TView`, `TGroup`, `TProgram`, `TApplication`, `TMenuItem`, `TStatusItem`, `ShellCommandIds`) plus existing widget/input primitives (`TInputLine`, `TListViewer`, `TListBox`, `TScrollBar`, `TScroller`, `TStringList`, `TFileInputLine`, `THistory`, `ManagedClipboard`, current `TParamText`, editor-oriented `TIndicator` as a contrast case only); MSTest; Coverlet via `dotnet test --collect:"XPlat Code Coverage"`; conditional `docfx docfx.json`; GitHub Actions and existing `tests/TuiVision.Examples.SmokeTests/` infrastructure as downstream contex
- 008-controls-revision: Controls revision implemented: `TSubMenu`, `TStatusDef`, `WindowFlags` added; `TMenuBar`, `TStatusLine`, `TWindow`, `TDialog`, `TMenuItem`, `TView` expanded; 338 tests green, 84.02 % Controls coverage, format gate passed; `docs/porting-status.md`, `Pflichtenheft.md`, and `docs/project-statistics.md` updated; `Lastenheft_ControlsRevision.md` renamed.


<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
