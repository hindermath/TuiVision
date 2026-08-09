using System.Diagnostics;
using System.Text.Json;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Infrastructure;

public sealed record ProcessExecutionResult(
    int ExitCode,
    string StandardOutput,
    string StandardError,
    AtomicRunReport? Report = null,
    MaintenanceEvent? CompletionEvent = null);

public interface IMaintenanceProcessRunner
{
    Task<ProcessExecutionResult> RunAsync(
        ProcessInvocation invocation,
        CancellationToken cancellationToken);
}

public sealed class MaintenanceProcessRunner : IMaintenanceProcessRunner
{
    private readonly Action<MaintenanceEvent>? _eventSink;
    private readonly Action<EventReaderState>? _degradationSink;

    public MaintenanceProcessRunner(
        Action<MaintenanceEvent>? eventSink = null,
        Action<EventReaderState>? degradationSink = null)
    {
        _eventSink = eventSink;
        _degradationSink = degradationSink;
    }

    public async Task<ProcessExecutionResult> RunAsync(
        ProcessInvocation invocation,
        CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ProcessExecutionResult(130, string.Empty, string.Empty);
        }

        var startInfo = new ProcessStartInfo(invocation.Executable)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        foreach (var argument in invocation.Arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        using var process = new Process { StartInfo = startInfo };
        if (!process.Start())
        {
            throw new InvalidOperationException("Maintenance process did not start.");
        }

        var stdout = process.StandardOutput.ReadToEndAsync();
        var stderr = process.StandardError.ReadToEndAsync();
        var eventTask = ReadEventsUntilExitAsync(process, invocation, CancellationToken.None);
        try
        {
            await process.WaitForExitAsync(cancellationToken);
        }
        catch (OperationCanceledException)
        {
            if (!process.HasExited)
            {
                // Der Wrapper erhaelt Ctrl+C bereits einmal ueber die gemeinsame Konsolengruppe.
                // The wrapper already receives Ctrl+C once through the shared console group.
                await process.WaitForExitAsync(CancellationToken.None);
            }

            await eventTask;
            var cancelledEvent = eventTask.Result.LastOrDefault(
                item => item.EventType == MaintenanceEventTypes.RunCompleted);
            var cancelledReport = ReadReport(
                cancelledEvent,
                invocation.ReportPath,
                invocation.RunId);
            return new ProcessExecutionResult(
                130,
                await stdout,
                await stderr,
                cancelledReport,
                cancelledEvent);
        }

        var events = await eventTask;
        var completionEvent = events.LastOrDefault(
            item => item.EventType == MaintenanceEventTypes.RunCompleted);
        var report = ReadReport(
            completionEvent,
            invocation.ReportPath,
            invocation.RunId);
        return new ProcessExecutionResult(
            process.ExitCode,
            await stdout,
            await stderr,
            report,
            completionEvent);
    }

    private async Task<IReadOnlyList<MaintenanceEvent>> ReadEventsUntilExitAsync(
        Process process,
        ProcessInvocation invocation,
        CancellationToken cancellationToken)
    {
        var events = new List<MaintenanceEvent>();
        var reader = new MaintenanceEventReader(invocation.RunId);
        long offset = 0;
        while (!process.HasExited)
        {
            ReadNewContent(invocation.EventStreamPath, reader, events, ref offset);
            await Task.Delay(LiveDelay, cancellationToken);
        }

        ReadNewContent(invocation.EventStreamPath, reader, events, ref offset);
        return events;
    }

    private static readonly TimeSpan LiveDelay = TimeSpan.FromMilliseconds(100);

    private void ReadNewContent(
        string path,
        MaintenanceEventReader reader,
        List<MaintenanceEvent> events,
        ref long offset)
    {
        if (!File.Exists(path))
        {
            return;
        }

        using var stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.ReadWrite | FileShare.Delete);
        if (stream.Length <= offset)
        {
            return;
        }

