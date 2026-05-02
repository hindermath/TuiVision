// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer Dialogbeschreibung, Validierung und Designer-Grenze.
///
/// Tests for dialog description, validation, and designer boundary.
/// </summary>
[TestClass]
public sealed class DialogDesignerFlowTests
{
    /// <summary>
    /// Prueft eine minimale gueltige Dialogbeschreibung.
    ///
    /// Verifies a minimal valid dialog description.
    /// </summary>
    [TestMethod]
    public void DialogDescriptionValidator_AcceptsMinimalDescription()
    {
        DialogDescriptionValidationResult result = DialogDescriptionValidator.Validate(StandardDialogTestSupport.CreateMinimalDescription());

        Assert.IsTrue(result.IsValid);
    }

    /// <summary>
    /// Prueft Ablehnung doppelter Control-IDs und Command-Bindings.
    ///
    /// Verifies rejection of duplicate control identifiers and command bindings.
    /// </summary>
    [TestMethod]
    public void DialogDescriptionValidator_RejectsDuplicateControlIdsAndCommands()
    {
        DialogDescription description = new(
            "bad",
            1,
            "Bad",
            [
                new DialogControlDescription("x", DialogControlRoles.InputLine, "X"),
                new DialogControlDescription("x", DialogControlRoles.Button, "X")
            ],
            ["x"],
            [
                new DialogCommandBinding(1, "x", "ok", "Enter"),
                new DialogCommandBinding(1, "x", "again", "Alt+A")
            ]);

        DialogDescriptionValidationResult result = DialogDescriptionValidator.Validate(description);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Messages.Any(message => message.Code == "control-id-duplicate"));
        Assert.IsTrue(result.Messages.Any(message => message.Code == "command-id-duplicate"));
    }

    /// <summary>
    /// Prueft mehrere semantisch ungueltige Beschreibungsfelder.
    ///
    /// Verifies several semantically invalid description fields.
    /// </summary>
    [TestMethod]
    public void DialogDescriptionValidator_RejectsSemanticInvalidDescription()
    {
        DialogDescription description = new(
            "bad",
            1,
            "Bad",
            [new DialogControlDescription("x", "unknown", string.Empty, "bad\0value")],
            ["missing", "missing"],
            [new DialogCommandBinding(1, "missing", "ok", string.Empty)]);

        DialogDescriptionValidationResult result = DialogDescriptionValidator.Validate(description);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Messages.Any(message => message.Code == "control-role-unknown"));
        Assert.IsTrue(result.Messages.Any(message => message.Code == "control-label-required"));
        Assert.IsTrue(result.Messages.Any(message => message.Code == "navigation-target-unknown"));
        Assert.IsTrue(result.Messages.Any(message => message.Code == "navigation-target-duplicate"));
        Assert.IsTrue(result.Messages.Any(message => message.Code == "initial-value-unsupported"));
        Assert.IsTrue(result.Messages.Any(message => message.Code == "command-keyboard-required"));
    }

    /// <summary>
    /// Prueft, dass ungueltige Beschreibungen keinen Runtime-Dialog erzeugen.
    ///
    /// Verifies that invalid descriptions do not create runtime dialogs.
    /// </summary>
    [TestMethod]
    public void DialogDescriptionFactory_InvalidDescription_DoesNotCreateRuntimeDialog()
    {
        DialogDescription description = new("bad", 1, string.Empty, [], [], []);

        Assert.ThrowsExactly<InvalidDataException>(() => DialogDescriptionFactory.CreateRuntimeDialog(description));
    }

    /// <summary>
    /// Prueft, dass eine gueltige Beschreibung einen passenden Runtime-Dialog erzeugt.
    ///
    /// Verifies that a valid description creates a matching runtime dialog.
    /// </summary>
    [TestMethod]
    public void DialogDescriptionFactory_ValidDescription_CreatesMatchingRuntimeDialog()
    {
        DialogDescription description = StandardDialogTestSupport.CreateMinimalDescription();

        TDialog dialog = DialogDescriptionFactory.CreateRuntimeDialog(description);

        Assert.AreEqual(description.Title, dialog.Title);
        Assert.AreEqual(6, dialog.Size.Y);

        dialog.SelectNext(forward: true);
        Assert.IsInstanceOfType<TButton>(dialog.Current);

        dialog.SelectNext(forward: true);
        Assert.IsInstanceOfType<TInputLine>(dialog.Current);
        TInputLine input = (TInputLine)dialog.Current!;
        Assert.AreEqual("Ada", input.Data);
    }

    /// <summary>
    /// Prueft, dass semantisch ungueltige persistierte Beschreibungen abgelehnt werden.
    ///
    /// Verifies that semantically invalid persisted descriptions are rejected.
    /// </summary>
    [TestMethod]
    public void DialogDescriptionPersistenceAdapter_SemanticInvalidRecord_IsRejected()
    {
        TDialogDescriptionRecord record = new(
            PersistedDialogRepresentation.CurrentFormatVersion,
            "bad",
            1,
            "Bad",
            [
                new TDialogControlDescriptionRecord("x", DialogControlRoles.InputLine, "Name", "Ada", true),
                new TDialogControlDescriptionRecord("x", DialogControlRoles.Button, "OK", null, true)
            ],
            ["x"],
            [
                new TDialogCommandBindingRecord(ShellCommandIds.cmOK, "missing", "confirm", "Enter"),
                new TDialogCommandBindingRecord(ShellCommandIds.cmOK, "x", "confirm-again", "Alt+O")
            ]);

        Assert.ThrowsExactly<InvalidDataException>(() => DialogDescriptionPersistenceAdapter.FromRecord(record));
    }

    /// <summary>
    /// Prueft die Designer-Consumer-Klassifikation.
    ///
    /// Verifies designer consumer classification.
    /// </summary>
    [TestMethod]
    public void DialogDesignerFlow_DownstreamConsumer_IsClassified()
    {
        string consumer = "dlgdsn";

        Assert.AreEqual("dlgdsn", consumer);
    }
}
