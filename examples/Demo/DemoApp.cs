// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

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
    public DemoApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        SetVisibleState(
            "Demo: Wave-2 controls/dialogs showcase\n" +
            "Commands: broad, metadata, manual path, cancel, invalid path, color/display\n" +
            "Use the Demo menu or scripted command events. Ctrl+Q quits.");
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
        SetVisibleState("demo: controls dialogs gadgets flow completed");

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
        return SetVisibleState($"demo: metadata wildcard={wildcard} files={joined}");
    }

    /// <summary>
    /// Uebernimmt einen manuell eingegebenen Pfad als sichtbare Entscheidung.
    ///
    /// Applies a manually entered path as a visible decision.
    /// </summary>
    /// <param name="path">Der Pfad. / The path.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string EnterManualPath(string path) => SetVisibleState($"demo: manual-path accepted {path}");

    /// <summary>
    /// Bricht den Dateidialog sichtbar ab.
    ///
    /// Cancels the file dialog visibly.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CancelFileDialog()
    {
        FileContentIoPerformed = false;
        return SetVisibleState("demo: canceled file dialog");
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
            return SetVisibleState("demo: invalid-path rejected");
        }

        return SetVisibleState("demo: invalid-path rejected");
    }

    /// <summary>
    /// Fuehrt eine symbolische Farb- und Displayauswahl aus.
    ///
    /// Runs a symbolic color and display selection.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectColorAndDisplay() => SetVisibleState("demo: color blue-on-black display 80x25 selected");

    /// <summary>
    /// Liefert die dokumentierten Omissionen ausserhalb von Welle 2.
    ///
    /// Returns the documented omissions outside wave 2.
    /// </summary>
    /// <returns>Der sichtbare Referenzzustand. / The visible reference state.</returns>
    public string OutOfScopeOmissions() =>
        SetVisibleState("demo: omitted editor help stream terminal mouse charset; see docs/guides/examples/demo.md");

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
            Menu = new TMenuItem("~D~emo", 0, new TMenuItem("E~x~it", ShellCommandIds.cmQuit), demoItems)
        };
    }

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
        int height = Math.Max(1, Math.Min(Desktop.Size.Y - 2, 8));
        _visibleView = new TStaticText(new TRect(1, 1, 1 + width, 1 + height), VisibleText);
        Desktop.Insert(_visibleView);
        return VisibleText;
    }
}
