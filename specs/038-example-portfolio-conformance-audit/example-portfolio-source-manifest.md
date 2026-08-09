# Quellenmanifest / Source Manifest

Kanonische Wahrheit: [example-portfolio-audit.json](example-portfolio-audit.json).

Das Manifest ist für den vollständigen Broad-Review-Slice populationiert. Alle
Pfade sind repository-relativ, alle historischen und akzeptierten
Vorgängerartefakte sind mit lowercase SHA-256 gebunden, und jede Vorwärts- hat
eine Rückrelation. Quelltext wurde ausschließlich gelesen.

*The manifest is populated for the complete broad-review slice. Every path is
repository-relative, every historical and accepted predecessor artifact is
bound by lowercase SHA-256, and every forward relation has a reverse relation.
Source code was read only.*

| ID-Familie | Authority | Anzahl | Grenze / Boundary |
|---|---|---:|---|
| `BASE-E001`–`BASE-E016` | TuiVisionEvidence | 16 | Akzeptierte Features 012–037; keine neue Remote-Behauptung |
| `TV203-E001`–`TV203-E081` | HistoricalTV203 | 81 | C/C++-/Header-/Asset-Absicht in eigenen Worten |
| `TVDEMOS-E001`–`TVDEMOS-E017` | HistoricalTVDEMOS | 17 | Pascal-/Help-Absicht in eigenen Worten |
| `TVFM-E001`–`TVFM-E024` | HistoricalTVFM | 24 | Exakt die Feature-037-gebundene TVFM-Menge |
| `EVD001`–`EVD128` | Local evidence | 128 | Entry, Guide, Feature/Closure und ausführbare Smoke-Evidence |

Die 138 Source-Knoten sind nach Authority-Präfix und Pfad ordinal gebunden.
Tutorial nennt `tvguid01.cc` bis `tvguid16.cc`; EX036 nennt exakt 24
TVFM-Dateien; EX037 besitzt bewusst keine historische Source-ID. Die
akzeptierten Vergleichspins bleiben Free Vision
`ffc03b34d8cafb85ddcf0686de1c5551601dacb2`, Terminal.GUI `v1.9.0` /
`d5abc2001fb2c5be4d16b23bbf34dfd99e752ea3` und magiblot
`57b6f56b38e0ee75240a80a10ee0e11470c24693`; keine Vergleichsbasis ist
Produktnorm.

*The 138 source nodes are ordinal by authority prefix and path. Tutorial binds
all sixteen steps, EX036 binds exactly 24 TVFM files, and EX037 intentionally
has no historical source. Comparison pins remain secondary evidence, never a
product norm.*
