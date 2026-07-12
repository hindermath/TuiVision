using TuiVision.Core;

namespace TuiVision.Core.Tests;

/// <summary>
/// Prüft die öffentlichen, textbasierten A11Y-Core-Verträge.
///
/// Verifies the public, text-based accessibility core contracts.
/// </summary>
[TestClass]
public sealed class AccessibleContractsTests
{
    /// <summary>Prüft den opt-in Widget-Vertrag. / Verifies the opt-in widget contract.</summary>
    [TestMethod]
    public void AccessibleWidget_OptInContract_ExposesSemanticTextAndFocusCapability()
    {
        IAccessibleWidget widget = new TestWidget("Speichern / Save", "Speichert das Dokument. / Saves the document.", true);

        Assert.AreEqual("Speichern / Save", widget.AccessibleLabel);
        Assert.AreEqual("Speichert das Dokument. / Saves the document.", widget.AccessibleDescription);
        Assert.IsTrue(widget.CanReceiveFocus);
    }

    /// <summary>Prüft unveränderliche Shortcut-Werte. / Verifies immutable shortcut values.</summary>
    [TestMethod]
    public void AccessibleShortcut_ValidValues_AreImmutableAndQueryable()
    {
        TAccessibleShortcut shortcut = new(0x2D00, "Alt+X Beenden / Quit", 1, "StatusLine");

        Assert.AreEqual((ushort)0x2D00, shortcut.KeyCode);
        Assert.AreEqual("Alt+X Beenden / Quit", shortcut.DisplayText);
        Assert.AreEqual((ushort)1, shortcut.Command);
        Assert.AreEqual("StatusLine", shortcut.Source);
    }

    /// <summary>Prüft die Shortcut-Validierung. / Verifies shortcut validation.</summary>
    /// <param name="keyCode">Tastencode. / Key code.</param>
    /// <param name="displayText">Anzeigetext. / Display text.</param>
    /// <param name="command">Befehl. / Command.</param>
    /// <param name="source">Quelle. / Source.</param>
    [TestMethod]
    [DataRow((ushort)0, "Alt+X Beenden / Quit", (ushort)1, "StatusLine")]
    [DataRow((ushort)0x2D00, " ", (ushort)1, "StatusLine")]
    [DataRow((ushort)0x2D00, "Alt+X Beenden / Quit", (ushort)0, "StatusLine")]
    [DataRow((ushort)0x2D00, "Alt+X Beenden / Quit", (ushort)1, "")]
    public void AccessibleShortcut_InvalidValues_AreRejected(
        ushort keyCode,
        string displayText,
        ushort command,
        string source)
    {
        if (keyCode == 0 || command == 0)
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TAccessibleShortcut(keyCode, displayText, command, source));
        }
        else
        {
            Assert.ThrowsExactly<ArgumentException>(() => new TAccessibleShortcut(keyCode, displayText, command, source));
        }
    }

    /// <summary>Prüft die schreibgeschützte Abfrage. / Verifies the read-only query.</summary>
    [TestMethod]
    public void ShortcutProvider_Query_IsReadOnly()
    {
        IAccessibleShortcutProvider provider = new TestShortcutProvider();

        IReadOnlyList<TAccessibleShortcut> shortcuts = provider.GetAccessibleShortcuts();

        Assert.HasCount(1, shortcuts);
        Assert.AreEqual("Test", shortcuts[0].Source);
    }

    private sealed record TestWidget(
        string AccessibleLabel,
        string? AccessibleDescription,
        bool CanReceiveFocus) : IAccessibleWidget;

    private sealed class TestShortcutProvider : IAccessibleShortcutProvider
    {
        public IReadOnlyList<TAccessibleShortcut> GetAccessibleShortcuts() =>
            [new TAccessibleShortcut(1, "Testaktion / Test action", 2, "Test")];
    }
}
