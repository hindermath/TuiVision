// Deutsch: Definiert die grundlegenden View-Zustände, Optionen und die Basisklasse für die sichtbare TuiVision-Hierarchie.
// English: Defines the fundamental view states, options, and the base class for the visible TuiVision hierarchy.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Zustandsmaske für eine Ansicht.
/// Die Werte entsprechen den <c>sfXxx</c>-Konstanten aus dem originalen Turbo-Vision-C++-Code.
///
/// State mask for a view.
/// The values correspond to the <c>sfXxx</c> constants from the original Turbo Vision C++ code.
/// </summary>
[Flags]
public enum TViewState : ushort
{
    /// <summary>Die Ansicht ist sichtbar. / The view is visible.</summary>
    Visible = 0x001,

    /// <summary>Der Cursor ist sichtbar. / The cursor is visible.</summary>
    CursorVisible = 0x002,

    /// <summary>Der Cursor befindet sich im Einfügemodus. / The cursor is in insert mode.</summary>
    CursorInsert = 0x004,

    /// <summary>Die Ansicht wirft einen Schatten. / The view casts a shadow.</summary>
    Shadow = 0x008,

    /// <summary>Die Ansicht gehört zum aktiven Fenster. / The view belongs to the active window.</summary>
    Active = 0x010,

    /// <summary>Die Ansicht ist ausgewählt. / The view is selected.</summary>
    Selected = 0x020,

    /// <summary>Die Ansicht hat den Eingabefokus. / The view has input focus.</summary>
    Focused = 0x040,

    /// <summary>Die Ansicht wird gerade verschoben oder skaliert. / The view is currently being dragged or resized.</summary>
    Dragging = 0x080,

    /// <summary>Die Ansicht ist deaktiviert und nimmt keine Eingaben entgegen. / The view is disabled and does not accept input.</summary>
    Disabled = 0x100,

    /// <summary>Die Ansicht ist modal und blockiert alle anderen Eingaben. / The view is modal and blocks all other input.</summary>
    Modal = 0x200,

    /// <summary>Die Ansicht ist die Standard-Schaltfläche (z. B. bei Enter). / The view is the default button (e.g. activated by Enter).</summary>
    Default = 0x400,

    /// <summary>Die Ansicht ist nicht vollständig von anderen Ansichten verdeckt. / The view is not fully hidden behind other views.</summary>
    Exposed = 0x800
}

/// <summary>
/// Optionsmaske für das Verhalten einer Ansicht.
/// Die Werte entsprechen den <c>ofXxx</c>-Konstanten aus dem originalen Turbo-Vision-C++-Code.
///
/// Option mask for view behavior.
/// The values correspond to the <c>ofXxx</c> constants from the original Turbo Vision C++ code.
/// </summary>
[Flags]
public enum TViewOptions : ushort
{
    /// <summary>Keine Optionen gesetzt. / No options set.</summary>
    None = 0,

    /// <summary>Die Ansicht kann mit der Maus ausgewählt werden. / The view can be selected with the mouse.</summary>
    Selectable = 0x001,

    /// <summary>Auswahl bringt die Ansicht an die oberste Z-Position. / Selection brings the view to the top Z position.</summary>
    TopSelect = 0x002,

    /// <summary>Der erste Mausklick aktiviert die Ansicht und wird an sie weitergeleitet. / The first mouse click activates the view and is forwarded to it.</summary>
    FirstClick = 0x004,

    /// <summary>Die Ansicht zeichnet einen sichtbaren Rahmen um sich. / The view draws a visible frame around itself.</summary>
    Framed = 0x008,

    /// <summary>Die Ansicht empfängt Ereignisse vor ihrem Eigentümer. / The view receives events before its owner.</summary>
    PreProcess = 0x010,

    /// <summary>Die Ansicht empfängt Ereignisse nach ihrem Eigentümer. / The view receives events after its owner.</summary>
    PostProcess = 0x020,

    /// <summary>Die Ansicht verwendet einen internen Zeichenpuffer. / The view uses an internal draw buffer.</summary>
    Buffered = 0x040,

