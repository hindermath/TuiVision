// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Collections.ObjectModel;
using System.Text;

namespace TuiVision.Serialization;

/// <summary>
/// Konfiguriert die Sicherheitsgrenzen des Help-Source-Compilers.
///
/// Configures the safety limits of the help source compiler.
/// </summary>
public sealed class THelpCompilerOptions
{
    /// <summary>
    /// Die maximale Anzahl dekodierter Quellzeichen.
    ///
    /// The maximum number of decoded source characters.
    /// </summary>
    public int MaxSourceCharacters { get; init; } = 1_048_576;

    /// <summary>
    /// Die maximale Anzahl Zeichen pro Quellzeile.
    ///
    /// The maximum number of characters per source line.
    /// </summary>
    public int MaxLineCharacters { get; init; } = 16_384;

    /// <summary>
    /// Die maximale Anzahl Themen-Deklarationen.
    ///
    /// The maximum number of topic declarations.
    /// </summary>
    public int MaxTopics { get; init; } = 10_000;

    internal void Validate()
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(MaxSourceCharacters);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(MaxLineCharacters);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(MaxTopics);
    }
}

/// <summary>
/// Schweregrad einer Help-Compiler-Diagnose.
///
/// Severity of a help compiler diagnostic.
/// </summary>
public enum THelpCompilerDiagnosticSeverity
{
    /// <summary>
    /// Die Quelle kann nicht als gueltiges Laufzeitmodell veroeffentlicht werden.
    ///
    /// The source cannot be published as a valid runtime model.
    /// </summary>
    Error
}

/// <summary>
/// Beschreibt einen positionsbezogenen Help-Compiler-Fehler.
///
/// Describes a source-positioned help compiler error.
/// </summary>
/// <param name="Severity">Der Schweregrad. / The severity.</param>
/// <param name="Code">Der stabile Diagnosecode. / The stable diagnostic code.</param>
/// <param name="Message">Die Diagnosemeldung. / The diagnostic message.</param>
/// <param name="SourceName">Der logische Quellname. / The logical source name.</param>
/// <param name="Line">Die einsbasierte Zeile. / The one-based line.</param>
/// <param name="Column">Die einsbasierte Spalte. / The one-based column.</param>
public readonly record struct THelpCompilerDiagnostic(
    THelpCompilerDiagnosticSeverity Severity,
    string Code,
    string Message,
    string SourceName,
    int Line,
    int Column);

/// <summary>
/// Atomisches Ergebnis einer Help-Source-Uebersetzung.
///
/// Atomic result of help source compilation.
/// </summary>
public sealed class THelpCompilationResult
{
    internal THelpCompilationResult(
        THelpFile? helpFile,
        IReadOnlyDictionary<string, int> symbols,
        IReadOnlyList<THelpCompilerDiagnostic> diagnostics)
    {
        HelpFile = helpFile;
        Symbols = symbols;
        Diagnostics = diagnostics;
    }

    /// <summary>
    /// Gibt an, ob ein vollstaendiges Laufzeitmodell vorliegt.
    ///
    /// Indicates whether a complete runtime model is available.
    /// </summary>
    public bool Success => HelpFile is not null;

    /// <summary>
    /// Das vollstaendige Hilfemodell oder <c>null</c> bei Fehlern.
    ///
    /// The complete help model or <c>null</c> when errors occurred.
    /// </summary>
    public THelpFile? HelpFile { get; }

    /// <summary>
    /// Die vollstaendige Symboltabelle oder eine leere Tabelle bei Fehlern.
    ///
    /// The complete symbol table or an empty table when errors occurred.
    /// </summary>
    public IReadOnlyDictionary<string, int> Symbols { get; }

    /// <summary>
    /// Die Diagnosen in stabiler Quellreihenfolge.
    ///
    /// The diagnostics in stable source order.
    /// </summary>
    public IReadOnlyList<THelpCompilerDiagnostic> Diagnostics { get; }
}

/// <summary>
/// Uebersetzt eine begrenzte historische Help-Source-Syntax in das verwaltete Laufzeitmodell.
///
/// Compiles a bounded historical help source syntax into the managed runtime model.
/// </summary>
public sealed class THelpSourceCompiler
{
    private const string DefaultMemorySourceName = "<memory>";
    private const string DefaultStreamSourceName = "<stream>";
    private readonly THelpCompilerOptions _options;

    /// <summary>
    /// Initialisiert einen Compiler mit Standardgrenzen.
    ///
    /// Initializes a compiler with default limits.
    /// </summary>
    public THelpSourceCompiler() : this(new THelpCompilerOptions())
    {
    }

