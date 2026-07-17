// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Wave6;

internal sealed class Wave6TextPanel : TView
{
    public Wave6TextPanel(TRect bounds) : base(bounds)
    {
    }

    public string Text { get; private set; } = string.Empty;

    public void SetText(string text)
    {
        Text = text ?? string.Empty;
        DrawView();
    }

    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        string[] logicalLines = Text.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        List<string> lines = [];
        foreach (string logicalLine in logicalLines)
        {
            if (logicalLine.Length == 0)
            {
                lines.Add(string.Empty);
                continue;
            }

            // Der lokale Umbruch hält Proof-Inhalte sichtbar; die Domänenausgabe selbst bleibt unverändert und prüfbar.
            // Local wrapping keeps proof content visible while the domain output remains unchanged and verifiable.
            for (int offset = 0; offset < logicalLine.Length; offset += Size.X)
            {
                lines.Add(logicalLine.Substring(offset, Math.Min(Size.X, logicalLine.Length - offset)));
            }
        }

        for (int row = 0; row < Size.Y; row++)
        {
            string line = row < lines.Count ? lines[row] : string.Empty;
            string clipped = line.Length <= Size.X ? line : line[..Size.X];
            buffer.WriteText(Origin.X, Origin.Y + row, clipped.PadRight(Size.X).AsSpan());
        }
    }
}

internal sealed class Wave6ShowcaseWindow : TWindow
{
    private readonly Wave6TextPanel _header;
    private readonly Wave6TextPanel _detail;
    private IReadOnlyList<Wave6DirectoryEntry> _entries = [];

    public Wave6ShowcaseWindow(int width, int height)
        : base("TP7 TVFM", 1, 0, width, height)
    {
        int contentRight = Math.Max(2, width - 1);
        int contentBottom = Math.Max(3, height - 1);
        bool constrained = width < 60 || height < 16;
        int listTop = constrained ? 3 : 4;

        _header = new Wave6TextPanel(new TRect(1, 1, contentRight, listTop));
        Insert(_header);

        if (constrained)
        {
            int detailTop = Math.Clamp(height / 2 + 1, listTop + 2, Math.Max(listTop + 2, contentBottom - 2));
            FileList = new TListBox(new TRect(1, listTop, contentRight, detailTop), 1, null);
            _detail = new Wave6TextPanel(new TRect(1, detailTop, contentRight, contentBottom));
        }
        else
        {
            int split = Math.Clamp(width / 2, 24, contentRight - 18);
            FileList = new TListBox(new TRect(1, listTop, split, contentBottom), 1, null);
            _detail = new Wave6TextPanel(new TRect(split + 1, listTop, contentRight, contentBottom));
        }

        Insert(FileList);
        Insert(_detail);
        SetFocus(FileList);
    }

    public TListBox FileList { get; }

    public Wave6DirectoryEntry? SelectedEntry =>
        FileList.FocusedItem >= 0 && FileList.FocusedItem < _entries.Count
            ? _entries[FileList.FocusedItem]
            : null;

    public void Refresh(
        Wave6DirectorySnapshot snapshot,
        string title,
        string detail,
        IReadOnlyCollection<string> taggedPaths)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(detail);
        ArgumentNullException.ThrowIfNull(taggedPaths);

        string? selectedPath = SelectedEntry?.RelativePath;
        _entries = snapshot.Entries;
        IEnumerable<string> rows = _entries.Select(entry =>
            $"{(taggedPaths.Contains(entry.RelativePath) ? "*" : " ")}"
            + $"{(entry.Kind == Wave6EntryKind.Directory ? "[D]" : "[F]")} {entry.Name}"
            + (entry.Size is long size ? $" {size} B" : string.Empty));
        FileList.List = new TStringList(rows.DefaultIfEmpty("<empty>"));

        int selectedIndex = selectedPath is null
            ? 0
            : _entries.Select((entry, index) => (entry, index))
                .Where(item => item.entry.RelativePath.Equals(selectedPath, StringComparison.Ordinal))
                .Select(item => item.index)
                .DefaultIfEmpty(0)
                .First();
        FileList.FocusItem(selectedIndex);

