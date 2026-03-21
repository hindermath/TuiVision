// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TInputLine"/> und die Editierlogik.
///
/// Tests for <see cref="TInputLine"/> and its editing logic.
/// </summary>
[TestClass]
public sealed class TInputLineTests
{
    /// <summary>
    /// Prüft Text-Eingabe, Cursorbewegung, Insert/Overwrite und Löschen.
    ///
    /// Verifies text entry, cursor movement, insert/overwrite, and deletion.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_SupportsEditingAndCursorMovement()
    {
        TInputLine inputLine = new(new TRect(0, 0, 6, 1), 6);

        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'B'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'C'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4B));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x52));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'X'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x53));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x0E));

        Assert.AreEqual("AB", inputLine.Data);
        Assert.AreEqual(2, inputLine.CurPos);
        Assert.IsFalse(inputLine.InsertMode);
    }

    /// <summary>
    /// Prüft horizontales Scrolling bei Eingaben über die sichtbare Breite hinaus.
    ///
    /// Verifies horizontal scrolling when the input exceeds the visible width.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_LongInputAdvancesViewport()
    {
        TInputLine inputLine = new(new TRect(0, 0, 4, 1), 10);
        TGroup owner = ControlTestContext.AttachToOwner(inputLine, new TRect(0, 0, 8, 3));

        foreach (char ch in "ABCDE")
        {
            inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ch));
        }

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual(2, inputLine.FirstPos);
        ControlBufferAssert.AssertRowEquals(buffer, 0, "CDE     ");
    }

    /// <summary>
    /// Prüft den Randfall <c>MaxLen = 0</c>.
    ///
    /// Verifies the <c>MaxLen = 0</c> boundary case.
    /// </summary>
    [TestMethod]
    public void TInputLine_HandleEvent_MaxLenZeroRejectsInput()
    {
        TInputLine inputLine = new(new TRect(0, 0, 5, 1), 0);

        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        inputLine.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4D));

        Assert.AreEqual(string.Empty, inputLine.Data);
        Assert.AreEqual(0, inputLine.CurPos);
        Assert.AreEqual(0, inputLine.FirstPos);
    }
}
