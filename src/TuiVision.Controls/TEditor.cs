// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Wiederverwendbarer mehrzeiliger Texteditor mit Cursor-, Auswahl- und Suchlogik.
///
/// Reusable multi-line text editor with cursor, selection, and search behaviour.
/// </summary>
public class TEditor : TScroller
{
    private const byte ScanBackspace = 0x0E;
    private const byte ScanEnter = 0x1C;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanUp = 0x48;
    private const byte ScanDown = 0x50;
    private const byte ScanHome = 0x47;
    private const byte ScanEnd = 0x4F;
    private const byte ScanInsert = 0x52;
    private const byte ScanDelete = 0x53;

    private readonly Stack<EditorSnapshot> _undoStack = new();
    private string _text = string.Empty;
    private int _cursorIndex;
    private int _selectionStart;
    private int _selectionEnd;
    private int _preferredColumn;

    /// <summary>
    /// Initialisiert einen neuen Editor.
    ///
    /// Initializes a new editor.
    /// </summary>
    /// <param name="bounds">Die Bounds des Editors. / The editor bounds.</param>
    public TEditor(TRect bounds) : base(bounds)
    {
        Options |= TViewOptions.Selectable;
        SetState(TViewState.CursorVisible, true);
        SetState(TViewState.CursorInsert, true);
        UpdateLimits();
    }

    /// <summary>
    /// Wird ausgelost, wenn sich Inhalt oder Zustand des Dokuments aendern.
    ///
    /// Raised when the document content or state changes.
    /// </summary>
    public event EventHandler? Changed;

    /// <summary>
    /// Gemeinsamer Zwischenablageninhalt fuer editororientierte Tests und Hosts.
    ///
    /// Shared clipboard content for editor-centric tests and hosts.
    /// </summary>
    public static string ClipboardText { get; set; } = string.Empty;

    /// <summary>
    /// Gibt an, ob der Editor im Einfuegemodus arbeitet.
    ///
    /// Indicates whether the editor operates in insert mode.
    /// </summary>
    public bool InsertMode { get; private set; } = true;

    /// <summary>
    /// Gibt an, ob ungespeicherte Aenderungen vorliegen.
    ///
    /// Indicates whether unsaved changes exist.
    /// </summary>
    public bool Modified { get; protected set; }

    /// <summary>
    /// Der Cursorindex bezogen auf den normalisierten Dokumenttext.
    ///
    /// The cursor index relative to the normalised document text.
    /// </summary>
    public int CursorIndex => _cursorIndex;

    /// <summary>
    /// Die aktuelle Cursorzeile.
    ///
    /// The current cursor row.
    /// </summary>
    public int CursorRow => GetPointFromIndex(_cursorIndex).Row;

    /// <summary>
    /// Die aktuelle Cursorspalte.
    ///
    /// The current cursor column.
    /// </summary>
    public int CursorColumn => GetPointFromIndex(_cursorIndex).Column;

    /// <summary>
    /// Die aktuelle Auswahlgrenze am Dokumentanfang.
    ///
    /// The current selection start boundary.
    /// </summary>
    public int SelectionStart => Math.Min(_selectionStart, _selectionEnd);

    /// <summary>
    /// Die aktuelle Auswahlgrenze am Dokumentende.
    ///
    /// The current selection end boundary.
    /// </summary>
    public int SelectionEnd => Math.Max(_selectionStart, _selectionEnd);

    /// <summary>
    /// Gibt an, ob aktuell Text ausgewaehlt ist.
    ///
    /// Indicates whether text is currently selected.
    /// </summary>
    public bool HasSelection => SelectionStart != SelectionEnd;

    /// <summary>
    /// Gibt die normalisierte Dokumentzeichenkette zurueck.
    ///
    /// Returns the normalised document string.
    /// </summary>
    /// <returns>Der Dokumenttext. / The document text.</returns>
    public string GetText() => _text;

    /// <summary>
    /// Laedt neuen Dokumenttext in den Editor.
    ///
    /// Loads new document text into the editor.
    /// </summary>
    /// <param name="text">Der Dokumenttext. / The document text.</param>
    /// <param name="markClean">Setzt das Dokument anschliessend auf clean. / Marks the document clean afterwards.</param>
    public void LoadText(string text, bool markClean = true)
    {
        _text = Normalise(text);
        _cursorIndex = 0;
        _selectionStart = 0;
        _selectionEnd = 0;
        _preferredColumn = 0;
        Modified = !markClean;
        _undoStack.Clear();
        UpdateLimits();
        EnsureCursorVisible();
        OnChanged();
    }

