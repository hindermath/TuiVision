// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Einfache Konfigurationsablage als managed Gegenstück zu <c>TConfigFile</c>
/// aus Turbo Vision (<c>configfile.cc</c>).
///
/// Simple configuration store as a managed counterpart to <c>TConfigFile</c>
/// from Turbo Vision (<c>configfile.cc</c>).
/// </summary>
public sealed class TConfigFile
{
    private readonly Dictionary<string, string> _values = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Speichert oder ersetzt einen Konfigurationswert.
    ///
    /// Stores or replaces a configuration value.
    /// </summary>
    /// <param name="key">Der Konfigurationsschlüssel. / The configuration key.</param>
    /// <param name="value">Der Konfigurationswert. / The configuration value.</param>
    public void Set(string key, string value)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);
        _values[key] = value;
    }

    /// <summary>
    /// Liest einen Konfigurationswert oder <c>null</c>, wenn kein Eintrag vorhanden ist.
    ///
    /// Reads a configuration value or <c>null</c> when no entry exists.
    /// </summary>
    /// <param name="key">Der Konfigurationsschlüssel. / The configuration key.</param>
    /// <returns>Der Konfigurationswert oder <c>null</c>. / The configuration value or <c>null</c>.</returns>
    public string? Get(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _values.TryGetValue(key, out string? value) ? value : null;
    }
}
