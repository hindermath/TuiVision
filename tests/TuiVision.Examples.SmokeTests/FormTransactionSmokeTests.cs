// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;
using TuiVision.Examples.FormTransaction;

namespace TuiVision.Examples.SmokeTests;

/// <summary>Prüft die sichtbaren Formtransaktionen über den echten App-Loop. / Verifies visible form transactions through the real app loop.</summary>
[TestClass]
public sealed class FormTransactionSmokeTests : ExampleTestBase
{
    /// <summary>Prüft Dirty, Change-Set, Persistenz und Accept. / Verifies dirty state, change set, persistence, and accept.</summary>
    [TestMethod]
    public void FormTransaction_AppLoop_PersistsThenAcceptsNestedForm()
    {
        FormTransactionApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            FormTransactionApp.CmEdit,
            FormTransactionApp.CmSubmitAccept).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertEqual(FormSubmitStatus.Success, app.LastSubmitStatus!.Value, "FormTransaction submit status");
        AssertEqual("Augusta", app.Model.Name, "FormTransaction bound name");
        AssertEqual("Paris", app.Model.Address.City, "FormTransaction nested city");
        AssertEqual(3, app.PersistedValues.Count, "FormTransaction persisted change count");
        Assert.IsFalse(app.Session.IsModified);
        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "FormTransaction main view");
        AssertRenderedRegionContainsFromAppLoop(
            app.Driver.BackBuffer,
            app.LastVisibleRegion,
            "Customer form transaction",
            "FormTransaction form cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "accepted", "FormTransaction accept status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft Validation und rekursives Reject. / Verifies validation and recursive reject.</summary>
    [TestMethod]
    public void FormTransaction_AppLoop_ValidatesAndRejectsWithoutModelMutation()
    {
        FormTransactionApp invalid = new(DefaultBounds(), headless: true);
        invalid.QueueEvents(InteractiveSmokeEventScript.Commands(FormTransactionApp.CmInvalid).Events);
        AssertSmokeRunCompletes(() => invalid.Run());
        AssertEqual(FormSubmitStatus.ValidationFailed, invalid.LastSubmitStatus!.Value, "FormTransaction invalid status");
        AssertEqual("Ada", invalid.Model.Name, "FormTransaction unchanged invalid model");
        AssertVisibleContainsFromAppLoop(invalid.LastStatusMessage, "ValidationFailed", "FormTransaction validation status");

        FormTransactionApp rejected = new(DefaultBounds(), headless: true);
        rejected.QueueEvents(InteractiveSmokeEventScript.Commands(
            FormTransactionApp.CmEdit,
            FormTransactionApp.CmReject).Events);
        AssertSmokeRunCompletes(() => rejected.Run());
        AssertEqual("Ada", rejected.NameInput.Data, "FormTransaction rejected input");
        AssertEqual("London", rejected.CityInput.Data, "FormTransaction rejected child input");
        Assert.IsFalse(rejected.Session.IsModified);
        AssertVisibleContainsFromAppLoop(rejected.LastStatusMessage, "rejected", "FormTransaction reject status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft Cancellation und veraltete Async-Ergebnisse. / Verifies cancellation and stale async results.</summary>
    [TestMethod]
    public void FormTransaction_AppLoop_ShowsCancellationAndStaleOutcome()
    {
        FormTransactionApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(
            FormTransactionApp.CmCancel,
            FormTransactionApp.CmStale).Events);

        AssertSmokeRunCompletes(() => app.Run());

        Assert.IsTrue(app.CancellationObserved);
        Assert.IsTrue(app.StaleObserved);
        AssertEqual(FormSubmitStatus.Stale, app.LastSubmitStatus!.Value, "FormTransaction stale status");
        AssertEqual("Ada", app.Model.Name, "FormTransaction stale model boundary");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Stale", "FormTransaction stale visible status");
        AssertPrimaryAssertionUsedAppLoop();
    }

    /// <summary>Prüft StatusLine und Help → Description. / Verifies StatusLine and Help → Description.</summary>
    [TestMethod]
    public void FormTransaction_AppLoop_ShowsDescription()
    {
        FormTransactionApp app = new(DefaultBounds(), headless: true);
        app.QueueEvents(InteractiveSmokeEventScript.Commands(FormTransactionApp.CmDescription).Events);

        AssertSmokeRunCompletes(() => app.Run());

        AssertViewTreeProofFromAppLoop(app.LastVisibleComponentKind, "TWindow", "FormTransaction description view");
        AssertRenderedRegionContainsFromAppLoop(
            app.Driver.BackBuffer,
            app.LastVisibleRegion,
            "FormTransaction description",
            "FormTransaction description cells");
        AssertVisibleContainsFromAppLoop(app.LastStatusMessage, "Help -> Description", "FormTransaction help status");
        AssertPrimaryAssertionUsedAppLoop();
    }
}
