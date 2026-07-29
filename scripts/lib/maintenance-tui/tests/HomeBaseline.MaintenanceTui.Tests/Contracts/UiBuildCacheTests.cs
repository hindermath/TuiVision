using System.Runtime.InteropServices;
using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Contracts;

[TestClass]
public sealed class UiBuildCacheTests
{
    [TestMethod]
    public void FingerprintIsOrderIndependentAndChangesWithContent()
    {
        var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(root);
        try
        {
            File.WriteAllText(Path.Combine(root, "a.txt"), "a");
            File.WriteAllText(Path.Combine(root, "b.txt"), "b");
            var first = UiBuildCache.ComputeSourceFingerprint(root, ["b.txt", "a.txt"], "1");
            var second = UiBuildCache.ComputeSourceFingerprint(root, ["a.txt", "b.txt"], "1");
            File.WriteAllText(Path.Combine(root, "b.txt"), "changed");
            var changed = UiBuildCache.ComputeSourceFingerprint(root, ["a.txt", "b.txt"], "1");
            var contractChanged = UiBuildCache.ComputeSourceFingerprint(root, ["a.txt", "b.txt"], "2");
            File.WriteAllText(Path.Combine(root, "packages.lock.json"), """{"version":1}""");
            var firstLock = UiBuildCache.ComputeSourceFingerprint(
                root,
                ["a.txt", "b.txt", "packages.lock.json"],
                "2");
            File.WriteAllText(Path.Combine(root, "packages.lock.json"), """{"version":2}""");
            var changedLock = UiBuildCache.ComputeSourceFingerprint(
                root,
                ["a.txt", "b.txt", "packages.lock.json"],
                "2");

            Assert.AreEqual(first, second);
            Assert.AreNotEqual(first, changed);
            Assert.AreNotEqual(changed, contractChanged);
            Assert.AreNotEqual(firstLock, changedLock);
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [TestMethod]
    [DataRow("OSX", Architecture.Arm64, UiPlatformId.MacOsArm64)]
    [DataRow("OSX", Architecture.X64, UiPlatformId.MacOsX64)]
    [DataRow("LINUX", Architecture.Arm64, UiPlatformId.LinuxArm64)]
    [DataRow("LINUX", Architecture.X64, UiPlatformId.LinuxX64)]
    [DataRow("WINDOWS", Architecture.Arm64, UiPlatformId.WindowsArm64)]
    [DataRow("WINDOWS", Architecture.X64, UiPlatformId.WindowsX64)]
    public void PlatformNormalizationSupportsSixTargets(
        string platform,
        Architecture architecture,
        UiPlatformId expected)
    {
        Assert.AreEqual(expected, UiBuildCache.NormalizePlatform(OSPlatform.Create(platform), architecture));
    }
}
