using TuiVision.Core;
using TuiVision.Controls;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Porttests für <see cref="TView"/>: Koordinatenverwaltung, Zustandsflags und Ereignisverarbeitung.
///
/// Port tests for <see cref="TView"/>: coordinate management, state flags, and event handling.
/// </summary>
[TestClass]
public sealed class TViewPortTests
{
    /// <summary>
    /// Prüft, dass der Konstruktor den Begrenzungsrahmen, die Größe und den
    /// sichtbaren Anfangszustand korrekt setzt.
    ///
    /// Verifies that the constructor correctly sets the bounding rectangle,
    /// the size, and the initial visible state.
    /// </summary>
    [TestMethod]
    public void Constructor_InitializesBoundsAndVisibleState()
    {
        TView view = new(new TRect(2, 3, 10, 8));

        Assert.AreEqual(new TRect(2, 3, 10, 8), view.GetBounds());
        Assert.AreEqual(new TPoint(8, 5), view.Size);
        Assert.IsTrue(view.GetState(TViewState.Visible));
    }

    /// <summary>
    /// Prüft, dass <see cref="TView.MoveTo"/> und <see cref="TView.GrowTo"/> den
    /// Begrenzungsrahmen relativ zur aktuellen Position korrekt aktualisieren.
    ///
    /// Verifies that <see cref="TView.MoveTo"/> and <see cref="TView.GrowTo"/> correctly
    /// update the bounding rectangle relative to the current position.
    /// </summary>
    [TestMethod]
    public void MoveToAndGrowTo_UpdateBoundsUsingCurrentOrigin()
    {
        TView view = new(new TRect(1, 1, 5, 4));

        view.MoveTo(4, 6);
        Assert.AreEqual(new TRect(4, 6, 8, 9), view.GetBounds());

        view.GrowTo(10, 2);
        Assert.AreEqual(new TRect(4, 6, 14, 8), view.GetBounds());
    }

    /// <summary>
    /// Prüft, dass <see cref="TView.MakeLocal"/> und <see cref="TView.MouseInView"/>
    /// die exklusive untere rechte Ecke korrekt berücksichtigen.
    ///
    /// Verifies that <see cref="TView.MakeLocal"/> and <see cref="TView.MouseInView"/>
    /// correctly respect the exclusive bottom-right edge.
    /// </summary>
    [TestMethod]
    public void MakeLocalAndMouseInView_UseExclusiveBottomRightEdges()
    {
        TView view = new(new TRect(10, 10, 14, 14));

        Assert.AreEqual(new TPoint(1, 2), view.MakeLocal(new TPoint(11, 12)));
        Assert.IsTrue(view.MouseInView(new TPoint(10, 10)));
        Assert.IsTrue(view.MouseInView(new TPoint(13, 13)));
        Assert.IsFalse(view.MouseInView(new TPoint(14, 13)));
    }

    /// <summary>
    /// Prüft, dass <see cref="TView.HandleEvent"/> eine auswählbare Ansicht selektiert
    /// und das Ereignis löscht, wenn <see cref="TViewOptions.FirstClick"/> nicht gesetzt ist.
    ///
    /// Verifies that <see cref="TView.HandleEvent"/> selects a selectable view
    /// and clears the event when <see cref="TViewOptions.FirstClick"/> is not set.
    /// </summary>
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

    /// <summary>
    /// Prüft, dass <see cref="TView.HandleEvent"/> eine deaktivierte Ansicht nicht auswählt
    /// und das Ereignis unverändert lässt.
    ///
    /// Verifies that <see cref="TView.HandleEvent"/> does not select a disabled view
    /// and leaves the event unchanged.
    /// </summary>
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

    /// <summary>
    /// Prüft, dass <see cref="TView.Locate"/> eine <see cref="ArgumentException"/> auslöst,
    /// wenn der übergebene Begrenzungsrahmen eine negative Größe hat.
    ///
    /// Verifies that <see cref="TView.Locate"/> throws an <see cref="ArgumentException"/>
    /// when the provided bounding rectangle has a negative size.
    /// </summary>
    [TestMethod]
    public void Locate_RejectsNegativeSizeBounds()
    {
        TView view = new(new TRect(0, 0, 2, 2));

        Assert.ThrowsExactly<ArgumentException>(() => view.Locate(new TRect(5, 5, 4, 4)));
    }
}
