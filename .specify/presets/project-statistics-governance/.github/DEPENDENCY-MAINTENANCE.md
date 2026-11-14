# Abhaengigkeiten pflegen / Dependency Maintenance

## Native macOS-Pruefung / Native macOS validation

DE: PR #15 aktualisiert setup-uv und verwendet fuer die unveraenderten nativen
Fixtures `macos-15`. GitHub startet die bisherige Konfiguration
`macos-14-arm64-us-default` nicht mehr (`configuration ... is not supported`).
macOS bleibt eine verpflichtende eigene Plattform; kein Test wird entfernt.
Dokumentationsentscheidung: UpdateRequired. Owner: Repository-Maintainer.
Quelle: `.github/workflows/preset.yml`; Source-only, kein Home-Sync.
Bei weiteren Runner- oder Plattformaenderungen erneut pruefen.

EN: PR #15 updates setup-uv and runs the unchanged native fixtures on
`macos-15`. GitHub rejects the previous `macos-14-arm64-us-default`
configuration as unsupported. macOS remains a required separate platform;
no test is removed. Documentation impact: UpdateRequired. Owner: repository
maintainer. Source: `.github/workflows/preset.yml`; source-only, no Home sync.
Re-evaluate after further runner or platform changes.

## Deutsch

Verbindliche Konfiguration: [Dependabot](dependabot.yml). Owner: Repository-Maintainer.
Versionsupdates werden montags ab 09:30 Uhr (Europe/Berlin) geprueft; die tatsaechliche Startzeit kann abweichen.
Minor- und Patch-Updates sind je Oekosystem gebuendelt, Major-Updates bleiben einzeln.
Je Oekosystem sind hoechstens fuenf Versions-PRs gleichzeitig offen.
Sicherheitsupdates verwenden eigene Gruppen und warten nicht auf den Wochenlauf.
Vorhandene SHA-Pins bleiben erhalten. Es wird kein Auto-Merge eingerichtet.

Das Preset besitzt derzeit keine Paketmanifeste. Dependabot pflegt seine GitHub Actions; neue Paketmanifeste erfordern eine erneute Abdeckungspruefung.
Historische Specs, Archive und Evidence-Fixtures werden nicht automatisch umgeschrieben.
Aktive Produkt-, Beispiel- und Testabhaengigkeiten bleiben erfasst.
Nicht in Manifesten erfasste Downloads und Toolversionen benoetigen weiterhin ihren eigenen Wartungsprozess.

Naechster Schritt: [Dependabot-Laeufe](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/actions/workflows/dependabot/dependabot-updates)
und [Sicherheitsmeldungen](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/security/dependabot) pruefen.
Bei Fehlern den betroffenen Job oeffnen und Manifestpfad, Registry-Zugriff und bestehende Pins kontrollieren.
Neue PRs anhand von Changelog, CI und Projektvorgaben pruefen und regulär freigeben.
Gruene Checks sind keine Produkt-, Plattform- oder Barrierefreiheitsabnahme.

## English

Authoritative configuration: [Dependabot](dependabot.yml). Owner: repository maintainer.
Version checks run on Mondays from 09:30 (Europe/Berlin); actual start times may vary.
Minor and patch updates are grouped per ecosystem; major updates stay separate.
Each ecosystem allows at most five open version PRs.
Security updates use independent groups and do not wait for the weekly run.
Existing SHA pins remain intact. This change configures no auto-merge.

The preset currently has no package manifests. Dependabot maintains its GitHub Actions; new package manifests require a fresh coverage review.
Historical specs, archives and evidence fixtures are not rewritten automatically.
Active product, example and test dependencies remain covered.
Downloads and tool versions outside supported manifests still need their existing maintenance process.

Next, inspect [Dependabot runs](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/actions/workflows/dependabot/dependabot-updates)
and [security alerts](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/security/dependabot).
For failed jobs, check manifest paths, registry access and existing pins in the job log.
Review new PRs using their changelog, CI and project requirements before a regular approval.
Passing checks do not establish product, platform or accessibility acceptance.

## Abdeckung / Coverage

| Oekosystem / Ecosystem | Pfad / Path |
|---|---|
| `github-actions` | `/` |

## Dokumentationsentscheidung / Documentation Decision

UpdateRequired, 2026-11-03. Zielgruppe / audience: Maintainer und / and agents.
Leserpfad / reader path: configuration -> this guide -> Dependabot run or alert -> reviewed PR.
Quelle / source: repository-local configuration. Klasse / class: ActiveSemantic.
Sprache / language: German first, equivalent English in the same file.
Source-only; kein / no Home Sync, runtime distribution or package installation.
Nachweis / evidence: YAML round-trip, Dependabot schema and manifest coverage checks;
hosted checks are recorded on the configuration PR before merge.
Risiko / risk: update proposals still require compatibility and security review.
Neu pruefen bei / reevaluate on: new manifests, moved paths, changed pins or maintenance ownership.
