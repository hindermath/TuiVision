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
    /// Prueft Copy, Cut, Paste, Eingabezustand und isolierten Clipboard-Fallback.
    ///
    /// Verifies copy, cut, paste, input state, and isolated clipboard fallback.
    /// </summary>
    [TestMethod]
    public void Clipboard_Copy_Cut_Paste_And_IsolatedClipboard_AreVisible()
    {
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
