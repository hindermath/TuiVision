using Spectre.Console;

namespace HomeBaseline.MaintenanceTui.Presentation;

public static class CommandSummaryView
{
    public static void Render(IAnsiConsole console, string displayCommand)
    {
        console.Write(new Rule("Ausführung / Execution"));
        console.WriteLine(displayCommand);
    }
}
