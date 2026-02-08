using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

[TestClass]
public sealed class TViewPortTests
{
    [TestMethod]
    public void Constructor_InitializesBoundsAndVisibleState()
    {
        TView view = new(new TRect(2, 3, 10, 8));

        Assert.AreEqual(new TRect(2, 3, 10, 8), view.GetBounds());
        Assert.AreEqual(new TPoint(8, 5), view.Size);
        Assert.IsTrue(view.GetState(TViewState.Visible));
    }

    [TestMethod]
    public void MoveToAndGrowTo_UpdateBoundsUsingCurrentOrigin()
    {
        TView view = new(new TRect(1, 1, 5, 4));

        view.MoveTo(4, 6);
        Assert.AreEqual(new TRect(4, 6, 8, 9), view.GetBounds());

        view.GrowTo(10, 2);
        Assert.AreEqual(new TRect(4, 6, 14, 8), view.GetBounds());
    }

    [TestMethod]
    public void MakeLocalAndMouseInView_UseExclusiveBottomRightEdges()
    {
        TView view = new(new TRect(10, 10, 14, 14));

        Assert.AreEqual(new TPoint(1, 2), view.MakeLocal(new TPoint(11, 12)));
        Assert.IsTrue(view.MouseInView(new TPoint(10, 10)));
        Assert.IsTrue(view.MouseInView(new TPoint(13, 13)));
        Assert.IsFalse(view.MouseInView(new TPoint(14, 13)));
    }

    [TestMethod]
    public void HandleEvent_SelectableView_SelectsAndClearsWithoutFirstClick()
    {
        TView view = new(new TRect(0, 0, 3, 3))
        {
            Options = TViewOptions.Selectable
        };
        TEvent mouseDown = TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(1, 1));

        view.HandleEvent(mouseDown);

        Assert.IsTrue(view.GetState(TViewState.Selected));
        Assert.AreEqual(TEventKind.Nothing, mouseDown.What);
    }

    [TestMethod]
    public void HandleEvent_DisabledView_DoesNotSelect()
    {
        TView view = new(new TRect(0, 0, 3, 3))
        {
            Options = TViewOptions.Selectable
        };
        view.SetState(TViewState.Disabled, true);
        TEvent mouseDown = TEvent.CreateMouse(TEventKind.MouseDown, TMouseButtons.Left, false, new TPoint(1, 1));

        view.HandleEvent(mouseDown);

        Assert.IsFalse(view.GetState(TViewState.Selected));
        Assert.AreEqual(TEventKind.MouseDown, mouseDown.What);
    }

    [TestMethod]
    public void Locate_RejectsNegativeSizeBounds()
    {
        TView view = new(new TRect(0, 0, 2, 2));

        Assert.ThrowsExactly<ArgumentException>(() => view.Locate(new TRect(5, 5, 4, 4)));
    }
}
