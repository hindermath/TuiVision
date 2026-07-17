// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave6;

internal sealed class Wave6StatusLine : TStatusLine
{
    public Wave6StatusLine(TRect bounds) : base(bounds)
    {
    }

    public string Message { get; private set; } = "Wave 6 TVFM | F1 Description | Ctrl+Q Quit";

    public void SetMessage(string message)
    {
        Message = message;
        DrawView();
    }

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0)
        {
            return;
        }

        for (int x = 0; x < Size.X; x++)
        {
            buffer.TrySetCell(Origin.X + x, Origin.Y, new TConsoleCell(' ', ConsoleColor.Black, ConsoleColor.Cyan));
        }

        string text = Message.Length <= Size.X ? Message : Message[..Size.X];
        buffer.WriteText(Origin.X, Origin.Y, text.AsSpan(), ConsoleColor.Yellow, ConsoleColor.Cyan);
    }
}

/// <summary>
/// Funktionale Lernanwendung für die historische TVFM-Absicht.
///
/// Functional learning application for the historical TVFM intent.
/// </summary>
public sealed class Tp7FileManagerApp : TApplication
{
    /// <summary>Navigation zum ersten Unterverzeichnis. / Navigation to the first subdirectory.</summary>
    public const ushort CmNavigateFirstDirectory = 33501;
    /// <summary>Textvorschau der ersten Datei. / Text preview of the first file.</summary>
    public const ushort CmPreviewText = 33502;
    /// <summary>Description öffnen. / Open Description.</summary>
    public const ushort CmDescription = 33503;
    /// <summary>Hexvorschau der ersten Datei. / Hex preview of the first file.</summary>
    public const ushort CmPreviewHex = 33504;
    /// <summary>Textdateien filtern. / Filter text files.</summary>
    public const ushort CmFilterText = 33505;
    /// <summary>Sortierung wechseln. / Change sort order.</summary>
    public const ushort CmSortSize = 33506;
    /// <summary>Ersten Eintrag markieren. / Tag first entry.</summary>
    public const ushort CmTagFirst = 33507;
    /// <summary>Textdateien suchen. / Search text files.</summary>
    public const ushort CmSearchText = 33508;
    /// <summary>Zugeordneten Viewer wählen. / Select associated viewer.</summary>
    public const ushort CmPreviewAssociated = 33509;
    /// <summary>Kopierabsicht vorbereiten. / Prepare copy intent.</summary>
    public const ushort CmPrepareCopy = 33510;
    /// <summary>Vorbereitete Aktion bestätigen. / Confirm prepared action.</summary>
    public const ushort CmConfirmOperation = 33511;
    /// <summary>Vorbereitete Aktion abbrechen. / Cancel prepared action.</summary>
    public const ushort CmCancelOperation = 33512;
    /// <summary>Tastatur-Drag/Drop-Absicht. / Keyboard drag/drop intent.</summary>
    public const ushort CmKeyboardDropIntent = 33513;
    /// <summary>Palette wechseln. / Change palette.</summary>
    public const ushort CmChangePalette = 33514;

    private readonly bool _headless;
    private readonly Queue<TEvent> _events = [];
    private readonly HashSet<string> _taggedPaths = new(StringComparer.Ordinal);
    private TView? _visibleView;
    private bool _realStatusLineCreated;

    /// <summary>Initialisiert die Anwendung. / Initializes the application.</summary>
    public Tp7FileManagerApp(ControlledFileWorkspace workspace, TRect bounds, bool headless) : base(bounds)
    {
        Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        _headless = headless;
        CurrentSnapshot = Workspace.List();
        ShowState("TP7 TVFM", CurrentSnapshot.Status);
    }

