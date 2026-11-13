# resolve-model-routing(1)

## NAME / NAME

resolve-model-routing - lokale Spec-Kit-Modellbindungen prüfen oder aktualisieren

*resolve-model-routing - inspect or refresh local Spec Kit model bindings*

## AUFRUF / SYNOPSIS

```text
pwsh -NoProfile -File scripts/resolve-model-routing.ps1 -Action Status [-Harness Codex]
bash scripts/resolve-model-routing.sh -Action Refresh -Harness Antigravity
```

## BESCHREIBUNG

`Status` liest ausschließlich den aktuellen Zustand. `Refresh` schreibt das
ausgewählte lokale Profil nur, wenn Discovery und Validierung genau eine
bekannte Zuordnung bestätigen. Unbekannte oder mehrdeutige Modelle blockieren.
Keine Aktion verändert das Repository oder erteilt Delivery-Autorität.

Alle fünf Harnesses haben automatische Pfade im Kandidaten 0.2.0. Claude/Copilot benötigen -SdkModulePath (absoluter vorhandener SDK-Einstieg). OpenCode benötigt -OpenCodeServerUrl (HTTP, 127.0.0.1 oder [::1], kein Pfad/Query/Redirect) und -OpenCodeProvider (verbunden). Siehe [Automatische Erkennung](../../.specify/presets/model-routing-governance/docs/automatic-discovery.md). Keine automatische Installation oder Serverstarts. Refresh prüft Metadaten; er
führt keinen Modellzugangs-Preflight aus. Der spätere Runner-Preflight kann
Kontingent verbrauchen und braucht die passende Ausführungsautorität.

## DESCRIPTION

`Status` is read-only. `Refresh` writes only the selected local profile after
a known, unique mapping passes discovery and validation. Unknown or ambiguous
models fail closed. Neither action changes a repository or grants delivery
authority.

All five harnesses have automatic paths in candidate 0.2.0. Claude/Copilot require -SdkModulePath (absolute existing SDK entry). OpenCode requires -OpenCodeServerUrl (HTTP loopback origin only) and -OpenCodeProvider (connected). See [Automatic discovery](../../.specify/presets/model-routing-governance/docs/automatic-discovery.EN.md). No automatic installation or server startup.
Refresh checks metadata; it does not run a model-access preflight. A later
runner preflight can consume quota and needs applicable execution authority.

## ENDESTATUS / EXIT STATUS

DE: Neue Adapter verwenden Fähigkeitsfilter und breite Modellpräferenzen.
`-FrontierModel` und `-FastModel` lösen Mehrdeutigkeit mit exakten Discovery-IDs.
Haiku ist keine Voraussetzung; Frontier darf mehrere Rollen übernehmen.
Ein neuer Profil-Hash `selectionPolicySha256` erkennt Auswahlpolicy-Drift.
Codex/agy-Regeln bleiben unverändert.

EN: New adapters use capability filters and broad model preferences.
`-FrontierModel` and `-FastModel` resolve ambiguity with exact discovered IDs.
Haiku is not required; frontier may serve multiple roles. New profile metadata
`selectionPolicySha256` detects selection-policy drift. Codex/agy are unchanged.

- `0`: ausgerichtet oder erfolgreich aktualisiert / aligned or refreshed successfully
- `2`: Aktualisierung oder Klärung erforderlich / refresh required or needs clarification
- `3`: durch fehlende Harness-, Discovery-, Modell- oder Reasoning-Evidence blockiert / blocked by missing harness, discovery, model, or reasoning evidence
