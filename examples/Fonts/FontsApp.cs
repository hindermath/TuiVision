// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Text;
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Examples.Shared;

namespace TuiVision.Examples.Fonts;

/// <summary>Kontrollierte Font-Fixture-Szenarien. / Controlled font-fixture scenarios.</summary>
public enum FontFixtureScenario
{
    /// <summary>Gültige Projekt-Fixture. / Valid project fixture.</summary>
    Valid,
    /// <summary>Falsche Datenlänge. / Wrong data length.</summary>
    WrongLength,
    /// <summary>Falsche Geometrie. / Wrong geometry.</summary>
    WrongGeometry,
    /// <summary>Falscher Stride. / Wrong stride.</summary>
    WrongStride,
    /// <summary>Nicht unterstütztes Format. / Unsupported format.</summary>
    UnsupportedFormat,
    /// <summary>Unzulässige Quellenkennung. / Invalid source identifier.</summary>
    InvalidSource,
    /// <summary>Formal gültige, aber leere ausgewählte Glyphe. / Formally valid but blank selected glyph.</summary>
    BlankSelectedGlyph
}

/// <summary>
/// Zeigt eine kontrollierte rohe 8x16-Font-Fixture ohne Host-Fontzugriff.
/// Shows a controlled raw 8x16 font fixture without host-font access.
/// </summary>
public sealed class FontsApp : Wave4Application
{
    /// <summary>Wählt die nächste Glyphe. / Selects the next glyph.</summary>
    public const ushort CmNextGlyph = 22201;

    /// <summary>Zeigt die Beschreibung. / Shows the description.</summary>
    public const ushort CmDescription = 22202;

    private readonly FontFixtureScenario _scenario;
    private TWindow? _fontWindow;

    /// <summary>Initialisiert die Fontdemo. / Initializes the font demo.</summary>
    public FontsApp(TRect bounds, bool headless = false, FontFixtureScenario scenario = FontFixtureScenario.Valid) : base(bounds, headless)
    {
        _scenario = scenario;
        SelectedGlyph = 65;
        FixtureResult = CreateFixture(scenario);
        RenderFixture();
    }

    /// <summary>Validierungsergebnis der Fixture. / Fixture validation result.</summary>
    public BitmapFontFixtureResult FixtureResult { get; }

    /// <summary>Ausgewählte Glyphe. / Selected glyph.</summary>
    public int SelectedGlyph { get; private set; }

    /// <summary>Ob die ausgewählte Glyphe sichtbare Pixel enthält. / Whether the selected glyph contains visible pixels.</summary>
    public bool SelectedGlyphHasInk { get; private set; }

    /// <summary>Ob ein sichtbarer Fallback nötig ist. / Whether a visible fallback is required.</summary>
    public bool FallbackVisible { get; private set; }

    /// <summary>Text des Beschreibungspfads. / Description-path text.</summary>
    public string DescriptionText =>
        "Fonts description: Die kopierte 8x16-Rohfixture wird nur gelesen und als Textmatrix gezeigt; kein Font wird installiert. / " +
        "The copied raw 8x16 fixture is read only and shown as a text matrix; no font is installed.";

    /// <inheritdoc />
    protected override TMenuBar InitMenuBar(TRect bounds) => new(bounds)
    {
        Menu = new TMenuItem(
            "~F~onts",
            0,
            Wave4Runtime.HelpMenu(CmDescription, new TMenuItem("~E~nde / E~x~it", ShellCommandIds.cmQuit)),
            new TMenuItem("~N~ächste Glyphe / ~N~ext glyph", CmNextGlyph))
    };

    /// <inheritdoc />
    public override void HandleEvent(TEvent @event)
    {
        if (@event.What == TEventKind.Command && @event.Message.Command == CmNextGlyph)
        {
            SelectedGlyph = (SelectedGlyph + 1) % BitmapFontFixture.RequiredGlyphCount;
            RenderFixture();
            @event.Clear();
            return;
        }

        if (@event.What == TEventKind.Command && @event.Message.Command == CmDescription)
        {
            ShowWave4Description("Fonts", DescriptionText);
            @event.Clear();
            return;
        }

        base.HandleEvent(@event);
    }

