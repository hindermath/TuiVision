namespace HomeBaseline.MaintenanceTui.Contracts;

public enum MaintenanceMode
{
    CheckOnly,
    DryRun,
    Update,
}

public enum StorageCleanupProfile
{
    Safe,
    Deep,
    None,
}

public sealed record MaintenanceSelection(
    MaintenanceMode Mode,
    bool ScriptsOnly = false,
    bool IncludeOptional = false,
    bool RepairDrift = false,
    string? HomeDirectory = null,
    bool Confirmed = false,
    StorageCleanupProfile CleanupProfile = StorageCleanupProfile.Safe,
    bool ConfirmDeepCleanup = false)
{
    public static MaintenanceSelection Default { get; } =
        new(MaintenanceMode.DryRun);
}
