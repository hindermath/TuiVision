// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.Clipboard;

/// <summary>
/// Zwischenablage-Beispiel mit Headless-Seam fuer Smoke-Tests.
///
/// Clipboard example with a headless seam for smoke tests.
/// </summary>
public sealed class ClipboardApp : TApplication
{
    /// <summary>Kopierbefehl. / Copy command.</summary>
    public const ushort CmCopy = 12100;

    /// <summary>Ausschneidebefehl. / Cut command.</summary>
    public const ushort CmCut = 12101;

    /// <summary>Einfuegebefehl. / Paste command.</summary>
    public const ushort CmPaste = 12102;

    /// <summary>Fallback-Befehl fuer nicht verfuegbare Zwischenablagen. / Fallback command for unavailable clipboards.</summary>
    public const ushort CmUnavailable = 12103;

    /// <summary>Help -> Description-Befehl. / Help -> Description command.</summary>
    public const ushort CmDescription = 12104;

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
    public ClipboardApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "Clipboard: copy, cut, paste, and fallback feedback\n" +
            "Commands: copy, cut, paste, unavailable clipboard. Ctrl+Q quits.",
            "ready",
            CreateInputView("clipboard ready"),
            "TInputLine");
    }

    /// <summary>
    /// Aktueller Eingabetext des Beispiels.
    ///
    /// Current input text of the example.
    /// </summary>
    public string InputText { get; private set; } = string.Empty;

    /// <summary>
    /// Letzter sichtbarer Ergebnistext.
    ///
    /// Last visible result text.
    /// </summary>
    public string LastVisibleMessage { get; private set; } = "clipboard: ready";

    /// <summary>Letzter Hauptkomponententyp. / Last main component type.</summary>
    public string LastVisibleComponentKind { get; private set; } = "TInputLine";

    /// <summary>Bereich der letzten Hauptkomponente. / Region of the last main component.</summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>Letzter Statuszeilentext. / Last status-line text.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>Beschreibungstext fuer Help -> Description. / Description text for Help -> Description.</summary>
    public string DescriptionText =>
        "Clipboard description: a visible input line shows copy, cut, paste, and unavailable clipboard states.";

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
    /// Setzt den Eingabetext.
    ///
    /// Sets the input text.
    /// </summary>
    /// <param name="text">Der neue Text. / The new text.</param>
    public void SetInput(string text) => InputText = text ?? string.Empty;

    /// <summary>
    /// Kopiert den Eingabetext in die verwaltete Zwischenablage.
    ///
    /// Copies the input text into the managed clipboard.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CopyInput()
    {
        ManagedClipboard.SetText(InputText);
        return SetVisibleState($"clipboard: copied '{InputText}'", "copied", CreateInputView(InputText), "TInputLine");
    }

    /// <summary>
    /// Schneidet den Eingabetext aus und merkt ihn in der verwalteten Zwischenablage.
    ///
    /// Cuts the input text and stores it in the managed clipboard.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CutInput()
    {
        ManagedClipboard.SetText(InputText);
        string cut = InputText;
        InputText = string.Empty;
        return SetVisibleState($"clipboard: cut '{cut}'", "cut", CreateInputView($"cut {cut}"), "TInputLine");
    }

    /// <summary>
    /// Fuegt den Text aus der verwalteten Zwischenablage ein.
    ///
    /// Pastes text from the managed clipboard.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string PasteIntoInput()
    {
        InputText = ManagedClipboard.GetText() ?? string.Empty;
        return SetVisibleState($"clipboard: pasted '{InputText}'", "pasted", CreateInputView(InputText), "TInputLine");
    }

    /// <summary>
    /// Liefert den sichtbaren Zustand fuer eine isolierte oder nicht verfuegbare Zwischenablage.
    ///
    /// Returns the visible state for an isolated or unavailable clipboard.
    /// </summary>
    /// <returns>Der sichtbare Fallback-Zustand. / The visible fallback state.</returns>
    public string SimulateUnavailableClipboard()
    {
        return SetVisibleState("clipboard: isolated fallback visible", "unavailable fallback", CreateInputView("isolated fallback visible"), "TInputLine");
    }

    /// <summary>Zeigt Help -> Description. / Shows Help -> Description.</summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(DescriptionText, "description visible", CreateDescriptionWindow("Clipboard", DescriptionText), "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem clipboardItems =
            new("~C~opy", CmCopy,
            new TMenuItem("C~u~t", CmCut,
            new TMenuItem("~P~aste", CmPaste,
            new TMenuItem("~U~navailable", CmUnavailable))));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~C~lipboard", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), clipboardItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("Clipboard", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmCopy:
                    CopyInput();
                    @event.Clear();
                    return;
                case CmCut:
                    CutInput();
                    @event.Clear();
                    return;
                case CmPaste:
                    PasteIntoInput();
                    @event.Clear();
                    return;
                case CmUnavailable:
                    SimulateUnavailableClipboard();
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
        LastVisibleMessage = text;
        _visibleHistory.Add(text);
        LastStatusMessage = Wave2Runtime.Status("Clipboard", status);
        Wave2Runtime.SetStatus(StatusLine, LastStatusMessage);
        if (Desktop is null)
        {
            return LastVisibleMessage;
        }

        if (_mainView?.Owner == Desktop)
        {
            Desktop.Remove(_mainView);
        }

        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        _mainView = mainView ?? new TStaticText(Wave2Runtime.MainRegion(Desktop), LastVisibleMessage);
        LastVisibleComponentKind = componentKind;
        LastVisibleRegion = Wave2Runtime.ScreenRegion(Desktop, _mainView);
        Desktop.Insert(_mainView);

        _visibleView = new TStaticText(Wave2Runtime.DetailRegion(Desktop), LastVisibleMessage);
        Desktop.Insert(_visibleView);
        return LastVisibleMessage;
    }

    private TInputLine? CreateInputView(string text)
    {
        if (Desktop is null)
        {
            return null;
        }

        TRect region = Wave2Runtime.MainRegion(Desktop, width: 38, height: 1);
        TInputLine input = new(region, 60) { Data = text };
        return input;
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
