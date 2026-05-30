// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Demo;
using TuiVision.Core;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer das breite Demo-Beispiel.
///
/// Smoke tests for the broad demo example.
/// </summary>
[TestClass]
public sealed class DemoSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Menue-/Befehlsrouting und drei sichtbare Ergebniszustaende ueber die App-Schleife.
    ///
    /// Verifies menu/command routing and three visible result states through the app loop.
    /// </summary>
    [TestMethod]
    public void Demo_AppLoop_Dispatches_Three_Visible_Command_States()
    {
        DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            DemoApp.CmBroadFlow,
            DemoApp.CmColorDisplay,
            DemoApp.CmOmissions).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "controls dialogs gadgets", "Demo app-loop broad flow");
        AssertVisibleContainsFromAppLoop(history, "color blue-on-black display", "Demo app-loop color/display");
        AssertVisibleContainsFromAppLoop(history, "omitted editor help stream", "Demo app-loop omission state");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Demo final omission window");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "omitted editor help stream", "Demo rendered omission window");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Datei-/Pfadmetadaten, Abbruch, ungueltige Pfade und manuelle Eingabe ueber die App-Schleife.
    ///
    /// Verifies file/path metadata, cancel, invalid paths, and manual entry through the app loop.
    /// </summary>
    [TestMethod]
    public void Demo_AppLoop_Dispatches_File_Metadata_Cancel_Invalid_And_Manual_Path()
    {
        using TempDirectory temp = TempDirectory.Create();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "do-not-read");
        DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.FromEvents(
            TEvent.CreateCommand(DemoApp.CmFileMetadata, new DemoApp.FileMetadataRequest(temp.Path, "*.txt")),
            TEvent.CreateCommand(DemoApp.CmCancelPath),
            TEvent.CreateCommand(DemoApp.CmInvalidPath, "\0bad"),
            TEvent.CreateCommand(DemoApp.CmManualPath, "/tmp/manual-wave2.txt")).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "notes.txt", "Demo app-loop metadata");
        AssertVisibleContainsFromAppLoop(history, "canceled", "Demo app-loop cancel");
        AssertVisibleContainsFromAppLoop(history, "invalid-path", "Demo app-loop invalid path");
        AssertVisibleContainsFromAppLoop(history, "manual-path accepted /tmp/manual-wave2.txt", "Demo app-loop manual path");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TDialog", "Demo final manual-path dialog");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "manual-path accepted", "Demo rendered manual path dialog");
        Assert.IsFalse(app.FileContentIoPerformed);
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft sichtbare Dialog-/Control-Komposition durch den App-Loop.
    ///
    /// Verifies visible dialog/control composition through the app loop.
    /// </summary>
    [TestMethod]
    public void Demo_AppLoop_Renders_Dialog_Control_Family()
    {
        DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(DemoApp.CmBroadFlow).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.VisibleText, "controls dialogs gadgets", "Demo visible dialog/control state");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TDialog", "Demo dialog/control view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "controls dialogs gadgets", "Demo dialog/control rendered region");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft sichtbare Datei-/Pfadmetadaten durch den App-Loop.
    ///
    /// Verifies visible file/path metadata through the app loop.
    /// </summary>
    [TestMethod]
    public void Demo_AppLoop_Renders_File_Path_Metadata_Family()
    {
        using TempDirectory temp = TempDirectory.Create();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "do-not-read");
        DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.FromEvents(
            TEvent.CreateCommand(DemoApp.CmFileMetadata, new DemoApp.FileMetadataRequest(temp.Path, "*.txt"))).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.VisibleText, "notes.txt", "Demo metadata visible state");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TDialog", "Demo metadata view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "notes.txt", "Demo rendered metadata dialog");
        Assert.IsFalse(app.FileContentIoPerformed);
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft sichtbare Farb-/Display-/Gadget-Komposition durch den App-Loop.
    ///
    /// Verifies visible colour/display/gadget composition through the app loop.
    /// </summary>
    [TestMethod]
    public void Demo_AppLoop_Renders_Display_Color_Gadget_Family()
    {
        DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(DemoApp.CmColorDisplay).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.VisibleText, "color blue-on-black display", "Demo color/display visible state");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Demo color/display view tree");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "blue-on-black", "Demo rendered color/display window");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft echte Statuszeile und Help -> Description durch den App-Loop.
    ///
    /// Verifies the real status line and Help -> Description through the app loop.
    /// </summary>
    [TestMethod]
    public void Demo_AppLoop_Shows_StatusLine_And_HelpDescription()
    {
        DemoApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(DemoApp.CmBroadFlow, DemoApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Help -> Description", "Demo status-line description hint");
        AssertVisibleContainsFromAppLoop(app.VisibleText, "Demo description", "Demo Help -> Description content");
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "Demo description view tree");
        AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Help -> Description", "Demo rendered status-line hint");
        AssertRenderedRegionContainsFromAppLoop(app.Driver.BackBuffer, app.LastVisibleRegion, "Demo description", "Demo rendered Help -> Description window");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Start und breiten Controls/Dialog/Gadget-Fluss.
    ///
    /// Verifies startup and broad controls/dialog/gadget flow.
    /// </summary>
    [TestMethod]
    public void Demo_Starts_And_Runs_Broad_Controls_Dialogs_Gadgets_Flow()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        DemoApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        string visible = app.RunBroadControlsDialogsGadgetsFlow();

        AssertVisibleContains(visible, "controls", "Demo controls proof");
        AssertVisibleContains(visible, "dialogs", "Demo dialogs proof");
        AssertVisibleContains(visible, "gadgets", "Demo gadgets proof");
    }

    /// <summary>
    /// Prueft reale Metadaten und Wildcard-Filter ohne Dateiinhalt-I/O.
    ///
    /// Verifies real metadata and wildcard filtering without file-content I/O.
    /// </summary>
    [TestMethod]
    public void Demo_StandardFileDialog_Shows_Real_Metadata_With_Wildcard_Filter()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        using TempDirectory temp = TempDirectory.Create();
        string path = Path.Combine(temp.Path, "notes.txt");
        File.WriteAllText(path, "do-not-read");
        DemoApp app = new(DefaultBounds(), headless: true);

        string visible = app.InspectStandardFileDialog(temp.Path, "*.txt");

        AssertVisibleContains(visible, "notes.txt", "Demo metadata file name");
        AssertVisibleContains(visible, "*.txt", "Demo wildcard state");
        Assert.IsFalse(app.FileContentIoPerformed);
    }

    /// <summary>
    /// Prueft sichtbare Entscheidung fuer manuelle Pfadeingabe.
    ///
    /// Verifies visible decision for manual path entry.
    /// </summary>
    [TestMethod]
    public void Demo_StandardFileDialog_Manual_Path_Entry_Visible_Decision()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        DemoApp app = new(DefaultBounds(), headless: true);

        string visible = app.EnterManualPath("/tmp/manual-wave2.txt");

        AssertVisibleContains(visible, "manual-path", "Demo manual path state");
        AssertVisibleContains(visible, "/tmp/manual-wave2.txt", "Demo manual path value");
    }

    /// <summary>
    /// Prueft sichtbare Abbruch- und Invalid-Path-Zustaende.
    ///
    /// Verifies visible cancel and invalid-path states.
    /// </summary>
    [TestMethod]
    public void Demo_StandardFileDialog_Cancel_And_InvalidPath_Are_Visible()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        DemoApp app = new(DefaultBounds(), headless: true);

        string canceled = app.CancelFileDialog();
        string invalid = app.RejectInvalidPath("\0bad");

        AssertVisibleContains(canceled, "canceled", "Demo cancel state");
        AssertVisibleContains(invalid, "invalid-path", "Demo invalid path state");
        Assert.IsFalse(app.FileContentIoPerformed);
    }

    /// <summary>
    /// Prueft sichtbare Farb- und Displayauswahl.
    ///
    /// Verifies visible color and display selection.
    /// </summary>
    [TestMethod]
    public void Demo_Color_And_Display_Dialog_Selection_Is_Visible()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        DemoApp app = new(DefaultBounds(), headless: true);

        string visible = app.SelectColorAndDisplay();

        AssertVisibleContains(visible, "color", "Demo color state");
        AssertVisibleContains(visible, "display", "Demo display state");
    }

    /// <summary>
    /// Prueft dokumentierte Omissionen ausserhalb von Welle 2.
    ///
    /// Verifies documented omissions outside wave 2.
    /// </summary>
    [TestMethod]
    public void Demo_Documents_Editor_Help_Stream_Terminal_Mouse_Charset_Omission()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        DemoApp app = new(DefaultBounds(), headless: true);

        string visible = app.OutOfScopeOmissions();

        AssertVisibleContains(visible, "editor", "Demo editor omission");
        AssertVisibleContains(visible, "help", "Demo help omission");
        AssertVisibleContains(visible, "stream", "Demo stream omission");
        AssertVisibleContains(visible, "terminal", "Demo terminal omission");
        AssertVisibleContains(visible, "mouse", "Demo mouse omission");
        AssertVisibleContains(visible, "charset", "Demo charset omission");
    }

    private sealed class TempDirectory : IDisposable
    {
        private TempDirectory(string path) => Path = path;

        public string Path { get; }

        public static TempDirectory Create()
        {
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "tuivision-demo-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(path);
            return new TempDirectory(path);
        }

        public void Dispose()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, recursive: true);
            }
        }
    }
}
