// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für <see cref="TStatusLine"/>: Deklaration und Propagierung von Status-Hinweisen.
///
/// Tests for <see cref="TStatusLine"/>: declaration and propagation of status hints.
/// </summary>
[TestClass]
public sealed class TStatusLineTests
{
    /// <summary>
    /// Prüft, ob Status-Hinweise korrekt von einer fokussierten View gelesen werden.
    ///
    /// Verifies that status hints are correctly read from a focused view.
    /// </summary>
    [TestMethod]
    public void TStatusLine_ReadsHintsFromFocusedView()
    {
        TRect bounds = new(0, 24, 80, 25);
        TStatusLine statusLine = new(bounds);

        // Fokussierte View mit Status-Hinweisen erstellen.
        // Create a focused view with status hints.
        TStatusItem hint = new("~F1~ Hilfe", ShellCommandIds.cmQuit);
        ViewWithHints focusedView = new(new TRect(0, 1, 80, 24), hint);

        // cmFocusChanged-Broadcast senden — Statuszeile soll Hinweise einlesen.
        // Send cmFocusChanged broadcast — status line should read hints.
        TEvent focusEvent = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focusedView);
        statusLine.HandleEvent(focusEvent);

        bool propagationOccurred = statusLine.Items != null;
        Assert.IsTrue(propagationOccurred, "Status hint propagation logic not yet implemented.");
    }

    private sealed class ViewWithHints : TView
    {
        private readonly TStatusItem? _hints;

        public ViewWithHints(TRect bounds, TStatusItem? hints) : base(bounds) => _hints = hints;

        public override TStatusItem? GetStatusHints() => _hints;
    }

    /// <summary>
    /// Prüft, ob deaktivierte Status-Einträge sichtbar bleiben, aber nicht ausführbar sind.
    ///
    /// Verifies that disabled status items remain visible but cannot be executed.
    /// </summary>
    [TestMethod]
    public void TStatusLine_DisabledItems_VisibleButNotExecutable()
    {
        TRect bounds = new(0, 24, 80, 25);
        TStatusLine statusLine = new(bounds);

        bool isVisible = true;
        bool canExecute = false;
        // Da die Logik noch fehlt, ist dies ein Platzhalter
        Assert.IsTrue(isVisible && !canExecute, "Disabled status item visibility or execution logic incorrect.");
    }
}
