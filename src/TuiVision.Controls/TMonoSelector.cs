// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfache Einzelauswahl aus Textoptionen als managed Gegenstück zu <c>TMonoSelector</c>
/// aus Turbo Vision (<c>tmonosel.cc</c>).
///
/// Simple single-selection control over text options as a managed counterpart to <c>TMonoSelector</c>
/// from Turbo Vision (<c>tmonosel.cc</c>).
/// </summary>
public sealed class TMonoSelector
{
    private readonly IReadOnlyList<string> _items;

    /// <summary>
    /// Erstellt einen neuen Selektor mit den verfügbaren Optionen.
    ///
    /// Creates a new selector with the available options.
    /// </summary>
    /// <param name="items">Die auswählbaren Einträge. / The selectable items.</param>
    public TMonoSelector(IReadOnlyList<string> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        _items = items.Count == 0 ? [] : items.ToArray();
        SelectedIndex = _items.Count == 0 ? -1 : 0;
    }

    /// <summary>
    /// Der aktuell ausgewählte Index oder <c>-1</c>, wenn keine Auswahl existiert.
    ///
    /// The currently selected index or <c>-1</c> when no selection exists.
    /// </summary>
    public int SelectedIndex { get; private set; }

    /// <summary>
    /// Der aktuell ausgewählte Eintrag oder <c>null</c>, wenn keine Auswahl existiert.
    ///
    /// The currently selected item or <c>null</c> when no selection exists.
    /// </summary>
    public string? SelectedItem => SelectedIndex < 0 ? null : _items[SelectedIndex];

    /// <summary>
    /// Setzt die Auswahl auf den angegebenen Index.
    ///
    /// Sets the selection to the specified index.
    /// </summary>
    /// <param name="index">Der Zielindex. / The target index.</param>
    public void Select(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "The selected index is outside the available item range.");
        }

        SelectedIndex = index;
    }
}
