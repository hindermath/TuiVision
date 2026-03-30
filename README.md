# TuiVision

TuiVision is an example project that ports Turbo Vision concepts to C#/.NET 10
(`net10.0`, .NET Core).

The project is intended as a learning and modernization showcase using
Agentic-AI workflows. It is not an official Turbo Vision continuation.

## Scope

- Target framework: .NET 10 (`net10.0`)
- Runtime model: managed .NET Core code
- Goal: example port and reference implementation

## Development Guides

- Multi-Mac workflow (MacBook Air M2 + Mac mini M4 Pro) with `gh` and `codex`:
  [`docs/guides/multi-mac-workflow.md`](docs/guides/multi-mac-workflow.md)

## Documentation Accessibility Checks

- Node-based A11y checks for the generated DocFX site live in `tests/web-a11y/`.
- Recommended runtime for this toolchain: Node `24.x` LTS.
- Install once in that folder with `npm install` and `npx playwright install chromium`.
- Keep `lynx` installed as the text-browser cross-check for generated HTML docs.
- Run the combined DocFX + Playwright + axe check with
  `cd tests/web-a11y && npm run test:docfx`.
- If DocFX output is regenerated, run the A11y check in the same work step.
- Use `lynx` as a second text-first review path, for example with
  `cd tests/web-a11y && npm run serve:docfx` in one terminal and
  `lynx -dump http://127.0.0.1:8123/index.html` in another.

## Documentation Policy

- Documentation changes MUST be bilingual with German text first and English text second.
- Explanatory text MUST target CEFR-B2 readability for both languages.
- Follow `Programmierung #include<everyone>`: documentation and generated API pages MUST stay usable on Braille displays, with screen readers, and in text browsers.
- Generated HTML documentation SHOULD meet WCAG 2.2 conformance level AA as the practical accessibility baseline.
- Prefer semantic headings, lists, tables, and ASCII/text-first diagrams. Do not rely on color or layout alone for key meaning.
- When DocFX structure or API presentation changes, validate representative `_site/` pages with a text-oriented review path, preferably using a local Playwright accessibility snapshot.
- Keep the Playwright + `@axe-core/playwright` smoke tests under `tests/web-a11y/` aligned with the current DocFX structure and representative pages.
- Treat every `docfx` regeneration as incomplete until the matching A11y smoke check has also passed.
- Public API changes MUST include complete XML documentation updates in the same change.

## CI

- GitHub Actions workflow:
  [`.github/workflows/ci.yml`](.github/workflows/ci.yml)
- The workflow validates restore/build/test and generates docfx documentation
  when `docfx.json` is present at repository root.
- The workflow intentionally fails if no `.sln` or `.csproj` exists yet, to
  prevent false-green CI results.

## Legal and License Notice

- TuiVision is an educational example project.
- The project is not intended to violate rights or licenses of Turbo Vision.
- The project is not intended to compete with Turbo Vision.
- TuiVision is not affiliated with, endorsed by, or officially connected to
  Turbo Vision rightsholders.

## Licensing Model

- Original TuiVision code in this repository is licensed under MIT
  (see [`LICENSE`](LICENSE)).
- Third-party source material (for example under `tv203s/`) remains under its
  own original license terms and notices.
- Third-party license terms take precedence for third-party files.

## Third-Party Source Base

The historical Turbo Vision source tree used as input is located in:

- `tv203s/`

Use and redistribution of these files must follow their original licensing and
copyright notices.
