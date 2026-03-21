// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Abstrakte Basis für scrollbare Listenansichten mit fokussiertem Eintrag.
///
/// Abstract base for scrollable list views with a focused item.
/// </summary>
public abstract class TListViewer : TView
{
    /// <summary>
    /// Scan-Code für Pfeil hoch.
    ///
    /// Scan code for arrow up.
    /// </summary>
    private const byte ScanUp = 0x48;

    /// <summary>
    /// Scan-Code für Pfeil runter.
    ///
    /// Scan code for arrow down.
    /// </summary>
    private const byte ScanDown = 0x50;

    /// <summary>
    /// Scan-Code für Pos1.
    ///
    /// Scan code for Home.
    /// </summary>
    private const byte ScanHome = 0x47;

    /// <summary>
    /// Scan-Code für Ende.
    ///
    /// Scan code for End.
    /// </summary>
    private const byte ScanEnd = 0x4F;

    /// <summary>
    /// Scan-Code für Bild hoch.
    ///
    /// Scan code for Page Up.
    /// </summary>
    private const byte ScanPageUp = 0x49;

    /// <summary>
    /// Scan-Code für Bild runter.
    ///
    /// Scan code for Page Down.
    /// </summary>
    private const byte ScanPageDown = 0x51;

    /// <summary>
    /// Erstellt eine neue Listenansicht.
    ///
    /// Creates a new list view.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="numCols">Die Anzahl Spalten. / The number of columns.</param>
    /// <param name="vScrollBar">Optionale vertikale Scrollbar. / Optional vertical scroll bar.</param>
    /// <param name="hScrollBar">Optionale horizontale Scrollbar. / Optional horizontal scroll bar.</param>
    protected TListViewer(TRect bounds, int numCols, TScrollBar? vScrollBar, TScrollBar? hScrollBar)
        : base(bounds)
    {
        NumCols = Math.Max(1, numCols);
        VScrollBar = vScrollBar;
        HScrollBar = hScrollBar;
        Options |= TViewOptions.Selectable;
    }

    /// <summary>
    /// Die Anzahl Spalten der Listenansicht.
    ///
    /// The number of columns in the list view.
    /// </summary>
    public int NumCols { get; }

    /// <summary>
    /// Index des ersten sichtbaren Eintrags.
    ///
    /// Index of the first visible item.
    /// </summary>
    public int TopItem { get; protected set; }

    /// <summary>
    /// Index des fokussierten Eintrags.
    ///
    /// Index of the focused item.
    /// </summary>
    public int FocusedItem { get; protected set; }

    /// <summary>
    /// Optionale vertikale Scrollbar.
    ///
    /// Optional vertical scroll bar.
    /// </summary>
    public TScrollBar? VScrollBar { get; }

    /// <summary>
    /// Optionale horizontale Scrollbar.
    ///
    /// Optional horizontal scroll bar.
    /// </summary>
    public TScrollBar? HScrollBar { get; }

    /// <summary>
    /// Gibt die Anzahl der anzeigbaren Einträge zurück.
    ///
    /// Returns the number of displayable items.
    /// </summary>
    /// <returns>Die Eintragszahl. / The item count.</returns>
    public abstract int GetNumItems();

    /// <summary>
    /// Liefert den Text eines Eintrags.
    ///
    /// Returns the text of an item.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    /// <param name="maxChars">Maximal darzustellende Zeichen. / Maximum number of displayable characters.</param>
    /// <returns>Der Eintragstext. / The item text.</returns>
    public abstract string GetText(int item, int maxChars);

    /// <summary>
    /// Setzt den Fokus auf einen Eintrag und synchronisiert TopItem/Scrollbars.
    ///
    /// Sets focus to an item and synchronises TopItem/scroll bars.
    /// </summary>
    /// <param name="item">Der gewünschte Fokusindex. / The desired focus index.</param>
    public void FocusItem(int item)
    {
        int itemCount = GetNumItems();
        if (itemCount <= 0)
        {
            FocusedItem = 0;
            TopItem = 0;
            SyncScrollBars();
            return;
        }

        FocusedItem = Math.Clamp(item, 0, itemCount - 1);
        int visibleRows = Math.Max(1, Size.Y);
        if (FocusedItem < TopItem)
        {
            TopItem = FocusedItem;
        }
        else if (FocusedItem >= TopItem + visibleRows)
        {
            TopItem = FocusedItem - visibleRows + 1;
        }

        TopItem = Math.Max(0, Math.Min(TopItem, Math.Max(0, itemCount - visibleRows)));
        SyncScrollBars();
    }

