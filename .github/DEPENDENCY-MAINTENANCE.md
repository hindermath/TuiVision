# Abhaengigkeiten pflegen / Dependency Maintenance

## Deutsch

Verbindliche Konfiguration: [Dependabot](dependabot.yml). Owner: Repository-Maintainer.
Versionsupdates werden montags ab 06:40 Uhr (Europe/Berlin) geprueft; die tatsaechliche Startzeit kann abweichen.
Minor- und Patch-Updates sind je Oekosystem gebuendelt, Major-Updates bleiben einzeln.
Je Oekosystem sind hoechstens fuenf Versions-PRs gleichzeitig offen.
Sicherheitsupdates verwenden eigene Gruppen und warten nicht auf den Wochenlauf.
Vorhandene SHA-Pins bleiben erhalten. Es wird kein Auto-Merge eingerichtet.

Kopierte Maintenance-TUI-Dateien und zentral verteilte Wartungsworkflows werden in home-baseline gepflegt. Ihre Aktualisierung erfolgt ueber den bestehenden Propagationsprozess.
Historische Specs, Archive und Evidence-Fixtures werden nicht automatisch umgeschrieben.
Aktive Produkt-, Beispiel- und Testabhaengigkeiten bleiben erfasst.
Nicht in Manifesten erfasste Downloads und Toolversionen benoetigen weiterhin ihren eigenen Wartungsprozess.

Naechster Schritt: [Dependabot-Laeufe](https://github.com/hindermath/TuiVision/actions/workflows/dependabot/dependabot-updates)
und [Sicherheitsmeldungen](https://github.com/hindermath/TuiVision/security/dependabot) pruefen.
Bei Fehlern den betroffenen Job oeffnen und Manifestpfad, Registry-Zugriff und bestehende Pins kontrollieren.
Neue PRs anhand von Changelog, CI und Projektvorgaben pruefen und regulär freigeben.
Gruene Checks sind keine Produkt-, Plattform- oder Barrierefreiheitsabnahme.

## English

Authoritative configuration: [Dependabot](dependabot.yml). Owner: repository maintainer.
Version checks run on Mondays from 06:40 (Europe/Berlin); actual start times may vary.
Minor and patch updates are grouped per ecosystem; major updates stay separate.
Each ecosystem allows at most five open version PRs.
Security updates use independent groups and do not wait for the weekly run.
Existing SHA pins remain intact. This change configures no auto-merge.

Copied Maintenance TUI files and centrally distributed maintenance workflows are maintained in home-baseline and updated through the existing propagation process.
Historical specs, archives and evidence fixtures are not rewritten automatically.
Active product, example and test dependencies remain covered.
Downloads and tool versions outside supported manifests still need their existing maintenance process.

Next, inspect [Dependabot runs](https://github.com/hindermath/TuiVision/actions/workflows/dependabot/dependabot-updates)
and [security alerts](https://github.com/hindermath/TuiVision/security/dependabot).
For failed jobs, check manifest paths, registry access and existing pins in the job log.
Review new PRs using their changelog, CI and project requirements before a regular approval.
Passing checks do not establish product, platform or accessibility acceptance.

## Abdeckung / Coverage

| Oekosystem / Ecosystem | Pfad / Path |
|---|---|
| `github-actions` | `/` |
| `nuget` | `/` |
| `npm` | `/tests/web-a11y` |
| `dotnet-sdk` | `/` |

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

## Claude-GitHub-Integration entfernt / Claude GitHub Integration Retired

DE: Die Claude-GitHub-App und ihre automatischen PR-/Issue-Workflows wurden
auf ausdruecklichen Ownerwunsch entfernt. Fuer dieses Repository starten keine
Claude-Code-GitHub-Actions mehr. Lokale Claude-Werkzeuge und CLAUDE.md bleiben
unabhaengige Entwicklungswerkzeuge. Technische Checks und menschliche Reviews
gelten weiter; Abhaengigkeits-PRs erhalten kein Auto-Merge.

EN: The owner explicitly retired the Claude GitHub App and its automatic
PR/issue workflows. This repository no longer starts Claude Code GitHub Actions.
Local Claude tools and CLAUDE.md remain independent development tools.
Technical checks and human reviews remain required; dependency PRs receive
no auto-merge.

## CI-Runner / CI runners

DE: Technische macOS-Pruefungen verwenden macos-15, da GitHub macos-14 am
2. November 2026 eingestellt hat. Pruefumfang, Berechtigungen und SHA-Pins bleiben erhalten.
EN: Technical macOS checks use macos-15 because GitHub retired macos-14 on
2 November 2026. Test coverage, permissions and SHA pins are retained.

Quelle / Source: https://github.blog/changelog/2026-10-01-github-actions-macos-14-runner-image-retirement/

## PR #216: technische Wartung / Technical maintenance

DE: Die fuer diesen Abhaengigkeits-PR erforderlichen CI-/Test-Anpassungen
erhalten Versionsbindung, Sicherheitspruefungen und Plattform-Gates. Die
konkrete Reparatur und technische Evidence stehen im PR; keine Paketmigration
aus einem anderen zurueckgestellten PR wird uebernommen. Owner: Repository-
Maintainer. Source-only, kein Home-Sync. Bei Manifest-, Pin- oder Workflow-
Aenderungen erneut pruefen.

EN: CI/test repairs needed for this dependency PR retain version binding,
security checks and platform gates. The PR records the specific repair and
technical evidence; it does not adopt a migration from another deferred PR.
Owner: repository maintainer. Source-only, no Home sync. Re-evaluate after
manifest, pin or workflow changes.