        string path = string.IsNullOrEmpty(snapshot.RelativeDirectory) ? "." : snapshot.RelativeDirectory;
        // Der feste Kopf hält Zweck und Pfad auch dann sichtbar, wenn der Detailbereich Hilfe oder Ergebnisse zeigt.
        // The fixed header keeps purpose and path visible while the detail region shows help or results.
        _header.SetText(
            $"TP7 TVFM | kontrollierte Dateiverwaltung\n"
            + $"Path: {path} | filter={snapshot.Filter} | sort={snapshot.Sort}");
        _detail.SetText($"{title}\n{detail}");
        SetFocus(FileList);
        DrawView();
    }

    public void RefreshSelectionStatus(string status)
    {
        _detail.SetText($"TP7 TVFM - Selection\n{status}");
        DrawView();
    }
}

internal sealed class Wave6RelativeTargetValidator : TValidator
{
    private readonly bool _leafOnly;

    public Wave6RelativeTargetValidator(bool leafOnly)
    {
        _leafOnly = leafOnly;
    }

    public override bool IsValid(string input)
    {
        if (string.IsNullOrWhiteSpace(input)
            || input.Length > 128
            || Path.IsPathRooted(input)
            || input.IndexOfAny(['\0', ':', '*', '?', '"', '<', '>', '|']) >= 0)
        {
            return false;
        }

        string[] parts = input.Split(['/', '\\'], StringSplitOptions.None);
        return (!_leafOnly || parts.Length == 1)
            && parts.All(part => !string.IsNullOrWhiteSpace(part) && part is not "." and not "..");
    }
}

internal sealed class Wave6OperationDialog : TDialog
{
    private const byte ScanF1 = 0x3B;
    private readonly Wave6TextPanel _description;

    public Wave6OperationDialog(
        TRect bounds,
        Wave6OperationKind kind,
        string sourceRelativePath,
        string? targetRelativePath)
        : base(bounds, $"{kind} / Entscheidung")
    {
        Kind = kind;
        SourceRelativePath = sourceRelativePath;
        bool requiresTarget = kind is Wave6OperationKind.Copy or Wave6OperationKind.Rename;
        int right = Math.Max(18, Size.X - 2);

        PreviewText = requiresTarget
            ? $"{kind}: {sourceRelativePath} -> {targetRelativePath}\nPreview only; Confirm revalidates."
            : $"{kind}: {sourceRelativePath}\nPreview only; Confirm revalidates.";
        _description = new Wave6TextPanel(new TRect(2, 1, right, 4));
        _description.SetText(PreviewText);
        Insert(_description);

        if (requiresTarget)
        {
            TargetInput = new TInputLine(new TRect(2, 4, right, 5), 128)
            {
                Data = targetRelativePath ?? string.Empty,
                Validator = new Wave6RelativeTargetValidator(kind == Wave6OperationKind.Rename)
            };
            Insert(TargetInput);
        }

        TButton confirm = new(
            new TRect(2, 6, Math.Min(17, right), 7),
            "~B~estätigen / OK",
            ShellCommandIds.cmOK,
            TButtonFlags.bfDefault);
        TButton cancel = new(
            new TRect(Math.Min(19, right - 14), 6, right, 7),
            "~A~bbrechen",
            ShellCommandIds.cmCancel,
            TButtonFlags.bfNormal);
        Insert(confirm);
        Insert(cancel);

        SetFocus((TView?)TargetInput ?? confirm);
        InitialFocusKind = Current?.GetType().Name ?? string.Empty;
    }

    public Wave6OperationKind Kind { get; }

    public string SourceRelativePath { get; }

    public TInputLine? TargetInput { get; }

    public string? TargetRelativePath => TargetInput?.Data;

    public string PreviewText { get; }

    public string InitialFocusKind { get; }

    public bool HelpRequested { get; private set; }

    public bool ValidationRejected { get; private set; }

    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.KeyDown && @event.KeyDown.ScanCode == ScanF1)
        {
            HelpRequested = true;
            _description.SetText(
                "Hilfe / Help: Confirm revalidates once.\n"
                + "Escape cancels with NoMutation.");
            @event.Clear(this);
            return;
        }

        base.HandleEvent(@event);
    }

    protected override bool Valid(ushort command)
    {
        bool valid = base.Valid(command);
        ValidationRejected |= command != ShellCommandIds.cmCancel && !valid;
        return valid;
    }
}
