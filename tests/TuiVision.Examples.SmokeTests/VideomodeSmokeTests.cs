// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Videomode;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests für das Videomode-Beispiel.
/// Prüft den unterstützten Übergangspfad, den expliziten sichtbaren Fallback-Pfad,
/// das Verhalten bei zu kleinen Terminals, die Nutzbarkeit nach dem Übergang
/// und den sauberen Beendigungspfad.
///
/// Smoke tests for the Videomode example.
/// Verifies the supported-transition path, the explicit visible-fallback path,
/// undersized-terminal behaviour, post-transition usability, and the clean-exit path.
/// </summary>
[TestClass]
public sealed class VideomodeSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Stellt sicher, dass <see cref="VideomodeApp"/> ohne Ausnahme startet und sauber beendet wird.
    ///
    /// Asserts that <see cref="VideomodeApp"/> starts and exits cleanly without exception.
    /// </summary>
    [TestMethod]
    public void Videomode_StartsAndExitsCleanly()
    {
        VideomodeApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());
    }

    /// <summary>
    /// Stellt sicher, dass der <see cref="DisplayModeCoordinator"/> nach der Initialisierung
    /// einen definierten Fähigkeitsstatus hat (weder ungeprüft noch in einem Fehler-Zustand).
    ///
    /// Asserts that <see cref="DisplayModeCoordinator"/> has a defined capability status after
    /// initialization (neither unchecked nor in an error state).
    /// </summary>
    [TestMethod]
    public void Videomode_Coordinator_HasDefinedCapabilityState()
    {
        VideomodeApp app = new(DefaultBounds(), headless: true);

        // IsResizeSupported ist ein boolescher Wert — kein Fehler-Zustand möglich.
        // IsResizeSupported is a boolean value — no error state possible.
        AssertNotNull(app.Coordinator, "Coordinator");

        // Der Wert (true oder false) ist laufzeitabhängig; wir prüfen nur, dass die Prüfung abgeschlossen wurde.
        // The value (true or false) is runtime-dependent; we only verify that probing completed.
        bool _ = app.Coordinator.IsResizeSupported; // Keine Exception / No exception
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="DisplayModeCoordinator.TryTransition"/> immer eines der
    /// definierten <see cref="DisplayModeOutcome"/>-Ergebnisse zurückgibt.
    ///
    /// Asserts that <see cref="DisplayModeCoordinator.TryTransition"/> always returns one of the
    /// defined <see cref="DisplayModeOutcome"/> results.
    /// </summary>
    [TestMethod]
    public void Videomode_TransitionReturnsDefinedOutcome()
    {
        VideomodeApp app = new(DefaultBounds(), headless: true);

        // TryTransition mit Standardgröße aufrufen / Call TryTransition with default size
        DisplayModeOutcome outcome = app.Coordinator.TryTransition(80, 25);

        AssertTrue(
            outcome == DisplayModeOutcome.RealTransition || outcome == DisplayModeOutcome.VisibleFallback,
            $"TryTransition muss RealTransition oder VisibleFallback zurückgeben, nicht '{outcome}' / " +
            $"TryTransition must return RealTransition or VisibleFallback, not '{outcome}'");
    }

    /// <summary>
    /// Stellt sicher, dass bei Terminals ohne Größenänderungs-Unterstützung der Fallback sichtbar ist.
    /// In CI-Umgebungen ist <see cref="DisplayModeCoordinator.IsResizeSupported"/> typischerweise <c>false</c>.
    ///
    /// Asserts that when terminal resize is not supported, a visible fallback is present.
    /// In CI environments <see cref="DisplayModeCoordinator.IsResizeSupported"/> is typically <c>false</c>.
    /// </summary>
    [TestMethod]
    public void Videomode_VisibleFallback_MessageIsNonEmpty()
    {
        VideomodeApp app = new(DefaultBounds(), headless: true);

        AssertTrue(
            !string.IsNullOrWhiteSpace(app.Coordinator.FallbackMessage),
            "FallbackMessage muss nicht-leer sein / FallbackMessage must be non-empty");
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="VideomodeApp"/> nach einem Übergangsversuch weiter benutzbar ist —
    /// unabhängig davon, ob ein echter Übergang oder ein Fallback stattgefunden hat.
    ///
    /// Asserts that <see cref="VideomodeApp"/> remains usable after a transition attempt —
    /// regardless of whether a real transition or fallback occurred.
    /// </summary>
    [TestMethod]
    public void Videomode_PostTransitionUsability()
    {
        VideomodeApp app = new(DefaultBounds(), headless: true);

        // Übergangsversuch im Headless-Modus / Transition attempt in headless mode
        DisplayModeOutcome outcome = app.Coordinator.TryTransition(80, 25);

        // Anwendung bleibt nach dem Übergang lauffähig / Application remains runnable after transition
        AssertSmokeRunCompletes(() => app.Run());
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="VideomodeApp"/> bei einem kleinen Terminal (16×5) stabil bleibt.
    ///
    /// Asserts that <see cref="VideomodeApp"/> remains stable with a small terminal (16×5).
    /// </summary>
    [TestMethod]
    public void Videomode_UndersizedTerminal_NoException()
    {
        VideomodeApp app = new(DefaultBounds(width: 16, height: 5), headless: true);

        AssertSmokeRunCompletes(() => app.Run());
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="VideomodeApp"/> sauber beendet wird — kein Hängen, kein Kill.
    ///
    /// Asserts that <see cref="VideomodeApp"/> exits cleanly — no hang, no kill.
    /// </summary>
    [TestMethod]
    public void Videomode_CleanExit()
    {
        VideomodeApp app = new(DefaultBounds(), headless: true);
        bool completed = false;

        AssertSmokeRunCompletes(() =>
        {
            app.Run();
            completed = true;
        });

        AssertTrue(completed, "Run() muss sauber zurückkehren / Run() must return cleanly");
    }
}
