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
    }

    /// <summary>
    /// Der aktuelle Hinterpuffer, in den Ansichten ihre Darstellung schreiben.
    ///
    /// The current back buffer into which views write their output.
    /// </summary>
    public TConsoleBuffer BackBuffer { get; private set; }

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
