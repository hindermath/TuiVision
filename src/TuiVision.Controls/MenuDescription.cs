// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>Unveränderliche Beschreibung eines Menüeintrags. / Immutable description of a menu item.</summary>
/// <param name="Id">Die stabile ID. / The stable identifier.</param>
/// <param name="ParentId">Die optionale Eltern-ID. / The optional parent identifier.</param>
/// <param name="Order">Die Geschwisterreihenfolge. / The sibling order.</param>
/// <param name="Label">Die Beschriftung. / The label.</param>
/// <param name="CommandId">Die Command-ID oder 0. / The command ID or zero.</param>
/// <param name="HelpContext">Der Hilfekontext. / The help context.</param>
/// <param name="Disabled">Der statische Deaktivierungszustand. / The static disabled state.</param>
public sealed record MenuItemDescription(
    string Id,
    string? ParentId,
    int Order,
    string Label,
    ushort CommandId,
    int HelpContext,
    bool Disabled);

/// <summary>Versionierte unveränderliche Menübeschreibung. / Versioned immutable menu description.</summary>
public sealed record MenuDescription
{
    /// <summary>Erstellt eine Menübeschreibung. / Creates a menu description.</summary>
    /// <param name="version">Die Modellversion. / The model version.</param>
    /// <param name="items">Die Einträge. / The items.</param>
    public MenuDescription(int version, IEnumerable<MenuItemDescription> items)
    {
        Version = version;
        Items = items?.ToArray() ?? [];
    }

    /// <summary>Die Modellversion. / The model version.</summary>
    public int Version { get; }
    /// <summary>Die Einträge. / The items.</summary>
    public IReadOnlyList<MenuItemDescription> Items { get; }
}
