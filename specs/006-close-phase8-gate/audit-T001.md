# Audit: 006-close-phase8-gate Setup Inventory

**Branch**: `006-close-phase8-gate` | **Datum / Date**: 2026-03-27

---

## T001: Ledger-Bestand / Ledger Inventory

### Gesamtzahl der Ledger-Zeilen / Total ledger rows

- Total `.cc` rows in `docs/porting-status.md`: **151**
- `portiert + getestet`: **18**
- `portiert + Test ausstehend`: **103**
- `bewusst ausgelassen + Begruendung`: **30**

### Alle Zeilen im Status `portiert + Test ausstehend` / All rows in provisional state (103 rows)

**Gemeinsame Framework-Dateien / Shared framework files:**

1. tv203s/contrib/tvision/classes/codepage.cc
2. tv203s/contrib/tvision/classes/configfile.cc
3. tv203s/contrib/tvision/classes/fpbase.cc
4. tv203s/contrib/tvision/classes/fpstream.cc
5. tv203s/contrib/tvision/classes/help.cc
6. tv203s/contrib/tvision/classes/helpbase.cc
7. tv203s/contrib/tvision/classes/ifpstrea.cc
8. tv203s/contrib/tvision/classes/iopstrea.cc
9. tv203s/contrib/tvision/classes/ipstream.cc
10. tv203s/contrib/tvision/classes/ofpstrea.cc
11. tv203s/contrib/tvision/classes/opstream.cc
12. tv203s/contrib/tvision/classes/osclipboard.cc
13. tv203s/contrib/tvision/classes/pstream.cc
14. tv203s/contrib/tvision/classes/tapplica.cc
15. tv203s/contrib/tvision/classes/tbackgro.cc
16. tv203s/contrib/tvision/classes/tbutton.cc
17. tv203s/contrib/tvision/classes/tchdirdi.cc
18. tv203s/contrib/tvision/classes/tcheckbo.cc
19. tv203s/contrib/tvision/classes/tclrdisp.cc
20. tv203s/contrib/tvision/classes/tcluster.cc
21. tv203s/contrib/tvision/classes/tcollect.cc
22. tv203s/contrib/tvision/classes/tcolordi.cc
23. tv203s/contrib/tvision/classes/tcolorgr.cc
24. tv203s/contrib/tvision/classes/tcolorit.cc
25. tv203s/contrib/tvision/classes/tcolorse.cc
26. tv203s/contrib/tvision/classes/tcommand.cc
27. tv203s/contrib/tvision/classes/tdesktop.cc
28. tv203s/contrib/tvision/classes/tdialog.cc
29. tv203s/contrib/tvision/classes/tdircoll.cc
30. tv203s/contrib/tvision/classes/tdirlist.cc
31. tv203s/contrib/tvision/classes/teditor.cc
32. tv203s/contrib/tvision/classes/teditorf.cc
33. tv203s/contrib/tvision/classes/teditwin.cc
34. tv203s/contrib/tvision/classes/tfilecol.cc
35. tv203s/contrib/tvision/classes/tfiledia.cc
36. tv203s/contrib/tvision/classes/tfileedi.cc
37. tv203s/contrib/tvision/classes/tfileinf.cc
38. tv203s/contrib/tvision/classes/tfileinp.cc
39. tv203s/contrib/tvision/classes/tfilelis.cc
40. tv203s/contrib/tvision/classes/tfilterv.cc
41. tv203s/contrib/tvision/classes/tframe.cc
42. tv203s/contrib/tvision/classes/tgkey.cc
43. tv203s/contrib/tvision/classes/tgroup.cc
44. tv203s/contrib/tvision/classes/thistory.cc
45. tv203s/contrib/tvision/classes/thistvie.cc
46. tv203s/contrib/tvision/classes/thistwin.cc
47. tv203s/contrib/tvision/classes/thwmouse.cc
48. tv203s/contrib/tvision/classes/tindicat.cc
49. tv203s/contrib/tvision/classes/tinputli.cc
50. tv203s/contrib/tvision/classes/tlabel.cc
51. tv203s/contrib/tvision/classes/tlistbox.cc
52. tv203s/contrib/tvision/classes/tlistvie.cc
53. tv203s/contrib/tvision/classes/tmemo.cc
54. tv203s/contrib/tvision/classes/tmenubar.cc
55. tv203s/contrib/tvision/classes/tmenubox.cc
56. tv203s/contrib/tvision/classes/tmenuvie.cc
57. tv203s/contrib/tvision/classes/tmonosel.cc
58. tv203s/contrib/tvision/classes/tmouse.cc
59. tv203s/contrib/tvision/classes/tnscolle.cc
60. tv203s/contrib/tvision/classes/tnssorte.cc
61. tv203s/contrib/tvision/classes/tpalette.cc
62. tv203s/contrib/tvision/classes/tparamte.cc
63. tv203s/contrib/tvision/classes/tprogini.cc
64. tv203s/contrib/tvision/classes/tprogram.cc
65. tv203s/contrib/tvision/classes/tradiobu.cc
66. tv203s/contrib/tvision/classes/trangeva.cc
67. tv203s/contrib/tvision/classes/trescoll.cc
68. tv203s/contrib/tvision/classes/tresfile.cc
69. tv203s/contrib/tvision/classes/tscrollb.cc
70. tv203s/contrib/tvision/classes/tscrolle.cc
71. tv203s/contrib/tvision/classes/tsortedc.cc
72. tv203s/contrib/tvision/classes/tsortedl.cc
73. tv203s/contrib/tvision/classes/tstatict.cc
74. tv203s/contrib/tvision/classes/tstatusd.cc
75. tv203s/contrib/tvision/classes/tstatusl.cc
76. tv203s/contrib/tvision/classes/tstrinde.cc
77. tv203s/contrib/tvision/classes/tstringc.cc
78. tv203s/contrib/tvision/classes/tstringl.cc
79. tv203s/contrib/tvision/classes/tstrlist.cc
80. tv203s/contrib/tvision/classes/tstrmcla.cc
81. tv203s/contrib/tvision/classes/tstrmtyp.cc
82. tv203s/contrib/tvision/classes/tsubmenu.cc
83. tv203s/contrib/tvision/classes/tvalidat.cc
84. tv203s/contrib/tvision/classes/tvedit1.cc
85. tv203s/contrib/tvision/classes/tvedit2.cc
86. tv203s/contrib/tvision/classes/tvedit3.cc
87. tv203s/contrib/tvision/classes/tvintl.cc
88. tv203s/contrib/tvision/classes/tvtext1.cc
89. tv203s/contrib/tvision/classes/tvtext2.cc
90. tv203s/contrib/tvision/classes/twindow.cc

