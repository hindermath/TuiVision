using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Tests.Integration;

[TestClass]
public sealed class CacheIntegrationTests
{
    [TestMethod]
    public void CompleteDescriptorRequiresAssemblyAndMetadata()
    {
        var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(root);
        try
        {
            Assert.IsFalse(UiCachePublication.IsComplete(root, "app.dll", "cache.json"));
            File.WriteAllText(Path.Combine(root, "app.dll"), "fixture");
            File.WriteAllText(
                Path.Combine(root, "cache.json"),
                """{"schemaVersion":1,"fingerprint":"abc","platform":"macos-arm64"}""");
            Assert.IsTrue(UiCachePublication.IsComplete(root, "app.dll", "cache.json"));
            Assert.IsTrue(UiCachePublication.IsComplete(
                root,
                "app.dll",
                "cache.json",
                "abc",
                "macos-arm64"));
            Assert.IsFalse(UiCachePublication.IsComplete(
                root,
                "app.dll",
                "cache.json",
                "stale",
                "macos-arm64"));
            Assert.IsFalse(UiCachePublication.IsComplete(
                root,
                "app.dll",
                "cache.json",
                "abc",
                "windows-x64"));
            File.WriteAllText(Path.Combine(root, "cache.json"), "{broken");
            Assert.IsFalse(UiCachePublication.IsComplete(
                root,
                "app.dll",
                "cache.json",
                "abc",
                "macos-arm64"));
            File.WriteAllText(
                Path.Combine(root, "cache.json"),
                """{"schemaVersion":"1","fingerprint":"abc","platform":"macos-arm64"}""");
            Assert.IsFalse(UiCachePublication.IsComplete(
                root,
                "app.dll",
                "cache.json",
                "abc",
                "macos-arm64"));
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [TestMethod]
    public void AtomicPublicationRejectsAnExistingDestination()
    {
        var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var temporary = Path.Combine(root, "temporary");
        var final = Path.Combine(root, "final");
        Directory.CreateDirectory(temporary);
        Directory.CreateDirectory(final);
        try
        {
            Assert.ThrowsExactly<IOException>(
                () => UiCachePublication.PublishAtomically(temporary, final));
            Assert.IsTrue(Directory.Exists(temporary));
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }

    [TestMethod]
    public void AtomicPublicationMakesOnlyTheCompleteDirectoryVisible()
    {
        var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var temporary = Path.Combine(root, "temporary");
        var final = Path.Combine(root, "final");
        Directory.CreateDirectory(temporary);
        File.WriteAllText(Path.Combine(temporary, "app.dll"), "fixture");
        File.WriteAllText(
            Path.Combine(temporary, "cache.json"),
            """{"schemaVersion":1,"fingerprint":"abc","platform":"linux-x64"}""");
        try
        {
            UiCachePublication.PublishAtomically(temporary, final);

            Assert.IsFalse(Directory.Exists(temporary));
            Assert.IsTrue(UiCachePublication.IsComplete(
                final,
                "app.dll",
                "cache.json",
                "abc",
                "linux-x64"));
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }
}
