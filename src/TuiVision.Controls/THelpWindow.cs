// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls.Internal;
using TuiVision.Core;
using TuiVision.Serialization;

namespace TuiVision.Controls;

/// <summary>
/// Nicht-modaler Host fuer runtime-lesbare Hilfe.
///
/// Non-modal host for runtime-readable help.
/// </summary>
public sealed class THelpWindow : FramedHostView
{
    /// <summary>
    /// Initialisiert ein neues Hilfefenster.
    ///
    /// Initializes a new help window.
    /// </summary>
    /// <param name="bounds">Die Bounds des Hosts. / The host bounds.</param>
    /// <param name="helpFile">Die Hilfedatei. / The help file.</param>
    /// <param name="context">Der Startkontext. / The initial context.</param>
    public THelpWindow(TRect bounds, THelpFile helpFile, int context) : base(bounds, "Help")
    {
        Viewer = new THelpViewer(new TRect(1, 1, Math.Max(2, bounds.Width - 1), Math.Max(2, bounds.Height - 1)), helpFile);
        Insert(Viewer);
        SetFocus(Viewer);
        Viewer.OpenContext(context);
    }

    /// <summary>
    /// Der eingebettete Help-Viewer.
    ///
    /// The embedded help viewer.
    /// </summary>
    public THelpViewer Viewer { get; }

    /// <summary>
    /// Oeffnet einen neuen Hilfekontext.
    ///
    /// Opens a new help context.
    /// </summary>
    /// <param name="context">Die Kontext-ID. / The context identifier.</param>
    public void OpenContext(int context)
    {
        Viewer.OpenContext(context);
        DrawView();
    }
}
