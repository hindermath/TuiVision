namespace HomeBaseline.MaintenanceTui.Contracts;

public sealed record AtomicRunReport(
    string Path,
    Guid RunId,
    bool Finalized,
    string OverallStatus,
    int ExitCode,
    string LogPath,
    RunDisplaySummary? Summary = null);

public sealed record RunDisplaySummary(
    string MutationBarrier,
    int TargetCount,
    int PassedCount,
    int WarningCount,
    int BlockedCount,
    int FailedCount,
    IReadOnlyDictionary<string, int> TargetStatusClasses,
    string LeaseStatus,
    string PresetProfileStatus,
    string LastSafeAction);

public sealed record ReconciledRunResult(
    int ProcessExitCode,
    AtomicRunReport? Report,
    MaintenanceEvent? CompletionEvent,
    string DisplayStatus,
    IReadOnlyList<string> MismatchReasons,
    string NextAction);
