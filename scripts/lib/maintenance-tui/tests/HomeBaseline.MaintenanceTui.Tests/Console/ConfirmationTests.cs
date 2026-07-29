using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class ConfirmationTests
{
    [TestMethod]
    public void UpdateRequiresExplicitConfirmation()
    {
        var result = MaintenanceSelectionValidator.Validate(
            new MaintenanceSelection(MaintenanceMode.Update));

        Assert.IsFalse(result.IsValid);
        CollectionAssert.Contains(result.Errors.ToArray(), "UPDATE_CONFIRMATION_REQUIRED");
    }

    [TestMethod]
    [DataRow(true, 130)]
    [DataRow(false, 0)]
    public void CancellationBeforeStartMapsToCanonicalExit(bool cancelled, int expected)
    {
        Assert.AreEqual(expected, MaintenanceSelectionValidator.ExitCodeForPreStartCancellation(cancelled));
    }
}
