using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Prüft den begrenzten SGR-1006-Mauseingang einschließlich atomarer Ablehnung,
/// Zustandsfolgen und deterministischer Doppelklickgrenzen.
///
/// Verifies the bounded SGR 1006 mouse ingress, including atomic rejection,
/// state transitions, and deterministic double-click boundaries.
/// </summary>
[TestClass]
public sealed class ConsoleMouseIngressTests
{
    private const int Width = 80;
    private const int Height = 25;

    /// <summary>
    /// Prüft die unterstützten SGR-Hostfamilien und die ehrlichen Unsupported-Grenzen.
    ///
    /// Verifies supported SGR host families and honest unsupported boundaries.
    /// </summary>
    [TestMethod]
    public void CapabilityDetector_ClassifiesSupportedAndUnsupportedHosts()
    {
        ConsoleMouseCapability mac = ConsoleMouseCapabilityDetector.Detect(false, false, "xterm-256color", false, false, true, false);
        ConsoleMouseCapability linux = ConsoleMouseCapabilityDetector.Detect(false, false, "xterm", false, false, false, true);
        ConsoleMouseCapability wsl = ConsoleMouseCapabilityDetector.Detect(false, false, "xterm-256color", true, true, false, true);
        ConsoleMouseCapability windows = ConsoleMouseCapabilityDetector.Detect(false, false, null, true, false, false, false);
        ConsoleMouseCapability headless = ConsoleMouseCapabilityDetector.Detect(true, false, "xterm", false, false, true, false);
        ConsoleMouseCapability dumb = ConsoleMouseCapabilityDetector.Detect(false, false, "dumb", false, false, false, true);

        Assert.AreEqual((ConsoleMouseCapabilityState.Disabled, ConsoleMouseHostFamily.MacOS, ConsoleMouseProtocol.Sgr1006), (mac.State, mac.HostFamily, mac.Protocol));
        Assert.AreEqual((ConsoleMouseCapabilityState.Disabled, ConsoleMouseHostFamily.Linux, ConsoleMouseProtocol.Sgr1006), (linux.State, linux.HostFamily, linux.Protocol));
        Assert.AreEqual((ConsoleMouseCapabilityState.Disabled, ConsoleMouseHostFamily.Wsl, ConsoleMouseProtocol.Sgr1006), (wsl.State, wsl.HostFamily, wsl.Protocol));
        Assert.AreEqual((ConsoleMouseCapabilityState.Unsupported, ConsoleMouseHostFamily.WindowsConsole, ConsoleMouseProtocol.None), (windows.State, windows.HostFamily, windows.Protocol));
        Assert.AreEqual((ConsoleMouseCapabilityState.Unsupported, ConsoleMouseHostFamily.Headless, ConsoleMouseProtocol.None), (headless.State, headless.HostFamily, headless.Protocol));
        Assert.AreEqual((ConsoleMouseCapabilityState.Unsupported, ConsoleMouseHostFamily.Linux, ConsoleMouseProtocol.None), (dumb.State, dumb.HostFamily, dumb.Protocol));
    }

    /// <summary>
    /// Prüft eine vollständige Press-Move-Release-Folge und die Umrechnung auf
    /// nullbasierte Framework-Koordinaten.
    ///
    /// Verifies a complete press-move-release sequence and conversion to
    /// zero-based framework coordinates.
    /// </summary>
    [TestMethod]
    public void TryAccept_ValidLeftSequence_PublishesCanonicalEventsExactlyOnce()
    {
        ConsoleMouseIngress ingress = CreateEnabledIngress();

        AssertAccepted(ingress, "\x1b[<0;1;1M", 100, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(0, 0));
        AssertAccepted(ingress, "\x1b[<32;80;25M", 110, TEventKind.MouseMove, TMouseButtons.Left, new TPoint(79, 24));
        AssertAccepted(ingress, "\x1b[<0;80;25m", 120, TEventKind.MouseUp, TMouseButtons.None, new TPoint(79, 24));
    }

    /// <summary>
    /// Prüft, dass Koordinaten außerhalb des aktuellen Puffers vollständig
    /// abgelehnt und nicht still geklemmt werden.
    ///
    /// Verifies that coordinates outside the current buffer are rejected
    /// completely instead of being silently clamped.
    /// </summary>
    [TestMethod]
    [DataRow("\x1b[<0;0;1M")]
    [DataRow("\x1b[<0;1;0M")]
    [DataRow("\x1b[<0;81;1M")]
    [DataRow("\x1b[<0;1;26M")]
    public void TryAccept_OutOfRangeCoordinates_RejectsAtomically(string sequence)
    {
        AssertRejected(CreateEnabledIngress(), sequence, ConsoleMouseRejectionReason.CoordinatesOutOfRange);
    }

