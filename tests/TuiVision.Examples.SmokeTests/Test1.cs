using TuiVision.Compatibility;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Serialization;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Integrations-Smoke-Tests, die das Zusammenspiel aller fünf Module prüfen.
/// Jeder Test deckt einen vollständigen Pfad vom Eingabeereignis bis zur Darstellung ab.
///
/// Integration smoke tests that verify the interaction of all five modules.
/// Each test covers a complete path from an input event through to output presentation.
/// </summary>
[TestClass]
public sealed class ModuleSmokeTests
{
    /// <summary>
    /// Prüft, dass <see cref="TKeyCodeTranslator"/> eine <see cref="ConsoleKeyInfo"/> für
    /// die Enter-Taste korrekt in ein Turbo-Vision-Tastenereignis umwandelt.
    ///
    /// Verifies that <see cref="TKeyCodeTranslator"/> correctly converts a
    /// <see cref="ConsoleKeyInfo"/> for the Enter key into a Turbo Vision key event.
    /// </summary>
    [TestMethod]
    public void Compatibility_MapsConsoleEnterToTurboVisionKeyDown()
    {
        ConsoleKeyInfo keyInfo = new('\r', ConsoleKey.Enter, shift: false, alt: false, control: false);

        TKeyDownEvent keyDown = TKeyCodeTranslator.FromConsoleKey(keyInfo);
        TEvent @event = TEvent.CreateKeyDown(keyDown);

        Assert.AreEqual(TEventKind.KeyDown, @event.What);
        Assert.AreEqual(TKeyCodeTranslator.KeyEnter, @event.KeyDown.KeyCode);
    }

    /// <summary>
    /// Prüft den vollständigen Serialisierungs-Deserialisierungs-Renderingzyklus:
    /// Ein Zustandsobjekt wird serialisiert, wiederhergestellt und in einem Konsolenpuffer gerendert.
    ///
    /// Verifies the full serialize-deserialize-render cycle:
    /// a state object is serialized, restored, and rendered into a console buffer.
    /// </summary>
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

    /// <summary>
    /// Prüft, dass der Serialisierer eine <see cref="KeyNotFoundException"/> auslöst,
    /// wenn der Typbezeichner in der Registry nicht registriert ist.
    ///
    /// Verifies that the serializer throws a <see cref="KeyNotFoundException"/>
    /// when the type identifier is not registered in the registry.
    /// </summary>
    [TestMethod]
    public void Serialization_RejectsUnregisteredType()
    {
        TRecordSerializer serializer = new(new TRecordRegistry());
        DemoWidgetState state = new("Menu", 8, true);
        byte[] blob = serializer.Serialize("demo.widget", state);

        Assert.ThrowsExactly<KeyNotFoundException>(() => serializer.Deserialize(blob));
    }

    /// <summary>
    /// Einfacher Zustandstyp für Smoke-Tests der Serialisierungsschicht.
    ///
    /// Simple state type used for smoke-testing the serialization layer.
    /// </summary>
    /// <param name="Title">Der Titel des Widgets. / The title of the widget.</param>
    /// <param name="Width">Die Breite des Widgets in Zeichen. / The width of the widget in characters.</param>
    /// <param name="IsVisible">Gibt an, ob das Widget sichtbar ist. / Indicates whether the widget is visible.</param>
    private sealed record DemoWidgetState(string Title, int Width, bool IsVisible) : ITStreamSerializable
    {
        /// <summary>
        /// Schreibt den Zustand in den angegebenen Writer.
        ///
        /// Writes the state to the specified writer.
        /// </summary>
        /// <param name="writer">Der Ziel-Writer. / The target writer.</param>
        public void SaveTo(TBinaryArchiveWriter writer)
        {
            writer.WriteString(Title);
            writer.WriteInt32(Width);
            writer.WriteBoolean(IsVisible);
        }

        /// <summary>
        /// Liest einen <see cref="DemoWidgetState"/> aus dem angegebenen Reader.
        ///
        /// Reads a <see cref="DemoWidgetState"/> from the specified reader.
        /// </summary>
        /// <param name="reader">Der Quell-Reader. / The source reader.</param>
        /// <returns>
        /// Eine neue <see cref="DemoWidgetState"/>-Instanz mit den gelesenen Werten.
        /// A new <see cref="DemoWidgetState"/> instance with the values that were read.
        /// </returns>
        public static DemoWidgetState LoadFrom(TBinaryArchiveReader reader)
        {
            string title = reader.ReadString();
            int width = reader.ReadInt32();
            bool isVisible = reader.ReadBoolean();
            return new DemoWidgetState(title, width, isVisible);
        }
    }
}
