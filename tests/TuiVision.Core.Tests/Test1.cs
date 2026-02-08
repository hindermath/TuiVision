using TuiVision.Core;

namespace TuiVision.Core.Tests;

[TestClass]
public sealed class CorePortTests
{
    [TestMethod]
    public void TPoint_Operators_AddAndSubtractCoordinates()
    {
        TPoint a = new(4, 7);
        TPoint b = new(2, 3);

        Assert.AreEqual(new TPoint(6, 10), a + b);
        Assert.AreEqual(new TPoint(2, 4), a - b);
        Assert.AreNotEqual(a, b);
    }

    [TestMethod]
    public void TRect_Contains_UsesTopLeftInclusiveBottomRightExclusive()
    {
        TRect rect = new(1, 1, 4, 4);

        Assert.IsTrue(rect.Contains(new TPoint(1, 1)));
        Assert.IsTrue(rect.Contains(new TPoint(3, 3)));
        Assert.IsFalse(rect.Contains(new TPoint(4, 3)));
        Assert.IsFalse(rect.Contains(new TPoint(3, 4)));
    }

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

    [TestMethod]
    public void TObject_Destroy_CallsShutdownAndDispose()
    {
        LifecycleProbe probe = new();

        TObject.Destroy(probe);

        Assert.AreEqual(1, probe.ShutDownCalls);
        Assert.AreEqual(1, probe.DisposeCalls);
    }

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

    [TestMethod]
    public void TEvent_CreateMouse_RejectsNonMouseEventKind()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(
            () => TEvent.CreateMouse(TEventKind.Command, TMouseButtons.Left, false, new TPoint(0, 0)));
    }

    private sealed class LifecycleProbe : TObject, IDisposable
    {
        public int ShutDownCalls { get; private set; }

        public int DisposeCalls { get; private set; }

        public override void ShutDown() => ShutDownCalls++;

        public void Dispose() => DisposeCalls++;
    }
}
