// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Editor-Hosts, Indikatoren und Shell-Integration.
///
/// Tests for editor hosts, indicators, and shell integration.
/// </summary>
[TestClass]
[DoNotParallelize]
public sealed class TEditWindowTests
{
    /// <summary>
    /// Setzt die gemeinsame Zwischenablage vor jedem Test zurueck.
    ///
    /// Resets the shared clipboard before each test.
    /// </summary>
    [TestInitialize]
    public void ResetClipboard()
    {
        TEditor.ClipboardText = string.Empty;
    }

    /// <summary>
    /// Prueft, dass der Indikator Cursorposition und Insert-Modus zeigt.
    ///
    /// Verifies that the indicator shows cursor position and insert mode.
    /// </summary>
    [TestMethod]
    public void TEditWindow_Indicator_TracksEditorState()
    {
        TMemo memo = new(new TRect(0, 0, 10, 3));
        TEditWindow window = new(new TRect(0, 0, 20, 6), memo, "Memo");

        memo.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));

        StringAssert.Contains(window.Indicator.Caption, "Ln 1");
        StringAssert.Contains(window.Indicator.Caption, "INS");
        StringAssert.Contains(window.Indicator.Caption, "*");
    }

    /// <summary>
    /// Prueft Safe-Close mit expliziter Verwerfungsentscheidung.
    ///
    /// Verifies safe close with an explicit discard decision.
    /// </summary>
    [TestMethod]
    public void TEditWindow_Close_ModifiedDocumentRequiresExplicitDecision()
    {
        TMemo memo = new(new TRect(0, 0, 10, 3));
        TEditWindow window = new(new TRect(0, 0, 20, 6), memo, "Memo");
        TGroup owner = ControlTestContext.AttachToOwner(window, new TRect(0, 0, 30, 10));
        owner.SetFocus(window);
        memo.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));
        window.ConfirmDiscard = () => false;

        TEvent reject = ControlEventFactory.CreateCommand(ShellCommandIds.cmClose);
        window.HandleEvent(reject);

        Assert.IsNotNull(window.Owner);

        window.ConfirmDiscard = () => true;
        TEvent accept = ControlEventFactory.CreateCommand(ShellCommandIds.cmClose);
        window.HandleEvent(accept);

        Assert.IsNull(window.Owner);
    }

    /// <summary>
    /// Prueft Event-Loop-Dispatch, Fokuswechsel, Menueausfuehrung und Statuszeilen-Routing.
    ///
    /// Verifies event-loop dispatch, focus transitions, menu execution, and status-line routing.
    /// </summary>
    [TestMethod]
    public void TEditWindow_ShellIntegration_RoutesCommandsThroughApplication()
    {
        EditorShellTestContext.TestApplication application = EditorShellTestContext.CreateApplication();
        TMemo memo = new(new TRect(0, 0, 20, 5));
        memo.LoadText("abcdef");
        memo.Select(0, 3);
        TEditWindow window = new(new TRect(1, 1, 30, 10), memo, "Memo");
        application.ShowOnDesktop(window);

        application.HandleEvent(ControlEventFactory.CreateBroadcast(ShellCommandIds.cmFocusChanged, window));
        application.HandleEvent(ControlEventFactory.CreateCommand(ShellCommandIds.cmCopy));
        application.HandleEvent(ControlEventFactory.CreateCommand(ShellCommandIds.cmCut));

        Assert.IsNotNull(application.StatusLine!.Items);
        Assert.AreEqual(ShellCommandIds.cmSave, application.StatusLine.Items.Command);

        application.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmPaste));
        application.Run();

        Assert.AreEqual("abc", TEditor.ClipboardText);
    }
}
