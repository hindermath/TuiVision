// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Console;

/// <summary>
/// Benennt das Ergebnis der Font-Fixture-Validierung.
///
/// Names the outcome of font-fixture validation.
/// </summary>
public enum BitmapFontFixtureOutcome
{
    /// <summary>Die Fixture erfüllt den exakten Raw-Vertrag. / The fixture satisfies the exact raw contract.</summary>
    Valid,

    /// <summary>Geometrie, Größe oder Quelle ist ungültig. / Geometry, size, or source is invalid.</summary>
    Rejected,

    /// <summary>Das Format liegt außerhalb des Raw-Vertrags. / The format is outside the raw contract.</summary>
    Unsupported
}

/// <summary>
/// Beschreibt das Ergebnis einer atomaren Font-Fixture-Validierung.
///
/// Describes the result of atomic font-fixture validation.
/// </summary>
/// <param name="Outcome">Validierungsstatus. / Validation outcome.</param>
/// <param name="Fixture">Veröffentlichte Fixture oder <c>null</c>. / Published fixture or <c>null</c>.</param>
/// <param name="Reason">Textorientierte Begründung. / Text-first rationale.</param>
public readonly record struct BitmapFontFixtureResult(
    BitmapFontFixtureOutcome Outcome,
    BitmapFontFixture? Fixture,
    string Reason);

/// <summary>
/// Repräsentiert exakt 256 rohe 8x16-Glyphen als beweisbare Metadaten, ohne
/// einen Host-Font zu installieren oder einen Generator auszuführen.
///
/// Represents exactly 256 raw 8x16 glyphs as provable metadata without
/// installing a host font or executing a generator.
/// </summary>
public sealed class BitmapFontFixture
{
    /// <summary>Erforderliche Glyphenbreite. / Required glyph width.</summary>
    public const int RequiredWidth = 8;

    /// <summary>Erforderliche Glyphenhöhe. / Required glyph height.</summary>
    public const int RequiredHeight = 16;

    /// <summary>Erforderliche Glyphenzahl. / Required glyph count.</summary>
    public const int RequiredGlyphCount = 256;

    /// <summary>Erforderliche Bytes pro Glyphe. / Required bytes per glyph.</summary>
    public const int RequiredBytesPerGlyph = 16;

    /// <summary>Erforderliche Gesamtlänge. / Required total length.</summary>
    public const int RequiredDataLength = 4096;

    private readonly byte[] _data;

    private BitmapFontFixture(byte[] data, string sourceId)
    {
        _data = data;
        SourceId = sourceId;
    }

    /// <summary>Breite einer Glyphe. / Width of one glyph.</summary>
    public int Width => RequiredWidth;

    /// <summary>Höhe einer Glyphe. / Height of one glyph.</summary>
    public int Height => RequiredHeight;

    /// <summary>Zahl der Glyphen. / Number of glyphs.</summary>
    public int GlyphCount => RequiredGlyphCount;

    /// <summary>Bytes pro Glyphe. / Bytes per glyph.</summary>
    public int BytesPerGlyph => RequiredBytesPerGlyph;

    /// <summary>Länge der kopierten Rohdaten. / Length of the copied raw data.</summary>
    public int DataLength => _data.Length;

    /// <summary>Repository- oder testrelative Quellenkennung. / Repository- or test-relative source identifier.</summary>
    public string SourceId { get; }

    /// <summary>
    /// Validiert alle Metadaten und Daten vor Veröffentlichung einer Fixture.
    ///
    /// Validates all metadata and data before publishing a fixture.
    /// </summary>
    /// <param name="data">Rohe Glyphendaten. / Raw glyph data.</param>
    /// <param name="width">Deklarierte Breite. / Declared width.</param>
    /// <param name="height">Deklarierte Höhe. / Declared height.</param>
    /// <param name="glyphCount">Deklarierte Glyphenzahl. / Declared glyph count.</param>
    /// <param name="bytesPerGlyph">Deklarierter Stride. / Declared stride.</param>
    /// <param name="format">Formatkennung, exakt <c>raw</c>. / Format identifier, exactly <c>raw</c>.</param>
    /// <param name="sourceId">Kontrollierte relative Quellenkennung. / Controlled relative source identifier.</param>
    /// <returns>Validierungsstatus und gegebenenfalls Fixture. / Validation outcome and fixture when valid.</returns>
    public static BitmapFontFixtureResult TryCreate(
        ReadOnlySpan<byte> data,
        int width,
        int height,
        int glyphCount,
        int bytesPerGlyph,
        string format,
        string sourceId)
    {
        ArgumentNullException.ThrowIfNull(format);
        ArgumentNullException.ThrowIfNull(sourceId);

        if (!string.Equals(format, "raw", StringComparison.OrdinalIgnoreCase))
        {
            return new BitmapFontFixtureResult(
                BitmapFontFixtureOutcome.Unsupported,
                null,
                "Only raw 8x16 fixtures are supported. / Nur rohe 8x16-Fixtures werden unterstützt.");
        }

        if (Path.IsPathRooted(sourceId) ||
            sourceId.Split('/', '\\').Any(part => part == "..") ||
            string.IsNullOrWhiteSpace(sourceId))
        {
            return Rejected("Fixture source must be a controlled relative identifier. / Fixture-Quelle muss eine kontrollierte relative Kennung sein.");
        }

        if (width != RequiredWidth || height != RequiredHeight ||
            glyphCount != RequiredGlyphCount || bytesPerGlyph != RequiredBytesPerGlyph)
        {
            return Rejected("Fixture geometry must be 8x16 with 256 glyphs and 16 bytes per glyph. / Fixture-Geometrie muss 8x16 mit 256 Glyphen und 16 Bytes pro Glyphe sein.");
        }

        if (data.Length != RequiredDataLength)
        {
            return Rejected("Fixture data must contain exactly 4,096 bytes. / Fixture-Daten müssen exakt 4.096 Bytes enthalten.");
        }

        return new BitmapFontFixtureResult(
            BitmapFontFixtureOutcome.Valid,
            new BitmapFontFixture(data.ToArray(), sourceId),
            "Raw 8x16 fixture validated. / Rohe 8x16-Fixture validiert.");
    }

    /// <summary>
    /// Liefert die 16 kopierten Zeilenbytes einer Glyphe.
    ///
    /// Returns the 16 copied row bytes of one glyph.
    /// </summary>
    /// <param name="glyphIndex">Index von 0 bis 255. / Index from 0 through 255.</param>
    /// <returns>Unabhängige Glyphenzeilen. / Independent glyph rows.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Der Index liegt außerhalb der Fixture. / The index is outside the fixture.</exception>
    public ReadOnlyMemory<byte> GetGlyphRows(int glyphIndex)
    {
        if (glyphIndex < 0 || glyphIndex >= RequiredGlyphCount)
        {
            throw new ArgumentOutOfRangeException(nameof(glyphIndex));
        }

        byte[] rows = new byte[RequiredBytesPerGlyph];
        Array.Copy(_data, glyphIndex * RequiredBytesPerGlyph, rows, 0, rows.Length);
        return rows;
    }

    private static BitmapFontFixtureResult Rejected(string reason) =>
        new(BitmapFontFixtureOutcome.Rejected, null, reason);
}
