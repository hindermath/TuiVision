// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Gemeinsame Basisinfrastruktur für Wave-1-Beispiel-Smoke-Tests.
/// Stellt Hilfsmethoden für Start-, Verhaltens- und Beendigungsassertionen bereit.
///
/// Shared base infrastructure for Wave 1 example smoke tests.
/// Provides helper methods for launch, behaviour, and clean-exit assertions.
/// </summary>
public abstract class ExampleTestBase
{
    /// <summary>
    /// Erstellt Standard-Anwendungsgrenzen für Headless-Smoke-Tests (80 × 25 Zeichen).
    ///
    /// Creates default application bounds for headless smoke tests (80 × 25 characters).
    /// </summary>
    /// <param name="width">Breite in Zeichen (Standard: 80). / Width in characters (default: 80).</param>
    /// <param name="height">Höhe in Zeichen (Standard: 25). / Height in characters (default: 25).</param>
    /// <returns>
    /// Ein <see cref="TRect"/> mit Ursprung (0, 0) und den angegebenen Abmessungen.
    /// A <see cref="TRect"/> with origin (0, 0) and the specified dimensions.
    /// </returns>
    protected static TRect DefaultBounds(int width = 80, int height = 25) =>
        new(0, 0, width, height);

    /// <summary>
    /// Stellt sicher, dass eine Beispielanwendung ohne Ausnahme startet und sich sauber beendet.
    /// Die Ausführungsaktion muss die Anwendung vollständig starten und beenden —
    /// typischerweise durch Aufruf von <c>app.Run()</c> im Headless-Modus.
    ///
    /// Asserts that an example application starts and exits cleanly without throwing an exception.
    /// The run action must fully start and shut down the application —
    /// typically by calling <c>app.Run()</c> in headless mode.
    /// </summary>
    /// <param name="runAction">
    /// Die auszuführende Aktion, die den vollständigen Start- und Beendigungszyklus des Beispiels abdeckt.
    /// The action that covers the complete start and shutdown cycle of the example.
    /// </param>
    protected static void AssertSmokeRunCompletes(Action runAction)
    {
        // Sicherstellen, dass kein unbehandelter Fehler den Smoke-Pfad unterbricht.
        // Ensure no unhandled error interrupts the smoke path.
        Exception? caught = null;
        try
        {
            runAction();
        }
        catch (Exception ex)
        {
            caught = ex;
        }

        Assert.IsNull(
            caught,
            $"Smoke-Ausführung hat eine unerwartete Ausnahme ausgelöst: {caught?.GetType().Name} — {caught?.Message}\n" +
            $"Smoke run threw an unexpected exception: {caught?.GetType().Name} — {caught?.Message}");
    }

    /// <summary>
    /// Stellt sicher, dass ein Objekt nicht <c>null</c> ist.
    /// Schlägt mit einer bilingualen Meldung fehl.
    ///
    /// Asserts that an object is not <c>null</c>.
    /// Fails with a bilingual message.
    /// </summary>
    /// <typeparam name="T">Der Typ des zu prüfenden Objekts. / The type of the object being checked.</typeparam>
    /// <param name="obj">Das zu prüfende Objekt. / The object to check.</param>
    /// <param name="description">Beschreibung für die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertNotNull<T>(T? obj, string description) where T : class
    {
        Assert.IsNotNull(
            obj,
            $"{description} darf nicht null sein. / {description} must not be null.");
    }

    /// <summary>
    /// Stellt sicher, dass eine Bedingung <c>true</c> ist.
    /// Schlägt mit einer bilingualen Meldung fehl.
    ///
    /// Asserts that a condition is <c>true</c>.
    /// Fails with a bilingual message.
    /// </summary>
    /// <param name="condition">Die zu prüfende Bedingung. / The condition to check.</param>
    /// <param name="description">Beschreibung für die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertTrue(bool condition, string description)
    {
        Assert.IsTrue(
            condition,
            $"{description} — Bedingung ist falsch. / {description} — condition is false.");
    }

    /// <summary>
    /// Stellt sicher, dass zwei Werte gleich sind.
    /// Schlägt mit einer bilingualen Meldung fehl.
    ///
    /// Asserts that two values are equal.
    /// Fails with a bilingual message.
    /// </summary>
    /// <typeparam name="T">Der Typ der zu vergleichenden Werte. / The type of the values being compared.</typeparam>
    /// <param name="expected">Der erwartete Wert. / The expected value.</param>
    /// <param name="actual">Der tatsächliche Wert. / The actual value.</param>
    /// <param name="description">Beschreibung für die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertEqual<T>(T expected, T actual, string description)
    {
        Assert.AreEqual(
            expected,
            actual,
            $"{description}: Erwartet '{expected}', erhalten '{actual}'. / " +
            $"{description}: expected '{expected}', got '{actual}'.");
    }
}
