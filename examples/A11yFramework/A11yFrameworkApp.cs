using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.A11yFramework;

/// <summary>
/// Zeigt die textbasierten Fokus-, Shortcut- und High-Contrast-Verträge von Feature 023.
///
/// Demonstrates the text-based focus, shortcut and high-contrast contracts of Feature 023.
/// </summary>
public sealed class A11yFrameworkApp : TApplication
{
    /// <summary>Fokussiert das nächste Widget. / Focuses the next widget.</summary>
    public const ushort CmFocusNext = 23001;

    /// <summary>Schaltet High Contrast um. / Toggles high contrast.</summary>
    public const ushort CmToggleContrast = 23002;

    /// <summary>Zeigt die Beschreibung. / Shows the description.</summary>
    public const ushort CmDescription = 23003;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<TAccessibleShortcut> _accessibleShortcuts = [];
    private bool _quitIssued;
    private TView? _descriptionView;

    /// <summary>
    /// Erstellt die sichtbare A11Y-Referenzanwendung.
    ///
    /// Creates the visible accessibility reference application.
    /// </summary>
    /// <param name="bounds">Die Anwendungsgrenzen. / The application bounds.</param>
    /// <param name="headless">Aktiviert die deterministische Testschleife. / Enables the deterministic test loop.</param>
    public A11yFrameworkApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;

        TRect firstBounds = MainRegion(Desktop!, top: 1);
        TRect secondBounds = MainRegion(Desktop!, top: Math.Min(5, Math.Max(2, Desktop!.Size.Y / 2)));
        FirstAction = new AccessibleActionView(
            firstBounds,
            "Erste Aktion / First action",
            "Tab wechselt den Fokus. / Tab moves focus.",
            CmFocusNext);
        SecondAction = new AccessibleActionView(
            secondBounds,
            "Zweite Aktion / Second action",
            "Enter schaltet High Contrast. / Enter toggles high contrast.",
            CmToggleContrast);
        Desktop.Insert(FirstAction);
        Desktop.Insert(SecondAction);
        Desktop.SetFocus(FirstAction);
        SetVisible(FirstAction, nameof(AccessibleActionView));

        if (MenuBar is IAccessibleShortcutProvider menuProvider)
        {
            _accessibleShortcuts.AddRange(menuProvider.GetAccessibleShortcuts());
        }

        if (StatusLine is IAccessibleShortcutProvider statusProvider)
        {
            _accessibleShortcuts.AddRange(statusProvider.GetAccessibleShortcuts());
        }

        UpdateStatus();
    }

    /// <summary>Das erste opt-in Widget. / The first opt-in widget.</summary>
    public TView FirstAction { get; }

    /// <summary>Das zweite opt-in Widget. / The second opt-in widget.</summary>
    public TView SecondAction { get; }

    /// <summary>Die letzte Fokusbezeichnung. / The last focus label.</summary>
    public string? LastFocusLabel { get; private set; }

    /// <summary>Der letzte sichtbare Status. / The last visible status.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Der letzte sichtbare Haupttyp. / The last visible main type.</summary>
    public string LastVisibleComponentKind { get; private set; } = string.Empty;

    /// <summary>Die letzte stabile Proof-Region. / The last stable proof region.</summary>
    public TRect LastVisibleRegion { get; private set; }

    /// <summary>Ob High Contrast aktiv ist. / Whether high contrast is active.</summary>
    public bool HighContrastEnabled { get; private set; }

    /// <summary>Der aktuelle Schemaname. / The current scheme name.</summary>
    public string CurrentSchemeName => ColorScheme.Name;

    /// <summary>Native Assistive-Technik ist in diesem Feature bewusst nicht verfügbar. / Native assistive technology is intentionally unavailable in this feature.</summary>
    public bool NativeBridgeAvailable => false;

    /// <summary>Die stabilen Shortcut-Beschreibungen der App. / The app's stable shortcut descriptions.</summary>
    public IReadOnlyList<TAccessibleShortcut> AccessibleShortcuts => _accessibleShortcuts.AsReadOnly();

    /// <summary>Fügt kontrollierte App-Loop-Ereignisse hinzu. / Adds controlled app-loop events.</summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        if (_headless && !_quitIssued)
        {
            _quitIssued = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem help = new(
            "~H~ilfe / ~H~elp",
            0,
            new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit),
            new TMenuItem("~B~eschreibung / ~D~escription", CmDescription));
        TMenuItem contrast = new("~K~ontrast / ~C~ontrast", CmToggleContrast, help);
        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~A~11Y", 0, subMenu: new TMenuItem("~W~eiter / ~N~ext focus", CmFocusNext, contrast))
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) => new A11yStatusLine(bounds);

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Broadcast
            && @event.Message.Command == ShellCommandIds.cmFocusChanged
            && @event.Message.Info is TFocusAnnouncement announcement)
        {
            LastFocusLabel = announcement.AccessibleLabel;
            SetVisible(announcement.Target, announcement.Target.GetType().Name);
            UpdateStatus();
        }

        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmFocusNext:
                    Desktop?.SelectNext(forward: true);
                    @event.Clear();
                    return;
                case CmToggleContrast:
                    HighContrastEnabled = !HighContrastEnabled;
                    ApplyColorScheme(HighContrastEnabled ? TColorScheme.HighContrast : TColorScheme.Default);
                    UpdateStatus();
                    @event.Clear();
                    return;
                case CmDescription:
                    ShowDescription();
                    @event.Clear();
                    return;
            }
        }

        base.HandleEvent(@event);
    }

    private void UpdateStatus()
    {
        string focus = LastFocusLabel ?? "Desktop";
        LastStatusMessage = $"A11Y: Focus={focus} | Scheme={CurrentSchemeName} | native bridge unavailable";
        if (StatusLine is A11yStatusLine status)
        {
            status.SetMessage(LastStatusMessage);
        }
    }

    private void ShowDescription()
    {
        if (Desktop is null)
        {
            return;
        }

        if (_descriptionView?.Owner == Desktop)
        {
            Desktop.Remove(_descriptionView);
        }

        TRect region = new(2, 1, Math.Max(14, Desktop.Size.X - 2), Math.Max(7, Desktop.Size.Y - 1));
        TWindow window = new("A11Y Description", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(
            new TRect(2, 2, Math.Max(4, region.Width - 2), Math.Max(4, region.Height - 1)),
            "A11Y description: Fokus, Shortcuts und High Contrast bleiben textbasiert. / " +
            "Focus, shortcuts and high contrast remain text based. Native bridge unavailable."));
        Desktop.Insert(window);
        Desktop.SetFocus(window);
        _descriptionView = window;
        SetVisible(window, nameof(TWindow));
        LastStatusMessage = "A11Y: Description visible | native bridge unavailable";
        if (StatusLine is A11yStatusLine status)
        {
            status.SetMessage(LastStatusMessage);
        }
    }

    private void SetVisible(TView view, string kind)
    {
        LastVisibleComponentKind = kind;
        if (Desktop is null)
        {
            LastVisibleRegion = view.GetBounds();
            return;
        }

        TRect bounds = view.GetBounds();
        LastVisibleRegion = new TRect(
            Desktop.Origin.X + bounds.A.X,
            Desktop.Origin.Y + bounds.A.Y,
            Desktop.Origin.X + bounds.B.X,
            Desktop.Origin.Y + bounds.B.Y);
    }

    private static TRect MainRegion(TGroup desktop, int top)
    {
        int bottom = Math.Min(desktop.Size.Y - 1, top + 3);
        return new TRect(2, top, Math.Max(14, desktop.Size.X - 2), Math.Max(top + 1, bottom));
    }
}

