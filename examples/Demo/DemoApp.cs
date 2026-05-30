// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.Demo;

/// <summary>
/// Breites Controls- und Dialog-Beispiel mit Headless-Seam.
///
/// Broad controls and dialogs example with a headless seam.
/// </summary>
public sealed class DemoApp : TApplication
{
    /// <summary>
    /// Befehl fuer den breiten Controls/Dialog/Gadget-Fluss.
    ///
    /// Command for the broad controls/dialog/gadget flow.
    /// </summary>
    public const ushort CmBroadFlow = 12010;

    /// <summary>
    /// Befehl fuer Datei-/Pfadmetadaten.
    ///
    /// Command for file/path metadata.
    /// </summary>
    public const ushort CmFileMetadata = 12011;

    /// <summary>
    /// Befehl fuer manuelle Pfadeingabe.
    ///
    /// Command for manual path entry.
    /// </summary>
    public const ushort CmManualPath = 12012;

    /// <summary>
    /// Befehl fuer sichtbaren Abbruch.
    ///
    /// Command for visible cancellation.
    /// </summary>
    public const ushort CmCancelPath = 12013;

    /// <summary>
    /// Befehl fuer ungueltige Pfade.
    ///
    /// Command for invalid paths.
    /// </summary>
    public const ushort CmInvalidPath = 12014;

    /// <summary>
    /// Befehl fuer Farb- und Displayauswahl.
    ///
    /// Command for color and display selection.
    /// </summary>
    public const ushort CmColorDisplay = 12015;

    /// <summary>
    /// Befehl fuer bewusst ausgelassene historische Demo-Bereiche.
    ///
    /// Command for intentionally omitted historical demo areas.
    /// </summary>
    public const ushort CmOmissions = 12016;

