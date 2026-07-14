# Evidence: Adoption von `autonomous-run-governance` v0.1.4

## Zweck und Grenze

Dieses Dokument weist die Aktualisierung des optionalen Presets von v0.1.2 auf
v0.1.4 vor Feature 028 nach. Version 0.1.3 überführt zwei unabhängige Feldfunde
aus Feature 025 und 026 in maschinenlesbare Acceptance-Gates und exakte
HEAD-Evidence. Version 0.1.4 macht den Validatoraufruf nach einer ZIP- oder
Preset-Installation portabel. Runtime, öffentliche API, Abhängigkeiten,
Beispiele, Projektdateien und `tv203s/` bleiben unverändert.

*This document records the optional preset update from v0.1.2 to v0.1.4 before
Feature 028. Version 0.1.3 turns two independent Feature 025 and 026 field
findings into machine-readable acceptance gates and exact-HEAD evidence. Version
0.1.4 makes validator invocation portable after ZIP or preset installation.
Runtime, public API, dependencies, examples, project files, and `tv203s/`
remain unchanged.*

Die früheren Adoptionen bleiben in
`specs/023-a11y-framework/preset-adoption-evidence.md` und
`specs/025-core-runtime-conformance-hardening/preset-adoption-v0.1.2-evidence.md`
historisch unverändert.

*Earlier adoption records remain historically unchanged in the Feature 023 and
Feature 025 evidence files.*

## Veröffentlichte Quelle

| Feld | Nachweis |
|---|---|
| Gate-Evidence-Paket | `hindermath/home-baseline#62`, Merge `cb58d751ac477f95f9831a32feb4fd24a09387fb` |
| Gate-Evidence-Preset | `hindermath/spec-kit-preset-autonomous-run-governance#3`, Merge `5a6355c93e21341279eef51729f6a19ebff12d53` |
| Installer-Patch-Paket | `hindermath/home-baseline#63`, Merge `9322fad9ba689d516da2c1391f12db7ef1818652` |
| Installer-Patch-Preset | `hindermath/spec-kit-preset-autonomous-run-governance#4`, Merge `0ab22e3262ea0e44faf87408ae3a9c7366277e8b` |
| Release | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/releases/tag/v0.1.4> |
| Tag-ZIP | <https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.1.4.zip> |
| SHA-256 | `da667e2fd3fc5ccf0a29f7fd078d9f030f50ba267f659fc5b31bc000b59767e0` |
| Spec Kit | lokal `0.12.11`, Preset-Anforderung `>=0.8.3` |
| Priorität | `70` nach der unveränderten Sechsermatrix |

## Installations- und Paritätsnachweis

Der veröffentlichte v0.1.4-Tag-ZIP wurde mit
`specify preset add --from <v0.1.4-tag-zip> --priority 70` installiert. Die
bekannte OpenCode-Legacy-Warnung zu `.opencode/command` blieb rein informativ.
Der installierte Payload entspricht bytegleich dem öffentlichen `main`.

*The published v0.1.4 tag ZIP was installed at priority 70. The known
`.opencode/command` legacy warning remained informational. The installed payload
is byte-identical to public `main`.*

| Oberfläche | `autonomous` | `autonomous-retrospective` | Ergebnis |
|---|---:|---:|---|
| Codex und Antigravity `.agents/skills/` | 1 | 1 | gemeinsamer eindeutiger Pfad; lokaler Codex-Override gezielt aktualisiert |
| Claude `.claude/skills/` | 1 | 1 | v0.1.4 erzeugt |
| Copilot `.github/agents/` | 1 | 1 | v0.1.4 erzeugt |
| Copilot `.github/prompts/` | 1 | 1 | bestehender Legacy-Prompt-Pfad eindeutig |
| OpenCode `.opencode/command/` | 1 | 1 | v0.1.4 erzeugt; Legacy-Pfad dokumentiert |

Der projektgebundene Codex-Override wurde bewusst von SHA-256
`370b6f184a4ccd86a8675bc07faad516148326134c42353d86c2ef439c03f15a`
auf `8e17466b6dc8a6bd314e4dab054da88fa28542117c8f8301a77cba336847b037`
aktualisiert. Seine OpenAI-UI-Metadaten blieben bytegleich bei
`e6e65ee0586b82a4fbbc30bddc8d32b3866a98405d216e6419a3462ba84acff0`.

*The project-owned Codex override changed intentionally to adopt the new gate
contract and explicit interpreter invocation. Its OpenAI UI metadata remained
byte-identical.*

## Portable Regeln

1. Vor der Implementierung deklariert ein geprüftes JSON-Artefakt jedes
   Acceptance-Gate mit stabiler ID, Scope, Command-Tokens und optionalen
   Runner-/Plattform-Tokens. `N/A` benötigt Begründung und Neubewertungstrigger.
