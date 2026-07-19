# Versionierung und Kompatibilitaet / Versioning and Compatibility

[Handbuch / Manual](README.md)

## Deutsch

### Drei getrennte Versionsebenen

| Ebene | Aktueller Wert | Bedeutung |
|---|---|---|
| Preset-Release | `v0.2.2` | Veroeffentlichtes Preset-Paket |
| `preset.yml`-Schema | `schema_version: "1.0"` | Spec-Kit-Presetmanifest |
| Kampagnenvertrag | `schemaVersion: "1.1"` | Manifest, State, Runner und Results |

### Preset-7-Abhaengigkeit

Regulaere Kampagnen benoetigen
`autonomous-run-governance >=0.2.2` in jedem Worker-Repository. Die gemeinsam
getestete aktuelle Kombination ist Preset 7 `v0.3.1` mit Preset 8 `v0.2.2`.

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

### Upgrade auf `v0.2.2`

`v0.2.2` ist ein Dokumentations-Patch. Es aendert keine Koordinator-,
Lifecycle-, Merge-, Runner-, Berechtigungs- oder Schema-Semantik gegenueber
`v0.2.1`.

## English

### Three version layers

| Layer | Current value | Meaning |
|---|---|---|
| Preset release | `v0.2.2` | Published preset package |
| `preset.yml` schema | `schema_version: "1.0"` | Spec Kit preset manifest |
| Campaign contract | `schemaVersion: "1.1"` | Manifest, state, runners, and results |

### Preset 7 dependency

Regular campaigns require `autonomous-run-governance >=0.2.2` in every worker
repository. The currently tested pair is Preset 7 `v0.3.1` with Preset 8
`v0.2.2`.

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

`v0.2.2` is a documentation patch and changes no coordinator, lifecycle,
merge, runner, authority, or schema semantics from `v0.2.1`.
