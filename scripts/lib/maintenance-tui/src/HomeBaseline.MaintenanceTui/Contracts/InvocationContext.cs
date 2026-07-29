namespace HomeBaseline.MaintenanceTui.Contracts;

public enum HostPlatform
{
    MacOs,
    Linux,
    Windows,
}

public enum HostArchitecture
{
    Arm64,
    X64,
}

public enum UiMode
{
    Auto,
    Enhanced,
    Plain,
    Headless,
}

public sealed record TerminalCapabilities(
    bool SupportsColor,
    bool IsInteractive,
    int Width,
    string? FallbackReason = null);

public sealed record InvocationContext(
    HostPlatform Platform,
    HostArchitecture Architecture,
    bool StandardInputInteractive,
    bool StandardOutputInteractive,
    TerminalCapabilities TerminalCapabilities,
    UiMode ExplicitUiMode,
    string HomeDirectory,
    IReadOnlyList<string> ExistingArguments)
{
    public bool HasFullTerminal =>
        StandardInputInteractive &&
        StandardOutputInteractive &&
        TerminalCapabilities.IsInteractive;
}
