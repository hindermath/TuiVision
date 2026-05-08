// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.TCombo;

/// <summary>
/// Kombinationsfeld-Beispiel mit Headless-Seam.
///
/// Combo-box example with a headless seam.
/// </summary>
public sealed class TComboApp : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;
    private TComboBox? _combo;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public TComboApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Aktueller Eingabewert.
    ///
    /// Current input value.
    /// </summary>
    public string InputValue => _combo?.Data ?? string.Empty;

    /// <summary>
    /// Laedt die Auswahlwerte.
    ///
    /// Loads choice values.
    /// </summary>
    /// <param name="choices">Die Auswahlwerte. / The choices.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string LoadChoices(IEnumerable<string> choices)
    {
        TStringList list = new();
        foreach (string choice in choices ?? [])
        {
            list.Add(choice);
        }

        _combo = new TComboBox(new TRect(0, 0, 20, 1), 40, list);
        return list.Count == 0 ? "tcombo: empty choices" : $"tcombo: loaded {list.Count} choices";
    }

    /// <summary>
    /// Waehlt einen Eintrag und synchronisiert die Eingabe.
    ///
    /// Selects an item and synchronises the input.
    /// </summary>
    /// <param name="index">Der Index. / The index.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectIndex(int index)
    {
        _combo?.SelectIndex(index);
        return $"tcombo: selected {InputValue}";
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
