// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TMenuBar"/>: Aktivierung und Navigation.
///
/// Tests for <see cref="TMenuBar"/>: activation and navigation.
/// </summary>
[TestClass]
public sealed class TMenuBarTests
{
    /// <summary>
    /// Prüft, ob ein Menüpunkt durch Tastatureingabe aktiviert werden kann.
    ///
    /// Verifies that a menu item can be activated via keyboard input.
    /// </summary>
    [TestMethod]
    public void TMenuBar_Activation_WorksWithKeyboard()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);

        // F10-Taste (ScanCode 0x44) aktiviert die Menüleiste.
        // F10 key (ScanCode 0x44) activates the menu bar.
        TEvent f10 = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x44, 0x4400, 0, 0x44));
        menuBar.HandleEvent(f10);

        bool activated = menuBar.IsMenuActive;
        Assert.IsTrue(activated, "Menu activation logic not yet implemented.");
    }
}