    /// <summary>
    /// Befehl fuer Help -> Description.
    ///
    /// Command for Help -> Description.
    /// </summary>
    public const ushort CmDescription = 12017;

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
    public DemoApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "Demo: Wave-2 controls/dialogs showcase\n" +
            "Commands: broad, metadata, manual path, cancel, invalid path, color/display\n" +
            "Use the Demo menu or scripted command events. Ctrl+Q quits.",
            "ready");
    }

    /// <summary>
    /// Anfrage fuer den Dateimetadaten-Befehl.
    ///
    /// Request for the file metadata command.
    /// </summary>
    /// <param name="Directory">Das Verzeichnis. / The directory.</param>
    /// <param name="Wildcard">Der Wildcard-Filter. / The wildcard filter.</param>
    public readonly record struct FileMetadataRequest(string Directory, string Wildcard);

    /// <summary>
    /// Gibt an, ob das Beispiel Dateiinhalt-I/O ausgefuehrt hat.
    ///
    /// Indicates whether the example performed file-content I/O.
    /// </summary>
    public bool FileContentIoPerformed { get; private set; }

    /// <summary>
    /// Letzter sichtbarer Textzustand.
    ///
    /// Last visible text state.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>
    /// Letzter sichtbarer Hauptkomponententyp.
    ///
    /// Last visible main component type.
    /// </summary>
    public string LastVisibleComponentKind { get; private set; } = "TStaticText";

    /// <summary>
    /// Bildschirmbereich der letzten sichtbaren Hauptkomponente.
    ///
    /// Screen region of the last visible main component.
    /// </summary>
    public TRect LastVisibleRegion { get; private set; } = new(0, 0, 0, 0);

    /// <summary>
    /// Letzter Statuszeilentext.
    ///
    /// Last status-line text.
    /// </summary>
    public string LastStatusMessage { get; private set; } = string.Empty;

    /// <summary>
    /// Beschreibungstext fuer Help -> Description.
    ///
    /// Description text for Help -> Description.
    /// </summary>
    public string DescriptionText =>
        "Demo description: visible TuiVision dialogs, file metadata, colour/display state, " +
        "and documented Wave-2 omissions are reachable from the Demo menu.";

    /// <summary>
    /// Sichtbare Zustandsfolge seit Start der Anwendung.
    ///
    /// Visible state sequence since application start.
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
    /// Fuehrt den breiten Controls/Dialog/Gadget-Fluss aus.
    ///
    /// Runs the broad controls/dialog/gadget flow.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RunBroadControlsDialogsGadgetsFlow() =>
        SetVisibleState(
            "demo: controls dialogs gadgets flow completed",
            "controls/dialogs/gadgets visible",
            CreateDialogView("Controls / Dialogs / Gadgets", "controls dialogs gadgets\nOK button and static text"),
            "TDialog");

    /// <summary>
    /// Zeigt Dateisystem-Metadaten und Wildcard-Zustand ohne Dateiinhalt zu lesen.
    ///
    /// Shows file-system metadata and wildcard state without reading file content.
    /// </summary>
    /// <param name="directory">Das Verzeichnis. / The directory.</param>
    /// <param name="wildcard">Der Wildcard-Filter. / The wildcard filter.</param>
    /// <returns>Der sichtbare Dialogzustand. / The visible dialog state.</returns>
    public string InspectStandardFileDialog(string directory, string wildcard)
    {
        FileContentIoPerformed = false;
        string safeDirectory = Directory.Exists(directory) ? directory : Environment.CurrentDirectory;
        string[] names = Directory.EnumerateFiles(safeDirectory, wildcard).Select(Path.GetFileName).OfType<string>().ToArray();
        string joined = names.Length == 0 ? "no-files" : string.Join(",", names);
        string text = $"demo: metadata wildcard={wildcard} files={joined}";
        return SetVisibleState(
            text,
            "file metadata visible",
            CreateDialogView("File metadata", $"wildcard {wildcard}\nfiles {joined}\ncontent not read"),
            "TDialog");
    }

    /// <summary>
    /// Uebernimmt einen manuell eingegebenen Pfad als sichtbare Entscheidung.
    ///
    /// Applies a manually entered path as a visible decision.
    /// </summary>
    /// <param name="path">Der Pfad. / The path.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string EnterManualPath(string path) =>
        SetVisibleState(
            $"demo: manual-path accepted {path}",
            "manual path accepted",
            CreateDialogView("Manual path", $"manual-path accepted\n{path}"),
            "TDialog");

    /// <summary>
    /// Bricht den Dateidialog sichtbar ab.
    ///
    /// Cancels the file dialog visibly.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CancelFileDialog()
    {
        FileContentIoPerformed = false;
        return SetVisibleState(
            "demo: canceled file dialog",
            "file dialog canceled",
            CreateDialogView("File dialog", "canceled"),
            "TDialog");
    }

    /// <summary>
    /// Lehnt einen ungueltigen Pfad sichtbar ab.
    ///
    /// Visibly rejects an invalid path.
    /// </summary>
    /// <param name="path">Der zu pruefende Pfad. / The path to check.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RejectInvalidPath(string path)
    {
        FileContentIoPerformed = false;
        try
        {
            _ = Path.GetFullPath(path);
        }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException or PathTooLongException)
        {
            return SetVisibleState(
                "demo: invalid-path rejected",
                "invalid path rejected",
                CreateDialogView("File dialog", "invalid-path rejected"),
                "TDialog");
        }

        return SetVisibleState(
            "demo: invalid-path rejected",
            "invalid path rejected",
            CreateDialogView("File dialog", "invalid-path rejected"),
            "TDialog");
    }

    /// <summary>
    /// Fuehrt eine symbolische Farb- und Displayauswahl aus.
    ///
    /// Runs a symbolic color and display selection.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectColorAndDisplay() =>
        SetVisibleState(
            "demo: color blue-on-black display 80x25 selected",
            "color/display visible",
            CreateWindowView("Display / Color", "color blue-on-black\ndisplay 80x25\ngadget state visible"),
            "TWindow");

    /// <summary>
    /// Liefert die dokumentierten Omissionen ausserhalb von Welle 2.
    ///
    /// Returns the documented omissions outside wave 2.
    /// </summary>
    /// <returns>Der sichtbare Referenzzustand. / The visible reference state.</returns>
    public string OutOfScopeOmissions() =>
        SetVisibleState(
            "demo: omitted editor help stream terminal mouse charset; see docs/guides/examples/demo.md",
            "omissions documented",
            CreateWindowView("Wave-2 omissions", "omitted editor help stream\nterminal mouse charset\nsee guide"),
            "TWindow");

    /// <summary>
    /// Zeigt die Laufzeitbeschreibung.
    ///
    /// Shows the runtime description.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ShowDescription() =>
        SetVisibleState(
            DescriptionText,
            "description visible",
            CreateWindowView("Help - Description", DescriptionText),
            "TWindow");

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuItem demoItems =
            new("~B~road controls/dialogs", CmBroadFlow,
            new TMenuItem("~M~etadata", CmFileMetadata,
            new TMenuItem("Manual ~p~ath", CmManualPath,
            new TMenuItem("~C~ancel", CmCancelPath,
            new TMenuItem("~I~nvalid path", CmInvalidPath,
            new TMenuItem("C~o~lor/display", CmColorDisplay,
            new TMenuItem("~O~missions", CmOmissions)))))));

        return new TMenuBar(bounds)
        {
            Menu = new TMenuItem("~D~emo", 0, Wave2Runtime.HelpMenu(CmDescription, new TMenuItem("E~x~it", ShellCommandIds.cmQuit)), demoItems)
        };
    }

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds) =>
        new Wave2StatusLine(bounds, Wave2Runtime.Status("Demo", "ready"));

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command)
        {
            switch (@event.Message.Command)
            {
                case CmBroadFlow:
                    RunBroadControlsDialogsGadgetsFlow();
                    @event.Clear();
                    return;
                case CmFileMetadata:
                    FileMetadataRequest request = @event.Message.Info is FileMetadataRequest metadataRequest
                        ? metadataRequest
                        : new FileMetadataRequest(Environment.CurrentDirectory, "*");
                    InspectStandardFileDialog(request.Directory, request.Wildcard);
                    @event.Clear();
                    return;
                case CmManualPath:
                    EnterManualPath(@event.Message.Info as string ?? "examples/Demo");
                    @event.Clear();
                    return;
                case CmCancelPath:
                    CancelFileDialog();
                    @event.Clear();
                    return;
                case CmInvalidPath:
                    RejectInvalidPath(@event.Message.Info as string ?? "\0invalid");
                    @event.Clear();
                    return;
                case CmColorDisplay:
                    SelectColorAndDisplay();
                    @event.Clear();
                    return;
                case CmOmissions:
                    OutOfScopeOmissions();
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
        LastStatusMessage = Wave2Runtime.Status("Demo", status);
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

        if (mainView is not null)
        {
            _visibleView = new TStaticText(Wave2Runtime.DetailRegion(Desktop), VisibleText);
            Desktop.Insert(_visibleView);
        }
        else
        {
            _visibleView = null;
        }

        return VisibleText;
    }

    private TDialog? CreateDialogView(string title, string body)
    {
        if (Desktop is null)
        {
            return null;
        }

        TDialog dialog = new(Wave2Runtime.MainRegion(Desktop, width: 48, height: 8), title);
        dialog.Insert(new TStaticText(new TRect(2, 2, 44, 5), body));
        dialog.Insert(new TButton(new TRect(16, 6, 30, 7), "~O~K", ShellCommandIds.cmOK, TButtonFlags.bfDefault));
        return dialog;
    }

    private TWindow? CreateWindowView(string title, string body)
    {
        if (Desktop is null)
        {
            return null;
        }

        TRect region = Wave2Runtime.MainRegion(Desktop, width: 54, height: 8);
        TWindow window = new(title, region.A.X, region.A.Y, region.Width, region.Height);
        window.Insert(new TStaticText(new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)), body));
        return window;
    }
}
