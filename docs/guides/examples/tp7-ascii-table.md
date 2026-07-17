# TP7 ASCII Table: Bytewerte untersuchen / Inspect Byte Values

## Zweck / Purpose

`Tp7AsciiTable` übernimmt den Lernzweck aus `TVDEMOS/ASCIITAB.PAS`: einen
Bytewert auswählen und seine dezimale, hexadezimale und druckbare Darstellung
gemeinsam sehen.

`Tp7AsciiTable` retains the learning purpose from `TVDEMOS/ASCIITAB.PAS`:
select a byte value and view its decimal, hexadecimal, and printable
representations together.

## Start / Launch

```bash
dotnet run --project examples/Tp7AsciiTable
```

```bash
dotnet run --no-build --configuration Release \
  --project examples/Tp7AsciiTable -- --smoke
```

## Showcase und Tastatur / Showcase and Keyboard

Eine fokussierbare 16x16-Tabelle zeigt jeden Bytewert als zweistellige
Hexadezimalzahl. Pfeiltasten bewegen die Auswahl um eine Zelle, PageUp und
PageDown um vier Zeilen, Home und End an die Grenzen. F1 beziehungsweise
`Help -> Description` erklärt Zweck und Proof-Grenze.

A focusable 16x16 table shows every byte as a two-digit hexadecimal value.
Arrow keys move the selection by one cell, PageUp and PageDown by four rows,
and Home and End to the boundaries. F1 or `Help -> Description` explains the
purpose and proof boundary.

Typisierte Commands können weiterhin direkt einen Wert von `0` bis `255`
wählen. Eine ungültige direkte Auswahl wird sichtbar abgelehnt und verändert
den letzten gültigen Wert nicht.

Typed commands can still select a value from `0` through `255` directly. An
invalid direct selection is visibly rejected and does not change the last
valid value.

## Moderne Abweichung / Modern Deviation

Nicht druckbare Werte erhalten ein textorientiertes `CTRL-nnn`-Label. Die
Umsetzung übernimmt keine historische Codepage oder Host-Zeichensatztabelle.

Non-printable values receive a text-first `CTRL-nnn` label. The implementation
does not retain a historical code page or host character table.

## Proof

Der primäre Smoke führt `app.Run()` aus und verbindet Zustand, Grid-Fokus,
Statuszeile und gerenderte Zellen. Die enge `52x22`-Fixture zeigt Identität,
erste und letzte Tabellenzelle sowie den Description-Pfad ohne Host-Codepage.

The primary smoke runs `app.Run()` and combines state, grid focus, status line,
and rendered cells. The constrained `52x22` fixture shows identity, first and
last table cells, and the Description path without a host code page.
