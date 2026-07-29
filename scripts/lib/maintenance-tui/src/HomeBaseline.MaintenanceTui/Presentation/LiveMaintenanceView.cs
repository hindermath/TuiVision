using HomeBaseline.MaintenanceTui.Contracts;
using Spectre.Console;

namespace HomeBaseline.MaintenanceTui.Presentation;

public enum MaintenanceLayout
{
    Enhanced,
    Compact,
    Linear,
}

public sealed class LiveMaintenanceView
{
    private readonly Func<DateTimeOffset> _clock;
    private DateTimeOffset? _lastRender;

    public LiveMaintenanceView()
        : this(() => DateTimeOffset.UtcNow)
    {
    }

    public LiveMaintenanceView(Func<DateTimeOffset> clock)
    {
        _clock = clock;
    }

    public static TimeSpan MinimumRefreshInterval { get; } = TimeSpan.FromMilliseconds(100);

    public static MaintenanceLayout SelectLayout(int width) =>
        width switch
        {
            < 40 => MaintenanceLayout.Linear,
            < 100 => MaintenanceLayout.Compact,
            _ => MaintenanceLayout.Enhanced,
        };

    public bool ShouldRender()
    {
        var now = _clock();
        if (_lastRender is not null && now - _lastRender < MinimumRefreshInterval)
        {
            return false;
        }

        _lastRender = now;
        return true;
    }

    public bool RenderEvent(IAnsiConsole console, MaintenanceEvent maintenanceEvent)
    {
        ArgumentNullException.ThrowIfNull(console);
        if (!ShouldRender())
        {
            return false;
        }
        var phase = maintenanceEvent.PhaseId ?? "run";
        console.MarkupLine(
            $"[grey]{maintenanceEvent.Sequence}[/] " +
            $"{MarkupText.Escape(phase)} " +
            $"[bold]{MarkupText.Escape(maintenanceEvent.Status)}[/] " +
            $"{MarkupText.Escape(maintenanceEvent.MessageDe)} / " +
            $"{MarkupText.Escape(maintenanceEvent.MessageEn)}");
        return true;
    }
}
