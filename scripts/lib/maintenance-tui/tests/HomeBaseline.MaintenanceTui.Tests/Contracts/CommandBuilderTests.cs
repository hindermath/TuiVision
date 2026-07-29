using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Contracts;

[TestClass]
public sealed class CommandBuilderTests
{
    [TestMethod]
    public void BashArgumentsRemainTypedWhenPathContainsShellText()
    {
        var runId = Guid.NewGuid();
        var context = Context(HostPlatform.MacOs);
        var invocation = new MaintenanceCommandBuilder().Build(
            context,
            new MaintenanceSelection(MaintenanceMode.DryRun, HomeDirectory: "/tmp/a b;echo no"),
            "/repo/scripts/maintain-agentic-workspace.sh",
            "/tmp/events.jsonl",
            runId);

        CollectionAssert.Contains(invocation.Arguments.ToArray(), "/tmp/a b;echo no");
        StringAssert.Contains(invocation.DisplayCommand, "'/tmp/a b;echo no'");
        Assert.AreEqual("bash", invocation.Executable);
    }

    [TestMethod]
    public void PowerShellUsesNativeSwitches()
    {
        var invocation = new MaintenanceCommandBuilder().Build(
            Context(HostPlatform.Windows),
            new MaintenanceSelection(MaintenanceMode.CheckOnly, ScriptsOnly: true),
            @"C:\repo\scripts\maintain-agentic-workspace.ps1",
            @"C:\temp\events.jsonl",
            Guid.NewGuid());

        CollectionAssert.Contains(invocation.Arguments.ToArray(), "-CheckOnly");
        CollectionAssert.Contains(invocation.Arguments.ToArray(), "-ScriptsOnly");
        CollectionAssert.Contains(invocation.Arguments.ToArray(), "-NoTui");
    }

    [TestMethod]
    public void UpdateOptionsMapWithoutCreatingExecutableShellText()
    {
        var selection = new MaintenanceSelection(
            MaintenanceMode.Update,
            IncludeOptional: true,
            RepairDrift: true,
            Confirmed: true);
        var invocation = new MaintenanceCommandBuilder().Build(
            Context(HostPlatform.Linux),
            selection,
            "/repo/wrapper.sh",
            "/tmp/events.jsonl",
            Guid.NewGuid());

        CollectionAssert.Contains(invocation.Arguments.ToArray(), "--include-optional");
        CollectionAssert.Contains(invocation.Arguments.ToArray(), "--repair-drift");
        CollectionAssert.DoesNotContain(invocation.Arguments.ToArray(), "--dry-run");
        CollectionAssert.DoesNotContain(invocation.Arguments.ToArray(), "--check-only");
        StringAssert.StartsWith(invocation.DisplayCommand, "'bash' ");
    }

    private static InvocationContext Context(HostPlatform platform) =>
        new(
            platform,
            HostArchitecture.X64,
            true,
            true,
            new TerminalCapabilities(true, true, 120),
            UiMode.Enhanced,
            "/tmp/home",
            []);
}