    /// <summary>
    /// Prüft die projektlokale Negativmatrix für Syntax-, Größen-, Zahlen- und
    /// Button-Grenzen.
    ///
    /// Verifies the project-local negative matrix for syntax, size, numeric,
    /// and button boundaries.
    /// </summary>
    [TestMethod]
    [DataRow("", ConsoleMouseRejectionReason.InvalidSyntax)]
    [DataRow("\x1b[<0;1;1", ConsoleMouseRejectionReason.InvalidSyntax)]
    [DataRow("\x1b[<x;1;1M", ConsoleMouseRejectionReason.InvalidNumber)]
    [DataRow("\x1b[<0;1;1Mtail", ConsoleMouseRejectionReason.InvalidSyntax)]
    [DataRow("\x1b[<1;1;1M", ConsoleMouseRejectionReason.UnsupportedButton)]
    [DataRow("\x1b[<64;1;1M", ConsoleMouseRejectionReason.UnsupportedButton)]
    public void TryAccept_InvalidObservation_RejectsWithoutPartialEvent(
        string sequence,
        ConsoleMouseRejectionReason expectedReason)
    {
        AssertRejected(CreateEnabledIngress(), sequence, expectedReason);
    }

    /// <summary>
    /// Prüft die feste Sequenzlängengrenze unabhängig von Zahlenüberläufen.
    ///
    /// Verifies the fixed sequence length boundary independently of numeric overflow.
    /// </summary>
    [TestMethod]
    public void TryAccept_OversizedObservation_RejectsBeforeParsing()
    {
        AssertRejected(CreateEnabledIngress(), "\x1b[<0;" + new string('9', 80) + ";1M", ConsoleMouseRejectionReason.SequenceTooLong);
    }

    /// <summary>
    /// Prüft ungültige Phasenfolgen, Capability-Grenzen und den Reset transienter Zustände.
    ///
    /// Verifies invalid phase sequences, capability boundaries, and transient-state reset.
    /// </summary>
    [TestMethod]
    public void TryAccept_InvalidPhaseOrCapability_RejectsFailSafe()
    {
        ConsoleMouseIngress ingress = CreateEnabledIngress();
        AssertRejected(ingress, "\x1b[<32;2;2M", ConsoleMouseRejectionReason.InvalidTransition);
        AssertRejected(ingress, "\x1b[<0;2;2m", ConsoleMouseRejectionReason.InvalidTransition);
        AssertAccepted(ingress, "\x1b[<0;2;2M", 100, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(1, 1));
        AssertRejected(ingress, "\x1b[<0;3;3M", ConsoleMouseRejectionReason.InvalidTransition);

        ingress.SetCapability(new ConsoleMouseCapability(
            ConsoleMouseCapabilityState.Disabled,
            ConsoleMouseHostFamily.MacOS,
            ConsoleMouseProtocol.Sgr1006,
            "Disabled for test"));
        AssertRejected(ingress, "\x1b[<32;3;3M", ConsoleMouseRejectionReason.CapabilityUnavailable);

        ingress.SetCapability(new ConsoleMouseCapability(
            ConsoleMouseCapabilityState.Unsupported,
            ConsoleMouseHostFamily.Headless,
            ConsoleMouseProtocol.None,
            "Headless"));
        AssertRejected(ingress, "\x1b[<0;1;1M", ConsoleMouseRejectionReason.CapabilityUnavailable);
    }

    /// <summary>
    /// Prüft, dass eine fehlerhafte Beobachtung die nächste eigenständige gültige
    /// Beobachtung nicht beschädigt.
    ///
    /// Verifies that a rejected observation does not damage the next independent
    /// valid observation.
    /// </summary>
    [TestMethod]
    public void TryAccept_RejectedObservation_PreservesNextIndependentObservation()
    {
        ConsoleMouseIngress ingress = CreateEnabledIngress();
        AssertRejected(ingress, "\x1b[<broken", ConsoleMouseRejectionReason.InvalidSyntax);
        AssertAccepted(ingress, "\x1b[<0;4;5M", 100, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(3, 4));
    }

    /// <summary>
    /// Prüft die inklusive 500-ms-Grenze bei identischem Button, Ziel und Zelle.
    ///
    /// Verifies the inclusive 500 ms boundary for the same button, target, and cell.
    /// </summary>
    [TestMethod]
    public void TryAccept_SecondMatchingPressWithin500Milliseconds_IsDoubleClick()
    {
        ConsoleMouseIngress ingress = CreateEnabledIngress();
        Func<TPoint, string?> target = _ => "button-a";

        AssertAccepted(ingress, "\x1b[<0;4;5M", 100, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(3, 4), false, target);
        AssertAccepted(ingress, "\x1b[<0;4;5m", 110, TEventKind.MouseUp, TMouseButtons.None, new TPoint(3, 4), false, target);
        AssertAccepted(ingress, "\x1b[<0;4;5M", 600, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(3, 4), true, target);
    }

