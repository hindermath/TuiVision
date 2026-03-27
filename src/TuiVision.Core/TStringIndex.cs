// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Core;

/// <summary>
/// Case-sensitive Zeichenkettenindex als managed Gegenstück zu <c>TStringIndex</c>
/// aus Turbo Vision (<c>tstrinde.cc</c>).
///
/// Case-sensitive string index as a managed counterpart to <c>TStringIndex</c>
/// from Turbo Vision (<c>tstrinde.cc</c>).
/// </summary>
public sealed class TStringIndex
{
    private readonly Dictionary<string, string> _items = new(StringComparer.Ordinal);

    /// <summary>
    /// Fügt einen Schlüssel-Wert-Eintrag hinzu oder ersetzt einen vorhandenen.
    ///
    /// Adds a key-value entry or replaces an existing one.
    /// </summary>
    /// <param name="key">Der Schlüssel. / The key.</param>
    /// <param name="value">Der Wert. / The value.</param>
    public void Add(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);
        _items[key] = value;
    }

    /// <summary>
    /// Gibt den Wert zum angegebenen Schlüssel zurück oder <c>null</c>, wenn kein Eintrag vorhanden ist.
    ///
    /// Returns the value for the specified key or <c>null</c> when no entry exists.
    /// </summary>
    /// <param name="key">Der Schlüssel. / The key.</param>
    /// <returns>Der gefundene Wert oder <c>null</c>. / The found value or <c>null</c>.</returns>
    public string? Get(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _items.TryGetValue(key, out string? value) ? value : null;
    }
}