**Plattformtreiber-Dateien / Platform driver files:**

91. tv203s/contrib/tvision/classes/linux/linuxkey.cc
92. tv203s/contrib/tvision/classes/linux/linuxmouse.cc
93. tv203s/contrib/tvision/classes/unix/unixkey.cc
94. tv203s/contrib/tvision/classes/unix/unixmouse.cc
95. tv203s/contrib/tvision/classes/unix/xtermkey.cc
96. tv203s/contrib/tvision/classes/unix/xtermmouse.cc
97. tv203s/contrib/tvision/classes/win32/win32clip.cc
98. tv203s/contrib/tvision/classes/win32/win32key.cc
99. tv203s/contrib/tvision/classes/win32/win32mouse.cc
100. tv203s/contrib/tvision/classes/winnt/winntkey.cc
101. tv203s/contrib/tvision/classes/winnt/winntmouse.cc

**Hinweis**: Zeilen 91–101 haben die Treiber als Primärziel
(`TuiVision.Drivers.Console/TConsoleDriver.cs`), sind aber noch
nicht vollständig getestet. Zeilen 97–101 sind also driver-basierte
`portiert + Test ausstehend`-Einträge.
*(Note: rows 91–101 map to driver targets but are still provisional.)*

### Nicht-Treiber `geplant`-Zeilen / Non-driver `geplant` target rows (29 rows)

These rows have a primary target containing `(geplant)` and are NOT pointing
to `TuiVision.Drivers.Console`:

