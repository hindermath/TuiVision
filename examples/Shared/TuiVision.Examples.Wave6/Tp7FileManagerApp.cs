// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;

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

        // In schmalen Terminals bleiben die bedienbaren Auswege wichtiger als der vollständige Zustandspräfix.
        // In narrow terminals, actionable escape paths take priority over the complete state prefix.
        string visibleMessage = Size.X < 60
            ? $"F1 Description | Ctrl+Q Quit | {Message.Replace("Tp7FileManager: ", string.Empty, StringComparison.Ordinal)}"
            : Message;
        string text = visibleMessage.Length <= Size.X ? visibleMessage : visibleMessage[..Size.X];
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
    /// <summary>Copy-Dialog öffnen. / Open the copy dialog.</summary>
    public const ushort CmCopyDialog = 33515;
    /// <summary>Rename-Dialog öffnen. / Open the rename dialog.</summary>
    public const ushort CmRenameDialog = 33516;
    /// <summary>Delete-Dialog öffnen. / Open the delete dialog.</summary>
    public const ushort CmDeleteDialog = 33517;
    /// <summary>Read-only-Dialog öffnen. / Open the read-only dialog.</summary>
    public const ushort CmReadOnlyDialog = 33518;

    private readonly bool _headless;
    private readonly Queue<TEvent> _events = [];
    private readonly HashSet<string> _taggedPaths = new(StringComparer.Ordinal);
    private Wave6ShowcaseWindow? _mainWindow;
    private bool _mouseDragActive;
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
    /// <summary>Letzter fokussierter View-Typ. / Last focused view type.</summary>
    public string LastFocusedComponentKind { get; private set; } = string.Empty;
    /// <summary>Persistentes Hauptfenster für den App-Loop-Nachweis. / Persistent main window for app-loop proof.</summary>
    public TWindow? MainWindow => _mainWindow;
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
    /// <summary>Letzter Dialog-Preview. / Last dialog preview.</summary>
    public string LastDialogPreview { get; private set; } = string.Empty;
    /// <summary>Initialer Dialogfokus. / Initial dialog focus.</summary>
    public string LastDialogInitialFocusKind { get; private set; } = string.Empty;
    /// <summary>Terminaler Dialog-Command. / Terminal dialog command.</summary>
    public ushort LastDialogTerminalCommand { get; private set; }
    /// <summary>Ob die Dialogvalidierung ablehnte. / Whether dialog validation rejected input.</summary>
    public bool LastDialogValidationRejected { get; private set; }
    /// <summary>Ob eine begrenzte Mausgeste aktiv ist. / Whether a bounded mouse gesture is active.</summary>
    public bool MouseDragActive => _mouseDragActive;
    /// <summary>Ob MouseUp eine bestätigungspflichtige Absicht vorbereitet hat. / Whether MouseUp prepared a confirmable intent.</summary>
    public bool MouseReleasePrepared { get; private set; }
    /// <summary>Letzter Maus-Abbruchgrund. / Last mouse cancellation reason.</summary>
    public string LastMouseCancellationReason { get; private set; } = string.Empty;

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
            Menu = BuildMenu()
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
            if (_mouseDragActive)
            {
                // Shutdown beendet nur die Geste; die bereits vorbereitete Absicht bleibt für den bestehenden Proof sichtbar.
                // Shutdown ends only the gesture; the already prepared intent remains visible for the existing proof.
                CancelMouseDrag("shutdown", clearPending: false, render: false);
            }

            QuitIssued = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Broadcast
            && @event.Message.Command == ShellCommandIds.cmMouseCapabilityChanged
            && @event.Message.Info is ConsoleMouseCapabilityState capability
            && capability != ConsoleMouseCapabilityState.Enabled
            && _mouseDragActive)
        {
            CancelMouseDrag("capability-loss", clearPending: true, render: true);
            @event.Clear();
            return;
        }

        bool escapeKey = @event.What == TEventKind.KeyDown
            && (@event.KeyDown.ScanCode == 0x01 || @event.KeyDown.CharCode == '\x1b');
        if (escapeKey && _mouseDragActive)
        {
            CancelMouseDrag("escape", clearPending: true, render: true);
            @event.Clear();
            return;
        }

        bool descriptionKey = @event.What == TEventKind.KeyDown && @event.KeyDown.ScanCode == 0x3B;
        if (descriptionKey || (@event.What == TEventKind.Command && @event.Message.Command == CmDescription))
        {
            DescriptionOpened = true;
            ShowState(
                "Help -> Description",
                "kontrollierte Wurzel: TVFM.\n"
                + "Historical purpose: controlled root.\n"
                + "F1 Description | Ctrl+Q Quit.\n"
                + "Proof: app.Run(), view, focus, cells.\n"
                + "Moderne C#-Komposition.\n"
                + "Modern C# composition.\n"
                + "Platform: text-first fallback.\n"
                + "Keine persönlichen Dateien, Shells oder externen Viewer.\n"
                + "No personal files, shells, or external viewers.");
            @event.Clear();
            return;
        }

        if (@event.What != TEventKind.Command)
        {
            if ((@event.What & TEventKind.Mouse) != 0 && HandleMouseIntent(@event))
            {
                return;
            }

            bool selectionNavigation = @event.What == TEventKind.KeyDown
                && @event.KeyDown.ScanCode is 0x47 or 0x48 or 0x4F or 0x50 or 0x51;
            base.HandleEvent(@event);
            if (selectionNavigation && _mainWindow?.SelectedEntry is Wave6DirectoryEntry selected)
            {
                string status = $"selected path={selected.RelativePath}";
                _mainWindow.RefreshSelectionStatus(status);
                LastFocusedComponentKind = nameof(TListBox);
                SetStatus(status);
            }

            return;
        }

        if (@event.Message.Command == ShellCommandIds.cmQuit && _mouseDragActive)
        {
            CancelMouseDrag("shutdown", clearPending: false, render: false);
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
                using (CancellationTokenSource cancellation = new())
                {
                    if (@event.Message.Info is string searchMode
                        && searchMode.Equals("cancel", StringComparison.Ordinal))
                    {
                        cancellation.Cancel();
                    }

                    LastSearch = Workspace.Search(CurrentSnapshot.RelativeDirectory, "*.txt", cancellation.Token);
                }

                ShowState("TP7 TVFM - Search", LastSearch.Status + "\n" + string.Join('\n', LastSearch.Matches));
                break;
            case CmPreviewAssociated:
                PreviewAssociated();
                break;
            case CmPrepareCopy:
                if (TryRunDialogRequest(@event.Message.Info))
                {
                    break;
                }

                PrepareFirstCopy("copy intent");
                break;
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
                if (@event.Message.Info is string resourceName)
                {
                    bool known = Enum.TryParse(resourceName, ignoreCase: true, out Wave6Palette selected)
                        && Enum.IsDefined(selected);
                    Palette = known ? selected : Wave6Palette.Default;
                    ShowState(
                        "TP7 TVFM - Palette",
                        known
                            ? $"palette={Palette}\n{FormatSnapshot()}"
                            : $"palette fallback=Default resource={resourceName}\n{FormatSnapshot()}");
                    break;
                }

                Palette = (Wave6Palette)(((int)Palette + 1) % Enum.GetValues<Wave6Palette>().Length);
                ShowState("TP7 TVFM - Palette", $"palette={Palette}\n{FormatSnapshot()}");
                break;
            case CmCopyDialog:
                RunOperationDialog(Wave6OperationKind.Copy, null);
                break;
            case CmRenameDialog:
                RunOperationDialog(Wave6OperationKind.Rename, null);
                break;
            case CmDeleteDialog:
                RunOperationDialog(Wave6OperationKind.Delete, null);
                break;
            case CmReadOnlyDialog:
                RunOperationDialog(Wave6OperationKind.SetReadOnly, null);
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
        Wave6DirectoryEntry? file = SelectedFile();
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
        Wave6DirectoryEntry? file = SelectedFile();
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

    private bool PrepareFirstCopy(string source)
    {
        Wave6DirectoryEntry? file = SelectedFile();
        if (file is not Wave6DirectoryEntry entry)
        {
            ShowState("TP7 TVFM - Operation", "no file available");
            return false;
        }

        string target = entry.RelativePath + ".copy";
        PendingOperation = Workspace.PrepareOperation(Wave6OperationKind.Copy, entry.RelativePath, target);
        ShowState("TP7 TVFM - Operation", $"{source}: {entry.RelativePath} -> {target}; confirm or cancel");
        return true;
    }

    private bool HandleMouseIntent(TEvent @event)
    {
        bool targetAvailable = _mainWindow is not null
            && ReferenceEquals(_mainWindow.Owner, Desktop)
            && LastVisibleRegion.Contains(@event.Mouse.Where);

        if (@event.What == TEventKind.MouseDown)
        {
            if (@event.Mouse.Buttons != TMouseButtons.Left || !targetAvailable)
            {
                return false;
            }

            MouseReleasePrepared = false;
            LastMouseCancellationReason = string.Empty;
            try
            {
                _mouseDragActive = PrepareFirstCopy("mouse drag/drop intent");
            }
            catch (Exception exception) when (IsExpectedOperationException(exception))
            {
                CancelMouseDrag("invalid-source", clearPending: true, render: true);
            }

            @event.Clear();
            return true;
        }

        if (!_mouseDragActive)
        {
            return false;
        }

        // Mausereignisse besitzen keine Schreibautorität; erst der vorhandene Confirm-Pfad darf Execute aufrufen.
        // Mouse events carry no write authority; only the existing Confirm path may call Execute.
        if (@event.What == TEventKind.MouseMove)
        {
            if (@event.Mouse.Buttons != TMouseButtons.Left || !targetAvailable)
            {
                CancelMouseDrag("invalid-target", clearPending: true, render: true);
            }

            @event.Clear();
            return true;
        }

        if (@event.What == TEventKind.MouseUp)
        {
            if (!targetAvailable)
            {
                CancelMouseDrag("invalid-target", clearPending: true, render: true);
            }
            else
            {
                _mouseDragActive = false;
                MouseReleasePrepared = true;
                LastMouseCancellationReason = string.Empty;
                ShowState(
                    "TP7 TVFM - Drag",
                    "mouse release prepared confirmation; no mutation before Confirm");
            }

            @event.Clear();
            return true;
        }

        return false;
    }

    private void CancelMouseDrag(string reason, bool clearPending, bool render)
    {
        _mouseDragActive = false;
        MouseReleasePrepared = false;
        LastMouseCancellationReason = reason;
        if (clearPending)
        {
            PendingOperation = null;
        }

        if (render)
        {
            ShowState("TP7 TVFM - Drag", $"mouse canceled={reason}; NoMutation");
        }
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

    private bool TryRunDialogRequest(object? info)
    {
        if (info is not string request || !request.StartsWith("dialog:", StringComparison.Ordinal))
        {
            return false;
        }

        string[] parts = request.Split(':', 3, StringSplitOptions.None);
        if (parts.Length < 2)
        {
            return false;
        }

        Wave6OperationKind? kind = parts[1] switch
        {
            "copy" => Wave6OperationKind.Copy,
            "rename" => Wave6OperationKind.Rename,
            "delete" => Wave6OperationKind.Delete,
            "readonly" => Wave6OperationKind.SetReadOnly,
            _ => null
        };
        if (kind is null)
        {
            return false;
        }

        RunOperationDialog(kind.Value, parts.Length == 3 ? parts[2] : null);
        return true;
    }

    private void RunOperationDialog(Wave6OperationKind requestedKind, string? requestedTarget)
    {
        Wave6DirectoryEntry? selected = SelectedFile();
        if (selected is not Wave6DirectoryEntry source || Desktop is null)
        {
            ShowState("TP7 TVFM - Operation", "Rejected: no file selected; NoMutation");
            return;
        }

        Wave6OperationKind kind = requestedKind;
        if (kind == Wave6OperationKind.SetReadOnly && source.ReadOnly)
        {
            kind = Wave6OperationKind.ClearReadOnly;
        }

        string? target = requestedTarget;
        if (kind == Wave6OperationKind.Copy && string.IsNullOrWhiteSpace(target))
        {
            target = source.RelativePath + ".copy";
        }
        else if (kind == Wave6OperationKind.Rename && string.IsNullOrWhiteSpace(target))
        {
            target = Path.GetFileNameWithoutExtension(source.Name)
                + ".renamed"
                + Path.GetExtension(source.Name);
        }

        int width = Math.Clamp(56, 28, Math.Max(28, Desktop.Size.X - 4));
        int height = Math.Clamp(10, 8, Math.Max(8, Desktop.Size.Y - 2));
        int x = Math.Max(0, (Desktop.Size.X - width) / 2);
        int y = Math.Max(0, (Desktop.Size.Y - height) / 2);
        Wave6OperationDialog dialog = new(
            new TRect(x, y, x + width, y + height),
            kind,
            source.RelativePath,
            target);

        // Erst die modale Entscheidung darf eine Workspace-Absicht erzeugen; Cancel besitzt daher nie Schreibautorität.
        // Only the modal decision may create a workspace intent, so Cancel never carries write authority.
        LastOperation = null;
        PendingOperation = null;
        LastDialogPreview = dialog.PreviewText;
        LastDialogInitialFocusKind = dialog.InitialFocusKind;
        LastDialogTerminalCommand = Desktop.ExecuteModal(dialog);
        LastDialogValidationRejected = dialog.ValidationRejected;
        DescriptionOpened |= dialog.HelpRequested;

        if (LastDialogTerminalCommand != ShellCommandIds.cmOK)
        {
            string state = dialog.ValidationRejected ? "Rejected validation" : "Canceled";
            ShowState("TP7 TVFM - Operation", $"{state}; NoMutation\n{dialog.PreviewText}");
            return;
        }

        string? acceptedTarget = dialog.TargetRelativePath;
        if (kind == Wave6OperationKind.Rename
            && !string.IsNullOrEmpty(acceptedTarget)
            && !string.IsNullOrEmpty(Path.GetDirectoryName(source.RelativePath)))
        {
            acceptedTarget = Path.Combine(
                Path.GetDirectoryName(source.RelativePath)!,
                acceptedTarget).Replace('\\', '/');
        }

        try
        {
            PendingOperation = Workspace.PrepareOperation(kind, source.RelativePath, acceptedTarget);
            // Execute revalidiert Quelle und Ziel unmittelbar vor der Mutation und verbraucht die Absicht genau einmal.
            // Execute revalidates source and target immediately before mutation and consumes the intent exactly once.
            LastOperation = Workspace.Execute(PendingOperation, confirmed: true);
            PendingOperation = null;
            CurrentSnapshot = Workspace.List(
                CurrentSnapshot.RelativeDirectory,
                CurrentSnapshot.Filter,
                CurrentSnapshot.Sort,
                CurrentSnapshot.Descending);
            ShowState(
                "TP7 TVFM - Operation",
                $"state={LastOperation.State} progress={LastOperation.ProgressPercent} "
                + $"recovery={LastOperation.RecoveryBoundary}\n{FormatSnapshot()}");
        }
        catch (Exception exception) when (IsExpectedOperationException(exception))
        {
            PendingOperation = null;
            LastOperation = null;
            ShowState(
                "TP7 TVFM - Operation",
                $"Rejected: {exception.GetType().Name}; NoMutation\n{dialog.PreviewText}");
        }
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
        if (_mainWindow is null)
        {
            _mainWindow = new Wave6ShowcaseWindow(width, height);
            Desktop.Insert(_mainWindow);
        }

        _mainWindow.Refresh(CurrentSnapshot, title, text, _taggedPaths);
        Desktop.SetFocus(_mainWindow);
        _mainWindow.SetFocus(_mainWindow.FileList);
        LastVisibleComponentKind = nameof(TWindow);
        LastFocusedComponentKind = nameof(TListBox);
        TRect bounds = _mainWindow.GetBounds();
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

    private Wave6DirectoryEntry? SelectedFile()
    {
        Wave6DirectoryEntry? selected = _mainWindow?.SelectedEntry;
        return selected is { Kind: Wave6EntryKind.File }
            ? selected
            : CurrentSnapshot.Entries.FirstOrDefault(entry => entry.Kind == Wave6EntryKind.File);
    }

    private static TMenuItem BuildMenu()
    {
        TMenuItem help = new(
            "~H~elp",
            0,
            subMenu: new TMenuItem("~D~escription", CmDescription));
        TMenuItem options = new(
            "~O~ptions",
            0,
            help,
            new TMenuItem(
                "~P~alette",
                CmChangePalette,
                new TMenuItem("~T~ag selected", CmTagFirst)));
        TMenuItem search = new(
            "~S~earch",
            0,
            options,
            new TMenuItem(
                "~F~ilter text",
                CmFilterText,
                new TMenuItem(
                    "~S~ort by size",
                    CmSortSize,
                    new TMenuItem("~F~ind text", CmSearchText))));
        TMenuItem view = new(
            "~V~iew",
            0,
            search,
            new TMenuItem(
                "~T~ext",
                CmPreviewText,
                new TMenuItem(
                    "~H~ex",
                    CmPreviewHex,
                    new TMenuItem("~A~ssociated", CmPreviewAssociated))));
        TMenuItem navigate = new(
            "~N~avigate",
            0,
            view,
            new TMenuItem("~F~irst directory", CmNavigateFirstDirectory));
        return new TMenuItem(
            "~F~ile",
            0,
            navigate,
            new TMenuItem(
                "~C~opy...",
                CmCopyDialog,
                new TMenuItem(
                    "~R~ename...",
                    CmRenameDialog,
                    new TMenuItem(
                        "~D~elete...",
                        CmDeleteDialog,
                        new TMenuItem(
                            "Read-~o~nly...",
                            CmReadOnlyDialog,
                            new TMenuItem(
                                "~P~repare copy",
                                CmPrepareCopy,
                                new TMenuItem(
                                    "~C~onfirm",
                                    CmConfirmOperation,
                                    new TMenuItem(
                                        "C~a~ncel",
                                        CmCancelOperation,
                                        new TMenuItem("E~x~it", ShellCommandIds.cmQuit)))))))));
    }

    private static bool IsExpectedOperationException(Exception exception) =>
        exception is ArgumentException
            or IOException
            or InvalidOperationException
            or NotSupportedException
            or UnauthorizedAccessException;

    private static string DisplayPath(string path) => string.IsNullOrEmpty(path) ? "." : path;
}