    /// <summary>
    /// Setzt den Cursor auf die angegebene Zeile und Spalte.
    ///
    /// Moves the cursor to the specified row and column.
    /// </summary>
    /// <param name="row">Die Zielzeile. / The target row.</param>
    /// <param name="column">Die Zielspalte. / The target column.</param>
    public void MoveCursorTo(int row, int column)
    {
        _cursorIndex = GetIndexFromPoint(row, column);
        ClearSelection();
        EnsureCursorVisible();
        OnChanged();
    }

    /// <summary>
    /// Setzt eine lineare Auswahl.
    ///
    /// Sets a linear selection.
    /// </summary>
    /// <param name="start">Der Startindex. / The start index.</param>
    /// <param name="end">Der Endindex. / The end index.</param>
    public void Select(int start, int end)
    {
        _selectionStart = ClampIndex(start);
        _selectionEnd = ClampIndex(end);
        _cursorIndex = _selectionEnd;
        _preferredColumn = CursorColumn;
        EnsureCursorVisible();
        OnChanged();
    }

    /// <summary>
    /// Selektiert den gesamten Dokumenttext.
    ///
    /// Selects the entire document text.
    /// </summary>
    public void SelectAll() => Select(0, _text.Length);

    /// <summary>
    /// Kopiert die aktuelle Auswahl in die gemeinsame Zwischenablage.
    ///
    /// Copies the current selection into the shared clipboard.
    /// </summary>
    public void CopySelection()
    {
        if (!HasSelection)
        {
            return;
        }

        ClipboardText = _text[SelectionStart..SelectionEnd];
        OnChanged();
    }

    /// <summary>
    /// Schneidet die aktuelle Auswahl aus.
    ///
    /// Cuts the current selection.
    /// </summary>
    public void CutSelection()
    {
        if (!HasSelection)
        {
            return;
        }

        SaveSnapshot();
        ClipboardText = _text[SelectionStart..SelectionEnd];
        DeleteSelectionCore();
        MarkModified();
    }

    /// <summary>
    /// Fuegt den Zwischenablageninhalt ein.
    ///
    /// Pastes the clipboard content.
    /// </summary>
    public void PasteClipboard()
    {
        if (string.IsNullOrEmpty(ClipboardText))
        {
            return;
        }

        SaveSnapshot();
        InsertTextCore(ClipboardText);
        MarkModified();
    }

    /// <summary>
    /// Macht den letzten Bearbeitungsschritt rueckgaengig.
    ///
    /// Undoes the last edit operation.
    /// </summary>
    /// <returns><c>true</c>, wenn ein Undo ausgefuehrt wurde. / <c>true</c> if an undo was performed.</returns>
    public bool Undo()
    {
        if (_undoStack.Count == 0)
        {
            return false;
        }

        EditorSnapshot snapshot = _undoStack.Pop();
        _text = snapshot.Text;
        _cursorIndex = snapshot.CursorIndex;
        _selectionStart = snapshot.SelectionStart;
        _selectionEnd = snapshot.SelectionEnd;
        InsertMode = snapshot.InsertMode;
        Modified = snapshot.Modified;
        SetState(TViewState.CursorInsert, InsertMode);
        UpdateLimits();
        EnsureCursorVisible();
        OnChanged();
        return true;
    }

    /// <summary>
    /// Sucht vorwaerts nach einem Suchbegriff und markiert den Treffer.
    ///
    /// Searches forward for a term and marks the match.
    /// </summary>
    /// <param name="term">Der Suchbegriff. / The search term.</param>
    /// <param name="caseSensitive">Gibt an, ob gross-/kleinschreibung beachtet wird. / Indicates whether case is respected.</param>
    /// <returns><c>true</c>, wenn ein Treffer gefunden wurde. / <c>true</c> if a match was found.</returns>
    public bool FindNext(string term, bool caseSensitive = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(term);

        StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        int start = HasSelection ? SelectionEnd : _cursorIndex;
        int index = _text.IndexOf(term, start, comparison);
        if (index < 0 && start > 0)
        {
            index = _text.IndexOf(term, 0, comparison);
        }

        if (index < 0)
        {
            return false;
        }

        Select(index, index + term.Length);
        return true;
    }

