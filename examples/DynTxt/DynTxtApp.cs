// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

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

    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private readonly List<string> _visibleHistory = [];
    private bool _headlessEventFired;
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
            "Commands update text through the app loop. Ctrl+Q quits.");
    }

    /// <summary>
    /// Aktueller sichtbarer Text.
    ///
    /// Current visible text.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

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
        return SetVisibleState(visible);
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem textItems =
            new("~S~hort", CmShortText,
            new TMenuItem("~L~ong", CmLongText,
            new TMenuItem("~C~onstrained", CmConstrainedText)));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~D~ynTxt", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), textItems)
        };
    }

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

    private string SetVisibleState(string text)
    {
        VisibleText = text;
        _visibleHistory.Add(text);
        if (Desktop is null)
        {
            return VisibleText;
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        int width = Math.Max(1, Desktop.Size.X - 2);
        int height = Math.Max(1, Math.Min(Desktop.Size.Y - 2, 6));
        _visibleView = new TStaticText(new TRect(1, 1, 1 + width, 1 + height), VisibleText);
        Desktop.Insert(_visibleView);
        return VisibleText;
    }
}
