// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Container-Ansicht, die Kind-Views in einer zirkulären doppelt-verlinkten Liste verwaltet.
/// <see cref="TGroup"/> implementiert Einfügen/Entfernen, Drei-Phasen-Ereignis-Dispatching,
/// Fokus-Management, Zeichenpuffer-Lifecycle und State-Propagation.
/// Portiert von <c>TGroup</c> aus Turbo Vision 2.0.3.
///
/// Container view that manages child views in a circular singly-linked list.
/// <see cref="TGroup"/> implements insertion/removal, three-phase event dispatching,
/// focus management, draw-buffer lifecycle, and state propagation.
/// Ported from <c>TGroup</c> in Turbo Vision 2.0.3.
/// </summary>
public class TGroup : TView
{
    // -------------------------------------------------------------------------
    // Private Felder / Private fields
    // -------------------------------------------------------------------------

    /// <summary>
    /// Zeiger auf das zuletzt eingefügte Kind-Element der zirkulären Liste,
    /// oder <c>null</c>, wenn die Gruppe leer ist.
    /// Invariante: <c>_last.Next == First()</c> — die Liste ist immer geschlossen.
    ///
    /// Pointer to the last inserted child in the circular list,
    /// or <c>null</c> if the group is empty.
    /// Invariant: <c>_last.Next == First()</c> — the list is always closed.
    /// </summary>
    private TView? _last;

    /// <summary>
    /// Zähler für verschachtelte <see cref="LockDraw"/>-Aufrufe.
    /// Zeichnen ist gesperrt, solange dieser Wert größer als 0 ist.
    /// Jeder <see cref="UnlockDraw"/>-Aufruf dekrementiert diesen Wert;
    /// nur wenn er 0 erreicht, wird <see cref="TView.DrawView"/> einmal aufgerufen.
    ///
    /// Counter for nested <see cref="LockDraw"/> calls.
    /// Drawing is locked as long as this value is greater than 0.
    /// Each <see cref="UnlockDraw"/> call decrements this value;
    /// only when it reaches 0 is <see cref="TView.DrawView"/> called once.
    /// </summary>
    private int _lockFlag;

    /// <summary>
    /// Der interne Zeichenpuffer dieser Gruppe, oder <c>null</c>, wenn noch kein Puffer
    /// alloziert wurde. Wird bei der ersten Zeichenoperation angelegt, wenn
    /// <see cref="TViewOptions.Buffered"/> gesetzt und die Gruppe exponiert ist.
    ///
    /// The internal draw buffer of this group, or <c>null</c> if no buffer has been
    /// allocated yet. Created on the first draw operation when
    /// <see cref="TViewOptions.Buffered"/> is set and the group is exposed.
    /// </summary>
    private TConsoleBuffer? _buffer;

    /// <summary>
    /// Das aktuelle Clipping-Rechteck dieser Gruppe in lokalen Koordinaten.
    /// Wird im Konstruktor und in <see cref="OnBoundsChanged"/> aktualisiert.
    ///
    /// The current clipping rectangle of this group in local coordinates.
    /// Updated in the constructor and in <see cref="OnBoundsChanged"/>.
    /// </summary>
    private TRect _clip;

    // -------------------------------------------------------------------------
    // Konstruktor / Constructor
    // -------------------------------------------------------------------------

    /// <summary>
    /// Erstellt eine neue Gruppe mit dem angegebenen Begrenzungsrahmen.
    /// Setzt <see cref="TViewOptions.Selectable"/> und <see cref="TViewOptions.Buffered"/>.
    ///
    /// Creates a new group with the specified bounding rectangle.
    /// Sets <see cref="TViewOptions.Selectable"/> and <see cref="TViewOptions.Buffered"/>.
    /// </summary>
    /// <param name="bounds">
    /// Der anfängliche Begrenzungsrahmen in den Koordinaten des Eigentümers.
    /// The initial bounding rectangle in the owner's coordinate space.
    /// </param>
    public TGroup(TRect bounds) : base(bounds)
    {
        Options |= TViewOptions.Selectable | TViewOptions.Buffered;
        _clip = GetExtent();
    }

