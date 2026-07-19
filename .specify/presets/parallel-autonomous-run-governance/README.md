# Parallel Autonomous Run Governance Preset

Version: `0.2.0`
Status: validiertes achtes Standard-Preset; Kampagnenstart bleibt delegationspflichtig
Priority: `80`
Requires: Spec Kit `>=0.8.3` and `autonomous-run-governance >=0.2.2`

## Zweck / Purpose

Dieses Preset koordiniert mehrere isolierte autonome Spec-Kit-Laeufe. Es
unterstuetzt replizierte Ziele, unabhaengige Features, menschlich ausgewaehlte
Alternativen und abhaengigkeitsgeordnete Pipelines. Es ersetzt nicht den
Einzellauf-Lebenszyklus und erteilt keine Ausfuehrungs- oder Remote-Berechtigung.

*This preset coordinates several isolated autonomous Spec Kit runs. It supports
replicated targets, independent features, human-selected alternatives, and
dependency-ordered pipelines. It does not replace the single-run lifecycle and
does not grant execution or remote authority.*

## Installation / Installation

```bash
specify preset add \
  --from https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/archive/refs/tags/v0.2.0.zip \
  --priority 80
specify preset add --dev /path/to/parallel-autonomous-run-governance --priority 80
specify preset info parallel-autonomous-run-governance
```

Version `0.2.0` gehoert zur installierten Standard-Achtermatrix. Das Starten
einer parallelen autonomen Kampagne bleibt eine ausdruecklich zu delegierende
Aktion. Die 13-Worker-Entwicklungsprobe und der 24-Worker-Feldtest mit sechs
MSL-Sprachen sind abgeschlossen.

*Version `0.2.0` is part of the installed standard eight-preset matrix. Starting
a parallel autonomous campaign still requires explicit delegation. The
13-worker development smoke and the 24-worker field test across six MSL
languages are complete.*

Nachweise / Evidence:

- [`docs/field-evidence/native-macos-smoke-2026-07-18.md`](docs/field-evidence/native-macos-smoke-2026-07-18.md)
- [`docs/field-evidence/native-multi-agent-smoke-2026-07-19.md`](docs/field-evidence/native-multi-agent-smoke-2026-07-19.md)
- [`docs/field-evidence/secure-casetracker-native-field-2026-07-18.md`](docs/field-evidence/secure-casetracker-native-field-2026-07-18.md)
- [`docs/field-evidence/secure-casetracker-native-field-retrospective-2026-07-19.md`](docs/field-evidence/secure-casetracker-native-field-retrospective-2026-07-19.md)

## Schema und Kompatibilitaet / Schema and Compatibility

Neue Templates verwenden Schema `1.1`. Der Koordinator liest weiterhin
Kampagnen-, Runner-, State- und Worker-Result-Artefakte in Schema `1.0`.
Schema `1.1` ergaenzt Worker-spezifische Runner-Profile, nicht geheime
Statusmetadaten, Provider-Preflights, Versuchshistorie und Post-Merge-Aktionen.
Ein historisches Schema-1.0-`MergeAndSync`-Manifest bleibt lesbar und kann bis
`ReadyForConsolidation` fortgefuehrt werden. Ein echter Merge erfordert jedoch
die explizite Migration auf den providergebundenen Preflight-Vertrag und die
Post-Merge-Aktionen aus Schema `1.1`; die unsichere Legacy-Mergeform wird nicht
automatisch ausgefuehrt.

*New templates emit schema `1.1`. The coordinator continues to read schema
`1.0` campaign, runner, state, and worker-result artifacts. Schema `1.1` adds
per-worker runner profiles, non-secret status metadata, provider preflights,
attempt history, and post-merge actions. A historical schema `1.0`
`MergeAndSync` manifest remains readable and may advance to
`ReadyForConsolidation`. Performing a merge requires explicit migration to the
schema `1.1` provider-preflight contract and post-merge actions; the unsafe
legacy merge shape is never executed automatically.*

## Entwicklungs-Override / Development Override

Der Repository-Eigentuemer hat die Smoke- und Secure-CaseTracker-Feldkampagnen
vom 2026-07-18 ausdruecklich fuer eine native Ausfuehrung auf dem
Entwicklungs-Mac freigegeben. Diese temporaere, kampagnenspezifische Ausnahme
ist mit dem Closeout abgelaufen. Container-First gilt wieder fuer regulaere
Secure-Trader-, Lernenden-, Produktions-, Wartungs- und spaetere Kampagnen.

*The repository owner explicitly authorized the 2026-07-18 smoke and Secure
CaseTracker field campaigns to run natively on the development Mac. This
temporary campaign-specific exception expired with the closeout. Container-
First again applies to routine Secure Trader, learner, production, maintenance,
and later campaigns.*

