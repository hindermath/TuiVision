// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für sichere StatusLine-Beschreibungen und Runtime-Rekonstruktion.
///
/// Tests for safe status-line descriptions and runtime reconstruction.
/// </summary>
[TestClass]
public sealed class StatusLineDescriptionTests
{
    /// <summary>
    /// Prüft Kontextreihenfolge, Command und zugänglichen Shortcut.
    /// Verifies context order, command, and accessible shortcut.
    /// </summary>
    [TestMethod]
    public void StatusLineDescription_F013_ValidModelReconstructsContextOrderAndShortcut()
    {
        StatusLineDescription description = CreateValid();
        TStatusLine status = StatusLineDescriptionFactory.CreateStatusLine(description, new TRect(0, 0, 40, 1));
        TView focused = new(new TRect(0, 0, 1, 1)) { HelpContext = 15 };

        status.HandleEvent(TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focused));

        Assert.AreEqual("~F1~ Help", status.Items!.Name);
        Assert.AreEqual(200, status.Items.Command);
        Assert.AreEqual((ushort)0x3B00, status.Items.KeyCode);
        Assert.HasCount(1, status.GetAccessibleShortcuts());
    }

    /// <summary>
    /// Prüft die Ablehnung ungültiger Bereiche, Reihenfolgen und Commands.
    /// Verifies rejection of invalid ranges, orders, and commands.
    /// </summary>
    [TestMethod]
    public void StatusLineDescription_F013_InvalidRangesOrdersLabelsAndCommandsAreRejected()
    {
        StatusLineDescription[] invalid =
        [
            new(1, [new(-1, 2, 0, [new("X", 1, 0, false)])]),
            new(1, [new(5, 2, 0, [new("X", 1, 0, false)])]),
            new(1, [new(0, 2, -1, [new("X", 1, 0, false)])]),
            new(1, [new(0, 2, 0, [new(" ", 1, 0, false)])]),
            new(1, [new(0, 2, 0, [new("X", 0, 0, false)])]),
            new(1, [new(0, 2, 0, [new("X", 1, 0, false)]), new(3, 4, 0, [new("Y", 2, 0, false)])])
        ];

        foreach (StatusLineDescription description in invalid)
        {
            Assert.ThrowsExactly<InvalidDataException>(() =>
                StatusLineDescriptionFactory.CreateStatusLine(description, new TRect(0, 0, 40, 1)));
        }
    }

    /// <summary>
    /// Prüft die gleiche Validierungsgrenze für Persistenz und Controls.
    /// Verifies the same validation boundary for persistence and controls.
    /// </summary>
    [TestMethod]
    public void StatusLineDescription_F013_PersistedAdapterMatchesControlsValidation()
    {
        StatusLineDescription source = CreateValid();
        TStatusLineDescriptionRecord record = UiDescriptionPersistenceAdapter.ToRecord(source);
        StatusLineDescription restored = UiDescriptionPersistenceAdapter.FromRecord(record);

        Assert.HasCount(source.Definitions.Count, restored.Definitions);
        Assert.AreEqual(source.Definitions[0].Items[0].CommandId, restored.Definitions[0].Items[0].CommandId);
        Assert.IsFalse(record.GetType().GetProperties().Any(property => typeof(TView).IsAssignableFrom(property.PropertyType)));
    }

    private static StatusLineDescription CreateValid() => new(
        1,
        [new StatusDefinitionDescription(10, 20, 0, [new StatusItemDescription("~F1~ Help", 200, 0x3B00, false)])]);
}
