// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Konkrete Listenansicht für <see cref="TStringList"/>.
///
/// Concrete list view for <see cref="TStringList"/>.
/// </summary>
public class TListBox : TListViewer
{
    /// <summary>
    /// Erstellt eine neue Listenbox.
    ///
    /// Creates a new list box.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="numCols">Die Anzahl Spalten. / The number of columns.</param>
    /// <param name="vScrollBar">Optionale vertikale Scrollbar. / Optional vertical scroll bar.</param>
    /// <param name="hScrollBar">Optionale horizontale Scrollbar. / Optional horizontal scroll bar.</param>
    public TListBox(TRect bounds, int numCols, TScrollBar? vScrollBar, TScrollBar? hScrollBar = null)
        : base(bounds, numCols, vScrollBar, hScrollBar)
    {
        Selection = -1;
    }

    /// <summary>
    /// Die Datenquelle der Liste.
    ///
    /// The data source of the list.
    /// </summary>
    public TStringList? List { get; set; }

    /// <summary>
    /// Der zuletzt bestätigte Eintrag.
    ///
    /// The last confirmed item.
    /// </summary>
    public int Selection { get; private set; }

    /// <summary>
    /// Liefert die Anzahl der Listeneinträge.
    ///
    /// Returns the number of list entries.
    /// </summary>
    /// <returns>Die Eintragszahl. / The item count.</returns>
    public override int GetNumItems() => List?.Count ?? 0;

    /// <summary>
    /// Liefert den Text eines Listeneintrags.
    ///
    /// Returns the text of a list entry.
    /// </summary>
    /// <param name="item">Der Eintragsindex. / The entry index.</param>
    /// <param name="maxChars">Die maximale Zeichenzahl. / The maximum number of characters.</param>
    /// <returns>Der geclippte Eintragstext. / The clipped entry text.</returns>
    public override string GetText(int item, int maxChars)
    {
        if (List is null || item < 0 || item >= List.Count || maxChars <= 0)
        {
            return string.Empty;
        }

        string text = List[item];
        return text.Length <= maxChars ? text : text[..maxChars];
    }

    /// <summary>
    /// Ergänzt Mausklick-Handling um die Doppelklick-Bestätigung.
    ///
    /// Extends mouse handling with double-click confirmation.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        bool isMouseDown = @event.What == TEventKind.MouseDown;
        bool isDoubleClick = isMouseDown && @event.Mouse.DoubleClick;
        int item = -1;
        bool hitItem = isMouseDown && TryGetMouseItem(@event.Mouse.Where, out item);

        base.HandleEvent(@event);

        if (isDoubleClick && hitItem)
        {
            Selection = item;
            @event.Clear(this);
        }
    }
}
