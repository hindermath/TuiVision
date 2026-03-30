// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TScrollBar"/>: Orientierung, Parameter und Clamping.
///
/// Tests for <see cref="TScrollBar"/>: orientation, parameters, and clamping.
/// </summary>
[TestClass]
public sealed class TScrollBarTests
{
    /// <summary>
    /// Prüft, dass die Orientierung aus dem Konstruktor übernommen wird.
    ///
    /// Verifies that orientation is preserved from the constructor.
    /// </summary>
    [TestMethod]
    public void TScrollBar_Constructor_StoresOrientation()
    {
        TScrollBar horizontal = new(new TRect(0, 0, 5, 1), horizontal: true);
        TScrollBar vertical = new(new TRect(0, 0, 1, 5));

        Assert.IsTrue(horizontal.Horizontal);
        Assert.IsFalse(vertical.Horizontal);
    }

    /// <summary>
    /// Prüft, dass <see cref="TScrollBar.ScrollTo"/> auf das gültige Intervall klemmt.
    ///
    /// Verifies that <see cref="TScrollBar.ScrollTo"/> clamps to the valid range.
    /// </summary>
    [TestMethod]
    public void TScrollBar_ScrollTo_ClampsToBounds()
    {
        TScrollBar scrollBar = new(new TRect(0, 0, 1, 5));
        scrollBar.SetLimit(10);

        scrollBar.ScrollTo(-5);
        Assert.AreEqual(0, scrollBar.Value);

        scrollBar.ScrollTo(25);
        Assert.AreEqual(10, scrollBar.Value);
    }

    /// <summary>
    /// Prüft, dass <see cref="TScrollBar.SetLimit"/> den aktuellen Wert klemmt.
    ///
    /// Verifies that <see cref="TScrollBar.SetLimit"/> clamps the current value.
    /// </summary>
    [TestMethod]
    public void TScrollBar_SetLimit_ClampsCurrentValue()
    {
        TScrollBar scrollBar = new(new TRect(0, 0, 1, 5));
        scrollBar.SetLimit(8);
        scrollBar.ScrollTo(8);

        scrollBar.SetLimit(3);

        Assert.AreEqual(3, scrollBar.Max);
        Assert.AreEqual(3, scrollBar.Value);
    }

    /// <summary>
    /// Prüft, dass <see cref="TScrollBar.SetParams"/> Werte und Schrittweiten übernimmt.
    ///
    /// Verifies that <see cref="TScrollBar.SetParams"/> stores values and step sizes.
    /// </summary>
    [TestMethod]
    public void TScrollBar_SetParams_StoresConfiguration()
    {
        TScrollBar scrollBar = new(new TRect(0, 0, 1, 5));

        scrollBar.SetParams(3, 0, 12, 4, 2);

        Assert.AreEqual(3, scrollBar.Value);
        Assert.AreEqual(12, scrollBar.Max);
        Assert.AreEqual(4, scrollBar.PgStep);
        Assert.AreEqual(2, scrollBar.ArStep);
    }

    /// <summary>
    /// Prüft, dass SetLimit(0) den Wert auf 0 klemmt.
    ///
    /// Verifies that SetLimit(0) clamps the value to 0.
    /// </summary>
    [TestMethod]
    public void TScrollBar_SetLimit_ZeroMax_ValueClamped()
    {
        TScrollBar scrollBar = new(new TRect(0, 0, 1, 5));

        scrollBar.SetLimit(0);

        Assert.AreEqual(0, scrollBar.Value);
    }

    /// <summary>
    /// Prüft, dass SetParams einen negativen Min-Wert als 0 behandelt.
    ///
    /// Verifies that SetParams treats a negative min value as 0.
    /// </summary>
    [TestMethod]
    public void TScrollBar_SetParams_NegativeMin_TreatedAsZero()
    {
        TScrollBar scrollBar = new(new TRect(0, 0, 1, 5));

        scrollBar.SetParams(0, -5, 10, 2, 1);

        Assert.AreEqual(10, scrollBar.Max);
    }
}
