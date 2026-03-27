// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Compatibility;

/// <summary>
/// Verwaltete Eingabe-Bruecke zwischen <see cref="ConsoleKeyInfo"/> und dem Turbo-Vision-Ereignismodell.
/// Die Klasse buendelt die xterm-kompatible Tastaturmenge, die in Phase 8 als Ersatz fuer
/// historische Raw-Input-Treiber nachgewiesen wird.
///
/// Managed input bridge between <see cref="ConsoleKeyInfo"/> and the Turbo Vision event model.
/// The class bundles the xterm-compatible key subset that Phase 8 uses as proof replacement
/// for the historical raw-input drivers.
/// </summary>
public static class TConsoleInputAdapter
{
    private static readonly HashSet<ConsoleKey> XtermCompatibleKeys =
    [
        ConsoleKey.Enter,
        ConsoleKey.Escape,
        ConsoleKey.Backspace,
        ConsoleKey.Tab,
        ConsoleKey.LeftArrow,
        ConsoleKey.RightArrow,
        ConsoleKey.UpArrow,
        ConsoleKey.DownArrow,
        ConsoleKey.Home,
        ConsoleKey.End,
        ConsoleKey.PageUp,
        ConsoleKey.PageDown,
        ConsoleKey.Insert,
        ConsoleKey.Delete,
        ConsoleKey.F1,
        ConsoleKey.F2,
        ConsoleKey.F3,
        ConsoleKey.F4,
        ConsoleKey.F5,
        ConsoleKey.F6,
        ConsoleKey.F7,
        ConsoleKey.F8,
        ConsoleKey.F9,
        ConsoleKey.F10,
    ];

    /// <summary>
    /// Erzeugt aus einer verwalteten Konsolentaste ein Turbo-Vision-Tastaturereignis.
    ///
    /// Creates a Turbo Vision keyboard event from a managed console key.
    /// </summary>
    /// <param name="keyInfo">Die .NET-Konsolentaste. / The .NET console key.</param>
    /// <returns>Ein vollstaendig initialisiertes Tastaturereignis. / A fully initialized keyboard event.</returns>
    public static TEvent CreateKeyDownEvent(ConsoleKeyInfo keyInfo) =>
        TEvent.CreateKeyDown(TKeyCodeTranslator.FromConsoleKey(keyInfo));

    /// <summary>
    /// Prueft, ob eine Taste zur xterm-kompatiblen Navigations-/Funktionstastenmenge gehoert,
    /// die vom verwalteten Uebersetzer explizit nachgebildet wird.
    ///
    /// Checks whether a key belongs to the xterm-compatible navigation/function-key subset
    /// that is explicitly reproduced by the managed translator.
    /// </summary>
    /// <param name="key">Die zu pruefende Taste. / The key to examine.</param>
    /// <returns>
    /// <c>true</c>, wenn die Taste im explizit unterstuetzten xterm-kompatiblen Satz liegt;
    /// andernfalls <c>false</c>.
    /// <c>true</c> if the key is part of the explicitly supported xterm-compatible subset;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool IsXtermCompatibleKey(ConsoleKey key) => XtermCompatibleKeys.Contains(key);
}
