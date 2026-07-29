using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Infrastructure;

public static class UiBuildCache
{
    public static string ComputeSourceFingerprint(
        string root,
        IEnumerable<string> relativePaths,
        string contractVersion)
    {
        using var incremental = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        Append(incremental, $"contract:{contractVersion}\n");
        foreach (var relativePath in relativePaths
                     .Select(path => path.Replace('\\', '/'))
                     .Order(StringComparer.Ordinal))
        {
            var absolutePath = Path.GetFullPath(relativePath, root);
            if (!File.Exists(absolutePath))
            {
                throw new FileNotFoundException("Fingerprint source is missing.", absolutePath);
            }

            Append(incremental, $"path:{relativePath}\nlength:{new FileInfo(absolutePath).Length}\n");
            incremental.AppendData(File.ReadAllBytes(absolutePath));
            Append(incremental, "\n");
        }

        return Convert.ToHexStringLower(incremental.GetHashAndReset());
    }

    public static UiPlatformId NormalizePlatform(OSPlatform platform, Architecture architecture) =>
        (platform.ToString(), architecture) switch
        {
            ("OSX", Architecture.Arm64) => UiPlatformId.MacOsArm64,
            ("OSX", Architecture.X64) => UiPlatformId.MacOsX64,
            ("LINUX", Architecture.Arm64) => UiPlatformId.LinuxArm64,
            ("LINUX", Architecture.X64) => UiPlatformId.LinuxX64,
            ("WINDOWS", Architecture.Arm64) => UiPlatformId.WindowsArm64,
            ("WINDOWS", Architecture.X64) => UiPlatformId.WindowsX64,
            _ => throw new PlatformNotSupportedException(
                $"Unsupported UI platform: {platform}/{architecture}"),
        };

    public static string ToStableName(UiPlatformId platform) => platform switch
    {
        UiPlatformId.MacOsArm64 => "macos-arm64",
        UiPlatformId.MacOsX64 => "macos-x64",
        UiPlatformId.LinuxArm64 => "linux-arm64",
        UiPlatformId.LinuxX64 => "linux-x64",
        UiPlatformId.WindowsArm64 => "windows-arm64",
        UiPlatformId.WindowsX64 => "windows-x64",
        _ => throw new ArgumentOutOfRangeException(nameof(platform)),
    };

    private static void Append(IncrementalHash hash, string value) =>
        hash.AppendData(Encoding.UTF8.GetBytes(value));
}