    /// <summary>Die Ansicht kann nebeneinander (gekachelt) angeordnet werden. / The view can be arranged in a tiled layout.</summary>
    Tileable = 0x080,

    /// <summary>Die Ansicht wird horizontal im Eigentümer zentriert. / The view is centered horizontally within its owner.</summary>
    CenterX = 0x100,

    /// <summary>Die Ansicht wird vertikal im Eigentümer zentriert. / The view is centered vertically within its owner.</summary>
    CenterY = 0x200,

    /// <summary>Die Ansicht wird horizontal und vertikal zentriert. / The view is centered both horizontally and vertically.</summary>
    Centered = CenterX | CenterY,

    /// <summary>Ausführliche Diagnoseausgabe ist aktiviert. / Verbose diagnostic output is enabled.</summary>
    Verbose = 0x400
}

/// <summary>
/// Grundlegendes visuelles Element, portiert von <c>TView</c> aus Turbo Vision.
/// Alle sichtbaren Elemente der Oberfläche erben von dieser Klasse und erben damit
/// Koordinatenverwaltung, Zustandsflags und Ereignisverarbeitung.
///
/// Fundamental visual element ported from <c>TView</c> in Turbo Vision.
/// All visible elements of the user interface inherit from this class, thereby
/// inheriting coordinate management, state flags, and event handling.
/// </summary>
public class TView : TObject
{
    /// <summary>
    /// Maximale Standardgröße einer Ansicht in beiden Dimensionen.
    /// Entspricht dem größtmöglichen Wert für <see cref="short"/>, damit Ansichten
    /// nicht über den darstellbaren Bereich hinauswachsen.
    ///
    /// Maximum default size of a view in both dimensions.
    /// Corresponds to the largest possible value of <see cref="short"/>, ensuring
    /// that views do not grow beyond the renderable area.
    /// </summary>
    private static readonly TPoint MaxDefaultSize = new(short.MaxValue, short.MaxValue);

    /// <summary>
    /// Der aktuelle Begrenzungsrahmen der Ansicht in den Koordinaten des Eigentümers.
    /// Er wird bei jedem Aufruf von <see cref="Locate"/> aktualisiert.
    ///
    /// The current bounding rectangle of the view in the owner's coordinate space.
    /// It is updated on every call to <see cref="Locate"/>.
    /// </summary>
    private TRect _bounds;

    /// <summary>
    /// Erstellt eine neue Ansicht mit dem angegebenen Begrenzungsrahmen.
    /// Der anfängliche Zustand ist <see cref="TViewState.Visible"/>;
    /// die Ereignismaske reagiert auf Mausklicks, Tastenanschläge und Befehle.
    ///
    /// Creates a new view with the specified bounding rectangle.
    /// The initial state is <see cref="TViewState.Visible"/>;
    /// the event mask responds to mouse clicks, key presses, and commands.
    /// </summary>
    /// <param name="bounds">
    /// Der anfängliche Begrenzungsrahmen in den Koordinaten des Eigentümers.
    /// Breite und Höhe dürfen nicht negativ sein.
    ///
    /// The initial bounding rectangle in the owner's coordinate space.
    /// Width and height must not be negative.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn die Breite oder Höhe von <paramref name="bounds"/> negativ ist.
    /// Thrown when the width or height of <paramref name="bounds"/> is negative.
    /// </exception>
    public TView(TRect bounds)
    {
        ValidateBounds(bounds);
        _bounds = bounds;
        EventMask = TEventKind.MouseDown | TEventKind.KeyDown | TEventKind.Command;
        State = TViewState.Visible;
        Options = TViewOptions.None;
    }

    /// <summary>
    /// Die Eigentümer-Gruppe dieser Ansicht, oder <c>null</c>, wenn die Ansicht keiner Gruppe angehört.
    /// Wird von <see cref="TGroup"/> beim Einfügen gesetzt und beim Entfernen auf <c>null</c> zurückgesetzt.
    ///
    /// The owner group of this view, or <c>null</c> if the view does not belong to any group.
    /// Set by <see cref="TGroup"/> on insertion and reset to <c>null</c> on removal.
    /// </summary>
    public TGroup? Owner { get; internal set; }

