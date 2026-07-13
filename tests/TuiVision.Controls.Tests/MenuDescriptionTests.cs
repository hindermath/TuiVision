// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für sichere Menübeschreibungen und deren Runtime-Rekonstruktion.
///
/// Tests for safe menu descriptions and their runtime reconstruction.
/// </summary>
[TestClass]
public sealed class MenuDescriptionTests
{
    /// <summary>
    /// Prüft Runtime-Identität, Reihenfolge, Command und sichtbare Zellen.
    /// Verifies runtime identity, order, command, and visible cells.
    /// </summary>
    [TestMethod]
    public void MenuDescription_F013_ValidModelReconstructsIdentityOrderCommandAndCells()
    {
        MenuDescription description = CreateValid();

        TMenuBar menu = MenuDescriptionFactory.CreateMenuBar(description, new TRect(0, 0, 30, 1));
        TGroup owner = ControlTestContext.AttachToOwner(menu, new TRect(0, 0, 30, 3));
        menu.DrawView();
        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual("~F~ile", menu.Menu!.Name);
        Assert.AreEqual("~O~pen", menu.Menu.SubMenu!.Name);
        Assert.AreEqual(100, menu.Menu.SubMenu.Command);
        Assert.AreEqual('O', menu.Menu.SubMenu.Mnemonic);
        ControlBufferAssert.AssertTextAt(buffer, 2, 0, "File");
    }

    /// <summary>
    /// Prüft die atomare Ablehnung ungültiger Menügraphen und Rollen.
    /// Verifies atomic rejection of invalid menu graphs and roles.
    /// </summary>
    [TestMethod]
    public void MenuDescription_F013_InvalidGraphOrderLabelAndCommandsAreRejected()
    {
        MenuDescription[] invalid =
        [
            new(1, [new("x", null, 0, "X", 1, 0, false), new("x", null, 1, "Y", 2, 0, false)]),
            new(1, [new("x", "missing", 0, "X", 1, 0, false)]),
            new(1, [new("a", "b", 0, "A", 0, 0, false), new("b", "a", 0, "B", 0, 0, false)]),
            new(1, [new("a", null, 0, "A", 1, 0, false), new("b", null, 0, "B", 2, 0, false)]),
            new(1, [new("x", null, 0, " ", 1, 0, false)]),
            new(1, [new("x", null, 0, "Leaf", 0, 0, false)]),
            CreateDepth(17)
        ];

        foreach (MenuDescription description in invalid)
        {
            Assert.ThrowsExactly<InvalidDataException>(() =>
                MenuDescriptionFactory.CreateMenuBar(description, new TRect(0, 0, 30, 1)));
        }
    }

    /// <summary>
    /// Prüft die gleiche Validierungsgrenze für Persistenz und Controls.
    /// Verifies the same validation boundary for persistence and controls.
    /// </summary>
    [TestMethod]
    public void MenuDescription_F013_PersistedAdapterMatchesControlsValidation()
    {
        MenuDescription source = CreateValid();
        TMenuDescriptionRecord record = UiDescriptionPersistenceAdapter.ToRecord(source);
        MenuDescription restored = UiDescriptionPersistenceAdapter.FromRecord(record);

        Assert.HasCount(source.Items.Count, restored.Items);
        Assert.AreEqual(source.Items[1].CommandId, restored.Items[1].CommandId);
        Assert.IsFalse(record.GetType().GetProperties().Any(property => typeof(TView).IsAssignableFrom(property.PropertyType)));
    }

    private static MenuDescription CreateValid() => new(
        1,
        [
            new MenuItemDescription("file", null, 0, "~F~ile", 0, 10, false),
            new MenuItemDescription("open", "file", 0, "~O~pen", 100, 11, false)
        ]);

    private static MenuDescription CreateDepth(int depth)
    {
        List<MenuItemDescription> items = [];
        string? parent = null;
        for (int index = 0; index < depth; index++)
        {
            string id = $"item-{index}";
            items.Add(new MenuItemDescription(id, parent, 0, id, index == depth - 1 ? (ushort)100 : (ushort)0, 0, false));
            parent = id;
        }

        return new MenuDescription(1, items);
    }
}
