// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Ergebnis eines sprachabhaengigen Ressourcen-Lookups.
///
/// Result of a language-aware resource lookup.
/// </summary>
/// <typeparam name="T">Der erwartete Ressourcentyp. / The expected resource type.</typeparam>
public sealed class TLocalizedResourceResult<T> where T : class
{
    internal TLocalizedResourceResult(T? value, string? matchedKey, IReadOnlyList<string> attemptedKeys)
    {
        Value = value;
        MatchedKey = matchedKey;
        AttemptedKeys = attemptedKeys;
    }

    /// <summary>
    /// Gibt an, ob ein Kandidat mit passendem Typ gefunden wurde.
    ///
    /// Indicates whether a candidate of the expected type was found.
    /// </summary>
    public bool Found => Value is not null;

    /// <summary>
    /// Der gefundene Wert oder <c>null</c>.
    ///
    /// The matched value or <c>null</c>.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Der exakt gefundene Schluessel oder <c>null</c>.
    ///
    /// The exact matched key or <c>null</c>.
    /// </summary>
    public string? MatchedKey { get; }

    /// <summary>
    /// Die in Reihenfolge versuchten exakten Schluessel.
    ///
    /// The exact keys attempted in order.
    /// </summary>
    public IReadOnlyList<string> AttemptedKeys { get; }
}

/// <summary>
/// Waehlt sprachabhaengige Ressourcen ueber eine explizite Fallback-Reihenfolge.
///
/// Selects language-specific resources through an explicit fallback order.
/// </summary>
public static class TLocalizedResourceLookup
{
    /// <summary>
    /// Sucht eine Ressource in exakter Sprache, expliziten Fallbacks und der neutralen Variante.
    ///
    /// Finds a resource in the exact language, explicit fallbacks, and the neutral variant.
    /// </summary>
    /// <typeparam name="T">Der erwartete Ressourcentyp. / The expected resource type.</typeparam>
    /// <param name="resources">Die Ressourcendatei. / The resource file.</param>
    /// <param name="baseKey">Der exakte neutrale Basisschluessel. / The exact neutral base key.</param>
    /// <param name="requestedLanguage">Der explizit angeforderte Sprach-Tag. / The explicitly requested language tag.</param>
    /// <param name="fallbackLanguages">Die geordneten Fallback-Tags. / The ordered fallback tags.</param>
    /// <returns>Wert, Match-Key und versuchte Kandidaten. / Value, matched key, and attempted candidates.</returns>
    public static TLocalizedResourceResult<T> Find<T>(
        TResourceFile resources,
        string baseKey,
        string requestedLanguage,
        IEnumerable<string>? fallbackLanguages = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(resources);
        ValidateBaseKey(baseKey);
        ValidateLanguageTag(requestedLanguage, nameof(requestedLanguage));

        // Der Aufrufer bestimmt die Sprachpolitik explizit; Host-Locale und versteckte Parent-Regeln bleiben ausserhalb des Ergebnisses.
        // The caller defines language policy explicitly; host locale and hidden parent rules stay outside the result.
        List<string> candidates = [];
        HashSet<string> seen = new(StringComparer.Ordinal);
        AddLanguageCandidate(baseKey, requestedLanguage, candidates, seen);
        if (fallbackLanguages is not null)
        {
            foreach (string fallback in fallbackLanguages)
            {
                ValidateLanguageTag(fallback, nameof(fallbackLanguages));
                AddLanguageCandidate(baseKey, fallback, candidates, seen);
            }
        }

        if (seen.Add(baseKey))
        {
            candidates.Add(baseKey);
        }

        for (int index = 0; index < candidates.Count; index++)
        {
            string candidate = candidates[index];
            if (resources.Resources.TryGet(candidate, out T? value))
            {
                return new TLocalizedResourceResult<T>(value, candidate, candidates.Take(index + 1).ToArray());
            }
        }

        // Ein leerer gueltiger Wert bleibt ein Treffer; nur ein fehlendes Objekt erzeugt Found=false.
        // An empty valid value remains a match; only a missing object produces Found=false.
        return new TLocalizedResourceResult<T>(null, null, candidates.AsReadOnly());
    }

    private static void AddLanguageCandidate(string baseKey, string language, List<string> candidates, HashSet<string> seen)
    {
        string candidate = $"{baseKey}.{language}";
        if (seen.Add(candidate))
        {
            candidates.Add(candidate);
        }
    }

    private static void ValidateBaseKey(string baseKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(baseKey);
        if (baseKey.Any(char.IsWhiteSpace) || baseKey.StartsWith(".", StringComparison.Ordinal) || baseKey.EndsWith(".", StringComparison.Ordinal))
        {
            throw new ArgumentException("Resource base key must be non-empty, exact, and contain no whitespace or edge dots.", nameof(baseKey));
        }
    }

    private static void ValidateLanguageTag(string language, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(language) || language.StartsWith("-", StringComparison.Ordinal) || language.EndsWith("-", StringComparison.Ordinal))
        {
            throw new ArgumentException("Language tag must contain non-empty letter or digit segments.", parameterName);
        }

        string[] segments = language.Split('-');
        if (segments.Any(segment => segment.Length == 0 || segment.Any(character => !char.IsLetterOrDigit(character))))
        {
            throw new ArgumentException("Language tag must contain non-empty letter or digit segments.", parameterName);
        }
    }
}
