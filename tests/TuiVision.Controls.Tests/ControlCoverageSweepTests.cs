// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Ergänzende Sweep-Tests für Randfälle und Rendering-Zustände über mehrere Controls hinweg.
///
/// Supplemental sweep tests for boundary cases and rendering states across multiple controls.
/// </summary>
[TestClass]
public sealed class ControlCoverageSweepTests
{
    /// <summary>
    /// Prüft, dass deaktivierte Buttons keine Commands auslösen.
    ///
    /// Verifies that disabled buttons do not dispatch commands.
    /// </summary>
    [TestMethod]
    public void TButton_HandleEvent_DisabledButtonDoesNotDispatchCommand()
    {
        RecordingGroup owner = new(new TRect(0, 0, 20, 6));
        owner.SetState(TViewState.Exposed, true);
        owner.SetState(TViewState.Selected, true);
        TButton button = new(new TRect(1, 1, 10, 2), "OK", ShellCommandIds.cmOK, TButtonFlags.bfNormal);
        button.SetState(TViewState.Disabled, true);
        owner.Insert(button);
        owner.SetFocus(button);

        TEvent @event = ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C);
        owner.HandleEvent(@event);

        Assert.AreEqual(0, owner.LastCommand);
        Assert.AreEqual(TEventKind.KeyDown, @event.What);
    }

    /// <summary>
    /// Prüft linksbündiges Button-Rendering innerhalb des Rahmens.
    ///
    /// Verifies left-justified button rendering inside the frame.
    /// </summary>
    [TestMethod]
    public void TButton_Draw_LeftJustifiedButtonAlignsCaptionToInnerLeft()
    {
        TButton button = new(new TRect(1, 1, 10, 2), "Go", ShellCommandIds.cmOK, TButtonFlags.bfLeftJustify);
        TGroup owner = ControlTestContext.AttachToOwner(button, new TRect(0, 0, 14, 4));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertTextAt(buffer, 1, 1, "[Go");
    }

    /// <summary>
    /// Prüft, dass der Setter von <see cref="TInputLine.Data"/> auf <see cref="TInputLine.MaxLen"/> begrenzt.
    ///
    /// Verifies that the <see cref="TInputLine.Data"/> setter clamps to <see cref="TInputLine.MaxLen"/>.
    /// </summary>
    [TestMethod]
    public void TInputLine_DataSetter_ClampsToMaxLen()
    {
        TInputLine inputLine = new(new TRect(0, 0, 5, 1), 3)
        {
            Data = "ABCDE"
        };

        Assert.AreEqual("ABC", inputLine.Data);
        Assert.AreEqual(0, inputLine.CurPos);
        Assert.AreEqual(0, inputLine.FirstPos);
    }

    /// <summary>
    /// Prüft, dass der Dialog seinen Rahmen und Titel in den Owner-Puffer zeichnet.
    ///
    /// Verifies that the dialog draws its frame and title into the owner buffer.
    /// </summary>
    [TestMethod]
    public void TDialog_Draw_RendersFrameAndTitleIntoOwnerBuffer()
    {
        TDialog dialog = new(new TRect(1, 1, 15, 6), "Dlg");
        TGroup owner = ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 20, 10));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertCharacterAt(buffer, 1, 1, '+');
        ControlBufferAssert.AssertTextAt(buffer, 2, 1, " Dlg ");
        ControlBufferAssert.AssertCharacterAt(buffer, 1, 5, '+');
    }

    /// <summary>
    /// Prüft, dass ein direktes Command-Ereignis den Dialog mit derselben Command-ID schließt.
    ///
    /// Verifies that a direct command event closes the dialog with the same command ID.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_DirectCommandEventClosesWithMatchingResult()
    {
        PassiveDialog dialog = new(new TRect(0, 0, 20, 6), "Cmd");
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 24, 10));
        dialog.Enqueue(ControlEventFactory.CreateCommand(ShellCommandIds.cmYes));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmYes, result);
    }

    private sealed class RecordingGroup : TGroup
    {
        public RecordingGroup(TRect bounds) : base(bounds)
        {
        }

        public ushort LastCommand { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command)
            {
                LastCommand = @event.Message.Command;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class PassiveDialog : TDialog
    {
        private readonly Queue<TEvent> _events = new();

        public PassiveDialog(TRect bounds, string? title) : base(bounds, title)
        {
        }

        public void Enqueue(TEvent @event) => _events.Enqueue(@event);

        protected override void GetEvent(out TEvent @event)
        {
            @event = _events.Count > 0 ? _events.Dequeue() : TEvent.CreateCommand(ShellCommandIds.cmCancel);
        }
    }
}
