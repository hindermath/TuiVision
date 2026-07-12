# Terminal- und Charset-Härtung / Terminal and Charset Hardening

## Zweck / Purpose

Feature 021 stellt einen kleinen, kontrollierten Frameworkvertrag für spätere
Terminalbeispiele bereit. Die Sitzung läuft vollständig im Prozess. Sie startet
keine Shell, keinen Hostprozess und kein PTY. Dadurch sind Zustand, Cursor,
Farben, Verlauf und sichtbare Zellen auf macOS, Linux und Windows/WSL gleich
prüfbar.

Feature 021 provides a small controlled framework contract for later terminal
examples. The session runs entirely in process. It does not start a shell, host
process, or PTY. This makes state, cursor, colors, history, and visible cells
equally testable on macOS, Linux, and Windows/WSL.

## Komponenten / Components

| Komponente / Component | Verantwortung / Responsibility |
|---|---|
| `TerminalSession` | Sichtbare Cells, Cursor, Farben, Verlauf, Reset, Lifecycle und begrenzte Steuerfolgen / Visible cells, cursor, colors, history, reset, lifecycle, and bounded control sequences |
| `TerminalCharsetMapper` | Hostunabhängiges Unicode- und KOI8-R-Mapping / Host-independent Unicode and KOI8-R mapping |
| `BitmapFontFixture` | Exakte rohe 8x16-Fixture als Metadatenbeweis / Exact raw 8x16 fixture as metadata proof |
| `TerminalProfile` | Geschlossenes JSON-Schema, Defaults und Capability-Fallback / Closed JSON schema, defaults, and capability fallback |
| `TTerminalView` | Projektion in den View-Baum, Tastatureingabe, Status und Cell-Proof / Projection into the view tree, keyboard input, status, and cell proof |

Die Verantwortung bleibt im Framework. Spätere Beispiele dürfen diese Logik
nicht als lokale Parser-, Mapper-, Font- oder Profilkopie neu anlegen.

Responsibility remains in the framework. Later examples must not recreate this
logic as local parser, mapper, font, or profile copies.

## Unterstütztes Subset / Supported Subset

Die Sitzung unterstützt normalen Text sowie die C0-Aktionen BEL, Backspace,
Tab, Carriage Return und Line Feed. Das CSI-Subset umfasst:

- relative Cursorbewegung: `A`, `B`, `C`, `D`
- absolute Position: `H`, `f`
- Display- und Zeilenlöschung: `J`, `K`, Modi 0 bis 2
- 16 Vorder- und Hintergrundfarben sowie Attributreset: `m`
- vollständiger Sitzungsreset: `ESC c`

The session supports plain text plus the C0 actions BEL, backspace, tab,
carriage return, and line feed. The CSI subset covers:

- relative cursor movement: `A`, `B`, `C`, `D`
- absolute position: `H`, `f`
- display and line erase: `J`, `K`, modes 0 through 2
- 16 foreground and background colors plus attribute reset: `m`
- full session reset: `ESC c`

Andere ANSI-, VT100-, XTerm- oder Eterm-Funktionen sind `Unsupported`. Das ist
eine bewusste Sicherheits- und Wartungsgrenze, kein Anspruch auf vollständige
Terminalemulation.

Other ANSI, VT100, XTerm, or Eterm functions are `Unsupported`. This is a
deliberate security and maintenance boundary, not a claim of complete terminal
emulation.

## Feste Grenzen / Fixed Limits

| Grenze / Limit | Wert / Value | Verhalten / Behavior |
|---|---:|---|
| Verlauf / History | 4.096 Cells | FIFO; älteste Cell wird zuerst entfernt / FIFO; oldest cell is removed first |
| Steuerfolge / Control sequence | 64 Zeichen / characters | Überlänge wird atomar abgelehnt / Oversize is rejected atomically |
| Numerische Parameter / Numeric parameters | 4 | Fünfter Parameter wird abgelehnt / Fifth parameter is rejected |
| Parameterwert / Parameter value | 0 bis / through 9.999 | 10.000 wird abgelehnt / 10,000 is rejected |

