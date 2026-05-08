// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.ListVi;

/// <summary>
/// Listenansicht-Beispiel mit Headless-Seam.
///
/// List viewer example with a headless seam.
/// </summary>
public sealed class ListViApp : TApplication
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
    public ListViApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Sichtbarer Auswahlzustand.
    ///
    /// Visible selection state.
    /// </summary>
    public string VisibleState { get; private set; } = "listvi: empty";

    /// <summary>
    /// Laedt die Listeneintraege.
    ///
    /// Loads list entries.
    /// </summary>
    /// <param name="items">Die Eintraege. / The entries.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string LoadItems(IEnumerable<string> items)
    {
        _items.Clear();
        _items.AddRange(items ?? []);
        _selectedIndex = 0;
        VisibleState = _items.Count == 0 ? "listvi: empty list" : $"listvi: selected {_items[0]} viewport={Math.Min(3, _items.Count)}";
        return VisibleState;
    }

    /// <summary>
    /// Bewegt die Auswahl um die angegebene Distanz.
    ///
    /// Moves selection by the specified distance.
    /// </summary>
    /// <param name="delta">Die Distanz. / The distance.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string MoveSelection(int delta)
    {
        if (_items.Count == 0)
        {
            VisibleState = "listvi: empty list";
            return VisibleState;
        }

        _selectedIndex = Math.Clamp(_selectedIndex + delta, 0, _items.Count - 1);
        VisibleState = $"listvi: selected {_items[_selectedIndex]} index={_selectedIndex}";
        return VisibleState;
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
