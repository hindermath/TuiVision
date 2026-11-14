---
name: speckit-secure-development-status
description: Secure-Development-Evidence vollständig und strikt read-only prüfen
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:secure-development-assurance-governance
---

# Speckit Secure Development Status Skill

# Secure Development Status

## Routing / Routing

Im bestehenden Model-Routing gilt `fast-mechanical`: vorhandene Ergebnisse
prüfen und verständlich berichten. Der Vertrag steht in `model-routing.json`.
Er richtet keine Modelle ein und wechselt keinen laufenden Chat um.
Der direkte Validator ist eine modellfreie Skriptprüfung. Fehlende Verträge
oder unpassende lokale Profile dürfen nicht durch Raten umgangen werden.

*Stable routing uses fast-mechanical to check and explain existing results.
The model-routing.json contract neither configures models nor switches a live
chat. The direct validator is model-free. Never guess around missing contracts
or unsuitable local profiles.*

Syntax:

~~~text
$speckit-secure-development-status [<evidence-dir>]
~~~

Prüfe das ausdrücklich angegebene Evidence-Verzeichnis. Fehlt der Parameter,
verwende das lexikografisch neueste Verzeichnis unter
`docs/security/secure-development/`.

Führe vom Projektwurzelverzeichnis auf Windows
`pwsh -NoProfile -File .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.ps1`
mit `-Action Status` und gegebenenfalls `-EvidenceDirectory <evidence-dir>` aus.
Führe auf macOS/Linux
`bash .specify/presets/secure-development-assurance-governance/scripts/validate-secure-development-assurance.sh status`
mit dem optionalen Evidence-Verzeichnis als letztem Argument aus.

Die vollständigen installierten Pfade sind verbindlich. Das Top-Level-
Skriptverzeichnis wird bei der Agentengenerierung umgeschrieben und ist kein
Preset-Pfad.

Der Befehl ist strikt read-only. Er darf keine Evidence, Richtlinie,
Checkliste, Baseline, Freigabe, Git- oder Remote-Zustände verändern.

Berichte textorientiert und in stabiler Reihenfolge:

1. den ausgewählten Kontext;
2. Baseline-, Delta-, Closure- und Image-Impact-Ergebnis;
3. das strengste Gesamtergebnis;
4. `technicalValidation`, `pilotAuthorization`, `projectAcceptance` und
   `generalRelease` getrennt;
5. die exakt dokumentierte nächste Aktion.

Blockiere bei fehlenden Quellen, Drift, ungültigen Statuskombinationen,
abgelaufenen Reviews, unvollständigen Risiken, fehlender
`security-governance`-Voraussetzung oder unzulässigen
Zertifizierungsbehauptungen. Erfolgreiche technische Validierung darf niemals
als menschliche Freigabe ausgegeben werden.

## English

Run the explicit installed validator path from the project root using Bash
or PowerShell. Do not shorten it to a top-level scripts directory: command
generation rewrites that directory to a different location.
Inspect the selected evidence directory without changing it. Validate the
complete baseline binding and all four gates. Report every gate, the worst
overall outcome, all four human decision boundaries, and the exact recorded
next action. Never infer an approval or certification from technical success.

## Nutzungshilfe / Reader help

DE: Projektgeführte Sicherheitsnachweise auf Bindung und Konsistenz prüfen; technische Ergebnisse und menschliche Entscheidungen trennen. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/main/docs/user-guide.md).

EN: Check binding and consistency of project-owned security evidence; separate technical results from human decisions. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
