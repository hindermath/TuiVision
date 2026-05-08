// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.Clipboard;

/// <summary>
/// Zwischenablage-Beispiel mit Headless-Seam fuer Smoke-Tests.
///
/// Clipboard example with a headless seam for smoke tests.
/// </summary>
public sealed class ClipboardApp : TApplication
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
    public ClipboardApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Aktueller Eingabetext des Beispiels.
    ///
    /// Current input text of the example.
    /// </summary>
    public string InputText { get; private set; } = string.Empty;

    /// <summary>
    /// Letzter sichtbarer Ergebnistext.
    ///
    /// Last visible result text.
    /// </summary>
    public string LastVisibleMessage { get; private set; } = "clipboard: ready";

    /// <summary>
    /// Setzt den Eingabetext.
    ///
    /// Sets the input text.
    /// </summary>
    /// <param name="text">Der neue Text. / The new text.</param>
    public void SetInput(string text) => InputText = text ?? string.Empty;

    /// <summary>
    /// Kopiert den Eingabetext in die verwaltete Zwischenablage.
    ///
    /// Copies the input text into the managed clipboard.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CopyInput()
    {
        ManagedClipboard.SetText(InputText);
        LastVisibleMessage = $"clipboard: copied '{InputText}'";
        return LastVisibleMessage;
    }

    /// <summary>
    /// Schneidet den Eingabetext aus und merkt ihn in der verwalteten Zwischenablage.
    ///
    /// Cuts the input text and stores it in the managed clipboard.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string CutInput()
    {
        ManagedClipboard.SetText(InputText);
        string cut = InputText;
        InputText = string.Empty;
        LastVisibleMessage = $"clipboard: cut '{cut}'";
        return LastVisibleMessage;
    }

    /// <summary>
    /// Fuegt den Text aus der verwalteten Zwischenablage ein.
    ///
    /// Pastes text from the managed clipboard.
    /// </summary>
    /// <returns>Der sichtbare Zustand. / The visible state.</returns>
    public string PasteIntoInput()
    {
        InputText = ManagedClipboard.GetText() ?? string.Empty;
        LastVisibleMessage = $"clipboard: pasted '{InputText}'";
        return LastVisibleMessage;
    }

    /// <summary>
    /// Liefert den sichtbaren Zustand fuer eine isolierte oder nicht verfuegbare Zwischenablage.
    ///
    /// Returns the visible state for an isolated or unavailable clipboard.
    /// </summary>
    /// <returns>Der sichtbare Fallback-Zustand. / The visible fallback state.</returns>
    public string SimulateUnavailableClipboard()
    {
        LastVisibleMessage = "clipboard: isolated fallback visible";
        return LastVisibleMessage;
    }

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
