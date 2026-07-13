# Evidence: Adoption von `autonomous-run-governance` v0.1.2

## Zweck und Grenze

Dieses Dokument weist die Aktualisierung des optionalen Presets von v0.1.0 auf
v0.1.2 vor Feature 026 nach. Die Änderung übernimmt zwei portable Erkenntnisse
aus Feature 025: die Prüfung des exakt beabsichtigten Delivery-Kandidaten und
die Bindung jedes Acceptance-Gates an den tatsächlich ausgeführten Nachweis.
Runtime, öffentliche API, Abhängigkeiten, Beispiele und `tv203s/` bleiben
unverändert.

*This document records the optional preset update from v0.1.0 to v0.1.2 before
Feature 026. The change adopts two portable Feature 025 findings: validation of
the exact intended delivery candidate and binding every acceptance gate to its
actually executed proof. Runtime, public API, dependencies, examples, and
`tv203s/` remain unchanged.*

Die ursprüngliche v0.1.0-Adoption bleibt in
`specs/023-a11y-framework/preset-adoption-evidence.md` historisch unverändert.

*The original v0.1.0 adoption remains historically unchanged in
`specs/023-a11y-framework/preset-adoption-evidence.md`.*

## Veröffentlichte Quelle

| Feld | Nachweis |
|---|---|
| Home-Baseline-Paket | `hindermath/home-baseline#61`, Merge `9ffe1d11707653c05cc3c2ad07b0d2b84fb5cdbf` |
| Öffentliches Preset | `hindermath/spec-kit-preset-autonomous-run-governance#2`, Merge `c878d138fecc698f0f958c210d4579b769f8fd57` |
| Release | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.1.2> |
| Tag-ZIP | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.1.2.zip> |
| SHA-256 | `6e4012895edf0fc9f8e4bd7795f02397190a28411034f9211cfaba5df899e2d0` |
| Spec Kit | lokal `0.12.11`, Preset-Anforderung `>=0.8.3` |
| Priorität | `70` nach der unveränderten Sechsermatrix |

## Installations- und Paritätsnachweis

`specify preset remove autonomous-run-governance` entfernte v0.1.0. Danach
installierte `specify preset add --from <v0.1.2-tag-zip> --priority 70` die
veröffentlichte Version. Die bekannte OpenCode-Legacy-Warnung
`.opencode/command` blieb rein informativ.

*The existing v0.1.0 registration was removed before the published v0.1.2 tag
ZIP was installed at priority 70. The known `.opencode/command` legacy warning
remained informational.*

| Oberfläche | `autonomous` | `autonomous-retrospective` | Ergebnis |
|---|---:|---:|---|
| Codex und Antigravity `.agents/skills/` | 1 | 1 | gemeinsamer eindeutiger Pfad; lokaler Codex-Override erhalten |
| Claude `.claude/skills/` | 1 | 1 | v0.1.2 erzeugt |
| Copilot `.github/agents/` | 1 | 1 | v0.1.2 erzeugt |
| Copilot `.github/prompts/` | 1 | 1 | bestehender Legacy-Prompt-Pfad eindeutig |
| OpenCode `.opencode/command/` | 1 | 1 | v0.1.2 erzeugt; Legacy-Pfad dokumentiert |

Der lokale Codex-Override blieb bytegleich: `SKILL.md` hat SHA-256
`370b6f184a4ccd86a8675bc07faad516148326134c42353d86c2ef439c03f15a`,
`agents/openai.yaml` hat
`e6e65ee0586b82a4fbbc30bddc8d32b3866a98405d216e6419a3462ba84acff0`.
Er enthält beide neuen portablen Regeln und zusätzlich die unveränderten
TuiVision-Verträge.

*The project-owned Codex override remained byte-identical. It contains both new
portable rules plus the unchanged TuiVision contracts.*

## Portable Regeln

1. Vor einem autorisierten Commit werden nur die beabsichtigten Pfade gestagt,
   mit `git diff --cached --check` geprüft und mit dem Repositorystatus
   abgeglichen. `LocalImplementation` verwendet einen gleichwertigen
   nicht-mutierenden beziehungsweise temporären Indexpfad.
