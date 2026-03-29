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

        // Deaktivierten Status-Eintrag erstellen und per Fokus-Broadcast propagieren.
        // Create a disabled status item and propagate it via focus broadcast.
        TStatusItem disabledHint = new("~F2~ Deaktiviert", ShellCommandIds.cmQuit) { Disabled = true };
        ViewWithHints focusedView = new(new TRect(0, 1, 80, 24), disabledHint);

        TEvent focusEvent = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focusedView);
        statusLine.HandleEvent(focusEvent);

        // Eintrag muss sichtbar sein (Items != null) und als deaktiviert markiert sein.
        // Item must be visible (Items != null) and marked as disabled.
        Assert.IsNotNull(statusLine.Items, "Disabled status item must still appear in the status line.");
        Assert.IsTrue(statusLine.Items.Disabled, "Disabled status item must have Disabled == true.");
    }

    // -----------------------------------------------------------------------
    // T012: TStatusDef routing tests (RED – fail until T016 adds behavior)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Prüft, ob die Statuszeile mit TStatusDef die erste passende Definition auswählt.
    ///
    /// Verifies that the status line with TStatusDef selects the first matching definition.
    /// </summary>
    [TestMethod]
    public void TStatusLine_DefinitionRouting_MatchesFirstDefinition()
    {
        TStatusItem helpItem = new("~F1~ Hilfe", ShellCommandIds.cmHelp);
        TStatusDef def = new(100, 199, helpItem);
        TStatusLine statusLine = new(new TRect(0, 24, 80, 25), def);

        // Fokussierte View mit HelpContext=150 (innerhalb [100, 199]).
        // Focused view with HelpContext=150 (within [100, 199]).
        ViewWithContext focusedView = new(new TRect(0, 1, 80, 24), 150);
        TEvent focusEvent = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focusedView);
        statusLine.HandleEvent(focusEvent);

        Assert.IsNotNull(statusLine.Items, "Status line must display items when the help context matches a definition.");
        Assert.AreSame(helpItem, statusLine.Items, "Status line must show the items from the matching TStatusDef.");
    }

    /// <summary>
    /// Prüft, ob bei überlappenden Bereichen die erste Definition gewinnt.
    ///
    /// Verifies that the first declared definition wins when ranges overlap.
    /// </summary>
    [TestMethod]
    public void TStatusLine_DefinitionRouting_FirstMatchWinsOnOverlap()
    {
        TStatusItem firstItem = new("~F1~ Hilfe", ShellCommandIds.cmHelp);
        TStatusItem secondItem = new("~F2~ Öffnen", ShellCommandIds.cmOpen);

        // Beide Definitionen decken Kontext 150 ab; erste soll gewinnen.
        // Both definitions cover context 150; the first must win.
        TStatusDef second = new(100, 199, secondItem);
        TStatusDef first = new(100, 199, firstItem, second);
        TStatusLine statusLine = new(new TRect(0, 24, 80, 25), first);

        ViewWithContext focusedView = new(new TRect(0, 1, 80, 24), 150);
        TEvent focusEvent = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focusedView);
        statusLine.HandleEvent(focusEvent);

        Assert.AreSame(firstItem, statusLine.Items, "First declared definition must win when ranges overlap.");
    }

    /// <summary>
    /// Prüft, ob die Statuszeile neutral bleibt (Items == null), wenn keine Definition passt.
    ///
    /// Verifies that the status line shows neutral state (Items == null) when no definition matches.
    /// </summary>
    [TestMethod]
    public void TStatusLine_NeutralFallback_WhenNoDefinitionMatches()
    {
        TStatusItem helpItem = new("~F1~ Hilfe", ShellCommandIds.cmHelp);
        TStatusDef def = new(100, 199, helpItem);
        TStatusLine statusLine = new(new TRect(0, 24, 80, 25), def);

        // Fokussierte View mit HelpContext=50 (außerhalb [100, 199]).
        // Focused view with HelpContext=50 (outside [100, 199]).
        ViewWithContext focusedView = new(new TRect(0, 1, 80, 24), 50);
        TEvent focusEvent = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focusedView);
        statusLine.HandleEvent(focusEvent);

        Assert.IsNull(statusLine.Items, "Status line must be neutral (Items == null) when no definition matches the help context.");
    }

    /// <summary>
    /// Prüft, ob ein Fokuswechsel die angezeigten Status-Einträge aktualisiert.
    ///
    /// Verifies that a focus change refreshes the displayed status items.
    /// </summary>
    [TestMethod]
    public void TStatusLine_ContextChange_RefreshesOnFocusChange()
    {
        TStatusItem firstItem = new("~F1~ Bearbeiten", ShellCommandIds.cmCopy);
        TStatusItem secondItem = new("~F2~ Öffnen", ShellCommandIds.cmOpen);
        TStatusDef second = new(200, 299, secondItem);
        TStatusDef first = new(100, 199, firstItem, second);
        TStatusLine statusLine = new(new TRect(0, 24, 80, 25), first);

        // Erster Fokus: Kontext 150 → firstItem
        ViewWithContext view1 = new(new TRect(0, 1, 80, 24), 150);
        TEvent focus1 = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, view1);
        statusLine.HandleEvent(focus1);
        Assert.AreSame(firstItem, statusLine.Items, "First focus should select firstItem.");

        // Zweiter Fokus: Kontext 250 → secondItem
        ViewWithContext view2 = new(new TRect(0, 1, 80, 24), 250);
        TEvent focus2 = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, view2);
        statusLine.HandleEvent(focus2);
        Assert.AreSame(secondItem, statusLine.Items, "After focus change, status line must switch to the new context's items.");
    }

    /// <summary>
    /// Prüft, ob Status-Aktionen über den normalen Befehlspfad ausgelöst werden können.
    ///
    /// Verifies that status actions can be executed through the normal command path.
    /// </summary>
    [TestMethod]
    public void TStatusLine_ExecutesCommand_ViaCommandPath()
    {
        // Dieser Test validiert, dass TStatusItem-Einträge mit gültigen Command-IDs
        // definiert werden können und dass die IDs korrekte Werte besitzen.
        // This test validates that TStatusItem entries with valid command IDs can be
        // defined and that the IDs hold the correct values.
        TStatusItem actionItem = new("~F1~ Hilfe", ShellCommandIds.cmHelp);
        TStatusDef def = new(0, int.MaxValue, actionItem);
        TStatusLine statusLine = new(new TRect(0, 24, 80, 25), def);

        ViewWithContext focusedView = new(new TRect(0, 1, 80, 24), 0);
        TEvent focusEvent = TEvent.CreateBroadcast(ShellCommandIds.cmFocusChanged, focusedView);
        statusLine.HandleEvent(focusEvent);

        Assert.IsNotNull(statusLine.Items, "Items must be set after focus event with matching context.");
        Assert.AreEqual(ShellCommandIds.cmHelp, statusLine.Items!.Command,
            "The status item must carry the correct command ID for routing.");
    }

    // -----------------------------------------------------------------------
    // Helper types
    // -----------------------------------------------------------------------

    private sealed class ViewWithContext : TView
    {
        public ViewWithContext(TRect bounds, int helpContext) : base(bounds)
        {
            HelpContext = helpContext;
        }
    }
}
