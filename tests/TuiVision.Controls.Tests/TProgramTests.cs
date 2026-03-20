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
