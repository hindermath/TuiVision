namespace HomeBaseline.MaintenanceTui.Contracts;

public enum UiPlatformId
{
    MacOsArm64,
    MacOsX64,
    LinuxArm64,
    LinuxX64,
    WindowsArm64,
    WindowsX64,
}

public sealed record UiBuildDescriptor(
    string SourceFingerprint,
    UiPlatformId PlatformId,
    string CacheDirectory,
    string EntryAssembly,
    bool PublishedAtomically);
