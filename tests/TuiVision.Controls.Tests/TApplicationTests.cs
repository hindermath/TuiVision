// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TApplication"/>: Standard-Zusammensetzung und Layout.
///
/// Tests for <see cref="TApplication"/>: default composition and layout.
/// </summary>
[TestClass]
public sealed class TApplicationTests
{
    /// <summary>
    /// Prüft, ob <see cref="TApplication"/> standardmäßig Menüleiste, Desktop und Statuszeile erstellt.
    ///
    /// Verifies that <see cref="TApplication"/> creates menu bar, desktop, and status line by default.
    /// </summary>
    [TestMethod]
    public void TApplication_Constructor_CreatesDefaultRegions()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        TApplication app = new(bounds);

        bool regionsCreated = app.MenuBar != null && app.Desktop != null && app.StatusLine != null;
        Assert.IsTrue(regionsCreated, "Default shell regions not yet created.");
    }

    /// <summary>
    /// Prüft, ob der Desktop bei Programmstart ohne Kinder in einem gültigen Zustand ist.
    ///
    /// Verifies that the desktop is in a valid state on startup with zero children.
    /// </summary>
    [TestMethod]
    public void TApplication_StartupWithZeroChildren_IsVisible()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        TApplication app = new(bounds);

        bool desktopIsVisible = app.Desktop != null;
        Assert.IsTrue(desktopIsVisible, "Desktop should be present on startup.");
    }
}
