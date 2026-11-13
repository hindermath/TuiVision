---
name: speckit-model-routing-status
description: Inspect harness-local model routing without changing files
compatibility: Requires spec-kit project structure with .specify/ directory
metadata:
  author: github-spec-kit
  source: preset:model-routing-governance
---

# Speckit Model Routing Status Skill

# Model Routing Status

Inspect the current harness, its version, discovery capability, available
models and reasoning levels, installed `model-routing.json` catalogs, and the
effective local role bindings. This command is strictly read-only. Do not
create or refresh a profile, do not invoke a model, and do not change Git or
remote state.

Resolve the script from the repository's top-level `scripts` directory when it
exists; otherwise use
`.specify/presets/model-routing-governance/scripts/resolve-model-routing.ps1`
or its Bash wrapper. Run `-Action Status`. Report `Aligned`, `RefreshRequired`,
`NeedsClarification`, or `Blocked`, followed by a text-first reason and exact
next action.

DE: Bei mehreren gleichrangigen Modellen nennt der Status Kandidaten-IDs.
`-FrontierModel`/`-FastModel` wählen exakte gemeldete IDs für die neuen Adapter,
ohne Fähigkeits-/Policy-Prüfungen zu umgehen. Ohne eigenes schnelles Modell
darf das gewählte Frontier-Modell mehrere Rollen übernehmen.

EN: Ambiguous tiers report candidate IDs. `-FrontierModel`/`-FastModel` select
exact discovered IDs for new adapters without bypassing capability/policy
checks. The frontier model may serve several roles if no fast model is selected.

DE: Für Claude/Copilot den ausdrücklich benannten vorhandenen SDK-Einstieg
mit `-SdkModulePath` verwenden. OpenCode benötigt `-OpenCodeServerUrl` und
`-OpenCodeProvider`; keinen Server starten. Keine Abhängigkeiten installieren.
Der Resolver schreibt keine Profile/Projektdateien; Herstellerprozesse können
eigene Logs/Caches pflegen. Details: [Adapterleitfaden](https://github.com/hindermath/spec-kit-preset-model-routing-governance/blob/main/docs/automatic-discovery.md).

EN: Claude/Copilot require an explicitly identified installed SDK entry through
`-SdkModulePath`. OpenCode requires `-OpenCodeServerUrl` and `-OpenCodeProvider`;
never start a server or install dependencies. The resolver writes no profiles
or project files; vendor processes may maintain their own logs/caches. See the
[adapter guide](https://github.com/hindermath/spec-kit-preset-model-routing-governance/blob/main/docs/automatic-discovery.EN.md).

## Nutzungshilfe / Reader help

DE: Stabile Aufgabenrollen an ausdrücklich eingerichtete lokale Profile binden; die Grenzen dieses Releases sichtbar halten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-model-routing-governance/blob/main/docs/user-guide.md).

EN: Bind stable task roles to explicitly configured local profiles; keep this release's limits visible. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-model-routing-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
