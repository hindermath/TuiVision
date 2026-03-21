// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Abstrakte Basis für gruppierte Auswahl-Controls.
/// Die Klasse kapselt Item-Verwaltung, Cursor-Navigation und die gemeinsame
/// Tastaturbehandlung für Checkboxen und Radiobuttons.
///
/// Abstract base for grouped selection controls.
/// The class encapsulates item management, cursor navigation, and the shared
/// keyboard handling for check boxes and radio buttons.
/// </summary>
public abstract class TCluster : TView
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
    /// Erstellt ein neues Cluster-Control.
    ///
    /// Creates a new cluster control.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The bounds of the control.</param>
    /// <param name="strings">Die anzuzeigenden Beschriftungen. / The labels to display.</param>
    protected TCluster(TRect bounds, string[] strings) : base(bounds)
    {
        ArgumentNullException.ThrowIfNull(strings);

        Items = strings.ToArray();
        Options |= TViewOptions.Selectable;
    }

    /// <summary>
    /// Die Optionen des Clusters.
    ///
    /// The cluster options.
    /// </summary>
    public string[] Items { get; }

    /// <summary>
    /// Der gespeicherte Cluster-Zustandswert.
    ///
    /// The stored cluster state value.
    /// </summary>
    public uint Value { get; set; }

    /// <summary>
    /// Der aktuell markierte Eintrag.
    ///
    /// The currently highlighted item.
    /// </summary>
    public int Sel { get; protected set; }

    /// <summary>
    /// Prüft, ob ein Eintrag als ausgewählt gilt.
    ///
    /// Checks whether an item is considered selected.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    /// <returns><c>true</c>, wenn das Item markiert ist. / <c>true</c> if the item is marked.</returns>
    protected abstract bool Mark(int item);

    /// <summary>
    /// Führt die Auswahlaktion für einen Eintrag aus.
    ///
    /// Executes the selection action for an item.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    protected abstract void Press(int item);

    /// <summary>
    /// Zeichnet die sichtbaren Cluster-Einträge mit Markierung.
    ///
    /// Draws the visible cluster items with their selection mark.
    /// </summary>
    public override void Draw()
    {
        TConsoleBuffer? buffer = GetDrawBuffer();
        if (buffer is null || Size.Y <= 0)
        {
            return;
        }

        int visibleItems = Math.Min(Size.Y, Items.Length);
        for (int index = 0; index < visibleItems; index++)
        {
            char cursor = index == Sel ? '>' : ' ';
            string text = $"{cursor}{(Mark(index) ? "[x]" : "[ ]")} {Items[index]}";
            ReadOnlySpan<char> visible = text.AsSpan(0, Math.Min(Size.X, text.Length));
            buffer.WriteText(Origin.X, Origin.Y + index, visible);
        }
    }

    /// <summary>
    /// Verarbeitet Tastatur-Navigation und Leertaste für die aktuelle Auswahl.
    ///
    /// Processes keyboard navigation and the space key for the current selection.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);
        if (GetState(TViewState.Disabled))
        {
            return;
        }

        if (@event.What != TEventKind.KeyDown || Items.Length == 0)
        {
            return;
        }

        switch (@event.KeyDown.ScanCode)
        {
            case ScanUp:
                Sel = Math.Max(0, Sel - 1);
                @event.Clear(this);
                return;
            case ScanDown:
                Sel = Math.Min(Items.Length - 1, Sel + 1);
                @event.Clear(this);
                return;
        }

        if (@event.KeyDown.CharCode == ' ')
        {
            Press(Sel);
            @event.Clear(this);
        }
    }
}