2. Vor dem Merge bindet eine temporäre Provider-Evidence jedes Gate an den
   exakten geprüften HEAD und genau einen `Primary`-Nachweis. Commands und Runner
   stammen aus Workflow-Definitionen oder Job-Logs, nicht aus grünen Namen.
3. Der installierte Validator wird über `bash <validator.sh>` oder
   `pwsh -NoProfile -File <validator.ps1>` aufgerufen. ZIP-Extraktion und Preset-
   Installation müssen das Git-Ausführungsbit nicht erhalten.
4. Der Validator ist read-only. Ein Pass erteilt keine Commit-, Push-, PR-,
   Merge-, Bypass- oder Provider-Berechtigung.

*Before implementation, a reviewed JSON artifact declares every acceptance
gate. Before merge, temporary provider evidence binds each gate to the exact
reviewed HEAD and exactly one Primary proof derived from workflow definitions or
job logs. Installed scripts run through an explicit Bash or PowerShell
interpreter because executable mode bits are not portable. Validator success is
read-only and grants no remote authority.*

## Validierung

| Prüfung | Ergebnis |
|---|---|
| `specify check` | Pass; Spec Kit und Antigravity verfügbar, direkte Gemini CLI nicht installiert |
| `preset list` und `preset info` | Pass; sieben Presets, v0.1.4 aktiv bei Priorität 70 und 14 Beiträgen |
| Constitution-/Spec-/Plan-/Tasks-Resolve | Pass; v0.1.4 je Kette genau einmal |
| JSON-Template-Resolve | Pass; Requirements und Evidence lösen eindeutig auf v0.1.4 auf |
| Öffentlicher Payload-Vergleich | Pass; installierter Inhalt entspricht dem gemergten öffentlichen Repository |
| GitHub-Tag-ZIP | Pass; SHA-256 geprüft und frische Installation erfolgreich |
| Installierter Bash-Modus | Erwartete Grenze; Tag-ZIP installiert das Skript als nicht ausführbares `0644` |
| Bash-Validator | Pass; expliziter `bash`-Aufruf akzeptiert zwei Gates und zwei Primary-Zeilen |
| PowerShell-Validator | Pass; äquivalente Fixture über `pwsh -NoProfile -File` |
| Agent-/Skill-Eindeutigkeit | Pass; beide Commands je gepflegter Oberfläche genau einmal |
| Codex-Override | Pass; Skill absichtlich aktualisiert, UI-Metadaten bytegleich erhalten |
| Antigravity | Pass; `agy` 1.1.1 aktiv, direkter Gemini-CLI-Befehl nicht installiert |
| Standard-Presets | Pass; Versionen 0.6.0/0.5.0/0.2.0/0.4.0/0.2.0/0.3.0 unverändert |
| Lokaler Skill | Pass; `quick_validate.py` mit der Spec-Kit-Python-Umgebung |
| Agent-Parität | Pass; gemeinsame v0.1.4-Regel auf fünf Guidance-Flächen bytegleich |
| `git diff --check` | Pass |
| `dotnet format --verify-no-changes --no-restore` | Pass |
| Secret-Scan | Pass; Gitleaks ohne Fund, keine High-Risiken |
| DocFX | Pass; viermal 0 Warnungen und 0 Fehler |
| Playwright und axe | Pass; 2/2 Tests |
| Textorientierter Review | Pass; Startseite und Projektstatistik mit UTF-8-`lynx` lesbar |
| Scope-Diff | Pass; keine Runtime-, API-, Abhängigkeits-, Beispiel-, Projekt- oder `tv203s/`-Änderung |

## Trigger und Restrisiko

Agent-Guidance, Repository-Template, Evidence und Projektstatistik lösen den
normalen DocFX-, A11Y- und textorientierten Nachweispfad aus. .NET-Build, Tests
und Coverage werden nicht ausgelöst, weil keine ausführbare Produktdatei,
Projektdatei, Abhängigkeit oder Testlogik geändert wird.

*Agent guidance, a repository template, evidence, and project statistics trigger
the normal DocFX, A11Y, and text-oriented proof path. .NET build, tests, and
coverage are not triggered because no executable product file, project file,
dependency, or test logic changes.*

Restrisiko bleibt der bewusst projektgebundene Codex-Override. Er liegt an genau
einem Pfad, besitzt eigene UI-Metadaten und wird im nächsten autonomen Lauf 028
zusammen mit dem portablen v0.1.4-Payload erneut geprüft.

*The intentional project-owned Codex override remains the residual risk. It has
one path, preserves its UI metadata, and will be revalidated with the portable
v0.1.4 payload during the next autonomous Feature 028 run.*
