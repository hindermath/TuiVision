// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TButton"/> und dessen Aktivierungswege.
///
/// Tests for <see cref="TButton"/> and its activation paths.
/// </summary>
[TestClass]
public sealed class TButtonTests
{
    /// <summary>
    /// Prüft, dass Enter auf einer fokussierten Schaltfläche deren Command auslöst.
    ///
    /// Verifies that Enter on a focused button dispatches its command.
    /// </summary>
    [TestMethod]
    public void TButton_HandleEvent_EnterDispatchesCommand()
    {
        RecordingGroup owner = CreateOwner();
        TButton button = new(new TRect(1, 1, 10, 2), "~O~K", ShellCommandIds.cmOK, TButtonFlags.bfNormal);
        owner.Insert(button);
        owner.SetFocus(button);

        TEvent @event = ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C);
        owner.HandleEvent(@event);

        Assert.AreEqual(ShellCommandIds.cmOK, owner.LastCommand);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass ein Mausklick innerhalb der Schaltfläche deren Command auslöst.
    ///
    /// Verifies that a mouse click inside the button dispatches its command.
    /// </summary>
    [TestMethod]
    public void TButton_HandleEvent_MouseClickDispatchesCommand()
    {
        RecordingGroup owner = CreateOwner();
        TButton button = new(new TRect(1, 1, 10, 2), "OK", ShellCommandIds.cmOK, TButtonFlags.bfNormal);
        owner.Insert(button);

        TEvent @event = ControlEventFactory.CreateMouseDown(2, 1);
        button.HandleEvent(@event);

        Assert.AreEqual(ShellCommandIds.cmOK, owner.LastCommand);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass ein Alt-Hotkey einen unfokussierten Button aktiviert.
    ///
    /// Verifies that an Alt hotkey activates an unfocused button.
    /// </summary>
    [TestMethod]
    public void TButton_HandleEvent_AltHotKeyDispatchesWithoutDirectFocus()
    {
        RecordingGroup owner = CreateOwner();
        TInputLine input = new(new TRect(1, 1, 8, 2), 10);
        TButton button = new(new TRect(1, 3, 10, 4), "~O~K", ShellCommandIds.cmOK, TButtonFlags.bfNormal);
        owner.Insert(input);
        owner.Insert(button);
        owner.SetFocus(input);

        TEvent @event = ControlEventFactory.CreateKeyDown(charCode: 'o', shiftState: 0x0004);
        owner.HandleEvent(@event);

        Assert.AreEqual(ShellCommandIds.cmOK, owner.LastCommand);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    /// <summary>
    /// Prüft, dass deaktivierte Buttons bei der Fokusnavigation übersprungen werden.
    ///
    /// Verifies that disabled buttons are skipped during focus navigation.
    /// </summary>
    [TestMethod]
    public void TButton_SelectNext_DisabledButtonIsSkipped()
    {
        RecordingGroup owner = CreateOwner();
        TInputLine first = new(new TRect(1, 1, 8, 2), 10);
        TButton disabled = new(new TRect(1, 3, 10, 4), "Disabled", ShellCommandIds.cmCancel, TButtonFlags.bfNormal);
        TButton enabled = new(new TRect(1, 5, 10, 6), "Enabled", ShellCommandIds.cmOK, TButtonFlags.bfNormal);
        disabled.SetState(TViewState.Disabled, true);
        owner.Insert(first);
        owner.Insert(disabled);
        owner.Insert(enabled);
        owner.SetFocus(first);

        owner.SelectNext(true);

        Assert.AreSame(enabled, owner.Current);
    }

    /// <summary>
    /// Prüft die Default-Darstellung und den korrekten State-Flag-Abgleich.
    ///
    /// Verifies default rendering and synchronisation with the default state flag.
    /// </summary>
    [TestMethod]
    public void TButton_Draw_DefaultButtonUsesDefaultFrame()
    {
        TButton button = new(new TRect(1, 1, 9, 2), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        TGroup owner = ControlTestContext.AttachToOwner(button, new TRect(0, 0, 12, 4));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertCharacterAt(buffer, 1, 1, '<');
        ControlBufferAssert.AssertCharacterAt(buffer, 8, 1, '>');
        Assert.IsTrue(button.AmDefault);
        Assert.IsTrue(button.GetState(TViewState.Default));
    }

    private static RecordingGroup CreateOwner()
    {
        RecordingGroup owner = new(new TRect(0, 0, 20, 8));
        owner.SetState(TViewState.Exposed, true);
        owner.SetState(TViewState.Selected, true);
        return owner;
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
}
