// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Beschreibt eine View, die eine Close-Anfrage mit explizitem Ergebnis beantwortet.
///
/// Describes a view that answers a close request with an explicit result.
/// </summary>
public interface ICloseableView
{
    /// <summary>
    /// Fordert den Abschluss des sichtbaren View-Lifecycles an.
    ///
    /// Requests completion of the visible view lifecycle.
    /// </summary>
    /// <param name="trigger">Der auslösende Pfad. / The triggering path.</param>
    /// <returns>Das unveränderliche Close-Ergebnis. / The immutable close result.</returns>
    TCloseResult RequestClose(TCloseTrigger trigger);
}
