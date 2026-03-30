// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TDialog"/> als modalen Koordinator.
///
/// Tests for <see cref="TDialog"/> as a modal coordinator.
/// </summary>
[TestClass]
public sealed class TDialogTests
{
    /// <summary>
    /// Prüft, dass Escape den Dialog mit <c>cmCancel</c> beendet und ein Dialog ohne fokussierbares Kind stabil bleibt.
    ///
    /// Verifies that Escape closes the dialog with <c>cmCancel</c> and that a dialog without a focusable child stays stable.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_EscapeReturnsCancelWithoutFocusableChild()
    {
        TestDialog dialog = new(new TRect(0, 0, 20, 6), "Test");
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(scanCode: 0x01));
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 24, 10));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result);
        Assert.IsNull(dialog.Current);
    }

    /// <summary>
    /// Prüft Fokus-Wrap-around via Tab und Shift-Tab.
    ///
    /// Verifies focus wrap-around via Tab and Shift-Tab.
    /// </summary>
    [TestMethod]
    public void TDialog_HandleEvent_TabNavigationWrapsAround()
    {
        TestDialog dialog = CreateDialogWithTwoButtons(out TButton okButton, out TButton cancelButton);
        dialog.SetFocus(cancelButton);

        TEvent forward = ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F);
        dialog.HandleEvent(forward);

        Assert.AreSame(okButton, dialog.Current);
        Assert.AreEqual(TEventKind.Nothing, forward.What);

        TEvent backward = ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F, shiftState: 0x0001);
        dialog.HandleEvent(backward);

        Assert.AreSame(cancelButton, dialog.Current);
        Assert.AreEqual(TEventKind.Nothing, backward.What);
    }

    /// <summary>
    /// Prüft, dass Mausereignisse außerhalb eines modalen Dialogs ignoriert werden.
    ///
    /// Verifies that mouse events outside a modal dialog are ignored.
    /// </summary>
    [TestMethod]
    public void TDialog_HandleEvent_MouseOutsideDialogIsIgnored()
    {
        TestDialog dialog = CreateDialogWithTwoButtons(out _, out _);

        TEvent @event = ControlEventFactory.CreateMouseDown(30, 30);
        dialog.HandleEvent(@event);

        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass ein Cancel-Button als Rückgabewert durchgereicht wird.
    ///
    /// Verifies that a cancel button result is returned by the dialog.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_CancelButtonReturnsCancelCommand()
    {
        TestDialog dialog = CreateDialogWithTwoButtons(out _, out TButton cancelButton);
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));
        dialog.SetFocus(cancelButton);

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result);
    }

    /// <summary>
    /// Prüft, dass Enter auf einem nicht konsumierenden Kind den Default-Button aktiviert.
    ///
    /// Verifies that Enter on a non-consuming child activates the default button.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_EnterActivatesDefaultButtonWhenChildDoesNotConsume()
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Default");
        TInputLine inputLine = new(new TRect(2, 2, 12, 3), 10);
        TButton okButton = new(new TRect(2, 4, 10, 5), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(inputLine);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        dialog.SetFocus(inputLine);
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmOK, result);
        Assert.AreSame(inputLine, dialog.Current);
    }

    /// <summary>
    /// Prüft, dass ein Kind-Control den Enter-Key konsumieren kann, ohne den Default-Button auszulösen.
    ///
    /// Verifies that a child control can consume Enter without triggering the default button.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_ChildControlCanConsumeEnterBeforeDefaultButton()
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Consume");
        EnterConsumingView consumer = new(new TRect(2, 2, 12, 3));
        TButton okButton = new(new TRect(2, 4, 10, 5), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(consumer);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        dialog.SetFocus(consumer);
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmCancel));

        ushort result = dialog.Run();

        Assert.AreEqual(1, consumer.EnterCount);
        Assert.AreEqual(ShellCommandIds.cmCancel, result);
    }

    // -----------------------------------------------------------------------
    // T019: Dialog validation tests (RED – fail until T021 adds Valid() hook)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Valid(ushort) false den Dialog offen hält (kein Schließen bei Ablehnung).
    ///
    /// Verifies that Valid(ushort) returning false keeps the dialog open.
    /// </summary>
    [TestMethod]
    public void TDialog_Valid_RejectsCloseWhenValidReturnsFalse()
    {
        RejectingDialog dialog = new(new TRect(0, 0, 20, 6), "Reject");
        TButton okButton = new(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 30, 10));

        // Befehl cmOK direkt senden – Valid() gibt false zurück, CloseDialog wird NICHT aufgerufen.
        // Send cmOK directly – Valid() returns false, CloseDialog must NOT be called.
        TEvent okCmd = ControlEventFactory.CreateCommand(ShellCommandIds.cmOK);
        dialog.HandleEvent(okCmd);

        Assert.AreEqual(0, dialog.CloseDialogCallCount,
            "Dialog must not call CloseDialog when Valid() returns false.");
    }

    /// <summary>
    /// Prüft, ob Valid(ushort) true den Dialog schließt und den Ergebniswert setzt.
    ///
    /// Verifies that Valid(ushort) returning true closes the dialog with the expected result.
    /// </summary>
    [TestMethod]
    public void TDialog_Valid_AcceptsCloseWhenValidReturnsTrue()
    {
        TestDialog dialog = new(new TRect(0, 0, 20, 6), "Accept");
        TButton okButton = new(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 30, 10));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmOK));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmCancel)); // Fallback

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmOK, result,
            "Dialog must close with cmOK when Valid() returns true.");
    }

    /// <summary>
    /// Stellt sicher, dass das bestehende Escape/OK-Verhalten nicht regressiert.
    ///
    /// Ensures that the existing Escape/OK behavior is not regressed.
    /// </summary>
    [TestMethod]
    public void TDialog_ExistingModalBehavior_NotRegressed()
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Regression");
        TButton okButton = new(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        TButton cancelButton = new(new TRect(12, 2, 24, 3), "Cancel", ShellCommandIds.cmCancel, TButtonFlags.bfNormal);
        dialog.Insert(okButton);
        dialog.Insert(cancelButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(scanCode: 0x01)); // Escape

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmCancel, result,
            "Existing Escape→cmCancel behavior must not be regressed by Valid() hook introduction.");
    }

    private static TestDialog CreateDialogWithTwoButtons(out TButton okButton, out TButton cancelButton)
    {
        TestDialog dialog = new(new TRect(0, 0, 30, 8), "Test");
        okButton = new TButton(new TRect(2, 2, 10, 3), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        cancelButton = new TButton(new TRect(12, 2, 24, 3), "Cancel", ShellCommandIds.cmCancel, TButtonFlags.bfNormal);
        dialog.Insert(okButton);
        dialog.Insert(cancelButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 40, 12));
        return dialog;
    }

    private sealed class TestDialog : TDialog
    {
        private readonly Queue<TEvent> _events = new();

        public TestDialog(TRect bounds, string? title) : base(bounds, title)
        {
        }

        public void Enqueue(TEvent @event) => _events.Enqueue(@event);

        protected override void GetEvent(out TEvent @event)
        {
            @event = _events.Count > 0 ? _events.Dequeue() : TEvent.CreateCommand(ShellCommandIds.cmCancel);
        }
    }

    /// <summary>
    /// Dialog, dessen Valid()-Methode stets <c>false</c> zurückgibt und der CloseDialog-Aufrufe zählt.
    ///
    /// Dialog whose Valid() method always returns <c>false</c> and counts CloseDialog calls.
    /// </summary>
    private sealed class RejectingDialog : TDialog
    {
        public RejectingDialog(TRect bounds, string? title) : base(bounds, title) { }

        /// <summary>
        /// Anzahl der CloseDialog-Aufrufe. / Number of CloseDialog call invocations.
        /// </summary>
        public int CloseDialogCallCount { get; private set; }

        /// <summary>
        /// Gibt stets <c>false</c> zurück, um das Schließen zu verhindern.
        ///
        /// Always returns <c>false</c> to prevent closing.
        /// </summary>
        /// <param name="command">Die Befehl-ID. / The command ID.</param>
        /// <returns><c>false</c></returns>
        protected override bool Valid(ushort command) => false;

        /// <summary>
        /// Zählt CloseDialog-Aufrufe mit.
        ///
        /// Counts CloseDialog calls.
        /// </summary>
        /// <param name="command">Die Rückgabe-Command-ID. / The command ID to return.</param>
        public new void CloseDialog(ushort command)
        {
            CloseDialogCallCount++;
            base.CloseDialog(command);
        }
    }

    private sealed class EnterConsumingView : TView
    {
        public EnterConsumingView(TRect bounds) : base(bounds)
        {
            Options = TViewOptions.Selectable;
        }

        public int EnterCount { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            base.HandleEvent(@event);
            if (@event.What == TEventKind.KeyDown && (@event.KeyDown.ScanCode == 0x1C || @event.KeyDown.CharCode == '\r'))
            {
                EnterCount++;
                @event.Clear(this);
            }
        }
    }
}
