# Architekturvision Welle 2 / Architecture Vision Wave 2

## Kontext

Welle 2 portiert elf historische Controls- und Dialog-Beispiele als verwaltete
C#-Beispielprojekte. Die Beispiele zeigen vorhandene Framework-Bausteine in
laufbaren Anwendungen und ergaenzen nur dort Framework-Code, wo die historische
Beispielabsicht sonst nicht beweisbar ist.

Wave 2 ports eleven historical controls and dialogs examples as managed C#
example projects. The examples show existing framework building blocks in
runnable applications and only add framework code where the historical example
purpose cannot otherwise be proven.

## Kontextdiagramm

```text
                +-------------------------------+
                | examples/                     |
                | Clipboard ... TProgB          |
                +---------------+---------------+
                                |
                                v
+-------------------+   +-------+--------+   +----------------------+
| TuiVision.Core    |   | TuiVision      |   | TuiVision            |
| geometry/events   |   | .Controls      |   | .Serialization       |
+-------------------+   | dialogs/widgets|   | dlgdsn fixtures      |
                        +-------+--------+   +----------------------+
                                |
                                v
                +---------------+---------------+
                | tests/TuiVision.Examples      |
                | .SmokeTests                   |
                +---------------+---------------+
                                |
                                v
                +---------------+---------------+
                | docs/guides/examples/         |
                | DE-first / EN-second guides   |
                +-------------------------------+

Runtime launch also consumes TuiVision.Compatibility and
TuiVision.Drivers.Console for managed console execution.
```

## In Scope

- Eleven wave-2 examples: `clipboard`, `demo`, `dlgdsn`, `dyntxt`, `inplis`,
  `listvi`, `progba`, `sdlg`, `sdlg2`, `tcombo`, and `tprogb`.
- Deterministic in-process smoke tests for every wave-2 example.
- DE-first and EN-second learner guides at CEFR-B2 level.
- Lightweight architecture, security, A11Y, coverage, and statistics evidence.
- Managed `TScrollGroup` as reusable foundation for `sdlg` and `sdlg2`.

## Out Of Scope

- Editor, help, stream, terminal-emulation, runtime mouse, and real charset
  effect acceptance.
- File-content read/write/save/delete/overwrite behavior inside standard-dialog
  acceptance.
- Wave-3, wave-4, wave-5, or wave-6 examples.
- Native bindings or a one-to-one recreation of historical build helper files.

## 012 Interactive Showcase Impact

Die interaktive Showcase-Stufe `012-interactive-wave2-demos` fuegt keine neue
Framework-Schicht hinzu. Die elf vorhandenen Wave-2-Beispiele erhalten
beispiel-lokale Command-IDs, Menueeintraege, sichtbare Zweck-/Feedback-Texte
und deterministische Event-Queues fuer Smoke-Tests. Direkte Hilfsmethoden aus
011 bleiben als Setup oder ergaenzende Assertions erhalten, sind aber nicht
mehr der primaere Akzeptanzpfad.

The interactive showcase stage `012-interactive-wave2-demos` adds no new
framework layer. The eleven existing Wave 2 examples receive example-local
command IDs, menu entries, visible purpose/feedback text, and deterministic
event queues for smoke tests. Direct helpers from 011 remain as setup or
supplemental assertions, but they are no longer the primary acceptance path.

## 013 Visual Remediation Impact

013 fuegt keine neue oeffentliche Framework-Schicht hinzu. Die neue
`examples/Shared/Wave2Runtime.cs`-Datei wird nur in die elf Beispielprojekte
gelinkt und bleibt beispielinterne Runtime-Hilfe fuer Statuszeile,
Beschreibungspfad und absolute Buffer-Regionen. Die Architekturgrenze bleibt:
Beispiele nutzen vorhandene Controls; `src/`-Framework-APIs werden nicht
ausgeweitet.

013 adds no new public framework layer. The new
`examples/Shared/Wave2Runtime.cs` file is linked only into the eleven example
projects and remains example-internal runtime glue for status line,
description path, and absolute buffer regions. The architecture boundary
remains: examples use existing controls; `src/` framework APIs are not
expanded.
