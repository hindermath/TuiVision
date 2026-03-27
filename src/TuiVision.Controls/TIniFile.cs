// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfache INI-artige Sektion- und Schlüsselablage als managed Gegenstück zu
/// <c>TProgIni</c> aus Turbo Vision (<c>tprogini.cc</c>).
///
/// Simple INI-style section and key store as a managed counterpart to
/// <c>TProgIni</c> from Turbo Vision (<c>tprogini.cc</c>).
/// </summary>
public sealed class TIniFile
{
    private readonly Dictionary<string, Dictionary<string, string>> _sections =
        new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Speichert oder ersetzt einen Wert in der angegebenen Sektion.
    ///
    /// Stores or replaces a value in the specified section.
    /// </summary>
    /// <param name="section">Die Sektion. / The section.</param>
    /// <param name="key">Der Schlüssel. / The key.</param>
    /// <param name="value">Der Wert. / The value.</param>
    public void Set(string section, string key, string value)
    {
        ArgumentNullException.ThrowIfNull(section);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);

        if (!_sections.TryGetValue(section, out Dictionary<string, string>? sectionValues))
        {
            sectionValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _sections[section] = sectionValues;
        }

        sectionValues[key] = value;
    }

    /// <summary>
    /// Liest einen Wert aus der angegebenen Sektion oder <c>null</c>, wenn kein Eintrag vorhanden ist.
    ///
    /// Reads a value from the specified section or <c>null</c> when no entry exists.
    /// </summary>
    /// <param name="section">Die Sektion. / The section.</param>
    /// <param name="key">Der Schlüssel. / The key.</param>
    /// <returns>Der gespeicherte Wert oder <c>null</c>. / The stored value or <c>null</c>.</returns>
    public string? Get(string section, string key)
    {
        ArgumentNullException.ThrowIfNull(section);
        ArgumentNullException.ThrowIfNull(key);

        if (!_sections.TryGetValue(section, out Dictionary<string, string>? sectionValues))
        {
            return null;
        }

        return sectionValues.TryGetValue(key, out string? value) ? value : null;
    }
}
