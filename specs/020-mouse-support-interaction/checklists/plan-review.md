# Plan Execution Review: Mouse Support and Interaction

**Purpose**: Execute each review instruction before generating `tasks.md`.
**Reviewed**: 2026-07-12

- [X] **Trace scope**: Map FR-001..FR-026 and SC-001..SC-013 to plan sections; all have an implementation or evidence boundary.
- [X] **Inspect architecture**: Confirm project references allow Driver -> Core and Controls -> Driver without a cycle; current csproj graph supports the design.
- [X] **Inspect existing behavior**: Confirm `TEvent` already represents press/move/release/double-click and Controls already consume mouse down; no parallel event type is needed.
- [X] **Inspect drag choice**: Confirm `TWindow` already owns keyboard move mode and `WindowFlags.Move`; title drag is the smallest reusable slice.
- [X] **Inspect host claim**: Confirm `System.Console` has no current native mouse contract; native Windows remains unsupported and WSL uses terminal SGR.
- [X] **Inspect historical intent**: Locate `tevent.cc`, `tmouse.cc`, `tview.cc`, `twindow.cc`, `unix/xtermmouse.cc`, and matching headers; tasks must review them read-only.
- [X] **Inspect test placement**: Driver parser/state proof belongs in Drivers.Tests; focus/coordinates/drag in Controls.Tests; end-to-end loop in Controls.Tests or existing smoke infrastructure.
- [X] **Inspect compile surface**: Tasks must validate imports, public XML docs, harness helpers, focus ownership, and linked-source identity before first red command.
- [X] **Inspect validation triggers**: Shared runtime changes require full Release and coverage; public XML/guide/toc changes require DocFX then axe.
- [X] **Inspect delivery**: MergeAndSync authority is explicit; every remote task must update `pr-evidence.md`; no empty closeout PR.

**Result**: 10/10 instructions executed. No corrective plan edit remains.
