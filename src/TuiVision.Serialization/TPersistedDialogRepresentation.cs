// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

namespace TuiVision.Serialization;

/// <summary>
/// Formatkonstanten fuer persistierte Dialogbeschreibungen.
///
/// Format constants for persisted dialog descriptions.
/// </summary>
public static class PersistedDialogRepresentation
{
    /// <summary>Aktuelle Formatversion. / Current format version.</summary>
    public const int CurrentFormatVersion = 1;

    /// <summary>Stabile Type-ID fuer Dialogbeschreibungs-Records. / Stable type id for dialog-description records.</summary>
    public const string TypeId = "tuivision.dialog-description.v1";
}
