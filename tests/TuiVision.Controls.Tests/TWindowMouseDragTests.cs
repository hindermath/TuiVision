using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft den einzigen Feature-020-Drag-Vertrag: ein bewegliches Fenster wird
/// am Titelrahmen gezogen, begrenzt und fail-safe beendet.
///
/// Verifies the sole Feature 020 drag contract: a movable window is dragged by
/// its title row, clamped, and ended fail-safe.
/// </summary>
[TestClass]
public sealed class TWindowMouseDragTests
{
    /// <summary>
    /// Prüft Press, mehrere Moves und Release mit festgeschriebener Endposition.
    ///
    /// Verifies press, multiple moves, and release with a committed final position.
    /// </summary>
    [TestMethod]
    public void TitleDrag_PressMovesRelease_CommitsPosition()
    {
        (TGroup owner, TWindow window) = CreateWindow();

        owner.HandleEvent(Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseMove, 8, 4, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseMove, 10, 6, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseUp, 10, 6, TMouseButtons.None));

        Assert.AreEqual(new TRect(9, 6, 19, 11), window.Bounds);
        Assert.IsFalse(window.IsMouseDragging);
    }

    /// <summary>
    /// Prüft das Clamping der vollständigen Fenstergrenzen an allen Owner-Kanten.
    ///
    /// Verifies clamping of the complete window bounds at all owner edges.
    /// </summary>
    [TestMethod]
    public void TitleDrag_OutsideOwner_ClampsCompleteWindow()
    {
        (TGroup owner, TWindow window) = CreateWindow();
        owner.HandleEvent(Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseMove, short.MaxValue, short.MaxValue, TMouseButtons.Left));
        Assert.AreEqual(new TRect(30, 10, 40, 15), window.Bounds);

        owner.HandleEvent(Mouse(TEventKind.MouseMove, short.MinValue, short.MinValue, TMouseButtons.Left));
        Assert.AreEqual(new TRect(0, 0, 10, 5), window.Bounds);
        owner.HandleEvent(Mouse(TEventKind.MouseUp, 0, 0, TMouseButtons.None));
    }

    /// <summary>
    /// Prüft, dass Körperklick, nicht bewegliche Fenster, falsche Buttons und
    /// Move ohne Press keinen Drag starten.
    ///
    /// Verifies that body clicks, non-movable windows, wrong buttons, and moves
    /// without a press do not start a drag.
    /// </summary>
    [TestMethod]
    public void InvalidDragStarts_DoNotChangeWindow()
    {
        (TGroup owner, TWindow window) = CreateWindow();
        TRect original = window.Bounds;
        owner.HandleEvent(Mouse(TEventKind.MouseDown, 6, 4, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseMove, 12, 8, TMouseButtons.Left));
        Assert.AreEqual(original, window.Bounds);

        (TGroup fixedOwner, TWindow fixedWindow) = CreateWindow(WindowFlags.None);
        fixedOwner.HandleEvent(Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Left));
        fixedOwner.HandleEvent(Mouse(TEventKind.MouseMove, 12, 8, TMouseButtons.Left));
        Assert.AreEqual(original, fixedWindow.Bounds);

        window.HandleEvent(Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Right));
        window.HandleEvent(Mouse(TEventKind.MouseMove, 12, 8, TMouseButtons.Left));
        Assert.AreEqual(original, window.Bounds);
    }

    /// <summary>
    /// Prüft Escape und Capability-Verlust als explizite Abbruchpfade.
    ///
    /// Verifies Escape and capability loss as explicit cancellation paths.
    /// </summary>
    [TestMethod]
    public void TitleDrag_EscapeOrCapabilityLoss_RestoresAndClearsState()
    {
        (TGroup owner, TWindow window) = CreateWindow();
        TRect original = window.Bounds;
        StartAndMove(owner);
        owner.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01));
        Assert.AreEqual(original, window.Bounds);
        Assert.IsFalse(window.IsMouseDragging);

        StartAndMove(owner);
        owner.HandleEvent(TEvent.CreateBroadcast(
            ShellCommandIds.cmMouseCapabilityChanged,
            ConsoleMouseCapabilityState.Disabled));
        Assert.AreEqual(original, window.Bounds);
        Assert.IsFalse(window.IsMouseDragging);
    }

    /// <summary>
    /// Prüft Disable, Removal und Shutdown als sofortige Abbruchgrenzen.
    ///
    /// Verifies disable, removal, and shutdown as immediate cancellation boundaries.
    /// </summary>
    [TestMethod]
    public void TitleDrag_DisableRemovalOrShutdown_ClearsState()
    {
        (TGroup owner, TWindow disabledWindow) = CreateWindow();
        StartAndMove(owner);
        disabledWindow.SetState(TViewState.Disabled, true);
        Assert.IsFalse(disabledWindow.IsMouseDragging);

        (TGroup removalOwner, TWindow removedWindow) = CreateWindow();
        StartAndMove(removalOwner);
        removalOwner.Remove(removedWindow);
        Assert.IsFalse(removedWindow.IsMouseDragging);

        (TGroup shutdownOwner, TWindow shutdownWindow) = CreateWindow();
        StartAndMove(shutdownOwner);
        shutdownWindow.ShutDown();
        Assert.IsFalse(shutdownWindow.IsMouseDragging);
    }

    /// <summary>
    /// Prüft, dass der bestehende Tastaturmodus neben dem Maus-Drag unverändert
    /// Enter-Commit und Escape-Restore unterstützt.
    ///
    /// Verifies that the existing keyboard mode still supports Enter commit and
    /// Escape restore alongside mouse drag.
    /// </summary>
    [TestMethod]
    public void KeyboardMoveMode_RemainsCompleteFallback()
    {
        (_, TWindow window) = CreateWindow();
        TRect original = window.Bounds;
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0004));
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4D));
        window.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));
        Assert.IsFalse(window.IsInMoveMode);
        Assert.AreNotEqual(original, window.Bounds);

        TRect committed = window.Bounds;
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0004));
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4D));
        window.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01));
        Assert.AreEqual(committed, window.Bounds);
    }

    private static (TGroup Owner, TWindow Window) CreateWindow(WindowFlags flags = WindowFlags.Move)
    {
        TGroup owner = new(new TRect(0, 0, 40, 15));
        TWindow window = new("Move", 5, 3, 10, 5, flags);
        owner.Insert(window);
        return (owner, window);
    }

    private static void StartAndMove(TGroup owner)
    {
        owner.HandleEvent(Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseMove, 10, 6, TMouseButtons.Left));
    }

    private static TEvent Mouse(TEventKind kind, int x, int y, TMouseButtons buttons) =>
        TEvent.CreateMouse(kind, buttons, false, new TPoint(x, y));
}