internal sealed class AccessibleActionView : TView, IAccessibleWidget
{
    private readonly ushort _command;

    public AccessibleActionView(TRect bounds, string label, string description, ushort command) : base(bounds)
    {
        AccessibleLabel = label;
        AccessibleDescription = description;
        _command = command;
        Options |= TViewOptions.Selectable | TViewOptions.PostProcess;
    }

    public string AccessibleLabel { get; }

    public string? AccessibleDescription { get; }

    public bool CanReceiveFocus => Options.HasFlag(TViewOptions.Selectable)
        && GetState(TViewState.Visible)
        && !GetState(TViewState.Disabled);

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        string marker = GetState(TViewState.Focused) ? "> " : "  ";
        WriteLine(buffer, 0, $"{marker}A11Y: {AccessibleLabel}");
        if (Size.Y > 1)
        {
            WriteLine(buffer, 1, AccessibleDescription ?? string.Empty);
        }
        if (Size.Y > 2)
        {
            WriteLine(buffer, 2, "native bridge unavailable");
        }
    }

    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (@event.What == TEventKind.KeyDown
            && (@event.KeyDown.ScanCode == 0x1C || @event.KeyDown.CharCode == '\r'))
        {
            Owner?.HandleEvent(TEvent.CreateCommand(_command, this));
            @event.Clear(this);
        }
    }

    private void WriteLine(TConsoleBuffer buffer, int row, string value)
    {
        string text = value.Length <= Size.X ? value : value[..Size.X];
        string padded = text.PadRight(Size.X);
        ConsoleColor foreground = GetState(TViewState.Focused) ? ColorScheme.Emphasis : ColorScheme.Text;
        buffer.WriteText(Origin.X, Origin.Y + row, padded.AsSpan(), foreground, ColorScheme.Background);
    }
}

internal sealed class A11yStatusLine : TStatusLine
{
    public A11yStatusLine(TRect bounds) : base(
        bounds,
        new TStatusDef(
            0,
            int.MaxValue,
            new TStatusItem("~F1~ Beschreibung / Description", A11yFrameworkApp.CmDescription,
                new TStatusItem("~Alt+C~ Kontrast / Contrast", A11yFrameworkApp.CmToggleContrast, keyCode: 0x2E00),
                keyCode: 0x3B00)))
    {
    }

    public string Message { get; private set; } = "A11Y: ready";

    public void SetMessage(string message)
    {
        Message = message;
        DrawView();
    }

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0)
        {
            return;
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        string padded = text.PadRight(Size.X);
        buffer.WriteText(Origin.X, Origin.Y, padded.AsSpan(), ColorScheme.StatusText, ColorScheme.StatusBackground);
    }
}
