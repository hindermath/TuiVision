using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Infrastructure;

public sealed class RunResultReconciler
{
    public ReconciledRunResult Reconcile(
        Guid expectedRunId,
        int processExitCode,
        AtomicRunReport? report,
        MaintenanceEvent? completionEvent)
    {
        var mismatches = new List<string>();
        if (report is null)
        {
            mismatches.Add("Finalisierter Bericht fehlt / finalized report is missing.");
        }
        else
        {
            if (report.RunId != expectedRunId)
                mismatches.Add("Run-ID des Berichts weicht ab / report run ID differs.");
            if (!report.Finalized)
                mismatches.Add("Bericht ist nicht finalisiert / report is not finalized.");
            if (report.ExitCode != processExitCode)
                mismatches.Add("Bericht und Prozess-Exitcode weichen ab / report and process exit code differ.");
        }

        if (completionEvent is not null)
        {
            if (completionEvent.RunId != expectedRunId)
                mismatches.Add("Run-ID des Abschlussereignisses weicht ab / completion event run ID differs.");
            if (!string.Equals(completionEvent.EventType, MaintenanceEventTypes.RunCompleted, StringComparison.Ordinal))
                mismatches.Add("Abschlussereignis hat den falschen Typ / completion event has the wrong type.");
            if (report is not null)
            {
                if (!TryReadText(completionEvent, "reportPath", out var eventReportPath) ||
                    !PathsEqual(eventReportPath, report.Path))
                {
                    mismatches.Add("Berichtspfad im Abschlussereignis weicht ab / completion event report path differs.");
                }
                if (!TryReadInt(completionEvent, "exitCode", out var eventExitCode) ||
                    eventExitCode != processExitCode)
                {
                    mismatches.Add("Exitcode im Abschlussereignis weicht ab / completion event exit code differs.");
                }
                if (!TryReadText(completionEvent, "overallStatus", out var eventStatus) ||
                    !string.Equals(eventStatus, report.OverallStatus, StringComparison.Ordinal))
                {
                    mismatches.Add("Status im Abschlussereignis weicht ab / completion event status differs.");
                }
            }
        }

        var displayStatus = mismatches.Count == 0
            ? report!.OverallStatus
            : "RESULT_MISMATCH";
        var nextAction = mismatches.Count == 0
            ? "N/A"
            : "Bericht, Ereignisse und Exitcode prüfen / review report, events, and exit code.";

        return new ReconciledRunResult(
            processExitCode,
            report,
            completionEvent,
            displayStatus,
            mismatches,
            nextAction);
    }

    private static bool TryReadText(
        MaintenanceEvent maintenanceEvent,
        string name,
        out string value)
    {
        value = string.Empty;
        return maintenanceEvent.Details.TryGetProperty(name, out var property) &&
               property.ValueKind == System.Text.Json.JsonValueKind.String &&
               !string.IsNullOrWhiteSpace(value = property.GetString() ?? string.Empty);
    }

    private static bool TryReadInt(
        MaintenanceEvent maintenanceEvent,
        string name,
        out int value)
    {
        value = default;
        return maintenanceEvent.Details.TryGetProperty(name, out var property) &&
               property.ValueKind == System.Text.Json.JsonValueKind.Number &&
               property.TryGetInt32(out value);
    }

    private static bool PathsEqual(string left, string right)
    {
        try
        {
            var comparison = OperatingSystem.IsWindows()
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;
            return string.Equals(
                Path.GetFullPath(left),
                Path.GetFullPath(right),
                comparison);
        }
        catch (Exception exception) when (
            exception is ArgumentException or
            IOException or
            NotSupportedException)
        {
            return false;
        }
    }
}
