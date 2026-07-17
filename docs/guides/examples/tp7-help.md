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
Binärformate werden nicht dekodiert. Das Topics-Menü öffnet Kontexte, aktiviert
den ausgewählten Querverweis und führt mit Back zum vorherigen Thema zurück.
Das Compiler-Menü zeigt die atomare Diagnose einer ungültigen Quelle.

Known contexts open a real `THelpWindow`. An unknown context shows the existing
“Help not found” fallback. Proprietary historical binary formats are not
decoded. The Topics menu opens contexts, activates the selected
cross-reference, and returns to the previous topic with Back. The Compiler
menu shows the atomic diagnostic for an invalid source.

## A11Y und Proof / A11Y and Proof

Kompilierung, Diagnose, Kontext, Cross-Reference, Back, Fallback und F1 sind
per Tastatur erreichbar. Der primäre Smoke kombiniert Compilerzustand,
fokussierten `THelpViewer`, `THelpWindow`-Identität, Navigation, Status,
Description und gerenderte Topic-/Fallback-Zellen. Die enge `48x16`-Ansicht
bleibt textorientiert bedienbar.

Compilation, diagnostics, context, cross-reference navigation, Back, fallback,
and F1 are keyboard reachable. The primary smoke combines compiler state, a
focused `THelpViewer`, `THelpWindow` identity, navigation, status,
Description, and rendered topic/fallback cells. The constrained `48x16` view
remains operable through text.
