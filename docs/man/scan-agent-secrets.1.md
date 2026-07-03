# scan-agent-secrets(1)

## Name

`scan-agent-secrets` - prueft Repositories und Agenten-Dateien auf moegliche Secrets

*checks repositories and agent files for possible secrets*

## Synopsis

```bash
bash scripts/scan-agent-secrets.sh [--fail-on-high] [WORKSPACE_ROOT]
```

```powershell
pwsh -NoProfile -File scripts/scan-agent-secrets.ps1 [-FailOnHigh] [-WorkspaceRoot PATH]
```

## Beschreibung / Description

Das Werkzeug ist die manuelle Secret-Scan-Schicht der Workspace-Baseline. Wenn
`gitleaks` installiert ist, prueft es zuerst den aktuellen Git-Diff mit
`gitleaks git --pre-commit --redact`. Danach bleiben die bestehenden
Regex-Pruefungen aktiv. Der Bash-Scanner bewertet zusaetzlich bekannte
KI-Agenten-Verzeichnisse, waehrend die PowerShell-Variante git-getrackte Dateien
prueft.

*This tool is the manual secret scanning layer of the workspace baseline. When
`gitleaks` is installed, it first checks the current git diff with
`gitleaks git --pre-commit --redact`. The existing regex checks remain active
afterwards. The Bash scanner additionally evaluates known AI agent directories,
while the PowerShell variant checks git-tracked files.*

Der gemeinsame `pre-push`-Hook nutzt `gitleaks` separat fuer die zu pushenden
Commit-Ranges. Fehlt `gitleaks`, blockiert das allein nicht; der Regex-Fallback
bleibt fuer die geaenderten getrackten Dateien aus diesen Push-Ranges aktiv.

*The shared `pre-push` hook uses `gitleaks` separately for the commit ranges
being pushed. Missing `gitleaks` does not block by itself; the regex fallback
remains active for the changed tracked files from those push ranges.*

## Optionen / Options

| Bash | PowerShell | Bedeutung / Meaning |
|---|---|---|
| `--fail-on-high` | `-FailOnHigh` | Exit-Code 2 bei HIGH-Befund / exit 2 on HIGH finding |
| `WORKSPACE_ROOT` | `-WorkspaceRoot PATH` | Zu pruefendes Repository / repository to scan |
| `--help` | `Get-Help` | Hilfe anzeigen / show help |

## Beispiele / Examples

```bash
bash scripts/scan-agent-secrets.sh --fail-on-high .
```

```powershell
pwsh -NoProfile -File scripts/scan-agent-secrets.ps1 -FailOnHigh
```

## Sicherheit / Security

`gitleaks` wird mit `--redact` ausgefuehrt, damit Befunde keine Roh-Secrets in
Terminalausgaben schreiben. Bei einem echten Treffer muss das Secret sofort
rotiert und die Ursache bereinigt werden.

*`gitleaks` runs with `--redact` so findings do not print raw secrets to the
terminal. On a real finding, rotate the secret immediately and fix the cause.*
