# Cyrillic Beispiel / Cyrillic Example

## Deutsch

`Cyrillic` zeigt kontrollierte KOI8-R-Bytes als Unicode-Zellen. Start:

```bash
dotnet run --project examples/Cyrillic
```

Die Hauptfläche beschriftet Quelle, Zielzeichen, Ergebnis und Begründung. Der
Menübefehl `Nächstes Mapping` durchläuft direkte Abbildung, Ersatzzeichen,
ungültigen Quellwert und nicht unterstützten Zeichensatz. Die Statuszeile und
`Help -> Description` bleiben per Tastatur erreichbar.

Die Abbildung verwendet die feste Framework-Tabelle. Sie liest oder verändert
weder Host-Locale noch Codepage, Font oder Tastaturbelegung. Damit bleibt das
Ergebnis auch in umgeleiteten und schmalen Terminals als Text prüfbar.

Historisch übernimmt die Demo den sichtbaren KOI8-R-/Cyrillic-Zweck der
Linux/X11-Beispiele. Root-Rechte, `/dev/vcsa`, Setup-Skripte und Hoständerungen
sind bewusst nicht Teil der modernen Portierung.

Host-Nachweis und Barrierefreiheit: Der automatisierte Nachweis ist
deterministisch und textorientiert; physische Hostbeobachtung bleibt getrennt.
Status, Mapping und Fallback sind per Tastatur erreichbar und nicht farbabhängig.

## English

`Cyrillic` shows controlled KOI8-R bytes as Unicode cells. Launch it with:

```bash
dotnet run --project examples/Cyrillic
```

The main area labels source, target glyph, outcome, and reason. `Next mapping`
cycles through direct mapping, replacement, invalid source value, and an
unsupported charset. The status line and `Help -> Description` remain keyboard
reachable.

Mapping uses the fixed framework table. It neither reads nor changes the host
locale, codepage, font, or keyboard map. The result therefore remains textually
reviewable in redirected and narrow terminals.

The demo retains the visible KOI8-R/Cyrillic purpose of the historical Linux/X11
examples. Root access, `/dev/vcsa`, setup scripts, and host mutation are
intentionally outside the modern port.

Host evidence and accessibility: Automated proof is deterministic and
text-first; physical host observation remains separate. Status, mapping, and
fallback are keyboard reachable and do not depend on color.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~CyrillicSmokeTests"
```
