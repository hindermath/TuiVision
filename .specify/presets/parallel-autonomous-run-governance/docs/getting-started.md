# Erste Kampagne / First Campaign

[Handbuch / Manual](README.md) | [Topologien / Topologies](topologies-and-scheduling.md)

## Deutsch

### Voraussetzungen

1. Spec Kit `>=0.8.3` ist installiert.
2. Preset 7 `>=0.2.2` ist in jedem Worker-Repository aktiv.
3. Preset 8 `>=0.2.3` ist im Koordinatorprojekt aktiv.
4. Jedes Repository ist ein Git-Worktree mit bekanntem sauberem Ausgangsstand.
5. Manifest und Runner-Konfiguration enthalten keine Secrets.
6. Delivery-Modus und Remote-Berechtigungen sind ausdruecklich delegiert.
7. `maxConcurrency` liegt zwischen `1` und `3`.

### Installationsreihenfolge

```bash
specify preset add \
  --from https://github.com/hindermath/spec-kit-preset-autonomous-run-governance/archive/refs/tags/v0.3.2.zip \
  --priority 70
specify preset add \
  --from https://github.com/hindermath/spec-kit-preset-parallel-autonomous-run-governance/archive/refs/tags/v0.2.3.zip \
  --priority 80
```

```bash
specify preset info autonomous-run-governance
specify preset info parallel-autonomous-run-governance
specify preset resolve parallel-campaign-template
specify preset resolve parallel-runner-profiles-template
```

### Dateien vorbereiten

Versioniert:

- `specs/<campaign>/parallel-campaign.json`,
- Kampagnen-Runbook, Evidence und Retrospektive,
- Worker-Result- und Handoff-Artefakte, soweit projektweit vorgesehen.

Lokal und nicht versioniert:

- Runner-Profile mit Executable und Argument-Array,
- Runtime-Root mit Worktrees, Locks, Logs und Prozessresultaten,
- Provider-Adapter-Konfiguration ohne Secrets im Manifest.

### Minimaler Preflight

```bash
bash .specify/presets/parallel-autonomous-run-governance/scripts/orchestrate-parallel-autonomous-runs.sh \
  -Action Validate \
  -Manifest specs/NNN-campaign/parallel-campaign.json \
  -RunnerConfig ~/.config/spec-kit/parallel-runner-profiles.json
```

PowerShell:

```powershell
pwsh -NoProfile -File `
  .specify/presets/parallel-autonomous-run-governance/scripts/orchestrate-parallel-autonomous-runs.ps1 `
  -Action Validate `
  -Manifest specs/NNN-campaign/parallel-campaign.json `
  -RunnerConfig ~/.config/spec-kit/parallel-runner-profiles.json
```

Der Preflight prueft Identitaet, Schema, Topologie, UUIDs, Worker-DAG,
Branches, Basen, Repository-Zustaende, Runner-Profile, Preset-7-Abhaengigkeit
und Konsolidierungsregeln. Ein Fehler startet keinen Worker.

### Start delegieren

```text
/speckit.parallel-autonomous

Starte die akzeptierte Kampagne aus
specs/NNN-campaign/parallel-campaign.json.
Delivery-Modus und Berechtigungen entsprechen dem Manifest und aktuellen
Auftrag. Maximal drei Worker gleichzeitig. Keine zusaetzlichen Rechte
ableiten. Bei Integritaets-, Sicherheits-, Berechtigungs- oder
Evidence-Fehlern keine neuen Worker starten.
```

### Direkt nach dem Start

```text
/speckit.parallel-autonomous-status
```

Pruefe Campaign-ID, Manifest-Hash, Worker-Zustaende, tatsaechlich beobachtete
Parallelitaet, Runner-Familien, Stop-Status und naechsten exakten Schritt.

## English

### Prerequisites

Spec Kit `>=0.8.3`, Preset 7 `>=0.2.2` in every worker repository, and Preset
8 `>=0.2.3` in the coordinator project are required. Repositories have known
clean bases, manifest and local runners contain no secrets, authority is
explicit, and `maxConcurrency` is between `1` and `3`.

### File boundary

Version campaign manifest, runbook, evidence, retrospective, and intended
result/handoff artifacts. Keep executable runner bindings, runtime worktrees,
locks, logs, and process results local and untracked.

### Validate before start

Use coordinator action `Validate` with `-Manifest` and `-RunnerConfig`. It
checks identity, schema, topology, UUIDs, DAG, branches, bases, repositories,
runner profiles, the Preset 7 dependency, and consolidation rules. A failure
starts no worker.

Explicitly delegate `/speckit.parallel-autonomous` only after validation.
Inspect the campaign immediately with
`/speckit.parallel-autonomous-status`.
