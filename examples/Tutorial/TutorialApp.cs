// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Tutorial.Steps;

namespace TuiVision.Examples.Tutorial;

/// <summary>
/// Launcher-Klasse für die 16-teilige TuiVision-Tutorial-Reihe.
/// Sucht den angeforderten Schritt im <see cref="TutorialStepCatalog"/> und führt
/// die zugehörige Anwendung aus. Wenn das Token unbekannt ist, wird eine
/// Fehleranwendung gestartet, die sofort beendet wird.
///
/// Launcher class for the 16-part TuiVision tutorial series.
/// Looks up the requested step in the <see cref="TutorialStepCatalog"/> and runs
/// the associated application. If the token is unknown, a fallback error
/// application is started that terminates immediately.
/// </summary>
public class TutorialApp
{
    private readonly TRect _bounds;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TutorialApp"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TutorialApp"/> class.
    /// </summary>
    /// <param name="token">
    /// Das Token des auszuführenden Tutorial-Schritts, z. B. „tvguid01".
    ///
    /// The token of the tutorial step to run, e.g. "tvguid01".
    /// </param>
    /// <param name="bounds">
    /// Die Konsolengrenzen für die gestartete Anwendung.
    ///
    /// The console bounds for the launched application.
    /// </param>
    /// <param name="headless">
    /// Wenn <c>true</c>, beendet sich die gestartete Anwendung nach dem ersten Ereigniszyklus.
    ///
    /// When <c>true</c>, the launched application terminates after the first event cycle.
    /// </param>
    public TutorialApp(string token, TRect bounds, bool headless = false)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
        _bounds = bounds;
        IsHeadless = headless;

        // Schritt im Katalog nachschlagen / Look up step in catalog
        TutorialStepCatalog.All.TryGetValue(token, out ITutorialStep? step);
        CurrentStep = step;
    }

    /// <summary>
    /// Das angeforderte Tutorial-Token.
    ///
    /// The requested tutorial token.
    /// </summary>
    public string Token { get; }

    /// <summary>
    /// Der gefundene Tutorial-Schritt, oder <c>null</c>, wenn das Token unbekannt ist.
    ///
    /// The found tutorial step, or <c>null</c> if the token is unknown.
    /// </summary>
    public ITutorialStep? CurrentStep { get; }

    /// <summary>
    /// Gibt an, ob der Launcher im Headless-Modus ausgeführt wird.
    ///
    /// Indicates whether the launcher runs in headless mode.
    /// </summary>
    public bool IsHeadless { get; }

    /// <summary>
    /// Das Token des zuletzt tatsaechlich gestarteten Tutorial-Schritts.
    /// Der Wert bleibt <c>null</c>, wenn ein unbekanntes Token den Fallback ausgeloest hat.
    ///
    /// The token of the most recently started tutorial step.
    /// The value remains <c>null</c> when an unknown token triggered fallback.
    /// </summary>
    public string? LastRunStepToken { get; private set; }

    /// <summary>
    /// Gibt an, ob der letzte Lauf die Fallback-Anwendung fuer ein unbekanntes Token genutzt hat.
    ///
    /// Indicates whether the last run used the fallback application for an unknown token.
    /// </summary>
    public bool LastRunUsedFallback { get; private set; }

    /// <summary>
    /// Führt den Tutorial-Schritt aus.
    /// Wenn das Token unbekannt ist, wird eine Fallback-Anwendung gestartet.
    ///
    /// Runs the tutorial step.
    /// If the token is unknown, a fallback application is started.
    /// </summary>
    public void Run()
    {
        if (CurrentStep != null)
        {
            // Bekannten Schritt starten / Start known step
            LastRunStepToken = CurrentStep.Token;
            LastRunUsedFallback = false;
            TApplication app = CurrentStep.CreateApp(_bounds, IsHeadless);
            app.Run();
        }
        else
        {
            // Fallback-Anwendung für unbekanntes Token starten / Start fallback application for unknown token
            LastRunStepToken = null;
            LastRunUsedFallback = true;
            FallbackApp fallback = new(_bounds, Token, IsHeadless);
            fallback.Run();
        }
    }
}

/// <summary>
/// Fallback-Anwendung, die bei einem unbekannten Tutorial-Token gestartet wird.
/// Sie meldet das fehlende Token und beendet sich sofort.
///
/// Fallback application started when an unknown tutorial token is requested.
/// It reports the missing token and terminates immediately.
/// </summary>
file sealed class FallbackApp : TApplication
{
    private bool _eventFired;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="FallbackApp"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="FallbackApp"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Anwendung. / The bounds of the application.</param>
    /// <param name="unknownToken">Das nicht gefundene Token. / The token that was not found.</param>
    /// <param name="headless">Headless-Modus aktivieren. / Enable headless mode.</param>
    public FallbackApp(TRect bounds, string unknownToken, bool headless) : base(bounds)
    {
        _ = headless; // Fallback beendet sich immer sofort / Fallback always terminates immediately
        UnknownToken = unknownToken;

        // Fehlerfenster einfügen / Insert error window
        TWindow errorWindow = new(
            $"Unbekanntes Token / Unknown token: {unknownToken}",
            5, 5, 60, 8);
        Desktop?.Insert(errorWindow);
    }

    /// <summary>
    /// Das nicht gefundene Token.
    ///
    /// The token that was not found.
    /// </summary>
    public string UnknownToken { get; }

    /// <inheritdoc/>
    public override void GetEvent(out TEvent @event)
    {
        // Fallback beendet sich immer sofort / Fallback always terminates immediately
        if (!_eventFired)
        {
            _eventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }
}
