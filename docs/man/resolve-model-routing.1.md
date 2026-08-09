# resolve-model-routing(1)

## NAME / NAME

`resolve-model-routing` - lokale Spec-Kit-Modellbindungen prüfen oder aktualisieren

*Inspects or refreshes local Spec Kit model bindings.*

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

## DESCRIPTION

`Status` is read-only. `Refresh` writes only the selected local profile after
a known, unique mapping passes discovery and validation. Unknown or ambiguous
models fail closed. Neither action changes a repository or grants delivery
authority.

## ENDESTATUS / EXIT STATUS

- `0`: ausgerichtet oder erfolgreich aktualisiert / aligned or refreshed successfully
- `2`: Aktualisierung oder Klärung erforderlich / refresh required or needs clarification
- `3`: durch fehlende Harness-, Discovery-, Modell- oder Reasoning-Evidence blockiert / blocked by missing harness, discovery, model, or reasoning evidence
