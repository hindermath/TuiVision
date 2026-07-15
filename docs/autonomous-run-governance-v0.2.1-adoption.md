# Adoption von `autonomous-run-governance` v0.2.1

Stand: 2026-07-15

## Zweck

Diese Evidence dokumentiert die begrenzte Aktualisierung des optionalen
Spec-Kit-Presets von v0.2.0 auf v0.2.1. Die Resume-Logik gleicht nach Preset-
oder Governance-Drift neue zwingende Korrektheits-, Sicherheits-,
Berechtigungs- und Evidenzregeln mit akzeptierten Plan-, Task- und
Checklist-Artefakten ab. Sie erweitert weder Feature-Scope noch Remote-Rechte.

*This evidence documents the bounded update of the optional Spec Kit preset
from v0.2.0 to v0.2.1. After preset or governance drift, resume reconciles new
mandatory correctness, security, permission, and evidence rules with accepted
Plan, Tasks, and checklist artifacts. It expands neither feature scope nor
remote authority.*

## Herkunft und Integrität

| Nachweis | Ergebnis |
|---|---|
| Reales Feldsignal | TuiVision Feature 028, PR [#79](https://github.com/hindermath/TuiVision/pull/79), Merge `28f23cc` |
| Home-Baseline-Produktisierung | PR [hindermath/home-baseline#67](https://github.com/hindermath/home-baseline/pull/67), Merge `37a3e6e` |
| Öffentliches Preset | PR [hindermath/spec-kit-preset-autonomous-run-governance#6](https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/pull/6), Merge `ac59d8a` |
| Release | [`v0.2.1`](https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.2.1) |
| Tag-ZIP | `https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.2.1.zip` |
| SHA-256 | `799cc189e10893c2fd7106b6f6532fc02a1fc10a65d66b95139465f2acb6cf75` |
| Paketparität | Entpacktes Tag-ZIP und `.specify/presets/autonomous-run-governance/` sind bytegleich |
| Spec-Kit-Auflösung | Spec Kit 0.12.11 meldet v0.2.1, Priorität 70 und 18 Beiträge; die sechs Standard-Presets bleiben unverändert |

*The installed payload matches the released tag ZIP byte for byte. Spec Kit
0.12.11 resolves v0.2.1 at priority 70 with 18 contributions while the six
baseline presets remain unchanged.*

## Übernommene Regel

Der echte 028-Resume rekonstruierte Zustand und Berechtigung korrekt und führte
Analyze erneut aus. Die akzeptierten Tasks waren jedoch vor der inzwischen
zwingenden Marker-Consumer-Suche erzeugt worden. Deshalb fand erst die erste
Remote-CI zwei veraltete Assertions. v0.2.1 ergänzt keinen zweiten
Marker-Sonderfall, sondern die fehlende Migrationsregel:

1. aktuelle zwingende Preset-Regeln mit Plan, Tasks und Checklists vergleichen,
2. nur anwendbare fehlende Regeln in-place ergänzen,
3. Readiness und Analyze erneut ausführen,
4. akzeptierten Scope und frühere Entscheidungen erhalten,
5. reine Effizienzpräferenzen nur retrospektiv behandeln.

*The real 028 resume reconstructed state and authority correctly and reran
Analyze. Its accepted tasks predated the now-mandatory marker-consumer search,
so the first remote CI found two stale assertions. Version 0.2.1 adds the
missing migration rule, not another project-specific marker exception.*

## Agent- und Command-Parität

| Oberfläche | Eindeutige Commands/Skills | Ergebnis |
|---|---:|---|
| Codex und Antigravity über `.agents/skills/` | 5 | PASS |
| Claude über `.claude/skills/` | 5 | PASS |
| Copilot Agents über `.github/agents/` | 5 | PASS |
| Copilot Prompts über `.github/prompts/` | 5 | PASS |
| OpenCode über `.opencode/command/` | 5 | PASS |
| Codex-UI-Metadaten `agents/openai.yaml` | 5 | PASS, projektgepflegt |

Der projektgebundene Codex-Skill `.agents/skills/speckit-autonomous/SKILL.md`
bleibt genau einmal als lokaler Override erhalten. Der Installer ersetzt diese
Datei zunächst durch den portablen Command und entfernt die fünf
`agents/openai.yaml`-Dateien. Die Adoption stellt den TuiVision-Override mit
seinen Nummerierungs-, Build-Zähler-, DocFX-/A11Y- und historischen
Source-Verträgen sowie die unveränderten UI-Metadaten bewusst wieder her.

Spec Kit 0.12.11 erzeugt Custom-Preset-Commands im validierten Copilot-
Legacy-Modus als Agent/Prompt-Paare. Der neue Copilot-Skills-Modus erzeugt
aktuell nur gebündelte Core-Skills, aber keine Custom-Preset-Commands. TuiVision
behält deshalb seine vorhandenen Agent-/Prompt-Flächen; dies ist ein externer
CLI-Kompatibilitätspunkt und kein bestandener v0.2.1-Nachweis.

*The project-owned Codex orchestration skill remains the single local override,
and its five UI metadata files are restored after installation. Copilot legacy
agents/prompts remain the validated project surface; missing custom preset
commands in Spec Kit 0.12.11 Copilot skills mode are recorded as an external CLI
compatibility point.*

## Lokale Validierung

| Prüfung | Ergebnis |
|---|---|
| `specify check` | PASS |
| `specify preset list`, `info`, `resolve` | PASS; sieben Presets, v0.2.1/70/18 |
| Tag-ZIP-Prüfsumme und Payload-Diff | PASS |
| Fünf Commands je gepflegter Agent-Oberfläche | PASS |
| Fünf Codex-UI-YAML-Dateien | PASS |
| Skill-Frontmatter und UI-YAML-Parsing | PASS, jeweils 5/5 |
| Agent-Guidance-Parität | PASS; deutscher und englischer v0.2.1-Block jeweils 5/5 identisch |
| Resume-Delta in Command, Runbook, Template und lokalem Override | PASS |
| `quick_validate.py` | Externe Schema-Grenze: der aktuelle Codex-Validator akzeptiert `compatibility` nicht, obwohl Spec Kit 0.12.11 dieses Feld erzeugt; YAML und installierte Skills bleiben nutzbar |
| `git diff --check` | PASS |
| `dotnet format --verify-no-changes --no-restore` | PASS |
| Secret-Scan | PASS; Gitleaks ohne Fund, `high=0`; lokale Agent-Konfiguration bleibt ein bekannter Medium-Hinweis |
| `docfx docfx.json` | PASS; 0 Warnungen, 0 Fehler |
| `npm run test:docfx` | PASS; DocFX erneut 0/0, Playwright/Axe 2/2 |
| UTF-8-`lynx` | PASS; Zweck, Herkunft, Regel, Parität und Grenzen bleiben textorientiert verständlich |
| Exakter Staging-Kandidat | PASS; `git diff --cached --check`, Pfadinventar und Scope-Abgleich sind vor dem Commit grün |

## Grenzen und nächste Reihenfolge

- Keine C#-Runtime, öffentliche API, Abhängigkeit, Paketversion, Beispiel-
  Portierung oder historische Quelle wird geändert.
- Keine .NET-Build-, Test- oder Coverage-Ausführung wird durch diese reine
  Preset-, Guidance- und Dokumentationsadoption ausgelöst.
- Feature 028 ist fachlich gemergt; sein kausaler Evidence-Closeout folgt erst
  nach dieser Adoption.
- Wave 5 und Wave 6 bleiben bis Feature 029 gesperrt.
- Feature 029 bleibt der einzige nächste Intake und wird in diesem Lauf nicht
  gestartet.
- Ein Community-Catalog-Update bleibt bis zum vereinbarten gebündelten
  Pre-Wave-5-Zeitpunkt zurückgestellt.

*No runtime, API, dependency, example, or historical-source change is included.
Feature 029 remains the sole next intake and is not started by this adoption.*
