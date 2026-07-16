// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Rekonstruiert die exakte Wave-5-Stage-1-Traceability aus der Evidence.
///
/// Reconstructs the exact Wave-5 Stage-1 traceability from the evidence.
/// </summary>
[TestClass]
public sealed class Wave5FunctionalSmokeMatrixTests
{
    private static readonly string[] Sources =
    [
        "TVDEMOS/TVDEMO.PAS",
        "TVDEMOS/DEMOCMDS.PAS",
        "TVDEMOS/DEMOSTRS.PAS",
        "TVDEMOS/GADGETS.PAS",
        "TVDEMOS/TVEDIT.PAS",
        "TVDEMOS/TVHC.PAS",
        "TVDEMOS/HELPFILE.PAS",
        "TVDEMOS/DEMOHELP.PAS",
        "TVDEMOS/TVRDEMO.PAS",
        "TVDEMOS/GENRDEMO.PAS",
        "TVDEMOS/ASCIITAB.PAS",
        "TVDEMOS/CALC.PAS",
        "TVDEMOS/CALENDAR.PAS",
        "TVDEMOS/PUZZLE.PAS",
        "TVDEMOS/MOUSEDLG.PAS"
    ];

    private static readonly string[] Consumers = ["W5-001", "W5-002", "W5-003", "W5-004", "W5-005", "W5-006"];

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

    /// <summary>Prüft exakt 15/6/10/10 eindeutige und vollständige Zeilen. / Verifies exactly 15/6/10/10 unique and complete rows.</summary>
    [TestMethod]
    public void Wave5Evidence_Has_Exact_Source_Consumer_Proof_And_Delta_Cardinality()
    {
        string evidence = File.ReadAllText(Path.Combine(RepositoryRoot(), "specs", "032-wave5-tp7-functional-porting", "pr-evidence.md"));
        ValidateEvidenceMatrices(evidence);
    }

    /// <summary>Prüft identische Evidence-Auswertung für LF und CRLF. / Verifies identical evidence evaluation for LF and CRLF.</summary>
    [TestMethod]
    public void Wave5Evidence_Parses_Lf_And_Crlf_Identically()
    {
        string evidence = File.ReadAllText(Path.Combine(RepositoryRoot(), "specs", "032-wave5-tp7-functional-porting", "pr-evidence.md"));
        string lfEvidence = NormalizeLineEndings(evidence);
        string crlfEvidence = lfEvidence.Replace("\n", "\r\n", StringComparison.Ordinal);

        ValidateEvidenceMatrices(lfEvidence);
        ValidateEvidenceMatrices(crlfEvidence);
    }

    private static void ValidateEvidenceMatrices(string evidence)
    {
        string[][] sourceRows = ParseTable(evidence, "## Historical Source Matrix");
        string[][] consumerRows = ParseTable(evidence, "## Consumer Decisions");
        string[][] proofRows = ParseTable(evidence, "## Primary Proof Matrix");
        string[][] deltaRows = ParseTable(evidence, "## Showcase Delta Matrix");

        ValidateExactRows(sourceRows, Sources, row => Unquote(row[0]), row => IsSourceRole(row[1]), minimumColumns: 5);
        ValidateExactRows(consumerRows, Consumers, row => row[0], row => row[1] == "UseExistingFramework", minimumColumns: 6);
        ValidateExactRows(proofRows, Examples.Select(example => example.Name), row => row[0], row => row[5] == "Pass", minimumColumns: 6);
        ValidateExactRows(
            deltaRows,
            Examples.Select(example => example.Name),
            row => row[0],
            row => row[1] == "Stage2Required" && row.Skip(2).All(cell => !string.IsNullOrWhiteSpace(cell) && cell != "Open"),
            minimumColumns: 9);
    }

    /// <summary>Prüft fail-closed Verhalten für fehlende, doppelte, unbekannte und leere Zeilen. / Verifies fail-closed behavior for missing, duplicate, unknown, and empty rows.</summary>
    [TestMethod]
    public void Wave5EvidenceValidator_Rejects_Malformed_Row_Sets()
    {
        string[][] valid =
        [
            ["Tp7Demo", "Stage2Required", "function", "visual", "interaction", "layout", "a11y", "P1", "evidence"],
            ["Tp7Edit", "Stage2Required", "function", "visual", "interaction", "layout", "a11y", "P1", "evidence"]
        ];
        string[] expected = ["Tp7Demo", "Tp7Edit"];

        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows(valid[..1], expected, row => row[0], _ => true, 9));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows([valid[0], valid[0]], expected, row => row[0], _ => true, 9));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows([valid[0], ["Unknown", .. valid[1][1..]]], expected, row => row[0], _ => true, 9));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateExactRows(
                [valid[0], ["Tp7Edit", "Stage2Required", "function", "", "interaction", "layout", "a11y", "P1", "evidence"]],
                expected,
                row => row[0],
                row => row.Skip(2).All(cell => !string.IsNullOrWhiteSpace(cell)),
                9));
    }

    /// <summary>Prüft Projekt-, Guide- und Proof-Dateien für alle zehn Beispiele. / Verifies project, guide, and proof files for all ten examples.</summary>
    [TestMethod]
    public void Wave5Examples_Have_Launch_Project_Guide_And_Proof_Test()
    {
        string root = RepositoryRoot();
        foreach (ExampleRow example in Examples)
        {
            Assert.IsTrue(
                File.Exists(Path.Combine(root, "examples", example.Name, $"{example.Name}.csproj")),
                $"{example.Name} launch project");
            Assert.IsTrue(
                File.Exists(Path.Combine(root, "examples", example.Name, "Program.cs")),
                $"{example.Name} entry point");
            Assert.IsTrue(
                File.Exists(Path.Combine(root, "docs", "guides", "examples", example.Guide)),
                $"{example.Name} guide");
            Assert.IsTrue(
                File.Exists(Path.Combine(root, "tests", "TuiVision.Examples.SmokeTests", example.ProofTest)),
                $"{example.Name} proof test");
        }
    }

    private static bool IsSourceRole(string role) =>
        role is "EntryPoint" or "SupportUnit" or "FixtureOrContent" or "GeneratorIntent" or "IntentionalOmission";

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

    private static string RepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }

    private sealed record ExampleRow(string Name, string Guide, string ProofTest);
}
