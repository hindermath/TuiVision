using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Application;

public sealed record SelectionValidationResult(bool IsValid, IReadOnlyList<string> Errors);

public static class MaintenanceSelectionValidator
{
    public const int CancelledExitCode = 130;

    public static int ExitCodeForPreStartCancellation(bool cancelled) =>
        cancelled ? CancelledExitCode : 0;

    public static SelectionValidationResult Validate(MaintenanceSelection selection)
    {
        var errors = new List<string>();
        if (selection.ScriptsOnly && selection.IncludeOptional)
        {
            errors.Add("SCRIPTS_ONLY_OPTIONAL_CONFLICT");
        }

        if (selection.RepairDrift && selection.Mode != MaintenanceMode.Update)
        {
            errors.Add("REPAIR_REQUIRES_UPDATE");
        }

        if (selection.Mode == MaintenanceMode.Update && !selection.Confirmed)
        {
            errors.Add("UPDATE_CONFIRMATION_REQUIRED");
        }

        return new SelectionValidationResult(errors.Count == 0, errors);
    }
}
