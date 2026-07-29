using System.Text.Json;
using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Presentation;
using Spectre.Console.Testing;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class AccessibilityTests
{
    [TestMethod]
    public void LinearStatusContainsTextForEveryResult()
    {
        var result = new ReconciledRunResult(
            1,
            null,
            null,
            "BLOCKED",
            ["Befund / finding"],
            "Bericht prüfen / review report.");

        var output = string.Join("\n", MaintenanceStatusView.Render(result));

        StringAssert.Contains(output, "Status: BLOCKED");
        StringAssert.Contains(output, "Exitcode / exit code: 1");
        StringAssert.Contains(output, "Nächste Aktion / next action:");
    }

    [TestMethod]
    public void LayoutSelectionHasStableNarrowFallback()
    {
        Assert.AreEqual(MaintenanceLayout.Linear, LiveMaintenanceView.SelectLayout(39));
        Assert.AreEqual(MaintenanceLayout.Compact, LiveMaintenanceView.SelectLayout(79));
        Assert.AreEqual(MaintenanceLayout.Enhanced, LiveMaintenanceView.SelectLayout(120));
    }

    [TestMethod]
    public void EnhancedEventRenderingKeepsAStableTextAlternative()
    {
        using var details = JsonDocument.Parse("{}");
        var maintenanceEvent = new MaintenanceEvent(
            1,
            Guid.NewGuid(),
            1,
            DateTimeOffset.Parse("2026-07-29T12:00:00Z"),
            MaintenanceEventTypes.PhaseProgress,
            "RUNNING",
            "fleet",
            null,
            "Flotte wird geprüft.",
            "Fleet is being checked.",
            details.RootElement.Clone());
        var console = new TestConsole().Interactive();

        new LiveMaintenanceView().RenderEvent(console, maintenanceEvent);

        StringAssert.Contains(console.Output, "RUNNING");
        StringAssert.Contains(console.Output, "Flotte wird geprüft.");
        StringAssert.Contains(console.Output, "Fleet is being checked.");
    }

    [TestMethod]
    public void LiveRenderingIsLimitedToTenUpdatesPerSecond()
    {
        var now = DateTimeOffset.Parse("2026-07-29T12:00:00Z");
        var view = new LiveMaintenanceView(() => now);
        var console = new TestConsole();
        using var details = JsonDocument.Parse("{}");
        var first = Event(1, details.RootElement.Clone());
        var second = Event(2, details.RootElement.Clone());

        Assert.IsTrue(view.RenderEvent(console, first));
        now = now.AddMilliseconds(99);
        Assert.IsFalse(view.RenderEvent(console, second));
        now = now.AddMilliseconds(1);
        Assert.IsTrue(view.RenderEvent(console, second));
    }

    [TestMethod]
    public void RunContextIsTextFirstAndContainsEveryStaticBoundary()
    {
        var context = new InvocationContext(
            HostPlatform.MacOs,
            HostArchitecture.Arm64,
            true,
            true,
            new TerminalCapabilities(false, true, 80),
            UiMode.Enhanced,
            "/tmp/home",
            []);
        var output = string.Join(
            "\n",
            MaintenanceStatusView.RenderContext(
                context,
                MaintenanceSelection.Default,
                "/repo/scripts/maintain-agentic-workspace.sh"));

        StringAssert.Contains(output, "Modus / mode: DryRun");
        StringAssert.Contains(output, "Plattform / platform: MacOs-Arm64");
        StringAssert.Contains(output, "Quelle / source:");
        StringAssert.Contains(output, "Home-Verzeichnis / home directory:");
        StringAssert.Contains(output, "Mutation Barrier:");
    }

    [TestMethod]
    public void EventDegradationHasAStableTextWarning()
    {
        var state = new EventReaderState(
            10,
            2,
            EventPresentationMode.Degraded,
            EventDegradationReason.SequenceGap,
            null);

        var output = MaintenanceStatusView.RenderEventDegradation(state);

        StringAssert.Contains(output, "EVENT_STREAM_DEGRADED");
        StringAssert.Contains(output, "SequenceGap");
        StringAssert.Contains(output, "Exitcode");
    }

    [TestMethod]
    public void EnhancedPromptSupportsAKeyboardOnlyDefaultFlow()
    {
        var console = new TestConsole().Interactive();
        console.Input.PushTextWithEnter(string.Empty);
        console.Input.PushTextWithEnter(string.Empty);
        console.Input.PushTextWithEnter(string.Empty);

        var selection = new EnhancedMaintenancePrompt(console).ReadSelection("/tmp/home");

        Assert.IsNotNull(selection);
        Assert.AreEqual(MaintenanceMode.DryRun, selection.Mode);
        Assert.IsFalse(selection.ScriptsOnly);
        Assert.IsFalse(selection.IncludeOptional);
        StringAssert.Contains(console.Output, "Modus wählen / Select mode");
        StringAssert.Contains(console.Output, "Nur Skripte");
        StringAssert.Contains(console.Output, "Optionale Werkzeuge");
    }

    [TestMethod]
    public void LinearSummaryKeepsCanonicalReadingOrderWithoutColor()
    {
        var result = new ReconciledRunResult(
            0,
            null,
            null,
            "PASSED",
            [],
            "N/A");

        var lines = MaintenanceStatusView.Render(result);

        Assert.AreEqual("Status: PASSED", lines[0]);
        Assert.AreEqual("Exitcode / exit code: 0", lines[1]);
        StringAssert.StartsWith(lines[^1], "Nächste Aktion / next action:");
        Assert.IsFalse(lines.Any(line => line.Contains('\u001b')));
    }

    private static MaintenanceEvent Event(long sequence, JsonElement details) =>
        new(
            1,
            Guid.Parse("73fe2ff0-2d27-4279-92dd-7d0cb0b78671"),
            sequence,
            DateTimeOffset.Parse("2026-07-29T12:00:00Z"),
            MaintenanceEventTypes.PhaseProgress,
            "RUNNING",
            "fleet",
            null,
            "Flotte wird geprüft.",
            "Fleet is being checked.",
            details);
}
