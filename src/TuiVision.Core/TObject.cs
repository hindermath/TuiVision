namespace TuiVision.Core;

/// <summary>
/// Base object for ported Turbo Vision types.
/// </summary>
public class TObject
{
    /// <summary>
    /// Performs explicit shutdown work before the object is released.
    /// </summary>
    public virtual void ShutDown()
    {
    }

    /// <summary>
    /// Calls <see cref="ShutDown"/> and disposes the instance when supported.
    /// </summary>
    public static void Destroy(TObject? instance)
    {
        if (instance is null)
        {
            return;
        }

        instance.ShutDown();
        if (instance is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
