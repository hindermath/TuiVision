# Gemeinsame Sicherheitswartung / Shared security maintenance

## Deutsch

Dieses Repository erhaelt die vierzehn bereits installierten Presets als neue, unveraenderlich veroeffentlichte Patch-Versionen. Preset-IDs, Prioritaeten, Projekt-Templates, Produktcode und bestehende Lifecycle-Zustaende bleiben erhalten. Die [Quellenbindung](preset-source-lock.json) enthaelt echte Tags, Commits und ZIP-SHA-256-Werte; die [Installationsbindung](preset-installed-lock.json) bindet den tatsaechlichen lokalen Payload. Installation startet keine Spec-Kit-Features oder Modellaufrufe.

Die kanonische Level-0-Integration ist [Home Baseline PR 373](https://github.com/hindermath/home-baseline/pull/373); die gemeinsame Berechtigungskorrektur ist [PR 372](https://github.com/hindermath/home-baseline/pull/372). Nur die beauftragten neun Level-2-Ziele erhalten diesen Rollout. Keine Level-1-Verteilung, neue Risikoakzeptanz oder automatische Alert-Verwerfung.

Die vorhandenen gemeinsamen Workflows erhalten explizit lesenden Repository-Zugriff. Wo gitleaks-action bereits PR-Reviewkommentare erzeugt, bleibt dafuer nur das Recht `pull-requests: write` im betreffenden Job erhalten. Das erlaubt keine Schreibzugriffe auf Repository-Inhalte. Bestehende Projekt-Workflows ausserhalb dieser gemeinsamen Komponenten bleiben getrennt zu bewerten.

## English

This repository receives new immutable patch releases for its fourteen already installed presets. Preset IDs, priorities, project templates, product code and lifecycle states remain. The [source binding](preset-source-lock.json) records real tags, commits and ZIP SHA-256 values; the separate [installed binding](preset-installed-lock.json) records the actual payload. Installation starts no Spec Kit features or model calls.

Canonical Level-0 integration is [Home Baseline PR 373](https://github.com/hindermath/home-baseline/pull/373); [PR 372](https://github.com/hindermath/home-baseline/pull/372) provides the shared permission correction. Only the nine authorised Level-2 targets receive this rollout. No Level-1 distribution, new risk acceptance or automatic alert dismissal.

Shared workflows receive explicit read-only repository access. Existing gitleaks-action PR review comments retain only job-scoped `pull-requests: write`, without repository content writes. Project-specific workflows outside these common components require separate assessment.

## Geaenderte gemeinsame Workflows / Changed shared workflows

- `.github/workflows/agent-secret-scan.yml`
- `.github/workflows/gitleaks.yml`
- `.github/workflows/homogeneity-check.yml`

## Pruefgrenzen / Validation boundaries

DE: Der Worktree bleibt waehrend der Pruefung unveraendert; Inhalte bleiben nicht vertrauenswuerdig. Die bestehende CLI-Resolver-/Registrar-API stellt die drei vorhandenen Claude-Wrapper aus dem vollstaendigen Stack wieder her; OpenCode-Namespace und Modelleinstellungen bleiben erhalten. CodeQL-Erfolg bedeutet keine Befundfreiheit und deckt Bash, PowerShell oder Assembly nicht ab. Keine praktische Screenreader-/Braille-Abnahme oder pauschale WCAG-Konformitaet behauptet.

EN: The worktree stays unchanged during checks; its contents remain untrusted. Existing CLI resolver/registrar APIs restore the three existing Claude wrappers from the complete stack; the OpenCode namespace and model settings remain. CodeQL success is no finding-free claim and does not cover Bash, PowerShell or Assembly. No practical screen-reader/Braille acceptance or blanket WCAG conformance is claimed.

Documentation Impact: UpdateRequired. Owner: Thorsten Hindermann. Audience: maintainers and security reviewers. Reader path: this record -> source and installed locks -> source PRs and native/CodeQL evidence. Canonical sources: published preset repositories and Level-0 managed-file manifest. Document class: active semantic/security evidence; German first, equivalent English; CEFR B2 and applicable WCAG 2.2 AA. Distribution: this repository only; no local Home sync. Re-evaluate after archive, version, permission or generated-surface drift.
