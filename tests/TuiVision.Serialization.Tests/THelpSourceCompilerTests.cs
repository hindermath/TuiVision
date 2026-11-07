// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests fuer den deterministischen Help-Source-Compiler.
///
/// Tests for the deterministic help source compiler.
/// </summary>
[TestClass]
public sealed class THelpSourceCompilerTests
{
    private const string ValidSource = """
        .topic Overview=100
        Start with {more information:Details}.

        .topic Details=200
         Detailed line
        """;

    /// <summary>
    /// Prueft Themen, Vorwaertsreferenz und deterministische Symbolwerte.
    ///
    /// Verifies topics, a forward reference, and deterministic symbol values.
    /// </summary>
    [TestMethod]
    public void Compile_ValidForwardReference_CreatesRuntimeHelpModel()
    {
        THelpCompilationResult result = new THelpSourceCompiler().Compile(ValidSource, "valid.txt");

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.HelpFile);
        Assert.HasCount(2, result.HelpFile.Topics);
        Assert.AreEqual(100, result.Symbols["Overview"]);
        Assert.AreEqual(200, result.Symbols["Details"]);
        Assert.AreEqual("Overview", result.HelpFile.Lookup(100)!.Title);
        Assert.AreEqual(200, result.HelpFile.Lookup(100)!.CrossReferences[0].TargetContext);
    }

    /// <summary>
    /// Prueft identische logische Ergebnisse bei wiederholter Uebersetzung.
    ///
    /// Verifies identical logical results across repeated compilation.
    /// </summary>
    [TestMethod]
    public void Compile_SameSourceTwice_ProducesEquivalentModels()
    {
        THelpSourceCompiler compiler = new();

        THelpCompilationResult first = compiler.Compile(ValidSource);
        THelpCompilationResult second = compiler.Compile(ValidSource);

        CollectionAssert.AreEqual(first.Symbols.ToArray(), second.Symbols.ToArray());
        CollectionAssert.AreEqual(
            first.HelpFile!.Topics.Select(DescribeTopic).ToArray(),
            second.HelpFile!.Topics.Select(DescribeTopic).ToArray());
    }

    /// <summary>
    /// Prueft mehrere Symbole und den ersten Symbolnamen als Titel.
    ///
    /// Verifies multiple symbols and the first symbol name as the title.
    /// </summary>
    [TestMethod]
    public void Compile_AliasSymbols_ExposeEveryContextWithPrimaryTitle()
    {
        const string source = ".topic Primary=7, Alias\nBody";

        THelpCompilationResult result = new THelpSourceCompiler().Compile(source);

        Assert.IsTrue(result.Success);
        Assert.AreEqual(7, result.Symbols["Primary"]);
        Assert.AreEqual(8, result.Symbols["Alias"]);
        Assert.AreEqual("Primary", result.HelpFile!.Lookup(7)!.Title);
        Assert.AreEqual("Primary", result.HelpFile.Lookup(8)!.Title);
    }

    /// <summary>
    /// Prueft den Roundtrip ueber die bestehende Ressourcenregistrierung.
    ///
    /// Verifies round-tripping through the existing resource registration.
    /// </summary>
    [TestMethod]
    public void Compile_ValidSource_RoundTripsThroughResourceFile()
    {
        THelpCompilationResult result = new THelpSourceCompiler().Compile(ValidSource);
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        TResourceFile.RegisterBuiltInTypes(registry);
        TResourceFile resources = new(registry);
        resources.Put("help", result.HelpFile!);
        using MemoryStream stream = new();

        resources.Save(stream);
        stream.Position = 0;
        THelpFile restored = TResourceFile.Load(stream, registry).Get<THelpFile>("help")!;

        Assert.AreEqual("Details", restored.Lookup(200)!.Title);
        Assert.AreEqual(200, restored.Lookup(100)!.CrossReferences[0].TargetContext);
    }

    /// <summary>
    /// Prueft atomare Fehler fuer doppelte und ungueltige Definitionen.
    ///
    /// Verifies atomic failures for duplicate and invalid definitions.
    /// </summary>
    [TestMethod]
    [DataRow(".topic One=1\nA\n.topic One=2\nB", "THC004")]
    [DataRow(".topic One=1\nA\n.topic Two=1\nB", "THC005")]
    [DataRow(".topic One=not-a-number\nA", "THC003")]
    [DataRow("Body before topic", "THC002")]
    [DataRow(".topic One=1\n.unknown value", "THC002")]
    public void Compile_InvalidDefinition_ReturnsDiagnosticWithoutPartialModel(string source, string code)
    {
        THelpCompilationResult result = new THelpSourceCompiler().Compile(source, "invalid.txt");

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.HelpFile);
        Assert.IsEmpty(result.Symbols);
        Assert.IsTrue(result.Diagnostics.Any(item => item.Code == code));
        Assert.IsTrue(result.Diagnostics.All(item => item.Line >= 1 && item.Column >= 1));
    }

    /// <summary>
    /// Prueft fehlerhafte und nicht aufloesbare Querverweise.
    ///
    /// Verifies malformed and unresolved cross references.
    /// </summary>
    [TestMethod]
    [DataRow(".topic One=1\nBroken {reference", "THC006")]
    [DataRow(".topic One=1\nMissing {target:Nowhere}", "THC007")]
    [DataRow(".topic One=1\nEmpty {:Two}\n.topic Two=2", "THC006")]
    public void Compile_InvalidReference_ReturnsDiagnosticWithoutModel(string source, string code)
    {
        THelpCompilationResult result = new THelpSourceCompiler().Compile(source);

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.HelpFile);
        Assert.IsTrue(result.Diagnostics.Any(item => item.Code == code));
    }

    /// <summary>
    /// Prueft striktes UTF-8 und konfigurierbare Eingabegrenzen.
    ///
    /// Verifies strict UTF-8 and configurable input limits.
    /// </summary>
    [TestMethod]
    public void Compile_InvalidUtf8OrLimit_ReturnsBoundedDiagnostic()
    {
        using MemoryStream invalidUtf8 = new([0xC3, 0x28]);
        using MemoryStream utf16 = new([0xFF, 0xFE, 0x2E, 0x00]);
        THelpCompilationResult encoding = new THelpSourceCompiler().Compile(invalidUtf8, "invalid-utf8.txt");
        THelpCompilationResult wrongEncoding = new THelpSourceCompiler().Compile(utf16, "utf16.txt");
        THelpSourceCompiler limited = new(new THelpCompilerOptions { MaxSourceCharacters = 8 });
        THelpCompilationResult limit = limited.Compile(".topic One=1\nBody");

        Assert.IsTrue(encoding.Diagnostics.Any(item => item.Code == "THC008"));
        Assert.IsTrue(wrongEncoding.Diagnostics.Any(item => item.Code == "THC008"));
        Assert.IsTrue(limit.Diagnostics.Any(item => item.Code == "THC009"));
        Assert.IsNull(encoding.HelpFile);
        Assert.IsNull(limit.HelpFile);
    }

    /// <summary>
    /// Prueft Zeilen-, Themen- und Optionsgrenzen.
    ///
    /// Verifies line, topic, and option limits.
    /// </summary>
    [TestMethod]
    public void Compile_ConfiguredLimits_AreValidatedAndReported()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
            new THelpSourceCompiler(new THelpCompilerOptions { MaxTopics = 0 }));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
            new THelpSourceCompiler(new THelpCompilerOptions { MaxExpandedModelItems = 0 }));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
            new THelpSourceCompiler(new THelpCompilerOptions { MaxSourceLines = 0 }));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
            new THelpSourceCompiler(new THelpCompilerOptions { MaxDiagnostics = 0 }));

        THelpCompilationResult longLine = new THelpSourceCompiler(
            new THelpCompilerOptions { MaxLineCharacters = 8 }).Compile(".topic One=1\nLong body");
        THelpCompilationResult topics = new THelpSourceCompiler(
            new THelpCompilerOptions { MaxTopics = 1 }).Compile(".topic One=1\nA\n.topic Two=2\nB");

        Assert.IsTrue(longLine.Diagnostics.Any(item => item.Code == "THC009"));
        Assert.IsTrue(topics.Diagnostics.Any(item => item.Code == "THC009"));
        Assert.IsNull(longLine.HelpFile);
        Assert.IsNull(topics.HelpFile);
    }

    /// <summary>
    /// Begrenzt dichte Fehlerquellen vor und waehrend der Diagnoseerzeugung.
    ///
    /// Bounds dense invalid sources before and during diagnostic generation.
    /// </summary>
    [TestMethod]
    public void Compile_DenseInvalidSource_StopsAtLineAndDiagnosticBudgets()
    {
        THelpSourceCompiler diagnosticLimited = new(new THelpCompilerOptions
        {
            MaxSourceLines = 100,
            MaxDiagnostics = 8
        });
        string denseSource = ".topic One=1\n" + string.Join('\n', Enumerable.Repeat("}", 40));
        using MemoryStream denseStream = new(Encoding.UTF8.GetBytes(denseSource));

        THelpCompilationResult fromString = diagnosticLimited.Compile(denseSource, "dense-string.topic");
        THelpCompilationResult fromStream = diagnosticLimited.Compile(denseStream, "dense-stream.topic");
        THelpCompilationResult singleLine = diagnosticLimited.Compile(".topic One=1\n" + new string('}', 40), "dense-line.topic");
        THelpCompilationResult tooManyLines = new THelpSourceCompiler(new THelpCompilerOptions
        {
            MaxSourceLines = 4
        }).Compile("one\ntwo\nthree\nfour\nfive", "lines.topic");
        THelpCompilationResult exactLimit = new THelpSourceCompiler(new THelpCompilerOptions
        {
            MaxDiagnostics = 1
        }).Compile(".topic One=1\n}", "exact.topic");

        foreach (THelpCompilationResult result in new[] { fromString, fromStream, singleLine })
        {
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.HelpFile);
            Assert.IsEmpty(result.Symbols);
            Assert.HasCount(8, result.Diagnostics);
            Assert.AreEqual("THC009", result.Diagnostics[^1].Code);
            Assert.IsTrue(result.Diagnostics.Take(7).All(item => item.Code == "THC006"));
        }

        Assert.HasCount(1, tooManyLines.Diagnostics);
        Assert.AreEqual("THC009", tooManyLines.Diagnostics[0].Code);
        Assert.IsNull(tooManyLines.HelpFile);
        Assert.HasCount(1, exactLimit.Diagnostics);
        Assert.AreEqual("THC006", exactLimit.Diagnostics[0].Code);
    }

    /// <summary>
    /// Prueft, dass Alias-Kopien vor der Modellerzeugung atomisch begrenzt werden.
    ///
    /// Verifies that alias copies are bounded atomically before model generation.
    /// </summary>
    [TestMethod]
    public void Compile_AliasExpansionBeyondConfiguredLimit_ReturnsAtomicDiagnostic()
    {
        const string source = ".topic A=1,B=2\n{x:A}{x:A}";
        THelpSourceCompiler rejectingCompiler = new(new THelpCompilerOptions { MaxExpandedModelItems = 7 });
        THelpSourceCompiler acceptingCompiler = new(new THelpCompilerOptions { MaxExpandedModelItems = 8 });
        using MemoryStream stream = new(Encoding.UTF8.GetBytes(source));

        THelpCompilationResult rejected = rejectingCompiler.Compile(stream);
        THelpCompilationResult accepted = acceptingCompiler.Compile(source);

        Assert.IsFalse(rejected.Success);
        Assert.IsNull(rejected.HelpFile);
        Assert.IsEmpty(rejected.Symbols);
        Assert.IsTrue(rejected.Diagnostics.Any(item => item.Code == "THC009"));
        Assert.IsTrue(accepted.Success);
        Assert.HasCount(2, accepted.HelpFile!.Topics);
    }

    /// <summary>
    /// Prueft die Standardgrenze gegen eine kleine Quelle mit starker Alias-Vermehrung.
    ///
    /// Verifies the default limit against a small source with strong alias amplification.
    /// </summary>
    [TestMethod]
    public void Compile_DefaultLimitRejectsAmplifiedAliasReferenceModel()
    {
        string aliases = string.Join(",", Enumerable.Range(0, 200).Select(index => $"A{index}={index + 1}"));
        string references = string.Concat(Enumerable.Repeat("{x:A0}", 600));

        THelpCompilationResult result = new THelpSourceCompiler().Compile($".topic {aliases}\n{references}");

        Assert.IsFalse(result.Success);
        Assert.IsNull(result.HelpFile);
        Assert.IsTrue(result.Diagnostics.Any(item => item.Code == "THC009"));
    }

    /// <summary>
    /// Prueft Leerquelle, Zeilenenden und fehlenden finalen Zeilenumbruch.
    ///
    /// Verifies empty input, line endings, and a missing final newline.
    /// </summary>
    [TestMethod]
    public void Compile_EmptyAndLineEndingInputs_AreHandledDeterministically()
    {
        THelpSourceCompiler compiler = new();

        THelpCompilationResult empty = compiler.Compile(string.Empty);
        THelpCompilationResult lf = compiler.Compile(".topic One=1\nBody");
        THelpCompilationResult crlf = compiler.Compile(".topic One=1\r\nBody");

        Assert.IsTrue(empty.Diagnostics.Any(item => item.Code == "THC001"));
        Assert.IsTrue(lf.Success);
        Assert.IsTrue(crlf.Success);
        Assert.AreEqual(lf.HelpFile!.Lookup(1)!.Paragraphs[0], crlf.HelpFile!.Lookup(1)!.Paragraphs[0]);
    }

    private static string DescribeTopic(THelpTopic topic)
    {
        return $"{topic.Context}|{topic.Title}|{string.Join(";", topic.Paragraphs)}|" +
               string.Join(";", topic.CrossReferences.Select(item => $"{item.TargetContext}:{item.Label}:{item.Offset}:{item.Length}"));
    }
}
