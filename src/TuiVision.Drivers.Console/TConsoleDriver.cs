// Copyright (c) 2025 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Drivers.Console;

/// <summary>
/// Empfängt einen gerenderten Frame von <see cref="TConsoleDriver"/> und stellt ihn dar.
/// Implementierungen können den Frame auf die echte Konsole, einen Test-Spy oder einen
/// anderen Ausgabekanal schreiben.
///
/// Receives a rendered frame from <see cref="TConsoleDriver"/> and presents it.
/// Implementations can write the frame to the real console, a test spy,
/// or any other output channel.
/// </summary>
public interface IConsolePresenter
{
    /// <summary>
    /// Stellt den angegebenen Frame dar.
    ///
    /// Presents the specified frame.
    /// </summary>
    /// <param name="frame">
    /// Der darzustellende Frame als unveränderlicher Puffer-Schnappschuss.
    /// The frame to present as an immutable buffer snapshot.
    /// </param>
    void Present(TConsoleBuffer frame);
}

/// <summary>
/// Verwaltet den aktiven Hinterpuffer und veröffentlicht unveränderliche Frame-Schnappschüsse
/// über das Presenter-Pattern. Rendering-Backends sind dadurch austauschbar.
///
/// Manages the active back buffer and publishes immutable frame snapshots
/// via the presenter pattern. Rendering backends are therefore interchangeable.
/// </summary>
public sealed class TConsoleDriver
{
    private readonly Queue<ConsoleMouseObservation> _mouseObservations = new();

    /// <summary>
    /// Erstellt einen neuen Treiber mit einem Hinterpuffer der angegebenen Größe.
    ///
    /// Creates a new driver with a back buffer of the specified size.
    /// </summary>
    /// <param name="width">
    /// Die Breite des Hinterpuffers in Zeichen. Muss positiv sein.
    /// The width of the back buffer in characters. Must be positive.
    /// </param>
    /// <param name="height">
    /// Die Höhe des Hinterpuffers in Zeichen. Muss positiv sein.
    /// The height of the back buffer in characters. Must be positive.
    /// </param>
    public TConsoleDriver(int width, int height)
    {
        BackBuffer = new TConsoleBuffer(width, height);
        MouseIngress = new ConsoleMouseIngress(new ConsoleMouseCapability(
            ConsoleMouseCapabilityState.Disabled,
            ConsoleMouseHostFamily.Unknown,
            ConsoleMouseProtocol.None,
            "Mouse ingress has not been enabled."));
    }

    /// <summary>
    /// Der aktuelle Hinterpuffer, in den Ansichten ihre Darstellung schreiben.
    ///
    /// The current back buffer into which views write their output.
    /// </summary>
    public TConsoleBuffer BackBuffer { get; private set; }

    /// <summary>
    /// Der begrenzte Host-Mauseingang dieses Treibers.
    ///
    /// The bounded host mouse ingress owned by this driver.
    /// </summary>
    public ConsoleMouseIngress MouseIngress { get; }

    /// <summary>
    /// Gibt an, ob eine kontrollierte Host-Beobachtung auf Verarbeitung wartet.
    ///
    /// Indicates whether a controlled host observation is waiting to be processed.
    /// </summary>
    public bool HasPendingMouseObservation => _mouseObservations.Count > 0;

    /// <summary>
    /// Reiht eine vollständige kontrollierte Host-Beobachtung für den nächsten
    /// Ereignisabruf ein.
    ///
    /// Queues one complete controlled host observation for the next event retrieval.
    /// </summary>
    /// <param name="observation">Einzureihende Beobachtung. / Observation to queue.</param>
    public void QueueMouseObservation(ConsoleMouseObservation observation)
    {
        ArgumentNullException.ThrowIfNull(observation.Sequence);
        _mouseObservations.Enqueue(observation);
    }

    /// <summary>
    /// Verarbeitet die nächste kontrollierte Beobachtung gegen die aktuelle
    /// Puffergröße.
    ///
    /// Processes the next controlled observation against the current buffer size.
    /// </summary>
    /// <param name="targetResolver">Optionaler Zielschlüssel-Auflöser. / Optional target-key resolver.</param>
    /// <param name="event">Kanonisches Ereignis oder `Nothing`. / Canonical event or `Nothing`.</param>
    /// <param name="rejectionReason">Ablehnungsgrund. / Rejection reason.</param>
    /// <returns><c>true</c>, wenn ein Ereignis veröffentlicht wurde. / <c>true</c> when an event was published.</returns>
    public bool TryGetMouseEvent(
        Func<TPoint, string?>? targetResolver,
        out TEvent @event,
        out ConsoleMouseRejectionReason rejectionReason)
    {
        if (_mouseObservations.Count == 0)
        {
            @event = TEvent.CreateNone();
            rejectionReason = ConsoleMouseRejectionReason.None;
            return false;
        }

        ConsoleMouseObservation observation = _mouseObservations.Dequeue();
        return MouseIngress.TryAccept(
            observation.Sequence,
            observation.TimestampMilliseconds,
            BackBuffer.Width,
            BackBuffer.Height,
            targetResolver,
            out @event,
            out rejectionReason);
    }

    /// <summary>
    /// Ändert die Größe des Hinterpuffers und kopiert den sichtbaren Inhalt des alten Puffers.
    /// Zellen außerhalb des neuen Bereichs werden verworfen; neue Zellen sind leer.
    ///
    /// Resizes the back buffer and copies the visible content from the old buffer.
    /// Cells outside the new area are discarded; new cells are empty.
    /// </summary>
    /// <param name="width">Die neue Breite in Zeichen. Muss positiv sein. / The new width in characters. Must be positive.</param>
    /// <param name="height">Die neue Höhe in Zeichen. Muss positiv sein. / The new height in characters. Must be positive.</param>
    public void Resize(int width, int height)
    {
        TConsoleBuffer resized = new(width, height);
        int copyWidth = Math.Min(width, BackBuffer.Width);
        int copyHeight = Math.Min(height, BackBuffer.Height);

        for (int y = 0; y < copyHeight; y++)
        {
            for (int x = 0; x < copyWidth; x++)
            {
                resized.SetCell(x, y, BackBuffer.GetCell(x, y));
            }
        }

        BackBuffer = resized;
    }

    /// <summary>
    /// Übergibt einen unveränderlichen Schnappschuss des Hinterpuffers an den Presenter.
    /// Der Presenter erhält eine Kopie, damit er das Original nicht versehentlich verändern kann.
    ///
    /// Passes an immutable snapshot of the back buffer to the presenter.
    /// The presenter receives a copy so it cannot accidentally modify the original.
    /// </summary>
    /// <param name="presenter">
    /// Der Presenter, der den Frame darstellt. Darf nicht <c>null</c> sein.
    /// The presenter that renders the frame. Must not be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Wird ausgelöst, wenn <paramref name="presenter"/> <c>null</c> ist.
    /// Thrown when <paramref name="presenter"/> is <c>null</c>.
    /// </exception>
    public void Present(IConsolePresenter presenter)
    {
        ArgumentNullException.ThrowIfNull(presenter);
        presenter.Present(BackBuffer.Clone());
    }
}
