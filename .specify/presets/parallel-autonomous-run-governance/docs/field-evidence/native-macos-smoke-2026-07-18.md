# Nativer macOS-Real-Agent-Smoke / Native macOS Real-Agent Smoke Evidence

Date: `2026-07-18`
Preset version under test: `0.1.0`
Current successor documented by this repository: `0.2.1`
Agent family: `Codex`
Execution environment: native macOS development host
Delivery mode: `LocalImplementation`
Remote writes: none

## Deutsche Fassung

### Autorisierter Override

Repository-Eigentuemer Thorsten Hindermann autorisierte ausdruecklich, diesen
13-Worker-Smoke nativ auf dem Entwicklungs-Mac auszufuehren. Die Ausnahme galt
nur fuer diese Preset-Entwicklungskampagne und endete mit der Erfassung der
Ergebnisse. Sie aendert die Container-First-Regel des Workspaces nicht.

### Ergebnisse

| Topologie | Worker | Ergebnis | Konfiguriertes Maximum | Beobachtetes Maximum |
|---|---:|---|---:|---:|
| `ReplicatedTargets` | 3 | Abgeschlossen | 3 | 3 |
| `IndependentFeatures` | 3 | Abgeschlossen | 3 | 3 |
| `AlternativeSolutions` | 3 | Nach benannter Auswahl konsolidiert | 3 | 3 |
| `Pipeline` | 4 | Abgeschlossen | 3 | 2 |

Alle 13 Worker:

- durchliefen den installierten Skill `speckit-autonomous` in einem
  vollstaendigen lokalen Lebenszyklus;
- erzeugten eine gueltige `autonomous-run-state.json`;
- endeten mit Exitcode `0`;
- blieben in isolierten nummerierten Branches und Worktrees;
- erzeugten keine Remote-Schreiboperation;
- wurden vom Kampagnenkoordinator als `Completed` erfasst.

Die Pipeline fuehrte `A -> {B, C} -> D` aus. A erzeugte zwei deklarierte
Handoffs. B und C starteten erst, nachdem die Erzeugerdateien und ihre
SHA-256-Werte validiert waren. D startete erst, nachdem B und C jeweils ihr
eigenes Handoff erzeugt und validiert hatten. Die maximal beobachtete
Pipeline-Parallelitaet betrug zwei.

### Erkenntnisse

1. Ein Worker kann einen abgeschlossenen autonomen Lebenszyklus speichern,
   bevor der externe Agentenprozess endet. Der Koordinator wartet korrekt auf
   das Prozessende und validiert danach den Ergebnisvertrag.
2. Unveraenderliche eingehende Handoffs brauchen im Consumer-Prompt einen
   lesbaren Erzeugerpfad und SHA-256. Nur Erzeuger- und Consumer-IDs reichen
   ueber isolierte Worktrees hinweg nicht aus.
3. Eine proportionale Reasoning-Stufe ist fuer triviale Smoke-Features
   sinnvoll. Das Test-Harness waehlt eine Effort-Stufe, schreibt aber kein
   Modell vor.
4. Codex CLI `0.144.1` protokollierte wiederholt einen
   Modell-Cache-Kompatibilitaetsfehler fuer `supports_reasoning_summaries`.
   Isolierte Analytics- und Shell-Snapshot-Warnungen traten ebenfalls auf.
   Diese agentenspezifischen Beobachtungen aenderten weder Exitcodes noch
   Kampagnenergebnisse.

### Entscheidung

Die vier Topologien, begrenzte Planung, ausdrueckliche Alternativauswahl und
gehashte Pipeline-Handoffs waren bereit fuer die groessere native
Secure-CaseTracker-Feldkampagne. Das getestete Artefakt `0.1.0` blieb zu diesem
Zeitpunkt experimentell.

Der folgende Feld-Preflight zeigte, dass aufeinanderfolgende Units in einem
Repository den exakten Vorgaenger-Head erben muessen. Version `0.1.1`
ergaenzte den validierten `baseWorkerId`-Vertrag; `0.1.2` schloss vor der
Feldkampagne die Publikationsparitaet ab. Diese Staende sind historische
Checkpoints; die Feldtesterkenntnisse wurden in `0.2.0` uebernommen.

## English Version

### Authorized Override

Repository owner Thorsten Hindermann explicitly authorized this 13-worker smoke
to run natively on the development Mac. The exception applied only to this
preset-development campaign and expired when these findings were captured. It
does not amend the workspace Container-First rule.

### Results

| Topology | Workers | Result | Configured max | Observed max |
|---|---:|---|---:|---:|
| `ReplicatedTargets` | 3 | Completed | 3 | 3 |
| `IndependentFeatures` | 3 | Completed | 3 | 3 |
| `AlternativeSolutions` | 3 | Consolidated after named selection | 3 | 3 |
| `Pipeline` | 4 | Completed | 3 | 2 |

All 13 workers:

- ran the installed `speckit-autonomous` skill through a complete local
  lifecycle;
- produced a valid `autonomous-run-state.json`;
- exited with code `0`;
- remained in isolated numbered branches and worktrees;
- produced no remote write;
- were recorded as `Completed` by the campaign coordinator.

The pipeline executed `A -> {B, C} -> D`. A produced two declared handoffs. B
and C started only after the producer files and SHA-256 values passed
validation. D started only after B and C had each produced and validated their
own handoff. Maximum observed pipeline concurrency was two.

### Findings

1. A worker can persist a completed autonomous lifecycle before the external
   agent process exits. The coordinator correctly waits for process completion
   and validates the result contract afterward.
2. Immutable incoming handoffs need an explicit readable producer path and
   SHA-256 in the consumer prompt. Recording only producer and consumer IDs is
   insufficient across isolated worktrees.
3. A proportional reasoning setting is useful for trivial smoke features. The
   harness selects an effort level but does not prescribe a model.
4. Codex CLI `0.144.1` repeatedly logged a model-cache compatibility error for
   `supports_reasoning_summaries`; isolated analytics and shell-snapshot
   warnings also occurred. These messages did not change worker exit codes or
   campaign results and are agent-specific observations.

### Decision

The four topologies, bounded scheduling, explicit alternative selection, and
hashed pipeline handoffs were ready for the larger native Secure CaseTracker
field campaign. The tested `0.1.0` artifact remained experimental at this
checkpoint.

The subsequent field preflight identified that sequential units in one
repository need to inherit the exact predecessor head. Version `0.1.1` added
the validated `baseWorkerId` contract, and `0.1.2` completed publication
alignment before the field campaign. These are historical checkpoints; field
findings are promoted in `0.2.0`.
