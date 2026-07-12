using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Beschreibt das Ziel und den textbasierten A11Y-Zustand eines Fokuswechsels.
///
/// Describes the target and text-based accessibility state of a focus transition.
/// </summary>
public sealed record TFocusAnnouncement
{
    private TFocusAnnouncement(
        TView target,
        string? accessibleLabel,
        string? accessibleDescription,
        bool canReceiveFocus)
    {
        Target = target;
        AccessibleLabel = accessibleLabel;
        AccessibleDescription = accessibleDescription;
        CanReceiveFocus = canReceiveFocus;
    }

    /// <summary>Die fokussierte View. / The focused view.</summary>
    public TView Target { get; }

    /// <summary>Die optionale stabile Bezeichnung. / The optional stable label.</summary>
    public string? AccessibleLabel { get; }

    /// <summary>Die optionale Beschreibung. / The optional description.</summary>
    public string? AccessibleDescription { get; }

    /// <summary>Die aktuelle Fokusfähigkeit. / The current focus capability.</summary>
    public bool CanReceiveFocus { get; }

    /// <summary>
    /// Erstellt eine unveränderliche Momentaufnahme aus einer View. Leere opt-in
    /// Texte werden unterdrückt, damit der Broadcast keine falsche Semantik behauptet.
    ///
    /// Creates an immutable snapshot from a view. Blank opt-in text is suppressed
    /// so the broadcast does not claim false semantics.
    /// </summary>
    /// <param name="target">Die fokussierte View. / The focused view.</param>
    /// <returns>Die Fokusankündigung. / The focus announcement.</returns>
    /// <exception cref="ArgumentNullException">
    /// Wird bei einem leeren Ziel ausgelöst. / Thrown for a null target.
    /// </exception>
    public static TFocusAnnouncement Create(TView target)
    {
        ArgumentNullException.ThrowIfNull(target);

        IAccessibleWidget? widget = target as IAccessibleWidget;
        string? label = string.IsNullOrWhiteSpace(widget?.AccessibleLabel)
            ? null
            : widget.AccessibleLabel.Trim();
        string? description = string.IsNullOrWhiteSpace(widget?.AccessibleDescription)
            ? null
            : widget.AccessibleDescription.Trim();
        bool viewCanFocus = target.Options.HasFlag(TViewOptions.Selectable)
            && target.GetState(TViewState.Visible)
            && !target.GetState(TViewState.Disabled);

        return new TFocusAnnouncement(
            target,
            label,
            description,
            viewCanFocus && (widget?.CanReceiveFocus ?? true));
    }
}
