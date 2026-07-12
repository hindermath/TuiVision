// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Console;

/// <summary>
/// Benennt die in Feature 021 unterstützten Zeichensätze.
///
/// Names the character sets supported by Feature 021.
/// </summary>
public enum TerminalCharset
{
    /// <summary>Unbekannter oder nicht unterstützter Zeichensatz. / Unknown or unsupported character set.</summary>
    Unknown,

    /// <summary>Unicode als kanonische Darstellung. / Unicode as the canonical representation.</summary>
    Unicode,

    /// <summary>Historische KOI8-R-Byteabbildung. / Historical KOI8-R byte mapping.</summary>
    Koi8R
}

/// <summary>
/// Benennt das Ergebnis einer Zeichensatzabbildung.
///
/// Names the outcome of a character-set mapping.
/// </summary>
public enum CharsetMappingOutcome
{
    /// <summary>Der Wert wurde exakt abgebildet. / The value was mapped exactly.</summary>
    Mapped,

    /// <summary>Der Wert wurde durch U+FFFD ersetzt. / The value was replaced by U+FFFD.</summary>
    Replaced,

    /// <summary>Der Eingabewert ist syntaktisch ungültig. / The input value is syntactically invalid.</summary>
    Rejected,

    /// <summary>Der Zeichensatz wird nicht unterstützt. / The character set is unsupported.</summary>
    Unsupported
}

/// <summary>
/// Beschreibt eine hostunabhängige Zeichenabbildung.
///
/// Describes a host-independent character mapping.
/// </summary>
/// <param name="SourceCharset">Quellzeichensatz. / Source character set.</param>
/// <param name="SourceValue">Numerischer Quellwert. / Numeric source value.</param>
/// <param name="Glyph">Ergebniszeichen oder U+FFFD. / Result glyph or U+FFFD.</param>
/// <param name="Outcome">Mappingstatus. / Mapping outcome.</param>
/// <param name="Reason">Textorientierte Begründung. / Text-first rationale.</param>
public readonly record struct CharsetMappingResult(
    TerminalCharset SourceCharset,
    int SourceValue,
    char Glyph,
    CharsetMappingOutcome Outcome,
    string Reason);

/// <summary>
/// Bildet Unicode- und KOI8-R-Werte ohne Host-Locale oder Host-Codepage ab.
///
/// Maps Unicode and KOI8-R values without a host locale or host codepage.
/// </summary>
public static class TerminalCharsetMapper
{
    private const char ReplacementCharacter = '\uFFFD';

    // Die feste Tabelle ist Evidence für KOI8-R; ein Host-Codec könnte je nach Installation fehlen.
    // The fixed table is KOI8-R evidence; a host codec may be absent depending on installation.
    private const string Koi8RHighBytes =
        "─│┌┐└┘├┤┬┴┼▀▄█▌▐░▒▓⌠■∙√≈≤≥ ⌡°²·÷═║╒ё╓╔╕╖╗╘╙╚╛╜╝╞╟╠╡Ё╢╣╤╥╦╧╨╩╪╫╬©" +
        "юабцдефгхийклмнопярстужвьызшэщчъЮАБЦДЕФГХИЙКЛМНОПЯРСТУЖВЬЫЗШЭЩЧЪ";

    /// <summary>
    /// Prüft ein einzelnes UTF-16-Zeichen für die zellenbasierte Unicode-Darstellung.
    ///
    /// Validates one UTF-16 character for the cell-based Unicode representation.
    /// </summary>
    /// <param name="value">Zu prüfendes Zeichen. / Character to validate.</param>
    /// <returns>Exaktes Zeichen oder U+FFFD mit Status. / Exact character or U+FFFD with status.</returns>
    public static CharsetMappingResult MapUnicode(char value)
    {
        if (char.IsSurrogate(value))
        {
            return new CharsetMappingResult(
                TerminalCharset.Unicode,
                value,
                ReplacementCharacter,
                CharsetMappingOutcome.Replaced,
                "Isolated UTF-16 surrogate replaced with U+FFFD. / Isoliertes UTF-16-Surrogat durch U+FFFD ersetzt.");
        }

        return new CharsetMappingResult(
            TerminalCharset.Unicode,
            value,
            value,
            CharsetMappingOutcome.Mapped,
            "Unicode character mapped directly. / Unicode-Zeichen direkt abgebildet.");
    }

    /// <summary>
    /// Bildet ein Byte aus dem angegebenen Zeichensatz in eine Unicode-Zelle ab.
    ///
    /// Maps one byte from the specified character set to a Unicode cell.
    /// </summary>
    /// <param name="value">Quellbyte. / Source byte.</param>
    /// <param name="charset">Quellzeichensatz. / Source character set.</param>
    /// <returns>Hostunabhängiges Mappingergebnis. / Host-independent mapping result.</returns>
    public static CharsetMappingResult MapByte(byte value, TerminalCharset charset)
    {
        if (charset == TerminalCharset.Koi8R)
        {
            char glyph = value < 0x80 ? (char)value : Koi8RHighBytes[value - 0x80];
            return new CharsetMappingResult(
                charset,
                value,
                glyph,
                CharsetMappingOutcome.Mapped,
                "KOI8-R byte mapped by the fixed table. / KOI8-R-Byte über die feste Tabelle abgebildet.");
        }

        if (charset == TerminalCharset.Unicode && value < 0x80)
        {
            return new CharsetMappingResult(
                charset,
                value,
                (char)value,
                CharsetMappingOutcome.Mapped,
                "ASCII subset mapped directly. / ASCII-Teilmenge direkt abgebildet.");
        }

        CharsetMappingOutcome outcome = charset == TerminalCharset.Unicode
            ? CharsetMappingOutcome.Replaced
            : CharsetMappingOutcome.Unsupported;
        string reason = charset == TerminalCharset.Unicode
            ? "A standalone high byte is not a Unicode scalar and was replaced. / Ein einzelnes High-Byte ist kein Unicode-Skalar und wurde ersetzt."
            : "Charset is unsupported; no host-codepage fallback is used. / Zeichensatz wird nicht unterstützt; kein Host-Codepage-Fallback.";
        return new CharsetMappingResult(charset, value, ReplacementCharacter, outcome, reason);
    }
}
