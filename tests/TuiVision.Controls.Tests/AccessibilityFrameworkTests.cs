using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft Fokusankündigungen, strukturierte Shortcuts und High Contrast.
///
/// Verifies focus announcements, structured shortcuts and high contrast.
/// </summary>
[TestClass]
public sealed class AccessibilityFrameworkTests
{
    /// <summary>Prüft einen typisierten Fokuswechsel. / Verifies a typed focus transition.</summary>
    [TestMethod]
    public void FocusTransition_AccessibleButton_EmitsTypedSemanticPayload()
    {
        RecordingProgram program = new(new TRect(0, 0, 40, 10));
        TButton button = new(new TRect(1, 1, 14, 3), "~S~peichern", 20, TButtonFlags.bfNormal);
        program.Insert(button);

        program.SetFocus(button);

        TFocusAnnouncement announcement = Assert.IsInstanceOfType<TFocusAnnouncement>(program.LastFocusInfo);
        Assert.AreSame(button, announcement.Target);
        Assert.AreEqual("Speichern", announcement.AccessibleLabel);
        Assert.IsTrue(announcement.CanReceiveFocus);
    }

    /// <summary>Prüft eine nicht migrierte View. / Verifies a non-migrated view.</summary>
    [TestMethod]
    public void FocusTransition_NonAccessibleView_DoesNotInventLabel()
    {
        RecordingProgram program = new(new TRect(0, 0, 40, 10));
        TView view = new(new TRect(1, 1, 5, 3)) { Options = TViewOptions.Selectable };
        program.Insert(view);

        program.SetFocus(view);

        TFocusAnnouncement announcement = Assert.IsInstanceOfType<TFocusAnnouncement>(program.LastFocusInfo);
        Assert.IsNull(announcement.AccessibleLabel);
        Assert.IsTrue(announcement.CanReceiveFocus);
    }

    /// <summary>Prüft die Same-target-No-op-Grenze. / Verifies the same-target no-op boundary.</summary>
    [TestMethod]
    public void FocusTransition_SameTarget_DoesNotEmitSecondAnnouncement()
    {
        RecordingProgram program = new(new TRect(0, 0, 40, 10));
        TButton button = new(new TRect(1, 1, 14, 3), "~O~K", 20, TButtonFlags.bfNormal);
        program.Insert(button);

        program.SetFocus(button);
        program.SetFocus(button);

        Assert.AreEqual(1, program.FocusAnnouncementCount);
    }

    /// <summary>Prüft Fokuswechsel innerhalb des Desktops. / Verifies focus transitions inside the desktop.</summary>
    [TestMethod]
    public void FocusTransition_DesktopDescendant_ReachesProgramBroadcast()
    {
        RecordingApplication app = new(new TRect(0, 0, 40, 10));
        TButton first = new(new TRect(1, 1, 12, 2), "~E~ins", 20, TButtonFlags.bfNormal);
        TButton second = new(new TRect(1, 3, 12, 4), "~Z~wei", 21, TButtonFlags.bfNormal);
        app.Desktop!.Insert(first);
        app.Desktop.Insert(second);
        int before = app.FocusAnnouncementCount;

        app.Desktop.SetFocus(first);
        app.Desktop.SetFocus(second);

        Assert.AreEqual(before + 2, app.FocusAnnouncementCount);
        TFocusAnnouncement announcement = Assert.IsInstanceOfType<TFocusAnnouncement>(app.LastFocusInfo);
        Assert.AreSame(second, announcement.Target);
    }

    /// <summary>Prüft typisierte und alte Status-Payloads. / Verifies typed and legacy status payloads.</summary>
    [TestMethod]
    public void StatusLine_FocusPayload_AcceptsTypedAndLegacyShapes()
    {
        HintView view = new(new TRect(0, 0, 5, 1));
        TStatusLine status = new(new TRect(0, 4, 40, 5));

        status.HandleEvent(TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, TFocusAnnouncement.Create(view)));
        Assert.IsNotNull(status.Items);

