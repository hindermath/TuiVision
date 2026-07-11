// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Core;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Klassifiziert direkte Hilfsmethoden in interaktiven Smoke-Tests.
/// Primaere Wave-2-Beweise muessen ueber die Anwendungsschleife laufen.
///
/// Classifies direct helper methods in interactive smoke tests.
/// Primary Wave 2 proof must run through the application loop.
/// </summary>
public enum DirectHelperUsage
{
    /// <summary>
    /// Keine direkte Hilfsmethode wurde fuer den Beweis verwendet.
    /// No direct helper was used for the proof.
    /// </summary>
    None,

    /// <summary>
    /// Direkte Hilfsmethoden bereiten nur den Zustand vor.
    /// Direct helpers only prepare state.
    /// </summary>
    SetupOnly,

    /// <summary>
    /// Direkte Hilfsmethoden sind Teil des primaeren Beweises, weil sie echte
    /// Beispiel- oder Anwendungslogik ueber eine oeffentliche Flaeche ausfuehren.
    ///
    /// Direct helpers are part of the primary proof because they execute real
    /// example or application logic through a public surface.
    /// </summary>
    PrimaryProof,

    /// <summary>
    /// Direkte Hilfsmethoden liefern nur ergaenzende Assertionen.
    /// Direct helpers provide supplemental proof assertions only.
    /// </summary>
    SupplementalProof,

    /// <summary>
    /// Alte Bezeichnung fuer <see cref="SupplementalProof"/>.
    /// Bestehende Wave-2-Tests behalten damit ihre Bedeutung.
    ///
    /// Old name for <see cref="SupplementalProof"/>.
    /// Existing Wave 2 tests keep their meaning.
    /// </summary>
    SupplementalAssertion = SupplementalProof,

    /// <summary>
    /// Direkte Hilfsmethoden sind nur temporaer akzeptiert und muessen spaeter
    /// durch sichtbare Runtime-Beweise ersetzt werden.
    ///
    /// Direct helpers are accepted only temporarily and must later be replaced
    /// by visible runtime proof.
    /// </summary>
    LegacyOrTemporary
}

/// <summary>
/// Gemeinsame Basisinfrastruktur für Wave-1- und Wave-2-Beispiel-Smoke-Tests.
/// Stellt Hilfsmethoden für Start-, Verhaltens-, Sichtbarkeits- und
/// Beendigungsassertionen bereit.
///
/// Shared base infrastructure for Wave 1 and Wave 2 example smoke tests.
/// Provides helper methods for launch, behaviour, visibility, and clean-exit assertions.
/// </summary>
public abstract class ExampleTestBase
{
    private DirectHelperUsage _directHelperUsage = DirectHelperUsage.None;
    private bool _primaryAssertionUsedAppLoop;

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
    protected void AssertSmokeRunCompletes(Action runAction)
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
    /// Merkt, ob ein Test direkte Hilfsmethoden nur kontrolliert einsetzen darf.
    ///
    /// Records whether a test uses direct helper methods only in a controlled role.
    /// </summary>
    /// <param name="usage">Die Klassifikation. / The classification.</param>
    protected void RecordDirectHelperUsage(DirectHelperUsage usage) => _directHelperUsage = usage;

    /// <summary>
    /// Merkt, dass die primaere sichtbare Assertion nach einem App-Loop-Pfad erfolgt ist.
    ///
    /// Records that the primary visible assertion followed an app-loop path.
    /// </summary>
    protected void RecordPrimaryAssertionUsedAppLoop() => _primaryAssertionUsedAppLoop = true;

    /// <summary>
    /// Prueft, dass ein Test seinen primaeren Beweis ueber die App-Schleife gefuehrt hat.
    ///
    /// Asserts that a test used the app loop for its primary proof.
    /// </summary>
    protected void AssertPrimaryAssertionUsedAppLoop()
    {
        Assert.IsTrue(
            _primaryAssertionUsedAppLoop,
            "Primaere Assertion muss ueber die Anwendungsschleife laufen. / Primary assertion must use the application loop.");
    }

