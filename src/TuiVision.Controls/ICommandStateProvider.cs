// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Liefert Command-Zustände für eine aktive View, ohne eine globale Registry
/// oder eine Abhängigkeit von konkreten Shell-Komponenten einzuführen.
///
/// Supplies command states for an active view without introducing a global
/// registry or a dependency on concrete shell components.
/// </summary>
public interface ICommandStateProvider
{
    /// <summary>
    /// Erstellt die Command-Zustände für die aktuelle View-Momentaufnahme.
    ///
    /// Creates command states for the current view snapshot.
    /// </summary>
    /// <returns>Eine Command-zu-Verfügbarkeit-Abbildung. / A command-to-availability mapping.</returns>
    IReadOnlyDictionary<ushort, bool> GetCommandStates();
}
