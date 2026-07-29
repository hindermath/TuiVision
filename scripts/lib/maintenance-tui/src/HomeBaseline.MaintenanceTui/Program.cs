using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;
using HomeBaseline.MaintenanceTui.Presentation;
using Spectre.Console;

namespace HomeBaseline.MaintenanceTui;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var parsed = ProgramArguments.Parse(args);
        if (!parsed.IsValid)
        {
            System.Console.Error.WriteLine(parsed.Error);
            return 2;
        }

        var context = TerminalContext.Capture(
            parsed.Plain ? UiMode.Plain : UiMode.Enhanced,
            parsed.HomeDirectory!,
            []);
        MaintenanceSelection? selection = parsed.Plain
            ? new PlainMaintenancePrompt(System.Console.In, System.Console.Out)
                .ReadSelection(parsed.HomeDirectory)
            : new EnhancedMaintenancePrompt(AnsiConsole.Console)
                .ReadSelection(parsed.HomeDirectory);
        if (selection is null)
        {
            return MaintenanceSelectionValidator.CancelledExitCode;
        }

        var runId = Guid.NewGuid();
        var eventDirectory = Path.Combine(
            parsed.HomeDirectory!,
            ".home-baseline",
            "events");
        Directory.CreateDirectory(eventDirectory);
        var eventPath = Path.Combine(eventDirectory, $"maintenance-{runId}.jsonl");
        var invocation = new MaintenanceCommandBuilder().Build(
            context,
            selection,
            parsed.Wrapper!,
            eventPath,
            runId);
        foreach (var line in MaintenanceStatusView.RenderContext(
                     context,
                     selection,
                     parsed.Wrapper!))
        {
            System.Console.WriteLine(line);
        }
        if (parsed.Plain)
        {
            System.Console.WriteLine($"Befehl / command: {invocation.DisplayCommand}");
        }
        else
        {
            CommandSummaryView.Render(AnsiConsole.Console, invocation.DisplayCommand);
        }

        var liveView = new LiveMaintenanceView();
        var runner = new MaintenanceProcessRunner(
            maintenanceEvent =>
            {
                if (parsed.Plain)
                {
                    if (liveView.ShouldRender())
                    {
                        System.Console.WriteLine(
                            $"{maintenanceEvent.Sequence} {maintenanceEvent.Status} " +
                            $"{maintenanceEvent.MessageDe} / {maintenanceEvent.MessageEn}");
                    }
                }
                else
                {
                    liveView.RenderEvent(AnsiConsole.Console, maintenanceEvent);
                }
            },
            state => System.Console.Error.WriteLine(
                MaintenanceStatusView.RenderEventDegradation(state)));
        var application = new MaintenanceTuiApplication(runner, new RunResultReconciler());
        using var interrupts = new InterruptController();
        ConsoleCancelEventHandler cancelHandler = (_, eventArgs) =>
        {
            eventArgs.Cancel = true;
            if (interrupts.RequestInterrupt())
            {
                System.Console.Error.WriteLine(
                    "Abbruchsignal wird einmal weitergereicht. / " +
                    "Forwarding one interrupt signal.");
            }
            else
            {
                System.Console.Error.WriteLine(
                    "Weiterer Abbruch startet weder ein zweites Signal noch eine Bereinigung. / " +
                    "Another cancellation starts neither a second signal nor cleanup.");
            }
        };
        System.Console.CancelKeyPress += cancelHandler;
        ReconciledRunResult result;
        try
        {
            result = await application.ExecuteAsync(invocation, interrupts.Token);
        }
        finally
        {
            System.Console.CancelKeyPress -= cancelHandler;
        }
        foreach (var line in MaintenanceStatusView.Render(result))
        {
            System.Console.WriteLine(line);
        }

        return result.ProcessExitCode;
    }

    private sealed record ProgramArguments(
        bool IsValid,
        string? Wrapper,
        string? HomeDirectory,
        bool Plain,
        string Error)
    {
        public static ProgramArguments Parse(IReadOnlyList<string> args)
        {
            string? wrapper = null;
            string? home = null;
            var plain = false;
            for (var index = 0; index < args.Count; index++)
            {
                switch (args[index])
                {
                    case "--wrapper" when index + 1 < args.Count:
                        wrapper = args[++index];
                        break;
                    case "--home-dir" when index + 1 < args.Count:
                        home = args[++index];
                        break;
                    case "--plain":
                        plain = true;
                        break;
                    default:
                        return new(false, null, null, false,
                            $"Unbekanntes Argument / unknown argument: {args[index]}");
                }
            }

            if (string.IsNullOrWhiteSpace(wrapper) || string.IsNullOrWhiteSpace(home))
            {
                return new(false, wrapper, home, plain,
                    "--wrapper und / and --home-dir sind erforderlich / are required.");
            }

            return new(true, Path.GetFullPath(wrapper), Path.GetFullPath(home), plain, string.Empty);
        }
    }
}
