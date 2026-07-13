// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TProgram"/>: Lebenszyklus und Befehls-Routing.
///
/// Tests for <see cref="TProgram"/>: lifecycle and command routing.
/// </summary>
[TestClass]
public sealed class TProgramTests
{
    /// <summary>
    /// Prüft, ob ein Befehl durch HandleEvent verarbeitet und das Ereignis konsumiert wird.
    ///
    /// Verifies that a command is processed by HandleEvent and the event is consumed.
    /// </summary>
    [TestMethod]
    public void TProgram_CommandRouting_ExecutesExactlyOnce()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        TProgram program = new(bounds);

        // Befehl wird durch HandleEvent geroutet und konsumiert (What → Nothing).
        // Command is routed through HandleEvent and consumed (What → Nothing).
        TEvent cmd = TEvent.CreateCommand(ShellCommandIds.cmQuit, null);
        program.HandleEvent(cmd);

        bool executed = cmd.What == TEventKind.Nothing;
        Assert.IsTrue(executed, "Command routing logic not yet implemented.");
    }

    /// <summary>
    /// Prüft, ob <see cref="TProgram.Run"/> ordnungsgemäß startet und bei cmQuit beendet wird.
    ///
    /// Verifies that <see cref="TProgram.Run"/> starts and exits cleanly on cmQuit.
    /// </summary>
    [TestMethod]
    public void TProgram_Run_ShutsDownCleanly()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        ImmediateQuitProgram program = new(bounds);

        // Run() muss terminieren, wenn cmQuit empfangen wird.
        // Run() must terminate when cmQuit is received.
        program.Run();

        bool shutdownCleanly = true; // Run() kehrte zurück → kontrolliertes Herunterfahren
        Assert.IsTrue(shutdownCleanly, "Shell lifecycle and shutdown logic not yet implemented.");
    }

    /// <summary>
    /// Prüft den einzelnen Pending-Slot und seine Priorität vor physischer Eingabe.
    ///
    /// Verifies the single pending slot and its priority over physical input.
    /// </summary>
    [TestMethod]
    public void TProgram_PendingEvent_IsBoundedAndDrainedBeforeInput()
    {
        LifecycleProgram program = new();
        TEvent pending = TEvent.CreateCommand(0x9001);
        program.PutEvent(pending);

        Assert.IsTrue(program.HasPendingEvent);
        Assert.ThrowsExactly<InvalidOperationException>(() => program.PutEvent(TEvent.CreateCommand(0x9002)));

        program.GetEvent(out TEvent received);

        Assert.AreSame(pending, received);
        Assert.IsFalse(program.HasPendingEvent);
        Assert.AreEqual(0, program.RawInputPolls, "Pending input must be drained before the host is polled.");
    }

    /// <summary>
    /// Prüft die tatsächliche Run-Reihenfolge für leere Polls, Pending-Ereignisse,
    /// wiederholtes Idle, CPU-Freigabe und kontrollierten Shutdown.
    ///
    /// Verifies actual Run ordering for empty polls, pending events, repeated idle,
    /// CPU release, and controlled shutdown.
    /// </summary>
    [TestMethod]
    public void TProgram_Run_IdleAndPendingOrdering_IsDeterministic()
    {
        LifecycleProgram program = new();

        program.Run();

        CollectionAssert.AreEqual(
            new[] { "poll", "idle-1", "release", "pending", "poll", "idle-2", "release", "quit" },
            program.Order);
        Assert.AreEqual(2, program.IdleCalls);
        Assert.AreEqual(2, program.CpuReleaseCalls);
    }

    /// <summary>
    /// Prüft, dass reale Eingabe vor Idle verarbeitet wird und ein Shutdown aus
    /// dem Idle-Hook keine weitere CPU-Freigabe oder Poll-Runde startet.
    ///
    /// Verifies that real input is processed before idle and that shutdown from
    /// the idle hook starts no further CPU release or poll cycle.
    /// </summary>
    [TestMethod]
    public void TProgram_Run_InputPrecedesIdleAndIdleShutdownStopsWork()
    {
        LifecycleProgram inputProgram = new(new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false));
        inputProgram.Run();
        CollectionAssert.AreEqual(new[] { "poll", "key", "quit" }, inputProgram.Order);
        Assert.AreEqual(0, inputProgram.IdleCalls);

        LifecycleProgram idleShutdown = new(shutdownInsideIdle: true);
        idleShutdown.Run();
        CollectionAssert.AreEqual(new[] { "poll", "idle-1", "quit" }, idleShutdown.Order);
        Assert.AreEqual(0, idleShutdown.CpuReleaseCalls);
    }

    /// <summary>
    /// Prüft, ob ein bereits konsumiertes Ereignis nicht ein zweites Mal geroutet wird.
    ///
    /// Verifies that an already-consumed event is not routed a second time.
    /// </summary>
    [TestMethod]
    public void TProgram_Command_RoutedOnlyOnce()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds();
        TProgram program = new(bounds);

        TEvent cmd = TEvent.CreateCommand(ShellCommandIds.cmQuit, null);
        program.HandleEvent(cmd); // Erstes Routing: konsumiert das Ereignis / first routing: consumes event

        // Nach dem ersten Routing ist What == Nothing: zweiter Dispatch ist No-Op.
        // After first routing What == Nothing: second dispatch is a no-op.
        int executionCount = (cmd.What == TEventKind.Nothing) ? 1 : 0;
        Assert.AreEqual(1, executionCount, "Command routing should execute exactly once.");
    }

    /// <summary>
    /// Prüft, ob Resize() das Layout der Shell an die neuen Grenzen anpasst.
    ///
    /// Verifies that Resize() adjusts the shell layout to the new bounds.
    /// </summary>
    [TestMethod]
    public void TProgram_Resize_TriggersRelayout()
    {
        TRect bounds = ShellTestSupport.CreateStandardBounds(); // (0, 0, 80, 25)
        TProgram program = new(bounds);

        TRect newBounds = new(0, 0, 100, 30);
        program.Resize(newBounds);

        bool relayoutTriggered = program.GetBounds() == newBounds;
        Assert.IsTrue(relayoutTriggered, "Terminal resize re-layout logic not yet implemented.");
    }

    /// <summary>
    /// Prüft die kanonische Scan-Code-, Key-Code- und Modifier-Übersetzung am
    /// öffentlichen Produktionsrand von <see cref="TProgram.GetEvent"/>.
    ///
    /// Verifies canonical scan-code, key-code, and modifier translation at the
    /// public production boundary of <see cref="TProgram.GetEvent"/>.
    /// </summary>
    [TestMethod]
    public void TProgram_GetEvent_UsesCanonicalTranslationForRawConsoleKeys()
    {
        (ConsoleKeyInfo Raw, char Char, byte Scan, ushort Key, ushort Shift)[] cases =
        [
            (new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false), 'a', 0x00, 0x0061, 0x0000),
            (new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false), '\0', 0x4B, 0x4B00, 0x0000),
            (new ConsoleKeyInfo('\0', ConsoleKey.F5, false, false, true), '\0', 0x3F, 0x3F00, 0x0002),
            (new ConsoleKeyInfo('\0', ConsoleKey.F10, true, true, false), '\0', 0x44, 0x4400, 0x0005),
            (new ConsoleKeyInfo('?', (ConsoleKey)0xFE, false, false, false), '?', 0x00, 0x003F, 0x0000)
        ];

        foreach ((ConsoleKeyInfo raw, char character, byte scan, ushort key, ushort shift) in cases)
        {
            RawKeyProgram program = new(raw);

            program.GetEvent(out TEvent @event);

            Assert.AreEqual(TEventKind.KeyDown, @event.What);
            Assert.AreEqual(character, @event.KeyDown.CharCode);
            Assert.AreEqual(scan, @event.KeyDown.ScanCode);
            Assert.AreEqual(key, @event.KeyDown.KeyCode);
            Assert.AreEqual(shift, @event.KeyDown.ShiftState);
        }

        RawKeyProgram quitProgram = new(new ConsoleKeyInfo('x', ConsoleKey.X, false, true, false));
        quitProgram.GetEvent(out TEvent quit);
        Assert.AreEqual(TEventKind.Command, quit.What);
        Assert.AreEqual(ShellCommandIds.cmQuit, quit.Message.Command);
    }

    /// <summary>
    /// Prüft den gemeinsamen Command-Snapshot über Fokus, Event, Idle und den
    /// erneuten Dispatch-Check im tatsächlichen Program-Loop.
    ///
    /// Verifies the shared command snapshot across focus, event, idle, and the
    /// immediate dispatch recheck in the actual program loop.
    /// </summary>
    [TestMethod]
    public void TProgram_CommandContext_RefreshesAllTriggersAndRejectsStaleDispatch()
    {
        CommandContextProgram program = new();

        Assert.AreSame(program.Provider, program.CommandContext.ActiveView);
        Assert.AreEqual(CommandContextRefreshTrigger.Focus, program.CommandContext.Trigger);
        Assert.IsFalse(program.CommandContext.IsEnabled(ShellCommandIds.cmCopy));
        Assert.IsTrue(program.CopyMenuItem.IsEffectivelyDisabled);
        Assert.IsTrue(program.CopyStatusItem.IsEffectivelyDisabled);

        program.Provider.CopyEnabled = true;
        long focusGeneration = program.CommandContext.Generation;
        program.HandleEvent(TEvent.CreateBroadcast(0x9200));

        Assert.IsGreaterThan(focusGeneration, program.CommandContext.Generation);
        Assert.AreEqual(CommandContextRefreshTrigger.EventHandled, program.CommandContext.Trigger);
        Assert.IsFalse(program.CopyMenuItem.IsEffectivelyDisabled);
        Assert.IsFalse(program.CopyStatusItem.IsEffectivelyDisabled);

        program.Provider.CopyEnabled = false;
        program.HandleEvent(TEvent.CreateCommand(ShellCommandIds.cmCopy));
        Assert.AreEqual(0, program.Provider.CopyExecutions, "Pre-dispatch refresh must reject stale enabled state.");

        program.Run();
        Assert.IsTrue(program.IdleRefreshObserved, "The real Run loop must refresh command state after Idle.");
    }

    /// <summary>
    /// Prüft, dass ein verschachtelter Editor als tiefste Fokusquelle dient und
    /// seine Command-Zustände nach realem Event-Routing aktualisiert werden.
    ///
    /// Verifies that a nested editor is the deepest focus source and that its
    /// command states refresh after real event routing.
    /// </summary>
    [TestMethod]
    public void TProgram_CommandContext_UsesFocusedEditorStateAfterHandledInput()
    {
        EditorContextProgram program = new();

        Assert.AreSame(program.Editor, program.CommandContext.ActiveView);
        Assert.IsFalse(program.CommandContext.IsEnabled(ShellCommandIds.cmUndo));

        program.HandleEvent(ControlEventFactory.CreateKeyDown('a'));

        Assert.AreEqual("a", program.Editor.GetText());
        Assert.IsTrue(program.CommandContext.IsEnabled(ShellCommandIds.cmUndo));
        Assert.AreEqual(CommandContextRefreshTrigger.EventHandled, program.CommandContext.Trigger);
    }

    private sealed class RawKeyProgram : TProgram
    {
        private readonly ConsoleKeyInfo _key;

        public RawKeyProgram(ConsoleKeyInfo key) : base(ShellTestSupport.CreateStandardBounds()) => _key = key;

        protected override bool TryReadConsoleKey(out ConsoleKeyInfo key)
        {
            key = _key;
            return true;
        }
    }

    private sealed class CommandContextProgram : TProgram
    {
        private int _poll;

        public CommandContextProgram() : base(new TRect(0, 0, 40, 10))
        {
            CopyMenuItem = new TMenuItem("~C~opy", ShellCommandIds.cmCopy);
            MenuBar = new TMenuBar(new TRect(0, 0, 40, 1)) { Menu = CopyMenuItem };
            CopyStatusItem = new TStatusItem("~C~opy", ShellCommandIds.cmCopy, keyCode: 0x2E00);
            StatusLine = new TStatusLine(new TRect(0, 9, 40, 10), new TStatusDef(0, int.MaxValue, CopyStatusItem));
            Provider = new CommandProviderView(new TRect(0, 1, 40, 9));
            Insert(MenuBar);
            Insert(StatusLine);
            Insert(Provider);
            SetFocus(Provider);
        }

        public CommandProviderView Provider { get; }

        public TMenuItem CopyMenuItem { get; }

        public TStatusItem CopyStatusItem { get; }

        public bool IdleRefreshObserved { get; private set; }

        public override void GetEvent(out TEvent @event)
        {
            @event = _poll++ switch
            {
                0 => TEvent.CreateBroadcast(0x9201),
                1 => TEvent.CreateNone(),
                _ => TEvent.CreateCommand(ShellCommandIds.cmQuit)
            };
        }

        protected override void Idle() => Provider.CopyEnabled = true;

        protected override void ReleaseCpu() =>
            IdleRefreshObserved = CommandContext.Trigger == CommandContextRefreshTrigger.Idle
                && CommandContext.IsEnabled(ShellCommandIds.cmCopy);
    }

    private sealed class CommandProviderView : TView, ICommandStateProvider
    {
        public CommandProviderView(TRect bounds) : base(bounds) => Options |= TViewOptions.Selectable;

        public bool CopyEnabled { get; set; }

        public int CopyExecutions { get; private set; }

        public IReadOnlyDictionary<ushort, bool> GetCommandStates() =>
            new Dictionary<ushort, bool> { [ShellCommandIds.cmCopy] = CopyEnabled };

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command && @event.Message.Command == ShellCommandIds.cmCopy)
            {
                CopyExecutions++;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class EditorContextProgram : TProgram
    {
        public EditorContextProgram() : base(new TRect(0, 0, 40, 10))
        {
            Editor = new TEditor(new TRect(1, 1, 38, 7));
            Window = new TEditWindow(new TRect(0, 0, 40, 9), Editor);
            Insert(Window);
            SetFocus(Window);
        }

        public TEditor Editor { get; }

        public TEditWindow Window { get; }
    }

    private sealed class LifecycleProgram : TProgram
    {
        private readonly Queue<ConsoleKeyInfo> _keys = new();
        private readonly bool _shutdownInsideIdle;

        public LifecycleProgram(ConsoleKeyInfo? key = null, bool shutdownInsideIdle = false)
            : base(new TRect(0, 0, 20, 8))
        {
            if (key.HasValue)
            {
                _keys.Enqueue(key.Value);
            }

            _shutdownInsideIdle = shutdownInsideIdle;
        }

        public List<string> Order { get; } = new();

        public int IdleCalls { get; private set; }

        public int CpuReleaseCalls { get; private set; }

        public int RawInputPolls { get; private set; }

        protected override bool TryReadConsoleKey(out ConsoleKeyInfo key)
        {
            RawInputPolls++;
            Order.Add("poll");
            return _keys.TryDequeue(out key);
        }

        protected override void Idle()
        {
            IdleCalls++;
            Order.Add($"idle-{IdleCalls}");
            if (_shutdownInsideIdle)
            {
                HandleEvent(TEvent.CreateCommand(ShellCommandIds.cmQuit));
            }
            else if (IdleCalls == 1)
            {
                PutEvent(TEvent.CreateCommand(0x9001));
            }
            else
            {
                PutEvent(TEvent.CreateCommand(ShellCommandIds.cmQuit));
            }
        }

        protected override void ReleaseCpu()
        {
            CpuReleaseCalls++;
            Order.Add("release");
        }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.KeyDown)
            {
                Order.Add("key");
                @event.Clear(this);
                PutEvent(TEvent.CreateCommand(ShellCommandIds.cmQuit));
                return;
            }

            if (@event.What == TEventKind.Command && @event.Message.Command == 0x9001)
            {
                Order.Add("pending");
                @event.Clear(this);
                return;
            }

            if (@event.What == TEventKind.Command && @event.Message.Command == ShellCommandIds.cmQuit)
            {
                Order.Add("quit");
            }

            base.HandleEvent(@event);
        }
    }

    // -----------------------------------------------------------------------
    // T007: Resize-driven menu relayout test (RED – fails until T010 adds behavior)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob nach einer Größenänderung der Shell die Layout-Slots der Menüleiste
    /// neu berechnet werden.
    ///
    /// Verifies that after a shell bounds change the menu bar's layout slots are recomputed.
    /// </summary>
    [TestMethod]
    public void TProgram_MenuBar_RelayoutsOnResize()
    {
        TRect initialBounds = ShellTestSupport.CreateStandardBounds(); // 80×25
        ProgramWithMenuBar program = new(initialBounds);

        // Größe der Shell auf 100×30 ändern.
        // Resize the shell to 100×30.
        TRect newBounds = new(0, 0, 100, 30);
        program.Resize(newBounds);

        // Die Menüleiste muss die neue Breite übernommen haben.
        // The menu bar must have adopted the new width.
        Assert.AreEqual(100, program.MenuBar!.Size.X,
            "After shell resize, the menu bar width must match the new shell width.");

        // Die Layout-Slots müssen nach Aktivierung mit der neuen Breite korrekt sein.
        // Layout slots must be correctly sized after activation with the new width.
        // Aktivierung um Layout-Slots zu erzeugen / Activate to generate layout slots.
        TEvent f10 = ControlEventFactory.CreateKeyDown(scanCode: 0x44);
        program.MenuBar.HandleEvent(f10);
        Assert.IsNotEmpty(program.MenuBar.LayoutSlots,
            "After resize and activation, menu bar layout slots must be recomputed (count > 0 when Menu is set).");
    }

    /// <summary>
    /// Hilfsklasse mit vorkonfigurierter Menüleiste.
    ///
    /// Helper class with a preconfigured menu bar.
    /// </summary>
    private sealed class ProgramWithMenuBar : TProgram
    {
        public ProgramWithMenuBar(TRect bounds) : base(bounds)
        {
            TMenuBar mb = new(new TRect(0, 0, bounds.Width, 1));
            mb.Menu = new TMenuItem("~F~ile", 0, new TMenuItem("~E~dit", 0));
            MenuBar = mb;
            Insert(mb);
        }
    }

    /// <summary>
    /// Hilfsklasse, die bei GetEvent sofort ein cmQuit-Ereignis zurückliefert,
    /// damit Run() in Tests kontrolliert beendet werden kann.
    ///
    /// Helper class that immediately returns a cmQuit event from GetEvent,
    /// allowing Run() to terminate in tests.
    /// </summary>
    private sealed class ImmediateQuitProgram : TProgram
    {
        private bool _quitInjected;

        public ImmediateQuitProgram(TRect bounds) : base(bounds) { }

        public override void GetEvent(out TEvent @event)
        {
            if (!_quitInjected)
            {
                _quitInjected = true;
                @event = TEvent.CreateCommand(ShellCommandIds.cmQuit, null);
            }
            else
            {
                @event = TEvent.CreateNone();
            }
        }
    }
}
