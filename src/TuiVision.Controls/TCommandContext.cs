// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Collections.ObjectModel;

namespace TuiVision.Controls;

/// <summary>
/// Benennt den Auslöser einer Command-Kontext-Aktualisierung.
///
/// Names the trigger of a command-context refresh.
/// </summary>
public enum CommandContextRefreshTrigger
{
    /// <summary>Der Fokus hat sich geändert. / Focus changed.</summary>
    Focus,

    /// <summary>Ein Ereignis wurde verarbeitet. / An event was handled.</summary>
    EventHandled,

    /// <summary>Eine leere Poll-Runde hat Idle ausgeführt. / An empty poll cycle ran Idle.</summary>
    Idle,

    /// <summary>Ein Befehl wird unmittelbar vor Dispatch erneut geprüft. / A command is rechecked immediately before dispatch.</summary>
    PreDispatch
}

/// <summary>
/// Enthält eine unveränderliche Momentaufnahme der Command-Verfügbarkeit für
/// genau eine Aktualisierung der Anwendungsschleife.
///
/// Contains an immutable snapshot of command availability for exactly one
/// application-loop refresh.
/// </summary>
public sealed class TCommandContext
{
    private readonly IReadOnlyDictionary<ushort, bool> _commandStates;

    /// <summary>
    /// Initialisiert eine unveränderliche Command-Momentaufnahme.
    ///
    /// Initializes an immutable command snapshot.
    /// </summary>
    /// <param name="generation">Die monoton steigende Laufgeneration. / The monotonically increasing run generation.</param>
    /// <param name="activeView">Die tiefste fokussierte View oder <c>null</c>. / The deepest focused view or <c>null</c>.</param>
    /// <param name="commandStates">Die ermittelten Command-Zustände. / The discovered command states.</param>
    /// <param name="trigger">Der Aktualisierungsauslöser. / The refresh trigger.</param>
    /// <exception cref="ArgumentNullException">
    /// Wird bei fehlenden Command-Zuständen ausgelöst. / Thrown when command states are missing.
    /// </exception>
    public TCommandContext(
        long generation,
        TView? activeView,
        IReadOnlyDictionary<ushort, bool> commandStates,
        CommandContextRefreshTrigger trigger)
    {
        ArgumentNullException.ThrowIfNull(commandStates);
        Generation = generation;
        ActiveView = activeView;
        Trigger = trigger;
        _commandStates = new ReadOnlyDictionary<ushort, bool>(new Dictionary<ushort, bool>(commandStates));
    }

    /// <summary>Die monoton steigende Laufgeneration. / The monotonically increasing run generation.</summary>
    public long Generation { get; }

    /// <summary>Die tiefste fokussierte View oder <c>null</c>. / The deepest focused view or <c>null</c>.</summary>
    public TView? ActiveView { get; }

    /// <summary>Der Aktualisierungsauslöser. / The refresh trigger.</summary>
    public CommandContextRefreshTrigger Trigger { get; }

    /// <summary>Die unveränderlichen Command-Zustände. / The immutable command states.</summary>
    public IReadOnlyDictionary<ushort, bool> CommandStates => _commandStates;

    /// <summary>
    /// Prüft einen Befehl. Nicht registrierte Befehle bleiben aus Kompatibilitätsgründen aktiviert.
    ///
    /// Checks a command. Unregistered commands remain enabled for compatibility.
    /// </summary>
    /// <param name="command">Die Command-ID. / The command identifier.</param>
    /// <returns><c>true</c>, wenn der Befehl verfügbar ist. / <c>true</c> when the command is available.</returns>
    public bool IsEnabled(ushort command) => !_commandStates.TryGetValue(command, out bool enabled) || enabled;
}
