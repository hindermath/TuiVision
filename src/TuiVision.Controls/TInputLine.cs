// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Einzeiliges Texteingabefeld mit Cursor- und Viewport-Verwaltung.
///
/// Single-line text input field with cursor and viewport management.
/// </summary>
public class TInputLine : TView
{
    private const byte ScanBackspace = 0x0E;
    private const byte ScanLeft = 0x4B;
    private const byte ScanRight = 0x4D;
    private const byte ScanHome = 0x47;
    private const byte ScanEnd = 0x4F;
    private const byte ScanInsert = 0x52;
    private const byte ScanDelete = 0x53;

    private string _data = string.Empty;
    private int _selectionStart;
    private int _selectionEnd;

    /// <summary>
    /// Erstellt eine neue Eingabezeile.
    ///
    /// Creates a new input line.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="maxLen">Die maximale Zeichenanzahl. / The maximum number of characters.</param>
    public TInputLine(TRect bounds, int maxLen) : base(bounds)
    {
        if (maxLen < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLen), "Maximum length must not be negative.");
        }

        MaxLen = maxLen;
        Options |= TViewOptions.Selectable;
        SetState(TViewState.CursorVisible, true);
        SetState(TViewState.CursorInsert, true);
    }

    /// <summary>
    /// Der aktuelle Textinhalt.
    ///
    /// The current text content.
    /// </summary>
    public string Data
    {
        get => _data;
        set
        {
            _data = (value ?? string.Empty);
            if (_data.Length > MaxLen)
            {
                _data = _data[..MaxLen];
            }

            CurPos = Math.Clamp(CurPos, 0, _data.Length);
            _selectionStart = Math.Clamp(_selectionStart, 0, _data.Length);
            _selectionEnd = Math.Clamp(_selectionEnd, _selectionStart, _data.Length);
            SyncViewport();
        }
    }

    /// <summary>
    /// Die maximale Zeichenanzahl.
    ///
    /// The maximum number of characters.
    /// </summary>
    public int MaxLen { get; }

    /// <summary>
    /// Die aktuelle Cursor-Position.
    ///
    /// The current cursor position.
    /// </summary>
    public int CurPos { get; private set; }

    /// <summary>
    /// Der erste sichtbare Zeichenindex.
    ///
    /// The index of the first visible character.
    /// </summary>
    public int FirstPos { get; private set; }

    /// <summary>
    /// Gibt an, ob Einfügemodus aktiv ist.
    ///
    /// Indicates whether insert mode is active.
    /// </summary>
    public bool InsertMode { get; private set; } = true;

    /// <summary>
    /// Der optionale Validator. <c>null</c> bewahrt das bisherige freie
    /// Eingabeverhalten.
    ///
    /// The optional validator. <c>null</c> preserves the previous unrestricted
    /// input behavior.
    /// </summary>
    public TValidator? Validator { get; set; }

    /// <summary>
    /// Das zuletzt beobachtbare Validierungsergebnis.
    ///
    /// The latest observable validation result.
    /// </summary>
    public TValidationResult LastValidationResult { get; private set; } =
        TValidationResult.Accepted(TValidationPhase.Edit);

    /// <summary>
    /// Der inklusive Startindex der aktuellen Auswahl.
    ///
    /// The inclusive start index of the current selection.
    /// </summary>
    public int SelectionStart => _selectionStart;

    /// <summary>
    /// Der exklusive Endindex der aktuellen Auswahl.
    ///
    /// The exclusive end index of the current selection.
    /// </summary>
    public int SelectionEnd => _selectionEnd;

    /// <summary>
    /// Setzt eine begrenzte Auswahl und stellt den Cursor an ihr Ende.
    ///
    /// Sets a bounded selection and places the cursor at its end.
    /// </summary>
    /// <param name="start">Der inklusive Startindex. / The inclusive start index.</param>
    /// <param name="end">Der exklusive Endindex. / The exclusive end index.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird für einen Bereich außerhalb des Texts oder für <c>end &lt; start</c> ausgelöst.
    /// Thrown for a range outside the text or for <c>end &lt; start</c>.
    /// </exception>
    public void SetSelection(int start, int end)
    {
        if (start < 0 || start > Data.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (end < start || end > Data.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(end));
        }

        _selectionStart = start;
        _selectionEnd = end;
        CurPos = end;
        SyncViewport();
    }

    /// <summary>
    /// Zeichnet den aktuell sichtbaren Textausschnitt.
    ///
    /// Draws the currently visible text slice.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.X <= 0 || Size.Y <= 0)
        {
            return;
        }

        int visibleWidth = Math.Max(0, Size.X);
        string visible = FirstPos < Data.Length
            ? Data.Substring(FirstPos, Math.Min(visibleWidth, Data.Length - FirstPos))
            : string.Empty;
        string line = visible.PadRight(visibleWidth);
        buffer.WriteText(Origin.X, Origin.Y, line.AsSpan());
    }

    /// <summary>
    /// Verarbeitet Textbearbeitung, Cursorbewegung und Insert/Overwrite.
    ///
    /// Processes text editing, cursor movement, and insert/overwrite mode.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (GetState(TViewState.Disabled) || @event.What != TEventKind.KeyDown)
        {
            return;
        }

        // Clipboard handling: Ctrl+C (copy), Ctrl+X (cut), Ctrl+V (paste)
        if (@event.KeyDown.CharCode == '\x03') // Ctrl+C: copy
        {
            string text = HasSelection
                ? Data[_selectionStart.._selectionEnd]
                : Data;
            if (!string.IsNullOrEmpty(text))
            {
                ManagedClipboard.SetText(text);
            }

            @event.Clear(this);
            return;
        }

        if (@event.KeyDown.CharCode == '\x18') // Ctrl+X: cut
        {
            int start = HasSelection ? _selectionStart : 0;
            int end = HasSelection ? _selectionEnd : Data.Length;
            string removed = Data[start..end];
            string candidate = Data.Remove(start, end - start);
            if (TryApplyCandidate(candidate, start) && removed.Length > 0)
            {
                ManagedClipboard.SetText(removed);
            }
            @event.Clear(this);
            return;
        }

        if (@event.KeyDown.CharCode == '\x16') // Ctrl+V: paste
        {
            string? clip = ManagedClipboard.GetText();
            if (!string.IsNullOrEmpty(clip))
            {
                int selectedLength = _selectionEnd - _selectionStart;
                int space = MaxLen - (Data.Length - selectedLength);
                string toInsert = clip.Length <= space ? clip : clip[..space];
                toInsert = new string(toInsert.Where(c => c >= ' ').ToArray());
                if (toInsert.Length > 0)
                {
                    int start = HasSelection ? _selectionStart : CurPos;
                    string candidate = Data.Remove(start, selectedLength).Insert(start, toInsert);
                    _ = TryApplyCandidate(candidate, start + toInsert.Length);
                }
            }

            @event.Clear(this);
            return;
        }

        switch (@event.KeyDown.ScanCode)
        {
            case ScanLeft:
                MoveCursor(Math.Max(0, HasSelection ? _selectionStart : CurPos - 1));
                @event.Clear(this);
                return;
            case ScanRight:
                MoveCursor(Math.Min(Data.Length, HasSelection ? _selectionEnd : CurPos + 1));
                @event.Clear(this);
                return;
            case ScanHome:
                MoveCursor(0);
                @event.Clear(this);
                return;
            case ScanEnd:
                MoveCursor(Data.Length);
                @event.Clear(this);
                return;
            case ScanInsert:
                InsertMode = !InsertMode;
                SetState(TViewState.CursorInsert, InsertMode);
                @event.Clear(this);
                return;
            case ScanDelete:
                if (HasSelection)
                {
                    _ = TryApplyCandidate(
                        Data.Remove(_selectionStart, _selectionEnd - _selectionStart),
                        _selectionStart);
                }
                else if (CurPos < Data.Length)
                {
                    _ = TryApplyCandidate(Data.Remove(CurPos, 1), CurPos);
                }

                @event.Clear(this);
                return;
            case ScanBackspace:
                if (HasSelection)
                {
                    _ = TryApplyCandidate(
                        Data.Remove(_selectionStart, _selectionEnd - _selectionStart),
                        _selectionStart);
                }
                else if (CurPos > 0)
                {
                    _ = TryApplyCandidate(Data.Remove(CurPos - 1, 1), CurPos - 1);
                }

                @event.Clear(this);
                return;
        }

        if (ShouldInsertCharacter(@event.KeyDown))
        {
            InsertCharacter(@event.KeyDown.CharCode);
            @event.Clear(this);
        }
    }

    private void InsertCharacter(char character)
    {
        if (MaxLen == 0 || character == '\0')
        {
            return;
        }

        if (HasSelection)
        {
            int available = MaxLen - (Data.Length - (_selectionEnd - _selectionStart));
            if (available > 0)
            {
                string candidate = Data.Remove(_selectionStart, _selectionEnd - _selectionStart)
                    .Insert(_selectionStart, character.ToString());
                _ = TryApplyCandidate(candidate, _selectionStart + 1);
            }

            return;
        }

        if (InsertMode)
        {
            if (Data.Length >= MaxLen)
            {
                return;
            }

            _ = TryApplyCandidate(Data.Insert(CurPos, character.ToString()), CurPos + 1);
            return;
        }

        if (CurPos < Data.Length)
        {
            string candidate = Data.Remove(CurPos, 1).Insert(CurPos, character.ToString());
            _ = TryApplyCandidate(candidate, CurPos + 1);
            return;
        }

        if (Data.Length < MaxLen)
        {
            _ = TryApplyCandidate(Data + character, CurPos + 1);
        }
    }

    /// <inheritdoc />
    public override bool CanReleaseFocus() =>
        Validate(TValidationPhase.FocusLoss).IsValid;

    /// <inheritdoc />
    public override TValidationResult Validate(TValidationPhase phase)
    {
        LastValidationResult = Validator?.Validate(Data, phase, this)
            ?? TValidationResult.Accepted(phase);
        return LastValidationResult;
    }

    private bool HasSelection => _selectionEnd > _selectionStart;

    private bool TryApplyCandidate(string candidate, int newCursor)
    {
        // Erst der vollständige Kandidat wird geprüft; dadurch bleibt bei Ablehnung jeder sichtbare Zustand atomar erhalten.
        // The complete candidate is validated first, preserving all visible state atomically on rejection.
        LastValidationResult = Validator?.Validate(candidate, TValidationPhase.Edit, this)
            ?? TValidationResult.Accepted(TValidationPhase.Edit);
        if (!LastValidationResult.IsValid)
        {
            return false;
        }

        _data = candidate;
        CurPos = Math.Clamp(newCursor, 0, _data.Length);
        _selectionStart = CurPos;
        _selectionEnd = CurPos;
        SyncViewport();
        return true;
    }

    private void MoveCursor(int position)
    {
        CurPos = Math.Clamp(position, 0, Data.Length);
        _selectionStart = CurPos;
        _selectionEnd = CurPos;
        SyncViewport();
    }

    private void SyncViewport()
    {
        CurPos = Math.Clamp(CurPos, 0, Data.Length);
        int visibleWidth = Math.Max(1, Size.X);

        if (CurPos < FirstPos)
        {
            FirstPos = CurPos;
        }
        else if (CurPos >= FirstPos + visibleWidth)
        {
            FirstPos = CurPos - visibleWidth + 1;
        }

        FirstPos = Math.Clamp(FirstPos, 0, Math.Max(0, Data.Length));
    }

    private static bool ShouldInsertCharacter(TKeyDownEvent keyDown)
    {
        if (keyDown.CharCode < ' ')
        {
            return false;
        }

        return (keyDown.ShiftState & 0x0006) == 0;
    }
}
