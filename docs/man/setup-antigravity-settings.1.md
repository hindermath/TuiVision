# setup-antigravity-settings(1)

## NAME

`setup-antigravity-settings.sh`, `setup-antigravity-settings.ps1` - gehaertete Antigravity-CLI-Einstellungen verwalten. *Manage hardened Antigravity CLI settings.*

## SYNOPSIS

```text
bash scripts/setup-antigravity-settings.sh [--dry-run] [--check-only]
pwsh -NoProfile -File scripts/setup-antigravity-settings.ps1 [-WhatIf] [-CheckOnly]
```

## DESCRIPTION

Die Skripte pflegen `~/.gemini/antigravity-cli/settings.json` strukturiert und idempotent. Sie setzen strikte Werkzeugfreigaben, verpflichtende Artefaktpruefung, Terminal-Sandbox, Workspace-Isolation und deaktivierte Telemetrie. Unbekannte JSON-Schluessel bleiben erhalten. Eine ASCII-only-Statuszeile zeigt nur technische Arbeitsdaten.

*The scripts maintain `~/.gemini/antigravity-cli/settings.json` structurally and idempotently. They enable strict tool approval, mandatory artifact review, terminal sandboxing, workspace isolation, and disabled telemetry. Unknown JSON keys are preserved. An ASCII-only status line shows technical workspace data only.*

## SECURITY

Ungueltiges JSON wird nicht ersetzt. Identitaets-, Tarif- und Sitzungsdaten werden nicht in der Statuszeile ausgegeben. Die Baseline verwendet keine Berechtigungs-Bypass-Optionen.

*Invalid JSON is never replaced. Identity, subscription, and session data are excluded from the status line. The baseline does not use permission bypass options.*
