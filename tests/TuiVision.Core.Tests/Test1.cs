using TuiVision.Core;

namespace TuiVision.Core.Tests;

/// <summary>
/// Porttests für die Kerntypen <see cref="TPoint"/>, <see cref="TRect"/>,
/// <see cref="TObject"/> und <see cref="TEvent"/>.
///
/// Port tests for the core types <see cref="TPoint"/>, <see cref="TRect"/>,
/// <see cref="TObject"/>, and <see cref="TEvent"/>.
/// </summary>
[TestClass]
public sealed class CorePortTests
{
    /// <summary>
    /// Prüft, dass <see cref="TPoint"/> die arithmetischen Operatoren <c>+</c> und <c>-</c>
    /// komponentenweise ausführt und dass die Gleichheitsoperatoren korrekt funktionieren.
    ///
    /// Verifies that <see cref="TPoint"/> performs the <c>+</c> and <c>-</c> operators
    /// component-wise and that the equality operators work correctly.
    /// </summary>
    [TestMethod]
    public void TPoint_Operators_AddAndSubtractCoordinates()
    {
        TPoint a = new(4, 7);
        TPoint b = new(2, 3);

        Assert.AreEqual(new TPoint(6, 10), a + b);
        Assert.AreEqual(new TPoint(2, 4), a - b);
        Assert.AreNotEqual(a, b);
    }

    /// <summary>
    /// Prüft, dass <see cref="TRect.Contains"/> die obere linke Ecke inklusiv
    /// und die untere rechte Ecke exklusiv behandelt.
    ///
    /// Verifies that <see cref="TRect.Contains"/> treats the top-left corner as inclusive
    /// and the bottom-right corner as exclusive.
    /// </summary>
    [TestMethod]
    public void TRect_Contains_UsesTopLeftInclusiveBottomRightExclusive()
    {
        TRect rect = new(1, 1, 4, 4);

        Assert.IsTrue(rect.Contains(new TPoint(1, 1)));
        Assert.IsTrue(rect.Contains(new TPoint(3, 3)));
        Assert.IsFalse(rect.Contains(new TPoint(4, 3)));
        Assert.IsFalse(rect.Contains(new TPoint(3, 4)));
    }

    /// <summary>
    /// Prüft, dass <see cref="TRect.Intersect"/> und <see cref="TRect.Union"/>
    /// die Semantik des originalen Turbo-Vision-Codes korrekt abbilden.
    ///
    /// Verifies that <see cref="TRect.Intersect"/> and <see cref="TRect.Union"/>
    /// correctly replicate the semantics of the original Turbo Vision code.
    /// </summary>
    [TestMethod]
    public void TRect_IntersectAndUnion_FollowTurboVisionSemantics()
    {
        TRect first = new(0, 0, 5, 5);
        TRect second = new(3, 3, 8, 8);

        TRect intersection = first;
        intersection.Intersect(second);
        Assert.AreEqual(new TRect(3, 3, 5, 5), intersection);

        TRect united = first;
        united.Union(second);
        Assert.AreEqual(new TRect(0, 0, 8, 8), united);
    }

    /// <summary>
    /// Prüft, dass <see cref="TRect"/> Bewegung, Wachstum, Leerheitsprüfung und Hash-/Objektgleichheit korrekt abbildet.
    ///
    /// Verifies that <see cref="TRect"/> correctly models movement, growth, emptiness, and hash/object equality.
    /// </summary>
    [TestMethod]
    public void TRect_MoveGrowEmptyAndEquality_Work()
    {
        TRect rect = new(1, 1, 3, 3);
        rect.Move(2, -1);
        Assert.AreEqual(new TRect(3, 0, 5, 2), rect);

        rect.Grow(1, 2);
        Assert.AreEqual(new TRect(2, -2, 6, 4), rect);
        Assert.IsFalse(rect.IsEmpty());

        object boxed = new TRect(2, -2, 6, 4);
        Assert.IsTrue(rect.Equals(boxed));
        Assert.AreEqual(boxed.GetHashCode(), rect.GetHashCode());

        TRect empty = new(4, 4, 4, 3);
        Assert.IsTrue(empty.IsEmpty());
    }

    /// <summary>
    /// Prüft, dass <see cref="TObject.Destroy"/> sowohl <c>ShutDown</c> als auch
    /// <c>Dispose</c> aufruft, wenn das Objekt <see cref="IDisposable"/> implementiert.
    ///
    /// Verifies that <see cref="TObject.Destroy"/> calls both <c>ShutDown</c> and
    /// <c>Dispose</c> when the object implements <see cref="IDisposable"/>.
    /// </summary>
    [TestMethod]
    public void TObject_Destroy_CallsShutdownAndDispose()
    {
        LifecycleProbe probe = new();

        TObject.Destroy(probe);

        Assert.AreEqual(1, probe.ShutDownCalls);
        Assert.AreEqual(1, probe.DisposeCalls);
    }