    /// <summary>Kontrollierter Workspace. / Controlled workspace.</summary>
    public ControlledFileWorkspace Workspace { get; }
    /// <summary>Letzter Snapshot. / Last snapshot.</summary>
    public Wave6DirectorySnapshot CurrentSnapshot { get; private set; }
    /// <summary>Letzte Vorschau. / Last preview.</summary>
    public Wave6PreviewResult? LastPreview { get; private set; }
    /// <summary>Letzte Suche. / Last search.</summary>
    public Wave6SearchResult? LastSearch { get; private set; }
    /// <summary>Vorbereitete Aktion. / Prepared action.</summary>
    public Wave6OperationIntent? PendingOperation { get; private set; }
    /// <summary>Letztes Aktionsergebnis. / Last operation result.</summary>
    public Wave6OperationResult? LastOperation { get; private set; }
    /// <summary>Aktuelle Lernpalette. / Current learning palette.</summary>
    public Wave6Palette Palette { get; private set; }
    /// <summary>Letzter sichtbarer View-Typ. / Last visible view type.</summary>
    public string LastVisibleComponentKind { get; private set; } = string.Empty;
    /// <summary>Letzte Proof-Region. / Last proof region.</summary>
    public TRect LastVisibleRegion { get; private set; }
    /// <summary>Letzter sichtbarer Status. / Last visible status.</summary>
    public string LastStatusMessage { get; private set; } = string.Empty;
    /// <summary>Ob Description geöffnet wurde. / Whether Description was opened.</summary>
    public bool DescriptionOpened { get; private set; }
    /// <summary>Ob kontrollierter Quit erzeugt wurde. / Whether controlled quit was issued.</summary>
    public bool QuitIssued { get; private set; }
    /// <summary>Ob eine echte Statuszeile vorhanden ist. / Whether a real status line is present.</summary>
    public bool HasRealStatusLine => _realStatusLineCreated;
    /// <summary>Markierte relative Pfade. / Tagged relative paths.</summary>
    public IReadOnlyCollection<string> TaggedPaths => _taggedPaths;