    // -------------------------------------------------------------------------
    // Eigenschaften / Properties
    // -------------------------------------------------------------------------

    /// <summary>
    /// Die aktuell fokussierte Kind-View, oder <c>null</c>, wenn keine View fokussiert ist.
    /// Initial: <c>null</c>; wird nicht automatisch bei <see cref="Insert"/> gesetzt.
    ///
    /// The currently focused child view, or <c>null</c> if no view is focused.
    /// Initial: <c>null</c>; not automatically set by <see cref="Insert"/>.
    /// </summary>
    public TView? Current { get; private set; }

    /// <summary>
    /// Die aktuelle Dispatch-Phase des Drei-Phasen-Ereignis-Dispatching.
    ///
    /// The current dispatch phase of the three-phase event dispatching.
    /// </summary>
    internal DrawPhase Phase { get; private set; }

    /// <summary>
    /// Gibt an, ob das Zeichnen durch <see cref="LockDraw"/> gesperrt ist.
    /// <c>true</c>, wenn mindestens ein ungeglichener <see cref="LockDraw"/>-Aufruf vorliegt.
    ///
    /// Indicates whether drawing is locked by <see cref="LockDraw"/>.
    /// <c>true</c> if at least one unmatched <see cref="LockDraw"/> call exists.
    /// </summary>
    internal bool IsLocked => _lockFlag > 0;

    // -------------------------------------------------------------------------
    // Öffentliche Methoden / Public methods
    // -------------------------------------------------------------------------

    /// <summary>
    /// Fügt <paramref name="view"/> am Ende der Kind-Liste ein und setzt ihren
    /// <see cref="TView.Owner"/> auf diese Gruppe.
    ///
    /// <para>
    /// <b>Algorithmus</b>: Die Kind-Liste ist eine zirkuläre Einfach-Liste.
    /// Jedes neue Element wird nach <c>_last</c> eingefügt und wird das neue <c>_last</c>.
    /// <c>First() = _last.Next</c> zeigt immer auf das erste (älteste) Element.
    /// </para>
    ///
    /// Inserts <paramref name="view"/> at the end of the child list and sets its
    /// <see cref="TView.Owner"/> to this group.
    ///
    /// <para>
    /// <b>Algorithm</b>: The child list is a circular singly-linked list.
    /// Each new element is inserted after <c>_last</c> and becomes the new <c>_last</c>.
    /// <c>First() = _last.Next</c> always points to the first (oldest) element.
    /// </para>
    /// </summary>
    /// <param name="view">
    /// Die einzufügende Kind-View. Darf nicht <c>null</c> sein, darf nicht bereits einer
    /// anderen Gruppe angehören und darf nicht die Gruppe selbst sein.
    ///
    /// The child view to insert. Must not be <c>null</c>, must not already belong to
    /// another group, and must not be the group itself.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="view"/> <c>null</c> ist.
    /// Thrown when <paramref name="view"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="view"/> bereits einen Eigentümer hat
    /// oder wenn <paramref name="view"/> die Gruppe selbst ist.
    ///
    /// Thrown when <paramref name="view"/> already has an owner,
    /// or when <paramref name="view"/> is the group itself.
    /// </exception>
    public void Insert(TView view)
    {
        // GUARD: null check
        ArgumentNullException.ThrowIfNull(view);

        // GUARD: Selbst-Insert / Self-insert
        if (view == this)
        {
            throw new ArgumentException("A group cannot be inserted into itself.", nameof(view));
        }

        // GUARD: bereits Mitglied / already member
        if (view.Owner != null)
        {
            throw new ArgumentException("The view already belongs to a group.", nameof(view));
        }

        view.Owner = this;

        // Zirkuläre Einfügung nach _last / Circular insertion after _last
        if (_last == null)
        {
            // Erste View: Selbst-Loop bilden / First view: create self-loop
            view.Next = view;
            _last = view;
        }
        else
        {
            // Nach _last einfügen, neue View wird _last / Insert after _last, new view becomes _last
            view.Next = _last.Next;
            _last.Next = view;
            _last = view;
        }

        // Zustandsflags von der Eigentümergruppe auf die neue View übertragen (Exposed, Active, Focused).
        // Propagate state flags from the owner group to the newly inserted view (Exposed, Active, Focused).
        TViewState inherited = TViewState.Exposed | TViewState.Active | TViewState.Focused;
        TViewState ownerState = State & inherited;
        if (ownerState != 0)
        {
            view.SetState(ownerState, true);
        }
    }

