// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Beschreibt einen einzelnen Schritt aus der 16-teiligen TuiVision-Tutorial-Reihe.
/// Jeder Schritt ist einer Turbo-Vision-Beispieldatei (<c>tvguidNN</c>) zugeordnet
/// und kann eine vollständige TApplication-Instanz erzeugen.
///
/// Describes a single step from the 16-part TuiVision tutorial series.
/// Each step maps to a Turbo Vision example file (<c>tvguidNN</c>) and can
/// create a complete TApplication instance.
/// </summary>
public interface ITutorialStep
{
    /// <summary>
    /// Das eindeutige Token dieses Schritts, z. B. „tvguid01".
    ///
    /// The unique token for this step, e.g. "tvguid01".
    /// </summary>
    string Token { get; }

    /// <summary>
    /// Die Nummer dieses Schritts in der Sequenz (1 bis 16).
    ///
    /// The sequence number of this step (1 through 16).
    /// </summary>
    int SequenceNumber { get; }

    /// <summary>
    /// Der kurze Titel dieses Schritts (zweisprachig Deutsch/Englisch).
    ///
    /// The short title of this step (bilingual German/English).
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Die ausführliche Beschreibung des Lernziels dieses Schritts (zweisprachig Deutsch/Englisch).
    ///
    /// The detailed description of the learning goal of this step (bilingual German/English).
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Erzeugt die TApplication-Instanz für diesen Tutorial-Schritt.
    ///
    /// Creates the TApplication instance for this tutorial step.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="headless">
    /// Wenn <c>true</c>, wird die Anwendung ohne Konsoleninteraktion gestartet
    /// und beendet sich nach dem ersten Ereigniszyklus selbst.
    ///
    /// When <c>true</c>, the application starts without console interaction
    /// and terminates itself after the first event cycle.
    /// </param>
    /// <returns>
    /// Eine neue <see cref="TApplication"/>-Instanz für diesen Schritt.
    /// A new <see cref="TApplication"/> instance for this step.
    /// </returns>
    TApplication CreateApp(TRect bounds, bool headless);
}
