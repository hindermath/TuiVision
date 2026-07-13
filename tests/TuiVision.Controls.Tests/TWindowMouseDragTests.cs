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
    /// Prüft Ein-Zell-Schwelle, genau eine aktive Session und Capture-Freigabe.
    ///
    /// Verifies the one-cell threshold, exactly one active session, and capture release.
    /// </summary>
    [TestMethod]
    public void TWindow_F009_OneCellThresholdAndOneCaptureAreShared()
    {
        (TGroup owner, TWindow window) = CreateWindow();
        TRect original = window.Bounds;

        owner.HandleEvent(Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Left));
        owner.HandleEvent(Mouse(TEventKind.MouseMove, 6, 3, TMouseButtons.Left));
        Assert.AreEqual(TDragSessionState.Pending, window.ActiveDragSession!.State);
        Assert.AreEqual(original, window.Bounds);

        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0002));
        Assert.AreEqual(TDragInputMode.Pointer, window.ActiveDragSession.Mode);

        owner.HandleEvent(Mouse(TEventKind.MouseMove, 7, 3, TMouseButtons.Left));
        Assert.AreEqual(TDragSessionState.Captured, window.ActiveDragSession.State);
        Assert.AreEqual(original.A.X + 1, window.Bounds.A.X);

        owner.HandleEvent(Mouse(TEventKind.MouseUp, 7, 3, TMouseButtons.None));
        Assert.IsNull(window.ActiveDragSession);
        Assert.AreEqual(TDragSessionState.Dropped, window.LastDragResult!.State);
    }

    /// <summary>
    /// Prüft opt-in Zielannahme, Ablehnung und unveränderliche Terminalergebnisse
    /// am generischen Session-Vertrag.
    ///
    /// Verifies opt-in target acceptance, rejection, and immutable terminal results
    /// at the generic session contract.
    /// </summary>
    [TestMethod]
    public void TDragSession_F009_TargetNegotiationIsExplicit()
    {
        TView source = new(new TRect(0, 0, 2, 2));
        DragTarget accepted = new(new TRect(5, 0, 8, 3), accepts: true);
        TDragSession session = new(source, "payload", new TPoint(0, 0), new TRect(0, 0, 10, 10), TDragInputMode.Keyboard, requireTarget: true);
        session.MoveBy(new TPoint(1, 0));

        TDragResult acceptedResult = session.Drop(accepted);
        Assert.AreEqual(TDragSessionState.Dropped, acceptedResult.State);
        Assert.AreSame(accepted, acceptedResult.Target);

        TDragSession rejected = new(source, null, new TPoint(0, 0), new TRect(0, 0, 10, 10), TDragInputMode.Keyboard, requireTarget: true);
        rejected.MoveBy(new TPoint(1, 0));
        TDragResult rejectedResult = rejected.Drop(new DragTarget(new TRect(5, 0, 8, 3), accepts: false));
        Assert.AreEqual(TDragSessionState.Rejected, rejectedResult.State);
        Assert.AreEqual(TDragCompletionReason.TargetRejected, rejectedResult.Reason);
    }

    /// <summary>
    /// Prüft im tatsächlichen Program-Loop identische Pointer-/Tastaturergebnisse
    /// samt gerendertem Fensterrahmen an der neuen Position.
    ///
    /// Verifies identical pointer/keyboard outcomes in the actual program loop,
    /// including a rendered window frame at the new position.
    /// </summary>
    [TestMethod]
    public void TWindow_F009_ActualRunPointerAndKeyboardRenderEquivalentResults()
    {
        DragRunProgram pointer = DragRunProgram.ForPointer();
        DragRunProgram keyboard = DragRunProgram.ForKeyboard();

        pointer.Run();
        keyboard.Run();

        Assert.AreEqual(pointer.Window.Bounds, keyboard.Window.Bounds);
        Assert.AreEqual(TDragSessionState.Dropped, pointer.Window.LastDragResult!.State);
        Assert.AreEqual(TDragSessionState.Dropped, keyboard.Window.LastDragResult!.State);
        Assert.IsTrue(pointer.RenderedMovedFrame);
        Assert.IsTrue(keyboard.RenderedMovedFrame);
    }
    /// <summary>
    /// Prüft Press, mehrere Moves und Release mit festgeschriebener Endposition.
    ///
    /// Verifies press, multiple moves, and release with a committed final position.
    /// </summary>
    [TestMethod]
    public void TitleDrag_F009_PressMovesRelease_CommitsPosition()
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
    public void TitleDrag_F009_OutsideOwner_ClampsCompleteWindow()
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
    public void InvalidDragStarts_F009_DoNotChangeWindow()
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
    public void TitleDrag_F009_EscapeOrCapabilityLoss_RestoresAndClearsState()
    {
        (TGroup owner, TWindow window) = CreateWindow();
        TRect original = window.Bounds;
        StartAndMove(owner);
        owner.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01));
        Assert.AreEqual(original, window.Bounds);
        Assert.IsFalse(window.IsMouseDragging);
        Assert.AreEqual(TDragCompletionReason.Cancelled, window.LastDragResult!.Reason);

        StartAndMove(owner);
        owner.HandleEvent(TEvent.CreateBroadcast(
            ShellCommandIds.cmMouseCapabilityChanged,
            ConsoleMouseCapabilityState.Disabled));
        Assert.AreEqual(original, window.Bounds);
        Assert.IsFalse(window.IsMouseDragging);
        Assert.AreEqual(TDragCompletionReason.CapabilityLost, window.LastDragResult!.Reason);
    }

    /// <summary>
    /// Prüft Disable, Removal und Shutdown als sofortige Abbruchgrenzen.
    ///
    /// Verifies disable, removal, and shutdown as immediate cancellation boundaries.
    /// </summary>
    [TestMethod]
    public void TitleDrag_F009_DisableRemovalOrShutdown_ClearsState()
    {
        (TGroup owner, TWindow disabledWindow) = CreateWindow();
        StartAndMove(owner);
        disabledWindow.SetState(TViewState.Disabled, true);
        Assert.IsFalse(disabledWindow.IsMouseDragging);
        Assert.AreEqual(TDragCompletionReason.Disabled, disabledWindow.LastDragResult!.Reason);

        (TGroup removalOwner, TWindow removedWindow) = CreateWindow();
        StartAndMove(removalOwner);
        removalOwner.Remove(removedWindow);
        Assert.IsFalse(removedWindow.IsMouseDragging);
        Assert.AreEqual(TDragCompletionReason.Removed, removedWindow.LastDragResult!.Reason);

        (TGroup shutdownOwner, TWindow shutdownWindow) = CreateWindow();
        StartAndMove(shutdownOwner);
        shutdownWindow.ShutDown();
        Assert.IsFalse(shutdownWindow.IsMouseDragging);
        Assert.AreEqual(TDragCompletionReason.Shutdown, shutdownWindow.LastDragResult!.Reason);
    }

    /// <summary>
    /// Prüft, dass der bestehende Tastaturmodus neben dem Maus-Drag unverändert
    /// Enter-Commit und Escape-Restore unterstützt.
    ///
    /// Verifies that the existing keyboard mode still supports Enter commit and
    /// Escape restore alongside mouse drag.
    /// </summary>
    [TestMethod]
    public void KeyboardMoveMode_F009_RemainsCompleteFallback()
    {
        (_, TWindow window) = CreateWindow();
        TRect original = window.Bounds;
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0002));
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4D));
        window.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));
        Assert.IsFalse(window.IsInMoveMode);
        Assert.AreNotEqual(original, window.Bounds);

        TRect committed = window.Bounds;
        window.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0002));
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

    private sealed class DragTarget : TView, IDragTarget
    {
        private readonly bool _accepts;

        public DragTarget(TRect bounds, bool accepts) : base(bounds) => _accepts = accepts;

        public bool CanAcceptDrop(TView source, object? payload, TPoint where) => _accepts;
    }

    private sealed class DragRunProgram : TProgram
    {
        private readonly Queue<TEvent> _events;

        private DragRunProgram(IEnumerable<TEvent> events) : base(new TRect(0, 0, 40, 15))
        {
            _events = new Queue<TEvent>(events);
            Window = new TWindow("Move", 5, 3, 10, 5, WindowFlags.Move);
            Insert(Window);
            SetFocus(Window);
        }

        public TWindow Window { get; }

        public bool RenderedMovedFrame { get; private set; }

        public static DragRunProgram ForPointer() => new(
        [
            Mouse(TEventKind.MouseDown, 6, 3, TMouseButtons.Left),
            Mouse(TEventKind.MouseMove, 9, 5, TMouseButtons.Left),
            Mouse(TEventKind.MouseUp, 9, 5, TMouseButtons.None),
            TEvent.CreateCommand(ShellCommandIds.cmQuit)
        ]);

        public static DragRunProgram ForKeyboard() => new(
        [
            ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0002),
            ControlEventFactory.CreateKeyDown(scanCode: 0x4D),
            ControlEventFactory.CreateKeyDown(scanCode: 0x4D),
            ControlEventFactory.CreateKeyDown(scanCode: 0x4D),
            ControlEventFactory.CreateKeyDown(scanCode: 0x50),
            ControlEventFactory.CreateKeyDown(scanCode: 0x50),
            ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C),
            TEvent.CreateCommand(ShellCommandIds.cmQuit)
        ]);

        public override void GetEvent(out TEvent @event)
        {
            if (_events.Count == 1)
            {
                TPoint origin = Window.Bounds.A;
                RenderedMovedFrame = Driver.BackBuffer.GetCell(origin.X, origin.Y).Glyph != ' ';
            }

            @event = _events.Dequeue();
        }

        protected override void ReleaseCpu()
        {
        }
    }
}
