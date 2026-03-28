// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.MsgCls;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests für das MsgCls-Beispiel.
/// Prüft den Start, den Broadcast-Routing-Pfad, das sichtbare Ergebnis,
/// die Stabilität bei wiederholtem Trigger und den sauberen Beendigungspfad.
///
/// Smoke tests for the MsgCls example.
/// Verifies startup, the broadcast routing path, the visible routed outcome,
/// repeated-trigger stability, and the clean-exit path.
/// </summary>
[TestClass]
public sealed class MsgClsSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Stellt sicher, dass <see cref="MsgClsApp"/> ohne Ausnahme startet und sauber beendet wird.
    ///
    /// Asserts that <see cref="MsgClsApp"/> starts and exits cleanly without exception.
    /// </summary>
    [TestMethod]
    public void MsgCls_StartsAndExitsCleanly()
    {
        MsgClsApp app = new(DefaultBounds(), headless: true);

        AssertSmokeRunCompletes(() => app.Run());
    }

    /// <summary>
    /// Stellt sicher, dass das Nachrichtenfenster vorhanden ist und Nachrichten empfangen kann.
    ///
    /// Asserts that the message window is present and can receive messages.
    /// </summary>
    [TestMethod]
    public void MsgCls_MessageWindowExists()
    {
        // Im Headless-Modus erstellt der Konstruktor ein MsgClsWindow und postet sofort eine Testnachricht.
        // In headless mode the constructor creates a MsgClsWindow and immediately posts a test message.
        MsgClsApp app = new(DefaultBounds(), headless: true);

        AssertNotNull(app.MsgWindow, "MsgWindow");
    }

    /// <summary>
    /// Stellt sicher, dass die Headless-Testnachricht korrekt in das Nachrichtenfenster geroutet wurde.
    ///
    /// Asserts that the headless test message was correctly routed into the message window.
    /// </summary>
    [TestMethod]
    public void MsgCls_HeadlessMessageRoutedToWindow()
    {
        // Im Headless-Modus postet der Konstruktor sofort „Testnachricht / Test message".
        // In headless mode the constructor immediately posts "Testnachricht / Test message".
        MsgClsApp app = new(DefaultBounds(), headless: true);

        AssertTrue(
            app.MsgWindow.MessageCount > 0,
            "MsgWindow muss nach Headless-Init mindestens eine Nachricht enthalten / " +
            "MsgWindow must contain at least one message after headless init");
    }

    /// <summary>
    /// Stellt sicher, dass mehrere Nachrichten nacheinander korrekt geroutet werden.
    ///
    /// Asserts that multiple messages are correctly routed in sequence.
    /// </summary>
    [TestMethod]
    public void MsgCls_RepeatedMessageTriggerIsStable()
    {
        MsgClsApp app = new(DefaultBounds(), headless: true);
        int countBefore = app.MsgWindow.MessageCount;

        // Drei weitere Nachrichten posten / Post three more messages
        app.PostMessage("Nachricht 1 / Message 1");
        app.PostMessage("Nachricht 2 / Message 2");
        app.PostMessage("Nachricht 3 / Message 3");

        AssertEqual(
            countBefore + 3,
            app.MsgWindow.MessageCount,
            "Nach drei weiteren Nachrichten muss MessageCount um 3 gestiegen sein / " +
            "MessageCount must have increased by 3 after three more messages");
    }

    /// <summary>
    /// Stellt sicher, dass <see cref="MsgClsApp"/> im Headless-Modus sauber beendet wird.
    ///
    /// Asserts that <see cref="MsgClsApp"/> exits cleanly in headless mode.
    /// </summary>
    [TestMethod]
    public void MsgCls_CleanExit()
    {
        MsgClsApp app = new(DefaultBounds(), headless: true);
        bool completed = false;

        AssertSmokeRunCompletes(() =>
        {
            app.Run();
            completed = true;
        });

        AssertTrue(completed, "Run() muss sauber zurückkehren / Run() must return cleanly");
    }
}
