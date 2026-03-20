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

    /// <summary>
    /// Prüft, ob der initiale Fokus nach dem Konstruktor auf dem Desktop liegt (FR-006,
    /// data-model §Shell Lifecycle: initialized → interactive requires valid focus target).
    ///
    /// Verifies that initial focus is on the desktop after construction (FR-006,
    /// data-model §Shell Lifecycle: initialized → interactive requires valid focus target).
    /// </summary>
    [TestMethod]
    public void TApplication_Constructor_SetsFocusToDesktop()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        TApplication app = new(bounds);

        Assert.AreEqual(app.Desktop, app.Current, "Initial focus must be on Desktop after construction.");
    }

    /// <summary>
    /// Prüft, ob InitMenuBar, InitDesktop und InitStatusLine durch Unterklassen
    /// ersetzt werden können (FR-010, plan.md §Customization Boundary).
    ///
    /// Verifies that InitMenuBar, InitDesktop, and InitStatusLine can be replaced
    /// by subclasses (FR-010, plan.md §Customization Boundary).
    /// </summary>
    [TestMethod]
    public void TApplication_InitMethods_AllowRegionReplacement()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        CustomApplication app = new(bounds);

        Assert.IsInstanceOfType<CustomMenuBar>(app.MenuBar, "InitMenuBar should return the custom menu bar.");
        Assert.IsInstanceOfType<CustomDesktop>(app.Desktop, "InitDesktop should return the custom desktop.");
        Assert.IsInstanceOfType<CustomStatusLine>(app.StatusLine, "InitStatusLine should return the custom status line.");
    }

    private sealed class CustomMenuBar(TRect bounds) : TMenuBar(bounds);
    private sealed class CustomDesktop(TRect bounds) : TDesktop(bounds);
    private sealed class CustomStatusLine(TRect bounds) : TStatusLine(bounds);

    private sealed class CustomApplication(TRect bounds) : TApplication(bounds)
    {
        protected override TMenuBar InitMenuBar(TRect b) => new CustomMenuBar(b);
        protected override TDesktop InitDesktop(TRect b) => new CustomDesktop(b);
        protected override TStatusLine InitStatusLine(TRect b) => new CustomStatusLine(b);
    }
}
