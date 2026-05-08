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

