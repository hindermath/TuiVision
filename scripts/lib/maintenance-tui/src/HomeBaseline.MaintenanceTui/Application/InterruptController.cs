namespace HomeBaseline.MaintenanceTui.Application;

public sealed class InterruptController : IDisposable
{
    private readonly CancellationTokenSource _source = new();
    private int _requests;

    public CancellationToken Token => _source.Token;

    public int RequestCount => Volatile.Read(ref _requests);

    public bool RequestInterrupt()
    {
        if (Interlocked.Increment(ref _requests) != 1)
        {
            return false;
        }

        _source.Cancel();
        return true;
    }

    public void Dispose() => _source.Dispose();
}