    /// <summary>
    /// Prüft Zeit-, Positions-, Ziel-, Uhrsprung- und Capability-Reset-Grenzen
    /// unabhängig voneinander.
    ///
    /// Verifies time, position, target, clock-regression, and capability-reset
    /// boundaries independently.
    /// </summary>
    [TestMethod]
    public void TryAccept_NonMatchingPresses_RemainSingleClicks()
    {
        AssertSecondPressIsSingle(100, 601, new TPoint(3, 4), new TPoint(3, 4), "a", "a");
        AssertSecondPressIsSingle(100, 600, new TPoint(3, 4), new TPoint(4, 4), "a", "a");
        AssertSecondPressIsSingle(100, 600, new TPoint(3, 4), new TPoint(3, 4), "a", "b");
        AssertSecondPressIsSingle(600, 100, new TPoint(3, 4), new TPoint(3, 4), "a", "a");

        ConsoleMouseIngress resetIngress = CreateEnabledIngress();
        Func<TPoint, string?> target = _ => "a";
        AssertAccepted(resetIngress, "\x1b[<0;4;5M", 100, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(3, 4), false, target);
        AssertAccepted(resetIngress, "\x1b[<0;4;5m", 110, TEventKind.MouseUp, TMouseButtons.None, new TPoint(3, 4), false, target);
        resetIngress.SetCapability(new ConsoleMouseCapability(ConsoleMouseCapabilityState.Disabled, ConsoleMouseHostFamily.MacOS, ConsoleMouseProtocol.Sgr1006, "Reset"));
        resetIngress.SetCapability(EnabledCapability());
        AssertAccepted(resetIngress, "\x1b[<0;4;5M", 200, TEventKind.MouseDown, TMouseButtons.Left, new TPoint(3, 4), false, target);
    }

    private static void AssertSecondPressIsSingle(
        long firstTime,
        long secondTime,
        TPoint firstPoint,
        TPoint secondPoint,
        string firstTarget,
        string secondTarget)
    {
        ConsoleMouseIngress ingress = CreateEnabledIngress();
        AssertAccepted(ingress, Sequence(firstPoint, false), firstTime, TEventKind.MouseDown, TMouseButtons.Left, firstPoint, false, _ => firstTarget);
        AssertAccepted(ingress, Sequence(firstPoint, true), firstTime + 1, TEventKind.MouseUp, TMouseButtons.None, firstPoint, false, _ => firstTarget);
        AssertAccepted(ingress, Sequence(secondPoint, false), secondTime, TEventKind.MouseDown, TMouseButtons.Left, secondPoint, false, _ => secondTarget);
    }

    private static string Sequence(TPoint point, bool release) =>
        $"\x1b[<0;{point.X + 1};{point.Y + 1}{(release ? 'm' : 'M')}";

    private static ConsoleMouseIngress CreateEnabledIngress() => new(EnabledCapability());

    private static ConsoleMouseCapability EnabledCapability() => new(
        ConsoleMouseCapabilityState.Enabled,
        ConsoleMouseHostFamily.MacOS,
        ConsoleMouseProtocol.Sgr1006,
        "Interactive SGR test host");

    private static void AssertAccepted(
        ConsoleMouseIngress ingress,
        string sequence,
        long timestamp,
        TEventKind expectedKind,
        TMouseButtons expectedButtons,
        TPoint expectedPoint,
        bool expectedDoubleClick = false,
        Func<TPoint, string?>? targetResolver = null)
    {
        bool accepted = ingress.TryAccept(
            sequence,
            timestamp,
            Width,
            Height,
            targetResolver,
            out TEvent @event,
            out ConsoleMouseRejectionReason rejection);

        Assert.IsTrue(accepted, $"Expected accepted event, rejection was {rejection}.");
        Assert.AreEqual(ConsoleMouseRejectionReason.None, rejection);
        Assert.AreEqual(expectedKind, @event.What);
        Assert.AreEqual(expectedButtons, @event.Mouse.Buttons);
        Assert.AreEqual(expectedPoint, @event.Mouse.Where);
        Assert.AreEqual(expectedDoubleClick, @event.Mouse.DoubleClick);
    }

    private static void AssertRejected(
        ConsoleMouseIngress ingress,
        string sequence,
        ConsoleMouseRejectionReason expectedReason)
    {
        bool accepted = ingress.TryAccept(sequence, 100, Width, Height, null, out TEvent @event, out ConsoleMouseRejectionReason rejection);
        Assert.IsFalse(accepted);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
        Assert.AreEqual(expectedReason, rejection);
    }
}