    /// <summary>
    /// Entfernt <paramref name="view"/> aus der Kind-Liste und setzt ihren
    /// <see cref="TView.Owner"/> auf <c>null</c>.
    /// Eine Exception in <see cref="Remove"/> lässt die Kind-Liste unverändert.
    ///
    /// Removes <paramref name="view"/> from the child list and sets its
    /// <see cref="TView.Owner"/> to <c>null</c>.
    /// An exception in <see cref="Remove"/> leaves the child list unchanged.
    /// </summary>
    /// <param name="view">
    /// Die zu entfernende Kind-View. Darf nicht <c>null</c> sein und
    /// muss dieser Gruppe angehören.
    ///
    /// The child view to remove. Must not be <c>null</c> and must belong to this group.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="view"/> <c>null</c> ist.
    /// Thrown when <paramref name="view"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="view"/> nicht dieser Gruppe angehört.
    /// Thrown when <paramref name="view"/> does not belong to this group.
    /// </exception>
    public void Remove(TView view)
    {
        // GUARD: null check
        ArgumentNullException.ThrowIfNull(view);

        // GUARD: Mitgliedschaft / Membership
        if (view.Owner != this)
        {
            throw new ArgumentException("The view does not belong to this group.", nameof(view));
        }

        // Vorgänger suchen (O(n)) / Find predecessor (O(n))
        TView prev = FindPrev(view);

        if (view == view.Next)
        {
            // Einziges Element / Only element
            _last = null;
        }
        else
        {
            prev.Next = view.Next;
            if (_last == view)
            {
                _last = prev;
            }
        }

        view.Next = null;
        view.Owner = null;

        if (Current == view)
        {
            ResetCurrent();
        }
    }

    /// <summary>
    /// Fährt alle Kind-Views in umgekehrter Einfügereihenfolge (LIFO) herunter und
    /// entfernt sie aus der Gruppe; danach wird <see cref="TObject.ShutDown"/> der Gruppe selbst aufgerufen.
    ///
    /// Shuts down all child views in reverse insertion order (LIFO) and removes them from the group;
    /// then calls <see cref="TObject.ShutDown"/> on the group itself.
    /// </summary>
    public override void ShutDown()
    {
        // LIFO-Traversal: rückwärts von _last bis First() / LIFO traversal: backwards from _last to First()
        while (_last != null)
        {
            TView child = _last;
            Remove(child);
            child.ShutDown();
        }

        base.ShutDown();
    }