        stream.Position = offset;
        using var textReader = new StreamReader(stream);
        var content = textReader.ReadToEnd();
        offset = stream.Position;
        var previousMode = reader.State.PresentationMode;
        foreach (var maintenanceEvent in reader.Feed(content))
        {
            events.Add(maintenanceEvent);
            _eventSink?.Invoke(maintenanceEvent);
        }
        if (previousMode != EventPresentationMode.Degraded &&
            reader.State.PresentationMode == EventPresentationMode.Degraded)
        {
            _degradationSink?.Invoke(reader.State);
        }
    }

    private static AtomicRunReport? ReadReport(
        MaintenanceEvent? completionEvent,
        string expectedReportPath,
        Guid expectedRunId)
    {
        var reportPath = expectedReportPath;
        if (completionEvent is not null &&
            completionEvent.Details.TryGetProperty("reportPath", out var reportProperty) &&
            reportProperty.ValueKind == JsonValueKind.String)
        {
            var eventReportPath = reportProperty.GetString();
            if (!string.IsNullOrWhiteSpace(eventReportPath) &&
                PathsEqual(eventReportPath, expectedReportPath))
            {
                reportPath = eventReportPath;
            }
        }

        // Der vor Prozessstart gebundene Pfad verhindert eine racy Suche nach fremden Laufberichten.
        // The pre-bound path avoids a racy search that could select another run's report.
        if (string.IsNullOrWhiteSpace(reportPath) || !File.Exists(reportPath))
        {
            return null;
        }

        try
        {
            using var document = JsonDocument.Parse(File.ReadAllText(reportPath));
            var root = document.RootElement;
            if (!root.TryGetProperty("runId", out var runProperty) ||
                !Guid.TryParse(runProperty.GetString(), out var runId) ||
                runId != expectedRunId ||
                !root.TryGetProperty("overallStatus", out var statusProperty) ||
                !root.TryGetProperty("exitCode", out var exitProperty))
            {
                return null;
            }

            var finalized = root.TryGetProperty("finalized", out var finalizedProperty) &&
                            finalizedProperty.ValueKind == JsonValueKind.True;
            var logPath = root.TryGetProperty("artifacts", out var artifacts) &&
                          artifacts.TryGetProperty("logPath", out var logProperty)
                ? logProperty.GetString() ?? "N/A"
                : "N/A";
            return new AtomicRunReport(
                reportPath,
                runId,
                finalized,
                statusProperty.GetString() ?? "UNKNOWN",
                exitProperty.GetInt32(),
                logPath,
                ReadDisplaySummary(root));
        }
        catch (Exception exception) when (
            exception is JsonException or
            IOException or
            UnauthorizedAccessException or
            InvalidOperationException)
        {
            return null;
        }
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

    private static RunDisplaySummary ReadDisplaySummary(JsonElement root)
    {
        var barrier = "UNKNOWN";
        if (root.TryGetProperty("mutationBarrier", out var mutationBarrier) &&
            mutationBarrier.ValueKind == JsonValueKind.Object &&
            mutationBarrier.TryGetProperty("fleetReady", out var fleetReady) &&
            fleetReady.ValueKind is JsonValueKind.True or JsonValueKind.False)
        {
            barrier = fleetReady.GetBoolean() ? "OPEN" : "BLOCKED";
        }

        var counts = root.TryGetProperty("counts", out var countObject) &&
                     countObject.ValueKind == JsonValueKind.Object
            ? countObject
            : default;
        var statusClasses = new Dictionary<string, int>(StringComparer.Ordinal);
        if (root.TryGetProperty("targets", out var targets) &&
            targets.ValueKind == JsonValueKind.Array)
        {
            foreach (var target in targets.EnumerateArray())
            {
                if (!target.TryGetProperty("status", out var status) ||
                    status.ValueKind != JsonValueKind.String)
                {
                    continue;
                }

                var name = status.GetString() ?? "UNKNOWN";
                statusClasses[name] = statusClasses.GetValueOrDefault(name) + 1;
            }
        }

        return new RunDisplaySummary(
            barrier,
            Count(counts, "targets"),
            Count(counts, "passed"),
            Count(counts, "warnings"),
            Count(counts, "blocked"),
            Count(counts, "failed"),
            statusClasses,
            "Nicht separat berichtet / not separately reported",
            StageStatus(root, "preset-profiles"),
            LastNextAction(root));
    }

    private static int Count(JsonElement counts, string name) =>
        counts.ValueKind == JsonValueKind.Object &&
        counts.TryGetProperty(name, out var value) &&
        value.ValueKind == JsonValueKind.Number &&
        value.TryGetInt32(out var result)
            ? result
            : 0;

    private static string StageStatus(JsonElement root, string stageId)
    {
        if (!root.TryGetProperty("stages", out var stages) ||
            stages.ValueKind != JsonValueKind.Array)
        {
            return "UNKNOWN";
        }

        foreach (var stage in stages.EnumerateArray())
        {
            if (stage.TryGetProperty("stageId", out var id) &&
                string.Equals(id.GetString(), stageId, StringComparison.Ordinal) &&
                stage.TryGetProperty("status", out var status) &&
                status.ValueKind == JsonValueKind.String)
            {
                return status.GetString() ?? "UNKNOWN";
            }
        }

        return "UNKNOWN";
    }

    private static string LastNextAction(JsonElement root)
    {
        if (root.TryGetProperty("mutationBarrier", out var barrier) &&
            barrier.ValueKind == JsonValueKind.Object &&
            barrier.TryGetProperty("nextAction", out var barrierAction) &&
            barrierAction.ValueKind == JsonValueKind.String &&
            !string.Equals(barrierAction.GetString(), "N/A", StringComparison.Ordinal))
        {
            return barrierAction.GetString() ?? "N/A";
        }

        if (!root.TryGetProperty("stages", out var stages) ||
            stages.ValueKind != JsonValueKind.Array)
        {
            return "N/A";
        }

        foreach (var stage in stages.EnumerateArray().Reverse())
        {
            if (stage.TryGetProperty("nextAction", out var action) &&
                action.ValueKind == JsonValueKind.String &&
                !string.IsNullOrWhiteSpace(action.GetString()))
            {
                return action.GetString() ?? "N/A";
            }
        }

        return "N/A";
    }

}
