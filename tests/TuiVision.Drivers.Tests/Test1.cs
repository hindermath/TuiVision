using TuiVision.Drivers.Console;

namespace TuiVision.Drivers.Tests;

[TestClass]
public sealed class ConsoleDriverTests
{
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

    [TestMethod]
    public void Buffer_RejectsNonPositiveSize()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TConsoleBuffer(0, 1));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TConsoleBuffer(1, 0));
    }

    private sealed class CapturingPresenter : IConsolePresenter
    {
        public TConsoleBuffer? LastFrame { get; private set; }

        public void Present(TConsoleBuffer frame) => LastFrame = frame;
    }
}
