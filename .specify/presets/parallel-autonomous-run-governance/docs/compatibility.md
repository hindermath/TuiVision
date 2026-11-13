# Versionierung und Kompatibilitaet / Versioning and Compatibility

<!-- current-package: 0.2.13 -->

DE: Dokumentations-Zielpaar: Autonomous 0.4.7 und Parallel 0.2.9; Mindestversionen und historische Feldtests unten bleiben unverändert. Die neue Paketvorbereitung steht im versionsgebundenen Nachweis.

EN: Documentation target pair: Autonomous 0.4.7 and Parallel 0.2.9; minimum versions and historical field tests below remain unchanged. See the version-bound preparation record for the new package.

DE: Paket 0.2.8 ist inzwischen regulär veröffentlicht; [historischer Release-Abschluss](release-0.2.8-validation.md). Die unten datierten Tabellen bewahren den historischen Prüfstand vor Veröffentlichung. Mindestversion, Beispielkombination und tatsächlich geprüfte Kombination bleiben getrennt; dieser Nachlauf behauptet keinen neuen Kombinationstest.

EN: Package 0.2.8 is now a regular published release; see the linked closeout. The dated tables below preserve the historical pre-publication assessment. Minimum, example and actually tested combinations stay distinct; this follow-up claims no new combination test.

[Deutsch: Leserpfad](user-guide.md) · [English: reader path](user-guide.EN.md)

[Handbuch / Manual](README.md)

## Deutsch

### Drei getrennte Versionsebenen

| Ebene | Prüfstand 2026-10-23 | Bedeutung |
|---|---|---|
| Bei Prüfungsbeginn veröffentlicht | `v0.2.7` | Historischer Release-Snapshot; aktuellen Stand auf der Release-Seite prüfen |
| Quellkandidat | `v0.2.8` | Aktueller Dokumentationskandidat, keine Veröffentlichungsbehauptung |
| `preset.yml`-Schema | `schema_version: "1.0"` | Spec-Kit-Presetmanifest |
| Kampagnenvertrag | `schemaVersion: "1.2"` | Manifest mit optionalem Intake-Review; State/Runner/Results bleiben 1.1 |

### Preset-7-Abhaengigkeit

Regulaere Kampagnen benoetigen
`autonomous-run-governance >=0.2.2` in jedem Worker-Repository. Der bisher
dokumentierte historische Prüfstand nennt Autonomous `v0.3.6` mit Parallel
`v0.2.6`. Die neue Dokumentations-Zielkombination ist Autonomous `v0.4.6`
mit Parallel `v0.2.8`. Das behauptet keinen neuen Live-Kampagnentest;
tatsächliche Fixture-/Plattformnachweise stehen getrennt im PR.

### Upgrade auf `v0.2.6`

`v0.2.6` richtet die aktuellen Installationsbeispiele auf Autonomous Run
Governance `v0.3.6` aus. Kampagnen-, Berechtigungs- und Routingverträge bleiben
unverändert.

`requireAutonomousPreset: false` existiert fuer isolierte interne
Koordinator-Fixtures. Es ist kein dokumentierter Produktionsmodus und hebt die
Worker-Governance aus Preset 7 nicht auf.

### Schema `1.0`

Der Koordinator liest historische Kampagnen-, Runner-, State- und
Worker-Result-Artefakte. Ein historisches `MergeAndSync` darf bis
`ReadyForConsolidation` laufen. Ein echter Merge benoetigt Migration auf den
providergebundenen Schema-`1.1`-Vertrag.

### Schema `1.1`

Schema `1.1` ergaenzt:

- Worker-spezifische Runner-Profile,
- nicht geheime Agentenmetadaten,
- Versuchszahlen und Events,
- Provider-Preflight und fortsetzbare Merge-Checkpoints,
- deklarierte idempotente Post-Merge-Aktionen,
- getrennte Completion-Felder.

### Upgrade auf `v0.2.5`

`v0.2.5` veröffentlicht den agentenneutralen `model-routing.json`-Vertrag;
Kampagnenverhalten und Schemas bleiben unverändert.

### Upgrade auf `v0.2.4`

`v0.2.4` ergaenzt Kampagnenschema 1.2 und das optionale Intake-Review-Gate.
Schema 1.0/1.1 und Kampagnen ohne aktive Pflicht bleiben kompatibel.

## English

### Three version layers

| Layer | Review snapshot 2026-10-23 | Meaning |
|---|---|---|
| Published at review start | `v0.2.7` | Historical release snapshot; check releases for current state |
| Source candidate | `v0.2.8` | Documentation candidate, not a publication claim |
| `preset.yml` schema | `schema_version: "1.0"` | Spec Kit preset manifest |
| Campaign contract | `schemaVersion: "1.2"` | Optional intake review in manifests; state/runners/results remain 1.1 |

### Preset 7 dependency

Regular campaigns require `autonomous-run-governance >=0.2.2` in every worker
repository. The previously documented historical pair is Autonomous `v0.3.6`
with Parallel `v0.2.6`. The new documentation target is Autonomous `v0.4.6`
with Parallel `v0.2.8`, not a newly claimed live campaign. Actual fixture/platform
evidence is recorded separately in the PR.

### Upgrade to `v0.2.6`

`v0.2.6` aligns current installation examples with Autonomous Run Governance
`v0.3.6`. Campaign, authority, and routing contracts remain unchanged.

`requireAutonomousPreset: false` exists for isolated internal coordinator
fixtures. It is not a documented production mode and does not replace Preset 7
worker governance.

### Schema compatibility

The coordinator reads historical schema `1.0` campaign, runner, state, and
worker-result artifacts. Historical `MergeAndSync` may reach
`ReadyForConsolidation`, but an actual merge requires explicit migration to
the provider-bound schema `1.1` contract.

Schema `1.1` adds per-worker runners, non-secret metadata, attempts and events,
provider preflight, resumable merge checkpoints, declared idempotent
post-merge actions, and separate completion fields.

`v0.2.5` publishes the agent-neutral `model-routing.json` contract; campaign
behavior and schemas are unchanged.

`v0.2.4` adds campaign schema 1.2 and the optional intake-review gate. Schema
1.0/1.1 and campaigns without an active requirement remain compatible.
