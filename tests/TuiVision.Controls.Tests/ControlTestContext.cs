// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using System.Reflection;
using TuiVision.Controls;
using TuiVision.Core;

namespace TuiVision.Controls.Tests;

/// <summary>
/// Gemeinsamer Testkontext für neue Control-Layer-Tests.
/// Die Hilfsmethoden kapseln Standard-Bounds, Owner-Verdrahtung und den Zugriff
/// auf den internen Zeichenpuffer einer exponierten <see cref="TGroup"/>.
///
/// Shared test context for new control-layer tests.
/// The helpers encapsulate standard bounds, owner wiring, and access to the
/// internal draw buffer of an exposed <see cref="TGroup"/>.
/// </summary>
public static class ControlTestContext
{
    /// <summary>
    /// Reflektierter Zugriff auf das private Feld <c>_buffer</c> von <see cref="TGroup"/>.
    ///
    /// Reflected access to the private <c>_buffer</c> field of <see cref="TGroup"/>.
    /// </summary>
    private static readonly FieldInfo BufferField =
        typeof(TGroup).GetField("_buffer", BindingFlags.Instance | BindingFlags.NonPublic)
        ?? throw new InvalidOperationException("The private TGroup buffer field could not be located.");

    /// <summary>
    /// Erstellt ein Standard-Rechteck mit Ursprung (0,0).
    ///
    /// Creates a standard rectangle with origin (0,0).
    /// </summary>
    /// <param name="width">Die gewünschte Breite. / The requested width.</param>
    /// <param name="height">Die gewünschte Höhe. / The requested height.</param>
    /// <returns>Ein neues Rechteck mit den angegebenen Maßen. / A new rectangle with the requested dimensions.</returns>
    public static TRect CreateBounds(short width = 20, short height = 5) => new(0, 0, width, height);

    /// <summary>
    /// Erstellt eine exponierte Testgruppe mit aktiviertem Puffer.
    ///
    /// Creates an exposed test group with buffer allocation enabled.
    /// </summary>
    /// <param name="bounds">Die Bounds der Gruppe. / The bounds for the group.</param>
    /// <returns>Eine testbereite Gruppe. / A group ready for testing.</returns>
    public static TGroup CreateExposedGroup(TRect bounds)
    {
        TGroup group = new(bounds);
        group.SetState(TViewState.Exposed, true);
        return group;
    }

    /// <summary>
    /// Fügt eine View in eine exponierte Eigentümergruppe ein und allokiert deren Puffer.
    ///
    /// Inserts a view into an exposed owner group and allocates its backing buffer.
    /// </summary>
    /// <param name="view">Die zu verdrahtende View. / The view to wire up.</param>
    /// <param name="ownerBounds">Optionale Bounds für die Eigentümergruppe. / Optional bounds for the owner group.</param>
    /// <returns>Die Eigentümergruppe der View. / The owner group of the view.</returns>
    public static TGroup AttachToOwner(TView view, TRect? ownerBounds = null)
    {
        ArgumentNullException.ThrowIfNull(view);

        TGroup owner = CreateExposedGroup(ownerBounds ?? CreateBounds(40, 10));
        owner.Insert(view);
        owner.Draw();
        return owner;
    }

    /// <summary>
    /// Liefert eine Klon-Kopie des internen Gruppenpuffers für Assertions.
    ///
    /// Returns a cloned copy of the internal group buffer for assertions.
    /// </summary>
    /// <param name="group">Die zu inspizierende Gruppe. / The group to inspect.</param>
    /// <returns>Ein geklonter Zeichenpuffer. / A cloned console buffer.</returns>
    public static TConsoleBuffer GetBufferSnapshot(TGroup group)
    {
        ArgumentNullException.ThrowIfNull(group);

        group.Draw();
        TConsoleBuffer? buffer = BufferField.GetValue(group) as TConsoleBuffer;
        if (buffer is null)
        {
            throw new InvalidOperationException("The control test group has not allocated a console buffer.");
        }

        return buffer.Clone();
    }
}
