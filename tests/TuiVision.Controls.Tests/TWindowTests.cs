// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TWindow"/>: Schließ-Affordanz, Ctrl+W, bewachtes Escape,
/// Ctrl+F5-Verschiebe-Modus und Rückgängig-Machen des Verschiebens.
///
/// Tests for <see cref="TWindow"/>: close affordance, Ctrl+W, guarded Escape,
/// Ctrl+F5 move mode, and move-restore behavior.
/// </summary>
[TestClass]
public sealed class TWindowTests
{
    // -----------------------------------------------------------------------
    // T018-1: Close affordance visible when WindowFlags.Close is set
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob das Schließ-Symbol (×) in der Titelzeile sichtbar ist,
    /// wenn <see cref="WindowFlags.Close"/> gesetzt ist.
    ///
    /// Verifies that the close affordance (×) is visible in the title bar
    /// when <see cref="WindowFlags.Close"/> is set.
    /// </summary>
    [TestMethod]
    public void TWindow_CloseAffordance_VisibleWhenClosable()
    {
        TWindow window = new("Test", 0, 0, 20, 6, WindowFlags.Close);
        TGroup owner = ControlTestContext.AttachToOwner(window, new TRect(0, 0, 40, 12));

        TConsoleBuffer snapshot = ControlTestContext.GetBufferSnapshot(owner);

        // Das Schließ-Symbol × wird bei (0, 0) in Fenster-lokalen Koordinaten gezeichnet.
        // The close affordance × is drawn at (0, 0) in window-local coordinates.
        // In owner buffer coordinates: Origin.X + 0 = 0, Origin.Y + 0 = 0.
        bool hasCloseAffordance = false;
        for (int x = 0; x < snapshot.Width; x++)
        {
            TConsoleCell cell = snapshot[x, 0];
            if (cell.Glyph == '×')
            {
                hasCloseAffordance = true;
                break;
            }
        }

        Assert.IsTrue(hasCloseAffordance, "Window with WindowFlags.Close must show the × close affordance in the title row.");
    }

    // -----------------------------------------------------------------------
    // T018-2: Ctrl+W closes the window
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Ctrl+W den Close-Befehl auslöst, wenn das Fenster schließbar ist.
    ///
    /// Verifies that Ctrl+W triggers the close command when the window is closable.
    /// </summary>
    [TestMethod]
    public void TWindow_CtrlW_ClosesWindow()
    {
        CommandCapturingGroup owner = new(new TRect(0, 0, 40, 12));
        TWindow window = new("Test", 0, 0, 20, 6, WindowFlags.Close);
        owner.Insert(window);
        owner.SetState(TViewState.Exposed, true);

        // Ctrl+W: CharCode = '\x17' (ASCII 23), ShiftState = 0x0004 (Ctrl-Bit gesetzt)
        // Ctrl+W: CharCode = '\x17' (ASCII 23), ShiftState = 0x0004 (Ctrl bit set)
        TEvent ctrlW = ControlEventFactory.CreateKeyDown(charCode: '\x17', scanCode: 0x11, shiftState: 0x0004);
        window.HandleEvent(ctrlW);

        Assert.IsTrue(owner.ReceivedClose, "Ctrl+W on a closable window must send cmClose to the owner.");
    }

    // -----------------------------------------------------------------------
    // T018-3: Escape closes window when not consumed by child
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Escape das Fenster schließt, wenn kein Kind das Ereignis konsumiert hat.
    ///
    /// Verifies that Escape closes the window when no child has consumed the event.
    /// </summary>
    [TestMethod]
    public void TWindow_Escape_ClosesWhenNotConsumedByChild()
    {
        CommandCapturingGroup owner = new(new TRect(0, 0, 40, 12));
        TWindow window = new("Test", 0, 0, 20, 6, WindowFlags.Close);
        owner.Insert(window);
        owner.SetState(TViewState.Exposed, true);

        // Escape: CharCode = '\x1b', ScanCode = 0x01
        TEvent escape = ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01);
        window.HandleEvent(escape);

