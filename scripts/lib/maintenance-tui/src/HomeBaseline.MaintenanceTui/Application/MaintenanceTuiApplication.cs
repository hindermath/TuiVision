using HomeBaseline.MaintenanceTui.Contracts;
using HomeBaseline.MaintenanceTui.Infrastructure;

namespace HomeBaseline.MaintenanceTui.Application;

public sealed class MaintenanceTuiApplication
{
    private readonly IMaintenanceProcessRunner _processRunner;
    private readonly RunResultReconciler _reconciler;

    public MaintenanceTuiApplication(
        IMaintenanceProcessRunner processRunner,
        RunResultReconciler reconciler)
    {
        _processRunner = processRunner;
        _reconciler = reconciler;
    }

    public async Task<ReconciledRunResult> ExecuteAsync(
        ProcessInvocation invocation,
        CancellationToken cancellationToken)
    {
        var execution = await _processRunner.RunAsync(invocation, cancellationToken);
        return _reconciler.Reconcile(
            invocation.RunId,
            execution.ExitCode,
            execution.Report,
            execution.CompletionEvent);
    }
}
