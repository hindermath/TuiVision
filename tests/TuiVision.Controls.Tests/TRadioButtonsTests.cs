// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TRadioButtons"/> mit exklusiver Auswahl.
///
/// Tests for <see cref="TRadioButtons"/> with exclusive selection.
/// </summary>
[TestClass]
public sealed class TRadioButtonsTests
{
    /// <summary>
    /// Prüft, dass eine neue Auswahl die vorherige ersetzt.
    ///
    /// Verifies that a new selection replaces the previous one.
    /// </summary>
    [TestMethod]
    public void TRadioButtons_HandleEvent_SpaceSetsExclusiveSelection()
    {
        TRadioButtons radioButtons = new(new TRect(0, 0, 12, 3), ["One", "Two", "Three"]);
        radioButtons.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x50));

        radioButtons.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ' '));

        Assert.AreEqual(1u, radioButtons.Value);
    }

    /// <summary>
    /// Prüft, dass ein Radio-Cluster mit nur einer Option stabil bleibt.
    ///
    /// Verifies that a radio cluster with a single option remains stable.
    /// </summary>
    [TestMethod]
    public void TRadioButtons_Draw_WithSingleOption_RemainsStable()
    {
        TRadioButtons radioButtons = new(new TRect(0, 0, 8, 1), ["Only"]);
        TGroup owner = ControlTestContext.AttachToOwner(radioButtons, new TRect(0, 0, 12, 3));

        owner.Draw();

        Assert.AreEqual(0u, radioButtons.Value);
        Assert.AreEqual(0, radioButtons.Sel);
    }
}
