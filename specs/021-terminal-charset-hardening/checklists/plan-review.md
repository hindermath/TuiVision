# Plan Execution Review: Terminal and Charset Hardening

**Purpose**: Execute each review instruction before generating `tasks.md`.
**Reviewed**: 2026-07-12

- [X] **Trace scope**: Map FR-001..FR-030, CR-001..CR-016, and SC-001..SC-013 to plan phases; every item has an implementation, evidence, validation, or explicit exclusion boundary.
- [X] **Inspect project graph**: Confirm Drivers.Console references Core, Controls references Drivers.Console and Core, and Compatibility references Core only; the design introduces no cycle or new project reference.
- [X] **Inspect existing buffers**: Confirm `TConsoleCell`, `TConsoleBuffer`, Driver resize/presentation, and Controls draw-buffer APIs already provide the required cell and snapshot primitives.
- [X] **Inspect view integration**: Confirm existing Controls tests and example smokes prove `app.Run()`, view identity, status, and buffer/cell regions; `TTerminalView` can reuse that pattern without a new example.
- [X] **Inspect key boundary**: Confirm `TConsoleInputAdapter` already owns xterm-compatible key translation in Compatibility; tasks must not duplicate or move it.
- [X] **Inspect parser ownership**: Confirm no reusable terminal-session parser currently exists; Drivers.Console is the smallest non-visual owner and Controls-only ownership would block deterministic headless proof.
- [X] **Inspect font/profile boundary**: Confirm the repository uses `System.Text.Json` for project-owned JSON and that the planned raw 8x16 fixture avoids host font installation, historical generator execution, and arbitrary user paths.
- [X] **Inspect historical intent**: Locate terminal source/header, Cyrillic KOI8 material, font fixture/generator/collection sources, Eterm resources, and XTerm sources/docs; tasks must review them read-only.
- [X] **Inspect compile surface**: Tasks must validate imports, complete public XML documentation, test helpers, lifecycle ownership, cell geometry, and cross-assembly type identity before the first red command.
- [X] **Inspect negative proof**: Tasks must keep malformed/truncated/oversized/unsupported parser, charset, font, and profile outcomes explicit while grouping only project-local cases with one owner.
- [X] **Inspect validation triggers**: Shared Driver/Controls runtime changes require targeted tests, full Release, and coverage; public XML/guide/toc changes require DocFX followed by axe.
- [X] **Inspect delivery**: MergeAndSync authority is explicit; every remote task records acceptance in `pr-evidence.md` or the pre-named causal closeout path, and no empty closeout PR is allowed.

**Result**: 12/12 instructions executed. No corrective plan edit remains.