    /// <summary>
    /// Prüft, dass <see cref="TObject.Destroy"/> mit <c>null</c> sicher ist.
    ///
    /// Verifies that <see cref="TObject.Destroy"/> is safe with <c>null</c>.
    /// </summary>
    [TestMethod]
    public void TObject_Destroy_Null_DoesNothing()
    {
        TObject.Destroy(null);
    }

    /// <summary>
    /// Prüft, dass <see cref="TEvent"/> nach dem Erstellen die richtigen Nutzdaten enthält
    /// und nach dem Aufruf von <see cref="TEvent.Clear"/> auf <see cref="TEventKind.Nothing"/> zurückgesetzt wird.
    ///
    /// Verifies that <see cref="TEvent"/> holds the correct payload after creation
    /// and resets to <see cref="TEventKind.Nothing"/> after <see cref="TEvent.Clear"/> is called.
    /// </summary>
    [TestMethod]
    public void TEvent_CreateAndClear_UpdatesEventChannels()
    {
        TEvent command = TEvent.CreateCommand(17, "payload");

        Assert.AreEqual(TEventKind.Command, command.What);
        Assert.AreEqual((ushort)17, command.Message.Command);
        Assert.AreEqual("payload", command.Message.Info);

        command.Clear();
        Assert.AreEqual(TEventKind.Nothing, command.What);
        Assert.AreEqual((ushort)0, command.Message.Command);
    }

    /// <summary>
    /// Prüft, dass <see cref="TEvent.CreateMouse"/> eine <see cref="ArgumentOutOfRangeException"/>
    /// auslöst, wenn ein Nicht-Maus-Ereignistyp übergeben wird.
    ///
    /// Verifies that <see cref="TEvent.CreateMouse"/> throws an <see cref="ArgumentOutOfRangeException"/>
    /// when a non-mouse event type is passed.
    /// </summary>
    [TestMethod]
    public void TEvent_CreateMouse_RejectsNonMouseEventKind()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => TEvent.CreateMouse(TEventKind.Command, TMouseButtons.Left, false, new TPoint(0, 0)));
    }

    /// <summary>
    /// Prüft, dass <see cref="TEvent"/> Tastatur-, Broadcast- und None-Kanäle korrekt initialisiert.
    ///
    /// Verifies that <see cref="TEvent"/> correctly initializes keyboard, broadcast, and none channels.
    /// </summary>
    [TestMethod]
    public void TEvent_CreateKeyBroadcastAndNone_InitializeChannels()
    {
        TKeyDownEvent keyDown = new('A', 0x1E, 0x1E41, 0x0001, 0x1E);
        TEvent keyEvent = TEvent.CreateKeyDown(keyDown);
        TEvent broadcast = TEvent.CreateBroadcast(77, "all");
        TEvent none = TEvent.CreateNone();

        Assert.AreEqual(TEventKind.KeyDown, keyEvent.What);
        Assert.AreEqual(keyDown, keyEvent.KeyDown);

        Assert.AreEqual(TEventKind.Broadcast, broadcast.What);
        Assert.AreEqual((ushort)77, broadcast.Message.Command);
        Assert.AreEqual("all", broadcast.Message.Info);

        Assert.AreEqual(TEventKind.Nothing, none.What);
        Assert.AreEqual((ushort)0, none.Message.Command);
    }

    /// <summary>
    /// Test-Hilfsobjekt, das Aufrufe von <c>ShutDown</c> und <c>Dispose</c> zählt.
    ///
    /// Test helper object that counts calls to <c>ShutDown</c> and <c>Dispose</c>.
    /// </summary>
    private sealed class LifecycleProbe : TObject, IDisposable
    {
        /// <summary>
        /// Die Anzahl der bisherigen <c>ShutDown</c>-Aufrufe.
        ///
        /// The number of <c>ShutDown</c> calls so far.
        /// </summary>
        public int ShutDownCalls { get; private set; }

        /// <summary>
        /// Die Anzahl der bisherigen <c>Dispose</c>-Aufrufe.
        ///
        /// The number of <c>Dispose</c> calls so far.
        /// </summary>
        public int DisposeCalls { get; private set; }

        /// <summary>Erhöht den <see cref="ShutDownCalls"/>-Zähler. / Increments the <see cref="ShutDownCalls"/> counter.</summary>
        public override void ShutDown() => ShutDownCalls++;

        /// <summary>Erhöht den <see cref="DisposeCalls"/>-Zähler. / Increments the <see cref="DisposeCalls"/> counter.</summary>
        public void Dispose() => DisposeCalls++;
    }
}