    /// <summary>
    /// Zeiger auf die nächste Ansicht in der zirkulären Kind-Liste der Eigentümer-Gruppe.
    /// Nur für <see cref="TGroup"/> zugänglich; <c>null</c>, wenn die Ansicht keiner Gruppe angehört.
    ///
    /// Pointer to the next view in the owner group's circular child list.
    /// Accessible only to <see cref="TGroup"/>; <c>null</c> when the view does not belong to a group.
    /// </summary>
    internal TView? Next { get; set; }

    /// <summary>
    /// Die Verhaltensoptionen dieser Ansicht.
    ///
    /// The behavior options of this view.
    /// </summary>
    public TViewOptions Options { get; set; }

    /// <summary>
    /// Das aktuell auf diese View angewendete Farbschema.
    ///
    /// The colour scheme currently applied to this view.
    /// </summary>
    public TColorScheme ColorScheme { get; private set; } = TColorScheme.Default;

    /// <summary>
    /// Wendet ein Farbschema explizit auf diese View an.
    ///
    /// Explicitly applies a colour scheme to this view.
    /// </summary>
    /// <param name="colorScheme">Das anzuwendende Schema. / The scheme to apply.</param>
    public virtual void ApplyColorScheme(TColorScheme colorScheme)
    {
        ArgumentNullException.ThrowIfNull(colorScheme);
        ColorScheme = colorScheme;
        DrawView();
    }

    /// <summary>
    /// Bitmaske der Ereignistypen, auf die diese Ansicht reagiert.
    ///
    /// Bit mask of event types that this view responds to.
    /// </summary>
    public TEventKind EventMask { get; set; }

    /// <summary>
    /// Die aktuellen Zustandsflags dieser Ansicht.
    ///
    /// The current state flags of this view.
    /// </summary>
    public TViewState State { get; private set; }

    /// <summary>
    /// Die Position der oberen linken Ecke der Ansicht im Koordinatenraum des Eigentümers.
    ///
    /// The position of the top-left corner of the view in the owner's coordinate space.
    /// </summary>
    public TPoint Origin => _bounds.A;

    /// <summary>
    /// Die Größe der Ansicht als Breite-Höhe-Vektor.
    ///
    /// The size of the view as a width-height vector.
    /// </summary>
    public TPoint Size => _bounds.B - _bounds.A;

    /// <summary>
    /// Gibt den aktuellen Begrenzungsrahmen in den Koordinaten des Eigentümers zurück.
    ///
    /// Returns the current bounding rectangle in the owner's coordinate space.
    /// </summary>
    /// <returns>
    /// Der Begrenzungsrahmen dieser Ansicht.
    /// The bounding rectangle of this view.
    /// </returns>
    public TRect GetBounds() => _bounds;

    /// <summary>
    /// Gibt den lokalen Ausdehnungsrahmen zurück, der immer bei (0,0) beginnt.
    ///
    /// Returns the local extent rectangle, which always starts at (0,0).
    /// </summary>
    /// <returns>
    /// Ein Rechteck von (0,0) bis zur Größe (<see cref="Size"/>) dieser Ansicht.
    /// A rectangle from (0,0) to the size (<see cref="Size"/>) of this view.
    /// </returns>
    public TRect GetExtent() => new(0, 0, Size.X, Size.Y);

    /// <summary>
    /// Prüft, ob alle Bits der angegebenen Zustandsmaske in <see cref="State"/> gesetzt sind.
    ///
    /// Checks whether all bits of the specified state mask are set in <see cref="State"/>.
    /// </summary>
    /// <param name="state">Die zu prüfende Zustandsmaske. / The state mask to check.</param>
    /// <returns>
    /// <c>true</c>, wenn alle Bits der Maske gesetzt sind; andernfalls <c>false</c>.
    /// <c>true</c> if all bits of the mask are set; otherwise <c>false</c>.
    /// </returns>
    public bool GetState(TViewState state) => (State & state) == state;

