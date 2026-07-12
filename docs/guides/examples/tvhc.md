# TvHc Beispiel / TvHc Example

## Deutsch

`TvHc` zeigt den verwalteten Help-Source-Compiler mit sichtbarem Ergebnis.
Start:

```bash
dotnet run --project examples/TvHc
```

Ein Menüpfad kompiliert eine gültige eingebettete `.topic`-Quelle. Ein zweiter
Pfad zeigt eine stabile Diagnose für ungültige Referenzen und übernimmt kein
Teilergebnis. Status und Ergebnisfenster nennen Quelle, Thema oder Fehler.
`Help -> Description` erklärt die Proof-Grenze.

Persistenz wird nur in einem vom Test erzeugten temporären Ordner bewiesen.
Normaler Start schreibt keine Datei. Traversal-Pfade werden abgelehnt. Die
moderne Implementierung ersetzt historische globale Puffer durch den
begrenzten `THelpSourceCompiler` aus Feature 018.

Barrierefreiheit: Compilerergebnis, Diagnose und Status sind textorientiert.
Alle demonstrierten Aktionen sind per Tastatur erreichbar.

## English

`TvHc` shows the managed help-source compiler with a visible result. Launch it
with:

```bash
dotnet run --project examples/TvHc
```

One menu path compiles a valid embedded `.topic` source. A second path shows a
stable diagnostic for an invalid reference and accepts no partial result. The
status and result window name the source, topic, or error.
`Help -> Description` explains the proof boundary.

Persistence is proven only inside a temporary directory created by the test.
Normal startup writes no file. Traversal paths are rejected. The modern
implementation replaces historical global buffers with the bounded
`THelpSourceCompiler` from Feature 018.

Accessibility: Compiler result, diagnostic, and status are text oriented. All
demonstrated actions are keyboard reachable.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~TvHc"
```