    /// <summary>
    /// Initialisiert einen Compiler mit expliziten positiven Grenzen.
    ///
    /// Initializes a compiler with explicit positive limits.
    /// </summary>
    /// <param name="options">Die Compilergrenzen. / The compiler limits.</param>
    public THelpSourceCompiler(THelpCompilerOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _options.Validate();
    }

    /// <summary>
    /// Uebersetzt bereits dekodierten Help-Source-Text.
    ///
    /// Compiles already decoded help source text.
    /// </summary>
    /// <param name="source">Der Quelltext. / The source text.</param>
    /// <param name="sourceName">Der logische Quellname fuer Diagnosen. / The logical source name for diagnostics.</param>
    /// <returns>Das atomische Compilerergebnis. / The atomic compilation result.</returns>
    public THelpCompilationResult Compile(string source, string sourceName = DefaultMemorySourceName)
    {
        ArgumentNullException.ThrowIfNull(source);
        sourceName = NormaliseSourceName(sourceName, DefaultMemorySourceName);
        if (source.Length > _options.MaxSourceCharacters)
        {
            return Failure(Diagnostic("THC009", "Quelltext ueberschreitet die konfigurierte Groessengrenze. / Source exceeds the configured size limit.", sourceName, 1, 1));
        }

        return CompileDecoded(source, sourceName);
    }

    /// <summary>
    /// Dekodiert einen Stream strikt als UTF-8 und uebersetzt ihn.
    ///
    /// Decodes a stream as strict UTF-8 and compiles it.
    /// </summary>
    /// <param name="source">Der lesbare Quellstream. / The readable source stream.</param>
    /// <param name="sourceName">Der logische Quellname fuer Diagnosen. / The logical source name for diagnostics.</param>
    /// <returns>Das atomische Compilerergebnis. / The atomic compilation result.</returns>
    public THelpCompilationResult Compile(Stream source, string sourceName = DefaultStreamSourceName)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (!source.CanRead)
        {
            throw new ArgumentException("Source stream must be readable.", nameof(source));
        }

        sourceName = NormaliseSourceName(sourceName, DefaultStreamSourceName);
        try
        {
            using StreamReader reader = new(
                source,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true),
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 4096,
                leaveOpen: true);
            int bufferLength = _options.MaxSourceCharacters >= 4096 ? 4096 : _options.MaxSourceCharacters + 1;
            char[] buffer = new char[bufferLength];
            StringBuilder decoded = new(Math.Min(_options.MaxSourceCharacters, 4096));
            while (true)
            {
                int read = reader.Read(buffer, 0, buffer.Length);
                if (read == 0)
                {
                    break;
                }

                if (decoded.Length + read > _options.MaxSourceCharacters)
                {
                    return Failure(Diagnostic("THC009", "Quelltext ueberschreitet die konfigurierte Groessengrenze. / Source exceeds the configured size limit.", sourceName, 1, 1));
                }

                decoded.Append(buffer, 0, read);
            }

            string decodedSource = decoded.ToString();
            if (decodedSource.StartsWith('\uFEFF'))
            {
                decodedSource = decodedSource[1..];
            }

