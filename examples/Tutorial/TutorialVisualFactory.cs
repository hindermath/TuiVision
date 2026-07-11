// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;
using TuiVision.Examples.Tutorial.Steps;

namespace TuiVision.Examples.Tutorial;

internal sealed record TutorialVisualDescriptor(string Token, string Intent, string ComponentKind, string Marker);

internal static class TutorialVisualFactory
{
    public static TutorialVisualDescriptor Describe(ITutorialStep step) => step.SequenceNumber switch
    {
        1 => new(step.Token, "minimal application shell", "TStaticText", "application lifecycle"),
        2 => new(step.Token, "status-line item", "TStaticText", "status-line lesson"),
        3 => new(step.Token, "menu and command handling", "TWindow", "menu command result"),
        4 => new(step.Token, "window insertion", "TWindow", "inserted window"),
        5 => new(step.Token, "drawing inside a window", "TWindow", "custom drawing"),
        6 => new(step.Token, "scrollable content introduction", "TWindow", "vertical scroll content"),
        7 => new(step.Token, "improved two-axis content", "TWindow", "two-axis scroll content"),
        8 => new(step.Token, "scroller delta", "TScrollGroup", "delta 2,1"),
        9 => new(step.Token, "multiple panes", "TWindow", "left pane | right pane"),
        10 => new(step.Token, "resize constraints", "TWindow", "minimum 24x7 maximum desktop"),
        11 => new(step.Token, "non-modal dialog", "TDialog", "non-modal dialog state"),
        12 => new(step.Token, "modal dialog behaviour", "TDialog", "modal result pending"),
        13 => new(step.Token, "dialog buttons", "TDialog", "OK and Cancel"),
        14 => new(step.Token, "labels and choices", "TDialog", "check and radio choices"),
        15 => new(step.Token, "input line", "TInputLine", "input value"),
        16 => new(step.Token, "data transfer and validation", "TDialog", "saved restored rejected"),
        _ => throw new ArgumentOutOfRangeException(nameof(step), step.SequenceNumber, "Tutorial sequence must be between 1 and 16.")
    };

    public static TView CreateMainView(TDesktop desktop, TutorialVisualDescriptor descriptor, string state)
    {
        TRect region = Wave1Runtime.MainRegion(desktop, width: 58, height: 11);
        string text = $"{descriptor.Token} | {descriptor.Intent}\n{descriptor.Marker} | {state}";

        // Die Factory zeigt pro kumulativem C++-Schritt nur die praegende neue Idee.
        // The factory shows only the defining addition of each cumulative C++ step.
        int sequence = int.Parse(descriptor.Token[^2..], System.Globalization.CultureInfo.InvariantCulture);
        return sequence switch
        {
            1 or 2 => new TStaticText(region, text),
            3 or 4 or 5 or 6 or 7 or 9 or 10 => CreateWindow(region, descriptor.Token, text, sequence),
            8 => CreateScrollGroup(region, text),
            11 or 12 or 13 or 14 or 16 => CreateDialog(region, descriptor.Token, text, sequence),
            15 => new TInputLine(new TRect(region.A.X, region.A.Y, region.B.X, region.A.Y + 1), 80) { Data = text },
            _ => throw new ArgumentOutOfRangeException(nameof(descriptor))
        };
    }

