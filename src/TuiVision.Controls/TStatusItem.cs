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
    /// <param name="keyCode">Der explizite Tastencode oder 0. / The explicit key code or zero.</param>
    public TStatusItem(string name, int command, TStatusItem? next = null, ushort keyCode = 0)
    {
        Name = name;
        Command = command;
        Next = next;
        KeyCode = keyCode;
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
    /// Der explizite Tastencode für eine strukturierte A11Y-Abfrage oder 0,
    /// wenn der historische Eintrag nur einen Anzeigehinweis enthält.
    ///
    /// The explicit key code for a structured accessibility query, or zero
    /// when the historical item only contains a display hint.
    /// </summary>
    public ushort KeyCode { get; }

    /// <summary>
    /// Der nächste Status-Eintrag. / The next status item.
    /// </summary>
    public TStatusItem? Next { get; }

    /// <summary>
    /// Gibt an, ob der Eintrag aktiviert ist. / Indicates whether the item is enabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gibt an, ob der aktuelle Command-Kontext den Eintrag deaktiviert.
    /// Der manuelle <see cref="Disabled"/>-Wert bleibt davon unberührt.
    ///
    /// Indicates whether the current command context disables the item.
    /// The manual <see cref="Disabled"/> value remains unchanged.
    /// </summary>
    public bool ContextDisabled { get; internal set; }

    /// <summary>
    /// Gibt die wirksame Sperre aus manueller und kontextabhängiger Vorgabe zurück.
    ///
    /// Returns the effective disablement from manual and context constraints.
    /// </summary>
    public bool IsEffectivelyDisabled => Disabled || ContextDisabled;
}
