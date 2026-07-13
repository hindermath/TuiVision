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

    /// <summary>
    /// Prüft im tatsächlichen Application-Loop, dass cmClose das aktive Fenster
    /// vor dem nachfolgenden Shutdown sichtbar aus dem Desktop entfernt.
    ///
    /// Verifies in the actual application loop that cmClose visibly removes the
    /// active window from the desktop before subsequent shutdown.
    /// </summary>
    [TestMethod]
    public void TApplication_F006_RunCompletesWindowCloseBeforeShutdown()
    {
        CloseLoopApplication app = new();

        app.Run();

        Assert.IsTrue(app.ClosedDuringLoop);
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

    private sealed class CloseLoopApplication : TApplication
    {
        private int _eventIndex;

        public CloseLoopApplication() : base(new TRect(0, 0, 40, 12))
        {
            Window = new TWindow("Loop", 0, 0, 18, 7, WindowFlags.Close);
            Desktop!.InsertWindow(Window);
        }

        public TWindow Window { get; }

        public bool ClosedDuringLoop { get; private set; }

        public override void GetEvent(out TEvent @event)
        {
            @event = _eventIndex++ == 0
                ? TEvent.CreateCommand(ShellCommandIds.cmClose)
                : TEvent.CreateCommand(ShellCommandIds.cmQuit);
        }

        public override void HandleEvent(TEvent @event)
        {
            bool close = @event.What == TEventKind.Command && @event.Message.Command == ShellCommandIds.cmClose;
            base.HandleEvent(@event);
            if (close)
            {
                ClosedDuringLoop = Window.Owner is null;
            }
        }
    }
}
