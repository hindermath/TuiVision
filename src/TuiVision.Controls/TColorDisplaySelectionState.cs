// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Art einer Farb-, Anzeige- oder Zeichensatzwahl.
///
/// Kind of colour, display, or charset choice.
/// </summary>
public enum ColorDisplaySelectionKind
{
    /// <summary>Farbwahl. / Colour choice.</summary>
    Color,

    /// <summary>Monochrome Anzeige. / Monochrome display.</summary>
    Mono,

    /// <summary>Symbolischer Zeichensatz. / Symbolic charset.</summary>
    SymbolicCharset,

    /// <summary>Anzeigeprofil. / Display profile.</summary>
    Display
}

/// <summary>
/// Synchroner Auswahlzustand fuer Farbe, Anzeige und symbolischen Zeichensatz.
///
/// Synchronous selection state for colour, display, and symbolic charset.
/// </summary>
/// <param name="SelectionKind">Die Auswahlart. / The selection kind.</param>
/// <param name="Group">Die Auswahlgruppe. / The selection group.</param>
/// <param name="SelectedValue">Der aktuell gewaählte Wert. / The currently selected value.</param>
/// <param name="PreviewValue">Der Vorschauwert. / The preview value.</param>
/// <param name="CommittedValue">Der bestaetigte Ausgangswert. / The committed starting value.</param>
/// <param name="SupportedOptions">Die unterstuetzten Werte. / The supported values.</param>
/// <param name="FallbackReason">Der optionale Fallback-Grund. / The optional fallback reason.</param>
public readonly record struct ColorDisplaySelectionState(
    ColorDisplaySelectionKind SelectionKind,
    string Group,
    string? SelectedValue,
    string? PreviewValue,
    string? CommittedValue,
    IReadOnlyList<string> SupportedOptions,
    string? FallbackReason)
{
    /// <summary>
    /// Gibt an, ob ein Fallback aktiv ist.
    ///
    /// Indicates whether fallback is active.
    /// </summary>
    public bool HasFallback => !string.IsNullOrWhiteSpace(FallbackReason);

    /// <summary>
    /// Erstellt einen validierten Auswahlzustand.
    ///
    /// Creates a validated selection state.
    /// </summary>
    /// <param name="kind">Die Auswahlart. / The selection kind.</param>
    /// <param name="group">Die Gruppe. / The group.</param>
    /// <param name="selectedValue">Der Auswahlwert. / The selected value.</param>
    /// <param name="committedValue">Der bestaetigte Wert. / The committed value.</param>
    /// <param name="supportedOptions">Die unterstuetzten Werte. / The supported values.</param>
    /// <returns>Der Auswahlzustand. / The selection state.</returns>
    public static ColorDisplaySelectionState Create(
        ColorDisplaySelectionKind kind,
        string group,
        string selectedValue,
        string? committedValue,
        IEnumerable<string> supportedOptions)
    {
        string[] options = supportedOptions.Where(option => !string.IsNullOrWhiteSpace(option)).Distinct(StringComparer.Ordinal).ToArray();
        if (options.Length == 0)
        {
            return CreateFallback(kind, group, committedValue, "No supported option is available.");
        }

        if (!options.Contains(selectedValue, StringComparer.Ordinal))
        {
            return CreateFallback(kind, group, committedValue, $"Unsupported value '{selectedValue}'.");
        }

        return new(kind, group, selectedValue, selectedValue, committedValue, options, null);
    }

    /// <summary>
    /// Erstellt einen begrenzten Fallback-Zustand.
    ///
    /// Creates a bounded fallback state.
    /// </summary>
    /// <param name="kind">Die Auswahlart. / The selection kind.</param>
    /// <param name="group">Die Gruppe. / The group.</param>
    /// <param name="committedValue">Der zu erhaltende Wert. / The value to preserve.</param>
    /// <param name="reason">Der Fallback-Grund. / The fallback reason.</param>
    /// <returns>Der Fallback-Zustand. / The fallback state.</returns>
    public static ColorDisplaySelectionState CreateFallback(
        ColorDisplaySelectionKind kind,
        string group,
        string? committedValue,
        string reason) =>
        new(kind, group, committedValue, committedValue, committedValue, [], reason);

    /// <summary>
    /// Stellt den bestaetigten Wert nach einem Abbruch wieder her.
    ///
    /// Restores the committed value after cancellation.
    /// </summary>
    /// <returns>Der wiederhergestellte Zustand. / The restored state.</returns>
    public ColorDisplaySelectionState Cancel() => this with
    {
        SelectedValue = CommittedValue,
        PreviewValue = CommittedValue
    };
}
