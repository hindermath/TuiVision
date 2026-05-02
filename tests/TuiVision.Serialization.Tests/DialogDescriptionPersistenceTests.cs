// Copyright (c) 2026 Thorsten Hindermann / TuiVision Contributors.
// Licensed under the MIT Licence. See LICENSE file in the project root for full licence information.

using TuiVision.Serialization;

namespace TuiVision.Serialization.Tests;

/// <summary>
/// Tests fuer persistierte Dialogbeschreibungen.
///
/// Tests for persisted dialog descriptions.
/// </summary>
[TestClass]
public sealed class DialogDescriptionPersistenceTests
{
    /// <summary>
    /// Prueft den minimalen Roundtrip einer Dialogbeschreibung.
    ///
    /// Verifies the minimal roundtrip of a dialog description.
    /// </summary>
    [TestMethod]
    public void TDialogDescriptionRecord_RoundtripsMinimalDescription()
    {
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        TDialogDescriptionRecord source = DialogDescriptionTestSupport.CreateMinimalRecord();

        byte[] data = serializer.Serialize(PersistedDialogRepresentation.TypeId, source);
        TDialogDescriptionRecord restored = serializer.Deserialize<TDialogDescriptionRecord>(data);

        Assert.AreEqual(source.DescriptionId, restored.DescriptionId);
        Assert.AreEqual(source.Title, restored.Title);
        Assert.IsFalse(restored.RuntimeStateIncluded);
        Assert.HasCount(1, restored.Controls);
    }

    /// <summary>
    /// Prueft, dass abgeschnittene Payloads abgelehnt werden.
    ///
    /// Verifies that truncated payloads are rejected.
    /// </summary>
    [TestMethod]
    public void TDialogDescriptionRecord_TruncatedPayload_IsRejected()
    {
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);

        Assert.ThrowsExactly<InvalidDataException>(() => serializer.Deserialize<TDialogDescriptionRecord>(DialogDescriptionTestSupport.CreateMalformedEnvelope()));
    }

    /// <summary>
    /// Prueft, dass nicht unterstuetzte Formatversionen abgelehnt werden.
    ///
    /// Verifies that unsupported format versions are rejected.
    /// </summary>
    [TestMethod]
    public void TDialogDescriptionRecord_UnsupportedVersion_IsRejected()
    {
        TDialogDescriptionRecord source = DialogDescriptionTestSupport.CreateMinimalRecord();
        TDialogDescriptionRecord record = new(
            999,
            source.DescriptionId,
            source.ModelVersion,
            source.Title,
            source.Controls,
            source.NavigationOrder,
            source.CommandBindings);
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        byte[] data = serializer.Serialize(PersistedDialogRepresentation.TypeId, record);

        Assert.ThrowsExactly<InvalidDataException>(() => serializer.Deserialize<TDialogDescriptionRecord>(data));
    }

    /// <summary>
    /// Prueft, dass Runtime-Zustand in Persistenzdaten abgelehnt wird.
    ///
    /// Verifies that runtime state in persisted data is rejected.
    /// </summary>
    [TestMethod]
    public void TDialogDescriptionRecord_RuntimeState_IsRejected()
    {
        TDialogDescriptionRecord record = new(
            PersistedDialogRepresentation.CurrentFormatVersion,
            "runtime",
            1,
            "Runtime",
            [],
            [],
            [],
            runtimeStateIncluded: true);
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        byte[] data = serializer.Serialize(PersistedDialogRepresentation.TypeId, record);

        Assert.ThrowsExactly<InvalidDataException>(() => serializer.Deserialize<TDialogDescriptionRecord>(data));
    }

    /// <summary>
    /// Prueft, dass Restdaten im Envelope abgelehnt werden.
    ///
    /// Verifies that trailing envelope data is rejected.
    /// </summary>
    [TestMethod]
    public void TRecordSerializer_TrailingData_IsRejected()
    {
        TRecordRegistry registry = new();
        TDialogDescriptionRecord.Register(registry);
        TRecordSerializer serializer = new(registry);
        byte[] data = serializer.Serialize(PersistedDialogRepresentation.TypeId, DialogDescriptionTestSupport.CreateMinimalRecord());
        byte[] withTrailing = [.. data, 42];

        Assert.ThrowsExactly<InvalidDataException>(() => serializer.Deserialize<TDialogDescriptionRecord>(withTrailing));
    }
}
