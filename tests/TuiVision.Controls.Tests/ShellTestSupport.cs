// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Gemeinsame Test-Hilfsmittel für die Validierung des Application Frameworks.
/// Diese Klasse bietet Hilfsmethoden zum Erstellen von Test-Szenarien.
///
/// Shared test support helpers for validating the Application Framework.
/// This class provides helper methods for creating test scenarios.
/// </summary>
public static class ShellTestSupport
{
    /// <summary>
    /// Erstellt ein Standard-Sichtbereichsrechteck für Shell-Tests (80x25).
    ///
    /// Creates a standard viewport rectangle for shell tests (80x25).
    /// </summary>
    /// <returns>Ein <see cref="TRect"/> mit den Standardmaßen 80x25. / A <see cref="TRect"/> with default 80x25 dimensions.</returns>
    public static TRect CreateStandardBounds() => new(0, 0, 80, 25);
}
