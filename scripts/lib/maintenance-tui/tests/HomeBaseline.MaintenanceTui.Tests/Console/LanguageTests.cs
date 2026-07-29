using HomeBaseline.MaintenanceTui.Presentation;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class LanguageTests
{
    [TestMethod]
    public void LabelsAreGermanFirstAndExplainDryRun()
    {
        StringAssert.StartsWith(Messages.DryRunLabel, "Vorschau");
        StringAssert.Contains(Messages.DryRunLabel, "Dry-run");
        StringAssert.Contains(Messages.DryRunExplanation, "keine");
        StringAssert.Contains(Messages.DryRunExplanation, "does not");
        StringAssert.StartsWith(Messages.MutationBarrierExplanation, "Mutationsbarriere");
        StringAssert.Contains(Messages.MutationBarrierExplanation, "Sperre vor Änderungen");
        StringAssert.Contains(Messages.MutationBarrierExplanation, "gate before changes");
    }
}
