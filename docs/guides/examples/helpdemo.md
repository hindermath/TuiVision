# HelpDemo Beispiel / HelpDemo Example

## Deutsch

`HelpDemo` verbindet Fokus, Hilfe-Kontext und Statushinweis. Start:

```bash
dotnet run --project examples/HelpDemo
```

Der Fokuswechsel zum Cancel-Control ändert Kontext und Hinweis gemeinsam. Der
Help-Befehl öffnet das passende Thema; ein unbekannter Kontext liefert einen
sichtbaren Fallback. `Help -> Description` fasst den Ablauf zusammen.

Die Implementierung nutzt vorhandene Controls, `THelpFile` und `THelpWindow`.
Mausaktivierung gehört nicht zu diesem Lauf und wird erst in Feature 020
geprüft. Der Tastaturpfad bleibt der vollständige Fallback.

Barrierefreiheit: Fokus, Kontext, Hinweis und Thema werden zusätzlich als Text
in der Statuszeile oder im Hilfefenster dargestellt.

## English

`HelpDemo` connects focus, help context, and status hint. Launch it with:

```bash
dotnet run --project examples/HelpDemo
```

Moving focus to the Cancel control changes context and hint together. The Help
command opens the matching topic; an unknown context provides visible fallback
content. `Help -> Description` summarizes the flow.

The implementation uses existing controls, `THelpFile`, and `THelpWindow`.
Mouse activation is outside this run and will be reviewed in Feature 020. The
keyboard path remains the complete fallback.

Accessibility: Focus, context, hint, and topic are also represented as text in
the status line or help window.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~HelpDemo"
```
