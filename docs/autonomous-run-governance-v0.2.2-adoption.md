# Adoption von `autonomous-run-governance` v0.2.2

Stand: 2026-07-16

## Zweck

Diese Evidence dokumentiert die begrenzte Aktualisierung des optionalen
Spec-Kit-Presets von v0.2.1 auf v0.2.2. Die neue Version liefert ein
ausführliches Bedien- und Lernhandbuch und schließt eine kleine
Zustandsvokabular-Lücke: `Deliver` bleibt eine lesbare Skill-Überschrift,
während der maschinenlesbare Run State für Remote-Closeout nur `Publish`,
`Review` oder `MergeAndSync` verwendet.

*This evidence documents the bounded update of the optional Spec Kit preset
from v0.2.1 to v0.2.2. The release adds an extensive operating and learning
guide and closes one small state-vocabulary gap: `Deliver` remains a readable
skill heading, while machine-readable remote-closeout state uses only
`Publish`, `Review`, or `MergeAndSync`.*

## Herkunft und Integrität

| Nachweis | Ergebnis |
|---|---|
| Reales Feldsignal | TuiVision Feature 029, PR [#84](https://github.com/hindermath/TuiVision/pull/84), Merge `e825b7d` |
| Home-Baseline-Produktisierung | PR [hindermath/home-baseline#70](https://github.com/hindermath/home-baseline/pull/70), Merge `d77e92b` |
| Öffentliches Preset | PR [hindermath/spec-kit-preset-autonomous-run-governance#7](https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/pull/7), Merge `6c737d1` |
| Release | [`v0.2.2`](https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.2.2) |
| Tag-ZIP | `https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.2.2.zip` |
| SHA-256 | `e9f1bea43d99c891242516767aa06491e22b6f89d1f932a3837e970d05cf4f0d` |
| Paketparität | Entpacktes Tag-ZIP und `.specify/presets/autonomous-run-governance/` sind inhaltlich identisch |
| Spec-Kit-Auflösung | Spec Kit 0.12.11 meldet v0.2.2, Priorität 70 und 18 Beiträge; die sechs Standard-Presets bleiben unverändert |

*The installed payload matches the released tag ZIP. Spec Kit 0.12.11 resolves
v0.2.2 at priority 70 with 18 contributions while the six baseline presets
remain unchanged.*

## Übernommene Regeln

1. Die README erklärt Zweck, Zielgruppen, Liefermodi, Konvergenz,
   Installation, vollständige Prompt-Beispiele, Status, Stop, Resume,
   Run-State- und Exact-Head-Evidence, Retrospektive, Lernreihenfolge und
   Fehlersuche.
2. Ein menschenlesbarer Workflow-Abschnitt darf nicht als Maschinenzustand
   abgeleitet werden.
3. Remote-Closeout speichert je nach laufender Operation `Publish`, `Review`
   oder `MergeAndSync`.
4. Die Validatoren bleiben streng und akzeptieren keinen Alias `Deliver`.
5. Alle Berechtigungs-, Resume-, Exact-Head- und Mandatory-Rule-Delta-Grenzen
   aus v0.2.1 bleiben unverändert.

*The operating guide is now part of the package. Human section labels are not
machine-state vocabulary. The strict validators and all existing permission,
resume, exact-head, and mandatory-rule-delta boundaries remain unchanged.*

## Agent- und Command-Parität

| Oberfläche | Eindeutige Commands/Skills | Ergebnis |
|---|---:|---|
| Codex und Antigravity über `.agents/skills/` | 5 | PASS |
| Claude über `.claude/skills/` | 5 | PASS |
| Copilot Agents über `.github/agents/` | 5 | PASS |
| Copilot Prompts über `.github/prompts/` | 5 | PASS |
| OpenCode über `.opencode/command/` | 5 | PASS |
| Codex-UI-Metadaten `agents/openai.yaml` | 5 | PASS, projektgepflegt |

Der Installer ersetzt den projektgebundenen Codex-Skill zunächst durch den
portablen Command und entfernt die fünf lokal gepflegten
`agents/openai.yaml`-Dateien. Die Adoption stellt deshalb den TuiVision-Skill
mit Nummerierungs-, Build-Zähler-, DocFX-/A11Y- und historischen
Source-Verträgen sowie die UI-Metadaten wieder her. Die neue Stage-Regel wird
in diesen lokalen Override übernommen.

*The installer initially replaces the project-owned Codex skill and removes
the five local UI metadata files. Adoption restores both while carrying the new
canonical stage rule into the TuiVision override.*

## Lokale Validierung

| Prüfung | Ergebnis |
|---|---|
| `specify check` | PASS |
| `specify preset list`, `info`, `resolve` | PASS; sieben Presets und v0.2.2/70/18 |
| Tag-ZIP-Prüfsumme und Payload-Diff | PASS |
| Fünf Commands je gepflegter Agent-Oberfläche | PASS |
| Fünf Codex-UI-YAML-Dateien | PASS |
| Skill-Frontmatter und UI-YAML-Parsing | PASS; Preset plus 5/5 UI-YAML-Dateien |
| Agent-Guidance-Parität | PASS; deutscher und englischer v0.2.2-Block jeweils 5/5 |
| Stage-Regel in Preset-Command, Runbook und lokalem Override | PASS |
| Bash-/PowerShell-Run-State-Validatoren | PASS; `Publish` gültig, `Deliver` wird von beiden abgelehnt |
| `git diff --check` | PASS |
| `dotnet format --verify-no-changes --no-restore` | PASS |
| Secret-Scan | PASS; `high=0`, lokale Agent-Konfiguration bleibt bekannter Medium-Hinweis |
| `docfx docfx.json` | PASS; nach explizitem Tool-`PATH` 0 Warnungen, 0 Fehler |
| `npm run test:docfx` | PASS; DocFX erneut 0/0, Playwright/Axe 2/2 |
| UTF-8-`lynx` | PASS; Zweck, Herkunft, Regeln, Parität und Grenzen sind textorientiert verständlich |

Der erste DocFX-Aufruf fand im eingeschränkten App-`PATH` den internen
`dotnet`-Prozess nicht. Der wiederholte Lauf mit explizitem Homebrew- und
`.dotnet/tools`-Pfad bestand vollständig; dies war eine lokale
Werkzeugauflösungsgrenze und kein Dokumentationsfehler.

*The first DocFX invocation could not find its internal `dotnet` process in the
restricted app PATH. Repeating with explicit Homebrew and `.dotnet/tools`
paths passed completely; this was a local tool-resolution boundary, not a
documentation defect.*

## Grenzen und nächste Reihenfolge

- Keine C#-Runtime, öffentliche API, Abhängigkeit, Paketversion, Beispiel-
  Portierung oder historische Quelle wird geändert.
- Keine .NET-Build-, Test- oder Coverage-Ausführung wird durch diese Preset-,
  Guidance- und Dokumentationsadoption ausgelöst.
- Feature 029 ist fachlich abgeschlossen.
- Feature 030 bleibt der einzige nächste Intake und wird in dieser Adoption
  nicht gestartet.
- Wave 5 und Wave 6 bleiben bis nach den vorgelagerten Audit- und
  Hardening-Läufen gesperrt.
- Ein gebündeltes Community-Preset-Update bleibt bis zum vereinbarten
  Pre-Wave-5-Zeitpunkt zurückgestellt.

*No runtime, API, dependency, example, or historical-source change is included.
Feature 030 remains the sole next intake and is not started by this adoption.*