    /// <summary>
    /// Macht die Ansicht sichtbar, indem das <see cref="TViewState.Visible"/>-Flag gesetzt wird.
    ///
    /// Makes the view visible by setting the <see cref="TViewState.Visible"/> flag.
    /// </summary>
    public void Show() => SetState(TViewState.Visible, true);

    /// <summary>
    /// Versteckt die Ansicht, indem das <see cref="TViewState.Visible"/>-Flag gelöscht wird.
    ///
    /// Hides the view by clearing the <see cref="TViewState.Visible"/> flag.
    /// </summary>
    public void Hide() => SetState(TViewState.Visible, false);

    /// <summary>
    /// Setzt oder löscht die angegebenen Zustandsbits in <see cref="State"/>.
    ///
    /// Sets or clears the specified state bits in <see cref="State"/>.
    /// </summary>
    /// <param name="state">Die zu ändernden Zustandsbits. / The state bits to change.</param>
    /// <param name="enable">
    /// <c>true</c>, um die Bits zu setzen; <c>false</c>, um sie zu löschen.
    /// <c>true</c> to set the bits; <c>false</c> to clear them.
    /// </param>
    public virtual void SetState(TViewState state, bool enable) =>
        State = enable ? State | state : State & ~state;

    /// <summary>
    /// Gibt die minimale und maximale Größe zurück, auf die diese Ansicht skaliert werden darf.
    /// Abgeleitete Klassen überschreiben diese Methode, um Größenbeschränkungen festzulegen.
    ///
    /// Returns the minimum and maximum size to which this view may be resized.
    /// Derived classes override this method to define size constraints.
    /// </summary>
    /// <param name="min">
    /// Wird mit der Mindestgröße belegt.
    /// Receives the minimum size.
    /// </param>
    /// <param name="max">
    /// Wird mit der Maximalgröße belegt.
    /// Receives the maximum size.
    /// </param>
    public virtual void SizeLimits(out TPoint min, out TPoint max)
    {
        min = new TPoint(0, 0);
        max = MaxDefaultSize;
    }

    /// <summary>
    /// Setzt den Begrenzungsrahmen und passt ihn an die Größengrenzen von <see cref="SizeLimits"/> an.
    ///
    /// Sets the bounding rectangle and clamps it to the size limits from <see cref="SizeLimits"/>.
    /// </summary>
    /// <param name="bounds">
    /// Der gewünschte neue Begrenzungsrahmen.
    /// The desired new bounding rectangle.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="bounds"/> eine negative Breite oder Höhe hat.
    /// Thrown when <paramref name="bounds"/> has a negative width or height.
    /// </exception>
    public void Locate(TRect bounds)
    {
        ValidateBounds(bounds);
        SizeLimits(out TPoint min, out TPoint max);

        int width = Math.Clamp(bounds.Width, min.X, max.X);
        int height = Math.Clamp(bounds.Height, min.Y, max.Y);

        _bounds = new TRect(bounds.A.X, bounds.A.Y, bounds.A.X + width, bounds.A.Y + height);
        OnBoundsChanged();
    }

    /// <summary>
    /// Verschiebt die Ansicht auf die angegebene Position, ohne ihre Größe zu ändern.
    ///
    /// Moves the view to the specified position without changing its size.
    /// </summary>
    /// <param name="x">Die neue X-Position (Spalte). / The new X position (column).</param>
    /// <param name="y">Die neue Y-Position (Zeile). / The new Y position (row).</param>
    public void MoveTo(int x, int y)
    {
        TPoint size = Size;
        Locate(new TRect(x, y, x + size.X, y + size.Y));
    }

    /// <summary>
    /// Ändert die Größe der Ansicht auf die angegebenen Abmessungen, ohne sie zu verschieben.
    ///
    /// Resizes the view to the specified dimensions without moving it.
    /// </summary>
    /// <param name="x">Die neue Breite in Zeichen. / The new width in characters.</param>
    /// <param name="y">Die neue Höhe in Zeichen. / The new height in characters.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Wird ausgelöst, wenn <paramref name="x"/> oder <paramref name="y"/> negativ ist.
    /// Thrown when <paramref name="x"/> or <paramref name="y"/> is negative.
    /// </exception>
    public void GrowTo(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Size must be non-negative.");
        }

