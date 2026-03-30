// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Compatibility;
using TuiVision.Core;
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

    /// <summary>
    /// Prüft, dass <see cref="DriverCapabilityBucket.DisplayAdaptation"/> den verwalteten
    /// Unicode-/Encoding-Ersatz für historische Codepage-Verwaltung explizit benennt.
    ///
    /// Verifies that <see cref="DriverCapabilityBucket.DisplayAdaptation"/> explicitly names the
    /// managed Unicode/encoding replacement for historical codepage management.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_DisplayAdaptationBucket_ReferencesUnicodeAndEncoding()
    {
        string replacement = DriverCapabilityMap.GetManagedReplacement(DriverCapabilityBucket.DisplayAdaptation);

        Assert.IsTrue(
            replacement.Contains("Unicode", StringComparison.OrdinalIgnoreCase),
            $"DisplayAdaptation-Ersatz muss Unicode erwähnen. " +
            $"DisplayAdaptation replacement must mention Unicode.\n{replacement}");
        Assert.IsTrue(
            replacement.Contains("System.Text.Encoding", StringComparison.Ordinal),
            $"DisplayAdaptation-Ersatz muss System.Text.Encoding erwähnen. " +
            $"DisplayAdaptation replacement must mention System.Text.Encoding.\n{replacement}");
    }

    /// <summary>
    /// Prüft, dass die verwaltete Tastaturbrücke die konsolenseitige Taste in ein
    /// Turbo-Vision-Ereignis überführt und dabei die xterm-kompatible Navigationsmenge abdeckt.
    ///
    /// Verifies that the managed keyboard bridge translates a console-side key into a
    /// Turbo Vision event while covering the xterm-compatible navigation subset.
    /// </summary>
    [TestMethod]
    public void ConsoleInputAdapter_KeyTranslation_ProducesTurboVisionEvent()
    {
        var keyInfo = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, shift: false, alt: false, control: false);
        TEvent result = TConsoleInputAdapter.CreateKeyDownEvent(keyInfo);

        Assert.AreEqual(TEventKind.KeyDown, result.What,
            "Die Tastaturbrücke muss ein KeyDown-Ereignis erzeugen. " +
            "The keyboard bridge must create a KeyDown event.");
        Assert.AreEqual((ushort)0x4B00, result.KeyDown.KeyCode,
            "LeftArrow muss als TV-KeyCode 0x4B00 abgebildet werden. " +
            "LeftArrow must be mapped to TV key code 0x4B00.");
        Assert.IsTrue(TConsoleInputAdapter.IsXtermCompatibleKey(ConsoleKey.LeftArrow),
            "LeftArrow muss als xterm-kompatible Taste markiert sein. " +
            "LeftArrow must be marked as an xterm-compatible key.");
    }

    /// <summary>
    /// Prüft, dass das verwaltete Mausereignismodell Koordinaten, Buttons und Doppelklickstatus
    /// ohne plattformspezifischen Raw-Driver transportiert.
    ///
    /// Verifies that the managed mouse event model carries coordinates, buttons, and double-click
    /// state without a platform-specific raw driver.
    /// </summary>
    [TestMethod]
    public void MouseEventModel_CreateMouse_PreservesButtonsAndCoordinates()
    {
        TEvent result = TEvent.CreateMouse(
            TEventKind.MouseMove,
            TMouseButtons.Left | TMouseButtons.Right,
            doubleClick: true,
            new TPoint(12, 4));

        Assert.AreEqual(TEventKind.MouseMove, result.What,
            "CreateMouse muss den übergebenen Mausereignistyp bewahren. " +
            "CreateMouse must preserve the provided mouse event kind.");
        Assert.AreEqual(TMouseButtons.Left | TMouseButtons.Right, result.Mouse.Buttons,
            "CreateMouse muss die gedrückten Buttons bewahren. " +
            "CreateMouse must preserve the pressed buttons.");
        Assert.IsTrue(result.Mouse.DoubleClick,
            "CreateMouse muss den Doppelklickstatus bewahren. " +
            "CreateMouse must preserve the double-click state.");
        Assert.AreEqual(new TPoint(12, 4), result.Mouse.Where,
            "CreateMouse muss die Koordinaten bewahren. " +
            "CreateMouse must preserve the coordinates.");
    }

    /// <summary>
    /// Prüft, dass die MouseInput-Fähigkeitsbeschreibung den bewussten Verzicht auf einen
    /// plattformspezifischen Raw-Maus-Treiber ausdrücklich dokumentiert.
    ///
    /// Verifies that the MouseInput capability description explicitly documents the conscious
    /// omission of a platform-specific raw mouse driver.
    /// </summary>
    [TestMethod]
    public void CapabilityMap_MouseInputBucket_DocumentsIntentionalOmission()
    {
        string replacement = DriverCapabilityMap.GetManagedReplacement(DriverCapabilityBucket.MouseInput);

        Assert.IsTrue(
            replacement.Contains("TEvent", StringComparison.Ordinal),
            $"MouseInput-Ersatz muss TEvent erwähnen. " +
            $"MouseInput replacement must mention TEvent.\n{replacement}");
        Assert.IsTrue(
            replacement.Contains("consciously", StringComparison.OrdinalIgnoreCase) ||
            replacement.Contains("not reproduced", StringComparison.OrdinalIgnoreCase),
            $"MouseInput-Ersatz muss den bewussten Verzicht dokumentieren. " +
            $"MouseInput replacement must document the conscious omission.\n{replacement}");
    }

    // ── Gate-006-Integritätsprüfungen / Gate-006 integrity assertions ─────────

    /// <summary>
    /// Gate-006-Integritätsprüfung: <c>TuiVision.Drivers.Console</c> darf nicht ausschließlich
    /// No-op-Code enthalten. <see cref="TConsoleDriver"/> muss echte Buffer-Verwaltung
    /// implementieren, messbar durch WriteText/GetCell.
    ///
    /// Gate-006 integrity check: <c>TuiVision.Drivers.Console</c> must not contain only
    /// no-op code. <see cref="TConsoleDriver"/> must implement real buffer management,
    /// measurable through WriteText/GetCell.
    /// </summary>
    [TestMethod]
    public void Driver_HasRealBufferImplementation_Gate006()
    {
        var driver = new TConsoleDriver(5, 3);

        // Must have a real back buffer (not null, not throw)
        Assert.IsNotNull(driver.BackBuffer,
            "Gate-006: TConsoleDriver.BackBuffer darf nicht null sein. " +
            "Gate-006: TConsoleDriver.BackBuffer must not be null.");

        // Writes must be observable through GetCell — no-op would fail this
        driver.BackBuffer.WriteText(0, 0, "Hi".AsSpan(), ConsoleColor.Cyan, ConsoleColor.DarkBlue);
        Assert.AreEqual('H', driver.BackBuffer.GetCell(0, 0).Glyph,
            "Gate-006: TConsoleDriver.BackBuffer.WriteText ist eine No-op-Implementierung. " +
            "Gate-006: TConsoleDriver.BackBuffer.WriteText is a no-op implementation.");
        Assert.AreEqual('i', driver.BackBuffer.GetCell(1, 0).Glyph,
            "Gate-006: TConsoleDriver.BackBuffer.WriteText schreibt keine zweite Zelle. " +
            "Gate-006: TConsoleDriver.BackBuffer.WriteText does not write the second cell.");

        // Colour information must be stored (no-op driver would use defaults)
        Assert.AreEqual(ConsoleColor.Cyan, driver.BackBuffer.GetCell(0, 0).Foreground,
            "Gate-006: TConsoleDriver.BackBuffer muss Vordergrundfarbe speichern. " +
            "Gate-006: TConsoleDriver.BackBuffer must store foreground colour.");
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
