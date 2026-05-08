// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Sdlg;

/// <summary>
/// Vertikales ScrollDialog-Beispiel mit Headless-Seam.
///
/// Vertical ScrollDialog example with a headless seam.
/// </summary>
public sealed class SdlgApp : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;
    private readonly TScrollGroup _scrollGroup;
    private readonly List<TStaticText> _controls = [];

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public SdlgApp(TRect bounds, bool headless = false) : base(bounds)
    {
        _headless = headless;
        _scrollGroup = new TScrollGroup(new TRect(0, 0, 12, 6), hScrollBar: null, vScrollBar: new TScrollBar(new TRect(12, 0, 13, 6)));
        _scrollGroup.SetLimit(new TPoint(12, 40));
        for (int index = 0; index < 40; index++)
        {
            TStaticText control = new(new TRect(0, index, 11, index + 1), $"Control {index + 1:00}");
            _controls.Add(control);
            _scrollGroup.Insert(control);
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
    /// Scrollt zum angegebenen Control-Index.
    ///
    /// Scrolls to the specified control index.
    /// </summary>
    /// <param name="zeroBasedIndex">Nullbasierter Control-Index. / Zero-based control index.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string ScrollToControl(int zeroBasedIndex)
    {
        int index = Math.Clamp(zeroBasedIndex, 0, _controls.Count - 1);
        _scrollGroup.ScrollTo(new TPoint(0, index));
        return _scrollGroup.VisibleState;
    }

    /// <summary>
    /// Fokussiert ein Control deterministisch als sichtbaren Smoke-Zustand.
    ///
    /// Focuses a control deterministically as visible smoke state.
    /// </summary>
    /// <param name="zeroBasedIndex">Nullbasierter Control-Index. / Zero-based control index.</param>
    /// <returns>Der sichtbare Fokuszustand. / The visible focus state.</returns>
    public string FocusControl(int zeroBasedIndex)
    {
        int index = Math.Clamp(zeroBasedIndex, 0, _controls.Count - 1);
        return $"sdlg: focused {_controls[index].Text}";
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
