// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Examples.SmokeTests;

/// <summary>
/// Beschreibt eine deterministische Ereignisfolge fuer interaktive Beispiel-Smokes.
/// Die Folge endet bewusst nicht selbst mit Quit; die Beispiel-Apps haengen im
/// Headless-Modus nach der Queue automatisch <see cref="ShellCommandIds.cmQuit"/> an.
///
/// Describes a deterministic event sequence for interactive example smoke tests.
/// The sequence intentionally does not append Quit itself; in headless mode the
/// example apps automatically append <see cref="ShellCommandIds.cmQuit"/> after the queue.
/// </summary>
public sealed class InteractiveSmokeEventScript
{
    private readonly TEvent[] _events;

    private InteractiveSmokeEventScript(IEnumerable<TEvent> events) => _events = events.ToArray();

    /// <summary>
    /// Die zu injizierenden Ereignisse.
    ///
    /// The events to inject. The deterministic quit path is intentionally owned
    /// by each headless example after this script has been drained.
    /// </summary>
    public IReadOnlyList<TEvent> Events => _events;

    /// <summary>
    /// Erstellt eine Befehlsfolge.
    ///
    /// Creates a command sequence.
    /// </summary>
    /// <param name="commands">Die Befehls-IDs. / The command IDs.</param>
    /// <returns>Die Ereignisfolge. / The event sequence.</returns>
    public static InteractiveSmokeEventScript Commands(params ushort[] commands) =>
        new(commands.Select(command => TEvent.CreateCommand(command)));

    /// <summary>
    /// Erstellt eine Ereignisfolge aus vorhandenen Ereignissen.
    ///
    /// Creates an event sequence from existing events.
    /// </summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    /// <returns>Die Ereignisfolge. / The event sequence.</returns>
    public static InteractiveSmokeEventScript FromEvents(params TEvent[] events) => new(events);

    /// <summary>
    /// Erzeugt eine neue Folge mit zusaetzlichen Befehlen am Ende.
    ///
    /// Creates a new sequence with additional commands appended.
    /// </summary>
    /// <param name="commands">Die Befehls-IDs. / The command IDs.</param>
    /// <returns>Die erweiterte Ereignisfolge. / The extended event sequence.</returns>
    public InteractiveSmokeEventScript ThenCommands(params ushort[] commands) =>
        new(_events.Concat(commands.Select(command => TEvent.CreateCommand(command))));

    /// <summary>
    /// Erzeugt eine neue Folge mit zusaetzlichen Ereignissen am Ende.
    ///
    /// Creates a new sequence with additional events appended.
    /// </summary>
    /// <param name="events">Die Ereignisse. / The events.</param>
    /// <returns>Die erweiterte Ereignisfolge. / The extended event sequence.</returns>
    public InteractiveSmokeEventScript ThenEvents(params TEvent[] events) => new(_events.Concat(events));
}
