// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Beschreibt den Zeitpunkt, zu dem eine View Eingabedaten validiert.
///
/// Describes the point at which a view validates input data.
/// </summary>
public enum TValidationPhase
{
    /// <summary>
    /// Prüft einen vorgeschlagenen Edit vor der Zustandsänderung.
    ///
    /// Checks a proposed edit before state is changed.
    /// </summary>
    Edit,

    /// <summary>
    /// Prüft, ob eine View den Fokus freigeben darf.
    ///
    /// Checks whether a view may release focus.
    /// </summary>
    FocusLoss,

    /// <summary>
    /// Prüft den endgültigen Zustand vor einem bestätigenden Dialogabschluss.
    ///
    /// Checks final state before affirmative dialog completion.
    /// </summary>
    Acceptance
}
