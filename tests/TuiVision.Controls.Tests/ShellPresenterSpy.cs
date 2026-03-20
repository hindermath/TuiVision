// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Drivers.Console;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Ein Test-Spion für <see cref="IConsolePresenter"/>, der den zuletzt präsentierten Frame speichert.
/// Er dient zur Validierung, ob und was die Shell gerendert hat.
///
/// A test spy for <see cref="IConsolePresenter"/> that stores the last presented frame.
/// Used to validate if and what the shell rendered.
/// </summary>
public sealed class ShellPresenterSpy : IConsolePresenter
{
    /// <summary>
    /// Ruft den zuletzt durch <see cref="Present"/> übergebenen Frame ab.
    ///
    /// Gets the last frame passed via <see cref="Present"/>.
    /// </summary>
    public TConsoleBuffer? LastFrame { get; private set; }

    /// <summary>
    /// Gibt an, ob <see cref="Present"/> mindestens einmal aufgerufen wurde.
    ///
    /// Indicates whether <see cref="Present"/> has been called at least once.
    /// </summary>
    public bool PresentCalled => LastFrame != null;

    /// <summary>
    /// Speichert den übergebenen Frame zur späteren Inspektion.
    ///
    /// Stores the provided frame for later inspection.
    /// </summary>
    /// <param name="frame">Der zu präsentierende Frame. / The frame to present.</param>
    public void Present(TConsoleBuffer frame)
    {
        LastFrame = frame;
    }

    /// <summary>
    /// Setzt den Spion-Status zurück.
    ///
    /// Resets the spy state.
    /// </summary>
    public void Reset()
    {
        LastFrame = null;
    }
}