        Assert.IsTrue(owner.ReceivedClose, "Escape on a closable window (no child consuming it) must send cmClose to the owner.");
    }

    // -----------------------------------------------------------------------
    // T018-4: Escape does NOT close when consumed by a child
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Escape das Fenster NICHT schließt, wenn ein Kind das Ereignis zuvor konsumiert hat.
    ///
    /// Verifies that Escape does NOT close the window when a child has consumed the event first.
    /// </summary>
    [TestMethod]
    public void TWindow_Escape_DoesNotCloseWhenConsumedByChild()
    {
        CommandCapturingGroup owner = new(new TRect(0, 0, 40, 12));
        TWindow window = new("Test", 0, 0, 20, 6, WindowFlags.Close);
        EscapeConsumingView consumer = new(new TRect(1, 1, 10, 2));
        window.Insert(consumer);
        owner.Insert(window);
        owner.SetState(TViewState.Exposed, true);
        window.SetFocus(consumer);

        TEvent escape = ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01);
        window.HandleEvent(escape);

        Assert.IsFalse(owner.ReceivedClose,
            "Escape must NOT trigger cmClose when a child view has already consumed the event.");
    }

    // -----------------------------------------------------------------------
    // T018-5: Ctrl+F5 enters move mode
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Ctrl+F5 den Verschiebe-Modus aktiviert.
    ///
    /// Verifies that Ctrl+F5 enters move mode.
    /// </summary>
    [TestMethod]
    public void TWindow_CtrlF5_EntersMoveMode()
    {
        TWindow window = new("Test", 2, 2, 20, 6, WindowFlags.Move);
        ControlTestContext.AttachToOwner(window, new TRect(0, 0, 40, 12));

        // Ctrl+F5: ScanCode = 0x3F (F5), ShiftState = 0x0004 (Ctrl-Bit)
        TEvent ctrlF5 = ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0004);
        window.HandleEvent(ctrlF5);

        Assert.IsTrue(window.IsInMoveMode, "Ctrl+F5 on a movable window must enter move mode.");
    }

    // -----------------------------------------------------------------------
    // T018-6: Arrow keys preview movement in move mode
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Pfeiltasten im Verschiebe-Modus die Fensterposition vorschlagen.
    ///
    /// Verifies that arrow keys preview movement in move mode.
    /// </summary>
    [TestMethod]
    public void TWindow_MoveMode_ArrowPreviewsMovement()
    {
        TWindow window = new("Test", 2, 2, 20, 6, WindowFlags.Move);
        ControlTestContext.AttachToOwner(window, new TRect(0, 0, 40, 20));

        // Verschiebe-Modus starten / Enter move mode
        TEvent ctrlF5 = ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0004);
        window.HandleEvent(ctrlF5);
        Assert.IsTrue(window.IsInMoveMode);

        TRect originalBounds = window.GetBounds();

        // Pfeiltaste Rechts (ScanCode 0x4D) / Right arrow
        TEvent right = ControlEventFactory.CreateKeyDown(scanCode: 0x4D);
        window.HandleEvent(right);

        TRect newBounds = window.GetBounds();
        Assert.AreNotEqual(originalBounds, newBounds, "Arrow key in move mode must change the window position.");
        Assert.AreEqual(originalBounds.A.X + 1, newBounds.A.X, "Right arrow must move window one column to the right.");
    }

    // -----------------------------------------------------------------------
    // T018-7: Enter commits position in move mode
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Enter im Verschiebe-Modus die neue Position übernimmt.
    ///
    /// Verifies that Enter commits the new position in move mode.
    /// </summary>
    [TestMethod]
    public void TWindow_MoveMode_EnterCommitsPosition()
    {
        TWindow window = new("Test", 2, 2, 20, 6, WindowFlags.Move);
        ControlTestContext.AttachToOwner(window, new TRect(0, 0, 40, 20));

        TEvent ctrlF5 = ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0004);
        window.HandleEvent(ctrlF5);

        TEvent right = ControlEventFactory.CreateKeyDown(scanCode: 0x4D);
        window.HandleEvent(right);

        TRect movedBounds = window.GetBounds();

        // Enter beendet den Verschiebe-Modus und übernimmt die Position.
        // Enter ends move mode and commits the position.
        TEvent enter = ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C);
        window.HandleEvent(enter);

        Assert.IsFalse(window.IsInMoveMode, "Enter must exit move mode.");
        Assert.AreEqual(movedBounds, window.GetBounds(), "Position after Enter must equal position when Enter was pressed.");
    }

    // -----------------------------------------------------------------------
    // T018-8: Escape restores original position in move mode
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob Escape im Verschiebe-Modus die ursprüngliche Position wiederherstellt.
    ///
    /// Verifies that Escape in move mode restores the original position.
    /// </summary>
    [TestMethod]
    public void TWindow_MoveMode_EscapeRestoresOriginalPosition()
    {
        TWindow window = new("Test", 2, 2, 20, 6, WindowFlags.Move);
        ControlTestContext.AttachToOwner(window, new TRect(0, 0, 40, 20));

        TRect originalBounds = window.GetBounds();

        TEvent ctrlF5 = ControlEventFactory.CreateKeyDown(scanCode: 0x3F, shiftState: 0x0004);
        window.HandleEvent(ctrlF5);

        TEvent right = ControlEventFactory.CreateKeyDown(scanCode: 0x4D);
        window.HandleEvent(right);

        // Escape während des Verschiebens → ursprüngliche Position wiederherstellen.
        // Escape during move → restore the original position.
        TEvent escape = ControlEventFactory.CreateKeyDown(charCode: '\x1b', scanCode: 0x01);
        window.HandleEvent(escape);

        Assert.IsFalse(window.IsInMoveMode, "Escape must exit move mode.");
        Assert.AreEqual(originalBounds, window.GetBounds(), "Escape in move mode must restore the original bounds.");
    }

    // -----------------------------------------------------------------------
    // Helper types
    // -----------------------------------------------------------------------

    /// <summary>
    /// Testgruppe, die alle eingehenden Command-Ereignisse aufzeichnet.
    ///
    /// Test group that records all incoming command events.
    /// </summary>
    private sealed class CommandCapturingGroup : TGroup
    {
        public CommandCapturingGroup(TRect bounds) : base(bounds)
        {
            SetState(TViewState.Exposed, true);
        }

        /// <summary>
        /// Gibt an, ob ein <c>cmClose</c>-Befehl empfangen wurde.
        ///
        /// Indicates whether a <c>cmClose</c> command was received.
        /// </summary>
        public bool ReceivedClose { get; private set; }

        /// <summary>
        /// Verarbeitet eingehende Ereignisse und merkt <c>cmClose</c> vor.
        ///
        /// Processes incoming events and notes <c>cmClose</c>.
        /// </summary>
        /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command && @event.Message.Command == ShellCommandIds.cmClose)
            {
                ReceivedClose = true;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }

    /// <summary>
    /// Ansicht, die alle Escape-Ereignisse konsumiert.
    ///
    /// View that consumes all Escape events.
    /// </summary>
    private sealed class EscapeConsumingView : TView
    {
        public EscapeConsumingView(TRect bounds) : base(bounds)
        {
            Options = TViewOptions.Selectable;
        }

        /// <summary>
        /// Konsumiert Escape-Ereignisse.
        ///
        /// Consumes Escape events.
        /// </summary>
        /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
        public override void HandleEvent(TEvent @event)
        {
            base.HandleEvent(@event);
            if (@event.What == TEventKind.KeyDown && @event.KeyDown.ScanCode == 0x01)
            {
                @event.Clear(this);
            }
        }
    }
}
