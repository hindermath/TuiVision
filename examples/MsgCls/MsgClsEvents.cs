// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Examples.MsgCls;

/// <summary>
/// Definiert die anwendungsspezifischen Befehls-IDs für das MsgCls-Beispiel.
/// Diese Werte werden als Broadcast-Nachrichten zwischen den Komponenten ausgetauscht.
///
/// Defines the application-specific command IDs for the MsgCls example.
/// These values are exchanged as broadcast messages between components.
/// </summary>
public static class MsgClsEvents
{
    /// <summary>
    /// Broadcast-Befehl zum Auffinden des Nachrichtenfensters.
    /// Wird von anderen Komponenten gesendet, um eine Referenz auf das Fenster zu ermitteln.
    ///
    /// Broadcast command to locate the message window.
    /// Sent by other components to discover a reference to the window.
    /// </summary>
    public const ushort cmFindMsgWindow = 200;

    /// <summary>
    /// Broadcast-Befehl zum Senden einer Nachricht an das Nachrichtenfenster.
    /// Der <c>Info</c>-Slot des Ereignisses enthält den Nachrichtentext als <see cref="string"/>.
    ///
    /// Broadcast command to post a message to the message window.
    /// The <c>Info</c> slot of the event carries the message text as a <see cref="string"/>.
    /// </summary>
    public const ushort cmPostToMsgWindow = 201;

    /// <summary>
    /// Menü-Befehl zum Senden des vorgegebenen Lorem-Ipsum-Texts an das Nachrichtenfenster.
    ///
    /// Menu command to post the predefined Lorem Ipsum text to the message window.
    /// </summary>
    public const ushort cmPostLoremIpsum = 202;
}
