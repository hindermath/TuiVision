// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Beschreibt das Ergebnis eines angeforderten Fokuswechsels.
///
/// Describes the result of a requested focus transition.
/// </summary>
public enum TFocusTransitionResult
{
    /// <summary>Der Fokus wurde auf das Ziel übertragen. / Focus was transferred to the target.</summary>
    Accepted,

    /// <summary>Der Fokuswechsel wurde vor einer Zustandsänderung abgelehnt. / The transition was rejected before state changed.</summary>
    Rejected,

    /// <summary>Das Ziel war bereits fokussiert; es war keine Änderung nötig. / The target was already focused; no change was needed.</summary>
    NoOp
}