        Locate(new TRect(Origin.X, Origin.Y, Origin.X + x, Origin.Y + y));
    }

    /// <summary>
    /// Setzt den Begrenzungsrahmen direkt. Entspricht einem Aufruf von <see cref="Locate"/>.
    ///
    /// Sets the bounding rectangle directly. Equivalent to calling <see cref="Locate"/>.
    /// </summary>
    /// <param name="bounds">Der neue Begrenzungsrahmen. / The new bounding rectangle.</param>
    public void SetBounds(TRect bounds) => Locate(bounds);

    /// <summary>
    /// Wandelt einen lokalen Punkt in globale Bildschirmkoordinaten um.
    ///
    /// Converts a local point to global screen coordinates.
    /// </summary>
    /// <param name="source">Der lokale Punkt. / The local point.</param>
    /// <returns>
    /// Der Punkt in globalen Koordinaten.
    /// The point in global coordinates.
    /// </returns>
    public TPoint MakeGlobal(TPoint source) =>
        Owner is null ? source + Origin : Owner.MakeGlobal(source + Origin);

    /// <summary>
    /// Wandelt einen globalen Punkt in lokale Ansichtskoordinaten um.
    ///
    /// Converts a global point to local view coordinates.
    /// </summary>
    /// <param name="source">Der globale Punkt. / The global point.</param>
    /// <returns>
    /// Der Punkt in lokalen Koordinaten.
    /// The point in local coordinates.
    /// </returns>
    public TPoint MakeLocal(TPoint source) => source - MakeGlobal(new TPoint(0, 0));

    /// <summary>
    /// Prüft, ob eine globale Mausposition innerhalb dieser Ansicht liegt.
    ///
    /// Checks whether a global mouse position lies within this view.
    /// </summary>
    /// <param name="mouse">Die globale Mausposition. / The global mouse position.</param>
    /// <returns>
    /// <c>true</c>, wenn die Maus innerhalb der Ansicht liegt; andernfalls <c>false</c>.
    /// <c>true</c> if the mouse is within the view; otherwise <c>false</c>.
    /// </returns>
    public bool MouseInView(TPoint mouse)
    {
        TPoint local = MakeLocal(mouse);
        return GetExtent().Contains(local);
    }

    /// <summary>
    /// Zeichnet den Inhalt dieser Ansicht.
    /// Die Basisimplementierung ist ein No-Op; abgeleitete Klassen überschreiben diese Methode,
    /// um ihren Inhalt in den Puffer der Eigentümer-Gruppe zu schreiben.
    /// <see cref="DrawView"/> ist die Template-Methode; abgeleitete Klassen überschreiben
    /// ausschließlich <see cref="Draw"/>.
    ///
    /// Draws the content of this view.
    /// The base implementation is a no-op; derived classes override this method
    /// to write their content into the owner group's buffer.
    /// <see cref="DrawView"/> is the template method; derived classes override
    /// only <see cref="Draw"/>.
    /// </summary>
    public virtual void Draw() { }

    /// <summary>
    /// Template-Methode: Prüft Sichtbarkeit und Lock-Status der Eigentümer-Gruppe,
    /// bevor sie <see cref="Draw"/> aufruft.
    /// Sichtbar = <see cref="GetState"/>(<see cref="TViewState.Visible"/>) == <c>true</c>.
    /// Ist <see cref="Owner"/> nicht <c>null</c> und gesperrt (<c>IsLocked</c>), wird nicht gezeichnet.
    ///
    /// Template method: checks visibility and the owner group's lock status
    /// before calling <see cref="Draw"/>.
    /// Visible = <see cref="GetState"/>(<see cref="TViewState.Visible"/>) == <c>true</c>.
    /// If <see cref="Owner"/> is not <c>null</c> and locked (<c>IsLocked</c>), drawing is skipped.
    /// </summary>
    public void DrawView()
    {
        if (!GetState(TViewState.Visible) || (Owner?.IsLocked ?? false))
        {
            return;
        }

        Draw();
    }

    /// <summary>
    /// Gibt den Zeichenpuffer der Eigentümergruppe zurück, wenn einer verfügbar ist.
    /// Controls verwenden diese Methode, um innerhalb ihrer <see cref="Draw"/>-Implementierung
    /// in den Puffer der umgebenden Gruppe zu schreiben.
    ///
    /// Returns the owning group's draw buffer when one is available.
    /// Controls use this method to write into the surrounding group's buffer from
    /// within their <see cref="Draw"/> implementation.
    /// </summary>
    /// <returns>
    /// Der Zeichenpuffer der Eigentümergruppe oder <c>null</c>, wenn keine Gruppe
    /// oder kein Puffer verfügbar ist.
    /// The owning group's draw buffer, or <c>null</c> when no group or buffer is available.
    /// </returns>
    protected TConsoleBuffer? GetDrawBuffer() => Owner?.GetDrawBuffer();

    /// <summary>
    /// Der Hilfekontext-Bezeichner dieser Ansicht.
    /// Wird von der Statuszeile verwendet, um die passende <see cref="TStatusDef"/>-Kette
    /// auszuwählen (First-Match-Wins). Standardwert ist 0.
    ///
    /// The help-context identifier of this view.
    /// Used by the status line to select the matching <see cref="TStatusDef"/> chain
    /// (first-match-wins). Default value is 0.
    /// </summary>
    public virtual int HelpContext { get; set; }

    /// <summary>
    /// Gibt die Status-Hinweise für diese Ansicht zurück.
    ///
    /// Returns the status hints for this view.
    /// </summary>
    /// <returns>Eine Kette von <see cref="TStatusItem"/>s oder <c>null</c>. / A chain of <see cref="TStatusItem"/>s or <c>null</c>.</returns>
    public virtual TStatusItem? GetStatusHints()
    {
        return null;
    }

    /// <summary>
    /// Verarbeitet ein eingehendes Ereignis.
    /// Bei Mausklick wird die Ansicht ausgewählt, wenn sie selektierbar ist.
    /// Abgeleitete Klassen überschreiben diese Methode, um zusätzliche Ereignistypen zu verarbeiten.
    ///
    /// Processes an incoming event.
    /// On mouse button press, the view is selected if it is selectable.
    /// Derived classes override this method to handle additional event types.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public virtual void HandleEvent(TEvent @event)
    {
        // FR-019: Deaktivierte Ansichten ignorieren alle Ereignisse (konform zum C++-Original).
        // FR-019: Disabled views ignore all events (conforming to the C++ original).
        if (GetState(TViewState.Disabled))
        {
            return;
        }

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

    /// <summary>
    /// Wird aufgerufen, nachdem der Begrenzungsrahmen durch <see cref="Locate"/> geändert wurde.
    /// Abgeleitete Klassen überschreiben diese Methode, um auf Größen- oder Positionsänderungen zu reagieren.
    ///
    /// Called after the bounding rectangle has been changed by <see cref="Locate"/>.
    /// Derived classes override this method to react to size or position changes.
    /// </summary>
    protected virtual void OnBoundsChanged()
    {
    }

    /// <summary>
    /// Prüft, ob ein Begrenzungsrahmen keine negativen Abmessungen hat.
    /// Wird im Konstruktor und in <see cref="Locate"/> aufgerufen, bevor der Rahmen übernommen wird.
    ///
    /// Validates that a bounding rectangle has no negative dimensions.
    /// Called in the constructor and in <see cref="Locate"/> before the bounds are applied.
    /// </summary>
    /// <param name="bounds">Der zu prüfende Rahmen. / The bounds to validate.</param>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn die Breite oder Höhe negativ ist.
    /// Thrown when the width or height is negative.
    /// </exception>
    private static void ValidateBounds(TRect bounds)
    {
        if (bounds.B.X < bounds.A.X || bounds.B.Y < bounds.A.Y)
        {
            throw new ArgumentException("Bounds must not have negative width or height.", nameof(bounds));
        }
    }
}
