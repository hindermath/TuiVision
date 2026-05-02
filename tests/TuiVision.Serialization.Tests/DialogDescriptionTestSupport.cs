// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Serialization.Tests;

internal static class DialogDescriptionTestSupport
{
    public static TDialogDescriptionRecord CreateMinimalRecord() => new(
        PersistedDialogRepresentation.CurrentFormatVersion,
        "minimal",
        1,
        "Minimal",
        [new TDialogControlDescriptionRecord("name", "input-line", "Name", "Ada", true)],
        ["name"],
        [new TDialogCommandBindingRecord(1, "name", "confirm", "Enter")]);

    public static byte[] CreateMalformedEnvelope()
    {
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        byte[] data = serializer.Serialize(PersistedDialogRepresentation.TypeId, CreateMinimalRecord());
        return data[..^1];
    }
}