## Koordinator / Coordinator

```bash
bash .specify/presets/parallel-autonomous-run-governance/scripts/orchestrate-parallel-autonomous-runs.sh \
  -Action Validate \
  -Manifest specs/NNN-campaign/parallel-campaign.json \
  -RunnerConfig ~/.config/spec-kit/parallel-runner-profiles.json
```

```powershell
pwsh -NoProfile -File .specify/presets/parallel-autonomous-run-governance/scripts/orchestrate-parallel-autonomous-runs.ps1 `
  -Action Status `
  -Manifest specs/NNN-campaign/parallel-campaign.json `
  -State specs/NNN-campaign/parallel-campaign-state.json `
  -OutputFormat Text
```

Runner-Profile enthalten ausfuehrbare Namen und Argument-Arrays, keine Secrets.
Ein Worker darf `runnerProfile` setzen; fehlt der Wert, gilt das
Kampagnenprofil. `agentFamily` ist Statusmetadatum. `model` und
`reasoningEffort` sind optional und werden nie erraten. Ohne ausdrueckliche
Angabe zeigt Status `Agent-Standard/nicht deklariert`.

*Runner profiles contain executable names and argument arrays, not secrets. A
worker may set `runnerProfile`; otherwise the campaign profile is the fallback.
`agentFamily` is status metadata. `model` and `reasoningEffort` are optional
and are never guessed. Status shows `Agent-Standard/nicht deklariert` when they
are not explicitly declared.*

[`templates/parallel-runner-profiles-examples.json`](templates/parallel-runner-profiles-examples.json)
zeigt lokale Profile fuer Codex, Claude Code, GitHub Copilot CLI, Google
Antigravity, OpenCode und Junie. Die Beispiele geben kein Modell vor und
erteilen keine pauschale Netzwerkfreigabe. Nicht interaktive Schreibrechte
muessen lokal so eng wie moeglich fuer das isolierte Worker-Worktree
konfiguriert werden.

*[`templates/parallel-runner-profiles-examples.json`](templates/parallel-runner-profiles-examples.json)
shows local profiles for Codex, Claude Code, GitHub Copilot CLI, Google
Antigravity, OpenCode, and Junie. The examples prescribe no model and grant no
blanket network access. Non-interactive write permissions must be configured
locally and scoped as narrowly as possible to the isolated worker worktree.*

## Konsolidierung und Closeout / Consolidation and Closeout

Der lokale Provider-Adapter schreibt vor jedem Merge einen standardisierten
Preflight-Vertrag: PR offen oder exakt gemergt, kein Draft, exakter Head,
mergebar, keine aktuelle Change Request, keine aktuellen ungeloesten Threads
und erfuellte Check-Policy ohne technische Fehler. Bereits verifizierte Merges
werden bei Resume uebersprungen; extern gemergte exakte Heads werden
uebernommen. Abweichungen werden `NeedsRevalidation`.

*Before every merge, the local provider adapter writes a standard preflight
contract: PR open or exactly merged, not draft, exact head, mergeable, no
current change request, no current unresolved threads, and a satisfied check
policy without technical failures. Resume skips verified merges and adopts an
externally merged exact head. Drift becomes `NeedsRevalidation`.*

`postMergeActions` stammen ausschliesslich aus dem geprueften Manifest. Ihre
lokalen Profile muessen idempotent sein und die Phasen `Synchronize`,
`PostMerge` und `Validate` abbilden. `Completed` wird erst nach Merge,
Synchronisation, Post-Merge-Aktionen und Abschlussvalidierung gesetzt.

*`postMergeActions` come only from the reviewed manifest. Their local profiles
must be idempotent and cover `Synchronize`, `PostMerge`, and `Validate`.
`Completed` is written only after merge, synchronization, post-merge actions,
and final validation.*

## Sicherheit / Safety

- Maximum supported concurrency is three.
- Every worker owns a separate branch and worktree.
- Every worker is instructed to remain on its assigned branch.
- A dependent worker may branch from a validated direct predecessor through
  `baseWorkerId`, preserving sequential feature history without sharing a
  worktree.
- Stop is cooperative and grants no process-kill authority.
- Alternative consolidation requires explicit human selection.
- Merge-and-sync uses an all-ready barrier and resumable per-merge checkpoints.
- Remaining direct and stacked PR bases are revalidated after each merge.
- Child workers in a `MergeAndSync` campaign stop at `PublishPR`; only the
  coordinator may execute the ordered merge profile after every result is
  ready.
- State events include timestamps and attempt counters without executable
  arguments, secrets, or undeclared provider configuration.
- Installation grants no remote, merge, bypass, cancellation, secret, or
  provider-administration rights.
