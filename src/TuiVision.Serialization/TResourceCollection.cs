// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Fallunterscheidbarer Ressourcenkatalog mit exakter Gross-/Kleinschreibung.
///
/// Case-sensitive resource catalog with exact key matching.
/// </summary>
public sealed class TResourceCollection
{
    private readonly Dictionary<string, object> _entries = new(StringComparer.Ordinal);

    /// <summary>
    /// Die Anzahl gespeicherter Ressourcen.
    ///
    /// The number of stored resources.
    /// </summary>
    public int Count => _entries.Count;

    /// <summary>
    /// Die vorhandenen Schluessel.
    ///
    /// The available keys.
    /// </summary>
    public IReadOnlyCollection<string> Keys => _entries.Keys;

    /// <summary>
    /// Speichert oder ersetzt eine Ressource.
    ///
    /// Stores or replaces a resource.
    /// </summary>
    /// <param name="key">Der Ressourcenschluessel. / The resource key.</param>
    /// <param name="value">Der Ressourcenwert. / The resource value.</param>
    public void Set(string key, object value)
    {
        _entries[key] = value;
    }

    /// <summary>
    /// Versucht eine Ressource typisiert zu lesen.
    ///
    /// Tries to read a resource as a typed value.
    /// </summary>
    /// <typeparam name="T">Der Zieltyp. / The target type.</typeparam>
    /// <param name="key">Der Ressourcenschluessel. / The resource key.</param>
    /// <param name="value">Der Ressourcenwert. / The resource value.</param>
    /// <returns><c>true</c>, wenn die Ressource existiert und passt. / <c>true</c> if the resource exists and matches.</returns>
    public bool TryGet<T>(string key, out T? value) where T : class
    {
        if (_entries.TryGetValue(key, out object? raw) && raw is T typed)
        {
            value = typed;
            return true;
        }

        value = null;
        return false;
    }

    /// <summary>
    /// Entfernt eine Ressource.
    ///
    /// Removes a resource.
    /// </summary>
    /// <param name="key">Der Ressourcenschluessel. / The resource key.</param>
    /// <returns><c>true</c>, wenn entfernt wurde. / <c>true</c> if removal succeeded.</returns>
    public bool Remove(string key) => _entries.Remove(key);

    internal IEnumerable<KeyValuePair<string, object>> Enumerate() => _entries;
}
