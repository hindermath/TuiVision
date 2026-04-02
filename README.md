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

## Barrierefreiheit / Accessibility (A11Y)

Dieses Projekt folgt grundlegenden Barrierefreiheitsstandards für alle
dokumentierten Inhalte und Benutzeroberflächen.

Richtlinien für Markdown-Dokumentation:

- Überschriften folgen einer klaren Hierarchie (h1 → h2 → h3 — keine Ebene überspringen)
- Alle Bilder haben aussagekräftige Alt-Texte (`![Beschreibung](bild.png)`)
- Linkbeschriftungen sind beschreibend (`[Installationsanleitung](...)` statt `[hier](...)`)
- Code-Blöcke geben die Sprache an (` ```bash `, ` ```powershell `)
- Tabellen haben Kopfzeilen für alle Spalten
- Keine Informationen werden ausschließlich über Farbe vermittelt

---

This project follows basic accessibility standards for all documented
content and user interfaces.

Guidelines for Markdown documentation:

- Headings follow a clear hierarchy (h1 → h2 → h3 — no level skipped)
- All images have meaningful alt texts (`![Description](image.png)`)
- Link labels are descriptive (`[Installation guide](...)` instead of `[here](...)`)
- Code blocks specify the language (` ```bash `, ` ```powershell `)
- Tables have header rows for all columns
- No information is conveyed through colour alone

## Spec-kit-Workflow

Neue Features in diesem Workspace werden nach dem **Specification-Driven Development (SDD)**-Workflow entwickelt.
Der Workflow verwendet das `speckit`-CLI-Tool (GitHub Copilot Skill).

Schritte für ein neues Feature:

1. **Spezifikation erstellen** — `speckit specify "Feature-Name"` → `specs/{branch}/spec.md`
2. **Klärungsfragen** — `speckit clarify` → offene Fragen in `spec.md` beantworten
3. **Implementierungsplan** — `speckit plan` → `specs/{branch}/plan.md`
4. **Aufgabenliste** — `speckit tasks` → `specs/{branch}/tasks.md`
5. **Implementieren** — `speckit implement` → Aufgaben aus `tasks.md` abarbeiten
6. **Validieren** — `bash scripts/check-homogeneity.sh` → Compliance-Score prüfen

Alle Spec-Artefakte werden im Branch-Verzeichnis `specs/{branch}/` gespeichert und versioniert.

---

## Spec-kit Workflow

New features in this workspace are developed following the **Specification-Driven Development (SDD)** workflow.
The workflow uses the `speckit` CLI tool (GitHub Copilot Skill).

Steps for a new feature:

1. **Create specification** — `speckit specify "Feature Name"` → `specs/{branch}/spec.md`
2. **Clarification questions** — `speckit clarify` → answer open questions in `spec.md`
3. **Implementation plan** — `speckit plan` → `specs/{branch}/plan.md`
4. **Task list** — `speckit tasks` → `specs/{branch}/tasks.md`
5. **Implement** — `speckit implement` → work through tasks in `tasks.md`
6. **Validate** — `bash scripts/check-homogeneity.sh` → check compliance score

All spec artefacts are stored and versioned in the branch directory `specs/{branch}/`.

---

## Homogeneity Guardian — Skript-Kurzreferenz / Script Quick Reference

### `scripts/check-homogeneity.sh` / `scripts/check-homogeneity.ps1`

Prüft dieses Projekt auf Compliance (constitution.md, A11Y, Spec-kit, Azubis-Abschnitte, STATS.md).
*Checks this project for compliance (constitution.md, A11Y, Spec-kit, Azubis sections, STATS.md).*

```bash
bash scripts/check-homogeneity.sh

# JSON-Ausgabe für CI/Scripting / JSON output for CI/scripting
bash scripts/check-homogeneity.sh --json
```

```powershell
pwsh scripts/check-homogeneity.ps1
pwsh scripts/check-homogeneity.ps1 -Json
```

