// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Gruppiert mehrere Farbelemente als managed Gegenstück zu <c>TColorGroup</c>
/// aus Turbo Vision (<c>tcolorgr.cc</c>).
///
/// Groups multiple colour items as a managed counterpart to <c>TColorGroup</c>
/// from Turbo Vision (<c>tcolorgr.cc</c>).
/// </summary>
public sealed class TColorGroup
{
    private readonly List<TColorItem> _items = new();

    /// <summary>
    /// Initialisiert eine Farbgruppe.
    ///
    /// Initializes a colour group.
    /// </summary>
    /// <param name="name">Der Gruppenname. / The group name.</param>
    public TColorGroup(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
    }

    /// <summary>
    /// Der Gruppenname.
    ///
    /// The group name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Die Anzahl der enthaltenen Farbelemente.
    ///
    /// The number of contained colour items.
    /// </summary>
    public int ItemCount => _items.Count;

    /// <summary>
    /// Fügt ein Farbelement hinzu.
    ///
    /// Adds a colour item.
    /// </summary>
    /// <param name="item">Das Farbelement. / The colour item.</param>
    public void AddItem(TColorItem item) => _items.Add(item);
}
