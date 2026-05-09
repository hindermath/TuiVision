// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Examples.DlgDsn;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Smoke-Tests fuer den dynamischen Dialog-Designer.
///
/// Smoke tests for the dynamic dialog designer.
/// </summary>
[TestClass]
public sealed class DlgDsnSmokeTests : ExampleTestBase
{
    /// <summary>
    /// Prueft Erzeugen, Laden, Rendern und Aendern einer gueltigen Beschreibung.
    ///
    /// Verifies creating, loading, rendering, and modifying a valid description.
    /// </summary>
    [TestMethod]
    public void DlgDsn_Loads_Renders_And_Modifies_Valid_Description()
    {
        DlgDsnApp app = new(DefaultBounds(), headless: true);
        AssertSmokeRunCompletes(() => app.Run());

        DialogDescription created = app.CreateValidDescription();
        DialogDescription loaded = app.LoadFixture("valid.tvdialog");
        string rendered = app.RenderDescription(loaded);
        DialogDescription modified = app.ApplySimpleChange(created, "Grace");

        AssertEqual("wave2-dialog", created.DescriptionId, "DlgDsn created id");
        AssertVisibleContains(rendered, "Runtime dialog", "DlgDsn render state");
        AssertEqual("Grace", modified.Controls[0].InitialValue, "DlgDsn modified value");
    }

    /// <summary>
    /// Prueft sichtbare Ablehnung fehlerhafter Beschreibungen.
    ///
    /// Verifies visible rejection of invalid descriptions.
    /// </summary>
    [TestMethod]
    public void DlgDsn_Rejects_Malformed_Incomplete_Duplicate_And_InvalidNavigation()
    {
        DlgDsnApp app = new(DefaultBounds(), headless: true);

        AssertVisibleContains(app.TryLoadFixture("malformed.tvdialog"), "malformed", "DlgDsn malformed rejection");
        AssertVisibleContains(app.TryLoadFixture("incomplete.tvdialog"), "incomplete", "DlgDsn incomplete rejection");
        AssertVisibleContains(app.TryLoadFixture("duplicate-control.tvdialog"), "duplicate-control", "DlgDsn duplicate rejection");
        AssertVisibleContains(app.TryLoadFixture("invalid-navigation.tvdialog"), "invalid-navigation", "DlgDsn navigation rejection");
    }

    /// <summary>
    /// Prueft diagnostische Ablehnung fuer malformed und unsichere Fixture-Namen.
    ///
    /// Verifies diagnostic rejection for malformed and unsafe fixture names.
    /// </summary>
    [TestMethod]
    public void DlgDsn_InvalidFixtures_AreDiagnosticAndPathConstrained()
    {
        DlgDsnApp app = new(DefaultBounds(), headless: true);

        AssertEqual("dlgdsn: rejected malformed", app.TryLoadFixture("malformed.tvdialog"), "DlgDsn malformed diagnostic");
        AssertEqual("dlgdsn: rejected fixture-name", app.TryLoadFixture("../valid.tvdialog"), "DlgDsn traversal rejection");
        Assert.ThrowsExactly<InvalidDataException>(() => app.LoadFixture("../valid.tvdialog"));
    }
}
