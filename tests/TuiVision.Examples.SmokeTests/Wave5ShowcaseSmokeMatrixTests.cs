// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Examples.Wave5;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die exakte Wave-5-Showcase-Matrix und ihre fail-closed Grenzen.
///
/// Verifies the exact Wave-5 showcase matrix and its fail-closed boundaries.
/// </summary>
[TestClass]
public sealed class Wave5ShowcaseSmokeMatrixTests
{
    private const string MatrixHeading = "## Showcase Evidence Matrix";

    private static readonly ExampleRow[] Examples =
    [
        new("Tp7Demo", "tp7-demo.md", "Tp7ApplicationSmokeTests.cs"),
        new("Tp7Edit", "tp7-edit.md", "Tp7ApplicationSmokeTests.cs"),
        new("Tp7Help", "tp7-help.md", "Tp7ApplicationSmokeTests.cs"),
        new("Tp7ResourceDemo", "tp7-resource-demo.md", "Tp7ResourceSmokeTests.cs"),
        new("Tp7ResourceGenerator", "tp7-resource-generator.md", "Tp7ResourceSmokeTests.cs"),
        new("Tp7AsciiTable", "tp7-ascii-table.md", "Tp7DomainSmokeTests.cs"),
        new("Tp7Calculator", "tp7-calculator.md", "Tp7CalculatorSmokeTests.cs"),
        new("Tp7Calendar", "tp7-calendar.md", "Tp7DomainSmokeTests.cs"),
        new("Tp7Puzzle", "tp7-puzzle.md", "Tp7DomainSmokeTests.cs"),
        new("Tp7MouseDialog", "tp7-mouse-dialog.md", "Tp7DomainSmokeTests.cs")
    ];

    private static readonly string[] AllowedDecisions =
    [
        "UseExistingFramework",
        "SmallFrameworkFix",
        "IntentionalDeviation",
        "FollowUpHardening"
    ];

    /// <summary>
    /// Prüft exakt zehn vollständige, eindeutige Showcase-Zeilen.
    ///
    /// Verifies exactly ten complete and unique showcase rows.
    /// </summary>
    [TestMethod]
    public void Wave5ShowcaseEvidence_Has_Exact_Complete_Ten_Row_Matrix()
    {
        ShowcaseRow[] rows = ReadRows();
        ValidateRows(rows);

        Assert.HasCount(10, rows);
        Assert.HasCount(10, rows.Select(row => row.ExampleId).Distinct(StringComparer.Ordinal));
        Assert.IsTrue(rows.All(row => row.FrameworkDecision == "UseExistingFramework"));
    }

