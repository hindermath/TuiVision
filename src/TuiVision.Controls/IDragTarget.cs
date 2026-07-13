// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Markiert eine View als opt-in Ziel einer begrenzten Drag-Session.
///
/// Marks a view as an opt-in target of a bounded drag session.
/// </summary>
public interface IDragTarget
{
    /// <summary>
    /// Prüft eine Drop-Anfrage ohne Seiteneffekte. Die eigentliche Fachaktion
    /// bleibt beim Nutzer des unveränderlichen <see cref="TDragResult"/>.
    ///
    /// Checks a drop request without side effects. The actual domain action
    /// remains with the consumer of the immutable <see cref="TDragResult"/>.
    /// </summary>
    /// <param name="source">Die Quell-View. / The source view.</param>
    /// <param name="payload">Die optionale laufzeitlokale Nutzlast. / The optional process-local payload.</param>
    /// <param name="where">Die begrenzte Zielposition. / The bounded target position.</param>
    /// <returns><c>true</c>, wenn das Ziel den Drop akzeptiert. / <c>true</c> when the target accepts the drop.</returns>
    bool CanAcceptDrop(TView source, object? payload, TPoint where);
}
