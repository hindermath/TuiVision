// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Examples.Tutorial.Steps;

/// <summary>
/// Zentrales Verzeichnis aller 16 Tutorial-Schritte.
/// Stellt sowohl einen nach Token indizierten Dictionary als auch eine sortierte Liste bereit.
///
/// Central catalog of all 16 tutorial steps.
/// Provides both a token-indexed dictionary and an ordered list.
/// </summary>
public static class TutorialStepCatalog
{
    // Alle 16 Schritte in Sequenzreihenfolge / All 16 steps in sequence order
    private static readonly ITutorialStep[] _steps =
    [
        new TvGuid01Step(),
        new TvGuid02Step(),
        new TvGuid03Step(),
        new TvGuid04Step(),
        new TvGuid05Step(),
        new TvGuid06Step(),
        new TvGuid07Step(),
        new TvGuid08Step(),
        new TvGuid09Step(),
        new TvGuid10Step(),
        new TvGuid11Step(),
        new TvGuid12Step(),
        new TvGuid13Step(),
        new TvGuid14Step(),
        new TvGuid15Step(),
        new TvGuid16Step()
    ];

    /// <summary>
    /// Alle 16 Tutorial-Schritte, nach Token indiziert (Schlüssel: „tvguid01" bis „tvguid16").
    ///
    /// All 16 tutorial steps indexed by token (keys: "tvguid01" through "tvguid16").
    /// </summary>
    public static IReadOnlyDictionary<string, ITutorialStep> All { get; } =
        _steps.ToDictionary(s => s.Token, s => s, StringComparer.Ordinal);

    /// <summary>
    /// Alle 16 Tutorial-Schritte in aufsteigender Sequenzreihenfolge.
    ///
    /// All 16 tutorial steps in ascending sequence order.
    /// </summary>
    public static IReadOnlyList<ITutorialStep> InOrder { get; } =
        [.. _steps.OrderBy(s => s.SequenceNumber)];
}