2. Vor einem Merge wird jedes Acceptance-Gate dem Workflow, Job, Runner oder
   der Plattform und dem tatsächlich ausgeführten Befehl zugeordnet. Grüne
   Sammelzustände, Plattformnamen und Bypass liefern keinen fehlenden
   technischen Nachweis.

*Before an authorized commit, the exact intended staged candidate is validated
and reconciled with repository state. Before merge, each acceptance gate maps
to the workflow, job, runner or platform, and command that actually executed
it. Green aggregate names and bypass cannot supply missing technical proof.*

## Bereits bestandene Prüfungen

| Prüfung | Ergebnis |
|---|---|
| `specify check` | Pass; Spec Kit bereit |
| `preset list` und `preset info` | Pass; sieben Presets, v0.1.2 aktiv bei Priorität 70 |
| Constitution-/Spec-/Plan-/Tasks-Resolve | Pass; v0.1.2 je Kette genau einmal |
| Öffentlicher Payload-Vergleich | Pass; installierter Inhalt entspricht dem gemergten öffentlichen Repository |
| Tag-ZIP-Smoke | Pass; SHA-256 geprüft und temporäre Installation erfolgreich |
| Agent-/Skill-Eindeutigkeit | Pass; beide Commands je gepflegter Oberfläche genau einmal |
| Codex-Override | Pass; beide Dateien bytegleich erhalten |
| Antigravity | Pass; `agy` 1.1.1 aktiv, direkter Gemini-CLI-Befehl nicht installiert |
| Standard-Presets | Pass; Versionen 0.6.0/0.5.0/0.2.0/0.4.0/0.2.0/0.3.0 unverändert |
| Lokaler Skill | Pass; `quick_validate.py` mit der Spec-Kit-Python-Umgebung |
| Generierte Skills | Pass; YAML-/Frontmatter-, Regel- und Eindeutigkeitsprüfung |
| Agent-Parität | Pass; die v0.1.2-Regel ist auf allen fünf Guidance-Flächen bytegleich |
| `git diff --check` | Pass |
| Secret-Scan | Pass; Gitleaks ohne Fund, keine High-Risiken |
| DocFX | Pass; zweimal 0 Warnungen und 0 Fehler |
| Playwright und axe | Pass; 2/2 Tests |
| Textorientierter Review | Pass; Startseite, Projektstatistik und autonomes Runbook mit UTF-8-`lynx` lesbar |
| Scope-Diff | Pass; keine Runtime-, API-, Paket-, Beispiel-, Projekt- oder `tv203s/`-Änderung |

Der erste Aufruf von `quick_validate.py` über das systemweite Python stoppte
vor der Skill-Prüfung, weil dort PyYAML fehlt. Die Wiederholung mit der
Spec-Kit-Python-Umgebung bestand für den lokalen Override. Der generische
Validator lehnt bei Spec-Kit-generierten Skills weiterhin das von Spec Kit
selbst erzeugte Frontmatter-Feld `compatibility` ab; deshalb wurden diese Skills
über YAML-Parsing, Pflichtfelder, Regelinhalt, Installation und Eindeutigkeit
geprüft.

*The first `quick_validate.py` invocation stopped before skill validation because
the system Python lacks PyYAML. Repeating it with the Spec Kit Python environment
passed for the local override. The generic validator still rejects Spec Kit's
own generated `compatibility` frontmatter field, so generated skills were
validated through YAML parsing, required fields, rule content, installation,
and uniqueness.*

## Trigger und Restrisiko

Da Agent-Guidance, ein Repository-Template und diese Evidence geändert werden,
laufen der normale DocFX-, A11Y- und textorientierte Nachweispfad. .NET-Build,
Tests und Coverage werden nicht ausgelöst, weil keine ausführbare Datei,
Projektdatei oder Testlogik geändert wird.

*Agent guidance, one repository template, and this evidence trigger the normal
DocFX, A11Y, and text-oriented proof path. .NET build, tests, and coverage are
not triggered because no executable file, project file, or test logic changes.*

Restrisiko bleibt die bewusst lokale Codex-Erweiterung. Sie ist auf einen
eindeutigen Pfad begrenzt und wird in Feature 026 als erster realer Feldlauf der
neuen Preset-Version erneut geprüft.

*The intentional project-specific Codex extension remains the residual risk. It
is limited to one path and will be revalidated in Feature 026 as the first real
field run of the new preset version.*
