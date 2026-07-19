# Feldnachweise / Field Evidence

[Handbuch / Manual](../README.md) | [Preset-README](../../README.md)

## Deutsch

Die folgenden Berichte sind historische, kampagnenspezifische Evidence. Sie
werden durch diesen Dokumentations-Patch nicht rueckwirkend veraendert.

| Nachweis | Umfang | Kernaussage |
|---|---|---|
| [Nativer macOS-Smoke](native-macos-smoke-2026-07-18.md) | Lokaler Real-Agent-Smoke | Grundlegende Runner- und Worktree-Faehigkeit |
| [Nativer Multi-Agent-Smoke](native-multi-agent-smoke-2026-07-19.md) | Mehrere Agentenfamilien | Agentenneutrale Profile und Berechtigungsgrenzen |
| [Secure-CaseTracker-Feldtest](secure-casetracker-native-field-2026-07-18.md) | 24 Worker, sechs MSL-Sprachen | Parallelitaet 3, Stop/Status/Resume, PRs und Closeout |
| [Feldtest-Retrospektive](secure-casetracker-native-field-retrospective-2026-07-19.md) | Portable und projektspezifische Erkenntnisse | Grundlage fuer Schema 1.1 und fortsetzbare Konsolidierung |

### Interpretationsgrenzen

- Der native macOS-Override galt nur fuer die dokumentierte
  Entwicklungskampagne und ist abgelaufen.
- Drei ist die real validierte maximale Parallelitaet.
- Provider-, Billing-, Sprach- und Modellbeobachtungen werden nicht pauschal zu
  Preset-Regeln verallgemeinert.
- Feld-Evidence erteilt keine Berechtigung fuer eine neue Kampagne.

## English

These reports are historical, campaign-specific evidence. This documentation
patch does not rewrite them retrospectively.

| Evidence | Scope | Main result |
|---|---|---|
| [Native macOS smoke](native-macos-smoke-2026-07-18.md) | Local real-agent smoke | Basic runner and worktree capability |
| [Native multi-agent smoke](native-multi-agent-smoke-2026-07-19.md) | Several agent families | Agent-neutral profiles and authority boundaries |
| [Secure CaseTracker field test](secure-casetracker-native-field-2026-07-18.md) | 24 workers, six MSL languages | Concurrency 3, stop/status/resume, PRs, and closeout |
| [Field-test retrospective](secure-casetracker-native-field-retrospective-2026-07-19.md) | Portable and project-specific findings | Basis for schema 1.1 and resumable consolidation |

The native macOS override applied only to the documented development campaign
and has expired. Three is the maximum concurrency validated in real use.
Provider, billing, language, and model observations are not generalized into
blanket rules. Field evidence grants no authority for a new campaign.