    /// <summary>
    /// Prueft die aufgezeichnete Klassifikation direkter Hilfsmethoden.
    ///
    /// Asserts the recorded classification for direct helpers.
    /// </summary>
    /// <param name="expected">Die erwartete Klassifikation. / The expected classification.</param>
    protected void AssertDirectHelperUsage(DirectHelperUsage expected)
    {
        Assert.AreEqual(
            expected,
            _directHelperUsage,
            $"Direkte Hilfsmethoden sind als {expected} erwartet. / Direct helper usage is expected as {expected}.");
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

    /// <summary>
    /// Stellt sicher, dass ein sichtbarer Textzustand eine erwartete Zeichenfolge enthaelt.
    /// Wave-2-Beispiele muessen damit einen beispielspezifischen, textorientierten
    /// Zustand nachweisen und duerfen nicht nur Start plus Ende pruefen.
    ///
    /// Asserts that a visible text state contains an expected string.
    /// Wave 2 examples must use this pattern to prove example-specific,
    /// text-first state and must not only verify startup plus exit.
    /// </summary>
    /// <param name="visibleText">Der sichtbare Textzustand. / The visible text state.</param>
    /// <param name="expectedFragment">Das erwartete Fragment. / The expected fragment.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertVisibleContains(string visibleText, string expectedFragment, string description)
    {
        Assert.IsTrue(
            visibleText.Contains(expectedFragment, StringComparison.Ordinal),
            $"{description}: sichtbarer Text enthaelt '{expectedFragment}' nicht. / " +
            $"{description}: visible text does not contain '{expectedFragment}'. Actual: {visibleText}");
    }

    /// <summary>
    /// Prueft sichtbaren Text und markiert ihn als primaeren App-Loop-Beweis.
    ///
    /// Checks visible text and marks it as primary app-loop proof.
    /// </summary>
    /// <param name="visibleText">Der sichtbare Textzustand. / The visible text state.</param>
    /// <param name="expectedFragment">Das erwartete Fragment. / The expected fragment.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected void AssertVisibleContainsFromAppLoop(string visibleText, string expectedFragment, string description)
    {
        AssertVisibleContains(visibleText, expectedFragment, description);
        RecordPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft den aufgezeichneten View-Baum-Zieltyp eines primaeren sichtbaren Wave-2-Beweises.
    ///
    /// Asserts the recorded view-tree target type of a primary visible Wave 2 proof.
    /// </summary>
    /// <param name="actualKind">Der beobachtete View-Typ. / The observed view type.</param>
    /// <param name="expectedKind">Der erwartete View-Typ. / The expected view type.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected void AssertViewTreeProofFromAppLoop(string actualKind, string expectedKind, string description)
    {
        AssertEqual(expectedKind, actualKind, description);
        RecordPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft, dass ein Renderpuffer einen erwarteten, control-spezifischen Text enthaelt.
    ///
    /// Asserts that a render buffer contains expected control-specific text.
    /// </summary>
    /// <param name="buffer">Der Renderpuffer. / The render buffer.</param>
    /// <param name="expectedFragment">Das erwartete Fragment. / The expected fragment.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected void AssertRenderedContainsFromAppLoop(TConsoleBuffer buffer, string expectedFragment, string description)
    {
        AssertRenderedContains(buffer, expectedFragment, description);
        RecordPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft, dass ein Renderpuffer einen erwarteten, control-spezifischen Text enthaelt.
    ///
    /// Asserts that a render buffer contains expected control-specific text.
    /// </summary>
    /// <param name="buffer">Der Renderpuffer. / The render buffer.</param>
    /// <param name="expectedFragment">Das erwartete Fragment. / The expected fragment.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertRenderedContains(TConsoleBuffer buffer, string expectedFragment, string description)
    {
        string text = BufferToText(buffer);
        Assert.IsTrue(
            text.Contains(expectedFragment, StringComparison.Ordinal),
            $"{description}: Renderpuffer enthaelt '{expectedFragment}' nicht. / " +
            $"{description}: render buffer does not contain '{expectedFragment}'. Actual: {text}");
    }

    /// <summary>
    /// Prueft eine stabile Region im Renderpuffer.
    ///
    /// Asserts a stable region in the render buffer.
    /// </summary>
    /// <param name="buffer">Der Renderpuffer. / The render buffer.</param>
    /// <param name="region">Die zu pruefende Region. / The region to check.</param>
    /// <param name="expectedFragment">Das erwartete Fragment. / The expected fragment.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected void AssertRenderedRegionContainsFromAppLoop(
        TConsoleBuffer buffer,
        TRect region,
        string expectedFragment,
        string description)
    {
        string text = BufferRegionToText(buffer, region);
        Assert.IsTrue(
            text.Contains(expectedFragment, StringComparison.Ordinal),
            $"{description}: Renderregion enthaelt '{expectedFragment}' nicht. / " +
            $"{description}: render region does not contain '{expectedFragment}'. Actual: {text}");
        RecordPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Wandelt einen Renderpuffer in zeilenweisen Text fuer stabile Smoke-Assertions um.
    ///
    /// Converts a render buffer into line-based text for stable smoke assertions.
    /// </summary>
    /// <param name="buffer">Der Renderpuffer. / The render buffer.</param>
    /// <returns>Der zeilenweise Text. / The line-based text.</returns>
    protected static string BufferToText(TConsoleBuffer buffer)
    {
        StringBuilder builder = new();
        for (int y = 0; y < buffer.Height; y++)
        {
            if (y > 0)
            {
                builder.Append('\n');
            }

            for (int x = 0; x < buffer.Width; x++)
            {
                char glyph = buffer.GetCell(x, y).Glyph;
                builder.Append(glyph == '\0' ? ' ' : glyph);
            }
        }

        return builder.ToString();
    }

    /// <summary>
    /// Wandelt eine stabile Renderpufferregion in zeilenweisen Text um.
    ///
    /// Converts a stable render-buffer region into line-based text.
    /// </summary>
    /// <param name="buffer">Der Renderpuffer. / The render buffer.</param>
    /// <param name="region">Die Region. / The region.</param>
    /// <returns>Der Regionstext. / The region text.</returns>
    protected static string BufferRegionToText(TConsoleBuffer buffer, TRect region)
    {
        // Clipping macht die Assertion bei kleinen Terminals stabil, beweist aber nur den sichtbaren Schnittbereich.
        // Clipping keeps the assertion stable on small terminals but proves only the visible intersection.
        int left = Math.Clamp(region.A.X, 0, buffer.Width);
        int top = Math.Clamp(region.A.Y, 0, buffer.Height);
        int right = Math.Clamp(region.B.X, left, buffer.Width);
        int bottom = Math.Clamp(region.B.Y, top, buffer.Height);

        StringBuilder builder = new();
        for (int y = top; y < bottom; y++)
        {
            if (y > top)
            {
                builder.Append('\n');
            }

            for (int x = left; x < right; x++)
            {
                char glyph = buffer.GetCell(x, y).Glyph;
                builder.Append(glyph == '\0' ? ' ' : glyph);
            }
        }

        return builder.ToString();
    }

    /// <summary>
    /// Stellt sicher, dass ein Grenzwertzustand exakt den erwarteten Wert besitzt.
    /// Dies wird fuer Listen, Kombinationsfelder, Fortschritt, dynamischen Text und
    /// scrollbare Dialoge verwendet.
    ///
    /// Asserts that a boundary state has the exact expected value.
    /// This is used for lists, combo boxes, progress, dynamic text, and scrollable dialogs.
    /// </summary>
    /// <typeparam name="T">Der Werttyp. / The value type.</typeparam>
    /// <param name="expected">Der erwartete Wert. / The expected value.</param>
    /// <param name="actual">Der beobachtete Wert. / The observed value.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertBoundary<T>(T expected, T actual, string description) =>
        AssertEqual(expected, actual, description);

    /// <summary>
    /// Stellt sicher, dass die textorientierte Ausgabe nicht leer ist.
    /// Die kanonische Wave-1/Wave-2-Headless-Seam bleibt ein Konstruktorparameter
    /// <c>bool headless</c> plus ein <c>GetEvent()</c>-Override, der Smoke-Tests
    /// deterministisch in-process beendet.
    ///
    /// Asserts that text-first output is not empty.
    /// The canonical Wave 1/Wave 2 headless seam remains a <c>bool headless</c>
    /// constructor parameter plus a <c>GetEvent()</c> override that lets smoke
    /// tests exit deterministically in process.
    /// </summary>
    /// <param name="visibleText">Der sichtbare Textzustand. / The visible text state.</param>
    /// <param name="description">Beschreibung fuer die Fehlermeldung. / Description for the failure message.</param>
    protected static void AssertTextFirstOutput(string visibleText, string description)
    {
        Assert.IsFalse(
            string.IsNullOrWhiteSpace(visibleText),
            $"{description}: textorientierte Ausgabe fehlt. / {description}: text-first output is missing.");
    }
}
