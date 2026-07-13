// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>Benannt Pointer- oder Tastatureingabe. / Names pointer or keyboard input.</summary>
public enum TDragInputMode
{
    /// <summary>Pointer-Eingabe. / Pointer input.</summary>
    Pointer,
    /// <summary>Tastatureingabe. / Keyboard input.</summary>
    Keyboard
}

/// <summary>Beschreibt den aktuellen Session-Zustand. / Describes the current session state.</summary>
public enum TDragSessionState
{
    /// <summary>Die Ein-Zell-Schwelle wurde noch nicht überschritten. / The one-cell threshold has not been crossed.</summary>
    Pending,
    /// <summary>Die Session besitzt das eine aktive Capture. / The session owns the one active capture.</summary>
    Captured,
    /// <summary>Der Drop wurde abgeschlossen. / The drop completed.</summary>
    Dropped,
    /// <summary>Das Ziel hat den Drop abgelehnt. / The target rejected the drop.</summary>
    Rejected,
    /// <summary>Die Session wurde abgebrochen. / The session was cancelled.</summary>
    Cancelled
}

/// <summary>Erklärt den terminalen Abschlussgrund. / Explains the terminal completion reason.</summary>
public enum TDragCompletionReason
{
    /// <summary>Noch kein terminales Ergebnis. / No terminal result yet.</summary>
    None,
    /// <summary>Der Drop wurde angenommen. / The drop was accepted.</summary>
    Dropped,
    /// <summary>Das opt-in Ziel hat abgelehnt oder fehlte. / The opt-in target rejected or was missing.</summary>
    TargetRejected,
    /// <summary>Expliziter Nutzerabbruch. / Explicit user cancellation.</summary>
    Cancelled,
    /// <summary>Der Owner ging während der Session verloren. / The owner was lost during the session.</summary>
    OwnerLost,
    /// <summary>Die Eingabe-Capability ging verloren. / The input capability was lost.</summary>
    CapabilityLost,
    /// <summary>Die Quelle wurde deaktiviert. / The source was disabled.</summary>
    Disabled,
    /// <summary>Die Quelle wurde entfernt. / The source was removed.</summary>
    Removed,
    /// <summary>Die Quelle wurde heruntergefahren. / The source was shut down.</summary>
    Shutdown
}

/// <summary>
/// Hält das unveränderliche terminale Ergebnis einer Drag-Session fest.
///
/// Captures the immutable terminal result of a drag session.
/// </summary>
public sealed class TDragResult
{
    internal TDragResult(
        TView source,
        object? payload,
        TView? target,
        TPoint position,
        TDragSessionState state,
        TDragCompletionReason reason)
    {
        Source = source;
        Payload = payload;
        Target = target;
        Position = position;
        State = state;
        Reason = reason;
    }

    /// <summary>Die Quell-View. / The source view.</summary>
    public TView Source { get; }
    /// <summary>Die optionale laufzeitlokale Nutzlast. / The optional process-local payload.</summary>
    public object? Payload { get; }
    /// <summary>Die akzeptierte oder abgelehnte Ziel-View. / The accepted or rejected target view.</summary>
    public TView? Target { get; }
    /// <summary>Die letzte begrenzte Position. / The final bounded position.</summary>
    public TPoint Position { get; }
    /// <summary>Der terminale Session-Zustand. / The terminal session state.</summary>
    public TDragSessionState State { get; }
    /// <summary>Der Abschlussgrund. / The completion reason.</summary>
    public TDragCompletionReason Reason { get; }
}
