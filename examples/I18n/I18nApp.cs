// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.Shared;
using TuiVision.Serialization;

namespace TuiVision.Examples.I18n;

/// <summary>Zeigt explizite, host-unabhängige Ressourcenwahl. / Shows explicit, host-independent resource selection.</summary>
public sealed class I18nApp : Wave3Application
{
    /// <summary>Spanisch wählen. / Select Spanish.</summary>
    public const ushort CmSpanish = 19301;
    /// <summary>Fehlende Sprache prüfen. / Probe a missing language.</summary>
    public const ushort CmMissingLanguage = 19302;
    /// <summary>Fehlenden Key prüfen. / Probe a missing key.</summary>
    public const ushort CmMissingKey = 19303;
    /// <summary>Beschreibung anzeigen. / Show description.</summary>
    public const ushort CmDescription = 19304;

    private readonly TResourceFile _resources;
    private readonly List<string> _visibleHistory = [];

    /// <summary>Initialisiert die Ressourcen-Demo. / Initializes the resource demo.</summary>
    public I18nApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        _resources = new TResourceFile(new TRecordRegistry());
        _resources.Put("window", "Window");
        _resources.Put("window.en", "Window");
        _resources.Put("window.es", "Ventana");
        ShowLanguage("en", []);
    }

    /// <summary>Sichtbarer Lookup-Verlauf. / Visible lookup history.</summary>
    public string VisibleHistoryText => string.Join('\n', _visibleHistory);
    /// <summary>Beschreibungstext. / Description text.</summary>
    public string DescriptionText =>
        "I18n description: Die Sprache wird explizit gewaehlt; host locale und gettext beeinflussen den Proof nicht. / " +
        "Language is selected explicitly; host locale and gettext do not influence the proof.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds)
    {
        TMenuBar bar = new(bounds)
        {
            Menu = new TMenuItem("~S~prache / ~L~anguage", 0,
                Wave3Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
                new TMenuItem("~E~spanol", CmSpanish, null,
                    new TMenuItem("~F~rancais fallback", CmMissingLanguage, null,
                        new TMenuItem("Missing ~K~ey", CmMissingKey))))
        };
        return bar;
    }

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmSpanish)
        {
            ShowLanguage("es", []);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmMissingLanguage)
        {
            ShowLanguage("fr", ["en"]);
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmMissingKey)
        {
            TLocalizedResourceResult<string> result = TLocalizedResourceLookup.Find<string>(_resources, "missing", "es", ["en"]);
            _visibleHistory.Add($"requested=es missing-key attempted={string.Join(',', result.AttemptedKeys)}");
            ShowWave3Result("I18n", "I18n resources", "I18n resource view\nText: Missing resource\nKey: missing", "requested=es missing-key");
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave3Description("I18n", DescriptionText);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private void ShowLanguage(string requested, IEnumerable<string> fallbacks)
    {
        TLocalizedResourceResult<string> result = TLocalizedResourceLookup.Find<string>(_resources, "window", requested, fallbacks);
        string value = result.Value ?? "Missing resource";
        string matched = result.MatchedKey ?? "none";
        bool fallback = !string.Equals(matched, $"window.{requested}", StringComparison.Ordinal);
        string state = $"requested={requested} matched={matched} {(fallback ? "fallback" : "exact")} value={value}";
        _visibleHistory.Add(state);
        ShowWave3Result("I18n", "I18n resources", $"I18n resource view\nLanguage: {requested}\nText: {value}\nKey: {matched}\nMode: {(fallback ? "fallback" : "exact")}", state);
    }
}
