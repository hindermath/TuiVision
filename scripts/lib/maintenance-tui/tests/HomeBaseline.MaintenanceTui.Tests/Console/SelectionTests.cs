using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Presentation;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class SelectionTests
{
    [TestMethod]
    public void DefaultSelectionUsesDryRun()
    {
        Assert.AreEqual(MaintenanceMode.DryRun, MaintenanceSelection.Default.Mode);
        Assert.AreEqual(StorageCleanupProfile.Safe, MaintenanceSelection.Default.CleanupProfile);
        Assert.IsFalse(MaintenanceSelection.Default.Confirmed);
    }

    [TestMethod]
    public void EveryModeCombinationHasOneDeterministicValidationResult()
    {
        foreach (var mode in Enum.GetValues<MaintenanceMode>())
            foreach (var scriptsOnly in new[] { false, true })
                foreach (var includeOptional in new[] { false, true })
                    foreach (var repairDrift in new[] { false, true })
                        foreach (var confirmed in new[] { false, true })
                            foreach (var cleanupProfile in Enum.GetValues<StorageCleanupProfile>())
                                foreach (var confirmDeepCleanup in new[] { false, true })
                                {
                                    var selection = new MaintenanceSelection(
                                        mode,
                                        scriptsOnly,
                                        includeOptional,
                                        repairDrift,
                                        Confirmed: confirmed,
                                        CleanupProfile: cleanupProfile,
                                        ConfirmDeepCleanup: confirmDeepCleanup);
                                    var expected = !(scriptsOnly && includeOptional) &&
                                                   (!scriptsOnly || cleanupProfile == StorageCleanupProfile.None) &&
                                                   (!repairDrift || mode == MaintenanceMode.Update) &&
                                                   (mode != MaintenanceMode.Update || confirmed) &&
                                                   (mode != MaintenanceMode.Update ||
                                                    cleanupProfile != StorageCleanupProfile.Deep ||
                                                    confirmDeepCleanup);

                                    Assert.AreEqual(
                                        expected,
                                        MaintenanceSelectionValidator.Validate(selection).IsValid,
                                        selection.ToString());
                                }
    }

    [TestMethod]
    public void PlainPromptUsesDryRunAndDefaultNoChoices()
    {
        using var input = new StringReader("\n\n\n\n");
        using var output = new StringWriter();

        var selection = new PlainMaintenancePrompt(input, output).ReadSelection("/tmp/home");

        Assert.IsNotNull(selection);
        Assert.AreEqual(MaintenanceMode.DryRun, selection.Mode);
        Assert.IsFalse(selection.ScriptsOnly);
        Assert.IsFalse(selection.IncludeOptional);
        Assert.IsFalse(selection.RepairDrift);
        Assert.AreEqual(StorageCleanupProfile.Safe, selection.CleanupProfile);
    }
}
