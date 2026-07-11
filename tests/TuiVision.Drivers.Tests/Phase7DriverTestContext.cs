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

    /// <summary>
    /// Der provisorische Statuswert, der im Phase-8-Gate-Abschluss nicht mehr erlaubt ist.
    /// Jede Zeile mit diesem Status blockiert den Gate-Abschluss (006).
    ///
    /// The provisional status value that is no longer allowed at Phase-8 gate closure.
    /// Any row carrying this status blocks gate closure (006).
    /// </summary>
    public const string ProvisionalStatus = "portiert + Test ausstehend";

    /// <summary>
    /// Liest alle Ledger-Zeilen aus <c>docs/porting-status.md</c> und gibt sie als strukturierte
    /// Einträge zurück. Jede Zeile beginnt mit dem Repository-relativen Pfad.
    ///
    /// Reads all ledger rows from <c>docs/porting-status.md</c> and returns them as structured
    /// entries. Every row begins with the repository-relative path.
    /// </summary>
    /// <returns>
    /// Geordnete Liste von Ledger-Einträgen. / Ordered list of ledger entries.
    /// </returns>
    public static IReadOnlyList<LedgerRow> ParseLedgerRows()
    {
        string content = ReadPortingStatus();
        var rows = new List<LedgerRow>();

        foreach (string line in content.Split('\n'))
        {
            string trimmed = line.TrimStart();
            if (!trimmed.StartsWith("| tv203s/", StringComparison.Ordinal))
                continue;

            string[] cells = trimmed.Split('|');
            if (cells.Length < 8)
                continue;

            rows.Add(new LedgerRow(
                SourceFile: cells[1].Trim(),
                CapabilityBucket: cells[2].Trim(),
                PrimaryTarget: cells[3].Trim(),
                SecondaryTargets: cells[4].Trim(),
                Status: cells[5].Trim(),
                Evidence: cells[6].Trim(),
                Rationale: cells[7].Trim()
            ));
        }

        return rows.AsReadOnly();
    }

    /// <summary>
    /// Prüft, ob ein Primärziel als Platzhalter gilt (enthält "(geplant)" und ist kein Treiberziel).
    ///
    /// Checks whether a primary target is considered a placeholder
    /// (contains "(geplant)" and is not a driver target).
    /// </summary>
    /// <param name="primaryTarget">Das zu prüfende Primärziel. / The primary target to check.</param>
    /// <returns>
    /// <c>true</c>, wenn das Ziel ein nicht-Treiber Platzhalter ist; andernfalls <c>false</c>.
    /// <c>true</c> if the target is a non-driver placeholder; otherwise <c>false</c>.
    /// </returns>
    public static bool IsNonDriverPlannedTarget(string primaryTarget)
    {
        if (!primaryTarget.Contains("(geplant)", StringComparison.Ordinal))
            return false;
        // Ein konkreter Treiberpfad ist prüfbar; andere geplante Ziele bleiben ohne separate Begründung nur Platzhalter.
        // A concrete driver path is reviewable; other planned targets remain placeholders without separate rationale.
        return !primaryTarget.Contains("TuiVision.Drivers", StringComparison.Ordinal);
    }

    /// <summary>
    /// Prüft, ob ein Nachweis-Verweis vorhanden und nicht leer ist.
    ///
    /// Checks whether an evidence reference is present and non-empty.
    /// </summary>
    /// <param name="evidence">Der zu prüfende Nachweis-Verweis. / The evidence reference to check.</param>
    /// <returns>
    /// <c>true</c>, wenn ein nicht-leerer Nachweis vorhanden ist; andernfalls <c>false</c>.
    /// <c>true</c> if a non-empty evidence reference is present; otherwise <c>false</c>.
    /// </returns>
    public static bool HasEvidence(string evidence)
        => !string.IsNullOrWhiteSpace(evidence) && evidence != "–";

    /// <summary>
    /// Prüft, ob eine Ledger-Zeile den finalen Beweisstatus hat.
    ///
    /// Checks whether a ledger row carries a final proof status.
    /// </summary>
    /// <param name="status">Der zu prüfende Statuswert. / The status value to check.</param>
    /// <returns>
    /// <c>true</c>, wenn der Status ein finaler Zustand ist; andernfalls <c>false</c>.
    /// <c>true</c> if the status is a final state; otherwise <c>false</c>.
    /// </returns>
    public static bool IsFinalProofStatus(string status)
        => status is "portiert + getestet" or "bewusst ausgelassen + Begruendung";
}

/// <summary>
/// Strukturierter Ledger-Eintrag aus <c>docs/porting-status.md</c>.
///
/// Structured ledger entry from <c>docs/porting-status.md</c>.
/// </summary>
/// <param name="SourceFile">Repository-relativer Pfad zur historischen .cc-Datei. / Repository-relative path to the historical .cc file.</param>
/// <param name="CapabilityBucket">Fähigkeitsgruppe / Capability bucket.</param>
/// <param name="PrimaryTarget">Primäres verwaltetes Ziel. / Primary managed target.</param>
/// <param name="SecondaryTargets">Optionale sekundäre Ziele. / Optional secondary targets.</param>
/// <param name="Status">Statuswert der Zeile. / Row status value.</param>
/// <param name="Evidence">Nachweis-Verweis. / Evidence reference.</param>
/// <param name="Rationale">Begründungsnotiz. / Rationale note.</param>
internal sealed record LedgerRow(
    string SourceFile,
    string CapabilityBucket,
    string PrimaryTarget,
    string SecondaryTargets,
    string Status,
    string Evidence,
    string Rationale);
