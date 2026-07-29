using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class TerminalCapabilityTests
{
    [TestMethod]
    public void ExplicitEnhancedFallsBackToPlainWhenTerminalIsUnsupported()
    {
        var context = new InvocationContext(
            HostPlatform.Linux,
            HostArchitecture.X64,
            true,
            true,
            new TerminalCapabilities(false, false, 80, "TERM=dumb"),
            UiMode.Enhanced,
            "/tmp/home",
            []);

        var result = InvocationRouter.Route(context);

        Assert.AreEqual(InvocationRoute.Plain, result.Route);
        StringAssert.Contains(result.Reason, "TERM=dumb");
    }

    [TestMethod]
    public void NoColorDoesNotRemoveKeyboardInteraction()
    {
        var context = new InvocationContext(
            HostPlatform.Linux,
            HostArchitecture.X64,
            true,
            true,
            new TerminalCapabilities(false, true, 80),
            UiMode.Enhanced,
            "/tmp/home",
            []);

        Assert.AreEqual(InvocationRoute.Enhanced, InvocationRouter.Route(context).Route);
        Assert.IsFalse(context.TerminalCapabilities.SupportsColor);
    }
}
