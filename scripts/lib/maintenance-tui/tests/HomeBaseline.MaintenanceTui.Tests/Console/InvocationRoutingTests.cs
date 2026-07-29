using HomeBaseline.MaintenanceTui.Application;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class InvocationRoutingTests
{
    [TestMethod]
    [DataRow(true, true, UiMode.Auto, InvocationRoute.Enhanced)]
    [DataRow(false, true, UiMode.Auto, InvocationRoute.Headless)]
    [DataRow(true, false, UiMode.Auto, InvocationRoute.Headless)]
    [DataRow(true, true, UiMode.Plain, InvocationRoute.Plain)]
    [DataRow(true, true, UiMode.Headless, InvocationRoute.Headless)]
    [DataRow(true, true, UiMode.Enhanced, InvocationRoute.Enhanced)]
    [DataRow(false, false, UiMode.Enhanced, InvocationRoute.Plain)]
    public void RouteMatchesTerminalAndExplicitMode(
        bool input,
        bool output,
        UiMode mode,
        InvocationRoute expected)
    {
        var context = Context(input, output, mode, input && output, []);

        var result = InvocationRouter.Route(context);

        Assert.AreEqual(expected, result.Route);
    }

    [TestMethod]
    public void ExistingMaintenanceArgumentAlwaysUsesHeadless()
    {
        var context = Context(true, true, UiMode.Auto, true, ["--check-only"]);

        Assert.AreEqual(InvocationRoute.Headless, InvocationRouter.Route(context).Route);
    }

    private static InvocationContext Context(
        bool input,
        bool output,
        UiMode mode,
        bool terminal,
        IReadOnlyList<string> arguments) =>
        new(
            HostPlatform.MacOs,
            HostArchitecture.Arm64,
            input,
            output,
            new TerminalCapabilities(true, terminal, 120),
            mode,
            "/tmp/home",
            arguments);
}
