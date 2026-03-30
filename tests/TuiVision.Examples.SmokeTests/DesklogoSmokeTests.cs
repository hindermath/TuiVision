// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Desklogo;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests für das Desklogo-Beispiel.
/// Prüft den Start, das statische Desktop-Logo-Rendering, das Verhalten bei
/// zu kleinen Terminals und den sauberen Beendigungspfad.
///
/// Smoke tests for the Desklogo example.
/// Verifies startup, static desktop-logo rendering, behaviour on undersized terminals,
/// and the clean-exit path.
/// </summary>
[TestClass]
public sealed class DesklogoSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Stellt sicher, dass <see cref="DesklogoApp"/> ohne Ausnahme gestartet
    /// und sauber beendet werden kann.
    ///
    /// Asserts that <see cref="DesklogoApp"/> can be started and exited cleanly without exception.
    /// </summary>
    [TestMethod]
    public void Desklogo_StartsAndExitsCleanly()
    {
        // Headless-Anwendung erstellen und ausführen — das ist die In-Process-Smoke-Seam.
        // Create and run the headless application — this is the in-process smoke seam.
        DesklogoApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="DesklogoDesktop"/> ein nicht-leeres Logo-Muster enthält.
    ///
    /// Asserts that <see cref="DesklogoDesktop"/> contains a non-empty logo pattern.
    /// </summary>
    [TestMethod]
    public void Desklogo_DesktopHasLogoPattern()
    {
        // Im Headless-Modus wird Desktop durch DesklogoDesktop ersetzt.
        // In headless mode the desktop is replaced by DesklogoDesktop.
        DesklogoApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        // Nach Run() ist Desktop null (ShutDown hat es freigegeben).
        // After Run() Desktop is null (ShutDown released it).
        // Stattdessen prüfen wir über eine frische Instanz (nicht Run()) die Logo-Eigenschaft.
        // Instead check the logo property via a fresh instance (without Run()).
        DesklogoApp freshApp = new(DefaultBounds(), headless: true);
        DesklogoDesktop? desktop = freshApp.Desktop as DesklogoDesktop;

        AssertNotNull(desktop, "DesklogoDesktop");
        AssertTrue(desktop!.LogoLines.Length > 0, "LogoLines muss Zeilen enthalten / LogoLines must contain lines");
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="DesklogoApp"/> bei einer sehr kleinen Terminalgröße
    /// (16×5 Zeichen) ohne Ausnahme startet und sauber beendet wird.
    /// Dies deckt das dokumentierte Clipping-Verhalten ab.
    ///
    /// Asserts that <see cref="DesklogoApp"/> starts and exits cleanly on a very small terminal
    /// (16×5 characters). This covers the documented clipping behaviour.
    /// </summary>
    [TestMethod]
    public void Desklogo_UndersizedTerminal_NoException()
    {
        // Sehr kleines Terminal: 16 × 5 Zeichen / Very small terminal: 16 × 5 characters
        DesklogoApp app = new(DefaultBounds(width: 16, height: 5), headless: true);

        AssertSmokeRunCompletes(() => app.Run());
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="DesklogoApp"/> im Headless-Modus sauber beendet wird —
    /// ohne manuelle Eingabe, hängende Zustände oder erzwungene Prozessbeendigung.
    ///
    /// Asserts that <see cref="DesklogoApp"/> in headless mode exits cleanly —
    /// without manual input, hanging state, or forced process termination.
    /// </summary>
    [TestMethod]
    public void Desklogo_CleanExit_NoHang()
    {
        // Run() muss sich innerhalb des Test-Timeouts beenden.
        // Run() must complete within the test timeout.
        DesklogoApp app = new(DefaultBounds(), headless: true);
        bool completed = false;

        AssertSmokeRunCompletes(() =>
        {
            app.Run();
            completed = true;
        });

        AssertTrue(completed, "Run() muss vor dem Test-Timeout zurückkehren / Run() must return before test timeout");
    }
}
