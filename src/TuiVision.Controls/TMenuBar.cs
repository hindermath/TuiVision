// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Core;

namespace TuiVision.Controls;

/// <summary>
/// Die Menüleiste am oberen Bildschirmrand.
/// Zeigt das Hauptmenü der Anwendung an.
///
/// The menu bar at the top of the screen.
/// Displays the main menu of the application.
/// </summary>
public class TMenuBar : TView
{
    /// <summary>
    /// Die Liste der Menüpunkte. / The list of menu items.
    /// </summary>
    public TMenuItem? Menu { get; set; }

    /// <summary>
    /// Gibt an, ob die Menüleiste gerade aktiv (geöffnet) ist.
    ///
    /// Indicates whether the menu bar is currently active (open).
    /// </summary>
    public bool IsMenuActive { get; private set; }

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TMenuBar"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TMenuBar"/> class.
    /// </summary>
    /// <param name="bounds">Die Grenzen der Menüleiste. / The bounds of the menu bar.</param>
    public TMenuBar(TRect bounds) : base(bounds)
    {
        // Menüleiste empfängt Tastatur-Ereignisse vor dem fokussierten Kind (F10-Aktivierung).
        // Menu bar receives keyboard events before the focused child (F10 activation).
        Options |= TViewOptions.PreProcess;
    }

    /// <summary>
    /// Verarbeitet ein Ereignis. Aktiviert die Menüleiste bei F10, reagiert auf Pfeiltasten.
    ///
    /// Processes an event. Activates the menu bar on F10, reacts to arrow keys.
    /// </summary>
    /// <param name="event">Das zu verarbeitende Ereignis. / The event to process.</param>
    public override void HandleEvent(TEvent @event)
    {
        base.HandleEvent(@event);

        if (@event.What == TEventKind.KeyDown)
        {
            // F10 (ScanCode 0x44) oder Alt-Taste (ShiftState-Bit 0x0004) aktiviert die Menüleiste.
            // F10 (ScanCode 0x44) or Alt key (ShiftState bit 0x0004) toggles menu bar activation.
            bool isF10 = @event.KeyDown.ScanCode == 0x44;
            bool isAlt = (@event.KeyDown.ShiftState & 0x0004) != 0;
            if (isF10 || isAlt)
            {
                IsMenuActive = !IsMenuActive;
                @event.Clear();
            }
        }
    }
}
