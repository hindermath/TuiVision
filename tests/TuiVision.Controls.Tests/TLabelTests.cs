// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TLabel"/> und die Fokusweitergabe über Hotkeys.
///
/// Tests for <see cref="TLabel"/> and focus forwarding via hotkeys.
/// </summary>
[TestClass]
public sealed class TLabelTests
{
    /// <summary>
    /// Prüft, dass ein Alt-Hotkey den Fokus auf das verknüpfte Control setzt.
    ///
    /// Verifies that an Alt hotkey moves focus to the linked control.
    /// </summary>
    [TestMethod]
    public void TLabel_HandleEvent_AltHotKeyMovesFocusToLinkedControl()
    {
        TGroup owner = ControlTestContext.CreateExposedGroup(new TRect(0, 0, 20, 5));
        TLabel label = new(new TRect(0, 0, 8, 1), "~N~ame:", null);
        TView target = new(new TRect(0, 1, 10, 2))
        {
            Options = TViewOptions.Selectable
        };
        TLabel linkedLabel = new(new TRect(0, 0, 8, 1), "~N~ame:", target);
        owner.Insert(linkedLabel);
        owner.Insert(target);

        linkedLabel.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'n', shiftState: 0x0004));

        Assert.AreSame(target, owner.Current);
        Assert.IsTrue(linkedLabel.Light);
    }

    /// <summary>
    /// Prüft, dass das Label den Light-Status aus dem Fokus des Ziel-Controls ableitet.
    ///
    /// Verifies that the label derives its light state from the linked control's focus.
    /// </summary>
    [TestMethod]
    public void TLabel_Draw_FocusedLinkSetsLight()
    {
        TGroup owner = ControlTestContext.CreateExposedGroup(new TRect(0, 0, 20, 5));
        TView target = new(new TRect(0, 1, 10, 2))
        {
            Options = TViewOptions.Selectable
        };
        TLabel label = new(new TRect(0, 0, 8, 1), "~N~ame:", target);
        owner.Insert(label);
        owner.Insert(target);
        owner.SetFocus(target);

        owner.Draw();

        Assert.IsTrue(label.Light);
    }
}
