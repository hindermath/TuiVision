// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TStaticText"/> als nicht-interaktive Textanzeige.
///
/// Tests for <see cref="TStaticText"/> as a non-interactive text display.
/// </summary>
[TestClass]
public sealed class TStaticTextTests
{
    /// <summary>
    /// Prüft, dass der Konstruktor den Text übernimmt und das Control nicht selektierbar macht.
    ///
    /// Verifies that the constructor stores the text and keeps the control non-selectable.
    /// </summary>
    [TestMethod]
    public void TStaticText_Constructor_StoresTextAndRemainsNonSelectable()
    {
        TStaticText text = new(new TRect(0, 0, 5, 2), "Hallo");

        Assert.AreEqual("Hallo", text.Text);
        Assert.IsFalse(text.Options.HasFlag(TViewOptions.Selectable));
    }

    /// <summary>
    /// Prüft, dass mehrzeiliger Text in den Owner-Puffer geschrieben wird.
    ///
    /// Verifies that multi-line text is written into the owner buffer.
    /// </summary>
    [TestMethod]
    public void TStaticText_Draw_WritesMultiLineTextIntoOwnerBuffer()
    {
        TStaticText text = new(new TRect(1, 1, 6, 3), "Hallo\nWelt!");
        TGroup owner = ControlTestContext.AttachToOwner(text, new TRect(0, 0, 10, 5));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertTextAt(buffer, 1, 1, "Hallo");
        ControlBufferAssert.AssertTextAt(buffer, 1, 2, "Welt!");
    }
}
