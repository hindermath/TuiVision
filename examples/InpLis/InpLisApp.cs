// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.InpLis;

/// <summary>
/// Eingabe-, Listen- und History-Beispiel mit Headless-Seam.
///
/// Input, list, and history example with a headless seam.
/// </summary>
public sealed class InpLisApp : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;
    private readonly List<string> _items = [];
    private int _selectedIndex;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public InpLisApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Aktueller Eingabetext.
    ///
    /// Current input text.
    /// </summary>
    public string InputText { get; private set; } = string.Empty;

    /// <summary>
    /// Textorientierter History-Zustand.
    ///
    /// Text-first history state.
    /// </summary>
    public string HistoryText { get; private set; } = string.Empty;

    /// <summary>
    /// Laedt die Listeninhalte.
    ///
    /// Loads list contents.
    /// </summary>
    /// <param name="items">Die Eintraege. / The items.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string LoadItems(IEnumerable<string> items)
    {
        _items.Clear();
        _items.AddRange(items ?? []);
        _selectedIndex = 0;
        InputText = _items.Count == 0 ? string.Empty : _items[0];
        return _items.Count == 0 ? "inplis: empty list" : $"inplis: selected {InputText}";
    }

    /// <summary>
    /// Waehlt den naechsten Listeneintrag.
    ///
    /// Selects the next list item.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectNext()
    {
        if (_items.Count == 0)
        {
            return "inplis: empty list";
        }

        _selectedIndex = Math.Min(_items.Count - 1, _selectedIndex + 1);
        InputText = _items[_selectedIndex];
        return $"inplis: selected {InputText}";
    }

    /// <summary>
    /// Uebernimmt Eingabetext in den History-Zustand.
    ///
    /// Commits input text to history state.
    /// </summary>
    /// <param name="text">Der Text. / The text.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CommitInput(string text)
    {
        InputText = text ?? string.Empty;
        HistoryText = string.IsNullOrEmpty(HistoryText) ? InputText : $"{HistoryText};{InputText}";
        return $"inplis: committed {InputText}";
    }

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }
}
