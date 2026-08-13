using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Presentation;

public static class MaintenanceStatusView
{
    public static string RenderEventDegradation(EventReaderState state) =>
        "WARN: EVENT_STREAM_DEGRADED; " +
        $"Grund / reason: {state.DegradationReason}; " +
        "Engine und Exitcode bleiben unverändert / engine and exit code remain unchanged.";

    public static IReadOnlyList<string> RenderContext(
        InvocationContext context,
        MaintenanceSelection selection,
        string source)
    {
        return
        [
            $"Modus / mode: {selection.Mode}",
            $"Storage-Profil / cleanup profile: {selection.CleanupProfile}",
            $"Plattform / platform: {context.Platform}-{context.Architecture}",
            $"Quelle / source: {source}",
            $"Home-Verzeichnis / home directory: {context.HomeDirectory}",
            Messages.MutationBarrierExplanation,
            "Mutation Barrier: noch nicht bewertet / not evaluated yet",
        ];
    }

    public static IReadOnlyList<string> Render(ReconciledRunResult result)
    {
        var lines = new List<string>
        {
            $"Status: {result.DisplayStatus}",
            $"Exitcode / exit code: {result.ProcessExitCode}",
        };
        if (result.Report is not null)
        {
            lines.Add($"Bericht / report: {result.Report.Path}");
            lines.Add($"Log / log: {result.Report.LogPath}");
            if (result.Report.Summary is not null)
            {
                var summary = result.Report.Summary;
                lines.Add($"Mutation Barrier: {summary.MutationBarrier}");
                lines.Add(
                    "Ziele / targets: " +
                    $"{summary.TargetCount}; pass={summary.PassedCount}; " +
                    $"warning={summary.WarningCount}; blocked={summary.BlockedCount}; " +
                    $"failed={summary.FailedCount}");
                var statusClasses = summary.TargetStatusClasses.Count == 0
                    ? "N/A"
                    : string.Join(
                        ", ",
                        summary.TargetStatusClasses
                            .OrderBy(item => item.Key, StringComparer.Ordinal)
                            .Select(item => $"{item.Key}={item.Value}"));
                lines.Add($"Pull-/Sperrklassen / pull and block classes: {statusClasses}");
                lines.Add($"Lease-Status / lease status: {summary.LeaseStatus}");
                lines.Add($"Preset-Profil / preset profile: {summary.PresetProfileStatus}");
                lines.Add($"Letzte sichere Aktion / last safe action: {summary.LastSafeAction}");
            }
        }

        foreach (var mismatch in result.MismatchReasons)
        {
            lines.Add($"WARN: {mismatch}");
        }

        lines.Add($"Nächste Aktion / next action: {result.NextAction}");
        return lines;
    }
}