    private static TWindow CreateWindow(TRect region, string token, string text, int sequence)
    {
        TWindow window = new($"Tutorial {token}", region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, region.Width - 3, 5), text));

        if (sequence is 6 or 7)
        {
            window.Insert(new TScrollBar(new TRect(region.Width - 2, 1, region.Width - 1, region.Height - 1)));
        }

        if (sequence == 7)
        {
            window.Insert(new TScrollBar(new TRect(1, region.Height - 2, region.Width - 2, region.Height - 1), horizontal: true));
        }

        if (sequence == 9)
        {
            int middle = region.Width / 2;
            window.Insert(new TStaticText(new TRect(2, 6, middle, 8), "left pane"));
            window.Insert(new TStaticText(new TRect(middle, 6, region.Width - 2, 8), "right pane"));
        }

        return window;
    }

    private static TScrollGroup CreateScrollGroup(TRect region, string text)
    {
        TScrollBar horizontal = new(new TRect(region.A.X, region.B.Y - 1, region.B.X - 1, region.B.Y), horizontal: true);
        TScrollBar vertical = new(new TRect(region.B.X - 1, region.A.Y, region.B.X, region.B.Y - 1));
        TScrollGroup group = new(region, horizontal, vertical);
        group.Insert(new TStaticText(new TRect(3, 2, region.Width - 2, 5), text));
        group.SetLimit(new TPoint(region.Width + 12, region.Height + 6));
        group.ScrollTo(new TPoint(2, 1));
        return group;
    }

    private static TDialog CreateDialog(TRect region, string token, string text, int sequence)
    {
        TDialog dialog = new(region, $"Tutorial {token}");
        dialog.Insert(new TStaticText(new TRect(2, 2, region.Width - 2, 5), text));

        if (sequence == 13)
        {
            dialog.Insert(new TButton(new TRect(4, 7, 16, 8), "~O~K", 1, TButtonFlags.bfDefault));
            dialog.Insert(new TButton(new TRect(19, 7, 33, 8), "~A~bbruch / ~C~ancel", 2, TButtonFlags.bfNormal));
        }
        else if (sequence == 14)
        {
            dialog.Insert(new TCheckBoxes(new TRect(3, 6, 26, 8), ["Tracing", "Status"]));
            dialog.Insert(new TRadioButtons(new TRect(29, 6, 52, 8), ["Deutsch", "English"]));
        }
        else if (sequence == 16)
        {
            dialog.Insert(new TInputLine(new TRect(3, 6, 34, 7), 40) { Data = "saved restored" });
            dialog.Insert(new TStaticText(new TRect(3, 8, 45, 9), "validation: rejected -> restored"));
        }

        return dialog;
    }
}

internal sealed class TutorialVisualApp : TApplication
{
    private readonly ITutorialStep _step;
    private readonly bool _headless;
    private readonly Queue<TEvent> _scriptedEvents = [];
    private bool _headlessEventFired;
    private TView? _mainView;

    public TutorialVisualApp(ITutorialStep step, TRect bounds, bool headless) : base(bounds)
    {
        _step = step;
        _headless = headless;
        SetMainState("ready");
    }

    public string LastVisibleComponentKind { get; private set; } = string.Empty;

    public TRect LastVisibleRegion { get; private set; }

    public string LastStatusMessage { get; private set; } = string.Empty;

    public string LastVisualSignature { get; private set; } = string.Empty;

    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _scriptedEvents.Enqueue(@event);
        }
    }

    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~L~ektion / ~L~esson",
            0,
            Wave1Runtime.HelpMenu(TutorialApp.CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
            new TMenuItem("~A~ktion / ~A~ction", TutorialApp.CmPrimary))
    };

    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave1StatusLine(bounds, Wave1Runtime.Status("Tutorial", "select a step"));

    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == TutorialApp.CmPrimary)
        {
            SetMainState("activated");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == TutorialApp.CmDescription)
        {
            ShowDescription();
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

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

    private void SetMainState(string state)
    {
        if (Desktop is null)
        {
            return;
        }

        RemoveMainView();
        TutorialVisualDescriptor descriptor = TutorialVisualFactory.Describe(_step);
        _mainView = TutorialVisualFactory.CreateMainView(Desktop, descriptor, state);
        Desktop.Insert(_mainView);
        LastVisibleComponentKind = descriptor.ComponentKind;
        LastVisibleRegion = Wave1Runtime.ScreenRegion(Desktop, _mainView);
        LastVisualSignature = $"{descriptor.Token}:{descriptor.ComponentKind}:{descriptor.Marker}:{state}";
        SetStatus($"{descriptor.Token} {state}");
    }

    private void ShowDescription()
    {
        if (Desktop is null)
        {
            return;
        }

        RemoveMainView();
        string body = $"Tutorial description: {_step.Token} - {_step.Title}\n{_step.Description}";
        _mainView = Wave1Runtime.CreateDescriptionWindow(Desktop, "Tutorial", body);
        if (_mainView is null)
        {
            return;
        }

        Desktop.Insert(_mainView);
        LastVisibleComponentKind = "TWindow";
        LastVisibleRegion = Wave1Runtime.ScreenRegion(Desktop, _mainView);
        LastVisualSignature = $"{_step.Token}:description";
        SetStatus($"{_step.Token} description visible");
    }

    private void RemoveMainView()
    {
        TView? mainView = _mainView;
        TDesktop? desktop = Desktop;
        if (mainView is not null && desktop is not null && mainView.Owner == desktop)
        {
            desktop.Remove(mainView);
        }
    }

    private void SetStatus(string state)
    {
        LastStatusMessage = Wave1Runtime.Status("Tutorial", state);
        Wave1Runtime.SetStatus(StatusLine, LastStatusMessage);
    }
}
