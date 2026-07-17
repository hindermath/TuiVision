// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Wave5;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft den geschlossenen TP7-Resource- und Generatorpfad.
///
/// Verifies the closed TP7 resource and generator path.
/// </summary>
[TestClass]
public sealed class Tp7ResourceSmokeTests : ExampleTestBase
{
    /// <summary>Prüft kontrollierte Erzeugung und exakte sichtbare Rekonstruktion. / Verifies controlled generation and exact visible reconstruction.</summary>
    [TestMethod]
    public void Tp7Resources_AppLoops_Generate_And_Load_Exact_Records()
    {
        string root = CreateRoot();
        try
        {
            Tp7ResourceGeneratorApp generator = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            generator.QueueEvents([TEvent.CreateCommand(
                Tp7ResourceGeneratorApp.CmGenerate,
                new Tp7ResourceGeneratorApp.GenerateRequest("tp7.tvr"))]);
            AssertSmokeRunCompletes(() => generator.Run());

            Assert.IsFalse(generator.GenerationRejected);
            Assert.IsNotNull(generator.GeneratedBytes);
            Assert.IsTrue(File.Exists(Path.Combine(root, "tp7.tvr")));
            AssertViewTreeProofFromAppLoop(generator.LastVisibleComponentKind, nameof(TDialog), "TP7 generator dialog");
            Assert.AreEqual(nameof(TInputLine), generator.FocusedControlKind);
            Assert.IsGreaterThanOrEqualTo(3, generator.VisibleControlCount);
            Assert.AreEqual(100, generator.ProgressPercent);
            AssertRenderedRegionContainsFromAppLoop(generator.Driver.BackBuffer, generator.LastVisibleRegion, "generated", "TP7 generator cells");

            Tp7ResourceDemoApp demo = new(DefaultBounds(), headless: true);
            demo.QueueEvents([TEvent.CreateCommand(Tp7ResourceDemoApp.CmLoadResources, generator.GeneratedBytes)]);
            AssertSmokeRunCompletes(() => demo.Run());

            Assert.IsTrue(demo.LoadSucceeded);
            Assert.AreEqual("TP7 Resource Dialog", demo.DialogTitle);
            Assert.AreEqual("~D~emo", demo.MenuLabel);
            Assert.AreEqual("~F1~ Help", demo.StatusLabel);
            AssertViewTreeProofFromAppLoop(demo.LastVisibleComponentKind, nameof(TDialog), "TP7 resource dialog");
            Assert.AreEqual(nameof(TButton), demo.FocusedControlKind);
            AssertRenderedRegionContainsFromAppLoop(demo.Driver.BackBuffer, demo.LastVisibleRegion, "TP7 Resource Dialog", "TP7 resource dialog cells");
            AssertPrimaryAssertionUsedAppLoop();
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft die Generatorgrenze gegen Traversal. / Verifies the generator boundary against traversal.</summary>
    [TestMethod]
    public void Tp7ResourceGenerator_AppLoop_Rejects_Path_Outside_Owned_Root()
    {
        string root = CreateRoot();
        try
        {
            Tp7ResourceGeneratorApp generator = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            generator.QueueEvents([TEvent.CreateCommand(
                Tp7ResourceGeneratorApp.CmGenerate,
                new Tp7ResourceGeneratorApp.GenerateRequest("../escape.tvr"))]);

            AssertSmokeRunCompletes(() => generator.Run());

            Assert.IsTrue(generator.GenerationRejected);
            Assert.IsNull(generator.GeneratedPath);
            Assert.IsFalse(File.Exists(Path.Combine(root, "..", "escape.tvr")));
            AssertVisibleContainsFromAppLoop(generator.LastStatusMessage, "outside", "TP7 generator traversal status");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft doppelte Keys, unbekannte Typen und ungültige Längen einzeln. / Verifies duplicate keys, unknown types, and invalid lengths separately.</summary>
    [TestMethod]
    public void Tp7ResourceDemo_AppLoop_Rejects_Malformed_Matrix_Atomically()
    {
        byte[] valid = GenerateValidBytes();
        (string Name, byte[] Bytes)[] cases =
        [
            ("duplicate-key", ReplaceAscii(valid, "Status", "Dialog")),
            ("unknown-type", ReplaceAscii(valid, "tuivision.dialog-description.v1", "tuivision.badbad-description.v1")),
            ("negative-length", SetFirstPayloadLength(valid, -1))
        ];

        foreach ((string name, byte[] bytes) in cases)
        {
            Tp7ResourceDemoApp app = new(DefaultBounds(), headless: true);
            app.QueueEvents([TEvent.CreateCommand(Tp7ResourceDemoApp.CmLoadResources, bytes)]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsFalse(app.LoadSucceeded, name);
            Assert.IsTrue(app.RejectedWithoutPartialModel, name);
            AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "rejected", $"TP7 resource rejection cells {name}");
        }

        RecordDirectHelperUsage(DirectHelperUsage.SetupOnly);
        AssertDirectHelperUsage(DirectHelperUsage.SetupOnly);
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prüft beide app-spezifischen Descriptions und enge Dialoglayouts.
    ///
    /// Verifies both app-specific Descriptions and constrained dialog layouts.
    /// </summary>
    [TestMethod]
    public void Tp7Resources_AppLoops_Show_Descriptions_In_Constrained_Viewport()
    {
        Tp7ResourceDemoApp demo = new(DefaultBounds(48, 16), headless: true);
        demo.QueueEvents([DescriptionKey()]);
        AssertSmokeRunCompletes(() => demo.Run());
        AssertVisibleContainsFromAppLoop(demo.LastDescriptionText, "exakten Namen", "Resource Demo Description key boundary");
        AssertRenderedContainsFromAppLoop(demo.Driver.BackBuffer, "Help -> Description", "Resource Demo constrained Description");

        Tp7ResourceGeneratorApp generator = new(DefaultBounds(48, 16), headless: true);
        generator.QueueEvents([DescriptionKey()]);
        AssertSmokeRunCompletes(() => generator.Run());
        AssertVisibleContainsFromAppLoop(generator.LastDescriptionText, "kontrollierten Root", "Resource Generator Description root boundary");
        AssertRenderedContainsFromAppLoop(generator.Driver.BackBuffer, "Help -> Description", "Resource Generator constrained Description");
    }

    private static string CreateRoot()
    {
        string root = Path.Combine(Path.GetTempPath(), $"tuivision-tp7resources-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        return root;
    }

    private static byte[] GenerateValidBytes()
    {
        string root = CreateRoot();
        try
        {
            Tp7ResourceGeneratorApp generator = new(new TRect(0, 0, 80, 25), headless: true, allowedOutputDirectory: root);
            generator.QueueEvents([TEvent.CreateCommand(
                Tp7ResourceGeneratorApp.CmGenerate,
                new Tp7ResourceGeneratorApp.GenerateRequest("fixture.tvr"))]);
            generator.Run();
            return generator.GeneratedBytes ?? [];
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    private static byte[] ReplaceAscii(byte[] source, string oldValue, string newValue)
    {
        byte[] result = source.ToArray();
        byte[] oldBytes = Encoding.UTF8.GetBytes(oldValue);
        byte[] newBytes = Encoding.UTF8.GetBytes(newValue);
        Assert.HasCount(oldBytes.Length, newBytes);
        int index = result.AsSpan().IndexOf(oldBytes);
        Assert.IsGreaterThanOrEqualTo(0, index);
        newBytes.CopyTo(result, index);
        return result;
    }

    private static byte[] SetFirstPayloadLength(byte[] source, int length)
    {
        byte[] result = source.ToArray();
        using MemoryStream stream = new(result, writable: true);
        using BinaryReader reader = new(stream, Encoding.UTF8, leaveOpen: true);
        _ = reader.ReadInt32();
        _ = reader.ReadString();
        using BinaryWriter writer = new(stream, Encoding.UTF8, leaveOpen: true);
        writer.Write(length);
        return result;
    }

    private static TEvent DescriptionKey() =>
        TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B));
}
