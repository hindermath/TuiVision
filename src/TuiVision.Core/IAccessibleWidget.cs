namespace TuiVision.Core;

/// <summary>
/// Beschreibt die textbasierten A11Y-Eigenschaften eines Widgets. Der Vertrag ist
/// bewusst opt-in, damit nicht migrierte Widgets keine erfundenen Angaben liefern.
///
/// Describes the text-based accessibility properties of a widget. The contract is
/// deliberately opt-in so non-migrated widgets do not expose invented information.
/// </summary>
public interface IAccessibleWidget
{
    /// <summary>
    /// Die kurze, stabile Bezeichnung des Widgets.
    ///
    /// The short, stable label of the widget.
    /// </summary>
    string AccessibleLabel { get; }

    /// <summary>
    /// Eine optionale Erklärung von Zweck oder Zustand des Widgets.
    ///
    /// An optional explanation of the widget's purpose or state.
    /// </summary>
    string? AccessibleDescription { get; }

    /// <summary>
    /// Gibt an, ob das Widget im aktuellen Zustand Eingabefokus erhalten kann.
    ///
    /// Indicates whether the widget can receive input focus in its current state.
    /// </summary>
    bool CanReceiveFocus { get; }
}
