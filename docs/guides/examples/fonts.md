# Fonts Beispiel / Fonts Example

## Deutsch

`Fonts` zeigt eine kontrollierte rohe 8x16-Font-Fixture als Textmatrix. Start:

```bash
dotnet run --project examples/Fonts
```

Die Hauptfläche nennt Breite, Höhe, Glyphenzahl, Datenlänge und ausgewählte
Glyphe. `Nächste Glyphe` wechselt die Auswahl. `Help -> Description` erklärt die
Grenze. Gesetzte Pixel erscheinen als `#`, leere Pixel als `.`, sodass Farbe
keine Bedeutung tragen muss.

Die 4.096-Byte-Fixture ist eine unveränderte, projektkontrollierte Kopie der
historischen `font.016`. Die Demo installiert keinen Font, startet keinen
Generator und schreibt keine Datei. Falsche Länge, Geometrie, Stride, Quelle,
Format oder eine leere Glyphe führen zu einem sichtbaren `Fallback`.

Host-Nachweis und Barrierefreiheit: Der Nachweis liest nur die kontrollierte
Fixture. Physische Fontdarstellung bleibt außerhalb des Claims. Auswahl,
Status, Beschreibung und Fallback sind per Tastatur und als Text erreichbar.

## English

`Fonts` shows a controlled raw 8x16 font fixture as a text matrix. Launch it with:

```bash
dotnet run --project examples/Fonts
```

The main area names width, height, glyph count, data length, and selected glyph.
`Next glyph` changes the selection. `Help -> Description` explains the boundary.
Set pixels use `#` and empty pixels use `.`, so meaning does not depend on color.

The 4,096-byte fixture is an unchanged project-controlled copy of the historical
`font.016`. The demo installs no font, runs no generator, and writes no file.
Wrong length, geometry, stride, source, format, or a blank glyph produces a
visible `Fallback`.

Host evidence and accessibility: Proof reads only the controlled fixture.
Physical font rendering remains outside the claim. Selection, status,
description, and fallback are keyboard reachable and textual.

## Nachweis / Proof

```bash
dotnet test tests/TuiVision.Examples.SmokeTests/ --filter "FullyQualifiedName~FontsSmokeTests"
```
