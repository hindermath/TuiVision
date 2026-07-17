// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.Wave6;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die reale Wave-6-Anwendung und ihre Traceability.
///
/// Verifies the real Wave-6 application and its traceability.
/// </summary>
[TestClass]
public sealed class Wave6FunctionalSmokeMatrixTests : ExampleTestBase
{
    private static readonly string[] Sources =
    [
        "TVFM/ASSOC.PAS",
        "TVFM/COLORS.PAS",
        "TVFM/CYAN.PAL",
        "TVFM/DEFAULT.PAL",
        "TVFM/DIRVIEW.PAS",
        "TVFM/DRAGDROP.PAS",
        "TVFM/EDITPAL.PAS",
        "TVFM/EQU.PAS",
        "TVFM/FILECOPY.PAS",
        "TVFM/FILEFIND.PAS",
        "TVFM/FILEVIEW.PAS",
        "TVFM/GAUGES.PAS",
        "TVFM/GLOBALS.PAS",
        "TVFM/INFOVIEW.PAS",
        "TVFM/MAKERES.PAS",
        "TVFM/MAKETVFM.BAT",
        "TVFM/ROSE.PAL",
        "TVFM/TOOLS.PAS",
        "TVFM/TRASH.PAS",
        "TVFM/TREEWIN.PAS",
        "TVFM/TVFM.PAS",
        "TVFM/TVFM.TVR",
        "TVFM/VIEWHEX.PAS",
        "TVFM/VIEWTEXT.PAS"
    ];

    private static readonly string[] Areas =
    [
        "W6-001",
        "W6-002",
        "W6-003",
        "W6-004",
        "W6-005",
        "W6-006",
        "W6-007",
        "W6-008",
        "W6-009",
        "W6-010"
    ];

