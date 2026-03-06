using TuiVision.Drivers.Console;

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Tests für <see cref="TConsoleBuffer"/> und <see cref="TConsoleDriver"/>:
/// Zeichenoperationen, Pufferverwaltung und das Presenter-Pattern.
///
/// Tests for <see cref="TConsoleBuffer"/> and <see cref="TConsoleDriver"/>:
/// drawing operations, buffer management, and the presenter pattern.
/// </summary>
[TestClass]
public sealed class ConsoleDriverTests
{
    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer.WriteText"/> Zeichen, die links außerhalb
    /// des Puffers beginnen, korrekt abschneidet (Clipping).
    ///
    /// Verifies that <see cref="TConsoleBuffer.WriteText"/> correctly clips characters
    /// that start to the left outside the buffer boundary.
    /// </summary>
    [TestMethod]
    public void Buffer_WriteText_ClipsHorizontally()
    {
        TConsoleBuffer buffer = new(5, 2);
        buffer.Clear(new TConsoleCell('.', ConsoleColor.DarkGray, ConsoleColor.Black));

        buffer.WriteText(-2, 1, "ABCDE".AsSpan(), ConsoleColor.Yellow, ConsoleColor.Blue);

        Assert.AreEqual('C', buffer[0, 1].Glyph);
        Assert.AreEqual('D', buffer[1, 1].Glyph);
        Assert.AreEqual('E', buffer[2, 1].Glyph);
        Assert.AreEqual('.', buffer[3, 1].Glyph);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleDriver.Resize"/> den sichtbaren Schnittbereich des
    /// alten Puffers in den neuen Puffer kopiert und neue Zellen leer lässt.
    ///
    /// Verifies that <see cref="TConsoleDriver.Resize"/> copies the visible intersection
    /// of the old buffer into the new buffer and leaves new cells empty.
    /// </summary>
    [TestMethod]
    public void Driver_Resize_PreservesVisibleIntersection()
    {
        TConsoleDriver driver = new(4, 2);
        TConsoleCell marker = new('K', ConsoleColor.Green, ConsoleColor.Black);
        driver.BackBuffer.SetCell(1, 1, marker);

        driver.Resize(3, 3);

        Assert.AreEqual(3, driver.BackBuffer.Width);
        Assert.AreEqual(3, driver.BackBuffer.Height);
        Assert.AreEqual(marker, driver.BackBuffer.GetCell(1, 1));
        Assert.AreEqual(TConsoleCell.Empty, driver.BackBuffer.GetCell(2, 2));
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleDriver.Present"/> dem Presenter einen Schnappschuss
    /// und nicht den Live-Puffer übergibt, sodass spätere Änderungen am Puffer den Frame
    /// nicht mehr beeinflussen.
    ///
    /// Verifies that <see cref="TConsoleDriver.Present"/> passes a snapshot to the presenter
    /// rather than the live buffer, so that subsequent buffer changes do not affect the frame.
    /// </summary>
    [TestMethod]
    public void Driver_Present_PublishesSnapshotInsteadOfLiveBuffer()
    {
        TConsoleDriver driver = new(2, 1);
        driver.BackBuffer.SetCell(0, 0, new TConsoleCell('A', ConsoleColor.White, ConsoleColor.Black));

        CapturingPresenter presenter = new();
        driver.Present(presenter);
        driver.BackBuffer.SetCell(0, 0, new TConsoleCell('B', ConsoleColor.White, ConsoleColor.Black));

        Assert.IsNotNull(presenter.LastFrame);
        Assert.AreEqual('A', presenter.LastFrame!.GetCell(0, 0).Glyph);
    }

    /// <summary>
    /// Prüft, dass <see cref="TConsoleBuffer"/> eine <see cref="ArgumentOutOfRangeException"/>
    /// auslöst, wenn Breite oder Höhe nicht positiv ist.
    ///
    /// Verifies that <see cref="TConsoleBuffer"/> throws an <see cref="ArgumentOutOfRangeException"/>
    /// when the width or height is not positive.
    /// </summary>
    [TestMethod]
    public void Buffer_RejectsNonPositiveSize()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TConsoleBuffer(0, 1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TConsoleBuffer(1, 0));
    }

    /// <summary>
    /// Test-Presenter, der den zuletzt empfangenen Frame für Assertions speichert.
    ///
    /// Test presenter that stores the last received frame for assertions.
    /// </summary>
    private sealed class CapturingPresenter : IConsolePresenter
    {
        /// <summary>
        /// Der zuletzt empfangene Frame oder <c>null</c>, wenn noch kein Frame übergeben wurde.
        ///
        /// The last received frame, or <c>null</c> if no frame has been passed yet.
        /// </summary>
        public TConsoleBuffer? LastFrame { get; private set; }

        /// <summary>Speichert den Frame für spätere Prüfungen. / Stores the frame for later assertions.</summary>
        /// <param name="frame">Der empfangene Frame. / The received frame.</param>
        public void Present(TConsoleBuffer frame) => LastFrame = frame;
    }
}
