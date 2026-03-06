namespace TuiVision.Core;

/// <summary>
/// Basisklasse für alle portierten Turbo-Vision-Typen.
/// Sie legt den grundlegenden Lebenszyklus-Vertrag fest: explizites Herunterfahren
/// und optionale Ressourcenfreigabe über <see cref="IDisposable"/>.
///
/// Base class for all ported Turbo Vision types.
/// It defines the fundamental lifecycle contract: explicit shutdown
/// and optional resource release via <see cref="IDisposable"/>.
/// </summary>
public class TObject
{
    /// <summary>
    /// Führt explizite Aufräumarbeiten durch, bevor das Objekt freigegeben wird.
    /// Abgeleitete Klassen überschreiben diese Methode, um eigene Ressourcen zu bereinigen.
    ///
    /// Performs explicit cleanup work before the object is released.
    /// Derived classes override this method to clean up their own resources.
    /// </summary>
    public virtual void ShutDown()
    {
    }

    /// <summary>
    /// Ruft <see cref="ShutDown"/> auf und gibt die Instanz frei,
    /// wenn sie <see cref="IDisposable"/> implementiert. Null-Referenzen werden sicher ignoriert.
    ///
    /// Calls <see cref="ShutDown"/> and disposes the instance when it implements
    /// <see cref="IDisposable"/>. Null references are safely ignored.
    /// </summary>
    /// <param name="instance">
    /// Die freizugebende Instanz oder <c>null</c>.
    /// The instance to release, or <c>null</c>.
    /// </param>
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
