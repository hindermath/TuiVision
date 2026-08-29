# Architekturüberblick / Architecture Overview

## Deutsch

### Lernziel

Dieser Guide zeigt, wo eine Änderung fachlich hingehört. TuiVision übernimmt
die Absicht von Turbo Vision, bleibt aber eine moderne, verwaltete C#-
Interpretation und keine zeilenweise Übersetzung.

### Schichten

| Schicht | Verantwortung | Typische Beispiele |
|---|---|---|
| `TuiVision.Core` | Werte, Ereignisse, Zellen und Puffer ohne UI-Shell | `TEvent`, `TPoint`, `TRect`, `TConsoleBuffer` |
| `TuiVision.Controls` | Views, Gruppen, Fokus, Dialoge, Menüs und Anwendungsschleife | `TView`, `TGroup`, `TDialog`, `TProgram` |
| `TuiVision.Serialization` | geschlossene, deterministische Persistenzverträge | Archive, Help und Resources |
| `TuiVision.Compatibility` | begrenzte historische Kompatibilitätsformen | Stream- und Typadapter |
| `TuiVision.Drivers.Console` | Host-Capabilities, Ein-/Ausgabe und Fallbacks | Console Driver und Terminal-Ingress |
| `examples/` | Lernende Consumer; keine zweite Framework-Schicht | sichtbare Anwendungen und Smokes |

Abhängigkeiten zeigen nach unten. Wiederverwendbare Logik gehört nicht dauerhaft
in ein Beispiel. Ein Beispiel darf aber eine begrenzte Komposition besitzen,
wenn die Evidence sie als `IntentionalDeviation` ausweist.

### Laufzeitpfad

```text
Host input -> TEvent -> TProgram -> TGroup/View tree -> state change
                                              |
                                              v
                                    buffers/cells -> presenter
```

Die Darstellung ist absichtlich textuell. Sie zeigt keine Nebenläufigkeit:
Ereignisverarbeitung, Zustandsänderung und der nächste Draw-Schritt bilden den
geordneten Hauptpfad.

### Quellen und Modernisierung

Aktuelle TuiVision-Verträge sind die Produktnorm. Historische Quellen unter
`tv203s/` erklären ursprüngliche Absicht. Magiblot/tvision, Free Vision und
Terminal.GUI sind unabhängige, nicht normative Vergleiche. Die verbindliche
Reihenfolge und erlaubten Entscheidungen stehen in der
[Quellenreferenz-Policy](../source-reference-policy.md).

### Übung

Ordne `TStatusLine`, `TResourceFile` und `TConsoleDriver` jeweils einer Schicht
zu. Prüfe anschließend in den Projektdateien, ob ihre Abhängigkeiten der
Richtung entsprechen. Nächster Schritt: [Event-Loop](concepts/event-loop.md).

## English

### Learning goal

This guide shows where a change belongs. TuiVision preserves Turbo Vision's
intent while remaining a modern managed C# interpretation, not a line-by-line
translation.

### Layers and runtime path

The table and text diagram above are the normative learner overview. Core owns
values and cells; Controls owns interaction; Serialization owns bounded stored
data; Compatibility contains limited adapters; Drivers.Console owns host
capabilities; examples are consumers rather than a second framework.

Dependencies point downwards. Reusable behavior must not remain duplicated in
examples. Current TuiVision contracts are normative, while historical and
external projects keep the distinct source roles described in the linked
policy.

### Exercise

Assign `TStatusLine`, `TResourceFile`, and `TConsoleDriver` to their layers,
then inspect the project references. Continue with the event-loop guide.