        status.HandleEvent(TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, view));
        Assert.IsNotNull(status.Items);
    }

    /// <summary>Prüft wahrheitsgetreue Menü-Shortcuts. / Verifies truthful menu shortcuts.</summary>
    [TestMethod]
    public void MenuBar_GetAccessibleShortcuts_ExcludesNonExecutableItems()
    {
        TMenuItem disabled = new("~D~eaktiviert", 13) { Disabled = true };
        TMenuItem separator = TMenuItem.Separator(disabled);
        TMenuItem open = new("~O~effnen", 12, separator);
        TMenuBar menu = new(new TRect(0, 0, 40, 1))
        {
            Menu = new TMenuItem("~D~atei", 0, subMenu: open)
        };

        IReadOnlyList<TAccessibleShortcut> shortcuts = menu.GetAccessibleShortcuts();

        Assert.HasCount(1, shortcuts);
        Assert.AreEqual((ushort)'O', shortcuts[0].KeyCode);
        Assert.AreEqual((ushort)12, shortcuts[0].Command);
        Assert.Contains("Oeffnen", shortcuts[0].DisplayText);
    }

    /// <summary>Prüft explizite Status-Shortcuts. / Verifies explicit status shortcuts.</summary>
    [TestMethod]
    public void StatusLine_GetAccessibleShortcuts_UsesExplicitKeysAndPreservesSources()
    {
        TStatusItem disabled = new("~F2~ Gesperrt", 31, keyCode: 0x3C00) { Disabled = true };
        TStatusItem enabled = new("~F1~ Hilfe / Help", 30, disabled, keyCode: 0x3B00);
        TStatusLine status = new(new TRect(0, 4, 40, 5), new TStatusDef(0, 10, enabled));

        IReadOnlyList<TAccessibleShortcut> shortcuts = status.GetAccessibleShortcuts();

        Assert.HasCount(1, shortcuts);
        Assert.AreEqual((ushort)0x3B00, shortcuts[0].KeyCode);
        Assert.AreEqual("TStatusLine", shortcuts[0].Source);
    }

    /// <summary>Prüft die semantischen High-Contrast-Rollen. / Verifies semantic high-contrast roles.</summary>
    [TestMethod]
    public void HighContrast_HasNamedDistinctSemanticRoles()
    {
        TColorScheme scheme = TColorScheme.HighContrast;

        Assert.AreEqual("HighContrast", scheme.Name);
        Assert.AreNotEqual(scheme.Background, scheme.Text);
        Assert.AreNotEqual(scheme.SelectionBackground, scheme.SelectionText);
        Assert.AreNotEqual(scheme.StatusBackground, scheme.StatusText);
    }

    /// <summary>Prüft die explizite, verhaltensneutrale Aktivierung. / Verifies explicit default-neutral activation.</summary>
    [TestMethod]
    public void ApplyColorScheme_IsExplicitAndPropagatesThroughGroups()
    {
        TGroup group = new(new TRect(0, 0, 40, 10));
        TMenuBar menu = new(new TRect(0, 0, 40, 1));
        group.Insert(menu);

        Assert.AreSame(TColorScheme.Default, group.ColorScheme);
        Assert.AreSame(TColorScheme.Default, menu.ColorScheme);

        group.ApplyColorScheme(TColorScheme.HighContrast);

        Assert.AreSame(TColorScheme.HighContrast, group.ColorScheme);
        Assert.AreSame(TColorScheme.HighContrast, menu.ColorScheme);
    }

    /// <summary>Prüft High Contrast auch im geöffneten Menü. / Verifies high contrast in the open menu.</summary>
    [TestMethod]
    public void MenuBar_HighContrast_AppliesToOpenSubmenuCells()
    {
        TMenuBar menu = new(new TRect(0, 0, 30, 1))
        {
            Menu = new TMenuItem("~D~atei", 0, subMenu: new TMenuItem("~O~effnen", 20))
        };
        TGroup owner = ControlTestContext.AttachToOwner(menu, new TRect(0, 0, 30, 8));
        owner.ApplyColorScheme(TColorScheme.HighContrast);
        menu.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x44));
        menu.HandleEvent(ControlEventFactory.CreateArrowDown());

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual(TColorScheme.HighContrast.Background, buffer.GetCell(1, 1).Background);
        Assert.AreEqual(TColorScheme.HighContrast.Text, buffer.GetCell(1, 1).Foreground);
    }

    private sealed class RecordingProgram : TProgram
    {
        public RecordingProgram(TRect bounds) : base(bounds)
        {
        }

        public object? LastFocusInfo { get; private set; }

        public int FocusAnnouncementCount { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Broadcast && @event.Message.Command == ShellCommandIds.cmFocusChanged)
            {
                LastFocusInfo = @event.Message.Info;
                FocusAnnouncementCount++;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class RecordingApplication : TApplication
    {
        public RecordingApplication(TRect bounds) : base(bounds)
        {
        }

        public object? LastFocusInfo { get; private set; }

        public int FocusAnnouncementCount { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Broadcast && @event.Message.Command == ShellCommandIds.cmFocusChanged)
            {
                LastFocusInfo = @event.Message.Info;
                FocusAnnouncementCount++;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class HintView : TView
    {
        public HintView(TRect bounds) : base(bounds)
        {
            Options |= TViewOptions.Selectable;
        }

        public override TStatusItem GetStatusHints() => new("~F1~ Hilfe / Help", 30, keyCode: 0x3B00);
    }
}
