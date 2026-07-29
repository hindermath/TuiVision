using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Infrastructure;

public sealed record ProcessInvocation(
    string Executable,
    IReadOnlyList<string> Arguments,
    string DisplayCommand,
    string EventStreamPath,
    Guid RunId);

public sealed class MaintenanceCommandBuilder
{
    public ProcessInvocation Build(
        InvocationContext context,
        MaintenanceSelection selection,
        string scriptPath,
        string eventStreamPath,
        Guid runId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scriptPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(eventStreamPath);

        var executable = context.Platform == HostPlatform.Windows ? "pwsh" : "bash";
        var arguments = new List<string>();
        if (context.Platform == HostPlatform.Windows)
        {
            arguments.AddRange(["-NoProfile", "-File", scriptPath, "-NoTui"]);
            AddPowerShellSelection(arguments, selection);
            arguments.AddRange(["-EventStream", eventStreamPath, "-RunId", runId.ToString()]);
        }
        else
        {
            arguments.AddRange([scriptPath, "--no-tui"]);
            AddBashSelection(arguments, selection);
            arguments.AddRange(["--event-stream", eventStreamPath, "--run-id", runId.ToString()]);
        }

        if (!string.IsNullOrWhiteSpace(selection.HomeDirectory))
        {
            arguments.Add(context.Platform == HostPlatform.Windows ? "-HomeDir" : "--home-dir");
            arguments.Add(selection.HomeDirectory);
        }

        return new ProcessInvocation(
            executable,
            arguments,
            BuildDisplayCommand(executable, arguments, context.Platform),
            eventStreamPath,
            runId);
    }

    private static void AddBashSelection(List<string> arguments, MaintenanceSelection selection)
    {
        if (selection.Mode == MaintenanceMode.CheckOnly)
        {
            arguments.Add("--check-only");
        }
        else if (selection.Mode == MaintenanceMode.DryRun)
        {
            arguments.Add("--dry-run");
        }

        if (selection.ScriptsOnly) arguments.Add("--scripts-only");
        if (selection.IncludeOptional) arguments.Add("--include-optional");
        if (selection.RepairDrift) arguments.Add("--repair-drift");
    }

    private static void AddPowerShellSelection(List<string> arguments, MaintenanceSelection selection)
    {
        if (selection.Mode == MaintenanceMode.CheckOnly)
        {
            arguments.Add("-CheckOnly");
        }
        else if (selection.Mode == MaintenanceMode.DryRun)
        {
            arguments.Add("-WhatIf");
        }

        if (selection.ScriptsOnly) arguments.Add("-ScriptsOnly");
        if (selection.IncludeOptional) arguments.Add("-IncludeOptional");
        if (selection.RepairDrift) arguments.Add("-RepairDrift");
    }

    private static string BuildDisplayCommand(
        string executable,
        IEnumerable<string> arguments,
        HostPlatform platform)
    {
        var tokens = new[] { executable }.Concat(arguments);
        return string.Join(
            " ",
            tokens.Select(token => platform == HostPlatform.Windows
                ? $"'{token.Replace("'", "''", StringComparison.Ordinal)}'"
                : $"'{token.Replace("'", "'\"'\"'", StringComparison.Ordinal)}'"));
    }
}
