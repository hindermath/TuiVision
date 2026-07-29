using Spectre.Console;

namespace HomeBaseline.MaintenanceTui.Presentation;

public static class MarkupText
{
    public static string Escape(string value) => Markup.Escape(value);
}
