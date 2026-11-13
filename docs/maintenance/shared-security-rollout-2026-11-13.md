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

### Integrationsnachweis / Integration evidence

Die aktuelle Paketpruefung verwendet einen neuen Installationsnachweis fuer den veroeffentlichten Stand; der alte v0.1.0-Nachweis bleibt unveraendert. Verteilte Wartungstests werden an ihre vorhandenen kanonischen Quellen angeglichen. Die Vorschau prueft Schreibfreiheit; der getrennte Schreibtest verwirft weiterhin Pfad-Ausbrueche.

The current package proof uses a successor installation receipt for the published version; the historical v0.1.0 receipt is unchanged. Distributed maintenance tests follow their existing canonical sources. Preview checks that no writes occur; the separate writing test still rejects path escapes.

DE: Fuenf fruehere Pfadmigrationen werden nun als GitBlob-belegte LegacyAdoption gebunden, weil historischer und aktueller Zielpfad verschieden sind. Ihre bisherigen Receipt-Bytes und historischen Vorgaenger bleiben unveraendert archiviert. Intake-Inhalte, IDs, Ziel-Hashes, Lifecycle und Lieferautoritaet bleiben erhalten. Die fuenf reparierten Receipts bestehen beide bestehenden Shell-Validatoren. Die Gesamtpruefung bleibt wegen des separat reproduzierten Standalone-Archivfehlers bei Intake 24 offen; kein Review oder Spec-Kit-Lauf startet.

EN: Five earlier path relocations now use GitBlob-backed LegacyAdoption because historical and current target paths differ. Previous receipt bytes and historical predecessors remain archived unchanged. Intake content, IDs, target hashes, lifecycle and delivery authority remain. Both existing shell validators pass for the five repaired receipts. Overall validation is still blocked by the separately reproduced standalone-archive issue at intake 24; no review or Spec Kit execution starts.

## Intake-Authoring-Folgekorrektur 0.3.14

DE: Die aktuelle Installation verwendet Intake Authoring **0.3.14** aus
[Preset-PR 28](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/28)
und [Release v0.3.14](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases/tag/v0.3.14).
Alle 72 Archivdateien wurden gegen den Tag-Commit geprüft. Die Folgekorrektur
ordnet ein archiviertes Standalone-Intake außerhalb einer unabhängigen Serie
nur bei eindeutigem Namen, Hash und physischer Verzeichnisbindung zu.
Seriengebundene Intakes behalten ihre Manifestgrenze. Die anderen 13 Versionen
und sämtliche Prioritäten bleiben erhalten. Aktuelle Level-0-Bindung:
[PR 374](https://github.com/hindermath/home-baseline/pull/374).
Die frühere Freigabe zum manuellen Schließen von Fehlalarmen gilt nur für
explizit genehmigte IDs bzw. drei bytegebundene Signaturen mit erneut geprüftem
Datenfluss; alle Abweichungen bleiben offen. Keine allgemeine Risikoannahme.

## Intake Authoring follow-up 0.3.14

EN: The current installation uses Intake Authoring **0.3.14** from
[preset PR 28](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/pull/28)
and [release v0.3.14](https://github.com/hindermath/spec-kit-preset-intake-authoring-governance/releases/tag/v0.3.14).
All 72 archive files were checked against the tag commit. The follow-up resolves
an archived standalone intake outside an unrelated series only with a unique
name, hash and physical directory binding. Series members retain their manifest
boundary. The other 13 versions and all priorities remain. Current Level-0 binding:
[PR 374](https://github.com/hindermath/home-baseline/pull/374).
Manual false-positive closure applies only to explicitly approved IDs or three
byte-bound signatures with a renewed dataflow review; deviations remain open.
No general risk acceptance is inferred.
