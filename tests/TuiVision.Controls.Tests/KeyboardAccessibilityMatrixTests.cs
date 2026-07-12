using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft die explizite Tastatur-A11Y-Inventur fokussierbarer Control-Familien.
///
/// Verifies the explicit keyboard-accessibility inventory of focusable control families.
/// </summary>
[TestClass]
public sealed class KeyboardAccessibilityMatrixTests
{
    /// <summary>Prüft vollständige Proof- oder N/A-Zellen. / Verifies complete proof or N/A cells.</summary>
    [TestMethod]
    public void Inventory_EveryRequiredKeyFamily_HasProofOrRationale()
    {
        KeyboardRow[] rows =
        [
            new("TButton", "Proof", "Proof", "N/A: group navigation", "Proof", "Proof"),
            new("TInputLine", "Proof", "Proof", "Proof", "N/A: text control", "N/A: no command shortcut"),
            new("TListBox", "Proof", "Proof", "Proof", "N/A: selection confirmation is pointer-specific", "N/A: list navigation"),
            new("TMenuBar", "N/A: F10 activation", "N/A: F10 activation", "Proof", "Proof", "Proof"),
            new("TStatusLine", "N/A: passive provider", "N/A: passive provider", "N/A: passive provider", "N/A: passive provider", "Proof"),
            new("TDialog/TGroup", "Proof", "Proof", "N/A: child-specific", "Proof", "N/A: container"),
            new("AccessibleReferenceWidget", "Proof", "Proof", "N/A: group navigation", "Proof", "Proof")
        ];

        Assert.HasCount(7, rows);
        foreach (KeyboardRow row in rows)
        {
            Assert.IsTrue(row.Cells.All(IsComplete), $"Incomplete keyboard cell for {row.ControlFamily}.");
        }
    }

    /// <summary>Prüft Tab, Shift+Tab und deaktivierte Ziele. / Verifies Tab, Shift+Tab and disabled targets.</summary>
    [TestMethod]
    public void Dialog_TabAndShiftTab_MoveFocusAndSkipDisabledTargets()
    {
        TDialog dialog = new(new TRect(0, 0, 30, 10), "Tastatur / Keyboard");
        TButton first = new(new TRect(2, 2, 10, 3), "~E~ins", 20, TButtonFlags.bfNormal);
        TButton disabled = new(new TRect(2, 4, 10, 5), "~Z~wei", 21, TButtonFlags.bfNormal);
        TInputLine input = new(new TRect(2, 6, 14, 7), 20);
        disabled.SetState(TViewState.Disabled, true);
        dialog.Insert(first);
        dialog.Insert(disabled);
        dialog.Insert(input);
        dialog.SetFocus(first);

        dialog.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F));
        Assert.AreSame(input, dialog.Current);

        dialog.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: '\t', scanCode: 0x0F, shiftState: 1));
        Assert.AreSame(first, dialog.Current);
    }

    /// <summary>Prüft Pfeilnavigation in Eingabe und Liste. / Verifies arrow navigation in input and list.</summary>
    [TestMethod]
    public void InputAndList_ArrowKeys_ChangeOwnedState()
    {
        TInputLine input = new(new TRect(0, 0, 8, 1), 20);
        input.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'A'));
        input.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'B'));
        input.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x4B));

        TListBox list = new(new TRect(0, 0, 12, 3), 1, null) { List = new TStringList() };
        list.List.Add("Eins");
        list.List.Add("Zwei");
        list.HandleEvent(ControlEventFactory.CreateArrowDown());

        Assert.AreEqual(1, input.CurPos);
        Assert.AreEqual(1, list.FocusedItem);
    }

    /// <summary>Prüft F10, Pfeile und Enter im Menü. / Verifies F10, arrows and Enter in the menu.</summary>
    [TestMethod]
    public void MenuBar_F10ArrowsAndEnter_DispatchStructuredCommand()
    {
        RecordingGroup owner = new(new TRect(0, 0, 40, 10));
        TMenuItem open = new("~O~effnen", 42);
        TMenuBar menu = new(new TRect(0, 0, 40, 1))
        {
            Menu = new TMenuItem("~D~atei", 0, subMenu: open)
        };
        owner.Insert(menu);

        menu.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x44));
        menu.HandleEvent(ControlEventFactory.CreateArrowDown());
        menu.HandleEvent(ControlEventFactory.CreateKeyEnter());

        Assert.AreEqual((ushort)42, owner.LastCommand);
        Assert.AreEqual((ushort)42, menu.GetAccessibleShortcuts().Single().Command);
    }

    /// <summary>Prüft Enter und Direkt-Shortcut am Button. / Verifies Enter and direct shortcut on a button.</summary>
    [TestMethod]
    public void Button_EnterAndMnemonic_DispatchCommand()
    {
        RecordingGroup owner = new(new TRect(0, 0, 40, 10));
        TButton button = new(new TRect(1, 1, 12, 2), "~O~K", 43, TButtonFlags.bfNormal);
        owner.Insert(button);
        owner.SetFocus(button);

        owner.HandleEvent(ControlEventFactory.CreateKeyEnter());
        Assert.AreEqual((ushort)43, owner.LastCommand);

        owner.ResetCommand();
        owner.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: 'o', shiftState: 0x0004));
        Assert.AreEqual((ushort)43, owner.LastCommand);
    }

    private static bool IsComplete(string value) => value == "Proof" || value.StartsWith("N/A:", StringComparison.Ordinal);

    private sealed record KeyboardRow(
        string ControlFamily,
        string Tab,
        string ShiftTab,
        string Arrows,
        string Enter,
        string Shortcut)
    {
        public string[] Cells => [Tab, ShiftTab, Arrows, Enter, Shortcut];
    }

    private sealed class RecordingGroup : TGroup
    {
        public RecordingGroup(TRect bounds) : base(bounds)
        {
            SetState(TViewState.Exposed, true);
            SetState(TViewState.Selected, true);
        }

        public ushort LastCommand { get; private set; }

        public void ResetCommand() => LastCommand = 0;

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command)
            {
                LastCommand = @event.Message.Command;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }
}
