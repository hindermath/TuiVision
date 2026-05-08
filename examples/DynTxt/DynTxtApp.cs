// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.DynTxt;

/// <summary>
/// Dynamisches Textbeispiel mit Headless-Seam.
///
/// Dynamic text example with a headless seam.
/// </summary>
public sealed class DynTxtApp : TApplication
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
    public DynTxtApp(TRect bounds, bool headless = false) : base(bounds) => _headless = headless;

    /// <summary>
    /// Aktueller sichtbarer Text.
    ///
    /// Current visible text.
    /// </summary>
    public string VisibleText { get; private set; } = string.Empty;

    /// <summary>
    /// Aktualisiert den dynamischen Text und klemmt ihn auf die Breite.
    ///
    /// Updates dynamic text and clips it to the width.
    /// </summary>
    /// <param name="value">Der neue Wert. / The new value.</param>
    /// <param name="width">Die sichtbare Breite. / The visible width.</param>
    /// <returns>Der sichtbare Text. / The visible text.</returns>
    public string UpdateText(string value, int width)
    {
        int effectiveWidth = Math.Max(0, width);
        string text = value ?? string.Empty;
        VisibleText = text.Length <= effectiveWidth ? text.PadRight(effectiveWidth) : text[..effectiveWidth];
        return VisibleText;
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
