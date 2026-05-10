// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Examples.Clipboard;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer Clipboard-Interaktionen.
///
/// Smoke tests for clipboard interactions.
/// </summary>
[TestClass]
public sealed class ClipboardSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Copy, Cut, Paste und Fallback ueber die Anwendungsschleife.
    ///
    /// Verifies copy, cut, paste, and fallback through the application loop.
    /// </summary>
    [TestMethod]
    public void Clipboard_AppLoop_Dispatches_Copy_Cut_Paste_And_Unavailable_Feedback()
    {
        ClipboardApp app = new(DefaultBounds(), headless: true);
        RecordDirectHelperUsage(DirectHelperUsage.SetupOnly);
        app.SetInput("wave2");
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            ClipboardApp.CmCopy,
            ClipboardApp.CmCut,
            ClipboardApp.CmPaste,
            ClipboardApp.CmUnavailable).Events);

        AssertSmokeRunCompletes(() => app.Run());

        string history = string.Join('\n', app.VisibleHistory);
        AssertVisibleContainsFromAppLoop(history, "copied 'wave2'", "Clipboard app-loop copy");
        AssertVisibleContainsFromAppLoop(history, "cut 'wave2'", "Clipboard app-loop cut");
        AssertVisibleContainsFromAppLoop(history, "pasted 'wave2'", "Clipboard app-loop paste");
        AssertVisibleContainsFromAppLoop(history, "isolated fallback", "Clipboard app-loop unavailable");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>
    /// Prueft Copy, Cut, Paste, Eingabezustand und isolierten Clipboard-Fallback.
    ///
    /// Verifies copy, cut, paste, input state, and isolated clipboard fallback.
    /// </summary>
    [TestMethod]
    public void Clipboard_Copy_Cut_Paste_And_IsolatedClipboard_AreVisible()
    {
        RecordDirectHelperUsage(DirectHelperUsage.SupplementalAssertion);
        ClipboardApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        app.SetInput("wave2");
        string copy = app.CopyInput();
        string cut = app.CutInput();
        AssertEqual(string.Empty, app.InputText, "Clipboard input after cut");
        string paste = app.PasteIntoInput();
        string fallback = app.SimulateUnavailableClipboard();

        AssertVisibleContains(copy, "copied", "Clipboard copy state");
        AssertVisibleContains(cut, "cut", "Clipboard cut state");
        AssertVisibleContains(paste, "wave2", "Clipboard paste state");
        AssertVisibleContains(fallback, "isolated", "Clipboard isolated fallback");
    }
}