    /// <summary>
    /// Überträgt den Eingabefokus auf <paramref name="view"/>.
    /// Ist die View bereits fokussiert, ist die Methode ein No-Op.
    ///
    /// Transfers input focus to <paramref name="view"/>.
    /// If the view is already focused, this method is a no-op.
    /// </summary>
    /// <param name="view">
    /// Die zu fokussierende Kind-View. Darf nicht <c>null</c> sein und
    /// muss dieser Gruppe angehören.
    ///
    /// The child view to focus. Must not be <c>null</c> and must belong to this group.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="view"/> <c>null</c> ist.
    /// Thrown when <paramref name="view"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Wird ausgelöst, wenn <paramref name="view"/> nicht dieser Gruppe angehört.
    /// Thrown when <paramref name="view"/> does not belong to this group.
    /// </exception>
    public void SetFocus(TView view)
    {
        ArgumentNullException.ThrowIfNull(view);

        if (view.Owner != this)
        {
            throw new ArgumentException("The view does not belong to this group.", nameof(view));
        }

        if (Current == view)
        {
            return;
        }

        Current?.SetState(TViewState.Focused, false);
        Current = view;
        Current.SetState(TViewState.Focused, true);
        CurrentChanged();
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich die aktuell fokussierte Ansicht (<see cref="Current"/>) ändert.
    /// Abgeleitete Klassen können diese Methode überschreiben, um auf Fokuswechsel zu reagieren.
    ///
    /// Called when the currently focused view (<see cref="Current"/>) changes.
    /// Derived classes can override this method to react to focus changes.
    /// </summary>
    protected virtual void CurrentChanged()
    {
    }

    /// <summary>
    /// Bewegt den Fokus zur nächsten (oder vorherigen) auswählbaren, sichtbaren und nicht
    /// deaktivierten Kind-View in der zirkulären Reihenfolge.
    /// Bei leerer Gruppe oder wenn keine passende View gefunden wird, ist die Methode ein No-Op.
    ///
    /// Moves focus to the next (or previous) selectable, visible, and non-disabled
    /// child view in circular order.
    /// If the group is empty or no suitable view is found, this method is a no-op.
    /// </summary>
    /// <param name="forward">
    /// <c>true</c>, um vorwärts zu navigieren; <c>false</c>, um rückwärts zu navigieren.
    /// <c>true</c> to navigate forward; <c>false</c> to navigate backward.
    /// </param>
    public void SelectNext(bool forward)
    {
        if (_last == null)
        {
            return;
        }

        TView start = Current ?? First()!;
        TView candidate = forward ? start.Next! : FindPrev(start);

        // Die Kinderliste ist zirkulär; die feste Obergrenze schützt auch bei ausschließlich ungeeigneten Views.
        // The child list is circular; the fixed bound also protects when every view is ineligible.
        int count = 0;
        int total = CountChildren();
        while (count < total)
        {
            if (candidate.Options.HasFlag(TViewOptions.Selectable)
                && candidate.GetState(TViewState.Visible)
                && !candidate.GetState(TViewState.Disabled))
            {
                SetFocus(candidate);
                return;
            }

            candidate = forward ? candidate.Next! : FindPrev(candidate);
            count++;

            if (candidate == start)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Zeichnet alle sichtbaren Kind-Views in Einfügereihenfolge (Z-Reihenfolge).
    /// Alloziert den Zeichenpuffer, wenn <see cref="TViewOptions.Buffered"/> gesetzt ist
    /// und die Gruppe exponiert ist.
    ///
    /// Draws all visible child views in insertion order (Z-order).
    /// Allocates the draw buffer if <see cref="TViewOptions.Buffered"/> is set
    /// and the group is exposed.
    /// </summary>
    public override void Draw()
    {
        // Puffer allozieren falls nötig / Allocate buffer if needed
        if (_buffer == null && Options.HasFlag(TViewOptions.Buffered) && GetState(TViewState.Exposed))
        {
            _buffer = new TConsoleBuffer(Size.X, Size.Y);
        }

        ForEach(v =>
        {
            if (v.GetState(TViewState.Visible))
            {
                v.DrawView();
            }
        });

        // Puffer der Kind-Gruppen in eigenen Puffer einblenden (Compositing).
        // Composite child-group buffers into own buffer (Compositing).
        if (_buffer != null)
        {
            ForEach(v =>
            {
                if (v is TGroup childGroup && childGroup._buffer != null)
                {
                    int offX = v.Origin.X;
                    int offY = v.Origin.Y;
                    for (int gy = 0; gy < childGroup._buffer.Height; gy++)
                    {
                        for (int gx = 0; gx < childGroup._buffer.Width; gx++)
                        {
                            _buffer.TrySetCell(offX + gx, offY + gy, childGroup._buffer.GetCell(gx, gy));
                        }
                    }
                }
            });
        }
    }

    /// <summary>
    /// Liefert den internen Zeichenpuffer der Gruppe und allokiert ihn bei Bedarf.
    /// Die Methode ist für untergeordnete Controls innerhalb desselben Assemblies gedacht.
    ///
    /// Returns the group's internal draw buffer and allocates it on demand.
    /// The method is intended for child controls within the same assembly.
    /// </summary>
    /// <returns>
    /// Der interne Zeichenpuffer oder <c>null</c>, wenn die Gruppe nicht exponiert ist.
    /// The internal draw buffer, or <c>null</c> if the group is not exposed.
    /// </returns>
    protected internal new TConsoleBuffer? GetDrawBuffer()
    {
        if (_buffer == null && Options.HasFlag(TViewOptions.Buffered) && GetState(TViewState.Exposed))
        {
            _buffer = new TConsoleBuffer(Size.X, Size.Y);
        }

        return _buffer;
    }

    /// <summary>
    /// Sperrt das Zeichnen dieser Gruppe. Für jeden Aufruf von <see cref="LockDraw"/>
    /// muss ein entsprechender Aufruf von <see cref="UnlockDraw"/> erfolgen.
    ///
    /// Locks drawing for this group. Each call to <see cref="LockDraw"/>
    /// must be matched by a corresponding call to <see cref="UnlockDraw"/>.
    /// </summary>
    public void LockDraw() => _lockFlag++;

    /// <summary>
    /// Entsperrt das Zeichnen. Erst wenn der Lock-Zähler 0 erreicht, wird
    /// <see cref="TView.DrawView"/> genau einmal aufgerufen — unabhängig von der Anzahl
    /// gesperrter Aufrufe.
    ///
    /// Unlocks drawing. Only when the lock counter reaches 0 is
    /// <see cref="TView.DrawView"/> called exactly once — regardless of how many
    /// locked draw calls were accumulated.
    /// </summary>
    public void UnlockDraw()
    {
        if (_lockFlag > 0)
        {
            _lockFlag--;
        }

        if (_lockFlag == 0)
        {
            DrawView();
        }
    }

    /// <summary>
    /// Setzt oder löscht Zustandsbits und propagiert den neuen Zustand an alle Kind-Views,
    /// wenn das Bit <see cref="TViewState.Active"/>, <see cref="TViewState.Focused"/> oder
    /// <see cref="TViewState.Disabled"/> betroffen ist.
    /// Verschachtelte Gruppen erhalten den State als eine Kind-View und propagieren ihn
    /// intern eigenständig.
    ///
    /// Sets or clears state bits and propagates the new state to all child views
    /// when the bit is <see cref="TViewState.Active"/>, <see cref="TViewState.Focused"/>, or
    /// <see cref="TViewState.Disabled"/>.
    /// Nested groups receive the state as a child view and propagate it internally on their own.
    /// </summary>
    /// <param name="state">Die zu ändernden Zustandsbits. / The state bits to change.</param>
    /// <param name="enable">
    /// <c>true</c>, um die Bits zu setzen; <c>false</c>, um sie zu löschen.
    /// <c>true</c> to set the bits; <c>false</c> to clear them.
    /// </param>
    public override void SetState(TViewState state, bool enable)
    {
        base.SetState(state, enable);

        if ((state & (TViewState.Active | TViewState.Focused | TViewState.Disabled | TViewState.Exposed)) != 0)
        {
            ForEach(v => v.SetState(state, enable));
        }
    }

    /// <summary>
    /// Verarbeitet ein Ereignis in drei Phasen: PreProcess → Focused → PostProcess.
    ///
    /// <list type="number">
    /// <item><description>PreProcess: Kind-Views mit <see cref="TViewOptions.PreProcess"/>-Flag erhalten das Ereignis.</description></item>
    /// <item><description>Focused: Das Ereignis wird an <see cref="Current"/> (Tastatur) weitergeleitet.</description></item>
    /// <item><description>PostProcess: Kind-Views mit <see cref="TViewOptions.PostProcess"/>-Flag erhalten das Ereignis, wenn es noch nicht verbraucht wurde.</description></item>
    /// </list>
    ///
    /// Processes an event in three phases: PreProcess → Focused → PostProcess.
    ///
    /// <list type="number">
    /// <item><description>PreProcess: child views with <see cref="TViewOptions.PreProcess"/> flag receive the event.</description></item>
    /// <item><description>Focused: the event is forwarded to <see cref="Current"/> (keyboard).</description></item>
    /// <item><description>PostProcess: child views with <see cref="TViewOptions.PostProcess"/> flag receive the event if not yet consumed.</description></item>
    /// </list>
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        // Vor- und Nachverarbeitung rahmen den fokussierten Empfänger ein; ein geleertes Ereignis beendet die Kette.
        // Pre- and post-processing surround the focused receiver; a cleared event stops the chain.
        base.HandleEvent(@event);
        if (@event.What == TEventKind.Nothing)
        {
            return;
        }

        Phase = DrawPhase.PreProcess;
        ForEach(v =>
        {
            if (@event.What != TEventKind.Nothing
                && v.Options.HasFlag(TViewOptions.PreProcess)
                && (@event.What & v.EventMask) != 0)
            {
                v.HandleEvent(@event);
            }
        });

        if (@event.What != TEventKind.Nothing)
        {
            Phase = DrawPhase.Focused;

            if ((@event.What & (TEventKind.KeyDown | TEventKind.Command)) != 0)
            {
                Current?.HandleEvent(@event);
            }
            else
            {
                ForEach(v =>
                {
                    if (@event.What != TEventKind.Nothing)
                    {
                        v.HandleEvent(@event);
                    }
                });
            }
        }

        if (@event.What != TEventKind.Nothing)
        {
            Phase = DrawPhase.PostProcess;
            ForEach(v =>
            {
                if (@event.What != TEventKind.Nothing
                    && v.Options.HasFlag(TViewOptions.PostProcess)
                    && (@event.What & v.EventMask) != 0)
                {
                    v.HandleEvent(@event);
                }
            });
        }
    }

    // -------------------------------------------------------------------------
    // Geschützte Methoden / Protected methods
    // -------------------------------------------------------------------------

    /// <summary>
    /// Wird nach einer Größen- oder Positionsänderung aufgerufen.
    /// Alloziert den Puffer neu, wenn er bereits existiert, und aktualisiert das Clip-Rechteck.
    ///
    /// Called after a size or position change.
    /// Reallocates the buffer if it already exists, and updates the clip rectangle.
    /// </summary>
    protected override void OnBoundsChanged()
    {
        if (_buffer != null)
        {
            _buffer = new TConsoleBuffer(Size.X, Size.Y);
        }

        _clip = GetExtent();
    }

    /// <summary>
    /// Liefert das erste Kind der zirkulären Kindliste oder <c>null</c>.
    /// Abgeleitete Klassen können damit kontrolliert über die Kinder iterieren.
    ///
    /// Returns the first child in the circular child list or <c>null</c>.
    /// Derived classes can use this to iterate over children in a controlled way.
    /// </summary>
    /// <returns>Das erste Kind oder <c>null</c>. / The first child or <c>null</c>.</returns>
    protected internal TView? GetFirstChild() => First();

    // -------------------------------------------------------------------------
    // Private Hilfsmethoden / Private helper methods
    // -------------------------------------------------------------------------

    /// <summary>
    /// Gibt das erste (älteste) Element der zirkulären Liste zurück,
    /// oder <c>null</c>, wenn die Liste leer ist.
    /// Gemäß der Invariante gilt: First() = _last?.Next.
    ///
    /// Returns the first (oldest) element of the circular list,
    /// or <c>null</c> if the list is empty.
    /// By invariant: First() = _last?.Next.
    /// </summary>
    /// <returns>
    /// Das erste Kind-Element oder <c>null</c>.
    /// The first child element, or <c>null</c>.
    /// </returns>
    private TView? First() => _last?.Next;

    /// <summary>
    /// Iteriert über alle Kind-Views und ruft <paramref name="action"/> auf.
    /// <para>
    /// <b>Sichere Mutation</b>: Der Nachfolger (<c>Next</c>) wird vor dem Aufruf von
    /// <paramref name="action"/> gecacht, damit Entfernen einer View während der Iteration
    /// die Traversierung nicht beschädigt.
    /// Zur Erklärung für Lernende: Bei einer <c>List&lt;TView&gt;</c> würde das Entfernen
    /// eines Elements während einer foreach-Schleife eine Ausnahme werfen — die zirkuläre
    /// Liste mit vorab-gecachtem <c>Next</c>-Zeiger vermeidet dieses Problem.
    /// </para>
    ///
    /// Iterates over all child views and calls <paramref name="action"/> on each.
    /// <para>
    /// <b>Safe mutation</b>: The successor (<c>Next</c>) is cached before calling
    /// <paramref name="action"/>, so removing a view during iteration does not corrupt traversal.
    /// For learners: with a <c>List&lt;TView&gt;</c>, removing an element inside a foreach loop
    /// would throw an exception — the circular list with a pre-cached <c>Next</c> pointer avoids this.
    /// </para>
    /// </summary>
    /// <param name="action">Die auf jede Kind-View anzuwendende Aktion. / The action to apply to each child view.</param>
    private void ForEach(Action<TView> action)
    {
        TView? current = First();
        if (current == null)
        {
            return;
        }

        TView? stop = current;
        do
        {
            // Next vorab cachen für sichere Mutation / Cache Next before action for safe mutation
            TView? next = current.Next;
            action(current);
            current = next;
        }
        while (current != null && current != stop);
    }

    /// <summary>
    /// Sucht den Vorgänger von <paramref name="target"/> in der zirkulären Liste (O(n)).
    /// Vorbedingung: <paramref name="target"/> ist Mitglied dieser Gruppe.
    ///
    /// Finds the predecessor of <paramref name="target"/> in the circular list (O(n)).
    /// Precondition: <paramref name="target"/> is a member of this group.
    /// </summary>
    /// <param name="target">Die View, deren Vorgänger gesucht wird. / The view whose predecessor is sought.</param>
    /// <returns>
    /// Der Vorgänger von <paramref name="target"/> in der zirkulären Liste.
    /// The predecessor of <paramref name="target"/> in the circular list.
    /// </returns>
    private TView FindPrev(TView target)
    {
        TView current = target;
        while (current.Next != target)
        {
            current = current.Next!;
        }

        return current;
    }

    /// <summary>
    /// Setzt <see cref="Current"/> auf <c>null</c>.
    /// Wird aufgerufen, wenn die aktuell fokussierte View entfernt wird.
    ///
    /// Resets <see cref="Current"/> to <c>null</c>.
    /// Called when the currently focused view is removed.
    /// </summary>
    private void ResetCurrent()
    {
        Current = null;
        CurrentChanged();
    }

    /// <summary>
    /// Zählt die Anzahl der Kind-Views in der zirkulären Liste.
    ///
    /// Counts the number of child views in the circular list.
    /// </summary>
    /// <returns>Anzahl der Kind-Views. / Number of child views.</returns>
    private int CountChildren()
    {
        if (_last == null)
        {
            return 0;
        }

        int count = 0;
        TView current = First()!;
        do
        {
            count++;
            current = current.Next!;
        }
        while (current != First());

        return count;
    }
}
