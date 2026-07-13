// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Benennt den Ursprung einer Close-Anfrage.
///
/// Names the origin of a close request.
/// </summary>
public enum TCloseTrigger
{
    /// <summary>Direkter Command-Dispatch. / Direct command dispatch.</summary>
    Command,
    /// <summary>Die Tastenkombination Ctrl+W. / The Ctrl+W shortcut.</summary>
    CtrlW,
    /// <summary>Der bewachte Escape-Pfad. / The guarded Escape path.</summary>
    Escape,
    /// <summary>Eine Desktop-Close-All-Operation. / A desktop Close-All operation.</summary>
    CloseAll
}

/// <summary>
/// Beschreibt genau eine Entscheidung einer Close-Anfrage.
///
/// Describes exactly one close-request decision.
/// </summary>
public enum TCloseDecision
{
    /// <summary>Die View wurde sichtbar aus ihrem Owner entfernt. / The view was visibly removed from its owner.</summary>
    Closed,
    /// <summary>Die View hat den Abschluss abgelehnt. / The view vetoed completion.</summary>
    Vetoed,
    /// <summary>Die View unterstützt den angeforderten Close-Pfad nicht. / The view does not support the requested close path.</summary>
    NotCloseable,
    /// <summary>Die View war bereits von einem Owner getrennt. / The view was already detached from an owner.</summary>
    AlreadyDetached
}

/// <summary>
/// Hält das unveränderliche Ergebnis einer Close-Anfrage fest.
///
/// Captures the immutable result of a close request.
/// </summary>
public sealed class TCloseResult
{
    internal TCloseResult(TView target, TCloseTrigger trigger, TCloseDecision decision, TGroup? ownerAfter)
    {
        Target = target;
        Trigger = trigger;
        Decision = decision;
        OwnerAfter = ownerAfter;
    }

    /// <summary>Die angefragte View. / The requested view.</summary>
    public TView Target { get; }

    /// <summary>Der auslösende Pfad. / The triggering path.</summary>
    public TCloseTrigger Trigger { get; }

    /// <summary>Die eindeutige Entscheidung. / The unambiguous decision.</summary>
    public TCloseDecision Decision { get; }

    /// <summary>Der Owner nach der Entscheidung oder <c>null</c>. / The owner after the decision or <c>null</c>.</summary>
    public TGroup? OwnerAfter { get; }
}
