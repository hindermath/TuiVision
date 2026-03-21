// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Zusammengesetzte Integrationsszenarien für vollständige Dialoge.
///
/// Composite integration scenarios for complete dialogs.
/// </summary>
[TestClass]
public sealed class TDialogCompositeTests
{
    /// <summary>
    /// Prüft einen vollständigen Dialogdurchlauf mit Pflicht-Controls.
    ///
    /// Verifies a complete dialog run with the required controls.
    /// </summary>
    [TestMethod]
    public void TDialog_Run_CompositeDialogCoordinatesRequiredControls()
    {
        CompositeDialog dialog = new(new TRect(0, 0, 40, 12), "Composite");
        TInputLine inputLine = new(new TRect(2, 2, 12, 3), 12);
        TListBox listBox = new(new TRect(2, 4, 14, 7), 1, null)
        {
            List = new TStringList()
        };
        listBox.List.Add("One");
        listBox.List.Add("Two");
        listBox.List.Add("Three");
        TCheckBoxes checkBoxes = new(new TRect(18, 2, 34, 4), ["Verbose", "Safe"]);
        TRadioButtons radioButtons = new(new TRect(18, 5, 34, 7), ["First", "Second"]);
        TButton okButton = new(new TRect(2, 9, 10, 10), "OK", ShellCommandIds.cmOK, TButtonFlags.bfDefault);
        dialog.Insert(inputLine);
        dialog.Insert(listBox);
        dialog.Insert(checkBoxes);
        dialog.Insert(radioButtons);
        dialog.Insert(okButton);
        ControlTestContext.AttachToOwner(dialog, new TRect(0, 0, 50, 16));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: 'H'));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: 'i'));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(scanCode: 0x50));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: ' '));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(scanCode: 0x50));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: ' '));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F));
        dialog.Enqueue(ControlEventFactory.CreateKeyDown(charCode: '\r', scanCode: 0x1C));

        ushort result = dialog.Run();

        Assert.AreEqual(ShellCommandIds.cmOK, result);
        Assert.AreEqual("Hi", inputLine.Data);
        Assert.AreEqual(1, listBox.FocusedItem);
        Assert.AreEqual(0, listBox.TopItem);
        Assert.AreEqual(1u, checkBoxes.Value);
        Assert.AreEqual(1u, radioButtons.Value);
    }

    private sealed class CompositeDialog : TDialog
    {
        private readonly Queue<TEvent> _events = new();

        public CompositeDialog(TRect bounds, string? title) : base(bounds, title)
        {
        }

        public void Enqueue(TEvent @event) => _events.Enqueue(@event);

        protected override void GetEvent(out TEvent @event)
        {
            @event = _events.Count > 0 ? _events.Dequeue() : TEvent.CreateCommand(ShellCommandIds.cmCancel);
        }
    }
}
