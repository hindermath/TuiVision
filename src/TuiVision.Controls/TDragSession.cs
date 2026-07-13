// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Koordiniert eine begrenzte Drag-Session mit Ein-Zell-Schwelle, genau einem
/// Capture sowie gemeinsamen Pointer- und Tastaturübergängen.
///
/// Coordinates a bounded drag session with a one-cell threshold, exactly one
/// capture, and shared pointer and keyboard transitions.
/// </summary>
public sealed class TDragSession
{
    private readonly TPoint _pointerAnchor;
    private readonly bool _requireTarget;

    /// <summary>
    /// Initialisiert eine Drag-Session. <paramref name="bounds"/> beschreibt die
    /// zulässigen Quellpositionen mit exklusiver unterer rechter Ecke.
    ///
    /// Initializes a drag session. <paramref name="bounds"/> describes allowed
    /// source positions with an exclusive bottom-right corner.
    /// </summary>
    /// <param name="source">Die Quell-View. / The source view.</param>
    /// <param name="payload">Die optionale laufzeitlokale Nutzlast. / The optional process-local payload.</param>
    /// <param name="start">Die ursprüngliche Quellposition. / The original source position.</param>
    /// <param name="bounds">Die zulässigen Positionsgrenzen. / The allowed position bounds.</param>
    /// <param name="mode">Pointer oder Tastatur. / Pointer or keyboard mode.</param>
    /// <param name="pointerAnchor">Der Pointer-Startpunkt; standardmäßig <paramref name="start"/>. / The pointer start point; defaults to <paramref name="start"/>.</param>
    /// <param name="requireTarget">Ob ein opt-in Ziel erforderlich ist. / Whether an opt-in target is required.</param>
    /// <exception cref="ArgumentNullException">Wird bei fehlender Quelle ausgelöst. / Thrown when the source is missing.</exception>
    /// <exception cref="ArgumentException">Wird bei leeren Bounds ausgelöst. / Thrown when bounds are empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Wird ausgelöst, wenn der Start außerhalb liegt. / Thrown when start lies outside the bounds.</exception>
    public TDragSession(
        TView source,
        object? payload,
        TPoint start,
        TRect bounds,
        TDragInputMode mode,
        TPoint? pointerAnchor = null,
        bool requireTarget = false)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (bounds.IsEmpty())
        {
            throw new ArgumentException("Drag bounds must contain at least one cell.", nameof(bounds));
        }

        if (!bounds.Contains(start))
        {
            throw new ArgumentOutOfRangeException(nameof(start), "Drag start must lie inside the allowed bounds.");
        }

