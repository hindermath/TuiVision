// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests für die gemeinsame Cluster-Basis.
///
/// Tests for the shared cluster base class.
/// </summary>
[TestClass]
public sealed class TClusterTests
{
    /// <summary>
    /// Prüft, dass Pfeilnavigation den markierten Eintrag bewegt.
    ///
    /// Verifies that arrow navigation moves the highlighted item.
    /// </summary>
    [TestMethod]
    public void TCluster_HandleEvent_ArrowNavigationUpdatesSelectionCursor()
    {
        TrackingCluster cluster = new(new TRect(0, 0, 12, 3), ["One", "Two", "Three"]);

        cluster.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x50));

        Assert.AreEqual(1, cluster.Sel);
    }

    /// <summary>
    /// Prüft, dass Leertaste die Press-Logik des aktuellen Eintrags auslöst.
    ///
    /// Verifies that the space key triggers the press logic of the current item.
    /// </summary>
    [TestMethod]
    public void TCluster_HandleEvent_SpaceTriggersPressForCurrentItem()
    {
        TrackingCluster cluster = new(new TRect(0, 0, 12, 3), ["One", "Two", "Three"]);
        cluster.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x50));

        cluster.HandleEvent(ControlEventFactory.CreateKeyDown(charCode: ' '));

        Assert.AreEqual(1, cluster.LastPressed);
    }

    /// <summary>
    /// Prüft, dass deaktivierte Cluster-Eingaben ignorieren.
    ///
    /// Verifies that disabled clusters ignore input.
    /// </summary>
    [TestMethod]
    public void TCluster_HandleEvent_DisabledClusterIgnoresInput()
    {
        TrackingCluster cluster = new(new TRect(0, 0, 12, 3), ["One", "Two", "Three"]);
        cluster.SetState(TViewState.Disabled, true);

        cluster.HandleEvent(ControlEventFactory.CreateKeyDown(scanCode: 0x50));

        Assert.AreEqual(0, cluster.Sel);
    }

    /// <summary>
    /// Kleine Test-Unterklasse zum Beobachten von <see cref="TCluster.Press(int)"/>.
    ///
    /// Small test subclass used to observe <see cref="TCluster.Press(int)"/>.
    /// </summary>
    private sealed class TrackingCluster : TCluster
    {
        /// <summary>
        /// Erstellt ein Tracking-Cluster.
        ///
        /// Creates a tracking cluster.
        /// </summary>
        /// <param name="bounds">Die Bounds des Controls. / The control bounds.</param>
        /// <param name="strings">Die Item-Beschriftungen. / The item labels.</param>
        public TrackingCluster(TRect bounds, string[] strings) : base(bounds, strings)
        {
            LastPressed = -1;
        }

        /// <summary>
        /// Der zuletzt gedrückte Index.
        ///
        /// The last pressed index.
        /// </summary>
        public int LastPressed { get; private set; }

        /// <summary>
        /// Für den Test werden keine persistierten Markierungen benötigt.
        ///
        /// No persisted marks are needed for this test.
        /// </summary>
        /// <param name="item">Der Item-Index. / The item index.</param>
        /// <returns>Immer <c>false</c>. / Always <c>false</c>.</returns>
        protected override bool Mark(int item) => false;

        /// <summary>
        /// Speichert den zuletzt gedrückten Index.
        ///
        /// Stores the last pressed index.
        /// </summary>
        /// <param name="item">Der Item-Index. / The item index.</param>
        protected override void Press(int item) => LastPressed = item;
    }
}
