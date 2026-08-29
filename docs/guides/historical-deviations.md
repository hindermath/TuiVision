# Bewusste historische Abweichungen / Intentional Historical Deviations

## Deutsch

### Zweck

TuiVision folgt der Absicht von Turbo Vision, bleibt aber moderner,
idiomatischer C#-Code. Dieser Wegweiser macht wichtige bewusste Abweichungen
auffindbar. Er ist kein neuer Konformitätsaudit und ersetzt nicht die
Feature-Evidence.

### Quellenrangfolge

1. Aktuelle TuiVision-Verträge sind die Produktnorm.
2. `magiblot/tvision` ist die gepinnte moderne, nicht normative Designreferenz.
3. Borland-Quellen und `tv203s/` erklären historische Absicht.
4. Free Vision und Terminal.GUI liefern unabhängige Vergleichsperspektiven.
5. `TVDEMOS/`, `TVFM/` und ausführbare Beispiele sind Consumer-Evidence.

Die vollständige Regel steht in der
[Drei-Achsen-Quellenpolicy](../source-reference-policy.md). Externe Checkouts
bleiben untracked; Quellkopie und Vendorisierung sind ausgeschlossen.

### Wichtige Abweichungsfamilien

| Bereich | Historische Absicht | Bewusste TuiVision-Abweichung | Evidence |
|---|---|---|---|
| Speicher und Lebensdauer | explizite Objekt- und Pufferverwaltung | verwalteter Speicher, `IDisposable` nur an echten Ressourcenrändern | [Core-Runtime-Konformität](core-runtime-conformance-hardening.md) |
| Plattformtreiber | getrennte DOS-/OS-Treiber und Hardwarezugriff | ein verwaltetes Capability-Modell mit ehrlichen Fallbacks | [Porting-Status](../porting-status.md), [Terminal-Guide](terminal-charset-hardening.md) |
| Zeichen und Fonts | Codepages und hardware-/dateinahe Fonts | Unicode-Ausgabe plus explizite KOI8-R- und 8x16-Fixtures | [Terminal- und Charset-Härtung](terminal-charset-hardening.md) |
| Hilfe | Borland-`.tch` und historische Streamformen | geschlossener, validierter `THelpFile`-Vertrag; `BHelp` dekodiert kein ungeprüftes proprietäres `.tch` | [BHelp](examples/bhelp.md), [Komponenten-/Datenkonformität](component-data-conformance-hardening.md) |
| Terminalbeispiele | Hostprozess, Shell oder Emulatorintegration | kontrollierte In-Process-Session ohne beliebige Shell oder PTY | [Terminal](examples/terminal.md), [ETerm](examples/eterm.md) |
| Serialisierung | C++-Objektstreams und historische Layouts | geschlossene Typregistries, strikte Grenzen und atomare Ablehnung | [Serialisierung](concepts/serialization.md) |
| Beispiele | zeit-, host- oder dateisystemabhängige Demonstrationen | feste Fixtures, test-eigene Pfade und textbasierte Fallbacks | [Beispiel-Lernpfade](example-learning-paths.md) |
| A11Y | Tastaturbedienung ohne moderne AT-Abstraktion | additive textbasierte Verträge; keine falsche native Bridge-Behauptung | [A11Y-Framework](a11y-framework.md) |

### Entscheidung lesen

Feature-Evidence verwendet genau eine Quellenentscheidung:
`AdoptModernization`, `PreserveHistoricalIntent`,
`IntentionalTuiVisionDeviation` oder `N/A`. Eine andere Quelltextform ist allein
kein Fehler. Relevant sind reproduzierbare Unterschiede im Produktvertrag,
Verhalten, Proof oder Lernzweck.

### Nächster Schritt

Beginne beim betroffenen Guide, folge dessen Evidence und prüfe erst dann die
read-only historische Quelle. Eine neue Abweichung gehört in das Feature, das
den Produktvertrag ändert; sie wird nicht still in diesem Wegweiser erfunden.

## English

### Purpose and source order

TuiVision follows Turbo Vision's intent while remaining modern, idiomatic C#.
This page makes important intentional deviations discoverable; it is not a new
conformance audit. Current TuiVision contracts are normative. The pinned
Magiblot revision is a modern non-normative reference, Borland and `tv203s/`
explain historical intent, Free Vision and Terminal.GUI are independent
comparisons, and the historical applications plus current examples are
consumer evidence.

### Main boundaries

The table above records the main families: managed lifetime, capability-based
drivers, explicit charset fixtures, validated help and serialization formats,
in-process terminal sessions, deterministic example data, and an additive
text-based accessibility layer. These choices preserve behavior and learning
value without copying C++ memory layout or DOS-specific integration.

### Reading a decision

Feature evidence ends in one of `AdoptModernization`,
`PreserveHistoricalIntent`, `IntentionalTuiVisionDeviation`, or `N/A`.
Different source syntax is not a defect by itself. Start with the affected
guide, follow its evidence, and inspect historical sources read-only only when
a concrete product or proof question remains.