        Source = source;
        Payload = payload;
        Start = start;
        Current = start;
        Bounds = bounds;
        Mode = mode;
        _pointerAnchor = pointerAnchor ?? start;
        _requireTarget = requireTarget;
    }

    /// <summary>Die Quell-View. / The source view.</summary>
    public TView Source { get; }
    /// <summary>Die optionale Nutzlast. / The optional payload.</summary>
    public object? Payload { get; }
    /// <summary>Die ursprüngliche Position. / The original position.</summary>
    public TPoint Start { get; }
    /// <summary>Die aktuelle begrenzte Position. / The current bounded position.</summary>
    public TPoint Current { get; private set; }
    /// <summary>Die zulässigen Positionsgrenzen. / The allowed position bounds.</summary>
    public TRect Bounds { get; }
    /// <summary>Der Eingabemodus. / The input mode.</summary>
    public TDragInputMode Mode { get; }
    /// <summary>Der aktuelle Zustand. / The current state.</summary>
    public TDragSessionState State { get; private set; } = TDragSessionState.Pending;
    /// <summary>Das terminale Ergebnis oder <c>null</c>. / The terminal result or <c>null</c>.</summary>
    public TDragResult? Result { get; private set; }
    /// <summary>Ob die Session noch Eingaben annimmt. / Whether the session still accepts input.</summary>
    public bool IsActive => State is TDragSessionState.Pending or TDragSessionState.Captured;
    /// <summary>Ob die Ein-Zell-Schwelle überschritten und Capture aktiv ist. / Whether the one-cell threshold was crossed and capture is active.</summary>
    public bool HasCapture => State == TDragSessionState.Captured;

    /// <summary>
    /// Aktualisiert eine Pointer-Session relativ zum ursprünglichen Press-Punkt.
    ///
    /// Updates a pointer session relative to the original press point.
    /// </summary>
    /// <param name="where">Die aktuelle globale Pointer-Position. / The current global pointer position.</param>
    /// <exception cref="InvalidOperationException">Wird bei abgeschlossener oder nicht-Pointer-basierter Session ausgelöst. / Thrown for a completed or non-pointer session.</exception>
    public void UpdatePointer(TPoint where)
    {
        EnsureActiveMode(TDragInputMode.Pointer);
        TPoint delta = where - _pointerAnchor;
        UpdatePosition(new TPoint(Start.X + delta.X, Start.Y + delta.Y), delta);
    }

    /// <summary>
    /// Bewegt eine Tastatur-Session um einen Zeichenraster-Versatz.
    ///
    /// Moves a keyboard session by a character-cell delta.
    /// </summary>
    /// <param name="delta">Der Raster-Versatz. / The cell delta.</param>
    /// <exception cref="InvalidOperationException">Wird bei abgeschlossener oder nicht-tastaturbasierter Session ausgelöst. / Thrown for a completed or non-keyboard session.</exception>
    public void MoveBy(TPoint delta)
    {
        EnsureActiveMode(TDragInputMode.Keyboard);
        UpdatePosition(Current + delta, delta);
    }

    /// <summary>
    /// Schließt die Session mit optionaler opt-in Zielprüfung ab.
    ///
    /// Completes the session with optional opt-in target validation.
    /// </summary>
    /// <param name="target">Die optionale Ziel-View. / The optional target view.</param>
    /// <returns>Das terminale Ergebnis. / The terminal result.</returns>
    public TDragResult Drop(TView? target = null)
    {
        if (!IsActive)
        {
            return Result!;
        }

        bool accepted = !_requireTarget && target is null;
        if (target is IDragTarget dragTarget)
        {
            try
            {
                accepted = dragTarget.CanAcceptDrop(Source, Payload, Current);
            }
            catch
            {
                // Zielcode darf die Session-Grenze nicht durchbrechen; ein Fehler wird sicher als Ablehnung sichtbar.
                // Target code cannot escape the session boundary; failure is exposed safely as rejection.
                accepted = false;
            }
        }

        State = accepted ? TDragSessionState.Dropped : TDragSessionState.Rejected;
        Result = new TDragResult(
            Source,
            Payload,
            target,
            Current,
            State,
            accepted ? TDragCompletionReason.Dropped : TDragCompletionReason.TargetRejected);
        return Result;
    }

    /// <summary>
    /// Bricht eine aktive Session ab und gibt ein terminales Ergebnis zurück.
    ///
    /// Cancels an active session and returns a terminal result.
    /// </summary>
    /// <param name="reason">Der konkrete Abbruchgrund. / The concrete cancellation reason.</param>
    /// <returns>Das terminale Ergebnis. / The terminal result.</returns>
    public TDragResult Cancel(TDragCompletionReason reason = TDragCompletionReason.Cancelled)
    {
        if (!IsActive)
        {
            return Result!;
        }

        State = TDragSessionState.Cancelled;
        Result = new TDragResult(Source, Payload, null, Start, State, reason);
        return Result;
    }

    private void UpdatePosition(TPoint proposed, TPoint thresholdDelta)
    {
        if (State == TDragSessionState.Pending
            && (Math.Abs(thresholdDelta.X) >= 1 || Math.Abs(thresholdDelta.Y) >= 1))
        {
            State = TDragSessionState.Captured;
        }

        if (State == TDragSessionState.Captured)
        {
            Current = new TPoint(
                Math.Clamp(proposed.X, Bounds.A.X, Bounds.B.X - 1),
                Math.Clamp(proposed.Y, Bounds.A.Y, Bounds.B.Y - 1));
        }
    }

    private void EnsureActiveMode(TDragInputMode expected)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("The drag session is already complete.");
        }

        if (Mode != expected)
        {
            throw new InvalidOperationException($"The drag session uses {Mode} input.");
        }
    }
}
