# Adoption von `autonomous-run-governance` v0.2.0

Stand: 2026-07-14

## Zweck

Diese Evidence dokumentiert die begrenzte Aktualisierung des optionalen
Spec-Kit-Presets von v0.1.4 auf v0.2.0. Die Aktualisierung ergänzt einen
lesenden Status, kooperatives Stoppen, geschützte Wiederaufnahme und einen
validierten feature-lokalen Laufzustand. Sie setzt Feature 028 nicht fort.

*This evidence documents the bounded update of the optional Spec Kit preset
from v0.1.4 to v0.2.0. The update adds read-only status, cooperative stop,
protected resume, and validated feature-local run state. It does not resume
Feature 028.*

## Herkunft und Integrität

| Nachweis | Ergebnis |
|---|---|
| Home-Baseline-Produktisierung | PR [hindermath/home-baseline#65](https://github.com/hindermath/home-baseline/pull/65), Merge `608d148` |
| Öffentliches Preset | PR [hindermath/spec-kit-preset-autonomous-run-governance#5](https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/pull/5), Merge `7bd8ef3` |
| Release | [`v0.2.0`](https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.2.0) |
| Tag-ZIP | `https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.2.0.zip` |
| SHA-256 | `7cde2b22306906e298decefd5e6af0e4f6848eb32e188837f122ade22fc17237` |
| Paketparität | Entpacktes Tag-ZIP und `.specify/presets/autonomous-run-governance/` sind bytegleich |
| Spec-Kit-Auflösung | v0.2.0, Priorität 70, 18 Beiträge; die sechs Standard-Presets bleiben unverändert aktiviert |

*The released tag ZIP matches the installed preset payload byte for byte. Spec
Kit resolves v0.2.0 at priority 70 with 18 contributions while the six baseline
presets remain enabled and unchanged.*

## Agent- und Command-Parität

Die fünf portablen Commands sind `speckit.autonomous`,
`speckit.autonomous-retrospective`, `speckit.autonomous-status`,
`speckit.autonomous-stop` und `speckit.autonomous-resume`.

| Oberfläche | Eindeutige Commands/Skills | Ergebnis |
|---|---:|---|
| Codex und Antigravity über `.agents/skills/` | 5 | PASS |
| Claude über `.claude/skills/` | 5 | PASS |
| Copilot Agents über `.github/agents/` | 5 | PASS |
| Copilot Prompts über `.github/prompts/` | 5 | PASS |
| OpenCode über `.opencode/command/` | 5 | PASS |

Der projektgebundene Codex-Skill `.agents/skills/speckit-autonomous/SKILL.md`
bleibt genau einmal als lokaler Override erhalten. Er ergänzt die portablen
Regeln um TuiVision-spezifische Nummerierung, Build-Zähler, DocFX-/A11Y-Gates
und historische Quellen. Die drei neuen Codex-Skills besitzen jeweils
projektgepflegte `agents/openai.yaml`-Metadaten. `GEMINI.md` bleibt die
Antigravity-kompatible Kontextoberfläche; eine Gemini-CLI-Installation ist
nicht erforderlich.

*The project-owned Codex orchestration skill remains exactly once as the local
override. It retains TuiVision numbering, build-counter, DocFX/A11Y, and
historical-source contracts. The three new Codex skills carry project-owned
OpenAI UI metadata. `GEMINI.md` remains the Antigravity-compatible context
surface; Gemini CLI is not required.*

## Laufzustand und Feature-028-Grenze

Feature 028 bleibt auf dem lokalen Branch
`028-pre-wave5-wave6-conformance-closure` am Commit
`5550fbfe61dc97650304a69bd86358d76929fd00`. Seine 146 Tasks wurden nicht
ausgeführt. Der Branch enthält keinen nachträglich erfundenen
`autonomous-run-state.json`; diese Adoption verändert weder Branch noch
Feature-Artefakte.

Eine temporäre, nicht eingecheckte Fixture bildet den ehrlichen Zustand
`PausedByUser` nach den akzeptierten Tasks und vor der Implementierung ab. Bash
und PowerShell akzeptieren diesen Zustand. Eine widersprüchliche Fixture mit
`Interrupted` und gleichzeitig `lastOperation.state=Completed` wird von beiden
Validatoren verworfen. Der installierte Bash-Validator hat erwartungsgemäß
Modus `0644` und wird deshalb portabel mit `bash <script>` aufgerufen.

Der nächste fachliche Schritt bleibt eine ausdrückliche Benutzerfreigabe für
Feature 028. Danach muss `$speckit-autonomous-resume` zuerst Branch,
Feature-Metadaten, akzeptierte Artefakte, Tasks, Evidence, Governance, lokalen
Besitz und aktuelle Remote-Berechtigung abgleichen. Der gespeicherte
Delivery-Modus wäre nur historische Evidence und keine neue Berechtigung.

*Feature 028 remains on local branch
`028-pre-wave5-wave6-conformance-closure` at commit
`5550fbfe61dc97650304a69bd86358d76929fd00`. None of its 146 tasks has been
executed. No synthetic run-state file is added retroactively, and this adoption
changes neither that branch nor its feature artifacts.*

*An untracked temporary fixture represents the honest `PausedByUser` boundary
after accepted tasks and before implementation. Bash and PowerShell accept it;
both reject an `Interrupted` state that simultaneously claims a completed last
operation. Resuming Feature 028 still requires explicit user release followed
by the full `$speckit-autonomous-resume` audit.*

## Lokale Validierung

| Befehl oder Prüfung | Ergebnis |
|---|---|
| `specify check` | PASS; Antigravity, Claude, Codex, Junie, OpenCode und VS Code erkannt |
| `specify preset list` | PASS; sieben Presets in Prioritäten 10 bis 70 |
| `specify preset info autonomous-run-governance` | PASS; v0.2.0 und 18 Beiträge |
| `specify preset resolve autonomous-run-state-template` | PASS; oberste Schicht v0.2.0 |
| Tag-ZIP-Prüfsumme und Payload-Diff | PASS |
| Fünf Commands je gepflegter Agent-Oberfläche | PASS |
| Bash-Validator mit gültigem `PausedByUser` | PASS, 0/146 Tasks |
| PowerShell-Validator mit gültigem `PausedByUser` | PASS, 0/146 Tasks |
| Widersprüchlicher `Interrupted`-Zustand | Erwartet verworfen; Bash Exit 2, PowerShell Exit 1 |
| Projektgebundener Codex-Skill | PASS mit `quick_validate.py` und isoliertem PyYAML |
| Generierte Skill-/YAML-Struktur | PASS; 10 Skills, 5 Codex-UI-Metadaten und Preset-Manifest |
| Bash-/PowerShell-Parser | PASS für beide Gate- und beide Laufzustandsvalidatoren |
| `mandoc -T lint` | Nur STYLE: die veröffentlichte `.TH`-Zeile hat 81 Bytes; kein lokaler Paketdrift zur Behebung |
| Agent-Guidance-Parität | PASS; deutscher und englischer v0.2.0-Block jeweils 5/5 identisch |
| `git diff --check` und Staging-Kandidat | PASS einschließlich neuer Dateien |
| `dotnet format --verify-no-changes --no-restore` | PASS |
| Secret-Scan | PASS; Gitleaks ohne Fund, `high=0`; lokale Agent-Konfiguration bleibt ein bekannter Medium-Hinweis |
| Homogeneity-Wrapper | Erwartet fail-closed, weil repository-lokal `scripts/lib/hg-*` fehlt; gezielte Agent-Parität ist maßgeblich |
| `docfx docfx.json` | PASS; 0 Warnungen, 0 Fehler |
| `npm run test:docfx` | PASS; DocFX erneut 0/0, Playwright/Axe 2/2 |
| UTF-8-`lynx` | PASS für Runbook, Projektstatistik und diese Adoption-Evidence |

## Trigger und Grenzen

- Keine C#-Runtime, öffentliche API, Abhängigkeit, Paketversion, Beispiel-
  Portierung oder historische Quelle wird geändert.
- Keine .NET-Build-, Test- oder Coverage-Ausführung wird durch die reine
  Preset-, Guidance- und Dokumentationsadoption ausgelöst.
- Feature 028, Wave 5 und Wave 6 bleiben gesperrt.
- Ein realer Resume-Feldnachweis wird erst nach ausdrücklicher Freigabe von
  Feature 028 möglich. Bis dahin wird keine Preset-Catalog-Aktualisierung mit
  einem noch nicht erbrachten Resume-Claim veröffentlicht.

*No C# runtime, public API, dependency, package version, example port, or
historical source changes. The documentation and governance-only adoption does
not trigger .NET build, test, or coverage execution. Feature 028, Wave 5, and
Wave 6 remain blocked. A real resume field proof remains deferred until the
user explicitly releases Feature 028.*