    /// <summary>Fügt Testereignisse hinzu. / Adds test events.</summary>
    public void QueueEvents(IEnumerable<TEvent> events)
    {
        foreach (TEvent @event in events)
        {
            _events.Enqueue(@event);
        }
    }

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) =>
        new(bounds)
        {
            Menu = new TMenuItem(
                "~D~atei / ~F~ile",
                0,
                new TMenuItem(
                    "~A~nsicht / ~V~iew",
                    0,
                    new TMenuItem(
                        "~H~ilfe / ~H~elp",
                        0,
                        subMenu: new TMenuItem("~B~eschreibung / ~D~escription", CmDescription)),
                    new TMenuItem("~T~ext", CmPreviewText, new TMenuItem("~H~ex", CmPreviewHex))),
                new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit))
        };

    /// <inheritdoc />
    protected override TStatusLine InitStatusLine(TRect bounds)
    {
        _realStatusLineCreated = true;
        return new Wave6StatusLine(bounds);
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && _events.Count > 0)
        {
            @event = _events.Dequeue();
            return;
        }

        if (_headless && !QuitIssued)
        {
            QuitIssued = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        bool descriptionKey = @event.What == TEventKind.KeyDown && @event.KeyDown.ScanCode == 0x3B;
        if (descriptionKey || (@event.What == TEventKind.Command && @event.Message.Command == CmDescription))
        {
            DescriptionOpened = true;
            ShowState(
                "Help -> Description",
                "kontrollierte Wurzel: TVFM-Lernzweck.\n"
                + "Historical learning purpose: TVFM inside a controlled root.\n"
                + "Tastatur / Keyboard: commands, F1 Description, Ctrl+Q Quit.\n"
                + "Keine persönlichen Dateien, Shells oder externen Viewer.");
            @event.Clear();
            return;
        }

        if (@event.What != TEventKind.Command)
        {
            if (@event.What == TEventKind.MouseDown && @event.Mouse.Buttons.HasFlag(TMouseButtons.Left))
            {
                PrepareFirstCopy("mouse drag/drop intent");
                @event.Clear();
                return;
            }

            base.HandleEvent(@event);
            return;
        }

        switch (@event.Message.Command)
        {
            case CmNavigateFirstDirectory:
                NavigateFirstDirectory();
                break;
            case CmPreviewText:
                PreviewFirstFile(Wave6ViewerDecision.Text);
                break;
            case CmPreviewHex:
                PreviewFirstFile(Wave6ViewerDecision.Hex);
                break;
            case CmFilterText:
                CurrentSnapshot = Workspace.List(CurrentSnapshot.RelativeDirectory, "*.txt");
                ShowState("TP7 TVFM - Filter", FormatSnapshot());
                break;
            case CmSortSize:
                CurrentSnapshot = Workspace.List(CurrentSnapshot.RelativeDirectory, CurrentSnapshot.Filter, Wave6Sort.Size);
                ShowState("TP7 TVFM - Sort", FormatSnapshot());
                break;
            case CmTagFirst:
                TagFirst();
                break;
            case CmSearchText:
                LastSearch = Workspace.Search(CurrentSnapshot.RelativeDirectory, "*.txt");
                ShowState("TP7 TVFM - Search", LastSearch.Status + "\n" + string.Join('\n', LastSearch.Matches));
                break;
            case CmPreviewAssociated:
                PreviewAssociated();
                break;
            case CmPrepareCopy:
            case CmKeyboardDropIntent:
                PrepareFirstCopy(@event.Message.Command == CmKeyboardDropIntent ? "keyboard drop intent" : "copy intent");
                break;
            case CmConfirmOperation:
                CompletePending(confirmed: true);
                break;
            case CmCancelOperation:
                CompletePending(confirmed: false);
                break;
            case CmChangePalette:
                Palette = (Wave6Palette)(((int)Palette + 1) % Enum.GetValues<Wave6Palette>().Length);
                ShowState("TP7 TVFM - Palette", $"palette={Palette}\n{FormatSnapshot()}");
                break;
            default:
                base.HandleEvent(@event);
                return;
        }

        @event.Clear();
    }

    private void NavigateFirstDirectory()
    {
        Wave6DirectoryEntry? directory = CurrentSnapshot.Entries
            .Where(entry => entry.Kind == Wave6EntryKind.Directory)
            .OrderByDescending(entry => entry.Name.Equals("docs", StringComparison.OrdinalIgnoreCase))
            .ThenBy(entry => entry.Name, StringComparer.Ordinal)
            .FirstOrDefault();
        if (directory is Wave6DirectoryEntry target && !string.IsNullOrEmpty(target.RelativePath))
        {
            CurrentSnapshot = Workspace.List(target.RelativePath);
            ShowState("TP7 TVFM - Directory", FormatSnapshot());
            return;
        }

        ShowState("TP7 TVFM - Directory", "no subdirectory available");
    }

    private void PreviewFirstFile(Wave6ViewerDecision decision)
    {
        Wave6DirectoryEntry? file = CurrentSnapshot.Entries.FirstOrDefault(entry => entry.Kind == Wave6EntryKind.File);
        if (file is not Wave6DirectoryEntry target || string.IsNullOrEmpty(target.RelativePath))
        {
            ShowState("TP7 TVFM - Preview", "no file available");
            return;
        }

        LastPreview = decision == Wave6ViewerDecision.Hex
            ? Workspace.PreviewHex(target.RelativePath)
            : Workspace.PreviewText(target.RelativePath);
        ShowState($"TP7 TVFM - {decision}", LastPreview.Status + "\n" + LastPreview.Content);
    }

    private void PreviewAssociated()
    {
        Wave6DirectoryEntry? file = CurrentSnapshot.Entries.FirstOrDefault(entry => entry.Kind == Wave6EntryKind.File);
        if (file is not Wave6DirectoryEntry target)
        {
            ShowState("TP7 TVFM - Association", "no file available");
            return;
        }

        Wave6ViewerDecision decision = Workspace.DecideViewer(target.RelativePath);
        if (decision == Wave6ViewerDecision.Fallback)
        {
            ShowState("TP7 TVFM - Association", $"fallback path={target.RelativePath}; choose Text or Hex");
            return;
        }

        LastPreview = decision == Wave6ViewerDecision.Text
            ? Workspace.PreviewText(target.RelativePath)
            : Workspace.PreviewHex(target.RelativePath);
        ShowState("TP7 TVFM - Association", LastPreview.Status + "\n" + LastPreview.Content);
    }

    private void TagFirst()
    {
        Wave6DirectoryEntry? first = CurrentSnapshot.Entries.FirstOrDefault();
        if (first is not Wave6DirectoryEntry entry)
        {
            ShowState("TP7 TVFM - Tag", "no entry available");
            return;
        }

        if (!_taggedPaths.Add(entry.RelativePath))
        {
            _taggedPaths.Remove(entry.RelativePath);
        }

        ShowState("TP7 TVFM - Tag", $"tagged={string.Join(',', _taggedPaths.Order(StringComparer.Ordinal))}\n{FormatSnapshot()}");
    }

    private void PrepareFirstCopy(string source)
    {
        Wave6DirectoryEntry? file = CurrentSnapshot.Entries.FirstOrDefault(entry => entry.Kind == Wave6EntryKind.File);
        if (file is not Wave6DirectoryEntry entry)
        {
            ShowState("TP7 TVFM - Operation", "no file available");
            return;
        }

        string target = entry.RelativePath + ".copy";
        PendingOperation = Workspace.PrepareOperation(Wave6OperationKind.Copy, entry.RelativePath, target);
        ShowState("TP7 TVFM - Operation", $"{source}: {entry.RelativePath} -> {target}; confirm or cancel");
    }

    private void CompletePending(bool confirmed)
    {
        if (PendingOperation is null)
        {
            ShowState("TP7 TVFM - Operation", "no pending operation");
            return;
        }

        LastOperation = Workspace.Execute(PendingOperation, confirmed);
        PendingOperation = null;
        CurrentSnapshot = Workspace.List(CurrentSnapshot.RelativeDirectory, CurrentSnapshot.Filter, CurrentSnapshot.Sort);
        ShowState(
            "TP7 TVFM - Operation",
            $"state={LastOperation.State} progress={LastOperation.ProgressPercent} recovery={LastOperation.RecoveryBoundary}\n"
            + FormatSnapshot());
    }

    private string FormatSnapshot()
    {
        IEnumerable<string> rows = CurrentSnapshot.Entries.Select(entry =>
            $"{(entry.Kind == Wave6EntryKind.Directory ? "[D]" : "[F]")} {entry.RelativePath}"
            + (entry.Size is long size ? $" {size} bytes" : string.Empty));
        return $"Root: {Workspace.RootPath}\nPath: {DisplayPath(CurrentSnapshot.RelativeDirectory)}\n"
            + string.Join('\n', rows.DefaultIfEmpty("<empty>"));
    }

    private void ShowState(string title, string text)
    {
        if (Desktop is null)
        {
            return;
        }

        int width = Math.Clamp(70, 18, Math.Max(18, Desktop.Size.X - 2));
        int height = Math.Clamp(18, 8, Math.Max(8, Desktop.Size.Y - 2));
        TWindow window = new(title, 1, 0, width, height);
        window.Insert(new TStaticText(new TRect(2, 1, Math.Max(3, width - 2), Math.Max(3, height - 1)), text));
        if (_visibleView?.Owner == Desktop)
        {
            Desktop.Remove(_visibleView);
        }

        Desktop.Insert(window);
        Desktop.SetFocus(window);
        _visibleView = window;
        LastVisibleComponentKind = nameof(TWindow);
        TRect bounds = window.GetBounds();
        LastVisibleRegion = new TRect(
            Desktop.Origin.X + bounds.A.X,
            Desktop.Origin.Y + bounds.A.Y,
            Desktop.Origin.X + bounds.B.X,
            Desktop.Origin.Y + bounds.B.Y);
        SetStatus(text.Split('\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? title);
    }

    private void SetStatus(string state)
    {
        LastStatusMessage = $"Tp7FileManager: {state} | F1 Description | Ctrl+Q Quit";
        if (StatusLine is Wave6StatusLine status)
        {
            status.SetMessage(LastStatusMessage);
        }
    }

    private static string DisplayPath(string path) => string.IsNullOrEmpty(path) ? "." : path;
}
