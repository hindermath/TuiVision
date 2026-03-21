// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TCheckBoxes"/> mit Bitmasken-Semantik.
///
/// Tests for <see cref="TCheckBoxes"/> with bitmask semantics.
/// </summary>
[TestClass]
public sealed class TCheckBoxesTests
{
    /// <summary>
    /// Prüft, dass Leertaste das Bit des aktuellen Eintrags toggelt.
    ///
    /// Verifies that the space key toggles the bit of the current item.
    /// </summary>
    [TestMethod]
    public void TCheckBoxes_HandleEvent_SpaceTogglesCurrentBit()
    {
        TCheckBoxes checkBoxes = new(new TRect(0, 0, 12, 3), ["One", "Two", "Three"]);

        checkBoxes.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ' '));
        Assert.AreEqual(1u, checkBoxes.Value);

        checkBoxes.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ' '));
        Assert.AreEqual(0u, checkBoxes.Value);
    }
}
