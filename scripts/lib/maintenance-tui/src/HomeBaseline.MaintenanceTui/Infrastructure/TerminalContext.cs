using System.Runtime.InteropServices;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Infrastructure;

public static class TerminalContext
{
    public static InvocationContext Capture(
        UiMode explicitMode,
        string homeDirectory,
        IReadOnlyList<string> existingArguments)
    {
        var platform = OperatingSystem.IsWindows()
            ? HostPlatform.Windows
            : OperatingSystem.IsMacOS()
                ? HostPlatform.MacOs
                : HostPlatform.Linux;
        var architecture = RuntimeInformation.ProcessArchitecture == Architecture.Arm64
            ? HostArchitecture.Arm64
            : HostArchitecture.X64;
        var inputInteractive = !System.Console.IsInputRedirected;
        var outputInteractive = !System.Console.IsOutputRedirected;
        var noColor = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NO_COLOR"));
        var term = Environment.GetEnvironmentVariable("TERM");
        var terminalInteractive =
            inputInteractive &&
            outputInteractive &&
            !string.Equals(term, "dumb", StringComparison.OrdinalIgnoreCase);
        var fallback = terminalInteractive
            ? null
            : "Terminal ist nicht vollständig interaktiv / terminal is not fully interactive.";

        return new InvocationContext(
            platform,
            architecture,
            inputInteractive,
            outputInteractive,
            new TerminalCapabilities(!noColor && terminalInteractive, terminalInteractive, GetWidth(), fallback),
            explicitMode,
            Path.GetFullPath(homeDirectory),
            existingArguments);
    }

    private static int GetWidth()
    {
        try
        {
            return System.Console.WindowWidth > 0 ? System.Console.WindowWidth : 80;
        }
        catch (IOException)
        {
            return 80;
        }
    }
}
