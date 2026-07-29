using HomeBaseline.MaintenanceTui.Contracts;

namespace HomeBaseline.MaintenanceTui.Application;

public enum InvocationRoute
{
    Enhanced,
    Plain,
    Headless,
}

public sealed record InvocationRoutingResult(InvocationRoute Route, string Reason);

public static class InvocationRouter
{
    public static InvocationRoutingResult Route(InvocationContext context)
    {
        if (context.ExistingArguments.Count > 0)
        {
            return new InvocationRoutingResult(
                InvocationRoute.Headless,
                "Bestehende Wartungsargumente bleiben headless / existing maintenance arguments remain headless.");
        }

        return context.ExplicitUiMode switch
        {
            UiMode.Headless => new(InvocationRoute.Headless, "Headless wurde ausdrücklich gewählt / explicitly selected."),
            UiMode.Plain => new(InvocationRoute.Plain, "Lineare Oberfläche wurde ausdrücklich gewählt / plain UI selected."),
            UiMode.Enhanced when context.HasFullTerminal =>
                new(InvocationRoute.Enhanced, "Erweiterte TUI ist verfügbar / enhanced TUI is available."),
            UiMode.Enhanced =>
                new(InvocationRoute.Plain, context.TerminalCapabilities.FallbackReason ??
                    "Terminal unterstützt die erweiterte TUI nicht / terminal does not support the enhanced TUI."),
            UiMode.Auto when context.HasFullTerminal =>
                new(InvocationRoute.Enhanced, "Interaktives Ein- und Ausgabegerät erkannt / interactive input and output detected."),
            _ => new(
                InvocationRoute.Headless,
                "Nicht interaktiver Aufruf bleibt unverändert / non-interactive invocation remains unchanged."),
        };
    }
}
