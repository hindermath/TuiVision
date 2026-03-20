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

    /// <summary>
    /// Prüft, ob die Alt-Taste die Menüleiste aktiviert (contract + FR-004: "F10 or Alt").
    ///
    /// Verifies that the Alt key activates the menu bar (contract + FR-004: "F10 or Alt").
    /// </summary>
    [TestMethod]
    public void TMenuBar_Activation_WorksWithAlt()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);

        // Alt-Taste: ShiftState-Bit 0x0004 gesetzt, beliebiger ScanCode.
        // Alt key: ShiftState bit 0x0004 set, arbitrary ScanCode.
        TEvent altKey = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x00, 0x0000, 0x0004, 0x00));
        menuBar.HandleEvent(altKey);

        Assert.IsTrue(menuBar.IsMenuActive, "Menu bar should activate on Alt key press.");
    }

    /// <summary>
    /// Prüft, ob ein zweiter F10-Druck die Menüleiste wieder deaktiviert (Toggle-Verhalten).
    ///
    /// Verifies that a second F10 press deactivates the menu bar again (toggle behavior).
    /// </summary>
    [TestMethod]
    public void TMenuBar_Activation_TogglesOnSecondF10()
    {
        TRect bounds = new(0, 0, 80, 1);
        TMenuBar menuBar = new(bounds);

        TEvent f10 = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x44, 0x4400, 0, 0x44));
        menuBar.HandleEvent(f10);
        Assert.IsTrue(menuBar.IsMenuActive, "First F10 should activate menu bar.");

        TEvent f10Again = TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x44, 0x4400, 0, 0x44));
        menuBar.HandleEvent(f10Again);
        Assert.IsFalse(menuBar.IsMenuActive, "Second F10 should deactivate menu bar.");
    }
}
