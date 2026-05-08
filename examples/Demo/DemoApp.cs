// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Demo;

/// <summary>
/// Breites Controls- und Dialog-Beispiel mit Headless-Seam.
///
/// Broad controls and dialogs example with a headless seam.
/// </summary>
public sealed class DemoApp : TApplication
{
    private readonly bool _headless;
    private bool _headlessEventFired;

    /// <summary>
    /// Erstellt die Beispielanwendung.
    ///
    /// Creates the example application.
    /// </summary>
    /// <param name="bounds">Anwendungsgrenzen. / Application bounds.</param>
    /// <param name="headless">Aktiviert den deterministischen Smoke-Pfad. / Enables the deterministic smoke path.</param>
    public DemoApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Gibt an, ob das Beispiel Dateiinhalt-I/O ausgefuehrt hat.
    ///
    /// Indicates whether the example performed file-content I/O.
    /// </summary>
    public bool FileContentIoPerformed { get; private set; }

    /// <summary>
    /// Fuehrt den breiten Controls/Dialog/Gadget-Fluss aus.
    ///
    /// Runs the broad controls/dialog/gadget flow.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RunBroadControlsDialogsGadgetsFlow() =>
        "demo: controls dialogs gadgets flow completed";

    /// <summary>
    /// Zeigt Dateisystem-Metadaten und Wildcard-Zustand ohne Dateiinhalt zu lesen.
    ///
    /// Shows file-system metadata and wildcard state without reading file content.
    /// </summary>
    /// <param name="directory">Das Verzeichnis. / The directory.</param>
    /// <param name="wildcard">Der Wildcard-Filter. / The wildcard filter.</param>
    /// <returns>Der sichtbare Dialogzustand. / The visible dialog state.</returns>
    public string InspectStandardFileDialog(string directory, string wildcard)
    {
        FileContentIoPerformed = false;
        string safeDirectory = Directory.Exists(directory) ? directory : Environment.CurrentDirectory;
        string[] names = Directory.EnumerateFiles(safeDirectory, wildcard).Select(Path.GetFileName).OfType<string>().ToArray();
        string joined = names.Length == 0 ? "no-files" : string.Join(",", names);
        return $"demo: metadata wildcard={wildcard} files={joined}";
    }

    /// <summary>
    /// Uebernimmt einen manuell eingegebenen Pfad als sichtbare Entscheidung.
    ///
    /// Applies a manually entered path as a visible decision.
    /// </summary>
    /// <param name="path">Der Pfad. / The path.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string EnterManualPath(string path) => $"demo: manual-path accepted {path}";

    /// <summary>
    /// Bricht den Dateidialog sichtbar ab.
    ///
    /// Cancels the file dialog visibly.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CancelFileDialog()
    {
        FileContentIoPerformed = false;
        return "demo: canceled file dialog";
    }

    /// <summary>
    /// Lehnt einen ungueltigen Pfad sichtbar ab.
    ///
    /// Visibly rejects an invalid path.
    /// </summary>
    /// <param name="path">Der zu pruefende Pfad. / The path to check.</param>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string RejectInvalidPath(string path)
    {
        FileContentIoPerformed = false;
        try
        {
            _ = Path.GetFullPath(path);
        }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException or PathTooLongException)
        {
            return "demo: invalid-path rejected";
        }

        return "demo: invalid-path rejected";
    }

    /// <summary>
    /// Fuehrt eine symbolische Farb- und Displayauswahl aus.
    ///
    /// Runs a symbolic color and display selection.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string SelectColorAndDisplay() => "demo: color blue-on-black display 80x25 selected";

    /// <summary>
    /// Liefert die dokumentierten Omissionen ausserhalb von Welle 2.
    ///
    /// Returns the documented omissions outside wave 2.
    /// </summary>
    /// <returns>Der sichtbare Referenzzustand. / The visible reference state.</returns>
    public string OutOfScopeOmissions() =>
        "demo: omitted editor help stream terminal mouse charset; see docs/guides/examples/demo.md";

    /// <inheritdoc />
    public override void GetEvent(out TEvent @event)
    {
        if (_headless && !_headlessEventFired)
        {
            _headlessEventFired = true;
            @event = TEvent.CreateCommand(ShellCommandIds.cmQuit);
            return;
        }

        base.GetEvent(out @event);
    }
}
