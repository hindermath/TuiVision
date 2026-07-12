using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft den echten `TProgram`-Ereignispfad und einen vollständigen App-Loop
/// für Fokus, Aktivierung, Doppelklick, Drag und Tastaturfallback.
///
/// Verifies the real `TProgram` event path and a complete app loop for focus,
/// activation, double click, drag, and keyboard fallback.
/// </summary>
[TestClass]
public sealed class TProgramMouseIntegrationTests
{
    /// <summary>
    /// Prüft, dass eine Driver-Beobachtung über `TProgram.GetEvent` als
    /// kanonisches Ereignis ankommt.
    ///
    /// Verifies that a Driver observation reaches the canonical contract through
    /// `TProgram.GetEvent`.
    /// </summary>
    [TestMethod]
    public void GetEvent_QueuedMouseObservation_UsesRealProgramIngress()
    {
        MouseProofProgram app = new();
        app.ConfigureMouseCapability(EnabledCapability());
        app.Driver.QueueMouseObservation(new ConsoleMouseObservation("\x1b[<0;18;3M", 100));

        app.GetEvent(out TEvent @event);

        Assert.AreEqual(TEventKind.MouseDown, @event.What);
        Assert.AreEqual(new TPoint(17, 2), @event.Mouse.Where);
        Assert.AreEqual(TMouseButtons.Left, @event.Mouse.Buttons);
    }

    /// <summary>
    /// Prüft Fokus, genau eine Aktivierung, sichtbaren Status und gerenderte Cells
    /// im echten App-Loop.
    ///
    /// Verifies focus, exactly one activation, visible status, and rendered cells
    /// in the real app loop.
    /// </summary>
    [TestMethod]
    public void AppLoop_ClickFocusAndActivation_ProducesStateViewAndCellProof()
    {
        MouseProofProgram app = new();
        app.ConfigureMouseCapability(EnabledCapability());
        app.QueueMouse("\x1b[<0;18;3M", 100, "\x1b[<0;18;3m", 110);

        app.Run();

        Assert.AreEqual(1, app.ActivationCount);
        Assert.AreEqual("Second", app.ActivatedButtonText);
        Assert.AreEqual("TButton", app.FocusedKindAtActivation);
        Assert.AreEqual(ConsoleMouseCapabilityState.Disabled, app.Driver.MouseIngress.Capability.State);
        Assert.IsTrue(BufferText(app.Driver.BackBuffer).Contains("Mouse activated Second", StringComparison.Ordinal));
    }

    /// <summary>
    /// Prüft die Doppelklickklassifizierung über Driver, Zielauflösung, Program
    /// und konkrete View-Behandlung.
    ///
    /// Verifies double-click classification through Driver, target resolution,
    /// Program, and concrete view handling.
    /// </summary>
    [TestMethod]
    public void AppLoop_DoubleClick_ReachesSameConcreteViewOnce()
    {
        MouseProofProgram app = new();
        app.ConfigureMouseCapability(EnabledCapability());
        app.QueueMouse(
            "\x1b[<0;32;3M", 100,
            "\x1b[<0;32;3m", 110,
            "\x1b[<0;32;3M", 600);

        app.Run();

        Assert.AreEqual(2, app.DoubleView.MouseDownCount);
        Assert.AreEqual(1, app.DoubleView.DoubleClickCount);
        Assert.IsTrue(BufferText(app.Driver.BackBuffer).Contains("Double click accepted", StringComparison.Ordinal));
    }

    /// <summary>
    /// Prüft Titel-Drag und gerenderte Endposition im echten App-Loop.
    ///
    /// Verifies title dragging and the rendered final position in the real app loop.
    /// </summary>
    [TestMethod]
    public void AppLoop_TitleDrag_ChangesWindowAndRenderedRegion()
    {
        MouseProofProgram app = new();
        app.ConfigureMouseCapability(EnabledCapability());
        app.QueueMouse(
            "\x1b[<0;7;9M", 100,
            "\x1b[<32;12;12M", 110,
            "\x1b[<0;12;12m", 120);

        app.Run();

        Assert.AreEqual(new TRect(10, 11, 20, 16), app.Window.Bounds);
        Assert.IsFalse(app.Window.IsMouseDragging);
        Assert.AreEqual('┌', app.Driver.BackBuffer.GetCell(10, 11).Glyph);
    }

