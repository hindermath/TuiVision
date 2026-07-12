# Evidence: Adoption von `autonomous-run-governance` v0.1.0

## Zweck und Grenze

Dieses Dokument weist die öffentliche Tag-ZIP-Adoption des optionalen Presets
`autonomous-run-governance` v0.1.0 in TuiVision nach. Die Adoption ergänzt die
bisherigen sechs Governance-Presets mit Priorität 70. Sie ändert kein
Runtime-Verhalten, keine öffentliche API, keine Abhängigkeit, kein Beispiel und
keine historische Quelle unter `tv203s/`.

This document records the public tag-ZIP adoption of the optional
`autonomous-run-governance` v0.1.0 preset in TuiVision. The adoption adds
priority 70 after the existing six governance presets. It changes no runtime
behavior, public API, dependency, example, or historical source under
`tv203s/`.

## Veröffentlichte Quelle

| Feld | Nachweis |
|---|---|
| Repository | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance> |
| Release | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.1.0> |
| Tag-ZIP | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.1.0.zip> |
| SHA-256 | `ebece5bbc39a4e6ccb14f6cb933f55e4f8c20bdd7baeacca30a25fa292e78e36` |
| Spec-Kit-Mindestversion | `>=0.8.3` |
| Upstream-Koordination | <https://github.com/github/spec-kit/issues/3479> |

Das ZIP wurde zweimal unabhängig geladen; beide Prüfsummen waren identisch.
Ein frisches temporäres Projekt installierte daraus Version 0.1.0 mit zwölf
Beiträgen und erzeugte beide Codex-Skills.

The ZIP was downloaded independently twice and both checksums matched. A fresh
temporary project installed version 0.1.0 with twelve contributions and
generated both Codex skills.

## Installationsnachweis

```text
specify preset add --from https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.1.0.zip --priority 70
```

Ergebnis: erfolgreich installiert. Spec Kit meldete nur den bereits bekannten
Legacy-Pfad `.opencode/command`; diese Adoption migriert keine bestehende
Agent-Integration. Die Registry führt das Preset aktiviert als Version 0.1.0
mit Priorität 70 und den Commands `speckit.autonomous` sowie
`speckit.autonomous-retrospective`.

Result: installed successfully. Spec Kit only reported the known legacy
`.opencode/command` path; this adoption does not migrate an existing agent
integration. The registry lists the enabled preset as version 0.1.0 at priority
70 with `speckit.autonomous` and `speckit.autonomous-retrospective`.

## Gestapelte Preset-Matrix

| Priorität | Preset | Version | Status |
|---:|---|---:|---|
| 10 | `security-governance` | 0.6.0 | Aktiviert |
| 20 | `architecture-governance` | 0.5.0 | Aktiviert |
| 30 | `isaqb-architecture-governance` | 0.2.0 | Aktiviert |
| 40 | `a11y-governance` | 0.4.0 | Aktiviert |
| 50 | `cross-platform-governance` | 0.2.0 | Aktiviert |
| 60 | `agent-parity-governance` | 0.3.0 | Aktiviert |
| 70 | `autonomous-run-governance` | 0.1.0 | Aktiviert |

## Agent-Oberflächen und lokaler Override

| Oberfläche | `autonomous` | `autonomous-retrospective` | Entscheidung |
|---|---:|---:|---|
| Codex `.agents/skills/` | 1 | 1 | Projekt-Skill bleibt lokaler Override; Retrospektive kommt aus dem Preset |
| Claude `.claude/skills/` | 1 | 1 | Beide Preset-Skills erzeugt |
| Copilot `.github/agents/` | 1 | 1 | Beide Agent-Dateien erzeugt |
| Copilot `.github/prompts/` | 1 | 1 | Beide Legacy-Prompt-Dateien innerhalb ihrer Oberfläche eindeutig |
| OpenCode `.opencode/command/` | 1 | 1 | Beide Commands erzeugt; Legacy-Pfad-Warnung dokumentiert |
| Gemini | 1 | 1 | In isolierter Paketvalidierung erzeugt; keine aktive TuiVision-Integration |

Der vorhandene Codex-Skill `$speckit-autonomous` bleibt genau an seinem
bisherigen Pfad. Sein Preset-Gegenstück ersetzt ihn noch nicht vollständig:
TuiVision benötigt zusätzlich nummerierte Branches, den manuellen Build-Zähler,
DocFX-/A11Y-Trigger und die historische Source-Policy. Der lokale Skill behält
deshalb seinen ursprünglichen Inhalt; ein zweiter gleichnamiger Skill wird nicht
angelegt. Runbook und projektspezifische Evidence bleiben ebenfalls bestehen.

