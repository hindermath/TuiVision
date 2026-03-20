// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Gibt die aktuelle Phase des Drei-Phasen-Ereignis-Dispatching einer <see cref="TGroup"/> an.
/// Diese Enumeration wird intern verwendet, um den Dispatch-Zustand zu verfolgen.
///
/// Indicates the current phase of a <see cref="TGroup"/>'s three-phase event dispatching.
/// This enumeration is used internally to track the dispatch state.
/// </summary>
internal enum DrawPhase
{
    /// <summary>
    /// Die fokussierte Phase: Das Ereignis wird an die aktuell fokussierte Kind-View gesendet.
    ///
    /// The focused phase: the event is sent to the currently focused child view.
    /// </summary>
    Focused,

    /// <summary>
    /// Die Vorverarbeitungsphase: Ereignisse werden an Kind-Views mit dem
    /// <see cref="TViewOptions.PreProcess"/>-Flag gesendet, bevor die fokussierte View sie erhält.
    ///
    /// The pre-processing phase: events are sent to child views with the
    /// <see cref="TViewOptions.PreProcess"/> flag before the focused view receives them.
    /// </summary>
    PreProcess,

    /// <summary>
    /// Die Nachverarbeitungsphase: Ereignisse werden an Kind-Views mit dem
    /// <see cref="TViewOptions.PostProcess"/>-Flag gesendet, nachdem die fokussierte View sie verarbeitet hat.
    ///
    /// The post-processing phase: events are sent to child views with the
    /// <see cref="TViewOptions.PostProcess"/> flag after the focused view has processed them.
    /// </summary>
    PostProcess
}
