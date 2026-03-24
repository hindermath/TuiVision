// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Drivers.Tests;

/// <summary>
/// Validierungstests für das M-07-Beweis-Ledger <c>docs/porting-status.md</c>.
/// Prüft Schema, Vollständigkeit und Statuswert-Konformität.
///
/// Validation tests for the M-07 proof ledger <c>docs/porting-status.md</c>.
/// Checks schema, completeness, and status-value conformance.
/// </summary>
[TestClass]
public sealed class PortingStatusLedgerTests
{
    /// <summary>
    /// Prüft, dass die Beweisledger-Datei im Repository vorhanden ist.
    ///
    /// Verifies that the proof ledger file exists in the repository.
    /// </summary>
    [TestMethod]
    public void LedgerFile_Exists()
    {
        string path = Phase7DriverTestContext.GetPortingStatusPath();
        Assert.IsTrue(File.Exists(path),
            $"docs/porting-status.md muss vorhanden sein unter: {path}");
    }

    /// <summary>
    /// Prüft, dass die Beweisledger-Datei mindestens 100 Ledger-Zeilen enthält,
    /// die mit dem Repository-relativen historischen Pfad beginnen.
    ///
    /// Verifies that the proof ledger contains at least 100 ledger rows
    /// beginning with the repository-relative historical path.
    /// </summary>
    [TestMethod]
    public void LedgerFile_ContainsSufficientRows()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        int rowCount = content
            .Split('\n')
            .Count(line => line.TrimStart().StartsWith("| tv203s/", StringComparison.Ordinal));

        Assert.IsGreaterThanOrEqualTo(100, rowCount,
            $"Erwartet mindestens 100 Ledger-Zeilen, gefunden: {rowCount}. " +
            "Expected at least 100 ledger rows.");
    }

    /// <summary>
    /// Prüft, dass jeder Statuswert im Ledger einem der drei erlaubten Werte entspricht.
    ///
    /// Verifies that every status value in the ledger matches one of the three allowed values.
    /// </summary>
    [TestMethod]
    public void LedgerFile_ContainsOnlyAllowedStatusValues()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        var violations = new List<string>();

        foreach (string line in content.Split('\n'))
        {
            string trimmed = line.TrimStart();
            if (!trimmed.StartsWith("| tv203s/", StringComparison.Ordinal))
                continue;

            string[] cells = trimmed.Split('|');
            if (cells.Length < 6)
            {
                violations.Add($"Zu wenige Spalten: {trimmed.Substring(0, Math.Min(80, trimmed.Length))}");
                continue;
            }

            // Column index 5 (0-based after split on '|') = Status column
            string status = cells[5].Trim();
            if (!Phase7DriverTestContext.AllowedStatuses.Contains(status))
            {
                violations.Add($"Unerlaubter Status '{status}' in: {trimmed.Substring(0, Math.Min(80, trimmed.Length))}");
            }
        }

        Assert.IsEmpty(violations,
            $"Unerlaubte Statuswerte gefunden / Disallowed status values found:\n{string.Join("\n", violations)}");
    }

    /// <summary>
    /// Prüft, dass jede Ledger-Zeile einen nicht-leeren Primärziel-Wert hat.
    ///
    /// Verifies that every ledger row has a non-empty primary target value.
    /// </summary>
    [TestMethod]
    public void LedgerFile_EveryRowHasNonEmptyPrimaryTarget()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        var violations = new List<string>();

        foreach (string line in content.Split('\n'))
        {
            string trimmed = line.TrimStart();
            if (!trimmed.StartsWith("| tv203s/", StringComparison.Ordinal))
                continue;

            string[] cells = trimmed.Split('|');
            if (cells.Length < 4)
            {
                violations.Add($"Zu wenige Spalten: {trimmed.Substring(0, Math.Min(80, trimmed.Length))}");
                continue;
            }

            // Column index 3 (0-based after split on '|') = primary target
            string primaryTarget = cells[3].Trim();
            if (string.IsNullOrWhiteSpace(primaryTarget) || primaryTarget == "–")
            {
                // '–' is allowed only when the status is 'bewusst ausgelassen + Begruendung'
                // and the literal target is 'bewusst ausgelassen'
                string target = cells[3].Trim();
                if (target == "–")
                {
                    // Check that the status is 'bewusst ausgelassen + Begruendung'
                    if (cells.Length >= 6)
                    {
                        string status = cells[5].Trim();
                        if (status != "bewusst ausgelassen + Begruendung")
                        {
                            violations.Add($"Leeres/Dash-Primärziel ohne 'bewusst ausgelassen'-Status: {trimmed.Substring(0, Math.Min(120, trimmed.Length))}");
                        }
                    }
                }
            }
        }

        Assert.IsEmpty(violations,
            $"Zeilen mit fehlendem Primärziel / Rows with missing primary target:\n{string.Join("\n", violations)}");
    }

    /// <summary>
    /// Prüft, dass jede Zeile mit Status 'bewusst ausgelassen + Begruendung'
    /// eine nicht-leere Begründungsnotiz enthält.
    ///
    /// Verifies that every row with status 'bewusst ausgelassen + Begruendung'
    /// contains a non-empty rationale note.
    /// </summary>
    [TestMethod]
    public void LedgerFile_OmittedRowsHaveRationale()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        var violations = new List<string>();
        const string omittedStatus = "bewusst ausgelassen + Begruendung";

        foreach (string line in content.Split('\n'))
        {
            string trimmed = line.TrimStart();
            if (!trimmed.StartsWith("| tv203s/", StringComparison.Ordinal))
                continue;

            string[] cells = trimmed.Split('|');
            if (cells.Length < 8)
                continue;

            string status = cells[5].Trim();
            if (status != omittedStatus)
                continue;

            string rationale = cells[7].Trim();
            if (string.IsNullOrWhiteSpace(rationale) || rationale == "–")
            {
                violations.Add($"Kein Begründungstext für 'bewusst ausgelassen': {trimmed.Substring(0, Math.Min(120, trimmed.Length))}");
            }
        }

        Assert.IsEmpty(violations,
            $"Bewusst-ausgelassen-Zeilen ohne Begründung / Omitted rows without rationale:\n{string.Join("\n", violations)}");
    }

    /// <summary>
    /// Prüft, dass die Beweisledger-Datei die Pflicht-Spaltenüberschrift der Tabelle enthält.
    ///
    /// Verifies that the proof ledger contains the mandatory table column header.
    /// </summary>
    [TestMethod]
    public void LedgerFile_ContainsTableHeader()
    {
        string content = Phase7DriverTestContext.ReadPortingStatus();
        Assert.IsTrue(
            content.Contains("| Quelldatei |", StringComparison.Ordinal),
            "Beweisledger muss eine Tabelle mit Spalte 'Quelldatei' enthalten. " +
            "Proof ledger must contain a table with column 'Quelldatei'.");
    }
}
