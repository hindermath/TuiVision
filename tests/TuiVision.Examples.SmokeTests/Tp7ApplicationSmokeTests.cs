// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Compatibility;
using TuiVision.Core;
using TuiVision.Examples.Wave5;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft TP7-Demo, Editor und Hilfe über echte Anwendungsereignisse.
///
/// Verifies TP7 demo, editor, and help through real application events.
/// </summary>
[TestClass]
public sealed class Tp7ApplicationSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Menü, genau einen Command und begrenzte Idle-Zyklen. / Verifies menu, exactly one command, and bounded idle cycles.</summary>
    [TestMethod]
    public void Tp7Demo_AppLoop_Processes_Command_Once_And_Bounds_Idle()
    {
        Tp7DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([
            TEvent.CreateNone(),
            TEvent.CreateNone(),
            TEvent.CreateCommand(Tp7DemoApp.CmOpenWindow)
        ]);
        Assert.IsNotNull(app.MenuBar);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.AreEqual(2, app.GadgetTicks);
        Assert.AreEqual(1, app.ProcessedCommandCount);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "TP7 Demo window identity");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Demo command window", "TP7 Demo command cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "command=1", "TP7 Demo command status");
    }

    /// <summary>Prüft den sichtbaren Hilfepfad der Demo. / Verifies the visible demo help path.</summary>
    [TestMethod]
    public void Tp7Demo_AppLoop_Shows_Help_Context()
    {
        Tp7DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([TEvent.CreateCommand(Tp7DemoApp.CmShowHelp)]);

        AssertSmokeRunCompletes(() => app.Run());

        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "TP7 Demo help", "TP7 Demo help cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "help", "TP7 Demo help status");
    }

    /// <summary>Prüft Änderung und ausdrückliche Safe-Close-Ablehnung. / Verifies modification and explicit safe-close rejection.</summary>
    [TestMethod]
    public void Tp7Edit_AppLoop_Preserves_Modified_Window_When_Close_Is_Rejected()
    {
        Tp7EditApp app = new(DefaultBounds(), headless: true);
        TKeyDownEvent key = TKeyCodeTranslator.FromConsoleKey(new ConsoleKeyInfo('X', ConsoleKey.X, false, false, false));
        app.QueueEvents([
            TEvent.CreateKeyDown(key),
            TEvent.CreateCommand(Tp7EditApp.CmRequestClose, false)
        ]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.Editor.Modified);
        Assert.IsFalse(app.CloseAccepted);
        Assert.IsTrue(app.WindowPresentAfterCloseAttempt);
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "close rejected", "TP7 Edit close status");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "X", "TP7 Edit modified cells");
    }

    /// <summary>Prüft Konfliktentscheidung und kontrollierten Zielpfad. / Verifies conflict decision and controlled target path.</summary>
    [TestMethod]
    public void Tp7Edit_AppLoop_Requires_Conflict_Decision_And_Rejects_Traversal()
    {
        string root = Path.Combine(Path.GetTempPath(), $"tuivision-tp7edit-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        string target = Path.Combine(root, "lesson.txt");
        File.WriteAllText(target, "EXTERNAL");
        try
        {
            Tp7EditApp rejected = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            rejected.QueueEvents([TEvent.CreateCommand(Tp7EditApp.CmSaveOwned, new Tp7EditApp.SaveRequest("lesson.txt", false))]);
            AssertSmokeRunCompletes(() => rejected.Run());
            Assert.IsFalse(rejected.SaveAccepted);
            Assert.AreEqual(TuiVision.Controls.TFileSaveConflictKind.ReplaceExisting, rejected.LastConflictKind);
            Assert.AreEqual("EXTERNAL", File.ReadAllText(target));

            Tp7EditApp accepted = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            accepted.QueueEvents([TEvent.CreateCommand(Tp7EditApp.CmSaveOwned, new Tp7EditApp.SaveRequest("lesson.txt", true))]);
            AssertSmokeRunCompletes(() => accepted.Run());
            Assert.IsTrue(accepted.SaveAccepted);

            Tp7EditApp traversal = new(DefaultBounds(), headless: true, allowedOutputDirectory: root);
            traversal.QueueEvents([TEvent.CreateCommand(Tp7EditApp.CmSaveOwned, new Tp7EditApp.SaveRequest("../escape.txt", true))]);
            AssertSmokeRunCompletes(() => traversal.Run());
            Assert.IsFalse(traversal.SaveAccepted);
            Assert.IsFalse(File.Exists(Path.Combine(root, "..", "escape.txt")));
            AssertVisibleContainsFromAppLoop(traversal.LastStatusMessage, "outside", "TP7 Edit traversal status");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>Prüft atomare Ablehnung einer ungültigen Help-Quelle. / Verifies atomic rejection of invalid help source.</summary>
    [TestMethod]
    public void Tp7Help_AppLoop_Rejects_Invalid_Source_Without_Partial_Model()
    {
        Tp7HelpApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents([TEvent.CreateCommand(Tp7HelpApp.CmCompileInvalid)]);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsNotNull(app.LastCompilation);
        Assert.IsFalse(app.LastCompilation.Success);
        Assert.IsTrue(app.LastRejectedWithoutPartialModel);
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "rejected", "TP7 Help rejection cells");
    }

    /// <summary>Prüft bekannten Kontext und sichtbaren Fallback. / Verifies known context and visible fallback.</summary>
    [TestMethod]
    public void Tp7Help_AppLoop_Shows_Known_And_Unknown_Contexts()
    {
        Tp7HelpApp known = new(DefaultBounds(), headless: true);
        known.QueueEvents([TEvent.CreateCommand(Tp7HelpApp.CmShowKnownContext)]);
        AssertSmokeRunCompletes(() => known.Run());
        AssertViewTreeProofFromAppLoop(known.LastVisibleComponentKind, "THelpWindow", "TP7 Help known view");
        AssertRenderedRegionContainsFromAppLoop(known.Driver.BackBuffer, known.LastVisibleRegion, "Welcome", "TP7 Help known cells");

        Tp7HelpApp unknown = new(DefaultBounds(), headless: true);
        unknown.QueueEvents([TEvent.CreateCommand(Tp7HelpApp.CmShowUnknownContext)]);
        AssertSmokeRunCompletes(() => unknown.Run());
        Assert.AreEqual(999, unknown.CurrentContext);
        AssertRenderedRegionContainsFromAppLoop(unknown.Driver.BackBuffer, unknown.LastVisibleRegion, "No help topic is available", "TP7 Help fallback cells");
    }
}
