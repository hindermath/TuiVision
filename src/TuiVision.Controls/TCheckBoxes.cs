// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Konkretes Cluster für unabhängige Mehrfachauswahl.
///
/// Concrete cluster for independent multi-selection.
/// </summary>
public sealed class TCheckBoxes : TCluster
{
    /// <summary>
    /// Erstellt ein neues Checkbox-Cluster.
    ///
    /// Creates a new check box cluster.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The bounds of the control.</param>
    /// <param name="strings">Die Item-Beschriftungen. / The item labels.</param>
    public TCheckBoxes(TRect bounds, string[] strings) : base(bounds, strings)
    {
    }

    /// <summary>
    /// Prüft, ob das Bit eines Eintrags gesetzt ist.
    ///
    /// Checks whether an item's bit is set.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    /// <returns><c>true</c>, wenn der Eintrag ausgewählt ist. / <c>true</c> if the item is selected.</returns>
    protected override bool Mark(int item) => item >= 0 && item < Items.Length && (Value & (1u << item)) != 0;

    /// <summary>
    /// Toggelt das Zustands-Bit eines Eintrags.
    ///
    /// Toggles the state bit of an item.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    protected override void Press(int item)
    {
        if (item < 0 || item >= Items.Length)
        {
            return;
        }

        Value ^= 1u << item;
    }
}
