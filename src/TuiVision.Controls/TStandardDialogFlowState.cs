// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Art eines wiederverwendbaren Standarddialog-Flows.
///
/// Kind of reusable standard-dialog flow.
/// </summary>
public enum StandardDialogKind
{
    /// <summary>Dateiauswahl. / File selection.</summary>
    File,

    /// <summary>Verzeichnisauswahl. / Directory selection.</summary>
    Directory,

    /// <summary>Farbauswahl. / Colour selection.</summary>
    Color,

    /// <summary>Symbolische Zeichensatzauswahl. / Symbolic charset selection.</summary>
    SymbolicCharset,

    /// <summary>Anzeigeauswahl. / Display selection.</summary>
    Display,

    /// <summary>Dialog-Designer-Flow. / Dialog-designer flow.</summary>
    Designer
}

/// <summary>
/// Interaktionszustand eines Standarddialogs.
///
/// Interaction state of a standard dialog.
/// </summary>
public enum StandardDialogInteractionState
{
    /// <summary>Inaktiv. / Idle.</summary>
    Idle,

    /// <summary>Aktiv. / Active.</summary>
    Active,

    /// <summary>Validiert. / Validated.</summary>
    Validated,

    /// <summary>Bestaetigt. / Confirmed.</summary>
    Confirmed,

    /// <summary>Abgebrochen. / Canceled.</summary>
    Canceled,

    /// <summary>Abgelehnt. / Rejected.</summary>
    Rejected
}

/// <summary>
/// Gemeinsamer Zustandsnachweis fuer wiederverwendbare Standarddialoge.
///
/// Shared state proof for reusable standard dialogs.
/// </summary>
/// <param name="DialogKind">Die Dialogart. / The dialog kind.</param>
/// <param name="InteractionState">Der Interaktionszustand. / The interaction state.</param>
/// <param name="KeyboardReachable">Ob Pflichtaktionen per Tastatur erreichbar sind. / Whether required actions are keyboard reachable.</param>
/// <param name="ValidationMessages">Die textorientierten Validierungsmeldungen. / The text-oriented validation messages.</param>
/// <param name="Decision">Der optionale Entscheidungswert. / The optional decision value.</param>
public readonly record struct StandardDialogFlowState(
    StandardDialogKind DialogKind,
    StandardDialogInteractionState InteractionState,
    bool KeyboardReachable,
    IReadOnlyList<StandardDialogValidationMessage> ValidationMessages,
    object? Decision)
{
    /// <summary>
    /// Erstellt einen aktiven Flow ohne Validierungsfehler.
    ///
    /// Creates an active flow without validation errors.
    /// </summary>
    /// <param name="dialogKind">Die Dialogart. / The dialog kind.</param>
    /// <returns>Der Flow-Zustand. / The flow state.</returns>
    public static StandardDialogFlowState Active(StandardDialogKind dialogKind) =>
        new(dialogKind, StandardDialogInteractionState.Active, true, [], null);
}
