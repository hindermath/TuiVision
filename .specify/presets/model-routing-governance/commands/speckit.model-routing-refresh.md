---
description: Refresh the local harness model-to-role binding
---

# Model Routing Refresh

Refresh exactly one local, non-versioned model-routing profile under current
explicit authority. Resolve the script from the repository's top-level
`scripts` directory first and
otherwise from `.specify/presets/model-routing-governance/scripts/`. Detect the
active harness explicitly. All five harnesses have automatic paths in the 0.2.0
candidate. Claude/Copilot require an explicitly supplied local SDK entry;
OpenCode requires an already running loopback server and a selected connected
provider. Follow the adapter guide; never install dependencies or start another
harness as fallback.

DE: Genau eine lokale Bindung nur nach ausdrücklichem Auftrag aktualisieren.
Codex und Antigravity bleiben unverändert. Neue Adapter haben ausdrückliche
Voraussetzungen; fehlende Fähigkeiten oder mehrdeutige Präferenzstufen blockieren.
Neue Adapter wählen geeignete Modelle mehrerer Familien. Exakte Discovery-IDs
können mit `-FrontierModel`/`-FastModel` vorgegeben werden.
Refresh prüft Discovery-Metadaten und schreibt das lokale Profil. Er führt
keinen Modellzugangs-Preflight aus. Ein späterer Runner-Preflight kann ein
Modell aufrufen und Kontingent verbrauchen; dafür gilt die eigene Laufautorität.

EN: Refresh checks discovery metadata and writes the local profile; it does
not execute the model-access preflight. A later runner preflight can call a
model and consume quota, under its own execution authority.

Automatically accept only unique eligible discovered models. Capability checks
precede broad family preferences; exact IDs may use `-FrontierModel`/`-FastModel`.
Missing capabilities, ambiguous tiers, unavailable or cross-provider mappings
fail closed with a proposal. Never write
credentials, machine identifiers, private paths, or repository files. Do not
start Spec Kit, autonomous delivery, commits, pushes, pull requests, or merges.

## Nutzungshilfe / Reader help

DE: Stabile Aufgabenrollen an ausdrücklich eingerichtete lokale Profile binden; die Grenzen dieses Releases sichtbar halten. Diese Command-Erklärung ändert keine Berechtigungen oder Voraussetzungen.
Die bestehenden technischen Feldnamen und Statuswerte nicht übersetzen oder
nur zum Bestehen eines Tests verändern. Einführung, Begriffe, Beispiel,
Ergebnis und Fehlerhilfe stehen im [deutschen Leserpfad](https://github.com/hindermath/spec-kit-preset-model-routing-governance/blob/main/docs/user-guide.md).

EN: Bind stable task roles to explicitly configured local profiles; keep this release's limits visible. This command explanation changes no authority or prerequisites.
Keep technical field names and states unchanged; never alter them just to pass.
See the [English reader path](https://github.com/hindermath/spec-kit-preset-model-routing-governance/blob/main/docs/user-guide.EN.md)
for introduction, terms, example, result and troubleshooting.
