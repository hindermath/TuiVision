// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Konkretes Cluster für gegenseitig ausschließende Einfachauswahl.
///
/// Concrete cluster for mutually exclusive single selection.
/// </summary>
public sealed class TRadioButtons : TCluster
{
    /// <summary>
    /// Erstellt ein neues Radio-Cluster.
    ///
    /// Creates a new radio-button cluster.
    /// </summary>
    /// <param name="bounds">Die Bounds des Controls. / The bounds of the control.</param>
    /// <param name="strings">Die Item-Beschriftungen. / The item labels.</param>
    public TRadioButtons(TRect bounds, string[] strings) : base(bounds, strings)
    {
    }

    /// <summary>
    /// Prüft, ob ein Eintrag dem aktuellen Index entspricht.
    ///
    /// Checks whether an item matches the current index.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    /// <returns><c>true</c>, wenn der Eintrag ausgewählt ist. / <c>true</c> if the item is selected.</returns>
    protected override bool Mark(int item) => item >= 0 && item < Items.Length && Value == (uint)item;

    /// <summary>
    /// Setzt einen Eintrag als exklusive Auswahl.
    ///
    /// Sets an item as the exclusive selection.
    /// </summary>
    /// <param name="item">Der Item-Index. / The item index.</param>
    protected override void Press(int item)
    {
        if (item < 0 || item >= Items.Length)
        {
            return;
        }

        Value = (uint)item;
    }
}
