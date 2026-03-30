// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Gemeinsamer Widget-Testkontext mit Factory-Methoden für neue Controls.
/// Kapselt Standardbounds, Owner-Verdrahtung und Pufferzugriff für Widget-Tests.
///
/// Shared widget test context with factory methods for new controls.
/// Encapsulates default bounds, owner wiring, and buffer access for widget tests.
/// </summary>
public static class ControlsWidgetTestContext
{
    /// <summary>
    /// Erstellt eine befüllte <see cref="TListBox"/> mit den angegebenen Einträgen.
    ///
    /// Creates a populated <see cref="TListBox"/> with the given items.
    /// </summary>
    /// <param name="items">Die Listeneinträge. / The list items.</param>
    /// <param name="bounds">Optionale Bounds. / Optional bounds.</param>
    /// <returns>Eine konfigurierte Listenbox. / A configured list box.</returns>
    public static TListBox CreateListBox(string[] items, TRect? bounds = null)
    {
        TRect rect = bounds ?? new TRect(0, 0, 20, 5);
        TStringList list = new(items);
        TListBox listBox = new(rect, 1, null)
        {
            List = list
        };
        return listBox;
    }

    /// <summary>
    /// Erstellt einen <see cref="TProgressBar"/> mit dem angegebenen Bereich.
    ///
    /// Creates a <see cref="TProgressBar"/> with the given range.
    /// </summary>
    /// <param name="min">Der minimale Wert. / The minimum value.</param>
    /// <param name="max">Der maximale Wert. / The maximum value.</param>
    /// <param name="bounds">Optionale Bounds. / Optional bounds.</param>
    /// <returns>Ein konfigurierter Fortschrittsbalken. / A configured progress bar.</returns>
    public static TProgressBar CreateProgressBar(int min, int max, TRect? bounds = null)
    {
        TRect rect = bounds ?? new TRect(0, 0, 20, 1);
        return new TProgressBar(rect, min, max);
    }

    /// <summary>
    /// Erstellt einen <see cref="TParamText"/> mit der angegebenen Vorlage.
    ///
    /// Creates a <see cref="TParamText"/> with the given template.
    /// </summary>
    /// <param name="template">Die Formatvorlage. / The format template.</param>
    /// <param name="bounds">Optionale Bounds. / Optional bounds.</param>
    /// <returns>Ein konfigurierter Parametertext. / A configured param text.</returns>
    public static TParamText CreateParamText(string template, TRect? bounds = null)
    {
        TRect rect = bounds ?? new TRect(0, 0, 20, 1);
        return new TParamText(rect, template);
    }

    /// <summary>
    /// Erstellt eine <see cref="TComboBox"/> mit den angegebenen Auswahlmöglichkeiten.
    ///
    /// Creates a <see cref="TComboBox"/> with the given choices.
    /// </summary>
    /// <param name="choices">Die Auswahlmöglichkeiten. / The choices.</param>
    /// <param name="maxLen">Die maximale Eingabelänge. / The maximum input length.</param>
    /// <param name="historyId">Die optionale History-ID. / The optional history identifier.</param>
    /// <param name="bounds">Optionale Bounds. / Optional bounds.</param>
    /// <returns>Eine konfigurierte Kombinationsfeld-Instanz. / A configured combo box instance.</returns>
    public static TComboBox CreateComboBox(
        string[] choices,
        int maxLen = 20,
        string? historyId = null,
        TRect? bounds = null)
    {
        TRect rect = bounds ?? new TRect(0, 0, 20, 1);
        TStringList list = new(choices);
        return new TComboBox(rect, maxLen, list, historyId);
    }
}
