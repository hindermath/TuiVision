// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Editierbares Kombinationsfeld auf Basis von <see cref="TInputLine"/> mit optionaler
/// Dropdown-Auswahlliste und History-Anbindung.
///
/// Editable combo box based on <see cref="TInputLine"/> with an optional dropdown
/// choice list and history integration.
/// </summary>
public class TComboBox : TInputLine
{
    /// <summary>
    /// Interne History-Instanz, wenn eine History-ID übergeben wurde.
    ///
    /// Internal history instance when a history ID was provided.
    /// </summary>
    private readonly THistory? _history;

    /// <summary>
    /// Erstellt eine neue Kombinationsfeld-Instanz.
    ///
    /// Creates a new combo box instance.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
    /// <param name="maxLen">Die maximale Zeichenanzahl. / The maximum number of characters.</param>
    /// <param name="choices">Die optionale Auswahlliste. / The optional choice list.</param>
    /// <param name="historyId">Die optionale History-ID. / The optional history identifier.</param>
    public TComboBox(TRect bounds, int maxLen, TStringList? choices, string? historyId = null)
        : base(bounds, maxLen)
    {
        Choices = choices;
        SelectedIndex = -1;
        DropDownOpen = false;
        if (historyId is not null)
        {
            _history = new THistory(historyId);
        }
    }

    /// <summary>
    /// Die optionale Auswahlliste.
    ///
    /// The optional choice list.
    /// </summary>
    public TStringList? Choices { get; }

    /// <summary>
    /// Gibt an, ob das Dropdown gerade geöffnet ist.
    ///
    /// Indicates whether the dropdown is currently open.
    /// </summary>
    public bool DropDownOpen { get; private set; }

    /// <summary>
    /// Der Index der aktuell gewählten Option, oder -1 wenn keine Option gewählt ist.
    ///
    /// The index of the currently selected choice, or -1 if no choice is selected.
    /// </summary>
    public int SelectedIndex { get; private set; }

    /// <summary>
    /// Die History-ID, falls eine übergeben wurde.
    ///
    /// The history identifier, if one was provided.
    /// </summary>
    public string? HistoryId => _history?.HistoryId;

    /// <summary>
    /// Öffnet das Dropdown, wenn mindestens eine Option vorhanden ist.
    ///
    /// Opens the dropdown if at least one choice is available.
    /// </summary>
    public void OpenDropDown()
    {
        if (Choices is not null && Choices.Count > 0)
        {
            DropDownOpen = true;
        }
    }

    /// <summary>
    /// Schließt das Dropdown.
    ///
    /// Closes the dropdown.
    /// </summary>
    public void CloseDropDown()
    {
        DropDownOpen = false;
    }

    /// <summary>
    /// Wählt die Option am angegebenen Index aus, übernimmt deren Text und schließt das Dropdown.
    /// Ungültige Indizes werden ignoriert.
    ///
    /// Selects the choice at the given index, copies its text, and closes the dropdown.
    /// Invalid indices are silently ignored.
    /// </summary>
    /// <param name="index">Der Auswahlindex. / The selection index.</param>
    public void SelectIndex(int index)
    {
        if (Choices is null || index < 0 || index >= Choices.Count)
        {
            return;
        }

        SelectedIndex = index;
        Data = Choices[index];
        CloseDropDown();
    }

    /// <summary>
    /// Übernimmt den aktuellen Eingabetext in die History, wenn eine History-ID konfiguriert ist.
    ///
    /// Commits the current input text to history if a history ID is configured.
    /// </summary>
    public void CommitToHistory()
    {
        _history?.Add(Data);
    }
}
