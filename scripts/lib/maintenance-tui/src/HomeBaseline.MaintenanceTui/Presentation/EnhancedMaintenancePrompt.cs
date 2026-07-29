using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;
using Spectre.Console;

namespace HomeBaseline.MaintenanceTui.Presentation;

public sealed class EnhancedMaintenancePrompt
{
    private readonly IAnsiConsole _console;

    public EnhancedMaintenancePrompt(IAnsiConsole console)
    {
        _console = console;
    }

    public MaintenanceSelection? ReadSelection(string? homeDirectory)
    {
        _console.Write(new Rule(Messages.Title));
        _console.MarkupLine(MarkupText.Escape(Messages.DryRunExplanation));

        var mode = _console.Prompt(
            new SelectionPrompt<MaintenanceMode>()
                .Title("Modus wählen / Select mode")
                .AddChoices(
                    MaintenanceMode.DryRun,
                    MaintenanceMode.CheckOnly,
                    MaintenanceMode.Update)
                .DefaultValue(MaintenanceMode.DryRun)
                .UseConverter(ModeLabel));

        var scriptsOnly = _console.Confirm(
            "Nur Skripte und Governance pflegen? / Maintain scripts and governance only?",
            false);
        var includeOptional = !scriptsOnly && _console.Confirm(
            "Optionale Werkzeuge einbeziehen? / Include optional tools?",
            false);
        var repairDrift = mode == MaintenanceMode.Update && _console.Confirm(
            "Wartungspaket-Drift lokal reparieren? / Repair maintenance-package drift locally?",
            false);
        var confirmed = mode != MaintenanceMode.Update ||
                        _console.Confirm(Messages.MutationConfirmation, false);
        if (mode == MaintenanceMode.Update && !confirmed)
        {
            _console.MarkupLine(MarkupText.Escape(Messages.Cancelled));
            return null;
        }

        var selection = new MaintenanceSelection(
            mode,
            scriptsOnly,
            includeOptional,
            repairDrift,
            homeDirectory,
            confirmed);
        var validation = MaintenanceSelectionValidator.Validate(selection);
        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                _console.MarkupLine($"[red]{MarkupText.Escape(error)}[/]");
            }

            return null;
        }

        return selection;
    }

    private static string ModeLabel(MaintenanceMode mode) => mode switch
    {
        MaintenanceMode.DryRun => Messages.DryRunLabel,
        MaintenanceMode.CheckOnly => Messages.CheckOnlyLabel,
        MaintenanceMode.Update => Messages.UpdateLabel,
        _ => mode.ToString(),
    };
}
