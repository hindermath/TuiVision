# BHelp Beispiel / BHelp Example

## Deutsch

`BHelp` zeigt kontrollierte Hilfethemen in einem echten `THelpWindow`. Start:

```bash
dotnet run --project examples/BHelp
```

Das Menü öffnet das nächste Thema oder einen unbekannten Kontext. Der zweite
Pfad zeigt verständlichen Fallback-Inhalt statt eines leeren Fensters. Die
Statuszeile nennt Kontext und Thema; `Help -> Description` erklärt die Grenze.

Die historische Anwendung las ein proprietäres, ungeprüftes Borland-`.tch`-
Format. Diese Variante übernimmt Navigation und Kontextverhalten, lässt den
Binärdecoder aber bewusst aus. Sie verwendet den sicheren verwalteten
`THelpFile`-Vertrag aus Feature 018.

Barrierefreiheit: Titel, Inhalt, Fallback und Status sind textorientiert und
vollständig per Tastatur erreichbar.

## English

`BHelp` shows controlled help topics in a real `THelpWindow`. Launch it with:

```bash
dotnet run --project examples/BHelp
```

The menu opens the next topic or an unknown context. The second path shows clear
fallback content instead of an empty window. The status line names context and
topic; `Help -> Description` explains the boundary.

The historical application read a proprietary, unchecked Borland `.tch`
format. This version retains navigation and context behaviour but intentionally
omits that binary decoder. It uses the safe managed `THelpFile` contract from
Feature 018.

Accessibility: Titles, content, fallback, and status are text oriented and
fully keyboard reachable.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~BHelp"
```
