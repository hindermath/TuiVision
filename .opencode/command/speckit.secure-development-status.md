---
description: Secure-Development-Evidence vollständig und strikt read-only prüfen
---


<!-- Source: secure-development-assurance-governance -->
# Secure Development Status

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