// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;
using TuiVision.Serialization;

namespace TuiVision.Examples.BHelp;

/// <summary>Sichtbarer moderner Help-Viewer für die BHelp-Absicht. / Visible modern help viewer for the BHelp intent.</summary>
public sealed class BHelpApp : Wave3Application
{
    /// <summary>Nächstes Thema. / Next topic.</summary>
    public const ushort CmNextTopic = 19101;
    /// <summary>Unbekannten Kontext prüfen. / Probe an unknown context.</summary>
    public const ushort CmUnknownContext = 19102;
    /// <summary>Beschreibung anzeigen. / Show description.</summary>
    public const ushort CmDescription = 19103;

    private readonly List<string> _visibleHistory = [];

    /// <summary>Initialisiert den Help-Viewer. / Initializes the help viewer.</summary>
    public BHelpApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        HelpFile = CreateHelpFile();
        TRect region = Wave3Runtime.MainRegion(Desktop!, 68, 17);
        HelpWindow = new THelpWindow(region, HelpFile, 1);
        Desktop!.Insert(HelpWindow);
        Desktop.SetFocus(HelpWindow);
        RecordTopic(1);
    }

    /// <summary>Das kontrollierte Hilfemodell. / The controlled help model.</summary>
    public THelpFile HelpFile { get; }
    /// <summary>Das sichtbare Hilfefenster. / The visible help window.</summary>
    public THelpWindow HelpWindow { get; }
    /// <summary>Aktueller sichtbarer Titel. / Current visible title.</summary>
    public string CurrentTopicTitle => HelpWindow.Viewer.CurrentTopic?.Title ?? string.Empty;
    /// <summary>Sichtbarer Zustandsverlauf. / Visible state history.</summary>
    public string VisibleHistoryText => string.Join('\n', _visibleHistory);
    /// <summary>Beschreibung der historischen Abweichung. / Description of the historical deviation.</summary>
    public string DescriptionText =>
        "BHelp description: Themen, Kontexte und Navigation bleiben sichtbar; der proprietary Borland .tch decoder " +
        "wird wegen seiner ungeprueften binaeren Grenzen nicht portiert. / Topics, contexts, and navigation stay visible; " +
        "the proprietary Borland .tch decoder is not ported because its binary boundaries are unchecked.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuBar bar = new(bounds)
        {
            Menu = new TMenuItem("~T~hema / ~T~opic", 0,
                Wave3Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
                new TMenuItem("~N~aechstes / ~N~ext", CmNextTopic, null,
                    new TMenuItem("~U~nbekannt / ~U~nknown", CmUnknownContext)))
        };
        return bar;
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmNextTopic)
        {
            OpenContext(2);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmUnknownContext)
        {
            OpenContext(999);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave3Description("BHelp", DescriptionText);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void OpenContext(int context)
    {
        HelpWindow.OpenContext(context);
        RecordTopic(context);
    }

    private void RecordTopic(int context)
    {
        string title = CurrentTopicTitle;
        _visibleHistory.Add(context == 999 ? "No help available: context=999" : $"context={context} topic={title}");
        SetWave3Visible(HelpWindow, "THelpWindow");
        SetWave3Status("BHelp", $"context={context} topic={title}");
    }

    private static THelpFile CreateHelpFile()
    {
        THelpTopic welcome = new(1, "BHelp Welcome");
        welcome.AddParagraph("Modern help viewer with controlled topics.");
        welcome.AddCrossReference(new THelpCrossReference(2, "Navigation", 0, 10));
        THelpTopic navigation = new(2, "Navigation");
        navigation.AddParagraph("Use topic commands to move through help contexts.");
        THelpFile file = new();
        file.AddTopic(welcome);
        file.AddTopic(navigation);
        return file;
    }
}
