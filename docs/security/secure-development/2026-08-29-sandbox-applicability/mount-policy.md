# Mount- und Schreibgrenzen / Mount and Write Boundaries

## Grundregel / Core Rule

Nur der ausgewählte TuiVision-Checkout darf für eine autorisierte Aufgabe
schreibbar sein. Der übergeordnete Projektordner, andere Repositories, Home,
Desktop, Downloads, Browserprofile und Credential Stores bleiben außerhalb.

*Only the selected TuiVision checkout may be writable for an authorized task.
Its parent project directory, other repositories, home, desktop, downloads,
browser profiles, and credential stores remain outside.*

## Rollen / Roles

| Rolle / Role | Containerziel / Target | Zugriff / Access | Zweck und Grenze / Purpose and boundary |
|---|---|---|---|
| `TuiVisionCheckout` | `/rider-projects/TuiVision` | Read-write | Nur Repository und ignorierte temporäre Ausgabe / repository and ignored temporary output only |
| `SandboxCheckout` | `/ade-dev-sandbox` | Technisch read-write, für TuiVision read-only behandeln / technically read-write, treat as read-only for TuiVision | Kein externer Repository-Write in Feature 044 / no external repository write in Feature 044 |
| `AgentStateVolume` | tool-spezifisch unter `/home/adedev` | Named volume | Sitzungen und Konfiguration getrennt vom Projekt / sessions and configuration separated from project |
| `BuildCacheVolume` | `/dotnet-build` | Named volume | Buildausgabe außerhalb des Source-Mounts / build output outside source mount |
| `AuditMetadataDirectory` | `/audit` | Read-write | Nur enge Metadaten, keine Prompts oder Antworten / narrow metadata only, no prompts or responses |
| `DotnetConfiguration` | `/dotnet-config/ContainerBuild.props` | Read-only | Sandbox-eigene Buildkonfiguration / sandbox-owned build configuration |
| `UnrelatedHostData` | nicht gemountet / not mounted | None | Alle projektfremden und privaten Daten / all unrelated and private data |

## Secrets

Lokale Environment-Dateien dürfen nur außerhalb der Versionskontrolle liegen.
Evidence nennt den Mechanismus, aber nie Wert, absoluten privaten Pfad oder
vollständige Umgebungsvariable. Prompt, Antwort, Screenshot und Audit-Export
enthalten keine Tokens oder Rohsitzungen.

*Local environment files stay outside version control. Evidence names the
mechanism but never a value, private absolute path, or complete environment
variable. Prompts, responses, screenshots, and audit exports contain no tokens
or raw sessions.*

## Stop-Grenze / Stop Boundary

Wenn eine Aufgabe einen breiteren Mount, produktive Daten, besondere Kategorien
personenbezogener Daten oder einen Credential Store benötigt, wird die Sitzung
nicht gestartet. Zuerst ist eine neue Security-Entscheidung erforderlich.

*Do not start the session when a task needs a broader mount, production data,
special-category personal data, or a credential store. A new security decision
is required first.*
