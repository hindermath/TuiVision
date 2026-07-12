// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft Projektion, Eingabe, Resize und echten App-Loop des Terminal-Views.
///
/// Verifies terminal-view projection, input, resize, and the real application loop.
/// </summary>
[TestClass]
public sealed class TTerminalViewTests
{
    /// <summary>
    /// Prüft Zeichen, Farben, Cursor und textorientierte Profilmetadaten im Owner-Puffer.
    ///
    /// Verifies glyphs, colors, cursor, and text-first profile metadata in the owner buffer.
    /// </summary>
    [TestMethod]
    public void TerminalView_Draw_ProjectsSessionCursorAndStatus()
    {
        using TerminalSession session = new(12, 2);
        TerminalProfile profile = TerminalProfile.Parse("""
            { "ProfileId": "proof", "Charset": "KOI8-R", "Foreground": "Yellow" }
            """).Profile!;
        session.ApplyProfile(profile);
        session.Write("Hi");
        TTerminalView view = new(new TRect(1, 1, 13, 4), session);
        TGroup owner = ControlTestContext.AttachToOwner(view, new TRect(0, 0, 20, 6));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual('H', buffer[1, 1].Glyph);
        Assert.AreEqual(ConsoleColor.Yellow, buffer[1, 1].Foreground);
        Assert.AreEqual(ConsoleColor.Black, buffer[3, 1].Foreground);
        Assert.AreEqual(ConsoleColor.Yellow, buffer[3, 1].Background);
        Assert.IsTrue(BufferText(buffer).Contains("proof", StringComparison.Ordinal));
    }

    /// <summary>
    /// Prüft kontrollierte Text- und Pfeiltasteneingabe ohne Parallelübersetzer.
    ///
    /// Verifies controlled text and arrow-key input without a parallel translator.
    /// </summary>
    [TestMethod]
    public void TerminalView_HandleEvent_UpdatesDriverOwnedSession()
    {
        using TerminalSession session = new(8, 2);
        TTerminalView view = new(new TRect(0, 0, 8, 3), session);
        ControlTestContext.AttachToOwner(view);

        view.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        view.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4B));
        view.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'B'));

        Assert.AreEqual('B', session.VisibleBuffer[0, 0].Glyph);
        Assert.AreEqual(new TPoint(1, 0), session.Cursor);
    }

    /// <summary>
    /// Prüft die Synchronisierung von View- und Sessiongröße.
    ///
    /// Verifies synchronization of view and session dimensions.
    /// </summary>
    [TestMethod]
    public void TerminalView_Locate_ResizesSessionToContentRows()
    {
        using TerminalSession session = new(8, 2);
        session.Write("AB");
        TTerminalView view = new(new TRect(0, 0, 8, 3), session);

        view.Locate(new TRect(0, 0, 4, 4));

        Assert.AreEqual(4, session.VisibleBuffer.Width);
        Assert.AreEqual(3, session.VisibleBuffer.Height);
        Assert.AreEqual('A', session.VisibleBuffer[0, 0].Glyph);
    }

    /// <summary>
    /// Prüft Zustand, View-Identität, Profilmetadaten, Status, Cells und Quit im echten App-Loop.
    ///
    /// Verifies state, view identity, profile metadata, status, cells, and quit in the real app loop.
    /// </summary>
    [TestMethod]
    public void TerminalView_AppLoop_ProducesStateViewStatusCellAndQuitProof()
    {
        TerminalProofApplication app = new(capabilityAvailable: true);
        app.Enqueue(
            ControlEventFactory.CreateKeyDown(charCode: 'H'),
            ControlEventFactory.CreateKeyDown(charCode: 'i'));

        app.Run();

        Assert.AreEqual("TTerminalView", app.TerminalView.GetType().Name);
        Assert.AreEqual("koi8-proof", app.Session.ActiveProfileId);
        Assert.AreEqual(TerminalCharset.Koi8R, app.Session.ActiveCharset);
        Assert.AreEqual(TerminalProfile.BuiltInFontId, app.Session.ActiveFontId);
        Assert.AreEqual("Hi", BufferText(app.Session.VisibleBuffer)[..2]);
        Assert.IsTrue(BufferText(app.Driver.BackBuffer).Contains("koi8-proof", StringComparison.Ordinal));
        Assert.IsTrue(app.QuitDelivered);
    }

    /// <summary>
    /// Prüft sichtbaren Unsupported-Status und weiterhin nutzbaren Tastatur-Quit.
    ///
    /// Verifies visible unsupported status and a keyboard quit path that remains usable.
    /// </summary>
    [TestMethod]
    public void TerminalView_AppLoop_UnsupportedCapabilityRemainsTextVisible()
    {
        TerminalProofApplication app = new(capabilityAvailable: false);

        app.Run();

        Assert.AreEqual(TerminalProfileCapabilityState.Unsupported, app.Session.PresentationCapability);
        string frame = BufferText(app.Driver.BackBuffer);
        Assert.IsTrue(frame.Contains("Unsupported", StringComparison.Ordinal));
        Assert.IsTrue(app.QuitDelivered);
    }

    private static string BufferText(TConsoleBuffer buffer)
    {
        System.Text.StringBuilder text = new();
        for (int y = 0; y < buffer.Height; y++)
        {
            for (int x = 0; x < buffer.Width; x++)
            {
                text.Append(buffer[x, y].Glyph);
            }

            text.Append('\n');
        }

        return text.ToString();
    }

    private sealed class TerminalProofApplication : TApplication
    {
        private readonly Queue<TEvent> _events = new();

        public TerminalProofApplication(bool capabilityAvailable) : base(new TRect(0, 0, 50, 12))
        {
            Session = new TerminalSession(36, 4);
            TerminalProfileParseResult parsed = TerminalProfile.Parse(
                """
                { "ProfileId": "koi8-proof", "Charset": "KOI8-R" }
                """,
                availableFontIds: Array.Empty<string>(),
                hostCapabilityAvailable: capabilityAvailable);
            Session.ApplyProfile(parsed.Profile!);
            TerminalView = new TTerminalView(new TRect(1, 1, 38, 7), Session);
            Desktop!.Insert(TerminalView);
            Desktop.SetFocus(TerminalView);
        }

        public TerminalSession Session { get; }

        public TTerminalView TerminalView { get; }

        public bool QuitDelivered { get; private set; }

        public void Enqueue(params TEvent[] events)
        {
            foreach (TEvent @event in events)
            {
                _events.Enqueue(@event);
            }
        }

        public override void GetEvent(out TEvent @event)
        {
            if (_events.Count > 0)
            {
                @event = _events.Dequeue();
                return;
            }

            QuitDelivered = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
        }
    }
}
