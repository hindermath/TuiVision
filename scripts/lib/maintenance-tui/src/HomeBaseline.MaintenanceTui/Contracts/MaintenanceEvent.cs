using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeBaseline.MaintenanceTui.Contracts;

public static class MaintenanceEventTypes
{
    public const string RunStarted = "run-started";
    public const string PhaseStarted = "phase-started";
    public const string PhaseProgress = "phase-progress";
    public const string Finding = "finding";
    public const string PhaseCompleted = "phase-completed";
    public const string RunCompleted = "run-completed";

    public static readonly IReadOnlySet<string> All = new HashSet<string>(
    [
        RunStarted,
        PhaseStarted,
        PhaseProgress,
        Finding,
        PhaseCompleted,
        RunCompleted,
    ], StringComparer.Ordinal);
}

public static class MaintenanceStatuses
{
    public static readonly IReadOnlySet<string> All = new HashSet<string>(
    [
        "RUNNING",
        "PASSED",
        "PARTIAL",
        "BLOCKED",
        "WARNING",
        "SKIPPED",
        "FAILED",
    ], StringComparer.Ordinal);
}

public static class MaintenancePhases
{
    public static readonly IReadOnlySet<string> All = new HashSet<string>(
    [
        "fleet",
        "level0",
        "home-sync",
        "registry",
        "propagation",
        "preset-profiles",
        "toolchain",
        "final",
    ], StringComparer.Ordinal);
}

public sealed record MaintenanceEvent(
    [property: JsonPropertyName("schemaVersion")] int SchemaVersion,
    [property: JsonPropertyName("runId")] Guid RunId,
    [property: JsonPropertyName("sequence")] long Sequence,
    [property: JsonPropertyName("timestampUtc")] DateTimeOffset TimestampUtc,
    [property: JsonPropertyName("eventType")] string EventType,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("phaseId")] string? PhaseId,
    [property: JsonPropertyName("targetId")] string? TargetId,
    [property: JsonPropertyName("messageDe")] string MessageDe,
    [property: JsonPropertyName("messageEn")] string MessageEn,
    [property: JsonPropertyName("details")] JsonElement Details);

public enum EventDegradationReason
{
    None,
    InvalidJson,
    InvalidSchema,
    SequenceGap,
    RunMismatch,
}

public enum EventPresentationMode
{
    Enhanced,
    Compact,
    Linear,
    Degraded,
}

public sealed record EventReaderState(
    long ByteOffset,
    long ExpectedSequence,
    EventPresentationMode PresentationMode,
    EventDegradationReason DegradationReason,
    MaintenanceEvent? LastEvent);
