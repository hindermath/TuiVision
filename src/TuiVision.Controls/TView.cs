using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// State mask for a view.
/// </summary>
[Flags]
public enum TViewState : ushort
{
    Visible = 0x001,
    CursorVisible = 0x002,
    CursorInsert = 0x004,
    Shadow = 0x008,
    Active = 0x010,
    Selected = 0x020,
    Focused = 0x040,
    Dragging = 0x080,
    Disabled = 0x100,
    Modal = 0x200,
    Default = 0x400,
    Exposed = 0x800
}

/// <summary>
/// Option mask for view behavior.
/// </summary>
[Flags]
public enum TViewOptions : ushort
{
    None = 0,
    Selectable = 0x001,
    TopSelect = 0x002,
    FirstClick = 0x004,
    Framed = 0x008,
    PreProcess = 0x010,
    PostProcess = 0x020,
    Buffered = 0x040,
    Tileable = 0x080,
    CenterX = 0x100,
    CenterY = 0x200,
    Centered = CenterX | CenterY,
    Verbose = 0x400
}

/// <summary>
/// Base visual element ported from Turbo Vision's TView.
/// </summary>
public class TView : TObject
{
    private static readonly TPoint MaxDefaultSize = new(short.MaxValue, short.MaxValue);
    private TRect _bounds;

    public TView(TRect bounds)
    {
        ValidateBounds(bounds);
        _bounds = bounds;
        EventMask = TEventKind.MouseDown | TEventKind.KeyDown | TEventKind.Command;
        State = TViewState.Visible;
        Options = TViewOptions.None;
    }

    public TViewOptions Options { get; set; }

    public TEventKind EventMask { get; set; }

    public TViewState State { get; private set; }

    public TPoint Origin => _bounds.A;

    public TPoint Size => _bounds.B - _bounds.A;

    public TRect GetBounds() => _bounds;

    public TRect GetExtent() => new(0, 0, Size.X, Size.Y);

    public bool GetState(TViewState state) => (State & state) == state;

    public void Show() => SetState(TViewState.Visible, true);

    public void Hide() => SetState(TViewState.Visible, false);

    public virtual void SetState(TViewState state, bool enable) =>
        State = enable ? State | state : State & ~state;

    public virtual void SizeLimits(out TPoint min, out TPoint max)
    {
        min = new TPoint(0, 0);
        max = MaxDefaultSize;
    }

    public void Locate(TRect bounds)
    {
        ValidateBounds(bounds);
        SizeLimits(out TPoint min, out TPoint max);

        int width = Math.Clamp(bounds.Width, min.X, max.X);
        int height = Math.Clamp(bounds.Height, min.Y, max.Y);

        _bounds = new TRect(bounds.A.X, bounds.A.Y, bounds.A.X + width, bounds.A.Y + height);
        OnBoundsChanged();
    }

    public void MoveTo(int x, int y)
    {
        TPoint size = Size;
        Locate(new TRect(x, y, x + size.X, y + size.Y));
    }

    public void GrowTo(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Size must be non-negative.");
        }

        Locate(new TRect(Origin.X, Origin.Y, Origin.X + x, Origin.Y + y));
    }

    public void SetBounds(TRect bounds) => Locate(bounds);

    public TPoint MakeGlobal(TPoint source) => source + Origin;

    public TPoint MakeLocal(TPoint source) => source - Origin;

    public bool MouseInView(TPoint mouse)
    {
        TPoint local = MakeLocal(mouse);
        return GetExtent().Contains(local);
    }

    public virtual void HandleEvent(TEvent @event)
    {
        if (@event.What != TEventKind.MouseDown)
        {
            return;
        }

        bool isSelectedOrDisabled = (State & (TViewState.Selected | TViewState.Disabled)) != 0;
        if (!isSelectedOrDisabled && Options.HasFlag(TViewOptions.Selectable))
        {
            SetState(TViewState.Selected, true);
            if (!GetState(TViewState.Selected) || !Options.HasFlag(TViewOptions.FirstClick))
            {
                @event.Clear(this);
            }
        }
    }

    protected virtual void OnBoundsChanged()
    {
    }

    private static void ValidateBounds(TRect bounds)
    {
        if (bounds.B.X < bounds.A.X || bounds.B.Y < bounds.A.Y)
        {
            throw new ArgumentException("Bounds must not have negative width or height.", nameof(bounds));
        }
    }
}