---

### `scripts/init-stats.sh` / `scripts/init-stats.ps1`

Schreibt einen Baseline-Eintrag in `STATS.md`. Einmalig nach dem Einrichten ausführen.
*Writes a baseline entry to `STATS.md`. Run once after initial setup.*

```bash
bash scripts/init-stats.sh
```

```powershell
pwsh scripts/init-stats.ps1
```

---

### `scripts/rename-lastenheft.sh` / `scripts/rename-lastenheft.ps1`

Benennt eine Lastenheft-Datei via `git mv` um und committet — fügt Branch-Suffix hinzu.
*Renames a Lastenheft file via `git mv` and commits — adds branch suffix.*

```bash
# Datei umbenennen und committen / Rename and commit
bash scripts/rename-lastenheft.sh Lastenheft_foo.md 002-feature-branch
# Ergebnis / Result: Lastenheft_foo.002-feature-branch.md
```

```powershell
pwsh scripts/rename-lastenheft.ps1 -File Lastenheft_foo.md -Branch 002-feature-branch
```

---

### `scripts/install-hooks.sh` / `scripts/install-hooks.ps1`

Installiert den `pre-push`-Hook nach dem Clonen auf einem neuen Gerät.
*Installs the `pre-push` hook after cloning on a new device.*

```bash
bash scripts/install-hooks.sh
```

```powershell
pwsh scripts/install-hooks.ps1
```

## Für Azubis / For Apprentices

Willkommen! Diese Sektion beschreibt den Einstieg in die Entwicklungsumgebung
für Fachinformatiker-Azubis und andere Einsteiger.

**Voraussetzungen:**

- Git (macOS: `brew install git` / Windows: `winget install Git.Git`)
- PowerShell 7+ (Windows: `winget install Microsoft.PowerShell`)
- ripgrep (macOS: `brew install ripgrep` / Windows: `winget install BurntSushi.ripgrep.MSVC`)
- GitHub CLI (macOS: `brew install gh` / Windows: `winget install GitHub.cli`)

**Ersten Schritt ausführen:**

```bash
# Repository klonen
git clone <repo-url>
cd <projekt-verzeichnis>

# Hooks installieren
bash scripts/install-hooks.sh

# Compliance prüfen
bash scripts/check-homogeneity.sh
```

**Hilfreiche Befehle:**

| Befehl | Beschreibung |
|--------|--------------|
| `bash scripts/check-homogeneity.sh` | Compliance-Bericht anzeigen |
| `bash scripts/init-stats.sh` | Compliance-Baseline in STATS.md schreiben |
| `git log --oneline -10` | Letzte 10 Commits anzeigen |

Bei Fragen: Issue im GitHub-Repository erstellen oder Mentor ansprechen.

---

Welcome! This section describes how to get started with the development
environment for apprentice software developers (Fachinformatiker-Azubis) and
other beginners.

**Prerequisites:**

- Git (macOS: `brew install git` / Windows: `winget install Git.Git`)
- PowerShell 7+ (Windows: `winget install Microsoft.PowerShell`)
- ripgrep (macOS: `brew install ripgrep` / Windows: `winget install BurntSushi.ripgrep.MSVC`)
- GitHub CLI (macOS: `brew install gh` / Windows: `winget install GitHub.cli`)

**First steps:**

```bash
# Clone the repository
git clone <repo-url>
cd <project-directory>

# Install hooks
bash scripts/install-hooks.sh

# Check compliance
bash scripts/check-homogeneity.sh
```

**Useful commands:**

| Command | Description |
|---------|-------------|
| `bash scripts/check-homogeneity.sh` | Show compliance report |
| `bash scripts/init-stats.sh` | Write compliance baseline to STATS.md |
| `git log --oneline -10` | Show last 10 commits |

For questions: open an issue in the GitHub repository or ask your mentor.