1. tv203s/contrib/tvision/classes/configfile.cc — TuiVision.Controls (geplant)
2. tv203s/contrib/tvision/classes/osclipboard.cc — TuiVision.Controls (geplant)
3. tv203s/contrib/tvision/classes/tclrdisp.cc — TuiVision.Controls (geplant)
4. tv203s/contrib/tvision/classes/tcollect.cc — TuiVision.Core (geplant)
5. tv203s/contrib/tvision/classes/tcolordi.cc — TuiVision.Controls (geplant)
6. tv203s/contrib/tvision/classes/tcolorgr.cc — TuiVision.Controls (geplant)
7. tv203s/contrib/tvision/classes/tcolorit.cc — TuiVision.Controls (geplant)
8. tv203s/contrib/tvision/classes/tcolorse.cc — TuiVision.Controls (geplant)
9. tv203s/contrib/tvision/classes/tfileinf.cc — TuiVision.Controls (geplant)
10. tv203s/contrib/tvision/classes/tfilterv.cc — TuiVision.Controls (geplant)
11. tv203s/contrib/tvision/classes/tgkey.cc — TuiVision.Compatibility (geplant)
12. tv203s/contrib/tvision/classes/tmonosel.cc — TuiVision.Controls (geplant)
13. tv203s/contrib/tvision/classes/tnscolle.cc — TuiVision.Core (geplant)
14. tv203s/contrib/tvision/classes/tnssorte.cc — TuiVision.Core (geplant)
15. tv203s/contrib/tvision/classes/tpalette.cc — TuiVision.Controls (geplant)
16. tv203s/contrib/tvision/classes/tparamte.cc — TuiVision.Controls (geplant)
17. tv203s/contrib/tvision/classes/tprogini.cc — TuiVision.Controls (geplant)
18. tv203s/contrib/tvision/classes/trangeva.cc — TuiVision.Controls (geplant)
19. tv203s/contrib/tvision/classes/tsortedc.cc — TuiVision.Core (geplant)
20. tv203s/contrib/tvision/classes/tsortedl.cc — TuiVision.Core (geplant)
21. tv203s/contrib/tvision/classes/tstrinde.cc — TuiVision.Core (geplant)
22. tv203s/contrib/tvision/classes/tstringc.cc — TuiVision.Core (geplant)
23. tv203s/contrib/tvision/classes/tstrlist.cc — TuiVision.Core (geplant)
24. tv203s/contrib/tvision/classes/tvalidat.cc — TuiVision.Controls (geplant)
25. tv203s/contrib/tvision/classes/tvintl.cc — TuiVision.Compatibility (geplant)
26. tv203s/contrib/tvision/classes/tvtext1.cc — TuiVision.Controls (geplant)
27. tv203s/contrib/tvision/classes/tvtext2.cc — TuiVision.Controls (geplant)
28. tv203s/contrib/tvision/classes/twindow.cc — TuiVision.Controls (geplant)
29. tv203s/contrib/tvision/classes/win32/win32clip.cc — TuiVision.Controls (geplant)

### Baseline-Abdeckung / Baseline Coverage

Run: `dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"`

Result: The resulting `coverage.cobertura.xml` does NOT contain any mention
of `TuiVision.Compatibility`. The assembly is not referenced by
`TuiVision.Controls.Tests` and thus receives **0 % transitive line coverage**
from the Controls test run. A dedicated `TuiVision.Compatibility.Tests`
project is therefore mandatory to reach the 70 % gate.

---

## T002: Modulverantwortlichkeits-Prüfung / Gate Module Responsibility Audit

### `src/TuiVision.Compatibility/Class1.cs`

**Status**: ECHTES FRAMEWORK-MODUL / REAL FRAMEWORK MODULE

The file (`Class1.cs` is just the filename but contains real production code):
- `TShiftState` enum: modifier key bit mask (Shift/Ctrl/Alt flags)
- `TKeyCodeTranslator` static class: maps `ConsoleKeyInfo` to TV key codes
  via `FromConsoleKey()`, `ComposeKeyCode()`, `IsPrintable()`, and
  `MapScanCode()` (scan codes for 24 keys defined)
- The module has real, non-trivial code with XML documentation

**Conclusion**: No restructuring needed. `TuiVision.Compatibility` carries
real gate responsibility. However, no dedicated test project yet exists — this
is the gap that T006 must address.

### `src/TuiVision.Drivers.Console/Class1.cs`

**Status**: BEWUSSTER STUB / DELIBERATE REDIRECT STUB

Content is comment-only: "This file is intentionally left empty.
TConsoleCell and TConsoleBuffer have been moved to TuiVision.Core.
TConsoleDriver and IConsolePresenter have been moved to TConsoleDriver.cs."

This is correct per T019 contract: `Class1.cs` documents a deliberate
relocation and must remain comment-only. Real code lives in `TConsoleDriver.cs`.

