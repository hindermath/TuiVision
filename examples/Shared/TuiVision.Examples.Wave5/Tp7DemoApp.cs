// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave5;

/// <summary>Funktionales TP7-Hauptdemobeispiel. / Functional TP7 main demo example.</summary>
public sealed class Tp7DemoApp : Wave5Application
{
    private readonly List<TWindow> _windows = [];

    /// <summary>Öffnet ein Demo-Fenster. / Opens a demo window.</summary>
    public const ushort CmOpenWindow = 32101;

    /// <summary>Zeigt den Help-Kontext. / Shows the help context.</summary>
    public const ushort CmShowHelp = 32102;

    /// <summary>Kachelt die Demo-Fenster. / Tiles the demo windows.</summary>
    public const ushort CmTile = 32103;

    /// <summary>Kaskadiert die Demo-Fenster. / Cascades the demo windows.</summary>
    public const ushort CmCascade = 32104;

    /// <summary>Wählt das nächste Demo-Fenster. / Selects the next demo window.</summary>
    public const ushort CmNextWindow = 32105;

    /// <summary>Schließt das aktive Demo-Fenster. / Closes the active demo window.</summary>
    public const ushort CmCloseWindow = 32106;

    /// <summary>Initialisiert die Demo. / Initializes the demo.</summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Kontrollierter Smoke-Modus. / Controlled smoke mode.</param>
    public Tp7DemoApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        OpenWindow("TP7 Demo Overview", "TP7 Demo\nApplication, commands and real windows");
        OpenWindow("TP7 Demo Gadgets", "Deterministic clock and heap-style learning feedback");
        SetStatus("Tp7Demo", "ready");
    }

    /// <summary>Anzahl verarbeiteter Demo-Commands. / Number of processed demo commands.</summary>
    public int ProcessedCommandCount { get; private set; }

    /// <summary>Anzahl begrenzter Idle-Zyklen. / Number of bounded idle cycles.</summary>
    public int GadgetTicks { get; private set; }

    /// <summary>Letzte Zahl sichtbarer Demo-Fenster. / Last number of visible demo windows.</summary>
    public int LastWindowCount { get; private set; }

    /// <summary>Letzte strukturierte Desktop-Operation. / Last structured desktop operation.</summary>
    public TDesktopOperationKind LastDesktopOperation { get; private set; } = TDesktopOperationKind.Insert;

    /// <summary>Letzter fokussierter Fenstertitel. / Last focused window title.</summary>
    public string LastFocusedWindowTitle { get; private set; } = string.Empty;

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) =>
        new(bounds)
        {
            Menu = new TMenuItem(
                "~D~emo",
                0,
                new TMenuItem(
                    "~W~indows",
                    0,
                    new TMenuItem(
                        "~H~ilfe / ~H~elp",
                        0,
                        null,
                        new TMenuItem(
                            "~K~ontext / ~C~ontext",
                            CmShowHelp,
                            new TMenuItem("~B~eschreibung / ~D~escription", CmDescription))),
                    new TMenuItem(
                        "~T~ile",
                        CmTile,
                        new TMenuItem(
                            "~C~ascade",
                            CmCascade,
                            new TMenuItem(
                                "~N~ext",
                                CmNextWindow,
                                new TMenuItem("~C~lose", CmCloseWindow))))),
                new TMenuItem("~O~pen window", CmOpenWindow))
        };

    /// <inheritdoc />
    protected override void Idle()
    {
        GadgetTicks++;
        SetStatus("Tp7Demo", $"idle={GadgetTicks}");
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmOpenWindow)
        {
            ProcessedCommandCount++;
            OpenWindow(
                $"TP7 Demo Command {ProcessedCommandCount}",
                $"Demo command window\nProcessed exactly once: {ProcessedCommandCount}\nExisting Desktop and TWindow path");
            SetStatus("Tp7Demo", $"command={ProcessedCommandCount}");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmTile)
        {
            TDesktopOperationResult result = Desktop!.TileWindows();
            RecordDesktopOperation(result, $"tiled={result.ParticipatingCount}");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmCascade)
        {
            TDesktopOperationResult result = Desktop!.CascadeWindows();
            RecordDesktopOperation(result, $"cascaded={result.ParticipatingCount}");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmNextWindow)
        {
            TDesktopOperationResult result = Desktop!.SelectNextWindow();
            RecordDesktopOperation(result, $"next={LastFocusedWindowTitle}");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmCloseWindow)
        {
            if (Desktop!.GetTopWindow() is TWindow top)
            {
                _ = top.RequestClose(TCloseTrigger.Command);
            }

            UpdateWindowProof();
            SetStatus("Tp7Demo", $"closed; windows={LastWindowCount}");
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

    /// <inheritdoc />
    protected override string BuildDescriptionText() =>
        """
        Historischer Lernzweck: Eine Fensterfamilie zeigt Application-, Desktop- und Command-Zusammenarbeit.
        Historical learning purpose: a window family shows Application, Desktop, and command collaboration.
        Tastatur: Alt+D öffnet ein Fenster; Windows bietet Tile, Cascade, Next und Close; F1 öffnet diese Beschreibung.
        Keyboard: Alt+D opens a window; Windows provides Tile, Cascade, Next, and Close; F1 opens this description.
        Modernes C# verwendet typisierte Desktop-Ergebnisse und deterministische Gadgets statt DOS-Globals.
        Modern C# uses typed desktop results and deterministic gadgets instead of DOS globals.
        Es werden keine Host-Speicherwerte gelesen und keine Dateien oder externen Dienste berührt.
        No host-memory values are read, and no files or external services are touched.
        Der App-Loop-Smoke beweist Fenster, Fokus, Operationen, Status und Zellen, nicht die native Terminal-Z-Reihenfolge.
        The app-loop smoke proves windows, focus, operations, status, and cells, not native terminal z-order.
        """;

    private void OpenWindow(string title, string text)
    {
        int index = _windows.Count;
        int width = Math.Min(34, Math.Max(16, Desktop!.Size.X - 4));
        int height = Math.Min(9, Math.Max(6, Desktop.Size.Y - 2));
        int maxX = Math.Max(1, Desktop.Size.X - width);
        int maxY = Math.Max(1, Desktop.Size.Y - height);
        TWindow window = new(
            title,
            1 + (index % maxX),
            index % maxY,
            width,
            height,
            WindowFlags.Close | WindowFlags.Move);
        window.Options |= TViewOptions.Tileable;
        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, width - 2), Math.Max(3, height - 1)), text));
        _windows.Add(window);
        TDesktopOperationResult result = Desktop.InsertWindow(window);
        LastDesktopOperation = result.Kind;
        TrackVisibleView(window, nameof(TWindow));
        LastVisibleText = text;
        UpdateWindowProof();
    }

    private void RecordDesktopOperation(TDesktopOperationResult result, string status)
    {
        LastDesktopOperation = result.Kind;
        UpdateWindowProof();
        if (result.SelectedView is TWindow selected)
        {
            TrackVisibleView(selected, nameof(TWindow));
        }

        SetStatus("Tp7Demo", status);
    }

    private void UpdateWindowProof()
    {
        LastWindowCount = _windows.Count(window => window.Owner == Desktop);
        if (Desktop?.GetTopWindow() is TWindow focused)
        {
            LastFocusedWindowTitle = focused.Title;
            TrackVisibleView(focused, nameof(TWindow));
        }
    }
}
