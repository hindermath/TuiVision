# Model Routing Governance

Version `0.1.2` validates PowerShell 7 before Bash wrappers delegate to it and
reports missing or outdated installations as a bilingual `Blocked` result.
Routing, discovery, and authority semantics are unchanged.

## Deutsch

`model-routing-governance` ordnet Spec-Kit-Arbeiten stabilen Rollen zu und
bindet diese Rollen auf jedem Rechner an tatsächlich verfügbare Modelle. Das
Preset ist öffentlich optional und wird mit Priorität `61` nach Agent Parity
(`60`) und vor Intake Authoring (`64`) installiert.

Das Remote-Repository enthält keine persönliche Modellliste. Berechtigungen,
Abonnements und Modellverfügbarkeit unterscheiden sich zwischen Rechnern und
Konten. Deshalb werden nur Rollen, Adapter und Auswahlregeln versioniert. Die
konkrete Bindung liegt lokal außerhalb des Projekt-Repositories.

### Rollen

| Rolle | Zweck |
|---|---|
| `frontier-reasoning` | Spezifikation, Klärung, Planung und Analyse |
| `long-running-implementation` | lange Implementierungsphasen |
| `coding-review` | Reviews und Retrospektiven |
| `fast-mechanical` | Status-, Read- und Next-Abfragen |
| `script-only` | deterministische Prüfungen ohne Modell |

### Harness-Fähigkeiten

- `Enumerate`: strukturierte Modelle und Reasoning-Stufen, beispielsweise
  Codex `model/list`.
- `EnumerateNames`: Modellnamenliste plus Validierung, beispielsweise
  `agy models`.
- `ValidateCandidate`: ausdrücklich konfigurierte Kandidaten einzeln prüfen.
- `ConfiguredOnly`: keine belastbare Discovery; nur vorhandene Konfiguration
  verwenden.

Unbekannte oder mehrdeutige Zuordnungen blockieren. Es gibt keinen stillen
Fallback und keinen automatischen Anbieterwechsel.

### Aufrufe

```text
$speckit-model-routing-status
$speckit-model-routing-refresh Refresh the local Codex binding.
```

Das Statuskommando ist read-only. Refresh schreibt ausschließlich die lokale
Konfigurationsdatei. Beide Commands erteilen keine Spec-Kit-, Git- oder
Remote-Autorität.

## English

`model-routing-governance` maps Spec Kit work to stable roles and binds those
roles to models that are actually available on each machine. The public preset
is optional and uses priority `61`, after Agent Parity (`60`) and before Intake
Authoring (`64`).

The remote repository never stores personal model availability. Only roles,
adapters, and selection rules are versioned. Concrete bindings remain local.
Structured enumeration, name-only enumeration, candidate validation, and
configured-only adapters are reported honestly. Unknown or ambiguous mappings
fail closed, and no provider switch happens silently.

## Installation

```bash
specify preset add --from https://github.com/hindermath/spec-kit-preset-model-routing-governance/archive/refs/tags/v0.1.2.zip --priority 61
```

License: MIT.
