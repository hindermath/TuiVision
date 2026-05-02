// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfacher verwalteter Farbkonfigurationsdialog als Gegenstück zu <c>TColorDialog</c>
/// aus Turbo Vision (<c>tcolordi.cc</c>).
///
/// Simple managed colour configuration dialog as a counterpart to <c>TColorDialog</c>
/// from Turbo Vision (<c>tcolordi.cc</c>).
/// </summary>
public sealed class TColorDialog
{
    private static readonly string[] DisplayOptions = ["color", "mono"];
    private static readonly string[] CharsetOptions = ["ascii", "cp437", "unicode-symbolic"];
    private ColorDisplaySelectionState _committedState;

    /// <summary>
    /// Initialisiert einen Dialog für die angegebene Palette.
    ///
    /// Initializes a dialog for the specified palette.
    /// </summary>
    /// <param name="palette">Die zu bearbeitende Palette. / The palette to edit.</param>
    public TColorDialog(TPalette palette)
    {
        ArgumentNullException.ThrowIfNull(palette);
        Palette = palette;
        Selector = new TColorSelector();
        Preview = new TColorDisplay();
        _committedState = ColorDisplaySelectionState.Create(
            ColorDisplaySelectionKind.Color,
            "palette",
            Selector.SelectedColor.ToString(),
            Selector.SelectedColor.ToString(),
            TColorSelector.SupportedColorNames);
        SelectionState = _committedState;
    }

    /// <summary>
    /// Die zu bearbeitende Palette.
    ///
    /// The palette being edited.
    /// </summary>
    public TPalette Palette { get; }

    /// <summary>
    /// Das interne Farbauswahl-Steuerelement.
    ///
    /// The internal colour selection control.
    /// </summary>
    public TColorSelector Selector { get; }

    /// <summary>
    /// Die interne Farbvorschau.
    ///
    /// The internal colour preview.
    /// </summary>
    public TColorDisplay Preview { get; }

    /// <summary>
    /// Der synchronisierte Auswahlzustand.
    ///
    /// The synchronized selection state.
    /// </summary>
    public ColorDisplaySelectionState SelectionState { get; private set; }

    /// <summary>
    /// Der gemeinsame Standarddialog-Flow-Zustand.
    ///
    /// The shared standard-dialog flow state.
    /// </summary>
    public StandardDialogFlowState FlowState { get; private set; } =
        StandardDialogFlowState.Active(StandardDialogKind.Color);

    /// <summary>
    /// Wählt eine Farbe aus und aktualisiert die Vorschau.
    ///
    /// Selects a colour and updates the preview.
    /// </summary>
    /// <param name="color">Die gewünschte Farbe. / The desired colour.</param>
    public void SelectColor(ConsoleColor color)
    {
        Selector.SelectColor(color);
        Preview.SetColors(color, Preview.Background);
        SelectionState = ColorDisplaySelectionState.Create(
            ColorDisplaySelectionKind.Color,
            "palette",
            color.ToString(),
            _committedState.CommittedValue,
            TColorSelector.SupportedColorNames);
        SyncFlow(StandardDialogInteractionState.Validated);
    }

    /// <summary>
    /// Waehlt ein Anzeigeprofil aus.
    ///
    /// Selects a display profile.
    /// </summary>
    /// <param name="display">Das Anzeigeprofil. / The display profile.</param>
    public void SelectDisplay(string display)
    {
        SelectionState = ColorDisplaySelectionState.Create(
            ColorDisplaySelectionKind.Display,
            "display",
            display,
            _committedState.CommittedValue,
            DisplayOptions);
        SyncFlow(StandardDialogInteractionState.Validated);
    }

    /// <summary>
    /// Waehlt einen symbolischen Zeichensatzwert aus.
    ///
    /// Selects a symbolic charset value.
    /// </summary>
    /// <param name="charset">Der symbolische Zeichensatz. / The symbolic charset.</param>
    public void SelectSymbolicCharset(string charset)
    {
        SelectionState = ColorDisplaySelectionState.Create(
            ColorDisplaySelectionKind.SymbolicCharset,
            "charset",
            charset,
            _committedState.CommittedValue,
            CharsetOptions);
        SyncFlow(StandardDialogInteractionState.Validated);
    }

    /// <summary>
    /// Aktiviert einen begrenzten Fallback, wenn keine Option verfuegbar ist.
    ///
    /// Activates a bounded fallback when no option is available.
    /// </summary>
    /// <param name="kind">Die Auswahlart. / The selection kind.</param>
    /// <param name="reason">Der Fallback-Grund. / The fallback reason.</param>
    public void UseFallback(ColorDisplaySelectionKind kind, string reason)
    {
        SelectionState = ColorDisplaySelectionState.CreateFallback(kind, kind.ToString(), _committedState.CommittedValue, reason);
        SyncFlow(StandardDialogInteractionState.Rejected);
    }

    /// <summary>
    /// Bestaetigt die aktuelle Auswahl.
    ///
    /// Confirms the current selection.
    /// </summary>
    /// <returns>Der bestaetigte Zustand. / The confirmed state.</returns>
    public ColorDisplaySelectionState ConfirmSelection()
    {
        _committedState = SelectionState with { CommittedValue = SelectionState.SelectedValue };
        SelectionState = _committedState;
        SyncFlow(StandardDialogInteractionState.Confirmed);
        return SelectionState;
    }

    /// <summary>
    /// Bricht die Auswahl ab und stellt den bestaetigten Wert wieder her.
    ///
    /// Cancels the selection and restores the committed value.
    /// </summary>
    /// <returns>Der wiederhergestellte Zustand. / The restored state.</returns>
    public ColorDisplaySelectionState CancelSelection()
    {
        SelectionState = _committedState.Cancel();
        SyncFlow(StandardDialogInteractionState.Canceled);
        return SelectionState;
    }

    private void SyncFlow(StandardDialogInteractionState interactionState)
    {
        StandardDialogKind kind = SelectionState.SelectionKind switch
        {
            ColorDisplaySelectionKind.SymbolicCharset => StandardDialogKind.SymbolicCharset,
            ColorDisplaySelectionKind.Display or ColorDisplaySelectionKind.Mono => StandardDialogKind.Display,
            _ => StandardDialogKind.Color
        };
        IReadOnlyList<StandardDialogValidationMessage> messages = SelectionState.HasFallback
            ? [StandardDialogValidationMessage.Warning("selection-fallback", SelectionState.FallbackReason!)]
            : [];
        FlowState = new StandardDialogFlowState(kind, interactionState, true, messages, SelectionState);
    }
}
