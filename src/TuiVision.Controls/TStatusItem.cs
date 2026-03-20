// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Controls;

/// <summary>
/// Repräsentiert einen Statuszeilen-Eintrag mit einem Anzeigenamen und einer Befehl-ID.
///
/// Represents a status line item with a display name and a command ID.
/// </summary>
public sealed class TStatusItem
{
    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="TStatusItem"/>-Klasse.
    ///
    /// Initializes a new instance of the <see cref="TStatusItem"/> class.
    /// </summary>
    /// <param name="name">Der Anzeigename des Eintrags. / The display name of the item.</param>
    /// <param name="command">Die zugehörige Befehl-ID. / The associated command ID.</param>
    /// <param name="next">Der nächste Eintrag in der Statuszeile. / The next item in the status line.</param>
    public TStatusItem(string name, int command, TStatusItem? next = null)
    {
        Name = name;
        Command = command;
        Next = next;
    }

    /// <summary>
    /// Der Anzeigename. / The display name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Die Befehl-ID. / The command ID.
    /// </summary>
    public int Command { get; }

    /// <summary>
    /// Der nächste Status-Eintrag. / The next status item.
    /// </summary>
    public TStatusItem? Next { get; }

    /// <summary>
    /// Gibt an, ob der Eintrag aktiviert ist. / Indicates whether the item is enabled.
    /// </summary>
    public bool Disabled { get; set; }
}
