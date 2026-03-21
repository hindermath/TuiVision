# Quickstart: Dialog-/Control-Schicht (003-dialog-control-layer)

**Phase**: 1 — Design & Contracts
**Date**: 2026-03-21

Dieser Leitfaden erklärt, wie die Dialog-/Control-Schicht entwickelt, getestet und verifiziert wird.
Er richtet sich an Entwickler, die zu TuiVision beitragen.

This guide explains how to develop, test, and verify the dialog/control layer.
It is aimed at developers contributing to TuiVision.

---

## Voraussetzungen / Prerequisites

```bash
# .NET 10 SDK prüfen / Check .NET 10 SDK
dotnet --version   # muss 10.x.x sein / must be 10.x.x

# Repository klonen und Branch wechseln / Clone and switch branch
git clone https://github.com/hindermath/TuiVision.git
cd TuiVision
git checkout 003-dialog-control-layer

# Abhängigkeiten wiederherstellen / Restore dependencies
dotnet restore
```

---

## Build & Tests ausführen / Build & Run Tests

```bash
# Alle Projekte bauen / Build all projects
dotnet build --configuration Release

# Alle Tests ausführen / Run all tests
dotnet test

# Nur Control-Tests ausführen / Run only control tests
dotnet test tests/TuiVision.Controls.Tests/

# Coverage messen (Gate: ≥ 70 %) / Measure coverage (gate: ≥ 70%)
dotnet test tests/TuiVision.Controls.Tests/ \
  --collect:"XPlat Code Coverage" \
  --results-directory ./coverage

# Einzelnen Test ausführen / Run a single test
dotnet test --filter "FullyQualifiedName~TDialog_Run_ReturnsCommandIdOnClose"
```

---

## TDD-Workflow (Pflicht / Mandatory)

Für jede neue Klasse gilt die **Red → Green → Refactor**-Reihenfolge.
Commit-Reihenfolge muss sichtbar sein (Constitution §II):

```bash
# 1. Red: Test schreiben, der noch scheitert
# Write failing test first
git add tests/TuiVision.Controls.Tests/TButtonTests.cs
git commit -m "test(controls): TButton_HandleEvent_ActivatesOnEnter (Red)"

# 2. Green: Minimale Implementation
# Minimal implementation to make test pass
git add src/TuiVision.Controls/TButton.cs
git commit -m "feat(controls): implement TButton command activation (Green)"

# 3. Refactor: Code verbessern ohne Tests zu brechen
# Refactor without breaking tests
git add src/TuiVision.Controls/TButton.cs
git commit -m "refactor(controls): TButton Draw() cleanup and XML docs (Refactor)"
```

---

## Implementierungsreihenfolge / Implementation Order

Klassen in dieser Reihenfolge portieren (Abhängigkeiten minimieren):

| Schritt | Klasse | C++ Quelle |
|---|---|---|
| 1 | `TStringList` | `tstrlist.cc` |
| 2 | `TScrollBar` | `tscrollb.cc` |
| 3 | `TScroller` | `tscrolle.cc` |
| 4 | `TStaticText` | `tstatict.cc` |
| 5 | `TCluster` | `cluster.h` |
| 6 | `TCheckBoxes` | `tcheckbo.cc` |
| 7 | `TRadioButtons` | `tradiobu.cc` |
| 8 | `TLabel` | `tlabel.cc` |
| 9 | `TListViewer` | `tlistvie.cc` |
| 10 | `TListBox` | `tlistbox.cc` |
| 11 | `TButton` | `tbutton.cc` |
| 12 | `TInputLine` | `tinputli.cc` |
| 13 | `TDialog` | `tdialog.cc` |

---

## Wo neue Dateien anlegen / Where to create new files

