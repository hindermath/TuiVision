// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.DynTxt;

/// <summary>
/// Dynamisches Textbeispiel mit Headless-Seam.
///
/// Dynamic text example with a headless seam.
/// </summary>
public sealed class DynTxtApp : TApplication
{
    /// <summary>Kurzer Textbefehl. / Short text command.</summary>
    public const ushort CmShortText = 12300;

    /// <summary>Langer Textbefehl. / Long text command.</summary>
    public const ushort CmLongText = 12301;

    /// <summary>Breitenbegrenzter Textbefehl. / Constrained-width text command.</summary>
    public const ushort CmConstrainedText = 12302;

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 12303;

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
    private TView? _mainView;
    private TStaticText? _visibleView;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public DynTxtApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "DynTxt: dynamic short, long, and constrained-width text\n" +
            "Commands update text through the app loop. Ctrl+Q quits.",
            "ready",
            CreateTextView("DynTxt ready", 14),
            "TStaticText");
    }

    /// <summary>
    /// Aktueller sichtbarer Text.
    ///
    /// Current visible text.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>Letzter Hauptkomponententyp. / Last main component type.</summary>
    public string LastVisibleComponentKind { get; private set; } = "TStaticText";

    /// <summary>Bereich der letzten Hauptkomponente. / Region of the last main component.</summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Beschreibungstext fuer Help -> Description. / Description text for Help -> Description.</summary>
    public string DescriptionText =>
        "DynTxt description: a dynamic text view shows changed, clipped, and constrained text.";

    /// <summary>
    /// Sichtbare Zustandsfolge seit Start.
    ///
    /// Visible state sequence since startup.
    /// </summary>
    public IReadOnlyList<string> VisibleHistory => _visibleHistory;

    /// <summary>
    /// Fuegt deterministische Ereignisse fuer den Headless-Lauf hinzu.
    ///
    /// Adds deterministic events for the headless run.
    /// </summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    /// <summary>
    /// Aktualisiert den dynamischen Text und klemmt ihn auf die Breite.
    ///
    /// Updates dynamic text and clips it to the width.
    /// </summary>
    /// <param name="value">Der neue Wert. / The new value.</param>
    /// <param name="width">Die sichtbare Breite. / The visible width.</param>
    /// <returns>Der sichtbare Text. / The visible text.</returns>
    public string UpdateText(string value, int width)
    {
        int effectiveWidth = Math.Max(0, width);
        string text = value ?? string.Empty;
        string visible = text.Length <= effectiveWidth ? text.PadRight(effectiveWidth) : text[..effectiveWidth];
        return SetVisibleState(visible, $"width {effectiveWidth}", CreateTextView(visible, Math.Max(1, effectiveWidth)), "TStaticText");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("DynTxt", DescriptionText), "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem textItems =
            new("~S~hort", CmShortText,
            new TMenuItem("~L~ong", CmLongText,
            new TMenuItem("~C~onstrained", CmConstrainedText)));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~D~ynTxt", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), textItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("DynTxt", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmShortText:
                    UpdateText("Ada", width: 12);
                    @event.Clear();
                    return;
                case CmLongText:
                    UpdateText("0123456789abcdef", width: 12);
                    @event.Clear();
                    return;
                case CmConstrainedText:
                    UpdateText("constrained", width: 5);
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

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _scriptedEvents.Count > 0)
        {
            @event = _scriptedEvents.Dequeue();
            return;
        }

        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    private string SetVisibleState(string text, string status, TView? mainView = null, string componentKind = "TStaticText")
    {
        VisibleText = text;
        _visibleHistory.Add(text);
        LastStatusMessage = Wave2Runtime.Status("DynTxt", status);
        Wave2Runtime.SetStatus(StatusLine, LastStatusMessage);
        if (Desktop is null)
        {
            return VisibleText;
        }

        if (_mainView?.Owner == Desktop)
        {
            Desktop.Remove(_mainView);
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        _mainView = mainView ?? new TStaticText(Wave2Runtime.MainRegion(Desktop), VisibleText);
        LastVisibleComponentKind = componentKind;
        LastVisibleRegion = Wave2Runtime.ScreenRegion(Desktop, _mainView);
        Desktop.Insert(_mainView);

        _visibleView = new TStaticText(Wave2Runtime.DetailRegion(Desktop), VisibleText);
        Desktop.Insert(_visibleView);
        return VisibleText;
    }

    private TStaticText? CreateTextView(string text, int width)
    {
        if (Desktop is null)
        {
            return null;
        }

        int actualWidth = Math.Clamp(width, 1, Math.Max(1, Desktop.Size.X - 4));
        return new TStaticText(new TRect(2, 1, 2 + actualWidth, 2), text);
    }

    private TWindow? CreateDescriptionWindow(string title, string body)
    {
        if (Desktop is null)
        {
            return null;
        }

        TRect region = Wave2Runtime.MainRegion(Desktop, width: 56, height: 7);
        TWindow window = new($"{title} Help", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, region.Width - 2, region.Height - 1), body));
        return window;
    }
}