Eine ungültige oder nicht unterstützte Folge verändert keine Cell, keinen
Cursor und kein Attribut. Die nächste unabhängige gültige Eingabe bleibt
nutzbar.

An invalid or unsupported sequence changes no cell, cursor, or attribute. The
next independent valid input remains usable.

## Charset-Mapping / Character-Set Mapping

Unicode ist die kanonische Darstellung. KOI8-R ist der einzige historische
Bytevertrag in Feature 021. Eine isolierte ungültige UTF-16-Einheit oder eine
nicht abbildbare Einheit erhält ausschließlich `U+FFFD`. Andere Codepages
werden nicht über Locale oder `Encoding.Default` erraten.

Unicode is the canonical representation. KOI8-R is the only historical byte
contract in Feature 021. An isolated invalid UTF-16 unit or an unmappable unit
receives only `U+FFFD`. Other codepages are not guessed through locale or
`Encoding.Default`.

Die feste KOI8-R-Tabelle macht Tests unabhängig von installierten Host-Codecs.
Sie übernimmt die historische Lernabsicht, ohne die damalige Console-Codepage
zu ändern.

The fixed KOI8-R table makes tests independent of installed host codecs. It
retains the historical learning intent without changing the former console
codepage.

## Font-Fixture / Font Fixture

Der erste Vertrag akzeptiert genau:

- Breite 8, Höhe 16
- 256 Glyphen
- 16 Bytes pro Glyphe
- 4.096 rohe, unkomprimierte Bytes
- eine kontrollierte repository- oder testrelative Quellenkennung

The first contract accepts exactly:

- width 8, height 16
- 256 glyphs
- 16 bytes per glyph
- 4,096 raw uncompressed bytes
- a controlled repository- or test-relative source identifier

Die Bytes sind Metadaten für Glyphenzeilen und Nachweise. TuiVision installiert
damit keinen Host-Font und führt den historischen Generator nicht aus. PSF,
SFT, gzip und beliebige Nutzerpfade bleiben außerhalb dieses Features.

The bytes are metadata for glyph rows and proof. TuiVision does not install a
host font with them and does not run the historical generator. PSF, SFT, gzip,
and arbitrary user paths remain outside this feature.

## Profile und Fallback / Profiles and Fallback

Ein Profil verwendet ein geschlossenes JSON-Schema:

```json
{
  "ProfileId": "koi8-proof",
  "Charset": "KOI8-R",
  "FontId": "built-in-8x16",
  "Foreground": "Gray",
  "Background": "Black"
}
```

`ProfileId` und `Charset` sind erforderlich. `FontId`, `Foreground` und
`Background` sind optional. Fehlende optionale Werte verwenden den eingebauten
8x16-Font, Grau und Schwarz.

`ProfileId` and `Charset` are required. `FontId`, `Foreground`, and
`Background` are optional. Missing optional values use the built-in 8x16 font,
gray, and black.

Unbekannte oder doppelte Keys, malformed JSON, falsche Typen und ungültige
Pflichtwerte lehnen das gesamte Profil ab. Eine syntaktisch gültige, aber nicht
verfügbare Font- oder Host-Capability verwendet dagegen den sicheren Default
und meldet `Unsupported` mit angefordertem und effektivem Wert. Schemafehler und
Capability-Fallback werden dadurch nicht vermischt.

Unknown or duplicate keys, malformed JSON, wrong types, and invalid required
values reject the complete profile. A syntactically valid but unavailable font
or host capability instead uses the safe default and reports `Unsupported` with
requested and effective values. Schema errors and capability fallback therefore
remain separate.

## View-, Tastatur- und A11Y-Vertrag / View, Keyboard, and A11Y Contract

