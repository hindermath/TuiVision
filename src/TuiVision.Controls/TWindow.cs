// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Einfaches Fenster-Container-Element als managed Gegenstück zu <c>TWindow</c>
/// aus Turbo Vision (<c>twindow.cc</c>).
///
/// Simple window container element as a managed counterpart to <c>TWindow</c>
/// from Turbo Vision (<c>twindow.cc</c>).
/// </summary>
public class TWindow : TGroup
{
    /// <summary>
    /// Initialisiert ein Fenster mit Titel und Begrenzungsrahmen.
    ///
    /// Initializes a window with a title and bounds.
    /// </summary>
    /// <param name="title">Der Fenstertitel. / The window title.</param>
    /// <param name="x">Die linke Spalte. / The left column.</param>
    /// <param name="y">Die obere Zeile. / The top row.</param>
    /// <param name="width">Die Fensterbreite. / The window width.</param>
    /// <param name="height">Die Fensterhöhe. / The window height.</param>
    public TWindow(string title, int x, int y, int width, int height)
        : base(new TRect(x, y, x + width, y + height))
    {
        ArgumentNullException.ThrowIfNull(title);
        Title = title;
    }

    /// <summary>
    /// Der Fenstertitel.
    ///
    /// The window title.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Die aktuellen Fenstergrenzen.
    ///
    /// The current window bounds.
    /// </summary>
    public TRect Bounds => GetBounds();
}