    /// <summary>
    /// Ersetzt die aktuelle Auswahl.
    ///
    /// Replaces the current selection.
    /// </summary>
    /// <param name="replacement">Der Ersatztext. / The replacement text.</param>
    /// <returns><c>true</c>, wenn eine Auswahl ersetzt wurde. / <c>true</c> if a selection was replaced.</returns>
    public bool ReplaceSelection(string replacement)
    {
        if (!HasSelection)
        {
            return false;
        }

        SaveSnapshot();
        InsertTextCore(replacement, replaceSelection: true);
        MarkModified();
        return true;
    }

    /// <summary>
    /// Ersetzt alle Vorkommen eines Suchbegriffs.
    ///
    /// Replaces all occurrences of a search term.
    /// </summary>
    /// <param name="term">Der Suchbegriff. / The search term.</param>
    /// <param name="replacement">Der Ersatztext. / The replacement text.</param>
    /// <param name="caseSensitive">Gibt an, ob gross-/kleinschreibung beachtet wird. / Indicates whether case is respected.</param>
    /// <returns>Die Anzahl ersetzter Treffer. / The number of replaced matches.</returns>
    public int ReplaceAll(string term, string replacement, bool caseSensitive = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(term);

        StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        int count = 0;
        int index = 0;
        string source = _text;
        while (true)
        {
            int match = source.IndexOf(term, index, comparison);
            if (match < 0)
            {
                break;
            }

            if (count == 0)
            {
                SaveSnapshot();
            }

            source = source.Remove(match, term.Length).Insert(match, Normalise(replacement));
            index = match + replacement.Length;
            count++;
        }

        if (count > 0)
        {
            _text = source;
            _cursorIndex = Math.Min(index, _text.Length);
            ClearSelection();
            UpdateLimits();
            MarkModified();
        }

        return count;
    }

    /// <summary>
    /// Prueft, ob ein Shell-Befehl aktuell verfuegbar ist.
    ///
    /// Checks whether a shell command is currently available.
    /// </summary>
    /// <param name="command">Die Command-ID. / The command identifier.</param>
    /// <returns><c>true</c>, wenn der Befehl verfuegbar ist. / <c>true</c> if the command is available.</returns>
    public virtual bool IsCommandEnabled(ushort command)
    {
        return command switch
        {
            ShellCommandIds.cmUndo => _undoStack.Count > 0,
            ShellCommandIds.cmCopy => HasSelection,
            ShellCommandIds.cmCut => HasSelection,
            ShellCommandIds.cmPaste => !string.IsNullOrEmpty(ClipboardText),
            ShellCommandIds.cmFind => _text.Length > 0,
            ShellCommandIds.cmReplace => HasSelection,
            _ => true
        };
    }

    /// <summary>
    /// Prueft, ob das Dokument ohne Datenverlust geschlossen werden darf.
    ///
    /// Checks whether the document may be closed without data loss.
    /// </summary>
    /// <param name="discardDecision">Optionale Verwerfungsentscheidung. / Optional discard decision.</param>
    /// <returns><c>true</c>, wenn geschlossen werden darf. / <c>true</c> if closing may proceed.</returns>
    public bool CanClose(Func<bool>? discardDecision = null)
    {
        return !Modified || (discardDecision?.Invoke() ?? false);
    }

