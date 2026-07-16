// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Hauptdemobeispiel. / Functional TP7 main demo example.</summary>
public sealed class Tp7DemoApp : Wave5Application
{
    /// <summary>Öffnet ein Demo-Fenster. / Opens a demo window.</summary>
    public const ushort CmOpenWindow = 32101;

    /// <summary>Zeigt den Help-Kontext. / Shows the help context.</summary>
    public const ushort CmShowHelp = 32102;

    /// <summary>Initialisiert die Demo. / Initializes the demo.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7DemoApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        ShowContent("TP7 Demo", "TP7 Demo\nApplication, commands, windows and bounded gadgets");
        SetStatus("Tp7Demo", "ready");
    }

    /// <summary>Anzahl verarbeiteter Demo-Commands. / Number of processed demo commands.</summary>
    public int ProcessedCommandCount { get; private set; }

    /// <summary>Anzahl begrenzter Idle-Zyklen. / Number of bounded idle cycles.</summary>
    public int GadgetTicks { get; private set; }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) =>
        new(bounds)
        {
            Menu = new TMenuItem(
                "~D~emo",
                0,
                new TMenuItem("~H~ilfe / ~H~elp", 0, null, new TMenuItem("~K~ontext / ~C~ontext", CmShowHelp)),
                new TMenuItem("~F~enster / ~W~indow", CmOpenWindow))
        };

    /// <inheritdoc />
    protected override void Idle()
    {
        GadgetTicks++;
        ShowContent(
            "TP7 Demo Gadgets",
            $"TP7 Demo gadgets\nClock step: {GadgetTicks:D2}\nHeap: intentionally not host-derived");
        SetStatus("Tp7Demo", $"idle={GadgetTicks}");
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmOpenWindow)
        {
            ProcessedCommandCount++;
            ShowContent(
                "TP7 Demo Command",
                $"Demo command window\nProcessed exactly once: {ProcessedCommandCount}\nExisting Desktop and TWindow path");
            SetStatus("Tp7Demo", $"command={ProcessedCommandCount}");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmShowHelp)
        {
            ProcessedCommandCount++;
            ShowContent(
                "TP7 Demo Help",
                "TP7 Demo help\nMenus, commands, windows and bounded idle gadgets\nKeyboard path remains primary");
            SetStatus("Tp7Demo", "help context visible");
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }
}
