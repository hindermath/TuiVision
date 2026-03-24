// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Drivers.Console;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Kompatibilitäts- und Managed-Only-Konformitätstests für <see cref="TConsoleDriver"/>.
/// Belegt, dass der Treiber keine nativen Bindungen, native Pakete oder Plattform-Spezifika
/// außerhalb der verwalteten .NET-10-Laufzeit erfordert.
///
/// Compatibility and managed-only conformance tests for <see cref="TConsoleDriver"/>.
/// Prove that the driver requires no native bindings, native packages, or platform specifics
/// outside the managed .NET 10 runtime.
/// </summary>
[TestClass]
public sealed class TConsoleDriverCompatibilityTests
{
    // ── Verwaltete Treiberoberfläche / Managed driver surface ─────────────────

    /// <summary>
    /// Prüft, dass <see cref="TConsoleDriver"/> keinen Assembly-Verweis auf
    /// plattformspezifische oder native Bibliotheken enthält.
    ///
    /// Verifies that <see cref="TConsoleDriver"/> does not reference any
    /// platform-specific or native library assemblies.
    /// </summary>
    [TestMethod]
    public void Driver_Assembly_ContainsNoPlatformSpecificReferences()
    {
        var driverAssembly = typeof(TConsoleDriver).Assembly;
        var referencedNames = driverAssembly.GetReferencedAssemblies()
            .Select(a => a.Name ?? string.Empty)
            .ToList();

        string[] forbiddenPatterns = ["native", "interop", "pinvoke", "win32", "linux.native", "posix"];

        foreach (string name in referencedNames)
        {
            string lower = name.ToLowerInvariant();
            foreach (string forbidden in forbiddenPatterns)
            {
                Assert.IsFalse(
                    lower.Contains(forbidden, StringComparison.OrdinalIgnoreCase),
                    $"Treiber-Assembly verweist auf verdächtige Bibliothek '{name}'. " +
                    $"Driver assembly references suspicious library '{name}'.");
            }
        }
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleDriver"/> und <see cref="IConsolePresenter"/>
    /// im Namespace <c>TuiVision.Drivers.Console</c> deklariert sind.
    ///
    /// Verifies that <see cref="TConsoleDriver"/> and <see cref="IConsolePresenter"/>
    /// are declared in the <c>TuiVision.Drivers.Console</c> namespace.
    /// </summary>
    [TestMethod]
    public void Driver_Types_DeclaredInCorrectNamespace()
    {
        Assert.AreEqual("TuiVision.Drivers.Console", typeof(TConsoleDriver).Namespace);
        Assert.AreEqual("TuiVision.Drivers.Console", typeof(IConsolePresenter).Namespace);
    }

    // ── Fähigkeitsgruppen-Kompatibilität / Capability bucket compatibility ────

    /// <summary>
    /// Prüft, dass <see cref="DriverCapabilityMap.GetManagedReplacement"/> für jede
    /// Fähigkeitsgruppe eine Beschreibung liefert, die auf verwaltete .NET-APIs verweist.
    ///
    /// Verifies that <see cref="DriverCapabilityMap.GetManagedReplacement"/> returns for every
    /// capability bucket a description that references managed .NET APIs.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_ManagedReplacementsReferenceNetApis()
    {
        string[] managedApiMarkers = ["System.Console", ".NET", "TConsoleDriver", "managed", "TuiVision"];

        foreach (var bucket in DriverCapabilityMap.AllBuckets)
        {
            string replacement = DriverCapabilityMap.GetManagedReplacement(bucket);
            bool mentionsAnyManagedApi = managedApiMarkers.Any(marker =>
                replacement.Contains(marker, StringComparison.OrdinalIgnoreCase));

            Assert.IsTrue(
                mentionsAnyManagedApi,
                $"Fähigkeitsgruppe '{bucket}': Ersatzbeschreibung erwähnt keine verwaltete .NET-API. " +
                $"Bucket '{bucket}': replacement description does not mention any managed .NET API.\n" +
                $"Beschreibung / Description: {replacement}");
        }
    }

    /// <summary>
    /// Prüft, dass <see cref="DriverCapabilityBucket.ScreenPresentation"/> die
    /// Typen <see cref="TConsoleDriver"/> und <see cref="IConsolePresenter"/> referenziert.
    ///
    /// Verifies that <see cref="DriverCapabilityBucket.ScreenPresentation"/> references
    /// <see cref="TConsoleDriver"/> and <see cref="IConsolePresenter"/> types.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_ScreenPresentationBucket_ReferencesTConsoleDriver()
    {
        string replacement = DriverCapabilityMap.GetManagedReplacement(DriverCapabilityBucket.ScreenPresentation);
        Assert.IsTrue(
            replacement.Contains("TConsoleDriver", StringComparison.Ordinal),
            $"ScreenPresentation-Ersatz muss TConsoleDriver erwähnen. " +
            $"ScreenPresentation replacement must mention TConsoleDriver.\n{replacement}");
    }

    /// <summary>
    /// Prüft, dass <see cref="DriverCapabilityBucket.KeyboardInput"/> auf
    /// <c>System.Console.ReadKey</c> oder die verwaltete Eingabe-API verweist.
    ///
    /// Verifies that <see cref="DriverCapabilityBucket.KeyboardInput"/> references
    /// <c>System.Console.ReadKey</c> or the managed input API.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_KeyboardInputBucket_ReferencesConsoleReadKey()
    {
        string replacement = DriverCapabilityMap.GetManagedReplacement(DriverCapabilityBucket.KeyboardInput);
        Assert.IsTrue(
            replacement.Contains("ReadKey", StringComparison.Ordinal),
            $"KeyboardInput-Ersatz muss ReadKey erwähnen. " +
            $"KeyboardInput replacement must mention ReadKey.\n{replacement}");
    }

    // ── Kompatibilitätsnachweis-Hilfstests / Compatibility evidence helper tests ──

    /// <summary>
    /// Prüft, dass der historische Quelldatei-Inventar (alle .cc-Dateien) mindestens
    /// 140 Dateien umfasst — damit der Testkontext das vollständige Inventar abdeckt.
    ///
    /// Verifies that the historical source file inventory (all .cc files) covers at least
    /// 140 files — ensuring the test context covers the complete inventory.
    /// </summary>
    [TestMethod]
    public void HistoricalInventory_ContainsAtLeast140CcFiles()
    {
        var files = Phase7DriverTestContext.GetHistoricalCcFiles();
        Assert.IsGreaterThanOrEqualTo(140, files.Count,
            $"Historisches Inventar sollte mindestens 140 .cc-Dateien enthalten, gefunden: {files.Count}. " +
            "Historical inventory should contain at least 140 .cc files.");
    }

    /// <summary>
    /// Prüft, dass das historische Inventar Dateien aus allen bekannten Plattform-Unterverzeichnissen enthält.
    ///
    /// Verifies that the historical inventory contains files from all known platform subdirectories.
    /// </summary>
    [TestMethod]
    public void HistoricalInventory_ContainsAllExpectedPlatformFamilies()
    {
        var files = Phase7DriverTestContext.GetHistoricalCcFiles();
        string[] expectedPlatforms = ["dos/", "linux/", "unix/", "win32/", "winnt/", "wingr/", "qnx4/", "qnxrtp/", "x11/"];

        foreach (string platform in expectedPlatforms)
        {
            Assert.IsTrue(
                files.Any(f => f.Contains(platform, StringComparison.Ordinal)),
                $"Historisches Inventar enthält keine Dateien aus Plattform '{platform}'. " +
                $"Historical inventory contains no files from platform '{platform}'.");
        }
    }
}
