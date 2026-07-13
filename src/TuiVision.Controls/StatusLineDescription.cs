// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>Unveränderliche StatusLine-Aktion. / Immutable status-line action.</summary>
/// <param name="Label">Die Beschriftung. / The label.</param>
/// <param name="CommandId">Die Command-ID. / The command identifier.</param>
/// <param name="KeyCode">Der Tastencode oder 0. / The key code or zero.</param>
/// <param name="Disabled">Der statische Deaktivierungszustand. / The static disabled state.</param>
public sealed record StatusItemDescription(string Label, ushort CommandId, ushort KeyCode, bool Disabled);

/// <summary>Unveränderliche StatusLine-Kontextdefinition. / Immutable status-line context definition.</summary>
public sealed record StatusDefinitionDescription
{
    /// <summary>Erstellt eine Definition. / Creates a definition.</summary>
    /// <param name="minContext">Der kleinste Kontext. / The minimum context.</param>
    /// <param name="maxContext">Der größte Kontext. / The maximum context.</param>
    /// <param name="order">Die First-Match-Reihenfolge. / The first-match order.</param>
    /// <param name="items">Die Aktionen. / The actions.</param>
    public StatusDefinitionDescription(int minContext, int maxContext, int order, IEnumerable<StatusItemDescription> items)
    {
        MinContext = minContext;
        MaxContext = maxContext;
        Order = order;
        Items = items?.ToArray() ?? [];
    }
    /// <summary>Der kleinste Kontext. / The minimum context.</summary>
    public int MinContext { get; }
    /// <summary>Der größte Kontext. / The maximum context.</summary>
    public int MaxContext { get; }
    /// <summary>Die Reihenfolge. / The order.</summary>
    public int Order { get; }
    /// <summary>Die Aktionen. / The actions.</summary>
    public IReadOnlyList<StatusItemDescription> Items { get; }
}

/// <summary>Versionierte unveränderliche StatusLine-Beschreibung. / Versioned immutable status-line description.</summary>
public sealed record StatusLineDescription
{
    /// <summary>Erstellt eine Beschreibung. / Creates a description.</summary>
    /// <param name="version">Die Modellversion. / The model version.</param>
    /// <param name="definitions">Die Definitionen. / The definitions.</param>
    public StatusLineDescription(int version, IEnumerable<StatusDefinitionDescription> definitions)
    {
        Version = version;
        Definitions = definitions?.ToArray() ?? [];
    }
    /// <summary>Die Modellversion. / The model version.</summary>
    public int Version { get; }
    /// <summary>Die Definitionen. / The definitions.</summary>
    public IReadOnlyList<StatusDefinitionDescription> Definitions { get; }
}
