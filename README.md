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
- Bounded SGR-1006 mouse support, host boundaries, keyboard fallback, and proof:
  [`docs/guides/mouse-support.md`](docs/guides/mouse-support.md)
- Bounded terminal session, KOI8-R, raw 8x16 fixture, profiles, and cell proof:
  [`docs/guides/terminal-charset-hardening.md`](docs/guides/terminal-charset-hardening.md)

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

## GitHub-Pages-Veröffentlichung / GitHub Pages Publishing

- Die DocFX-HTML-Seite wird über `.github/workflows/pages.yml` in GitHub
  Actions gebaut, mit Playwright plus axe geprüft und als GitHub-Pages-Artefakt
  veröffentlicht.
- `_site/` und generierte `api/*.yml`-Dateien bleiben bewusst aus Git heraus.
  Im Repository liegen nur Quellen, `docfx.json`, Templates, Guides und
  handgeschriebene Einstiegsseiten wie `api/index.md`.
- In GitHub muss unter `Settings > Pages` die Quelle `GitHub Actions` aktiv sein.
  Nach dem ersten erfolgreichen Lauf auf `main` zeigt das Environment
  `github-pages` die veröffentlichte URL.

- The DocFX HTML site is built by `.github/workflows/pages.yml` in GitHub
  Actions, checked with Playwright plus axe, and published as a GitHub Pages
  artifact.
- `_site/` and generated `api/*.yml` files stay out of Git by design. The
  repository keeps only sources, `docfx.json`, templates, guides, and
  handwritten entry pages such as `api/index.md`.
- In GitHub, `Settings > Pages` must use `GitHub Actions` as the source. After
  the first successful run on `main`, the `github-pages` environment shows the
  published URL.

## CI

- GitHub Actions workflow:
  [`.github/workflows/ci.yml`](.github/workflows/ci.yml)
- GitHub Pages workflow: `.github/workflows/pages.yml`
- The CI workflow validates restore/build/test and generates docfx documentation
  when `docfx.json` is present at repository root.
- The Pages workflow builds the same root DocFX configuration, runs the
  `tests/web-a11y/` smoke test, uploads `_site` as a Pages artifact, and deploys
  it only outside pull requests.
- The CI workflow intentionally fails if no `.sln` or `.csproj` exists yet, to
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

## Spec-Kit-Governance-Presets / Spec Kit Governance Presets

Das registrierte Standardprofil dieser Workspace-Familie umfasst auf Level 0,
Level 1 und Level 2 alle acht Governance-Presets. Eine Teilmenge ist nur als
begründete, dokumentierte Projektausnahme zulässig.

Standard-Preset-Set:

- `security-governance` v0.6.1, Priority 10
- `architecture-governance` v0.5.1, Priority 20
- `isaqb-architecture-governance` v0.2.1, Priority 30
- `a11y-governance` v0.4.1, Priority 40
- `cross-platform-governance` v0.2.1, Priority 50
- `agent-parity-governance` v0.4.0, Priority 60
- `autonomous-run-governance` v0.3.2, Priority 70
- `parallel-autonomous-run-governance` v0.2.3, Priority 80

TuiVision aktiviert zusätzlich `intake-review-governance` v0.1.0 mit Priority
65. Das Preset bleibt außerhalb der Standard-Achtermatrix und bindet den
nächsten Intake vor der Feature-Erstellung an ein aktuelles Review-Ergebnis.

Die ursprünglichen sechs Presets sind seit 2026-05-04 im `github/spec-kit`
Community-Katalog enthalten; `autonomous-run-governance` v0.2.2 wurde dort am
2026-07-17 verifiziert. `parallel-autonomous-run-governance` v0.2.3 ist
eigenständig veröffentlicht; v0.2.2 wurde mit `github/spec-kit#3591` für den
Katalog eingereicht. Installation startet keinen
autonomen oder parallelen Lauf und erteilt keine zusätzlichen Rechte.

Alle acht Presets erzeugen oder verlangen audit-ready Spec-Kit-Run-Evidenz mit
`Applicable` / `N/A` / `Open`, Begründung, Evidenzpfad, Reviewer, Restrisiko und
Follow-up. Die Feldtesterkenntnisse ergänzen exakte Head-/Review-/Check-Gates,
fortsetzbare Closeouts, barrierearme Statusausgabe und geheimnisfreie,
agentenneutrale Runner-Metadaten.

Nach Installation oder Update prüfen:

```bash
bash scripts/install-spec-kit-governance-presets.sh --preset-config scripts/config/spec-kit-intake-review-governance-presets.json --check-only --repo .
specify preset list
specify preset info intake-review-governance
specify preset resolve intake-review-policy-template
```

Wenn Presets Projekt-Policy sind, `.specify/presets/` und erzeugte
Agenten-/Command-Dateien committen; `.specify/presets/.cache/` nicht committen.

*The registered standard profile for this workspace family includes all eight
governance presets at level 0, level 1, and level 2. A subset requires a
justified, documented project exception. Installation starts no autonomous or
parallel run and grants no additional authority. Verify the exact matrix with
`install-spec-kit-governance-presets.* --check-only` / `-CheckOnly`, then use
`specify preset list`, `info`, and `resolve` as applicable. Commit
`.specify/presets/` and generated agent/command files when presets are project
policy; do not commit `.specify/presets/.cache/`.*

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

<!-- statistics-profile-2-readme:begin -->
## Statistikprofil 2 / Statistics Profile 2

Die lebende Projektstatistik steht in `docs/project-statistics.md`. Sie wird reproduzierbar aus `docs/project-statistics.config.json` mit `scripts/render-project-statistics.sh` oder `scripts/render-project-statistics.ps1` erzeugt. Alle Diagramme sind ASCII-only, hoechstens 100 Zeichen breit und durch genaue Werte sowie eine deutsche und englische Textalternative ergaenzt.

*The living project statistics are stored in `docs/project-statistics.md`. They are rendered reproducibly from `docs/project-statistics.config.json` with the Bash or PowerShell renderer. Every chart is ASCII-only, at most 100 characters wide, and accompanied by exact values plus German and English text alternatives.*
<!-- statistics-profile-2-readme:end -->
