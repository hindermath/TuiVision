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
            if (!string.IsNullOrEmpty(Data))
            {
                ManagedClipboard.SetText(Data);
            }

            @event.Clear(this);
            return;
        }

        if (@event.KeyDown.CharCode == '\x18') // Ctrl+X: cut
        {
            if (!string.IsNullOrEmpty(Data))
            {
                ManagedClipboard.SetText(Data);
            }

            _data = string.Empty;
            CurPos = 0;
            FirstPos = 0;
            @event.Clear(this);
            return;
        }

        if (@event.KeyDown.CharCode == '\x16') // Ctrl+V: paste
        {
            string? clip = ManagedClipboard.GetText();
            if (!string.IsNullOrEmpty(clip) && Data.Length < MaxLen)
            {
                int space = MaxLen - Data.Length;
                string toInsert = clip.Length <= space ? clip : clip[..space];
                toInsert = new string(toInsert.Where(c => c >= ' ').ToArray());
                if (toInsert.Length > 0)
                {
                    _data = Data.Insert(CurPos, toInsert);
                    CurPos = Math.Min(Data.Length, CurPos + toInsert.Length);
                    SyncViewport();
                }
            }

            @event.Clear(this);
            return;
        }

        switch (@event.KeyDown.ScanCode)
        {
            case ScanLeft:
                CurPos = Math.Max(0, CurPos - 1);
                SyncViewport();
                @event.Clear(this);
                return;
            case ScanRight:
                CurPos = Math.Min(Data.Length, CurPos + 1);
                SyncViewport();
                @event.Clear(this);
                return;
            case ScanHome:
                CurPos = 0;
                SyncViewport();
                @event.Clear(this);
                return;
            case ScanEnd:
                CurPos = Data.Length;
                SyncViewport();
                @event.Clear(this);
                return;
            case ScanInsert:
                InsertMode = !InsertMode;
                SetState(TViewState.CursorInsert, InsertMode);
                @event.Clear(this);
                return;
            case ScanDelete:
                if (CurPos < Data.Length)
                {
                    _data = Data.Remove(CurPos, 1);
                    SyncViewport();
                }

                @event.Clear(this);
                return;
            case ScanBackspace:
                if (CurPos > 0)
                {
                    _data = Data.Remove(CurPos - 1, 1);
                    CurPos--;
                    SyncViewport();
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

        if (InsertMode)
        {
            if (Data.Length >= MaxLen)
            {
                return;
            }

            _data = Data.Insert(CurPos, character.ToString());
            CurPos++;
            SyncViewport();
            return;
        }

        if (CurPos < Data.Length)
        {
            char[] chars = Data.ToCharArray();
            chars[CurPos] = character;
            _data = new string(chars);
            CurPos++;
            SyncViewport();
            return;
        }

        if (Data.Length < MaxLen)
        {
            _data = Data + character;
            CurPos++;
            SyncViewport();
        }
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
