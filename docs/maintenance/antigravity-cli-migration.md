# Antigravity-CLI-Migration / Antigravity CLI Migration

## Entscheidung / Decision

Seit dem Wartungslauf vom 11. Juli 2026 ersetzt Antigravity CLI die bisherige Gemini CLI in allen aktiven Level-0-, Level-1- und Level-2-Betriebsflächen. Der kanonische Befehl ist `agy`; die minimale bei der Migration geprüfte Version ist `1.1.1`.

*Since the maintenance run of 11 July 2026, Antigravity CLI replaces the former Gemini CLI in every active Level-0, Level-1, and Level-2 operational surface. The canonical command is `agy`; the minimum version verified during migration is `1.1.1`.*

Verbindliche Herstellergrundlagen sind die [Antigravity-Migrationsanleitung](https://antigravity.google/assets/docs/cli/gcli-migration.md), die [CLI-Referenz](https://antigravity.google/docs/cli-reference) und die [Sandbox-Dokumentation](https://antigravity.google/docs/cli-sandbox).

## Repository-Konventionen / Repository Conventions

- `GEMINI.md` bleibt erhalten, weil Antigravity diese Kontextdatei weiterhin liest.
- Spec Kit verwendet `--integration agy`, `.specify/integrations/agy.manifest.json` und die mit Codex geteilten `.agents/skills/`.
- `.gemini/commands/` und `gemini.manifest.json` sind keine aktiven Integrationsflächen mehr.
- Abgeschlossene Specs, Lastenhefte, Changelogs und Testausgaben bleiben unveränderte historische Nachweise.
- Direkte Aufrufe von `gemini` sind außerhalb dieser historischen Artefakte nicht zulässig.

*`GEMINI.md` remains because Antigravity continues to read this context file. Spec Kit uses `--integration agy`, the `agy` manifest, and shared `.agents/skills/`. Completed specifications and logs remain unchanged historical evidence; active direct `gemini` invocations are not allowed.*

## Installation und Updates / Installation and Updates

| Plattform / Platform | Verwalteter Pfad / Managed route |
|---|---|
| macOS | Homebrew-Cask `antigravity-cli` |
| Linux | Offizieller Installer nach SHA-256-Prüfung gegen die Required-CLI-Registry |
| Windows | WinGet-Paket `Google.AntigravityCLI` |

Eine geänderte Linux-Installer-Prüfsumme führt zum Abbruch. Die Registry darf erst nach manueller Prüfung der neuen Herstellerdatei aktualisiert werden.

*A changed Linux installer digest fails closed. The registry may only be updated after manual review of the new vendor file.*

Spec Kit `0.8.3` zeigt bei `--integration agy` derzeit einen Hinweis auf
„Antigravity v1.20.5“. Diese Versionsangabe passt nicht zum eigenständigen
CLI-Versionsschema (`agy 1.1.1`). Die Integration wurde dennoch erfolgreich
erzeugt. Bis zur upstream-seitigen Klärung gilt deshalb `agy --version` plus
ein vorhandenes `agy.manifest.json` als technischer Nachweis; die Baseline
erfindet keine `1.20.5`-Mindestversion.

*Spec Kit `0.8.3` currently prints an “Antigravity v1.20.5” advisory for the
`agy` integration. That version does not match the standalone CLI versioning
scheme (`agy 1.1.1`). The integration still completes successfully. Until the
upstream wording is clarified, `agy --version` plus an `agy.manifest.json`
is the technical evidence; the baseline does not invent a `1.20.5` minimum.*

## Sicherheitsbaseline / Security Baseline

`setup-antigravity-settings.*` setzt `toolPermission=strict`, `artifactReviewPolicy=asks-for-review`, Terminal-Sandbox, Workspace-Isolation und deaktivierte Telemetrie. Unbekannte JSON-Schlüssel bleiben erhalten. Berechtigungs-Bypass-Optionen sind in der Baseline untersagt.

*The setup scripts enforce strict tool approval, artifact review, terminal sandboxing, workspace isolation, and disabled telemetry while preserving unknown JSON keys. Permission bypass options are prohibited in the baseline.*

`.agents/mcp_config.json`, globale MCP-Konfiguration, Settings, Logs, Historien, Gesprächsdatenbanken und Installation-IDs sind lokale sensible Daten und werden nicht versioniert. Nur `.agents/skills/**` ist über eine chirurgische Gitignore-Allowlist freigegeben.

## Betrieb / Operations

```bash
bash scripts/setup-antigravity-settings.sh --check-only
bash scripts/audit-antigravity-migration.sh --fail-on-open
bash scripts/update-spec-kit.sh --dry-run
```

Die Migration ist abgeschlossen, wenn der Audit keine offenen Befunde meldet, `agy --version` auf allen Plattformen funktioniert und die Repository-CI grün ist. Antigravity bleibt reine Entwicklungs-Toolchain; eine produktbezogene AI-SBOM ist deshalb `N/A`, solange keine KI-Runtime ausgeliefert oder betrieben wird.

*Migration is complete when the audit has no open findings, `agy --version` works on every platform, and repository CI is green. Antigravity remains development tooling; product AI-SBOM applicability stays `N/A` unless an AI runtime is shipped or operated.*
