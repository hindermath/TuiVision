using TuiVision.Compatibility;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Serialization;

namespace TuiVision.Examples.SmokeTests;

[TestClass]
public sealed class ModuleSmokeTests
{
    [TestMethod]
    public void Compatibility_MapsConsoleEnterToTurboVisionKeyDown()
    {
        ConsoleKeyInfo keyInfo = new('\r', ConsoleKey.Enter, shift: false, alt: false, control: false);

        TKeyDownEvent keyDown = TKeyCodeTranslator.FromConsoleKey(keyInfo);
        TEvent @event = TEvent.CreateKeyDown(keyDown);

        Assert.AreEqual(TEventKind.KeyDown, @event.What);
        Assert.AreEqual(TKeyCodeTranslator.KeyEnter, @event.KeyDown.KeyCode);
    }

    [TestMethod]
    public void Serialization_And_Driver_WorkTogether()
    {
        TRecordRegistry registry = new();
        registry.Register("demo.widget", DemoWidgetState.LoadFrom);
        TRecordSerializer serializer = new(registry);

        DemoWidgetState initial = new("Viewport", 12, true);
        byte[] blob = serializer.Serialize("demo.widget", initial);
        DemoWidgetState restored = serializer.Deserialize<DemoWidgetState>(blob);

        TConsoleDriver driver = new(restored.Width, 1);
        driver.BackBuffer.WriteText(0, 0, restored.Title.AsSpan(), ConsoleColor.White, ConsoleColor.Black);

        Assert.AreEqual(initial.Title, restored.Title);
        Assert.IsTrue(restored.IsVisible);
        Assert.AreEqual('V', driver.BackBuffer.GetCell(0, 0).Glyph);
        Assert.AreEqual('t', driver.BackBuffer.GetCell(7, 0).Glyph);
    }

    [TestMethod]
    public void Serialization_RejectsUnregisteredType()
    {
        TRecordSerializer serializer = new(new TRecordRegistry());
        DemoWidgetState state = new("Menu", 8, true);
        byte[] blob = serializer.Serialize("demo.widget", state);

        Assert.ThrowsExactly<KeyNotFoundException>(() => serializer.Deserialize(blob));
    }

    private sealed record DemoWidgetState(string Title, int Width, bool IsVisible) : ITStreamSerializable
    {
        public void SaveTo(TBinaryArchiveWriter writer)
        {
            writer.WriteString(Title);
            writer.WriteInt32(Width);
            writer.WriteBoolean(IsVisible);
        }

        public static DemoWidgetState LoadFrom(TBinaryArchiveReader reader)
        {
            string title = reader.ReadString();
            int width = reader.ReadInt32();
            bool isVisible = reader.ReadBoolean();
            return new DemoWidgetState(title, width, isVisible);
        }
    }
}
