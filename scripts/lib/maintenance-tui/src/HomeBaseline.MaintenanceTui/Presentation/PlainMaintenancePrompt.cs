using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Presentation;

public sealed class PlainMaintenancePrompt
{
    private readonly TextReader _input;
    private readonly TextWriter _output;

    public PlainMaintenancePrompt(TextReader input, TextWriter output)
    {
        _input = input;
        _output = output;
    }

    public MaintenanceSelection? ReadSelection(string? homeDirectory)
    {
        _output.WriteLine(Messages.Title);
        _output.WriteLine(Messages.DryRunExplanation);
        _output.WriteLine("1) Vorschau / Dry-run [Standard / default]");
        _output.WriteLine("2) Nur prüfen / Check-only");
        _output.WriteLine("3) Aktualisieren / Update");
        _output.Write("Auswahl / Selection [1]: ");
        var raw = _input.ReadLine()?.Trim();
        var mode = raw switch
        {
            "2" => MaintenanceMode.CheckOnly,
            "3" => MaintenanceMode.Update,
            _ => MaintenanceMode.DryRun,
        };
        var scriptsOnly = AskYesNo(
            "Nur Skripte? / Scripts only? [y/N]: ");
        var includeOptional = !scriptsOnly && AskYesNo(
            "Optionale Werkzeuge? / Optional tools? [y/N]: ");
        var repair = mode == MaintenanceMode.Update && AskYesNo(
            "Drift lokal reparieren? / Repair drift locally? [y/N]: ");
        var confirmed = mode != MaintenanceMode.Update ||
                        AskYesNo("Schreibenden Lauf starten? / Start mutating run? [y/N]: ");
        if (mode == MaintenanceMode.Update && !confirmed)
        {
            _output.WriteLine(Messages.Cancelled);
            return null;
        }

        var selection = new MaintenanceSelection(
            mode,
            scriptsOnly,
            includeOptional,
            repair,
            homeDirectory,
            confirmed);
        return MaintenanceSelectionValidator.Validate(selection).IsValid ? selection : null;
    }

    private bool AskYesNo(string prompt)
    {
        _output.Write(prompt);
        var answer = _input.ReadLine()?.Trim();
        return string.Equals(answer, "y", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(answer, "j", StringComparison.OrdinalIgnoreCase);
    }
}