The existing Codex `$speckit-autonomous` skill remains at exactly its previous
path. Its preset counterpart does not yet replace it fully because TuiVision
also needs numbered branches, the manual build counter, DocFX/A11Y triggers,
and the historical-source policy. The local skill therefore keeps its original
content and no second skill with the same name is added. The runbook and
project-specific evidence also remain in place.

## Validierung

| Prüfung | Erwartung | Ergebnis |
|---|---|---|
| `specify check` | Installation und Projektzustand gültig | Bestanden; Spec Kit 0.12.11 bereit |
| Preset `list`, `info`, `resolve` | Sieben aktivierte Presets in Priorität 10 bis 70 | Bestanden; Spec-, Plan- und Tasks-Kette enthalten alle sieben Layer |
| Öffentlicher Payload-Vergleich | Installierter Preset-Inhalt entspricht dem Tag-ZIP | Bestanden; SHA-256 stimmt und `diff -ru` meldet keine Abweichung |
| Skill- und Command-Eindeutigkeit | Beide Commands je unterstützter Oberfläche genau einmal | Bestanden; Codex/AGY teilen den eindeutigen Skill-Pfad, Claude, Copilot und OpenCode sind je Oberfläche eindeutig |
| Codex-Override-Integrität | Projekt-Skill entspricht dem Stand vor der Installation | Bestanden; `SKILL.md` bleibt `919ebb6c...d557`, `openai.yaml` bleibt `e6e65ee0...acff0` |
| Agent-Parität | Gemeinsame Adoption-Regel auf fünf Agent-Flächen synchron | Bestanden; deutsche und englische Regel erscheinen je Datei genau einmal |
| Template-Auflösung | Autonomie-Addenda sind in den aufgelösten Templates sichtbar | Bestanden für `spec-template`, `plan-template` und `tasks-template` |
| `git diff --check` | Keine Whitespace-Fehler | Bestanden |
| Skill-Validierung | Codex- und Claude-Skills strukturell gültig | Bestanden: lokaler Codex-Skill über `quick_validate.py`; Preset-Skills über Installation, Frontmatter- und Eindeutigkeitsprüfung. Der generische Validator lehnt das von Spec Kit selbst erzeugte `compatibility`-Feld ebenso wie bei bestehenden Spec-Kit-Skills ab. |
| Secret-Scan | Keine hochriskanten Funde | Bestanden; Gitleaks-Diff und Scan der Git-getrackten Dateien ohne Fund |
| DocFX | Keine Warnungen oder Fehler | Bestanden; zweimal 0 Warnungen und 0 Fehler |
| Playwright plus axe | Zwei von zwei A11Y-Smokes erfolgreich | Bestanden; 2/2 |
| Textorientierter Spot-Check | Drei repräsentative Seiten mit `lynx` lesbar | Bestanden für Startseite, Projektstatistik und autonomes Runbook |
| Scope-Diff | Keine Runtime-, API-, Paket-, Beispiel- oder `tv203s/`-Änderung | Bestanden; finaler Staging-Whitelist-Test ohne unerlaubten Pfad |

`dotnet build`, `dotnet test` und Coverage werden nicht ausgelöst, weil diese
Adoption keinen ausführbaren Code, keine Testlogik und keine Projektdatei
ändert. DocFX und der zugehörige A11Y-Pfad werden ausgelöst, weil gepflegte
Agent-Guidance, Template- und Evidence-Dokumente geändert werden.

`dotnet build`, `dotnet test`, and coverage are not triggered because this
adoption changes no executable code, test logic, or project file. DocFX and its
matching A11Y path are triggered because maintained agent guidance, template,
and evidence documents change.

## Restrisiko und Folgegrenze

Die einzige bewusste Abweichung vom vollständig generierten Zustand ist der
projektspezifische Codex-Override. Er ist sichtbar dokumentiert und bleibt auf
einen Pfad begrenzt. Eine spätere Preset-Version darf ihn erst entfernen, wenn
die vier TuiVision-Verträge portabel abgebildet und mit einem erneuten
Paritätsnachweis bestätigt sind. Das Warten auf Rückmeldung oder Aufnahme durch
`github/spec-kit` blockiert diese lokale Adoption nicht.

The only intentional deviation from a fully generated state is the
project-specific Codex override. It is documented visibly and remains limited
to one path. A later preset version may remove it only after the four TuiVision
contracts are represented portably and confirmed by a repeated parity proof.
Waiting for feedback or catalog inclusion from `github/spec-kit` does not block
this local adoption.