    /// <summary>
    /// Zeichnet den sichtbaren Dokumentausschnitt.
    ///
    /// Draws the visible document slice.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        string[] lines = GetLines();
        for (int row = 0; row < Size.Y; row++)
        {
            int lineIndex = Delta.Y + row;
            string line = lineIndex < lines.Length ? lines[lineIndex] : string.Empty;
            string visible = Delta.X < line.Length
                ? line.Substring(Delta.X, Math.Min(Size.X, line.Length - Delta.X))
                : string.Empty;
            buffer.WriteText(Origin.X, Origin.Y + row, visible.PadRight(Size.X).AsSpan(0, Size.X));
        }
    }

    /// <summary>
    /// Verarbeitet Tastatureingaben fuer Bearbeitung und Navigation.
    ///
    /// Processes keyboard input for editing and navigation.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (@event.What != TEventKind.KeyDown || GetState(TViewState.Disabled))
        {
            return;
        }

        switch (@event.KeyDown.ScanCode)
        {
            case ScanLeft:
                MoveHorizontal(-1);
                @event.Clear(this);
                return;
            case ScanRight:
                MoveHorizontal(1);
                @event.Clear(this);
                return;
            case ScanUp:
                MoveVertical(-1);
                @event.Clear(this);
                return;
            case ScanDown:
                MoveVertical(1);
                @event.Clear(this);
                return;
            case ScanHome:
                MoveToLineBoundary(start: true);
                @event.Clear(this);
                return;
            case ScanEnd:
                MoveToLineBoundary(start: false);
                @event.Clear(this);
                return;
            case ScanInsert:
                InsertMode = !InsertMode;
                SetState(TViewState.CursorInsert, InsertMode);
                OnChanged();
                @event.Clear(this);
                return;
            case ScanDelete:
                DeleteForward();
                @event.Clear(this);
                return;
            case ScanBackspace:
                DeleteBackward();
                @event.Clear(this);
                return;
            case ScanEnter:
                InsertText(Environment.NewLine == "\r\n" ? "\n" : "\n");
                @event.Clear(this);
                return;
        }

        if (ShouldInsertCharacter(@event.KeyDown))
        {
            InsertText(@event.KeyDown.CharCode.ToString());
            @event.Clear(this);
        }
    }

    /// <summary>
    /// Liefert shellsichtbare Status-Hinweise fuer den Editor.
    ///
    /// Returns shell-visible status hints for the editor.
    /// </summary>
    /// <returns>Die Hint-Kette. / The hint chain.</returns>
    public override TStatusItem? GetStatusHints()
    {
        TStatusItem paste = new("~Ins~ Paste", ShellCommandIds.cmPaste) { Disabled = !IsCommandEnabled(ShellCommandIds.cmPaste) };
        TStatusItem cut = new("~Shift+Del~ Cut", ShellCommandIds.cmCut, paste) { Disabled = !IsCommandEnabled(ShellCommandIds.cmCut) };
        TStatusItem copy = new("~Ctrl+Ins~ Copy", ShellCommandIds.cmCopy, cut) { Disabled = !IsCommandEnabled(ShellCommandIds.cmCopy) };
        TStatusItem undo = new("~Alt+BkSp~ Undo", ShellCommandIds.cmUndo, copy) { Disabled = !IsCommandEnabled(ShellCommandIds.cmUndo) };
        return new TStatusItem("~F2~ Save", ShellCommandIds.cmSave, undo) { Disabled = !IsCommandEnabled(ShellCommandIds.cmSave) };
    }

    /// <summary>
    /// Setzt den Modified-Status explizit zurueck.
    ///
    /// Clears the modified status explicitly.
    /// </summary>
    protected void MarkClean()
    {
        Modified = false;
        OnChanged();
    }

    private void InsertText(string text)
    {
        SaveSnapshot();
        InsertTextCore(text);
        MarkModified();
    }

    private void DeleteBackward()
    {
        if (!HasSelection && _cursorIndex == 0)
        {
            return;
        }

        SaveSnapshot();
        if (HasSelection)
        {
            DeleteSelectionCore();
        }
        else
        {
            _text = _text.Remove(_cursorIndex - 1, 1);
            _cursorIndex--;
            ClearSelection();
        }

        UpdateLimits();
        MarkModified();
    }

    private void DeleteForward()
    {
        if (!HasSelection && _cursorIndex >= _text.Length)
        {
            return;
        }

        SaveSnapshot();
        if (HasSelection)
        {
            DeleteSelectionCore();
        }
        else
        {
            _text = _text.Remove(_cursorIndex, 1);
            ClearSelection();
        }

        UpdateLimits();
        MarkModified();
    }

    private void MoveHorizontal(int delta)
    {
        _cursorIndex = ClampIndex(_cursorIndex + delta);
        ClearSelection();
        _preferredColumn = CursorColumn;
        EnsureCursorVisible();
        OnChanged();
    }

    private void MoveVertical(int deltaRows)
    {
        (int row, int column) = GetPointFromIndex(_cursorIndex);
        int targetRow = Math.Max(0, Math.Min(GetLines().Length - 1, row + deltaRows));
        _cursorIndex = GetIndexFromPoint(targetRow, _preferredColumn);
        ClearSelection();
        EnsureCursorVisible();
        OnChanged();
    }

    private void MoveToLineBoundary(bool start)
    {
        (int row, _) = GetPointFromIndex(_cursorIndex);
        _cursorIndex = GetIndexFromPoint(row, start ? 0 : int.MaxValue);
        ClearSelection();
        _preferredColumn = CursorColumn;
        EnsureCursorVisible();
        OnChanged();
    }

    private void InsertTextCore(string text, bool replaceSelection = true)
    {
        string normalised = Normalise(text);
        if (replaceSelection && HasSelection)
        {
            DeleteSelectionCore();
        }

        if (string.IsNullOrEmpty(normalised))
        {
            return;
        }

        if (InsertMode || _cursorIndex >= _text.Length || normalised.Length > 1)
        {
            _text = _text.Insert(_cursorIndex, normalised);
            _cursorIndex += normalised.Length;
        }
        else
        {
            _text = _text.Remove(_cursorIndex, 1).Insert(_cursorIndex, normalised);
            _cursorIndex += normalised.Length;
        }

        ClearSelection();
        UpdateLimits();
    }

    private void DeleteSelectionCore()
    {
        int start = SelectionStart;
        int length = SelectionEnd - start;
        _text = _text.Remove(start, length);
        _cursorIndex = start;
        ClearSelection();
        UpdateLimits();
    }

    private void SaveSnapshot()
    {
        _undoStack.Push(new EditorSnapshot(_text, _cursorIndex, _selectionStart, _selectionEnd, InsertMode, Modified));
    }

    private void MarkModified()
    {
        Modified = true;
        _preferredColumn = CursorColumn;
        EnsureCursorVisible();
        OnChanged();
    }

    private void UpdateLimits()
    {
        string[] lines = GetLines();
        int maxWidth = 0;
        foreach (string line in lines)
        {
            maxWidth = Math.Max(maxWidth, line.Length);
        }

        SetLimit(new TPoint(maxWidth, lines.Length));
    }

    private void EnsureCursorVisible()
    {
        (int row, int column) = GetPointFromIndex(_cursorIndex);
        int targetX = Delta.X;
        int targetY = Delta.Y;

        if (column < Delta.X)
        {
            targetX = column;
        }
        else if (column >= Delta.X + Math.Max(1, Size.X))
        {
            targetX = column - Math.Max(1, Size.X) + 1;
        }

        if (row < Delta.Y)
        {
            targetY = row;
        }
        else if (row >= Delta.Y + Math.Max(1, Size.Y))
        {
            targetY = row - Math.Max(1, Size.Y) + 1;
        }

        ScrollTo(new TPoint(targetX, targetY));
    }

    private static string Normalise(string? text)
    {
        return (text ?? string.Empty).Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
    }

    private static bool ShouldInsertCharacter(TKeyDownEvent keyDown)
    {
        return keyDown.CharCode >= ' ' && (keyDown.ShiftState & 0x0006) == 0;
    }

    private int ClampIndex(int index) => Math.Clamp(index, 0, _text.Length);

    private void ClearSelection()
    {
        _selectionStart = _cursorIndex;
        _selectionEnd = _cursorIndex;
    }

    private string[] GetLines() => _text.Length == 0 ? [""] : _text.Split('\n');

    private (int Row, int Column) GetPointFromIndex(int index)
    {
        int safeIndex = ClampIndex(index);
        int row = 0;
        int lastBreak = -1;
        for (int current = 0; current < safeIndex; current++)
        {
            if (_text[current] == '\n')
            {
                row++;
                lastBreak = current;
            }
        }

        return (row, safeIndex - lastBreak - 1);
    }

    private int GetIndexFromPoint(int row, int column)
    {
        string[] lines = GetLines();
        int safeRow = Math.Max(0, Math.Min(lines.Length - 1, row));
        int index = 0;
        for (int current = 0; current < safeRow; current++)
        {
            index += lines[current].Length + 1;
        }

        int safeColumn = Math.Max(0, Math.Min(lines[safeRow].Length, column == int.MaxValue ? lines[safeRow].Length : column));
        return index + safeColumn;
    }

    private void OnChanged()
    {
        Changed?.Invoke(this, EventArgs.Empty);
        DrawView();
    }

    private sealed record EditorSnapshot(
        string Text,
        int CursorIndex,
        int SelectionStart,
        int SelectionEnd,
        bool InsertMode,
        bool Modified);
}
