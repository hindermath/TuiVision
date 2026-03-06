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

## Documentation Policy

- Documentation changes MUST be bilingual with German text first and English text second.
- Explanatory text MUST target CEFR-B2 readability for both languages.
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
