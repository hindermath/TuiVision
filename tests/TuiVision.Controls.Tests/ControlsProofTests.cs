// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Nachweis-Tests für die geplanten Controls-Typen im M-07-Beweis-Ledger.
/// Deckt configfile.cc, tprogini.cc, twindow.cc, tfileinf.cc,
/// tvalidat.cc, tfilterv.cc, trangeva.cc, tclrdisp.cc, tcolordi.cc,
/// tcolorgr.cc, tcolorit.cc, tcolorse.cc, tpalette.cc, tmonosel.cc,
/// tparamte.cc, tvtext1.cc, tvtext2.cc, osclipboard.cc / win32clip.cc,
/// tsubmenu.cc und tstatusd.cc ab.
///
/// Proof tests for planned Controls types in the M-07 proof ledger.
/// Covers configfile.cc, tprogini.cc, twindow.cc, tfileinf.cc,
/// tvalidat.cc, tfilterv.cc, trangeva.cc, tclrdisp.cc, tcolordi.cc,
/// tcolorgr.cc, tcolorit.cc, tcolorse.cc, tpalette.cc, tmonosel.cc,
/// tparamte.cc, tvtext1.cc, tvtext2.cc, osclipboard.cc / win32clip.cc,
/// tsubmenu.cc, and tstatusd.cc.
/// </summary>
[TestClass]
public sealed class ControlsProofTests
{
    // ── TConfigFile (configfile.cc) ───────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TConfigFile"/> Schlüssel-Wert-Paare speichert und abruft.
    ///
    /// Verifies that <see cref="TConfigFile"/> stores and retrieves key-value pairs.
    /// </summary>
    [TestMethod]
    public void TConfigFile_SetAndGet_ReturnsStoredValue()
    {
        var config = new TConfigFile();
        config.Set("color", "blue");

        Assert.AreEqual("blue", config.Get("color"),
            "TConfigFile.Get muss gespeicherten Wert zurückgeben. " +
            "TConfigFile.Get must return the stored value.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TConfigFile"/> fehlende Schlüssel als null zurückgibt.
    ///
    /// Verifies that <see cref="TConfigFile"/> returns null for missing keys.
    /// </summary>
    [TestMethod]
    public void TConfigFile_Get_MissingKey_ReturnsNull()
    {
        var config = new TConfigFile();

        Assert.IsNull(config.Get("nonexistent"),
            "TConfigFile.Get muss null für fehlende Schlüssel zurückgeben. " +
            "TConfigFile.Get must return null for missing keys.");
    }

    // ── TIniFile (tprogini.cc) ────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TIniFile"/> Sektionen und Werte unterstützt.
    ///
    /// Verifies that <see cref="TIniFile"/> supports sections and values.
    /// </summary>
    [TestMethod]
    public void TIniFile_SetInSection_ReturnsValueBySection()
    {
        var ini = new TIniFile();
        ini.Set("Display", "Width", "80");
        ini.Set("Display", "Height", "25");

        Assert.AreEqual("80", ini.Get("Display", "Width"),
            "TIniFile.Get muss Wert aus Sektion zurückgeben. " +
            "TIniFile.Get must return value from section.");
        Assert.AreEqual("25", ini.Get("Display", "Height"),
            "TIniFile.Get muss zweiten Wert aus Sektion zurückgeben. " +
            "TIniFile.Get must return second value from section.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TIniFile"/> fehlende Schlüssel als null zurückgibt.
    ///
    /// Verifies that <see cref="TIniFile"/> returns null for missing keys.
    /// </summary>
    [TestMethod]
    public void TIniFile_Get_MissingSection_ReturnsNull()
    {
        var ini = new TIniFile();

        Assert.IsNull(ini.Get("NoSuchSection", "key"),
            "TIniFile.Get muss null zurückgeben wenn Sektion fehlt. " +
            "TIniFile.Get must return null when section is missing.");
    }

    // ── TWindow (twindow.cc) ──────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TWindow"/> Titel und Bounds korrekt setzt.
    ///
    /// Verifies that <see cref="TWindow"/> correctly sets title and bounds.
    /// </summary>
    [TestMethod]
    public void TWindow_Constructor_SetsProperties()
    {
        var window = new TWindow("My Window", 0, 0, 40, 12);

        Assert.AreEqual("My Window", window.Title,
            "TWindow.Title muss Konstruktor-Wert enthalten. TWindow.Title must hold the constructor value.");
        Assert.AreEqual(40, window.Bounds.Width,
            "TWindow.Bounds.Width muss 40 sein. TWindow.Bounds.Width must be 40.");
    }

    // ── TFileInfo (tfileinf.cc) ───────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TFileInfo"/> Datei-Metadaten hält.
    ///
    /// Verifies that <see cref="TFileInfo"/> holds file metadata.
    /// </summary>
    [TestMethod]
    public void TFileInfo_Constructor_StoresMetadata()
    {
        var fi = new TFileInfo("readme.txt", 1024L, new DateTime(2026, 1, 1));

        Assert.AreEqual("readme.txt", fi.FileName,
            "TFileInfo.FileName muss Konstruktor-Wert enthalten. TFileInfo.FileName must hold constructor value.");
        Assert.AreEqual(1024L, fi.Size,
            "TFileInfo.Size muss 1024 sein. TFileInfo.Size must be 1024.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TFileInfo"/> Datum zurückgibt.
    ///
    /// Verifies that <see cref="TFileInfo"/> returns the date.
    /// </summary>
    [TestMethod]
    public void TFileInfo_LastModified_IsSetFromConstructor()
    {
        var date = new DateTime(2026, 3, 27);
        var fi = new TFileInfo("doc.txt", 0L, date);

        Assert.AreEqual(date, fi.LastModified,
            "TFileInfo.LastModified muss Datum aus Konstruktor zurückgeben. " +
            "TFileInfo.LastModified must return the date from constructor.");
    }

    // ── TValidator (tvalidat.cc) ──────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass eine abgeleitete <see cref="TValidator"/>-Klasse die Validierungslogik kapselt.
    ///
    /// Verifies that a derived <see cref="TValidator"/> class encapsulates validation logic.
    /// </summary>
    [TestMethod]
    public void TValidator_Validate_ReturnsExpectedResult()
    {
        // Concrete subclass for testing
        var validator = new AlwaysValidValidator();

        Assert.IsTrue(validator.IsValid("anything"),
            "TValidator.IsValid muss für AlwaysValid true zurückgeben. " +
            "TValidator.IsValid must return true for AlwaysValid.");
    }

    /// <summary>
    /// Prüft, dass eine versagende <see cref="TValidator"/>-Implementierung false zurückgibt.
    ///
    /// Verifies that a failing <see cref="TValidator"/> implementation returns false.
    /// </summary>
    [TestMethod]
    public void TValidator_Validate_FailingValidator_ReturnsFalse()
    {
        var validator = new AlwaysInvalidValidator();

        Assert.IsFalse(validator.IsValid("anything"),
            "TValidator.IsValid muss für AlwaysInvalid false zurückgeben. " +
            "TValidator.IsValid must return false for AlwaysInvalid.");
    }

    // ── TFilterValidator (tfilterv.cc) ────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TFilterValidator"/> nur erlaubte Zeichen zulässt.
    ///
    /// Verifies that <see cref="TFilterValidator"/> allows only permitted characters.
    /// </summary>
    [TestMethod]
    public void TFilterValidator_AllowedChars_AcceptsMatchingInput()
    {
        var validator = new TFilterValidator("0123456789");

        Assert.IsTrue(validator.IsValid("12345"),
            "TFilterValidator muss reine Zifferneingabe akzeptieren. " +
            "TFilterValidator must accept pure digit input.");
        Assert.IsFalse(validator.IsValid("12a45"),
            "TFilterValidator muss Eingabe mit nicht erlaubtem Zeichen ablehnen. " +
            "TFilterValidator must reject input with a non-allowed character.");
    }

    // ── TRangeValidator (trangeva.cc) ─────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TRangeValidator"/> numerische Bereiche prüft.
    ///
    /// Verifies that <see cref="TRangeValidator"/> checks numeric ranges.
    /// </summary>
    [TestMethod]
    public void TRangeValidator_InRange_ReturnsTrue()
    {
        var validator = new TRangeValidator(1, 100);

        Assert.IsTrue(validator.IsValid("50"),
            "TRangeValidator muss 50 (in Bereich 1–100) akzeptieren. " +
            "TRangeValidator must accept 50 (in range 1–100).");
    }

    /// <summary>
    /// Prüft, dass <see cref="TRangeValidator"/> Werte außerhalb des Bereichs ablehnt.
    ///
    /// Verifies that <see cref="TRangeValidator"/> rejects values outside the range.
    /// </summary>
    [TestMethod]
    public void TRangeValidator_OutOfRange_ReturnsFalse()
    {
        var validator = new TRangeValidator(1, 100);

        Assert.IsFalse(validator.IsValid("0"),
            "TRangeValidator muss 0 (außerhalb 1–100) ablehnen. " +
            "TRangeValidator must reject 0 (outside range 1–100).");
        Assert.IsFalse(validator.IsValid("101"),
            "TRangeValidator muss 101 (außerhalb 1–100) ablehnen. " +
            "TRangeValidator must reject 101 (outside range 1–100).");
        Assert.IsFalse(validator.IsValid("abc"),
            "TRangeValidator muss 'abc' (kein numerischer Wert) ablehnen. " +
            "TRangeValidator must reject 'abc' (non-numeric value).");
    }

    // ── TColorItem (tcolorit.cc) ──────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TColorItem"/> Name und Farbwert speichert.
    ///
    /// Verifies that <see cref="TColorItem"/> stores name and colour value.
    /// </summary>
    [TestMethod]
    public void TColorItem_Constructor_StoresNameAndColor()
    {
        var item = new TColorItem("Background", ConsoleColor.Black);

        Assert.AreEqual("Background", item.Name,
            "TColorItem.Name muss Konstruktor-Wert enthalten. TColorItem.Name must hold constructor value.");
        Assert.AreEqual(ConsoleColor.Black, item.Color,
            "TColorItem.Color muss Konstruktor-Wert enthalten. TColorItem.Color must hold constructor value.");
    }

    // ── TColorGroup (tcolorgr.cc) ─────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TColorGroup"/> mehrere Farbelemente enthält.
    ///
    /// Verifies that <see cref="TColorGroup"/> holds multiple colour items.
    /// </summary>
    [TestMethod]
    public void TColorGroup_AddItem_IncreasesCount()
    {
        var group = new TColorGroup("Window Colors");
        group.AddItem(new TColorItem("Background", ConsoleColor.Black));
        group.AddItem(new TColorItem("Foreground", ConsoleColor.White));

        Assert.AreEqual(2, group.ItemCount,
            "TColorGroup.ItemCount muss 2 nach zwei Add-Aufrufen sein. " +
            "TColorGroup.ItemCount must be 2 after two Add calls.");
    }

    // ── TColorDialog (tcolordi.cc) ────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TColorDialog"/> Palette, Selektor und Vorschau miteinander verbindet.
    ///
    /// Verifies that <see cref="TColorDialog"/> wires palette, selector, and preview together.
    /// </summary>
    [TestMethod]
    public void TColorDialog_SelectColor_UpdatesSelectorAndPreview()
    {
        var palette = new TPalette();
        var dialog = new TColorDialog(palette);

        dialog.SelectColor(ConsoleColor.Yellow);

        Assert.AreSame(palette, dialog.Palette,
            "TColorDialog muss die übergebene Palette behalten. TColorDialog must keep the provided palette.");
        Assert.AreEqual(ConsoleColor.Yellow, dialog.Selector.SelectedColor,
            "TColorDialog.SelectColor muss den Selektor aktualisieren. TColorDialog.SelectColor must update the selector.");
        Assert.AreEqual(ConsoleColor.Yellow, dialog.Preview.Foreground,
            "TColorDialog.SelectColor muss die Vorschau aktualisieren. TColorDialog.SelectColor must update the preview.");
    }

    // ── TColorSelector (tcolorse.cc) ──────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TColorSelector"/> eine Farbauswahl verwaltet.
    ///
    /// Verifies that <see cref="TColorSelector"/> manages a colour selection.
    /// </summary>
    [TestMethod]
    public void TColorSelector_SelectColor_UpdatesSelection()
    {
        var selector = new TColorSelector();
        selector.SelectColor(ConsoleColor.Cyan);

        Assert.AreEqual(ConsoleColor.Cyan, selector.SelectedColor,
            "TColorSelector.SelectedColor muss nach SelectColor(Cyan) Cyan sein. " +
            "TColorSelector.SelectedColor must be Cyan after SelectColor(Cyan).");
    }

    // ── TColorDisplay (tclrdisp.cc) ───────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TColorDisplay"/> Vordergrund- und Hintergrundfarbe speichert.
    ///
    /// Verifies that <see cref="TColorDisplay"/> stores foreground and background colours.
    /// </summary>
    [TestMethod]
    public void TColorDisplay_SetColors_StoresForegroundAndBackground()
    {
        var display = new TColorDisplay();
        display.SetColors(ConsoleColor.White, ConsoleColor.DarkBlue);

        Assert.AreEqual(ConsoleColor.White, display.Foreground,
            "TColorDisplay.Foreground muss White sein. TColorDisplay.Foreground must be White.");
        Assert.AreEqual(ConsoleColor.DarkBlue, display.Background,
            "TColorDisplay.Background muss DarkBlue sein. TColorDisplay.Background must be DarkBlue.");
    }

    // ── TPalette (tpalette.cc) ────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TPalette"/> Farbgruppen verwaltet.
    ///
    /// Verifies that <see cref="TPalette"/> manages colour groups.
    /// </summary>
    [TestMethod]
    public void TPalette_AddGroup_IncreasesGroupCount()
    {
        var palette = new TPalette();
        palette.AddGroup(new TColorGroup("Dialogs"));
        palette.AddGroup(new TColorGroup("Menus"));

        Assert.AreEqual(2, palette.GroupCount,
            "TPalette.GroupCount muss 2 nach zwei AddGroup-Aufrufen sein. " +
            "TPalette.GroupCount must be 2 after two AddGroup calls.");
    }

    // ── TMonoSelector (tmonosel.cc) ───────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TMonoSelector"/> eine Einzelauswahl aus mehreren Optionen verwaltet.
    ///
    /// Verifies that <see cref="TMonoSelector"/> manages a single selection from multiple options.
    /// </summary>
    [TestMethod]
    public void TMonoSelector_SelectItem_UpdatesSelection()
    {
        var selector = new TMonoSelector(["Red", "Green", "Blue"]);
        selector.Select(1);

        Assert.AreEqual(1, selector.SelectedIndex,
            "TMonoSelector.SelectedIndex muss 1 sein. TMonoSelector.SelectedIndex must be 1.");
        Assert.AreEqual("Green", selector.SelectedItem,
            "TMonoSelector.SelectedItem muss 'Green' sein. TMonoSelector.SelectedItem must be 'Green'.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TMonoSelector"/> ungültige Indizes ablehnt.
    ///
    /// Verifies that <see cref="TMonoSelector"/> rejects invalid indices.
    /// </summary>
    [TestMethod]
    public void TMonoSelector_Select_InvalidIndex_Throws()
    {
        var selector = new TMonoSelector(["A", "B"]);

        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => selector.Select(5),
            "TMonoSelector.Select muss ArgumentOutOfRangeException für ungültigen Index werfen. " +
            "TMonoSelector.Select must throw ArgumentOutOfRangeException for an invalid index.");
    }

    // ── TParamText (tparamte.cc) ──────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TParamText"/> Platzhalter mit Parametern ersetzt.
    ///
    /// Verifies that <see cref="TParamText"/> replaces placeholders with parameters.
    /// </summary>
    [TestMethod]
    public void TParamText_Format_ReplacesPlaceholders()
    {
        var text = new TParamText("Hello, {0}! You have {1} messages.");
        string result = text.Format("Alice", "3");

        Assert.AreEqual("Hello, Alice! You have 3 messages.", result,
            "TParamText.Format muss Platzhalter ersetzen. TParamText.Format must replace placeholders.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TParamText"/> Text ohne Platzhalter unverändert zurückgibt.
    ///
    /// Verifies that <see cref="TParamText"/> returns text without placeholders unchanged.
    /// </summary>
    [TestMethod]
    public void TParamText_Format_NoPlaceholders_ReturnsOriginal()
    {
        var text = new TParamText("Static text.");
        string result = text.Format();

        Assert.AreEqual("Static text.", result,
            "TParamText.Format muss Text ohne Platzhalter unverändert zurückgeben. " +
            "TParamText.Format must return text without placeholders unchanged.");
    }

    // ── ManagedClipboard / osclipboard.cc + win32clip.cc ─────────────────────

    /// <summary>
    /// Prüft, dass <see cref="ManagedClipboard"/> Zeichenketten-Inhalte speichert.
    ///
    /// Verifies that <see cref="ManagedClipboard"/> stores string content.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_SetAndGet_RoundTripsText()
    {
        ManagedClipboard.SetText("clipboard content");
        string? result = ManagedClipboard.GetText();

        Assert.AreEqual("clipboard content", result,
            "ManagedClipboard.GetText muss gespeicherten Text zurückgeben. " +
            "ManagedClipboard.GetText must return stored text.");
    }

    /// <summary>
    /// Prüft, dass <see cref="ManagedClipboard"/> leere Zwischenablage als null zurückgibt.
    ///
    /// Verifies that <see cref="ManagedClipboard"/> returns null for empty clipboard.
    /// </summary>
    [TestMethod]
    public void ManagedClipboard_Clear_ReturnsNullOnGet()
    {
        ManagedClipboard.SetText("something");
        ManagedClipboard.Clear();

        Assert.IsNull(ManagedClipboard.GetText(),
            "ManagedClipboard.GetText muss null nach Clear zurückgeben. " +
            "ManagedClipboard.GetText must return null after Clear.");
    }

    // ── TvUIStrings / tvtext1.cc + tvtext2.cc ─────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TvUIStrings"/> die erwarteten Standard-UI-Strings enthält.
    ///
    /// Verifies that <see cref="TvUIStrings"/> contains the expected default UI strings.
    /// </summary>
    [TestMethod]
    public void TvUIStrings_DefaultStrings_AreNonEmpty()
    {
        Assert.IsFalse(string.IsNullOrEmpty(TvUIStrings.Ok),
            "TvUIStrings.Ok darf nicht leer sein. TvUIStrings.Ok must not be empty.");
        Assert.IsFalse(string.IsNullOrEmpty(TvUIStrings.Cancel),
            "TvUIStrings.Cancel darf nicht leer sein. TvUIStrings.Cancel must not be empty.");
        Assert.IsFalse(string.IsNullOrEmpty(TvUIStrings.Open),
            "TvUIStrings.Open darf nicht leer sein. TvUIStrings.Open must not be empty.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TvUIStrings"/> alle Standardtexte als nicht-null zurückgibt.
    ///
    /// Verifies that <see cref="TvUIStrings"/> returns all standard texts as non-null.
    /// </summary>
    [TestMethod]
    public void TvUIStrings_AllStandardStrings_AreNotNull()
    {
        Assert.IsNotNull(TvUIStrings.Save,
            "TvUIStrings.Save darf nicht null sein. TvUIStrings.Save must not be null.");
        Assert.IsNotNull(TvUIStrings.Close,
            "TvUIStrings.Close darf nicht null sein. TvUIStrings.Close must not be null.");
    }

    // ── TSubMenu (tsubmenu.cc) ────────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TSubMenu"/> Name, HelpCtx und Items-Liste korrekt speichert.
    ///
    /// Verifies that <see cref="TSubMenu"/> correctly stores name, HelpCtx, and item list.
    /// </summary>
    [TestMethod]
    public void TSubMenu_Constructor_StoresNameAndItems()
    {
        TMenuItem items = new("~N~eu / ~N~ew", 100);
        var sub = new TSubMenu("~D~atei / ~F~ile", 0x2100, items);

        Assert.AreEqual("~D~atei / ~F~ile", sub.Name,
            "TSubMenu.Name muss Konstruktor-Wert enthalten. TSubMenu.Name must hold the constructor value.");
        Assert.AreEqual(0x2100, sub.HelpCtx,
            "TSubMenu.HelpCtx muss 0x2100 sein. TSubMenu.HelpCtx must be 0x2100.");
        Assert.AreSame(items, sub.Items,
            "TSubMenu.Items muss die übergebene TMenuItem-Instanz enthalten. " +
            "TSubMenu.Items must hold the provided TMenuItem instance.");
        Assert.IsNull(sub.Next,
            "TSubMenu.Next muss null sein wenn kein Next übergeben wurde. " +
            "TSubMenu.Next must be null when no next was provided.");
    }

    /// <summary>
    /// Prüft, dass verknüpfte <see cref="TSubMenu"/>-Knoten korrekt traversiert werden können.
    ///
    /// Verifies that linked <see cref="TSubMenu"/> nodes can be traversed correctly.
    /// </summary>
    [TestMethod]
    public void TSubMenu_LinkedList_NextNodeIsAccessible()
    {
        var second = new TSubMenu("~E~dit", 0);
        var first = new TSubMenu("~F~ile", 0, next: second);

        Assert.AreSame(second, first.Next,
            "TSubMenu.Next muss zweites Untermenü zurückgeben. " +
            "TSubMenu.Next must return the second submenu.");
    }

    // ── TStatusDef (tstatusd.cc) ──────────────────────────────────────────────

    /// <summary>
    /// Prüft, dass <see cref="TStatusDef"/> einen Hilfekontext-Bereich und zugehörige Einträge speichert.
    ///
    /// Verifies that <see cref="TStatusDef"/> stores a help-context range and associated items.
    /// </summary>
    [TestMethod]
    public void TStatusDef_Constructor_StoresRangeAndItems()
    {
        TStatusItem item = new("~F1~ Hilfe", ShellCommandIds.cmHelp);
        var def = new TStatusDef(100, 199, item);

        Assert.AreEqual(100, def.MinCtx,
            "TStatusDef.MinCtx muss 100 sein. TStatusDef.MinCtx must be 100.");
        Assert.AreEqual(199, def.MaxCtx,
            "TStatusDef.MaxCtx muss 199 sein. TStatusDef.MaxCtx must be 199.");
        Assert.AreSame(item, def.Items,
            "TStatusDef.Items muss die übergebene TStatusItem-Instanz enthalten. " +
            "TStatusDef.Items must hold the provided TStatusItem instance.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TStatusDef.Matches"/> korrekt für Werte innerhalb und außerhalb des Bereichs antwortet.
    ///
    /// Verifies that <see cref="TStatusDef.Matches"/> responds correctly for values inside and outside the range.
    /// </summary>
    [TestMethod]
    public void TStatusDef_Matches_ReturnsTrueForInRangeAndFalseForOutOfRange()
    {
        var def = new TStatusDef(100, 199, null);

        Assert.IsTrue(def.Matches(100),
            "TStatusDef.Matches muss true für untere Grenze zurückgeben. " +
            "TStatusDef.Matches must return true for the lower bound.");
        Assert.IsTrue(def.Matches(150),
            "TStatusDef.Matches muss true für Mittelwert zurückgeben. " +
            "TStatusDef.Matches must return true for a mid-range value.");
        Assert.IsTrue(def.Matches(199),
            "TStatusDef.Matches muss true für obere Grenze zurückgeben. " +
            "TStatusDef.Matches must return true for the upper bound.");
        Assert.IsFalse(def.Matches(99),
            "TStatusDef.Matches muss false für Wert unterhalb des Bereichs zurückgeben. " +
            "TStatusDef.Matches must return false for a value below the range.");
        Assert.IsFalse(def.Matches(200),
            "TStatusDef.Matches muss false für Wert oberhalb des Bereichs zurückgeben. " +
            "TStatusDef.Matches must return false for a value above the range.");
    }

    // ── Helper validators for TValidator tests ────────────────────────────────

    private sealed class AlwaysValidValidator : TValidator
    {
        /// <inheritdoc />
        public override bool IsValid(string input) => true;
    }

    private sealed class AlwaysInvalidValidator : TValidator
    {
        /// <inheritdoc />
        public override bool IsValid(string input) => false;
    }
}