`TTerminalView` zeigt Session-Cells, den Cursor über Farbumkehr und eine
textorientierte Statuszeile. Der Status priorisiert Profilkennung und
Capability, damit `Unsupported` auch in einer schmalen View sichtbar bleibt.
Profil, Charset und effektiver Font sind Text und nicht nur Farbe oder Layout.

`TTerminalView` shows session cells, the cursor through color inversion, and a
text-first status row. The status prioritizes profile identity and capability
so `Unsupported` remains visible in a narrow view. Profile, charset, and
effective font are text, not color or layout alone.

Druckbare Zeichen, Backspace, Tab, CR/LF und Pfeiltasten werden über den
Driver-owned Session-Vertrag verarbeitet. Die vorhandene
`TConsoleInputAdapter`-Übersetzung bleibt der Compatibility-Vertrag; die View
führt keinen zweiten XTerm-Key-Parser ein. Quit und andere Shell-Befehle bleiben
bei `TApplication`.

Printable characters, backspace, tab, CR/LF, and arrow keys are processed
through the Driver-owned session contract. Existing `TConsoleInputAdapter`
translation remains the Compatibility contract; the view adds no second XTerm
key parser. Quit and other shell commands remain with `TApplication`.

## Plattform- und Proof-Grenzen / Platform and Proof Boundaries

Deterministische In-Process-Tests sind auf allen Hosts der Primärnachweis.
Remote-CI und physische Terminalbeobachtung bleiben getrennte Evidence-Klassen.
Ein macOS-, Linux- oder Windows/WSL-Label beweist nicht automatisch ein echtes
interaktives Terminal. Nicht verfügbare physische Prüfungen bleiben `NotRun`.

Deterministic in-process tests are the primary proof on every host. Remote CI
and physical terminal observation remain separate evidence classes. A macOS,
Linux, or Windows/WSL label does not automatically prove a real interactive
terminal. Unavailable physical checks remain `NotRun`.

Der Nachweis verändert keine Host-Fonts, Keyboardmaps, Codepages,
Terminalprofile, Fensterfarben oder Audioeinstellungen. Er liest keine
beliebigen Nutzerdateien und startet keine externen Prozesse.

The proof changes no host fonts, keyboard maps, codepages, terminal profiles,
window colors, or audio settings. It reads no arbitrary user files and starts
no external processes.

## Historische Abweichung / Historical Deviation

Die historischen Beispiele nutzten Ringpuffer, Shells, Console-Geräte,
`consolechars`, `loadkeys`, XTerm-/Eterm-Ressourcen und echte Fontänderungen.
Feature 021 übernimmt den Zweck, Terminaltext, KOI8-R, 8x16-Glyphen und
Capability-Grenzen zu erklären. Es übernimmt nicht die Hostmanipulation oder
eine mechanische C/C++-Portierung.

The historical examples used ring buffers, shells, console devices,
`consolechars`, `loadkeys`, XTerm/Eterm resources, and real font changes.
Feature 021 retains the purpose of explaining terminal text, KOI8-R, 8x16
glyphs, and capability boundaries. It does not retain host mutation or perform
a mechanical C/C++ port.

## Verifikation / Verification

```bash
dotnet test tests/TuiVision.Drivers.Tests/TuiVision.Drivers.Tests.csproj --configuration Release
dotnet test tests/TuiVision.Controls.Tests/TuiVision.Controls.Tests.csproj --configuration Release
dotnet test tests/TuiVision.Compatibility.Tests/TuiVision.Compatibility.Tests.csproj --configuration Release
```

Öffentliche XML-Dokumentation und dieser Guide lösen zusätzlich DocFX sowie den
Playwright-/axe-A11Y-Pfad aus. Der vollständige Lauf und die genauen Grenzen
stehen in `specs/021-terminal-charset-hardening/pr-evidence.md`.

Public XML documentation and this guide also trigger DocFX plus the
Playwright/axe A11Y path. The complete run and exact boundaries are recorded in
`specs/021-terminal-charset-hardening/pr-evidence.md`.
