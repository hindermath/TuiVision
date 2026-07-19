# Nativer Multi-Agent-Smoke / Native Multi-Agent Smoke

Datum / Date: `2026-07-19`
Plattform / Platform: macOS `26.5.2`
Ausfuehrung / Execution: isolierte lokale Git-Repositories ohne Remote

## Ziel und Grenze / Goal and Boundary

Der Smoke pruefte, ob die in Version `0.2.0` vorgesehenen Agentenfamilien einen
kleinen, nicht interaktiven Schreibauftrag in einem isolierten Worker-
Repository ausfuehren koennen. Jeder erfolgreiche Runner sollte genau die
Datei `agent-smoke-result.md` erzeugen. Kein Runner durfte committen oder ein
Git-Remote verwenden.

*The smoke checked whether the agent families supported by version `0.2.0`
could perform a small non-interactive write task in an isolated worker
repository. Each successful runner had to create only
`agent-smoke-result.md`. No runner was allowed to commit or use a Git remote.*

## Ergebnisse / Results

| Runner | CLI-Version | Ergebnis / Result | Modellbehandlung / Model handling |
|---|---:|---|---|
| Codex | `0.144.6` | Bestanden / Passed | Agent-Standard, nicht deklariert |
| Claude Code | `2.1.197` | Bestanden / Passed | Agent-Standard, nicht deklariert |
| GitHub Copilot CLI | `1.0.16` | Bestanden / Passed | Agent-Standard, nicht deklariert |
| Google Antigravity | `1.1.4` | Bestanden / Passed | Agent-Standard, nicht deklariert |
| OpenCode | `1.17.15` | Bestanden / Passed | lokaler Provider-Fallback nur fuer den Smoke |
| Junie | `888.46` | Nicht blockierend: keine nicht interaktive Authentifizierung / Non-blocking: no non-interactive authentication | nicht gestartet / not started |

Alle fuenf verpflichtenden Real-Smokes erzeugten das erwartete Artefakt. Die
Arbeitsbaeume enthielten danach neben der unveraenderten Baseline nur diese
unversionierte Datei. OpenCodes voreingestellter lokaler Provider antwortete
mit einem technischen Fehler; der einmalige Lauf ueber ein bereits
authentifiziertes alternatives lokales Providerprofil bestand. Diese Auswahl
ist Evidenz des Testsystems und keine Vorgabe des Presets.

*All five required real-agent smokes produced the expected artifact. After the
run, each worktree contained only that untracked file in addition to the
unchanged baseline. OpenCode's configured local default provider returned a
technical error; the single retry through an already authenticated alternative
local provider profile passed. That choice is test-system evidence, not a
preset requirement.*

## Berechtigungsbefunde / Permission Findings

Antigravity verweigerte im ersten Headless-Versuch die Schreibaktion, weil die
lokale `write_file`-Freigabe fehlte. Der Wiederholungslauf erhielt im
isolierten Smoke-Sandboxkontext eine ausdrueckliche Werkzeugfreigabe und
bestand. Produktive Runner-Profile sollen stattdessen eine dauerhaft enge
lokale Freigaberegel fuer das jeweilige Worker-Worktree verwenden.

Junie wurde als zusaetzliches Runner-Profil validiert, konnte ohne vorhandene
nicht interaktive Authentifizierung aber keinen echten Agentenlauf starten.
Dieser Befund ist gemaess Testplan nicht blockierend; das Profil bleibt durch
Schema-, Fixture- und Argument-Array-Tests abgedeckt.

*Antigravity denied the first headless write because no local `write_file`
allow rule was present. The retry received explicit tool permission inside the
isolated smoke sandbox and passed. Production runner profiles should instead
use a persistently narrow local allow rule scoped to the assigned worktree.*

*Junie was validated as an additional runner profile but could not start a real
agent run without existing non-interactive authentication. As defined by the
test plan, this finding is non-blocking; schema, fixture, and argument-array
tests continue to cover the profile.*

## Schlussfolgerung / Conclusion

Das Schema `1.1` kann verschiedene Agentenfamilien darstellen, ohne
anbieterbezogene Modelle oder Secrets in das Preset zu schreiben. Die
Ausfuehrbarkeit haengt weiterhin von lokal gepruefter Authentifizierung,
Berechtigung und Providerverfuegbarkeit ab.

*Schema `1.1` can represent different agent families without writing
provider-specific models or secrets into the preset. Execution still depends
on locally reviewed authentication, permissions, and provider availability.*