            return CompileDecoded(decodedSource, sourceName);
        }
        catch (DecoderFallbackException)
        {
            return Failure(Diagnostic("THC008", "Quelle ist kein gueltiges UTF-8. / Source is not valid UTF-8.", sourceName, 1, 1));
        }
    }

    private THelpCompilationResult CompileDecoded(string source, string sourceName)
    {
        string[] lines = source.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n').Split('\n');
        List<THelpCompilerDiagnostic> diagnostics = [];
        List<TopicBuilder> topics = [];
        Dictionary<string, int> symbols = new(StringComparer.Ordinal);
        HashSet<int> contexts = [];
        TopicBuilder? currentTopic = null;
        int nextContext = 1;

        for (int index = 0; index < lines.Length; index++)
        {
            string line = lines[index];
            int lineNumber = index + 1;
            if (line.Length > _options.MaxLineCharacters)
            {
                diagnostics.Add(Diagnostic("THC009", "Quellzeile ueberschreitet die konfigurierte Laengengrenze. / Source line exceeds the configured length limit.", sourceName, lineNumber, 1));
                continue;
            }

            if (line.StartsWith(".topic", StringComparison.Ordinal))
            {
                if (topics.Count >= _options.MaxTopics)
                {
                    diagnostics.Add(Diagnostic("THC009", "Zu viele Hilfethemen. / Too many help topics.", sourceName, lineNumber, 1));
                    continue;
                }

                currentTopic = ParseTopic(line, sourceName, lineNumber, symbols, contexts, diagnostics, ref nextContext);
                if (currentTopic is not null)
                {
                    topics.Add(currentTopic);
                }

                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith(".", StringComparison.Ordinal))
            {
                diagnostics.Add(Diagnostic("THC002", "Unbekannte Help-Source-Direktive. / Unknown help source directive.", sourceName, lineNumber, 1));
                continue;
            }

            if (currentTopic is null)
            {
                diagnostics.Add(Diagnostic("THC002", "Text steht vor der ersten Topic-Deklaration. / Text appears before the first topic declaration.", sourceName, lineNumber, 1));
                continue;
            }

            ParseParagraph(line, sourceName, lineNumber, currentTopic, diagnostics);
        }

        if (topics.Count == 0 && diagnostics.Count == 0)
        {
            diagnostics.Add(Diagnostic("THC001", "Die Quelle enthaelt kein Hilfethema. / The source contains no help topic.", sourceName, 1, 1));
        }

        foreach (TopicBuilder topic in topics)
        {
            foreach (PendingReference reference in topic.References)
            {
                if (!symbols.ContainsKey(reference.TargetSymbol))
                {
                    diagnostics.Add(Diagnostic("THC007", $"Unbekanntes Referenzziel '{reference.TargetSymbol}'. / Unknown reference target '{reference.TargetSymbol}'.", sourceName, reference.Line, reference.Column));
                }
            }
        }

        if (diagnostics.Count > 0)
        {
            return Failure(diagnostics);
        }

        // Erst nach vollstaendiger Validierung entsteht das veroeffentlichte Modell; Fehler brauchen daher keinen Rollback.
        // The published model is created only after full validation, so failures require no rollback.
        THelpFile helpFile = BuildHelpFile(topics, symbols);
        return new THelpCompilationResult(
            helpFile,
            new ReadOnlyDictionary<string, int>(new Dictionary<string, int>(symbols, StringComparer.Ordinal)),
            Array.Empty<THelpCompilerDiagnostic>());
    }

    private static TopicBuilder? ParseTopic(
        string line,
        string sourceName,
        int lineNumber,
        Dictionary<string, int> symbols,
        HashSet<int> contexts,
        List<THelpCompilerDiagnostic> diagnostics,
        ref int nextContext)
    {
        if (line.Length == 6 || !char.IsWhiteSpace(line[6]))
        {
            diagnostics.Add(Diagnostic("THC002", "Ungueltige Topic-Deklaration. / Invalid topic declaration.", sourceName, lineNumber, 1));
            return null;
        }

        string declaration = line[7..].Trim();
        if (declaration.Length == 0)
        {
            diagnostics.Add(Diagnostic("THC002", "Topic-Deklaration ohne Symbol. / Topic declaration has no symbol.", sourceName, lineNumber, 1));
            return null;
        }

        TopicBuilder topic = new();
        foreach (string rawItem in declaration.Split(','))
        {
            string item = rawItem.Trim();
            int equals = item.IndexOf('=');
            string symbol = (equals >= 0 ? item[..equals] : item).Trim();
            if (!IsValidSymbol(symbol))
            {
                diagnostics.Add(Diagnostic("THC002", $"Ungueltiges Topic-Symbol '{symbol}'. / Invalid topic symbol '{symbol}'.", sourceName, lineNumber, Math.Max(1, line.IndexOf(rawItem, StringComparison.Ordinal) + 1)));
                continue;
            }

            int context;
            if (equals >= 0)
            {
                string numberText = item[(equals + 1)..].Trim();
                if (!int.TryParse(numberText, System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out context) || context < 0)
                {
                    diagnostics.Add(Diagnostic("THC003", $"Ungueltiger Kontextwert '{numberText}'. / Invalid context value '{numberText}'.", sourceName, lineNumber, 1));
                    continue;
                }
            }
            else
            {
                context = nextContext;
            }

            if (symbols.ContainsKey(symbol))
            {
                diagnostics.Add(Diagnostic("THC004", $"Doppeltes Topic-Symbol '{symbol}'. / Duplicate topic symbol '{symbol}'.", sourceName, lineNumber, 1));
                continue;
            }

            if (!contexts.Add(context))
            {
                diagnostics.Add(Diagnostic("THC005", $"Doppelter Kontextwert '{context}'. / Duplicate context value '{context}'.", sourceName, lineNumber, 1));
                continue;
            }

            symbols.Add(symbol, context);
            topic.Symbols.Add(new SymbolDefinition(symbol, context));
            nextContext = context == int.MaxValue ? int.MaxValue : context + 1;
        }

        return topic.Symbols.Count > 0 ? topic : null;
    }

    private static void ParseParagraph(
        string line,
        string sourceName,
        int lineNumber,
        TopicBuilder topic,
        List<THelpCompilerDiagnostic> diagnostics)
    {
        StringBuilder visible = new(line.Length);
        int index = 0;
        while (index < line.Length)
        {
            if (line[index] == '}')
            {
                diagnostics.Add(Diagnostic("THC006", "Schliessende Klammer ohne Referenzbeginn. / Closing brace without a reference start.", sourceName, lineNumber, index + 1));
                index++;
                continue;
            }

            if (line[index] != '{')
            {
                visible.Append(line[index++]);
                continue;
            }

            int close = line.IndexOf('}', index + 1);
            if (close < 0)
            {
                diagnostics.Add(Diagnostic("THC006", "Nicht abgeschlossene Querverweis-Klammer. / Unterminated cross-reference brace.", sourceName, lineNumber, index + 1));
                visible.Append(line.AsSpan(index));
                break;
            }

            string expression = line[(index + 1)..close];
            int colon = expression.IndexOf(':');
            string label = (colon >= 0 ? expression[..colon] : expression).Trim();
            string target = (colon >= 0 ? expression[(colon + 1)..] : expression).Trim();
            if (label.Length == 0 || !IsValidSymbol(target))
            {
                diagnostics.Add(Diagnostic("THC006", "Ungueltiger Querverweis. / Invalid cross reference.", sourceName, lineNumber, index + 1));
                index = close + 1;
                continue;
            }

            int offset = visible.Length;
            visible.Append(label);
            topic.References.Add(new PendingReference(target, label, offset, lineNumber, index + 1));
            index = close + 1;
        }

        topic.Paragraphs.Add(visible.ToString());
    }

    private static THelpFile BuildHelpFile(IReadOnlyList<TopicBuilder> topics, IReadOnlyDictionary<string, int> symbols)
    {
        THelpFile helpFile = new();
        foreach (TopicBuilder builder in topics)
        {
            string title = builder.Symbols[0].Name;
            foreach (SymbolDefinition alias in builder.Symbols)
            {
                THelpTopic topic = new(alias.Context, title);
                foreach (string paragraph in builder.Paragraphs)
                {
                    topic.AddParagraph(paragraph);
                }

                foreach (PendingReference reference in builder.References)
                {
                    topic.AddCrossReference(new THelpCrossReference(
                        symbols[reference.TargetSymbol],
                        reference.Label,
                        reference.Offset,
                        reference.Label.Length));
                }

                helpFile.AddTopic(topic);
            }
        }

        return helpFile;
    }

    private static bool IsValidSymbol(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !(char.IsLetter(value[0]) || value[0] == '_'))
        {
            return false;
        }

        return value.All(character => char.IsLetterOrDigit(character) || character == '_');
    }

    private static string NormaliseSourceName(string? sourceName, string fallback)
    {
        return string.IsNullOrWhiteSpace(sourceName) ? fallback : sourceName;
    }

    private static THelpCompilerDiagnostic Diagnostic(string code, string message, string sourceName, int line, int column)
    {
        return new THelpCompilerDiagnostic(THelpCompilerDiagnosticSeverity.Error, code, message, sourceName, line, column);
    }

    private static THelpCompilationResult Failure(THelpCompilerDiagnostic diagnostic)
    {
        return Failure([diagnostic]);
    }

    private static THelpCompilationResult Failure(IEnumerable<THelpCompilerDiagnostic> diagnostics)
    {
        return new THelpCompilationResult(
            helpFile: null,
            new ReadOnlyDictionary<string, int>(new Dictionary<string, int>(StringComparer.Ordinal)),
            diagnostics.ToArray());
    }

    private sealed class TopicBuilder
    {
        public List<SymbolDefinition> Symbols { get; } = [];

        public List<string> Paragraphs { get; } = [];

        public List<PendingReference> References { get; } = [];
    }

    private readonly record struct SymbolDefinition(string Name, int Context);

    private readonly record struct PendingReference(
        string TargetSymbol,
        string Label,
        int Offset,
        int Line,
        int Column);
}
