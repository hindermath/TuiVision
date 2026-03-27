// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Vollständigkeitstests für <c>docs/porting-status.md</c>: Prüft, dass jede historische
/// <c>.cc</c>-Datei aus <c>tv203s/contrib/tvision/classes</c> genau einmal im Ledger erscheint
/// und keine undokumentierten Lücken verbleiben.
///
/// Completeness tests for <c>docs/porting-status.md</c>: verifies that every historical
/// <c>.cc</c> file from <c>tv203s/contrib/tvision/classes</c> appears exactly once in the ledger
/// and no undocumented gaps remain.
/// </summary>
[TestClass]
public sealed class PortingStatusCompletenessTests
{
    /// <summary>
    /// Prüft, dass jede historische <c>.cc</c>-Datei mindestens einmal im Ledger erwähnt wird.
    ///
    /// Verifies that every historical <c>.cc</c> file is mentioned at least once in the ledger.
    /// </summary>
    [TestMethod]
    public void LedgerFile_CoversAllHistoricalCcFiles()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        var historicalFiles = Phase7DriverTestContext.GetHistoricalCcFiles();

        var missing = new List<string>();
        foreach (string file in historicalFiles)
        {
            // Normalize: the ledger uses forward slashes and tv203s/ prefix
            string normalized = file.Replace('\\', '/');
            if (!content.Contains(normalized, StringComparison.Ordinal))
            {
                missing.Add(normalized);
            }
        }

        Assert.IsEmpty(missing,
            $"Folgende historischen .cc-Dateien fehlen im Ledger ({missing.Count}):\n" +
            $"The following historical .cc files are missing from the ledger ({missing.Count}):\n" +
            string.Join("\n", missing.Take(20)) +
            (missing.Count > 20 ? $"\n...und {missing.Count - 20} weitere / ...and {missing.Count - 20} more" : string.Empty));
    }

    /// <summary>
    /// Prüft, dass keine historische <c>.cc</c>-Datei mehr als einmal im Ledger erscheint.
    ///
    /// Verifies that no historical <c>.cc</c> file appears more than once in the ledger.
    /// </summary>
    [TestMethod]
    public void LedgerFile_NoCcFileAppearsMoreThanOnce()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        var historicalFiles = Phase7DriverTestContext.GetHistoricalCcFiles();

        var duplicates = new List<string>();
        foreach (string file in historicalFiles)
        {
            string normalized = file.Replace('\\', '/');
            int count = CountOccurrences(content, normalized);
            if (count > 1)
            {
                duplicates.Add($"{normalized} ({count}x)");
            }
        }

        Assert.IsEmpty(duplicates,
            $"Folgende .cc-Dateien erscheinen mehrfach im Ledger:\n" +
            $"The following .cc files appear more than once in the ledger:\n" +
            string.Join("\n", duplicates));
    }

    /// <summary>
    /// Prüft, dass der Ledger keine Zeilen mit undokumentierten Platzhalter-Statuswerten enthält
    /// (kein TODO, offen, später, oder leerer Status).
    ///
    /// Verifies that the ledger contains no rows with undocumented placeholder status values
    /// (no TODO, offen, später, or empty status).
    /// </summary>
    [TestMethod]
    public void LedgerFile_NoPlaceholderStatusValues()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        string[] forbidden = ["TODO", "offen", "später", "tbd", "TBD", "pending-doc"];

        var violations = new List<string>();
        foreach (string line in content.Split('\n'))
        {
            string trimmed = line.TrimStart();
            if (!trimmed.StartsWith("| tv203s/", StringComparison.Ordinal))
                continue;

            foreach (string placeholder in forbidden)
            {
                if (trimmed.Contains(placeholder, StringComparison.Ordinal))
                {
                    violations.Add($"Platzhalter '{placeholder}' in: {trimmed.Substring(0, Math.Min(120, trimmed.Length))}");
                }
            }
        }

        Assert.IsEmpty(violations,
            $"Platzhalter-Statuswerte gefunden / Placeholder status values found:\n{string.Join("\n", violations)}");
    }

    /// <summary>
    /// Prüft, dass der Support-Datei-Verweis für DOS-spezifische Ancillary-Dateien im Ledger
    /// mindestens einmal erwähnt wird (vgastate.h/vgaregs.h als bekannte Abhängigkeiten).
    ///
    /// Verifies that the support-file reference for DOS-specific ancillary files is mentioned
    /// at least once in the ledger (vgastate.h/vgaregs.h as known dependencies).
    /// </summary>
    [TestMethod]
    public void LedgerFile_MentionsKnownAncillarySupportFiles()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();

        string[] knownAncillary = ["vgastate.h", "vgaregs.h"];
        foreach (string ancillary in knownAncillary)
        {
            Assert.IsTrue(
                content.Contains(ancillary, StringComparison.Ordinal),
                $"Ledger muss die bekannte Hilfsdatei '{ancillary}' erwähnen. " +
                $"Ledger must mention known ancillary file '{ancillary}'.");
        }
    }

    /// <summary>
    /// Bewachungsregel (Phase-8-Gate, 006): Keine nicht-Treiber-Zeile mit einem <c>(geplant)</c>-Primärziel
    /// darf ohne eine dokumentierte Begründungsnotiz verbleiben. Jede solche undokumentierte
    /// Planzeile blockiert den Gate-Abschluss.
    ///
    /// Guardrail (Phase-8 gate, 006): No non-driver row with a <c>(geplant)</c> primary target
    /// may remain without a documented rationale note. Every such undocumented planned row
    /// blocks gate closure.
    /// </summary>
    [TestMethod]
    public void LedgerFile_NonDriverPlannedTargets_HaveRationale_Gate006()
    {
        var rows = Phase7DriverTestContext.ParseLedgerRows();
        var undocumented = new List<string>();

        foreach (var row in rows)
        {
            if (!Phase7DriverTestContext.IsNonDriverPlannedTarget(row.PrimaryTarget))
                continue;

            // A non-driver geplant row must have a rationale explaining what will be done
            if (string.IsNullOrWhiteSpace(row.Rationale) || row.Rationale == "–")
            {
                undocumented.Add($"{row.SourceFile} → {row.PrimaryTarget}");
            }
        }

        Assert.IsEmpty(undocumented,
            $"Phase-8-Gate-006: {undocumented.Count} nicht-Treiber-(geplant)-Zeile(n) ohne Begründungsnotiz. " +
            $"Diese Zeilen blockieren den Gate-Abschluss:\n" +
            $"Phase-8-Gate-006: {undocumented.Count} non-driver (geplant) row(s) without rationale note. " +
            $"These rows block gate closure:\n" +
            string.Join("\n", undocumented));
    }

    // ── Hilfsmethoden / Helper methods ─────────────────────────────────────────

    private static int CountOccurrences(string text, string pattern)
    {
        int count = 0;
        int index = 0;
        while ((index = text.IndexOf(pattern, index, StringComparison.Ordinal)) >= 0)
        {
            count++;
            index += pattern.Length;
        }
        return count;
    }
}
