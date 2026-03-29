// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Ordnet einem inklusiven Hilfekontext-Bereich einen Satz von Statuszeilen-Aktionen zu.
/// Mehrere Definitionen werden als verkettete Liste organisiert; die erste passende
/// Definition gewinnt (First-Match-Wins-Reihenfolge).
/// Wenn keine Definition passt, wird ein neutraler Leer-Zustand angezeigt.
///
/// Maps one inclusive help-context range to one set of status-line actions.
/// Multiple definitions are organised as a linked list; the first matching
/// definition wins (first-match-wins ordering).
/// When no definition matches, a neutral empty state is shown.
/// </summary>
public class TStatusDef
{
    /// <summary>
    /// Initialisiert eine neue Statuszeilen-Definition.
    ///
    /// Initializes a new status-line definition.
    /// </summary>
    /// <param name="minCtx">
    /// Untere Grenze des inklusiven Hilfekontext-Bereichs.
    /// Lower bound of the inclusive help-context range.
    /// </param>
    /// <param name="maxCtx">
    /// Obere Grenze des inklusiven Hilfekontext-Bereichs.
    /// Upper bound of the inclusive help-context range.
    /// </param>
    /// <param name="items">
    /// Das erste Element der Statuszeilen-Aktionsliste für diesen Kontext.
    /// The first item in the status-action list for this context.
    /// </param>
    /// <param name="next">
    /// Die nächste Definition in der Kette (niedrigere Priorität).
    /// The next definition in the chain (lower priority).
    /// </param>
    public TStatusDef(int minCtx, int maxCtx, TStatusItem? items, TStatusDef? next = null)
    {
        MinCtx = minCtx;
        MaxCtx = maxCtx;
        Items = items;
        Next = next;
    }

    /// <summary>
    /// Untere inklusive Grenze des Hilfekontext-Bereichs.
    ///
    /// Lower inclusive bound of the help-context range.
    /// </summary>
    public int MinCtx { get; }

    /// <summary>
    /// Obere inklusive Grenze des Hilfekontext-Bereichs.
    ///
    /// Upper inclusive bound of the help-context range.
    /// </summary>
    public int MaxCtx { get; }

    /// <summary>
    /// Der erste Statuszeilen-Eintrag für diesen Hilfekontext-Bereich.
    ///
    /// The first status-line item for this help-context range.
    /// </summary>
    public TStatusItem? Items { get; }

    /// <summary>
    /// Die nächste Definition in der Kette (niedrigere Priorität).
    ///
    /// The next definition in the chain (lower priority).
    /// </summary>
    public TStatusDef? Next { get; }

    /// <summary>
    /// Gibt <c>true</c> zurück, wenn <paramref name="helpContext"/> innerhalb dieses
    /// inklusiven Bereichs liegt.
    ///
    /// Returns <c>true</c> when <paramref name="helpContext"/> falls within this
    /// inclusive range.
    /// </summary>
    /// <param name="helpContext">Der zu prüfende Hilfekontext-Wert. / The help-context value to check.</param>
    /// <returns>
    /// <c>true</c>, wenn der Wert im Bereich liegt; andernfalls <c>false</c>.
    /// <c>true</c> if the value is within the range; otherwise <c>false</c>.
    /// </returns>
    public bool Matches(int helpContext) => helpContext >= MinCtx && helpContext <= MaxCtx;
}
