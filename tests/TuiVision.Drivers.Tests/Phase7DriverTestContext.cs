// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Gemeinsame Hilfsmittel für Phase-7-Treibertests und Nachweis-Ledger-Validierung.
/// Stellt Pfadauflösung und Inventarauflistung für das historische Quellverzeichnis bereit.
///
/// Shared helpers for Phase-7 driver tests and proof-ledger validation.
/// Provides path resolution and inventory enumeration for the historical source tree.
/// </summary>
internal static class Phase7DriverTestContext
{
    /// <summary>
    /// Ermittelt das Repository-Wurzelverzeichnis, indem von der Test-Assembly ausgehend
    /// nach oben navigiert wird, bis das Verzeichnis <c>tv203s/</c> gefunden wird.
    ///
    /// Finds the repository root by walking upward from the test assembly until
    /// the directory containing <c>tv203s/</c> is found.
    /// </summary>
    /// <returns>Absoluter Pfad zum Repository-Wurzelverzeichnis. / Absolute path to the repository root.</returns>
    /// <exception cref="InvalidOperationException">
    /// Wird ausgelöst, wenn kein Repository-Wurzelverzeichnis gefunden werden kann.
    /// Thrown when no repository root can be located.
    /// </exception>
    public static string FindRepoRoot()
    {
        string? dir = AppDomain.CurrentDomain.BaseDirectory;
        while (dir is not null)
        {
            if (Directory.Exists(Path.Combine(dir, "tv203s")))
                return dir;
            dir = Path.GetDirectoryName(dir);
        }

        throw new InvalidOperationException(
            "Cannot locate repository root. Expected a parent directory containing 'tv203s/'.");
    }

    /// <summary>
    /// Gibt den absoluten Pfad zur Beweisledger-Datei <c>docs/porting-status.md</c> zurück.
    ///
    /// Returns the absolute path to the proof ledger file <c>docs/porting-status.md</c>.
    /// </summary>
    public static string GetPortingStatusPath()
        => Path.Combine(FindRepoRoot(), "docs", "porting-status.md");

    /// <summary>
    /// Listet alle historischen <c>.cc</c>-Implementierungsdateien unter
    /// <c>tv203s/contrib/tvision/classes</c> auf und gibt sie als repository-relative
    /// Pfade mit Schrägstrich als Trennzeichen zurück.
    ///
    /// Enumerates all historical <c>.cc</c> implementation files under
    /// <c>tv203s/contrib/tvision/classes</c> and returns them as repository-relative
    /// forward-slash paths.
    /// </summary>
    /// <returns>Geordnete Menge relativer Pfade. / Ordered set of relative paths.</returns>
    public static IReadOnlyList<string> GetHistoricalCcFiles()
    {
        string repoRoot = FindRepoRoot();
        string classesDir = Path.Combine(repoRoot, "tv203s", "contrib", "tvision", "classes");

        return Directory
            .GetFiles(classesDir, "*.cc", SearchOption.AllDirectories)
            .Select(f => Path.GetRelativePath(repoRoot, f).Replace('\\', '/'))
            .OrderBy(f => f, StringComparer.Ordinal)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>
    /// Liest den Inhalt der Beweisledger-Datei <c>docs/porting-status.md</c>.
    /// Wirft eine <see cref="AssertFailedException"/>, wenn die Datei nicht vorhanden ist.
    ///
    /// Reads the content of the proof ledger <c>docs/porting-status.md</c>.
    /// Throws an <see cref="AssertFailedException"/> when the file is absent.
    /// </summary>
    public static string ReadPortingStatus()
    {
        string path = GetPortingStatusPath();
        Assert.IsTrue(File.Exists(path),
            $"docs/porting-status.md must exist at: {path}");
        return File.ReadAllText(path, System.Text.Encoding.UTF8);
    }

    /// <summary>
    /// Erlaubte Status-Werte gemäß dem Phasenbeweis-Kontrakt.
    ///
    /// Allowed status values according to the phase-proof contract.
    /// </summary>
    public static readonly IReadOnlySet<string> AllowedStatuses = new HashSet<string>(StringComparer.Ordinal)
    {
        "portiert + getestet",
        "portiert + Test ausstehend",
        "bewusst ausgelassen + Begruendung",
    };
}