    /// <summary>
    /// Alias für das explizite Anwählen eines Eintrags.
    ///
    /// Alias for explicitly selecting an item.
    /// </summary>
    /// <param name="item">Der gewünschte Index. / The desired index.</param>
    public void SelectItem(int item) => FocusItem(item);

    /// <summary>
    /// Zeichnet sichtbare Listeneinträge in den Owner-Puffer.
    ///
    /// Draws visible list items into the owner buffer.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.Y <= 0 || Size.X <= 0)
        {
            return;
        }

        int itemCount = GetNumItems();
        for (int row = 0; row < Size.Y; row++)
        {
            int itemIndex = TopItem + row;
            string content = itemIndex < itemCount ? GetText(itemIndex, Math.Max(0, Size.X - 1)) : string.Empty;
            string line = $"{(itemIndex == FocusedItem && itemIndex < itemCount ? '>' : ' ')}{content}";
            string padded = line.PadRight(Size.X);
            buffer.WriteText(Origin.X, Origin.Y + row, padded.AsSpan(0, Size.X));
        }
    }

    /// <summary>
    /// Verarbeitet Tastatur- und Mausnavigation innerhalb der Liste.
    ///
    /// Processes keyboard and mouse navigation inside the list.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        TEventKind originalKind = @event.What;
        TKeyDownEvent originalKeyDown = @event.KeyDown;
        TMouseEvent originalMouse = @event.Mouse;

        base.HandleEvent(@event);
        if (GetState(TViewState.Disabled))
        {
            return;
        }

        int itemCount = GetNumItems();
        if (originalKind == TEventKind.KeyDown && itemCount > 0)
        {
            int visibleRows = Math.Max(1, Size.Y);
            switch (originalKeyDown.ScanCode)
            {
                case ScanUp:
                    FocusItem(FocusedItem - 1);
                    @event.Clear(this);
                    return;
                case ScanDown:
                    FocusItem(FocusedItem + 1);
                    @event.Clear(this);
                    return;
                case ScanHome:
                    FocusItem(0);
                    @event.Clear(this);
                    return;
                case ScanEnd:
                    FocusItem(itemCount - 1);
                    @event.Clear(this);
                    return;
                case ScanPageUp:
                    FocusItem(FocusedItem - visibleRows);
                    @event.Clear(this);
                    return;
                case ScanPageDown:
                    FocusItem(FocusedItem + visibleRows);
                    @event.Clear(this);
                    return;
            }
        }

        if (originalKind == TEventKind.MouseDown && TryGetMouseItem(originalMouse.Where, out int mouseItem))
        {
            FocusItem(mouseItem);
            @event.Clear(this);
        }
    }

    /// <summary>
    /// Synchronisiert die Scrollbars mit Fokus und TopItem.
    ///
    /// Synchronises the scroll bars with focus and TopItem.
    /// </summary>
    protected void SyncScrollBars()
    {
        int visibleRows = Math.Max(1, Size.Y);
        int verticalLimit = Math.Max(0, GetNumItems() - visibleRows);
        VScrollBar?.SetLimit(verticalLimit);
        VScrollBar?.ScrollTo(TopItem);
        HScrollBar?.SetLimit(0);
        HScrollBar?.ScrollTo(0);
    }

    /// <summary>
    /// Prüft, ob eine Mausposition auf einen sichtbaren Eintrag zeigt.
    ///
    /// Checks whether a mouse position points to a visible item.
    /// </summary>
    /// <param name="globalPoint">Die globale Mausposition. / The global mouse position.</param>
    /// <param name="item">Der ermittelte Item-Index. / The resolved item index.</param>
    /// <returns><c>true</c>, wenn ein sichtbarer Eintrag getroffen wurde. / <c>true</c> if a visible item was hit.</returns>
    protected bool TryGetMouseItem(TPoint globalPoint, out int item)
    {
        item = -1;
        TPoint local = MakeLocal(globalPoint);
        if (!GetExtent().Contains(local))
        {
            return false;
        }

        int candidate = TopItem + local.Y;
        if (candidate < 0 || candidate >= GetNumItems())
        {
            return false;
        }

        item = candidate;
        return true;
    }
}