    /// <summary>
    /// Öffnet jede Description über F1 und prüft alle fünf Inhaltsdimensionen.
    ///
    /// Opens every Description through F1 and verifies all five content dimensions.
    /// </summary>
    [TestMethod]
    public void Wave5ShowcaseDescriptions_Are_App_Specific_And_Five_Dimensional()
    {
        DescriptionCase[] cases =
        [
            new("Tp7Demo", bounds => new Tp7DemoApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "Modernes C#", "keine Host", "App-Loop"]),
            new("Tp7Edit", bounds => new Tp7EditApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "Modernes C#", "kontrollierten Root", "App-Loop"]),
            new("Tp7Help", bounds => new Tp7HelpApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "Modernes C#", "kein Teilmodell", "App-Loop"]),
            new("Tp7ResourceDemo", bounds => new Tp7ResourceDemoApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "Modernes C#", "exakten Namen", "App-Loop"]),
            new("Tp7ResourceGenerator", bounds => new Tp7ResourceGeneratorApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "Modernes C#", "kontrollierten Root", "App-Loop"]),
            new("Tp7AsciiTable", bounds => new Tp7AsciiTableApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "moderne Abweichung", "0 bis 255", "App-Loop"]),
            new("Tp7Calculator", bounds => new Tp7CalculatorApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "modernes C#", "Division durch null", "App-Loop"]),
            new("Tp7Calendar", bounds => new Tp7CalendarApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "moderne Abweichung", "Host-Datum", "App-Loop"]),
            new("Tp7Puzzle", bounds => new Tp7PuzzleApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "moderne Abweichung", "benachbarte", "App-Loop"]),
            new("Tp7MouseDialog", bounds => new Tp7MouseDialogApp(bounds, headless: true), ["Historischer Lernzweck", "Tastatur", "moderne Abweichung", "Capability", "App-Loop"])
        ];

        foreach (DescriptionCase descriptionCase in cases)
        {
            Wave5Application app = descriptionCase.Factory(new TRect(0, 0, 80, 25));
            app.QueueEvents([DescriptionKey()]);
            app.Run();

            Assert.IsTrue(app.DescriptionOpened, descriptionCase.ExampleId);
            foreach (string fragment in descriptionCase.RequiredFragments)
            {
                Assert.IsTrue(
                    app.LastDescriptionText.Contains(fragment, StringComparison.Ordinal),
                    $"{descriptionCase.ExampleId} Description misses '{fragment}'.");
            }
        }
    }

    /// <summary>
    /// Prüft fehlende, doppelte, unbekannte und unvollständige Zeilen.
    ///
    /// Verifies missing, duplicate, unknown, and incomplete rows.
    /// </summary>
    [TestMethod]
    public void Wave5ShowcaseValidator_Rejects_Missing_Duplicate_Unknown_And_Incomplete_Rows()
    {
        ShowcaseRow[] valid = ReadRows();

        Assert.ThrowsExactly<InvalidDataException>(() => ValidateRows(valid[..^1]));
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateRows([.. valid[..^1], valid[0]]));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, 0, valid[0] with { FrameworkDecision = "UnknownDecision" })));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, 0, valid[0] with { MainComponent = "Open" })));
    }

    /// <summary>
    /// Prüft Shortcut-, Layout-, Textfallback-, Host- und kontrollierte Grenzen.
    ///
    /// Verifies shortcut, layout, text fallback, host, and controlled boundaries.
    /// </summary>
    [TestMethod]
    public void Wave5ShowcaseValidator_Rejects_Weakened_Interaction_And_Safety_Boundaries()
    {
        ShowcaseRow[] valid = ReadRows();
        int demo = IndexOf(valid, "Tp7Demo");
        int edit = IndexOf(valid, "Tp7Edit");
        int mouse = IndexOf(valid, "Tp7MouseDialog");

        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, demo, valid[demo] with { KeyboardInventory = "Tile, Cascade, Ctrl+Q" })));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, demo, valid[demo] with { ConstrainedProof = string.Empty })));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, demo, valid[demo] with { StatusProof = "color-only" })));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, mouse, valid[mouse] with { IntentionalDeviation = "host mutation: true" })));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateRows(Replace(valid, edit, valid[edit] with { IntentionalDeviation = "arbitrary user paths" })));
    }

    /// <summary>
    /// Prüft identische Auswertung für LF und CRLF.
    ///
    /// Verifies identical evaluation for LF and CRLF.
    /// </summary>
    [TestMethod]
    public void Wave5ShowcaseEvidence_Parses_Lf_And_Crlf_Identically()
    {
        string evidence = File.ReadAllText(EvidencePath());
        string lf = NormalizeLineEndings(evidence);
        string crlf = lf.Replace("\n", "\r\n", StringComparison.Ordinal);

        ValidateRows(ParseRows(lf));
        ValidateRows(ParseRows(crlf));
    }

    /// <summary>
    /// Prüft Entry-Point-, Guide- und Proof-Pfade für alle zehn Beispiele.
    ///
    /// Verifies entry-point, guide, and proof paths for all ten examples.
    /// </summary>
    [TestMethod]
    public void Wave5Showcase_Has_All_Launch_Guide_And_Proof_Paths()
    {
        string root = RepositoryRoot();
        foreach (ExampleRow example in Examples)
        {
            Assert.IsTrue(File.Exists(Path.Combine(root, "examples", example.ExampleId, $"{example.ExampleId}.csproj")), example.ExampleId);
            Assert.IsTrue(File.Exists(Path.Combine(root, "examples", example.ExampleId, "Program.cs")), example.ExampleId);
            Assert.IsTrue(File.Exists(Path.Combine(root, "docs", "guides", "examples", example.Guide)), example.ExampleId);
            Assert.IsTrue(File.Exists(Path.Combine(root, "tests", "TuiVision.Examples.SmokeTests", example.ProofTest)), example.ExampleId);
        }
    }

    private static ShowcaseRow[] ReadRows() => ParseRows(File.ReadAllText(EvidencePath()));

    private static ShowcaseRow[] ParseRows(string evidence)
    {
        evidence = NormalizeLineEndings(evidence);
        int start = evidence.IndexOf(MatrixHeading, StringComparison.Ordinal);
        if (start < 0)
        {
            throw new InvalidDataException($"Missing section: {MatrixHeading}");
        }

        int end = evidence.IndexOf("\n## ", start + MatrixHeading.Length, StringComparison.Ordinal);
        string section = end < 0 ? evidence[start..] : evidence[start..end];
        return section
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.StartsWith('|') && !line.Contains("---", StringComparison.Ordinal))
            .Skip(1)
            .Select(ParseRow)
            .ToArray();
    }

    private static ShowcaseRow ParseRow(string line)
    {
        string[] cells = line
            .Trim('|')
            .Split('|')
            .Select(cell => Unquote(cell.Trim()))
            .ToArray();
        if (cells.Length != 11)
        {
            throw new InvalidDataException($"A showcase row has {cells.Length} instead of 11 columns.");
        }

        return new ShowcaseRow(
            cells[0],
            cells[1],
            cells[2],
            cells[3],
            cells[4],
            cells[5],
            cells[6],
            cells[7],
            cells[8],
            cells[9],
            cells[10]);
    }

    private static void ValidateRows(IEnumerable<ShowcaseRow> source)
    {
        ShowcaseRow[] rows = source.ToArray();
        string[] expected = Examples.Select(example => example.ExampleId).Order(StringComparer.Ordinal).ToArray();
        string[] actual = rows.Select(row => row.ExampleId).ToArray();
        if (actual.Distinct(StringComparer.Ordinal).Count() != actual.Length
            || !actual.Order(StringComparer.Ordinal).SequenceEqual(expected, StringComparer.Ordinal))
        {
            throw new InvalidDataException("The showcase ID set is missing, duplicated, or unknown.");
        }

        foreach (ShowcaseRow row in rows)
        {
            if (!AllowedDecisions.Contains(row.FrameworkDecision, StringComparer.Ordinal)
                || row.RequiredCells.Any(cell => string.IsNullOrWhiteSpace(cell) || cell == "Open")
                || !row.DescriptionProof.Contains("F1", StringComparison.Ordinal)
                || !row.KeyboardInventory.Contains("F1", StringComparison.Ordinal)
                || !row.KeyboardInventory.Contains("Ctrl+Q", StringComparison.Ordinal)
                || !row.NormalProof.Contains("app-loop", StringComparison.OrdinalIgnoreCase)
                || !row.ConstrainedProof.Any(char.IsDigit)
                || row.StatusProof.Contains("color-only", StringComparison.OrdinalIgnoreCase)
                || row.StatusProof.Contains("pointer-only", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException($"Showcase row '{row.ExampleId}' is incomplete or non-text-only.");
            }
        }

        RequireContains(rows, "Tp7Edit", "controlled root");
        RequireContains(rows, "Tp7Help", "atomic");
        RequireContains(rows, "Tp7ResourceDemo", "exact");
        RequireContains(rows, "Tp7ResourceGenerator", "controlled root");
        RequireContains(rows, "Tp7MouseDialog", "no host");
        ShowcaseRow mouse = rows.Single(row => row.ExampleId == "Tp7MouseDialog");
        if (mouse.AllText.Contains("host mutation: true", StringComparison.OrdinalIgnoreCase)
            || mouse.AllText.Contains("HostMutationPerformed == true", StringComparison.Ordinal))
        {
            throw new InvalidDataException("Mouse evidence claims forbidden host mutation.");
        }
    }

    private static void RequireContains(IEnumerable<ShowcaseRow> rows, string exampleId, string fragment)
    {
        ShowcaseRow row = rows.Single(candidate => candidate.ExampleId == exampleId);
        if (!row.AllText.Contains(fragment, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidDataException($"{exampleId} lacks controlled-boundary evidence '{fragment}'.");
        }
    }

    private static ShowcaseRow[] Replace(ShowcaseRow[] rows, int index, ShowcaseRow replacement)
    {
        ShowcaseRow[] result = rows.ToArray();
        result[index] = replacement;
        return result;
    }

    private static int IndexOf(ShowcaseRow[] rows, string exampleId) =>
        Array.FindIndex(rows, row => row.ExampleId == exampleId);

    private static string EvidencePath() =>
        Path.Combine(RepositoryRoot(), "specs", "033-wave5-tp7-showcase-remediation", "pr-evidence.md");

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }

    private static string NormalizeLineEndings(string document) =>
        document.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');

    private static string Unquote(string value) =>
        value.Length >= 2 && value[0] == '`' && value[^1] == '`' ? value[1..^1] : value;

    private static TEvent DescriptionKey() =>
        TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B));

    private sealed record ExampleRow(string ExampleId, string Guide, string ProofTest);

    private sealed record DescriptionCase(
        string ExampleId,
        Func<TRect, Wave5Application> Factory,
        string[] RequiredFragments);

    private sealed record ShowcaseRow(
        string ExampleId,
        string FrameworkDecision,
        string MainComponent,
        string StatusProof,
        string DescriptionProof,
        string KeyboardInventory,
        string NormalProof,
        string ConstrainedProof,
        string HistoricalIntent,
        string IntentionalDeviation,
        string FollowUpBoundary)
    {
        public string[] RequiredCells =>
        [
            ExampleId,
            FrameworkDecision,
            MainComponent,
            StatusProof,
            DescriptionProof,
            KeyboardInventory,
            NormalProof,
            ConstrainedProof,
            HistoricalIntent,
            IntentionalDeviation,
            FollowUpBoundary
        ];

        public string AllText => string.Join(' ', RequiredCells);
    }
}
