namespace HomeBaseline.MaintenanceTui.Presentation;

public static class Messages
{
    public const string Title =
        "Agentischer Workspace: Wartung / Agentic workspace maintenance";

    public const string DryRunLabel =
        "Vorschau (Dry-run)";

    public const string DryRunExplanation =
        "Die Vorschau zeigt geplante Änderungen und nimmt keine Änderung vor. / " +
        "The dry-run shows planned changes and does not modify the workspace.";

    public const string CheckOnlyLabel =
        "Nur prüfen (Check-only)";

    public const string UpdateLabel =
        "Aktualisieren (Update)";

    public const string MutationConfirmation =
        "Schreibende Wartung wirklich einmal starten? / Start one mutating maintenance run?";

    public const string MutationBarrierExplanation =
        "Mutationsbarriere: Sperre vor Änderungen, bis alle Pflichtprüfungen bestanden sind. / " +
        "Mutation barrier: gate before changes until every required check has passed.";

    public const string Cancelled =
        "Vor dem Engine-Start abgebrochen. / Cancelled before engine start.";
}
