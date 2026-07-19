# install-spec-kit-governance-presets(1)

## Name

`install-spec-kit-governance-presets` — installiert die zentral konfigurierte GitHub-Spec-Kit-Governance-Preset-Matrix

*installs the centrally configured GitHub Spec Kit governance preset matrix*

## Synopsis

```bash
bash scripts/install-spec-kit-governance-presets.sh [--repo PATH] [--preset-config PATH] [--check-only] [--force] [--dry-run]
```

```powershell
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -Repo PATH -PresetConfig PATH -CheckOnly -Force -WhatIf
```

## Beschreibung / Description

Das Werkzeug liest `scripts/config/spec-kit-governance-presets.json` und
installiert oder prueft die dort genannten acht Governance-Presets in
Level-0-, Level-1- und Level-2-Spec-Kit-Repositories.
Die Skriptlogik enthaelt keine fest eingebauten Preset-Versionen. Neue
Versionen werden durch Aktualisieren der zentralen Matrix wirksam.

*The tool reads `scripts/config/spec-kit-governance-presets.json` and installs
the eight listed governance presets in level-0, level-1, and level-2 Spec Kit
repositories. The script logic
does not hard-code preset versions. New versions become effective by updating
the central matrix.*

Die Installation ist projektlokal. `.specify/presets/` wird versioniert, waehrend
`.specify/presets/.cache/` nicht committed wird.

*Installation is project-local. `.specify/presets/` is versioned, while
`.specify/presets/.cache/` is not committed.*

Nach der Installation normalisiert das Werkzeug trailing whitespace in
Markdown-Dateien unter `.specify/presets/`. Dadurch blockieren fremde
Preset-ZIP-Artefakte keine lokalen Git-Hooks; fachliche Inhalte bleiben
unveraendert.

*After installation, the tool normalizes trailing whitespace in Markdown files
under `.specify/presets/`. This prevents external preset ZIP artefacts from
blocking local Git hooks; semantic content remains unchanged.*

## Optionen / Options

| Bash | PowerShell | Bedeutung / Meaning |
|---|---|---|
| `--repo PATH` | `-Repo PATH` | Ziel-Repository; wiederholbar |
| `--preset-config PATH` | `-PresetConfig PATH` | Alternative Preset-Matrix |
| `--check-only` | `-CheckOnly` | Exakte installierte Matrix ohne Schreibzugriff prüfen |
| `--force` | `-Force` | Vorhandene Presets entfernen und aus der Matrix neu installieren |
| `--dry-run` | `-WhatIf` | Nur anzeigen, keine Schreiboperationen |

## Beispiele / Examples

```bash
bash scripts/install-spec-kit-governance-presets.sh --dry-run
bash scripts/install-spec-kit-governance-presets.sh --check-only
bash scripts/install-spec-kit-governance-presets.sh --repo ~/SecureCaseTrackerProjects/SecureCaseTracker-CSharp
bash scripts/install-spec-kit-governance-presets.sh --repo ~/SecureCaseTrackerProjects/SecureCaseTracker-CSharp --force
```

```powershell
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -WhatIf
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -CheckOnly
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -Repo ~/SecureCaseTrackerProjects/SecureCaseTracker-CSharp
pwsh -NoProfile -File scripts/install-spec-kit-governance-presets.ps1 -Repo ~/SecureCaseTrackerProjects/SecureCaseTracker-CSharp -Force
```

## Nachweis / Evidence

Nach der Installation:

```bash
specify preset list
specify preset info security-governance
specify preset resolve spec-template
```

*After installation, verify with `specify preset list`, at least one
`specify preset info`, and a relevant `specify preset resolve` check.*
