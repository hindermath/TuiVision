// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Sdlg2;

/// <summary>
/// Horizontal und vertikal scrollbares Dialog-Beispiel mit Headless-Seam.
///
/// Horizontally and vertically scrollable dialog example with a headless seam.
/// </summary>
public sealed class Sdlg2App : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;
    private readonly TScrollGroup _scrollGroup;
    private readonly Dictionary<(int X, int Y), TStaticText> _controls = [];

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public Sdlg2App(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        _scrollGroup = new TScrollGroup(
            new TRect(0, 0, 8, 5),
            new TScrollBar(new TRect(0, 5, 8, 6), horizontal: true),
            new TScrollBar(new TRect(8, 0, 9, 5)));
        _scrollGroup.SetLimit(new TPoint(30, 20));
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 30; x++)
            {
                TStaticText control = new(new TRect(x, y, x + 10, y + 1), $"Cell {x:00}/{y:00}");
                _controls[(x, y)] = control;
                _scrollGroup.Insert(control);
            }
        }
    }

    /// <summary>
    /// Aktueller Scroll-Offset.
    ///
    /// Current scroll offset.
    /// </summary>
    public TPoint ScrollOffset => _scrollGroup.Delta;

    /// <summary>
    /// Maximaler Scroll-Offset.
    ///
    /// Maximum scroll offset.
    /// </summary>
    public TPoint MaximumOffset => new(Math.Max(0, _scrollGroup.Limit.X - _scrollGroup.Size.X), Math.Max(0, _scrollGroup.Limit.Y - _scrollGroup.Size.Y));

    /// <summary>
    /// Scrollt zur angegebenen Zelle.
    ///
    /// Scrolls to the specified cell.
    /// </summary>
    /// <param name="x">Die X-Position. / The X position.</param>
    /// <param name="y">Die Y-Position. / The Y position.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ScrollToCell(int x, int y)
    {
        _scrollGroup.ScrollTo(new TPoint(x, y));
        return _scrollGroup.VisibleState;
    }

    /// <summary>
    /// Fokussiert eine Zelle deterministisch als sichtbaren Smoke-Zustand.
    ///
    /// Focuses a cell deterministically as visible smoke state.
    /// </summary>
    /// <param name="x">Die X-Position. / The X position.</param>
    /// <param name="y">Die Y-Position. / The Y position.</param>
    /// <returns>Der sichtbare Fokuszustand. / The visible focus state.</returns>
    public string FocusControl(int x, int y)
    {
        int clampedX = Math.Clamp(x, 0, 29);
        int clampedY = Math.Clamp(y, 0, 19);
        TStaticText control = _controls[(clampedX, clampedY)];
        _scrollGroup.SetFocus(control);
        return $"sdlg2: focused {control.Text}; visible {_scrollGroup.VisibleState}";
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
