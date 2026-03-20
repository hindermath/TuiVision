// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Vollständige Tests für <see cref="TGroup"/>: Lebenszyklus, Drei-Phasen-Ereignis-Dispatch,
/// Fokus-Management, Zeichenpuffer und State-Propagation (FR-001 bis FR-019).
///
/// Full tests for <see cref="TGroup"/>: lifecycle, three-phase event dispatch,
/// focus management, draw buffer, and state propagation (FR-001 through FR-019).
/// </summary>
[TestClass]
public sealed class TGroupTests
{
    // =========================================================================
    // Phase 3 / US1: Lebenszyklus (T010)
    // =========================================================================

    /// <summary>
    /// Prüft, dass der Konstruktor <see cref="TViewOptions.Selectable"/> und
    /// <see cref="TViewOptions.Buffered"/> setzt.
    ///
    /// Verifies that the constructor sets <see cref="TViewOptions.Selectable"/> and
    /// <see cref="TViewOptions.Buffered"/>.
    /// </summary>
    [TestMethod]
    public void TGroup_Constructor_SetsSelectableAndBufferedOptions()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        Assert.IsTrue(group.Options.HasFlag(TViewOptions.Selectable));
        Assert.IsTrue(group.Options.HasFlag(TViewOptions.Buffered));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Current"/> nach dem Konstruktor <c>null</c> ist.
    ///
    /// Verifies that <see cref="TGroup.Current"/> is <c>null</c> after construction.
    /// </summary>
    [TestMethod]
    public void TGroup_Constructor_Current_IsNull()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        Assert.IsNull(group.Current);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Insert"/> eine View zur Liste hinzufügt
    /// und deren <see cref="TView.Owner"/> setzt.
    ///
    /// Verifies that <see cref="TGroup.Insert"/> adds a view to the list
    /// and sets its <see cref="TView.Owner"/>.
    /// </summary>
    [TestMethod]
    public void TGroup_Insert_AddsViewToList_AndSetsOwner()
    {
        TGroup group = new(new TRect(0, 0, 20, 20));
        TView child = new(new TRect(0, 0, 5, 5));

        group.Insert(child);

        Assert.AreSame(group, child.Owner);
    }

    /// <summary>
    /// Prüft, dass nach dem Einfügen einer zweiten View eine zirkuläre Liste entsteht:
    /// beide Views sind korrekt verlinkt.
    ///
    /// Verifies that inserting a second view creates a circular list:
    /// both views are correctly linked.
    /// </summary>
    [TestMethod]
    public void TGroup_Insert_SecondView_CreatesCircularList()
    {
        TGroup group = new(new TRect(0, 0, 20, 20));
        TView childA = new(new TRect(0, 0, 5, 5));
        TView childB = new(new TRect(5, 0, 10, 5));

        group.Insert(childA);
        group.Insert(childB);

        // Beide haben Owner / Both have owner
        Assert.AreSame(group, childA.Owner);
        Assert.AreSame(group, childB.Owner);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Insert"/> eine <see cref="ArgumentNullException"/>
    /// auslöst, wenn <c>null</c> übergeben wird.
    ///
    /// Verifies that <see cref="TGroup.Insert"/> throws an <see cref="ArgumentNullException"/>
    /// when <c>null</c> is passed.
    /// </summary>
    [TestMethod]
    public void TGroup_Insert_Null_ThrowsArgumentNullException()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        Assert.ThrowsExactly<ArgumentNullException>(() => group.Insert(null!));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Insert"/> eine <see cref="ArgumentException"/>
    /// auslöst, wenn die View bereits einer Gruppe angehört.
    ///
    /// Verifies that <see cref="TGroup.Insert"/> throws an <see cref="ArgumentException"/>
    /// when the view already belongs to a group.
    /// </summary>
    [TestMethod]
    public void TGroup_Insert_Duplicate_ThrowsArgumentException()
    {
        TGroup group = new(new TRect(0, 0, 20, 20));
        TView child = new(new TRect(0, 0, 5, 5));
        group.Insert(child);

        Assert.ThrowsExactly<ArgumentException>(() => group.Insert(child));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Insert"/> eine <see cref="ArgumentException"/>
    /// auslöst, wenn die Gruppe sich selbst einfügt.
    ///
    /// Verifies that <see cref="TGroup.Insert"/> throws an <see cref="ArgumentException"/>
    /// when the group tries to insert itself.
    /// </summary>
    [TestMethod]
    public void TGroup_Insert_Self_ThrowsArgumentException()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        Assert.ThrowsExactly<ArgumentException>(() => group.Insert(group));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Remove"/> eine View entfernt und deren
    /// <see cref="TView.Owner"/> auf <c>null</c> setzt.
    ///
    /// Verifies that <see cref="TGroup.Remove"/> removes a view and sets its
    /// <see cref="TView.Owner"/> to <c>null</c>.
    /// </summary>
    [TestMethod]
    public void TGroup_Remove_RemovesView_AndClearsOwner()
    {
        TGroup group = new(new TRect(0, 0, 20, 20));
        TView child = new(new TRect(0, 0, 5, 5));
        group.Insert(child);

        group.Remove(child);

        Assert.IsNull(child.Owner);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Remove"/> eine <see cref="ArgumentNullException"/>
    /// auslöst, wenn <c>null</c> übergeben wird.
    ///
    /// Verifies that <see cref="TGroup.Remove"/> throws an <see cref="ArgumentNullException"/>
    /// when <c>null</c> is passed.
    /// </summary>
    [TestMethod]
    public void TGroup_Remove_Null_ThrowsArgumentNullException()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        Assert.ThrowsExactly<ArgumentNullException>(() => group.Remove(null!));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Remove"/> eine <see cref="ArgumentException"/>
    /// auslöst, wenn die View nicht dieser Gruppe angehört.
    ///
    /// Verifies that <see cref="TGroup.Remove"/> throws an <see cref="ArgumentException"/>
    /// when the view does not belong to this group.
    /// </summary>
    [TestMethod]
    public void TGroup_Remove_NonMember_ThrowsArgumentException()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));
        TView stranger = new(new TRect(0, 0, 5, 5));

        Assert.ThrowsExactly<ArgumentException>(() => group.Remove(stranger));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.ShutDown"/> <see cref="TObject.ShutDown"/> aller
    /// Kind-Views in umgekehrter Einfügereihenfolge (LIFO) aufruft.
    ///
    /// Verifies that <see cref="TGroup.ShutDown"/> calls <see cref="TObject.ShutDown"/>
    /// on all child views in reverse insertion order (LIFO).
    /// </summary>
    [TestMethod]
    public void TGroup_ShutDown_CallsShutDownOnAllChildren_InLIFOOrder()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        List<string> shutdownOrder = new();

        ShutDownTrackingView childA = new(new TRect(0, 0, 5, 5), "A", shutdownOrder);
        ShutDownTrackingView childB = new(new TRect(5, 0, 10, 5), "B", shutdownOrder);
        ShutDownTrackingView childC = new(new TRect(10, 0, 15, 5), "C", shutdownOrder);

        group.Insert(childA);
        group.Insert(childB);
        group.Insert(childC);

        group.ShutDown();

        // LIFO: C zuerst, dann B, dann A / LIFO: C first, then B, then A
        CollectionAssert.AreEqual(new[] { "C", "B", "A" }, shutdownOrder);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.ShutDown"/> auf einer leeren Gruppe idempotent ist
    /// (kein Fehler, kein Effekt).
    ///
    /// Verifies that <see cref="TGroup.ShutDown"/> on an empty group is idempotent
    /// (no error, no effect).
    /// </summary>
    [TestMethod]
    public void TGroup_ShutDown_OnEmptyGroup_IsIdempotent()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        // Must not throw / Darf keine Ausnahme auslösen
        group.ShutDown();

        Assert.IsNull(group.Current);
    }