### `src/TuiVision.Drivers.Console/TConsoleDriver.cs`

**Status**: ECHTES FRAMEWORK-MODUL / REAL FRAMEWORK MODULE

Contains `IConsolePresenter` interface and the start of `TConsoleDriver` class.
Real production code; not placeholder-only.

### `src/TuiVision.Drivers.Console/DriverCapabilityMap.cs`

**Status**: ECHTES FRAMEWORK-MODUL / REAL FRAMEWORK MODULE

Contains `DriverCapabilityBucket` enum (5 buckets: ScreenPresentation,
KeyboardInput, MouseInput, DisplayAdaptation, TerminalModeControl) with
`DriverCapabilityMap` class providing `AllBuckets` and
`GetManagedReplacement()`. Real, non-trivial code.

### Gate Module Summary

| Module | Has Real Code | Gate Responsibility | Action Required |
|---|---|---|---|
| `TuiVision.Core` | Yes | Framework base types, draw buffer, events | None — covered by existing tests |
| `TuiVision.Controls` | Yes | TView hierarchy, dialogs, editor, lists | None — many geplant rows need implementing |
| `TuiVision.Serialization` | Yes | Streams, help, resources | None — pending test coverage |
| `TuiVision.Compatibility` | Yes | Key translation, shift state | Create `TuiVision.Compatibility.Tests` (T006) |
| `TuiVision.Drivers.Console` | Yes (in TConsoleDriver.cs + DriverCapabilityMap.cs) | Console rendering, capability map | Class1.cs is correct stub; no restructuring needed |

**No restructuring required.** All five gate modules carry real framework
responsibility. The gate-scope restructuring package does NOT need to be
applied.

---

## T003: Proof-Surface-Konsistenzprüfung / Proof Surface Consistency Audit

### Fünf Nachweisoberflächen / Five Proof Surfaces

The five authoritative proof surfaces examined for consistency:

1. **`Pflichtenheft.md`** (§8.2 Eingangstor Phase 8)
2. **`docs/porting-status.md`**
3. **`specs/005-driver-consolidation-m07/checklists/phase-8-gate-review.md`**
4. **`specs/006-close-phase8-gate/quickstart.md`**
5. **`specs/006-close-phase8-gate/contracts/phase-8-gate-contract.md`**

### Übereinstimmungsanalyse / Consistency Analysis

| Gate Criterion | Pflichtenheft | porting-status | phase-8-gate-review | quickstart | contract |
|---|---|---|---|---|---|
| All 151 `.cc` rows must reach final state | Yes (§8.2) | Open — 103 provisional | Open | Stated | Stated |
| 5-assembly coverage >= 70% each | Yes (§9.4 Nr. 1) | Open | Open | Stated | Stated |
| `dotnet build --configuration Release` PASS | Yes | Phase-7 PASS | Phase-7 PASS | Required | Hard gate |
| `dotnet test` all modules PASS | Yes | Open | Open | Required | Hard gate |
| `dotnet format --verify-no-changes` PASS | Yes | Open | Open | Required | Hard gate |
| `docfx docfx.json` on API changes | Yes | n/a | n/a | Required | Hard gate |
| Dedicated Compatibility.Tests project | Implied | Implied | Implied | Explicitly required | Required |
| Explicit closure commit | Yes | No | No | Required | Required |
| No placeholder-only gate module | Yes | Yes (Class1.cs is stub) | Yes | Required | Hard gate |
| Coverage per assembly, not aggregated | Yes | Open | Open | Required | Assembly-report guarantee |

### Konsistenzurteil / Consistency Verdict

All five proof surfaces are **consistent** with each other. They all:
- Require the same 6 hard gate criteria
- Agree that `portiert + Test ausstehend` is a provisional state that blocks gate closure
- Require coverage per assembly (not aggregate)
- Require a dedicated closure commit
- Agree on the 5 gate assemblies: Core, Controls, Serialization, Compatibility, Drivers.Console

Minor differences in detail level (quickstart and contract are more prescriptive
than Pflichtenheft) but no contradictions. The proof surfaces are ready for
the 006 closure work.

**Note**: The `>>> NAECHSTER SCHRITT <<<` marker in `Pflichtenheft.md` at line
593 correctly points to M-07 closure and Phase-8 gate as the highest-priority
open item — consistent with the 006 feature scope.
