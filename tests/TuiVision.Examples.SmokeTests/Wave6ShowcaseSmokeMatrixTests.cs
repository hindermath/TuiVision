// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Security.Cryptography;
using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Drivers.Console;
using TuiVision.Examples.Wave6;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Prüft die sichtbare und interaktive zweite Wave-6-Stufe.
///
/// Verifies the visible and interactive Wave-6 Stage 2.
/// </summary>
[TestClass]
public sealed class Wave6ShowcaseSmokeMatrixTests : ExampleTestBase
{
    private static readonly ushort[] ExistingReadCommandIds =
    [
        Tp7FileManagerApp.CmNavigateFirstDirectory,
        Tp7FileManagerApp.CmPreviewText,
        Tp7FileManagerApp.CmDescription,
        Tp7FileManagerApp.CmPreviewHex,
        Tp7FileManagerApp.CmFilterText,
        Tp7FileManagerApp.CmSortSize,
        Tp7FileManagerApp.CmTagFirst,
        Tp7FileManagerApp.CmSearchText,
        Tp7FileManagerApp.CmPreviewAssociated,
        Tp7FileManagerApp.CmChangePalette
    ];

    /// <summary>
    /// Prüft, dass Navigation die persistente Hauptkomposition aktualisiert.
    ///
    /// Verifies that navigation updates the persistent main composition.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Keeps_Persistent_List_And_Focus()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            TWindow? initialWindow = app.MainWindow;
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x50, 0x5000, 0, 0x50))
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.AreSame(initialWindow, app.MainWindow, "Navigation must update one persistent main window.");
            Assert.AreEqual(nameof(TListBox), app.LastFocusedComponentKind);
            Assert.AreEqual("docs", app.CurrentSnapshot.RelativeDirectory);
            AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "docs", "focused path status");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Path: docs", "persistent path cells");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft ersten Frame, echte StatusLine und Description.
    ///
    /// Verifies the first frame, real StatusLine, and Description.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Proves_First_Frame_Status_And_Description()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents([TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B))]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsTrue(app.HasRealStatusLine);
            Assert.IsTrue(app.DescriptionOpened);
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "TP7 TVFM", "first-frame purpose");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "F1", "Description keyboard path");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "kontrollierte", "Description safety boundary");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Moderne C#", "Description modernization boundary");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Platform", "Description platform boundary");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "app.Run()", "Description proof boundary");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft die wesentlichen Bereiche bei 48 mal 16 Zellen.
    ///
    /// Verifies the essential regions at 48 by 16 cells.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Constrained_AppLoop_Keeps_List_Status_Help_And_Quit()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(48, 16), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B))
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsTrue(app.QuitIssued);
            Assert.IsTrue(app.DescriptionOpened);
            Assert.IsNotNull(app.MainWindow);
            Assert.AreEqual(nameof(TListBox), app.LastFocusedComponentKind);
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "docs", "constrained selected path");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Ctrl+Q", "constrained quit path");
            Assert.IsLessThanOrEqualTo(48, app.LastVisibleRegion.B.X);
            Assert.IsLessThanOrEqualTo(15, app.LastVisibleRegion.B.Y);
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft sechs geschlossene Menügruppen und vorhandene Befehls-IDs.
    ///
    /// Verifies six closed menu groups and existing command identifiers.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Menu_Exposes_All_Closed_Read_Command_Groups()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            TMenuItem[] groups = EnumerateSiblings(app.MenuBar?.Menu).ToArray();
            string[] names = groups.Select(item => StripMarkers(item.Name)).ToArray();
            int[] commandIds = groups.SelectMany(item => EnumerateTree(item.SubMenu))
                .Select(item => item.Command)
                .Where(command => command > 0)
                .ToArray();

            CollectionAssert.AreEqual(
                new[] { "File", "Navigate", "View", "Search", "Options", "Help" },
                names);
            foreach (ushort commandId in ExistingReadCommandIds)
            {
                CollectionAssert.Contains(commandIds, (int)commandId);
            }
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Text-, UTF-8- und Hexgrenzen im sichtbaren App-Loop.
    ///
    /// Verifies text, UTF-8, and hex boundaries in the visible app loop.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Renders_Bounded_Text_And_Hex_Previews()
    {
        string textRoot = CreateWorkspace();
        string hexRoot = CreateWorkspace();
        try
        {
            File.WriteAllText(
                Path.Combine(textRoot, "docs", "large.txt"),
                new string('x', ControlledFileWorkspace.PreviewByteLimit + 20));
            using ControlledFileWorkspace textWorkspace = new(textRoot);
            Wave6PreviewResult truncated = textWorkspace.PreviewText("docs/large.txt");
            Assert.IsTrue(truncated.Truncated);

            using ControlledFileWorkspace hexWorkspace = new(hexRoot);
            Tp7FileManagerApp app = new(hexWorkspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x50, 0x5000, 0, 0x50)),
                TEvent.CreateCommand(Tp7FileManagerApp.CmPreviewText),
                TEvent.CreateCommand(Tp7FileManagerApp.CmPreviewHex)
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.AreEqual(Wave6ViewerDecision.Hex, app.LastPreview?.Decision);
            Assert.AreEqual("docs/sample.bin", app.LastPreview?.RelativePath);
            Assert.IsTrue(hexWorkspace.PreviewText("docs/sample.bin").InvalidUtf8);
            AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "docs/sample.bin", "hex preview status");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "00000000", "hex offset");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, ".A.", "hex printable region");
        }
        finally
        {
            Directory.Delete(textRoot, recursive: true);
            Directory.Delete(hexRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Filter, Sortierung, Tags und leere Ergebnisse.
    ///
    /// Verifies filtering, sorting, tags, and empty results.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Renders_Filter_Sort_Tag_And_Empty_State()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Assert.HasCount(0, workspace.List("docs", "*.missing").Entries);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateCommand(Tp7FileManagerApp.CmFilterText),
                TEvent.CreateCommand(Tp7FileManagerApp.CmSortSize),
                TEvent.CreateCommand(Tp7FileManagerApp.CmTagFirst)
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.AreEqual("*.txt", app.CurrentSnapshot.Filter);
            Assert.AreEqual(Wave6Sort.Size, app.CurrentSnapshot.Sort);
            CollectionAssert.Contains(app.TaggedPaths.ToArray(), "docs/lesson.txt");
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "*[F] lesson.txt", "visible tag marker");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Suche, Abbruch und Ergebnisgrenzen.
    ///
    /// Verifies search, cancellation, and result limits.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Renders_Search_Cancel_And_Limit_Boundaries()
    {
        string root = CreateWorkspace();
        try
        {
            for (int index = 0; index <= ControlledFileWorkspace.SearchResultLimit; index++)
            {
                File.WriteAllText(Path.Combine(root, $"match-{index:D3}.txt"), "match");
            }

            using ControlledFileWorkspace workspace = new(root);
            Wave6SearchResult limited = workspace.Search("", "*.txt");
            Assert.IsTrue(limited.LimitReached);
            Assert.HasCount(ControlledFileWorkspace.SearchResultLimit, limited.Matches);

            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents([TEvent.CreateCommand(Tp7FileManagerApp.CmSearchText, "cancel")]);
            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsTrue(app.LastSearch?.Canceled);
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "search canceled", "search cancellation");
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft internen Viewer-Fallback und geschlossene Paletten.
    ///
    /// Verifies internal-viewer fallback and closed palettes.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Renders_Association_And_Palette_Fallbacks()
    {
        string associationRoot = CreateWorkspace();
        string paletteRoot = CreateWorkspace();
        try
        {
            File.WriteAllText(Path.Combine(associationRoot, "docs", "unknown.xyz"), "unknown");
            using ControlledFileWorkspace associationWorkspace = new(associationRoot);
            Tp7FileManagerApp associationApp = new(associationWorkspace, DefaultBounds(), headless: true);
            associationApp.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmNavigateFirstDirectory),
                TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x50, 0x5000, 0, 0x50)),
                TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x50, 0x5000, 0, 0x50)),
                TEvent.CreateCommand(Tp7FileManagerApp.CmPreviewAssociated)
            ]);
            AssertSmokeRunCompletes(() => associationApp.Run());
            AssertRenderedContainsFromAppLoop(associationApp.Driver.BackBuffer, "fallback", "association fallback");

            using ControlledFileWorkspace paletteWorkspace = new(paletteRoot);
            Tp7FileManagerApp paletteApp = new(paletteWorkspace, DefaultBounds(), headless: true);
            paletteApp.QueueEvents([TEvent.CreateCommand(Tp7FileManagerApp.CmChangePalette, "unknown-palette")]);
            AssertSmokeRunCompletes(() => paletteApp.Run());
            Assert.AreEqual(Wave6Palette.Default, paletteApp.Palette);
            AssertRenderedContainsFromAppLoop(paletteApp.Driver.BackBuffer, "fallback=Default", "palette fallback");
            CollectionAssert.AreEqual(
                new[] { "Default", "Cyan", "Rose", "HighContrast" },
                Enum.GetNames<Wave6Palette>());
        }
        finally
        {
            Directory.Delete(associationRoot, recursive: true);
            Directory.Delete(paletteRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Copy-Dialog, Enter-Bestätigung und Escape-Abbruch.
    ///
    /// Verifies the copy dialog, Enter confirmation, and Escape cancellation.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Copy_Dialog_Confirms_Or_Cancels_Explicitly()
    {
        string confirmRoot = CreateWorkspace();
        string cancelRoot = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace confirmWorkspace = new(confirmRoot);
            Tp7FileManagerApp confirmApp = new(confirmWorkspace, DefaultBounds(), headless: true);
            confirmApp.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:copy:confirmed.txt"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\r', 0x1C, 0x1C0D, 0, 0x1C))
            ]);
            AssertSmokeRunCompletes(() => confirmApp.Run());

            Assert.AreEqual(Wave6OperationState.Completed, confirmApp.LastOperation?.State);
            Assert.IsTrue(File.Exists(Path.Combine(confirmRoot, "confirmed.txt")));
            Assert.AreEqual(nameof(TInputLine), confirmApp.LastDialogInitialFocusKind);
            Assert.AreEqual(ShellCommandIds.cmOK, confirmApp.LastDialogTerminalCommand);
            StringAssert.Contains(confirmApp.LastDialogPreview, "README.txt -> confirmed.txt");
            AssertRenderedContainsFromAppLoop(confirmApp.Driver.BackBuffer, "Completed", "copy dialog result");

            using ControlledFileWorkspace cancelWorkspace = new(cancelRoot);
            Tp7FileManagerApp cancelApp = new(cancelWorkspace, DefaultBounds(), headless: true);
            cancelApp.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:copy:canceled.txt"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\x1b', 0x01, 0x011B, 0, 0x01))
            ]);
            AssertSmokeRunCompletes(() => cancelApp.Run());

            Assert.IsNull(cancelApp.PendingOperation);
            Assert.IsNull(cancelApp.LastOperation);
            Assert.AreEqual(ShellCommandIds.cmCancel, cancelApp.LastDialogTerminalCommand);
            Assert.IsFalse(File.Exists(Path.Combine(cancelRoot, "canceled.txt")));
            AssertRenderedContainsFromAppLoop(cancelApp.Driver.BackBuffer, "Canceled", "copy dialog cancel");
        }
        finally
        {
            Directory.Delete(confirmRoot, recursive: true);
            Directory.Delete(cancelRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Rename-, Delete- und Read-only-Dialoge.
    ///
    /// Verifies rename, delete, and read-only dialogs.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Rename_Delete_And_ReadOnly_Use_Real_Decisions()
    {
        string renameRoot = CreateWorkspace();
        string deleteRoot = CreateWorkspace();
        string readOnlyRoot = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace renameWorkspace = new(renameRoot);
            Tp7FileManagerApp renameApp = new(renameWorkspace, DefaultBounds(), headless: true);
            renameApp.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:rename:renamed.txt"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\r', 0x1C, 0x1C0D, 0, 0x1C))
            ]);
            AssertSmokeRunCompletes(() => renameApp.Run());
            Assert.IsFalse(File.Exists(Path.Combine(renameRoot, "README.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(renameRoot, "renamed.txt")));

            using ControlledFileWorkspace deleteWorkspace = new(deleteRoot);
            Tp7FileManagerApp deleteApp = new(deleteWorkspace, DefaultBounds(), headless: true);
            deleteApp.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:delete"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\r', 0x1C, 0x1C0D, 0, 0x1C))
            ]);
            AssertSmokeRunCompletes(() => deleteApp.Run());
            Assert.IsFalse(File.Exists(Path.Combine(deleteRoot, "README.txt")));

            using ControlledFileWorkspace readOnlyWorkspace = new(readOnlyRoot);
            Tp7FileManagerApp readOnlyApp = new(readOnlyWorkspace, DefaultBounds(), headless: true);
            readOnlyApp.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:readonly"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\r', 0x1C, 0x1C0D, 0, 0x1C))
            ]);
            AssertSmokeRunCompletes(() => readOnlyApp.Run());
            Assert.IsTrue(File.GetAttributes(Path.Combine(readOnlyRoot, "README.txt")).HasFlag(FileAttributes.ReadOnly));
        }
        finally
        {
            Directory.Delete(renameRoot, recursive: true);
            Directory.Delete(deleteRoot, recursive: true);
            File.SetAttributes(Path.Combine(readOnlyRoot, "README.txt"), FileAttributes.Normal);
            Directory.Delete(readOnlyRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft ungültige Ziele, Konflikte und NoMutation.
    ///
    /// Verifies invalid targets, conflicts, and NoMutation.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Dialog_Rejects_Unsafe_Or_Conflicting_Targets()
    {
        string root = CreateWorkspace();
        string outside = Path.Combine(Path.GetDirectoryName(root)!, "outside.txt");
        try
        {
            File.WriteAllText(Path.Combine(root, "exists.txt"), "exists");
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:copy:../outside.txt"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\r', 0x1C, 0x1C0D, 0, 0x1C)),
                TEvent.CreateKeyDown(new TKeyDownEvent('\x1b', 0x01, 0x011B, 0, 0x01))
            ]);
            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsNull(app.PendingOperation);
            Assert.IsNull(app.LastOperation);
            Assert.IsTrue(app.LastDialogValidationRejected);
            Assert.IsFalse(File.Exists(outside));
            AssertRenderedContainsFromAppLoop(app.Driver.BackBuffer, "Rejected", "unsafe target result");

            Assert.ThrowsExactly<IOException>(() =>
                workspace.PrepareOperation(Wave6OperationKind.Copy, "README.txt", "exists.txt"));
            Assert.ThrowsExactly<InvalidOperationException>(() =>
                workspace.PrepareOperation(Wave6OperationKind.Copy, "README.txt", "../outside.txt"));
        }
        finally
        {
            if (File.Exists(outside))
            {
                File.Delete(outside);
            }

            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Tab, Shift+Tab, F1 und Default-Enter im modalen Pfad.
    ///
    /// Verifies Tab, Shift+Tab, F1, and default Enter in the modal path.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Dialog_Proves_Focus_Order_Help_And_Default_Button()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateCommand(Tp7FileManagerApp.CmPrepareCopy, "dialog:copy:focused.txt"),
                TEvent.CreateKeyDown(new TKeyDownEvent('\t', 0x0F, 0x0F09, 0, 0x0F)),
                TEvent.CreateKeyDown(new TKeyDownEvent('\t', 0x0F, 0x0F09, 0x0001, 0x0F)),
                TEvent.CreateKeyDown(new TKeyDownEvent('\0', 0x3B, 0x3B00, 0, 0x3B)),
                TEvent.CreateKeyDown(new TKeyDownEvent('\r', 0x1C, 0x1C0D, 0, 0x1C))
            ]);
            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsTrue(app.DescriptionOpened);
            Assert.AreEqual(nameof(TInputLine), app.LastDialogInitialFocusKind);
            Assert.AreEqual(ShellCommandIds.cmOK, app.LastDialogTerminalCommand);
            Assert.AreEqual(Wave6OperationState.Completed, app.LastOperation?.State);
            Assert.IsTrue(File.Exists(Path.Combine(root, "focused.txt")));
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft, dass eine vollständige Drag-Folge nur dieselbe bestätigungspflichtige Absicht wie die Tastatur vorbereitet.
    ///
    /// Verifies that a complete drag sequence only prepares the same confirmable intent as the keyboard.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Mouse_Release_Prepares_Keyboard_Equivalent_Without_Mutation()
    {
        string keyboardRoot = CreateWorkspace();
        string mouseRoot = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace keyboardWorkspace = new(keyboardRoot);
            using ControlledFileWorkspace mouseWorkspace = new(mouseRoot);
            Tp7FileManagerApp keyboard = new(keyboardWorkspace, DefaultBounds(), headless: true);
            Tp7FileManagerApp mouse = new(mouseWorkspace, DefaultBounds(), headless: true);
            keyboard.QueueEvents([TEvent.CreateCommand(Tp7FileManagerApp.CmKeyboardDropIntent)]);
            mouse.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4)),
                TEvent.CreateMouse(TEventKind.MouseMove, TMouseButtons.Left, false, new TPoint(8, 5)),
                TEvent.CreateMouse(TEventKind.MouseUp, TMouseButtons.None, false, new TPoint(8, 5))
            ]);

            AssertSmokeRunCompletes(() => keyboard.Run());
            AssertSmokeRunCompletes(() => mouse.Run());

            Assert.IsNotNull(keyboard.PendingOperation);
            Assert.IsNotNull(mouse.PendingOperation);
            Assert.AreEqual(keyboard.PendingOperation.Kind, mouse.PendingOperation.Kind);
            Assert.AreEqual(keyboard.PendingOperation.SourceRelativePath, mouse.PendingOperation.SourceRelativePath);
            Assert.AreEqual(keyboard.PendingOperation.TargetRelativePath, mouse.PendingOperation.TargetRelativePath);
            Assert.IsNull(mouse.LastOperation);
            Assert.IsFalse(File.Exists(Path.Combine(mouseRoot, "README.txt.copy")));
            Assert.IsFalse(mouse.MouseDragActive);
            Assert.IsTrue(mouse.MouseReleasePrepared);
            AssertVisibleContainsFromAppLoop(mouse.LastStatusMessage, "release", "mouse release authority boundary");
        }
        finally
        {
            Directory.Delete(keyboardRoot, recursive: true);
            Directory.Delete(mouseRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft, dass ungültige Mausziele und nicht unterstützte Tasten keine Absicht behalten.
    ///
    /// Verifies that invalid mouse targets and unsupported buttons retain no intent.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Mouse_Rejects_Outside_Regions_And_Unsupported_Buttons()
    {
        string outsideRoot = CreateWorkspace();
        string buttonRoot = CreateWorkspace();
        string sourceRoot = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace outsideWorkspace = new(outsideRoot);
            Tp7FileManagerApp outside = new(outsideWorkspace, DefaultBounds(), headless: true);
            outside.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4)),
                TEvent.CreateMouse(TEventKind.MouseMove, TMouseButtons.Left, false, new TPoint(99, 30)),
                TEvent.CreateMouse(TEventKind.MouseUp, TMouseButtons.None, false, new TPoint(99, 30))
            ]);
            AssertSmokeRunCompletes(() => outside.Run());
            Assert.IsNull(outside.PendingOperation);
            Assert.IsNull(outside.LastOperation);
            Assert.AreEqual("invalid-target", outside.LastMouseCancellationReason);
            Assert.IsFalse(File.Exists(Path.Combine(outsideRoot, "README.txt.copy")));

            using ControlledFileWorkspace buttonWorkspace = new(buttonRoot);
            Tp7FileManagerApp button = new(buttonWorkspace, DefaultBounds(), headless: true);
            button.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Right, false, new TPoint(4, 4))
            ]);
            AssertSmokeRunCompletes(() => button.Run());
            Assert.IsNull(button.PendingOperation);
            Assert.IsNull(button.LastOperation);

            using ControlledFileWorkspace sourceWorkspace = new(sourceRoot);
            Tp7FileManagerApp source = new(sourceWorkspace, DefaultBounds(), headless: true);
            File.Delete(Path.Combine(sourceRoot, "README.txt"));
            source.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4))
            ]);
            AssertSmokeRunCompletes(() => source.Run());
            Assert.IsNull(source.PendingOperation);
            Assert.IsNull(source.LastOperation);
            Assert.AreEqual("invalid-source", source.LastMouseCancellationReason);
        }
        finally
        {
            Directory.Delete(outsideRoot, recursive: true);
            Directory.Delete(buttonRoot, recursive: true);
            Directory.Delete(sourceRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft Escape, Capability-Verlust und entfernte Ziel-Views als sichere Abbruchgrenzen.
    ///
    /// Verifies Escape, capability loss, and removed target views as safe cancellation boundaries.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_AppLoop_Mouse_Cancels_On_Escape_Capability_Loss_And_View_Removal()
    {
        string escapeRoot = CreateWorkspace();
        string capabilityRoot = CreateWorkspace();
        string removalRoot = CreateWorkspace();
        string shutdownRoot = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace escapeWorkspace = new(escapeRoot);
            Tp7FileManagerApp escape = new(escapeWorkspace, DefaultBounds(), headless: true);
            escape.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4)),
                TEvent.CreateKeyDown(new TKeyDownEvent('\x1b', 0x01, 0x011B, 0, 0x01))
            ]);
            AssertSmokeRunCompletes(() => escape.Run());
            Assert.IsNull(escape.PendingOperation);
            Assert.AreEqual("escape", escape.LastMouseCancellationReason);

            using ControlledFileWorkspace capabilityWorkspace = new(capabilityRoot);
            Tp7FileManagerApp capability = new(capabilityWorkspace, DefaultBounds(), headless: true);
            capability.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4)),
                TEvent.CreateBroadcast(
                    ShellCommandIds.cmMouseCapabilityChanged,
                    ConsoleMouseCapabilityState.Disabled)
            ]);
            AssertSmokeRunCompletes(() => capability.Run());
            Assert.IsNull(capability.PendingOperation);
            Assert.AreEqual("capability-loss", capability.LastMouseCancellationReason);

            using ControlledFileWorkspace removalWorkspace = new(removalRoot);
            Tp7FileManagerApp removal = new(removalWorkspace, DefaultBounds(), headless: true);
            Assert.IsNotNull(removal.MainWindow);
            removal.Desktop?.Remove(removal.MainWindow);
            removal.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4))
            ]);
            AssertSmokeRunCompletes(() => removal.Run());
            Assert.IsNull(removal.PendingOperation);
            Assert.IsNull(removal.LastOperation);

            using ControlledFileWorkspace shutdownWorkspace = new(shutdownRoot);
            Tp7FileManagerApp shutdown = new(shutdownWorkspace, DefaultBounds(), headless: true);
            shutdown.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4))
            ]);
            AssertSmokeRunCompletes(() => shutdown.Run());
            Assert.IsFalse(shutdown.MouseDragActive);
            Assert.AreEqual("shutdown", shutdown.LastMouseCancellationReason);
            Assert.IsNotNull(shutdown.PendingOperation);
            Assert.IsNull(shutdown.LastOperation);
            Assert.IsFalse(File.Exists(Path.Combine(shutdownRoot, "README.txt.copy")));
        }
        finally
        {
            Directory.Delete(escapeRoot, recursive: true);
            Directory.Delete(capabilityRoot, recursive: true);
            Directory.Delete(removalRoot, recursive: true);
            Directory.Delete(shutdownRoot, recursive: true);
        }
    }

    /// <summary>
    /// Prüft denselben begrenzten Mauspfad im 48-mal-16-Layout.
    ///
    /// Verifies the same bounded mouse path in the 48-by-16 layout.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Constrained_AppLoop_Mouse_Target_Remains_Inside_And_NonMutating()
    {
        string root = CreateWorkspace();
        try
        {
            using ControlledFileWorkspace workspace = new(root);
            Tp7FileManagerApp app = new(workspace, DefaultBounds(48, 16), headless: true);
            app.QueueEvents(
            [
                TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(4, 4)),
                TEvent.CreateMouse(TEventKind.MouseMove, TMouseButtons.Left, false, new TPoint(8, 5)),
                TEvent.CreateMouse(TEventKind.MouseUp, TMouseButtons.None, false, new TPoint(8, 5))
            ]);

            AssertSmokeRunCompletes(() => app.Run());

            Assert.IsNotNull(app.PendingOperation);
            Assert.IsNull(app.LastOperation);
            Assert.IsTrue(app.MouseReleasePrepared);
            Assert.IsFalse(File.Exists(Path.Combine(root, "README.txt.copy")));
            Assert.IsTrue(app.LastVisibleRegion.Contains(new TPoint(8, 5)));
            Assert.IsLessThanOrEqualTo(48, app.LastVisibleRegion.B.X);
            Assert.IsLessThanOrEqualTo(15, app.LastVisibleRegion.B.Y);
        }
        finally
        {
            Directory.Delete(root, recursive: true);
        }
    }

    /// <summary>
    /// Prüft exakt zehn Showcase-Bereiche, einen Abschluss und 24 unveränderte Quellen.
    ///
    /// Verifies exactly ten showcase areas, one closure row, and 24 unchanged sources.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Evidence_Has_Exact_Area_Entry_And_Source_Closure()
    {
        string evidence = File.ReadAllText(ShowcaseEvidencePath());
        ValidateShowcaseRows(ParseTable(evidence, "## Showcase Area Evidence"));
        ValidateEntryRows(ParseTable(evidence, "## Entry-Point Decision"));
        ValidateHistoricalSourceRows(ParseTable(evidence, "## Feature-035 Historical Baseline"));
    }

    /// <summary>
    /// Prüft fehlende, doppelte, unbekannte, vertauschte und fehlerhafte Evidence-Zeilen.
    ///
    /// Verifies missing, duplicate, unknown, reordered, and malformed evidence rows.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Evidence_Rejects_Cardinality_And_Order_Drift()
    {
        string[][] valid = CreateValidShowcaseRows();

        Assert.ThrowsExactly<InvalidDataException>(() => ValidateShowcaseRows(valid[..^1]));
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateShowcaseRows([.. valid[..^1], valid[0]]));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows([.. valid[..^1], ["W6S-999", .. valid[^1][1..]]]));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows([valid[1], valid[0], .. valid[2..]]));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows([valid[0][..^1], .. valid[1..]]));
    }

    /// <summary>
    /// Prüft Entscheidungen, Pflichtfelder, offene Deltas und Follow-up-Ownership.
    ///
    /// Verifies decisions, required fields, open deltas, and follow-up ownership.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Evidence_Rejects_Open_Or_Inconsistent_Closure()
    {
        string[][] valid = CreateValidShowcaseRows();

        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 10, "UnknownDecision")));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 4, "Planned")));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 5, "Open")));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 4, string.Empty)));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 4, "ShowcaseDelta unresolved")));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 11, string.Empty)));
        Assert.ThrowsExactly<InvalidDataException>(() =>
            ValidateShowcaseRows(ReplaceCell(valid, 0, 10, "FollowUpHardening")));
        string[] openEntry = CreateValidEntryRow();
        openEntry[5] = "ShowcaseDelta";
        string[] blockedEntry = CreateValidEntryRow();
        blockedEntry[5] = "ProductDecision";
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateEntryRows([openEntry]));
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateEntryRows([blockedEntry]));
    }

    /// <summary>
    /// Prüft Quellhash- und geschützte Pfaddrift fail-closed.
    ///
    /// Verifies source-hash and protected-path drift fail closed.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Evidence_Rejects_Historical_Source_Drift()
    {
        string[][] sources = ParseTable(
            File.ReadAllText(ShowcaseEvidencePath()),
            "## Feature-035 Historical Baseline");
        string[][] changedHash = sources.Select(row => row.ToArray()).ToArray();
        changedHash[0][1] = "`" + new string('0', 64) + "`";
        string[][] changedPath = sources.Select(row => row.ToArray()).ToArray();
        changedPath[0][0] = "`TVFM/UNKNOWN.PAS`";

        Assert.ThrowsExactly<InvalidDataException>(() => ValidateHistoricalSourceRows(changedHash));
        Assert.ThrowsExactly<InvalidDataException>(() => ValidateHistoricalSourceRows(changedPath));
    }

    /// <summary>
    /// Prüft plattformneutrale Text-Hashes bei unveränderten binären Quellen.
    ///
    /// Verifies platform-neutral text hashes while binary sources remain exact.
    /// </summary>
    [TestMethod]
    public void Wave6Showcase_Historical_Text_Hashes_Are_LineEndingNeutral_But_Binary_Hashes_Are_Exact()
    {
        byte[] lf = System.Text.Encoding.ASCII.GetBytes("first\nsecond\n");
        byte[] crlf = System.Text.Encoding.ASCII.GetBytes("first\r\nsecond\r\n");

        Assert.AreEqual(
            HashHistoricalSource("TVFM/ASSOC.PAS", lf),
            HashHistoricalSource("TVFM/ASSOC.PAS", crlf));
        Assert.AreNotEqual(
            HashHistoricalSource("TVFM/CYAN.PAL", lf),
            HashHistoricalSource("TVFM/CYAN.PAL", crlf));
    }

    private static IEnumerable<TMenuItem> EnumerateSiblings(TMenuItem? first)
    {
        for (TMenuItem? item = first; item is not null; item = item.Next)
        {
            yield return item;
        }
    }

    private static IEnumerable<TMenuItem> EnumerateTree(TMenuItem? first)
    {
        foreach (TMenuItem item in EnumerateSiblings(first))
        {
            yield return item;
            foreach (TMenuItem child in EnumerateTree(item.SubMenu))
            {
                yield return child;
            }
        }
    }

    private static string StripMarkers(string value) => value.Replace("~", string.Empty, StringComparison.Ordinal);

    private static readonly string[] ShowcaseAreaIds =
        Enumerable.Range(1, 10).Select(index => $"W6S-{index:D3}").ToArray();

    private static void ValidateShowcaseRows(string[][] rows)
    {
        ValidateOrderedRows(rows, ShowcaseAreaIds, 12);
        foreach (string[] row in rows)
        {
            string decision = Unquote(row[10]);
            if (row.Skip(1).Any(IsIncompleteCell)
                || row.Skip(1).Any(cell => cell.Contains("ShowcaseDelta", StringComparison.Ordinal))
                || decision is not ("UseExistingFramework"
                    or "SmallFrameworkFix"
                    or "IntentionalDeviation"
                    or "FollowUpHardening"))
            {
                throw new InvalidDataException("A showcase row is incomplete or inconsistent.");
            }

            if (decision == "FollowUpHardening"
                && (!row[11].Contains("Owner=", StringComparison.Ordinal)
                    || !row[11].Contains("Evidence=", StringComparison.Ordinal)))
            {
                throw new InvalidDataException("FollowUpHardening requires owner and evidence.");
            }
        }
    }

    private static void ValidateEntryRows(string[][] rows)
    {
        ValidateOrderedRows(rows, ["Tp7FileManager"], 7);
        string[] row = rows[0];
        string decision = Unquote(row[5]);
        if (row.Skip(1).Any(IsIncompleteCell)
            || decision is not ("ShowcaseComplete" or "IntentionalMinimalSurface")
            || row.Any(cell => cell.Contains("ShowcaseDelta", StringComparison.Ordinal)))
        {
            throw new InvalidDataException("The entry-point closure is incomplete or blocking.");
        }
    }

    private static void ValidateHistoricalSourceRows(string[][] rows)
    {
        string repositoryRoot = ShowcaseRepositoryRoot();
        string[] expected = Directory.GetFiles(Path.Combine(repositoryRoot, "TVFM"), "*", SearchOption.AllDirectories)
            .Select(path => Path.GetRelativePath(repositoryRoot, path).Replace('\\', '/'))
            .Order(StringComparer.Ordinal)
            .ToArray();
        ValidateOrderedRows(rows, expected, 2);
        foreach (string[] row in rows)
        {
            string relativePath = Unquote(row[0]);
            string expectedHash = Unquote(row[1]);
            byte[] content = File.ReadAllBytes(Path.Combine(repositoryRoot, relativePath));
            string actualHash = HashHistoricalSource(relativePath, content);
            if (!actualHash.Equals(expectedHash, StringComparison.Ordinal))
            {
                throw new InvalidDataException($"Historical source hash drift: {relativePath}");
            }
        }
    }

    private static string HashHistoricalSource(string relativePath, byte[] content)
    {
        string extension = Path.GetExtension(relativePath);
        byte[] canonical = extension.Equals(".PAS", StringComparison.OrdinalIgnoreCase)
            || extension.Equals(".BAT", StringComparison.OrdinalIgnoreCase)
                ? NormalizeHistoricalTextBytes(content)
                : content;
        return Convert.ToHexString(SHA256.HashData(canonical)).ToLowerInvariant();
    }

    private static byte[] NormalizeHistoricalTextBytes(byte[] content)
    {
        List<byte> normalized = new(content.Length);
        for (int index = 0; index < content.Length; index++)
        {
            if (content[index] != (byte)'\r')
            {
                normalized.Add(content[index]);
                continue;
            }

            // Git darf historische Textquellen je Plattform als CRLF auschecken; der Inhaltsbeweis bleibt LF-kanonisch.
            // Git may check out historical text sources as CRLF per platform; the content proof remains LF-canonical.
            if (index + 1 < content.Length && content[index + 1] == (byte)'\n')
            {
                index++;
            }

            normalized.Add((byte)'\n');
        }

        return normalized.ToArray();
    }

    private static void ValidateOrderedRows(string[][] rows, string[] expectedIds, int exactColumns)
    {
        if (rows.Length != expectedIds.Length || rows.Any(row => row.Length != exactColumns))
        {
            throw new InvalidDataException("Evidence cardinality or column count differs.");
        }

        string[] actual = rows.Select(row => Unquote(row[0])).ToArray();
        if (actual.Distinct(StringComparer.Ordinal).Count() != actual.Length
            || !actual.SequenceEqual(expectedIds, StringComparer.Ordinal))
        {
            throw new InvalidDataException("Evidence identifiers are missing, duplicated, unknown, or out of order.");
        }
    }

    private static bool IsIncompleteCell(string value) =>
        string.IsNullOrWhiteSpace(value)
        || value.Equals("Open", StringComparison.OrdinalIgnoreCase)
        || value.Contains("Planned", StringComparison.OrdinalIgnoreCase);

    private static string[][] ParseTable(string document, string heading)
    {
        string normalized = document.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
        int start = normalized.IndexOf(heading, StringComparison.Ordinal);
        if (start < 0)
        {
            throw new InvalidDataException($"Missing evidence section: {heading}");
        }

        int end = normalized.IndexOf("\n## ", start + heading.Length, StringComparison.Ordinal);
        string section = end < 0 ? normalized[start..] : normalized[start..end];
        return section
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(line => line.StartsWith('|') && !line.Contains("---", StringComparison.Ordinal))
            .Skip(1)
            .Select(line => line.Trim('|').Split('|').Select(cell => cell.Trim()).ToArray())
            .ToArray();
    }

    private static string[][] CreateValidShowcaseRows() =>
        ShowcaseAreaIds.Select(id =>
            new[]
            {
                id,
                "scope",
                "feature proof",
                "visible access",
                "normal proof",
                "focus proof",
                "framework contracts",
                "local composition",
                "historical intent",
                "security boundary",
                "UseExistingFramework",
                "residual risk and re-evaluation"
            }).ToArray();

    private static string[] CreateValidEntryRow() =>
    [
        "Tp7FileManager",
        "state proof",
        "view proof",
        "cell proof",
        "app-loop proof",
        "ShowcaseComplete",
        "residual risk and re-evaluation"
    ];

    private static string[][] ReplaceCell(string[][] source, int row, int column, string value)
    {
        string[][] copy = source.Select(item => item.ToArray()).ToArray();
        copy[row][column] = value;
        return copy;
    }

    private static string Unquote(string value) =>
        value.Length >= 2 && value[0] == '`' && value[^1] == '`' ? value[1..^1] : value;

    private static string ShowcaseEvidencePath() =>
        Path.Combine(
            ShowcaseRepositoryRoot(),
            "specs",
            "036-wave6-tvfm-showcase-remediation",
            "pr-evidence.md");

    private static string ShowcaseRepositoryRoot()
    {
        DirectoryInfo? current = new(AppContext.BaseDirectory);
        while (current is not null && !File.Exists(Path.Combine(current.FullName, "TuiVision.sln")))
        {
            current = current.Parent;
        }

        return current?.FullName
            ?? throw new DirectoryNotFoundException("TuiVision repository root was not found.");
    }

    private static string CreateWorkspace()
    {
        string root = Path.Combine(Path.GetTempPath(), "tuivision-wave6-showcase-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        Directory.CreateDirectory(Path.Combine(root, "docs"));
        File.WriteAllText(Path.Combine(root, "README.txt"), "root lesson");
        File.WriteAllText(Path.Combine(root, "docs", "lesson.txt"), "lesson line");
        File.WriteAllBytes(Path.Combine(root, "docs", "sample.bin"), [0x00, 0x41, 0xFF]);
        return root;
    }
}