    /// <summary>
    /// Prüft, dass deaktivierter und nicht unterstützter Maussupport keine
    /// Tastaturaktivierung verhindert und nach Run kein aktiver Zustand bleibt.
    ///
    /// Verifies that disabled and unsupported mouse support does not prevent
    /// keyboard activation and leaves no active state after Run.
    /// </summary>
    [TestMethod]
    public void AppLoop_DisabledOrUnsupportedMouse_PreservesKeyboardFallback()
    {
        foreach (ConsoleMouseCapability capability in new[]
                 {
                     new ConsoleMouseCapability(ConsoleMouseCapabilityState.Disabled, ConsoleMouseHostFamily.MacOS, ConsoleMouseProtocol.Sgr1006, "Disabled test"),
                     new ConsoleMouseCapability(ConsoleMouseCapabilityState.Unsupported, ConsoleMouseHostFamily.Headless, ConsoleMouseProtocol.None, "Unsupported test")
                 })
        {
            MouseProofProgram app = new();
            app.ConfigureMouseCapability(capability);
            app.FocusFirstButton();
            app.QueueKeyboard(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));

            app.Run();

            Assert.AreEqual(1, app.ActivationCount);
            Assert.AreEqual("First", app.ActivatedButtonText);
            Assert.AreNotEqual(ConsoleMouseCapabilityState.Enabled, app.Driver.MouseIngress.Capability.State);
            Assert.IsTrue(BufferText(app.Driver.BackBuffer).Contains("Keyboard activated First", StringComparison.Ordinal));
        }
    }

    private static ConsoleMouseCapability EnabledCapability() => new(
        ConsoleMouseCapabilityState.Enabled,
        ConsoleMouseHostFamily.MacOS,
        ConsoleMouseProtocol.Sgr1006,
        "Controlled app-loop proof");

    private static string BufferText(TConsoleBuffer buffer)
    {
        System.Text.StringBuilder text = new();
        for (int y = 0; y < buffer.Height; y++)
        {
            for (int x = 0; x < buffer.Width; x++)
            {
                char glyph = buffer.GetCell(x, y).Glyph;
                text.Append(glyph == '\0' ? ' ' : glyph);
            }

            text.Append('\n');
        }

        return text.ToString();
    }

    private sealed class MouseProofProgram : TProgram
    {
        private const ushort FirstCommand = 2001;
        private const ushort SecondCommand = 2002;
        private readonly Queue<TEvent> _keyboardEvents = new();
        private readonly TButton _firstButton;
        private readonly TButton _secondButton;
        private readonly ProofStatusView _status;

        public MouseProofProgram() : base(new TRect(0, 0, 80, 25))
        {
            _firstButton = new TButton(new TRect(2, 2, 14, 5), "First", FirstCommand, TButtonFlags.bfNormal);
            _secondButton = new TButton(new TRect(16, 2, 28, 5), "Second", SecondCommand, TButtonFlags.bfNormal);
            DoubleView = new DoubleClickProofView(new TRect(30, 2, 46, 5), SetStatus);
            Window = new TWindow("Drag", 5, 8, 10, 5, WindowFlags.Move);
            _status = new ProofStatusView(new TRect(0, 23, 80, 24));
            Insert(_firstButton);
            Insert(_secondButton);
            Insert(DoubleView);
            Insert(Window);
            Insert(_status);
        }

        public int ActivationCount { get; private set; }

        public string ActivatedButtonText { get; private set; } = string.Empty;

        public string FocusedKindAtActivation { get; private set; } = string.Empty;

        public DoubleClickProofView DoubleView { get; }

        public TWindow Window { get; }

        public void QueueMouse(params object[] sequenceTimePairs)
        {
            for (int index = 0; index < sequenceTimePairs.Length; index += 2)
            {
                Driver.QueueMouseObservation(new ConsoleMouseObservation(
                    (string)sequenceTimePairs[index],
                    Convert.ToInt64(sequenceTimePairs[index + 1], System.Globalization.CultureInfo.InvariantCulture)));
            }
        }

        public void QueueKeyboard(params TEvent[] events)
        {
            foreach (TEvent @event in events)
            {
                _keyboardEvents.Enqueue(@event);
            }
        }

        public void FocusFirstButton() => SetFocus(_firstButton);

        public override void GetEvent(out TEvent @event)
        {
            if (Driver.HasPendingMouseObservation)
            {
                base.GetEvent(out @event);
                return;
            }

            @event = _keyboardEvents.Count > 0
                ? _keyboardEvents.Dequeue()
                : TEvent.CreateCommand(ShellCommandIds.cmQuit);
        }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command
                && @event.Message.Command is FirstCommand or SecondCommand)
            {
                ActivationCount++;
                ActivatedButtonText = @event.Message.Command == FirstCommand ? "First" : "Second";
                FocusedKindAtActivation = Current?.GetType().Name ?? "None";
                string input = Driver.MouseIngress.Capability.State == ConsoleMouseCapabilityState.Enabled ? "Mouse" : "Keyboard";
                SetStatus($"{input} activated {ActivatedButtonText}");
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }

        private void SetStatus(string text) => _status.Text = text;
    }

    private sealed class DoubleClickProofView : TView
    {
        private readonly Action<string> _setStatus;

        public DoubleClickProofView(TRect bounds, Action<string> setStatus) : base(bounds)
        {
            _setStatus = setStatus;
            Options |= TViewOptions.Selectable;
        }

        public int MouseDownCount { get; private set; }

        public int DoubleClickCount { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.MouseDown)
            {
                MouseDownCount++;
                if (@event.Mouse.DoubleClick)
                {
                    DoubleClickCount++;
                    _setStatus("Double click accepted");
                }

                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }

        public override void Draw()
        {
            GetDrawBuffer()?.WriteText(Origin.X, Origin.Y, "Double target".AsSpan());
        }
    }

    private sealed class ProofStatusView : TView
    {
        public ProofStatusView(TRect bounds) : base(bounds)
        {
        }

        public string Text { get; set; } = "Mouse proof ready";

        public override void Draw()
        {
            TConsoleBuffer? buffer = GetDrawBuffer();
            if (buffer is null)
            {
                return;
            }

            string value = Text.PadRight(Size.X);
            buffer.WriteText(Origin.X, Origin.Y, value.AsSpan(0, Size.X));
        }
    }
}