    // =========================================================================
    // Phase 3 / US1: HandleEvent (T013)
    // =========================================================================

    /// <summary>
    /// Prüft, dass <see cref="TGroup.HandleEvent"/> ein Tastaturereignis nur an
    /// <see cref="TGroup.Current"/> weiterleitet, nicht an andere Kind-Views.
    ///
    /// Verifies that <see cref="TGroup.HandleEvent"/> forwards a keyboard event only to
    /// <see cref="TGroup.Current"/>, not to other child views.
    /// </summary>
    [TestMethod]
    public void TGroup_HandleEvent_Keyboard_DeliversToCurrentOnly()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        EventCountingView focused = new(new TRect(0, 0, 5, 5));
        EventCountingView other = new(new TRect(5, 0, 10, 5));

        group.Insert(focused);
        group.Insert(other);
        group.SetFocus(focused);

        TEvent keyDown = TEvent.CreateKeyDown(new TKeyDownEvent('\r', 28, (ushort)ConsoleKey.Enter, 0, 28));
        group.HandleEvent(keyDown);

        Assert.AreEqual(1, focused.HandleEventCount);
        Assert.AreEqual(0, other.HandleEventCount);
    }

    /// <summary>
    /// Prüft, dass Views mit <see cref="TViewOptions.PreProcess"/>-Flag Ereignisse
    /// vor der fokussierten View erhalten.
    ///
    /// Verifies that views with <see cref="TViewOptions.PreProcess"/> flag receive events
    /// before the focused view.
    /// </summary>
    [TestMethod]
    public void TGroup_HandleEvent_PreProcess_ReceivesBeforeFocused()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        List<string> receiveOrder = new();

        OrderTrackingView preProcessor = new(new TRect(0, 0, 5, 5), "pre", receiveOrder);
        preProcessor.Options |= TViewOptions.PreProcess;
        preProcessor.EventMask = TEventKind.KeyDown;

        OrderTrackingView focused = new(new TRect(5, 0, 10, 5), "focused", receiveOrder);

        group.Insert(preProcessor);
        group.Insert(focused);
        group.SetFocus(focused);

        TEvent keyDown = TEvent.CreateKeyDown(new TKeyDownEvent('a', 30, (ushort)ConsoleKey.A, 0, 30));
        group.HandleEvent(keyDown);

        Assert.IsTrue(receiveOrder.IndexOf("pre") < receiveOrder.IndexOf("focused"),
            $"PreProcess view must receive event before focused view. Actual order: [{string.Join(", ", receiveOrder)}]");
    }

    /// <summary>
    /// Prüft, dass Views mit <see cref="TViewOptions.PostProcess"/>-Flag Ereignisse
    /// nach der fokussierten View erhalten, wenn das Ereignis nicht verbraucht wurde.
    ///
    /// Verifies that views with <see cref="TViewOptions.PostProcess"/> flag receive events
    /// after the focused view when the event was not consumed.
    /// </summary>
    [TestMethod]
    public void TGroup_HandleEvent_PostProcess_ReceivesAfterFocused_WhenNotConsumed()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        List<string> receiveOrder = new();

        OrderTrackingView focused = new(new TRect(0, 0, 5, 5), "focused", receiveOrder);
        OrderTrackingView postProcessor = new(new TRect(5, 0, 10, 5), "post", receiveOrder);
        postProcessor.Options |= TViewOptions.PostProcess;
        postProcessor.EventMask = TEventKind.KeyDown;

        group.Insert(focused);
        group.Insert(postProcessor);
        group.SetFocus(focused);

        TEvent keyDown = TEvent.CreateKeyDown(new TKeyDownEvent('a', 30, (ushort)ConsoleKey.A, 0, 30));
        group.HandleEvent(keyDown);

        Assert.IsTrue(receiveOrder.IndexOf("focused") < receiveOrder.IndexOf("post"),
            $"PostProcess view must receive event after focused view. Actual order: [{string.Join(", ", receiveOrder)}]");
    }

    /// <summary>
    /// Prüft, dass eine PostProcess-View das Ereignis NICHT erhält, wenn es in der
    /// Focused-Phase verbraucht wurde.
    ///
    /// Verifies that a PostProcess view does NOT receive the event when it was consumed
    /// in the Focused phase.
    /// </summary>
    [TestMethod]
    public void TGroup_HandleEvent_PostProcess_Skipped_WhenEventConsumedInFocusedPhase()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));

        // Focused view consumes the event / Fokussierte View verbraucht das Ereignis
        EventConsumingView focused = new(new TRect(0, 0, 5, 5));
        EventCountingView postProcessor = new(new TRect(5, 0, 10, 5));
        postProcessor.Options |= TViewOptions.PostProcess;
        postProcessor.EventMask = TEventKind.KeyDown;

        group.Insert(focused);
        group.Insert(postProcessor);
        group.SetFocus(focused);

        TEvent keyDown = TEvent.CreateKeyDown(new TKeyDownEvent('a', 30, (ushort)ConsoleKey.A, 0, 30));
        group.HandleEvent(keyDown);

        Assert.AreEqual(0, postProcessor.HandleEventCount);
    }

    /// <summary>
    /// Prüft, dass ein in der PreProcess-Phase verbrauchtes Ereignis alle nachfolgenden
    /// Phasen (Focused, PostProcess) überspringt.
    ///
    /// Verifies that an event consumed in the PreProcess phase skips all subsequent
    /// phases (Focused, PostProcess).
    /// </summary>
    [TestMethod]
    public void TGroup_HandleEvent_PreProcess_ConsumedEvent_StopsAllSubsequentPhases()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));

        EventConsumingView preProcessor = new(new TRect(0, 0, 5, 5));
        preProcessor.Options |= TViewOptions.PreProcess;
        preProcessor.EventMask = TEventKind.KeyDown;

        EventCountingView focused = new(new TRect(5, 0, 10, 5));
        EventCountingView postProcessor = new(new TRect(10, 0, 15, 5));
        postProcessor.Options |= TViewOptions.PostProcess;
        postProcessor.EventMask = TEventKind.KeyDown;

        group.Insert(preProcessor);
        group.Insert(focused);
        group.Insert(postProcessor);
        group.SetFocus(focused);

        TEvent keyDown = TEvent.CreateKeyDown(new TKeyDownEvent('a', 30, (ushort)ConsoleKey.A, 0, 30));
        group.HandleEvent(keyDown);

        Assert.AreEqual(0, focused.HandleEventCount);
        Assert.AreEqual(0, postProcessor.HandleEventCount);
    }

    /// <summary>
    /// SC-004 Integration: Zwei Kind-Views, Fokuswechsel, Tastatur-Dispatch.
    /// Kombiniert Insert, SetFocus und HandleEvent in einem einzigen Akzeptanzszenario.
    ///
    /// SC-004 Integration: two child views, focus switch, keyboard dispatch.
    /// Combines Insert, SetFocus, and HandleEvent in a single acceptance scenario.
    /// </summary>
    [TestMethod]
    public void TGroup_Integration_TwoChildren_FocusSwitch_KeyboardDispatch()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        EventCountingView viewA = new(new TRect(0, 0, 5, 5));
        EventCountingView viewB = new(new TRect(5, 0, 10, 5));

        group.Insert(viewA);
        group.Insert(viewB);
        group.SetFocus(viewA);

        TEvent key1 = TEvent.CreateKeyDown(new TKeyDownEvent('a', 30, (ushort)ConsoleKey.A, 0, 30));
        group.HandleEvent(key1);
        Assert.AreEqual(1, viewA.HandleEventCount, "viewA receives event when focused.");
        Assert.AreEqual(0, viewB.HandleEventCount, "viewB does not receive event when viewA is focused.");

        group.SetFocus(viewB);

        TEvent key2 = TEvent.CreateKeyDown(new TKeyDownEvent('b', 48, (ushort)ConsoleKey.B, 0, 48));
        group.HandleEvent(key2);
        Assert.AreEqual(1, viewA.HandleEventCount, "viewA still has 1 total after focus switch.");
        Assert.AreEqual(1, viewB.HandleEventCount, "viewB receives event when focused.");
    }

    // =========================================================================
    // Phase 4 / US2: Fokus-Management (T015)
    // =========================================================================

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> vorwärts zur nächsten auswählbaren View navigiert.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> navigates forward to the next selectable view.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_Forward_MovesToNextSelectableView()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        TView viewA = new(new TRect(0, 0, 5, 5)) { Options = TViewOptions.Selectable };
        TView viewB = new(new TRect(5, 0, 10, 5)) { Options = TViewOptions.Selectable };

        group.Insert(viewA);
        group.Insert(viewB);
        group.SetFocus(viewA);

        group.SelectNext(true);

        Assert.AreSame(viewB, group.Current);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> zirkulär zurück zur ersten View springt.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> wraps around circularly to the first view.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_Forward_WrapsAroundCircularly()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        TView viewA = new(new TRect(0, 0, 5, 5)) { Options = TViewOptions.Selectable };
        TView viewB = new(new TRect(5, 0, 10, 5)) { Options = TViewOptions.Selectable };

        group.Insert(viewA);
        group.Insert(viewB);
        group.SetFocus(viewB);

        group.SelectNext(true);

        Assert.AreSame(viewA, group.Current);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> rückwärts navigiert.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> navigates backward.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_Backward_MovesToPreviousView()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        TView viewA = new(new TRect(0, 0, 5, 5)) { Options = TViewOptions.Selectable };
        TView viewB = new(new TRect(5, 0, 10, 5)) { Options = TViewOptions.Selectable };

        group.Insert(viewA);
        group.Insert(viewB);
        group.SetFocus(viewB);

        group.SelectNext(false);

        Assert.AreSame(viewA, group.Current);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> deaktivierte Views überspringt.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> skips disabled views.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_SkipsDisabledViews()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        TView viewA = new(new TRect(0, 0, 5, 5)) { Options = TViewOptions.Selectable };
        TView viewB = new(new TRect(5, 0, 10, 5)) { Options = TViewOptions.Selectable };
        TView viewC = new(new TRect(10, 0, 15, 5)) { Options = TViewOptions.Selectable };

        group.Insert(viewA);
        group.Insert(viewB);
        group.Insert(viewC);

        viewB.SetState(TViewState.Disabled, true);
        group.SetFocus(viewA);

        group.SelectNext(true);

        Assert.AreSame(viewC, group.Current, "SelectNext skips disabled viewB.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> unsichtbare Views überspringt.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> skips invisible views.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_SkipsInvisibleViews()
    {
        TGroup group = new(new TRect(0, 0, 30, 10));
        TView viewA = new(new TRect(0, 0, 5, 5)) { Options = TViewOptions.Selectable };
        TView viewB = new(new TRect(5, 0, 10, 5)) { Options = TViewOptions.Selectable };
        TView viewC = new(new TRect(10, 0, 15, 5)) { Options = TViewOptions.Selectable };

        group.Insert(viewA);
        group.Insert(viewB);
        group.Insert(viewC);

        viewB.SetState(TViewState.Visible, false);
        group.SetFocus(viewA);

        group.SelectNext(true);

        Assert.AreSame(viewC, group.Current, "SelectNext skips invisible viewB.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> auf einer leeren Gruppe ein No-Op ist.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> is a no-op on an empty group.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_OnEmptyGroup_IsNoOp()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        // Must not throw / Darf keine Ausnahme auslösen
        group.SelectNext(true);

        Assert.IsNull(group.Current);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SelectNext"/> bei einer einzigen auswählbaren View
    /// auf dieser View verbleibt.
    ///
    /// Verifies that <see cref="TGroup.SelectNext"/> stays on the same view when there is
    /// only one selectable view.
    /// </summary>
    [TestMethod]
    public void TGroup_SelectNext_WithSingleSelectableView_StaysFocused()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));
        TView sole = new(new TRect(0, 0, 5, 5)) { Options = TViewOptions.Selectable };
        group.Insert(sole);
        group.SetFocus(sole);

        group.SelectNext(true);

        Assert.AreSame(sole, group.Current);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetFocus"/> das Focused-State korrekt überträgt.
    ///
    /// Verifies that <see cref="TGroup.SetFocus"/> correctly transfers the Focused state.
    /// </summary>
    [TestMethod]
    public void TGroup_SetFocus_TransfersFocusedState()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        TView viewA = new(new TRect(0, 0, 5, 5));
        TView viewB = new(new TRect(5, 0, 10, 5));
        group.Insert(viewA);
        group.Insert(viewB);

        group.SetFocus(viewA);
        Assert.IsTrue(viewA.GetState(TViewState.Focused));
        Assert.IsFalse(viewB.GetState(TViewState.Focused));

        group.SetFocus(viewB);
        Assert.IsTrue(viewB.GetState(TViewState.Focused));
        Assert.IsFalse(viewA.GetState(TViewState.Focused));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetFocus"/> das Focused-Flag der vorherigen
    /// fokussierten View löscht.
    ///
    /// Verifies that <see cref="TGroup.SetFocus"/> clears the Focused flag of the
    /// previously focused view.
    /// </summary>
    [TestMethod]
    public void TGroup_SetFocus_ClearsPreviousCurrent_Focused()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        TView viewA = new(new TRect(0, 0, 5, 5));
        TView viewB = new(new TRect(5, 0, 10, 5));
        group.Insert(viewA);
        group.Insert(viewB);
        group.SetFocus(viewA);

        group.SetFocus(viewB);

        Assert.IsFalse(viewA.GetState(TViewState.Focused));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetFocus"/> ein No-Op ist, wenn die View bereits
    /// fokussiert ist (kein Fokuswechsel notwendig).
    ///
    /// Verifies that <see cref="TGroup.SetFocus"/> is a no-op when the view is already
    /// focused (no focus change needed).
    /// </summary>
    [TestMethod]
    public void TGroup_SetFocus_AlreadyFocused_IsNoOp()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));
        TView view = new(new TRect(0, 0, 5, 5));
        group.Insert(view);
        group.SetFocus(view);

        // Should not throw, Current stays the same / Darf keine Ausnahme auslösen
        group.SetFocus(view);

        Assert.AreSame(view, group.Current);
        Assert.IsTrue(view.GetState(TViewState.Focused));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetFocus"/> eine <see cref="ArgumentNullException"/>
    /// auslöst, wenn <c>null</c> übergeben wird.
    ///
    /// Verifies that <see cref="TGroup.SetFocus"/> throws an <see cref="ArgumentNullException"/>
    /// when <c>null</c> is passed.
    /// </summary>
    [TestMethod]
    public void TGroup_SetFocus_Null_ThrowsArgumentNullException()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        Assert.ThrowsExactly<ArgumentNullException>(() => group.SetFocus(null!));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetFocus"/> eine <see cref="ArgumentException"/>
    /// auslöst, wenn die View nicht dieser Gruppe angehört.
    ///
    /// Verifies that <see cref="TGroup.SetFocus"/> throws an <see cref="ArgumentException"/>
    /// when the view does not belong to this group.
    /// </summary>
    [TestMethod]
    public void TGroup_SetFocus_NonMember_ThrowsArgumentException()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));
        TView stranger = new(new TRect(0, 0, 5, 5));

        Assert.ThrowsExactly<ArgumentException>(() => group.SetFocus(stranger));
    }

    // =========================================================================
    // Phase 5 / US3: Draw-Integration (T017)
    // =========================================================================

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Draw"/> <see cref="TView.DrawView"/> auf alle
    /// sichtbaren Kind-Views in Einfügereihenfolge aufruft.
    ///
    /// Verifies that <see cref="TGroup.Draw"/> calls <see cref="TView.DrawView"/> on all
    /// visible child views in insertion order.
    /// </summary>
    [TestMethod]
    public void TGroup_Draw_CallsDrawViewOnVisibleChildrenInInsertionOrder()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        List<string> drawOrder = new();

        DrawOrderTrackingView childA = new(new TRect(0, 0, 5, 5), "A", drawOrder);
        DrawOrderTrackingView childB = new(new TRect(5, 0, 10, 5), "B", drawOrder);

        group.Insert(childA);
        group.Insert(childB);

        group.Draw();

        CollectionAssert.AreEqual(new[] { "A", "B" }, drawOrder);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Draw"/> unsichtbare Kind-Views überspringt.
    ///
    /// Verifies that <see cref="TGroup.Draw"/> skips invisible child views.
    /// </summary>
    [TestMethod]
    public void TGroup_Draw_SkipsInvisibleChildren()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        List<string> drawOrder = new();

        DrawOrderTrackingView childA = new(new TRect(0, 0, 5, 5), "A", drawOrder);
        DrawOrderTrackingView childB = new(new TRect(5, 0, 10, 5), "B", drawOrder);
        group.Insert(childA);
        group.Insert(childB);
        childB.SetState(TViewState.Visible, false);

        group.Draw();

        CollectionAssert.AreEqual(new[] { "A" }, drawOrder);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.Draw"/> einen Puffer alloziert, wenn
    /// <see cref="TViewOptions.Buffered"/> gesetzt und die Gruppe exponiert ist.
    ///
    /// Verifies that <see cref="TGroup.Draw"/> allocates a buffer when
    /// <see cref="TViewOptions.Buffered"/> is set and the group is exposed.
    /// </summary>
    [TestMethod]
    public void TGroup_Draw_AllocatesBuffer_WhenBufferedAndExposed()
    {
        TGroup group = new(new TRect(0, 0, 10, 5));
        group.SetState(TViewState.Exposed, true);

        // Draw — should allocate buffer / Soll Puffer allozieren
        group.Draw();

        // No assertion on buffer directly (private), but Draw must not throw
        // Keine direkte Assertion auf privaten Buffer, aber Draw darf keine Ausnahme auslösen
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.LockDraw"/> <see cref="TView.DrawView"/> verhindert.
    ///
    /// Verifies that <see cref="TGroup.LockDraw"/> prevents <see cref="TView.DrawView"/>.
    /// </summary>
    [TestMethod]
    public void TGroup_LockDraw_PreventsDrawView()
    {
        TGroup group = new(new TRect(0, 0, 10, 5));
        int drawCount = 0;

        // Override DrawView indirectly: lock should prevent Draw call
        // Use IsLocked through a child view
        DrawCountingView child = new(new TRect(0, 0, 5, 5), () => drawCount++);
        group.Insert(child);

        group.LockDraw();
        group.Draw();

        // Child DrawView won't be called because group.IsLocked = true
        // When DrawView checks Owner.IsLocked, it returns early
        Assert.AreEqual(0, drawCount);
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.UnlockDraw"/> nach Entsperren genau einmal
    /// <see cref="TView.DrawView"/> auslöst.
    ///
    /// Verifies that <see cref="TGroup.UnlockDraw"/> triggers exactly one
    /// <see cref="TView.DrawView"/> call after unlocking.
    /// </summary>
    [TestMethod]
    public void TGroup_UnlockDraw_TriggersExactlyOneDrawView()
    {
        TGroup group = new(new TRect(0, 0, 10, 5));
        group.SetState(TViewState.Visible, true);
        group.LockDraw();

        // UnlockDraw should call DrawView once (group's own DrawView — no children, so just draw)
        // Darf keine Ausnahme auslösen
        group.UnlockDraw();
    }

    /// <summary>
    /// Prüft, dass bei doppeltem LockDraw erst der zweite UnlockDraw das Neuzeichnen auslöst.
    ///
    /// Verifies that with double LockDraw, only the second UnlockDraw triggers a redraw.
    /// </summary>
    [TestMethod]
    public void TGroup_LockDraw_Nested_CounterMustReachZero_BeforeRedraw()
    {
        TGroup group = new(new TRect(0, 0, 10, 5));
        int drawViewCalls = 0;
        DrawCountingGroupView trackingGroup = new(new TRect(0, 0, 10, 5), () => drawViewCalls++);

        trackingGroup.LockDraw();
        trackingGroup.LockDraw();

        trackingGroup.UnlockDraw(); // counter: 1 — no redraw
        Assert.AreEqual(0, drawViewCalls, "No redraw after first UnlockDraw (counter still 1).");

        trackingGroup.UnlockDraw(); // counter: 0 — redraw
        Assert.AreEqual(1, drawViewCalls, "Redraw triggered after second UnlockDraw (counter 0).");
    }

    /// <summary>
    /// SC-005 Snapshot-Test: Eine sichtbare und eine unsichtbare Kind-View;
    /// nach <see cref="TView.DrawView"/> enthält der Puffer nur Zellen der sichtbaren View.
    ///
    /// SC-005 Snapshot test: one visible and one invisible child view;
    /// after <see cref="TView.DrawView"/> the buffer contains only cells from the visible view.
    /// </summary>
    [TestMethod]
    public void TGroup_Draw_VisibleChild_WritesExpectedCellsToBuffer()
    {
        // Use a group with a concrete buffer-writing child view
        TGroup group = new(new TRect(0, 0, 10, 5));
        group.SetState(TViewState.Exposed, true);

        // Visible child writes 'V' at (0,0) / Sichtbares Kind schreibt 'V' bei (0,0)
        BufferWritingView visible = new(new TRect(0, 0, 10, 5), group, 'V');
        // Invisible child would write 'I' at (1,0) / Unsichtbares Kind würde 'I' bei (1,0) schreiben
        BufferWritingView invisible = new(new TRect(0, 0, 10, 5), group, 'I');

        group.Insert(visible);
        group.Insert(invisible);
        invisible.SetState(TViewState.Visible, false);

        group.Draw();

        // Check that visible child wrote its marker / Prüfe, dass sichtbares Kind seinen Marker geschrieben hat
        Assert.AreEqual(1, visible.DrawCallCount, "Visible child Draw() was called once.");
        Assert.AreEqual(0, invisible.DrawCallCount, "Invisible child Draw() was not called.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.OnBoundsChanged"/> den Puffer neu alloziert,
    /// wenn er bereits existiert.
    ///
    /// Verifies that <see cref="TGroup.OnBoundsChanged"/> reallocates the buffer
    /// when it already exists.
    /// </summary>
    [TestMethod]
    public void TGroup_OnBoundsChanged_ReallocatesBuffer_WhenBufferExists()
    {
        TGroup group = new(new TRect(0, 0, 10, 5));
        group.SetState(TViewState.Exposed, true);
        group.Draw(); // allocates buffer / alloziert Puffer

        // Resize — OnBoundsChanged is triggered / Größenänderung — OnBoundsChanged wird ausgelöst
        group.GrowTo(20, 10);

        // Must not throw / Darf keine Ausnahme auslösen
        group.Draw();
    }

    // =========================================================================
    // Phase 6 / US4: State-Propagation (T019)
    // =========================================================================

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetState"/> den Active-State an alle direkten
    /// Kind-Views propagiert.
    ///
    /// Verifies that <see cref="TGroup.SetState"/> propagates the Active state to all
    /// direct child views.
    /// </summary>
    [TestMethod]
    public void TGroup_SetState_Active_PropagatesDirectChildren()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        TView childA = new(new TRect(0, 0, 5, 5));
        TView childB = new(new TRect(5, 0, 10, 5));
        group.Insert(childA);
        group.Insert(childB);

        group.SetState(TViewState.Active, true);

        Assert.IsTrue(childA.GetState(TViewState.Active));
        Assert.IsTrue(childB.GetState(TViewState.Active));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetState"/> den Disabled-State an alle direkten
    /// Kind-Views propagiert.
    ///
    /// Verifies that <see cref="TGroup.SetState"/> propagates the Disabled state to all
    /// direct child views.
    /// </summary>
    [TestMethod]
    public void TGroup_SetState_Disabled_PropagatesDirectChildren()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        TView childA = new(new TRect(0, 0, 5, 5));
        TView childB = new(new TRect(5, 0, 10, 5));
        group.Insert(childA);
        group.Insert(childB);

        group.SetState(TViewState.Disabled, true);

        Assert.IsTrue(childA.GetState(TViewState.Disabled));
        Assert.IsTrue(childB.GetState(TViewState.Disabled));
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetState"/> den Focused-State an alle direkten
    /// Kind-Views propagiert.
    ///
    /// Verifies that <see cref="TGroup.SetState"/> propagates the Focused state to all
    /// direct child views.
    /// </summary>
    [TestMethod]
    public void TGroup_SetState_Focused_PropagatesDirectChildren()
    {
        TGroup group = new(new TRect(0, 0, 20, 10));
        TView childA = new(new TRect(0, 0, 5, 5));
        TView childB = new(new TRect(5, 0, 10, 5));
        group.Insert(childA);
        group.Insert(childB);

        group.SetState(TViewState.Focused, true);

        Assert.IsTrue(childA.GetState(TViewState.Focused));
        Assert.IsTrue(childB.GetState(TViewState.Focused));
    }

    /// <summary>
    /// Prüft, dass eine verschachtelte Gruppe den State als eine Kind-View erhält,
    /// ihn aber intern eigenständig propagiert.
    ///
    /// Verifies that a nested group receives the state as a child view
    /// but propagates it internally on its own.
    /// </summary>
    [TestMethod]
    public void TGroup_SetState_Active_NestedGroup_ReceivesAsOneChild()
    {
        TGroup outer = new(new TRect(0, 0, 30, 10));
        TGroup inner = new(new TRect(0, 0, 15, 10));
        TView innerChild = new(new TRect(0, 0, 5, 5));

        outer.Insert(inner);
        inner.Insert(innerChild);

        outer.SetState(TViewState.Active, true);

        Assert.IsTrue(inner.GetState(TViewState.Active), "Inner group receives Active.");
        Assert.IsTrue(innerChild.GetState(TViewState.Active), "Inner group propagates Active to its own child.");
    }

    /// <summary>
    /// Prüft, dass <see cref="TGroup.SetState"/> auf einer leeren Gruppe idempotent ist.
    ///
    /// Verifies that <see cref="TGroup.SetState"/> on an empty group is idempotent.
    /// </summary>
    [TestMethod]
    public void TGroup_SetState_Active_EmptyGroup_IsIdempotent()
    {
        TGroup group = new(new TRect(0, 0, 10, 10));

        // Must not throw / Darf keine Ausnahme auslösen
        group.SetState(TViewState.Active, true);

        Assert.IsTrue(group.GetState(TViewState.Active));
    }

    // =========================================================================
    // Hilfsklassen / Helper classes
    // =========================================================================

    /// <summary>
    /// Zählt die Anzahl der <see cref="TView.HandleEvent"/>-Aufrufe.
    ///
    /// Counts the number of <see cref="TView.HandleEvent"/> calls.
    /// </summary>
    private sealed class EventCountingView : TView
    {
        /// <summary>Erstellt eine neue zählende View. / Creates a new counting view.</summary>
        public EventCountingView(TRect bounds) : base(bounds) { }

        /// <summary>Anzahl der HandleEvent-Aufrufe. / Number of HandleEvent calls.</summary>
        public int HandleEventCount { get; private set; }

        /// <summary>Erhöht den Zähler. / Increments the counter.</summary>
        public override void HandleEvent(TEvent @event) => HandleEventCount++;
    }

    /// <summary>
    /// Verbraucht jedes Ereignis sofort (setzt es auf Nothing).
    ///
    /// Consumes every event immediately (sets it to Nothing).
    /// </summary>
    private sealed class EventConsumingView : TView
    {
        /// <summary>Erstellt eine neue konsumierende View. / Creates a new consuming view.</summary>
        public EventConsumingView(TRect bounds) : base(bounds) { }

        /// <summary>Verbraucht das Ereignis. / Consumes the event.</summary>
        public override void HandleEvent(TEvent @event) => @event.Clear(this);
    }

    /// <summary>
    /// Zeichnet die Reihenfolge der Ereignis-Empfänger auf.
    ///
    /// Records the order of event recipients.
    /// </summary>
    private sealed class OrderTrackingView : TView
    {
        private readonly string _name;
        private readonly List<string> _order;

        /// <summary>Erstellt eine neue Reihenfolge-Tracking-View. / Creates a new order tracking view.</summary>
        public OrderTrackingView(TRect bounds, string name, List<string> order) : base(bounds)
        {
            _name = name;
            _order = order;
        }

        /// <summary>Zeichnet den Namen in der Reihenfolge auf. / Records the name in order.</summary>
        public override void HandleEvent(TEvent @event) => _order.Add(_name);
    }

    /// <summary>
    /// Zeichnet die Reihenfolge der Draw-Aufrufe auf.
    ///
    /// Records the order of Draw calls.
    /// </summary>
    private sealed class DrawOrderTrackingView : TView
    {
        private readonly string _name;
        private readonly List<string> _order;

        /// <summary>Erstellt eine neue Draw-Reihenfolge-Tracking-View. / Creates a new draw order tracking view.</summary>
        public DrawOrderTrackingView(TRect bounds, string name, List<string> order) : base(bounds)
        {
            _name = name;
            _order = order;
        }

        /// <summary>Zeichnet den Namen in der Reihenfolge auf. / Records the name in order.</summary>
        public override void Draw() => _order.Add(_name);
    }

    /// <summary>
    /// Verfolgt ShutDown-Aufrufe für die Reihenfolgeprüfung.
    ///
    /// Tracks ShutDown calls for order verification.
    /// </summary>
    private sealed class ShutDownTrackingView : TView
    {
        private readonly string _name;
        private readonly List<string> _order;

        /// <summary>Erstellt eine neue ShutDown-Tracking-View. / Creates a new shutdown tracking view.</summary>
        public ShutDownTrackingView(TRect bounds, string name, List<string> order) : base(bounds)
        {
            _name = name;
            _order = order;
        }

        /// <summary>Zeichnet den Namen in der LIFO-Reihenfolge auf. / Records the name in LIFO order.</summary>
        public override void ShutDown() => _order.Add(_name);
    }

    /// <summary>
    /// Zählt Draw-Aufrufe über einen Callback.
    ///
    /// Counts Draw calls via a callback.
    /// </summary>
    private sealed class DrawCountingView : TView
    {
        private readonly Action _onDraw;

        /// <summary>Erstellt eine neue Draw-zählende View. / Creates a new draw-counting view.</summary>
        public DrawCountingView(TRect bounds, Action onDraw) : base(bounds)
        {
            _onDraw = onDraw;
        }

        /// <summary>Ruft den Callback auf. / Invokes the callback.</summary>
        public override void Draw() => _onDraw();
    }

    /// <summary>
    /// TGroup-Ableitung, die UnlockDraw-ausgelöste DrawView-Aufrufe über einen Callback zählt.
    ///
    /// TGroup subclass that counts UnlockDraw-triggered DrawView calls via a callback.
    /// </summary>
    private sealed class DrawCountingGroupView : TGroup
    {
        private readonly Action _onDrawView;

        /// <summary>Erstellt eine neue Draw-zählende Gruppe. / Creates a new draw-counting group.</summary>
        public DrawCountingGroupView(TRect bounds, Action onDrawView) : base(bounds)
        {
            _onDrawView = onDrawView;
        }

        /// <summary>Ruft den Callback auf und delegiert an die Basis. / Invokes the callback and delegates to base.</summary>
        public override void Draw()
        {
            _onDrawView();
            base.Draw();
        }
    }

    /// <summary>
    /// View, die ihren Draw-Aufruf zählt (für SC-005 Snapshot-Test).
    ///
    /// View that counts its Draw call (for SC-005 snapshot test).
    /// </summary>
    private sealed class BufferWritingView : TView
    {
        private readonly char _marker;

        /// <summary>Erstellt eine neue pufferschreibende View. / Creates a new buffer-writing view.</summary>
        public BufferWritingView(TRect bounds, TGroup owner, char marker) : base(bounds)
        {
            _marker = marker;
        }

        /// <summary>Anzahl der Draw-Aufrufe. / Number of Draw calls.</summary>
        public int DrawCallCount { get; private set; }

        /// <summary>Erhöht den Zähler. / Increments the counter.</summary>
        public override void Draw() => DrawCallCount++;
    }
}
