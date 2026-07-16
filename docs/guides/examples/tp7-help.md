# TP7 Help: Compiler und Viewer / Compiler and Viewer

## Zweck / Purpose

`Tp7Help` ordnet `TVHC.PAS`, `HELPFILE.PAS` und `DEMOHELP.PAS` dem
vorhandenen sicheren Help-Stack zu. Gültige `.topic`-Quellen erzeugen ein
vollständiges Modell; fehlerhafte Referenzen erzeugen stabile Diagnosen ohne
Teilmodell.

`Tp7Help` maps `TVHC.PAS`, `HELPFILE.PAS`, and `DEMOHELP.PAS` to the existing
safe Help stack. Valid `.topic` sources create a complete model; invalid
references create stable diagnostics without a partial model.

## Start und Kontexte / Launch and Contexts

```bash
dotnet run --project examples/Tp7Help
```

Bekannte Kontexte öffnen ein echtes `THelpWindow`. Ein unbekannter Kontext
zeigt den vorhandenen Fallback „Help not found“. Proprietäre historische
Binärformate werden nicht dekodiert.

Known contexts open a real `THelpWindow`. An unknown context shows the existing
“Help not found” fallback. Proprietary historical binary formats are not
decoded.

## A11Y und Proof / A11Y and Proof

Kompilierung, Diagnose, Kontext und Fallback sind über Commands und
Tastaturpfade erreichbar. Der primäre Smoke kombiniert Compilerzustand,
`THelpWindow`-Identität und gerenderte Topic-/Fallback-Zellen. Stage 2 ergänzt
vollständige Navigation, Cross-Reference-Bedienung und `Help -> Description`.

Compilation, diagnostics, context, and fallback are reachable through commands
and keyboard paths. The primary smoke combines compiler state,
`THelpWindow` identity, and rendered topic/fallback cells. Stage 2 adds
complete navigation, cross-reference operation, and `Help -> Description`.
