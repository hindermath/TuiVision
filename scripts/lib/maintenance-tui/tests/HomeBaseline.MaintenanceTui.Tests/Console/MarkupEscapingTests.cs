using HomeBaseline.MaintenanceTui.Presentation;

namespace HomeBaseline.MaintenanceTui.Tests.Console;

[TestClass]
public sealed class MarkupEscapingTests
{
    [TestMethod]
    public void ForeignMarkupIsEscaped()
    {
        Assert.AreEqual("[[red]]unsafe[[/]]", MarkupText.Escape("[red]unsafe[/]"));
    }
}
