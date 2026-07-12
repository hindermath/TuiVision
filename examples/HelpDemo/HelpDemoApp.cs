// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;
using TuiVision.Serialization;

namespace TuiVision.Examples.HelpDemo;

/// <summary>Demonstriert fokussensitive Hilfe und Status-Hints. / Demonstrates focus-sensitive help and status hints.</summary>
public sealed class HelpDemoApp : Wave3Application
{
    /// <summary>Fokus auf Cancel setzen. / Focus Cancel.</summary>
    public const ushort CmFocusCancel = 19201;
    /// <summary>Aktuellen Kontext anzeigen. / Show current context.</summary>
    public const ushort CmShowHelp = 19202;
    /// <summary>Unbekannten Kontext anzeigen. / Show unknown context.</summary>
    public const ushort CmUnknownContext = 19203;
    /// <summary>Beschreibung anzeigen. / Show description.</summary>
    public const ushort CmDescription = 19204;

    private readonly THelpFile _helpFile;
    private readonly List<string> _visibleHistory = [];

    /// <summary>Initialisiert die Kontextdemo. / Initializes the context demo.</summary>
    public HelpDemoApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        _helpFile = CreateHelpFile();
        TRect region = Wave3Runtime.MainRegion(Desktop!, 54, 12);
        ControlWindow = new TWindow("HelpDemo contexts", region.A.X, region.A.Y, region.Width, region.Height);
        ControlWindow.Insert(new TStaticText(new TRect(2, 2, 34, 4), "Focus a control, then open its help context."));
        OkButton = new TButton(new TRect(4, 6, 18, 8), "~O~K", 19210, TButtonFlags.bfDefault) { HelpContext = 101 };
        CancelButton = new TButton(new TRect(22, 6, 38, 8), "~C~ancel", 19211, TButtonFlags.bfNormal) { HelpContext = 102 };
        ControlWindow.Insert(OkButton);
        ControlWindow.Insert(CancelButton);
        ControlWindow.SetFocus(OkButton);
        Desktop!.Insert(ControlWindow);
        Desktop.SetFocus(ControlWindow);
        CurrentContext = 101;
        CurrentHint = "OK button / OK-Schaltflaeche";
        SetWave3Visible(ControlWindow, "TWindow");
        SetWave3Status("HelpDemo", "context=101 hint=OK");
    }

    /// <summary>Fenster mit fokussierbaren Controls. / Window with focusable controls.</summary>
    public TWindow ControlWindow { get; }
    /// <summary>OK-Schaltfläche. / OK button.</summary>
    public TButton OkButton { get; }
    /// <summary>Cancel-Schaltfläche. / Cancel button.</summary>
    public TButton CancelButton { get; }
    /// <summary>Aktueller Hilfekontext. / Current help context.</summary>
    public int CurrentContext { get; private set; }
    /// <summary>Aktueller sichtbarer Hint. / Current visible hint.</summary>
    public string CurrentHint { get; private set; }
    /// <summary>Sichtbarer Zustandsverlauf. / Visible state history.</summary>
    public string VisibleHistoryText => string.Join('\n', _visibleHistory);
    /// <summary>Beschreibungstext. / Description text.</summary>
    public string DescriptionText =>
        "HelpDemo description: Fokus, HelpContext, Status-Hint und Topic bleiben ueber Tastaturbefehle verbunden. / " +
        "Focus, HelpContext, status hint, and topic remain connected through keyboard commands.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuBar bar = new(bounds)
        {
            Menu = new TMenuItem("~D~emo", 0,
                Wave3Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
                new TMenuItem("~C~ancel focus", CmFocusCancel, null,
                    new TMenuItem("~H~elp context", CmShowHelp, null,
                        new TMenuItem("~U~nknown context", CmUnknownContext))))
        };
        return bar;
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmFocusCancel)
        {
            ControlWindow.SetFocus(CancelButton);
            // Hint und Kontext werden am selben Dispatch-Punkt aktualisiert, damit Status und spaeteres Topic nicht auseinanderlaufen.
            // Hint and context update at the same dispatch point so status and the later topic cannot diverge.
            CurrentContext = 102;
            CurrentHint = "Cancel button / Cancel-Schaltflaeche";
            SetWave3Status("HelpDemo", "context=102 hint=Cancel");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmShowHelp)
        {
            ShowContext(CurrentContext);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmUnknownContext)
        {
            CurrentContext = 999;
            CurrentHint = "No help available";
            ShowContext(CurrentContext);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave3Description("HelpDemo", DescriptionText);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void ShowContext(int context)
    {
        TRect region = Wave3Runtime.MainRegion(Desktop!, 58, 13);
        THelpWindow window = new(region, _helpFile, context);
        Desktop!.Insert(window);
        Desktop.SetFocus(window);
        string title = window.Viewer.CurrentTopic?.Title ?? string.Empty;
        _visibleHistory.Add(context == 999 ? "No help available: context=999" : $"context={context} topic={title}");
        SetWave3Visible(window, "THelpWindow");
        SetWave3Status("HelpDemo", $"context={context} topic={title}");
    }

    private static THelpFile CreateHelpFile()
    {
        THelpTopic ok = new(101, "OK button");
        ok.AddParagraph("The default control accepts the dialog action.");
        THelpTopic cancel = new(102, "Cancel button");
        cancel.AddParagraph("Cancel leaves the demonstrated operation unchanged.");
        THelpFile file = new();
        file.AddTopic(ok);
        file.AddTopic(cancel);
        return file;
    }
}
