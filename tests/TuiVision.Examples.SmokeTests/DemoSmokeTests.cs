// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Demo;

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
    /// Prueft Start und breiten Controls/Dialog/Gadget-Fluss.
    ///
    /// Verifies startup and broad controls/dialog/gadget flow.
    /// </summary>
    [TestMethod]
    public void Demo_Starts_And_Runs_Broad_Controls_Dialogs_Gadgets_Flow()
    {
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
