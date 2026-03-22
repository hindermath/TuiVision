// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls.Internal;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Tests fuer nicht-modale gerahmte Host-Views.
///
/// Tests for non-modal framed host views.
/// </summary>
[TestClass]
public sealed class FramedHostViewTests
{
    /// <summary>
    /// Prueft, dass ein Host seinen Rahmen zeichnet und einen eingebetteten Inhalt hostet.
    ///
    /// Verifies that a host draws its frame and hosts embedded content.
    /// </summary>
    [TestMethod]
    public void FramedHostView_Draw_RendersFrameAndContent()
    {
        TestHostView host = new(new TRect(0, 0, 18, 6), "Editor");
        TView content = new TStaticText(new TRect(1, 1, 8, 2), "Inside");
        host.AttachContent(content);
        TGroup owner = ControlTestContext.AttachToOwner(host, new TRect(0, 0, 30, 10));

        TConsoleBuffer buffer = ControlTestContext.GetBufferSnapshot(owner);

        Assert.AreEqual('+', buffer.GetCell(0, 0).Glyph);
        Assert.AreEqual('I', buffer.GetCell(1, 1).Glyph);
    }

    /// <summary>
    /// Prueft, dass der Host ueber Command-Ereignisse schliessbar bleibt.
    ///
    /// Verifies that the host remains closeable through command events.
    /// </summary>
    [TestMethod]
    public void FramedHostView_HandleEvent_CloseCommandRemovesViewFromOwner()
    {
        TestHostView host = new(new TRect(0, 0, 18, 6), "Closable");
        TGroup owner = ControlTestContext.AttachToOwner(host, new TRect(0, 0, 30, 10));
        owner.SetFocus(host);
        TEvent close = ControlEventFactory.CreateCommand(ShellCommandIds.cmClose);

        host.HandleEvent(close);

        Assert.IsNull(host.Owner);
        Assert.AreEqual(TEventKind.Nothing, close.What);
    }

    private sealed class TestHostView : FramedHostView
    {
        public TestHostView(TRect bounds, string title) : base(bounds, title)
        {
        }

        public void AttachContent(TView view)
        {
            Insert(view);
            SetFocus(view);
        }
    }
}
