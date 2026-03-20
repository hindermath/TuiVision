// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Der zentrale Arbeitsbereich der Anwendung.
/// Er verwaltet Kind-Fenster und sorgt für eine korrekte Fokus-Wiederherstellung.
///
/// The central workspace of the application.
/// It manages child windows and ensures correct focus recovery.
/// </summary>
public class TDesktop : TGroup
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TDesktop"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TDesktop"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen des Desktops. / The bounds of the desktop.</param>
    public TDesktop(TRect bounds) : base(bounds)
    {
    }

    /// <summary>
    /// Wird aufgerufen, wenn sich die aktuell fokussierte Ansicht ändert.
    /// Sorgt für einen Fallback zum Desktop selbst, wenn kein Kind mehr fokussierbar ist.
    ///
    /// Called when the currently focused view changes.
    /// Ensures fallback to the desktop itself if no child is focusable.
    /// </summary>
    protected override void CurrentChanged()
    {
        base.CurrentChanged();

        if (Current == null)
        {
            // Fallback: nächstes auswählbares Kind auswählen, sonst bleibt Desktop ohne Fokus.
            // Fallback: select the next eligible child; desktop remains focus target if none exists.
            SelectNext(true);
        }
    }
}
