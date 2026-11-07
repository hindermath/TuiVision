// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests für allowlist-basierte UI-Beschreibungsrecords.
///
/// Tests for allowlist-based UI-description records.
/// </summary>
[TestClass]
public sealed class TUiDescriptionRecordTests
{
    /// <summary>
    /// Akzeptiert den maximalen flachen Menürecord ohne quadratische Kindersuche.
    ///
    /// Accepts the maximum flat menu record without quadratic child lookup.
    /// </summary>
    [TestMethod]
    public void TMenuDescriptionRecord_MaximumFlatGraph_Validates()
    {
        List<TMenuItemDescriptionRecord> items = [new("root", null, 0, "Root", 0, 0, false)];
        for (int index = 1; index < TMenuDescriptionRecord.MaxItems; index++)
        {
            items.Add(new TMenuItemDescriptionRecord(
                $"leaf-{index}", "root", index, $"Leaf {index}", (ushort)(index % ushort.MaxValue + 1), 0, false));
        }

        TMenuDescriptionRecord record = new(TMenuDescriptionRecord.CurrentFormatVersion, items);
        SaveArchive(record);

        Assert.HasCount(TMenuDescriptionRecord.MaxItems, record.Items);
    }

    /// <summary>
    /// Lehnt viele einzeln gueltige Menues ab, sobald sie das gemeinsame Archivbudget ueberschreiten.
    ///
    /// Rejects many individually valid menus once they exceed the shared archive budget.
    /// </summary>
    [TestMethod]
    public void TMenuDescriptionRecord_AggregateResourceItems_ConsumeSharedBudget()
    {
        TRecordRegistry registry = SerializationTestSupport.CreateStreamRegistry();
        TResourceFile.RegisterBuiltInTypes(registry);
        TResourceFile resources = new(registry);
        List<TMenuItemDescriptionRecord> items = [new("root", null, 0, "Root", 0, 0, false)];
        for (int index = 1; index < TMenuDescriptionRecord.MaxItems; index++)
        {
            items.Add(new TMenuItemDescriptionRecord(
                $"leaf-{index}", "root", index, $"Leaf {index}", (ushort)(index % ushort.MaxValue + 1), 0, false));
        }

        TMenuDescriptionRecord menu = new(TMenuDescriptionRecord.CurrentFormatVersion, items);
        for (int index = 0; index < 25; index++)
        {
            resources.Put($"Menu-{index}", menu);
        }

        using MemoryStream stream = new();
        resources.Save(stream);
        stream.Position = 0;

        InvalidDataException exception = Assert.ThrowsExactly<InvalidDataException>(() => TResourceFile.Load(stream, registry));
        StringAssert.Contains(exception.Message, "Aggregate collection count");
    }

    /// <summary>
    /// Prüft Built-in-Roundtrips für Dialog, Menü und StatusLine mit exakten Keys.
    /// Verifies built-in roundtrips for dialog, menu, and status line with exact keys.
    /// </summary>
    [TestMethod]
    public void TUiDescriptionRecord_F013_BuiltInsRoundtripDialogMenuAndStatusWithExactKeys()
    {
        TRecordRegistry registry = new();
        TResourceFile.RegisterBuiltInTypes(registry);
        TResourceFile source = new(registry);
        source.Put("Dialog", CreateDialog());
        source.Put("Menu", CreateMenu());
        source.Put("Status", CreateStatus());
        using MemoryStream stream = new();

        source.Save(stream);
        stream.Position = 0;
        TResourceFile restored = TResourceFile.Load(stream, registry);

        Assert.IsNotNull(restored.Get<TDialogDescriptionRecord>("Dialog"));
        Assert.IsNotNull(restored.Get<TMenuDescriptionRecord>("Menu"));
        Assert.IsNotNull(restored.Get<TStatusLineDescriptionRecord>("Status"));
        Assert.IsNull(restored.Get<TMenuDescriptionRecord>("menu"));
    }

    /// <summary>
    /// Prüft die Ablehnung ungültiger Versionen, Graphen, Bereiche und Commands.
    /// Verifies rejection of invalid versions, graphs, ranges, and commands.
    /// </summary>
    [TestMethod]
    public void TUiDescriptionRecord_F013_InvalidVersionGraphRangeAndCommandsAreRejectedOnSave()
    {
        TMenuDescriptionRecord invalidMenu = new(
            999,
            [new TMenuItemDescriptionRecord("leaf", null, 0, "Leaf", 0, 0, false)]);
        TStatusLineDescriptionRecord invalidStatus = new(
            1,
            [new TStatusDefinitionDescriptionRecord(5, 2, 0, [new TStatusItemDescriptionRecord("X", 0, 0, false)])]);

        Assert.ThrowsExactly<InvalidDataException>(() => SaveArchive(invalidMenu));
        Assert.ThrowsExactly<InvalidDataException>(() => SaveArchive(invalidStatus));
    }

    internal static TDialogDescriptionRecord CreateDialog() => new(
        PersistedDialogRepresentation.CurrentFormatVersion,
        "dialog",
        1,
        "Dialog",
        [new TDialogControlDescriptionRecord("ok", "button", "OK", null, true)],
        ["ok"],
        [new TDialogCommandBindingRecord(10, "ok", "accept", "Enter")]);

    internal static TMenuDescriptionRecord CreateMenu() => new(
        1,
        [
            new TMenuItemDescriptionRecord("file", null, 0, "~F~ile", 0, 10, false),
            new TMenuItemDescriptionRecord("open", "file", 0, "~O~pen", 100, 11, false)
        ]);

    internal static TStatusLineDescriptionRecord CreateStatus() => new(
        1,
        [new TStatusDefinitionDescriptionRecord(0, 100, 0, [new TStatusItemDescriptionRecord("~F1~ Help", 200, 0x3B00, false)])]);

    private static void SaveArchive(ITStreamSerializable record)
    {
        using MemoryStream stream = new();
        using TBinaryArchiveWriter writer = new(stream);
        record.SaveTo(writer);
    }
}
