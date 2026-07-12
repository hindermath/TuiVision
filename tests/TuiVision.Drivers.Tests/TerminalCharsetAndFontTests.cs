// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Console.Tests;

/// <summary>
/// Prüft hostunabhängiges Unicode-/KOI8-R-Mapping und die begrenzte rohe
/// 8x16-Font-Fixture.
///
/// Verifies host-independent Unicode/KOI8-R mapping and the bounded raw 8x16
/// font fixture.
/// </summary>
[TestClass]
public sealed class TerminalCharsetAndFontTests
{
    /// <summary>
    /// Prüft ASCII und repräsentative russische KOI8-R-Bytes.
    ///
    /// Verifies ASCII and representative Russian KOI8-R bytes.
    /// </summary>
    [TestMethod]
    public void CharsetMapper_Koi8R_MapsKnownBytes()
    {
        Assert.AreEqual('A', TerminalCharsetMapper.MapByte(0x41, TerminalCharset.Koi8R).Glyph);
        Assert.AreEqual('а', TerminalCharsetMapper.MapByte(0xC1, TerminalCharset.Koi8R).Glyph);
        Assert.AreEqual('А', TerminalCharsetMapper.MapByte(0xE1, TerminalCharset.Koi8R).Glyph);
        Assert.AreEqual('ё', TerminalCharsetMapper.MapByte(0xA3, TerminalCharset.Koi8R).Glyph);
        Assert.AreEqual('Ё', TerminalCharsetMapper.MapByte(0xB3, TerminalCharset.Koi8R).Glyph);
    }

    /// <summary>
    /// Prüft Unicode-Identität und das einzige Ersatzzeichen U+FFFD.
    ///
    /// Verifies Unicode identity and the sole U+FFFD replacement character.
    /// </summary>
    [TestMethod]
    public void CharsetMapper_Unicode_MapsOrReplacesDeterministically()
    {
        CharsetMappingResult mapped = TerminalCharsetMapper.MapUnicode('ü');
        CharsetMappingResult invalid = TerminalCharsetMapper.MapUnicode('\uD800');

        Assert.AreEqual(CharsetMappingOutcome.Mapped, mapped.Outcome);
        Assert.AreEqual('ü', mapped.Glyph);
        Assert.AreEqual(CharsetMappingOutcome.Replaced, invalid.Outcome);
        Assert.AreEqual('\uFFFD', invalid.Glyph);
    }

    /// <summary>
    /// Prüft Unsupported ohne Host-Codepage-Fallback.
    ///
    /// Verifies unsupported input without host-codepage fallback.
    /// </summary>
    [TestMethod]
    public void CharsetMapper_UnknownCharset_IsUnsupported()
    {
        CharsetMappingResult result = TerminalCharsetMapper.MapByte(0xE1, TerminalCharset.Unknown);

        Assert.AreEqual(CharsetMappingOutcome.Unsupported, result.Outcome);
        Assert.AreEqual('\uFFFD', result.Glyph);
        Assert.IsTrue(result.Reason.Contains("unsupported", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Prüft, dass Locale und Standard-Encoding kein Mapping beeinflussen.
    ///
    /// Verifies that locale and default encoding do not influence mapping.
    /// </summary>
    [TestMethod]
    public void CharsetMapper_IsIndependentOfCurrentCulture()
    {
        System.Globalization.CultureInfo original = System.Globalization.CultureInfo.CurrentCulture;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
            char first = TerminalCharsetMapper.MapByte(0xE9, TerminalCharset.Koi8R).Glyph;
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
            char second = TerminalCharsetMapper.MapByte(0xE9, TerminalCharset.Koi8R).Glyph;
            Assert.AreEqual(first, second);
        }
        finally
        {
            System.Globalization.CultureInfo.CurrentCulture = original;
        }
    }

    /// <summary>
    /// Prüft die source-controlled 8x16-Fixture und unabhängige Glyphenzeilen.
    ///
    /// Verifies the source-controlled 8x16 fixture and independent glyph rows.
    /// </summary>
    [TestMethod]
    public void FontFixture_ExactRawShape_IsPublished()
    {
        byte[] data = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Fixtures", "terminal-font-8x16.bin"));

        BitmapFontFixtureResult result = BitmapFontFixture.TryCreate(
            data,
            width: 8,
            height: 16,
            glyphCount: 256,
            bytesPerGlyph: 16,
            format: "raw",
            sourceId: "Fixtures/terminal-font-8x16.bin");

        Assert.AreEqual(BitmapFontFixtureOutcome.Valid, result.Outcome);
        Assert.IsNotNull(result.Fixture);
        Assert.AreEqual(4096, result.Fixture.DataLength);
        Assert.AreEqual(16, result.Fixture.GetGlyphRows(0).Length);
    }

    /// <summary>
    /// Prüft atomare Geometrie-, Größen-, Format- und Pfadablehnung.
    ///
    /// Verifies atomic geometry, size, format, and path rejection.
    /// </summary>
    [TestMethod]
    public void FontFixture_InvalidShapeFormatOrPath_IsNotPublished()
    {
        byte[] exact = new byte[4096];
        (BitmapFontFixtureResult Result, BitmapFontFixtureOutcome Outcome)[] cases =
        {
            (BitmapFontFixture.TryCreate(exact, 9, 16, 256, 16, "raw", "fixture.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(exact, 8, 15, 256, 16, "raw", "fixture.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(exact, 8, 16, 255, 16, "raw", "fixture.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(exact, 8, 16, 256, 15, "raw", "fixture.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(new byte[4095], 8, 16, 256, 16, "raw", "fixture.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(new byte[4097], 8, 16, 256, 16, "raw", "fixture.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(exact, 8, 16, 256, 16, "psf", "fixture.psf"), BitmapFontFixtureOutcome.Unsupported),
            (BitmapFontFixture.TryCreate(exact, 8, 16, 256, 16, "raw", "/tmp/font.bin"), BitmapFontFixtureOutcome.Rejected),
            (BitmapFontFixture.TryCreate(exact, 8, 16, 256, 16, "raw", "../font.bin"), BitmapFontFixtureOutcome.Rejected)
        };

        foreach ((BitmapFontFixtureResult result, BitmapFontFixtureOutcome expected) in cases)
        {
            Assert.AreEqual(expected, result.Outcome);
            Assert.IsNull(result.Fixture);
        }
    }
}
