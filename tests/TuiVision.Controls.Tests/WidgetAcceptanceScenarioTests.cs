// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Framework-First Akzeptanztests für Widget- und Collections-Szenarien (US3).
/// Alle Szenarien laufen vollständig innerhalb des Controls-Schicht-Testprojekts
/// ohne externe Beispielprojekte zu benötigen.
///
/// Framework-first acceptance tests for widget and collections scenarios (US3).
/// All scenarios run entirely within the Controls-layer test project without
/// requiring any external example projects.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class WidgetAcceptanceScenarioTests
{
    /// <summary>
    /// Setzt Zwischenablage und History vor jedem Test zurück.
    ///
    /// Resets clipboard and history before each test.
    /// </summary>
    [TestInitialize]
    public void TestInitialize()
    {
        ManagedClipboard.Clear();
        THistory.ClearAll();
    }

    /// <summary>
    /// Prüft, dass TListViewer Fokusnavigation per Tastatur isoliert verarbeitet.
    ///
    /// Verifies that TListViewer processes focus navigation via keyboard in isolation.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_ListViewer_FocusNavigation_WorksInIsolation()
    {
        TListBox listBox = ControlsWidgetTestContext.CreateListBox(["Alpha", "Beta", "Gamma"]);

        listBox.HandleEvent(ControlEventFactory.CreateArrowDown());
        listBox.HandleEvent(ControlEventFactory.CreateArrowDown());

        Assert.AreEqual(2, listBox.FocusedItem);
    }

    /// <summary>
    /// Prüft, dass TComboBox den vollständigen Dropdown-Auswahl-Schließ-Zyklus durchläuft.
    ///
    /// Verifies that TComboBox completes the full open-select-close cycle.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_ComboBox_DropDownCycle_SelectAndClose()
    {
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["Apple", "Banana", "Cherry"]);

        combo.OpenDropDown();
        Assert.IsTrue(combo.DropDownOpen);

        combo.SelectIndex(1);

        Assert.IsFalse(combo.DropDownOpen);
        Assert.AreEqual("Banana", combo.Data);
        Assert.AreEqual(1, combo.SelectedIndex);
    }

    /// <summary>
    /// Prüft, dass TProgressBar korrekt von Running über 50% bis Completed fortschreitet.
    ///
    /// Verifies that TProgressBar correctly progresses from Running through 50% to Completed.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_ProgressBar_RunningToCompleted_StateTransition()
    {
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 100);

        Assert.AreEqual(ProgressBarState.Running, bar.BarState);

        bar.SetValue(50);
        Assert.AreEqual(ProgressBarState.Running, bar.BarState);
        Assert.AreEqual(50, bar.Value);

        bar.SetValue(100);
        Assert.AreEqual(ProgressBarState.Completed, bar.BarState);
    }

    /// <summary>
    /// Prüft, dass TParamText die Anzeige nach SetValues aktualisiert.
    ///
    /// Verifies that TParamText refreshes the display after SetValues.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_ParamText_ValueRefresh_UpdatesDisplay()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("User: {0}", new TRect(0, 0, 20, 1));
        TGroup owner = ControlTestContext.AttachToOwner(pt, new TRect(0, 0, 25, 3));

        pt.SetValues("Alice");
        owner.Draw();

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);
        ControlBufferAssert.AssertTextAt(buffer, 0, 0, "User: Alice");
    }

    /// <summary>
    /// Prüft, dass Zwischenablage-Ausschneiden und Einfügen zwischen zwei TInputLine-Instanzen funktioniert.
    ///
    /// Verifies that clipboard cut-and-paste works between two TInputLine instances.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_ManagedClipboard_CutAndPaste_RoundTrip()
    {
        TInputLine source = new(new TRect(0, 0, 20, 1), 30);
        TInputLine target = new(new TRect(0, 0, 20, 1), 30);

        // Type into source
        foreach (char ch in "Transfer")
        {
            source.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ch));
        }

        // Cut from source
        source.HandleEvent(ControlEventFactory.CreateCtrlX());

        Assert.AreEqual(string.Empty, source.Data);
        Assert.AreEqual("Transfer", ManagedClipboard.GetText());

        // Paste into target
        target.HandleEvent(ControlEventFactory.CreateCtrlV());

        Assert.AreEqual("Transfer", target.Data);
    }

    /// <summary>
    /// Prüft, dass TFileInputLine Einträge korrekt speichert und Most-Recent-First zurückgibt.
    ///
    /// Verifies that TFileInputLine stores entries and recalls them in most-recent-first order.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_History_SessionRecall_MostRecentFirst()
    {
        TFileInputLine fileLine = new(new TRect(0, 0, 40, 1), 100, "session-recall");

        fileLine.Data = "first.txt";
        fileLine.CommitToHistory();

        fileLine.Data = "second.txt";
        fileLine.CommitToHistory();

        IReadOnlyList<string> entries = fileLine.Recall();

        Assert.HasCount(2, entries);
        Assert.AreEqual("second.txt", entries[0]);
        Assert.AreEqual("first.txt", entries[1]);
    }

    /// <summary>
    /// Prüft, dass alle neuen Widget-Typen ohne Fehler an einen Owner angehängt und gezeichnet werden können.
    ///
    /// Verifies that all new widget types can be attached to an owner and drawn without error.
    /// </summary>
    [TestMethod]
    public void WidgetAcceptance_AllWidgets_FirstRedraw_VisibleInOwnerBuffer()
    {
        // TProgressBar
        TProgressBar bar = ControlsWidgetTestContext.CreateProgressBar(0, 10, new TRect(0, 0, 10, 1));
        bar.SetValue(5);
        TGroup barOwner = ControlTestContext.AttachToOwner(bar, new TRect(0, 0, 15, 3));
        TConsoleBuffer barBuffer = ControlTestContext.GetBufferSnapshot(barOwner);
        Assert.AreNotEqual('\0', barBuffer[0, 0].Glyph);

        // TParamText
        TParamText pt = ControlsWidgetTestContext.CreateParamText("Test {0}", new TRect(0, 0, 15, 1));
        pt.SetValues("OK");
        TGroup ptOwner = ControlTestContext.AttachToOwner(pt, new TRect(0, 0, 20, 3));
        TConsoleBuffer ptBuffer = ControlTestContext.GetBufferSnapshot(ptOwner);
        Assert.AreNotEqual('\0', ptBuffer[0, 0].Glyph);

        // TComboBox
        TComboBox combo = ControlsWidgetTestContext.CreateComboBox(["X"], bounds: new TRect(0, 0, 10, 1));
        combo.Data = "X";
        TGroup comboOwner = ControlTestContext.AttachToOwner(combo, new TRect(0, 0, 15, 3));
        TConsoleBuffer comboBuffer = ControlTestContext.GetBufferSnapshot(comboOwner);
        Assert.AreEqual('X', comboBuffer[0, 0].Glyph);
    }
}