```text
src/TuiVision.Controls/
├── TStringList.cs        ← neu / new
├── TScrollBar.cs         ← neu / new
├── TScroller.cs          ← neu / new
├── TStaticText.cs        ← neu / new
├── TLabel.cs             ← neu / new
├── TButton.cs            ← neu / new (inkl. TButtonFlags)
├── TInputLine.cs         ← neu / new
├── TCluster.cs           ← neu / new
├── TCheckBoxes.cs        ← neu / new
├── TRadioButtons.cs      ← neu / new
├── TListViewer.cs        ← neu / new
├── TListBox.cs           ← neu / new
└── TDialog.cs            ← neu / new

tests/TuiVision.Controls.Tests/
├── TStringListTests.cs   ← neu / new
├── TScrollBarTests.cs    ← neu / new
├── TScrollerTests.cs     ← neu / new
├── TStaticTextTests.cs   ← neu / new
├── TLabelTests.cs        ← neu / new
├── TButtonTests.cs       ← neu / new
├── TInputLineTests.cs    ← neu / new
├── TClusterTests.cs      ← neu / new
├── TCheckBoxesTests.cs   ← neu / new
├── TRadioButtonsTests.cs ← neu / new
├── TListViewerTests.cs   ← neu / new
├── TListBoxTests.cs      ← neu / new
└── TDialogTests.cs       ← neu / new
```

---

## Dokumentations-Pflicht / Documentation Requirement

Jede neue Klasse und jedes öffentliche Member **muss** zweisprachige XML-Dokumentation
enthalten (Constitution §III):

```csharp
/// <summary>
/// Einzeiliges Texteingabefeld. Verwaltet Zeicheninhalt, Cursor-Position
/// und Maximallänge. Portiert von <c>TInputLine</c> aus Turbo Vision 2.0.3.
///
/// Single-line text input field. Manages character content, cursor position,
/// and maximum length. Ported from <c>TInputLine</c> in Turbo Vision 2.0.3.
/// </summary>
public class TInputLine : TView { ... }
```

Fehlende XML-Dokumentation für öffentliche Member wird als Build-Fehler behandelt
(CS1591 ist aktiviert). / Missing XML documentation for public members is treated
as a build error (CS1591 is enabled).

---

## Quality Gates vor Merge / Quality Gates Before Merge

```bash
# 1. Build ohne Warnings / Build without warnings
dotnet build --configuration Release

# 2. Alle Tests grün / All tests green
dotnet test

# 3. Coverage ≥ 70 % für TuiVision.Controls
dotnet test tests/TuiVision.Controls.Tests/ --collect:"XPlat Code Coverage"

# 4. Formatierung prüfen / Check formatting
dotnet format --verify-no-changes

# 5. Dokumentation generieren (falls docfx.json vorhanden) / Generate docs
docfx docfx.json
```

Zusätzlicher Governance-Check / Additional governance check:

```bash
# Falls im Feature JSON-Hilfsdateien, Testdaten oder Tooling ergänzt wurden:
# Nur System.Text.Json verwenden; keine implizite Newtonsoft.Json-Einführung
rg -n "Newtonsoft\\.Json|PackageReference Include=\"Newtonsoft\\.Json\"" \
  src tests specs/003-dialog-control-layer
```

Der Check muss leer bleiben, solange keine dokumentierte Ausnahme für
`Newtonsoft.Json` im Plan oder PR begründet wurde.

The check must return no matches unless the plan or PR documents an approved
exception for `Newtonsoft.Json`.

---

## Referenz-Quellen / Reference Sources

Die originalen C/C++-Quelldateien befinden sich in `tv203s/contrib/tvision/` und dienen
als Verhaltensreferenz. **Nicht modifizieren!**

The original C/C++ source files are in `tv203s/contrib/tvision/` and serve as behavioral
reference. **Do not modify!**

```bash
# Originalverhalten nachschlagen / Look up original behavior
cat tv203s/contrib/tvision/classes/tinputli.cc
cat tv203s/contrib/tvision/classes/tbutton.cc
cat tv203s/contrib/tvision/include/tv/button.h
```
