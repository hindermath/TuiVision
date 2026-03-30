// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Sammlung von Farbgruppen als managed Gegenstück zu <c>TPalette</c>
/// aus Turbo Vision (<c>tpalette.cc</c>).
///
/// Collection of colour groups as a managed counterpart to <c>TPalette</c>
/// from Turbo Vision (<c>tpalette.cc</c>).
/// </summary>
public sealed class TPalette
{
    private readonly List<TColorGroup> _groups = new();

    /// <summary>
    /// Die Anzahl der Farbgruppen.
    ///
    /// The number of colour groups.
    /// </summary>
    public int GroupCount => _groups.Count;

    /// <summary>
    /// Fügt eine Farbgruppe hinzu.
    ///
    /// Adds a colour group.
    /// </summary>
    /// <param name="group">Die Farbgruppe. / The colour group.</param>
    public void AddGroup(TColorGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);
        _groups.Add(group);
    }
}
