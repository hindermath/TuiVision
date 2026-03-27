// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Benanntes Farbelement als managed Gegenstück zu <c>TColorItem</c> aus Turbo Vision
/// (<c>tcolorit.cc</c>).
///
/// Named colour item as a managed counterpart to <c>TColorItem</c> from Turbo Vision
/// (<c>tcolorit.cc</c>).
/// </summary>
/// <param name="Name">Der Anzeigename. / The display name.</param>
/// <param name="Color">Die Farbe. / The colour.</param>
public readonly record struct TColorItem(string Name, ConsoleColor Color);