    private static BitmapFontFixtureResult CreateFixture(FontFixtureScenario scenario)
    {
        byte[] data = scenario == FontFixtureScenario.BlankSelectedGlyph
            ? new byte[BitmapFontFixture.RequiredDataLength]
            : File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Fixtures", "font-8x16.bin"));
        int width = scenario == FontFixtureScenario.WrongGeometry ? 9 : BitmapFontFixture.RequiredWidth;
        int stride = scenario == FontFixtureScenario.WrongStride ? 15 : BitmapFontFixture.RequiredBytesPerGlyph;
        string format = scenario == FontFixtureScenario.UnsupportedFormat ? "psf" : "raw";
        string source = scenario == FontFixtureScenario.InvalidSource ? "../font-8x16.bin" : "Fixtures/font-8x16.bin";
        ReadOnlySpan<byte> candidate = scenario == FontFixtureScenario.WrongLength ? data.AsSpan(0, data.Length - 1) : data;

        // Alle Metadaten gehen gemeinsam durch den Framework-Vertrag; die App veröffentlicht nie Teilzustände.
        // All metadata passes through the framework contract together; the app never publishes partial state.
        return BitmapFontFixture.TryCreate(
            candidate,
            width,
            BitmapFontFixture.RequiredHeight,
            BitmapFontFixture.RequiredGlyphCount,
            stride,
            format,
            source);
    }

    private void RenderFixture()
    {
        TGroup desktop = Desktop!;
        if (_fontWindow is TWindow previous && previous.Owner == desktop)
        {
            desktop.Remove(previous);
        }

        TRect region = Wave4Runtime.MainRegion(desktop, 58, 22);
        TWindow window = new("Fonts", region.A.X, region.A.Y, region.Width, region.Height);
        string text = BuildFixtureText();
        window.Insert(new TStaticText(
            new TRect(2, 2, Math.Max(3, region.Width - 2), Math.Max(3, region.Height - 1)),
            text));
        desktop.Insert(window);
        desktop.SetFocus(window);
        _fontWindow = window;
        SetWave4Visible(window, "TWindow");
        SetWave4Status("Fonts", $"{FixtureResult.Outcome} scenario={_scenario} glyph={SelectedGlyph}");
    }

    private string BuildFixtureText()
    {
        if (FixtureResult.Fixture is null)
        {
            FallbackVisible = true;
            SelectedGlyphHasInk = false;
            return $"Fonts Fixture Fallback\nScenario: {_scenario}\nOutcome: {FixtureResult.Outcome}\n{FixtureResult.Reason}";
        }

        ReadOnlySpan<byte> rows = FixtureResult.Fixture.GetGlyphRows(SelectedGlyph).Span;
        SelectedGlyphHasInk = rows.ContainsAnyExcept((byte)0);
        if (!SelectedGlyphHasInk)
        {
            FallbackVisible = true;
            return $"Fonts Fixture Fallback\nScenario: {_scenario}\nGlyph {SelectedGlyph} is blank\nOutcome: {FixtureResult.Outcome}";
        }

        FallbackVisible = false;
        StringBuilder text = new();
        text.AppendLine("Fonts raw 8x16 fixture");
        text.AppendLine($"Glyph {SelectedGlyph} | 8x16 | 256 glyphs | 4096 bytes");
        // MSB links zu zeichnen macht Bitreihenfolge und Zell-Proof ohne Farbe sichtbar.
        // Drawing the MSB on the left makes bit order and cell proof visible without color.
        foreach (byte row in rows)
        {
            for (int bit = 7; bit >= 0; bit--)
            {
                text.Append((row & (1 << bit)) == 0 ? '.' : '#');
            }

            text.AppendLine();
        }

        return text.ToString();
    }
}
