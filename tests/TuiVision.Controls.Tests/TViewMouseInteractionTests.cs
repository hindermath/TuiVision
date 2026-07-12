using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Prüft globale Mauskoordinaten, Topmost-Hit-Routing, Fokusbesitz und
/// Exactly-once-Aktivierung in verschachtelten View-Bäumen.
///
/// Verifies global mouse coordinates, topmost hit routing, focus ownership,
/// and exactly-once activation in nested view trees.
/// </summary>
[TestClass]
public sealed class TViewMouseInteractionTests
{
    /// <summary>
    /// Prüft die vollständige Owner-Kette für globale und lokale Koordinaten.
    ///
    /// Verifies the complete owner chain for global and local coordinates.
    /// </summary>
    [TestMethod]
    public void MakeGlobalAndLocal_NestedOwners_UseScreenCoordinates()
    {
        TGroup root = new(new TRect(0, 0, 80, 25));
        TGroup desktop = new(new TRect(0, 1, 80, 24));
        TGroup window = new(new TRect(3, 2, 23, 12));
        TView child = new(new TRect(2, 1, 8, 4));
        root.Insert(desktop);
        desktop.Insert(window);
        window.Insert(child);

        Assert.AreEqual(new TPoint(5, 4), child.MakeGlobal(new TPoint(0, 0)));
        Assert.AreEqual(new TPoint(0, 0), child.MakeLocal(new TPoint(5, 4)));
        Assert.IsTrue(child.MouseInView(new TPoint(6, 5)));
        Assert.IsFalse(child.MouseInView(new TPoint(4, 3)));
    }

    /// <summary>
    /// Prüft, dass nur der zuletzt eingefügte sichtbare Treffer das Ereignis
    /// erhält und vor der Behandlung fokussiert ist.
    ///
    /// Verifies that only the last inserted visible hit receives the event and
    /// is focused before handling.
    /// </summary>
    [TestMethod]
    public void HandleEvent_OverlappingViews_FocusesAndDispatchesOnlyTopmost()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        RecordingView bottom = new(new TRect(1, 1, 12, 6));
        RecordingView top = new(new TRect(1, 1, 12, 6));
        group.Insert(bottom);
        group.Insert(top);

        group.HandleEvent(MouseDown(3, 3));

        Assert.AreSame(top, group.Current);
        Assert.IsTrue(top.WasFocusedWhenHandled);
        Assert.AreEqual(1, top.MouseCount);
        Assert.AreEqual(0, bottom.MouseCount);
    }

    /// <summary>
    /// Prüft, dass ungeeignete obere Views den passenden darunterliegenden
    /// Treffer weder verdecken noch selbst fokussiert werden.
    ///
    /// Verifies that ineligible upper views neither hide an eligible lower hit
    /// nor receive focus themselves.
    /// </summary>
    [TestMethod]
    public void HandleEvent_IneligibleTopViews_SelectsEligibleVisibleTarget()
    {
        foreach (Action<RecordingView> makeIneligible in new Action<RecordingView>[]
                 {
                     view => view.SetState(TViewState.Visible, false),
                     view => view.SetState(TViewState.Disabled, true)
                 })
        {
            TGroup group = new(new TRect(0, 0, 30, 10));
            RecordingView eligible = new(new TRect(1, 1, 12, 6));
            RecordingView ineligible = new(new TRect(1, 1, 12, 6));
            makeIneligible(ineligible);
            group.Insert(eligible);
            group.Insert(ineligible);

            group.HandleEvent(MouseDown(3, 3));

            Assert.AreSame(eligible, group.Current);
            Assert.AreEqual(1, eligible.MouseCount);
            Assert.AreEqual(0, ineligible.MouseCount);
        }
    }

    /// <summary>
    /// Prüft, dass eine sichtbare nicht selektierbare obere View das Ereignis
    /// erhält, ohne Fokus zu beanspruchen oder die darunterliegende View zu aktivieren.
    ///
    /// Verifies that a visible non-selectable upper view receives the event
    /// without claiming focus or activating the lower view.
    /// </summary>
    [TestMethod]
    public void HandleEvent_NonSelectableTopView_ReceivesMouseWithoutFocus()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        RecordingView lower = new(new TRect(1, 1, 12, 6));
        RecordingView upper = new(new TRect(1, 1, 12, 6));
        upper.Options &= ~TViewOptions.Selectable;
        group.Insert(lower);
        group.Insert(upper);

        group.HandleEvent(MouseDown(3, 3));

        Assert.IsNull(group.Current);
        Assert.AreEqual(1, upper.MouseCount);
        Assert.IsFalse(upper.WasFocusedWhenHandled);
        Assert.AreEqual(0, lower.MouseCount);
    }

    /// <summary>
    /// Prüft, dass ein äußerer Klick keinen Fokus oder Command erzeugt.
    ///
    /// Verifies that an outside click produces neither focus nor a command.
    /// </summary>
    [TestMethod]
    public void HandleEvent_ClickOutsideTargets_DoesNotFocusOrActivate()
    {
        CommandRecordingGroup group = new(new TRect(0, 0, 30, 10));
        TButton button = new(new TRect(1, 1, 10, 3), "OK", 4242, TButtonFlags.bfNormal);
        group.Insert(button);

        group.HandleEvent(MouseDown(20, 8));

        Assert.IsNull(group.Current);
        Assert.AreEqual(0, group.CommandCount);
    }

    /// <summary>
    /// Prüft, dass derselbe MouseDown-Pfad den bestehenden Button-Command genau
    /// einmal auslöst.
    ///
    /// Verifies that the same mouse-down path invokes the existing button
    /// command exactly once.
    /// </summary>
    [TestMethod]
    public void HandleEvent_ButtonHit_FocusesAndActivatesExactlyOnce()
    {
        CommandRecordingGroup group = new(new TRect(0, 0, 30, 10));
        TButton button = new(new TRect(1, 1, 10, 3), "OK", 4242, TButtonFlags.bfNormal);
        group.Insert(button);

        TEvent @event = MouseDown(3, 2);
        group.HandleEvent(@event);

        Assert.AreSame(button, group.Current);
        Assert.AreEqual(1, group.CommandCount);
        Assert.AreEqual((ushort)4242, group.LastCommand);
        Assert.AreEqual(TEventKind.Nothing, @event.What);
    }

    private static TEvent MouseDown(int x, int y) =>
        TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint((short)x, (short)y));

    private sealed class RecordingView : TView
    {
        public RecordingView(TRect bounds) : base(bounds)
        {
            Options |= TViewOptions.Selectable;
        }

        public int MouseCount { get; private set; }

        public bool WasFocusedWhenHandled { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if ((@event.What & TEventKind.Mouse) != 0)
            {
                MouseCount++;
                WasFocusedWhenHandled = GetState(TViewState.Focused);
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }

    private sealed class CommandRecordingGroup : TGroup
    {
        public CommandRecordingGroup(TRect bounds) : base(bounds)
        {
        }

        public int CommandCount { get; private set; }

        public ushort LastCommand { get; private set; }

        public override void HandleEvent(TEvent @event)
        {
            if (@event.What == TEventKind.Command)
            {
                CommandCount++;
                LastCommand = @event.Message.Command;
                @event.Clear(this);
                return;
            }

            base.HandleEvent(@event);
        }
    }
}
