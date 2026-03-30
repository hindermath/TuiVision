// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TParamText"/>: Formatierung, SetValues, Clipping und Rendering.
///
/// Tests for <see cref="TParamText"/>: formatting, SetValues, clipping, and rendering.
/// </summary>
[TestClass]
public sealed class TParamTextTests
{
    /// <summary>
    /// Prüft, dass Format ohne Werte die Vorlage unverändert zurückgibt.
    ///
    /// Verifies that Format without values returns the template unchanged.
    /// </summary>
    [TestMethod]
    public void TParamText_Format_NoValues_ReturnsTemplate()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("Hello");

        string result = pt.Format();

        Assert.AreEqual("Hello", result);
    }

    /// <summary>
    /// Prüft, dass Format mit Werten korrekt formatiert.
    ///
    /// Verifies that Format with values produces correct output.
    /// </summary>
    [TestMethod]
    public void TParamText_Format_WithValues_FormatsInline()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("Hello {0}");

        string result = pt.Format("World");

        Assert.AreEqual("Hello World", result);
    }

    /// <summary>
    /// Prüft, dass SetValues die gespeicherten Werte aktualisiert (indirekt über Draw).
    ///
    /// Verifies that SetValues updates the stored values (indirectly through Draw).
    /// </summary>
    [TestMethod]
    public void TParamText_SetValues_UpdatesStoredValues()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("Val: {0}", new TRect(0, 0, 10, 1));
        TGroup owner = ControlTestContext.AttachToOwner(pt, new TRect(0, 0, 15, 3));

        pt.SetValues("X");
        owner.Draw();

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);
        ControlBufferAssert.AssertTextAt(buffer, 0, 0, "Val: X");
    }

    /// <summary>
    /// Prüft, dass Draw den formatierten Text in den Puffer rendert.
    ///
    /// Verifies that Draw renders the formatted text into the buffer.
    /// </summary>
    [TestMethod]
    public void TParamText_Draw_FirstRedraw_RendersFormattedText()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("Hi {0}", new TRect(0, 0, 10, 1));
        pt.SetValues("World");
        TGroup owner = ControlTestContext.AttachToOwner(pt, new TRect(0, 0, 15, 3));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        ControlBufferAssert.AssertTextAt(buffer, 0, 0, "Hi World");
    }

    /// <summary>
    /// Prüft, dass Draw langen Text auf die View-Breite clippt.
    ///
    /// Verifies that Draw clips long text to the view width.
    /// </summary>
    [TestMethod]
    public void TParamText_Draw_ClipsToWidth()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("ABCDEFGHIJ", new TRect(0, 0, 5, 1));
        TGroup owner = ControlTestContext.AttachToOwner(pt, new TRect(0, 0, 10, 3));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        // Only first 5 chars should be rendered
        ControlBufferAssert.AssertTextAt(buffer, 0, 0, "ABCDE");
    }

    /// <summary>
    /// Prüft, dass die Template-Eigenschaft den Konstruktorwert enthält.
    ///
    /// Verifies that the Template property contains the constructor value.
    /// </summary>
    [TestMethod]
    public void TParamText_Template_IsStored()
    {
        TParamText pt = ControlsWidgetTestContext.CreateParamText("My Template");

        Assert.AreEqual("My Template", pt.Template);
    }
}