    /// <summary>Prüft Navigation und Textvorschau über den App-Loop. / Verifies navigation and text preview through the app loop.</summary>
    [TestMethod]
    public void Wave6FileManager_AppLoop_Navigates_Previews_And_Renders_State()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateCommand(Tp7FileManagerApp.CmPreviewText)
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.AreEqual("docs", app.CurrentSnapshot?.RelativeDirectory);
            Assert.AreEqual("docs/lesson.txt", app.LastPreview?.RelativePath);
            AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Wave-6 main view");
            AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "lesson", "Wave-6 preview cells");
            AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "docs/lesson.txt", "Wave-6 status");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft F1-Description über den echten Ereignispfad. / Verifies F1 Description through the real event path.</summary>
    [TestMethod]
    public void Wave6FileManager_AppLoop_Opens_Description()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(48, 16), headless: true);
            app.QueueEvents([TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B))]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsTrue(app.DescriptionOpened);
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "kontrollierte Wurzel", "Wave-6 Description safety");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "F1", "Wave-6 Description hint");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft Liste, Tag, Hex, Suche und Palette im App-Loop. / Verifies list, tag, hex, search and palette in the app loop.</summary>
    [TestMethod]
    public void Wave6FileManager_AppLoop_Exposes_Read_And_Resource_Commands()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateCommand(Tp7FileManagerApp.CmTagFirst),
                TEvent.CreateCommand(Tp7FileManagerApp.CmPreviewHex),
                TEvent.CreateCommand(Tp7FileManagerApp.CmSearchText),
                TEvent.CreateCommand(Tp7FileManagerApp.CmChangePalette)
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            CollectionAssert.Contains(app.TaggedPaths.ToArray(), "docs/lesson.txt");
            Assert.AreEqual(Wave6ViewerDecision.Hex, app.LastPreview?.Decision);
            Assert.IsNotNull(app.LastSearch);
            Assert.HasCount(1, app.LastSearch.Matches);
            Assert.AreEqual(Wave6Palette.Cyan, app.Palette);
            AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "palette=Cyan", "Wave-6 palette cells");
            Assert.IsTrue(app.HasRealStatusLine);
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft Tastatur-Intent, Abbruch und bestätigte Kopie. / Verifies keyboard intent, cancel and confirmed copy.</summary>
    [TestMethod]
    public void Wave6FileManager_AppLoop_Requires_Explicit_Mutation_Decision()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmKeyboardDropIntent),
                TEvent.CreateCommand(Tp7FileManagerApp.CmCancelOperation),
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy),
                TEvent.CreateCommand(Tp7FileManagerApp.CmConfirmOperation)
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.AreEqual(Wave6OperationState.Completed, app.LastOperation?.State);
            Assert.AreEqual(100, app.LastOperation?.ProgressPercent);
            Assert.IsTrue(File.Exists(Path.Combine(root, "README.txt.copy")));
            AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Completed", "Wave-6 mutation cells");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft, dass Maus und Tastatur dieselbe bestätigungspflichtige Absicht erzeugen. / Verifies that mouse and keyboard create the same confirmable intent.</summary>
    [TestMethod]
    public void Wave6FileManager_AppLoop_Mouse_Only_Prepares_The_Keyboard_Intent()
    {
        string keyboardRoot = CreateWorkspace();
        string mouseRoot = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace keyboardWorkspace = new(keyboardRoot);
            using ControlledFileWorkspace mouseWorkspace = new(mouseRoot);
            Tp7FileManagerApp keyboard = new(keyboardWorkspace, DefaultBounds(), headless: true);
            Tp7FileManagerApp mouse = new(mouseWorkspace, DefaultBounds(), headless: true);
            keyboard.QueueEvents([TEvent.CreateCommand(Tp7FileManagerApp.CmKeyboardDropIntent)]);
            mouse.QueueEvents([TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4))]);

            AssertSmokeRunCompletes(() => keyboard.Run());
            AssertSmokeRunCompletes(() => mouse.Run());

            Assert.IsNotNull(keyboard.PendingOperation);
            Assert.IsNotNull(mouse.PendingOperation);
            Assert.AreEqual(keyboard.PendingOperation.Kind, mouse.PendingOperation.Kind);
            Assert.AreEqual(keyboard.PendingOperation.SourceRelativePath, mouse.PendingOperation.SourceRelativePath);
            Assert.AreEqual(keyboard.PendingOperation.TargetRelativePath, mouse.PendingOperation.TargetRelativePath);
            Assert.IsNull(mouse.LastOperation);
            Assert.IsFalse(File.Exists(Path.Combine(mouseRoot, "README.txt.copy")));
        }
        finally
        {
            Directory.Delete(keyboardRoot, recursive: true);
            Directory.Delete(mouseRoot, recursive: true);
        }
    }

    /// <summary>Prüft die exakte 24/10/1/1-Evidence-Cardinality. / Verifies the exact 24/10/1/1 evidence cardinality.</summary>
    [TestMethod]
    public void Wave6Evidence_Has_Exact_Source_Area_Entry_And_Stage2_Cardinality()
    {
        string evidence = File.ReadAllText(EvidencePath());
        ValidateEvidenceMatrices(evidence);
    }

    /// <summary>Prüft fail-closed Verhalten der Evidence-Matrix. / Verifies fail-closed evidence matrix behavior.</summary>
    [TestMethod]
    public void Wave6EvidenceValidator_Rejects_Malformed_Row_Sets()
    {
        string[][] valid =
        [
            ["W6-001", "scope", "UseExistingFramework", "contracts", "logic", "proof", "risk"],
            ["W6-002", "scope", "IntentionalDeviation", "contracts", "logic", "proof", "risk"]
        ];
        string[] expected = ["W6-001", "W6-002"];

        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows(valid[..1], expected, row => row[0], _ => true, 7));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows([valid[0], valid[0]], expected, row => row[0], _ => true, 7));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows([valid[0], ["W6-999", .. valid[1][1..]]], expected, row => row[0], _ => true, 7));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows(
                [valid[0], ["W6-002", "scope", "Open", "contracts", "logic", "proof", "risk"]],
                expected,
                row => row[0],
                row => IsFrameworkDecision(row[2]),
                7));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows(
                [valid[0], ["W6-002", "scope", "UseExistingFramework", "contracts", "", "proof", "risk"]],
                expected,
                row => row[0],
                row => row.Skip(1).All(cell => !string.IsNullOrWhiteSpace(cell)),
                7));
    }

    private static void ValidateEvidenceMatrices(string evidence)
    {
        string[][] sourceRows = ParseTable(evidence, "## Historical Source Matrix");
        string[][] areaRows = ParseTable(evidence, "## Functional Area Decisions");
        string[][] entryRows = ParseTable(evidence, "## Primary Proof and Stage-2");

        ValidateExactRows(
            sourceRows,
            Sources,
            row => Unquote(row[0]),
            row => IsSourceRole(row[2])
                && row.Skip(3).All(IsCompletedCell)
                && !row[6].Contains("Planned", StringComparison.OrdinalIgnoreCase),
            7);
        ValidateExactRows(
            areaRows,
            Areas,
            row => row[0],
            row => IsFrameworkDecision(row[2])
                && row.Skip(3).All(IsCompletedCell)
                && !row[5].Contains("Planned", StringComparison.OrdinalIgnoreCase),
            7);
        ValidateExactRows(
            entryRows,
            ["Tp7FileManager"],
            row => Unquote(row[0]),
            row => row[1..5].All(IsCompletedCell) && IsStage2Disposition(row[5]),
            6);
        Assert.AreEqual(1, entryRows.Count(row => IsStage2Disposition(row[5])), "Exactly one Stage-2 disposition is required.");
    }

    private static bool IsSourceRole(string role) =>
        role is "EntryPoint"
            or "ApplicationSupport"
            or "ViewOrInteraction"
            or "FileOperation"
            or "ResourceOrPalette"
            or "BuildIntent"
            or "IntentionalOmission";

    private static bool IsFrameworkDecision(string decision) =>
        decision is "UseExistingFramework" or "SmallFrameworkFix" or "IntentionalDeviation" or "FollowUpHardening";

    private static bool IsStage2Disposition(string value) =>
        value.StartsWith("ShowcaseComplete", StringComparison.Ordinal)
            || value.StartsWith("ShowcaseDelta", StringComparison.Ordinal)
            || value.StartsWith("IntentionalMinimalSurface", StringComparison.Ordinal)
            || value.StartsWith("ProductDecision", StringComparison.Ordinal);

    private static bool IsCompletedCell(string value) =>
        !string.IsNullOrWhiteSpace(value)
        && !value.Equals("Open", StringComparison.OrdinalIgnoreCase)
        && !value.Contains("Planned", StringComparison.OrdinalIgnoreCase);

    private static string[][] ParseTable(string document, string heading)
    {
        document = NormalizeLineEndings(document);
        int start = document.IndexOf(heading, StringComparison.Ordinal);
        if (start < 0)
        {
            throw new InvalidDataException($"Missing section: {heading}");
        }

        int end = document.IndexOf("\n## ", start + heading.Length, StringComparison.Ordinal);
        string section = end < 0 ? document[start..] : document[start..end];
        return section
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.StartsWith('|') && !line.Contains("---", StringComparison.Ordinal))
            .Skip(1)
            .Select(line => line.Trim('|').Split('|').Select(cell => cell.Trim()).ToArray())
            .ToArray();
    }

    private static string NormalizeLineEndings(string document) =>
        document.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');

    private static void ValidateExactRows(
        IEnumerable<string[]> rows,
        IEnumerable<string> expectedIds,
        Func<string[], string> idSelector,
        Func<string[], bool> rowValidator,
        int minimumColumns)
    {
        string[][] materialized = rows.ToArray();
        string[] expected = expectedIds.Order(StringComparer.Ordinal).ToArray();
        if (materialized.Any(row => row.Length < minimumColumns))
        {
            throw new InvalidDataException("A matrix row has too few columns.");
        }

        string[] actual = materialized.Select(idSelector).ToArray();
        if (actual.Distinct(StringComparer.Ordinal).Count() != actual.Length)
        {
            throw new InvalidDataException("A matrix ID is duplicated.");
        }

        if (!actual.Order(StringComparer.Ordinal).SequenceEqual(expected, StringComparer.Ordinal))
        {
            throw new InvalidDataException("The matrix ID set is missing or unknown.");
        }

        if (materialized.Any(row => !rowValidator(row)))
        {
            throw new InvalidDataException("A matrix row has an unknown decision or empty required field.");
        }
    }

    private static string Unquote(string value) =>
        value.Length >= 2 && value[0] == '`' && value[^1] == '`' ? value[1..^1] : value;

    private static string EvidencePath() =>
        Path.Combine(RepositoryRoot(), "specs", "035-wave6-tvfm-functional-porting", "pr-evidence.md");

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }

    private static string CreateWorkspace()
    {
        string root = Path.Combine(Path.GetTempPath(), $"tuivision-wave6-app-{Guid.NewGuid():N}");
        Directory.CreateDirectory(Path.Combine(root, "docs"));
        Directory.CreateDirectory(Path.Combine(root, "data"));
        File.WriteAllText(Path.Combine(root, "README.txt"), "root");
        File.WriteAllText(Path.Combine(root, "docs", "lesson.txt"), "lesson");
        return root;
    }
}
