# Feldtest-Retrospektive / Field-Test Retrospective

Kampagne / Campaign: `91c2c1a0-1526-479a-b8a3-e36a7d15d2b1`
Entscheidung / Decision: `Promote to v0.2.0`

## Portable Erkenntnisse / Portable Findings

| Erkenntnis / Finding | Entscheidung fuer v0.2.0 / Decision for v0.2.0 |
|---|---|
| Ein Kampagnenprofil reicht nicht fuer gemischte Agenten. | Optionales `runnerProfile` je Worker; Kampagnenprofil bleibt Fallback. |
| Modellnamen altern und sind agentenspezifisch. | Nur ausdruecklich deklarierte Modell-/Effort-Metadaten anzeigen; sonst `Agent-Standard/nicht deklariert`. |
| Merge-Shellerfolg beweist keinen PR-Zustand. | Providergebundener Preflight vor und nach jedem Merge. |
| Teilmerges sind bei mehreren Repositories normal moeglich. | Verifizierte `Merged`-Worker ueberspringen; exakte externe Merges uebernehmen. |
| Ein gestapelter Merge kann verbleibende PR-Basen veraendern. | Alle verbleibenden direkten und gestapelten PRs nach jedem Merge revalidieren. |
| Stop muss auch in der Konsolidierung gelten. | Stop-Anforderung zwischen Merge-, Sync-, Closeout- und Validierungsschritten pruefen. |
| Merge ist nicht Kampagnenabschluss. | Manifestdeklarierte idempotente `postMergeActions`; `Completed` erst nach finaler Validierung. |
| Ein einzelner Zeitstempel reicht fuer Resume-Analyse nicht. | Dauerhafte Ereignisse und Versuchszähler in Schema 1.1. |
| Status muss Menschen und Werkzeuge bedienen. | JSON plus barrierearme Textausgabe ohne Secrets. |
| Parallelitaet 3 wurde real belegt, groessere Werte nicht. | Obergrenze bleibt `3`. |

*These rules are provider- and agent-neutral. They preserve exact-head proof,
explicit authority, resumability, and observable closeout without prescribing
a model or provider.*

## Nicht portable Beobachtungen / Non-Portable Observations

| Kategorie / Category | Beobachtung / Observation | Behandlung / Treatment |
|---|---|---|
| macOS | Der Feldtest lief nativ auf macOS 26.5.2. | Nur Evidenz; keine Preset-Voraussetzung. |
| GitHub | `gh pr merge --delete-branch` beeinflusste gestapelte Swift-PRs. | Provider-Adapter und Revalidation, keine GitHub-Hardcodierung im Kern. |
| Billing | 160 Entwicklungs- und 60 Closeout-Jobs wurden vor Schritt 1 abgewiesen. | Enger Feldtest-Override endet; keine allgemeine Regel. |
| C# | 83 Tests und .NET-spezifische Gates. | Repository-Evidenz, nicht Preset-Vertrag. |
| Go | Race/Vet/`govulncheck`. | Repository-Evidenz, nicht Preset-Vertrag. |
| Java | Maven, 173 Tests, Checkstyle und SBOM. | Repository-Evidenz, nicht Preset-Vertrag. |
| Python | 165 Tests und `pip-audit`. | Repository-Evidenz, nicht Preset-Vertrag. |
| Rust | Clippy, Rustdoc und Cargo-Metadaten. | Repository-Evidenz, nicht Preset-Vertrag. |
| Swift | 37 Tests und gestapelte PR-Basen. | Sprachgates lokal; Basis-Revalidation portabel. |
| Codex | Lokale Konfiguration zeigte `gpt-5.6-sol`, `xhigh`. | Nur beobachtete Provenienz; keine Modellvorgabe. |

## Fehler, die Evidenz erzeugten / Failures That Produced Evidence

1. Der leere Auswahlparameter blockierte die erste Pipeline-Konsolidierung vor
   jedem Merge. Das war ein sicherer Fehler ohne Remote-Aenderung.
2. Der zweite Versuch bewies den nicht fortsetzbaren V1-Teilmerge: 21
   erfolgreiche Merges blieben erhalten, Swift #4 wurde nach Loeschen seiner
   Basis automatisch geschlossen.
3. Der Root-Dateipfad im Lastenheft-Rename-Skript schlug ohne `./` vor jeder
   Dateiaenderung fehl.
4. Der historische Campaign-State konnte den manuellen, kontrollierten
   Restabschluss nicht aufnehmen und bleibt deshalb `MergeFailed`.

*The failures were retained as evidence. They directly motivate optional
pipeline selection, resumable merge adoption, stacked-base revalidation,
manifest-only closeout actions, and richer state history.*

## Sicherheits- und Berechtigungsbewertung / Security and Authority Review

- Keine Worker-Berechtigung wurde in Merge-Berechtigung umgedeutet.
- Der Admin-Bypass wurde erst nach exaktem Head-, Review- und lokalem
  Gate-Nachweis verwendet.
- Runner-Konfiguration und lokale Modellherkunft enthalten keine Secrets im
  oeffentlichen Preset.
- Post-Merge-Kommandos werden nicht aus Worker-Handoffs uebernommen.
- Der native Override und der Billing-Override sind abgelaufen.

*No worker authority was converted into merge authority. Admin bypass followed
exact-head, review, and local-gate evidence. Public state contains no runner
secrets. Worker handoffs cannot define post-merge commands. Both development
overrides have expired.*

## Entscheidung / Decision

Die Feldbreite von 24 Workern, sechs MSL-Sprachen, vier sequenziellen Units je
Sprache und beobachteter Parallelitaet 3 reicht fuer die Promotion von
`parallel-autonomous-run-governance` auf `v0.2.0`. Sie rechtfertigt keine
Erhoehung der maximalen Parallelitaet und keine allgemeine Billing-Ausnahme.

*The field breadth supports promotion to `v0.2.0`. It does not justify
concurrency above three or a general billing exception.*
