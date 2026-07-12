// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.Cyrillic;

/// <summary>
/// Zeigt kontrollierte KOI8-R-/Unicode-Abbildungen ohne Host-Codepage.
/// Shows controlled KOI8-R/Unicode mappings without a host codepage.
/// </summary>
public sealed class CyrillicApp : Wave4Application
{
    /// <summary>Wechselt zum nächsten Mappingzustand. / Advances to the next mapping state.</summary>
    public const ushort CmNextMapping = 22101;

    /// <summary>Zeigt die Beschreibung. / Shows the description.</summary>
    public const ushort CmDescription = 22102;

    private readonly List<CharsetMappingOutcome> _observedOutcomes = [];
    private int _mappingIndex;
    private TWindow? _mappingWindow;

    /// <summary>Initialisiert die Zeichensatzdemo. / Initializes the charset demo.</summary>
    public CyrillicApp(TRect bounds, bool headless = false) : base(bounds, headless)
    {
        CurrentMapping = ResolveMapping(0);
        _observedOutcomes.Add(CurrentMapping.Outcome);
        RenderMapping();
    }

    /// <summary>Aktuelles Mappingergebnis. / Current mapping result.</summary>
    public CharsetMappingResult CurrentMapping { get; private set; }

    /// <summary>Im App-Loop beobachtete Zustände. / States observed in the app loop.</summary>
    public IReadOnlyList<CharsetMappingOutcome> ObservedOutcomes => _observedOutcomes;

    /// <summary>Text des Beschreibungspfads. / Description-path text.</summary>
    public string DescriptionText =>
        "Cyrillic description: Die feste KOI8-R-Tabelle macht die Abbildung ohne Host-Locale oder Host-Codepage prüfbar. / " +
        "The fixed KOI8-R table keeps mapping verifiable without a host locale or host codepage.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~C~yrillic",
            0,
            Wave4Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
            new TMenuItem("~N~ächstes Mapping / ~N~ext mapping", CmNextMapping))
    };

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmNextMapping)
        {
            _mappingIndex = (_mappingIndex + 1) % 4;
            CurrentMapping = ResolveMapping(_mappingIndex);
            _observedOutcomes.Add(CurrentMapping.Outcome);
            RenderMapping();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave4Description("Cyrillic", DescriptionText);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private static CharsetMappingResult ResolveMapping(int index) => index switch
    {
        0 => TerminalCharsetMapper.MapByte(0xE1, TerminalCharset.Koi8R),
        1 => TerminalCharsetMapper.MapByte(0xE1, TerminalCharset.Unicode),
        // Der ungültige Bereich wird vor dem Byte-Mapper abgelehnt, damit kein stilles Kürzen entsteht.
        // The invalid range is rejected before the byte mapper so no silent truncation occurs.
        2 => new CharsetMappingResult(
            TerminalCharset.Koi8R,
            -1,
            '\uFFFD',
            CharsetMappingOutcome.Rejected,
            "Source value is outside one byte. / Quellwert liegt außerhalb eines Bytes."),
        _ => TerminalCharsetMapper.MapByte(0xE1, TerminalCharset.Unknown)
    };

    private void RenderMapping()
    {
        TGroup desktop = Desktop!;
        if (_mappingWindow is TWindow previous && previous.Owner == desktop)
        {
            desktop.Remove(previous);
        }

        TRect region = Wave4Runtime.MainRegion(desktop, 64, 14);
        TWindow window = new("Cyrillic", region.A.X, region.A.Y, region.Width, region.Height);
        string source = CurrentMapping.SourceValue < 0 ? "invalid" : $"0x{CurrentMapping.SourceValue:X2}";
        string text =
            "Cyrillic mapping grid\n" +
            $"KOI8-R source: {source}\n" +
            $"Unicode glyph: {CurrentMapping.Glyph}\n" +
            $"Outcome: {CurrentMapping.Outcome}\n" +
            $"Reason: {CurrentMapping.Reason}\n" +
            "Proof: fixed-table; no Host-Locale/codepage";
        window.Insert(new TStaticText(
            new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)),
            text));
        desktop.Insert(window);
        desktop.SetFocus(window);
        _mappingWindow = window;
        SetWave4Visible(window, "TWindow");
        SetWave4Status("Cyrillic", $"fixed-table {CurrentMapping.Outcome} source={source}");
    }
}
