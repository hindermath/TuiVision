// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="ManagedClipboard"/>: Zustand, HasText und Roundtrip-Semantik.
///
/// Tests for <see cref="ManagedClipboard"/>: state, HasText, and roundtrip semantics.
/// </summary>
[TestClass]
public sealed class TManagedClipboardTests
{
    /// <summary>
    /// Leert die Zwischenablage vor jedem Test.
    ///
    /// Clears the clipboard before each test.
    /// </summary>
    [TestInitialize]
    public void TestInitialize() => ManagedClipboard.Clear();

    /// <summary>
    /// Prüft, dass GetText initial null zurückgibt.
    ///
    /// Verifies that GetText initially returns null.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_GetText_Initially_ReturnsNull()
    {
        Assert.IsNull(ManagedClipboard.GetText());
    }

    /// <summary>
    /// Prüft, dass HasText initial false zurückgibt.
    ///
    /// Verifies that HasText initially returns false.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_HasText_Initially_ReturnsFalse()
    {
        Assert.IsFalse(ManagedClipboard.HasText);
    }

    /// <summary>
    /// Prüft, dass SetText den Text korrekt speichert.
    ///
    /// Verifies that SetText stores the text correctly.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_SetText_StoresText()
    {
        ManagedClipboard.SetText("hello");

        Assert.AreEqual("hello", ManagedClipboard.GetText());
    }

    /// <summary>
    /// Prüft, dass HasText nach SetText true zurückgibt.
    ///
    /// Verifies that HasText returns true after SetText.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_HasText_AfterSetText_ReturnsTrue()
    {
        ManagedClipboard.SetText("hi");

        Assert.IsTrue(ManagedClipboard.HasText);
    }

    /// <summary>
    /// Prüft, dass Clear den Inhalt entfernt.
    ///
    /// Verifies that Clear removes the content.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_Clear_RemovesText()
    {
        ManagedClipboard.SetText("hi");

        ManagedClipboard.Clear();

        Assert.IsNull(ManagedClipboard.GetText());
    }

    /// <summary>
    /// Prüft, dass HasText nach Clear false zurückgibt.
    ///
    /// Verifies that HasText returns false after Clear.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_HasText_AfterClear_ReturnsFalse()
    {
        ManagedClipboard.SetText("hi");

        ManagedClipboard.Clear();

        Assert.IsFalse(ManagedClipboard.HasText);
    }

    /// <summary>
    /// Prüft, dass SetText bestehenden Text überschreibt.
    ///
    /// Verifies that SetText replaces existing text.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_SetText_ReplacesExistingText()
    {
        ManagedClipboard.SetText("a");

        ManagedClipboard.SetText("b");

        Assert.AreEqual("b", ManagedClipboard.GetText());
    }
}
